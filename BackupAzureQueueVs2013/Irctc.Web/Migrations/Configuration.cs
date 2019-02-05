namespace Irctc.Web.Migrations
{
    using Irctc.Web.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Irctc.Web.Models.IrctcDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Irctc.Web.Models.IrctcDbContext context)
        {
            #region SEEDING_PASSENGERS
            context.Passengers.AddOrUpdate(
               i => i.Name,
               new Passenger
               {
                   Name = "Ashok",
                   Age = 25,
                   MobileNumber = "1234567890",
                   PanNumber = "azwpa0128l",
                   Tickets = new List<Ticket> 
                   { 
                       new Ticket{BerthPreferred="Side Lower", DateOfJourney="Tomorrow", DateOfCancelation="Na", IfNotCompromisation=false, IsActive=true, SeatNumber="SL13", Status="Confirmed" }
                   }
               },
               new Passenger
               {
                   Name = "Vinoth",
                   Age = 24,
                   MobileNumber = "1234567890",
                   PanNumber = "azwpa0118l",
                   Tickets = new List<Ticket> 
                   { 
                       new Ticket{BerthPreferred="Side Lower", DateOfJourney="Tomorrow", DateOfCancelation="Na", IfNotCompromisation=false, IsActive=true, SeatNumber="SL14", Status="Confirmed" }
                   }
               },
               new Passenger
               {
                   Name = "Prasanna",
                   Age = 22,
                   MobileNumber = "1234567890",
                   PanNumber = "azwpa0108l",
                   Tickets = new List<Ticket> 
                   { 
                       new Ticket{BerthPreferred="Side Lower", DateOfJourney="Tomorrow", DateOfCancelation="Na", IfNotCompromisation=false, IsActive=true, SeatNumber="SL15", Status="Confirmed" }
                   }
               }

           );
            #endregion

            #region SEEDING_TRAINSEATS
            for (var trainTracker = 0; trainTracker < 5; trainTracker += 1)
            {
                context.Trains.AddOrUpdate(
                    t => t.TrainId,
                    new Train
                    {
                        TrainId = "Tr" + (trainTracker + 1).ToString("000"),
                        TrainName = "Chennai-Hyderabad Express",
                        FromStation = "Chennai",
                        ToStation = "Hyderabad"
                    });
                for (int trainSeatTracker = 1; trainSeatTracker <= 10; trainSeatTracker += 1)
                {
                    context.TrainSeats.AddOrUpdate(
                        ts => ts.SeatNumber,
                        new TrainSeat { SeatNumber = "Seat" + ((trainTracker * 10) + trainSeatTracker).ToString("000"), IsReserved = false, SeatType = TrainSeatType.Berth.ToString(), TrainScheduledDateTime = DateTime.Parse("2015-10-20"), TrainId = "Tr" + (trainTracker + 1).ToString("000") }
                        );
                }
            }
            #endregion
        }
    }
}
