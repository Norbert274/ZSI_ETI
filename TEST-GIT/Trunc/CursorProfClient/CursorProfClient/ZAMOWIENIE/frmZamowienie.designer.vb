<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmZamowienie
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmZamowienie))
        Me.btnZapiszZmiany = New System.Windows.Forms.Button()
        Me.btnZamknij = New System.Windows.Forms.Button()
        Me.sc = New System.Windows.Forms.SplitContainer()
        Me.dgv = New System.Windows.Forms.DataGridView()
        Me.btnZlozZamowienie = New System.Windows.Forms.Button()
        Me.gbDaneKontaktowe = New System.Windows.Forms.GroupBox()
        Me.lblUwagiPozostaloZnakow = New System.Windows.Forms.Label()
        Me.txtUwagi = New System.Windows.Forms.TextBox()
        Me.lblUwagi = New System.Windows.Forms.Label()
        Me.txtTelefonKontaktowy = New System.Windows.Forms.TextBox()
        Me.lblTelefonKontaktowy = New System.Windows.Forms.Label()
        Me.lblOsobaKontaktowa = New System.Windows.Forms.Label()
        Me.txtOsobaKontaktowa = New System.Windows.Forms.TextBox()
        Me.gbOpcjeDostawy = New System.Windows.Forms.GroupBox()
        Me.txtKodPocztowy = New System.Windows.Forms.MaskedTextBox()
        Me.sbSzukajAdresu = New System.Windows.Forms.Button()
        Me.btnDaneDpd = New System.Windows.Forms.Button()
        Me.btnOdbiorcy = New System.Windows.Forms.Button()
        Me.lblOdbiorcy = New System.Windows.Forms.Label()
        Me.rbZamowienieGrupowe = New System.Windows.Forms.RadioButton()
        Me.cmbMagazynOdbiorWlasnyDPD = New System.Windows.Forms.ComboBox()
        Me.rbOdbiorWlasnyDPD = New System.Windows.Forms.RadioButton()
        Me.dtpDataRealizacji = New System.Windows.Forms.DateTimePicker()
        Me.LabelDataRealizacji = New System.Windows.Forms.Label()
        Me.cmbMagazynOdbiorWlasny = New System.Windows.Forms.ComboBox()
        Me.lblNazwa = New System.Windows.Forms.Label()
        Me.txtNazwa = New System.Windows.Forms.TextBox()
        Me.lblAdres = New System.Windows.Forms.Label()
        Me.txtAdres = New System.Windows.Forms.TextBox()
        Me.txtMiasto = New System.Windows.Forms.TextBox()
        Me.lblMiasto = New System.Windows.Forms.Label()
        Me.lblKod = New System.Windows.Forms.Label()
        Me.rbDostawaNaAdres = New System.Windows.Forms.RadioButton()
        Me.cmbDostawaNaZdefiniowanyAdres = New System.Windows.Forms.ComboBox()
        Me.rbDostawaNaZdefiniowanyAdres = New System.Windows.Forms.RadioButton()
        Me.rbOdbiorWlasny = New System.Windows.Forms.RadioButton()
        Me.tsPozycje = New System.Windows.Forms.ToolStrip()
        Me.btnDodajPozycje = New System.Windows.Forms.ToolStripButton()
        Me.btnUsunPozycje = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.lblLimitOpis = New System.Windows.Forms.ToolStripLabel()
        Me.lblLimit = New System.Windows.Forms.ToolStripLabel()
        Me.lblLimitJM = New System.Windows.Forms.ToolStripLabel()
        Me.lblWartoscOpis = New System.Windows.Forms.ToolStripLabel()
        Me.lblWartosc = New System.Windows.Forms.ToolStripLabel()
        Me.LblWartoscJM = New System.Windows.Forms.ToolStripLabel()
        Me.lblStatusOpis = New System.Windows.Forms.ToolStripLabel()
        Me.lblStatusZamowienia = New System.Windows.Forms.ToolStripLabel()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        CType(Me.sc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sc.Panel1.SuspendLayout()
        Me.sc.Panel2.SuspendLayout()
        Me.sc.SuspendLayout()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbDaneKontaktowe.SuspendLayout()
        Me.gbOpcjeDostawy.SuspendLayout()
        Me.tsPozycje.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnZapiszZmiany
        '
        Me.btnZapiszZmiany.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnZapiszZmiany.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnZapiszZmiany.ForeColor = System.Drawing.Color.White
        Me.btnZapiszZmiany.Location = New System.Drawing.Point(675, 248)
        Me.btnZapiszZmiany.Name = "btnZapiszZmiany"
        Me.btnZapiszZmiany.Size = New System.Drawing.Size(100, 23)
        Me.btnZapiszZmiany.TabIndex = 2
        Me.btnZapiszZmiany.Text = "Zapisz zmiany"
        Me.btnZapiszZmiany.UseVisualStyleBackColor = False
        '
        'btnZamknij
        '
        Me.btnZamknij.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnZamknij.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnZamknij.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnZamknij.ForeColor = System.Drawing.Color.White
        Me.btnZamknij.Location = New System.Drawing.Point(897, 248)
        Me.btnZamknij.Name = "btnZamknij"
        Me.btnZamknij.Size = New System.Drawing.Size(93, 23)
        Me.btnZamknij.TabIndex = 4
        Me.btnZamknij.Text = "Zamknij"
        Me.btnZamknij.UseVisualStyleBackColor = False
        '
        'sc
        '
        Me.sc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sc.IsSplitterFixed = True
        Me.sc.Location = New System.Drawing.Point(0, 25)
        Me.sc.Name = "sc"
        Me.sc.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'sc.Panel1
        '
        Me.sc.Panel1.Controls.Add(Me.dgv)
        Me.sc.Panel1MinSize = 200
        '
        'sc.Panel2
        '
        Me.sc.Panel2.BackColor = System.Drawing.Color.White
        Me.sc.Panel2.Controls.Add(Me.btnZamknij)
        Me.sc.Panel2.Controls.Add(Me.btnZlozZamowienie)
        Me.sc.Panel2.Controls.Add(Me.gbDaneKontaktowe)
        Me.sc.Panel2.Controls.Add(Me.gbOpcjeDostawy)
        Me.sc.Panel2.Controls.Add(Me.btnZapiszZmiany)
        Me.sc.Panel2MinSize = 275
        Me.sc.Size = New System.Drawing.Size(1002, 487)
        Me.sc.SplitterDistance = 200
        Me.sc.TabIndex = 14
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
        Me.dgv.Location = New System.Drawing.Point(0, 0)
        Me.dgv.Name = "dgv"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.dgv.RowHeadersVisible = False
        Me.dgv.Size = New System.Drawing.Size(1002, 200)
        Me.dgv.TabIndex = 0
        '
        'btnZlozZamowienie
        '
        Me.btnZlozZamowienie.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnZlozZamowienie.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnZlozZamowienie.ForeColor = System.Drawing.Color.White
        Me.btnZlozZamowienie.Location = New System.Drawing.Point(781, 248)
        Me.btnZlozZamowienie.Name = "btnZlozZamowienie"
        Me.btnZlozZamowienie.Size = New System.Drawing.Size(110, 23)
        Me.btnZlozZamowienie.TabIndex = 3
        Me.btnZlozZamowienie.Text = "Z³ó¿ zamówienie"
        Me.btnZlozZamowienie.UseVisualStyleBackColor = False
        '
        'gbDaneKontaktowe
        '
        Me.gbDaneKontaktowe.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbDaneKontaktowe.Controls.Add(Me.lblUwagiPozostaloZnakow)
        Me.gbDaneKontaktowe.Controls.Add(Me.txtUwagi)
        Me.gbDaneKontaktowe.Controls.Add(Me.lblUwagi)
        Me.gbDaneKontaktowe.Controls.Add(Me.txtTelefonKontaktowy)
        Me.gbDaneKontaktowe.Controls.Add(Me.lblTelefonKontaktowy)
        Me.gbDaneKontaktowe.Controls.Add(Me.lblOsobaKontaktowa)
        Me.gbDaneKontaktowe.Controls.Add(Me.txtOsobaKontaktowa)
        Me.gbDaneKontaktowe.ForeColor = System.Drawing.Color.Black
        Me.gbDaneKontaktowe.Location = New System.Drawing.Point(593, 3)
        Me.gbDaneKontaktowe.Name = "gbDaneKontaktowe"
        Me.gbDaneKontaktowe.Size = New System.Drawing.Size(406, 239)
        Me.gbDaneKontaktowe.TabIndex = 1
        Me.gbDaneKontaktowe.TabStop = False
        Me.gbDaneKontaktowe.Text = "Dane kontaktowe"
        '
        'lblUwagiPozostaloZnakow
        '
        Me.lblUwagiPozostaloZnakow.AutoSize = True
        Me.lblUwagiPozostaloZnakow.Location = New System.Drawing.Point(112, 210)
        Me.lblUwagiPozostaloZnakow.Name = "lblUwagiPozostaloZnakow"
        Me.lblUwagiPozostaloZnakow.Size = New System.Drawing.Size(0, 13)
        Me.lblUwagiPozostaloZnakow.TabIndex = 6
        '
        'txtUwagi
        '
        Me.txtUwagi.Location = New System.Drawing.Point(112, 71)
        Me.txtUwagi.Multiline = True
        Me.txtUwagi.Name = "txtUwagi"
        Me.txtUwagi.Size = New System.Drawing.Size(285, 133)
        Me.txtUwagi.TabIndex = 5
        '
        'lblUwagi
        '
        Me.lblUwagi.AutoSize = True
        Me.lblUwagi.ForeColor = System.Drawing.Color.Black
        Me.lblUwagi.Location = New System.Drawing.Point(6, 74)
        Me.lblUwagi.Name = "lblUwagi"
        Me.lblUwagi.Size = New System.Drawing.Size(40, 13)
        Me.lblUwagi.TabIndex = 4
        Me.lblUwagi.Text = "Uwagi:"
        '
        'txtTelefonKontaktowy
        '
        Me.txtTelefonKontaktowy.Location = New System.Drawing.Point(112, 45)
        Me.txtTelefonKontaktowy.Name = "txtTelefonKontaktowy"
        Me.txtTelefonKontaktowy.Size = New System.Drawing.Size(285, 20)
        Me.txtTelefonKontaktowy.TabIndex = 3
        '
        'lblTelefonKontaktowy
        '
        Me.lblTelefonKontaktowy.AutoSize = True
        Me.lblTelefonKontaktowy.ForeColor = System.Drawing.Color.Black
        Me.lblTelefonKontaktowy.Location = New System.Drawing.Point(6, 47)
        Me.lblTelefonKontaktowy.Name = "lblTelefonKontaktowy"
        Me.lblTelefonKontaktowy.Size = New System.Drawing.Size(86, 13)
        Me.lblTelefonKontaktowy.TabIndex = 2
        Me.lblTelefonKontaktowy.Text = "Tel. kontaktowy:"
        '
        'lblOsobaKontaktowa
        '
        Me.lblOsobaKontaktowa.AutoSize = True
        Me.lblOsobaKontaktowa.ForeColor = System.Drawing.Color.Black
        Me.lblOsobaKontaktowa.Location = New System.Drawing.Point(6, 21)
        Me.lblOsobaKontaktowa.Name = "lblOsobaKontaktowa"
        Me.lblOsobaKontaktowa.Size = New System.Drawing.Size(100, 13)
        Me.lblOsobaKontaktowa.TabIndex = 0
        Me.lblOsobaKontaktowa.Text = "Osoba kontaktowa:"
        '
        'txtOsobaKontaktowa
        '
        Me.txtOsobaKontaktowa.Location = New System.Drawing.Point(112, 18)
        Me.txtOsobaKontaktowa.Name = "txtOsobaKontaktowa"
        Me.txtOsobaKontaktowa.Size = New System.Drawing.Size(285, 20)
        Me.txtOsobaKontaktowa.TabIndex = 1
        '
        'gbOpcjeDostawy
        '
        Me.gbOpcjeDostawy.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbOpcjeDostawy.Controls.Add(Me.txtKodPocztowy)
        Me.gbOpcjeDostawy.Controls.Add(Me.sbSzukajAdresu)
        Me.gbOpcjeDostawy.Controls.Add(Me.btnDaneDpd)
        Me.gbOpcjeDostawy.Controls.Add(Me.btnOdbiorcy)
        Me.gbOpcjeDostawy.Controls.Add(Me.lblOdbiorcy)
        Me.gbOpcjeDostawy.Controls.Add(Me.rbZamowienieGrupowe)
        Me.gbOpcjeDostawy.Controls.Add(Me.cmbMagazynOdbiorWlasnyDPD)
        Me.gbOpcjeDostawy.Controls.Add(Me.rbOdbiorWlasnyDPD)
        Me.gbOpcjeDostawy.Controls.Add(Me.dtpDataRealizacji)
        Me.gbOpcjeDostawy.Controls.Add(Me.LabelDataRealizacji)
        Me.gbOpcjeDostawy.Controls.Add(Me.cmbMagazynOdbiorWlasny)
        Me.gbOpcjeDostawy.Controls.Add(Me.lblNazwa)
        Me.gbOpcjeDostawy.Controls.Add(Me.txtNazwa)
        Me.gbOpcjeDostawy.Controls.Add(Me.lblAdres)
        Me.gbOpcjeDostawy.Controls.Add(Me.txtAdres)
        Me.gbOpcjeDostawy.Controls.Add(Me.txtMiasto)
        Me.gbOpcjeDostawy.Controls.Add(Me.lblMiasto)
        Me.gbOpcjeDostawy.Controls.Add(Me.lblKod)
        Me.gbOpcjeDostawy.Controls.Add(Me.rbDostawaNaAdres)
        Me.gbOpcjeDostawy.Controls.Add(Me.cmbDostawaNaZdefiniowanyAdres)
        Me.gbOpcjeDostawy.Controls.Add(Me.rbDostawaNaZdefiniowanyAdres)
        Me.gbOpcjeDostawy.Controls.Add(Me.rbOdbiorWlasny)
        Me.gbOpcjeDostawy.ForeColor = System.Drawing.Color.Black
        Me.gbOpcjeDostawy.Location = New System.Drawing.Point(0, 3)
        Me.gbOpcjeDostawy.Name = "gbOpcjeDostawy"
        Me.gbOpcjeDostawy.Size = New System.Drawing.Size(584, 239)
        Me.gbOpcjeDostawy.TabIndex = 0
        Me.gbOpcjeDostawy.TabStop = False
        Me.gbOpcjeDostawy.Text = "Opcje dostawy"
        '
        'txtKodPocztowy
        '
        Me.txtKodPocztowy.Location = New System.Drawing.Point(53, 155)
        Me.txtKodPocztowy.Mask = "00-000"
        Me.txtKodPocztowy.Name = "txtKodPocztowy"
        Me.txtKodPocztowy.Size = New System.Drawing.Size(43, 20)
        Me.txtKodPocztowy.TabIndex = 21
        '
        'sbSzukajAdresu
        '
        Me.sbSzukajAdresu.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.sbSzukajAdresu.BackColor = System.Drawing.Color.DodgerBlue
        Me.sbSzukajAdresu.ForeColor = System.Drawing.Color.White
        Me.sbSzukajAdresu.Location = New System.Drawing.Point(496, 73)
        Me.sbSzukajAdresu.Name = "sbSzukajAdresu"
        Me.sbSzukajAdresu.Size = New System.Drawing.Size(81, 23)
        Me.sbSzukajAdresu.TabIndex = 20
        Me.sbSzukajAdresu.Text = "Szukaj ..."
        Me.ToolTip.SetToolTip(Me.sbSzukajAdresu, "Umo¿liwia wyszukiwanie adresu w wygodnym formularzu")
        Me.sbSzukajAdresu.UseVisualStyleBackColor = False
        '
        'btnDaneDpd
        '
        Me.btnDaneDpd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDaneDpd.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnDaneDpd.ForeColor = System.Drawing.Color.White
        Me.btnDaneDpd.Location = New System.Drawing.Point(12, 209)
        Me.btnDaneDpd.Name = "btnDaneDpd"
        Me.btnDaneDpd.Size = New System.Drawing.Size(110, 23)
        Me.btnDaneDpd.TabIndex = 17
        Me.btnDaneDpd.Text = "Dane dostawy"
        Me.ToolTip.SetToolTip(Me.btnDaneDpd, "Dodatkowe dane dostawy.")
        Me.btnDaneDpd.UseVisualStyleBackColor = False
        Me.btnDaneDpd.Visible = False
        '
        'btnOdbiorcy
        '
        Me.btnOdbiorcy.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnOdbiorcy.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnOdbiorcy.Enabled = False
        Me.btnOdbiorcy.ForeColor = System.Drawing.Color.White
        Me.btnOdbiorcy.Location = New System.Drawing.Point(276, 185)
        Me.btnOdbiorcy.Name = "btnOdbiorcy"
        Me.btnOdbiorcy.Size = New System.Drawing.Size(81, 23)
        Me.btnOdbiorcy.TabIndex = 16
        Me.btnOdbiorcy.Text = "Odbiorcy"
        Me.ToolTip.SetToolTip(Me.btnOdbiorcy, "Umo¿liwia z³o¿enie kilku takich samych zamówieñ do ró¿nych odbiorców")
        Me.btnOdbiorcy.UseVisualStyleBackColor = False
        '
        'lblOdbiorcy
        '
        Me.lblOdbiorcy.AutoSize = True
        Me.lblOdbiorcy.Location = New System.Drawing.Point(139, 189)
        Me.lblOdbiorcy.Name = "lblOdbiorcy"
        Me.lblOdbiorcy.Size = New System.Drawing.Size(118, 13)
        Me.lblOdbiorcy.TabIndex = 15
        Me.lblOdbiorcy.Text = "Nie wybrano odbiorców"
        Me.ToolTip.SetToolTip(Me.lblOdbiorcy, "Umo¿liwia z³o¿enie kilku takich samych zamówieñ do ró¿nych odbiorców")
        '
        'rbZamowienieGrupowe
        '
        Me.rbZamowienieGrupowe.AutoSize = True
        Me.rbZamowienieGrupowe.Enabled = False
        Me.rbZamowienieGrupowe.Location = New System.Drawing.Point(11, 188)
        Me.rbZamowienieGrupowe.Name = "rbZamowienieGrupowe"
        Me.rbZamowienieGrupowe.Size = New System.Drawing.Size(129, 17)
        Me.rbZamowienieGrupowe.TabIndex = 14
        Me.rbZamowienieGrupowe.TabStop = True
        Me.rbZamowienieGrupowe.Text = "Zamówienie grupowe:"
        Me.ToolTip.SetToolTip(Me.rbZamowienieGrupowe, "Umo¿liwia z³o¿enie kilku takich samych zamówieñ do ró¿nych odbiorców")
        Me.rbZamowienieGrupowe.UseVisualStyleBackColor = True
        '
        'cmbMagazynOdbiorWlasnyDPD
        '
        Me.cmbMagazynOdbiorWlasnyDPD.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbMagazynOdbiorWlasnyDPD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMagazynOdbiorWlasnyDPD.DropDownWidth = 400
        Me.cmbMagazynOdbiorWlasnyDPD.Enabled = False
        Me.cmbMagazynOdbiorWlasnyDPD.FormattingEnabled = True
        Me.cmbMagazynOdbiorWlasnyDPD.Location = New System.Drawing.Point(238, 47)
        Me.cmbMagazynOdbiorWlasnyDPD.Name = "cmbMagazynOdbiorWlasnyDPD"
        Me.cmbMagazynOdbiorWlasnyDPD.Size = New System.Drawing.Size(340, 21)
        Me.cmbMagazynOdbiorWlasnyDPD.TabIndex = 3
        '
        'rbOdbiorWlasnyDPD
        '
        Me.rbOdbiorWlasnyDPD.AutoSize = True
        Me.rbOdbiorWlasnyDPD.ForeColor = System.Drawing.Color.Black
        Me.rbOdbiorWlasnyDPD.Location = New System.Drawing.Point(9, 48)
        Me.rbOdbiorWlasnyDPD.Name = "rbOdbiorWlasnyDPD"
        Me.rbOdbiorWlasnyDPD.Size = New System.Drawing.Size(223, 17)
        Me.rbOdbiorWlasnyDPD.TabIndex = 2
        Me.rbOdbiorWlasnyDPD.TabStop = True
        Me.rbOdbiorWlasnyDPD.Text = "Odbiór w³asny w oddziale firmy kurierskiej:"
        Me.rbOdbiorWlasnyDPD.UseVisualStyleBackColor = True
        '
        'dtpDataRealizacji
        '
        Me.dtpDataRealizacji.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtpDataRealizacji.CustomFormat = "yyyy-MM-dd"
        Me.dtpDataRealizacji.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDataRealizacji.Location = New System.Drawing.Point(482, 210)
        Me.dtpDataRealizacji.Name = "dtpDataRealizacji"
        Me.dtpDataRealizacji.Size = New System.Drawing.Size(95, 20)
        Me.dtpDataRealizacji.TabIndex = 19
        Me.ToolTip.SetToolTip(Me.dtpDataRealizacji, "Przewidywana data dostawy")
        '
        'LabelDataRealizacji
        '
        Me.LabelDataRealizacji.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelDataRealizacji.AutoSize = True
        Me.LabelDataRealizacji.ForeColor = System.Drawing.Color.Black
        Me.LabelDataRealizacji.Location = New System.Drawing.Point(332, 213)
        Me.LabelDataRealizacji.Name = "LabelDataRealizacji"
        Me.LabelDataRealizacji.Size = New System.Drawing.Size(144, 13)
        Me.LabelDataRealizacji.TabIndex = 18
        Me.LabelDataRealizacji.Text = "Przewidywana data dostawy:"
        Me.ToolTip.SetToolTip(Me.LabelDataRealizacji, "Przewidywana data dostawy")
        '
        'cmbMagazynOdbiorWlasny
        '
        Me.cmbMagazynOdbiorWlasny.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbMagazynOdbiorWlasny.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMagazynOdbiorWlasny.Enabled = False
        Me.cmbMagazynOdbiorWlasny.FormattingEnabled = True
        Me.cmbMagazynOdbiorWlasny.Location = New System.Drawing.Point(238, 20)
        Me.cmbMagazynOdbiorWlasny.Name = "cmbMagazynOdbiorWlasny"
        Me.cmbMagazynOdbiorWlasny.Size = New System.Drawing.Size(340, 21)
        Me.cmbMagazynOdbiorWlasny.TabIndex = 1
        '
        'lblNazwa
        '
        Me.lblNazwa.AutoSize = True
        Me.lblNazwa.ForeColor = System.Drawing.Color.Black
        Me.lblNazwa.Location = New System.Drawing.Point(189, 109)
        Me.lblNazwa.Name = "lblNazwa"
        Me.lblNazwa.Size = New System.Drawing.Size(43, 13)
        Me.lblNazwa.TabIndex = 7
        Me.lblNazwa.Text = "Nazwa:"
        '
        'txtNazwa
        '
        Me.txtNazwa.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNazwa.Location = New System.Drawing.Point(238, 106)
        Me.txtNazwa.Name = "txtNazwa"
        Me.txtNazwa.Size = New System.Drawing.Size(340, 20)
        Me.txtNazwa.TabIndex = 7
        '
        'lblAdres
        '
        Me.lblAdres.AutoSize = True
        Me.lblAdres.ForeColor = System.Drawing.Color.Black
        Me.lblAdres.Location = New System.Drawing.Point(6, 132)
        Me.lblAdres.Name = "lblAdres"
        Me.lblAdres.Size = New System.Drawing.Size(37, 13)
        Me.lblAdres.TabIndex = 8
        Me.lblAdres.Text = "Adres:"
        '
        'txtAdres
        '
        Me.txtAdres.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtAdres.Location = New System.Drawing.Point(53, 129)
        Me.txtAdres.Name = "txtAdres"
        Me.txtAdres.Size = New System.Drawing.Size(525, 20)
        Me.txtAdres.TabIndex = 9
        '
        'txtMiasto
        '
        Me.txtMiasto.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtMiasto.Location = New System.Drawing.Point(142, 154)
        Me.txtMiasto.Name = "txtMiasto"
        Me.txtMiasto.Size = New System.Drawing.Size(436, 20)
        Me.txtMiasto.TabIndex = 13
        '
        'lblMiasto
        '
        Me.lblMiasto.AutoSize = True
        Me.lblMiasto.ForeColor = System.Drawing.Color.Black
        Me.lblMiasto.Location = New System.Drawing.Point(97, 157)
        Me.lblMiasto.Name = "lblMiasto"
        Me.lblMiasto.Size = New System.Drawing.Size(41, 13)
        Me.lblMiasto.TabIndex = 12
        Me.lblMiasto.Text = "Miasto:"
        '
        'lblKod
        '
        Me.lblKod.AutoSize = True
        Me.lblKod.ForeColor = System.Drawing.Color.Black
        Me.lblKod.Location = New System.Drawing.Point(6, 157)
        Me.lblKod.Name = "lblKod"
        Me.lblKod.Size = New System.Drawing.Size(29, 13)
        Me.lblKod.TabIndex = 10
        Me.lblKod.Text = "Kod:"
        '
        'rbDostawaNaAdres
        '
        Me.rbDostawaNaAdres.AutoSize = True
        Me.rbDostawaNaAdres.ForeColor = System.Drawing.Color.Black
        Me.rbDostawaNaAdres.Location = New System.Drawing.Point(9, 103)
        Me.rbDostawaNaAdres.Name = "rbDostawaNaAdres"
        Me.rbDostawaNaAdres.Size = New System.Drawing.Size(114, 17)
        Me.rbDostawaNaAdres.TabIndex = 6
        Me.rbDostawaNaAdres.TabStop = True
        Me.rbDostawaNaAdres.Text = "Dostawa na adres:"
        Me.rbDostawaNaAdres.UseVisualStyleBackColor = True
        '
        'cmbDostawaNaZdefiniowanyAdres
        '
        Me.cmbDostawaNaZdefiniowanyAdres.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbDostawaNaZdefiniowanyAdres.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDostawaNaZdefiniowanyAdres.DropDownWidth = 400
        Me.cmbDostawaNaZdefiniowanyAdres.Enabled = False
        Me.cmbDostawaNaZdefiniowanyAdres.FormattingEnabled = True
        Me.cmbDostawaNaZdefiniowanyAdres.Location = New System.Drawing.Point(238, 74)
        Me.cmbDostawaNaZdefiniowanyAdres.Name = "cmbDostawaNaZdefiniowanyAdres"
        Me.cmbDostawaNaZdefiniowanyAdres.Size = New System.Drawing.Size(255, 21)
        Me.cmbDostawaNaZdefiniowanyAdres.TabIndex = 5
        '
        'rbDostawaNaZdefiniowanyAdres
        '
        Me.rbDostawaNaZdefiniowanyAdres.AutoSize = True
        Me.rbDostawaNaZdefiniowanyAdres.ForeColor = System.Drawing.Color.Black
        Me.rbDostawaNaZdefiniowanyAdres.Location = New System.Drawing.Point(9, 75)
        Me.rbDostawaNaZdefiniowanyAdres.Name = "rbDostawaNaZdefiniowanyAdres"
        Me.rbDostawaNaZdefiniowanyAdres.Size = New System.Drawing.Size(178, 17)
        Me.rbDostawaNaZdefiniowanyAdres.TabIndex = 4
        Me.rbDostawaNaZdefiniowanyAdres.TabStop = True
        Me.rbDostawaNaZdefiniowanyAdres.Text = "Dostawa na zdefiniowany adres:"
        Me.rbDostawaNaZdefiniowanyAdres.UseVisualStyleBackColor = True
        '
        'rbOdbiorWlasny
        '
        Me.rbOdbiorWlasny.AutoSize = True
        Me.rbOdbiorWlasny.ForeColor = System.Drawing.Color.Black
        Me.rbOdbiorWlasny.Location = New System.Drawing.Point(9, 19)
        Me.rbOdbiorWlasny.Name = "rbOdbiorWlasny"
        Me.rbOdbiorWlasny.Size = New System.Drawing.Size(157, 17)
        Me.rbOdbiorWlasny.TabIndex = 0
        Me.rbOdbiorWlasny.TabStop = True
        Me.rbOdbiorWlasny.Text = "Odbiór w³asny w magazynie"
        Me.rbOdbiorWlasny.UseVisualStyleBackColor = True
        '
        'tsPozycje
        '
        Me.tsPozycje.AutoSize = False
        Me.tsPozycje.BackColor = System.Drawing.Color.DodgerBlue
        Me.tsPozycje.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnDodajPozycje, Me.btnUsunPozycje, Me.ToolStripSeparator1, Me.lblLimitOpis, Me.lblLimit, Me.lblLimitJM, Me.lblWartoscOpis, Me.lblWartosc, Me.LblWartoscJM, Me.lblStatusOpis, Me.lblStatusZamowienia})
        Me.tsPozycje.Location = New System.Drawing.Point(0, 0)
        Me.tsPozycje.Name = "tsPozycje"
        Me.tsPozycje.Size = New System.Drawing.Size(1002, 25)
        Me.tsPozycje.TabIndex = 0
        Me.tsPozycje.TabStop = True
        Me.tsPozycje.Text = "ToolStrip1"
        '
        'btnDodajPozycje
        '
        Me.btnDodajPozycje.ForeColor = System.Drawing.Color.White
        Me.btnDodajPozycje.Image = CType(resources.GetObject("btnDodajPozycje.Image"), System.Drawing.Image)
        Me.btnDodajPozycje.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnDodajPozycje.Name = "btnDodajPozycje"
        Me.btnDodajPozycje.Size = New System.Drawing.Size(95, 22)
        Me.btnDodajPozycje.Text = "Dodaj pozycjê"
        '
        'btnUsunPozycje
        '
        Me.btnUsunPozycje.ForeColor = System.Drawing.Color.White
        Me.btnUsunPozycje.Image = CType(resources.GetObject("btnUsunPozycje.Image"), System.Drawing.Image)
        Me.btnUsunPozycje.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnUsunPozycje.Name = "btnUsunPozycje"
        Me.btnUsunPozycje.Size = New System.Drawing.Size(91, 22)
        Me.btnUsunPozycje.Text = "Usuñ pozycjê"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'lblLimitOpis
        '
        Me.lblLimitOpis.ForeColor = System.Drawing.Color.White
        Me.lblLimitOpis.Name = "lblLimitOpis"
        Me.lblLimitOpis.Size = New System.Drawing.Size(35, 22)
        Me.lblLimitOpis.Text = "Limit: "
        '
        'lblLimit
        '
        Me.lblLimit.ForeColor = System.Drawing.Color.White
        Me.lblLimit.Name = "lblLimit"
        Me.lblLimit.Size = New System.Drawing.Size(31, 22)
        Me.lblLimit.Text = "0000"
        '
        'lblLimitJM
        '
        Me.lblLimitJM.ForeColor = System.Drawing.Color.White
        Me.lblLimitJM.Name = "lblLimitJM"
        Me.lblLimitJM.Size = New System.Drawing.Size(25, 22)
        Me.lblLimitJM.Text = "PKT"
        '
        'lblWartoscOpis
        '
        Me.lblWartoscOpis.ForeColor = System.Drawing.Color.White
        Me.lblWartoscOpis.Name = "lblWartoscOpis"
        Me.lblWartoscOpis.Size = New System.Drawing.Size(54, 22)
        Me.lblWartoscOpis.Text = "Wartoœæ: "
        '
        'lblWartosc
        '
        Me.lblWartosc.ForeColor = System.Drawing.Color.White
        Me.lblWartosc.Name = "lblWartosc"
        Me.lblWartosc.Size = New System.Drawing.Size(31, 22)
        Me.lblWartosc.Text = "0000"
        '
        'LblWartoscJM
        '
        Me.LblWartoscJM.ForeColor = System.Drawing.Color.White
        Me.LblWartoscJM.Name = "LblWartoscJM"
        Me.LblWartoscJM.Size = New System.Drawing.Size(25, 22)
        Me.LblWartoscJM.Text = "PKT"
        '
        'lblStatusOpis
        '
        Me.lblStatusOpis.ForeColor = System.Drawing.Color.White
        Me.lblStatusOpis.Name = "lblStatusOpis"
        Me.lblStatusOpis.Size = New System.Drawing.Size(42, 22)
        Me.lblStatusOpis.Text = "Status:"
        '
        'lblStatusZamowienia
        '
        Me.lblStatusZamowienia.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.lblStatusZamowienia.ForeColor = System.Drawing.Color.White
        Me.lblStatusZamowienia.Name = "lblStatusZamowienia"
        Me.lblStatusZamowienia.Size = New System.Drawing.Size(38, 22)
        Me.lblStatusZamowienia.Text = "Status"
        '
        'frmZamowienie
        '
        Me.AcceptButton = Me.btnZapiszZmiany
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnZamknij
        Me.ClientSize = New System.Drawing.Size(1002, 512)
        Me.Controls.Add(Me.sc)
        Me.Controls.Add(Me.tsPozycje)
        Me.MinimumSize = New System.Drawing.Size(880, 320)
        Me.Name = "frmZamowienie"
        Me.Text = "Zamówienie"
        Me.sc.Panel1.ResumeLayout(False)
        Me.sc.Panel2.ResumeLayout(False)
        CType(Me.sc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sc.ResumeLayout(False)
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbDaneKontaktowe.ResumeLayout(False)
        Me.gbDaneKontaktowe.PerformLayout()
        Me.gbOpcjeDostawy.ResumeLayout(False)
        Me.gbOpcjeDostawy.PerformLayout()
        Me.tsPozycje.ResumeLayout(False)
        Me.tsPozycje.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tsPozycje As System.Windows.Forms.ToolStrip
    Friend WithEvents dgv As System.Windows.Forms.DataGridView
    Friend WithEvents sc As System.Windows.Forms.SplitContainer
    Friend WithEvents btnZapiszZmiany As System.Windows.Forms.Button
    Friend WithEvents btnDodajPozycje As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnUsunPozycje As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents lblStatusOpis As System.Windows.Forms.ToolStripLabel
    Friend WithEvents gbOpcjeDostawy As System.Windows.Forms.GroupBox
    Friend WithEvents rbOdbiorWlasny As System.Windows.Forms.RadioButton
    Friend WithEvents rbDostawaNaAdres As System.Windows.Forms.RadioButton
    Friend WithEvents cmbDostawaNaZdefiniowanyAdres As System.Windows.Forms.ComboBox
    Friend WithEvents rbDostawaNaZdefiniowanyAdres As System.Windows.Forms.RadioButton
    Friend WithEvents lblAdres As System.Windows.Forms.Label
    Friend WithEvents txtAdres As System.Windows.Forms.TextBox
    Friend WithEvents txtMiasto As System.Windows.Forms.TextBox
    Friend WithEvents lblMiasto As System.Windows.Forms.Label
    Friend WithEvents lblKod As System.Windows.Forms.Label
    Friend WithEvents gbDaneKontaktowe As System.Windows.Forms.GroupBox
    Friend WithEvents lblTelefonKontaktowy As System.Windows.Forms.Label
    Friend WithEvents lblOsobaKontaktowa As System.Windows.Forms.Label
    Friend WithEvents txtOsobaKontaktowa As System.Windows.Forms.TextBox
    Friend WithEvents txtTelefonKontaktowy As System.Windows.Forms.TextBox
    Friend WithEvents txtUwagi As System.Windows.Forms.TextBox
    Friend WithEvents lblUwagi As System.Windows.Forms.Label
    Friend WithEvents btnZlozZamowienie As System.Windows.Forms.Button
    Friend WithEvents lblNazwa As System.Windows.Forms.Label
    Friend WithEvents txtNazwa As System.Windows.Forms.TextBox
    Friend WithEvents cmbMagazynOdbiorWlasny As System.Windows.Forms.ComboBox
    Friend WithEvents lblStatusZamowienia As System.Windows.Forms.ToolStripLabel
    Friend WithEvents dtpDataRealizacji As System.Windows.Forms.DateTimePicker
    Friend WithEvents LabelDataRealizacji As System.Windows.Forms.Label
    Friend WithEvents cmbMagazynOdbiorWlasnyDPD As System.Windows.Forms.ComboBox
    Friend WithEvents rbOdbiorWlasnyDPD As System.Windows.Forms.RadioButton
    Friend WithEvents lblLimitOpis As System.Windows.Forms.ToolStripLabel
    Friend WithEvents lblLimit As System.Windows.Forms.ToolStripLabel
    Friend WithEvents lblWartoscOpis As System.Windows.Forms.ToolStripLabel
    Friend WithEvents lblWartosc As System.Windows.Forms.ToolStripLabel
    Friend WithEvents lblLimitJM As System.Windows.Forms.ToolStripLabel
    Friend WithEvents LblWartoscJM As System.Windows.Forms.ToolStripLabel
    Friend WithEvents btnDaneDpd As System.Windows.Forms.Button
    Friend WithEvents btnOdbiorcy As System.Windows.Forms.Button
    Friend WithEvents lblOdbiorcy As System.Windows.Forms.Label
    Friend WithEvents rbZamowienieGrupowe As System.Windows.Forms.RadioButton
    Friend WithEvents lblUwagiPozostaloZnakow As System.Windows.Forms.Label
    Friend WithEvents btnZamknij As System.Windows.Forms.Button
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents sbSzukajAdresu As System.Windows.Forms.Button
    Friend WithEvents txtKodPocztowy As System.Windows.Forms.MaskedTextBox
End Class
