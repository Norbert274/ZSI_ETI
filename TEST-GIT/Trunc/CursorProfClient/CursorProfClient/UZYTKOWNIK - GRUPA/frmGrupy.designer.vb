<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGrupy
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGrupy))
        Me.tsGorny = New System.Windows.Forms.ToolStrip()
        Me.btnNowyPodrzedny = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnEdytujGrupe = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnUsun = New System.Windows.Forms.ToolStripButton()
        Me.tv = New System.Windows.Forms.TreeView()
        Me.tsDolny = New System.Windows.Forms.ToolStrip()
        Me.btnOdswiez = New System.Windows.Forms.ToolStripButton()
        Me.btnAnuluj = New System.Windows.Forms.Button()
        Me.tsGorny.SuspendLayout()
        Me.tsDolny.SuspendLayout()
        Me.SuspendLayout()
        '
        'tsGorny
        '
        Me.tsGorny.BackColor = System.Drawing.Color.White
        Me.tsGorny.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNowyPodrzedny, Me.ToolStripSeparator2, Me.btnEdytujGrupe, Me.ToolStripSeparator1, Me.btnUsun})
        Me.tsGorny.Location = New System.Drawing.Point(0, 0)
        Me.tsGorny.Name = "tsGorny"
        Me.tsGorny.Size = New System.Drawing.Size(401, 25)
        Me.tsGorny.TabIndex = 0
        Me.tsGorny.Text = "ToolStrip3"
        '
        'btnNowyPodrzedny
        '
        Me.btnNowyPodrzedny.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnNowyPodrzedny.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnNowyPodrzedny.ForeColor = System.Drawing.Color.White
        Me.btnNowyPodrzedny.Image = CType(resources.GetObject("btnNowyPodrzedny.Image"), System.Drawing.Image)
        Me.btnNowyPodrzedny.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnNowyPodrzedny.Name = "btnNowyPodrzedny"
        Me.btnNowyPodrzedny.Size = New System.Drawing.Size(76, 22)
        Me.btnNowyPodrzedny.Text = "Dodaj grupê"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'btnEdytujGrupe
        '
        Me.btnEdytujGrupe.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnEdytujGrupe.ForeColor = System.Drawing.Color.White
        Me.btnEdytujGrupe.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnEdytujGrupe.Name = "btnEdytujGrupe"
        Me.btnEdytujGrupe.Size = New System.Drawing.Size(78, 22)
        Me.btnEdytujGrupe.Text = "Edytuj grupê"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'btnUsun
        '
        Me.btnUsun.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnUsun.ForeColor = System.Drawing.Color.White
        Me.btnUsun.Image = CType(resources.GetObject("btnUsun.Image"), System.Drawing.Image)
        Me.btnUsun.Name = "btnUsun"
        Me.btnUsun.RightToLeftAutoMirrorImage = True
        Me.btnUsun.Size = New System.Drawing.Size(54, 22)
        Me.btnUsun.Text = "Usuñ"
        '
        'tv
        '
        Me.tv.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tv.Location = New System.Drawing.Point(0, 28)
        Me.tv.Name = "tv"
        Me.tv.Size = New System.Drawing.Size(400, 356)
        Me.tv.TabIndex = 1
        '
        'tsDolny
        '
        Me.tsDolny.BackColor = System.Drawing.Color.White
        Me.tsDolny.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.tsDolny.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnOdswiez})
        Me.tsDolny.Location = New System.Drawing.Point(0, 388)
        Me.tsDolny.Name = "tsDolny"
        Me.tsDolny.Size = New System.Drawing.Size(401, 25)
        Me.tsDolny.TabIndex = 2
        Me.tsDolny.Text = "ToolStrip1"
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
        Me.btnAnuluj.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnAnuluj.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnAnuluj.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnAnuluj.ForeColor = System.Drawing.Color.White
        Me.btnAnuluj.Location = New System.Drawing.Point(12, 388)
        Me.btnAnuluj.Name = "btnAnuluj"
        Me.btnAnuluj.Size = New System.Drawing.Size(75, 23)
        Me.btnAnuluj.TabIndex = 11
        Me.btnAnuluj.Text = "Anuluj"
        Me.btnAnuluj.UseVisualStyleBackColor = False
        '
        'frmGrupy
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnAnuluj
        Me.ClientSize = New System.Drawing.Size(401, 413)
        Me.Controls.Add(Me.tsDolny)
        Me.Controls.Add(Me.tv)
        Me.Controls.Add(Me.tsGorny)
        Me.Controls.Add(Me.btnAnuluj)
        Me.MinimumSize = New System.Drawing.Size(408, 302)
        Me.Name = "frmGrupy"
        Me.Text = "Grupy"
        Me.tsGorny.ResumeLayout(False)
        Me.tsGorny.PerformLayout()
        Me.tsDolny.ResumeLayout(False)
        Me.tsDolny.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tsGorny As System.Windows.Forms.ToolStrip
    Friend WithEvents btnUsun As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnNowyPodrzedny As System.Windows.Forms.ToolStripButton
    Friend WithEvents tv As System.Windows.Forms.TreeView
    Friend WithEvents tsDolny As System.Windows.Forms.ToolStrip
    Friend WithEvents btnOdswiez As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnEdytujGrupe As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnAnuluj As System.Windows.Forms.Button
End Class
