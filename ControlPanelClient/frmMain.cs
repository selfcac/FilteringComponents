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

        private void frmMain_Load(object sender, EventArgs e)
        {
            //string port = Common.Config.Instance.ProxyPort.ToString();
            //cbUrlBlock.Items.Add("http://127.0.0.1:" + port + Common.ProxyCommands.LOG_SHOW);
            //cbUrlBlock.Items.Add("http://127.0.0.1:" + port + Common.ProxyCommands.LOG_CLEAR);
            //cbUrlBlock.Items.Add("http://127.0.0.1:" + port + Common.ProxyCommands.LOG_DISTINCT);
        }

        private async void btnEcho_Click(object sender, EventArgs e)
        {
            await doCommand(async () =>
            {
                return await Common.Scenarios.Echo_Client();
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

        string passSource = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!~@$*";
        string randomPass(int len)
        {
            Random rnd = new Random();
            string result = "";

            for (int i=0;i<len;i++)
            {
                result += passSource[rnd.Next(0, passSource.Length)];
            }

            return result;
        }

        private async void btnRandom_Click(object sender, EventArgs e)
        {
            await doCommand(async () =>
            {
                return await Common.Scenarios.ChangePass_Client(randomPass(20));
            });
        }

       
    }
}
