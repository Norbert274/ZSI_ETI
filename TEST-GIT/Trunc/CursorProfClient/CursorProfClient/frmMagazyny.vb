Public Class frmMagazyny
    Public intIdWybranegoMagazynu As Integer = -1 'z tej zmiennej okno wywo³uj¹ce mo¿e odczytaæ wybrane TEAM_ID
    Public strNazwaWybranegoMagazynu As String 'z tej zmiennej okno wywo³uj¹ce mo¿e odczytaæ nazwê wybranego TEAMu

    Private Sub frmMagazyny_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If Not wczytaj() Then Me.Close()
    End Sub

    Private Sub btnWybierz_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWybierz.Click
        If wybierz(dgv.CurrentCell) Then Me.Close()
    End Sub

    Private Function wybierz(ByVal dgvCell) As Boolean
        If dgv.CurrentCell Is Nothing Then
            MsgBox("Najpierw wybierz magazyn.", MsgBoxStyle.Exclamation)
            Return False
        End If
        intIdWybranegoMagazynu = dgv.Rows(dgv.CurrentCell.RowIndex).Cells("magazyn_id").Value
        strNazwaWybranegoMagazynu = dgv.Rows(dgv.CurrentCell.RowIndex).Cells("nazwa").Value
        Return True
    End Function

    Private Function wczytaj() As Boolean
       System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
System.Net.ServicePointManager.Expect100Continue = False
ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        'ws.Url = frmGlowna.strWebservice
        Dim wsWynik As wsCursorProf.MagazynyOdczytajWynik

        Try
            Cursor = Cursors.WaitCursor
            wsWynik = ws.MagazynyOdczytaj(frmGlowna.sesja)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("B³¹d komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegó³y b³êdu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
            Return False
        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical)
            Return False
        ElseIf wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation)
        End If
        If wsWynik.dane.Tables.Count > 0 Then
            dgv.DataSource = wsWynik.dane.Tables(0)
            If dgv.Columns.Contains("magazyn_id") Then dgv.Columns("magazyn_id").Visible = False
            dgv.CurrentCell = Nothing
        Else
            MsgBox("B³¹d wewnêtrzny systemu. Serwer nie przes³a³ listy dostêpnych magazynów." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
            Me.Close()
        End If
        Return True

    End Function

    Private Sub btnOdswiez_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOdswiez.Click
        wczytaj()
    End Sub

    Private Sub dgv_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellDoubleClick
        If wybierz(dgv.Rows(e.RowIndex).Cells(e.ColumnIndex)) Then Me.Close()
    End Sub

    Public Sub odswiezListy()
        wczytaj()
    End Sub

    Private Sub btnAnuluj_Click(sender As System.Object, e As System.EventArgs) Handles btnAnuluj.Click
        Me.Close()
    End Sub
End Class