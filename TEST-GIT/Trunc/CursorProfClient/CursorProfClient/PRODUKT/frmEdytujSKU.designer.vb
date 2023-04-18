<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEdytujSKU
    Inherits CursorProfClient.frmBase

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEdytujSKU))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmbTypOkresZamowien = New System.Windows.Forms.ComboBox()
        Me.txtMaxIloscZamowien = New System.Windows.Forms.TextBox()
        Me.lblIloscZamowien = New System.Windows.Forms.Label()
        Me.lblOpisRozszerzony = New System.Windows.Forms.Label()
        Me.chkCzyLimitWydanOsoba = New System.Windows.Forms.CheckBox()
        Me.chkNowosc = New System.Windows.Forms.CheckBox()
        Me.lblKategoria = New System.Windows.Forms.Label()
        Me.lblBranza = New System.Windows.Forms.Label()
        Me.lblMarka = New System.Windows.Forms.Label()
        Me.lblWaga = New System.Windows.Forms.Label()
        Me.lblRozmiar = New System.Windows.Forms.Label()
        Me.lblCena = New System.Windows.Forms.Label()
        Me.txtMaxIlosc = New System.Windows.Forms.TextBox()
        Me.chkCzyMoznaZamawiac = New System.Windows.Forms.CheckBox()
        Me.lblMaxIloscZamowienie = New System.Windows.Forms.Label()
        Me.lblOpis = New System.Windows.Forms.Label()
        Me.lblNazProduktu = New System.Windows.Forms.Label()
        Me.lblNumProduktu = New System.Windows.Forms.Label()
        Me.btnZapisz = New System.Windows.Forms.Button()
        Me.btnAnuluj = New System.Windows.Forms.Button()
        Me.lblOkres = New System.Windows.Forms.Label()
        Me.txtNazwaSKU = New System.Windows.Forms.TextBox()
        Me.txtOpisRozszerzony = New System.Windows.Forms.TextBox()
        Me.lblBrakZdjecia = New System.Windows.Forms.Label()
        Me.lblNumerSKU = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnEdycjaGalerii = New System.Windows.Forms.Button()
        Me.CtrImgGaleriaEdycjaSKU = New CursorProfClient.ctrImgGaleria()
        Me.GroupBoxAtrybuty = New System.Windows.Forms.GroupBox()
        Me.cmbKategoria = New System.Windows.Forms.ComboBox()
        Me.cmbBranza = New System.Windows.Forms.ComboBox()
        Me.cmbMarka = New System.Windows.Forms.ComboBox()
        Me.cmbJM = New System.Windows.Forms.ComboBox()
        Me.txtWaga = New System.Windows.Forms.TextBox()
        Me.txtGlebokosc = New System.Windows.Forms.TextBox()
        Me.txtSzerokosc = New System.Windows.Forms.TextBox()
        Me.txtWysokosc = New System.Windows.Forms.TextBox()
        Me.txtCenaJM = New System.Windows.Forms.TextBox()
        Me.lblJM = New System.Windows.Forms.Label()
        Me.txtOpisSKU = New System.Windows.Forms.TextBox()
        Me.txtSztOpk = New System.Windows.Forms.TextBox()
        Me.lblSztOpk = New System.Windows.Forms.Label()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBoxAtrybuty.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolTip1
        '
        Me.ToolTip1.AutoPopDelay = 10000
        Me.ToolTip1.InitialDelay = 500
        Me.ToolTip1.ReshowDelay = 500
        '
        'cmbTypOkresZamowien
        '
        Me.cmbTypOkresZamowien.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTypOkresZamowien.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cmbTypOkresZamowien.ForeColor = System.Drawing.Color.Black
        Me.cmbTypOkresZamowien.FormattingEnabled = True
        Me.cmbTypOkresZamowien.Location = New System.Drawing.Point(276, 436)
        Me.cmbTypOkresZamowien.Name = "cmbTypOkresZamowien"
        Me.cmbTypOkresZamowien.Size = New System.Drawing.Size(145, 21)
        Me.cmbTypOkresZamowien.TabIndex = 17
        Me.ToolTip1.SetToolTip(Me.cmbTypOkresZamowien, "Okres, w trakcie którego każdy użytkownik możę złożyć podaną maksymalną ilość zam" & _
        "ówień na ten produkt.")
        '
        'txtMaxIloscZamowien
        '
        Me.txtMaxIloscZamowien.ForeColor = System.Drawing.Color.Black
        Me.txtMaxIloscZamowien.Location = New System.Drawing.Point(173, 437)
        Me.txtMaxIloscZamowien.MaxLength = 10
        Me.txtMaxIloscZamowien.Name = "txtMaxIloscZamowien"
        Me.txtMaxIloscZamowien.Size = New System.Drawing.Size(76, 20)
        Me.txtMaxIloscZamowien.TabIndex = 15
        Me.ToolTip1.SetToolTip(Me.txtMaxIloscZamowien, "Ilość zamówień na dany produkt jaką w podanym okresie może złożyć każdy użytkowni" & _
        "k.")
        '
        'lblIloscZamowien
        '
        Me.lblIloscZamowien.AutoSize = True
        Me.lblIloscZamowien.ForeColor = System.Drawing.Color.Black
        Me.lblIloscZamowien.Location = New System.Drawing.Point(21, 440)
        Me.lblIloscZamowien.Name = "lblIloscZamowien"
        Me.lblIloscZamowien.Size = New System.Drawing.Size(146, 13)
        Me.lblIloscZamowien.TabIndex = 14
        Me.lblIloscZamowien.Text = "Maksymalna ilość zamówień: "
        Me.ToolTip1.SetToolTip(Me.lblIloscZamowien, "Ilość zamówień jaką w podanym okresie może złożyć każdy użytkownik.")
        '
        'lblOpisRozszerzony
        '
        Me.lblOpisRozszerzony.AutoSize = True
        Me.lblOpisRozszerzony.ForeColor = System.Drawing.Color.Black
        Me.lblOpisRozszerzony.Location = New System.Drawing.Point(14, 125)
        Me.lblOpisRozszerzony.Name = "lblOpisRozszerzony"
        Me.lblOpisRozszerzony.Size = New System.Drawing.Size(89, 13)
        Me.lblOpisRozszerzony.TabIndex = 6
        Me.lblOpisRozszerzony.Text = "Opis rozszerzony:"
        Me.ToolTip1.SetToolTip(Me.lblOpisRozszerzony, "Rozszerzony opis produktu")
        Me.lblOpisRozszerzony.Visible = False
        '
        'chkCzyLimitWydanOsoba
        '
        Me.chkCzyLimitWydanOsoba.AutoSize = True
        Me.chkCzyLimitWydanOsoba.ForeColor = System.Drawing.Color.Black
        Me.chkCzyLimitWydanOsoba.Location = New System.Drawing.Point(21, 387)
        Me.chkCzyLimitWydanOsoba.Name = "chkCzyLimitWydanOsoba"
        Me.chkCzyLimitWydanOsoba.Size = New System.Drawing.Size(150, 17)
        Me.chkCzyLimitWydanOsoba.TabIndex = 11
        Me.chkCzyLimitWydanOsoba.Text = "Czy limit wydań na osobę?"
        Me.ToolTip1.SetToolTip(Me.chkCzyLimitWydanOsoba, resources.GetString("chkCzyLimitWydanOsoba.ToolTip"))
        Me.chkCzyLimitWydanOsoba.UseVisualStyleBackColor = True
        '
        'chkNowosc
        '
        Me.chkNowosc.AutoSize = True
        Me.chkNowosc.ForeColor = System.Drawing.Color.Black
        Me.chkNowosc.Location = New System.Drawing.Point(21, 366)
        Me.chkNowosc.Name = "chkNowosc"
        Me.chkNowosc.Size = New System.Drawing.Size(178, 17)
        Me.chkNowosc.TabIndex = 10
        Me.chkNowosc.Text = "Wyświetl produkt w nowościach"
        Me.ToolTip1.SetToolTip(Me.chkNowosc, "Zaznaczanie tej opcji powoduje, że produkt zostaje oznaczony jako nowość.")
        Me.chkNowosc.UseVisualStyleBackColor = True
        '
        'lblKategoria
        '
        Me.lblKategoria.AutoSize = True
        Me.lblKategoria.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblKategoria.ForeColor = System.Drawing.Color.Black
        Me.lblKategoria.Location = New System.Drawing.Point(6, 22)
        Me.lblKategoria.Name = "lblKategoria"
        Me.lblKategoria.Size = New System.Drawing.Size(52, 13)
        Me.lblKategoria.TabIndex = 0
        Me.lblKategoria.Text = "Kategoria"
        Me.ToolTip1.SetToolTip(Me.lblKategoria, "Kategoria produktu")
        '
        'lblBranza
        '
        Me.lblBranza.AutoSize = True
        Me.lblBranza.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblBranza.ForeColor = System.Drawing.Color.Black
        Me.lblBranza.Location = New System.Drawing.Point(6, 75)
        Me.lblBranza.Name = "lblBranza"
        Me.lblBranza.Size = New System.Drawing.Size(40, 13)
        Me.lblBranza.TabIndex = 2
        Me.lblBranza.Text = "Rodzaj"
        Me.ToolTip1.SetToolTip(Me.lblBranza, "Rodzaj produktu")
        Me.lblBranza.Visible = False
        '
        'lblMarka
        '
        Me.lblMarka.AutoSize = True
        Me.lblMarka.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblMarka.ForeColor = System.Drawing.Color.Black
        Me.lblMarka.Location = New System.Drawing.Point(6, 49)
        Me.lblMarka.Name = "lblMarka"
        Me.lblMarka.Size = New System.Drawing.Size(35, 13)
        Me.lblMarka.TabIndex = 4
        Me.lblMarka.Text = "Brand"
        Me.ToolTip1.SetToolTip(Me.lblMarka, "Brand produktu")
        '
        'lblWaga
        '
        Me.lblWaga.AutoSize = True
        Me.lblWaga.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblWaga.ForeColor = System.Drawing.Color.Black
        Me.lblWaga.Location = New System.Drawing.Point(308, 127)
        Me.lblWaga.Name = "lblWaga"
        Me.lblWaga.Size = New System.Drawing.Size(60, 13)
        Me.lblWaga.TabIndex = 14
        Me.lblWaga.Text = "Waga (gr.) "
        Me.ToolTip1.SetToolTip(Me.lblWaga, "Waga produktu.")
        Me.lblWaga.Visible = False
        '
        'lblRozmiar
        '
        Me.lblRozmiar.AutoSize = True
        Me.lblRozmiar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblRozmiar.ForeColor = System.Drawing.Color.Black
        Me.lblRozmiar.Location = New System.Drawing.Point(6, 149)
        Me.lblRozmiar.Name = "lblRozmiar"
        Me.lblRozmiar.Size = New System.Drawing.Size(155, 13)
        Me.lblRozmiar.TabIndex = 10
        Me.lblRozmiar.Text = "Wys. x Szer. x Głębokość (mm)"
        Me.ToolTip1.SetToolTip(Me.lblRozmiar, "Wymiary produktu: wysokość x szerokość x głębokość")
        Me.lblRozmiar.Visible = False
        '
        'lblCena
        '
        Me.lblCena.AutoSize = True
        Me.lblCena.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblCena.ForeColor = System.Drawing.Color.Black
        Me.lblCena.Location = New System.Drawing.Point(6, 100)
        Me.lblCena.Name = "lblCena"
        Me.lblCena.Size = New System.Drawing.Size(115, 13)
        Me.lblCena.TabIndex = 6
        Me.lblCena.Text = "Koszt punktowy za jm. "
        Me.ToolTip1.SetToolTip(Me.lblCena, "Koszt punktowy za jednostkę produktu")
        '
        'txtMaxIlosc
        '
        Me.txtMaxIlosc.ForeColor = System.Drawing.Color.Black
        Me.txtMaxIlosc.Location = New System.Drawing.Point(231, 411)
        Me.txtMaxIlosc.MaxLength = 10
        Me.txtMaxIlosc.Name = "txtMaxIlosc"
        Me.txtMaxIlosc.Size = New System.Drawing.Size(190, 20)
        Me.txtMaxIlosc.TabIndex = 13
        Me.ToolTip1.SetToolTip(Me.txtMaxIlosc, resources.GetString("txtMaxIlosc.ToolTip"))
        '
        'chkCzyMoznaZamawiac
        '
        Me.chkCzyMoznaZamawiac.AutoSize = True
        Me.chkCzyMoznaZamawiac.ForeColor = System.Drawing.Color.Black
        Me.chkCzyMoznaZamawiac.Location = New System.Drawing.Point(21, 345)
        Me.chkCzyMoznaZamawiac.Name = "chkCzyMoznaZamawiac"
        Me.chkCzyMoznaZamawiac.Size = New System.Drawing.Size(111, 17)
        Me.chkCzyMoznaZamawiac.TabIndex = 9
        Me.chkCzyMoznaZamawiac.Text = "Można zamawiać "
        Me.ToolTip1.SetToolTip(Me.chkCzyMoznaZamawiac, "Zaznaczenie opcji ""Można zamawiać"" powoduje, " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "że będzie można składać zamówienia" & _
        " na ten produkt.")
        Me.chkCzyMoznaZamawiac.UseVisualStyleBackColor = True
        '
        'lblMaxIloscZamowienie
        '
        Me.lblMaxIloscZamowienie.AutoSize = True
        Me.lblMaxIloscZamowienie.ForeColor = System.Drawing.Color.Black
        Me.lblMaxIloscZamowienie.Location = New System.Drawing.Point(21, 414)
        Me.lblMaxIloscZamowienie.Name = "lblMaxIloscZamowienie"
        Me.lblMaxIloscZamowienie.Size = New System.Drawing.Size(204, 13)
        Me.lblMaxIloscZamowienie.TabIndex = 12
        Me.lblMaxIloscZamowienie.Text = "Maksymalna ilość w jednym zamówieniu : "
        Me.ToolTip1.SetToolTip(Me.lblMaxIloscZamowienie, resources.GetString("lblMaxIloscZamowienie.ToolTip"))
        '
        'lblOpis
        '
        Me.lblOpis.AutoSize = True
        Me.lblOpis.ForeColor = System.Drawing.Color.Black
        Me.lblOpis.Location = New System.Drawing.Point(12, 77)
        Me.lblOpis.Name = "lblOpis"
        Me.lblOpis.Size = New System.Drawing.Size(31, 13)
        Me.lblOpis.TabIndex = 4
        Me.lblOpis.Text = "Opis:"
        Me.ToolTip1.SetToolTip(Me.lblOpis, "Opis produktu")
        '
        'lblNazProduktu
        '
        Me.lblNazProduktu.AutoSize = True
        Me.lblNazProduktu.ForeColor = System.Drawing.Color.Black
        Me.lblNazProduktu.Location = New System.Drawing.Point(12, 33)
        Me.lblNazProduktu.Name = "lblNazProduktu"
        Me.lblNazProduktu.Size = New System.Drawing.Size(43, 13)
        Me.lblNazProduktu.TabIndex = 2
        Me.lblNazProduktu.Text = "Nazwa:"
        Me.ToolTip1.SetToolTip(Me.lblNazProduktu, "Nazwa produktu")
        '
        'lblNumProduktu
        '
        Me.lblNumProduktu.AutoSize = True
        Me.lblNumProduktu.ForeColor = System.Drawing.Color.Black
        Me.lblNumProduktu.Location = New System.Drawing.Point(12, 9)
        Me.lblNumProduktu.Name = "lblNumProduktu"
        Me.lblNumProduktu.Size = New System.Drawing.Size(32, 13)
        Me.lblNumProduktu.TabIndex = 0
        Me.lblNumProduktu.Text = "Sku: "
        Me.ToolTip1.SetToolTip(Me.lblNumProduktu, "Sku oznacza numer produktu")
        '
        'btnZapisz
        '
        Me.btnZapisz.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnZapisz.ForeColor = System.Drawing.Color.White
        Me.btnZapisz.Location = New System.Drawing.Point(711, 488)
        Me.btnZapisz.Name = "btnZapisz"
        Me.btnZapisz.Size = New System.Drawing.Size(110, 31)
        Me.btnZapisz.TabIndex = 21
        Me.btnZapisz.Text = "Zapisz"
        Me.btnZapisz.UseVisualStyleBackColor = False
        '
        'btnAnuluj
        '
        Me.btnAnuluj.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnAnuluj.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnAnuluj.ForeColor = System.Drawing.Color.White
        Me.btnAnuluj.Location = New System.Drawing.Point(827, 488)
        Me.btnAnuluj.Name = "btnAnuluj"
        Me.btnAnuluj.Size = New System.Drawing.Size(104, 31)
        Me.btnAnuluj.TabIndex = 22
        Me.btnAnuluj.Text = "Anuluj"
        Me.btnAnuluj.UseVisualStyleBackColor = False
        '
        'lblOkres
        '
        Me.lblOkres.AutoSize = True
        Me.lblOkres.ForeColor = System.Drawing.Color.Black
        Me.lblOkres.Location = New System.Drawing.Point(255, 440)
        Me.lblOkres.Name = "lblOkres"
        Me.lblOkres.Size = New System.Drawing.Size(15, 13)
        Me.lblOkres.TabIndex = 16
        Me.lblOkres.Text = "w"
        '
        'txtNazwaSKU
        '
        Me.txtNazwaSKU.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtNazwaSKU.ForeColor = System.Drawing.Color.Black
        Me.txtNazwaSKU.Location = New System.Drawing.Point(107, 30)
        Me.txtNazwaSKU.Multiline = True
        Me.txtNazwaSKU.Name = "txtNazwaSKU"
        Me.txtNazwaSKU.Size = New System.Drawing.Size(326, 42)
        Me.txtNazwaSKU.TabIndex = 3
        '
        'txtOpisRozszerzony
        '
        Me.txtOpisRozszerzony.ForeColor = System.Drawing.Color.Black
        Me.txtOpisRozszerzony.Location = New System.Drawing.Point(107, 125)
        Me.txtOpisRozszerzony.MaxLength = 1000
        Me.txtOpisRozszerzony.Multiline = True
        Me.txtOpisRozszerzony.Name = "txtOpisRozszerzony"
        Me.txtOpisRozszerzony.Size = New System.Drawing.Size(326, 38)
        Me.txtOpisRozszerzony.TabIndex = 7
        Me.txtOpisRozszerzony.Visible = False
        '
        'lblBrakZdjecia
        '
        Me.lblBrakZdjecia.AutoSize = True
        Me.lblBrakZdjecia.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblBrakZdjecia.Location = New System.Drawing.Point(20, 529)
        Me.lblBrakZdjecia.Name = "lblBrakZdjecia"
        Me.lblBrakZdjecia.Size = New System.Drawing.Size(23, 15)
        Me.lblBrakZdjecia.TabIndex = 27
        Me.lblBrakZdjecia.Text = "    "
        '
        'lblNumerSKU
        '
        Me.lblNumerSKU.AutoSize = True
        Me.lblNumerSKU.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblNumerSKU.ForeColor = System.Drawing.Color.Black
        Me.lblNumerSKU.Location = New System.Drawing.Point(104, 9)
        Me.lblNumerSKU.Name = "lblNumerSKU"
        Me.lblNumerSKU.Size = New System.Drawing.Size(152, 15)
        Me.lblNumerSKU.TabIndex = 1
        Me.lblNumerSKU.Text = "Numer produktu (SKU)"
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.White
        Me.GroupBox2.Controls.Add(Me.btnEdycjaGalerii)
        Me.GroupBox2.Controls.Add(Me.CtrImgGaleriaEdycjaSKU)
        Me.GroupBox2.ForeColor = System.Drawing.Color.Black
        Me.GroupBox2.Location = New System.Drawing.Point(441, 5)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(488, 457)
        Me.GroupBox2.TabIndex = 20
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Zdjęcie"
        '
        'btnEdycjaGalerii
        '
        Me.btnEdycjaGalerii.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnEdycjaGalerii.ForeColor = System.Drawing.Color.White
        Me.btnEdycjaGalerii.Location = New System.Drawing.Point(352, 422)
        Me.btnEdycjaGalerii.Name = "btnEdycjaGalerii"
        Me.btnEdycjaGalerii.Size = New System.Drawing.Size(130, 29)
        Me.btnEdycjaGalerii.TabIndex = 0
        Me.btnEdycjaGalerii.Text = "Edycja galerii"
        Me.btnEdycjaGalerii.UseVisualStyleBackColor = False
        '
        'CtrImgGaleriaEdycjaSKU
        '
        Me.CtrImgGaleriaEdycjaSKU.BackColor = System.Drawing.Color.White
        Me.CtrImgGaleriaEdycjaSKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrImgGaleriaEdycjaSKU.Location = New System.Drawing.Point(6, 16)
        Me.CtrImgGaleriaEdycjaSKU.MaximumSize = New System.Drawing.Size(477, 400)
        Me.CtrImgGaleriaEdycjaSKU.MinimumSize = New System.Drawing.Size(477, 400)
        Me.CtrImgGaleriaEdycjaSKU.Name = "CtrImgGaleriaEdycjaSKU"
        Me.CtrImgGaleriaEdycjaSKU.Size = New System.Drawing.Size(477, 400)
        Me.CtrImgGaleriaEdycjaSKU.TabIndex = 1
        '
        'GroupBoxAtrybuty
        '
        Me.GroupBoxAtrybuty.BackColor = System.Drawing.Color.White
        Me.GroupBoxAtrybuty.Controls.Add(Me.txtSztOpk)
        Me.GroupBoxAtrybuty.Controls.Add(Me.lblSztOpk)
        Me.GroupBoxAtrybuty.Controls.Add(Me.cmbKategoria)
        Me.GroupBoxAtrybuty.Controls.Add(Me.lblKategoria)
        Me.GroupBoxAtrybuty.Controls.Add(Me.cmbBranza)
        Me.GroupBoxAtrybuty.Controls.Add(Me.cmbMarka)
        Me.GroupBoxAtrybuty.Controls.Add(Me.cmbJM)
        Me.GroupBoxAtrybuty.Controls.Add(Me.txtWaga)
        Me.GroupBoxAtrybuty.Controls.Add(Me.txtWysokosc)
        Me.GroupBoxAtrybuty.Controls.Add(Me.txtCenaJM)
        Me.GroupBoxAtrybuty.Controls.Add(Me.txtGlebokosc)
        Me.GroupBoxAtrybuty.Controls.Add(Me.lblRozmiar)
        Me.GroupBoxAtrybuty.Controls.Add(Me.txtSzerokosc)
        Me.GroupBoxAtrybuty.Controls.Add(Me.lblBranza)
        Me.GroupBoxAtrybuty.Controls.Add(Me.lblMarka)
        Me.GroupBoxAtrybuty.Controls.Add(Me.lblWaga)
        Me.GroupBoxAtrybuty.Controls.Add(Me.lblCena)
        Me.GroupBoxAtrybuty.Controls.Add(Me.lblJM)
        Me.GroupBoxAtrybuty.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.GroupBoxAtrybuty.ForeColor = System.Drawing.Color.Black
        Me.GroupBoxAtrybuty.Location = New System.Drawing.Point(15, 169)
        Me.GroupBoxAtrybuty.Name = "GroupBoxAtrybuty"
        Me.GroupBoxAtrybuty.Size = New System.Drawing.Size(418, 170)
        Me.GroupBoxAtrybuty.TabIndex = 8
        Me.GroupBoxAtrybuty.TabStop = False
        Me.GroupBoxAtrybuty.Text = "Atrybuty SKU"
        '
        'cmbKategoria
        '
        Me.cmbKategoria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbKategoria.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cmbKategoria.ForeColor = System.Drawing.Color.Black
        Me.cmbKategoria.FormattingEnabled = True
        Me.cmbKategoria.Location = New System.Drawing.Point(167, 19)
        Me.cmbKategoria.Name = "cmbKategoria"
        Me.cmbKategoria.Size = New System.Drawing.Size(239, 21)
        Me.cmbKategoria.TabIndex = 1
        '
        'cmbBranza
        '
        Me.cmbBranza.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBranza.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cmbBranza.ForeColor = System.Drawing.Color.Black
        Me.cmbBranza.FormattingEnabled = True
        Me.cmbBranza.Location = New System.Drawing.Point(167, 72)
        Me.cmbBranza.Name = "cmbBranza"
        Me.cmbBranza.Size = New System.Drawing.Size(239, 21)
        Me.cmbBranza.TabIndex = 3
        Me.cmbBranza.Visible = False
        '
        'cmbMarka
        '
        Me.cmbMarka.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMarka.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cmbMarka.ForeColor = System.Drawing.Color.Black
        Me.cmbMarka.FormattingEnabled = True
        Me.cmbMarka.Location = New System.Drawing.Point(167, 46)
        Me.cmbMarka.Name = "cmbMarka"
        Me.cmbMarka.Size = New System.Drawing.Size(239, 21)
        Me.cmbMarka.TabIndex = 5
        '
        'cmbJM
        '
        Me.cmbJM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbJM.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cmbJM.ForeColor = System.Drawing.Color.Black
        Me.cmbJM.FormattingEnabled = True
        Me.cmbJM.Location = New System.Drawing.Point(306, 97)
        Me.cmbJM.Name = "cmbJM"
        Me.cmbJM.Size = New System.Drawing.Size(100, 21)
        Me.cmbJM.TabIndex = 9
        '
        'txtWaga
        '
        Me.txtWaga.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtWaga.ForeColor = System.Drawing.Color.Black
        Me.txtWaga.Location = New System.Drawing.Point(374, 124)
        Me.txtWaga.Name = "txtWaga"
        Me.txtWaga.Size = New System.Drawing.Size(32, 20)
        Me.txtWaga.TabIndex = 15
        Me.txtWaga.Visible = False
        '
        'txtGlebokosc
        '
        Me.txtGlebokosc.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtGlebokosc.ForeColor = System.Drawing.Color.Black
        Me.txtGlebokosc.Location = New System.Drawing.Point(335, 146)
        Me.txtGlebokosc.Name = "txtGlebokosc"
        Me.txtGlebokosc.Size = New System.Drawing.Size(71, 20)
        Me.txtGlebokosc.TabIndex = 13
        Me.txtGlebokosc.Visible = False
        '
        'txtSzerokosc
        '
        Me.txtSzerokosc.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtSzerokosc.ForeColor = System.Drawing.Color.Black
        Me.txtSzerokosc.Location = New System.Drawing.Point(256, 146)
        Me.txtSzerokosc.Name = "txtSzerokosc"
        Me.txtSzerokosc.Size = New System.Drawing.Size(65, 20)
        Me.txtSzerokosc.TabIndex = 12
        Me.txtSzerokosc.Visible = False
        '
        'txtWysokosc
        '
        Me.txtWysokosc.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtWysokosc.ForeColor = System.Drawing.Color.Black
        Me.txtWysokosc.Location = New System.Drawing.Point(167, 146)
        Me.txtWysokosc.Name = "txtWysokosc"
        Me.txtWysokosc.Size = New System.Drawing.Size(74, 20)
        Me.txtWysokosc.TabIndex = 11
        Me.txtWysokosc.Visible = False
        '
        'txtCenaJM
        '
        Me.txtCenaJM.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtCenaJM.ForeColor = System.Drawing.Color.Black
        Me.txtCenaJM.Location = New System.Drawing.Point(167, 98)
        Me.txtCenaJM.Name = "txtCenaJM"
        Me.txtCenaJM.Size = New System.Drawing.Size(103, 20)
        Me.txtCenaJM.TabIndex = 7
        '
        'lblJM
        '
        Me.lblJM.AutoSize = True
        Me.lblJM.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblJM.ForeColor = System.Drawing.Color.Black
        Me.lblJM.Location = New System.Drawing.Point(276, 100)
        Me.lblJM.Name = "lblJM"
        Me.lblJM.Size = New System.Drawing.Size(24, 13)
        Me.lblJM.TabIndex = 8
        Me.lblJM.Text = "JM."
        '
        'txtOpisSKU
        '
        Me.txtOpisSKU.ForeColor = System.Drawing.Color.Black
        Me.txtOpisSKU.Location = New System.Drawing.Point(107, 77)
        Me.txtOpisSKU.Multiline = True
        Me.txtOpisSKU.Name = "txtOpisSKU"
        Me.txtOpisSKU.Size = New System.Drawing.Size(326, 42)
        Me.txtOpisSKU.TabIndex = 5
        '
        'txtSztOpk
        '
        Me.txtSztOpk.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtSztOpk.ForeColor = System.Drawing.Color.Black
        Me.txtSztOpk.Location = New System.Drawing.Point(167, 124)
        Me.txtSztOpk.Name = "txtSztOpk"
        Me.txtSztOpk.Size = New System.Drawing.Size(74, 20)
        Me.txtSztOpk.TabIndex = 17
        '
        'lblSztOpk
        '
        Me.lblSztOpk.AutoSize = True
        Me.lblSztOpk.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblSztOpk.ForeColor = System.Drawing.Color.Black
        Me.lblSztOpk.Location = New System.Drawing.Point(6, 127)
        Me.lblSztOpk.Name = "lblSztOpk"
        Me.lblSztOpk.Size = New System.Drawing.Size(129, 13)
        Me.lblSztOpk.TabIndex = 16
        Me.lblSztOpk.Text = "Ilość sztuk w opakowaniu"
        Me.ToolTip1.SetToolTip(Me.lblSztOpk, "Waga produktu.")
        '
        'frmEdytujSKU
        '
        Me.AcceptButton = Me.btnZapisz
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnAnuluj
        Me.ClientSize = New System.Drawing.Size(943, 527)
        Me.Controls.Add(Me.lblOkres)
        Me.Controls.Add(Me.cmbTypOkresZamowien)
        Me.Controls.Add(Me.txtMaxIloscZamowien)
        Me.Controls.Add(Me.lblIloscZamowien)
        Me.Controls.Add(Me.txtNazwaSKU)
        Me.Controls.Add(Me.txtOpisRozszerzony)
        Me.Controls.Add(Me.lblOpisRozszerzony)
        Me.Controls.Add(Me.chkCzyLimitWydanOsoba)
        Me.Controls.Add(Me.chkNowosc)
        Me.Controls.Add(Me.lblBrakZdjecia)
        Me.Controls.Add(Me.lblNumerSKU)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBoxAtrybuty)
        Me.Controls.Add(Me.txtMaxIlosc)
        Me.Controls.Add(Me.btnZapisz)
        Me.Controls.Add(Me.btnAnuluj)
        Me.Controls.Add(Me.txtOpisSKU)
        Me.Controls.Add(Me.chkCzyMoznaZamawiac)
        Me.Controls.Add(Me.lblMaxIloscZamowienie)
        Me.Controls.Add(Me.lblOpis)
        Me.Controls.Add(Me.lblNazProduktu)
        Me.Controls.Add(Me.lblNumProduktu)
        Me.MinimumSize = New System.Drawing.Size(951, 539)
        Me.Name = "frmEdytujSKU"
        Me.Text = "Edycja SKU"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBoxAtrybuty.ResumeLayout(False)
        Me.GroupBoxAtrybuty.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblNumProduktu As System.Windows.Forms.Label
    Friend WithEvents lblNazProduktu As System.Windows.Forms.Label
    Friend WithEvents lblOpis As System.Windows.Forms.Label
    Friend WithEvents lblMaxIloscZamowienie As System.Windows.Forms.Label
    Friend WithEvents chkCzyMoznaZamawiac As System.Windows.Forms.CheckBox
    Friend WithEvents txtOpisSKU As System.Windows.Forms.TextBox
    Friend WithEvents btnAnuluj As System.Windows.Forms.Button
    Friend WithEvents btnZapisz As System.Windows.Forms.Button
    Friend WithEvents txtMaxIlosc As System.Windows.Forms.TextBox
    Friend WithEvents GroupBoxAtrybuty As System.Windows.Forms.GroupBox
    Friend WithEvents cmbJM As System.Windows.Forms.ComboBox
    Friend WithEvents txtWaga As System.Windows.Forms.TextBox
    Friend WithEvents txtGlebokosc As System.Windows.Forms.TextBox
    Friend WithEvents txtSzerokosc As System.Windows.Forms.TextBox
    Friend WithEvents txtWysokosc As System.Windows.Forms.TextBox
    Friend WithEvents txtCenaJM As System.Windows.Forms.TextBox
    Friend WithEvents lblBranza As System.Windows.Forms.Label
    Friend WithEvents lblMarka As System.Windows.Forms.Label
    Friend WithEvents lblWaga As System.Windows.Forms.Label
    Friend WithEvents lblRozmiar As System.Windows.Forms.Label
    Friend WithEvents lblCena As System.Windows.Forms.Label
    Friend WithEvents lblJM As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents lblNumerSKU As System.Windows.Forms.Label
    Friend WithEvents cmbBranza As System.Windows.Forms.ComboBox
    Friend WithEvents cmbMarka As System.Windows.Forms.ComboBox
    Friend WithEvents chkNowosc As System.Windows.Forms.CheckBox
    Friend WithEvents chkCzyLimitWydanOsoba As System.Windows.Forms.CheckBox
    Friend WithEvents lblBrakZdjecia As System.Windows.Forms.Label
    Friend WithEvents cmbKategoria As System.Windows.Forms.ComboBox
    Friend WithEvents lblKategoria As System.Windows.Forms.Label
    Friend WithEvents btnEdycjaGalerii As System.Windows.Forms.Button
    Friend WithEvents CtrImgGaleriaEdycjaSKU As CursorProfClient.ctrImgGaleria
    Friend WithEvents txtOpisRozszerzony As System.Windows.Forms.TextBox
    Friend WithEvents lblOpisRozszerzony As System.Windows.Forms.Label
    Friend WithEvents txtNazwaSKU As System.Windows.Forms.TextBox
    Friend WithEvents txtMaxIloscZamowien As System.Windows.Forms.TextBox
    Friend WithEvents lblIloscZamowien As System.Windows.Forms.Label
    Friend WithEvents cmbTypOkresZamowien As System.Windows.Forms.ComboBox
    Friend WithEvents lblOkres As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents txtSztOpk As System.Windows.Forms.TextBox
    Friend WithEvents lblSztOpk As System.Windows.Forms.Label
End Class
