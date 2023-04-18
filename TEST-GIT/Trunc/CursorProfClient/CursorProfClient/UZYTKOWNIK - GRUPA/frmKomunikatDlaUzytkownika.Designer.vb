<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmKomunikatDlaUzytkownika
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
        Me.rtbKomunikat = New System.Windows.Forms.RichTextBox()
        Me.btnZamknij = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'rtbKomunikat
        '
        Me.rtbKomunikat.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rtbKomunikat.BackColor = System.Drawing.Color.White
        Me.rtbKomunikat.Location = New System.Drawing.Point(5, 3)
        Me.rtbKomunikat.MaximumSize = New System.Drawing.Size(675, 2065)
        Me.rtbKomunikat.MinimumSize = New System.Drawing.Size(675, 420)
        Me.rtbKomunikat.Name = "rtbKomunikat"
        Me.rtbKomunikat.ReadOnly = True
        Me.rtbKomunikat.Size = New System.Drawing.Size(675, 420)
        Me.rtbKomunikat.TabIndex = 0
        Me.rtbKomunikat.Text = ""
        '
        'btnZamknij
        '
        Me.btnZamknij.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnZamknij.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnZamknij.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnZamknij.ForeColor = System.Drawing.Color.White
        Me.btnZamknij.Location = New System.Drawing.Point(582, 426)
        Me.btnZamknij.Name = "btnZamknij"
        Me.btnZamknij.Size = New System.Drawing.Size(97, 23)
        Me.btnZamknij.TabIndex = 1
        Me.btnZamknij.Text = "Zamknij"
        Me.btnZamknij.UseVisualStyleBackColor = False
        '
        'frmKomunikatDlaUzytkownika
        '
        Me.AcceptButton = Me.btnZamknij
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnZamknij
        Me.ClientSize = New System.Drawing.Size(683, 452)
        Me.Controls.Add(Me.btnZamknij)
        Me.Controls.Add(Me.rtbKomunikat)
        Me.MinimumSize = New System.Drawing.Size(699, 490)
        Me.Name = "frmKomunikatDlaUzytkownika"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Komunikat"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents rtbKomunikat As System.Windows.Forms.RichTextBox
    Friend WithEvents btnZamknij As System.Windows.Forms.Button
End Class
