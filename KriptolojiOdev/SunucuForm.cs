using KriptolojiOdev.Baglanti.Class;
using KriptolojiOdev.Baglanti.Interface;
using KriptolojiOdev.Sifreleme.Class;
using KriptolojiOdev.Sifreleme.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KriptolojiOdev
{
    public partial class SunucuForm : Form
    {
        private IConnectionService connectionService = new ConnectionService();
        private IEncryptorService encryptorService = new EncryptorService();
        public SunucuForm()
        {
            InitializeComponent();
            connectionService.OnMessage = (msg) =>
            {
                this.Invoke((MethodInvoker)(() =>
                {
                    serverLog.AppendText(msg + Environment.NewLine);
                }));
            };
        }
        public string MesajYaz(string mesaj)
        {
            var gelen = mesaj.Split('|');
            var sifreleme = gelen[0];
            string sifrele = sifreleme switch
            {
                "CAESAR" => encryptorService.CaesarEncrypt(mesaj),
                "VİGENERE" => encryptorService.VigenereEncrypt(mesaj),
                "SUBSTİTİUİON" => encryptorService.SubstitutionEncrypt(mesaj),
                "AFFİNE" => encryptorService.SubstitutionEncrypt(mesaj),
                _ => mesaj 
            };
            return sifrele;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                serverLog.AppendText(connectionService.StartServer());
            }
            catch (Exception ex)
            {
                serverLog.AppendText("Hata: " + ex.Message + Environment.NewLine);
                return;
            }
        }
    }
}
