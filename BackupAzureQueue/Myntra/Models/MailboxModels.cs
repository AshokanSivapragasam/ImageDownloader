using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Myntra.Models
{
    public class MailboxContext : DbContext
    {
        public MailboxContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<AccountEmails> AccountEmails { get; set; }
    }

    [Table("AccountEmails")]
    public class AccountEmails
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int AccountEmailId { get; set; }
        public string EmailSubject { get; set; }
        public string SenderEmailAddress { get; set; }
        public string EmailSenderFirstName { get; set; }
        public string EmailSenderLastName { get; set; }
        public string ReceiverEmailAddresses { get; set; }
        public string EmailBody { get; set; }
    }

    public class AccountEmailsModel
    {
        [Required]
        [Display(Name = "Email Id")]
        public int AccountEmailId { get; set; }

        [Required]
        [Display(Name = "Email Subject")]
        public string EmailSubject { get; set; }

        [Required]
        [Display(Name = "Sender Email Address")]
        public string SenderEmailAddress { get; set; }

        [Required]
        [Display(Name = "Email Sender First Name")]
        public string EmailSenderFirstName { get; set; }

        [Required]
        [Display(Name = "Email Sender Last Name")]
        public string EmailSenderLastName { get; set; }

        [Required]
        [Display(Name = "Receiver Email Addresses")]
        public string ReceiverEmailAddresses { get; set; }

        [Required]
        [Display(Name = "Receiver Email Addresses")]
        public string EmailBody { get; set; }
    }
}