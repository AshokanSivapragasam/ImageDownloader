namespace SendEmail
{
    partial class frmSendEmail
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
            this.txtSmtpHost = new System.Windows.Forms.TextBox();
            this.txtSmtpPort = new System.Windows.Forms.TextBox();
            this.txtOperationTimeout = new System.Windows.Forms.TextBox();
            this.chkIsSslEnabled = new System.Windows.Forms.CheckBox();
            this.txtSenderAccount = new System.Windows.Forms.TextBox();
            this.txtSenderAccountPassword = new System.Windows.Forms.TextBox();
            this.txtReceiverEmailAddresses = new System.Windows.Forms.TextBox();
            this.btnSendEmail = new System.Windows.Forms.Button();
            this.lblStatusOfSendingEmail = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtSmtpHost
            // 
            this.txtSmtpHost.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSmtpHost.ForeColor = System.Drawing.SystemColors.Highlight;
            this.txtSmtpHost.Location = new System.Drawing.Point(12, 13);
            this.txtSmtpHost.Name = "txtSmtpHost";
            this.txtSmtpHost.Size = new System.Drawing.Size(260, 22);
            this.txtSmtpHost.TabIndex = 0;
            this.txtSmtpHost.Text = "Smtp server";
            this.txtSmtpHost.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSmtpHost_KeyDown);
            this.txtSmtpHost.Leave += new System.EventHandler(this.txtSmtpHost_Leave);
            // 
            // txtSmtpPort
            // 
            this.txtSmtpPort.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSmtpPort.ForeColor = System.Drawing.SystemColors.Highlight;
            this.txtSmtpPort.Location = new System.Drawing.Point(12, 51);
            this.txtSmtpPort.Name = "txtSmtpPort";
            this.txtSmtpPort.Size = new System.Drawing.Size(76, 22);
            this.txtSmtpPort.TabIndex = 1;
            this.txtSmtpPort.Text = "Smtp port";
            this.txtSmtpPort.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSmtpPort_KeyDown);
            this.txtSmtpPort.Leave += new System.EventHandler(this.txtSmtpPort_Leave);
            // 
            // txtOperationTimeout
            // 
            this.txtOperationTimeout.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOperationTimeout.ForeColor = System.Drawing.SystemColors.Highlight;
            this.txtOperationTimeout.Location = new System.Drawing.Point(187, 51);
            this.txtOperationTimeout.Name = "txtOperationTimeout";
            this.txtOperationTimeout.Size = new System.Drawing.Size(85, 22);
            this.txtOperationTimeout.TabIndex = 2;
            this.txtOperationTimeout.Text = "Smtp timeout";
            this.txtOperationTimeout.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOperationTimeout_KeyDown);
            this.txtOperationTimeout.Leave += new System.EventHandler(this.txtOperationTimeout_Leave);
            // 
            // chkIsSslEnabled
            // 
            this.chkIsSslEnabled.AutoSize = true;
            this.chkIsSslEnabled.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIsSslEnabled.Location = new System.Drawing.Point(94, 53);
            this.chkIsSslEnabled.Name = "chkIsSslEnabled";
            this.chkIsSslEnabled.Size = new System.Drawing.Size(90, 17);
            this.chkIsSslEnabled.TabIndex = 3;
            this.chkIsSslEnabled.Text = "IsSslEnabled";
            this.chkIsSslEnabled.UseVisualStyleBackColor = true;
            // 
            // txtSenderAccount
            // 
            this.txtSenderAccount.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSenderAccount.ForeColor = System.Drawing.SystemColors.Highlight;
            this.txtSenderAccount.Location = new System.Drawing.Point(12, 89);
            this.txtSenderAccount.Name = "txtSenderAccount";
            this.txtSenderAccount.Size = new System.Drawing.Size(260, 22);
            this.txtSenderAccount.TabIndex = 4;
            this.txtSenderAccount.Text = "Sender email account";
            this.txtSenderAccount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSenderAccount_KeyDown);
            this.txtSenderAccount.Leave += new System.EventHandler(this.txtSenderAccount_Leave);
            // 
            // txtSenderAccountPassword
            // 
            this.txtSenderAccountPassword.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSenderAccountPassword.ForeColor = System.Drawing.SystemColors.Highlight;
            this.txtSenderAccountPassword.Location = new System.Drawing.Point(12, 133);
            this.txtSenderAccountPassword.Name = "txtSenderAccountPassword";
            this.txtSenderAccountPassword.PasswordChar = '*';
            this.txtSenderAccountPassword.Size = new System.Drawing.Size(260, 22);
            this.txtSenderAccountPassword.TabIndex = 5;
            this.txtSenderAccountPassword.Text = "Password";
            this.txtSenderAccountPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSenderAccountPassword_KeyDown);
            this.txtSenderAccountPassword.Leave += new System.EventHandler(this.txtSenderAccountPassword_Leave);
            // 
            // txtReceiverEmailAddresses
            // 
            this.txtReceiverEmailAddresses.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReceiverEmailAddresses.ForeColor = System.Drawing.SystemColors.Highlight;
            this.txtReceiverEmailAddresses.Location = new System.Drawing.Point(12, 176);
            this.txtReceiverEmailAddresses.Multiline = true;
            this.txtReceiverEmailAddresses.Name = "txtReceiverEmailAddresses";
            this.txtReceiverEmailAddresses.Size = new System.Drawing.Size(169, 57);
            this.txtReceiverEmailAddresses.TabIndex = 6;
            this.txtReceiverEmailAddresses.Text = "Receiver email addresses";
            this.txtReceiverEmailAddresses.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtReceiverEmailAddresses_KeyDown);
            this.txtReceiverEmailAddresses.Leave += new System.EventHandler(this.txtReceiverEmailAddresses_Leave);
            // 
            // btnSendEmail
            // 
            this.btnSendEmail.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSendEmail.Location = new System.Drawing.Point(187, 176);
            this.btnSendEmail.Name = "btnSendEmail";
            this.btnSendEmail.Size = new System.Drawing.Size(85, 57);
            this.btnSendEmail.TabIndex = 7;
            this.btnSendEmail.Text = "Send Email";
            this.btnSendEmail.UseVisualStyleBackColor = true;
            this.btnSendEmail.Click += new System.EventHandler(this.btnSendEmail_Click);
            // 
            // lblStatusOfSendingEmail
            // 
            this.lblStatusOfSendingEmail.AutoSize = true;
            this.lblStatusOfSendingEmail.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusOfSendingEmail.Location = new System.Drawing.Point(12, 240);
            this.lblStatusOfSendingEmail.MaximumSize = new System.Drawing.Size(270, 0);
            this.lblStatusOfSendingEmail.Name = "lblStatusOfSendingEmail";
            this.lblStatusOfSendingEmail.Size = new System.Drawing.Size(0, 15);
            this.lblStatusOfSendingEmail.TabIndex = 8;
            // 
            // frmSendEmail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.lblStatusOfSendingEmail);
            this.Controls.Add(this.btnSendEmail);
            this.Controls.Add(this.txtReceiverEmailAddresses);
            this.Controls.Add(this.txtSenderAccountPassword);
            this.Controls.Add(this.txtSenderAccount);
            this.Controls.Add(this.chkIsSslEnabled);
            this.Controls.Add(this.txtOperationTimeout);
            this.Controls.Add(this.txtSmtpPort);
            this.Controls.Add(this.txtSmtpHost);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmSendEmail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Email Account Verifier";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSmtpHost;
        private System.Windows.Forms.TextBox txtSmtpPort;
        private System.Windows.Forms.TextBox txtOperationTimeout;
        private System.Windows.Forms.CheckBox chkIsSslEnabled;
        private System.Windows.Forms.TextBox txtSenderAccount;
        private System.Windows.Forms.TextBox txtSenderAccountPassword;
        private System.Windows.Forms.TextBox txtReceiverEmailAddresses;
        private System.Windows.Forms.Button btnSendEmail;
        private System.Windows.Forms.Label lblStatusOfSendingEmail;
    }
}

