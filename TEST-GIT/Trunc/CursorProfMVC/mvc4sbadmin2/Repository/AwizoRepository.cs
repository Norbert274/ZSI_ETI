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
using System.Text;
using nclprospekt.NCL_WS;

namespace nclprospekt.Repository
{
    public class AwizoRepository : IAwizoRepository
    {
        private CursorService _proxy;

        public AwizoRepository(CursorService proxy)
        {
            _proxy = proxy;
        }

        public AwizoListaWO AwizaLista(byte[] sesja, DateTime data_utworzenia_od, DateTime data_utworzenia_do,
                                        DateTime data_planowana_dostawy_od, DateTime data_planowana_dostawy_do, string nr_awiza, string nr_po, string dostawca,
                                        string qguar_za, string qguar_dostawa, string sortowanie,
                                        int strona, int ilosc_na_stronie, bool rosnaco, string strXmlStatusy, int user_id, string sku)
        {
            AwizoListaWO awizoLista = new AwizoListaWO();
            IEnumerable<AwizoLista> awizo = null;
            IEnumerable<AwizoListaStatusy> awizoStatus = null;

            RezultatObject rez = new RezultatObject();
            rez.status = 0;
            rez.message = "";

            try
            {

                long data_od = data_utworzenia_od.ToBinary();
                long data_do = data_utworzenia_do.Date.AddHours(23).AddMinutes(59).AddSeconds(59).ToBinary();
                long data_dostawy_od = data_planowana_dostawy_od.ToBinary();
                long data_dostawy_do = data_planowana_dostawy_do.Date.AddHours(23).AddMinutes(59).AddSeconds(59).ToBinary();
                nr_awiza = (nr_awiza == null) ? "" : nr_awiza;
                nr_po = (nr_po == null) ? "" : nr_po;
                dostawca = (dostawca == null) ? "" : dostawca;
                qguar_za = (qguar_za == null) ? "" : qguar_za;
                qguar_dostawa = (qguar_dostawa == null) ? "" : qguar_dostawa;
                strXmlStatusy = (strXmlStatusy == null) ? "" : strXmlStatusy;

                if (strXmlStatusy != null && strXmlStatusy != "") //Zmiana na XML
                {
                    char[] delimeterValue = new char[] { ',' };
                    string[] statusy = strXmlStatusy.Split(delimeterValue);
                    StringBuilder sb = new StringBuilder();

                    foreach (string status in statusy)
                    {
                        sb.Append(string.Format("<row status_awiza={0} />", status));
                    }
                    strXmlStatusy = (strXmlStatusy == null) ? "" : sb.ToString();
                }




                var aWynik = _proxy.AwizaListaWczytaj(sesja, data_od, data_do,
                                        data_dostawy_od, data_dostawy_do, nr_awiza, nr_po, dostawca, qguar_za, qguar_dostawa,
                                        sortowanie, strona, ilosc_na_stronie, rosnaco, strXmlStatusy, sku);

                if (aWynik.status != 0) SesjaExceptionCheck.SesjaCheckThrowIfError(aWynik.status, aWynik.status_opis);

                rez.status = aWynik.status;
                rez.message = aWynik.status_opis;

                if (rez.status == 0)
                {
                    DataTable tblAwiza = new DataTable();
                    tblAwiza = aWynik.dane.Tables[0].Copy();
                    awizo = tblAwiza.AsEnumerable()
                   .Select(dr =>
                    new AwizoLista
                    {
                        KOLEJNOSC = Convert.ToInt32(dr.Field<Int64>(EnumAwizoListaFields.KOLEJNOSC.ToString())),
                        AWIZO_ID = Convert.ToInt32(dr.Field<string>(EnumAwizoListaFields.AWIZO_ID.ToString())),
                        DATA_UTWORZENIA_AWIZA = dr.Field<string>(EnumAwizoListaFields.DATA_UTWORZENIA_AWIZA.ToString()),
                        USER_ID = Convert.ToInt32(dr.Field<Int32>(EnumAwizoListaFields.USER_ID.ToString())),
                        PLANOWANA_DATA_DOSTAWY = dr.Field<string>(EnumAwizoListaFields.PLANOWANA_DATA_DOSTAWY.ToString()),
                        AWIZO_STATUS_OPIS = dr.Field<string>(EnumAwizoListaFields.OPIS_STATUSU.ToString()),
                        AWIZO_STATUS = dr.Field<string>(EnumAwizoListaFields.STATUS.ToString()),
                        DOSTAWCA = dr.Field<string>(EnumAwizoListaFields.DOSTAWCA.ToString()),
                        NUMER_PO = dr.Field<string>(EnumAwizoListaFields.NUMER_PO.ToString()),
                        QGUAR_ZA = dr.Field<string>(EnumAwizoListaFields.QGUAR_ZA.ToString()),
                        QGUAR_DOSTAWA = dr.Field<string>(EnumAwizoListaFields.QGUAR_DOSTAWA.ToString()),
                        QGUAR_PZ = dr.Field<string>(EnumAwizoListaFields.QGUAR_PZ.ToString()),
                        //To chyba najlepsze co moglem zrobic z tym co jest... P.Sz.
                        Readonly = ((Convert.ToInt32(dr.Field<Int32>(EnumAwizoListaFields.USER_ID.ToString()))) == user_id) ? ((dr.Field<string>(EnumAwizoListaFields.STATUS.ToString()) == "ZAPISANE") ? false : true) : true

                    }).ToList();

                    awizoLista.awizaLista = awizo.ToArray();


                    DataTable tblAwizaStatusy = new DataTable();
                    tblAwizaStatusy = aWynik.dane.Tables[1].Copy();
                    awizoStatus = tblAwizaStatusy.AsEnumerable()
                   .Select(dr =>
                  new AwizoListaStatusy
                  {
                      status = dr.Field<string>(EnumAwizoListaStatusyFields.status.ToString()),
                  }).ToList();

                    awizoLista.awizoStatusy = awizoStatus.ToArray();

                    awizoLista.ilosc_stron = aWynik.ilosc_stron;
                    awizoLista.ilosc_pozycji = aWynik.ilosc_total_rekordow;
                }
                else
                {
                    awizoLista.awizaLista = new List<AwizoLista>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return awizoLista;

        }
        //Pobiera awizo (ale tylko w statusie do edycji), przesłane do realizacji już tylko AwizoPodgladWczytaj/AwizaSzczegolyWczytaj
        public AwizoWO AwizoEdycjaWczytaj(byte[] sesja, int awizoID)
        {
            AwizoWO awizoWO = new AwizoWO();
            awizoWO.awizo = new Awizo();
            IEnumerable<AwizoPozycja> awizoPozycje = null;

            if (awizoID < 0)
            {
                awizoWO.awizo.DOSTAWCA_ID = -1;
                awizoPozycje = new List<AwizoPozycja>();
                awizoWO.awizoPozycje = awizoPozycje.ToArray();
                return awizoWO;
            }

            RezultatObject rez = new RezultatObject();
            rez.status = 0;
            rez.message = "";

            try
            {
                var wynik = _proxy.AwizoWczytaj(sesja, awizoID);

                if (wynik.status != 0) SesjaExceptionCheck.SesjaCheckThrowIfError(wynik.status, wynik.status_opis);

                rez.status = wynik.status;
                rez.message = wynik.status_opis;

                if (rez.status == 0)
                {
                    awizoWO.awizo.AWIZO_ID = wynik.awizo_id_out;
                    awizoWO.awizo.DOSTAWCA_ID = wynik.dostawca_id;
                    awizoWO.awizo.PLANOWANA_DATA_DOSTAWY = wynik.planowana_data_dostawy;

                    awizoWO.awizo.ILOSC_PALET = int.Parse((NZ(wynik.ilosc_palet, "").ToString() == "") ? "0" : wynik.ilosc_palet);
                    awizoWO.awizo.ILOSC_PACZEK = int.Parse((NZ(wynik.ilosc_paczek, "").ToString() == "") ? "0" : wynik.ilosc_paczek);
                    awizoWO.awizo.TELEFON_KONTAKTOWY = wynik.telefon;
                    awizoWO.awizo.UWAGI = wynik.uwagi;
                    awizoWO.awizo.NUMER_PO = wynik.numer_po;
                    awizoWO.awizo.QGUAR_DOSTAWA = wynik.qguar_dostawa;
                    awizoWO.awizo.QGUAR_ZA = wynik.qguar_za;
                    awizoWO.awizo.AWIZO_STATUS = wynik.awizo_status;
                    awizoWO.awizo.OSOBA_KONTAKTOWA = wynik.osoba_kontaktowa;

                    DataTable tblAwizoPozycje = new DataTable();
                    if (wynik.dane != null && wynik.dane.Tables.Count > 0)
                    {
                        tblAwizoPozycje = wynik.dane.Tables[0].Copy();
                        awizoPozycje = tblAwizoPozycje.AsEnumerable()
                        .Select(dr =>
                        new AwizoPozycja
                        {
                            SKU = dr.Field<string>(EnumAwizoWczytajPozycjaFields.sku.ToString()),
                            NAZWA = dr.Field<string>(EnumAwizoWczytajPozycjaFields.nazwa.ToString()),
                            ILOSC = Convert.ToInt32(dr.Field<int>(EnumAwizoWczytajPozycjaFields.ilosc.ToString())),
                            GRUPA_ID = Convert.ToInt32(dr.Field<int>(EnumAwizoWczytajPozycjaFields.GRUPA_ID.ToString()))
                        }).ToList();

                        awizoWO.awizoPozycje = awizoPozycje.ToArray();
                    }
                    else
                    {
                        awizoPozycje = new List<AwizoPozycja>();
                        awizoWO.awizoPozycje = awizoPozycje.ToArray();
                    }



                }
                else
                {
                    Exception ex = new Exception(rez.message);
                    throw ex;
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return awizoWO;

        }
        public AwizoWO AwizoPodgladWczytaj(byte[] sesja, int awizoID)
        {
            AwizoWO awizoWO = new AwizoWO();
            awizoWO.awizo = new Awizo();
            awizoWO.dostawca = new AwizoDostawca();
            IEnumerable<AwizoPozycja> awizoPozycje = null;

            if (awizoID < 0)
            {
                awizoWO.awizo.DOSTAWCA_ID = -1;
                awizoPozycje = new List<AwizoPozycja>();
                awizoWO.awizoPozycje = awizoPozycje.ToArray();
                return awizoWO;
            }

            RezultatObject rez = new RezultatObject();
            rez.status = 0;
            rez.message = "";
            try
            {
                var wynik = _proxy.AwizaSzczegolyWczytaj(sesja, awizoID);

                if (wynik.status != 0) SesjaExceptionCheck.SesjaCheckThrowIfError(wynik.status, wynik.status_opis);

                rez.status = wynik.status;
                rez.message = wynik.status_opis;

                if (rez.status == 0)
                {
                    awizoWO.awizo.AWIZO_ID = awizoID;

                    awizoWO.awizo.PLANOWANA_DATA_DOSTAWY = wynik.planowana_data_dostawy;

                    awizoWO.awizo.ILOSC_PALET = int.Parse((NZ(wynik.ilosc_palet, "").ToString() == "") ? "0" : wynik.ilosc_palet);
                    awizoWO.awizo.ILOSC_PACZEK = int.Parse((NZ(wynik.ilosc_paczek, "").ToString() == "") ? "0" : wynik.ilosc_paczek);
                    awizoWO.awizo.TELEFON_KONTAKTOWY = wynik.telefon;
                    awizoWO.awizo.UWAGI = wynik.uwagi;
                    awizoWO.awizo.NUMER_PO = wynik.numer_PO;
                    awizoWO.awizo.QGUAR_DOSTAWA = wynik.qguar_dostawa;
                    awizoWO.awizo.QGUAR_ZA = wynik.qguar_za;
                    awizoWO.awizo.AWIZO_STATUS = wynik.awizo_status;
                    awizoWO.awizo.OSOBA_KONTAKTOWA = wynik.osoba_kontaktowa;

                    awizoWO.dostawca.NAZWA = wynik.dostawca_nazwa;
                    awizoWO.dostawca.ADRES = wynik.@dostawca_adres;
                    awizoWO.dostawca.KOD = wynik.@dostawca_kod;
                    awizoWO.dostawca.MIASTO = wynik.@dostawca_miasto;
                    awizoWO.dostawca.KRAJ = wynik.dostawca_kraj;

                    DataTable tblAwizoPozycje = new DataTable();
                    if (wynik.dane != null && wynik.dane.Tables.Count > 0)
                    {
                        tblAwizoPozycje = wynik.dane.Tables[0].Copy();
                        awizoPozycje = tblAwizoPozycje.AsEnumerable()
                        .Select(dr =>
                        new AwizoPozycja
                        {
                            SKU = dr.Field<string>(EnumAwizoPodgladPozycjaFields.SKU.ToString()),
                            NAZWA = dr.Field<string>(EnumAwizoPodgladPozycjaFields.SKU_NAZWA.ToString()),
                            ILOSC = Convert.ToInt32(dr.Field<int>(EnumAwizoPodgladPozycjaFields.ilosc_awizowana.ToString())),
                            ILOSC_DOSTARCZONA = dr.Field<string>(EnumAwizoPodgladPozycjaFields.ilosc_dostarczona.ToString()),
                            GRUPA_NAZWA = dr.Field<string>(EnumAwizoPodgladPozycjaFields.grupa.ToString()),
                            KLASA_NAZWA = dr.Field<string>(EnumAwizoPodgladPozycjaFields.klasa.ToString()),
                        }).ToList();

                        awizoWO.awizoPozycje = awizoPozycje.ToArray();
                    }
                    else
                    {
                        awizoPozycje = new List<AwizoPozycja>();
                        awizoWO.awizoPozycje = awizoPozycje.ToArray();
                    }
                }
                else
                {
                    throw new Exception(rez.message);
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return awizoWO;

        }


        public RezultatObject AwizoEdycjaZapisz(byte[] sesja, AwizoWO awizo)
        {
            RezultatObject rez = new RezultatObject();
            rez.status = 0;
            rez.message = "";

            try
            {
                DataSet pozycjeAwizaDS;
                pozycjeAwizaDS = nclprospekt.DAL.Helpers.ToDataSet(awizo.awizoPozycje);
                string NUMER_PO = awizo.awizo.NUMER_PO == null ? "" : awizo.awizo.NUMER_PO;
                string OSOBA_KONTAKTOWA = awizo.awizo.OSOBA_KONTAKTOWA == null ? "" : awizo.awizo.OSOBA_KONTAKTOWA;
                string TELEFON_KONTAKTOWY = awizo.awizo.TELEFON_KONTAKTOWY == null ? "" : awizo.awizo.TELEFON_KONTAKTOWY;
                string UWAGI = awizo.awizo.UWAGI == null ? "" : awizo.awizo.UWAGI;

                var wynik = _proxy.AwizoZapisz(sesja, awizo.awizo.AWIZO_ID, awizo.awizo.DOSTAWCA_ID, NUMER_PO, awizo.awizo.PLANOWANA_DATA_DOSTAWY,
                    OSOBA_KONTAKTOWA, TELEFON_KONTAKTOWY, UWAGI, pozycjeAwizaDS, awizo.awizo.ILOSC_PALET, awizo.awizo.ILOSC_PACZEK,2);

                if (wynik.status != 0) SesjaExceptionCheck.SesjaCheckThrowIfError(wynik.status, wynik.status_opis);

                rez.status = wynik.status;
                rez.message = wynik.status_opis;
                rez.rekord_id = wynik.awizo_id_out;
                awizo.awizo.AWIZO_ID = wynik.awizo_id_out;
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return rez;

        }

        public RezultatObject AwizoRealizuj(byte[] sesja, AwizoWO awizoWO)
        {
            RezultatObject rez = new RezultatObject();
            rez.status = 0;
            rez.message = "";

            try
            {
                //Procedura wymaga przeslania wszystkich danych ponownie - wiec tu je pobieram z bazy
                AwizoWO awizoBaza = AwizoEdycjaWczytaj(sesja, awizoWO.awizo.AWIZO_ID);
                int ileNiezerowych = awizoBaza.awizoPozycje.Count();
                if (ileNiezerowych == 0) throw new Exception("Awizo nie zawiera pozycji. Nie można zatwierdzić");

                int ileZerowych = awizoBaza.awizoPozycje.Where(x => x.ILOSC == 0).Count();
                if (ileZerowych > 0) throw new Exception("Awizo zawiera pozycje zerowe. Nie można zatwierdzić");

                DataSet pozycjeAwizaDS;
                pozycjeAwizaDS = nclprospekt.DAL.Helpers.ToDataSet(awizoBaza.awizoPozycje);
                string NUMER_PO = awizoBaza.awizo.NUMER_PO == null ? "" : awizoBaza.awizo.NUMER_PO;
                string OSOBA_KONTAKTOWA = awizoBaza.awizo.OSOBA_KONTAKTOWA == null ? "" : awizoBaza.awizo.OSOBA_KONTAKTOWA;
                string TELEFON_KONTAKTOWY = awizoBaza.awizo.TELEFON_KONTAKTOWY == null ? "" : awizoBaza.awizo.TELEFON_KONTAKTOWY;
                string UWAGI = awizoBaza.awizo.UWAGI == null ? "" : awizoBaza.awizo.UWAGI;

                var wynik = _proxy.AwizoZatwierdz(sesja, awizoBaza.awizo.AWIZO_ID, awizoBaza.awizo.DOSTAWCA_ID, NUMER_PO
                    , awizoBaza.awizo.PLANOWANA_DATA_DOSTAWY,OSOBA_KONTAKTOWA, TELEFON_KONTAKTOWY, UWAGI, pozycjeAwizaDS
                    , awizoBaza.awizo.ILOSC_PALET, awizoBaza.awizo.ILOSC_PACZEK,-1,2);

                if (wynik.status != 0) SesjaExceptionCheck.SesjaCheckThrowIfError(wynik.status, wynik.status_opis);

                rez.status = wynik.status;
                rez.message = wynik.status_opis;
                rez.rekord_id = wynik.awizo_id_out;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return rez;

        }
        public AwizoWO AwizoDostawcyLista(byte[] sesja)
        {
            AwizoWO awizo = new AwizoWO();

            IEnumerable<AwizoDostawca> dostawcyLista = null;
            IEnumerable<AwizoGrupyLista> grupaLista = null;
            RezultatObject rez = new RezultatObject();
            rez.status = 0;
            rez.message = "";

            try
            {
                var wynikDostawcy = _proxy.DostawcyWczytaj(sesja);

                if (wynikDostawcy.status != 0) SesjaExceptionCheck.SesjaCheckThrowIfError(wynikDostawcy.status, wynikDostawcy.status_opis);

                rez.status = wynikDostawcy.status;
                rez.message = wynikDostawcy.status_opis;
                if (rez.status == 0)
                {
                    DataTable tblAwizoDostawcy = new DataTable();
                    tblAwizoDostawcy = wynikDostawcy.dane.Tables[0].Copy();
                    dostawcyLista = tblAwizoDostawcy.AsEnumerable()
                    .Select(dr =>
                    new AwizoDostawca
                    {
                        DOSTAWCA_ID = dr.Field<int>(EnumAwizoDostawcaFields.dostawca_id.ToString()),
                        NAZWA = dr.Field<string>(EnumAwizoDostawcaFields.nazwa.ToString())
                    }).ToList();

                    awizo.dostawcyLista = dostawcyLista.ToArray();

                    DataTable tblAwizoGrupy = new DataTable();
                    tblAwizoGrupy = wynikDostawcy.dane.Tables[1].Copy();
                    grupaLista = tblAwizoGrupy.AsEnumerable()
                    .Select(dr =>
                    new AwizoGrupyLista
                    {
                        GRUPA_ID = dr.Field<int>(EnumAwizoGrupaFields.GRUPA_ID.ToString()),
                        GRUPA = dr.Field<string>(EnumAwizoGrupaFields.GRUPA.ToString())
                    }).ToList();

                    awizo.grupyLista = grupaLista.ToArray();
                }
                else
                {
                    Exception ex = new Exception(rez.message);
                    throw ex;
                }




            }
            catch (Exception ex)
            {
                throw ex;
            }

            return awizo;

        }
        public AwizoDostawca AwizoDostawcaSzczegoly(byte[] sesja, int dostawca_id)
        {
            AwizoDostawca dostawcaSzczegoly = new AwizoDostawca();
            RezultatObject rez = new RezultatObject();
            rez.status = 0;
            rez.message = "";

            if (dostawca_id < 0)
            {
                dostawcaSzczegoly.DOSTAWCA_ID = dostawca_id;
                dostawcaSzczegoly.KOD = "";
                dostawcaSzczegoly.MIASTO = "";
                dostawcaSzczegoly.KRAJ = "";
                dostawcaSzczegoly.ADRES = "";
                return dostawcaSzczegoly;
            }


            try
            {
                var wynikDostawcy = _proxy.DostawcaSzczegolyWczytaj(sesja, dostawca_id);

                if (wynikDostawcy.status != 0) SesjaExceptionCheck.SesjaCheckThrowIfError(wynikDostawcy.status, wynikDostawcy.status_opis);

                rez.status = wynikDostawcy.status;
                rez.message = wynikDostawcy.status_opis;

                if (rez.status == 0)
                {
                    dostawcaSzczegoly.DOSTAWCA_ID = dostawca_id;
                    dostawcaSzczegoly.KOD = wynikDostawcy.kod;
                    dostawcaSzczegoly.MIASTO = wynikDostawcy.miasto;
                    dostawcaSzczegoly.KRAJ = wynikDostawcy.kraj;
                    dostawcaSzczegoly.ADRES = wynikDostawcy.adres;
                }
                else
                {
                    Exception ex = new Exception(rez.message);
                    throw ex;
                }




            }
            catch (Exception ex)
            {
                throw ex;
            }


            return dostawcaSzczegoly;

        }

        public RezultatObject AwizoDostawcaZapisz(byte[] sesja, AwizoDostawca dostawca)
        {
            RezultatObject rez = new RezultatObject();
            rez.status = 0;
            rez.message = "";

            try
            {
                if (dostawca.DOSTAWCA_ID == 0)
                {
                    dostawca.DOSTAWCA_ID = -1;
                }

                var wynikDostawcy = _proxy.DostawcaEdycjaZapisz(sesja, dostawca.DOSTAWCA_ID
                    , dostawca.NAZWA, dostawca.ADRES, dostawca.KOD, dostawca.MIASTO, dostawca.KRAJ);

                if (wynikDostawcy.status != 0) SesjaExceptionCheck.SesjaCheckThrowIfError(wynikDostawcy.status, wynikDostawcy.status_opis);


                rez.status = wynikDostawcy.status;
                rez.message = wynikDostawcy.status_opis;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return rez;
        }

        public RezultatObject AwizoDostawcaUsun(byte[] sesja, int dostawca_id)
        {
            RezultatObject rez = new RezultatObject();
            rez.status = 0;
            rez.message = "";

            try
            {
                var wynik = _proxy.DostawcaUsun(sesja, dostawca_id);

                if (wynik.status != 0) SesjaExceptionCheck.SesjaCheckThrowIfError(wynik.status, wynik.status_opis);

                rez.status = wynik.status;
                rez.message = wynik.status_opis;

            }
            catch (Exception ex)
            {
                throw ex;
            }


            return rez;
        }

        //Pobiera liste artykulów możliwych do dodania do Awiza
        public AwizoWO AwizoSKULista(byte[] sesja)
        {
            AwizoWO awizoSKULista = new AwizoWO();

            IEnumerable<AwizoPozycja> pozycjeLista = null;

            RezultatObject rez = new RezultatObject();
            rez.status = 0;
            rez.message = "";

            try
            {
                var wynikSKU = _proxy.SkuListaWczytaj(sesja);

                if (wynikSKU.status != 0) SesjaExceptionCheck.SesjaCheckThrowIfError(wynikSKU.status, wynikSKU.status_opis);

                rez.status = wynikSKU.status;
                rez.message = wynikSKU.status_opis;
                if (rez.status == 0)
                {
                    DataTable tblAwizoSKU = new DataTable();
                    tblAwizoSKU = wynikSKU.dane.Tables[0].Copy();
                    pozycjeLista = tblAwizoSKU.AsEnumerable()
                    .Select(dr =>
                    new AwizoPozycja
                    {
                        wybierz = dr.Field<bool>(EnumAwizoSKUFields.wybierz.ToString()),
                        SKU_ID = dr.Field<int>(EnumAwizoSKUFields.SKU_ID.ToString()),
                        SKU = dr.Field<string>(EnumAwizoSKUFields.sku.ToString()),
                        NAZWA = dr.Field<string>(EnumAwizoSKUFields.nazwa.ToString())
                    }).ToList();

                    awizoSKULista.awizoSKULista = pozycjeLista.ToArray();

                }
                else
                {
                    Exception ex = new Exception(rez.message);
                    throw ex;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return awizoSKULista;

        }



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


    }
}
