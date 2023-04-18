Public Class frmZwrotyBezZamowien
    Private dtZwrotyBezZamowien As DataTable
    Private dtZamowienia As DataTable
    Private dtZamowieniaPozycje As DataTable
    Private dv As DataView
    Private wczytuje As Boolean = False

    Private Sub frmZwrotyBezZamowien_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If Not wczytaj() Then
            MsgBox("Nie znaleziono zwrotów bez zamówień.", MsgBoxStyle.Exclamation, "")
            Exit Sub
        End If
    End Sub

    Private Function wczytaj() As Boolean
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        'ws.Url = frmGlowna.strWebservice
        Dim wsWynik As wsCursorProf.ZwrotyBezZamowienWczytajWynik

        'odczyt z serwera
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.ZwrotyBezZamowienWczytaj(frmGlowna.sesja)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical)
            Return False
        End Try
        If wsWynik.status = -1 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical)
            Return False
        End If
        wczytuje = True
        dtZwrotyBezZamowien = wsWynik.dane.Tables(0).Copy()
        cmbDostawa.DisplayMember = "DOSTAWA_Q"
        cmbDostawa.ValueMember = "ZWROT_ID"
        cmbDostawa.DataSource = dtZwrotyBezZamowien
        cmbDostawa.SelectedIndex = -1
        wczytuje = False
        cmbZamowienie.Enabled = False


        If wsWynik.status = -1 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation)
        End If

        Return True
    End Function

    Private Function wczytajZamowienia(ByVal zwrot_id As Integer) As Boolean
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        'ws.Url = frmGlowna.strWebservice
        Dim wsWynik As wsCursorProf.ZwrotyBezZamowienWczytajZamowieniaWynik

        'odczyt z serwera
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.ZwrotyBezZamowienWczytajZamowienia(frmGlowna.sesja, zwrot_id)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical)
            Return False
        End Try
        If wsWynik.status = -1 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical)
            Return False
        End If
        If wsWynik.status = 1 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Information)
            Return True
        End If
        If wsWynik.dane.Tables.Count > 1 Then
            dtZamowieniaPozycje = wsWynik.dane.Tables(1).Copy()
            dv = New DataView(dtZamowieniaPozycje)
            dv.RowFilter = "1=0"
            dgv.DataSource = dv
            If dgv.Columns.Contains("Numer") Then
                dgv.Columns("Numer").Visible = False
            End If
        Else
            MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał listy pozycji zamówień." & frmGlowna.kontaktIt, MsgBoxStyle.Critical)
            Return False
        End If
        If wsWynik.dane.Tables.Count > 0 Then
            dtZamowienia = wsWynik.dane.Tables(0).Copy()
            cmbZamowienie.Enabled = True

            cmbZamowienie.DisplayMember = "Numer"
            cmbZamowienie.ValueMember = "Numer"
            cmbZamowienie.DataSource = dtZamowienia
            If dtZamowienia.Rows.Count > 0 Then
                cmbZamowienie.SelectedIndex = 0
            Else
                cmbZamowienie.SelectedIndex = -1
            End If
        Else
            MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał listy zamówień." & frmGlowna.kontaktIt, MsgBoxStyle.Critical)
            Return False
        End If
        Return True
    End Function

    Private Sub cmbDostawa_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbDostawa.SelectedIndexChanged
        If cmbDostawa.SelectedIndex > -1 And wczytuje = False Then
            If wczytajZamowienia(CInt(cmbDostawa.SelectedValue)) = False Then
                Me.Close()
            End If
        End If
    End Sub

    Private Sub cmbZamowienie_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbZamowienie.SelectedIndexChanged
        If cmbZamowienie.SelectedIndex > -1 Then
            Dim dr As DataRow
            dr = dtZamowienia.Select(String.Format("Numer = {0}", cmbZamowienie.SelectedValue))(0)
            lblDataRealizacji.Text = dr.Item("Data")
            lblZamowil.Text = dr.Item("User")
            lblTypZamowienia.Text = dr.Item("Typ")
            dv.RowFilter = String.Format("Numer = {0}", cmbZamowienie.SelectedValue)
            btnPrzypisz.Enabled = True
        Else
            dv.RowFilter = "1=0"
        End If
    End Sub

    'Private Sub cmbDostawa_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbDostawa.SelectedIndexChanged
    '    If Not wczytaj() Then
    '       System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
    'System.Net.ServicePointManager.Expect100Continue = False
    'ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
    'ws.Proxy.Credentials = CredentialCache.DefaultCredentials
    '        'ws.Url = frmGlowna.strWebservice
    '        Dim wsWynik As wsCursorProf.ZwrotyBezZamowienWczytajZamowieniaWynik

    '        'odczyt z serwera
    '        Try
    '            Cursor = Cursors.WaitCursor
    '            Application.DoEvents()
    '            wsWynik = ws.ZwrotyBezZamowienWczytajZamowienia(frmGlowna.sesja, cmbDostawa.SelectedValue)
    '            Cursor = Cursors.Default
    '        Catch ex As Exception
    '            Cursor = Cursors.Default
    '            MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical)
    '        End Try
    '        If wsWynik.status = -1 Then
    '            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical)
    '        End If
    '        If wsWynik.status = 1 Then
    '            MsgBox(wsWynik.status_opis, MsgBoxStyle.Information)
    '        End If

    '    End If
    'End Sub

    Private Sub btnZamknij_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnZamknij.Click
        Me.Close()
    End Sub

    Private Sub btnPrzypisz_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrzypisz.Click

    End Sub
End Class