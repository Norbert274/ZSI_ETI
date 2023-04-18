using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace nclprospekt.Models
{

    public enum AdresFieldsEnum
    {
        adres_id, //int
        nazwa, //int
        adres, //int
        kod,
        miasto,
        DOMYSLNY
    }
    
    public enum AdresMiastoDlaKoduEnum
    {
       KOD_POCZTOWY,
        MIASTO,
        TIME0930,
        TIME1200
    }

    public class AdresDetaleWO
    {
        public int user_id { get; set; }
        public int blokada_id { get; set; }
        public Adres adres { get; set; }
    }

    public class AdresyWO
    {
        public virtual IList<Adres> adresy { get; set; }
        public int ilosc_stron { get; set; }
        public int ilosc_pozycji { get; set; }
    }

    public class AdresLista 
    {
       [Key]
       public int Adres_Id { get; set; } 
       [Display(Name = "Nazwa")]
       [RegularExpression(@"^[a-zA-Z0-9\-.,ąćęłńóśźżĄĆĘŁŃÓŚŹŻ ]{1,200}$", ErrorMessage = "Znaki specjalne są niedozwolone")]
       public string Nazwa { get; set; }
    }

    public class Adres
    {
       [Key]
       public int Adres_Id { get; set; } 
       public int AdresTyp_Id { get; set; }
       [Required(ErrorMessage="Nazwa jest wymagana")]
       [Display(Name = "Nazwa")]
       [RegularExpression(@"^[a-zA-Z0-9\-.,ąćęłńóśźżĄĆĘŁŃÓŚŹŻ ]{1,200}$", ErrorMessage = "Znaki specjalne są niedozwolone")]
       public string Nazwa { get; set; }
       [Required(ErrorMessage = "Ulica jest Wymagana")]
       [Display(Name = "Ulica")]
       [RegularExpression(@"^[a-zA-Z0-9\-\/\\.,ąćęłńóśźżĄĆĘŁŃÓŚŹŻ ]{1,100}$", ErrorMessage = "Niektóre znaki specjalne są niedozwolone")]
       public string Ulica { get; set; }
       [Display(Name = "Nr. Domu")]
       [RegularExpression(@"^[a-zA-Z0-9\/.ąćęłńóśźżĄĆĘŁŃÓŚŹŻ ]{1,50}$", ErrorMessage = "Znaki specjalne są niedozwolone")]
       public string NrDomu { get; set; }
       [Display(Name = "Nr. Lokalu")]
       [RegularExpression(@"^[a-zA-Z0-9\/.ąćęłńóśźżĄĆĘŁŃÓŚŹŻ ]{1,50}$", ErrorMessage = "Znaki specjalne są niedozwolone")]
       public string NrLokalu { get; set; }

       [Required(ErrorMessage = "Kod jest wymagany")]
       [Display(Name = "Kod")]
       [RegularExpression(@"\d{2}[-]\d{3}", ErrorMessage = "Prawidłowy format: 12-345")]
       public string Kod { get; set; }

       [Required(ErrorMessage = "Miejscowość jest wymagana")]
       [Display(Name = "Miejscowość")]
       [RegularExpression(@"^[a-zA-Z0-9\-.ąćęłńóśźżĄĆĘŁŃÓŚŹŻ ]{1,100}$", ErrorMessage = "Znaki specjalne są niedozwolone")]
       public string Miasto { get; set; }
       [Display(Name = "Województwo")]
       [RegularExpression(@"^[a-zA-Z\-.ąćęłńóśźżĄĆĘŁŃÓŚŹŻ ]{1,100}$", ErrorMessage = "Znaki specjalne są niedozwolone")]
       public string Wojewodztwo { get; set; }

       [Display(Name = "Kraj")]
       [RegularExpression(@"^[a-zA-Z0-9ąćęłńóśźżĄĆĘŁŃÓŚŹŻ]{1,50}$", ErrorMessage = "Znaki specjalne są niedozwolone")]
       public string Kraj { get; set; }  
    }


}