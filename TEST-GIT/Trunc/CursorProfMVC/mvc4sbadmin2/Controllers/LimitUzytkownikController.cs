using nclprospekt.Models;
using nclprospekt.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace nclprospekt.Controllers
{
    public class LimitUzytkownikController : Controller
    {
        private ILimitUzytkownikaRepository _limitUzytkownikRepo;

        public LimitUzytkownikController(ILimitUzytkownikaRepository limitUzytkownikRepo)
        {
            _limitUzytkownikRepo = limitUzytkownikRepo;
        }


        // GET: /LimitUzytkownik/
        public ActionResult Index()
        {
            //GET
            var limtyUzytkownik = _limitUzytkownikRepo.LimityGet(((CustomPrincipal)User).Identity.Sesja);

            ViewBag.Message = TempData["Message"];
            ViewBag.Mode = TempData["Mode"];

            return View(limtyUzytkownik);
        }

        [HttpPost]
        public ActionResult Index(LimitVmP vm)
        {
            if (vm.Users.Where(x=>x.Wybierz).ToList().Count==0)
            {
                TempData["Message"] = "Nie wybrano żadnego użytkownika";
                TempData["Mode"] = "Error";
            }
            else
            {
                 //UPDATE
                 var result = _limitUzytkownikRepo.UserLimityZapiszWybranym(((CustomPrincipal)User).Identity.Sesja, vm);

                 if (result.status == 0)
                 {
                     TempData["Message"] = result.status_opis;
                     TempData["Mode"] = "Success";
                 }
                 else
                 {
                     TempData["Message"] = result.status_opis;
                     TempData["Mode"] = "Error";
                 }
            }
   

            return RedirectToAction("Index");
        }

        
    }
}
