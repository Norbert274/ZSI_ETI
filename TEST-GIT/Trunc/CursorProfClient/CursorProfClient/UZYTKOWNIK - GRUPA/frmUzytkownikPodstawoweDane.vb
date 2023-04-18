Public Class frmUzytkownikPodstawoweDane
    Public frmRodzic As Form
    Public intIdUzytkownika As Integer = -1 'przekazane z formy rodzica; jeśli =0 znaczy tworzymy nowego
    Private intIdBlokady As Integer = -1 'id blokady wygenerowane w bazie na potrzeby edycji tego rekordu; -1 oznacza brak blokady
    Private strImie As String = ""
    Private strNazwisko As String = ""
    Private strTelkom As String = ""
    Private strEmail As String = ""
    Private strLogin As String = ""
    Public blue As Color = Color.DodgerBlue
    Public user_id_do_hasla As Integer 'id usera któremu zmieniamy bądź resetujemy hasło 
    Public CzyUserOddzial As Boolean = False

    Private Sub frmUzytkownik_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs)
        If intIdBlokady >= 0 Then
            'zwalniamy blokadę rekordu
            System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
            System.Net.ServicePointManager.Expect100Continue = False
            ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
            ws.Proxy.Credentials = CredentialCache.DefaultCredentials
            'ws.Url = frmGlowna.strWebservice
            Try
                ws.UserEdytujAnuluj(frmGlowna.sesja, intIdBlokady)
            Catch ex As Exception
            End Try
        End If
    End Sub



    Private Sub btnAnuluj_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAnuluj.Click

        'intIdBlokady = -1
        Me.Close()
    End Sub

    Private Function bylyZmiany() As Boolean
        If txtImie.Text <> strImie OrElse _
        txtNazwisko.Text <> strNazwisko OrElse _
        txtTelkom.Text <> strTelkom OrElse _
        txtEmail.Text <> strEmail OrElse _
        txtLogin.Text <> strLogin OrElse _
        txtHaslo.Text.Length > 0 OrElse _
        txtHaslo2.Text.Length > 0 Then

            Return True
        End If
        Return False
    End Function

    Private Function zapiszZmiany(ByVal pozostawBlokade As Boolean) As Boolean
        Dim bBylyZmiany As Boolean = False
        Dim czy_maile As Integer

        czy_maile = 1
        'Else
        'czy_maile = 0
        'End If
        'jeżeli edytujemy nowy rekord, to zaznaczamy flagę bBylyZmiany zawsze na True - żeby zawsze zapisać dane
        If intIdBlokady <= 0 Then bBylyZmiany = True

        'czy coś się zmieniło?
        bBylyZmiany = bylyZmiany()
            'weryfikacja poprawności danych
            If txtNazwa.Text.Length < 1 Then
                MsgBox("Pole 'nazwa wyświetlana' nie może być puste.", MsgBoxStyle.Exclamation, Me.Text)
                txtNazwa.Focus()
                Return False
            End If
            If txtHaslo.Text <> txtHaslo2.Text Then
                MsgBox("Zawartość pól 'hasło' i 'hasło (powtórz)' jest różna.", MsgBoxStyle.Exclamation, Me.Text)
                txtHaslo.Focus()
                Return False
            End If
            If intIdBlokady <= 0 AndAlso txtHaslo.Text.Length < 1 Then
                MsgBox("Musisz nadać hasło użytkownikowi.", MsgBoxStyle.Exclamation, Me.Text)
                txtHaslo.Focus()
                Return False
            ElseIf txtHaslo.Text.Length > 0 AndAlso txtHaslo.Text.Length < 6 Then
                Dim wynik = MsgBox("Podane hasło jest krótsze niż 6 znaków. Czy na pewno stworzyć użytkownika z tak krótkim hasłem?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, Me.Text)
                txtHaslo.Focus()
                If wynik <> vbYes Then Return False
            End If

            'If chkMaile.Checked = True Then
            czy_maile = 1
            'Else
            '    czy_maile = 0
            'End If

            System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
            System.Net.ServicePointManager.Expect100Continue = False
            ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
            ws.Proxy.Credentials = CredentialCache.DefaultCredentials
            'ws.Url = frmGlowna.strWebservice
            Dim wsWynik As wsCursorProf.UserEdytujPodstawoweDaneZapiszWynik
            Try
                Cursor = Cursors.WaitCursor
                Application.DoEvents()
                wsWynik = ws.UserEdytujPodstawoweDaneZapisz(frmGlowna.sesja, txtImie.Text, txtNazwisko.Text, _
                    txtNazwa.Text, txtTelkom.Text, txtEmail.Text, txtLogin.Text, txtHaslo.Text, _
                    czy_maile, intIdBlokady, pozostawBlokade)
                Cursor = Cursors.Default
            Catch ex As Exception
                Cursor = Cursors.Default
                MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
                Return False
            End Try
            If wsWynik.status < 0 Then
                MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, Me.Text)
                Return False
            ElseIf wsWynik.status > 0 Then
                MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, Me.Text)
            End If

            'modyfikujemy zmienne formy po udanym zapisie

            intIdBlokady = wsWynik.blokada_id
            frmGlowna.lblStatus.Text = "Poprawnie zapisano dane użytkownika " & txtNazwa.Text
            frmGlowna.timer.Interval = 2000 'komunikat zniknie po 2s
            frmGlowna.timer.Start()
            txtHaslo.Text = ""
            txtHaslo2.Text = ""
            btnZastosuj.Enabled = False
            btnZastosuj.BackColor = Color.LightGray
            strImie = txtImie.Text
            txtImie.SelectAll()
            strNazwisko = txtNazwisko.Text
            strTelkom = txtTelkom.Text
            strEmail = txtEmail.Text
            strLogin = txtLogin.Text
            btnAdresy.Enabled = True

            'jeśli nie był pokazany label "status hasła" to znaczy, że zapisaliśmy nowego użytkownika
            If Not lblHasloStatus.Visible Then
                txtHaslo.Visible = False
                txtHaslo2.Visible = False
                lblHaslo2.Visible = False
                lblHasloStatus.Visible = True
                btnZmienHaslo.Visible = True
                If txtHaslo.Text.Length > 0 Then
                    lblHasloStatus.Text = "(ustawione)"
                Else
                    lblHasloStatus.Text = "(puste)"
                End If
            End If

        If Not bBylyZmiany Then
            frmGlowna.lblStatus.Text = "Dane użytkownika " & txtNazwa.Text & " nie uległy zmianie, zapis do bazy nie był konieczny"
            frmGlowna.timer.Interval = 2000 'komunikat zniknie po 2s
            frmGlowna.timer.Start()
            If Not pozostawBlokade Then
                Dim ws1 As New wsCursorProf.CursorService
                System.Net.ServicePointManager.Expect100Continue = False
                ws1.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
                ws1.Proxy.Credentials = CredentialCache.DefaultCredentials
                'ws.Url = frmGlowna.strWebservice
                Try
                    ws1.UserEdytujAnuluj(frmGlowna.sesja, intIdBlokady)
                Catch ex As Exception
                End Try
            End If
        End If

        MsgBox("Poprawnie zapisano dane użytkownika.", MsgBoxStyle.Information, Me.Text)

        Return True
    End Function

    Private Sub btnZastosuj_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnZastosuj.Click
        zapiszZmiany(True)
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        If zapiszZmiany(False) Then
            intIdBlokady = -1 'blokada już zwolniona w funkcji zapiszZmiany()
            Me.Close()
        End If
    End Sub

    Private Sub txtImie_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtImie.TextChanged
        btnZastosuj.Enabled = True
        btnOk.Enabled = True
        btnOk.BackColor = blue
        btnZastosuj.BackColor = blue
    End Sub

    Private Sub txtNazwisko_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNazwisko.TextChanged
        btnZastosuj.Enabled = True
        btnOk.Enabled = True
        btnOk.BackColor = blue
        btnZastosuj.BackColor = blue
    End Sub

    Private Sub txtNazwa_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNazwa.TextChanged
        btnZastosuj.Enabled = True
        btnOk.Enabled = True
    End Sub

    Private Sub txtTelkom_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTelkom.TextChanged
        btnZastosuj.Enabled = True
        btnOk.Enabled = True
        btnOk.BackColor = blue
        btnZastosuj.BackColor = blue
    End Sub

    Private Sub txtEmail_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEmail.TextChanged
        btnZastosuj.Enabled = True
        btnOk.Enabled = True
        btnOk.BackColor = blue
        btnZastosuj.BackColor = blue
    End Sub

    Private Sub txtLogin_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLogin.TextChanged
        btnZastosuj.Enabled = True
        btnOk.Enabled = True
        btnOk.BackColor = blue
        btnZastosuj.BackColor = blue
    End Sub

    Private Sub txtHaslo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtHaslo.TextChanged
        btnZastosuj.Enabled = True
        btnOk.Enabled = True
        btnOk.BackColor = blue
        btnZastosuj.BackColor = blue
    End Sub

    Private Sub txtHaslo2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtHaslo2.TextChanged
        btnZastosuj.Enabled = True
        btnOk.Enabled = True
    End Sub

    Private Sub btnZmienHaslo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnZmienHaslo.Click
        Dim frm As New frmUzytkownikHaslo
        frm.user_id = user_id_do_hasla
        frm.ShowDialog()
        If frm.strHaslo.Length > 0 Then
            'ustawiono hasło
            txtHaslo.Text = frm.strHaslo
            txtHaslo2.Text = frm.strHaslo
            lblHasloStatus.Text = "(ustawione)"
        End If
        frm.Dispose()
    End Sub

    Private Sub btnAdresy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdresy.Click
        If intIdUzytkownika < 0 Then
            MsgBox("Błąd wewnętrzny systemu. Zmienna intIdUzytkownika jest nie ustawiona.", MsgBoxStyle.Critical, Me.Text)
            Exit Sub
        End If
        Dim frm As New frmAdresy
        frm.frmRodzic = Me
        frm.intIdUzytkownika = intIdUzytkownika
        frm.strNazwaUzytkownika = txtNazwa.Text
        If Me.Modal Then
            frm.ShowDialog()
        Else
            frm.MdiParent = frmGlowna
            frm.Show()
        End If
    End Sub

    Private Sub chkMaile_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        btnOk.Enabled = True
        btnZastosuj.Enabled = True
        btnOk.BackColor = blue
        btnZastosuj.BackColor = blue
    End Sub

    Public Sub AdresyLicznikZwieksz()
        lblAdresy.Text += 1
    End Sub

    Public Sub AdresyLicznikZmniejsz()
        lblAdresy.Text -= 1
    End Sub

    Private Sub frmUzytkownikPodstawoweDane_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If bylyZmiany() Then
            Dim result As DialogResult = MsgBox("Od ostatniego odczytu z serwera wprowadzono zmiany w tym użytkowniku. Czy zapisać wprowadzone zmiany?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1, Me.Text)
            If result = Windows.Forms.DialogResult.Yes Then
                If zapiszZmiany(False) Then
                    intIdBlokady = -1
                    Me.Close()
                End If
            Else
                Exit Sub

                Me.Close()
            End If
        End If

    End Sub

    Private Sub frmUzytkownikPodstawoweDane_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If Not wczytaj() Then
            Me.Close()
        End If

        If sprawdz_czy_user_typ_oddzial() Then
            If CzyUserOddzial Then
                gbAdresy.Enabled = False
            Else
                gbAdresy.Enabled = True
            End If
        End If

    End Sub
    Public Function wczytaj() As Boolean
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        'ws.Url = frmGlowna.strWebservice
        Dim wsWynik As wsCursorProf.UserEdytujWynik
        user_id_do_hasla = intIdUzytkownika
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.UserEdytuj(frmGlowna.sesja, intIdUzytkownika)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
            Return False
        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, Me.Text)
            Return False
        ElseIf wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, Me.Text)
        End If

        intIdBlokady = wsWynik.blokada_id
        Me.Text = "Edycja użytkownika " & wsWynik.nazwa
        txtImie.Text = wsWynik.imie
        strImie = wsWynik.imie
        txtImie.Focus()
        txtImie.SelectAll()
        txtNazwisko.Text = wsWynik.nazwisko
        strNazwisko = wsWynik.nazwisko
        txtNazwa.Text = wsWynik.nazwa
        txtTelkom.Text = wsWynik.telkom
        strTelkom = wsWynik.telkom
        txtEmail.Text = wsWynik.email
        strEmail = wsWynik.email
        txtLogin.Text = wsWynik.login
        strLogin = wsWynik.login
        txtHaslo.Visible = False
        txtHaslo2.Visible = False
        lblHaslo2.Visible = False
        lblHasloStatus.Visible = True
        btnZmienHaslo.Visible = True
        'chkMaile.Checked = True
        If wsWynik.haslo Then
            lblHasloStatus.Text = "(ustawione)"
        Else
            lblHasloStatus.Text = "(puste)"
        End If
        lblAdresy.Text = wsWynik.adresy_ilosc

        btnZastosuj.Enabled = False
        If intIdBlokady <= 0 Then btnOk.Enabled = False
        btnZastosuj.BackColor = Color.LightGray
        If btnOk.Enabled Then
            btnOk.BackColor = blue
        Else
            btnOk.BackColor = Color.LightGray
        End If
        Return True
    End Function

    Private Function sprawdz_czy_user_typ_oddzial() As Boolean
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        'ws.Url = frmGlowna.strWebservice
        Dim wsWynik As wsCursorProf.CzyUserOddzialWynik

        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.CzyUserOddzial(frmGlowna.sesja)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
            Me.Close()
            Return False
            Exit Function
        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, Me.Text)
            Return False
        End If
        If wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, Me.Text)
        End If
        If wsWynik.status = 0 Then
            If wsWynik.czy_oddzial = 1 Then
                CzyUserOddzial = True
            ElseIf wsWynik.czy_oddzial = 0 Then
                CzyUserOddzial = False
            Else
                CzyUserOddzial = True
            End If
        Else
            MsgBox("Nie ustalono typu", MsgBoxStyle.Exclamation, Me.Text)
        End If
        Return True
    End Function

  
    Private Sub btnNotyfikacje_Click(sender As System.Object, e As System.EventArgs) Handles btnNotyfikacje.Click
        Dim frm As New frmNotyfikacjeUzytkownikow
        frm.intIdUser = frmGlowna.intIdUzytkownikZalogowany
        frm.ShowDialog()
    End Sub
End Class