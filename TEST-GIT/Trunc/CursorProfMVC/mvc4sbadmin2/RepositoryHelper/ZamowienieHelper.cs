using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using nclprospekt.Models;
using nclprospekt.NCL_WS;

namespace nclprospekt.RepositoryHelper
{
    public static class ZamowienieHelper
    {
        internal static ZamowienieWczytane mapZamowienieFromWs(ZamowienieWczytajWynik wsWynik)
        {
            ZamowienieWczytane zam = new ZamowienieWczytane();
            try
            {
                zam.zamowienie_id = wsWynik.zamowienie_id;
                zam.magazyn_id = wsWynik.magazyn_id;
                zam.blokada_id = wsWynik.blokada_id;
                zam.wlasciciel_nazwa = wsWynik.wlasciciel_nazwa;
                zam.tryb_pracy = wsWynik.tryb_pracy;
                zam.zamowienie_status = wsWynik.zamowienie_status;
                zam.zamowienie_status_opis = wsWynik.zamowienie_status_opis;
                zam.typ_zamowienia = wsWynik.typ_zamowienia;
                zam.centrala = wsWynik.centrala;
                zam.magazyn_docelowy_id = wsWynik.magazyn_docelowy_id;
                zam.oddzial_docelowy_id = wsWynik.oddzial_docelowy_id;
                zam.adres_id = wsWynik.adres_id;
                zam.nazwa = wsWynik.nazwa;
                zam.adres = wsWynik.adres;
                zam.kod = wsWynik.kod;
                zam.miasto = wsWynik.miasto;
                zam.ilosc_adresow = wsWynik.ilosc_adresow;
                zam.osoba_kontaktowa = wsWynik.osoba_kontaktowa;
                zam.telefon_kontaktowy = wsWynik.telefon_kontaktowy;
                zam.uwagi = wsWynik.uwagi;
                zam.data_realizacji = wsWynik.data_realizacji < DateTime.Parse("1901-01-01") ? DateTime.Now : wsWynik.data_realizacji;
                zam.limit = wsWynik.limit;
                zam.koszt_dostawy = wsWynik.koszt_dostawy;
                zam.zamowienie_niepelne = 0;
                zam.users_ids = wsWynik.users_ids;
                zam.grupy = wsWynik.grupy;
                zam.wielkosci = wsWynik.wielkosci;
                zam.DokZw = wsWynik.DokZw;
                zam.PrzZw = wsWynik.PrzZw;
                zam.OsPryw = wsWynik.OsPryw;
                zam.DPDKwotaCOD = wsWynik.DPDKwotaCOD;
                zam.DPDWartosc = wsWynik.DPDWartosc;
                zam.daneDPD = wsWynik.maDaneDpd;
                zam.DPDTyp = wsWynik.DPDTyp;

                zam.wartosc = wsWynik.wartosc;
                zam.zamowienie_status = wsWynik.zamowienie_status;
                zam.zamowienie_status_opis = wsWynik.zamowienie_status_opis;
            }
            catch (Exception)
            {

                throw new Exception("Błąd mappowania zamówienia");
            }

            return zam;
        }

        internal static List<ZamowienieAdresCentralaWczytane> mapAdresyCentralneFromWs(DataTable dt)
        {
            List<ZamowienieAdresCentralaWczytane> zamAdresyCentr = new List<ZamowienieAdresCentralaWczytane>();
            try
            {
                if (dt.Rows.Count > 0)
                {
                    zamAdresyCentr = dt.AsEnumerable().Select(dr =>
                    new ZamowienieAdresCentralaWczytane
                    {
                        adres_id = Convert.ToInt32(dr.Field<int>("adres_id"))
                    }).ToList();
                }
            }
            catch (Exception)
            {

                throw new Exception("Błąd mapowania adresów");
            }

            return zamAdresyCentr;
        }

        internal static List<ZamowienieMagazynWczytane> mapMagazynyFromWs(DataTable dt)
        {
            List<ZamowienieMagazynWczytane> zamMagazyny = new List<ZamowienieMagazynWczytane>();

            try
            {
                if (dt.Rows.Count > 0)
                {
                    zamMagazyny = dt.AsEnumerable().Select(dr =>
                    new ZamowienieMagazynWczytane
                    {
                        magazyn_id = Convert.ToInt32(dr.Field<int>("magazyn_id")),
                        nazwa = dr.Field<string>("nazwa")
                    }).ToList();
                }
            }
            catch (Exception)
            {
                throw new Exception("Błąd mapowania magazynów");
            }

            return zamMagazyny;
        }

