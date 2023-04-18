Imports DevExpress.XtraGrid

Public Class frmNotyfikacjeSystemowe
    Public frmRodzic As frmNotyfikacjeUzytkownikow
    Private dt As DataTable
    Private dtZmiany As DataTable

    Private Sub btnAnuluj_Click(sender As System.Object, e As System.EventArgs) Handles btnAnuluj.Click
        Me.Close()
    End Sub

    Private Function wczytaj() As Boolean

        gc.DataSource = Nothing
        dt = frmRodzic.dtSystemowe.Copy
        gc.DataSource = frmRodzic.dtSystemowe
        If Not IsNothing(gv.Columns("PARAMETR_ID")) Then
            gv.Columns("PARAMETR_ID").Visible = False
        Else
            MsgBox("Błąd wewnętrzny systemu. Przesłana lista pozycji zamówienia nie zawiera kolumny PARAMETR_ID." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
            Return False
        End If
        'gv.ColumnAutoWidth = False
        gv.BestFitColumns()

        For Each column As Columns.GridColumn In gv.Columns
            If column.FieldName = "ADRES" Then
                column.OptionsColumn.AllowEdit = True
                column.OptionsColumn.AllowFocus = True
            Else
                column.OptionsColumn.AllowEdit = False
                column.OptionsColumn.AllowFocus = False
            End If

        Next
        
        dtZmiany = Nothing
        Return True
    End Function

    Private Sub Zmiany()
        dtZmiany = New DataTable
        dtZmiany.Columns.Add("NOTYFIKACJA")
        dtZmiany.Columns.Add("ADRES")

        Dim new_row As DataRow
        For i As Integer = 0 To gv.DataRowCount - 1
            Dim rows As DataRow()
            rows = dt.Select("PARAMETR_ID = " & gv.GetRow(i).row("PARAMETR_ID").ToString & "")
            Dim col_name As String = "ADRES"
            If gv.GetRow(i).row(col_name) <> rows(0).Item(col_name) Then
                new_row = dtZmiany.NewRow()
                new_row.Item("NOTYFIKACJA") = gv.GetRow(i).row("NOTYFIKACJA")
                new_row.Item(col_name) = gv.GetRow(i).row(col_name)
                dtZmiany.Rows.Add(new_row)
            End If

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
            Dim wsWynik As wsCursorProf.NotyfikacjeSystemoweZapiszWynik

            Try
                Cursor = Cursors.WaitCursor
                Application.DoEvents()

                wsWynik = ws.NotyfikacjeSystemoweZapisz(frmGlowna.sesja, dtZmiany)
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
            MsgBox("Brak zmian w notyfikacjach systemowych do zapisania", MsgBoxStyle.Information, Me.Text)
            Return False
        End If
        Return True
        dtZmiany = Nothing

    End Function

    Private Sub btnZapisz_Click(sender As Object, e As System.EventArgs) Handles btnZapisz.Click
        'zapisuje zmiany do dtZmiany
        Zmiany()
        'zapisuje zmiany do bazy
        If Zapisz() = True Then
            wczytaj()
            MsgBox("Poprawnie zapisano notyfikacje systemowe", MsgBoxStyle.Information, Me.Text)
        End If
    End Sub

    Private Sub frmNotyfikacjeSystemowe_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Zmiany()
        If dtZmiany.Rows.Count > 0 Then
            Dim msgResult As MsgBoxResult
            msgResult = MsgBox("Czy chcesz zapisać zmiany notyfikacji systemowej?", MsgBoxStyle.YesNoCancel + MsgBoxStyle.Question, Me.Text)
            If msgResult = MsgBoxResult.Yes Then
                If Zapisz() = True Then
                    MsgBox("Poprawnie zapisano notyfikacje systemową", MsgBoxStyle.Information, Me.Text)
                End If
            End If
            If msgResult = MsgBoxResult.Cancel Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub frmNotyfikacjeSystemowe_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        wczytaj()
    End Sub
End Class