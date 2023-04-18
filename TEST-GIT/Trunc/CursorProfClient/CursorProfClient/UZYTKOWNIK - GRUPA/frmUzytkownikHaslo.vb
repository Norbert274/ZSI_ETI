Public Class frmUzytkownikHaslo
    Public strHaslo As String = ""
    Public user_id As Integer

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        If txtHaslo.Text.Length < 1 Then
            MsgBox("Has³o musi mieæ d³ugoœæ przynajmniej 1 znaku. Je¿eli chcesz usun¹æ has³o dla u¿ytkownika, zamknij to okno i skorzystaj z przycisku ""Usuñ has³o"" na ekranie u¿ytkownika.", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If txtHaslo.Text <> txtHaslo2.Text Then
            MsgBox("Has³a podane w poszczególnych polach s¹ ró¿ne.", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        strHaslo = txtHaslo.Text

       System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
System.Net.ServicePointManager.Expect100Continue = False
ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik As New wsCursorProf.ZmienHasloUzytkownikaWynik

        'ws.Url = frmGlowna.strWebservice
        'ws.Url = frmGlowna.strWebservice

        Try
            wsWynik = ws.ZmienHasloUzytkownika(frmGlowna.sesja, user_id, strHaslo)
        Catch ex As Exception
            wsWynik.status = -1
            wsWynik.status_opis = "B³¹d komunikacji z serwerem: " & ex.Message & frmGlowna.kontaktIt
        End Try
        Me.Close()
    End Sub

    Private Sub btnAnuluj_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAnuluj.Click
        Me.Close()
    End Sub

    Private Sub frmUzytkownikHaslo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown, txtHaslo.KeyDown, txtHaslo2.KeyDown

        If e.KeyCode = Keys.Escape Then
            btnAnuluj.PerformClick()
        ElseIf e.KeyCode = Keys.Enter Then
            btnOk.PerformClick()
        End If

    End Sub
End Class