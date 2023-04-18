Imports System.Reflection
Imports System
Imports System.IO
Imports OfficeOpenXml
Imports OfficeOpenXml.Style
Imports OfficeOpenXml.Utils


Public Enum frmZamowienieINVTrybPracy
    edycjaWlasnegoKoszyka = 1
    podgladObcegoKoszyka = 2
    podgladZamowienia = 3
End Enum

Public Enum frmZamowienieINVTypZamowienia
    inwentaryzacja_na_plus = 12
    inwentaryzacja_na_minus = 13
End Enum
Public Class frmZamowienieINV

    Public frmRodzic As Form
    Public strFunkcjaPowiadomieniaOGotowosci As String = Nothing 'jeśli zmienna ustawiona, to znaczy że mamy notyfikować okno rodzica o zakończeniu przygotowywania ekranu
    Public intIdZamowienia As Integer = -1 'tylko do odczytu: przekazane z formy rodzica; jeśli =-1 to znaczy koszyk
    Private intIdBlokady As Integer = -1 'tylko do odczytu: id blokady wygenerowane w bazie na potrzeby edycji tego rekordu; -1 oznacza brak blokady
    Private strUserNazwa As String 'nazwa użytkownika do którego należy zamówienie
    Private enumTrybPracy As frmZamowienieINVTrybPracy 'tylko do odczytu: świadczy o tym, jak rysować ekran
    Private bCentrala As Boolean 'tylko do odczytu: czy użytkownik jest członkiem grupy centrala?
    Private dtTypyZamowienINV As DataTable 'magazyny do pokazania w ComboBox
    Private dtOddzialy As DataTable 'magazyny do pokazania w ComboBox
    Private dtAdresy As DataTable 'adresy zdefiniowane do pokazania w ComboBox
    Private enumTypZamowienia As frmZamowienieINVTypZamowienia 'rodzaj zamówienia (odbiór własny, dostawa, itp.)
    Private bReagujNaZmianyComboMagazyn As Boolean = True
    Private intIdOstatnioWybranegoMagazynu As Integer = -1
    'kopia zmiennych z momentu wczytania z bazy
    Public intIdMagazynu As Integer = -1
    Private enumTypZamowieniaKopia As frmZamowienieINVTypZamowienia
    Private intIdMagazynuDocelowego As Integer = -1
    Private intIdOddzialuDocelowego As Integer = -1
    Private intIdAdresuZdefiniowanego As Integer = -1
    Private strNazwa As String
    Private strAdres As String
    Private strKod As String
    Private strMiasto As String
    Private dtAdresyGrupowe As DataTable
    Private dtAdresyGrupoweKopia As DataTable
    Private strOsobaKontaktowa As String
    Private strTelefonKontaktowy As String
    Private strUwagi As String
    Private dataRealizacji As DateTime
    Private kosztDostawy As Decimal
    Public dtPozycjeZamowienia As DataTable 'pozycje zamówienia użytkownika w momencie otwarcia okna
    Public BlokujZapisz As Boolean = False
    Public idMagazyn As Integer = 1
    Private users_ids As String
    Private warunek_grupowy As String = ""
    Private grupy As String = String.Empty
    Private typy As String = String.Empty
    Private wielkosc As String = String.Empty
    Private strusers_ids As String
    Private strgrupy As String
    Private strtypy As String
    Private strwielkosc As String
    Public kolor As Color = Color.DodgerBlue
    Public CzyUserOddzial As Boolean = False
    Private intUwagiPozostaloZnakow As Integer = 170
    Private nazwaSzablonu As String = "Szablon - import pozycji do zamówienia INV.xlsx"

    Private Sub wylaczKontrolki()
        btnImportPozycjiExcel.Enabled = False
        btnDodajPozycje.Enabled = False
        btnUsunPozycje.Enabled = False
        cmbTypZamowienia.Enabled = False
        txtUwagi.Enabled = False
        btnZlozZamowienie.Enabled = False
        dgv.ReadOnly = True
    End Sub

    Private Sub frmZamowienie_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If BlokujZapisz = False Then
            If bylyZmiany() Then
                Dim odp As MsgBoxResult = MsgBox("Od ostatniego odczytu z serwera wprowadzono zmiany w tym zamówieniu. Czy zapisać wprowadzone zmiany?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNoCancel + MsgBoxStyle.DefaultButton1, "Zapisanie zmian")
                If odp = MsgBoxResult.Yes Then
                    If Not zapiszZmiany() Then
                        e.Cancel = True
                        Exit Sub
                    End If
                ElseIf odp = MsgBoxResult.Cancel Then
                    e.Cancel = True
                    Exit Sub
                End If
            End If

            If intIdBlokady >= 0 Then
                'zwalniamy blokadę rekordu
                System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
                System.Net.ServicePointManager.Expect100Continue = False
                ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
                ws.Proxy.Credentials = CredentialCache.DefaultCredentials
                'ws.Url = frmGlowna.strWebservice
                Try
                    ws.ZamowienieEdytujAnuluj(frmGlowna.sesja, intIdBlokady)
                Catch ex As Exception
                End Try
            End If
            If Me.Equals(frmGlowna.frmKoszykINV) Then frmGlowna.frmKoszykINV = Nothing
        End If
    End Sub

    Private Sub frmZamowienie_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

        If Not wczytaj() Then
            BlokujZapisz = True
            Me.Close()
            Exit Sub
        End If

        If enumTrybPracy = frmZamowienieINVTrybPracy.edycjaWlasnegoKoszyka Then
            If frmGlowna.frmKoszykINV Is Nothing Then
                frmGlowna.frmKoszykINV = Me
            Else
                'coś nie tak - jest już otwarte inne okno koszyka, a my właśnie otworzyliśmy koszyk po raz kolejny - tak miało nie być!
                'pokażmy tamto okno i zamknijmy te
                If frmGlowna.frmKoszykINV.WindowState = FormWindowState.Minimized Then frmGlowna.frmKoszykINV.WindowState = FormWindowState.Normal
                frmGlowna.frmKoszykINV.Activate()
                Me.Close()
                Exit Sub
            End If
        End If

        'czy mamy powiadomnić ekran stan, że nasz ekran gotowy?
        If Not strFunkcjaPowiadomieniaOGotowosci Is Nothing Then
            Dim m As MethodInfo() = frmRodzic.GetType.GetMethods()
            For licznik As Integer = 0 To m.GetUpperBound(0)
                If m(licznik).Name = strFunkcjaPowiadomieniaOGotowosci Then
                    m(licznik).Invoke(frmRodzic, Nothing)
                End If
            Next
        End If

    End Sub

    Private Function wczytaj() As Boolean

        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik As New wsCursorProf.ZamowienieINVWczytajWynik

        intIdOstatnioWybranegoMagazynu = intIdMagazynu 'linia mająca znaczenie przy otwarciu formy

        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.ZamowienieINVWczytaj(frmGlowna.sesja, intIdZamowienia, intIdMagazynu, intIdBlokady)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical)
            Me.Close()
            Exit Function
        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical)
            Return False
        End If
        If wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation)
        End If

        intIdZamowienia = wsWynik.zamowienie_id
        intIdMagazynu = wsWynik.magazyn_id
        intIdOstatnioWybranegoMagazynu = wsWynik.magazyn_id
        intIdBlokady = wsWynik.blokada_id
        strUserNazwa = wsWynik.wlasciciel_nazwa
        lblStatusZamowienia.Text = wsWynik.zamowienie_status
        lblStatusZamowienia.ToolTipText = wsWynik.zamowienie_status_opis

        strUwagi = wsWynik.uwagi
        txtUwagi.Text = strUwagi

        If wsWynik.dane.Tables.Count > 0 Then
            dgv.DataSource = Nothing
            dgv.DataSource = wsWynik.dane.Tables(0)
            dgv.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells)
            If dgv.Columns.Contains("grupa_id") Then
                dgv.Columns("grupa_id").Visible = False
            Else
                MsgBox("Błąd wewnętrzny systemu. Przesłana lista pozycji zamówienia nie zawiera kolumny grupa_id." & frmGlowna.kontaktIt, MsgBoxStyle.Critical)
                Return False
            End If
            If dgv.Columns.Contains("sku_id") Then
                dgv.Columns("sku_id").Visible = False
            Else
                MsgBox("Błąd wewnętrzny systemu. Przesłana lista pozycji zamówienia nie zawiera kolumny sku_id." & frmGlowna.kontaktIt, MsgBoxStyle.Critical)
                Return False
            End If

            'ustawiamy właściwość "tylko do odczytu" na wszystkich kolumnach oprócz ilości
            For Each dgvColumn As DataGridViewColumn In dgv.Columns
                If dgvColumn.HeaderText.ToLower <> "ilosc" Then dgvColumn.ReadOnly = True
            Next
            'zachowujemy kopię pozycji zamówień wczytanych przy otwieraniu okna
            dtPozycjeZamowienia = wsWynik.dane.Tables(0).Copy
        Else
            MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał pozycji zamówienia." & frmGlowna.kontaktIt, MsgBoxStyle.Critical)
            Return False
        End If

        If wsWynik.dane.Tables.Count > 1 Then
            bReagujNaZmianyComboMagazyn = False
            dtTypyZamowienINV = wsWynik.dane.Tables(1).Copy
            cmbTypZamowienia.DataSource = dtTypyZamowienINV
            cmbTypZamowienia.DisplayMember = "NAZWA"
            cmbTypZamowienia.ValueMember = "ZAMOWIENIE_TYP_ID"
            bReagujNaZmianyComboMagazyn = True
        Else
            MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał listy dostępnych magazynów." & frmGlowna.kontaktIt, MsgBoxStyle.Critical)
            Return False
        End If

        'inicjalne ustawienie typu zamówienia

        Select Case wsWynik.tryb_pracy
            Case 1
                enumTrybPracy = frmZamowienieINVTrybPracy.edycjaWlasnegoKoszyka
                Me.Text = "Koszyk - inwentaryzacja +/-"
            Case 2
                enumTrybPracy = frmZamowienieINVTrybPracy.podgladObcegoKoszyka
                Me.Text = "Podgląd koszyka inwentaryzacji użytkownika " & strUserNazwa
                wylaczKontrolki()
            Case 3
                enumTrybPracy = frmZamowienieINVTrybPracy.podgladZamowienia
                Me.Text = "Podgląd zamówienia inwentaryzacji numer " & intIdZamowienia & " użytkownika " & strUserNazwa
                wylaczKontrolki()
            Case Else
                MsgBox("Błąd wewnętrzny systemu. Serwer przesłał nieznany tryb pracy (" & wsWynik.tryb_pracy.ToString & ")." & frmGlowna.kontaktIt, MsgBoxStyle.Critical)
                Return False
        End Select
        'ustawienie kontrolek na podstawie wczytanego typu zamówienia
        Select Case wsWynik.typ_zamowienia
            Case 12
                enumTypZamowieniaKopia = frmZamowienieINVTypZamowienia.inwentaryzacja_na_plus
                enumTypZamowienia = frmZamowienieINVTypZamowienia.inwentaryzacja_na_plus
                cmbTypZamowienia.SelectedValue = wsWynik.typ_zamowienia
            Case 13
                enumTypZamowieniaKopia = frmZamowienieINVTypZamowienia.inwentaryzacja_na_minus
                enumTypZamowienia = frmZamowienieINVTypZamowienia.inwentaryzacja_na_minus
                cmbTypZamowienia.SelectedValue = wsWynik.typ_zamowienia
            Case Else
                MsgBox("Błąd wewnętrzny systemu. Serwer przesłał nieznany typ zamówienia (" & wsWynik.typ_zamowienia.ToString & ")." & frmGlowna.kontaktIt, MsgBoxStyle.Critical)
                Return False
        End Select

        btnZapiszZmiany.Enabled = False
        btnZapiszZmiany.BackColor = Color.LightGray
        Return True
    End Function

    Private Function zapiszZmiany() As Boolean

        If bylyZmiany() Then

            System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
            System.Net.ServicePointManager.Expect100Continue = False
            ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
            ws.Proxy.Credentials = CredentialCache.DefaultCredentials
            Dim wsWynik As New wsCursorProf.KoszykINVZapiszWynik

            Try
                Dim ds As New DataSet
                Dim dataTmp As New DateTime(1, 1, 1)

                ds.Tables.Add(dgv.DataSource.Copy)
                If ds.Tables(0).Columns.Contains("nazwa") Then ds.Tables(0).Columns.Remove(ds.Tables(0).Columns("nazwa"))
                If ds.Tables(0).Columns.Contains("ilosc_dostepna") Then ds.Tables(0).Columns.Remove(ds.Tables(0).Columns("ilosc_dostepna"))
                Cursor = Cursors.WaitCursor
                Application.DoEvents()

                wsWynik = ws.KoszykINVZapisz(frmGlowna.sesja, intIdBlokady, idMagazyn, txtUwagi.Text, enumTypZamowienia, ds)

                Cursor = Cursors.Default
            Catch ex As Exception
                Cursor = Cursors.Default
                MsgBox("Błąd komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical)
                Return False
            End Try

            If wsWynik.status < 0 Then
                MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, "Zapisywanie zamówienia")
                Return False
            ElseIf wsWynik.status > 0 Then
                MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, "Zapisywanie zamówienia")
            End If

            Select Case enumTrybPracy
                Case frmZamowienieINVTrybPracy.edycjaWlasnegoKoszyka
                    frmGlowna.lblStatus.Text = "Koszyk zapisany poprawnie."
                Case frmZamowienieINVTrybPracy.podgladObcegoKoszyka
                    frmGlowna.lblStatus.Text = "Koszyk użytkownika " & strUserNazwa & " zapisany poprawnie."
                Case frmZamowienieINVTrybPracy.podgladZamowienia
                    frmGlowna.lblStatus.Text = "Zamówienie numer " & intIdZamowienia & " użytkownika " & strUserNazwa & " zapisane poprawnie."
            End Select
            frmGlowna.timer.Interval = 3000 'komunikat zniknie po 3s
            frmGlowna.timer.Start()
            btnZapiszZmiany.Enabled = False

            'odświeżamy okno rodzica (jeżeli jego okno prezentuje taką metodę)
            If Not frmRodzic Is Nothing Then
                Dim m As MethodInfo() = frmRodzic.GetType.GetMethods()
                For licznik As Integer = 0 To m.GetUpperBound(0)
                    If m(licznik).Name = "odswiezListy" Then
                        m(licznik).Invoke(frmRodzic, Nothing)
                    End If
                Next
            End If

            'odświeżamy zamówienie z bazy
            wczytaj()
        Else
            Select Case enumTrybPracy
                Case frmZamowienieINVTrybPracy.edycjaWlasnegoKoszyka
                    frmGlowna.lblStatus.Text = "Zawartość koszyka nie uległa zmianie, zapis do bazy nie był konieczny."
                Case frmZamowienieINVTrybPracy.podgladObcegoKoszyka
                    frmGlowna.lblStatus.Text = frmGlowna.lblStatus.Text = "Zawartość koszyka użytkownika " & strUserNazwa & " nie uległa zmianie, zapis do bazy nie był konieczny."
                Case frmZamowienieINVTrybPracy.podgladZamowienia
                    frmGlowna.lblStatus.Text = "Zamówienie numer " & intIdZamowienia & " użytkownika " & strUserNazwa & " nie uległo zmianie, zapis do bazy nie był konieczny."
            End Select
            frmGlowna.timer.Interval = 3000 'komunikat zniknie po 3s
            frmGlowna.timer.Start()
            btnZapiszZmiany.Enabled = False
        End If
        If btnZapiszZmiany.Enabled Then
            btnZapiszZmiany.BackColor = kolor
        Else
            btnZapiszZmiany.BackColor = Color.LightGray
        End If

        ''odświeżamy kontrolkę dgv z formy frmZamowienia
        If Not frmGlowna.frmZamowieniaLista Is Nothing Then
            Dim m As MethodInfo() = frmGlowna.frmZamowieniaLista.GetType.GetMethods()
            For licznik As Integer = 0 To m.GetUpperBound(0)
                If m(licznik).Name = "wczytaj" Then
                    m(licznik).Invoke(frmGlowna.frmZamowieniaLista, Nothing)
                End If
            Next
        End If

        Return True
    End Function

    Private Function bylyZmiany() As Boolean
        Dim bBylyZmiany As Boolean = False
        Dim dtSku As DataTable
        If enumTrybPracy <> frmZamowienieINVTrybPracy.edycjaWlasnegoKoszyka Then
            Return False
        End If
        'czy zmienił się magazyn źródłowy?
        ' IIf(cmbZamowienieZMagazynu.ComboBox.SelectedValue Is Nothing, -1, cmbZamowienieZMagazynu.ComboBox.SelectedValue)
        If intIdMagazynu <> idMagazyn Then
            Return True
        End If

        'zmieniła się ilość pozycji?
        If dgv.Rows.Count <> dtPozycjeZamowienia.Rows.Count Then
            Return True
        End If

        'a może zmieniły się ilości w pozycjach?
        bBylyZmiany = False
        For Each dgvWiersz As DataGridViewRow In dgv.Rows
            dtPozycjeZamowienia.DefaultView.RowFilter = "sku_id=" & dgvWiersz.Cells("sku_id").Value & "AND grupa_id=" & dgvWiersz.Cells("grupa_id").Value
            dtSku = dtPozycjeZamowienia.DefaultView.ToTable
            If dtSku.Rows.Count > 1 Then
                MsgBox("Błąd wewnętrzny aplikacji. Dla SKU " & dgvWiersz.Cells("sku").Value & " i grupy " & dgvWiersz.Cells("grupa").Value & " istnieje " & dtSku.Rows.Count & " wierszy, a powinien istnieć tylko jeden." & frmGlowna.kontaktIt, MsgBoxStyle.Critical)
                Return True 'lepiej zwrócić, że były zmiany ...
            ElseIf dtSku.Rows.Count < 1 Then
                bBylyZmiany = True
                Exit For
            End If
            If dtSku.Rows(0).Item("ilosc") <> dgvWiersz.Cells("ilosc").Value Then
                bBylyZmiany = True
                Exit For
            End If
        Next
        If bBylyZmiany Then Return bBylyZmiany

        'czy zmienił się tryb pracy?
        If enumTypZamowienia <> enumTypZamowieniaKopia Then
            Return True
        End If

        'może zmieniły się uwagi?
        If txtUwagi.Text <> strUwagi Then
            Return True
        End If

        If btnZapiszZmiany.Enabled Then
            btnZapiszZmiany.BackColor = kolor
        Else
            btnZapiszZmiany.BackColor = Color.LightGray
        End If
        'nic się nie zmieniło
        Return False

    End Function

    Private Sub dgv_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles dgv.CellValidating
        If enumTrybPracy = frmZamowienieINVTrybPracy.edycjaWlasnegoKoszyka Then
            If dgv.Columns(e.ColumnIndex).HeaderText.ToLower = "ilosc" Then
                Dim intIlosc As Integer
                If Not Integer.TryParse(e.FormattedValue, intIlosc) Then
                    MsgBox("Podaj liczbę całkowitą większą bądź równą zero.", MsgBoxStyle.Exclamation)
                    e.Cancel = True
                    Exit Sub
                End If
                If intIlosc < 0 Then
                    MsgBox("Podaj liczbę całkowitą większą bądź równą zero.", MsgBoxStyle.Exclamation)
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        End If
    End Sub

    Private Sub dgv_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellEndEdit
        'zatwierdzamy zmiany na poziomie komórki
        BindingContext(dgv.DataSource).EndCurrentEdit()
        'odblokowujemy kontrolki
        btnZapiszZmiany.Enabled = True
        If btnZapiszZmiany.Enabled Then
            btnZapiszZmiany.BackColor = kolor
        Else
            btnZapiszZmiany.BackColor = Color.LightGray
        End If

    End Sub

    Private Sub btnDodajPozycje_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDodajPozycje.Click

        'szukamy okna o nazwie frmStan
        Dim frm As frmStan = Nothing
        For Each frmOkno As Form In Me.MdiParent.MdiChildren
            If frmOkno.Name = "frmStan" Then
                frm = frmOkno
                If frm.ctr.intIdMagazynu = idMagazyn And frm.ctr.bStanDlaKoszykINV = True Then
                    'przywołujemy to okno na pierwszy plan
                    frm.WindowState = FormWindowState.Normal
                    frm.Activate()
                    Exit Sub
                Else
                    frm = Nothing
                End If
            End If
        Next

        If frm Is Nothing Then
            'otwieramy nowe okno stany
            frm = New frmStan
            frm.MdiParent = Me.MdiParent
            frm.ctr.intIdMagazynu = idMagazyn
            frm.bStanDlaKoszykINV = True
            frm.ctr.btnDoKoszyka.Text = "Do Koszyka INV"
            frm.ctr.btnPodzielGrupa.Visible = False
            frm.Show()
        End If
        CType(frm.ctr.Controls("ts"), ToolStrip).Items("btnDoKoszyka").Enabled = True
    End Sub

    Private Sub btnUsunPozycje_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUsunPozycje.Click
        If dgv.CurrentCell Is Nothing Then
            MsgBox("Zaznacz pozycję do usunięcia.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        dgv.Rows.RemoveAt(dgv.CurrentCell.RowIndex)
        btnZapiszZmiany.Enabled = True
        dgv.DataSource.AcceptChanges()

    End Sub


    Private Sub txtUwagi_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUwagi.TextChanged
        btnZapiszZmiany.Enabled = True
        btnZapiszZmiany.BackColor = kolor
    End Sub

    Private Sub btnZapiszZmiany_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnZapiszZmiany.Click
        zapiszZmiany()
    End Sub

    Private Sub btnZatwierdzZamowienie_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnZlozZamowienie.Click

        If intUwagiPozostaloZnakow < 0 Then
            MsgBox("Pole uwagi zawiera za duzo znaków", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        'kontrola pól zależnych od typu zamówienia
        Dim intTest As Integer

        'czy są jakieś niezapisane zmiany? Jeśli tak, zapiszmy je
        If bylyZmiany() Then
            If Not zapiszZmiany() Then Exit Sub
        End If

        'zatwierdzamy zamówienie
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik As New wsCursorProf.KoszykINVZatwierdzWynik

        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.KoszykINVZatwierdz(frmGlowna.sesja, intIdBlokady)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical)
            Me.Close()
            Exit Sub
        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, "Zatwierdzenie zamówienia")
            Exit Sub
        End If
        If wsWynik.status = 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Information, "Zatwierdzenie zamówienia")
        End If

        If wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Information, "Zatwierdzenie zamówienia")
        End If

        intIdBlokady = -1

        Select Case enumTrybPracy
            Case frmZamowienieINVTrybPracy.edycjaWlasnegoKoszyka
                frmGlowna.lblStatus.Text = "Pomyślnie zatwierdzono koszyk."
            Case frmZamowienieINVTrybPracy.podgladObcegoKoszyka
                frmGlowna.lblStatus.Text = "Pomyślnie zatwierdzono koszyk użytkownika " & strUserNazwa & "."
            Case frmZamowienieINVTrybPracy.podgladZamowienia
                frmGlowna.lblStatus.Text = "Pomyślnie zapisano zmiany w zamówieniu " & intIdZamowienia & " użytkownika " & strUserNazwa & "."
        End Select
        frmGlowna.timer.Interval = 3000 'komunikat zniknie po 3s
        frmGlowna.timer.Start()

        If Not frmGlowna.frmKoszykINV Is Nothing Then frmGlowna.frmKoszykINV = Nothing
        wczytaj()

        ''odświeżamy kontrolkę dgv z formy frmZamowienia
        If Not frmGlowna.frmZamowieniaLista Is Nothing Then
            Dim m As MethodInfo() = frmGlowna.frmZamowieniaLista.GetType.GetMethods()
            For licznik As Integer = 0 To m.GetUpperBound(0)
                If m(licznik).Name = "wczytaj" Then
                    m(licznik).Invoke(frmGlowna.frmZamowieniaLista, Nothing)
                End If
            Next
        End If

        'odświeżamy okno frmStan
        Dim frm As frmStan = Nothing
        For Each frmOkno As Form In Me.MdiParent.MdiChildren
            If frmOkno.Name = "frmStan" Then
                frm = frmOkno
                If frm.ctr.intIdMagazynu = idMagazyn And frm.ctr.bStanDlaKoszykINV = True Then
                    frm.odswiezListy()
                End If
            End If
        Next

    End Sub


    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub odswiezUprawnienia()
        'MyBase.Wlacz(frmGlowna.sesja)
    End Sub

    Private Sub cmbTypZamowienia_SelectionChangeCommitted(sender As Object, e As System.EventArgs) Handles cmbTypZamowienia.SelectionChangeCommitted
        If Not IsNothing(cmbTypZamowienia.SelectedValue) Then
            btnZapiszZmiany.Enabled = True
            Select Case cmbTypZamowienia.SelectedValue
                Case 12
                    enumTypZamowienia = frmZamowienieINVTypZamowienia.inwentaryzacja_na_plus
                Case 13
                    enumTypZamowienia = frmZamowienieINVTypZamowienia.inwentaryzacja_na_minus
            End Select
        End If
    End Sub

    Private Sub btnZapiszZmiany_EnabledChanged(sender As Object, e As System.EventArgs) Handles btnZapiszZmiany.EnabledChanged, btnZlozZamowienie.EnabledChanged
        If sender.Enabled Then
            sender.BackColor = kolor
            sender.ForeColor = Color.White
        Else
            sender.BackColor = Color.LightGray
        End If
    End Sub

    Private Sub btnPobierzSzablon_Click(sender As System.Object, e As System.EventArgs)
        PobierzPlik(nazwaSzablonu)
    End Sub
    Private Function PobierzPlik(ByVal nazwa As String) As Boolean

        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik As New wsCursorProf.PobierzPlikWynik

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
            sfd.FileName = nazwa

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

    Private Sub btnImportPozycjiExcel_Click(sender As System.Object, e As System.EventArgs) Handles btnImportPozycjiExcel.Click

        If dgv.Rows.Count > 0 Then
            If MsgBox("Uwaga! W koszyku znajdują się pozycje. Jeśli ich nie usuniesz to ilości zamawianych produktów z koszyka i pliku zostaną zsumowane." & vbNewLine & _
                      "Czy chesz usunąć przed importem istniejące pozycje z koszyka?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Pytanie") = MsgBoxResult.Yes Then
                For i As Integer = dgv.Rows.Count - 1 To 0 Step -1
                    dgv.Rows.RemoveAt(i)
                    dgv.DataSource.AcceptChanges()
                Next
            End If
        End If
        dgv.DataSource.AcceptChanges()
        Dim f As New frmZamowienieINVPozycjeExcel
        f.magazyn_id = intIdMagazynu
        If f.ShowDialog = Windows.Forms.DialogResult.OK Then
            If f.dtSKUExcel.Rows.Count > 0 Then
                importuj_dane_z_excela(f.dtDostepneSKU)
            End If
        End If
    End Sub

    Private Function importuj_dane_z_excela(dt As DataTable) As Boolean

        Dim bZnaleziono As Boolean
        Dim bDodano As Boolean = False
        Dim dodanoLicznik As Integer = 0
        For Each dtRow As DataRow In dt.Rows
            bZnaleziono = False
            For Each dgvRow As DataGridViewRow In dgv.Rows
                If dtRow("sku_id") = dgvRow.Cells("sku_id").Value And dtRow("grupa_id") = dgvRow.Cells("grupa_id").Value Then
                    dgvRow.Cells("ilosc").Value += CInt(dtRow("ilosc"))
                    bZnaleziono = True
                    Exit For
                End If
            Next
            If Not bZnaleziono Then
                dgv.DataSource.Rows.Add(dtRow("sku_id"), dtRow("sku"), dtRow("sku_nazwa"), dtRow("ilosc"), dtRow("ilosc_dostepna"), dtRow("grupa_id"), dtRow("grupa"))
                dodanoLicznik = dodanoLicznik + 1
                If Not bDodano Then
                    'dodaliśmy pierwszą pozycję, ustawiamy na niej aktywną komórkę w koszyku
                    For Each dgvRow As DataGridViewRow In dgv.Rows
                        If dgvRow.Cells("sku_id").Value = dtRow("sku_id") And dtRow("grupa_id") = dgvRow.Cells("grupa_id").Value Then
                            dgv.CurrentCell = dgv.Rows(dgvRow.Index).Cells("ilosc")
                            dgv.BeginEdit(True)
                            Exit For
                        End If
                    Next
                    bDodano = True
                End If
            End If
        Next

        dgv.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells)

        'If Not bDodano Then
        '    MsgBox("Wszystkie importowane sku masz już w swoim koszyku!", MsgBoxStyle.Exclamation, "Import pozycji do zamówienia")
        'End If

        Return True

    End Function

End Class