using nclprospekt.Models;
using nclprospekt.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nclprospekt.Controllers
{
    public class SlownikController : Controller
    {
        private ISlownikRepository _slownikiRepo;
        //
        // GET: /Slowniki/

        public SlownikController(ISlownikRepository slownikiRepo)
        {
            _slownikiRepo=slownikiRepo;
        }

        public ActionResult Index()
        {
  
                var slowniki = _slownikiRepo.slowniki(((CustomPrincipal)User).Identity.Sesja);
                return View(slowniki);
 
        }

        public ActionResult Manage(int id)
        {
            var slownik=_slownikiRepo.slownikiWartosci(((CustomPrincipal)User).Identity.Sesja,id);

            ViewBag.Message = TempData["Message"];
            ViewBag.Mode= TempData["Mode"];
           

            return View(slownik);
        }

        

        [HttpPost]
        [ValidateAntiForgeryToken]  
        public ActionResult Delete(int slownikId, int slownikWartoscId)
        {
            var result = _slownikiRepo.slownikWartoscUsun(((CustomPrincipal)User).Identity.Sesja, slownikId, slownikWartoscId);

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

            return RedirectToAction(string.Format("Manage/{0}", slownikId));
        }



        [HttpPost]
        [ValidateAntiForgeryToken]  
        public ActionResult Edit(SlownikWartosc sw)
        {
            if (ModelState.IsValid)
            {
                var result = _slownikiRepo.slwonikWartosciEdytuj(((CustomPrincipal)User).Identity.Sesja
                , sw.SlownikId, sw.SlownikWartoscId, sw.NazwaWartosc);

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
            else
            {
                TempData["Message"] = "Błąd formularza";
                TempData["Mode"] = "Error";
            }


            return RedirectToAction(string.Format("Manage/{0}", sw.SlownikId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]  
        public ActionResult AddItem(int slownikId,  string nazwaWartosc)
        {
            var result= _slownikiRepo.slownikWartoscDodaj(((CustomPrincipal)User).Identity.Sesja, slownikId, nazwaWartosc);

            if (result.status==0)
            {
                TempData["Message"] = result.status_opis;
                TempData["Mode"] = "Success";
            }
            else
            {
                TempData["Message"] = result.status_opis;
                TempData["Mode"] = "Error";
            }
            

            return RedirectToAction(string.Format("Manage/{0}", slownikId));
        }
    }
}
