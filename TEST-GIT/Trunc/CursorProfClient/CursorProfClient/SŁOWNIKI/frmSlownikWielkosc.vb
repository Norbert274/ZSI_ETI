Public Class frmSlownikWielkosc
    Const CONST_WIELKOSC_ID = "USER_WIELKOSC_ID"
    Const CONST_NAZWA = "NAZWA"
    Const CONST_MSG_TITLE = "Wielkości użytkowników"
    Private dtDane As New DataTable
    Private intID As Integer = -1
    Private Nazwa As String = ""
    Private Sub frmSlownikWielkosc_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        wczytaj()
    End Sub

    Private Sub wczytaj()
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik As wsCursorProf.SlownikWielkoscOdczytajWynik
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.SlownikWielkoscOdczytaj(frmGlowna.sesja)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, CONST_MSG_TITLE)
            Exit Sub
        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, CONST_MSG_TITLE)
            Exit Sub
        ElseIf wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, CONST_MSG_TITLE)
        End If

        If wsWynik.dane.Tables.Count < 1 Then
            MsgBox("Błąd systemu - serwer nie zwrócił tabeli z wielkościami użytkowników" & frmGlowna.kontaktIt, MsgBoxStyle.Critical, CONST_MSG_TITLE)
            Exit Sub
        End If

        dtDane = wsWynik.dane.Tables(0)
        dgv.DataSource = dtDane
        If dgv.Columns.Contains(CONST_WIELKOSC_ID) Then dgv.Columns(CONST_WIELKOSC_ID).Visible = False
        'dgv.AutoResizeColumns()
        For Each col As DataGridViewColumn In dgv.Columns
            dgv.Columns(col.Index).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        Next

    End Sub

    Private Sub usun(ByVal id As Integer)
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik As wsCursorProf.SlownikwielkoscUsunWynik
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.SlownikWielkoscUsun(frmGlowna.sesja, id)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, "Usuwanie " + CONST_MSG_TITLE)
            Exit Sub
        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, "Usuwanie " + CONST_MSG_TITLE)
            Exit Sub
        ElseIf wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, "Usuwanie " + CONST_MSG_TITLE)
        Else
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Information, "Usuwanie " + CONST_MSG_TITLE)
        End If

    End Sub

    Private Sub edytuj(ByVal wielkosc_id As Integer, _
                                        ByVal nazwa_wielkosci As String)
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik As wsCursorProf.SlownikWielkoscEdytujWynik
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.SlownikWielkoscEdytuj(frmGlowna.sesja, wielkosc_id, nazwa_wielkosci)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, "Edycja " + CONST_MSG_TITLE)
            Exit Sub
        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, "Edycja " + CONST_MSG_TITLE)
            Exit Sub
        ElseIf wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, "Edycja " + CONST_MSG_TITLE)
        Else
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Information, "Edycja " + CONST_MSG_TITLE)
        End If

    End Sub

    Private Sub btnUsun_Click(sender As System.Object, e As System.EventArgs) Handles btnUsun.Click
        If Not dgv.CurrentCell Is Nothing Then
            intID = dgv.Rows(dgv.CurrentCell.RowIndex).Cells(CONST_WIELKOSC_ID).Value
            Nazwa = dgv.Rows(dgv.CurrentCell.RowIndex).Cells(CONST_NAZWA).Value
            If intID > -1 Then
                If MsgBox("Czy na pewno chcesz usunąć wielkość o nazwie: " & Nazwa, MsgBoxStyle.YesNo, "Usuwanie " + CONST_MSG_TITLE) = MsgBoxResult.Yes Then
                    usun(intID)
                    wczytaj()
                End If
            End If
        End If
    End Sub

    Private Sub dgv_CellClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellClick
        If e.RowIndex > -1 Then
            intID = dgv.Rows(e.RowIndex).Cells(CONST_WIELKOSC_ID).Value
            Nazwa = dgv.Rows(e.RowIndex).Cells(CONST_NAZWA).Value
        End If
    End Sub

    Private Sub btnNowy_Click(sender As System.Object, e As System.EventArgs) Handles btnNowy.Click
        Dim f As New frmSlownikWielkoscEdycja
        f.dtDane = dtDane
        If f.ShowDialog = Windows.Forms.DialogResult.OK Then
            edytuj(-1, f.txtNazwa.Text)
            wczytaj()
        End If

    End Sub

    Private Sub frmEdytuj_Click(sender As System.Object, e As System.EventArgs) Handles frmEdytuj.Click
        If Not dgv.CurrentCell Is Nothing Then
            intID = dgv.Rows(dgv.CurrentCell.RowIndex).Cells(CONST_WIELKOSC_ID).Value
            Nazwa = dgv.Rows(dgv.CurrentCell.RowIndex).Cells(CONST_NAZWA).Value
            If intID > -1 Then
                Dim f As New frmSlownikWielkoscEdycja
                f.dtDane = dtDane
                f.txtNazwa.Text = Nazwa
                If f.ShowDialog = Windows.Forms.DialogResult.OK Then
                    edytuj(intID, f.txtNazwa.Text)
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