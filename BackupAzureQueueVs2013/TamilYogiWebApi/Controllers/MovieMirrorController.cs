using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TamilYogiWebApi.Models;

namespace TamilYogiWebApi.Controllers
{
    public class VideoMirrorController : ApiController
    {
        private TamilYogiDb db = new TamilYogiDb();

        // OPTIONS
        public HttpResponseMessage Options()
        {
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
        }

        // GET api/VideoMirror
        public IQueryable<VideoMirrorViewModel> GetVideoMirrors()
        {
            return db.VideoMirrors.Select(videoMirror =>
                    new VideoMirrorViewModel
                    {
                        VideoMirrorId = videoMirror.Id,
                        VideoUrl = videoMirror.VideoUrl,
                        VideoMimeType = videoMirror.VideoMimeType,
                        VideoPosterUrl = videoMirror.VideoPosterUrl,
                        CopyOnServerDateTime = videoMirror.CopyOnServerDateTime
                    });
        }

        // GET api/VideoMirror/5
        [ResponseType(typeof(VideoMirror))]
        public IHttpActionResult GetVideoMirror(int movieId)
        {
            var videoMirrorViewModel = new VideoMirrorViewModel();
            var videoMirrorResults = db.VideoMirrors.Where(mirror => mirror.MovieRefId == movieId);
            var videoMirror = videoMirrorResults != null ? videoMirrorResults.FirstOrDefault() : null;
            if (videoMirror != null)
            {
                videoMirrorViewModel = new VideoMirrorViewModel
                {
                    VideoMirrorId = videoMirror.Id,
                    VideoUrl = videoMirror.VideoUrl,
                    VideoMimeType = videoMirror.VideoMimeType,
                    VideoPosterUrl = videoMirror.VideoPosterUrl,
                    CopyOnServerDateTime = videoMirror.CopyOnServerDateTime
                };
                return Ok(videoMirrorViewModel);
            }
            else
            {
                return NotFound();
            }
        }

        // PUT api/VideoMirror/5
        public IHttpActionResult PutVideoMirror(int id, VideoMirror videomirror)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != videomirror.Id)
            {
                return BadRequest();
            }

            db.Entry(videomirror).State = System.Data.Entity.EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VideoMirrorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/VideoMirror
        [ResponseType(typeof(VideoMirror))]
        public IHttpActionResult PostVideoMirror(VideoMirror videomirror)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.VideoMirrors.Add(videomirror);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = videomirror.Id }, videomirror);
        }

        // DELETE api/VideoMirror/5
        [ResponseType(typeof(VideoMirror))]
        public IHttpActionResult DeleteVideoMirror(int id)
        {
            VideoMirror videomirror = db.VideoMirrors.Find(id);
            if (videomirror == null)
            {
                return NotFound();
            }

            db.VideoMirrors.Remove(videomirror);
            db.SaveChanges();

            return Ok(videomirror);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VideoMirrorExists(int id)
        {
            return db.VideoMirrors.Count(e => e.Id == id) > 0;
        }
    }
}