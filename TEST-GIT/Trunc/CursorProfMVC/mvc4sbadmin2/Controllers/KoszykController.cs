using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using nclprospekt.Models;
using nclprospekt.Exceptions;
using nclprospekt.Repository;
using System.Text.RegularExpressions;
using System.Web.Helpers;
using nclprospekt.Utils;
using System.Data;
using nclprospekt.JDataTables;
using System.Globalization;

namespace nclprospekt.Controllers
{
      [Authorize(Roles = "KoszykToolStripMenuItem")]
    public class KoszykController : Controller
    {

        private readonly IZamowienieRepository _zamowienieRepository;
        private readonly IAdresRepository _adresRepository;
        private readonly IUzytkownikRepository _uzytkownikRepository;

        public KoszykController(IZamowienieRepository zamowienieRepository, IAdresRepository adresRepository, IUzytkownikRepository uzytkownikRepository)
        {
            _zamowienieRepository = zamowienieRepository;
            _adresRepository = adresRepository;
            _uzytkownikRepository = uzytkownikRepository;
        }

        //
        // GET: /Koszyk/Details/5
        [HttpGet]
      
        public ActionResult Details(int id)
        {
            if (id <= 0) return RedirectToAction("Index");

            ZamowienieWczytaneWO zamWO = new ZamowienieWczytaneWO();

            try
            {
                zamWO = _zamowienieRepository.FindById(id, ((CustomPrincipal)User).Identity.Sesja);

                ModelState.Clear();
                return View(zamWO);
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
        [NoCache]
        public ActionResult Edit(int id = -1)
        {

            ViewBag.Message = "Koszyk";
            ZamowienieWczytaneWO currZamowienie = new ZamowienieWczytaneWO();
            try
            {

                currZamowienie = _zamowienieRepository.FindById(id, ((CustomPrincipal)User).Identity.Sesja);
                
                //tryb_pracy = zwracane wywołującemu: 1 - edycja własnego koszyka, 2 - podgląd obcego koszyka, 3 - podgląd zamówienia
                ViewBag.TrybPracy = currZamowienie.zamowienie.tryb_pracy; 
                if (currZamowienie.zamowienie.status != 0)
                {
                    TempData["warning"] = currZamowienie.zamowienie.status_opis;
                }
                

                return View(currZamowienie);
            }
            catch (SesjaException sx)
            {
                ModelState.AddModelError("", sx.Message);
                TempData["msg"] = sx.Message;
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                ModelState.AddModelError("", ex.Message);
                return RedirectToAction("Index", "Home");
            }

        }

        [HttpGet]
        [NoCache]
        public ActionResult ZamowienieAdresZdefiniowany(int zamowienie_id = -1, int adres_id=-1, int typ_zamowienia = -1)
        {
            

            ZamowienieWczytaneWO zamWO = new ZamowienieWczytaneWO();
            zamWO.zamowienie = new ZamowienieWczytane();
            try
            {
                if (zamowienie_id == -1 && adres_id == -1 && typ_zamowienia == 1)
                {
                    SesjaException logout = new SesjaException("Sesja wygasła. Zaloguj się ponownie!");
                    throw logout;
                }

                zamWO = _zamowienieRepository.PobierzAdresZdefiniowanyZamowienia(((CustomPrincipal)User).Identity.Sesja, zamowienie_id, adres_id);
                zamWO.zamowienie.zamowienie_id = zamowienie_id;
                zamWO.zamowienie.typ_zamowienia = typ_zamowienia;
                zamWO.zamowienie.adres_id = adres_id;
                
                //Nie można tak zrobić bo nie będzie zdarzenia na zmianie adresu więc nie zaczytają się prawidłowe daty wysyłki
                //Ustawianie adresu na pierwszy który nie ma id = -1 - tylko w przypadku kiedy stacja ma tylko 1 adres (czyli pusty i swój) i tylko dla nowych zamówień
                //if (adres_id == -1 && zamWO != null && zamWO.zamowienieZdefiniowaneAdresy != null && zamWO.zamowienieZdefiniowaneAdresy.Count == 2)
                //{
                //    foreach (ZamowienieAdresZdefiniowanyWczytane adr in zamWO.zamowienieZdefiniowaneAdresy)
                //    { 
                //    if(adr.adres_id != -1)
                //        {
                //            zamWO.zamowienie.adres_id = adr.adres_id;
                //        }
                //    }
                //}

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
                ModelState.AddModelError("", ex.Message);
                TempData["error"] = ex.Message;
                Response.StatusCode = 400;
                Response.TrySkipIisCustomErrors = true;
                return Json(new { status = -1, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

            return PartialView(zamWO);

        }

        [HttpGet]
        [NoCache]
        public ActionResult ZamowienieAdresyKurier(ZamowienieWczytaneWO model)
        {

            ZamowienieWczytaneWO zamWO = new ZamowienieWczytaneWO();

            try
            {
                zamWO = _zamowienieRepository.PobierzAdresKurier(((CustomPrincipal)User).Identity.Sesja);
                zamWO.zamowienie = model.zamowienie;
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
                ModelState.AddModelError("", ex.Message);
                TempData["error"] = ex.Message;
                Response.StatusCode = 400;
                Response.TrySkipIisCustomErrors = true;
                return Json(new { status = -1, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

            return PartialView(zamWO);

        }

        [HttpGet]
        [NoCache]
        public ActionResult AdresyZdefiniowaneUzytkownika()
        {

            ViewBag.Message = "Adresy zdefiniowane dla użytkownika";

            try
            {
                if (TempData["Error"] != null)
                {
                    ViewBag.Error = TempData["Error"];
                }
                if (TempData["Info"] != null)
                {
                    ViewBag.Info = TempData["Info"];
                }
                if (TempData["Warning"] != null)
                {
                    ViewBag.Warning = TempData["Warning"];
                }

                return PartialView();

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
                ModelState.AddModelError("", ex.Message);
                TempData["error"] = ex.Message;
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = 400;
                return Json(new
                {
                    redirectUrl = Url.Action("Index", "Stany"),
                    isRedirect = true
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AdresyZdefiniowaneUzytkownika(DataTablesRequest DataTablesModel)
        {
            AdresyWO adresyWO = new AdresyWO();
            ViewBag.Message = "Adresy zdefiniowane tabela";

            try
            {
                string sortowanie = "";
                bool rosnaco = true;

                foreach (nclprospekt.JDataTables.Column column in DataTablesModel.Columns)
                {
                    if (column.IsOrdered == true && sortowanie == "")
                    {
                        sortowanie = column.Name;
                        rosnaco = (column.SortDirection == nclprospekt.JDataTables.Column.OrderDirection.Ascendant) ? true : false;
                        break;
                    }
                }

                string filtr = DataTablesModel.Search.Value;

                int numer_strony = (DataTablesModel.Start / DataTablesModel.Length) + 1;
                int ilosc_na_stronie = DataTablesModel.Length;

                int uzytkownikId = Convert.ToInt32(NZ(_uzytkownikRepository.GetByUserName(((CustomPrincipal)User).Identity.Sesja, User.Identity.Name).Uzytkownik_Id, 0));
                adresyWO = _adresRepository.LoadAdresLista(((CustomPrincipal)User).Identity.Sesja, uzytkownikId, numer_strony, ilosc_na_stronie, filtr, sortowanie, rosnaco);

                return Json(new nclprospekt.JDataTables.DataTablesResponse(
                                            draw: DataTablesModel.Draw,
                                            data: adresyWO.adresy,
                                            recordsTotal: adresyWO.ilosc_pozycji,
                                            recordsFiltered: adresyWO.ilosc_pozycji
                                           ));

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
                Response.StatusCode = 400;
                Response.TrySkipIisCustomErrors = true;
                ModelState.AddModelError("Error: ", ex.Message);
                return Json(new { status = -1, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        [NoCache]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ZamowienieWczytaneWO model)
        {
            RezultatObject rez = new RezultatObject();
            rez.status = 0;
            try
            {
                rez = _zamowienieRepository.SaveCartAdjustModel(((CustomPrincipal)User).Identity.Sesja, model);
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
                rez.status = -1;
                rez.message = ex.Message;
             }


            if (rez.status != 0)
            {
                Response.StatusCode = 400;
                Response.TrySkipIisCustomErrors = true;
                ModelState.AddModelError("Error: ", rez.message);
                return Json(new { status = rez.status, Message = rez.message }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Response.StatusCode = 200;
                return Json(new { status = rez.status, Message = rez.message }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Realizuj(ZamowienieWczytaneWO model)
        {
            RezultatObject rez = new RezultatObject();
            rez.status = -1;
            rez.message = "Brak odpowiedzi od serwera";
            try
            {

                rez.status = 1;
                rez = _zamowienieRepository.ZatwierdzKoszyk(((CustomPrincipal)User).Identity.Sesja, model.zamowienie.blokada_id);
            }
            catch (SesjaException sx)
            {
                ModelState.AddModelError("", sx.Message);
                ViewBag.Message = sx.Message;
                ViewBag.Status = -1;
                TempData["msg"] = sx.Message;
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                ModelState.AddModelError("error: ", ex.Message);
                return RedirectToAction("Edit", "Koszyk");
            }
            if (rez.status == 0)
            { 
           TempData[rez.statusString] = string.Format("{1} {2} Zamówienie {0} przesłano do realizacji", NZ(model.zamowienie.zamowienie_id, -1), rez.message,Environment.NewLine);
            }
            else
            {
                TempData[rez.statusString] = rez.message;

            }
           //Gdy wszystko OK
            return RedirectToAction("Index", "Stany");
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
                filterContext.ExceptionHandled = true;
            }

            base.OnException(filterContext);
        }

        #endregion
    }
}
