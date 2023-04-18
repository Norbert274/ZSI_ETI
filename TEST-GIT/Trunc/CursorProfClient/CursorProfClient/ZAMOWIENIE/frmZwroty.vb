Public Class frmZwroty
    Public dtZwroty As DataTable
    Public bMultiSelect As Boolean = True
    Public zaznacz As Boolean = False
    Private bReagujNaCheckBoxHeader As Boolean = True

    Private Function wczytaj() As Boolean
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        'ws.Url = frmGlowna.strWebservice
        Dim wsWynik As wsCursorProf.ZwrotyWczytajWynik

        'odczyt z serwera
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.ZwrotyWczytaj(frmGlowna.sesja)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical)
            Return False
        End Try
        If wsWynik.status = -1 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, "Brak uprawnień")
            Return False
        End If
        dtZwroty = wsWynik.dane.Tables(0)
        dgvZwroty.DataSource = dtZwroty
        For Each col As DataGridViewColumn In dgvZwroty.Columns
            If col.HeaderText = "SKU_ID" Then
                col.Visible = False
            End If
            If col.HeaderText <> "ilosc_do_zwrotu" Then
                col.ReadOnly = True
            End If
        Next
        'dgvZwroty.Columns(0).Visible = False
        For Each col As DataGridViewColumn In dgvZwroty.Columns
            dgvZwroty.Columns(col.Index).AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
            dgvZwroty.Columns(col.Index).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        Next

        'dodajemy kolumnę z checkboxami
        dgvZwroty.Columns.Insert(0, New DataGridViewCheckBoxColumn)
        dgvZwroty.Columns(0).Width = 22
        Dim dgvCell As DataGridViewCheckBoxCell
        For Each row As DataGridViewRow In dgvZwroty.Rows
            dgvCell = DirectCast(row.Cells(0), DataGridViewCheckBoxCell)
            dgvCell.Value = zaznacz And bMultiSelect
        Next
        If bMultiSelect = True Then

            Dim rect As Rectangle = dgvZwroty.GetCellDisplayRectangle(0, -1, True)
            rect.X = rect.Location.X + 4
            rect.Y = rect.Location.Y + 4
            Dim checkboxHeader As New CheckBox()
            checkboxHeader.Name = "checkboxHeader"
            checkboxHeader.Size = New Size(15, 14)
            checkboxHeader.Location = rect.Location
            checkboxHeader.Checked = zaznacz And bMultiSelect
            dgvZwroty.Controls.Add(checkboxHeader)
            AddHandler checkboxHeader.CheckedChanged, AddressOf dgv_checkboxHeaderCheckedChanged

        End If
        dgvZwroty.AllowUserToAddRows = False
        dgvZwroty.AllowUserToDeleteRows = False
        If wsWynik.status = -1 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation)
        End If

        Return True
    End Function

    Private Sub dgv_checkboxHeaderCheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        'dgv(0, i).Value = DirectCast(dgv.Controls.Find("checkboxHeader", True)(0), CheckBox).Checked
        'dgv.EndEdit()
        If bReagujNaCheckBoxHeader Then
            If dgvZwroty.Rows.Count > 0 Then
                'czy wszystkie checkboxy zaznaczone ?
                Dim bWszystkie As Boolean = True
                For Each dgvWiersz As DataGridViewRow In dgvZwroty.Rows
                    If dgvWiersz.Cells("sku_id").Value < 0 Then Continue For
                    If dgvWiersz.Cells(0).Value Is Nothing OrElse dgvWiersz.Cells(0).Value = False Then
                        bWszystkie = False
                        Exit For
                    End If
                Next
                'zaznaczamy lub odznaczamy
                For Each dgvWiersz As DataGridViewRow In dgvZwroty.Rows
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

    Private Sub btnZamknij_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnZamknij.Click
        Me.Close()
    End Sub

    Private Sub frmZwroty_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If Not wczytaj() Then
            Me.Close()
        End If
    End Sub

    Private Sub btnZapisz_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnZapisz.Click
        If Not zapisz_zwroty() Then
            MsgBox("Nie zapisano zwrotów.", MsgBoxStyle.Exclamation)
        End If
    End Sub
    Private Function zapisz_zwroty() As Boolean

        'zbieramy co zaznaczono
        Dim dtZwrotZaznaczone As New DataTable
        dtZwrotZaznaczone.Columns.Add("sku_id", GetType(Integer))
        dtZwrotZaznaczone.Columns.Add("nr_zamowienia", GetType(Integer))
        dtZwrotZaznaczone.Columns.Add("ilosc_zwrot", GetType(Integer))

        For Each dgvRow As DataGridViewRow In dgvZwroty.Rows
            If dgvRow.Cells(0).Value = True Then dtZwrotZaznaczone.Rows.Add(dgvRow.Cells("sku_id").Value, dgvRow.Cells("nr_zamowienia").Value, dgvRow.Cells("ilosc_do_zwrotu").Value)
        Next
        'If dtZwrotZaznaczone.Rows.Count < 1 Then
        '    MsgBox("Zaznacz przynajmniej jedną pozycję", MsgBoxStyle.Exclamation)
        '    Exit Function
        'End If

        ''zapisujemy zwroty do bazy
        'Dim ds As New DataSet
        'ds.Tables.Add(dtZwrotZaznaczone)
        'Dim ws As NewSystem.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        'System.Net.ServicePointManager.Expect100Continue = False
        'ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        'ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        'Dim wsWynik As wsCursorProf.ZwrotyZapiszWynik
        'Try
        '    Cursor = Cursors.WaitCursor
        '    Application.DoEvents()
        '    ' wsWynik = ws.ZwrotyZapisz(frmGlowna.sesja, ds)
        '    Cursor = Cursors.Default
        'Catch ex As Exception
        '    Cursor = Cursors.Default
        '    MsgBox("Błąd komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical)
        '    Exit Function
        'End Try
        'Cursor = Cursors.Default
        'If wsWynik.status < 0 Then
        '    MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical)
        '    Exit Function
        'ElseIf wsWynik.status > 0 Then
        '    MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation)
        'End If
        'If wsWynik.dane.Tables.Count < 1 Then
        '    MsgBox("Błąd wewnętrzny systemu: serwer nie zapisał zwrotów dla wybranych produktów." & frmGlowna.kontaktIt, MsgBoxStyle.Critical)
        '    Exit Function
        'End If
        Return True
    End Function

    Private Sub dgvZwroty_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles dgvZwroty.CellValidating
        Dim i As Integer = 0
        If dgvZwroty.Columns(e.ColumnIndex).HeaderText = "ilosc_do_zwrotu" Then
            If Not Integer.TryParse(e.FormattedValue, i) Then
                MsgBox("Błąd wartość musi byc liczbą.", MsgBoxStyle.Critical)
                e.Cancel = True
                Exit Sub
            End If
            If e.FormattedValue > 0 Then
                dgvZwroty.Rows(e.RowIndex).Cells(0).Value = True
            ElseIf e.FormattedValue = 0 Then
                dgvZwroty.Rows(e.RowIndex).Cells(0).Value = False
            End If
        End If
    End Sub
End Class