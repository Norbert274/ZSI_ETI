using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace nclprospekt.Models
{

    public enum StanyFieldsEnum
    {
		SKU
       ,SKU_ID
       ,NAZWA
       ,DOSTEPNE
       ,ZDJECIE
       ,ZDJECIE_MINIATURA
       ,AKTYWNY
       ,GRUPA_ID
       ,GRUPA
       ,OPIS
       ,KOSZT_PUNKTOWY
       ,CZY_NOWOSC
       ,KATEGORIA_TOWARU //kategoria towaru
       ,KATEGORIA
       ,JM //J.M.
       ,MARKA

    }

    public enum StanyMagazynFieldsEnum
    {
        magazyn_id,
        nazwa
    }

    public class Stany
    {
        #region Instance Properties

        private int _SKU_ID;
        private string _SKU;
        private string _NAZWA;
        private int _DOSTEPNE;
        private string _ZDJECIE;
        private string _ZDJECIE_MINIATURA;
        private string _AKTYWNY;
        private int _GRUPA_ID;
        private string _GRUPA;
        private string _Opis_rozszerzony; //[Opis rozszerzony]
        private decimal _Koszt_punktowy;//[Koszt punktowy]
        //private decimal _CENA;
        private int _CZY_NOWOSC;
        private string _KATEGORIA_TOWARU;
        //private string _SPOSOB_PAKOWANIA;
      
        //Atrybuty z Pivot-a
        private string _MARKA;
        private string _RODZAJ;
        private string _JM;
        private string _KATEGORIA;
        private string _WAGA;
        private string _SZEROKOSC;
        private string _WYSOKOSC;
        private string _GLEBOKOSC;
        private string _LIMIT_LOGISTYCZNY;
        //private string _NUMER_FAKTURY;
        private int _ILOSC;

        [Display(Name = "SKU_ID")]
        public int SKU_ID { get { return _SKU_ID; } set { _SKU_ID = value; } }
        [Display(Name = "SKU")]
        public string SKU { get { return _SKU; } set { _SKU = value; } }
        [Display(Name = "Nazwa")]
        public string NAZWA { get { return  _NAZWA; } set { _NAZWA  = value; } }
        [Display(Name = "Miniatura zdjęcia")]
        public string ZDJECIE_MINIATURA { get { return _ZDJECIE_MINIATURA; } set { _ZDJECIE_MINIATURA = value; } }
        [Display(Name = "Rodzaj")]
        public string RODZAJ { get { return _RODZAJ; } set { _RODZAJ = value; } }
        [Display(Name = "Waga")]
        public string WAGA { get { return _WAGA; } set { _WAGA = value; } }
        [Display(Name = "Zdjęcie")]
        public string ZDJECIE { get { return _ZDJECIE; } set { _ZDJECIE = value; } }
        [Display(Name = "Grupa_id")]
        public int GRUPA_ID { get { return _GRUPA_ID; } set { _GRUPA_ID = value; } }
        [Display(Name = "Grupa")]
        public string GRUPA { get { return _GRUPA; } set { _GRUPA = value; } }
        [Display(Name = "Ilość dostępna")]
        public int DOSTEPNE { get { return _DOSTEPNE; } set { _DOSTEPNE = value; } }
      //  [Display(Name = "CENA")]
      //  public decimal CENA { get { return _CENA; } set { _CENA = value; } }
        [Display(Name = "Opis rozszerzony")]
        public string Opis_rozszerzony { get { return _Opis_rozszerzony; } set { _Opis_rozszerzony = value; } }
        [Display(Name = "Koszt punktowy")]
        public decimal Koszt_punktowy { get { return _Koszt_punktowy; } set { _Koszt_punktowy = value; } }
        [Display(Name = "CZY_NOWOSC")]
        public int CZY_NOWOSC { get { return _CZY_NOWOSC; } set { _CZY_NOWOSC = value; } }
        [Display(Name = "Aktywny")]
        public string AKTYWNY { get { return _AKTYWNY; } set { _AKTYWNY = value; } }
     //   [Display(Name = "Sposób pakowania")]
     //   public string SPOSOB_PAKOWANIA { get { return _SPOSOB_PAKOWANIA; } set { _SPOSOB_PAKOWANIA = value; } }
        [Display(Name = "MARKA")]
        public string MARKA { get { return _MARKA; } set { _MARKA = value; } }
        [Display(Name = "Kategoria towaru")]
        public string KATEGORIA_TOWARU { get { return _KATEGORIA_TOWARU; } set { _KATEGORIA_TOWARU = value; } }
        [Display(Name = "Kategoria")]
        public string KATEGORIA { get { return _KATEGORIA; } set { _KATEGORIA  = value; } }
        [Display(Name = "JM")]
        public string JM { get { return _JM; } set { _JM = value; } }
        [Display(Name = "SZEROKOŚĆ")]
        public string SZEROKOSC { get { return _SZEROKOSC; } set { _SZEROKOSC = value; } }
        [Display(Name = "WYSOKOŚĆ")]
        public string WYSOKOSC { get { return _WYSOKOSC; } set { _WYSOKOSC = value; } }
        [Display(Name = "GŁĘBOKOŚĆ")]
        public string GLEBOKOSC { get { return _GLEBOKOSC; } set { _GLEBOKOSC = value; } }
        [Display(Name = "Limit logistyczny")]
        public string LIMIT_LOGISTYCZNY { get { return _LIMIT_LOGISTYCZNY; } set { _LIMIT_LOGISTYCZNY = value; } }
       // [Display(Name = "Numer faktury")]
       // public string NUMER_FAKTURY { get { return _NUMER_FAKTURY; } set { _NUMER_FAKTURY = value; } }
	[Display(Name = "Ilość")]
        public int ILOSC { get { return _ILOSC; } set { _ILOSC = value; } }

        #endregion Instance Properties
    }

    public class MagazynLista
    {
        public int magazyn_id {get;set;}
        public string nazwa {get;set;}
    }

    public class MarkaLista
    {
        public int marka_id { get; set; }
        public string nazwa { get; set; }
    }

    public class GrupaLista
    {
        public int grupa_id { get; set; }
        public string nazwa { get; set; }
    }

    public class KategoriaLista
    {
        public int kategoria_id { get; set; }
        public string nazwa { get; set; }
    }

    public class BranzaLista
    {
        public int branza_id { get; set; }
        public string nazwa { get; set; }
    }

    public class ProduktZdjecia
    {
        public int ZDJECIE_ID { get; set; }
        public int ZDJECIE_SKU_ID { get; set; }
        public string ZDJECIE_SKU { get; set; }
        public string ZDJECIE_NAZWA { get; set; }
        public byte[] ZDJECIE_BIN { get; set; }
    }

    public class StanyWO
    {
        public int magazyn_id { get; set; }
        public virtual IList<Stany> stany { get; set; }
        public virtual IList<MagazynLista> magazyny { get; set; }
        public virtual IList<MarkaLista> marki { get; set; }
        public virtual IList<BranzaLista> branze { get; set; }
        public virtual IList<GrupaLista> grupy { get; set; }
        public virtual IList<KategoriaLista> kategorie { get; set; }
        public int ilosc_stron { get; set; }
        public int ilosc_pozycji { get; set; }
    }
        
    public class ProduktZdjeciaWO
    {
        public virtual IList<ProduktZdjecia> produktZdjecia { get; set; }
    }

    public class ProduktEditWO
    {
        public int user_id { get; set; }
        public int blokada_id { get; set; }
        public Stany produkt { get; set; }
        public virtual IList<ProduktZdjecia> produktZdjecia { get; set; }
    }

    public class ProduktDetailsWO
    {
        public int user_id { get; set; }
        public int blokada_id { get; set; }
        public Stany produkt { get; set; }
        public virtual IList<ProduktZdjecia> produktZdjecia { get; set; }
    }

    interface IStany
    {
    }

}