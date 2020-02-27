using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SteelStudio.Web.Controllers
{
    public class DesktopController : Controller
    {
        // GET: Desktop
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult _MenuPartialView()
        {            
            return PartialView();
        }
    }
}