Public Class frmDaneDpd
    Public intZamowienieId As Integer
    Public dtTypy As DataTable
    Public sKod As String
    Public dtKody As DataTable

    Public intDokZw As Integer
    Public intPrzZw As Integer
    Public intOsPryw As Integer
    Public dblWartosc As Decimal
    Public dblCOD As Decimal = -1
    Public kwota_zam As Decimal
    Public strTyp As String
    Public bInEdit As Boolean
    Public bMamDane As Boolean
    Private uiSep As String = Globalization.CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator
    Private alGwaranty As New ArrayList
    Public bkodvalid As Boolean
    Private Dok_Zwrotne_Visible As Integer = -1
    Private Dok_Zwrotne_Enable As Integer = -1
    Private Prz_Zwrotne_Visible As Integer = -1
    Private Prz_Zwrotne_Enable As Integer = -1
    Private Osob_Pryw_Visible As Integer = -1
    Private Osob_Pryw_Enable As Integer = -1
    Private Wartosc_Visible As Integer = -1
    Private Wartosc_Enable As Integer = -1
    Private COD_Visible As Integer = -1
    Private COD_Enable As Integer = -1
    Private Dost_Gw_Visible As Integer = -1
    Private Dost_Gw_Enable As Integer = -1
    Public odb_oddzial As Integer = 0

    

    Private Function wczytaj_uprawnienia() As Boolean
       System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
