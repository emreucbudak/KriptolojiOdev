using KriptolojiOdev.Baglanti.Interface;
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


        public void ConnectToServer()
        {
            throw new NotImplementedException();
        }

        public string StartServer()
        {
            try
            {
                listener = new TcpListener(ip, port);
                listener.Start();

                return "Server " + ip + ":" + port + " Başlatıldı!" + Environment.NewLine;

            }
            catch (Exception ex)
            {
                return "Hata: " + ex.Message + Environment.NewLine;
            }
        }


        public void StopServer()
        {
            throw new NotImplementedException();
        }
    }
}
