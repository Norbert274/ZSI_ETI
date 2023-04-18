<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmResetHasla
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
        Me.lblLogin = New System.Windows.Forms.Label()
        Me.txtLogin = New System.Windows.Forms.TextBox()
        Me.btnResetujHaslo = New System.Windows.Forms.Button()
        Me.btnAnuluj = New System.Windows.Forms.Button()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.lblHaslo2 = New System.Windows.Forms.Label()
        Me.txtHaslo2 = New System.Windows.Forms.TextBox()
        Me.txtHaslo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblKodResetujacy = New System.Windows.Forms.Label()
        Me.txtKodBezpieczenstwa = New System.Windows.Forms.TextBox()
        Me.btnWyslijKodBezpieczenstwa = New System.Windows.Forms.Button()
        Me.lblInformacja = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lblLogin
        '
        Me.lblLogin.AutoSize = True
        Me.lblLogin.ForeColor = System.Drawing.Color.Black
        Me.lblLogin.Location = New System.Drawing.Point(1, 15)
        Me.lblLogin.Name = "lblLogin"
        Me.lblLogin.Size = New System.Drawing.Size(33, 13)
        Me.lblLogin.TabIndex = 0
        Me.lblLogin.Text = "Login"
        '
        'txtLogin
        '
        Me.txtLogin.ForeColor = System.Drawing.Color.Black
        Me.txtLogin.Location = New System.Drawing.Point(40, 12)
        Me.txtLogin.Name = "txtLogin"
        Me.txtLogin.Size = New System.Drawing.Size(174, 20)
        Me.txtLogin.TabIndex = 0
        '
        'btnResetujHaslo
        '
        Me.btnResetujHaslo.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnResetujHaslo.ForeColor = System.Drawing.Color.White
        Me.btnResetujHaslo.Location = New System.Drawing.Point(58, 167)
        Me.btnResetujHaslo.Name = "btnResetujHaslo"
        Me.btnResetujHaslo.Size = New System.Drawing.Size(75, 23)
        Me.btnResetujHaslo.TabIndex = 5
        Me.btnResetujHaslo.Text = "Resetuj"
        Me.btnResetujHaslo.UseVisualStyleBackColor = False
        '
        'btnAnuluj
        '
        Me.btnAnuluj.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnAnuluj.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnAnuluj.Enabled = False
        Me.btnAnuluj.ForeColor = System.Drawing.Color.White
        Me.btnAnuluj.Location = New System.Drawing.Point(139, 167)
        Me.btnAnuluj.Name = "btnAnuluj"
        Me.btnAnuluj.Size = New System.Drawing.Size(75, 23)
        Me.btnAnuluj.TabIndex = 6
        Me.btnAnuluj.Text = "Anuluj"
        Me.btnAnuluj.UseVisualStyleBackColor = False
        '
        'lblHaslo2
        '
        Me.lblHaslo2.AutoSize = True
        Me.lblHaslo2.ForeColor = System.Drawing.Color.Black
        Me.lblHaslo2.Location = New System.Drawing.Point(2, 144)
        Me.lblHaslo2.Name = "lblHaslo2"
        Me.lblHaslo2.Size = New System.Drawing.Size(94, 13)
        Me.lblHaslo2.TabIndex = 12
        Me.lblHaslo2.Text = "Has³o (ponownie):"
        '
        'txtHaslo2
        '
        Me.txtHaslo2.Location = New System.Drawing.Point(102, 141)
        Me.txtHaslo2.Name = "txtHaslo2"
        Me.txtHaslo2.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtHaslo2.Size = New System.Drawing.Size(112, 20)
        Me.txtHaslo2.TabIndex = 4
        '
        'txtHaslo
        '
        Me.txtHaslo.Location = New System.Drawing.Point(102, 115)
        Me.txtHaslo.Name = "txtHaslo"
        Me.txtHaslo.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtHaslo.Size = New System.Drawing.Size(112, 20)
        Me.txtHaslo.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(2, 118)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 13)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "Has³o:"
        '
        'lblKodResetujacy
        '
        Me.lblKodResetujacy.AutoSize = True
        Me.lblKodResetujacy.ForeColor = System.Drawing.Color.Black
        Me.lblKodResetujacy.Location = New System.Drawing.Point(1, 92)
        Me.lblKodResetujacy.Name = "lblKodResetujacy"
        Me.lblKodResetujacy.Size = New System.Drawing.Size(62, 13)
        Me.lblKodResetujacy.TabIndex = 14
        Me.lblKodResetujacy.Text = "Kod (e-mail)"
        '
        'txtKodBezpieczenstwa
        '
        Me.txtKodBezpieczenstwa.ForeColor = System.Drawing.Color.Black
        Me.txtKodBezpieczenstwa.Location = New System.Drawing.Point(69, 89)
        Me.txtKodBezpieczenstwa.Name = "txtKodBezpieczenstwa"
        Me.txtKodBezpieczenstwa.Size = New System.Drawing.Size(145, 20)
        Me.txtKodBezpieczenstwa.TabIndex = 2
        '
        'btnWyslijKodBezpieczenstwa
        '
        Me.btnWyslijKodBezpieczenstwa.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnWyslijKodBezpieczenstwa.ForeColor = System.Drawing.Color.White
        Me.btnWyslijKodBezpieczenstwa.Location = New System.Drawing.Point(139, 38)
        Me.btnWyslijKodBezpieczenstwa.Name = "btnWyslijKodBezpieczenstwa"
        Me.btnWyslijKodBezpieczenstwa.Size = New System.Drawing.Size(75, 23)
        Me.btnWyslijKodBezpieczenstwa.TabIndex = 1
        Me.btnWyslijKodBezpieczenstwa.Text = "Wyœlij kod"
        Me.btnWyslijKodBezpieczenstwa.UseVisualStyleBackColor = False
        '
        'lblInformacja
        '
        Me.lblInformacja.AutoSize = True
        Me.lblInformacja.ForeColor = System.Drawing.Color.Black
        Me.lblInformacja.Location = New System.Drawing.Point(1, 73)
        Me.lblInformacja.Name = "lblInformacja"
        Me.lblInformacja.Size = New System.Drawing.Size(205, 13)
        Me.lblInformacja.TabIndex = 17
        Me.lblInformacja.Text = "Przepisz poni¿ej dane z wiadomoœci e-mail"
        '
        'frmResetHasla
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(220, 202)
        Me.Controls.Add(Me.lblInformacja)
        Me.Controls.Add(Me.btnWyslijKodBezpieczenstwa)
        Me.Controls.Add(Me.lblKodResetujacy)
        Me.Controls.Add(Me.txtKodBezpieczenstwa)
        Me.Controls.Add(Me.lblHaslo2)
        Me.Controls.Add(Me.txtHaslo2)
        Me.Controls.Add(Me.txtHaslo)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnAnuluj)
        Me.Controls.Add(Me.lblLogin)
        Me.Controls.Add(Me.txtLogin)
        Me.Controls.Add(Me.btnResetujHaslo)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(226, 230)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(226, 230)
        Me.Name = "frmResetHasla"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Reset has³a"
        Me.TransparencyKey = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblLogin As System.Windows.Forms.Label
    Friend WithEvents txtLogin As System.Windows.Forms.TextBox
    Friend WithEvents btnResetujHaslo As System.Windows.Forms.Button
    Friend WithEvents btnAnuluj As System.Windows.Forms.Button
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents lblHaslo2 As System.Windows.Forms.Label
    Friend WithEvents txtHaslo2 As System.Windows.Forms.TextBox
    Friend WithEvents txtHaslo As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblKodResetujacy As System.Windows.Forms.Label
    Friend WithEvents txtKodBezpieczenstwa As System.Windows.Forms.TextBox
    Friend WithEvents btnWyslijKodBezpieczenstwa As System.Windows.Forms.Button
    Friend WithEvents lblInformacja As System.Windows.Forms.Label

End Class
