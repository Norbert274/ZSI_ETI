Public Class frmWiadomosci
    Public dtWiadomosci As DataTable
    Public IdWiadomosc As Integer
    Private wczytuje As Boolean = False

    Private Sub btnZamknij_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnZamknij.Click
        Me.Close()
    End Sub

    Private Sub frmWiadomosci_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If Not wczytaj() Then
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
        Dim wsWynik As wsCursorProf.WiadomosciListaWynik
        'odczyt z serwera
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.WiadomosciLista(frmGlowna.sesja)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
            Return False
        End Try

        dtWiadomosci = wsWynik.dane.Tables(0)
        wczytuje = True
        For Each row As DataRow In dtWiadomosci.Rows
            If row.Item("TYTUL").ToString.Length > 33 Then
                ListBoxTytuly.Items.Add(Mid(row.Item("TYTUL").ToString, 1, 30) + "...")
            Else
                ListBoxTytuly.Items.Add(row.Item("TYTUL").ToString)
            End If
            'ToolTipTytul.SetToolTip(ListBoxTytuly, "") ' ListBoxTytuly.Items(ListBoxTytuly.Items.Count-1).
        Next
        wczytuje = False
        If ListBoxTytuly.Items.Count > 0 Then
            ListBoxTytuly.SelectedIndex = 0
        End If
        'If wsWynik.status_opis = "Nie centrala" Then
        '    btnUsun.Enabled = False
        '    btnDodajWiadomosc.Enabled = False
        'End If
        If wsWynik.status = -1 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, Me.Text)
        End If
        Return True
    End Function

    Private Sub ListBoxTytuly_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListBoxTytuly.SelectedIndexChanged
        Dim index As Integer
        index = ListBoxTytuly.SelectedIndex
        rtxtTresc.Text = dtWiadomosci.Rows(index).Item("TRESC").ToString
        IdWiadomosc = dtWiadomosci.Rows(index).Item("WIADOMOSC_ID")
        If wczytuje = False Then
            ToolTipTytul.SetToolTip(ListBoxTytuly, dtWiadomosci.Rows(index).Item("TYTUL").ToString)
        End If
    End Sub
    Private Sub ClearDs()
        dtWiadomosci.Rows.Clear()
    End Sub

    Private Sub btnDodajWiadomosc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDodajWiadomosc.Click
        'If ListBoxTytuly.SelectedIndex = -1 Then
        '    MessageBox.Show("Zaznacz najpierw tytuł wiadomości", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Exit Sub
        'End If

        Dim frm As New frmNowaWiadomosc
        frm.dtWiadomosci = dtWiadomosci
        'If MessageBox.Show("Nowa wiadomość zostanie zapisana do bazy danych." & vbNewLine & "Czy chcesz kontynuować?", "Potwierdź zmiany", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
        frm.ShowDialog()
        If frm.DialogResult = Windows.Forms.DialogResult.OK Then

            'ListBoxTytuly.Items.Add(frm.strTytul)
            'Dim nowyNode As New TreeNode(frm.Nazwa)
            'nowyNode.Name = -1
            'tv.SelectedNode.Nodes.Add(nowyNode)
            'Dim rowGrupa As DataRow
            'rowGrupa = ds.Tables("Grupy").NewRow()
            'rowGrupa.Item("Grupa_Id") = -1
            'rowGrupa.Item("Nazwa") = frm.Nazwa
            'rowGrupa.Item("Dodaj") = 1
            'ds.Tables("Grupy").Rows.Add(rowGrupa)
            'AddHierchia(tv.SelectedNode.Name, -1, 1)

           System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
System.Net.ServicePointManager.Expect100Continue = False
ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
ws.Proxy.Credentials = CredentialCache.DefaultCredentials
            'ws.Url = frmGlowna.strWebservice
            Dim grupa_id As Integer
            grupa_id = -1
            Dim wsWynik As wsCursorProf.WiadomoscDodajNowaWynik
            'odczyt z serwera
            Try
                Cursor = Cursors.WaitCursor
                Application.DoEvents()
                wsWynik = ws.WiadomoscDodajNowa(frmGlowna.sesja, frm.strTytul, frm.strTresc)
                Cursor = Cursors.Default
            Catch ex As Exception
                Cursor = Cursors.Default
                MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
                Return
            End Try
            'If wsWynik.status >= 0 Then
            '    nowyNode.Name = wsWynik.grupa_id
            'End If
            ClearDs()
            ListBoxTytuly.Items.Clear()
            sprawdzWynik(wsWynik.status, wsWynik.status_opis)

        End If
        'End If
    End Sub

    Public Sub sprawdzWynik(ByVal status As Integer, ByVal status_opis As String)
        If status < 0 Then
            Me.odswiezListy()
            MsgBox(status_opis, MsgBoxStyle.Critical, Me.Text)
            Return
        ElseIf status > 0 Then
            'intIdWybranejGrupy = selectedId
            Me.odswiezListy()
            MsgBox(status_opis, MsgBoxStyle.Exclamation, Me.Text)
        ElseIf status = 0 Then
            'intIdWybranejGrupy = selectedId
            Me.odswiezListy()
            MsgBox(status_opis, MsgBoxStyle.Information, Me.Text)
        End If
    End Sub

    Public Sub odswiezListy()
        wczytaj()
    End Sub

    Private Sub btnUsun_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUsun.Click
        If ListBoxTytuly.SelectedIndex = -1 Then
            MessageBox.Show("Zaznacz najpierw wiadomość do usunięcia.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        ClearDs()
        'Dim frm As New frmUsunGrupaChkBox
        'frm.blnUsunPodrzedne = False
        If MessageBox.Show("Zamierzasz usunąć wiadomość z bazy danych." & vbNewLine & "Czy chcesz kontynuować?", "Potwierdź zmiany", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
           System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
System.Net.ServicePointManager.Expect100Continue = False
ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
ws.Proxy.Credentials = CredentialCache.DefaultCredentials
            'ws.Url = frmGlowna.strWebservice
            Dim wsWynik As wsCursorProf.WiadomoscUsunWynik

            'odczyt z serwera
            Try
                Cursor = Cursors.WaitCursor
                Application.DoEvents()
                wsWynik = ws.WiadomoscUsun(frmGlowna.sesja, IdWiadomosc)
                Cursor = Cursors.Default
            Catch ex As Exception
                Cursor = Cursors.Default
                MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
                Return
            End Try

            If wsWynik.status = -1 Then
                MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, Me.Text)
            End If
            'sprawdzWynik(wsWynik.status, wsWynik.status_opis,)
        End If
        'odświeżenie kontrolki TreeView z grupami

        ListBoxTytuly.Items.Clear()
        rtxtTresc.Text = ""
        wczytaj()
    End Sub

    Private Sub btnOdswiez_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOdswiez.Click
        ListBoxTytuly.Items.Clear()
        wczytaj()
    End Sub

    
End Class