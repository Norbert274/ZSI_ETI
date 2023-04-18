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
Public Class frmRaportTerminowosciFedEx
    Private dtDane As DataTable
    Private Function wczytaj() As Boolean

        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
        'Dim ws As New wsProf.ProfServerSoapClient
        '        'ws.Url = frmGlowna.strWebservice
        '        Dim wsWynik As wsProf.RaportTerminowosciFedExWczytajWynik

        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
        Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        'ws.Url = frmGlowna.strWebservice
        Dim wsWynik As wsCursorProf.RaportTerminowosciFedExWczytajWynik

        'odczyt z serwera
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.RaportTerminowosciFedExWczytaj(frmGlowna.sesja, DateTime.Parse(dtpDataOd.Value.ToString("yyyy-MM-dd") & " 00:00:00.001"), DateTime.Parse(dtpDataDo.Value.ToString("yyyy-MM-dd") + " 23:59:59.999"))
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical)
            Return False
        End Try
        If wsWynik.status = -1 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, "Komunikat")
            Return False
        End If
        'kontrola wyników
        If wsWynik.dane.Tables.Count > 0 Then
            If wsWynik.dane.Tables(0).Rows.Count < 1 Then
                MsgBox("Brak danych o zamówieniach z wybranego okresu.", MsgBoxStyle.Exclamation)
                Return False
            End If
        Else
            MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał tabeli z raportem." & frmGlowna.kontaktIt, MsgBoxStyle.Critical)
            Return False
            Exit Function
        End If

        dtDane = wsWynik.dane.Tables(0)
        dgv.DataSource = dtDane

        If wsWynik.status = -1 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation)
        End If
        Return True
    End Function

    Private Sub btnGenerujRaport_Click(sender As System.Object, e As System.EventArgs) Handles btnGenerujRaport.Click
        If dtpDataOd.Value.ToString("yyyy-MM-dd") > dtpDataDo.Value.ToString("yyyy-MM-dd") Then
            MessageBox.Show("'Data od' nie może być większa od 'Data do'!", "Błąd daty", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)
            Exit Sub
        End If
        If wczytaj() Then
            btnExportExcel.Enabled = True
            btnExportExcel.ForeColor = Color.White
            btnExportExcel.BackColor = System.Drawing.Color.DodgerBlue
        End If
    End Sub

    Private Sub btnExportExcel_Click(sender As System.Object, e As System.EventArgs) Handles btnExportExcel.Click
        Dim sfd As New SaveFileDialog
        sfd.Filter = "Skoroszyt programu Excel|*.xlsx"
        sfd.FileName = Me.Text
        If sfd.ShowDialog = Windows.Forms.DialogResult.OK Then
            ZamowieniaDoExcela_(dtDane, sfd.FileName, Me.Text)
        End If
    End Sub

    Private Sub ZamowieniaDoExcela_(ByVal tbl As DataTable, ByVal filePath As String, ByVal tytul As String)


        Try
            Cursor = Cursors.WaitCursor

            If File.Exists(filePath) Then
                File.Delete(filePath)
            End If
            Dim newFile As New FileInfo(filePath)


            Using pck As New ExcelPackage(newFile)

                'Create the worksheet

                pck.Workbook.Properties.Author = ""
                pck.Workbook.Properties.Title = tytul
                pck.Workbook.Properties.Company = "Cursor S.A."

                Dim ws As ExcelWorksheet = pck.Workbook.Worksheets.Add("Zamowienia_bez_pozycji")

                'Load the datatable into the sheet, starting from cell A1. Print the column names on row 1
                ws.Cells("A1").Value = tytul
                ws.Cells("A2").LoadFromDataTable(tbl, True)

                Dim ile_kolumn As Integer = tbl.Columns.Count
                Dim i As Integer
                For i = 1 To ile_kolumn
                    If i = 3 Or i = 4 Or i = 5 Then
                        ws.Column(i).Style.Numberformat.Format = "yyyy/mm/dd hh:mm:ss;@"
                    End If
                    ws.Column(i).AutoFit()
                Next

                ws.Cells(2, 1, 2, ile_kolumn).Style.Fill.PatternType = ExcelFillStyle.Solid
                ws.Cells(2, 1, 2, ile_kolumn).Style.Fill.BackgroundColor.SetColor(Color.SteelBlue)
                ws.Cells(2, 1, 2, ile_kolumn).Style.VerticalAlignment = ExcelVerticalAlignment.Center
                ws.Cells(2, 1, 2, ile_kolumn).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
                ws.Cells(2, 1, 2, ile_kolumn).Style.Font.Bold = True
                'ws.Cells("A2:N2").Style.Font.Color.
                ws.Cells(2, 1, 2, ile_kolumn).Style.Font.Color.SetColor(Color.White)

                ' zapisanie wyniku do pliku 

                pck.Save()
                Cursor = Cursors.Default
                MsgBox("Zapisano raport: " & filePath, MsgBoxStyle.Information)
            End Using
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem. " & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical)
            Exit Sub
        End Try
    End Sub

    Private Sub frmRaportTerminowosciFedEx_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        dtpDataOd.Value = DateAdd(DateInterval.Month, -1, Today)
        dtpDataDo.Value = Today
    End Sub
End Class