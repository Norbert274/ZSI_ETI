<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmKontakt
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
        Me.gbTemat = New System.Windows.Forms.GroupBox()
        Me.txtTemat = New System.Windows.Forms.TextBox()
        Me.gbTresc = New System.Windows.Forms.GroupBox()
        Me.txtTresc = New System.Windows.Forms.TextBox()
        Me.btnWyslijWiadomosc = New System.Windows.Forms.Button()
        Me.btnAnuluj = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.gbTemat.SuspendLayout()
        Me.gbTresc.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'gbTemat
        '
        Me.gbTemat.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbTemat.Controls.Add(Me.txtTemat)
        Me.gbTemat.ForeColor = System.Drawing.Color.Black
        Me.gbTemat.Location = New System.Drawing.Point(12, 95)
        Me.gbTemat.Name = "gbTemat"
        Me.gbTemat.Size = New System.Drawing.Size(647, 93)
        Me.gbTemat.TabIndex = 1
        Me.gbTemat.TabStop = False
        Me.gbTemat.Text = "Temat:"
        '
        'txtTemat
        '
        Me.txtTemat.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTemat.Location = New System.Drawing.Point(6, 17)
        Me.txtTemat.Multiline = True
        Me.txtTemat.Name = "txtTemat"
        Me.txtTemat.Size = New System.Drawing.Size(635, 70)
        Me.txtTemat.TabIndex = 0
        Me.ToolTip.SetToolTip(Me.txtTemat, "Temat wiadomości")
        '
        'gbTresc
        '
        Me.gbTresc.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbTresc.Controls.Add(Me.txtTresc)
        Me.gbTresc.ForeColor = System.Drawing.Color.Black
        Me.gbTresc.Location = New System.Drawing.Point(12, 194)
        Me.gbTresc.Name = "gbTresc"
        Me.gbTresc.Size = New System.Drawing.Size(647, 334)
        Me.gbTresc.TabIndex = 2
        Me.gbTresc.TabStop = False
        Me.gbTresc.Text = "Treść wiadomości:"
        '
        'txtTresc
        '
        Me.txtTresc.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTresc.Location = New System.Drawing.Point(6, 19)
        Me.txtTresc.Multiline = True
        Me.txtTresc.Name = "txtTresc"
        Me.txtTresc.Size = New System.Drawing.Size(635, 309)
        Me.txtTresc.TabIndex = 0
        Me.ToolTip.SetToolTip(Me.txtTresc, "Treść wiadomości")
        '
        'btnWyslijWiadomosc
        '
        Me.btnWyslijWiadomosc.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnWyslijWiadomosc.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnWyslijWiadomosc.ForeColor = System.Drawing.Color.White
        Me.btnWyslijWiadomosc.Location = New System.Drawing.Point(460, 528)
        Me.btnWyslijWiadomosc.Name = "btnWyslijWiadomosc"
        Me.btnWyslijWiadomosc.Size = New System.Drawing.Size(108, 23)
        Me.btnWyslijWiadomosc.TabIndex = 3
        Me.btnWyslijWiadomosc.Text = "Wyślij wiadomość"
        Me.btnWyslijWiadomosc.UseVisualStyleBackColor = False
        '
        'btnAnuluj
        '
        Me.btnAnuluj.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAnuluj.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnAnuluj.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnAnuluj.ForeColor = System.Drawing.Color.White
        Me.btnAnuluj.Location = New System.Drawing.Point(574, 528)
        Me.btnAnuluj.Name = "btnAnuluj"
        Me.btnAnuluj.Size = New System.Drawing.Size(86, 23)
        Me.btnAnuluj.TabIndex = 4
        Me.btnAnuluj.Text = "Anuluj"
        Me.btnAnuluj.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(17, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(539, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "W sprawach związanych z zamawianiem materiałów POS za pośrednictwem strony oraz z" & _
    "głaszaniem reklamacji, "
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(17, 47)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(465, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Użytkownicy mogą kontaktować się poprzez ten formularz, klikając przycisk ""Wyślij" & _
    " wiadomość"", "
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 7)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(647, 82)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Informacja"
        '
        'frmKontakt
        '
        Me.AcceptButton = Me.btnWyslijWiadomosc
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnAnuluj
        Me.ClientSize = New System.Drawing.Size(671, 559)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnAnuluj)
        Me.Controls.Add(Me.btnWyslijWiadomosc)
        Me.Controls.Add(Me.gbTresc)
        Me.Controls.Add(Me.gbTemat)
        Me.Name = "frmKontakt"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Kontakt"
        Me.gbTemat.ResumeLayout(False)
        Me.gbTemat.PerformLayout()
        Me.gbTresc.ResumeLayout(False)
        Me.gbTresc.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gbTemat As System.Windows.Forms.GroupBox
    Friend WithEvents txtTemat As System.Windows.Forms.TextBox
    Friend WithEvents gbTresc As System.Windows.Forms.GroupBox
    Friend WithEvents txtTresc As System.Windows.Forms.TextBox
    Friend WithEvents btnWyslijWiadomosc As System.Windows.Forms.Button
    Friend WithEvents btnAnuluj As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
End Class
