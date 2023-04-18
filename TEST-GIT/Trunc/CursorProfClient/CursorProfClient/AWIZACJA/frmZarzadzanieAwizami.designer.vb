<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmZarzadzanieAwizami
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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmZarzadzanieAwizami))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dgv = New System.Windows.Forms.DataGridView()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtSKU = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.btnWyczyscFiltry = New System.Windows.Forms.Button()
        Me.txtDostawca = New System.Windows.Forms.TextBox()
        Me.lblDostawca = New System.Windows.Forms.Label()
        Me.btnFiltruj = New System.Windows.Forms.Button()
        Me.dtpUtworzoneDo = New System.Windows.Forms.DateTimePicker()
        Me.dtpUtworzoneOd = New System.Windows.Forms.DateTimePicker()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.dtpPlanowanaDo = New System.Windows.Forms.DateTimePicker()
        Me.dtpPlanowanaOd = New System.Windows.Forms.DateTimePicker()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.chkListStatus = New System.Windows.Forms.CheckedListBox()
        Me.txtQGUAR_DOSTAWA = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtQGUAR_ZA = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtNrPO = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtNrAwiza = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
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
        Me.btnZamknij = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnOdswiez = New System.Windows.Forms.ToolStripButton()
        Me.lblWyswietlajPo = New System.Windows.Forms.ToolStripLabel()
        Me.cmbIloscNaStronie = New System.Windows.Forms.ToolStripComboBox()
        Me.lblWierszyNaStronie = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsKolumny = New System.Windows.Forms.ToolStripDropDownButton()
        Me.btnAnuluj = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.menu_kontekstowe = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.EdytujToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PokazSzczegolyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AnulujAwizoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.tsNawigator.SuspendLayout()
        Me.menu_kontekstowe.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(153, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Lista awiz:"
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
        Me.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv.DefaultCellStyle = DataGridViewCellStyle3
        Me.dgv.Location = New System.Drawing.Point(152, 19)
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
        Me.dgv.Size = New System.Drawing.Size(652, 501)
        Me.dgv.TabIndex = 2
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtSKU)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.btnWyczyscFiltry)
        Me.GroupBox1.Controls.Add(Me.txtDostawca)
        Me.GroupBox1.Controls.Add(Me.lblDostawca)
        Me.GroupBox1.Controls.Add(Me.btnFiltruj)
        Me.GroupBox1.Controls.Add(Me.dtpUtworzoneDo)
        Me.GroupBox1.Controls.Add(Me.dtpUtworzoneOd)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.dtpPlanowanaDo)
        Me.GroupBox1.Controls.Add(Me.dtpPlanowanaOd)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.chkListStatus)
        Me.GroupBox1.Controls.Add(Me.txtQGUAR_DOSTAWA)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtQGUAR_ZA)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtNrPO)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtNrAwiza)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(147, 517)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Wyszukaj według:"
        '
        'txtSKU
        '
        Me.txtSKU.Location = New System.Drawing.Point(6, 149)
        Me.txtSKU.Name = "txtSKU"
        Me.txtSKU.Size = New System.Drawing.Size(135, 20)
        Me.txtSKU.TabIndex = 7
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(6, 133)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(32, 13)
        Me.Label13.TabIndex = 6
        Me.Label13.Text = "SKU:"
        '
        'btnWyczyscFiltry
        '
        Me.btnWyczyscFiltry.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnWyczyscFiltry.ForeColor = System.Drawing.Color.White
        Me.btnWyczyscFiltry.Location = New System.Drawing.Point(75, 481)
        Me.btnWyczyscFiltry.Name = "btnWyczyscFiltry"
        Me.btnWyczyscFiltry.Size = New System.Drawing.Size(65, 29)
        Me.btnWyczyscFiltry.TabIndex = 25
        Me.btnWyczyscFiltry.Text = "Wyczyść"
        Me.ToolTip1.SetToolTip(Me.btnWyczyscFiltry, "Czyści kryteria filtru")
        Me.btnWyczyscFiltry.UseVisualStyleBackColor = False
        '
        'txtDostawca
        '
        Me.txtDostawca.Location = New System.Drawing.Point(6, 110)
        Me.txtDostawca.Name = "txtDostawca"
        Me.txtDostawca.Size = New System.Drawing.Size(135, 20)
        Me.txtDostawca.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.txtDostawca, "Ustawia filtr na nazwę dostawcy")
        '
        'lblDostawca
        '
        Me.lblDostawca.AutoSize = True
        Me.lblDostawca.Location = New System.Drawing.Point(6, 94)
        Me.lblDostawca.Name = "lblDostawca"
        Me.lblDostawca.Size = New System.Drawing.Size(91, 13)
        Me.lblDostawca.TabIndex = 4
        Me.lblDostawca.Text = "Nazwa dostawcy:"
        Me.ToolTip1.SetToolTip(Me.lblDostawca, "Ustawia filtr na nazwę dostawcy")
        '
        'btnFiltruj
        '
        Me.btnFiltruj.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnFiltruj.ForeColor = System.Drawing.Color.White
        Me.btnFiltruj.Location = New System.Drawing.Point(6, 481)
        Me.btnFiltruj.Name = "btnFiltruj"
        Me.btnFiltruj.Size = New System.Drawing.Size(65, 29)
        Me.btnFiltruj.TabIndex = 24
        Me.btnFiltruj.Text = "Filtruj"
        Me.ToolTip1.SetToolTip(Me.btnFiltruj, "Zwraca listę awiz spełniających warunki filtrowania")
        Me.btnFiltruj.UseVisualStyleBackColor = False
        '
        'dtpUtworzoneDo
        '
        Me.dtpUtworzoneDo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpUtworzoneDo.Location = New System.Drawing.Point(29, 455)
        Me.dtpUtworzoneDo.Name = "dtpUtworzoneDo"
        Me.dtpUtworzoneDo.Size = New System.Drawing.Size(111, 20)
        Me.dtpUtworzoneDo.TabIndex = 22
        Me.ToolTip1.SetToolTip(Me.dtpUtworzoneDo, "Ustawia filtr na wybrany zakres dat utworzenia awiza")
        '
        'dtpUtworzoneOd
        '
        Me.dtpUtworzoneOd.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpUtworzoneOd.Location = New System.Drawing.Point(29, 429)
        Me.dtpUtworzoneOd.Name = "dtpUtworzoneOd"
        Me.dtpUtworzoneOd.Size = New System.Drawing.Size(111, 20)
        Me.dtpUtworzoneOd.TabIndex = 20
        Me.ToolTip1.SetToolTip(Me.dtpUtworzoneOd, "Ustawia filtr na wybrany zakres dat utworzenia awiza")
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(3, 461)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(24, 13)
        Me.Label10.TabIndex = 23
        Me.Label10.Text = "Do:"
        Me.ToolTip1.SetToolTip(Me.Label10, "Ustawia filtr na wybrany zakres dat utworzenia awiza")
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(2, 435)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(24, 13)
        Me.Label11.TabIndex = 21
        Me.Label11.Text = "Od:"
        Me.ToolTip1.SetToolTip(Me.Label11, "Ustawia filtr na wybrany zakres dat utworzenia awiza")
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(2, 413)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(117, 13)
        Me.Label12.TabIndex = 19
        Me.Label12.Text = "Data utworzenia awiza:"
        Me.ToolTip1.SetToolTip(Me.Label12, "Ustawia filtr na wybrany zakres dat utworzenia awiza")
        '
        'dtpPlanowanaDo
        '
        Me.dtpPlanowanaDo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpPlanowanaDo.Location = New System.Drawing.Point(29, 390)
        Me.dtpPlanowanaDo.Name = "dtpPlanowanaDo"
        Me.dtpPlanowanaDo.Size = New System.Drawing.Size(111, 20)
        Me.dtpPlanowanaDo.TabIndex = 18
        Me.ToolTip1.SetToolTip(Me.dtpPlanowanaDo, "Ustawia filtr na wybrany zakres dat planowanej dostawy")
        '
        'dtpPlanowanaOd
        '
        Me.dtpPlanowanaOd.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpPlanowanaOd.Location = New System.Drawing.Point(29, 364)
        Me.dtpPlanowanaOd.Name = "dtpPlanowanaOd"
        Me.dtpPlanowanaOd.Size = New System.Drawing.Size(111, 20)
        Me.dtpPlanowanaOd.TabIndex = 16
        Me.ToolTip1.SetToolTip(Me.dtpPlanowanaOd, "Ustawia filtr na wybrany zakres dat planowanej dostawy")
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(3, 396)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(24, 13)
        Me.Label9.TabIndex = 17
        Me.Label9.Text = "Do:"
        Me.ToolTip1.SetToolTip(Me.Label9, "Ustawia filtr na wybrany zakres dat planowanej dostawy")
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(2, 370)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(24, 13)
        Me.Label8.TabIndex = 15
        Me.Label8.Text = "Od:"
        Me.ToolTip1.SetToolTip(Me.Label8, "Ustawia filtr na wybrany zakres dat planowanej dostawy")
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(2, 348)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(129, 13)
        Me.Label7.TabIndex = 14
        Me.Label7.Text = "Planowana data dostawy:"
        Me.ToolTip1.SetToolTip(Me.Label7, "Ustawia filtr na wybrany zakres dat planowanej dostawy")
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(3, 250)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(70, 13)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "Status awiza:"
        Me.ToolTip1.SetToolTip(Me.Label6, "Ustawia filtr na wybrane statusy awiza")
        '
        'chkListStatus
        '
        Me.chkListStatus.CheckOnClick = True
        Me.chkListStatus.FormattingEnabled = True
        Me.chkListStatus.Location = New System.Drawing.Point(5, 266)
        Me.chkListStatus.Name = "chkListStatus"
        Me.chkListStatus.Size = New System.Drawing.Size(135, 79)
        Me.chkListStatus.TabIndex = 13
        Me.ToolTip1.SetToolTip(Me.chkListStatus, "Ustawia filtr na wybrane statusy awiza")
        '
        'txtQGUAR_DOSTAWA
        '
        Me.txtQGUAR_DOSTAWA.Location = New System.Drawing.Point(5, 227)
        Me.txtQGUAR_DOSTAWA.Name = "txtQGUAR_DOSTAWA"
        Me.txtQGUAR_DOSTAWA.Size = New System.Drawing.Size(135, 20)
        Me.txtQGUAR_DOSTAWA.TabIndex = 11
        Me.ToolTip1.SetToolTip(Me.txtQGUAR_DOSTAWA, "Ustawia filtr na numer dostawy")
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 211)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(110, 13)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "QGUAR_DOSTAWA:"
        Me.ToolTip1.SetToolTip(Me.Label4, "Ustawia filtr na numer dostawy")
        '
        'txtQGUAR_ZA
        '
        Me.txtQGUAR_ZA.Location = New System.Drawing.Point(6, 188)
        Me.txtQGUAR_ZA.Name = "txtQGUAR_ZA"
        Me.txtQGUAR_ZA.Size = New System.Drawing.Size(135, 20)
        Me.txtQGUAR_ZA.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.txtQGUAR_ZA, "Ustawia filtr na numer dokumentu ZA")
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 172)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(69, 13)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "QGUAR_ZA:"
        Me.ToolTip1.SetToolTip(Me.Label5, "Ustawia filtr na numer dokumentu ZA")
        '
        'txtNrPO
        '
        Me.txtNrPO.Location = New System.Drawing.Point(6, 71)
        Me.txtNrPO.Name = "txtNrPO"
        Me.txtNrPO.Size = New System.Drawing.Size(135, 20)
        Me.txtNrPO.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.txtNrPO, "Ustawia filtr na numer PO (Purchase Order)")
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 55)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(39, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Nr PO:"
        Me.ToolTip1.SetToolTip(Me.Label3, "Ustawia filtr na numer PO (Purchase Order)")
        '
        'txtNrAwiza
        '
        Me.txtNrAwiza.Location = New System.Drawing.Point(6, 32)
        Me.txtNrAwiza.Name = "txtNrAwiza"
        Me.txtNrAwiza.Size = New System.Drawing.Size(135, 20)
        Me.txtNrAwiza.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.txtNrAwiza, "Ustawia filtr na numer awiza")
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Nr awiza:"
        Me.ToolTip1.SetToolTip(Me.Label2, "Ustawia filtr na numer awiza")
        '
        'tsNawigator
        '
        Me.tsNawigator.BackColor = System.Drawing.Color.White
        Me.tsNawigator.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.tsNawigator.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnPoczatek, Me.btnPoprzedni, Me.ToolStripSeparator1, Me.lblEkran, Me.txtNumerEkranu, Me.lblIloscEkranow, Me.ToolStripSeparator2, Me.btnNastepny, Me.btnOstatni, Me.ToolStripSeparator3, Me.btnZamknij, Me.ToolStripSeparator4, Me.btnOdswiez, Me.lblWyswietlajPo, Me.cmbIloscNaStronie, Me.lblWierszyNaStronie, Me.ToolStripSeparator5, Me.tsKolumny})
        Me.tsNawigator.Location = New System.Drawing.Point(0, 523)
        Me.tsNawigator.Name = "tsNawigator"
        Me.tsNawigator.Size = New System.Drawing.Size(808, 25)
        Me.tsNawigator.TabIndex = 3
        Me.tsNawigator.Text = "Wyświetlaj po"
        '
        'btnPoczatek
        '
        Me.btnPoczatek.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnPoczatek.Image = CType(resources.GetObject("btnPoczatek.Image"), System.Drawing.Image)
        Me.btnPoczatek.Name = "btnPoczatek"
        Me.btnPoczatek.RightToLeftAutoMirrorImage = True
        Me.btnPoczatek.Size = New System.Drawing.Size(23, 22)
        Me.btnPoczatek.Text = "Przejdź do pierwszego ekranu"
        '
        'btnPoprzedni
        '
        Me.btnPoprzedni.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnPoprzedni.Image = CType(resources.GetObject("btnPoprzedni.Image"), System.Drawing.Image)
        Me.btnPoprzedni.Name = "btnPoprzedni"
        Me.btnPoprzedni.RightToLeftAutoMirrorImage = True
        Me.btnPoprzedni.Size = New System.Drawing.Size(23, 22)
        Me.btnPoprzedni.Text = "Przejdź do poprzedniego ekranu"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'lblEkran
        '
        Me.lblEkran.BackColor = System.Drawing.Color.DodgerBlue
        Me.lblEkran.ForeColor = System.Drawing.Color.Black
        Me.lblEkran.Name = "lblEkran"
        Me.lblEkran.Size = New System.Drawing.Size(36, 22)
        Me.lblEkran.Text = "Ekran"
        '
        'txtNumerEkranu
        '
        Me.txtNumerEkranu.AccessibleName = "Position"
        Me.txtNumerEkranu.AutoSize = False
        Me.txtNumerEkranu.ForeColor = System.Drawing.Color.Black
        Me.txtNumerEkranu.Name = "txtNumerEkranu"
        Me.txtNumerEkranu.Size = New System.Drawing.Size(30, 21)
        Me.txtNumerEkranu.ToolTipText = "Bieżący ekran"
        '
        'lblIloscEkranow
        '
        Me.lblIloscEkranow.BackColor = System.Drawing.Color.DodgerBlue
        Me.lblIloscEkranow.ForeColor = System.Drawing.Color.Black
        Me.lblIloscEkranow.Name = "lblIloscEkranow"
        Me.lblIloscEkranow.Size = New System.Drawing.Size(22, 22)
        Me.lblIloscEkranow.Text = "z X"
        Me.lblIloscEkranow.ToolTipText = "Całkowita ilość ekranów przybieżącym filtrze"
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
        Me.btnNastepny.Text = "Przejdź do następnego ekranu"
        '
        'btnOstatni
        '
        Me.btnOstatni.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnOstatni.Image = CType(resources.GetObject("btnOstatni.Image"), System.Drawing.Image)
        Me.btnOstatni.Name = "btnOstatni"
        Me.btnOstatni.RightToLeftAutoMirrorImage = True
        Me.btnOstatni.Size = New System.Drawing.Size(23, 22)
        Me.btnOstatni.Text = "Przejdź do ostatniego ekranu"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'btnZamknij
        '
        Me.btnZamknij.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.btnZamknij.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnZamknij.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnZamknij.ForeColor = System.Drawing.Color.White
        Me.btnZamknij.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnZamknij.Name = "btnZamknij"
        Me.btnZamknij.Size = New System.Drawing.Size(54, 22)
        Me.btnZamknij.Text = "Zamknij"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 25)
        '
        'btnOdswiez
        '
        Me.btnOdswiez.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.btnOdswiez.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnOdswiez.ForeColor = System.Drawing.Color.White
        Me.btnOdswiez.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.btnOdswiez.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnOdswiez.Name = "btnOdswiez"
        Me.btnOdswiez.Size = New System.Drawing.Size(55, 22)
        Me.btnOdswiez.Text = "&Odswież"
        '
        'lblWyswietlajPo
        '
        Me.lblWyswietlajPo.BackColor = System.Drawing.Color.DodgerBlue
        Me.lblWyswietlajPo.ForeColor = System.Drawing.Color.Black
        Me.lblWyswietlajPo.Name = "lblWyswietlajPo"
        Me.lblWyswietlajPo.Size = New System.Drawing.Size(80, 22)
        Me.lblWyswietlajPo.Text = "Wyświetlaj po"
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
        Me.lblWierszyNaStronie.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblWierszyNaStronie.ForeColor = System.Drawing.Color.Black
        Me.lblWierszyNaStronie.Name = "lblWierszyNaStronie"
        Me.lblWierszyNaStronie.Size = New System.Drawing.Size(102, 22)
        Me.lblWierszyNaStronie.Text = "wierszy na ekranie"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(6, 25)
        '
        'tsKolumny
        '
        Me.tsKolumny.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.tsKolumny.BackColor = System.Drawing.Color.DodgerBlue
        Me.tsKolumny.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tsKolumny.ForeColor = System.Drawing.Color.White
        Me.tsKolumny.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsKolumny.Name = "tsKolumny"
        Me.tsKolumny.Size = New System.Drawing.Size(68, 22)
        Me.tsKolumny.Text = "Kolumny"
        Me.tsKolumny.Visible = False
        '
        'btnAnuluj
        '
        Me.btnAnuluj.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnAnuluj.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnAnuluj.ForeColor = System.Drawing.Color.White
        Me.btnAnuluj.Location = New System.Drawing.Point(739, 531)
        Me.btnAnuluj.Name = "btnAnuluj"
        Me.btnAnuluj.Size = New System.Drawing.Size(75, 22)
        Me.btnAnuluj.TabIndex = 4
        Me.btnAnuluj.Text = "Anuluj"
        Me.btnAnuluj.UseVisualStyleBackColor = False
        '
        'ToolTip1
        '
        Me.ToolTip1.AutoPopDelay = 10000
        Me.ToolTip1.InitialDelay = 500
        Me.ToolTip1.ReshowDelay = 500
        '
        'menu_kontekstowe
        '
        Me.menu_kontekstowe.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EdytujToolStripMenuItem, Me.PokazSzczegolyToolStripMenuItem, Me.AnulujAwizoToolStripMenuItem})
        Me.menu_kontekstowe.Name = "menu_kontekstowe"
        Me.menu_kontekstowe.Size = New System.Drawing.Size(159, 70)
        '
        'EdytujToolStripMenuItem
        '
        Me.EdytujToolStripMenuItem.BackColor = System.Drawing.Color.White
        Me.EdytujToolStripMenuItem.Name = "EdytujToolStripMenuItem"
        Me.EdytujToolStripMenuItem.Size = New System.Drawing.Size(158, 22)
        Me.EdytujToolStripMenuItem.Text = "Edytuj"
        '
        'PokazSzczegolyToolStripMenuItem
        '
        Me.PokazSzczegolyToolStripMenuItem.BackColor = System.Drawing.Color.White
        Me.PokazSzczegolyToolStripMenuItem.Name = "PokazSzczegolyToolStripMenuItem"
        Me.PokazSzczegolyToolStripMenuItem.Size = New System.Drawing.Size(158, 22)
        Me.PokazSzczegolyToolStripMenuItem.Text = "Pokaż szczegóły"
        '
        'AnulujAwizoToolStripMenuItem
        '
        Me.AnulujAwizoToolStripMenuItem.BackColor = System.Drawing.Color.White
        Me.AnulujAwizoToolStripMenuItem.Name = "AnulujAwizoToolStripMenuItem"
        Me.AnulujAwizoToolStripMenuItem.Size = New System.Drawing.Size(158, 22)
        Me.AnulujAwizoToolStripMenuItem.Text = "Anuluj awizo"
        '
        'frmZarzadzanieAwizami
        '
        Me.AcceptButton = Me.btnFiltruj
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnAnuluj
        Me.ClientSize = New System.Drawing.Size(808, 548)
        Me.Controls.Add(Me.tsNawigator)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.dgv)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnAnuluj)
        Me.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.MinimumSize = New System.Drawing.Size(497, 538)
        Me.Name = "frmZarzadzanieAwizami"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Zarządzanie Awizami"
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.tsNawigator.ResumeLayout(False)
        Me.tsNawigator.PerformLayout()
        Me.menu_kontekstowe.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dgv As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtQGUAR_DOSTAWA As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtQGUAR_ZA As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtNrPO As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtNrAwiza As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtpUtworzoneDo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpUtworzoneOd As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents dtpPlanowanaDo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpPlanowanaOd As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents chkListStatus As System.Windows.Forms.CheckedListBox
    Friend WithEvents tsNawigator As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents lblEkran As System.Windows.Forms.ToolStripLabel
    Friend WithEvents txtNumerEkranu As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents lblIloscEkranow As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnOdswiez As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblWyswietlajPo As System.Windows.Forms.ToolStripLabel
    Friend WithEvents cmbIloscNaStronie As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents lblWierszyNaStronie As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnZamknij As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsKolumny As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents btnFiltruj As System.Windows.Forms.Button
    Friend WithEvents btnPoczatek As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnPoprzedni As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnNastepny As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnOstatni As System.Windows.Forms.ToolStripButton
    Friend WithEvents txtDostawca As System.Windows.Forms.TextBox
    Friend WithEvents lblDostawca As System.Windows.Forms.Label
    Friend WithEvents btnAnuluj As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents btnWyczyscFiltry As System.Windows.Forms.Button
    Friend WithEvents menu_kontekstowe As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents EdytujToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PokazSzczegolyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AnulujAwizoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents txtSKU As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label

End Class
