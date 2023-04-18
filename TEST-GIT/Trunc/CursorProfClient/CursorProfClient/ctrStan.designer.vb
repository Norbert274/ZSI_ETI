<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctrStan
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ctrStan))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
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
        Me.dgv = New System.Windows.Forms.DataGridView()
        Me.ts = New System.Windows.Forms.ToolStrip()
        Me.btnDoKoszyka = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnPodzielGrupa = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.lblIloscZaznaczonych = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnWyczyscSelect = New System.Windows.Forms.ToolStripButton()
        Me.lblNumer = New System.Windows.Forms.Label()
        Me.tbNumer = New System.Windows.Forms.TextBox()
        Me.lblNazwa = New System.Windows.Forms.Label()
        Me.tbNazwa = New System.Windows.Forms.TextBox()
        Me.TimerPokazGalerie = New System.Windows.Forms.Timer(Me.components)
        Me.pnlFiltr = New System.Windows.Forms.Panel()
        Me.btnWyczyscFiltry = New System.Windows.Forms.Button()
        Me.treeListGrupy = New DevExpress.XtraTreeList.TreeList()
        Me.chkMarkaAll = New System.Windows.Forms.CheckBox()
        Me.chkRodzajAll = New System.Windows.Forms.CheckBox()
        Me.chkKategoriaAll = New System.Windows.Forms.CheckBox()
        Me.chkgrupaAll = New System.Windows.Forms.CheckBox()
        Me.lblKategoria = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.listKategoria = New System.Windows.Forms.CheckedListBox()
        Me.listRodzaj = New System.Windows.Forms.CheckedListBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblGrupa = New System.Windows.Forms.Label()
        Me.chbNowosci = New System.Windows.Forms.CheckBox()
        Me.chbDostepne = New System.Windows.Forms.CheckBox()
        Me.listMarka = New System.Windows.Forms.CheckedListBox()
        Me.vscrolFiltr = New System.Windows.Forms.VScrollBar()
        Me.menu_kontekstowe = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.HistoriaProduktuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.tsNawigator.SuspendLayout()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ts.SuspendLayout()
        Me.pnlFiltr.SuspendLayout()
        CType(Me.treeListGrupy, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.menu_kontekstowe.SuspendLayout()
        Me.SuspendLayout()
        '
        'tsNawigator
        '
        Me.tsNawigator.BackColor = System.Drawing.Color.White
        Me.tsNawigator.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.tsNawigator.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnPoczatek, Me.btnPoprzedni, Me.ToolStripSeparator1, Me.lblEkran, Me.txtNumerEkranu, Me.lblIloscEkranow, Me.ToolStripSeparator2, Me.btnNastepny, Me.btnOstatni, Me.ToolStripSeparator3, Me.btnOdswiez, Me.lblWyswietlajPo, Me.cmbIloscNaStronie, Me.lblWierszyNaStronie, Me.ToolStripSeparator4, Me.tsKolumny, Me.ToolStripSeparator8})
        Me.tsNawigator.Location = New System.Drawing.Point(0, 661)
        Me.tsNawigator.Name = "tsNawigator"
        Me.tsNawigator.Size = New System.Drawing.Size(1314, 25)
        Me.tsNawigator.TabIndex = 9
        Me.tsNawigator.Text = "Wyœwietlaj po"
        '
        'btnPoczatek
        '
        Me.btnPoczatek.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnPoczatek.Image = CType(resources.GetObject("btnPoczatek.Image"), System.Drawing.Image)
        Me.btnPoczatek.Name = "btnPoczatek"
        Me.btnPoczatek.RightToLeftAutoMirrorImage = True
        Me.btnPoczatek.Size = New System.Drawing.Size(23, 22)
        Me.btnPoczatek.Text = "PrzejdŸ do pierwszego ekranu"
        '
        'btnPoprzedni
        '
        Me.btnPoprzedni.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnPoprzedni.Image = CType(resources.GetObject("btnPoprzedni.Image"), System.Drawing.Image)
        Me.btnPoprzedni.Name = "btnPoprzedni"
        Me.btnPoprzedni.RightToLeftAutoMirrorImage = True
        Me.btnPoprzedni.Size = New System.Drawing.Size(23, 22)
        Me.btnPoprzedni.Text = "PrzejdŸ do poprzedniego ekranu"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'lblEkran
        '
        Me.lblEkran.ForeColor = System.Drawing.Color.Black
        Me.lblEkran.Name = "lblEkran"
        Me.lblEkran.Size = New System.Drawing.Size(36, 22)
        Me.lblEkran.Text = "Ekran"
        '
        'txtNumerEkranu
        '
        Me.txtNumerEkranu.AccessibleName = "Position"
        Me.txtNumerEkranu.AutoSize = False
        Me.txtNumerEkranu.Name = "txtNumerEkranu"
        Me.txtNumerEkranu.Size = New System.Drawing.Size(30, 21)
        Me.txtNumerEkranu.ToolTipText = "Bie¿¹cy ekran"
        '
        'lblIloscEkranow
        '
        Me.lblIloscEkranow.ForeColor = System.Drawing.Color.Black
        Me.lblIloscEkranow.Name = "lblIloscEkranow"
        Me.lblIloscEkranow.Size = New System.Drawing.Size(21, 22)
        Me.lblIloscEkranow.Text = "z 1"
        Me.lblIloscEkranow.ToolTipText = "Ca³kowita iloœæ ekranów przybie¿¹cym filtrze"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'btnNastepny
        '
        Me.btnNastepny.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnNastepny.Image = CType(resources.GetObject("btnNastepny.Image"), System.Drawing.Image)
        Me.btnNastepny.Name = "btnNastepny"
        Me.btnNastepny.RightToLeftAutoMirrorImage = True
        Me.btnNastepny.Size = New System.Drawing.Size(23, 22)
        Me.btnNastepny.Text = "PrzejdŸ do nastêpnego ekranu"
        '
        'btnOstatni
        '
        Me.btnOstatni.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnOstatni.Image = CType(resources.GetObject("btnOstatni.Image"), System.Drawing.Image)
        Me.btnOstatni.Name = "btnOstatni"
        Me.btnOstatni.RightToLeftAutoMirrorImage = True
        Me.btnOstatni.Size = New System.Drawing.Size(23, 22)
        Me.btnOstatni.Text = "PrzejdŸ do ostatniego ekranu"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'btnOdswiez
        '
        Me.btnOdswiez.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.btnOdswiez.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnOdswiez.ForeColor = System.Drawing.Color.White
        Me.btnOdswiez.Image = CType(resources.GetObject("btnOdswiez.Image"), System.Drawing.Image)
        Me.btnOdswiez.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.btnOdswiez.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnOdswiez.Name = "btnOdswiez"
        Me.btnOdswiez.Size = New System.Drawing.Size(71, 22)
        Me.btnOdswiez.Text = "&Odswie¿"
        '
        'lblWyswietlajPo
        '
        Me.lblWyswietlajPo.BackColor = System.Drawing.Color.White
        Me.lblWyswietlajPo.ForeColor = System.Drawing.Color.Black
        Me.lblWyswietlajPo.Name = "lblWyswietlajPo"
        Me.lblWyswietlajPo.Size = New System.Drawing.Size(80, 22)
        Me.lblWyswietlajPo.Text = "Wyœwietlaj po"
        '
        'cmbIloscNaStronie
        '
        Me.cmbIloscNaStronie.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbIloscNaStronie.Items.AddRange(New Object() {"10", "25", "50", "100", "200", "500", "1000"})
        Me.cmbIloscNaStronie.Name = "cmbIloscNaStronie"
        Me.cmbIloscNaStronie.Size = New System.Drawing.Size(121, 25)
        '
        'lblWierszyNaStronie
        '
        Me.lblWierszyNaStronie.ForeColor = System.Drawing.Color.Black
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
        Me.tsKolumny.ForeColor = System.Drawing.Color.Black
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
        'dgv
        '
        Me.dgv.AllowUserToAddRows = False
        Me.dgv.AllowUserToDeleteRows = False
        Me.dgv.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue
        Me.dgv.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgv.ColumnHeadersHeight = 22
        Me.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgv.Location = New System.Drawing.Point(308, 28)
        Me.dgv.Name = "dgv"
        Me.dgv.ReadOnly = True
        Me.dgv.RowHeadersVisible = False
        Me.dgv.Size = New System.Drawing.Size(1003, 633)
        Me.dgv.TabIndex = 10
        '
        'ts
        '
        Me.ts.BackColor = System.Drawing.Color.DodgerBlue
        Me.ts.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnDoKoszyka, Me.ToolStripSeparator5, Me.btnPodzielGrupa, Me.ToolStripSeparator6, Me.lblIloscZaznaczonych, Me.ToolStripSeparator7, Me.btnWyczyscSelect})
        Me.ts.Location = New System.Drawing.Point(0, 0)
        Me.ts.Name = "ts"
        Me.ts.Size = New System.Drawing.Size(1314, 25)
        Me.ts.TabIndex = 1
        Me.ts.Text = "ToolStrip1"
        '
        'btnDoKoszyka
        '
        Me.btnDoKoszyka.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnDoKoszyka.ForeColor = System.Drawing.Color.White
        Me.btnDoKoszyka.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnDoKoszyka.Name = "btnDoKoszyka"
        Me.btnDoKoszyka.Size = New System.Drawing.Size(71, 22)
        Me.btnDoKoszyka.Text = "Do Koszyka"
        Me.btnDoKoszyka.ToolTipText = "Dodaje zaznaczone pozycje do koszyka"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(6, 25)
        '
        'btnPodzielGrupa
        '
        Me.btnPodzielGrupa.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnPodzielGrupa.ForeColor = System.Drawing.Color.White
        Me.btnPodzielGrupa.Image = CType(resources.GetObject("btnPodzielGrupa.Image"), System.Drawing.Image)
        Me.btnPodzielGrupa.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPodzielGrupa.Name = "btnPodzielGrupa"
        Me.btnPodzielGrupa.Size = New System.Drawing.Size(85, 22)
        Me.btnPodzielGrupa.Text = "Podziel Towar"
        Me.btnPodzielGrupa.ToolTipText = "Omo¿liwia podzielenie zaznaczonych pozycji na grupy podrzedne"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(6, 25)
        '
        'lblIloscZaznaczonych
        '
        Me.lblIloscZaznaczonych.ForeColor = System.Drawing.Color.White
        Me.lblIloscZaznaczonych.Name = "lblIloscZaznaczonych"
        Me.lblIloscZaznaczonych.Size = New System.Drawing.Size(94, 22)
        Me.lblIloscZaznaczonych.Text = "Zaznaczonych: 0"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(6, 25)
        '
        'btnWyczyscSelect
        '
        Me.btnWyczyscSelect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnWyczyscSelect.ForeColor = System.Drawing.Color.White
        Me.btnWyczyscSelect.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnWyczyscSelect.Name = "btnWyczyscSelect"
        Me.btnWyczyscSelect.Size = New System.Drawing.Size(121, 22)
        Me.btnWyczyscSelect.Text = "Wyczyœæ zaznaczenia"
        Me.btnWyczyscSelect.ToolTipText = "Omo¿liwia podzielenie zaznaczonych pozycji na grupy podrzedne"
        '
        'lblNumer
        '
        Me.lblNumer.AutoSize = True
        Me.lblNumer.ForeColor = System.Drawing.Color.White
        Me.lblNumer.Location = New System.Drawing.Point(4, 10)
        Me.lblNumer.Name = "lblNumer"
        Me.lblNumer.Size = New System.Drawing.Size(29, 13)
        Me.lblNumer.TabIndex = 18
        Me.lblNumer.Text = "Sku:"
        '
        'tbNumer
        '
        Me.tbNumer.Location = New System.Drawing.Point(3, 26)
        Me.tbNumer.Name = "tbNumer"
        Me.tbNumer.Size = New System.Drawing.Size(274, 20)
        Me.tbNumer.TabIndex = 19
        Me.ToolTip.SetToolTip(Me.tbNumer, "Filtruje po numerze produktu")
        '
        'lblNazwa
        '
        Me.lblNazwa.AutoSize = True
        Me.lblNazwa.ForeColor = System.Drawing.Color.White
        Me.lblNazwa.Location = New System.Drawing.Point(4, 51)
        Me.lblNazwa.Name = "lblNazwa"
        Me.lblNazwa.Size = New System.Drawing.Size(43, 13)
        Me.lblNazwa.TabIndex = 20
        Me.lblNazwa.Text = "Nazwa:"
        '
        'tbNazwa
        '
        Me.tbNazwa.Location = New System.Drawing.Point(3, 67)
        Me.tbNazwa.Name = "tbNazwa"
        Me.tbNazwa.Size = New System.Drawing.Size(274, 20)
        Me.tbNazwa.TabIndex = 21
        Me.ToolTip.SetToolTip(Me.tbNazwa, "Filtruje po nazwie produktu")
        '
        'TimerPokazGalerie
        '
        Me.TimerPokazGalerie.Interval = 10000
        '
        'pnlFiltr
        '
        Me.pnlFiltr.Controls.Add(Me.btnWyczyscFiltry)
        Me.pnlFiltr.Controls.Add(Me.treeListGrupy)
        Me.pnlFiltr.Controls.Add(Me.chkMarkaAll)
        Me.pnlFiltr.Controls.Add(Me.chkRodzajAll)
        Me.pnlFiltr.Controls.Add(Me.chkKategoriaAll)
        Me.pnlFiltr.Controls.Add(Me.chkgrupaAll)
        Me.pnlFiltr.Controls.Add(Me.lblKategoria)
        Me.pnlFiltr.Controls.Add(Me.Label4)
        Me.pnlFiltr.Controls.Add(Me.listKategoria)
        Me.pnlFiltr.Controls.Add(Me.listRodzaj)
        Me.pnlFiltr.Controls.Add(Me.Label1)
        Me.pnlFiltr.Controls.Add(Me.lblGrupa)
        Me.pnlFiltr.Controls.Add(Me.chbNowosci)
        Me.pnlFiltr.Controls.Add(Me.chbDostepne)
        Me.pnlFiltr.Controls.Add(Me.tbNazwa)
        Me.pnlFiltr.Controls.Add(Me.lblNumer)
        Me.pnlFiltr.Controls.Add(Me.lblNazwa)
        Me.pnlFiltr.Controls.Add(Me.tbNumer)
        Me.pnlFiltr.Controls.Add(Me.listMarka)
        Me.pnlFiltr.Location = New System.Drawing.Point(3, 27)
        Me.pnlFiltr.Name = "pnlFiltr"
        Me.pnlFiltr.Size = New System.Drawing.Size(280, 860)
        Me.pnlFiltr.TabIndex = 23
        '
        'btnWyczyscFiltry
        '
        Me.btnWyczyscFiltry.ForeColor = System.Drawing.SystemColors.Window
        Me.btnWyczyscFiltry.Location = New System.Drawing.Point(197, 4)
        Me.btnWyczyscFiltry.Name = "btnWyczyscFiltry"
        Me.btnWyczyscFiltry.Size = New System.Drawing.Size(80, 23)
        Me.btnWyczyscFiltry.TabIndex = 37
        Me.btnWyczyscFiltry.Text = "Wyczyœæ filtry"
        Me.btnWyczyscFiltry.UseVisualStyleBackColor = False
        '
        'treeListGrupy
        '
        Me.treeListGrupy.Location = New System.Drawing.Point(4, 149)
        Me.treeListGrupy.Name = "treeListGrupy"
        Me.treeListGrupy.OptionsView.ShowColumns = False
        Me.treeListGrupy.Size = New System.Drawing.Size(273, 156)
        Me.treeListGrupy.TabIndex = 24
        '
        'chkMarkaAll
        '
        Me.chkMarkaAll.AutoSize = True
        Me.chkMarkaAll.Checked = True
        Me.chkMarkaAll.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkMarkaAll.Location = New System.Drawing.Point(7, 441)
        Me.chkMarkaAll.Name = "chkMarkaAll"
        Me.chkMarkaAll.Size = New System.Drawing.Size(15, 14)
        Me.chkMarkaAll.TabIndex = 36
        Me.chkMarkaAll.UseVisualStyleBackColor = True
        '
        'chkRodzajAll
        '
        Me.chkRodzajAll.AutoSize = True
        Me.chkRodzajAll.Checked = True
        Me.chkRodzajAll.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkRodzajAll.Location = New System.Drawing.Point(7, 576)
        Me.chkRodzajAll.Name = "chkRodzajAll"
        Me.chkRodzajAll.Size = New System.Drawing.Size(15, 14)
        Me.chkRodzajAll.TabIndex = 35
        Me.chkRodzajAll.UseVisualStyleBackColor = True
        '
        'chkKategoriaAll
        '
        Me.chkKategoriaAll.AutoSize = True
        Me.chkKategoriaAll.Checked = True
        Me.chkKategoriaAll.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkKategoriaAll.Location = New System.Drawing.Point(7, 307)
        Me.chkKategoriaAll.Name = "chkKategoriaAll"
        Me.chkKategoriaAll.Size = New System.Drawing.Size(15, 14)
        Me.chkKategoriaAll.TabIndex = 34
        Me.chkKategoriaAll.UseVisualStyleBackColor = True
        '
        'chkgrupaAll
        '
        Me.chkgrupaAll.AutoSize = True
        Me.chkgrupaAll.Checked = True
        Me.chkgrupaAll.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkgrupaAll.Location = New System.Drawing.Point(7, 133)
        Me.chkgrupaAll.Name = "chkgrupaAll"
        Me.chkgrupaAll.Size = New System.Drawing.Size(15, 14)
        Me.chkgrupaAll.TabIndex = 33
        Me.chkgrupaAll.UseVisualStyleBackColor = True
        '
        'lblKategoria
        '
        Me.lblKategoria.AutoSize = True
        Me.lblKategoria.ForeColor = System.Drawing.Color.White
        Me.lblKategoria.Location = New System.Drawing.Point(24, 308)
        Me.lblKategoria.Name = "lblKategoria"
        Me.lblKategoria.Size = New System.Drawing.Size(55, 13)
        Me.lblKategoria.TabIndex = 32
        Me.lblKategoria.Text = "Kategoria:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(26, 442)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(40, 13)
        Me.Label4.TabIndex = 31
        Me.Label4.Text = "Marka:"
        '
        'listKategoria
        '
        Me.listKategoria.CheckOnClick = True
        Me.listKategoria.FormattingEnabled = True
        Me.listKategoria.Location = New System.Drawing.Point(4, 327)
        Me.listKategoria.Name = "listKategoria"
        Me.listKategoria.Size = New System.Drawing.Size(273, 109)
        Me.listKategoria.TabIndex = 30
        '
        'listRodzaj
        '
        Me.listRodzaj.CheckOnClick = True
        Me.listRodzaj.FormattingEnabled = True
        Me.listRodzaj.Location = New System.Drawing.Point(5, 593)
        Me.listRodzaj.Name = "listRodzaj"
        Me.listRodzaj.Size = New System.Drawing.Size(272, 109)
        Me.listRodzaj.TabIndex = 26
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(26, 577)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(43, 13)
        Me.Label1.TabIndex = 28
        Me.Label1.Text = "Rodzaj:"
        '
        'lblGrupa
        '
        Me.lblGrupa.AutoSize = True
        Me.lblGrupa.ForeColor = System.Drawing.Color.White
        Me.lblGrupa.Location = New System.Drawing.Point(24, 134)
        Me.lblGrupa.Name = "lblGrupa"
        Me.lblGrupa.Size = New System.Drawing.Size(39, 13)
        Me.lblGrupa.TabIndex = 27
        Me.lblGrupa.Text = "Grupa:"
        '
        'chbNowosci
        '
        Me.chbNowosci.AutoSize = True
        Me.chbNowosci.ForeColor = System.Drawing.Color.White
        Me.chbNowosci.Location = New System.Drawing.Point(7, 113)
        Me.chbNowosci.Name = "chbNowosci"
        Me.chbNowosci.Size = New System.Drawing.Size(123, 17)
        Me.chbNowosci.TabIndex = 24
        Me.chbNowosci.Text = "Poka¿ tylko nowoœci"
        Me.ToolTip.SetToolTip(Me.chbNowosci, "Pokazuje tylko produkty oznaczone jako nowoœæ")
        Me.chbNowosci.UseVisualStyleBackColor = True
        '
        'chbDostepne
        '
        Me.chbDostepne.AutoSize = True
        Me.chbDostepne.ForeColor = System.Drawing.Color.White
        Me.chbDostepne.Location = New System.Drawing.Point(7, 93)
        Me.chbDostepne.Name = "chbDostepne"
        Me.chbDostepne.Size = New System.Drawing.Size(132, 17)
        Me.chbDostepne.TabIndex = 23
        Me.chbDostepne.Text = "Poka¿ tylko niezerowe"
        Me.ToolTip.SetToolTip(Me.chbDostepne, "Pokazuje tylko stany wiêksze od zera")
        Me.chbDostepne.UseVisualStyleBackColor = True
        '
        'listMarka
        '
        Me.listMarka.CheckOnClick = True
        Me.listMarka.FormattingEnabled = True
        Me.listMarka.Location = New System.Drawing.Point(4, 462)
        Me.listMarka.Name = "listMarka"
        Me.listMarka.Size = New System.Drawing.Size(273, 109)
        Me.listMarka.TabIndex = 25
        '
        'vscrolFiltr
        '
        Me.vscrolFiltr.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.vscrolFiltr.Location = New System.Drawing.Point(286, 27)
        Me.vscrolFiltr.Maximum = 302
        Me.vscrolFiltr.Minimum = 28
        Me.vscrolFiltr.Name = "vscrolFiltr"
        Me.vscrolFiltr.Size = New System.Drawing.Size(19, 633)
        Me.vscrolFiltr.TabIndex = 0
        Me.vscrolFiltr.Value = 28
        '
        'menu_kontekstowe
        '
        Me.menu_kontekstowe.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HistoriaProduktuToolStripMenuItem})
        Me.menu_kontekstowe.Name = "menu_kontekstowe"
        Me.menu_kontekstowe.Size = New System.Drawing.Size(168, 26)
        '
        'HistoriaProduktuToolStripMenuItem
        '
        Me.HistoriaProduktuToolStripMenuItem.BackColor = System.Drawing.Color.White
        Me.HistoriaProduktuToolStripMenuItem.Name = "HistoriaProduktuToolStripMenuItem"
        Me.HistoriaProduktuToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
        Me.HistoriaProduktuToolStripMenuItem.Text = "Historia produktu"
        Me.HistoriaProduktuToolStripMenuItem.Visible = False
        '
        'ctrStan
        '
        Me.BackColor = System.Drawing.Color.DodgerBlue
        Me.Controls.Add(Me.ts)
        Me.Controls.Add(Me.tsNawigator)
        Me.Controls.Add(Me.pnlFiltr)
        Me.Controls.Add(Me.dgv)
        Me.Controls.Add(Me.vscrolFiltr)
        Me.Name = "ctrStan"
        Me.Size = New System.Drawing.Size(1314, 686)
        Me.tsNawigator.ResumeLayout(False)
        Me.tsNawigator.PerformLayout()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ts.ResumeLayout(False)
        Me.ts.PerformLayout()
        Me.pnlFiltr.ResumeLayout(False)
        Me.pnlFiltr.PerformLayout()
        CType(Me.treeListGrupy, System.ComponentModel.ISupportInitialize).EndInit()
        Me.menu_kontekstowe.ResumeLayout(False)
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
    Friend WithEvents dgv As System.Windows.Forms.DataGridView
    Friend WithEvents ts As System.Windows.Forms.ToolStrip
    Friend WithEvents btnDoKoszyka As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnPodzielGrupa As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblNumer As System.Windows.Forms.Label
    Friend WithEvents tbNumer As System.Windows.Forms.TextBox
    Friend WithEvents lblNazwa As System.Windows.Forms.Label
    Friend WithEvents tbNazwa As System.Windows.Forms.TextBox
    Friend WithEvents TimerPokazGalerie As System.Windows.Forms.Timer
    Friend WithEvents pnlFiltr As System.Windows.Forms.Panel
    Friend WithEvents lblKategoria As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents listKategoria As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblGrupa As System.Windows.Forms.Label
    Friend WithEvents listRodzaj As System.Windows.Forms.CheckedListBox
    Friend WithEvents listMarka As System.Windows.Forms.CheckedListBox
    Friend WithEvents chbNowosci As System.Windows.Forms.CheckBox
    Friend WithEvents chbDostepne As System.Windows.Forms.CheckBox
    Friend WithEvents vscrolFiltr As System.Windows.Forms.VScrollBar
    Friend WithEvents chkgrupaAll As System.Windows.Forms.CheckBox
    Friend WithEvents chkMarkaAll As System.Windows.Forms.CheckBox
    Friend WithEvents chkRodzajAll As System.Windows.Forms.CheckBox
    Friend WithEvents chkKategoriaAll As System.Windows.Forms.CheckBox
    Friend WithEvents menu_kontekstowe As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents HistoriaProduktuToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents btnWyczyscFiltry As System.Windows.Forms.Button
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents lblIloscZaznaczonych As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnWyczyscSelect As System.Windows.Forms.ToolStripButton
    Friend WithEvents treeListGrupy As DevExpress.XtraTreeList.TreeList

End Class
