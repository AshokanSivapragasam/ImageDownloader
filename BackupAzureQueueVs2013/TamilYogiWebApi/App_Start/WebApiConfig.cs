using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace TamilYogiWebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "FileManagerApi",
                routeTemplate: "resources/filemanager/{action}",
                defaults: new { controller = "FileManager" }
            );

            config.Routes.MapHttpRoute(
                name: "EiRequestsApi",
                routeTemplate: "resources/eirequests/{action}",
                defaults: new { controller = "EiRequests"}
            );

            config.Routes.MapHttpRoute(
                name: "VideoSubtitleApi",
                routeTemplate: "resources/videosubtitles/{videoMirrorId}",
                defaults: new { videoMirrorId = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "VideoCommentApi",
                routeTemplate: "resources/videocomments/{videoMirrorId}",
                defaults: new { controller = "Comment", videoMirrorId = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "VideoMirrorApi",
                routeTemplate: "resources/videomirrors/{movieId}",
                defaults: new { controller = "VideoMirror", movieId = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "MovieApi",
                routeTemplate: "resources/movies/{movieId}",
                defaults: new { controller = "Movie", movieId = RouteParameter.Optional }
            );
        }
    }
}
