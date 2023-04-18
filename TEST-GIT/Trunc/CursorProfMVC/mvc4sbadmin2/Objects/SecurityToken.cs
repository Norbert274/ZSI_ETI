using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nclprospekt.Objects
{
    public class SecurityToken
    {
        public byte[] token
        {
            get;
            set;
        }

        public int czyPierwszy
        {
            get;
            set;
        }
    }
}