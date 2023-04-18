Imports System.Reflection
Imports System
Imports System.IO
Imports Microsoft.Office.Interop
Imports System.Text.RegularExpressions
Imports System.Data.SqlClient
Imports System.Threading
Imports System.Globalization
Public Class frmSKUNoweZExcela
    Private dtProdukty As New DataTable
    Private nazwa_pliku_XLS As String
    Private NazwaArkusza As String = ""
    Private ile_sku As Integer
    Private lstKlasy As New List(Of String)
    Public dtGrupyArtQ As DataTable
    Private nazwaSzablonu As String = "Szablon_Dodaj_Pozycje_Awiza.xlsx"

    Private Sub btnWybierzPlik_Click(sender As System.Object, e As System.EventArgs) Handles btnWybierzPlik.Click

        Dim i As Integer = 0
        Dim j As Integer = 0
        ProgressBar1.Value = 0
        lblProgress.Text = ""
        lblIloscSKU.Text = ""

        If dgv.Columns.Contains("L.p.") Then dgv.Columns.Remove("L.p.")
        dgv.DataSource = Nothing

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
                wkb.Close()
                objXls.Quit()
                wkb = Nothing
                objXls = Nothing
                Exit Sub
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

            wkb.Close()
            objXls.Quit()
            wkb = Nothing
            objXls = Nothing
        End If
    End Sub

    Private Sub cmbNazwaArkusza_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbNazwaArkusza.SelectedIndexChanged

        Dim i As Integer
        Dim Process() As Process = System.Diagnostics.Process.GetProcessesByName("excel")
        '' sprawdzamy czy wybrano arkusz
        If cmbNazwaArkusza.SelectedIndex = -1 Then
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
            Exit Sub
        End Try

        '' czy wkb jest ReadOnly
        If wkb.ReadOnly Then
            MsgBox("Plik " & nazwa_pliku_XLS & _
                  ", jest otwarty." & vbNewLine & _
                  "Proszę go zamknąć i spróbować ponownie wybrać.", _
                  MsgBoxStyle.Exclamation, "Wybrany plik jest otwarty")
            wkb.Close()
            objXls.Quit()
            wkb = Nothing
            objXls = Nothing
            Exit Sub
        End If


        objXls.Visible = False

        '' sprawdzamy czy mamy poprawnie sformatowany plik Excela z produktami
        Dim czy_poprawne_naglowki As Boolean = True
        If Replace(CStr(wkb.Sheets(NazwaArkusza).Range("A1").Value), vbLf, "") <> "Kod dla Cursor" Then
            MsgBox("Niepoprawny nagłówek kolumny w komórce ""A1"". " & vbNewLine & _
                   "Wpisano: " & CStr(wkb.Sheets(NazwaArkusza).Range("A1").Value) & vbNewLine & _
                  "Prawidłowa nazwa kolumny: Kod dla Cursor" & vbNewLine & _
                   vbNewLine & "Proszę poprawić plik i spróbować ponownie założyć z niego produkty. ", MsgBoxStyle.Exclamation, "Błędnie przygotowany plik Excela")

            czy_poprawne_naglowki = False
        End If

        If Replace(CStr(wkb.Sheets(NazwaArkusza).Range("B1").Value), vbLf, "") <> "Nazwa dla Cursor" Then
            MsgBox("Niepoprawny nagłówek kolumny w komórce ""B1"". " & vbNewLine & _
                  "Wpisano: " & CStr(wkb.Sheets(NazwaArkusza).Range("B1").Value) & vbNewLine & _
                 "Prawidłowa nazwa kolumny: Nazwa dla Cursor" & vbNewLine & _
                  vbNewLine & "Proszę poprawić plik i spróbować ponownie założyć z niego produkty. ", MsgBoxStyle.Exclamation, "Błędnie przygotowany plik Excela")

            czy_poprawne_naglowki = False
        End If

        If Replace(CStr(wkb.Sheets(NazwaArkusza).Range("C1").Value), vbLf, "") <> "Ilość" Then
            MsgBox("Niepoprawny nagłówek kolumny w komórce ""C1"". " & vbNewLine & _
                  "Wpisano: " & CStr(wkb.Sheets(NazwaArkusza).Range("C1").Value) & vbNewLine & _
                 "Prawidłowa nazwa kolumny: Ilość" & vbNewLine & _
                  vbNewLine & "Proszę poprawić plik i spróbować ponownie założyć z niego produkty. ", MsgBoxStyle.Exclamation, "Błędnie przygotowany plik Excela")

            czy_poprawne_naglowki = False
        End If

        If Replace(CStr(wkb.Sheets(NazwaArkusza).Range("D1").Value), vbLf, "") <> "Kategoria A,B,C" Then
            MsgBox("Niepoprawny nagłówek kolumny w komórce ""D1"". " & vbNewLine & _
                  "Wpisano: " & CStr(wkb.Sheets(NazwaArkusza).Range("D1").Value) & vbNewLine & _
                 "Prawidłowa nazwa kolumny: Kategoria A,B,C" & vbNewLine & _
                  vbNewLine & "Proszę poprawić plik i spróbować ponownie założyć z niego produkty. ", MsgBoxStyle.Exclamation, "Błędnie przygotowany plik Excela")

            czy_poprawne_naglowki = False
        End If

        If Replace(CStr(wkb.Sheets(NazwaArkusza).Range("E1").Value), vbLf, "") <> "Kategoria w systemie" Then
            MsgBox("Niepoprawny nagłówek kolumny w komórce ""E1"". " & vbNewLine & _
                  "Wpisano: " & CStr(wkb.Sheets(NazwaArkusza).Range("E1").Value) & vbNewLine & _
                 "Prawidłowa nazwa kolumny: Kategoria w systemie" & vbNewLine & _
                  vbNewLine & "Proszę poprawić plik i spróbować ponownie założyć z niego produkty. ", MsgBoxStyle.Exclamation, "Błędnie przygotowany plik Excela")

            czy_poprawne_naglowki = False
        End If

        If Replace(CStr(wkb.Sheets(NazwaArkusza).Range("F1").Value), vbLf, "") <> "Brand" Then
            MsgBox("Niepoprawny nagłówek kolumny w komórce ""F1"". " & vbNewLine & _
                  "Wpisano: " & CStr(wkb.Sheets(NazwaArkusza).Range("F1").Value) & vbNewLine & _
                 "Prawidłowa nazwa kolumny: Brand" & vbNewLine & _
                  vbNewLine & "Proszę poprawić plik i spróbować ponownie założyć z niego produkty. ", MsgBoxStyle.Exclamation, "Błędnie przygotowany plik Excela")

            czy_poprawne_naglowki = False
        End If

        If Replace(CStr(wkb.Sheets(NazwaArkusza).Range("G1").Value), vbLf, "") <> "Jednostki miary" Then
            MsgBox("Niepoprawny nagłówek kolumny w komórce ""G1"". " & vbNewLine & _
                  "Wpisano: " & CStr(wkb.Sheets(NazwaArkusza).Range("G1").Value) & vbNewLine & _
                 "Prawidłowa nazwa kolumny: Jednostki miary" & vbNewLine & _
                  vbNewLine & "Proszę poprawić plik i spróbować ponownie założyć z niego produkty. ", MsgBoxStyle.Exclamation, "Błędnie przygotowany plik Excela")

            czy_poprawne_naglowki = False
        End If

        If Replace(CStr(wkb.Sheets(NazwaArkusza).Range("H1").Value), vbLf, "") <> "Grupa" Then
            MsgBox("Niepoprawny nagłówek kolumny w komórce ""H1"". " & vbNewLine & _
                  "Wpisano: " & CStr(wkb.Sheets(NazwaArkusza).Range("H1").Value) & vbNewLine & _
                 "Prawidłowa nazwa kolumny: Grupa" & vbNewLine & _
                  vbNewLine & "Proszę poprawić plik i spróbować ponownie założyć z niego produkty. ", MsgBoxStyle.Exclamation, "Błędnie przygotowany plik Excela")

            czy_poprawne_naglowki = False
        End If

        If czy_poprawne_naglowki = False Then
            btnZalozProdukty.Enabled = False
            wkb.Close()
            objXls.Quit()
            wkb = Nothing
            objXls = Nothing
            Exit Sub
        End If


        Dim j As Integer = 0
        ProgressBar1.Value = 0
        lblProgress.Text = ""
        lblIloscSKU.Text = ""

        If dgv.Columns.Contains("L.p.") Then dgv.Columns.Remove("L.p.")
        dgv.DataSource = Nothing

        '' Jeśli dtProdukty ma już wiersze to usuwamy je
        If dtProdukty.Rows.Count > 0 Then
            For i = dtProdukty.Rows.Count - 1 To 0 Step -1
                dtProdukty.Rows.RemoveAt(i)
            Next
        End If

        '' Jeśli dtProdukty ma już kolumny to usuwamy je
        If dtProdukty.Columns.Count > 0 Then
            For i = dtProdukty.Columns.Count - 1 To 0 Step -1
                dtProdukty.Columns.RemoveAt(i)
            Next
        End If

        '' Dodajemy kolumny do zmiennej dtProdukty
        dtProdukty.Columns.Add("L.p.")
        dtProdukty.Columns.Add("kod_dla_Cursor")
        dtProdukty.Columns.Add("nazwa_dla_Cursor")
        dtProdukty.Columns.Add("kategoria_ABC")
        dtProdukty.Columns.Add("kategoria_w_systemie")
        dtProdukty.Columns.Add("brand")
        dtProdukty.Columns.Add("jm")
        dtProdukty.Columns.Add("status")
        dtProdukty.Columns.Add("status_opis")

        wkb.Sheets(NazwaArkusza).Range("I1") = "STATUS"
        wkb.Sheets(NazwaArkusza).Range("J1") = "STATUS_OPIS"

        objXls.DisplayAlerts = False
        wkb.Save()

        i = 0
        '' dodajemy kolejno dane z pliku Excela do zmiennej dtProdukty
        Do Until CStr(wkb.Sheets(NazwaArkusza).Range("A2").Offset(i, 0).Value) = ""
            dtProdukty.Rows.Add()
            If Not lstKlasy.Contains(wkb.Sheets(NazwaArkusza).Range("D2").Offset(i, 0).Value) Then
                MsgBox("W załadowanym pliku w linii " & CStr(i + 1) & _
                       ". w kolumnie Kategoria A,B,C podano niepoprawną wartosć: " & _
                       wkb.Sheets(NazwaArkusza).Range("D2").Offset(i, 0).Value & _
                       "! Dopuszczalne są jedynie wartości: A, B lub C." & vbNewLine, _
                  MsgBoxStyle.Exclamation, _
                 "Niepoprawna wartość")
                wkb.Close()
                objXls.Quit()
                wkb = Nothing
                objXls = Nothing
                Exit Sub
            End If
            dtProdukty.Rows(i).Item("L.p.") = i + 1
            dtProdukty.Rows(i).Item("kod_dla_Cursor") = wkb.Sheets(NazwaArkusza).Range("A2").Offset(i, 0).Value
            dtProdukty.Rows(i).Item("nazwa_dla_Cursor") = wkb.Sheets(NazwaArkusza).Range("B2").Offset(i, 0).Value
            dtProdukty.Rows(i).Item("kategoria_ABC") = wkb.Sheets(NazwaArkusza).Range("D2").Offset(i, 0).Value
            dtProdukty.Rows(i).Item("kategoria_w_systemie") = wkb.Sheets(NazwaArkusza).Range("E2").Offset(i, 0).Value
            dtProdukty.Rows(i).Item("brand") = wkb.Sheets(NazwaArkusza).Range("F2").Offset(i, 0).Value
            dtProdukty.Rows(i).Item("jm") = wkb.Sheets(NazwaArkusza).Range("G2").Offset(i, 0).Value
            i = i + 1
        Loop

        ' ilość produktow
        ile_sku = i
        lblIloscSKU.Text = "Ilość produktów: " & ile_sku

        '' sprawdzamy, czy dtProdukty mają choć jeden rekord
        If dtProdukty.Rows.Count > 0 Then
            btnZalozProdukty.Enabled = True
            dgv.DataSource = dtProdukty
        Else
            btnZalozProdukty.Enabled = False
            MsgBox("W załadowanym pliku nie ma żadnego produktu do założenia!" & vbNewLine, _
                      MsgBoxStyle.Exclamation, _
                     "Brak produktów w pliku")
        End If

        wkb.Close()
        objXls.Quit()
        wkb = Nothing
        objXls = Nothing
  
    End Sub

    Private Sub btnAnuluj_Click(sender As System.Object, e As System.EventArgs) Handles btnAnuluj.Click
        Me.Close()
    End Sub

    Private Sub btnZalozProdukty_Click(sender As System.Object, e As System.EventArgs) Handles btnZalozProdukty.Click
       
        Dim i As Integer
        Dim ile_sku_zalozono As Integer = 0
        Dim ile_sku_nie_zalozono As Integer = 0
        Dim status As Integer
        Dim opis_statusu As String
        Dim numer_sku As String
        Dim nazwa_sku As String
        Dim klasa As String
        Dim kategoria_systemowa As String
        Dim brand As String
        Dim jm As String

        Dim Plik_EXCEL As Byte()
        Dim numery_zalozonych_sku As String = ""

        '' sprawdzamy czy wybrano plik Excela
        If txtPlik.Text = String.Empty Then
            MsgBox("Nie wybrano pliku Excela z zamówieniami.", MsgBoxStyle.Exclamation, "Brak pliku Excela")
            Exit Sub
        End If

        '' sprawdzamy czy wybrano grupę artykułów
        If Not IIf(cmbGrupaArtykulow.SelectedValue Is Nothing, -1, cmbGrupaArtykulow.SelectedValue) > 0 Then
            MsgBox("Nie wybrano grupy artykułów", MsgBoxStyle.Exclamation, "Brak grupy artykułów")
            Exit Sub
        End If

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
            Exit Sub
        End Try

        '' czy wkb jest ReadOnly
        If wkb.ReadOnly Then
            MsgBox("Plik " & nazwa_pliku_XLS & _
                  ", jest otwarty." & vbNewLine & _
                  "Proszę go zamknąć i spróbować ponownie wybrać.", _
                  MsgBoxStyle.Exclamation, "Wybrany plik jest otwarty")
            wkb.Close()
            objXls.Quit()
            wkb = Nothing
            objXls = Nothing
            Exit Sub
        End If


        objXls.Visible = False
        ProgressBar1.Maximum = ile_sku - 1
        ProgressBar1.Step = 1

        For i = 0 To ile_sku - 1

            dgv.CurrentCell = dgv.Rows(i).Cells(1)
            dgv.BeginEdit(False)
            dgv.EndEdit()
            dgv.Rows(i).Cells(1).Selected = False

            numer_sku = wkb.Sheets(NazwaArkusza).Range("A2").Offset(i, 0).Value
            nazwa_sku = wkb.Sheets(NazwaArkusza).Range("B2").Offset(i, 0).Value
            klasa = wkb.Sheets(NazwaArkusza).Range("D2").Offset(i, 0).Value
            kategoria_systemowa = wkb.Sheets(NazwaArkusza).Range("E2").Offset(i, 0).Value
            brand = wkb.Sheets(NazwaArkusza).Range("F2").Offset(i, 0).Value
            jm = wkb.Sheets(NazwaArkusza).Range("G2").Offset(i, 0).Value

            If Not lstKlasy.Contains(klasa) Then
                status = -1
                opis_statusu = "W pozycji " & CStr(i + 1) & ". podano niepoprawną wartość dla Kategorii A,B,C"
                GoTo wynik_zakladania_produktow
            End If

            System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
            System.Net.ServicePointManager.Expect100Continue = False
            ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
            ws.Proxy.Credentials = CredentialCache.DefaultCredentials
            Dim wsWynik As wsCursorProf.ZalozNowyProduktQGUARWynik

            Try
                Cursor = Cursors.WaitCursor
                Application.DoEvents()
                wsWynik = ws.ZalozNowyProduktQGUAR(frmGlowna.sesja, numer_sku, nazwa_sku, klasa, kategoria_systemowa, brand, jm, 0, cmbGrupaArtykulow.SelectedValue)
                Cursor = Cursors.Default
            Catch ex As Exception
                Cursor = Cursors.Default
                status = -1
                opis_statusu = "Błąd komunikacji z serwerem. Szczegóły błędu:" & ex.Message
                GoTo wynik_zakladania_produktow
            End Try

            If wsWynik.status = 0 Then
                status = 0
                opis_statusu = wsWynik.status_opis
            Else
                status = wsWynik.status
                opis_statusu = wsWynik.status_opis
            End If

