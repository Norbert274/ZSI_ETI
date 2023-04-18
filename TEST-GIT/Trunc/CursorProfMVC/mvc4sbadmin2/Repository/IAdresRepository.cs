using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using nclprospekt.Models;
using System.Data;

namespace nclprospekt.Repository
{
    public interface IAdresRepository
    {
        IEnumerable<Adres> MiastoDlaKodu(byte[] sesja, string kod_pocztowy);
        AdresyWO LoadAdresLista(byte[] sesja, int userIdAdresy, int strona, int iloscNaStronie, string filtr, string sortowanie, bool rosnaco);
       IEnumerable<AdresLista> AdresyOdczytaj(byte[] sesja, int id);
         AdresDetaleWO AdresEdytuj(byte[] sesja, int id);
        RezultatObject AdresEdytujAnuluj(byte[] sesja, int id);
        RezultatObject AdresEdytujZapisz(byte[] sesja, AdresDetaleWO adresWO, bool pozostaw_blokade);
        RezultatObject AdresUsun(byte[] sesja, int id);
    }
}