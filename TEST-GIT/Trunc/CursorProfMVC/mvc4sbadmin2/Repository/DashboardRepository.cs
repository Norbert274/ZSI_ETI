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

namespace nclprospekt.Repository
{
    public class DashboardRepository : IDashboardRepository
    {

        #region "WS Methods"

        public DashboardWO CountStatistics(byte[] sesja)
        {
            int status = 0;
            string message = "";

            DashboardWO dashWO = new DashboardWO();
            IEnumerable<DashboardWykres> wykresPozycje = null;

            dashWO.IloscAdresow = 0;
            dashWO.IloscProduktow = 0;
            dashWO.IloscWKoszyku = 0;
            dashWO.IloscZamowien = 0;

            try
            {
                NCL_WS.StatystykiWczytajWynik wsWynik = null;
                NCL_WS.CursorService proxy = new NCL_WS.CursorService();
                wsWynik = proxy.StatystykiWczytaj(sesja);

                status = wsWynik.status;
                message = wsWynik.status_opis;

                if (status == 0)
                {
                    dashWO.IloscAdresow = wsWynik.IloscAdresow;
                    dashWO.IloscProduktow = wsWynik.IloscProduktow;
                    dashWO.IloscWKoszyku = wsWynik.IloscWKoszyku;
                    dashWO.IloscZamowien = wsWynik.IloscZamowien;

                    if (wsWynik.dane != null && wsWynik.dane.Tables.Count > 0)
                    { 
                    wykresPozycje = wsWynik.dane.Tables[0].AsEnumerable()
                    .Select(dr =>
                            new DashboardWykres
                            {
                                miesiac = dr.Field<string>("DATA_ZAMOWIENIA"),
                                ZamowienRealizowanych = dr.Field<int>("ILOSC_ZAMOWIEN_REALIZOWANYCH"),
                                ZamowienRoboczych = dr.Field<int>("ILOSC_ZAMOWIEN_ROBOCZYCH")
                            }).ToList();

                    dashWO.daneWykres = wykresPozycje.ToArray();
                    }                


                }
            }
            catch (Exception ex)
            {
               // throw ex;
                message = ex.Message;
                status = -1;
                return dashWO;
            }
            finally
            { }

            if (status != 0)
            {
                status = nclprospekt.Exceptions.SesjaExceptionCheck.SesjaCheck(status, message);
                if (status == -2) 
                {
                    SesjaException sx = new SesjaException(message);
                    throw sx;
                }
                
            }

            return dashWO;
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