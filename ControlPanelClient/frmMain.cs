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

        private async void btnEcho_Click(object sender, EventArgs e)
        {
            string result = "";
            try
            {
               result = await TcpCommands.Echo();
            }
            catch (Exception ex)
            {
                result = ex.ToString();
            }
            MessageBox.Show(result);
        }
    }
}
