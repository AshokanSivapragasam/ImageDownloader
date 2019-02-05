namespace MvcApp01.Migrations
{
    using MvcApp01.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MvcApp01.Models.RestaurantDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(MvcApp01.Models.RestaurantDb context)
        {
            //  This method will be called after migrating to the latest version.
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.

            context.Restaurants.AddOrUpdate(
              r => r.Name,
              new Restaurant
              {
                  Name = "A2B",
                  City = "Cuddalore",
                  Country = "India",
                  Reviews =
                    new List<RestaurantReview> 
                    { 
                        new RestaurantReview { Rating = 10, ReviewBody = "Wah~", ReviewerName = "Appa"},
                        new RestaurantReview { Rating = 9, ReviewBody = "Wah~", ReviewerName = "Amma"},
                        new RestaurantReview { Rating = 8, ReviewBody = "Wah~", ReviewerName = "Ashok"}, 
                        new RestaurantReview { Rating = 7, ReviewBody = "Wah~", ReviewerName = "Vinoth"},
                        new RestaurantReview { Rating = 6, ReviewBody = "Wah~", ReviewerName = "Prasanna"}
                    }
              },
              new Restaurant { Name = "McDonalds", City = "Hyderabad", Country = "India", Reviews = new List<RestaurantReview> { new RestaurantReview { Rating = 9, ReviewBody = "Alright", ReviewerName = "Ashok" } } },
              new Restaurant { Name = "PizzaHut", City = "Chennai", Country = "India", Reviews = new List<RestaurantReview> { new RestaurantReview { Rating = 8, ReviewBody = "Not bad", ReviewerName = "Ashok" } } },
              new Restaurant { Name = "ChefsWorld", City = "Hyderabad", Country = "India", Reviews = new List<RestaurantReview> { new RestaurantReview { Rating = 7, ReviewBody = "Not bad", ReviewerName = "Prasanna" } } }
            );

            for (int idx = 0; idx < 1000; idx++)
            {
                context.Restaurants.AddOrUpdate(
                  r => r.Name,
                  new Restaurant
                  {
                      Name = "Restaurant" + idx.ToString("0000"),
                      City = "Nowhere",
                      Country = "India",
                      Reviews =
                        new List<RestaurantReview> 
                        { 
                            new RestaurantReview { Rating = 6, ReviewBody = "ReviewBody"+idx.ToString("0000"), ReviewerName = "ReviewerName"+idx.ToString("0000")}
                        }
                  });
            }
        }
    }
}