System.Net.ServicePointManager.Expect100Continue = False
ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        'ws.Url = frmGlowna.strWebservice
        Dim wsWynik As wsCursorProf.UserDaneDpdWczytajWynik
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.UserDaneDpdWczytaj(frmGlowna.sesja, frmGlowna.intIdUzytkownikZalogowany)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
            Return False
            Exit Function
        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, Me.Text)
            Return False
            Exit Function
        ElseIf wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation,Me.Text)
        End If

        
        Return True
    End Function

    Private Sub frmDaneDpd_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        wczytaj()
    End Sub
    Public Function wczytaj() As Boolean
        If dtTypy Is Nothing Then
            If Not doczytajTypy() Then
                Return False
                Exit Function
            End If
            If Dok_Zwrotne_Visible = -1 Then
                If Not wczytaj_uprawnienia() Then
                    Return False
                    Exit Function
                End If
            End If
            cmbDorGwTyp.Items.Clear()
            cmbDorGwTyp.DataSource = dtTypy
            cmbDorGwTyp.DisplayMember = "DGT_TYP_OPIS"
            cmbDorGwTyp.ValueMember = "DGT_TYP_NAZWA"
            cmbDorGwTyp.SelectedValue = DBNull.Value
        End If

        If bMamDane = True Then
            chkDokZw.Checked = intDokZw = 1
            chkPrzZwr.Checked = intPrzZw = 1
            chkOsPryw.Checked = intOsPryw = 1
            txtWartosc.Text = dblWartosc.ToString
            If dblCOD < 0 Then
                txtCOD.Text = String.Empty
            Else
                txtCOD.Text = dblCOD.ToString
            End If

            If bkodvalid = True Then
                cmbDorGwTyp.Text = strTyp
                If cmbDorGwTyp.SelectedIndex = -1 Then
                    cmbDorGwTyp.SelectedIndex = 0
                End If
            End If



        End If

        If bInEdit = True Then

            chkDokZw.Enabled = Dok_Zwrotne_Enable

            chkPrzZwr.Enabled = Prz_Zwrotne_Enable
            
            chkOsPryw.Enabled = Osob_Pryw_Enable

            If odb_oddzial = 1 Then
                chkOsPryw.Enabled = False
            End If
            txtWartosc.Enabled = Wartosc_Enable
            txtCOD.Enabled = COD_Enable
            cmbDorGwTyp.Enabled = Dost_Gw_Enable
            If bkodvalid = False Or odb_oddzial = 1 Then
                cmbDorGwTyp.Enabled = False
            End If
            btnOk.Enabled = True
        Else
            chkDokZw.Enabled = False
            chkPrzZwr.Enabled = False
            chkOsPryw.Enabled = False
            txtWartosc.Enabled = False
            txtCOD.Enabled = False
            cmbDorGwTyp.Enabled = False
            btnOk.Enabled = False
        End If
        chkDokZw.Visible = Dok_Zwrotne_Visible
        chkPrzZwr.Visible = Prz_Zwrotne_Visible
        chkOsPryw.Visible = Osob_Pryw_Visible
        txtWartosc.Visible = Wartosc_Visible
        lblWartosc.Visible = Wartosc_Visible
        txtCOD.Visible = COD_Visible
        lblCOD.Visible = COD_Visible
        If bInEdit = True Then
            If chkDokZw.Enabled = False And chkDokZw.Visible = True Then
                ToolTip.SetToolTip(chkDokZw, "Brak uprawnień do wyboru dokumentu zwrotnego")
            Else
                ToolTip.SetToolTip(chkDokZw, "")
            End If
            If chkPrzZwr.Enabled = False And chkPrzZwr.Visible = True Then
                ToolTip.SetToolTip(chkPrzZwr, "Brak uprawnień do wyboru przesyłki zwrotnej")
            Else
                ToolTip.SetToolTip(chkPrzZwr, "")
            End If

            If chkOsPryw.Enabled = False And chkOsPryw.Visible = True Then
                ToolTip.SetToolTip(chkOsPryw, "Brak uprawnień do wyboru odbiera osoba prywatna")
            Else
                ToolTip.SetToolTip(chkOsPryw, "")
            End If

            If txtCOD.Enabled = False And txtCOD.Visible = True Then
                ToolTip.SetToolTip(txtCOD, "Brak uprawnień do wprowadzenia kwoty COD")
            Else
                ToolTip.SetToolTip(txtCOD, "")
            End If
            If txtWartosc.Enabled = False And txtWartosc.Visible = True Then
                ToolTip.SetToolTip(txtWartosc, "Brak uprawnień do wprowadzenia wartości przesyłki")
            Else
                ToolTip.SetToolTip(txtWartosc, "")
            End If
            If cmbDorGwTyp.Enabled = False And bkodvalid And cmbDorGwTyp.Visible = True Then
                ToolTip.SetToolTip(cmbDorGwTyp, "Brak uprawnień do wyboru doręczenia gwarantowanego")

            End If
            If cmbDorGwTyp.Enabled = False And bkodvalid = False And cmbDorGwTyp.Visible = True Then
                ToolTip.SetToolTip(cmbDorGwTyp, "Nieprawidłowy kod pocztowy")

            End If
            If cmbDorGwTyp.Enabled = True Then
                ToolTip.SetToolTip(cmbDorGwTyp, "")

            End If
        End If
        If bInEdit = False Then
            If cmbDorGwTyp.Visible = True Then
                ToolTip.SetToolTip(cmbDorGwTyp, "Tryb podglądu danych dpd")
            End If
            If chkDokZw.Visible = True Then
                ToolTip.SetToolTip(chkDokZw, "Tryb podglądu danych dpd")
            End If
            If chkOsPryw.Visible = True Then
                ToolTip.SetToolTip(chkOsPryw, "Tryb podglądu danych dpd")
            End If
            If chkPrzZwr.Visible = True Then
                ToolTip.SetToolTip(chkPrzZwr, "Tryb podglądu danych dpd")
            End If
            If txtCOD.Visible = True Then
                ToolTip.SetToolTip(txtCOD, "Tryb podglądu danych dpd")
            End If
            If txtWartosc.Visible = True Then
                ToolTip.SetToolTip(txtWartosc, "Tryb podglądu danych dpd")
            End If
            ToolTip.SetToolTip(btnOk, "Tryb podglądu danych dpd")
        End If

        Return True
    End Function
    Private Function doczytajTypy() As Boolean
       System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
