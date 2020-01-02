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
            this.label3 = new System.Windows.Forms.Label();
            this.btnChangePassword = new System.Windows.Forms.Button();
            this.txtNewPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dateLockUntil = new System.Windows.Forms.DateTimePicker();
            this.btnLockAdmin = new System.Windows.Forms.Button();
            this.grpAdmin = new System.Windows.Forms.GroupBox();
            this.btnAdminRun = new System.Windows.Forms.Button();
            this.btnRandom = new System.Windows.Forms.Button();
            this.cbAdminCMD = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnResetUSB = new System.Windows.Forms.Button();
            this.grpUser = new System.Windows.Forms.GroupBox();
            this.btnAllowedRun = new System.Windows.Forms.Button();
            this.cbAllowedCMD = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnEcho = new System.Windows.Forms.Button();
            this.btnIsLocked = new System.Windows.Forms.Button();
            this.dlgUserReset = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnResetUnlock = new System.Windows.Forms.Button();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.grpAdmin.SuspendLayout();
            this.grpUser.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(9, 85);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(174, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "New admin password:";
            // 
            // btnChangePassword
            // 
            this.btnChangePassword.Location = new System.Drawing.Point(303, 106);
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
            this.txtNewPassword.Location = new System.Drawing.Point(14, 110);
            this.txtNewPassword.Margin = new System.Windows.Forms.Padding(5);
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.Size = new System.Drawing.Size(279, 26);
            this.txtNewPassword.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(10, 195);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(298, 20);
            this.label4.TabIndex = 9;
            this.label4.Text = "Lock until (remember to change pass):";
            // 
            // dateLockUntil
            // 
            this.dateLockUntil.CustomFormat = "dd/MM/yyyy hh:mm tt";
            this.dateLockUntil.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateLockUntil.Location = new System.Drawing.Point(14, 220);
            this.dateLockUntil.Margin = new System.Windows.Forms.Padding(5);
            this.dateLockUntil.Name = "dateLockUntil";
            this.dateLockUntil.Size = new System.Drawing.Size(414, 26);
            this.dateLockUntil.TabIndex = 10;
            // 
            // btnLockAdmin
            // 
            this.btnLockAdmin.Location = new System.Drawing.Point(438, 218);
            this.btnLockAdmin.Margin = new System.Windows.Forms.Padding(5);
            this.btnLockAdmin.Name = "btnLockAdmin";
            this.btnLockAdmin.Size = new System.Drawing.Size(125, 35);
            this.btnLockAdmin.TabIndex = 11;
            this.btnLockAdmin.Text = "Lock";
            this.btnLockAdmin.UseVisualStyleBackColor = true;
            this.btnLockAdmin.Click += new System.EventHandler(this.btnLockAdmin_Click);
            // 
            // grpAdmin
            // 
            this.grpAdmin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.grpAdmin.Controls.Add(this.btnAdminRun);
            this.grpAdmin.Controls.Add(this.btnRandom);
            this.grpAdmin.Controls.Add(this.cbAdminCMD);
            this.grpAdmin.Controls.Add(this.label2);
            this.grpAdmin.Controls.Add(this.label3);
            this.grpAdmin.Controls.Add(this.btnLockAdmin);
            this.grpAdmin.Controls.Add(this.txtNewPassword);
            this.grpAdmin.Controls.Add(this.dateLockUntil);
            this.grpAdmin.Controls.Add(this.btnChangePassword);
            this.grpAdmin.Controls.Add(this.label4);
            this.grpAdmin.Location = new System.Drawing.Point(20, 267);
            this.grpAdmin.Margin = new System.Windows.Forms.Padding(5);
            this.grpAdmin.Name = "grpAdmin";
            this.grpAdmin.Padding = new System.Windows.Forms.Padding(5);
            this.grpAdmin.Size = new System.Drawing.Size(584, 261);
            this.grpAdmin.TabIndex = 15;
            this.grpAdmin.TabStop = false;
            this.grpAdmin.Text = "Time locked actions";
            // 
            // btnAdminRun
            // 
            this.btnAdminRun.Location = new System.Drawing.Point(433, 50);
            this.btnAdminRun.Margin = new System.Windows.Forms.Padding(5);
            this.btnAdminRun.Name = "btnAdminRun";
            this.btnAdminRun.Size = new System.Drawing.Size(125, 35);
            this.btnAdminRun.TabIndex = 21;
            this.btnAdminRun.Text = "Run!";
            this.btnAdminRun.UseVisualStyleBackColor = true;
            this.btnAdminRun.Click += new System.EventHandler(this.btnAdminRun_Click);
            // 
            // btnRandom
            // 
            this.btnRandom.Location = new System.Drawing.Point(14, 146);
            this.btnRandom.Margin = new System.Windows.Forms.Padding(5);
            this.btnRandom.Name = "btnRandom";
            this.btnRandom.Size = new System.Drawing.Size(414, 35);
            this.btnRandom.TabIndex = 12;
            this.btnRandom.Text = "Change pass to Random (20)";
            this.btnRandom.UseVisualStyleBackColor = true;
            this.btnRandom.Click += new System.EventHandler(this.btnRandom_Click);
            // 
            // cbAdminCMD
            // 
            this.cbAdminCMD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAdminCMD.FormattingEnabled = true;
            this.cbAdminCMD.Location = new System.Drawing.Point(8, 54);
            this.cbAdminCMD.Name = "cbAdminCMD";
            this.cbAdminCMD.Size = new System.Drawing.Size(415, 28);
            this.cbAdminCMD.TabIndex = 23;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(10, 31);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(181, 20);
            this.label2.TabIndex = 22;
            this.label2.Text = "Restricted Commands:";
            // 
            // btnResetUSB
            // 
            this.btnResetUSB.Location = new System.Drawing.Point(46, 27);
            this.btnResetUSB.Margin = new System.Windows.Forms.Padding(5);
            this.btnResetUSB.Name = "btnResetUSB";
            this.btnResetUSB.Size = new System.Drawing.Size(247, 35);
            this.btnResetUSB.TabIndex = 13;
            this.btnResetUSB.Text = "Make admin pass 1234";
            this.btnResetUSB.UseVisualStyleBackColor = true;
            this.btnResetUSB.Click += new System.EventHandler(this.btnResetUSB_Click);
            // 
            // grpUser
            // 
            this.grpUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grpUser.Controls.Add(this.btnClearLog);
            this.grpUser.Controls.Add(this.btnAllowedRun);
            this.grpUser.Controls.Add(this.cbAllowedCMD);
            this.grpUser.Controls.Add(this.label1);
            this.grpUser.Controls.Add(this.btnIsLocked);
            this.grpUser.Controls.Add(this.btnEcho);
            this.grpUser.Location = new System.Drawing.Point(20, 18);
            this.grpUser.Margin = new System.Windows.Forms.Padding(5);
            this.grpUser.Name = "grpUser";
            this.grpUser.Padding = new System.Windows.Forms.Padding(5);
            this.grpUser.Size = new System.Drawing.Size(584, 148);
            this.grpUser.TabIndex = 17;
            this.grpUser.TabStop = false;
            this.grpUser.Text = "Unlocked actions";
            // 
            // btnAllowedRun
            // 
            this.btnAllowedRun.Location = new System.Drawing.Point(433, 96);
            this.btnAllowedRun.Margin = new System.Windows.Forms.Padding(5);
            this.btnAllowedRun.Name = "btnAllowedRun";
            this.btnAllowedRun.Size = new System.Drawing.Size(125, 35);
            this.btnAllowedRun.TabIndex = 13;
            this.btnAllowedRun.Text = "Run!";
            this.btnAllowedRun.UseVisualStyleBackColor = true;
            this.btnAllowedRun.Click += new System.EventHandler(this.btnAllowedRun_Click);
            // 
            // cbAllowedCMD
            // 
            this.cbAllowedCMD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAllowedCMD.FormattingEnabled = true;
            this.cbAllowedCMD.Location = new System.Drawing.Point(8, 100);
            this.cbAllowedCMD.Name = "cbAllowedCMD";
            this.cbAllowedCMD.Size = new System.Drawing.Size(415, 28);
            this.cbAllowedCMD.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(10, 77);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(162, 20);
            this.label1.TabIndex = 13;
            this.label1.Text = "Allowed Commands:";
            // 
            // btnEcho
            // 
            this.btnEcho.Location = new System.Drawing.Point(8, 29);
            this.btnEcho.Margin = new System.Windows.Forms.Padding(5);
            this.btnEcho.Name = "btnEcho";
            this.btnEcho.Size = new System.Drawing.Size(138, 35);
            this.btnEcho.TabIndex = 18;
            this.btnEcho.Text = "🗣 Echo Check";
            this.btnEcho.UseVisualStyleBackColor = true;
            this.btnEcho.Click += new System.EventHandler(this.btnEcho_Click);
            // 
            // btnIsLocked
            // 
            this.btnIsLocked.Location = new System.Drawing.Point(156, 29);
            this.btnIsLocked.Margin = new System.Windows.Forms.Padding(5);
            this.btnIsLocked.Name = "btnIsLocked";
            this.btnIsLocked.Size = new System.Drawing.Size(161, 35);
            this.btnIsLocked.TabIndex = 12;
            this.btnIsLocked.Text = "⏳ Is Unlocked?";
            this.btnIsLocked.UseVisualStyleBackColor = true;
            this.btnIsLocked.Click += new System.EventHandler(this.btnIsLocked_Click);
            // 
            // dlgUserReset
            // 
            this.dlgUserReset.Filter = "Password File (.psw)|*.psw";
            this.dlgUserReset.Title = "Open admin password file";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.groupBox1.Controls.Add(this.btnResetUnlock);
            this.groupBox1.Controls.Add(this.btnResetUSB);
            this.groupBox1.Location = new System.Drawing.Point(21, 174);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(583, 85);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "USB Actions";
            // 
            // btnResetUnlock
            // 
            this.btnResetUnlock.Location = new System.Drawing.Point(303, 27);
            this.btnResetUnlock.Margin = new System.Windows.Forms.Padding(5);
            this.btnResetUnlock.Name = "btnResetUnlock";
            this.btnResetUnlock.Size = new System.Drawing.Size(255, 35);
            this.btnResetUnlock.TabIndex = 14;
            this.btnResetUnlock.Text = "Reset unlock date";
            this.btnResetUnlock.UseVisualStyleBackColor = true;
            this.btnResetUnlock.Click += new System.EventHandler(this.btnResetUnlock_Click);
            // 
            // rtbLog
            // 
            this.rtbLog.Location = new System.Drawing.Point(21, 537);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.ReadOnly = true;
            this.rtbLog.Size = new System.Drawing.Size(583, 119);
            this.rtbLog.TabIndex = 19;
            this.rtbLog.Text = "";
            // 
            // btnClearLog
            // 
            this.btnClearLog.Location = new System.Drawing.Point(433, 29);
            this.btnClearLog.Margin = new System.Windows.Forms.Padding(5);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(125, 35);
            this.btnClearLog.TabIndex = 21;
            this.btnClearLog.Text = "Clear log";
            this.btnClearLog.UseVisualStyleBackColor = true;
            this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 668);
            this.Controls.Add(this.rtbLog);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpUser);
            this.Controls.Add(this.grpAdmin);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Safe ControlPanel Client v2";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.grpAdmin.ResumeLayout(false);
            this.grpAdmin.PerformLayout();
            this.grpUser.ResumeLayout(false);
            this.grpUser.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnChangePassword;
        private System.Windows.Forms.TextBox txtNewPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dateLockUntil;
        private System.Windows.Forms.Button btnLockAdmin;
        private System.Windows.Forms.GroupBox grpAdmin;
        private System.Windows.Forms.GroupBox grpUser;
        private System.Windows.Forms.Button btnEcho;
        private System.Windows.Forms.Button btnIsLocked;
        private System.Windows.Forms.Button btnRandom;
        private System.Windows.Forms.Button btnResetUSB;
        private System.Windows.Forms.Button btnAllowedRun;
        private System.Windows.Forms.ComboBox cbAllowedCMD;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAdminRun;
        private System.Windows.Forms.ComboBox cbAdminCMD;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.OpenFileDialog dlgUserReset;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnResetUnlock;
        private System.Windows.Forms.Button btnClearLog;
        private System.Windows.Forms.RichTextBox rtbLog;
    }
}

