Imports DevExpress.XtraGrid
Imports System.Text
Imports DevExpress.Utils.Menu
Imports DevExpress.XtraGrid.Columns

Public Class frmNotyfikacjeUzytkownikow
    Inherits frmBase

    Private dtNotyfikacje As DataTable
    Public dtSystemowe As DataTable
    Private alBitColumns As New ArrayList
    Private dtZmiany As New DataTable
    Public intIdUser As Integer = -1

    Private Function wczytaj() As Boolean
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        'ws.Url = frmGlowna.strWebservice
        Dim wsWynik As New wsCursorProf.NotyfikacjeOdczytajWynik

        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.NotyfikacjeOdczytaj(frmGlowna.sesja, intIdUser)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
            Me.Close()
            'Exit Function
        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, Me.Text)
            Return False
        End If
        If wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, Me.Text)
        End If

        If wsWynik.dane.Tables.Count > 0 Then
            gc.DataSource = Nothing
            dtNotyfikacje = wsWynik.dane.Tables(0).Copy()
            gc.DataSource = wsWynik.dane.Tables(0)

            'gv.ColumnAutoWidth = False
            gv.BestFitColumns()
            gv.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
            gv.ColumnPanelRowHeight = 40
            If Not IsNothing(gv.Columns("USER_ID")) Then
                gv.Columns("USER_ID").Visible = False
            Else
                MsgBox("Błąd wewnętrzny systemu. Przesłana lista pozycji zamówienia nie zawiera kolumny USER_ID." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
                Return False
            End If
            For Each column As Columns.GridColumn In gv.Columns
                If column.ColumnType.Name = "Boolean" Then
                    alBitColumns.Add(column.GetCaption)
                    column.OptionsColumn.AllowEdit = True
                    column.OptionsColumn.AllowFocus = True
                Else
                    column.OptionsColumn.AllowEdit = False
                    column.OptionsColumn.AllowFocus = False
                    column.BestFit()
                End If

            Next

        Else
            MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał listy notyfikacji." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
            Return False
        End If

        If wsWynik.dane.Tables.Count > 1 Then
            dtSystemowe = wsWynik.dane.Tables(1).Copy()
        Else
            MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał listy dostępnych notyfikacji systemowych." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
            Return False
        End If
        dtZmiany = Nothing
        OdswiezUprawnienia()
        If intIdUser >= 0 Then
            btnNotyfikacjieSystemowe.Visible = False
        Else
            btnNotyfikacjieSystemowe.Visible = True
        End If
        Return True
    End Function

    Public Sub OdswiezUprawnienia()
        MyBase.Wlacz(frmGlowna.sesja)
    End Sub

    Private Sub frmNotyfikacjeUzytkownikow_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        Zmiany()
        If dtZmiany.Rows.Count > 0 Then
            Dim msgResult As MsgBoxResult
            msgResult = MsgBox("Czy chcesz zapisać zmiany notyfikacji użytkowników?", MsgBoxStyle.YesNoCancel + MsgBoxStyle.Question, Me.Text)
            If msgResult = MsgBoxResult.Yes Then
                If Zapisz() = True Then
                    MsgBox("Poprawnie zapisano notyfikację", MsgBoxStyle.Information, Me.Text)
                End If
            End If
            If msgResult = MsgBoxResult.Cancel Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub frmNotyfikacjeUzytkownikow_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        If Not wczytaj() Then
            Me.Close()
            Exit Sub
        End If
    End Sub
    Private Sub Zmiany()
        dtZmiany = New DataTable
        '<row user_id ="1" notyfikacja = "POTWIERDZENIE ZŁOŻENIA ZAMÓWIENIA" wlacz = "1"/>
        dtZmiany.Columns.Add("user_id")
        dtZmiany.Columns.Add("notyfikacja")
        dtZmiany.Columns.Add("wlacz")

        Dim new_row As DataRow
        For i As Integer = 0 To gv.DataRowCount - 1
            Dim rows As DataRow()
            rows = dtNotyfikacje.Select("user_id = " & gv.GetRow(i).row("user_id").ToString)
            For Each col_name As String In alBitColumns
                If gv.GetRow(i).row(col_name) <> rows(0).Item(col_name) Then
                    new_row = dtZmiany.NewRow()
                    new_row.Item("user_id") = gv.GetRow(i).row("user_id")
                    new_row.Item("notyfikacja") = col_name
                    new_row.Item("wlacz") = gv.GetRow(i).row(col_name)
                    dtZmiany.Rows.Add(new_row)
                End If
            Next
        Next i
        dtZmiany.TableName = "Zmiany"

    End Sub

    Private Function Zapisz() As Boolean
        If dtZmiany.Rows.Count > 0 Then
            System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
            System.Net.ServicePointManager.Expect100Continue = False
            ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
            ws.Proxy.Credentials = CredentialCache.DefaultCredentials
            'ws.Url = frmGlowna.strWebservice
            Dim wsWynik As wsCursorProf.NotyfikacjeZapiszWynik

            Try
                Cursor = Cursors.WaitCursor
                Application.DoEvents()

                wsWynik = ws.NotyfikacjeZapisz(frmGlowna.sesja, dtZmiany)
                Cursor = Cursors.Default
            Catch ex As Exception
                Cursor = Cursors.Default
                MsgBox("Błąd komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
                Me.Close()
                Return False
            End Try

            If wsWynik.status < 0 Then
                MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, Me.Text)
                Return False
            End If
            If wsWynik.status > 0 Then
                MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, Me.Text)
            End If
        Else
            MsgBox("Brak zmian w notyfikacjach do zapisania", MsgBoxStyle.Critical, Me.Text)
            Return False
        End If
        Return True


    End Function

    Private Sub btnZapisz_Click(sender As Object, e As System.EventArgs) Handles btnZapisz.Click
        'zapisuje zmiany do dtZmiany
        Zmiany()
        'zapisuje zmiany do bazy
        If Zapisz() = True Then
            wczytaj()
            MsgBox("Poprawnie zapisano notyfikację", MsgBoxStyle.Information, Me.Text)
        End If
    End Sub

    Private Sub btnAnuluj_Click(sender As Object, e As System.EventArgs) Handles btnAnuluj.Click
        Me.Close()
    End Sub

    Private Sub btnNotyfikacjieSystemowe_Click(sender As Object, e As System.EventArgs) Handles btnNotyfikacjieSystemowe.Click
        Dim frm As New frmNotyfikacjeSystemowe
        frm.frmRodzic = Me
        frm.ShowDialog()
    End Sub

    Private Sub gv_PopupMenuShowing(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles gv.PopupMenuShowing
        If e.MenuType = Views.Grid.GridMenuType.Column Then
            Dim menu As Menu.GridViewColumnMenu
            menu = e.Menu
            If alBitColumns.Contains(menu.Column.FieldName) Then
                menu.Items.Clear()
                'Dim item As DXMenuItem
                menu.Items.Add(CreateZaznaczMenuItem(menu.Column))
                menu.Items.Add(CreateOdznaczMenuItem(menu.Column))
            End If
        End If
    End Sub

    Private Function CreateZaznaczMenuItem(column As GridColumn) As DXMenuItem
        Dim item As New DXMenuItem("Zaznacz wszystkie", AddressOf ZaznaczWszystkie)
        item.Tag = column
        Return item
    End Function

    Private Function CreateOdznaczMenuItem(column As GridColumn) As DXMenuItem

        Dim item As New DXMenuItem("Odznacz wszystkie", AddressOf OdznaczWszystkie)
        item.Tag = column
        Return item
    End Function

    Private Sub ZaznaczWszystkie(sender As System.Object, e As EventArgs)
        For i As Integer = 0 To gv.DataRowCount - 1
            gv.SetRowCellValue(i, CType(CType(sender, DXMenuItem).Tag, GridColumn), True)
        Next i
    End Sub

    Private Sub OdznaczWszystkie(sender As System.Object, e As EventArgs)
        For i As Integer = 0 To gv.DataRowCount - 1
            gv.SetRowCellValue(i, CType(CType(sender, DXMenuItem).Tag, GridColumn), False)
        Next i
    End Sub
End Class