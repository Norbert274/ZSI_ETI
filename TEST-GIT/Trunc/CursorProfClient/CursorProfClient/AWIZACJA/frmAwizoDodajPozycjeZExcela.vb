Imports System.Reflection
Imports System
Imports System.IO
Imports Microsoft.Office.Interop
Imports System.Text.RegularExpressions
Imports System.Data.SqlClient
Imports System.Threading
Imports System.Globalization
Public Class frmAwizoDodajPozycjeZExcela
    Public dtSKUExcel As New DataTable
    Public dtGrupy As New DataTable
    Public dtSKUBaza As New DataTable
    Private nazwa_pliku_XLS As String
    Private NazwaArkusza As String = ""
    Private ile_sku As Integer
    Private nazwaSzablonu As String = "Szablon_Dodaj_Pozycje_Awiza.xlsx"
    'Private lst As New List(Of String)
    Private Sub btnWybierzPlik_Click(sender As System.Object, e As System.EventArgs) Handles btnWybierzPlik.Click
        Dim i As Integer = 0
        Dim j As Integer = 0
        lblIloscSKU.Text = ""

        Dim ofdPlikXLS As New OpenFileDialog
        ofdPlikXLS.Filter = "Pliki Programu Excel (*.xls, *.xlsx)|*.xls;*.xlsx"
        ofdPlikXLS.ReadOnlyChecked = False
        If ofdPlikXLS.ShowDialog = Windows.Forms.DialogResult.OK Then

            'deklaracja zmiennych pliku excel z zamówieniami
            Dim objXls = CreateObject("Excel.Application")
            Dim wkb As Excel.Workbook

            objXls.DisplayAlerts = False
            Try
                'wkb = objXls.Workbooks.Open(ofdPlikXLS.FileName, , False)
                'DirectCast(objXls.Workbooks.Open(ofdPlikXLS.FileName), Excel.Workbook)
                wkb = objXls.Workbooks.Open(fileName:=ofdPlikXLS.FileName, ReadOnly:=False)
            Catch ex As Exception
                MsgBox("Nie można założyć produktów z pliku " & ofdPlikXLS.FileName & _
                       ", gdyż jest on otwarty." & vbNewLine & _
                       "Proszę zamknąć ten plik i spróbować ponownie go wybrać.", _
                       MsgBoxStyle.Exclamation, "Wybrany plik jest otwarty")
                objXls.Quit()
                wkb = Nothing
                objXls = Nothing
                Exit Sub
            End Try

            '' czy wkb jest ReadOnly
            If wkb.ReadOnly Then
                MsgBox("Nie można założyć produktów z pliku " & ofdPlikXLS.FileName & _
                      ", gdyż jest on otwarty." & vbNewLine & _
                      "Proszę zamknąć ten plik i spróbować ponownie go wybrać.", _
                      MsgBoxStyle.Exclamation, "Wybrany plik jest otwarty")
                GoTo koniec
            End If

            objXls.Visible = False

            If cmbNazwaArkusza.Items.Count > 0 Then
                cmbNazwaArkusza.Items.Clear()
            End If
            For i = 1 To wkb.Sheets.Count
                cmbNazwaArkusza.Items.Add(wkb.Sheets(i).Name)
            Next

            nazwa_pliku_XLS = ofdPlikXLS.FileName
            txtPlik.Text = ofdPlikXLS.FileName
