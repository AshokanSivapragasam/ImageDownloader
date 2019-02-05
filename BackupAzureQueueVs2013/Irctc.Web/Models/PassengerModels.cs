using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Irctc.Web.Models
{
    [Table("Passenger")]
    public class Passenger
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public string PanNumber { get; set; }

        public string MobileNumber { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }

    [Table("Ticket")]
    public class Ticket
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string SeatNumber { get; set; }

        public string DateOfJourney { get; set; }

        public string DateOfCancelation { get; set; }

        public string BerthPreferred { get; set; }

        public bool IfNotCompromisation { get; set; }

        public string Status { get; set; }

        public bool IsActive { get; set; }
    }

    class PassengerViewModel
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public string PanNumber { get; set; }

        public string MobileNumber { get; set; }

        public string SeatNumber { get; set; }

        public string DateOfJourney { get; set; }

        public string DateOfCancelation { get; set; }

        public string BerthPreferred { get; set; }

        public bool IfNotCompromisation { get; set; }

        public string Status { get; set; }

        public bool IsActive { get; set; }
    }
}