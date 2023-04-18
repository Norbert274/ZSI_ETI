Public Class ZalogujWynik
    Public sesja As Byte() 'zwracany identyfikator sesjii
    Public uzytkownik_id As Integer
    Public uzytkownik As String
    Public telefon As String
    Public database As String
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
    Public czy_pierwszy As Integer
    Public komunikat_dla_uzytkownika As String
End Class


Public Class Komunikat
    Public id As Integer 'identyfikator komunikatu (aplikacja potwierdza przy jego użyciu odbiór komunikatu)
    Public status As Integer '0 - ok, 1 - uwaga, -1 - błąd
    Public status_opis As String 'opis słowny wyniku wywołania (w przypdaku powodzenia często niepokazywany)
End Class

Public Class SprawdzFunkcjeWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class StanWynik
    Public dane As DataSet
    Public magazyn_id As Integer
    Public ilosc_stron As Integer
    Public ilosc_total_rekordow As Integer
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat
    Public opis_rozszerzony As Integer
End Class

Public Class UserStronaWynik
    Public dane As DataSet
    Public iloscStron As Integer
    Public ilosc_total_rekordow As Integer
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat
End Class

Public Class GrupaListaWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat
End Class

Public Class UserUsunWynik
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat
End Class

Public Class UserEdytujWynik
    Public imie As String
    Public nazwisko As String
    Public nazwa As String
    Public telkom As String
    Public email As String
    Public login As String
    Public haslo As Boolean
    Public grupy_obszary As DataSet
    Public blokada_id As Integer
    Public przelozony_id As Integer
    Public przelozony_nazwa As String
    Public magazyn_id As Integer
    Public magazyn_nazwa As String
    Public adresy_ilosc As Integer
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat
    Public czy_maile As Integer
    Public rola As String
    Public typ_id As Integer
    Public wielkosc_id As Integer
    Public obszar_sprzedazy_id As Integer
    Public siec_sprzedazy_id As Integer
    Public region_sprzedazy_id As Integer
    Public zespol_sprzedazy_id As Integer
    Public czy_limit_zamowien As Integer
    Public max_ilosc_zamowien As Integer
    Public typ_okres_zamowien_id As Integer
End Class

Public Class UserEdytujZapiszWynik
    Public status As Integer
    Public status_opis As String
    Public blokada_id As Integer
    Public komunikaty() As Komunikat
End Class

Public Class ZmienHasloUzytkownikaWynik
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class AdresEdytujZapiszWynik
    Public status As Integer
    Public status_opis As String
    Public blokada_id As Integer
    Public komunikaty() As Komunikat
End Class

Public Class AdresEdytujWynik
    Public nazwa As String
    Public adres As String
    Public kod As String
    Public miasto As String
    Public domyslny As Integer
    Public blokada_id As Integer
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat
End Class

Public Class AdresStronaWynik
    Public dane As DataSet
    Public iloscStron As Integer
    Public ilosc_total_rekordow As Integer
    Public userAdresy As String
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat
End Class

Public Class AdresUsunWynik
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat
End Class

Public Class MagazynyOdczytajWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class FunkcjaListaWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat
End Class

Public Class ZamowienieStronaWynik
    Public dane As DataSet
    Public ilosc_stron As Integer
    Public ilosc_total_rekordow As Integer
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class AnulujZamowienieWynik
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class AdresyOdczytajWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class StanSkuWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat
End Class

