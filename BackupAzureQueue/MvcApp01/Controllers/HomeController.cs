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
    public class HomeController : Controller
    {
        private RestaurantDb db = new RestaurantDb();

        //
        // GET: /Home/

        public ViewResult Index()
        {
            return View(db.Restaurants.ToList());
        }

        //
        // GET: /Home/Details/5

        public ViewResult Details(int id)
        {
            Restaurant restaurant = db.Restaurants.Find(id);
            return View(restaurant);
        }

        //
        // GET: /Home/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Home/Create

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
        // GET: /Home/Edit/5
 
        public ActionResult Edit(int id)
        {
            Restaurant restaurant = db.Restaurants.Find(id);
            return View(restaurant);
        }

        //
        // POST: /Home/Edit/5

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
        // GET: /Home/Delete/5
 
        public ActionResult Delete(int id)
        {
            Restaurant restaurant = db.Restaurants.Find(id);
            return View(restaurant);
        }

        //
        // POST: /Home/Delete/5

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