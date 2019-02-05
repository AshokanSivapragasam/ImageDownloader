using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace MsExchangeEmailParser
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var p = new Program();
            var textEmailBody = p.GetTextEmailBodyByEmailSubjectFromExchangeServer(
                microsoftUsername: "v-assiva",
                microsoftPassword: "password",
                microsoftDomainName: "fareast",
                emailAddress: "v-assiva@microsoft.com",
                emailFolderName: "Inbox",
                emailSubject: "Reconciliation");

            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory.TrimEnd(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar + "EmailBodyTextVersion.txt", textEmailBody);
        }

        /// <summary>
        /// Gets text body of the specific email from MS Exchange Server by Email Subject
        /// </summary>
        /// <param name="microsoftUsername"></param>
        /// <param name="microsoftPassword"></param>
        /// <param name="microsoftDomainName"></param>
        /// <param name="emailAddress"></param>
        /// <param name="emailSubject"></param>
        public string GetTextEmailBodyByEmailSubjectFromExchangeServer(string microsoftUsername, string microsoftPassword, string microsoftDomainName, string emailAddress, string emailFolderName, string emailSubject)
        {
            var textEmailBody = string.Empty;
            ExchangeService exchangeService = null;

            try
            {
                exchangeService = new ExchangeService();
                exchangeService.Credentials = new NetworkCredential(microsoftUsername, microsoftPassword, microsoftDomainName);
                exchangeService.AutodiscoverUrl(emailAddress);

                var folderView = new FolderView(1);
                var itemView = new ItemView(1);
                itemView.OrderBy.Add(ItemSchema.DateTimeReceived, SortDirection.Ascending);
                itemView.PropertySet = new PropertySet(BasePropertySet.IdOnly, ItemSchema.Subject, ItemSchema.DateTimeReceived);

                var folders = exchangeService.FindFolders(emailFolderName.ToLower().Equals("inbox") ? WellKnownFolderName.MsgFolderRoot : WellKnownFolderName.Inbox, new SearchFilter.SearchFilterCollection(LogicalOperator.Or, new SearchFilter.IsEqualTo(FolderSchema.DisplayName, emailFolderName)), folderView);
                var folderId = folders.FirstOrDefault().Id;
                var findResults = exchangeService.FindItems(folderId, new SearchFilter.SearchFilterCollection(LogicalOperator.Or, new SearchFilter.ContainsSubstring(ItemSchema.Subject, emailSubject)), itemView);

                foreach (Item item in findResults.Items)
                {
                    var propSet = new PropertySet(BasePropertySet.IdOnly, EmailMessageSchema.Body, ItemSchema.TextBody);
                    var message = EmailMessage.Bind(exchangeService, item.Id, propSet);
                    textEmailBody = message.TextBody.Text;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return textEmailBody;
        }
    }
}
