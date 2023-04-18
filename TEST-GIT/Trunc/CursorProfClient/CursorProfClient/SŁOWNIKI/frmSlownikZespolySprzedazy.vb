Public Class frmSlownikZespolySprzedazy
    Private dtZespolySprzedazy As New DataTable
    Private intUserZespolSprzedazy As Integer = -1
    Private NazwaZespolu As String = ""
    Private Sub frmSlownikZespolySprzedazy_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        wczytaj()
    End Sub

    Private Sub wczytaj()
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik As wsCursorProf.SlownikZespolySprzedazyOdczytajWynik
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.SlownikZespolySprzedazyOdczytaj(frmGlowna.sesja)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, "Odczyt zespołów sprzedaży")
            Exit Sub
        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, "Odczyt zespołów sprzedaży")
            Exit Sub
        ElseIf wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, "Odczyt zespołów sprzedaży")
        End If

        If wsWynik.dane.Tables.Count < 1 Then
            MsgBox("Błąd systemu - serwer nie zwrócił tabeli z zespołami sprzedaży" & frmGlowna.kontaktIt, MsgBoxStyle.Critical, "Odczyt zespołów sprzedaży")
            Exit Sub
        End If

        dtZespolySprzedazy = wsWynik.dane.Tables(0)
        dgv.DataSource = dtZespolySprzedazy
        If dgv.Columns.Contains("USER_ZESPOL_ID") Then dgv.Columns("USER_ZESPOL_ID").Visible = False
        'dgv.AutoResizeColumns()
        For Each col As DataGridViewColumn In dgv.Columns
            dgv.Columns(col.Index).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        Next
    End Sub

    Private Sub usun_zespol_sprzedazy(ByVal zespol_id As Integer)
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik As wsCursorProf.SlownikZespolSprzedazyUsunWynik
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.SlownikZespolSprzedazyUsun(frmGlowna.sesja, zespol_id)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, "Usuwanie zespołu sprzedaży")
            Exit Sub
        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, "Usuwanie zespołu sprzedaży")
            Exit Sub
        ElseIf wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, "Usuwanie zespołu sprzedaży")
        Else
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Information, "Usuwanie zespołu sprzedaży")
        End If

    End Sub

    Private Sub edytuj_zespol_sprzedazy(ByVal zespol_id As Integer, _
                                        ByVal nazwa_zespolu As String)
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik As wsCursorProf.SlownikZespolSprzedazyEdytujWynik
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.SlownikZespolSprzedazyEdytuj(frmGlowna.sesja, zespol_id, nazwa_zespolu)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, "Edycja zespołu sprzedaży")
            Exit Sub
        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, "Edycja zespołu sprzedaży")
            Exit Sub
        ElseIf wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, "Edycja zespołu sprzedaży")
        Else
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Information, "Edycja zespołu sprzedaży")
        End If

    End Sub

    Private Sub btnUsun_Click(sender As System.Object, e As System.EventArgs) Handles btnUsun.Click
        If Not dgv.CurrentCell Is Nothing Then
            intUserZespolSprzedazy = dgv.Rows(dgv.CurrentCell.RowIndex).Cells("USER_ZESPOL_ID").Value
            NazwaZespolu = dgv.Rows(dgv.CurrentCell.RowIndex).Cells("NAZWA").Value
            If intUserZespolSprzedazy > -1 Then
                If MsgBox("Czy na pewno chcesz usunąć zespół sprzedaży o nazwie: " & NazwaZespolu, MsgBoxStyle.YesNo, "Usuwanie zespołu sprzedaży") = MsgBoxResult.Yes Then
                    usun_zespol_sprzedazy(intUserZespolSprzedazy)
                    wczytaj()
                End If
            End If
        End If
    End Sub

    Private Sub dgv_CellClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellClick
        If e.RowIndex > -1 Then
            intUserZespolSprzedazy = dgv.Rows(e.RowIndex).Cells("USER_ZESPOL_ID").Value
            NazwaZespolu = dgv.Rows(e.RowIndex).Cells("NAZWA").Value
        End If
    End Sub

    Private Sub btnNowy_Click(sender As System.Object, e As System.EventArgs) Handles btnNowy.Click
        Dim f As New frmSlownikZespolSprzedazyEdycja
        f.dtZespoly = dtZespolySprzedazy
        If f.ShowDialog = Windows.Forms.DialogResult.OK Then
            edytuj_zespol_sprzedazy(-1, f.txtNazwaZespolu.Text)
            wczytaj()
        End If

    End Sub

    Private Sub frmEdytuj_Click(sender As System.Object, e As System.EventArgs) Handles frmEdytuj.Click
        If Not dgv.CurrentCell Is Nothing Then
            intUserZespolSprzedazy = dgv.Rows(dgv.CurrentCell.RowIndex).Cells("USER_ZESPOL_ID").Value
            NazwaZespolu = dgv.Rows(dgv.CurrentCell.RowIndex).Cells("NAZWA").Value
            If intUserZespolSprzedazy > -1 Then
                Dim f As New frmSlownikZespolSprzedazyEdycja
                f.dtZespoly = dtZespolySprzedazy
                f.txtNazwaZespolu.Text = NazwaZespolu
                If f.ShowDialog = Windows.Forms.DialogResult.OK Then
                    edytuj_zespol_sprzedazy(intUserZespolSprzedazy, f.txtNazwaZespolu.Text)
                    wczytaj()
                End If
            End If
        Else
            MsgBox("Nie wybrano zespołu sprzedaży do edycji!", MsgBoxStyle.Exclamation, "Brak zespołu sprzedaży do edycji")
        End If
    End Sub

    Private Sub btnZastosuj_Click(sender As System.Object, e As System.EventArgs) Handles btnZastosuj.Click
        wczytaj()
    End Sub

    Private Sub btnAnuluj_Click(sender As System.Object, e As System.EventArgs) Handles btnAnuluj.Click
        Me.Close()
    End Sub

End Class