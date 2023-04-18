<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAdres
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
        Me.txtMiasto = New System.Windows.Forms.TextBox()
        Me.txtAdres = New System.Windows.Forms.TextBox()
        Me.lblKod = New System.Windows.Forms.Label()
        Me.lblMiasto = New System.Windows.Forms.Label()
        Me.lblAdres = New System.Windows.Forms.Label()
        Me.btnOk = New System.Windows.Forms.Button()
        Me.btnAnuluj = New System.Windows.Forms.Button()
        Me.btnZastosuj = New System.Windows.Forms.Button()
        Me.txtKod = New System.Windows.Forms.MaskedTextBox()
        Me.txtNazwa = New System.Windows.Forms.TextBox()
        Me.lblNazwa = New System.Windows.Forms.Label()
        Me.chkDomyslny = New System.Windows.Forms.CheckBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.SuspendLayout()
        '
        'txtMiasto
        '
        Me.txtMiasto.Location = New System.Drawing.Point(148, 59)
        Me.txtMiasto.Name = "txtMiasto"
        Me.txtMiasto.Size = New System.Drawing.Size(180, 20)
        Me.txtMiasto.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.txtMiasto, "Miasto")
        '
        'txtAdres
        '
        Me.txtAdres.Location = New System.Drawing.Point(55, 33)
        Me.txtAdres.Name = "txtAdres"
        Me.txtAdres.Size = New System.Drawing.Size(273, 20)
        Me.txtAdres.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.txtAdres, "Ulica i numer domu")
        '
        'lblKod
        '
        Me.lblKod.AutoSize = True
        Me.lblKod.ForeColor = System.Drawing.Color.Black
        Me.lblKod.Location = New System.Drawing.Point(8, 62)
        Me.lblKod.Name = "lblKod"
        Me.lblKod.Size = New System.Drawing.Size(29, 13)
        Me.lblKod.TabIndex = 4
        Me.lblKod.Text = "Kod:"
        Me.ToolTip1.SetToolTip(Me.lblKod, "Kod pocztowy")
        '
        'lblMiasto
        '
        Me.lblMiasto.AutoSize = True
        Me.lblMiasto.ForeColor = System.Drawing.Color.Black
        Me.lblMiasto.Location = New System.Drawing.Point(101, 62)
        Me.lblMiasto.Name = "lblMiasto"
        Me.lblMiasto.Size = New System.Drawing.Size(41, 13)
        Me.lblMiasto.TabIndex = 6
        Me.lblMiasto.Text = "Miasto:"
        Me.ToolTip1.SetToolTip(Me.lblMiasto, "Miasto")
        '
        'lblAdres
        '
        Me.lblAdres.AutoSize = True
        Me.lblAdres.ForeColor = System.Drawing.Color.Black
        Me.lblAdres.Location = New System.Drawing.Point(8, 36)
        Me.lblAdres.Name = "lblAdres"
        Me.lblAdres.Size = New System.Drawing.Size(37, 13)
        Me.lblAdres.TabIndex = 2
        Me.lblAdres.Text = "Adres:"
        Me.ToolTip1.SetToolTip(Me.lblAdres, "Ulica i numer domu")
        '
        'btnOk
        '
        Me.btnOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOk.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnOk.ForeColor = System.Drawing.Color.White
        Me.btnOk.Location = New System.Drawing.Point(89, 107)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(75, 23)
        Me.btnOk.TabIndex = 9
        Me.btnOk.Text = "OK"
        Me.ToolTip1.SetToolTip(Me.btnOk, "Zapisuje adres i zamyka okno edycji")
        Me.btnOk.UseVisualStyleBackColor = False
        '
        'btnAnuluj
        '
        Me.btnAnuluj.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAnuluj.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnAnuluj.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnAnuluj.ForeColor = System.Drawing.Color.White
        Me.btnAnuluj.Location = New System.Drawing.Point(251, 107)
        Me.btnAnuluj.Name = "btnAnuluj"
        Me.btnAnuluj.Size = New System.Drawing.Size(75, 23)
        Me.btnAnuluj.TabIndex = 11
        Me.btnAnuluj.Text = "Anuluj"
        Me.ToolTip1.SetToolTip(Me.btnAnuluj, "Zamyka okno edycji")
        Me.btnAnuluj.UseVisualStyleBackColor = False
        '
        'btnZastosuj
        '
        Me.btnZastosuj.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnZastosuj.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnZastosuj.ForeColor = System.Drawing.Color.White
        Me.btnZastosuj.Location = New System.Drawing.Point(170, 107)
        Me.btnZastosuj.Name = "btnZastosuj"
        Me.btnZastosuj.Size = New System.Drawing.Size(75, 23)
        Me.btnZastosuj.TabIndex = 10
        Me.btnZastosuj.Text = "&Zastosuj"
        Me.ToolTip1.SetToolTip(Me.btnZastosuj, "Zapisuje adres i nie zamyka okna edycji")
        Me.btnZastosuj.UseVisualStyleBackColor = False
        '
        'txtKod
        '
        Me.txtKod.Location = New System.Drawing.Point(55, 59)
        Me.txtKod.Mask = "00-000"
        Me.txtKod.Name = "txtKod"
        Me.txtKod.Size = New System.Drawing.Size(40, 20)
        Me.txtKod.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.txtKod, "Kod pocztowy")
        '
        'txtNazwa
        '
        Me.txtNazwa.Location = New System.Drawing.Point(55, 7)
        Me.txtNazwa.Name = "txtNazwa"
        Me.txtNazwa.Size = New System.Drawing.Size(273, 20)
        Me.txtNazwa.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.txtNazwa, "Nazwa adresu zdefiniowanego")
        '
        'lblNazwa
        '
        Me.lblNazwa.AutoSize = True
        Me.lblNazwa.ForeColor = System.Drawing.Color.Black
        Me.lblNazwa.Location = New System.Drawing.Point(8, 10)
        Me.lblNazwa.Name = "lblNazwa"
        Me.lblNazwa.Size = New System.Drawing.Size(43, 13)
        Me.lblNazwa.TabIndex = 0
        Me.lblNazwa.Text = "Nazwa:"
        Me.ToolTip1.SetToolTip(Me.lblNazwa, "Nazwa adresu zdefiniowanego")
        '
        'chkDomyslny
        '
        Me.chkDomyslny.AutoSize = True
        Me.chkDomyslny.Location = New System.Drawing.Point(11, 85)
        Me.chkDomyslny.Name = "chkDomyslny"
        Me.chkDomyslny.Size = New System.Drawing.Size(71, 17)
        Me.chkDomyslny.TabIndex = 8
        Me.chkDomyslny.Text = "Domyœlny"
        Me.ToolTip1.SetToolTip(Me.chkDomyslny, "Zaznaczenie tej opcji oznacza edytowany adres jako domyœlny. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Uwaga! Ka¿dy u¿ytk" & _
        "ownik mo¿e mieæ nieskoñczenie wiele adresów zdefiniowanych, ale tylko jeden adre" & _
        "s domyœlny.")
        Me.chkDomyslny.UseVisualStyleBackColor = True
        '
        'ToolTip1
        '
        Me.ToolTip1.AutoPopDelay = 10000
        Me.ToolTip1.InitialDelay = 500
        Me.ToolTip1.ReshowDelay = 500
        '
        'frmAdres
        '
        Me.AcceptButton = Me.btnZastosuj
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnAnuluj
        Me.ClientSize = New System.Drawing.Size(335, 135)
        Me.Controls.Add(Me.chkDomyslny)
        Me.Controls.Add(Me.lblNazwa)
        Me.Controls.Add(Me.txtNazwa)
        Me.Controls.Add(Me.txtKod)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.btnAnuluj)
        Me.Controls.Add(Me.btnZastosuj)
        Me.Controls.Add(Me.lblAdres)
        Me.Controls.Add(Me.lblMiasto)
        Me.Controls.Add(Me.lblKod)
        Me.Controls.Add(Me.txtAdres)
        Me.Controls.Add(Me.txtMiasto)
        Me.MaximumSize = New System.Drawing.Size(351, 173)
        Me.MinimumSize = New System.Drawing.Size(351, 173)
        Me.Name = "frmAdres"
        Me.Text = "Adres"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtMiasto As System.Windows.Forms.TextBox
    Friend WithEvents txtAdres As System.Windows.Forms.TextBox
    Friend WithEvents lblKod As System.Windows.Forms.Label
    Friend WithEvents lblMiasto As System.Windows.Forms.Label
    Friend WithEvents lblAdres As System.Windows.Forms.Label
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents btnAnuluj As System.Windows.Forms.Button
    Friend WithEvents btnZastosuj As System.Windows.Forms.Button
    Friend WithEvents txtKod As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtNazwa As System.Windows.Forms.TextBox
    Friend WithEvents lblNazwa As System.Windows.Forms.Label
    Friend WithEvents chkDomyslny As System.Windows.Forms.CheckBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class
