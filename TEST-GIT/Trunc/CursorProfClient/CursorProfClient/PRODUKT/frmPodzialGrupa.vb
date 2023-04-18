Imports System.Reflection
Imports DevExpress.XtraTreeList.Columns
Imports System.Collections.Generic
Imports System.Linq
Public Class frmPodzialGrupa
    Public frmRodzic As Form
    Public intIdMagazynu As Integer = -1
    Public strMagazyn As String = ""

    Public dtSku As DataTable
    Private dtPrzydzialy As DataTable
    Private bBylyZmiany As Boolean = False
    Public dtStany As New DataTable

    Private dtPodwladni As DataTable


    Private Const CONST_DOSTEPNE As String = "DOSTĘPNE"
    Private Const CONST_NAZWA As String = "nazwa"
    Private Const CONST_GRUPA_ID As String = "grupa_id"
    Private Const CONST_NADRZEDNA_ID As String = "NADRZEDNA_ID"


    Private Sub frmPodzialGrupa_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If bylyZmiany() Then
            Dim odp As MsgBoxResult = MsgBox("Od ostatniego odczytu z serwera wprowadzono zmiany z podziałach. Czy zapisać wprowadzone zmiany?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNoCancel + MsgBoxStyle.DefaultButton1, "Zapis zmian")
            If odp = MsgBoxResult.Cancel Then
                e.Cancel = True
                Exit Sub
            End If
            If odp = MsgBoxResult.Yes Then
                If Not zapisz() Then
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        End If
    End Sub

    Private Sub frmPodzial_Shown(ByVal sender As Object, ByVal e As System.EventArgs)
        'wczytanie zawartości drzewka i gridu
        Me.Text = "Podział towaru w magazynie " & strMagazyn
        If Not wczytaj() Then Me.Close()
    End Sub

    Private Sub btnZapisz_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnZapisz.Click
        zapisz()
    End Sub

    Private Function wczytaj() As Boolean
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        'ws.Url = frmGlowna.strWebservice
        Dim wsWynik As wsCursorProf.PodzialGrupaOdczytajWynik
        Dim sortowanieKolumna As String = ""
        Dim sortowanieNarastajaco As Boolean = True
        Dim intIdWybranegoWiersza = Nothing
        Dim intNumerWybranejKolumny = Nothing


        Dim ds As New DataSet
        ds.Tables.Add(dtSku)
        'odczyt listy z serwera
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.PodzialGrupaOdczytaj(frmGlowna.sesja, intIdMagazynu, ds)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem. " & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, "Odczyt podziałów")
            Return False
        End Try
        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, "Odczyt podziałów")
            Return False
        ElseIf wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, "Odczyt podziałów")
        End If

        'wypełnienie kontrolek wynikami
        If wsWynik.dane.Tables.Count > 0 Then

            'wypełnienie gridu - przygotowanie listy podwładnych
            If Not (wsWynik.dane.Tables(0).Columns.Contains(CONST_GRUPA_ID) AndAlso wsWynik.dane.Tables(0).Columns.Contains(CONST_NAZWA) AndAlso wsWynik.dane.Tables(0).Columns.Contains(CONST_NADRZEDNA_ID)) Then
                MsgBox("Błąd wewnętrzny systemu. W przysłanej liście podwładnych brakuje kolumny grupa_id, nazwa lub nadrzedna_id." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, "Brak potrzebnych danych")
                Return False
            End If

            dtPodwladni = wsWynik.dane.Tables(0).Clone

            If wsWynik.dane.Tables(0).Rows.Count < 1 Then
                MsgBox("Błąd wewnętrzny systemu. W przysłanej liście podwładnych brakuje wiersza ""Dostępne""." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, "Brak potrzebnych danych")
                Return False
            End If

            Dim bPierwszy As Boolean = True
            For Each dtRow As DataRow In wsWynik.dane.Tables(0).Rows
                Dim newRow As DataRow = dtPodwladni.NewRow
                If bPierwszy Then
                    newRow.ItemArray = dtRow.ItemArray
                    newRow.Item(CONST_NAZWA) = CONST_DOSTEPNE
                    dtPodwladni.Rows.Add(newRow)
                    bPierwszy = False
                Else
                    newRow(CONST_GRUPA_ID) = dtRow(CONST_GRUPA_ID)
                    newRow(CONST_NAZWA) = dtRow(CONST_NAZWA)
                    newRow(CONST_NADRZEDNA_ID) = dtRow(CONST_NADRZEDNA_ID)
                    dtPodwladni.Rows.Add(newRow)
                End If
            Next

            treeListPodzial.DataSource = Nothing

            dtPrzydzialy = wsWynik.dane.Tables(0).Copy
            dtStany = wsWynik.dane.Tables(1).Copy
            treeListPodzial.KeyFieldName = CONST_GRUPA_ID
            treeListPodzial.ParentFieldName = CONST_NADRZEDNA_ID
            treeListPodzial.DataSource = dtPodwladni
            treeListPodzial.CheckAll()
            treeListPodzial.ExpandAll()
            treeListPodzial.OptionsBehavior.EnableFiltering = True
            treeListPodzial.OptionsFilter.FilterMode = DevExpress.XtraTreeList.FilterMode.Smart
            treeListPodzial.OptionsMenu.EnableColumnMenu = False
            treeListPodzial.OptionsMenu.EnableFooterMenu = False

            For Each tCol As TreeListColumn In treeListPodzial.Columns
                tCol.OptionsColumn.AllowSort = False
                tCol.OptionsColumn.FixedWidth = True

                tCol.Width = 200
                Select Case tCol.FieldName
                    Case CONST_GRUPA_ID
                        tCol.OptionsColumn.AllowEdit = False
                        tCol.OptionsColumn.ReadOnly = True
                    Case CONST_NADRZEDNA_ID
                        tCol.OptionsColumn.AllowEdit = False
                        tCol.OptionsColumn.ReadOnly = True
                    Case CONST_NAZWA
                        tCol.OptionsColumn.AllowEdit = False
                        tCol.OptionsColumn.ReadOnly = True
                    Case Else
                        tCol.Width = 150
                        tCol.MinWidth = 150
                End Select

            Next

        Else
            MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał listy podwładnych." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, "Brak potrzebnych danych")
            Return False
        End If
        ds.Tables.Clear()

        Return True
    End Function

    Private Function bylyZmiany() As Boolean

        Dim bBylyZmiany As Boolean = False

        Try



            For i As Integer = 0 To dtPodwladni.Rows.Count - 1
                'wiersz 1 = dostepne którego nie liczę, ale jako że nie jestem pewien sortowania to If
                If Not dtPodwladni.Rows(i)(CONST_NAZWA) = CONST_DOSTEPNE Then
                    For Each dCol As DataColumn In dtPodwladni.Columns
                        Select Case dCol.ColumnName
                            Case CONST_NAZWA
                                Exit Select
                            Case CONST_GRUPA_ID
                                Exit Select
                            Case CONST_NADRZEDNA_ID
                                Exit Select
                            Case Else 'czyli SKU
                                If Not IsDBNull(dtPodwladni.Rows(i)(dCol.ColumnName)) AndAlso dCol.ColumnName.ToString.Length > 0 AndAlso Not dtPodwladni.Rows(i)(dCol.ColumnName) = 0 Then
                                    bBylyZmiany = True
                                    Return bBylyZmiany
                                End If
                        End Select
                    Next
                End If

            Next

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return bBylyZmiany
    End Function

    Private Function zapisz() As Boolean
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        'ws.Url = frmGlowna.strWebservice
        Dim wsWynik As wsCursorProf.PodzialGrupaZapiszWynik

        'zebranie ilości z gridu
        Dim dt As New DataTable
        dt.Columns.Add("grupa_id")
        dt.Columns.Add("sku")
        'dt.Columns.Add("sku_nazwa")
        dt.Columns.Add("przydzial")


        For Each dRow As DataRow In dtPodwladni.Rows
            For Each dCol As DataColumn In dtPodwladni.Columns
                Select Case dCol.ColumnName
                    Case CONST_NAZWA
                        Exit Select
                    Case CONST_GRUPA_ID
                        Exit Select
                    Case CONST_NADRZEDNA_ID
                        Exit Select
                    Case Else 'czyli SKU
                        If Not IsDBNull(dRow(dCol)) AndAlso NZ(dRow(dCol), 0) <> 0 AndAlso Not dRow(CONST_GRUPA_ID) = 0 Then
                            Dim zapisRow As DataRow = dt.NewRow
                            zapisRow("grupa_id") = dRow(CONST_GRUPA_ID)
                            zapisRow("sku") = dCol.ColumnName
                            zapisRow("przydzial") = dRow(dCol)
                            dt.Rows.Add(zapisRow)
                        End If
                End Select
            Next
        Next
        If dt.Rows.Count < 1 Then
            MsgBox("Nie wprowadzono zmian w przydziałach, aktualizacja serwera nie jest konieczna.", MsgBoxStyle.Exclamation, "Aktualizacja zmian")
            Return True
        End If

        'Dim ds As New DataSet
        'ds.Tables.Add(dt)

        Dim ds As New DataSet
        ds.Tables.Add(dt)
        ds.Tables(0).TableName = "dt"
        If Not dtStany.DataSet Is Nothing Then dtStany.DataSet.Tables.Clear()
        ds.Tables.Add(dtStany)
        ds.Tables(1).TableName = "dtStany"

        dt.Columns.Add("id_grupy_towarowe")
        dt.Columns.Add("akcja_id")

        'For Each dr As DataRow In dt.Rows
        '    For Each drStany As DataRow In dtStany.Rows
        '        If dr.Item("sku") = drStany.Item("naglowek") Then
        '            dr.Item("id_grupy_towarowe") = drStany.Item("id_grupy_towarowe")
        '            dr.Item("akcja_id") = drStany.Item("akcja_id")
        '        End If
        '    Next
        'Next

        'zapis podziału na serwer
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.PodzialGrupaZapisz(frmGlowna.sesja, intIdMagazynu, ds)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem. " & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, "Zapis podziału towaru")
            Return False
        End Try
        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, "Zapis podziału towaru")
            Return False
        ElseIf wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, "Zapis podziału towaru")
        End If

        frmGlowna.lblStatus.Text = "Pomyślnie zapisano podział towaru w magazynie " & strMagazyn & "."
        frmGlowna.timer.Interval = 3000 'komunikat zniknie po 3s
        frmGlowna.timer.Start()

        'odświeżamy grid rodzica (tylko jeśli jego okno prezentuje metodę "odswiezListy")
        If Not frmRodzic Is Nothing Then
            Dim m As MethodInfo() = frmRodzic.GetType.GetMethods()
            For licznik As Integer = 0 To m.GetUpperBound(0)
                If m(licznik).Name = "odswiezListy" Then
                    m(licznik).Invoke(frmRodzic, Nothing)
                End If
            Next
        End If

        'wczytujemy nowe ilości dostępne
        Return wczytaj()

    End Function

