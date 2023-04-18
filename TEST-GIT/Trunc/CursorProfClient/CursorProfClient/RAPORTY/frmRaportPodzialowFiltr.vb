Public Class frmRaportPodzialowFiltr
    Public frm As New frmRaportPodzialow

    Private Function wczytaj() As Boolean

        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        'ws.Url = frmGlowna.strWebservice
        Dim wsWynik As wsCursorProf.RaportPodzialyWczytajWynik

        'odczyt z serwera
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.RaportPodzialyWczytaj(frmGlowna.sesja, Date.Parse(dtpDataOd.Value.ToString("yyyy-MM-dd")), Date.Parse(dtpDataDo.Value.ToString("yyyy-MM-dd")))
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
            Return False
        End Try
        If wsWynik.status = -1 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, "Brak uprawnień")
            Return False
        End If
        'kontrola wyników
        If wsWynik.dane.Tables.Count > 0 Then
            If wsWynik.dane.Tables(0).Rows.Count < 1 Then
                MsgBox("Brak danych o podziałach z wybranego okresu.", MsgBoxStyle.Exclamation, Me.Text)
                Return False
            End If
        Else
            MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał tabeli z raportem podziałów." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
            Return False
            Exit Function
        End If
        frm.dtDane = wsWynik.dane.Tables(0)
        frm.dgv.DataSource = frm.dtDane
        frm.dgv.Columns(0).Visible = True
        For Each col As DataGridViewColumn In frm.dgv.Columns
            frm.dgv.Columns(col.Index).AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
            frm.dgv.Columns(col.Index).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        Next
        frm.dgv.AllowUserToAddRows = False
        frm.dgv.AllowUserToDeleteRows = False
        If wsWynik.status = -1 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation)
        End If
        Return True
    End Function


    Private Sub btnGenerujRaport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerujRaport.Click

        If Date.Parse(dtpDataOd.Value.ToString("yyyy-MM-dd")) > Date.Parse(dtpDataDo.Value.ToString("yyyy-MM-dd")) Then
            MessageBox.Show("'Data od' nie może być większa od 'Data do'!", "Błąd daty", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)
            dtpDataOd.Select()
            Exit Sub
        End If
        If Not wczytaj() Then
            Exit Sub
        Else
            frm.Text = "Raport podziałów towaru za okres od " & dtpDataOd.Text & " do " & dtpDataDo.Text
            frm.MdiParent = frmGlowna
            frm.Show()
            frm = New frmRaportPodzialow
        End If

    End Sub

    Private Sub frmRaportLimityFiltr_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

        dtpDataDo.Text = Now

    End Sub

    Private Sub btnAnuluj_Click(sender As System.Object, e As System.EventArgs) Handles btnAnuluj.Click
        Me.Close()
    End Sub
End Class