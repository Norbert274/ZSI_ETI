<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDostawca
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
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtKrajDostawca = New System.Windows.Forms.TextBox()
        Me.txtMiastoDostawca = New System.Windows.Forms.TextBox()
        Me.txtAdresDostawca = New System.Windows.Forms.TextBox()
        Me.txtNazwaDostawcy = New System.Windows.Forms.TextBox()
        Me.btnDodajNowegoDostawce = New System.Windows.Forms.Button()
        Me.btnAnuluj = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.txtKodPocztowy = New System.Windows.Forms.MaskedTextBox()
        Me.SuspendLayout()
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(144, 67)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(41, 13)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Miasto:"
        Me.ToolTip1.SetToolTip(Me.Label5, "Miasto")
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(144, 93)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(28, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Kraj:"
        Me.ToolTip1.SetToolTip(Me.Label4, "Kraj")
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(12, 42)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(37, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Adres:"
        Me.ToolTip1.SetToolTip(Me.Label3, "Adres dostawcy")
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(12, 67)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(77, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Kod pocztowy:"
        Me.ToolTip1.SetToolTip(Me.Label2, "Kod pocztowy")
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(12, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(43, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Nazwa:"
        Me.ToolTip1.SetToolTip(Me.Label1, "Nazwa dostawcy")
        '
        'txtKrajDostawca
        '
        Me.txtKrajDostawca.Location = New System.Drawing.Point(191, 90)
        Me.txtKrajDostawca.Name = "txtKrajDostawca"
        Me.txtKrajDostawca.Size = New System.Drawing.Size(180, 20)
        Me.txtKrajDostawca.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.txtKrajDostawca, "Kraj")
        '
        'txtMiastoDostawca
        '
        Me.txtMiastoDostawca.Location = New System.Drawing.Point(191, 64)
        Me.txtMiastoDostawca.Name = "txtMiastoDostawca"
        Me.txtMiastoDostawca.Size = New System.Drawing.Size(180, 20)
        Me.txtMiastoDostawca.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.txtMiastoDostawca, "Miasto")
        '
        'txtAdresDostawca
        '
        Me.txtAdresDostawca.Location = New System.Drawing.Point(55, 39)
        Me.txtAdresDostawca.Name = "txtAdresDostawca"
        Me.txtAdresDostawca.Size = New System.Drawing.Size(316, 20)
        Me.txtAdresDostawca.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.txtAdresDostawca, "Adres dostawcy")
        '
        'txtNazwaDostawcy
        '
        Me.txtNazwaDostawcy.Location = New System.Drawing.Point(55, 12)
        Me.txtNazwaDostawcy.Name = "txtNazwaDostawcy"
        Me.txtNazwaDostawcy.Size = New System.Drawing.Size(316, 20)
        Me.txtNazwaDostawcy.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.txtNazwaDostawcy, "Nazwa dostawcy")
        '
        'btnDodajNowegoDostawce
        '
        Me.btnDodajNowegoDostawce.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnDodajNowegoDostawce.ForeColor = System.Drawing.Color.White
        Me.btnDodajNowegoDostawce.Location = New System.Drawing.Point(169, 117)
        Me.btnDodajNowegoDostawce.Name = "btnDodajNowegoDostawce"
        Me.btnDodajNowegoDostawce.Size = New System.Drawing.Size(125, 25)
        Me.btnDodajNowegoDostawce.TabIndex = 10
        Me.btnDodajNowegoDostawce.Text = "Dodaj Dostawcę"
        Me.ToolTip1.SetToolTip(Me.btnDodajNowegoDostawce, "Zapisanie danych dostawcy do systemu")
        Me.btnDodajNowegoDostawce.UseVisualStyleBackColor = False
        '
        'btnAnuluj
        '
        Me.btnAnuluj.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnAnuluj.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnAnuluj.ForeColor = System.Drawing.Color.White
        Me.btnAnuluj.Location = New System.Drawing.Point(300, 117)
        Me.btnAnuluj.Name = "btnAnuluj"
        Me.btnAnuluj.Size = New System.Drawing.Size(71, 25)
        Me.btnAnuluj.TabIndex = 11
        Me.btnAnuluj.Text = "Anuluj"
        Me.btnAnuluj.UseVisualStyleBackColor = False
        '
        'ToolTip1
        '
        Me.ToolTip1.AutoPopDelay = 10000
        Me.ToolTip1.InitialDelay = 500
        Me.ToolTip1.ReshowDelay = 500
        '
        'txtKodPocztowy
        '
        Me.txtKodPocztowy.Location = New System.Drawing.Point(95, 64)
        Me.txtKodPocztowy.Mask = "00-000"
        Me.txtKodPocztowy.Name = "txtKodPocztowy"
        Me.txtKodPocztowy.Size = New System.Drawing.Size(43, 20)
        Me.txtKodPocztowy.TabIndex = 5
        '
        'frmDostawca
        '
        Me.AcceptButton = Me.btnDodajNowegoDostawce
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnAnuluj
        Me.ClientSize = New System.Drawing.Size(381, 148)
        Me.Controls.Add(Me.txtKodPocztowy)
        Me.Controls.Add(Me.btnAnuluj)
        Me.Controls.Add(Me.btnDodajNowegoDostawce)
        Me.Controls.Add(Me.txtNazwaDostawcy)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtKrajDostawca)
        Me.Controls.Add(Me.txtMiastoDostawca)
        Me.Controls.Add(Me.txtAdresDostawca)
        Me.MaximumSize = New System.Drawing.Size(397, 186)
        Me.MinimumSize = New System.Drawing.Size(397, 186)
        Me.Name = "frmDostawca"
        Me.Text = "Nowy dostawca"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtKrajDostawca As System.Windows.Forms.TextBox
    Friend WithEvents txtMiastoDostawca As System.Windows.Forms.TextBox
    Friend WithEvents txtAdresDostawca As System.Windows.Forms.TextBox
    Friend WithEvents txtNazwaDostawcy As System.Windows.Forms.TextBox
    Friend WithEvents btnDodajNowegoDostawce As System.Windows.Forms.Button
    Friend WithEvents btnAnuluj As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents txtKodPocztowy As System.Windows.Forms.MaskedTextBox
End Class
