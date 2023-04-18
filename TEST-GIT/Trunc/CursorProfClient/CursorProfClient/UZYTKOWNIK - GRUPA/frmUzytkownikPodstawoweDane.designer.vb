<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUzytkownikPodstawoweDane
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
        Me.gbDanePodstawowe = New System.Windows.Forms.GroupBox()
        Me.txtTelkom = New System.Windows.Forms.MaskedTextBox()
        Me.lblEmail = New System.Windows.Forms.Label()
        Me.lblTelkom = New System.Windows.Forms.Label()
        Me.lblNazwa = New System.Windows.Forms.Label()
        Me.lblNazwisko = New System.Windows.Forms.Label()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.txtNazwa = New System.Windows.Forms.TextBox()
        Me.txtNazwisko = New System.Windows.Forms.TextBox()
        Me.txtImie = New System.Windows.Forms.TextBox()
        Me.lblImie = New System.Windows.Forms.Label()
        Me.gbDaneLogowania = New System.Windows.Forms.GroupBox()
        Me.lblHasloStatus = New System.Windows.Forms.Label()
        Me.lblHaslo2 = New System.Windows.Forms.Label()
        Me.txtHaslo2 = New System.Windows.Forms.TextBox()
        Me.lblHaslo = New System.Windows.Forms.Label()
        Me.txtHaslo = New System.Windows.Forms.TextBox()
        Me.txtLogin = New System.Windows.Forms.TextBox()
        Me.lblLogin = New System.Windows.Forms.Label()
        Me.gbAdresy = New System.Windows.Forms.GroupBox()
        Me.lblAdresyOpis = New System.Windows.Forms.Label()
        Me.btnAdresy = New System.Windows.Forms.Button()
        Me.lblAdresy = New System.Windows.Forms.Label()
        Me.btnOk = New System.Windows.Forms.Button()
        Me.btnAnuluj = New System.Windows.Forms.Button()
        Me.btnZastosuj = New System.Windows.Forms.Button()
        Me.btnZmienHaslo = New System.Windows.Forms.Button()
        Me.btnNotyfikacje = New System.Windows.Forms.Button()
        Me.gbDanePodstawowe.SuspendLayout()
        Me.gbDaneLogowania.SuspendLayout()
        Me.gbAdresy.SuspendLayout()
        Me.SuspendLayout()
        '
        'gbDanePodstawowe
        '
        Me.gbDanePodstawowe.Controls.Add(Me.txtTelkom)
        Me.gbDanePodstawowe.Controls.Add(Me.lblEmail)
        Me.gbDanePodstawowe.Controls.Add(Me.lblTelkom)
        Me.gbDanePodstawowe.Controls.Add(Me.lblNazwa)
        Me.gbDanePodstawowe.Controls.Add(Me.lblNazwisko)
        Me.gbDanePodstawowe.Controls.Add(Me.txtEmail)
        Me.gbDanePodstawowe.Controls.Add(Me.txtNazwa)
        Me.gbDanePodstawowe.Controls.Add(Me.txtNazwisko)
        Me.gbDanePodstawowe.Controls.Add(Me.txtImie)
        Me.gbDanePodstawowe.Controls.Add(Me.lblImie)
        Me.gbDanePodstawowe.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbDanePodstawowe.ForeColor = System.Drawing.Color.Black
        Me.gbDanePodstawowe.Location = New System.Drawing.Point(12, 12)
        Me.gbDanePodstawowe.Name = "gbDanePodstawowe"
        Me.gbDanePodstawowe.Size = New System.Drawing.Size(276, 158)
        Me.gbDanePodstawowe.TabIndex = 0
        Me.gbDanePodstawowe.TabStop = False
        Me.gbDanePodstawowe.Text = "Dane podstawowe"
        '
        'txtTelkom
        '
        Me.txtTelkom.Location = New System.Drawing.Point(115, 100)
        Me.txtTelkom.Mask = "000000000"
        Me.txtTelkom.Name = "txtTelkom"
        Me.txtTelkom.Size = New System.Drawing.Size(149, 20)
        Me.txtTelkom.TabIndex = 7
        '
        'lblEmail
        '
        Me.lblEmail.AutoSize = True
        Me.lblEmail.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblEmail.ForeColor = System.Drawing.Color.Black
        Me.lblEmail.Location = New System.Drawing.Point(6, 126)
        Me.lblEmail.Name = "lblEmail"
        Me.lblEmail.Size = New System.Drawing.Size(35, 13)
        Me.lblEmail.TabIndex = 8
        Me.lblEmail.Text = "Email:"
        '
        'lblTelkom
        '
        Me.lblTelkom.AutoSize = True
        Me.lblTelkom.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblTelkom.ForeColor = System.Drawing.Color.Black
        Me.lblTelkom.Location = New System.Drawing.Point(6, 100)
        Me.lblTelkom.Name = "lblTelkom"
        Me.lblTelkom.Size = New System.Drawing.Size(85, 13)
        Me.lblTelkom.TabIndex = 6
        Me.lblTelkom.Text = "Tel. komórkowy:"
        '
        'lblNazwa
        '
        Me.lblNazwa.AutoSize = True
        Me.lblNazwa.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblNazwa.ForeColor = System.Drawing.Color.Black
        Me.lblNazwa.Location = New System.Drawing.Point(6, 74)
        Me.lblNazwa.Name = "lblNazwa"
        Me.lblNazwa.Size = New System.Drawing.Size(103, 13)
        Me.lblNazwa.TabIndex = 4
        Me.lblNazwa.Text = "Nazwa wyświetlana:"
        '
        'lblNazwisko
        '
        Me.lblNazwisko.AutoSize = True
        Me.lblNazwisko.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblNazwisko.ForeColor = System.Drawing.Color.Black
        Me.lblNazwisko.Location = New System.Drawing.Point(6, 48)
        Me.lblNazwisko.Name = "lblNazwisko"
        Me.lblNazwisko.Size = New System.Drawing.Size(56, 13)
        Me.lblNazwisko.TabIndex = 2
        Me.lblNazwisko.Text = "Nazwisko:"
        '
        'txtEmail
        '
        Me.txtEmail.Location = New System.Drawing.Point(115, 123)
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(149, 20)
        Me.txtEmail.TabIndex = 9
        '
        'txtNazwa
        '
        Me.txtNazwa.Location = New System.Drawing.Point(115, 71)
        Me.txtNazwa.Name = "txtNazwa"
        Me.txtNazwa.Size = New System.Drawing.Size(149, 20)
        Me.txtNazwa.TabIndex = 5
        '
        'txtNazwisko
        '
        Me.txtNazwisko.Location = New System.Drawing.Point(115, 45)
        Me.txtNazwisko.Name = "txtNazwisko"
        Me.txtNazwisko.Size = New System.Drawing.Size(149, 20)
        Me.txtNazwisko.TabIndex = 3
        '
        'txtImie
        '
        Me.txtImie.Location = New System.Drawing.Point(115, 19)
        Me.txtImie.Name = "txtImie"
        Me.txtImie.Size = New System.Drawing.Size(149, 20)
        Me.txtImie.TabIndex = 1
        '
        'lblImie
        '
        Me.lblImie.AutoSize = True
        Me.lblImie.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblImie.ForeColor = System.Drawing.Color.Black
        Me.lblImie.Location = New System.Drawing.Point(6, 22)
        Me.lblImie.Name = "lblImie"
        Me.lblImie.Size = New System.Drawing.Size(29, 13)
        Me.lblImie.TabIndex = 0
        Me.lblImie.Text = "Imię:"
        '
        'gbDaneLogowania
        '
        Me.gbDaneLogowania.Controls.Add(Me.lblHasloStatus)
        Me.gbDaneLogowania.Controls.Add(Me.lblHaslo2)
        Me.gbDaneLogowania.Controls.Add(Me.txtHaslo2)
        Me.gbDaneLogowania.Controls.Add(Me.lblHaslo)
        Me.gbDaneLogowania.Controls.Add(Me.txtHaslo)
        Me.gbDaneLogowania.Controls.Add(Me.txtLogin)
        Me.gbDaneLogowania.Controls.Add(Me.lblLogin)
        Me.gbDaneLogowania.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbDaneLogowania.ForeColor = System.Drawing.Color.Black
        Me.gbDaneLogowania.Location = New System.Drawing.Point(12, 177)
        Me.gbDaneLogowania.Name = "gbDaneLogowania"
        Me.gbDaneLogowania.Size = New System.Drawing.Size(276, 108)
        Me.gbDaneLogowania.TabIndex = 1
        Me.gbDaneLogowania.TabStop = False
        Me.gbDaneLogowania.Text = "Dane do logowania"
        '
        'lblHasloStatus
        '
        Me.lblHasloStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblHasloStatus.Location = New System.Drawing.Point(51, 48)
        Me.lblHasloStatus.Name = "lblHasloStatus"
        Me.lblHasloStatus.Size = New System.Drawing.Size(213, 15)
        Me.lblHasloStatus.TabIndex = 3
        Me.lblHasloStatus.Text = "<brak>"
        Me.lblHasloStatus.Visible = False
        '
        'lblHaslo2
        '
        Me.lblHaslo2.AutoSize = True
        Me.lblHaslo2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblHaslo2.ForeColor = System.Drawing.Color.Black
        Me.lblHaslo2.Location = New System.Drawing.Point(6, 74)
        Me.lblHaslo2.Name = "lblHaslo2"
        Me.lblHaslo2.Size = New System.Drawing.Size(85, 13)
        Me.lblHaslo2.TabIndex = 5
        Me.lblHaslo2.Text = "Hasło (powtórz):"
        '
        'txtHaslo2
        '
        Me.txtHaslo2.Location = New System.Drawing.Point(115, 71)
        Me.txtHaslo2.Name = "txtHaslo2"
        Me.txtHaslo2.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtHaslo2.Size = New System.Drawing.Size(149, 20)
        Me.txtHaslo2.TabIndex = 6
        '
        'lblHaslo
        '
        Me.lblHaslo.AutoSize = True
        Me.lblHaslo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblHaslo.ForeColor = System.Drawing.Color.Black
        Me.lblHaslo.Location = New System.Drawing.Point(6, 48)
        Me.lblHaslo.Name = "lblHaslo"
        Me.lblHaslo.Size = New System.Drawing.Size(39, 13)
        Me.lblHaslo.TabIndex = 2
        Me.lblHaslo.Text = "Hasło:"
        '
        'txtHaslo
        '
        Me.txtHaslo.Location = New System.Drawing.Point(115, 45)
        Me.txtHaslo.Name = "txtHaslo"
        Me.txtHaslo.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtHaslo.Size = New System.Drawing.Size(149, 20)
        Me.txtHaslo.TabIndex = 4
        '
        'txtLogin
        '
        Me.txtLogin.Enabled = False
        Me.txtLogin.Location = New System.Drawing.Point(115, 19)
        Me.txtLogin.Name = "txtLogin"
        Me.txtLogin.Size = New System.Drawing.Size(149, 20)
        Me.txtLogin.TabIndex = 1
        '
        'lblLogin
        '
        Me.lblLogin.AutoSize = True
        Me.lblLogin.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblLogin.ForeColor = System.Drawing.Color.Black
        Me.lblLogin.Location = New System.Drawing.Point(6, 22)
        Me.lblLogin.Name = "lblLogin"
        Me.lblLogin.Size = New System.Drawing.Size(36, 13)
        Me.lblLogin.TabIndex = 0
        Me.lblLogin.Text = "Login:"
        '
        'gbAdresy
        '
        Me.gbAdresy.Controls.Add(Me.lblAdresyOpis)
        Me.gbAdresy.Controls.Add(Me.btnAdresy)
        Me.gbAdresy.Controls.Add(Me.lblAdresy)
        Me.gbAdresy.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbAdresy.ForeColor = System.Drawing.Color.Black
        Me.gbAdresy.Location = New System.Drawing.Point(12, 328)
        Me.gbAdresy.Name = "gbAdresy"
        Me.gbAdresy.Size = New System.Drawing.Size(276, 75)
        Me.gbAdresy.TabIndex = 3
        Me.gbAdresy.TabStop = False
        Me.gbAdresy.Text = "Adresy zdefiniowane dla użytkownika"
        '
        'lblAdresyOpis
        '
        Me.lblAdresyOpis.AutoSize = True
        Me.lblAdresyOpis.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblAdresyOpis.ForeColor = System.Drawing.Color.Black
        Me.lblAdresyOpis.Location = New System.Drawing.Point(6, 24)
        Me.lblAdresyOpis.Name = "lblAdresyOpis"
        Me.lblAdresyOpis.Size = New System.Drawing.Size(151, 13)
        Me.lblAdresyOpis.TabIndex = 0
        Me.lblAdresyOpis.Text = "Ilość zdefiniowanych adresów:"
        '
        'btnAdresy
        '
        Me.btnAdresy.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnAdresy.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnAdresy.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnAdresy.ForeColor = System.Drawing.Color.White
        Me.btnAdresy.Location = New System.Drawing.Point(120, 46)
        Me.btnAdresy.Name = "btnAdresy"
        Me.btnAdresy.Size = New System.Drawing.Size(150, 23)
        Me.btnAdresy.TabIndex = 2
        Me.btnAdresy.Text = "Edytuj adresy zdefiniowane"
        Me.btnAdresy.UseVisualStyleBackColor = False
        '
        'lblAdresy
        '
        Me.lblAdresy.AutoSize = True
        Me.lblAdresy.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblAdresy.Location = New System.Drawing.Point(163, 24)
        Me.lblAdresy.Name = "lblAdresy"
        Me.lblAdresy.Size = New System.Drawing.Size(14, 13)
        Me.lblAdresy.TabIndex = 1
        Me.lblAdresy.Text = "0"
        '
        'btnOk
        '
        Me.btnOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOk.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnOk.ForeColor = System.Drawing.Color.White
        Me.btnOk.Location = New System.Drawing.Point(49, 438)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(75, 23)
        Me.btnOk.TabIndex = 5
        Me.btnOk.Text = "OK"
        Me.btnOk.UseVisualStyleBackColor = False
        '
        'btnAnuluj
        '
        Me.btnAnuluj.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAnuluj.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnAnuluj.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnAnuluj.ForeColor = System.Drawing.Color.White
        Me.btnAnuluj.Location = New System.Drawing.Point(211, 438)
        Me.btnAnuluj.Name = "btnAnuluj"
        Me.btnAnuluj.Size = New System.Drawing.Size(75, 23)
        Me.btnAnuluj.TabIndex = 6
        Me.btnAnuluj.Text = "Anuluj"
        Me.btnAnuluj.UseVisualStyleBackColor = False
        '
        'btnZastosuj
        '
        Me.btnZastosuj.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnZastosuj.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnZastosuj.ForeColor = System.Drawing.Color.White
        Me.btnZastosuj.Location = New System.Drawing.Point(130, 438)
        Me.btnZastosuj.Name = "btnZastosuj"
        Me.btnZastosuj.Size = New System.Drawing.Size(75, 23)
        Me.btnZastosuj.TabIndex = 7
        Me.btnZastosuj.Text = "&Zastosuj"
        Me.btnZastosuj.UseVisualStyleBackColor = False
        '
        'btnZmienHaslo
        '
        Me.btnZmienHaslo.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnZmienHaslo.ForeColor = System.Drawing.Color.White
        Me.btnZmienHaslo.Location = New System.Drawing.Point(211, 291)
        Me.btnZmienHaslo.Name = "btnZmienHaslo"
        Me.btnZmienHaslo.Size = New System.Drawing.Size(77, 23)
        Me.btnZmienHaslo.TabIndex = 2
        Me.btnZmienHaslo.Text = "Zmień hasło"
        Me.btnZmienHaslo.UseVisualStyleBackColor = False
        '
        'btnNotyfikacje
        '
        Me.btnNotyfikacje.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnNotyfikacje.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnNotyfikacje.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnNotyfikacje.ForeColor = System.Drawing.Color.White
        Me.btnNotyfikacje.Location = New System.Drawing.Point(132, 409)
        Me.btnNotyfikacje.Name = "btnNotyfikacje"
        Me.btnNotyfikacje.Size = New System.Drawing.Size(150, 23)
        Me.btnNotyfikacje.TabIndex = 3
        Me.btnNotyfikacje.Text = "Edytuj notyfikacje"
        Me.btnNotyfikacje.UseVisualStyleBackColor = False
        '
        'frmUzytkownikPodstawoweDane
        '
        Me.AcceptButton = Me.btnZastosuj
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnAnuluj
        Me.ClientSize = New System.Drawing.Size(298, 473)
        Me.Controls.Add(Me.btnNotyfikacje)
        Me.Controls.Add(Me.btnZmienHaslo)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.btnAnuluj)
        Me.Controls.Add(Me.btnZastosuj)
        Me.Controls.Add(Me.gbAdresy)
        Me.Controls.Add(Me.gbDanePodstawowe)
        Me.Controls.Add(Me.gbDaneLogowania)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MinimumSize = New System.Drawing.Size(304, 495)
        Me.Name = "frmUzytkownikPodstawoweDane"
        Me.Text = "Konto"
        Me.gbDanePodstawowe.ResumeLayout(False)
        Me.gbDanePodstawowe.PerformLayout()
        Me.gbDaneLogowania.ResumeLayout(False)
        Me.gbDaneLogowania.PerformLayout()
        Me.gbAdresy.ResumeLayout(False)
        Me.gbAdresy.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gbDanePodstawowe As System.Windows.Forms.GroupBox
    Friend WithEvents txtTelkom As System.Windows.Forms.MaskedTextBox
    Friend WithEvents lblEmail As System.Windows.Forms.Label
    Friend WithEvents lblTelkom As System.Windows.Forms.Label
    Friend WithEvents lblNazwa As System.Windows.Forms.Label
    Friend WithEvents lblNazwisko As System.Windows.Forms.Label
    Friend WithEvents txtEmail As System.Windows.Forms.TextBox
    Friend WithEvents txtNazwa As System.Windows.Forms.TextBox
    Friend WithEvents txtNazwisko As System.Windows.Forms.TextBox
    Friend WithEvents txtImie As System.Windows.Forms.TextBox
    Friend WithEvents lblImie As System.Windows.Forms.Label
    Friend WithEvents gbDaneLogowania As System.Windows.Forms.GroupBox
    Friend WithEvents lblHasloStatus As System.Windows.Forms.Label
    Friend WithEvents lblHaslo2 As System.Windows.Forms.Label
    Friend WithEvents txtHaslo2 As System.Windows.Forms.TextBox
    Friend WithEvents lblHaslo As System.Windows.Forms.Label
    Friend WithEvents txtHaslo As System.Windows.Forms.TextBox
    Friend WithEvents txtLogin As System.Windows.Forms.TextBox
    Friend WithEvents lblLogin As System.Windows.Forms.Label
    Friend WithEvents gbAdresy As System.Windows.Forms.GroupBox
    Friend WithEvents lblAdresyOpis As System.Windows.Forms.Label
    Friend WithEvents btnAdresy As System.Windows.Forms.Button
    Friend WithEvents lblAdresy As System.Windows.Forms.Label
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents btnAnuluj As System.Windows.Forms.Button
    Friend WithEvents btnZastosuj As System.Windows.Forms.Button
    Friend WithEvents btnZmienHaslo As System.Windows.Forms.Button
    Friend WithEvents btnNotyfikacje As System.Windows.Forms.Button
End Class
