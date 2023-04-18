<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmStan
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmStan))
        Me.btnZamknij = New System.Windows.Forms.Button()
        Me.ctr = New CursorProfClient.ctrStan()
        Me.SuspendLayout()
        '
        'btnZamknij
        '
        Me.btnZamknij.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnZamknij.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnZamknij.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnZamknij.ForeColor = System.Drawing.Color.White
        Me.btnZamknij.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnZamknij.Location = New System.Drawing.Point(1001, 631)
        Me.btnZamknij.Name = "btnZamknij"
        Me.btnZamknij.Size = New System.Drawing.Size(97, 24)
        Me.btnZamknij.TabIndex = 4
        Me.btnZamknij.Text = "Zamknij"
        Me.btnZamknij.UseVisualStyleBackColor = False
        '
        'ctr
        '
        Me.ctr.BackColor = System.Drawing.Color.DodgerBlue
        Me.ctr.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ctr.Location = New System.Drawing.Point(0, 0)
        Me.ctr.Name = "ctr"
        Me.ctr.Size = New System.Drawing.Size(1250, 666)
        Me.ctr.TabIndex = 0
        '
        'frmStan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnZamknij
        Me.ClientSize = New System.Drawing.Size(1250, 666)
        Me.Controls.Add(Me.ctr)
        Me.Controls.Add(Me.btnZamknij)
        Me.MinimumSize = New System.Drawing.Size(1000, 550)
        Me.Name = "frmStan"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Stan"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ctr As CursorProfClient.ctrStan
    Friend WithEvents btnZamknij As System.Windows.Forms.Button
End Class
