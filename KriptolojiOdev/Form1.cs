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
        private TcpClient client = new TcpClient();
       
        private IConnectionService connectionService = new ConnectionService();
        private bool isConnected = false;
        private SunucuForm serverForm;



        public Form1(SunucuForm serverForm)
        {
            InitializeComponent();
            this.serverForm = serverForm;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (isConnected)
                {
                    throw new Exception("Zaten baðlý durumdasýnýz!");

                }
                var connect = await connectionService.ConnectToServer();
                clientLog.AppendText(connect.message);
                isConnected = true;

            }
            catch (Exception ex)
            {
                clientLog.AppendText("Hata: " + ex.Message + Environment.NewLine);
            }
        }





        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {


                var (message,client) = await connectionService.ConnectToServer();
                clientLog.AppendText(message);
       
                if (client == null) return; 

                NetworkStream stream = client.GetStream();
                string msg = "CAESAR|" + textBox2.Text;


                byte[] data = Encoding.UTF8.GetBytes(msg);

                await stream.WriteAsync(data, 0, data.Length);
                await stream.FlushAsync();
                byte[] buffer = new byte[1024];
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                serverForm.MesajYaz(msg);

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


                if (client == null) return; 

                NetworkStream stream = client.GetStream();
                string msg = "SUBSTÝTÝUÝON|" + textBox2.Text;

                byte[] data = Encoding.UTF8.GetBytes(msg);

                await stream.WriteAsync(data, 0, data.Length);

                byte[] buffer = new byte[1024];
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                serverForm.MesajYaz(msg);
                textBox1.Text = response;
            }
            catch (Exception ex)
            {
                clientLog.AppendText("Hata: " + ex.Message + Environment.NewLine);

            }
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            try
            {
                var (message, client) = await connectionService.ConnectToServer();


                if (client == null) return; 

                NetworkStream stream = client.GetStream();
                string msg = "AFFÝNE|" + textBox2.Text;

                byte[] data = Encoding.UTF8.GetBytes(msg);

                await stream.WriteAsync(data, 0, data.Length);

                byte[] buffer = new byte[1024];
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                serverForm.MesajYaz(msg);
                textBox1.Text = response;
            }
            catch (Exception ex)
            {
                clientLog.AppendText("Hata: " + ex.Message + Environment.NewLine);

            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            try
            {
                var (message, client) = await connectionService.ConnectToServer();


                if (client == null) return; 

                NetworkStream stream = client.GetStream();
                string msg = "VÝGENERE|" + textBox2.Text;
  
                byte[] data = Encoding.UTF8.GetBytes(msg);

                await stream.WriteAsync(data, 0, data.Length);

                byte[] buffer = new byte[1024];
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                serverForm.MesajYaz(msg);
                textBox1.Text = response;
            }
            catch (Exception ex)
            {
                clientLog.AppendText("Hata: " + ex.Message + Environment.NewLine);

            }
        }
    }
}