#Region "Handlery formy"


    Private Sub treeListPodzial_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles treeListPodzial.KeyDown
        Try
            Dim tv As DevExpress.XtraTreeList.TreeList = DirectCast(sender, DevExpress.XtraTreeList.TreeList)
            If tv.FocusedNode Is Nothing Then Exit Sub
            If tv.FocusedColumn.FieldName = CONST_GRUPA_ID OrElse tv.FocusedColumn.FieldName = CONST_NADRZEDNA_ID OrElse tv.FocusedColumn.FieldName = CONST_NAZWA Then Exit Sub

            If e.KeyCode = Keys.Delete Then
                'poprawimy podsumowanie
                tv.Nodes(0).Item(tv.FocusedColumn.FieldName) += IIf(IsDBNull(tv.FocusedNode.Item(tv.FocusedColumn.FieldName)), 0, tv.FocusedNode.Item(tv.FocusedColumn.FieldName))
                'aktualizujemy komórkę
                tv.FocusedNode.SetValue(tv.FocusedColumn.FieldName, DBNull.Value)
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub treeListPodzial_ValidatingEditor(sender As Object, e As DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs) Handles treeListPodzial.ValidatingEditor


        Dim tv As DevExpress.XtraTreeList.TreeList = DirectCast(sender, DevExpress.XtraTreeList.TreeList)
        Try

            If Not tv.FocusedNode.Item(CONST_GRUPA_ID) = tv.Nodes(0).Item(CONST_GRUPA_ID) Then
                Select Case tv.FocusedColumn.FieldName
                    Case CONST_GRUPA_ID
                    Case CONST_NADRZEDNA_ID
                    Case CONST_NAZWA
                    Case Else

                        Dim strWpisywane As String = NZ(e.Value, NZ(tv.FocusedNode.Item(tv.FocusedColumn.FieldName), 0))
                        If IsDBNull(strWpisywane) OrElse strWpisywane = "" Then strWpisywane = 0

                        Dim intIlosc As Integer
                        If Not Integer.TryParse(strWpisywane, intIlosc) Then
                            e.Valid = False
                            e.ErrorText = "Podaj liczbę całkowitą."
                            Return
                        End If

                        Dim intPoprzedniaZmianaO As Integer = NZ(tv.FocusedNode.Item(tv.FocusedColumn.FieldName), 0)
                        Dim intStanDostepne As Integer = tv.Nodes(0).Item(tv.FocusedColumn.FieldName)

                        If CInt(strWpisywane) = intPoprzedniaZmianaO Then Return

                        'dajemy, czy zabieramy?
                        If CInt(strWpisywane) > intPoprzedniaZmianaO Then
                            'dajemy
                            'czy chcemy przydzilić więcej niż mamy?
                            Dim intDajemy = CInt(strWpisywane) - intPoprzedniaZmianaO
                            If intDajemy > intStanDostepne Then
                                e.Valid = False
                                e.ErrorText = "Nie możesz przydzilić tyle towaru. Pozostało dostępne tylko " & intStanDostepne & " sztuk."
                                Return
                            Else
                                Return
                            End If


                        Else
                            'zabieramy
                            'czy chcieliśmy odebrać więcej niż ma nasz podwładny?
                            Dim intZabieramy = intPoprzedniaZmianaO - CInt(strWpisywane)
                            Dim qry As String = String.Format("[{0}]='{1}'", CONST_GRUPA_ID, tv.FocusedNode.Item(CONST_GRUPA_ID))
                            Dim intGrupaObecnaIlosc = NZ(dtPrzydzialy.Select(qry).FirstOrDefault()(tv.FocusedColumn.FieldName), 0)
                            If intZabieramy > (intGrupaObecnaIlosc + intPoprzedniaZmianaO) Then
                                e.Valid = False
                                e.ErrorText = "Nie możesz odebrać tyle towaru. Grupa ma tylko " & intGrupaObecnaIlosc & " sztuk."
                                Return
                            Else
                                Return
                            End If

                        End If
                End Select
            Else
                e.Valid = True
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub treeListPodzial_CellValueChanged(sender As Object, e As DevExpress.XtraTreeList.CellValueChangedEventArgs) Handles treeListPodzial.CellValueChanged

        Dim tv As DevExpress.XtraTreeList.TreeList = DirectCast(sender, DevExpress.XtraTreeList.TreeList)
        Try

            If Not tv.FocusedNode.Item(CONST_GRUPA_ID) = tv.Nodes(0).Item(CONST_GRUPA_ID) Then
                Select Case tv.FocusedColumn.FieldName
                    Case CONST_GRUPA_ID
                    Case CONST_NADRZEDNA_ID
                    Case CONST_NAZWA
                    Case Else

                        Dim strWpisywane As String = e.Value
                        If IsDBNull(strWpisywane) OrElse strWpisywane = "" Then strWpisywane = 0

                        Dim intIlosc As Integer
                        If Not Integer.TryParse(strWpisywane, intIlosc) Then
                            Return
                        End If

                        Dim intPoprzedniaZmianaO As Integer = NZ(tv.ActiveEditor.OldEditValue, 0)
                        Dim intStanDostepne As Integer = tv.Nodes(0).Item(tv.FocusedColumn.FieldName)

                        If CInt(strWpisywane) = intPoprzedniaZmianaO Then Return

                        'dajemy, czy zabieramy?
                        If CInt(strWpisywane) > intPoprzedniaZmianaO Then
                            'dajemy
                            'czy chcemy przydzilić więcej niż mamy?
                            Dim intDajemy = CInt(strWpisywane) - intPoprzedniaZmianaO
                            If intDajemy > intStanDostepne Then
                                Return
                            Else
                                tv.Nodes(0).Item(tv.FocusedColumn.FieldName) -= intDajemy
                                Return
                            End If


                        Else
                            'zabieramy
                            'czy chcieliśmy odebrać więcej niż ma nasz podwładny?
                            Dim intZabieramy = intPoprzedniaZmianaO - CInt(strWpisywane)
                            Dim qry As String = String.Format("[{0}]='{1}'", CONST_GRUPA_ID, tv.FocusedNode.Item(CONST_GRUPA_ID))
                            Dim intGrupaObecnaIlosc = NZ(dtPrzydzialy.Select(qry).FirstOrDefault()(tv.FocusedColumn.FieldName), 0)
                            If intZabieramy > (intGrupaObecnaIlosc + intPoprzedniaZmianaO) Then
                                Return
                            Else
                                Dim obecnieDostepne As Integer = tv.Nodes(0).GetValue(tv.FocusedColumn.FieldName)
                                dtPodwladni.Rows(0)(tv.FocusedColumn.FieldName) = obecnieDostepne + intZabieramy
                                Return
                            End If

                        End If
                End Select

            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub treeListPodzial_CustomDrawNodeCell(sender As Object, e As DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs) Handles treeListPodzial.CustomDrawNodeCell
        Try


            If e.Column.FieldName = CONST_GRUPA_ID Then Return
            If e.Column.FieldName = CONST_NAZWA Then Return
            If e.Column.FieldName = CONST_NADRZEDNA_ID Then Return
            If e.Node.Item(CONST_NAZWA) = CONST_DOSTEPNE Then Return
            If e.Node.ToString = "DevExpress.XtraTreeList.Nodes.TreeListAutoFilterNode" Then Return

            'narysuj to co zwykle
            'e.Painter.DrawObject() ' Paint(e.ClipBounds, e.PaintParts)
            e.Graphics.DrawString(e.CellText, e.Appearance.Font, Brushes.Black, e.Bounds.X, e.Bounds.Y)

            'potem dorysuj nasz tekst
            If e.CellText.ToString.Length <= 5 OrElse (e.CellText.ToString.StartsWith("-") AndAlso e.CellText.ToString.Length <= 6) Then
                Dim qry As String = String.Format("[{0}]='{1}'", CONST_GRUPA_ID, e.Node.Item(CONST_GRUPA_ID))

                Dim dRow As DataRow = dtPrzydzialy.Select(qry).FirstOrDefault()


                Dim val As String = ""
                Try
                    val = NZ(dRow(e.Column.FieldName).ToString, "0")
                Catch ex As Exception
                    If 1 = 2 Then

                    End If

                End Try


                Dim valSize As SizeF = e.Graphics.MeasureString(val, e.Appearance.Font)
                Dim valStart As PointF = New PointF(e.Bounds.Width - valSize.Width - 4, 4)
                Dim valColor As Color = Color.Gray 'e.CellStyle.ForeColor
                'If (e.State And DataGridViewElementStates.Selected) = DataGridViewElementStates.Selected Then
                ' valColor = e.CellStyle.SelectionForeColor
                'End If
                Using brush As New SolidBrush(valColor)
                    e.Graphics.DrawString(val, e.Appearance.Font, brush, _
                       e.Bounds.X + valStart.X, _
                       e.Bounds.Y + valStart.Y)
                End Using
                e.Handled = True
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub btnOdswiez_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If bylyZmiany() Then
            Dim odp As MsgBoxResult = MsgBox("Od ostatniego odczytu z serwera wprowadzono zmiany z podziałach. Czy zapisać wprowadzone zmiany?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNoCancel + MsgBoxStyle.DefaultButton1, "Zapis zmian w podziałach")
            If odp = MsgBoxResult.Cancel Then Exit Sub
            If odp = MsgBoxResult.Yes Then
                If Not zapisz() Then Exit Sub
            End If
        End If
        wczytaj()
    End Sub

    Private Sub frmPodzialGrupa_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If Not wczytaj() Then Me.Close()
    End Sub


    Private Sub btnZamknij_Click(sender As System.Object, e As System.EventArgs) Handles btnZamknij.Click
        Me.Close()
    End Sub


    Private Sub treeListPodzial_ShownEditor(sender As Object, e As System.EventArgs) Handles treeListPodzial.ShownEditor
        Dim tv As DevExpress.XtraTreeList.TreeList = DirectCast(sender, DevExpress.XtraTreeList.TreeList)

        If Not tv.FocusedNode Is Nothing AndAlso Not tv.FocusedNode(CONST_NAZWA) Is Nothing AndAlso tv.FocusedNode(CONST_NAZWA).ToString = CONST_DOSTEPNE Then
            tv.ActiveEditor.Properties.ReadOnly = True
        End If

    End Sub

    Private Sub treeListPodzial_ShowingEditor(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles treeListPodzial.ShowingEditor
        Dim tv As DevExpress.XtraTreeList.TreeList = DirectCast(sender, DevExpress.XtraTreeList.TreeList)

        If Not tv.FocusedNode Is Nothing AndAlso Not tv.FocusedNode(CONST_NAZWA) Is Nothing AndAlso tv.FocusedNode(CONST_NAZWA).ToString = CONST_DOSTEPNE Then

            e.Cancel = True
        End If
    End Sub
#End Region

End Class