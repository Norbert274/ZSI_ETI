Imports System.Reflection
Public Class frmUzytkownik
    Public frmRodzic As Form
    Public intIdUzytkownika As Integer = -1 'przekazane z formy rodzica; jeśli =0 znaczy tworzymy nowego
    Private bOdswiezycRodzica As Boolean = False 'czy nasza forma dokonała chociaż jednego udanego zapisu do bazy ?
    Private intIdBlokady As Integer = -1 'id blokady wygenerowane w bazie na potrzeby edycji tego rekordu; -1 oznacza brak blokady
    Private strImie As String = ""
    Private strNazwisko As String = ""
    Private strNazwa As String = ""
    Private strTelkom As String = ""
    Private strEmail As String = ""
    Private strLogin As String = ""
    Private intWielkoscId As Integer = -1
    Private intTypId As Integer = -1
    Private intObszarSprzedazyId As Integer = -1
    Private intSiecSprzedazyId As Integer = -1
    Private intRegionSprzedazyId As Integer = -1
    Private intZespolSprzedazyId As Integer = -1
    Private dtSlowniki As DataTable

    Private bCzyLimitZamowien As Boolean = False
    Private strMaxIloscZamowien As String = ""
    Private intTypOkresZamowienid As Integer = -1

    Private strPrzelozony As String = "<brak>"
    Private intIdPrzelozonego As Integer = -1
    Private intIdPrzelozonegoWczytane As Integer = -1
    Private strMagazyn As String
    Private intIdMagazynu As Integer
	Dim Parametry As New ZmienneGlobalne
    Private idMagazyn As Integer = Parametry.idMagazyn
    Private czy_maile As Integer = 1
    Private intIdMagazynuWczytane As Integer
    Public dtGrupyUzytkownika As DataTable 'grupy do których należy użytkownik w momencie otwarcia okna
    Public dtGrupyDostepne As DataTable 'grupy dostępne w systemie; uzupełniane dopiero po otwarciu okna frmUzytkownikGrupy i trzymane tu w razie potrzeby ponownego użycia
    Public dtFunkcjeUzytkownika As DataTable 'funkcje powiazane z uzytkownikiem w momencie otwarcia okna
    Public dtFunkcjeDostepne As DataTable 'funkcje dostępne w systemie; uzupełniane dopiero po otwarciu okna frmUzytkownikGrupy i trzymane tu w razie potrzeby ponownego użycia
    Public dtRegiony As DataTable
    Public dtArea As DataTable
    Public dtWielkosc As DataTable
    Public dtTyp As DataTable
    Public dtRegionySprzedazy As DataTable
    Public dtObszarySprzedazy As DataTable
    Public dtSieciSprzedazy As DataTable
    Public dtZespolySprzedazy As DataTable
    Public blue As Color = Color.DodgerBlue

    Const conWartosc As String = "Wartosc"
    Const conSlownikWartoscId As String = "ID_SLOWNIK_WARTOSCI"

    Private bReagujNaTxtNazwa As Boolean = True 'używane podczas edycji nowego użytkownika
    Private bUzytkownikSamZmienilNazwe As Boolean = False 'używane podczas edycji nowego użytkownika
    Public user_id_do_hasla As Integer 'id usera któremu zmieniamy bądź resetujemy hasło 
    Private bFormShown As Boolean = False

    Private Sub frmUzytkownik_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If intIdBlokady >= 0 Then
            'zwalniamy blokadę rekordu
            System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
            System.Net.ServicePointManager.Expect100Continue = False
            ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
            ws.Proxy.Credentials = CredentialCache.DefaultCredentials
            'ws.Url = frmGlowna.strWebservice
            Try
                ws.UserEdytujAnuluj(frmGlowna.sesja, intIdBlokady)
            Catch ex As Exception
            End Try
        End If
        'jeżeli coś zapisywaliśmy, odświeżamy grid rodzica (tylko jeśli jego okno prezentuje metodę "odswiezListy")
        If bOdswiezycRodzica AndAlso Not frmRodzic Is Nothing Then
            Dim m As MethodInfo() = frmRodzic.GetType.GetMethods()
            For licznik As Integer = 0 To m.GetUpperBound(0)
                If m(licznik).Name = "odswiezListy" Then
                    m(licznik).Invoke(frmRodzic, Nothing)
                End If
            Next
        End If
    End Sub
    

    Private Sub frmUzytkownik_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If intIdUzytkownika > 0 Then
            'tryb edycji - ładujemy dane o użytkowniku i oznaczamy blokadę w bazie
            System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
            System.Net.ServicePointManager.Expect100Continue = False
            ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
            ws.Proxy.Credentials = CredentialCache.DefaultCredentials
            'ws.Url = frmGlowna.strWebservice
            Dim wsWynik As wsCursorProf.UserEdytujWynik
            user_id_do_hasla = intIdUzytkownika
            Try
                Cursor = Cursors.WaitCursor
                Application.DoEvents()
                wsWynik = ws.UserEdytuj(frmGlowna.sesja, intIdUzytkownika)
                Cursor = Cursors.Default
            Catch ex As Exception
                Cursor = Cursors.Default
                MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
                Me.Close()
                Exit Sub
            End Try

            If wsWynik.status < 0 Then
                MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, Me.Text)
                Me.Close()
                Exit Sub
            ElseIf wsWynik.status > 0 Then
                MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, Me.Text)
            End If

            intIdBlokady = wsWynik.blokada_id
            Me.Text = "Edycja użytkownika " & wsWynik.nazwa
            txtImie.Text = wsWynik.imie
            strImie = wsWynik.imie
            txtImie.Focus()
            txtImie.SelectAll()
            txtNazwisko.Text = wsWynik.nazwisko
            strNazwisko = wsWynik.nazwisko
            txtNazwa.Text = wsWynik.nazwa
            strNazwa = wsWynik.nazwa
            txtTelkom.Text = wsWynik.telkom
            strTelkom = wsWynik.telkom
            txtEmail.Text = wsWynik.email
            strEmail = wsWynik.email
            txtLogin.Text = wsWynik.login
            strLogin = wsWynik.login
            intWielkoscId = wsWynik.wielkosc_id
            If wsWynik.grupy_obszary.Tables.Count > 2 Then
                dtSlowniki = wsWynik.grupy_obszary.Tables(2)
            Else
                MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał tabeli ze słownikami" & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
                Me.Close()
            End If

            intWielkoscId = wsWynik.wielkosc_id

            cmbWielkosc.DataSource = wsWynik.grupy_obszary.Tables(3)
            cmbWielkosc.DisplayMember = conWartosc
            cmbWielkosc.ValueMember = conSlownikWartoscId
            cmbWielkosc.SelectedValue = intWielkoscId


            intTypId = wsWynik.typ_id

            cmbTyp.DataSource = wsWynik.grupy_obszary.Tables(4)
            cmbTyp.DisplayMember = conWartosc
            cmbTyp.ValueMember = conSlownikWartoscId
            cmbTyp.SelectedValue = intTypId

            '' OBSZARY SPRZEDAZY
            intObszarSprzedazyId = wsWynik.obszar_sprzedazy_id

            cmbObszarSprzedazy.DataSource = wsWynik.grupy_obszary.Tables(5)
            cmbObszarSprzedazy.DisplayMember = conWartosc
            cmbObszarSprzedazy.ValueMember = conSlownikWartoscId
            cmbObszarSprzedazy.SelectedValue = intObszarSprzedazyId

            '' SIECI SPRZEDAZY
            intSiecSprzedazyId = wsWynik.siec_sprzedazy_id

            cmbSiecSprzedazy.DataSource = wsWynik.grupy_obszary.Tables(6)
            cmbSiecSprzedazy.DisplayMember = conWartosc
            cmbSiecSprzedazy.ValueMember = conSlownikWartoscId
            cmbSiecSprzedazy.SelectedValue = intSiecSprzedazyId

            '' REGIONY SPRZEDAZY
            intRegionSprzedazyId = wsWynik.region_sprzedazy_id

            cmbRegionSprzedazy.DataSource = wsWynik.grupy_obszary.Tables(7)
            cmbRegionSprzedazy.DisplayMember = conWartosc
            cmbRegionSprzedazy.ValueMember = conSlownikWartoscId
            cmbRegionSprzedazy.SelectedValue = intRegionSprzedazyId


            '' ZESPOLY SPRZEDAZY
            intZespolSprzedazyId = wsWynik.zespol_sprzedazy_id

            cmbZespolSprzedazy.DataSource = wsWynik.grupy_obszary.Tables(8)
            cmbZespolSprzedazy.DisplayMember = conWartosc
            cmbZespolSprzedazy.ValueMember = conSlownikWartoscId
            cmbZespolSprzedazy.SelectedValue = intZespolSprzedazyId

            intTypOkresZamowienid = wsWynik.typ_okres_zamowien_id

            If wsWynik.grupy_obszary.Tables.Count > 9 Then
                cmbTypOkresZamowien.DataSource = wsWynik.grupy_obszary.Tables(9)
                cmbTypOkresZamowien.DisplayMember = conWartosc
                cmbTypOkresZamowien.ValueMember = conSlownikWartoscId
                cmbTypOkresZamowien.SelectedValue = intTypOkresZamowienid
            Else
                MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał listy typów okresów zamawiania." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
                Me.Close()
            End If


            If dgvGrupy.Columns.Contains("grupa_id") Then dgvGrupy.Columns("grupa_id").Visible = False
            dtGrupyUzytkownika = wsWynik.grupy_obszary.Tables(0).Copy 'zachowujemy grupy w których był użytkownik
            tpGrupy.Text = "Grupy (" & dtGrupyUzytkownika.Rows.Count & ")"
            'txtNrZewn.Text = wsWynik.nr_zew
            txtHaslo.Visible = False
            txtHaslo2.Visible = False
            lblHaslo2.Visible = False
            lblHasloStatus.Visible = True
            btnZmienHaslo.Visible = True
            btnUsunHaslo.Visible = True
            czy_maile = wsWynik.czy_maile
            'If wsWynik.czy_maile = 0 Then
            '    chkMaile.Checked = False
            'ElseIf wsWynik.czy_maile = 1 Then
            '    chkMaile.Checked = True
            'End If

            If wsWynik.haslo Then
                lblHasloStatus.Text = "(ustawione)"
            Else
                lblHasloStatus.Text = "(puste)"
            End If
            'lblPrzelozony.Text = wsWynik.przelozony_nazwa
            strPrzelozony = wsWynik.przelozony_nazwa
            intIdPrzelozonego = wsWynik.przelozony_id
            intIdPrzelozonegoWczytane = wsWynik.przelozony_id
            'lblMagazyn.Text = wsWynik.magazyn_nazwa
            strMagazyn = wsWynik.magazyn_nazwa
            intIdMagazynu = wsWynik.magazyn_id
            intIdMagazynuWczytane = wsWynik.magazyn_id
            lblAdresy.Text = wsWynik.adresy_ilosc

            bCzyLimitZamowien = wsWynik.czy_limit_zamowien
            strMaxIloscZamowien = wsWynik.max_ilosc_zamowien


            If wsWynik.czy_limit_zamowien = 1 Then
                chkCzyLimitWydanOsoba.Checked = True
                bCzyLimitZamowien = True
            ElseIf wsWynik.czy_limit_zamowien = 0 Then
                chkCzyLimitWydanOsoba.Checked = False
                bCzyLimitZamowien = False
            End If

            If chkCzyLimitWydanOsoba.Checked = True Then
                txtMaxIloscZamowien.Text = strMaxIloscZamowien
                txtMaxIloscZamowien.Enabled = True
                cmbTypOkresZamowien.Enabled = True
            Else
                txtMaxIloscZamowien.Text = ""
                txtMaxIloscZamowien.Enabled = False
                cmbTypOkresZamowien.Enabled = False
            End If


            If wsWynik.grupy_obszary.Tables.Count > 0 Then
                dgvGrupy.DataSource = wsWynik.grupy_obszary.Tables(0)
                If dgvGrupy.Columns.Contains("grupa_id") Then dgvGrupy.Columns("grupa_id").Visible = False
                dtGrupyUzytkownika = wsWynik.grupy_obszary.Tables(0).Copy 'zachowujemy grupy w których był użytkownik
                tpGrupy.Text = "Grupy (" & dtGrupyUzytkownika.Rows.Count & ")"
            Else
                MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał listy grup użytkownika" & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
                Me.Close()
            End If
            If wsWynik.grupy_obszary.Tables.Count > 1 Then
                dgvFunkcje.DataSource = wsWynik.grupy_obszary.Tables(1)
                If dgvFunkcje.Columns.Contains("funkcja_id") Then dgvFunkcje.Columns("funkcja_id").Visible = False
                If dgvFunkcje.Columns.Contains("FORMA_NAZWA") Then dgvFunkcje.Columns("FORMA_NAZWA").Visible = False
                If dgvFunkcje.Columns.Contains("WLACZ") Then dgvFunkcje.Columns("WLACZ").Visible = False
                If dgvFunkcje.Columns.Contains("WLACZ") Then dgvFunkcje.Columns("WLACZ").ReadOnly = True
                dtFunkcjeUzytkownika = wsWynik.grupy_obszary.Tables(1).Copy 'zachowujemy grupy w których był użytkownik
                tpFunkcje.Text = "Funkcje (" & dtFunkcjeUzytkownika.Rows.Count & ")"
            Else
                MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał listy funkcji użytkownika" & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
                Me.Close()
            End If

        Else
            'tryb dodawania nowego
            Dim ws2 As New wsCursorProf.CursorService
            System.Net.ServicePointManager.Expect100Continue = False
            ws2.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
            ws2.Proxy.Credentials = CredentialCache.DefaultCredentials
            'ws.Url = frmGlowna.strWebservice
            Dim wsWynik2 As wsCursorProf.UserWielkoscTypWynik
            Try
                Cursor = Cursors.WaitCursor
                Application.DoEvents()
                wsWynik2 = ws2.UserWielkoscTyp(frmGlowna.sesja)
                Cursor = Cursors.Default
            Catch ex As Exception
                Cursor = Cursors.Default
                MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
                Me.Close()
                Exit Sub
            End Try

            If wsWynik2.status < 0 Then
                MsgBox(wsWynik2.status_opis, MsgBoxStyle.Critical, Me.Text)
                Me.Close()
                Exit Sub
            ElseIf wsWynik2.status > 0 Then
                MsgBox(wsWynik2.status_opis, MsgBoxStyle.Exclamation, Me.Text)
            End If

            chkCzyLimitWydanOsoba.Checked = True
            '' WIELKOSCI
            If wsWynik2.dane.Tables.Count > 0 Then
                cmbWielkosc.DataSource = wsWynik2.dane.Tables(0)
                cmbWielkosc.DisplayMember = "NAZWA"
                cmbWielkosc.ValueMember = "ID_SLOWNIK_WARTOSCI"
                cmbWielkosc.SelectedIndex = 0
                dtWielkosc = wsWynik2.dane.Tables(0).Copy 'zachowujemy role
            Else
                MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał listy wielkości użytkownika" & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
                Me.Close()
            End If

            '' TYPY
            If wsWynik2.dane.Tables.Count > 1 Then
                cmbTyp.DataSource = wsWynik2.dane.Tables(1)
                cmbTyp.DisplayMember = "NAZWA"
                cmbTyp.ValueMember = "ID_SLOWNIK_WARTOSCI"
                cmbTyp.SelectedIndex = 0
                dtTyp = wsWynik2.dane.Tables(1).Copy
            Else
                MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał listy typu użytkownika" & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
                Me.Close()
            End If

            '' OBSZARY SPRZEDAŻY
            If wsWynik2.dane.Tables.Count > 2 Then
                cmbObszarSprzedazy.DataSource = wsWynik2.dane.Tables(2)
                cmbObszarSprzedazy.DisplayMember = "NAZWA"
                cmbObszarSprzedazy.ValueMember = "ID_SLOWNIK_WARTOSCI"
                cmbObszarSprzedazy.SelectedIndex = 0
                dtObszarySprzedazy = wsWynik2.dane.Tables(2).Copy
            Else
                MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał listy obszarów sprzedaży!" & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
                Me.Close()
            End If

            '' REGIONY SPRZEDAŻY
            If wsWynik2.dane.Tables.Count > 3 Then
                cmbRegionSprzedazy.DataSource = wsWynik2.dane.Tables(3)
                cmbRegionSprzedazy.DisplayMember = "NAZWA"
                cmbRegionSprzedazy.ValueMember = "ID_SLOWNIK_WARTOSCI"
                cmbRegionSprzedazy.SelectedIndex = 0
                dtRegionySprzedazy = wsWynik2.dane.Tables(3).Copy
            Else
                MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał listy regionów sprzedaży!" & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
                Me.Close()
            End If

            '' ZESPOLY SPRZEDAZY
            If wsWynik2.dane.Tables.Count > 4 Then
                cmbZespolSprzedazy.DataSource = wsWynik2.dane.Tables(4)
                cmbZespolSprzedazy.DisplayMember = "Nazwa"
                cmbZespolSprzedazy.ValueMember = "ID_SLOWNIK_WARTOSCI"
                cmbZespolSprzedazy.SelectedIndex = 0
                dtZespolySprzedazy = wsWynik2.dane.Tables(4).Copy
            Else
                MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał listy zespołów sprzedaży" & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
                Me.Close()
            End If

            '' SIECI SPRZEDAZY
            If wsWynik2.dane.Tables.Count > 5 Then
                cmbSiecSprzedazy.DataSource = wsWynik2.dane.Tables(5)
                cmbSiecSprzedazy.DisplayMember = "Nazwa"
                cmbSiecSprzedazy.ValueMember = "ID_SLOWNIK_WARTOSCI"
                cmbSiecSprzedazy.SelectedIndex = 0
                dtSieciSprzedazy = wsWynik2.dane.Tables(5).Copy
            Else
                MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał listy sieci sprzedaży" & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
                Me.Close()
            End If

            If wsWynik2.dane.Tables.Count > 6 Then
                cmbTypOkresZamowien.DataSource = wsWynik2.dane.Tables(6)
                cmbTypOkresZamowien.DisplayMember = "Nazwa"
                cmbTypOkresZamowien.ValueMember = "TYP_OKRES_ZAMOWIEN_ID"
                cmbTypOkresZamowien.SelectedIndex = -1
                chkCzyLimitWydanOsoba.Checked = False
            Else
                MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał listy okresów zamówie" & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
                Me.Close()
            End If

            wczytajFunkcjeDostepne()
            Me.Text = "Tworzenie nowego użytkownika"
            txtImie.Focus()
            dtGrupyUzytkownika = New DataTable
            dtGrupyUzytkownika.Columns.Add("grupa_id")
            dtGrupyUzytkownika.Columns.Add("nazwa")
            dgvGrupy.DataSource = dtGrupyUzytkownika.Copy
            dgvGrupy.Columns("grupa_id").Visible = False
            tpGrupy.Text = "Grupy"
            dtFunkcjeUzytkownika = dtFunkcjeDostepne.Copy()
            Dim isCentrala As Boolean = False
            For Each dgvRowGrupa As DataGridViewRow In dgvGrupy.Rows
                If dgvRowGrupa.Cells("NAZWA").Value.ToString() = "Biuro" Then
                    isCentrala = True
                    Exit For
                End If
            Next
            If isCentrala = False Then
                'Wyszukuje funkcje do usuniecia 6-RaportyToolStripMenuItem; 8-AwizacjeToolStripMenuItem
                For Each rowUsunac As DataRow In dtFunkcjeUzytkownika.Select("(funkcja_id <> 1) AND (funkcja_id <> 4) AND (funkcja_id <> 7) AND (funkcja_id <> 18)")  '' .Select("(funkcja_id = 6) OR (funkcja_id = 8)")
                    dtFunkcjeUzytkownika.Rows.Remove(rowUsunac)
                Next
                dtFunkcjeUzytkownika.AcceptChanges()
            End If

            dgvFunkcje.DataSource = dtFunkcjeUzytkownika.Copy()
            dgvFunkcje.Columns("funkcja_id").Visible = False
            dgvFunkcje.Columns("WLACZ").ReadOnly = True
            dgvFunkcje.Columns("WLACZ").ValueType = Type.GetType("System.Boolean")

            ''właczam wszystkie dostepne funkcje dla uzytkownika
            'For Each rowFunkcjeWlacz As DataGridViewRow In dgvFunkcje.Rows
            '    rowFunkcjeWlacz.Cells("WLACZ").Value = True
            'Next

            dgvFunkcje.Text = "Funkcje"
            btnAdresy.Enabled = False
            btnDaneDostawy.Enabled = False
            intIdMagazynu = idMagazyn
        End If
        btnZastosuj.Enabled = False
        If intIdBlokady <= 0 Then btnOk.Enabled = False
        If btnZastosuj.Enabled Then
            btnZastosuj.BackColor = blue
        Else
            btnZastosuj.BackColor = Color.LightGray
        End If

        bFormShown = True
    End Sub

    Private Sub btnAnuluj_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAnuluj.Click
        'If  Then
        '    Dim result As DialogResult = MsgBox("Od ostatniego odczytu z serwera wprowadzono zmiany w tym użytkowniku. Czy zapisać wprowadzone zmiany?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1, Me.Text)

        '    If result = Windows.Forms.DialogResult.No Then
        '        Me.Close()
        '    Else
        '        If zapiszZmiany(False) Then
        '            intIdBlokady = -1 'blokada już zwolniona w funkcji zapiszZmiany()
        '            Me.Close()
        '        End If
        '    End If
        'Else
        '    Me.Close()
        'End If
        
        Me.Close()
        intIdBlokady = -1
    End Sub

    Private Function zapiszZmiany(ByVal pozostawBlokade As Boolean) As Boolean
        Dim bBylyZmiany As Boolean = False
        Dim objZnalezionyWiersz As DataRow 'wiersz odnaleziony
        Dim wlaczValue As Boolean 'wartosc wczytaj dal wiersza odnalezionego
        Dim dtKopia As DataTable
        Dim dtKopiaFunkcje As DataTable
        'Dim czy_maile As Integer
        'If chkMaile.Checked Then
        '    czy_maile = 1
        'Else
        '    czy_maile = 0
        'End If
        'If Not IsNumeric(txtTelkom.Text) Then
        '    MessageBox.Show("Telefon musi może zawierać tylko cyfry oraz być w formacie 500600700", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information)
        '    Exit Function
        'End If

        'najpierw sprawdzamy, czy zmienił się zestaw grup do którego należy użytkownik
        dtKopia = dtGrupyUzytkownika.Copy
        For Each wierszBiezacy As DataGridViewRow In dgvGrupy.Rows
            objZnalezionyWiersz = Nothing
            For Each wierszKopia As DataRow In dtKopia.Rows
                If wierszBiezacy.Cells("grupa_id").Value = wierszKopia.Item("grupa_id") Then
                    objZnalezionyWiersz = wierszKopia
                    Exit For
                End If
            Next
            If objZnalezionyWiersz Is Nothing Then
                bBylyZmiany = True
                Exit For
            Else
                dtKopia.Rows.Remove(objZnalezionyWiersz)
            End If
        Next
        If dtKopia.Rows.Count > 0 Then bBylyZmiany = True
        Dim usuniete As Integer = 0
        'potem sprawdzamy, czy zmienił się zestaw funkcji przynaleznych do użytkownika
        'dtFunkcjeUzytkownika.AcceptChanges()
        'dgvFunkcje.DataSource.AcceptChanges()
        'If Not dtFunkcjeUzytkownika.GetChanges() Is Nothing Then bBylyZmiany = True
        dtKopiaFunkcje = dtFunkcjeUzytkownika.Copy
        For Each wierszBiezacy As DataGridViewRow In dgvFunkcje.Rows
            objZnalezionyWiersz = Nothing
            For Each wierszKopia As DataRow In dtKopiaFunkcje.Rows
                If wierszBiezacy.Cells("funkcja_id").Value = wierszKopia.Item("funkcja_id") Then
                    objZnalezionyWiersz = wierszKopia
                    wlaczValue = wierszBiezacy.Cells("WLACZ").Value
                    Exit For
                End If
            Next
            If objZnalezionyWiersz Is Nothing Then
                bBylyZmiany = True
                Exit For
            Else
                If wlaczValue = objZnalezionyWiersz.Item("WLACZ") Then
                    dtKopiaFunkcje.Rows.Remove(objZnalezionyWiersz)
                    usuniete = usuniete + 1
                Else
                    dtKopiaFunkcje.Select(String.Format("funkcja_id = {0}", objZnalezionyWiersz.Item("funkcja_id")))(0).Item("WLACZ") = wlaczValue
                    bBylyZmiany = True
                End If
            End If
        Next
        If dtKopiaFunkcje.Rows.Count > 0 Or dgvFunkcje.Rows.Count - usuniete > 0 Then bBylyZmiany = True

        'jeżeli edytujemy nowy rekord, to zaznaczamy flagę bBylyZmiany zawsze na True - żeby zawsze zapisać dane
        If intIdBlokady <= 0 Then bBylyZmiany = True

        'czy coś się zmieniło?
        If txtImie.Text <> strImie OrElse _
        txtNazwisko.Text <> strNazwisko OrElse _
        txtNazwa.Text <> strNazwa OrElse _
        txtTelkom.Text <> strTelkom OrElse _
        txtEmail.Text <> strEmail OrElse _
        txtLogin.Text <> strLogin OrElse _
        txtHaslo.Text.Length > 0 OrElse _
        txtHaslo2.Text.Length > 0 OrElse _
        intIdPrzelozonego <> intIdPrzelozonegoWczytane OrElse _
        intIdMagazynu <> intIdMagazynuWczytane OrElse _
        cmbWielkosc.SelectedValue <> intWielkoscId OrElse _
        cmbTyp.SelectedValue <> intTypId OrElse _
        cmbObszarSprzedazy.SelectedValue <> intObszarSprzedazyId OrElse _
        cmbRegionSprzedazy.SelectedValue <> intRegionSprzedazyId OrElse _
        cmbZespolSprzedazy.SelectedValue <> intZespolSprzedazyId OrElse _
        cmbSiecSprzedazy.SelectedValue <> intSiecSprzedazyId OrElse _
        strMaxIloscZamowien <> txtMaxIloscZamowien.Text OrElse _
        intTypOkresZamowienid <> cmbTypOkresZamowien.SelectedValue OrElse _
        bCzyLimitZamowien <> chkCzyLimitWydanOsoba.Checked OrElse _
        bBylyZmiany Then

            bBylyZmiany = True

            'weryfikacja poprawności danych
            If txtNazwa.Text.Length < 1 Then
                MsgBox("Pole 'nazwa wyświetlana' nie może być puste.", MsgBoxStyle.Exclamation, Me.Text)
                Return False
            End If
            If txtHaslo.Text <> txtHaslo2.Text Then
                MsgBox("Zawartość pól 'hasło' i 'hasło (powtórz)' jest różna.", MsgBoxStyle.Exclamation, Me.Text)
                Return False
            End If
            If intIdBlokady <= 0 AndAlso txtHaslo.Text.Length < 1 Then
                MsgBox("Musisz nadać hasło użytkownikowi.", MsgBoxStyle.Exclamation, Me.Text)
                Return False
            ElseIf txtHaslo.Text.Length > 0 AndAlso txtHaslo.Text.Length < 6 Then
                Dim wynik = MsgBox("Podane hasło jest krótsze niż 6 znaków. Czy na pewno stworzyć użytkownika z tak krótkim hasłem?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, Me.Text)
                If wynik <> vbYes Then Return False
            End If
            If intIdMagazynu.ToString.Length = 0 Then
                Dim wynik = MsgBox("Brak przypisanego magazynu. Przypisz magazyn do użytkownika!", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, Me.Text)
                If wynik <> vbYes Then Return False
            End If
            If dgvGrupy.Rows.Count < 1 Then
                Dim wynik = MsgBox("Uzytkownik nie należy do żadnej z grup. Czy na pewno tak ma być?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, Me.Text)
                If wynik <> vbYes Then Return False
            End If
            If dgvFunkcje.Rows.Count < 1 Then
                Dim wynik = MsgBox("Uzytkownik nie posiada żadnej funkcji. Czy na pewno tak ma być?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, Me.Text)
                If wynik <> vbYes Then Return False
            End If

            If chkCzyLimitWydanOsoba.Checked = True Then
                Dim maxIloscZamowien As Integer
                Dim maxIloscZamowienInt64 As Int64
                If txtMaxIloscZamowien.Text = "" Then
                    MsgBox("Maksymalna ilość zamówień musi być liczbą całkowitą większą od zera!", MsgBoxStyle.Exclamation, Me.Text)
                    txtMaxIloscZamowien.Focus()
                    Return False
                ElseIf Not Integer.TryParse(txtMaxIloscZamowien.Text, maxIloscZamowien) Then
                    If Int64.TryParse(txtMaxIloscZamowien.Text, maxIloscZamowienInt64) Then
                        MsgBox("Przekroczono maksymalną wartość dla maksymalnej ilości zamówień: " & Integer.MaxValue & "!", MsgBoxStyle.Exclamation, Me.Text)
                        txtMaxIloscZamowien.Focus()
                        Return False
                    Else
                        MsgBox("Maksymalna ilość zamówień musi być liczbą całkowitą większą od zera!", MsgBoxStyle.Exclamation, Me.Text)
                        txtMaxIloscZamowien.Focus()
                        Return False
                    End If
                ElseIf maxIloscZamowien <= 0 Then
                    MsgBox("Maksymalna ilość zamówień musi być liczbą całkowitą większą od zera!", MsgBoxStyle.Exclamation, Me.Text)
                    txtMaxIloscZamowien.Focus()
                    Return False
                End If

                If IIf(cmbTypOkresZamowien.SelectedValue Is Nothing, -1, cmbTypOkresZamowien.SelectedValue) = -1 Then
                    MsgBox("Proszę wybrać okres, w jakim użytkownik może złożyć podaną ilość zamówień!", MsgBoxStyle.Exclamation, Me.Text)
                    cmbTypOkresZamowien.Focus()
                    Return False
                End If

            End If

            'If chkMaile.Checked = True Then
            czy_maile = 1
            'Else
            '    czy_maile = 0
            'End If

            System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
            System.Net.ServicePointManager.Expect100Continue = False
            ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
            ws.Proxy.Credentials = CredentialCache.DefaultCredentials
            'ws.Url = frmGlowna.strWebservice
            Dim wsWynik As wsCursorProf.UserEdytujZapiszWynik
            Try
                Dim ds As New DataSet
                ds.Tables.Add(dgvGrupy.DataSource.Copy)
                dgvFunkcje.DataSource.AcceptChanges()
                ds.Tables.Add(dgvFunkcje.DataSource.Copy)
                Cursor = Cursors.WaitCursor
                Application.DoEvents()
                wsWynik = ws.UserEdytujZapisz(frmGlowna.sesja, txtImie.Text, txtNazwisko.Text, _
                    txtNazwa.Text, txtTelkom.Text, txtEmail.Text, txtLogin.Text, txtHaslo.Text, _
                    intIdPrzelozonego, intIdMagazynu, czy_maile, intIdBlokady, pozostawBlokade, ds, "<brak>", _
                    cmbTyp.SelectedValue, cmbWielkosc.SelectedValue, _
                    cmbObszarSprzedazy.SelectedValue, cmbSiecSprzedazy.SelectedValue, _
                    cmbRegionSprzedazy.SelectedValue, cmbZespolSprzedazy.SelectedValue, IIf(chkCzyLimitWydanOsoba.Checked, 1, 0), _
                    IIf(txtMaxIloscZamowien.Text = "", 0, txtMaxIloscZamowien.Text), cmbTypOkresZamowien.SelectedValue)
                Cursor = Cursors.Default
            Catch ex As Exception
                Cursor = Cursors.Default
                MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
                Return False
            End Try
            If wsWynik.status < 0 Then
                MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, Me.Text)
                Return False
            ElseIf wsWynik.status > 0 Then
                MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, Me.Text)
            End If

            'modyfikujemy zmienne formy po udanym zapisie
            bOdswiezycRodzica = True
            intIdBlokady = wsWynik.blokada_id
            frmGlowna.lblStatus.Text = "Poprawnie zapisano dane użytkownika " & txtNazwa.Text
            frmGlowna.timer.Interval = 2000 'komunikat zniknie po 2s
            frmGlowna.timer.Start()
            txtHaslo.Text = ""
            txtHaslo2.Text = ""
            btnZastosuj.Enabled = False
            strImie = txtImie.Text
            txtImie.SelectAll()
            strNazwisko = txtNazwisko.Text
            strNazwa = txtNazwa.Text
            strTelkom = txtTelkom.Text
            strEmail = txtEmail.Text
            strLogin = txtLogin.Text
            intIdPrzelozonegoWczytane = intIdPrzelozonego
            bCzyLimitZamowien = chkCzyLimitWydanOsoba.Checked
            strMaxIloscZamowien = txtMaxIloscZamowien.Text
            intTypOkresZamowienid = cmbTypOkresZamowien.SelectedValue

            dtGrupyUzytkownika = dgvGrupy.DataSource.Copy 'zachowujemy grupy w których był użytkownik
            tpGrupy.Text = "Grupy (" & dtGrupyUzytkownika.Rows.Count & ")"
            dtFunkcjeUzytkownika = dgvFunkcje.DataSource.Copy 'zachowujemy grupy w których był użytkownik
            tpFunkcje.Text = "Funkcje (" & dtFunkcjeUzytkownika.Rows.Count & ")"
            btnAdresy.Enabled = True
            btnDaneDostawy.Enabled = True
            'jeśli nie był pokazany label "status hasła" to znaczy, że zapisaliśmy nowego użytkownika
            If Not lblHasloStatus.Visible Then
                txtHaslo.Visible = False
                txtHaslo2.Visible = False
                lblHaslo2.Visible = False
                lblHasloStatus.Visible = True
                btnZmienHaslo.Visible = True
                btnUsunHaslo.Visible = True
                If txtHaslo.Text.Length > 0 Then
                    lblHasloStatus.Text = "(ustawione)"
                Else
                    lblHasloStatus.Text = "(puste)"
                End If
            End If

        Else
            bBylyZmiany = False
        End If

        If Not bBylyZmiany Then
            frmGlowna.lblStatus.Text = "Dane użytkownika " & txtNazwa.Text & " nie uległy zmianie, zapis do bazy nie był konieczny"
            frmGlowna.timer.Interval = 2000 'komunikat zniknie po 2s
            frmGlowna.timer.Start()
            If Not pozostawBlokade Then
                System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
                System.Net.ServicePointManager.Expect100Continue = False
                ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
                ws.Proxy.Credentials = CredentialCache.DefaultCredentials
                'ws.Url = frmGlowna.strWebservice
                Try
                    ws.UserEdytujAnuluj(frmGlowna.sesja, intIdBlokady)
                Catch ex As Exception
                End Try
            End If
        End If
        If btnZastosuj.Enabled Then
            btnZastosuj.BackColor = blue
        Else
            btnZastosuj.BackColor = Color.LightGray
        End If
        Return True
    End Function

    Private Sub btnZastosuj_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnZastosuj.Click
        zapiszZmiany(True)
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        If zapiszZmiany(False) Then
            intIdBlokady = -1 'blokada już zwolniona w funkcji zapiszZmiany()
            Me.Close()
        End If
    End Sub

    Private Sub txtImie_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtImie.TextChanged
        btnZastosuj.Enabled = True
        btnOk.Enabled = True
        btnZastosuj.BackColor = blue
        If intIdBlokady <= 0 AndAlso Not bUzytkownikSamZmienilNazwe Then
            bReagujNaTxtNazwa = False
            txtNazwa.Text = txtImie.Text & " " & txtNazwisko.Text
            bReagujNaTxtNazwa = True
        End If

    End Sub

    Private Sub txtNazwisko_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNazwisko.TextChanged
        btnZastosuj.Enabled = True
        btnOk.Enabled = True
        btnZastosuj.BackColor = blue
        If intIdBlokady <= 0 AndAlso Not bUzytkownikSamZmienilNazwe Then
            bReagujNaTxtNazwa = False
            txtNazwa.Text = txtImie.Text & " " & txtNazwisko.Text
            bReagujNaTxtNazwa = True
        End If
    End Sub

    Private Sub txtNazwa_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNazwa.TextChanged
        btnZastosuj.Enabled = True
        btnOk.Enabled = True
        btnZastosuj.BackColor = blue
        If bReagujNaTxtNazwa Then bUzytkownikSamZmienilNazwe = True
    End Sub

    Private Sub txtTelkom_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTelkom.TextChanged
        btnZastosuj.Enabled = True
        btnOk.Enabled = True
        btnZastosuj.BackColor = blue
    End Sub

    Private Sub txtEmail_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEmail.TextChanged
        btnZastosuj.Enabled = True
        btnOk.Enabled = True
        btnZastosuj.BackColor = blue
    End Sub

    Private Sub txtLogin_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLogin.TextChanged
        btnZastosuj.Enabled = True
        btnOk.Enabled = True
        btnZastosuj.BackColor = blue
    End Sub

    Private Sub txtHaslo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtHaslo.TextChanged
        btnZastosuj.Enabled = True
        btnOk.Enabled = True
        btnZastosuj.BackColor = blue
    End Sub

    Private Sub txtHaslo2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtHaslo2.TextChanged
        btnZastosuj.Enabled = True
        btnOk.Enabled = True
        btnZastosuj.BackColor = blue
    End Sub

    Private Sub btnEdytujGrupy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdytujGrupy.Click

        'wyświetlamy okno wyboru grup w trybie modalnym
        Dim frm As frmUzytkownikGrupy = New frmUzytkownikGrupy

        'w razie potrzeby wczytujemy listę grup dostępnych w systemie
        If dtGrupyDostepne Is Nothing Then
            System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
            System.Net.ServicePointManager.Expect100Continue = False
            ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
            ws.Proxy.Credentials = CredentialCache.DefaultCredentials
            'ws.Url = frmGlowna.strWebservice
            Dim wsWynik As wsCursorProf.GrupaListaWynik
            Try
                Cursor = Cursors.WaitCursor
                Application.DoEvents()
                wsWynik = ws.GrupaLista(frmGlowna.sesja)
                Cursor = Cursors.Default
            Catch ex As Exception
                Cursor = Cursors.Default
                MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
                Exit Sub
            End Try

            If wsWynik.status < 0 Then
                MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, Me.Text)
                Exit Sub
            ElseIf wsWynik.status > 0 Then
                MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, Me.Text)
            End If

            'testujemy, czy odpowiedź serwera jest prawidłowa (dwie tabele - pierwsza z nazwami grup i druga z hierarchią grup)
            If wsWynik.dane.Tables.Count < 2 Then
                MsgBox("Błąd systemu - serwer nie zwrócił dwóch tabel z opisem grup" & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
                Exit Sub
            End If

            dtGrupyDostepne = wsWynik.dane.Tables(0)
        End If
        frm.frmRodzic = Me
        If intIdBlokady > 1 Then
            frm.Text = "Edycja grup dla użytkownika " & strNazwa
        Else
            frm.Text = "Edycja grup dla nowo tworzonego użytkownika"
        End If
        frm.ShowDialog()
    End Sub

    Private Sub btnZmienPrzelozonego_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim frm As New frmUzytkownicy
        frm.bTrybWyboruUzytkownika = True
        frm.ShowDialog()
        If frm.intIdWybranegoUzytkownika > 0 Then
            intIdPrzelozonego = frm.intIdWybranegoUzytkownika
            'lblPrzelozony.Text = frm.strNazwaWybranegoUzytkownika
            btnZastosuj.Enabled = True
            btnOk.Enabled = True
            btnZastosuj.BackColor = blue
        End If
    End Sub

    Private Sub btnZmienMagazyn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim frm As New frmMagazyny
        frm.ShowDialog()
        If frm.intIdWybranegoMagazynu > 0 Then
            intIdMagazynu = frm.intIdWybranegoMagazynu
            'lblMagazyn.Text = frm.strNazwaWybranegoMagazynu
            btnZastosuj.Enabled = True
            btnOk.Enabled = True
            btnZastosuj.BackColor = blue
        End If
    End Sub

    Private Sub btnZmienHaslo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnZmienHaslo.Click
        Dim frm As New frmUzytkownikHaslo
        frm.user_id = user_id_do_hasla
        frm.ShowDialog()
        If frm.strHaslo.Length > 0 Then
            'ustawiono hasło
            txtHaslo.Text = frm.strHaslo
            txtHaslo2.Text = frm.strHaslo
            lblHasloStatus.Text = "(ustawione)"
        End If
        frm.Dispose()
    End Sub

    Private Sub btnUsunHaslo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUsunHaslo.Click
        Dim frm As New frmUzytkownikHaslo
        frm.user_id = user_id_do_hasla
        frm.txtHaslo.Text = ""
        frm.txtHaslo2.Text = ""

        txtHaslo.Text = ""
        txtHaslo2.Text = ""
        lblHasloStatus.Text = "(puste)"
        btnZastosuj.Enabled = True
        btnOk.Enabled = True
        btnZastosuj.BackColor = blue

        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik As New wsCursorProf.ZmienHasloUzytkownikaWynik

        'ws.Url = frmGlowna.strWebservice
        'ws.Url = frmGlowna.strWebservice

        Try
            wsWynik = ws.ZmienHasloUzytkownika(frmGlowna.sesja, user_id_do_hasla, "")
        Catch ex As Exception
            wsWynik.status = -1
            wsWynik.status_opis = "Błąd komunikacji z serwerem: " & ex.Message & frmGlowna.kontaktIt
        End Try
    End Sub

    Private Sub btnAdresy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdresy.Click
        If intIdUzytkownika < 0 Then
            MsgBox("Błąd wewnętrzny systemu. Zmienna intIdUzytkownika jest nie ustawiona.", MsgBoxStyle.Critical, Me.Text)
            Exit Sub
        End If
        Dim frm As New frmAdresy
        frm.frmRodzic = Me
        frm.intIdUzytkownika = intIdUzytkownika
        frm.strNazwaUzytkownika = strNazwa
        If Me.Modal Then
            frm.ShowDialog()
        Else
            frm.MdiParent = frmGlowna
            frm.Show()
        End If
    End Sub
    Private Sub wczytajFunkcjeDostepne()
        If dtFunkcjeDostepne Is Nothing Then
            System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
            System.Net.ServicePointManager.Expect100Continue = False
            ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
            ws.Proxy.Credentials = CredentialCache.DefaultCredentials
            'ws.Url = frmGlowna.strWebservice
            Dim wsWynik As wsCursorProf.FunkcjaListaWynik
            Try
                Cursor = Cursors.WaitCursor
                Application.DoEvents()
                wsWynik = ws.FunkcjaLista(frmGlowna.sesja)
                Cursor = Cursors.Default
            Catch ex As Exception
                Cursor = Cursors.Default
                MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
                Exit Sub
            End Try

            If wsWynik.status < 0 Then
                MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, Me.Text)
                Exit Sub
            ElseIf wsWynik.status > 0 Then
                MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, Me.Text)
            End If

            'testujemy, czy odpowiedź serwera jest prawidłowa (dwie tabele - pierwsza z nazwami grup i druga z hierarchią grup)
            If wsWynik.dane.Tables.Count < 1 Then
                MsgBox("Błąd systemu - serwer nie zwrócił tabeli z opisem funkcji" & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
                Exit Sub
            End If

            dtFunkcjeDostepne = wsWynik.dane.Tables(0)
        End If
    End Sub

    Private Sub btnEdytujFunkcje_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdytujFunkcje.Click
        'wyświetlamy okno wyboru funkcji w trybie modalnym
        Dim frm As frmUzytkownikFunkcje = New frmUzytkownikFunkcje

        'w razie potrzeby wczytujemy listę grup dostępnych w systemie
        wczytajFunkcjeDostepne()
        frm.frmRodzic = Me
        If intIdBlokady > 1 Then
            frm.Text = "Edycja funkcji dla użytkownika " & strNazwa
        Else
            frm.Text = "Edycja funkcji dla nowo tworzonego użytkownika"
        End If
        frm.ShowDialog()
    End Sub

    Private Sub chkMaile_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        btnOk.Enabled = True
        btnZastosuj.Enabled = True
        If btnZastosuj.Enabled Then
            btnZastosuj.BackColor = blue
        Else
            btnZastosuj.BackColor = Color.LightGray
        End If
    End Sub

    Public Sub AdresyLicznikZwieksz()
        lblAdresy.Text += 1
    End Sub

    Public Sub AdresyLicznikZmniejsz()
        lblAdresy.Text -= 1
    End Sub


    Private Sub btnDaneDostawy_Click(sender As System.Object, e As System.EventArgs) Handles btnDaneDostawy.Click
        Dim frm As New frmDaneDpdUprawnienia
        frm.user_id = user_id_do_hasla
        frm.ShowDialog()
    End Sub

    Private Sub cmbRegionSprzedazy_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbRegionSprzedazy.SelectedIndexChanged
        If bFormShown Then
            btnZastosuj.Enabled = True
            btnOk.Enabled = True
            btnZastosuj.BackColor = blue
        End If
    End Sub

    Private Sub cmbObszarSprzedazy_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbObszarSprzedazy.SelectedIndexChanged
        If bFormShown Then
            btnZastosuj.Enabled = True
            btnOk.Enabled = True
            btnZastosuj.BackColor = blue
        End If
    End Sub

    Private Sub cmbTyp_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbTyp.SelectedIndexChanged
        If bFormShown Then
            btnZastosuj.Enabled = True
            btnOk.Enabled = True
            btnZastosuj.BackColor = blue
        End If
    End Sub

    Private Sub cmbWielkosc_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbWielkosc.SelectedIndexChanged
        If bFormShown Then
            btnZastosuj.Enabled = True
            btnOk.Enabled = True
            btnZastosuj.BackColor = blue
        End If
    End Sub

    Private Sub cmbZespolSprzedazy_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbZespolSprzedazy.SelectedIndexChanged
        If bFormShown Then
            btnZastosuj.Enabled = True
            btnOk.Enabled = True
            btnZastosuj.BackColor = blue
        End If
    End Sub

    Private Sub cmbSiecSprzedazy_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbSiecSprzedazy.SelectedIndexChanged
        If bFormShown Then
            btnZastosuj.Enabled = True
            btnOk.Enabled = True
            btnZastosuj.BackColor = blue
        End If
    End Sub

    Private Sub chkCzyLimitWydanOsoba_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkCzyLimitWydanOsoba.CheckedChanged
        If chkCzyLimitWydanOsoba.Checked = True Then
            txtMaxIloscZamowien.Text = strMaxIloscZamowien
            txtMaxIloscZamowien.Enabled = True
            cmbTypOkresZamowien.Enabled = True
        Else
            txtMaxIloscZamowien.Text = ""
            txtMaxIloscZamowien.Enabled = False
            cmbTypOkresZamowien.Enabled = False
        End If
        btnZastosuj.Enabled = True
        btnOk.Enabled = True
        btnZastosuj.BackColor = blue
    End Sub

    Private Sub txtMaxIloscZamowien_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtMaxIloscZamowien.TextChanged
        btnZastosuj.Enabled = True
        btnOk.Enabled = True
        btnZastosuj.BackColor = blue
    End Sub

    Private Sub cmbTypOkresZamowien_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbTypOkresZamowien.SelectedIndexChanged
        btnZastosuj.Enabled = True
        btnOk.Enabled = True
        btnZastosuj.BackColor = blue
    End Sub

    Private Sub btnNotyfikacja_Click(sender As System.Object, e As System.EventArgs) Handles btnNotyfikacja.Click
        Dim frm As New frmNotyfikacjeUzytkownikow
        frm.intIdUser = intIdUzytkownika
        frm.ShowDialog()
    End Sub
End Class