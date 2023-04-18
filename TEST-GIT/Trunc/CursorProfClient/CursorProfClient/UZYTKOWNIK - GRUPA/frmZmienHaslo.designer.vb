<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmZmienHaslo
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
        Me.lblNoweHaslo = New System.Windows.Forms.Label()
        Me.txtNoweHaslo = New System.Windows.Forms.TextBox()
        Me.lblStareHaslo = New System.Windows.Forms.Label()
        Me.txtObecneHaslo = New System.Windows.Forms.TextBox()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnAnuluj = New System.Windows.Forms.Button()
        Me.txtPotwierdzHaslo = New System.Windows.Forms.TextBox()
        Me.lblPotwierdzHaslo = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lblNoweHaslo
        '
        Me.lblNoweHaslo.AutoSize = True
        Me.lblNoweHaslo.ForeColor = System.Drawing.Color.Black
        Me.lblNoweHaslo.Location = New System.Drawing.Point(12, 41)
        Me.lblNoweHaslo.Name = "lblNoweHaslo"
        Me.lblNoweHaslo.Size = New System.Drawing.Size(68, 13)
        Me.lblNoweHaslo.TabIndex = 2
        Me.lblNoweHaslo.Text = "Nowe has≥o:"
        '
        'txtNoweHaslo
        '
        Me.txtNoweHaslo.Location = New System.Drawing.Point(133, 38)
        Me.txtNoweHaslo.Name = "txtNoweHaslo"
        Me.txtNoweHaslo.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtNoweHaslo.Size = New System.Drawing.Size(120, 20)
        Me.txtNoweHaslo.TabIndex = 3
        '
        'lblStareHaslo
        '
        Me.lblStareHaslo.AutoSize = True
        Me.lblStareHaslo.ForeColor = System.Drawing.Color.Black
        Me.lblStareHaslo.Location = New System.Drawing.Point(12, 15)
        Me.lblStareHaslo.Name = "lblStareHaslo"
        Me.lblStareHaslo.Size = New System.Drawing.Size(78, 13)
        Me.lblStareHaslo.TabIndex = 0
        Me.lblStareHaslo.Text = "Obecne has≥o:"
        '
        'txtObecneHaslo
        '
        Me.txtObecneHaslo.Location = New System.Drawing.Point(133, 12)
        Me.txtObecneHaslo.Name = "txtObecneHaslo"
        Me.txtObecneHaslo.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtObecneHaslo.Size = New System.Drawing.Size(120, 20)
        Me.txtObecneHaslo.TabIndex = 1
        '
        'btnOK
        '
        Me.btnOK.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnOK.ForeColor = System.Drawing.Color.White
        Me.btnOK.Location = New System.Drawing.Point(95, 90)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 6
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = False
        '
        'btnAnuluj
        '
        Me.btnAnuluj.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnAnuluj.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnAnuluj.Enabled = False
        Me.btnAnuluj.ForeColor = System.Drawing.Color.White
        Me.btnAnuluj.Location = New System.Drawing.Point(176, 90)
        Me.btnAnuluj.Name = "btnAnuluj"
        Me.btnAnuluj.Size = New System.Drawing.Size(75, 23)
        Me.btnAnuluj.TabIndex = 7
        Me.btnAnuluj.Text = "Anuluj"
        Me.btnAnuluj.UseVisualStyleBackColor = False
        '
        'txtPotwierdzHaslo
        '
        Me.txtPotwierdzHaslo.Location = New System.Drawing.Point(133, 64)
        Me.txtPotwierdzHaslo.Name = "txtPotwierdzHaslo"
        Me.txtPotwierdzHaslo.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPotwierdzHaslo.Size = New System.Drawing.Size(120, 20)
        Me.txtPotwierdzHaslo.TabIndex = 5
        '
        'lblPotwierdzHaslo
        '
        Me.lblPotwierdzHaslo.AutoSize = True
        Me.lblPotwierdzHaslo.ForeColor = System.Drawing.Color.Black
        Me.lblPotwierdzHaslo.Location = New System.Drawing.Point(12, 67)
        Me.lblPotwierdzHaslo.Name = "lblPotwierdzHaslo"
        Me.lblPotwierdzHaslo.Size = New System.Drawing.Size(115, 13)
        Me.lblPotwierdzHaslo.TabIndex = 4
        Me.lblPotwierdzHaslo.Text = "Potwierdü nowe has≥o:"
        '
        'frmZmienHaslo
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnAnuluj
        Me.ClientSize = New System.Drawing.Size(263, 119)
        Me.Controls.Add(Me.lblPotwierdzHaslo)
        Me.Controls.Add(Me.txtPotwierdzHaslo)
        Me.Controls.Add(Me.btnAnuluj)
        Me.Controls.Add(Me.lblNoweHaslo)
        Me.Controls.Add(Me.txtNoweHaslo)
        Me.Controls.Add(Me.lblStareHaslo)
        Me.Controls.Add(Me.txtObecneHaslo)
        Me.Controls.Add(Me.btnOK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Name = "frmZmienHaslo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Zmiana has≥a"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblNoweHaslo As System.Windows.Forms.Label
    Friend WithEvents txtNoweHaslo As System.Windows.Forms.TextBox
    Friend WithEvents lblStareHaslo As System.Windows.Forms.Label
    Friend WithEvents txtObecneHaslo As System.Windows.Forms.TextBox
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnAnuluj As System.Windows.Forms.Button
    Friend WithEvents txtPotwierdzHaslo As System.Windows.Forms.TextBox
    Friend WithEvents lblPotwierdzHaslo As System.Windows.Forms.Label

End Class
