﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using nclprospekt.Models;
using nclprospekt.Exceptions;
using nclprospekt.Repository;
using System.Data;

namespace nclprospekt.Controllers
{
	[Authorize]
    public class HomeController : Controller
    {
 

        private readonly IDashboardRepository _dashboardRepository;
        public HomeController(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        //[Authorize]//(Roles = "Administrator, Development")]
        [HttpGet]
        public ActionResult Index()
        {

            DashboardWO dashWO  = new DashboardWO();
            IList<DashboardWykres> wykresPozycje = new List<DashboardWykres>();
            dashWO.daneWykres = wykresPozycje;
            ViewBag.Message = "Informacje ogólne";

            try
            {

                byte[] sesja = ((CustomPrincipal)User).Identity.Sesja;
                dashWO = _dashboardRepository.CountStatistics(sesja);
                               
                return View(dashWO);

            }
            catch (SesjaException sx)
            {
                ModelState.AddModelError("", sx.Message);
                TempData["msg"] = sx.Message;
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(dashWO);
            }
        }

       [HttpGet]
        public ActionResult Help()
        {
            ViewBag.Message = "...";

            return View();
        }

        #region "Error handle"

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception is HttpAntiForgeryException)
        {

                this.RedirectToAction("Index", "Home", null);

                //this.RedirectToAction(
                //    filterContext.RouteData.Values["action"].ToString(),
                //    filterContext.RouteData.Values);
                filterContext.ExceptionHandled = true;
        }

            base.OnException(filterContext);
        }

        #endregion
    }
}
