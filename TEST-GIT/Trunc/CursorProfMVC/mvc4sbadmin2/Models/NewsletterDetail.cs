using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nclprospekt.Models
{
    public class NewsletterDetail
    {
        public int NewsletterID { get; set; }

        public string Tresc { get; set; }

        public List<string> UzytkonicyGrupy { get; set; }

        public List<string> UzytkownicyWielkosc { get; set; }

        public List<string> UzytkownicyNazwy { get; set; }
    }
}