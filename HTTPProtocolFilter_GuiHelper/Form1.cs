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
using HTTPProtocolFilter;

namespace HTTPProtocolFilter_GuiHelper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        FilterPolicy mainPolicy = new FilterPolicy();
        string[] blockLogs =  { };

        #region Tools

        private void Form1_Load(object sender, EventArgs e)
        {
            gpEditEp.Dock = gpEditDomain.Dock = DockStyle.Fill;
        }

        

        private void changeEditPanel(bool toDomain)
        {
            // Was docked to fill in form.load()
            gpEditDomain.Visible = toDomain;
            gpEditEp.Visible = !toDomain;
        }

        #endregion

        #region  Policy Menu

        private void clearPolicyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainPolicy = new FilterPolicy();
        }

        private void loadPolicyJsonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dlgOpen.Title = "Open existing policy:";
            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                mainPolicy.reloadPolicy(dlgOpen.FileName);
            }
        }

        private void savePolicyJsonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainPolicy.proxyMode = WorkingMode.ENFORCE;
            if (MessageBox.Show("In mapping mode?", "Choose mode", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                mainPolicy.proxyMode = WorkingMode.MAPPING;
            }

            dlgSave.Title = "Save policy:";
            if (dlgSave.ShowDialog() == DialogResult.OK)
            {
                mainPolicy.savePolicy(dlgSave.FileName);
            }
        }

        private void loadLogFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dlgOpen.Title = "Open block log file:";
            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                blockLogs = File.ReadAllLines(dlgOpen.FileName);
            }
        }

        #endregion

        #region Simulator Menu

        private void getBlockedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IHTTPFilter filter = mainPolicy;
            lbxPhrases.Items.Clear();
            lbxPhrases.Items.AddRange(
                blockLogs.Where((log) => !filter.isWhitelistedURL(new Uri(log))).ToArray<object>()
            );
        }

        private void getAllowedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IHTTPFilter filter = mainPolicy;
            lbxPhrases.Items.Clear();
            lbxPhrases.Items.AddRange(
                blockLogs.Where((log) => filter.isWhitelistedURL(new Uri(log))).ToArray<object>()
            );
        }

        #endregion


    }
}
