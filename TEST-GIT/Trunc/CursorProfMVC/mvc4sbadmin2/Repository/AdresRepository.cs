using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using nclprospekt.DAL;
using nclprospekt.Models;
using Dapper;
using System.Collections;
using System.Data;
using nclprospekt.Exceptions;
using nclprospekt.NCL_WS;

namespace nclprospekt.Repository
{
    public class AdresRepository : IAdresRepository
    {
        private CursorService _proxy;

        #region "WS Methods"

        public AdresRepository(CursorService proxy)
        {
            _proxy = proxy;
        }

        public AdresyWO LoadAdresLista(byte[] sesja, int userIdAdresy, int strona, int iloscNaStronie, string filtr, string sortowanie, bool rosnaco)
        {
            RezultatObject rez = new RezultatObject();

            rez.status = 0;
            rez.message = "";

            AdresyWO adresWO = new AdresyWO();
            IEnumerable<Adres> adresy = null;

            DataTable tblAdr = new DataTable();

            DataSet ukryteKolumny = new DataSet();
            DataTable uk = new DataTable();
            ukryteKolumny.Tables.Add(uk);
            bool adresyDoWysGrp = false;


            try
            {
                filtr = filtr + "%";

                var adrLista = _proxy.AdresStrona(sesja, userIdAdresy, adresyDoWysGrp, strona
                    , iloscNaStronie, filtr, sortowanie, rosnaco, ukryteKolumny);

                if (adrLista.status != 0) SesjaExceptionCheck.SesjaCheckThrowIfError(adrLista.status, adrLista.status_opis);

                rez.status = adrLista.status;
                rez.message = adrLista.status_opis;

                if (adrLista.status == 0 && adrLista.dane.Tables.Count > 0)
                {
                    tblAdr = adrLista.dane.Tables[0].Copy();
                    adresy = tblAdr.AsEnumerable()
                    .Select(dr =>
                    new Adres
                    {
                        Adres_Id = Convert.ToInt32(dr.Field<int>(AdresFieldsEnum.adres_id.ToString())),
                        AdresTyp_Id = Convert.ToInt32(0),
                        Nazwa = dr.Field<string>(AdresFieldsEnum.nazwa.ToString()),
                        Ulica = dr.Field<string>(AdresFieldsEnum.adres.ToString()),
                        NrDomu = "", //dr.Field<string>(""),
                        NrLokalu = "", //dr.Field<string>(""),
                        Kod = dr.Field<string>(AdresFieldsEnum.kod.ToString()),
                        Miasto = dr.Field<string>(AdresFieldsEnum.miasto.ToString()),
                        Kraj = "", //dr.Field<string>("status przesyłki"),
                        Wojewodztwo = "" //dr.Field<string>("data modyfikacji")
                    }).ToList();

                    adresWO.adresy = adresy.ToArray();   
                    adresWO.ilosc_stron = adrLista.iloscStron;
                    adresWO.ilosc_pozycji = adrLista.ilosc_total_rekordow;
                }
                else
                {
                    adresWO.adresy = new List<Adres>();
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return adresWO;

        }
        public IEnumerable<AdresLista> AdresyOdczytaj(byte[] sesja, int id)
        {
            IEnumerable<AdresLista> aLista = null;

            DataTable tblAdresy = new DataTable();

            DataSet ukryteKolumny = new DataSet();
            DataTable uk = new DataTable();
            ukryteKolumny.Tables.Add(uk);

            try
            {
                var adresLista =  _proxy.AdresyOdczytaj(sesja, id);

                if (adresLista.status != 0) SesjaExceptionCheck.SesjaCheckThrowIfError(adresLista.status, adresLista.status_opis);

                if (adresLista.status == 0)
                {
                    tblAdresy = adresLista.dane.Tables[0].Copy();
                    aLista = tblAdresy.AsEnumerable()
                        //.Skip(1)
                    .Select(dr =>
                    new AdresLista
                    {
                        Adres_Id = Convert.ToInt32(dr.Field<int>(AdresFieldsEnum.adres_id.ToString())),
                        Nazwa = dr.Field<string>(AdresFieldsEnum.nazwa.ToString())
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return aLista;

        }




        public AdresDetaleWO AdresEdytuj(byte[] sesja, int id)
        {
            AdresDetaleWO adrWO = new AdresDetaleWO();
            Adres adr = new Adres();
            adrWO.adres = adr;

            try
            {
                var aWynik =  _proxy.AdresEdytuj(sesja, id);

                if (aWynik.status != 0) SesjaExceptionCheck.SesjaCheckThrowIfError(aWynik.status, aWynik.status_opis);

                if (aWynik.status >= 0)
                {
                    adr = 
                    new Adres()
                    {
                        Adres_Id = Convert.ToInt32(id),
                        AdresTyp_Id = 0,
                        Ulica = aWynik.adres,
                        NrDomu = "",
                        NrLokalu = "",
                        Kod = aWynik.kod,
                        Miasto = aWynik.miasto,
                        Wojewodztwo = "",
                        Kraj = "",
                        Nazwa = aWynik.nazwa
                    };
                    adrWO.blokada_id = aWynik.blokada_id;
                    adrWO.adres = adr;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }	

            return adrWO;
        }

        public RezultatObject AdresEdytujAnuluj(byte[] sesja, int id)
        {
            RezultatObject rez = new RezultatObject();
            rez.status = 0;
            rez.message = "";


            try
            {
                _proxy.AdresEdytujAnuluj(sesja, id); // blokada_id
                
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return rez;

        }
        
        public RezultatObject AdresUsun(byte[] sesja, int id)
        {
            Adres adr = new Adres();
            RezultatObject rez = new RezultatObject();
            rez.status = 0;
            rez.message = "";

            try
            {
                var aWynik = _proxy.AdresUsun(sesja, id);

                if (aWynik.status != 0) SesjaExceptionCheck.SesjaCheckThrowIfError(aWynik.status, aWynik.status_opis);

                rez.status = aWynik.status;
                rez.message = aWynik.status_opis;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return rez;

        }

        public RezultatObject AdresEdytujZapisz(byte[] sesja, AdresDetaleWO adresWO, bool pozostaw_blokade)
        {

            string nazwa_in = "";
            string adres_in = "";
            string kod_in = "";
            string miasto_in = "";

            int user_id_in = 0;
            int blokada_id_in = 0;

            RezultatObject rez = new RezultatObject();
            rez.status = -1;
            rez.message = "Brak danych do zapisu";

            if (adresWO != null && adresWO.adres != null)
            {
                nazwa_in = adresWO.adres.Nazwa;
                adres_in = adresWO.adres.Ulica;
                kod_in = adresWO.adres.Kod;
                miasto_in = adresWO.adres.Miasto;

                user_id_in = adresWO.user_id;
                blokada_id_in = adresWO.blokada_id;

                rez.status = 0;
                rez.message = "";
            }
            else
                return rez;

            try
            {
                var aWynik =  _proxy.AdresEdytujZapisz(sesja, user_id_in, nazwa_in, adres_in, kod_in, miasto_in
                    , blokada_id_in, pozostaw_blokade, 1);

                if (aWynik.status != 0) SesjaExceptionCheck.SesjaCheckThrowIfError(aWynik.status, aWynik.status_opis);

                rez.status = aWynik.status;
                rez.message = aWynik.status_opis;
            }
            catch (Exception ex)
            {
                throw ex;
            }  

            return rez;
        }
        
        public IEnumerable<Adres> MiastoDlaKodu(byte[] sesja, string kod_pocztowy)
        {
            IEnumerable<Adres> adr = null;
            RezultatObject rez = new RezultatObject();
            rez.status = 0;
            rez.message = "";

            try
            {
                var aWynik = _proxy.KodyPocztoweOdczytaj(sesja,kod_pocztowy);

                if (aWynik.status != 0) SesjaExceptionCheck.SesjaCheckThrowIfError(aWynik.status, aWynik.status_opis);

                rez.status = aWynik.status;
                rez.message = aWynik.status_opis;


                if (rez.status == 0)
                {
                    DataTable tblAdresy = new DataTable();
                    tblAdresy = aWynik.dane.Tables[0].Copy();
                    adr = tblAdresy.AsEnumerable()
                    .Select(dr =>
                    new Adres
                    {
                        Kod = dr.Field<string>(AdresMiastoDlaKoduEnum.KOD_POCZTOWY.ToString()),
                        Miasto = dr.Field<string>(AdresMiastoDlaKoduEnum.MIASTO.ToString())
                    }).ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
          

            return adr;
        }
        #endregion

        #region "Funckcje pomocnicze"
        private object NZ(object S, object Def)
        {
            if (DBNull.Value.Equals(S))
            {
                return Def;
            }
            else
            {
                if ((S != null))
                {
                    return (S);
                }
                else
                {
                    return Def;
                }
            }
        }

        internal static T CastTo<T>(object value)
        {
            return value != DBNull.Value ? (T)value : default(T);
        }
        #endregion

    }
}