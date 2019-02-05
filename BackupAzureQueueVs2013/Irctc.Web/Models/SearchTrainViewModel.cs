using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Irctc.Web.Models
{
    public class SearchTrainViewModel
    {
        public string FromStation { get; set; }
        public string ToStation { get; set; }
        public DateTime DateOfJourney { get; set; }
    }
}