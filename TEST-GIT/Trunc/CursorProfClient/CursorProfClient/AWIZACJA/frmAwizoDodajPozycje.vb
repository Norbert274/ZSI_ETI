Public Class frmAwizoDodajPozycje

    Private dtSKU As New DataTable
    Private strSKU As String
    Private strSKU_nazwa As String
    Private strKlasa As String
    Private intGrupaArtQ As Integer
    Private intUtylizacja As Integer
    Private strKategoria As String
    Private strBrand As String
    Private strJM As String
    Public dtWybraneSKU As New DataTable
    Private dtkategorie As DataTable
    Private dtGrupyArtykulow As DataTable
    Private dtBrand As DataTable
    Private dtJM As DataTable
    Private licznikZaznaczonychSKU As Integer = 0
    Public dtGrupy As DataTable
    Public Grupa_id As Integer
    Public decimalCena As New Decimal
    Private bWczytaj As Boolean = False
    Private opisUsunProdukt As String = ""
    Private statusUsunProdukt As Integer = -1
    Private sku As String
    Private iloscSkuPoSfiltrowaniu As Integer = 0
    Private rowIndex As Integer = 0

    Private Sub frmAwizoDodajPozycje_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

        '' Na zdarzeniu Shown formy tworzymy kolumny dla dtWybraneSKU
        dtWybraneSKU.Columns.Add("sku")
        dtWybraneSKU.Columns.Add("nazwa")
        dtWybraneSKU.Columns.Add("ilosc")
        dtWybraneSKU.Columns.Add("GRUPA_ID", GetType(Integer))
        'dtWybraneSKU.Columns.Add("cena")
        '' wczytujemy z serwera listę SKU
        'If Not wczytaj() Then Exit Sub
        wczytaj()
        ''
        ''wypełniamy dgv danymi z SKU
        dgv.DataSource = dtSKU
        dgv.Columns(0).Width = 50
        dgv.Columns("sku").Width = 110
        dgv.Columns("nazwa").Width = 440
        Dim i As Integer
        For i = 0 To dgv.Columns.Count - 1
            If dgv.Columns(i).Name <> "wybierz" Then
                dgv.Columns(i).ReadOnly = True
            End If
            If dgv.Columns(i).Name = "SKU_ID" Then
                dgv.Columns(i).Visible = False
            End If
        Next

    End Sub

    Private Function wczytaj() As Boolean

        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        'ws.Url = frmGlowna.strWebservice
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
            Me.Close()
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

        btnUsunProdukt.Enabled = False
        btnUsunProdukt.BackColor = Color.LightGray

        If wsWynik.dane.Tables.Count > 0 Then
            dtSKU = wsWynik.dane.Tables(0)
        Else
            MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał listy produktów." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, "Wczytanie listy produktów")
            'Return False
            'Me.Close()
            'Exit Function
        End If

        If wsWynik.dane.Tables.Count > 1 And wsWynik.dane.Tables(1).Rows.Count > 0 Then
            dtkategorie = wsWynik.dane.Tables(1)
        Else
            MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał listy dostępnych kategorii." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, "Wczytanie listy kategorii")
            Return False
            Me.Close()
            Exit Function
        End If

        If wsWynik.dane.Tables.Count > 2 And wsWynik.dane.Tables(2).Rows.Count > 0 Then
            dtBrand = wsWynik.dane.Tables(2)
        Else
            MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał listy dostępnych brandów." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, "Wczytanie listy brandów")
            Return False
            Me.Close()
            Exit Function
        End If

        If wsWynik.dane.Tables.Count > 3 And wsWynik.dane.Tables(3).Rows.Count > 0 Then
            dtJM = wsWynik.dane.Tables(3)
        Else
            MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał listy dostępnych jednostek miary." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, "Wczytanie listy jednostek miary")
            Return False
            Me.Close()
            Exit Function
        End If

        If wsWynik.dane.Tables.Count > 4 And wsWynik.dane.Tables(4).Rows.Count > 0 Then
            dtGrupyArtykulow = wsWynik.dane.Tables(4)
        Else
            MsgBox("Błąd wewnętrzny systemu. Serwer nie przesłał listy dostępnych grup artykułów." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, "Wczytanie listy grup artykułów")
            Return False
            Me.Close()
            Exit Function
        End If

        bWczytaj = True
        cmbGrupa.DataSource = dtGrupy
        cmbGrupa.DisplayMember = "GRUPA"
        cmbGrupa.ValueMember = "GRUPA_ID"
        If Grupa_id > 0 Then
            cmbGrupa.SelectedValue = Grupa_id
        End If
        bWczytaj = False
        Return True
    End Function

    Private Sub Filtruj()
        iloscSkuPoSfiltrowaniu = 0

        Dim dv As New DataView(dtSKU)
        Dim filtr As String = String.Empty
        If txtWyszukajSKU.Text <> "" Then
            filtr = "(sku LIKE '%" & txtWyszukajSKU.Text & "%' or nazwa LIKE '%" & txtWyszukajSKU.Text & "%')"
        Else
            filtr = String.Empty
        End If

        dv.RowFilter = filtr
        dgv.DataSource = dv

        iloscSkuPoSfiltrowaniu = dv.Count
    End Sub

    Private Sub txtWyszukajSKU_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtWyszukajSKU.TextChanged
        Filtruj()
    End Sub

    Private Sub btnDodajPozycje_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDodajPozycje.Click

        If dgv.DataSource.GetType() = GetType(DataView) Then
            If CType(dgv.DataSource, DataView).Table.Select("wybierz=1").Length = 0 Then
                MsgBox("Nie wybrano żadnego produktu", MsgBoxStyle.Exclamation, "Brak wybranych pozycji")
                Exit Sub
            End If
        Else
            If CType(dgv.DataSource, DataTable).Select("wybierz=1").Length = 0 Then
                MsgBox("Nie wybrano żadnego produktu", MsgBoxStyle.Exclamation, "Brak wybranych pozycji")
                Exit Sub
            End If
        End If

        For Each row As DataGridViewRow In dgv.Rows
            If row.Cells("wybierz").Value = True Then
                dtWybraneSKU.Rows.Add(row.Cells("sku").Value, row.Cells("nazwa").Value, 0, Grupa_id)
            End If
        Next
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnAnuluj_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAnuluj.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub btnDodajNowyProdukt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDodajNowyProdukt.Click
        Dim f As New frmSKUNowy

        f.dtKategorie = dtkategorie
        f.dtBrand = dtBrand
        f.dtJM = dtJM
        f.dtGrupyArtykulowQ = dtGrupyArtykulow
        If iloscSkuPoSfiltrowaniu = 0 Then
            f.skuProponowane = txtWyszukajSKU.Text
        End If
        f.ShowDialog()
        If f.DialogResult = System.Windows.Forms.DialogResult.OK Then
            strSKU = f.txtNrSKU.Text
            strSKU_nazwa = f.txtNazwaSku.Text
            strKlasa = f.cmbKlasa.SelectedItem
            strKategoria = f.cmbKategoria.SelectedItem
            strBrand = f.cmbBrand.SelectedItem
            strJM = f.cmbJM.SelectedItem
            decimalCena = f.cena
            intGrupaArtQ = f.cmbGrupaArtykulow.SelectedValue
            If zapisz_nowy_produkt_QGUAR() Then
                wczytaj()
                dgv.DataSource = dtSKU
                For i = 0 To dgv.Columns.Count - 1
                    If dgv.Columns(i).Name = "SKU_ID" Then
                        dgv.Columns(i).Visible = False
                    End If
                Next
                MsgBox("Pomyślnie założono nowy produkt: " & f.txtNrSKU.Text & " o nazwie (" & f.txtNazwaSku.Text & ")", MsgBoxStyle.Information, "Nowy produkt")
            End If
        End If
    End Sub

    Private Function zapisz_nowy_produkt_QGUAR() As Boolean

        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        ' ws.Url = frmGlowna.strWebservice
        Dim wsWynik As wsCursorProf.ZalozNowyProduktQGUARWynik

        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.ZalozNowyProduktQGUAR(frmGlowna.sesja, strSKU, strSKU_nazwa, strKlasa, strKategoria, strBrand, strJM, decimalCena, intGrupaArtQ)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, "Zakładanie nowego produktu")
            Return False
            Me.Close()
            Exit Function
        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, "Zakładanie nowego produktu")
            Return False
            Exit Function
        End If
        If wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, "Zakładanie nowego produktu")
            Return False
            Exit Function
        End If
        Return True
    End Function

    Private Sub btnZalozNoweProduktyZExcela_Click(sender As System.Object, e As System.EventArgs) Handles btnZalozNoweProduktyZExcela.Click
        Dim f As New frmSKUNoweZExcela
        f.dtGrupyArtQ = dtGrupyArtykulow
        f.ShowDialog()
        If Not wczytaj() Then Me.Close()

        ''wypełniamy dgv danymi z SKU
        dgv.DataSource = dtSKU
        dgv.Columns(0).Width = 50
        'dgv.Columns("sku").Width = 110
        'dgv.Columns("nazwa").Width = 200
        Dim i As Integer
        For i = 0 To dgv.Columns.Count - 1
            If dgv.Columns(i).Name <> "wybierz" Then
                dgv.Columns(i).ReadOnly = True
            End If
            If dgv.Columns(i).Name = "SKU_ID" Then
                dgv.Columns(i).Visible = False
            End If
        Next
    End Sub


    Private Sub cmbGrupa_SelectedValueChanged(sender As Object, e As System.EventArgs) Handles cmbGrupa.SelectedValueChanged
        If cmbGrupa.ValueMember <> "" And bWczytaj = False Then
            Grupa_id = cmbGrupa.SelectedValue
        End If
    End Sub


    'Private Sub dgv_CellContentClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellContentClick
    '    If dgv.Columns(e.ColumnIndex).Name = "wybierz" Then
    '        sprawdzSkuDoUsuniecia()
    '    End If
    'End Sub
    Private Sub sprawdzSkuDoUsuniecia()

        licznikZaznaczonychSKU = 0
        dgv.EndEdit()
        dgv.RefreshEdit()
        For Each row As DataGridViewRow In dgv.Rows
            If row.Cells("wybierz").Value = True Then
                sku = row.Cells("sku").Value
                licznikZaznaczonychSKU = licznikZaznaczonychSKU + 1
                rowIndex = row.Index
            End If
        Next

        If licznikZaznaczonychSKU <> 1 Then
            btnUsunProdukt.Enabled = False
            btnUsunProdukt.BackColor = Color.LightGray
            opisUsunProdukt = "Przy usuwaniu produktu należy wybrać dokładnie jedną pozycję."
        Else
            System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
            System.Net.ServicePointManager.Expect100Continue = False
            ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
            ws.Proxy.Credentials = CredentialCache.DefaultCredentials
            ' ws.Url = frmGlowna.strWebservice
            Dim wsWynik As wsCursorProf.SkuUsunWczytajWynik

            Try
                Cursor = Cursors.WaitCursor
                Application.DoEvents()
                wsWynik = ws.SkuUsunWczytaj(frmGlowna.sesja, sku)
                Cursor = Cursors.Default
            Catch ex As Exception
                Cursor = Cursors.Default
            End Try

            If wsWynik.status = 1 Then
                MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, Me.Text)
                Exit Sub
            End If

            opisUsunProdukt = wsWynik.status_opis
            statusUsunProdukt = wsWynik.status

            btnUsunProdukt.Enabled = True
            btnUsunProdukt.BackColor = Color.DodgerBlue

        End If

        ToolTip2.SetToolTip(btnUsunProdukt, opisUsunProdukt)

    End Sub

    Private Sub btnUsunPozycje_Click(sender As Object, e As System.EventArgs) Handles btnUsunProdukt.Click
        Dim statusUsuwania As Integer = 0

        If statusUsunProdukt < 0 Then
            MsgBox(opisUsunProdukt, MsgBoxStyle.Exclamation, "Usuwanie produktu")
        Else
            System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
            System.Net.ServicePointManager.Expect100Continue = False
            ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
            ws.Proxy.Credentials = CredentialCache.DefaultCredentials
            ' ws.Url = frmGlowna.strWebservice
            Dim wsWynik As wsCursorProf.SkuUsunWynik

            Try
                Cursor = Cursors.WaitCursor
                Application.DoEvents()
                wsWynik = ws.SkuUsun(frmGlowna.sesja, sku)
                Cursor = Cursors.Default
            Catch ex As Exception
                Cursor = Cursors.Default
                MsgBox("Błąd komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, "Zakładanie nowego produktu")
                Me.Close()
                Exit Sub
            End Try

            statusUsuwania = wsWynik.status

            If wsWynik.status < 0 Then
                MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, "Usuwanie produktu")
                Exit Sub
            Else
                MsgBox(wsWynik.status_opis, MsgBoxStyle.Information, "Usuwanie produktu")
            End If
        End If

        wczytaj()
        Filtruj()

        If statusUsuwania < 0 Then
            dgv.CurrentCell = dgv.Rows(rowIndex).Cells("wybierz")
            dgv.Rows(rowIndex).Cells("wybierz").Value = 1

        End If
      
        btnUsunProdukt.Enabled = False
        btnUsunProdukt.BackColor = Color.LightGray

        sprawdzSkuDoUsuniecia()


    End Sub
End Class