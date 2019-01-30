using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management.Automation;
using System.IO;

namespace RemotePowerShell
{
    public partial class Form1 : Form
    {
        private PowerShellEngine psEngine = new PowerShellEngine();

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonExecute_Click(object sender, EventArgs e)
        {
            textBoxResults.Clear();
            if (radioGetItem.Checked)
            {
                var results = psEngine.ExecuteScript(radioGetItem.Text, null, textBoxRemoteMachine.Text);
                foreach (var result in results)
                {
                    textBoxResults.AppendText(result.ToString() + "\r\n");
                }
            }
            else if (radioGetProcess.Checked)
            {
                var results = psEngine.ExecuteScript(radioGetProcess.Text, null, textBoxRemoteMachine.Text);
                foreach (var result in results)
                {
                    textBoxResults.AppendText(
                        string.Format("{1}({0})\r\n", result.Members["Id"].Value, result.Members["ProcessName"].Value));
                }
            }
            else if (radioGetService.Checked)
            {
                var results = psEngine.ExecuteScript(radioGetService.Text, null, textBoxRemoteMachine.Text);
                foreach (var result in results)
                {
                    textBoxResults.AppendText(result.Members["ServiceName"].Value + "\r\n");
                }
            }
        }
    }
}
