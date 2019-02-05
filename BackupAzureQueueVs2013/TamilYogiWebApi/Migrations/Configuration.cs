namespace TamilYogiWebApi.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TamilYogiWebApi.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<TamilYogiWebApi.Models.TamilYogiDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(TamilYogiWebApi.Models.TamilYogiDb context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Movies.AddOrUpdate(
                m => m.Title,
                new Movie
                { 
                    Id = 1, Title = "Pasanga-2", AgeRestrictions = "12+", Caption = "", Tags="", Certificate = "U", Country = "India", Genre = "Action, Thriller", Language = "Tamil", Quality = "TcRip", ReleasedDateTime = DateTime.Now.AddDays(-10), ThumbnailUrl= "http://hydpcm347350d/vault/images/thumbnails/Pasanga-2-2015-228x160.jpg",
                    Mirrors = new[] { new VideoMirror { Id = 1, VideoUrl = "http://hydpcm347350d/vault/videos/gametrailers/Call of Duty Modern Warfare 3 Reveal Trailer.mp4", CopyOnServerDateTime = DateTime.Now.AddDays(-1), VideoMimeType = "video/mp4", VideoPosterUrl = "http://hydpcm347350d/vault/images/posters/callofduty-wide-wp-1366x768.jpg",
                    VideoSubtitles = new []{ new VideoSubtitle { Id = 1, SubtitleUrl = "http://www.videogular.com/assets/subs/pale-blue-dot.vtt", Language = "en" }},
                    VideoComments = new []
                    { 
                        new VideoComment { Id = 1, By = "Ashokan Sivapragasam", Email = "ashok@ymail.com", On = DateTime.Now.AddDays(-1), Words = "Awesome" },
                        new VideoComment { Id = 2, By = "Vinoth Sivapragasam", Email = "vinoth@ymail.com", On = DateTime.Now.AddDays(-2), Words = "Nice" },
                        new VideoComment { Id = 3, By = "Prasanna Sivapragasam", Email = "prasanna@ymail.com", On = DateTime.Now.AddDays(-3), Words = "Great work!" }
                    }}}
                },
                new Movie
                { 
                    Id = 2, Title = "Thanga Magan", AgeRestrictions = "18+", Caption = "", Certificate = "UA", Country = "India", Genre = "Romance, Action, Thriller", Language = "Tamil", Quality = "DvdRip", ReleasedDateTime = DateTime.Now.AddDays(-5), ThumbnailUrl= "http://hydpcm347350d/vault/images/thumbnails/Thanga-Magan-2015-228x160.jpg",
                    Mirrors = new[] { new VideoMirror { Id = 2, VideoUrl = "http://hydpcm347350d/vault/videos/gametrailers/Batman Arkham Origins - Official Trailer.mp4", CopyOnServerDateTime = DateTime.Now.AddDays(-1), VideoMimeType = "video/mp4", VideoPosterUrl = "http://hydpcm347350d/vault/images/posters/wordgun-wide-wp-1366x768.jpg",
                    VideoSubtitles = new []{ new VideoSubtitle { Id = 1, SubtitleUrl = "http://www.videogular.com/assets/subs/pale-blue-dot.vtt", Language = "en" }},
                    VideoComments = new []
                    { 
                        new VideoComment { Id = 1, By = "Ashokan Sivapragasam", Email = "ashok@ymail.com", On = DateTime.Now.AddDays(-1), Words = "Awesome" },
                        new VideoComment { Id = 2, By = "Vinoth Sivapragasam", Email = "vinoth@ymail.com", On = DateTime.Now.AddDays(-2), Words = "Nice" },
                        new VideoComment { Id = 3, By = "Prasanna Sivapragasam", Email = "prasanna@ymail.com", On = DateTime.Now.AddDays(-3), Words = "Great work!" }
                    }}}
                },
                new Movie 
                { 
                    Id = 3, Title = "Inji Idupazhaghi", AgeRestrictions = "16+", Caption = "", Certificate = "U", Country = "India", Genre = "Romance, Social", Language = "Tamil", Quality = "HdRip", ReleasedDateTime = DateTime.Now.AddDays(-3), ThumbnailUrl= "http://hydpcm347350d/vault/images/thumbnails/Inji-Iduppazhagi-2015-228x160.jpg",
                    Mirrors = new[] { new VideoMirror { Id = 3, VideoUrl = "http://hydpcm347350d/vault/videos/gametrailers/God of War 3 PC Game Launch Trailer Video god of war.mp4", CopyOnServerDateTime = DateTime.Now.AddDays(-1), VideoMimeType = "video/mp4", VideoPosterUrl = "http://hydpcm347350d/vault/images/posters/youtube-logo-medium-wp-485x728.jpg",
                    VideoSubtitles = new []{ new VideoSubtitle { Id = 1, SubtitleUrl = "http://www.videogular.com/assets/subs/pale-blue-dot.vtt", Language = "en" }},
                    VideoComments = new []
                    { 
                        new VideoComment { Id = 1, By = "Ashokan Sivapragasam", Email = "ashok@ymail.com", On = DateTime.Now.AddDays(-1), Words = "Awesome" },
                        new VideoComment { Id = 2, By = "Vinoth Sivapragasam", Email = "vinoth@ymail.com", On = DateTime.Now.AddDays(-2), Words = "Nice" },
                        new VideoComment { Id = 3, By = "Prasanna Sivapragasam", Email = "prasanna@ymail.com", On = DateTime.Now.AddDays(-3), Words = "Great work!" }
                    }}}
                },
                new Movie 
                { 
                    Id = 4, Title = "144", AgeRestrictions = "12+", Caption = "", Certificate = "U", Country = "India", Genre = "Romance, Comedy, Action, Thriller", Language = "Tamil", Quality = "HdRip", ReleasedDateTime = DateTime.Now.AddDays(-13), ThumbnailUrl= "http://hydpcm347350d/vault/images/thumbnails/144-2015-228x160.jpg",
                    Mirrors = new[] { new VideoMirror { Id = 4, VideoUrl = "http://hydpcm347350d/vault/videos/gametrailers/HITMAN - E3 2015 Trailer PS4.mp4", CopyOnServerDateTime = DateTime.Now.AddDays(-1), VideoMimeType = "video/mp4", VideoPosterUrl = "http://hydpcm347350d/vault/images/posters/youtube-texture-wide-wp-1366x768.jpg",
                    VideoSubtitles = new []{ new VideoSubtitle { Id = 1, SubtitleUrl = "http://www.videogular.com/assets/subs/pale-blue-dot.vtt", Language = "en" }},
                    VideoComments = new []
                    { 
                        new VideoComment { Id = 1, By = "Ashokan Sivapragasam", Email = "ashok@ymail.com", On = DateTime.Now.AddDays(-1), Words = "Awesome" },
                        new VideoComment { Id = 2, By = "Vinoth Sivapragasam", Email = "vinoth@ymail.com", On = DateTime.Now.AddDays(-2), Words = "Nice" },
                        new VideoComment { Id = 3, By = "Prasanna Sivapragasam", Email = "prasanna@ymail.com", On = DateTime.Now.AddDays(-3), Words = "Great work!" }
                    }}}
                },
                new Movie 
                { 
                    Id = 5, Title = "Bajirao Mastani", AgeRestrictions = "12+", Caption = "", Certificate = "U", Country = "India", Genre = "Romance, Action, Thriller", Language = "Tamil", Quality = "DvdRip", ReleasedDateTime = DateTime.Now.AddDays(-23), ThumbnailUrl = "http://hydpcm347350d/vault/images/thumbnails/Bajirao-Mastani-2015-228x160.jpg",
                    Mirrors = new[] { new VideoMirror { Id = 5, VideoUrl = "http://hydpcm347350d/vault/videos/gametrailers/Man of Steel Trailer 2013.mp4", CopyOnServerDateTime = DateTime.Now.AddDays(-1), VideoMimeType = "video/mp4", VideoPosterUrl = "http://hydpcm347350d/vault/images/posters/youtube-wood-wide-wp-1366x768.jpg",
                    VideoSubtitles = new []{ new VideoSubtitle { Id = 1, SubtitleUrl = "http://www.videogular.com/assets/subs/pale-blue-dot.vtt", Language = "en" }},
                    VideoComments = new []
                    { 
                        new VideoComment { Id = 1, By = "Ashokan Sivapragasam", Email = "ashok@ymail.com", On = DateTime.Now.AddDays(-1), Words = "Awesome" },
                        new VideoComment { Id = 2, By = "Vinoth Sivapragasam", Email = "vinoth@ymail.com", On = DateTime.Now.AddDays(-2), Words = "Nice" },
                        new VideoComment { Id = 3, By = "Prasanna Sivapragasam", Email = "prasanna@ymail.com", On = DateTime.Now.AddDays(-3), Words = "Great work!" }
                    }}}
                },
                new Movie 
                { 
                    Id = 6, Title = "Vedhalam", AgeRestrictions = "16+", Caption = "", Certificate = "U", Country = "India", Genre = "Action, Thriller", Language = "Tamil", Quality = "HdRip", ReleasedDateTime = DateTime.Now.AddDays(-15), ThumbnailUrl = "http://hydpcm347350d/vault/images/thumbnails/Vedhalam-2015-228x160.jpg",
                    Mirrors = new[] { new VideoMirror { Id = 6, VideoUrl = "http://hydpcm347350d/vault/videos/gametrailers/Mortal Kombat X official trailer (2015).mp4", CopyOnServerDateTime = DateTime.Now.AddDays(-1), VideoMimeType = "video/mp4", VideoPosterUrl = "http://hydpcm347350d/vault/images/posters/callofduty-wide-wp-1366x768.jpg",
                    VideoSubtitles = new []{ new VideoSubtitle { Id = 1, SubtitleUrl = "http://www.videogular.com/assets/subs/pale-blue-dot.vtt", Language = "en" }},
                    VideoComments = new []
                    { 
                        new VideoComment { Id = 1, By = "Ashokan Sivapragasam", Email = "ashok@ymail.com", On = DateTime.Now.AddDays(-1), Words = "Awesome" },
                        new VideoComment { Id = 2, By = "Vinoth Sivapragasam", Email = "vinoth@ymail.com", On = DateTime.Now.AddDays(-2), Words = "Nice" },
                        new VideoComment { Id = 3, By = "Prasanna Sivapragasam", Email = "prasanna@ymail.com", On = DateTime.Now.AddDays(-3), Words = "Great work!" }
                    }}}
                },
                new Movie 
                { 
                    Id = 7, Title = "Uppu Karuvadu", AgeRestrictions = "16+", Caption = "", Certificate = "U", Country = "India", Genre = "Romance, Action, Thriller", Language = "Tamil", Quality = "HdRip", ReleasedDateTime = DateTime.Now.AddDays(-10), ThumbnailUrl = "http://hydpcm347350d/vault/images/thumbnails/Uppu-Karuvaadu-2015-228x160.jpg",
                    Mirrors = new[] { new VideoMirror { Id = 7, VideoUrl = "http://hydpcm347350d/vault/videos/gametrailers/Tekken 3 Ultimate Trailer.mp4", CopyOnServerDateTime = DateTime.Now.AddDays(-1), VideoMimeType = "video/mp4", VideoPosterUrl = "http://hydpcm347350d/vault/images/posters/wordgun-wide-wp-1366x768.jpg",
                    VideoSubtitles = new []{ new VideoSubtitle { Id = 1, SubtitleUrl = "http://www.videogular.com/assets/subs/pale-blue-dot.vtt", Language = "en" }},
                    VideoComments = new []
                    { 
                        new VideoComment { Id = 1, By = "Ashokan Sivapragasam", Email = "ashok@ymail.com", On = DateTime.Now.AddDays(-1), Words = "Awesome" },
                        new VideoComment { Id = 2, By = "Vinoth Sivapragasam", Email = "vinoth@ymail.com", On = DateTime.Now.AddDays(-2), Words = "Nice" },
                        new VideoComment { Id = 3, By = "Prasanna Sivapragasam", Email = "prasanna@ymail.com", On = DateTime.Now.AddDays(-3), Words = "Great work!" }
                    }}}
                },
                new Movie 
                { 
                    Id = 8, Title = "Eetti", AgeRestrictions = "12+", Caption = "", Certificate = "U", Country = "India", Genre = "Romance, Action, Thriller", Language = "Tamil", Quality = "DvdRip", ReleasedDateTime = DateTime.Now.AddDays(-7), ThumbnailUrl = "http://hydpcm347350d/vault/images/thumbnails/Eetti-2015-228x160.jpg",
                    Mirrors = new[] { new VideoMirror { Id = 8, VideoUrl = "http://hydpcm347350d/vault/videos/gametrailers/Tekken7GameplayTrailer.mp4", CopyOnServerDateTime = DateTime.Now.AddDays(-1), VideoMimeType = "video/mp4", VideoPosterUrl = "http://hydpcm347350d/vault/images/posters/youtube-logo-medium-wp-485x728.jpg",
                    VideoSubtitles = new []{ new VideoSubtitle { Id = 1, SubtitleUrl = "http://www.videogular.com/assets/subs/pale-blue-dot.vtt", Language = "en" }},
                    VideoComments = new []
                    { 
                        new VideoComment { Id = 1, By = "Ashokan Sivapragasam", Email = "ashok@ymail.com", On = DateTime.Now.AddDays(-1), Words = "Awesome" },
                        new VideoComment { Id = 2, By = "Vinoth Sivapragasam", Email = "vinoth@ymail.com", On = DateTime.Now.AddDays(-2), Words = "Nice" },
                        new VideoComment { Id = 3, By = "Prasanna Sivapragasam", Email = "prasanna@ymail.com", On = DateTime.Now.AddDays(-3), Words = "Great work!" }
                    }}}
                },
                new Movie 
                { 
                    Id = 9, Title = "Thenindian", AgeRestrictions = "16+", Caption = "", Certificate = "U", Country = "India", Genre = "Romance, Action, Thriller", Language = "Tamil", Quality = "DvdRip", ReleasedDateTime = DateTime.Now.AddDays(-2), ThumbnailUrl = "http://hydpcm347350d/vault/images/thumbnails/Thenindian-2015-228x160.jpg",
                    Mirrors = new[] { new VideoMirror { Id = 9, VideoUrl = "http://hydpcm347350d/vault/videos/gametrailers/AssassinsCreedUnityReview.mp4", CopyOnServerDateTime = DateTime.Now.AddDays(-1), VideoMimeType = "video/mp4", VideoPosterUrl = "http://hydpcm347350d/vault/images/posters/youtube-texture-wide-wp-1366x768.jpg",
                    VideoSubtitles = new []{ new VideoSubtitle { Id = 1, SubtitleUrl = "http://www.videogular.com/assets/subs/pale-blue-dot.vtt", Language = "en" }},
                    VideoComments = new []
                    { 
                        new VideoComment { Id = 1, By = "Ashokan Sivapragasam", Email = "ashok@ymail.com", On = DateTime.Now.AddDays(-1), Words = "Awesome" },
                        new VideoComment { Id = 2, By = "Vinoth Sivapragasam", Email = "vinoth@ymail.com", On = DateTime.Now.AddDays(-2), Words = "Nice" },
                        new VideoComment { Id = 3, By = "Prasanna Sivapragasam", Email = "prasanna@ymail.com", On = DateTime.Now.AddDays(-3), Words = "Great work!" }
                    }}}
                }
                );

            /*context.Movies.AddOrUpdate(
                m => m.Title,
                new Movie { Id = 1, Title = "Pasanga-2", AgeRestrictions = "12+", Caption = "", Certificate = "U", Country = "India", Genre = "Action, Thriller", Language = "Tamil", Quality = "TcRip", ReleasedDateTime = DateTime.Now.AddDays(-10) },
                new Movie { Id = 2, Title = "Thanga Magan", AgeRestrictions = "18+", Caption = "", Certificate = "UA", Country = "India", Genre = "Romance, Action, Thriller", Language = "Tamil", Quality = "DvdRip", ReleasedDateTime = DateTime.Now.AddDays(-5) },
                new Movie { Id = 3, Title = "Inji Idupazhaghi", AgeRestrictions = "16+", Caption = "", Certificate = "U", Country = "India", Genre = "Romance, Social", Language = "Tamil", Quality = "HdRip", ReleasedDateTime = DateTime.Now.AddDays(-3) },
                new Movie { Id = 4, Title = "144", AgeRestrictions = "12+", Caption = "", Certificate = "U", Country = "India", Genre = "Romance, Comedy, Action, Thriller", Language = "Tamil", Quality = "HdRip", ReleasedDateTime = DateTime.Now.AddDays(-13) },
                new Movie { Id = 5, Title = "Bajirao Mastani", AgeRestrictions = "12+", Caption = "", Certificate = "U", Country = "India", Genre = "Romance, Action, Thriller", Language = "Tamil", Quality = "DvdRip", ReleasedDateTime = DateTime.Now.AddDays(-23) },
                new Movie { Id = 6, Title = "Vedhalam", AgeRestrictions = "16+", Caption = "", Certificate = "U", Country = "India", Genre = "Action, Thriller", Language = "Tamil", Quality = "HdRip", ReleasedDateTime = DateTime.Now.AddDays(-15) },
                new Movie { Id = 7, Title = "Uppu Karuvadu", AgeRestrictions = "16+", Caption = "", Certificate = "U", Country = "India", Genre = "Romance, Action, Thriller", Language = "Tamil", Quality = "HdRip", ReleasedDateTime = DateTime.Now.AddDays(-10) },
                new Movie { Id = 8, Title = "Eetti", AgeRestrictions = "12+", Caption = "", Certificate = "U", Country = "India", Genre = "Romance, Action, Thriller", Language = "Tamil", Quality = "DvdRip", ReleasedDateTime = DateTime.Now.AddDays(-7) },
                new Movie { Id = 9, Title = "Thenindian", AgeRestrictions = "16+", Caption = "", Certificate = "U", Country = "India", Genre = "Romance, Action, Thriller", Language = "Tamil", Quality = "DvdRip", ReleasedDateTime = DateTime.Now.AddDays(-2) }
                );

            context.VideoMirrors.AddOrUpdate(
                mm => mm.VideoUrl,
                new VideoMirror { Id = 1, MovieId = 1, VideoUrl = "http://hydpcm347350d/vault/videos/AngularJS Tutorial - Code School-9.mp4", CopyOnServerDateTime = DateTime.Now.AddDays(-1), VideoMimeType = "video/mp4", VideoPosterUrl = "http://hydpcm347350d/vault/images/thumbnails/Pasanga-2-2015-228x160.jpg" },
                new VideoMirror { Id = 2, MovieId = 2, VideoUrl = "http://hydpcm347350d/vault/videos/AngularJS Tutorial - Code School-10.mp4", CopyOnServerDateTime = DateTime.Now.AddDays(-1), VideoMimeType = "video/mp4", VideoPosterUrl = "http://hydpcm347350d/vault/images/thumbnails/Thanga-Magan-2015-228x160.jpg" },
                new VideoMirror { Id = 3, MovieId = 3, VideoUrl = "http://hydpcm347350d/vault/videos/AngularJS Tutorial - Code School-9.mp4", CopyOnServerDateTime = DateTime.Now.AddDays(-1), VideoMimeType = "video/mp4", VideoPosterUrl = "http://hydpcm347350d/vault/images/thumbnails/Inji-Iduppazhagi-2015-228x160.jpg" },
                new VideoMirror { Id = 4, MovieId = 4, VideoUrl = "http://hydpcm347350d/vault/videos/AngularJS Tutorial - Code School-10.mp4", CopyOnServerDateTime = DateTime.Now.AddDays(-1), VideoMimeType = "video/mp4", VideoPosterUrl = "http://hydpcm347350d/vault/images/thumbnails/144-2015-228x160.jpg" },
                new VideoMirror { Id = 5, MovieId = 5, VideoUrl = "http://hydpcm347350d/vault/videos/AngularJS Tutorial - Code School-9.mp4", CopyOnServerDateTime = DateTime.Now.AddDays(-1), VideoMimeType = "video/mp4", VideoPosterUrl = "http://hydpcm347350d/vault/images/thumbnails/Bajirao-Mastani-2015-228x160.jpg" },
                new VideoMirror { Id = 6, MovieId = 6, VideoUrl = "http://hydpcm347350d/vault/videos/AngularJS Tutorial - Code School-10.mp4", CopyOnServerDateTime = DateTime.Now.AddDays(-1), VideoMimeType = "video/mp4", VideoPosterUrl = "http://hydpcm347350d/vault/images/thumbnails/Vedhalam-2015-228x160.jpg" },
                new VideoMirror { Id = 7, MovieId = 7, VideoUrl = "http://hydpcm347350d/vault/videos/AngularJS Tutorial - Code School-9.mp4", CopyOnServerDateTime = DateTime.Now.AddDays(-1), VideoMimeType = "video/mp4", VideoPosterUrl = "http://hydpcm347350d/vault/images/thumbnails/Uppu-Karuvaadu-2015-228x160.jpg" },
                new VideoMirror { Id = 8, MovieId = 8, VideoUrl = "http://hydpcm347350d/vault/videos/AngularJS Tutorial - Code School-10.mp4", CopyOnServerDateTime = DateTime.Now.AddDays(-1), VideoMimeType = "video/mp4", VideoPosterUrl = "http://hydpcm347350d/vault/images/thumbnails/Eetti-2015-228x160.jpg" },
                new VideoMirror { Id = 9, MovieId = 9, VideoUrl = "http://hydpcm347350d/vault/videos/AngularJS Tutorial - Code School-9.mp4", CopyOnServerDateTime = DateTime.Now.AddDays(-1), VideoMimeType = "video/mp4", VideoPosterUrl = "http://hydpcm347350d/vault/images/thumbnails/Thenindian-2015-228x160.jpg" }
                );

            context.VideoSubtitles.AddOrUpdate(
                ms => ms.SubtitleUrl,
                new VideoSubtitle { Id = 1, VideoMirrorId = 1, SubtitleUrl = "", Language = "en" },
                new VideoSubtitle { Id = 2, VideoMirrorId = 2, SubtitleUrl = "", Language = "en" },
                new VideoSubtitle { Id = 3, VideoMirrorId = 3, SubtitleUrl = "", Language = "en" },
                new VideoSubtitle { Id = 4, VideoMirrorId = 4, SubtitleUrl = "", Language = "en" },
                new VideoSubtitle { Id = 5, VideoMirrorId = 5, SubtitleUrl = "", Language = "en" },
                new VideoSubtitle { Id = 6, VideoMirrorId = 6, SubtitleUrl = "", Language = "en" },
                new VideoSubtitle { Id = 7, VideoMirrorId = 7, SubtitleUrl = "", Language = "en" },
                new VideoSubtitle { Id = 8, VideoMirrorId = 8, SubtitleUrl = "", Language = "en" },
                new VideoSubtitle { Id = 9, VideoMirrorId = 9, SubtitleUrl = "", Language = "en" }
                );*/
        }
    }
}
