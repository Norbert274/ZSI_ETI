Imports System.Reflection
Imports System
Imports System.IO
Imports OfficeOpenXml
Imports OfficeOpenXml.Style

Public Class frmZamowienia
    Dim dtpZakresDatOd As DateTimePicker
    Dim dtpZakresDatDo As DateTimePicker
    Public frmRodzic As Form
    Private numerEkranu As Integer 'numer bie¿¹cego ekranu
    Private iloscEkranow As Integer 'iloœæ ekranów przy bie¿¹cym filtrze
    Private bReagujNaComboIloscNaStronie As Boolean = False
    Private intNrZamRoboczeDoUsuniecia As Integer = -1
    Dim Parametry As New ZmienneGlobalne
    Private IdZamowienia As Integer = -1
    Dim dtFunkcje As New DataTable

    Private Sub frmZamowienia_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Me.Equals(frmGlowna.frmZamowieniaLista) Then frmGlowna.frmZamowieniaLista = Nothing
    End Sub

    Private Sub frmZamowienia_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Cursor = Cursors.WaitCursor
        'dodanie dtp "Data od"
        dtpZakresDatOd = New DateTimePicker
        dtpZakresDatOd.Size = New System.Drawing.Size(90, 20)
        dtpZakresDatOd.CustomFormat = "yyyy-MM-dd"
        dtpZakresDatOd.Format = DateTimePickerFormat.Custom
        ts.Items.Insert(1, New ToolStripControlHost(dtpZakresDatOd))
        dtpZakresDatOd.Value = DateAdd(DateInterval.Day, -14, Today)

        'dodanie dtp "Data do"
        dtpZakresDatDo = New DateTimePicker
        dtpZakresDatDo.Size = New System.Drawing.Size(90, 20)
        dtpZakresDatDo.CustomFormat = "yyyy-MM-dd"
        dtpZakresDatDo.Format = DateTimePickerFormat.Custom
        ts.Items.Insert(3, New ToolStripControlHost(dtpZakresDatDo))
        dtpZakresDatDo.Value = Today

        'inicjalizacja ustawieñ kontrolek
        numerEkranu = 1
        txtNumerEkranu.Text = numerEkranu
        cmbIloscNaStronie.SelectedIndex = 1
        bReagujNaComboIloscNaStronie = True
        Dim rKey As Microsoft.Win32.RegistryKey = My.Computer.Registry.CurrentUser.OpenSubKey("Software\Cursor\" & Parametry._NazwaProjektu & "\" & Me.Name)
        If Not rKey Is Nothing Then
            Dim strKlucze As String() = rKey.GetValueNames()
            Dim tsKolumna As ToolStripMenuItem
            For Each strKlucz As String In strKlucze
                If rKey.OpenSubKey(strKlucz) Is Nothing Then
                    'jeœli nie da siê tego otworzyæ, czyli jest to wartoœæ to dodajemy j¹ na ToolStripButton
                    tsKolumna = tsKolumny.DropDownItems.Add(strKlucz)
                    tsKolumna.Checked = False
                End If
            Next
        End If

        dtFunkcje = SprawdzUprawnienia(frmGlowna.sesja)

        If Not wczytaj() Then Me.Close()
        wczytajStatusyZamowienia()

        Cursor = Cursors.Default

    End Sub


    Private Function SprawdzUprawnienia(ByVal sesja As Byte()) As DataTable

        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        ' ws.Url = frmGlowna.strWebservice
        Dim wsWynik As wsCursorProf.SprawdzFunkcjeWynik

        Try
            Application.DoEvents()
            wsWynik = ws.SprawdzFunkcje(frmGlowna.sesja)
        Catch ex As Exception
            MsgBox("B³¹d komunikacji z serwerem. " & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegó³y b³êdu:" & ex.Message, MsgBoxStyle.Critical)
            Return Nothing
            Exit Function
        End Try
        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical)
            Return Nothing
            Exit Function
        ElseIf wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation)
        End If
        Return wsWynik.dane.Tables(0)

    End Function

    Public Function wczytaj() As Boolean
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        'ws.Url = frmGlowna.strWebservice
        Dim wsWynik As wsCursorProf.ZamowienieStronaWynik
        Dim sortowanieKolumna As String = ""
        Dim sortowanieNarastajaco As Boolean = True
        Dim intIdWybranegoWiersza = Nothing
        Dim intNumerWybranejKolumny = Nothing

        'sprawdzamy poprawnoœæ danych filtru
        If dtpZakresDatOd.Value > dtpZakresDatDo.Value Then
            MsgBox("B³êdne ustawienie filtru dat: data ""od"" nie mo¿e byæ póŸniejsza od daty ""do""", MsgBoxStyle.Exclamation)
            Return False
        End If

        'zapisujemy id wybranego wiersza i numer wybranej kolumny, jeœli jakiœ wiersz jest wybrany
        If Not dgv.CurrentCell Is Nothing Then
            intIdWybranegoWiersza = dgv.Rows(dgv.CurrentCell.RowIndex).Cells("numer").Value
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

        'budujemy listê ukrytych kolumn
        Dim dsKolumnyUkryte As New DataSet
        dsKolumnyUkryte.Tables.Add()
        dsKolumnyUkryte.Tables(0).Columns.Add("nazwa")
        For Each tsKolumna As ToolStripMenuItem In tsKolumny.DropDownItems
            If Not tsKolumna.Checked Then dsKolumnyUkryte.Tables(0).Rows.Add(tsKolumna.Text)
        Next

        'odczyt listy z serwera
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.ZamowienieStrona(frmGlowna.sesja, txtNumerEkranu.Text, cmbIloscNaStronie.SelectedItem, _
                New Date(dtpZakresDatOd.Value.Year, dtpZakresDatOd.Value.Month, dtpZakresDatOd.Value.Day, 0, 0, 0).ToBinary, _
                New Date(dtpZakresDatDo.Value.Year, dtpZakresDatDo.Value.Month, dtpZakresDatDo.Value.Day, 23, 59, 59).ToBinary, _
                txtFiltruj.Text, sortowanieKolumna, sortowanieNarastajaco, dsKolumnyUkryte, _
                NZ(cboStatusyZamowienia.ComboBox.SelectedValue, -1))
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("B³¹d komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegó³y b³êdu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
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

            FixGridColumns(sortowanieKolumna, sortowanieNarastajaco)

            'synchronizujemy listê ukrytych kolumn z rejestrem
            Dim objWartoscZRejestru
            Dim bByla As Boolean

            'przepisujemy obecnie wypisane kolumny z guzika do DataTable
            Dim dtKolumnyZGuzika As New DataTable
            dtKolumnyZGuzika.Columns.Add("nazwa")
            dtKolumnyZGuzika.Columns.Add("zaznaczona")
            dtKolumnyZGuzika.Columns.Add("pozostawic")
            dtKolumnyZGuzika.Columns.Add("znaleziona")
            For Each tsKolumna As ToolStripMenuItem In tsKolumny.DropDownItems
                dtKolumnyZGuzika.Rows.Add(tsKolumna.Text, tsKolumna.Checked, 1, 0)
            Next
            'przegl¹damy, które kolumny wróci³y w wyniku
            For Each cKolumnaWyniku As DataColumn In wsWynik.dane.Tables(0).Columns
                'czy taka kolumna by³a na guziku?
                bByla = False
                For Each rKolumnaZGuzika As DataRow In dtKolumnyZGuzika.Rows
                    If cKolumnaWyniku.Caption = rKolumnaZGuzika("nazwa") Then
                        'jest na guziku i w wyniku
                        If rKolumnaZGuzika("zaznaczona") Then
                            'kolumna zaznaczona i przysz³a w wyniku - jest ok
                        Else
                            'dziwne - kolumna nie by³a zaznaczona, prosiliœmy aby jej nie przysy³aæ, a przysz³a
                            'trudno - baza ma wy¿szoœæ - pokazujemy t¹ kolumnê
                            rKolumnaZGuzika("zaznaczona") = 1
                        End If
                        rKolumnaZGuzika("znaleziona") = 1
                        bByla = True
                        Exit For
                    End If
                Next
                If Not bByla AndAlso cKolumnaWyniku.Caption.ToLower <> "numer" AndAlso cKolumnaWyniku.Caption.ToLower <> "koszyk" Then
                    'kolumny nie by³o na guziku, a jest w wyniku - dopisujemy na guzik
                    dtKolumnyZGuzika.Rows.Add(cKolumnaWyniku.Caption, 1, 1, 1)
                End If
            Next

            'przegl¹damy tabelê z kolumnami na guziku

            'dopisujemy do rejestru te, które s¹ nie zaznaczone
            For Each rKolumnaZGuzika As DataRow In dtKolumnyZGuzika.Rows
                If rKolumnaZGuzika("zaznaczona") Then
                    If rKolumnaZGuzika("znaleziona") Then
                        'kolumna mia³a przyjœæ i przysz³a - usuwamy z rejestru
                        objWartoscZRejestru = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Cursor\" & Parametry._NazwaProjektu & "\" & Me.Name, rKolumnaZGuzika("nazwa"), Nothing)
                        If Not objWartoscZRejestru Is Nothing Then
                            Dim rKey As Microsoft.Win32.RegistryKey = My.Computer.Registry.CurrentUser.OpenSubKey("Software\Cursor\" & Parametry._NazwaProjektu & "\" & Me.Name, True)
                            rKey.DeleteValue(rKolumnaZGuzika("nazwa"))
                        End If
                    Else
                        'kolumna mia³a przyjœæ i nie przysz³a - wyrzucamy z guzika
                        rKolumnaZGuzika("pozostawic") = 0
                    End If
                Else
                    'kolumna odznaczona - zapisujemy do rejestru
                    objWartoscZRejestru = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Cursor\" & Parametry._NazwaProjektu & "\" & Me.Name, rKolumnaZGuzika("nazwa"), Nothing)
                    If objWartoscZRejestru Is Nothing Then My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Software\Cursor\" & Parametry._NazwaProjektu & "\" & Me.Name, rKolumnaZGuzika("nazwa"), 1)
                End If
            Next

            'sortujemy kolumny i zapisujemy do guzika
            dtKolumnyZGuzika.DefaultView.Sort = "nazwa"
            tsKolumny.DropDownItems.Clear()
            Dim tsNowaKolumna As ToolStripMenuItem
            For Each rKolumnaZGuzika As DataRow In dtKolumnyZGuzika.Rows
                If rKolumnaZGuzika("pozostawic") Then
                    tsNowaKolumna = tsKolumny.DropDownItems.Add(rKolumnaZGuzika("nazwa"))
                    tsNowaKolumna.Checked = rKolumnaZGuzika("zaznaczona")
                End If
            Next

            'podœwietlamy ostatnio wybran¹ komórkê, jeœli któraœ by³a wybrana
            If Not intIdWybranegoWiersza Is Nothing Then
                For Each wiersz As DataGridViewRow In dgv.Rows
                    If wiersz.Cells("numer").Value = intIdWybranegoWiersza Then
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
            MsgBox("B³¹d wewnêtrzny systemu. Serwer nie przes³a³ listy zamówieñ." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
            Me.Close()
        End If
        If wsWynik.ilosc_stron > 1 Then
            iloscEkranow = wsWynik.ilosc_stron
        Else
            iloscEkranow = 1
        End If
        numerEkranu = txtNumerEkranu.Text
        lblIloscEkranow.Text = "z " & iloscEkranow

        Return True
    End Function
    Public Function wczytajStatusyZamowienia() As Boolean
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials

        Dim wsWynik As wsCursorProf.ZamowienieStatusyListaWynik
        'odczyt listy z serwera
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.ZamowienieStatusyLista(frmGlowna.sesja)
            Cursor = Cursors.Default

            If Not wsWynik Is Nothing AndAlso Not wsWynik.dane Is Nothing AndAlso wsWynik.dane.Tables.Count > 0 Then
                cboStatusyZamowienia.ComboBox.DisplayMember = UCase(EnumName(EnumZamowienieStatusyLista.NAZWA))
                cboStatusyZamowienia.ComboBox.ValueMember = UCase(EnumName(EnumZamowienieStatusyLista.ZAMOWIENIE_STATUS_ID))
                cboStatusyZamowienia.ComboBox.DataSource = wsWynik.dane.Tables(0)
                cboStatusyZamowienia.ComboBox.SelectedIndex = -1
                AddHandler cboStatusyZamowienia.SelectedIndexChanged, AddressOf cboStatusyZamowienia_SelectedIndexChanged
            End If

        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("B³¹d komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegó³y b³êdu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
            Return False
        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, Me.Text)
            Return False
        ElseIf wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, Me.Text)
        End If

        Return True
    End Function

    Public Sub odswiezListy()
        wczytaj()
    End Sub


    Private Sub FixGridColumns(ByVal sortowanieKolumna As String, ByVal sortowanieNarastajaco As Boolean)

        For Each gcolumn As DataGridViewColumn In dgv.Columns
            Select Case UCase(gcolumn.Name.Replace(" ", "_"))
                Case UCase(EnumName(EnumZamowienieStrona.KOSZYK))
                    gcolumn.Visible = False
                    Exit Select
                    'Case UCase(EnumName(EnumZamowienieStrona.OSOBA_MODYFIKUJACA))
                    '    gcolumn.HeaderText = "Ostatnio modyfikowa³"
                    '    gcolumn.Width = 300
                    '    Exit Select
                Case UCase(EnumName(EnumZamowienieStrona.NUMER))
                    gcolumn.HeaderText = "Numer"
                    Exit Select
                Case UCase(EnumName(EnumZamowienieStrona.ZLECAJACY))
                    gcolumn.HeaderText = "Zlecaj¹cy"
                    Exit Select
                Case UCase(EnumName(EnumZamowienieStrona.STATUS_ZLECENIA))
                    gcolumn.HeaderText = "Status zlecenia"
                    Exit Select
                Case UCase(EnumName(EnumZamowienieStrona.TYP))
                    gcolumn.HeaderText = "Typ"
                    Exit Select
                Case UCase(EnumName(EnumZamowienieStrona.DATA_ZLOZENIA))
                    gcolumn.HeaderText = "Data z³o¿enia"
                    Exit Select
                Case UCase(EnumName(EnumZamowienieStrona.UWAGI))
                    gcolumn.HeaderText = "Uwagi"
                    Exit Select
                Case UCase(EnumName(EnumZamowienieStrona.NUMER_LISTU_PRZEWOZOWEGO))
                    gcolumn.HeaderText = "Numer listu przewozowego"
                    Exit Select
                Case UCase(EnumName(EnumZamowienieStrona.STATUS_PRZESY£KI))
                    gcolumn.HeaderText = "Status przesy³ki"
                    Exit Select
                Case UCase(EnumName(EnumZamowienieStrona.DATA_OSTATNIEJ_ZMIANY))
                    gcolumn.HeaderText = "Data ostatniej zmiany"
                    Exit Select
                    'Case UCase(EnumName(EnumZamowienieStrona.KOD))
                    '    gcolumn.HeaderText = "Kod pocztowy"
                    '    Exit Select
                    'Case UCase(EnumName(EnumZamowienieStrona.MIASTO))
                    '    gcolumn.HeaderText = "Miasto"
                    '    Exit Select
                    'Case UCase(EnumName(EnumZamowienieStrona.ADRES))
                    '    gcolumn.HeaderText = "Adres"
                    '    Exit Select
                    'Case UCase(EnumName(EnumZamowienieStrona.NAZWA))
                    '    gcolumn.HeaderText = "Nazwa"
                    '    Exit Select
                Case UCase(EnumName(EnumZamowienieStrona.DATA_REALIZACJI))
                    If dgv.Columns.Contains(EnumName(EnumZamowienieStrona.DATA_ZLOZENIA)) = True Then
                        gcolumn.DisplayIndex = dgv.Columns(EnumName(EnumZamowienieStrona.DATA_ZLOZENIA)).DisplayIndex + 1
                    End If
                    gcolumn.HeaderText = "Data realizacji"
                    Exit Select
            End Select
            gcolumn.SortMode = DataGridViewColumnSortMode.Programmatic
            If gcolumn.Name = sortowanieKolumna And sortowanieKolumna <> String.Empty Then
                If sortowanieNarastajaco Then
                    gcolumn.HeaderCell.SortGlyphDirection = SortOrder.Ascending
                    dgv.Sort(gcolumn, System.ComponentModel.ListSortDirection.Ascending)
                Else
                    gcolumn.HeaderCell.SortGlyphDirection = SortOrder.Descending
                    dgv.Sort(gcolumn, System.ComponentModel.ListSortDirection.Descending)
                End If
            End If
        Next
        dgv.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells)
    End Sub


#Region "Handlery formy"

    Private Sub btnFiltruj_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFiltruj.Click
        txtNumerEkranu.Text = 1
        wczytaj()
    End Sub

    Private Sub txtFiltruj_HideSelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFiltruj.HideSelectionChanged

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

    Private Sub dgv_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellClick
        If e.RowIndex > -1 Then
            If dgv.Rows(e.RowIndex).Cells("koszyk").Value = 1 Or _
                (frmGlowna.bBiuro And dgv.Rows(e.RowIndex).Cells("status zlecenia").Value = "ROBOCZE") Then
                btnUsunZamowienieRobocze.Enabled = True
                intNrZamRoboczeDoUsuniecia = dgv.Rows(e.RowIndex).Cells("numer").Value
            Else
                btnUsunZamowienieRobocze.Enabled = False
            End If
        End If
    End Sub
    Private Sub dgv_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellDoubleClick


        'czy klikniêto koszyk?
        If dgv.Rows(e.RowIndex).Cells("koszyk").Value = 1 Then
            'tak! A czy mamy ju¿ otwarty w³asny koszyk?
            If dgv.Rows(e.RowIndex).Cells("typ").Value = "INWENTARYZACJA NA PLUS" Or dgv.Rows(e.RowIndex).Cells("typ").Value = "INWENTARYZACJA NA MINUS" Then
                If Not frmGlowna.frmKoszykINV Is Nothing Then
                    frmGlowna.frmKoszykINV.WindowState = FormWindowState.Normal
                    frmGlowna.frmKoszykINV.Activate()
                    Exit Sub
                End If
            Else
                If Not frmGlowna.frmKoszyk Is Nothing Then
                    frmGlowna.frmKoszyk.WindowState = FormWindowState.Normal
                    frmGlowna.frmKoszyk.Activate()
                    Exit Sub
                End If
            End If
        End If

        If dgv.Rows(e.RowIndex).Cells("typ").Value = "INWENTARYZACJA NA PLUS" Or dgv.Rows(e.RowIndex).Cells("typ").Value = "INWENTARYZACJA NA MINUS" Then
            'jeœli doszliœmy dot¹d, otwieramy normalne zamówienie
            Dim frm As New frmZamowienieINV
            frm.MdiParent = Me.MdiParent
            frm.frmRodzic = Me
            frm.intIdZamowienia = dgv.Rows(e.RowIndex).Cells("numer").Value
            frm.Show()
        Else
            'jeœli doszliœmy dot¹d, otwieramy normalne zamówienie
            Dim frm As New frmZamowienie
            frm.MdiParent = Me.MdiParent
            frm.frmRodzic = Me
            frm.intIdZamowienia = dgv.Rows(e.RowIndex).Cells("numer").Value
            frm.Show()
        End If
       


    End Sub

    Private Sub dgv_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgv.ColumnHeaderMouseClick
        Dim sortowanieKolumna As String
        Dim sortowanieRosnaco As Boolean
        Dim kolumna As DataGridViewColumn
        Dim kolumnaKliknieta As DataGridViewColumn

        kolumnaKliknieta = dgv.Columns(e.ColumnIndex)
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
        wczytaj()

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
    End Sub

    Private Sub tsKolumny_DropDownItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tsKolumny.DropDownItemClicked
        Dim pozycja As ToolStripMenuItem = DirectCast(e.ClickedItem, ToolStripMenuItem)
        If pozycja.Checked Then
            pozycja.Checked = False
        Else
            'zaznaczamy, pokazujemy kolumnê
            pozycja.Checked = True
        End If
    End Sub

    Private Sub btnStworzNoweZamowienie_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStworzNoweZamowienie.Click
        'pokazujemy koszyk; czy koszyk jest ju¿ otwarty?
        If frmGlowna.frmKoszyk Is Nothing Then
            'Nie, koszyk nie jest otwarty - ³adujemy go
            Dim frm As New frmZamowienie
            frm.intIdZamowienia = -1
            frm.MdiParent = Me.MdiParent
            frm.frmRodzic = Me
            frm.Show()
            'frm.cmbZamowienieZMagazynu.Focus()

        Else
            'Tak, koszyk otwarty - pokazujemy go
            frmGlowna.frmKoszyk.WindowState = FormWindowState.Normal
            frmGlowna.frmKoszyk.Activate()
        End If
    End Sub

#End Region


    Private Sub btnAnulujZamowienie_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If dgv.CurrentCell Is Nothing Then
            MessageBox.Show("Wybierz zamówienie do anulowania.", "Anulowanie", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        'ws.Url = frmGlowna.strWebservice
        Dim wsWynik As New wsCursorProf.AnulujZamowienieWynik
        Dim zamowienie_id As Integer = dgv.Rows(dgv.CurrentCell.RowIndex).Cells("NUMER").Value
        Dim result As New DialogResult

        If zamowienie_id = Nothing Then
            MessageBox.Show("Wybierz zamówienie do anulowania.", "Anulowanie", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            result = MessageBox.Show("Czy na pewno chcesz anulowaæ zamówienie: " & zamowienie_id & " ?", "Anulowanie", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = Windows.Forms.DialogResult.Yes Then
                wsWynik = ws.AnulujZamowienie(frmGlowna.sesja, dgv.Rows(dgv.CurrentCell.RowIndex).Cells("NUMER").Value)
            Else
                MessageBox.Show("Zamówienie nie zosta³o anulowane.", "Anulowanie", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

        End If

        If wsWynik.status = -1 Then
            MessageBox.Show(wsWynik.status_opis, "Anulowanie", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            MessageBox.Show(wsWynik.status_opis, "Anulowanie", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        'odœwie¿am ekran
        btnOdswiez_Click(sender, e)
    End Sub

    Private Sub btnUsunZamowienieRobocze_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUsunZamowienieRobocze.Click

        If Not frmGlowna.frmKoszyk Is Nothing Then
            If frmGlowna.frmKoszyk.intIdZamowienia = intNrZamRoboczeDoUsuniecia Then
                MsgBox("Zamówienie które chcesz usunaæ jest aktualnie otwarte w oknie. Zamknij najpierw okno z tym zamówieniem.", MsgBoxStyle.Critical, "Usuwanie zamówienia roboczego")
                Exit Sub
            End If
        End If
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        'ws.Url = frmGlowna.strWebservice
        Dim wsWynik As wsCursorProf.ZamowienieRoboczeUsunWynik


        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.ZamowienieRoboczeUsun(frmGlowna.sesja, intNrZamRoboczeDoUsuniecia)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("B³¹d komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegó³y b³êdu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
            Exit Sub
        End Try
        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, "Usuwanie zamówienia roboczego")
        ElseIf wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, "Usuwanie zamówienia roboczego")
        Else
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Information, "Usuwanie zamówienia roboczego")
        End If

        btnUsunZamowienieRobocze.Enabled = False
        wczytaj()
    End Sub

    Private Sub btnAnuluj_Click(sender As System.Object, e As System.EventArgs) Handles btnAnuluj.Click
        Me.Close()
    End Sub

    Private Sub cboStatusyZamowienia_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles cboStatusyZamowienia.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtNumerEkranu.Text = 1
            wczytaj()
        End If
    End Sub

    Private Sub cboStatusyZamowienia_SelectedIndexChanged(sender As System.Object, e As System.EventArgs)
        'Handler dodawany na koniec wczytajStatusyZamowienia()
        wczytaj()
    End Sub

    Private Sub btnExport_Click(sender As System.Object, e As System.EventArgs) Handles btnExport.Click
        Dim sfd As New SaveFileDialog
        sfd.Filter = "Skoroszyt programu Excel|*.xlsx" '|Plik csv|*.csv"
        sfd.FileName = Me.Text
        If sfd.ShowDialog = Windows.Forms.DialogResult.OK Then
            DumpDGVToExcel(dgv, sfd.FileName, Me.Text)
        End If
    End Sub

    Private Sub DumpDGVToExcel(ByVal view As DataGridView, ByVal filePath As String, ByVal tytul As String)
        Try
            Dim tbl As New DataTable

            For Each col As DataGridViewColumn In view.Columns
                If col.Visible = True Then
                    tbl.Columns.Add(col.HeaderText)
                End If
            Next

            For Each row As DataGridViewRow In view.Rows
                Dim dRow As DataRow = tbl.NewRow()
                For Each cell As DataGridViewCell In row.Cells
                    If cell.Visible = True Then
                        dRow(dgv.Columns(cell.ColumnIndex).HeaderText) = cell.Value
                    Else

                    End If

                Next
                tbl.Rows.Add(dRow)
            Next

            If File.Exists(filePath) Then
                File.Delete(filePath)
            End If
            Dim newFile As New FileInfo(filePath)

            Using pck As New ExcelPackage(newFile)

                'Create the worksheet

                pck.Workbook.Properties.Author = ""
                pck.Workbook.Properties.Title = tytul
                pck.Workbook.Properties.Company = "OEX E-Business"

                Dim ws As ExcelWorksheet = pck.Workbook.Worksheets.Add("Raport")
                Dim ile_kolumn As Integer = tbl.Columns.Count
                Dim i As Integer = 1

                'Load the datatable into the sheet, starting from cell A1. Print the column names on row 1
                ws.Cells("A1").Value = tytul
                ws.Cells("A1").Style.Font.Bold = True
                ws.Cells("A1").Style.Font.Size = 14
                ws.Cells("A3").LoadFromDataTable(tbl, True)

                For i = 0 To ile_kolumn - 1
                    If tbl.Columns(i).ColumnName.ToUpper.Contains("DATA") Then
                        '' formatowanie kolumny w excelu na datê
                        ws.Column(i + 1).Style.Numberformat.Format = "YYYY-MM-DD"
                    End If
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
                MsgBox("Zapisano raport: " & filePath, MsgBoxStyle.Information, Me.Text)

            End Using

        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("B³¹d komunikacji z serwerem. " & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegó³y b³êdu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
            Exit Sub
        End Try
    End Sub

    Private Sub GenerujAwizoZwrotuToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles GenerujAwizoZwrotuToolStripMenuItem.Click
        
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        'ws.Url = frmGlowna.strWebservice
        Dim wsWynik As wsCursorProf.AwizoPrzygotujZwrotWynik

        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.AwizoPrzygotujZwrot(frmGlowna.sesja, IdZamowienia)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("B³¹d komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegó³y b³êdu:" & ex.Message, MsgBoxStyle.Critical)
            Exit Sub
        End Try
        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, "Generowanie awiza zwrotu")
        ElseIf wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, "Generowanie awiza zwrotu")
        Else

            If Not frmGlowna.frmAwizaNew Is Nothing Then
                frmGlowna.frmAwizaNew.WindowState = FormWindowState.Normal
                frmGlowna.frmAwizaNew.Activate()
                frmGlowna.frmAwizaNew.Close()
            End If

            Dim frm As New frmAwizacje
            frmGlowna.frmAwizaNew = frm
            frm.strFunkcjaPowiadomieniaOGotowosci = "odswiezListy"
            frm.awizo_id = -1

            frm.zamowienie_zwrot_id = IdZamowienia
            frm.MdiParent = frmGlowna
            frm.frmRodzic = frmGlowna

            frm.dtListaAwiza = wsWynik.dane.Tables(0)
            frm.dgv.DataSource = frm.dtListaAwiza
            frm.dgv.AutoResizeColumns()

            frm.dgv.Columns.Insert(0, New DataGridViewCheckBoxColumn)
            frm.dgv.Columns(0).Width = 52
            frm.dgv.Columns(0).Name = "wybierz"
            frm.dgv.Columns("sku").Width = 100
            frm.dgv.Columns("nazwa").Width = 390
            frm.dgv.Columns("ilosc").Width = 90
            frm.dgv.Columns("sku").ReadOnly = True
            frm.dgv.Columns("nazwa").ReadOnly = True
            Dim dgvCell As DataGridViewCheckBoxCell

            For Each row As DataGridViewRow In frm.dgv.Rows
                dgvCell = DirectCast(row.Cells(0), DataGridViewCheckBoxCell)
                dgvCell.Value = False
            Next

            Dim cmb As New DataGridViewComboBoxColumn
                cmb.MinimumWidth = 180
                cmb.Width = 180
                cmb.HeaderText = "GRUPA"
            cmb.DataSource = wsWynik.dane.Tables(1)
                cmb.DisplayMember = "GRUPA"
                cmb.ValueMember = "GRUPA_ID"
                cmb.DataPropertyName = "GRUPA_ID"
                cmb.Name = "Id"
            frm.dgv.Columns.Add(cmb)
            frm.dgv.Columns("GRUPA_ID").Visible = False

            frm.txtOsobaKontaktowa.Text = wsWynik.osoba_kontaktowa
            frm.txtTelefonKontaktowy.Text = wsWynik.telefon
            frm.txtUwagi.Text = wsWynik.uwagi
            frm.dostawca_id = wsWynik.dostawca_id
            frm.txtNrPO.Text = wsWynik.numer_po
            frm.dtpPlanowanaDataDostawy.Text = wsWynik.planowana_data_dostawy
            frm.txtIloscPalet.Text = wsWynik.ilosc_palet
            frm.txtIloscPaczek.Text = wsWynik.ilosc_paczek
            frm.Text = "Awizo zwrotu zamówienia nr " & CStr(IdZamowienia)
            frm.btnZapisz.Visible = False
            frm.btnUsunAwizo.Visible = False
            frm.cmbTypDostawy.SelectedValue = 1
            frm.cmbTypDostawy.Enabled = False
            frm.Show()
        End If

    End Sub

    Private Sub dgv_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles dgv.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Dim ht As DataGridView.HitTestInfo
            ht = Me.dgv.HitTest(e.X, e.Y)
            IdZamowienia = dgv.Rows(ht.RowIndex).Cells("numer").Value
            If dtFunkcje.Select("FUNKCJE_ID=39").Length > 0 Then
                If dgv.Rows(ht.RowIndex).Cells("status zlecenia").Value = "ZREALIZOWANE" Or dgv.Rows(ht.RowIndex).Cells("status zlecenia").Value = "ZREALIZOWANE Z ZWROTEM" Then
                    menu_kontekstowe.Items("GenerujAwizoZwrotuToolStripMenuItem").Visible = True
                Else
                    menu_kontekstowe.Items("GenerujAwizoZwrotuToolStripMenuItem").Visible = False
                End If

                If ht.Type = DataGridViewHitTestType.Cell Then
                    dgv.ContextMenuStrip = menu_kontekstowe
                End If
            End If

        End If
    End Sub

End Class