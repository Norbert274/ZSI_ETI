Public Class frmKontakt

    Private Sub btnWyslijWiadomosc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWyslijWiadomosc.Click
        If Not wyslij() Then
            Exit Sub
        End If
    End Sub

    Private Function wyslij() As Boolean
       System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
System.Net.ServicePointManager.Expect100Continue = False
ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        'ws.Url = frmGlowna.strWebservice
        Dim wsWynik As wsCursorProf.KontaktMailWyslijWynik

        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If txtTemat.Text = String.Empty Then
                MsgBox("Nie podano tematu wiadomości. To pole jest wymagane!", MsgBoxStyle.Exclamation, "Temat wiadomości")
                txtTemat.Focus()
                Return False
            ElseIf txtTresc.Text = String.Empty Then
                MsgBox("Nie podano treści wiadomości. To pole jest wymagane!", MsgBoxStyle.Exclamation, "Treść wiadomości")
                txtTresc.Focus()
                Return False
            Else
                wsWynik = ws.KontaktMailWyslij(frmGlowna.sesja, txtTemat.Text, txtTresc.Text, "", 1) 'Brak zalacnzika, zrodlo = 1 czyli stary format kontaktu
            End If
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, "Wysyłanie wiadomości")
            Return False
        End Try

        If wsWynik.status = -1 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, "Wysyłanie wiadomości")
            Return False
        End If
        If wsWynik.status = 0 Then
            Me.Close()
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Information, "Wysyłanie wiadomości")
            Return True

        End If
        Return True
    End Function

    Private Sub btnAnuluj_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAnuluj.Click
        Me.Close()
    End Sub

    Private Sub Label4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub frmKontakt_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        txtTemat.Focus()
    End Sub
End Class