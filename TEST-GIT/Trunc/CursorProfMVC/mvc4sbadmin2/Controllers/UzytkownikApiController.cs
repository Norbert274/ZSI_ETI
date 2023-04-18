
using nclprospekt.Models;
using nclprospekt.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace nclprospekt.Controllers
{
    public class UzytkownikApiController : ApiController
    {
        private readonly IUzytkownikRepository _uzytkownikRepository;

        public UzytkownikApiController(IUzytkownikRepository uzytkownikRepository)
        {
            _uzytkownikRepository = uzytkownikRepository;
        }

        // GET api/uzytkownikapi
        public HttpResponseMessage Get()
        {
            IEnumerable<UzytkownikDoListy> uzytkownicy;
            try 
	        {
                uzytkownicy = _uzytkownikRepository.GetUsers(((CustomPrincipal)HttpContext.Current.User).Identity.Sesja,0);
	        }
	        catch (Exception ex)
	        {
		        return Request.CreateResponse(HttpStatusCode.BadRequest, new { status = -1, Message = ex.Message }, Configuration.Formatters.JsonFormatter);
	        }


            return Request.CreateResponse(HttpStatusCode.OK, uzytkownicy.ToList(), Configuration.Formatters.JsonFormatter);
        }

        // GET api/uzytkownikapi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/uzytkownikapi
        public void Post([FromBody]string value)
        {
        }

        // PUT api/uzytkownikapi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/uzytkownikapi/5
        public void Delete(int id)
        {
        }
    }
}
