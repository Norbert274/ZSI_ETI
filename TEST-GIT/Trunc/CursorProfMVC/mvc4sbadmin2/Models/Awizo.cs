using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace nclprospekt.Models
{
    public enum EnumAwizoListaFields
    {
        KOLEJNOSC,
        AWIZO_ID,
        DATA_UTWORZENIA_AWIZA,
        NUMER_PO,
        DOSTAWCA,
        USER_ID,
        PLANOWANA_DATA_DOSTAWY,
        STATUS,
        OPIS_STATUSU,
        QGUAR_ZA,
        QGUAR_DOSTAWA,
        QGUAR_PZ,
        SKU

    }

    public enum EnumAwizoListaStatusyFields
    {
        status
    }

    public enum EnumAwizoDostawcaFields
    {
        dostawca_id,
        nazwa
    }

    public enum EnumAwizoSKUFields
    {
        wybierz, 
	       SKU_ID, 
		   sku,
		   nazwa
    }


    public enum EnumAwizoGrupaFields
    {
        GRUPA_ID,
        GRUPA
    }

    public enum EnumAwizoWczytajPozycjaFields
    {
        sku,
        nazwa,
        ilosc,
        GRUPA_ID
    }

    public enum EnumAwizoPodgladPozycjaFields
    {
        SKU,
        SKU_NAZWA,
        klasa,
        ilosc_awizowana,
        ilosc_dostarczona,
        grupa
    }



    public class AwizoListaStatusy
    {
        [Display(Name = "Status awiza")]
        public string status { get; set; }
    }

    public class AwizoLista
    {
        [Display(Name = "Lp")]
        public int KOLEJNOSC { get; set; }

        [Display(Name = "Identyfikator awiza")]
        public int AWIZO_ID { get; set; }

        [Display(Name = "Data utworzenia")]
        public string DATA_UTWORZENIA_AWIZA { get; set; }

        [Display(Name = "Numer PO")]
        public string NUMER_PO { get; set; }

        [Display(Name = "Dostawca")]
        public string DOSTAWCA { get; set; }

        [Display(Name = "Identyfikator użytkownika")]
        public int USER_ID { get; set; }

        [Display(Name = "Planowana data dostawy")]
        public string PLANOWANA_DATA_DOSTAWY { get; set; }

        [Display(Name = "Status awiza")]
        public string AWIZO_STATUS { get; set; }

        [Display(Name = "Opis statusu")]
        public string AWIZO_STATUS_OPIS { get; set; }

        [Display(Name = "QGuar ZA")]
        public string QGUAR_ZA { get; set; }

        [Display(Name = "QGuar dostawa")]
        public string QGUAR_DOSTAWA { get; set; }

        [Display(Name = "QGuar PZ")]
        public string QGUAR_PZ { get; set; }

        public bool Readonly { get; set; }

    }

    public class Awizo
    {
        [Display(Name = "Identyfikator awiza")]
        public int AWIZO_ID { get; set; }
       
        //  public int PROJEKT_ID { get; set; }
        //  public int UZYTKOWNIK_ID { get; set; }
        // public int MAGAZYN_ID { get; set; }
        // public int OSOBA_ID { get; set; }
        [Display(Name = "Telefon kontaktowy")]
        public string TELEFON_KONTAKTOWY { get; set; }
        
        
        [Display(Name = "Uwagi")]
        public string UWAGI { get; set; }

        [Display(Name = "Status awiza")]
        public string AWIZO_STATUS { get; set; }

        [Display(Name = "Dostawca ID")]
        public int DOSTAWCA_ID { get; set; }

        [Display(Name = "Planowana data dostawy")]
        public DateTime PLANOWANA_DATA_DOSTAWY { get; set; }

        [Display(Name = "Numer PO")]
        [Required(ErrorMessage = "Wartość wymagana")]
        public string NUMER_PO { get; set; }

        [Display(Name = "QGuar ZA")]
        public string QGUAR_ZA { get; set; }

        [Display(Name = "QGuar dostawa")]
        public string QGUAR_DOSTAWA { get; set; }
        //public bool DOSTAWA_PRZYJECHALA { get; set; }
        //public string QGUAR_PZ { get; set; }
        //public bool CZY_SPRZEDAZ { get; set; }

        [Display(Name = "Osoba kontaktowa")]
        [StringLength(255, ErrorMessage = "{0} powinno mieć maksumum {1} znaków.", MinimumLength = 0)]
        public string OSOBA_KONTAKTOWA { get; set; }


        [Display(Name = "Ilość palet")]
        [Range(0, int.MaxValue, ErrorMessage = "Wartość musi być liczbą całkowitą i wiekszą od zera")]
        public int ILOSC_PALET { get; set; }
        
        [Display(Name = "Ilość paczek")]
        [Range(0, int.MaxValue, ErrorMessage = "Wartość musi być liczbą całkowitą i wiekszą od zera")]
        public int ILOSC_PACZEK { get; set; }
        
        [Display(Name = "Dostawca adres")]
        [Required(ErrorMessage = "Wartość wymagana")]
        public string DOSTAWCA_ADRES { get; set; }

        [Display(Name = "Dostawca kod")]
        [Required(ErrorMessage = "Wartość wymagana")]
        public string DOSTAWCA_KOD { get; set; }

        [Display(Name = "Dostawca kraj")]
        [Required(ErrorMessage = "Wartość wymagana")]
        public string DOSTAWCA_KRAJ { get; set; }

        [Display(Name = "Dostawca miasto")]
        [Required(ErrorMessage = "Wartość wymagana")]
        public string DOSTAWCA_MIASTO { get; set; }

        [Display(Name = "Dostawca nazwa")]
        [Required(ErrorMessage = "Wartość wymagana")]
        public string DOSTAWCA_NAZWA { get; set; }

        //public string UZYTKOWNIK { get; set; }
    }

    public class AwizoDostawca
    {
        public int DOSTAWCA_ID { get; set; }
        [Required(ErrorMessage = "Nazwa jest wymagana")]
        [Display(Name = "Nazwa")]
        [RegularExpression(@"^[a-zA-Z0-9\-\/\\.,ąćęłńóśźżĄĆĘŁŃÓŚŹŻ ]{1,100}$", ErrorMessage = "Niektóre znaki specjalne są niedozwolone")]
        [StringLength(100, ErrorMessage = "{0} powinno mieć maksumum {1} znaków.", MinimumLength = 0)]
        public string NAZWA { get; set; }
        [Required(ErrorMessage = "Adres jest wymagany")]
        [Display(Name = "Adres")]
        [RegularExpression(@"^[a-zA-Z0-9\-\/\\.,ąćęłńóśźżĄĆĘŁŃÓŚŹŻ ]{1,100}$", ErrorMessage = "Niektóre znaki specjalne są niedozwolone")]
        [StringLength(100, ErrorMessage = "{0} powinno mieć maksumum {1} znaków.", MinimumLength = 0)]
        public string ADRES { get; set; }
        [Required(ErrorMessage = "Kod jest wymagany")]
        [Display(Name = "Kod")]
        [RegularExpression(@"\d{2}[-]\d{3}", ErrorMessage = "Prawidłowy format: 12-345")]
        [StringLength(50, ErrorMessage = "{0} powinno mieć maksumum {1} znaków.", MinimumLength = 0)]
        public string KOD { get; set; }
        [Required(ErrorMessage = "Miejscowość jest wymagana")]
        [Display(Name = "Miejscowość")]
        [StringLength(100, ErrorMessage = "{0} powinno mieć maksumum {1} znaków.", MinimumLength = 0)]
        public string MIASTO { get; set; }
        [Display(Name = "Kraj")]
        [RegularExpression(@"^[a-zA-Z0-9ąćęłńóśźżĄĆĘŁŃÓŚŹŻ]{1,50}$", ErrorMessage = "Znaki specjalne są niedozwolone")]
        [StringLength(100, ErrorMessage = "{0} powinno mieć maksumum {1} znaków.", MinimumLength = 0)]
        public string KRAJ { get; set; }
    }

    public class AwizoPozycja
    {
        [Display(Name = "Wybierz")]
        public bool wybierz { get; set; }
        [Display(Name = "Identyfikator SKU")]
        public int SKU_ID { get; set; }
        [Display(Name = "SKU")]
        public string SKU { get; set; }
        [Display(Name = "Nazwa")]
        public string NAZWA { get; set; }
        [Display(Name = "Ilość")]
        [Range(1, int.MaxValue, ErrorMessage = "Wartość musi być liczbą całkowitą i wiekszą od zera")]
        public int ILOSC { get; set; }
        [Display(Name = "Identyfikator grupy")]
        public int GRUPA_ID { get; set; }

        //Potrzebne dla podgladu
        [Display(Name = "Klasa")]
        public string KLASA_NAZWA { get; set; }
        [Display(Name = "Nazwa grupy")]
        public string GRUPA_NAZWA { get; set; }
        [Display(Name = "Ilość dostarczona")]
        public string ILOSC_DOSTARCZONA { get; set; } //String poniewaz z procedury dostaje "---" zamiast NULL
    }

    public class KategorieLista
    {
        [Display(Name = "Nazwa kategorii")]
        public string NAZWA { get; set; }
    }

    public class AwizoGrupyLista
    {
        [Display(Name = "Identyfikator grupy")]
        public int GRUPA_ID { get; set; }
        [Display(Name = "Nazwa grupy")]
        public string GRUPA { get; set; }
    }

    public class AwizoWO
    {
        public Awizo awizo { get; set; }
        public virtual IList<AwizoPozycja> awizoPozycje { get; set; }

        public AwizoDostawca dostawca { get; set; }
        public virtual IEnumerable<AwizoDostawca> dostawcyLista { get; set; }
        public virtual IList<KategorieLista> kategorieLista { get; set; }
        public virtual IList<AwizoGrupyLista> grupyLista { get; set; }
        public virtual IList<AwizoPozycja> awizoSKULista { get; set; }
    }

    public class AwizoListaWO
    {
        public virtual IList<AwizoLista> awizaLista { get; set; }
        public virtual IList<AwizoListaStatusy> awizoStatusy { get; set; }
        
        public int ilosc_stron { get; set; }
        public int ilosc_pozycji { get; set; }

    }

}
