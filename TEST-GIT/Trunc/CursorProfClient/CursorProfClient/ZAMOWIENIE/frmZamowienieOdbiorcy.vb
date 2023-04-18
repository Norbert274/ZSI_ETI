Public Class frmZamowienieOdbiorcy

    Private dtGrupy As DataTable
    Private dtUzytkownicy As DataTable
    Private dvUzytkownicy As DataView
    Private dtTypy As DataTable
    Private dtWielkosc As DataTable
    Private wczytuje As Boolean = False
    Public intIdZamowienie As Integer
    Public frmRodzic As frmZamowienie
    Public uzytkownicy_ids As String
    Public grupy As String
    Public wielkosc As String
    Public typy As String
    Public uzytkownicy_cnt As Integer
    Private grupa_cheked As String = String.Empty
    Private typ_cheked As String = String.Empty
    Private wielkosc_cheked As String = String.Empty
    Private cheked_state As CheckState
    Public InEdit As Boolean = False
    Public warunek As String
    Private ustawiamLicznik As Boolean = False
    Private alGrupy As New ArrayList
    Private alWielkosc As New ArrayList
    Private alTypy As New ArrayList
    Private iloscFiltrowWlaczonych As Integer
    Private iloscFiltrowWypelnionych As Integer
    Private bWczytano As Boolean = False

    Private Sub wczytaj()
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        'ws.Url = frmGlowna.strWebservice
        Dim wsWynik As wsCursorProf.ZamowienieOdbiorcyOdczytajWynik

        Dim data As DateTime = DateTime.Now
        Try
            Cursor = Cursors.WaitCursor
            wsWynik = ws.ZamowienieOdbiorcyOdczytaj(frmGlowna.sesja, intIdZamowienie)
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
        
        If wsWynik.dane.Tables.Count > 8 Then
            If warunek <> "AND" And warunek <> "OR" Then
                warunek = wsWynik.dane.Tables(8).Rows(0).Item(0).ToString
            End If
            If warunek = "AND" Then
                rbAnd.Checked = True
            End If
            If warunek = "OR" Then
                rbOr.Checked = True
            End If
        Else
            MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał warunku." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
            Me.Close()
            Exit Sub
        End If
        wczytuje = True
        'GRUPY  
        If wsWynik.dane.Tables.Count > 0 Then

            dtGrupy = wsWynik.dane.Tables(0).Copy()
            chkListGrupy.Items.Clear()

            For Each row As DataRow In dtGrupy.Rows
                chkListGrupy.Items.Add(row.Item("NAZWA").ToString, False)
            Next

        Else
            MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał listy grup." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
            Me.Close()
            Exit Sub
        End If
        If grupy <> String.Empty Then
            For i As Integer = 0 To chkListGrupy.Items.Count - 1
                If grupy.IndexOf("<row grupa = """ & chkListGrupy.Items(i) & """/>") > -1 Then
                    chkListGrupy.SetItemChecked(i, True)
                End If
            Next
        End If
        'typ     
        If wsWynik.dane.Tables.Count > 1 Then

            dtTypy = wsWynik.dane.Tables(1).Copy()
            chkListTyp.Items.Clear()

            For Each row As DataRow In dtTypy.Rows
                chkListTyp.Items.Add(row.Item("NAZWA").ToString, False)
            Next

        Else
            MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał listy typów." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
            Me.Close()
            Exit Sub
        End If
        If typy <> String.Empty Then
            For i As Integer = 0 To chkListTyp.Items.Count - 1
                If typy.IndexOf("<row typ = """ & chkListTyp.Items(i) & """/>") > -1 Then
                    chkListTyp.SetItemChecked(i, True)
                End If
            Next
        End If
        'wielkosc     
        If wsWynik.dane.Tables.Count > 2 Then

            dtWielkosc = wsWynik.dane.Tables(2).Copy()
            chkListWielkosc.Items.Clear()

            For Each row As DataRow In dtWielkosc.Rows
                chkListWielkosc.Items.Add(row.Item("NAZWA").ToString, False)
            Next

        Else
            MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał listy wielkości." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
            Me.Close()
            Exit Sub
        End If

        If wielkosc <> String.Empty Then
            For i As Integer = 0 To chkListWielkosc.Items.Count - 1
                If wielkosc.IndexOf("<row wielkosc = """ & chkListWielkosc.Items(i) & """/>") > -1 Then
                    chkListWielkosc.SetItemChecked(i, True)
                End If
            Next
        End If


        If intIdZamowienie > 0 Then


            'grupy zaznaczanie
            If wsWynik.dane.Tables.Count > 4 Then
                wczytuje = True
                For Each rowGrupa As DataRow In wsWynik.dane.Tables(4).Rows
                    For i As Integer = 0 To chkListGrupy.Items.Count - 1
                        If chkListGrupy.GetItemText(chkListGrupy.Items(i)) = rowGrupa.Item("NAZWA") Then
                            chkListGrupy.SetItemCheckState(i, CheckState.Checked)
                            Exit For
                        End If
                    Next
                Next
                'chkListGrupy.Enabled = False
                wczytuje = False
            Else
                MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał listy grup odbiorców" & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
                Me.Close()
                Exit Sub
            End If

            'typ zaznaczanie
            If wsWynik.dane.Tables.Count > 5 Then
                wczytuje = True
                For Each rowTyp As DataRow In wsWynik.dane.Tables(5).Rows
                    For i As Integer = 0 To chkListTyp.Items.Count - 1
                        If chkListTyp.GetItemText(chkListTyp.Items(i)) = rowTyp.Item("NAZWA") Then
                            chkListTyp.SetItemCheckState(i, CheckState.Checked)
                            Exit For
                        End If
                    Next
                Next
                'chkListTyp.Enabled = False
                wczytuje = False
            Else
                MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał listy typu odbiorców" & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
                Me.Close()
                Exit Sub
            End If

            'wielkosc zaznaczanie
            If wsWynik.dane.Tables.Count > 6 Then
                wczytuje = True
                For Each rowWielkosc As DataRow In wsWynik.dane.Tables(6).Rows
                    For i As Integer = 0 To chkListWielkosc.Items.Count - 1
                        If chkListWielkosc.GetItemText(chkListWielkosc.Items(i)) = rowWielkosc.Item("NAZWA") Then
                            chkListWielkosc.SetItemCheckState(i, CheckState.Checked)
                            Exit For
                        End If
                    Next
                Next
                'chkListWielkosc.Enabled = False
                wczytuje = False
            Else
                MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał listy wielkości odbiorców" & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
                Me.Close()
                Exit Sub
            End If

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
            If dgv.Columns.Contains("ADRES_ID") Then
                dgv.Columns("ADRES_ID").Visible = False
            End If
        Else
            MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał listy użytkowników." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
            Me.Close()
            Exit Sub
        End If
        Dim cnt As Integer = 0
        Dim ids As String = ""
        If uzytkownicy_ids <> String.Empty Then
            For i As Integer = 0 To dgv.Rows.Count - 1
                If uzytkownicy_ids.IndexOf("<row user_id = """ & dgv.Rows(i).Cells("USER_ID").Value & """/>") > -1 Then
                    dgv.Rows(i).Cells("WYBIERZ").Value = 1
                End If
            Next
        End If

        If intIdZamowienie > 0 Then
            'user zaznaczanie
            If wsWynik.dane.Tables.Count > 7 Then

                For Each rowUser As DataRow In wsWynik.dane.Tables(7).Rows
                    For i As Integer = 0 To dgv.Rows.Count - 1
                        If dgv.Rows(i).Cells("user_id").Value = rowUser.Item("user_id") Then
                            dgv.Rows(i).Cells("WYBIERZ").Value = 1
                            cnt = cnt + 1
                            ids = ids & CStr(rowUser.Item("user_id")) & ","
                            Exit For
                        End If
                    Next
                Next
                'dgv.Enabled = False
                CType(dgv.DataSource, DataView).Table.AcceptChanges()

            Else
                MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał listy odbiorców" & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
                Me.Close()
                Exit Sub
            End If
            'Else 'jestem w trybie edycji nowego zamówienia na rozpoczecie zaznaczam wszystkie
            '    For Each row As DataGridViewRow In dgv.Rows
            '        CType(row.Cells("WYBIERZ"), DataGridViewCheckBoxCell).Value = True
            '    Next
        End If
        wybierzEnabled()
        wczytuje = False
        For Each col As DataGridViewColumn In dgv.Columns
            If col.HeaderText = "WYBIERZ" And InEdit = True Then
                col.ReadOnly = False
            Else
                col.ReadOnly = True
            End If
        Next
        'btnWyslij.Enabled = False
        btnAnuluj.Enabled = True
        ustawLicznik()
        'stanFiltrow = String.Empty
    End Sub

    Private Sub wybierzEnabled()
        If InEdit = True Then
            Dim userCheked As Boolean = False
            For Each row As DataGridViewRow In dgv.Rows
                If row.Cells("Wybierz").Value = True Then
                    userCheked = True
                    Exit For
                End If
            Next
            If userCheked Then
                btnWybierz.Enabled = True
            Else
                btnWybierz.Enabled = False
            End If
            chkListGrupy.Enabled = chbGrupyUser.CheckState
            chkListTyp.Enabled = chbTypyUser.CheckState
            chkListWielkosc.Enabled = chbWielkoscUser.CheckState
            dgv.Columns("WYBIERZ").ReadOnly = False

            chkAll.Enabled = True
            rbAnd.Enabled = True
            rbOr.Enabled = True
        Else
            btnWybierz.Enabled = False
            chkListGrupy.Enabled = chbGrupyUser.CheckState
            chkListTyp.Enabled = chbTypyUser.CheckState
            chkListWielkosc.Enabled = chbWielkoscUser.CheckState
            dgv.Columns("WYBIERZ").ReadOnly = True
            chkAll.Enabled = False
            rbAnd.Enabled = False
            rbOr.Enabled = False
        End If
    End Sub

    Private Function filtrUzytkownicy() As String
        iloscFiltrowWypelnionych = 0

        Dim filtr As String = String.Empty
        Dim operat As String = ""
        If rbAnd.Checked = True Then
            operat = "AND"
        End If
        If rbOr.Checked = True Then
            operat = "OR"
        End If
        If chkListGrupy.CheckedItems.Count > 0 And chkListGrupy.Enabled = True Then
            iloscFiltrowWypelnionych += 1
            filtr = filtr & "("
            For Each item As String In chkListGrupy.CheckedItems
                filtr = filtr & "GRUPA = '" & item & "' OR "
            Next
            'usuwam ostatni or
            filtr = filtr.Substring(0, filtr.Length - 4)
            filtr = filtr & ")"
        End If

        If chkListTyp.CheckedItems.Count > 0 And chkListTyp.Enabled = True Then
            iloscFiltrowWypelnionych += 1
            If filtr.Length > 0 Then
                filtr = filtr & " " & operat & " "
            End If
            filtr = filtr & "("
            For Each item As String In chkListTyp.CheckedItems
                filtr = filtr & "TYP = '" & item & "' OR "
            Next
            'usuwam ostatni or
            filtr = filtr.Substring(0, filtr.Length - 4)
            filtr = filtr & ")"
        End If

        If chkListWielkosc.CheckedItems.Count > 0 And chkListWielkosc.Enabled = True Then
            iloscFiltrowWypelnionych += 1
            If filtr.Length > 0 Then
                filtr = filtr & " " & operat & " "
            End If
            filtr = filtr & "("
            For Each item As String In chkListWielkosc.CheckedItems
                filtr = filtr & "WIELKOSC = '" & item & "' OR "
            Next
            'usuwam ostatni or
            filtr = filtr.Substring(0, filtr.Length - 4)
            filtr = filtr & ")"
        End If
        If rbAnd.Checked = True AndAlso iloscFiltrowWlaczonych > iloscFiltrowWypelnionych Then
            filtr = "1=0"
        End If

        If filtr.Length = 0 Then
            filtr = "1=0"
        End If
        Return filtr
    End Function

    Private Sub frmZamowienieOdbiorcy_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        chbGrupyUser.Checked = True
        chbTypyUser.Checked = True
        chbWielkoscUser.Checked = True
        iloscFiltrowWlaczonych = 3
        wczytaj()
        bWczytano = True
    End Sub

    Private Sub btnWybierz_Click(sender As System.Object, e As System.EventArgs) Handles btnWybierz.Click
        Dim brak_adresow As Boolean = False
        For Each row As DataGridViewRow In dgv.Rows
            If row.Cells("Wybierz").Value = True And row.Cells("adres_id").Value = -1 Then
                MsgBox("Dla odbiorcy " & row.Cells("NAZWA").Value & ", nie znaleziono adresu domyślnego." & vbNewLine & "Przejdź do edycji adresów użytkownika i w jednym z nich zaznacz pole 'Domyslny'", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
        Next


        uzytkownicy_ids = String.Empty
        uzytkownicy_cnt = 0
        For Each row As DataGridViewRow In dgv.Rows
            If row.Cells("Wybierz").Value = True Then
                uzytkownicy_ids = uzytkownicy_ids & "<row user_id = """ & row.Cells("USER_ID").Value & """/>"
                uzytkownicy_cnt = uzytkownicy_cnt + 1
            End If
        Next
        grupy = ""
        For i As Integer = 0 To chkListGrupy.Items.Count - 1
            If chkListGrupy.GetItemChecked(i) = True Then
                grupy = grupy & "<row grupa = """ & chkListGrupy.Items(i) & """/>"
            End If
        Next
        typy = ""
        For i As Integer = 0 To chkListTyp.Items.Count - 1
            If chkListTyp.GetItemChecked(i) = True Then
                typy = typy & "<row typ = """ & chkListTyp.Items(i) & """/>"
            End If
        Next
        wielkosc = ""
        For i As Integer = 0 To chkListWielkosc.Items.Count - 1
            If chkListWielkosc.GetItemChecked(i) = True Then
                wielkosc = wielkosc & "<row wielkosc = """ & chkListWielkosc.Items(i) & """/>"
            End If
        Next
        'If Len(uzytkownicy_ids) > 0 Then
        '    uzytkownicy_ids = uzytkownicy_ids.Substring(0, Len(uzytkownicy_ids) - 1)
        'End If
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnAnuluj_Click(sender As System.Object, e As System.EventArgs) Handles btnAnuluj.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub chkListGrupy_ItemCheck(sender As Object, e As System.Windows.Forms.ItemCheckEventArgs) Handles chkListGrupy.ItemCheck
        If InEdit = True Or wczytuje = True Then
            grupa_cheked = chkListGrupy.Items(e.Index)
            cheked_state = e.NewValue
        Else
            e.NewValue = e.CurrentValue
        End If

    End Sub

    

    Private Sub chkListGrupy_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles chkListGrupy.MouseUp
        If InEdit = True Then
            dvUzytkownicy.RowFilter = filtrUzytkownicy()
            If cheked_state = CheckState.Checked Then
                alGrupy.Add(grupa_cheked)
            Else
                alGrupy.Remove(grupa_cheked)
            End If

            For Each row As DataGridViewRow In dgv.Rows
                If alGrupy.Contains(row.Cells("grupa").Value) Or alTypy.Contains(row.Cells("TYP").Value) Or alWielkosc.Contains(row.Cells("WIELKOSC").Value) Then
                    row.Cells("WYBIERZ").Value = CheckState.Checked
                Else
                    row.Cells("WYBIERZ").Value = CheckState.Unchecked
                End If
            Next
            wybierzEnabled()
            ustawLicznik()
        End If
    End Sub

    Private Sub chkListTyp_ItemCheck(sender As Object, e As System.Windows.Forms.ItemCheckEventArgs) Handles chkListTyp.ItemCheck
        If InEdit = True Or wczytuje = True Then
            typ_cheked = chkListTyp.Items(e.Index)
            cheked_state = e.NewValue
        Else
            e.NewValue = e.CurrentValue
        End If

    End Sub

    Private Sub chkListTyp_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles chkListTyp.MouseUp
        If InEdit = True Then
            dvUzytkownicy.RowFilter = filtrUzytkownicy()
            If cheked_state = CheckState.Checked Then
                alTypy.Add(typ_cheked)
            Else
                alTypy.Remove(typ_cheked)
            End If

            For Each row As DataGridViewRow In dgv.Rows
                If alGrupy.Contains(row.Cells("grupa").Value) Or alTypy.Contains(row.Cells("TYP").Value) Or alWielkosc.Contains(row.Cells("WIELKOSC").Value) Then
                    row.Cells("WYBIERZ").Value = CheckState.Checked
                Else
                    row.Cells("WYBIERZ").Value = CheckState.Unchecked
                End If
            Next
            wybierzEnabled()
            ustawLicznik()
        End If
    End Sub

    Private Sub chkListWielkosc_ItemCheck(sender As Object, e As System.Windows.Forms.ItemCheckEventArgs) Handles chkListWielkosc.ItemCheck
        If InEdit = True Or wczytuje = True Then
            wielkosc_cheked = chkListWielkosc.Items(e.Index)
            cheked_state = e.NewValue
        Else
            e.NewValue = e.CurrentValue
        End If

    End Sub

    Private Sub chkListWielkosc_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles chkListWielkosc.MouseUp
        If InEdit = True Then
            dvUzytkownicy.RowFilter = filtrUzytkownicy()
            If cheked_state = CheckState.Checked Then
                alWielkosc.Add(wielkosc_cheked)
            Else
                alWielkosc.Remove(wielkosc_cheked)
            End If

            For Each row As DataGridViewRow In dgv.Rows
                If alGrupy.Contains(row.Cells("grupa").Value) Or alTypy.Contains(row.Cells("TYP").Value) Or alWielkosc.Contains(row.Cells("WIELKOSC").Value) Then
                    row.Cells("WYBIERZ").Value = CheckState.Checked
                Else
                    row.Cells("WYBIERZ").Value = CheckState.Unchecked
                End If
            Next
            wybierzEnabled()
            ustawLicznik()
        End If
    End Sub

    Private Sub dgv_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles dgv.MouseUp
        If InEdit = True Then
            dgv.CommitEdit(DataGridViewDataErrorContexts.Commit)
            wybierzEnabled()
            ustawLicznik()
        End If
    End Sub

    

    Private Sub ustawLicznik()
        Dim zaznaczone As Integer = 0

        For Each row As DataGridViewRow In dgv.Rows
            If row.Cells("WYBIERZ").Value = True Then
                zaznaczone = zaznaczone + 1
            End If
        Next
        lbluzytkownicyLicznik.Text = String.Format("Wybrano {0} z {1}", zaznaczone, dgv.Rows.Count)
        ustawiamLicznik = True
        If zaznaczone = dgv.Rows.Count And chkAll.Checked = False Then
            chkAll.Checked = True
        End If
        If chkAll.Checked = True And zaznaczone <> dgv.Rows.Count Then
            chkAll.Checked = False
        End If
        ustawiamLicznik = False
    End Sub

    Private Sub chkAll_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkAll.CheckedChanged
        'If chkAll.Checked = False Then
        If ustawiamLicznik = False Then
            For Each row As DataGridViewRow In dgv.Rows
                row.Cells("WYBIERZ").Value = chkAll.CheckState
            Next
            'els()
            'End If
            wybierzEnabled()
            ustawLicznik()
        End If
        
    End Sub

    Private Sub rbOr_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbOr.CheckedChanged
        If rbOr.Checked = True And Not IsNothing(dvUzytkownicy) Then
            dvUzytkownicy.RowFilter = filtrUzytkownicy()
            chkAll.Checked = True
            wybierzEnabled()
            ustawLicznik()
            warunek = "OR"
        End If
    End Sub

    Private Sub rbAnd_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbAnd.CheckedChanged
        If rbAnd.Checked = True And Not IsNothing(dvUzytkownicy) Then
            dvUzytkownicy.RowFilter = filtrUzytkownicy()
            chkAll.Checked = True
            wybierzEnabled()
            ustawLicznik()
            warunek = "AND"
        End If
    End Sub

    Private Sub chbGrupyUser_CheckedChanged(sender As Object, e As System.EventArgs) Handles chbGrupyUser.CheckedChanged
        chkListGrupy.Enabled = chbGrupyUser.CheckState
        If chbGrupyUser.Checked = True Then
            iloscFiltrowWlaczonych += 1
        Else
            iloscFiltrowWlaczonych -= 1
        End If
        If bWczytano = True Then
            dvUzytkownicy.RowFilter = filtrUzytkownicy()
        End If
        ustawLicznik()
    End Sub

    Private Sub chbTypyUser_CheckedChanged(sender As Object, e As System.EventArgs) Handles chbTypyUser.CheckedChanged
        chkListTyp.Enabled = chbTypyUser.CheckState
        If chbTypyUser.Checked = True Then
            iloscFiltrowWlaczonych += 1
        Else
            iloscFiltrowWlaczonych -= 1
        End If
        If bWczytano = True Then
            dvUzytkownicy.RowFilter = filtrUzytkownicy()
        End If

        ustawLicznik()
    End Sub

    Private Sub chbWielkoscUser_CheckedChanged(sender As Object, e As System.EventArgs) Handles chbWielkoscUser.CheckedChanged
        chkListWielkosc.Enabled = chbWielkoscUser.CheckState
        If chbWielkoscUser.Checked = True Then
            iloscFiltrowWlaczonych += 1
        Else
            iloscFiltrowWlaczonych -= 1
        End If
        If bWczytano = True Then
            dvUzytkownicy.RowFilter = filtrUzytkownicy()
        End If
        ustawLicznik()
    End Sub
End Class