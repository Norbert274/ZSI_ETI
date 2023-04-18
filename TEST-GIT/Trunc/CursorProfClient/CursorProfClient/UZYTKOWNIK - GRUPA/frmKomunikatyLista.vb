Public Class frmKomunikatyLista
    Private dtKomunikaty As DataTable
    Private dtKomunikatyGrupy As DataTable
    Private intKomunikat As Integer
    Private strKomunikatRTF As String
    Private iloscKomunikatow As Integer
    Private kolorAktywnosci As Color = Color.DodgerBlue
    Private kolorBrakuAktywnosci As Color = Color.LightGray


    Private Sub btnZamknij_Click(sender As System.Object, e As System.EventArgs) Handles btnZamknij.Click
        Me.Close()
    End Sub

    Private Sub frmKomunikatyLista_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        wczytaj_komunikaty()
        policzIloscKomunikatow()
    End Sub


    Private Sub wczytaj_komunikaty()
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik As wsCursorProf.KomunikatyWczytajWynik
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.KomunikatyWczytaj(frmGlowna.sesja)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
            Exit Sub
        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, Me.Text)
            Exit Sub
        ElseIf wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, Me.Text)
        End If

        If wsWynik.dane.Tables.Count < 1 Then
            MsgBox("Błąd systemu - serwer nie zwrócił tabeli z komunikatami!" & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
            Exit Sub
        End If

        dtKomunikaty = wsWynik.dane.Tables(0)
        dgvKomunikaty.DataSource = dtKomunikaty
        If dgvKomunikaty.Columns.Contains("TRESC_KOMUNIKATU_RTF") Then dgvKomunikaty.Columns("TRESC_KOMUNIKATU_RTF").Visible = False
        If dgvKomunikaty.Columns.Contains("CZY_AKTYWNY") Then dgvKomunikaty.Columns("CZY_AKTYWNY").Visible = False
        dgvKomunikaty.AutoResizeColumns()

        '' kolorowanie wierszy
        '' jesli komunikat aktywny to kolorujemy na zielono, w przeciwnym razie na szaro
        For Each r As DataGridViewRow In dgvKomunikaty.Rows
            If r.Cells("CZY_AKTYWNY").Value = 1 Then
                r.DefaultCellStyle.BackColor = Color.LightGreen
            Else
                r.DefaultCellStyle.BackColor = Color.FromName("ButtonFace")
            End If
        Next

        dtKomunikatyGrupy = wsWynik.dane.Tables(1)

        If Not dgvKomunikaty.CurrentCell Is Nothing Then
            intKomunikat = dgvKomunikaty.Rows(dgvKomunikaty.CurrentCell.RowIndex).Cells("NUMER_KOMUNIKATU").Value
            strKomunikatRTF = dgvKomunikaty.Rows(dgvKomunikaty.CurrentCell.RowIndex).Cells("TRESC_KOMUNIKATU_RTF").Value
            rtbKomunikat.Rtf = strKomunikatRTF
            If intKomunikat > -1 Then
                Dim dv As New DataView(dtKomunikatyGrupy)
                dv.RowFilter = "KOMUNIKAT_ID is null or KOMUNIKAT_ID = " & intKomunikat.ToString
                dgvGrupy.DataSource = dv.ToTable
                For Each c As DataGridViewColumn In dgvGrupy.Columns
                    If c.Name = "wybierz" Then
                        c.ReadOnly = False
                    Else
                        c.ReadOnly = True
                    End If
                Next
                If dgvGrupy.Columns.Contains("KOMUNIKAT_ID") Then dgvGrupy.Columns("KOMUNIKAT_ID").Visible = False
                If dgvGrupy.Columns.Contains("GRUPA_ID") Then dgvGrupy.Columns("GRUPA_ID").Visible = False
                dgvGrupy.AutoResizeColumns()
            End If
        End If

    End Sub

    Private Sub btnNowy_Click(sender As System.Object, e As System.EventArgs) Handles btnNowy.Click

        Dim f As New frmKomunikatEdycja
        If f.ShowDialog = Windows.Forms.DialogResult.OK Then
            edytuj_komunikat(-1, f.ctrEdytorKomunikatu.rtbTekst.Text, f.ctrEdytorKomunikatu.rtbTekst.Rtf)
            wczytaj_komunikaty()
        End If
        policzIloscKomunikatow()
    End Sub

    Private Sub edytuj_komunikat(ByVal komunikat_id As Integer, _
                                 ByVal komunikat_tresc As String, _
                                 ByVal komunikat_tresc_RTF As String
                                 )

        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik As wsCursorProf.KomunikatEdytujWynik
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.KomunikatEdytuj(frmGlowna.sesja, komunikat_id, komunikat_tresc, komunikat_tresc_RTF)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
            Exit Sub
        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, Me.Text)
            Exit Sub
        ElseIf wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Information, Me.Text)
        End If

    End Sub

    Private Sub usun_komunikat(ByVal komunikat_id As Integer)
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik As wsCursorProf.KomunikatUsunWynik
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.KomunikatUsun(frmGlowna.sesja, komunikat_id)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
            Exit Sub
        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, Me.Text)
            Exit Sub
        ElseIf wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Information, Me.Text)
        End If

    End Sub

    Private Sub przypisz_komunikat_do_grup(ByVal komunikat_id As Integer, _
                                           ByVal grupy_xml As String)
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik As wsCursorProf.KomunikatGrupyPrzypiszWynik
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.KomunikatGrupyPrzypisz(frmGlowna.sesja, komunikat_id, grupy_xml)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
            Exit Sub
        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, Me.Text)
            Exit Sub
        ElseIf wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Information, Me.Text)
        End If

    End Sub

    Private Sub dgvKomunikaty_CellClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvKomunikaty.CellClick
        If e.RowIndex > -1 Then
            intKomunikat = dgvKomunikaty.Rows(e.RowIndex).Cells("NUMER_KOMUNIKATU").Value
            strKomunikatRTF = dgvKomunikaty.Rows(e.RowIndex).Cells("TRESC_KOMUNIKATU_RTF").Value
            rtbKomunikat.Rtf = strKomunikatRTF

            Dim dv As New DataView(dtKomunikatyGrupy)
            dv.RowFilter = "KOMUNIKAT_ID is null or KOMUNIKAT_ID = " & intKomunikat.ToString
            dgvGrupy.DataSource = dv.ToTable
            For Each c As DataGridViewColumn In dgvGrupy.Columns
                If c.Name = "wybierz" Then
                    c.ReadOnly = False
                Else
                    c.ReadOnly = True
                End If
            Next
            If dgvGrupy.Columns.Contains("KOMUNIKAT_ID") Then dgvGrupy.Columns("KOMUNIKAT_ID").Visible = False
            If dgvGrupy.Columns.Contains("GRUPA_ID") Then dgvGrupy.Columns("GRUPA_ID").Visible = False
            dgvGrupy.AutoResizeColumns()

        End If
    End Sub

    Private Sub frmEdytuj_Click(sender As System.Object, e As System.EventArgs) Handles btnEdytuj.Click

        If Not dgvKomunikaty.CurrentCell Is Nothing Then
            intKomunikat = dgvKomunikaty.Rows(dgvKomunikaty.CurrentCell.RowIndex).Cells("NUMER_KOMUNIKATU").Value
            strKomunikatRTF = dgvKomunikaty.Rows(dgvKomunikaty.CurrentCell.RowIndex).Cells("TRESC_KOMUNIKATU_RTF").Value
            If intKomunikat > -1 Then
                Dim f As New frmKomunikatEdycja
                f.rtf_komunikat = strKomunikatRTF
                If f.ShowDialog = Windows.Forms.DialogResult.OK Then
                    edytuj_komunikat(intKomunikat, f.ctrEdytorKomunikatu.rtbTekst.Text, f.ctrEdytorKomunikatu.rtbTekst.Rtf)
                    wczytaj_komunikaty()
                End If
            End If
        Else
            MsgBox("Nie wybrano komunikatu do edycji!", MsgBoxStyle.Exclamation, "Brak komunikatu do edycji")
        End If
    End Sub

    Private Sub btnUsun_Click(sender As System.Object, e As System.EventArgs) Handles btnUsun.Click
        If Not dgvKomunikaty.CurrentCell Is Nothing Then
            intKomunikat = dgvKomunikaty.Rows(dgvKomunikaty.CurrentCell.RowIndex).Cells("NUMER_KOMUNIKATU").Value
            strKomunikatRTF = dgvKomunikaty.Rows(dgvKomunikaty.CurrentCell.RowIndex).Cells("TRESC_KOMUNIKATU_RTF").Value

            Dim tresc_wiadomosci_dla_aktywny As String = ""
            If dgvKomunikaty.Rows(dgvKomunikaty.CurrentCell.RowIndex).Cells("CZY_AKTYWNY").Value = 1 Then
                tresc_wiadomosci_dla_aktywny = "Komunikat o numerze " & intKomunikat.ToString & _
                    " jest aktualnie przydzielony do niektórych grup!" & vbNewLine & vbNewLine
            End If

            If intKomunikat > -1 Then
                If MsgBox(tresc_wiadomosci_dla_aktywny & "Czy na pewno chcesz usunąć ten komunikat?", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation, "Usuwanie komunikatu") = MsgBoxResult.Yes Then
                    usun_komunikat(intKomunikat)
                    wczytaj_komunikaty()
                End If
            End If
        End If

        policzIloscKomunikatow()
    End Sub

    Private Sub btnPrzypiszKomunikatDoGrup_Click(sender As System.Object, e As System.EventArgs) Handles btnPrzypiszKomunikatDoGrup.Click
        Dim xml_grupy As String = ""
        If Not dgvKomunikaty.CurrentCell Is Nothing Then
            intKomunikat = dgvKomunikaty.Rows(dgvKomunikaty.CurrentCell.RowIndex).Cells("NUMER_KOMUNIKATU").Value
            If intKomunikat > -1 Then
                For Each r As DataGridViewRow In dgvGrupy.Rows
                    xml_grupy += "<row grupa_id=""" & r.Cells("GRUPA_ID").Value & """ wlaczony=""" & IIf(r.Cells("wybierz").Value, 1, 0) & """/>"
                Next
                przypisz_komunikat_do_grup(intKomunikat, xml_grupy)
                wczytaj_komunikaty()
            End If
        Else
            MsgBox("Nie wybrano komunikatu do edycji!", MsgBoxStyle.Exclamation, "Brak komunikatu do edycji")
        End If
    End Sub

    Private Sub policzIloscKomunikatow()
        iloscKomunikatow = 0

        iloscKomunikatow = dgvKomunikaty.RowCount

        If iloscKomunikatow < 1 Then
            btnEdytuj.BackColor = kolorBrakuAktywnosci
            btnEdytuj.Enabled = False
            btnUsun.BackColor = kolorBrakuAktywnosci
            btnUsun.Enabled = False
            btnPrzypiszKomunikatDoGrup.BackColor = kolorBrakuAktywnosci
            btnPrzypiszKomunikatDoGrup.Enabled = False
            dgvGrupy.DataSource = Nothing
            rtbKomunikat.Clear()

        Else
            btnEdytuj.BackColor = kolorAktywnosci
            btnEdytuj.Enabled = True
            btnUsun.BackColor = kolorAktywnosci
            btnUsun.Enabled = True
            btnPrzypiszKomunikatDoGrup.BackColor = kolorAktywnosci
            btnPrzypiszKomunikatDoGrup.Enabled = True
        End If

    End Sub
 
End Class