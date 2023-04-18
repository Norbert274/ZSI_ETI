Imports System.Threading
Imports System.Reflection
Imports System.Text
Imports System.Data
Imports System.Linq
Imports System.IO
Imports CursorProfClient.My.Resources
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraTreeList.Nodes


Public Class ctrStan
    Public intIdMagazynu As Integer = -1
    'Public intIdGrupy As Integer = -1
    Private numerEkranu As Integer 'numer bie¿¹cego ekranu
    Private iloscEkranow As Integer 'iloœæ ekranów przy bie¿¹cym filtrze
    Private bReagujNaComboIloscNaStronie As Boolean = False
    Private bReagujNaCheckBoxHeader As Boolean = True
    Private bReagujNaComboMagazyn As Boolean = False
    Private wsWynik As wsCursorProf.StanWynik
    Private wsWynikFiltr As wsCursorProf.SkuStanFiltryWynik
    Public zaznacz As Boolean = False
    Public dgvMultiSelect As Boolean = True
    Public dtSkuFiltred As DataTable
    Private DodajeFiltry As Boolean = False
    Private checkingMarka As Boolean = False
    Public bTrybWyboruSKU As Boolean = False 'jeœli True, po dwukliku u¿ytkownik zostanie zwrócony do zmiennej intIdWybranegoUzytkownika kakaka
    Public intIdSKU As Integer = -1
    Public strNazwaSKU As String = ""
    Dim Parametry As New ZmienneGlobalne
    Public idMagazyn As Integer = Parametry.idMagazyn
    Private dtDane As DataTable
    Private dtHistoriaSku As DataTable
    Private strSKUclicked As String
    Private branzaCheked As Boolean = False
    Private markaCheked As Boolean = False
    Private grupaCheked As Boolean = False
    Private kategoriaCheked As Boolean = False
    Private xsize As Integer
    Private ysize As Integer
    'Private frm As New frmGaleriaDialog
    Public frmRodzic As Form
    Private czy_otwarta_frmGaleria As Boolean = False
    Private nazwa_col As String = ""
    Private col_index As Integer
    Private row_index As Integer
    Private b_czy_juz_byl_mouse_move_nad_wybrana_komorka As Boolean = False
    Private selectionChanged As Boolean = False
    Private ctrLoaded As Boolean = False
    Private bTylkoEdycjaGalerii As Boolean = False
    Private CzyWczytacDane As Boolean = False
    Private bZmianaStrony As Boolean = False
	Public bStanDlaKoszykINV As Boolean = False
    Const CONST_SKU_ID As String = "sku_id"
    Const CONST_SKU As String = "sku"
    Const CONST_SKU_NAZWA As String = "sku_nazwa"
    Const CONST_NAZWA As String = "Nazwa"
    Const CONST_JM As String = "J.M."
    'Const CONST_KOSZTPUNKTOWY As String = "koszt punktowy"
    Const CONST_GRUPA_ID As String = "grupa_id"
    Const CONST_GRUPA_NAZWA As String = "grupa"
    Const CONST_ZDJECIE_MINIATURA As String = "Zdjecie_miniatura"
    Const CONST_DOSTEPNE As String = "Dostepne"
    Const CONST_AKTYWNY As String = "Aktywny"
    Const CONST_KATEGORIA As String = "KATEGORIA"



    Private _dtSelectedRows As DataTable 'Tu przechowywane sa wybrane wiersze (na potrzeby stronicowania i filtrowania po stronie SQL)

    Public Property dtSelectedRows() As DataTable
        Get
            If _dtSelectedRows Is Nothing Then
                _dtSelectedRows = New DataTable
                _dtSelectedRows.Columns.Add(CONST_SKU_ID)
                _dtSelectedRows.Columns.Add(CONST_SKU)
                _dtSelectedRows.Columns.Add(CONST_SKU_NAZWA)
                _dtSelectedRows.Columns.Add(CONST_JM)
                '_dtSelectedRows.Columns.Add(CONST_KOSZTPUNKTOWY)
                _dtSelectedRows.Columns.Add(CONST_GRUPA_ID)
                _dtSelectedRows.Columns.Add(CONST_GRUPA_NAZWA)

                'Przeniesione z frmStan - specyficzne dla klienta
                _dtSelectedRows.Columns.Add("SZEROKOŒÆ")
                _dtSelectedRows.Columns.Add("WYSOKOŒÆ")
                _dtSelectedRows.Columns.Add("G£ÊBOKOŒÆ")
                _dtSelectedRows.Columns.Add("wielokrotnosc")
                _dtSelectedRows.Columns.Add("Obj_kart")

                'Przeniesione z przydziel do grupy
                _dtSelectedRows.Columns.Add("id_grupy_towarowe")
                _dtSelectedRows.Columns.Add("akcja_id")

                lblIloscZaznaczonych.Text = String.Format("Zaznaczonych: {0}", _dtSelectedRows.Rows.Count)
            End If

            Return _dtSelectedRows
        End Get
        Set(ByVal value As DataTable)
            _dtSelectedRows = value

        End Set
    End Property

    Public Sub czy_otwarta_Galeria(ByVal b As Boolean)
        czy_otwarta_frmGaleria = b
        b_czy_juz_byl_mouse_move_nad_wybrana_komorka = b
    End Sub
    Private Function UstawFiltry() As Boolean

        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials

        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynikFiltr = ws.SkuStanFiltry(frmGlowna.sesja)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("B³¹d komunikacji z serwerem. " & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegó³y b³êdu:" & ex.Message, MsgBoxStyle.Critical, "Ustawienie filtrów")
            Return False
        End Try
        If wsWynikFiltr.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, "Ustawienie filtrów")
            Return False
        ElseIf wsWynikFiltr.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, "Ustawienie filtrów")
        End If
        'Marka
        DodajeFiltry = True
        If wsWynikFiltr.dane.Tables.Count > 0 Then
            markaCheked = True
            listMarka.Items.Add("NIEOKREŒLONA", True)
            For Each row As DataRow In wsWynikFiltr.dane.Tables(0).Rows
                If Not row.Item(0).ToString = "NIEOKREŒLONA" Then
                    listMarka.Items.Add(row.Item("WARTOSC").ToString, True)
                End If
            Next
            markaCheked = False
        Else
            MsgBox("B³¹d wewnêtrzny systemu. Serwer nie przes³a³ listy dostêpnych marek." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, "Ustawienie filtrów")
            DodajeFiltry = False
            Return False
        End If
        'Branza
        If wsWynikFiltr.dane.Tables.Count > 1 Then
            listRodzaj.Items.Add("NIEOKREŒLONA", True)
            For Each row As DataRow In wsWynikFiltr.dane.Tables(1).Rows
                If Not row.Item(0).ToString = "NIEOKREŒLONA" Then
                    listRodzaj.Items.Add(row.Item("WARTOSC").ToString, True)
                End If
            Next
        Else
            MsgBox("B³¹d wewnêtrzny systemu. Serwer nie przes³a³ listy dostêpnych bran¿." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, "Ustawienie filtrów")
            DodajeFiltry = False
            Return False
        End If
        'Grupy
        If wsWynikFiltr.dane.Tables.Count > 2 AndAlso wsWynikFiltr.dane.Tables(2).Columns.Contains("GRUPA_ID") AndAlso wsWynikFiltr.dane.Tables(2).Columns.Contains("NADRZEDNA_ID") AndAlso wsWynikFiltr.dane.Tables(2).Columns.Contains("WARTOSC") Then

            treeListGrupy.OptionsSelection.MultiSelect = True
            treeListGrupy.OptionsView.ShowCheckBoxes = True

            'treeListGrupy.ch
            treeListGrupy.KeyFieldName = "GRUPA_ID"
            treeListGrupy.ParentFieldName = "NADRZEDNA_ID"
            treeListGrupy.DataSource = wsWynikFiltr.dane.Tables(2)
            treeListGrupy.Columns("WARTOSC").OptionsColumn.AllowEdit = False
            treeListGrupy.CheckAll()
            treeListGrupy.ExpandAll()
        Else
            MsgBox("B³¹d wewnêtrzny systemu. Serwer nie przes³a³ listy dostêpnych grup. Lub przes³a³ zbyt ma³o kolumn" & frmGlowna.kontaktIt, MsgBoxStyle.Critical, "Ustawienie filtrów")
            DodajeFiltry = False
            Return False
        End If
        'kategorie
        If wsWynikFiltr.dane.Tables.Count > 3 Then
            listKategoria.Items.Add("NIEOKREŒLONA", True)
            For Each row As DataRow In wsWynikFiltr.dane.Tables(3).Rows
                If Not row.Item(0).ToString = "NIEOKREŒLONA" Then
                    listKategoria.Items.Add(row.Item("WARTOSC").ToString, True)
                End If
            Next
        Else
            MsgBox("B³¹d wewnêtrzny systemu. Serwer nie przes³a³ listy dostêpnych kategorii." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, "Ustawienie filtrów")
            DodajeFiltry = False
            Return False
        End If
        DodajeFiltry = False
        Return True
    End Function
    Private Sub FixGridColumns(ByVal sortowanieKolumna As String, ByVal sortowanieNarastajaco As Boolean)

        For Each gcolumn As DataGridViewColumn In dgv.Columns
            Select Case UCase(gcolumn.Name)

                Case UCase(CONST_ZDJECIE_MINIATURA)
                    'Moim zdaniem szybsza metoda
                    Dim BrakZdjecia As Image
                    BrakZdjecia = My.Resources.Brak_zdjecia
                    Dim originalWidth As Integer = BrakZdjecia.Width
                    Dim originalHeight As Integer = BrakZdjecia.Height
                    Dim newHeight As Integer = dgv.RowTemplate.Height
                    Dim newWidth As Integer = (CSng(newHeight) / CSng(originalHeight)) * originalWidth
                    gcolumn.DefaultCellStyle.NullValue = ResizeImage(BrakZdjecia, New Size(newWidth, newHeight))
                    Exit Select
                Case UCase(CONST_SKU)
                    gcolumn.Width = 100
                    Exit Select
                Case UCase(CONST_NAZWA)
                    gcolumn.Width = 300
                    Exit Select
                Case UCase(CONST_GRUPA_NAZWA)
                    If dgv.Columns.Contains(CONST_NAZWA) Then
                        gcolumn.DisplayIndex = dgv.Columns(CONST_NAZWA).DisplayIndex + 1
                    End If
                    Exit Select
                Case UCase(CONST_JM)
                    gcolumn.HeaderText = "J.m"
                    If dgv.Columns.Contains(CONST_DOSTEPNE) Then
                        gcolumn.DisplayIndex = dgv.Columns(CONST_DOSTEPNE).DisplayIndex + 1
                    End If

                Case UCase("Brand")
                    If dgv.Columns.Contains(CONST_AKTYWNY) Then
                        gcolumn.HeaderText = "Brand"
                        gcolumn.DisplayIndex = dgv.Columns(CONST_AKTYWNY).DisplayIndex + 1
                    End If
                Case UCase(CONST_AKTYWNY)

                    Exit Select
                Case UCase(CONST_KATEGORIA)
                    gcolumn.HeaderText = "Kategoria"
                    If dgv.Columns.Contains(CONST_AKTYWNY) Then
                        gcolumn.DisplayIndex = dgv.Columns(CONST_AKTYWNY).DisplayIndex + 2
                    End If
                    Exit Select
                Case UCase("GRUPA_MATERIA£OWA")
                    gcolumn.HeaderText = "Grupa_materia³owa"
                    gcolumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader

                    If dgv.Columns.Contains(CONST_AKTYWNY) Then
                        gcolumn.DisplayIndex = dgv.Columns(CONST_AKTYWNY).DisplayIndex + 3
                    End If
                    Exit Select
                Case UCase("kategoria towaru")
                    gcolumn.Visible = False
                    Exit Select
                Case UCase("sku_skr")
                    gcolumn.Visible = False
                    Exit Select
                Case UCase(CONST_SKU_ID)
                    gcolumn.Visible = False
                    Exit Select
                Case UCase(CONST_GRUPA_ID)
                    gcolumn.Visible = False
                    Exit Select
                Case UCase("CZY_NOWOSC")
                    gcolumn.Visible = False
                    Exit Select
                Case UCase("CZY_ZAMAWIAC")
                    gcolumn.Visible = False
                    Exit Select
                    'Case UCase("RODZAJ")
                    '    gcolumn.Visible = False
                    '    Exit Select
                Case UCase("Zdjecie")
                    gcolumn.Visible = False
                    Exit Select


            End Select
            gcolumn.SortMode = DataGridViewColumnSortMode.Programmatic


            If gcolumn.Name = sortowanieKolumna And sortowanieKolumna <> String.Empty Then
                If sortowanieNarastajaco Then
                    gcolumn.HeaderCell.SortGlyphDirection = SortOrder.Ascending
                    'dgv.Sort(gcolumn, System.ComponentModel.ListSortDirection.Ascending)
                Else
                    gcolumn.HeaderCell.SortGlyphDirection = SortOrder.Descending
                    'dgv.Sort(gcolumn, System.ComponentModel.ListSortDirection.Descending)
                End If
            End If
        Next
    End Sub
    'Private Function wczytaj(ByVal Zzerowymi As Integer) As Boolean
    Private Function wczytaj() As Boolean
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials

        'ws.Url = frmGlowna.strWebservice
        'Dim wsWynik As wsJJ.StanWynik
        Dim sortowanieKolumna As String = ""
        Dim sortowanieNarastajaco As Boolean = True
        Dim intIdWybranegoWiersza = Nothing
        Dim intNumerWybranejKolumny = Nothing

        'zapisujemy id wybranego wiersza, jeœli jakiœ jest wybrany
        If Not dgv.CurrentCell Is Nothing And bZmianaStrony = False Then
            intIdWybranegoWiersza = dgv.Rows(dgv.CurrentCell.RowIndex).Cells(CONST_SKU_ID).Value
            intNumerWybranejKolumny = dgv.CurrentCell.ColumnIndex
        End If

        'czy sortujemy po jakiejœ kolumnie?
        For Each dgvKolumna As DataGridViewColumn In dgv.Columns
            If dgvKolumna.HeaderCell.SortGlyphDirection <> SortOrder.None Then
                sortowanieKolumna = dgvKolumna.Name
                If dgvKolumna.HeaderCell.SortGlyphDirection = SortOrder.Ascending Then
                    sortowanieNarastajaco = True
                Else
                    sortowanieNarastajaco = False
                End If
                Exit For
            End If
        Next

        'odczyt listy z serwera
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.Stan(frmGlowna.sesja, intIdMagazynu, GrupyHierarchia, Marki, Branze, Kategorie, _
                              tbNumer.Text, tbNazwa.Text, txtNumerEkranu.Text, cmbIloscNaStronie.Text, _
                              sortowanieKolumna, sortowanieNarastajaco, chbDostepne.Checked, chbNowosci.Checked)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("B³¹d komunikacji z serwerem. " & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegó³y b³êdu:" & ex.Message, MsgBoxStyle.Critical, "Wczytanie stanów magazynowych")
            Return False
        End Try
        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, "Wczytanie stanów magazynowych")
            Return False
        ElseIf wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, "Wczytanie stanów magazynowych")
        End If
        'czyszczenie kontrolek przed wype³nieniem
        dgv.DataSource = Nothing
        dgv.Columns.Clear()
        dgv.Controls.Clear()
        dgv.RowTemplate.Height = 55   ' wysokoœæ wiersza - od niej zalezy jak bedzie wygladalo zdjecie 

        iloscEkranow = wsWynik.ilosc_stron

        'wype³nienie kontrolek wynikami
        If wsWynik.dane.Tables.Count > 0 Then
            'wype³nienie gridu
            dtDane = wsWynik.dane.Tables(0).Copy
            'FiltrujBezWczytaj()
            dgv.DataSource = dtDane


            'dodajemy kolumnê z checkboxami
            dgv.Columns.Insert(0, New DataGridViewCheckBoxColumn)
            dgv.Columns(0).Width = 22
            dgv.Columns(0).Frozen = True
            Dim dgvCell As DataGridViewCheckBoxCell
            Dim bWszystkie As Boolean = True
            If dtSelectedRows.Rows.Count = 0 Then bWszystkie = False
            Dim testSQL As String 'Warunek istnienia wiersza = to samo sku_id i ta sama grupa_id

            For Each row As DataGridViewRow In dgv.Rows
                dgvCell = DirectCast(row.Cells(0), DataGridViewCheckBoxCell)
                'zaznaczamy wiersze, które powinny byæ wybrane (przy zmianie strony lub filtrowaniu)
                testSQL = String.Format("[{0}]='{1}' AND [{2}]='{3}'", CONST_SKU_ID, row.Cells(CONST_SKU_ID).Value, CONST_GRUPA_ID, row.Cells(CONST_GRUPA_ID).Value)
                If dtSelectedRows.Select(testSQL).Length > 0 Then
                    dgvCell.Value = True And dgvMultiSelect
                Else
                    bWszystkie = False
                    dgvCell.Value = zaznacz And dgvMultiSelect
                End If
            Next
            If dgvMultiSelect = True Then

                Dim rect As Rectangle = dgv.GetCellDisplayRectangle(0, -1, True)
                rect.X = rect.Location.X + 3
                rect.Y = rect.Location.Y + 3
                Dim checkboxHeader As New CheckBox()
                RemoveHandler checkboxHeader.CheckedChanged, AddressOf dgv_checkboxHeaderCheckedChanged
                checkboxHeader.Name = "checkboxHeader"
                checkboxHeader.Size = New Size(18, 18)
                checkboxHeader.BackColor = Color.WhiteSmoke
                checkboxHeader.Location = rect.Location
                checkboxHeader.Checked = zaznacz And dgvMultiSelect
                checkboxHeader.Checked = bWszystkie
                dgv.Controls.Add(checkboxHeader)
                AddHandler checkboxHeader.CheckedChanged, AddressOf dgv_checkboxHeaderCheckedChanged

            End If


            '    If dgv.Columns.Contains("Sku") Then dgv.Columns("Sku").Width = 100
            '    If dgv.Columns.Contains("Nazwa") Then dgv.Columns("Nazwa").Width = 300


            '    If dgv.Columns.Contains("Grupa") Then
            '        If dgv.Columns.Contains("Nazwa") Then
            '            dgv.Columns("Grupa").DisplayIndex = dgv.Columns("Nazwa").DisplayIndex + 1
            '       End If
            '   End If

            '    If dgv.Columns.Contains("J.M.") Then
            '        dgv.Columns("J.M.").HeaderText = "J.m."
            '        If dgv.Columns.Contains("Dostepne") Then
            '            dgv.Columns("J.M.").DisplayIndex = dgv.Columns("Dostepne").DisplayIndex + 1
            '        End If
            '    End If

            '    If dgv.Columns.Contains("Aktywny") Then
            '        If dgv.Columns.Contains("MARKA") Then
            '            dgv.Columns("MARKA").HeaderText = "Brand"
            '            dgv.Columns("MARKA").DisplayIndex = dgv.Columns("Aktywny").DisplayIndex + 1
            '        End If
            '    End If

            '    If dgv.Columns.Contains("KATEGORIA") Then
            '        dgv.Columns("KATEGORIA").HeaderText = "Kategoria"
            '        If dgv.Columns.Contains("Aktywny") Then
            '            dgv.Columns("KATEGORIA").DisplayIndex = dgv.Columns("Aktywny").DisplayIndex + 2
            '        End If
            '    End If

            '    If dgv.Columns.Contains("kategoria towaru") Then dgv.Columns("kategoria towaru").Visible = False
            '    If dgv.Columns.Contains("sku_skr") Then dgv.Columns("sku_skr").Visible = False
            '    If dgv.Columns.Contains("sku_id") Then dgv.Columns("sku_id").Visible = False
            '    If dgv.Columns.Contains("grupa_id") Then dgv.Columns("grupa_id").Visible = False
            '    If dgv.Columns.Contains("CZY_NOWOSC") Then dgv.Columns("CZY_NOWOSC").Visible = False
            '    If dgv.Columns.Contains("CZY_ZAMAWIAC") Then dgv.Columns("CZY_ZAMAWIAC").Visible = False
            '    If dgv.Columns.Contains("RODZAJ") Then dgv.Columns("RODZAJ").Visible = False
            '    If dgv.Columns.Contains("Zdjecie") Then dgv.Columns("Zdjecie").Visible = False
            FixGridColumns(sortowanieKolumna, sortowanieNarastajaco) 'Tu s¹ ustawienia kolumn            
            If dgv.Columns.Contains("Opis rozszerzony") And wsWynik.opis_rozszerzony = 0 Then dgv.Columns("Opis rozszerzony").Visible = False
            If dgv.Columns.Contains("Aktywny") And bStanDlaKoszykINV = True Then dgv.Columns("Aktywny").Visible = False

            If bStanDlaKoszykINV = True Then
                If dgv.Columns.Contains("Aktywny") Then dgv.Columns("Aktywny").Visible = False
                If dgv.Columns.Contains("Opis rozszerzony") Then dgv.Columns("Opis rozszerzony").Visible = False
                If dgv.Columns.Contains("Zdjecie_miniatura") Then dgv.Columns("Zdjecie_miniatura").Visible = False
                If dgv.Columns.Contains("MARKA") Then dgv.Columns("MARKA").Visible = False
            End If


            '  For Each kolumna As DataGridViewColumn In dgv.Columns
            '      kolumna.SortMode = DataGridViewColumnSortMode.Programmatic
            '      If kolumna.HeaderText = sortowanieKolumna And sortowanieKolumna <> String.Empty Then
            '          If sortowanieNarastajaco Then
            '              kolumna.HeaderCell.SortGlyphDirection = SortOrder.Ascending
            '              'dgv.Sort(kolumna, System.ComponentModel.ListSortDirection.Ascending)
            '          Else
            '              kolumna.HeaderCell.SortGlyphDirection = SortOrder.Descending
            '              'dgv.Sort(kolumna, System.ComponentModel.ListSortDirection.Descending)
            '          End If
            '      End If
            '  Next

            'podœwietlamy ostatnio wybran¹ komórkê, jeœli któraœ by³a wybrana
            If Not intIdWybranegoWiersza Is Nothing Then
                For Each wiersz As DataGridViewRow In dgv.Rows
                    If wiersz.Cells(CONST_SKU_ID).Value = intIdWybranegoWiersza Then
                        'sprawdzamy, czy mo¿emy ustawiæ kursor w kolumnie w której by³ ostatnio
                        '(czy wróci³o tyle kolumn)
                        If dgv.Columns.Count - 1 < intNumerWybranejKolumny Then
                            'ustawiamy kursor w ostatniej kolumnie
                            dgv.CurrentCell = dgv.Rows(wiersz.Index).Cells(dgv.Columns.Count - 1)
                        Else
                            'ustawiamy kursor w kolumnie, w której sta³
                            dgv.CurrentCell = dgv.Rows(wiersz.Index).Cells(intNumerWybranejKolumny)
                        End If
                    End If
                Next
            End If

        Else
            MsgBox("B³¹d wewnêtrzny systemu. Serwer nie przes³a³ listy dostêpnych towarów." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, "Wczytanie stanów magazynowych")
            Return False
        End If

        intIdMagazynu = wsWynik.magazyn_id
        If intIdMagazynu < 0 Then
            MsgBox("B³¹d wewnêtrzny systemu. Serwer nie przes³a³ identyfikatora magazynu, którego zawartoœæ pokazuje." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, "Wczytanie stanów magazynowych")
            Return False
        End If
        If wsWynik.dane.Tables.Count > 1 Then
            'wype³nienie combo Magazyn
            'bReagujNaComboMagazyn = False
            'cmbMagazyn.ComboBox.ValueMember = "magazyn_id"
            'cmbMagazyn.ComboBox.DisplayMember = "nazwa"
            'cmbMagazyn.ComboBox.DataSource = wsWynik.dane.Tables(1)
            'cmbMagazyn.ComboBox.SelectedValue = intIdMagazynu
            'bReagujNaComboMagazyn = True
        Else
            MsgBox("B³¹d wewnêtrzny systemu. Serwer nie przes³a³ listy magazynów." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, "Wczytanie stanów magazynowych")
            Return False
        End If

        If wsWynik.ilosc_stron > 1 Then
            iloscEkranow = wsWynik.ilosc_stron
        Else
            iloscEkranow = 1
        End If

        numerEkranu = txtNumerEkranu.Text
        lblIloscEkranow.Text = "z " & iloscEkranow

        If CInt(txtNumerEkranu.Text) > iloscEkranow And wsWynik.dane.Tables(0).Rows.Count = 0 Then
            txtNumerEkranu.Text = 1
            wczytaj()
        End If

        UstawPrzyciskiNawigacji()

        'If Not dtSkuFiltred Is Nothing Then
        '    filtruj()
        'End If
        Return True
    End Function

    Public Sub odswiezListy()
        wczytaj()
    End Sub

    Private Sub dgv_CellClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellClick
        If e.RowIndex < 0 Then
            Exit Sub
        End If
        Dim col_index As Integer = e.ColumnIndex

        For Each col As DataGridViewColumn In dgv.Columns
            If col.Index = col_index Then
                nazwa_col = col.Name
                If e.RowIndex > -1 Then
                    intIdSKU = dgv.Rows(e.RowIndex).Cells(CONST_SKU_ID).Value
                End If
            End If
        Next

        If nazwa_col.ToUpper = UCase(CONST_ZDJECIE_MINIATURA) Then
            If e.RowIndex > -1 And Not IsDBNull(dgv.Rows(e.RowIndex).Cells(nazwa_col).Value) Then
                intIdSKU = dgv.Rows(e.RowIndex).Cells(CONST_SKU_ID).Value

                Dim frm As New frmGaleriaDialog
                Dim PoLewej As Boolean = False
                frm.sku_id = intIdSKU
                frm.wczytaj_zdjecia()

                xsize = frm.Width
                ysize = frm.Height


                Dim lewy As Integer
                lewy = dgv.Left + dgv.GetColumnDisplayRectangle(col_index, True).Left - xsize
                If lewy < 0 Then
                    lewy = dgv.Left + dgv.GetColumnDisplayRectangle(col_index, True).Right
                End If

                frm.Left = lewy
                If e.RowIndex > -1 Then
                    frm.Top = dgv.Top + dgv.GetRowDisplayRectangle(e.RowIndex, True).Top
                End If

                If frm.Top + ysize > dgv.Bottom Then
                    frm.Top = dgv.Bottom - ysize
                End If

                frm.ShowDialog(Me)
            End If
        End If
    End Sub


    Private Sub dgv_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellContentClick

        If e.RowIndex < 0 Then
            Exit Sub
        End If
        If e.ColumnIndex = 0 Then

            Dim dgvCell As DataGridViewCheckBoxCell = DirectCast(dgv.Rows(e.RowIndex).Cells(e.ColumnIndex), DataGridViewCheckBoxCell)
            If dgvMultiSelect = False Then
                If dgvCell.Value Is Nothing OrElse Not dgvCell.Value Then
                    For Each dgvWiersz As DataGridViewRow In dgv.Rows
                        If dgvWiersz.Cells(0).Value = True Then
                            dgvWiersz.Cells(0).Value = False
                        End If
                    Next

                    ZaznaczenieWiersza(dgv.Rows(e.RowIndex), True)

                    dgvCell.Value = True
                Else
                    ZaznaczenieWiersza(dgv.Rows(e.RowIndex), False)
                    dgvCell.Value = False
                End If
            Else
                If dgvCell.Value Is Nothing OrElse Not dgvCell.Value Then
                    If dgv.Rows(e.RowIndex).Cells(CONST_AKTYWNY).Value = "NIE" Then
                        MsgBox("Nie mo¿na wybraæ nieaktywnych produktów", MsgBoxStyle.Information, "Komunikat")
                        dgvCell.Value = False
                        ZaznaczenieWiersza(dgv.Rows(e.RowIndex), False)
                        'bReagujNaCheckBoxHeader = False
                        'DirectCast(dgv.Controls.Find("checkboxHeader", True)(0), CheckBox).Checked = False
                        'bReagujNaCheckBoxHeader = True
                    Else
                        dgvCell.Value = True
                        ZaznaczenieWiersza(dgv.Rows(e.RowIndex), True)
                        'zaznacz = True
                        'wczytaj()
                        'czy wszystkie s¹ zaznaczone?
                        Dim bWszystkie As Boolean = True
                        For Each dgvWiersz As DataGridViewRow In dgv.Rows
                            If dgvWiersz.Cells(CONST_SKU_ID).Value < 0 Then Continue For
                            If dgvWiersz.Cells(0).Value Is Nothing OrElse dgvWiersz.Cells(0).Value = False Then
                                bWszystkie = False
                                Exit For
                            End If
                        Next
                        If bWszystkie Then
                            bReagujNaCheckBoxHeader = False
                            DirectCast(dgv.Controls.Find("checkboxHeader", True)(0), CheckBox).Checked = True
                            bReagujNaCheckBoxHeader = True
                        End If
                    End If
                Else
                    dgvCell.Value = False
                    ZaznaczenieWiersza(dgv.Rows(e.RowIndex), False)
                    bReagujNaCheckBoxHeader = False
                    DirectCast(dgv.Controls.Find("checkboxHeader", True)(0), CheckBox).Checked = False
                    bReagujNaCheckBoxHeader = True
                End If
            End If
        End If

    End Sub

    Private Sub ZaznaczenieWiersza(ByVal dgvRow As DataGridViewRow, ByVal czyZaznaczenie As Boolean)

        If dgvMultiSelect = False Then 'Jesli nie ma byc multiselect to zawsze 1 wiec tylko dodajemy
            dtSelectedRows.Clear()
            dtSelectedRows.AcceptChanges()
        End If

        Dim testSQL As String 'Warunek istnienia wiersza = to samo sku_id i ta sama grupa_id
        testSQL = String.Format("[{0}]='{1}' AND [{2}]='{3}'", CONST_SKU_ID, dgvRow.Cells(CONST_SKU_ID).Value, CONST_GRUPA_ID, dgvRow.Cells(CONST_GRUPA_ID).Value)

        Dim rowDR As DataRow
        'Jesli nie bylo to trzeba dodac (jest stronicowanie wiec trzeba trzymac wszystko zeby zaznaczyc przy zmianie strony i przy zapisie)
        If dtSelectedRows.Select(testSQL).Length > 0 Then 'Jesli juz bylo to pomijamy lub usuwamy jesli to odznaczanie

            If czyZaznaczenie = False Then
                rowDR = dtSelectedRows.Select(testSQL).FirstOrDefault()
                rowDR.Delete()
            End If

        Else 'Jesli nie bylo to trzeba dodac (jest stronicowanie wiec trzeba trzymac wszystko zeby zaznaczyc przy zmianie strony i przy zapisie)
            rowDR = dtSelectedRows.NewRow
            rowDR.Item(CONST_SKU_ID) = dgvRow.Cells(CONST_SKU_ID).Value
            rowDR.Item(CONST_SKU) = dgvRow.Cells(CONST_SKU).Value
            rowDR.Item(CONST_SKU_NAZWA) = dgvRow.Cells(CONST_NAZWA).Value
            rowDR.Item(CONST_JM) = dgvRow.Cells(CONST_JM).Value
            'rowDR.Item(CONST_KOSZTPUNKTOWY) = dgvRow.Cells(CONST_KOSZTPUNKTOWY).Value
            rowDR.Item(CONST_GRUPA_ID) = dgvRow.Cells(CONST_GRUPA_ID).Value
            rowDR.Item(CONST_GRUPA_NAZWA) = dgvRow.Cells(CONST_GRUPA_NAZWA).Value
            dtSelectedRows.Rows.Add(rowDR)
        End If
        dtSelectedRows.AcceptChanges()
        lblIloscZaznaczonych.Text = String.Format("Zaznaczonych: {0}", dtSelectedRows.Rows.Count)
    End Sub



    Private Sub dgv_checkboxHeaderCheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        'dgv(0, i).Value = DirectCast(dgv.Controls.Find("checkboxHeader", True)(0), CheckBox).Checked
        'dgv.EndEdit()
        If bReagujNaCheckBoxHeader Then
            If dgv.Rows.Count > 0 Then
                'czy wszystkie checkboxy zaznaczone ?
                Dim bWszystkie As Boolean = True
                For Each dgvWiersz As DataGridViewRow In dgv.Rows
                    If dgvWiersz.Cells(CONST_SKU_ID).Value < 0 Then Continue For
                    If dgvWiersz.Cells(0).Value Is Nothing OrElse dgvWiersz.Cells(0).Value = False AndAlso dgvWiersz.Cells(CONST_AKTYWNY).Value = "TAK" Then
                        bWszystkie = False
                        Exit For
                    End If
                Next

                ZaznaczenieWszystkich(bWszystkie)

                If bWszystkie Then
                    sender.Checked = False
                Else
                    sender.Checked = True
                End If
            End If
        End If
    End Sub

    Private Sub ZaznaczenieWszystkich(ByVal bOdznaczWszystkie As Boolean)
        'zaznaczamy lub odznaczamy
        For Each dgvWiersz As DataGridViewRow In dgv.Rows
            If dgvWiersz.Cells(CONST_AKTYWNY).Value = "TAK" Then
                If bOdznaczWszystkie Then
                    'odznaczamy
                    dgvWiersz.Cells(0).Value = False
                Else
                    'zaznaczamy
                    dgvWiersz.Cells(0).Value = True
                    ZaznaczenieWiersza(dgvWiersz, True)  'Robione pojedynczo
                End If
            End If
        Next
        If bOdznaczWszystkie Then
            dtSelectedRows.Clear() 'Czyszczenie selekcji
            lblIloscZaznaczonych.Text = String.Format("Zaznaczonych: {0}", dtSelectedRows.Rows.Count)
        End If

    End Sub

    Private Sub btnOdswiez_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOdswiez.Click
        wczytaj()
    End Sub

    Private Sub txtNumerEkranu_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtNumerEkranu.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim intNumerEkranu As Integer
            If Not Integer.TryParse(txtNumerEkranu.Text, intNumerEkranu) Then
                MsgBox("Numer ekranu musi byæ liczb¹", MsgBoxStyle.Critical, "Niepoprawny numer ekranu")
                Return
            End If
            If intNumerEkranu = numerEkranu Then Return 'u¿ytkownik nie zmieni³ numeru ekranu, nic nie robimy
            wczytaj()
        End If
        If e.KeyCode = Keys.Escape Then
            txtNumerEkranu.Text = numerEkranu
        End If
    End Sub

    Private Sub txtNumerEkranu_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtNumerEkranu.Validating
        Dim intNumerEkranu As Integer
        If Not Integer.TryParse(txtNumerEkranu.Text, intNumerEkranu) Then
            MsgBox("Numer ekranu musi byæ liczb¹", MsgBoxStyle.Critical, "Niepoprawny numer ekranu")
            e.Cancel = True
            Return
        End If
        If txtNumerEkranu.Text <> numerEkranu Then
            MsgBox("Je¿eli chcesz przejœæ na wpisany ekran, naciœnij Enter, jeœli chcesz wyjœæ z tego pola - naciœnij Escape", MsgBoxStyle.Exclamation, "Niepoprawny numer ekranu")
            e.Cancel = True
            Return
        End If
    End Sub

    Private Sub btnNastepny_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNastepny.Click
        If txtNumerEkranu.Text >= iloscEkranow Then
            'MsgBox("To jest ostatni ekran. Nie mo¿esz przejœæ do nastêpnego ekranu.", MsgBoxStyle.Exclamation, "Ostatni numer ekranu")
            Return
        End If
        txtNumerEkranu.Text = txtNumerEkranu.Text + 1
        bZmianaStrony = True
        wczytaj()
        bZmianaStrony = False
        UstawPrzyciskiNawigacji()
    End Sub

    Private Sub btnOstatni_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOstatni.Click
        If txtNumerEkranu.Text = iloscEkranow Then
            'MsgBox("To jest ostatni ekran.", MsgBoxStyle.Exclamation, "Ostatni numer ekranu")
            Return
        End If
        txtNumerEkranu.Text = iloscEkranow
        bZmianaStrony = True
        wczytaj()
        bZmianaStrony = False
        UstawPrzyciskiNawigacji()
    End Sub

    Private Sub btnPoprzedni_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPoprzedni.Click
        If txtNumerEkranu.Text = 1 Then
            'MsgBox("To jest pierwszy ekran. Nie mo¿esz przejœæ do poprzedniego ekranu.", MsgBoxStyle.Exclamation, "Pierwszy numer ekranu")
            Return
        End If
        txtNumerEkranu.Text = txtNumerEkranu.Text - 1
        bZmianaStrony = True
        wczytaj()
        bZmianaStrony = False
        UstawPrzyciskiNawigacji()
    End Sub

    Private Sub btnPoczatek_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPoczatek.Click
        If txtNumerEkranu.Text = 1 Then
            'MsgBox("To jest pierwszy ekran.", MsgBoxStyle.Exclamation, "Pierwszy numer ekranu")
            Return
        End If
        txtNumerEkranu.Text = 1
        bZmianaStrony = True
        wczytaj()
        bZmianaStrony = False
        UstawPrzyciskiNawigacji()
    End Sub

    Private Sub UstawPrzyciskiNawigacji()
        If iloscEkranow = CInt(txtNumerEkranu.Text) And CInt(txtNumerEkranu.Text) = "1" Then
            btnNastepny.Enabled = False
            btnOstatni.Enabled = False
            btnPoprzedni.Enabled = False
            btnPoczatek.Enabled = False
        ElseIf iloscEkranow = CInt(txtNumerEkranu.Text) And CInt(txtNumerEkranu.Text) <> "1" Then
            btnNastepny.Enabled = False
            btnOstatni.Enabled = False
            btnPoprzedni.Enabled = True
            btnPoczatek.Enabled = True
        ElseIf iloscEkranow > CInt(txtNumerEkranu.Text) And CInt(txtNumerEkranu.Text) = "1" Then
            btnNastepny.Enabled = True
            btnOstatni.Enabled = True
            btnPoprzedni.Enabled = False
            btnPoczatek.Enabled = False
        ElseIf iloscEkranow > CInt(txtNumerEkranu.Text) And CInt(txtNumerEkranu.Text) <> "1" Then
            btnNastepny.Enabled = True
            btnOstatni.Enabled = True
            btnPoprzedni.Enabled = True
            btnPoczatek.Enabled = True
        Else
            btnNastepny.Enabled = False
            btnOstatni.Enabled = False
            btnPoprzedni.Enabled = True
            btnPoczatek.Enabled = True
        End If
    End Sub

    Private Sub cmbIloscNaStronie_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbIloscNaStronie.SelectedIndexChanged
        If bReagujNaComboIloscNaStronie Then
            txtNumerEkranu.Text = 1
            wczytaj()
        End If
    End Sub

    Private Sub dgv_CellContentDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellContentDoubleClick

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
            MsgBox("B³¹d komunikacji z serwerem. " & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegó³y b³êdu:" & ex.Message, MsgBoxStyle.Critical, "Sprawdzenie uprawnieñ u¿ytkownika")
            Return Nothing
            Exit Function
        End Try
        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, "Sprawdzenie uprawnieñ u¿ytkownika")
            Return Nothing
            Exit Function
        ElseIf wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, "Sprawdzenie uprawnieñ u¿ytkownika")
        End If
        Return wsWynik.dane.Tables(0)

    End Function

    Private Sub dgv_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellDoubleClick
        If e.RowIndex <> -1 Then
            If bTylkoEdycjaGalerii Then
                Dim f As New frmGaleria
                f.inputIdSKU = intIdSKU
                f.nazwa_sku = strNazwaSKU
                f.sku = dgv.Rows(e.RowIndex).Cells(CONST_SKU).Value
                f.frmRodzic = frmRodzic
                'frm.MdiParent = frmGlowna
                f.ShowDialog()
            Else
                Dim frm As frmEdytujSKU = New frmEdytujSKU
                frm.WindowState = FormWindowState.Normal
                frm.frmRodzic = frmRodzic
                If e.RowIndex > -1 Then
                    frm.intIdSKU = dgv.Rows(e.RowIndex).Cells(CONST_SKU_ID).Value
                End If
                frm.MdiParent = frmGlowna
                frm.Show()
            End If
        End If
    End Sub



    Private Sub dgv_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgv.ColumnHeaderMouseClick
        Dim sortowanieKolumna As String
        Dim sortowanieRosnaco As Boolean
        Dim kolumna As DataGridViewColumn
        Dim kolumnaKliknieta As DataGridViewColumn

        kolumnaKliknieta = dgv.Columns(e.ColumnIndex)
        If kolumnaKliknieta.Name <> "dostepne" And kolumnaKliknieta.Name <> "dostepne_podwladni" Then
            sortowanieKolumna = kolumnaKliknieta.Name

            For Each kolumna In dgv.Columns
                If kolumnaKliknieta.Name <> kolumna.Name Then kolumna.HeaderCell.SortGlyphDirection = SortOrder.None
            Next
            If kolumnaKliknieta.HeaderCell.SortGlyphDirection = SortOrder.Ascending Then
                kolumnaKliknieta.HeaderCell.SortGlyphDirection = SortOrder.Descending
                sortowanieRosnaco = False
            Else
                kolumnaKliknieta.HeaderCell.SortGlyphDirection = SortOrder.Ascending
                sortowanieRosnaco = True
            End If

            'wype³niamy grid od nowa
            txtNumerEkranu.Text = "1"
            If iloscEkranow > 1 Then 'po co wczytywaæ ponownie jeœli mamy 1 ekran??
                wczytaj()
            Else
                FixGridColumns(sortowanieKolumna, sortowanieRosnaco)
            End If

            'szukamy, czy w wyniku jest przed chwil¹ klikniêta kolumna (po nazwie)
            For Each kolumna In dgv.Columns
                If kolumna.Name = sortowanieKolumna Then
                    'jest - rysujemy jej "sorting glyph"
                    If sortowanieRosnaco Then
                        kolumna.HeaderCell.SortGlyphDirection = SortOrder.Ascending
                    Else
                        kolumna.HeaderCell.SortGlyphDirection = SortOrder.Descending
                    End If
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Sub cmbMagazyn_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If bReagujNaComboMagazyn Then
            intIdMagazynu = Parametry.idMagazyn 'cmbMagazyn.ComboBox.SelectedValue
            wczytaj()
        End If
    End Sub


    Private Sub dgv_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgv.DoubleClick
        ''zbieramy co zaznaczono
        'Dim dtSku As New DataTable
        'Dim id_wiersza As String = dgv.CurrentCell.Value

        'dtSku.Columns.Add("sku_id")
        'dtSku.Columns.Add("sku")
        'dtSku.Columns.Add("nazwa")

        'For Each dgvRow As DataGridViewRow In dgv.Rows
        '    If id_wiersza.Contains(dgvRow.Cells("sku_id").Value) Or id_wiersza.Contains(dgvRow.Cells("sku").Value) Or id_wiersza.Contains(dgvRow.Cells("nazwa").Value) Then
        '        dtSku.Rows.Add(dgvRow.Cells("sku_id").Value, dgvRow.Cells("sku").Value, dgvRow.Cells("nazwa").Value)
        '    End If
        'Next

        'Dim dr As DataRow
        'dr = dtSku.Rows(0)

        'Dim frm As New frmZdjecieSku
        'frm.MdiParent = frmRodzic.MdiParent
        'frm.sku_id_zdjecie = dr.Item("sku_id")
        'frm.sku_nazwa_zdjecie = dr.Item("nazwa")
        'frm.sku_zdjecie = dr.Item("sku")
        'frm.Show()
    End Sub

    Private Sub dgv_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgv.KeyDown
        'If e.Control And e.KeyCode = Keys.C Then
        '    dgv.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithAutoHeaderText
        '    If dgv.GetCellCount(DataGridViewElementStates.Selected) > 0 Then
        '        Clipboard.SetDataObject(dgv.GetClipboardContent())
        '        Dim str As String = Clipboard.GetText()
        '        Dim w1250 As Encoding = Encoding.GetEncoding("windows-1250")
        '        Dim [unicode] As Encoding = Encoding.Unicode
        '        Dim unicodeBytes As Byte() = [unicode].GetBytes(str)
        '        Dim w1250Bytes As Byte() = Encoding.Convert([unicode], w1250, unicodeBytes)
        '        Dim w1250Chars(w1250.GetCharCount(w1250Bytes, 0, w1250Bytes.Length) - 1) As Char
        '        w1250.GetChars(w1250Bytes, 0, w1250Bytes.Length, w1250Chars, 0)
        '        Dim w1250String As New String(w1250Chars)
        '        Clipboard.SetDataObject(w1250String)
        '        e.Handled = True
        '    End If
        'End If
    End Sub

    Public Sub ShowStan()
        'inicjalizacja ustawieñ kontrolek
        If Not UstawFiltry() Then
            frmRodzic.Close()
        End If

        numerEkranu = 1
        txtNumerEkranu.Text = numerEkranu
        cmbIloscNaStronie.SelectedIndex = 1
        bReagujNaComboIloscNaStronie = True
        DodajeFiltry = True
        chbDostepne.Checked = True ' zaznaczam aby pokazaæ stany <> 0
        chbNowosci.Checked = False
        DodajeFiltry = False

        Dim dtTable As New DataTable
        dtTable = SprawdzUprawnienia(frmGlowna.sesja)
        If dtTable.Rows.Count > 0 Then
            For Each r As DataRow In dtTable.Rows
                If r.Item("FUNKCJE_ID") = 24 Then
                    bTylkoEdycjaGalerii = True
                    btnPodzielGrupa.Visible = False
                    btnDoKoszyka.Visible = False
                End If

                '' czy user ma uprawnienie do raportu historii materia³u
                If r.Item("FUNKCJE_ID") = 28 Then
                    menu_kontekstowe.Items("HistoriaProduktuToolStripMenuItem").Visible = True
                End If

            Next
        End If

        'inicjalizacja ustawieñ kontrolek
        cmbIloscNaStronie.Text = 25

        If Not wczytaj() Then frmRodzic.Close()

    End Sub

    Public Function getStany() As DataTable
        Dim dtStanySelected As DataTable
        dtStanySelected = dgv.DataSource.Table.Copy()
        For i As Integer = dtStanySelected.Rows.Count - 1 To 0 Step -1
            If Not dgv.Rows.Item(i).Cells(0).Value Then
                dtStanySelected.Rows.RemoveAt(i)
            End If
        Next
        Return dtStanySelected
    End Function

    Public Sub setStan(ByVal sku_id As Integer, ByVal id_grupy_towarowe As Integer, ByVal akcja_id As Integer)
        For Each row As DataRow In dgv.DataSource.Rows
            If row.Item(CONST_SKU_ID) = sku_id And row.Item("id_grupy_towarowe") = id_grupy_towarowe And row.Item("akcja_id") = akcja_id Then
                row.Item(0).value = True
            End If
        Next
    End Sub


    Private Sub ctrStan_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'inicjalizacja ustawieñ kontrolek
        ctrLoaded = False
        numerEkranu = 1
        txtNumerEkranu.Text = numerEkranu
        cmbIloscNaStronie.SelectedIndex = 1
        bReagujNaComboIloscNaStronie = True
        DodajeFiltry = True
        chbDostepne.Checked = True ' zaznaczam aby pokazaæ stany <> 0
        DodajeFiltry = False
        ctrLoaded = True
        'If Not wczytaj() Then frmRodzic.Close()
    End Sub


    Private Sub btnDoKoszyka_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDoKoszyka.Click
        'czy koszyk jest ju¿ otwarty?
		 If bStanDlaKoszykINV = True Then
            If frmGlowna.frmKoszykINV Is Nothing Then
                'Nie, koszyk nie jest otwarty - ³adujemy go
                Dim frmKoszykINV As New frmZamowienieINV
                frmKoszykINV.intIdZamowienia = -1
                frmKoszykINV.intIdMagazynu = Parametry.idMagazyn 'cmbMagazyn.ComboBox.SelectedValue
                frmKoszykINV.MdiParent = frmRodzic.MdiParent
                frmKoszykINV.frmRodzic = frmRodzic
                frmKoszykINV.strFunkcjaPowiadomieniaOGotowosci = "zamowienieINVGotoweGrupa"
                frmKoszykINV.Show()
            Else
                Dim m As MethodInfo() = frmRodzic.GetType.GetMethods()
                For licznik As Integer = 0 To m.GetUpperBound(0)
                    If m(licznik).Name = "zamowienieINVGotoweGrupa" Then
                        m(licznik).Invoke(frmRodzic, Nothing)
                    End If
                Next
            End If

        Else
            If frmGlowna.frmKoszyk Is Nothing Then
            'Nie, koszyk nie jest otwarty - ³adujemy go
            Dim frmKoszyk As New frmZamowienie
            frmKoszyk.intIdZamowienia = -1
            frmKoszyk.intIdMagazynu = Parametry.idMagazyn 'cmbMagazyn.ComboBox.SelectedValue
            frmKoszyk.MdiParent = frmRodzic.MdiParent
            frmKoszyk.frmRodzic = frmRodzic
            frmKoszyk.strFunkcjaPowiadomieniaOGotowosci = "zamowienieGotoweGrupa"
            frmKoszyk.Show()
	        Else
	            Dim m As MethodInfo() = frmRodzic.GetType.GetMethods()
	            For licznik As Integer = 0 To m.GetUpperBound(0)
	                If m(licznik).Name = "zamowienieGotoweGrupa" Then
	                    m(licznik).Invoke(frmRodzic, Nothing)
	                End If
	            Next
	        End If
        End If		

        

    End Sub

    Private Sub btnPodzielGrupa_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPodzielGrupa.Click
        'zbieramy co zaznaczono
        Dim dtSku As New DataTable
        'dtSku.Columns.Add(CONST_SKU_ID)
        'dtSku.Columns.Add("id_grupy_towarowe")
        'dtSku.Columns.Add("akcja_id")
        dtSku = dtSelectedRows.Copy
        'For Each dgvRow As DataGridViewRow In dgv.Rows
        '    If dgvRow.Cells(0).Value Then
        '        dtSku.Rows.Add(dgvRow.Cells(CONST_SKU_ID).Value)
        '    End If
        'Next

        If dtSku.Rows.Count < 1 Then
            MsgBox("Zaznacz przynajmniej jedn¹ pozycjê", MsgBoxStyle.Exclamation, "Zaznaczenie pozycji")
            Exit Sub
        End If

        'otwieramy okno frmPodzial
        Dim frm As New frmPodzialGrupa
        frm.MdiParent = frmRodzic.MdiParent
        frm.frmRodzic = frmRodzic
        frm.intIdMagazynu = intIdMagazynu
        frm.strMagazyn = Parametry.strMagazyn 'cmbMagazyn.ComboBox.Text
        frm.dtSku = dtSku
        frm.Show()


        'odznaczamy wszystkie checkboxy
        For Each dgvRow As DataGridViewRow In dgv.Rows
            If dgvRow.Cells(0).Value Then dgvRow.Cells(0).Value = False
        Next
        odznaczWszystkie()

    End Sub

    Public Sub odznaczWszystkie()

        bReagujNaCheckBoxHeader = False
        CType(dgv.Controls.Item("checkboxHeader"), CheckBox).Checked = False
        ZaznaczenieWszystkich(True)
        bReagujNaCheckBoxHeader = True

    End Sub

    Private Function Marki() As String
        Dim wynik As String = String.Empty
        Dim i As Integer
        For i = 0 To listMarka.Items.Count - 1
            If listMarka.GetItemChecked(i) Then
                wynik = wynik + "'" + listMarka.Items(i).Replace("'", "''") + "',"
            End If
        Next
        If wynik.Length > 0 Then
            wynik = wynik.Remove(wynik.Length - 1)
        End If
        Return wynik
    End Function

    Private Function Branze() As String
        Dim wynik As String = String.Empty
        Dim i As Integer
        For i = 0 To listRodzaj.Items.Count - 1
            If listRodzaj.GetItemChecked(i) Then
                wynik = wynik + "'" + listRodzaj.Items(i).ToString.Replace("'", "''") + "',"
            End If
        Next
        If wynik.Length > 0 Then
            wynik = wynik.Remove(wynik.Length - 1)
        End If
        Return wynik
    End Function

    Private wszystkieGrupyZaznaczone As Boolean = True
    Private Function GrupyHierarchia() As String
        Dim wynik As String = String.Empty
        wszystkieGrupyZaznaczone = True

        For Each node As TreeListNode In treeListGrupy.Nodes
            If node.Checked = True Then
                wynik = wynik + "'" + node.Item("WARTOSC").ToString.Replace("'", "''") + "',"
            ElseIf wszystkieGrupyZaznaczone = True Then
                wszystkieGrupyZaznaczone = False
            End If
            If node.Nodes.Count > 0 Then
                getSubNodes(node, wynik, wszystkieGrupyZaznaczone)
            End If
        Next

        If wynik.Length > 0 Then
            wynik = wynik.Remove(wynik.Length - 1)
        End If
        Return wynik
    End Function

    Private Sub getSubNodes(ByVal parentNode As TreeListNode, ByRef wynik As String, ByRef wszystkieGrupyZaznaczone As Boolean)
        For Each node As TreeListNode In parentNode.Nodes
            If node.Checked = True Then
                wynik = wynik + "'" + node.Item("WARTOSC").ToString.Replace("'", "''") + "',"
            ElseIf wszystkieGrupyZaznaczone = True Then
                wszystkieGrupyZaznaczone = False
            End If
            If node.Nodes.Count > 0 Then
                getSubNodes(node, wynik, wszystkieGrupyZaznaczone)
            End If
        Next
    End Sub


    Private Function Kategorie() As String
        Dim wynik As String = String.Empty
        Dim i As Integer
        For i = 0 To listKategoria.Items.Count - 1
            If listKategoria.GetItemChecked(i) Then
                wynik = wynik + "'" + listKategoria.Items(i).ToString.Replace("'", "''") + "',"
            End If
        Next
        If wynik.Length > 0 Then
            wynik = wynik.Remove(wynik.Length - 1)
        End If
        Return wynik
    End Function

    Private Sub tbNumer_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tbNumer.KeyDown
        If e.KeyCode = Keys.Enter Then
            wczytaj()
        End If
    End Sub

    Private Sub tbNazwa_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tbNazwa.KeyDown
        If e.KeyCode = Keys.Enter Then
            wczytaj()
        End If
    End Sub



    Public Sub SKUEdit(ByVal sku_id As Integer)
        Dim frm As frmEdytujSKU = New frmEdytujSKU
        'frm.frmRodzic = Me
        frm.WindowState = FormWindowState.Normal
        frm.intIdSKU = sku_id
        'If Me.Modal Then
        '    frm.ShowDialog()
        'Else
        frm.MdiParent = frmGlowna
        frm.Show()
        'End If
    End Sub

  
    Private Sub chbDostepne_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If DodajeFiltry = False Then
            'FiltrujBezWczytaj()
        End If
    End Sub

    Private Sub chbNowosci_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If DodajeFiltry = False Then
            'FiltrujBezWczytaj()
        End If
    End Sub

    Private Sub listMarka_ItemCheck(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles listMarka.ItemCheck
        If markaCheked = False And DodajeFiltry = False Then
            markaCheked = True
            selectionChanged = True
            listMarka.SetSelected(e.Index, True)
            listMarka.SetItemCheckState(e.Index, e.NewValue)
            'FiltrujBezWczytaj()
            markaCheked = False
            selectionChanged = False
        End If

    End Sub

    Private Sub listRodzaj_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles listRodzaj.ItemCheck
        If branzaCheked = False And DodajeFiltry = False Then
            branzaCheked = True
            selectionChanged = True
            listRodzaj.SetSelected(e.Index, True)
            listRodzaj.SetItemCheckState(e.Index, e.NewValue)
            'FiltrujBezWczytaj()
            branzaCheked = False
            selectionChanged = False
        End If
    End Sub

   

    Private Sub treeListGrupy_AfterCheckNode(sender As Object, e As DevExpress.XtraTreeList.NodeEventArgs) Handles treeListGrupy.AfterCheckNode
        If grupaCheked = False And DodajeFiltry = False Then
            grupaCheked = True
            selectionChanged = True
            'FiltrujBezWczytaj()
            grupaCheked = False
            selectionChanged = False
        End If

        If selectionChanged = False Then
            GrupyHierarchia() 'przy okazji sprawdza czy wszystko zaznaczone

            selectionChanged = True
            chkgrupaAll.Checked = wszystkieGrupyZaznaczone

            If ctrLoaded = True Then
                wczytaj()
            End If
            selectionChanged = False

        End If
    End Sub

  

    Private Sub listKategoria_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles listKategoria.ItemCheck
        If kategoriaCheked = False And DodajeFiltry = False Then
            kategoriaCheked = True
            selectionChanged = True
            listKategoria.SetSelected(e.Index, True)
            listKategoria.SetItemCheckState(e.Index, e.NewValue)
            'FiltrujBezWczytaj()
            kategoriaCheked = False
            selectionChanged = False
        End If
    End Sub

    Private Sub pokaz_galerie()

        If nazwa_col = "Zdjecie" Then
            Dim frm As New frmGaleriaDialog
            Dim PoLewej As Boolean = False
            frm.sku_id = intIdSKU
            frm.wczytaj_zdjecia()

            xsize = frm.Width
            ysize = frm.Height


            Dim lewy As Integer
            Dim _f_left As Integer
            Dim _f_top As Integer
            Dim _f_bottom As Integer

            For Each _f As Form In frmGlowna.MdiChildren
                If _f.Name = "frmStan" Then
                    _f_left = _f.Left
                    _f_top = _f.Top
                    _f_bottom = _f.Bottom

                End If

            Next
            lewy = _f_left + dgv.GetColumnDisplayRectangle(col_index, True).Left + dgv.Left + 10
            If lewy < 0 Then
                lewy = _f_left + dgv.GetColumnDisplayRectangle(col_index, True).Right
            End If

            frm.Left = lewy
            frm.Top = _f_top + dgv.GetRowDisplayRectangle(row_index, True).Top + 128


            'If PoLewej Then
            '    Dim lewy As Integer
            '    lewy = dgv.Left + dgv.GetColumnDisplayRectangle(col_index, False).Left - xsize
            '    If lewy < 0 Then
            '        lewy = dgv.Left + dgv.GetColumnDisplayRectangle(col_index, False).Right
            '    End If

            '    frm.Left = lewy
            '    frm.Top = dgv.Top + dgv.GetRowDisplayRectangle(e.RowIndex, False).Top
            'Else
            '    frm.Left = dgv.Left + dgv.GetColumnDisplayRectangle(col_index, False).Left
            '    frm.Top = dgv.Top + dgv.GetRowDisplayRectangle(e.RowIndex, False).Bottom
            'End If

            If ysize > _f_bottom - dgv.GetRowDisplayRectangle(row_index, True).Top - dgv.Top - _f_top Then
                frm.Top = frm.Top - ysize - 23
            End If

            'If frm.Top + ysize > dgv.Bottom Then
            '    frm.Top = dgv.Bottom - ysize
            'End If

            'frm.Visible = True
            'frm.MdiParent = frmGlowna
            frm.ShowDialog(Me)
            'czy_otwarta_frmGaleria = True
        End If
    End Sub

    Private Sub dgv_CellMouseLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellMouseLeave

    End Sub

    Private Sub VScrollBar1_Scroll(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles vscrolFiltr.Scroll
        pnlFiltr.Location = New Point(pnlFiltr.Location.X, 28 - (vscrolFiltr.Value - 28))
    End Sub



    Private Sub chkgrupaAll_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkgrupaAll.CheckedChanged
        If selectionChanged = False Then
            selectionChanged = True
            If chkgrupaAll.Checked = True Then
                treeListGrupy.CheckAll()
            Else
                treeListGrupy.UncheckAll()
            End If
            If ctrLoaded = True Then
                wczytaj()
            End If
            selectionChanged = False
        End If
    End Sub

    Private Sub chkMarkaAll_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMarkaAll.CheckedChanged
        If selectionChanged = False Then
            selectionChanged = True
            For i As Integer = 0 To listMarka.Items.Count - 1
                listMarka.SetItemChecked(i, chkMarkaAll.Checked)
            Next
            If ctrLoaded = True Then
                wczytaj()
            End If
            selectionChanged = False
        End If
    End Sub

    Private Sub chkBranzaAll_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkRodzajAll.CheckedChanged
        If selectionChanged = False Then
            selectionChanged = True
            For i As Integer = 0 To listRodzaj.Items.Count - 1
                listRodzaj.SetItemChecked(i, chkRodzajAll.Checked)
            Next
            If ctrLoaded = True Then
                wczytaj()
            End If
            selectionChanged = False
        End If
    End Sub

    Private Sub chkKategoriaAll_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkKategoriaAll.CheckedChanged
        If selectionChanged = False Then
            selectionChanged = True
            For i As Integer = 0 To listKategoria.Items.Count - 1
                listKategoria.SetItemChecked(i, chkKategoriaAll.Checked)
            Next
            If ctrLoaded = True Then
                wczytaj()
            End If
            selectionChanged = False
        End If
    End Sub

    Private Sub listMarka_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles listMarka.SelectedValueChanged
        If selectionChanged = False Then
            Dim selectedAll As Boolean = True
            For i As Integer = 0 To listMarka.Items.Count - 1
                If listMarka.GetItemChecked(i) = False Then
                    selectedAll = False
                    Exit For
                End If
            Next
            selectionChanged = True
            chkMarkaAll.Checked = selectedAll
            If ctrLoaded = True Then
                wczytaj()
            End If
            selectionChanged = False
        End If
    End Sub

    Private Sub listBranza_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles listRodzaj.SelectedValueChanged
        If selectionChanged = False Then
            Dim selectedAll As Boolean = True
            For i As Integer = 0 To listRodzaj.Items.Count - 1
                If listRodzaj.GetItemChecked(i) = False Then
                    selectedAll = False
                    Exit For
                End If
            Next
            selectionChanged = True
            chkRodzajAll.Checked = selectedAll
            If ctrLoaded = True Then
                wczytaj()
            End If
            selectionChanged = False
        End If
    End Sub

    Private Sub listKategoria_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles listKategoria.SelectedValueChanged
        If selectionChanged = False Then
            Dim selectedAll As Boolean = True
            For i As Integer = 0 To listKategoria.Items.Count - 1
                If listKategoria.GetItemChecked(i) = False Then
                    selectedAll = False
                    Exit For
                End If
            Next
            selectionChanged = True
            chkKategoriaAll.Checked = selectedAll
            If ctrLoaded = True Then
                wczytaj()
            End If
            selectionChanged = False
        End If
    End Sub


    Private Sub QueryTable_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles dgv.CellFormatting
        If e.Value IsNot Nothing And e.Value IsNot DBNull.Value Then
            If TypeOf dgv.Columns(e.ColumnIndex) Is DataGridViewImageColumn Then 'resizes images
                ' CType(dgv.Columns(e.ColumnIndex), DataGridViewImageColumn).ImageLayout = DataGridViewImageCellLayout.Stretch
                CType(dgv.Columns(e.ColumnIndex), DataGridViewImageColumn).ImageLayout = DataGridViewImageCellLayout.Zoom
                'DirectCast(dgv.Columns(e.ColumnIndex), DataGridViewImageColumn).DefaultCellStyle.NullValue = Nothing
            End If
        End If
    End Sub



    Private Function historia_materialu_wczytaj(ByVal sku As String) As Boolean

        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik As wsCursorProf.RaportHistoriiMaterialuWczytajWynik

        sku = "<row sku=""" & sku.ToString().Replace("&", "&amp;").Replace("'", "&apos;").Replace("""", "&quot;") & """/>"

        'odczyt z serwera
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.RaportHistoriiMaterialuWczytaj(frmGlowna.sesja, sku)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("B³¹d komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegó³y b³êdu:" & ex.Message, MsgBoxStyle.Critical, "Raport historii materia³u")
            Return False
        End Try
        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, "Raport historii materia³u")
            Return False
        End If

        'kontrola wyników
        If wsWynik.dane.Tables.Count > 0 Then
            If wsWynik.dane.Tables(0).Rows.Count < 1 Then
                MsgBox("Brak danych o historii materia³u dla SKU: " & sku, MsgBoxStyle.Information, "Brak danych")
                Return False
            End If
        Else
            MsgBox("B³¹d wewnêtrzny systemu. Serwer nie przes³a³ tabeli z histori¹ materia³u." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, "Raport historii materia³u")
            Return False
            Exit Function
        End If

        dtHistoriaSku = wsWynik.dane.Tables(0)

        Return True
    End Function


    Private Sub dgv_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles dgv.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then

            Dim ht As DataGridView.HitTestInfo
            ht = Me.dgv.HitTest(e.X, e.Y)
            'If ht.RowIndex < 0 Then Return
            strSKUclicked = dgv.Rows(ht.RowIndex).Cells("SKU").Value
            If ht.Type = DataGridViewHitTestType.Cell Then
                dgv.ContextMenuStrip = menu_kontekstowe
            End If

        End If

    End Sub

    Private Sub HistoriaProduktuToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles HistoriaProduktuToolStripMenuItem.Click
        If Not historia_materialu_wczytaj(strSKUclicked) Then
            Exit Sub
        End If

        Dim f As New frmRaportHistoriiMaterialu
        f.dtDane = dtHistoriaSku
        f.sku = strSKUclicked
        f.sc.Panel1Collapsed = True
        f.Text = "Raport historii produktu " & strSKUclicked
        f.MdiParent = frmGlowna
        f.Show()

    End Sub

    Private Sub chbDostepne_CheckedChanged(sender As Object, e As System.EventArgs) Handles chbDostepne.CheckedChanged
        If DodajeFiltry = False Then
            bZmianaStrony = True
            wczytaj()
            bZmianaStrony = False
        End If
    End Sub

    Private Sub chbNowosci_CheckedChanged(sender As Object, e As System.EventArgs) Handles chbNowosci.CheckedChanged
        If DodajeFiltry = False Then
            bZmianaStrony = True
            wczytaj()
            bZmianaStrony = False
        End If
    End Sub

    Private Sub btnWyczyscFiltry_Click(sender As System.Object, e As System.EventArgs) Handles btnWyczyscFiltry.Click
        UstawWartosciDomyslneFiltrow()
        wczytaj()
    End Sub

    Private Sub UstawWartosciDomyslneFiltrow()
        tbNumer.Text = ""
        tbNazwa.Text = ""
        chbDostepne.Checked = True
        chbNowosci.Checked = False
        chkgrupaAll.Checked = True
        chkKategoriaAll.Checked = True
        chkMarkaAll.Checked = True
    End Sub

    Private Sub btnWyczyscSelect_Click(sender As System.Object, e As System.EventArgs) Handles btnWyczyscSelect.Click
        ZaznaczenieWszystkich(True)
    End Sub



   
End Class


