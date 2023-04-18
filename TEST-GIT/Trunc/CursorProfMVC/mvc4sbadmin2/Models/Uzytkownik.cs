using System;
using System.Collections.Generic;
using System.Web.Security;
using System.Security.Principal;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Data;
using System.Web.Mvc;

namespace nclprospekt.Models
{
    public enum UzytkownikDaneFieldsEnum
    {
        Uzytkownik_Id,
        Imie,
        Nazwisko,
        Nazwa,
        Password,
        Email,
        IsAdmin,
        IsSuperUser,
        CreatedOn,
        ModifiedOn

    }

    public class UzytkownikNotyfikacje
    {
        public int user_Id { get; set; }
        public string notyfikacja { get; set; }
        public bool wlacz { get; set; }

    }

    public class Uzytkownik
    {
        private ICollection<Role> _roles;

        public int Uzytkownik_Id { get; set; }

        [Display(Name = "Imię")]
        [RegularExpression(@"^[a-zA-Z0-9ąćęłńóśźżĄĆĘŁŃÓŚŹŻ\s]{1,1000}$", ErrorMessage = "Znaki specjalne są niedozwolone")]
        [StringLength(50, ErrorMessage = "do 50-ciu znaków")]
        [Required]
        public string Imie { get; set; }

        [Display(Name = "Nazwisko")]
        [RegularExpression(@"^[a-zA-Z0-9\-ąćęłńóśźżĄĆĘŁŃÓŚŹŻ\s]{1,1000}$", ErrorMessage = "Znaki specjalne są niedozwolone")]
        [StringLength(50, ErrorMessage = "do 50-ciu znaków")]
        [Required]
        public string Nazwisko { get; set; }

        [Required(ErrorMessage = "Nazwa jest wymagana")]
        [Display(Name = "Nazwa")]
        //[RegularExpression(@"^[a-zA-Z'.\s]{1,40}$", ErrorMessage = "Znaki specjalne nie są dozwolone")]
        [RegularExpression(@"^[a-zA-Z0-9\-.ąćęłńóśźżĄĆĘŁŃÓŚŹŻ\s]{1,1000}$", ErrorMessage = "Znaki specjalne są niedozwolone")]
        [StringLength(100, ErrorMessage = "do 100-ciu znaków")]
        public string Nazwa { get; set; }


        [Required(ErrorMessage = "Login jest wymagany")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane")]
        [RegularExpression(@"^[a-zA-Z0-9!?ąćęłńóśźżĄĆĘŁŃÓŚŹŻ]{1,1000}$", ErrorMessage = "Znaki specjalne są niedozwolone, oprócz ! i ? ")]
        [StringLength(50, MinimumLength = 6,ErrorMessage = "Hasło musi mieć conajmniej 6 znaków.")]
        public string Haslo { get; set; }

        [Display(Name="Potwierdź Hasło")]
        [Required(ErrorMessage = "Hasło jest wymagane")]
        [RegularExpression(@"^[a-zA-Z0-9!?ąćęłńóśźżĄĆĘŁŃÓŚŹŻ]{1,1000}$", ErrorMessage = "Znaki specjalne są niedozwolone, oprócz ! i ? ")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Hasło musi mieć conajmniej 6 znaków.")]
        [Compare("Haslo", ErrorMessage = "Hasła muszą być takie same.")]
        public string PotwierdzHaslo { get; set; }



        [Required(ErrorMessage = "E-Mail jest wymagany")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Zły format adresu Email")]
        [StringLength(50, ErrorMessage = "do 50-ciu znaków")]
        public string Email { get; set; }

        [Display(Name = "Telefon komórkowy")]
        [Range(0, int.MaxValue, ErrorMessage = "Wpisz numer używając samych cyfr lub 0 gdy brak")]
        public int TelefonKomorkowy { get; set; }

        public int BLOKADA_ID { get; set; }
        public int PRZELOZONY_ID { get; set; }
        public string PRZELOZONY_NAZWA { get; set; }
        public int MAGAZYN_ID { get; set; }
        public string MAGAZYN_NAZWA { get; set; }
        public int ADRESY_ILOSC { get; set; }

         [Display(Name = "Czy wysyłać notyfikacje e-mailowe?")]
        public bool CZY_MAILE { get; set; }
        public int TYP_ID { get; set; }
        public int WIELKOSC_ID { get; set; }
        public int OBSZAR_SPRZEDAZY_ID { get; set; }
        public int SIEC_SPRZEDAZY_ID { get; set; }
        public int REGION_SPRZEDAZY_ID { get; set; }
        public int ZESPOL_SPRZEDAZY_ID { get; set; }


        //Do Edycji
        public int GrupaId { get; set; }
        public List<int> FunkcjeIds { get; set; }
        public List<int> NotyfikacjeIds { get; set; }
        public int userBlokowanyId { get; set; }

        [Range(0, Double.MaxValue, ErrorMessage = "Limit nie może być wartością ujemną")]
        [Required(ErrorMessage = "Limit nie może być wartością ujemną")]
        public int? Limit { get; set; }

        public DataSet grupy { get; set; }
                
        public virtual IList<UzytkownikNotyfikacje> notyfikacje { get; set; }
        
        public string rola { get; set; }


        public bool IsAdmin { get; set; }

        [Display(Name = "Sprzedawca")]
        public bool IsSuperUser { get; set; }
        public bool PwdChange { get; set; }
        public virtual ICollection<Role> Roles
        {
            get { return _roles ?? (_roles = new List<Role>()); }
            set { _roles = value; }
        }

     }

    public class UzytkownikDoListy:Uzytkownik
    {
        [Display(Name="Nazwa Oddziału")]
        public string NazwaOddzialu { get; set; }
        public string Grupa { get; set; }
        public string Typ { get; set; }

        [Display(Name="Wielkość")]
        public string Wielkosc { get; set; }

        [Display(Name="Sieć Sprzedaży")]
        public string SiecSprzedazy { get; set; }
        public string Region { get; set; }

        

        //listy jako stringi rozdzielone przecinkiem
        public string NotifikacjeList { get; set; }
        public string NotifikacjeListIds { get; set; }
        public string FunkcjeList { get; set; }
        public string FunkcjeListIds { get; set; }
    //}

    //public class UserDodaj : UzytkownikDoListy
    //{
        public List<Slownik> Slowniki { get; set; }
        public List<Grupa> GrupyUser { get; set; }
        public List<Funkcja> FunkcjeUser { get; set; }
        public List<Notyfikacja> NotyfikacjeUser { get; set; }
    }

}