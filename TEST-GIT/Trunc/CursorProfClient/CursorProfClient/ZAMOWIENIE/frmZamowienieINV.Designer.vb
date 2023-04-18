<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmZamowienieINV
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmZamowienieINV))
        Me.txtUwagi = New System.Windows.Forms.TextBox()
        Me.lblUwagi = New System.Windows.Forms.Label()
        Me.btnZapiszZmiany = New System.Windows.Forms.Button()
        Me.tsPozycje = New System.Windows.Forms.ToolStrip()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.lblStatusOpis = New System.Windows.Forms.ToolStripLabel()
        Me.lblStatusZamowienia = New System.Windows.Forms.ToolStripLabel()
        Me.gbTypZamowienia = New System.Windows.Forms.GroupBox()
        Me.lblTypZamowienia = New System.Windows.Forms.Label()
        Me.cmbTypZamowienia = New System.Windows.Forms.ComboBox()
        Me.btnZlozZamowienie = New System.Windows.Forms.Button()
        Me.dgv = New System.Windows.Forms.DataGridView()
        Me.sc = New System.Windows.Forms.SplitContainer()
        Me.btnDodajPozycje = New System.Windows.Forms.ToolStripButton()
        Me.btnUsunPozycje = New System.Windows.Forms.ToolStripButton()
        Me.btnImportPozycjiExcel = New System.Windows.Forms.ToolStripButton()
        Me.tsPozycje.SuspendLayout()
        Me.gbTypZamowienia.SuspendLayout()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sc.Panel1.SuspendLayout()
        Me.sc.Panel2.SuspendLayout()
        Me.sc.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtUwagi
        '
        Me.txtUwagi.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtUwagi.Location = New System.Drawing.Point(112, 47)
        Me.txtUwagi.Multiline = True
        Me.txtUwagi.Name = "txtUwagi"
        Me.txtUwagi.Size = New System.Drawing.Size(577, 65)
        Me.txtUwagi.TabIndex = 3
        '
        'lblUwagi
        '
        Me.lblUwagi.AutoSize = True
        Me.lblUwagi.ForeColor = System.Drawing.Color.Black
        Me.lblUwagi.Location = New System.Drawing.Point(9, 45)
        Me.lblUwagi.Name = "lblUwagi"
        Me.lblUwagi.Size = New System.Drawing.Size(40, 13)
        Me.lblUwagi.TabIndex = 2
        Me.lblUwagi.Text = "Uwagi:"
        '
        'btnZapiszZmiany
        '
        Me.btnZapiszZmiany.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnZapiszZmiany.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnZapiszZmiany.ForeColor = System.Drawing.Color.White
        Me.btnZapiszZmiany.Location = New System.Drawing.Point(607, 125)
        Me.btnZapiszZmiany.Name = "btnZapiszZmiany"
        Me.btnZapiszZmiany.Size = New System.Drawing.Size(100, 23)
        Me.btnZapiszZmiany.TabIndex = 2
        Me.btnZapiszZmiany.Text = "Zapisz zmiany"
        Me.btnZapiszZmiany.UseVisualStyleBackColor = False
        '
        'tsPozycje
        '
        Me.tsPozycje.AutoSize = False
        Me.tsPozycje.BackColor = System.Drawing.Color.DodgerBlue
        Me.tsPozycje.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnDodajPozycje, Me.btnUsunPozycje, Me.ToolStripSeparator1, Me.lblStatusOpis, Me.lblStatusZamowienia, Me.btnImportPozycjiExcel})
        Me.tsPozycje.Location = New System.Drawing.Point(0, 0)
        Me.tsPozycje.Name = "tsPozycje"
        Me.tsPozycje.Size = New System.Drawing.Size(719, 25)
        Me.tsPozycje.TabIndex = 0
        Me.tsPozycje.TabStop = True
        Me.tsPozycje.Text = "ToolStrip1"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
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
        'gbTypZamowienia
        '
        Me.gbTypZamowienia.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbTypZamowienia.Controls.Add(Me.lblTypZamowienia)
        Me.gbTypZamowienia.Controls.Add(Me.txtUwagi)
        Me.gbTypZamowienia.Controls.Add(Me.cmbTypZamowienia)
        Me.gbTypZamowienia.Controls.Add(Me.lblUwagi)
        Me.gbTypZamowienia.ForeColor = System.Drawing.Color.Black
        Me.gbTypZamowienia.Location = New System.Drawing.Point(12, 3)
        Me.gbTypZamowienia.Name = "gbTypZamowienia"
        Me.gbTypZamowienia.Size = New System.Drawing.Size(695, 118)
        Me.gbTypZamowienia.TabIndex = 0
        Me.gbTypZamowienia.TabStop = False
        Me.gbTypZamowienia.Text = "Informacje o zamówieniu:"
        '
        'lblTypZamowienia
        '
        Me.lblTypZamowienia.AutoSize = True
        Me.lblTypZamowienia.ForeColor = System.Drawing.Color.Black
        Me.lblTypZamowienia.Location = New System.Drawing.Point(9, 23)
        Me.lblTypZamowienia.Name = "lblTypZamowienia"
        Me.lblTypZamowienia.Size = New System.Drawing.Size(97, 13)
        Me.lblTypZamowienia.TabIndex = 0
        Me.lblTypZamowienia.Text = "Typ inwentaryzacji:"
        '
        'cmbTypZamowienia
        '
        Me.cmbTypZamowienia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTypZamowienia.FormattingEnabled = True
        Me.cmbTypZamowienia.Location = New System.Drawing.Point(112, 20)
        Me.cmbTypZamowienia.Name = "cmbTypZamowienia"
        Me.cmbTypZamowienia.Size = New System.Drawing.Size(326, 21)
        Me.cmbTypZamowienia.TabIndex = 1
        '
        'btnZlozZamowienie
        '
        Me.btnZlozZamowienie.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnZlozZamowienie.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnZlozZamowienie.ForeColor = System.Drawing.Color.White
        Me.btnZlozZamowienie.Location = New System.Drawing.Point(491, 125)
        Me.btnZlozZamowienie.Name = "btnZlozZamowienie"
        Me.btnZlozZamowienie.Size = New System.Drawing.Size(110, 23)
        Me.btnZlozZamowienie.TabIndex = 1
        Me.btnZlozZamowienie.Text = "Złóż zamówienie"
        Me.btnZlozZamowienie.UseVisualStyleBackColor = False
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
        Me.dgv.Size = New System.Drawing.Size(719, 277)
        Me.dgv.TabIndex = 0
        '
        'sc
        '
        Me.sc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sc.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.sc.Location = New System.Drawing.Point(0, 25)
        Me.sc.Name = "sc"
        Me.sc.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'sc.Panel1
        '
        Me.sc.Panel1.Controls.Add(Me.dgv)
        '
        'sc.Panel2
        '
        Me.sc.Panel2.BackColor = System.Drawing.Color.White
        Me.sc.Panel2.Controls.Add(Me.btnZlozZamowienie)
        Me.sc.Panel2.Controls.Add(Me.btnZapiszZmiany)
        Me.sc.Panel2.Controls.Add(Me.gbTypZamowienia)
        Me.sc.Panel2MinSize = 160
        Me.sc.Size = New System.Drawing.Size(719, 441)
        Me.sc.SplitterDistance = 277
        Me.sc.TabIndex = 16
        '
        'btnDodajPozycje
        '
        Me.btnDodajPozycje.ForeColor = System.Drawing.Color.White
        Me.btnDodajPozycje.Image = CType(resources.GetObject("btnDodajPozycje.Image"), System.Drawing.Image)
        Me.btnDodajPozycje.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnDodajPozycje.Name = "btnDodajPozycje"
        Me.btnDodajPozycje.Size = New System.Drawing.Size(101, 22)
        Me.btnDodajPozycje.Text = "Dodaj pozycję"
        '
        'btnUsunPozycje
        '
        Me.btnUsunPozycje.ForeColor = System.Drawing.Color.White
        Me.btnUsunPozycje.Image = CType(resources.GetObject("btnUsunPozycje.Image"), System.Drawing.Image)
        Me.btnUsunPozycje.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnUsunPozycje.Name = "btnUsunPozycje"
        Me.btnUsunPozycje.Size = New System.Drawing.Size(97, 22)
        Me.btnUsunPozycje.Text = "Usuń pozycję"
        '
        'btnImportPozycjiExcel
        '
        Me.btnImportPozycjiExcel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.btnImportPozycjiExcel.ForeColor = System.Drawing.Color.White
        Me.btnImportPozycjiExcel.Image = CType(resources.GetObject("btnImportPozycjiExcel.Image"), System.Drawing.Image)
        Me.btnImportPozycjiExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnImportPozycjiExcel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnImportPozycjiExcel.Name = "btnImportPozycjiExcel"
        Me.btnImportPozycjiExcel.Size = New System.Drawing.Size(159, 22)
        Me.btnImportPozycjiExcel.Text = "Importuj pozycje z Excela"
        '
        'frmZamowienieINV
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(719, 466)
        Me.Controls.Add(Me.sc)
        Me.Controls.Add(Me.tsPozycje)
        Me.MinimumSize = New System.Drawing.Size(483, 292)
        Me.Name = "frmZamowienieINV"
        Me.Text = "Koszyk - inwentaryzacja +/-"
        Me.tsPozycje.ResumeLayout(False)
        Me.tsPozycje.PerformLayout()
        Me.gbTypZamowienia.ResumeLayout(False)
        Me.gbTypZamowienia.PerformLayout()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sc.Panel1.ResumeLayout(False)
        Me.sc.Panel2.ResumeLayout(False)
        CType(Me.sc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sc.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtUwagi As System.Windows.Forms.TextBox
    Friend WithEvents lblUwagi As System.Windows.Forms.Label
    Friend WithEvents btnDodajPozycje As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnZapiszZmiany As System.Windows.Forms.Button
    Friend WithEvents tsPozycje As System.Windows.Forms.ToolStrip
    Friend WithEvents btnUsunPozycje As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents lblStatusOpis As System.Windows.Forms.ToolStripLabel
    Friend WithEvents lblStatusZamowienia As System.Windows.Forms.ToolStripLabel
    Friend WithEvents gbTypZamowienia As System.Windows.Forms.GroupBox
    Friend WithEvents lblTypZamowienia As System.Windows.Forms.Label
    Friend WithEvents cmbTypZamowienia As System.Windows.Forms.ComboBox
    Friend WithEvents btnZlozZamowienie As System.Windows.Forms.Button
    Friend WithEvents dgv As System.Windows.Forms.DataGridView
    Friend WithEvents sc As System.Windows.Forms.SplitContainer
    Friend WithEvents btnImportPozycjiExcel As System.Windows.Forms.ToolStripButton
End Class
