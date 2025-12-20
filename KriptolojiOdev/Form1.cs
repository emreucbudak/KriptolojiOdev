using KriptolojiOdev.Baglanti.Class;
using KriptolojiOdev.Baglanti.Interface;
using KriptolojiOdev.Sifreleme.Class;
using KriptolojiOdev.Sifreleme.Interface;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace KriptolojiOdev
{
    public partial class Form1 : Form
    {
        private IConnectionService connectionService = new ConnectionService();
        private ITransportSecurityService transportService = new TransportSecurityService();
        private IDecryptorService decryptorService = new DecryptorService();
        private IEncryptorService encryptorService = new EncryptorService();
        private TcpClient _activeClient;
        private NetworkStream _activeStream;

        public Form1()
        {
            InitializeComponent();
        }

        private async Task StartListeningAsync()
        {
            byte[] buffer = new byte[8192];
            try
            {
                while (_activeClient != null && _activeClient.Connected)
                {
                    int bytesRead = await _activeStream.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead <= 0) continue;
                    string incoming = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Invoke((MethodInvoker)(() =>
                    {
                        MesajLogla(incoming);
                    }));
                }
            }
            catch { }
        }

        private void MesajLogla(string paket)
        {
            try
            {
                var p = paket.Split('|');
                if (p.Length < 4 || p[0] != "CLIENT") return;

                // Paket: CLIENT|MESSAGE|ALGO|DATA|KEY|IV
                string algoritma = p[2].Trim().ToUpperInvariant(); // Boşlukları sil ve büyük harf yap

                string transportDecrypted = transportService.Decrypt(p[3]).Trim();
                string securedKey = p.Length > 4 ? transportService.Decrypt(p[4]).Trim() : "";
                string securedIv = p.Length > 5 ? transportService.Decrypt(p[5]).Trim() : "";

                string keyToUse = !string.IsNullOrEmpty(securedKey) ? securedKey : textBox3.Text;

                // 🛡️ KRİTİK NOKTA: Buradaki algoritma ismi (CAESAR) 
                // DecryptorService içindeki case ile BİREBİR aynı olmalı.
                string plainText = decryptorService.DecryptByAlgorithm(algoritma, transportDecrypted, keyToUse, securedIv);

                clientLog.SelectionColor = Color.Red;
                clientLog.AppendText("[SUNUCUDAN YENİ MESAJ]\n");
                clientLog.SelectionColor = Color.Black;
                clientLog.AppendText($"Mesaj: {plainText}\n"); // Burası artık çözülmüş olmalı
                clientLog.AppendText("-----------------------\n");
                clientLog.ScrollToCaret();
            }
            catch (Exception ex) { clientLog.AppendText($"[HATA]: {ex.Message}\n"); }
        }

        private async Task SendMessageToServerAsync(string operation, string algorithm, string text, string key = "", string iv = "")
        {
            try
            {
                if (_activeClient == null || !_activeClient.Connected)
                {
                    var (_, client) = await connectionService.ConnectToServer();
                    _activeClient = client;
                    if (_activeClient == null) return;
                    _activeStream = _activeClient.GetStream();
                    _ = Task.Run(StartListeningAsync);
                }

                string encryptedByAlgo = algorithm switch
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
                    "RSA" => encryptorService.RsaEncrypt(text, key),
                    _ => text
                };

                string finalKeyToSend = key;
                if ((algorithm == "AES" || algorithm == "DES" || algorithm == "MANUEL_DES") && !string.IsNullOrEmpty(textBox1.Text))
                {
                    finalKeyToSend = encryptorService.RsaEncrypt(key, textBox1.Text);
                }

                string securedText = transportService.Encrypt(encryptedByAlgo);
                string securedKey = string.IsNullOrEmpty(finalKeyToSend) ? "" : transportService.Encrypt(finalKeyToSend);
                string securedIV = string.IsNullOrEmpty(iv) ? "" : transportService.Encrypt(iv);

                string msg = $"SUNUCU|{operation}|{algorithm}|{securedText}|{securedKey}|{securedIV}";
                byte[] data = Encoding.UTF8.GetBytes(msg);
                await _activeStream.WriteAsync(data, 0, data.Length);

                clientLog.SelectionColor = Color.Blue;
                clientLog.AppendText($"[GÖNDERİLDİ]: {algorithm}\n");
                clientLog.SelectionColor = Color.Black;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
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

        private async void button2_Click(object sender, EventArgs e) => await SendMessageToServerAsync("Encrypt", "CAESAR", textBox2.Text);
        private async void button3_Click(object sender, EventArgs e) => await SendMessageToServerAsync("Encrypt", "VIGENERE", textBox2.Text, textBox3.Text);
        private async void button4_Click(object sender, EventArgs e) => await SendMessageToServerAsync("Encrypt", "SUBSTITUTION", textBox2.Text, textBox3.Text);
        private async void button5_Click(object sender, EventArgs e) => await SendMessageToServerAsync("Encrypt", "AFFINE", textBox2.Text);
        private async void button9_Click(object sender, EventArgs e) => await SendMessageToServerAsync("Encrypt", "ROTA", textBox2.Text, textBox3.Text);
        private async void button10_Click(object sender, EventArgs e) => await SendMessageToServerAsync("Encrypt", "COLUMNAR", textBox2.Text, textBox3.Text);
        private async void button11_Click(object sender, EventArgs e) => await SendMessageToServerAsync("Encrypt", "POLYBIUS", textBox2.Text, textBox3.Text);
        private async void button12_Click(object sender, EventArgs e) => await SendMessageToServerAsync("Encrypt", "PIGPEN", textBox2.Text, textBox3.Text);
        private async void button13_Click(object sender, EventArgs e) => await SendMessageToServerAsync("Encrypt", "HILL", textBox2.Text, textBox3.Text);
        private async void button19_Click(object sender, EventArgs e) => await SendMessageToServerAsync("Encrypt", "TRENRAYI", textBox2.Text, textBox3.Text);
        private async void button21_Click(object sender, EventArgs e) => await SendMessageToServerAsync("Encrypt", "AES", textBox2.Text, textBox3.Text, textBox7.Text);
        private async void button22_Click(object sender, EventArgs e) => await SendMessageToServerAsync("Encrypt", "DES", textBox2.Text, textBox3.Text, textBox7.Text);

        private async void button27_Click(object sender, EventArgs e)
        {
            if (textBox3.Text.Length != 8) return;
            await SendMessageToServerAsync("Encrypt", "MANUEL_DES", textBox2.Text, textBox3.Text, textBox7.Text);
        }

        private async void button25_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text)) return;
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                RsaAnahtarUret(out string pub, out string priv);
                textBox1.Text = pub;
                textBox4.Text = priv;
            }
            await SendMessageToServerAsync("Encrypt", "RSA", textBox2.Text, textBox1.Text);
        }
        private async void label1_Click(object sender, EventArgs e)

        {
        }

        private async void groupBox1_Enter(object sender, EventArgs e)

        {
        }
    }
}