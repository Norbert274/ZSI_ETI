<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSKUNoweZExcela
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
        Me.dgv = New System.Windows.Forms.DataGridView()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.btnAnuluj = New System.Windows.Forms.Button()
        Me.btnZalozProdukty = New System.Windows.Forms.Button()
        Me.lblIloscSKU = New System.Windows.Forms.Label()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnSzablon = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbNazwaArkusza = New System.Windows.Forms.ComboBox()
        Me.btnWybierzPlik = New System.Windows.Forms.Button()
        Me.txtPlik = New System.Windows.Forms.TextBox()
        Me.lblPrzetwarzanie = New System.Windows.Forms.Label()
        Me.gbGrupaArtykulow = New System.Windows.Forms.GroupBox()
        Me.cmbGrupaArtykulow = New System.Windows.Forms.ComboBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.gbGrupaArtykulow.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgv
        '
        Me.dgv.AllowUserToAddRows = False
        Me.dgv.AllowUserToDeleteRows = False
        Me.dgv.AllowUserToResizeRows = False
        Me.dgv.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgv.BackgroundColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgv.Location = New System.Drawing.Point(6, 81)
        Me.dgv.Name = "dgv"
        Me.dgv.ReadOnly = True
        Me.dgv.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dgv.RowHeadersVisible = False
        Me.dgv.Size = New System.Drawing.Size(652, 187)
        Me.dgv.TabIndex = 2
        '
        'lblProgress
        '
        Me.lblProgress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblProgress.AutoSize = True
        Me.lblProgress.BackColor = System.Drawing.Color.Transparent
        Me.lblProgress.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblProgress.Location = New System.Drawing.Point(374, 270)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(11, 13)
        Me.lblProgress.TabIndex = 4
        Me.lblProgress.Text = " "
        '
        'btnAnuluj
        '
        Me.btnAnuluj.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAnuluj.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnAnuluj.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnAnuluj.ForeColor = System.Drawing.Color.White
        Me.btnAnuluj.Location = New System.Drawing.Point(565, 316)
        Me.btnAnuluj.Name = "btnAnuluj"
        Me.btnAnuluj.Size = New System.Drawing.Size(93, 23)
        Me.btnAnuluj.TabIndex = 8
        Me.btnAnuluj.Text = "Zamknij"
        Me.btnAnuluj.UseVisualStyleBackColor = False
        '
        'btnZalozProdukty
        '
        Me.btnZalozProdukty.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnZalozProdukty.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnZalozProdukty.Enabled = False
        Me.btnZalozProdukty.ForeColor = System.Drawing.Color.White
        Me.btnZalozProdukty.Location = New System.Drawing.Point(418, 316)
        Me.btnZalozProdukty.Name = "btnZalozProdukty"
        Me.btnZalozProdukty.Size = New System.Drawing.Size(141, 23)
        Me.btnZalozProdukty.TabIndex = 7
        Me.btnZalozProdukty.Text = "Załóż produkty"
        Me.ToolTip1.SetToolTip(Me.btnZalozProdukty, "Zakładanie produktów z wczytanego pliku do importu produktów")
        Me.btnZalozProdukty.UseVisualStyleBackColor = False
        '
        'lblIloscSKU
        '
        Me.lblIloscSKU.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblIloscSKU.AutoSize = True
        Me.lblIloscSKU.BackColor = System.Drawing.Color.Transparent
        Me.lblIloscSKU.Location = New System.Drawing.Point(10, 271)
        Me.lblIloscSKU.Name = "lblIloscSKU"
        Me.lblIloscSKU.Size = New System.Drawing.Size(94, 13)
        Me.lblIloscSKU.TabIndex = 3
        Me.lblIloscSKU.Text = "Ilość produktów: 0"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProgressBar1.Location = New System.Drawing.Point(11, 287)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(647, 23)
        Me.ProgressBar1.TabIndex = 5
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnSzablon)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.cmbNazwaArkusza)
        Me.GroupBox1.Controls.Add(Me.btnWybierzPlik)
        Me.GroupBox1.Controls.Add(Me.txtPlik)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(413, 71)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Wybierz plik :"
        '
        'btnSzablon
        '
        Me.btnSzablon.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnSzablon.ForeColor = System.Drawing.Color.White
        Me.btnSzablon.Location = New System.Drawing.Point(326, 41)
        Me.btnSzablon.Name = "btnSzablon"
        Me.btnSzablon.Size = New System.Drawing.Size(81, 23)
        Me.btnSzablon.TabIndex = 4
        Me.btnSzablon.Text = "Szablon"
        Me.btnSzablon.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 46)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(83, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Nazwa arkusza:"
        '
        'cmbNazwaArkusza
        '
        Me.cmbNazwaArkusza.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbNazwaArkusza.FormattingEnabled = True
        Me.cmbNazwaArkusza.Location = New System.Drawing.Point(96, 43)
        Me.cmbNazwaArkusza.Name = "cmbNazwaArkusza"
        Me.cmbNazwaArkusza.Size = New System.Drawing.Size(224, 21)
        Me.cmbNazwaArkusza.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.cmbNazwaArkusza, "Wybieranie nazwy arkusza")
        '
        'btnWybierzPlik
        '
        Me.btnWybierzPlik.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnWybierzPlik.ForeColor = System.Drawing.Color.White
        Me.btnWybierzPlik.Location = New System.Drawing.Point(326, 15)
        Me.btnWybierzPlik.Name = "btnWybierzPlik"
        Me.btnWybierzPlik.Size = New System.Drawing.Size(81, 23)
        Me.btnWybierzPlik.TabIndex = 1
        Me.btnWybierzPlik.Text = "Wybierz plik"
        Me.ToolTip1.SetToolTip(Me.btnWybierzPlik, "Wybieranie pliku do importu produktów do systemu")
        Me.btnWybierzPlik.UseVisualStyleBackColor = False
        '
        'txtPlik
        '
        Me.txtPlik.Location = New System.Drawing.Point(10, 17)
        Me.txtPlik.Name = "txtPlik"
        Me.txtPlik.ReadOnly = True
        Me.txtPlik.Size = New System.Drawing.Size(310, 20)
        Me.txtPlik.TabIndex = 0
        '
        'lblPrzetwarzanie
        '
        Me.lblPrzetwarzanie.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPrzetwarzanie.AutoSize = True
        Me.lblPrzetwarzanie.BackColor = System.Drawing.Color.Transparent
        Me.lblPrzetwarzanie.Location = New System.Drawing.Point(10, 314)
        Me.lblPrzetwarzanie.Name = "lblPrzetwarzanie"
        Me.lblPrzetwarzanie.Size = New System.Drawing.Size(10, 13)
        Me.lblPrzetwarzanie.TabIndex = 6
        Me.lblPrzetwarzanie.Text = " "
        '
        'gbGrupaArtykulow
        '
        Me.gbGrupaArtykulow.Controls.Add(Me.cmbGrupaArtykulow)
        Me.gbGrupaArtykulow.Location = New System.Drawing.Point(425, 4)
        Me.gbGrupaArtykulow.Name = "gbGrupaArtykulow"
        Me.gbGrupaArtykulow.Size = New System.Drawing.Size(235, 49)
        Me.gbGrupaArtykulow.TabIndex = 1
        Me.gbGrupaArtykulow.TabStop = False
        Me.gbGrupaArtykulow.Text = "Grupa artykułów:"
        Me.ToolTip1.SetToolTip(Me.gbGrupaArtykulow, "Wybierz grupę artykułów")
        '
        'cmbGrupaArtykulow
        '
        Me.cmbGrupaArtykulow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbGrupaArtykulow.FormattingEnabled = True
        Me.cmbGrupaArtykulow.Location = New System.Drawing.Point(6, 17)
        Me.cmbGrupaArtykulow.Name = "cmbGrupaArtykulow"
        Me.cmbGrupaArtykulow.Size = New System.Drawing.Size(223, 21)
        Me.cmbGrupaArtykulow.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.cmbGrupaArtykulow, "Wybieranie grupy artykułów dla zakładanych produktów")
        '
        'ToolTip1
        '
        Me.ToolTip1.AutoPopDelay = 10000
        Me.ToolTip1.InitialDelay = 500
        Me.ToolTip1.ReshowDelay = 500
        '
        'frmSKUNoweZExcela
        '
        Me.AcceptButton = Me.btnZalozProdukty
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnAnuluj
        Me.ClientSize = New System.Drawing.Size(665, 351)
        Me.Controls.Add(Me.gbGrupaArtykulow)
        Me.Controls.Add(Me.lblProgress)
        Me.Controls.Add(Me.lblPrzetwarzanie)
        Me.Controls.Add(Me.dgv)
        Me.Controls.Add(Me.btnAnuluj)
        Me.Controls.Add(Me.btnZalozProdukty)
        Me.Controls.Add(Me.lblIloscSKU)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.GroupBox1)
        Me.MinimumSize = New System.Drawing.Size(681, 389)
        Me.Name = "frmSKUNoweZExcela"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Dodawanie produktów z pliku Excela"
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.gbGrupaArtykulow.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgv As System.Windows.Forms.DataGridView
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents btnAnuluj As System.Windows.Forms.Button
    Friend WithEvents btnZalozProdukty As System.Windows.Forms.Button
    Friend WithEvents lblIloscSKU As System.Windows.Forms.Label
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbNazwaArkusza As System.Windows.Forms.ComboBox
    Friend WithEvents btnWybierzPlik As System.Windows.Forms.Button
    Friend WithEvents txtPlik As System.Windows.Forms.TextBox
    Friend WithEvents lblPrzetwarzanie As System.Windows.Forms.Label
    Friend WithEvents gbGrupaArtykulow As System.Windows.Forms.GroupBox
    Friend WithEvents cmbGrupaArtykulow As System.Windows.Forms.ComboBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents btnSzablon As System.Windows.Forms.Button
End Class
