namespace LogTailViewer
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cbPath = new System.Windows.Forms.ComboBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.blockSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtBlockSize = new System.Windows.Forms.ToolStripTextBox();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.modeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.smartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bufferToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.cbPath, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.menuStrip1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.rtbLog, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 670);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // cbPath
            // 
            this.cbPath.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbPath.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPath.FormattingEnabled = true;
            this.cbPath.Location = new System.Drawing.Point(3, 3);
            this.cbPath.Name = "cbPath";
            this.cbPath.Size = new System.Drawing.Size(794, 21);
            this.cbPath.TabIndex = 0;
            this.cbPath.SelectedIndexChanged += new System.EventHandler(this.cbPath_SelectedIndexChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem,
            this.toolStripMenuItem2,
            this.toolStripMenuItem1,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4,
            this.blockSizeToolStripMenuItem,
            this.txtBlockSize,
            this.modeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 643);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 27);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(66, 23);
            this.refreshToolStripMenuItem.Text = "[Refresh]";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(35, 23);
            this.toolStripMenuItem2.Text = "<<";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(27, 23);
            this.toolStripMenuItem1.Text = "<";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(27, 23);
            this.toolStripMenuItem3.Text = ">";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(35, 23);
            this.toolStripMenuItem4.Text = ">>";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // blockSizeToolStripMenuItem
            // 
            this.blockSizeToolStripMenuItem.Enabled = false;
            this.blockSizeToolStripMenuItem.Name = "blockSizeToolStripMenuItem";
            this.blockSizeToolStripMenuItem.Size = new System.Drawing.Size(95, 23);
            this.blockSizeToolStripMenuItem.Text = "Block size (KB)";
            // 
            // txtBlockSize
            // 
            this.txtBlockSize.Name = "txtBlockSize";
            this.txtBlockSize.Size = new System.Drawing.Size(100, 23);
            this.txtBlockSize.Text = "10";
            this.txtBlockSize.TextChanged += new System.EventHandler(this.txtBlockSize_TextChanged);
            // 
            // rtbLog
            // 
            this.rtbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbLog.Location = new System.Drawing.Point(3, 30);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.Size = new System.Drawing.Size(794, 610);
            this.rtbLog.TabIndex = 2;
            this.rtbLog.Text = "";
            // 
            // modeToolStripMenuItem
            // 
            this.modeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smartToolStripMenuItem,
            this.bufferToolStripMenuItem});
            this.modeToolStripMenuItem.Name = "modeToolStripMenuItem";
            this.modeToolStripMenuItem.Size = new System.Drawing.Size(50, 23);
            this.modeToolStripMenuItem.Text = "Mode";
            // 
            // smartToolStripMenuItem
            // 
            this.smartToolStripMenuItem.Checked = true;
            this.smartToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.smartToolStripMenuItem.Name = "smartToolStripMenuItem";
            this.smartToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.smartToolStripMenuItem.Text = "Line Smart";
            this.smartToolStripMenuItem.Click += new System.EventHandler(this.smartToolStripMenuItem_Click);
            // 
            // bufferToolStripMenuItem
            // 
            this.bufferToolStripMenuItem.Name = "bufferToolStripMenuItem";
            this.bufferToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.bufferToolStripMenuItem.Text = "Buffer";
            this.bufferToolStripMenuItem.Click += new System.EventHandler(this.bufferToolStripMenuItem_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 670);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Text = "Log tail viewer";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox cbPath;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem blockSizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox txtBlockSize;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem modeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem smartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bufferToolStripMenuItem;
    }
}

