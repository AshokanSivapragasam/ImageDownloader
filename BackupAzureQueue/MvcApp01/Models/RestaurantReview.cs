using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MvcApp01.Models
{
    public class RestaurantReview
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required]
        [ScaffoldColumn(true)]
        public string ReviewerName { get; set; }

        [Required]
        [Range(0, 10)]
        public int Rating { get; set; }

        [StringLength(400)]
        public string ReviewBody { get; set; }

        [ScaffoldColumn(true)]
        public int RestaurantId { get; set; }
    }
}