Public Class ZamowienieWczytajWynik
    Public dane As DataSet
    Public zamowienie_id As Integer
    Public magazyn_id As Integer
    Public blokada_id As Integer
    Public wlasciciel_nazwa As String
    Public tryb_pracy As Integer '1 - edycja własnego koszyka, 2 - podgląd obcego koszyka, 3 - podgląd zamówienia
    Public zamowienie_status As String
    Public zamowienie_status_opis As String
    Public typ_zamowienia As Integer '1 - transfer, 2 - odbior własny, 3 - dostawa na adres zdefiniowany,
    '4 - dostawa na adres inny, 5 - wydanie specjalne na utylizację, 6 - wydanie specjalne na remont standów,
    '7 - wydanie specjalne na klasyczny Copacking, 8 - zamówienie grupowe
    Public centrala As Boolean 'czy użytkownik jest członkiem grupy centrala
    Public magazyn_docelowy_id As Integer
    Public oddzial_docelowy_id As Integer
    Public adres_id As Integer
    Public nazwa As String
    Public adres As String
    Public kod As String
    Public miasto As String
    Public ilosc_adresow As Integer 'dla zamówienia centralnego
    Public osoba_kontaktowa As String
    Public telefon_kontaktowy As String
    Public uwagi As String
    Public status As Integer
    Public status_opis As String
    Public data_realizacji As DateTime
    Public wartosc As Decimal
    Public limit As Decimal
    Public koszt_dostawy As Decimal
    Public maDaneDpd As Integer
    Public DokZw As Integer
    Public PrzZw As Integer
    Public OsPryw As Integer
    Public DPDWartosc As Decimal
    Public DPDKwotaCOD As Decimal
    Public DPDTyp As String
    Public users_ids As String
    Public grupy As String
    Public typy As String
    Public wielkosci As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class KoszykZapiszWynik
    Public dane As DataSet
    Public zamowienie_id As Integer
    Public blokada_id As Integer
    Public user_nazwa As String
    Public koszyk As Boolean
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class MinimalnaDataRealizacjiWynik
    Public status As Integer
    Public status_opis As String
    Public data As Date
    Public komunikaty() As Komunikat
End Class

Public Class ZdjecieOdczytajWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class StanSkuGrupaWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat
End Class

Public Class PodzialGrupaZapiszWynik
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat
End Class

Public Class PodzialGrupaOdczytajWynik
    Public dane As DataSet
    Public sku As String
    Public sku_nazwa As String
    Public magazyn_nazwa As String
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat
End Class

Public Class UserLimityWczytajWynik
    Public dane As DataSet
    Public iloscStron As Integer
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat
End Class

Public Class OddzialyOdczytajWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class UserLimityZapiszWynik
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat
End Class

Public Class KoszykZatwierdzWynik
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class UserLimityZapiszWybranymWynik
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat
End Class

Public Class UserLimityWczytajWybranychWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat
End Class

Public Class SkuStanFiltryWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat
End Class

Public Class ZmienHasloWynik
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class


Public Class SKUEdytujWynik
    Public sku_nazwa As String
    Public sku As String
    Public opis As String
    Public cena As Decimal
    Public zdjecie As Byte()
    Public wysokosc As Integer
    Public szerokosc As Integer
    Public glebokosc As Integer
    Public waga As Decimal
    Public marka As String
    Public kategoria As String
    Public branza As String
    Public jm As String
    Public max_ilosc As Integer
    Public czy_mozna_zamawiac As Integer
    Public czy_nowosc As Integer
    Public czy_limit_wydan As Integer
    Public blokada_id As Integer
    Public opis_rozszerzony As String
    Public max_ilosc_zamowien As Integer
    Public typ_okres_zamowien_id As Integer
    Public sztuk_w_opakowaniu As Integer
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat
End Class

Public Class SKUWczytajWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat
End Class

Public Class SKUEdytujFiltryWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat
End Class

Public Class UserEdytujPodstawoweDaneZapiszWynik
    Public status As Integer
    Public status_opis As String
    Public blokada_id As Integer
    Public komunikaty() As Komunikat
End Class


Public Class ZapiszAtachmentWynik
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class UsunAtachmentWynik
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class WyslijAtachmentWynik
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class GrupyPokazWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat
End Class

Public Class GrupyDodajNowaWynik
    Public grupa_id As Integer
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat
End Class

Public Class GrupyEdycjaWybranaWynik
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat
End Class

Public Class GrupaUsunWynik
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat
End Class

Public Class NewsleterOdczytajWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class NewsleterWyslijWynik
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat
End Class

Public Class NewsleterListaWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class WiadomosciListaWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class WiadomoscDodajNowaWynik
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat
End Class

Public Class WiadomoscUsunWynik
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat
End Class

Public Class PlikiDoPobraniaListaWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public maxRozmiarPliku As Integer
    Public centrala As Integer
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class SKUEdytujZapiszWynik
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class ZwrotyWczytajWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class ZwrotyZapiszWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class ZapiszMiniaturkeWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class PobierzPlikWynik
    Public status As Integer
    Public status_opis As String
    Public plik() As Byte
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class ZwrotyBezZamowienWczytajWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class ZwrotyBezZamowienWczytajZamowieniaWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class PlikiDoPobraniaUsunWynik
    Public status As Integer
    Public status_opis As String
    Public centrala_out As Integer
    Public komunikaty() As Komunikat
