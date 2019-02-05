using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Irctc.Web.Models;

namespace Irctc.Web.Controllers
{
    [Authorize]
    public class TicketController : Controller
    {
        private IrctcDbContext db = new IrctcDbContext();

        //
        // GET: /Ticket/

        public ActionResult Index()
        {
            return View(db.Passengers.ToList());
        }

        //
        // GET: /Ticket/Details/5

        public ActionResult Details(int id = 0)
        {
            Passenger passenger = db.Passengers.Find(id);
            if (passenger == null)
            {
                return HttpNotFound();
            }
            return View(passenger);
        }

        //
        // GET: /Ticket/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Ticket/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Passenger passenger)
        {
            if (ModelState.IsValid)
            {
                db.Passengers.Add(passenger);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(passenger);
        }

        //
        // GET: /Ticket/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Passenger passenger = db.Passengers.Find(id);
            if (passenger == null)
            {
                return HttpNotFound();
            }
            return View(passenger);
        }

        //
        // POST: /Ticket/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Passenger passenger)
        {
            if (ModelState.IsValid)
            {
                db.Entry(passenger).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(passenger);
        }

        //
        // GET: /Ticket/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Passenger passenger = db.Passengers.Find(id);
            if (passenger == null)
            {
                return HttpNotFound();
            }
            return View(passenger);
        }

        //
        // POST: /Ticket/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Passenger passenger = db.Passengers.Find(id);
            db.Passengers.Remove(passenger);
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