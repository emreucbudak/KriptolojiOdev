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

                    string operation = parts[1];
                    string algorithm = parts[2].ToUpperInvariant();

                    string text = transportService.Decrypt(parts[3]);
                    string xorDecKey = parts.Length > 4 ? transportService.Decrypt(parts[4]) : string.Empty;
                    string iv = parts.Length > 5 ? transportService.Decrypt(parts[5]) : string.Empty;

                    string actualKey = xorDecKey;

                    if ((algorithm == "AES" || algorithm == "DES" || algorithm == "MANUEL_DES") && !string.IsNullOrEmpty(xorDecKey))
                    {
                        try
                        {
                            actualKey = decryptor.RsaDecrypt(xorDecKey, ServerPrivateKey);
                        }
                        catch
                        {
                            actualKey = xorDecKey;
                        }
                    }

                    string resultText = operation switch
                    {
                        "Encrypt" => decryptor.DecryptByAlgorithm(algorithm, text, actualKey, iv),
                        "Decrypt" => encryptor.AesEncrypt(text, actualKey, iv),
                        _ => "Hata"
                    };

                    OnMessage?.Invoke($"{target}|{algorithm}|{text}|{actualKey}|{resultText}");

                    string fullResponse = $"CLIENT|Response|{algorithm}|{transportService.Encrypt(resultText)}||";
                    byte[] responseData = Encoding.UTF8.GetBytes(fullResponse);
                    stream.Write(responseData, 0, responseData.Length);
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

        private string EncryptorServiceCaller(string algorithm, string metin, string? key, string? iv)
        {
            try
            {
                return algorithm switch
                {
                    "SUBSTITUTION" => encryptor.SubstitutionEncrypt(metin, key),
                    "VIGENERE" => encryptor.VigenereEncrypt(metin, key),
                    "AFFINE" => encryptor.AffineEncrypt(metin),
                    "CAESAR" => encryptor.CaesarEncrypt(metin),
                    "ROTA" => encryptor.RotaEncrypt(metin, key),
                    "COLUMNAR" => encryptor.ColumnarEncrypt(metin, key),
                    "POLYBIUS" => encryptor.PolybiusEncrypt(metin, key),
                    "PIGPEN" => encryptor.PigpenEncrypt(metin, key),
                    "HILL" => encryptor.HillEncrypt(metin, key),
                    "TRENRAYI" => encryptor.TrenRayiEncrypt(metin, key),
                    "AES" => encryptor.AesEncrypt(metin, key, iv),
                    "DES" => encryptor.DesEncrypt(metin, key, iv),
                    "RSA" => encryptor.RsaEncrypt(metin, key),
                    "MANUEL_DES" => encryptor.ManuelDesEncrypt(metin, key, iv),
                    _ => "Hata"
                };
            }
            catch (Exception ex) { return "Hata: " + ex.Message; }
        }

        private string DecryptorServiceCaller(string algorithm, string metin, string? key, string? iv)
        {
            try
            {
                return algorithm switch
                {
                    "SUBSTITUTION" => decryptor.DecryptorSubstitiuion(metin, key),
                    "VIGENERE" => decryptor.DecryptorVigenere(metin, key),
                    "AFFINE" => decryptor.DecryptorAffine(metin),
                    "CAESAR" => decryptor.DecryptorCaesar(metin),
                    "ROTA" => decryptor.RotaDecrypt(metin, key),
                    "COLUMNAR" => decryptor.ColumnarDecrypt(metin, key),
                    "POLYBIUS" => decryptor.PolybiusDecrypt(metin, key),
                    "PIGPEN" => decryptor.PigpenDecrypt(metin, key),
                    "HILL" => decryptor.HillDecrypt(metin, key),
                    "TRENRAYI" => decryptor.TrenRayiDecrypt(metin, key),
                    "AES" => decryptor.AesDecrypt(metin, key, iv),
                    "DES" => decryptor.DesDecrypt(metin, key, iv),
                    "RSA" => decryptor.RsaDecrypt(metin, key),
                    "MANUEL_DES" => decryptor.ManuelDesDecrypt(metin, key, iv),
                    _ => "Hata"
                };
            }
            catch (Exception ex) { return "Hata: " + ex.Message; }
        }
    }
}