Imports System.Text

Public Class frmStan
    Public bStanDlaKoszykINV As Boolean = False
    Private Sub frmStan_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        ctr.dgvMultiSelect = True
        ctr.frmRodzic = Me
        ctr.zaznacz = False
        ctr.bStanDlaKoszykINV = bStanDlaKoszykINV
        ctr.ShowStan()
    End Sub

    Public Sub sprawdz_czy_byl_mouse_move(ByRef b As Boolean)
        ctr.czy_otwarta_Galeria(b)
    End Sub

    Public Sub zamowienieGotoweGrupa()

        'zbieramy co zaznaczono
        'Dim dtSku As New DataTable
        'dtSku.Columns.Add("sku_id")
        'dtSku.Columns.Add("sku")
        'dtSku.Columns.Add("sku_nazwa")
        'dtSku.Columns.Add("J.M.")
        'dtSku.Columns.Add("koszt punktowy")
        'dtSku.Columns.Add("grupa_id")
        'dtSku.Columns.Add("grupa")

        'For Each dgvRow As DataGridViewRow In ctr.dgv.Rows
        '    If dgvRow.Cells(0).Value Then dtSku.Rows.Add(dgvRow.Cells("sku_id").Value, dgvRow.Cells("sku").Value, dgvRow.Cells("nazwa").Value, dgvRow.Cells("J.M.").Value, dgvRow.Cells("koszt punktowy").Value, dgvRow.Cells("grupa_id").Value, dgvRow.Cells("grupa").Value)
        'Next

        'Nowy zapis
        Dim dtSku As DataTable
        dtSku = ctr.dtSelectedRows.Copy

        If dtSku.Rows.Count < 1 Then
            MsgBox("Zaznacz przynajmniej jedną pozycję", MsgBoxStyle.Exclamation, "Zaznaczenie pozycji")
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
            wsWynik = ws.StanSkuGrupa(frmGlowna.sesja, ctr.intIdMagazynu, ds)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, "Sprawdzenie dostępności towaru")
            Exit Sub
        End Try
        Cursor = Cursors.Default
        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, "Sprawdzenie dostępności towaru")
            Exit Sub
        ElseIf wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, "Sprawdzenie dostępności towaru")
        End If
        If wsWynik.dane.Tables.Count < 1 Then
            MsgBox("Błąd wewnętrzny systemu: serwer nie zwrócił listy dostępnych ilości dla wybranych sku." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, "Sprawdzenie dostępności towaru")
            Exit Sub
        End If

        'Dodajemy do koszyka sku, które użytkownik zaznaczył
        If frmGlowna.frmKoszyk Is Nothing Then
            MsgBox("Błąd wewnętrzny systemu. Koszyk jest zamknięty w funkcji zamowienieGotoweGrupa()." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, "Błąd wewnętrzny systemu")
            Exit Sub
        End If
        Dim frmKoszyk As frmZamowienie = frmGlowna.frmKoszyk
        Dim bZnaleziono As Boolean
        Dim bDodano As Boolean = False
        Dim dtDostepne As DataTable = wsWynik.dane.Tables(0)
        Dim dodanoLicznik As Integer = 0
        For Each dtRow As DataRow In dtDostepne.Rows
            bZnaleziono = False
            For Each dgvRow As DataGridViewRow In frmKoszyk.dgv.Rows
                If dtRow("sku_id") = dgvRow.Cells("sku_id").Value And dtRow("grupa_id") = dgvRow.Cells("grupa_id").Value Then
                    bZnaleziono = True
                    Exit For
                End If
            Next
            If Not bZnaleziono Then
                frmKoszyk.dgv.DataSource.Rows.Add(dtRow("sku_id"), dtRow("sku"), dtRow("sku_nazwa"), 0, dtRow("ilosc_dostepna"), dtRow("J.M."), dtRow("Limit"), dtRow("koszt punktowy"), dtRow("grupa_id"), dtRow("grupa"), dtRow("sztuk_w_opakowaniu"))
                dodanoLicznik = dodanoLicznik + 1
                If Not bDodano Then
                    'dodaliśmy pierwszą pozycję, ustawiamy na niej aktywną komórkę w koszyku
                    For Each dgvRow As DataGridViewRow In frmKoszyk.dgv.Rows
                        If dgvRow.Cells("sku_id").Value = dtRow("sku_id") And dtRow("grupa_id") = dgvRow.Cells("grupa_id").Value Then
                            frmKoszyk.dgv.CurrentCell = frmKoszyk.dgv.Rows(dgvRow.Index).Cells("ilosc")
                            frmKoszyk.dgv.BeginEdit(True)
                            Exit For
                        End If
                    Next
                    bDodano = True
                End If
            End If
        Next
        If bDodano Then
            'Ustawianie cmbo magazyn na wartosc z stanu 
            'ctr.cmbMagazyn.ComboBox.SelectedValue
            If frmKoszyk.intIdMagazynu <> ctr.idMagazyn Then
                'Jesli istniały jakies pozycje wczesniej dla innego magazynu to wyrzucam bład i zamykam
                If frmKoszyk.dgv.Rows.Count > dodanoLicznik Then
                    MsgBox("W jednym zamówieniu nie można zamawiać towarów z różnych magazynów", MsgBoxStyle.Critical, "Błąd w zamówieniu")
                    frmKoszyk.BlokujZapisz = True
                    frmKoszyk.Close()
                    frmKoszyk.BlokujZapisz = False
                    Exit Sub
                Else
                    'ctr.cmbMagazyn.ComboBox.SelectedValue
                    frmKoszyk.intIdMagazynu = ctr.idMagazyn
                    'frmKoszyk.cmbZamowienieZMagazynu.ComboBox.SelectedValue = ctr.cmbMagazyn.ComboBox.SelectedValue
                    frmKoszyk.idMagazyn = ctr.idMagazyn
                End If
            End If
            frmKoszyk.dgv.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells)
            frmKoszyk.btnZapiszZmiany.Enabled = True
        Else
            If dtDostepne.Rows.Count > 0 Then
                MsgBox("Wszystkie zaznaczone SKU masz już w swoim koszyku lub próbujesz dodać produkt, którego ilość dostępna jest równa 0!", MsgBoxStyle.Exclamation, "Komunikat")
            Else
                MsgBox("Nie można dodać do koszyka żadnego z zaznaczonych produktów ponieważ ich ilość dostępna jest równa 0!", MsgBoxStyle.Exclamation, "Komunikat")
            End If
        End If
        frmKoszyk.WindowState = FormWindowState.Normal
        frmKoszyk.Activate()

        'odznaczamy wszystkie checkboxy
        For Each dgvRow As DataGridViewRow In ctr.dgv.Rows
            If dgvRow.Cells(0).Value Then dgvRow.Cells(0).Value = False
        Next
        ctr.odznaczWszystkie()
    End Sub
