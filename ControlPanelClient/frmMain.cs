using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlPanelClient
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnLockAdmin_Click(object sender, EventArgs e)
        {
            grpAdmin.Enabled = false;
        }

        public static string evilPayLoad()
        {
            string s20 = "01234567890123456789";
            string result = "";
            for (int i = 0; i < 100; i++) result += s20;
            return result;
        }

        private async void btnEcho_Click(object sender, EventArgs e)
        {
            string result = "";
            try
            {
               result = await Common.Scenarios.Echo_Start();
            }
            catch (Exception ex)
            {
                result = ex.ToString();
            }
            MessageBox.Show(result);
        }
    }
}
