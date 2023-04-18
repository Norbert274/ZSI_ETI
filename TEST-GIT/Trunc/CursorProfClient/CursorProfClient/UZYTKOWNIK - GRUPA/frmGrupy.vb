Imports System.Text
Public Class frmGrupy

    Public intIdWybranejGrupy As Integer = -1
    Dim grupa_wybrana As String = ""
    Private dtGrupy As DataTable 'do drzewa z grupami
    Private dtHierarchia As DataTable 'do drzewa z grupami
    Private ds As New DataSet()
    Private listGrupyPodrzedne As New List(Of Integer)

    Private Sub frmGrupy_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        'inicjalizacja ustawieñ kontrolek
        wypelnijStruktureDS()
        If Not wczytaj() Then Me.Close()
    End Sub

    Private Function wczytaj() As Boolean
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        'ws.Url = frmGlowna.strWebservice
        Dim wsWynik As wsCursorProf.GrupyPokazWynik
        Dim sortowanieKolumna As String = ""
        Dim sortowanieNarastajaco As Boolean = True

        'odczyt listy z serwera
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.GrupyPokaz(frmGlowna.sesja)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("B³¹d komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegó³y b³êdu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
            Return False
        End Try
        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, Me.Text)
            Return False
        ElseIf wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, Me.Text)
        End If

        'czyszczenie kontrolek przed wype³nieniem
        tv.Nodes.Clear()
        'wype³nienie kontrolek wynikami
        If wsWynik.dane.Tables.Count > 0 Then
            zaladujDrzewoGrup()
            selectNode()
        Else
            MsgBox("B³¹d wewnêtrzny systemu. Serwer nie przes³a³ listy grup" & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
            Me.Close()
        End If
        Return True
    End Function

    Private Sub selectNode()
        If intIdWybranejGrupy >= 0 Then
            'tv.SelectedNode = tv.Nodes.Find(grupa_wybrana, True)(0)
            tv.SelectedNode = tv.Nodes(0)
        Else
            If tv.Nodes.Count > 0 Then
                tv.SelectedNode = tv.Nodes(0)
            End If
        End If
        tv.ExpandAll()
        tv.Focus()
        UpdateButtonsEnabled()
    End Sub

    Public Sub odswiezListy()
        wczytaj()
    End Sub


#Region "Handlery formy"

    Private Sub btnOdswiez_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOdswiez.Click
        wczytaj()
    End Sub

    Private Sub btnUsun_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUsun.Click
        If tv.SelectedNode Is Nothing Then
            MessageBox.Show("Zaznacz najpierw grupê", "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        grupa_wybrana = tv.SelectedNode.Text

        ClearDs()
        'Dim frm As New frmUsunGrupaChkBox
        'frm.blnUsunPodrzedne = False
        If MessageBox.Show("Zamierzasz usun¹æ grupê: " & grupa_wybrana & " z bazy danych." & vbNewLine & "Czy chcesz kontynuowaæ?", "PotwierdŸ zmiany", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
            System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
            System.Net.ServicePointManager.Expect100Continue = False
            ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
            ws.Proxy.Credentials = CredentialCache.DefaultCredentials
            'ws.Url = frmGlowna.strWebservice
            Dim wsWynik As wsCursorProf.GrupaUsunWynik

            'odczyt z serwera
            Try
                Cursor = Cursors.WaitCursor
                Application.DoEvents()
                wsWynik = ws.GrupaUsun(frmGlowna.sesja, grupa_wybrana)
                Cursor = Cursors.Default
            Catch ex As Exception
                Cursor = Cursors.Default
                MsgBox("B³¹d komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegó³y b³êdu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
                Return
            End Try

            If wsWynik.status = -1 Then
                MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, Me.Text)
            End If
            'sprawdzWynik(wsWynik.status, wsWynik.status_opis,)
        End If
        'odœwie¿enie kontrolki TreeView z grupami
        wczytaj()
    End Sub

    Private Sub btnNowyPodrzedny_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNowyPodrzedny.Click
        If tv.SelectedNode Is Nothing Then
            MessageBox.Show("Zaznacz najpierw grupê", "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        ClearDs()
        Dim frm As New frmNowaGrupaNazwa
        frm.dtGrupy = dtGrupy
        If MessageBox.Show("Nowa grupa zostanie zapisana do bazy danych." & vbNewLine & "Czy chcesz kontynuowaæ?", "PotwierdŸ zmiany", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
            frm.ShowDialog()
            If frm.DialogResult = Windows.Forms.DialogResult.OK Then
                Dim nowyNode As New TreeNode(frm.Nazwa)
                nowyNode.Name = -1
                tv.SelectedNode.Nodes.Add(nowyNode)
                Dim rowGrupa As DataRow
                rowGrupa = ds.Tables("Grupy").NewRow()
                rowGrupa.Item("Grupa_Id") = -1
                rowGrupa.Item("Nazwa") = frm.Nazwa
                rowGrupa.Item("Dodaj") = 1
                ds.Tables("Grupy").Rows.Add(rowGrupa)
                AddHierchia(tv.SelectedNode.Name, -1, 1)

                System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
                System.Net.ServicePointManager.Expect100Continue = False
                ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
                ws.Proxy.Credentials = CredentialCache.DefaultCredentials
                'ws.Url = frmGlowna.strWebservice
                Dim grupa_id As Integer
                grupa_id = -1
                Dim wsWynik As wsCursorProf.GrupyDodajNowaWynik
                'odczyt z serwera
                Try
                    Cursor = Cursors.WaitCursor
                    Application.DoEvents()
                    wsWynik = ws.GrupyDodajNowa(frmGlowna.sesja, grupa_id, frm.Nazwa, ds)
                    Cursor = Cursors.Default
                Catch ex As Exception
                    Cursor = Cursors.Default
                    MsgBox("B³¹d komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegó³y b³êdu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
                    Return
                End Try
                If wsWynik.status >= 0 Then
                    nowyNode.Name = wsWynik.grupa_id
                End If
                sprawdzWynik(wsWynik.status, wsWynik.status_opis, nowyNode.Name)
            End If
        End If
    End Sub

    Private Sub tv_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tv.AfterSelect
        intIdWybranejGrupy = tv.SelectedNode.Name
        UpdateButtonsEnabled()
    End Sub


#End Region

    Public Sub sprawdzWynik(ByVal status As Integer, ByVal status_opis As String, ByVal selectedId As Integer)
        If status < 0 Then
            Me.odswiezListy()
            MsgBox(status_opis, MsgBoxStyle.Critical, Me.Text)
            Return
        ElseIf status > 0 Then
            intIdWybranejGrupy = selectedId
            Me.odswiezListy()
            MsgBox(status_opis, MsgBoxStyle.Exclamation, Me.Text)
        ElseIf status = 0 Then
            intIdWybranejGrupy = selectedId
            Me.odswiezListy()
            MsgBox(status_opis, MsgBoxStyle.Information, Me.Text)
        End If
    End Sub

    Private Sub zaladujDrzewoGrup()
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        'ws.Url = frmGlowna.strWebservice
        Dim wsWynik As wsCursorProf.GrupyPokazWynik

        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.GrupyPokaz(frmGlowna.sesja)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("B³¹d komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegó³y b³êdu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
            Me.Close()
            Exit Sub
        End Try
        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, Me.Text)
            Me.Close()
            Exit Sub
        ElseIf wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, Me.Text)
        End If
        dtGrupy = wsWynik.dane.Tables(0)
        dtHierarchia = wsWynik.dane.Tables(1)

        'dodajemy wêz³y
        dtHierarchia.DefaultView.RowFilter = "nadrzedna_id is null"
        Dim dtMojaHierarchia As DataTable = wsWynik.dane.Tables(1).DefaultView.ToTable
        For Each dtWiersz As DataRow In dtMojaHierarchia.Rows
            dodajWezlyPotomne(-1, dtWiersz("podrzedna_id"), dtGrupy, dtHierarchia)
        Next
        tv.ExpandAll()
    End Sub

    Private Sub dodajWezlyPotomne(ByVal idRodzica As Integer, ByVal idDoDodania As Integer, ByVal dtGrupy As DataTable, ByVal dtHierarchia As DataTable)
        'odszukanie w³asnej nazwy
        dtGrupy.DefaultView.RowFilter = "grupa_id=" & idDoDodania
        Dim dtGrupa As DataTable = dtGrupy.DefaultView.ToTable
        If dtGrupa.Rows.Count < 1 Then Exit Sub
        Dim strNazwa As String = dtGrupa.Rows(0).Item("nazwa")

        'odszukanie wêz³a rodzica w drzewie i dodanie siê do niego
        Dim newNode As TreeNode
        If idRodzica < 0 Then
            newNode = tv.Nodes.Add(idDoDodania, strNazwa)
            newNode.Tag = idDoDodania

        Else
            Dim nodes() As TreeNode
            nodes = tv.Nodes.Find(idRodzica, True)
            For Each node As TreeNode In nodes
                newNode = node.Nodes.Add(idDoDodania, strNazwa)
                newNode.Tag = idDoDodania
            Next
        End If

        'czy mamy wêz³y potomne?
        dtHierarchia.DefaultView.RowFilter = "nadrzedna_id=" & idDoDodania
        Dim dtPotomne As DataTable = dtHierarchia.DefaultView.ToTable
        For Each dtWiersz As DataRow In dtPotomne.Rows
            dodajWezlyPotomne(idDoDodania, dtWiersz("podrzedna_id"), dtGrupy, dtHierarchia)
        Next
    End Sub

    Private Sub UsunGrupaHierarchia(ByVal node As TreeNode, ByVal usunPodrzedne As Boolean)
        Dim rowGrupa As DataRow
        rowGrupa = ds.Tables("Grupy").NewRow()
        rowGrupa.Item("Grupa_Id") = node.Name
        rowGrupa.Item("Nazwa") = node.Text
        rowGrupa.Item("Dodaj") = 0
        ds.Tables("Grupy").Rows.Add(rowGrupa)
        For Each childNode As TreeNode In node.Nodes
            AddHierchia(node.Name, childNode.Name, 0)
            If usunPodrzedne = True Then
                UsunGrupaHierarchia(childNode, usunPodrzedne)
            End If
        Next
    End Sub

    Private Sub UpdateButtonsEnabled()
        If Not tv.SelectedNode Is Nothing Then
            btnNowyPodrzedny.Enabled = True
            If tv.SelectedNode.Level = 0 Then
                btnUsun.Enabled = False
                btnEdytujGrupe.Enabled = False
            End If
            If tv.SelectedNode.Level = 1 Then
                btnUsun.Enabled = True
                btnEdytujGrupe.Enabled = True
            End If
            If tv.SelectedNode.Nodes.Count = 0 Then
            End If
            If tv.SelectedNode.Nodes.Count > 0 And tv.SelectedNode.Level > 0 Then
            End If
            If tv.SelectedNode.Level > 1 Then
                btnUsun.Enabled = True
                btnEdytujGrupe.Enabled = True
            End If
        Else
            btnNowyPodrzedny.Enabled = False
            btnUsun.Enabled = False
        End If
    End Sub

    Private Sub UsunNode(ByVal UsunPodrzedne As Boolean)
        If UsunPodrzedne = False Then
            Dim parentNode As TreeNode
            parentNode = tv.SelectedNode.Parent
            Dim childNodes(tv.SelectedNode.Nodes.Count - 1) As TreeNode
            tv.SelectedNode.Nodes.CopyTo(childNodes, 0)
            tv.SelectedNode.Nodes.Clear()
            Dim child As TreeNode
            If parentNode Is Nothing Then
                For Each child In childNodes
                    tv.Nodes.Add(child)
                Next
            Else
                For Each child In childNodes
                    parentNode.Nodes.Add(child)
                Next
            End If
        End If
        tv.SelectedNode.Remove()
        tv.Refresh()
    End Sub

    Private Sub wypelnijStruktureDS()
        Dim dtGrupy As New DataTable("Grupy")
        dtGrupy.Columns.Add(New DataColumn("Grupa_Id"))
        dtGrupy.Columns.Add(New DataColumn("Nazwa"))
        dtGrupy.Columns.Add(New DataColumn("Dodaj"))
        Dim dtHierarchia As New DataTable("Hierarchia")
        dtHierarchia.Columns.Add(New DataColumn("Nadrzedna_Id"))
        dtHierarchia.Columns.Add(New DataColumn("Podrzedna_Id"))
        dtHierarchia.Columns.Add(New DataColumn("Dodaj"))
        ds.Tables.Add(dtGrupy)
        ds.Tables.Add(dtHierarchia)
    End Sub

    Private Sub AddHierchia(ByVal nadrzedne As Integer, ByVal podrzedne As Integer, ByVal dodaj As Integer)
        Dim rowHierarchia As DataRow
        rowHierarchia = ds.Tables("Hierarchia").NewRow()
        rowHierarchia.Item("Nadrzedna_Id") = nadrzedne
        rowHierarchia.Item("Podrzedna_Id") = podrzedne
        rowHierarchia.Item("Dodaj") = dodaj
        ds.Tables("Hierarchia").Rows.Add(rowHierarchia)
    End Sub

    Private Sub ClearDs()
        ds.Tables("Grupy").Rows.Clear()
        ds.Tables("Hierarchia").Rows.Clear()
    End Sub



    Private Sub btnEdytujGrupe_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdytujGrupe.Click
        If tv.SelectedNode Is Nothing Then
            MessageBox.Show("Zaznacz najpierw grupê", "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        Dim grupa_do_edycji As String = tv.SelectedNode.Text
        ClearDs()
        Dim frm As New frmNowaGrupaNazwa
        frm.Text = "Zmiana nazwy"
        frm.dtGrupy = dtGrupy
        frm.tbNazwa.Text = grupa_do_edycji
        If MessageBox.Show("Zamierzasz edytowaæ grupê: " & grupa_do_edycji & vbNewLine & "Czy chcesz kontynuowaæ?", "Zmiana nazwy", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
            frm.ShowDialog()
            If frm.DialogResult = Windows.Forms.DialogResult.OK Then
                Dim nowyNode As New TreeNode(frm.Nazwa)
                nowyNode.Name = -1
                tv.SelectedNode.Nodes.Add(nowyNode)
                Dim rowGrupa As DataRow
                rowGrupa = ds.Tables("Grupy").NewRow()
                rowGrupa.Item("Grupa_Id") = -1
                rowGrupa.Item("Nazwa") = frm.Nazwa
                rowGrupa.Item("Dodaj") = 1
                ds.Tables("Grupy").Rows.Add(rowGrupa)
                AddHierchia(tv.SelectedNode.Name, -1, 1)

                System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
                System.Net.ServicePointManager.Expect100Continue = False
                ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
                ws.Proxy.Credentials = CredentialCache.DefaultCredentials
                'ws.Url = frmGlowna.strWebservice
                Dim grupa_id As Integer
                grupa_id = -1
                Dim wsWynik As wsCursorProf.GrupyEdycjaWybranaWynik
                'odczyt z serwera
                Try
                    Cursor = Cursors.WaitCursor
                    Application.DoEvents()
                    wsWynik = ws.GrupyEdycjaWybrana(frmGlowna.sesja, grupa_do_edycji, frm.Nazwa)
                    Cursor = Cursors.Default
                Catch ex As Exception
                    Cursor = Cursors.Default
                    MsgBox("B³¹d komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegó³y b³êdu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
                    Return
                End Try
                'If wsWynik.status >= 0 Then
                '    nowyNode.Name = wsWynik.grupa_id
                'End If
                sprawdzWynik(wsWynik.status, wsWynik.status_opis, nowyNode.Name)
            End If
        End If
    End Sub

    Private Sub btnAnuluj_Click(sender As System.Object, e As System.EventArgs) Handles btnAnuluj.Click
        Me.Close()
    End Sub
End Class