koniec:
            wkb.Close()
            objXls.DisplayAlerts = True
            objXls.Quit()
            wkb = Nothing
            objXls = Nothing
        End If
    End Sub

    Private Function SprawdzNaglowekKolumny(ByVal wpisany As String, _
                                           ByVal poprawny As String, _
                                           ByVal ExcelCell As String) As Boolean
        If wpisany.ToUpper.Trim <> poprawny.ToUpper.Trim Then
            Cursor = Cursors.Default
            MsgBox("Niepoprawny nagłówek kolumny w komórce """ & ExcelCell & """. " & vbNewLine & _
                   "Wpisano: " & wpisany & vbNewLine & _
                  "Prawidłowa nazwa kolumny: " & poprawny & vbNewLine & _
                   vbNewLine & "Proszę poprawić plik i spróbować ponownie złożyć z niego zamówienia. ", _
                   MsgBoxStyle.Exclamation, "Błędnie przygotowany plik Excela")
            Return False
        End If
        Return True
    End Function

    Private Function ExcelWalidacjaSkuIloscGrupa(ByVal sku As String, _
                                                 ByVal ilosc As String, _
                                                 ByVal grupa As String) As Boolean
        Dim czy_poprawne_dane As Boolean = True
        Dim dRow As DataRow()
        '' czy ilość jest liczbą całkowitą większą od zera?
        Dim int_test As Integer = 0
        If Not Integer.TryParse(ilosc, int_test) Then
            Cursor = Cursors.Default
            MsgBox("W załadowanym pliku dla produktu o numerze sku " & sku & " podano ilość, która nie jest liczbą całkowitą!", MsgBoxStyle.Exclamation, "Błędna ilość produktu")
            czy_poprawne_dane = False
        ElseIf CInt(ilosc) < 1 Then
            Cursor = Cursors.Default
            MsgBox("W załadowanym pliku dla produktu o numerze sku " & sku & " podano ilość, mniejszą od 1.", MsgBoxStyle.Exclamation, "Błędna ilość produktu")
            czy_poprawne_dane = False
        End If

        '' czy sku istnieje w bazie danych?
        dRow = dtSKUBaza.Select("sku='" & sku & "'")
        If Not dRow.Length > 0 Then
            Cursor = Cursors.Default
            MsgBox("W załadowanym pliku produkt o numerze sku " & sku & " nie istnieje w systemie!", MsgBoxStyle.Exclamation, "Błędny numer SKU")
            czy_poprawne_dane = False
        End If

        '' czy grupa istnieje w bazie danych?
        dRow = dtGrupy.Select("grupa='" & grupa & "'")
        If Not dRow.Length > 0 Then
            Cursor = Cursors.Default
            MsgBox("W załadowanym pliku dla produktu o numerze sku " & sku & " podano nazwę grupy: " & grupa & _
                   ", która nie w systemie!", MsgBoxStyle.Exclamation, "Błędna nazwa grupy")
            czy_poprawne_dane = False
        End If

        If Not czy_poprawne_dane Then
            Return False
        End If
        Return True
    End Function

    Private Function UstalGrupaId(ByVal sku As String, ByVal nazwa_grupy As String) As Integer

        '' ustalamy grupa_id dla nazwy grupy
        Dim grupa_id As Integer = 0
        For Each r As DataRow In dtGrupy.Rows
            If r.Item("grupa") = nazwa_grupy Then
                grupa_id = r.Item("grupa_id")
                Exit For
            End If
        Next
        If grupa_id = 0 Then
            Cursor = Cursors.Default
            MsgBox("W załadowanym pliku dla SKU " & sku & _
                   " nie ustalono wartości grupa_id dla grupy o nazwie: " & _
                   nazwa_grupy & " !" & vbNewLine, MsgBoxStyle.Exclamation, "Brak parametru grupa_id")
        End If
        Return grupa_id
    End Function

    Private Function UstalNazweSku(ByVal sku As String) As String
        Dim nazwa_sku As String = ""
        For Each r As DataRow In dtSKUBaza.Rows
            If r.Item("sku") = sku Then
                nazwa_sku = r.Item("nazwa")
                Exit For
            End If
        Next
        If nazwa_sku = "" Then
            Cursor = Cursors.Default
            MsgBox("W załadowanym pliku nie ustalono nazwy produktu dla SKU " & _
                   sku & " !" & vbNewLine, MsgBoxStyle.Exclamation, "Brak nazwy SKU")
        End If
        Return nazwa_sku
    End Function

    Private Sub btnOK_Click(sender As System.Object, e As System.EventArgs) Handles btnOK.Click
        Dim i As Integer
        Dim czyOK As Boolean = True '' jeśli True to DialogResult = Windows.Forms.DialogResult.OK a jeśli nie to DialogResult = Windows.Forms.DialogResult.No
        Dim Process() As Process = System.Diagnostics.Process.GetProcessesByName("excel")
        '' sprawdzamy czy wybrano arkusz
        If cmbNazwaArkusza.SelectedIndex = -1 Then
            MsgBox("Nie wybrano nazwy arkusza, z którego mają zostać wczytane pozycje do awiza!", MsgBoxStyle.Exclamation, "Brak nazwy arkusza")
            Exit Sub
        End If

        NazwaArkusza = cmbNazwaArkusza.Text

        'deklaracja zmiennych pliku excel z zamówieniami
        Dim objXls = CreateObject("Excel.Application")
        Dim wkb As Excel.Workbook '' DirectCast(objXls.Workbooks.Open(nazwa_pliku_XLS), Excel.Workbook)

        objXls.DisplayAlerts = False
        Try
            wkb = objXls.Workbooks.Open(fileName:=nazwa_pliku_XLS, ReadOnly:=False)
        Catch ex As Exception
            MsgBox("Plik " & nazwa_pliku_XLS & _
                   ", jest on otwarty." & vbNewLine & _
                   "Proszę go zamknąć i spróbować ponownie wybrać.", _
                   MsgBoxStyle.Exclamation, "Wybrany plik jest otwarty")
            objXls.Quit()
            wkb = Nothing
            objXls = Nothing
            DialogResult = Windows.Forms.DialogResult.No
            Me.Close()
            Exit Sub
        End Try

        '' czy wkb jest ReadOnly
        If wkb.ReadOnly Then
            MsgBox("Plik " & nazwa_pliku_XLS & _
                  ", jest otwarty." & vbNewLine & _
                  "Proszę go zamknąć i spróbować ponownie wybrać.", _
                  MsgBoxStyle.Exclamation, "Wybrany plik jest otwarty")
            czyOK = False
            GoTo koniec
        End If

        objXls.Visible = False
        Cursor = Cursors.WaitCursor

        '' sprawdzamy czy mamy poprawnie sformatowany plik Excela wg poniższego układu kolumn:
        Dim dtNaglowki As New DataTable
        dtNaglowki.Columns.Add("naglowek")
        dtNaglowki.Columns.Add("excel_cell")
        dtNaglowki.Rows.Add("Kod dla Cursor", "A1")
        dtNaglowki.Rows.Add("Nazwa dla Cursor", "B1")
        dtNaglowki.Rows.Add("Ilość", "C1")
        dtNaglowki.Rows.Add("Kategoria A,B,C", "D1")
        dtNaglowki.Rows.Add("Kategoria w systemie", "E1")
        dtNaglowki.Rows.Add("Grupa", "H1")

        Dim czy_poprawne_naglowki As Boolean = True
        For Each r As DataRow In dtNaglowki.Rows
            If Not SprawdzNaglowekKolumny(Trim(CStr(wkb.Sheets(NazwaArkusza).Range(r.Item("excel_cell")).Value)), _
                                          r.Item("naglowek").ToString, r.Item("excel_cell")) Then
                czy_poprawne_naglowki = False
            End If
        Next

        If czy_poprawne_naglowki = False Then
            '' nazwa lub nazwy kolumn różnią się od nazw z szablonu; kończymy procedurę
            czyOK = False
            GoTo koniec
        End If

        Dim j As Integer = 0
        lblIloscSKU.Text = ""

        '' Jeśli dtSKUExcel ma już wiersze to usuwamy je
        If dtSKUExcel.Rows.Count > 0 Then
            dtSKUExcel.Rows.Clear()
        End If

        objXls.DisplayAlerts = False
        wkb.Save()

        Dim grupa_id As Integer = 0
        Dim nazwa_sku As String = ""
        i = 0
        '' dodajemy kolejno dane z pliku Excela do zmiennej dtSKUExcel
        Do Until CStr(wkb.Sheets(NazwaArkusza).Range("A2").Offset(i, 0).Value) = ""
            If Not ExcelWalidacjaSkuIloscGrupa(Trim(CStr(wkb.Sheets(NazwaArkusza).Range("A2").Offset(i, 0).Value)), _
                                               Trim(CStr(wkb.Sheets(NazwaArkusza).Range("C2").Offset(i, 0).Value)), _
                                               Trim(CStr(wkb.Sheets(NazwaArkusza).Range("H2").Offset(i, 0).Value))) Then
                czyOK = False
                GoTo koniec
            End If

            '' ustalamy grupa_id dla nazwy grupy
            grupa_id = UstalGrupaId(Trim(CStr(wkb.Sheets(NazwaArkusza).Range("A2").Offset(i, 0).Value)), _
                                    Trim(CStr(wkb.Sheets(NazwaArkusza).Range("H2").Offset(i, 0).Value)))
            If grupa_id = 0 Then
                czyOK = False
                GoTo koniec
            End If

            '' ustalamy nazwę produktu z bazy dla sku z Excela, aby nie wstawiać nazwy sku z Excela
            nazwa_sku = UstalNazweSku(Trim(CStr(wkb.Sheets(NazwaArkusza).Range("A2").Offset(i, 0).Value)))
            If nazwa_sku = "" Then
                czyOK = False
                GoTo koniec
            End If

            dtSKUExcel.Rows.Add()
            dtSKUExcel.Rows(i).Item("sku") = Trim(CStr(wkb.Sheets(NazwaArkusza).Range("A2").Offset(i, 0).Value))

            dtSKUExcel.Rows(i).Item("nazwa") = nazwa_sku '' wkb.Sheets(NazwaArkusza).Range("B2").Offset(i, 0).Value
            dtSKUExcel.Rows(i).Item("ilosc") = CInt(wkb.Sheets(NazwaArkusza).Range("C2").Offset(i, 0).Value)
            dtSKUExcel.Rows(i).Item("GRUPA_ID") = grupa_id '' wkb.Sheets(NazwaArkusza).Range("E2").Offset(i, 0).Value
            i = i + 1
        Loop

        ' ilość produktow
        ile_sku = i
        lblIloscSKU.Text = "Ilość produktów: " & ile_sku

        '' sprawdzamy, czy dtSKUExcel mają choć jeden rekord
        If dtSKUExcel.Rows.Count > 0 Then
            btnOK.Enabled = True
        Else
            btnOK.Enabled = False
            czyOK = False
            MsgBox("W załadowanym pliku nie ma żadnego produktu do założenia!" & vbNewLine, _
                      MsgBoxStyle.Exclamation, _
                     "Brak produktów w pliku")
        End If
koniec:
        wkb.Close()
        objXls.DisplayAlerts = True
        objXls.Quit()
        wkb = Nothing
        objXls = Nothing
        Cursor = Cursors.Default
        If czyOK Then
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Else
            Me.DialogResult = System.Windows.Forms.DialogResult.No
        End If
        Me.Close()
    End Sub

    Private Sub btnZamknij_Click(sender As System.Object, e As System.EventArgs) Handles btnZamknij.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmAwizoDodajPozycjeZExcela_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        dtSKUExcel.Columns.Add("sku")
        dtSKUExcel.Columns.Add("nazwa")
        dtSKUExcel.Columns.Add("ilosc")
        dtSKUExcel.Columns.Add("GRUPA_ID", GetType(Integer))
        '' wczytujemy z serwera listę SKU
        If Not wczytaj() Then Me.Close()
    End Sub

    Private Function wczytaj() As Boolean

        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik As wsCursorProf.SkuListaWczytajWynik

        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.SkuListaWczytaj(frmGlowna.sesja)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, "Wczytanie listy produktów")
            Return False
            Me.Close()
            Exit Function
        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, "Wczytanie listy produktów")
            Return False
            Exit Function
        End If
        If wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, "Wczytanie listy produktów")
            Return False
            Exit Function
        End If
        dtSKUBaza = wsWynik.dane.Tables(0)
        'If wsWynik.dane.Tables.Count > 1 And wsWynik.dane.Tables(1).Rows.Count > 0 Then
        '    dtkategorie = wsWynik.dane.Tables(1)
        'Else
        '    MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał listy dostępnych kategorji." & frmGlowna.kontaktIt, MsgBoxStyle.Critical)
        '    Return False
        '    Me.Close()
        '    Exit Function
        'End If
        'bWczytaj = True
        'cmbGrupa.DataSource = dtGrupy
        'cmbGrupa.DisplayMember = "GRUPA"
        'cmbGrupa.ValueMember = "GRUPA_ID"
        'If Grupa_id > 0 Then
        '    cmbGrupa.SelectedValue = Grupa_id
        'End If
        'bWczytaj = False
        Return True
    End Function

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
End Class