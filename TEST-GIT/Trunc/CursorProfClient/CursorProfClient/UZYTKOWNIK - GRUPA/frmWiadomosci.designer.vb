<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmWiadomosci
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmWiadomosci))
        Me.ListBoxTytuly = New System.Windows.Forms.ListBox()
        Me.rtxtTresc = New System.Windows.Forms.RichTextBox()
        Me.tsWiadomosci = New System.Windows.Forms.ToolStrip()
        Me.btnDodajWiadomosc = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnUsun = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnOdswiez = New System.Windows.Forms.ToolStripButton()
        Me.lblListaTytulow = New System.Windows.Forms.Label()
        Me.lblTresc = New System.Windows.Forms.Label()
        Me.btnZamknij = New System.Windows.Forms.Button()
        Me.ToolTipTytul = New System.Windows.Forms.ToolTip(Me.components)
        Me.tsWiadomosci.SuspendLayout()
        Me.SuspendLayout()
        '
        'ListBoxTytuly
        '
        Me.ListBoxTytuly.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ListBoxTytuly.FormattingEnabled = True
        Me.ListBoxTytuly.Location = New System.Drawing.Point(12, 44)
        Me.ListBoxTytuly.Name = "ListBoxTytuly"
        Me.ListBoxTytuly.Size = New System.Drawing.Size(207, 316)
        Me.ListBoxTytuly.TabIndex = 3
        '
        'rtxtTresc
        '
        Me.rtxtTresc.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rtxtTresc.Location = New System.Drawing.Point(225, 44)
        Me.rtxtTresc.Name = "rtxtTresc"
        Me.rtxtTresc.ReadOnly = True
        Me.rtxtTresc.Size = New System.Drawing.Size(439, 317)
        Me.rtxtTresc.TabIndex = 4
        Me.rtxtTresc.Text = ""
        '
        'tsWiadomosci
        '
        Me.tsWiadomosci.BackColor = System.Drawing.Color.White
        Me.tsWiadomosci.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnDodajWiadomosc, Me.ToolStripSeparator2, Me.btnUsun, Me.ToolStripSeparator1, Me.btnOdswiez})
        Me.tsWiadomosci.Location = New System.Drawing.Point(0, 0)
        Me.tsWiadomosci.Name = "tsWiadomosci"
        Me.tsWiadomosci.Size = New System.Drawing.Size(676, 25)
        Me.tsWiadomosci.TabIndex = 0
        Me.tsWiadomosci.Text = "ToolStrip1"
        '
        'btnDodajWiadomosc
        '
        Me.btnDodajWiadomosc.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.btnDodajWiadomosc.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnDodajWiadomosc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnDodajWiadomosc.ForeColor = System.Drawing.Color.White
        Me.btnDodajWiadomosc.Image = CType(resources.GetObject("btnDodajWiadomosc.Image"), System.Drawing.Image)
        Me.btnDodajWiadomosc.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnDodajWiadomosc.Name = "btnDodajWiadomosc"
        Me.btnDodajWiadomosc.Size = New System.Drawing.Size(106, 22)
        Me.btnDodajWiadomosc.Text = "Dodaj wiadomość"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'btnUsun
        '
        Me.btnUsun.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.btnUsun.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnUsun.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnUsun.ForeColor = System.Drawing.Color.White
        Me.btnUsun.Image = CType(resources.GetObject("btnUsun.Image"), System.Drawing.Image)
        Me.btnUsun.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnUsun.Name = "btnUsun"
        Me.btnUsun.Size = New System.Drawing.Size(38, 22)
        Me.btnUsun.Text = "Usuń"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripSeparator1.BackColor = System.Drawing.SystemColors.HotTrack
        Me.ToolStripSeparator1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Padding = New System.Windows.Forms.Padding(0, 20, 0, 20)
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'btnOdswiez
        '
        Me.btnOdswiez.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.btnOdswiez.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnOdswiez.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnOdswiez.ForeColor = System.Drawing.Color.White
        Me.btnOdswiez.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnOdswiez.Name = "btnOdswiez"
        Me.btnOdswiez.Size = New System.Drawing.Size(55, 22)
        Me.btnOdswiez.Text = "Odśwież"
        '
        'lblListaTytulow
        '
        Me.lblListaTytulow.AutoSize = True
        Me.lblListaTytulow.ForeColor = System.Drawing.Color.Black
        Me.lblListaTytulow.Location = New System.Drawing.Point(12, 28)
        Me.lblListaTytulow.Name = "lblListaTytulow"
        Me.lblListaTytulow.Size = New System.Drawing.Size(70, 13)
        Me.lblListaTytulow.TabIndex = 1
        Me.lblListaTytulow.Text = "Lista tytułów:"
        '
        'lblTresc
        '
        Me.lblTresc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTresc.AutoSize = True
        Me.lblTresc.ForeColor = System.Drawing.Color.Black
        Me.lblTresc.Location = New System.Drawing.Point(222, 28)
        Me.lblTresc.Name = "lblTresc"
        Me.lblTresc.Size = New System.Drawing.Size(37, 13)
        Me.lblTresc.TabIndex = 2
        Me.lblTresc.Text = "Treść:"
        '
        'btnZamknij
        '
        Me.btnZamknij.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnZamknij.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnZamknij.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnZamknij.ForeColor = System.Drawing.Color.White
        Me.btnZamknij.Location = New System.Drawing.Point(589, 364)
        Me.btnZamknij.Name = "btnZamknij"
        Me.btnZamknij.Size = New System.Drawing.Size(75, 23)
        Me.btnZamknij.TabIndex = 5
        Me.btnZamknij.Text = "Zamknij"
        Me.btnZamknij.UseVisualStyleBackColor = False
        '
        'frmWiadomosci
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnZamknij
        Me.ClientSize = New System.Drawing.Size(676, 391)
        Me.Controls.Add(Me.btnZamknij)
        Me.Controls.Add(Me.lblTresc)
        Me.Controls.Add(Me.lblListaTytulow)
        Me.Controls.Add(Me.tsWiadomosci)
        Me.Controls.Add(Me.rtxtTresc)
        Me.Controls.Add(Me.ListBoxTytuly)
        Me.MinimumSize = New System.Drawing.Size(559, 338)
        Me.Name = "frmWiadomosci"
        Me.Text = "Wiadomości"
        Me.tsWiadomosci.ResumeLayout(False)
        Me.tsWiadomosci.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ListBoxTytuly As System.Windows.Forms.ListBox
    Friend WithEvents rtxtTresc As System.Windows.Forms.RichTextBox
    Friend WithEvents tsWiadomosci As System.Windows.Forms.ToolStrip
    Friend WithEvents btnUsun As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnDodajWiadomosc As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents lblListaTytulow As System.Windows.Forms.Label
    Friend WithEvents lblTresc As System.Windows.Forms.Label
    Friend WithEvents btnZamknij As System.Windows.Forms.Button
    Friend WithEvents btnOdswiez As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolTipTytul As System.Windows.Forms.ToolTip
End Class
