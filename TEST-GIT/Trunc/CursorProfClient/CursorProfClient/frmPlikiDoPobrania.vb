Imports System.IO
Public Class frmPlikiDoPobrania
    Private dtPliki As DataTable
    Private nazwa_pliku_user As String
    Private nazwa_pliku_baza As String
    Private str_plik_dodaj As String
    Private str_miniaturka_dodaj As String
    Private str_tytul_pliku As String
    Private bool_wyswietlaj_www As Boolean
    Private komunikat As String
    Private ilePlikow As Integer
    Private maxRozmiarPliku As Integer

    Public Function wczytaj() As Boolean
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        'ws.Url = frmGlowna.strWebservice
        Dim wsWynik As wsCursorProf.PlikiDoPobraniaListaWynik

        'odczyt z serwera
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.PlikiDoPobraniaLista(frmGlowna.sesja)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, "Wczytanie listy plików")
            Return False
        End Try
        dtPliki = wsWynik.dane.Tables(0)
        maxRozmiarPliku = wsWynik.maxRozmiarPliku
        Dim dv As New DataView(dtPliki)
        dgvPliki.DataSource = dv
        If dgvPliki.Columns.Contains("plik_id") Then dgvPliki.Columns("plik_id").Visible = False
        If dgvPliki.Columns.Contains("czy_edytowalna") Then dgvPliki.Columns("czy_edytowalna").Visible = False
        'Dim col_chk As New DataGridViewCheckBoxColumn
        'dgvPliki.Columns.Insert(1, col_chk)
        'dgvPliki.Columns(1).HeaderText = "wybierz"
        'dgvPliki.Columns("wybierz").ReadOnly = False
        dgvPliki.CurrentCell = Nothing

        For Each dgvColumn As DataGridViewColumn In dgvPliki.Columns
            If dgvColumn.HeaderText.ToLower = "wybierz" Then
                dgvColumn.ReadOnly = False
                For Each row As DataGridViewRow In dgvPliki.Rows
                    row.Cells(dgvColumn.Index).Value = False
                Next

            Else
                dgvColumn.ReadOnly = True
            End If
            If dgvColumn.Name = "WYSWIETLAJ_NA_WWW" Then
                dgvColumn.HeaderText = "Wyświetlaj na WWW"
            End If

        Next

        For Each col As DataGridViewColumn In dgvPliki.Columns
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        Next
        dgvPliki.AllowUserToAddRows = False
        dgvPliki.AllowUserToDeleteRows = False
        If wsWynik.status = -1 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, "Wczytanie listy plików")
            Return False
        End If
        'If wsWynik.centrala = 1 Then
        '    btnDodajPlik.Visible = True
        'ElseIf wsWynik.centrala = 0 Then
        '    btnDodajPlik.Visible = False
        'End If
        For Each row As DataGridViewRow In dgvPliki.Rows
            If row.Cells("czy_edytowalna").Value = 0 Then
                row.Cells("wybierz").ReadOnly = True
            End If
        Next

        Dim tt As New ToolTipWithImage(dgvPliki, "miniaturka")
        tt.KOLUMNA_NAZWA = "MINIATURKA"
        tt.ID_NAZWA = "PLIK_ID"
        tt.TABELA_NAZWA = "PLIKI_DO_POBRANIA"
        Return True
    End Function

    Private Sub frmPlikiDoPobrania_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        btnUsunPliki.Enabled = False
        btnUsunPliki.BackColor = Color.LightGray
        If Not wczytaj() Then
            Exit Sub
        End If

    End Sub

    Private Sub btnZamknij_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnZamknij.Click
        Me.Close()
    End Sub

    Private Sub btnDodajPlik_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDodajPlik.Click
        Dim frm As New frmPlikDodaj
        'frm.Parent = Me
        frm.ShowDialog()

        If frm.DialogResult = Windows.Forms.DialogResult.OK Then

            str_plik_dodaj = frm.txtPilk.Text
            str_miniaturka_dodaj = frm.txtMiniaturka.Text
            str_tytul_pliku = frm.txtTytul.Text
            bool_wyswietlaj_www = frm.chkWyswietlajNaWWW.Checked

            If ZapiszPlikiNaSerwer() Then
                wczytaj()
                'MsgBox("Poprawnie zapisano pliki na serwer.", MsgBoxStyle.Information, "Zapis pliku")

                'Else : MsgBox("Nie zapisano plików na serwerze.", MsgBoxStyle.Exclamation, "Zapis pliku")
            End If

        End If
    End Sub

    Private Sub dgvPliki_CellContentClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvPliki.CellContentClick
       
        If dgvPliki.Columns(e.ColumnIndex).Name = "Wybierz" Then
            dgvPliki.EndEdit()
            dgvPliki.RefreshEdit()
            policzPliki()
        End If

    End Sub

    Public Sub dgvPliki_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvPliki.CellDoubleClick

        If e.RowIndex >= 0 And e.RowIndex < dgvPliki.Rows.Count Then
            'If dgvPliki.Rows(e.RowIndex).Cells("pobierz").Selected Then

            nazwa_pliku_baza = Nothing
            nazwa_pliku_baza = dgvPliki.Rows(e.RowIndex).Cells("nazwa_pliku").Value
            'Dim sfdPlik As New SaveFileDialog()
            'sfdPlik.Filter = "Word document (*.doc;*.docx)|*.doc;*.docx | PDF document (*.pdf)|*.pdf"

            'If sfdPlik.ShowDialog() = Windows.Forms.DialogResult.OK Then
            '    nazwa_pliku_user = sfdPlik.FileName
            'End If

            If Not PobierzPlik(nazwa_pliku_baza) Then
                MessageBox.Show("Nie pobrano pliku", "Wystapił problem", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)
            End If

            'End If
        End If

    End Sub

    Private Function ZapiszPlikiNaSerwer() As Boolean
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        'ws.Url = frmGlowna.strWebservice
        Dim wsWynik As wsCursorProf.ZapiszAtachmentWynik


        ''zapis miniaturki na serwer
        'Try
        '    Cursor = Cursors.WaitCursor
        '    Application.DoEvents()
        '    wsWynik = ws.ZapiszAtachment(miniatutka, Path.GetFileName(txtMiniaturka.Text))
        '    Cursor = Cursors.Default
        'Catch ex As Exception
        '    Cursor = Cursors.Default
        '    MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical)
        '    Return False
        'End Try
        Dim fstr As FileStream = File.Open(str_plik_dodaj, FileMode.Open)
        Dim plik(fstr.Length) As Byte

        Try


            If fstr.Length > maxRozmiarPliku Then
                MsgBox("Przekroczono rozmiar pliku. Plik musi być mniejszy niż " & maxRozmiarPliku / 1048576 & " MB.", MsgBoxStyle.Critical, "Rozmiar pliku")
                fstr.Flush()
                fstr.Close()
                fstr.Dispose()
                Return False
            End If
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            Return False
        End Try

       

        ' sprawdzamy czy dodawane pliki nie są plikami ukrytymi lub tylko do odczytu
        Dim fi As FileInfo = New FileInfo(str_plik_dodaj)
        If fi.Attributes = 33 Or fi.Attributes = 35 Or fi.Attributes = 36 Then
            MsgBox("Wczytany plik jest tylko do odczytu bądź plikiem ukrytym. Proszę wybrać inny plik.", MsgBoxStyle.Exclamation, "Zły atrybut pliku")
            Return False
        Else

            Dim wsWynikMiniaturka As wsCursorProf.ZapiszMiniaturkeWynik

            'zapis miniaturki i nazwy pliku do bazy danych
            If Not str_miniaturka_dodaj = "" Then
                Dim fstr_m As FileStream = File.Open(str_miniaturka_dodaj, FileMode.Open)
                'fstr_m = File.Open(str_miniaturka_dodaj, FileMode.Open)
                Dim miniatutka(fstr_m.Length) As Byte

                fstr_m.Read(miniatutka, 0, fstr_m.Length)
                'fstr.Write(plik, 0, plik.Length)
                fstr_m.Flush()
                fstr_m.Close()
                fstr_m.Dispose()

                Try
                    Cursor = Cursors.WaitCursor
                    Application.DoEvents()
                    wsWynikMiniaturka = ws.ZapiszMiniaturke(frmGlowna.sesja, str_tytul_pliku, Path.GetFileName(str_plik_dodaj), miniatutka, bool_wyswietlaj_www)
                    Cursor = Cursors.Default
                Catch ex As Exception
                    Cursor = Cursors.Default
                    MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, "Zapis miniaturki")
                    Return False
                End Try

                If wsWynikMiniaturka.status < 0 Then
                    MsgBox(wsWynikMiniaturka.status_opis, MsgBoxStyle.Critical, Me.Text)
                    Return False
                ElseIf wsWynikMiniaturka.status > 0 Then
                    MsgBox(wsWynikMiniaturka.status_opis, MsgBoxStyle.Exclamation, Me.Text)
                    Return False
                End If
            Else
                Try
                    Cursor = Cursors.WaitCursor
                    Application.DoEvents()
                    wsWynikMiniaturka = ws.ZapiszMiniaturke(frmGlowna.sesja, str_tytul_pliku, Path.GetFileName(str_plik_dodaj), Nothing, bool_wyswietlaj_www)
                    Cursor = Cursors.Default
                Catch ex As Exception
                    Cursor = Cursors.Default
                    MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, "Zapis miniaturki")
                    Return False
                End Try

                If wsWynikMiniaturka.status < 0 Then
                    MsgBox(wsWynikMiniaturka.status_opis, MsgBoxStyle.Critical, Me.Text)
                    Return False
                ElseIf wsWynikMiniaturka.status > 0 Then
                    MsgBox(wsWynikMiniaturka.status_opis, MsgBoxStyle.Exclamation, Me.Text)
                    Return False
                End If
            End If

            'fstr.Read(plik, 0, fstr.Length)
            'fstr.Write(plik, 0, plik.Length)
            fstr.Flush()
            fstr.Close()
            fstr.Dispose()

            plik = File.ReadAllBytes(str_plik_dodaj)

            'zapis pliku na serwer
            Try
                Cursor = Cursors.WaitCursor
                Application.DoEvents()
                wsWynik = ws.ZapiszAtachment(plik, Path.GetFileName(str_plik_dodaj))
                Cursor = Cursors.Default

            Catch ex As Exception
                Cursor = Cursors.Default
                MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
                Return False
            End Try

            If wsWynik.status <> 0 Then
                MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, Me.Text)
                Return False
            Else
                MsgBox(wsWynik.status_opis, MsgBoxStyle.Information, Me.Text)
            End If

            Return True

        End If



    End Function

    Private Function PobierzPlik(ByVal nazwa As String) As Boolean

        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik As wsCursorProf.PobierzPlikWynik
        'Dim stat As String

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
            Dim extension As String = Path.GetExtension(nazwa_pliku_baza)
            sfd.Filter = "*" & extension & "|*" & extension
            sfd.FileName = nazwa_pliku_baza

            'Dim sfd As New SaveFileDialog()
            'Dim typ_pliku As String = ""
            'typ_pliku = Mid(nazwa_pliku_baza, Len(nazwa_pliku_baza) - 3)
            'If typ_pliku = ".doc" Then
            '    sfd.Filter = "Word document (*.doc;*.docx)|*.doc;*.docx "
            '    sfd.FileName = nazwa_pliku_baza
            'ElseIf typ_pliku = ".pdf" Then
            '    sfd.Filter = "PDF document (*.pdf)|*.pdf"
            'End If

            If sfd.ShowDialog = Windows.Forms.DialogResult.OK Then
                'If Mid(sfd.FileName, Len(sfd.FileName) - 3) = typ_pliku Then
                Dim zapiszPath As String = sfd.FileName
                Dim fs As New FileStream(zapiszPath, FileMode.Create)
                fs.Write(plik, 0, plik.Length)
                fs.Flush()
                fs.Close()
                fs.Dispose()
                MsgBox("Poprawnie zapisano plik '" + zapiszPath + "'", MsgBoxStyle.Information, "Zapis pliku")
                'Else : MsgBox("Nie można zapisać tego pliku w formacie innym niż '" + typ_pliku + "'", MsgBoxStyle.Critical, "Błąd formatu pliku")
                '    Return False
            End If

        End If
        Return True
    End Function
    Private Function usun_pliki_baza() As Boolean
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        'ws.Url = frmGlowna.strWebservice
        Dim wsWynik As wsCursorProf.PlikiDoPobraniaUsunWynik
        Dim Xml As String = String.Empty
        For Each row As DataGridViewRow In dgvPliki.Rows

            If CType(row.Cells(0).Value, Boolean) = True Then
                Xml = Xml + String.Format("<row plik_id=""{0}""/>", row.Cells("plik_id").Value)
            End If
            'If CType(row.Cells("wybierz").Value, Boolean) Then
            '    Xml = Xml + String.Format("<row plik_id=""{0}""/>", row.Cells("plik_id").Value)
            '    'xml = xml + "<row user_id=""" + row.Cells("user_id") + """ />"
            'End If
        Next

        If Not Xml = String.Empty Then

            'odczyt z serwera
            Try
                Cursor = Cursors.WaitCursor
                Application.DoEvents()
                wsWynik = ws.PlikiDoPobraniaUsun(frmGlowna.sesja, Xml)
                Cursor = Cursors.Default
            Catch ex As Exception
                Cursor = Cursors.Default
                MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, "Usuwanie pliku")
                Return False
            End Try



            'dtPliki = wsWynik.dane.Tables(0)
            'Dim dv As New DataView(dtPliki)
            'dgvPliki.DataSource = dv
            'dgvPliki.Columns("plik_id").Visible = False
            'For Each col As DataGridViewColumn In dgvPliki.Columns
            '    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            'Next
            'dgvPliki.AllowUserToAddRows = False
            'dgvPliki.AllowUserToDeleteRows = False
            komunikat = wsWynik.status_opis

            If wsWynik.status = -1 Then
                ' MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation)
                Return False
            End If

            If wsWynik.centrala_out = 0 Then
                btnUsunPliki.Visible = True
            ElseIf wsWynik.centrala_out = 1 Then
                btnUsunPliki.Visible = False
            End If
            Return True
        Else
            Return False
        End If

    End Function

    Private Function usun_pliki_serwer() As Boolean

        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik As wsCursorProf.UsunAtachmentWynik


        Dim dt As New DataTable
        dt.Columns.Add("nazwa_pliku")
        For Each row As DataGridViewRow In dgvPliki.Rows
            If CType(row.Cells(0).Value, Boolean) = True Then
                dt.Rows.Add(row.Cells("nazwa_pliku").Value)
            End If
        Next

        For Each row As DataRow In dt.Rows

            'odczyt z serwera
            Try
                Cursor = Cursors.WaitCursor
                Application.DoEvents()
                wsWynik = ws.UsunAtachment(row.Item("nazwa_pliku").ToString)
                Cursor = Cursors.Default
            Catch ex As Exception
                Cursor = Cursors.Default
                MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, "Usuwanie pliku")
                Return False
            End Try


        Next
        Return True
    End Function


    Private Sub btnUsunPliki_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUsunPliki.Click
        Dim bCzyZaznaczono As Boolean
        For Each row As DataGridViewRow In dgvPliki.Rows
            If row.Cells(0).Value = True Then
                bCzyZaznaczono = True
            End If
        Next
        If Not bCzyZaznaczono Then
            MsgBox("Nie zaznaczono żadnego pliku do usunięcia!", MsgBoxStyle.Exclamation, "Usuwanie pliku")
            Exit Sub
        End If
        If Not usun_pliki_baza() Then
            MsgBox(komunikat, MsgBoxStyle.Critical, "Błąd przy usuwaniu pliku")
            Exit Sub
        Else
            If Not usun_pliki_serwer() Then
                MsgBox("Wystąpił problem z usunięciem plików z serwera.", MsgBoxStyle.Critical, "Błąd przy usuwaniu pliku")
                Exit Sub
            Else
                wczytaj()
                MsgBox(komunikat, MsgBoxStyle.Information, "Usuwanie pliku")
            End If
        End If

    End Sub

    Private Sub policzPliki()
        ilePlikow = 0

       
        For Each row As DataGridViewRow In dgvPliki.Rows
            If row.Cells("Wybierz").Value = True Then
                ilePlikow += 1
            End If
        Next

        If ilePlikow < 1 Then
            btnUsunPliki.Enabled = False
            btnUsunPliki.BackColor = Color.LightGray
        Else
            btnUsunPliki.Enabled = True
            btnUsunPliki.BackColor = Color.DodgerBlue
        End If
    End Sub
End Class