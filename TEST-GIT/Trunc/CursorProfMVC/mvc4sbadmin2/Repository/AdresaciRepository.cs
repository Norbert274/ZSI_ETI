using nclprospekt.Exceptions;
using nclprospekt.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace nclprospekt.Repository
{
    public class AdresaciRepository:IAdresaciRepository
    {



        public Adresaci pobierzAdresatow(byte[] sesja)
        {
            Adresaci Adresaci = new Adresaci();

            int status = 0;
            string message = "";
            DataSet adresaciDane = new DataSet();

            try
            {
                NCL_WS.NewsleterOdczytajWynik wsWynik = null;
                NCL_WS.CursorService proxy = new NCL_WS.CursorService();

                wsWynik = proxy.NewsleterOdczytaj(sesja,0);

                status = wsWynik.status;
                message = wsWynik.status_opis;


                if (status == 0)
                {
                    adresaciDane = wsWynik.dane;

                    if (adresaciDane != null && adresaciDane.Tables.Count > 0)
                    {
                        Adresaci.adresaci = addListAdresat(adresaciDane.Tables[3]);
                    }
                }
                else
                {
                    status = nclprospekt.Exceptions.SesjaExceptionCheck.SesjaCheck(status, message);
                    if (status == -2)
                    {
                        SesjaException sx = new SesjaException(message);
                        throw sx;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Adresaci;
        }

        private List<Adresat> addListAdresat(DataTable tabela)
        {
            List<Adresat> lista = new List<Adresat>();

            foreach (var row in tabela.AsEnumerable())
	        {

                Adresat adresat = new Adresat()
                {
                    UserId = row.Field<int>("USER_ID"),
                    Imie = row.Field<string>("IMIE"),
                    Nazwisko = row.Field<string>("NAZWISKO"),
                    Nazwa = row.Field<string>("NAZWA"),
                    GrupaNazwa=row.Field<string>("GRUPA"),
                    GrupaId = row.Field<int>("GRUPA_ID"),
                    TypNazwa=row.Field<string>("TYP"),
                    TypId=row.Field<int>("TYP_ID"),
                    WielkoscNazwa = row.Field<string>("WIELKOSC"),
                    WielkoscId = row.Field<int>("WIELKOSC_ID")
                };

                lista.Add(adresat);
	        }

            return lista;
        }
        
    }
}