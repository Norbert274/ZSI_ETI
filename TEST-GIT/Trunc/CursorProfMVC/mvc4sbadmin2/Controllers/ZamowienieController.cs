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
using System.Web.WebPages;

namespace nclprospekt.Controllers
{
    [Authorize]
      [Authorize(Roles = "ZamowieniaToolStripMenuItem")]
    public class ZamowienieController : Controller
    {
        private int _minRequiredZLLength = 8;
        private string _ZLRegularExpression = "";
        private readonly IZamowienieRepository _zamowienieRepository;

        public ZamowienieController(IZamowienieRepository zamowienieRepository)
        {
            _zamowienieRepository = zamowienieRepository;
        }

        public ActionResult Index(string DataOd = "", string DataDo = "", string filtr = "", int exExcelTypId = 0,
                                  int page = 1, int pageSize = 15, string sortBy = "data_zlozenia", string sortOrder = "DESC"
            , int zamowienieStatusID = -1)
        {

            ViewBag.Message = "Zamówienia";
            ZamowienieListaWO WO = new ZamowienieListaWO();
            string dataAkcji = DateTime.Now.Date.ToString("yyyy-MM-dd");

            if (DataOd == "") DataOd = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd"); ;
            if (DataDo == "") DataDo = dataAkcji;
            
            DateTime dtDataOd = (DataOd == "") ? DateTime.Now : DateTime.Parse(DataOd);
            DateTime dtDataDo = (DataDo == "") ? DateTime.Now : DateTime.Parse(DataDo);

            if (DataDo != "")
            {
                dtDataDo = new DateTime(dtDataDo.Year, dtDataDo.Month, dtDataDo.Day, 23, 59, 59, 997);
            };


            WO.DataOd = dtDataOd;
            WO.DataDo = dtDataDo;
            WO.filtr = filtr;

            try
            {
                WO.zamowieniaLista = _zamowienieRepository.LoadZamowienieLista(((CustomPrincipal)User).Identity.Sesja, dtDataOd, dtDataDo, filtr, zamowienieStatusID).ToList();
                WO.zamowienieStatusLista = _zamowienieRepository.LoadZamowienieStatusyLista(((CustomPrincipal)User).Identity.Sesja).ToList();
                WO.zamowienieStatusID = zamowienieStatusID;
                //byte[] sesja = ((CustomPrincipal)User).Identity.Sesja;
                return View(WO);

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
                TempData["error"] = ex.Message;
                return View();
            }
        }

