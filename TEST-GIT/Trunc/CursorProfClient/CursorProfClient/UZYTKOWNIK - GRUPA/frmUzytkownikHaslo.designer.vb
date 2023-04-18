<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUzytkownikHaslo
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
        Me.lblHaslo = New System.Windows.Forms.Label()
        Me.txtHaslo = New System.Windows.Forms.TextBox()
        Me.txtHaslo2 = New System.Windows.Forms.TextBox()
        Me.lblHaslo2 = New System.Windows.Forms.Label()
        Me.btnOk = New System.Windows.Forms.Button()
        Me.btnAnuluj = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lblHaslo
        '
        Me.lblHaslo.AutoSize = True
        Me.lblHaslo.ForeColor = System.Drawing.Color.Black
        Me.lblHaslo.Location = New System.Drawing.Point(12, 15)
        Me.lblHaslo.Name = "lblHaslo"
        Me.lblHaslo.Size = New System.Drawing.Size(39, 13)
        Me.lblHaslo.TabIndex = 0
        Me.lblHaslo.Text = "Has³o:"
        '
        'txtHaslo
        '
        Me.txtHaslo.Location = New System.Drawing.Point(112, 12)
        Me.txtHaslo.Name = "txtHaslo"
        Me.txtHaslo.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtHaslo.Size = New System.Drawing.Size(168, 20)
        Me.txtHaslo.TabIndex = 1
        '
        'txtHaslo2
        '
        Me.txtHaslo2.Location = New System.Drawing.Point(112, 38)
        Me.txtHaslo2.Name = "txtHaslo2"
        Me.txtHaslo2.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtHaslo2.Size = New System.Drawing.Size(168, 20)
        Me.txtHaslo2.TabIndex = 3
        '
        'lblHaslo2
        '
        Me.lblHaslo2.AutoSize = True
        Me.lblHaslo2.ForeColor = System.Drawing.Color.Black
        Me.lblHaslo2.Location = New System.Drawing.Point(12, 41)
        Me.lblHaslo2.Name = "lblHaslo2"
        Me.lblHaslo2.Size = New System.Drawing.Size(94, 13)
        Me.lblHaslo2.TabIndex = 2
        Me.lblHaslo2.Text = "Has³o (ponownie):"
        '
        'btnOk
        '
        Me.btnOk.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnOk.ForeColor = System.Drawing.Color.White
        Me.btnOk.Location = New System.Drawing.Point(124, 64)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(75, 23)
        Me.btnOk.TabIndex = 5
        Me.btnOk.Text = "OK"
        Me.btnOk.UseVisualStyleBackColor = False
        '
        'btnAnuluj
        '
        Me.btnAnuluj.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnAnuluj.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnAnuluj.ForeColor = System.Drawing.Color.White
        Me.btnAnuluj.Location = New System.Drawing.Point(205, 64)
        Me.btnAnuluj.Name = "btnAnuluj"
        Me.btnAnuluj.Size = New System.Drawing.Size(75, 23)
        Me.btnAnuluj.TabIndex = 4
        Me.btnAnuluj.Text = "Anuluj"
        Me.btnAnuluj.UseVisualStyleBackColor = False
        '
        'frmUzytkownikHaslo
        '
        Me.AcceptButton = Me.btnOk
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnAnuluj
        Me.ClientSize = New System.Drawing.Size(292, 93)
        Me.Controls.Add(Me.btnAnuluj)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.lblHaslo2)
        Me.Controls.Add(Me.txtHaslo2)
        Me.Controls.Add(Me.txtHaslo)
        Me.Controls.Add(Me.lblHaslo)
        Me.MinimumSize = New System.Drawing.Size(308, 131)
        Me.Name = "frmUzytkownikHaslo"
        Me.Text = "Ustawianie has³a u¿ytkownika"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblHaslo As System.Windows.Forms.Label
    Friend WithEvents txtHaslo As System.Windows.Forms.TextBox
    Friend WithEvents txtHaslo2 As System.Windows.Forms.TextBox
    Friend WithEvents lblHaslo2 As System.Windows.Forms.Label
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents btnAnuluj As System.Windows.Forms.Button
End Class
