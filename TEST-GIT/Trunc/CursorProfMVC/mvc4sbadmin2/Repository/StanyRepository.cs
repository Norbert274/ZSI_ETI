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
    public class StanyRepository : IStanyRepository
    {
        private CursorService _proxy;

        public StanyRepository(CursorService proxy)
        {
            _proxy = proxy;
        }

        #region "WS Methods"


        public StanyWO LoadStanyWO(byte[] sesja, int magazyn_id, string grupy, string marki, string branze, string kategorie, string sku, string nazwa, int strona, int ilosc_na_stronie, string sortowanie, bool rosnaco, bool czy_niezerowe, bool onlynew)
        {

            StanyWO stanWO = new StanyWO();
            IEnumerable<Stany> stany = null;
            IEnumerable<MagazynLista> magazyny = null;

            stanWO.stany = new List<Stany>();
            stanWO.magazyny = new List<MagazynLista>();

            try
            {
                var wsWynik = _proxy.Stan(sesja, magazyn_id, grupy, marki, branze, kategorie, sku, nazwa, strona, ilosc_na_stronie
                    , sortowanie, rosnaco, czy_niezerowe, onlynew);

                if (wsWynik.status != 0) SesjaExceptionCheck.SesjaCheckThrowIfError(wsWynik.status, wsWynik.status_opis);

                stanWO.magazyn_id = wsWynik.magazyn_id;
                stanWO.ilosc_stron = wsWynik.ilosc_stron;
                stanWO.ilosc_pozycji = wsWynik.ilosc_total_rekordow;



                if (wsWynik.dane != null && wsWynik.dane.Tables.Count > 0)
                {

                    if (wsWynik.dane.Tables[0].Rows.Count > 0)
                    {

                        string sqlQry = "";

                        if (onlynew == true)
                        {
                            sqlQry = string.Format("{0} = {1}", "Czy_nowosc", onlynew);
                        }


                        stany = wsWynik.dane.Tables[0].Select(sqlQry).AsEnumerable()
                        //.Skip(1)
                        .Select(dr =>
                        new Stany
                        {
                            Koszt_punktowy = dr.Field<decimal>("Koszt punktowy"),
                            SKU_ID = CastTo<int>(dr.Field<int?>(StanyFieldsEnum.SKU_ID.ToString())),
                            SKU = dr.Field<string>(StanyFieldsEnum.SKU.ToString()),
                            NAZWA = dr.Field<string>(StanyFieldsEnum.NAZWA.ToString()),
                            DOSTEPNE = dr.Field<int>(StanyFieldsEnum.DOSTEPNE.ToString()),
                            // ZDJECIE = (dr.IsNull(StanyFieldsEnum.ZDJECIE_MINIATURA.ToString()) == false) ? Convert.ToBase64String(dr.Field<byte[]>(StanyFieldsEnum.ZDJECIE_MINIATURA.ToString())) : null,//dr.Field<string>(StanyFieldsEnum.ZDJECIE.ToString()),
                            ZDJECIE_MINIATURA = (dr.IsNull(StanyFieldsEnum.ZDJECIE_MINIATURA.ToString()) == false) ? Convert.ToBase64String(dr.Field<byte[]>(StanyFieldsEnum.ZDJECIE_MINIATURA.ToString())) : null, //(dr.IsNull(StanyFieldsEnum.ZDJECIE_MINIATURA.ToString()) == false) ? Convert.ToBase64String(dr.Field<byte[]>(StanyFieldsEnum.ZDJECIE_MINIATURA.ToString())) : null,(dr.IsNull(StanyFieldsEnum.ZDJECIE_MINIATURA.ToString()) == false) ? dr.Field<byte[]>(StanyFieldsEnum.ZDJECIE_MINIATURA.ToString()) : null,
                            AKTYWNY = dr.Field<string>(StanyFieldsEnum.AKTYWNY.ToString()),
                            GRUPA_ID = (dr.Field<int?>(StanyFieldsEnum.GRUPA_ID.ToString()) == null) ? 0 : Convert.ToInt32(dr.Field<int>(StanyFieldsEnum.GRUPA_ID.ToString())),
                            GRUPA = dr.Field<string>(StanyFieldsEnum.GRUPA.ToString()),
                            //CENA = (dr.Field<decimal?>("CENA") == null) ? 0 : Convert.ToDecimal(dr.Field<decimal>("CENA")),
                            CZY_NOWOSC = dr.Field<int>(StanyFieldsEnum.CZY_NOWOSC.ToString()),
                            KATEGORIA_TOWARU = dr.Field<string>("kategoria towaru"),
                            //SPOSOB_PAKOWANIA = dr.Field<string>("Sposob_pakowania"),

                            //Atrybuty z pivot-a ponizej
                            MARKA = dr.Field<string>(StanyFieldsEnum.MARKA.ToString()),
                            //RODZAJ = dr.Field<string>(StanyFieldsEnum.RODZAJ.ToString()),
                            JM = dr.Field<string>("J.M."),
                            KATEGORIA = dr.Field<string>(StanyFieldsEnum.KATEGORIA.ToString())
                            // WAGA = dr.Field<string>("WAGA"),
                            //SZEROKOSC = dr.Field<string>("SZEROKOŚĆ"),
                            //WYSOKOSC = dr.Field<string>("WYSOKOŚĆ"),
                            //GLEBOKOSC = dr.Field<string>("GŁĘBOKOŚĆ"),
                            // LIMIT_LOGISTYCZNY = dr.Field<string>("LIMIT_LOGISTYCZNY"),
                            // NUMER_FAKTURY = dr.Field<string>("NUMER_FAKTURY")

                        }).ToList();

                        stanWO.stany = stany.ToArray();
                    }
                    else
                        stanWO.stany = new List<Stany>();

                    if (wsWynik.dane.Tables.Count > 1)
                    {
                        if (wsWynik.dane.Tables[1].Rows.Count > 0)
                        {
                            magazyny = wsWynik.dane.Tables[1].AsEnumerable()
                            //.Skip(1)
                            .Select(dr =>
                            new MagazynLista
                            {
                                magazyn_id = Convert.ToInt32(dr.Field<int>(StanyMagazynFieldsEnum.magazyn_id.ToString())),
                                nazwa = dr.Field<string>(StanyMagazynFieldsEnum.nazwa.ToString())
                            }).ToList();

                            stanWO.magazyny = magazyny.ToArray();
                        }
                        else
                            stanWO.magazyny = new List<MagazynLista>();

                    } // wsWynik.dane.Tables.Count > 1              

                    //Doczytanie słowników kategorii



                }
                else
                {

                    stanWO.stany = new List<Stany>();
                    stanWO.magazyny = new List<MagazynLista>();

                    stanWO.marki = new List<MarkaLista>();
                    stanWO.branze = new List<BranzaLista>();
                    stanWO.grupy = new List<GrupaLista>();
                    stanWO.kategorie = new List<KategoriaLista>();

                } // wsWynik.dane.Tables.Count > 0

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return stanWO;
        }
        public StanyWO LoadDictionaries(byte[] sesja)
        {
            int status = 0;
            string message = "";

            StanyWO stanWO = new StanyWO();

            IEnumerable<MarkaLista> marki = null;
            IEnumerable<BranzaLista> branze = null;
            IEnumerable<GrupaLista> grupy = null;
            IEnumerable<KategoriaLista> kategorie = null;

            stanWO.marki = new List<MarkaLista>();
            stanWO.branze = new List<BranzaLista>();
            stanWO.grupy = new List<GrupaLista>();
            stanWO.kategorie = new List<KategoriaLista>();

            try
            {
                var wsWynik = _proxy.SkuStanFiltry(sesja);

                if (wsWynik.status != 0) SesjaExceptionCheck.SesjaCheckThrowIfError(wsWynik.status, wsWynik.status_opis);

                if (wsWynik.dane != null && wsWynik.dane.Tables.Count > 0)
                {
                    //Marka
                    //wsWynik.dane.Tables.Count > 0;
                    if (wsWynik.dane.Tables.Count > 0)
                    {

                        string sqlQry = "";
                        marki = wsWynik.dane.Tables[0].Select(sqlQry).AsEnumerable()

                        .Select(dr =>
                        new MarkaLista
                        {
                            nazwa = dr.Field<string>("WARTOSC")
                        }).ToList();

                        stanWO.marki = marki.ToArray();
                    }
                    else
                        stanWO.marki = new List<MarkaLista>();

                    bool markaNieokreslona = false;
                    foreach (MarkaLista marka in stanWO.marki)
                    {
                        if (marka.nazwa == "NIEOKREŚLONA")
                        {
                            markaNieokreslona = true;
                            break;
                        }
                    }
                    if (markaNieokreslona == false)
                    {
                        MarkaLista nieokreslonaMarka = new MarkaLista();
                        nieokreslonaMarka.nazwa = "NIEOKREŚLONA";
                        List<MarkaLista> lista = stanWO.marki.ToList();
                        lista.Insert(0, nieokreslonaMarka);
                        stanWO.marki = lista.ToList();
                    }

                    //Branza
                    //wsWynik.dane.Tables.Count > 1;
                    if (wsWynik.dane.Tables.Count > 1)
                    {

                        string sqlQry = "";
                        branze = wsWynik.dane.Tables[1].Select(sqlQry).AsEnumerable()

                            .Select(dr =>
                        new BranzaLista
                        {
                            nazwa = dr.Field<string>("WARTOSC")
                        }).ToList();

                        stanWO.branze = branze.ToArray();
                    }
                    else
                        stanWO.branze = new List<BranzaLista>();

                    bool branzaNieokreslona = false;
                    foreach (BranzaLista brazna in stanWO.branze)
                    {
                        if (brazna.nazwa == "NIEOKREŚLONA")
                        {
                            branzaNieokreslona = true;
                            break;
                        }
                    }
                    if (branzaNieokreslona == false)
                    {
                        BranzaLista nieokreslonaBranza = new BranzaLista();
                        nieokreslonaBranza.nazwa = "NIEOKREŚLONA";
                        List<BranzaLista> lista = stanWO.branze.ToList();
                        lista.Insert(0, nieokreslonaBranza);
                        stanWO.branze = lista.ToList();
                    }

                    //Grupy
                    //wsWynik.dane.Tables.Count > 2;
                    if (wsWynik.dane.Tables.Count > 2)
                    {

                        string sqlQry = "";
                        grupy = wsWynik.dane.Tables[2].Select(sqlQry).AsEnumerable()

                        .Select(dr =>
                        new GrupaLista
                        {
                            nazwa = dr.Field<string>("WARTOSC")
                        }).ToList();

                        stanWO.grupy = grupy.ToArray();
                    }
                    else
                        stanWO.grupy = new List<GrupaLista>();

                    bool grupaNieokreslona = false;
                    foreach (GrupaLista grupa in stanWO.grupy)
                    {
                        if (grupa.nazwa == "NIEOKREŚLONA")
                        {
                            grupaNieokreslona = true;
                            break;
                        }
                    }
                    if (grupaNieokreslona == false)
                    {
                        GrupaLista nieokreslonaGrupa = new GrupaLista();
                        List<GrupaLista> lista = stanWO.grupy.ToList();
                        nieokreslonaGrupa.nazwa = "NIEOKREŚLONA";
                        lista.Insert(0, nieokreslonaGrupa);
                        stanWO.grupy = lista.ToList();
                    }


                    //Kategorie
                    //wsWynik.dane.Tables.Count > 3;
                    if (wsWynik.dane.Tables.Count > 3)
                    {

                        string sqlQry = "";
                        kategorie = wsWynik.dane.Tables[3].Select(sqlQry).AsEnumerable()
                        //.Skip(1)
                        .Select(dr =>
                        new KategoriaLista
                        {
                            nazwa = dr.Field<string>("WARTOSC")
                        }).ToList();

                        stanWO.kategorie = kategorie.ToArray();
                    }
                    else
                        stanWO.kategorie = new List<KategoriaLista>();

                    bool kategoriaNieokreslona = false;
                    foreach (KategoriaLista kategoria in stanWO.kategorie)
                    {
                        if (kategoria.nazwa == "NIEOKREŚLONA")
                        {
                            kategoriaNieokreslona = true;
                            break;
                        }
                    }
                    if (kategoriaNieokreslona == false)
                    {
                        KategoriaLista nieokreslonaKategoria = new KategoriaLista();
                        nieokreslonaKategoria.nazwa = "NIEOKREŚLONA";
                        List<KategoriaLista> lista = stanWO.kategorie.ToList();
                        lista.Insert(0, nieokreslonaKategoria);
                        stanWO.kategorie = lista.ToList();
                    }


                }
                else
                {
                    stanWO.marki = new List<MarkaLista>();
                    stanWO.branze = new List<BranzaLista>();
                    stanWO.grupy = new List<GrupaLista>();
                    stanWO.kategorie = new List<KategoriaLista>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return stanWO;


        }

        public int PobierzDomyslnyMagazynID(byte[] sesja)
        {
            int magazyn_id = -1;

            try
            {
                var wsWynik = _proxy.PobierzIdMagazynDomyslny(sesja);

                if (wsWynik.status != 0) SesjaExceptionCheck.SesjaCheckThrowIfError(wsWynik.status, wsWynik.status_opis);

                magazyn_id = wsWynik.magazyn_id;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return magazyn_id;


        }
        public ProduktZdjeciaWO ShowGallery(byte[] sesja, int sku_id)
        {
            ProduktZdjeciaWO produktZdjeciaWO = new ProduktZdjeciaWO();
            IEnumerable<ProduktZdjecia> produktZdjecia = new List<ProduktZdjecia>();
            produktZdjeciaWO.produktZdjecia = new List<ProduktZdjecia>();

            try
            {
                var wsWynik = _proxy.ZdjeciaWczytaj(sesja, sku_id);

                if (wsWynik.status != 0) SesjaExceptionCheck.SesjaCheckThrowIfError(wsWynik.status, wsWynik.status_opis);

                if (wsWynik.dane != null && wsWynik.dane.Tables.Count > 0)
                {
                 
                    if (wsWynik.dane.Tables[0].Rows.Count > 0)
                    {
                        produktZdjecia = wsWynik.dane.Tables[0].AsEnumerable().ToList()
                             .Select(dr =>
                        new ProduktZdjecia
                        {

                            ZDJECIE_ID = dr.Field<int>("ID"),
                            ZDJECIE_SKU_ID = dr.Field<int>("SKU_ID"),
                            ZDJECIE_SKU = dr.Field<string>("SKU"),
                            ZDJECIE_NAZWA = dr.Field<string>("NAZWA"),
                            ZDJECIE_BIN = dr.Field<byte[]>("ZDJECIE"),

                        }).ToList();

                    }

                }

            }
            catch (Exception ex)
            {
                if (ex.Message != "Nie znaleziono zdjęć dla produktu") throw ex;
            }
            produktZdjeciaWO.produktZdjecia = produktZdjecia.ToList();
            return produktZdjeciaWO;
        }
        public ProduktDetailsWO ProductDetails(byte[] sesja, int magazyn_id, int sku_id, int grupa_id)
        {
            DataSet dane = new DataSet();
            DataTable produkt = new DataTable();
            produkt.Columns.Add(new DataColumn("sku_id", typeof(string)));
            produkt.Columns.Add(new DataColumn("grupa_id", typeof(string)));
            DataRow produktWiersz = produkt.NewRow();
            produktWiersz["sku_id"] = sku_id;
            produktWiersz["grupa_id"] = grupa_id;
            produkt.Rows.Add(produktWiersz);
            produkt.AcceptChanges();
            dane.Tables.Add(produkt);

            ProduktDetailsWO produktWO = new ProduktDetailsWO();
            Stany produktDetails = new Stany();




            //Pobieranie danych produktu
            try
            {
                var wsWynikProd = _proxy.StanMagazynSkuGrupa(sesja, magazyn_id, dane);

                if (wsWynikProd.status != 0) SesjaExceptionCheck.SesjaCheckThrowIfError(wsWynikProd.status, wsWynikProd.status_opis);

                if (wsWynikProd.dane != null && wsWynikProd.dane.Tables.Count > 0 && wsWynikProd.dane.Tables[0].Rows.Count > 0)
                {
                    DataRow produktRow = wsWynikProd.dane.Tables[0].Rows[0];

                    produktDetails =
                       new Stany()
                       {
                           SKU_ID = produktRow.Field<int>(StanyFieldsEnum.SKU_ID.ToString()),
                           SKU = produktRow.Field<string>(StanyFieldsEnum.SKU.ToString()),
                           NAZWA = produktRow.Field<string>("sku_nazwa"),
                           GRUPA_ID = produktRow.Field<int>(StanyFieldsEnum.GRUPA_ID.ToString()),
                           GRUPA = produktRow.Field<string>(StanyFieldsEnum.GRUPA.ToString()),
                           DOSTEPNE = produktRow.Field<int>("Ilosc_dostepna"),
                           JM = produktRow.Field<string>("J.M."),
                           //SPOSOB_PAKOWANIA = produktRow.Field<string>(StanyFieldsEnum.SPOSOB_PAKOWANIA.ToString()),
                           LIMIT_LOGISTYCZNY = produktRow.Field<string>("LIMIT"),
                           //CENA = produktRow.Field<decimal>(StanyFieldsEnum.CENA.ToString()),
                           Koszt_punktowy = produktRow.Field<decimal>("koszt punktowy"),
                       };
                    produktWO.produkt = produktDetails;

                };

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return produktWO;
        }
        public RezultatObject UploadPhoto(byte[] sesja, int skuID, string sku, string sciezka, string nazwa_zdjecia, byte[] zdjecie_byte)
        {
            RezultatObject rez = new RezultatObject();

            try
            {
                var wsWynik = _proxy.ZdjecieDodaj(sesja, skuID, sku, sciezka, nazwa_zdjecia, zdjecie_byte);
                rez.status = wsWynik.status;
                rez.message = wsWynik.status_opis;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return rez;

        }

        public RezultatObject UploadThumbnail(byte[] sesja, int skuID, string sku, int zdjecieID, byte[] zdjecie_byte)
        {
            RezultatObject rez = new RezultatObject();

            try
            {
                var wsWynik = _proxy.utworzMiniature(sesja, skuID, zdjecieID, zdjecie_byte);

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

        private static string EnumName<T>(T value)
        {
            return Enum.GetName(typeof(T), value);
        }


        internal static T CastTo<T>(object value)
        {
            return value != DBNull.Value ? (T)value : default(T);
        }
        #endregion

    }
}