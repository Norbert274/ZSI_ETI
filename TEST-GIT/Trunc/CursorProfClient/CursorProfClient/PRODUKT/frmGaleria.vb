Imports System.Reflection
Imports System.IO
Imports System.Data
Public Class frmGaleria
    Public inputIdSKU As Integer
    Public sku As String
    Public nazwa_sku As String
    Public sciezka As String
    Public dtZdjecia As DataTable
    Public frmRodzic As Form
    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        Dim ofd As New OpenFileDialog
        ofd.Filter = "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg"
        ofd.Multiselect = False
        If ofd.ShowDialog() = Windows.Forms.DialogResult.OK Then
            sciezka = ofd.FileName
            txtSciezka.Text = sciezka

            Dim im As Image = Image.FromFile(sciezka)
            picZdjeciePodglad.Image = im
            picZdjeciePodglad.SizeMode = PictureBoxSizeMode.Zoom
            btnDodajZdjecie.Enabled = True
            txtNazwa.Text = sku
        End If
    End Sub

    Private Function zapisz_zdjecie() As Boolean

        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik As wsCursorProf.ZdjecieDodajWynik
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            Dim zdjecie As Byte()
            Dim zdjecieNazwa As String = String.Empty

            If File.Exists(sciezka) Then
                zdjecie = File.ReadAllBytes(sciezka)
                zdjecieNazwa = Path.GetFileName(sciezka)
            Else
                zdjecie = Nothing
            End If

            wsWynik = ws.ZdjecieDodaj(frmGlowna.sesja, inputIdSKU, sku, zdjecieNazwa, txtNazwa.Text, zdjecie)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, "Dodawanie zdjęcia produktu")
            Me.Close()
            Return False
        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, "Dodawanie zdjęcia produktu")
            Return False
        ElseIf wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, "Dodawanie zdjęcia produktu")
        End If


        Return True
    End Function

    Private Sub btnDodajZdjecie_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDodajZdjecie.Click

        ' sprawdzamy, czy użytkownik wybrał plik
        If txtSciezka.Text = String.Empty Then
            MsgBox("Nie wybrano żadnego zdjęcia do zapisania!", MsgBoxStyle.Exclamation, "Brak zdjęcia")
            Exit Sub
        End If
        ' sprawdzamy, czy użytkownik podał nazwę pliku
        If txtNazwa.Text = String.Empty Then
            MsgBox("Nie wprowadzono nazwy dla wybranego zdjęcia!", MsgBoxStyle.Exclamation, "Brak nazwy zdjęcia")
            Exit Sub
        End If
        ' zapisujemy zdjęcie do bazy danych
        If Not zapisz_zdjecie() Then
            'MsgBox("Wystąpił problem. Nie zapisano wybranego zdjęcia!", MsgBoxStyle.Critical, "Problem z zapisaniem zdjęcia")
            txtSciezka.Text = ""
            txtNazwa.Text = ""
            picZdjeciePodglad.Image = Nothing
            Exit Sub
        End If
        If ctrImgGaleria.ilosc_zdjec > 1 Then
            ctrImgGaleria.nr_biezacego_zdjecia = ctrImgGaleria.ilosc_zdjec + 1
        Else
            ctrImgGaleria.nr_biezacego_zdjecia = 1
        End If

        ctrImgGaleria.PokazZdjecie()
        If ctrImgGaleria.ilosc_zdjec = 1 Then ctrImgGaleria.chkIsThumbnail.Checked = True
        dtZdjecia = ctrImgGaleria.dtZdjecia
        btnUsunZdjecie.Enabled = True
        btnUsunZdjecie.BackColor = ctrImgGaleria._blue
        ' frmRodzic.Show()
        picZdjeciePodglad.Image = Nothing

        'odświeżamy kontrolkę ze zdjęciami w formularzu frmEdytujSKU
        If Not frmRodzic Is Nothing Then
            Dim m As MethodInfo() = frmRodzic.GetType.GetMethods()
            For licznik As Integer = 0 To m.GetUpperBound(0)
                If m(licznik).Name = "odswiezZdjecia" Then
                    m(licznik).Invoke(frmRodzic, Nothing)
                End If
            Next
        End If
    End Sub

    Private Sub btnUsunZdjecie_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUsunZdjecie.Click
        If Not usun_zdjecie() Then
            Exit Sub
        End If
        btnUsunZdjecie.Enabled = True
        btnUsunZdjecie.BackColor = ctrImgGaleria._blue
        Dim wybrane_zdjecie As Integer = ctrImgGaleria.nr_biezacego_zdjecia
        If wybrane_zdjecie = 1 And ctrImgGaleria.ilosc_zdjec = 1 Then
            btnUsunZdjecie.Enabled = False
            btnUsunZdjecie.BackColor = Color.LightGray
            ctrImgGaleria.PokazZdjecie()
            ctrImgGaleria.nr_biezacego_zdjecia = 0

            'odświeżamy kontrolkę ze zdjęciami w formularzu frmEdytujSKU
            If Not frmRodzic Is Nothing Then
                Dim m As MethodInfo() = frmRodzic.GetType.GetMethods()
                For licznik As Integer = 0 To m.GetUpperBound(0)
                    If m(licznik).Name = "odswiezZdjecia" Then
                        m(licznik).Invoke(frmRodzic, Nothing)
                    End If
                Next
            End If

            dtZdjecia = Nothing
            Exit Sub
        End If
        If ctrImgGaleria.nr_biezacego_zdjecia > 1 Then
            ctrImgGaleria.nr_biezacego_zdjecia = wybrane_zdjecie - 1
        Else : ctrImgGaleria.nr_biezacego_zdjecia = 1
        End If

        ctrImgGaleria.PokazZdjecie()
        dtZdjecia = ctrImgGaleria.dtZdjecia

        'odświeżamy kontrolkę ze zdjęciami w formularzu frmEdytujSKU
        If Not frmRodzic Is Nothing Then
            DirectCast(frmRodzic, frmEdytujSKU).CtrImgGaleriaEdycjaSKU.nr_biezacego_zdjecia = ctrImgGaleria.nr_biezacego_zdjecia
            Dim m As MethodInfo() = frmRodzic.GetType.GetMethods()
            For licznik As Integer = 0 To m.GetUpperBound(0)
                If m(licznik).Name = "odswiezZdjecia" Then
                    m(licznik).Invoke(frmRodzic, Nothing)
                End If
            Next
        End If

    End Sub

    Private Function usun_zdjecie() As Boolean

        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik As wsCursorProf.ZdjecieUsunWynik
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()

            ' ustalamy id dla usuwanego zdjęcia
            Dim id_zdjecia As Integer
            id_zdjecia = dtZdjecia.Rows(ctrImgGaleria.nr_biezacego_zdjecia - 1).Item("ID")

            wsWynik = ws.ZdjecieUsun(frmGlowna.sesja, id_zdjecia)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, "Usuwanie zdjęć produktu")

            Return False
        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, "Usuwanie zdjęć produktu")
            Return False
        ElseIf wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, "Usuwanie zdjęć produktu")
        End If

        Return True
    End Function

    Private Sub frmGaleria_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        ctrImgGaleria.frmRodzic = Me
        ctrImgGaleria.inputIdSKU = inputIdSKU
        ctrImgGaleria.PokazZdjecie()
        'If ctrImgGaleria.dtZdjecia.Rows.Count = 0 Then
        '    btnUsunZdjecie.Enabled = False
        'End If
        lblSKU.Text = nazwa_sku & " [SKU: " & sku & "]"
        Me.Text = "Galeria zdjęć dla produktu - " & nazwa_sku & " [SKU: " & sku & "]"
        dtZdjecia = ctrImgGaleria.dtZdjecia
        If ctrImgGaleria.ilosc_zdjec > 1 Then
            ctrImgGaleria.btnStart.Enabled = True
            ctrImgGaleria.btnKoniec.Enabled = True
            ctrImgGaleria.btnNastepne.Enabled = True
            ctrImgGaleria.btnPoprzednie.Enabled = True
            ctrImgGaleria.btnStart.BackColor = ctrImgGaleria._blue
            ctrImgGaleria.btnKoniec.BackColor = ctrImgGaleria._blue
            ctrImgGaleria.btnNastepne.BackColor = ctrImgGaleria._blue
            ctrImgGaleria.btnPoprzednie.BackColor = ctrImgGaleria._blue
        ElseIf ctrImgGaleria.ilosc_zdjec = 1 Then
            ctrImgGaleria.btnStart.Enabled = False
            ctrImgGaleria.btnKoniec.Enabled = False
            ctrImgGaleria.btnNastepne.Enabled = False
            ctrImgGaleria.btnPoprzednie.Enabled = False
            ctrImgGaleria.btnStart.BackColor = Color.LightGray
            ctrImgGaleria.btnKoniec.BackColor = Color.LightGray
            ctrImgGaleria.btnNastepne.BackColor = Color.LightGray
            ctrImgGaleria.btnPoprzednie.BackColor = Color.LightGray
        ElseIf ctrImgGaleria.ilosc_zdjec = 0 Then
            btnUsunZdjecie.Enabled = False
            btnUsunZdjecie.BackColor = Color.LightGray
        End If

        If ctrImgGaleria.ilosc_zdjec > 1 And ctrImgGaleria.nr_biezacego_zdjecia = 1 Then
            ctrImgGaleria.btnStart.Enabled = False
            ctrImgGaleria.btnPoprzednie.Enabled = False
            ctrImgGaleria.btnStart.BackColor = Color.LightGray
            ctrImgGaleria.btnPoprzednie.BackColor = Color.LightGray
        ElseIf ctrImgGaleria.ilosc_zdjec > 1 And ctrImgGaleria.nr_biezacego_zdjecia = ctrImgGaleria.ilosc_zdjec Then
            ctrImgGaleria.btnKoniec.Enabled = False
            ctrImgGaleria.btnNastepne.Enabled = False
            ctrImgGaleria.btnKoniec.BackColor = Color.LightGray
            ctrImgGaleria.btnNastepne.BackColor = Color.LightGray
        End If

    End Sub


    
    Private Sub btnZamknij_Click(sender As System.Object, e As System.EventArgs) Handles btnZamknij.Click
        Me.Close()
    End Sub
End Class