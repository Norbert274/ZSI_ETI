Imports system.Threading

Public Class frmLogowanie
    Public czy_pierwszy As Integer
    Private th As Thread

    Private Class frmLogowanieThread
        Dim Parametry As New ZmienneGlobalne
        Public frmRodzic As frmLogowanie
        Public login As String
        Public haslo As String
        Public wersja As String
        Public produkcja As Boolean 'czy serwer produkcyjny
        Public wsWynik As wsCursorProf.ZalogujWynik
        Private produkcjaUrl As String = Parametry.WsProdukcja
        Private testUrl As String = Parametry.Wstestowka

        Private Sub frmLogowanieThread_finished()
            With frmRodzic
                .Cursor = Cursors.Default
                .txtLogin.Enabled = True
                .txtHaslo.Enabled = True
                .btnZaloguj.Enabled = True
                .btnAnuluj.Enabled = False
                .rbProdukcyjny.Enabled = True
                .rbTestowy.Enabled = True
            End With
            If wsWynik.status < 0 Then
                MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical)
                Exit Sub
            ElseIf wsWynik.status > 0 Then
                MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, "B³¹d logowania.")
            End If
            frmGlowna.czyPierwszy = wsWynik.czy_pierwszy
            frmGlowna.strKomunikatDlaUzytkownika = wsWynik.komunikat_dla_uzytkownika
            frmGlowna.sesja = wsWynik.sesja
            frmGlowna.intIdUzytkownikZalogowany = wsWynik.uzytkownik_id
            frmGlowna.strUzytkownikZalogowany = wsWynik.uzytkownik
            frmGlowna.strTelefon = wsWynik.telefon
            frmGlowna.strWebservice = IIf(produkcja, produkcjaUrl, testUrl)
            frmGlowna.bProdukcja = produkcja

            frmGlowna.Text = "Fulfilio Sp. z o.o. - system rezerwacji i dystrybucji towaru (zalogowany jako " & wsWynik.uzytkownik & ") [" & IIf(produkcja, wsWynik.database, wsWynik.database) & "]"
            frmGlowna.showOrRefresh()
            frmRodzic.Close()
            'Dim frm As New frmZmienHaslo
            'If frmGlowna.czyPierwszy Then
            '    'if
            '    frm.pierwszy = True
            '    'frm.MdiParent = Me
            '    frm.ShowDialog(frmGlowna)
            'Else
            '    frm.pierwszy = False
            'End If
        End Sub

        Public Sub frmLogowanieThread_doWork()
            System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
            System.Net.ServicePointManager.Expect100Continue = False
            ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
            ws.Proxy.Credentials = CredentialCache.DefaultCredentials
            wsWynik = New wsCursorProf.ZalogujWynik

            'If produkcja Then
            '    ws.Url = produkcjaUrl
            'Else
            '    ws.Url = testUrl
            'End If

            Try
                wsWynik = ws.Zaloguj_szablon(login, haslo, 1, My.Computer.Name)

                'czy_pierwszy=wsWynik.
            Catch ex As Exception
                wsWynik.status = -1
                wsWynik.status_opis = "B³¹d komunikacji z serwerem: " & ex.Message & frmGlowna.kontaktIt
            End Try

            Dim method As New MethodInvoker(AddressOf frmLogowanieThread_finished)
            frmRodzic.Invoke(method)
        End Sub

    End Class

    Private Sub frmLogowanie_Resize(sender As Object, e As System.EventArgs) Handles Me.Resize
        'txtLogin.Text = "p.wojslaw"
        'txtHaslo.Text = "system321"
    End Sub

    Private Sub frmLogowanie_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        '  btnBezLogowania.Visible = False
        txtLogin.Focus()
        txtLogin.SelectAll()
        btnAnuluj.BackColor = Color.LightGray
        If My.Application.IsNetworkDeployed Then
            lblWersja.Text = "Wersja aplikacji: " & My.Application.Deployment.CurrentVersion.Revision.ToString()
        Else
            lblWersja.Text = "Wersja aplikacji: developerska"
        End If
    End Sub

    Private Sub btnAnuluj_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAnuluj.Click
        If Not th Is Nothing Then th.Abort()
        th = Nothing
        Cursor = Cursors.Default
        txtLogin.Enabled = True
        txtHaslo.Enabled = True
        btnZaloguj.Enabled = True
        rbProdukcyjny.Enabled = True
        rbTestowy.Enabled = True
        btnAnuluj.Enabled = False
    End Sub

    Private Sub btnZaloguj_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnZaloguj.Click
        zaloguj()
    End Sub

    Private Sub frmLogowanie_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then zaloguj()
    End Sub

    Private Sub zaloguj()
        txtLogin.Enabled = False
        txtHaslo.Enabled = False
        btnZaloguj.Enabled = False
        rbProdukcyjny.Enabled = False
        rbTestowy.Enabled = False
        btnAnuluj.Enabled = True
        btnAnuluj.BackColor = btnZaloguj.BackColor
        btnZaloguj.BackColor = Color.LightGray

        Cursor = Cursors.AppStarting
        Dim par As New frmLogowanieThread
        par.frmRodzic = Me
        par.login = txtLogin.Text
        par.haslo = txtHaslo.Text
        If My.Application.IsNetworkDeployed Then
            par.wersja = My.Application.Deployment.CurrentVersion.Revision.ToString()
        Else
            par.wersja = "vsDevMagicString" 'uniwersalna wersja pasuj¹ca zawsze
        End If
        If rbProdukcyjny.Checked Then
            par.produkcja = True
        Else
            par.produkcja = False
        End If
        th = New Thread(AddressOf par.frmLogowanieThread_doWork)
        th.IsBackground = True
        th.Start()

    End Sub

    Private Sub btnBezLogowania_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        frmGlowna.Show()
        Me.Close()
    End Sub

    Private Sub lblZapomnialemHasla_Click(sender As System.Object, e As System.EventArgs) Handles lblZapomnialemHasla.Click
        Dim frm As New frmResetHasla
        frm.ShowDialog()
    End Sub
End Class
