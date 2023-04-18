Public Class frmSlownikSieciSprzedazy
    Private dtSieciSprzedazy As New DataTable
    Private intUserSiecSprzedazy As Integer = -1
    Private NazwaSieci As String = ""
    Private Sub frmSlownikSieciSprzedazy_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        wczytaj()
    End Sub

    Private Sub wczytaj()
       System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
System.Net.ServicePointManager.Expect100Continue = False
ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik As wsCursorProf.SlownikSieciSprzedazyOdczytajWynik
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.SlownikSieciSprzedazyOdczytaj(frmGlowna.sesja)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, "Odczyt sieci sprzedaży")
            Exit Sub
        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, "Odczyt sieci sprzedaży")
            Exit Sub
        ElseIf wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, "Odczyt sieci sprzedaży")
        End If

        If wsWynik.dane.Tables.Count < 1 Then
            MsgBox("Błąd systemu - serwer nie zwrócił tabeli z sieciami sprzedaży" & frmGlowna.kontaktIt, MsgBoxStyle.Critical, "Odczyt sieci sprzedaży")
            Exit Sub
        End If

        dtSieciSprzedazy = wsWynik.dane.Tables(0)
        dgv.DataSource = dtSieciSprzedazy
        If dgv.Columns.Contains("USER_SIEC_ID") Then dgv.Columns("USER_SIEC_ID").Visible = False
        'dgv.AutoResizeColumns()
        For Each col As DataGridViewColumn In dgv.Columns
            dgv.Columns(col.Index).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        Next
    End Sub

    Private Sub usun_siec_sprzedazy(ByVal siec_id As Integer)
       System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
System.Net.ServicePointManager.Expect100Continue = False
ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik As wsCursorProf.SlownikSiecSprzedazyUsunWynik
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.SlownikSiecSprzedazyUsun(frmGlowna.sesja, siec_id)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, "Usuwanie sieci sprzedaży")
            Exit Sub
        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, "Usuwanie sieci sprzedaży")
            Exit Sub
        ElseIf wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, "Usuwanie sieci sprzedaży")
        Else
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Information, "Usuwanie sieci sprzedaży")
        End If

    End Sub

    Private Sub edytuj_siec_sprzedazy(ByVal siec_id As Integer, _
                                        ByVal nazwa_sieci As String)
       System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
System.Net.ServicePointManager.Expect100Continue = False
ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik As wsCursorProf.SlownikSiecSprzedazyEdytujWynik
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.SlownikSiecSprzedazyEdytuj(frmGlowna.sesja, siec_id, nazwa_sieci)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, "Edycja sieci sprzedaży")
            Exit Sub
        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, "Edycja sieci sprzedaży")
            Exit Sub
        ElseIf wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, "Edycja sieci sprzedaży")
        Else
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Information, "Edycja sieci sprzedaży")
        End If

    End Sub

    Private Sub btnUsun_Click(sender As System.Object, e As System.EventArgs) Handles btnUsun.Click
        If Not dgv.CurrentCell Is Nothing Then
            intUserSiecSprzedazy = dgv.Rows(dgv.CurrentCell.RowIndex).Cells("USER_SIEC_ID").Value
            NazwaSieci = dgv.Rows(dgv.CurrentCell.RowIndex).Cells("NAZWA").Value
            If intUserSiecSprzedazy > -1 Then
                If MsgBox("Czy na pewno chcesz usunąć sieć sprzedaży o nazwie: " & NazwaSieci, MsgBoxStyle.YesNo, "Usuwanie sieci sprzedaży") = MsgBoxResult.Yes Then
                    usun_siec_sprzedazy(intUserSiecSprzedazy)
                    wczytaj()
                End If
            End If
        End If
    End Sub

    Private Sub dgv_CellClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellClick
        If e.RowIndex > -1 Then
            intUserSiecSprzedazy = dgv.Rows(e.RowIndex).Cells("USER_SIEC_ID").Value
            NazwaSieci = dgv.Rows(e.RowIndex).Cells("NAZWA").Value
        End If
    End Sub

    Private Sub btnNowy_Click(sender As System.Object, e As System.EventArgs) Handles btnNowy.Click
        Dim f As New frmSlownikSiecSprzedazyEdycja
        f.dtSieci = dtSieciSprzedazy
        If f.ShowDialog = Windows.Forms.DialogResult.OK Then
            edytuj_siec_sprzedazy(-1, f.txtNazwaSieci.Text)
            wczytaj()
        End If

    End Sub

    Private Sub frmEdytuj_Click(sender As System.Object, e As System.EventArgs) Handles frmEdytuj.Click
        If Not dgv.CurrentCell Is Nothing Then
            intUserSiecSprzedazy = dgv.Rows(dgv.CurrentCell.RowIndex).Cells("USER_SIEC_ID").Value
            NazwaSieci = dgv.Rows(dgv.CurrentCell.RowIndex).Cells("NAZWA").Value
            If intUserSiecSprzedazy > -1 Then
                Dim f As New frmSlownikSiecSprzedazyEdycja
                f.dtSieci = dtSieciSprzedazy
                f.txtNazwaSieci.Text = NazwaSieci
                If f.ShowDialog = Windows.Forms.DialogResult.OK Then
                    edytuj_siec_sprzedazy(intUserSiecSprzedazy, f.txtNazwaSieci.Text)
                    wczytaj()
                End If
            End If
        Else
            MsgBox("Nie wybrano sieci sprzedaży do edycji!", MsgBoxStyle.Exclamation, "Brak sieci sprzedaży do edycji")
        End If
    End Sub

    Private Sub btnZastosuj_Click(sender As System.Object, e As System.EventArgs) Handles btnZastosuj.Click
        wczytaj()
    End Sub

    Private Sub btnAnuluj_Click(sender As System.Object, e As System.EventArgs) Handles btnAnuluj.Click
        Me.Close()
    End Sub
End Class