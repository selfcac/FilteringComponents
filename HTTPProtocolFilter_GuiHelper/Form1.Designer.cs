namespace HTTPProtocolFilter_GuiHelper
{
    partial class Form1
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.loadPolicyJsonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.savePolicyJsonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadLogFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.getBlockedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getAllowedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lbxSimulated = new System.Windows.Forms.ListBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lbxPhrases = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.menuStrip3 = new System.Windows.Forms.MenuStrip();
            this.addPhraseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip4 = new System.Windows.Forms.MenuStrip();
            this.addDomainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addEPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteSelectedToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tvwDomainEp = new System.Windows.Forms.TreeView();
            this.clearPolicyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.menuStrip3.SuspendLayout();
            this.menuStrip4.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearPolicyToolStripMenuItem,
            this.loadPolicyJsonToolStripMenuItem,
            this.savePolicyJsonToolStripMenuItem,
            this.loadLogFileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(840, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // loadPolicyJsonToolStripMenuItem
            // 
            this.loadPolicyJsonToolStripMenuItem.Name = "loadPolicyJsonToolStripMenuItem";
            this.loadPolicyJsonToolStripMenuItem.Size = new System.Drawing.Size(105, 19);
            this.loadPolicyJsonToolStripMenuItem.Text = "Load policy json";
            // 
            // savePolicyJsonToolStripMenuItem
            // 
            this.savePolicyJsonToolStripMenuItem.Name = "savePolicyJsonToolStripMenuItem";
            this.savePolicyJsonToolStripMenuItem.Size = new System.Drawing.Size(104, 19);
            this.savePolicyJsonToolStripMenuItem.Text = "Save policy Json";
            // 
            // loadLogFileToolStripMenuItem
            // 
            this.loadLogFileToolStripMenuItem.Name = "loadLogFileToolStripMenuItem";
            this.loadLogFileToolStripMenuItem.Size = new System.Drawing.Size(84, 19);
            this.loadLogFileToolStripMenuItem.Text = "Load log file";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 25);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(840, 566);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbxSimulated);
            this.groupBox1.Controls.Add(this.menuStrip2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(424, 4);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(412, 558);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "<< Simulator >>";
            // 
            // menuStrip2
            // 
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.getBlockedToolStripMenuItem,
            this.getAllowedToolStripMenuItem});
            this.menuStrip2.Location = new System.Drawing.Point(4, 21);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.menuStrip2.Size = new System.Drawing.Size(404, 25);
            this.menuStrip2.TabIndex = 1;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // getBlockedToolStripMenuItem
            // 
            this.getBlockedToolStripMenuItem.Name = "getBlockedToolStripMenuItem";
            this.getBlockedToolStripMenuItem.Size = new System.Drawing.Size(110, 19);
            this.getBlockedToolStripMenuItem.Text = "Simulate blocked";
            // 
            // getAllowedToolStripMenuItem
            // 
            this.getAllowedToolStripMenuItem.Name = "getAllowedToolStripMenuItem";
            this.getAllowedToolStripMenuItem.Size = new System.Drawing.Size(109, 19);
            this.getAllowedToolStripMenuItem.Text = "Simulate allowed";
            // 
            // lbxSimulated
            // 
            this.lbxSimulated.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxSimulated.FormattingEnabled = true;
            this.lbxSimulated.ItemHeight = 18;
            this.lbxSimulated.Location = new System.Drawing.Point(4, 46);
            this.lbxSimulated.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lbxSimulated.Name = "lbxSimulated";
            this.lbxSimulated.Size = new System.Drawing.Size(404, 508);
            this.lbxSimulated.TabIndex = 2;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(4, 4);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(412, 558);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableLayoutPanel3);
            this.tabPage1.Controls.Add(this.menuStrip4);
            this.tabPage1.Location = new System.Drawing.Point(4, 27);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage1.Size = new System.Drawing.Size(404, 527);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Domains\\Ep";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tableLayoutPanel2);
            this.tabPage2.Controls.Add(this.menuStrip3);
            this.tabPage2.Location = new System.Drawing.Point(4, 27);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage2.Size = new System.Drawing.Size(404, 527);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Phrases";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.lbxPhrases, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.groupBox2, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(4, 28);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(396, 495);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // lbxPhrases
            // 
            this.lbxPhrases.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxPhrases.FormattingEnabled = true;
            this.lbxPhrases.ItemHeight = 18;
            this.lbxPhrases.Location = new System.Drawing.Point(4, 4);
            this.lbxPhrases.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lbxPhrases.Name = "lbxPhrases";
            this.lbxPhrases.Size = new System.Drawing.Size(388, 239);
            this.lbxPhrases.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboBox1);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(4, 251);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(388, 240);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Edit phrase";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 35);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Phrase Text:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 103);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "Phrase Type:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(8, 57);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(372, 24);
            this.textBox1.TabIndex = 2;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "CONTAIN",
            "EXACT-WORD",
            "WORD-CONTAINING",
            "REGEX"});
            this.comboBox1.Location = new System.Drawing.Point(8, 124);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(372, 26);
            this.comboBox1.TabIndex = 3;
            // 
            // menuStrip3
            // 
            this.menuStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addPhraseToolStripMenuItem,
            this.deleteSelectedToolStripMenuItem});
            this.menuStrip3.Location = new System.Drawing.Point(4, 4);
            this.menuStrip3.Name = "menuStrip3";
            this.menuStrip3.Size = new System.Drawing.Size(396, 24);
            this.menuStrip3.TabIndex = 1;
            this.menuStrip3.Text = "menuStrip3";
            // 
            // addPhraseToolStripMenuItem
            // 
            this.addPhraseToolStripMenuItem.Name = "addPhraseToolStripMenuItem";
            this.addPhraseToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.addPhraseToolStripMenuItem.Text = "Add phrase";
            // 
            // deleteSelectedToolStripMenuItem
            // 
            this.deleteSelectedToolStripMenuItem.Name = "deleteSelectedToolStripMenuItem";
            this.deleteSelectedToolStripMenuItem.Size = new System.Drawing.Size(98, 20);
            this.deleteSelectedToolStripMenuItem.Text = "Delete selected";
            // 
            // menuStrip4
            // 
            this.menuStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addDomainToolStripMenuItem,
            this.addEPToolStripMenuItem,
            this.deleteSelectedToolStripMenuItem1});
            this.menuStrip4.Location = new System.Drawing.Point(4, 4);
            this.menuStrip4.Name = "menuStrip4";
            this.menuStrip4.Size = new System.Drawing.Size(396, 24);
            this.menuStrip4.TabIndex = 0;
            this.menuStrip4.Text = "menuStrip4";
            // 
            // addDomainToolStripMenuItem
            // 
            this.addDomainToolStripMenuItem.Name = "addDomainToolStripMenuItem";
            this.addDomainToolStripMenuItem.Size = new System.Drawing.Size(86, 20);
            this.addDomainToolStripMenuItem.Text = "Add Domain";
            // 
            // addEPToolStripMenuItem
            // 
            this.addEPToolStripMenuItem.Name = "addEPToolStripMenuItem";
            this.addEPToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.addEPToolStripMenuItem.Text = "Add EP";
            // 
            // deleteSelectedToolStripMenuItem1
            // 
            this.deleteSelectedToolStripMenuItem1.Name = "deleteSelectedToolStripMenuItem1";
            this.deleteSelectedToolStripMenuItem1.Size = new System.Drawing.Size(98, 20);
            this.deleteSelectedToolStripMenuItem1.Text = "Delete selected";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.tvwDomainEp, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(4, 28);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(396, 495);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // tvwDomainEp
            // 
            this.tvwDomainEp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvwDomainEp.Location = new System.Drawing.Point(3, 3);
            this.tvwDomainEp.Name = "tvwDomainEp";
            this.tvwDomainEp.Size = new System.Drawing.Size(390, 241);
            this.tvwDomainEp.TabIndex = 0;
            // 
            // clearPolicyToolStripMenuItem
            // 
            this.clearPolicyToolStripMenuItem.Name = "clearPolicyToolStripMenuItem";
            this.clearPolicyToolStripMenuItem.Size = new System.Drawing.Size(81, 19);
            this.clearPolicyToolStripMenuItem.Text = "Clear Policy";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 591);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.menuStrip3.ResumeLayout(false);
            this.menuStrip3.PerformLayout();
            this.menuStrip4.ResumeLayout(false);
            this.menuStrip4.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem loadPolicyJsonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem savePolicyJsonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadLogFileToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lbxSimulated;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem getBlockedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getAllowedToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ListBox lbxPhrases;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.MenuStrip menuStrip3;
        private System.Windows.Forms.ToolStripMenuItem addPhraseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteSelectedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearPolicyToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TreeView tvwDomainEp;
        private System.Windows.Forms.MenuStrip menuStrip4;
        private System.Windows.Forms.ToolStripMenuItem addDomainToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addEPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteSelectedToolStripMenuItem1;
    }
}

