Imports System.Reflection

Public Class frmAdresy
    Public Enum frmAdresyTrybPracy
        adresyDlaUzytkownika
        adresyDoWysylkiGrupowej
    End Enum

    Public frmRodzic As Form
    Public intIdUzytkownika As Integer = -1 'przekazane z formy rodzica
    Public strNazwaUzytkownika As String = "" 'przekazane z formy rodzica
    'Public enumTrybPracy As frmAdresyTrybPracy = frmAdresyTrybPracy.adresyDlaUzytkownika
    Public dtAdresy As DataTable 'tu w trybie pracy adresyDoWysylkiGrupowej bêd¹ zaznaczeni odbiorcy
    Private numerEkranu As Integer 'numer bie¿¹cego ekranu
    Private iloscEkranow As Integer 'iloœæ ekranów przy bie¿¹cym filtrze
    Private bReagujNaComboIloscNaStronie As Boolean = False
    Private bReagujNaCheckBoxHeader As Boolean = True
    Dim Parametry As New ZmienneGlobalne

    Private Sub frmAdresy_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        'inicjalizacja ustawieñ kontrolek
        'Select Case enumTrybPracy
        '    Case frmAdresyTrybPracy.adresyDlaUzytkownika
        '        btnWybierzDoZamowienia.Visible = False
        '    Case frmAdresyTrybPracy.adresyDoWysylkiGrupowej
        '        btnWybierzDoZamowienia.Visible = True
        'End Select
        
        numerEkranu = 1
        txtNumerEkranu.Text = numerEkranu
        cmbIloscNaStronie.SelectedIndex = 1
        bReagujNaComboIloscNaStronie = True
        
        If Not wczytaj() Then Me.Close()
    End Sub

    Private Function usun(ByVal intIdAdres) As Boolean
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        'ws.Url = frmGlowna.strWebservice
        Dim wsWynik As wsCursorProf.AdresUsunWynik

        'komunikacja z serwerem
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.AdresUsun(frmGlowna.sesja, intIdAdres)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("B³¹d komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegó³y b³êdu:" & ex.Message, MsgBoxStyle.Critical, "Usuwanie adresu")
            Return False
        End Try
        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, "Usuwanie adresu")
            Return False
        ElseIf wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, "Usuwanie adresu")
        End If

        'aktualizujemy licznik w oknie rodzica-rodzica
        'If Not frmRodzic Is Nothing Then
        '    Select Case enumTrybPracy
        '        Case frmAdresyTrybPracy.adresyDlaUzytkownika
        If Not frmRodzic Is Nothing Then
            Dim m As MethodInfo() = frmRodzic.GetType.GetMethods()
            For licznik As Integer = 0 To m.GetUpperBound(0)
                If m(licznik).Name = "AdresyLicznikZmniejsz" Then
                    m(licznik).Invoke(frmRodzic, Nothing)
                End If
            Next
        End If
        '        Case frmAdresyTrybPracy.adresyDoWysylkiGrupowej
        ''Dim frm As frmZamowienie = CType(frmRodzic, frmZamowienie)
        ''frm.lblAdresy.Text -= 1
        '    End Select
        'End If

        If Not wczytaj() Then Return False
        Return True

    End Function

    Private Function wczytaj() As Boolean
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        'ws.Url = frmGlowna.strWebservice
        Dim wsWynik As wsCursorProf.AdresStronaWynik
        Dim sortowanieKolumna As String = ""
        Dim sortowanieNarastajaco As Boolean = True
        Dim intIdWybranegoWiersza = Nothing
        Dim intNumerWybranejKolumny = Nothing

        'zapisujemy id wybranego wiersza, jeœli jakiœ jest wybrany
        If Not dgv.CurrentCell Is Nothing Then
            intIdWybranegoWiersza = dgv.Rows(dgv.CurrentCell.RowIndex).Cells("adres_id").Value
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

        'budujemy listê ukrytych kolumn
        Dim dsKolumnyUkryte As New DataSet
        dsKolumnyUkryte.Tables.Add()
        dsKolumnyUkryte.Tables(0).Columns.Add("nazwa")
       
        'odczyt listy z serwera
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            'Select Case enumTrybPracy
            '    Case frmAdresyTrybPracy.adresyDlaUzytkownika
            wsWynik = ws.AdresStrona(frmGlowna.sesja, intIdUzytkownika, False, txtNumerEkranu.Text, _
                cmbIloscNaStronie.SelectedItem, txtFiltruj.Text, sortowanieKolumna, sortowanieNarastajaco, _
                dsKolumnyUkryte)
            '    Case frmAdresyTrybPracy.adresyDoWysylkiGrupowej
            'wsWynik = ws.AdresStrona(frmGlowna.sesja, intIdUzytkownika, True, txtNumerEkranu.Text, _
            '    cmbIloscNaStronie.SelectedItem, txtFiltruj.Text, sortowanieKolumna, sortowanieNarastajaco, _
            '    dsKolumnyUkryte)
            '    Case Else
            'MsgBox("B³¹d wewnêtrzny aplikacji. Zmienna enumTrybPracy ma wartoœæ " & enumTrybPracy.ToString & frmGlowna.kontaktIt, MsgBoxStyle.Critical)
            'Return False
            'End Select
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("B³¹d komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegó³y b³êdu:" & ex.Message, MsgBoxStyle.Critical, "Wczytanie listy adresów")
            Return False
        End Try
        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, "Wczytanie listy adresów")
            Return False
        ElseIf wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, "Wczytanie listy adresów")
        End If

        'czyszczenie kontrolek przed wype³nieniem
        dgv.DataSource = Nothing
        dgv.Columns.Clear()
        dgv.Controls.Clear()

        'wype³nienie kontrolek wynikami
        'Select Case enumTrybPracy
        '    Case frmAdresyTrybPracy.adresyDlaUzytkownika
        Me.Text = "Edycja adresów dla u¿ytkownika " & wsWynik.userAdresy
        '    Case frmAdresyTrybPracy.adresyDoWysylkiGrupowej
        'Me.Text = "Wybór odbiorców dla zamówienia grupowego"
        'End Select

        If wsWynik.dane.Tables.Count > 0 Then
            'wype³nienie gridu
            dgv.DataSource = wsWynik.dane.Tables(0)
            dgv.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells)
            If dgv.Columns.Contains("adres_id") Then dgv.Columns("adres_id").Visible = False
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

            'w trybie pracy zamówienie grupowe dodajemy kolumnê z checkboxami
            'If enumTrybPracy = frmAdresyTrybPracy.adresyDoWysylkiGrupowej Then
            '    dgv.Columns.Insert(0, New DataGridViewCheckBoxColumn)
            '    dgv.Columns(0).Width = 22
            '    Dim rect As Rectangle = dgv.GetCellDisplayRectangle(0, -1, True)
            '    rect.X = rect.Location.X + 3
            '    rect.Y = rect.Location.Y + 3
            '    Dim checkboxHeader As New CheckBox()
            '    checkboxHeader.Name = "checkboxHeader"
            '    checkboxHeader.Size = New Size(18, 18)
            '    checkboxHeader.Location = rect.Location
            '    dgv.Controls.Add(checkboxHeader)
            '    AddHandler checkboxHeader.CheckedChanged, AddressOf dgv_checkboxHeaderCheckedChanged

            '    'zaznaczamy wiersze, które rodzic przys³a³ jako "wybrane"
            '    For Each dtRow As DataRow In dtAdresy.Rows
            '        For Each dgvRow As DataGridViewRow In dgv.Rows
            '            If dgvRow.Cells("adres_id").Value = dtRow("adres_id") Then
            '                dgvRow.Cells(0).Value = True
            '            End If
            '        Next
            '    Next

            '    'czy wszystkie checkboxy s¹ zaznaczone?
            '    Dim bWszystkie As Boolean = True
            '    For Each dgvWiersz As DataGridViewRow In dgv.Rows
            '        If dgvWiersz.Cells("adres_id").Value < 0 Then Continue For
            '        If dgvWiersz.Cells(0).Value Is Nothing OrElse dgvWiersz.Cells(0).Value = False Then
            '            bWszystkie = False
            '            Exit For
            '        End If
            '    Next
            '    If bWszystkie Then
            '        bReagujNaCheckBoxHeader = False
            '        DirectCast(dgv.Controls.Find("checkboxHeader", True)(0), CheckBox).Checked = True
            '        bReagujNaCheckBoxHeader = True
            '    End If

            'End If

            'synchronizujemy listê ukrytych kolumn z rejestrem
            'Dim objWartoscZRejestru
            'Dim bByla As Boolean

            'przepisujemy obecnie wypisane kolumny z guzika do DataTable

           
            'przegl¹damy, które kolumny wróci³y w wyniku
            'For Each cKolumnaWyniku As DataColumn In wsWynik.dane.Tables(0).Columns
            '    'czy taka kolumna by³a na guziku?
            '    bByla = False
            '    For Each rKolumnaZGuzika As DataRow In dtKolumnyZGuzika.Rows
            '        If cKolumnaWyniku.Caption = rKolumnaZGuzika("nazwa") Then
            '            'jest na guziku i w wyniku
            '            If rKolumnaZGuzika("zaznaczona") Then
            '                'kolumna zaznaczona i przysz³a w wyniku - jest ok
            '            Else
            '                'dziwne - kolumna nie by³a zaznaczona, prosiliœmy aby jej nie przysy³aæ, a przysz³a
            '                'trudno - baza ma wy¿szoœæ - pokazujemy t¹ kolumnê
            '                rKolumnaZGuzika("zaznaczona") = 1
            '            End If
            '            rKolumnaZGuzika("znaleziona") = 1
            '            bByla = True
            '            Exit For
            '        End If
            '    Next
            '    If Not bByla AndAlso cKolumnaWyniku.Caption.ToLower <> "adres_id" Then
            '        'kolumny nie by³o na guziku, a jest w wyniku - dopisujemy na guzik
            '        dtKolumnyZGuzika.Rows.Add(cKolumnaWyniku.Caption, 1, 1, 1)
            '    End If
            'Next

            ''przegl¹damy tabelê z kolumnami na guziku

            ''dopisujemy do rejestru te, które s¹ nie zaznaczone
            'For Each rKolumnaZGuzika As DataRow In dtKolumnyZGuzika.Rows
            '    If rKolumnaZGuzika("zaznaczona") Then
            '        If rKolumnaZGuzika("znaleziona") Then
            '            'kolumna mia³a przyjœæ i przysz³a - usuwamy z rejestru
            '            objWartoscZRejestru = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Cursor\" & Parametry._NazwaProjektu & "\" & Me.Name, rKolumnaZGuzika("nazwa"), Nothing)
            '            If Not objWartoscZRejestru Is Nothing Then
            '                Dim rKey As Microsoft.Win32.RegistryKey = My.Computer.Registry.CurrentUser.OpenSubKey("Software\Cursor\" & Parametry._NazwaProjektu & "\" & Me.Name, True)
            '                rKey.DeleteValue(rKolumnaZGuzika("nazwa"))
            '            End If
            '        Else
            '            'kolumna mia³a przyjœæ i nie przysz³a - wyrzucamy z guzika
            '            rKolumnaZGuzika("pozostawic") = 0
            '        End If
            '    Else
            '        'kolumna odznaczona - zapisujemy do rejestru
            '        objWartoscZRejestru = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Cursor\" & Parametry._NazwaProjektu & "\" & Me.Name, rKolumnaZGuzika("nazwa"), Nothing)
            '        If objWartoscZRejestru Is Nothing Then My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Software\Cursor" & Parametry._NazwaProjektu & "\" & Me.Name, rKolumnaZGuzika("nazwa"), 1)
            '    End If
            'Next

            'sortujemy kolumny i zapisujemy do guzika


            'podœwietlamy ostatnio wybran¹ komórkê, jeœli któraœ by³a wybrana
            If Not intIdWybranegoWiersza Is Nothing Then
                For Each wiersz As DataGridViewRow In dgv.Rows
                    If wiersz.Cells("adres_id").Value = intIdWybranegoWiersza Then
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
            MsgBox("B³¹d wewnêtrzny systemu. Serwer nie przes³a³ listy adresów zdefiniowanych dla u¿ytkownika." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, "Lista adresów zdefiniowanych")
            Me.Close()
        End If
        If wsWynik.iloscStron > 1 Then
            iloscEkranow = wsWynik.iloscStron
        Else
            iloscEkranow = 1
        End If
        numerEkranu = txtNumerEkranu.Text
        lblIloscEkranow.Text = "z " & iloscEkranow

        Return True
    End Function

    Private Sub edytuj(ByVal adres_id As Integer)
        Dim frm As frmAdres = New frmAdres
        frm.frmRodzic = Me
        frm.WindowState = FormWindowState.Normal
        frm.intIdAdresu = adres_id
        frm.intIdUzytkownika = intIdUzytkownika
        frm.strNazwaUzytkownika = strNazwaUzytkownika
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

