using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApp01.Models;
using MvcApp01.Filters;
using System.Text;

namespace MvcApp01.Controllers
{
    [LoggingFilter]
    public class RestaurantController : Controller
    {
        private RestaurantDb db = new RestaurantDb();

        [ChildActionOnly]
        public ActionResult BestRestaurant()
        {
            var bestRestaurantViewModels = 
                        (from r in db.Restaurants
                         join rr in db.RestaurantReviews on new { Id = r.Id } equals new { Id = rr.RestaurantId }
                         where
                             (from r0 in db.Restaurants
                              join rr1 in db.RestaurantReviews on new { Id = r0.Id } equals new { Id = rr1.RestaurantId }
                              group r0 by new {r0.Id} into g
                              orderby g.Count() descending
                              select new
                              {
                                  Id = g.Key.Id
                              }).Contains(new { Id = rr.RestaurantId })
                              orderby rr.Rating descending
                              select new BestRestaurantViewModel
                              {
                                Name = r.Name,
                                City = r.City,
                                Country = r.Country,
                                ReviewBody = rr.ReviewBody,
                                BestRating = rr.Rating,
                                ReviewerName = rr.ReviewerName
                              }).Skip(0).Take(1);

            return PartialView("_BestRestaurant", bestRestaurantViewModels.FirstOrDefault());
        }

        //
        // GET: /Restaurant/

        public ActionResult Index()
        {
            return View(db.Restaurants.Take(10).ToList());
        }

        //
        // GET: /Restaurant/
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult AutoComplete(string term)
        {
            var suggestedRestaurants = db.Restaurants.Where(r => r.Name.Contains(term)).Take(5).Select(r => new { label = r.Name });
            return Json(suggestedRestaurants, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Restaurant/
        [AcceptVerbs(HttpVerbs.Get)]
        [OutputCache(Duration = 30, VaryByParam = "restaurantName")]
        public ActionResult Search(string restaurantName, int pageNumber = 1)
        {
            var restaurants = db.Restaurants.Where(r => r.Name.Contains(restaurantName)).Take(10);//.ToPagedList(null, null);

            if(Request.IsAjaxRequest())
                return PartialView("_Restaurants", restaurants);

            return View("Index", restaurants);
        }

        //
        // GET: /Restaurant/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Restaurant/Create

        [HttpPost]
        public ActionResult Create(Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                db.Restaurants.Add(restaurant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(restaurant);
        }

        //
        // GET: /Restaurant/Edit/5

        public ActionResult Edit(int id)
        {
            Restaurant restaurant = db.Restaurants.Find(id);
            return View(restaurant);
        }

        //
        // POST: /Restaurant/Edit/5

        [HttpPost]
        public ActionResult Edit(Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(restaurant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(restaurant);
        }

        //
        // GET: /Restaurant/Delete/5

        public ActionResult Delete(int id)
        {
            Restaurant restaurant = db.Restaurants.Find(id);
            return View(restaurant);
        }

        //
        // POST: /Restaurant/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Restaurant restaurant = db.Restaurants.Find(id);
            db.Restaurants.Remove(restaurant);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}