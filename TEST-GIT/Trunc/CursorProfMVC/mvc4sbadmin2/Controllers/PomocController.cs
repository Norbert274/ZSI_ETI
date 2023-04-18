using System;
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
    public class PomocController : Controller
    {


        private readonly IPomocRepository _pomocRepository;
        public PomocController( IPomocRepository pomocRepository)
        {
            _pomocRepository = pomocRepository;
        }

        [HttpGet]
        public ActionResult Help()
        {
            ViewBag.Message = "...";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "...";
            ViewBag.PokazKontaktDoIt = false;
            KontaktDaneDodatkowe daneDodatkowe = _pomocRepository.PobierzParametrPoNazwie(((CustomPrincipal)User).Identity.Sesja, "KONTAKT_TELEFONICZNY_KLIENT");

            if (daneDodatkowe != null && daneDodatkowe.wartoscParametru != null)
            {
                ViewBag.KontaktKlient = daneDodatkowe.wartoscParametru;
            }


            return View();
        }

        [HttpPost]
        [NoCache]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(KontaktWO model)
        {
            RezultatObject rez = new RezultatObject();
            rez.status = 0;
            rez.message = "Brak odpowiedzi od serwera";
            try
            {
                string nazwaPliku = "";
                if (model.Plik != null)
                {
                    //Dodawanie TimeStamp w celu zachowania unikalnosci nazw plikow
                    nazwaPliku = model.Plik.FileName;
                    nazwaPliku = string.Concat(System.IO.Path.GetFileNameWithoutExtension(nazwaPliku), DateTime.Now.ToString("yyyyMMddHHmmss"), System.IO.Path.GetExtension(nazwaPliku));

                    byte[] plik;
                    using (System.IO.Stream inputStream = model.Plik.InputStream)
                    {
                        System.IO.MemoryStream memoryStream = inputStream as System.IO.MemoryStream;
                        if (memoryStream == null)
                        {
                            memoryStream = new System.IO.MemoryStream();
                            inputStream.CopyTo(memoryStream);
                        }
                        plik = memoryStream.ToArray();
                    }
                    rez = _pomocRepository.SaveAttachment(((CustomPrincipal)User).Identity.Sesja, nazwaPliku, plik);
                }
                rez = _pomocRepository.SendMail(((CustomPrincipal)User).Identity.Sesja, model.Tytul, string.Format("Ekran: [{0}] Treść: {1}", model.NazwaEkranu, model.Tresc), nazwaPliku, model.zrodloMaila);


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
                return View(model); //Zwracam co dostałem - żeby drugi raz nie wypełniali tego samego - (uwaga - wypełni się wszędzie)
            }


            if (rez.status == 0)
            {
                TempData[rez.statusString] = string.Format("Twoja wiadomość została wysłana");
            }
            else
            {
                TempData[rez.statusString] = rez.message;

            }
            //Gdy wszystko OK
            return RedirectToAction("Contact", "Pomoc");

        }

        [HttpGet]
        public ActionResult FilesDownload()
        {
            PomocPlikiListaWO model = new PomocPlikiListaWO();
            RezultatObject rez = new RezultatObject();
            rez.status = 0;
            try
            {
                model = _pomocRepository.PobierzListePlikow(((CustomPrincipal)User).Identity.Sesja);
                rez.status = 0;
                rez.message = "Pobrano pliki";
            }
            catch (SesjaException sx)
            {
                rez.status = -1;
                rez.message = sx.Message;
                TempData[rez.statusString] = rez.message;
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                rez.status = -1;
                rez.message = ex.Message;
                TempData[rez.statusString] = rez.message;
                return RedirectToAction("Index","Home");
            }


            return View(model);
        }

        [HttpGet]
        [NoCache]
        public ActionResult Download(string nazwaPliku) 
        {
            //FileContentResult - inherited from ActionResult
            RezultatObject rez = new RezultatObject();
            rez.status = 0;
            rez.message = "Brak odpowiedzi od serwera";

            PomocPobierzPlik wo = new PomocPobierzPlik();


            byte[] fileContent = null;
            string mimeType = "";
            string fileName = "";
            string extension = "";

            try
            {
                extension = System.IO.Path.GetExtension(nazwaPliku);
                mimeType = nclprospekt.DAL.Helpers.GetMimeType(extension);

                if (mimeType == "application/octet-stream")
                {
                    throw new Exception("Plik jest w niedozwolonym formacie! Zablokowano próbę pobrania pliku");
                }

                wo = _pomocRepository.PobierzPlik(((CustomPrincipal)User).Identity.Sesja, nazwaPliku);

                fileContent = wo.plik;
                fileName = wo.NazwaPliku;

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

                return RedirectToAction("FilesDownload", "Pomoc");
            }

            return File(fileContent, mimeType, fileName);

        }


        #region "Error handle"

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception is HttpAntiForgeryException)
            {

                this.RedirectToAction("Index", "Pomoc", null);

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
