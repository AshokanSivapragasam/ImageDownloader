using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TamilYogiWebApi.Models
{
    public class TamilYogiDbContext : DbContext
    {
        public TamilYogiDbContext()
            : base("TamilYogiDb")
        {

        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<VideoMirror> VideoMirrors { get; set; }
        public DbSet<VideoSubtitle> VideoSubtitles { get; set; }
    }
}