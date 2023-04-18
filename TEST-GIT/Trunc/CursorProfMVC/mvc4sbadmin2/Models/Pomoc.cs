using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace nclprospekt.Models
{

    public enum PomocPlikiDoPobraniaListaFieldsEnum
    {
        Wybierz,
        plik_id,
        tytul,
        miniaturka,
        nazwa_pliku,
        WYSWIETLAJ_NA_WWW
    }

    public class PomocPobierzPlik
    {
        public string NazwaPliku { get; set; }
        public byte[] plik { get; set; }
    }

    public class PomocPlikiLista
    {
        private bool _WYBIERZ;
        private int _PLIK_ID;
        private string _TYTUL;
        private string _MINIATURKA;
        private string _NAZWA_PLIKU;
        private bool _WYSWIETLAJ_NA_WWW;
        [Display(Name = "Wybierz")]
        public bool WYBIERZ { get { return _WYBIERZ; } set { _WYBIERZ = value; } }
        [Display(Name = "ID pliku")]
        public int PLIK_ID { get { return _PLIK_ID; } set { _PLIK_ID = value; } }
        [Display(Name = "Tytuł pliku")]
        public string TYTUL { get { return _TYTUL; } set { _TYTUL = value; } }
        [Display(Name = "Miniaturka")]
        public string MINIATURKA { get { return _MINIATURKA; } set { _MINIATURKA = value; } }
        [Display(Name = "Nazwa pliku")]
        public string NAZWA_PLIKU { get { return _NAZWA_PLIKU; } set { _NAZWA_PLIKU = value; } }
        [Display(Name = "Czy wyświetlać na WWW")]
        public bool WYSWIETLAJ_NA_WWW { get { return _WYSWIETLAJ_NA_WWW; } set { _WYSWIETLAJ_NA_WWW = value; } }
    }

    public class PomocPlikiListaWO
    {
        public virtual IList<PomocPlikiLista> pomocPlikiLista { get; set; }
    }
}