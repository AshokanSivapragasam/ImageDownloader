namespace GPH_QuickMessageServiceClient
{
    partial class MessageForm
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
            this.lblName = new System.Windows.Forms.Label();
            this.lblRecieveMessage = new System.Windows.Forms.Label();
            this.lblSendMessage = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtMessageOutbound = new System.Windows.Forms.TextBox();
            this.btnJoin = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtMessageLog = new System.Windows.Forms.TextBox();
            this.btnLeave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(47, 49);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(38, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Name:";
            // 
            // lblRecieveMessage
            // 
            this.lblRecieveMessage.AutoSize = true;
            this.lblRecieveMessage.Location = new System.Drawing.Point(47, 150);
            this.lblRecieveMessage.Name = "lblRecieveMessage";
            this.lblRecieveMessage.Size = new System.Drawing.Size(93, 13);
            this.lblRecieveMessage.TabIndex = 2;
            this.lblRecieveMessage.Text = "Recieve Message";
            // 
            // lblSendMessage
            // 
            this.lblSendMessage.AutoSize = true;
            this.lblSendMessage.Location = new System.Drawing.Point(47, 79);
            this.lblSendMessage.Name = "lblSendMessage";
            this.lblSendMessage.Size = new System.Drawing.Size(78, 13);
            this.lblSendMessage.TabIndex = 3;
            this.lblSendMessage.Text = "Send Message";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(91, 49);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(104, 20);
            this.txtName.TabIndex = 4;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // txtMessageOutbound
            // 
            this.txtMessageOutbound.Location = new System.Drawing.Point(50, 107);
            this.txtMessageOutbound.Name = "txtMessageOutbound";
            this.txtMessageOutbound.Size = new System.Drawing.Size(401, 20);
            this.txtMessageOutbound.TabIndex = 5;
            // 
            // btnJoin
            // 
            this.btnJoin.Location = new System.Drawing.Point(376, 39);
            this.btnJoin.Name = "btnJoin";
            this.btnJoin.Size = new System.Drawing.Size(75, 23);
            this.btnJoin.TabIndex = 6;
            this.btnJoin.Text = "&Join";
            this.btnJoin.UseVisualStyleBackColor = true;
            this.btnJoin.Click += new System.EventHandler(this.btnJoin_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(468, 343);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 7;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(468, 107);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 8;
            this.btnSend.Text = "&Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtMessageLog
            // 
            this.txtMessageLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.txtMessageLog.Location = new System.Drawing.Point(50, 166);
            this.txtMessageLog.Multiline = true;
            this.txtMessageLog.Name = "txtMessageLog";
            this.txtMessageLog.ReadOnly = true;
            this.txtMessageLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMessageLog.Size = new System.Drawing.Size(401, 200);
            this.txtMessageLog.TabIndex = 9;
            // 
            // btnLeave
            // 
            this.btnLeave.Location = new System.Drawing.Point(468, 39);
            this.btnLeave.Name = "btnLeave";
            this.btnLeave.Size = new System.Drawing.Size(75, 23);
            this.btnLeave.TabIndex = 10;
            this.btnLeave.Text = "&Leave";
            this.btnLeave.UseVisualStyleBackColor = true;
            this.btnLeave.Click += new System.EventHandler(this.btnLeave_Click);
            // 
            // MessageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 385);
            this.Controls.Add(this.btnLeave);
            this.Controls.Add(this.txtMessageLog);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnJoin);
            this.Controls.Add(this.txtMessageOutbound);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblSendMessage);
            this.Controls.Add(this.lblRecieveMessage);
            this.Controls.Add(this.lblName);
            this.Name = "MessageForm";
            this.Text = "The GPH Quick Message Service Client Interface";
            this.Load += new System.EventHandler(this.MessageForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MessageForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblRecieveMessage;
        private System.Windows.Forms.Label lblSendMessage;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtMessageOutbound;
        private System.Windows.Forms.Button btnJoin;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtMessageLog;
        private System.Windows.Forms.Button btnLeave;
    }
}

