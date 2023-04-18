Imports System.IO
Imports OfficeOpenXml
Imports OfficeOpenXml.Style
Imports OfficeOpenXml.Utils
Imports System.Text.RegularExpressions
Imports System.ComponentModel
Imports System.Text

Public Class frmAdresKopiuj
    Private dtUser As DataTable
    Private dtUserDokad As DataTable
    Private dtAdres As New DataTable
    Private nazwa_pliku_XLS As String
    Private intOdlegloscOdNaglowka As Integer = 1
    Private sciezka As String
    Private dtAdresyDoSkopiowania As New DataTable
    Private dtAdresyDoExcela As New DataTable
    Private listaUserDokad As New List(Of Integer)
    Private bCzyMoznaKopiowac As Boolean = True
    Private bCzyDoExcela As Boolean = False
    Private licznikZaznaczonychAdresow As Integer = 0
    Private licznikZaznaczonychUser As Integer = 0
    Private liczbaAdresow As Integer
    Private liczbaAdresowSkopiowanych As Integer = 0
    Private liczbaDubliPrzyKopiowaniu As Integer = 0
    Private nazwaSzablonu As String
    Private iloscDubli As Integer

    Private bWczytano As Boolean = False
    Private kolorAktywnosci As Color = Color.DodgerBlue
    Private kolorBrakuAktywnosci As Color = Color.LightGray
    Private dvUserDokad As DataView

    Const Ok As String = "OK"
    Const BrakNazwy As String = "Brak nazwy"
    Const BrakAdresu As String = "Brak adresu"
    Const KodMaNiepoprawnaStrukture As String = "Kod ma niepoprawną strukturę"
    Const BrakMiasta As String = "Brak miasta"
    Const Telefon9znakow As String = "Telefon musi mieć 9 znaków"
    Const EmailStruktura As String = "E-mail ma niepoprawną strukturę"
    Const BrakOsobyKontaktowej As String = "Brak osoby kontaktowej"

    Const Wybierz As String = "Wybierz"
    Const Nazwa As String = "Nazwa"
    Const Adres As String = "Adres"
    Const Kod As String = "Kod"
    Const Miasto As String = "Miasto"
    Const TelKom As String = "Telefon"
    Const Email As String = "Email"
    Const OsobaKon As String = "OsobaKontaktowa"
    Const Status As String = "status"
    
    Private Enum EnumPodstawoweNaglowki
        ID
        Wybierz
        Nazwa
        Adres
        Miasto
        Kod
        Telefon
        Email
        OsobaKontaktowa
        status
    End Enum

    Public Enum ImportResult
        Success = 0
        Wrongformat = 1
        NotExcel = 2
        FileStillOpen = 3

    End Enum

    Public Enum enTvpAdesyDoSkopiowania
        Nazwa
        Adres
        Kod
        Miasto
        Telefon
        [Email]
        OsobaKontaktowa
        user_id_dokad
        status
    End Enum

    Private Sub frmAdresKopiuj_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        rbUser.Checked = True
        rbExcel.Checked = False
        txtSciezkaExcel.Enabled = False
        btnSciezkaExcel.Enabled = False
        btnSciezkaExcel.BackColor = kolorBrakuAktywnosci
        btnPobierzSzablon.Enabled = False
        btnPobierzSzablon.BackColor = kolorBrakuAktywnosci
        chbUser.Checked = False
        chbAdresy.Checked = False
        btnExcel.Enabled = False
        btnExcel.BackColor = kolorBrakuAktywnosci
        btnKopiuj.Enabled = False
        btnKopiuj.BackColor = kolorBrakuAktywnosci

        wczytaj()
        bWczytano = True
    End Sub

    Private Function wczytaj() As Boolean

        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik = New wsCursorProf.AdresyKopiujFiltryWczytajWynik

        'odczyt z serwera
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.AdresyKopiujFiltryWczytaj(frmGlowna.sesja)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
            Return False
        End Try
        If wsWynik.status = -1 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, Me.Text)
            Return False
        End If
        'kontrola wyników
        If wsWynik.dane.Tables.Count > 0 Then
            dtUser = wsWynik.dane.Tables(0)
            If wsWynik.dane.Tables(0).Rows.Count < 1 Then
                MsgBox("Brak danych użytkowników wysyłających adresy.", MsgBoxStyle.Exclamation, Me.Text)
                Return False
            End If
        Else
            MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał użytkowników wysyłających adresy." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
            Return False
            Exit Function
        End If


        If wsWynik.dane.Tables.Count > 1 Then
            chbUser.Checked = False
            dtUserDokad = wsWynik.dane.Tables(1)
            If wsWynik.dane.Tables(1).Rows.Count < 1 Then
                MsgBox("Brak danych użytkowników odbierających adresy.", MsgBoxStyle.Exclamation, Me.Text)
                Return False
            End If
        Else
            MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał użytkowników odbierających adresy." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
            Return False
            Exit Function
        End If

        cmbUser.DataSource = dtUser
        cmbUser.DisplayMember = "ImieiNazwisko"
        cmbUser.ValueMember = "USER_ID"
        cmbUser.SelectedValue = -1

        nazwaSzablonu = wsWynik.nazwaSzablonu

        dgvUserDokad.DataSource = dtUserDokad
        If dgvUserDokad.Columns.Contains("USER_ID") Then
            dgvUserDokad.Columns("USER_ID").Visible = False
        End If

        lblLicznikUser.Text = "Licznik zaznaczonych użytkowników do których mają byc skopiowane adresy: " & licznikZaznaczonychUser
        Return True
    End Function

    Private Sub rbUser_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbUser.CheckedChanged
        If rbUser.Checked = True Then
            cmbUser.Enabled = True
            For Each row As DataGridViewRow In dgvUserDokad.Rows
                If row.Cells(0).Value = cmbUser.SelectedValue Then
                    row.Visible = False
                Else
                    row.Visible = True
                End If
            Next
        Else
            cmbUser.Enabled = False
        End If
        filtrUserDokad()
    End Sub

    Private Sub rbExcel_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbExcel.CheckedChanged
        chbAdresy.Checked = False
        dgvAdresy.DataSource = Nothing
        If rbExcel.Checked = True Then
            txtSciezkaExcel.Enabled = True
            btnSciezkaExcel.Enabled = True
            btnSciezkaExcel.BackColor = kolorAktywnosci
            btnPobierzSzablon.Enabled = True
            btnPobierzSzablon.BackColor = kolorAktywnosci
        Else
            txtSciezkaExcel.Enabled = False
            btnSciezkaExcel.Enabled = False
            btnSciezkaExcel.BackColor = kolorBrakuAktywnosci
            btnPobierzSzablon.Enabled = False
            btnPobierzSzablon.BackColor = kolorBrakuAktywnosci
        End If

        filtrUserDokad()
    End Sub

    Private Sub btnPobierz_Click(sender As Object, e As System.EventArgs) Handles btnPobierzSzablon.Click
        PobierzPlik(nazwaSzablonu)
    End Sub

    Private Sub dgvUserDokad_CellContentClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvUserDokad.CellContentClick
        licznikZaznaczonychUser = 0

        If dgvUserDokad.Columns(e.ColumnIndex).Name = "Wybierz" Then
            dgvUserDokad.EndEdit()
            dgvUserDokad.RefreshEdit()
            For Each row As DataGridViewRow In dgvUserDokad.Rows
                If row.Cells(1).Value = True Then
                    licznikZaznaczonychUser += 1
                End If
            Next

        End If

        lblLicznikUser.Text = "Licznik zaznaczonych użytkowników do których mają byc skopiowane adresy: " & licznikZaznaczonychUser
    End Sub

    Private Sub chbUser_CheckedChanged(sender As Object, e As System.EventArgs) Handles chbUser.CheckedChanged
        licznikZaznaczonychUser = 0
        For Each row As DataGridViewRow In dgvUserDokad.Rows
            If chbUser.Checked = True Then
                row.Cells("Wybierz").Value = True
                btnKopiuj.BackColor = kolorAktywnosci
                btnKopiuj.Enabled = True
            Else
                row.Cells("Wybierz").Value = False
                btnKopiuj.BackColor = kolorBrakuAktywnosci
                btnKopiuj.Enabled = False
            End If

            If row.Cells("Wybierz").Value = True Then
                licznikZaznaczonychUser += 1
            End If
        Next

        lblLicznikUser.Text = "Licznik zaznaczonych użytkowników do których mają byc skopiowane adresy: " & licznikZaznaczonychUser
    End Sub

    Private Sub btnSciezkaExcel_Click(sender As System.Object, e As System.EventArgs) Handles btnSciezkaExcel.Click

        Dim ofdPlikXLS As New OpenFileDialog
        ofdPlikXLS.Filter = "Pliki Programu Excel (*.xlsx)|*.xlsx"

        If ofdPlikXLS.ShowDialog = Windows.Forms.DialogResult.OK Then
            sciezka = ofdPlikXLS.FileName
            txtSciezkaExcel.Text = sciezka
        End If
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

    Function ImportDataRecords(file As FileInfo) As ImportResult
        Using excel As New ExcelPackage(file)
            If Not file.Name.EndsWith("xlsx") Then
                Return ImportResult.NotExcel
            End If
            Dim worksheet = excel.Workbook.Worksheets(1)
            If IsDBNull(worksheet) = True Then
                Return ImportResult.Wrongformat
            End If
            If Not worksheet.Cells("A3").Value.Equals("Nazwa") Or
                worksheet.Cells("B3").Value.Equals("Adres") Or worksheet.Cells("D3").Value.Equals("Miasto") Or
                worksheet.Cells("E3").Value.Equals("Telefon") Or worksheet.Cells("F3").Value.Equals("Email") Or
                worksheet.Cells("C3").Value.Equals("Kod") Or worksheet.Cells("G3").Value.Equals("OsobaKontaktowa") Then


                Return ImportResult.Wrongformat
            End If
            Dim lastRow = worksheet.Dimension.End.Row
        End Using
        Return ImportResult.Success
    End Function

    Private Sub przygotujTabele()
        ' Jeśli dtZamowienia ma już wiersze to usuwamy je
        If dtAdres.Rows.Count > 0 Then
            dtAdres.Rows.Clear()
        End If

        '' Jeśli dtZamowienia ma już kolumny to usuwamy je
        If dtAdres.Columns.Count > 0 Then
            dtAdres.Columns.Clear()
        End If

        dtAdres.Columns.Add(EnumPodstawoweNaglowki.ID.ToString)
        dtAdres.Columns.Add(EnumPodstawoweNaglowki.Wybierz.ToString, GetType(Boolean))
        dtAdres.Columns.Add(EnumPodstawoweNaglowki.nazwa.ToString)
        dtAdres.Columns.Add(EnumPodstawoweNaglowki.adres.ToString)
        dtAdres.Columns.Add(EnumPodstawoweNaglowki.kod.ToString)
        dtAdres.Columns.Add(EnumPodstawoweNaglowki.miasto.ToString)
        dtAdres.Columns.Add(EnumPodstawoweNaglowki.telefon.ToString)
        dtAdres.Columns.Add(EnumPodstawoweNaglowki.eMail.ToString)
        dtAdres.Columns.Add(EnumPodstawoweNaglowki.osobaKontaktowa.ToString)
        dtAdres.Columns.Add(EnumPodstawoweNaglowki.status.ToString)
    End Sub

    Private Shared Function ValidateEmail(ByVal mail As String) As Boolean
        Dim re As New Regex("\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", RegexOptions.IgnoreCase)
        If mail IsNot Nothing AndAlso mail.Length > 0 AndAlso Not re.IsMatch(mail) Then
            Return False
        End If

        Return True
    End Function


    Private Sub btnPobierzAdresy_Click(sender As Object, e As System.EventArgs) Handles btnPobierzAdresy.Click
        liczbaAdresow = 0
        licznikZaznaczonychAdresow = 0


        If rbExcel.Checked = True Then
            ustawAdresyZExcela()
        Else
            ustawAdresyZUsera(cmbUser.SelectedValue)
        End If
    End Sub

    Private Sub ustawAdresyZExcela()
        Try

            Dim newFile As New FileInfo(sciezka)

            Using pck As New ExcelPackage(newFile)

                nazwa_pliku_XLS = sciezka
                ImportDataRecords(newFile)

                dtAdres = New DataTable

                Dim wsDane As ExcelWorksheet = pck.Workbook.Worksheets(1)
                Dim intIndexWiersza As Integer = 0
                Dim intIndexKolumny As Integer = 0
                Dim kod_poczt As String = ""


                Cursor = Cursors.WaitCursor
                przygotujTabele()
                Do Until CStr(wsDane.Cells("A3").Offset(intIndexWiersza + intOdlegloscOdNaglowka, 0).Value) & _
                     CStr(wsDane.Cells("B3").Offset(intIndexWiersza + intOdlegloscOdNaglowka, 0).Value) & _
                     CStr(wsDane.Cells("C3").Offset(intIndexWiersza + intOdlegloscOdNaglowka, 0).Value) & _
                     CStr(wsDane.Cells("D3").Offset(intIndexWiersza + intOdlegloscOdNaglowka, 0).Value) & _
                     CStr(wsDane.Cells("E3").Offset(intIndexWiersza + intOdlegloscOdNaglowka, 0).Value) & _
                      CStr(wsDane.Cells("F3").Offset(intIndexWiersza + intOdlegloscOdNaglowka, 0).Value) & _
                     CStr(wsDane.Cells("G3").Offset(intIndexWiersza + intOdlegloscOdNaglowka, 0).Value) = ""
                    dtAdres.Rows.Add()
                    bCzyMoznaKopiowac = True
                    Dim status As String = "Błąd: "
                    For intIndexKolumny = 0 To dtAdres.Columns.Count - 1
                        Select Case dtAdres.Columns(intIndexKolumny).ColumnName

                            Case EnumPodstawoweNaglowki.ID.ToString
                                dtAdres.Rows(intIndexWiersza).Item(intIndexKolumny) = intIndexWiersza + 1

                            Case EnumPodstawoweNaglowki.Wybierz.ToString
                                dtAdres.Rows(intIndexWiersza).Item(intIndexKolumny) = True
                            Case EnumPodstawoweNaglowki.Nazwa.ToString
                                If Trim(CStr(wsDane.Cells("A3").Offset(intIndexWiersza + intOdlegloscOdNaglowka, 0).Value)) = String.Empty Then
                                    status = status + BrakNazwy
                                End If
                                dtAdres.Rows(intIndexWiersza).Item(intIndexKolumny) = wsDane.Cells("A3").Offset(intIndexWiersza + intOdlegloscOdNaglowka, 0).Value

                            Case EnumPodstawoweNaglowki.Adres.ToString
                                If Trim(CStr(wsDane.Cells("B3").Offset(intIndexWiersza + intOdlegloscOdNaglowka, 0).Value)) = String.Empty Then
                                    status = status & BrakAdresu & " , "
                                End If
                                dtAdres.Rows(intIndexWiersza).Item(intIndexKolumny) = wsDane.Cells("B3").Offset(intIndexWiersza + intOdlegloscOdNaglowka, 0).Value

                            Case EnumPodstawoweNaglowki.Kod.ToString
                                'sprawdzamy, czy kod pocztowy jest poprawny poprzez regular expression
                                Dim reg_exp As New Regex("^([0-9]{5})$")
                                If Trim(CStr(wsDane.Cells("C3").Offset(intIndexWiersza + intOdlegloscOdNaglowka, 0).Value)) = String.Empty Then

                                    status = status & KodMaNiepoprawnaStrukture & ", "

                                ElseIf Not reg_exp.IsMatch(CStr(wsDane.Cells("C3").Offset(intIndexWiersza + intOdlegloscOdNaglowka, 0).Value).Replace("-", "").Trim) Then
                                    '' mamy niepoprawny kod pocztowy
                                    status = status & KodMaNiepoprawnaStrukture & ", "

                                End If
                                kod_poczt = CStr(wsDane.Cells("C3").Offset(intIndexWiersza + intOdlegloscOdNaglowka, 0).Value).Replace("-", "").Trim
                                kod_poczt = Mid(kod_poczt, 1, 2) & "-" & Mid(kod_poczt, 3, 3)
                                dtAdres.Rows(intIndexWiersza).Item(intIndexKolumny) = kod_poczt '' wsDane.Cells("H9").Offset(i, 0).Value
                                dtAdres.Rows(intIndexWiersza).Item(intIndexKolumny) = wsDane.Cells("C3").Offset(intIndexWiersza + intOdlegloscOdNaglowka, 0).Value
                            Case EnumPodstawoweNaglowki.Miasto.ToString

                                If Trim(CStr(wsDane.Cells("D3").Offset(intIndexWiersza, 0).Value)) = String.Empty Then

                                    status = status & BrakMiasta & ", "

                                End If
                                dtAdres.Rows(intIndexWiersza).Item(intIndexKolumny) = wsDane.Cells("D3").Offset(intIndexWiersza + intOdlegloscOdNaglowka, 0).Value

                            Case EnumPodstawoweNaglowki.Telefon.ToString
                                Dim telefon As String = String.Empty
                                telefon = CStr(wsDane.Cells("E3").Offset(intIndexWiersza + intOdlegloscOdNaglowka, 0).Value)

                                If Trim(telefon) = String.Empty Or telefon.Length <> 9 Then
                                    status = status & Telefon9znakow & ", "

                                End If

                                dtAdres.Rows(intIndexWiersza).Item(intIndexKolumny) = wsDane.Cells("E3").Offset(intIndexWiersza + intOdlegloscOdNaglowka, 0).Value

                            Case EnumPodstawoweNaglowki.Email.ToString
                                Dim mail As String = String.Empty
                                mail = CStr(wsDane.Cells("F3").Offset(intIndexWiersza + intOdlegloscOdNaglowka, 0).Value)
                                If Trim(mail) = String.Empty Or ValidateEmail(mail) = False Then
                                    status = status & EmailStruktura & ", "

                                End If
                                dtAdres.Rows(intIndexWiersza).Item(intIndexKolumny) = wsDane.Cells("F3").Offset(intIndexWiersza + intOdlegloscOdNaglowka, 0).Value

                            Case EnumPodstawoweNaglowki.OsobaKontaktowa.ToString
                                If Trim(CStr(wsDane.Cells("G3").Offset(intIndexWiersza + intOdlegloscOdNaglowka, 0).Value)) = String.Empty Then
                                    status = status & BrakOsobyKontaktowej & ", "
                                End If
                                dtAdres.Rows(intIndexWiersza).Item(intIndexKolumny) = wsDane.Cells("G3").Offset(intIndexWiersza + intOdlegloscOdNaglowka, 0).Value

                            Case EnumPodstawoweNaglowki.status.ToString
                                If status <> "Błąd: " Then
                                    If status.EndsWith(", ") Then
                                        dtAdres.Rows(intIndexWiersza).Item(intIndexKolumny) = status.Substring(0, status.Length - 2)
                                    Else
                                        dtAdres.Rows(intIndexWiersza).Item(intIndexKolumny) = status
                                    End If
                                Else
                                    dtAdres.Rows(intIndexWiersza).Item(intIndexKolumny) = Ok

                                End If
                        End Select
                    Next
                    intIndexWiersza = intIndexWiersza + 1

                Loop
            End Using
            Dim newColumn As DataGridViewColumn
            Dim xmlKody As New StringBuilder

            For Each row As DataRow In dtAdres.Rows
                xmlKody.Append("<row ID=""" & row.Item("ID") & """ KOD=""" & row.Item("KOD") & """ MIASTO=""" & row.Item("MIASTO") & """/>")
            Next

            Dim dtAdresyZlyKod As New DataTable

            dtAdresyZlyKod = sprawdzKodyPocztowe(xmlKody.ToString)

            For Each row As DataRow In dtAdres.Rows
                If dtAdresyZlyKod.Select("ID=" & row.Item("ID")).Length > 0 Then
                    If row.Item("status") <> "OK" Then
                        row.Item("status") = row.Item("status") + ", Nie istnieje taki kod pocztowy"
                    Else
                        row.Item("status") = "Nie istnieje taki kod pocztowy"
                    End If

                End If
            Next

            For Each row As DataRow In dtAdres.Rows
                If row.Item("status") <> "OK" Then
                    row.Item("Wybierz") = False
                End If
            Next

            If dtAdres.Rows.Count < 1 Then
                MsgBox("Nie pobrano tabeli adresów", MsgBoxStyle.Exclamation, Me.Text)
                btnExcel.Enabled = False
                btnExcel.BackColor = kolorBrakuAktywnosci
                btnKopiuj.Enabled = False
                btnKopiuj.BackColor = kolorBrakuAktywnosci
            Else
                dgvAdresy.DataSource = dtAdres
                If dgvAdresy.Columns.Contains("ID") Then dgvAdresy.Columns("ID").Visible = False
                newColumn = dgvAdresy.Columns(9)
                dgvAdresy.Sort(newColumn, System.ComponentModel.ListSortDirection.Ascending)
                btnExcel.Enabled = True
                btnExcel.BackColor = kolorAktywnosci
                btnKopiuj.Enabled = True
                btnKopiuj.BackColor = kolorAktywnosci
            End If

            For Each col As DataGridViewColumn In dgvAdresy.Columns
                If col.Name <> "Wybierz" Then
                    col.ReadOnly = True
                End If
            Next

            For Each row As DataGridViewRow In dgvAdresy.Rows
                If row.Cells("status").Value = "OK" Then
                    row.Cells("status").Style.ForeColor = Color.Green
                    licznikZaznaczonychAdresow = licznikZaznaczonychAdresow + 1
                Else
                    row.Cells("Wybierz").ReadOnly = True
                    'row.Cells("Wybierz").Value = False
                    row.Cells("status").Style.ForeColor = Color.Red
                End If
                liczbaAdresow = liczbaAdresow + 1
            Next

            Dim iloscZaznacoznychUserow As Integer = 0

            For Each row As DataGridViewRow In dgvUserDokad.Rows
                If row.Cells("Wybierz").Value = True Then
                    iloscZaznacoznychUserow += 1
                End If
            Next

            If iloscZaznacoznychUserow = 0 Then
                btnKopiuj.Enabled = False
                btnKopiuj.BackColor = kolorBrakuAktywnosci
            Else
                btnKopiuj.Enabled = True
                btnKopiuj.BackColor = kolorAktywnosci
            End If

            lblLicznik.Text = "Liczba zaznaczonych adresów do skopiowania: " & licznikZaznaczonychAdresow
            lblLicznik.Visible = True
            If licznikZaznaczonychAdresow > 0 Then
                chbAdresy.Checked = True
            End If

            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            If ex.Message.Contains("because it is being used by another process") Then
                MsgBox("Plik o nazwie " & sciezka & _
                       " jest otwarty! Proszę zamknąć ten plik i spróbowac ponownie go wczytać.", _
                       MsgBoxStyle.Exclamation)
            Else
                MsgBox("Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, "Wystąpił wyjątek")
            End If
            Exit Sub
        End Try
    End Sub

    Private Sub ustawAdresyZUsera(ByVal userId As Integer)
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik = New wsCursorProf.AdresKopiujUserWczytajWynik

        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.AdresKopiujUserWczytaj(frmGlowna.sesja, userId)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical)
        End Try
        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, "Brak niektórych SKU")

        End If

        iloscDubli = wsWynik.iloscZdublowanych

        If wsWynik.dane.Tables.Count < 1 Then
            MsgBox("Nie pobrano tabeli adresów", MsgBoxStyle.Critical, Me.Text)
            btnExcel.Enabled = False
            btnExcel.BackColor = kolorBrakuAktywnosci
            btnKopiuj.Enabled = False
            btnKopiuj.BackColor = kolorBrakuAktywnosci
        Else
            dgvAdresy.DataSource = wsWynik.dane.Tables(0)
            For Each row As DataGridViewRow In dgvAdresy.Rows
                If row.Cells("Wybierz").Value = True Then
                    licznikZaznaczonychAdresow = licznikZaznaczonychAdresow + 1
                End If
            Next
            btnExcel.Enabled = True
            btnExcel.BackColor = kolorAktywnosci
            btnKopiuj.Enabled = True
            btnKopiuj.BackColor = kolorAktywnosci
            lblLicznik.Text = "Liczba zaznaczonych adresów do skopiowania: " & licznikZaznaczonychAdresow
            lblLicznik.Visible = True
        End If
        Dim iloscZaznacoznychUserow As Integer = 0

        For Each row As DataGridViewRow In dgvUserDokad.Rows
            If row.Cells("Wybierz").Value = True Then
                iloscZaznacoznychUserow += 1
            End If
        Next

        If iloscZaznacoznychUserow = 0 Then
            btnKopiuj.Enabled = False
            btnKopiuj.BackColor = kolorBrakuAktywnosci
        Else
            btnKopiuj.Enabled = True
            btnKopiuj.BackColor = kolorAktywnosci
        End If

        If iloscDubli > 0 Then
            MsgBox("Usunięto zduplikowanych adresów: " & iloscDubli, MsgBoxStyle.Information, Me.Text)
        End If
        If licznikZaznaczonychAdresow > 0 Then
            chbAdresy.Checked = True
        End If
    End Sub

    Private Sub PobierzTabeleZdefiniowana()
        dtAdresyDoSkopiowania = Nothing
        dtAdresyDoSkopiowania = getDtTvpAdesyDoSkopiowania()


        If dgvAdresy.Rows.Count > 0 Then
            For i As Integer = 0 To listaUserDokad.Count - 1
                For Each row As DataGridViewRow In dgvAdresy.Rows
                    Dim dr As DataRow
                    dr = dtAdresyDoSkopiowania.NewRow()
                    dr(0) = row.Cells(Nazwa).Value
                    dr(1) = row.Cells(Adres).Value
                    dr(2) = row.Cells(Kod).Value
                    dr(3) = row.Cells(Miasto).Value
                    dr(4) = row.Cells(TelKom).Value
                    dr(5) = row.Cells(Email).Value
                    dr(6) = row.Cells(OsobaKon).Value
                    dr(7) = listaUserDokad.Item(i)
                    If row.Cells(Wybierz).Value = True Then
                        dtAdresyDoSkopiowania.Rows.Add(dr)
                    End If
                Next
            Next

        End If
    End Sub

    Private Sub PobierzTabeleDoExcela()

        dtAdresyDoExcela = Nothing
        dtAdresyDoExcela = getDtTvpAdesyDoSkopiowania()

        If dgvAdresy.Rows.Count > 0 Then
            For Each row As DataGridViewRow In dgvAdresy.Rows
                Dim dr As DataRow
                dr = dtAdresyDoExcela.NewRow()
                dr(0) = row.Cells(Nazwa).Value
                dr(1) = row.Cells(Adres).Value
                dr(2) = row.Cells(Kod).Value
                dr(3) = row.Cells(Miasto).Value
                dr(4) = row.Cells(TelKom).Value
                dr(5) = row.Cells(Email).Value
                dr(6) = row.Cells(OsobaKon).Value
                If rbExcel.Checked = True Then
                    dr(7) = row.Cells(Status).Value
                End If
                dtAdresyDoExcela.Rows.Add(dr)

            Next
        Else
            MsgBox("Nie ma adresów do skopiowania do excela.", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub

    Private Function getDtTvpAdesyDoSkopiowania() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add(enTvpAdesyDoSkopiowania.Nazwa.ToString, GetType(String)).SetOrdinal(0)
        dt.Columns.Add(enTvpAdesyDoSkopiowania.Adres.ToString, GetType(String)).SetOrdinal(1)
        dt.Columns.Add(enTvpAdesyDoSkopiowania.Kod.ToString, GetType(String)).SetOrdinal(2)
        dt.Columns.Add(enTvpAdesyDoSkopiowania.Miasto.ToString, GetType(String)).SetOrdinal(3)
        dt.Columns.Add(enTvpAdesyDoSkopiowania.Telefon.ToString, GetType(String)).SetOrdinal(4)
        dt.Columns.Add(enTvpAdesyDoSkopiowania.Email.ToString, GetType(String)).SetOrdinal(5)
        dt.Columns.Add(enTvpAdesyDoSkopiowania.OsobaKontaktowa.ToString, GetType(String)).SetOrdinal(6)
        If bCzyDoExcela = False Then
            dt.Columns.Add(enTvpAdesyDoSkopiowania.user_id_dokad.ToString, GetType(Integer)).SetOrdinal(7)
        Else
            If rbExcel.Checked = True Then
                dt.Columns.Add(enTvpAdesyDoSkopiowania.status.ToString, GetType(String)).SetOrdinal(7)
            End If
        End If

        dt.Columns(enTvpAdesyDoSkopiowania.Adres.ToString).MaxLength = 255

        Return dt
    End Function

    Private Sub btnKopiuj_Click(sender As Object, e As System.EventArgs) Handles btnKopiuj.Click
        listaUserDokad.Clear()
        For Each row As DataGridViewRow In dgvUserDokad.Rows
            If row.Cells(1).Value = True Then
                listaUserDokad.Add(row.Cells(0).Value)
            End If
        Next
        Dim ds As New DataSet

        PobierzTabeleZdefiniowana()
        ds.Tables.Add(dtAdresyDoSkopiowania)

        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik = New wsCursorProf.AdresySkopiowaneZapiszWynik

        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.AdresySkopiowaneZapisz(frmGlowna.sesja, ds)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical)

        End Try

        liczbaAdresowSkopiowanych = wsWynik.iloscAdresowSkopiowanych
        liczbaDubliPrzyKopiowaniu = wsWynik.iloscDubli

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox("Liczba adresów wybranych do kopiowania: " & liczbaAdresowSkopiowanych + liczbaDubliPrzyKopiowaniu & ". Skopiowano: " & liczbaAdresowSkopiowanych _
                   & ". Ilośc dubli: " & liczbaDubliPrzyKopiowaniu & ".", MsgBoxStyle.Information, "Podsumowanie kopiowania")
        End If

        dtAdresyDoSkopiowania.Clear()
    End Sub

    Private Sub chbAdresy_CheckedChanged(sender As Object, e As System.EventArgs) Handles chbAdresy.CheckedChanged
        licznikZaznaczonychAdresow = 0

        For Each row As DataGridViewRow In dgvAdresy.Rows
            If chbAdresy.Checked = True AndAlso row.Cells("Wybierz").ReadOnly = False Then
                row.Cells("Wybierz").Value = True
            Else
                row.Cells("Wybierz").Value = False
            End If

            If row.Cells("Wybierz").Value = True Then
                licznikZaznaczonychAdresow = licznikZaznaczonychAdresow + 1
            End If
        Next

        lblLicznik.Text = "Liczba zaznaczonych adresów do skopiowania: " & licznikZaznaczonychAdresow

    End Sub



    Private Sub DumpExcel(ByVal tbl As DataTable, ByVal filePath As String, ByVal tytul As String)

        Try
            If File.Exists(filePath) Then
                File.Delete(filePath)
            End If
            Dim newFile As New FileInfo(filePath)

            Using pck As New ExcelPackage(newFile)

                'Create the worksheet

                pck.Workbook.Properties.Author = ""
                pck.Workbook.Properties.Title = tytul
                pck.Workbook.Properties.Company = "OEX E-Business"

                Dim ws As ExcelWorksheet = pck.Workbook.Worksheets.Add("Adresy")
                Dim ile_kolumn As Integer = tbl.Columns.Count
                Dim i As Integer = 1

                'Load the datatable into the sheet, starting from cell A1. Print the column names on row 1
                ws.Cells("A1").Value = tytul
                ws.Cells("A1").Style.Font.Bold = True
                ws.Cells("A1").Style.Font.Size = 14
                ws.Cells("A3").LoadFromDataTable(tbl, True)

                For i = 0 To ile_kolumn - 1
                    If i = 0 Then
                        ws.Column(i + 1).Width = 18
                    Else
                        ws.Column(i + 1).AutoFit()
                    End If
                Next

                With ws.Cells(3, 1, 3, ile_kolumn).Style
                    .Fill.PatternType = ExcelFillStyle.Solid
                    .Fill.BackgroundColor.SetColor(Color.SteelBlue)
                    .VerticalAlignment = ExcelVerticalAlignment.Center
                    .HorizontalAlignment = ExcelHorizontalAlignment.Center
                    .Font.Color.SetColor(Color.White)
                    .Font.Bold = True
                End With

                ' zapisanie wyniku do pliku 

                pck.Save()
                MsgBox("Zapisano raport: " & filePath, MsgBoxStyle.Information, "Adresy")

            End Using

        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem. " & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, "Adresy")
            Exit Sub
        End Try
    End Sub

    Private Sub btnExcel_Click(sender As Object, e As System.EventArgs) Handles btnExcel.Click
        bCzyDoExcela = True
        dtAdresyDoExcela.Clear()

        Dim sfd As New SaveFileDialog
        sfd.Filter = "Skoroszyt programu Excel|*.xlsx"
        sfd.FileName = "Adresy"
        If sfd.ShowDialog = Windows.Forms.DialogResult.OK Then
            PobierzTabeleDoExcela()
            DumpExcel(dtAdresyDoExcela, sfd.FileName, "Adresy")
        End If
        bCzyDoExcela = False
    End Sub

    Private Sub dgvAdresy_CellContentClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvAdresy.CellContentClick

        If dgvAdresy.Columns(e.ColumnIndex).Name = "Wybierz" Then
            licznikZaznaczonychAdresow = 0
            dgvAdresy.EndEdit()
            dgvAdresy.RefreshEdit()
            For Each row As DataGridViewRow In dgvAdresy.Rows
                If row.Cells("Wybierz").Value = True Then
                    licznikZaznaczonychAdresow = licznikZaznaczonychAdresow + 1
                End If
            Next
        End If

        lblLicznik.Text = "Liczba zaznaczonych adresów do skopiowania: " & licznikZaznaczonychAdresow
    End Sub

    Private Function sprawdzKodyPocztowe(xmlKody As String) As DataTable
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik = New wsCursorProf.KodyPocztoweWieleSprawdzWynik

        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.KodyPocztoweWieleSprawdz(frmGlowna.sesja, xmlKody)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical)

        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, Me.Text)
        End If

        Dim dt As New DataTable
        If wsWynik.dane.Tables.Count < 1 Then
            MsgBox("Serwer nie wysłał tabeli z niepoprawnymi kodami pocztowymi", MsgBoxStyle.Critical, Me.Text)
        Else
            dt = wsWynik.dane.Tables(0)
        End If

        Return dt
    End Function

    Private Sub cmbUser_SelectedValueChanged(sender As Object, e As System.EventArgs) Handles cmbUser.SelectedValueChanged
        btnPobierzAdresy.Enabled = True
        btnPobierzAdresy.BackColor = kolorAktywnosci
        chbAdresy.Checked = False
        dgvAdresy.DataSource = Nothing
        filtrUserDokad()
    End Sub

    Private Sub filtrUserDokad()
        licznikZaznaczonychAdresow = 0
        licznikZaznaczonychUser = 0

        If bWczytano = True Then
            dvUserDokad = dtUserDokad.DefaultView
            dvUserDokad.RowFilter = "Imię LIKE '%" & txtUserFiltr.Text & "%' or Nazwisko LIKE '%" & txtUserFiltr.Text & "%' or Login LIKE '%" & txtUserFiltr.Text & "%' or Grupa LIKE '%" & txtUserFiltr.Text & "%'"
            dgvUserDokad.DataSource = dvUserDokad
        End If
        For Each row As DataGridViewRow In dgvUserDokad.Rows
                If rbUser.Checked = True AndAlso row.Cells(0).Value = cmbUser.SelectedValue Then
                    dgvUserDokad.CurrentCell = Nothing
                    row.Visible = False
                Else
                    row.Visible = True
                End If

            If row.Cells("Wybierz").Value = True Then
                licznikZaznaczonychUser += 1
            End If
        Next
        lblLicznik.Visible = False
    End Sub

    Private Sub btnUserFiltr_Click(sender As System.Object, e As System.EventArgs) Handles btnUserFiltr.Click
        filtrUserDokad()
    End Sub

    Private Sub lblLicznik_TextChanged(sender As Object, e As System.EventArgs) Handles lblLicznik.TextChanged
        If licznikZaznaczonychAdresow > 0 And licznikZaznaczonychUser > 0 Then
            btnKopiuj.Enabled = True
            btnKopiuj.BackColor = kolorAktywnosci
        Else
            btnKopiuj.Enabled = False
            btnKopiuj.BackColor = kolorBrakuAktywnosci
        End If

        If licznikZaznaczonychAdresow = 0 Then
            chbAdresy.Checked = False
        End If
    End Sub

    Private Sub lblLicznikUser_TextChanged(sender As Object, e As System.EventArgs) Handles lblLicznikUser.TextChanged
        If licznikZaznaczonychAdresow > 0 And licznikZaznaczonychUser > 0 Then
            btnKopiuj.Enabled = True
            btnKopiuj.BackColor = kolorAktywnosci
        Else
            btnKopiuj.Enabled = False
            btnKopiuj.BackColor = kolorBrakuAktywnosci
        End If

    End Sub

    Private Sub btnZamknij_Click(sender As Object, e As System.EventArgs) Handles btnZamknij.Click
        Me.Close()
    End Sub
End Class