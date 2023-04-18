using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nclprospekt
{
    public class AuthorizeAttribute : System.Web.Mvc.AuthorizeAttribute
    {


        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                filterContext.HttpContext.Response.Redirect("~/Home/Index");
               filterContext.Controller.TempData["error"] = "Nie masz prawa dostępu do tej strony";

            }

            //else do normal process
            base.HandleUnauthorizedRequest(filterContext);
        }
    }
}