End Class

Public Class UserRolaWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class ZwrotZamowieniePrzypiszWynik
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class PierwszeLogowanieWczytajWynik
    Public telefon As Integer
    Public email As String
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class ZmienHasloTelEmailWynik
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class KontaktMailWyslijWynik
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat
End Class

Public Class ZdjecieDodajWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class ZdjeciaWczytajWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class ZdjecieUsunWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class utworzMiniatureWynik
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class UserWielkoscTypWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class KategorieSkuOdczytajWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class CzyIstniejaWszystkieSkuWynik
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat
End Class

Public Class PlikZamowieniaExcelZapiszWynik
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat
End Class

Public Class GrupyZamowieniaExcelOdczytajWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class PodajSkuIdZSKUWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class PlikiExcelaZamowieniaWczytajWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public ilosc_stron As Integer
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class PlikZamowieniaExcelPobierzWynik
    Public status As Integer
    Public status_opis As String
    Public dane As DataSet
    Public blokada_id As Integer
    Public komunikaty() As Komunikat
End Class

Public Class AdresyZamowieniaOdczytajWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public wyswietlaj_combo As Boolean
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class CzyUserOddzialWynik
    Public status As Integer
    Public status_opis As String
    Public czy_oddzial As Integer
    Public komunikaty() As Komunikat
End Class

Public Class ZamowienieOdbiorcyOdczytajWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class ZamowienieRoboczeUsunWynik
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat
End Class

Public Class CzyBiuroWynik
    Public czy_biuro As Integer
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class SlownikZespolySprzedazyOdczytajWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class SlownikZespolSprzedazyEdytujWynik
    Public status As Integer
    Public status_opis As String
End Class

Public Class SlownikZespolSprzedazyUsunWynik
    Public status As Integer
    Public status_opis As String
End Class

Public Class SlownikSieciSprzedazyOdczytajWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class SlownikSiecSprzedazyEdytujWynik
    Public status As Integer
    Public status_opis As String
End Class

Public Class SlownikSiecSprzedazyUsunWynik
    Public status As Integer
    Public status_opis As String
End Class

Public Class SlownikObszarySprzedazyOdczytajWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class SlownikObszarSprzedazyEdytujWynik
    Public status As Integer
    Public status_opis As String
End Class

Public Class SlownikObszarSprzedazyUsunWynik
    Public status As Integer
    Public status_opis As String
End Class

Public Class SlownikWielkoscOdczytajWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class SlownikWielkoscEdytujWynik
    Public status As Integer
    Public status_opis As String
End Class

Public Class SlownikWielkoscUsunWynik
    Public status As Integer
    Public status_opis As String
End Class

Public Class KomunikatEdytujWynik
    Public status As Integer
    Public status_opis As String
End Class

Public Class KomunikatUsunWynik
    Public status As Integer
    Public status_opis As String
End Class

Public Class KomunikatGrupyPrzypiszWynik
    Public status As Integer
    Public status_opis As String
End Class

Public Class KomunikatGrupyWczytajWynik
    Public status As Integer
    Public status_opis As String
    Public dane As DataSet
End Class

Public Class KomunikatyWczytajWynik
    Public status As Integer
    Public status_opis As String
    Public dane As DataSet
End Class

Public Class NotyfikacjeOdczytajWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class NotyfikacjeZapiszWynik
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat
End Class

Public Class NotyfikacjeSystemoweZapiszWynik
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat
End Class

Public Class AdresKopiujUserWczytajWynik
    Public dane As DataSet
    Public iloscZdublowanych As Integer
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class AdresySkopiowaneZapiszWynik
    Public iloscDubli As Integer
    Public iloscAdresowSkopiowanych As Integer
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class AdresyKopiujFiltryWczytajWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public nazwaSzablonu As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class RaportAwizaDostawaFiltrWczytajWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public minimalnaDataAwiza As Date
    Public komunikaty() As Komunikat
End Class

Public Class RaportAwizDostawaGenerujWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
    Public ileRekordow As Integer
End Class
#Region "Awizacja"
Public Class SkuListaWczytajWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public nazwaSzablonuDodajPozycjeAwiza As String
    Public komunikaty() As Komunikat 'zaleg�e komunikaty dla u�ytkownika
