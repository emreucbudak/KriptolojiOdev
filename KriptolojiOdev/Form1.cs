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






        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {


                var (message, client) = await connectionService.ConnectToServer();
                clientLog.AppendText(message);

                if (client == null) return;

                NetworkStream stream = client.GetStream();
                string msg = "Encrypt" + "CAESAR|" + textBox2.Text;


                byte[] data = Encoding.UTF8.GetBytes(msg);

                await stream.WriteAsync(data, 0, data.Length);

                byte[] buffer = new byte[1024];
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                serverForm.MesajYaz(msg);
                clientLog.AppendText("Gönderilen Mesaj: " + msg + "Þifreleme Sonucu = " + response + Environment.NewLine);
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
                clientLog.AppendText(message);

                if (client == null) return;

                NetworkStream stream = client.GetStream();
                string key = textBox3.Text.ToUpper();
                if (key.Length != 26 || key.Distinct().Count() != 26)
                {
                    throw new Exception("Girdiðiniz key geçersiz substitiuion þifrelemesi için 26 harflik ve her harf benzersiz olacak þekilde bir key girmelisiniz.");
                }
                string msg = "Encrypt" + "SUBSTÝTÝUÝON|" + textBox2.Text + "|" + key;
                //Örnek substitiuion key isterseniz QWERTYUIOPASDFGHJKLZXCVBNM

                byte[] data = Encoding.UTF8.GetBytes(msg);

                await stream.WriteAsync(data, 0, data.Length);

                byte[] buffer = new byte[1024];
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                serverForm.MesajYaz(msg);
                clientLog.AppendText("Gönderilen Mesaj: " + msg + "Þifreleme Sonucu = " + response + Environment.NewLine);
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

                clientLog.AppendText(message);
                if (client == null) return;

                NetworkStream stream = client.GetStream();
                string msg = "Encrypt" + "AFFÝNE|" + textBox2.Text;

                byte[] data = Encoding.UTF8.GetBytes(msg);

                await stream.WriteAsync(data, 0, data.Length);

                byte[] buffer = new byte[1024];
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                serverForm.MesajYaz(msg);
                clientLog.AppendText("Gönderilen Mesaj: " + msg + "Þifreleme Sonucu = " + response + Environment.NewLine);
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
                clientLog.AppendText(message);

                if (client == null) return;

                NetworkStream stream = client.GetStream();
                string key = textBox3.Text.ToUpper();

                if (string.IsNullOrWhiteSpace(key) || !key.All(char.IsLetter))
                {
                    throw new Exception("Hata! Vigenere þifrelemesi yalnýzca harflerden oluþmalýdýr.");
                }
                string msg = "Encrypt" + "VÝGENERE|" + textBox2.Text + "|" + textBox3.Text;
                // Örnek Vigenere key isterseniz ANAHTAR

                byte[] data = Encoding.UTF8.GetBytes(msg);

                await stream.WriteAsync(data, 0, data.Length);

                byte[] buffer = new byte[1024];
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                serverForm.MesajYaz(msg);
                clientLog.AppendText("Gönderilen Mesaj: " + msg + "Þifreleme Sonucu = " + response + Environment.NewLine);
                textBox1.Text = response;
            }
            catch (Exception ex)
            {
                clientLog.AppendText("Hata: " + ex.Message + Environment.NewLine);

            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {


                var (message, client) = await connectionService.ConnectToServer();
                clientLog.AppendText(message);

                if (client == null) return;

                NetworkStream stream = client.GetStream();
                string msg = "Decrypt" + "CAESAR|" + textBox2.Text;


                byte[] data = Encoding.UTF8.GetBytes(msg);

                await stream.WriteAsync(data, 0, data.Length);

                byte[] buffer = new byte[1024];
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                serverForm.MesajYaz(msg);
                clientLog.AppendText("Gönderilen Mesaj: " + msg + "Çözme Sonucu  = " + response + Environment.NewLine);
                textBox1.Text = response;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }

        }

        private async void button6_Click(object sender, EventArgs e)
        {
            try
            {
                var (message, client) = await connectionService.ConnectToServer();
                clientLog.AppendText(message);

                if (client == null) return;

                NetworkStream stream = client.GetStream();
                string key = textBox3.Text.ToUpper();

                if (string.IsNullOrWhiteSpace(key) || !key.All(char.IsLetter))
                {
                    throw new Exception("Hata! Vigenere þifrelemesi yalnýzca harflerden oluþmalýdýr.");
                }
                string msg = "Decrypt" + "VÝGENERE|" + textBox2.Text + "|" + textBox3.Text;
                //Kullandýgýnýz keyi kullanmalýsýnýz

                byte[] data = Encoding.UTF8.GetBytes(msg);

                await stream.WriteAsync(data, 0, data.Length);

                byte[] buffer = new byte[1024];
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                serverForm.MesajYaz(msg);
                clientLog.AppendText("Gönderilen Mesaj: " + msg + "Çözme Sonucu = " + response + Environment.NewLine);
                textBox1.Text = response;
            }
            catch (Exception ex)
            {
                clientLog.AppendText("Hata: " + ex.Message + Environment.NewLine);

            }

        }

        private async void button7_Click(object sender, EventArgs e)
        {
            try
            {
                var (message, client) = await connectionService.ConnectToServer();
                clientLog.AppendText(message);

                if (client == null) return;

                NetworkStream stream = client.GetStream();
                string key = textBox3.Text.ToUpper();
                if (key.Length != 26 || key.Distinct().Count() != 26)
                {
                    throw new Exception("Girdiðiniz key geçersiz substitiuion þifrelemesi için 26 harflik ve her harf benzersiz olacak þekilde bir key girmelisiniz.");
                }
                string msg = "Decrypt" + "SUBSTÝTÝUÝON|" + textBox2.Text + "|" + key;
                //Kullandýgýnýz keyi kullanmalýsýnýz

                byte[] data = Encoding.UTF8.GetBytes(msg);

                await stream.WriteAsync(data, 0, data.Length);

                byte[] buffer = new byte[1024];
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                serverForm.MesajYaz(msg);
                clientLog.AppendText("Gönderilen Mesaj: " + msg + "Çözme Sonucu = " + response + Environment.NewLine);
                textBox1.Text = response;
            }
            catch (Exception ex)
            {
                clientLog.AppendText("Hata: " + ex.Message + Environment.NewLine);

            }
        }

        private async void button8_Click(object sender, EventArgs e)
        {
            try
            {
                var (message, client) = await connectionService.ConnectToServer();

                clientLog.AppendText(message);
                if (client == null) return;

                NetworkStream stream = client.GetStream();
                string msg = "Decrypt" + "AFFÝNE|" + textBox2.Text;

                byte[] data = Encoding.UTF8.GetBytes(msg);

                await stream.WriteAsync(data, 0, data.Length);

                byte[] buffer = new byte[1024];
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                serverForm.MesajYaz(msg);
                clientLog.AppendText("Gönderilen Mesaj: " + msg + "Çözme Sonucu = " + response + Environment.NewLine);
                textBox1.Text = response;
            }
            catch (Exception ex)
            {
                clientLog.AppendText("Hata: " + ex.Message + Environment.NewLine);

            }
        }
    }
}
