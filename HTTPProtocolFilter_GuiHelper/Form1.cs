﻿using System;
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
            gpEditAllowEp.Dock = gpEditDomain.Dock = DockStyle.Fill;
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
            var linq = blockLogs.Where((log) => !filter.isWhitelistedURL(new Uri(log), out _));
            lblLogStatus.Text = string.Format("{0}/{1}", linq.Count(), blockLogs.Length);
            lbxSimulated.Items.AddRange(
                linq.Take(1000).ToArray<object>()
            );
        }

        private void getAllowedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IHTTPFilter filter = mainPolicy;
            lbxSimulated.Items.Clear();
            var linq = blockLogs.Where((log) => filter.isWhitelistedURL(new Uri(log), out _));
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
                    return !mainPolicy.isWhitelistedHost(uri.Host) 
                            && mainPolicy.isContentAllowed(uri.PathAndQuery, BlockPhraseScope.URL, out _);
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
                    return !mainPolicy.isWhitelistedHost(uri.Host) &&
                            mainPolicy.isContentAllowed(uri.PathAndQuery, BlockPhraseScope.URL, out _);
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
                    return !mainPolicy.isWhitelistedHost(uri.Host) &&
                            mainPolicy.isContentAllowed(uri.PathAndQuery, BlockPhraseScope.URL, out _);
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
                    return !mainPolicy.isWhitelistedHost(uri.Host) &&
                            mainPolicy.isContentAllowed(uri.PathAndQuery, BlockPhraseScope.URL, out _);
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
                mainPolicy.AllowedDomains.Add(new DomainPolicy()
                {
                    DomainFormat = u.Host,
                    Type = AllowDomainType.EXACT,
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
                mainPolicy.AllowedDomains.Add(new DomainPolicy()
                {
                    DomainFormat = u.Host,
                    Type = AllowDomainType.EXACT,
                    AllowEP = new List<EPPolicy>()
                    {
                        new EPPolicy()
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

                mainPolicy.AllowedDomains.Add(new DomainPolicy()
                {
                    DomainFormat = newHost,
                    Type = AllowDomainType.SUBDOMAINS,
                    AllowEP = new List<EPPolicy>(),
                    BlockEP = new List<EPPolicy>()
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
                cbPhraseScope.SelectedIndex = (int)p.Scope;
            }
            else
            {

            }
        }

        private void btnPApply_Click(object sender, EventArgs e)
        {
            PhraseFilter p = lbxPhrases.SelectedItem as PhraseFilter;
            if (p != null)
            {
                p.Type = (BlockPhraseType)cbPhraseType.SelectedIndex;
                p.Phrase = txtPhrase.Text;
                p.Scope = (BlockPhraseScope)cbPhraseScope.SelectedIndex;

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
                lbxAllowEp.DataSource = null;
                lbxAllowEp.DataSource = ((DomainPolicy)lbxDomains.SelectedItem).AllowEP;

                lbxBlockEp.DataSource = null;
                lbxBlockEp.DataSource = ((DomainPolicy)lbxDomains.SelectedItem).BlockEP;
            }
        }

        private void addDomainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainPolicy.AllowedDomains.Add(new DomainPolicy()
            {
                DomainFormat = "Enter.Domain",
                Type = AllowDomainType.EXACT,
                AllowEP = new List<EPPolicy>(),
                BlockEP = new List<EPPolicy>(),
            });

            mainPolicy.AllowedDomains = mainPolicy.AllowedDomains;

            refreshDomains();
        }

        

        private void deleteDomainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DomainPolicy d = lbxDomains.SelectedItem as DomainPolicy;
            if (d != null)
            {
                mainPolicy.AllowedDomains.Remove(d);
                refreshDomains();

                mainPolicy.AllowedDomains = mainPolicy.AllowedDomains;
            }
        }

        private void lbxDomains_SelectedIndexChanged(object sender, EventArgs e)
        {
            DomainPolicy d = lbxDomains.SelectedItem as DomainPolicy;
            if (d != null)
            {
                txtDomainPattern.Text = d.DomainFormat;
                cbDomainType.SelectedIndex = (int)d.Type;
                cbBlockDomain.Checked = d.DomainBlocked;

                refreshEPs();
            }
        }



        private void lbxEp_SelectedIndexChanged(object sender, EventArgs e)
        {
            EPPolicy ep = lbxAllowEp.SelectedItem as EPPolicy;
            if (ep != null)
            {
                txtEpAllowPattern.Text = ep.EpFormat;
                cbEpAllowType.SelectedIndex = (int)ep.Type;

            }
        }


        private void lbxEpBlock_SelectedIndexChanged(object sender, EventArgs e)
        {
            EPPolicy ep = lbxBlockEp.SelectedItem as EPPolicy;
            if (ep != null)
            {
                txtEpBlockPattern.Text = ep.EpFormat;
                cbEpBlockType.SelectedIndex = (int)ep.Type;

            }
        }



        private void btnDApply_Click(object sender, EventArgs e)
        {
            DomainPolicy d = lbxDomains.SelectedItem as DomainPolicy;
            if (d != null)
            {
                d.DomainFormat = txtDomainPattern.Text;
                d.Type = (AllowDomainType)cbDomainType.SelectedIndex;
                d.DomainBlocked = cbBlockDomain.Checked;

                mainPolicy.AllowedDomains = mainPolicy.AllowedDomains;
                refreshDomains();
            }
        }

        private void btnEPApply_Click(object sender, EventArgs e)
        {
            EPPolicy ep = lbxAllowEp.SelectedItem as EPPolicy;
            if (ep != null)
            {
                ep.EpFormat = txtEpAllowPattern.Text;
                ep.Type = (AllowEPType)cbEpAllowType.SelectedIndex;
                refreshDomains();
                refreshEPs();
            }
        }

        private void btnEPBlockApply_Click(object sender, EventArgs e)
        {
            EPPolicy ep = lbxBlockEp.SelectedItem as EPPolicy;
            if (ep != null)
            {
                ep.EpFormat = txtEpBlockPattern.Text;
                ep.Type = (AllowEPType)cbEpBlockType.SelectedIndex;
                refreshDomains();
                refreshEPs();
            }
        }

        #endregion

        private void btnAddAllowEP_Click(object sender, EventArgs e)
        {
            DomainPolicy d = lbxDomains.SelectedItem as DomainPolicy;
            if (d != null)
            {
                d.AllowEP.Add(new EPPolicy()
                {
                    EpFormat = "/z/enter/ep",
                    Type = AllowEPType.CONTAIN
                });
                refreshEPs();
            }
        }

        private void btnDelAllowEP_Click(object sender, EventArgs e)
        {
            DomainPolicy d = lbxDomains.SelectedItem as DomainPolicy;
            EPPolicy ep = lbxAllowEp.SelectedItem as EPPolicy;
            if (d != null && ep != null)
            {
                d.AllowEP.Remove(ep);
                refreshDomains();
                refreshEPs();
            }
        }

        private void btnAddBlockEP_Click(object sender, EventArgs e)
        {
            DomainPolicy d = lbxDomains.SelectedItem as DomainPolicy;
            if (d != null)
            {
                d.BlockEP.Add(new EPPolicy()
                {
                    EpFormat = "/z/enter/ep",
                    Type = AllowEPType.CONTAIN
                });
                refreshEPs();
            }
        }

        private void btnDelBlockEp_Click(object sender, EventArgs e)
        {
            DomainPolicy d = lbxDomains.SelectedItem as DomainPolicy;
            EPPolicy ep = lbxAllowEp.SelectedItem as EPPolicy;
            if (d != null && ep != null)
            {
                d.BlockEP.Remove(ep);
                refreshDomains();
                refreshEPs();
            }
        }
    }
}
