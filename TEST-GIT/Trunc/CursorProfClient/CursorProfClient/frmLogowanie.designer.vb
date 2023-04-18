<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLogowanie
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLogowanie))
        Me.lblHaslo = New System.Windows.Forms.Label()
        Me.txtHaslo = New System.Windows.Forms.TextBox()
        Me.lblLogin = New System.Windows.Forms.Label()
        Me.txtLogin = New System.Windows.Forms.TextBox()
        Me.btnZaloguj = New System.Windows.Forms.Button()
        Me.btnAnuluj = New System.Windows.Forms.Button()
        Me.lblWersja = New System.Windows.Forms.Label()
        Me.rbProdukcyjny = New System.Windows.Forms.RadioButton()
        Me.lblServer = New System.Windows.Forms.Label()
        Me.rbTestowy = New System.Windows.Forms.RadioButton()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.lblZapomnialemHasla = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblHaslo
        '
        Me.lblHaslo.AutoSize = True
        Me.lblHaslo.ForeColor = System.Drawing.Color.Black
        Me.lblHaslo.Location = New System.Drawing.Point(1, 153)
        Me.lblHaslo.Name = "lblHaslo"
        Me.lblHaslo.Size = New System.Drawing.Size(34, 13)
        Me.lblHaslo.TabIndex = 2
        Me.lblHaslo.Text = "Haslo"
        '
        'txtHaslo
        '
        Me.txtHaslo.ForeColor = System.Drawing.Color.Black
        Me.txtHaslo.Location = New System.Drawing.Point(40, 150)
        Me.txtHaslo.Name = "txtHaslo"
        Me.txtHaslo.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtHaslo.Size = New System.Drawing.Size(174, 20)
        Me.txtHaslo.TabIndex = 3
        '
        'lblLogin
        '
        Me.lblLogin.AutoSize = True
        Me.lblLogin.ForeColor = System.Drawing.Color.Black
        Me.lblLogin.Location = New System.Drawing.Point(1, 127)
        Me.lblLogin.Name = "lblLogin"
        Me.lblLogin.Size = New System.Drawing.Size(33, 13)
        Me.lblLogin.TabIndex = 0
        Me.lblLogin.Text = "Login"
        '
        'txtLogin
        '
        Me.txtLogin.ForeColor = System.Drawing.Color.Black
        Me.txtLogin.Location = New System.Drawing.Point(40, 124)
        Me.txtLogin.Name = "txtLogin"
        Me.txtLogin.Size = New System.Drawing.Size(174, 20)
        Me.txtLogin.TabIndex = 1
        '
        'btnZaloguj
        '
        Me.btnZaloguj.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnZaloguj.ForeColor = System.Drawing.Color.White
        Me.btnZaloguj.Location = New System.Drawing.Point(52, 226)
        Me.btnZaloguj.Name = "btnZaloguj"
        Me.btnZaloguj.Size = New System.Drawing.Size(75, 23)
        Me.btnZaloguj.TabIndex = 8
        Me.btnZaloguj.Text = "Zaloguj"
        Me.btnZaloguj.UseVisualStyleBackColor = False
        '
        'btnAnuluj
        '
        Me.btnAnuluj.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnAnuluj.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnAnuluj.Enabled = False
        Me.btnAnuluj.ForeColor = System.Drawing.Color.White
        Me.btnAnuluj.Location = New System.Drawing.Point(133, 226)
        Me.btnAnuluj.Name = "btnAnuluj"
        Me.btnAnuluj.Size = New System.Drawing.Size(75, 23)
        Me.btnAnuluj.TabIndex = 9
        Me.btnAnuluj.Text = "Anuluj"
        Me.btnAnuluj.UseVisualStyleBackColor = False
        '
        'lblWersja
        '
        Me.lblWersja.AutoSize = True
        Me.lblWersja.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblWersja.ForeColor = System.Drawing.Color.Black
        Me.lblWersja.Location = New System.Drawing.Point(1, 179)
        Me.lblWersja.Name = "lblWersja"
        Me.lblWersja.Size = New System.Drawing.Size(84, 13)
        Me.lblWersja.TabIndex = 4
        Me.lblWersja.Text = "Wersja aplikacji:"
        Me.ToolTip.SetToolTip(Me.lblWersja, "Aktualna wersja aplikacji")
        '
        'rbProdukcyjny
        '
        Me.rbProdukcyjny.AutoSize = True
        Me.rbProdukcyjny.Checked = True
        Me.rbProdukcyjny.ForeColor = System.Drawing.Color.Black
        Me.rbProdukcyjny.Location = New System.Drawing.Point(50, 203)
        Me.rbProdukcyjny.Name = "rbProdukcyjny"
        Me.rbProdukcyjny.Size = New System.Drawing.Size(83, 17)
        Me.rbProdukcyjny.TabIndex = 6
        Me.rbProdukcyjny.TabStop = True
        Me.rbProdukcyjny.Text = "Produkcyjny"
        Me.rbProdukcyjny.UseVisualStyleBackColor = True
        '
        'lblServer
        '
        Me.lblServer.AutoSize = True
        Me.lblServer.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblServer.ForeColor = System.Drawing.Color.Black
        Me.lblServer.Location = New System.Drawing.Point(1, 205)
        Me.lblServer.Name = "lblServer"
        Me.lblServer.Size = New System.Drawing.Size(43, 13)
        Me.lblServer.TabIndex = 5
        Me.lblServer.Text = "Serwer:"
        '
        'rbTestowy
        '
        Me.rbTestowy.AutoSize = True
        Me.rbTestowy.Enabled = False
        Me.rbTestowy.ForeColor = System.Drawing.Color.Black
        Me.rbTestowy.Location = New System.Drawing.Point(131, 203)
        Me.rbTestowy.Name = "rbTestowy"
        Me.rbTestowy.Size = New System.Drawing.Size(65, 17)
        Me.rbTestowy.TabIndex = 7
        Me.rbTestowy.Text = "Testowy"
        Me.rbTestowy.UseVisualStyleBackColor = True
        '
        'lblZapomnialemHasla
        '
        Me.lblZapomnialemHasla.AutoSize = True
        Me.lblZapomnialemHasla.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblZapomnialemHasla.ForeColor = System.Drawing.Color.DodgerBlue
        Me.lblZapomnialemHasla.Location = New System.Drawing.Point(108, 254)
        Me.lblZapomnialemHasla.Name = "lblZapomnialemHasla"
        Me.lblZapomnialemHasla.Size = New System.Drawing.Size(102, 13)
        Me.lblZapomnialemHasla.TabIndex = 19
        Me.lblZapomnialemHasla.Text = "Zapomnia³em has³a"
        Me.ToolTip.SetToolTip(Me.lblZapomnialemHasla, "Pozwala na wygenerowanie nowego has³a")
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.White
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(4, 13)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(210, 78)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 18
        Me.PictureBox1.TabStop = False
        '
        'frmLogowanie
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(220, 276)
        Me.Controls.Add(Me.lblZapomnialemHasla)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.rbTestowy)
        Me.Controls.Add(Me.lblServer)
        Me.Controls.Add(Me.rbProdukcyjny)
        Me.Controls.Add(Me.lblWersja)
        Me.Controls.Add(Me.btnAnuluj)
        Me.Controls.Add(Me.lblHaslo)
        Me.Controls.Add(Me.txtHaslo)
        Me.Controls.Add(Me.lblLogin)
        Me.Controls.Add(Me.txtLogin)
        Me.Controls.Add(Me.btnZaloguj)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(226, 301)
        Me.MinimumSize = New System.Drawing.Size(226, 301)
        Me.Name = "frmLogowanie"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Logowanie"
        Me.TransparencyKey = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblHaslo As System.Windows.Forms.Label
    Friend WithEvents txtHaslo As System.Windows.Forms.TextBox
    Friend WithEvents lblLogin As System.Windows.Forms.Label
    Friend WithEvents txtLogin As System.Windows.Forms.TextBox
    Friend WithEvents btnZaloguj As System.Windows.Forms.Button
    Friend WithEvents btnAnuluj As System.Windows.Forms.Button
    Friend WithEvents lblWersja As System.Windows.Forms.Label
    Friend WithEvents rbProdukcyjny As System.Windows.Forms.RadioButton
    Friend WithEvents lblServer As System.Windows.Forms.Label
    Friend WithEvents rbTestowy As System.Windows.Forms.RadioButton
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents lblZapomnialemHasla As System.Windows.Forms.Label

End Class
