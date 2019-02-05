namespace ControlPanelClient
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnStartProxy = new System.Windows.Forms.Button();
            this.btnStopProxy = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtWhitelist = new System.Windows.Forms.TextBox();
            this.btnAddWhiteList = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnChangePassword = new System.Windows.Forms.Button();
            this.txtNewPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dateLockUntil = new System.Windows.Forms.DateTimePicker();
            this.btnLockAdmin = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.btnDeleteLog = new System.Windows.Forms.Button();
            this.grpAdmin = new System.Windows.Forms.GroupBox();
            this.grpUser = new System.Windows.Forms.GroupBox();
            this.cbUrlBlock = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnEcho = new System.Windows.Forms.Button();
            this.btnStartFirewall = new System.Windows.Forms.Button();
            this.btnStopFireWall = new System.Windows.Forms.Button();
            this.btnIsLocked = new System.Windows.Forms.Button();
            this.btnRandom = new System.Windows.Forms.Button();
            this.grpAdmin.SuspendLayout();
            this.grpUser.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(8, 115);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Proxy server:";
            // 
            // btnStartProxy
            // 
            this.btnStartProxy.Location = new System.Drawing.Point(148, 108);
            this.btnStartProxy.Margin = new System.Windows.Forms.Padding(5);
            this.btnStartProxy.Name = "btnStartProxy";
            this.btnStartProxy.Size = new System.Drawing.Size(125, 35);
            this.btnStartProxy.TabIndex = 1;
            this.btnStartProxy.Text = "Start";
            this.btnStartProxy.UseVisualStyleBackColor = true;
            this.btnStartProxy.Click += new System.EventHandler(this.btnStartProxy_Click);
            // 
            // btnStopProxy
            // 
            this.btnStopProxy.Location = new System.Drawing.Point(305, 108);
            this.btnStopProxy.Margin = new System.Windows.Forms.Padding(5);
            this.btnStopProxy.Name = "btnStopProxy";
            this.btnStopProxy.Size = new System.Drawing.Size(125, 35);
            this.btnStopProxy.TabIndex = 2;
            this.btnStopProxy.Text = "Stop";
            this.btnStopProxy.UseVisualStyleBackColor = true;
            this.btnStopProxy.Click += new System.EventHandler(this.btnStopProxy_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(13, 41);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Add to whitelist:";
            // 
            // txtWhitelist
            // 
            this.txtWhitelist.Location = new System.Drawing.Point(18, 66);
            this.txtWhitelist.Margin = new System.Windows.Forms.Padding(5);
            this.txtWhitelist.Name = "txtWhitelist";
            this.txtWhitelist.Size = new System.Drawing.Size(414, 26);
            this.txtWhitelist.TabIndex = 4;
            // 
            // btnAddWhiteList
            // 
            this.btnAddWhiteList.Location = new System.Drawing.Point(442, 62);
            this.btnAddWhiteList.Margin = new System.Windows.Forms.Padding(5);
            this.btnAddWhiteList.Name = "btnAddWhiteList";
            this.btnAddWhiteList.Size = new System.Drawing.Size(125, 35);
            this.btnAddWhiteList.TabIndex = 5;
            this.btnAddWhiteList.Text = "Add";
            this.btnAddWhiteList.UseVisualStyleBackColor = true;
            this.btnAddWhiteList.Click += new System.EventHandler(this.btnAddWhiteList_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(13, 97);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(174, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "New admin password:";
            // 
            // btnChangePassword
            // 
            this.btnChangePassword.Location = new System.Drawing.Point(307, 118);
            this.btnChangePassword.Margin = new System.Windows.Forms.Padding(5);
            this.btnChangePassword.Name = "btnChangePassword";
            this.btnChangePassword.Size = new System.Drawing.Size(125, 35);
            this.btnChangePassword.TabIndex = 8;
            this.btnChangePassword.Text = "Change";
            this.btnChangePassword.UseVisualStyleBackColor = true;
            this.btnChangePassword.Click += new System.EventHandler(this.btnChangePassword_Click);
            // 
            // txtNewPassword
            // 
            this.txtNewPassword.Location = new System.Drawing.Point(18, 122);
            this.txtNewPassword.Margin = new System.Windows.Forms.Padding(5);
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.Size = new System.Drawing.Size(279, 26);
            this.txtNewPassword.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(14, 163);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 20);
            this.label4.TabIndex = 9;
            this.label4.Text = "Lock until:";
            // 
            // dateLockUntil
            // 
            this.dateLockUntil.CustomFormat = "dd/MM/yyyy hh:mm tt";
            this.dateLockUntil.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateLockUntil.Location = new System.Drawing.Point(18, 188);
            this.dateLockUntil.Margin = new System.Windows.Forms.Padding(5);
            this.dateLockUntil.Name = "dateLockUntil";
            this.dateLockUntil.Size = new System.Drawing.Size(414, 26);
            this.dateLockUntil.TabIndex = 10;
            // 
            // btnLockAdmin
            // 
            this.btnLockAdmin.Location = new System.Drawing.Point(442, 186);
            this.btnLockAdmin.Margin = new System.Windows.Forms.Padding(5);
            this.btnLockAdmin.Name = "btnLockAdmin";
            this.btnLockAdmin.Size = new System.Drawing.Size(125, 35);
            this.btnLockAdmin.TabIndex = 11;
            this.btnLockAdmin.Text = "Lock";
            this.btnLockAdmin.UseVisualStyleBackColor = true;
            this.btnLockAdmin.Click += new System.EventHandler(this.btnLockAdmin_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Blue;
            this.label5.Location = new System.Drawing.Point(11, 160);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 20);
            this.label5.TabIndex = 13;
            this.label5.Text = "Block log:";
            // 
            // btnDeleteLog
            // 
            this.btnDeleteLog.Location = new System.Drawing.Point(449, 153);
            this.btnDeleteLog.Margin = new System.Windows.Forms.Padding(5);
            this.btnDeleteLog.Name = "btnDeleteLog";
            this.btnDeleteLog.Size = new System.Drawing.Size(125, 35);
            this.btnDeleteLog.TabIndex = 14;
            this.btnDeleteLog.Text = "Open";
            this.btnDeleteLog.UseVisualStyleBackColor = true;
            this.btnDeleteLog.Click += new System.EventHandler(this.btnDeleteLog_Click);
            // 
            // grpAdmin
            // 
            this.grpAdmin.Controls.Add(this.btnRandom);
            this.grpAdmin.Controls.Add(this.label2);
            this.grpAdmin.Controls.Add(this.txtWhitelist);
            this.grpAdmin.Controls.Add(this.btnAddWhiteList);
            this.grpAdmin.Controls.Add(this.label3);
            this.grpAdmin.Controls.Add(this.btnLockAdmin);
            this.grpAdmin.Controls.Add(this.txtNewPassword);
            this.grpAdmin.Controls.Add(this.dateLockUntil);
            this.grpAdmin.Controls.Add(this.btnChangePassword);
            this.grpAdmin.Controls.Add(this.label4);
            this.grpAdmin.Location = new System.Drawing.Point(20, 317);
            this.grpAdmin.Margin = new System.Windows.Forms.Padding(5);
            this.grpAdmin.Name = "grpAdmin";
            this.grpAdmin.Padding = new System.Windows.Forms.Padding(5);
            this.grpAdmin.Size = new System.Drawing.Size(584, 232);
            this.grpAdmin.TabIndex = 15;
            this.grpAdmin.TabStop = false;
            this.grpAdmin.Text = "Time locked actions";
            // 
            // grpUser
            // 
            this.grpUser.Controls.Add(this.cbUrlBlock);
            this.grpUser.Controls.Add(this.textBox1);
            this.grpUser.Controls.Add(this.label7);
            this.grpUser.Controls.Add(this.label6);
            this.grpUser.Controls.Add(this.btnEcho);
            this.grpUser.Controls.Add(this.label1);
            this.grpUser.Controls.Add(this.btnStartFirewall);
            this.grpUser.Controls.Add(this.btnStopFireWall);
            this.grpUser.Controls.Add(this.btnStartProxy);
            this.grpUser.Controls.Add(this.btnStopProxy);
            this.grpUser.Controls.Add(this.btnDeleteLog);
            this.grpUser.Controls.Add(this.label5);
            this.grpUser.Location = new System.Drawing.Point(20, 18);
            this.grpUser.Margin = new System.Windows.Forms.Padding(5);
            this.grpUser.Name = "grpUser";
            this.grpUser.Padding = new System.Windows.Forms.Padding(5);
            this.grpUser.Size = new System.Drawing.Size(584, 244);
            this.grpUser.TabIndex = 17;
            this.grpUser.TabStop = false;
            this.grpUser.Text = "Unlocked actions";
            // 
            // cbUrlBlock
            // 
            this.cbUrlBlock.FormattingEnabled = true;
            this.cbUrlBlock.Location = new System.Drawing.Point(148, 157);
            this.cbUrlBlock.Name = "cbUrlBlock";
            this.cbUrlBlock.Size = new System.Drawing.Size(284, 28);
            this.cbUrlBlock.TabIndex = 20;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(8, 74);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(422, 26);
            this.textBox1.TabIndex = 19;
            this.textBox1.Text = "netsh advfirewall show allprofiles";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Blue;
            this.label7.Location = new System.Drawing.Point(11, 204);
            this.label7.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 20);
            this.label7.TabIndex = 19;
            this.label7.Text = "Echo:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Blue;
            this.label6.Location = new System.Drawing.Point(8, 38);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 20);
            this.label6.TabIndex = 0;
            this.label6.Text = "Firewall:";
            // 
            // btnEcho
            // 
            this.btnEcho.Location = new System.Drawing.Point(148, 197);
            this.btnEcho.Margin = new System.Windows.Forms.Padding(5);
            this.btnEcho.Name = "btnEcho";
            this.btnEcho.Size = new System.Drawing.Size(282, 35);
            this.btnEcho.TabIndex = 18;
            this.btnEcho.Text = "Echo Check";
            this.btnEcho.UseVisualStyleBackColor = true;
            this.btnEcho.Click += new System.EventHandler(this.btnEcho_Click);
            // 
            // btnStartFirewall
            // 
            this.btnStartFirewall.Location = new System.Drawing.Point(148, 31);
            this.btnStartFirewall.Margin = new System.Windows.Forms.Padding(5);
            this.btnStartFirewall.Name = "btnStartFirewall";
            this.btnStartFirewall.Size = new System.Drawing.Size(125, 35);
            this.btnStartFirewall.TabIndex = 1;
            this.btnStartFirewall.Text = "Start";
            this.btnStartFirewall.UseVisualStyleBackColor = true;
            this.btnStartFirewall.Click += new System.EventHandler(this.btnStartFirewall_Click);
            // 
            // btnStopFireWall
            // 
            this.btnStopFireWall.Location = new System.Drawing.Point(305, 31);
            this.btnStopFireWall.Margin = new System.Windows.Forms.Padding(5);
            this.btnStopFireWall.Name = "btnStopFireWall";
            this.btnStopFireWall.Size = new System.Drawing.Size(125, 35);
            this.btnStopFireWall.TabIndex = 2;
            this.btnStopFireWall.Text = "Stop";
            this.btnStopFireWall.UseVisualStyleBackColor = true;
            this.btnStopFireWall.Click += new System.EventHandler(this.btnStopFireWall_Click);
            // 
            // btnIsLocked
            // 
            this.btnIsLocked.Location = new System.Drawing.Point(20, 272);
            this.btnIsLocked.Margin = new System.Windows.Forms.Padding(5);
            this.btnIsLocked.Name = "btnIsLocked";
            this.btnIsLocked.Size = new System.Drawing.Size(125, 35);
            this.btnIsLocked.TabIndex = 12;
            this.btnIsLocked.Text = "Is Unlocked?";
            this.btnIsLocked.UseVisualStyleBackColor = true;
            this.btnIsLocked.Click += new System.EventHandler(this.btnIsLocked_Click);
            // 
            // btnRandom
            // 
            this.btnRandom.Location = new System.Drawing.Point(442, 118);
            this.btnRandom.Margin = new System.Windows.Forms.Padding(5);
            this.btnRandom.Name = "btnRandom";
            this.btnRandom.Size = new System.Drawing.Size(125, 35);
            this.btnRandom.TabIndex = 12;
            this.btnRandom.Text = "Random (20)";
            this.btnRandom.UseVisualStyleBackColor = true;
            this.btnRandom.Click += new System.EventHandler(this.btnRandom_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 567);
            this.Controls.Add(this.btnIsLocked);
            this.Controls.Add(this.grpUser);
            this.Controls.Add(this.grpAdmin);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Safe ControlPanel Client";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.grpAdmin.ResumeLayout(false);
            this.grpAdmin.PerformLayout();
            this.grpUser.ResumeLayout(false);
            this.grpUser.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnStartProxy;
        private System.Windows.Forms.Button btnStopProxy;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtWhitelist;
        private System.Windows.Forms.Button btnAddWhiteList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnChangePassword;
        private System.Windows.Forms.TextBox txtNewPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dateLockUntil;
        private System.Windows.Forms.Button btnLockAdmin;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnDeleteLog;
        private System.Windows.Forms.GroupBox grpAdmin;
        private System.Windows.Forms.GroupBox grpUser;
        private System.Windows.Forms.Button btnEcho;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnStartFirewall;
        private System.Windows.Forms.Button btnStopFireWall;
        private System.Windows.Forms.Button btnIsLocked;
        private System.Windows.Forms.ComboBox cbUrlBlock;
        private System.Windows.Forms.Button btnRandom;
    }
}

