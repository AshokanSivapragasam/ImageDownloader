using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApp02.Models
{
    public class BestRestaurantViewModel
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ReviewerName { get; set; }
        public int BestRating { get; set; }
        public string ReviewBody { get; set; }
    }
}