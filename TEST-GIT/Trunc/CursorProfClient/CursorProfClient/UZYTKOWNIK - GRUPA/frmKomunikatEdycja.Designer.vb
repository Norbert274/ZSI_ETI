<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmKomunikatEdycja
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
        Me.btnZamknij = New System.Windows.Forms.Button()
        Me.btnZapisz = New System.Windows.Forms.Button()
        Me.ctrEdytorKomunikatu = New CursorProfClient.ctrEdytorTekstu()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.SuspendLayout()
        '
        'btnZamknij
        '
        Me.btnZamknij.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnZamknij.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnZamknij.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnZamknij.ForeColor = System.Drawing.Color.White
        Me.btnZamknij.Location = New System.Drawing.Point(424, 414)
        Me.btnZamknij.Name = "btnZamknij"
        Me.btnZamknij.Size = New System.Drawing.Size(95, 23)
        Me.btnZamknij.TabIndex = 1
        Me.btnZamknij.Text = "Zamknij"
        Me.ToolTip1.SetToolTip(Me.btnZamknij, "Zamknięcie okna")
        Me.btnZamknij.UseVisualStyleBackColor = False
        '
        'btnZapisz
        '
        Me.btnZapisz.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnZapisz.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnZapisz.ForeColor = System.Drawing.Color.White
        Me.btnZapisz.Location = New System.Drawing.Point(290, 414)
        Me.btnZapisz.Name = "btnZapisz"
        Me.btnZapisz.Size = New System.Drawing.Size(128, 23)
        Me.btnZapisz.TabIndex = 2
        Me.btnZapisz.Text = "Zapisz komunikat"
        Me.ToolTip1.SetToolTip(Me.btnZapisz, "Zapisanie komunikatu")
        Me.btnZapisz.UseVisualStyleBackColor = False
        '
        'ctrEdytorKomunikatu
        '
        Me.ctrEdytorKomunikatu.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ctrEdytorKomunikatu.BackColor = System.Drawing.Color.White
        Me.ctrEdytorKomunikatu.Location = New System.Drawing.Point(4, 4)
        Me.ctrEdytorKomunikatu.Name = "ctrEdytorKomunikatu"
        Me.ctrEdytorKomunikatu.Size = New System.Drawing.Size(523, 404)
        Me.ctrEdytorKomunikatu.TabIndex = 0
        '
        'ToolTip1
        '
        Me.ToolTip1.AutoPopDelay = 10000
        Me.ToolTip1.InitialDelay = 500
        Me.ToolTip1.ReshowDelay = 500
        '
        'frmKomunikatEdycja
        '
        Me.AcceptButton = Me.btnZapisz
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnZamknij
        Me.ClientSize = New System.Drawing.Size(530, 442)
        Me.Controls.Add(Me.ctrEdytorKomunikatu)
        Me.Controls.Add(Me.btnZapisz)
        Me.Controls.Add(Me.btnZamknij)
        Me.MaximumSize = New System.Drawing.Size(716, 606)
        Me.MinimumSize = New System.Drawing.Size(546, 480)
        Me.Name = "frmKomunikatEdycja"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Edytowanie komunikatu"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnZamknij As System.Windows.Forms.Button
    Friend WithEvents btnZapisz As System.Windows.Forms.Button
    Friend WithEvents ctrEdytorKomunikatu As CursorProfClient.ctrEdytorTekstu
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class
