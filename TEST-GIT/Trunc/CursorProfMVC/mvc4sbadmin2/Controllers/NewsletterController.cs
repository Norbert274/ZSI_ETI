using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using nclprospekt.Models;
using nclprospekt.Repository;
using PagedList;

namespace nclprospekt.Controllers
{
    public class NewsletterController : Controller
    {

        private INewsletterRepository _newsletterRepo;
        private INewsletterDetailRepository _newsDetRepo;
        public IAdresaciRepository _adresaciRepo;

        public NewsletterController(INewsletterRepository newsletterRepo
            , INewsletterDetailRepository newsDetRepo,IAdresaciRepository adresaciRepo)
        {
            this._newsletterRepo = newsletterRepo;
            this._newsDetRepo = newsDetRepo;
            _adresaciRepo = adresaciRepo;
        }

        // GET: /Newsletter/
        public ActionResult Index(int page=1,int pageSize=10)
        {
            ViewBag.Message = TempData["Message"];
            ViewBag.Mode = TempData["Mode"];
           
            var newsletters=_newsletterRepo.listaNewsletter(((CustomPrincipal)User).Identity.Sesja);

            PagedList<Newsletter> model = new PagedList<Newsletter>(newsletters.Reverse(), page, pageSize);

            return View(model);
        }

        // GET: /Newsletter/Details/5

        public ActionResult Details(int id)
        {
            return View(_newsDetRepo.pobierzNewsletterDetail(((CustomPrincipal)User).Identity.Sesja,id));
        }

        // GET: /Newsletter/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Newsletter newsletter)
        {
            if (ModelState.IsValid)
            {

               var result = _newsletterRepo.zapiszNewsletter(((CustomPrincipal)User).Identity.Sesja, newsletter);

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

                return RedirectToAction("Index");

            }

            return View();
        }


        [HttpGet]
        public ActionResult Adresaci()
        {
            return View(_adresaciRepo.pobierzAdresatow(((CustomPrincipal)User).Identity.Sesja).adresaci);
        }

        [HttpPost]
        public ActionResult Adresaci(IEnumerable<AdresatViewModel>  adresaci)
        {
            Newsletter newsletter = new Newsletter();

            newsletter.Odbiorcy = adresaci.Where(x => x.Wybierz).Select(x => x.Nazwa).ToList();

            return View("Create", newsletter);
        }

    }
}
