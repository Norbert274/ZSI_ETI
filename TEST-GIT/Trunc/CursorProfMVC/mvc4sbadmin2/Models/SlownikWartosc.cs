using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace nclprospekt.Models
{
    public class SlownikWartosc
    {
        public int SlownikWartoscId { get; set; }

        [Required(ErrorMessage = "Nazwa nie może być pusta")]
        [StringLength(100,MinimumLength=1)]
        public string NazwaWartosc { get; set; }
        public int SlownikId { get; set; }
        public string SlownikNazwa { get; set; }
      
    }
}