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

            refreshDomains();
            refreshPhrases();
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
            rtbSimulator.Text =
                string.Join("\r\n", blockLogs.Where((log) => !filter.isWhitelistedURL(new Uri(log))));
        }

        private void getAllowedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IHTTPFilter filter = mainPolicy;
            rtbSimulator.Text =
                string.Join("\r\n", blockLogs.Where((log) => filter.isWhitelistedURL(new Uri(log))));
        }


        #endregion

        #region Blocked Phrases

        private void refreshPhrases()
        {
            lbxPhrases.DataSource = null;
            lbxPhrases.DataSource = mainPolicy.BlockedPhrases;
        }

        private void addPhraseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainPolicy.BlockedPhrases.Add(new PhraseFilter()
            {
                Phrase = "Enter phrase",
                Type = BlockPhraseType.CONTAIN
            });

            refreshPhrases();
        }

        private void deleteSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lbxPhrases.SelectedItem != null)
            {
                mainPolicy.BlockedPhrases.Remove((PhraseFilter)lbxPhrases.SelectedItem);
            }

            refreshPhrases();
        }

        private void lbxPhrases_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxPhrases.SelectedItem != null)
            {
                PhraseFilter p = (PhraseFilter)lbxPhrases.SelectedItem;

                txtPhrase.Text = p.Phrase;
                cbPhraseType.SelectedIndex = (int)p.Type;

                gpEditPhrase.Enabled = true;
            }
            else
            {
                gpEditPhrase.Enabled = false;
            }
        }

        private void btnPApply_Click(object sender, EventArgs e)
        {
            PhraseFilter p = lbxPhrases.SelectedItem as PhraseFilter;
            if (p != null)
            {
                p.Type = (BlockPhraseType)cbPhraseType.SelectedIndex;
                p.Phrase = txtPhrase.Text;
                refreshPhrases();
            }
        }


        #endregion

        #region Domains and EP
        private void refreshDomains()
        {
            lbxDomains.DataSource = null;
            lbxDomains.DataSource = mainPolicy.AllowedDomains;
        }

        private void refreshEPs()
        {
            if (lbxDomains.SelectedItem != null)
            {
                lbxEp.DataSource = null;
                lbxEp.DataSource = ((AllowDomain)lbxDomains.SelectedItem).WhiteListEP;
            }
        }

        private void addDomainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainPolicy.AllowedDomains.Add(new AllowDomain()
            {
                DomainFormat = "Enter.Domain",
                Type = AllowDomainType.EXACT,
                WhiteListEP = new List<AllowEP>()
            });

            mainPolicy.AllowedDomains = mainPolicy.AllowedDomains;

            refreshDomains();
        }

        private void addEPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AllowDomain d = lbxDomains.SelectedItem as AllowDomain;
            if (d != null)
            {
                d.WhiteListEP.Add(new AllowEP()
                {
                    EpFormat = "/enter/ep",
                    Type = AllowEPType.CONTAIN
                });
                refreshEPs();
            }

        }

        private void deleteDomainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AllowDomain d = lbxDomains.SelectedItem as AllowDomain;
            if (d != null)
            {
                mainPolicy.AllowedDomains.Remove(d);
                refreshDomains();

                mainPolicy.AllowedDomains = mainPolicy.AllowedDomains;
            }
        }

        private void deleteEPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AllowDomain d = lbxDomains.SelectedItem as AllowDomain;
            AllowEP ep = lbxEp.SelectedItem as AllowEP;
            if (d != null && ep != null)
            {
                d.WhiteListEP.Remove(ep);
                refreshDomains();
                refreshEPs();
            }
        }

        private void lbxDomains_SelectedIndexChanged(object sender, EventArgs e)
        {
            AllowDomain d = lbxDomains.SelectedItem as AllowDomain;
            if (d != null)
            {
                txtDomainPattern.Text = d.DomainFormat;
                cbDomainType.SelectedIndex = (int)d.Type;

                refreshEPs();

                gpEditDomain.Visible = gpEditDomain.Enabled = true;
            }
            else
            {
                gpEditDomain.Visible = gpEditDomain.Enabled = false;
            }
        }

        private void lbxEp_SelectedIndexChanged(object sender, EventArgs e)
        {
            AllowEP ep = lbxEp.SelectedItem as AllowEP;
            if (ep != null)
            {
                txtEpPattern.Text = ep.EpFormat;
                cbEpType.SelectedIndex = (int)ep.Type;

                gpEditEp.Visible = gpEditEp.Enabled = true;
            }
            else
            {
                gpEditEp.Visible = gpEditEp.Enabled = false;
            }
        }

        

        private void btnDApply_Click(object sender, EventArgs e)
        {
            AllowDomain d = lbxDomains.SelectedItem as AllowDomain;
            if (d != null)
            {
                d.DomainFormat = txtDomainPattern.Text;
                d.Type = (AllowDomainType)cbDomainType.SelectedIndex;

                mainPolicy.AllowedDomains = mainPolicy.AllowedDomains;
                refreshDomains();
            }
        }

        private void btnEPApply_Click(object sender, EventArgs e)
        {
            AllowEP ep = lbxEp.SelectedItem as AllowEP;
            if (ep != null)
            {
                ep.EpFormat = txtEpPattern.Text;
                ep.Type = (AllowEPType)cbEpType.SelectedIndex;
                refreshDomains();
                refreshEPs();
            }
        }

        #endregion

       
    }
}
