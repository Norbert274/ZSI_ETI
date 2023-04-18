using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace nclprospekt.Models
{
    public class DashboardWO
    {
        [Display(Name = "Złożone zamówienia")]
        public int IloscZamowien { get; set; }
        [Display(Name = "Pozycje w koszyku")]
        public int IloscWKoszyku { get; set; }
        [Display(Name = "Liczba produktów")]
        public int IloscProduktow { get; set; }
        [Display(Name = "Zdefiniowane adresy")]
        public int IloscAdresow { get; set; }
        public virtual IList<DashboardWykres> daneWykres { get; set; }
    }

    public class DashboardWykres
    {
        public string miesiac {get;set;}
        public int ZamowienRealizowanych { get; set; }
        public int ZamowienRoboczych { get; set; }
    }
}