
Imports System.Threading
Imports System.Text.RegularExpressions

Public Class frmZmianaHasla_Tel_Email
    Private th As Thread
    Public pierwszy As Boolean
    Private dtDane As DataTable
    

    Private Class frmZmienHasloThread
        Public frmRodzic As frmZmianaHasla_Tel_Email
        Public obecne_haslo As String
        Public nowe_haslo As String
        Public Aktualny_Email As String
        Public Aktualny_Telefon As Integer
        Public wsWynik As wsCursorProf.ZmienHasloTelEmailWynik
        Public pierwszy As Boolean

        Private Sub frmZmianaHasla_Tel_EmailThread_finished()
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
                MsgBox("Hasło zostało zmienione.", MsgBoxStyle.Information)

                frmZmianaHasla_Tel_Email.Close()
            End If
            frmRodzic.Close()
        End Sub

        Public Sub frmZmianaHasla_Tel_EmailThread_doWork()
           System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
System.Net.ServicePointManager.Expect100Continue = False
ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
ws.Proxy.Credentials = CredentialCache.DefaultCredentials
            'ws.Url = frmGlowna.strWebservice
            wsWynik = New wsCursorProf.ZmienHasloTelEmailWynik
            ' ws.Url = frmGlowna.strWebservice

            Try
                wsWynik = ws.ZmienHasloTelEmail(frmGlowna.sesja, obecne_haslo, nowe_haslo, Aktualny_Telefon, Aktualny_Email)
            Catch ex As Exception
                wsWynik.status = -1
                wsWynik.status_opis = "Błąd komunikacji z serwerem: " & ex.Message & frmGlowna.kontaktIt
            End Try

            Dim method As New MethodInvoker(AddressOf frmZmianaHasla_Tel_EmailThread_finished)

            frmRodzic.Invoke(method)
        End Sub

    End Class

    Private Sub frmZmianaHasla_Tel_Email_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If pierwszy Then
            If txtNoweHaslo.Text = "" Or txtObecneHaslo.Text = "" Or txtPotwierdzHaslo.Text = "" Then
                Dim answer As MsgBoxResult = MsgBox("Czy chcesz wyjść z aplikacji? Logujesz się pierwszy raz i musisz teraz zmienić hasło, aby móc z niej korzystać.", MsgBoxStyle.YesNo, "Pierwsze logowanie do aplikacji")
                If answer = MsgBoxResult.Yes Then
                    Me.Dispose()
                    frmGlowna.Close()
                Else
                    e.Cancel = True
                End If
                'ElseIf txtObecneHaslo.Text = txtNoweHaslo.Text Then
                '    MsgBox("Obecne hasło i nowe nie mogą być takie same!", MsgBoxStyle.Exclamation)
                '    e.Cancel = True
            End If
        End If
    End Sub

    Private Sub frmZmianaHasla_Tel_Email_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If Not wczytaj() Then
            Me.Close()
        End If
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
            'przerywamy wywołanie, ale zostawiamy ekran otwarty
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

    Private Function wczytaj() As Boolean

       System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
