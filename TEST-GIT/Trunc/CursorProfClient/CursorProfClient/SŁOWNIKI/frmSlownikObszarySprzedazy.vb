Public Class frmSlownikObszarySprzedazy
    Private dtObszarySprzedazy As New DataTable
    Private intUserObszarSprzedazy As Integer = -1
    Private NazwaObszaru As String = ""
    Private Sub frmSlownikObszarySprzedazy_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        wczytaj()
    End Sub

    Private Sub wczytaj()
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik As wsCursorProf.SlownikObszarySprzedazyOdczytajWynik
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.SlownikObszarySprzedazyOdczytaj(frmGlowna.sesja)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, "Odczyt obszarów sprzedaży")
            Exit Sub
        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, "Odczyt obszarów sprzedaży")
            Exit Sub
        ElseIf wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, "Odczyt obszarów sprzedaży")
        End If

        If wsWynik.dane.Tables.Count < 1 Then
            MsgBox("Błąd systemu - serwer nie zwrócił tabeli z obszarami sprzedaży" & frmGlowna.kontaktIt, MsgBoxStyle.Critical, "Odczyt obszarów sprzedaży")
            Exit Sub
        End If

        dtObszarySprzedazy = wsWynik.dane.Tables(0)
        dgv.DataSource = dtObszarySprzedazy
        If dgv.Columns.Contains("USER_OBSZAR_ID") Then dgv.Columns("USER_OBSZAR_ID").Visible = False
        'dgv.AutoResizeColumns()
        For Each col As DataGridViewColumn In dgv.Columns
            dgv.Columns(col.Index).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        Next

    End Sub

    Private Sub usun_obszar_sprzedazy(ByVal obszar_id As Integer)
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik As wsCursorProf.SlownikObszarSprzedazyUsunWynik
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.SlownikObszarSprzedazyUsun(frmGlowna.sesja, obszar_id)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, "Usuwanie obszaru sprzedaży")
            Exit Sub
        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, "Usuwanie obszaru sprzedaży")
            Exit Sub
        ElseIf wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, "Usuwanie obszaru sprzedaży")
        Else
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Information, "Usuwanie obszaru sprzedaży")
        End If

    End Sub

    Private Sub edytuj_obszar_sprzedazy(ByVal obszar_id As Integer, _
                                        ByVal nazwa_obszaru As String)
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik As wsCursorProf.SlownikObszarSprzedazyEdytujWynik
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.SlownikObszarSprzedazyEdytuj(frmGlowna.sesja, obszar_id, nazwa_obszaru)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, "Edycja obszaru sprzedaży")
            Exit Sub
        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, "Edycja obszaru sprzedaży")
            Exit Sub
        ElseIf wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, "Edycja obszaru sprzedaży")
        Else
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Information, "Edycja obszaru sprzedaży")
        End If

    End Sub

    Private Sub btnUsun_Click(sender As System.Object, e As System.EventArgs) Handles btnUsun.Click
        If Not dgv.CurrentCell Is Nothing Then
            intUserObszarSprzedazy = dgv.Rows(dgv.CurrentCell.RowIndex).Cells("USER_OBSZAR_ID").Value
            NazwaObszaru = dgv.Rows(dgv.CurrentCell.RowIndex).Cells("NAZWA").Value
            If intUserObszarSprzedazy > -1 Then
                If MsgBox("Czy na pewno chcesz usunąć obszar sprzedaży o nazwie: " & NazwaObszaru, MsgBoxStyle.YesNo, "Usuwanie obszaru sprzedaży") = MsgBoxResult.Yes Then
                    usun_obszar_sprzedazy(intUserObszarSprzedazy)
                    wczytaj()
                End If
            End If
        End If
    End Sub

    Private Sub dgv_CellClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellClick
        If e.RowIndex > -1 Then
            intUserObszarSprzedazy = dgv.Rows(e.RowIndex).Cells("USER_OBSZAR_ID").Value
            NazwaObszaru = dgv.Rows(e.RowIndex).Cells("NAZWA").Value
        End If
    End Sub

    Private Sub btnNowy_Click(sender As System.Object, e As System.EventArgs) Handles btnNowy.Click
        Dim f As New frmSlownikObszarSprzedazyEdycja
        f.dtObszary = dtObszarySprzedazy
        If f.ShowDialog = Windows.Forms.DialogResult.OK Then
            edytuj_obszar_sprzedazy(-1, f.txtNazwaObszaru.Text)
            wczytaj()
        End If

    End Sub

    Private Sub frmEdytuj_Click(sender As System.Object, e As System.EventArgs) Handles frmEdytuj.Click
        If Not dgv.CurrentCell Is Nothing Then
            intUserObszarSprzedazy = dgv.Rows(dgv.CurrentCell.RowIndex).Cells("USER_OBSZAR_ID").Value
            NazwaObszaru = dgv.Rows(dgv.CurrentCell.RowIndex).Cells("NAZWA").Value
            If intUserObszarSprzedazy > -1 Then
                Dim f As New frmSlownikObszarSprzedazyEdycja
                f.dtObszary = dtObszarySprzedazy
                f.txtNazwaObszaru.Text = NazwaObszaru
                If f.ShowDialog = Windows.Forms.DialogResult.OK Then
                    edytuj_obszar_sprzedazy(intUserObszarSprzedazy, f.txtNazwaObszaru.Text)
                    wczytaj()
                End If
            End If
        Else
            MsgBox("Nie wybrano obszaru sprzedaży do edycji!", MsgBoxStyle.Exclamation, "Brak obszaru sprzedaży do edycji")
        End If
    End Sub

    Private Sub btnZastosuj_Click(sender As System.Object, e As System.EventArgs) Handles btnZastosuj.Click
        wczytaj()
    End Sub

    Private Sub btnAnuluj_Click(sender As System.Object, e As System.EventArgs) Handles btnAnuluj.Click
        Me.Close()
    End Sub
End Class