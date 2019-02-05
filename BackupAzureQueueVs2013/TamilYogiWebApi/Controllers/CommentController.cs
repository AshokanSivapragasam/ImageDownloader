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
    public class CommentController : ApiController
    {
        private TamilYogiDb db = new TamilYogiDb();

        // OPTIONS
        public HttpResponseMessage Options()
        {
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
        }

        // GET api/Comment/5
        public IQueryable<VideoCommentViewModel> GetVideoComments(int videoMirrorId)
        {
            var videoCommentResults = db.VideoComments.Where(comment => comment.VideoMirrorRefId == videoMirrorId);
            if (videoCommentResults != null)
                return videoCommentResults.Select(videoComment =>
                        new VideoCommentViewModel
                        {
                            VideoCommentId = videoComment.Id,
                            By = videoComment.By,
                            Email = videoComment.Email,
                            On = videoComment.On,
                            Words = videoComment.Words
                        });

            return null;
        }

        // PUT api/Comment/5
        public IHttpActionResult PutVideoComment(int id, VideoComment videocomment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != videocomment.Id)
            {
                return BadRequest();
            }

            db.Entry(videocomment).State = System.Data.Entity.EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VideoCommentExists(id))
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

        // POST api/Comment
        [ResponseType(typeof(VideoComment))]
        public IHttpActionResult PostVideoComment(VideoComment videocomment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.VideoComments.Add(videocomment);
            db.SaveChanges();

            return CreatedAtRoute("VideoCommentApi", new { id = videocomment.Id }, videocomment);
        }

        // DELETE api/Comment/5
        [ResponseType(typeof(VideoComment))]
        public IHttpActionResult DeleteVideoComment(int id)
        {
            VideoComment videocomment = db.VideoComments.Find(id);
            if (videocomment == null)
            {
                return NotFound();
            }

            db.VideoComments.Remove(videocomment);
            db.SaveChanges();

            return Ok(videocomment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VideoCommentExists(int id)
        {
            return db.VideoComments.Count(e => e.Id == id) > 0;
        }
    }
}