        [HttpGet]
        [NoCache]
        public ActionResult ViewZamDetails(int id = -99)
            {

            ZamowienieWczytaneWO zamWO = new ZamowienieWczytaneWO();
            ZamowienieWczytaneWO zamWOAdresZdefiniowany = new ZamowienieWczytaneWO();
            ZamowienieWczytaneWO zamWOAdresKurier = new ZamowienieWczytaneWO();
            try
            {
                if (id == -99) throw new Exception("Błędny ID zamówienia!");
          
                zamWO = _zamowienieRepository.FindById(id, ((CustomPrincipal)User).Identity.Sesja);
                zamWOAdresZdefiniowany = _zamowienieRepository.PobierzAdresZdefiniowanyZamowienia(((CustomPrincipal)User).Identity.Sesja, zamWO.zamowienie.zamowienie_id, zamWO.zamowienie.adres_id);
                zamWOAdresKurier = _zamowienieRepository.PobierzAdresKurier(((CustomPrincipal)User).Identity.Sesja);
                zamWO.zamowienieZdefiniowaneAdresy = zamWOAdresZdefiniowany.zamowienieZdefiniowaneAdresy;
                zamWO.zamowienieKurierAdresy = zamWOAdresKurier.zamowienieKurierAdresy;
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
                //TempData["error"] = ex.Message; --inaczej to pojawi się na innym ekranie
                Response.StatusCode = 400;
                Response.TrySkipIisCustomErrors = true;
                return Json(new { status = -1, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

            return PartialView(zamWO);

        }


        [HttpGet]
        [NoCache]
        public ActionResult ViewAdresyZdefiniowane(ZamowienieWczytaneWO zamWo)
        {

            ZamowienieWczytaneWO zamWO = new ZamowienieWczytaneWO();

            try
            {
                zamWO = _zamowienieRepository.PobierzAdresZdefiniowanyZamowienia(((CustomPrincipal)User).Identity.Sesja,zamWo.zamowienie.zamowienie_id, zamWo.zamowienie.adres_id);

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
        public ActionResult ViewAdresyKuriera(ZamowienieWczytaneWO zamWo)
        {

            ZamowienieWczytaneWO zamWO = new ZamowienieWczytaneWO();

            try
            {
                zamWO = _zamowienieRepository.PobierzAdresKurier(((CustomPrincipal)User).Identity.Sesja);

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
        public ActionResult Create()
        {
            List<SelectListItem> zamTypList = new List<SelectListItem>();

            try
            {
                ViewBag.ZamowienieTypList = zamTypList;
                return View(new Zamowienie() { ZAMOWIENIE_TYP_ID = 0 });

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
                return RedirectToAction("Index");
            }
        }

        //
        // POST: /Uzytkownik/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Zamowienie model)//FormCollection collection)
        {

            Zamowienie zam = new Zamowienie();

            try
            {
                List<SelectListItem> zamTypList = new List<SelectListItem>();
                ViewBag.ZamowienieTypList = zamTypList;

                if (ModelState.IsValid)
                {

                    if (model.QGUAR_ZL.Length < _minRequiredZLLength)
                    {
                        //status = InvalidZL;
                        return View(model);
                    }

                    int count = 0;

                    for (int i = 0; i < model.QGUAR_ZL.Length; i++)
                    {
                        if (!char.IsLetterOrDigit(model.QGUAR_ZL, i))
                        {
                            count++;
                        }
                    }


                    if (_ZLRegularExpression.Length > 0)
                    {
                        if (!Regex.IsMatch(model.QGUAR_ZL, _ZLRegularExpression))
                        {
                            //status = InvalidZL;
                            return View(model);
                        }
                    }

                    Zamowienie z = new Zamowienie();
                    z.ZAMOWIENIE_ID = 0;//Guid.NewGuid().ToString();
                    z.WARTOSC = model.WARTOSC;
                    if (model.UWAGI != null) z.UWAGI = model.UWAGI; //Crypto.Hash(model.USER_ID);
                    z.ZSI_ID = model.ZSI_ID;

                    Zamowienie newZam = new Zamowienie();
                    //newZam = _zamowienieRepository.Create(((CustomPrincipal)User).Identity.Sesja, z);

                    if (newZam != null)
                    {

                        ModelState.Clear();
                        ViewBag.Message = "Zamówienie zostało utworzone.";

                        Zamowienie zamClean = new Zamowienie() { ZAMOWIENIE_ID = 0 };
                        return View(zamClean);
                    }
                    else
                        return View(model);
                }
                else
                {
                    Zamowienie zamClean = new Zamowienie() { ZAMOWIENIE_ID = 0 };
                    return View(zamClean);
                }
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
                return View(model);
            }
        }

        //
        // GET: /Uzytkownik/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (id <= 0) return RedirectToAction("Index");

            Zamowienie z = new Zamowienie();

            try
            {
                List<SelectListItem> zamTypList = new List<SelectListItem>();
                ViewBag.zamowienieTypList = zamTypList;

                return View(z);
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
                return RedirectToAction("Index");
            }
        }

        //
        // POST: /Uzytkownik/Edit/5
        [HttpPost]
        [NoCache]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Zamowienie model)
        {
            Zamowienie zam = new Zamowienie();

            try
            {
                List<SelectListItem> zamTypList = new List<SelectListItem>();
                ViewBag.ZamowienieTypList = zamTypList;

                if (ModelState.IsValid)
                {

                    Zamowienie z = new Zamowienie();
                    z.ZAMOWIENIE_ID = model.ZAMOWIENIE_ID;
                    z.WARTOSC = model.WARTOSC;
                    if (model.UWAGI != null) z.UWAGI = model.UWAGI; //Crypto.Hash(model.USER_ID);
                    z.ZSI_ID = model.ZSI_ID;

                    Zamowienie updatedZam = new Zamowienie();
                    //updatedZam = _zamowienieRepository.UpdateZamowienie(((CustomPrincipal)User).Identity.Sesja, z);

                    if (updatedZam != null)
                    {
                        ModelState.Clear();
                        ViewBag.Message = "Dane użytkownika zostały zaktualizowane.";
                        return View(updatedZam);
                    }
                    else
                        return View(model);
                }
                else
                {
                    Zamowienie z = new Zamowienie();
                    z.ZAMOWIENIE_ID = model.ZAMOWIENIE_ID;
                    z.WARTOSC = model.WARTOSC;
                    if (model.UWAGI != null) z.UWAGI = model.UWAGI; //Crypto.Hash(model.USER_ID);
                    z.ZSI_ID = model.ZSI_ID;
                    return View(z);
                }
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
                return View(model);
            }
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
