using KriptolojiOdev.Baglanti.Interface;
using KriptolojiOdev.Sifreleme.Class;
using KriptolojiOdev.Sifreleme.Interface;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace KriptolojiOdev.Baglanti.Class
{
    public class ConnectionService : IConnectionService
    {
        private readonly IPAddress ip = IPAddress.Parse("127.0.0.1");
        private int port = 8080;
        private TcpListener listener;
        private Thread thread;
        private TcpClient client;
        private List<TcpClient> connectedClients = new List<TcpClient>();

        private IEncryptorService encryptor = new EncryptorService();
        private IDecryptorService decryptor = new DecryptorService();
        private ITransportSecurityService transportService = new TransportSecurityService();

        public Action<string> OnMessage { get; set; }
        public string ServerPrivateKey { get; set; }

        public async Task<(string message, TcpClient client)> ConnectToServer()
        {
            try
            {
                client = new TcpClient();
                await client.ConnectAsync("127.0.0.1", 8080);
                return ("Server'a bağlandı!\n", client);
            }
            catch (Exception ex)
            {
                return ("Hata: " + ex.Message + Environment.NewLine, client);
            }
        }

        public string StartServer()
        {
            try
            {
                listener = new TcpListener(ip, port);
                listener.Start();
                thread = new Thread(ListenForClients);
                thread.IsBackground = true;
                thread.Start();
                return "Server " + ip + ":" + port + " Başlatıldı." + Environment.NewLine;
            }
            catch (Exception ex)
            {
                return "Hata: " + ex.Message + Environment.NewLine;
            }
        }

        private void ListenForClients()
        {
            while (true)
            {
                try
                {
                    TcpClient client = listener.AcceptTcpClient();
                    connectedClients.Add(client);
                    OnMessage?.Invoke("Yeni client bağlandı!");
                    Thread clientThread = new Thread(() => HandleClient(client));
                    clientThread.IsBackground = true;
                    clientThread.Start();
                }
                catch { break; }
            }
        }

        private void HandleClient(TcpClient client)
        {
            try
            {
                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[8192];
                int bytesRead;

                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    string incomingMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    string[] parts = incomingMessage.Split('|');

                    if (parts.Length < 4) continue;

                    string target = parts[0];
                    if (target != "SUNUCU") continue;

                    string algorithm = parts[2].ToUpperInvariant();
                    string cipherText = transportService.Decrypt(parts[3]);
                    string securedKey = parts.Length > 4 ? transportService.Decrypt(parts[4]) : string.Empty;
                    string securedIv = parts.Length > 5 ? transportService.Decrypt(parts[5]) : string.Empty;

                    string actualKey = securedKey;
                    if ((algorithm == "AES" || algorithm == "DES" || algorithm == "MANUEL_DES") && !string.IsNullOrEmpty(securedKey))
                    {
                        try
                        {
                            actualKey = decryptor.RsaDecrypt(securedKey, ServerPrivateKey);
                        }
                        catch
                        {
                            actualKey = securedKey;
                        }
                    }

                    string resultText = decryptor.DecryptByAlgorithm(algorithm, cipherText, actualKey, securedIv);

                    OnMessage?.Invoke($"SUNUCU|MESSAGE|{algorithm}|{resultText}");
                }
            }
            catch { }
            finally
            {
                connectedClients.Remove(client);
                client.Close();
            }
        }

        public void Broadcast(string target, string operation, string algorithm, string text, string key, string iv)
        {
            string securedText = transportService.Encrypt(text);
            string securedKey = string.IsNullOrEmpty(key) ? "" : transportService.Encrypt(key);
            string securedIV = string.IsNullOrEmpty(iv) ? "" : transportService.Encrypt(iv);

            string msg = $"{target}|{operation}|{algorithm}|{securedText}|{securedKey}|{securedIV}";
            byte[] data = Encoding.UTF8.GetBytes(msg);

            foreach (var c in connectedClients.ToList())
            {
                try
                {
                    if (c.Connected)
                    {
                        c.GetStream().Write(data, 0, data.Length);
                    }
                }
                catch { connectedClients.Remove(c); }
            }
        }
    }
}