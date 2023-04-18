Imports System.Reflection
Imports System
Imports System.IO
'Imports Microsoft.Office.Interop
Imports System.Text.RegularExpressions
Imports System.Data.SqlClient
Imports System.Threading
Imports System.Globalization
Imports System.Text
Imports OfficeOpenXml
Imports OfficeOpenXml.Style
Imports OfficeOpenXml.Utils

Public Class frmZamowieniaZExcela

    Private ile_zamowien As Integer
    Private ile_SKU As Integer
    Private ds As New DataSet
    Private dtPozycjeZamowienia As New DataTable
    Private dtZamowienia As New DataTable
    Private dtStanNaPodstawieSKU As New DataTable
    Private dtSkuGrupa As New DataTable
    Private dtMagazyny As New DataTable
    Private dtGrupy As New DataTable
    Private sku_xml As String = ""  '' zmienna którą wyślemy na serwer aby dostać SKU_ID i ilości_dostępne na podstawie SKU
    Private status As Integer
    Private opis_statusu As String
    Private numer_zamowienia As String
    Private intIdBlokady As Integer
    Private Plik_EXCEL As Byte()
    Private nazwa_pliku_XLS As String
    Private NazwaArkusza As String = ""
    Dim Parametry As New ZmienneGlobalne
    Private lstPodstawoweNaglowki As New List(Of String)
    Private grupy_sku_xml As String = ""
    Private id_magazyn As Integer = Parametry.idMagazyn

    Private intDokZw As Integer
    Private intPrzZw As Integer
    Private intOsPryw As Integer
    Private dblWartosc As Decimal
    Private dblCOD As Decimal
    Private strDorGwTyp As String
    Private bMamDaneDPD As Boolean
    Private bMamyFunkcjeDaneDostawy As Boolean = False
    Private uiSep As String = Globalization.CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator

    Private dtTypy As DataTable
    Private sKod As String
    Private sDostGwTyp As String
    Private dtKody As DataTable
    Private kwota_zam As Decimal
    Private strTyp As String
    Private alGwaranty As New ArrayList
    Private bkodvalid As Boolean

    Private Dok_Zwrotne_Enable As Integer = -1
    Private Prz_Zwrotne_Enable As Integer = -1
    Private Osob_Pryw_Enable As Integer = -1
    Private Wartosc_Enable As Integer = -1
    Private COD_Enable As Integer = -1
    Private Dost_Gw_Enable As Integer = -1
    Private nazwaSzablonu As String = "Szablon_Dodaj_Zamowienie.xlsx"

    Private dtSlownik As DataTable

    Const conWartosc As String = "Wartosc"
    Const conIdSlownikWartosc As String = "ID_SLOWNIK_WARTOSCI"
#Region "Konfiguracja pliku"
    Private strCellSkuStart As String = "N4" '' komórka w pliku Excela od której zaczynają się produkty

    'Adresy naszych nagłówków podstawowych:
    Private strCellLp As String = "A6"
    Private strNazwaLp As String = "Lp"
    Private strCellNazwiskoImie As String = "B6"
    Private strNazwaNazwiskoImie As String = "Imię i Nazwisko odbiorcy lub nazwa firmy"
    Private strCellMiasto As String = "C6"
    Private strNazwaMiasto As String = "Miasto"
    Private strCellKod As String = "D6"
    Private strNazwaKod As String = "Kod"
    Private strCellUlica As String = "E6"
    Private strNazwaUlica As String = "Ulica"
    Private strCellOsobaKontaktowa As String = "F6"
    Private strNazwaOsobaKontaktowa As String = "Osoba kontaktowa"
    Private strCellTelefon As String = "G6"
    Private strNazwaTelefon As String = "Nr telefonu"
    'Dane dostawy
    Private strCellDokZwr As String = "H6"
    Private strNazwaDokZwr As String = "dokumenty zwrotne"
    Private strCellPrzesZwr As String = "I6"
    Private strNazwaPrzesZwr As String = "przesyłka zwrotna"
    Private strCellOsPryw As String = "J6"
    Private strNazwaOsPryw As String = "czy odbiera osoba prywatna"
    Private strCellWartosc As String = "K6"
    Private strNazwaWartosc As String = "wartość"
    Private strCellCOD As String = "L6"
    Private strNazwaCOD As String = "kwota pobrania"
    Private strCellDostGwTypDPD As String = "M6"
    Private strNazwaTypDPD As String = "doręczenie gwarantowane"

    'Kolumna w której znajdziemy nagłówek grupy:
    Private strCellGrupaWSystemie As String = "M1"
    Private strNazwaGrupaWSystemie As String = "Grupa w systemie"

    'wynik zapisywany w pliku po prawej stronie za wszystkimi sku przy każdym zamówieniu:
    Private strCellStatus As String = "N6" 'Podajemy tu ades komórki w której ma sie pojawić status jak by nie było Sku. zazwyczaj będzie to kolumna jak w strcellSkuStart, a wiersz jak w strCellLp
    Private strNazwaStatus As String = "STATUS"
    Private strNazwaStatusOpis As String = "OPIS_STATUSU"
    Private strNazwaNumerZamowienia As String = "NUMER ZAMÓWIENIA"


    Private intOdstepOdNaglowka As Integer = 1 'jeżeli jest =1 to nie ma pustego wiersza pod nagłówkiem

    Private Enum EnumPodstawoweNaglowki
        Lp
        nazwa
        miasto
        kod
        adres
        osoba_kontaktowa
        telefon
        dokumenty_zwrotne
        przesylka_zwrotna
        osoba_prywatna
        wartosc_przesylki
        kwota_pobrania
        doreczenie_gwarantowane
    End Enum

    Private Enum EnumDtNaglowki
        naglowek
        excel_cell
    End Enum

    Private Enum EnumDtPozycjeZamowienia
        sku
        ilosc
        sku_id
        ilosc_dostepna
        grupa
    End Enum

    Private Enum EnumDtSkuGrupa
        sku
        grupa
    End Enum

