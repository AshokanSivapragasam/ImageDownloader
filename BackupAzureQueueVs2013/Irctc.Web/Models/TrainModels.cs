using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Irctc.Web.Models
{
    [Table("Train")]
    public class Train
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string TrainId { get; set; }
        public string TrainName { get; set; }
        public string FromStation { get; set; }
        public string ToStation { get; set; }
    }

    [Table("TrainSeat")]
    public class TrainSeat
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string SeatNumber { get; set; }
        public string SeatType { get; set; }
        public DateTime TrainScheduledDateTime { get; set; }
        public bool IsReserved { get; set; }
        public string TrainId { get; set; }
    }

    [Table("PassengerInformation")]
    public class PassengerInformation
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string NameOfPassenger { get; set; }
        public string PanNumber { get; set; }
        public bool IsPaymentSuccessful { get; set; }
        public bool IsPaymentReturned { get; set; }
        public bool IsTicketCanceled { get; set; }
        public string SeatNumber { get; set; }
    }

    public enum TrainSeatType
    {
        Berth,
        SemiSleeper
    }
}