Imports System.IO
Imports System.Threading
Imports System.Globalization
Imports Microsoft.Office.Interop

Public Class frmPobieraniePlikowExcelaZBazy
    Private dtDane As New DataTable
    Dim Parametry As New ZmienneGlobalne
    Private numerEkranu As Integer 'numer bieżącego ekranu
    Private iloscEkranow As Integer 'ilość ekranów przy bieżącym filtrze
    Private bReagujNaComboIloscNaStronie As Boolean = False


    Private Function wczytaj() As Boolean

        If dtpDataOd.Value > dtpDataDo.Value Then
            MsgBox("""Data od"" nie może być późniejsza od ""Data do"". Proszę poprawić daty", MsgBoxStyle.Exclamation, "Niepoprawny zakres dat")
            Return False
        End If

        Dim sortowanieKolumna As String = ""
        Dim sortowanieNarastajaco As Boolean = True
        Dim intIdWybranegoWiersza = Nothing
        Dim intNumerWybranejKolumny = Nothing

        'czy sortujemy po jakiejś kolumnie?
        For Each dgvKolumna As DataGridViewColumn In dgv.Columns
            If dgvKolumna.HeaderCell.SortGlyphDirection <> SortOrder.None Then
                sortowanieKolumna = dgvKolumna.HeaderText
                If dgvKolumna.HeaderCell.SortGlyphDirection = SortOrder.Ascending Then
                    sortowanieNarastajaco = True
                Else
                    sortowanieNarastajaco = False
                End If
                Exit For
            End If
        Next

        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik As wsCursorProf.PlikiExcelaZamowieniaWczytajWynik

        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.PlikiExcelaZamowieniaWczytaj(frmGlowna.sesja, dtpDataOd.Value.ToString("yyyy-MM-dd"), dtpDataDo.Value.ToString("yyyy-MM-dd"), _
                                                      txtFiltr.Text, sortowanieKolumna, sortowanieNarastajaco, _
                                                      txtNumerEkranu.Text, cmbIloscNaStronie.SelectedItem)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
            Return False
        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical)

        ElseIf wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation)
        End If
        'czyszczenie kontrolek przed wypełnieniem
        dgv.DataSource = Nothing
        dgv.Columns.Clear()
        dgv.Controls.Clear()
        If wsWynik.status = 0 Then
            If wsWynik.dane.Tables(0).Rows.Count > 0 Then
                dgv.DataSource = wsWynik.dane.Tables(0)
                If dgv.Columns.Contains("kolejnosc") Then dgv.Columns("kolejnosc").Visible = False
                If dgv.Columns.Contains("PLIK_ID") Then dgv.Columns("PLIK_ID").Visible = False

                For Each kolumna As DataGridViewColumn In dgv.Columns
                    kolumna.Width = 155
                    kolumna.SortMode = DataGridViewColumnSortMode.Programmatic
                    If kolumna.HeaderText = sortowanieKolumna Then
                        If sortowanieNarastajaco Then
                            kolumna.HeaderCell.SortGlyphDirection = SortOrder.Ascending
                        Else
                            kolumna.HeaderCell.SortGlyphDirection = SortOrder.Descending
                        End If
                    End If
                Next

                Me.Refresh()
                Dim dgvc As DataGridViewButtonCell
                dgv.Columns.Insert(0, New DataGridViewButtonColumn)
                dgv.Columns(0).Name = "Pobieranie plików"
                dgv.Columns("Pobieranie plików").Width = 100

                For Each row As DataGridViewRow In dgv.Rows
                    dgvc = DirectCast(row.Cells("Pobieranie plików"), DataGridViewButtonCell)
                    dgvc.Value = "Pobierz plik"
                    dgvc.Style.ForeColor = Color.Gray
                Next
            Else
                MsgBox(String.Format("W okresie od {0} do {1}, brak plików Excela z zamówieniami w bazie danych." & Environment.NewLine & "Spróbuj zmienić zakres dat.", dtpDataOd.Value.ToString("yyyy-MM-dd"), dtpDataDo.Value.ToString("yyyy-MM-dd")), MsgBoxStyle.Information, "Brak plików")
            End If
            If wsWynik.ilosc_stron > 1 Then
                iloscEkranow = wsWynik.ilosc_stron
            Else
                iloscEkranow = 1
            End If
            numerEkranu = txtNumerEkranu.Text
            lblIloscEkranow.Text = "z " & iloscEkranow
        Else
            Return False
        End If
        Return True
    End Function

    Private Sub btnWyszukaj_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWyszukaj.Click
        txtNumerEkranu.Text = 1
        wczytaj()
    End Sub

    Private Sub frmPobieraniePlikowExcelaZBazy_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        'inicjalizacja kontrolek
        dtpDataOd.CustomFormat = "yyyy-MM-dd"
        dtpDataDo.CustomFormat = "yyyy-MM-dd"
        dtpDataDo.Value = Now
        dtpDataOd.Value = DateAdd(DateInterval.Day, -30, Now)

        numerEkranu = 1
        txtNumerEkranu.Text = numerEkranu
        cmbIloscNaStronie.SelectedIndex = 1
        bReagujNaComboIloscNaStronie = True

        If Not wczytaj() Then
            Me.Close()
        End If

    End Sub

    Private Sub dgv_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellContentClick
        If e.RowIndex > -1 Then

            If e.ColumnIndex = 0 Then

                Dim plik_ext As String = ""
                Dim plik_id As Integer = 0
                plik_id = dgv.Rows(e.RowIndex).Cells("PLIK_ID").Value
                plik_ext = Path.GetExtension(dgv.Rows(e.RowIndex).Cells("NAZWA_PLIKU").Value)
                System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
                System.Net.ServicePointManager.Expect100Continue = False
                ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
                ws.Proxy.Credentials = CredentialCache.DefaultCredentials
                Dim wsWynik As New wsCursorProf.PlikZamowieniaExcelPobierzWynik

                Try
                    Cursor = Cursors.WaitCursor
                    Application.DoEvents()
                    wsWynik = ws.PlikZamowieniaExcelPobierz(frmGlowna.sesja, plik_id)
                    Cursor = Cursors.Default
                Catch ex As Exception
                    Cursor = Cursors.Default
                    MsgBox("Błąd komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
                End Try

                If wsWynik.status < 0 Then
                    MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical)
                ElseIf wsWynik.status > 0 Then
                    MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation)
                End If

                If wsWynik.status = 0 Then
                    Dim plik_excel As Byte() = CType(wsWynik.dane.Tables(0).Rows(0).Item("PLIK"), Byte())

                    Dim sfd As New SaveFileDialog
                    sfd.Filter = "Pliki programu Excel (" & plik_ext & ")|*" & plik_ext
                    sfd.RestoreDirectory = True
                    'sfd.DefaultExt = "xls"
                    sfd.Title = "Zapis pliku z zamówieniami"
                    sfd.FileName = "zamowienia_" & Parametry.PlikiZExcelaNazwa & "_" & CStr(dgv.Rows(e.RowIndex).Cells("PLIK_ID").Value) & ".xls"

                    If sfd.ShowDialog = DialogResult.OK Then
                        '' czy plik jest otwarty, jeśli tak to zamykamy go
                        Dim Process() As Process = System.Diagnostics.Process.GetProcessesByName("excel")
                        For Each p As Process In Process
                            If p.MainWindowTitle.Contains(System.IO.Path.GetFileName(sfd.FileName)) Then
                                MsgBox("Nie udało się pobrać pliku. Plik o podanej nazwie """ & sfd.FileName & _
                                       """ jest obecnie otwarty. " & vbNewLine & _
                                       "Proszę spróbować ponownie pobrać plik, zapisując go pod inną nazwą.", _
                                       MsgBoxStyle.Exclamation, "Plik o tej nazwie jest już otwarty")
                                Exit Sub
                            End If
                        Next
                        Dim fs As FileStream = New FileStream(sfd.FileName, FileMode.Create, FileAccess.Write)
                        fs.Write(plik_excel, 0, plik_excel.Length)
                        fs.Flush()
                        fs.Close()
                        fs.Dispose()

                        '' otwieramy plik użytkownikowi
                        'System.Diagnostics.Process.Start(sfd.FileName)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub dgv_ColumnHeaderMouseClick(sender As Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgv.ColumnHeaderMouseClick
        Dim sortowanieKolumna As String
        Dim sortowanieRosnaco As Boolean
        Dim kolumna As DataGridViewColumn
        Dim kolumnaKliknieta As DataGridViewColumn

        kolumnaKliknieta = dgv.Columns(e.ColumnIndex)
        sortowanieKolumna = kolumnaKliknieta.HeaderText

        For Each kolumna In dgv.Columns
            If kolumnaKliknieta.HeaderText <> kolumna.HeaderText Then kolumna.HeaderCell.SortGlyphDirection = SortOrder.None
        Next
        If kolumnaKliknieta.HeaderCell.SortGlyphDirection = SortOrder.Ascending Then
            kolumnaKliknieta.HeaderCell.SortGlyphDirection = SortOrder.Descending
            sortowanieRosnaco = False
        Else
            kolumnaKliknieta.HeaderCell.SortGlyphDirection = SortOrder.Ascending
            sortowanieRosnaco = True
        End If

        'wypełniamy grid od nowa
        txtNumerEkranu.Text = "1"
        wczytaj()

        'szukamy, czy w wyniku jest przed chwilą kliknięta kolumna (po nazwie)
        For Each kolumna In dgv.Columns
            If kolumna.HeaderText = sortowanieKolumna Then
                'jest - rysujemy jej "sorting glyph"
                If sortowanieRosnaco Then
                    kolumna.HeaderCell.SortGlyphDirection = SortOrder.Ascending
                Else
                    kolumna.HeaderCell.SortGlyphDirection = SortOrder.Descending
                End If
                Exit For
            End If
        Next
    End Sub

    Private Sub dgv_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgv.Sorted
        Dim dgvc As DataGridViewButtonCell
        For Each row As DataGridViewRow In dgv.Rows
            dgvc = DirectCast(row.Cells("Pobieranie plików"), DataGridViewButtonCell)
            dgvc.Value = "Pobierz plik"
            dgvc.Style.ForeColor = Color.Gray

        Next
    End Sub

    Private Sub txtFiltr_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtFiltr.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtNumerEkranu.Text = 1
            wczytaj()
        End If
    End Sub

    Private Sub btnOdswiez_Click(sender As System.Object, e As System.EventArgs) Handles btnOdswiez.Click
        txtNumerEkranu.Text = 1
        wczytaj()
    End Sub

    Private Sub txtNumerEkranu_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtNumerEkranu.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim intNumerEkranu As Integer
            If Not Integer.TryParse(txtNumerEkranu.Text, intNumerEkranu) Then
                MsgBox("Numer ekranu musi być liczbą", MsgBoxStyle.Critical, "Niepoprawna wartość")
                Return
            End If
            If intNumerEkranu = numerEkranu Then Return 'użytkownik nie zmienił numeru ekranu, nic nie robimy
            wczytaj()
        End If
        If e.KeyCode = Keys.Escape Then
            txtNumerEkranu.Text = numerEkranu
        End If
    End Sub

    Private Sub txtNumerEkranu_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtNumerEkranu.Validating
        Dim intNumerEkranu As Integer
        If Not Integer.TryParse(txtNumerEkranu.Text, intNumerEkranu) Then
            MsgBox("Numer ekranu musi być liczbą", MsgBoxStyle.Critical, "Niepoprawna wartość")
            e.Cancel = True
            Return
        End If
        If txtNumerEkranu.Text <> numerEkranu Then
            MsgBox("Jeżeli chcesz przejść na wpisany ekran, naciśnij Enter, jeśli chcesz wyjść z tego pola - naciśnij Escape", MsgBoxStyle.Exclamation, Me.Text)
            e.Cancel = True
            Return
        End If
    End Sub

    Private Sub btnNastepny_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNastepny.Click
        If txtNumerEkranu.Text >= iloscEkranow Then
            MsgBox("To jest ostatni ekran. Nie możesz przejść do następnego ekranu.", MsgBoxStyle.Exclamation, "Ostatni ekran")
            Return
        End If
        txtNumerEkranu.Text = txtNumerEkranu.Text + 1
        wczytaj()
    End Sub

    Private Sub btnOstatni_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOstatni.Click
        If txtNumerEkranu.Text = iloscEkranow Then
            MsgBox("To jest ostatni ekran.", MsgBoxStyle.Exclamation, "Ostatni ekran")
            Return
        End If
        txtNumerEkranu.Text = iloscEkranow
        wczytaj()
    End Sub

    Private Sub btnPoprzedni_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPoprzedni.Click
        If txtNumerEkranu.Text = 1 Then
            MsgBox("To jest pierwszy ekran. Nie możesz przejść do poprzedniego ekranu.", MsgBoxStyle.Exclamation, "Pierwszy ekran")
            Return
        End If
        txtNumerEkranu.Text = txtNumerEkranu.Text - 1
        wczytaj()
    End Sub

    Private Sub btnPoczatek_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPoczatek.Click
        If txtNumerEkranu.Text = 1 Then
            MsgBox("To jest pierwszy ekran.", MsgBoxStyle.Exclamation, "Pierwszy ekran")
            Return
        End If
        txtNumerEkranu.Text = 1
        wczytaj()
    End Sub

    Private Sub cmbIloscNaStronie_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbIloscNaStronie.SelectedIndexChanged
        If bReagujNaComboIloscNaStronie Then
            txtNumerEkranu.Text = 1
            wczytaj()
        End If
    End Sub

    Private Sub btnZamknij_Click(sender As System.Object, e As System.EventArgs) Handles btnZamknij.Click
        Me.Close()
    End Sub

    Private Sub btnAnuluj_Click(sender As System.Object, e As System.EventArgs) Handles btnAnuluj.Click
        Me.Close()
    End Sub
End Class