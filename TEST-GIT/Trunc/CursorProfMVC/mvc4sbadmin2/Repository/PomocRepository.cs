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
    public class PomocRepository : IPomocRepository
    {
        private CursorService _proxy;

        public PomocRepository(CursorService proxy)
        {
            _proxy = proxy;
        }

        #region "WS Methods"
        public RezultatObject SendMail(byte[] sesja, string tytul, string tresc, string nazwaPliku, int zrodloMaila)
        {
            RezultatObject rez = new RezultatObject();

            try
            {
                var wsWynik = _proxy.KontaktMailWyslij(sesja, tytul, tresc, nazwaPliku, zrodloMaila);

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
        public RezultatObject SaveAttachment(byte[] sesja, string nazwaPliku, byte[] plik)
        {
            RezultatObject rez = new RezultatObject();

            try
            {
                var wsWynik = _proxy.ZapiszAtachment(plik, nazwaPliku);

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
        public KontaktDaneDodatkowe PobierzParametrPoNazwie(byte[] sesja, string nazwaParametru)
        {
            KontaktDaneDodatkowe parametr = new KontaktDaneDodatkowe();

            parametr.typParametruID = -1;
            parametr.wartoscParametru = "";

            int status = 0;
            string message = "";

            try
            {
                var wsWynik = _proxy.ParametrWartoscPobierz(sesja, nazwaParametru);

                if (wsWynik.status != 0) SesjaExceptionCheck.SesjaCheckThrowIfError(wsWynik.status, wsWynik.status_opis);

                status = wsWynik.status;
                message = wsWynik.status_opis;

                if (status == 0)
                {
                    parametr.wartoscParametru = wsWynik.wartosc;
                    parametr.typParametruID = wsWynik.typParametruID;
                }
            }
            catch (Exception ex)
            {
                // throw ex;
                message = ex.Message;
                status = -1;
                return parametr;
            }

            return parametr;
        }
        public PomocPobierzPlik PobierzPlik(byte[] sesja, string nazwaPliku)
        {
            PomocPobierzPlik wo = new PomocPobierzPlik();

            int status = 0;
            string message = "";

            try
            {
                var wsWynik = _proxy.PobierzPlik(nazwaPliku);

                if (wsWynik.status != 0) SesjaExceptionCheck.SesjaCheckThrowIfError(wsWynik.status, wsWynik.status_opis);

                status = wsWynik.status;
                message = wsWynik.status_opis;
                wo.NazwaPliku = nazwaPliku;
                wo.plik = wsWynik.plik;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            return wo;
        }
        public PomocPlikiListaWO PobierzListePlikow(byte[] sesja)
        {
            PomocPlikiListaWO wo = new PomocPlikiListaWO();
            IEnumerable<PomocPlikiLista> lista = new List<PomocPlikiLista>();


            try
            {
                var wsWynik = _proxy.PlikiDoPobraniaLista(sesja);

                if (wsWynik.status != 0) SesjaExceptionCheck.SesjaCheckThrowIfError(wsWynik.status, wsWynik.status_opis);

                if (wsWynik.dane.Tables[0].Rows.Count > 0)
                {
                    string sqlQry = "[WYSWIETLAJ_NA_WWW]=1";

                    lista = wsWynik.dane.Tables[0].Select(sqlQry).AsEnumerable()
                    .Select(dr =>
                    new PomocPlikiLista
                    {
                        WYBIERZ = dr.Field<bool>(PomocPlikiDoPobraniaListaFieldsEnum.Wybierz.ToString()),
                        PLIK_ID = dr.Field<int>(PomocPlikiDoPobraniaListaFieldsEnum.plik_id.ToString()),
                        TYTUL = dr.Field<string>(PomocPlikiDoPobraniaListaFieldsEnum.tytul.ToString()),
                        MINIATURKA = dr.Field<string>(PomocPlikiDoPobraniaListaFieldsEnum.miniaturka.ToString()),
                        NAZWA_PLIKU = dr.Field<string>(PomocPlikiDoPobraniaListaFieldsEnum.nazwa_pliku.ToString()),
                        WYSWIETLAJ_NA_WWW = dr.Field<bool>(PomocPlikiDoPobraniaListaFieldsEnum.WYSWIETLAJ_NA_WWW.ToString())

                    }).ToList();
                    wo.pomocPlikiLista = lista.ToArray();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return wo;
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

        #endregion

    }
}