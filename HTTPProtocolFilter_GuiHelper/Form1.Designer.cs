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
            this.clearPolicyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadPolicyJsonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.savePolicyJsonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadLogFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbxSimulated = new System.Windows.Forms.ListBox();
            this.menuStrip5 = new System.Windows.Forms.MenuStrip();
            this.newDomainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.domainEPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.subdomainsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtSimulatedSelected = new System.Windows.Forms.TextBox();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.getBlockedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getAllowedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.gpEditEp = new System.Windows.Forms.GroupBox();
            this.btnEPApply = new System.Windows.Forms.Button();
            this.cbEpType = new System.Windows.Forms.ComboBox();
            this.txtEpPattern = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.gpEditDomain = new System.Windows.Forms.GroupBox();
            this.btnDApply = new System.Windows.Forms.Button();
            this.cbDomainType = new System.Windows.Forms.ComboBox();
            this.txtDomainPattern = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbxDomains = new System.Windows.Forms.ListBox();
            this.lbxEp = new System.Windows.Forms.ListBox();
            this.menuStrip4 = new System.Windows.Forms.MenuStrip();
            this.addDomainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addEPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteDomainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteEPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lbxPhrases = new System.Windows.Forms.ListBox();
            this.gpEditPhrase = new System.Windows.Forms.GroupBox();
            this.btnPApply = new System.Windows.Forms.Button();
            this.cbPhraseType = new System.Windows.Forms.ComboBox();
            this.txtPhrase = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip3 = new System.Windows.Forms.MenuStrip();
            this.addPhraseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dlgOpen = new System.Windows.Forms.OpenFileDialog();
            this.dlgSave = new System.Windows.Forms.SaveFileDialog();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.lblLogStatus = new System.Windows.Forms.ToolStripMenuItem();
            this.simulateUnkownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.orderedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.orderedGroupedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.menuStrip5.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.gpEditEp.SuspendLayout();
            this.gpEditDomain.SuspendLayout();
            this.menuStrip4.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.gpEditPhrase.SuspendLayout();
            this.menuStrip3.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
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
            this.menuStrip1.Size = new System.Drawing.Size(965, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // clearPolicyToolStripMenuItem
            // 
            this.clearPolicyToolStripMenuItem.Name = "clearPolicyToolStripMenuItem";
            this.clearPolicyToolStripMenuItem.Size = new System.Drawing.Size(81, 19);
            this.clearPolicyToolStripMenuItem.Text = "Clear Policy";
            this.clearPolicyToolStripMenuItem.Click += new System.EventHandler(this.clearPolicyToolStripMenuItem_Click);
            // 
            // loadPolicyJsonToolStripMenuItem
            // 
            this.loadPolicyJsonToolStripMenuItem.Name = "loadPolicyJsonToolStripMenuItem";
            this.loadPolicyJsonToolStripMenuItem.Size = new System.Drawing.Size(105, 19);
            this.loadPolicyJsonToolStripMenuItem.Text = "Load policy json";
            this.loadPolicyJsonToolStripMenuItem.Click += new System.EventHandler(this.loadPolicyJsonToolStripMenuItem_Click);
            // 
            // savePolicyJsonToolStripMenuItem
            // 
            this.savePolicyJsonToolStripMenuItem.Name = "savePolicyJsonToolStripMenuItem";
            this.savePolicyJsonToolStripMenuItem.Size = new System.Drawing.Size(104, 19);
            this.savePolicyJsonToolStripMenuItem.Text = "Save policy Json";
            this.savePolicyJsonToolStripMenuItem.Click += new System.EventHandler(this.savePolicyJsonToolStripMenuItem_Click);
            // 
            // loadLogFileToolStripMenuItem
            // 
            this.loadLogFileToolStripMenuItem.Name = "loadLogFileToolStripMenuItem";
            this.loadLogFileToolStripMenuItem.Size = new System.Drawing.Size(84, 19);
            this.loadLogFileToolStripMenuItem.Text = "Load log file";
            this.loadLogFileToolStripMenuItem.Click += new System.EventHandler(this.loadLogFileToolStripMenuItem_Click);
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
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(965, 709);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbxSimulated);
            this.groupBox1.Controls.Add(this.menuStrip5);
            this.groupBox1.Controls.Add(this.txtSimulatedSelected);
            this.groupBox1.Controls.Add(this.menuStrip2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(486, 4);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(475, 701);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "<< Simulator >>";
            // 
            // lbxSimulated
            // 
            this.lbxSimulated.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxSimulated.FormattingEnabled = true;
            this.lbxSimulated.ItemHeight = 18;
            this.lbxSimulated.Location = new System.Drawing.Point(4, 46);
            this.lbxSimulated.Name = "lbxSimulated";
            this.lbxSimulated.Size = new System.Drawing.Size(467, 603);
            this.lbxSimulated.TabIndex = 7;
            this.lbxSimulated.SelectedIndexChanged += new System.EventHandler(this.lbxSimulated_SelectedIndexChanged);
            // 
            // menuStrip5
            // 
            this.menuStrip5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.menuStrip5.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newDomainToolStripMenuItem,
            this.domainEPToolStripMenuItem,
            this.subdomainsToolStripMenuItem});
            this.menuStrip5.Location = new System.Drawing.Point(4, 649);
            this.menuStrip5.Name = "menuStrip5";
            this.menuStrip5.Size = new System.Drawing.Size(467, 24);
            this.menuStrip5.TabIndex = 6;
            this.menuStrip5.Text = "menuStrip5";
            // 
            // newDomainToolStripMenuItem
            // 
            this.newDomainToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.newDomainToolStripMenuItem.Name = "newDomainToolStripMenuItem";
            this.newDomainToolStripMenuItem.Size = new System.Drawing.Size(88, 20);
            this.newDomainToolStripMenuItem.Text = "New Domain";
            this.newDomainToolStripMenuItem.Click += new System.EventHandler(this.newDomainToolStripMenuItem_Click);
            // 
            // domainEPToolStripMenuItem
            // 
            this.domainEPToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.domainEPToolStripMenuItem.Name = "domainEPToolStripMenuItem";
            this.domainEPToolStripMenuItem.Size = new System.Drawing.Size(90, 20);
            this.domainEPToolStripMenuItem.Text = "Domain && EP";
            this.domainEPToolStripMenuItem.Click += new System.EventHandler(this.domainEPToolStripMenuItem_Click);
            // 
            // subdomainsToolStripMenuItem
            // 
            this.subdomainsToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.subdomainsToolStripMenuItem.Name = "subdomainsToolStripMenuItem";
            this.subdomainsToolStripMenuItem.Size = new System.Drawing.Size(85, 20);
            this.subdomainsToolStripMenuItem.Text = "Subdomains";
            this.subdomainsToolStripMenuItem.Click += new System.EventHandler(this.subdomainsToolStripMenuItem_Click);
            // 
            // txtSimulatedSelected
            // 
            this.txtSimulatedSelected.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtSimulatedSelected.Location = new System.Drawing.Point(4, 673);
            this.txtSimulatedSelected.Name = "txtSimulatedSelected";
            this.txtSimulatedSelected.ReadOnly = true;
            this.txtSimulatedSelected.Size = new System.Drawing.Size(467, 24);
            this.txtSimulatedSelected.TabIndex = 5;
            // 
            // menuStrip2
            // 
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.simulateUnkownToolStripMenuItem,
            this.getBlockedToolStripMenuItem,
            this.getAllowedToolStripMenuItem,
            this.lblLogStatus});
            this.menuStrip2.Location = new System.Drawing.Point(4, 21);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.menuStrip2.Size = new System.Drawing.Size(467, 25);
            this.menuStrip2.TabIndex = 1;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // getBlockedToolStripMenuItem
            // 
            this.getBlockedToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.getBlockedToolStripMenuItem.Name = "getBlockedToolStripMenuItem";
            this.getBlockedToolStripMenuItem.Size = new System.Drawing.Size(110, 19);
            this.getBlockedToolStripMenuItem.Text = "Simulate blocked";
            this.getBlockedToolStripMenuItem.Click += new System.EventHandler(this.getBlockedToolStripMenuItem_Click);
            // 
            // getAllowedToolStripMenuItem
            // 
            this.getAllowedToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.getAllowedToolStripMenuItem.Name = "getAllowedToolStripMenuItem";
            this.getAllowedToolStripMenuItem.Size = new System.Drawing.Size(109, 19);
            this.getAllowedToolStripMenuItem.Text = "Simulate allowed";
            this.getAllowedToolStripMenuItem.Click += new System.EventHandler(this.getAllowedToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(4, 4);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(474, 701);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableLayoutPanel3);
            this.tabPage1.Controls.Add(this.menuStrip4);
            this.tabPage1.Location = new System.Drawing.Point(4, 27);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage1.Size = new System.Drawing.Size(466, 670);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Domains\\Ep";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.panel2, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.lbxDomains, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.lbxEp, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(4, 28);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49.83389F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.94352F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.22259F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(458, 638);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tabControl2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 428);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(452, 207);
            this.panel2.TabIndex = 2;
            // 
            // gpEditEp
            // 
            this.gpEditEp.Controls.Add(this.btnEPApply);
            this.gpEditEp.Controls.Add(this.cbEpType);
            this.gpEditEp.Controls.Add(this.txtEpPattern);
            this.gpEditEp.Controls.Add(this.label5);
            this.gpEditEp.Controls.Add(this.label6);
            this.gpEditEp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpEditEp.Enabled = false;
            this.gpEditEp.Location = new System.Drawing.Point(3, 3);
            this.gpEditEp.Name = "gpEditEp";
            this.gpEditEp.Size = new System.Drawing.Size(414, 193);
            this.gpEditEp.TabIndex = 1;
            this.gpEditEp.TabStop = false;
            this.gpEditEp.Text = "Edit Ep";
            // 
            // btnEPApply
            // 
            this.btnEPApply.Location = new System.Drawing.Point(6, 149);
            this.btnEPApply.Name = "btnEPApply";
            this.btnEPApply.Size = new System.Drawing.Size(75, 40);
            this.btnEPApply.TabIndex = 9;
            this.btnEPApply.Text = "Apply";
            this.btnEPApply.UseVisualStyleBackColor = true;
            this.btnEPApply.Click += new System.EventHandler(this.btnEPApply_Click);
            // 
            // cbEpType
            // 
            this.cbEpType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEpType.FormattingEnabled = true;
            this.cbEpType.Items.AddRange(new object[] {
            "CONTAIN",
            "START-WITH (remember \'/\')",
            "REGEX"});
            this.cbEpType.Location = new System.Drawing.Point(6, 110);
            this.cbEpType.Name = "cbEpType";
            this.cbEpType.Size = new System.Drawing.Size(372, 26);
            this.cbEpType.TabIndex = 7;
            // 
            // txtEpPattern
            // 
            this.txtEpPattern.Location = new System.Drawing.Point(9, 46);
            this.txtEpPattern.Margin = new System.Windows.Forms.Padding(4);
            this.txtEpPattern.Name = "txtEpPattern";
            this.txtEpPattern.Size = new System.Drawing.Size(372, 24);
            this.txtEpPattern.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 89);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 18);
            this.label5.TabIndex = 5;
            this.label5.Text = "Phrase Type:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 24);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 18);
            this.label6.TabIndex = 4;
            this.label6.Text = "Ep Pattern:";
            // 
            // gpEditDomain
            // 
            this.gpEditDomain.Controls.Add(this.btnDApply);
            this.gpEditDomain.Controls.Add(this.cbDomainType);
            this.gpEditDomain.Controls.Add(this.txtDomainPattern);
            this.gpEditDomain.Controls.Add(this.label3);
            this.gpEditDomain.Controls.Add(this.label4);
            this.gpEditDomain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpEditDomain.Enabled = false;
            this.gpEditDomain.Location = new System.Drawing.Point(3, 3);
            this.gpEditDomain.Name = "gpEditDomain";
            this.gpEditDomain.Size = new System.Drawing.Size(414, 193);
            this.gpEditDomain.TabIndex = 0;
            this.gpEditDomain.TabStop = false;
            this.gpEditDomain.Text = "Edit Domain";
            // 
            // btnDApply
            // 
            this.btnDApply.Location = new System.Drawing.Point(6, 147);
            this.btnDApply.Name = "btnDApply";
            this.btnDApply.Size = new System.Drawing.Size(75, 40);
            this.btnDApply.TabIndex = 8;
            this.btnDApply.Text = "Apply";
            this.btnDApply.UseVisualStyleBackColor = true;
            this.btnDApply.Click += new System.EventHandler(this.btnDApply_Click);
            // 
            // cbDomainType
            // 
            this.cbDomainType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDomainType.FormattingEnabled = true;
            this.cbDomainType.Items.AddRange(new object[] {
            "DOMAIN-ONLY",
            "INCLUDE-SUBDOMAIN"});
            this.cbDomainType.Location = new System.Drawing.Point(4, 108);
            this.cbDomainType.Name = "cbDomainType";
            this.cbDomainType.Size = new System.Drawing.Size(372, 26);
            this.cbDomainType.TabIndex = 7;
            // 
            // txtDomainPattern
            // 
            this.txtDomainPattern.Location = new System.Drawing.Point(7, 44);
            this.txtDomainPattern.Margin = new System.Windows.Forms.Padding(4);
            this.txtDomainPattern.Name = "txtDomainPattern";
            this.txtDomainPattern.Size = new System.Drawing.Size(372, 24);
            this.txtDomainPattern.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 87);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(140, 18);
            this.label3.TabIndex = 5;
            this.label3.Text = "Domain match type:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 22);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 18);
            this.label4.TabIndex = 4;
            this.label4.Text = "Domain pattern";
            // 
            // lbxDomains
            // 
            this.lbxDomains.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxDomains.FormattingEnabled = true;
            this.lbxDomains.ItemHeight = 18;
            this.lbxDomains.Location = new System.Drawing.Point(3, 3);
            this.lbxDomains.Name = "lbxDomains";
            this.lbxDomains.Size = new System.Drawing.Size(452, 311);
            this.lbxDomains.Sorted = true;
            this.lbxDomains.TabIndex = 3;
            this.lbxDomains.SelectedIndexChanged += new System.EventHandler(this.lbxDomains_SelectedIndexChanged);
            // 
            // lbxEp
            // 
            this.lbxEp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxEp.FormattingEnabled = true;
            this.lbxEp.ItemHeight = 18;
            this.lbxEp.Location = new System.Drawing.Point(3, 320);
            this.lbxEp.Name = "lbxEp";
            this.lbxEp.Size = new System.Drawing.Size(452, 102);
            this.lbxEp.TabIndex = 4;
            this.lbxEp.SelectedIndexChanged += new System.EventHandler(this.lbxEp_SelectedIndexChanged);
            // 
            // menuStrip4
            // 
            this.menuStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addDomainToolStripMenuItem,
            this.addEPToolStripMenuItem,
            this.deleteDomainToolStripMenuItem,
            this.deleteEPToolStripMenuItem});
            this.menuStrip4.Location = new System.Drawing.Point(4, 4);
            this.menuStrip4.Name = "menuStrip4";
            this.menuStrip4.Size = new System.Drawing.Size(458, 24);
            this.menuStrip4.TabIndex = 0;
            this.menuStrip4.Text = "menuStrip4";
            // 
            // addDomainToolStripMenuItem
            // 
            this.addDomainToolStripMenuItem.Name = "addDomainToolStripMenuItem";
            this.addDomainToolStripMenuItem.Size = new System.Drawing.Size(86, 20);
            this.addDomainToolStripMenuItem.Text = "Add Domain";
            this.addDomainToolStripMenuItem.Click += new System.EventHandler(this.addDomainToolStripMenuItem_Click);
            // 
            // addEPToolStripMenuItem
            // 
            this.addEPToolStripMenuItem.Name = "addEPToolStripMenuItem";
            this.addEPToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.addEPToolStripMenuItem.Text = "Add EP";
            this.addEPToolStripMenuItem.Click += new System.EventHandler(this.addEPToolStripMenuItem_Click);
            // 
            // deleteDomainToolStripMenuItem
            // 
            this.deleteDomainToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.deleteDomainToolStripMenuItem.Name = "deleteDomainToolStripMenuItem";
            this.deleteDomainToolStripMenuItem.Size = new System.Drawing.Size(97, 20);
            this.deleteDomainToolStripMenuItem.Text = "Delete Domain";
            this.deleteDomainToolStripMenuItem.Click += new System.EventHandler(this.deleteDomainToolStripMenuItem_Click);
            // 
            // deleteEPToolStripMenuItem
            // 
            this.deleteEPToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.deleteEPToolStripMenuItem.Name = "deleteEPToolStripMenuItem";
            this.deleteEPToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.deleteEPToolStripMenuItem.Text = "Delete EP";
            this.deleteEPToolStripMenuItem.Click += new System.EventHandler(this.deleteEPToolStripMenuItem_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tableLayoutPanel2);
            this.tabPage2.Controls.Add(this.menuStrip3);
            this.tabPage2.Location = new System.Drawing.Point(4, 27);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage2.Size = new System.Drawing.Size(466, 670);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Phrases";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.lbxPhrases, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.gpEditPhrase, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(4, 28);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(458, 638);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // lbxPhrases
            // 
            this.lbxPhrases.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxPhrases.FormattingEnabled = true;
            this.lbxPhrases.ItemHeight = 18;
            this.lbxPhrases.Location = new System.Drawing.Point(4, 4);
            this.lbxPhrases.Margin = new System.Windows.Forms.Padding(4);
            this.lbxPhrases.Name = "lbxPhrases";
            this.lbxPhrases.Size = new System.Drawing.Size(450, 311);
            this.lbxPhrases.Sorted = true;
            this.lbxPhrases.TabIndex = 0;
            this.lbxPhrases.SelectedIndexChanged += new System.EventHandler(this.lbxPhrases_SelectedIndexChanged);
            // 
            // gpEditPhrase
            // 
            this.gpEditPhrase.Controls.Add(this.btnPApply);
            this.gpEditPhrase.Controls.Add(this.cbPhraseType);
            this.gpEditPhrase.Controls.Add(this.txtPhrase);
            this.gpEditPhrase.Controls.Add(this.label2);
            this.gpEditPhrase.Controls.Add(this.label1);
            this.gpEditPhrase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpEditPhrase.Enabled = false;
            this.gpEditPhrase.Location = new System.Drawing.Point(4, 323);
            this.gpEditPhrase.Margin = new System.Windows.Forms.Padding(4);
            this.gpEditPhrase.Name = "gpEditPhrase";
            this.gpEditPhrase.Padding = new System.Windows.Forms.Padding(4);
            this.gpEditPhrase.Size = new System.Drawing.Size(450, 311);
            this.gpEditPhrase.TabIndex = 1;
            this.gpEditPhrase.TabStop = false;
            this.gpEditPhrase.Text = "Edit phrase";
            // 
            // btnPApply
            // 
            this.btnPApply.Location = new System.Drawing.Point(7, 179);
            this.btnPApply.Name = "btnPApply";
            this.btnPApply.Size = new System.Drawing.Size(75, 40);
            this.btnPApply.TabIndex = 10;
            this.btnPApply.Text = "Apply";
            this.btnPApply.UseVisualStyleBackColor = true;
            this.btnPApply.Click += new System.EventHandler(this.btnPApply_Click);
            // 
            // cbPhraseType
            // 
            this.cbPhraseType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPhraseType.FormattingEnabled = true;
            this.cbPhraseType.Items.AddRange(new object[] {
            "CONTAIN",
            "EXACT-WORD",
            "WORD-CONTAINING",
            "REGEX"});
            this.cbPhraseType.Location = new System.Drawing.Point(8, 124);
            this.cbPhraseType.Name = "cbPhraseType";
            this.cbPhraseType.Size = new System.Drawing.Size(372, 26);
            this.cbPhraseType.TabIndex = 3;
            // 
            // txtPhrase
            // 
            this.txtPhrase.Location = new System.Drawing.Point(8, 57);
            this.txtPhrase.Margin = new System.Windows.Forms.Padding(4);
            this.txtPhrase.Name = "txtPhrase";
            this.txtPhrase.Size = new System.Drawing.Size(372, 24);
            this.txtPhrase.TabIndex = 2;
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
            // menuStrip3
            // 
            this.menuStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addPhraseToolStripMenuItem,
            this.deleteSelectedToolStripMenuItem});
            this.menuStrip3.Location = new System.Drawing.Point(4, 4);
            this.menuStrip3.Name = "menuStrip3";
            this.menuStrip3.Size = new System.Drawing.Size(458, 24);
            this.menuStrip3.TabIndex = 1;
            this.menuStrip3.Text = "menuStrip3";
            // 
            // addPhraseToolStripMenuItem
            // 
            this.addPhraseToolStripMenuItem.Name = "addPhraseToolStripMenuItem";
            this.addPhraseToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.addPhraseToolStripMenuItem.Text = "Add phrase";
            this.addPhraseToolStripMenuItem.Click += new System.EventHandler(this.addPhraseToolStripMenuItem_Click);
            // 
            // deleteSelectedToolStripMenuItem
            // 
            this.deleteSelectedToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.deleteSelectedToolStripMenuItem.Name = "deleteSelectedToolStripMenuItem";
            this.deleteSelectedToolStripMenuItem.Size = new System.Drawing.Size(144, 20);
            this.deleteSelectedToolStripMenuItem.Text = "Delete selected (Phrase)";
            this.deleteSelectedToolStripMenuItem.Click += new System.EventHandler(this.deleteSelectedToolStripMenuItem_Click);
            // 
            // dlgOpen
            // 
            this.dlgOpen.Filter = "JSON\\Text File|*.json;*.txt|Any File|*.*";
            // 
            // dlgSave
            // 
            this.dlgSave.Filter = "JSON\\Text File|*.json;*.txt|Any File|*.*";
            // 
            // tabControl2
            // 
            this.tabControl2.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Multiline = true;
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(452, 207);
            this.tabControl2.TabIndex = 2;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.gpEditDomain);
            this.tabPage3.Location = new System.Drawing.Point(28, 4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(420, 199);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "Domain";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.gpEditEp);
            this.tabPage4.Location = new System.Drawing.Point(28, 4);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(420, 199);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "Ep";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // lblLogStatus
            // 
            this.lblLogStatus.Enabled = false;
            this.lblLogStatus.Name = "lblLogStatus";
            this.lblLogStatus.Size = new System.Drawing.Size(67, 19);
            this.lblLogStatus.Text = "<Status>";
            // 
            // simulateUnkownToolStripMenuItem
            // 
            this.simulateUnkownToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.orderedToolStripMenuItem,
            this.orderedGroupedToolStripMenuItem,
            this.toolStripMenuItem3});
            this.simulateUnkownToolStripMenuItem.Name = "simulateUnkownToolStripMenuItem";
            this.simulateUnkownToolStripMenuItem.Size = new System.Drawing.Size(112, 19);
            this.simulateUnkownToolStripMenuItem.Text = "Simulate Unkown";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(196, 22);
            this.toolStripMenuItem2.Text = "1000";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // orderedToolStripMenuItem
            // 
            this.orderedToolStripMenuItem.Name = "orderedToolStripMenuItem";
            this.orderedToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.orderedToolStripMenuItem.Text = "1000 Ordered";
            this.orderedToolStripMenuItem.Click += new System.EventHandler(this.orderedToolStripMenuItem_Click);
            // 
            // orderedGroupedToolStripMenuItem
            // 
            this.orderedGroupedToolStripMenuItem.Name = "orderedGroupedToolStripMenuItem";
            this.orderedGroupedToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.orderedGroupedToolStripMenuItem.Text = "1000 Ordered & Grouped";
            this.orderedGroupedToolStripMenuItem.Click += new System.EventHandler(this.orderedGroupedToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(196, 22);
            this.toolStripMenuItem3.Text = "2000";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(965, 734);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Policy Editor";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.menuStrip5.ResumeLayout(false);
            this.menuStrip5.PerformLayout();
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.gpEditEp.ResumeLayout(false);
            this.gpEditEp.PerformLayout();
            this.gpEditDomain.ResumeLayout(false);
            this.gpEditDomain.PerformLayout();
            this.menuStrip4.ResumeLayout(false);
            this.menuStrip4.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.gpEditPhrase.ResumeLayout(false);
            this.gpEditPhrase.PerformLayout();
            this.menuStrip3.ResumeLayout(false);
            this.menuStrip3.PerformLayout();
            this.tabControl2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
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
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem getBlockedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getAllowedToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ListBox lbxPhrases;
        private System.Windows.Forms.GroupBox gpEditPhrase;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbPhraseType;
        private System.Windows.Forms.TextBox txtPhrase;
        private System.Windows.Forms.MenuStrip menuStrip3;
        private System.Windows.Forms.ToolStripMenuItem addPhraseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteSelectedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearPolicyToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.MenuStrip menuStrip4;
        private System.Windows.Forms.ToolStripMenuItem addDomainToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addEPToolStripMenuItem;
        private System.Windows.Forms.GroupBox gpEditDomain;
        private System.Windows.Forms.ComboBox cbDomainType;
        private System.Windows.Forms.TextBox txtDomainPattern;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox gpEditEp;
        private System.Windows.Forms.ComboBox cbEpType;
        private System.Windows.Forms.TextBox txtEpPattern;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.OpenFileDialog dlgOpen;
        private System.Windows.Forms.SaveFileDialog dlgSave;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListBox lbxDomains;
        private System.Windows.Forms.ListBox lbxEp;
        private System.Windows.Forms.ToolStripMenuItem deleteDomainToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteEPToolStripMenuItem;
        private System.Windows.Forms.Button btnEPApply;
        private System.Windows.Forms.Button btnDApply;
        private System.Windows.Forms.Button btnPApply;
        private System.Windows.Forms.ListBox lbxSimulated;
        private System.Windows.Forms.MenuStrip menuStrip5;
        private System.Windows.Forms.ToolStripMenuItem newDomainToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem domainEPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem subdomainsToolStripMenuItem;
        private System.Windows.Forms.TextBox txtSimulatedSelected;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ToolStripMenuItem lblLogStatus;
        private System.Windows.Forms.ToolStripMenuItem simulateUnkownToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem orderedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem orderedGroupedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
    }
}

