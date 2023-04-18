using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace nclprospekt.Models
{

    public enum ZamowienieListaFieldsEnum
    {
        KOSZYK,
        NUMER,
        ZLECAJACY,
        STATUS_ZLECENIA,  //[status zlecenia]
        TYP,
        DATA_ZLOZENIA,
        UWAGI,
        NUMER_LISTU_PRZEWOZOWEGO, //[numer listu przewozowego]
        STATUS_PRZESYLKI, //[status przesyłki]
        DATA_OSTATNIEJ_ZMIANY, //[data ostatniej zmiany]
        DATA_REALIZACJI
    }

    public enum ZamowienieWczytaneFieldsEnum
    {
        SKU_ID,
        SKU,
        SKU_NAZWA,
        ILOSC,
        ILOSC_DOSTEPNA,
        jm, //[J.M.]
        LIMIT,
        koszt_punktowy, //[koszt punktowy]
        GRUPA_ID,
        GRUPA
    }

    public enum SkuStanGrupaFieldsEnum
    {
        SKU_ID,
        SKU,
        SKU_NAZWA,
        GRUPA_ID,
        GRUPA,
        ILOSC_DOSTEPNA,
        jm, //[J.M.]
        LIMIT,
        koszt_punktowy //[koszt punktowy]

    }

    public enum ZamowienieWczytaneMagazynyFieldsEnum
    {
        MAGAZYN_ID,
        NAZWA, //int
    }

    public enum EnumZamowienieStatusyLista
    {
        ZAMOWIENIE_STATUS_ID,
        NAZWA,
        OPIS
    }
    public class ZamowienieStatusyLista
    {
        [Display(Name = "Status ID")]
        public int ZAMOWIENIE_STATUS_ID { get; set; }
        [Display(Name = "Nazwa statusu")]
        public string NAZWA { get; set; }
        [Display(Name = "Opis statusu")]
        public string OPIS { get; set; }
    }
    public class ZamowienieLista
    {
        #region Instance Properties

        private int _koszyk;
        //private int _oczekujace_edytowane;
        private int _numer;
        private string _zlecajacy;
        private string _status;
        private string _typ;
        private DateTime? _data_zlozenia;
        private string _uwagi;
        //private string _wydanie;
        private string _list_przewozowy_nr;
        private string _status_przesylki;
        private string _data_ostatniej_zmiany;
        private string _data_realizacji;

        [Display(Name = "Koszyk")]
        public int koszyk { get { return _koszyk; } set { _koszyk = value; } }
        //[Display(Name = "ocz. edyt.")]
        //public int oczekujace_edytowane { get { return _oczekujace_edytowane; } set { _oczekujace_edytowane = value; } }
        [Display(Name = "Numer")]
        public int numer { get { return _numer; } set { _numer = value; } }
        [Display(Name = "Zlecający")]
        public string zlecajacy { get { return _zlecajacy; } set { _zlecajacy = value; } }
        [Display(Name = "Status ID")]
        public string status { get { return _status; } set { _status = value; } }
        [Display(Name = "Typ")]
        public string typ { get { return _typ; } set { _typ = value; } }
        [Display(Name = "Data złożenia zamówienia")]
        [DisplayFormat(NullDisplayText = "", DataFormatString = "{0:d}")]
        public DateTime? data_zlozenia { get { return _data_zlozenia; } set { _data_zlozenia = value; } }
        [Display(Name = "Uwagi")]
        public string uwagi { get { return _uwagi; } set { _uwagi = value; } }
        //[Display(Name = "wydanie")]
        //public string wydanie { get { return _wydanie; } set {_wydanie  = value; } }
        [Display(Name = "Numer listu")]
        public string list_przewozowy_nr { get { return _list_przewozowy_nr; } set { _list_przewozowy_nr = value; } }
        [Display(Name = "Status przesyłki")]
        public string status_przesylki { get { return _status_przesylki; } set { _status_przesylki = value; } }
        [Display(Name = "Data ostatniej zmiany")]
        public string data_ostatniej_zmiany { get { return _data_ostatniej_zmiany; } set { _data_ostatniej_zmiany = value; } }
        [Display(Name = "Data realizacji")]
        public string data_realizacji { get { return _data_realizacji; } set { _data_realizacji = value; } }

        #endregion Instance Properties
    }

    public class ZamowienieListaWO
    {
        public DateTime DataOd { get; set; }
        public DateTime DataDo { get; set; }
        public string filtr { get; set; }
        public int zamowienieStatusID { get; set; }
        public virtual IList<ZamowienieLista> zamowieniaLista { get; set; }
        public virtual IList<ZamowienieStatusyLista> zamowienieStatusLista { get; set; }

    }



    public class ZamowienieWczytane
    {
        public int zamowienie_id { get; set; }
        public int magazyn_id { get; set; }
        public int blokada_id { get; set; }
        [Display(Name = "Właściciel")]
        public string wlasciciel_nazwa { get; set; }
        public int tryb_pracy { get; set; } //1 - edycja własnego koszyka, 2 - podgląd obcego koszyka, 3 - podgląd zamówienia
        [Display(Name = "Status")]
        public string zamowienie_status { get; set; }
        [Display(Name = "Status opis")]
        public string zamowienie_status_opis { get; set; }
        public int typ_zamowienia { get; set; }
        //'1 - transfer, 2 - odbior własny, 3 - dostawa na adres zdefiniowany,
        //'4 - dostawa na adres inny, 5 - wydanie specjalne na utylizację, 6 - wydanie specjalne na remont standów,
        //'7 - wydanie specjalne na klasyczny Copacking, 8 - zamówienie grupowe
        public bool centrala { get; set; } //czy użytkownik jest członkiem grupy centrala

        private int _magazyn_docelowy_id = -1;
        public int magazyn_docelowy_id
        {

            get
            {
                return _magazyn_docelowy_id;
            }

            set
            {
                if (typ_zamowienia == 2)
                {
                    _magazyn_docelowy_id = value;
                }
                else
                {
                    _magazyn_docelowy_id = -1;
                }
            }
        }

        private int _oddzial_docelowy_id = -1;
        public int oddzial_docelowy_id
        {
            get
            { return _oddzial_docelowy_id; }
            set
            {
                if (typ_zamowienia == 6)
                { _oddzial_docelowy_id = value; }
                else { _oddzial_docelowy_id = -1; }
            }
        }

        private int _adres_id = -1;
        public int adres_id
        {
            get
            { return _adres_id; }
            set
            {
                if (typ_zamowienia == 3)
                { _adres_id = value; }
                else { _adres_id = -1; }
            }
        }



        [Display(Name = "Nazwa")]
        [RegularExpression(@"^[a-zA-Z0-9\-.,ąćęłńóśźżĄĆĘŁŃÓŚŹŻ ]{1,200}$", ErrorMessage = "Znaki specjalne są niedozwolone")]
        [StringLength(50, ErrorMessage = "do 50-ciu znaków")]
        public string nazwa { get; set; }
        [Display(Name = "Adres")]
        [RegularExpression(@"^[a-zA-Z0-9\-\/\\.,ąćęłńóśźżĄĆĘŁŃÓŚŹŻ ]{1,100}$", ErrorMessage = "Niektóre znaki specjalne są niedozwolone")]
        [StringLength(100, ErrorMessage = "do 100 znaków")]
        public string adres { get; set; }
        [Display(Name = "Kod")]
        [RegularExpression(@"\d{2}[-]?\d{3}", ErrorMessage = "Prawidłowy format: 12-345")]
        [StringLength(100, ErrorMessage = "do 100 znaków")]
        public string kod { get; set; }
        [Display(Name = "Miasto")]
        [RegularExpression(@"^[a-zA-Z0-9\-\/\\.,ąćęłńóśźżĄĆĘŁŃÓŚŹŻ ]{1,100}$", ErrorMessage = "Niektóre znaki specjalne są niedozwolone")]
        [StringLength(100, ErrorMessage = "do 100 znaków")]

        public string miasto { get; set; }
        public int ilosc_adresow { get; set; }//'dla zamówienia centralnego
        [Display(Name = "Osoba kontakowa")]
        [StringLength(50, ErrorMessage = "do 50-ciu znaków")]
        public string osoba_kontaktowa { get; set; }
        [Display(Name = "Telefon kontaktowy")]
        [StringLength(50, ErrorMessage = "do 50-ciu znaków")]
        public string telefon_kontaktowy { get; set; }
        [Display(Name = "Uwagi")]
        [StringLength(250, ErrorMessage = "do 250-ciu znaków")]
        public string uwagi { get; set; }
        public int status { get; set; }

        public string status_opis { get; set; }
        [Display(Name = "Wartość")]
        public decimal wartosc { get; set; }
        [Display(Name = "Limit")]
        public decimal limit { get; set; }
        public decimal koszt_dostawy { get; set; }
        public DateTime? data_realizacji { get; set; }
        public int zamowienie_niepelne { get; set; }
        //Ponizej pozycje dopisane
        //public int czy_zamowienie_specjalne { get; set; } - brak w SuperZSI
        // [Display(Name = "E-mail")]
        // public string email_odbiorcy { get; set; } - brak w SuperZSI
        public string users_ids { get; set; }
        public string grupy { get; set; }
        public string wielkosci { get; set; }
        public int DokZw { get; set; }
        public int PrzZw { get; set; }
        public int OsPryw { get; set; }
        public decimal DPDKwotaCOD { get; set; }
        public decimal DPDWartosc { get; set; }
        public int daneDPD { get; set; }
        public string DPDTyp { get; set; }
    }


    public class ZamowieniePozycjaWczytane
    {
        [Display(Name = "SKU ID")]
        public int sku_id { get; set; }
        [Display(Name = "SKU")]
        public string sku { get; set; }
        [Display(Name = "Nazwa")]
        public string sku_nazwa { get; set; }
        [Display(Name = "Ważne od")]
        public string wazne_od { get; set; }
        [Display(Name = "Ważne do")]
        public string wazne_do { get; set; }
        [Display(Name = "Ilość")]
        public int ilosc { get; set; }
        [Display(Name = "Ilość dostępna")]
        public int ilosc_dostepna { get; set; }
        [Display(Name = "J.M.")]
        public string JM { get; set; }
        [Display(Name = "Limit logistyczny")]
        public string limit { get; set; }
        [Display(Name = "Koszt punktowy")]
        public decimal koszt_punktowy { get; set; }
        [Display(Name = "Grupa ID")]
        public int grupa_id { get; set; }
        [Display(Name = "Grupa")]
        public string grupa { get; set; }
        [Display(Name = "Szerokość")]
        public string SZEROKOSC { get; set; }
        [Display(Name = "Wysokość")]
        public string WYSOKOSC { get; set; }
        [Display(Name = "Głębokość")]
        public string GŁĘBOKSC { get; set; }
        [Display(Name = "Wielokrotność")]
        public int wielokrotnosc { get; set; }
        [Display(Name = "Objętość kartonu")]
        public decimal obj_kartonu { get; set; }
        //[Display(Name = "Sposób pakowania")]
        // public string Sposob_pakowania { get; set; }

    }

    public class ZamowienieMagazynWczytane
    {
        public int magazyn_id { get; set; }
        public string nazwa { get; set; }
    }

    public class ZamowienieAdresCentralaWczytane
    {
        public int adres_id { get; set; }
        public string nazwa { get; set; }
    }

    public class ZamowienieAdresZdefiniowanyWczytane
    {
        public int adres_id { get; set; }
        public string nazwa { get; set; }
    }

    public class ZamowienieAdresKurierWczytane
    {
        public int oddzial_id { get; set; }
        public string nazwa { get; set; }
    }

    public class ZamowienieWczytaneWO
    {
        public bool uzywajComboAdresow { get; set; }
        public ZamowienieWczytane zamowienie { get; set; } = new ZamowienieWczytane();
        public virtual List<ZamowieniePozycjaWczytane> zamowieniePozycje { get; set; } = new List<ZamowieniePozycjaWczytane>();
        public virtual IList<ZamowienieMagazynWczytane> zamowienieMagazyny { get; set; } = new List<ZamowienieMagazynWczytane>();
        public virtual IList<ZamowienieAdresCentralaWczytane> zamowienieCentralaAdresy { get; set; } = new List<ZamowienieAdresCentralaWczytane>();
        public virtual IList<ZamowienieAdresZdefiniowanyWczytane> zamowienieZdefiniowaneAdresy { get; set; } = new List<ZamowienieAdresZdefiniowanyWczytane>();
        public virtual IList<ZamowienieAdresKurierWczytane> zamowienieKurierAdresy { get; set; }
        public virtual IList<ZamowienieStatusyLista> zamowienieMozliweStatusyLista { get; set; }
    }

    public class Zamowienie
    {
        public Zamowienie()
        { }

        #region Instance Properties

        private int m_zAMOWIENIEID;
        private int m_wERSJA;
        private DateTime m_dATAOD;
        private DateTime m_dATADO;
        private int m_uSERID;
        private int m_uSERWERSJA;
        private int m_mAGAZYNID;
        private int m_mAGAZYNWERSJA;
        private int m_mAGAZYNDOCELOWYID;
        private int m_mAGAZYNDOCELOWYWERSJA;
        private string m_nAZWA;
        private string m_aDRES;
        private string m_kOD;
        private string m_mIASTO;
        private int m_aDRESID;
        private int m_aDRESWERSJA;
        private string m_oSOBAKONTAKTOWA;
        private string m_tELEFONKONTAKTOWY;
        private int m_zAMOWIENIESTATUSID;
        private int m_zAMOWIENIETYPID;
        private string m_uWAGI;
        private int m_zSIID;
        private string m_qGUARZL;
        private string m_qGUARZA;
        private string m_qGUARDOSTAWA;
        private int m_oDDZIALDOCELOWYID;
        private decimal m_wARTOSC;
        private decimal m_kOSZTDOSTAWY;

        public int ZAMOWIENIE_ID { get { return m_zAMOWIENIEID; } set { m_zAMOWIENIEID = value; } }
        public int WERSJA { get { return m_wERSJA; } set { m_wERSJA = value; } }
        public DateTime DATA_OD { get { return m_dATAOD; } set { m_dATAOD = value; } }
        public DateTime DATA_DO { get { return m_dATADO; } set { m_dATADO = value; } }
        public int USER_ID { get { return m_uSERID; } set { m_uSERID = value; } }
        public int USER_WERSJA { get { return m_uSERWERSJA; } set { m_uSERWERSJA = value; } }
        public int MAGAZYN_ID { get { return m_mAGAZYNID; } set { m_mAGAZYNID = value; } }
        public int MAGAZYN_WERSJA { get { return m_mAGAZYNWERSJA; } set { m_mAGAZYNWERSJA = value; } }
        public int MAGAZYN_DOCELOWY_ID { get { return m_mAGAZYNDOCELOWYID; } set { m_mAGAZYNDOCELOWYID = value; } }
        public int MAGAZYN_DOCELOWY_WERSJA { get { return m_mAGAZYNDOCELOWYWERSJA; } set { m_mAGAZYNDOCELOWYWERSJA = value; } }
        public string NAZWA { get { return m_nAZWA; } set { m_nAZWA = value; } }
        public string ADRES { get { return m_aDRES; } set { m_aDRES = value; } }
        public string KOD { get { return m_kOD; } set { m_kOD = value; } }
        public string MIASTO { get { return m_mIASTO; } set { m_mIASTO = value; } }
        public int ADRES_ID { get { return m_aDRESID; } set { m_aDRESID = value; } }
        public int ADRES_WERSJA { get { return m_aDRESWERSJA; } set { m_aDRESWERSJA = value; } }
        public string OSOBA_KONTAKTOWA { get { return m_oSOBAKONTAKTOWA; } set { m_oSOBAKONTAKTOWA = value; } }
        public string TELEFON_KONTAKTOWY { get { return m_tELEFONKONTAKTOWY; } set { m_tELEFONKONTAKTOWY = value; } }
        public int ZAMOWIENIE_STATUS_ID { get { return m_zAMOWIENIESTATUSID; } set { m_zAMOWIENIESTATUSID = value; } }
        public int ZAMOWIENIE_TYP_ID { get { return m_zAMOWIENIETYPID; } set { m_zAMOWIENIETYPID = value; } }
        public string UWAGI { get { return m_uWAGI; } set { m_uWAGI = value; } }
        public int ZSI_ID { get { return m_zSIID; } set { m_zSIID = value; } }
        public string QGUAR_ZL { get { return m_qGUARZL; } set { m_qGUARZL = value; } }
        public string QGUAR_ZA { get { return m_qGUARZA; } set { m_qGUARZA = value; } }
        public string QGUAR_DOSTAWA { get { return m_qGUARDOSTAWA; } set { m_qGUARDOSTAWA = value; } }
        public int ODDZIAL_DOCELOWY_ID { get { return m_oDDZIALDOCELOWYID; } set { m_oDDZIALDOCELOWYID = value; } }
        public decimal WARTOSC { get { return m_wARTOSC; } set { m_wARTOSC = value; } }
        public decimal KOSZT_DOSTAWY { get { return m_kOSZTDOSTAWY; } set { m_kOSZTDOSTAWY = value; } }

        #endregion Instance Properties

    }

    public class ZamowienieListaPaged
    {
        public IEnumerable<nclprospekt.Models.ZamowienieLista> ZamowienieLst { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
        public string sortBy { get; set; }
        public string sortOrder { get; set; }
        public int rowsCount { get; set; }
    }

    public static class ExtensionMethodsClass
    {
        /// <summary>
        /// Takes a generic IEnumerable and converts it to a CSV for using in an excel file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public static string ToCsv<T>(this IEnumerable<T> items)
           where T : class
        {
            var csvBuilder = new System.Text.StringBuilder();
            var properties = typeof(T).GetProperties();
            //Header row
            foreach (var prop in properties)
            {
                csvBuilder.Append(prop.Name.ToCsvValue() + ",");
            }
            csvBuilder.AppendLine("");//Add line break
            //Body
            foreach (T item in items)
            {
                string line = string.Join(",", properties.Select(p => p.GetValue(item, null).ToCsvValue()).ToArray());
                csvBuilder.AppendLine(line);
            }
            return csvBuilder.ToString();
        }

        /// <summary>
        /// Helper method for dealing with nulls and escape characters
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public static string ToCsvValue<T>(this T item)
        {
            if (item == null) return "\"\"";
            if (item is string)
            {
                return string.Format("\"{0}\"", item.ToString().Replace("\"", "\\\""));
            }
            double dummy;
            if (double.TryParse(item.ToString(), out dummy))
            {
                return string.Format("{0}", item);
            }
            return string.Format("\"{0}\"", item);
        }
    }
}