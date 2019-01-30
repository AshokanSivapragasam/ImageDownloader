using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace ClientSubscriber
{
    public partial class SubscriberForm : Form
    {
        public SubscriberForm()
        {
            InitializeComponent();
        }

        private void btnInvoke_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtParameterValue.Text.Trim()))
                {
                    MessageBox.Show("Please set some value for parameter.");
                    txtParameterValue.Focus();
                }
                else
                {
                    Web_HelloWorld.HelloWorldService service = new ClientSubscriber.Web_HelloWorld.HelloWorldService();
                    MessageBox.Show(service.HelloWorld(txtParameterValue.Text.Trim()));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
