Imports System
Imports System.Globalization
Imports System.Security.Permissions
Imports System.Threading
Imports Microsoft.Office.Interop
Imports System.ComponentModel
Imports OfficeOpenXml
Imports OfficeOpenXml.Style
Imports OfficeOpenXml.Utils
Imports System.IO

Public Class frmRaportPaletodni
    Public dtPaletodni As DataTable
    Public data_od As Date
    Public data_do As Date
    Private Sub btnZamknij_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnZamknij.Click
        Me.Close()
    End Sub

    'Private Sub btnRaportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRaportExcel.Click
    '    ' Changes the CurrentCulture of the current thread to pl-PL.
    '    Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US", False)
    '    Console.WriteLine("CurrentCulture is now {0}.", CultureInfo.CurrentCulture.Name)

    '    ' Changes the CurrentUICulture of the current thread to pl-PL.
    '    Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US", False)
    '    Console.WriteLine("CurrentUICulture is now {0}.", CultureInfo.CurrentUICulture.Name)

    '    'wpisanie danych do Excela
    '    Dim objXls = CreateObject("Excel.Application")
    '    Dim objWrk = DirectCast(objXls.Workbooks.Add, Excel.Workbook)
    '    Dim objSheet = DirectCast(objWrk.Sheets(1), Excel.Worksheet)

    '    'objSheet.Rows("3:3").RowHeight = 25
    '    'objSheet.Cells("A3:G3").HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter
    '    'objSheet.Cells("A3:G3").VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter

    '    Dim i As Integer = 0
    '    For i = 1 To dtZamowienia.Columns.Count
    '        With objSheet.Cells(3, i)
    '            .Value = dtZamowienia.Columns(i - 1).ColumnName
    '            .Interior.ColorIndex = 15
    '            .Interior.Pattern = 1 'xlSolid
    '            .Interior.PatternColorIndex = -4105 'xlAutomatic
    '            .Font.Bold = True
    '        End With
    '    Next

    '    'With objSheet.Cells(3, 1)
    '    '    .Value = "NUMER ZAMÓWIENIA"
    '    '    .Interior.ColorIndex = 15
    '    '    .Interior.Pattern = 1 'xlSolid
    '    '    .Interior.PatternColorIndex = -4105 'xlAutomatic
    '    'End With
    '    'With objSheet.Cells(3, 2)
    '    '    .Value = "ZAMAWIAJĄCY"
    '    '    .Interior.ColorIndex = 15
    '    '    .Interior.Pattern = 1 'xlSolid
    '    '    .Interior.PatternColorIndex = -4105 'xlAutomatic
    '    'End With
    '    'With objSheet.Cells(3, 3)
    '    '    .Value = "TYP UŻYTKOWNIKA"
    '    '    .Interior.ColorIndex = 15
    '    '    .Interior.Pattern = 1 'xlSolid
    '    '    .Interior.PatternColorIndex = -4105 'xlAutomatic
    '    'End With
    '    'With objSheet.Cells(3, 4)
    '    '    .Value = "WIELKOŚĆ UŻYTKOWNIKA"
    '    '    .Interior.ColorIndex = 15
    '    '    .Interior.Pattern = 1 'xlSolid
    '    '    .Interior.PatternColorIndex = -4105 'xlAutomatic
    '    'End With
    '    'With objSheet.Cells(3, 5)
    '    '    .Value = "DATA ZŁOŻENIA ZAMÓWIENIA"
    '    '    .Interior.ColorIndex = 15
    '    '    .Interior.Pattern = 1 'xlSolid
    '    '    .Interior.PatternColorIndex = -4105 'xlAutomatic
    '    'End With
    '    'With objSheet.Cells(3, 6)
    '    '    .Value = "DATA REALIZACJI"
    '    '    .Interior.ColorIndex = 15
    '    '    .Interior.Pattern = 1 'xlSolid
    '    '    .Interior.PatternColorIndex = -4105 'xlAutomatic
    '    'End With
    '    'With objSheet.Cells(3, 7)
    '    '    .Value = "ODBIORCA"
    '    '    .Interior.ColorIndex = 15
    '    '    .Interior.Pattern = 1 'xlSolid
    '    '    .Interior.PatternColorIndex = -4105 'xlAutomatic
    '    'End With
    '    'With objSheet.Cells(3, 8)
    '    '    .Value = "ADRES"
    '    '    .Interior.ColorIndex = 15
    '    '    .Interior.Pattern = 1 'xlSolid
    '    '    .Interior.PatternColorIndex = -4105 'xlAutomatic
    '    'End With
    '    'With objSheet.Cells(3, 9)
    '    '    .Value = "KOMENTARZ"
    '    '    .Interior.ColorIndex = 15
    '    '    .Interior.Pattern = 1 'xlSolid
    '    '    .Interior.PatternColorIndex = -4105 'xlAutomatic
    '    'End With
    '    'With objSheet.Cells(3, 10)
    '    '    .Value = "NUMER PRODUKTU"
    '    '    .Interior.ColorIndex = 15
    '    '    .Interior.Pattern = 1 'xlSolid
    '    '    .Interior.PatternColorIndex = -4105 'xlAutomatic
    '    'End With
    '    'With objSheet.Cells(3, 11)
    '    '    .Value = "NAZWA PRODUKTU"
    '    '    .Interior.ColorIndex = 15
    '    '    .Interior.Pattern = 1 'xlSolid
    '    '    .Interior.PatternColorIndex = -4105 'xlAutomatic
    '    'End With
    '    'With objSheet.Cells(3, 12)
    '    '    .Value = "KOSZT PUNKTOWY"
    '    '    .Interior.ColorIndex = 15
    '    '    .Interior.Pattern = 1 'xlSolid
    '    '    .Interior.PatternColorIndex = -4105 'xlAutomatic
    '    'End With
    '    'With objSheet.Cells(3, 13)
    '    '    .Value = "ILOSC ZAMÓWIONA"
    '    '    .Interior.ColorIndex = 15
    '    '    .Interior.Pattern = 1 'xlSolid
    '    '    .Interior.PatternColorIndex = -4105 'xlAutomatic
    '    'End With


    '    Dim intWierszXls As Integer = 4
    '    For Each dtRow As DataRow In dtZamowienia.Rows
    '        For i = 1 To dtZamowienia.Columns.Count
    '            objSheet.Cells(intWierszXls, i).value = dtRow.Item(i - 1)
    '        Next

    '        'objSheet.Cells(intWierszXls, 2).value = dtRow("ZAMAWIAJACY")
    '        'objSheet.Cells(intWierszXls, 3).value = dtRow("TYP_UZYTKOWNIKA")
    '        'objSheet.Cells(intWierszXls, 4).value = dtRow("WIELKOSC_UZYTKOWNIKA")
    '        'objSheet.Cells(intWierszXls, 5).value = dtRow("DATA_ZLOZENIA_ZAMOWIENIA")
    '        'objSheet.Cells(intWierszXls, 6).value = dtRow("DATA_REALIZACJI")
    '        'objSheet.Cells(intWierszXls, 7).value = dtRow("ODBIORCA")
    '        'objSheet.Cells(intWierszXls, 8).value = dtRow("ADRES")
    '        'objSheet.Cells(intWierszXls, 9).value = dtRow("KOMENTARZ")
    '        'objSheet.Cells(intWierszXls, 10).value = dtRow("NUMER_PRODUKTU")
    '        'objSheet.Cells(intWierszXls, 11).value = dtRow("NAZWA_PRODUKTU")
    '        'objSheet.Cells(intWierszXls, 12).value = dtRow("KOSZT PUNKTOWY")
    '        'objSheet.Cells(intWierszXls, 13).value = dtRow("ILOSC_ZAMOWIONA")
    '        intWierszXls += 1
    '    Next

    '    For i = 1 To dtZamowienia.Columns.Count
    '        objSheet.Columns(i).EntireColumn.AutoFit()
    '    Next

    '    'objSheet.Columns(1).EntireColumn.AutoFit()
    '    'objSheet.Columns(2).EntireColumn.AutoFit()
    '    'objSheet.Columns(3).EntireColumn.AutoFit()
    '    'objSheet.Columns(4).EntireColumn.AutoFit()
    '    'objSheet.Columns(5).EntireColumn.AutoFit()
    '    'objSheet.Columns(6).EntireColumn.AutoFit()
    '    'objSheet.Columns(7).EntireColumn.AutoFit()
    '    'objSheet.Columns(8).EntireColumn.AutoFit()
    '    'objSheet.Columns(9).EntireColumn.AutoFit()
    '    'objSheet.Columns(10).EntireColumn.AutoFit()
    '    'objSheet.Columns(11).EntireColumn.AutoFit()
    '    'objSheet.Columns(12).EntireColumn.AutoFit()
    '    'objSheet.Columns(13).EntireColumn.AutoFit()
    '    objSheet.Cells(1, 1) = Me.Text
    '    objXls.Visible = True

    '    ' Changes the CurrentCulture of the current thread to pl-PL.
    '    Thread.CurrentThread.CurrentCulture = New CultureInfo("pl-PL", False)
    '    Console.WriteLine("CurrentCulture is now {0}.", CultureInfo.CurrentCulture.Name)

    '    ' Changes the CurrentUICulture of the current thread to pl-PL.
    '    Thread.CurrentThread.CurrentUICulture = New CultureInfo("pl-PL", False)
    '    Console.WriteLine("CurrentUICulture is now {0}.", CultureInfo.CurrentUICulture.Name)
    'End Sub

    Private Sub btnRaportExcel_Click(sender As System.Object, e As System.EventArgs) Handles btnRaportExcel.Click
        Dim sfd As New SaveFileDialog
        sfd.Filter = "Skoroszyt programu Excel|*.xlsx"
        sfd.FileName = Me.Text
        If sfd.ShowDialog = Windows.Forms.DialogResult.OK Then
            DumpExcel(dtPaletodni, sfd.FileName, Me.Text)
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
                ws.Cells(3, 1, 3, ile_kolumn).Style.Font.Bold = True
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

                ws.Cells(3, 1, 3, ile_kolumn).Style.Fill.PatternType = ExcelFillStyle.Solid
                ws.Cells(3, 1, 3, ile_kolumn).Style.Fill.BackgroundColor.SetColor(Color.Yellow)

                ' zapisanie wyniku do pliku 

                pck.Save()
                MsgBox("Zapisano raport: " & filePath, MsgBoxStyle.Information)

            End Using

        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem. " & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical)
            Exit Sub
        End Try
    End Sub

End Class