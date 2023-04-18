Imports System.Reflection
Imports System.Data
Imports System.Text
Public Class frmZarzadzanieAwizami

    Public frmRodzic As Form
    Public dtListaAwiz As New DataTable
    Public dtListaStatusy As New DataTable
    Private statusCheked As Boolean = False
    Private filtrTxtBoxy As String = String.Empty
    Private filtrCheckListStatus As String = String.Empty
    Private czy_filtrowac As Boolean = False
    Private intAwizoId As Integer
    Private strQguarZA As String
    Private strQguarDostawa As String
    Private numerEkranu As Integer 'numer bieżącego ekranu
    Private iloscEkranow As Integer 'ilość ekranów przy bieżącym filtrze
    Private bReagujNaComboIloscNaStronie As Boolean = False
    Dim dtFunkcje As New DataTable

    Private Sub frmZarzadzanieAwizami_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        frmGlowna.frmAwizaZarzadzanie = Nothing
    End Sub

    'Private DodajeFiltry As Boolean = False
    Private Sub frmZarzadzanieAwizami_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        'If frmGlowna.frmAwizaZarzadzanie Is Nothing Then
        '    frmGlowna.frmAwizaZarzadzanie = New frmZarzadzanieAwizami
        'Else
        '    frmRodzic.WindowState = FormWindowState.Normal
        '    frmRodzic.Activate()
        '    Me.Close()
        '    Exit Sub
        'End If

        'inicjalizacja ustawień kontrolek
        dtpUtworzoneOd.CustomFormat = "yyyy-MM-dd"
        dtpUtworzoneOd.Format = DateTimePickerFormat.Custom
        dtpUtworzoneOd.Value = DateAdd(DateInterval.Day, -14, Today)

        dtpUtworzoneDo.CustomFormat = "yyyy-MM-dd"
        dtpUtworzoneDo.Format = DateTimePickerFormat.Custom
        dtpUtworzoneDo.Value = Today

        numerEkranu = 1
        txtNumerEkranu.Text = numerEkranu
        cmbIloscNaStronie.Text = 25
        'cmbIloscNaStronie.SelectedIndex = 1
        bReagujNaComboIloscNaStronie = True

        dtFunkcje = SprawdzUprawnienia(frmGlowna.sesja)

        If Not wczytaj() Then
            ' Me.Activate()
            'Me.Close()
        End If
        czy_filtrowac = True
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
            MsgBox("Błąd komunikacji z serwerem. " & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical)
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

    Private Function wczytaj() As Boolean

        Dim sortowanieKolumna As String = ""
        Dim sortowanieNarastajaco As Boolean = True
        Dim intIdWybranegoWiersza = Nothing
        Dim intNumerWybranejKolumny = Nothing

        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        ' ws.Url = frmGlowna.strWebservice
        Dim wsWynik As wsCursorProf.AwizaListaWczytajWynik

        'czy sortujemy po jakiejś kolumnie?
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

        ' zbieramy zaznaczone statusy awiza
        Dim strXmlStatusy As New StringBuilder
        Dim i As Integer
        For i = 0 To chkListStatus.Items.Count - 1
            If chkListStatus.GetItemChecked(i) = True Then
                strXmlStatusy.Append("<row status_awiza=""" & String.Format("{0}", chkListStatus.GetItemText(chkListStatus.Items(i))) & """ />")
            End If
        Next

        ' czy wczytujemy dane przy otwarciu formy?
        If Not czy_filtrowac Then
            dtpPlanowanaOd.Value = DateAdd(DateInterval.Month, -12, Today)
            dtpPlanowanaDo.Value = DateAdd(DateInterval.Day, 14, Today)
            sortowanieKolumna = "awizo_id"
            sortowanieNarastajaco = False
        End If

        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.AwizaListaWczytaj(frmGlowna.sesja, _
                New Date(dtpUtworzoneOd.Value.Year, dtpUtworzoneOd.Value.Month, dtpUtworzoneOd.Value.Day, 0, 0, 0).ToBinary, _
                New Date(dtpUtworzoneDo.Value.Year, dtpUtworzoneDo.Value.Month, dtpUtworzoneDo.Value.Day, 23, 59, 59).ToBinary, _
                New Date(dtpPlanowanaOd.Value.Year, dtpPlanowanaOd.Value.Month, dtpPlanowanaOd.Value.Day, 0, 0, 0).ToBinary, _
                New Date(dtpPlanowanaDo.Value.Year, dtpPlanowanaDo.Value.Month, dtpPlanowanaDo.Value.Day, 23, 59, 59).ToBinary, _
                txtNrAwiza.Text, txtNrPO.Text, txtDostawca.Text, txtQGUAR_ZA.Text, txtQGUAR_DOSTAWA.Text, _
                sortowanieKolumna, txtNumerEkranu.Text, cmbIloscNaStronie.SelectedItem, sortowanieNarastajaco, strXmlStatusy.ToString, txtSKU.Text)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, "Wczytanie listy awiz")
            Me.Close()
            Return False
            Exit Function
        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, "Wczytanie listy awiz")
            Me.Close()
            Return False
        End If
        If wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, "Wczytanie listy awiz")
            Me.Close()
            Return False
        End If

        If wsWynik.dane.Tables(0).Rows.Count < 1 Then
            If czy_filtrowac Then
                MsgBox("Nie znaleziono awiz w systemie spełniających warunki filtrowania.", MsgBoxStyle.Information, "Filtrowanie awiz")
            Else
                MsgBox("Brak awiz w systemie.", MsgBoxStyle.Exclamation, "Brak awiz")
            End If
            Return False
        End If
        If wsWynik.status = 0 Then
            dgv.DataSource = Nothing
            dgv.Rows.Clear()
            dgv.Columns.Clear()
            dgv.Refresh()
            dtListaAwiz = wsWynik.dane.Tables(0)
            dtListaStatusy = wsWynik.dane.Tables(1)
            'dtpPlanowanaOd.Text = wsWynik.dane.Tables(2).Rows(0).Item("PLANOWANA_DATA_DOSTAWY")
            'dtpPlanowanaDo.Text = wsWynik.dane.Tables(3).Rows(0).Item("PLANOWANA_DATA_DOSTAWY")
            'dtpUtworzoneOd.Text = wsWynik.dane.Tables(4).Rows(0).Item("DATA_OD")
            'dtpUtworzoneDo.Text = wsWynik.dane.Tables(5).Rows(0).Item("DATA_OD")
            dgv.Refresh()
            dgv.DataSource = wsWynik.dane.Tables(0)
            If dgv.Columns.Contains("kolejnosc") Then dgv.Columns("kolejnosc").Visible = False
            dgv.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells)
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

            Me.Refresh()
            Dim dgvc As DataGridViewButtonCell
            If Not dgv.Columns.Contains("Szczegóły/Edycja") Then dgv.Columns.Insert(0, New DataGridViewButtonColumn)
            dgv.Columns(0).Name = "Szczegóły/Edycja"

            For Each row As DataGridViewRow In dgv.Rows
                If row.Cells("user_id").Value = frmGlowna.intIdUzytkownikZalogowany And _
                    row.Cells("status").Value = "ZAPISANE" Then
                    dgvc = DirectCast(row.Cells(0), DataGridViewButtonCell)
                    dgvc.Value = "Edytuj"
                    dgvc.Style.ForeColor = Color.Gray
                Else
                    dgvc = DirectCast(row.Cells(0), DataGridViewButtonCell)
                    dgvc.Value = "Pokaż szczegóły"
                    dgvc.Style.ForeColor = Color.Gray
                End If
            Next

            For Each col As DataGridViewColumn In dgv.Columns

                If col.Name = "awizo_id" Then
                    col.Width = 70
                End If
                If col.Name = "status" Then
                    col.Width = 85
                End If
                If col.Name = "USER_ID" Then
                    col.Visible = False
                End If
                If col.Name = "Szczegóły/Edycja" Then
                    col.Width = 100
                    'col.DefaultCellStyle.ForeColor = Color.DarkGray
                    col.DefaultCellStyle.Font = New Font("Arial", 8, FontStyle.Regular, GraphicsUnit.Point)
                End If
                If col.Name = "data_utworzenia_awiza" Or col.Name = "planowana_data_dostawy" Then
                    col.Width = 140
                End If
                If col.Name = "NUMER_PO" Then
                    col.Width = 110
                End If
                If col.Name = "QGUAR_DOSTAWA" Then
                    col.Width = 110
                End If
                If col.Name = "QGUAR_ZA" Then
                    col.Width = 100
                End If
            Next

            czy_filtrowac = False
            Dim ile_statusow As Integer = chkListStatus.Items.Count
            '' dodajemy do checkboxlist nazwy statusów, które występują w liście awiz
            Dim lstStatusyChecked As New List(Of String)
            If ile_statusow > 0 Then
                For i = ile_statusow - 1 To 0 Step -1
                    If chkListStatus.GetItemChecked(i) = True Then
                        lstStatusyChecked.Add(String.Format("{0}", chkListStatus.GetItemText(chkListStatus.Items(i))))
                    End If
                    chkListStatus.Items.RemoveAt(i)
                Next i

                For Each dtRow As DataRow In dtListaStatusy.Rows
                    If lstStatusyChecked.Contains(dtRow("status").ToString) Then
                        chkListStatus.Items.Add(dtRow("status").ToString, True)
                    Else
                        chkListStatus.Items.Add(dtRow("status").ToString, False)
                    End If

                Next

            Else
                For Each dtRow As DataRow In dtListaStatusy.Rows
                    chkListStatus.Items.Add(dtRow("status").ToString, True)
                Next
            End If
            czy_filtrowac = True
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

    Private Sub UstawWartosciDomyslneFiltrow()
        txtNrAwiza.Text = ""
        txtNrPO.Text = ""
        txtDostawca.Text = ""
        txtQGUAR_ZA.Text = ""
        txtQGUAR_DOSTAWA.Text = ""
        txtSKU.Text = ""
        dtpPlanowanaOd.Value = DateAdd(DateInterval.Month, -12, Today)
        dtpPlanowanaDo.Value = DateAdd(DateInterval.Day, 14, Today)
        dtpUtworzoneOd.Value = DateAdd(DateInterval.Day, -14, Today)
        dtpUtworzoneDo.Value = Today

        Dim i As Integer
        For i = 0 To chkListStatus.Items.Count - 1
            chkListStatus.SetItemCheckState(i, CheckState.Checked)
        Next
    End Sub
    'Private Sub btnSzczegoly_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSzczegoly.Click

    '    Dim ile_zaznaczono As Integer = 0
    '    Dim i As Integer
    '    Dim id_awiza As Integer = 0
    '    Dim ile_rows As Integer = dgv.Rows.Count
    '    If ile_rows = 0 Then
    '        MsgBox("W bazie danych nie ma żadnych awiz.", MsgBoxStyle.Exclamation, "Brak awiz")
    '        Exit Sub
    '    End If
    '    For i = 0 To ile_rows - 1
    '        If dgv.Rows(i).Cells("wybierz").Value = True Then
    '            ile_zaznaczono = ile_zaznaczono + 1
    '            id_awiza = dgv.Rows(i).Cells("AWIZO_ID").Value
    '        End If
    '    Next

    '    If ile_zaznaczono > 1 Then
    '        MsgBox("Zaznaczono więcej niż jedną pozycję. Aby zobaczyć szczegóły awiza należy zaznaczyć w kolumnie (wybierz) tylko jedno awizo.", MsgBoxStyle.Exclamation, "Zaznacz jedną pozycję")
    '    ElseIf ile_zaznaczono = 1 Then
    '        Dim frm As New frmAwizaSzczegoly
    '        frm.awizo_id = id_awiza
    '        frm.Show()
    '    End If
    'End Sub

    Private Sub dgv_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellContentClick
        Dim id_awiza As Integer = 0
        If dgv.Columns(e.ColumnIndex).Name = "Szczegóły/Edycja" And e.RowIndex > -1 Then
            id_awiza = CInt(dgv.Rows(e.RowIndex).Cells("AWIZO_ID").Value)
            '' sprawdzamy czy pokazujemy formę do edycji awiza czy do podglądu

            If dgv.Rows(e.RowIndex).Cells("Szczegóły/Edycja").Value = "Edytuj" Then

                If frmGlowna.frmAwizaNew Is Nothing Then
                    'Nie, okno nie jest otwarte - ładujemy je
                    Dim frm As New frmAwizacje
                    frmGlowna.frmAwizaNew = frm
                    frm.strFunkcjaPowiadomieniaOGotowosci = "odswiezListy"
                    frm.awizo_id = id_awiza
                    frm.MdiParent = frmGlowna
                    frm.frmRodzic = frmGlowna
                    frm.Show()
                Else
                    'Tak, koszyk otwarty - pokazujemy go
                    frmGlowna.frmAwizaNew.WindowState = FormWindowState.Normal
                    frmGlowna.frmAwizaNew.Activate()
                    frmGlowna.frmAwizaNew.Close()

                    Dim frm1 As New frmAwizacje
                    frmGlowna.frmAwizaNew = frm1
                    frm1.strFunkcjaPowiadomieniaOGotowosci = "odswiezListy"
                    frm1.awizo_id = id_awiza
                    frm1.MdiParent = frmGlowna
                    frm1.frmRodzic = frmGlowna
                    frm1.Show()
                End If
            Else
                Dim frm2 As New frmAwizaSzczegoly
                frm2.awizo_id = id_awiza
                frm2.ShowDialog()
            End If

        End If


    End Sub

    Private Sub dgv_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgv.Sorted

        'Dim dgvc As DataGridViewButtonCell
        ''dgv.Columns.Insert(0, New DataGridViewButtonColumn)
        'dgv.Columns(0).Name = "Szczegóły awiza"
        'For Each row As DataGridViewRow In dgv.Rows
        '    dgvc = DirectCast(row.Cells(0), DataGridViewButtonCell)
        '    dgvc.Value = "Pokaż szczegóły"
        '    dgvc.InheritedStyle.ForeColor = Color.Gray
        'Next

        'For Each col As DataGridViewColumn In dgv.Columns
        '    If col.Name = "AWIZO_ID" Then
        '        col.Visible = False
        '    End If
        '    If col.Name = "Szczegóły awiza" Then
        '        col.Width = 100
        '        'col.DefaultCellStyle.ForeColor = Color.DarkGray
        '        col.DefaultCellStyle.Font = New Font("Arial", 8, FontStyle.Regular, GraphicsUnit.Point)
        '    End If
        '    If col.Name = "data_utworzenia_awiza" Or col.Name = "planowana_data_dostawy" Then
        '        col.Width = 140
        '    End If
        '    If col.Name = "NUMER_PO" Or col.Name = "status" Then
        '        col.Width = 110
        '    End If
        'Next

        Dim dgvc As DataGridViewButtonCell
        'dgv.Columns.Insert(0, New DataGridViewButtonColumn)
        If Not dgv.Columns.Contains("Szczegóły/Edycja") Then dgv.Columns.Insert(0, New DataGridViewButtonColumn)
        dgv.Columns(0).Name = "Szczegóły/Edycja"
        For Each row As DataGridViewRow In dgv.Rows
            If row.Cells("user_id").Value = frmGlowna.intIdUzytkownikZalogowany And _
                row.Cells("status").Value = "ZAPISANE" Then
                dgvc = DirectCast(row.Cells(0), DataGridViewButtonCell)
                dgvc.Value = "Edytuj"
                dgvc.Style.ForeColor = Color.Gray
            Else
                dgvc = DirectCast(row.Cells(0), DataGridViewButtonCell)
                dgvc.Value = "Pokaż szczegóły"
                dgvc.Style.ForeColor = Color.Gray
            End If

        Next



        For Each col As DataGridViewColumn In dgv.Columns
            'If col.Name = "AWIZO_ID" Then
            '    col.Visible = False
            'End If
            If col.Name = "USER_ID" Then
                col.Visible = False
            End If
            If col.Name = "Szczegóły/Edycja" Then
                col.Width = 100
                'col.DefaultCellStyle.ForeColor = Color.DarkGray
                col.DefaultCellStyle.Font = New Font("Arial", 8, FontStyle.Regular, GraphicsUnit.Point)
            End If
            If col.Name = "data_utworzenia_awiza" Or col.Name = "planowana_data_dostawy" Then
                col.Width = 140
            End If
            If col.Name = "NUMER_PO" Or col.Name = "status" Then
                col.Width = 110
            End If
        Next

    End Sub

    'Private Sub FiltrujWgStatus()
    '    If czy_filtrowac Then


    '        Dim dv As New DataView(dtListaAwiz)
    '        Dim filtr As String = String.Empty
    '        ' Dim filtrStatus As String = String.Empty
    '        Dim StatusZaznaczony As Boolean = False
    '        If filtrCheckListStatus <> String.Empty Then
    '            filtrCheckListStatus = String.Empty
    '        End If
    '        For i As Integer = 0 To chkListStatus.Items.Count - 1
    '            If chkListStatus.GetItemChecked(i) = True Then
    '                StatusZaznaczony = True
    '                If filtrCheckListStatus.Length > 0 Then
    '                    filtrCheckListStatus = filtrCheckListStatus + ", "
    '                End If
    '                filtrCheckListStatus = filtrCheckListStatus + String.Format("'{0}'", chkListStatus.GetItemText(chkListStatus.Items(i)))
    '            End If
    '        Next
    '        If StatusZaznaczony = False Then
    '            filtrCheckListStatus = "(1=0)"
    '        Else
    '            filtrCheckListStatus = String.Format("(status IN ({0}))", filtrCheckListStatus)
    '        End If
    '        If filtrTxtBoxy <> String.Empty Then
    '            filtr = filtrCheckListStatus & " AND " & filtrTxtBoxy
    '        Else
    '            filtr = filtrCheckListStatus
    '        End If
    '        'filtr = filtrStatus
    '        'dv.RowFilter = filtr
    '        'dgv.DataSource = dv
    '        'dgv.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells)
    '        'Dim dgvc As DataGridViewButtonCell
    '        ''dgv.Columns.Insert(0, New DataGridViewButtonColumn)
    '        'dgv.Columns(0).Name = "Szczegóły awiza"
    '        'For Each row As DataGridViewRow In dgv.Rows
    '        '    dgvc = DirectCast(row.Cells(0), DataGridViewButtonCell)
    '        '    dgvc.Value = "Pokaż szczegóły"
    '        '    dgvc.InheritedStyle.ForeColor = Color.Gray
    '        'Next

    '        dv.RowFilter = filtr
    '        dgv.DataSource = dv
    '        dgv.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells)
    '        Dim dgvc As DataGridViewButtonCell
    '        'dgv.Columns.Insert(0, New DataGridViewButtonColumn)
    '        If Not dgv.Columns.Contains("Szczegóły/Edycja") Then dgv.Columns.Insert(0, New DataGridViewButtonColumn)
    '        dgv.Columns(0).Name = "Szczegóły/Edycja"
    '        For Each row As DataGridViewRow In dgv.Rows
    '            If row.Cells("user_id").Value = frmGlowna.intIdUzytkownikZalogowany And _
    '                row.Cells("status").Value = "ZAPISANE" Then
    '                dgvc = DirectCast(row.Cells(0), DataGridViewButtonCell)
    '                dgvc.Value = "Edytuj"
    '                dgvc.Style.ForeColor = Color.Gray
    '            Else
    '                dgvc = DirectCast(row.Cells(0), DataGridViewButtonCell)
    '                dgvc.Value = "Pokaż szczegóły"
    '                dgvc.Style.ForeColor = Color.Gray
    '            End If

    '        Next
    '    End If
    'End Sub

    'Private Sub Filtruj()
    '    Dim dv As New DataView(dtListaAwiz)
    '    Dim filtr As String = String.Empty
    '    Dim filtrNrAwiza As String = String.Empty
    '    Dim filtrNrPO As String = String.Empty
    '    Dim filtrQGUAR_DOSTAWA As String = String.Empty
    '    Dim filtrQGUAR_ZA As String = String.Empty
    '    Dim filtrDaty As String = "planowana_data_dostawy >= '" & dtpPlanowanaOd.Text.ToString & _
    '                              "' AND planowana_data_dostawy <= '" & dtpPlanowanaDo.Text.ToString & _
    '                              "' AND data_utworzenia_awiza >= '" & dtpUtworzoneOd.Text.ToString & _
    '                              "' AND data_utworzenia_awiza <= '" & dtpUtworzoneDo.Text.ToString & "'"

    '    If txtNrAwiza.Text.ToString <> "" Then
    '        filtrNrAwiza = "(awizo_id LIKE '%" & txtNrAwiza.Text.ToString & "%')"
    '    Else
    '        filtrNrAwiza = String.Empty
    '    End If

    '    If txtNrPO.Text <> "" Then
    '        filtrNrPO = "(numer_PO LIKE '%" & txtNrPO.Text & "%')"
    '    Else
    '        filtrNrPO = String.Empty
    '    End If
    '    If txtQGUAR_DOSTAWA.Text <> "" Then
    '        filtrQGUAR_DOSTAWA = "(QGUAR_DOSTAWA LIKE '%" & txtQGUAR_DOSTAWA.Text & "%')"
    '    Else
    '        filtrQGUAR_DOSTAWA = String.Empty
    '    End If

    '    If txtQGUAR_ZA.Text <> "" Then
    '        filtrQGUAR_ZA = "(QGUAR_ZA LIKE '%" & txtQGUAR_ZA.Text & "%')"
    '    Else
    '        filtrQGUAR_ZA = String.Empty
    '    End If

    '    If filtrNrAwiza <> String.Empty And filtrNrPO <> String.Empty And filtrQGUAR_DOSTAWA <> String.Empty And filtrQGUAR_ZA <> String.Empty Then
    '        filtrTxtBoxy = filtrNrAwiza & " AND " & filtrNrPO & " AND " & filtrQGUAR_DOSTAWA & " AND " & filtrQGUAR_ZA
    '    ElseIf filtrNrAwiza <> String.Empty And filtrNrPO <> String.Empty And filtrQGUAR_DOSTAWA <> String.Empty And filtrQGUAR_ZA = String.Empty Then
    '        filtrTxtBoxy = filtrNrAwiza & " AND " & filtrNrPO & " AND " & filtrQGUAR_DOSTAWA
    '    ElseIf filtrNrAwiza <> String.Empty And filtrNrPO <> String.Empty And filtrQGUAR_DOSTAWA = String.Empty And filtrQGUAR_ZA = String.Empty Then
    '        filtrTxtBoxy = filtrNrAwiza & " AND " & filtrNrPO
    '    ElseIf filtrNrAwiza <> String.Empty And filtrNrPO = String.Empty And filtrQGUAR_DOSTAWA = String.Empty And filtrQGUAR_ZA = String.Empty Then
    '        filtrTxtBoxy = filtrNrAwiza
    '    ElseIf filtrNrAwiza <> String.Empty And filtrNrPO <> String.Empty And filtrQGUAR_DOSTAWA = String.Empty And filtrQGUAR_ZA <> String.Empty Then
    '        filtrTxtBoxy = filtrNrAwiza & " AND " & filtrNrPO & " AND " & filtrQGUAR_ZA
    '    ElseIf filtrNrAwiza <> String.Empty And filtrNrPO = String.Empty And filtrQGUAR_DOSTAWA <> String.Empty And filtrQGUAR_ZA <> String.Empty Then
    '        filtrTxtBoxy = filtrNrAwiza & " AND " & filtrQGUAR_DOSTAWA & " AND " & filtrQGUAR_ZA
    '    ElseIf filtrNrAwiza = String.Empty And filtrNrPO <> String.Empty And filtrQGUAR_DOSTAWA <> String.Empty And filtrQGUAR_ZA <> String.Empty Then
    '        filtrTxtBoxy = filtrNrPO & " AND " & filtrQGUAR_DOSTAWA & " AND " & filtrQGUAR_ZA

    '    ElseIf filtrNrAwiza = String.Empty And filtrNrPO <> String.Empty And filtrQGUAR_DOSTAWA = String.Empty And filtrQGUAR_ZA = String.Empty Then
    '        filtrTxtBoxy = filtrNrPO
    '    ElseIf filtrNrAwiza = String.Empty And filtrNrPO = String.Empty And filtrQGUAR_DOSTAWA <> String.Empty And filtrQGUAR_ZA = String.Empty Then
    '        filtrTxtBoxy = filtrQGUAR_DOSTAWA
    '    ElseIf filtrNrAwiza = String.Empty And filtrNrPO = String.Empty And filtrQGUAR_DOSTAWA = String.Empty And filtrQGUAR_ZA <> String.Empty Then
    '        filtrTxtBoxy = filtrQGUAR_ZA

    '    ElseIf filtrNrAwiza <> String.Empty And filtrNrPO <> String.Empty And filtrQGUAR_DOSTAWA = String.Empty And filtrQGUAR_ZA = String.Empty Then
    '        filtrTxtBoxy = filtrNrAwiza & " AND " & filtrNrPO
    '    ElseIf filtrNrAwiza = String.Empty And filtrNrPO = String.Empty And filtrQGUAR_DOSTAWA <> String.Empty And filtrQGUAR_ZA <> String.Empty Then
    '        filtrTxtBoxy = filtrQGUAR_DOSTAWA & " AND " & filtrQGUAR_ZA

    '    ElseIf filtrNrAwiza <> String.Empty And filtrNrPO = String.Empty And filtrQGUAR_DOSTAWA = String.Empty And filtrQGUAR_ZA <> String.Empty Then
    '        filtrTxtBoxy = filtrNrAwiza & " AND " & filtrQGUAR_ZA
    '    ElseIf filtrNrAwiza = String.Empty And filtrNrPO <> String.Empty And filtrQGUAR_DOSTAWA = String.Empty And filtrQGUAR_ZA <> String.Empty Then
    '        filtrTxtBoxy = filtrNrPO & " AND " & filtrQGUAR_ZA
    '    ElseIf filtrNrAwiza = String.Empty And filtrNrPO <> String.Empty And filtrQGUAR_DOSTAWA <> String.Empty And filtrQGUAR_ZA = String.Empty Then
    '        filtrTxtBoxy = filtrNrPO & " AND " & filtrQGUAR_DOSTAWA
    '    ElseIf filtrNrAwiza <> String.Empty And filtrNrPO = String.Empty And filtrQGUAR_DOSTAWA <> String.Empty And filtrQGUAR_ZA = String.Empty Then

    '        filtrTxtBoxy = filtrNrAwiza & " AND " & filtrQGUAR_DOSTAWA
    '    Else
    '        filtrTxtBoxy = String.Empty
    '    End If

    '    'Dim dv As New DataView(dtSKU)
    '    'Dim filtr As String = String.Empty
    '    'If txtWyszukajSKU.Text <> "" Then
    '    '    filtr = "(sku LIKE '%" & txtWyszukajSKU.Text & "%')"
    '    'Else
    '    '    filtr = String.Empty
    '    'End If

    '    'dv.RowFilter = filtr
    '    'dgv.DataSource = dv

    '    'If chbDostepne.Checked = True And chbZeZdjeciem.Checked = True Then
    '    '    filtr = "(dostepne <> '0' AND zdjecie='TAK')"
    '    'ElseIf chbDostepne.Checked = False And chbZeZdjeciem.Checked = True Then
    '    '    filtr = "(zdjecie='TAK')"
    '    'ElseIf chbDostepne.Checked = True And chbZeZdjeciem.Checked = False Then
    '    '    filtr = "(dostepne <> '0')"
    '    'ElseIf chbDostepne.Checked = False And chbZeZdjeciem.Checked = False Then
    '    '    filtr = String.Empty
    '    'Else
    '    '    filtr = String.Empty
    '    'End If

    '    'filtr = CStr(filtrNrAwiza) & " AND " & filtrNrPO & " AND " & filtrQGUAR_DOSTAWA & " AND " & filtrQGUAR_ZA
    '    If filtrCheckListStatus <> String.Empty And filtrTxtBoxy <> String.Empty Then
    '        filtr = filtrTxtBoxy & " AND " & filtrCheckListStatus & " AND " & filtrDaty
    '    ElseIf filtrCheckListStatus = String.Empty And filtrTxtBoxy <> String.Empty Then
    '        filtr = filtrTxtBoxy & " AND " & filtrDaty
    '    ElseIf filtrCheckListStatus <> String.Empty And filtrTxtBoxy = String.Empty Then
    '        filtr = filtrCheckListStatus & " AND " & filtrDaty
    '    ElseIf filtrCheckListStatus = String.Empty And filtrTxtBoxy = String.Empty Then
    '        filtr = filtrDaty
    '    End If

    '    dv.RowFilter = filtr
    '    dgv.DataSource = dv
    '    dgv.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells)
    '    Dim dgvc As DataGridViewButtonCell

    '    'Dim czy_jest_col_SzczegEdycja As Boolean = False
    '    'For Each col As DataGridViewColumn In dgv.Columns
    '    '    If col.Name = "Szczegóły/Edycja" Then
    '    '        czy_jest_col_SzczegEdycja = True
    '    '    End If
    '    'Next

    '    'If Not czy_jest_col_SzczegEdycja Then
    '    '    dgv.Columns.Insert(0, New DataGridViewButtonColumn)
    '    'End If
    '    If Not dgv.Columns.Contains("Szczegóły/Edycja") Then dgv.Columns.Insert(0, New DataGridViewButtonColumn)

    '    dgv.Columns(0).Name = "Szczegóły/Edycja"
    '    For Each row As DataGridViewRow In dgv.Rows
    '        If row.Cells("user_id").Value = frmGlowna.intIdUzytkownikZalogowany And _
    '            row.Cells("status").Value = "ZAPISANE" Then
    '            dgvc = DirectCast(row.Cells(0), DataGridViewButtonCell)
    '            dgvc.Value = "Edytuj"
    '            dgvc.Style.ForeColor = Color.Gray
    '        Else
    '            dgvc = DirectCast(row.Cells(0), DataGridViewButtonCell)
    '            dgvc.Value = "Pokaż szczegóły"
    '            dgvc.Style.ForeColor = Color.Gray
    '        End If
    '    Next

    '    'dv.RowFilter = filtr
    '    'dgv.DataSource = dv
    '    'dgv.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells)
    '    'Dim dgvc As DataGridViewButtonCell
    '    ''dgv.Columns.Insert(0, New DataGridViewButtonColumn)
    '    'dgv.Columns(0).Name = "Szczegóły awiza"
    '    'For Each row As DataGridViewRow In dgv.Rows
    '    '    dgvc = DirectCast(row.Cells(0), DataGridViewButtonCell)
    '    '    dgvc.Value = "Pokaż szczegóły"
    '    '    dgvc.InheritedStyle.ForeColor = Color.Gray
    '    'Next
    'End Sub

    'Private Sub txtNrAwiza_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNrAwiza.TextChanged
    '    'Filtruj()
    '    txtNumerEkranu.Text = 1
    '    wczytaj()
    'End Sub

    'Private Sub txtNrPO_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNrPO.TextChanged
    '    'Filtruj()
    '    txtNumerEkranu.Text = 1
    '    wczytaj()
    'End Sub

    'Private Sub txtQGUAR_DOSTAWA_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtQGUAR_DOSTAWA.TextChanged
    '    'Filtruj()
    '    txtNumerEkranu.Text = 1
    '    wczytaj()
    'End Sub

    'Private Sub txtQGUAR_ZA_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtQGUAR_ZA.TextChanged
    '    'Filtruj()
    '    txtNumerEkranu.Text = 1
    '    wczytaj()
    'End Sub

    'Private Sub chkListStatus_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles chkListStatus.ItemCheck
    '    If czy_filtrowac Then ''statusCheked = False
    '        'statusCheked = True
    '        'chkListStatus.SetSelected(e.Index, True)
    '        'chkListStatus.SetItemCheckState(e.Index, e.NewValue)
    '        txtNumerEkranu.Text = 1
    '        wczytaj()
    '        'FiltrujWgStatus()
    '        'statusCheked = False
    '    End If

    '    txtNumerEkranu.Text = 1
    '    wczytaj()

    'End Sub

    'Private Sub dtpPlanowanaOd_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpPlanowanaOd.TextChanged
    '    If czy_filtrowac Then
    '        'Filtruj()
    '        txtNumerEkranu.Text = 1
    '        wczytaj()
    '    End If
    'End Sub

    'Private Sub dtpPlanowanaDo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpPlanowanaDo.TextChanged
    '    If czy_filtrowac Then
    '        'Filtruj()
    '        txtNumerEkranu.Text = 1
    '        wczytaj()
    '    End If
    'End Sub

    'Private Sub dtpUtworzoneOd_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpUtworzoneOd.TextChanged
    '    If czy_filtrowac Then
    '        'Filtruj()
    '        txtNumerEkranu.Text = 1
    '        wczytaj()
    '    End If
    'End Sub

    'Private Sub dtpUtworzoneDo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpUtworzoneDo.TextChanged
    '    If czy_filtrowac Then
    '        'Filtruj()
    '        txtNumerEkranu.Text = 1
    '        wczytaj()
    '    End If
    'End Sub

    Public Sub odswiezListy()
        wczytaj()
        Me.WindowState = FormWindowState.Normal
        Me.Activate()
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub btnZamknij_Click(sender As System.Object, e As System.EventArgs) Handles btnZamknij.Click
        Me.Close()
    End Sub

    Private Sub txtNumerEkranu_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtNumerEkranu.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim intNumerEkranu As Integer
            If Not Integer.TryParse(txtNumerEkranu.Text, intNumerEkranu) Then
                MsgBox("Numer ekranu musi być liczbą", MsgBoxStyle.Critical, "Niepoprawny numer ekranu")
                Return
            End If
            If intNumerEkranu = numerEkranu Then Return 'użytkownik nie zmienił numeru ekranu, nic nie robimy
            wczytaj()
        End If
        If e.KeyCode = Keys.Escape Then
            txtNumerEkranu.Text = numerEkranu
        End If
    End Sub

    Private Sub txtNumerEkranu_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtNumerEkranu.Validating
        Dim intNumerEkranu As Integer
        If Not Integer.TryParse(txtNumerEkranu.Text, intNumerEkranu) Then
            MsgBox("Numer ekranu musi być liczbą", MsgBoxStyle.Critical, "Niepoprawny numer ekranu")
            e.Cancel = True
            Return
        End If
        If txtNumerEkranu.Text <> numerEkranu Then
            MsgBox("Jeżeli chcesz przejść na wpisany ekran, naciśnij Enter, jeśli chcesz wyjść z tego pola - naciśnij Escape", MsgBoxStyle.Exclamation, "Numer ekranu")
            e.Cancel = True
            Return
        End If
    End Sub

    Private Sub cmbIloscNaStronie_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbIloscNaStronie.SelectedIndexChanged
        If bReagujNaComboIloscNaStronie Then
            txtNumerEkranu.Text = 1
            wczytaj()
        End If
    End Sub

    Private Sub btnOdswiez_Click(sender As System.Object, e As System.EventArgs) Handles btnOdswiez.Click
        txtNumerEkranu.Text = 1
        wczytaj()
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
            If kolumnaKliknieta.SortMode <> DataGridViewColumnSortMode.NotSortable Then
                kolumnaKliknieta.HeaderCell.SortGlyphDirection = SortOrder.Descending
            End If
            sortowanieRosnaco = False
        Else
            If kolumnaKliknieta.SortMode <> DataGridViewColumnSortMode.NotSortable Then
                kolumnaKliknieta.HeaderCell.SortGlyphDirection = SortOrder.Ascending
            End If
            sortowanieRosnaco = True
        End If

        'wypełniamy grid od nowa
        txtNumerEkranu.Text = "1"
        wczytaj()

        'szukamy, czy w wyniku jest przed chwilą kliknięta kolumna (po nazwie)
        For Each kolumna In dgv.Columns
            If kolumna.HeaderText = sortowanieKolumna Then
                'jest - rysujemy jej "sorting glyph"
                If sortowanieRosnaco Then
                    If kolumnaKliknieta.SortMode <> DataGridViewColumnSortMode.NotSortable Then
                        kolumna.HeaderCell.SortGlyphDirection = SortOrder.Ascending
                    End If
                Else
                    If kolumnaKliknieta.SortMode <> DataGridViewColumnSortMode.NotSortable Then
                        kolumna.HeaderCell.SortGlyphDirection = SortOrder.Descending
                    End If
                End If
                Exit For
            End If
        Next
    End Sub

    Private Sub btnFiltruj_Click(sender As System.Object, e As System.EventArgs) Handles btnFiltruj.Click
        txtNumerEkranu.Text = 1
        wczytaj()
    End Sub

    Private Sub btnWyczyscFiltry_Click(sender As System.Object, e As System.EventArgs) Handles btnWyczyscFiltry.Click
        UstawWartosciDomyslneFiltrow()
        txtNumerEkranu.Text = 1
        wczytaj()
    End Sub

    Private Sub txtNrAwiza_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtNrAwiza.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtNumerEkranu.Text = 1
            wczytaj()
        End If
    End Sub

    Private Sub txtNrPO_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtNrPO.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtNumerEkranu.Text = 1
            wczytaj()
        End If
    End Sub

    Private Sub txtQGUAR_DOSTAWA_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtQGUAR_DOSTAWA.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtNumerEkranu.Text = 1
            wczytaj()
        End If
    End Sub

    Private Sub txtQGUAR_ZA_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtQGUAR_ZA.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtNumerEkranu.Text = 1
            wczytaj()
        End If
    End Sub

    Private Sub txtDostawca_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtDostawca.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtNumerEkranu.Text = 1
            wczytaj()
        End If
    End Sub

    Private Sub btnPoczatek_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPoczatek.Click
        If txtNumerEkranu.Text = 1 Then
            MsgBox("To jest pierwszy ekran.", MsgBoxStyle.Exclamation, "Pierwszy ekran")
            Return
        End If
        txtNumerEkranu.Text = 1
        wczytaj()
    End Sub

    Private Sub btnPoprzedni_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPoprzedni.Click
        If txtNumerEkranu.Text = 1 Then
            MsgBox("To jest pierwszy ekran. Nie możesz przejść do poprzedniego ekranu.", MsgBoxStyle.Exclamation, "Pierwszy ekran")
            Return
        End If
        txtNumerEkranu.Text = txtNumerEkranu.Text - 1
        wczytaj()
    End Sub

    Private Sub btnNastepny_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNastepny.Click
        If txtNumerEkranu.Text >= iloscEkranow Then
            MsgBox("To jest ostatni ekran. Nie możesz przejść do następnego ekranu.", MsgBoxStyle.Exclamation, "Ostatni ekran")
            Return
        End If
        txtNumerEkranu.Text = txtNumerEkranu.Text + 1
        wczytaj()
    End Sub


    Private Sub btnOstatni_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOstatni.Click
        If txtNumerEkranu.Text = iloscEkranow Then
            MsgBox("To jest ostatni ekran.", MsgBoxStyle.Exclamation, "Ostatni ekran")
            Return
        End If
        txtNumerEkranu.Text = iloscEkranow
        wczytaj()
    End Sub

    Private Sub dgv_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles dgv.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Dim ht As DataGridView.HitTestInfo
            ht = Me.dgv.HitTest(e.X, e.Y)
            If ht.RowIndex > -1 Then
                intAwizoId = dgv.Rows(ht.RowIndex).Cells("AWIZO_ID").Value
                strQguarZA = dgv.Rows(ht.RowIndex).Cells("QGUAR_ZA").Value
                strQguarDostawa = dgv.Rows(ht.RowIndex).Cells("QGUAR_DOSTAWA").Value
                If dgv.Rows(ht.RowIndex).Cells("Szczegóły/Edycja").Value = "Edytuj" Then
                    menu_kontekstowe.Items("EdytujToolStripMenuItem").Visible = True
                Else
                    menu_kontekstowe.Items("EdytujToolStripMenuItem").Visible = False
                End If

                If dtFunkcje.Select("FUNKCJE_ID=33").Length > 0 And _
                                  (dgv.Rows(ht.RowIndex).Cells("Status").Value = "ZAPISANE" Or _
                                   dgv.Rows(ht.RowIndex).Cells("Status").Value = "POPRAWNE" Or _
                                   dgv.Rows(ht.RowIndex).Cells("Status").Value = "OCZEKUJĄCE") Then
                    menu_kontekstowe.Items("AnulujAwizoToolStripMenuItem").Visible = True
                Else
                    menu_kontekstowe.Items("AnulujAwizoToolStripMenuItem").Visible = False
                End If

                If ht.Type = DataGridViewHitTestType.Cell Then
                    dgv.ContextMenuStrip = menu_kontekstowe
                End If
            Else
                dgv.ContextMenuStrip = Nothing
            End If

        End If
    End Sub

    Private Function anuluj_awizo() As Boolean

        Dim odp As MsgBoxResult = MsgBox("Czy na pewno chcesz anulować awizo " & intAwizoId & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNoCancel, "Anulowanie awiza")
        If odp <> MsgBoxResult.Yes Then
            Return False
        End If

        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        ' ws.Url = frmGlowna.strWebservice
        Dim wsWynik As wsCursorProf.AwizoAnulujWynik

        'odczyt z serwera
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.AwizoAnuluj(frmGlowna.sesja, intAwizoId, strQguarZA, strQguarDostawa)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, "Raport historii materiału")
            Return False
        End Try
        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, "Anulowanie awiza")
            Return False
        Else
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Information, "Anulowanie awiza")
        End If

        Return True
    End Function

    Private Sub AnulujAwizoToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles AnulujAwizoToolStripMenuItem.Click
        If anuluj_awizo() Then
            txtNumerEkranu.Text = 1
            wczytaj()
        End If
    End Sub

    Private Sub PokazSzczegolyToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles PokazSzczegolyToolStripMenuItem.Click
        Dim frm2 As New frmAwizaSzczegoly
        frm2.awizo_id = intAwizoId
        frm2.ShowDialog()
    End Sub

    Private Sub EdytujToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles EdytujToolStripMenuItem.Click
        If frmGlowna.frmAwizaNew Is Nothing Then
            'Nie, okno nie jest otwarte - ładujemy je
            Dim frm As New frmAwizacje
            frmGlowna.frmAwizaNew = frm
            frm.strFunkcjaPowiadomieniaOGotowosci = "odswiezListy"
            frm.awizo_id = intAwizoId
            frm.MdiParent = frmGlowna
            frm.frmRodzic = frmGlowna
            frm.Show()
        Else
            'Tak, koszyk otwarty - pokazujemy go
            frmGlowna.frmAwizaNew.WindowState = FormWindowState.Normal
            frmGlowna.frmAwizaNew.Activate()
            frmGlowna.frmAwizaNew.Close()

            Dim frm1 As New frmAwizacje
            frmGlowna.frmAwizaNew = frm1
            frm1.strFunkcjaPowiadomieniaOGotowosci = "odswiezListy"
            frm1.awizo_id = intAwizoId
            frm1.MdiParent = frmGlowna
            frm1.frmRodzic = frmGlowna
            frm1.Show()
        End If
    End Sub

    Private Sub btnAnuluj_Click(sender As System.Object, e As System.EventArgs) Handles btnAnuluj.Click
        Me.Close()
    End Sub
End Class