using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using nclprospekt.Models;
using nclprospekt.Exceptions;
using nclprospekt.Repository;
using System.Data;
using nclprospekt.NCL_WS;

namespace nclprospekt.Controllers
{
     [Authorize]
    public class UzytkownikController : Controller
    {
        private readonly IUzytkownikRepository _uzytkownikRepository;
        

        public UzytkownikController(IUzytkownikRepository uzytkownikRepository)
        {
            _uzytkownikRepository = uzytkownikRepository;
            
        }
        

        //
        // GET: /Uzytkownik/
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Message = TempData["Message"];
            ViewBag.Mode = TempData["Mode"];

            //return View();

            //BYŁO
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {

            Uzytkownik uzytkownik = new Uzytkownik();
            if (id <= 0) return RedirectToAction("Index");
           
            try
            {
                ModelState.Clear();
                return View(uzytkownik);
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
                //Logger.Error("Failed for app " + CurrentApplication, ex);
                return RedirectToAction("Index");
            }

        }     

        [HttpGet]
        public ActionResult Edit()
           {
               Uzytkownik daneUzytkownika = new Uzytkownik();

               try
               {
                byte[] sesja = User.ToCustomPrincipal().Identity.Sesja;
                int uzytkownikId = Convert.ToInt32(NZ(_uzytkownikRepository.GetByUserName(sesja, User.ToCustomPrincipal().Identity.Name).Uzytkownik_Id, 0));

                       daneUzytkownika = _uzytkownikRepository.EditUser(((CustomPrincipal)User).Identity.Sesja, uzytkownikId);

               }
               catch (SesjaException sx)
               {
                   ModelState.AddModelError("", sx.Message);
                   TempData["msg"] = sx.Message;
                   Response.StatusDescription = sx.Message;
                   return RedirectToAction("Login", "Account");
               }
               catch (Exception ex)
               {
                   TempData["Error"] = ex.Message;
                  // ModelState.AddModelError("Error: ", ex.Message);
                  // Response.StatusCode = 400;
                  // Response.TrySkipIisCustomErrors = true;
                // return Json(new { status = -1, Message = ex.Message }, JsonRequestBehavior.AllowGet);
                   return View(daneUzytkownika);

               }
               return View(daneUzytkownika);
           }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Uzytkownik model)
           {

               RezultatObject rezultatZapisu = new RezultatObject();
           
               try
               {
                       bool pozostaw_blokade = false;
                   int czy_maile = (model.CZY_MAILE == true) ? 1 : 0;
                   string telefonKom = model.TelefonKomorkowy.ToString();
                       model.Haslo = ""; //Dzięki temu hasło się nie zmieni
                       rezultatZapisu = _uzytkownikRepository.EditUserBasicDataSave(((CustomPrincipal)User).Identity.Sesja, model.Imie, model.Nazwisko, model.Nazwa, telefonKom, model.Email, model.Login, model.Haslo, czy_maile, model.BLOKADA_ID, pozostaw_blokade);
                    
               }
               catch (SesjaException sx)
               {
                   ModelState.AddModelError("", sx.Message);
                   
                   TempData["msg"] = sx.Message;
                   Response.StatusDescription = sx.Message;
                   return RedirectToAction("Login", "Account");
               }
               catch (Exception ex)
               {
                   TempData["Error"] = ex.Message;
                   ModelState.AddModelError("Error: ", ex.Message);
                   Response.StatusCode = 400;
                   Response.TrySkipIisCustomErrors = true;

                   return RedirectToAction("Edit", "Uzytkownik");

               }
               
               TempData[rezultatZapisu.statusString] = rezultatZapisu.message;
               return RedirectToAction("Edit", "Uzytkownik");
           }

        [HttpGet]
        public ActionResult Notyfikacje()
        {
            Uzytkownik daneUzytkownika = new Uzytkownik();

            try
            {
                byte[] sesja = User.ToCustomPrincipal().Identity.Sesja;
                int uzytkownikId = Convert.ToInt32(NZ(_uzytkownikRepository.GetByUserName(sesja, User.ToCustomPrincipal().Identity.Name).Uzytkownik_Id, 0));
                daneUzytkownika = _uzytkownikRepository.UserNotyfikacjePobierz(((CustomPrincipal)User).Identity.Sesja, uzytkownikId);

            }
            catch (SesjaException sx)
            {
                ModelState.AddModelError("", sx.Message);
                TempData["msg"] = sx.Message;
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = 403;
                return Json(new
                {
                    redirectUrl = Url.Action("Login", "Account"),
                    isRedirect = true
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                ModelState.AddModelError("Error: ", ex.Message);
                Response.StatusCode = 400;
                Response.TrySkipIisCustomErrors = true;
                return Json(new { status = -1, Message = ex.Message }, JsonRequestBehavior.AllowGet);

            }
            return PartialView(daneUzytkownika);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Notyfikacje(Uzytkownik model)
           {
               RezultatObject rezultatZapisu = new RezultatObject();
               DataTable dt = new DataTable();
               try
               {
                   DataTable notyfikacjeDT = new DataTable();
                   notyfikacjeDT.Columns.Add("user_id");
                   notyfikacjeDT.Columns.Add("notyfikacja");
                   notyfikacjeDT.Columns.Add("wlacz");


                   foreach (UzytkownikNotyfikacje notyfikacjaObj in model.notyfikacje)
                   { 
                       DataRow notyfikacjaRow = notyfikacjeDT.NewRow();
                       notyfikacjaRow["user_id"] = notyfikacjaObj.user_Id;
                       notyfikacjaRow["notyfikacja"] = notyfikacjaObj.notyfikacja;
                       notyfikacjaRow["wlacz"] = notyfikacjaObj.wlacz;
                       notyfikacjeDT.Rows.Add(notyfikacjaRow);
                       notyfikacjeDT.AcceptChanges();
                   }
                   
                   rezultatZapisu = _uzytkownikRepository.UserNotyfikacjeZapisz(((CustomPrincipal)User).Identity.Sesja, notyfikacjeDT);

               }
               catch (SesjaException sx)
               {
                   ModelState.AddModelError("", sx.Message);

                   TempData["msg"] = sx.Message;
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = 403;
                return Json(new
                {
                    redirectUrl = Url.Action("Login", "Account"),
                    isRedirect = true
                }, JsonRequestBehavior.AllowGet);
               }
               catch (Exception ex)
               {
                   //TempData["Error"] = ex.Message;
                   ModelState.AddModelError("Error: ", ex.Message);
                   Response.StatusCode = 400;
                   Response.TrySkipIisCustomErrors = true;

                   //return RedirectToAction("Edit", "Uzytkownik");
                   return Json(new { status = -1, Message = ex.Message }, JsonRequestBehavior.AllowGet);

               }

               ModelState.Clear();
               ViewBag.Message = rezultatZapisu.message;
               ViewBag.status = rezultatZapisu.status;
               Response.StatusCode = 200;
               return Json(new { status = rezultatZapisu.status, Message = rezultatZapisu.message, blokada_id = rezultatZapisu.blokada_id }, JsonRequestBehavior.AllowGet);
             
           }


        private object NZ(object S, object Def)
        {
            if (DBNull.Value.Equals(S))
            {
                return Def;
            }
            else
            {
                if ((S != null))
                {
                    return (S);
                }
                else
                {
                    return Def;
                }
            }
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
