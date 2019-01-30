using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class CategorizeContentController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
