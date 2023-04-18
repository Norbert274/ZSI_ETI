Imports System.Text
Public Class frmLimityDodajUzytkownik

    Public intIdWybranegoMagazynu As Integer = -1 'z tej zmiennej okno wywołujące może odczytać wybrane TEAM_ID
    Public strNazwaWybranegoMagazynu As String
    Private bMoznaZamknac As Boolean = False

    Private Sub frmLimityDodajUzytkownik_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If czyBylyZmiany() = True AndAlso bMoznaZamknac = False Then
            Dim result As DialogResult = MsgBox("Istnieją wypełnione pola limitów. Czy chcesz zapisać zmiany?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1, Me.Text)
            If result = MsgBoxResult.Yes Then
                If zapisz_limit() = True Then
                    txtLimit.Text = ""
                    rtxtKomentarz.Text = ""
                    MsgBox("Pomyślnie zapisano limity dla wybranych użytkowników.", MsgBoxStyle.Information, Me.Text)
                Else
                    e.Cancel = True
                    Exit Sub
                End If

            Else
                bMoznaZamknac = True
                e.Cancel = True
                Me.Close()
            End If
        End If
    End Sub 'z tej zmiennej okno wywołujące może odczytać nazwę wybranego TEAMu

    Private Sub frmLimityDodajUzytkownik_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If Not wczytaj() Then Me.Close()
    End Sub

    Private Function zapisz_limit() As Boolean
        Dim wynik As Decimal
        For Each row As DataGridViewRow In dgv.Rows
            If dgv.Rows(row.Index).Cells("wybierz").Selected Then
                If dgv.CurrentCell.IsInEditMode Then
                    dgv.EndEdit()
                End If
            End If
        Next

        'If wybierz(dgv.CurrentCell) Then Me.Close()


        If Not Decimal.TryParse(txtLimit.Text, wynik) Then
            MsgBox("Wartość limitu musi być liczbą", MsgBoxStyle.Critical, Me.Text)
            txtLimit.Focus()
            Return False
        End If

        If wynik < 0 Then
            MsgBox("Wartość limitu nie moze być liczbą ujemną", MsgBoxStyle.Critical, Me.Text)
            txtLimit.Focus()
            Return False
        End If
        Dim ile_zaznaczono As Integer = 0
        For Each row As DataGridViewRow In dgv.Rows
            If CType(row.Cells("wybierz").Value, Boolean) Then
                ile_zaznaczono = ile_zaznaczono + 1
            End If
        Next
        If ile_zaznaczono = 0 Then
            MsgBox("Nie zaznaczono żadnej pozycji do zapisania.", MsgBoxStyle.Critical, Me.Text)
            Return False
        End If


        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        'ws.Url = frmGlowna.strWebservice
        Dim wsWynik As wsCursorProf.UserLimityZapiszWybranymWynik
        Dim xml As String = String.Empty

        For Each row As DataGridViewRow In dgv.Rows
            If CType(row.Cells("wybierz").Value, Boolean) Then
                xml = xml + String.Format("<row user_id=""{0}""/>", row.Cells("user_id").Value)
                'xml = xml + "<row user_id=""" + row.Cells("user_id") + """ />"
            End If
        Next

        'zapis zmian limitów na serwer
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.UserLimityZapiszWybranym(frmGlowna.sesja, xml, txtLimit.Text, rtxtKomentarz.Text)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem. " & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
            Return False
        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, Me.Text)
            Return False
        ElseIf wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, Me.Text)
        End If
        'odświeżamy dgv 
        wczytaj()
        'zerujemy zmienną "suma_limitow_po_zmianie"
        'suma_limitow_po_zmianie = 0

        'tslStatus.Text = "POMYŚLNIE ZAPISANO LIMITY."
        'timer_l.Interval = 3000 'komunikat zniknie po 3s
        'timer_l.Start()

        frmGlowna.lblStatus.Text = "Pomyślnie zapisano limity dla użytkowników "
        frmGlowna.timer.Interval = 3000 'komunikat zniknie po 3s
        frmGlowna.timer.Start()
        Return True
    End Function

    'Private Function wybierz(ByVal dgvCell) As Boolean
    '    If dgv.CurrentCell Is Nothing Then
    '        MsgBox("Najpierw wybierz magazyn.", MsgBoxStyle.Exclamation)
    '        Return False
    '    End If
    '    'intIdWybranegoMagazynu = dgv.Rows(dgv.CurrentCell.RowIndex).Cells("magazyn_id").Value
    '    'strNazwaWybranegoMagazynu = dgv.Rows(dgv.CurrentCell.RowIndex).Cells("nazwa").Value
    '    Return True
    'End Function

    Private Function wczytaj() As Boolean
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        'ws.Url = frmGlowna.strWebservice
        Dim wsWynik As wsCursorProf.UserLimityWczytajWybranychWynik

        Try
            Cursor = Cursors.WaitCursor
            wsWynik = ws.UserLimityWczytajWybranych(frmGlowna.sesja, txtFiltruj.Text)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
            Return False
        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, Me.Text)
            Return False
        ElseIf wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, Me.Text)
        End If
        If wsWynik.dane.Tables.Count > 0 Then
            dgv.DataSource = wsWynik.dane.Tables(0)
            If dgv.Columns.Contains("user_id") Then dgv.Columns("user_id").Visible = False
            dgv.Columns("wybierz").ReadOnly = False
            dgv.CurrentCell = Nothing
            For Each dgvColumn As DataGridViewColumn In dgv.Columns
                If dgvColumn.HeaderText.ToLower = "wybierz" Then
                    dgvColumn.ReadOnly = False
                Else
                    dgvColumn.ReadOnly = True
                End If


            Next
        Else
            MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał listy dostępnych list użytkowników ." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
            Me.Close()
        End If
        Return True

    End Function

    'Private Sub btnOdswiez_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOdswiez.Click
    '    wczytaj()
    'End Sub

    'Private Sub dgv_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellDoubleClick
    '    If wybierz(dgv.Rows(e.RowIndex).Cells(e.ColumnIndex)) Then Me.Close()
    'End Sub

    Public Sub odswiezListy()
        wczytaj()
    End Sub


    Private Sub btnFiltruj_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFiltruj.Click
        If Not wczytaj() Then Me.Close()
    End Sub

    Private Sub txtFiltruj_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtFiltruj.KeyDown
        If e.KeyCode = Keys.Enter Then
            wczytaj()
        End If
    End Sub


    Private Sub btnZapisz_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnZapisz.Click
        
        If zapisz_limit() = True Then
            txtLimit.Text = ""
            rtxtKomentarz.Text = ""
            MsgBox("Pomyślnie zapisano limity dla wybranych użytkowników.", MsgBoxStyle.Information, Me.Text)
        End If
    End Sub

    Private Sub btnZamknij_Click(sender As System.Object, e As System.EventArgs) Handles btnZamknij.Click
        Me.Close()
    End Sub

    Private Sub dgv_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellContentClick

    End Sub

    Private Sub btnFiltr_Click(sender As Object, e As System.EventArgs) Handles btnFiltr.Click
        If Not wczytaj() Then Me.Close()
    End Sub

    Private Function czyBylyZmiany() As Boolean
        Dim ile_zaznaczono As Integer = 0
        For Each row As DataGridViewRow In dgv.Rows
            If CType(row.Cells("wybierz").Value, Boolean) Then
                ile_zaznaczono = ile_zaznaczono + 1
            End If
        Next
        If ile_zaznaczono > 0 OrElse txtLimit.Text.Length > 0 OrElse rtxtKomentarz.Text.Length > 0 Then
            Return True
        End If

        Return False
    End Function
End Class