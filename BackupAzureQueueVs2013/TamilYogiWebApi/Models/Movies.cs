using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TamilYogiWebApi.Models
{
    public class TamilYogiDb : DbContext
    {
        public TamilYogiDb()
            : base("TamilYogiDbConnection")
        {

        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<VideoMirror> VideoMirrors { get; set; }
        public DbSet<VideoSubtitle> VideoSubtitles { get; set; }

        public System.Data.Entity.DbSet<TamilYogiWebApi.Models.VideoComment> VideoComments { get; set; }
    }

    [Table("Movie")]
    public class Movie
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Caption { get; set; }

        public string Tags { get; set; }

        public string Genre { get; set; }

        public string Quality { get; set; }

        public string AgeRestrictions { get; set; }

        public string Certificate { get; set; }

        public string Language { get; set; }

        public string Country { get; set; }

        public string ThumbnailUrl { get; set; }

        public DateTime ReleasedDateTime { get; set; }

        public virtual ICollection<VideoMirror> Mirrors { get; set; }
    }

    [Table("VideoMirror")]
    public class VideoMirror
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string VideoUrl { get; set; }

        public string VideoMimeType { get; set; }

        public string VideoPosterUrl { get; set; }

        public DateTime CopyOnServerDateTime { get; set; }

        [ForeignKey("MovieProperty")]
        public int MovieRefId { get; set; }

        public Movie MovieProperty { get; set; }

        public virtual ICollection<VideoSubtitle> VideoSubtitles { get; set; }

        public virtual ICollection<VideoComment> VideoComments { get; set; }
    }

    [Table("VideoSubtitle")]
    public class VideoSubtitle
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Language { get; set; }

        public string SubtitleUrl { get; set; }

        public string SubtitleUploadedDate { get; set; }

        [ForeignKey("VideoMirrorProperty")]
        public int VideoMirrorRefId { get; set; }

        public VideoMirror VideoMirrorProperty { get; set; }
    }

    [Table("VideoComment")]
    public class VideoComment
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string By { get; set; }

        public string Email { get; set; }

        public DateTime On { get; set; }

        public string Words { get; set; }

        [ForeignKey("VideoMirrorProperty")]
        public int VideoMirrorRefId { get; set; }

        public VideoMirror VideoMirrorProperty { get; set; }
    }

    public class MovieViewModel
    {
        public int MovieId { get; set; }

        public string Title { get; set; }

        public string Caption { get; set; }

        public string Tags { get; set; }

        public string Genre { get; set; }

        public string Quality { get; set; }

        public string AgeRestrictions { get; set; }

        public string Certificate { get; set; }

        public string Language { get; set; }

        public string Country { get; set; }

        public string ThumbnailUrl { get; set; }

        public DateTime ReleasedDateTime { get; set; }
    }

    public class VideoMirrorViewModel
    {
        public int VideoMirrorId { get; set; }

        public string VideoUrl { get; set; }

        public string VideoMimeType { get; set; }

        public string VideoPosterUrl { get; set; }

        public DateTime CopyOnServerDateTime { get; set; }
    }

    public class VideoSubtitleViewModel
    {
        public int VideoSubtitleId { get; set; }

        public string Language { get; set; }

        public string SubtitleUrl { get; set; }
    }

    public class VideoCommentViewModel
    {
        public int VideoCommentId { get; set; }

        public string By { get; set; }

        public string Email { get; set; }

        public DateTime On { get; set; }

        public string Words { get; set; }
    }

    public class BulksendRequestViewModel
    {
        public int BulksendRequestId { get; set; }
        public string BatchId { get; set; }
        public string BulksendId { get; set; }
        public string TenantAccountId { get; set; }
        public int EmailContentId { get; set; }
        public bool BulksendApproach { get; set; }
        public string BulksendInputDataFile { get; set; }
        public bool EmailClassification { get; set; }
        public bool DataImportType { get; set; }
        public bool IsEmailSendInvoke { get; set; }
        public bool WantToScheduleEmailSendTime { get; set; }
        public DateTime EmailSendScheduleDatetime { get; set; }
        public string DataExtensionTemplateName { get; set; }
    }
}