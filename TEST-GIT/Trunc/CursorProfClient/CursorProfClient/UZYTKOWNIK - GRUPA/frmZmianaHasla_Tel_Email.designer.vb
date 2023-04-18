<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmZmianaHasla_Tel_Email
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
        Me.lblPotwierdzHaslo = New System.Windows.Forms.Label()
        Me.txtPotwierdzHaslo = New System.Windows.Forms.TextBox()
        Me.btnAnuluj = New System.Windows.Forms.Button()
        Me.lblNoweHaslo = New System.Windows.Forms.Label()
        Me.txtNoweHaslo = New System.Windows.Forms.TextBox()
        Me.lblStareHaslo = New System.Windows.Forms.Label()
        Me.txtObecneHaslo = New System.Windows.Forms.TextBox()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.lblEmail = New System.Windows.Forms.Label()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.lblTelefon = New System.Windows.Forms.Label()
        Me.txtTelefon = New System.Windows.Forms.TextBox()
        Me.gbTelefon_EMail = New System.Windows.Forms.GroupBox()
        Me.gbHaslo = New System.Windows.Forms.GroupBox()
        Me.gbTelefon_EMail.SuspendLayout()
        Me.gbHaslo.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblPotwierdzHaslo
        '
        Me.lblPotwierdzHaslo.AutoSize = True
        Me.lblPotwierdzHaslo.ForeColor = System.Drawing.Color.Black
        Me.lblPotwierdzHaslo.Location = New System.Drawing.Point(18, 74)
        Me.lblPotwierdzHaslo.Name = "lblPotwierdzHaslo"
        Me.lblPotwierdzHaslo.Size = New System.Drawing.Size(115, 13)
        Me.lblPotwierdzHaslo.TabIndex = 4
        Me.lblPotwierdzHaslo.Text = "Potwierdź nowe hasło:"
        '
        'txtPotwierdzHaslo
        '
        Me.txtPotwierdzHaslo.Location = New System.Drawing.Point(139, 71)
        Me.txtPotwierdzHaslo.Name = "txtPotwierdzHaslo"
        Me.txtPotwierdzHaslo.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPotwierdzHaslo.Size = New System.Drawing.Size(144, 20)
        Me.txtPotwierdzHaslo.TabIndex = 5
        '
        'btnAnuluj
        '
        Me.btnAnuluj.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnAnuluj.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnAnuluj.ForeColor = System.Drawing.Color.White
        Me.btnAnuluj.Location = New System.Drawing.Point(236, 211)
        Me.btnAnuluj.Name = "btnAnuluj"
        Me.btnAnuluj.Size = New System.Drawing.Size(75, 23)
        Me.btnAnuluj.TabIndex = 3
        Me.btnAnuluj.Text = "Anuluj"
        Me.btnAnuluj.UseVisualStyleBackColor = False
        '
        'lblNoweHaslo
        '
        Me.lblNoweHaslo.AutoSize = True
        Me.lblNoweHaslo.ForeColor = System.Drawing.Color.Black
        Me.lblNoweHaslo.Location = New System.Drawing.Point(18, 48)
        Me.lblNoweHaslo.Name = "lblNoweHaslo"
        Me.lblNoweHaslo.Size = New System.Drawing.Size(68, 13)
        Me.lblNoweHaslo.TabIndex = 2
        Me.lblNoweHaslo.Text = "Nowe hasło:"
        '
        'txtNoweHaslo
        '
        Me.txtNoweHaslo.Location = New System.Drawing.Point(139, 45)
        Me.txtNoweHaslo.Name = "txtNoweHaslo"
        Me.txtNoweHaslo.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtNoweHaslo.Size = New System.Drawing.Size(144, 20)
        Me.txtNoweHaslo.TabIndex = 3
        '
        'lblStareHaslo
        '
        Me.lblStareHaslo.AutoSize = True
        Me.lblStareHaslo.ForeColor = System.Drawing.Color.Black
        Me.lblStareHaslo.Location = New System.Drawing.Point(18, 22)
        Me.lblStareHaslo.Name = "lblStareHaslo"
        Me.lblStareHaslo.Size = New System.Drawing.Size(78, 13)
        Me.lblStareHaslo.TabIndex = 0
        Me.lblStareHaslo.Text = "Obecne hasło:"
        '
        'txtObecneHaslo
        '
        Me.txtObecneHaslo.Location = New System.Drawing.Point(139, 19)
        Me.txtObecneHaslo.Name = "txtObecneHaslo"
        Me.txtObecneHaslo.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtObecneHaslo.Size = New System.Drawing.Size(144, 20)
        Me.txtObecneHaslo.TabIndex = 1
        '
        'btnOK
        '
        Me.btnOK.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnOK.ForeColor = System.Drawing.Color.White
        Me.btnOK.Location = New System.Drawing.Point(155, 211)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 2
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = False
        '
        'lblEmail
        '
        Me.lblEmail.AutoSize = True
        Me.lblEmail.ForeColor = System.Drawing.Color.Black
        Me.lblEmail.Location = New System.Drawing.Point(48, 48)
        Me.lblEmail.Name = "lblEmail"
        Me.lblEmail.Size = New System.Drawing.Size(35, 13)
        Me.lblEmail.TabIndex = 2
        Me.lblEmail.Text = "Email:"
        '
        'txtEmail
        '
        Me.txtEmail.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmail.Location = New System.Drawing.Point(101, 45)
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(182, 20)
        Me.txtEmail.TabIndex = 3
        '
        'lblTelefon
        '
        Me.lblTelefon.AutoSize = True
        Me.lblTelefon.ForeColor = System.Drawing.Color.Black
        Me.lblTelefon.Location = New System.Drawing.Point(37, 22)
        Me.lblTelefon.Name = "lblTelefon"
        Me.lblTelefon.Size = New System.Drawing.Size(46, 13)
        Me.lblTelefon.TabIndex = 0
        Me.lblTelefon.Text = "Telefon:"
        '
        'txtTelefon
        '
        Me.txtTelefon.Location = New System.Drawing.Point(101, 19)
        Me.txtTelefon.MaxLength = 9
        Me.txtTelefon.Name = "txtTelefon"
        Me.txtTelefon.Size = New System.Drawing.Size(182, 20)
        Me.txtTelefon.TabIndex = 1
        '
        'gbTelefon_EMail
        '
        Me.gbTelefon_EMail.Controls.Add(Me.lblEmail)
        Me.gbTelefon_EMail.Controls.Add(Me.txtTelefon)
        Me.gbTelefon_EMail.Controls.Add(Me.txtEmail)
        Me.gbTelefon_EMail.Controls.Add(Me.lblTelefon)
        Me.gbTelefon_EMail.ForeColor = System.Drawing.Color.Black
        Me.gbTelefon_EMail.Location = New System.Drawing.Point(12, 128)
        Me.gbTelefon_EMail.Name = "gbTelefon_EMail"
        Me.gbTelefon_EMail.Size = New System.Drawing.Size(299, 77)
        Me.gbTelefon_EMail.TabIndex = 1
        Me.gbTelefon_EMail.TabStop = False
        Me.gbTelefon_EMail.Text = "Dane obowiązkowe"
        '
        'gbHaslo
        '
        Me.gbHaslo.Controls.Add(Me.lblPotwierdzHaslo)
        Me.gbHaslo.Controls.Add(Me.txtObecneHaslo)
        Me.gbHaslo.Controls.Add(Me.lblStareHaslo)
        Me.gbHaslo.Controls.Add(Me.txtPotwierdzHaslo)
        Me.gbHaslo.Controls.Add(Me.txtNoweHaslo)
        Me.gbHaslo.Controls.Add(Me.lblNoweHaslo)
        Me.gbHaslo.ForeColor = System.Drawing.Color.Black
        Me.gbHaslo.Location = New System.Drawing.Point(12, 12)
        Me.gbHaslo.Name = "gbHaslo"
        Me.gbHaslo.Size = New System.Drawing.Size(299, 110)
        Me.gbHaslo.TabIndex = 0
        Me.gbHaslo.TabStop = False
        Me.gbHaslo.Text = "Hasło"
        '
        'frmZmianaHasla_Tel_Email
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnAnuluj
        Me.ClientSize = New System.Drawing.Size(323, 242)
        Me.Controls.Add(Me.gbHaslo)
        Me.Controls.Add(Me.gbTelefon_EMail)
        Me.Controls.Add(Me.btnAnuluj)
        Me.Controls.Add(Me.btnOK)
        Me.MaximumSize = New System.Drawing.Size(339, 280)
        Me.MinimumSize = New System.Drawing.Size(339, 280)
        Me.Name = "frmZmianaHasla_Tel_Email"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Zmiana Hasla "
        Me.gbTelefon_EMail.ResumeLayout(False)
        Me.gbTelefon_EMail.PerformLayout()
        Me.gbHaslo.ResumeLayout(False)
        Me.gbHaslo.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblPotwierdzHaslo As System.Windows.Forms.Label
    Friend WithEvents txtPotwierdzHaslo As System.Windows.Forms.TextBox
    Friend WithEvents btnAnuluj As System.Windows.Forms.Button
    Friend WithEvents lblNoweHaslo As System.Windows.Forms.Label
    Friend WithEvents txtNoweHaslo As System.Windows.Forms.TextBox
    Friend WithEvents lblStareHaslo As System.Windows.Forms.Label
    Friend WithEvents txtObecneHaslo As System.Windows.Forms.TextBox
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents lblEmail As System.Windows.Forms.Label
    Friend WithEvents txtEmail As System.Windows.Forms.TextBox
    Friend WithEvents lblTelefon As System.Windows.Forms.Label
    Friend WithEvents txtTelefon As System.Windows.Forms.TextBox
    Friend WithEvents gbTelefon_EMail As System.Windows.Forms.GroupBox
    Friend WithEvents gbHaslo As System.Windows.Forms.GroupBox
End Class
