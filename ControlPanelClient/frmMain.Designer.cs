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
            this.btnOpenBlockLog = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.btnDeleteLog = new System.Windows.Forms.Button();
            this.grpAdmin = new System.Windows.Forms.GroupBox();
            this.btnTryUnlockAdmin = new System.Windows.Forms.Button();
            this.grpUser = new System.Windows.Forms.GroupBox();
            this.btnEcho = new System.Windows.Forms.Button();
            this.grpAdmin.SuspendLayout();
            this.grpUser.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(10, 35);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Proxy server:";
            // 
            // btnStartProxy
            // 
            this.btnStartProxy.Location = new System.Drawing.Point(150, 28);
            this.btnStartProxy.Margin = new System.Windows.Forms.Padding(5);
            this.btnStartProxy.Name = "btnStartProxy";
            this.btnStartProxy.Size = new System.Drawing.Size(125, 35);
            this.btnStartProxy.TabIndex = 1;
            this.btnStartProxy.Text = "Start";
            this.btnStartProxy.UseVisualStyleBackColor = true;
            // 
            // btnStopProxy
            // 
            this.btnStopProxy.Location = new System.Drawing.Point(307, 28);
            this.btnStopProxy.Margin = new System.Windows.Forms.Padding(5);
            this.btnStopProxy.Name = "btnStopProxy";
            this.btnStopProxy.Size = new System.Drawing.Size(125, 35);
            this.btnStopProxy.TabIndex = 2;
            this.btnStopProxy.Text = "Stop";
            this.btnStopProxy.UseVisualStyleBackColor = true;
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
            this.btnAddWhiteList.Text = "add";
            this.btnAddWhiteList.UseVisualStyleBackColor = true;
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
            this.btnChangePassword.Location = new System.Drawing.Point(442, 118);
            this.btnChangePassword.Margin = new System.Windows.Forms.Padding(5);
            this.btnChangePassword.Name = "btnChangePassword";
            this.btnChangePassword.Size = new System.Drawing.Size(125, 35);
            this.btnChangePassword.TabIndex = 8;
            this.btnChangePassword.Text = "Change";
            this.btnChangePassword.UseVisualStyleBackColor = true;
            // 
            // txtNewPassword
            // 
            this.txtNewPassword.Location = new System.Drawing.Point(18, 122);
            this.txtNewPassword.Margin = new System.Windows.Forms.Padding(5);
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.Size = new System.Drawing.Size(414, 26);
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
            this.dateLockUntil.CustomFormat = "dd/MM/yyyy hh:mm";
            this.dateLockUntil.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateLockUntil.Location = new System.Drawing.Point(18, 188);
            this.dateLockUntil.Margin = new System.Windows.Forms.Padding(5);
            this.dateLockUntil.Name = "dateLockUntil";
            this.dateLockUntil.Size = new System.Drawing.Size(414, 26);
            this.dateLockUntil.TabIndex = 10;
            // 
            // btnLockAdmin
            // 
            this.btnLockAdmin.Location = new System.Drawing.Point(442, 179);
            this.btnLockAdmin.Margin = new System.Windows.Forms.Padding(5);
            this.btnLockAdmin.Name = "btnLockAdmin";
            this.btnLockAdmin.Size = new System.Drawing.Size(125, 35);
            this.btnLockAdmin.TabIndex = 11;
            this.btnLockAdmin.Text = "Lock";
            this.btnLockAdmin.UseVisualStyleBackColor = true;
            this.btnLockAdmin.Click += new System.EventHandler(this.btnLockAdmin_Click);
            // 
            // btnOpenBlockLog
            // 
            this.btnOpenBlockLog.Location = new System.Drawing.Point(150, 72);
            this.btnOpenBlockLog.Margin = new System.Windows.Forms.Padding(5);
            this.btnOpenBlockLog.Name = "btnOpenBlockLog";
            this.btnOpenBlockLog.Size = new System.Drawing.Size(125, 35);
            this.btnOpenBlockLog.TabIndex = 12;
            this.btnOpenBlockLog.Text = "Open";
            this.btnOpenBlockLog.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Blue;
            this.label5.Location = new System.Drawing.Point(13, 80);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 20);
            this.label5.TabIndex = 13;
            this.label5.Text = "Block log:";
            // 
            // btnDeleteLog
            // 
            this.btnDeleteLog.Location = new System.Drawing.Point(307, 72);
            this.btnDeleteLog.Margin = new System.Windows.Forms.Padding(5);
            this.btnDeleteLog.Name = "btnDeleteLog";
            this.btnDeleteLog.Size = new System.Drawing.Size(125, 35);
            this.btnDeleteLog.TabIndex = 14;
            this.btnDeleteLog.Text = "Delete";
            this.btnDeleteLog.UseVisualStyleBackColor = true;
            // 
            // grpAdmin
            // 
            this.grpAdmin.Controls.Add(this.label2);
            this.grpAdmin.Controls.Add(this.txtWhitelist);
            this.grpAdmin.Controls.Add(this.btnAddWhiteList);
            this.grpAdmin.Controls.Add(this.label3);
            this.grpAdmin.Controls.Add(this.btnLockAdmin);
            this.grpAdmin.Controls.Add(this.txtNewPassword);
            this.grpAdmin.Controls.Add(this.dateLockUntil);
            this.grpAdmin.Controls.Add(this.btnChangePassword);
            this.grpAdmin.Controls.Add(this.label4);
            this.grpAdmin.Enabled = false;
            this.grpAdmin.Location = new System.Drawing.Point(20, 214);
            this.grpAdmin.Margin = new System.Windows.Forms.Padding(5);
            this.grpAdmin.Name = "grpAdmin";
            this.grpAdmin.Padding = new System.Windows.Forms.Padding(5);
            this.grpAdmin.Size = new System.Drawing.Size(584, 233);
            this.grpAdmin.TabIndex = 15;
            this.grpAdmin.TabStop = false;
            this.grpAdmin.Text = "Admin actions";
            // 
            // btnTryUnlockAdmin
            // 
            this.btnTryUnlockAdmin.Location = new System.Drawing.Point(20, 165);
            this.btnTryUnlockAdmin.Margin = new System.Windows.Forms.Padding(5);
            this.btnTryUnlockAdmin.Name = "btnTryUnlockAdmin";
            this.btnTryUnlockAdmin.Size = new System.Drawing.Size(187, 35);
            this.btnTryUnlockAdmin.TabIndex = 16;
            this.btnTryUnlockAdmin.Text = "Try unlocking admin";
            this.btnTryUnlockAdmin.UseVisualStyleBackColor = true;
            // 
            // grpUser
            // 
            this.grpUser.Controls.Add(this.label1);
            this.grpUser.Controls.Add(this.btnStartProxy);
            this.grpUser.Controls.Add(this.btnStopProxy);
            this.grpUser.Controls.Add(this.btnDeleteLog);
            this.grpUser.Controls.Add(this.btnOpenBlockLog);
            this.grpUser.Controls.Add(this.label5);
            this.grpUser.Location = new System.Drawing.Point(20, 18);
            this.grpUser.Margin = new System.Windows.Forms.Padding(5);
            this.grpUser.Name = "grpUser";
            this.grpUser.Padding = new System.Windows.Forms.Padding(5);
            this.grpUser.Size = new System.Drawing.Size(454, 128);
            this.grpUser.TabIndex = 17;
            this.grpUser.TabStop = false;
            this.grpUser.Text = "User actions";
            // 
            // btnEcho
            // 
            this.btnEcho.Location = new System.Drawing.Point(217, 165);
            this.btnEcho.Margin = new System.Windows.Forms.Padding(5);
            this.btnEcho.Name = "btnEcho";
            this.btnEcho.Size = new System.Drawing.Size(187, 35);
            this.btnEcho.TabIndex = 18;
            this.btnEcho.Text = "Echo Check";
            this.btnEcho.UseVisualStyleBackColor = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 462);
            this.Controls.Add(this.btnEcho);
            this.Controls.Add(this.grpUser);
            this.Controls.Add(this.btnTryUnlockAdmin);
            this.Controls.Add(this.grpAdmin);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Safe ControlPanel Client";
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
        private System.Windows.Forms.Button btnOpenBlockLog;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnDeleteLog;
        private System.Windows.Forms.GroupBox grpAdmin;
        private System.Windows.Forms.Button btnTryUnlockAdmin;
        private System.Windows.Forms.GroupBox grpUser;
        private System.Windows.Forms.Button btnEcho;
    }
}

