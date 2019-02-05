using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp_RoleClaims_DotNet.Models
{
    public class ToGo
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public string Owner { get; set; }
    }
}