        internal static List<ZamowieniePozycjaWczytane> mapPozycjeZamowienia(DataTable dt)
        {
            List<ZamowieniePozycjaWczytane> zamPozycje = new List<ZamowieniePozycjaWczytane>();

            try
            {
                if (dt.Rows.Count > 0)
                {
                    zamPozycje = dt.AsEnumerable().Select(dr => new ZamowieniePozycjaWczytane
                    {
                        sku_id = NclHelper.CastTo<int>(dr.Field<int?>(ZamowienieWczytaneFieldsEnum.SKU_ID.ToString())),
                        sku = dr.Field<string>(ZamowienieWczytaneFieldsEnum.SKU.ToString()),
                        sku_nazwa = dr.Field<string>(ZamowienieWczytaneFieldsEnum.SKU_NAZWA.ToString()),
                        ilosc = (dr.Field<int?>(ZamowienieWczytaneFieldsEnum.ILOSC.ToString()) == null) ? 0 
                            : Convert.ToInt32(dr.Field<int>(ZamowienieWczytaneFieldsEnum.ILOSC.ToString())),
                        ilosc_dostepna = (dr.Field<int?>(ZamowienieWczytaneFieldsEnum.ILOSC_DOSTEPNA.ToString()) == null) ? 0 
                            : Convert.ToInt32(dr.Field<int>(ZamowienieWczytaneFieldsEnum.ILOSC_DOSTEPNA.ToString())),
                        JM = dr.Field<string>("J.M."),
                        limit = dr.Field<string>(ZamowienieWczytaneFieldsEnum.LIMIT.ToString()),
                        koszt_punktowy = (dr.Field<decimal?>("koszt punktowy") == null) ? 0 
                            : Convert.ToDecimal(dr.Field<decimal>("koszt punktowy")),
                        grupa_id = (dr.Field<int?>(ZamowienieWczytaneFieldsEnum.GRUPA_ID.ToString()) == null) ? 0 
                            : Convert.ToInt32(dr.Field<int>(ZamowienieWczytaneFieldsEnum.GRUPA_ID.ToString())),
                        grupa = String.IsNullOrEmpty(dr.Field<string>(ZamowienieWczytaneFieldsEnum.GRUPA.ToString())) 
                            ?"": dr.Field<string>(ZamowienieWczytaneFieldsEnum.GRUPA.ToString()),
                        SZEROKOSC = dr.Table.Columns.Contains("SZEROKOŚĆ") ? dr.Field<string>("SZEROKOŚĆ") : "0",
                        WYSOKOSC = dr.Table.Columns.Contains("WYSOKOŚĆ") ? dr.Field<string>("WYSOKOŚĆ") : "0",
                        GŁĘBOKSC = dr.Table.Columns.Contains("GŁĘBOKOŚĆ") ? dr.Field<string>("GŁĘBOKOŚĆ") : "0",
                        wielokrotnosc = dr.Table.Columns.Contains("wielokrotnosc") ? ((dr.Field<int?>("wielokrotnosc") == null) ? 0 
                            : Convert.ToInt32(dr.Field<int>("wielokrotnosc"))) : 0,
                        obj_kartonu = dr.Table.Columns.Contains("GŁĘBOKOŚĆ") ? ((dr.Field<decimal?>("obj_kartonu") == null) ? 0 
                            : Convert.ToDecimal(dr.Field<decimal>("obj_kartonu"))) : 0
                    }).ToList();

                }
            }
            catch (Exception)
            {

                throw new Exception("Błąd mappowania pozycji zamówienia");
            }



            return zamPozycje;
        }


        internal static void fixZamWoModel(ref ZamowienieWczytaneWO model)
        {
            try
            {
                model.zamowienie.magazyn_id = model.zamowienie.magazyn_id < 1 ? 1 : model.zamowienie.magazyn_id;
                model.zamowienie.nazwa = String.IsNullOrEmpty(model.zamowienie.nazwa) ?"": model.zamowienie.nazwa ;
                model.zamowienie.adres = model.zamowienie.adres != null ? model.zamowienie.adres : "";
                model.zamowienie.kod = model.zamowienie.kod != null ? model.zamowienie.kod : "";
                model.zamowienie.miasto = model.zamowienie.miasto != null ? model.zamowienie.miasto : "";
                model.zamowienie.osoba_kontaktowa = model.zamowienie.osoba_kontaktowa != null ? model.zamowienie.osoba_kontaktowa : "";
                model.zamowienie.telefon_kontaktowy = model.zamowienie.telefon_kontaktowy != null ? model.zamowienie.telefon_kontaktowy : "";
                model.zamowienie.uwagi = model.zamowienie.uwagi != null ? model.zamowienie.uwagi : "";
                model.zamowienie.DPDTyp = String.IsNullOrEmpty(model.zamowienie.DPDTyp) ? "" : model.zamowienie.DPDTyp;
                model.zamowienie.grupy = String.IsNullOrEmpty(model.zamowienie.grupy) ? "" : model.zamowienie.grupy;
                model.zamowienie.wielkosci = String.IsNullOrEmpty(model.zamowienie.wielkosci) ? "" : model.zamowienie.wielkosci;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        internal static DateTime fixOrderDate(DateTime? data_realizacji)
        {
            DateTime dataAktualna =DateTime.Now;

            try
            {

                int dodawaneDni = 1;
                if (dataAktualna.Hour > 15)
                {
                    dodawaneDni = 2;
                }
                if (dataAktualna.AddDays(dodawaneDni).DayOfWeek == DayOfWeek.Saturday)
                {
                    dodawaneDni = dodawaneDni + 2;
                }
                if (dataAktualna.AddDays(dodawaneDni).DayOfWeek == DayOfWeek.Sunday)
                {
                    dodawaneDni = dodawaneDni + 1;
                }

                DateTime dataMinimalna = dataAktualna.AddDays(dodawaneDni).Date;

                if (data_realizacji < dataMinimalna)
                {
                    throw new Exception(string.Format("Nie można wybrać daty wysyłki wcześniejszej niż {0}"
                        , dataMinimalna.ToString("yyyy-MM-dd")));
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dataAktualna;
        }

    }
}