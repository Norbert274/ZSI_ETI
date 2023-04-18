using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace nclprospekt.Models
{
    public class Adresaci
    {
       public List<Adresat> adresaci;
    }

    public class Adresat
    {
        [Display(Name="Imie i Nazwisko")]
        public string Nazwa { get; set; }

        [Display(Name = "Imie")]
        public string Imie { get; set; }

        [Display(Name = "Nazwisko")]
        public string Nazwisko { get; set; }
        public int UserId { get; set; }

        public bool Wybierz { get; set; }

        [Display(Name = "Grupa")]
        public string GrupaNazwa { get; set; }
        public int GrupaId { get; set; }

        [Display(Name = "Typ")]
        public string TypNazwa { get; set; }
        public int TypId { get; set; }

        [Display(Name = "Wielkosc")]
        public string WielkoscNazwa { get; set; }
        public int WielkoscId { get; set; }

    }

    public class AdresatViewModel
    {
        public bool Wybierz { get; set; }
        public int UserId { get; set; }
        public string Nazwa { get; set; }
    }

}