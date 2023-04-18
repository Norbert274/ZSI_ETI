Imports System.IO
Imports OfficeOpenXml
Imports OfficeOpenXml.Style

Public Class frmRaportAwiz

    Private Function wczytaj() As Boolean

        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        'ws.Url = frmGlowna.strWebservice
        Dim wsWynik As wsCursorProf.RaportAwizWynik

        'odczyt z serwera
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.RaportAwiz(frmGlowna.sesja, DateTime.Parse(dtpOd.Value.ToString("yyyy-MM-dd") & " 00:00:00.001"), DateTime.Parse(dtpDo.Value.ToString("yyyy-MM-dd") + " 23:59:59.999"))
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, "Raport awiz")
            Return False
        End Try
        If wsWynik.status = -1 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, "Brak uprawnień")
            Return False
        End If
        'kontrola wyników
        If wsWynik.dane.Tables.Count > 0 Then
            If wsWynik.dane.Tables(0).Rows.Count < 1 Then
                MsgBox("Brak danych o awizach z wybranego okresu.", MsgBoxStyle.Exclamation, "Raport awiz")
                Return False
            End If
        Else
            MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał tabeli z raportem awiz." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, "Raport awiz")
            Return False
        End If

        dgv.DataSource = wsWynik.dane.Tables(0)

        Me.Text = "Raport awiz za okres od " & dtpOd.Value.ToString("yyy-MM-dd") & " do " & dtpDo.Value.ToString("yyyy-MM-dd")
        lblIlePozycji.Text = "Ilość pozycji: " & dgv.RowCount
        For Each col As DataGridViewColumn In dgv.Columns
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        Next


        Return True
    End Function


    Private Sub btnGeneruj_Click(sender As System.Object, e As System.EventArgs) Handles btnGeneruj.Click
        wczytaj()
    End Sub

    Private Sub btnRaportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRaportExcel.Click
        Dim sfd As New SaveFileDialog
        sfd.Filter = "Skoroszyt programu Excel|*.xlsx"
        sfd.FileName = Me.Text
        If sfd.ShowDialog = Windows.Forms.DialogResult.OK Then
            DumpExcel(dgv.DataSource, sfd.FileName, Me.Text)
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
                ws.Cells(3, 1, 3, ile_kolumn).Style.Font.Color.SetColor(Color.White)
                ws.Cells("A3").LoadFromDataTable(tbl, True)

                For i = 0 To ile_kolumn - 1
                    If tbl.Columns(i).ColumnName.ToUpper.Contains("DATA") = True Then
                        '' formatowanie kolumny w excelu na datę
                        ws.Column(i + 1).Style.Numberformat.Format = "YYYY-MM-DD"
                    ElseIf tbl.Columns(i).ColumnName = "cena_jednostkowa" Or _
                        tbl.Columns(i).ColumnName = "wartosc" Then
                        ws.Column(i + 1).Style.Numberformat.Format = "#,##0.00zł"
                    End If

                    If i > 0 Then
                        ws.Column(i + 1).AutoFit()
                    Else
                        ws.Column(i + 1).Width = 18
                    End If

                Next

                ws.Cells(3, 1, 3, ile_kolumn).Style.Fill.PatternType = ExcelFillStyle.Solid
                ws.Cells(3, 1, 3, ile_kolumn).Style.Fill.BackgroundColor.SetColor(Color.SteelBlue)



                'For i = 1 To ile_kolumn
                '    ws.Column(i).AutoFit()
                'Next

                ' zapisanie wyniku do pliku 

                pck.Save()
                MsgBox("Zapisano raport: " & filePath, MsgBoxStyle.Information, "Zapis pliku z raportem")

            End Using

        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem. " & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, "Raport awiz")
            Exit Sub
        End Try
    End Sub

    Private Sub btnZamknij_Click(sender As System.Object, e As System.EventArgs) Handles btnZamknij.Click
        Me.Close()
    End Sub

    Private Sub txtFiltrPC_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            wczytaj()
        End If
    End Sub


End Class