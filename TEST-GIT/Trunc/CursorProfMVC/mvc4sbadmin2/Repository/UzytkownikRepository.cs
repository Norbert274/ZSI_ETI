using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using nclprospekt.Models;
using nclprospekt.DAL;
using nclprospekt.Exceptions;
using Dapper;
using Ninject;
using nclprospekt.Objects;
using nclprospekt.NCL_WS;

namespace nclprospekt.Repository
{

    public class AccountRepositoryFactory
    {
        private IUzytkownikRepository  _repository;
        
        public AccountRepositoryFactory()
        {
            _repository = _repository ?? new UzytkownikRepository();
        }

        public IUzytkownikRepository GetRepository()
        {
            return _repository;
        }
    }

    public class UzytkownikRepository:IUzytkownikRepository
    {

        #region "Manage Methods"
  
        public bool ChangePassword(byte[] sesja, string oldPassword, string newPassword)
        {
            int status = 0;
            string message = "";
            
            NCL_WS.ZmienHasloWynik retVal = new NCL_WS.ZmienHasloWynik();
            NCL_WS.CursorService proxy = new NCL_WS.CursorService();

            try
            {
                retVal = proxy.ZmienHaslo(sesja, oldPassword, newPassword);
                status = retVal.status;
                message = retVal.status_opis;
            }
            catch (Exception ex)
            { 
                Exception ex2 = new Exception(ex.Message);
                throw ex2;
            }

            if (status != 0)
            {
                status = nclprospekt.Exceptions.SesjaExceptionCheck.SesjaCheck(status, message);
                if (status == -2)
                {
                    SesjaException sx = new SesjaException(message);
                    throw sx;
                }
                else
                {
                    Exception ex = new Exception(message);
                    throw ex;
                }
            }

            return (status == 0) ? true: false;
        }

