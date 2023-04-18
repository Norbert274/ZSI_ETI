using System;
using System.Collections.Generic;
using System.Web.Security;
using System.Security.Principal;
using System.ComponentModel.DataAnnotations;

namespace nclprospekt.Models
{
    public class ZamowienieTyp
    {
        public int ZamowienieTyp_Id { get; set; }
        [Display(Name = "Typ Nazwa")]
        [RegularExpression(@"^[a-zA-Z'.\s]{1,40}$", ErrorMessage = "Znaki specjalne są niedozwolone")]
        [StringLength(255, ErrorMessage = "Do 255 znakow")]
        public string PartnerTypNazwa { get; set; }      
     }
}