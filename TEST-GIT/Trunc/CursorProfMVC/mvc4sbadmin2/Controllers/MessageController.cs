using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nclprospekt.Controllers
{
    public class MessageController : Controller
    {
        [ChildActionOnly]
        public ActionResult TempMessage()
        {
            if (TempData["error"] !=null)
            {
                TempData["error"] = TempData["error"].ToString();
            }

            return PartialView();
        }

    }
}
