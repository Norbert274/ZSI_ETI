using System;
using System.Collections.Generic;
using System.Web.Security;
using System.Security.Principal;
using System.ComponentModel.DataAnnotations;

namespace nclprospekt.Models
{
    public class Grupa
    {
        public int Grupa_Id { get; set; }
        public int GrupaOwner_Id { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9\\_]{1,50}$", ErrorMessage = "Polskie znaki i znaki specjalne są niedozwolone")]
        [StringLength(50, ErrorMessage = "Do 50 znakow")]
        public string GrupaNr { get; set; }
        [Required(ErrorMessage = "Nazwa jest wymagana")]
        [Display(Name = "Nazwa")]
        [RegularExpression(@"^[a-zA-Z0-9\-._\s]{1,255}$", ErrorMessage = "Poskie znaki i znaki specjalne są niedozwolone")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "Od 3 do 255 znakow")]
        public string GrupaNazwa { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9\s]{1,10}$", ErrorMessage = "Polskie znaki i znaki specjalne są niedozwolone")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Stała długość: 3 znaki (tylko litery i liczby")]
        public string Prefix { get; set; }
     }

    public class GrupaListaPaged
    {
        public IEnumerable<nclprospekt.Models.GrupaReadOnly> GrupaLista { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
        public string sortBy { get; set; }
        public string sortOrder { get; set; }
        public int rowsCount { get; set; }
    }

    public class GrupaReadOnly
    {
        public int lp { get; set; }
        public int Grupa_Id { get; set; }
        [Display(Name = "Numer grupy")]
        public string GrupaNr { get; set; }
        [Display(Name = "Nazwa")]
        public string GrupaNazwa { get; set; }
        [Display(Name = "Grupa nadrzędna")]
        public string GrupaOwnerNazwa { get; set; }
        public string Prefix { get; set; }
        [Display(Name = "Poziom")]
        public int Poziom { get; set; }
    }

    public class GrupaPodgrupa
    {
        public Grupa grupa {get; set;}
        public GrupaListaPaged podgrupaListPaged { get; set; }

    }
}