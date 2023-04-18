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
using nclprospekt.Exceptions;
using System.Text;

namespace nclprospekt.Controllers
{
    public class UstawieniaApiController : ApiController
    {

        private readonly IAdresRepository _adresRepository;
        private readonly IUzytkownikRepository _uzytkownikRepository;

        public UstawieniaApiController(IAdresRepository adresRepository, IUzytkownikRepository uzytkownikRepository)
        {
            _adresRepository = adresRepository;
            _uzytkownikRepository = uzytkownikRepository;
        }


        //Zdejmowanie blokady//
        [Authorize]
        [ValidateAjaxAntiForgeryToken]
        [NoCache]
        public HttpResponseMessage Post([FromBody] string value) //blokada_id= )
        {
            string[] elementy = null;
            if (value == null) throw new HttpResponseException(HttpStatusCode.BadRequest);
            RezultatObject rez = new RezultatObject();
            rez.status = -1;
            rez.message = "Brak odpowiedzi z serwera";
            try
            {
                //Dzielimy input string....
                elementy = value.Split('=');
                if (elementy.Length == 2)
                {
                    
                    if (elementy[0] == "blokada_id")
                    {
                        int blokada_id = Convert.ToInt32(elementy[1]);

                        rez = _adresRepository.AdresEdytujAnuluj(((CustomPrincipal)HttpContext.Current.User).Identity.Sesja, blokada_id);
                    }
                                        
                }
                else
                {
                    rez.status = -1;
                    rez.message = "Dane blokady w nieprawidłowym formacie";
                }
                
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

            return Request.CreateResponse(HttpStatusCode.OK, new { status = rez.status, Message = rez.message }, Configuration.Formatters.JsonFormatter);

        }



        // PUT api/zamowieniaapi/5
        [Authorize]
        public void Put(int id, [FromBody]string value)
        {
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
