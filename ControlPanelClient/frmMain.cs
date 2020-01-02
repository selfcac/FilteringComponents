using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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

        void log(string text)
        {
            string logEntry = string.Format("[{0}] {1}", DateTime.Now, text);
            if (rtbLog.InvokeRequired)
            {
                rtbLog.Invoke(new Action(() => { log(text); }));
            }
            else
            {
                rtbLog.Text = logEntry + "\n" + rtbLog.Text;
            }
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

            log(result);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            rtbLog.Text = ">>> this version:\n" + Properties.Resources.GitInfo + ">>> Common:\n" + Common.GitInfo.GetInfo();

            foreach(var cmd in Common.Config.Instance.ALLOWED_COMMANDS)
            {
                cbAllowedCMD.Items.Add(cmd.name);
            }

            foreach (var cmd in Common.Config.Instance.ADMIN_COMMANDS)
            {
                cbAdminCMD.Items.Add(cmd.name);
            }
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

        private async void btnResetUSB_Click(object sender, EventArgs e)
        {
            if (dlgUserReset.ShowDialog() == DialogResult.OK && File.Exists(dlgUserReset.FileName))
            {
                await doCommand(async () =>
                {
                    return await Common.Scenarios.ResetPass_Client(dlgUserReset.FileName);
                });
            }
        }

        private async void btnAllowedRun_Click(object sender, EventArgs e)
        {
            await doCommand(async () =>
            {
                return await Common.Scenarios.Allowed_command_client(cbAllowedCMD.SelectedIndex);
            });
        }

        private async void btnAdminRun_Click(object sender, EventArgs e)
        {
            await doCommand(async () =>
            {
                return await Common.Scenarios.Admin_command_client(cbAdminCMD.SelectedIndex);
            });
        }

        private async void btnResetUnlock_Click(object sender, EventArgs e)
        {
            if (dlgUserReset.ShowDialog() == DialogResult.OK && File.Exists(dlgUserReset.FileName))
            {
                await doCommand(async () =>
                {
                    return await Common.Scenarios.RESET_UNLOCK_Client(dlgUserReset.FileName);
                });
            }
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            rtbLog.Text = "";
        }
    }
}
