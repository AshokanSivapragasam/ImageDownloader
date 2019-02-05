using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Irctc.Web.Models
{
    public class IrctcDbContext : DbContext
    {
        public IrctcDbContext()
            : base("IrctcDb")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Train> Trains { get; set; }
        public DbSet<TrainSeat> TrainSeats { get; set; }
        public DbSet<PassengerInformation> PassengerInfo { get; set; }
    }
}