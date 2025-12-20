using KriptolojiOdev.Baglanti.Class;
using KriptolojiOdev.Baglanti.Interface;
using KriptolojiOdev.Sifreleme.Class;
using KriptolojiOdev.Sifreleme.Interface;
using System.Security.Cryptography;

namespace KriptolojiOdev
{
    public partial class SunucuForm : Form
    {
        private IConnectionService connectionService = new ConnectionService();
        private IEncryptorService encryptorService = new EncryptorService();
        private IDecryptorService decryptorService = new DecryptorService();
        private ITransportSecurityService transportService = new TransportSecurityService();
        private string serverPrivateKey;
        private string serverPublicKey;

        public SunucuForm()
        {
            InitializeComponent();
            RsaAnahtarUret();

            if (connectionService is ConnectionService cs)
                cs.ServerPrivateKey = serverPrivateKey;

            connectionService.OnMessage = (msg) =>
            {
                Invoke((MethodInvoker)(() =>
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
                var p = paket.Split('|');
                if (p.Length < 4) return; // SUNUCU|MESSAGE|ALGO|TEXT -> En az 4 parça
                if (p[0] != "SUNUCU") return;

                string algoritma = p[2]; // ALGO kısmını al
                string cozulmusMetin = p[3]; // TEXT kısmını al (Service zaten çözdü)

                serverLog.SelectionColor = Color.Blue;
                serverLog.AppendText("[CLIENT'TAN YENİ MESAJ]\n");
                serverLog.SelectionColor = Color.Black;
                serverLog.AppendText($"Mesaj: {cozulmusMetin} ({algoritma})\n"); // Artık düzgün metin basar
                serverLog.AppendText("------------------\n");
                serverLog.ScrollToCaret();
            }
            catch (Exception ex) { MessageBox.Show("Hata: " + ex.Message); }
        }
        private async Task SendToClientAsync(string algorithm, string text, string key = "", string iv = "")
        {
            try
            {
                // 1. Önce algoritma ile şifrele (Caesar, AES vs.)
                string encryptedText = algorithm switch
                {
                    "CAESAR" => encryptorService.CaesarEncrypt(text),
                    "VIGENERE" => encryptorService.VigenereEncrypt(text, key),
                    "SUBSTITUTION" => encryptorService.SubstitutionEncrypt(text, key),
                    "AFFINE" => encryptorService.AffineEncrypt(text),
                    "ROTA" => encryptorService.RotaEncrypt(text, key),
                    "COLUMNAR" => encryptorService.ColumnarEncrypt(text, key),
                    "POLYBIUS" => encryptorService.PolybiusEncrypt(text, key),
                    "PIGPEN" => encryptorService.PigpenEncrypt(text, key),
                    "HILL" => encryptorService.HillEncrypt(text, key),
                    "TRENRAYI" => encryptorService.TrenRayiEncrypt(text, key),
                    "AES" => encryptorService.AesEncrypt(text, key, iv),
                    "DES" => encryptorService.DesEncrypt(text, key, iv),
                    "MANUEL_DES" => encryptorService.ManuelDesEncrypt(text, key, iv),
                    _ => text
                };

                string clientPublicKey = textBox4.Text;
                string finalKey = key;

                // RSA ile Key şifreleme (Bu kalmalı, çünkü bu algoritma güvenliği)
                if (!string.IsNullOrEmpty(clientPublicKey) && !string.IsNullOrEmpty(key) &&
                    (algorithm == "AES" || algorithm == "DES" || algorithm == "MANUEL_DES"))
                {
                    finalKey = encryptorService.RsaEncrypt(key, clientPublicKey);
                }

                // ✅ DÜZELTME: securedText/Key/Iv değişkenlerini burada oluşturma!
                // Doğrudan ham (algoritma ile şifrelenmiş ama transport katmanına girmemiş) hallerini gönder.
                connectionService.Broadcast("CLIENT", "MESSAGE", algorithm, encryptedText, finalKey, iv);

                serverLog.SelectionColor = Color.Green;
                serverLog.AppendText($"[CLIENT'A GÖNDERİLDİ]: {algorithm}\n");
                serverLog.ScrollToCaret();
            }
            catch (Exception ex) { MessageBox.Show("Gönderim Hatası: " + ex.Message); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            serverLog.AppendText(connectionService.StartServer());
            textBox5.Text = serverPublicKey;
        }

        private async void button2_Click(object sender, EventArgs e) => await SendToClientAsync("CAESAR", textBox1.Text);
        private async void button3_Click(object sender, EventArgs e) => await SendToClientAsync("VIGENERE", textBox1.Text, textBox2.Text);
        private async void button4_Click(object sender, EventArgs e) => await SendToClientAsync("SUBSTITUTION", textBox1.Text, textBox2.Text);
        private async void button5_Click(object sender, EventArgs e) => await SendToClientAsync("AFFINE", textBox1.Text);
        private async void button6_Click(object sender, EventArgs e) => await SendToClientAsync("ROTA", textBox1.Text, textBox2.Text);
        private async void button7_Click(object sender, EventArgs e) => await SendToClientAsync("COLUMNAR", textBox1.Text, textBox2.Text);
        private async void button8_Click(object sender, EventArgs e) => await SendToClientAsync("POLYBIUS", textBox1.Text, textBox2.Text);
        private async void button9_Click(object sender, EventArgs e) => await SendToClientAsync("PIGPEN", textBox1.Text, textBox2.Text);
        private async void button10_Click(object sender, EventArgs e) => await SendToClientAsync("HILL", textBox1.Text, textBox2.Text);
        private async void button11_Click(object sender, EventArgs e) => await SendToClientAsync("TRENRAYI", textBox1.Text, textBox2.Text);
        private async void button12_Click(object sender, EventArgs e) => await SendToClientAsync("AES", textBox1.Text, textBox2.Text, textBox3.Text);
        private async void button13_Click(object sender, EventArgs e) => await SendToClientAsync("DES", textBox1.Text, textBox2.Text, textBox3.Text);
        private async void button14_Click(object sender, EventArgs e) => await SendToClientAsync("MANUEL_DES", textBox1.Text, textBox2.Text, textBox3.Text);

        private void textBox5_TextChanged(object sender, EventArgs e) { }
        private void SunucuForm_Load(object sender, EventArgs e) { }
    }
}