#Region "Handlery formy"

    Private Sub dgv_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellContentClick
        'If enumTrybPracy = frmAdresyTrybPracy.adresyDoWysylkiGrupowej AndAlso e.ColumnIndex = 0 Then
        '    Dim dgvCell As DataGridViewCheckBoxCell = DirectCast(dgv.Rows(e.RowIndex).Cells(e.ColumnIndex), DataGridViewCheckBoxCell)
        '    If dgvCell.Value Is Nothing OrElse Not dgvCell.Value Then
        '        'zaznaczamy
        '        dgvCell.Value = True
        '        dtAdresy.Rows.Add(dgv.Rows(e.RowIndex).Cells("adres_id").Value)
        '        CType(frmRodzic, frmZamowienie).lblZamowienieGrupoweIlosc.Text = "wybrano " & dtAdresy.Rows.Count & " odbiorców"
        '        CType(frmRodzic, frmZamowienie).btnZapiszZmiany.Enabled = True

        '        'czy wszystkie s¹ zaznaczone?
        '        Dim bWszystkie As Boolean = True
        '        For Each dgvWiersz As DataGridViewRow In dgv.Rows
        '            If dgvWiersz.Cells("adres_id").Value < 0 Then Continue For
        '            If dgvWiersz.Cells(0).Value Is Nothing OrElse dgvWiersz.Cells(0).Value = False Then
        '                bWszystkie = False
        '                Exit For
        '            End If
        '        Next
        '        If bWszystkie Then
        '            bReagujNaCheckBoxHeader = False
        '            DirectCast(dgv.Controls.Find("checkboxHeader", True)(0), CheckBox).Checked = True
        '            bReagujNaCheckBoxHeader = True
        '        End If
        '    Else
        '        'odznaczamy
        '        dgvCell.Value = False
        '        For Each dtRow As DataRow In dtAdresy.Rows
        '            If dtRow("adres_id") = dgv.Rows(e.RowIndex).Cells("adres_id").Value Then
        '                dtRow.Delete()
        '                Exit For
        '            End If
        '        Next
        '        dtAdresy.AcceptChanges()
        '        CType(frmRodzic, frmZamowienie).lblZamowienieGrupoweIlosc.Text = "wybrano " & dtAdresy.Rows.Count & " odbiorców"
        '        CType(frmRodzic, frmZamowienie).btnZapiszZmiany.Enabled = True

        '        bReagujNaCheckBoxHeader = False
        '        DirectCast(dgv.Controls.Find("checkboxHeader", True)(0), CheckBox).Checked = False
        '        bReagujNaCheckBoxHeader = True
        '    End If
        'End If
    End Sub

    Private Sub dgv_checkboxHeaderCheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        'dgv(0, i).Value = DirectCast(dgv.Controls.Find("checkboxHeader", True)(0), CheckBox).Checked
        'dgv.EndEdit()
        If bReagujNaCheckBoxHeader Then
            If dgv.Rows.Count > 0 Then
                'czy wszystkie checkboxy zaznaczone ?
                Dim bWszystkie As Boolean = True
                For Each dgvWiersz As DataGridViewRow In dgv.Rows
                    If dgvWiersz.Cells("adres_id").Value < 0 Then Continue For
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

    Private Sub btnFiltruj_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFiltruj.Click
        txtNumerEkranu.Text = 1
        wczytaj()
    End Sub
    Private Sub btnWyczyscFiltry_Click(sender As System.Object, e As System.EventArgs) Handles btnWyczyscFiltry.Click
        txtFiltruj.Text = ""
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
                MsgBox("Numer ekranu musi byæ liczb¹", MsgBoxStyle.Critical, "B³êdny numer ekranu")
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
            MsgBox("Numer ekranu musi byæ liczb¹", MsgBoxStyle.Critical, "B³êdny numer ekranu")
            e.Cancel = True
            Return
        End If
        If txtNumerEkranu.Text <> numerEkranu Then
            MsgBox("Je¿eli chcesz przejœæ na wpisany ekran, naciœnij Enter, jeœli chcesz wyjœæ z tego pola - naciœnij Escape", MsgBoxStyle.Exclamation, "B³êdny numer ekranu")
            e.Cancel = True
            Return
        End If
    End Sub

    Private Sub btnNastepny_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNastepny.Click
        If txtNumerEkranu.Text >= iloscEkranow Then
            MsgBox("To jest ostatni ekran. Nie mo¿esz przejœæ do nastêpnego ekranu.", MsgBoxStyle.Exclamation, "Ostatni numer ekranu")
            Return
        End If
        txtNumerEkranu.Text = txtNumerEkranu.Text + 1
        wczytaj()
    End Sub

    Private Sub btnOstatni_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOstatni.Click
        If txtNumerEkranu.Text = iloscEkranow Then
            MsgBox("To jest ostatni ekran.", MsgBoxStyle.Exclamation, "Ostatni numer ekranu")
            Return
        End If
        txtNumerEkranu.Text = iloscEkranow
        wczytaj()
    End Sub

    Private Sub btnPoprzedni_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPoprzedni.Click
        If txtNumerEkranu.Text = 1 Then
            MsgBox("To jest pierwszy ekran. Nie mo¿esz przejœæ do poprzedniego ekranu.", MsgBoxStyle.Exclamation, "Pierwszy numer ekranu")
            Return
        End If
        txtNumerEkranu.Text = txtNumerEkranu.Text - 1
        wczytaj()
    End Sub

    Private Sub btnPoczatek_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPoczatek.Click
        If txtNumerEkranu.Text = 1 Then
            MsgBox("To jest pierwszy ekran.", MsgBoxStyle.Exclamation, "Pierwszy numer ekranu")
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
        If e.RowIndex <> -1 Then
            edytuj(dgv.Rows(e.RowIndex).Cells("adres_id").Value)
        End If
    End Sub

    Private Sub dgv_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgv.KeyDown
        If e.KeyCode = Keys.Enter AndAlso Not dgv.CurrentCell Is Nothing Then
            edytuj(dgv.Rows(dgv.CurrentCell.RowIndex).Cells("adres_id").Value)
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

    Private Sub btnNowy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNowy.Click
        Dim frm As New frmAdres
        frm.frmRodzic = Me
        frm.intIdUzytkownika = intIdUzytkownika
        frm.strNazwaUzytkownika = strNazwaUzytkownika
        If Me.Modal Then
            frm.ShowDialog()
        Else
            frm.MdiParent = frmGlowna
            frm.Show()
        End If
    End Sub

    Private Sub btnEdytuj_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdytuj.Click
        If dgv.CurrentCell Is Nothing Then
            MsgBox("Wybierz wiersz do edycji.", MsgBoxStyle.Exclamation, "Brak zaznaczenia wiersza")
            Exit Sub
        End If
        edytuj(dgv.Rows(dgv.CurrentCell.RowIndex).Cells("adres_id").Value)
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

    Private Sub btnUsun_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUsun.Click
        If dgv.CurrentCell Is Nothing Then
            MsgBox("Wybierz wiersz do usuniêcia.", MsgBoxStyle.Exclamation, "Brak zaznaczenia wiersza")
            Exit Sub
        End If

        If dgv.Rows(dgv.CurrentCell.RowIndex).Cells("DOMYSLNY").Value = "Tak" Then
            MsgBox("Usuniêto domyœlny adres!", MsgBoxStyle.Information, Me.Text)
        End If

        usun(dgv.Rows(dgv.CurrentCell.RowIndex).Cells("adres_id").Value)

        
    End Sub
#End Region

    Private Sub btnAnuluj_Click(sender As System.Object, e As System.EventArgs) Handles btnAnuluj.Click
        Me.Close()
    End Sub
End Class