wynik_zakladania_produktow:
            '' sprawdzamy czy poprawnie założono produkt i zwiększamy "ile_sku_zalozono" o 1
            If status = 0 Then
                '' zaznaczamy na dgv na zielono poprawnie złożone zamówienie
                dgv.Rows(i).DefaultCellStyle.BackColor = Color.LawnGreen
                ile_sku_zalozono = ile_sku_zalozono + 1
                If numery_zalozonych_sku = "" Then
                    numery_zalozonych_sku = numer_sku
                Else
                    numery_zalozonych_sku = numery_zalozonych_sku & "," & numer_sku
                End If
            Else
                dgv.Rows(i).DefaultCellStyle.BackColor = Color.Tomato
                ile_sku_nie_zalozono = ile_sku_nie_zalozono + 1
            End If


            '' wpisujemy wynik do pliku Excela
            wkb.Sheets(NazwaArkusza).Range("I2").Offset(i, 0) = status
            wkb.Sheets(NazwaArkusza).Range("J2").Offset(i, 0) = opis_statusu
            dgv.Rows(i).Cells("status").Value = status
            dgv.Rows(i).Cells("status_opis").Value = opis_statusu
            ProgressBar1.PerformStep()
            lblProgress.Text = "Wykonano " & CStr(Math.Round((i / (ile_sku - 1)) * 100, 0)) & "%"
            lblPrzetwarzanie.Text = "Trwa przetwarzanie rekordu " & CStr(i + 1) & " z " & ile_sku & " ..."

        Next

