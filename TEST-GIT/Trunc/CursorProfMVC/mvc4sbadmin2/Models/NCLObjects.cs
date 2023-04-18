using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nclprospekt.Models
{
    enum StatusValueEnum
    {
        error = -1,
        success = 0,
        warning = 1

    }

    public class RezultatObject
    {
        public int status { get; set; }
        public string message { get; set; }
        public int blokada_id { get; set; }
        public int rekord_id { get; set; }
       // private string _statusString = string.Empty;
        public string statusString
        {
            get
            {
                return ((StatusValueEnum)status).ToString();
            }
        }
    }


}