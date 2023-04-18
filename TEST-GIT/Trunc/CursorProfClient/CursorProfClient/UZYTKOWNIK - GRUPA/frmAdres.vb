Imports System.Reflection
Public Class frmAdres
    Public frmRodzic As frmAdresy
    Public intIdAdresu As Integer = -1 'przekazane z formy rodzica; jeœli =0 znaczy tworzymy nowy
    Public intIdUzytkownika As Integer = -1 'przekazane z formy rodzica
    Public strNazwaUzytkownika As String = "" 'przekazane z formy rodzica
    Private bSprawdzicZmianyPrzyWyjsciu As Boolean = True 'czy zaproponowaæ u¿ytkownikowi zapisanie zmian przy zamykaniu okna?
    Private intIdBlokady As Integer = -1 'id blokady wygenerowane w bazie na potrzeby edycji tego rekordu; -1 oznacza brak blokady
    Private strNazwa As String = ""
    Private strAdres As String = ""
    Private strKod As String = "  -"
    Private strMiasto As String = ""
    Private bDomyslny As Boolean = False

    Private odKodPocz As Boolean = False
    Private bKodvalid As Boolean = False
    Private dtkodyPocztowe As New DataTable

    Private Sub frmAdres_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If bSprawdzicZmianyPrzyWyjsciu AndAlso bylyZmiany() Then
            Dim odp As MsgBoxResult = MsgBox("Od ostatniego odczytu z serwera wprowadzono zmiany w tym adresie. Czy zapisaæ wprowadzone zmiany?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1, Me.Text)
            If odp = MsgBoxResult.Yes Then
                If Not zapiszZmiany(False) Then
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        End If
        If intIdBlokady >= 0 Then
            'zwalniamy blokadê rekordu
            System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
            System.Net.ServicePointManager.Expect100Continue = False
            ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
            ws.Proxy.Credentials = CredentialCache.DefaultCredentials
            ' ws.Url = frmGlowna.strWebservice
            Try
                ws.AdresEdytujAnuluj(frmGlowna.sesja, intIdBlokady)
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub frmAdres_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If intIdAdresu > 0 Then
            'tryb edycji - ³adujemy dane o adresie i oznaczamy blokadê w bazie
            System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
            System.Net.ServicePointManager.Expect100Continue = False
            ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
            ws.Proxy.Credentials = CredentialCache.DefaultCredentials
            'ws.Url = frmGlowna.strWebservice
            Dim wsWynik As wsCursorProf.AdresEdytujWynik

            Try
                Cursor = Cursors.WaitCursor
                Application.DoEvents()
                wsWynik = ws.AdresEdytuj(frmGlowna.sesja, intIdAdresu)
                Cursor = Cursors.Default
            Catch ex As Exception
                Cursor = Cursors.Default
                MsgBox("B³¹d komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegó³y b³êdu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
                Me.Close()
                Exit Sub
            End Try

            If wsWynik.status < 0 Then
                MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, Me.Text)
                Me.Close()
                Exit Sub
            ElseIf wsWynik.status > 0 Then
                MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, Me.Text)
            End If

            intIdBlokady = wsWynik.blokada_id
            Me.Text = "Edycja adresu dla " & strNazwaUzytkownika
            txtNazwa.Text = wsWynik.nazwa
            strNazwa = wsWynik.nazwa
            txtNazwa.Focus()
            txtNazwa.SelectAll()
            txtAdres.Text = wsWynik.adres
            strAdres = wsWynik.adres
            txtKod.Text = wsWynik.kod
            strKod = wsWynik.kod
            txtMiasto.Text = wsWynik.miasto
            strMiasto = wsWynik.miasto
            bDomyslny = wsWynik.domyslny
            chkDomyslny.Checked = bDomyslny
        Else
            'tryb dodawania nowego
            Me.Text = "Tworzenie nowego adresu dla " & strNazwaUzytkownika
            txtNazwa.Focus()
        End If
        btnZastosuj.Enabled = False
        If intIdBlokady <= 0 Then btnOk.Enabled = False
    End Sub

    Private Sub btnAnuluj_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAnuluj.Click
        'bSprawdzicZmianyPrzyWyjsciu = False
        Me.Close()
    End Sub

    Private Function zapiszZmiany(ByVal pozostawBlokade As Boolean) As Boolean
        Dim bBylyZmiany As Boolean = False

        'czy coœ siê zmieni³o?
        If bylyZmiany() OrElse intIdBlokady <= 0 Then

            bBylyZmiany = True

            'weryfikacja poprawnoœci danych
            If txtNazwa.Text.Length < 1 Then
                MsgBox("Pole 'Nazwa' nie mo¿e byæ puste.", MsgBoxStyle.Exclamation, Me.Text)
                txtNazwa.Focus()
                Return False
            End If

            If txtAdres.Text.Length < 1 Then
                MsgBox("Pole 'Adres' nie mo¿e byæ puste.", MsgBoxStyle.Exclamation, Me.Text)
                txtAdres.Focus()
                Return False
            End If

            If txtKod.Text.Length <> 6 Then
                MsgBox("Pole 'Kod' ma niepoprawny format.", MsgBoxStyle.Exclamation, Me.Text)
                txtKod.Focus()
                Return False
            End If
            If txtMiasto.Text.Length < 1 Then
                MsgBox("Pole 'Miasto' nie mo¿e byæ puste.", MsgBoxStyle.Exclamation, Me.Text)
                txtMiasto.Focus()
                Return False
            End If
         
            System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
            System.Net.ServicePointManager.Expect100Continue = False
            ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
            ws.Proxy.Credentials = CredentialCache.DefaultCredentials
            'ws.Url = frmGlowna.strWebservice
            Dim wsWynik As wsCursorProf.AdresEdytujZapiszWynik
            Try
                Cursor = Cursors.WaitCursor
                Application.DoEvents()
                wsWynik = ws.AdresEdytujZapisz(frmGlowna.sesja, intIdUzytkownika, txtNazwa.Text, txtAdres.Text, _
                    txtKod.Text, txtMiasto.Text, intIdBlokady, pozostawBlokade, IIf(chkDomyslny.Checked, 1, 0))
                Cursor = Cursors.Default
            Catch ex As Exception
                Cursor = Cursors.Default
                MsgBox("B³¹d komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegó³y b³êdu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
                Return False
            End Try
            If wsWynik.status < 0 Then
                MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, Me.Text)
                Return False
            ElseIf wsWynik.status > 0 Then
                MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, Me.Text)
            End If

            bSprawdzicZmianyPrzyWyjsciu = False
            'modyfikujemy zmienne formy po udanym zapisie
            frmGlowna.lblStatus.Text = "Poprawnie zapisano dane adresowe dla u¿ytkownika " & strNazwaUzytkownika
            frmGlowna.timer.Interval = 3000 'komunikat zniknie po 2s
            frmGlowna.timer.Start()
            btnZastosuj.Enabled = False
            strNazwa = txtNazwa.Text
            txtNazwa.SelectAll()
            strAdres = txtAdres.Text
            strKod = txtKod.Text
            strMiasto = txtMiasto.Text

            If intIdBlokady <= 0 Then 'zapisaliœmy nowy rekord
                Me.Text = "Edycja adresu dla " & strNazwaUzytkownika
                If Not frmRodzic Is Nothing Then
                    'aktualizujemy licznik w oknie rodzica-rodzica
                    'If frmRodzic.enumTrybPracy = frmAdresy.frmAdresyTrybPracy.adresyDlaUzytkownika Then
                    Dim frmRodzicRodzica As Form
                    frmRodzicRodzica = frmRodzic.frmRodzic
                    If Not frmRodzicRodzica Is Nothing Then
                        Dim m As MethodInfo() = frmRodzicRodzica.GetType.GetMethods()
                        For licznik As Integer = 0 To m.GetUpperBound(0)
                            If m(licznik).Name = "AdresyLicznikZwieksz" Then
                                m(licznik).Invoke(frmRodzicRodzica, Nothing)
                            End If
                        Next
                    End If
                    'End If
                End If
            End If



            If Not frmRodzic Is Nothing Then
                Dim m As MethodInfo() = frmRodzic.GetType.GetMethods()
                For licznik As Integer = 0 To m.GetUpperBound(0)
                    If m(licznik).Name = "odswiezListy" Then
                        m(licznik).Invoke(frmRodzic, Nothing)
                    End If
                Next
            End If
            intIdBlokady = wsWynik.blokada_id
        Else
            bBylyZmiany = False
        End If

        If Not bBylyZmiany Then
            frmGlowna.lblStatus.Text = "Dane adresowe nie uleg³y zmianie, zapis do bazy nie by³ konieczny."
            frmGlowna.timer.Interval = 3000 'komunikat zniknie po 2s
            frmGlowna.timer.Start()
            If Not pozostawBlokade Then
                System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
                System.Net.ServicePointManager.Expect100Continue = False
                ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
                ws.Proxy.Credentials = CredentialCache.DefaultCredentials
                'ws.Url = frmGlowna.strWebservice
                Try
                    ws.AdresEdytujAnuluj(frmGlowna.sesja, intIdBlokady)
                Catch ex As Exception
                End Try
            End If
        End If

        Return True
    End Function

    Private Function bylyZmiany() As Boolean
        If txtNazwa.Text <> strNazwa OrElse _
        txtAdres.Text <> strAdres OrElse _
        txtKod.Text <> strKod OrElse _
        chkDomyslny.Checked <> bDomyslny OrElse _
        txtMiasto.Text <> strMiasto Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub btnZastosuj_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnZastosuj.Click
        zapiszZmiany(True)
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        If zapiszZmiany(False) Then
            intIdBlokady = -1 'blokada ju¿ zwolniona w funkcji zapiszZmiany()
            Me.Close()
        End If
    End Sub

    Private Sub txtNazwa_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNazwa.TextChanged
        btnZastosuj.Enabled = True
        btnOk.Enabled = True
    End Sub

    Private Sub txtAdres_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAdres.TextChanged
        btnZastosuj.Enabled = True
        btnOk.Enabled = True
    End Sub

    'Private Sub txtKod_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtKod.TextChanged
    '    btnZastosuj.Enabled = True
    '    btnOk.Enabled = True
    'End Sub

    Private Sub txtMiasto_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMiasto.TextChanged
        btnZastosuj.Enabled = True
        btnOk.Enabled = True
    End Sub

    Private Sub txtKod_TextChanged(sender As Object, e As System.EventArgs) Handles txtKod.TextChanged
        If odKodPocz = False Then
            If txtKod.Text.Length = 6 Then
                odswierzKodPocztowy(txtKod.Text)
            End If
            txtMiasto.Text = String.Empty
            validKodPocztowy(txtKod.Text)

        End If
    End Sub
    Private Sub validKodPocztowy(ByVal kod As String)
        bKodvalid = False

        If kod.Length = 6 Then
            If dtkodyPocztowe.Select(String.Format("KOD_POCZTOWY = '{0}'", kod)).Length = 1 Then

                txtMiasto.Text = dtkodyPocztowe.Select(String.Format("KOD_POCZTOWY = '{0}'", kod))(0).Item("MIASTO").ToString

                bKodvalid = True
            Else
                MsgBox("Nieprawid³owy kod pocztowy.", MsgBoxStyle.Critical, "Nieprawid³owe dane")
                txtKod.Text = ""
                txtKod.Focus()
            End If

        End If

    End Sub

    Private Function odswierzKodPocztowy(ByVal kod As String) As Boolean
        odKodPocz = True
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        'ws.Url = frmGlowna.strWebservice
        Dim wsWynik As wsCursorProf.KodyPocztoweOdczytajWynik

        Try
            'Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.KodyPocztoweOdczytaj(frmGlowna.sesja, kod)
            'Cursor = Cursors.Default
        Catch ex As Exception
            'Cursor = Cursors.Default
            MsgBox("B³¹d komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegó³y b³êdu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
            Me.Close()
            odKodPocz = False
            Return False
            Exit Function
        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, Me.Text)
            Return False
            odKodPocz = False
        End If
        If wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, Me.Text)
        End If
        odKodPocz = True
        dtkodyPocztowe = wsWynik.dane.Tables(0)


        txtKod.Text = kod
        txtKod.SelectionStart = kod.Length
        txtKod.SelectionLength = 0

        'cmbKodPocztowy.SelectionStart = kod.Length
        'cmbKodPocztowy.SelectionLength = 0
        'odswierzKodPocztowy(kod)
        'cmbKodPocztowy.DataSource = Nothing
        'cmbKodPocztowy.DataSource = dtkodyPocztowe
        'cmbKodPocztowy.DisplayMember = "KOD_POCZTOWY"
        'cmbKodPocztowy.ValueMember = "KOD_POCZTOWY"
        'cmbKodPocztowy.SelectedValue = kod
        odKodPocz = False
        Return True
    End Function
End Class