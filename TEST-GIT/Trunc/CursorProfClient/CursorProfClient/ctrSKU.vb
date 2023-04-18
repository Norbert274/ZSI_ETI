Imports System.Threading
Imports System.Reflection
Imports System.Text
Imports System.Data
Imports System.IO

Public Class ctrSKU

    Public intIdMagazynu As Integer = -1
    'Public intIdGrupy As Integer = -1
    Private numerEkranu As Integer 'numer bie¿¹cego ekranu
    Private iloscEkranow As Integer 'iloœæ ekranów przy bie¿¹cym filtrze
    Private bReagujNaComboIloscNaStronie As Boolean = False
    Private bReagujNaCheckBoxHeader As Boolean = True
    Private bReagujNaComboMagazyn As Boolean = False
    Private wsWynik As wsCursorProf.SKUWczytajWynik
    Private wsWynikFiltr As wsCursorProf.SkuStanFiltryWynik
    Public zaznacz As Boolean = False
    Public dgvMultiSelect As Boolean = True
    Public dtSkuFiltred As DataTable
    Public bTrybWyboruSKU As Boolean = False 'jeœli True, po dwukliku u¿ytkownik zostanie zwrócony do zmiennej intIdWybranegoUzytkownika
    Public intIdSKU As Integer = -1
    Public NrSKU As String = ""
    Public strNazwaSKU As String = ""
    Public frmRodzic As Form

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
            MsgBox("B³¹d komunikacji z serwerem. " & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegó³y b³êdu:" & ex.Message, MsgBoxStyle.Critical)
            Return False
        End Try
        If wsWynikFiltr.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical)
            Return False
        ElseIf wsWynikFiltr.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation)
        End If
        'Marka
        If wsWynikFiltr.dane.Tables.Count > 0 Then
            For Each row As DataRow In wsWynikFiltr.dane.Tables(0).Rows
                listMarka.Items.Add(row.Item("WARTOSC").ToString, True)
            Next

        Else
            MsgBox("B³¹d wewnêtrzny systemu. Serwer nie przes³a³ listy dostêpnych marek." & frmGlowna.kontaktIt, MsgBoxStyle.Critical)
            Return False
        End If
        'Branza
        If wsWynikFiltr.dane.Tables.Count > 1 Then
            For Each row As DataRow In wsWynikFiltr.dane.Tables(1).Rows
                ListBranza.Items.Add(row.Item("WARTOSC").ToString, True)
            Next
        Else
            MsgBox("B³¹d wewnêtrzny systemu. Serwer nie przes³a³ listy dostêpnych bran¿." & frmGlowna.kontaktIt, MsgBoxStyle.Critical)
            Return False
        End If
        'Grupy
        If wsWynikFiltr.dane.Tables.Count > 2 Then
            listGrupa.Items.Add("BRAK", True)
            For Each row As DataRow In wsWynikFiltr.dane.Tables(2).Rows
                If Not row.Item(0).ToString = "BRAK" Then
                    listGrupa.Items.Add(row.Item("WARTOSC").ToString, True)
                End If
            Next
        Else
            MsgBox("B³¹d wewnêtrzny systemu. Serwer nie przes³a³ listy dostêpnych grup." & frmGlowna.kontaktIt, MsgBoxStyle.Critical)

            Return False
        End If
        'kategorie
        If wsWynikFiltr.dane.Tables.Count > 3 Then
            listKategoria.Items.Add("BRAK", True)
            For Each row As DataRow In wsWynikFiltr.dane.Tables(3).Rows
                If Not row.Item(0).ToString = "BRAK" Then
                    listKategoria.Items.Add(row.Item("WARTOSC").ToString, True)
                End If
            Next
        Else
            MsgBox("B³¹d wewnêtrzny systemu. Serwer nie przes³a³ listy dostêpnych kategorii." & frmGlowna.kontaktIt, MsgBoxStyle.Critical)

            Return False
        End If
        Return True
    End Function
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
        If Not dgv.CurrentCell Is Nothing Then
            intIdWybranegoWiersza = dgv.Rows(dgv.CurrentCell.RowIndex).Cells("sku_id").Value
            intNumerWybranejKolumny = dgv.CurrentCell.ColumnIndex
        End If

        'czy sortujemy po jakiejœ kolumnie?
        For Each dgvKolumna As DataGridViewColumn In dgv.Columns
            If dgvKolumna.HeaderCell.SortGlyphDirection <> SortOrder.None Then
                sortowanieKolumna = dgvKolumna.HeaderText
                If dgvKolumna.HeaderCell.SortGlyphDirection = SortOrder.Ascending Then
                    sortowanieNarastajaco = True
                Else
                    sortowanieNarastajaco = False
                End If
                Exit For
            End If
        Next

        'sprawdzanie czy zaznaczono co najmniej jedn¹ markê i jedn¹ bran¿ê
        Dim zaznaczonoMarke As Boolean = False
        For i As Integer = 0 To listMarka.Items.Count - 1
            If listMarka.GetItemChecked(i) = True Then
                zaznaczonoMarke = True
            End If
        Next
        Dim zaznaczonoBranze As Boolean = False
        For i As Integer = 0 To ListBranza.Items.Count - 1
            If ListBranza.GetItemChecked(i) = True Then
                zaznaczonoBranze = True
            End If
        Next
        Dim zaznaczonoGrupe As Boolean = False
        For i As Integer = 0 To listGrupa.Items.Count - 1
            If listGrupa.GetItemChecked(i) = True Then
                zaznaczonoGrupe = True
            End If
        Next
        Dim zaznaczonoKategorie As Boolean = False
        For i As Integer = 0 To listKategoria.Items.Count - 1
            If listKategoria.GetItemChecked(i) = True Then
                zaznaczonoKategorie = True
            End If
        Next
        If zaznaczonoMarke = False Or zaznaczonoBranze = False Or zaznaczonoGrupe = False Or zaznaczonoKategorie = False Then
            MsgBox("W filtrach musi byæ zaznaczone co najmniej jeden paramtr z listy.", MsgBoxStyle.Exclamation)
            Return False
        End If
        'odczyt listy z serwera
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            'txtNumerEkranu.Text, cmbIloscNaStronie.SelectedItem, _
            'txtFiltruj.Text, sortowanieKolumna, sortowanieNarastajaco
            wsWynik = ws.SKUWczytaj(frmGlowna.sesja, Marki, Branze, tbNumer.Text, tbNazwa.Text)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("B³¹d komunikacji z serwerem. " & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegó³y b³êdu:" & ex.Message, MsgBoxStyle.Critical)
            Return False
        End Try
        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical)
            Return False
        ElseIf wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation)
        End If
        'czyszczenie kontrolek przed wype³nieniem
        dgv.DataSource = Nothing
        dgv.Columns.Clear()
        dgv.Controls.Clear()

        'wype³nienie kontrolek wynikami
        If wsWynik.dane.Tables.Count > 0 Then
            'wype³nienie gridu

            Dim dv As New DataView(wsWynik.dane.Tables(0))
            'If chbDostepne.Checked = True Then
            '    dv.RowFilter = "(dostepne <> '0')"
            'Else
            '    dv.RowFilter = String.Empty
            'End If

            dgv.DataSource = dv

            'dodawanie tooltip z obrazkami
            'CType(dgv.Columns("ZDJECIE"), DataGridViewImageColumn).ImageLayout = DataGridViewImageCellLayout.Zoom
            Dim tt As New ToolTipWithImage(dgv, "zdjecie")


            'dodajemy kolumnê z checkboxami
            dgv.Columns.Insert(0, New DataGridViewCheckBoxColumn)
            dgv.Columns(0).Width = 22
            Dim dgvCell As DataGridViewCheckBoxCell
            For Each row As DataGridViewRow In dgv.Rows
                dgvCell = DirectCast(row.Cells(0), DataGridViewCheckBoxCell)
                dgvCell.Value = zaznacz And dgvMultiSelect
            Next
            If dgvMultiSelect = True Then

                Dim rect As Rectangle = dgv.GetCellDisplayRectangle(0, -1, True)
                rect.X = rect.Location.X + 3
                rect.Y = rect.Location.Y + 3
                Dim checkboxHeader As New CheckBox()
                checkboxHeader.Name = "checkboxHeader"
                checkboxHeader.Size = New Size(18, 18)
                checkboxHeader.Location = rect.Location
                checkboxHeader.Checked = zaznacz And dgvMultiSelect
                dgv.Controls.Add(checkboxHeader)
                AddHandler checkboxHeader.CheckedChanged, AddressOf dgv_checkboxHeaderCheckedChanged

            End If

            Dim tb As DataTable = wsWynik.dane.Tables(0)
            'Dim tb As DataTable = dgv.DataSource
            tb.DefaultView.Sort = "sku"
            dgv.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells)
            If dgv.Columns.Contains("sku_skr") Then dgv.Columns("sku_skr").Visible = False
            If dgv.Columns.Contains("sku_id") Then dgv.Columns("sku_id").Visible = False
            If dgv.Columns.Contains("grupa_id") Then dgv.Columns("grupa_id").Visible = False

            For Each kolumna As DataGridViewColumn In dgv.Columns
                kolumna.SortMode = DataGridViewColumnSortMode.Programmatic
                If kolumna.HeaderText = sortowanieKolumna And sortowanieKolumna <> String.Empty Then
                    If sortowanieNarastajaco Then
                        kolumna.HeaderCell.SortGlyphDirection = SortOrder.Ascending
                        dgv.Sort(kolumna, System.ComponentModel.ListSortDirection.Ascending)
                    Else
                        kolumna.HeaderCell.SortGlyphDirection = SortOrder.Descending
                        dgv.Sort(kolumna, System.ComponentModel.ListSortDirection.Ascending)
                    End If
                End If
            Next

            'podœwietlamy ostatnio wybran¹ komórkê, jeœli któraœ by³a wybrana
            If Not intIdWybranegoWiersza Is Nothing Then
                For Each wiersz As DataGridViewRow In dgv.Rows
                    If wiersz.Cells("sku_id").Value = intIdWybranegoWiersza Then
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
            MsgBox("B³¹d wewnêtrzny systemu. Serwer nie przes³a³ listy dostêpnych towarów." & frmGlowna.kontaktIt, MsgBoxStyle.Critical)
            Return False
        End If

        'If wsWynik.iloscStron > 1 Then
        '    iloscEkranow = wsWynik.iloscStron
        'Else
        iloscEkranow = 1
        'End If
        numerEkranu = txtNumerEkranu.Text
        lblIloscEkranow.Text = "z " & iloscEkranow
        If Not dtSkuFiltred Is Nothing Then
            filtruj()
        End If
        Return True
    End Function

    Public Sub odswiezListy()
        wczytaj()
    End Sub


    Private Sub dgv_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellContentClick
        If e.ColumnIndex = 0 Then
            Dim dgvCell As DataGridViewCheckBoxCell = DirectCast(dgv.Rows(e.RowIndex).Cells(e.ColumnIndex), DataGridViewCheckBoxCell)
            If dgvMultiSelect = False Then
                If dgvCell.Value Is Nothing OrElse Not dgvCell.Value Then
                    For Each dgvWiersz As DataGridViewRow In dgv.Rows
                        If dgvWiersz.Cells(0).Value = True Then
                            dgvWiersz.Cells(0).Value = False
                        End If
                    Next
                    dgvCell.Value = True
                Else
                    dgvCell.Value = False
                End If
            Else
                If dgvCell.Value Is Nothing OrElse Not dgvCell.Value Then
                    dgvCell.Value = True
                    'czy wszystkie s¹ zaznaczone?
                    Dim bWszystkie As Boolean = True
                    For Each dgvWiersz As DataGridViewRow In dgv.Rows
                        If dgvWiersz.Cells("sku_id").Value < 0 Then Continue For
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

                Else
                    dgvCell.Value = False
                    bReagujNaCheckBoxHeader = False
                    DirectCast(dgv.Controls.Find("checkboxHeader", True)(0), CheckBox).Checked = False
                    bReagujNaCheckBoxHeader = True
                End If
            End If
        End If
    End Sub

    Private Sub dgv_checkboxHeaderCheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        'dgv(0, i).Value = DirectCast(dgv.Controls.Find("checkboxHeader", True)(0), CheckBox).Checked
        'dgv.EndEdit()
        If bReagujNaCheckBoxHeader Then
            If dgv.Rows.Count > 0 Then
                'czy wszystkie checkboxy zaznaczone ?
                Dim bWszystkie As Boolean = True
                For Each dgvWiersz As DataGridViewRow In dgv.Rows
                    If dgvWiersz.Cells("sku_id").Value < 0 Then Continue For
                    If dgvWiersz.Cells(0).Value Is Nothing OrElse dgvWiersz.Cells(0).Value = False Then
                        bWszystkie = False
                        Exit For
                    End If
                Next
                'zaznaczamy lub odznaczamy
                For Each dgvWiersz As DataGridViewRow In dgv.Rows
                    If bWszystkie Then
                        'odznaczamy
                        dgvWiersz.Cells(0).Value = False
                    Else
                        'zaznaczamy
                        dgvWiersz.Cells(0).Value = True
                    End If
                Next
                If bWszystkie Then
                    sender.Checked = False
                Else
                    sender.Checked = True
                End If
            End If
        End If
    End Sub

    Private Sub btnOdswiez_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOdswiez.Click
        wczytaj()
    End Sub

    Private Sub txtNumerEkranu_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtNumerEkranu.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim intNumerEkranu As Integer
            If Not Integer.TryParse(txtNumerEkranu.Text, intNumerEkranu) Then
                MsgBox("Numer ekranu musi byæ liczb¹", MsgBoxStyle.Critical)
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
            MsgBox("Numer ekranu musi byæ liczb¹", MsgBoxStyle.Critical)
            e.Cancel = True
            Return
        End If
        If txtNumerEkranu.Text <> numerEkranu Then
            MsgBox("Je¿eli chcesz przejœæ na wpisany ekran, naciœnij Enter, jeœli chcesz wyjœæ z tego pola - naciœnij Escape", MsgBoxStyle.Exclamation)
            e.Cancel = True
            Return
        End If
    End Sub

    Private Sub btnNastepny_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNastepny.Click
        If txtNumerEkranu.Text >= iloscEkranow Then
            MsgBox("To jest ostatni ekran. Nie mo¿esz przejœæ do nastêpnego ekranu.", MsgBoxStyle.Exclamation)
            Return
        End If
        txtNumerEkranu.Text = txtNumerEkranu.Text + 1
        wczytaj()
    End Sub

    Private Sub btnOstatni_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOstatni.Click
        If txtNumerEkranu.Text = iloscEkranow Then
            MsgBox("To jest ostatni ekran.", MsgBoxStyle.Exclamation)
            Return
        End If
        txtNumerEkranu.Text = iloscEkranow
        wczytaj()
    End Sub

    Private Sub btnPoprzedni_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPoprzedni.Click
        If txtNumerEkranu.Text = 1 Then
            MsgBox("To jest pierwszy ekran. Nie mo¿esz przejœæ do poprzedniego ekranu.", MsgBoxStyle.Exclamation)
            Return
        End If
        txtNumerEkranu.Text = txtNumerEkranu.Text - 1
        wczytaj()
    End Sub

    Private Sub btnPoczatek_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPoczatek.Click
        If txtNumerEkranu.Text = 1 Then
            MsgBox("To jest pierwszy ekran.", MsgBoxStyle.Exclamation)
            Return
        End If
        txtNumerEkranu.Text = 1
        wczytaj()
    End Sub

    Private Sub cmbIloscNaStronie_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbIloscNaStronie.SelectedIndexChanged
        If bReagujNaComboIloscNaStronie Then
            txtNumerEkranu.Text = 1
            wczytaj()
        End If
    End Sub

    Private Sub dgv_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellDoubleClick
        If bTrybWyboruSKU Then
            intIdSKU = dgv.Rows(e.RowIndex).Cells("sku_id").Value
            NrSKU = dgv.Rows(e.RowIndex).Cells("sku").Value
            If dgv.Columns.Contains("nazwa") Then
                strNazwaSKU = dgv.Rows(e.RowIndex).Cells("nazwa").Value
            End If
            'Me.Close()
        Else
            SKUEdit(dgv.Rows(e.RowIndex).Cells("sku_id").Value)
        End If

    End Sub

    Private Sub dgv_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellEndEdit

    End Sub

    Private Sub dgv_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgv.ColumnHeaderMouseClick
        Dim sortowanieKolumna As String
        Dim sortowanieRosnaco As Boolean
        Dim kolumna As DataGridViewColumn
        Dim kolumnaKliknieta As DataGridViewColumn

        kolumnaKliknieta = dgv.Columns(e.ColumnIndex)
        If kolumnaKliknieta.HeaderText <> "dostepne" And kolumnaKliknieta.HeaderText <> "dostepne_podwladni" Then
            sortowanieKolumna = kolumnaKliknieta.HeaderText

            For Each kolumna In dgv.Columns
                If kolumnaKliknieta.HeaderText <> kolumna.HeaderText Then kolumna.HeaderCell.SortGlyphDirection = SortOrder.None
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
            wczytaj()

            'szukamy, czy w wyniku jest przed chwil¹ klikniêta kolumna (po nazwie)
            For Each kolumna In dgv.Columns
                If kolumna.HeaderText = sortowanieKolumna Then
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
        If e.Control And e.KeyCode = Keys.C Then
            dgv.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithAutoHeaderText
            If dgv.GetCellCount(DataGridViewElementStates.Selected) > 0 Then
                Clipboard.SetDataObject(dgv.GetClipboardContent())
                Dim str As String = Clipboard.GetText()
                Dim w1250 As Encoding = Encoding.GetEncoding("windows-1250")
                Dim [unicode] As Encoding = Encoding.Unicode
                Dim unicodeBytes As Byte() = [unicode].GetBytes(str)
                Dim w1250Bytes As Byte() = Encoding.Convert([unicode], w1250, unicodeBytes)
                Dim w1250Chars(w1250.GetCharCount(w1250Bytes, 0, w1250Bytes.Length) - 1) As Char
                w1250.GetChars(w1250Bytes, 0, w1250Bytes.Length, w1250Chars, 0)
                Dim w1250String As New String(w1250Chars)
                Clipboard.SetDataObject(w1250String)
                e.Handled = True
            End If
        End If
    End Sub

    Public Sub ShowStan()
        'inicjalizacja ustawieñ kontrolek
        UstawFiltry()
        numerEkranu = 1
        txtNumerEkranu.Text = numerEkranu
        cmbIloscNaStronie.SelectedIndex = 1
        bReagujNaComboIloscNaStronie = True
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
            If row.Item("sku_id") = sku_id And row.Item("id_grupy_towarowe") = id_grupy_towarowe And row.Item("akcja_id") = akcja_id Then
                row.Item(0).value = True
            End If
        Next
    End Sub

    Private Sub filtruj()
        For Each rowIstnieje As DataRow In dtSkuFiltred.Rows()
            For i As Integer = dgv.DataSource.Table.Rows.Count - 1 To 0 Step -1
                'If Not (rowIstnieje.Item("sku_id") = dgv.DataSource.Table.Rows(i).Item("sku_id") And rowIstnieje.Item("id_grupa_towarowa") = dgv.DataSource.Table.Rows(i).Item("id_grupa_towarowa") And rowIstnieje.Item("akcja_id") = dgv.DataSource.Table.Rows(i).Item("akcja_id")) Then
                'dgv.DataSource.Rows(i).Visable = False
                'End If
            Next
        Next
    End Sub


    Private Sub ctrSKU_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ''inicjalizacja ustawieñ kontrolek
        'numerEkranu = 1
        'txtNumerEkranu.Text = numerEkranu
        'cmbIloscNaStronie.SelectedIndex = 1
        'bReagujNaComboIloscNaStronie = True

        'If Not wczytaj() Then frmRodzic.Close()
    End Sub



    Private Function Marki() As String
        Dim wynik As String = String.Empty
        Dim i As Integer
        For i = 0 To listMarka.Items.Count - 1
            If listMarka.GetItemChecked(i) Then
                wynik = wynik + "'" + listMarka.Items(i) + "',"
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
        For i = 0 To ListBranza.Items.Count - 1
            If ListBranza.GetItemChecked(i) Then
                wynik = wynik + "'" + ListBranza.Items(i) + "',"
            End If
        Next
        If wynik.Length > 0 Then wynik = wynik.Remove(wynik.Length - 1)
        'End If
        Return wynik
    End Function

    Private Function grupy() As String
        Dim wynik As String = String.Empty
        Dim i As Integer
        For i = 0 To listGrupa.Items.Count - 1
            If listGrupa.GetItemChecked(i) Then
                wynik = wynik + "'" + listGrupa.Items(i) + "',"
            End If
        Next
        If wynik.Length > 0 Then wynik = wynik.Remove(wynik.Length - 1)
        'End If
        Return wynik
    End Function

    Private Function kategorie() As String
        Dim wynik As String = String.Empty
        Dim i As Integer
        For i = 0 To listKategoria.Items.Count - 1
            If listKategoria.GetItemChecked(i) Then
                wynik = wynik + "'" + listKategoria.Items(i) + "',"
            End If
        Next
        If wynik.Length > 0 Then wynik = wynik.Remove(wynik.Length - 1)
        'End If
        Return wynik
    End Function

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

End Class
