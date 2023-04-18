Public Class frmNewsleterLista
    Private dtNewsleter As DataTable
    Public strTytul As String
    Public strTresc As String

    Private Sub frmNewsleterLista_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If Not wczytaj() Then
            Me.Close()
        End If
    End Sub

    Private Function wczytaj() As Boolean
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        'ws.Url = frmGlowna.strWebservice
        Dim wsWynik As wsCursorProf.NewsleterListaWynik

        'odczyt z serwera
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.NewsleterLista(frmGlowna.sesja)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
            Return False
        End Try
        If wsWynik.status = -1 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, Me.Text)
            Return False
        End If
        dtNewsleter = wsWynik.dane.Tables(0)
        dgvNewsleter.DataSource = dtNewsleter
        dgvNewsleter.Columns(0).Visible = False
        For Each col As DataGridViewColumn In dgvNewsleter.Columns
            dgvNewsleter.Columns(col.Index).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        Next
        dgvNewsleter.AllowUserToAddRows = False
        dgvNewsleter.AllowUserToDeleteRows = False
        If wsWynik.status = -1 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, Me.Text)
        End If
        Return True
    End Function

    Private Sub dgvNewsleter_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvNewsleter.CellDoubleClick
        NewsleterEdit(dgvNewsleter.Rows(e.RowIndex).Cells("newsleter_id").Value)
    End Sub

    Public Sub NewsleterEdit(ByVal newsleter_id As Integer)
        Dim frm As frmNewsletter = New frmNewsletter
        frm.frmRodzic = Me
        frm.WindowState = FormWindowState.Normal
        frm.intIdNewsleter = newsleter_id
        'If Me.Modal Then
        '    frm.ShowDialog()
        'Else
        frm.MdiParent = frmGlowna
        frm.Show()
        'End If
    End Sub

    Private Sub btnZamknij_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnZamknij.Click
        Me.Close()
    End Sub

    Private Sub btnNowyNewsletter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNowyNewsletter.Click
        Dim frm As New frmNewsletter
        frm.intIdNewsleter = -1
        frm.MdiParent = frmGlowna
        frm.frmRodzic = Me
        frm.Show()
    End Sub

    Private Sub btnOdswierz_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOdswierz.Click
        wczytaj()
    End Sub

    Public Sub OdswierzListy()
        wczytaj()
    End Sub
End Class