End Class

Public Class DostawcaEdycjaZapiszWynik
    Public status As Integer
    Public status_opis As String
    Public blokada_id As Integer
    Public komunikaty() As Komunikat
    Public dostawca_id_out As Integer
End Class

Public Class DostawcyWczytajWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public nazwaSzablonuDodajPozycjeAwiza As String
    Public komunikaty() As Komunikat 'zaleg�e komunikaty dla u�ytkownika
End Class

Public Class DostawcaSzczegolyWczytajWynik
    Public status As Integer
    Public status_opis As String
    Public adres As String
    Public kod As String
    Public miasto As String
    Public kraj As String
    Public nazwaSzablonuDodajPozycjeAwiza As String
    Public komunikaty() As Komunikat 'zaleg�e komunikaty dla u�ytkownika
End Class

Public Class DostawcaUsunWynik
    Public blokada_id As Integer
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class ZalozNowyProduktQGUARWynik
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaleg�e komunikaty dla u�ytkownika
End Class

Public Class AwizoZapiszWynik
    Public dane As DataSet
    Public blokada_id As Integer
    Public awizo_id_out As Integer
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaleg�e komunikaty dla u�ytkownika
End Class

Public Class AwizaListaWczytajWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public ilosc_stron As Integer
    Public ilosc_total_rekordow As Integer
    Public komunikaty() As Komunikat 'zaleg�e komunikaty dla u�ytkownika
End Class

Public Class AwizoTypyDostawQguarWczytajWynik
    Public dane As DataSet
    Public blokada_id As Integer
    Public awizo_id_out As Integer
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class AwizaSzczegolyWczytajWynik
    Public dane As DataSet
    Public dostawca_nazwa As String
    Public dostawca_adres As String
    Public dostawca_kod As String
    Public dostawca_miasto As String
    Public dostawca_kraj As String
    Public osoba_kontaktowa As String
    Public telefon As String
    Public uwagi As String
    Public status As Integer
    Public ilosc_palet As String
    Public ilosc_paczek As String
    Public status_opis As String
    Public numer_PO As String
    Public planowana_data_dostawy As DateTime
    Public awizo_status As String
    Public qguar_za As String
    Public qguar_dostawa As String
    Public qguar_delivery_kind As String
    Public komunikaty() As Komunikat 'zaleg�e komunikaty dla u�ytkownika
End Class

Public Class AwizoWczytajWynik
    Public dane As DataSet
    Public dostawca_id As Integer
    Public osoba_kontaktowa As String
    Public telefon As String
    Public uwagi As String
    Public numer_po As String
    Public status As Integer
    Public awizo_id_out As Integer
    Public status_opis As String
    Public planowana_data_dostawy As DateTime
    Public ilosc_palet As String
    Public ilosc_paczek As String
    Public awizo_status As String
    Public qguar_za As String
    Public qguar_dostawa As String
    Public qguar_delivery_kind_id As Integer
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class AwizoUsunWynik
    Public blokada_id As Integer
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class AwizoZatwierdzWynik
    Public dane As DataSet
    Public blokada_id As Integer
    Public awizo_id_out As Integer
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class AwizoAnulujWynik
    Public blokada_id As Integer
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class AwizoPrzygotujZwrotWynik
    Public dane As DataSet
    Public dostawca_id As Integer
    Public osoba_kontaktowa As String
    Public telefon As String
    Public uwagi As String
    Public numer_po As String
    Public planowana_data_dostawy As DateTime
    Public ilosc_palet As String
    Public ilosc_paczek As String
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class PlikExcelZakladanieSkuZapiszWynik
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat
End Class

#End Region

#Region "Dane dostawy dla zamówienia"

Public Class KodyPocztoweDpdWczytajWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class KodyPocztoweOdczytajWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class DostawyGwarantowaneTypyWczytajWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class ZamowienieDaneDpdWczytajWynik
    Public DokZw As Integer
    Public PrzZw As Integer
    Public OsPryw As Integer
    Public Wartosc As Decimal
    Public KwotaCOD As Decimal
    Public Typ As String
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class UserDaneDpdWczytajWynik
    Public Dok_Zwrotne_Visible As Integer
    Public Dok_Zwrotne_Enable As Integer
    Public Prz_Zwrotne_Visible As Integer
    Public Prz_Zwrotne_Enable As Integer
    Public Osob_Pryw_Visible As Integer
    Public Osob_Pryw_Enable As Integer
    Public Wartosc_Visible As Integer
    Public Wartosc_Enable As Integer
    Public COD_Visible As Integer
    Public COD_Enable As Integer
    Public Dost_Gw_Visible As Integer
    Public Dost_Gw_Enable As Integer
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class


