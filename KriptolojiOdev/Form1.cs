using KriptolojiOdev.Baglanti.Class;
using KriptolojiOdev.Baglanti.Interface;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace KriptolojiOdev
{
    public partial class Form1 : Form
    {
        private TcpListener tcpListener;
        private Thread thread;
        private TcpClient client;
        private IConnectionService connectionService = new ConnectionService();



        public Form1()
        {
            InitializeComponent();
            connectionService.OnMessage = (msg) =>
            {
                this.Invoke((MethodInvoker)(() =>
                {
                    serverLog.AppendText(msg + Environment.NewLine);
                }));
            };

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var connect = await connectionService.ConnectToServer();
                clientLog.AppendText(connect.message);

            }
            catch (Exception ex)
            {
                clientLog.AppendText("Hata: " + ex.Message + Environment.NewLine);
            }
        }


        private void startButton_Click(object sender, EventArgs e)
        {
            try
            {
                serverLog.AppendText(connectionService.StartServer());
            }
            catch (Exception ex)
            {
                serverLog.AppendText("Hata: " + ex.Message + Environment.NewLine);
                return;
            }
        }


        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var (message, client) = await connectionService.ConnectToServer();
                serverLog.AppendText(message); // string mesajý göster

                if (client == null) return; // baðlantý baþarýsýzsa iþlemi durdur

                NetworkStream stream = client.GetStream();
                string msg = "CAESAR|" + textBox2.Text;
                byte[] data = Encoding.UTF8.GetBytes(msg);

                await stream.WriteAsync(data, 0, data.Length);

                byte[] buffer = new byte[1024];
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                textBox1.Text = response;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            try
            {
                var (message, client) = await connectionService.ConnectToServer();
                serverLog.AppendText(message); // string mesajý göster

                if (client == null) return; // baðlantý baþarýsýzsa iþlemi durdur

                NetworkStream stream = client.GetStream();
                string msg = "SUBSTÝTÝUÝON|" + textBox2.Text;
                byte[] data = Encoding.UTF8.GetBytes(msg);

                await stream.WriteAsync(data, 0, data.Length);

                byte[] buffer = new byte[1024];
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                textBox1.Text = response;
            }
            catch (Exception ex)
            {
                clientLog.AppendText("Hata: " + ex.Message + Environment.NewLine);
                serverLog.AppendText("Hata: " + ex.Message + Environment.NewLine);
            }
        }
    }
}
