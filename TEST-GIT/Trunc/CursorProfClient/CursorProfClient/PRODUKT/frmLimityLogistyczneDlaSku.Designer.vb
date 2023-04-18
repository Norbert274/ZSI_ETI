<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLimityLogistyczneDlaSku
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLimityLogistyczneDlaSku))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.tsFiltry = New System.Windows.Forms.ToolStrip()
        Me.txtFiltr = New System.Windows.Forms.ToolStripTextBox()
        Me.btnFiltruj = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.dgv = New System.Windows.Forms.DataGridView()
        Me.tsListaDolny = New System.Windows.Forms.ToolStrip()
        Me.btnOdswiez = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnZapisz = New System.Windows.Forms.ToolStripButton()
        Me.btnZamknij = New System.Windows.Forms.Button()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnWyczyscFiltry = New System.Windows.Forms.ToolStripButton()
        Me.tsFiltry.SuspendLayout()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tsListaDolny.SuspendLayout()
        Me.SuspendLayout()
        '
        'tsFiltry
        '
        Me.tsFiltry.BackColor = System.Drawing.Color.DodgerBlue
        Me.tsFiltry.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.txtFiltr, Me.btnFiltruj, Me.ToolStripSeparator2, Me.btnWyczyscFiltry})
        Me.tsFiltry.Location = New System.Drawing.Point(0, 0)
        Me.tsFiltry.Name = "tsFiltry"
        Me.tsFiltry.Size = New System.Drawing.Size(693, 25)
        Me.tsFiltry.TabIndex = 0
        Me.tsFiltry.Text = "ToolStrip1"
        '
        'txtFiltr
        '
        Me.txtFiltr.Name = "txtFiltr"
        Me.txtFiltr.Size = New System.Drawing.Size(180, 25)
        Me.txtFiltr.ToolTipText = "Filtrowanie po numerze produktu lub nazwie"
        '
        'btnFiltruj
        '
        Me.btnFiltruj.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnFiltruj.ForeColor = System.Drawing.Color.White
        Me.btnFiltruj.Image = CType(resources.GetObject("btnFiltruj.Image"), System.Drawing.Image)
        Me.btnFiltruj.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnFiltruj.Name = "btnFiltruj"
        Me.btnFiltruj.Size = New System.Drawing.Size(41, 22)
        Me.btnFiltruj.Text = "Filtruj"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'dgv
        '
        Me.dgv.AllowUserToAddRows = False
        Me.dgv.AllowUserToResizeRows = False
        Me.dgv.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv.ColumnHeadersHeight = 25
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgv.Location = New System.Drawing.Point(3, 28)
        Me.dgv.Name = "dgv"
        Me.dgv.RowHeadersVisible = False
        Me.dgv.Size = New System.Drawing.Size(689, 453)
        Me.dgv.TabIndex = 1
        '
        'tsListaDolny
        '
        Me.tsListaDolny.BackColor = System.Drawing.Color.White
        Me.tsListaDolny.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.tsListaDolny.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnOdswiez, Me.ToolStripSeparator1, Me.btnZapisz})
        Me.tsListaDolny.Location = New System.Drawing.Point(0, 484)
        Me.tsListaDolny.Name = "tsListaDolny"
        Me.tsListaDolny.Size = New System.Drawing.Size(693, 25)
        Me.tsListaDolny.TabIndex = 2
        Me.tsListaDolny.Text = "ToolStrip1"
        '
        'btnOdswiez
        '
        Me.btnOdswiez.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.btnOdswiez.Image = CType(resources.GetObject("btnOdswiez.Image"), System.Drawing.Image)
        Me.btnOdswiez.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.btnOdswiez.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnOdswiez.Name = "btnOdswiez"
        Me.btnOdswiez.Size = New System.Drawing.Size(71, 22)
        Me.btnOdswiez.Text = "&Odswież"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'btnZapisz
        '
        Me.btnZapisz.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.btnZapisz.Image = CType(resources.GetObject("btnZapisz.Image"), System.Drawing.Image)
        Me.btnZapisz.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnZapisz.Name = "btnZapisz"
        Me.btnZapisz.Size = New System.Drawing.Size(155, 22)
        Me.btnZapisz.Text = "Zapisz limity logistyczne"
        '
        'btnZamknij
        '
        Me.btnZamknij.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnZamknij.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnZamknij.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnZamknij.ForeColor = System.Drawing.Color.White
        Me.btnZamknij.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnZamknij.Location = New System.Drawing.Point(3, 484)
        Me.btnZamknij.Name = "btnZamknij"
        Me.btnZamknij.Size = New System.Drawing.Size(97, 24)
        Me.btnZamknij.TabIndex = 3
        Me.btnZamknij.Text = "Zamknij"
        Me.btnZamknij.UseVisualStyleBackColor = False
        '
        'btnWyczyscFiltry
        '
        Me.btnWyczyscFiltry.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnWyczyscFiltry.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnWyczyscFiltry.ForeColor = System.Drawing.Color.White
        Me.btnWyczyscFiltry.Image = CType(resources.GetObject("btnWyczyscFiltry.Image"), System.Drawing.Image)
        Me.btnWyczyscFiltry.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnWyczyscFiltry.Name = "btnWyczyscFiltry"
        Me.btnWyczyscFiltry.Size = New System.Drawing.Size(77, 22)
        Me.btnWyczyscFiltry.Text = "Wyczyść filtr"
        '
        'frmLimityLogistyczneDlaSku
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnZamknij
        Me.ClientSize = New System.Drawing.Size(693, 509)
        Me.Controls.Add(Me.dgv)
        Me.Controls.Add(Me.tsListaDolny)
        Me.Controls.Add(Me.tsFiltry)
        Me.Controls.Add(Me.btnZamknij)
        Me.Name = "frmLimityLogistyczneDlaSku"
        Me.Text = "Limity logistyczne dla produktów"
        Me.tsFiltry.ResumeLayout(False)
        Me.tsFiltry.PerformLayout()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tsListaDolny.ResumeLayout(False)
        Me.tsListaDolny.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tsFiltry As System.Windows.Forms.ToolStrip
    Friend WithEvents txtFiltr As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents btnFiltruj As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents dgv As System.Windows.Forms.DataGridView
    Friend WithEvents tsListaDolny As System.Windows.Forms.ToolStrip
    Friend WithEvents btnOdswiez As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnZapisz As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnZamknij As System.Windows.Forms.Button
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents btnWyczyscFiltry As System.Windows.Forms.ToolStripButton
End Class
