using Myntra.Filters;
using Myntra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Myntra.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class MailboxController : Controller
    {
        //
        // GET: /Mailbox/
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Inbox()
        {
            var emailList = new List<AccountEmailsModel>()
            {
                new AccountEmailsModel()
                {
                    AccountEmailId=1,
                    EmailSenderFirstName="Ashok",
                    EmailSenderLastName="Sivapragasam",
                    EmailSubject="This is 1st test mail",
                    SenderEmailAddress="ashok@gmail.com",
                    ReceiverEmailAddresses="ashok@gmail.com,vinoth@gmail.com,prasanna@gmail.com",
                    EmailBody = System.IO.File.ReadAllText(@"D:\Usr\Ashok\HtmlReports_004.html")
                },
                new AccountEmailsModel()
                {
                    AccountEmailId=2,
                    EmailSenderFirstName="Vinoth",
                    EmailSenderLastName="Sivapragasam",
                    EmailSubject="This is 2nd test mail",
                    SenderEmailAddress="vinoth@gmail.com",
                    ReceiverEmailAddresses="ashok@gmail.com,vinoth@gmail.com,prasanna@gmail.com",
                    EmailBody = System.IO.File.ReadAllText(@"D:\Usr\Ashok\HtmlReports_004.html")
                },
                new AccountEmailsModel()
                {
                    AccountEmailId=3,
                    EmailSenderFirstName="Prasanna",
                    EmailSenderLastName="Sivapragasam",
                    EmailSubject="This is 3rd test mail",
                    SenderEmailAddress="prasanna@gmail.com",
                    ReceiverEmailAddresses="ashok@gmail.com,vinoth@gmail.com,prasanna@gmail.com",
                    EmailBody = System.IO.File.ReadAllText(@"D:\Usr\Ashok\HtmlReports_004.html")
                },
                new AccountEmailsModel()
                {
                    AccountEmailId=4,
                    EmailSenderFirstName="Prasanna",
                    EmailSenderLastName="Sivapragasam",
                    EmailSubject="This is 3rd test mail",
                    SenderEmailAddress="prasanna@gmail.com",
                    ReceiverEmailAddresses="ashok@gmail.com,vinoth@gmail.com,prasanna@gmail.com",
                    EmailBody = System.IO.File.ReadAllText(@"D:\Usr\Ashok\HtmlReports_004.html")
                },
                new AccountEmailsModel()
                {
                    AccountEmailId=5,
                    EmailSenderFirstName="Prasanna",
                    EmailSenderLastName="Sivapragasam",
                    EmailSubject="This is 3rd test mail",
                    SenderEmailAddress="prasanna@gmail.com",
                    ReceiverEmailAddresses="ashok@gmail.com,vinoth@gmail.com,prasanna@gmail.com",
                    EmailBody = System.IO.File.ReadAllText(@"D:\Usr\Ashok\HtmlReports_004.html")
                },
                new AccountEmailsModel()
                {
                    AccountEmailId=6,
                    EmailSenderFirstName="Prasanna",
                    EmailSenderLastName="Sivapragasam",
                    EmailSubject="This is 3rd test mail",
                    SenderEmailAddress="prasanna@gmail.com",
                    ReceiverEmailAddresses="ashok@gmail.com,vinoth@gmail.com,prasanna@gmail.com",
                    EmailBody = System.IO.File.ReadAllText(@"D:\Usr\Ashok\HtmlReports_004.html")
                },
                new AccountEmailsModel()
                {
                    AccountEmailId=7,
                    EmailSenderFirstName="Prasanna",
                    EmailSenderLastName="Sivapragasam",
                    EmailSubject="This is 3rd test mail",
                    SenderEmailAddress="prasanna@gmail.com",
                    ReceiverEmailAddresses="ashok@gmail.com,vinoth@gmail.com,prasanna@gmail.com",
                    EmailBody = System.IO.File.ReadAllText(@"D:\Usr\Ashok\HtmlReports_004.html")
                },
                new AccountEmailsModel()
                {
                    AccountEmailId=8,
                    EmailSenderFirstName="Prasanna",
                    EmailSenderLastName="Sivapragasam",
                    EmailSubject="This is 3rd test mail",
                    SenderEmailAddress="prasanna@gmail.com",
                    ReceiverEmailAddresses="ashok@gmail.com,vinoth@gmail.com,prasanna@gmail.com",
                    EmailBody = System.IO.File.ReadAllText(@"D:\Usr\Ashok\HtmlReports_004.html")
                }
            };
            return View(emailList);
        }
    }
}
