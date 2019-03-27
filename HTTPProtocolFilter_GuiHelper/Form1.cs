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
using HTTPProtocolFilterTests.Utils;

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
            refreshDomains();
            refreshPhrases();
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
                for (int i = 0; i < blockLogs.Length; i++)
                    blockLogs[i] = blockLogs[i].Substring(blockLogs[i].IndexOf("||") + 2);
            }
        }

        #endregion

        #region Simulator Menu

        private void getBlockedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IHTTPFilter filter = mainPolicy;
            lbxSimulated.Items.Clear();
            var linq = blockLogs.Where((log) => !filter.isWhitelistedURL(new Uri(log)));
            lblLogStatus.Text = string.Format("{0}/{1}", linq.Count(), blockLogs.Length);
            lbxSimulated.Items.AddRange(
                linq.Take(1000).ToArray<object>()
            );
        }

        private void getAllowedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IHTTPFilter filter = mainPolicy;
            lbxSimulated.Items.Clear();
            var linq = blockLogs.Where((log) => filter.isWhitelistedURL(new Uri(log)));
            lblLogStatus.Text = string.Format("{0}/{1}", linq.Count(), blockLogs.Length);
            lbxSimulated.Items.AddRange(
                linq.Take(1000).ToArray<object>()
            );
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            lbxSimulated.Items.Clear();
            var linq = blockLogs.Where(
                (log) =>
                {
                    Uri uri = new Uri(log);
                    return !mainPolicy.isWhitelistedHost(uri.Host) && mainPolicy.checkPhrase(uri.PathAndQuery);
                }
                );

            lblLogStatus.Text = string.Format("{0}/{1}", linq.Count(), blockLogs.Length);
            lbxSimulated.Items.AddRange(
                linq.Take(1000).ToArray<object>()
            );
        }

        private void orderedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lbxSimulated.Items.Clear();
            var linq = blockLogs.Where(
                (log) => {
                    Uri uri = new Uri(log);
                    return !mainPolicy.isWhitelistedHost(uri.Host) && mainPolicy.checkPhrase(uri.PathAndQuery);
                }
                ).OrderBy(
                (log) => new Uri(log).Host
                );

            lblLogStatus.Text = string.Format("{0}/{1}", linq.Count(), blockLogs.Length);
            lbxSimulated.Items.AddRange(
                linq.Take(1000).ToArray<object>()
            );
        }

        private void orderedGroupedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lbxSimulated.Items.Clear();
            var linq = blockLogs.Where(
                (log) => {
                    Uri uri = new Uri(log);
                    return !mainPolicy.isWhitelistedHost(uri.Host) && mainPolicy.checkPhrase(uri.PathAndQuery);
                }
                ).GroupBy(
                (log) => new Uri(log).Host,
                (log) => log,
                (key, hosts) => hosts.First()
                );

            lblLogStatus.Text = string.Format("{0}/{1}", linq.Count(), blockLogs.Length);
            lbxSimulated.Items.AddRange(
                linq.Take(1000).ToArray<object>()
            );
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            lbxSimulated.Items.Clear();
            var linq = blockLogs.Where(
                (log) =>
                {
                    Uri uri = new Uri(log);
                    return !mainPolicy.isWhitelistedHost(uri.Host) && mainPolicy.checkPhrase(uri.PathAndQuery);
                }
                );

            lblLogStatus.Text = string.Format("{0}/{1}", linq.Count(), blockLogs.Length);
            lbxSimulated.Items.AddRange(
                linq.Take(2000).ToArray<object>()
            );
        }

        private void newDomainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string url = lbxSimulated.SelectedItem as string;
            if (url != null)
            {
                Uri u = new Uri(url);
                mainPolicy.AllowedDomains.Add(new AllowDomain()
                {
                    DomainFormat = u.Host,
                    Type = AllowDomainType.EXACT,
                    WhiteListEP = new List<AllowEP>()
                });

                mainPolicy.AllowedDomains = mainPolicy.AllowedDomains;

                refreshDomains();
            }
        }

        private void domainEPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string url = lbxSimulated.SelectedItem as string;
            if (url != null)
            {
                Uri u = new Uri(url);
                mainPolicy.AllowedDomains.Add(new AllowDomain()
                {
                    DomainFormat = u.Host,
                    Type = AllowDomainType.EXACT,
                    WhiteListEP = new List<AllowEP>()
                    {
                        new AllowEP()
                        {
                            EpFormat = u.AbsolutePath,
                            Type = AllowEPType.STARTWITH
                        }
                    }
                });

                mainPolicy.AllowedDomains = mainPolicy.AllowedDomains;

                refreshDomains();
            }
        }

        private void subdomainsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string url = lbxSimulated.SelectedItem as string;
            if (url != null)
            {
                Uri u = new Uri(url);
                string[] hostParts = u.Host.Split('.');
                string newHost = "";

                if (hostParts.Length <= 2)
                {
                    newHost = u.Host;
                }
                else 
                {
                    if (hostParts[hostParts.Length -2 ] == "co")
                    {
                        // chose last 3
                        newHost = string.Join(".", new[] {
                            hostParts[hostParts.Length -3 ],
                            hostParts[hostParts.Length -2 ],
                            hostParts[hostParts.Length -1 ]
                        });
                    }
                    else
                    {
                        // chose last 2
                        newHost = string.Join(".", new[] {
                            hostParts[hostParts.Length -2 ],
                            hostParts[hostParts.Length -1 ]
                        });
                    }
                }

                mainPolicy.AllowedDomains.Add(new AllowDomain()
                {
                    DomainFormat = newHost,
                    Type = AllowDomainType.SUBDOMAINS,
                    WhiteListEP = new List<AllowEP>()
                });

                mainPolicy.AllowedDomains = mainPolicy.AllowedDomains;

                refreshDomains();
            }
        }

        private void lbxSimulated_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxSimulated.SelectedIndex > -1)
                txtSimulatedSelected.Text = (string)lbxSimulated.SelectedItem;
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

               gpEditDomain.Enabled = true;
            }
            else
            {
               gpEditDomain.Enabled = false;
            }
        }

        private void lbxEp_SelectedIndexChanged(object sender, EventArgs e)
        {
            AllowEP ep = lbxEp.SelectedItem as AllowEP;
            if (ep != null)
            {
                txtEpPattern.Text = ep.EpFormat;
                cbEpType.SelectedIndex = (int)ep.Type;

                gpEditEp.Enabled = true;
            }
            else
            {
                gpEditEp.Enabled = false;
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
