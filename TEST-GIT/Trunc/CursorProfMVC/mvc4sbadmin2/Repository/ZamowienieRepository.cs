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
using nclprospekt.RepositoryHelper;

namespace nclprospekt.Repository
{
    public class ZamowienieRepository : IZamowienieRepository
    {
        private CursorService _proxy;
        #region "Query Methods"

        public ZamowienieRepository(CursorService proxy)
        {
            _proxy = proxy;
        }


        public RezultatObject AddToCart(byte[] sesja, ProduktDetailsWO product)
        {
            RezultatObject rez = new RezultatObject();

            try
            {
                //Kopia istniejącego koszyka
                ZamowienieWczytaneWO zamWO = FindById(-1, sesja);

                zamWO.zamowieniePozycje.Add(new ZamowieniePozycjaWczytane()
                {
                    sku_id = product.produkt.SKU_ID,
                    grupa = String.IsNullOrWhiteSpace(product.produkt.GRUPA) ? "" : product.produkt.GRUPA,
                    ilosc = product.produkt.ILOSC
                });


                zamWO.zamowieniePozycje = zamWO.zamowieniePozycje.GroupBy(x => new
                {
                    x.sku_id,
                    grupa = (String.IsNullOrWhiteSpace(product.produkt.GRUPA )
                    || x.grupa.ToUpper() == "BRAK" || x.grupa.ToUpper() == "NIEOKREŚLONA" ? "" : x.grupa.ToUpper()),
                }).Select(x => new ZamowieniePozycjaWczytane()
                {
                    sku_id = x.Key.sku_id,
                    grupa = x.Key.grupa,
                    ilosc = x.Sum(i => i.ilosc)
                }).ToList();

                DataSet danePozZamowienia = new DataSet();
                var dt = Helpers.ToDataTable(zamWO.zamowieniePozycje);
                danePozZamowienia.Tables.Add(dt);

                string warunek_grupowy = "";
                string typy = "";
                string odbiorcy = "";

                //TODO VAL DATY
               // DateTime data_realizacji = ZamowienieHelper.fixOrderDate(zamWO.zamowienie.data_realizacji);

                rez = SaveCart(sesja, zamWO.zamowienie.blokada_id, zamWO.zamowienie.magazyn_id
                    , zamWO.zamowienie.magazyn_docelowy_id, zamWO.zamowienie.adres_id, zamWO.zamowienie.nazwa
                    , zamWO.zamowienie.adres, zamWO.zamowienie.kod, zamWO.zamowienie.miasto, zamWO.zamowienie.osoba_kontaktowa
                    , zamWO.zamowienie.telefon_kontaktowy, zamWO.zamowienie.uwagi, zamWO.zamowienie.typ_zamowienia
                    , danePozZamowienia, zamWO.zamowienie.data_realizacji ?? DateTime.Now, zamWO.zamowienie.oddzial_docelowy_id
                    , zamWO.zamowienie.daneDPD,zamWO.zamowienie.DokZw, zamWO.zamowienie.OsPryw, zamWO.zamowienie.PrzZw
                    , zamWO.zamowienie.DPDKwotaCOD, zamWO.zamowienie.DPDWartosc, zamWO.zamowienie.DPDTyp
                    , odbiorcy,zamWO.zamowienie.grupy, typy, zamWO.zamowienie.wielkosci, warunek_grupowy);


            }
            catch (Exception ex)
            {

                throw ex;
            }

            return rez;
        }


 