System.Net.ServicePointManager.Expect100Continue = False
ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        'ws.Url = frmGlowna.strWebservice
        Dim wsWynik As wsCursorProf.DostawyGwarantowaneTypyWczytajWynik

        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.DostawyGwarantowaneTypyWczytaj(frmGlowna.sesja)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical)
            Me.Close()
            Return False
            Exit Function
        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical)
            Return False
        End If
        If wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation)
        End If
        dtTypy = wsWynik.dane.Tables(0)
        If Not dtKody Is Nothing Then
            alGwaranty.Clear()
            For Each col As DataColumn In dtKody.Columns
                If col.ColumnName <> "KOD_POCZTOWY" And col.ColumnName <> "MIASTO" Then
                    alGwaranty.Add(col.ColumnName)
                End If
            Next
        End If

        Return True
    End Function

    Private Sub chkDokZw_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkDokZw.CheckedChanged
        If chkDokZw.Checked = True Then
            chkPrzZwr.Checked = False
        End If
    End Sub

    Private Sub chkPrzZwr_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkPrzZwr.CheckedChanged
        If chkPrzZwr.Checked = True Then
            chkDokZw.Checked = False
        End If
    End Sub

    Private Sub txtWartosc_Leave(sender As Object, e As System.EventArgs) Handles txtWartosc.Leave
        Dim wynik As Decimal
        If txtWartosc.Text <> String.Empty Then
            If Not Decimal.TryParse(txtWartosc.Text.ToString.Replace(".", uiSep).Replace(",", uiSep), wynik) Then
                txtWartosc.Focus()
                MsgBox("Pole wartość nie zawiera poprawnej liczby.", MsgBoxStyle.Critical, Me.Text)
            End If
        End If

    End Sub

    Private Sub btnOk_Click(sender As System.Object, e As System.EventArgs) Handles btnOk.Click
        Dim wynik As Decimal
        If txtWartosc.Text <> String.Empty Then
            If Not Decimal.TryParse(txtWartosc.Text.ToString.Replace(".", uiSep).Replace(",", uiSep), wynik) Then
                txtWartosc.Focus()
                MsgBox("Pole wartość nie zawiera poprawnej liczby.", MsgBoxStyle.Critical, Me.Text)
                Exit Sub
            End If
            dblWartosc = wynik
        End If

        If txtCOD.Text <> String.Empty Then
            If Not Decimal.TryParse(txtCOD.Text.ToString.Replace(".", uiSep).Replace(",", uiSep), wynik) Then
                txtCOD.Focus()
                MsgBox("Pole Kwota COD nie zawiera poprawnej liczby.", MsgBoxStyle.Critical, Me.Text)
                Exit Sub
            End If
            dblCOD = wynik
        End If
        If chkDokZw.Checked = True Then
            intDokZw = 1
        Else
            intDokZw = 0
        End If
        If chkOsPryw.Checked = True Then
            intOsPryw = 1
        Else
            intOsPryw = 0
        End If
        If chkPrzZwr.Checked = True Then
            intPrzZw = 1
        Else
            intPrzZw = 0
        End If
        strTyp = IIf(cmbDorGwTyp.SelectedValue Is DBNull.Value, String.Empty, cmbDorGwTyp.SelectedValue)
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnAnuluj_Click(sender As System.Object, e As System.EventArgs) Handles btnAnuluj.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub cmbDorGwTyp_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbDorGwTyp.SelectedIndexChanged
        If cmbDorGwTyp.SelectedIndex > 0 Then
            If alGwaranty.Contains(cmbDorGwTyp.SelectedValue) Then
                If dtKody.Select(String.Format("KOD_POCZTOWY = '{0}'", sKod)).Length = 1 Then
                    If dtKody.Select(String.Format("KOD_POCZTOWY = '{0}'", sKod))(0).Item(cmbDorGwTyp.SelectedValue).ToString <> "TAK" Then
                        MsgBox("Dla kodu pocztowego: " & sKod & ", nie można wybrać dostawy gwarantowanej: " & cmbDorGwTyp.Text, MsgBoxStyle.Critical, Me.Text)
                        cmbDorGwTyp.SelectedIndex = 0
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub txtCOD_Leave(sender As Object, e As System.EventArgs) Handles txtCOD.Leave
        If txtCOD.Text <> String.Empty Then
            Dim wynik As Decimal

            If Not Decimal.TryParse(txtCOD.Text.ToString.Replace(".", uiSep).Replace(",", uiSep), wynik) Then
                txtCOD.Focus()
                MsgBox("Pole Kwota COD nie zawiera poprawnej liczby.", MsgBoxStyle.Critical, Me.Text)
                Exit Sub
            End If
            'If wynik >= 5000 Then
            '    Dim wynikWartosc As Decimal
            '    If Not Decimal.TryParse(txtWartosc.Text.ToString.Replace(".", uiSep).Replace(",", uiSep), wynikWartosc) Then
            '        txtWartosc.Text = txtCOD.Text
            '    End If
            '    If wynik > wynikWartosc Then
            '        txtWartosc.Text = txtCOD.Text
            '    End If
            'End If
        End If
    End Sub

    
    
End Class