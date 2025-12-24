using KriptolojiOdev.Baglanti.Class;
using KriptolojiOdev.Baglanti.Interface;
using KriptolojiOdev.Sifreleme.Class;
using KriptolojiOdev.Sifreleme.Interface;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Diagnostics;

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
            OtomatikAnahtarHazirla();
        }

        private void OtomatikAnahtarHazirla()
        {
            RsaAnahtarUret(out string rsaPub, out string rsaPriv);
            textBox1.Text = rsaPub;
            textBox4.Text = rsaPriv;
            encryptorService.EccKeyGenerate(out string eccPub, out string eccPriv);
            textBox5.Text = eccPub;
            textBox6.Text = eccPriv;
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
                    Invoke((MethodInvoker)(() => { MesajLogla(incoming); }));
                }
            }
            catch { }
        }

        private void MesajLogla(string paket)
        {
            try
            {
                var p = paket.Split('|');
                if (p.Length < 4) return;

                string transportDecrypted = transportService.Decrypt(p[3]).Trim();

                if (transportDecrypted.Contains("SUNUCU|KEY_EXCHANGE"))
                {
                    var parts = transportDecrypted.Split('|');
                    Invoke((MethodInvoker)(() =>
                    {
                        textBox8.Text = parts[2];
                        textBox9.Text = parts[3];
                        clientLog.AppendText("[SİSTEM]: Sunucu anahtarları başarıyla yüklendi.\n");
                    }));
                    return;
                }

                if (p[0] != "CLIENT") return;

                string algoritma = p[2].Trim().ToUpperInvariant();
                string securedKey = p.Length > 4 ? transportService.Decrypt(p[4]).Trim() : "";
                string securedIv = p.Length > 5 ? transportService.Decrypt(p[5]).Trim() : "";
                string secType = p.Length > 6 ? p[6] : "KLASİK";

                string actualKey = securedKey;
                if (!string.IsNullOrEmpty(securedKey) && (secType == "RSA" || secType == "ECC"))
                {
                    if (secType == "ECC")
                        actualKey = decryptorService.EccDecrypt(securedKey, textBox6.Text);
                    else
                        actualKey = decryptorService.RsaDecrypt(securedKey, textBox4.Text);
                }

                Stopwatch sw = Stopwatch.StartNew();
                string plainText = decryptorService.DecryptByAlgorithm(algoritma, transportDecrypted, actualKey, securedIv);
                sw.Stop();
                clientLog.SelectionColor = Color.Red;
                clientLog.AppendText($"[SUNUCUDAN MESAJ] ({secType})\n");
                clientLog.SelectionColor = Color.Black;
                clientLog.AppendText($"Mesaj: {plainText}\n");
                clientLog.SelectionColor = Color.DarkGray; 
                clientLog.AppendText($"Çözülme Süresi: {sw.Elapsed.TotalMilliseconds} ms\n");
                clientLog.SelectionColor = Color.Black;
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
                    string handshake = $"CLIENT|KEY_EXCHANGE|{textBox1.Text}|{textBox5.Text}";
                    byte[] hsData = Encoding.UTF8.GetBytes(handshake);
                    await _activeStream.WriteAsync(hsData, 0, hsData.Length);
                    clientLog.AppendText("[SİSTEM]: Bağlanıldı, anahtarlar gönderildi.\n");
                    return;
                }

                if ((algorithm == "AES" || algorithm == "DES" || algorithm == "RSA") && string.IsNullOrEmpty(textBox8.Text))
                {
                    MessageBox.Show("Önce sunucu anahtarlarının gelmesini bekleyin!");
                    return;
                }
                Stopwatch sw = Stopwatch.StartNew();
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
                    "RSA" => encryptorService.RsaEncrypt(text, textBox8.Text),
                    _ => text
                };
                sw.Stop();
                double encryptionDuration = sw.Elapsed.TotalMilliseconds;
                string finalKeyToSend = key;
                string secType = "KLASİK";

                if (!string.IsNullOrEmpty(key) && (algorithm == "AES" || algorithm == "DES" || algorithm == "MANUEL_DES"))
                {
                    if (radioButton2.Checked)
                    {
                        finalKeyToSend = encryptorService.EccEncrypt(key, textBox9.Text);
                        secType = "ECC";
                    }
                    else
                    {
                        finalKeyToSend = encryptorService.RsaEncrypt(key, textBox8.Text);
                        secType = "RSA";
                    }
                }

                string securedText = transportService.Encrypt(encryptedByAlgo);
                string securedKey = string.IsNullOrEmpty(finalKeyToSend) ? "" : transportService.Encrypt(finalKeyToSend);
                string securedIV = string.IsNullOrEmpty(iv) ? "" : transportService.Encrypt(iv);

                string msg = $"SUNUCU|{operation}|{algorithm}|{securedText}|{securedKey}|{securedIV}|{secType}";
                byte[] data = Encoding.UTF8.GetBytes(msg);
                await _activeStream.WriteAsync(data, 0, data.Length);
                clientLog.SelectionColor = Color.Blue;
                clientLog.AppendText($"[GÖNDERİLDİ]: {algorithm} ({secType})\n");
                clientLog.SelectionColor = Color.DarkGray;
                clientLog.AppendText($"Şifreleme Süresi: {encryptionDuration} ms\n");
            }
            catch (Exception ex) { MessageBox.Show("Hata: " + ex.Message); }
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
            await SendMessageToServerAsync("Encrypt", "RSA", textBox2.Text);
        }
        private async void label1_Click(object sender, EventArgs e) { }
        private async void groupBox1_Enter(object sender, EventArgs e) { }

    }
}