using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using nclprospekt.Models;
using System.Data;

namespace nclprospekt.Repository
{
    public interface IZamowienieRepository
    {

        ZamowienieWczytaneWO FindById(int id, byte[] sesja);
        ZamowienieWczytaneWO PobierzAdresKurier(byte[] sesja);
        ZamowienieWczytaneWO PobierzAdresZdefiniowanyZamowienia(byte[] sesja, int idZamowienia, int idAdresu);
        DataTable LoadZamowienieListaTbl(byte[] sesja, DateTime dataOd, DateTime dataDo, string filtr);
        IEnumerable<ZamowienieLista> LoadZamowienieLista(byte[] sesja, DateTime dataOd, DateTime dataDo, string filtr,int zamowienieStatusID);
        RezultatObject SaveCart(byte[] sesja, int blokada_id, int magazyn_id, int magazyn_docelowy_id, int adres_id, string nazwa,
                    string adres, string kod, string miasto, string osoba_kontaktowa, string telefon_kontaktowy, string uwagi,
                    int typ_zamowienia, DataSet dane, DateTime data_realizacji, int oddzial_docelowy_id, int zapisz_dane_dpd,
                    int dok_zw, int os_pryw, int prz_zw, decimal cod, decimal dpd_wartosc, string dpd_typ, string odbiorcy,
                    string grupy, string typy, string wielkosc, string warunek); //, string email_odbiorcy, int czy_zamowienie_specjalne);
        RezultatObject ZatwierdzKoszyk(byte[] sesja, int blokada_id);
		IEnumerable<ZamowienieStatusyLista> LoadZamowienieStatusyLista(byte[] sesja);
        ZamowienieWczytaneWO SprawdzSkuStanGrupa(byte[] sesja, int idMagazynu, DataSet pozycjeDodane);

        RezultatObject AddToCart(byte[] sesja, ProduktDetailsWO produkt);

        RezultatObject SaveCartAdjustModel(byte[] sesja, ZamowienieWczytaneWO model);
    }
}