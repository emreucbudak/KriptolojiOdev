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
        private IEncryptorService encryptorService = new EncryptorService();
        private IDecryptorService decryptorService = new DecryptorService();
        private SunucuForm serverForm;
        private TcpClient _activeClient;
        private NetworkStream _activeStream;

        public Form1(SunucuForm serverForm)
        {
            InitializeComponent();
            this.serverForm = serverForm;
        }

        private async Task StartListeningAsync()
        {
            byte[] buffer = new byte[8192];
            try
            {
                while (_activeClient != null && _activeClient.Connected)
                {
                    int bytesRead = await _activeStream.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead > 0)
                    {
                        string incoming = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        this.Invoke((MethodInvoker)(() =>
                        {
                            MesajCozVeLogla(incoming);
                        }));
                    }
                }
            }
            catch { }
        }

        private void MesajCozVeLogla(string paket)
        {
            try
            {
                var parcalar = paket.Split('|');
                if (parcalar.Length < 4 || parcalar[0] != "CLIENT") return;

                string algoritma = parcalar[2].ToUpperInvariant();
                string sifreliMetin = transportService.Decrypt(parcalar[3]);
                string gelenKey = parcalar.Length > 4 ? transportService.Decrypt(parcalar[4]) : "";
                string iv = parcalar.Length > 5 ? transportService.Decrypt(parcalar[5]) : "";

                string gercekKey = gelenKey;

                if (!string.IsNullOrEmpty(gelenKey) && (algoritma == "AES" || algoritma == "DES" || algoritma == "MANUEL_DES"))
                {
                    try
                    {
                        gercekKey = decryptorService.RsaDecrypt(gelenKey, textBox4.Text);
                    }
                    catch { }
                }




            }
            catch { }
        }

        private async Task SendMessageToServerAsync(string operation, string algorithm, string text, string key = "", string iv = "")
        {
            try
            {
                if (_activeClient == null || !_activeClient.Connected)
                {
                    var (info, client) = await connectionService.ConnectToServer();
                    _activeClient = client;
                    if (_activeClient != null)
                    {
                        _activeStream = _activeClient.GetStream();
                        _ = Task.Run(() => StartListeningAsync());
                    }
                }

                if (_activeClient == null) return;

                string finalKey = key;
                string algoUpper = algorithm.ToUpperInvariant();

                if (!string.IsNullOrEmpty(key) && (algoUpper == "AES" || algoUpper == "DES" || algoUpper == "MANUEL_DES" || algoUpper == "RSA"))
                {
                    if (!string.IsNullOrEmpty(textBox1.Text))
                        finalKey = encryptorService.RsaEncrypt(key, textBox1.Text);
                }

                string securedText = transportService.Encrypt(text);
                string securedKey = string.IsNullOrEmpty(finalKey) ? "" : transportService.Encrypt(finalKey);
                string securedIV = string.IsNullOrEmpty(iv) ? "" : transportService.Encrypt(iv);

                string msg = $"SUNUCU|{operation}|{algoUpper}|{securedText}|{securedKey}|{securedIV}";
                byte[] data = Encoding.UTF8.GetBytes(msg);
                await _activeStream.WriteAsync(data, 0, data.Length);

                clientLog.AppendText($"[GÖNDERÝLDÝ]: {algoUpper}{Environment.NewLine}");
            }
            catch (Exception ex)
            {
                clientLog.AppendText($"Hata: {ex.Message}{Environment.NewLine}");
            }
        }

        public async Task SendRawMessageAsync(string rawMessage)
        {
            if (_activeClient != null && _activeClient.Connected)
            {
                byte[] data = Encoding.UTF8.GetBytes(rawMessage);
                await _activeStream.WriteAsync(data, 0, data.Length);
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
        private async void button4_Click(object sender, EventArgs e) => await SendMessageToServerAsync("Encrypt", "SUBSTITUTION", textBox2.Text, textBox3.Text);
        private async void button5_Click(object sender, EventArgs e) => await SendMessageToServerAsync("Encrypt", "AFFINE", textBox2.Text);
        private async void button3_Click(object sender, EventArgs e) => await SendMessageToServerAsync("Encrypt", "VIGENERE", textBox2.Text, textBox3.Text);
        private async void button9_Click(object sender, EventArgs e) => await SendMessageToServerAsync("Encrypt", "ROTA", textBox2.Text, textBox3.Text);
        private async void button10_Click(object sender, EventArgs e) => await SendMessageToServerAsync("Encrypt", "COLUMNAR", textBox2.Text, textBox3.Text);
        private async void button11_Click(object sender, EventArgs e) => await SendMessageToServerAsync("Encrypt", "POLYBIUS", textBox2.Text, textBox3.Text);
        private async void button12_Click(object sender, EventArgs e) => await SendMessageToServerAsync("Encrypt", "PIGPEN", textBox2.Text, textBox3.Text);
        private async void button13_Click(object sender, EventArgs e) => await SendMessageToServerAsync("Encrypt", "HILL", textBox2.Text, textBox3.Text);
        private async void button19_Click(object sender, EventArgs e) => await SendMessageToServerAsync("Encrypt", "TRENRAYI", textBox2.Text, textBox3.Text);
        private async void button21_Click(object sender, EventArgs e) => await SendMessageToServerAsync("Encrypt", "AES", textBox2.Text, textBox3.Text, textBox7.Text);
        private async void button22_Click(object sender, EventArgs e) => await SendMessageToServerAsync("Encrypt", "DES", textBox2.Text, textBox3.Text, textBox7.Text);

        private async void button25_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text)) return;
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                string pub, priv;
                RsaAnahtarUret(out pub, out priv);
                textBox1.Text = pub;
                textBox4.Text = priv;
            }
            await SendMessageToServerAsync("Encrypt", "RSA", textBox2.Text, textBox1.Text, "");
        }

        private async void button27_Click(object sender, EventArgs e)
        {
            if (textBox3.Text.Length != 8) return;
            await SendMessageToServerAsync("Encrypt", "MANUEL_DES", textBox2.Text, textBox3.Text, textBox7.Text);
        }

        private void label1_Click(object sender, EventArgs e) { }
        private void groupBox1_Enter(object sender, EventArgs e) { }
    }
}