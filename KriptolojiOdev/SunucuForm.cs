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
                if (parcalar.Length < 4)
                {
                    serverLog.AppendText(paket + Environment.NewLine);
                    return;
                }

                string hedef = parcalar[0];
                string islem = parcalar[1];
                string algoritma = parcalar[2];
                string sifreliMetin = parcalar[3];

                serverLog.SelectionColor = Color.Blue;
                serverLog.AppendText($"[{hedef}] {algoritma} işlemi yapıldı." + Environment.NewLine);
                serverLog.SelectionColor = Color.Black;
                serverLog.AppendText($"Ham Paket: {paket}" + Environment.NewLine + "---" + Environment.NewLine);
            }
            catch (Exception ex)
            {
                serverLog.AppendText("Görüntüleme Hatası: " + ex.Message + Environment.NewLine);
            }
        }

        private async Task SendToClientAsync(string algorithm, string text, string key = "", string iv = "")
        {
            try
            {
                string clientPubKey = txtClientPubKey.Text;
                string encryptedKey = key;

                if (!string.IsNullOrEmpty(clientPubKey) && !string.IsNullOrEmpty(key))
                {
                    byte[] rsaBytes = encryptorService.RSAEncrypt(Encoding.UTF8.GetBytes(key), clientPubKey);
                    encryptedKey = Convert.ToBase64String(rsaBytes);
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
            txtServerPublicKey.Text = serverPublicKey;
        }

        private async void button2_Click(object sender, EventArgs e) => await SendToClientAsync("CAESAR", txtInput.Text, txtKey.Text);
        private async void button3_Click(object sender, EventArgs e) => await SendToClientAsync("VİGENERE", txtInput.Text, txtKey.Text);
        private async void button4_Click(object sender, EventArgs e) => await SendToClientAsync("SUBSTİTİUİON", txtInput.Text, txtKey.Text);
        private async void button5_Click(object sender, EventArgs e) => await SendToClientAsync("AFFİNE", txtInput.Text, txtKey.Text);
        private async void button6_Click(object sender, EventArgs e) => await SendToClientAsync("ROTA", txtInput.Text, txtKey.Text);
        private async void button7_Click(object sender, EventArgs e) => await SendToClientAsync("COLUMNAR", txtInput.Text, txtKey.Text);
        private async void button8_Click(object sender, EventArgs e) => await SendToClientAsync("POLYBİUS", txtInput.Text, txtKey.Text);
        private async void button9_Click(object sender, EventArgs e) => await SendToClientAsync("PİGPEN", txtInput.Text, txtKey.Text);
        private async void button10_Click(object sender, EventArgs e) => await SendToClientAsync("HİLL", txtInput.Text, txtKey.Text);
        private async void button11_Click(object sender, EventArgs e) => await SendToClientAsync("TRENRAYI", txtInput.Text, txtKey.Text);
        private async void button12_Click(object sender, EventArgs e) => await SendToClientAsync("AES", txtInput.Text, txtKey.Text, txtIv.Text);
        private async void button13_Click(object sender, EventArgs e) => await SendToClientAsync("DES", txtInput.Text, txtKey.Text, txtIv.Text);
        private async void button14_Click(object sender, EventArgs e) => await SendToClientAsync("MANUEL_DES", txtInput.Text, txtKey.Text, txtIv.Text);

        private void SunucuForm_Load(object sender, EventArgs e) { }
    }
}