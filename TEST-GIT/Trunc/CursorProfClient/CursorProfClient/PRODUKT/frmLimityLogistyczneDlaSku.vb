Imports System.Reflection
Imports System
Public Class frmLimityLogistyczneDlaSku
    Private bFormShown As Boolean = False
    Private dtLimityProduktow As DataTable
    Private kolumnaSortowanie As String

    Private Sub frmSkuLimity_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If bylyZmiany() Then
            Dim odp As MsgBoxResult = MsgBox("Od ostatniego odczytu z serwera wprowadzono zmiany w limitach produktów. Czy zapisać wprowadzone zmiany?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNoCancel + MsgBoxStyle.DefaultButton1, "Zmiany w limitach produktów")
            If odp = MsgBoxResult.Cancel Then Exit Sub
            If odp = MsgBoxResult.Yes Then
                dgv.EndEdit()
                If Not zapisz() Then Exit Sub
            End If
        End If
    End Sub

    Private Sub frmPodzialNesForce_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        Me.Text = "Limity logistyczne dla produktów"
        If Not wczytaj() Then Me.Close()
        bFormShown = True
    End Sub

    Private Function wczytaj() As Boolean

       System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
System.Net.ServicePointManager.Expect100Continue = False
ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik As wsCursorProf.ProduktyLimitLogistycznyListaWczytajWynik

        Dim sortowanieKolumna As String = ""
        Dim sortowanieNarastajaco As Boolean = True
        Dim intIdWybranegoWiersza = Nothing
        Dim intNumerWybranejKolumny = Nothing

        'odczyt listy z serwera
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.ProduktyLimitLogistycznyListaWczytaj(frmGlowna.sesja)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem. " & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, "Odczytanie limitów produktów")
            Return False
        End Try
        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, "Odczytanie limitów produktów")
            Return False
        ElseIf wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, "Odczytanie limitów produktów")
        End If

        'czyszczenie kontrolek przed wypełnieniem
        dgv.DataSource = Nothing

        'wypełnienie kontrolek wynikami
        If wsWynik.dane.Tables.Count > 0 Then

            If Not (wsWynik.dane.Tables(0).Columns.Contains("sku_id") AndAlso _
                    wsWynik.dane.Tables(0).Columns.Contains("limit") AndAlso _
                     wsWynik.dane.Tables(0).Columns.Contains("wartosc_oryginalna")) Then
                MsgBox("Błąd wewnętrzny systemu. W przysłanej liście z limitami produktów brakuje wszystkich kolumn!" & frmGlowna.kontaktIt, MsgBoxStyle.Critical, "Odczytanie limitów produktów")
                Return False
            End If

            dtLimityProduktow = wsWynik.dane.Tables(0)

            If wsWynik.dane.Tables(0).Rows.Count < 1 Then
                MsgBox("Błąd wewnętrzny systemu. Brak produktów w systemie!" & frmGlowna.kontaktIt, MsgBoxStyle.Exclamation, "Odczytanie limitów produktów")
                Return False
            End If

            dgv.DataSource = dtLimityProduktow

            For Each c As DataGridViewColumn In dgv.Columns
                If c.Name = "sku" Then
                    c.Width = 184
                ElseIf c.Name = "nazwa" Then
                    c.Width = 410
                ElseIf c.Name = "limit" Then
                    c.Width = 100
                End If
            Next

            Dim dgvStyl As New DataGridViewCellStyle
            dgvStyl.BackColor = Color.LightGray
            dgvStyl.Alignment = DataGridViewContentAlignment.MiddleCenter

            For Each kolumna As DataGridViewColumn In dgv.Columns
                kolumna.SortMode = DataGridViewColumnSortMode.Programmatic
                If kolumna.Name = "limit" Then
                    kolumna.ReadOnly = False
                    kolumna.DefaultCellStyle = dgvStyl
                    kolumna.Width = 80
                Else
                    kolumna.ReadOnly = True
                End If
            Next

            '' ukrycie niektórych kolumn
            If dgv.Columns.Contains("sku_id") Then dgv.Columns("sku_id").Visible = False
            If dgv.Columns.Contains("wartosc_oryginalna") Then dgv.Columns("wartosc_oryginalna").Visible = False

            Filtrowanie(IIf(kolumnaSortowanie Is Nothing, "sku", kolumnaSortowanie))

        Else
            MsgBox("Błąd wewnętrzny systemu. System nie przesłał tabeli z limitami produktów." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, "Odczytanie limitów produktów")
            Return False
        End If

        Return True
    End Function

    Private Sub dgv_CellEndEdit(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellEndEdit
        BindingContext(dgv.DataSource).EndCurrentEdit()


    End Sub

    Private Sub dgv_CellFormatting(sender As Object, e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles dgv.CellFormatting

        Dim r_dgv As DataGridViewRow = dgv.Rows(e.RowIndex)

        If r_dgv Is Nothing Then Return

        If CInt(r_dgv.Cells("limit").Value) = CInt(r_dgv.Cells("wartosc_oryginalna").Value) Then
            For Each c As DataGridViewColumn In dgv.Columns
                If c.Name = "limit" Then
                    r_dgv.Cells(c.Name).Style.BackColor = Color.LightGray
                Else
                    r_dgv.Cells(c.Name).Style.BackColor = Color.White
                End If
            Next
        Else   'jeśli wpisano limit różny od wartości oryginalnej - kolorujemy na żółto
            For Each c As DataGridViewColumn In dgv.Columns
                r_dgv.Cells(c.Name).Style.BackColor = System.Drawing.Color.FromArgb(255, 255, 195)
            Next
        End If

    End Sub

    Private Sub dgv_CellValidating(sender As Object, e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles dgv.CellValidating
        If dgv.Columns(e.ColumnIndex).Name = "limit" Then
            Dim strWpisywane As String
            If IsDBNull(e.FormattedValue) OrElse e.FormattedValue = "" Then
                strWpisywane = "0"
            Else
                strWpisywane = e.FormattedValue
            End If

            Dim intIlosc As Integer
            If Not Integer.TryParse(strWpisywane, intIlosc) Then
                MsgBox("Podaj liczbę całkowitą.", MsgBoxStyle.Exclamation, "Niepoprawny limit dla produktu")
                e.Cancel = True
                Exit Sub
            End If

            If intIlosc < 0 Then
                MsgBox("Limit nie może być wartością ujemną!", MsgBoxStyle.Exclamation, "Niepoprawny limit dla produktu")
                e.Cancel = True
                Exit Sub
            End If

            Dim intIloscPrzedWpisaniem As Integer = IIf(IsDBNull(dgv.Rows(e.RowIndex).Cells(e.ColumnIndex).Value), 0, dgv.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
            If CInt(strWpisywane) = intIloscPrzedWpisaniem Then Exit Sub

            '' jeśli wpisano limit taki jak wartość oryginalna - nie kolorujemy wiersza
            If CInt(strWpisywane) = CInt(dgv.Rows(e.RowIndex).Cells("wartosc_oryginalna").Value) Then
                For Each c As DataGridViewColumn In dgv.Columns
                    If c.Name = "limit" Then
                        dgv.Rows(e.RowIndex).Cells(c.Name).Style.BackColor = Color.LightGray
                    Else
                        dgv.Rows(e.RowIndex).Cells(c.Name).Style.BackColor = Color.White
                    End If
                Next
            Else  '' a jeśli wpisano limit różny od wartości oryginalnej - kolorujemy na żółto
                For Each c As DataGridViewColumn In dgv.Columns
                    dgv.Rows(e.RowIndex).Cells(c.Name).Style.BackColor = System.Drawing.Color.FromArgb(255, 255, 195)
                Next
            End If

        End If

    End Sub

    Private Sub btnOdswiez_Click(sender As System.Object, e As System.EventArgs) Handles btnOdswiez.Click
        If bylyZmiany() Then
            Dim odp As MsgBoxResult = MsgBox("Od ostatniego odczytu z serwera wprowadzono zmiany w limitach produktów. Czy zapisać wprowadzone zmiany?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNoCancel + MsgBoxStyle.DefaultButton1, "Zmiany w limitach produktów")
            If odp = MsgBoxResult.Cancel Then Exit Sub
            If odp = MsgBoxResult.Yes Then
                dgv.EndEdit()
                If Not zapisz() Then Exit Sub
            End If
        End If
        wczytaj()
    End Sub

    Private Function bylyZmiany() As Boolean
        Dim bBylyZmiany As Boolean = False
        For Each dgvRow As DataGridViewRow In dgv.Rows
            If Not IsDBNull(dgvRow.Cells("limit").Value) AndAlso dgvRow.Cells("limit").Value <> dgvRow.Cells("wartosc_oryginalna").Value Then
                bBylyZmiany = True
                Exit For
            End If
        Next
        Return bBylyZmiany
    End Function

    Private Function zapisz() As Boolean
       System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
System.Net.ServicePointManager.Expect100Continue = False
ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik As wsCursorProf.ProduktyLimitLogistycznyZapiszWynik

        'zebranie limitów z gridu
        Dim dt As New DataTable
        dt.Columns.Add("sku_id")
        dt.Columns.Add("limit")

        For Each dgvRow As DataGridViewRow In dgv.Rows
            If Not IsDBNull(dgvRow.Cells("limit").Value) AndAlso _
                dgvRow.Cells("limit").Value <> dgvRow.Cells("wartosc_oryginalna").Value Then
                dt.Rows.Add(dgvRow.Cells("sku_id").Value, dgvRow.Cells("limit").Value)
            End If
        Next
        If dt.Rows.Count < 1 Then
            MsgBox("Nie wprowadzono zmian w limitach, aktualizacja serwera nie jest konieczna.", MsgBoxStyle.Exclamation, "Zapis limitu dla produktów")
            Return True
        End If

        Dim ds As New DataSet
        ds.Tables.Add(dt)
        ds.Tables(0).TableName = "dt"

        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.ProduktyLimitLogistycznyZapisz(frmGlowna.sesja, ds)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem. " & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, "Zapis limitu dla produktów")
            Return False
        End Try

        Dim tytul_komunikatu As String = "Zapis limitu dla produktów"

        If wsWynik.status = 0 Then
            'wczytujemy dane o limitach
            wczytaj()
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Information, tytul_komunikatu)
        ElseIf wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, tytul_komunikatu)
            Return False
        ElseIf wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, tytul_komunikatu)
        End If

        Return True

    End Function

    Private Sub btnZapisz_Click(sender As System.Object, e As System.EventArgs) Handles btnZapisz.Click
        dgv.EndEdit()
        zapisz()
    End Sub

    Private Sub dgv_ColumnHeaderMouseClick(sender As Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgv.ColumnHeaderMouseClick
        kolumnaSortowanie = dgv.Columns(e.ColumnIndex).Name
        If dgv.Columns(e.ColumnIndex).HeaderCell.SortGlyphDirection = SortOrder.Ascending Then
            dgv.Columns(e.ColumnIndex).HeaderCell.SortGlyphDirection = SortOrder.Descending
        Else
            dgv.Columns(e.ColumnIndex).HeaderCell.SortGlyphDirection = SortOrder.Ascending
        End If
        Filtrowanie(kolumnaSortowanie)
    End Sub


    Private Sub Filtrowanie(ByVal kolumnaKliknietaNazwa As String)
        Dim sortowanieKolumna As String
        Dim sortowanieRosnaco As Boolean
        Dim kolumna As DataGridViewColumn
        Dim kolumnaKliknieta As DataGridViewColumn

        kolumnaKliknieta = dgv.Columns(kolumnaKliknietaNazwa)
        sortowanieKolumna = kolumnaKliknieta.Name

        For Each kolumna In dgv.Columns
            If kolumnaKliknieta.HeaderText <> kolumna.HeaderText Then kolumna.HeaderCell.SortGlyphDirection = SortOrder.None
        Next
        If kolumnaKliknieta.HeaderCell.SortGlyphDirection = SortOrder.Descending Then
            sortowanieRosnaco = False
        Else
            sortowanieRosnaco = True
        End If

        Dim filtr As String = ""
        For Each c As DataGridViewColumn In dgv.Columns
            If c.Name <> "sku_id" Then
                If filtr = "" Then
                    filtr = "CONVERT([" & c.Name & "] ,'System.String') LIKE '%" & txtFiltr.Text & "%'"
                Else
                    filtr += " OR CONVERT([" & c.Name & "] ,'System.String') LIKE '%" & txtFiltr.Text & "%'"
                End If
            End If
        Next

        '' dodajemy do filtra wiersze w, których użytkownik zmienił limit
        filtr += " OR CONVERT([limit] ,'System.String') <> CONVERT([wartosc_oryginalna] ,'System.String')"

        Dim sortowanie As String
        If sortowanieRosnaco Then
            sortowanie = "[" & kolumnaKliknieta.Name & "] ASC"
        Else
            sortowanie = "[" & kolumnaKliknieta.Name & "] DESC"
        End If

        Dim view As New DataView(dtLimityProduktow)
        view.RowFilter = filtr
        view.Sort = sortowanie

        dgv.DataSource = Nothing
        dgv.DataSource = view

        For Each c As DataGridViewColumn In dgv.Columns
            If c.Name = "sku" Then
                c.Width = 184
            ElseIf c.Name = "nazwa" Then
                c.Width = 410
            ElseIf c.Name = "limit" Then
                c.Width = 100
            End If
        Next

        Dim dgvStyl As New DataGridViewCellStyle
        dgvStyl.BackColor = Color.LightGray
        dgvStyl.Alignment = DataGridViewContentAlignment.MiddleCenter

        For Each kolumna In dgv.Columns
            '' ustawiamy sortowanie tylko z poziomu kodu aplikacji
            kolumna.SortMode = DataGridViewColumnSortMode.Programmatic

            'szukamy, czy w wyniku jest przed chwilą kliknięta kolumna (po nazwie)
            If kolumna.Name = sortowanieKolumna Then
                'jest - rysujemy jej "sorting glyph"
                If sortowanieRosnaco Then
                    kolumna.HeaderCell.SortGlyphDirection = SortOrder.Ascending
                Else
                    kolumna.HeaderCell.SortGlyphDirection = SortOrder.Descending
                End If
                If dgv.Rows.Count > 1 Then
                    dgv.CurrentCell = dgv.Rows(1).Cells(kolumna.Index)
                    dgv.CurrentCell = Nothing
                End If
            End If

            '' ustawienie szerokości kolumn, nazw nagłówków, stylu
            If kolumna.Name = "limit" Then
                kolumna.ReadOnly = False
                kolumna.DefaultCellStyle = dgvStyl
                kolumna.Width = 80
            Else
                kolumna.ReadOnly = True
            End If
        Next



        '' ukrycie niektórych kolumn
        If dgv.Columns.Contains("sku_id") Then dgv.Columns("sku_id").Visible = False
        If dgv.Columns.Contains("wartosc_oryginalna") Then dgv.Columns("wartosc_oryginalna").Visible = False

    End Sub

    Private Sub btnFiltruj_Click(sender As System.Object, e As System.EventArgs) Handles btnFiltruj.Click
        Filtrowanie(IIf(kolumnaSortowanie Is Nothing, "sku", kolumnaSortowanie))
    End Sub

    Private Sub txtFiltr_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtFiltr.KeyDown
        If e.KeyCode = Keys.Enter Then
            Filtrowanie(IIf(kolumnaSortowanie Is Nothing, "sku", kolumnaSortowanie))
        End If
    End Sub

    Private Sub btnZamknij_Click(sender As System.Object, e As System.EventArgs) Handles btnZamknij.Click
        Me.Close()
    End Sub

    Private Sub btnAnuluj_Click(sender As System.Object, e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub btnWyczyscFiltry_Click(sender As System.Object, e As System.EventArgs) Handles btnWyczyscFiltry.Click
        txtFiltr.Text = ""
        Filtrowanie(IIf(kolumnaSortowanie Is Nothing, "sku", kolumnaSortowanie))
    End Sub
End Class