        public SecurityToken GetNewSecurityToken(string name, string password, DateTime validUntil)
        {
            int status = 0;
            string message = "";
            //byte[] securityToken = new byte[16];
            SecurityToken retVal =  new SecurityToken();
            
            try
            {
                NCL_WS.ZalogujWynik sesjaWynik = default(NCL_WS.ZalogujWynik);
                NCL_WS.CursorService proxy = new NCL_WS.CursorService();

                sesjaWynik = proxy.Zaloguj_szablon(name, password, "1", "byle_co");

                retVal.token = sesjaWynik.sesja;
                retVal.czyPierwszy = sesjaWynik.czy_pierwszy;
                status = sesjaWynik.status;
                message = sesjaWynik.status_opis;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

            if (status != 0)
            {
                status = nclprospekt.Exceptions.SesjaExceptionCheck.SesjaCheck(status, message);
                if (status == -2)
                {
                    SesjaException sx = new SesjaException(message);
                    throw sx;
                }
                else
                {
                    Exception ex = new Exception(message);
                    throw ex;
                }
            }

            return retVal;
        }

        #endregion

        #region "Query Methods"

        public Uzytkownik GetByUserName(byte[] sesja, string login)
        {
            int status = 0;
            string message = "";
            Uzytkownik uzytkownik = new Uzytkownik();

            try
            {
                NCL_WS.CursorService proxy = new NCL_WS.CursorService();
                NCL_WS.UzytkownikDaneWynik daneUzytkownika = proxy.PobierzDaneUzytkownika(sesja, login);
                status=daneUzytkownika.status;
                message = daneUzytkownika.status_opis;

                if (status == 0)
                { 
                DataRow dr = daneUzytkownika.dane.Tables[0].Rows[0];
                uzytkownik.Uzytkownik_Id = dr.Field<int>(UzytkownikDaneFieldsEnum.Uzytkownik_Id.ToString());
                uzytkownik.Imie = dr.Field<string>(UzytkownikDaneFieldsEnum.Imie.ToString());
                uzytkownik.Nazwisko = dr.Field<string>(UzytkownikDaneFieldsEnum.Nazwisko.ToString());
                uzytkownik.Nazwa = dr.Field<string>(UzytkownikDaneFieldsEnum.Nazwa.ToString());
                uzytkownik.Haslo = dr.Field<string>(UzytkownikDaneFieldsEnum.Password.ToString());
                uzytkownik.Email = dr.Field<string>(UzytkownikDaneFieldsEnum.Email.ToString());
                uzytkownik.IsAdmin = Convert.ToBoolean(dr.Field<int>(UzytkownikDaneFieldsEnum.IsAdmin.ToString()));
                uzytkownik.IsSuperUser = Convert.ToBoolean(dr.Field<int>(UzytkownikDaneFieldsEnum.IsSuperUser.ToString()));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (status != 0)
            {
                status = nclprospekt.Exceptions.SesjaExceptionCheck.SesjaCheck(status, message);
                if (status == -2)
                {
                    SesjaException sx = new SesjaException(message);
                    throw sx;
                }
                else
                {
                    Exception ex = new Exception(message);
                    throw ex;
                }
            }
            
            return uzytkownik;
        }

        public Uzytkownik EditUser(byte[] sesja, int user_id)
        {
            int status = 0;
            string message = "";

            Uzytkownik uzytkownik = new Uzytkownik();

            try
            {
                NCL_WS.CursorService proxy = new NCL_WS.CursorService();
                NCL_WS.UserEdytujWynik daneUzytkownika = proxy.UserEdytuj(sesja, user_id);
                status = daneUzytkownika.status;
                message = daneUzytkownika.status_opis;
                                

                if (status == 0)
                {
                    uzytkownik.Imie = daneUzytkownika.imie;
                    uzytkownik.Nazwisko = daneUzytkownika.nazwisko;
                    uzytkownik.Nazwa = daneUzytkownika.nazwa;
                    uzytkownik.TelefonKomorkowy = int.Parse(daneUzytkownika.telkom);
                    uzytkownik.Email = daneUzytkownika.email;
                    uzytkownik.Nazwa = daneUzytkownika.nazwa;
                    uzytkownik.Login = daneUzytkownika.login;
                    //uzytkownik.HASLO=daneUzytkownika.haslo;
                    uzytkownik.BLOKADA_ID = daneUzytkownika.blokada_id;
                    uzytkownik.PRZELOZONY_ID = daneUzytkownika.przelozony_id;
                    uzytkownik.PRZELOZONY_NAZWA = daneUzytkownika.przelozony_nazwa;
                    uzytkownik.MAGAZYN_ID = daneUzytkownika.magazyn_id;
                    uzytkownik.MAGAZYN_NAZWA = daneUzytkownika.magazyn_nazwa;
                    uzytkownik.ADRESY_ILOSC = daneUzytkownika.adresy_ilosc;
                    uzytkownik.CZY_MAILE = (daneUzytkownika.czy_maile == 1) ? true : false;

                    uzytkownik.TYP_ID = daneUzytkownika.typ_id;
                    uzytkownik.WIELKOSC_ID = daneUzytkownika.wielkosc_id;
                    uzytkownik.OBSZAR_SPRZEDAZY_ID = daneUzytkownika.obszar_sprzedazy_id;
                    uzytkownik.SIEC_SPRZEDAZY_ID = daneUzytkownika.siec_sprzedazy_id;
                    uzytkownik.REGION_SPRZEDAZY_ID = daneUzytkownika.region_sprzedazy_id;
                    uzytkownik.ZESPOL_SPRZEDAZY_ID = daneUzytkownika.zespol_sprzedazy_id;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (status != 0)
            {
                status = nclprospekt.Exceptions.SesjaExceptionCheck.SesjaCheck(status, message);
                if (status == -2)
                {
                    SesjaException sx = new SesjaException(message);
                    throw sx;
                }
                else
                {
                    Exception ex = new Exception(message);
                    throw ex;
                }
            }

            return uzytkownik;
        }

        public RezultatObject EditUserSave(byte[] sesja, string imie, string nazwisko, string nazwa, string telkom, string email, string login,
                                        string haslo, int przelozony_id, int magazynid, int czy_maile, int blokada_id, bool pozostaw_blokade, DataSet grupy, string rola,
                                        int typ_id, int wielkosc_id, int obszar_sprzedazy_id, int siec_sprzedazy_id, int region_sprzedazy_id, int zespol_sprzedazy_id,
                                        int czy_limit_zamowien,int max_ilosc_zamowien,int typ_okres_zamowien_id)
        {
            RezultatObject rez = new RezultatObject();

            try
            {
                NCL_WS.CursorService proxy = new NCL_WS.CursorService();
                NCL_WS.UserEdytujZapiszWynik daneUzytkownika = proxy.UserEdytujZapisz(sesja, imie, nazwisko, nazwa, telkom, email, login,
                            haslo, przelozony_id, magazynid, czy_maile, blokada_id, pozostaw_blokade, grupy, rola, typ_id, wielkosc_id,
                            obszar_sprzedazy_id, siec_sprzedazy_id, region_sprzedazy_id, zespol_sprzedazy_id,
                            czy_limit_zamowien, max_ilosc_zamowien, typ_okres_zamowien_id);

                rez.status = daneUzytkownika.status;
                rez.message = daneUzytkownika.status_opis;
                rez.blokada_id = daneUzytkownika.blokada_id;
        
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (rez.status != 0)
            {
                rez.status = nclprospekt.Exceptions.SesjaExceptionCheck.SesjaCheck(rez.status, rez.message);
                if (rez.status == -2)
                {
                    SesjaException sx = new SesjaException(rez.message);
                    throw sx;
                }
                else
                {
                    Exception ex = new Exception(rez.message);
                    throw ex;
                }
            }

            return rez;
        }

        public RezultatObject EditUserBasicDataSave(byte[] sesja, string imie, string nazwisko, string nazwa, string telkom, string email, string login,
                                        string haslo, int czy_maile, int blokada_id, bool pozostaw_blokade)
        {
            RezultatObject rez = new RezultatObject();
            try
            {
                NCL_WS.CursorService proxy = new NCL_WS.CursorService();
                NCL_WS.UserEdytujPodstawoweDaneZapiszWynik daneUzytkownika = proxy.UserEdytujPodstawoweDaneZapisz(sesja, imie, nazwisko, nazwa, telkom, email, login,
                            haslo,  czy_maile, blokada_id, pozostaw_blokade);

                rez.status = daneUzytkownika.status;
                rez.message = daneUzytkownika.status_opis;
                rez.blokada_id = daneUzytkownika.blokada_id;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (rez.status != 0)
            {
                rez.status = nclprospekt.Exceptions.SesjaExceptionCheck.SesjaCheck(rez.status, rez.message);
                if (rez.status == -2)
                {
                    SesjaException sx = new SesjaException(rez.message);
                    throw sx;
                }
                else
                {
                    Exception ex = new Exception(rez.message);
                    throw ex;
                }
            }

            return rez;


        }

        public IEnumerable<Role> GetRolesForUser(byte[] sesja)
        {
            int status = 0;
            string message = "";

            IEnumerable<Role> role = null;

            // Get a proxy to the data service provider
            NCL_WS.CursorService proxy = new NCL_WS.CursorService();    //Service1SoapClient();

            try
            {
                //NCL_WS.UserRolaWynik rolaLista = proxy.UserRola(sesja);
                NCL_WS.SprawdzFunkcjeWynik rolaLista = proxy.SprawdzFunkcje(sesja);
                status = rolaLista.status;
                message = (rolaLista.status_opis == null) ? "" : rolaLista.status_opis;

                if (rolaLista.status == 0 && rolaLista.dane != null && rolaLista.dane.Tables.Count > 0)
                {
                    //dostepne zwracane pola
                    //FUNCKJE_ID
                    //BUTTON_NAZWA
                    //FORMA_NAZWA
                    //WLACZ
                    
                    DataTable tblRole = rolaLista.dane.Tables[0].Copy();
                    role = tblRole.AsEnumerable()
                        //.Skip(1)
                    .Select(dr =>
                    new Role(
                         dr.Field<int>("FUNKCJE_ID"),
                        dr.Field<string>("BUTTON_NAZWA"),
                        dr.Field<string>("BUTTON_NAZWA")
                    )
                    ).ToList();

                    //DataAkcji = dr.Field<DateTime>("DataAKcji")
                }
            }
            catch (Exception ex)
            {
                status = -2;
                message = ex.Message;
            }
            finally
            {
            }

            if (status != 0)
            {
                status = nclprospekt.Exceptions.SesjaExceptionCheck.SesjaCheck(status, message);
                if (status == -2)
                {
                    SesjaException sx = new SesjaException(message);
                    throw sx;
                }
                else
                {
                    Exception ex = new Exception(message);
                    throw ex;
                }
            }		

            return role;
        }

        //public IEnumerable<Role> GetRolesForUser(int id)
        //{
        //    IEnumerable<Role> role = null;
        //    return role;
        //}

        //public IEnumerable<Role> GetRolesForUser(Uzytkownik user)
        //{
        //    IEnumerable<Role> role = null;
        //    return role;         
        //}


        public Uzytkownik UserNotyfikacjePobierz(byte[] sesja, int user_id)
        {
            RezultatObject rez = new RezultatObject();
            rez.status = -1;
            rez.message = "Brak komunikacji z serwerem lub błędne dane wynikowe";

            Uzytkownik uzytkownik = new Uzytkownik();
            UzytkownikNotyfikacje uzytkownikNotyfikacja ;
            IList<UzytkownikNotyfikacje> uzytkownikNotyfikacjaLista = new List<UzytkownikNotyfikacje>();
            try
            {
                NCL_WS.CursorService proxy = new NCL_WS.CursorService();
                NCL_WS.NotyfikacjeOdczytajWynik notyfikacjeUzytkownika = proxy.NotyfikacjeOdczytaj(sesja, user_id);
                rez.status = notyfikacjeUzytkownika.status;
                rez.message = notyfikacjeUzytkownika.status_opis;


                if (rez.status == 0)
                {

                    if (notyfikacjeUzytkownika.dane != null && notyfikacjeUzytkownika.dane.Tables.Count > 0 && notyfikacjeUzytkownika.dane.Tables[0].Rows.Count > 0)
                    {
                        DataTable dtNotyfikacje = notyfikacjeUzytkownika.dane.Tables[0];
                        DataRow dr = dtNotyfikacje.Rows[0];


                        foreach (DataColumn col in dtNotyfikacje.Columns)
                        {
                            if (col.Ordinal > 3)
                            {
                                uzytkownikNotyfikacja = new UzytkownikNotyfikacje();
                                uzytkownikNotyfikacja.user_Id = dr.Field<int>("USER_ID");
                                uzytkownikNotyfikacja.notyfikacja = col.Caption;
                                uzytkownikNotyfikacja.wlacz = (bool)dr[col];
                                uzytkownikNotyfikacjaLista.Add(uzytkownikNotyfikacja);
                            }
                        }

                        uzytkownik.notyfikacje = uzytkownikNotyfikacjaLista;
                    }
                }
                else
                {
                    rez.status = -1;
                    rez.message = "Brak danych o notyfikacji";
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (rez.status != 0)
            {
                rez.status = nclprospekt.Exceptions.SesjaExceptionCheck.SesjaCheck(rez.status, rez.message);
                if (rez.status == -2)
                {
                    SesjaException sx = new SesjaException(rez.message);
                    throw sx;
                }
                else
                {
                    Exception ex = new Exception(rez.message);
                    throw ex;
                }
            }

            return uzytkownik;
        }

        public RezultatObject UserNotyfikacjeZapisz(byte[] sesja, DataTable notyfikacje)
        {
            RezultatObject rez = new RezultatObject();
            try
            {
                NCL_WS.CursorService proxy = new NCL_WS.CursorService();


                if (notyfikacje.TableName == "") { notyfikacje.TableName = "notyfikacje"; };

                NCL_WS.NotyfikacjeZapiszWynik notyfikacjeWynik = proxy.NotyfikacjeZapisz(sesja, notyfikacje);

                rez.status = notyfikacjeWynik.status;
                rez.message = notyfikacjeWynik.status_opis;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (rez.status != 0)
            {
                rez.status = nclprospekt.Exceptions.SesjaExceptionCheck.SesjaCheck(rez.status, rez.message);
                if (rez.status == -2)
                {
                    SesjaException sx = new SesjaException(rez.message);
                    throw sx;
                }
                else
                {
                    Exception ex = new Exception(rez.message);
                    throw ex;
                }
            }

            return rez;


        }




 public RezultatObject ForgottenPassowrdSendToken(string userName)
        {
            RezultatObject rez = new RezultatObject();
            rez.message = "Brak odpowiedzi od serwera";
            rez.status = -1;

            try
            {
                NCL_WS.CursorService proxy = new NCL_WS.CursorService();
                NCL_WS.UserResetHaslaGenerujTokenWynik wsWynik = proxy.UserResetHaslaGenerujToken(userName);

                rez.status = wsWynik.status;
                rez.message = wsWynik.status_opis;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (rez.status != 0)
            {
                rez.status = nclprospekt.Exceptions.SesjaExceptionCheck.SesjaCheck(rez.status, rez.message);
                if (rez.status == -2)
                {
                    SesjaException sx = new SesjaException(rez.message);
                    throw sx;
                }
                else
                {
                    Exception ex = new Exception(rez.message);
                    throw ex;
                }
            }

            return rez;


        }

        public RezultatObject ResetPasswordByToken(string token, string newPassword)
        {
            RezultatObject rez = new RezultatObject();
            rez.message = "Brak odpowiedzi od serwera";
            rez.status = -1;

            NCL_WS.UserResetHaslaUstawNoweHasloWynik retVal = new NCL_WS.UserResetHaslaUstawNoweHasloWynik();
            NCL_WS.CursorService proxy = new NCL_WS.CursorService();

            try
            {
                retVal = proxy.UserResetHaslaUstawNoweHaslo(token, newPassword);
                rez.message = retVal.status_opis;
                rez.status = -retVal.status;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (rez.status != 0)
            {
                rez.status = nclprospekt.Exceptions.SesjaExceptionCheck.SesjaCheck(rez.status, rez.message);
                if (rez.status == -2)
                {
                    SesjaException sx = new SesjaException(rez.message);
                    throw sx;
                }
                else
                {
                    Exception ex = new Exception(rez.message);
                    throw ex;
                }
            }

            return rez;

        }



        #endregion


       
        
    }

}