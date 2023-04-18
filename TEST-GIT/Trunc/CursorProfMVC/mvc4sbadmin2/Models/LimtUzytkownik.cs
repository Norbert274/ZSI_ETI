using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace nclprospekt.Models
{
    public class LimtUzytkownik
    {
        public int UserId { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Grupa { get; set; }

        [Range(0.0, Double.MaxValue)]
        public decimal Limit { get; set; }
        public bool Wybierz { get; set; }

        [Required(ErrorMessage="Podaj nowy limit")]
        [Range(0.0, Double.MaxValue)]
        public decimal NowyLimit { get; set; }
    }

   public class LimitVmP
   {

       public List<UserSelect> Users { get; set; }

       [Required]
       [Range(0.1, Double.MaxValue)]
       public decimal NowyLimit { get; set; }

       public string Komentarz { get; set; }
   }

   public class UserSelect
   {
       public int UserId { get; set; }
       public bool Wybierz { get; set; }
   }

}