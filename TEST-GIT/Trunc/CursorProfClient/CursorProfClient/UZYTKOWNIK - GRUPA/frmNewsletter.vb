Imports System.IO
Imports System.Data
Public Class frmNewsletter
    Private dtGrupy As DataTable
    Private dtUzytkownicy As DataTable
    Private dvUzytkownicy As DataView
    Private dtTypy As DataTable
    Private dtWielkosc As DataTable
    Private sciezka As String
    Public intIdNewsleter As Integer
    Private wczytuje As Boolean = False
    Public frmRodzic As frmNewsleterLista

    Private Sub wczytaj()
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        'ws.Url = frmGlowna.strWebservice
        Dim wsWynik As wsCursorProf.NewsleterOdczytajWynik
        Dim data As DateTime = DateTime.Now
        Try
            Cursor = Cursors.WaitCursor

            wsWynik = ws.NewsleterOdczytaj(frmGlowna.sesja, intIdNewsleter)

            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
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

        'GRUPY

        If wsWynik.dane.Tables.Count > 0 Then
            wczytuje = True
            dtGrupy = wsWynik.dane.Tables(0).Copy()
            chkListGrupy.Items.Clear()

            For Each row As DataRow In dtGrupy.Rows
                chkListGrupy.Items.Add(row.Item("NAZWA").ToString, False)
            Next
            wczytuje = False
        Else
            MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał listy grup." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
            Me.Close()
            Exit Sub
        End If

        'typ     
        If wsWynik.dane.Tables.Count > 1 Then
            wczytuje = True
            dtTypy = wsWynik.dane.Tables(1).Copy()
            chkListTyp.Items.Clear()

            For Each row As DataRow In dtTypy.Rows
                chkListTyp.Items.Add(row.Item("NAZWA").ToString, False)
            Next
            wczytuje = False
        Else
            MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał listy typów." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
            Me.Close()
            Exit Sub
        End If

        'wielkosc     
        If wsWynik.dane.Tables.Count > 2 Then
            wczytuje = True
            dtWielkosc = wsWynik.dane.Tables(2).Copy()
            chkListWielkosc.Items.Clear()

            For Each row As DataRow In dtWielkosc.Rows
                chkListWielkosc.Items.Add(row.Item("NAZWA").ToString, False)
            Next
            wczytuje = False
        Else
            MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał listy wielkości." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
            Me.Close()
            Exit Sub
        End If




        If intIdNewsleter > 0 Then

            If wsWynik.dane.Tables.Count > 4 Then
                Me.txtTytul.Text = wsWynik.dane.Tables(4).Rows(0).Item("TYTUL")
                Me.rtxtWiadomosc.Text = wsWynik.dane.Tables(4).Rows(0).Item("TRESC")
                Me.txtTytul.Enabled = False
                Me.rtxtWiadomosc.Enabled = False
            Else
                MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał danych newslettera" & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
                Me.Close()
                Exit Sub
            End If
            'grupy zaznaczanie
            If wsWynik.dane.Tables.Count > 5 Then
                wczytuje = True
                For Each rowGrupa As DataRow In wsWynik.dane.Tables(5).Rows
                    For i As Integer = 0 To chkListGrupy.Items.Count - 1
                        If chkListGrupy.GetItemText(chkListGrupy.Items(i)) = rowGrupa.Item("NAZWA") Then
                            chkListGrupy.SetItemCheckState(i, CheckState.Checked)
                            Exit For
                        End If
                    Next
                Next
                chkListGrupy.Enabled = False
                wczytuje = False
            Else
                MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał listy grup newslettera" & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
                Me.Close()
                Exit Sub
            End If

            'typ zaznaczanie
            If wsWynik.dane.Tables.Count > 6 Then
                wczytuje = True
                For Each rowTyp As DataRow In wsWynik.dane.Tables(6).Rows
                    For i As Integer = 0 To chkListTyp.Items.Count - 1
                        If chkListTyp.GetItemText(chkListTyp.Items(i)) = rowTyp.Item("NAZWA") Then
                            chkListTyp.SetItemCheckState(i, CheckState.Checked)
                            Exit For
                        End If
                    Next
                Next
                chkListTyp.Enabled = False
                wczytuje = False
            Else
                MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał listy typu newslettera" & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
                Me.Close()
                Exit Sub
            End If

            'wielkosc zaznaczanie
            If wsWynik.dane.Tables.Count > 7 Then
                wczytuje = True
                For Each rowWielkosc As DataRow In wsWynik.dane.Tables(7).Rows
                    For i As Integer = 0 To chkListWielkosc.Items.Count - 1
                        If chkListWielkosc.GetItemText(chkListWielkosc.Items(i)) = rowWielkosc.Item("NAZWA") Then
                            chkListWielkosc.SetItemCheckState(i, CheckState.Checked)
                            Exit For
                        End If
                    Next
                Next
                chkListWielkosc.Enabled = False
                wczytuje = False
            Else
                MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał listy wielkosci newslettera" & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
                Me.Close()
                Exit Sub
            End If

            gbZalacznik.Visible = False
        End If

        'uzytkownicy     
        If wsWynik.dane.Tables.Count > 3 Then
            dtUzytkownicy = wsWynik.dane.Tables(3).Copy()
            dvUzytkownicy = New DataView(dtUzytkownicy)
            dvUzytkownicy.RowFilter = filtrUzytkownicy()
            dgv.DataSource = dvUzytkownicy
            If dgv.Columns.Contains("USER_ID") Then
                dgv.Columns("USER_ID").Visible = False
            End If
        Else
            MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał listy użytkowników." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
            Me.Close()
            Exit Sub
        End If

        If intIdNewsleter > 0 Then
            'user zaznaczanie
            If wsWynik.dane.Tables.Count > 8 Then
                wczytuje = True
                For Each rowUser As DataRow In wsWynik.dane.Tables(8).Rows
                    For i As Integer = 0 To dgv.Rows.Count - 1
                        If dgv.Rows(i).Cells("NAZWA").Value = rowUser.Item("NAZWA") Then
                            dgv.Rows(i).Cells("WYBIERZ").Value = 1
                            Exit For
                        End If
                    Next
                Next
                dgv.Enabled = False
                wczytuje = False
            Else
                MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał listy wielkosci newslettera" & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
                Me.Close()
                Exit Sub
            End If
        Else 'jestem w trybie edycji nowego newsletera na rozpoczecie zaznaczam wszystkie
            For Each row As DataGridViewRow In dgv.Rows
                CType(row.Cells("WYBIERZ"), DataGridViewCheckBoxCell).Value = True
            Next
        End If
        wyslijEnabled()
        'btnWyslij.Enabled = False
        btnAnuluj.Enabled = True
        'stanFiltrow = String.Empty
    End Sub

    Private Sub wyslij()
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        'ws.Url = frmGlowna.strWebservice
        Dim wsWynik As wsCursorProf.NewsleterWyslijWynik

        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            Dim xml_grupy As String = String.Empty
            'Dim grupa_id As Integer
            For i As Integer = 0 To chkListGrupy.Items.Count - 1
                If chkListGrupy.GetItemCheckState(i) = CheckState.Checked Then
                    Dim rows() As DataRow = dtGrupy.Select(String.Format("NAZWA = '{0}'", chkListGrupy.Items(i).ToString))
                    If rows.Length = 1 Then
                        xml_grupy = xml_grupy + "<row grupa_id = """ + CType(rows(0).Item("Grupa_id"), Integer).ToString + """/>"
                    End If

                End If
            Next
            Dim xml_typy As String = String.Empty
            For i As Integer = 0 To chkListTyp.Items.Count - 1
                If chkListTyp.GetItemCheckState(i) = CheckState.Checked Then
                    Dim rows() As DataRow = dtTypy.Select(String.Format("NAZWA = '{0}'", chkListTyp.Items(i).ToString))
                    If rows.Length = 1 Then
                        xml_typy = xml_typy + "<row typ_id = """ + CType(rows(0).Item("ID_SLOWNIK_WARTOSCI"), Integer).ToString + """/>"
                    End If

                End If
            Next

            Dim xml_wielkosc As String = String.Empty
            For i As Integer = 0 To chkListWielkosc.Items.Count - 1
                If chkListWielkosc.GetItemCheckState(i) = CheckState.Checked Then
                    Dim rows() As DataRow = dtWielkosc.Select(String.Format("NAZWA = '{0}'", chkListWielkosc.Items(i).ToString))
                    If rows.Length = 1 Then
                        xml_wielkosc = xml_wielkosc + "<row wielkosc_id = """ + CType(rows(0).Item("ID_SLOWNIK_WARTOSCI"), Integer).ToString + """/>"
                    End If

                End If
            Next

            Dim xml_user As String = String.Empty
            For Each row As DataGridViewRow In dgv.Rows
                If row.Cells("Wybierz").Value = True Then
                    xml_user = xml_user + "<row user_id = """ + row.Cells("user_id").Value.ToString + """/>"
                End If
            Next
            Dim plik As String = ""
            If File.Exists(lblPlik.Text) Then
                plik = Path.GetFileName(lblPlik.Text)
            End If
            If xml_user <> String.Empty Then
                wsWynik = ws.NewsleterWyslij(frmGlowna.sesja, txtTytul.Text, rtxtWiadomosc.Text, xml_grupy, xml_typy, xml_wielkosc, xml_user, plik)
            Else
                MsgBox("Nie wybrano użytkowników", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
            Return
        End Try

        If wsWynik.status = -1 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, Me.Text)
        End If
        If wsWynik.status > -1 Then
            frmRodzic.OdswierzListy()
            Me.Close()
        End If
    End Sub

    Private Sub frmNewsletter_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        wczytaj()
    End Sub


    Private Sub tabWiadomosc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub


    Private Sub btnWyslij_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWyslij.Click
        If Not sciezka Is Nothing And File.Exists(sciezka) Then
            wyslijPlik()
        End If
        wyslij()
    End Sub



    Private Sub wyslijPlik()
        Dim fs As New FileStream(sciezka, FileMode.Open)
        Dim ba(fs.Length) As Byte
        fs.Read(ba, 0, fs.Length)
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik As wsCursorProf.ZapiszAtachmentWynik
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.ZapiszAtachment(ba, Path.GetFileName(sciezka))
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
            Return
        End Try

        If wsWynik.status = -1 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
    End Sub

    Private Sub btnAnuluj_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAnuluj.Click
        Me.Close()
    End Sub


    Private Sub btnPlik_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPlik.Click
        Dim ofd As New OpenFileDialog
        ofd.Multiselect = False
        If ofd.ShowDialog() = Windows.Forms.DialogResult.OK Then
            sciezka = ofd.FileName
            lblPlik.Text = sciezka
        End If
    End Sub

    Private Sub chkListGrupy_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles chkListGrupy.ItemCheck
        If wczytuje = False Then
            'If e.NewValue = CheckState.Checked Then
            '    wczytuje = True
            '    For i As Integer = 0 To chkListGrupy.Items.Count - 1
            '        chkListGrupy.SetItemChecked(i, False)
            '    Next
            '    wczytuje = False
            'End If
            'chkListGrupy.SetItemChecked(e.Index, e.NewValue)
            wyslijEnabled()
        End If
    End Sub

    Private Sub wyslijEnabled()
        If intIdNewsleter = -1 Then
            Dim userCheked As Boolean = False
            For Each row As DataGridViewRow In dgv.Rows
                If row.Cells("Wybierz").Value = True Then
                    userCheked = True
                    Exit For
                End If
            Next
            If userCheked And txtTytul.Text.Length > 0 And rtxtWiadomosc.Text.Length > 0 Then
                btnWyslij.Enabled = True
            Else
                btnWyslij.Enabled = False
            End If
        Else
            btnWyslij.Enabled = False
        End If
    End Sub

    Private Sub txtTytul_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTytul.Leave
        If wczytuje = False Then
            wyslijEnabled()
        End If
    End Sub

    Private Sub rtxtWiadomosc_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles rtxtWiadomosc.Leave
        If wczytuje = False Then
            wyslijEnabled()
        End If
    End Sub
    Private Function filtrUzytkownicy() As String
        Dim filtr As String = String.Empty
        If chkListGrupy.CheckedItems.Count > 0 Then
            filtr = filtr + "("
            For Each item As String In chkListGrupy.CheckedItems
                filtr = filtr & "GRUPA = '" & item & "' OR "
            Next
            'usuwam ostatni ot
            filtr = filtr.Substring(0, filtr.Length - 4)
            filtr = filtr + ")"
        Else
            filtr = filtr & "1=0"
        End If
        '
        

        If chkListTyp.CheckedItems.Count > 0 Then
            If filtr.Length > 0 Then
                filtr = filtr & " AND "
            End If
            filtr = filtr + "("
            For Each item As String In chkListTyp.CheckedItems
                filtr = filtr & "TYP = '" & item & "' OR "
            Next
            'usuwam ostatni or
            filtr = filtr.Substring(0, filtr.Length - 4)
            filtr = filtr + ")"
        Else
            filtr = filtr & " AND 1=0"
        End If
       

        If chkListWielkosc.CheckedItems.Count > 0 Then
            If filtr.Length > 0 Then
                filtr = filtr & " AND "
            End If
            filtr = filtr + "("
            For Each item As String In chkListWielkosc.CheckedItems
                filtr = filtr & "WIELKOSC = '" & item & "' OR "
            Next
            'usuwam ostatni or
            filtr = filtr.Substring(0, filtr.Length - 4)
            filtr = filtr + ")"
        Else
            filtr = filtr & " AND 1=0"
        End If
        

        If filtr.Length = 0 Then
            filtr = "1=0 "
        End If
        Return filtr
    End Function

    Private Sub chkListWielkosc_ItemCheck(sender As Object, e As System.Windows.Forms.ItemCheckEventArgs) Handles chkListWielkosc.ItemCheck
        If wczytuje = False Then
            'If e.NewValue = CheckState.Checked Then
            '    wczytuje = True
            '    For i As Integer = 0 To chkListWielkosc.Items.Count - 1
            '        chkListWielkosc.SetItemChecked(i, False)
            '    Next
            '    wczytuje = False
            'End If
            'chkListGrupy.SetItemChecked(e.Index, e.NewValue)
            wyslijEnabled()
        End If
    End Sub

    
    

    Private Sub chkListWielkosc_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles chkListWielkosc.MouseUp
        If wczytuje = False Then
            dvUzytkownicy.RowFilter = filtrUzytkownicy()
            For Each row As DataGridViewRow In dgv.Rows
                CType(row.Cells("WYBIERZ"), DataGridViewCheckBoxCell).Value = True
            Next
            wyslijEnabled()
        End If
    End Sub

    
    Private Sub chkListGrupy_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles chkListGrupy.MouseUp
        If wczytuje = False Then

            dvUzytkownicy.RowFilter = filtrUzytkownicy()
            For Each row As DataGridViewRow In dgv.Rows
                CType(row.Cells("WYBIERZ"), DataGridViewCheckBoxCell).Value = True
            Next
            wyslijEnabled()
        End If
    End Sub

    Private Sub chkListTyp_ItemCheck(sender As Object, e As System.Windows.Forms.ItemCheckEventArgs) Handles chkListTyp.ItemCheck
        If wczytuje = False Then
            'If e.NewValue = CheckState.Checked Then
            '    wczytuje = True
            '    For i As Integer = 0 To chkListTyp.Items.Count - 1
            '        chkListTyp.SetItemChecked(i, False)
            '    Next
            '    wczytuje = False
            'End If
            'chkListGrupy.SetItemChecked(e.Index, e.NewValue)
            wyslijEnabled()
        End If
    End Sub

    Private Sub chkListTyp_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles chkListTyp.MouseUp
        If wczytuje = False Then
            dvUzytkownicy.RowFilter = filtrUzytkownicy()
            For Each row As DataGridViewRow In dgv.Rows
                CType(row.Cells("WYBIERZ"), DataGridViewCheckBoxCell).Value = True
            Next
            wyslijEnabled()
        End If
    End Sub
End Class