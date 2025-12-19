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
                if (p.Length < 4) return;
                if (p[0] != "SUNUCU") return;

                string islem = p[1];
                string algoritma = p[2];
                string sifreliMetin = p[3];
                string anahtar = p.Length > 4 ? p[4] : "";
                string iv = p.Length > 5 ? p[5] : "";

                string cozulmus = decryptorService.DecryptByAlgorithm(
                    algoritma,
                    sifreliMetin,
                    anahtar,
                    iv
                );

                serverLog.SelectionColor = Color.Blue;
                serverLog.AppendText("[CLIENT'TAN MESAJ]\n");
                serverLog.SelectionColor = Color.Black;
                serverLog.AppendText($"Mesaj: {algoritma}\n");

                serverLog.AppendText("------------------\n");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task SendToClientAsync(string algorithm, string text, string key = "", string iv = "")
        {
            try
            {
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

                // Simetrik algoritmalarda key RSA ile şifrelenir
                if (!string.IsNullOrEmpty(clientPublicKey) &&
                    !string.IsNullOrEmpty(key) &&
                    (algorithm == "AES" || algorithm == "DES" || algorithm == "MANUEL_DES"))
                {
                    finalKey = encryptorService.RsaEncrypt(key, clientPublicKey);
                }

                connectionService.Broadcast(
                    "CLIENT",
                    "Encrypt",
                    algorithm,
                    encryptedText,
                    finalKey,
                    iv
                );

                serverLog.SelectionColor = Color.Green;
                serverLog.AppendText($"[CLIENT'A GÖNDERİLDİ]: {algorithm}\n");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gönderim Hatası: " + ex.Message);
            }
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

        private void SunucuForm_Load(object sender, EventArgs e) { }
    }
}
