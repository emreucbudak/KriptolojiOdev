using System.Net;
using System.Net.Sockets;
using System.Text;

namespace KriptolojiOdev
{
    public partial class Form1 : Form
    {
        private TcpListener tcpListener;
        private Thread thread;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void startButton_Click(object sender, EventArgs e)
        {
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            int port = 8080;
            tcpListener = new TcpListener(ip, port);
            tcpListener.Start();
            serverLog.AppendText("Server " + ip + ":" + port + " Baþlatýldý!\n");
            openServer.Text = "Açýk";


            thread = new Thread(ListenForClients);
            thread.IsBackground = true;
            thread.Start();
        }

        private void ListenForClients()
        {
            while (true)
            {
                TcpClient client = tcpListener.AcceptTcpClient(); 
                this.Invoke((MethodInvoker)(() =>
                {
                    serverLog.AppendText("Yeni client baðlandý!\n");
                }));

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

                this.Invoke((MethodInvoker)(() =>
                {
                    serverLog.AppendText("Client mesajý: " + message + Environment.NewLine);
                }));

                byte[] response = Encoding.UTF8.GetBytes("Server aldý: " + message);
                stream.Write(response, 0, response.Length);
            }

            client.Close();
        }

    }
}
