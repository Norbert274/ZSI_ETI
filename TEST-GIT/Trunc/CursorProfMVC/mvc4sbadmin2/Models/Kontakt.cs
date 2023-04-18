using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace nclprospekt.Models
{
    public class KontaktDaneDodatkowe
    {
        public string wartoscParametru { get; set; }
        public int typParametruID { get; set; }
    }

    public class KontaktWO
    {
        [Display(Name = "Tytuł wiadomości")]
        [Required(ErrorMessage = "Tytuł jest wymagany")]
        public string Tytul { get; set; }

        [Display(Name = "Zakładka gdzie napotkano błąd")]
        [Required(ErrorMessage = "Miejsce napotkania błędu jest wymagane")]
        public string NazwaEkranu { get; set; }
        [Display(Name = "Treść wiadomości")]
        [Required(ErrorMessage = "Treść wiadomości jest wymagana")]
        public string Tresc { get; set; }
        //[Display(Name = "Nazwa pliku")]
        //public string NazwaPliku { get; set; }
        [Display(Name = "Załącznik (zrzut ekranu z błędem)")]
        public HttpPostedFileBase Plik { get; set; }
        public int zrodloMaila { get; set; }
    }
        
}