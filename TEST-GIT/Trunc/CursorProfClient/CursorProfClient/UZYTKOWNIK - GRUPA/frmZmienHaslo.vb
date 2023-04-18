Imports system.Threading

Public Class frmZmienHaslo
    Private th As Thread
    Public pierwszy As Boolean

    Private Class frmZmienHasloThread
        Public frmRodzic As frmZmienHaslo
        Public obecne_haslo As String
        Public nowe_haslo As String
        Public wsWynik As wsCursorProf.ZmienHasloWynik
        Public pierwszy As Boolean

        Private Sub frmZmienHasloThread_finished()
            With frmRodzic
                .Cursor = Cursors.Default
                .txtObecneHaslo.Enabled = True
                .txtNoweHaslo.Enabled = True
                .txtPotwierdzHaslo.Enabled = True
                .btnOK.Enabled = True
            End With

            If wsWynik.status < 0 Then
                MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, frmRodzic.Text)
                If pierwszy Then
                    frmGlowna.Close()
                End If
                Exit Sub
            ElseIf wsWynik.status > 0 Then
                MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, frmRodzic.Text)
            Else
                MsgBox("Has³o zosta³o zmienione.", MsgBoxStyle.Information, frmRodzic.Text)

                frmZmienHaslo.Close()
            End If
            frmRodzic.Close()
        End Sub

        Public Sub frmZmienHasloThread_doWork()
           System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
System.Net.ServicePointManager.Expect100Continue = False
ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
ws.Proxy.Credentials = CredentialCache.DefaultCredentials
            'ws.Url = frmGlowna.strWebservice
            wsWynik = New wsCursorProf.ZmienHasloWynik
            ' ws.Url = frmGlowna.strWebservice

            Try
                wsWynik = ws.ZmienHaslo(frmGlowna.sesja, obecne_haslo, nowe_haslo)
            Catch ex As Exception
                wsWynik.status = -1
                wsWynik.status_opis = "B³¹d komunikacji z serwerem: " & ex.Message & frmGlowna.kontaktIt
            End Try

            Dim method As New MethodInvoker(AddressOf frmZmienHasloThread_finished)

            frmRodzic.Invoke(method)
        End Sub

    End Class

    Private Sub frmZmienHaslo_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If pierwszy Then
            If txtNoweHaslo.Text = "" Or txtObecneHaslo.Text = "" Or txtPotwierdzHaslo.Text = "" Then
                Dim answer As MsgBoxResult = MsgBox("Czy chcesz wyjœæ z aplikacji? Logujesz siê pierwszy raz i musisz teraz zmieniæ has³o, aby móc z niej korzystaæ.", MsgBoxStyle.YesNo, Me.Text)
                If answer = MsgBoxResult.Yes Then
                    Me.Dispose()
                    frmGlowna.Close()
                Else
                    e.Cancel = True
                End If
            ElseIf txtObecneHaslo.Text = txtNoweHaslo.Text Then
                MsgBox("Obecne has³o i nowe nie mog¹ byæ takie same!", MsgBoxStyle.Exclamation, Me.Text)
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub frmZmienHaslo_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        
        txtObecneHaslo.Focus()
        txtObecneHaslo.SelectAll()
    End Sub

    Private Sub btnAnuluj_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAnuluj.Click
        If btnOK.Enabled Then
            'zamykamy ekran
            Me.Close()
            If pierwszy Then
                frmGlowna.Close()
            End If
        Else
            'przerywamy wywo³anie, ale zostawiamy ekran otwarty
            If Not th Is Nothing Then th.Abort()
            th = Nothing
            Cursor = Cursors.Default
            txtObecneHaslo.Enabled = True
            txtNoweHaslo.Enabled = True
            txtPotwierdzHaslo.Enabled = True
            btnOK.Enabled = True
        End If
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        zmienHaslo()
    End Sub

    Private Sub frmLogowanie_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then zmienHaslo()
    End Sub

 

    Private Sub zmienHaslo()
        txtObecneHaslo.Enabled = False
        txtNoweHaslo.Enabled = False
        txtPotwierdzHaslo.Enabled = False
        btnOK.Enabled = False

        If txtNoweHaslo.Text <> txtPotwierdzHaslo.Text Then
            MsgBox("Has³a podane w polach ""Nowe has³o"" i ""PotwierdŸ nowe has³o"" ró¿ni¹ siê.", MsgBoxStyle.Exclamation, Me.Text)
            txtObecneHaslo.Enabled = True
            txtNoweHaslo.Enabled = True
            txtPotwierdzHaslo.Enabled = True
            btnOK.Enabled = True
            Exit Sub
        End If

        If txtObecneHaslo.Text = txtNoweHaslo.Text Then
            MsgBox("Takie has³o jest obecnie ustawione. Nowe has³o powinno byæ ró¿ne od obecnego.", MsgBoxStyle.Exclamation, Me.Text)
            txtObecneHaslo.Enabled = True
            txtNoweHaslo.Enabled = True
            txtPotwierdzHaslo.Enabled = True
            btnOK.Enabled = True
            Exit Sub
        End If

        Cursor = Cursors.AppStarting
        Dim par As New frmZmienHasloThread
        par.pierwszy = Me.pierwszy
        par.frmRodzic = Me
        par.obecne_haslo = txtObecneHaslo.Text
        par.nowe_haslo = txtNoweHaslo.Text
        pierwszy = False
        th = New Thread(AddressOf par.frmZmienHasloThread_doWork)
        th.IsBackground = True
        th.Start()
    End Sub

    
End Class
