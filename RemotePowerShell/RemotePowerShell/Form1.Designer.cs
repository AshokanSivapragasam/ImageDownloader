namespace RemotePowerShell
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
            this.buttonExecute = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxRemoteMachine = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxResults = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioGetItem = new System.Windows.Forms.RadioButton();
            this.radioGetService = new System.Windows.Forms.RadioButton();
            this.radioGetProcess = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonExecute
            // 
            this.buttonExecute.Location = new System.Drawing.Point(223, 231);
            this.buttonExecute.Name = "buttonExecute";
            this.buttonExecute.Size = new System.Drawing.Size(75, 23);
            this.buttonExecute.TabIndex = 2;
            this.buttonExecute.Text = "Execute";
            this.buttonExecute.UseVisualStyleBackColor = true;
            this.buttonExecute.Click += new System.EventHandler(this.buttonExecute_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Remote Machine:";
            // 
            // textBoxRemoteMachine
            // 
            this.textBoxRemoteMachine.Location = new System.Drawing.Point(136, 6);
            this.textBoxRemoteMachine.Name = "textBoxRemoteMachine";
            this.textBoxRemoteMachine.Size = new System.Drawing.Size(229, 22);
            this.textBoxRemoteMachine.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 264);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Results:";
            // 
            // textBoxResults
            // 
            this.textBoxResults.Location = new System.Drawing.Point(15, 284);
            this.textBoxResults.Multiline = true;
            this.textBoxResults.Name = "textBoxResults";
            this.textBoxResults.ReadOnly = true;
            this.textBoxResults.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxResults.Size = new System.Drawing.Size(537, 245);
            this.textBoxResults.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(371, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(176, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "(Leave blank for localhost)";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioGetProcess);
            this.groupBox1.Controls.Add(this.radioGetService);
            this.groupBox1.Controls.Add(this.radioGetItem);
            this.groupBox1.Location = new System.Drawing.Point(8, 51);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(543, 163);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select a Cmdlet to Execute";
            // 
            // radioGetItem
            // 
            this.radioGetItem.AutoSize = true;
            this.radioGetItem.Checked = true;
            this.radioGetItem.Location = new System.Drawing.Point(26, 30);
            this.radioGetItem.Name = "radioGetItem";
            this.radioGetItem.Size = new System.Drawing.Size(109, 21);
            this.radioGetItem.TabIndex = 0;
            this.radioGetItem.TabStop = true;
            this.radioGetItem.Text = "Get-Item C:\\*";
            this.radioGetItem.UseVisualStyleBackColor = true;
            // 
            // radioGetService
            // 
            this.radioGetService.AutoSize = true;
            this.radioGetService.Location = new System.Drawing.Point(26, 57);
            this.radioGetService.Name = "radioGetService";
            this.radioGetService.Size = new System.Drawing.Size(104, 21);
            this.radioGetService.TabIndex = 1;
            this.radioGetService.Text = "Get-Service";
            this.radioGetService.UseVisualStyleBackColor = true;
            // 
            // radioGetProcess
            // 
            this.radioGetProcess.AutoSize = true;
            this.radioGetProcess.Location = new System.Drawing.Point(26, 84);
            this.radioGetProcess.Name = "radioGetProcess";
            this.radioGetProcess.Size = new System.Drawing.Size(108, 21);
            this.radioGetProcess.TabIndex = 2;
            this.radioGetProcess.Text = "Get-Process";
            this.radioGetProcess.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 569);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxResults);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxRemoteMachine);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonExecute);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonExecute;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxRemoteMachine;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxResults;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioGetService;
        private System.Windows.Forms.RadioButton radioGetItem;
        private System.Windows.Forms.RadioButton radioGetProcess;
    }
}

