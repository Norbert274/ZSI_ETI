Imports system.Threading

Public Class frmResetHasla
    Public strHaslo As String = ""
    Public user_id As Integer

    Private Sub btnResetujHaslo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnResetujHaslo.Click
        If txtKodBezpieczenstwa.Text.Length < 1 Then
            MsgBox("W celu ustawienia nowego has³a nalezy podaæ kod bezpieczeñstwa", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

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
        Dim wsWynik As New wsCursorProf.UserResetHaslaUstawNoweHasloWynik

        Try
            wsWynik = ws.UserResetHaslaUstawNoweHaslo(txtKodBezpieczenstwa.Text, strHaslo)

            If wsWynik.status = 0 Then
                MsgBox(wsWynik.status_opis, MsgBoxStyle.Information, Me.Text)
                Me.Close()
            Else
                MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, Me.Text)
            End If


        Catch ex As Exception
            wsWynik.status = -1
            wsWynik.status_opis = "B³¹d komunikacji z serwerem: " & ex.Message & frmGlowna.kontaktIt
        End Try

    End Sub

    Private Sub btnAnuluj_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAnuluj.Click
        Me.Close()
    End Sub

    Private Sub btnWyslijKodBezpieczenstwa_Click(sender As System.Object, e As System.EventArgs) Handles btnWyslijKodBezpieczenstwa.Click
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik As New wsCursorProf.UserResetHaslaGenerujTokenWynik

        Try
            wsWynik = ws.UserResetHaslaGenerujToken(txtLogin.Text)
            If wsWynik.status = 0 Then
                MsgBox(wsWynik.status_opis, MsgBoxStyle.Information, Me.Text)
            Else
                MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, Me.Text)
            End If

        Catch ex As Exception
            wsWynik.status = -1
            wsWynik.status_opis = "B³¹d komunikacji z serwerem: " & ex.Message & frmGlowna.kontaktIt
        End Try

    End Sub
End Class
