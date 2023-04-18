<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMagazyny
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMagazyny))
        Me.sc = New System.Windows.Forms.SplitContainer()
        Me.dgv = New System.Windows.Forms.DataGridView()
        Me.ts = New System.Windows.Forms.ToolStrip()
        Me.btnNowy = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnEdytuj = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnUsun = New System.Windows.Forms.ToolStripButton()
        Me.btnOdswiez = New System.Windows.Forms.ToolStripButton()
        Me.btnAnuluj = New System.Windows.Forms.Button()
        Me.btnWybierz = New System.Windows.Forms.Button()
        CType(Me.sc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sc.Panel1.SuspendLayout()
        Me.sc.Panel2.SuspendLayout()
        Me.sc.SuspendLayout()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ts.SuspendLayout()
        Me.SuspendLayout()
        '
        'sc
        '
        Me.sc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sc.Location = New System.Drawing.Point(0, 0)
        Me.sc.Name = "sc"
        Me.sc.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'sc.Panel1
        '
        Me.sc.Panel1.Controls.Add(Me.dgv)
        Me.sc.Panel1.Controls.Add(Me.ts)
        '
        'sc.Panel2
        '
        Me.sc.Panel2.BackColor = System.Drawing.Color.White
        Me.sc.Panel2.Controls.Add(Me.btnAnuluj)
        Me.sc.Panel2.Controls.Add(Me.btnWybierz)
        Me.sc.Size = New System.Drawing.Size(534, 341)
        Me.sc.SplitterDistance = 308
        Me.sc.TabIndex = 2
        '
        'dgv
        '
        Me.dgv.AllowUserToAddRows = False
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
        Me.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
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
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.dgv.RowHeadersVisible = False
        Me.dgv.Size = New System.Drawing.Size(534, 283)
        Me.dgv.TabIndex = 1
        '
        'ts
        '
        Me.ts.BackColor = System.Drawing.Color.White
        Me.ts.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNowy, Me.ToolStripSeparator2, Me.btnEdytuj, Me.ToolStripSeparator1, Me.btnUsun, Me.btnOdswiez})
        Me.ts.Location = New System.Drawing.Point(0, 0)
        Me.ts.Name = "ts"
        Me.ts.Size = New System.Drawing.Size(534, 25)
        Me.ts.TabIndex = 0
        Me.ts.Text = "ToolStrip1"
        '
        'btnNowy
        '
        Me.btnNowy.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnNowy.Enabled = False
        Me.btnNowy.ForeColor = System.Drawing.Color.White
        Me.btnNowy.Image = CType(resources.GetObject("btnNowy.Image"), System.Drawing.Image)
        Me.btnNowy.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnNowy.Name = "btnNowy"
        Me.btnNowy.Size = New System.Drawing.Size(58, 22)
        Me.btnNowy.Text = "Nowy"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'btnEdytuj
        '
        Me.btnEdytuj.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnEdytuj.Enabled = False
        Me.btnEdytuj.ForeColor = System.Drawing.Color.White
        Me.btnEdytuj.Image = CType(resources.GetObject("btnEdytuj.Image"), System.Drawing.Image)
        Me.btnEdytuj.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnEdytuj.Name = "btnEdytuj"
        Me.btnEdytuj.Size = New System.Drawing.Size(60, 22)
        Me.btnEdytuj.Text = "Edytuj"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'btnUsun
        '
        Me.btnUsun.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnUsun.Enabled = False
        Me.btnUsun.ForeColor = System.Drawing.Color.White
        Me.btnUsun.Image = CType(resources.GetObject("btnUsun.Image"), System.Drawing.Image)
        Me.btnUsun.Name = "btnUsun"
        Me.btnUsun.RightToLeftAutoMirrorImage = True
        Me.btnUsun.Size = New System.Drawing.Size(54, 22)
        Me.btnUsun.Text = "Usuñ"
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
        'btnAnuluj
        '
        Me.btnAnuluj.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAnuluj.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnAnuluj.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnAnuluj.ForeColor = System.Drawing.Color.White
        Me.btnAnuluj.Location = New System.Drawing.Point(447, 3)
        Me.btnAnuluj.Name = "btnAnuluj"
        Me.btnAnuluj.Size = New System.Drawing.Size(75, 23)
        Me.btnAnuluj.TabIndex = 1
        Me.btnAnuluj.Text = "Anuluj"
        Me.btnAnuluj.UseVisualStyleBackColor = False
        '
        'btnWybierz
        '
        Me.btnWybierz.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnWybierz.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnWybierz.ForeColor = System.Drawing.Color.White
        Me.btnWybierz.Location = New System.Drawing.Point(366, 2)
        Me.btnWybierz.Name = "btnWybierz"
        Me.btnWybierz.Size = New System.Drawing.Size(75, 23)
        Me.btnWybierz.TabIndex = 0
        Me.btnWybierz.Text = "Wybierz"
        Me.btnWybierz.UseVisualStyleBackColor = False
        '
        'frmMagazyny
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnAnuluj
        Me.CausesValidation = False
        Me.ClientSize = New System.Drawing.Size(534, 341)
        Me.Controls.Add(Me.sc)
        Me.Name = "frmMagazyny"
        Me.Text = "Magazyny"
        Me.sc.Panel1.ResumeLayout(False)
        Me.sc.Panel1.PerformLayout()
        Me.sc.Panel2.ResumeLayout(False)
        CType(Me.sc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sc.ResumeLayout(False)
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ts.ResumeLayout(False)
        Me.ts.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents sc As System.Windows.Forms.SplitContainer
    Friend WithEvents dgv As System.Windows.Forms.DataGridView
    Friend WithEvents ts As System.Windows.Forms.ToolStrip
    Friend WithEvents btnNowy As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnEdytuj As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnUsun As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnOdswiez As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnWybierz As System.Windows.Forms.Button
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnAnuluj As System.Windows.Forms.Button
End Class
