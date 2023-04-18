Imports System.Threading
Imports System.Globalization
Imports Microsoft.Office.Interop
Imports OfficeOpenXml
Imports OfficeOpenXml.Style
Imports OfficeOpenXml.Utils
Imports System.IO
Public Class frmRaportPodzialyZamowieniaPerUser
    Private dtDane As New DataTable
    Private dtSku As New DataTable
    Private dtNazwySku As New DataTable
    Private dtUsers As New DataTable
    Private dtGrupyObdzielane As New DataTable
    Private bReagujNaZmianeComboBox As Boolean = False

    Private Sub btnZamknij_Click(sender As System.Object, e As System.EventArgs) Handles btnZamknij.Click
        Me.Close()
    End Sub

    Private Sub btnWyszukaj_Click(sender As System.Object, e As System.EventArgs) Handles btnWyszukaj.Click
        generuj_raport()


    End Sub

    Private Sub frmRaportNadzialySkuGrupaObdzielana_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        If Not wczytaj() Then
            Me.Close()
        End If

        cmbUsers.DataSource = dtUsers
        cmbUsers.DisplayMember = "NAZWA"
        cmbUsers.ValueMember = "USER_ID"
        cmbUsers.SelectedValue = frmGlowna.intIdUzytkownikZalogowany
        generuj_raport()

    End Sub

    Private Function generuj_raport() As Boolean

        If cmbUsers.SelectedIndex = -1 Then
            MsgBox("Proszę wybrać użytkownika z listy rozwijanej!", MsgBoxStyle.Exclamation, "Brak użytkownika")
            Return False
            Exit Function
        End If

     

        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
        Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        'ws.Url = frmGlowna.strWebservice
        Dim wsWynik As wsCursorProf.RaportPodzialyZamowieniaPerUserWynik

        'odczyt z serwera
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.RaportPodzialyZamowieniaPerUser(frmGlowna.sesja, cmbUsers.SelectedValue)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
            Return False
        End Try

        If wsWynik.status = -1 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, Me.Text)
            Return False
        Else
            'kontrola wyników
            If wsWynik.dane.Tables.Count > 0 Then
                If wsWynik.dane.Tables(0).Rows.Count < 1 Then
                    MsgBox("Brak danych.", MsgBoxStyle.Exclamation, Me.Text)
                Else
                    dtDane = wsWynik.dane.Tables(0)
                    dgv.DataSource = Nothing
                    dgv.DataSource = dtDane
                    dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
                End If
            Else
                MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał tabeli z raportem." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
                Return False
                Exit Function
            End If
        End If


        Return True
    End Function

    Private Function wczytaj() As Boolean

        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
        Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        'ws.Url = frmGlowna.strWebservice
        Dim wsWynik As wsCursorProf.RaportPodzialySkuGrupaObdzielanaWczytajWynik

        'odczyt z serwera
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.RaportPodzialySkuGrupaObdzielanaWczytaj(frmGlowna.sesja)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
            Return False
        End Try

        If wsWynik.status = -1 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, Me.Text)
            Return False
        Else
            'kontrola wyników
            If wsWynik.dane.Tables.Count > 2 Then
                If wsWynik.dane.Tables(3).Rows.Count < 1 Then
                    MsgBox("Nie pobrano listy z użytkownikami.", MsgBoxStyle.Exclamation, Me.Text)
                    Return False
                Else
                    dtUsers = wsWynik.dane.Tables(3)
                End If
            Else
                MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał tabeli z nazwami użytkowników do listy rozwijanej." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
                Return False
                Exit Function
            End If
        End If


        Return True
    End Function

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

                Dim ws As ExcelWorksheet = pck.Workbook.Worksheets.Add("Raport")
                Dim ile_kolumn As Integer = tbl.Columns.Count
                Dim i As Integer = 1

                'Load the datatable into the sheet, starting from cell A1. Print the column names on row 1
                ws.Cells("A1").Value = tytul
                ws.Cells("A1").Style.Font.Bold = True
                ws.Cells("A1").Style.Font.Size = 14
                ws.Cells("A3").LoadFromDataTable(tbl, True)

                For i = 0 To ile_kolumn - 1
                    If tbl.Columns(i).ColumnName.ToUpper.Contains("DATA") Then
                        '' formatowanie kolumny w excelu na datę
                        ws.Column(i + 1).Style.Numberformat.Format = "YYYY-MM-DD"
                    End If
                    If i = 0 Then
                        ws.Column(i + 1).Width = 18
                    Else
                        ws.Column(i + 1).AutoFit()
                    End If
                Next

                With ws.Cells(3, 1, 3, ile_kolumn).Style
                    .Fill.PatternType = ExcelFillStyle.Solid
                    .Fill.BackgroundColor.SetColor(Color.SteelBlue)
                    .VerticalAlignment = ExcelVerticalAlignment.Center
                    .HorizontalAlignment = ExcelHorizontalAlignment.Center
                    .Font.Color.SetColor(Color.White)
                    .Font.Bold = True
                End With

                ' zapisanie wyniku do pliku 
                pck.Save()
                MsgBox("Zapisano raport: " & filePath, MsgBoxStyle.Information, Me.Text)

            End Using

        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem. " & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
            Exit Sub
        End Try
    End Sub

    Private Sub btnRaportExcel_Click(sender As System.Object, e As System.EventArgs) Handles btnRaportExcel.Click
        ' Changes the CurrentCulture of the current thread to pl-PL.
        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US", False)
        Console.WriteLine("CurrentCulture is now {0}.", CultureInfo.CurrentCulture.Name)

        ' Changes the CurrentUICulture of the current thread to pl-PL.
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US", False)
        Console.WriteLine("CurrentUICulture is now {0}.", CultureInfo.CurrentUICulture.Name)

        Dim sfd As New SaveFileDialog
        sfd.Filter = "Skoroszyt programu Excel|*.xlsx"
        sfd.FileName = Me.Text
        If sfd.ShowDialog = Windows.Forms.DialogResult.OK Then
            DumpExcel(dtDane, sfd.FileName, Me.Text)
        End If

        ' Changes the CurrentCulture of the current thread to pl-PL.
        Thread.CurrentThread.CurrentCulture = New CultureInfo("pl-PL", False)
        Console.WriteLine("CurrentCulture is now {0}.", CultureInfo.CurrentCulture.Name)

        ' Changes the CurrentUICulture of the current thread to pl-PL.
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("pl-PL", False)
        Console.WriteLine("CurrentUICulture is now {0}.", CultureInfo.CurrentUICulture.Name)
    End Sub

   
End Class