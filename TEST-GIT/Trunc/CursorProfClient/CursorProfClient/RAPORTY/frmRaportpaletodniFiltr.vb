Public Class frmRaportPaletodniFiltr
    Public frm As New frmRaportPaletodni

    Private Function wczytaj() As Boolean

        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik As wsCursorProf.RaportPaletodniWczytajWynik

            'odczyt z serwera
            Try
                Cursor = Cursors.WaitCursor
                Application.DoEvents()
            wsWynik = ws.RaportPaletodniWczytaj(frmGlowna.sesja, DateTime.Parse(dtpDataOd.Value.ToString("yyyy-MM-dd") & " 00:00:00.001"), DateTime.Parse(dtpDataDo.Value.ToString("yyyy-MM-dd") + " 23:59:59.999"))
                Cursor = Cursors.Default
            Catch ex As Exception
                Cursor = Cursors.Default
                MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical)
                Return False
            End Try
            If wsWynik.status = -1 Then
                MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, "Brak uprawnień")
                Return False
            End If
            'kontrola wyników
            If wsWynik.dane.Tables.Count > 0 Then
                If wsWynik.dane.Tables(0).Rows.Count < 1 Then
                MsgBox("Brak danych o paletodniach z wybranego okresu.", MsgBoxStyle.Exclamation, Me.Text)
                    Return False
                End If
            Else
            MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał tabeli z raportem paletodni." & frmGlowna.kontaktIt, MsgBoxStyle.Critical)
                Return False
                Exit Function
            End If
            'frm.data_od = dtpDataOd.Text
            'frm.data_do = dtpDataDo.Text
            frm.dtPaletodni = wsWynik.dane.Tables(0)
            frm.dgvPaletodni.DataSource = frm.dtPaletodni
            frm.dgvPaletodni.Columns(0).Visible = True
            For Each col As DataGridViewColumn In frm.dgvPaletodni.Columns
                frm.dgvPaletodni.Columns(col.Index).AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
                frm.dgvPaletodni.Columns(col.Index).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            Next
            frm.dgvPaletodni.AllowUserToAddRows = False
            frm.dgvPaletodni.AllowUserToDeleteRows = False
            If wsWynik.status = -1 Then
                MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation)
            End If
        

        Return True
    End Function


    Private Sub frmRaportPaletodni_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Dim today As DateTime = Now
        dtpDataDo.Value = today.AddDays(-1)
        dtpDataOd.Value = today.AddDays(-1)
    End Sub

    Private Sub btnGenerujRaport_Click1(sender As Object, e As System.EventArgs) Handles btnGenerujRaport.Click
        If CDate(dtpDataOd.Value) > CDate(dtpDataDo.Value) Then
            MessageBox.Show("'Data od' nie może być większa od 'Data do'!", "Błąd daty", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)
            Exit Sub
        End If
        If Not wczytaj() Then
            Exit Sub
        Else

            frm.Text = "Raport paletodni za okres od " & dtpDataOd.Text & " do " & dtpDataDo.Text


            frm.MdiParent = frmGlowna
            frm.Show()
            frm = New frmRaportPaletodni
        End If
    End Sub

    Private Sub btnAnuluj_Click(sender As Object, e As System.EventArgs) Handles btnAnuluj.Click
        Me.Close()
    End Sub
End Class