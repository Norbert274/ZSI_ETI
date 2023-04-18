Imports System.Reflection
Imports System.IO
Imports System.Data
Public Class ctrImgGaleria

    Public sku As String
    Public inputIdSKU As Integer
    Public sciezka As String
    Public dtZdjecia As DataTable
    Public ilosc_zdjec As Integer
    Public nr_biezacego_zdjecia As Integer = 1
    Public frmRodzic As Form
    Private _img As Image
    Public _blue As Color = Color.DodgerBlue


    Private Sub btnStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStart.Click

        btnStart.Enabled = True
        btnStart.BackColor = _blue
        lblNrBiezacegoZdjecia.Text = 1
        nr_biezacego_zdjecia = lblNrBiezacegoZdjecia.Text
        PokazZdjecie()
    End Sub

    Private Sub btnKoniec_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKoniec.Click

        lblNrBiezacegoZdjecia.Text = ilosc_zdjec
        nr_biezacego_zdjecia = lblNrBiezacegoZdjecia.Text
        PokazZdjecie()
    End Sub

    Private Sub btnPoprzednie_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPoprzednie.Click

        lblNrBiezacegoZdjecia.Text = lblNrBiezacegoZdjecia.Text - 1
        nr_biezacego_zdjecia = lblNrBiezacegoZdjecia.Text
        PokazZdjecie()
    End Sub

    Private Sub btnNastepne_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNastepne.Click

        lblNrBiezacegoZdjecia.Text = lblNrBiezacegoZdjecia.Text + 1
        nr_biezacego_zdjecia = lblNrBiezacegoZdjecia.Text
        PokazZdjecie()
    End Sub

    Public Sub PokazZdjecie()

        If Not wczytaj() Then
            lblBrakZdjecia.Visible = True
            lblIloscZdjec.Visible = False
            lblZ.Visible = False
            lblNrBiezacegoZdjecia.Visible = False
            btnKoniec.Enabled = False
            btnStart.Enabled = False
            btnNastepne.Enabled = False
            btnPoprzednie.Enabled = False
            btnKoniec.BackColor = Color.LightGray
            btnStart.BackColor = Color.LightGray
            btnNastepne.BackColor = Color.LightGray
            btnPoprzednie.BackColor = Color.LightGray
            picZdjecie.Image = Nothing
            chkIsThumbnail.Checked = False
            chkIsThumbnail.Enabled = False
            dtZdjecia = Nothing
            Exit Sub
        End If

        lblBrakZdjecia.Visible = False
        lblIloscZdjec.Visible = True
        lblZ.Visible = True
        lblNrBiezacegoZdjecia.Visible = True
        btnKoniec.Enabled = True
        btnStart.Enabled = True
        btnNastepne.Enabled = True
        btnPoprzednie.Enabled = True
        btnKoniec.BackColor = _blue
        btnStart.BackColor = _blue
        btnNastepne.BackColor = _blue
        btnPoprzednie.BackColor = _blue
        chkIsThumbnail.Enabled = True

        ilosc_zdjec = dtZdjecia.Rows.Count
        lblIloscZdjec.Text = ilosc_zdjec
        lblNrBiezacegoZdjecia.Text = nr_biezacego_zdjecia

        If ilosc_zdjec > 1 Then
            btnStart.Enabled = True
            btnKoniec.Enabled = True
            btnNastepne.Enabled = True
            btnPoprzednie.Enabled = True
            btnStart.BackColor = _blue
            btnKoniec.BackColor = _blue
            btnNastepne.BackColor = _blue
            btnPoprzednie.BackColor = _blue
        ElseIf ilosc_zdjec = 1 Then
            btnStart.Enabled = False
            btnKoniec.Enabled = False
            btnNastepne.Enabled = False
            btnPoprzednie.Enabled = False
            btnStart.BackColor = Color.LightGray
            btnKoniec.BackColor = Color.LightGray
            btnNastepne.BackColor = Color.LightGray
            btnPoprzednie.BackColor = Color.LightGray
        End If

        If ilosc_zdjec > 1 And nr_biezacego_zdjecia = 1 Then
            btnStart.Enabled = False
            btnPoprzednie.Enabled = False
            btnStart.BackColor = Color.LightGray
            btnPoprzednie.BackColor = Color.LightGray
        ElseIf ilosc_zdjec > 1 And nr_biezacego_zdjecia = ilosc_zdjec Then
            btnKoniec.Enabled = False
            btnNastepne.Enabled = False
            btnKoniec.BackColor = Color.LightGray
            btnNastepne.BackColor = Color.LightGray
        End If

        Dim arrPicture() As Byte
        If dtZdjecia.Rows.Count > 0 Then
            Try
                arrPicture = CType(dtZdjecia.Rows(nr_biezacego_zdjecia - 1).Item("ZDJECIE"), Byte())
                Dim ms As New IO.MemoryStream(arrPicture)
                _img = Image.FromStream(ms)
                picZdjecie.Image = _img
                chkIsThumbnail.Checked = dtZdjecia.Rows(nr_biezacego_zdjecia - 1).Item("IsThumbnail")
                picZdjecie.SizeMode = PictureBoxSizeMode.Zoom
            Catch ex As Exception
                MsgBox("Błąd: " & ex.Message)
            End Try
           
        End If

    End Sub

    Private Function wczytaj() As Boolean

        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik As wsCursorProf.ZdjeciaWczytajWynik
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.ZdjeciaWczytaj(frmGlowna.sesja, inputIdSKU)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)

            Return False
        End Try

        If wsWynik.status < 0 Then
            Return False
        End If
        dtZdjecia = wsWynik.dane.Tables(0)
        sku = dtZdjecia.Rows(0).Item("SKU")

        Return True
    End Function

    Private Sub utworzMiniature(ByVal sesja As Byte(), ByVal SKU_ID As Integer, ByVal ID As Integer, ByVal Image As Byte())


        ' wysyłanie miniatury na serwer 
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik As wsCursorProf.utworzMiniatureWynik
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.utworzMiniature(frmGlowna.sesja, SKU_ID, ID, Image)
            Cursor = Cursors.Default

            If wsWynik.status <> 0 Then
                MsgBox(wsWynik.status_opis.ToString)
            End If

        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
        End Try
    End Sub

    Private Sub chkIsThumbnail_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkIsThumbnail.CheckedChanged
        Dim mImage As Byte() = {}

        If chkIsThumbnail.Checked And dtZdjecia.Rows(nr_biezacego_zdjecia - 1).Item("IsThumbnail") = 0 Then
            ' zdjęcie zaznaczono jako miniaturkę

            mImage = CreateThumbnail(dtZdjecia.Rows(nr_biezacego_zdjecia - 1).Item("ZDJECIE"), 160)

            ' zmiana rozmiaru obrazka 


            ' koniec zmiany rozmiaru obrazka 
            utworzMiniature(frmGlowna.sesja, dtZdjecia.Rows(nr_biezacego_zdjecia - 1).Item("SKU_ID"), dtZdjecia.Rows(nr_biezacego_zdjecia - 1).Item("ID"), mImage)
            PokazZdjecie()
        ElseIf Not (chkIsThumbnail.Checked) And dtZdjecia.Rows(nr_biezacego_zdjecia - 1).Item("IsThumbnail") = 1 Then
            ' zdjęcie odznaczono. mozna usunac miniaturkę
            utworzMiniature(frmGlowna.sesja, dtZdjecia.Rows(nr_biezacego_zdjecia - 1).Item("SKU_ID"), dtZdjecia.Rows(nr_biezacego_zdjecia - 1).Item("ID"), mImage)
            PokazZdjecie()
        Else
            ' nic do zrobienia 

        End If

    End Sub