koniec:
        objXls.DisplayAlerts = False
        wkb.Save()
        wkb.Close()
        objXls.Quit()
        wkb = Nothing
        objXls = Nothing

        lblPrzetwarzanie.Text = ""

        MsgBox("Zakończono zakładanie produktów." & vbNewLine & _
           "Ilość produktów poprawnie założonych: " & ile_sku_zalozono.ToString & vbNewLine & _
           "Ilość produktów, których nie udało się założyć: " & ile_sku_nie_zalozono.ToString & vbNewLine & _
            "Szczegóły zapisano do wczytanego pliku.", MsgBoxStyle.Information, "Wynik zakładania produktów z pliku Excela")

        '' Zapis pliku Excela do bazy danych 
        If File.Exists(nazwa_pliku_XLS) Then
            Plik_EXCEL = File.ReadAllBytes(nazwa_pliku_XLS)
            '' przygotowujemy zmienną numery_zlozonych_zamowien
            If numery_zalozonych_sku <> "" Then
                If Mid(numery_zalozonych_sku, Len(numery_zalozonych_sku) - 1, 1) = "," Then
                    numery_zalozonych_sku = Mid(numery_zalozonych_sku, 1, Len(numery_zalozonych_sku) - 1)
                End If
            End If
            If Not ZapiszDoBazyPlikExcel(Plik_EXCEL, nazwa_pliku_XLS, numery_zalozonych_sku) Then
                MsgBox("Nie zapisano pliku do bazy danych.", MsgBoxStyle.Exclamation, "Błąd zapisu pliku do bazy danych")
            End If
        End If

        btnZalozProdukty.Enabled = False
        txtPlik.Text = ""

    End Sub

    Private Sub frmSKUNoweZExcela_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        lstKlasy.Add("A")
        lstKlasy.Add("B")
        lstKlasy.Add("C")

        cmbGrupaArtykulow.DataSource = dtGrupyArtQ
        cmbGrupaArtykulow.DisplayMember = "NAZWA"
        cmbGrupaArtykulow.ValueMember = "PROD_GROUP_ID"
        cmbGrupaArtykulow.SelectedValue = -1

    End Sub

    Private Function ZapiszDoBazyPlikExcel(ByVal plik As Byte(), ByVal nazwa_pliku As String, _
                                           ByVal numery_sku As String) As Boolean

        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik As wsCursorProf.PlikExcelZakladanieSkuZapiszWynik

        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.PlikExcelZakladanieSkuZapisz(frmGlowna.sesja, plik, System.IO.Path.GetFileName(txtPlik.Text), numery_sku)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, "Zapis pliku do bazy")
            Return False
        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, "Zapis pliku do bazy")
            Return False
        ElseIf wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, "Zapis pliku do bazy")
        End If

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