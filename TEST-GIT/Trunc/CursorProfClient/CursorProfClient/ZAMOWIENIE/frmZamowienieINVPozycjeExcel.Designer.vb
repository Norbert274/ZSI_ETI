<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmZamowienieINVPozycjeExcel
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
        Me.btnZamknij = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnSzablon = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbNazwaArkusza = New System.Windows.Forms.ComboBox()
        Me.btnWybierzPlik = New System.Windows.Forms.Button()
        Me.txtPlik = New System.Windows.Forms.TextBox()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnZamknij
        '
        Me.btnZamknij.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnZamknij.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnZamknij.ForeColor = System.Drawing.Color.White
        Me.btnZamknij.Location = New System.Drawing.Point(460, 90)
        Me.btnZamknij.Name = "btnZamknij"
        Me.btnZamknij.Size = New System.Drawing.Size(85, 23)
        Me.btnZamknij.TabIndex = 7
        Me.btnZamknij.Text = "Zamknij"
        Me.btnZamknij.UseVisualStyleBackColor = False
        '
        'btnOK
        '
        Me.btnOK.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnOK.ForeColor = System.Drawing.Color.White
        Me.btnOK.Location = New System.Drawing.Point(383, 90)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(71, 23)
        Me.btnOK.TabIndex = 6
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnSzablon)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.cmbNazwaArkusza)
        Me.GroupBox1.Controls.Add(Me.btnWybierzPlik)
        Me.GroupBox1.Controls.Add(Me.txtPlik)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(533, 74)
        Me.GroupBox1.TabIndex = 4
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
        Me.btnSzablon.Text = "Pobierz szablon"
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
        'frmZamowienieINVPozycjeExcel
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnZamknij
        Me.ClientSize = New System.Drawing.Size(556, 125)
        Me.Controls.Add(Me.btnZamknij)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.GroupBox1)
        Me.MaximumSize = New System.Drawing.Size(572, 163)
        Me.MinimumSize = New System.Drawing.Size(572, 163)
        Me.Name = "frmZamowienieINVPozycjeExcel"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Import pozycji z Excela do zamówienia INV"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnZamknij As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnSzablon As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbNazwaArkusza As System.Windows.Forms.ComboBox
    Friend WithEvents btnWybierzPlik As System.Windows.Forms.Button
    Friend WithEvents txtPlik As System.Windows.Forms.TextBox
End Class
