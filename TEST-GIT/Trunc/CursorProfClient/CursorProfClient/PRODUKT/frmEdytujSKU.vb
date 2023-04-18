Imports System.Reflection
Imports System.IO
Imports System.Data

Public Class frmEdytujSKU
    Inherits frmBase
    Public sciezka As String
    Public frmRodzic As frmStan
    Public intIdSKU As Integer = -1 'przekazane z formy rodzica; jeśli =0 znaczy tworzymy nowego
    Public NrSKU As String
    Private bOdswiezycRodzica As Boolean = False 'czy nasza forma dokonała chociaż jednego udanego zapisu do bazy ?
    Private intIdBlokady As Integer = -1 'id blokady wygenerowane w bazie na potrzeby edycji tego rekordu; -1 oznacza brak blokady
    Private strNumerSKU As String = ""
    Private strNazwaSKU As String = ""
    Private strOpisSKU As String = ""
    Private strOpisRozszerzony As String = ""
    Private strCenaJM As String = ""
    Private strWysokosc As String = ""
    Private strSzerokosc As String = ""
    Private strWaga As String = ""
    Private strGlebokosc As String = ""
    Private strMaxIlosc As String = ""
    Private strMaxIloscZamowien As String = ""
    Private strMarka As String = ""
    Private strBranza As String = ""
    Private strJM As String = ""
    Private strSztOpk As String = ""
    Private intTypOkresZamowienid As Integer = -1
    Private strLimitLogistyczny As String = ""
    Private strKategoria As String = ""
    Private img As Image
    Private zdjecieZmieniono As Boolean = False
    Private bCzyMoznaZamawiac As Boolean = False
    Private bCzyNowosc As Boolean = False
    Private bCzyLimit As Boolean = False
    Public dtBranze As DataTable
    Public dtMarki As DataTable
    Public dtJM As DataTable
    Public blue As Color = Color.DodgerBlue
    Private bZamykamyForme As Boolean = False
    Private bZapisano As Boolean = False
    'Private bReagujNaTxtNazwa As Boolean = True 'używane podczas edycji nowego użytkownika
    'Private bUzytkownikSamZmienilNazwe As Boolean = False 'używane podczas edycji nowego użytkownika
    'Public user_id_do_hasla As Integer 'id usera któremu zmieniamy bądź resetujemy hasło 

    Private Sub frmEdytujSKU_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        bZamykamyForme = True

        If bylyZmiany() And bZapisano = False Then
            Dim odp As MsgBoxResult = MsgBox("Uwaga! Wprowadzono zmiany w tym produkcie. Czy chcesz je zapisać?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNoCancel + MsgBoxStyle.DefaultButton1, "Zapisanie zmian")
            If odp = MsgBoxResult.Yes Then
                zapiszZmiany()
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

    Private Function SKUEdytujFiltry() As Boolean
        Dim w As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        w.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        w.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wWynik As wsCursorProf.SKUEdytujFiltryWynik
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wWynik = w.SKUEdytujFiltry(frmGlowna.sesja)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, "Wczytanie listy wartości atrybutów")
            Me.Close()
            Return False
        End Try

        If wWynik.status < 0 Then
            MsgBox(wWynik.status_opis, MsgBoxStyle.Critical, "Wczytanie listy wartości atrybutów")
            Return False
        ElseIf wWynik.status > 0 Then
            MsgBox(wWynik.status_opis, MsgBoxStyle.Exclamation, "Wczytanie listy wartości atrybutów")
        End If

        'Marka
        If wWynik.dane.Tables.Count > 3 And wWynik.dane.Tables(3).Rows.Count > 0 Then
            For Each row As DataRow In wWynik.dane.Tables(3).Rows
                cmbMarka.Items.Add(row.Item("nazwa_atrybut").ToString)
            Next
            'cmbMarka.Items.Add("BRAK")

        Else
            MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał listy dostępnych marek." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, "Wczytanie listy marek")
            Return False
        End If
        cmbMarka.SelectedIndex = 0
        'Branza
        If wWynik.dane.Tables.Count > 1 And wWynik.dane.Tables(1).Rows.Count > 0 Then
            For Each row As DataRow In wWynik.dane.Tables(1).Rows
                cmbBranza.Items.Add(row.Item("nazwa_atrybut").ToString)
            Next
            cmbBranza.Items.Add("BRAK")
        Else
            MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał listy dostępnych branż." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, "Wczytanie listy branż")
            Return False
        End If
        cmbBranza.SelectedIndex = 0
        'J.M.
        If wWynik.dane.Tables.Count > 2 And wWynik.dane.Tables(2).Rows.Count > 0 Then
            For Each row As DataRow In wWynik.dane.Tables(2).Rows
                cmbJM.Items.Add(row.Item("nazwa_atrybut").ToString)
            Next
        Else
            MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał listy dostępnych jednostek miary." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, "Wczytanie listy jednostek miary")
            Return False
        End If
        cmbJM.SelectedIndex = 0

        If wWynik.dane.Tables.Count > 0 And wWynik.dane.Tables(0).Rows.Count > 0 Then
            For Each row As DataRow In wWynik.dane.Tables(0).Rows
                cmbKategoria.Items.Add(row.Item("nazwa_atrybut").ToString)
            Next
            'cmbKategoria.Items.Add("BRAK")
        Else
            MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał listy dostępnych kategorii." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, "Wczytanie listy wartości kategorii")
            Return False
        End If
        cmbKategoria.SelectedIndex = 0

        'Typ_OKRES_ZAMOWIEN
        If wWynik.dane.Tables.Count > 4 And wWynik.dane.Tables(4).Rows.Count > 0 Then
            cmbTypOkresZamowien.DataSource = wWynik.dane.Tables(4).Copy
            cmbTypOkresZamowien.DisplayMember = "NAZWA"
            cmbTypOkresZamowien.ValueMember = "TYP_OKRES_ZAMOWIEN_ID"
            For Each row As DataRow In wWynik.dane.Tables(4).Rows
                If intTypOkresZamowienid < 0 AndAlso CInt(row.Item("DOMYSLNY")) = 1 Then
                    intTypOkresZamowienid = CInt(row.Item("TYP_OKRES_ZAMOWIEN_ID"))
                End If
            Next
        Else
            MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał listy dostępnych typów okresu zamówień." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, "Wczytanie listy typów okresu zamowień")
            Return False
        End If
        cmbTypOkresZamowien.SelectedValue = intTypOkresZamowienid
        Return True
    End Function

    'Private Sub zapisz_zdjecie()
    '    Dim im As Image = Image.FromFile(sciezka)
    '    imgSKU.Image = im
    '    imgSKU.SizeMode = PictureBoxSizeMode.Zoom
    'End Sub
    Private Function wczytaj() As Boolean
        If intIdSKU > 0 Then
            'tryb edycji - ładujemy dane o użytkowniku i oznaczamy blokadę w bazie
            System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
            Dim ws As New wsCursorProf.CursorService
            System.Net.ServicePointManager.Expect100Continue = False
            ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
            ws.Proxy.Credentials = CredentialCache.DefaultCredentials
            'ws.Url = frmGlowna.strWebservice
            Dim wsWynik As wsCursorProf.SKUEdytujWynik
            'user_id_do_hasla = intIdUzytkownika
            Try
                Cursor = Cursors.WaitCursor
                Application.DoEvents()
                wsWynik = ws.SKUEdytuj(frmGlowna.sesja, intIdSKU)
                Cursor = Cursors.Default
            Catch ex As Exception
                Cursor = Cursors.Default
                MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, "Rozpoczęcie edycji produktu")
                Me.Close()
                Return False
            End Try

            If wsWynik.status < 0 Then
                MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, "Rozpoczęcie edycji produktu")
                Me.Close()
                Return False
            ElseIf wsWynik.status > 0 Then
                MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, "Rozpoczęcie edycji produktu")
            End If

            'przypisanie wartości zmiennym formularza z wyniku procedury SP_SKU_EDYCJA_ROZPOCZNIJ
            sciezka = String.Empty
            strNumerSKU = wsWynik.sku
            strNazwaSKU = wsWynik.sku_nazwa
            strOpisSKU = wsWynik.opis
            strOpisRozszerzony = wsWynik.opis_rozszerzony
            strCenaJM = wsWynik.cena.ToString("0.00")
            strWysokosc = wsWynik.wysokosc
            strSzerokosc = wsWynik.szerokosc
            strGlebokosc = wsWynik.glebokosc
            strWaga = wsWynik.waga
            strMaxIlosc = wsWynik.max_ilosc
            strMaxIloscZamowien = wsWynik.max_ilosc_zamowien
            strMarka = wsWynik.marka
            strBranza = wsWynik.branza
            strJM = wsWynik.jm
            strSztOpk = wsWynik.sztuk_w_opakowaniu
            intTypOkresZamowienid = wsWynik.typ_okres_zamowien_id
            strKategoria = wsWynik.kategoria
            'strLimitLogistyczny = wsWynik.limit_logistyczny

            'przypisanie wartości kontrolkom formularza
            intIdBlokady = wsWynik.blokada_id
            Me.Text = "Edycja SKU: " & strNumerSKU & " (" & wsWynik.sku_nazwa & ")"
            lblNumerSKU.Text = strNumerSKU
            txtNazwaSKU.Text = strNazwaSKU
            txtOpisSKU.Text = strOpisSKU
            txtOpisRozszerzony.Text = strOpisRozszerzony
            cmbJM.SelectedItem = strJM
            cmbTypOkresZamowien.SelectedValue = intTypOkresZamowienid
            txtCenaJM.Text = strCenaJM
            txtWysokosc.Text = strWysokosc
            txtSzerokosc.Text = strSzerokosc
            txtGlebokosc.Text = strGlebokosc
            txtWaga.Text = strWaga
            If strMarka = "" Then
                strMarka = "NIEOKREŚLONA"
            End If
            If strBranza = "" Then
                strBranza = "NIEOKREŚLONA"
            End If
            If strKategoria = "" Then
                strKategoria = "NIEOKREŚLONA"
            End If
            cmbMarka.SelectedItem = strMarka
            cmbBranza.SelectedItem = strBranza
            cmbJM.SelectedItem = strJM
            cmbKategoria.SelectedItem = strKategoria
            txtMaxIlosc.Text = strMaxIlosc
            txtMaxIloscZamowien.Text = strMaxIloscZamowien
            txtSztOpk.Text = strSztOpk
            lblBrakZdjecia.Text = ""
            If wsWynik.czy_mozna_zamawiac = 1 Then
                chkCzyMoznaZamawiac.Checked = True
                bCzyMoznaZamawiac = True
            ElseIf wsWynik.czy_mozna_zamawiac = 0 Then
                chkCzyMoznaZamawiac.Checked = False
                bCzyMoznaZamawiac = False
            End If
            zdjecieZmieniono = False
            If wsWynik.czy_nowosc = 1 Then
                chkNowosc.Checked = True
                bCzyNowosc = True
            ElseIf wsWynik.czy_nowosc = 0 Then
                chkNowosc.Checked = False
                bCzyNowosc = False
            End If

            If wsWynik.czy_limit_wydan = 1 Then
                chkCzyLimitWydanOsoba.Checked = True
                bCzyLimit = True
            ElseIf wsWynik.czy_limit_wydan = 0 Then
                chkCzyLimitWydanOsoba.Checked = False
                bCzyLimit = False
            End If

            '' wczytanie zdjęcia wybranego SKU
            'If Not wsWynik.zdjecie.Length = 0 Then
            '    Dim arrPicture() As Byte = CType(wsWynik.zdjecie, Byte())
            '    Dim ms As New IO.MemoryStream(arrPicture)
            '    Dim im As Image = Image.FromStream(ms)
            '    imgSKU.Image = im
            '    img = im
            '    btnUsunZdjecie.Enabled = True
            '    btnDodajZdjecie.Enabled = False
            '    btnDodajZdjecie.BackColor = Color.LightGray
            '    imgSKU.SizeMode = PictureBoxSizeMode.Zoom
            'Else
            '    lblBrakZdjecia.Text = "Brak zdjęcia"
            '    btnUsunZdjecie.Enabled = False
            '    btnDodajZdjecie.Enabled = True
            '    btnUsunZdjecie.BackColor = Color.LightGray
            'End If

            '    If wsWynik.grupy_obszary.Tables.Count > 0 Then
            '        dgvGrupy.DataSource = wsWynik.grupy_obszary.Tables(0)
            '        If dgvGrupy.Columns.Contains("grupa_id") Then dgvGrupy.Columns("grupa_id").Visible = False
            '        dtGrupyUzytkownika = wsWynik.grupy_obszary.Tables(0).Copy 'zachowujemy grupy w których był użytkownik
            '        tpGrupy.Text = "Grupy (" & dtGrupyUzytkownika.Rows.Count & ")"
            '    Else
            '        MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał listy grup użytkownika" & frmGlowna.kontaktIt, MsgBoxStyle.Critical)
            '        Me.Close()
            '    End If
            '    If wsWynik.grupy_obszary.Tables.Count > 1 Then
            '        dgvFunkcje.DataSource = wsWynik.grupy_obszary.Tables(1)
            '        If dgvFunkcje.Columns.Contains("funkcja_id") Then dgvFunkcje.Columns("funkcja_id").Visible = False
            '        If dgvFunkcje.Columns.Contains("WLACZ") Then dgvFunkcje.Columns("WLACZ").ReadOnly = True

            '        dtFunkcjeUzytkownika = wsWynik.grupy_obszary.Tables(1).Copy 'zachowujemy grupy w których był użytkownik
            '        tpFunkcje.Text = "Funkcje (" & dtFunkcjeUzytkownika.Rows.Count & ")"
            '    Else
            '        MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał listy funkcji użytkownika" & frmGlowna.kontaktIt, MsgBoxStyle.Critical)
            '        Me.Close()
            '    End If



            'Else
            '    'tryb dodawania nowego
            '    wczytajFunkcjeDostepne()
            '    Me.Text = "Tworzenie nowego użytkownika"
            '    txtImie.Focus()
            '    dtGrupyUzytkownika = New DataTable
            '    dtGrupyUzytkownika.Columns.Add("grupa_id")
            '    dtGrupyUzytkownika.Columns.Add("nazwa")
            '    dgvGrupy.DataSource = dtGrupyUzytkownika.Copy
            '    dgvGrupy.Columns("grupa_id").Visible = False
            '    tpGrupy.Text = "Grupy"
            '    dtFunkcjeUzytkownika = dtFunkcjeDostepne.Copy()
            '    Dim isCentrala As Boolean = False
            '    For Each dgvRowGrupa As DataGridViewRow In dgvGrupy.Rows
            '        If dgvRowGrupa.Cells("NAZWA").Value.ToString() = "CENTRALA" Then
            '            isCentrala = True
            '            Exit For
            '        End If
            '    Next
            '    If isCentrala = False Then
            '        'Wyszukuje funkcje do usuniecia 6-RaportyToolStripMenuItem; 8-AwizacjeToolStripMenuItem
            '        For Each rowUsunac As DataRow In dtFunkcjeUzytkownika.Select("(funkcja_id = 6) OR (funkcja_id = 8)")
            '            dtFunkcjeUzytkownika.Rows.Remove(rowUsunac)
            '        Next
            '        dtFunkcjeUzytkownika.AcceptChanges()
            '    End If
            '    dgvFunkcje.DataSource = dtFunkcjeUzytkownika.Copy()
            '    dgvFunkcje.Columns("funkcja_id").Visible = False
            '    dgvFunkcje.Columns("WLACZ").ReadOnly = True
            '    dgvFunkcje.Columns("WLACZ").ValueType = Type.GetType("System.Boolean")
            '    ''właczam wszystkie dostepne funkcje dla uzytkownika
            '    'For Each rowFunkcjeWlacz As DataGridViewRow In dgvFunkcje.Rows
            '    '    rowFunkcjeWlacz.Cells("WLACZ").Value = True
            '    'Next
            '    dgvFunkcje.Text = "Funkcje"
            '    btnAdresy.Enabled = False
        End If
        'btnZapisz.Enabled = False
        MyBase.Wlacz(frmGlowna.sesja)
        Return True

    End Function

    Private Sub frmEdytujSKU_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

        If Not SKUEdytujFiltry() Then
            Me.Close()
        End If
        If Not wczytaj() Then
            Me.Close()
        End If
        CtrImgGaleriaEdycjaSKU.inputIdSKU = intIdSKU
        CtrImgGaleriaEdycjaSKU.PokazZdjecie()
    End Sub

    Private Sub btnAnuluj_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAnuluj.Click
        Me.Close()
        intIdBlokady = -1
    End Sub

    'Private Function zapiszZmiany(ByVal pozostawBlokade As Boolean) As Boolean
    '    Dim bBylyZmiany As Boolean = False
    '    Dim objZnalezionyWiersz As DataRow 'wiersz odnaleziony
    '    Dim wlaczValue As Boolean 'wartosc wczytaj dal wiersza odnalezionego
    '    Dim dtKopia As DataTable
    '    Dim dtKopiaFunkcje As DataTable
    '    'Dim czy_maile As Integer
    '    'If chkMaile.Checked Then
    '    '    czy_maile = 1
    '    'Else
    '    '    czy_maile = 0
    '    'End If
    '    'If Not IsNumeric(txtTelkom.Text) Then
    '    '    MessageBox.Show("Telefon musi może zawierać tylko cyfry oraz być w formacie 500600700", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '    '    Exit Function
    '    'End If

    '    'najpierw sprawdzamy, czy zmienił się zestaw grup do którego należy użytkownik
    '    dtKopia = dtGrupyUzytkownika.Copy
    '    For Each wierszBiezacy As DataGridViewRow In dgvGrupy.Rows
    '        objZnalezionyWiersz = Nothing
    '        For Each wierszKopia As DataRow In dtKopia.Rows
    '            If wierszBiezacy.Cells("grupa_id").Value = wierszKopia.Item("grupa_id") Then
    '                objZnalezionyWiersz = wierszKopia
    '                Exit For
    '            End If
    '        Next
    '        If objZnalezionyWiersz Is Nothing Then
    '            bBylyZmiany = True
    '            Exit For
    '        Else
    '            dtKopia.Rows.Remove(objZnalezionyWiersz)
    '        End If
    '    Next
    '    If dtKopia.Rows.Count > 0 Then bBylyZmiany = True
    '    Dim usuniete As Integer = 0
    '    'potem sprawdzamy, czy zmienił się zestaw funkcji przynaleznych do użytkownika
    '    'dtFunkcjeUzytkownika.AcceptChanges()
    '    'dgvFunkcje.DataSource.AcceptChanges()
    '    'If Not dtFunkcjeUzytkownika.GetChanges() Is Nothing Then bBylyZmiany = True
    '    dtKopiaFunkcje = dtFunkcjeUzytkownika.Copy
    '    For Each wierszBiezacy As DataGridViewRow In dgvFunkcje.Rows
    '        objZnalezionyWiersz = Nothing
    '        For Each wierszKopia As DataRow In dtKopiaFunkcje.Rows
    '            If wierszBiezacy.Cells("funkcja_id").Value = wierszKopia.Item("funkcja_id") Then
    '                objZnalezionyWiersz = wierszKopia
    '                wlaczValue = wierszBiezacy.Cells("WLACZ").Value
    '                Exit For
    '            End If
    '        Next
    '        If objZnalezionyWiersz Is Nothing Then
    '            bBylyZmiany = True
    '            Exit For
    '        Else
    '            If wlaczValue = objZnalezionyWiersz.Item("WLACZ") Then
    '                dtKopiaFunkcje.Rows.Remove(objZnalezionyWiersz)
    '                usuniete = usuniete + 1
    '            Else
    '                dtKopiaFunkcje.Select(String.Format("funkcja_id = {0}", objZnalezionyWiersz.Item("funkcja_id")))(0).Item("WLACZ") = wlaczValue
    '                bBylyZmiany = True
    '            End If
    '        End If
    '    Next
    '    If dtKopiaFunkcje.Rows.Count > 0 Or dgvFunkcje.Rows.Count - usuniete > 0 Then bBylyZmiany = True

    '    'jeżeli edytujemy nowy rekord, to zaznaczamy flagę bBylyZmiany zawsze na True - żeby zawsze zapisać dane
    '    If intIdBlokady <= 0 Then bBylyZmiany = True

    '    'czy coś się zmieniło?
    '    If txtImie.Text <> strImie OrElse _
    '    txtNazwisko.Text <> strNazwisko OrElse _
    '    txtNazwa.Text <> strNazwa OrElse _
    '    txtTelkom.Text <> strTelkom OrElse _
    '    txtEmail.Text <> strEmail OrElse _
    '    txtLogin.Text <> strLogin OrElse _
    '    txtHaslo.Text.Length > 0 OrElse _
    '    txtHaslo2.Text.Length > 0 OrElse _
    '    intIdPrzelozonego <> intIdPrzelozonegoWczytane OrElse _
    '    intIdMagazynu <> intIdMagazynuWczytane OrElse _
    '    CType(czy_maile, Boolean) <> chkMaile.Checked OrElse _
    '    bBylyZmiany Then

    '        bBylyZmiany = True

    '        'weryfikacja poprawności danych
    '        If txtNazwa.Text.Length < 1 Then
    '            MsgBox("Pole 'nazwa wyświetlana' nie może być puste.", MsgBoxStyle.Exclamation)
    '            Return False
    '        End If
    '        If txtHaslo.Text <> txtHaslo2.Text Then
    '            MsgBox("Zawartość pól 'hasło' i 'hasło (powtórz)' jest różna.", MsgBoxStyle.Exclamation)
    '            Return False
    '        End If
    '        If intIdBlokady <= 0 AndAlso txtHaslo.Text.Length < 1 Then
    '            MsgBox("Musisz nadać hasło użytkownikowi.", MsgBoxStyle.Exclamation)
    '            Return False
    '        ElseIf txtHaslo.Text.Length > 0 AndAlso txtHaslo.Text.Length < 6 Then
    '            Dim wynik = MsgBox("Podane hasło jest krótsze niż 6 znaków. Czy na pewno stworzyć użytkownika z tak krótkim hasłem?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2)
    '            If wynik <> vbYes Then Return False
    '        End If
    '        If intIdMagazynu.ToString.Length = 0 Then
    '            Dim wynik = MsgBox("Brak przypisanego magazynu. Przypisz magazyn do użytkownika!", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2)
    '            If wynik <> vbYes Then Return False
    '        End If
    '        If dgvGrupy.Rows.Count < 1 Then
    '            Dim wynik = MsgBox("Uzytkownik nie należy do żadnej z grup. Czy na pewno tak ma być?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2)
    '            If wynik <> vbYes Then Return False
    '        End If
    '        If dgvFunkcje.Rows.Count < 1 Then
    '            Dim wynik = MsgBox("Uzytkownik nie posiada żadnej funkcji. Czy na pewno tak ma być?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2)
    '            If wynik <> vbYes Then Return False
    '        End If

    '        If chkMaile.Checked = True Then
    '            czy_maile = 1
    '        Else
    '            czy_maile = 0
    '        End If

    '       System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
    Dim ws As New wsCursorProf.CursorService
    'System.Net.ServicePointManager.Expect100Continue = False
    'ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
    'ws.Proxy.Credentials = CredentialCache.DefaultCredentials
    '    '        'ws.Url = frmGlowna.strWebservice
    '        Dim wsWynik As wsCursorProf.UserEdytujZapiszWynik
    '        Try
    '            Dim ds As New DataSet
    '            ds.Tables.Add(dgvGrupy.DataSource.Copy)
    '            dgvFunkcje.DataSource.AcceptChanges()
    '            ds.Tables.Add(dgvFunkcje.DataSource.Copy)
    '            Cursor = Cursors.WaitCursor
    '            Application.DoEvents()
    '            wsWynik = ws.UserEdytujZapisz(frmGlowna.sesja, txtImie.Text, txtNazwisko.Text, _
    '                txtNazwa.Text, txtTelkom.Text, txtEmail.Text, txtLogin.Text, txtHaslo.Text, _
    '                intIdPrzelozonego, intIdMagazynu, czy_maile, intIdBlokady, pozostawBlokade, ds)
    '            Cursor = Cursors.Default
    '        Catch ex As Exception
    '            Cursor = Cursors.Default
    '            MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical)
    '            Return False
    '        End Try
    '        If wsWynik.status < 0 Then
    '            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical)
    '            Return False
    '        ElseIf wsWynik.status > 0 Then
    '            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation)
    '        End If

    '        'modyfikujemy zmienne formy po udanym zapisie
    '        bOdswiezycRodzica = True
    '        intIdBlokady = wsWynik.blokada_id
    '        frmGlowna.lblStatus.Text = "Poprawnie zapisano dane użytkownika " & txtNazwa.Text
    '        frmGlowna.timer.Interval = 2000 'komunikat zniknie po 2s
    '        frmGlowna.timer.Start()
    '        txtHaslo.Text = ""
    '        txtHaslo2.Text = ""
    '        btnZastosuj.Enabled = False
    '        strImie = txtImie.Text
    '        txtImie.SelectAll()
    '        strNazwisko = txtNazwisko.Text
    '        strNazwa = txtNazwa.Text
    '        strTelkom = txtTelkom.Text
    '        strEmail = txtEmail.Text
    '        strLogin = txtLogin.Text
    '        intIdPrzelozonegoWczytane = intIdPrzelozonego
    '        dtGrupyUzytkownika = dgvGrupy.DataSource.Copy 'zachowujemy grupy w których był użytkownik
    '        tpGrupy.Text = "Grupy (" & dtGrupyUzytkownika.Rows.Count & ")"
    '        dtFunkcjeUzytkownika = dgvFunkcje.DataSource.Copy 'zachowujemy grupy w których był użytkownik
    '        tpFunkcje.Text = "Funkcje (" & dtFunkcjeUzytkownika.Rows.Count & ")"
    '        btnAdresy.Enabled = True

    '        'jeśli nie był pokazany label "status hasła" to znaczy, że zapisaliśmy nowego użytkownika
    '        If Not lblHasloStatus.Visible Then
    '            txtHaslo.Visible = False
    '            txtHaslo2.Visible = False
    '            lblHaslo2.Visible = False
    '            lblHasloStatus.Visible = True
    '            btnZmienHaslo.Visible = True
    '            btnUsunHaslo.Visible = True
    '            If txtHaslo.Text.Length > 0 Then
    '                lblHasloStatus.Text = "(ustawione)"
    '            Else
    '                lblHasloStatus.Text = "(puste)"
    '            End If
    '        End If

    '    Else
    '        bBylyZmiany = False
    '    End If

    '    If Not bBylyZmiany Then
    '        frmGlowna.lblStatus.Text = "Dane użytkownika " & txtNazwa.Text & " nie uległy zmianie, zapis do bazy nie był konieczny"
    '        frmGlowna.timer.Interval = 2000 'komunikat zniknie po 2s
    '        frmGlowna.timer.Start()
    '        If Not pozostawBlokade Then
    '           System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
    'Dim ws As New wsCursorProf.CursorService
    'System.Net.ServicePointManager.Expect100Continue = False
    'ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
    'ws.Proxy.Credentials = CredentialCache.DefaultCredentials
    '            'ws.Url = frmGlowna.strWebservice
    '            Try
    '                ws.UserEdytujAnuluj(frmGlowna.sesja, intIdBlokady)
    '            Catch ex As Exception
    '            End Try
    '        End If
    '    End If

    '    Return True
    'End Function

    Private Sub btnZastosuj_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnZapisz.Click
        zapiszZmiany()
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'If zapiszZmiany(False) Then
        '    intIdBlokady = -1 'blokada już zwolniona w funkcji zapiszZmiany()
        '    Me.Close()
        'End If
    End Sub



    'Private Sub btnZmienPrzelozonego_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnZmienPrzelozonego.Click
    '    Dim frm As New frmUzytkownicy
    '    frm.bTrybWyboruUzytkownika = True
    '    frm.ShowDialog()
    '    If frm.intIdWybranegoUzytkownika > 0 Then
    '        intIdPrzelozonego = frm.intIdWybranegoUzytkownika
    '        lblPrzelozony.Text = frm.strNazwaWybranegoUzytkownika
    '        btnZastosuj.Enabled = True
    '        btnOK.Enabled = True
    '    End If
    'End Sub

    'Private Sub btnZmienMagazyn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnZmienMagazyn.Click
    '    Dim frm As New frmMagazyny
    '    frm.ShowDialog()
    '    If frm.intIdWybranegoMagazynu > 0 Then
    '        intIdMagazynu = frm.intIdWybranegoMagazynu
    '        lblMagazyn.Text = frm.strNazwaWybranegoMagazynu
    '        btnZastosuj.Enabled = True
    '        btnOK.Enabled = True
    '    End If
    'End Sub

    'Private Sub btnZmienHaslo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnZmienHaslo.Click
    '    Dim frm As New frmUzytkownikHaslo
    '    frm.user_id = user_id_do_hasla
    '    frm.ShowDialog()
    '    If frm.strHaslo.Length > 0 Then
    '        'ustawiono hasło
    '        txtHaslo.Text = frm.strHaslo
    '        txtHaslo2.Text = frm.strHaslo
    '        lblHasloStatus.Text = "(ustawione)"
    '    End If
    '    frm.Dispose()
    'End Sub

    'Private Sub btnUsunHaslo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUsunHaslo.Click
    '    Dim frm As New frmUzytkownikHaslo
    '    frm.user_id = user_id_do_hasla
    '    frm.txtHaslo.Text = ""
    '    frm.txtHaslo2.Text = ""

    '    txtHaslo.Text = ""
    '    txtHaslo2.Text = ""
    '    lblHasloStatus.Text = "(puste)"
    '    btnZastosuj.Enabled = True
    '    btnOK.Enabled = True

    '   System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
    'Dim ws As New wsCursorProf.CursorService
    'System.Net.ServicePointManager.Expect100Continue = False
    'ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
    'ws.Proxy.Credentials = CredentialCache.DefaultCredentials
    '    Dim wsWynik As New wsCursorProf.ZmienHasloUzytkownikaWynik

    '    'ws.Url = frmGlowna.strWebservice
    '    'ws.Url = frmGlowna.strWebservice

    '    Try
    '        wsWynik = ws.ZmienHasloUzytkownika(frmGlowna.sesja, user_id_do_hasla, "")
    '    Catch ex As Exception
    '        wsWynik.status = -1
    '        wsWynik.status_opis = "Błąd komunikacji z serwerem: " & ex.Message & frmGlowna.kontaktIt
    '    End Try
    'End Sub

    'Private Sub btnAdresy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdresy.Click
    '    If intIdUzytkownika < 0 Then
    '        MsgBox("Błąd wewnętrzny systemu. Zmienna intIdUzytkownika jest nie ustawiona.", MsgBoxStyle.Critical)
    '        Exit Sub
    '    End If
    '    Dim frm As New frmAdresy
    '    frm.frmRodzic = Me
    '    frm.intIdUzytkownika = intIdUzytkownika
    '    frm.strNazwaUzytkownika = strNazwa
    '    If Me.Modal Then
    '        frm.ShowDialog()
    '    Else
    '        frm.MdiParent = frmGlowna
    '        frm.Show()
    '    End If
    'End Sub
    'Private Sub wczytajFunkcjeDostepne()
    '    If dtFunkcjeDostepne Is Nothing Then
    '       System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
    'Dim ws As New wsCursorProf.CursorService
    'System.Net.ServicePointManager.Expect100Continue = False
    'ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
    'ws.Proxy.Credentials = CredentialCache.DefaultCredentials
    '        'ws.Url = frmGlowna.strWebservice
    '        Dim wsWynik As wsCursorProf.FunkcjaListaWynik
    '        Try
    '            Cursor = Cursors.WaitCursor
    '            Application.DoEvents()
    '            wsWynik = ws.FunkcjaLista(frmGlowna.sesja)
    '            Cursor = Cursors.Default
    '        Catch ex As Exception
    '            Cursor = Cursors.Default
    '            MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical)
    '            Exit Sub
    '        End Try

    '        If wsWynik.status < 0 Then
    '            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical)
    '            Exit Sub
    '        ElseIf wsWynik.status > 0 Then
    '            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation)
    '        End If

    '        'testujemy, czy odpowiedź serwera jest prawidłowa (dwie tabele - pierwsza z nazwami grup i druga z hierarchią grup)
    '        If wsWynik.dane.Tables.Count < 1 Then
    '            MsgBox("Błąd systemu - serwer nie zwrócił tabeli z opisem funkcji" & frmGlowna.kontaktIt, MsgBoxStyle.Critical)
    '            Exit Sub
    '        End If

    '        dtFunkcjeDostepne = wsWynik.dane.Tables(0)
    '    End If
    'End Sub

    'Private Sub btnEdytujFunkcje_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdytujFunkcje.Click
    '    'wyświetlamy okno wyboru funkcji w trybie modalnym
    '    Dim frm As frmUzytkownikFunkcje = New frmUzytkownikFunkcje

    '    'w razie potrzeby wczytujemy listę grup dostępnych w systemie
    '    wczytajFunkcjeDostepne()
    '    frm.frmRodzic = Me
    '    If intIdBlokady > 1 Then
    '        frm.Text = "Edycja grup dla użytkownika " & strNazwa
    '    Else
    '        frm.Text = "Edycja grup dla nowo tworzonego użytkownika"
    '    End If
    '    frm.ShowDialog()
    'End Sub



    Private Sub txtCenaJM_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCenaJM.TextChanged
        btnZapisz.Enabled = True
    End Sub

    Private Sub txtGlebokosc_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtGlebokosc.TextChanged
        btnZapisz.Enabled = True
    End Sub

    Private Sub txtMaxIlosc_LostFocus(sender As Object, e As System.EventArgs) Handles txtMaxIlosc.LostFocus, txtMaxIloscZamowien.LostFocus
        'btnZapisz.Enabled = True
        If Not txtMaxIlosc.Text = "" And Not txtMaxIloscZamowien.Text = "" Then
            chkCzyLimitWydanOsoba.Checked = True
        Else
            If txtMaxIlosc.Focused = False And txtMaxIloscZamowien.Focused = False And (txtMaxIlosc.Text = "" And txtMaxIloscZamowien.Text = "") Then
                chkCzyLimitWydanOsoba.Checked = False
            End If
        End If
    End Sub

    Private Sub txtMaxIlosc_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMaxIlosc.TextChanged, txtMaxIloscZamowien.TextChanged
        btnZapisz.Enabled = True
        If Not txtMaxIlosc.Text = "" And Not txtMaxIloscZamowien.Text = "" Then
            chkCzyLimitWydanOsoba.Checked = True
        Else
            If txtMaxIlosc.Focused = False And txtMaxIloscZamowien.Focused = False And (txtMaxIlosc.Text = "" And txtMaxIloscZamowien.Text = "") Then
                chkCzyLimitWydanOsoba.Checked = False
            End If
        End If

    End Sub

    Private Sub txtOpisSKU_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtOpisSKU.TextChanged
        btnZapisz.Enabled = True
    End Sub


    Private Sub txtSzerokosc_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSzerokosc.TextChanged
        btnZapisz.Enabled = True
    End Sub

    Private Sub txtWaga_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtWaga.TextChanged
        btnZapisz.Enabled = True
    End Sub

    Private Sub txtWysokosc_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtWysokosc.TextChanged
        btnZapisz.Enabled = True
    End Sub

    Private Sub cmbMarka_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbMarka.SelectedValueChanged
        btnZapisz.Enabled = True
    End Sub

    Private Sub cmbJM_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbJM.SelectedValueChanged
        btnZapisz.Enabled = True
    End Sub

    Private Sub cmbBranza_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbBranza.SelectedValueChanged
        btnZapisz.Enabled = True
    End Sub

    Private Sub chkCzyMoznaZamawiac_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkCzyMoznaZamawiac.CheckedChanged
        btnZapisz.Enabled = True
    End Sub

    Private Sub zapiszZmiany()
        If bylyZmiany() = True Then
            System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
            Dim ws As New wsCursorProf.CursorService
            System.Net.ServicePointManager.Expect100Continue = False
            ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
            ws.Proxy.Credentials = CredentialCache.DefaultCredentials
            'ws.Url = frmGlowna.strWebservice
            Dim wsWynik As wsCursorProf.SKUEdytujZapiszWynik
            'user_id_do_hasla = intIdUzytkownika
            Try

                Dim zdjecie As Byte()
                Dim zdjecieNazwa As String = String.Empty
                Dim czyNowosci As Integer = 0
                Dim czyLimit As Integer = 0
                Dim czyZamawiac As Integer = 0
                Dim zdjecieZm As Integer = 0

                If zdjecieZmieniono = True Then
                    zdjecieZm = 1
                End If

                If chkNowosc.Checked = True Then
                    czyNowosci = 1
                End If

                If chkCzyMoznaZamawiac.Checked = True Then
                    czyZamawiac = 1
                End If

                If chkCzyLimitWydanOsoba.Checked = True Then
                    czyLimit = 1
                End If

                If File.Exists(sciezka) Then
                    zdjecie = File.ReadAllBytes(sciezka)
                    zdjecieNazwa = Path.GetFileName(sciezka)
                Else
                    zdjecie = Nothing
                End If

                'Dim maxIlosc As Integer
                'Dim maxIloscInt64 As Integer
                'If txtMaxIlosc.Text = "" Then
                '    maxIlosc = 0
                'ElseIf Not Integer.TryParse(txtMaxIlosc.Text, maxIlosc) Then
                '    If Int64.TryParse(txtMaxIlosc.Text, maxIloscInt64) Then
                '        MsgBox("Przekroczono maksymalną wartość dla ilości w jednym zamówieniu: " & Integer.MaxValue & "!", MsgBoxStyle.Critical, Me.Text)
                '        txtMaxIlosc.Focus()
                '        Exit Sub
                '    Else
                '        MsgBox("Maksymalna ilość w jednym zamówieniu musi być liczbą całkowitą dodatnią!", MsgBoxStyle.Critical, Me.Text)
                '        txtMaxIlosc.Focus()
                '        Exit Sub
                '    End If
                'ElseIf maxIlosc < 0 Then
                '    MsgBox("Maksymalna ilość w jednym zamówieniu musi być liczbą całkowitą dodatnią!", MsgBoxStyle.Critical, Me.Text)
                '    txtMaxIlosc.Focus()
                '    Exit Sub
                'End If

                'Dim maxIloscZamowien As Integer
                'Dim maxIloscZamowienInt64 As Int64
                'If txtMaxIloscZamowien.Text = "" Then
                '    maxIloscZamowien = 0
                'ElseIf Not Integer.TryParse(txtMaxIloscZamowien.Text, maxIloscZamowien) Then
                '    If Int64.TryParse(txtMaxIloscZamowien.Text, maxIloscZamowienInt64) Then
                '        MsgBox("Przekroczono maksymalną wartość dla maksymalnej ilości zamówień: " & Integer.MaxValue & "!", MsgBoxStyle.Critical, Me.Text)
                '        txtMaxIloscZamowien.Focus()
                '        Exit Sub
                '    Else
                '        MsgBox("Maksymalna ilość zamówień musi być liczbą całkowitą dodatnią!", MsgBoxStyle.Critical, Me.Text)
                '        txtMaxIloscZamowien.Focus()
                '        Exit Sub
                '    End If
                'ElseIf maxIloscZamowien < 0 Then
                '    MsgBox("Maksymalna ilość zamówień musi być liczbą całkowitą dodatnią!", MsgBoxStyle.Critical, Me.Text)
                '    txtMaxIloscZamowien.Focus()
                '    Exit Sub
                'End If

                'Dim limitLogistyczny As Integer
                'Dim limitLogistycznyInt64 As Int64
                'If txtLimitLogistyczny.Text = "" Then
                '    limitLogistyczny = 0
                'ElseIf Not Integer.TryParse(txtLimitLogistyczny.Text, limitLogistyczny) Then
                '    If Int64.TryParse(txtLimitLogistyczny.Text, limitLogistycznyInt64) Then
                '        MsgBox("Przekroczono maksymalną wartość dla limitu logistycznego: " & Integer.MaxValue & "!", MsgBoxStyle.Critical, Me.Text)
                '        txtLimitLogistyczny.Focus()
                '        Exit Sub
                '    Else
                '        MsgBox("Limit logistyczny musi być liczbą całkowitą dodatnią!", MsgBoxStyle.Critical, Me.Text)
                '        txtLimitLogistyczny.Focus()
                '        Exit Sub
                '    End If
                'ElseIf limitLogistyczny < 0 Then
                '    MsgBox("Limit logistyczny musi być liczbą całkowitą dodatnią!", MsgBoxStyle.Critical, Me.Text)
                '    txtLimitLogistyczny.Focus()
                '    Exit Sub
                'End If

                Cursor = Cursors.WaitCursor
                Application.DoEvents()

                wsWynik = ws.SKUEdytujZapisz(frmGlowna.sesja, intIdBlokady, zdjecie, zdjecieNazwa, zdjecieZm, txtOpisSKU.Text, _
                                             cmbMarka.GetItemText(cmbMarka.SelectedItem), cmbBranza.GetItemText(cmbBranza.SelectedItem), _
                                             txtCenaJM.Text, cmbJM.GetItemText(cmbJM.SelectedItem), txtWysokosc.Text, txtSzerokosc.Text, _
                                             txtGlebokosc.Text, txtWaga.Text, IIf(txtMaxIlosc.Text = "", 0, txtMaxIlosc.Text), czyLimit, _
                                             czyZamawiac, czyNowosci, _
                                             cmbKategoria.GetItemText(cmbKategoria.SelectedItem), _
                                             txtOpisRozszerzony.Text, txtNazwaSKU.Text, IIf(txtMaxIloscZamowien.Text = "", 0, txtMaxIloscZamowien.Text), cmbTypOkresZamowien.SelectedValue, txtSztOpk.Text)
                Cursor = Cursors.Default
            Catch ex As Exception
                Cursor = Cursors.Default
                MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, "Karta produktu")
                Me.Close()
                Exit Sub
            End Try

            If wsWynik.status < 0 Then
                MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, "Karta produktu")
                Me.Close()
                Exit Sub
            ElseIf wsWynik.status > 0 Then
                MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, "Karta produktu")
            End If
            If wsWynik.status = 0 Then
                bOdswiezycRodzica = True
                If bZamykamyForme = False Then
                    bZapisano = True
                    Me.Close()
                End If
            End If
        End If
    End Sub

    Public Sub odswiezZdjecia()
        CtrImgGaleriaEdycjaSKU.PokazZdjecie()
    End Sub

    Private Function bylyZmiany() As Boolean
        If String.Compare(strOpisSKU, txtOpisSKU.Text, False) <> 0 Or _
            String.Compare(strOpisRozszerzony, txtOpisRozszerzony.Text, False) <> 0 Or _
            strCenaJM <> txtCenaJM.Text Or _
            strWysokosc <> txtWysokosc.Text Or _
            strSzerokosc <> txtSzerokosc.Text Or _
            strWaga <> txtWaga.Text Or _
            strGlebokosc <> txtGlebokosc.Text Or _
            strSztOpk <> txtSztOpk.Text Or _
            IIf(strMaxIlosc = "0", "", strMaxIlosc) <> IIf(txtMaxIlosc.Text = "0", "", txtMaxIlosc.Text) Or _
            IIf(strMaxIloscZamowien = "0", "", strMaxIloscZamowien) <> IIf(txtMaxIloscZamowien.Text = "0", "", txtMaxIloscZamowien.Text) Or _
            strBranza <> cmbBranza.GetItemText(cmbBranza.SelectedItem) Or _
            strJM <> cmbJM.GetItemText(cmbJM.SelectedItem) Or _
            intTypOkresZamowienid <> cmbTypOkresZamowien.SelectedValue Or _
            sciezka <> String.Empty Or _
            zdjecieZmieniono = True Or _
            bCzyLimit <> chkCzyLimitWydanOsoba.Checked Or _
            bCzyMoznaZamawiac <> chkCzyMoznaZamawiac.Checked Or _
            strMarka <> cmbMarka.GetItemText(cmbMarka.SelectedItem) Or _
            strKategoria <> cmbKategoria.GetItemText(cmbKategoria.SelectedItem) Or _
            bCzyNowosc <> chkNowosc.Checked Or _
            String.Compare(strNazwaSKU, txtNazwaSKU.Text, False) <> 0 Then
            Return czyPoprawneDane()
        Else
            If bZamykamyForme = False Then
                MsgBox("Dane produktu nie uległy zmianie, zapis do bazy nie był konieczny.", MsgBoxStyle.Information)
            End If
            Return False
        End If

    End Function

    Private Function czyPoprawneDane()
        If sciezka <> String.Empty And Not File.Exists(sciezka) Then
            MsgBox(String.Format("Nie znaleziono pliku '{0}'", sciezka), MsgBoxStyle.Critical, "Niepoprawna ścieżka")
            Return False
        End If
        Dim wynik As Decimal
        If Not Decimal.TryParse(txtCenaJM.Text, wynik) Then
            MsgBox("Wartość kosztu punktowego musi być liczbą", MsgBoxStyle.Critical, "Niepoprawny koszt punktowy")
            Return False
        End If
        If Not Decimal.TryParse(txtWaga.Text, wynik) Then
            MsgBox("Wartość wagi musi być liczbą", MsgBoxStyle.Critical, "Niepoprawna waga")
            Return False
        End If
        Dim wynikInt As Integer
        If Not Integer.TryParse(txtWysokosc.Text, wynikInt) Then
            MsgBox("Wartość wysokości musi być liczbą", MsgBoxStyle.Critical, "Niepoprawna wysokość")
            Return False
        End If
        If Not Integer.TryParse(txtSzerokosc.Text, wynikInt) Then
            MsgBox("Wartość szerokości musi być liczbą", MsgBoxStyle.Critical, "Niepoprawna szerokość")
            Return False
        End If
        If Not Integer.TryParse(txtGlebokosc.Text, wynikInt) Then
            MsgBox("Wartość głębokości musi być liczbą", MsgBoxStyle.Critical, "Niepoprawna głębokość")
            Return False
        End If
        If Not Integer.TryParse(txtSztOpk.Text, wynikInt) Then
            MsgBox("Ilość sztuk w opakowaniu zbiorczym musi być liczbą większą od zera", MsgBoxStyle.Critical, "Niepoprawna ilość sztuk w opakowaniu zbiorczym")
            Return False
        End If
        'If Not Integer.TryParse(txtMaxIlosc.Text, wynikInt) And chkCzyLimitWydanOsoba.Checked = True Then
        '    MsgBox("Wartość limitu musi być liczbą", MsgBoxStyle.Critical, "Niepoprawny limit")
        '    Return False
        'End If
        If chkCzyLimitWydanOsoba.Checked = True Then
            Dim maxIlosc As Integer
            Dim maxIloscInt64 As Integer
            If txtMaxIlosc.Text = "" Then
                MsgBox("Maksymalna ilość w jednym zamówieniu musi być liczbą całkowitą dodatnią!", MsgBoxStyle.Critical, Me.Text)
                txtMaxIlosc.Focus()
                Return False
            ElseIf Not Integer.TryParse(txtMaxIlosc.Text, maxIlosc) Then
                If Int64.TryParse(txtMaxIlosc.Text, maxIloscInt64) Then
                    MsgBox("Przekroczono maksymalną wartość dla ilości w jednym zamówieniu: " & Integer.MaxValue & "!", MsgBoxStyle.Critical, Me.Text)
                    txtMaxIlosc.Focus()
                    Return False
                Else
                    MsgBox("Maksymalna ilość w jednym zamówieniu musi być liczbą całkowitą dodatnią!", MsgBoxStyle.Critical, Me.Text)
                    txtMaxIlosc.Focus()
                    Return False
                End If
            ElseIf maxIlosc <= 0 Then
                MsgBox("Maksymalna ilość w jednym zamówieniu musi być liczbą całkowitą dodatnią!", MsgBoxStyle.Critical, Me.Text)
                txtMaxIlosc.Focus()
                Return False
            End If

            Dim maxIloscZamowien As Integer
            Dim maxIloscZamowienInt64 As Int64
            If txtMaxIloscZamowien.Text = "" Then
                MsgBox("Maksymalna ilość zamówień musi być liczbą całkowitą dodatnią!", MsgBoxStyle.Critical, Me.Text)
                txtMaxIloscZamowien.Focus()
                Return False
            ElseIf Not Integer.TryParse(txtMaxIloscZamowien.Text, maxIloscZamowien) Then
                If Int64.TryParse(txtMaxIloscZamowien.Text, maxIloscZamowienInt64) Then
                    MsgBox("Przekroczono maksymalną wartość dla maksymalnej ilości zamówień: " & Integer.MaxValue & "!", MsgBoxStyle.Critical, Me.Text)
                    txtMaxIloscZamowien.Focus()
                    Return False
                Else
                    MsgBox("Maksymalna ilość zamówień musi być liczbą całkowitą dodatnią!", MsgBoxStyle.Critical, Me.Text)
                    txtMaxIloscZamowien.Focus()
                    Return False
                End If
            ElseIf maxIloscZamowien <= 0 Then
                MsgBox("Maksymalna ilość zamówień musi być liczbą całkowitą dodatnią!", MsgBoxStyle.Critical, Me.Text)
                txtMaxIloscZamowien.Focus()
                Return False
            End If
        End If
        Dim limitLogistyczny As Integer
        Dim limitLogistycznyInt64 As Int64

        Return True
    End Function

    Private Sub chkCzyLimitWydanOsoba_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCzyLimitWydanOsoba.CheckedChanged
        If chkCzyLimitWydanOsoba.Checked = True Then
            txtMaxIlosc.Text = strMaxIlosc
            txtMaxIlosc.Enabled = True
            txtMaxIloscZamowien.Text = strMaxIloscZamowien
            txtMaxIloscZamowien.Enabled = True
            cmbTypOkresZamowien.Enabled = True
        Else
            txtMaxIlosc.Text = ""
            txtMaxIlosc.Enabled = False
            txtMaxIloscZamowien.Text = ""
            txtMaxIloscZamowien.Enabled = False
            cmbTypOkresZamowien.Enabled = False
        End If
    End Sub

    Private Sub btnEdycjaGalerii_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdycjaGalerii.Click
        Dim frm As New frmGaleria
        frm.inputIdSKU = intIdSKU
        frm.nazwa_sku = strNazwaSKU
        frm.sku = strNumerSKU
        frm.frmRodzic = Me
        'frm.MdiParent = frmGlowna
        frm.ShowDialog()


    End Sub

    Private Sub txtOpisRozszerzony_TextChanged(sender As Object, e As System.EventArgs) Handles txtOpisRozszerzony.TextChanged
        btnZapisz.Enabled = True
    End Sub



    Private Sub txtOpisRozszerzony_VisibleChanged(sender As Object, e As System.EventArgs) Handles txtOpisRozszerzony.VisibleChanged
        lblOpisRozszerzony.Visible = txtOpisRozszerzony.Visible
    End Sub

    Private Sub txtNazwaSku_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtNazwaSKU.TextChanged
        btnZapisz.Enabled = True
    End Sub


End Class