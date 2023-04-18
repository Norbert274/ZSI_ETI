Imports System
Imports System.Globalization
Imports System.Security.Permissions
Imports System.Threading
Imports Microsoft.Office.Interop

Imports OfficeOpenXml
Imports OfficeOpenXml.Style
Imports OfficeOpenXml.Utils

Imports System.IO

Public Class frmRaportWejsciaWyjscia
    Private dtWeWy As New DataTable
    Private Sub btnGeneruj_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerujRaport.Click

        ' Changes the CurrentCulture of the current thread to pl-PL.
        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US", False)
        Console.WriteLine("CurrentCulture is now {0}.", CultureInfo.CurrentCulture.Name)

        ' Changes the CurrentUICulture of the current thread to pl-PL.
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US", False)
        Console.WriteLine("CurrentUICulture is now {0}.", CultureInfo.CurrentUICulture.Name)

       System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
System.Net.ServicePointManager.Expect100Continue = False
ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik As wsCursorProf.RaportWejsciaWyjsciaWynik


        'odczyt danych z serwera
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.RaportWejsciaWyjscia(frmGlowna.sesja, CDate(dtpDataOd.Value).ToString("yyyy-MM-dd"), CDate(dtpDataDo.Value).ToString("yyyy-MM-dd"))
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("B³¹d komunikacji z serwerem. " & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegó³y b³êdu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
            Exit Sub
        End Try
        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, Me.Text)
            Exit Sub
        ElseIf wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, Me.Text)
        End If

        dtWeWy = wsWynik.dane.Tables(0)
        'kontrola wyników
        If wsWynik.dane.Tables.Count > 0 Then
            If wsWynik.dane.Tables(0).Rows.Count < 1 Then
                MsgBox("W systemie nie znaleziono danych spe³niaj¹cych podane kryteria.", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            Else
                Try
                    'imie = imie_nazwisko.Tables(0).Rows(0).Item("IMIE").ToString()
                    'nazwisko = imie_nazwisko.Tables(0).Rows(0).Item("NAZWISKO").ToString()

                    Dim zapisz As New SaveFileDialog
                    zapisz.Filter = "Excel 2010 (*.xlsx)|*.xlsx"
                    zapisz.Title = "Zapisz jako..."
                    zapisz.FileName = "Raport_wejsc_wyjsc_" & dtpDataOd.Text & "_" & dtpDataDo.Text
                    zapisz.OverwritePrompt = True
                    If zapisz.ShowDialog = Windows.Forms.DialogResult.OK Then
                        If File.Exists(zapisz.FileName) Then File.Delete(zapisz.FileName)
                        GenerujRaportWeWyExcel(dtWeWy, zapisz.FileName)

                    End If

                Catch ex As Exception
                    Cursor = Cursors.Default
                    MsgBox("Szczegó³y b³êdu: Kod 19" & vbNewLine & vbNewLine & ex.Message, MsgBoxStyle.Exclamation, "Wystapi³ wyj¹tek")
                End Try
            End If
        Else
            MsgBox("B³¹d wewnêtrzny systemu. Serwer nie przes³a³ tabeli z raportem wejœæ-wyjœæ." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
            Exit Sub
        End If


    End Sub
    Private Sub DumpExcel(ByVal tbl As DataTable, ByVal filePath As String, ByVal tytul As String)
        Try
            If File.Exists(filePath) Then
                File.Delete(filePath)
            End If
            Dim newFile As New FileInfo(filePath)

            Using pck As New ExcelPackage(newFile)

                'Create the worksheet
                pck.Workbook.Properties.Title = "Raport wejœæ-wyjœæ"
                pck.Workbook.Properties.Author = ""
                pck.Workbook.Properties.Title = tytul
                pck.Workbook.Properties.Company = "OEX E-Business"

                Dim ws As ExcelWorksheet = pck.Workbook.Worksheets.Add("WEJSCIA_WYJSCIA")
                Dim ile_kolumn As Integer = tbl.Columns.Count
                Dim i As Integer = 1

                'Load the datatable into the sheet, starting from cell A1. Print the column names on row 1
                ws.Cells("A1").Value = tytul
                ws.Cells("A1").Style.Font.Bold = True
                ws.Cells("A1").Style.Font.Size = 14
                ws.Cells("A3").LoadFromDataTable(tbl, True)

                For i = 0 To ile_kolumn - 1
                    If tbl.Columns(i).ColumnName.ToUpper.Contains("DATA") Then
                        '' formatowanie kolumny w excelu na datê
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
            MsgBox("B³¹d komunikacji z serwerem. " & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegó³y b³êdu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
            Exit Sub
        End Try
    End Sub
    Private Sub GenerujRaportWeWyExcel(ByVal tbl As DataTable, ByVal filename As String)

        Dim newFile As New FileInfo(filename)

        Using pck As New ExcelPackage(newFile)

            'Create the worksheet

            'pck.Workbook.Properties.Author = "Autor raportu"
            pck.Workbook.Properties.Title = "Raport wejœæ-wyjœæ"
            pck.Workbook.Properties.Company = "OEX E-Business"

            Dim ws As ExcelWorksheet = pck.Workbook.Worksheets.Add("WEJSCIA_WYJSCIA")

            'Load the datatable into the sheet, starting from cell A1. Print the column names on row 1
            ws.Cells("A1").LoadFromDataTable(tbl, True)

            Dim ile_kolumn As Integer = tbl.Columns.Count
            Dim i As Integer
            For i = 1 To ile_kolumn
                If i = 4 Then
                    ws.Column(i).Style.Numberformat.Format = "yyyy/mm/dd;@"
                End If
                ws.Column(i).AutoFit()
            Next

            ws.Cells("A1:I1").Style.Fill.PatternType = ExcelFillStyle.Solid
            ws.Cells("A1:I1").Style.Fill.BackgroundColor.SetColor(Color.SteelBlue)
            ws.Cells("A1:I1").Style.VerticalAlignment = ExcelVerticalAlignment.Center
            ws.Cells("A1:I1").Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
            ws.Cells("A1:I1").Style.Font.Bold = True
            ws.Cells("A1:I1").Style.Font.Color.SetColor(Color.White)

            ' zapisanie wyniku do pliku 
            pck.Save()
            MsgBox("Poprawnie zapisano plik: " & filename, MsgBoxStyle.Information, "Zapisano plik")

        End Using


    End Sub

    Private Sub frmRaportWejsciaWyjscia_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        dtpDataOd.CustomFormat = "yyyy-MM-dd"
        dtpDataDo.CustomFormat = "yyyy-MM-dd"
    End Sub

    Private Sub btnAnuluj_Click(sender As System.Object, e As System.EventArgs) Handles btnAnuluj.Click
        Me.Close()
    End Sub
End Class