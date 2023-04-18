Imports System.Reflection
Imports System
Imports Microsoft.Office.Interop
Imports System.Text.RegularExpressions
Imports System.Data.SqlClient
Imports System.Threading
Imports System.Globalization
Imports System.Text
Imports OfficeOpenXml
Imports OfficeOpenXml.Style
Imports OfficeOpenXml.Utils
Imports System.IO

Public Class frmZamowienieINVPozycjeExcel
    Public dtSKUExcel As New DataTable
    Public dtDostepneSKU As New DataTable
    Public magazyn_id As Integer
    Private nazwa_pliku_XLS As String
    Private NazwaArkusza As String = ""
    Private ile_sku As Integer
    Private nazwaSzablonu As String = "Szablon - import pozycji do zamówienia INV.xlsx"

    Private Sub btnWybierzPlik_Click(sender As System.Object, e As System.EventArgs) Handles btnWybierzPlik.Click
        Dim i As Integer = 0
        Dim j As Integer = 0

        Dim ofdPlikXLS As New OpenFileDialog
        ofdPlikXLS.Filter = "Pliki Programu Excel (*.xls, *.xlsx)|*.xls;*.xlsx"
        ofdPlikXLS.ReadOnlyChecked = False
        If ofdPlikXLS.ShowDialog = Windows.Forms.DialogResult.OK Then

            Try
                Dim ExcelFile As New FileInfo(ofdPlikXLS.FileName)

                Using pck As New ExcelPackage(ExcelFile)

                    If cmbNazwaArkusza.Items.Count > 0 Then
                        cmbNazwaArkusza.Items.Clear()
                    End If

                    For i = 1 To pck.Workbook.Worksheets.Count
                        cmbNazwaArkusza.Items.Add(pck.Workbook.Worksheets(i).Name)
                    Next

                    If pck.Workbook.Worksheets.Count = 1 Then
                        cmbNazwaArkusza.SelectedIndex = 0
                    End If

                    nazwa_pliku_XLS = ofdPlikXLS.FileName
                    txtPlik.Text = ofdPlikXLS.FileName
                End Using
            Catch ex As Exception
                If ex.Message.Contains("because it is being used by another process") Then
                    MsgBox("Wybrany plik jest aktualnie otwarty! Proszę go zamknąć i spróbować wczytać ponownie.", MsgBoxStyle.Exclamation, "Komunikat")
                Else
                    MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Komunikat")
                End If
            End Try

        End If
    End Sub

    Private Function SprawdzNaglowekKolumny(ByVal wpisany As String, _
                                           ByVal poprawny As String, _
                                           ByVal ExcelCell As String) As Boolean
        If wpisany.ToUpper.Trim <> poprawny.ToUpper.Trim Then
            Cursor = Cursors.Default
            MsgBox("Niepoprawny nagłówek kolumny w komórce """ & ExcelCell & """. " & vbNewLine & _
                   "Wpisano: " & wpisany & vbNewLine & _
                  "Prawidłowa nazwa kolumny: " & poprawny & vbNewLine & _
                   vbNewLine & "Proszę poprawić plik i spróbować ponownie złożyć z niego zamówienia. ", _
                   MsgBoxStyle.Exclamation, "Błędnie przygotowany plik Excela")
            Return False
        End If
        Return True
    End Function

    Private Sub btnOK_Click(sender As System.Object, e As System.EventArgs) Handles btnOK.Click

        If pobierz_dane_z_pliku() = True Then
            If sprawdz_dostepne_ilosci() = True Then
                Me.DialogResult = System.Windows.Forms.DialogResult.OK
            End If
        End If

    End Sub

    Private Function pobierz_dane_z_pliku() As Boolean

        '' sprawdzamy czy wybrano arkusz
        If cmbNazwaArkusza.SelectedIndex = -1 Then
            MsgBox("Nie wybrano nazwy arkusza, z którego mają zostać wczytane pozycje do awiza!", MsgBoxStyle.Exclamation, "Brak nazwy arkusza")
            Return False
        End If

        NazwaArkusza = cmbNazwaArkusza.Text
        Dim czyOK As Boolean = False
        Dim ExcelFile As New FileInfo(nazwa_pliku_XLS)

        Try
            Using pck As New ExcelPackage(ExcelFile)

                Dim ws As ExcelWorksheet = pck.Workbook.Worksheets(NazwaArkusza)
                Dim i As Integer

                Cursor = Cursors.WaitCursor

                '' sprawdzamy czy mamy poprawnie sformatowany plik Excela wg poniższego układu kolumn:
                Dim dtNaglowki As New DataTable
                dtNaglowki.Columns.Add("naglowek")
                dtNaglowki.Columns.Add("excel_cell")
                dtNaglowki.Rows.Add("SKU", "A1")
                dtNaglowki.Rows.Add("GRUPA", "B1")
                dtNaglowki.Rows.Add("ILOŚĆ", "C1")

                Dim czy_poprawne_naglowki As Boolean = True
                For Each r As DataRow In dtNaglowki.Rows
                    If Not SprawdzNaglowekKolumny(Trim(CStr(ws.Cells(r.Item("excel_cell")).Value)), _
                                                  r.Item("naglowek").ToString, r.Item("excel_cell")) Then
                        czy_poprawne_naglowki = False
                    End If
                Next

                If czy_poprawne_naglowki = False Then
                    '' nazwa lub nazwy kolumn różnią się od nazw z szablonu; kończymy procedurę
                    czyOK = False
                    GoTo koniec
                End If

                Dim j As Integer = 0

                '' Jeśli dtSKUExcel ma już wiersze to usuwamy je
                If dtSKUExcel.Rows.Count > 0 Then
                    dtSKUExcel.Rows.Clear()
                End If

                Dim grupa_id As Integer = 0
                Dim nazwa_sku As String = ""
                i = 0

                '' dodajemy kolejno dane z pliku Excela do zmiennej dtSKUExcel
                Do Until CStr(ws.Cells("A2").Offset(i, 0).Value) = "" And CStr(ws.Cells("B2").Offset(i, 0).Value) = "" And CStr(ws.Cells("C2").Offset(i, 0).Value) = ""

                    Dim r As DataRow = dtSKUExcel.NewRow
                    r.Item("wiersz_nr") = i + 2
                    r.Item("sku") = Trim(CStr(ws.Cells("A2").Offset(i, 0).Value))
                    r.Item("grupa") = Trim(CStr(ws.Cells("B2").Offset(i, 0).Value))
                    r.Item("ilosc") = Trim(CStr(ws.Cells("C2").Offset(i, 0).Value))

                    dtSKUExcel.Rows.Add(r)

                    i = i + 1
                Loop

                ' ilość produktow
                ile_sku = i

                '' sprawdzamy, czy dtSKUExcel mają choć jeden rekord
                If dtSKUExcel.Rows.Count > 0 Then
                    btnOK.Enabled = True
                    czyOK = True
                Else
                    btnOK.Enabled = False
                    czyOK = False
                    MsgBox("W załadowanym pliku nie ma żadnej pozycji!" & vbNewLine, _
                              MsgBoxStyle.Exclamation, _
                             "Brak produktów w pliku")
                End If

koniec:
                Cursor = Cursors.Default

            End Using
        Catch ex As Exception
            If ex.Message.Contains("because it is being used by another process") Then
                MsgBox("Wybrany plik jest aktualnie otwarty! Proszę go zamknąć i spróbować wczytać ponownie.", MsgBoxStyle.Exclamation, "Komunikat")
            Else
                MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Komunikat")
            End If
        End Try

        If czyOK Then
            Return True
        Else
            Return False
        End If
    End Function


    Private Function sprawdz_dostepne_ilosci() As Boolean

        If dtSKUExcel.Rows.Count < 1 Then
            MsgBox("Brak pozycji do importu.", MsgBoxStyle.Exclamation, "Brak pozycji")
            Return False
        End If

        Dim strXml As New StringBuilder
        For Each r As DataRow In dtSKUExcel.Rows
            strXml.Append("<row wiersz_nr=""" & r.Item("wiersz_nr").ToString.Replace("&", "&amp;").Replace("'", "&apos;").Replace("""", "&quot;") & """ sku=""" & r.Item("sku").ToString.Replace("&", "&amp;").Replace("'", "&apos;").Replace("""", "&quot;") & """ grupa_nazwa=""" & r.Item("grupa").ToString.Replace("&", "&amp;").Replace("'", "&apos;").Replace("""", "&quot;") & """ ilosc=""" & r.Item("ilosc") & """/>")
        Next

        'doczytujemy ilości dostępne z bazy
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik As New wsCursorProf.StanSkuGrupaINVWynik

        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.StanSkuGrupaINV(frmGlowna.sesja, magazyn_id, "", strXml.ToString())
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
            Return False
        End Try
        Cursor = Cursors.Default
        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, Me.Text)
            Return False
        End If

        If wsWynik.dane.Tables.Count < 1 Then
            MsgBox("Błąd wewnętrzny systemu: serwer nie zwrócił listy dostępnych ilości dla wybranych sku." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
            Return False
        End If

        dtDostepneSKU = wsWynik.dane.Tables(0)

        Return True
    End Function

    Private Sub btnZamknij_Click(sender As System.Object, e As System.EventArgs) Handles btnZamknij.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmAwizoDodajPozycjeZExcela_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        dtSKUExcel.Columns.Add("wiersz_nr")
        dtSKUExcel.Columns.Add("sku")
        dtSKUExcel.Columns.Add("grupa")
        dtSKUExcel.Columns.Add("ilosc")
    End Sub


    Private Sub btnSzablon_Click(sender As System.Object, e As System.EventArgs) Handles btnSzablon.Click
        PobierzPlik(nazwaSzablonu)
    End Sub

    Private Function PobierzPlik(ByVal nazwa As String) As Boolean

        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik As New wsCursorProf.PobierzPlikWynik

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
            Dim extension As String = Path.GetExtension(nazwa)
            sfd.Filter = "*" & extension & "|*" & extension
            sfd.FileName = nazwa

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

End Class