Public Sub zamowienieINVGotoweGrupa()

        'zbieramy co zaznaczono
        'Dim dtSku As New DataTable
        'dtSku.Columns.Add("sku_id")
        'dtSku.Columns.Add("sku")
        'dtSku.Columns.Add("sku_nazwa")
        'dtSku.Columns.Add("J.M.")
        'dtSku.Columns.Add("koszt punktowy")
        'dtSku.Columns.Add("grupa_id")
        'dtSku.Columns.Add("grupa")

		 'Nowy zapis
        Dim dtSku As DataTable
        dtSku = ctr.dtSelectedRows.Copy

        Dim strXml As New StringBuilder

        For Each dRow As DataRow In dtSku.Rows
            strXml.Append("<row sku_id=""" & dRow.Item("sku_id") & """ grupa_id=""" & dRow.Item("grupa_id") & """/>")
           
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
        Dim wsWynik As New wsCursorProf.StanSkuGrupaINVWynik

        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.StanSkuGrupaINV(frmGlowna.sesja, ctr.intIdMagazynu, strXml.ToString(), "")
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
        If frmGlowna.frmKoszykINV Is Nothing Then
            MsgBox("Błąd wewnętrzny systemu. Koszyk jest zamknięty w funkcji zamowienieGotoweGrupa()." & frmGlowna.kontaktIt, MsgBoxStyle.Critical)
            Exit Sub
        End If
        Dim frmKoszyk As frmZamowienieINV = frmGlowna.frmKoszykINV
        Dim bZnaleziono As Boolean
        Dim bDodano As Boolean = False
        Dim dtDostepne As DataTable = wsWynik.dane.Tables(0)
        Dim dodanoLicznik As Integer = 0
        For Each dtRow As DataRow In dtDostepne.Rows
            bZnaleziono = False
            For Each dgvRow As DataGridViewRow In frmKoszyk.dgv.Rows
                If dtRow("sku_id") = dgvRow.Cells("sku_id").Value And dtRow("grupa_id") = dgvRow.Cells("grupa_id").Value Then
                    bZnaleziono = True
                    Exit For
                End If
            Next
            If Not bZnaleziono Then
                frmKoszyk.dgv.DataSource.Rows.Add(dtRow("sku_id"), dtRow("sku"), dtRow("sku_nazwa"), 0, dtRow("ilosc_dostepna"), dtRow("grupa_id"), dtRow("grupa"))
                dodanoLicznik = dodanoLicznik + 1
                If Not bDodano Then
                    'dodaliśmy pierwszą pozycję, ustawiamy na niej aktywną komórkę w koszyku
                    For Each dgvRow As DataGridViewRow In frmKoszyk.dgv.Rows
                        If dgvRow.Cells("sku_id").Value = dtRow("sku_id") And dtRow("grupa_id") = dgvRow.Cells("grupa_id").Value Then
                            frmKoszyk.dgv.CurrentCell = frmKoszyk.dgv.Rows(dgvRow.Index).Cells("ilosc")
                            frmKoszyk.dgv.BeginEdit(True)
                            Exit For
                        End If
                    Next
                    bDodano = True
                End If
            End If
        Next
        If bDodano Then
            'Ustawianie cmbo magazyn na wartosc z stanu 
            'ctr.cmbMagazyn.ComboBox.SelectedValue
            If frmKoszyk.intIdMagazynu <> ctr.idMagazyn Then
                'Jesli istniały jakies pozycje wczesniej dla innego magazynu to wyrzucam bład i zamykam
                If frmKoszyk.dgv.Rows.Count > dodanoLicznik Then
                    MsgBox("W jednym zamówieniu nie można zamawiać towarów z różnych magazynów", MsgBoxStyle.Critical)
                    frmKoszyk.BlokujZapisz = True
                    frmKoszyk.Close()
                    frmKoszyk.BlokujZapisz = False
                    Exit Sub
                Else
                    'ctr.cmbMagazyn.ComboBox.SelectedValue
                    frmKoszyk.intIdMagazynu = ctr.idMagazyn
                    'frmKoszyk.cmbZamowienieZMagazynu.ComboBox.SelectedValue = ctr.cmbMagazyn.ComboBox.SelectedValue
                    frmKoszyk.idMagazyn = ctr.idMagazyn
                End If
            End If
            frmKoszyk.dgv.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells)
            frmKoszyk.btnZapiszZmiany.Enabled = True
        Else
            MsgBox("Wszystkie zaznaczone SKU masz już w swoim koszyku", MsgBoxStyle.Exclamation)
        End If
        frmKoszyk.WindowState = FormWindowState.Normal
        frmKoszyk.Activate()

        'odznaczamy wszystkie checkboxy
        For Each dgvRow As DataGridViewRow In ctr.dgv.Rows
            If dgvRow.Cells(0).Value Then dgvRow.Cells(0).Value = False
        Next
        ctr.odznaczWszystkie()
    End Sub

    Public Sub odswiezListy()
        ctr.odswiezListy()
    End Sub

    Private Sub btnZamknij_Click(sender As System.Object, e As System.EventArgs) Handles btnZamknij.Click
        Me.Close()
    End Sub
End Class