<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctrSKU
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ctrSKU))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.tsNawigator = New System.Windows.Forms.ToolStrip()
        Me.btnPoczatek = New System.Windows.Forms.ToolStripButton()
        Me.btnPoprzedni = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.lblEkran = New System.Windows.Forms.ToolStripLabel()
        Me.txtNumerEkranu = New System.Windows.Forms.ToolStripTextBox()
        Me.lblIloscEkranow = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnNastepny = New System.Windows.Forms.ToolStripButton()
        Me.btnOstatni = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnOdswiez = New System.Windows.Forms.ToolStripButton()
        Me.lblWyswietlajPo = New System.Windows.Forms.ToolStripLabel()
        Me.cmbIloscNaStronie = New System.Windows.Forms.ToolStripComboBox()
        Me.lblWierszyNaStronie = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsKolumny = New System.Windows.Forms.ToolStripDropDownButton()
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.listMarka = New System.Windows.Forms.CheckedListBox()
        Me.ListBranza = New System.Windows.Forms.CheckedListBox()
        Me.lblMarka = New System.Windows.Forms.Label()
        Me.lblBranza = New System.Windows.Forms.Label()
        Me.lblNumer = New System.Windows.Forms.Label()
        Me.tbNumer = New System.Windows.Forms.TextBox()
        Me.lblNazwa = New System.Windows.Forms.Label()
        Me.tbNazwa = New System.Windows.Forms.TextBox()
        Me.chbNowosci = New System.Windows.Forms.CheckBox()
        Me.chbNieuzupelnione = New System.Windows.Forms.CheckBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.listGrupa = New System.Windows.Forms.CheckedListBox()
        Me.dgv = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.listKategoria = New System.Windows.Forms.CheckedListBox()
        Me.tsNawigator.SuspendLayout()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tsNawigator
        '
        Me.tsNawigator.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.tsNawigator.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnPoczatek, Me.btnPoprzedni, Me.ToolStripSeparator1, Me.lblEkran, Me.txtNumerEkranu, Me.lblIloscEkranow, Me.ToolStripSeparator2, Me.btnNastepny, Me.btnOstatni, Me.ToolStripSeparator3, Me.btnOdswiez, Me.lblWyswietlajPo, Me.cmbIloscNaStronie, Me.lblWierszyNaStronie, Me.ToolStripSeparator4, Me.tsKolumny, Me.ToolStripSeparator8})
        Me.tsNawigator.Location = New System.Drawing.Point(0, 630)
        Me.tsNawigator.Name = "tsNawigator"
        Me.tsNawigator.Size = New System.Drawing.Size(1314, 25)
        Me.tsNawigator.TabIndex = 9
        Me.tsNawigator.Text = "Wyúwietlaj po"
        '
        'btnPoczatek
        '
        Me.btnPoczatek.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnPoczatek.Enabled = False
        Me.btnPoczatek.Image = CType(resources.GetObject("btnPoczatek.Image"), System.Drawing.Image)
        Me.btnPoczatek.Name = "btnPoczatek"
        Me.btnPoczatek.RightToLeftAutoMirrorImage = True
        Me.btnPoczatek.Size = New System.Drawing.Size(23, 22)
        Me.btnPoczatek.Text = "Przejdü do pierwszego ekranu"
        '
        'btnPoprzedni
        '
        Me.btnPoprzedni.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnPoprzedni.Enabled = False
        Me.btnPoprzedni.Image = CType(resources.GetObject("btnPoprzedni.Image"), System.Drawing.Image)
        Me.btnPoprzedni.Name = "btnPoprzedni"
        Me.btnPoprzedni.RightToLeftAutoMirrorImage = True
        Me.btnPoprzedni.Size = New System.Drawing.Size(23, 22)
        Me.btnPoprzedni.Text = "Przejdü do poprzedniego ekranu"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'lblEkran
        '
        Me.lblEkran.Enabled = False
        Me.lblEkran.Name = "lblEkran"
        Me.lblEkran.Size = New System.Drawing.Size(36, 22)
        Me.lblEkran.Text = "Ekran"
        '
        'txtNumerEkranu
        '
        Me.txtNumerEkranu.AccessibleName = "Position"
        Me.txtNumerEkranu.AutoSize = False
        Me.txtNumerEkranu.Enabled = False
        Me.txtNumerEkranu.Name = "txtNumerEkranu"
        Me.txtNumerEkranu.Size = New System.Drawing.Size(30, 21)
        Me.txtNumerEkranu.ToolTipText = "Bieøπcy ekran"
        '
        'lblIloscEkranow
        '
        Me.lblIloscEkranow.Enabled = False
        Me.lblIloscEkranow.Name = "lblIloscEkranow"
        Me.lblIloscEkranow.Size = New System.Drawing.Size(21, 22)
        Me.lblIloscEkranow.Text = "z 1"
        Me.lblIloscEkranow.ToolTipText = "Ca≥kowita iloúÊ ekranÛw przybieøπcym filtrze"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'btnNastepny
        '
        Me.btnNastepny.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnNastepny.Enabled = False
        Me.btnNastepny.Image = CType(resources.GetObject("btnNastepny.Image"), System.Drawing.Image)
        Me.btnNastepny.Name = "btnNastepny"
        Me.btnNastepny.RightToLeftAutoMirrorImage = True
        Me.btnNastepny.Size = New System.Drawing.Size(23, 22)
        Me.btnNastepny.Text = "Przejdü do nastÍpnego ekranu"
        '
        'btnOstatni
        '
        Me.btnOstatni.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnOstatni.Enabled = False
        Me.btnOstatni.Image = CType(resources.GetObject("btnOstatni.Image"), System.Drawing.Image)
        Me.btnOstatni.Name = "btnOstatni"
        Me.btnOstatni.RightToLeftAutoMirrorImage = True
        Me.btnOstatni.Size = New System.Drawing.Size(23, 22)
        Me.btnOstatni.Text = "Przejdü do ostatniego ekranu"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'btnOdswiez
        '
        Me.btnOdswiez.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.btnOdswiez.Image = CType(resources.GetObject("btnOdswiez.Image"), System.Drawing.Image)
        Me.btnOdswiez.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.btnOdswiez.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnOdswiez.Name = "btnOdswiez"
        Me.btnOdswiez.Size = New System.Drawing.Size(71, 22)
        Me.btnOdswiez.Text = "&Odswieø"
        '
        'lblWyswietlajPo
        '
        Me.lblWyswietlajPo.Enabled = False
        Me.lblWyswietlajPo.Name = "lblWyswietlajPo"
        Me.lblWyswietlajPo.Size = New System.Drawing.Size(80, 22)
        Me.lblWyswietlajPo.Text = "Wyúwietlaj po"
        '
        'cmbIloscNaStronie
        '
        Me.cmbIloscNaStronie.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbIloscNaStronie.Enabled = False
        Me.cmbIloscNaStronie.Items.AddRange(New Object() {"10", "25", "50", "100", "200", "500", "1000"})
        Me.cmbIloscNaStronie.Name = "cmbIloscNaStronie"
        Me.cmbIloscNaStronie.Size = New System.Drawing.Size(121, 25)
        '
        'lblWierszyNaStronie
        '
        Me.lblWierszyNaStronie.Enabled = False
        Me.lblWierszyNaStronie.Name = "lblWierszyNaStronie"
        Me.lblWierszyNaStronie.Size = New System.Drawing.Size(102, 22)
        Me.lblWierszyNaStronie.Text = "wierszy na ekranie"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 25)
        '
        'tsKolumny
        '
        Me.tsKolumny.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.tsKolumny.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tsKolumny.Enabled = False
        Me.tsKolumny.Image = CType(resources.GetObject("tsKolumny.Image"), System.Drawing.Image)
        Me.tsKolumny.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsKolumny.Name = "tsKolumny"
        Me.tsKolumny.Size = New System.Drawing.Size(68, 22)
        Me.tsKolumny.Text = "Kolumny"
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(6, 25)
        '
        'listMarka
        '
        Me.listMarka.FormattingEnabled = True
        Me.listMarka.Location = New System.Drawing.Point(49, 8)
        Me.listMarka.Name = "listMarka"
        Me.listMarka.Size = New System.Drawing.Size(150, 34)
        Me.listMarka.TabIndex = 14
        '
        'ListBranza
        '
        Me.ListBranza.FormattingEnabled = True
        Me.ListBranza.Location = New System.Drawing.Point(254, 9)
        Me.ListBranza.Name = "ListBranza"
        Me.ListBranza.Size = New System.Drawing.Size(150, 34)
        Me.ListBranza.TabIndex = 15
        '
        'lblMarka
        '
        Me.lblMarka.AutoSize = True
        Me.lblMarka.Location = New System.Drawing.Point(3, 9)
        Me.lblMarka.Name = "lblMarka"
        Me.lblMarka.Size = New System.Drawing.Size(40, 13)
        Me.lblMarka.TabIndex = 16
        Me.lblMarka.Text = "Marka:"
        '
        'lblBranza
        '
        Me.lblBranza.AutoSize = True
        Me.lblBranza.Location = New System.Drawing.Point(205, 9)
        Me.lblBranza.Name = "lblBranza"
        Me.lblBranza.Size = New System.Drawing.Size(43, 13)
        Me.lblBranza.TabIndex = 17
        Me.lblBranza.Text = "Rodzaj:"
        '
        'lblNumer
        '
        Me.lblNumer.AutoSize = True
        Me.lblNumer.Location = New System.Drawing.Point(755, 12)
        Me.lblNumer.Name = "lblNumer"
        Me.lblNumer.Size = New System.Drawing.Size(41, 13)
        Me.lblNumer.TabIndex = 18
        Me.lblNumer.Text = "Numer:"
        '
        'tbNumer
        '
        Me.tbNumer.Location = New System.Drawing.Point(802, 10)
        Me.tbNumer.Name = "tbNumer"
        Me.tbNumer.Size = New System.Drawing.Size(150, 20)
        Me.tbNumer.TabIndex = 19
        '
        'lblNazwa
        '
        Me.lblNazwa.AutoSize = True
        Me.lblNazwa.Location = New System.Drawing.Point(958, 13)
        Me.lblNazwa.Name = "lblNazwa"
        Me.lblNazwa.Size = New System.Drawing.Size(43, 13)
        Me.lblNazwa.TabIndex = 20
        Me.lblNazwa.Text = "Nazwa:"
        '
        'tbNazwa
        '
        Me.tbNazwa.Location = New System.Drawing.Point(1007, 9)
        Me.tbNazwa.Name = "tbNazwa"
        Me.tbNazwa.Size = New System.Drawing.Size(150, 20)
        Me.tbNazwa.TabIndex = 21
        '
        'chbNowosci
        '
        Me.chbNowosci.AutoSize = True
        Me.chbNowosci.Location = New System.Drawing.Point(1176, 7)
        Me.chbNowosci.Name = "chbNowosci"
        Me.chbNowosci.Size = New System.Drawing.Size(123, 17)
        Me.chbNowosci.TabIndex = 22
        Me.chbNowosci.Text = "Pokaø tylko nowoúci"
        Me.chbNowosci.UseVisualStyleBackColor = True
        '
        'chbNieuzupelnione
        '
        Me.chbNieuzupelnione.AutoSize = True
        Me.chbNieuzupelnione.Location = New System.Drawing.Point(1176, 28)
        Me.chbNieuzupelnione.Name = "chbNieuzupelnione"
        Me.chbNieuzupelnione.Size = New System.Drawing.Size(132, 17)
        Me.chbNieuzupelnione.TabIndex = 23
        Me.chbNieuzupelnione.Text = "Pokaø nieuzupe≥nione"
        Me.chbNieuzupelnione.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(587, 10)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(36, 13)
        Me.Label7.TabIndex = 35
        Me.Label7.Text = "Grupa"
        '
        'listGrupa
        '
        Me.listGrupa.FormattingEnabled = True
        Me.listGrupa.Location = New System.Drawing.Point(629, 9)
        Me.listGrupa.Name = "listGrupa"
        Me.listGrupa.Size = New System.Drawing.Size(120, 34)
        Me.listGrupa.TabIndex = 37
        '
        'dgv
        '
        Me.dgv.AllowUserToAddRows = False
        Me.dgv.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue
        Me.dgv.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgv.ColumnHeadersHeight = 22
        Me.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv.DefaultCellStyle = DataGridViewCellStyle3
        Me.dgv.Location = New System.Drawing.Point(0, 48)
        Me.dgv.MultiSelect = False
        Me.dgv.Name = "dgv"
        Me.dgv.ReadOnly = True
        Me.dgv.RowHeadersVisible = False
        Me.dgv.Size = New System.Drawing.Size(1314, 562)
        Me.dgv.TabIndex = 10
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(411, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 13)
        Me.Label1.TabIndex = 38
        Me.Label1.Text = "Kategoria:"
        '
        'listKategoria
        '
        Me.listKategoria.FormattingEnabled = True
        Me.listKategoria.Location = New System.Drawing.Point(463, 9)
        Me.listKategoria.Name = "listKategoria"
        Me.listKategoria.Size = New System.Drawing.Size(120, 34)
        Me.listKategoria.TabIndex = 39
        '
        'ctrSKU
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.listKategoria)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.listGrupa)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.chbNieuzupelnione)
        Me.Controls.Add(Me.chbNowosci)
        Me.Controls.Add(Me.tbNazwa)
        Me.Controls.Add(Me.lblNazwa)
        Me.Controls.Add(Me.tbNumer)
        Me.Controls.Add(Me.lblNumer)
        Me.Controls.Add(Me.lblBranza)
        Me.Controls.Add(Me.lblMarka)
        Me.Controls.Add(Me.ListBranza)
        Me.Controls.Add(Me.listMarka)
        Me.Controls.Add(Me.dgv)
        Me.Controls.Add(Me.tsNawigator)
        Me.Name = "ctrSKU"
        Me.Size = New System.Drawing.Size(1314, 655)
        Me.tsNawigator.ResumeLayout(False)
        Me.tsNawigator.PerformLayout()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tsNawigator As System.Windows.Forms.ToolStrip
    Friend WithEvents btnPoczatek As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnPoprzedni As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents lblEkran As System.Windows.Forms.ToolStripLabel
    Friend WithEvents txtNumerEkranu As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents lblIloscEkranow As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnNastepny As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnOstatni As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnOdswiez As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblWyswietlajPo As System.Windows.Forms.ToolStripLabel
    Friend WithEvents cmbIloscNaStronie As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents lblWierszyNaStronie As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsKolumny As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents listMarka As System.Windows.Forms.CheckedListBox
    Friend WithEvents ListBranza As System.Windows.Forms.CheckedListBox
    Friend WithEvents lblMarka As System.Windows.Forms.Label
    Friend WithEvents lblBranza As System.Windows.Forms.Label
    Friend WithEvents lblNumer As System.Windows.Forms.Label
    Friend WithEvents tbNumer As System.Windows.Forms.TextBox
    Friend WithEvents lblNazwa As System.Windows.Forms.Label
    Friend WithEvents tbNazwa As System.Windows.Forms.TextBox
    Friend WithEvents chbNowosci As System.Windows.Forms.CheckBox
    Friend WithEvents chbNieuzupelnione As System.Windows.Forms.CheckBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents listGrupa As System.Windows.Forms.CheckedListBox
    Friend WithEvents dgv As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents listKategoria As System.Windows.Forms.CheckedListBox

End Class