#Region "Zmiana rozmiaru zdjęcia"
    ' Create a thumbnail in byte array format from the image encoded in the passed byte array.  
    ' (RESIZE an image in a byte[] variable.)  
    Public Shared Function CreateThumbnail(PassedImage As Byte(), LargestSide As Integer) As Byte()
        Dim ReturnedThumbnail As Byte()

        Using StartMemoryStream As New MemoryStream(), NewMemoryStream As New MemoryStream()
            ' write the string to the stream  
            StartMemoryStream.Write(PassedImage, 0, PassedImage.Length)

            ' create the start Bitmap from the MemoryStream that contains the image  
            Dim startBitmap As New Bitmap(StartMemoryStream)

            ' set thumbnail height and width proportional to the original image.  
            Dim newHeight As Integer
            Dim newWidth As Integer
            Dim HW_ratio As Double
            If startBitmap.Height > startBitmap.Width Then
                newHeight = LargestSide
                HW_ratio = CDbl(CDbl(LargestSide) / CDbl(startBitmap.Height))
                newWidth = CInt(Math.Truncate(HW_ratio * CDbl(startBitmap.Width)))
            Else
                newWidth = LargestSide
                HW_ratio = CDbl(CDbl(LargestSide) / CDbl(startBitmap.Width))
                newHeight = CInt(Math.Truncate(HW_ratio * CDbl(startBitmap.Height)))
            End If

            ' create a new Bitmap with dimensions for the thumbnail.  
            Dim newBitmap As New Bitmap(newWidth, newHeight)

            ' Copy the image from the START Bitmap into the NEW Bitmap.  
            ' This will create a thumnail size of the same image.  
            newBitmap = ResizeImage(startBitmap, newWidth, newHeight)

            ' Save this image to the specified stream in the specified format.  
            newBitmap.Save(NewMemoryStream, System.Drawing.Imaging.ImageFormat.Jpeg)

            ' Fill the byte[] for the thumbnail from the new MemoryStream.  
            ReturnedThumbnail = NewMemoryStream.ToArray()
        End Using

        ' return the resized image as a string of bytes.  
        Return ReturnedThumbnail
    End Function

    ' Resize a Bitmap  
    Private Shared Function ResizeImage(image As Bitmap, width As Integer, height As Integer) As Bitmap
        Dim resizedImage As New Bitmap(width, height)
        Using gfx As Graphics = Graphics.FromImage(resizedImage)
            gfx.DrawImage(image, New Rectangle(0, 0, width, height), New Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel)
        End Using
        Return resizedImage
    End Function

#End Region


End Class
