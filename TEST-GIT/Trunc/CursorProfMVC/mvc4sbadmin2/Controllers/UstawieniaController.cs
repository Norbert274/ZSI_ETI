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
using nclprospekt.JDataTables;
using nclprospekt.Utils;

namespace nclprospekt.Controllers
{
      [Authorize]
    public class UstawieniaController : Controller
    {
        private readonly IAdresRepository _adresRepository;
        private readonly IUzytkownikRepository _uzytkownikRepository;

        public UstawieniaController(IAdresRepository adresRepository, IUzytkownikRepository uzytkownikRepository)
        {
            _adresRepository = adresRepository;
            _uzytkownikRepository = uzytkownikRepository;
        }

        [HttpGet]
        public ActionResult Index()
        {

            ViewBag.Message = "Ustawienia";

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

                return View();

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
                return View();
            }
        }

        [HttpPost]
     	[ValidateAntiForgeryToken]
        public JsonResult Index(DataTablesRequest DataTablesModel)
        {
            AdresyWO adresyWO = new AdresyWO();
            ViewBag.Message = "Adresy zdefiniowane";

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

                int uzytkownikId = Convert.ToInt32(NZ(_uzytkownikRepository.GetByUserName(((CustomPrincipal)User).Identity.Sesja,User.Identity.Name).Uzytkownik_Id, 0));
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

        //
        // GET: /Ustawienia/Create
        [HttpGet]
        
        public ActionResult Create()
        {
            List<SelectListItem> wojewodztwaList = new List<SelectListItem>();
            AdresDetaleWO adr = new AdresDetaleWO() { adres = new Adres() { Adres_Id = 0 } }; ;
            try
            {
                ViewBag.WojewodztwaList = wojewodztwaList;
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
                return RedirectToAction("Index");
            }

                return PartialView(adr);
          
           

        }

        //
        // POST: /Ustawienia/Create
        [HttpPost]
        [ValidateAjaxAntiForgeryToken]
        public ActionResult Create(AdresDetaleWO model)//FormCollection collection)
        {
            RezultatObject rez = new RezultatObject();
            rez.status = -1;
            rez.message = "Brak odpowiedzi z serwera";

            try
            {

                if (ModelState.IsValid)
                {

                    byte[] sesja = User.ToCustomPrincipal().Identity.Sesja;
                    int uzytkownikId = Convert.ToInt32(NZ(_uzytkownikRepository.GetByUserName(sesja, User.ToCustomPrincipal().Identity.Name).Uzytkownik_Id, 0));

                    model.user_id = uzytkownikId;
                    
                    if (model.adres.Adres_Id == 0)
                    {
                    rez = _adresRepository.AdresEdytujZapisz(((CustomPrincipal)User).Identity.Sesja, model, false);
                    }
                    else
                    {
                        rez.message = "Ten adres został już dodany.";
                        rez.status = -1;
                    };

                  
                    
                }
                else
                {
                    ViewBag.Message = this.ModelState;
                    Exception ex = new Exception("Dane zawierają błąd: " + this.ModelState);
                    throw ex;
                }
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
                return Json(new { status = -1, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }


            if (rez.status ==0)
            {
                ModelState.Clear();
                model.adres.Adres_Id = -1;
            }

            ModelState.Clear();
            ViewBag.Message = rez.message;
            ViewBag.status = rez.status;
            Response.StatusCode = 200;
            return Json(new { status = rez.status, Message = rez.message }, JsonRequestBehavior.AllowGet);


        }

        //
        // GET: /Ustawienia/Edit/5
        [HttpGet]
        [NoCache]
        public ActionResult Edit(int id =-1)
        {

            if (id <= 0) return RedirectToAction("Index");


            AdresDetaleWO adr = (new AdresDetaleWO() { adres = new Adres() { Adres_Id = id } });
            List<SelectListItem> wojewodztwaList = new List<SelectListItem>();
           
            try
            {
                byte[] sesja = ((CustomPrincipal)User).Identity.Sesja;
                adr = _adresRepository.AdresEdytuj(sesja, id);

                    ViewBag.WojewodztwaList = wojewodztwaList;

                    return PartialView(adr);
           


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
                ModelState.AddModelError("Error: ", ex.Message);
                Response.StatusCode = 400;
                Response.TrySkipIisCustomErrors = true;
                return Json(new { status = -1, Message = ex.Message }, JsonRequestBehavior.AllowGet);

            }
        }

        //
        // POST: /Ustawienia/Edit/5
        [HttpPost]
        [Authorize]
        [ValidateAjaxAntiForgeryToken]
        public ActionResult Edit(AdresDetaleWO model)
        {
            RezultatObject rez = new RezultatObject();
            rez.status = -1;
            rez.message = "Brak odpowiedzi z serwera";
           
            try
            {

                byte[] sesja = User.ToCustomPrincipal().Identity.Sesja;
                int uzytkownikId = Convert.ToInt32(NZ(_uzytkownikRepository.GetByUserName(sesja, User.ToCustomPrincipal().Identity.Name).Uzytkownik_Id, 0)); 
                
                    model.user_id = uzytkownikId;
                
                    if (model.blokada_id != -1)
                    {
                    rez = _adresRepository.AdresEdytujZapisz(((CustomPrincipal)User).Identity.Sesja, model, false);
                    };

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
                    ModelState.AddModelError("Error: ", ex.Message);
                    Response.StatusCode = 400;
                    Response.TrySkipIisCustomErrors = true;
                return Json(new { status = -1, Message = ex.Message }, JsonRequestBehavior.AllowGet);
                
                }

            ModelState.Clear();
            ViewBag.Message = rez.message;
            ViewBag.status = rez.status;
            
            
            Response.StatusCode = 200;
            return Json(new { status = rez.status, Message = rez.message }, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Ustawienia/Delete
        [HttpGet]
        public ActionResult Delete(int id = -1)
        {

            if (id <= 0) return RedirectToAction("Index");

            AdresDetaleWO adr = (new AdresDetaleWO() { adres = new Adres() { Adres_Id = id } });
            List<SelectListItem> wojewodztwaList = new List<SelectListItem>();

            try
            {
                adr = _adresRepository.AdresEdytuj(((CustomPrincipal)User).Identity.Sesja, id);
                ViewBag.WojewodztwaList = wojewodztwaList;
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
                    ModelState.AddModelError("Error: ", ex.Message);
                    Response.StatusCode = 400;
                    Response.TrySkipIisCustomErrors = true;
                return Json(new { status = -1, Message = ex.Message }, JsonRequestBehavior.AllowGet);

            }

                    return PartialView(adr);
      
        }
        
        // POST: /Ustawienia/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(AdresDetaleWO model)
        {
            RezultatObject rez = new RezultatObject();
            rez.status = -1;
            rez.message = "Brak odpowiedzi z serwera";

            try
            {

                    if (model.adres.Adres_Id > 0)
                    {
                        rez = _adresRepository.AdresUsun(((CustomPrincipal)User).Identity.Sesja, model.adres.Adres_Id);
                    }
                    else
                        {
                            ModelState.AddModelError("", "Brak ID adresu do usunięcia");
                     
                    };

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
                return Json(new { status = -1, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

            ModelState.Clear();
            ViewBag.Message = rez.message;
            ViewBag.status = rez.status;

            Response.StatusCode = 200;
            return Json(new { status = rez.status, Message = rez.message }, JsonRequestBehavior.AllowGet);
            }

        [HttpGet]
        public ActionResult MiastoDlaKoduPocztowego(string kod = "")
        {
            RezultatObject rez = new RezultatObject();
            rez.status = 0;
            rez.message = "OK";
            string miasto = "";
            if (kod.Length == 6)
            {
                IEnumerable<Adres> adr = null;

                try
                {
                    byte[] sesja = ((CustomPrincipal)User).Identity.Sesja;
                    adr = _adresRepository.MiastoDlaKodu(sesja, kod);
                    if (adr.Count() > 0)
                    { 
                    Adres pierwszyAdres = adr.FirstOrDefault();
                    miasto = pierwszyAdres.Miasto;
                    }
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
                    ModelState.AddModelError("Error: ", ex.Message);
                    Response.StatusCode = 400;
                    Response.TrySkipIisCustomErrors = true;
                    return Json(new { status = -1, Message = ex.Message }, JsonRequestBehavior.AllowGet);

                }
            }

            ModelState.Clear();
            ViewBag.Message = rez.message;
            ViewBag.status = rez.status;
            Response.StatusCode = 200;
            return Json(new { status = rez.status, Message = rez.message, Miasto = miasto }, JsonRequestBehavior.AllowGet);

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
