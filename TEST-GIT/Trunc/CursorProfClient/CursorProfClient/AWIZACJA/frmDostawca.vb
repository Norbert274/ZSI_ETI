Public Class frmDostawca
    Private ktore_niepoprawne As String = String.Empty
    Private czy_byl_klik_na_dodaj As Boolean = False
    Public strNazwaDostawcy As String
    Public strAdresDostawcy As String
    Public strKodDostawcy As String
    Public strMiastoDostawcy As String
    Public strKrajDostawcy As String
    Private dtkodyPocztowe As DataTable
    Private bKodvalid As Boolean = False
    Private odKodPocz As Boolean = False
    Public dostawcaId As Integer

    Private Sub btnAnuluj_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAnuluj.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub btnDodajNowegoDostawce_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDodajNowegoDostawce.Click
        If Not WalidacjaDostawcy() Then
            If InStr(ktore_niepoprawne, ",") > 0 Then
                MsgBox("Nie wypełniono następujących pól: " & vbNewLine & ktore_niepoprawne & ".", MsgBoxStyle.Exclamation, "Pola wymagane")
            Else
                MsgBox("Nie wypełniono pola " & ktore_niepoprawne & ".", MsgBoxStyle.Exclamation, "Pole wymagane")
            End If
            ktore_niepoprawne = String.Empty
            Exit Sub
        End If

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub frmDostawca_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If dostawcaId > -1 Then
            If bylyZmiany() AndAlso Me.DialogResult = System.Windows.Forms.DialogResult.Cancel Then
                Dim result As DialogResult = MsgBox("Wprowadzono zmiany w danych dostawcy. Czy chcesz zapisać wynik?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "Edycja dostawcy")
                If result = Windows.Forms.DialogResult.No Then
                    Exit Sub
                    Me.Close()
                Else
                    If Not WalidacjaDostawcy() Then
                        If InStr(ktore_niepoprawne, ",") > 0 Then
                            MsgBox("Nie wypełniono następujących pól: " & vbNewLine & ktore_niepoprawne & ".", MsgBoxStyle.Exclamation, "Pola wymagane")
                        Else
                            MsgBox("Nie wypełniono pola " & ktore_niepoprawne & ".", MsgBoxStyle.Exclamation, "Pole wymagane")
                        End If
                        ktore_niepoprawne = String.Empty
                        Exit Sub
                    End If

                    Me.DialogResult = System.Windows.Forms.DialogResult.OK
                End If
            End If
            'Else
            '    If czyCosWypelnione() Then
            '        Dim result As DialogResult = MsgBox("Wprowadzono dane dla nowego dostawcy. Czy chcesz zapisać wynik?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "Nowy dostawca")
            '        If result = Windows.Forms.DialogResult.No Then
            '            Exit Sub
            '            Me.Close()
            '        Else
            '            If Not WalidacjaDostawcy() Then
            '                If InStr(ktore_niepoprawne, ",") > 0 Then
            '                    MsgBox("Nie wypełniono następujących pól: " & vbNewLine & ktore_niepoprawne & ".", MsgBoxStyle.Exclamation, "Pola wymagane")
            '                Else
            '                    MsgBox("Nie wypełniono pola " & ktore_niepoprawne & ".", MsgBoxStyle.Exclamation, "Pole wymagane")
            '                End If
            '                ktore_niepoprawne = String.Empty
            '                Exit Sub
            '            End If

            '            Me.DialogResult = System.Windows.Forms.DialogResult.OK
            '        End If
            '    End If
            
        End If

    End Sub

    Private Sub frmDostawca_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        txtAdresDostawca.BackColor = Color.White
        txtKodPocztowy.BackColor = Color.White
        txtKrajDostawca.BackColor = Color.White
        txtMiastoDostawca.BackColor = Color.White
        txtNazwaDostawcy.BackColor = Color.White
        txtNazwaDostawcy.Text = strNazwaDostawcy
    End Sub
    Private Function WalidacjaDostawcy() As Boolean
        czy_byl_klik_na_dodaj = True
        Dim czy_jest_ok As Boolean = True

        If txtNazwaDostawcy.Text = String.Empty Then
            txtNazwaDostawcy.BackColor = Color.LightCoral
            txtNazwaDostawcy.ForeColor = Color.White
            txtNazwaDostawcy.Focus()
            If ktore_niepoprawne = String.Empty Then
                ktore_niepoprawne = "   - Nazwa"
            End If
            czy_jest_ok = False
        End If
        If txtAdresDostawca.Text = String.Empty Then
            txtAdresDostawca.BackColor = Color.LightCoral
            txtAdresDostawca.ForeColor = Color.White
            txtAdresDostawca.Focus()
            If ktore_niepoprawne = String.Empty Then
                ktore_niepoprawne = "   - Adres"
            Else : ktore_niepoprawne = ktore_niepoprawne & ", " & vbNewLine & "   - Adres"
            End If
            czy_jest_ok = False
        End If
        If txtKodPocztowy.Text = String.Empty Then
            txtKodPocztowy.BackColor = Color.LightCoral
            txtKodPocztowy.ForeColor = Color.White
            txtKodPocztowy.Focus()
            If ktore_niepoprawne = String.Empty Then
                ktore_niepoprawne = "   - Kod pocztowy"
            Else : ktore_niepoprawne = ktore_niepoprawne & ", " & vbNewLine & "   - Kod pocztowy"
            End If
            czy_jest_ok = False
        End If
        If txtMiastoDostawca.Text = String.Empty Then
            txtMiastoDostawca.BackColor = Color.LightCoral
            txtMiastoDostawca.ForeColor = Color.White
            txtMiastoDostawca.Focus()
            If ktore_niepoprawne = String.Empty Then
                ktore_niepoprawne = "   - Miasto"
            Else : ktore_niepoprawne = ktore_niepoprawne & ", " & vbNewLine & "   - Miasto"
            End If
            czy_jest_ok = False
        End If
        If txtKrajDostawca.Text = String.Empty Then
            txtKrajDostawca.BackColor = Color.LightCoral
            txtKrajDostawca.ForeColor = Color.White
            txtKrajDostawca.Focus()
            If ktore_niepoprawne = String.Empty Then
                ktore_niepoprawne = "   - Kraj"
            Else : ktore_niepoprawne = ktore_niepoprawne & ", " & vbNewLine & "   - Kraj"
            End If
            czy_jest_ok = False
        End If

        If Len(txtNazwaDostawcy.Text) > 25 Then
            txtNazwaDostawcy.BackColor = Color.LightCoral
            txtNazwaDostawcy.ForeColor = Color.White
            txtNazwaDostawcy.Focus()
            MsgBox("Nazwa dostawcy nie może być dłuższa niż 25 znaków!", MsgBoxStyle.Exclamation, "Zbyt długa nazwa dostawcy")
            czy_jest_ok = False
        End If
        If czy_jest_ok Then
            Return True
        Else
            Return False
        End If
        Return True
    End Function

    Private Sub txtAdresDostawca_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAdresDostawca.Validated
        If czy_byl_klik_na_dodaj Then
            If Not txtAdresDostawca.Text = String.Empty Then
                txtAdresDostawca.BackColor = Color.White
                txtAdresDostawca.ForeColor = Color.Black
                txtAdresDostawca.Focus()
            Else : txtAdresDostawca.BackColor = Color.LightCoral
                txtAdresDostawca.ForeColor = Color.White
            End If
        End If

    End Sub

    Private Sub txtKodPocztowy_Validated(ByVal sender As Object, ByVal e As System.EventArgs)
        If czy_byl_klik_na_dodaj Then
            If Not txtKodPocztowy.Text = String.Empty Then
                txtKodPocztowy.BackColor = Color.White
                txtKodPocztowy.ForeColor = Color.Black
                txtKodPocztowy.Focus()
            Else : txtKodPocztowy.BackColor = Color.LightCoral
                txtKodPocztowy.ForeColor = Color.White
            End If
        End If

    End Sub

    Private Sub txtKrajDostawca_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtKrajDostawca.Validated
        If czy_byl_klik_na_dodaj Then
            If Not txtKrajDostawca.Text = String.Empty Then
                txtKrajDostawca.BackColor = Color.White
                txtKrajDostawca.ForeColor = Color.Black
                txtKrajDostawca.Focus()
            Else : txtKrajDostawca.BackColor = Color.LightCoral
                txtKrajDostawca.ForeColor = Color.White
            End If
        End If

    End Sub

    Private Sub txtMiastoDostawca_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMiastoDostawca.Validated
        If czy_byl_klik_na_dodaj Then
            If Not txtMiastoDostawca.Text = String.Empty Then
                txtMiastoDostawca.BackColor = Color.White
                txtMiastoDostawca.ForeColor = Color.Black
                txtMiastoDostawca.Focus()
            Else : txtMiastoDostawca.BackColor = Color.LightCoral
                txtMiastoDostawca.ForeColor = Color.White
            End If
        End If

    End Sub

    Private Sub txtNazwaDostawcy_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNazwaDostawcy.Validated
        If czy_byl_klik_na_dodaj Then
            If Not txtNazwaDostawcy.Text = String.Empty Or Len(txtNazwaDostawcy.Text) > 25 Then
                txtNazwaDostawcy.BackColor = Color.White
                txtNazwaDostawcy.ForeColor = Color.Black
                txtNazwaDostawcy.Focus()
            Else : txtNazwaDostawcy.BackColor = Color.LightCoral
            End If
        End If

    End Sub

    Private Function bylyZmiany() As Boolean
        If strAdresDostawcy <> txtAdresDostawca.Text OrElse _
            strNazwaDostawcy <> txtNazwaDostawcy.Text OrElse _
            strKodDostawcy <> txtKodPocztowy.Text OrElse _
            strMiastoDostawcy <> txtMiastoDostawca.Text OrElse _
            strKrajDostawcy <> txtKrajDostawca.Text Then
            Return True
        End If
        Return False
    End Function

    Private Sub validKodPocztowy(ByVal kod As String)
        bKodvalid = False

        If kod.Length = 6 Then
            If dtkodyPocztowe.Select(String.Format("KOD_POCZTOWY = '{0}'", kod)).Length = 1 Then

                txtMiastoDostawca.Text = dtkodyPocztowe.Select(String.Format("KOD_POCZTOWY = '{0}'", kod))(0).Item("MIASTO").ToString
            End If
        Else
            MsgBox("Nieprawidłowy kod pocztowy.", MsgBoxStyle.Critical, "Nieprawidłowe dane")
            txtKodPocztowy.Text = ""
            txtKodPocztowy.Focus()
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
            MsgBox("Błąd komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
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
        dtkodyPocztowe = wsWynik.dane.Tables(0)
      
            txtKodPocztowy.Text = kod
            txtKodPocztowy.SelectionStart = kod.Length
            txtKodPocztowy.SelectionLength = 0

            odKodPocz = False
            Return True
    End Function

    Private Sub txtKodPocztowy_TextChanged(sender As Object, e As System.EventArgs) Handles txtKodPocztowy.TextChanged
        If txtKodPocztowy.Text.Length <> 6 Then
            Exit Sub
        End If

        If odKodPocz = False Then
            odswierzKodPocztowy(txtKodPocztowy.Text)
            validKodPocztowy(txtKodPocztowy.Text)
        End If
    End Sub

    'Private Function czyCosWypelnione() As Boolean
    '    If txtAdresDostawca.TextLength > 0 OrElse txtKodPocztowy.Text <> "  -" OrElse txtKrajDostawca.TextLength > 0 OrElse _
    '    txtMiastoDostawca.TextLength > 0 OrElse txtNazwaDostawcy.TextLength > 0 Then
    '        Return True
    '    End If

    '    Return False

    'End Function
End Class