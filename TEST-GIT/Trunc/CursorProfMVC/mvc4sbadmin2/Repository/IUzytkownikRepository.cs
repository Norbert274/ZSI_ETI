using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using nclprospekt.Models;
using nclprospekt.Objects;
using System.Data;
using nclprospekt.NCL_WS;
namespace nclprospekt.Repository
{
    public interface IUzytkownikRepository
    {

        IEnumerable<Role> GetRolesForUser(byte[] sesja);
        SecurityToken GetNewSecurityToken(string name, string password, DateTime validUntil);
        bool ChangePassword(byte[] sesja, string OldPassword, string NewPassword);
        Uzytkownik GetByUserName(byte[] sesja, string login);
        Uzytkownik EditUser(byte[] sesja, int user_id);
        RezultatObject EditUserSave(byte[] sesja, string imie, string nazwisko, string nazwa, string telkom, string email, string login, string haslo, int przelozony_id, int magazynid, int czy_maile, int blokada_id, bool pozostaw_blokade, DataSet grupy, string rola, int typ_id, int wielkosc_id, int obszar_sprzedazy_id, int siec_sprzedazy_id, int region_sprzedazy_id, int zespol_sprzedazy_id, int czy_limit_zamowien, int max_ilosc_zamowien, int typ_okres_zamowien_id);
        RezultatObject EditUserBasicDataSave(byte[] sesja, string imie, string nazwisko, string nazwa, string telkom, string email, string login, string haslo, int czy_maile, int blokada_id, bool pozostaw_blokade);
        Uzytkownik UserNotyfikacjePobierz(byte[] sesja, int user_id);
        RezultatObject UserNotyfikacjeZapisz(byte[] sesja, DataTable notyfikacje);
        RezultatObject ForgottenPassowrdSendToken(string userName);
        RezultatObject ResetPasswordByToken(string token, string newPassword);

    }

}