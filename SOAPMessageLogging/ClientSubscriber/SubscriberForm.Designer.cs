namespace ClientSubscriber
{
    partial class SubscriberForm
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
            this.btnInvoke = new System.Windows.Forms.Button();
            this.lblText = new System.Windows.Forms.Label();
            this.txtParameterValue = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnInvoke
            // 
            this.btnInvoke.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInvoke.Location = new System.Drawing.Point(65, 55);
            this.btnInvoke.Name = "btnInvoke";
            this.btnInvoke.Size = new System.Drawing.Size(128, 23);
            this.btnInvoke.TabIndex = 0;
            this.btnInvoke.Text = "Invoke Service";
            this.btnInvoke.UseVisualStyleBackColor = true;
            this.btnInvoke.Click += new System.EventHandler(this.btnInvoke_Click);
            // 
            // lblText
            // 
            this.lblText.AutoSize = true;
            this.lblText.Location = new System.Drawing.Point(21, 19);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(85, 13);
            this.lblText.TabIndex = 1;
            this.lblText.Text = "Parameter Value";
            // 
            // txtParameterValue
            // 
            this.txtParameterValue.Location = new System.Drawing.Point(113, 19);
            this.txtParameterValue.Name = "txtParameterValue";
            this.txtParameterValue.Size = new System.Drawing.Size(100, 20);
            this.txtParameterValue.TabIndex = 2;
            // 
            // SubscriberForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(278, 179);
            this.Controls.Add(this.txtParameterValue);
            this.Controls.Add(this.lblText);
            this.Controls.Add(this.btnInvoke);
            this.Name = "SubscriberForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Subscriber Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnInvoke;
        private System.Windows.Forms.Label lblText;
        private System.Windows.Forms.TextBox txtParameterValue;
    }
}

