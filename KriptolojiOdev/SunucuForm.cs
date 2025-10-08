using KriptolojiOdev.Baglanti.Class;
using KriptolojiOdev.Baglanti.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
