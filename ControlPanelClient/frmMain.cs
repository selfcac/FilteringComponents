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

       

        public static string evilPayLoad()
        {
            string s20 = "01234567890123456789";
            string result = "";
            for (int i = 0; i < 100; i++) result += s20;
            return result;
        }

        async Task doCommand(Func<Task<string>> action)
        {
            string result = "";
            try
            {
                result = await action();
            }
            catch (Exception ex)
            {
                result = ex.ToString();
            }
            MessageBox.Show(result);
        }

        private async void btnEcho_Click(object sender, EventArgs e)
        {
            await doCommand(async () =>
            {
                return await Common.Scenarios.Echo_Client();
            });
        }

        private async void btnStartProxy_Click(object sender, EventArgs e)
        {
            await doCommand(async () =>
            {
                return await Common.Scenarios.Proxy_Client(start: true);
            });
        }

        private async void btnStopProxy_Click(object sender, EventArgs e)
        {
            await doCommand(async () =>
            {
                return await Common.Scenarios.Proxy_Client(start: false);
            });
        }

        private async void btnChangePassword_Click(object sender, EventArgs e)
        {
            await doCommand(async () =>
            {
                return await Common.Scenarios.ChangePass_Client(txtNewPassword.Text);
            });
            txtNewPassword.Text = "";
        }

        private async void btnStartFirewall_Click(object sender, EventArgs e)
        {
            await doCommand(async () =>
            {
                return await Common.Scenarios.Firewall_client(start: true);
            });
        }

        private async void btnStopFireWall_Click(object sender, EventArgs e)
        {
            await doCommand(async () =>
            {
                return await Common.Scenarios.Firewall_client(start: false);
            });
        }

        private async void btnIsLocked_Click(object sender, EventArgs e)
        {
            await doCommand(async () =>
            {
                return await Common.Scenarios.Lock_Client(true, DateTime.Now);
            });
        }

        private async void btnLockAdmin_Click(object sender, EventArgs e)
        {
            await doCommand(async () =>
            {
                return await Common.Scenarios.Lock_Client(false, dateLockUntil.Value);
            });
        }

        private async void btnAddWhiteList_Click(object sender, EventArgs e)
        {
            await doCommand(async () =>
            {
                return await Common.Scenarios.ADDURL_Client(txtWhitelist.Text);
            });
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            string port = Common.Config.Instance.ProxyPort.ToString();
            cbUrlBlock.Items.Add("http://127.0.0.1:" + port + Common.ProxyCommands.LOG_SHOW);
            cbUrlBlock.Items.Add("http://127.0.0.1:" + port + Common.ProxyCommands.LOG_CLEAR);
            cbUrlBlock.Items.Add("http://127.0.0.1:" + port + Common.ProxyCommands.LOG_DISTINCT);
        }

        private void btnDeleteLog_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(cbUrlBlock.SelectedItem.ToString());
        }
    }
}
