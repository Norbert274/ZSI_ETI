Imports System.Text
Public Class frmLimity
    Private numerEkranu As Integer 'numer bie¿¹cego ekranu
    Private iloscEkranow As Integer 'iloœæ ekranów przy bie¿¹cym filtrze
    Private bReagujNaComboIloscNaStronie As Boolean = False
    Public bTrybWyboruUzytkownika As Boolean = False 'jeœli True, po dwukliku u¿ytkownik zostanie zwrócony do zmiennej intIdWybranegoUzytkownika
    Public intIdWybranegoUzytkownika As Integer = -1
    Public strNazwaWybranegoUzytkownika As String = ""
    Private dtGrupy As DataTable 'do drzewa z grupami
    Private dtHierarchia As DataTable 'do drzewa z grupami
    Private bReagujNaZmianyDrzewaGrup As Boolean = False
    Private BlokujZapisz As Boolean = False
    Private suma_limitow As Decimal
    Private suma_limitow_po_zmianie As Decimal
    Private ile_wierszy_limit As Integer = 0




    Private Sub frmLimity_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        If suma_limitow_po_zmianie > suma_limitow Then
            Dim odp As MsgBoxResult = MsgBox("Od ostatniego odczytu z serwera wprowadzono zmiany w limitach. Czy zapisaæ wprowadzone zmiany?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNoCancel + MsgBoxStyle.DefaultButton1, Me.Text)
            If odp = MsgBoxResult.Yes Then
                If Not zapisz_limit() Then
                    e.Cancel = True
                    Exit Sub
                End If
            ElseIf odp = MsgBoxResult.Cancel Then
                e.Cancel = True
                Exit Sub
            End If
        End If
    End Sub

    Private Sub frmLimity_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

        'inicjalizacja ustawieñ kontrolek
        numerEkranu = 1
        txtNumerEkranu.Text = numerEkranu
        cmbIloscNaStronie.SelectedIndex = 1
        bReagujNaComboIloscNaStronie = True

        If Not wczytaj() Then Me.Close()
    End Sub
    'Private Function zapisz_limit() As Boolean
    '    Dim bBylyZmiany As Boolean = False
    '    Dim objZnalezionyWiersz As DataRow 'wiersz odnaleziony
    '    Dim wlaczValue As Boolean 'wartosc wczytaj dal wiersza odnalezionego
    '    Dim dtKopia As DataTable
    '    Dim dtKopiaFunkcje As DataTable

    '   System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
    'System.Net.ServicePointManager.Expect100Continue = False
    'ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
    'ws.Proxy.Credentials = CredentialCache.DefaultCredentials
    '    'ws.Url = frmGlowna.strWebservice
    '    Dim wsWynik As wsCursorProf.UserLimityZapiszWynik



    '    btnZapisz.Enabled = True
    '    Return True
    'End Function
    Private Function wczytaj() As Boolean
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        'ws.Url = frmGlowna.strWebservice
        Dim wsWynik As wsCursorProf.UserLimityWczytajWynik
        Dim sortowanieKolumna As String = ""
        Dim sortowanieNarastajaco As Boolean = True
        Dim intIdWybranegoWiersza = Nothing

        'zapisujemy id wybranego wiersza, jeœli jakiœ jest wybrany
        If Not dgv.CurrentCell Is Nothing Then
            intIdWybranegoWiersza = dgv.Rows(dgv.CurrentCell.RowIndex).Cells("user_id").Value
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


        'budujemy listê grup do pokazania - jeœli za³adowano drzewo grup
        Dim dsGrupyDoPokazania As New DataSet
        Dim dtGrupyDoPokazania As New DataTable
        dtGrupyDoPokazania.Columns.Add("grupa_id")
        If Not (dtGrupy Is Nothing OrElse dtHierarchia Is Nothing) Then
            For Each node As TreeNode In tv.Nodes
                dodajZaznaczonePotomneWezly(node, dtGrupyDoPokazania)
            Next
            dsGrupyDoPokazania.Tables.Add(dtGrupyDoPokazania)
        End If

        'odczyt listy z serwera
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.UserLimityWczytaj(frmGlowna.sesja, txtNumerEkranu.Text, cmbIloscNaStronie.SelectedItem, _
                txtFiltruj.Text, sortowanieKolumna, sortowanieNarastajaco, dsGrupyDoPokazania)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("B³¹d komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegó³y b³êdu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
            Return False
        End Try
        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, Me.Text)
            Return False
        ElseIf wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, Me.Text)
        End If

        'czyszczenie kontrolek przed wype³nieniem
        dgv.DataSource = Nothing

        'wype³nienie kontrolek wynikami
        If wsWynik.dane.Tables.Count > 0 Then

            'wype³nienie gridu
            dgv.DataSource = wsWynik.dane.Tables(0)
            If dgv.Columns.Contains("user_id") Then dgv.Columns("user_id").Visible = False
            For Each kolumna As DataGridViewColumn In dgv.Columns
                kolumna.SortMode = DataGridViewColumnSortMode.Programmatic
                If kolumna.HeaderText = sortowanieKolumna Then
                    If sortowanieNarastajaco Then
                        kolumna.HeaderCell.SortGlyphDirection = SortOrder.Ascending
                    Else
                        kolumna.HeaderCell.SortGlyphDirection = SortOrder.Descending
                    End If
                End If
            Next

            'synchronizujemy listê ukrytych kolumn z rejestrem
            'Dim objWartoscZRejestru
            'Dim bByla As Boolean



            'podœwietlamy ostatnio wybran¹ komórkê, jeœli któraœ by³a wybrana
            If Not intIdWybranegoWiersza Is Nothing Then
                For Each wiersz As DataGridViewRow In dgv.Rows
                    If wiersz.Cells("user_id").Value = intIdWybranegoWiersza Then
                        'szukamy pierwszej widocznej kolumny
                        For Each kolumna As DataGridViewColumn In dgv.Columns
                            If kolumna.Visible Then
                                dgv.CurrentCell = dgv.Rows(wiersz.Index).Cells(kolumna.Index)
                                Exit For
                            End If
                        Next
                    End If
                Next
            End If


        Else
            MsgBox("B³¹d wewnêtrzny systemu. Serwer nie przes³a³ listy limitów" & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
            Me.Close()
        End If

        'zapis sumy z kolumny Dodaj
        For Each wiersz As DataGridViewRow In dgv.Rows
            For Each kol As DataGridViewColumn In dgv.Columns
                If kol.Name = "przydzial" Then
                    suma_limitow += CType(dgv.Rows(wiersz.Index).Cells(kol.Index).Value, Decimal)
                End If
            Next

        Next

        'zapis iloœci wierszy w kolumnnie Dodaj
        For Each wiersz As DataGridViewRow In dgv.Rows
            For Each kol As DataGridViewColumn In dgv.Columns
                If kol.Name = "przydzial" Then
                    ile_wierszy_limit += 1
                End If
            Next

        Next

        If wsWynik.iloscStron > 1 Then
            iloscEkranow = wsWynik.iloscStron
        Else
            iloscEkranow = 1
        End If
        numerEkranu = txtNumerEkranu.Text
        lblIloscEkranow.Text = "z " & iloscEkranow
        'btnZapisz.Enabled = False
        For Each dgvColumn As DataGridViewColumn In dgv.Columns
            If dgvColumn.HeaderText.ToLower = "przydzial" Then
                dgvColumn.ReadOnly = False
            Else
                dgvColumn.ReadOnly = True
            End If


        Next
        'dgv.Columns("Dodaj").ReadOnly = False
        Return True
    End Function

    Private Sub dodajZaznaczonePotomneWezly(ByVal node As TreeNode, ByRef dt As DataTable)
        If node.Checked Then dt.Rows.Add(node.Name)
        For Each nodePotomny As TreeNode In node.Nodes
            dodajZaznaczonePotomneWezly(nodePotomny, dt)
        Next
    End Sub

    Public Sub userEdit(ByVal user_id As Integer)
        Dim frm As frmUzytkownik = New frmUzytkownik
        frm.frmRodzic = Me
        frm.WindowState = FormWindowState.Normal
        frm.intIdUzytkownika = user_id
        If Me.Modal Then
            frm.ShowDialog()
        Else
            frm.MdiParent = frmGlowna
            frm.Show()
        End If
    End Sub

    Public Sub odswiezListy()
        wczytaj()
    End Sub

    Private Sub zaladujDrzewoGrup()
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
            MsgBox("B³¹d komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegó³y b³êdu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
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
        dtGrupy = wsWynik.dane.Tables(0)
        dtHierarchia = wsWynik.dane.Tables(1)

        'dodajemy wêz³y
        dtHierarchia.DefaultView.RowFilter = "nadrzedna_id is null"
        Dim dtMojaHierarchia As DataTable = wsWynik.dane.Tables(1).DefaultView.ToTable
        For Each dtWiersz As DataRow In dtMojaHierarchia.Rows
            dodajWezlyPotomne(-1, dtWiersz("podrzedna_id"), dtGrupy, dtHierarchia)
        Next
        tv.ExpandAll()
        bReagujNaZmianyDrzewaGrup = True
    End Sub

    Private Sub dodajWezlyPotomne(ByVal idRodzica As Integer, ByVal idDoDodania As Integer, ByVal dtGrupy As DataTable, ByVal dtHierarchia As DataTable)
        'odszukanie w³asnej nazwy
        dtGrupy.DefaultView.RowFilter = "grupa_id=" & idDoDodania
        Dim dtGrupa As DataTable = dtGrupy.DefaultView.ToTable
        If dtGrupa.Rows.Count < 1 Then Exit Sub
        Dim strNazwa As String = dtGrupa.Rows(0).Item("nazwa")

        'odszukanie wêz³a rodzica w drzewie i dodanie siê do niego
        Dim newNode As TreeNode
        If idRodzica < 0 Then
            newNode = tv.Nodes.Add(idDoDodania, strNazwa)
            newNode.Checked = True
        Else
            Dim nodes() As TreeNode
            nodes = tv.Nodes.Find(idRodzica, True)
            For Each node As TreeNode In nodes
                newNode = node.Nodes.Add(idDoDodania, strNazwa)
                newNode.Checked = True
            Next
        End If

        'czy mamy wêz³y potomne?
        dtHierarchia.DefaultView.RowFilter = "nadrzedna_id=" & idDoDodania
        Dim dtPotomne As DataTable = dtHierarchia.DefaultView.ToTable
        For Each dtWiersz As DataRow In dtPotomne.Rows
            dodajWezlyPotomne(idDoDodania, dtWiersz("podrzedna_id"), dtGrupy, dtHierarchia)
        Next
    End Sub

#Region "Handlery formy"

    Private Sub btnFiltruj_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFiltruj.Click
        txtNumerEkranu.Text = 1
        wczytaj()
    End Sub

    Private Sub txtFiltruj_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtFiltruj.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtNumerEkranu.Text = 1
            wczytaj()
        End If
    End Sub

    Private Sub btnOdswiez_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOdswiez.Click
        wczytaj()
    End Sub

    Private Sub txtNumerEkranu_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtNumerEkranu.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim intNumerEkranu As Integer
            If Not Integer.TryParse(txtNumerEkranu.Text, intNumerEkranu) Then
                MsgBox("Numer ekranu musi byæ liczb¹", MsgBoxStyle.Critical, Me.Text)
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
            MsgBox("Numer ekranu musi byæ liczb¹", MsgBoxStyle.Critical, Me.Text)
            e.Cancel = True
            Return
        End If
        If txtNumerEkranu.Text <> numerEkranu Then
            MsgBox("Je¿eli chcesz przejœæ na wpisany ekran, naciœnij Enter, jeœli chcesz wyjœæ z tego pola - naciœnij Escape", MsgBoxStyle.Exclamation, Me.Text)
            e.Cancel = True
            Return
        End If
    End Sub

    Private Sub btnNastepny_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNastepny.Click
        If txtNumerEkranu.Text >= iloscEkranow Then
            MsgBox("To jest ostatni ekran. Nie mo¿esz przejœæ do nastêpnego ekranu.", MsgBoxStyle.Exclamation, Me.Text)
            Return
        End If
        txtNumerEkranu.Text = txtNumerEkranu.Text + 1
        wczytaj()
    End Sub

    Private Sub btnOstatni_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOstatni.Click
        If txtNumerEkranu.Text = iloscEkranow Then
            MsgBox("To jest ostatni ekran.", MsgBoxStyle.Exclamation, Me.Text)
            Return
        End If
        txtNumerEkranu.Text = iloscEkranow
        wczytaj()
    End Sub

    Private Sub btnPoprzedni_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPoprzedni.Click
        If txtNumerEkranu.Text = 1 Then
            MsgBox("To jest pierwszy ekran. Nie mo¿esz przejœæ do poprzedniego ekranu.", MsgBoxStyle.Exclamation, Me.Text)
            Return
        End If
        txtNumerEkranu.Text = txtNumerEkranu.Text - 1
        wczytaj()
    End Sub

    Private Sub btnPoczatek_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPoczatek.Click
        If txtNumerEkranu.Text = 1 Then
            MsgBox("To jest pierwszy ekran.", MsgBoxStyle.Exclamation, Me.Text)
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




    Private Sub dgv_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgv.ColumnHeaderMouseClick
        Dim sortowanieKolumna As String
        Dim sortowanieRosnaco As Boolean
        Dim kolumna As DataGridViewColumn
        Dim kolumnaKliknieta As DataGridViewColumn

        kolumnaKliknieta = dgv.Columns(e.ColumnIndex)
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
    End Sub

    Private Sub tsKolumny_DropDownItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs)
        Dim pozycja As ToolStripMenuItem = DirectCast(e.ClickedItem, ToolStripMenuItem)
        If pozycja.Checked Then
            pozycja.Checked = False
        Else
            'zaznaczamy, pokazujemy kolumnê
            pozycja.Checked = True
        End If
    End Sub

    Private Sub btnGrupy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrupy.Click
        If sc.Panel1Collapsed Then
            If dtGrupy Is Nothing OrElse dtHierarchia Is Nothing Then
                zaladujDrzewoGrup()
            End If
            sc.Panel1Collapsed = False
            btnGrupy.Text = "Ukryj grupy"
        Else
            sc.Panel1Collapsed = True
            btnGrupy.Text = "Poka¿ grupy"
        End If
    End Sub

    Private Sub btnZaznaczWszystkie_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnZaznaczWszystkie.Click
        For Each node As TreeNode In tv.Nodes
            zmienStanZaznaczenia(node, True)
        Next
    End Sub

    Private Sub zmienStanZaznaczenia(ByVal node As TreeNode, ByVal stan As Boolean)
        'zmieñ stan mój
        node.Checked = stan

        'i moich dzieci
        For Each nodePotomny As TreeNode In node.Nodes
            zmienStanZaznaczenia(nodePotomny, stan)
        Next
    End Sub

    Private Sub btnOdznaczWszystkie_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOdznaczWszystkie.Click
        For Each node As TreeNode In tv.Nodes
            zmienStanZaznaczenia(node, False)
        Next
    End Sub

#End Region
    Private Sub dgv_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellEndEdit
        'If dgv.Columns(e.ColumnIndex).HeaderText = "przydzial" Then
        '    Dim suma_limitow_po_zmianie As Decimal
        '    suma_limitow_po_zmianie = 0
        '    For Each row As DataGridViewRow In dgv.Rows
        '        Dim wynik As Decimal
        '        If Not Decimal.TryParse(row.Cells("przydzial").Value, wynik) Then
        ''            MsgBox("Wartoœæ limitu musi byæ liczb¹", MsgBoxStyle.Critical)
        '            Exit For
        '        End If
        '        suma_limitow_po_zmianie = suma_limitow_po_zmianie + wynik

        '    Next
        '    daj_limity_po_zmianie(suma_limitow_po_zmianie)
        '    btnZapisz.Enabled = True
        '    'MsgBox(suma_limitow_po_zmianie)
        'End If
    End Sub

    Private Sub dgv_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles dgv.CellValidating
        If dgv.Columns(e.ColumnIndex).HeaderText = "przydzial" Then
            Dim suma_limitow_po_zmianie As Decimal
            suma_limitow_po_zmianie = 0
            For Each row As DataGridViewRow In dgv.Rows
                Dim wynik As Decimal
                If Not Decimal.TryParse(row.Cells("przydzial").Value, wynik) Then
                    MsgBox("Wartoœæ limitu musi byæ liczb¹", MsgBoxStyle.Critical, Me.Text)
                    Exit For
                End If
                If wynik < 0 Then
                    MsgBox("Wartoœæ limitu nie moze byæ liczb¹ ujemn¹", MsgBoxStyle.Critical, Me.Text)
                    Exit For
                End If
                suma_limitow_po_zmianie = suma_limitow_po_zmianie + wynik

            Next
            daj_limity_po_zmianie(suma_limitow_po_zmianie)
            btnZapisz.Enabled = True
            'MsgBox(suma_limitow_po_zmianie)
        End If
    End Sub


    Public Function daj_limity_po_zmianie(ByVal suma As Decimal) As Decimal
        suma_limitow_po_zmianie = suma
        Return suma_limitow_po_zmianie
    End Function

    Private Sub btnZapisz_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnZapisz.Click
        If dgv.CurrentCell.IsInEditMode Then
            Dim wynik As Decimal
            dgv.EndEdit()
            If Not Decimal.TryParse(dgv.CurrentCell.Value, wynik) Then
                MsgBox("Wartoœæ limitu musi byæ liczb¹", MsgBoxStyle.Critical, Me.Text)
                Exit Sub
            End If
            If wynik < 0 Then
                MsgBox("Wartoœæ limitu nie moze byæ liczb¹ ujemn¹", MsgBoxStyle.Critical, Me.Text)
                Exit Sub
            End If
            suma_limitow_po_zmianie = suma_limitow_po_zmianie + wynik
            daj_limity_po_zmianie(suma_limitow_po_zmianie)
        End If
        zapisz_limit()
    End Sub

    Private Function zapisz_limit() As Boolean
        If dgv.CurrentCell.IsInEditMode Then
            CType(dgv.DataSource, DataTable).AcceptChanges()
            dgv.CurrentCell = dgv.Rows(0).Cells(0)
        End If

        If suma_limitow_po_zmianie > suma_limitow Then
            System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
            System.Net.ServicePointManager.Expect100Continue = False
            ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
            ws.Proxy.Credentials = CredentialCache.DefaultCredentials
            'ws.Url = frmGlowna.strWebservice
            Dim wsWynik As wsCursorProf.UserLimityZapiszWynik

            Dim ds As New DataSet
            ds.Tables.Add(dgv.DataSource.Copy)

            'zapis zmian limitów na serwer
            Try
                Cursor = Cursors.WaitCursor
                Application.DoEvents()
                wsWynik = ws.UserLimityZapisz(frmGlowna.sesja, ds)
                Cursor = Cursors.Default
            Catch ex As Exception
                Cursor = Cursors.Default
                MsgBox("B³¹d komunikacji z serwerem. " & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegó³y b³êdu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
                Return False
            End Try

            If wsWynik.status < 0 Then
                MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, Me.Text)
                Return False
            ElseIf wsWynik.status > 0 Then
                MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, Me.Text)
            End If
            'odœwie¿amy dgv 
            wczytaj()
            'zerujemy zmienn¹ "suma_limitow_po_zmianie"
            suma_limitow_po_zmianie = 0


            frmGlowna.lblStatus.Text = "Pomyœlnie zapisano limity dla u¿ytkowników "
            frmGlowna.timer.Interval = 3000 'komunikat zniknie po 3s
            frmGlowna.timer.Start()
        End If
        Return True
    End Function
End Class