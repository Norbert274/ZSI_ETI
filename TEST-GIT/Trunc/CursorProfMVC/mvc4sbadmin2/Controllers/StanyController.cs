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
using nclprospekt.JDataTables;

namespace nclprospekt.Controllers
{
    [Authorize]
    [Authorize(Roles = "StanyToolStripMenuItem")]
    public class StanyController : Controller
    {

        private readonly IStanyRepository _stanyRepository;

        public StanyController(IStanyRepository stanyRepository)
        {
            _stanyRepository = stanyRepository;
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Index()
        {
            StanyWO stanyWO = new StanyWO();
            ViewBag.Message = "Stany";

            try
            {
                StanyWO listyFiltrow = _stanyRepository.LoadDictionaries(((CustomPrincipal)User).Identity.Sesja);
                stanyWO.magazyn_id = _stanyRepository.PobierzDomyslnyMagazynID(((CustomPrincipal)User).Identity.Sesja);
                
                stanyWO.marki = listyFiltrow.marki;
                stanyWO.branze = listyFiltrow.branze;
                stanyWO.grupy = listyFiltrow.grupy;
                stanyWO.kategorie = listyFiltrow.kategorie;                                               

                if (Request.IsAjaxRequest())
                {
                    return PartialView("_Stany", stanyWO);
                }
                else
                {
                    return View(stanyWO);
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
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
		[ValidateAntiForgeryToken]

        public JsonResult Index(DataTablesRequest DataTablesModel)
	   {
            StanyWO stanyWO = new StanyWO();
            ViewBag.Message = "Stany";

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
                string sku = "";
                string nazwa = DataTablesModel.Search.Value;

                int numer_strony = (DataTablesModel.Start / DataTablesModel.Length) +1;
                stanyWO = _stanyRepository.LoadStanyWO(((CustomPrincipal)User).Identity.Sesja, DataTablesModel.magazyn_id, DataTablesModel.filtryGrup, DataTablesModel.filtryMarek, DataTablesModel.filtryBranz, DataTablesModel.filtryKategorii, sku, nazwa, numer_strony, DataTablesModel.Length, sortowanie, rosnaco, DataTablesModel.czy_niezerowe, DataTablesModel.czy_tylko_nowe);
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
                  redirectUrl = Url.Action("Index", "Home"),
                  isRedirect = true
              }, JsonRequestBehavior.AllowGet);

            }

            return Json(new nclprospekt.JDataTables.DataTablesResponse(
                            draw: DataTablesModel.Draw,
                            data: stanyWO.stany,
                            recordsTotal: stanyWO.ilosc_pozycji,
                            recordsFiltered: stanyWO.ilosc_pozycji
                           ));
       }

       
        [HttpGet]
        public ActionResult ViewProductDetails(int magazyn_id =-1, int sku_id =-1, int grupa_id =-1)
        {
            try
            {
                if (magazyn_id == -1 || sku_id == -1 || grupa_id == -1)
                {
                    return RedirectToAction("Index", "Stany");
                }
                                
                ProduktDetailsWO produktDetailsWO = new ProduktDetailsWO();
                produktDetailsWO = _stanyRepository.ProductDetails(((CustomPrincipal)User).Identity.Sesja, magazyn_id, sku_id, grupa_id);

                if (produktDetailsWO.produkt == null)
                {
                     Exception ex = new Exception("Nie udało się pobrać produktu z bazy");
                    throw ex;
                };

                return PartialView(produktDetailsWO);
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

        [HttpGet]
        [NoCache]
        public ActionResult ProductPhotos(int sku_id = -1)
        {
            try
            {
                if (sku_id == -1)
                {
                    return RedirectToAction("Index", "Stany");
                }

                byte[] sesja = User.ToCustomPrincipal().Identity.Sesja;
                ProduktZdjeciaWO produktZdjeciaWO = new ProduktZdjeciaWO();
                produktZdjeciaWO = _stanyRepository.ShowGallery(sesja, sku_id);

                return PartialView(produktZdjeciaWO);
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
