using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nclprospekt.Models
{
    public class Slownik
    {
        public Slownik()
        {
            Wartosci = new List<SlownikWartosc>();
            Grupy = new List<Grupa>();
            Funkcje = new List<Funkcja>();
            Notyfikacje = new List<Notyfikacja>();
        }

        public int SlownikId { get; set; }
        public string Nazwa { get; set; }
        public int Edytowalny { get; set; }
        public List<SlownikWartosc> Wartosci;
        public List<Grupa> Grupy;
        public List<Funkcja> Funkcje { get; set; }
        public List<Notyfikacja> Notyfikacje { get; set; }
    }
}