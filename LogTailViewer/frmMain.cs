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

namespace LogTailViewer
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        string[] allowedLogs = {
            @"C:\ProxyServices\Divert_HTTP\block_log.json",
            @"C:\Users\Yoni\Desktop\2020\LogTailViewer\try.txt"
        };

        private void frmMain_Load(object sender, EventArgs e)
        {
            cbPath.Items.AddRange(allowedLogs);
        }

        private void cbPath_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbPath.SelectedIndex > -1)
            {
                current = new FilePositionInfo();
                current.currentFile = cbPath.SelectedItem as string;
                current.currentPosition = -1;
                readLines(-1);
            }
        }

        class FilePositionInfo
        {
            public string currentFile = "";
            public long currentPosition = -1;
            public long firstN = -1;
            public long beforeLastN = -1;
        }

        bool smartMode = true;
        FilePositionInfo current = new FilePositionInfo();

        void readLines(int delta)
        {
            try
            {
                if (current.currentFile == "")
                    return;

                long fileSize = new FileInfo(current.currentFile).Length;
                int blockSize = int.Parse(txtBlockSize.Text) * 1024;

                using (FileStream fs = File.Open(current.currentFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    if (current.currentPosition == -1)
                    {
                        current.currentPosition = Math.Min(Math.Max(0, fileSize  + (delta * blockSize)), fileSize);
                    }
                    else
                    {
                        long nextStartPos = current.currentPosition;
                        if (smartMode)
                        {
                            if (delta > 0 && current.beforeLastN > 0)
                            {
                                // Go forwared, start from lastN
                                nextStartPos += current.beforeLastN;
                            }
                            else if (delta < 0 && current.firstN > 0)
                            {
                                nextStartPos += current.firstN + (delta * blockSize);
                            }
                        }
                        else
                        {
                            nextStartPos = current.currentPosition  + (delta * blockSize);
                        }

                        current.currentPosition = Math.Min(Math.Max(0, nextStartPos), fileSize);
                    }

                    fs.Seek(current.currentPosition, SeekOrigin.Begin);

                    long sizeToRead = Math.Min(blockSize, fileSize - current.currentPosition);
                    byte[] bytes = new byte[sizeToRead];
                    fs.Read(bytes, 0, (int)sizeToRead);

                    for (int i = 0; i < bytes.Length; i++)
                    {
                        if (bytes[i] == '\n')
                        {
                            current.firstN =  i + 1;
                            break;
                        }
                    }

                    for (int i = bytes.Length -1; i > -1; i--)
                    {
                        if (bytes[i] == '\n')
                        {
                            current.beforeLastN =  i ; 
                            break;
                        }
                    }

                    if (sizeToRead > 0)
                    {
                        string result = "";
                        if (smartMode)
                            result = Encoding.UTF8.GetString(bytes, (int)current.firstN , (int)(bytes.Length - current.firstN));
                        else
                            result = Encoding.UTF8.GetString(bytes, 0,bytes.Length );


                        rtbLog.Text = string.Format(
                            "Reading {0} bytes at position {1}. Result:\n\n{2}",
                            smartMode ? (bytes.Length - (int)current.firstN) : bytes.Length,
                            current.currentPosition,
                            result
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                rtbLog.Text = ex.ToString();
            }
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            current.currentPosition = -1;
            readLines(-1);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            readLines(-1);
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            readLines(1);
        }

        private void txtBlockSize_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(txtBlockSize.Text, out _))
                txtBlockSize.Text = (10).ToString();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            current.currentPosition = 0;
            readLines(-1);
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            current.currentPosition = -1;
            readLines(-1);
        }

        private void smartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            smartMode = true;
            smartToolStripMenuItem.Checked = smartMode;
            bufferToolStripMenuItem.Checked = !smartMode;

        }

        private void bufferToolStripMenuItem_Click(object sender, EventArgs e)
        {
            smartMode = false;
            smartToolStripMenuItem.Checked = smartMode;
            bufferToolStripMenuItem.Checked = !smartMode;
        }
    }
}