System.Net.ServicePointManager.Expect100Continue = False
ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik As wsCursorProf.PierwszeLogowanieWczytajWynik

        'odczyt z serwera
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.PierwszeLogowanieWczytaj(frmGlowna.sesja)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
            Return False
        End Try

        txtEmail.Text = wsWynik.email
        If wsWynik.telefon = 0 Then
            txtTelefon.Text = ""
        Else
            txtTelefon.Text = wsWynik.telefon
        End If

        If wsWynik.status = -1 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, Me.Text)
        End If
        Return True
    End Function


    Private Sub zmienHaslo()
        txtObecneHaslo.Enabled = False
        txtNoweHaslo.Enabled = False
        txtPotwierdzHaslo.Enabled = False
        'btnOK.Enabled = False

        If txtNoweHaslo.Text <> txtPotwierdzHaslo.Text Then
            MsgBox("Hasła podane w polach ""Nowe hasło"" i ""Potwierdź nowe hasło"" różnią się.", MsgBoxStyle.Exclamation, Me.Text)
            txtObecneHaslo.Enabled = True
            txtNoweHaslo.Enabled = True
            txtPotwierdzHaslo.Enabled = True
            btnOK.Enabled = True
            Exit Sub
        End If

        If txtObecneHaslo.Text = txtNoweHaslo.Text Then
            MsgBox("Nowe hasło powinno być różne od obecnego.", MsgBoxStyle.Exclamation, Me.Text)
            txtObecneHaslo.Enabled = True
            txtNoweHaslo.Enabled = True
            txtPotwierdzHaslo.Enabled = True
            btnOK.Enabled = True
            Exit Sub
        End If

        ' sprawdzamy, czy numer telefonu jest liczbą 
        Dim tel As Integer
        If Int32.TryParse(txtTelefon.Text, tel) Then
            
            If txtTelefon.TextLength <> 9 Then
                MsgBox("Proszę podać poprawny 9-cyfrowy numer telefonu. To pole jest obowiązkowe.", MsgBoxStyle.Exclamation, "Niepoprawny numer telefonu")
                txtTelefon.Focus()
                txtTelefon.SelectAll()
                Exit Sub
            End If
        Else
            If txtTelefon.Text = "" Then
                MsgBox("Proszę podać 9-cyfrowy numer telefonu. To pole jest obowiązkowe.", MsgBoxStyle.Exclamation, "Brak numeru telefonu")
                txtTelefon.Focus()
                Exit Sub
            End If
            MsgBox("Numer telefonu może składać się wyłącznie z cyfr. Proszę podać poprawny 9-cyfrowy numer telefonu. To pole jest obowiązkowe.", MsgBoxStyle.Exclamation, "Niepoprawny numer telefonu")
            txtTelefon.Focus()
            Exit Sub
        End If


        ' sprawdzamy, czy poprawny email 
        If Not EmailAddressCheck(txtEmail.Text) Then
            MsgBox("Proszę podać prawidłowy adres e-mail. To pole jest obowiązkowe.", MsgBoxStyle.Exclamation, "Niepoprawny adres e-mail")
            txtEmail.Focus()
            txtEmail.SelectAll()
            Exit Sub
        End If

        

        btnOK.Enabled = True

        Cursor = Cursors.AppStarting
        Dim par As New frmZmienHasloThread
        par.pierwszy = Me.pierwszy
        par.frmRodzic = Me
        par.obecne_haslo = txtObecneHaslo.Text
        par.nowe_haslo = txtNoweHaslo.Text
        par.Aktualny_Email = txtEmail.Text
        par.Aktualny_Telefon = txtTelefon.Text
        pierwszy = False
        th = New Thread(AddressOf par.frmZmianaHasla_Tel_EmailThread_doWork)
        th.IsBackground = True
        th.Start()
    End Sub
    Function EmailAddressCheck(ByVal emailAddress As String) As Boolean
        Dim pattern As String = "^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"
        Dim emailAddressMatch As Match = Regex.Match(emailAddress, pattern)
        If emailAddressMatch.Success Then
            EmailAddressCheck = True
        Else
            EmailAddressCheck = False
        End If
    End Function

    Private Sub txtEmail_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEmail.Validated
        If Not EmailAddressCheck(txtEmail.Text) Then
            MsgBox("Podano nieprawidłowy adres email. Proszę poprawić.", MsgBoxStyle.Exclamation, "Nieprawidłowy adres email")
            txtEmail.BackColor = Color.LightSalmon
            txtEmail.Focus()
        Else : txtEmail.BackColor = Color.White
        End If
    End Sub

    Private Sub txtTelefon_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTelefon.Validated
        Dim tel As Integer
        If Int32.TryParse(txtTelefon.Text, tel) Then

            If txtTelefon.TextLength <> 9 Then
                MsgBox("Proszę podać poprawny 9-cyfrowy numer telefonu. To pole jest obowiązkowe.", MsgBoxStyle.Exclamation, "Niepoprawny numer telefonu")
                txtTelefon.BackColor = Color.LightSalmon
                txtTelefon.Focus()
                Exit Sub
            End If
            txtTelefon.BackColor = Color.White
        Else
            If txtTelefon.Text = "" Then
                MsgBox("Proszę podać 9-cyfrowy numer telefonu. To pole jest obowiązkowe.", MsgBoxStyle.Exclamation, "Brak numeru telefonu")
                txtTelefon.BackColor = Color.LightSalmon
                txtTelefon.Focus()
                Exit Sub
            End If
            MsgBox("Numer telefonu może składać się wyłącznie z cyfr. Proszę podać poprawny 9-cyfrowy numer telefonu. To pole jest obowiązkowe.", MsgBoxStyle.Exclamation, "Niepoprawny numer telefonu")
            txtTelefon.Focus()
            Exit Sub
        End If
    End Sub
End Class

