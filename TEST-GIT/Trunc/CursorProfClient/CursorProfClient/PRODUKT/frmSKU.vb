Public Class frmSKU

    Private Sub frmSKU_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        ctrSKU.dgvMultiSelect = True
        ctrSKU.frmRodzic = Me
        ctrSKU.zaznacz = False
        ctrSKU.ShowStan()
    End Sub

    Public Sub zamowienieGotoweGrupa()

        'zbieramy co zaznaczono
        Dim dtSku As New DataTable
        dtSku.Columns.Add("sku_id")
        dtSku.Columns.Add("sku")
        dtSku.Columns.Add("sku_nazwa")
        dtSku.Columns.Add("koszt punktowy")
        dtSku.Columns.Add("grupa_id")
        dtSku.Columns.Add("grupa")

        For Each dgvRow As DataGridViewRow In ctrSKU.dgv.Rows
            If dgvRow.Cells(0).Value Then dtSku.Rows.Add(dgvRow.Cells("sku_id").Value, dgvRow.Cells("sku").Value, dgvRow.Cells("sku_nazwa").Value, dgvRow.Cells("koszt punktowy").Value, dgvRow.Cells("grupa_id").Value, dgvRow.Cells("grupa").Value)
        Next
        If dtSku.Rows.Count < 1 Then
            MsgBox("Zaznacz przynajmniej jedną pozycję", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        'doczytujemy ilości dostępne z bazy
        Dim ds As New DataSet
        ds.Tables.Add(dtSku)
       System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
System.Net.ServicePointManager.Expect100Continue = False
ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik As wsCursorProf.StanSkuGrupaWynik
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.StanSkuGrupa(frmGlowna.sesja, ctrSKU.intIdMagazynu, ds)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical)
            Exit Sub
        End Try
        Cursor = Cursors.Default
        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical)
            Exit Sub
        ElseIf wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation)
        End If
        If wsWynik.dane.Tables.Count < 1 Then
            MsgBox("Błąd wewnętrzny systemu: serwer nie zwrócił listy dostępnych ilości dla wybranych sku." & frmGlowna.kontaktIt, MsgBoxStyle.Critical)
            Exit Sub
        End If

        'Dodajemy do koszyka sku, które użytkownik zaznaczył
        If frmGlowna.frmKoszyk Is Nothing Then
            MsgBox("Błąd wewnętrzny systemu. Koszyk jest zamknięty w funkcji zamowienieGotoweGrupa()." & frmGlowna.kontaktIt, MsgBoxStyle.Critical)
            Exit Sub
        End If
        'Dim frmKoszyk As frmZamowienie = frmGlowna.frmKoszyk
        'Dim bZnaleziono As Boolean
        'Dim bDodano As Boolean = False
        'Dim dtDostepne As DataTable = wsWynik.dane.Tables(0)
        'Dim dodanoLicznik As Integer = 0
        'For Each dtRow As DataRow In dtDostepne.Rows
        '    bZnaleziono = False
        '    For Each dgvRow As DataGridViewRow In frmKoszyk.dgv.Rows
        '        If dtRow("sku_id") = dgvRow.Cells("sku_id").Value And dtRow("grupa_id") = dgvRow.Cells("grupa_id").Value Then
        '            bZnaleziono = True
        '            Exit For
        '        End If
        '    Next
        '    If Not bZnaleziono Then
        '        frmKoszyk.dgv.DataSource.Rows.Add(dtRow("sku_id"), dtRow("sku"), dtRow("sku_nazwa"), 0, dtRow("ilosc_dostepna"), dtRow("Limit"), dtRow("CENA"), dtRow("grupa_id"), dtRow("grupa"))
        '        dodanoLicznik = dodanoLicznik + 1
        '        If Not bDodano Then
        '            'dodaliśmy pierwszą pozycję, ustawiamy na niej aktywną komórkę w koszyku
        '            For Each dgvRow As DataGridViewRow In frmKoszyk.dgv.Rows
        '                If dgvRow.Cells("sku_id").Value = dtRow("sku_id") And dtRow("grupa_id") = dgvRow.Cells("grupa_id").Value Then
        '                    frmKoszyk.dgv.CurrentCell = frmKoszyk.dgv.Rows(dgvRow.Index).Cells("ilosc")
        '                    frmKoszyk.dgv.BeginEdit(True)
        '                    Exit For
        '                End If
        '            Next
        '            bDodano = True
        '        End If
        '    End If
        'Next
       

        'odznaczamy wszystkie checkboxy
        For Each dgvRow As DataGridViewRow In ctrSKU.dgv.Rows
            If dgvRow.Cells(0).Value Then dgvRow.Cells(0).Value = False
        Next
    End Sub

    
End Class