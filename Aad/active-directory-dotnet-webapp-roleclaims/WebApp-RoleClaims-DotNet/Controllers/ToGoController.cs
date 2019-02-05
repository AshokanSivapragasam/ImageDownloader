using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp_RoleClaims_DotNet.Controllers
{
    public class ToGoController : Controller
    {
        // GET: ToGo
        public ActionResult Index()
        {
            return View();
        }

        // GET: ToGo/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ToGo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ToGo/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ToGo/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ToGo/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ToGo/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ToGo/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
