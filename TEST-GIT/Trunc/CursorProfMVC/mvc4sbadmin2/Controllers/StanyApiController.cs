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

namespace nclprospekt.Controllers
{
    [Authorize]
     [Authorize(Roles = "StanyToolStripMenuItem")]
    public class StanyApiController : ApiController
    {

        private readonly IStanyRepository _stanRepository;

        public StanyApiController(IStanyRepository stanRepository)
        {
            _stanRepository = stanRepository;
        }



        #region "Error handle"



        #endregion
    }
}