#End Region



    Private Sub btnWybierzPlik_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWybierzPlik.Click

        Dim i As Integer = 0
        ProgressBar1.Value = 0
        lblProgress.Text = ""
        lblIloscZamowien.Text = ""
        btnZlozZamowienia.Enabled = False
        dgv.DataSource = Nothing

        Dim ofdPlikXLS As New OpenFileDialog
        ofdPlikXLS.Filter = "Pliki Programu Excel (*.xlsx)|*.xlsx"

        If ofdPlikXLS.ShowDialog = Windows.Forms.DialogResult.OK Then

            Try
                Dim newFile As New FileInfo(ofdPlikXLS.FileName)

                Using pck As New ExcelPackage(newFile)

                    nazwa_pliku_XLS = ofdPlikXLS.FileName
                    txtPlik.Text = ofdPlikXLS.FileName

                    '' Jeśli dtZamowienia ma już wiersze to usuwamy je
                    If dtZamowienia.Rows.Count > 0 Then
                        For i = dtZamowienia.Rows.Count - 1 To 0 Step -1
                            dtZamowienia.Rows.RemoveAt(i)
                        Next
                    End If

                    '' Jeśli dtZamowienia ma już kolumny to usuwamy je
                    If dtZamowienia.Columns.Count > 0 Then
                        For i = dtZamowienia.Columns.Count - 1 To 0 Step -1
                            dtZamowienia.Columns.RemoveAt(i)
                        Next
                    End If

                    If cmbNazwaArkusza.Items.Count > 0 Then
                        cmbNazwaArkusza.Items.Clear()
                    End If

                    For i = 1 To pck.Workbook.Worksheets.Count
                        cmbNazwaArkusza.Items.Add(pck.Workbook.Worksheets(i).Name)
                    Next

                End Using
            Catch ex As Exception
                Cursor = Cursors.Default
                If ex.Message.Contains("because it is being used by another process") Then
                    MsgBox("Plik o nazwie " & nazwa_pliku_XLS & _
                           " jest otwarty! Proszę zamknąć ten plik i spróbowac ponownie go wczytać.", _
                           MsgBoxStyle.Exclamation)
                Else
                    MsgBox("Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, "Wystąpił wyjątek")
                End If
                Exit Sub
            End Try
        End If
    End Sub

    Private Function SprawdzNaglowekKolumny(ByVal wpisany As String, _
                                            ByVal poprawny As String, _
                                            ByVal ExcelCell As String) As Boolean
        If wpisany.ToUpper.Trim <> poprawny.ToUpper.Trim Then
            MsgBox("Niepoprawny nagłówek kolumny w komórce """ & ExcelCell & """. " & vbNewLine & _
                   "Wpisano: " & wpisany & vbNewLine & _
                  "Prawidłowa nazwa kolumny: " & poprawny & vbNewLine & _
                   vbNewLine & "Proszę poprawić plik i spróbować ponownie złożyć z niego zamówienia. ", _
                   MsgBoxStyle.Exclamation, "Błędnie przygotowany plik Excela")
            Return False
        End If
        Return True
    End Function

    Private Sub PrzygotujTabele()
        '' Jeśli dtZamowienia ma już wiersze to usuwamy je
        If dtZamowienia.Rows.Count > 0 Then
            dtZamowienia.Rows.Clear()
        End If

        '' Jeśli dtZamowienia ma już kolumny to usuwamy je
        If dtZamowienia.Columns.Count > 0 Then
            dtZamowienia.Columns.Clear()
        End If

        If lstPodstawoweNaglowki.Count > 0 Then
            lstPodstawoweNaglowki.Clear()
        End If

        If dtSkuGrupa.Rows.Count > 0 Then
            dtSkuGrupa.Rows.Clear()
        End If

        lstPodstawoweNaglowki.Add(EnumPodstawoweNaglowki.Lp.ToString)
        lstPodstawoweNaglowki.Add(EnumPodstawoweNaglowki.nazwa.ToString)
        lstPodstawoweNaglowki.Add(EnumPodstawoweNaglowki.miasto.ToString)
        lstPodstawoweNaglowki.Add(EnumPodstawoweNaglowki.kod.ToString)
        lstPodstawoweNaglowki.Add(EnumPodstawoweNaglowki.adres.ToString)
        lstPodstawoweNaglowki.Add(EnumPodstawoweNaglowki.osoba_kontaktowa.ToString)
        lstPodstawoweNaglowki.Add(EnumPodstawoweNaglowki.telefon.ToString)

        lstPodstawoweNaglowki.Add(EnumPodstawoweNaglowki.dokumenty_zwrotne.ToString)
        lstPodstawoweNaglowki.Add(EnumPodstawoweNaglowki.przesylka_zwrotna.ToString)
        lstPodstawoweNaglowki.Add(EnumPodstawoweNaglowki.osoba_prywatna.ToString)
        lstPodstawoweNaglowki.Add(EnumPodstawoweNaglowki.wartosc_przesylki.ToString)
        lstPodstawoweNaglowki.Add(EnumPodstawoweNaglowki.kwota_pobrania.ToString)
        lstPodstawoweNaglowki.Add(EnumPodstawoweNaglowki.doreczenie_gwarantowane.ToString)

        '' Dodajemy kolumny do zmiennej dtZamowienia
        dtZamowienia.Columns.Add(EnumPodstawoweNaglowki.Lp.ToString)
        dtZamowienia.Columns.Add(EnumPodstawoweNaglowki.nazwa.ToString)
        dtZamowienia.Columns.Add(EnumPodstawoweNaglowki.miasto.ToString)
        dtZamowienia.Columns.Add(EnumPodstawoweNaglowki.kod.ToString)
        dtZamowienia.Columns.Add(EnumPodstawoweNaglowki.adres.ToString)
        dtZamowienia.Columns.Add(EnumPodstawoweNaglowki.osoba_kontaktowa.ToString)
        dtZamowienia.Columns.Add(EnumPodstawoweNaglowki.telefon.ToString)

        dtZamowienia.Columns.Add(EnumPodstawoweNaglowki.dokumenty_zwrotne.ToString)
        dtZamowienia.Columns.Add(EnumPodstawoweNaglowki.przesylka_zwrotna.ToString)
        dtZamowienia.Columns.Add(EnumPodstawoweNaglowki.osoba_prywatna.ToString)
        dtZamowienia.Columns.Add(EnumPodstawoweNaglowki.wartosc_przesylki.ToString)
        dtZamowienia.Columns.Add(EnumPodstawoweNaglowki.kwota_pobrania.ToString)
        dtZamowienia.Columns.Add(EnumPodstawoweNaglowki.doreczenie_gwarantowane.ToString)

    End Sub

    Private Function CzyIstniejaWszystkieSKU(ByVal xml_sku As String) As Boolean

        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik As wsCursorProf.CzyIstniejaWszystkieSkuWynik

        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.CzyIstniejaWszystkieSku(frmGlowna.sesja, xml_sku)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical)
            Return False
        End Try
        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, "Brak niektórych SKU")
            Return False
        End If
        Return True
    End Function

    Private Sub cmbNazwaArkusza_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbNazwaArkusza.SelectedIndexChanged

        Dim intIndexWiersza As Integer = 0
        Dim intIndexKolumny As Integer = 0
        ProgressBar1.Value = 0
        btnZlozZamowienia.Enabled = False
        lblProgress.Text = ""
        lblIloscZamowien.Text = ""
        dgv.DataSource = Nothing
        Dim row_grupa_w_systemie As Integer

        '' sprawdzamy czy wybrano arkusz
        If cmbNazwaArkusza.SelectedIndex = -1 Then
            Exit Sub
        End If

        NazwaArkusza = cmbNazwaArkusza.Text

        Try
            Dim newFile As New FileInfo(nazwa_pliku_XLS)

            Using pck As New ExcelPackage(newFile)

                ''pck.File.Attributes = FileAttributes.Normal

                Dim wsDane As ExcelWorksheet = pck.Workbook.Worksheets(NazwaArkusza)

                '' sprawdzamy czy mamy poprawnie sformatowany plik Excela wg poniższego układu kolumn:
                Dim dtNaglowki As New DataTable
                dtNaglowki.Columns.Add(EnumDtNaglowki.naglowek.ToString)
                dtNaglowki.Columns.Add(EnumDtNaglowki.excel_cell.ToString)
                dtNaglowki.Rows.Add(strNazwaLp, strCellLp)
                dtNaglowki.Rows.Add(strNazwaNazwiskoImie, strCellNazwiskoImie)
                dtNaglowki.Rows.Add(strNazwaMiasto, strCellMiasto)
                dtNaglowki.Rows.Add(strNazwaKod, strCellKod)
                dtNaglowki.Rows.Add(strNazwaUlica, strCellUlica)
                dtNaglowki.Rows.Add(strNazwaOsobaKontaktowa, strCellOsobaKontaktowa)
                dtNaglowki.Rows.Add(strNazwaTelefon, strCellTelefon)
                'Dane dostawy
                dtNaglowki.Rows.Add(strNazwaDokZwr, strCellDokZwr)
                dtNaglowki.Rows.Add(strNazwaPrzesZwr, strCellPrzesZwr)
                dtNaglowki.Rows.Add(strNazwaOsPryw, strCellOsPryw)
                dtNaglowki.Rows.Add(strNazwaWartosc, strCellWartosc)
                dtNaglowki.Rows.Add(strNazwaCOD, strCellCOD)
                dtNaglowki.Rows.Add(strNazwaTypDPD, strCellDostGwTypDPD)

                Dim czy_poprawne_naglowki As Boolean = True
                For Each r As DataRow In dtNaglowki.Rows
                    If Not SprawdzNaglowekKolumny(Trim(CStr(wsDane.Cells(r.Item(EnumDtNaglowki.excel_cell.ToString)).Value)), _
                                                  r.Item(EnumDtNaglowki.naglowek.ToString).ToString, r.Item(EnumDtNaglowki.excel_cell.ToString)) Then
                        czy_poprawne_naglowki = False
                    End If
                Next

                If czy_poprawne_naglowki = False Then
                    '' nazwa lub nazwy kolumn różnią się od nazw z szablonu; kończymy procedurę
                    btnZlozZamowienia.Enabled = False
                    Cursor = Cursors.Default
                    lblPrzetwarzanieDanych.Visible = False
                    Exit Sub
                End If

                '' przygotowanie tabel dtZamowienia i dtSkuGrupa
                PrzygotujTabele()

                '' do zmiennej lstDodaneKolumnySKU będziemy wrzucać dodane do tabeli nazwy kolumn (numery SKU)
                Dim lstDodaneKolumnySKU As New List(Of String)
                intIndexWiersza = 0
                '' sprawdzamy ile mamy SKU.... i dodajemy kolejno kolumny o nazwie SKU 
                Do Until NZ(CStr(wsDane.Cells(strCellSkuStart).Offset(0, intIndexWiersza).Value), "") = ""
                    If dtZamowienia.Columns.Count > 0 Then
                        If lstDodaneKolumnySKU.Contains(NZ(CStr(wsDane.Cells(strCellSkuStart).Offset(0, intIndexWiersza).Value), "")) Then
                            '' mamy zdublowaną kolumnę
                            MsgBox("W wybranym pliku, produkt o numerze SKU: " & _
                                   NZ(CStr(wsDane.Cells(strCellSkuStart).Offset(0, intIndexWiersza).Value), "") & _
                                   " występuje w więcej niż jednej pozycji. Proszę poprawić plik tak, aby numery SKU nie powtarzały się w nim.", _
                                   MsgBoxStyle.Exclamation, "Zdublowane kolumny SKU")
                            Cursor = Cursors.Default
                            lblPrzetwarzanieDanych.Visible = False
                            Exit Sub
                        End If
                        dtZamowienia.Columns.Add(NZ(CStr(wsDane.Cells(strCellSkuStart).Offset(0, intIndexWiersza).Value), ""))
                        lstDodaneKolumnySKU.Add(NZ(CStr(wsDane.Cells(strCellSkuStart).Offset(0, intIndexWiersza).Value), ""))
                    Else
                        dtZamowienia.Columns.Add(NZ(CStr(wsDane.Cells(strCellSkuStart).Offset(0, intIndexWiersza).Value), ""))
                        lstDodaneKolumnySKU.Add(NZ(CStr(wsDane.Cells(strCellSkuStart).Offset(0, intIndexWiersza).Value), ""))
                    End If
                    intIndexWiersza = intIndexWiersza + 1
                Loop

                '' znamy już ilość SKU w pliku
                ile_SKU = intIndexWiersza

                '' teraz sprawdźmy czy wszystkie podane SKU w pliku Excela istnieją w bazie danych
                Dim strXmlSKU As New StringBuilder
                For intIndexWiersza = 0 To lstDodaneKolumnySKU.Count - 1
                    strXmlSKU.Append("<row sku=""" & lstDodaneKolumnySKU.Item(intIndexWiersza) & """ />")
                Next

                If Not CzyIstniejaWszystkieSKU(strXmlSKU.ToString.Replace("&", "&amp;")) Then
                    Cursor = Cursors.Default
                    lblPrzetwarzanieDanych.Visible = False
                    Exit Sub
                End If


                intIndexWiersza = 0


                Dim kod_poczt As String = ""
                Dim intKolumnaExcel As Integer = 0
                Dim test_int As Integer = 0
                Dim sprawdzana_ilosc As String = ""
                Dim suma_wszystkich_sku_zamowienie As Integer = 0
                Dim bZnaleziono As Boolean = False
                Dim nr_pozycji_zamowienia As Integer = 0

                Dim intOdstepOdNaglowkaSku As Integer = wsDane.Cells(strCellLp).Offset(intOdstepOdNaglowka, 0).End.Row - wsDane.Cells(strCellSkuStart).Offset(0, 0).End.Row '--Odstęp jaki jest między nagłówkiem Sku a pierwszym wierszem z danymi

                '' W pliku Excela ustalamy położenie kolumn: status, status_opis i numer zamówienia
                wsDane.Cells(strCellStatus).Offset(0, ile_SKU).Value = strNazwaStatus
                wsDane.Cells(strCellStatus).Offset(0, ile_SKU + 1).Value = strNazwaStatusOpis
                wsDane.Cells(strCellStatus).Offset(0, ile_SKU + 2).Value = strNazwaNumerZamowienia

                ''--------------------------------------------------------------------------------------------------------
                ''----------------------------- WALIDACJA danych z pliku Excela oraz dodanie ich do zmiennej dtZamowienia
                ''--------------------------------------------------------------------------------------------------------
                Dim bPoprawneDane As Boolean = True '' zmienna do oznaczenia poprawności danych w pliku
                Cursor = Cursors.WaitCursor
                lblPrzetwarzanieDanych.Visible = True
                Do Until NZ(wsDane.Cells(strCellLp).Offset(intIndexWiersza + intOdstepOdNaglowka, 0).Value, "").ToString & _
                         NZ(wsDane.Cells(strCellNazwiskoImie).Offset(intIndexWiersza + intOdstepOdNaglowka, 0).Value, "").ToString & _
                         NZ(wsDane.Cells(strCellMiasto).Offset(intIndexWiersza + intOdstepOdNaglowka, 0).Value, "").ToString & _
                         NZ(wsDane.Cells(strCellKod).Offset(intIndexWiersza + intOdstepOdNaglowka, 0).Value, "").ToString & _
                         NZ(wsDane.Cells(strCellUlica).Offset(intIndexWiersza + intOdstepOdNaglowka, 0).Value, "").ToString = ""
                    dtZamowienia.Rows.Add()

                    wsDane.Cells(strCellStatus).Offset(intIndexWiersza + intOdstepOdNaglowka, ile_SKU).Value = ""
                    wsDane.Cells(strCellStatus).Offset(intIndexWiersza + intOdstepOdNaglowka, ile_SKU + 1).Value = ""

                    For intIndexKolumny = 0 To dtZamowienia.Columns.Count - 1
                        Select Case dtZamowienia.Columns(intIndexKolumny).ColumnName
                            Case EnumPodstawoweNaglowki.Lp.ToString
                                dtZamowienia.Rows(intIndexWiersza).Item(intIndexKolumny) = intIndexWiersza + 1
                            Case EnumPodstawoweNaglowki.nazwa.ToString
                                If NZ(wsDane.Cells(strCellNazwiskoImie).Offset(intIndexWiersza + intOdstepOdNaglowka, 0).Value, "").ToString = String.Empty Then
                                    bPoprawneDane = False
                                    wsDane.Cells(strCellStatus).Offset(intIndexWiersza + intOdstepOdNaglowka, ile_SKU).Value = -1
                                    wsDane.Cells(strCellStatus).Offset(intIndexWiersza + intOdstepOdNaglowka, ile_SKU + 1).Value += vbNewLine & "W załadowanym pliku Excela, w pozycji " & CStr(intIndexWiersza + 1) & "." & " nie wprowadzono nazwy odbiorcy!"
                                End If
                                dtZamowienia.Rows(intIndexWiersza).Item(intIndexKolumny) = wsDane.Cells(strCellNazwiskoImie).Offset(intIndexWiersza + intOdstepOdNaglowka, 0).Value

                            Case EnumPodstawoweNaglowki.miasto.ToString
                                If NZ(wsDane.Cells(strCellMiasto).Offset(intIndexWiersza + intOdstepOdNaglowka, 0).Value, "").ToString = String.Empty Then
                                    bPoprawneDane = False
                                    wsDane.Cells(strCellStatus).Offset(intIndexWiersza + intOdstepOdNaglowka, ile_SKU).Value = -1
                                    wsDane.Cells(strCellStatus).Offset(intIndexWiersza + intOdstepOdNaglowka, ile_SKU + 1).Value += Chr(10) & "Nie wprowadzono miasta!"
                                End If
                                dtZamowienia.Rows(intIndexWiersza).Item(intIndexKolumny) = wsDane.Cells(strCellMiasto).Offset(intIndexWiersza + intOdstepOdNaglowka, 0).Value
                            Case EnumPodstawoweNaglowki.kod.ToString
                                '' sprawdzamy, czy kod pocztowy jest poprawny poprzez regular expression
                                Dim reg_exp As New Regex("^([0-9]{5})$")
                                If NZ(wsDane.Cells(strCellKod).Offset(intIndexWiersza + intOdstepOdNaglowka, 0).Value, "").ToString = String.Empty Then
                                    bPoprawneDane = False
                                    wsDane.Cells(strCellStatus).Offset(intIndexWiersza + intOdstepOdNaglowka, ile_SKU).Value = -1
                                    wsDane.Cells(strCellStatus).Offset(intIndexWiersza + intOdstepOdNaglowka, ile_SKU + 1).Value += Chr(10) & "Nie wprowadzono kodu pocztowego!"
                                ElseIf Not reg_exp.IsMatch(wsDane.Cells(strCellKod).Offset(intIndexWiersza + intOdstepOdNaglowka, 0).Value.ToString.Replace("-", "").Trim) Then
                                    '' mamy niepoprawny kod pocztowy
                                    bPoprawneDane = False
                                    wsDane.Cells(strCellStatus).Offset(intIndexWiersza + intOdstepOdNaglowka, ile_SKU).Value = -1
                                    wsDane.Cells(strCellStatus).Offset(intIndexWiersza + intOdstepOdNaglowka, ile_SKU + 1).Value += Chr(10) & "Wprowadzono błędny kod pocztowy: " & wsDane.Cells(strCellKod).Offset(intIndexWiersza + intOdstepOdNaglowka, 0).Value
                                End If
                                kod_poczt = CStr(wsDane.Cells(strCellKod).Offset(intIndexWiersza + intOdstepOdNaglowka, 0).Value).Replace("-", "").Trim
                                kod_poczt = Mid(kod_poczt, 1, 2) & "-" & Mid(kod_poczt, 3, 3)
                                If Not odswiezKodPocztowy(kod_poczt) Then
                                    bPoprawneDane = False
                                    wsDane.Cells(strCellStatus).Offset(intIndexWiersza + intOdstepOdNaglowka, ile_SKU).Value = -1
                                    wsDane.Cells(strCellStatus).Offset(intIndexWiersza + intOdstepOdNaglowka, ile_SKU + 1).Value += Chr(10) & "Wystąpił błąd przy walidacji kodu pocztowego: " & kod_poczt
                                End If
                                If Not validKodPocztowy(kod_poczt) Then
                                    bPoprawneDane = False
                                    wsDane.Cells(strCellStatus).Offset(intIndexWiersza + intOdstepOdNaglowka, ile_SKU).Value = -1
                                    wsDane.Cells(strCellStatus).Offset(intIndexWiersza + intOdstepOdNaglowka, ile_SKU + 1).Value += Chr(10) & "Wprowadzono błędny kod pocztowy: " & kod_poczt
                                End If
                                dtZamowienia.Rows(intIndexWiersza).Item(intIndexKolumny) = kod_poczt '' wsDane.Cells("H9").Offset(i, 0).Value
                            Case EnumPodstawoweNaglowki.adres.ToString
                                If NZ(wsDane.Cells(strCellUlica).Offset(intIndexWiersza + intOdstepOdNaglowka, 0).Value, "").ToString = String.Empty Then
                                    bPoprawneDane = False
                                    wsDane.Cells(strCellStatus).Offset(intIndexWiersza + intOdstepOdNaglowka, ile_SKU).Value = -1
                                    wsDane.Cells(strCellStatus).Offset(intIndexWiersza + intOdstepOdNaglowka, ile_SKU + 1).Value += Chr(10) & "Nie wprowadzono adresu!"
                                End If
                                dtZamowienia.Rows(intIndexWiersza).Item(intIndexKolumny) = wsDane.Cells(strCellUlica).Offset(intIndexWiersza + intOdstepOdNaglowka, 0).Value
                            Case EnumPodstawoweNaglowki.osoba_kontaktowa.ToString
                                '' czy jest osoba kontaktowa

                                dtZamowienia.Rows(intIndexWiersza).Item(intIndexKolumny) = wsDane.Cells(strCellOsobaKontaktowa).Offset(intIndexWiersza + intOdstepOdNaglowka, 0).Value

                                If NZ(IIf(IsDBNull(dtZamowienia.Rows(intIndexWiersza).Item(intIndexKolumny)), "", dtZamowienia.Rows(intIndexWiersza).Item(intIndexKolumny)), "").ToString = String.Empty Then
                                    bPoprawneDane = False
                                    wsDane.Cells(strCellStatus).Offset(intIndexWiersza + intOdstepOdNaglowka, ile_SKU).Value = -1
                                    wsDane.Cells(strCellStatus).Offset(intIndexWiersza + intOdstepOdNaglowka, ile_SKU + 1).Value += Chr(10) & "Nie wprowadzono osoby kontaktowej!"
                                End If
                            Case EnumPodstawoweNaglowki.telefon.ToString
                                '' czy jest telefon kontaktowy
                                dtZamowienia.Rows(intIndexWiersza).Item(intIndexKolumny) = wsDane.Cells(strCellTelefon).Offset(intIndexWiersza + intOdstepOdNaglowka, 0).Value

                                If NZ(dtZamowienia.Rows(intIndexWiersza).Item(intIndexKolumny), "").ToString = String.Empty Then
                                    bPoprawneDane = False
                                    wsDane.Cells(strCellStatus).Offset(intIndexWiersza + intOdstepOdNaglowka, ile_SKU).Value = -1
                                    wsDane.Cells(strCellStatus).Offset(intIndexWiersza + intOdstepOdNaglowka, ile_SKU + 1).Value += Chr(10) & "Nie wprowadzono telefonu kontaktowego!"
                                End If


                                ''''''''''''''''''''''''''''''
                                '' DANE DOSTAWY
                                ''''''''''''''''''''''''''''''


                            Case EnumPodstawoweNaglowki.dokumenty_zwrotne.ToString
                                If NZ(Trim(CStr(wsDane.Cells(strCellDokZwr).Offset(intIndexWiersza + intOdstepOdNaglowka, 0).Value)), "").ToString = String.Empty Then
                                    dtZamowienia.Rows(intIndexWiersza).Item(intIndexKolumny) = 0
                                    wsDane.Cells(strCellDokZwr).Offset(intIndexWiersza + intOdstepOdNaglowka, 0).Value = 0
                                ElseIf NZ(Trim(CStr(wsDane.Cells(strCellDokZwr).Offset(intIndexWiersza + intOdstepOdNaglowka, 0).Value)), "").ToString <> "0" And _
                                    NZ(Trim(CStr(wsDane.Cells(strCellDokZwr).Offset(intIndexWiersza + intOdstepOdNaglowka, 0).Value)), "").ToString <> "1" Then
                                    bPoprawneDane = False
                                    wsDane.Cells(strCellStatus).Offset(intIndexWiersza + intOdstepOdNaglowka, ile_SKU).Value = -1
                                    wsDane.Cells(strCellStatus).Offset(intIndexWiersza + intOdstepOdNaglowka, ile_SKU + 1).Value += Chr(10) & "Niepoprawnie określono czy są dokumenty zwrotne! Dozwolne wartości w tej kolumnie to 1 lub 0, gdzie 1 oznacza TAK, a 0 NIE."
                                Else
                                    '' czy user ma uprawnienie do zapisu danych dostawy
                                    If Not CzyMamyUprawnienieDoOpcjiDostawy(EnumPodstawoweNaglowki.dokumenty_zwrotne.ToString, _
                                                                     wsDane.Cells(strCellDokZwr).Offset(intIndexWiersza + intOdstepOdNaglowka, 0).Value, _
                                                                     intIndexWiersza) Then
                                        bPoprawneDane = False
                                        wsDane.Cells(strCellStatus).Offset(intIndexWiersza + intOdstepOdNaglowka, ile_SKU).Value = -1
                                        wsDane.Cells(strCellStatus).Offset(intIndexWiersza + intOdstepOdNaglowka, ile_SKU + 1).Value += Chr(10) & "Nie masz uprawnień do opcji dostawy - dokumenty zwrotne!"

                                    Else
                                        dtZamowienia.Rows(intIndexWiersza).Item(intIndexKolumny) = wsDane.Cells(strCellDokZwr).Offset(intIndexWiersza + intOdstepOdNaglowka, 0).Value
                                    End If

                                End If
                            Case EnumPodstawoweNaglowki.przesylka_zwrotna.ToString
                                If NZ(Trim(CStr(wsDane.Cells(strCellPrzesZwr).Offset(intIndexWiersza + intOdstepOdNaglowka, 0).Value)), "").ToString = String.Empty Then
                                    dtZamowienia.Rows(intIndexWiersza).Item(intIndexKolumny) = 0
                                    wsDane.Cells(strCellPrzesZwr).Offset(intIndexWiersza + intOdstepOdNaglowka, 0).Value = 0
                                ElseIf NZ(Trim(CStr(wsDane.Cells(strCellPrzesZwr).Offset(intIndexWiersza + intOdstepOdNaglowka, 0).Value)), "").ToString <> "0" And _
                                    NZ(Trim(CStr(wsDane.Cells(strCellPrzesZwr).Offset(intIndexWiersza + intOdstepOdNaglowka, 0).Value)), "").ToString <> "1" Then

                                    bPoprawneDane = False
                                    wsDane.Cells(strCellStatus).Offset(intIndexWiersza + intOdstepOdNaglowka, ile_SKU).Value = -1
                                    wsDane.Cells(strCellStatus).Offset(intIndexWiersza + intOdstepOdNaglowka, ile_SKU + 1).Value += Chr(10) & "Niepoprawnie określono czy jest przesyłka zwrotna! Dozwolne wartości w tej kolumnie to 1 lub 0, gdzie 1 oznacza TAK, a 0 NIE."
                                Else
                                    '' czy user ma uprawnienie do zapisu danych dostawy
                                    If Not CzyMamyUprawnienieDoOpcjiDostawy(EnumPodstawoweNaglowki.przesylka_zwrotna.ToString, _
                                                                     wsDane.Cells(strCellPrzesZwr).Offset(intIndexWiersza + intOdstepOdNaglowka, 0).Value, _
                                                                     intIndexWiersza) Then
                                        bPoprawneDane = False
                                        wsDane.Cells(strCellStatus).Offset(intIndexWiersza + intOdstepOdNaglowka, ile_SKU).Value = -1
                                        wsDane.Cells(strCellStatus).Offset(intIndexWiersza + intOdstepOdNaglowka, ile_SKU + 1).Value += Chr(10) & "Nie masz uprawnień do opcji dostawy - przesyłka zwrotna!"
                                    Else
                                        dtZamowienia.Rows(intIndexWiersza).Item(intIndexKolumny) = wsDane.Cells(strCellPrzesZwr).Offset(intIndexWiersza + intOdstepOdNaglowka, 0).Value
                                    End If

                                End If
                            Case EnumPodstawoweNaglowki.osoba_prywatna.ToString
                                If NZ(Trim(CStr(wsDane.Cells(strCellOsPryw).Offset(intIndexWiersza + intOdstepOdNaglowka, 0).Value)), "").ToString = String.Empty Then
                                    dtZamowienia.Rows(intIndexWiersza).Item(intIndexKolumny) = 0
                                    wsDane.Cells(strCellOsPryw).Offset(intIndexWiersza + intOdstepOdNaglowka, 0).Value = 0
                                ElseIf NZ(Trim(CStr(wsDane.Cells(strCellOsPryw).Offset(intIndexWiersza + intOdstepOdNaglowka, 0).Value)), "").ToString <> "0" And _
                                    NZ(Trim(CStr(wsDane.Cells(strCellOsPryw).Offset(intIndexWiersza + intOdstepOdNaglowka, 0).Value)), "").ToString <> "1" Then
                                    bPoprawneDane = False
                                    wsDane.Cells(strCellStatus).Offset(intIndexWiersza + intOdstepOdNaglowka, ile_SKU).Value = -1
                                    wsDane.Cells(strCellStatus).Offset(intIndexWiersza + intOdstepOdNaglowka, ile_SKU + 1).Value += Chr(10) & "Niepoprawnie określono czy jest osoba prywatna! Dozwolne wartości w tej kolumnie to 1 lub 0, gdzie 1 oznacza TAK, a 0 NIE."
                                Else
                                    '' czy user ma uprawnienie do zapisu danych dostawy
                                    If Not CzyMamyUprawnienieDoOpcjiDostawy(EnumPodstawoweNaglowki.osoba_prywatna.ToString, _
                                                                     wsDane.Cells(strCellOsPryw).Offset(intIndexWiersza + intOdstepOdNaglowka, 0).Value, _
                                                                     intIndexWiersza) Then
                                        bPoprawneDane = False
                                        wsDane.Cells(strCellStatus).Offset(intIndexWiersza + intOdstepOdNaglowka, ile_SKU).Value = -1
                                        wsDane.Cells(strCellStatus).Offset(intIndexWiersza + intOdstepOdNaglowka, ile_SKU + 1).Value += Chr(10) & "Nie masz uprawnień do opcji dostawy - osoba prywatna!"
                                    Else
                                        dtZamowienia.Rows(intIndexWiersza).Item(intIndexKolumny) = wsDane.Cells(strCellOsPryw).Offset(intIndexWiersza + intOdstepOdNaglowka, 0).Value
                                    End If

                                End If
                            Case EnumPodstawoweNaglowki.wartosc_przesylki.ToString
                                Dim wynik_dec As Decimal
                                If NZ(Trim(CStr(wsDane.Cells(strCellWartosc).Offset(intIndexWiersza + intOdstepOdNaglowka, 0).Value)), "").ToString = String.Empty Then
                                    dtZamowienia.Rows(intIndexWiersza).Item(intIndexKolumny) = 0
                                    wsDane.Cells(strCellWartosc).Offset(intIndexWiersza + intOdstepOdNaglowka, 0).Value = 0
                                ElseIf Not Decimal.TryParse(NZ(wsDane.Cells(strCellWartosc).Offset(intIndexWiersza + intOdstepOdNaglowka, 0).Value, "").ToString.Replace(".", uiSep).Replace(",", uiSep), wynik_dec) Then
                                    bPoprawneDane = False
                                    wsDane.Cells(strCellStatus).Offset(intIndexWiersza + intOdstepOdNaglowka, ile_SKU).Value = -1
                                    wsDane.Cells(strCellStatus).Offset(intIndexWiersza + intOdstepOdNaglowka, ile_SKU + 1).Value += Chr(10) & "Podano niepoprawną wartość dostawy! Proszę wprowadzić wartość, która jest liczbą."
                                Else
                                    '' czy user ma uprawnienie do zapisu danych dostawy
                                    If Not CzyMamyUprawnienieDoOpcjiDostawy(EnumPodstawoweNaglowki.wartosc_przesylki.ToString, _
                                                                     NZ(Trim(CStr(wsDane.Cells(strCellWartosc).Offset(intIndexWiersza + intOdstepOdNaglowka, 0).Value)).ToString.Replace(".", uiSep).Replace(",", uiSep), ""), _
                                                                     intIndexWiersza) Then
                                        bPoprawneDane = False
                                        wsDane.Cells(strCellStatus).Offset(intIndexWiersza + intOdstepOdNaglowka, ile_SKU).Value = -1
                                        wsDane.Cells(strCellStatus).Offset(intIndexWiersza + intOdstepOdNaglowka, ile_SKU + 1).Value += Chr(10) & "Nie masz uprawnień do opcji dostawy - wartość dostawy!"
                                    Else
                                        dtZamowienia.Rows(intIndexWiersza).Item(intIndexKolumny) = NZ(Trim(CStr(wsDane.Cells(strCellWartosc).Offset(intIndexWiersza + intOdstepOdNaglowka, 0).Value)).ToString.Replace(".", uiSep).Replace(",", uiSep), "")
                                        wsDane.Cells(strCellWartosc).Offset(intIndexWiersza + intOdstepOdNaglowka, 0).Value = NZ(Trim(CStr(wsDane.Cells(strCellWartosc).Offset(intIndexWiersza + intOdstepOdNaglowka, 0).Value)).ToString.Replace(".", uiSep).Replace(",", uiSep), "")
                                    End If

                                End If
                            Case EnumPodstawoweNaglowki.kwota_pobrania.ToString
                                Dim wynik_dec As Decimal
                                If NZ(Trim(CStr(wsDane.Cells(strCellCOD).Offset(intIndexWiersza + intOdstepOdNaglowka, 0).Value)), "").ToString = String.Empty Then
                                    dtZamowienia.Rows(intIndexWiersza).Item(intIndexKolumny) = 0
                                    wsDane.Cells(strCellCOD).Offset(intIndexWiersza + intOdstepOdNaglowka, 0).Value = 0
                                ElseIf Not Decimal.TryParse(NZ(Trim(CStr(wsDane.Cells(strCellCOD).Offset(intIndexWiersza + intOdstepOdNaglowka, 0).Value)), "").ToString.Replace(".", uiSep).Replace(",", uiSep), wynik_dec) Then
                                    bPoprawneDane = False
                                    wsDane.Cells(strCellStatus).Offset(intIndexWiersza + intOdstepOdNaglowka, ile_SKU).Value = -1
                                    wsDane.Cells(strCellStatus).Offset(intIndexWiersza + intOdstepOdNaglowka, ile_SKU + 1).Value += Chr(10) & "Podano niepoprawną kwotę pobrania! Proszę wprowadzić wartość, która jest liczbą."
                                Else
                                    '' czy user ma uprawnienie do zapisu danych dostawy
                                    If Not CzyMamyUprawnienieDoOpcjiDostawy(EnumPodstawoweNaglowki.kwota_pobrania.ToString, _
                                                                     NZ(Trim(CStr(wsDane.Cells(strCellCOD).Offset(intIndexWiersza + intOdstepOdNaglowka, 0).Value)), "").ToString.Replace(".", uiSep).Replace(",", uiSep), _
                                                                     intIndexWiersza) Then
                                        bPoprawneDane = False
                                        wsDane.Cells(strCellStatus).Offset(intIndexWiersza + intOdstepOdNaglowka, ile_SKU).Value = -1
                                        wsDane.Cells(strCellStatus).Offset(intIndexWiersza + intOdstepOdNaglowka, ile_SKU + 1).Value += Chr(10) & "Nie masz uprawnień do opcji dostawy - kwota pobrania!"
                                    Else
                                        dtZamowienia.Rows(intIndexWiersza).Item(intIndexKolumny) = NZ(Trim(CStr(wsDane.Cells(strCellCOD).Offset(intIndexWiersza + intOdstepOdNaglowka, 0).Value)).ToString.Replace(".", uiSep).Replace(",", uiSep), "")
                                        wsDane.Cells(strCellCOD).Offset(intIndexWiersza + intOdstepOdNaglowka, 0).Value = NZ(Trim(CStr(wsDane.Cells(strCellCOD).Offset(intIndexWiersza + intOdstepOdNaglowka, 0).Value)).ToString.Replace(".", uiSep).Replace(",", uiSep), "")
                                    End If

                                End If
                            Case EnumPodstawoweNaglowki.doreczenie_gwarantowane.ToString
                                sKod = NZ(Trim(CStr(wsDane.Cells(strCellKod).Offset(intIndexWiersza + intOdstepOdNaglowka, 0).Value)), "")
                                sDostGwTyp = NZ(Trim(CStr(wsDane.Cells(strCellDostGwTypDPD).Offset(intIndexWiersza + intOdstepOdNaglowka, 0).Value)), "")

                                '' czy user ma uprawnienie do zapisu danych dostawy
                                If Not CzyMamyUprawnienieDoOpcjiDostawy(EnumPodstawoweNaglowki.doreczenie_gwarantowane.ToString, _
                                                                 sDostGwTyp, _
                                                                 intIndexWiersza) Then
                                    bPoprawneDane = False
                                    wsDane.Cells(strCellStatus).Offset(intIndexWiersza + intOdstepOdNaglowka, ile_SKU).Value = -1
                                    wsDane.Cells(strCellStatus).Offset(intIndexWiersza + intOdstepOdNaglowka, ile_SKU + 1).Value += Chr(10) & "Nie masz uprawnień do opcji dostawy - doręczenie gwarantowane!"
                                Else
                                    If sDostGwTyp <> String.Empty Then
                                        If sDostGwTyp <> "Sobota" And sDostGwTyp <> "Godzina0930" And sDostGwTyp <> "Godzina1200" Then
                                            bPoprawneDane = False
                                            wsDane.Cells(strCellStatus).Offset(intIndexWiersza + intOdstepOdNaglowka, ile_SKU).Value = -1
                                            wsDane.Cells(strCellStatus).Offset(intIndexWiersza + intOdstepOdNaglowka, ile_SKU + 1).Value += Chr(10) & "Podano niepoprawne doręczenie gwarantowane! Można wprowadzić jedną z trzech opcji: Sobota, Godzina0930, Godzina1200"
                                        End If

                                        If alGwaranty.Contains(sDostGwTyp) Then
                                            If dtKody.Select(String.Format("KOD_POCZTOWY = '{0}'", sKod)).Length = 1 Then
                                                If dtKody.Select(String.Format("KOD_POCZTOWY = '{0}'", sKod))(0).Item(sDostGwTyp).ToString <> "TAK" Then
                                                    bPoprawneDane = False
                                                    wsDane.Cells(strCellStatus).Offset(intIndexWiersza + intOdstepOdNaglowka, ile_SKU).Value = -1
                                                    wsDane.Cells(strCellStatus).Offset(intIndexWiersza + intOdstepOdNaglowka, ile_SKU + 1).Value += Chr(10) & "Dla kodu pocztowego: " & sKod & ", nie można wybrać dostawy gwarantowanej: " & sDostGwTyp
                                                End If
                                            End If
                                        End If

                                    End If
                                    dtZamowienia.Rows(intIndexWiersza).Item(intIndexKolumny) = sDostGwTyp
                                    wsDane.Cells(strCellDostGwTypDPD).Offset(intIndexWiersza + intOdstepOdNaglowka, 0).Value = sDostGwTyp
                                End If

                            Case Else '' KOLUMNY z SKU
                                '' jeśli nie wpisano ilości wstawiamy 0

                                sprawdzana_ilosc = wsDane.Cells(strCellSkuStart).Offset(intIndexWiersza + intOdstepOdNaglowkaSku, intKolumnaExcel).Value
                                If NZ(sprawdzana_ilosc, "") = "" Then
                                    wsDane.Cells(strCellSkuStart).Offset(intIndexWiersza + intOdstepOdNaglowkaSku, intKolumnaExcel).Value = 0
                                    sprawdzana_ilosc = 0
                                End If
                                If Not Integer.TryParse(sprawdzana_ilosc, test_int) Then
                                    bPoprawneDane = False
                                    wsDane.Cells(strCellStatus).Offset(intIndexWiersza + intOdstepOdNaglowka, ile_SKU).Value = -1
                                    wsDane.Cells(strCellStatus).Offset(intIndexWiersza + intOdstepOdNaglowka, ile_SKU + 1).Value += Chr(10) & "Dla SKU o numerze " & wsDane.Cells(strCellSkuStart).Offset(0, intKolumnaExcel).Value & " wprowadzono błędną ilość: " & sprawdzana_ilosc
                                End If
                                suma_wszystkich_sku_zamowienie = suma_wszystkich_sku_zamowienie + sprawdzana_ilosc
                                dtZamowienia.Rows(intIndexWiersza).Item(intIndexKolumny) = CInt(wsDane.Cells(strCellSkuStart).Offset(intIndexWiersza + intOdstepOdNaglowkaSku, intKolumnaExcel).Value)
                                intKolumnaExcel += 1

                        End Select
                    Next

                    '' sprawdzamy czy we wstawionym wierszu o numerze (i) długość osoba_kontaktowa + telefon + uwagi nie będzie przekraczała 170 znaków
                    If Len(dtZamowienia.Rows(intIndexWiersza).Item(EnumPodstawoweNaglowki.osoba_kontaktowa.ToString) + _
                           dtZamowienia.Rows(intIndexWiersza).Item(EnumPodstawoweNaglowki.telefon.ToString) + _
                           System.IO.Path.GetFileName(txtPlik.Text)) > 170 Then

                        bPoprawneDane = False
                        wsDane.Cells(strCellStatus).Offset(intIndexWiersza + intOdstepOdNaglowka, ile_SKU).Value = -1
                        wsDane.Cells(strCellStatus).Offset(intIndexWiersza + intOdstepOdNaglowka, ile_SKU + 1).Value += Chr(10) & _
                            "Połączenie pól: " & EnumPodstawoweNaglowki.osoba_kontaktowa.ToString & ", " & _
                                       "- " & EnumPodstawoweNaglowki.telefon.ToString & _
                                       "oraz  nazwy załadowanego pliku Excela (uwagi) przekracza dopuszczalną wartość 170 znaków!" & _
                                       "Ilość znaków, o którą należy skrócić połączenie tych pól: " & _
                                       CStr(Len(dtZamowienia.Rows(intIndexWiersza).Item("osoba_kontaktowa") + _
                                       dtZamowienia.Rows(intIndexWiersza).Item("telefon") + System.IO.Path.GetFileName(txtPlik.Text) _
                                      ) - 170)

                    End If

                    If suma_wszystkich_sku_zamowienie = 0 Then
                        bPoprawneDane = False
                        wsDane.Cells(strCellStatus).Offset(intIndexWiersza + intOdstepOdNaglowka, ile_SKU).Value = -1
                        wsDane.Cells(strCellStatus).Offset(intIndexWiersza + intOdstepOdNaglowka, ile_SKU + 1).Value += Chr(10) & "Suma wszystkich zamawianych SKU jest równa 0! Proszę poprawić ilości w tym zamówieniu albo usunąć ten wiersz z pliku."
                    End If

                    intKolumnaExcel = 0
                    intIndexWiersza = intIndexWiersza + 1
                    suma_wszystkich_sku_zamowienie = 0
                Loop

                ' ilość zamówień
                ile_zamowien = intIndexWiersza

                If bPoprawneDane = False Then
                    btnZlozZamowienia.Enabled = False
                    pck.Save()
                    MsgBox("W załadowanym pliku występują błędy! Szczegóły zapisano w kolumnie opis statusu. Popraw plik i spróbuj wgrać ponownie.", _
                           MsgBoxStyle.Exclamation, "Błędne dane w pliku")
                    Cursor = Cursors.Default
                    lblPrzetwarzanieDanych.Visible = False
                    Exit Sub
                End If


                '''''''''''''''' Szukamy w pliku Excela wiersza z Grupami w systemie
                Dim m As Integer = 0
                bZnaleziono = False

                For m = 0 To ile_zamowien + 100
                    If NZ(CStr(wsDane.Cells(strCellGrupaWSystemie).Offset(m, 0).Value), "") = strNazwaGrupaWSystemie Then
                        bZnaleziono = True
                        row_grupa_w_systemie = m
                    End If
                Next
                If Not bZnaleziono Then
                    MsgBox("Nie odnaleziono w pliku Excela wiersza z nazwami grup, z których będą zamawiane poszczególne SKU!", _
                           MsgBoxStyle.Exclamation, "Brak wiersza z nazwami grup")
                    Cursor = Cursors.Default
                    lblPrzetwarzanieDanych.Visible = False
                    Exit Sub
                End If
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                '' obliczamy sumę poszczególnych sku
                dtPozycjeZamowienia.Rows.Clear()
                Dim suma_SKU As Integer = 0
                Dim NazwaGrupy As String = ""
                For Each col As DataColumn In dtZamowienia.Columns
                    If Not lstPodstawoweNaglowki.Contains(col.ColumnName) Then
                        For Each row As DataRow In dtZamowienia.Rows
                            suma_SKU += row.Item(col.ColumnName)
                        Next
                        If suma_SKU < 1 Then
                            MsgBox("Dla produktu o numerze SKU: " & col.ColumnName & _
                                   " suma ilości ze wszystkich zamówień jest równa zero. " & _
                                   "Jeśli produkt ten nie będzie zamawiany należy usunąć kolumnę z tym towarem. " & _
                                   "W przeciwnym razie proszę wpisać ilości zamawiane!", _
                                   MsgBoxStyle.Exclamation, "Zerowa ilość dla zamawianego produktu")
                            Cursor = Cursors.Default
                            lblPrzetwarzanieDanych.Visible = False
                            Exit Sub
                        End If
                        dtPozycjeZamowienia.Rows.Add()
                        dtPozycjeZamowienia.Rows(nr_pozycji_zamowienia).Item("sku") = col.ColumnName
                        dtPozycjeZamowienia.Rows(nr_pozycji_zamowienia).Item("ilosc") = suma_SKU
                        NazwaGrupy = wsDane.Cells(strCellGrupaWSystemie).Offset(row_grupa_w_systemie, nr_pozycji_zamowienia + 1).Value
                        NazwaGrupy = IIf(IsNothing(NazwaGrupy) Or IsDBNull(NazwaGrupy), "", NazwaGrupy)
                        dtSkuGrupa.Rows.Add(col.ColumnName, NazwaGrupy)
                        nr_pozycji_zamowienia += 1
                        suma_SKU = 0
                    End If
                Next

                '' przygotowuje grupy_sku_xml
                grupy_sku_xml = ""
                Dim sku_bez_grupy As String = ""
                Dim lp_grupa As Integer = 0
                For Each r As DataRow In dtSkuGrupa.Rows
                    If r.Item("grupa") = String.Empty Then
                        lp_grupa += 1
                        If sku_bez_grupy = "" Then
                            sku_bez_grupy = "- " & r.Item(EnumDtSkuGrupa.sku.ToString)
                        Else
                            sku_bez_grupy = sku_bez_grupy & vbNewLine & "- " & r.Item(EnumDtSkuGrupa.sku.ToString)
                        End If
                    End If
                    grupy_sku_xml = grupy_sku_xml & "<row sku=""" & r.Item(EnumDtSkuGrupa.sku.ToString).Replace("&", "&amp;").Replace("'", "&apos;").Replace("""", "&quot;") & _
                                                      """ grupa=""" & r.Item(EnumDtSkuGrupa.grupa.ToString).Replace("&", "&amp;").Replace("'", "&apos;").Replace("""", "&quot;") & """/>"
                Next
                If sku_bez_grupy <> "" Then
                    If lp_grupa = 1 Then
                        MsgBox("Dla produktu o numerze SKU: " & vbNewLine & sku_bez_grupy & _
                               " nie wybrano grupy, z której będzie zamawiany!", MsgBoxStyle.Exclamation, "Nie wybrano grupy dla SKU")
                    Else
                        MsgBox("Dla produktów o numerach SKU: " & vbNewLine & sku_bez_grupy & vbNewLine & _
                               " nie wybrano grup, z których będą zamawiane!", MsgBoxStyle.Exclamation, "Nie wybrano grup dla produktów")
                    End If
                    Cursor = Cursors.Default
                    lblPrzetwarzanieDanych.Visible = False
                    Exit Sub
                End If

                Dim sku_xml As String = ""
                '' Sprawdzamy czy mamy wystarczającą ilość towaru na stanie wybranej grupy, aby złożyć wszystkie zamówienia z pliku
                '' aktualnie w zmiennej dtPozycjeZamowienia mamy podsumowane wszystkie zamawiane SKU
                '' Zbudujmy zatem sku_xml
                For Each r As DataRow In dtPozycjeZamowienia.Rows
                    sku_xml = sku_xml & "<row sku=""" & r.Item(EnumDtPozycjeZamowienia.sku.ToString) & """ ilosc=""" & r.Item(EnumDtPozycjeZamowienia.ilosc.ToString) & """/>"
                Next
                If Not Podaj_stan_na_podstawie_SKU(sku_xml, grupy_sku_xml, True) Then
                    Cursor = Cursors.Default
                    lblPrzetwarzanieDanych.Visible = False
                    Exit Sub
                End If

                lblIloscZamowien.Text = "Ilość zamówień: " & ile_zamowien

                '' sprawdzamy, dtZamowienia mają choć jeden rekord
                If dtZamowienia.Rows.Count > 0 Then
                    btnZlozZamowienia.Enabled = True
                Else
                    btnZlozZamowienia.Enabled = False
                End If

                '' ukrywamy labelke informującą o przetwarzaniu danych
                lblPrzetwarzanieDanych.Visible = False

                ''wszystko jest ok, wypełniamy dgv bez kolumn z produktami
                dgv.DataSource = dtZamowienia
                For Each c As DataGridViewColumn In dgv.Columns
                    If Not lstPodstawoweNaglowki.Contains(c.Name) Then
                        c.Visible = False
                    End If
                    c.SortMode = DataGridViewColumnSortMode.Programmatic
                Next


                btnZlozZamowienia.Enabled = True
                pck.Save()

            End Using
        Catch ex As Exception
            Cursor = Cursors.Default
            If ex.Message.Contains("because it is being used by another process") Then
                MsgBox("Plik o nazwie " & nazwa_pliku_XLS & _
                       " jest otwarty! Proszę zamknąć ten plik i spróbowac ponownie go wczytać.", _
                       MsgBoxStyle.Exclamation, Me.Text)
            Else
                MsgBox("Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, "Wystąpił wyjątek")
            End If
            Exit Sub
        End Try



    End Sub

    Private Sub btnAnuluj_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAnuluj.Click
        Me.Close()
    End Sub

    Private Sub ControlsOff()
        dgv.DataSource = Nothing
        cmbGrupy.SelectedIndex = -1
        cmbNazwaArkusza.SelectedIndex = -1
        btnZlozZamowienia.Enabled = False
    End Sub

    Private Function Przygotowanie() As Boolean
        '' sprawdzamy czy wybrano plik Excela
        If nazwa_pliku_XLS = String.Empty Or txtPlik.Text = String.Empty Then
            MsgBox("Nie wybrano pliku Excela z zamówieniami.", MsgBoxStyle.Exclamation, "Brak pliku Excela")
            ControlsOff()
            Return False
        End If

        If grupy_sku_xml = String.Empty Then
            MsgBox("Nie ustalono grup dla zamawianych sku.", MsgBoxStyle.Exclamation, "Brak grup")
            ControlsOff()
            Return False
        End If

        '' sprawdzamy, czy mamy ustaloną ilość SKU w pliku
        If ile_SKU < 1 Then
            MsgBox("Nie ustalono ilości produktów w załadowanym pliku.", MsgBoxStyle.Exclamation, "Brak ilości produktów")
            ControlsOff()
            Return False
        End If

        '' sprawdzamy, czy poprawna data realizacji
        If dtpDataRealizacji.Value.Date < minimalnaDataRealizacji().Date Then
            MsgBox(String.Format("Nie można wybrać przewidywanej daty dostawy wcześniejszej niż {0}", minimalnaDataRealizacji().ToString("yyyy-MM-dd")), MsgBoxStyle.Exclamation, Me.Text)
            Return False
        End If

        '' czy mamy dane w dtZamowienia
        If Not dtZamowienia.Rows.Count > 0 Then
            MsgBox("Brak zamówień do złożenia!", MsgBoxStyle.Exclamation, "Brak zamówień")
            ControlsOff()
            Return False
        End If

        '' czy mamy dane w dtPozycjeZamowienia
        If Not dtPozycjeZamowienia.Rows.Count > 0 Then
            MsgBox("Brak danych o pozycjach zamówień!", MsgBoxStyle.Exclamation, "Brak pozycji zamówień")
            ControlsOff()
            Return False
        End If

        Return True
    End Function

    Private Sub DezaktywacjaKontrolek()
        For Each c As Control In Me.Controls
            c.Enabled = False
        Next
    End Sub

    Private Sub AktywacjaKontrolek()
        For Each c As Control In Me.Controls
            If c.Name <> "btnZlozZamowienia" Then
                c.Enabled = True
            End If
        Next
    End Sub

    Private Sub btnZlozZamowienia_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnZlozZamowienia.Click

        Dim i, j As Integer
        Dim ile_zamowien_zlozono As Integer = 0
        Dim numery_zlozonych_zamowien As String = ""
        Dim xml_zlozone_zamowienia As String = ""
        Dim ile_zamowien_nie_zlozono As Integer = 0
        Dim ile_zamowien_wczesniej_zlozonych As Integer = 0
        Dim biezace_zamowienie As Integer = 0
        ProgressBar1.Maximum = ile_zamowien - 1
        ProgressBar1.Step = 1
        opis_statusu = ""
        i = 0

        btnZlozZamowienia.Enabled = False

        DezaktywacjaKontrolek()

        If Not Przygotowanie() Then
            AktywacjaKontrolek()
            Exit Sub
        End If

        Dim zapisalemXLS As Boolean = False

        Try
            Dim newFile As New FileInfo(nazwa_pliku_XLS)

            Using pck As New ExcelPackage(newFile)
                Dim wsDane As ExcelWorksheet = pck.Workbook.Worksheets(NazwaArkusza)
                Try
                    '' W pliku Excela ustalamy położenie kolumn: status, status_opis i numer zamówienia
                    wsDane.Cells(strCellStatus).Offset(0, ile_SKU).Value = strNazwaStatus
                    wsDane.Cells(strCellStatus).Offset(0, ile_SKU + 1).Value = strNazwaStatusOpis
                    wsDane.Cells(strCellStatus).Offset(0, ile_SKU + 2).Value = strNazwaNumerZamowienia

                    '' wychodzi na to że możemy składać zamówienia, więc zaczynamy...
                    For i = 0 To ile_zamowien - 1
                        '' sprawdzamy, czy zamówienie było już wcześniej złożone
                        If CStr(wsDane.Cells(strCellStatus).Offset(i + intOdstepOdNaglowka, ile_SKU).Value) = "0" And _
                            CStr(wsDane.Cells(strCellStatus).Offset(i + intOdstepOdNaglowka, ile_SKU + 2).Value) <> "---" Then

                            '' tak było już złożone to zamówienie
                            status = 2
                            opis_statusu = "To zamówienie zostało już wcześniej złożone z tego pliku"
                            numer_zamowienia = CStr(wsDane.Cells(strCellStatus).Offset(i + intOdstepOdNaglowka, ile_SKU + 2).Value) & " ---> już wcześniej złożone"
                            GoTo wynik_zamowienia
                        End If

                        ''''''''''''' SPRAWDZENIE ILOŚCI PRZED ZŁOŻENIEM ZAMÓWIENIA'''''''''''''''''''''''
                        sku_xml = ""
                        dtStanNaPodstawieSKU.Rows.Clear() '' czyścimy tabelę; funkcja Podaj_stan_na_podstawie_SKU wstawi do niej dane 
                        For j = dtZamowienia.Columns.Count - ile_SKU To dtZamowienia.Columns.Count - 1
                            If dtZamowienia.Rows(i).Item(j) > 0 Then
                                sku_xml = sku_xml & "<row sku=""" & dtZamowienia.Columns(j).ColumnName & """ ilosc=""" & dtZamowienia.Rows(i).Item(j) & """/>"
                            End If
                        Next
                        If sku_xml <> "" Then
                            If Not Podaj_stan_na_podstawie_SKU(sku_xml, grupy_sku_xml, False) Then
                                status = -1
                                numer_zamowienia = "---"
                                GoTo wynik_zamowienia
                            Else
                                If dtStanNaPodstawieSKU.Rows.Count < 1 Then
                                    status = -1
                                    numer_zamowienia = "Program nie ustalił żadnego wiersza dla tabeli dtStanNaPodstawieSKU!"
                                    GoTo wynik_zamowienia
                                End If
                            End If
                        Else
                            status = -1
                            opis_statusu = "Program nie ustalił żadnej pozycji dla tego zamówienia!"
                            numer_zamowienia = "---"
                            GoTo wynik_zamowienia
                        End If

                        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                        '''''''''''''''''''''''''''''''''''''''''''''''
                        '''''''''''''' TWORZYMY NOWY KOSZYK
                        '''''''''''''''''''''''''''''''''''''''''''''''
                        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
                        System.Net.ServicePointManager.Expect100Continue = False
                        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
                        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
                        Dim wsWynik As wsCursorProf.ZamowienieWczytajWynik

                        Try
                            Cursor = Cursors.WaitCursor
                            Application.DoEvents()
                            wsWynik = ws.ZamowienieWczytaj(frmGlowna.sesja, -1, id_magazyn, -1)
                            Cursor = Cursors.Default
                        Catch ex As Exception
                            Cursor = Cursors.Default
                            status = -1
                            opis_statusu = "Błąd komunikacji z serwerem. Szczegóły błędu:" & ex.Message
                            numer_zamowienia = "---"
                            GoTo wynik_zamowienia
                        End Try
                        '' przed ustawieniem wartości ustawiamy numer_zamowienia oraz intIdBlokady na -1
                        numer_zamowienia = -1
                        intIdBlokady = -1

                        If wsWynik.status < 0 Then
                            status = wsWynik.status
                            opis_statusu = wsWynik.status_opis
                            numer_zamowienia = "---"
                            GoTo wynik_zamowienia
                        ElseIf wsWynik.status = 0 Then
                            numer_zamowienia = wsWynik.zamowienie_id
                            intIdBlokady = wsWynik.blokada_id
                        End If

                        If numer_zamowienia = -1 Or intIdBlokady = -1 Then
                            status = -1
                            opis_statusu = "Nie ustalono wartości dla zmiennych oraz intIdBlokady."
                            numer_zamowienia = "---"
                            GoTo wynik_zamowienia
                        End If


                        '''''''''''''''''''''''''''''''''''''''''''''''
                        '''''''''''''' KOSZYK ZAPISZ
                        '''''''''''''''''''''''''''''''''''''''''''''''
                        If Not Koszyk_Zapisz(intIdBlokady, id_magazyn, _
                                             -1, -1, dtZamowienia.Rows(i).Item(EnumPodstawoweNaglowki.nazwa.ToString), _
                                             dtZamowienia.Rows(i).Item(EnumPodstawoweNaglowki.adres.ToString), _
                                             dtZamowienia.Rows(i).Item(EnumPodstawoweNaglowki.kod.ToString), _
                                             dtZamowienia.Rows(i).Item(EnumPodstawoweNaglowki.miasto.ToString), _
                                             dtZamowienia.Rows(i).Item(EnumPodstawoweNaglowki.osoba_kontaktowa.ToString), _
                                             dtZamowienia.Rows(i).Item(EnumPodstawoweNaglowki.telefon.ToString), _
                                             System.IO.Path.GetFileName(txtPlik.Text).Replace(Path.GetExtension(System.IO.Path.GetFileName(txtPlik.Text)), "") & "/" & _
                                             "/" & dtZamowienia.Rows(i).Item(EnumPodstawoweNaglowki.nazwa.ToString), 4, _
                                             dtZamowienia.Rows(i).Item(EnumPodstawoweNaglowki.dokumenty_zwrotne.ToString), _
                                             dtZamowienia.Rows(i).Item(EnumPodstawoweNaglowki.przesylka_zwrotna.ToString), _
                                             dtZamowienia.Rows(i).Item(EnumPodstawoweNaglowki.osoba_prywatna.ToString), _
                                             dtZamowienia.Rows(i).Item(EnumPodstawoweNaglowki.wartosc_przesylki.ToString), _
                                             dtZamowienia.Rows(i).Item(EnumPodstawoweNaglowki.kwota_pobrania.ToString), _
                                             IIf(IsDBNull(dtZamowienia.Rows(i).Item(EnumPodstawoweNaglowki.doreczenie_gwarantowane.ToString)), "", dtZamowienia.Rows(i).Item(EnumPodstawoweNaglowki.doreczenie_gwarantowane.ToString))) Then
                            GoTo wynik_zamowienia
                        End If

                        '''''''''''''''''''''''''''''''''''''''''''''''
                        '''''''''''''' KOSZYK ZATWIERDZ
                        '''''''''''''''''''''''''''''''''''''''''''''''
                        Dim ws3 As New wsCursorProf.CursorService
                        System.Net.ServicePointManager.Expect100Continue = False
                        ws3.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
                        ws3.Proxy.Credentials = CredentialCache.DefaultCredentials
                        Dim wsWynik3 As wsCursorProf.KoszykZatwierdzWynik
                        Try
                            Cursor = Cursors.WaitCursor
                            Application.DoEvents()
                            wsWynik3 = ws.KoszykZatwierdz(frmGlowna.sesja, intIdBlokady)
                            Cursor = Cursors.Default
                        Catch ex As Exception
                            Cursor = Cursors.Default
                            status = -1
                            opis_statusu = "Błąd komunikacji z serwerem. Szczegóły błędu:" & ex.Message
                            numer_zamowienia = "---"
                            GoTo wynik_zamowienia
                        End Try

                        If wsWynik3.status < 0 Then
                            status = wsWynik3.status
                            opis_statusu = wsWynik3.status_opis
                            numer_zamowienia = "---"
                            GoTo wynik_zamowienia
                        ElseIf wsWynik3.status > 0 Then
                            status = wsWynik3.status
                            opis_statusu = wsWynik3.status_opis
                            numer_zamowienia = "---"
                            GoTo wynik_zamowienia
                        Else
                            status = wsWynik3.status
                            opis_statusu = wsWynik3.status_opis
                        End If

wynik_zamowienia:
                        '' sprawdzamy czy poprawnie złożono zamowienie i zwiększamy "ile_zamowien_zlozono" o 1
                        If status = 0 And numer_zamowienia <> "---" Then
                            '' zaznaczamy na dgv na zielono poprawnie złożone zamówienie
                            dgv.Rows(i).DefaultCellStyle.BackColor = Color.LawnGreen
                            ile_zamowien_zlozono = ile_zamowien_zlozono + 1
                            If numery_zlozonych_zamowien = "" Then
                                numery_zlozonych_zamowien = CStr(numer_zamowienia)
                                xml_zlozone_zamowienia = "<row id_zam=""" & CStr(numer_zamowienia) & """/>"
                            Else
                                numery_zlozonych_zamowien = numery_zlozonych_zamowien & "," & CStr(numer_zamowienia)
                                xml_zlozone_zamowienia = xml_zlozone_zamowienia & "<row id_zam=""" & CStr(numer_zamowienia) & """/>"
                            End If
                        ElseIf status = 2 Then
                            status = 0
                            dgv.Rows(i).DefaultCellStyle.BackColor = Color.Yellow
                            ile_zamowien_wczesniej_zlozonych += 1
                        Else
                            '' zaznaczamy na czerwono niezłożone zamowienie
                            dgv.Rows(i).DefaultCellStyle.BackColor = Color.Tomato
                            ile_zamowien_nie_zlozono = ile_zamowien_nie_zlozono + 1
                        End If
                        dgv.CurrentCell = dgv.Rows(i).Cells(0)
                        '' wpisujemy wynik wykonanego zamówienia do pliku Excela
                        wsDane.Cells(strCellStatus).Offset(i + intOdstepOdNaglowka, ile_SKU).Value = status
                        wsDane.Cells(strCellStatus).Offset(i + intOdstepOdNaglowka, ile_SKU + 1).Value = opis_statusu
                        wsDane.Cells(strCellStatus).Offset(i + intOdstepOdNaglowka, ile_SKU + 2).Value = numer_zamowienia
                        ProgressBar1.PerformStep()
                        sku_xml = ""
                        lblProgress.Text = "Wykonano " & CStr(Math.Round(((i + 1) / (ile_zamowien)) * 100, 0)) & "%"
                    Next i

koniec_zamowien:
                    If zapisalemXLS = False Then
                        pck.Save()
                        zapisalemXLS = True
                    End If


                    MsgBox("Zakończono składanie zamówień." & vbNewLine & _
                       "Ilość zamówień poprawnie złożonych: " & ile_zamowien_zlozono.ToString & vbNewLine & _
                       "Ilość zamówień, których nie udało się złożyć: " & ile_zamowien_nie_zlozono.ToString & vbNewLine & _
                       "Ilość zamówień, które zostały już wcześniej złożone z tego pliku: " & ile_zamowien_wczesniej_zlozonych.ToString & vbNewLine & _
                        "Szczegóły zapisano do wczytanego pliku.", MsgBoxStyle.Information, "Wynik składania zamówień z pliku Excel")

                Catch ex As Exception
                    If zapisalemXLS = False Then
                        pck.Save()
                        zapisalemXLS = True
                    End If
                    MsgBox("Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, "Wystąpił wyjątek")
                End Try


            End Using
        Catch ex As Exception
            Cursor = Cursors.Default
            If ex.Message.Contains("because it is being used by another process") Then
                MsgBox("Plik o nazwie " & nazwa_pliku_XLS & _
                       " jest otwarty! Proszę zamknąć ten plik i spróbowac ponownie go wczytać.", _
                       MsgBoxStyle.Exclamation)
            Else
                MsgBox("Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, "Wystąpił wyjątek")
            End If
            AktywacjaKontrolek()
            Exit Sub
        End Try

        '' Zapis pliku Excela do bazy danych 
        If File.Exists(nazwa_pliku_XLS) Then
            Plik_EXCEL = File.ReadAllBytes(nazwa_pliku_XLS)
            '' przygotowujemy zmienną numery_zlozonych_zamowien
            If numery_zlozonych_zamowien <> "" Then
                If Mid(numery_zlozonych_zamowien, Len(numery_zlozonych_zamowien) - 1, 1) = "," Then
                    numery_zlozonych_zamowien = Mid(numery_zlozonych_zamowien, 1, Len(numery_zlozonych_zamowien) - 1)
                End If
            End If
            If Not ZapisPlikZamowieniaExcel(System.IO.Path.GetFileName(nazwa_pliku_XLS), _
                                            "", _
                                            "", Plik_EXCEL, numery_zlozonych_zamowien) Then
                MsgBox("Nie zapisano pliku do bazy danych.", MsgBoxStyle.Exclamation, "Błąd zapisu pliku do bazy danych")
            End If
        End If

        btnZlozZamowienia.Enabled = False
        AktywacjaKontrolek()
        If cmbNazwaArkusza.Items.Count > 0 Then
            cmbNazwaArkusza.Items.Clear()
        End If

        '' otwieramy plik użytkownikowi
        'System.Diagnostics.Process.Start(nazwa_pliku_XLS)
        txtPlik.Text = ""
        Exit Sub


    End Sub

    Private Function ZapisPlikZamowieniaExcel(ByVal nazwa_pliku As String, ByVal typ_zlecenia As String, _
                                              ByVal uwagi As String, ByVal plik As Byte(), _
                                              ByVal numery_zamowien As String) As Boolean


        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik As wsCursorProf.PlikZamowieniaExcelZapiszWynik

        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.PlikZamowieniaExcelZapisz(frmGlowna.sesja, nazwa_pliku, typ_zlecenia, uwagi, plik, numery_zamowien)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
            Return False
        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, Me.Text)
            Return False
        ElseIf wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, Me.Text)
        End If

        Return True
    End Function


    Private Function Podaj_stan_na_podstawie_SKU(ByVal xml_sku As String, ByVal xml_grupy_sku As String, _
                                                 ByVal czy_wszystkie_zamowienia As Boolean) As Boolean

        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik As wsCursorProf.PodajSkuIdZSKUWynik

        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.PodajStanSkuIdZSKU(frmGlowna.sesja, xml_sku, xml_grupy_sku, 1, czy_wszystkie_zamowienia)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            If czy_wszystkie_zamowienia Then
                MsgBox("Błąd komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
            Else
                status = -1
                opis_statusu = "Błąd komunikacji z serwerem." & vbNewLine & "Szczegóły błędu:" & ex.Message
                numer_zamowienia = "---"
            End If
            Return False
        End Try
        Dim title As String = ""
        If wsWynik.status < 0 Then
            If czy_wszystkie_zamowienia Then
                If wsWynik.status_opis.Contains("występują grupy,") Or wsWynik.status_opis.Contains("występuje grupa,") Then
                    title = "Nieprawidłowa nazwa grupy"
                Else
                    title = "Niewystarczająca ilość towaru na stanie"
                End If
                MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, title)
            Else
                status = -1
                opis_statusu = wsWynik.status_opis
                numer_zamowienia = "---"
            End If
            Return False
        End If
        If wsWynik.dane.Tables.Count > 0 Then
            If wsWynik.dane.Tables(0).Rows.Count < 1 Then
                If czy_wszystkie_zamowienia Then
                    MsgBox("Serwer nie przesłał danych w tabeli z ilościami dostępnymi dla pozycji zamówienia!", MsgBoxStyle.Critical, Me.Text)
                Else
                    status = -1
                    opis_statusu = "Serwer nie przesłał danych w tabeli z ilościami dostępnymi dla pozycji zamówienia!"
                    numer_zamowienia = "---"
                End If
                Return False
            End If
        Else
            If czy_wszystkie_zamowienia Then
                MsgBox("Serwer nie przesłał tabeli z ilościami dostępnymi dla pozycji zamówienia!", MsgBoxStyle.Critical, Me.Text)
            Else
                status = -1
                opis_statusu = "Serwer nie przesłał tabeli z ilościami dostępnymi dla pozycji zamówienia!"
                numer_zamowienia = "---"
            End If
            Return False
        End If

        dtStanNaPodstawieSKU = wsWynik.dane.Tables(0)
        Return True
    End Function

    Private Function minimalnaDataRealizacji() As Date
        Dim wynikData As Date = Date.Now

        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik As wsCursorProf.MinimalnaDataRealizacjiWynik

        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.MinimalnaDataRealizacji(frmGlowna.sesja)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
            Me.Close()
            wynikData = DateTime.Now
        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, Me.Text)
            wynikData = DateTime.Now
        End If
        If wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, Me.Text)
            wynikData = DateTime.Now
        End If
        wynikData = wsWynik.data


        Return wynikData
    End Function

    Private Function Koszyk_Zapisz(ByVal blokada_id As Integer, _
                                    ByVal magazyn_id As Integer, ByVal magazyn_docelowy_id As Integer, ByVal adres_id As Integer, _
                                    ByVal nazwa As String, ByVal adres As String, ByVal kod As String, ByVal miasto As String, _
                                    ByVal osoba_kontaktowa As String, ByVal telefon_kontaktowy As String, ByVal uwagi As String, _
                                    ByVal typ_zamowienia As Integer, _
                                    ByVal intDokZw As Integer, ByVal intPrzZw As Integer, ByVal intOsPryw As Integer, _
                                    ByVal dblWartosc As Decimal, ByVal dblCOD As Decimal, ByVal strTypDPD As String) As Boolean
        Dim i As Integer
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik As wsCursorProf.KoszykZapiszWynik
        Dim CzyZapisacDaneDostawy As Integer = IIf(CzyZapisujemyDaneDostawy(intDokZw, intPrzZw, intOsPryw, dblWartosc, dblCOD, strTypDPD), 1, 0)
        Try
            Dim ds As New DataSet
            Dim dataTmp As New DateTime(1, 1, 1)


            Dim dtn As DateTime
            dtn = DateTime.Now()
            If dtpDataRealizacji.Value.Date < minimalnaDataRealizacji().Date Then
                MsgBox(String.Format("Nie można wybrać przewidywanej daty dostawy wcześniejszej niż {0}", minimalnaDataRealizacji().ToString("yyyy-MM-dd")), MsgBoxStyle.Exclamation, Me.Text)
                status = -1
                opis_statusu = String.Format("Nie można wybrać przewidywanej daty dostawy wcześniejszej niż {0}", minimalnaDataRealizacji().ToString("yyyy-MM-dd"))
                numer_zamowienia = "---"
                Return False
            End If
            dataTmp = dtpDataRealizacji.Value


            ds.Tables.Add(dtStanNaPodstawieSKU.Copy)
            '' usuwamy z pozycji zamówienia te SKU których ilość = 0
            For i = ds.Tables(0).Rows.Count - 1 To 0 Step -1
                If ds.Tables(0).Rows(i).Item(EnumDtPozycjeZamowienia.ilosc.ToString) < 1 Then
                    ds.Tables(0).Rows.RemoveAt(i)
                End If
            Next
            Cursor = Cursors.WaitCursor
            Application.DoEvents()

            wsWynik = ws.KoszykZapisz(frmGlowna.sesja, blokada_id, magazyn_id, _
                                 -1, -1, nazwa, adres, kod, miasto, _
                                 osoba_kontaktowa, telefon_kontaktowy, uwagi, 4, ds, dataTmp, -1, _
                                 CzyZapisacDaneDostawy, intDokZw, intOsPryw, intPrzZw, dblCOD, dblWartosc, strTypDPD, "", "", "", "", "")

            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            status = -1
            opis_statusu = "Błąd komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message
            Return False
        End Try

        If wsWynik.status < 0 Then
            status = wsWynik.status
            opis_statusu = wsWynik.status_opis
            Return False
        Else
            status = wsWynik.status
            opis_statusu = wsWynik.status_opis
        End If
        Return True
    End Function

    Private Sub frmZamowieniaZExcela_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

        btnZlozZamowienia.Enabled = False

        '' dodajemy 4 kolumny do zmiennej dtPozycjeZamowienia (sku, ilosc, sku_id, ilosc_dostepna)
        dtPozycjeZamowienia.Columns.Add(EnumDtPozycjeZamowienia.sku.ToString)

        Dim colIlosc As DataColumn = New DataColumn(EnumDtPozycjeZamowienia.ilosc.ToString)
        colIlosc.DataType = System.Type.GetType("System.Int32")
        dtPozycjeZamowienia.Columns.Add(colIlosc)

        Dim colSku_ID As DataColumn = New DataColumn(EnumDtPozycjeZamowienia.sku_id.ToString)
        colSku_ID.DataType = System.Type.GetType("System.Int32")
        dtPozycjeZamowienia.Columns.Add(colSku_ID)

        Dim colIloscDost As DataColumn = New DataColumn(EnumDtPozycjeZamowienia.ilosc_dostepna.ToString)
        colIloscDost.DataType = System.Type.GetType("System.Int32")
        dtPozycjeZamowienia.Columns.Add(colIloscDost)

        dtPozycjeZamowienia.Columns.Add(EnumDtPozycjeZamowienia.grupa.ToString)

        dtpDataRealizacji.Value = minimalnaDataRealizacji()

        dtSkuGrupa.Columns.Add(EnumDtSkuGrupa.sku.ToString)
        dtSkuGrupa.Columns.Add(EnumDtSkuGrupa.grupa.ToString)

        Dim dtTable As New DataTable
        dtTable = SprawdzUprawnienia(frmGlowna.sesja)
        If dtTable.Rows.Count > 0 Then
            For Each r As DataRow In dtTable.Rows
                If r.Item("FUNKCJE_ID") = 28 Then
                    bMamyFunkcjeDaneDostawy = True
                End If
            Next
        End If

        If Not doczytajTypy() Then
            Me.Close()
            Exit Sub
        End If

        If Not wczytaj_uprawnienia_dla_danych_dostawy() Then
            Me.Close()
            Exit Sub
        End If

    End Sub

    Private Function SprawdzUprawnienia(ByVal sesja As Byte()) As DataTable
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik = New wsCursorProf.SprawdzFunkcjeWynik
        Try
            Application.DoEvents()
            wsWynik = ws.SprawdzFunkcje(frmGlowna.sesja)
        Catch ex As Exception
            MsgBox("Błąd komunikacji z serwerem. " & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
            Return Nothing
            Exit Function
        End Try
        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, Me.Text)
            Return Nothing
            Exit Function
        ElseIf wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, Me.Text)
        End If
        Return wsWynik.dane.Tables(0)

    End Function

    Private Function OdczytajGrupyZKtorychMoznaZamawiac() As Boolean

        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        'ws.Url = frmGlowna.strWebservice
        Dim wsWynik As wsCursorProf.GrupyZamowieniaExcelOdczytajWynik

        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.GrupyZamowieniaExcelOdczytaj(frmGlowna.sesja, 1)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
            Return False
        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, Me.Text)
            Return False
        ElseIf wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, Me.Text)
        End If
        dtGrupy = wsWynik.dane.Tables(0)
        Return True

    End Function

    Private Function doczytajTypy() As Boolean
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        'ws.Url = frmGlowna.strWebservice
        Dim wsWynik As wsCursorProf.DostawyGwarantowaneTypyWczytajWynik

        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.DostawyGwarantowaneTypyWczytaj(frmGlowna.sesja)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
            Me.Close()
            Return False
            Exit Function
        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, Me.Text)
            Return False
        End If
        If wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, Me.Text)
        End If
        dtTypy = wsWynik.dane.Tables(0)
        If Not dtKody Is Nothing Then
            alGwaranty.Clear()
            For Each col As DataColumn In dtKody.Columns
                If col.ColumnName <> "KOD_POCZTOWY" And col.ColumnName <> "MIASTO" Then
                    alGwaranty.Add(col.ColumnName)
                End If
            Next
        End If

        Return True
    End Function

    Private Function odswiezKodPocztowy(ByVal kod As String) As Boolean
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik As wsCursorProf.KodyPocztoweOdczytajWynik

        Try
            Application.DoEvents()
            wsWynik = ws.KodyPocztoweOdczytaj(frmGlowna.sesja, kod)
        Catch ex As Exception
            MsgBox("Błąd komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
            Me.Close()
            Return False
            Exit Function
        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, Me.Text)
            Return False
        End If
        If wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, Me.Text)
        End If
        dtKody = wsWynik.dane.Tables(0)
        Return True
    End Function

    Private Function validKodPocztowy(ByVal kod As String) As Boolean
        If kod.Length = 6 Then
            If dtKody.Select(String.Format("KOD_POCZTOWY = '{0}'", kod)).Length = 1 Then
                Return True
            Else
                Return False
            End If
        End If
        Return True
    End Function

    Private Function CzyZapisujemyDaneDostawy(ByVal dok_zwr As Integer, ByVal przes_zwr As Integer, ByVal os_pryw As Integer, _
                                              ByVal wartosc_przesylki As Decimal, ByVal COD As Decimal, ByVal DorGwar As String) As Boolean
        Dim bZapisujemy As Boolean = False
        If dok_zwr = 1 Or przes_zwr = 1 Or os_pryw = 1 Or wartosc_przesylki > 0 Or COD > 0 Or DorGwar <> "" Then
            bZapisujemy = True
        Else
            bZapisujemy = False
        End If
        Return bZapisujemy
    End Function

    Private Function wczytaj_uprawnienia_dla_danych_dostawy() As Boolean
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        'ws.Url = frmGlowna.strWebservice
        Dim wsWynik As wsCursorProf.UserDaneDpdWczytajWynik
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.UserDaneDpdWczytaj(frmGlowna.sesja, frmGlowna.intIdUzytkownikZalogowany)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
            Return False
            Exit Function
        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, Me.Text)
            Return False
            Exit Function
        ElseIf wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, Me.Text)
        End If

        Dok_Zwrotne_Enable = wsWynik.Dok_Zwrotne_Enable
        Prz_Zwrotne_Enable = wsWynik.Prz_Zwrotne_Enable
        Osob_Pryw_Enable = wsWynik.Osob_Pryw_Enable
        Wartosc_Enable = wsWynik.Wartosc_Enable
        COD_Enable = wsWynik.COD_Enable
        Dost_Gw_Enable = wsWynik.Dost_Gw_Enable

        Return True
    End Function

    Private Function CzyMamyUprawnienieDoOpcjiDostawy(ByVal opcja_dostawy As String, _
                                                      ByVal wartosc_opcji_dostawy As String, _
                                                      ByVal index_row As Integer) As Boolean

        Try
            If bMamyFunkcjeDaneDostawy Then
                Select Case opcja_dostawy
                    Case EnumPodstawoweNaglowki.dokumenty_zwrotne.ToString
                        If Dok_Zwrotne_Enable < 1 And wartosc_opcji_dostawy = "1" Then
                            MsgBox("W załadowanym pliku Excela, w pozycji " & CStr(index_row + 1) & "." & _
                               " wybrano w danych dostawy dokumenty zwrotne. Niestety nie masz uprawnień do opcji dokumenty zwrotne i dane dostawy nie mogą zostać zapisane! ", MsgBoxStyle.Exclamation, "Brak uprawnień")
                            Return False
                        End If
                    Case EnumPodstawoweNaglowki.przesylka_zwrotna.ToString
                        If Prz_Zwrotne_Enable < 1 And wartosc_opcji_dostawy = "1" Then
                            MsgBox("W załadowanym pliku Excela, w pozycji " & CStr(index_row + 1) & "." & _
                              " wybrano w danych dostawy przesyłka zwrotna. Niestety nie masz uprawnień do opcji przesyłka zwrotna i dane dostawy nie mogą zostać zapisane! ", MsgBoxStyle.Exclamation, "Brak uprawnień")
                            Return False
                        End If
                    Case EnumPodstawoweNaglowki.osoba_prywatna.ToString
                        If Osob_Pryw_Enable < 1 And wartosc_opcji_dostawy = "1" Then
                            MsgBox("W załadowanym pliku Excela, w pozycji " & CStr(index_row + 1) & "." & _
                              " wybrano w danych dostawy osoba prywatna. Niestety nie masz uprawnień do opcji osoba prywatna i dane dostawy nie mogą zostać zapisane! ", MsgBoxStyle.Exclamation, "Brak uprawnień")
                            Return False
                        End If
                    Case EnumPodstawoweNaglowki.wartosc_przesylki.ToString
                        If Wartosc_Enable < 1 And wartosc_opcji_dostawy > 0 Then
                            MsgBox("W załadowanym pliku Excela, w pozycji " & CStr(index_row + 1) & "." & _
                            " wybrano w danych dostawy wartość przesyłki. Niestety nie masz uprawnień do opcji wartość przesyłki i dane dostawy nie mogą zostać zapisane! ", MsgBoxStyle.Exclamation, "Brak uprawnień")
                            Return False
                        End If
                    Case EnumPodstawoweNaglowki.kwota_pobrania.ToString
                        If COD_Enable < 1 And wartosc_opcji_dostawy > 0 Then
                            MsgBox("W załadowanym pliku Excela, w pozycji " & CStr(index_row + 1) & "." & _
                            " wybrano w danych dostawy kwotę pobrania. Niestety nie masz uprawnień do opcji kwota pobrania i dane dostawy nie mogą zostać zapisane! ", MsgBoxStyle.Exclamation, "Brak uprawnień")
                            Return False
                        End If
                    Case EnumPodstawoweNaglowki.doreczenie_gwarantowane.ToString
                        If Dost_Gw_Enable < 1 And wartosc_opcji_dostawy <> "" Then
                            MsgBox("W załadowanym pliku Excela, w pozycji " & CStr(index_row + 1) & "." & _
                            " wybrano w danych dostawy doręczenie gwarantowane. Niestety nie masz uprawnień do opcji doręczenie gwarantowane i dane dostawy nie mogą zostać zapisane! ", MsgBoxStyle.Exclamation, "Brak uprawnień")
                            Return False
                        End If
                End Select

            Else '' użytkownik nie posiada funkcji Dane dostawy
                '' jeśli okazało się, że wybrał dane dostawy to zgłaszamy stosowny komunikat
                Select Case opcja_dostawy
                    Case EnumPodstawoweNaglowki.doreczenie_gwarantowane.ToString
                        If wartosc_opcji_dostawy <> "" Then
                            MsgBox("W załadowanym pliku Excela, w pozycji " & CStr(index_row + 1) & "." & _
                            " wybrano w danych dostawy kwotę pobrania. Niestety nie możesz złożyć zamówień ze wskazymi danymi dostawy, ponieważ nie masz przypisanej funkcji Dane dostawy! ", MsgBoxStyle.Exclamation, "Brak uprawnień")
                            Return False
                        End If
                    Case Else
                        If wartosc_opcji_dostawy > 0 Then
                            MsgBox("W załadowanym pliku Excela, w pozycji " & CStr(index_row + 1) & "." & _
                            " wybrano w danych dostawy kwotę pobrania. Niestety nie możesz złożyć zamówień ze wskazymi danymi dostawy, ponieważ nie masz przypisanej funkcji Dane dostawy! ", MsgBoxStyle.Exclamation, "Brak uprawnień")
                            Return False
                        End If
                End Select
            End If

        Catch ex As Exception
            MsgBox("Błąd przy sprawdzaniu uprawnień opcji dostawy w pozycji " & CStr(index_row + 1) & "." & _
                       ex.Message.ToString, MsgBoxStyle.Exclamation, "Wystąpił błąd")
            Return False
        End Try

        Return True
    End Function

    Private Sub btnZapiszLubZatwierdzZamowienieEnabledChanged(sender As Object, e As System.EventArgs) Handles btnZlozZamowienia.EnabledChanged
        If sender.Enabled Then
            sender.BackColor = Color.DodgerBlue
            sender.ForeColor = Color.White
        Else
            sender.BackColor = Color.LightGray
            sender.ForeColor = Color.Gray
        End If
    End Sub

    Private Sub btnSzablon_Click(sender As System.Object, e As System.EventArgs) Handles btnSzablon.Click
        PobierzPlik(nazwaSzablonu)
    End Sub

    Private Function PobierzPlik(ByVal nazwa As String) As Boolean
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik = New wsCursorProf.PobierzPlikWynik

        'pobieranie pliku przez usera
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.PobierzPlik(nazwa)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, "Pobieranie pliku")
            Return False
        End Try
        If wsWynik.status = -1 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, "Pobieranie pliku")
            Return False
        End If
        If wsWynik.status = 0 Then

            Dim plik() As Byte = CType(wsWynik.plik, Byte())

            Dim sfd As New SaveFileDialog()
            Dim typ_pliku As String = ""
            Dim extension As String = Path.GetExtension("Szablon.xlsx")
            sfd.Filter = "*" & extension & "|*" & extension
            sfd.FileName = nazwaSzablonu


            If sfd.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim zapiszPath As String = sfd.FileName
                Dim fs As New FileStream(zapiszPath, FileMode.Create)
                fs.Write(plik, 0, plik.Length)
                fs.Flush()
                fs.Close()
                fs.Dispose()
                MsgBox("Poprawnie zapisano plik '" + zapiszPath + "'", MsgBoxStyle.Information, "Zapis pliku")
            End If

        End If
        Return True
    End Function

    Public Function NZ(ByVal S As Object, ByVal Def As Object) As Object
        If IsDBNull(S) Then
            Return Def
        Else
            If Not (S Is Nothing) Then
                Return (S)
            Else
                Return Def
            End If
        End If
    End Function

End Class