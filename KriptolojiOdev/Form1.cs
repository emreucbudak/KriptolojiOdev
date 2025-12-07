using KriptolojiOdev.Baglanti.Class;
using KriptolojiOdev.Baglanti.Interface;
using KriptolojiOdev.Sifreleme.Class;
using KriptolojiOdev.Sifreleme.Interface;
using System;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KriptolojiOdev
{
    public partial class Form1 : Form
    {
        private IConnectionService connectionService = new ConnectionService();
        private ITransportSecurityService transportService = new TransportSecurityService();
        private SunucuForm serverForm;

        public Form1(SunucuForm serverForm)
        {
            InitializeComponent();
            this.serverForm = serverForm;
        }

        private async Task SendMessageToServerAsync(string operation, string algorithm, string text, string key = "", string iv = "")
        {
            try
            {
                var (message, client) = await connectionService.ConnectToServer();
                clientLog.AppendText(message);

                if (client == null) return;

                NetworkStream stream = client.GetStream();

                string securedText = transportService.Encrypt(text);
                string securedKey = string.IsNullOrEmpty(key) ? "" : transportService.Encrypt(key);
                string securedIV = string.IsNullOrEmpty(iv) ? "" : transportService.Encrypt(iv);

                string msg = $"{operation}|{algorithm}|{securedText}|{securedKey}|{securedIV}";

                byte[] data = Encoding.UTF8.GetBytes(msg);
                await stream.WriteAsync(data, 0, data.Length);

                byte[] buffer = new byte[4096];
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                serverForm.MesajYaz(msg);

                string logPrefix = operation == "Encrypt" ? "Þifreleme Sonucu" : "Çözme Sonucu";
                clientLog.AppendText($"Gönderilen: {msg}{Environment.NewLine}{logPrefix} = {response}{Environment.NewLine}");

                if (operation == "Encrypt") textBox1.Text = response;
                else textBox6.Text = response;
            }
            catch (Exception ex)
            {
                clientLog.AppendText($"Hata: {ex.Message}{Environment.NewLine}");
                MessageBox.Show($"Bir hata oluþtu: {ex.Message}");
            }
        }

        private void RsaAnahtarUret(out string publicKey, out string privateKey)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048))
            {
                publicKey = rsa.ToXmlString(false);
                privateKey = rsa.ToXmlString(true);
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            await SendMessageToServerAsync("Encrypt", "CAESAR", textBox2.Text);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await SendMessageToServerAsync("Decrypt", "CAESAR", textBox4.Text);
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            string key = textBox3.Text.ToUpper();
            if (key.Length != 26 || key.Distinct().Count() != 26)
            {
                MessageBox.Show("Substitution için 26 benzersiz harf giriniz.");
                return;
            }
            await SendMessageToServerAsync("Encrypt", "SUBSTÝTÝUÝON", textBox2.Text, key);
        }

        private async void button7_Click(object sender, EventArgs e)
        {
            string key = textBox5.Text.ToUpper();
            if (key.Length != 26 || key.Distinct().Count() != 26)
            {
                MessageBox.Show("Substitution için 26 benzersiz harf giriniz.");
                return;
            }
            await SendMessageToServerAsync("Decrypt", "SUBSTÝTÝUÝON", textBox4.Text, key);
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            await SendMessageToServerAsync("Encrypt", "AFFÝNE", textBox2.Text);
        }

        private async void button8_Click(object sender, EventArgs e)
        {
            await SendMessageToServerAsync("Decrypt", "AFFÝNE", textBox4.Text);
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            string key = textBox3.Text.ToUpper();
            if (string.IsNullOrWhiteSpace(key) || !key.All(char.IsLetter))
            {
                MessageBox.Show("Vigenere için sadece harf giriniz.");
                return;
            }
            await SendMessageToServerAsync("Encrypt", "VÝGENERE", textBox2.Text, key);
        }

        private async void button6_Click(object sender, EventArgs e)
        {
            string key = textBox5.Text.ToUpper();
            if (string.IsNullOrWhiteSpace(key) || !key.All(char.IsLetter))
            {
                MessageBox.Show("Vigenere için sadece harf giriniz.");
                return;
            }
            await SendMessageToServerAsync("Decrypt", "VÝGENERE", textBox4.Text, key);
        }

        private async void button9_Click(object sender, EventArgs e)
        {
            await SendMessageToServerAsync("Encrypt", "ROTA", textBox2.Text, textBox3.Text);
        }

        private async void button14_Click(object sender, EventArgs e)
        {
            await SendMessageToServerAsync("Decrypt", "ROTA", textBox4.Text, textBox5.Text);
        }

        private async void button10_Click(object sender, EventArgs e)
        {
            await SendMessageToServerAsync("Encrypt", "COLUMNAR", textBox2.Text, textBox3.Text);
        }

        private async void button15_Click(object sender, EventArgs e)
        {
            await SendMessageToServerAsync("Decrypt", "COLUMNAR", textBox4.Text, textBox5.Text);
        }

        private async void button11_Click(object sender, EventArgs e)
        {
            await SendMessageToServerAsync("Encrypt", "POLYBÝUS", textBox2.Text, textBox3.Text);
        }

        private async void button16_Click(object sender, EventArgs e)
        {
            await SendMessageToServerAsync("Decrypt", "POLYBÝUS", textBox4.Text, textBox5.Text);
        }

        private async void button12_Click(object sender, EventArgs e)
        {
            await SendMessageToServerAsync("Encrypt", "PÝGPEN", textBox2.Text, textBox3.Text);
        }

        private async void button17_Click(object sender, EventArgs e)
        {
            await SendMessageToServerAsync("Decrypt", "PÝGPEN", textBox4.Text, textBox5.Text);
        }

        private async void button13_Click(object sender, EventArgs e)
        {
            await SendMessageToServerAsync("Encrypt", "HÝLL", textBox2.Text, textBox3.Text);
        }

        private async void button18_Click(object sender, EventArgs e)
        {
            await SendMessageToServerAsync("Decrypt", "HÝLL", textBox4.Text, textBox5.Text);
        }

        private async void button19_Click(object sender, EventArgs e)
        {
            await SendMessageToServerAsync("Encrypt", "TRENRAYI", textBox2.Text, textBox3.Text);
        }

        private async void button20_Click(object sender, EventArgs e)
        {
            await SendMessageToServerAsync("Decrypt", "TRENRAYI", textBox4.Text, textBox5.Text);
        }

        private async void button21_Click(object sender, EventArgs e)
        {
            await SendMessageToServerAsync("Encrypt", "AES", textBox2.Text, textBox3.Text, textBox7.Text);
        }

        private async void button23_Click(object sender, EventArgs e)
        {
            await SendMessageToServerAsync("Decrypt", "AES", textBox4.Text, textBox5.Text, textBox8.Text);
        }

        private async void button22_Click(object sender, EventArgs e)
        {
            await SendMessageToServerAsync("Encrypt", "DES", textBox2.Text, textBox3.Text, textBox7.Text);
        }

        private async void button24_Click(object sender, EventArgs e)
        {
            await SendMessageToServerAsync("Decrypt", "DES", textBox4.Text, textBox5.Text, textBox8.Text);
        }

        private async void button25_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox2.Text))
                {
                    MessageBox.Show("Lütfen þifrelenecek metni giriniz.");
                    return;
                }

                if (string.IsNullOrEmpty(textBox3.Text))
                {
                    string yeniPublicKey, yeniPrivateKey;

                    RsaAnahtarUret(out yeniPublicKey, out yeniPrivateKey);

                    textBox3.Text = yeniPublicKey;
                    textBox5.Text = yeniPrivateKey;

                    MessageBox.Show("RSA Anahtarlarý Oluþturuldu!");
                }

                await SendMessageToServerAsync("Encrypt", "RSA", textBox2.Text, textBox3.Text, "");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }
        private void groupBox1_Enter(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }

        private async void button26_Click_1(object sender, EventArgs e)
        {
 
            try
            {
                await SendMessageToServerAsync("Decrypt", "RSA", textBox4.Text, textBox5.Text, "");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }
    }
}