Public Class frmRaportZamowienFiltr
    Public bWersjaZDanymiUzytkownika As Boolean = False
    Public bWersjaZPozycjami As Boolean = False
    Public frm As New frmRaportZamowienia

    Private Function wczytaj() As Boolean

        If bWersjaZDanymiUzytkownika = True Then
            System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
            System.Net.ServicePointManager.Expect100Continue = False
            ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
            ws.Proxy.Credentials = CredentialCache.DefaultCredentials
            Dim wsWynik As wsCursorProf.RaportZamowieniaDaneUzytkownikaWynik

            'odczyt z serwera
            Try
                Cursor = Cursors.WaitCursor
                Application.DoEvents()
                wsWynik = ws.RaportZamowieniaDaneUzytkownika(frmGlowna.sesja, Date.Parse(dtpDataOd.Value.ToString("yyyy-MM-dd")), Date.Parse(dtpDataDo.Value.ToString("yyyy-MM-dd")))
                Cursor = Cursors.Default
            Catch ex As Exception
                Cursor = Cursors.Default
                MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
                Return False
            End Try
            If wsWynik.status = -1 Then
                MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, Me.Text)
                Return False
            End If
            'kontrola wyników
            If wsWynik.dane.Tables.Count > 0 Then
                If wsWynik.dane.Tables(0).Rows.Count < 1 Then
                    MsgBox("Brak danych o zamówieniach z wybranego okresu.", MsgBoxStyle.Exclamation, Me.Text)
                    Return False
                End If
            Else
                MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał tabeli z raportem zamówień." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
                Return False
                Exit Function
            End If
            'frm.data_od = dtpDataOd.Text
            'frm.data_do = dtpDataDo.Text
            frm.dtZamowienia = wsWynik.dane.Tables(0)
            frm.dgvZamowienia.DataSource = frm.dtZamowienia
            frm.dgvZamowienia.Columns(0).Visible = True
            For Each col As DataGridViewColumn In frm.dgvZamowienia.Columns
                frm.dgvZamowienia.Columns(col.Index).AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
                frm.dgvZamowienia.Columns(col.Index).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            Next
            frm.dgvZamowienia.AllowUserToAddRows = False
            frm.dgvZamowienia.AllowUserToDeleteRows = False
            If wsWynik.status = -1 Then
                MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation)
            End If

        ElseIf bWersjaZPozycjami = True Then
            System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
            System.Net.ServicePointManager.Expect100Continue = False
            ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
            ws.Proxy.Credentials = CredentialCache.DefaultCredentials
            'ws.Url = frmGlowna.strWebservice
            Dim wsWynik As wsCursorProf.RaportZamowieniaPozycjeWczytajWynik

            'odczyt z serwera
            Try
                Cursor = Cursors.WaitCursor
                Application.DoEvents()
                wsWynik = ws.RaportZamowieniaPozycjeWczytaj(frmGlowna.sesja, DateTime.Parse(dtpDataOd.Value.ToString("yyyy-MM-dd") & " 00:00:00.001"), DateTime.Parse(dtpDataDo.Value.ToString("yyyy-MM-dd") + " 23:59:59.999"))
                Cursor = Cursors.Default
            Catch ex As Exception
                Cursor = Cursors.Default
                MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
                Return False
            End Try
            If wsWynik.status = -1 Then
                MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, Me.Text)
                Return False
            End If
            'kontrola wyników
            If wsWynik.dane.Tables.Count > 0 Then
                If wsWynik.dane.Tables(0).Rows.Count < 1 Then
                    MsgBox("Brak danych o zamówieniach z wybranego okresu.", MsgBoxStyle.Exclamation, Me.Text)
                    Return False
                End If
            Else
                MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał tabeli z raportem zamówień." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
                Return False
                Exit Function
            End If
            'frm.data_od = dtpDataOd.Text
            'frm.data_do = dtpDataDo.Text
            frm.dtZamowienia = wsWynik.dane.Tables(0)
            frm.dgvZamowienia.DataSource = frm.dtZamowienia
            frm.dgvZamowienia.Columns(0).Visible = True
            For Each col As DataGridViewColumn In frm.dgvZamowienia.Columns
                frm.dgvZamowienia.Columns(col.Index).AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
                frm.dgvZamowienia.Columns(col.Index).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            Next
            frm.dgvZamowienia.AllowUserToAddRows = False
            frm.dgvZamowienia.AllowUserToDeleteRows = False
            If wsWynik.status = -1 Then
                MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation)
            End If
        Else
            System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
            System.Net.ServicePointManager.Expect100Continue = False
            ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
            ws.Proxy.Credentials = CredentialCache.DefaultCredentials
            'ws.Url = frmGlowna.strWebservice
            Dim wsWynik As wsCursorProf.RaportZamowieniaWczytajWynik

            'odczyt z serwera
            Try
                Cursor = Cursors.WaitCursor
                Application.DoEvents()
                wsWynik = ws.RaportZamowieniaWczytaj(frmGlowna.sesja, Date.Parse(dtpDataOd.Value.ToString("yyyy-MM-dd")), Date.Parse(dtpDataDo.Value.ToString("yyyy-MM-dd")))
                Cursor = Cursors.Default
            Catch ex As Exception
                Cursor = Cursors.Default
                MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
                Return False
            End Try
            If wsWynik.status = -1 Then
                MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, Me.Text)
                Return False
            End If
            'kontrola wyników
            If wsWynik.dane.Tables.Count > 0 Then
                If wsWynik.dane.Tables(0).Rows.Count < 1 Then
                    MsgBox("Brak danych o zamówieniach z wybranego okresu.", MsgBoxStyle.Exclamation, Me.Text)
                    Return False
                End If
            Else
                MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał tabeli z raportem zamówień." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
                Return False
                Exit Function
            End If
            'frm.data_od = dtpDataOd.Text
            'frm.data_do = dtpDataDo.Text
            frm.dtZamowienia = wsWynik.dane.Tables(0)
            frm.dgvZamowienia.DataSource = frm.dtZamowienia
            frm.dgvZamowienia.Columns(0).Visible = True
            For Each col As DataGridViewColumn In frm.dgvZamowienia.Columns
                frm.dgvZamowienia.Columns(col.Index).AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
                frm.dgvZamowienia.Columns(col.Index).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            Next
            frm.dgvZamowienia.AllowUserToAddRows = False
            frm.dgvZamowienia.AllowUserToDeleteRows = False
            If wsWynik.status = -1 Then
                MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, Me.Text)
            End If
        End If

        Return True
    End Function


    Private Sub btnGenerujRaport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerujRaport.Click

        If Date.Parse(dtpDataOd.Value.ToString("yyyy-MM-dd")) > Date.Parse(dtpDataDo.Value.ToString("yyyy-MM-dd")) Then
            MessageBox.Show("'Data od' nie może być większa od 'Data do'!", "Błąd daty", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)
            Exit Sub
        End If
        If Not wczytaj() Then
            Exit Sub
        Else
            If bWersjaZDanymiUzytkownika = True Then
                frm.Text = "Raport zamówień z danymi użytkownika za okres od " & dtpDataOd.Text & " do " & dtpDataDo.Text
            ElseIf bWersjaZPozycjami = True Then
                frm.Text = "Raport zamówień z pozycjami za okres od " & dtpDataOd.Text & " do " & dtpDataDo.Text
            Else
                frm.Text = "Raport zamówień za okres od " & dtpDataOd.Text & " do " & dtpDataDo.Text
            End If

            frm.MdiParent = frmGlowna
            frm.Show()
            frm = New frmRaportZamowienia
        End If

    End Sub

    Private Sub frmRaportZamowienFiltr_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        dtpDataDo.Text = Now

        If bWersjaZDanymiUzytkownika = True Then
            Me.Text = "Raport zamówień z danymi użytkownika"
        ElseIf bWersjaZPozycjami = True Then
            Me.Text = "Raport zamówień z pozycjami"
        Else
            Me.Text = "Raport zamówień"
        End If
    End Sub

    Private Sub btnAnuluj_Click(sender As System.Object, e As System.EventArgs) Handles btnAnuluj.Click
        Me.Close()
    End Sub
End Class