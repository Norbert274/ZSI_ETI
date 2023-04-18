Imports OfficeOpenXml
Imports OfficeOpenXml.Style
Imports OfficeOpenXml.Utils
Imports System.IO
Public Class frmRaportHistoriiMaterialu
    Public dtDane As DataTable
    Public sku As String
    Private dtSku As DataTable
    Private bPierwszeWczytanieFormy As Boolean = True
    Private skuDoKomunikatu As String = ""

    Private Sub btnZamknij_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnZamknij.Click
        Me.Close()
    End Sub

    Private Sub btnRaportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRaportExcel.Click

        If dtDane Is Nothing OrElse dtDane.Rows.Count < 1 Then
            MsgBox("Brak danych.", MsgBoxStyle.Exclamation, "Raport historii materiału")
            Exit Sub
        Else
            Try

                Dim zapisz As New SaveFileDialog
                zapisz.Filter = "Excel 2010 (*.xlsx)|*.xlsx"
                zapisz.Title = "Zapisz jako..."
                zapisz.FileName = "Historia_materialu - " & skuDoKomunikatu & ".xlsx"
                zapisz.OverwritePrompt = True
                If zapisz.ShowDialog = Windows.Forms.DialogResult.OK Then
                    If File.Exists(zapisz.FileName) Then File.Delete(zapisz.FileName)
                    GenerujRaportExcel(dtDane, zapisz.FileName, "Raport historii materiału - " & skuDoKomunikatu)
                End If

            Catch ex As Exception
                Cursor = Cursors.Default
                MsgBox("Szczegóły błędu: Kod 19" & vbNewLine & vbNewLine & ex.Message, MsgBoxStyle.Exclamation, "Wystapił wyjątek")
            End Try
        End If

    End Sub

    Private Sub GenerujRaportExcel(ByVal tbl As DataTable, ByVal filename As String, ByVal tytul As String)

        Dim newFile As New FileInfo(filename)

        Using pck As New ExcelPackage(newFile)

            'Create the worksheet

            'pck.Workbook.Properties.Author = "Autor raportu"
            pck.Workbook.Properties.Title = "Historia materiału"
            pck.Workbook.Properties.Company = "OEX E-Business"

            Dim ws As ExcelWorksheet = pck.Workbook.Worksheets.Add("Historia_materialu")
            Dim ile_kolumn As Integer = tbl.Columns.Count

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
            MsgBox("Poprawnie zapisano plik: " & filename, MsgBoxStyle.Information, "Zapisano plik")

        End Using

    End Sub

    Private Function wczytaj_liste_produktow() As Boolean

        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik As wsCursorProf.SkuListaWczytajWynik

        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.SkuListaWczytaj(frmGlowna.sesja)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, "Wczytanie listy produktów")
            Return False
            Exit Function
        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, "Wczytanie listy produktów")
            Return False
            Exit Function
        End If
        If wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, "Wczytanie listy produktów")
            Return False
            Exit Function
        End If

        If wsWynik.dane.Tables.Count > 0 And wsWynik.dane.Tables(1).Rows.Count > 0 Then
            dtSKU = wsWynik.dane.Tables(0)
        Else
            MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał listy produktów." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, "Wczytanie listy produktów")
            Return False
            Exit Function
        End If

        dgvProdukty.DataSource = dtSku
        dgvProdukty.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells)

        For Each c As DataGridViewColumn In dgvProdukty.Columns
            If c.Name.ToLower = "wybierz" Then
                c.ReadOnly = False
            Else
                c.ReadOnly = True
            End If

            If c.Name = "SKU_ID" Then
                c.Visible = False
            End If
        Next

        Return True
    End Function

    Private Sub frmRaportHistoriiMaterialu_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        bPierwszeWczytanieFormy = True
        If sku.Length > 0 Then
            dgvRaport.DataSource = dtDane
            dgvRaport.AutoResizeColumns()
        Else
            If Not wczytaj_liste_produktow() Then Me.Close()
        End If

        bPierwszeWczytanieFormy = True
    End Sub

    Private Function historia_materialu_wczytaj(ByVal sku As String) As Boolean

        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik As wsCursorProf.RaportHistoriiMaterialuWczytajWynik

        'odczyt z serwera
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.RaportHistoriiMaterialuWczytaj(frmGlowna.sesja, sku)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, "Raport historii materiału")
            Return False
        End Try
        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, "Brak uprawnień")
            Return False
        End If

        'kontrola wyników
        If wsWynik.dane.Tables.Count > 0 Then
            If wsWynik.dane.Tables(0).Rows.Count < 1 Then
                MsgBox("Brak danych o historii materiału dla SKU: " & skuDoKomunikatu, MsgBoxStyle.Information, "Brak danych")
                Return False
            End If
        Else
            MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał tabeli z historią materiału." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, "Brak danych")
            Return False
            Exit Function
        End If

        dtDane = wsWynik.dane.Tables(0)
        dgvRaport.DataSource = dtDane
        dgvRaport.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells)

        Return True
    End Function

    Private Sub btnGenerujRaport_Click(sender As System.Object, e As System.EventArgs) Handles btnGenerujRaport.Click
        dgvRaport.DataSource = Nothing
        Dim wybrane_sku As String = ""
        sku = ""
        For Each r As DataGridViewRow In dgvProdukty.Rows
            If r.Cells("Wybierz").Value Then
                wybrane_sku += "<row sku=""" & r.Cells("sku").Value & """/>"
                sku = sku & r.Cells("sku").Value & ", "
            End If
        Next

        sku = Replace(Replace(Mid(sku, 1, 50) & IIf(Len(sku) > 50, "...", ""), ":", ""), "/", "_")
        If sku.Length > 0 Then
            skuDoKomunikatu = Mid(sku, 1, sku.Length - 2)
        End If



        If wybrane_sku <> "" Then
            historia_materialu_wczytaj(wybrane_sku)
        Else
            MsgBox("Proszę wybrać sku z listy!", MsgBoxStyle.Exclamation, "Brak zaznaczenia")
        End If
    End Sub

    Private Sub txtFiltr_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtFiltr.TextChanged
        If Not dtSku Is Nothing Then
            Dim dv As New DataView(dtSku)
            dv.RowFilter = "sku like '%" & txtFiltr.Text.Replace("'", "''") & "%' or nazwa like '%" & txtFiltr.Text.Replace("'", "''") & "%'"
            dgvProdukty.DataSource = dv
        End If
    End Sub
End Class