<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAwizoDodajPozycjeZExcela
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnSzablon = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbNazwaArkusza = New System.Windows.Forms.ComboBox()
        Me.btnWybierzPlik = New System.Windows.Forms.Button()
        Me.txtPlik = New System.Windows.Forms.TextBox()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.lblIloscSKU = New System.Windows.Forms.Label()
        Me.btnZamknij = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnSzablon)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.cmbNazwaArkusza)
        Me.GroupBox1.Controls.Add(Me.btnWybierzPlik)
        Me.GroupBox1.Controls.Add(Me.txtPlik)
        Me.GroupBox1.Location = New System.Drawing.Point(5, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(533, 74)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Wybierz plik :"
        '
        'btnSzablon
        '
        Me.btnSzablon.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnSzablon.ForeColor = System.Drawing.Color.White
        Me.btnSzablon.Location = New System.Drawing.Point(414, 41)
        Me.btnSzablon.Name = "btnSzablon"
        Me.btnSzablon.Size = New System.Drawing.Size(110, 21)
        Me.btnSzablon.TabIndex = 4
        Me.btnSzablon.Text = "Szablon"
        Me.btnSzablon.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 45)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(83, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Nazwa arkusza:"
        '
        'cmbNazwaArkusza
        '
        Me.cmbNazwaArkusza.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbNazwaArkusza.FormattingEnabled = True
        Me.cmbNazwaArkusza.Location = New System.Drawing.Point(96, 41)
        Me.cmbNazwaArkusza.Name = "cmbNazwaArkusza"
        Me.cmbNazwaArkusza.Size = New System.Drawing.Size(312, 21)
        Me.cmbNazwaArkusza.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.cmbNazwaArkusza, "Wybieranie nazwy arkusza w pliku do importu")
        '
        'btnWybierzPlik
        '
        Me.btnWybierzPlik.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnWybierzPlik.ForeColor = System.Drawing.Color.White
        Me.btnWybierzPlik.Location = New System.Drawing.Point(414, 15)
        Me.btnWybierzPlik.Name = "btnWybierzPlik"
        Me.btnWybierzPlik.Size = New System.Drawing.Size(110, 23)
        Me.btnWybierzPlik.TabIndex = 1
        Me.btnWybierzPlik.Text = "Wybierz plik"
        Me.ToolTip1.SetToolTip(Me.btnWybierzPlik, "Wybieranie pliku Excela do importu pozycji do awiza")
        Me.btnWybierzPlik.UseVisualStyleBackColor = False
        '
        'txtPlik
        '
        Me.txtPlik.Enabled = False
        Me.txtPlik.Location = New System.Drawing.Point(7, 17)
        Me.txtPlik.Name = "txtPlik"
        Me.txtPlik.Size = New System.Drawing.Size(401, 20)
        Me.txtPlik.TabIndex = 0
        '
        'btnOK
        '
        Me.btnOK.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnOK.ForeColor = System.Drawing.Color.White
        Me.btnOK.Location = New System.Drawing.Point(376, 84)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(71, 23)
        Me.btnOK.TabIndex = 2
        Me.btnOK.Text = "OK"
        Me.ToolTip1.SetToolTip(Me.btnOK, "Dodanie pozycji do awiza z wczytanego pliku")
        Me.btnOK.UseVisualStyleBackColor = False
        '
        'lblIloscSKU
        '
        Me.lblIloscSKU.AutoSize = True
        Me.lblIloscSKU.Location = New System.Drawing.Point(6, 84)
        Me.lblIloscSKU.Name = "lblIloscSKU"
        Me.lblIloscSKU.Size = New System.Drawing.Size(10, 13)
        Me.lblIloscSKU.TabIndex = 1
        Me.lblIloscSKU.Text = " "
        '
        'btnZamknij
        '
        Me.btnZamknij.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnZamknij.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnZamknij.ForeColor = System.Drawing.Color.White
        Me.btnZamknij.Location = New System.Drawing.Point(453, 84)
        Me.btnZamknij.Name = "btnZamknij"
        Me.btnZamknij.Size = New System.Drawing.Size(85, 23)
        Me.btnZamknij.TabIndex = 3
        Me.btnZamknij.Text = "Zamknij"
        Me.btnZamknij.UseVisualStyleBackColor = False
        '
        'ToolTip1
        '
        Me.ToolTip1.AutoPopDelay = 10000
        Me.ToolTip1.InitialDelay = 500
        Me.ToolTip1.ReshowDelay = 500
        '
        'frmAwizoDodajPozycjeZExcela
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnZamknij
        Me.ClientSize = New System.Drawing.Size(542, 111)
        Me.Controls.Add(Me.btnZamknij)
        Me.Controls.Add(Me.lblIloscSKU)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "frmAwizoDodajPozycjeZExcela"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Dodaj pozycje do awiza z pliku Excela"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbNazwaArkusza As System.Windows.Forms.ComboBox
    Friend WithEvents btnWybierzPlik As System.Windows.Forms.Button
    Friend WithEvents txtPlik As System.Windows.Forms.TextBox
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents lblIloscSKU As System.Windows.Forms.Label
    Friend WithEvents btnZamknij As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents btnSzablon As System.Windows.Forms.Button
End Class
