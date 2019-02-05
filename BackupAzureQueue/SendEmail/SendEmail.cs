using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SendEmail
{
    public partial class frmSendEmail : Form
    {
        public frmSendEmail()
        {
            InitializeComponent();
        }

        #region USER_EXPERIENCE_EVENTS
        private void txtSmtpHost_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtSmtpHost.Text.Equals("Smtp server"))
                txtSmtpHost.Text = string.Empty;
        }

        private void txtSmtpHost_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSmtpHost.Text))
                txtSmtpHost.Text = "Smtp server";
        }

        private void txtSmtpPort_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtSmtpPort.Text.Equals("Smtp port"))
                txtSmtpPort.Text = string.Empty;
        }

        private void txtSmtpPort_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSmtpPort.Text))
                txtSmtpPort.Text = "Smtp port";
        }

        private void txtOperationTimeout_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtOperationTimeout.Text.Equals("Smtp timeout"))
                txtOperationTimeout.Text = string.Empty;
        }

        private void txtOperationTimeout_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtOperationTimeout.Text))
                txtOperationTimeout.Text = "Smtp timeout";
        }

        private void txtSenderAccount_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtSenderAccount.Text.Equals("Sender email account"))
                txtSenderAccount.Text = string.Empty;
        }

        private void txtSenderAccount_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSenderAccount.Text))
                txtSenderAccount.Text = "Sender email account";
        }

        private void txtSenderAccountPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtSenderAccountPassword.Text.Equals("Password"))
                txtSenderAccountPassword.Text = string.Empty;
        }

        private void txtSenderAccountPassword_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSenderAccountPassword.Text))
                txtSenderAccountPassword.Text = "Password";
        }

        private void txtReceiverEmailAddresses_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtReceiverEmailAddresses.Text.Equals("Receiver email addresses"))
                txtReceiverEmailAddresses.Text = string.Empty;
        }

        private void txtReceiverEmailAddresses_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtReceiverEmailAddresses.Text))
                txtReceiverEmailAddresses.Text = "Receiver email addresses";
        }
        #endregion

        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            try
            {
                Emailer.SmtpHost = txtSmtpHost.Text;
                Emailer.SmtpPort = Convert.ToInt32(txtSmtpPort.Text);
                Emailer.IsSslEnabled = chkIsSslEnabled.Checked;
                Emailer.SmtpOperationTimeoutInMilliseconds = Convert.ToInt32(txtOperationTimeout.Text);
                Emailer.SenderEmailAccount = txtSenderAccount.Text;
                Emailer.SenderEmailAccountPassword = txtSenderAccountPassword.Text;
                Emailer.RecipientEmailAccounts = txtReceiverEmailAddresses.Text;
                Emailer.EmailSubject = "Test email";

                Emailer.SendEmail(@"
<html>
<body font>
Hi,<br/>
<br/>
This is the test email to verify the Inbox and SMTP details.<br/>
<br/>
Thanks,<br/>
Email Interchange Team<br/>
</body>
</html>");
                lblStatusOfSendingEmail.ResetText();
                this.Size = this.DefaultSize;
                lblStatusOfSendingEmail.ForeColor = Color.Teal;
                lblStatusOfSendingEmail.Text = "Email sent successfully";
            }
            catch (Exception ex)
            {
                lblStatusOfSendingEmail.ForeColor = Color.IndianRed;
                lblStatusOfSendingEmail.Text = "Exception: " + ex.Message.Replace("\r", " ").Replace("\n", " ");
            }
        }
    }

    public class Emailer
    {
        #region STATIC_VARIABLES
        public static string SmtpHost;
        public static int SmtpPort;
        public static bool IsSslEnabled;
        public static int SmtpOperationTimeoutInMilliseconds;
        public static string SenderEmailAccount;
        public static string SenderEmailAccountPassword;
        public static string RecipientEmailAccounts;
        public static string EmailSubject;
        #endregion

        /// <summary>
        /// It sends the email with specified stmp host or server
        /// </summary>
        /// <param name="EmailBody"></param>
        public static void SendEmail(string EmailBody)
        {
            var smtp = new SmtpClient
            {
                Host = SmtpHost,
                Port = SmtpPort,
                EnableSsl = IsSslEnabled,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new System.Net.NetworkCredential(SenderEmailAccount, SenderEmailAccountPassword),
                Timeout = SmtpOperationTimeoutInMilliseconds,
            };

            var message = new MailMessage(SenderEmailAccount, RecipientEmailAccounts, EmailSubject, EmailBody)
            {
                IsBodyHtml = true,
                Priority = MailPriority.High
            };
            smtp.Send(message);
        }
    }
}
