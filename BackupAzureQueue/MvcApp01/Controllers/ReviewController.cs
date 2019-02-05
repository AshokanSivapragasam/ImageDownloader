using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApp01.Models;

namespace MvcApp01.Controllers
{ 
    public class ReviewController : Controller
    {
        private RestaurantDb db = new RestaurantDb();

        //
        // GET: /Review/

        public ViewResult List(int restaurantId)
        {
            ViewData["restaurantId"] = restaurantId;
            return View(db.Restaurants.Find(restaurantId).Reviews.ToList());
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ViewResult Search(int searchByRating, int restaurantId)
        {
            var modelReviews = from r in db.Restaurants
                        join rr in db.RestaurantReviews on r.Id equals rr.RestaurantId
                        where r.Id == restaurantId && rr.Rating >= searchByRating
                        select rr;

            ViewData["restaurantId"] = restaurantId;

            return View("List", modelReviews);
        }

        //
        // GET: /Review/Details/5

        public ViewResult Details(int id)
        {
            RestaurantReview restaurantreview = db.RestaurantReviews.Find(id);
            return View(restaurantreview);
        }

        //
        // GET: /Review/Create

        public ActionResult Create(int restaurantId)
        {
            var model = new RestaurantReview();
            model.RestaurantId = restaurantId;

            return View(model);
        } 

        //
        // POST: /Review/Create

        [HttpPost]
        public ActionResult Create(RestaurantReview restaurantreview)
        {
            if (ModelState.IsValid)
            {
                db.RestaurantReviews.Add(restaurantreview);
                db.SaveChanges();
                return RedirectToAction("List", new { restaurantId = restaurantreview.RestaurantId });
            }

            return View(restaurantreview);
        }
        
        //
        // GET: /Review/Edit/5
 
        public ActionResult Edit(int id)
        {
            RestaurantReview restaurantreview = db.RestaurantReviews.Find(id);
            return View(restaurantreview);
        }

        //
        // POST: /Review/Edit/5

        [HttpPost]
        public ActionResult Edit(RestaurantReview restaurantreview)
        {
            if (ModelState.IsValid)
            {
                db.Entry(restaurantreview).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("List", new { restaurantId = restaurantreview.RestaurantId });
            }
            return View(restaurantreview);
        }

        //
        // GET: /Review/Delete/5
 
        public ActionResult Delete(int id)
        {
            RestaurantReview restaurantreview = db.RestaurantReviews.Find(id);
            return View(restaurantreview);
        }

        //
        // POST: /Review/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            RestaurantReview restaurantreview = db.RestaurantReviews.Find(id);
            db.RestaurantReviews.Remove(restaurantreview);
            db.SaveChanges();
            return RedirectToAction("List", new { restaurantId = restaurantreview.RestaurantId });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}