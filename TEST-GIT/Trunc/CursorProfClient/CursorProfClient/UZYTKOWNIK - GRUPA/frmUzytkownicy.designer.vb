<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUzytkownicy
    Inherits System.Windows.Forms.Form

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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUzytkownicy))
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.dgv = New System.Windows.Forms.DataGridView()
        Me.lblIloscEkranow = New System.Windows.Forms.ToolStripLabel()
        Me.btnNastepny = New System.Windows.Forms.ToolStripButton()
        Me.txtNumerEkranu = New System.Windows.Forms.ToolStripTextBox()
        Me.btnPoprzedni = New System.Windows.Forms.ToolStripButton()
        Me.lblEkran = New System.Windows.Forms.ToolStripLabel()
        Me.btnOstatni = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.sc = New System.Windows.Forms.SplitContainer()
        Me.tv = New System.Windows.Forms.TreeView()
        Me.tsGrupy = New System.Windows.Forms.ToolStrip()
        Me.lblWszystkie = New System.Windows.Forms.ToolStripLabel()
        Me.btnZaznaczWszystkie = New System.Windows.Forms.ToolStripButton()
        Me.btnOdznaczWszystkie = New System.Windows.Forms.ToolStripButton()
        Me.tsGorny = New System.Windows.Forms.ToolStrip()
        Me.btnGrupy = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnNowy = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator9 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnEdytuj = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnUsun = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.txtFiltruj = New System.Windows.Forms.ToolStripTextBox()
        Me.btnFiltruj = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator10 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnWyczyscFiltry = New System.Windows.Forms.ToolStripButton()
        Me.lblWierszyNaStronie = New System.Windows.Forms.ToolStripLabel()
        Me.btnOdswiez = New System.Windows.Forms.ToolStripButton()
        Me.cmbIloscNaStronie = New System.Windows.Forms.ToolStripComboBox()
        Me.btnPoczatek = New System.Windows.Forms.ToolStripButton()
        Me.lblWyswietlajPo = New System.Windows.Forms.ToolStripLabel()
        Me.tsNawigator = New System.Windows.Forms.ToolStrip()
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnAnuluj = New System.Windows.Forms.Button()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sc.Panel1.SuspendLayout()
        Me.sc.Panel2.SuspendLayout()
        Me.sc.SuspendLayout()
        Me.tsGrupy.SuspendLayout()
        Me.tsGorny.SuspendLayout()
        Me.tsNawigator.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'dgv
        '
        Me.dgv.AllowUserToAddRows = False
        Me.dgv.AllowUserToDeleteRows = False
        Me.dgv.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue
        Me.dgv.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgv.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv.DefaultCellStyle = DataGridViewCellStyle3
        Me.dgv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgv.Location = New System.Drawing.Point(0, 25)
        Me.dgv.Name = "dgv"
        Me.dgv.ReadOnly = True
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.dgv.RowHeadersVisible = False
        Me.dgv.Size = New System.Drawing.Size(779, 370)
        Me.dgv.TabIndex = 10
        '
        'lblIloscEkranow
        '
        Me.lblIloscEkranow.ForeColor = System.Drawing.Color.Black
        Me.lblIloscEkranow.Name = "lblIloscEkranow"
        Me.lblIloscEkranow.Size = New System.Drawing.Size(22, 22)
        Me.lblIloscEkranow.Text = "z X"
        Me.lblIloscEkranow.ToolTipText = "Ca�kowita ilo�� ekran�w przybie��cym filtrze"
        '
        'btnNastepny
        '
        Me.btnNastepny.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnNastepny.Image = CType(resources.GetObject("btnNastepny.Image"), System.Drawing.Image)
        Me.btnNastepny.Name = "btnNastepny"
        Me.btnNastepny.RightToLeftAutoMirrorImage = True
        Me.btnNastepny.Size = New System.Drawing.Size(23, 22)
        Me.btnNastepny.Text = "Przejd� do nast�pnego ekranu"
        '
        'txtNumerEkranu
        '
        Me.txtNumerEkranu.AccessibleName = "Position"
        Me.txtNumerEkranu.AutoSize = False
        Me.txtNumerEkranu.ForeColor = System.Drawing.Color.Black
        Me.txtNumerEkranu.Name = "txtNumerEkranu"
        Me.txtNumerEkranu.Size = New System.Drawing.Size(30, 21)
        Me.txtNumerEkranu.ToolTipText = "Bie��cy ekran"
        '
        'btnPoprzedni
        '
        Me.btnPoprzedni.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnPoprzedni.Image = CType(resources.GetObject("btnPoprzedni.Image"), System.Drawing.Image)
        Me.btnPoprzedni.Name = "btnPoprzedni"
        Me.btnPoprzedni.RightToLeftAutoMirrorImage = True
        Me.btnPoprzedni.Size = New System.Drawing.Size(23, 22)
        Me.btnPoprzedni.Text = "Przejd� do poprzedniego ekranu"
        '
        'lblEkran
        '
        Me.lblEkran.ForeColor = System.Drawing.Color.Black
        Me.lblEkran.Name = "lblEkran"
        Me.lblEkran.Size = New System.Drawing.Size(36, 22)
        Me.lblEkran.Text = "Ekran"
        '
        'btnOstatni
        '
        Me.btnOstatni.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnOstatni.Image = CType(resources.GetObject("btnOstatni.Image"), System.Drawing.Image)
        Me.btnOstatni.Name = "btnOstatni"
        Me.btnOstatni.RightToLeftAutoMirrorImage = True
        Me.btnOstatni.Size = New System.Drawing.Size(23, 22)
        Me.btnOstatni.Text = "Przejd� do ostatniego ekranu"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripSeparator4.BackColor = System.Drawing.Color.DodgerBlue
        Me.ToolStripSeparator4.ForeColor = System.Drawing.Color.White
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 25)
        '
        'sc
        '
        Me.sc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sc.Location = New System.Drawing.Point(0, 0)
        Me.sc.Name = "sc"
        '
        'sc.Panel1
        '
        Me.sc.Panel1.Controls.Add(Me.tv)
        Me.sc.Panel1.Controls.Add(Me.tsGrupy)
        Me.sc.Panel1Collapsed = True
        Me.sc.Panel1MinSize = 180
        '
        'sc.Panel2
        '
        Me.sc.Panel2.Controls.Add(Me.dgv)
        Me.sc.Panel2.Controls.Add(Me.tsGorny)
        Me.sc.Size = New System.Drawing.Size(779, 395)
        Me.sc.SplitterDistance = 180
        Me.sc.TabIndex = 8
        '
        'tv
        '
        Me.tv.CheckBoxes = True
        Me.tv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tv.Location = New System.Drawing.Point(0, 25)
        Me.tv.Name = "tv"
        Me.tv.Size = New System.Drawing.Size(180, 75)
        Me.tv.TabIndex = 11
        '
        'tsGrupy
        '
        Me.tsGrupy.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblWszystkie, Me.btnZaznaczWszystkie, Me.btnOdznaczWszystkie})
        Me.tsGrupy.Location = New System.Drawing.Point(0, 0)
        Me.tsGrupy.Name = "tsGrupy"
        Me.tsGrupy.Size = New System.Drawing.Size(180, 25)
        Me.tsGrupy.TabIndex = 10
        Me.tsGrupy.Text = "ToolStrip1"
        '
        'lblWszystkie
        '
        Me.lblWszystkie.Name = "lblWszystkie"
        Me.lblWszystkie.Size = New System.Drawing.Size(61, 22)
        Me.lblWszystkie.Text = "Wszystkie:"
        '
        'btnZaznaczWszystkie
        '
        Me.btnZaznaczWszystkie.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnZaznaczWszystkie.Image = CType(resources.GetObject("btnZaznaczWszystkie.Image"), System.Drawing.Image)
        Me.btnZaznaczWszystkie.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnZaznaczWszystkie.Name = "btnZaznaczWszystkie"
        Me.btnZaznaczWszystkie.Size = New System.Drawing.Size(53, 22)
        Me.btnZaznaczWszystkie.Text = "Zaznacz"
        '
        'btnOdznaczWszystkie
        '
        Me.btnOdznaczWszystkie.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnOdznaczWszystkie.Image = CType(resources.GetObject("btnOdznaczWszystkie.Image"), System.Drawing.Image)
        Me.btnOdznaczWszystkie.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnOdznaczWszystkie.Name = "btnOdznaczWszystkie"
        Me.btnOdznaczWszystkie.Size = New System.Drawing.Size(56, 22)
        Me.btnOdznaczWszystkie.Text = "Odznacz"
        '
        'tsGorny
        '
        Me.tsGorny.BackColor = System.Drawing.Color.White
        Me.tsGorny.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnGrupy, Me.ToolStripSeparator7, Me.btnNowy, Me.ToolStripSeparator9, Me.btnEdytuj, Me.ToolStripSeparator6, Me.btnUsun, Me.ToolStripSeparator5, Me.txtFiltruj, Me.btnFiltruj, Me.ToolStripSeparator10, Me.btnWyczyscFiltry})
        Me.tsGorny.Location = New System.Drawing.Point(0, 0)
        Me.tsGorny.Name = "tsGorny"
        Me.tsGorny.Size = New System.Drawing.Size(779, 25)
        Me.tsGorny.TabIndex = 8
        Me.tsGorny.Text = "ToolStrip3"
        '
        'btnGrupy
        '
        Me.btnGrupy.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnGrupy.ForeColor = System.Drawing.Color.White
        Me.btnGrupy.Image = CType(resources.GetObject("btnGrupy.Image"), System.Drawing.Image)
        Me.btnGrupy.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnGrupy.Name = "btnGrupy"
        Me.btnGrupy.Size = New System.Drawing.Size(92, 22)
        Me.btnGrupy.Text = "Poka� grupy"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.ForeColor = System.Drawing.Color.Black
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(6, 25)
        '
        'btnNowy
        '
        Me.btnNowy.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnNowy.ForeColor = System.Drawing.Color.White
        Me.btnNowy.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnNowy.Name = "btnNowy"
        Me.btnNowy.Size = New System.Drawing.Size(42, 22)
        Me.btnNowy.Text = "Nowy"
        '
        'ToolStripSeparator9
        '
        Me.ToolStripSeparator9.ForeColor = System.Drawing.Color.Black
        Me.ToolStripSeparator9.Name = "ToolStripSeparator9"
        Me.ToolStripSeparator9.Size = New System.Drawing.Size(6, 25)
        '
        'btnEdytuj
        '
        Me.btnEdytuj.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnEdytuj.ForeColor = System.Drawing.Color.White
        Me.btnEdytuj.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnEdytuj.Name = "btnEdytuj"
        Me.btnEdytuj.Size = New System.Drawing.Size(44, 22)
        Me.btnEdytuj.Text = "Edytuj"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.ForeColor = System.Drawing.Color.Black
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(6, 25)
        '
        'btnUsun
        '
        Me.btnUsun.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnUsun.ForeColor = System.Drawing.Color.White
        Me.btnUsun.Name = "btnUsun"
        Me.btnUsun.RightToLeftAutoMirrorImage = True
        Me.btnUsun.Size = New System.Drawing.Size(38, 22)
        Me.btnUsun.Text = "Usu�"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.ForeColor = System.Drawing.Color.Black
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(6, 25)
        '
        'txtFiltruj
        '
        Me.txtFiltruj.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFiltruj.Name = "txtFiltruj"
        Me.txtFiltruj.Size = New System.Drawing.Size(200, 25)
        '
        'btnFiltruj
        '
        Me.btnFiltruj.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnFiltruj.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnFiltruj.ForeColor = System.Drawing.Color.White
        Me.btnFiltruj.Image = CType(resources.GetObject("btnFiltruj.Image"), System.Drawing.Image)
        Me.btnFiltruj.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnFiltruj.Name = "btnFiltruj"
        Me.btnFiltruj.Size = New System.Drawing.Size(41, 22)
        Me.btnFiltruj.Text = "Filtruj"
        '
        'ToolStripSeparator10
        '
        Me.ToolStripSeparator10.Name = "ToolStripSeparator10"
        Me.ToolStripSeparator10.Size = New System.Drawing.Size(6, 25)
        '
        'btnWyczyscFiltry
        '
        Me.btnWyczyscFiltry.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnWyczyscFiltry.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnWyczyscFiltry.ForeColor = System.Drawing.Color.White
        Me.btnWyczyscFiltry.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnWyczyscFiltry.Name = "btnWyczyscFiltry"
        Me.btnWyczyscFiltry.Size = New System.Drawing.Size(77, 22)
        Me.btnWyczyscFiltry.Text = "Wyczy�c filtr"
        '
        'lblWierszyNaStronie
        '
        Me.lblWierszyNaStronie.ForeColor = System.Drawing.Color.Black
        Me.lblWierszyNaStronie.Name = "lblWierszyNaStronie"
        Me.lblWierszyNaStronie.Size = New System.Drawing.Size(102, 22)
        Me.lblWierszyNaStronie.Text = "wierszy na ekranie"
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
        Me.btnOdswiez.Text = "&Odswie�"
        '
        'cmbIloscNaStronie
        '
        Me.cmbIloscNaStronie.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbIloscNaStronie.Items.AddRange(New Object() {"10", "25", "50", "100", "200", "500", "1000"})
        Me.cmbIloscNaStronie.Name = "cmbIloscNaStronie"
        Me.cmbIloscNaStronie.Size = New System.Drawing.Size(121, 25)
        '
        'btnPoczatek
        '
        Me.btnPoczatek.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnPoczatek.Image = CType(resources.GetObject("btnPoczatek.Image"), System.Drawing.Image)
        Me.btnPoczatek.Name = "btnPoczatek"
        Me.btnPoczatek.RightToLeftAutoMirrorImage = True
        Me.btnPoczatek.Size = New System.Drawing.Size(23, 22)
        Me.btnPoczatek.Text = "Przejd� do pierwszego ekranu"
        '
        'lblWyswietlajPo
        '
        Me.lblWyswietlajPo.ForeColor = System.Drawing.Color.Black
        Me.lblWyswietlajPo.Name = "lblWyswietlajPo"
        Me.lblWyswietlajPo.Size = New System.Drawing.Size(80, 22)
        Me.lblWyswietlajPo.Text = "Wy�wietlaj po"
        '
        'tsNawigator
        '
        Me.tsNawigator.BackColor = System.Drawing.Color.White
        Me.tsNawigator.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.tsNawigator.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnPoczatek, Me.btnPoprzedni, Me.ToolStripSeparator1, Me.lblEkran, Me.txtNumerEkranu, Me.lblIloscEkranow, Me.ToolStripSeparator2, Me.btnNastepny, Me.btnOstatni, Me.ToolStripSeparator3, Me.btnOdswiez, Me.lblWyswietlajPo, Me.cmbIloscNaStronie, Me.lblWierszyNaStronie, Me.ToolStripSeparator4, Me.ToolStripSeparator8})
        Me.tsNawigator.Location = New System.Drawing.Point(0, 395)
        Me.tsNawigator.Name = "tsNawigator"
        Me.tsNawigator.Size = New System.Drawing.Size(779, 25)
        Me.tsNawigator.TabIndex = 9
        Me.tsNawigator.Text = "Wy�wietlaj po"
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(6, 25)
        '
        'btnAnuluj
        '
        Me.btnAnuluj.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAnuluj.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnAnuluj.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnAnuluj.ForeColor = System.Drawing.Color.White
        Me.btnAnuluj.Location = New System.Drawing.Point(550, 395)
        Me.btnAnuluj.Name = "btnAnuluj"
        Me.btnAnuluj.Size = New System.Drawing.Size(75, 23)
        Me.btnAnuluj.TabIndex = 11
        Me.btnAnuluj.Text = "Anuluj"
        Me.btnAnuluj.UseVisualStyleBackColor = False
        '
        'frmUzytkownicy
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnAnuluj
        Me.ClientSize = New System.Drawing.Size(779, 420)
        Me.Controls.Add(Me.sc)
        Me.Controls.Add(Me.tsNawigator)
        Me.Controls.Add(Me.btnAnuluj)
        Me.Name = "frmUzytkownicy"
        Me.Text = "U�ytkownicy"
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sc.Panel1.ResumeLayout(False)
        Me.sc.Panel1.PerformLayout()
        Me.sc.Panel2.ResumeLayout(False)
        Me.sc.Panel2.PerformLayout()
        CType(Me.sc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sc.ResumeLayout(False)
        Me.tsGrupy.ResumeLayout(False)
        Me.tsGrupy.PerformLayout()
        Me.tsGorny.ResumeLayout(False)
        Me.tsGorny.PerformLayout()
        Me.tsNawigator.ResumeLayout(False)
        Me.tsNawigator.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents dgv As System.Windows.Forms.DataGridView
    Friend WithEvents lblIloscEkranow As System.Windows.Forms.ToolStripLabel
    Friend WithEvents btnNastepny As System.Windows.Forms.ToolStripButton
    Friend WithEvents txtNumerEkranu As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents btnPoprzedni As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblEkran As System.Windows.Forms.ToolStripLabel
    Friend WithEvents btnOstatni As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents sc As System.Windows.Forms.SplitContainer
    Friend WithEvents tv As System.Windows.Forms.TreeView
    Friend WithEvents tsGrupy As System.Windows.Forms.ToolStrip
    Friend WithEvents lblWszystkie As System.Windows.Forms.ToolStripLabel
    Friend WithEvents btnZaznaczWszystkie As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnOdznaczWszystkie As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsGorny As System.Windows.Forms.ToolStrip
    Friend WithEvents btnGrupy As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnNowy As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnEdytuj As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnUsun As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents txtFiltruj As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents btnFiltruj As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents lblWierszyNaStronie As System.Windows.Forms.ToolStripLabel
    Friend WithEvents btnOdswiez As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmbIloscNaStronie As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents btnPoczatek As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblWyswietlajPo As System.Windows.Forms.ToolStripLabel
    Friend WithEvents tsNawigator As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator9 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnAnuluj As System.Windows.Forms.Button
    Friend WithEvents ToolStripSeparator10 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnWyczyscFiltry As System.Windows.Forms.ToolStripButton
End Class
