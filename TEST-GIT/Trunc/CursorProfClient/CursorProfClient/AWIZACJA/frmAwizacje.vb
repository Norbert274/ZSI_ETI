Imports System.Reflection
Imports System
Imports System.Text
Imports System.IO

Public Class frmAwizacje

    Public frmRodzic As Form
    Public strFunkcjaPowiadomieniaOGotowosci As String = Nothing 'jeśli zmienna ustawiona, to znaczy że mamy notyfikować okno rodzica o zakończeniu przygotowywania ekranu
    Private strNazwaDostawcy As String
    Private strAdresDostawcy As String
    Private strKodDostawcy As String
    Private strMiastoDostawcy As String
    Private strKrajDostawcy As String
    Private dtTypyDostawQguar As New DataTable
    Private dtDostawcy As New DataTable
    Private dtgrupy As New DataTable
    Private grupaId As Integer = 0
    Private bReagujNaZmianycmbNazwaDostawcy As Boolean = True
    Public dtListaAwiza As New DataTable
    Private niepoprawne_ilosci_sku As String = ""
    Private czy_wybrano_dostawce As String = ""
    Private czy_jest_NrPO As String = ""
    Private czy_wprowadzono_pozycje As String = ""
    Public awizo_id As Integer = -1
    Private nazwaSzablonu As String
    Public dostawca_id As Integer
    Private dostawca_id_out As Integer
	Public zamowienie_zwrot_id As Integer = -1

    Private Function CzyBylyZmiany() As Boolean
        If strNazwaDostawcy <> cmbNazwaDostawcy.Text OrElse _
           strAdresDostawcy <> txtAdresDostawca.Text OrElse _
           strKodDostawcy <> txtKodPocztowy.Text OrElse _
           strMiastoDostawcy <> txtMiastoDostawca.Text OrElse _
           strKrajDostawcy <> txtKrajDostawca.Text Then
            Return True
        End If
        Return False
    End Function

    Private Sub btnEdycjaDostawcy_Click(sender As System.Object, e As System.EventArgs) Handles btnEdycjaDostawcy.Click

        dostawca_id = cmbNazwaDostawcy.SelectedValue
        If dostawca_id = -1 Then Exit Sub

        Dim frm As New frmDostawca
        frm.btnDodajNowegoDostawce.Text = "Zapisz"
        frm.Text = "Edycja dostawcy: " & cmbNazwaDostawcy.Text
        frm.txtNazwaDostawcy.Text = cmbNazwaDostawcy.Text
        frm.txtAdresDostawca.Text = txtAdresDostawca.Text
        frm.txtKodPocztowy.Text = txtKodPocztowy.Text
        frm.txtMiastoDostawca.Text = txtMiastoDostawca.Text
        frm.txtKrajDostawca.Text = txtKrajDostawca.Text

        frm.strNazwaDostawcy = cmbNazwaDostawcy.Text
        frm.strAdresDostawcy = txtAdresDostawca.Text
        frm.strKodDostawcy = txtKodPocztowy.Text
        frm.strMiastoDostawcy = txtMiastoDostawca.Text
        frm.strKrajDostawcy = txtKrajDostawca.Text
        frm.dostawcaId = dostawca_id

        frm.ShowDialog()

        'If frm.DialogResult = System.Windows.Forms.DialogResult.Cancel Then
        '    If CzyBylyZmiany() Then
        '        Dim result As DialogResult = MsgBox("Wprowadzono zmiany w danych dostawcy. Czy chcesz zapisać wynik?.", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "Edycja dostawcy")
        '        If result = Windows.Forms.DialogResult.No Then
        '            frm.Close()
        '            Exit Sub
        '        Else
        '            If Not WalidacjaDostawcy() Then Exit Sub
        '            If zapisz_dostawce(dostawca_id) Then
        '                wczytaj_dostawcow()
        '                cmbNazwaDostawcy.SelectedValue = dostawca_id
        '            End If
        '        End If

        '    End If
        'End If

        If frm.DialogResult = System.Windows.Forms.DialogResult.OK Then
            strNazwaDostawcy = frm.txtNazwaDostawcy.Text
            strAdresDostawcy = frm.txtAdresDostawca.Text
            strKodDostawcy = frm.txtKodPocztowy.Text
            strMiastoDostawcy = frm.txtMiastoDostawca.Text
            strKrajDostawcy = frm.txtKrajDostawca.Text
            If Not CzyBylyZmiany() Then
                MsgBox("Nie wprowadzono żadnych zmian w danych dostawcy. Zapis nie był potrzebny.", MsgBoxStyle.Information, "Edycja dostawcy")
                Exit Sub
            End If
            If Not WalidacjaDostawcy() Then Exit Sub
            If zapisz_dostawce(dostawca_id) Then
                wczytaj_dostawcow()
                cmbNazwaDostawcy.SelectedValue = dostawca_id
            End If
        End If


    End Sub

    Private Sub btnDodajDostawce_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDodajDostawce.Click

        dostawca_id = -1

        Dim frm As New frmDostawca

        frm.Text = "Nowy dostawca"
        frm.dostawcaId = dostawca_id
        frm.ShowDialog()
        If frm.DialogResult = System.Windows.Forms.DialogResult.OK Then
            strNazwaDostawcy = frm.txtNazwaDostawcy.Text
            strAdresDostawcy = frm.txtAdresDostawca.Text
            strKodDostawcy = frm.txtKodPocztowy.Text
            strMiastoDostawcy = frm.txtMiastoDostawca.Text
            strKrajDostawcy = frm.txtKrajDostawca.Text

            If Not WalidacjaDostawcy() Then Exit Sub
            If zapisz_dostawce(dostawca_id) Then
                'dostawca_id = cmbNazwaDostawcy.SelectedValue
                wczytaj_dostawcow()
                cmbNazwaDostawcy.SelectedValue = dostawca_id_out
            End If
        End If
    End Sub
    Private Function WalidacjaDostawcy() As Boolean
        If strNazwaDostawcy = String.Empty Then
            MsgBox("Nie wypełniono pola (Nazwa).", MsgBoxStyle.Exclamation, "Pole wymagane")
            Return False
        End If
        If strAdresDostawcy = String.Empty Then
            MsgBox("Nie wypełniono pola (Adres).", MsgBoxStyle.Exclamation, "Pole wymagane")
            Return False
        End If
        If strKodDostawcy = String.Empty Then
            MsgBox("Nie wypełniono pola (Kod pocztowy).", MsgBoxStyle.Exclamation, "Pole wymagane")
            Return False
        End If
        If strMiastoDostawcy = String.Empty Then
            MsgBox("Nie wypełniono pola (Miasto).", MsgBoxStyle.Exclamation, "Pole wymagane")
            Return False
        End If
        If strKrajDostawcy = String.Empty Then
            MsgBox("Nie wypełniono pola (Kraj).", MsgBoxStyle.Exclamation, "Pole wymagane")
            Return False
        End If
        Return True
    End Function

    Private Sub btnDodajPozycjeZExcela_Click(sender As System.Object, e As System.EventArgs) Handles btnDodajPozycjeZExcela.Click
        Dim f As New frmAwizoDodajPozycjeZExcela
        Dim czy_jest_col_wybierz As Boolean = False
        Dim czy_juz_wybrano_sku As Boolean = False
        Dim sku_juz_wczesniej_wybrane As String = ""
        If Not dgv.DataSource Is Nothing Then
            dtListaAwiza = dgv.DataSource.Copy
        End If

        f.dtGrupy = dtgrupy
        f.ShowDialog()
        If f.DialogResult = System.Windows.Forms.DialogResult.OK Then
            If dgv.Rows.Count > 0 Then
                For Each r As DataRow In f.dtSKUExcel.Rows
                    For Each dgvRow As DataGridViewRow In dgv.Rows
                        If r.Item("sku") = dgvRow.Cells("sku").Value Then
                            czy_juz_wybrano_sku = True
                            If sku_juz_wczesniej_wybrane = "" Then
                                sku_juz_wczesniej_wybrane = CStr(r.Item("sku")) & " - " & CStr(r.Item("nazwa")) & ", " & vbNewLine
                            Else
                                sku_juz_wczesniej_wybrane = sku_juz_wczesniej_wybrane & CStr(r.Item("sku")) & " - " & CStr(r.Item("nazwa")) & ", " & vbNewLine
                            End If
                        End If
                    Next
                    If Not czy_juz_wybrano_sku Then
                        '' sprawdzamy, czy takie sku istnieje w bazie danych
                        dtListaAwiza.Rows.Add(r.Item("sku"), r.Item("nazwa"), r.Item("ilosc"), r.Item("grupa_id"))
                        czy_juz_wybrano_sku = False
                    Else
                        czy_juz_wybrano_sku = False
                    End If
                Next
                '' jeśli sku były już wcześniej na liście awiza dajemy stosowny komunikat
                If Not sku_juz_wczesniej_wybrane = "" Then
                    MsgBox("Następujące SKU znajdują się już na liście pozycji awiza:" & vbNewLine & _
                           sku_juz_wczesniej_wybrane, MsgBoxStyle.Information, "Dodawanie pozycji do awiza")
                End If
                dgv.DataSource = dtListaAwiza
            Else
                dtListaAwiza = f.dtSKUExcel
                dgv.DataSource = dtListaAwiza
            End If

            UstawKolWybierzOrazKolGrupa()
        End If

    End Sub

    Private Sub UstawKolWybierzOrazKolGrupa()
        Dim czy_jest_col_wybierz As Boolean = False
        For Each col As DataGridViewColumn In dgv.Columns
            If col.Name = "wybierz" Then
                czy_jest_col_wybierz = True
            End If
        Next

        If Not czy_jest_col_wybierz Then dgv.Columns.Insert(0, New DataGridViewCheckBoxColumn)
        dgv.Columns(0).Width = 52
        dgv.Columns(0).Name = "wybierz"
        dgv.Columns("sku").Width = 100
        dgv.Columns("nazwa").Width = 390
        dgv.Columns("ilosc").Width = 90
        dgv.Columns("sku").ReadOnly = True
        dgv.Columns("nazwa").ReadOnly = True
        Dim dgvCell As DataGridViewCheckBoxCell

        For Each row As DataGridViewRow In dgv.Rows
            dgvCell = DirectCast(row.Cells(0), DataGridViewCheckBoxCell)
            dgvCell.Value = False
        Next
        If Not czy_jest_col_wybierz Then
            Dim cmb As New DataGridViewComboBoxColumn
            cmb.MinimumWidth = 180
            cmb.Width = 180
            cmb.HeaderText = "GRUPA"
            cmb.DataSource = dtgrupy
            cmb.DisplayMember = "GRUPA"
            cmb.ValueMember = "GRUPA_ID"
            cmb.DataPropertyName = "GRUPA_ID"
            cmb.Name = "Id"
            dgv.Columns.Add(cmb)
            dgv.Columns("GRUPA_ID").Visible = False
        End If
    End Sub

    Private Sub btnDodajPozycje_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDodajPozycje.Click
        Dim frm As New frmAwizoDodajPozycje
        Dim czy_jest_col_wybierz As Boolean = False
        Dim czy_juz_wybrano_sku As Boolean = False
        Dim sku_juz_wczesniej_wybrane As String = ""
        'Dim dtListaAwiza As New DataTable
        If Not dgv.DataSource Is Nothing Then
            dtListaAwiza = dgv.DataSource.Copy
        End If

        frm.dtGrupy = dtgrupy
        frm.Grupa_id = grupaId
        frm.ShowDialog()
        If frm.DialogResult = System.Windows.Forms.DialogResult.OK Then
            grupaId = frm.Grupa_id
            If dgv.Rows.Count > 0 Then
                For Each r As DataRow In frm.dtWybraneSKU.Rows
                    For Each dgvRow As DataGridViewRow In dgv.Rows
                        If r.Item("sku") = dgvRow.Cells("sku").Value Then
                            czy_juz_wybrano_sku = True
                            If sku_juz_wczesniej_wybrane = "" Then
                                sku_juz_wczesniej_wybrane = CStr(r.Item("sku")) & " - " & CStr(r.Item("nazwa")) & ", " & vbNewLine
                            Else
                                sku_juz_wczesniej_wybrane = sku_juz_wczesniej_wybrane & CStr(r.Item("sku")) & " - " & CStr(r.Item("nazwa")) & ", " & vbNewLine
                            End If
                        End If
                    Next
                    If Not czy_juz_wybrano_sku Then
                        dtListaAwiza.Rows.Add(r.Item("sku"), r.Item("nazwa"), 0, grupaId)
                        czy_juz_wybrano_sku = False
                    Else
                        czy_juz_wybrano_sku = False
                    End If
                Next
                '' jeśli sku były już wcześniej na liście awiza dajemy stosowny komunikat
                If Not sku_juz_wczesniej_wybrane = "" Then
                    MsgBox("Następujące SKU znajdują się już na liście pozycji awiza:" & vbNewLine & _
                           sku_juz_wczesniej_wybrane, MsgBoxStyle.Information, "Dodawanie pozycji do awiza")
                End If
                dgv.DataSource = dtListaAwiza
            Else
                dtListaAwiza = frm.dtWybraneSKU
                dgv.DataSource = dtListaAwiza
            End If

            UstawKolWybierzOrazKolGrupa()

        End If
    End Sub

    Private Function zapisz_dostawce(ByVal dostawca_id As Integer) As Boolean

        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        'ws.Url = frmGlowna.strWebservice
        Dim wsWynik As wsCursorProf.DostawcaEdycjaZapiszWynik

        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.DostawcaEdycjaZapisz(frmGlowna.sesja, dostawca_id, strNazwaDostawcy, strAdresDostawcy, strKodDostawcy, strMiastoDostawcy, strKrajDostawcy)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, "Zapis dostawcy")
            Return False
            Me.Close()
            Exit Function
        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, "Zapis dostawcy")
            Return False
        End If

        If wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, "Zapis dostawcy")
            Return False
        End If

        dostawca_id_out = wsWynik.dostawca_id_out
        MsgBox(wsWynik.status_opis, MsgBoxStyle.Information, "Zapis dostawcy")
        Return True
    End Function

    Private Sub frmAwizacje_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        frmGlowna.frmAwizaNew = Nothing
    End Sub

    Private Sub frmAwizacje_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub frmAwizacje_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

        'If frmGlowna.frmAwizaNew Is Nothing Then
        '    frmGlowna.frmAwizaNew = New frmAwizacje
        'Else : frmGlowna.frmAwizaNew.WindowState = FormWindowState.Normal
        '    frmGlowna.frmAwizaNew.Activate()
        '    Me.Close()
        '    Exit Sub
        'End If

        dtpPlanowanaDataDostawy.Text = ""
        If Not wczytaj_dostawcow() Then Me.Close()
        If Not wczytaj_typy_dostaw_Qguar() Then Me.Close()
        cmbNazwaDostawcy.SelectedValue = dostawca_id
        btnEdycjaDostawcy.Enabled = False
        btnEdycjaDostawcy.BackColor = Color.LightGray
        btnUsunDostawce.Enabled = False
        btnUsunDostawce.BackColor = Color.LightGray
        If awizo_id > -1 Then
            wczytaj_awizo()
        End If
    End Sub

    Private Sub wczytaj_awizo()
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        'ws.Url = frmGlowna.strWebservice
        Dim wsWynik As wsCursorProf.AwizoWczytajWynik

        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.AwizoWczytaj(frmGlowna.sesja, awizo_id)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, "Wczytanie awiza")
            Me.Close()
            Exit Sub
        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, "Wczytanie awiza")
            Exit Sub
        End If
        If wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, "Wczytanie awiza")
            Exit Sub
        End If
        If wsWynik.status = 0 Then
            If wsWynik.awizo_id_out > 0 Then
                dtListaAwiza = wsWynik.dane.Tables(0)
                dgv.DataSource = dtListaAwiza
                dgv.AutoResizeColumns()

                dgv.Columns.Insert(0, New DataGridViewCheckBoxColumn)
                dgv.Columns(0).Width = 52
                dgv.Columns(0).Name = "wybierz"
                dgv.Columns("sku").Width = 100
                dgv.Columns("nazwa").Width = 390
                dgv.Columns("ilosc").Width = 90
                dgv.Columns("sku").ReadOnly = True
                dgv.Columns("nazwa").ReadOnly = True
                Dim dgvCell As DataGridViewCheckBoxCell

                For Each row As DataGridViewRow In dgv.Rows
                    dgvCell = DirectCast(row.Cells(0), DataGridViewCheckBoxCell)
                    dgvCell.Value = False
                Next

                Dim cmb As New DataGridViewComboBoxColumn
                cmb.MinimumWidth = 180
                cmb.Width = 180

                cmb.HeaderText = "GRUPA"
                cmb.DataSource = dtgrupy
                cmb.DisplayMember = "GRUPA"
                cmb.ValueMember = "GRUPA_ID"
                cmb.DataPropertyName = "GRUPA_ID"
                cmb.Name = "Id"
                dgv.Columns.Add(cmb)

                dgv.Columns("GRUPA_ID").Visible = False

                txtOsobaKontaktowa.Text = wsWynik.osoba_kontaktowa
                txtTelefonKontaktowy.Text = wsWynik.telefon
                txtUwagi.Text = wsWynik.uwagi
                cmbNazwaDostawcy.SelectedValue = wsWynik.dostawca_id
                txtNrPO.Text = wsWynik.numer_po
                dtpPlanowanaDataDostawy.Text = wsWynik.planowana_data_dostawy
                txtIloscPalet.Text = wsWynik.ilosc_palet
                txtIloscPaczek.Text = wsWynik.ilosc_paczek
                cmbTypDostawy.SelectedValue = wsWynik.qguar_delivery_kind_id
                Me.Text = "Edycja awiza nr " & CStr(wsWynik.awizo_id_out)
            End If
        End If

    End Sub

    Private Function wczytaj_dostawcow() As Boolean

        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        'ws.Url = frmGlowna.strWebservice
        Dim wsWynik As wsCursorProf.DostawcyWczytajWynik

        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.DostawcyWczytaj(frmGlowna.sesja)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, "Wczytanie dostawców")
            Return False
        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, "Wczytanie dostawców")
            Return False
        End If
        If wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, "Wczytanie dostawców")
            Return False
        End If

        dtDostawcy = wsWynik.dane.Tables(0)


        If dtDostawcy.Rows.Count > 0 Then
            bReagujNaZmianycmbNazwaDostawcy = False

            cmbNazwaDostawcy.DataSource = dtDostawcy
            cmbNazwaDostawcy.DisplayMember = "nazwa"
            cmbNazwaDostawcy.ValueMember = "dostawca_id"
            bReagujNaZmianycmbNazwaDostawcy = True
        End If

        If wsWynik.dane.Tables.Count > 1 AndAlso wsWynik.dane.Tables(1).Rows.Count > 0 Then
            dtgrupy = wsWynik.dane.Tables(1)
        Else
            MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał listy dostępnych grup." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, "Wczytanie grup")
            Return False
        End If
        Return True
    End Function

    Private Function wczytaj_typy_dostaw_Qguar() As Boolean

        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik As wsCursorProf.AwizoTypyDostawQguarWczytajWynik

        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.AwizoTypyDostawQguarWczytaj(frmGlowna.sesja)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, "Wczytanie typów dostaw")
            Me.Close()
            Return False
        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, "Wczytanie typów dostaw")
            Return False
        End If
        If wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, "Wczytanie typów dostaw")
            Return False
        End If

        dtTypyDostawQguar = wsWynik.dane.Tables(0)

        If dtTypyDostawQguar.Rows.Count > 0 Then
            cmbTypDostawy.DataSource = dtTypyDostawQguar
            cmbTypDostawy.DisplayMember = "NAZWA"
            cmbTypDostawy.ValueMember = "DELIVERY_KIND_ID"
            If zamowienie_zwrot_id > 0 Then
                cmbTypDostawy.SelectedValue = 1
                cmbTypDostawy.Enabled = False
            Else
                cmbTypDostawy.SelectedValue = -1
            End If
        End If

        Return True
    End Function

    Private Sub cmbNazwaDostawcy_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbNazwaDostawcy.SelectedValueChanged

        If bReagujNaZmianycmbNazwaDostawcy Then

            If cmbNazwaDostawcy.SelectedValue > 0 Then

                System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
                System.Net.ServicePointManager.Expect100Continue = False
                ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
                ws.Proxy.Credentials = CredentialCache.DefaultCredentials
                ' ws.Url = frmGlowna.strWebservice
                Dim wsWynik As wsCursorProf.DostawcaSzczegolyWczytajWynik

                Try
                    Cursor = Cursors.WaitCursor
                    Application.DoEvents()
                    wsWynik = ws.DostawcaSzczegolyWczytaj(frmGlowna.sesja, cmbNazwaDostawcy.SelectedValue)
                    Cursor = Cursors.Default
                Catch ex As Exception
                    Cursor = Cursors.Default
                    MsgBox("Błąd komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, "Wczytanie danych o dostawcy")
                    Me.Close()
                    Exit Sub
                End Try

                If wsWynik.status < 0 Then
                    MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, "Wczytanie danych o dostawcy")
                End If
                If wsWynik.status > 0 Then
                    MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, "Wczytanie danych o dostawcy")
                End If
                txtAdresDostawca.Text = wsWynik.adres
                txtKodPocztowy.Text = wsWynik.kod
                txtMiastoDostawca.Text = wsWynik.miasto
                txtKrajDostawca.Text = wsWynik.kraj
                btnEdycjaDostawcy.Enabled = True
                btnEdycjaDostawcy.BackColor = Color.DodgerBlue
                btnUsunDostawce.Enabled = True
                btnUsunDostawce.BackColor = Color.DodgerBlue
            Else
                txtAdresDostawca.Text = ""
                txtKodPocztowy.Text = ""
                txtKrajDostawca.Text = ""
                txtMiastoDostawca.Text = ""
                btnEdycjaDostawcy.Enabled = False
                btnEdycjaDostawcy.BackColor = Color.LightGray
                btnUsunDostawce.Enabled = False
                btnUsunDostawce.BackColor = Color.LightGray
            End If
        End If
    End Sub

    Private Sub btnUsunPozycje_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUsunPozycje.Click

        Dim czy_zaznaczono As Boolean = False
        '' usuwamy z dgv zaznaczone pozycje
        Dim i As Integer
        Dim ile_rows As Integer = dgv.Rows.Count
        If ile_rows = 0 Then
            MsgBox("Brak pozycji do usunięcia.", MsgBoxStyle.Exclamation, "Brak pozycji na liście awiza")
            Exit Sub
        End If
        Dim dtPozycjeAwizaBezUsunietych As New DataTable
        dtPozycjeAwizaBezUsunietych = dtListaAwiza.Copy
        dtPozycjeAwizaBezUsunietych.Rows.Clear()

        For i = ile_rows - 1 To 0 Step -1
            If dgv.Rows(i).Cells("wybierz").Value = False Then
                For Each r As DataRow In dtListaAwiza.Rows
                    If r.Item("sku") = dgv.Rows(i).Cells("sku").Value Then
                        dtPozycjeAwizaBezUsunietych.ImportRow(r)
                    End If
                Next
                'dgv.Rows.RemoveAt(i)
            Else
                czy_zaznaczono = True
            End If
        Next

        If Not czy_zaznaczono Then
            MsgBox("Nie zaznaczono żadnej pozycji do usunięcia. Aby usunąć pozycję należy zaznaczyć ją w kolumnie (wybierz) w odpowiadającym jej wierszu.", MsgBoxStyle.Exclamation, "Brak zaznaczenia pozycji do usunięcia")
        Else
            dtListaAwiza = dtPozycjeAwizaBezUsunietych
            dgv.DataSource = dtListaAwiza
        End If

    End Sub


    Private Sub dgv_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles dgv.CellValidating

        If dgv.Columns(e.ColumnIndex).HeaderText.ToLower = "ilosc" Then
            If e.RowIndex > -1 Then
                Dim intIlosc As Integer
                If Not Integer.TryParse(e.FormattedValue, intIlosc) Then
                    dgv.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.LightCoral
                    dgv.Rows(e.RowIndex).DefaultCellStyle.ForeColor = Color.White
                    MsgBox("Podana liczba nie jest liczbą całkowitą. W kolumnie (ilość) należy wpisać liczbę całkowitą większą od zera.", MsgBoxStyle.Exclamation, "Niepoprawna wartość w kolumnie (ilość)")
                    e.Cancel = True
                    Exit Sub
                End If
                If intIlosc < 1 Then
                    dgv.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.LightCoral
                    dgv.Rows(e.RowIndex).DefaultCellStyle.ForeColor = Color.White
                    MsgBox("Podaj liczbę całkowitą większą od zera.", MsgBoxStyle.Exclamation, "Niepoprawna wartość w kolumnie (ilość)")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        End If
        dgv.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.White
        dgv.Rows(e.RowIndex).DefaultCellStyle.ForeColor = Color.Black
    End Sub

    Private Sub btnAnuluj_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAnuluj.Click
        Me.Close()
    End Sub

    Private Sub btnZatwierdz_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnZatwierdz.Click
        '' walidujemy awizo i budujemy odpowiedni msgbox z błędem
        If Not WalidacjaAwiza() Then
            If Not czy_wprowadzono_pozycje = "" Then
                MsgBox(czy_wprowadzono_pozycje & " Aby zapisać awizo należy najpierw wprowadzić jego pozycje.", MsgBoxStyle.Exclamation, "Brak pozycji awiza")
                czy_wprowadzono_pozycje = ""
                czy_wybrano_dostawce = ""
                czy_jest_NrPO = ""
                Exit Sub
            End If
            If Not niepoprawne_ilosci_sku = "" Then
                If Not czy_wybrano_dostawce = "" And Not czy_jest_NrPO = "" Then
                    MsgBox("W awizie występują błędnie podane ilości dla następujących SKU: " & vbNewLine & niepoprawne_ilosci_sku & "." & _
                       vbNewLine & "Ilości muszą być liczbami całkowitymi i większymi od zera." & vbNewLine & czy_wybrano_dostawce & vbNewLine & czy_jest_NrPO, MsgBoxStyle.Exclamation, "Błędnie podane ilości")
                    niepoprawne_ilosci_sku = ""
                    czy_wybrano_dostawce = ""
                    czy_jest_NrPO = ""
                    Exit Sub
                ElseIf Not czy_wybrano_dostawce = "" And czy_jest_NrPO = "" Then
                    MsgBox("W awizie występują błędnie podane ilości dla następujących SKU: " & vbNewLine & niepoprawne_ilosci_sku & "." & _
                           vbNewLine & "Ilości muszą być liczbami całkowitymi i większymi od zera." & vbNewLine & czy_wybrano_dostawce, MsgBoxStyle.Exclamation, "Błędnie podane ilości")
                    niepoprawne_ilosci_sku = ""
                    czy_wybrano_dostawce = ""
                    Exit Sub
                ElseIf czy_wybrano_dostawce = "" And Not czy_jest_NrPO = "" Then
                    MsgBox("W awizie występują błędnie podane ilości dla następujących SKU: " & vbNewLine & niepoprawne_ilosci_sku & "." & _
                           vbNewLine & "Ilości muszą być liczbami całkowitymi i większymi od zera." & vbNewLine & czy_jest_NrPO, MsgBoxStyle.Exclamation, "Błędnie podane ilości")
                    niepoprawne_ilosci_sku = ""
                    czy_jest_NrPO = ""
                    Exit Sub
                Else
                    MsgBox("W awizie występują błędnie podane ilości dla następujących SKU: " & vbNewLine & niepoprawne_ilosci_sku & "." & _
                          vbNewLine & "Ilości muszą być liczbami całkowitymi i większymi od zera.", MsgBoxStyle.Exclamation, "Błędnie podane ilości")
                    niepoprawne_ilosci_sku = ""
                    Exit Sub
                End If
            ElseIf Not czy_wybrano_dostawce = "" And czy_jest_NrPO = "" Then
                MsgBox(czy_wybrano_dostawce, MsgBoxStyle.Exclamation, "Brak dostawcy")
                czy_wybrano_dostawce = ""
                Exit Sub
            ElseIf Not czy_jest_NrPO = "" And czy_wybrano_dostawce = "" Then
                MsgBox(czy_jest_NrPO, MsgBoxStyle.Exclamation, "Brak Numeru PO")
                czy_jest_NrPO = ""
                Exit Sub
            ElseIf Not czy_jest_NrPO = "" And Not czy_wybrano_dostawce = "" Then
                MsgBox(czy_wybrano_dostawce & vbNewLine & czy_jest_NrPO, MsgBoxStyle.Exclamation, "Brak Dostawcy i Numeru PO")
                czy_jest_NrPO = ""
                czy_wybrano_dostawce = ""
                Exit Sub
            End If
            Exit Sub
        End If

        '' walidacja ilości palet i paczek > 0
        Dim intTest As Integer
        If Not Integer.TryParse(IIf(txtIloscPalet.Text = String.Empty, 0, txtIloscPalet.Text), intTest) Then
            MsgBox("Proszę wprowadzić poprawną ilość palet!", MsgBoxStyle.Exclamation, "Niepoprawna ilość palet")
            txtIloscPalet.Focus()
            Exit Sub
        ElseIf CInt(IIf(txtIloscPalet.Text = String.Empty, 0, txtIloscPalet.Text)) < 0 Then
            MsgBox("Proszę wprowadzić poprawną ilość palet większą od zera!", MsgBoxStyle.Exclamation, "Niepoprawna ilość palet")
            txtIloscPalet.Focus()
            Exit Sub
        End If

        If Not Integer.TryParse(IIf(txtIloscPaczek.Text = String.Empty, 0, txtIloscPaczek.Text), intTest) Then
            MsgBox("Proszę wprowadzić poprawną ilość paczek!", MsgBoxStyle.Exclamation, "Niepoprawna ilość paczek")
            txtIloscPaczek.Focus()
            Exit Sub
        ElseIf CInt(IIf(txtIloscPaczek.Text = String.Empty, 0, txtIloscPaczek.Text)) < 0 Then
            MsgBox("Proszę wprowadzić poprawną ilość paczek większą od zera!", MsgBoxStyle.Exclamation, "Niepoprawna ilość paczek")
            txtIloscPaczek.Focus()
            Exit Sub
        End If

        If NZ(cmbTypDostawy.SelectedValue, -1) < 1 Then
            MsgBox("Proszę wybrać typ dostawy!", MsgBoxStyle.Exclamation, "Typ dostawy")
            Exit Sub
        End If

        If Zatwierdz_Awizo() Then
            '' czyścimy kontrolki
            txtAdresDostawca.Text = ""
            txtKodPocztowy.Text = ""
            txtKrajDostawca.Text = ""
            txtMiastoDostawca.Text = ""
            txtNrPO.Text = ""
            txtOsobaKontaktowa.Text = ""
            txtTelefonKontaktowy.Text = ""
            txtUwagi.Text = ""
            cmbNazwaDostawcy.SelectedValue = 0
            cmbTypDostawy.SelectedValue = -1
            Dim i As Integer
            For i = dgv.Rows.Count - 1 To 0 Step -1
                dgv.Rows.RemoveAt(i)
            Next

            'Dim frmRodzic As Form = frmZarzadzanieAwizami
            'odświeżamy okno rodzica (jeżeli jego okno prezentuje taką metodę)

            ''czy mamy powiadomnić ekran stan, że nasz ekran gotowy ?
            'If Not strFunkcjaPowiadomieniaOGotowosci Is Nothing Then
            '    Dim m As MethodInfo() = frmRodzic.GetType.GetMethods()
            '    For licznik As Integer = 0 To m.GetUpperBound(0)
            '        If m(licznik).Name = strFunkcjaPowiadomieniaOGotowosci Then
            '            m(licznik).Invoke(frmRodzic, Nothing)
            '        End If
            '    Next
            'End If


            'odświeżamy kontrolkę dgv z formy frmZarzadzanieAwizami
            If Not frmGlowna.frmAwizaZarzadzanie Is Nothing Then
                Dim m As MethodInfo() = frmGlowna.frmAwizaZarzadzanie.GetType.GetMethods()
                For licznik As Integer = 0 To m.GetUpperBound(0)
                    If m(licznik).Name = "odswiezListy" Then
                        m(licznik).Invoke(frmGlowna.frmAwizaZarzadzanie, Nothing)
                    End If
                Next
            End If
            'MsgBox("Poprawnie zatwierdzono awizo", MsgBoxStyle.Information, "Zatwierdzenie awiza")
            Me.Close()
            'If Not frmGlowna.frmAwizaZarzadzanie Is Nothing Then

            '    Dim m As MethodInfo() = frmGlowna.frmAwizaZarzadzanie.GetType.GetMethods()
            '    For licznik As Integer = 0 To m.GetUpperBound(0)
            '        If m(licznik).Name = "odswiezListy" Then
            '            m(licznik).Invoke(frmGlowna.frmAwizaZarzadzanie, Nothing)
            '        End If

            '    Next
            'End If

        End If

    End Sub

    Private Function WalidacjaAwiza() As Boolean
        Dim intIlosc As Integer

        Dim czy_jest_ok As Boolean = True
        If Not dgv.Rows.Count > 0 Then
            czy_wprowadzono_pozycje = "Nie wybrano żadnych pozycji awiza."
            czy_jest_ok = False
        End If
        For Each dgvRow As DataGridViewRow In dgv.Rows
            If Integer.TryParse(dgvRow.Cells("ilosc").Value, intIlosc) And dgvRow.Cells("ilosc").Value < Integer.MaxValue Then
                If dgvRow.Cells("ilosc").Value < 1 Then
                    czy_jest_ok = False
                    dgvRow.DefaultCellStyle.BackColor = Color.LightCoral
                    dgvRow.DefaultCellStyle.ForeColor = Color.White
                    If niepoprawne_ilosci_sku = String.Empty Then
                        niepoprawne_ilosci_sku = "  - " & dgvRow.Cells("sku").Value
                    Else
                        niepoprawne_ilosci_sku = niepoprawne_ilosci_sku & ", " & vbNewLine & "  - " & dgvRow.Cells("sku").Value
                    End If
                End If
            Else
                dgvRow.DefaultCellStyle.BackColor = Color.LightCoral
                dgvRow.DefaultCellStyle.ForeColor = Color.White
                czy_jest_ok = False
                If niepoprawne_ilosci_sku = String.Empty Then
                    niepoprawne_ilosci_sku = "  - " & dgvRow.Cells("sku").Value
                Else
                    niepoprawne_ilosci_sku = niepoprawne_ilosci_sku & ", " & vbNewLine & "  - " & dgvRow.Cells("sku").Value
                End If

            End If

            'If Not Decimal.TryParse(dgvRow.Cells("cena").Value, intCena) Then
            '    MsgBox("Proszę wprowadzić poprawną wartość ceny dla produktu " & dgvRow.Cells("sku").Value & "!", MsgBoxStyle.Exclamation, "Niepoprawna cena")
            '    czy_jest_ok = False
            'ElseIf dgvRow.Cells("cena").Value < 0 Then
            '    MsgBox("Proszę wprowadzić poprawną wartość ceny dla produktu " & dgvRow.Cells("sku").Value & "!", MsgBoxStyle.Exclamation, "Niepoprawna cena")
            '        czy_jest_ok = False
            'End If

        Next
        '' sprawdzamy czy wybrano dostawcę
        If txtAdresDostawca.Text = String.Empty Or _
            txtKodPocztowy.Text = String.Empty Or _
            txtKrajDostawca.Text = String.Empty Or _
            txtMiastoDostawca.Text = String.Empty Then
            czy_jest_ok = False
            czy_wybrano_dostawce = "Nie wybrano dostawcy."
        End If
        ' '' sprawdzamy czy wybrano Numer PO
        'If txtNrPO.Text = String.Empty Then
        '    czy_jest_ok = False
        '    czy_jest_NrPO = "Nie wprowadzono NumeruPO."
        'End If
        If czy_jest_ok Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function Zapisz_Awizo() As Boolean

        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik As wsCursorProf.AwizoZapiszWynik

        Dim ds As New DataSet
        ds.Tables.Add(dgv.DataSource.Copy)

        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()

            wsWynik = ws.AwizoZapisz(frmGlowna.sesja, awizo_id, cmbNazwaDostawcy.SelectedValue, txtNrPO.Text, _
                                     dtpPlanowanaDataDostawy.Text, txtOsobaKontaktowa.Text, txtTelefonKontaktowy.Text, _
                                     txtUwagi.Text, ds, _
                                     CInt(IIf(txtIloscPalet.Text = String.Empty, -1, txtIloscPalet.Text)), _
                                     CInt(IIf(txtIloscPaczek.Text = String.Empty, -1, txtIloscPaczek.Text)), cmbTypDostawy.SelectedValue)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & _
                   ex.Message, MsgBoxStyle.Critical, "Zapis awiza")
            Me.Close()
            Return False
            Exit Function
        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, "Zapis awiza")
            Return False
        End If
        If wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, "Zapis awiza")
            Return False
        End If
        If wsWynik.status = 0 Then
            awizo_id = wsWynik.awizo_id_out
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Information, "Zapis awiza")
        End If
        Return True
    End Function


    Private Sub btnZapisz_Click(sender As System.Object, e As System.EventArgs) Handles btnZapisz.Click
        '' walidujemy awizo i budujemy odpowiedni msgbox z błędem
        If Not WalidacjaAwiza() Then
            If Not czy_wprowadzono_pozycje = "" Then
                MsgBox(czy_wprowadzono_pozycje & " Aby zapisać awizo należy najpierw wprowadzić jego pozycje.", MsgBoxStyle.Exclamation, "Brak pozycji awiza")
                czy_wprowadzono_pozycje = ""
                czy_wybrano_dostawce = ""
                czy_jest_NrPO = ""
                Exit Sub
            End If
            If Not niepoprawne_ilosci_sku = "" Then
                If Not czy_wybrano_dostawce = "" And Not czy_jest_NrPO = "" Then
                    MsgBox("W awizie występują błędnie podane ilości dla następujących SKU: " & vbNewLine & niepoprawne_ilosci_sku & "." & _
                       vbNewLine & "Ilości muszą być liczbami całkowitymi i większymi od zera." & vbNewLine & czy_wybrano_dostawce & vbNewLine & czy_jest_NrPO, MsgBoxStyle.Exclamation, "Błędnie podane ilości")
                    niepoprawne_ilosci_sku = ""
                    czy_wybrano_dostawce = ""
                    czy_jest_NrPO = ""
                    Exit Sub
                ElseIf Not czy_wybrano_dostawce = "" And czy_jest_NrPO = "" Then
                    MsgBox("W awizie występują błędnie podane ilości dla następujących SKU: " & vbNewLine & niepoprawne_ilosci_sku & "." & _
                           vbNewLine & "Ilości muszą być liczbami całkowitymi i większymi od zera." & vbNewLine & czy_wybrano_dostawce, MsgBoxStyle.Exclamation, "Błędnie podane ilości")
                    niepoprawne_ilosci_sku = ""
                    czy_wybrano_dostawce = ""
                    Exit Sub
                ElseIf czy_wybrano_dostawce = "" And Not czy_jest_NrPO = "" Then
                    MsgBox("W awizie występują błędnie podane ilości dla następujących SKU: " & vbNewLine & niepoprawne_ilosci_sku & "." & _
                           vbNewLine & "Ilości muszą być liczbami całkowitymi i większymi od zera." & vbNewLine & czy_jest_NrPO, MsgBoxStyle.Exclamation, "Błędnie podane ilości")
                    niepoprawne_ilosci_sku = ""
                    czy_jest_NrPO = ""
                    Exit Sub
                Else
                    MsgBox("W awizie występują błędnie podane ilości dla następujących SKU: " & vbNewLine & niepoprawne_ilosci_sku & "." & _
                          vbNewLine & "Ilości muszą być liczbami całkowitymi i większymi od zera.", MsgBoxStyle.Exclamation, "Błędnie podane ilości")
                    niepoprawne_ilosci_sku = ""
                    Exit Sub
                End If
            ElseIf Not czy_wybrano_dostawce = "" And czy_jest_NrPO = "" Then
                MsgBox(czy_wybrano_dostawce, MsgBoxStyle.Exclamation, "Brak dostawcy")
                czy_wybrano_dostawce = ""
                Exit Sub
            ElseIf Not czy_jest_NrPO = "" And czy_wybrano_dostawce = "" Then
                MsgBox(czy_jest_NrPO, MsgBoxStyle.Exclamation, "Brak Numeru PO")
                czy_jest_NrPO = ""
                Exit Sub
            ElseIf Not czy_jest_NrPO = "" And Not czy_wybrano_dostawce = "" Then
                MsgBox(czy_wybrano_dostawce & vbNewLine & czy_jest_NrPO, MsgBoxStyle.Exclamation, "Brak Dostawcy i Numeru PO")
                czy_jest_NrPO = ""
                czy_wybrano_dostawce = ""
                Exit Sub
            End If
            Exit Sub
        End If

        '' walidacja ilości palet i paczek > 0
        Dim intTest As Integer
        If Not Integer.TryParse(IIf(txtIloscPalet.Text = String.Empty, 0, txtIloscPalet.Text), intTest) Then
            MsgBox("Proszę wprowadzić poprawną ilość palet!", MsgBoxStyle.Exclamation, "Niepoprawna ilość palet")
            Exit Sub
        ElseIf CInt(IIf(txtIloscPalet.Text = String.Empty, 0, txtIloscPalet.Text)) < 0 Then
            MsgBox("Proszę wprowadzić poprawną ilość palet większą od zera!", MsgBoxStyle.Exclamation, "Niepoprawna ilość palet")
            Exit Sub
        End If

        If Not Integer.TryParse(IIf(txtIloscPaczek.Text = String.Empty, 0, txtIloscPaczek.Text), intTest) Then
            MsgBox("Proszę wprowadzić poprawną ilość paczek!", MsgBoxStyle.Exclamation, "Niepoprawna ilość paczek")
            Exit Sub
        ElseIf CInt(IIf(txtIloscPaczek.Text = String.Empty, 0, txtIloscPaczek.Text)) < 0 Then
            MsgBox("Proszę wprowadzić poprawną ilość paczek większą od zera!", MsgBoxStyle.Exclamation, "Niepoprawna ilość paczek")
            Exit Sub
        End If

        If NZ(cmbTypDostawy.SelectedValue, -1) < 1 Then
            MsgBox("Proszę wybrać typ dostawy!", MsgBoxStyle.Exclamation, "Typ dostawy")
            Exit Sub
        End If

        If Zapisz_Awizo() Then
            'odświeżamy kontrolkę dgv z formy frmZarzadzanieAwizami
            If Not frmGlowna.frmAwizaZarzadzanie Is Nothing Then
                Dim m As MethodInfo() = frmGlowna.frmAwizaZarzadzanie.GetType.GetMethods()
                For licznik As Integer = 0 To m.GetUpperBound(0)
                    If m(licznik).Name = "odswiezListy" Then
                        m(licznik).Invoke(frmGlowna.frmAwizaZarzadzanie, Nothing)
                    End If
                Next
            End If
            Me.Activate()
        End If
    End Sub


    Private Sub btnUsunAwizo_Click(sender As System.Object, e As System.EventArgs) Handles btnUsunAwizo.Click
        If awizo_id = -1 Then
            Exit Sub
        End If
        Dim wynikMsg As DialogResult = MessageBox.Show("Zamierzasz usunać awizo nr " & CStr(awizo_id) & "! Czy chcesz kontynuować?", _
          "Usuwanie awiza", _
          MessageBoxButtons.YesNo)
        If wynikMsg = Windows.Forms.DialogResult.Yes Then
            Usun_Awizo()
            'odświeżamy kontrolkę dgv z formy frmZarzadzanieAwizami
            If Not frmGlowna.frmAwizaZarzadzanie Is Nothing Then
                Dim m As MethodInfo() = frmGlowna.frmAwizaZarzadzanie.GetType.GetMethods()
                For licznik As Integer = 0 To m.GetUpperBound(0)
                    If m(licznik).Name = "odswiezListy" Then
                        m(licznik).Invoke(frmGlowna.frmAwizaZarzadzanie, Nothing)
                    End If
                Next
            End If
            Me.Close()
        End If
    End Sub

    Private Function Zatwierdz_Awizo() As Boolean

        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik As wsCursorProf.AwizoZatwierdzWynik

        Dim ds As New DataSet
        ds.Tables.Add(dgv.DataSource.Copy)
        'Dim dtListaAwiza As New DataTable
        'dtListaAwiza.Columns.Add("sku")
        'dtListaAwiza.Columns.Add("nazwa")
        'dtListaAwiza.Columns.Add("ilosc")
        'dtListaAwiza.Columns.Add("cena")
        'For Each row As DataGridViewRow In dgv.Rows
        '    dtListaAwiza.Rows.Add(row.Cells("sku").Value, row.Cells("nazwa").Value, _
        '                          row.Cells("ilosc").Value, row.Cells("cena").Value)
        'Next
        'ds.Tables.Add(dtListaAwiza)

        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.AwizoZatwierdz(frmGlowna.sesja, awizo_id, cmbNazwaDostawcy.SelectedValue, txtNrPO.Text, _
                                     dtpPlanowanaDataDostawy.Text, txtOsobaKontaktowa.Text, _
                                     txtTelefonKontaktowy.Text, txtUwagi.Text, ds, _
                                     CInt(IIf(txtIloscPalet.Text = String.Empty, -1, txtIloscPalet.Text)), _
                                     CInt(IIf(txtIloscPaczek.Text = String.Empty, -1, txtIloscPaczek.Text)), zamowienie_zwrot_id, cmbTypDostawy.SelectedValue)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, "Zatwierdzanie awiza")
            Me.Close()
            Return False
            Exit Function
        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, "Zatwierdzanie awiza")
            Return False
        End If
        If wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, "Zatwierdzanie awiza")
            Return False
        End If
        If wsWynik.status = 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Information, "Zatwierdzanie awiza")
            Return True
        End If
        Return True
    End Function

    Private Function Usun_Awizo() As Boolean
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        'ws.Url = frmGlowna.strWebservice
        Dim wsWynik As wsCursorProf.AwizoUsunWynik
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.AwizoUsun(frmGlowna.sesja, awizo_id)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, "Usuwanie awiza")
            Me.Close()
            Return False
            Exit Function
        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, "Usuwanie awiza")
            Return False
        End If
        If wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, "Usuwanie awiza")
            Return False
        End If
        If wsWynik.status = 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Information, "Usuwanie awiza")
        End If
        Return True
    End Function

    Private Function UsunDostawce(ByVal dostawca_id As Integer) As Boolean
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        'ws.Url = frmGlowna.strWebservice
        Dim wsWynik As wsCursorProf.DostawcaUsunWynik
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.DostawcaUsun(frmGlowna.sesja, dostawca_id)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, "Usuwanie dostawcy")
            Me.Close()
            Return False
            Exit Function
        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, "Usuwanie dostawcy")
            Return False
        End If
        If wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, "Usuwanie dostawcy")
            Return False
        End If
        If wsWynik.status = 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Information, "Usuwanie dostawcy")
        End If
        Return True
    End Function


    Private Sub btnUsunDostawce_Click(sender As System.Object, e As System.EventArgs) Handles btnUsunDostawce.Click
        Dim dostawca_id As Integer = cmbNazwaDostawcy.SelectedValue
        If dostawca_id = -1 Then Exit Sub

        If MsgBox("Czy na pewno chcesz usunąć dostawcę """ & cmbNazwaDostawcy.Text & """ ?" & vbNewLine, _
                   MsgBoxStyle.YesNo, "Usuwanie dostawcy") = MsgBoxResult.Yes Then

            UsunDostawce(dostawca_id)
            wczytaj_dostawcow()
            cmbNazwaDostawcy.SelectedValue = -1
            txtAdresDostawca.Text = ""
            txtKodPocztowy.Text = ""
            txtKrajDostawca.Text = ""
            txtMiastoDostawca.Text = ""
        End If
    End Sub

    Private Function PobierzPlik(ByVal nazwa As String) As Boolean
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik = New wsCursorProf.PobierzPlikWynik

        'pobieranie pliku przez usera
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.PobierzPlik(nazwa)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, "Pobieranie pliku")
            Return False
        End Try
        If wsWynik.status = -1 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, "Pobieranie pliku")
            Return False
        End If
        If wsWynik.status = 0 Then

            Dim plik() As Byte = CType(wsWynik.plik, Byte())

            Dim sfd As New SaveFileDialog()
            Dim typ_pliku As String = ""
            Dim extension As String = Path.GetExtension("Szablon.xlsx")
            sfd.Filter = "*" & extension & "|*" & extension
            sfd.FileName = nazwaSzablonu


            If sfd.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim zapiszPath As String = sfd.FileName
                Dim fs As New FileStream(zapiszPath, FileMode.Create)
                fs.Write(plik, 0, plik.Length)
                fs.Flush()
                fs.Close()
                fs.Dispose()
                MsgBox("Poprawnie zapisano plik '" + zapiszPath + "'", MsgBoxStyle.Information, "Zapis pliku")
            End If

        End If
        Return True
    End Function
End Class