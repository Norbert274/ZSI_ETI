Imports System.IO
Imports OfficeOpenXml
Imports OfficeOpenXml.Style
Imports OfficeOpenXml.Utils

Public Class frmSlownik
    Private dtSlowniki As New DataTable
    Private dtWartosci As New DataTable
    Private kolorAktywnosci As Color = Color.DodgerBlue
    Private kolorBrakuAktywnosci As Color = Color.LightGray
    Private nazwaSzablonu As String
    Private sciezka As String

    Private Const conWartosc As String = "Wartosc"


    Private Sub wczytajSlowniki()
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik As wsCursorProf.SlownikListaWczytajWynik
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.SlownikListaWczytaj(frmGlowna.sesja)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
            Exit Sub
        End Try
        If wsWynik.status = -1 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, Me.Text)
            Exit Sub
        End If

        If wsWynik.dane.Tables.Count > 0 Then
            dtSlowniki = wsWynik.dane.Tables(0)
        Else
            MsgBox("Nie udało się pobrać słowników", MsgBoxStyle.Critical, Me.Text)
            Exit Sub
        End If

        cmbSlowniki.DataSource = dtSlowniki
        cmbSlowniki.DisplayMember = "NAZWA"
        cmbSlowniki.ValueMember = "ID_SLOWNIK"
        cmbSlowniki.SelectedValue = -1

        nazwaSzablonu = wsWynik.nazwaSzablonu
    End Sub

    Private Sub frmSlownik_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        wczytajSlowniki()
        btnNowy.Enabled = False
        btnNowy.BackColor = kolorBrakuAktywnosci
        btnUsun.Enabled = False
        btnUsun.BackColor = kolorBrakuAktywnosci
        btnEdytuj.Enabled = False
        btnEdytuj.BackColor = kolorBrakuAktywnosci
        btnFromExcel.Enabled = False
        btnFromExcel.BackColor = kolorBrakuAktywnosci
        btnToExcel.Enabled = False
        btnToExcel.BackColor = kolorBrakuAktywnosci
    End Sub

    Private Sub btnNowy_Click(sender As Object, e As System.EventArgs) Handles btnNowy.Click
        If cmbSlowniki.SelectedValue <> -1 Then
            Dim f As New frmSlownikPozycjaDodaj
            f.slownikId = cmbSlowniki.SelectedValue
            f.slownikNazwa = cmbSlowniki.Text
            f.bEdycja = False
            If f.ShowDialog = Windows.Forms.DialogResult.OK Then
                If edytujWartosci(cmbSlowniki.SelectedValue, "", f.txtWartosc.Text) = True Then
                    MsgBox("Poprawnie zapisano nową wartość.", MsgBoxStyle.Information, Me.Text)
                End If
            End If
            wczytajWartosci()
            czyMaWartosciDoEdycji()
        Else
            MsgBox("Nie wybrano słownika", MsgBoxStyle.Critical, Me.Text)
            Exit Sub
        End If
    End Sub

    Private Function edytujWartosci(ByVal slownikId As Integer, ByVal staraNazwa As String, ByVal nowaNazwa As String) As Boolean

        For Each row As DataGridViewRow In dgv.Rows
            If row.Cells(conWartosc).Value = nowaNazwa Then
                MsgBox("Istnieje już taka wartość w tym słowniku!", MsgBoxStyle.Critical, Me.Text)
                Return False
                Exit Function
            End If
        Next

        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik As wsCursorProf.SlownikWartoscEdytujWynik
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.SlownikWartoscEdytuj(frmGlowna.sesja, slownikId, nowaNazwa, staraNazwa)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
            Return False
        End Try
        If wsWynik.status = -1 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, Me.Text)
            Return False
        End If

        Return True
    End Function

    Public Sub wczytajWartosci()
        If cmbSlowniki.SelectedValue <> -1 Then
            System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
            System.Net.ServicePointManager.Expect100Continue = False
            ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
            ws.Proxy.Credentials = CredentialCache.DefaultCredentials
            Dim wsWynik As wsCursorProf.SlownikWartoscWczytajWynik
            Try
                Cursor = Cursors.WaitCursor
                Application.DoEvents()
                wsWynik = ws.SlownikWartoscWczytaj(frmGlowna.sesja, cmbSlowniki.SelectedValue)
                Cursor = Cursors.Default
            Catch ex As Exception
                Cursor = Cursors.Default
                MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
                Exit Sub
            End Try
            If wsWynik.status = -1 Then
                MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, Me.Text)
                Exit Sub
            End If

            If wsWynik.dane.Tables.Count > 0 Then
                dtWartosci = wsWynik.dane.Tables(0)
            Else
                MsgBox("Nie udało się pobrać wartości", MsgBoxStyle.Critical, Me.Text)
                Exit Sub
            End If

            dgv.DataSource = dtWartosci

            For Each col As DataGridViewColumn In dgv.Columns
                If col.Name <> conWartosc Then
                    col.Visible = False
                End If
            Next

            If dtWartosci.Rows.Count = 0 Then
                btnToExcel.Enabled = False
                btnToExcel.BackColor = kolorBrakuAktywnosci
            Else
                btnToExcel.Enabled = True
                btnToExcel.BackColor = kolorAktywnosci
            End If
        Else
            MsgBox("Nie wybrano słownika", MsgBoxStyle.Critical, Me.Text)
            Exit Sub
        End If
    End Sub

    Private Function czyMoznaEdytowac() As Boolean
        If cmbSlowniki.SelectedValue <> -1 Then
            For Each row As DataRow In dtSlowniki.Select("ID_SLOWNIK = " & cmbSlowniki.SelectedValue)
                If row.Item("czy_edytowalna") = 1 Then
                    Return True
                End If
            Next
        End If
        Return False
    End Function

    Private Sub cmbSlowniki_SelectionChangeCommitted(sender As Object, e As System.EventArgs) Handles cmbSlowniki.SelectionChangeCommitted
        If cmbSlowniki.SelectedValue <> -1 Then
            wczytajWartosci()
            czyMaWartosciDoEdycji()

            If czyMoznaEdytowac() = True Then
                btnNowy.Enabled = True
                btnNowy.BackColor = kolorAktywnosci
                btnFromExcel.Enabled = True
                btnFromExcel.BackColor = kolorAktywnosci
            Else
                btnNowy.Enabled = False
                btnNowy.BackColor = kolorBrakuAktywnosci
                btnFromExcel.Enabled = False
                btnFromExcel.BackColor = kolorBrakuAktywnosci
            End If
          
        End If
    End Sub

    Private Sub czyMaWartosciDoEdycji()
        If dtWartosci.Rows.Count > 0 AndAlso czyMoznaEdytowac() = True Then
            btnEdytuj.Enabled = True
            btnEdytuj.BackColor = kolorAktywnosci
            btnUsun.Enabled = True
            btnUsun.BackColor = kolorAktywnosci
        Else
            btnEdytuj.Enabled = False
            btnEdytuj.BackColor = kolorBrakuAktywnosci
            btnUsun.Enabled = False
            btnUsun.BackColor = kolorBrakuAktywnosci
        End If
    End Sub

    Private Sub btnUsun_Click(sender As Object, e As System.EventArgs) Handles btnUsun.Click
        If cmbSlowniki.SelectedValue <> -1 AndAlso dgv.CurrentCell.Value <> Nothing Then

            If MessageBox.Show("Czy na pewno chcesz usunąć wartość: " & dgv.CurrentCell.Value, "Potwierdź zmiany", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
                System.Net.ServicePointManager.Expect100Continue = False
                ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
                ws.Proxy.Credentials = CredentialCache.DefaultCredentials
                Dim wsWynik As wsCursorProf.SlownikWartoscUsunWynik
                Try
                    Cursor = Cursors.WaitCursor
                    Application.DoEvents()
                    wsWynik = ws.SlownikWartoscUsun(frmGlowna.sesja, cmbSlowniki.SelectedValue, dgv.CurrentCell.Value)
                    Cursor = Cursors.Default
                Catch ex As Exception
                    Cursor = Cursors.Default
                    MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
                    Exit Sub
                End Try
                If wsWynik.status = -1 Then
                    MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, Me.Text)
                    Exit Sub
                End If
            End If
            wczytajWartosci()
            czyMaWartosciDoEdycji()
        Else
            MsgBox("Nie wybrano słownika lub wartości do usunięcia", MsgBoxStyle.Critical, Me.Text)
            Exit Sub
        End If
    End Sub

    Private Sub btnEdytuj_Click(sender As Object, e As System.EventArgs) Handles btnEdytuj.Click
        If cmbSlowniki.SelectedValue <> -1 AndAlso dgv.CurrentCell.Value <> Nothing Then

            Dim f As New frmSlownikPozycjaDodaj
            f.slownikId = cmbSlowniki.SelectedValue
            f.slownikNazwa = cmbSlowniki.Text
            f.wartoscNazwa = dgv.CurrentCell.Value
            f.bEdycja = True

            If f.ShowDialog = Windows.Forms.DialogResult.OK Then
                If edytujWartosci(cmbSlowniki.SelectedValue, dgv.CurrentCell.Value, f.txtWartosc.Text) = True Then
                    MsgBox("Poprawnie zapisano nową wartość.", MsgBoxStyle.Information, Me.Text)
                End If
            End If

            wczytajWartosci()
            czyMaWartosciDoEdycji()
        Else
            MsgBox("Nie wybrano słownika lub wartości do edycji", MsgBoxStyle.Critical, Me.Text)
            Exit Sub
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
            Dim extension As String = ".xlsx"
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

    Private Sub btnSzablon_Click(sender As Object, e As System.EventArgs) Handles btnSzablon.Click
        PobierzPlik(nazwaSzablonu)
    End Sub

    Private Sub ustawWartosciZExcela()
        Dim dtWartosci As New DataTable

        If dtWartosci.Rows.Count > 0 Then
            dtWartosci.Rows.Clear()
        End If

        '' Jeśli dtZamowienia ma już kolumny to usuwamy je
        If dtWartosci.Columns.Count > 0 Then
            dtWartosci.Columns.Clear()
        End If

        dtWartosci.Columns.Add(conWartosc)

        Dim ofdPlikXLS As New OpenFileDialog
        ofdPlikXLS.Filter = "Pliki Programu Excel (*.xlsx)|*.xlsx"

        If ofdPlikXLS.ShowDialog = Windows.Forms.DialogResult.OK Then
            sciezka = ofdPlikXLS.FileName
        End If

        Try
            Dim newFile As New FileInfo(sciezka)

            Using pck As New ExcelPackage(newFile)

                'ImportDataRecords(newFile)

                Dim wsDane As ExcelWorksheet = pck.Workbook.Worksheets(1)
                Dim intIndexWiersza As Integer = 1

                Do Until CStr(wsDane.Cells("B3").Offset(intIndexWiersza, 0).Value) = ""
                    dtWartosci.Rows.Add()
                    dtWartosci.Rows(intIndexWiersza - 1).Item(0) = wsDane.Cells("B3").Offset(intIndexWiersza, 0).Value
                    intIndexWiersza += 1
                Loop
            End Using


            If dtWartosci.Rows.Count = 0 Then
                MsgBox("W excelu nie ma żadnych pozycji do skopiowania", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            Dim ds As New DataSet

            ds.Tables.Add(dtWartosci)

            System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
            System.Net.ServicePointManager.Expect100Continue = False
            ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
            ws.Proxy.Credentials = CredentialCache.DefaultCredentials
            Dim wsWynik = New wsCursorProf.SlownikWartoscKilkaDodajWynik

            'pobieranie pliku przez usera
            Try
                Cursor = Cursors.WaitCursor
                Application.DoEvents()
                wsWynik = ws.SlownikWartoscKilkaDodaj(frmGlowna.sesja, cmbSlowniki.SelectedValue, ds)
                Cursor = Cursors.Default
            Catch ex As Exception
                Cursor = Cursors.Default
                MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, "Pobieranie pliku")
                Exit Sub
            End Try
            If wsWynik.status = -1 Then
                MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, "Pobieranie pliku")
                Exit Sub
            End If

            If wsWynik.iloscDodana > 0 Then
                MsgBox("Poprawnie dodano " & wsWynik.iloscDodana & " wartości", MsgBoxStyle.Information, Me.Text)
            Else
                MsgBox("Nie dodano żadnej nowej wartości", MsgBoxStyle.Information, Me.Text)
            End If


        Catch ex As Exception
            Cursor = Cursors.Default
            If ex.Message.Contains("because it is being used by another process") Then
                MsgBox("Plik o nazwie " & sciezka & _
                       " jest otwarty! Proszę zamknąć ten plik i spróbowac ponownie go wczytać.", _
                       MsgBoxStyle.Exclamation)
            Else
                MsgBox("Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, "Wystąpił wyjątek")
            End If
            Exit Sub
        End Try
    End Sub

    Private Sub btnFromExcel_Click(sender As Object, e As System.EventArgs) Handles btnFromExcel.Click
        ustawWartosciZExcela()
        wczytajWartosci()
    End Sub

    Private Sub DumpExcel(ByVal tbl As DataTable, ByVal filePath As String, ByVal tytul As String)

        Try
            If File.Exists(filePath) Then
                File.Delete(filePath)
            End If
            Dim newFile As New FileInfo(filePath)

            Using pck As New ExcelPackage(newFile)

                'Create the worksheet

                pck.Workbook.Properties.Author = ""
                pck.Workbook.Properties.Title = tytul
                pck.Workbook.Properties.Company = "OEX E-Business"

                Dim ws As ExcelWorksheet = pck.Workbook.Worksheets.Add("Wartości_do_slownika")
                Dim ile_kolumn As Integer = 1
                Dim i As Integer = 1

                'Load the datatable into the sheet, starting from cell A1. Print the column names on row 1
                ws.Cells("A1").Value = tytul
                ws.Cells("A1").Style.Font.Bold = True
                ws.Cells("A1").Style.Font.Size = 14
                ws.Cells("B3").LoadFromDataTable(tbl, True)

                For i = 1 To ile_kolumn
                    If i = 0 Then
                        ws.Column(i + 1).Width = 18
                    Else
                        ws.Column(i + 1).AutoFit()
                    End If
                Next

                With ws.Cells(3, 2, 3, ile_kolumn + 1).Style
                    .Fill.PatternType = ExcelFillStyle.Solid
                    .Fill.BackgroundColor.SetColor(Color.SteelBlue)
                    .VerticalAlignment = ExcelVerticalAlignment.Center
                    .HorizontalAlignment = ExcelHorizontalAlignment.Center
                    .Font.Color.SetColor(Color.White)
                    .Font.Bold = True
                End With

                ' zapisanie wyniku do pliku 

                pck.Save()
                MsgBox("Zapisano wartości: " & filePath, MsgBoxStyle.Information, "Wartości do słownika")

            End Using

        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem. " & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, "Wartości do słownika")
            Exit Sub
        End Try
    End Sub

    Private Sub btnToExcel_Click(sender As Object, e As System.EventArgs) Handles btnToExcel.Click
        If cmbSlowniki.SelectedValue <> -1 Then
            Dim sfd As New SaveFileDialog
            sfd.Filter = "Skoroszyt programu Excel|*.xlsx"
            sfd.FileName = "Wartości_do_słownika_" & ZamienNiedozwoloneZnaki(cmbSlowniki.Text)

            Dim dtExcel As New DataTable
            dtExcel.Columns.Add("Wartość")

            For Each row As DataRow In dtWartosci.Rows
                dtExcel.Rows.Add(row.Item("Wartosc"))
            Next


            If sfd.ShowDialog = Windows.Forms.DialogResult.OK Then
                DumpExcel(dtExcel, sfd.FileName, "Wartości do słownika " & cmbSlowniki.Text)
            End If
        End If

    End Sub

    Private Sub btnAnuluj_Click(sender As Object, e As System.EventArgs) Handles btnZamknij.Click
        Me.Close()
    End Sub

    Private Function ZamienNiedozwoloneZnaki(ByVal tekst As String) As String
        Return tekst.Replace(" ", "_").Replace("/", "_").Replace("\", "_").Replace("?", "_").Replace(":", "_").Replace("*", "_").Replace("""", "_").Replace(">", "_").Replace("<", "_")
    End Function
End Class