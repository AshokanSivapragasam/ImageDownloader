using Microsoft.AspNet.Identity;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace AspNetIdentity.WebApi.Services
{
    public class EmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            await SendEmailAsync(message, "token" as object);
        }

        public async Task SendEmailAsync(IdentityMessage message, object userToken)
        {
            System.Net.Mail.MailAddress from = new System.Net.Mail.MailAddress(ConfigurationManager.AppSettings["emailService:Account"]);
            string senderPassword = ConfigurationManager.AppSettings["emailService:Password"];

            try
            {
                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtphost.redmond.corp.microsoft.com",//"internal.smtp.mscom.phx.gbl",//"smtphost.redmond.corp.microsoft.com",//"HKXPRD3002.prod.outlook.com",
                    Port = 25,
                    EnableSsl = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new System.Net.NetworkCredential(from.Address, senderPassword),
                    Timeout = 30000,
                };

                MailAddress to = new System.Net.Mail.MailAddress(message.Destination);
                MailMessage msg = new MailMessage(from, to)
                {
                    IsBodyHtml = true,
                    Priority = MailPriority.High,
                    Subject = message.Subject,
                    Body = message.Body
                };
                
                smtp.SendAsync(msg, userToken);
            }
            catch
            {
                await Task.FromResult(0);
            }
        }

        // Use NuGet to install SendGrid (Basic C# client lib) 
        private async Task configSendGridasync(IdentityMessage message)
        {
            var myMessage = new SendGridMessage();
            
            myMessage.AddTo(message.Destination);
            myMessage.From = new System.Net.Mail.MailAddress("v-assiva@microsoft.com", "Ashokan");
            myMessage.Subject = message.Subject;
            myMessage.Text = message.Body;
            myMessage.Html = message.Body;

            var credentials = new NetworkCredential(ConfigurationManager.AppSettings["emailService:Account"], 
                                                    ConfigurationManager.AppSettings["emailService:Password"]);

            // Create a Web transport for sending email.
            var transportWeb = new Web(credentials);

            // Send the email.
            if (transportWeb != null)
            {
                await transportWeb.DeliverAsync(myMessage);
            }
            else
            {
                //Trace.TraceError("Failed to create Web transport.");
                await Task.FromResult(0);
            }
        }
    }
}