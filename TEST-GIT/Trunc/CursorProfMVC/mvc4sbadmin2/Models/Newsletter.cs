using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace nclprospekt.Models
{
    public class Newsletter
    {
        public int NewsletterId { get; set; }

        [Display(Name = "Tytuł")]
        [Required]
        [StringLength(200,MinimumLength=2)]
        public string Tytul { get; set; }

        [Display(Name="Wiadomość")]
        [Required]
        public string Wiadomosc { get; set; }

        [Display(Name ="Data utworzenia")]
        public DateTime DataUtworzenia { get; set; }

        [Display(Name ="Utworzone przez")]
        public string NazwaUzytkownika { get; set; }

        [Display(Name ="Status")]
        public string Status { get; set; }

        [Display(Name="Odbiorcy")]
        [Required]
        public List<string> Odbiorcy { get;  set; }
        
        
    }
}