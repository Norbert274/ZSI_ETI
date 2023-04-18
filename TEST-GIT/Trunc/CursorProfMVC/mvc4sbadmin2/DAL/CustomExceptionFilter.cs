using nclprospekt.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace nclprospekt
{
    public class CustomExceptionFilter : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            //Dzięki temu jesli gdzies sie trafi blad sesji to wyrzuci logowanie (np sprawdzanie rol uzytkownika w Authorize)
            if (filterContext.Exception.GetType() == typeof(SesjaException))
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.HttpContext.Response.Redirect("~/Account/Login");
                    filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
                    JsonResult result = new JsonResult
                    {
                        Data = new 
                        {
                            redirectUrl = filterContext.HttpContext.Response.RedirectLocation,
                            isRedirect = true
                        },
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                        
                        
                    filterContext.Result = result;
                }

                else
                {
                filterContext.HttpContext.Response.Redirect("~/Account/Login");
                filterContext.ExceptionHandled = true;
            }
        }
        }



    }
}
