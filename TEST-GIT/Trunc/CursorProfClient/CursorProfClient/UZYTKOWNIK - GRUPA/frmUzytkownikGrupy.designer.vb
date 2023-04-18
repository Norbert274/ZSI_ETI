<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUzytkownikGrupy
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUzytkownikGrupy))
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.lblDostepneGrupy = New System.Windows.Forms.ToolStripLabel()
        Me.scLewy = New System.Windows.Forms.SplitContainer()
        Me.dgvBiezace = New System.Windows.Forms.DataGridView()
        Me.tsLewy = New System.Windows.Forms.ToolStrip()
        Me.lblGrupyDoKtorychNalezy = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.txtBiezace = New System.Windows.Forms.ToolStripTextBox()
        Me.btnBiezace = New System.Windows.Forms.ToolStripButton()
        Me.scPrawy = New System.Windows.Forms.SplitContainer()
        Me.btnUsun = New System.Windows.Forms.Button()
        Me.btnDodaj = New System.Windows.Forms.Button()
        Me.dgvDostepne = New System.Windows.Forms.DataGridView()
        Me.tsPrawy = New System.Windows.Forms.ToolStrip()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.txtDostepne = New System.Windows.Forms.ToolStripTextBox()
        Me.btnDostepne = New System.Windows.Forms.ToolStripButton()
        Me.btnAnuluj = New System.Windows.Forms.Button()
        Me.scPoziomy = New System.Windows.Forms.SplitContainer()
        Me.btnOk = New System.Windows.Forms.Button()
        CType(Me.scLewy, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scLewy.Panel1.SuspendLayout()
        Me.scLewy.Panel2.SuspendLayout()
        Me.scLewy.SuspendLayout()
        CType(Me.dgvBiezace, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tsLewy.SuspendLayout()
        CType(Me.scPrawy, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scPrawy.Panel1.SuspendLayout()
        Me.scPrawy.Panel2.SuspendLayout()
        Me.scPrawy.SuspendLayout()
        CType(Me.dgvDostepne, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tsPrawy.SuspendLayout()
        CType(Me.scPoziomy, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scPoziomy.Panel1.SuspendLayout()
        Me.scPoziomy.Panel2.SuspendLayout()
        Me.scPoziomy.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblDostepneGrupy
        '
        Me.lblDostepneGrupy.ForeColor = System.Drawing.Color.White
        Me.lblDostepneGrupy.Name = "lblDostepneGrupy"
        Me.lblDostepneGrupy.Size = New System.Drawing.Size(91, 22)
        Me.lblDostepneGrupy.Text = "Dostêpne grupy"
        '
        'scLewy
        '
        Me.scLewy.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scLewy.IsSplitterFixed = True
        Me.scLewy.Location = New System.Drawing.Point(0, 0)
        Me.scLewy.Name = "scLewy"
        '
        'scLewy.Panel1
        '
        Me.scLewy.Panel1.Controls.Add(Me.dgvBiezace)
        Me.scLewy.Panel1.Controls.Add(Me.tsLewy)
        '
        'scLewy.Panel2
        '
        Me.scLewy.Panel2.Controls.Add(Me.scPrawy)
        Me.scLewy.Size = New System.Drawing.Size(742, 333)
        Me.scLewy.SplitterDistance = 321
        Me.scLewy.TabIndex = 0
        '
        'dgvBiezace
        '
        Me.dgvBiezace.AllowUserToAddRows = False
        Me.dgvBiezace.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue
        Me.dgvBiezace.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvBiezace.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgvBiezace.BackgroundColor = System.Drawing.Color.White
        Me.dgvBiezace.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvBiezace.ColumnHeadersVisible = False
        Me.dgvBiezace.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvBiezace.Location = New System.Drawing.Point(0, 25)
        Me.dgvBiezace.Name = "dgvBiezace"
        Me.dgvBiezace.ReadOnly = True
        Me.dgvBiezace.RowHeadersVisible = False
        Me.dgvBiezace.Size = New System.Drawing.Size(321, 308)
        Me.dgvBiezace.TabIndex = 1
        '
        'tsLewy
        '
        Me.tsLewy.BackColor = System.Drawing.Color.DodgerBlue
        Me.tsLewy.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblGrupyDoKtorychNalezy, Me.ToolStripSeparator2, Me.txtBiezace, Me.btnBiezace})
        Me.tsLewy.Location = New System.Drawing.Point(0, 0)
        Me.tsLewy.Name = "tsLewy"
        Me.tsLewy.Size = New System.Drawing.Size(321, 25)
        Me.tsLewy.TabIndex = 0
        Me.tsLewy.Text = "ToolStrip1"
        '
        'lblGrupyDoKtorychNalezy
        '
        Me.lblGrupyDoKtorychNalezy.ForeColor = System.Drawing.Color.White
        Me.lblGrupyDoKtorychNalezy.Name = "lblGrupyDoKtorychNalezy"
        Me.lblGrupyDoKtorychNalezy.Size = New System.Drawing.Size(138, 22)
        Me.lblGrupyDoKtorychNalezy.Text = "Grupy, do których nale¿y"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'txtBiezace
        '
        Me.txtBiezace.Enabled = False
        Me.txtBiezace.Name = "txtBiezace"
        Me.txtBiezace.Size = New System.Drawing.Size(100, 25)
        '
        'btnBiezace
        '
        Me.btnBiezace.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnBiezace.Enabled = False
        Me.btnBiezace.ForeColor = System.Drawing.Color.White
        Me.btnBiezace.Image = CType(resources.GetObject("btnBiezace.Image"), System.Drawing.Image)
        Me.btnBiezace.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnBiezace.Name = "btnBiezace"
        Me.btnBiezace.Size = New System.Drawing.Size(41, 22)
        Me.btnBiezace.Text = "Filtruj"
        '
        'scPrawy
        '
        Me.scPrawy.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scPrawy.IsSplitterFixed = True
        Me.scPrawy.Location = New System.Drawing.Point(0, 0)
        Me.scPrawy.Name = "scPrawy"
        '
        'scPrawy.Panel1
        '
        Me.scPrawy.Panel1.BackColor = System.Drawing.Color.White
        Me.scPrawy.Panel1.Controls.Add(Me.btnUsun)
        Me.scPrawy.Panel1.Controls.Add(Me.btnDodaj)
        '
        'scPrawy.Panel2
        '
        Me.scPrawy.Panel2.Controls.Add(Me.dgvDostepne)
        Me.scPrawy.Panel2.Controls.Add(Me.tsPrawy)
        Me.scPrawy.Size = New System.Drawing.Size(417, 333)
        Me.scPrawy.SplitterDistance = 78
        Me.scPrawy.TabIndex = 0
        '
        'btnUsun
        '
        Me.btnUsun.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnUsun.ForeColor = System.Drawing.Color.White
        Me.btnUsun.Location = New System.Drawing.Point(3, 178)
        Me.btnUsun.Name = "btnUsun"
        Me.btnUsun.Size = New System.Drawing.Size(75, 23)
        Me.btnUsun.TabIndex = 1
        Me.btnUsun.Text = "Usuñ >>"
        Me.btnUsun.UseVisualStyleBackColor = False
        '
        'btnDodaj
        '
        Me.btnDodaj.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnDodaj.ForeColor = System.Drawing.Color.White
        Me.btnDodaj.Location = New System.Drawing.Point(3, 149)
        Me.btnDodaj.Name = "btnDodaj"
        Me.btnDodaj.Size = New System.Drawing.Size(75, 23)
        Me.btnDodaj.TabIndex = 0
        Me.btnDodaj.Text = "<< Dodaj"
        Me.btnDodaj.UseVisualStyleBackColor = False
        '
        'dgvDostepne
        '
        Me.dgvDostepne.AllowUserToAddRows = False
        Me.dgvDostepne.AllowUserToResizeRows = False
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.AliceBlue
        Me.dgvDostepne.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvDostepne.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgvDostepne.BackgroundColor = System.Drawing.Color.White
        Me.dgvDostepne.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDostepne.ColumnHeadersVisible = False
        Me.dgvDostepne.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvDostepne.Location = New System.Drawing.Point(0, 25)
        Me.dgvDostepne.MultiSelect = False
        Me.dgvDostepne.Name = "dgvDostepne"
        Me.dgvDostepne.ReadOnly = True
        Me.dgvDostepne.RowHeadersVisible = False
        Me.dgvDostepne.Size = New System.Drawing.Size(335, 308)
        Me.dgvDostepne.TabIndex = 1
        '
        'tsPrawy
        '
        Me.tsPrawy.BackColor = System.Drawing.Color.DodgerBlue
        Me.tsPrawy.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblDostepneGrupy, Me.ToolStripSeparator1, Me.txtDostepne, Me.btnDostepne})
        Me.tsPrawy.Location = New System.Drawing.Point(0, 0)
        Me.tsPrawy.Name = "tsPrawy"
        Me.tsPrawy.Size = New System.Drawing.Size(335, 25)
        Me.tsPrawy.TabIndex = 0
        Me.tsPrawy.Text = "ToolStrip1"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'txtDostepne
        '
        Me.txtDostepne.Enabled = False
        Me.txtDostepne.Name = "txtDostepne"
        Me.txtDostepne.Size = New System.Drawing.Size(100, 25)
        '
        'btnDostepne
        '
        Me.btnDostepne.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnDostepne.Enabled = False
        Me.btnDostepne.ForeColor = System.Drawing.Color.White
        Me.btnDostepne.Image = CType(resources.GetObject("btnDostepne.Image"), System.Drawing.Image)
        Me.btnDostepne.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnDostepne.Name = "btnDostepne"
        Me.btnDostepne.Size = New System.Drawing.Size(41, 22)
        Me.btnDostepne.Text = "Filtruj"
        '
        'btnAnuluj
        '
        Me.btnAnuluj.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnAnuluj.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnAnuluj.ForeColor = System.Drawing.Color.White
        Me.btnAnuluj.Location = New System.Drawing.Point(666, 2)
        Me.btnAnuluj.Name = "btnAnuluj"
        Me.btnAnuluj.Size = New System.Drawing.Size(75, 23)
        Me.btnAnuluj.TabIndex = 2
        Me.btnAnuluj.Text = "Anuluj"
        Me.btnAnuluj.UseVisualStyleBackColor = False
        '
        'scPoziomy
        '
        Me.scPoziomy.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scPoziomy.IsSplitterFixed = True
        Me.scPoziomy.Location = New System.Drawing.Point(0, 0)
        Me.scPoziomy.Name = "scPoziomy"
        Me.scPoziomy.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'scPoziomy.Panel1
        '
        Me.scPoziomy.Panel1.Controls.Add(Me.scLewy)
        '
        'scPoziomy.Panel2
        '
        Me.scPoziomy.Panel2.BackColor = System.Drawing.Color.White
        Me.scPoziomy.Panel2.Controls.Add(Me.btnAnuluj)
        Me.scPoziomy.Panel2.Controls.Add(Me.btnOk)
        Me.scPoziomy.Size = New System.Drawing.Size(742, 362)
        Me.scPoziomy.SplitterDistance = 333
        Me.scPoziomy.TabIndex = 2
        '
        'btnOk
        '
        Me.btnOk.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnOk.ForeColor = System.Drawing.Color.White
        Me.btnOk.Location = New System.Drawing.Point(585, 2)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(75, 23)
        Me.btnOk.TabIndex = 1
        Me.btnOk.Text = "OK"
        Me.btnOk.UseVisualStyleBackColor = False
        '
        'frmUzytkownikGrupy
        '
        Me.AcceptButton = Me.btnOk
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnAnuluj
        Me.ClientSize = New System.Drawing.Size(742, 362)
        Me.Controls.Add(Me.scPoziomy)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmUzytkownikGrupy"
        Me.scLewy.Panel1.ResumeLayout(False)
        Me.scLewy.Panel1.PerformLayout()
        Me.scLewy.Panel2.ResumeLayout(False)
        CType(Me.scLewy, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scLewy.ResumeLayout(False)
        CType(Me.dgvBiezace, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tsLewy.ResumeLayout(False)
        Me.tsLewy.PerformLayout()
        Me.scPrawy.Panel1.ResumeLayout(False)
        Me.scPrawy.Panel2.ResumeLayout(False)
        Me.scPrawy.Panel2.PerformLayout()
        CType(Me.scPrawy, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scPrawy.ResumeLayout(False)
        CType(Me.dgvDostepne, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tsPrawy.ResumeLayout(False)
        Me.tsPrawy.PerformLayout()
        Me.scPoziomy.Panel1.ResumeLayout(False)
        Me.scPoziomy.Panel2.ResumeLayout(False)
        CType(Me.scPoziomy, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scPoziomy.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblDostepneGrupy As System.Windows.Forms.ToolStripLabel
    Friend WithEvents scLewy As System.Windows.Forms.SplitContainer
    Friend WithEvents dgvBiezace As System.Windows.Forms.DataGridView
    Friend WithEvents tsLewy As System.Windows.Forms.ToolStrip
    Friend WithEvents lblGrupyDoKtorychNalezy As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents txtBiezace As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents btnBiezace As System.Windows.Forms.ToolStripButton
    Friend WithEvents scPrawy As System.Windows.Forms.SplitContainer
    Friend WithEvents btnUsun As System.Windows.Forms.Button
    Friend WithEvents btnDodaj As System.Windows.Forms.Button
    Friend WithEvents dgvDostepne As System.Windows.Forms.DataGridView
    Friend WithEvents tsPrawy As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents txtDostepne As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents btnDostepne As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnAnuluj As System.Windows.Forms.Button
    Friend WithEvents scPoziomy As System.Windows.Forms.SplitContainer
    Friend WithEvents btnOk As System.Windows.Forms.Button
End Class
