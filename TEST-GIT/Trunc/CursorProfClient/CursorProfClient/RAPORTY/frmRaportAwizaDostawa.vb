Imports OfficeOpenXml
Imports OfficeOpenXml.Style
Imports OfficeOpenXml.Utils
Imports OfficeOpenXml.DataValidation
Imports System.IO

Public Class frmRaportAwizaDostawa
    Private dtRaport As New DataTable
    Private dtStatus As New DataTable
    Private bStatusyAll As Boolean = True 'zeby nie robic oddzielnej procedury na wczytanie do listy checkboxow.
    Private xmlStatus As String = String.Empty
    Private xmlStatusAll As String = String.Empty
    Private bWczytano As Boolean = False
    Private kolorAktywnosci As Color = Color.DodgerBlue
    Private kolorBrakuAktywnosci As Color = Color.LightGray
    Private listaFormatowKolumnRaportow As New List(Of String)
    Private dtFormaty As New DataTable
    Private dataPlanowanaOd As Date
    Private dataPlanowanaDo As Date

    Private Function generuj() As Boolean
        dataPlanowanaOd = dtpPlanowanaDataOd.Value
        dataPlanowanaDo = dtpPlanowanaDataDo.Value

        If dataPlanowanaOd > dataPlanowanaDo Then
            MsgBox("'Data do' jest za wysoka w stosunku do 'data od'!. Zmień daty.", MsgBoxStyle.Exclamation, Me.Text)
            Return False
        End If
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials


        Dim wsWynik As wsCursorProf.RaportAwizDostawaGenerujWynik

        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.RaportAwizDostawaGeneruj(frmGlowna.sesja, txtAwizoFiltr.Text, txtPOFiltr.Text, bStatusyAll, xmlStatus, txtQrZaFiltr.Text, txtQrDostawaFiltr.Text, txtQrPzFiltr.Text, DateTime.Parse(dataPlanowanaOd.ToString("yyyy-MM-dd") & " 00:00:01.001"), DateTime.Parse(dataPlanowanaDo.ToString("yyyy-MM-dd") & " 23:59:59.999"))
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

        If wsWynik.dane.Tables.Count > 0 Then
            dtRaport = wsWynik.dane.Tables(0)
            dgv.DataSource = dtRaport
            If dgv.Columns.Contains("STATUS_ID") Then dgv.Columns("STATUS_ID").Visible = False
        Else
            MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał tabeli z raportem awiz." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
            Return False
            Exit Function
        End If

        If wsWynik.ileRekordow < 1 Then
            btnEksportExcel.Enabled = False
            btnEksportExcel.BackColor = kolorBrakuAktywnosci
            MsgBox("Dla wybranych warunków filtrowania nie ma danych.", MsgBoxStyle.Information, Me.Text)
        Else
            btnEksportExcel.Enabled = True
            btnEksportExcel.BackColor = kolorAktywnosci
        End If


        If wsWynik.dane.Tables.Count > 1 Then
            dtFormaty = wsWynik.dane.Tables(1)
            For Each row As DataRow In dtFormaty.Rows
                listaFormatowKolumnRaportow.Add(IIf(IsDBNull(row.Item("format")), "", row.Item("format")))
            Next
        Else
            MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał tabeli z formatami kolumn." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
            Return False
            Exit Function
        End If

        Return True
    End Function

    Private Function wczytajWarunkiFiltrowania() As Boolean

        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials


        Dim wsWynik As wsCursorProf.RaportAwizaDostawaFiltrWczytajWynik

        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.RaportAwizaDostawaFiltrWczytaj(frmGlowna.sesja)
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


        If wsWynik.dane.Tables.Count > 0 Then
            dtStatus = wsWynik.dane.Tables(0)
            listStatus.DataSource = dtStatus
            listStatus.DisplayMember = "Nazwa"
            listStatus.ValueMember = "ID"

        Else
            MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał tabeli ze statusami awiz." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
            Return False
            Exit Function
        End If

        dtpPlanowanaDataOd.Value = wsWynik.minimalnaDataAwiza
        Return True
    End Function

    Private Sub PobierzXml()
        xmlStatus = String.Empty

        For i As Integer = 0 To listStatus.Items.Count - 1
            If listStatus.GetItemChecked(i) Then
                xmlStatus = xmlStatus & String.Format("<row status_id=""{0}"" nazwa=""{1}""/>", CType(listStatus.Items(i).Row, DataRow).Item("ID"), CType(listStatus.Items(i).Row, DataRow).Item("Nazwa"))
            End If
        Next

        If bWczytano = False Then
            xmlStatusAll = xmlStatus
        End If

        If xmlStatus.Length <> xmlStatusAll.Length Then
            bStatusyAll = False
        Else
            bStatusyAll = True
        End If
    End Sub

    Private Sub frmRaportCursorAwiza_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        'dtpPlanowanaDataOd.Value = DateAdd(DateInterval.Month, -1, Date.Now)
        wczytajWarunkiFiltrowania()
        chbStatusy.Checked = True
        btnEksportExcel.Enabled = True
        btnEksportExcel.BackColor = kolorBrakuAktywnosci
        PobierzXml()
        bWczytano = True
    End Sub

    Private Sub btnZamknij_Click(sender As Object, e As System.EventArgs) Handles btnZamknij.Click
        Me.Close()
    End Sub

    Private Sub btnGeneruj_Click(sender As Object, e As System.EventArgs) Handles btnGeneruj.Click
        PobierzXml()
        generuj()
    End Sub

    Private Sub chbStatusy_CheckedChanged(sender As Object, e As System.EventArgs) Handles chbStatusy.CheckedChanged
        For i As Integer = 0 To listStatus.Items.Count - 1
            listStatus.SetItemChecked(i, chbStatusy.Checked)
        Next
    End Sub

    Public Sub DumpExcel(ByVal tbl As DataTable, ByVal filePath As String, ByVal tytul As String)
        Try
            If File.Exists(filePath) Then
                File.Delete(filePath)
            End If
            Dim newFile As New FileInfo(filePath)

            Using pck As New ExcelPackage(newFile)

                pck.Workbook.Properties.Author = ""
                pck.Workbook.Properties.Title = tytul
                pck.Workbook.Properties.Company = "OEX E-Business"

                Dim ws As ExcelWorksheet = pck.Workbook.Worksheets.Add("Raport")

                ws.Cells("A1").Value = tytul
                ws.Cells("A1").Style.Font.Bold = True
                ws.Cells("A1").Style.Font.Size = 14
                ws.Cells(3, 1, 3, tbl.Columns.Count).Style.Font.Bold = True
                ws.Cells("A3").LoadFromDataTable(tbl, True)

                For i As Integer = 0 To listaFormatowKolumnRaportow.Count - 1
                    If listaFormatowKolumnRaportow(i).Length > 0 Then
                        ws.Column(i + 1).Style.Numberformat.Format = listaFormatowKolumnRaportow(i)
                    End If
                Next
                ws.Cells(3, 1, 3, tbl.Columns.Count).Style.Fill.PatternType = ExcelFillStyle.Solid
                ws.Cells(3, 1, 3, tbl.Columns.Count).Style.Fill.BackgroundColor.SetColor(Color.SteelBlue)
                ws.Cells(3, 1, 3, tbl.Columns.Count).Style.Font.Color.SetColor(Color.White)
                For i As Integer = 1 To tbl.Columns.Count
                    ws.Column(i).AutoFit()
                Next
                ' zapisanie wyniku do pliku 
                pck.Save()
                MsgBox("Zapisano raport: " & filePath, MsgBoxStyle.Information, Me.Text)

            End Using

        Catch ex As Exception
            Cursor = Cursors.Default
            If ex.Message.Contains("because it is being used by another process") Then
                MsgBox("Plik o nazwie " & tytul & _
                       " jest otwarty! Proszę zamknąć ten plik i spróbowac ponownie go wczytać.", _
                       MsgBoxStyle.Exclamation, Me.Text)
            Else
                MsgBox("Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, "Wystąpił wyjątek")
            End If
            Exit Sub
        End Try
    End Sub

    Private Sub btnEksportExcel_Click(sender As Object, e As System.EventArgs) Handles btnEksportExcel.Click
        Dim sfd As New SaveFileDialog
        sfd.Filter = "Skoroszyt programu Excel|*.xlsx"
        sfd.FileName = Me.Text & "_" & DateTime.Now.Date.ToString("MM-dd-yyyy")
        Dim dtRaportExcel As New DataTable
        dtRaportExcel = dtRaport.Copy

        For Each col As DataGridViewColumn In dgv.Columns
            If col.Visible = False Then
                dtRaportExcel.Columns.Remove(col.DataPropertyName)
            End If
        Next
        If sfd.ShowDialog = Windows.Forms.DialogResult.OK Then
            DumpExcel(dtRaportExcel, sfd.FileName, Me.Text & "_" & DateTime.Now.Date.ToString("MM-dd-yyyy"))
        End If
    End Sub
End Class