<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmZamowienia
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
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmZamowienia))
        Me.dgv = New System.Windows.Forms.DataGridView()
        Me.btnNastepny = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.lblIloscEkranow = New System.Windows.Forms.ToolStripLabel()
        Me.txtNumerEkranu = New System.Windows.Forms.ToolStripTextBox()
        Me.lblEkran = New System.Windows.Forms.ToolStripLabel()
        Me.btnOstatni = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnOdswiez = New System.Windows.Forms.ToolStripButton()
        Me.tsKolumny = New System.Windows.Forms.ToolStripDropDownButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.lblWierszyNaStronie = New System.Windows.Forms.ToolStripLabel()
        Me.cmbIloscNaStronie = New System.Windows.Forms.ToolStripComboBox()
        Me.lblWyswietlajPo = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnPoprzedni = New System.Windows.Forms.ToolStripButton()
        Me.lblZakresDatOdOpis = New System.Windows.Forms.ToolStripLabel()
        Me.ts = New System.Windows.Forms.ToolStrip()
        Me.lblZakresDatDo = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.txtFiltruj = New System.Windows.Forms.ToolStripTextBox()
        Me.btnFiltruj = New System.Windows.Forms.ToolStripButton()
        Me.btnStworzNoweZamowienie = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnUsunZamowienieRobocze = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.cboStatusyZamowienia = New System.Windows.Forms.ToolStripComboBox()
        Me.btnPoczatek = New System.Windows.Forms.ToolStripButton()
        Me.tsNawigator = New System.Windows.Forms.ToolStrip()
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnExport = New System.Windows.Forms.ToolStripButton()
        Me.btnAnuluj = New System.Windows.Forms.Button()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.menu_kontekstowe = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.GenerujAwizoZwrotuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ts.SuspendLayout()
        Me.tsNawigator.SuspendLayout()
        Me.menu_kontekstowe.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgv
        '
        Me.dgv.AllowUserToAddRows = False
        Me.dgv.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue
        Me.dgv.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
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
        Me.dgv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgv.Location = New System.Drawing.Point(0, 25)
        Me.dgv.MultiSelect = False
        Me.dgv.Name = "dgv"
        Me.dgv.ReadOnly = True
        Me.dgv.RowHeadersVisible = False
        Me.dgv.Size = New System.Drawing.Size(977, 386)
        Me.dgv.TabIndex = 1
        '
        'btnNastepny
        '
        Me.btnNastepny.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnNastepny.Image = CType(resources.GetObject("btnNastepny.Image"), System.Drawing.Image)
        Me.btnNastepny.Name = "btnNastepny"
        Me.btnNastepny.RightToLeftAutoMirrorImage = True
        Me.btnNastepny.Size = New System.Drawing.Size(23, 29)
        Me.btnNastepny.Text = "PrzejdŸ do nastêpnego ekranu"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 32)
        '
        'lblIloscEkranow
        '
        Me.lblIloscEkranow.BackColor = System.Drawing.Color.DodgerBlue
        Me.lblIloscEkranow.ForeColor = System.Drawing.Color.Black
        Me.lblIloscEkranow.Name = "lblIloscEkranow"
        Me.lblIloscEkranow.Size = New System.Drawing.Size(21, 29)
        Me.lblIloscEkranow.Text = "z X"
        Me.lblIloscEkranow.ToolTipText = "Ca³kowita iloœæ ekranów przybie¿¹cym filtrze"
        '
        'txtNumerEkranu
        '
        Me.txtNumerEkranu.AccessibleName = "Position"
        Me.txtNumerEkranu.AutoSize = False
        Me.txtNumerEkranu.ForeColor = System.Drawing.Color.Black
        Me.txtNumerEkranu.Name = "txtNumerEkranu"
        Me.txtNumerEkranu.Size = New System.Drawing.Size(30, 21)
        Me.txtNumerEkranu.ToolTipText = "Bie¿¹cy ekran"
        '
        'lblEkran
        '
        Me.lblEkran.BackColor = System.Drawing.Color.DodgerBlue
        Me.lblEkran.ForeColor = System.Drawing.Color.Black
        Me.lblEkran.Name = "lblEkran"
        Me.lblEkran.Size = New System.Drawing.Size(34, 29)
        Me.lblEkran.Text = "Ekran"
        '
        'btnOstatni
        '
        Me.btnOstatni.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnOstatni.Image = CType(resources.GetObject("btnOstatni.Image"), System.Drawing.Image)
        Me.btnOstatni.Name = "btnOstatni"
        Me.btnOstatni.RightToLeftAutoMirrorImage = True
        Me.btnOstatni.Size = New System.Drawing.Size(23, 29)
        Me.btnOstatni.Text = "PrzejdŸ do ostatniego ekranu"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 32)
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
        Me.btnOdswiez.Size = New System.Drawing.Size(67, 29)
        Me.btnOdswiez.Text = "&Odswie¿"
        '
        'tsKolumny
        '
        Me.tsKolumny.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.tsKolumny.BackColor = System.Drawing.Color.DodgerBlue
        Me.tsKolumny.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tsKolumny.ForeColor = System.Drawing.Color.White
        Me.tsKolumny.Image = CType(resources.GetObject("tsKolumny.Image"), System.Drawing.Image)
        Me.tsKolumny.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsKolumny.Name = "tsKolumny"
        Me.tsKolumny.Size = New System.Drawing.Size(60, 29)
        Me.tsKolumny.Text = "Kolumny"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 32)
        '
        'lblWierszyNaStronie
        '
        Me.lblWierszyNaStronie.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblWierszyNaStronie.ForeColor = System.Drawing.Color.Black
        Me.lblWierszyNaStronie.Name = "lblWierszyNaStronie"
        Me.lblWierszyNaStronie.Size = New System.Drawing.Size(96, 29)
        Me.lblWierszyNaStronie.Text = "wierszy na ekranie"
        '
        'cmbIloscNaStronie
        '
        Me.cmbIloscNaStronie.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbIloscNaStronie.Items.AddRange(New Object() {"10", "25", "50", "100", "200", "500", "1000"})
        Me.cmbIloscNaStronie.Name = "cmbIloscNaStronie"
        Me.cmbIloscNaStronie.Size = New System.Drawing.Size(121, 32)
        '
        'lblWyswietlajPo
        '
        Me.lblWyswietlajPo.BackColor = System.Drawing.Color.DodgerBlue
        Me.lblWyswietlajPo.ForeColor = System.Drawing.Color.Black
        Me.lblWyswietlajPo.Name = "lblWyswietlajPo"
        Me.lblWyswietlajPo.Size = New System.Drawing.Size(74, 29)
        Me.lblWyswietlajPo.Text = "Wyœwietlaj po"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 32)
        '
        'btnPoprzedni
        '
        Me.btnPoprzedni.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnPoprzedni.Image = CType(resources.GetObject("btnPoprzedni.Image"), System.Drawing.Image)
        Me.btnPoprzedni.Name = "btnPoprzedni"
        Me.btnPoprzedni.RightToLeftAutoMirrorImage = True
        Me.btnPoprzedni.Size = New System.Drawing.Size(23, 29)
        Me.btnPoprzedni.Text = "PrzejdŸ do poprzedniego ekranu"
        '
        'lblZakresDatOdOpis
        '
        Me.lblZakresDatOdOpis.ForeColor = System.Drawing.Color.White
        Me.lblZakresDatOdOpis.Name = "lblZakresDatOdOpis"
        Me.lblZakresDatOdOpis.Size = New System.Drawing.Size(180, 22)
        Me.lblZakresDatOdOpis.Text = "Zakres dat z³o¿enia zamówienia: od "
        '
        'ts
        '
        Me.ts.BackColor = System.Drawing.Color.DodgerBlue
        Me.ts.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblZakresDatOdOpis, Me.lblZakresDatDo, Me.ToolStripSeparator5, Me.txtFiltruj, Me.btnFiltruj, Me.btnStworzNoweZamowienie, Me.ToolStripSeparator6, Me.btnUsunZamowienieRobocze, Me.ToolStripSeparator7, Me.cboStatusyZamowienia})
        Me.ts.Location = New System.Drawing.Point(0, 0)
        Me.ts.Name = "ts"
        Me.ts.Size = New System.Drawing.Size(977, 25)
        Me.ts.TabIndex = 0
        Me.ts.Text = "ToolStrip1"
        '
        'lblZakresDatDo
        '
        Me.lblZakresDatDo.ForeColor = System.Drawing.Color.White
        Me.lblZakresDatDo.Name = "lblZakresDatDo"
        Me.lblZakresDatDo.Size = New System.Drawing.Size(25, 22)
        Me.lblZakresDatDo.Text = " do "
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(6, 25)
        '
        'txtFiltruj
        '
        Me.txtFiltruj.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFiltruj.Name = "txtFiltruj"
        Me.txtFiltruj.Size = New System.Drawing.Size(150, 25)
        Me.txtFiltruj.ToolTipText = "Filtrowanie po numer zamówienia, sk³adaj¹cy zamówienie, status zamówienia, typ za" & _
    "mówienia, uwagi, Numer listu przewozowego, status przesy³ki"
        '
        'btnFiltruj
        '
        Me.btnFiltruj.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnFiltruj.ForeColor = System.Drawing.Color.White
        Me.btnFiltruj.Image = CType(resources.GetObject("btnFiltruj.Image"), System.Drawing.Image)
        Me.btnFiltruj.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnFiltruj.Name = "btnFiltruj"
        Me.btnFiltruj.Size = New System.Drawing.Size(38, 22)
        Me.btnFiltruj.Text = "Filtruj"
        '
        'btnStworzNoweZamowienie
        '
        Me.btnStworzNoweZamowienie.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.btnStworzNoweZamowienie.ForeColor = System.Drawing.Color.White
        Me.btnStworzNoweZamowienie.Image = CType(resources.GetObject("btnStworzNoweZamowienie.Image"), System.Drawing.Image)
        Me.btnStworzNoweZamowienie.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnStworzNoweZamowienie.Name = "btnStworzNoweZamowienie"
        Me.btnStworzNoweZamowienie.Size = New System.Drawing.Size(147, 22)
        Me.btnStworzNoweZamowienie.Text = "Stwórz nowe zamówienie"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(6, 25)
        '
        'btnUsunZamowienieRobocze
        '
        Me.btnUsunZamowienieRobocze.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.btnUsunZamowienieRobocze.Enabled = False
        Me.btnUsunZamowienieRobocze.ForeColor = System.Drawing.Color.White
        Me.btnUsunZamowienieRobocze.Image = CType(resources.GetObject("btnUsunZamowienieRobocze.Image"), System.Drawing.Image)
        Me.btnUsunZamowienieRobocze.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnUsunZamowienieRobocze.Name = "btnUsunZamowienieRobocze"
        Me.btnUsunZamowienieRobocze.Size = New System.Drawing.Size(150, 22)
        Me.btnUsunZamowienieRobocze.Text = "Usuñ zamówienie robocze"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(6, 25)
        '
        'cboStatusyZamowienia
        '
        Me.cboStatusyZamowienia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStatusyZamowienia.Name = "cboStatusyZamowienia"
        Me.cboStatusyZamowienia.Size = New System.Drawing.Size(200, 25)
        '
        'btnPoczatek
        '
        Me.btnPoczatek.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnPoczatek.Image = CType(resources.GetObject("btnPoczatek.Image"), System.Drawing.Image)
        Me.btnPoczatek.Name = "btnPoczatek"
        Me.btnPoczatek.RightToLeftAutoMirrorImage = True
        Me.btnPoczatek.Size = New System.Drawing.Size(23, 29)
        Me.btnPoczatek.Text = "PrzejdŸ do pierwszego ekranu"
        '
        'tsNawigator
        '
        Me.tsNawigator.BackColor = System.Drawing.Color.White
        Me.tsNawigator.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.tsNawigator.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnPoczatek, Me.btnPoprzedni, Me.ToolStripSeparator1, Me.lblEkran, Me.txtNumerEkranu, Me.lblIloscEkranow, Me.ToolStripSeparator2, Me.btnNastepny, Me.btnOstatni, Me.ToolStripSeparator3, Me.btnOdswiez, Me.lblWyswietlajPo, Me.cmbIloscNaStronie, Me.lblWierszyNaStronie, Me.ToolStripSeparator4, Me.tsKolumny, Me.ToolStripSeparator8, Me.btnExport})
        Me.tsNawigator.Location = New System.Drawing.Point(0, 411)
        Me.tsNawigator.Name = "tsNawigator"
        Me.tsNawigator.Size = New System.Drawing.Size(977, 32)
        Me.tsNawigator.TabIndex = 2
        Me.tsNawigator.Text = "Wyœwietlaj po"
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(6, 32)
        '
        'btnExport
        '
        Me.btnExport.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.btnExport.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnExport.ForeColor = System.Drawing.Color.White
        Me.btnExport.Image = CType(resources.GetObject("btnExport.Image"), System.Drawing.Image)
        Me.btnExport.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.btnExport.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(68, 29)
        Me.btnExport.Text = "Export"
        '
        'btnAnuluj
        '
        Me.btnAnuluj.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAnuluj.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnAnuluj.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnAnuluj.ForeColor = System.Drawing.Color.White
        Me.btnAnuluj.Location = New System.Drawing.Point(748, 418)
        Me.btnAnuluj.Name = "btnAnuluj"
        Me.btnAnuluj.Size = New System.Drawing.Size(75, 23)
        Me.btnAnuluj.TabIndex = 11
        Me.btnAnuluj.Text = "Anuluj"
        Me.btnAnuluj.UseVisualStyleBackColor = False
        '
        'menu_kontekstowe
        '
        Me.menu_kontekstowe.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.GenerujAwizoZwrotuToolStripMenuItem})
        Me.menu_kontekstowe.Name = "menu_kontekstowe"
        Me.menu_kontekstowe.Size = New System.Drawing.Size(179, 26)
        '
        'GenerujAwizoZwrotuToolStripMenuItem
        '
        Me.GenerujAwizoZwrotuToolStripMenuItem.BackColor = System.Drawing.Color.White
        Me.GenerujAwizoZwrotuToolStripMenuItem.Name = "GenerujAwizoZwrotuToolStripMenuItem"
        Me.GenerujAwizoZwrotuToolStripMenuItem.Size = New System.Drawing.Size(178, 22)
        Me.GenerujAwizoZwrotuToolStripMenuItem.Text = "Generuj awizo zwrotu"
        '
        'frmZamowienia
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DodgerBlue
        Me.CancelButton = Me.btnAnuluj
        Me.ClientSize = New System.Drawing.Size(977, 443)
        Me.Controls.Add(Me.dgv)
        Me.Controls.Add(Me.ts)
        Me.Controls.Add(Me.tsNawigator)
        Me.Controls.Add(Me.btnAnuluj)
        Me.Name = "frmZamowienia"
        Me.Text = "Zamówienia"
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ts.ResumeLayout(False)
        Me.ts.PerformLayout()
        Me.tsNawigator.ResumeLayout(False)
        Me.tsNawigator.PerformLayout()
        Me.menu_kontekstowe.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgv As System.Windows.Forms.DataGridView
    Friend WithEvents btnNastepny As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents lblIloscEkranow As System.Windows.Forms.ToolStripLabel
    Friend WithEvents txtNumerEkranu As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents lblEkran As System.Windows.Forms.ToolStripLabel
    Friend WithEvents btnOstatni As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnOdswiez As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsKolumny As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents lblWierszyNaStronie As System.Windows.Forms.ToolStripLabel
    Friend WithEvents cmbIloscNaStronie As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents lblWyswietlajPo As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnPoprzedni As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblZakresDatOdOpis As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ts As System.Windows.Forms.ToolStrip
    Friend WithEvents btnPoczatek As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsNawigator As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents lblZakresDatDo As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnStworzNoweZamowienie As System.Windows.Forms.ToolStripButton
    Friend WithEvents txtFiltruj As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents btnFiltruj As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnUsunZamowienieRobocze As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnAnuluj As System.Windows.Forms.Button
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cboStatusyZamowienia As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents btnExport As System.Windows.Forms.ToolStripButton
    Friend WithEvents menu_kontekstowe As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents GenerujAwizoZwrotuToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
