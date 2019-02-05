using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TamilYogiWebApi.Models
{
    [Table("Movie")]
    public class Movie
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Caption { get; set; }
        public string Quality { get; set; }
        public string Genre { get; set; }
        public DateTime ReleaseDateTime { get; set; }
        public string ThumbnailUrl { get; set; }
        public ICollection<VideoMirror> VideoMirrors { get; set; }
    }

    [Table("VideoMirror")]
    public class VideoMirror
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int VideoUrl { get; set; }
        public int VideoPosterUrl { get; set; }
        public int VideoUploadedDateTime { get; set; }
        public ICollection<VideoSubtitle> VideoSubtitles { get; set; }
    }

    [Table("VideoSubtitle")]
    public class VideoSubtitle
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string SubtitleUrl { get; set; }
        public string SubtitleLanguage { get; set; }
        public DateTime SubtitleUploadedDateTime { get; set; }
    }
}