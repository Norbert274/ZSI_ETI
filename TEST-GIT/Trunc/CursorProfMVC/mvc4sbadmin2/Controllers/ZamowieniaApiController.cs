using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using nclprospekt.Models;
using System.Data;
using nclprospekt.Repository;
using js = Newtonsoft.Json;
using nclprospekt.Utils;
using System.Web;
using System.Text;
using nclprospekt.Exceptions;
using nclprospekt.ControllersHelper;

namespace nclprospekt.Controllers
{
    [Authorize(Roles = "ZamowieniaToolStripMenuItem")]
    public class ZamowieniaApiController : ApiController
    {

        private readonly IZamowienieRepository _zamowienieRepository;

        public ZamowieniaApiController(IZamowienieRepository zamowienieRepository)
        {
            _zamowienieRepository = zamowienieRepository;
        }

        // POST api/zamowieniaapi
        [HttpPost]
        [ValidateAjaxAntiForgeryToken]
        public HttpResponseMessage Post([FromBody] ProduktDetailsWO productDetailsWO) //Dodaj pozycje do koszyka
        {
            //Dopisanie pozycji do aktualnego koszyka (trzeba przeslac)
            //'&sku_id[0]=sku12345&ilosc[0]=0&grupa[0]=nazwagrupy|&sku_id[1]=sku12346&ilosc[1]=0&grupa[1]=nazwagrupy|";
            //ResztaBranaZZamowienia
            RezultatObject rez = new RezultatObject();

            try
            {
                ProductValidator.validateProduktDetailsWO(productDetailsWO);

                rez = _zamowienieRepository.AddToCart(((CustomPrincipal)HttpContext.Current.User).Identity.Sesja, productDetailsWO);

            }
            catch (SesjaException sx)
            {
                rez.message = sx.Message;
                string url = Url.Route("Default", new { Controller = "Account", Action = "Login" });
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { redirectUrl = url, isRedirect = true }, Configuration.Formatters.JsonFormatter);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { status = -1, Message = ex.Message }, Configuration.Formatters.JsonFormatter);
            }

            if (rez.status == 0)
            {
                rez.message = "Poprawnie dodano pozycję do koszyka";
            }

            return Request.CreateResponse(HttpStatusCode.OK, new { status = rez.status, Message = rez.message }, Configuration.Formatters.JsonFormatter);

        }
    }

}
