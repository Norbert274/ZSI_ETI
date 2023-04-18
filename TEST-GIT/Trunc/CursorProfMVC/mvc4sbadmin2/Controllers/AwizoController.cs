using nclprospekt.Exceptions;
using nclprospekt.JDataTables;
using nclprospekt.Models;
using nclprospekt.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using nclprospekt.Utils;

namespace nclprospekt.Controllers
{
    [Authorize]
    [Authorize(Roles = "AwizacjeToolStripMenuItem")]
    public class AwizoController : Controller
    {
        private readonly IAwizoRepository _awizoRepository;
        private readonly IUzytkownikRepository _uzytkownikRepository;

        public AwizoController(IAwizoRepository awizoRepository, IUzytkownikRepository uzytkownikRepository)
        {
            _awizoRepository = awizoRepository;
            _uzytkownikRepository = uzytkownikRepository;
        }

        [HttpGet]
        [NoCache]
        public ActionResult Index()
        {
            AwizoListaWO awizaListaWO = new AwizoListaWO();
            ViewBag.Message = "Awiza lista";

            try
            {
                CustomPrincipal user = User.ToCustomPrincipal();
                byte[] sesja = user.Identity.Sesja;
                int uzytkownikId = Convert.ToInt32(NZ(_uzytkownikRepository.GetByUserName(sesja, User.Identity.Name).Uzytkownik_Id, 0));

                DateTime data_planowana_dostawy_od = DateTime.Today.AddYears(-1);
                DateTime data_planowana_dostawy_do = DateTime.Today.AddYears(-1);
                DateTime data_utworzenia_do = DateTime.Today;
                DateTime data_utworzenia_od = DateTime.Today.AddDays(-14);
                string nr_awiza = "NIE_ZWRACAJ_NIC";
                string nr_po = "NIE_ZWRACAJ_NIC";
                string dostawca = "NIE_ZWRACAJ_NIC";
                string qguar_za = "NIE_ZWRACAJ_NIC";
                string qguar_dostawa = "NIE_ZWRACAJ_NIC";
                string sortowanie = "";
                bool rosnaco = true;
                int numer_strony = 0;
                int ilosc_na_stronie = 0;
                string strXmlStatusy = "";
                string sku = "";
                
                int user_id = Convert.ToInt32(NZ(_uzytkownikRepository.GetByUserName(sesja, User.ToCustomPrincipal().Identity.Name).Uzytkownik_Id, 0));
                

                awizaListaWO = _awizoRepository.AwizaLista(sesja,
                              data_utworzenia_od, data_utworzenia_do,
                              data_planowana_dostawy_od, data_planowana_dostawy_do,
                              nr_awiza, nr_po, dostawca, qguar_za,
                              qguar_dostawa, sortowanie, numer_strony, ilosc_na_stronie, rosnaco, strXmlStatusy, user_id, sku);

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

                ViewBag.dataDostawyOd = DateTime.Today.AddYears(-1).ToString("yyyy-MM-dd");
                ViewBag.dataDostawyDo = DateTime.Today.AddDays(14).ToString("yyyy-MM-dd");
                ViewBag.dataUtworzeniaDo = DateTime.Today.ToString("yyyy-MM-dd");
                ViewBag.dataUtworzeniaOd = DateTime.Today.AddDays(-14).ToString("yyyy-MM-dd");

                return View(awizaListaWO);

            }
            catch (SesjaException sx)
            {
                ModelState.AddModelError("", sx.Message);
                TempData["msg"] = sx.Message;
                return RedirectToAction("Login", "Account", new { @returnUrl = "/Awizo/Index" });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(awizaListaWO);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Index(DataTablesRequest DataTablesModel)
        {
            AwizoListaWO awizaListaWO = new AwizoListaWO();
            ViewBag.Message = "Awiza lista";

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
                string sku = "";

                int numer_strony = (DataTablesModel.Start / DataTablesModel.Length) + 1;
                int ilosc_na_stronie = DataTablesModel.Length;

                // int uzytkownikId = Convert.ToInt32(NZ(_uzytkownikRepository.GetByUserName(((CustomPrincipal)User).Identity.Sesja, User.Identity.Name).Uzytkownik_Id, 0));
                DateTime data_planowana_dostawy_od = DateTime.Parse((DataTablesModel.data_planowana_dostawy_od == null) ? DateTime.Today.AddYears(-1).ToString() : DataTablesModel.data_planowana_dostawy_od);
                DateTime data_planowana_dostawy_do = DateTime.Parse((DataTablesModel.data_planowana_dostawy_do == null) ? DateTime.Today.AddDays(14).ToString() : DataTablesModel.data_planowana_dostawy_do);
                DateTime data_utworzenia_do = DateTime.Parse((DataTablesModel.data_utworzenia_do == null) ? DateTime.Today.ToString() : DataTablesModel.data_utworzenia_do);
                DateTime data_utworzenia_od = DateTime.Parse((DataTablesModel.data_utworzenia_od == null) ? DateTime.Today.AddDays(-14).ToString() : DataTablesModel.data_utworzenia_od);

                
                byte[] sesja = User.ToCustomPrincipal().Identity.Sesja;
                int user_id = Convert.ToInt32(NZ(_uzytkownikRepository.GetByUserName(sesja, User.ToCustomPrincipal().Identity.Name).Uzytkownik_Id, 0));
                

                awizaListaWO = _awizoRepository.AwizaLista(sesja,
                data_utworzenia_od, data_utworzenia_do,
                data_planowana_dostawy_od, data_planowana_dostawy_do,
                DataTablesModel.nr_awiza, DataTablesModel.nr_po, DataTablesModel.dostawca, DataTablesModel.qguar_za,
                DataTablesModel.qguar_dostawa, sortowanie, numer_strony, ilosc_na_stronie, rosnaco, DataTablesModel.strXmlStatusy, user_id,sku);

                return Json(new nclprospekt.JDataTables.DataTablesResponse(
                                            draw: DataTablesModel.Draw,
                                            data: awizaListaWO.awizaLista,
                                            recordsTotal: awizaListaWO.ilosc_pozycji,
                                            recordsFiltered: awizaListaWO.ilosc_pozycji
                                           ));

            }

            catch (SesjaException sx)
            {
                ModelState.AddModelError("", sx.Message);
                TempData["msg"] = sx.Message;

                Response.StatusCode = 403;
                return Json(new
                {
                    redirectUrl = Url.Action("Index", "Home"),
                    isRedirect = true
                });//Powróci do indexu który zwróci właściwy błąd

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                TempData["error"] = ex.Message;
                Response.StatusCode = 400;
                return Json(new
                {
                    redirectUrl = Url.Action("Index", "Awizo"),
                    isRedirect = true
                });
            }

        }

        [HttpGet]
        [NoCache]
        public ActionResult Preview(int id = -1)
        {
            AwizoWO awizoWO = new AwizoWO();
            AwizoWO slowniki = new AwizoWO();

            ViewBag.Message = "Awizo podgląd";
            int awizo_id = id;
            try
            {
                byte[] sesja = ((CustomPrincipal)User).Identity.Sesja;
                awizoWO = _awizoRepository.AwizoPodgladWczytaj(sesja, awizo_id);
              
            }
            catch (SesjaException sx)
            {
                ModelState.AddModelError("", sx.Message);
                TempData["msg"] = sx.Message;
                return RedirectToAction("Login", "Account", new { @returnUrl = "/Awizo/Index" });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                TempData["error"] = ex.Message;
                Response.StatusCode = 400;
                Response.TrySkipIisCustomErrors = true;
                return Json(new { status = -1, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

            return PartialView(awizoWO);
        }


        [HttpGet]
        [NoCache]
        public ActionResult Edit(int id = -1)
        {
            AwizoWO awizoWO = new AwizoWO();
            AwizoWO slowniki = new AwizoWO();

            ViewBag.Message = "Awizo edycja";
            int awizo_id = id;
            try
            {
                byte[] sesja = ((CustomPrincipal)User).Identity.Sesja;
                awizoWO = _awizoRepository.AwizoEdycjaWczytaj(sesja, awizo_id);
                slowniki = _awizoRepository.AwizoDostawcyLista(sesja);
                awizoWO.dostawca = new AwizoDostawca();
                awizoWO.dostawcyLista = slowniki.dostawcyLista;
                awizoWO.grupyLista = slowniki.grupyLista;

                if (awizoWO.awizo.PLANOWANA_DATA_DOSTAWY  == null || awizoWO.awizo.PLANOWANA_DATA_DOSTAWY < DateTime.Parse("1902-01-01"))
                {
                    awizoWO.awizo.PLANOWANA_DATA_DOSTAWY = DateTime.Today;
                }

                if (awizoWO.awizo.DOSTAWCA_ID > 0)
                {
                    AwizoDostawca dostawcaSzczegoly = _awizoRepository.AwizoDostawcaSzczegoly(sesja, awizoWO.awizo.DOSTAWCA_ID);
                    awizoWO.awizo.DOSTAWCA_KOD = dostawcaSzczegoly.KOD;
                    awizoWO.awizo.DOSTAWCA_KRAJ = dostawcaSzczegoly.KRAJ;
                    awizoWO.awizo.DOSTAWCA_MIASTO = dostawcaSzczegoly.MIASTO;
                    awizoWO.awizo.DOSTAWCA_NAZWA = dostawcaSzczegoly.NAZWA;
                }

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

            }
            catch (SesjaException sx)
            {
                ModelState.AddModelError("", sx.Message);
                TempData["msg"] = sx.Message;
                return RedirectToAction("Login", "Account", new { @returnUrl = "/Awizo/Index" });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                TempData["error"] = ex.Message;
                Response.StatusCode = 400;
                Response.TrySkipIisCustomErrors = true;
                return Json(new { status = -1, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

            return PartialView(awizoWO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AwizoWO model)
        {
            RezultatObject rez = new RezultatObject();
            rez.status = 0;
            rez.message = "Nie wykonano operacji";

            try
            {
                byte[] sesja = ((CustomPrincipal)User).Identity.Sesja;
                rez = _awizoRepository.AwizoEdycjaZapisz(sesja, model);
                if (rez.status != 0)
                {
                    ModelState.AddModelError("Error: ", rez.message);
                    Response.StatusCode = 400;
                    Response.TrySkipIisCustomErrors = true;
                    return Json(new { status = rez.status, Message = rez.message }, JsonRequestBehavior.AllowGet);
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

            Response.StatusCode = 200;
            return Json(new { status = rez.status, Message = rez.message, rekord_id = rez.rekord_id }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Realizuj(AwizoWO model)
        {
            RezultatObject rez = new RezultatObject();
            rez.status = 0;
            rez.message = "Nie wykonano operacji";

            try
            {
                byte[] sesja = ((CustomPrincipal)User).Identity.Sesja;
                rez = _awizoRepository.AwizoRealizuj(sesja, model);
                if (rez.status != 0)
                {
                    Response.StatusCode = 400;
                    Response.TrySkipIisCustomErrors = true;
                    return Json(new { status = rez.status, Message = rez.message }, JsonRequestBehavior.AllowGet);
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
            TempData["success"] = "Awizo przesłano do realizacji";
            Response.TrySkipIisCustomErrors = true;
            Response.StatusCode = 403;
            return Json(new
            {
                redirectUrl = Url.Action("Index", "Awizo"),
                isRedirect = true
            }, JsonRequestBehavior.AllowGet);


        }

        [HttpGet]
        [NoCache]
        public ActionResult DostawcaSzczegoly(int awizo_DOSTAWCA_ID = -1)
        {
            AwizoWO awizoWO = new AwizoWO();
            AwizoWO slowniki = new AwizoWO();
            awizoWO.awizo = new Awizo();
            ViewBag.Message = "Dostawca szczególy";

            try
            {
                byte[] sesja = ((CustomPrincipal)User).Identity.Sesja;
                slowniki = _awizoRepository.AwizoDostawcyLista(sesja);

                awizoWO.dostawcyLista = slowniki.dostawcyLista;
                awizoWO.dostawca = _awizoRepository.AwizoDostawcaSzczegoly(sesja, awizo_DOSTAWCA_ID);

                foreach (AwizoDostawca dost in awizoWO.dostawcyLista)
                {
                    if (dost.DOSTAWCA_ID == awizo_DOSTAWCA_ID)
                    {
                        awizoWO.dostawca.NAZWA = dost.NAZWA;
                        break;
                    }
                }
                awizoWO.dostawca.DOSTAWCA_ID = awizo_DOSTAWCA_ID;
                awizoWO.awizo.DOSTAWCA_NAZWA = awizoWO.dostawca.NAZWA;
                awizoWO.awizo.DOSTAWCA_KOD = awizoWO.dostawca.KOD;
                awizoWO.awizo.DOSTAWCA_KRAJ = awizoWO.dostawca.KRAJ;
                awizoWO.awizo.DOSTAWCA_MIASTO = awizoWO.dostawca.MIASTO;
                awizoWO.awizo.DOSTAWCA_NAZWA = awizoWO.dostawca.NAZWA;

            }
            catch (SesjaException sx)
            {
                ModelState.AddModelError("", sx.Message);
                TempData["msg"] = sx.Message;
                return RedirectToAction("Login", "Account", new { @returnUrl = "/Awizo/Index" });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error: ", ex.Message);
                Response.StatusCode = 400;
                Response.TrySkipIisCustomErrors = true;
                return Json(new { status = -1, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

            return PartialView(awizoWO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DostawcaSzczegoly(AwizoDostawca dostawca)
        {

            RezultatObject rez = new RezultatObject();
            try
            {
                byte[] sesja = ((CustomPrincipal)User).Identity.Sesja;
                if (dostawca.KRAJ == null || dostawca.KRAJ.ToString() == "")
                {
                    dostawca.KRAJ = "PL";
                }
                rez = _awizoRepository.AwizoDostawcaZapisz(sesja, dostawca);

            }
            catch (SesjaException sx)
            {
                ModelState.AddModelError("", sx.Message);
                TempData["msg"] = sx.Message;
                return RedirectToAction("Login", "Account", new { @returnUrl = "/Awizo/Index" });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                TempData["msg"] = ex.Message;
                return RedirectToAction("Index", "Awizo");
            }


            if (dostawca.DOSTAWCA_ID > 0)
            {
                return RedirectToAction("DostawcaSzczegoly", "Awizo", new { awizo_DOSTAWCA_ID = dostawca.DOSTAWCA_ID });
            }
            else
            {
                return RedirectToAction("DostawcaSzczegoly", "Awizo");
            }


        }

        [HttpPost]
        [ValidateAjaxAntiForgeryToken] //Uwaga ajax!
        public ActionResult DostawcaDelete(int awizo_DOSTAWCA_ID)
        {

            RezultatObject rez = new RezultatObject();
            try
            {
                byte[] sesja = ((CustomPrincipal)User).Identity.Sesja;

                rez = _awizoRepository.AwizoDostawcaUsun(sesja, awizo_DOSTAWCA_ID);

            }
            catch (SesjaException sx)
            {
                ModelState.AddModelError("", sx.Message);
                TempData["msg"] = sx.Message;
                return RedirectToAction("Login", "Account", new { @returnUrl = "/Awizo/Index" });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error: ", ex.Message);
                Response.StatusCode = 400;
                Response.TrySkipIisCustomErrors = true;
                return Json(new { status = -1, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

            return RedirectToAction("DostawcaSzczegoly", "Awizo", new { awizo_DOSTAWCA_ID = -1 });

        }

        [HttpGet]
		[NoCache]
        public ActionResult ProduktyLista()
        {
            AwizoWO awizoWOProdukty = new AwizoWO();
                        
            try
            {
                byte[] sesja = ((CustomPrincipal)User).Identity.Sesja;
                awizoWOProdukty = _awizoRepository.AwizoSKULista(sesja);
            }
            catch (SesjaException sx)
            {
                ModelState.AddModelError("", sx.Message);
                TempData["msg"] = sx.Message;
                return RedirectToAction("Login", "Account", new { @returnUrl = "/Awizo/Index" });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                TempData["error"] = ex.Message;
                Response.StatusCode = 400;
                Response.TrySkipIisCustomErrors = true;
                return Json(new { status = -1, Message = ex.Message }, JsonRequestBehavior.AllowGet);
               // return RedirectToAction("Index", "Awizo");
            }

            return PartialView(awizoWOProdukty);
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

    }
}
