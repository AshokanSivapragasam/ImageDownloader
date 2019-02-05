using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcApp02.Models
{
    public class Restaurant
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [ScaffoldColumn(true)]
        public string Name { get; set; }

        [Required]
        [StringLength(10)]
        [ScaffoldColumn(true)]
        public string City { get; set; }

        [Required]
        [StringLength(10)]
        [ScaffoldColumn(true)]
        public string Country { get; set; }

        public virtual ICollection<RestaurantReview> Reviews { get; set; }
    }
}