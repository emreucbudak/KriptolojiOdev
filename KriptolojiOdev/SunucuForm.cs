using KriptolojiOdev.Baglanti.Class;
using KriptolojiOdev.Baglanti.Interface;
using KriptolojiOdev.Sifreleme.Class;
using KriptolojiOdev.Sifreleme.Interface;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

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
        private string serverEccPublicKey;
        private string serverEccPrivateKey;

        public SunucuForm()
        {
            InitializeComponent();
            RsaAnahtarUret();
            EccAnahtarUret();

            if (connectionService is ConnectionService cs)
            {
                cs.ServerPrivateKey = serverPrivateKey;
                cs.ServerEccPrivateKey = serverEccPrivateKey;
            }

            connectionService.OnMessage = (msg) =>
            {
                Invoke((MethodInvoker)(() => { MesajYaz(msg); }));
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

        private void EccAnahtarUret()
        {
            encryptorService.EccKeyGenerate(out string pub, out string priv);
            serverEccPublicKey = pub;
            serverEccPrivateKey = priv;
        }

        public void MesajYaz(string paket)
        {
            try
            {
                var p = paket.Split('|');

                if (p.Length >= 4 && p[0] == "CLIENT" && p[1] == "KEY_EXCHANGE")
                {
                    Invoke((MethodInvoker)(() =>
                    {
                        textBox7.Text = p[2];
                        textBox6.Text = p[3];
                        serverLog.AppendText("[SİSTEM]: İstemci bağlandı, anahtarlar alındı.\n");
                        string response = $"SUNUCU|KEY_EXCHANGE|{serverPublicKey}|{serverEccPublicKey}";
                        connectionService.Broadcast("CLIENT", "INFO", "HANDSHAKE", response, "", "", "NONE");
                    }));
                    return;
                }
                if (p.Length < 4 || p[0] != "SUNUCU") return;

                string algoritma = p[2];
                string plainText = p[3];
                string durationMs = p.Length > 4 ? p[4] : "0";
                serverLog.SelectionColor = Color.Blue;
                serverLog.AppendText("[CLIENT'TAN MESAJ]\n");
                serverLog.SelectionColor = Color.Black;
                serverLog.AppendText($"Mesaj: {plainText} (Algoritma: {algoritma})\n");
                serverLog.SelectionColor = Color.DarkGray;
                serverLog.AppendText($"Sunucu Çözme Süresi: {durationMs} ms\n"); // Ekrana yazdır
                serverLog.SelectionColor = Color.Black;
                serverLog.AppendText("------------------\n");
                serverLog.ScrollToCaret();
            }
            catch (Exception ex) { serverLog.AppendText("[HATA]: " + ex.Message + "\n"); }
        }

        private async Task SendToClientAsync(string algorithm, string text, string key = "", string iv = "")
        {
            try
            {
                if (string.IsNullOrEmpty(textBox7.Text))
                {
                    MessageBox.Show("İstemci henüz bağlanmadı!");
                    return;
                }
                Stopwatch sw = Stopwatch.StartNew();
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
                sw.Stop();
                double encDuration = sw.Elapsed.TotalMilliseconds;
                string finalKeyToSend = key;
                string secType = "KLASİK";

                if (!string.IsNullOrEmpty(key) && (algorithm == "AES" || algorithm == "DES" || algorithm == "MANUEL_DES"))
                {
                    if (radioButton2.Checked)
                    {
                        finalKeyToSend = encryptorService.EccEncrypt(key, textBox6.Text);
                        secType = "ECC";
                    }
                    else
                    {
                        finalKeyToSend = encryptorService.RsaEncrypt(key, textBox7.Text);
                        secType = "RSA";
                    }
                }

                connectionService.Broadcast("CLIENT", "MESSAGE", algorithm, encryptedText, finalKeyToSend, iv, secType);
                serverLog.SelectionColor = Color.Green;
                serverLog.AppendText($"[GÖNDERİLDİ]: {algorithm} ({secType})\n");
                serverLog.SelectionColor = Color.DarkGray;
                serverLog.AppendText($"Şifreleme Süresi: {encDuration} ms\n");
            }
            catch (Exception ex) { MessageBox.Show("Hata: " + ex.Message); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            serverLog.AppendText(connectionService.StartServer());
            textBox5.Text = serverPublicKey;
            textBox8.Text = serverEccPublicKey;
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
        private async void textBox5_TextChanged(object sender, EventArgs e) { }
        private async void SunucuForm_Load(object sender, EventArgs e) { }
    }
}