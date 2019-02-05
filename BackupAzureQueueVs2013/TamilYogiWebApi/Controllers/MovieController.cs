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
    public class MovieController : ApiController
    {
        private TamilYogiDb db = new TamilYogiDb();

        // OPTIONS
        public HttpResponseMessage Options()
        {
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
        }

        // GET api/Movie
        public IQueryable<MovieViewModel> GetMovies()
        {
            var movieViewModel = db.Movies.Select(movie =>
                                    new MovieViewModel
                                    {
                                        AgeRestrictions = movie.AgeRestrictions,
                                        Caption = movie.Caption,
                                        Certificate = movie.Certificate,
                                        Country = movie.Country,
                                        Genre = movie.Genre,
                                        Language = movie.Language,
                                        MovieId = movie.Id,
                                        Quality = movie.Quality,
                                        ReleasedDateTime = movie.ReleasedDateTime,
                                        Tags = movie.Tags,
                                        Title = movie.Title,
                                        ThumbnailUrl = movie.ThumbnailUrl
                                    });

            return movieViewModel;
        }

        // GET api/Movie/5
        [ResponseType(typeof(MovieViewModel))]
        public IHttpActionResult GetMovie(int movieId)
        {
            var movieViewModel = new MovieViewModel();
            var movie = db.Movies.Find(movieId);
            if (movie != null)
            {
                movieViewModel = new MovieViewModel
                {
                    AgeRestrictions = movie.AgeRestrictions,
                    Caption = movie.Caption,
                    Certificate = movie.Certificate,
                    Country = movie.Country,
                    Genre = movie.Genre,
                    Language = movie.Language,
                    MovieId = movie.Id,
                    Quality = movie.Quality,
                    ReleasedDateTime = movie.ReleasedDateTime,
                    Tags = movie.Tags,
                    Title = movie.Title,
                    ThumbnailUrl = movie.ThumbnailUrl
                };
                return Ok(movieViewModel);
            }
            else
            {
                return NotFound();
            }
        }

        // PUT api/Movie/5
        public IHttpActionResult PutMovie(int id, Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != movie.Id)
            {
                return BadRequest();
            }

            db.Entry(movie).State = System.Data.Entity.EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
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

        // POST api/Movie
        [ResponseType(typeof(Movie))]
        public IHttpActionResult PostMovie(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Movies.Add(movie);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = movie.Id }, movie);
        }

        // DELETE api/Movie/5
        [ResponseType(typeof(Movie))]
        public IHttpActionResult DeleteMovie(int id)
        {
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return NotFound();
            }

            db.Movies.Remove(movie);
            db.SaveChanges();

            return Ok(movie);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MovieExists(int id)
        {
            return db.Movies.Count(e => e.Id == id) > 0;
        }
    }
}
