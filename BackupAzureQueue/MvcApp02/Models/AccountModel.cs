using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcApp02.Models
{
    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [StringLength(10)]
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class LogonViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class RegisterViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}