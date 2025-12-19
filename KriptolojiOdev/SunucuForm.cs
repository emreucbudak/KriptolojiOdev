using KriptolojiOdev.Baglanti.Class;
using KriptolojiOdev.Baglanti.Interface;
using KriptolojiOdev.Sifreleme.Class;
using KriptolojiOdev.Sifreleme.Interface;
using System.Security.Cryptography;
using System.Text;

namespace KriptolojiOdev
{
    public partial class SunucuForm : Form
    {
        private IConnectionService connectionService = new ConnectionService();
        private IEncryptorService encryptorService = new EncryptorService();
        private string serverPrivateKey;
        private string serverPublicKey;

        public SunucuForm()
        {
            InitializeComponent();
            RsaAnahtarUret();

            if (connectionService is ConnectionService cs)
            {
                cs.ServerPrivateKey = serverPrivateKey;
            }

            connectionService.OnMessage = (msg) =>
            {
                this.Invoke((MethodInvoker)(() =>
                {
                    MesajYaz(msg);
                }));
            };
        }

        private void RsaAnahtarUret()
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048))
            {
                serverPublicKey = rsa.ToXmlString(false);
                serverPrivateKey = rsa.ToXmlString(true);
            }
        }

        public void MesajYaz(string paket)
        {
            try
            {
                var parcalar = paket.Split('|');
                if (parcalar.Length < 5)
                {
                    serverLog.AppendText($"[SİSTEM]: {paket}{Environment.NewLine}");
                    return;
                }

                string hedef = parcalar[0];
                string algoritma = parcalar[1];
                string sifreliHali = parcalar[2];
                string cozulunKey = parcalar[3];
                string sonuc = parcalar[4];

                serverLog.SelectionColor = Color.Blue;
                serverLog.AppendText($"[{hedef}] {algoritma} İşlemi Alındı.{Environment.NewLine}");
                serverLog.SelectionColor = Color.Red;
                serverLog.AppendText($"Gelen Şifre: {sifreliHali}{Environment.NewLine}");
                serverLog.SelectionColor = Color.Green;
                serverLog.AppendText($"Çözülen Mesaj: {sonuc}{Environment.NewLine}---{Environment.NewLine}");
            }
            catch (Exception ex)
            {
                serverLog.AppendText("Hata: " + ex.Message + Environment.NewLine);
            }
        }

        private async Task SendToClientAsync(string algorithm, string text, string key = "", string iv = "")
        {
            try
            {
                string clientPubKey = textBox4.Text;
                string encryptedKey = key;

                if (!string.IsNullOrEmpty(clientPubKey) && !string.IsNullOrEmpty(key))
                {
                    encryptedKey = encryptorService.RsaEncrypt(key, clientPubKey);
                }

                connectionService.Broadcast("CLIENT", "Encrypt", algorithm, text, encryptedKey, iv);
                serverLog.AppendText($"[SUNUCU -> CLIENT]: {algorithm} paketi gönderildi." + Environment.NewLine);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gönderim Hatası: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            serverLog.AppendText(connectionService.StartServer());
            textBox4.Text = serverPublicKey;
        }

        private async void button2_Click(object sender, EventArgs e) => await SendToClientAsync("CAESAR", textBox1.Text, textBox2.Text);
        private async void button3_Click(object sender, EventArgs e) => await SendToClientAsync("VIGENERE", textBox1.Text, textBox2.Text);
        private async void button4_Click(object sender, EventArgs e) => await SendToClientAsync("SUBSTITUTION", textBox1.Text, textBox2.Text);
        private async void button5_Click(object sender, EventArgs e) => await SendToClientAsync("AFFINE", textBox1.Text, textBox2.Text);
        private async void button6_Click(object sender, EventArgs e) => await SendToClientAsync("ROTA", textBox1.Text, textBox2.Text);
        private async void button7_Click(object sender, EventArgs e) => await SendToClientAsync("COLUMNAR", textBox1.Text, textBox2.Text);
        private async void button8_Click(object sender, EventArgs e) => await SendToClientAsync("POLYBIUS", textBox1.Text, textBox2.Text);
        private async void button9_Click(object sender, EventArgs e) => await SendToClientAsync("PIGPEN", textBox1.Text, textBox2.Text);
        private async void button10_Click(object sender, EventArgs e) => await SendToClientAsync("HILL", textBox1.Text, textBox2.Text);
        private async void button11_Click(object sender, EventArgs e) => await SendToClientAsync("TRENRAYI", textBox1.Text, textBox2.Text);
        private async void button12_Click(object sender, EventArgs e) => await SendToClientAsync("AES", textBox1.Text, textBox2.Text, textBox3.Text);
        private async void button13_Click(object sender, EventArgs e) => await SendToClientAsync("DES", textBox1.Text, textBox2.Text, textBox3.Text);
        private async void button14_Click(object sender, EventArgs e) => await SendToClientAsync("MANUEL_DES", textBox1.Text, textBox2.Text, textBox3.Text);

        private void SunucuForm_Load(object sender, EventArgs e) { }
    }
}