        public ZamowienieWczytaneWO FindById(int id, byte[] sesja)
        {

            ZamowienieWczytaneWO ZamWO = new ZamowienieWczytaneWO();

            try
            {
                var wsWynik = _proxy.ZamowienieWczytaj(sesja, id, 0, 0);

                if (wsWynik.status != 0) SesjaExceptionCheck.SesjaCheckThrowIfError(wsWynik.status, wsWynik.status_opis);

                ZamWO.zamowienie = ZamowienieHelper.mapZamowienieFromWs(wsWynik);

                if (wsWynik.dane != null && wsWynik.dane.Tables.Count > 0)
                {
                    ZamWO.zamowieniePozycje = ZamowienieHelper.mapPozycjeZamowienia(wsWynik.dane.Tables[0]);

                    ZamWO.zamowienie.wartosc = ZamWO.zamowieniePozycje.Sum(x => x.ilosc * x.koszt_punktowy);

                    if (wsWynik.dane.Tables.Count > 1)
                    {
                        ZamWO.zamowienieMagazyny = ZamowienieHelper.mapMagazynyFromWs(wsWynik.dane.Tables[1]);

                        if (wsWynik.dane.Tables.Count > 2)
                        {
                            ZamWO.zamowienieCentralaAdresy = ZamowienieHelper.mapAdresyCentralneFromWs(wsWynik.dane.Tables[2]);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ZamWO;
        }

        public ZamowienieWczytaneWO SprawdzSkuStanGrupa(byte[] sesja, int idMagazynu, DataSet pozycjeDodane)
        {
            ZamowienieWczytaneWO ZamWO = new ZamowienieWczytaneWO();

            try
            {

                var wsWynik = _proxy.StanSkuGrupa(sesja, idMagazynu, pozycjeDodane);

                if (wsWynik.status != 0) SesjaExceptionCheck.SesjaCheckThrowIfError(wsWynik.status, wsWynik.status_opis);

                if (wsWynik.dane != null && wsWynik.dane.Tables.Count > 0)
                {

                    if (wsWynik.dane.Tables[0].Rows.Count > 0)
                    {
                        ZamWO.zamowieniePozycje = wsWynik.dane.Tables[0].AsEnumerable()
                        .Select(dr =>
                        new ZamowieniePozycjaWczytane
                        {
                            sku_id = CastTo<int>(dr.Field<int?>(SkuStanGrupaFieldsEnum.SKU_ID.ToString())),
                            sku = dr.Field<string>(SkuStanGrupaFieldsEnum.SKU.ToString()),
                            sku_nazwa = dr.Field<string>(SkuStanGrupaFieldsEnum.SKU_NAZWA.ToString()),
                            grupa_id = (dr.Field<int?>(SkuStanGrupaFieldsEnum.GRUPA_ID.ToString()) == null) ? 0 : Convert.ToInt32(dr.Field<int>(ZamowienieWczytaneFieldsEnum.GRUPA_ID.ToString())),
                            grupa = dr.Field<string>(SkuStanGrupaFieldsEnum.GRUPA.ToString()),
                            //ilosc = (dr.Field<int?>(SkuStanGrupaFieldsEnum.ILOSC.ToString()) == null) ? 0 : Convert.ToInt32(dr.Field<int>(ZamowienieWczytaneFieldsEnum.ILOSC.ToString())),
                            ilosc_dostepna = (dr.Field<int?>(SkuStanGrupaFieldsEnum.ILOSC_DOSTEPNA.ToString()) == null) ? 0 : Convert.ToInt32(dr.Field<int>(ZamowienieWczytaneFieldsEnum.ILOSC_DOSTEPNA.ToString())),
                            JM = dr.Field<string>("J.M."),
                            limit = dr.Field<string>(SkuStanGrupaFieldsEnum.LIMIT.ToString()),
                            koszt_punktowy = (dr.Field<decimal?>("koszt punktowy") == null) ? 0 : Convert.ToDecimal(dr.Field<decimal>("koszt punktowy"))

                        }).ToList();

                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ZamWO;
        }


        public ZamowienieWczytaneWO PobierzAdresZdefiniowanyZamowienia(byte[] sesja, int idZamowienia, int idAdresu)
        {

            ZamowienieWczytaneWO ZamWO = new ZamowienieWczytaneWO();

            try
            {

                //Wczytywanie adresow zdefiniowanych mozliwych do wyboru w zamowieniu
                var wsWynikAdres = _proxy.AdresyZamowieniaOdczytaj(sesja, idZamowienia, idAdresu);

                if (wsWynikAdres.status != 0) SesjaExceptionCheck.SesjaCheckThrowIfError(wsWynikAdres.status, wsWynikAdres.status_opis);

                ZamWO.uzywajComboAdresow = wsWynikAdres.wyswietlaj_combo;

                if (wsWynikAdres.dane != null && wsWynikAdres.dane.Tables[0].Rows.Count > 0)
                {
                    ZamWO.zamowienieZdefiniowaneAdresy = wsWynikAdres.dane.Tables[0].AsEnumerable()
                        .Select(dr =>
                        new ZamowienieAdresZdefiniowanyWczytane
                        {
                            adres_id = Convert.ToInt32(dr.Field<int>("adres_id")),
                            nazwa = dr.Field<string>("nazwa")
                        }).ToList();

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ZamWO;
        }

        public ZamowienieWczytaneWO PobierzAdresKurier(byte[] sesja)
        {
            ZamowienieWczytaneWO ZamWO = new ZamowienieWczytaneWO();

            try
            {
                var wsWynik = _proxy.OddzialyOdczytaj(sesja);

                if (wsWynik.status != 0) SesjaExceptionCheck.SesjaCheckThrowIfError(wsWynik.status, wsWynik.status_opis);

                if (wsWynik.dane != null && wsWynik.dane.Tables[0].Rows.Count > 0)
                {
                    ZamWO.zamowienieKurierAdresy = wsWynik.dane.Tables[0].Select("Aktywny = 1").AsEnumerable()
                    .Select(dr =>
                    new ZamowienieAdresKurierWczytane
                    {
                        oddzial_id = Convert.ToInt32(dr.Field<int>("oddzial_id")),
                        nazwa = dr.Field<string>("nazwa")
                    }).ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ZamWO;
        }



        #endregion

        #region "Manage Methods"


        #endregion


        #region "WS Methods"

        public DataTable LoadZamowienieListaTbl(byte[] sesja, DateTime dataOd, DateTime dataDo, string filtr)
        {

            DataTable zLista = null;

            int zamowienieStatus = -1;

            DataTable tblZam = new DataTable();

            // Get a proxy to the data service provider
            DataSet ukryteKolumny = new DataSet();
            DataTable uk = new DataTable();
            ukryteKolumny.Tables.Add(uk);

            try
            {
                var zamLista = _proxy.ZamowienieStrona(sesja, strona: 1, ilosc_na_stronie: 10000, data_od: dataOd.ToBinary()
                    , data_do: dataDo.ToBinary(), filtr: filtr, sortowanie: "", rosnaco: true, ukryte_kolumny: ukryteKolumny
                    , statusID: zamowienieStatus); //DSListaKlienciPobierz(sesja);

                if (zamLista.status != 0) SesjaExceptionCheck.SesjaCheckThrowIfError(zamLista.status, zamLista.status_opis);

                zLista = zamLista.dane.Tables[0].Copy();
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
   
            return zLista;

        }

        public IEnumerable<ZamowienieLista> LoadZamowienieLista(byte[] sesja, DateTime dataOd, DateTime dataDo, string filtr, int zamowienieStatusID)
        {

            IEnumerable<ZamowienieLista> zLista = null;

            DataTable tblZam = new DataTable();

            //dtpZakresDatOd.Value.Year, dtpZakresDatOd.Value.Month, dtpZakresDatOd.Value.Day
            //long dataOd  = new DateTime(2013, 1, 1, 0, 0, 0).ToBinary();
            //long dataDo = new DateTime(2015, 2, 28, 23, 59, 59).ToBinary();

            // Get a proxy to the data service provider
            DataSet ukryteKolumny = new DataSet();
            DataTable uk = new DataTable();
            ukryteKolumny.Tables.Add(uk);

            try
            {
                var zamLista = _proxy.ZamowienieStrona(sesja, strona: 1, ilosc_na_stronie: 100
                    , data_od: dataOd.ToBinary(), data_do: dataDo.ToBinary(), filtr: filtr, sortowanie: "", rosnaco: true
                    , ukryte_kolumny: ukryteKolumny, statusID: zamowienieStatusID); //DSListaKlienciPobierz(sesja);

                if (zamLista.status != 0) SesjaExceptionCheck.SesjaCheckThrowIfError(zamLista.status, zamLista.status_opis);

                if (zamLista.status == 0)
                {
                    tblZam = zamLista.dane.Tables[0].Copy();
                    zLista = tblZam.AsEnumerable()
                    //.Skip(1)
                    .Select(dr =>
                    new ZamowienieLista
                    {
                        koszyk = Convert.ToInt32(dr.Field<int>(ZamowienieListaFieldsEnum.KOSZYK.ToString())),
                        //oczekujace_edytowane = Convert.ToInt32(dr.Field<int>("oczekujace_edytowane")),
                        numer = Convert.ToInt32(dr.Field<int>(ZamowienieListaFieldsEnum.NUMER.ToString())),
                        zlecajacy = dr.Field<string>(ZamowienieListaFieldsEnum.ZLECAJACY.ToString()),
                        status = dr.Field<string>("status zlecenia"),
                        typ = dr.Field<string>(ZamowienieListaFieldsEnum.TYP.ToString()),
                        data_zlozenia = dr.Field<DateTime?>(ZamowienieListaFieldsEnum.DATA_ZLOZENIA.ToString()),
                        uwagi = dr.Field<string>(ZamowienieListaFieldsEnum.UWAGI.ToString()),
                        //wydanie = dr.Field<string>("wydanie"),
                        list_przewozowy_nr = dr.Field<string>("numer listu przewozowego"),
                        status_przesylki = dr.Field<string>("status przesyłki"),
                        data_ostatniej_zmiany = dr.Field<string>("data ostatniej zmiany"),
                        data_realizacji = dr.Field<string>(ZamowienieListaFieldsEnum.DATA_REALIZACJI.ToString())
                    }).ToList();

                    //DataAkcji = dr.Field<DateTime>("DataAKcji")
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
          
            return zLista;

        }

        public IEnumerable<ZamowienieStatusyLista> LoadZamowienieStatusyLista(byte[] sesja)
        {

            IEnumerable<ZamowienieStatusyLista> WO = null;
            DataTable wynikDT = new DataTable();

            try
            {
               var wsWynik = _proxy.ZamowienieStatusyLista(sesja);

                if (wsWynik.status != 0) SesjaExceptionCheck.SesjaCheckThrowIfError(wsWynik.status, wsWynik.status_opis);

                if (wsWynik.status == 0)
                {
                    wynikDT = wsWynik.dane.Tables[0].Copy();

                    WO = wynikDT.AsEnumerable()
                          .Select(dr =>
                          new ZamowienieStatusyLista
                          {
                              ZAMOWIENIE_STATUS_ID = Convert.ToInt32(dr.Field<int>(EnumZamowienieStatusyLista.ZAMOWIENIE_STATUS_ID.ToString())),
                              NAZWA = dr.Field<string>(EnumZamowienieStatusyLista.NAZWA.ToString()),
                              OPIS = dr.Field<string>(EnumZamowienieStatusyLista.OPIS.ToString())
                          }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return WO;
        }


        public RezultatObject SaveCartAdjustModel(byte[] sesja, ZamowienieWczytaneWO zamWO)
        {
            RezultatObject rez = new RezultatObject();

            rez.status = -1;
            rez.message = "Błąd zapisu";

            try
            {

                ZamowienieHelper.fixZamWoModel(ref zamWO);

                DataSet danePozZamowienia = new DataSet();
                var dt = Helpers.ToDataTable(zamWO.zamowieniePozycje);
                danePozZamowienia.Tables.Add(dt);

                string warunek_grupowy = "";
                string typy = "";
                string odbiorcy = "";
                DateTime data_realizacji = new DateTime();


                //TODO VAL DATY
               if (zamWO.zamowienie.data_realizacji <= DateTime.Now)
                {
                    data_realizacji = ZamowienieHelper.fixOrderDate(zamWO.zamowienie.data_realizacji);
                }
               else
                {
                    data_realizacji = zamWO.zamowienie.data_realizacji ?? DateTime.Now;
                }

               

                rez = SaveCart(sesja, zamWO.zamowienie.blokada_id, zamWO.zamowienie.magazyn_id
                    , zamWO.zamowienie.magazyn_docelowy_id, zamWO.zamowienie.adres_id, zamWO.zamowienie.nazwa
                    , zamWO.zamowienie.adres, zamWO.zamowienie.kod, zamWO.zamowienie.miasto, zamWO.zamowienie.osoba_kontaktowa
                    , zamWO.zamowienie.telefon_kontaktowy, zamWO.zamowienie.uwagi, zamWO.zamowienie.typ_zamowienia
                    , danePozZamowienia, data_realizacji, zamWO.zamowienie.oddzial_docelowy_id
                    , zamWO.zamowienie.daneDPD, zamWO.zamowienie.DokZw, zamWO.zamowienie.OsPryw, zamWO.zamowienie.PrzZw
                    , zamWO.zamowienie.DPDKwotaCOD, zamWO.zamowienie.DPDWartosc, zamWO.zamowienie.DPDTyp
                    , odbiorcy, zamWO.zamowienie.grupy, typy, zamWO.zamowienie.wielkosci, warunek_grupowy);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return rez;
        }



        public RezultatObject SaveCart(byte[] sesja, int blokada_id, int magazyn_id, int magazyn_docelowy_id, int adres_id, string nazwa,
                    string adres, string kod, string miasto, string osoba_kontaktowa, string telefon_kontaktowy, string uwagi,
                    int typ_zamowienia, DataSet dane, DateTime data_realizacji, int oddzial_docelowy_id, int zapisz_dane_dpd,
                    int dok_zw, int os_pryw, int prz_zw, decimal cod, decimal dpd_wartosc, string dpd_typ, string odbiorcy,
                    string grupy, string typy, string wielkosc, string warunek) //, string email_odbiorcy, int czy_zamowienie_specjalne)
        {

            RezultatObject rez = new RezultatObject();

            try
            {
               var koszykZapiszWynik = _proxy.KoszykZapisz(sesja, blokada_id,
                    magazyn_id, magazyn_docelowy_id, adres_id, nazwa, adres, kod, miasto, osoba_kontaktowa, telefon_kontaktowy,
               uwagi, typ_zamowienia, dane, data_realizacji, oddzial_docelowy_id, zapisz_dane_dpd, dok_zw, os_pryw,
               prz_zw, cod, dpd_wartosc, dpd_typ, odbiorcy, grupy, typy, wielkosc, warunek);

                if (koszykZapiszWynik.status != 0) SesjaExceptionCheck.SesjaCheckThrowIfError(koszykZapiszWynik.status, koszykZapiszWynik.status_opis);

                rez.status = koszykZapiszWynik.status;
                rez.message = koszykZapiszWynik.status_opis;
                rez.blokada_id = koszykZapiszWynik.blokada_id;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return rez;
        }

        public RezultatObject ZatwierdzKoszyk(byte[] sesja, int blokada_id)
        {
            RezultatObject rez = new RezultatObject();

            try
            {
                var wsWynik = _proxy.KoszykZatwierdz(sesja, blokada_id);

                if (wsWynik.status != 0) SesjaExceptionCheck.SesjaCheckThrowIfError(wsWynik.status, wsWynik.status_opis);

                rez.status = wsWynik.status;
                rez.message = wsWynik.status_opis;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return rez;
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