Public Class UserDaneDpdZapiszWynik
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class KodyPocztoweWieleSprawdzWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class
#End Region

#Region "Raporty"

Public Class RaportAwizWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class RaportHistoriiMaterialuWczytajWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class RaportLimityWczytajWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class RaportPodzialySkuGrupaObdzielanaGenerujWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class RaportPodzialySkuGrupaObdzielanaWczytajWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class RaportPodzialyWczytajWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class RaportPrzyjeciaPoDatachWczytajWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class RaportRentownosciWczytajWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class RaportRozchodyWczytajWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class RaportStanWczytajWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class RaportWejsciaWyjsciaWynik
    Public dane As dataset
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class RaportZamowieniaDaneUzytkownikaWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class RaportZamowieniaPozycjeWczytajWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class RaportZamowieniaWczytajWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class RaportPaletodniWczytajWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class
#End Region

Public Class ZamowienieStatusyListaWynik
    Public status As Integer
    Public status_opis As String
    Public dane As DataSet
End Class
#Region "Struktury dodatkowe dla MVC"
Public Class StatystykiWczytajWynik
    Public IloscAdresow As Integer
    Public IloscProduktow As Integer
    Public IloscWKoszyku As Integer
    Public IloscZamowien As Integer
    Public dane As DataSet
    Public textKomunikat As String
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat
End Class

Public Class ParametrWartoscPobierzWynik
    Public wartosc As String
    Public typParametruID As Integer
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat
End Class

Public Class MagazynDomyslnyWynik
    Public dane As DataSet
    Public magazyn_id As Integer
    Public status As Integer
    Public status_opis As String
End Class

Public Class UzytkownikDaneWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
End Class

Public Class UserResetHaslaGenerujTokenWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
End Class

Public Class UserResetHaslaUstawNoweHasloWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
End Class

Public Class StanMagazynSkuGrupaWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat
End Class

#End Region

Public Class SlownikListaWczytajWynik
    Public dane As DataSet
    Public nazwaSzablonu As String
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class SlownikWartoscWczytajWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class SlownikWartoscUsunWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class SlownikWartoscEdytujWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class SlownikWartoscKilkaDodajWynik
    Public iloscDodana As Integer
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class SkuUsunWczytajWynik
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class SkuUsunWynik
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class


#Region "Zamowienia_INV"

Public Class ZamowienieINVWczytajWynik
    Public dane As DataSet
    Public zamowienie_id As Integer
    Public magazyn_id As Integer
    Public blokada_id As Integer
    Public wlasciciel_nazwa As String
    Public tryb_pracy As Integer '1 - edycja własnego koszyka, 2 - podgląd obcego koszyka, 3 - podgląd zamówienia
    Public zamowienie_status As String
    Public zamowienie_status_opis As String
    Public typ_zamowienia As Integer '12 - INWENTARYZACJA NA PLUS, 13 - INWENTARYZACJA NA MINUS
    Public uwagi As String
    Public status As Integer
    Public status_opis As String
End Class

Public Class KoszykINVZapiszWynik
    Public dane As DataSet
    Public zamowienie_id As Integer
    Public blokada_id As Integer
    Public user_nazwa As String
    Public koszyk As Boolean
    Public status As Integer
    Public status_opis As String
End Class

Public Class KoszykINVZatwierdzWynik
    Public status As Integer
    Public status_opis As String
End Class

Public Class StanSkuGrupaINVWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat
End Class

#End Region

Public Class RaportZamowieniaRozliczenieWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class RaportTerminowosciFedExWczytajWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class RaportStanBezGrupWczytajWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
    Public komunikaty() As Komunikat 'zaległe komunikaty dla użytkownika
End Class

Public Class RaportPodzialyZamowieniaPerUserWynik
    Public dane As DataSet
    Public status As Integer
    Public status_opis As String
End Class
