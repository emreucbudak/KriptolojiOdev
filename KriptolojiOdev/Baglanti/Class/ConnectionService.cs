using KriptolojiOdev.Baglanti.Interface;
using KriptolojiOdev.Sifreleme.Class;
using KriptolojiOdev.Sifreleme.Interface;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KriptolojiOdev.Baglanti.Class
{
    public class ConnectionService : IConnectionService
    {
        private readonly IPAddress ip = IPAddress.Parse("127.0.0.1");
        private int port = 8080;
        private TcpListener listener;
        private Thread thread;
        private TcpClient client;

        private IEncryptorService encryptor = new EncryptorService();
        private IDecryptorService decryptor = new DecryptorService();
        private ITransportSecurityService transportService = new TransportSecurityService();

        public Action<string> OnMessage { get; set; }

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
                TcpClient client = listener.AcceptTcpClient();
                OnMessage?.Invoke("Yeni client bağlandı!");
                Thread clientThread = new Thread(() => HandleClient(client));
                clientThread.IsBackground = true;
                clientThread.Start();
            }
        }

        private void HandleClient(TcpClient client)
        {
            try
            {
                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[4096];
                int bytesRead;

                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    string[] parts = message.Split('|');

                    string chooseCrypt = parts[0];
                    string algorithm = parts[1].ToUpper();

                    string text = transportService.Decrypt(parts[2]);
                    string key = parts.Length > 3 ? transportService.Decrypt(parts[3]) : string.Empty;
                    string iv = parts.Length > 4 ? transportService.Decrypt(parts[4]) : string.Empty;

                    string responseText = chooseCrypt switch
                    {
                        "Encrypt" => EncryptorServiceCaller(algorithm, text, string.IsNullOrEmpty(key) ? null : key, string.IsNullOrEmpty(iv) ? null : iv),
                        "Decrypt" => DecryptorServiceCaller(algorithm, text, string.IsNullOrEmpty(key) ? null : key, string.IsNullOrEmpty(iv) ? null : iv),
                        _ => "Hata: Geçersiz işlem türü"
                    };

                    byte[] response = Encoding.UTF8.GetBytes(responseText);
                    stream.Write(response, 0, response.Length);
                }
            }
            catch
            {
            }
            finally
            {
                client.Close();
            }
        }

        private string EncryptorServiceCaller(string algorithm, string metin, string? key, string? iv)
        {
            try
            {
                return algorithm switch
                {
                    "SUBSTİTİUİON" => encryptor.SubstitutionEncrypt(metin, key),
                    "VİGENERE" => encryptor.VigenereEncrypt(metin, key),
                    "AFFİNE" => encryptor.AffineEncrypt(metin),
                    "CAESAR" => encryptor.CaesarEncrypt(metin),
                    "ROTA" => encryptor.RotaEncrypt(metin, key),
                    "COLUMNAR" => encryptor.ColumnarEncrypt(metin, key),
                    "POLYBİUS" => encryptor.PolybiusEncrypt(metin, key),
                    "PİGPEN" => encryptor.PigpenEncrypt(metin, key),
                    "HİLL" => encryptor.HillEncrypt(metin, key),
                    "TRENRAYI" => encryptor.TrenRayiEncrypt(metin, key),
                    "AES" => encryptor.AesEncrypt(metin, key, iv),
                    "DES" => encryptor.DesEncrypt(metin, key, iv),
                    "RSA" => encryptor.RsaEncrypt(metin, key),
                    _ => "İstenilen Algoritma Bulunamadı"
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
                    "SUBSTİTİUİON" => decryptor.DecryptorSubstitiuion(metin, key),
                    "VİGENERE" => decryptor.DecryptorVigenere(metin, key),
                    "AFFİNE" => decryptor.DecryptorAffine(metin),
                    "CAESAR" => decryptor.DecryptorCaesar(metin),
                    "ROTA" => decryptor.RotaDecrypt(metin, key),
                    "COLUMNAR" => decryptor.ColumnarDecrypt(metin, key),
                    "POLYBİUS" => decryptor.PolybiusDecrypt(metin, key),
                    "PİGPEN" => decryptor.PigpenDecrypt(metin, key),
                    "HİLL" => decryptor.HillDecrypt(metin, key),
                    "TRENRAYI" => decryptor.TrenRayiDecrypt(metin, key),
                    "AES" => decryptor.AesDecrypt(metin, key, iv),
                    "DES" => decryptor.DesDecrypt(metin, key, iv),
                    "RSA" => decryptor.RsaDecrypt(metin, key),
                    _ => "İstenilen Algoritma Bulunamadı"
                };
            }
            catch (Exception ex) { return "Hata: " + ex.Message; }
        }
    }
}