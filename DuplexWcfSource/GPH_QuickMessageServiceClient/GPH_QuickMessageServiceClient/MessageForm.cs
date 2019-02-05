using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.ServiceModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
// Location of the proxy.
using GPH_QuickMessageServiceClient.ServiceReference1;

namespace GPH_QuickMessageServiceClient
{
    // Specify for the callback to NOT use the current synchronization context
    [CallbackBehavior(
        ConcurrencyMode = ConcurrencyMode.Single,
        UseSynchronizationContext = false)]
    public partial class MessageForm : Form, ServiceReference1.GPH_QuickMessageServiceCallback
    {
        private SynchronizationContext _uiSyncContext = null;
        private ServiceReference1.GPH_QuickMessageServiceClient _GPH_QuickMessageService = null;
        public MessageForm()
        {
            InitializeComponent();
        }

        private void MessageForm_Load(object sender, EventArgs e)
        {
            // Capture the UI synchronization context
            _uiSyncContext = SynchronizationContext.Current;

            // The client callback interface must be hosted for the server to invoke the callback
            // Open a connection to the message service via the proxy (qualifier ServiceReference1 needed due to name clash)
            _GPH_QuickMessageService = new ServiceReference1.GPH_QuickMessageServiceClient(new InstanceContext(this), "WSDualHttpBinding_GPH_QuickMessageService1");
            _GPH_QuickMessageService.Open();

            // Initialize the fields / buttons
            this.btnJoin.Enabled = false;
            this.btnSend.Enabled = false;
            this.btnLeave.Enabled = false;
            this.btnExit.Enabled = true;
            this.txtMessageOutbound.Enabled = false;

            // Initial eventhandlers
            this.txtName.TextChanged += new EventHandler(txtName_TextChanged);
            this.FormClosing += new FormClosingEventHandler(MessageForm_FormClosing);

        }

        private void MessageForm_FormClosing(object sender, FormClosingEventArgs e)
        {
           //Terminate the connection to the service.
            _GPH_QuickMessageService.Close();

        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (this.txtName.Text != String.Empty)
            {
                this.btnJoin.Enabled = true;
            }

        }
        private void btnJoin_Click(object sender, EventArgs e)
        {
            //contact the service.
            _GPH_QuickMessageService.JoinTheConversation(this.txtName.Text);

            // change the button states
            this.btnJoin.Enabled = false;
            this.btnSend.Enabled = true;
            this.btnLeave.Enabled = true;
            this.txtMessageOutbound.Enabled = true;
        }

        private void btnLeave_Click(object sender, EventArgs e)
        {
            // Let the service know that this user is leaving
            _GPH_QuickMessageService.LeaveTheConversation(this.txtName.Text);

            // Update the button/field states
            this.btnJoin.Enabled = true;
            this.btnSend.Enabled = false;
            this.btnLeave.Enabled = false;
            this.txtMessageOutbound.Enabled = false;

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            // Forward the message to the service
            _GPH_QuickMessageService.ReceiveMessage(this.txtName.Text, null, this.txtMessageOutbound.Text);

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            // Leave the conversation and close the form    
            _GPH_QuickMessageService.LeaveTheConversation(this.txtName.Text);
            this.Close();

        }
        private void WriteMessage(string message)
        {
            string format = this.txtMessageLog.Text.Length > 0 ? "{0}\r\n{1} {2}" : "{0}{1} {2}";
            this.txtMessageLog.Text = String.Format(format, this.txtMessageLog.Text, DateTime.Now.ToShortTimeString(), message);
            this.txtMessageLog.SelectionStart = this.txtMessageLog.Text.Length - 1;
            this.txtMessageLog.ScrollToCaret();
        }

        #region GPH_QuickMessageServiceCallback Methods

        public void NotifyUserJoinedTheConversation(string arg_Name)
        {
            // The UI thread won't be handling the callback, but it is the only one allowed to update the controls.  
            // So, we will dispatch the UI update back to the UI sync context.
            SendOrPostCallback callback =
                delegate(object state)
                {
                    string msg_user = state.ToString();
                    msg_user = msg_user.ToUpper();
                    this.WriteMessage(String.Format("[{0}] has joined the conversation.", msg_user));
                };

            _uiSyncContext.Post(callback, arg_Name);
        }
        public void NotifyUserOfMessage(string arg_Name, string arg_Message)
        {
            // The UI thread won't be handling the callback, but it is the only one allowed to update the controls.  
            // So, we will dispatch the UI update back to the UI sync context.
            SendOrPostCallback callback =
                delegate(object state)
                {
                    this.WriteMessage(String.Format("[{0}]: {1}", arg_Name.ToUpper(), arg_Message));
                };

            _uiSyncContext.Post(callback, arg_Name);
        }

        public void NotifyUserLeftTheConversation(string arg_Name)
        {
            // The UI thread won't be handling the callback, but it is the only one allowed to update the controls.  
            // So, we will dispatch the UI update back to the UI sync context.
            SendOrPostCallback callback =
                delegate(object state)
                {
                    string msg_user = state.ToString();
                    msg_user = msg_user.ToUpper();
                    this.WriteMessage(String.Format("[{0}] has left the conversation.", msg_user));
                };

            _uiSyncContext.Post(callback, arg_Name);
        }
        #endregion  
    }
}
