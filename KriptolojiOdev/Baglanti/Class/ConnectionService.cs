using KriptolojiOdev.Baglanti.Interface;
using KriptolojiOdev.Sifreleme.Class;
using KriptolojiOdev.Sifreleme.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace KriptolojiOdev.Baglanti.Class
{
    public class ConnectionService : IConnectionService
    {
        private readonly IPAddress ip = IPAddress.Parse("127.0.0.1");
        int port = 8080;
        private TcpListener listener;
        private Thread thread;
        private TcpClient client;
        private IEncryptorService encryptor = new EncryptorService();
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
                return ("Hata: " + ex.Message + Environment.NewLine,client); 
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

                return "Server " + ip + ":" + port + " Başlatıldı!" + Environment.NewLine;

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
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead;

            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
            {
                string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);


                string[] parts = message.Split('|');
                if (parts.Length != 2) continue;

                string algorithm = parts[0].ToUpper();
                string text = parts[1];
                string responseText;

                if (algorithm == "CAESAR")
                {
                    responseText = encryptor.CaesarEncrypt(text);
                }
                if (algorithm == "SUBSTİTİUİON")
                {
                    responseText = encryptor.SubstitutionEncrypt(text);
                }
                else
                {
                    responseText = text;
                }

                byte[] response = Encoding.UTF8.GetBytes(responseText);
                stream.Write(response, 0, response.Length);
            }

            client.Close();
        }



    }
}
