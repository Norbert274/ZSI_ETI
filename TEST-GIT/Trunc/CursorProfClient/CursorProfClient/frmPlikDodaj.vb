Imports System.IO
Public Class frmPlikDodaj
    Private dtPliki As DataTable

    Private Sub btnDodaj_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDodaj.Click
        If txtTytul.Text = "" Then
            MsgBox("Nie podano tytułu pliku.", MsgBoxStyle.Exclamation, "Brak tytułu pliku.")
            Exit Sub
        ElseIf txtPilk.Text = "" Then
            MsgBox("Nie wybrano pliku.", MsgBoxStyle.Exclamation, "Nie wybrano pliku.")
            Exit Sub
        Else : Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        End If
        
    End Sub
    

    'Public Function wczytaj() As Boolean
    '   System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
    'System.Net.ServicePointManager.Expect100Continue = False
    'ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
    'ws.Proxy.Credentials = CredentialCache.DefaultCredentials
    '    'ws.Url = frmGlowna.strWebservice
    '    Dim wsWynik As wsCursorProf.PlikiDoPobraniaListaWynik
    '    Dim frm As New frmPlikiDoPobrania
    '    'odczyt z serwera
    '    Try
    '        Cursor = Cursors.WaitCursor
    '        Application.DoEvents()
    '        wsWynik = ws.PlikiDoPobraniaLista(frmGlowna.sesja)
    '        Cursor = Cursors.Default
    '    Catch ex As Exception
    '        Cursor = Cursors.Default
    '        MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical)
    '        Return False
    '    End Try
    '    dtPliki = wsWynik.dane.Tables(0)
    '    Dim dv As New DataView(dtPliki)

    '    If wsWynik.status = -1 Then
    '        MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation)
    '        Return False
    '    End If
    '    If wsWynik.status_opis = "Nie centrala" Then
    '        frm.btnDodajPlik.Enabled = False
    '    End If
    '    frm.dgvPliki.DataSource = dtPliki
    '    Return True
    'End Function
    Private Sub btnAnuluj_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAnuluj.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub btnPlik_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPlik.Click
        Dim ofdPlik As New OpenFileDialog()
        'ofdPlik.Filter = "Word document (*.doc;*.docx)|*.doc;*docx| PDF document (*.pdf)|*.pdf"
        ofdPlik.Multiselect = False

        If ofdPlik.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtPilk.Text = ofdPlik.FileName
        End If


    End Sub

    Private Sub btnMiniaturka_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMiniaturka.Click

        Dim ofdMin As New OpenFileDialog()
        ofdMin.Filter = "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg"
        ofdMin.Multiselect = False

        If ofdMin.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtMiniaturka.Text = ofdMin.FileName
        End If
    End Sub


    Private Sub frmPlikDodaj_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        txtMiniaturka.ReadOnly = True
        txtPilk.ReadOnly = True
        txtTytul.Focus()
    End Sub

    Private Sub frmPlikDodaj_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class