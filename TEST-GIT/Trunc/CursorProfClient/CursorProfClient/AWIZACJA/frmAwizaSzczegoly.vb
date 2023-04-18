Public Class frmAwizaSzczegoly
    Public awizo_id As Integer

    Private Sub frmAwizaSzczegoly_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If Not wczytaj() Then

        End If
    End Sub

    Private Function wczytaj()
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        'ws.Url = frmGlowna.strWebservice
        Dim wsWynik As wsCursorProf.AwizaSzczegolyWczytajWynik


        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.AwizaSzczegolyWczytaj(frmGlowna.sesja, awizo_id)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, "Wczytanie szczegółów awiza")
            Me.Close()
            Return False
            Exit Function
        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, "Wczytanie szczegółów awiza")
            Return False
        End If
        If wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, "Wczytanie szczegółów awiza")
            Return False
        End If
        If wsWynik.status = 0 Then
            dgv.DataSource = wsWynik.dane.Tables(0)
          
            dgv.Columns("sku").Width = 120
            dgv.Columns("sku_nazwa").Width = 380
            dgv.Columns("klasa").Width = 50
            dgv.Columns("ilosc_awizowana").Width = 95
            dgv.Columns("ilosc_dostarczona").Width = 95
            dgv.Columns("sku").ReadOnly = True
            dgv.Columns("sku_nazwa").ReadOnly = True
            txtAdresDostawca.Text = wsWynik.dostawca_adres
            txtKodDostawca.Text = wsWynik.dostawca_kod
            txtKrajDostawca.Text = wsWynik.dostawca_kraj
            txtMiastoDostawca.Text = wsWynik.dostawca_miasto
            txtNazwaDostawca.Text = wsWynik.dostawca_nazwa
            dtpPlanowanaDataDostawy.Text = wsWynik.planowana_data_dostawy
            txtNrPO.Text = wsWynik.numer_PO
            txtOsobaKontaktowa.Text = wsWynik.osoba_kontaktowa
            txtTelefonKontaktowy.Text = wsWynik.telefon
            txtUwagi.Text = wsWynik.uwagi
            txtQGUAR_DOSTAWA.Text = wsWynik.qguar_dostawa
            txtQGUAR_ZA.Text = wsWynik.qguar_za
            txtStatusAwiza.Text = wsWynik.awizo_status
            txtIloscPalet.Text = wsWynik.ilosc_palet
            txtIloscPaczek.Text = wsWynik.ilosc_paczek
            txtTypDostawy.Text = wsWynik.qguar_delivery_kind
            Me.Text = "Szczegóły awiza nr " & CStr(awizo_id)
        End If
        Return True
    End Function

    Private Sub btnZamknij_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnZamknij.Click
        Me.Close()
    End Sub
End Class