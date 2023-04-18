<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAdresKopiuj
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
        Me.gbSkad = New System.Windows.Forms.GroupBox()
        Me.btnPobierzSzablon = New System.Windows.Forms.Button()
        Me.btnSciezkaExcel = New System.Windows.Forms.Button()
        Me.txtSciezkaExcel = New System.Windows.Forms.TextBox()
        Me.cmbUser = New System.Windows.Forms.ComboBox()
        Me.rbExcel = New System.Windows.Forms.RadioButton()
        Me.btnPobierzAdresy = New System.Windows.Forms.Button()
        Me.rbUser = New System.Windows.Forms.RadioButton()
        Me.gbDokad = New System.Windows.Forms.GroupBox()
        Me.lblLicznikUser = New System.Windows.Forms.Label()
        Me.btnKopiuj = New System.Windows.Forms.Button()
        Me.btnExcel = New System.Windows.Forms.Button()
        Me.btnUserFiltr = New System.Windows.Forms.Button()
        Me.txtUserFiltr = New System.Windows.Forms.TextBox()
        Me.chbUser = New System.Windows.Forms.CheckBox()
        Me.dgvUserDokad = New System.Windows.Forms.DataGridView()
        Me.btnZamknij = New System.Windows.Forms.Button()
        Me.gbAdresy = New System.Windows.Forms.GroupBox()
        Me.lblLicznik = New System.Windows.Forms.Label()
        Me.chbAdresy = New System.Windows.Forms.CheckBox()
        Me.dgvAdresy = New System.Windows.Forms.DataGridView()
        Me.gbSkad.SuspendLayout()
        Me.gbDokad.SuspendLayout()
        CType(Me.dgvUserDokad, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbAdresy.SuspendLayout()
        CType(Me.dgvAdresy, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gbSkad
        '
        Me.gbSkad.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbSkad.Controls.Add(Me.btnPobierzSzablon)
        Me.gbSkad.Controls.Add(Me.btnSciezkaExcel)
        Me.gbSkad.Controls.Add(Me.txtSciezkaExcel)
        Me.gbSkad.Controls.Add(Me.cmbUser)
        Me.gbSkad.Controls.Add(Me.rbExcel)
        Me.gbSkad.Controls.Add(Me.btnPobierzAdresy)
        Me.gbSkad.Controls.Add(Me.rbUser)
        Me.gbSkad.Location = New System.Drawing.Point(13, 13)
        Me.gbSkad.Name = "gbSkad"
        Me.gbSkad.Size = New System.Drawing.Size(591, 71)
        Me.gbSkad.TabIndex = 0
        Me.gbSkad.TabStop = False
        Me.gbSkad.Text = "Skąd"
        '
        'btnPobierzSzablon
        '
        Me.btnPobierzSzablon.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPobierzSzablon.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnPobierzSzablon.ForeColor = System.Drawing.Color.White
        Me.btnPobierzSzablon.Location = New System.Drawing.Point(384, 39)
        Me.btnPobierzSzablon.Name = "btnPobierzSzablon"
        Me.btnPobierzSzablon.Size = New System.Drawing.Size(63, 23)
        Me.btnPobierzSzablon.TabIndex = 6
        Me.btnPobierzSzablon.Text = "Szablon"
        Me.btnPobierzSzablon.UseVisualStyleBackColor = False
        '
        'btnSciezkaExcel
        '
        Me.btnSciezkaExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSciezkaExcel.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnSciezkaExcel.ForeColor = System.Drawing.Color.White
        Me.btnSciezkaExcel.Location = New System.Drawing.Point(343, 39)
        Me.btnSciezkaExcel.Name = "btnSciezkaExcel"
        Me.btnSciezkaExcel.Size = New System.Drawing.Size(35, 23)
        Me.btnSciezkaExcel.TabIndex = 5
        Me.btnSciezkaExcel.Text = "..."
        Me.btnSciezkaExcel.UseVisualStyleBackColor = False
        '
        'txtSciezkaExcel
        '
        Me.txtSciezkaExcel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSciezkaExcel.Location = New System.Drawing.Point(93, 41)
        Me.txtSciezkaExcel.Name = "txtSciezkaExcel"
        Me.txtSciezkaExcel.ReadOnly = True
        Me.txtSciezkaExcel.Size = New System.Drawing.Size(244, 20)
        Me.txtSciezkaExcel.TabIndex = 4
        '
        'cmbUser
        '
        Me.cmbUser.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbUser.FormattingEnabled = True
        Me.cmbUser.Location = New System.Drawing.Point(93, 16)
        Me.cmbUser.Name = "cmbUser"
        Me.cmbUser.Size = New System.Drawing.Size(285, 21)
        Me.cmbUser.TabIndex = 2
        '
        'rbExcel
        '
        Me.rbExcel.AutoSize = True
        Me.rbExcel.Location = New System.Drawing.Point(7, 44)
        Me.rbExcel.Name = "rbExcel"
        Me.rbExcel.Size = New System.Drawing.Size(51, 17)
        Me.rbExcel.TabIndex = 3
        Me.rbExcel.TabStop = True
        Me.rbExcel.Text = "Excel"
        Me.rbExcel.UseVisualStyleBackColor = True
        '
        'btnPobierzAdresy
        '
        Me.btnPobierzAdresy.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPobierzAdresy.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnPobierzAdresy.ForeColor = System.Drawing.Color.White
        Me.btnPobierzAdresy.Location = New System.Drawing.Point(453, 39)
        Me.btnPobierzAdresy.Name = "btnPobierzAdresy"
        Me.btnPobierzAdresy.Size = New System.Drawing.Size(131, 23)
        Me.btnPobierzAdresy.TabIndex = 6
        Me.btnPobierzAdresy.Text = "Pokaż adresy"
        Me.btnPobierzAdresy.UseVisualStyleBackColor = False
        '
        'rbUser
        '
        Me.rbUser.AutoSize = True
        Me.rbUser.Location = New System.Drawing.Point(7, 20)
        Me.rbUser.Name = "rbUser"
        Me.rbUser.Size = New System.Drawing.Size(80, 17)
        Me.rbUser.TabIndex = 1
        Me.rbUser.TabStop = True
        Me.rbUser.Text = "Użytkownik"
        Me.rbUser.UseVisualStyleBackColor = True
        '
        'gbDokad
        '
        Me.gbDokad.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbDokad.Controls.Add(Me.lblLicznikUser)
        Me.gbDokad.Controls.Add(Me.btnKopiuj)
        Me.gbDokad.Controls.Add(Me.btnExcel)
        Me.gbDokad.Controls.Add(Me.btnUserFiltr)
        Me.gbDokad.Controls.Add(Me.txtUserFiltr)
        Me.gbDokad.Controls.Add(Me.chbUser)
        Me.gbDokad.Controls.Add(Me.dgvUserDokad)
        Me.gbDokad.Controls.Add(Me.btnZamknij)
        Me.gbDokad.Location = New System.Drawing.Point(13, 276)
        Me.gbDokad.Name = "gbDokad"
        Me.gbDokad.Size = New System.Drawing.Size(591, 180)
        Me.gbDokad.TabIndex = 2
        Me.gbDokad.TabStop = False
        Me.gbDokad.Text = "Dokąd"
        '
        'lblLicznikUser
        '
        Me.lblLicznikUser.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblLicznikUser.AutoSize = True
        Me.lblLicznikUser.Location = New System.Drawing.Point(4, 154)
        Me.lblLicznikUser.Name = "lblLicznikUser"
        Me.lblLicznikUser.Size = New System.Drawing.Size(39, 13)
        Me.lblLicznikUser.TabIndex = 4
        Me.lblLicznikUser.Text = "Label1"
        '
        'btnKopiuj
        '
        Me.btnKopiuj.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnKopiuj.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnKopiuj.ForeColor = System.Drawing.Color.White
        Me.btnKopiuj.Location = New System.Drawing.Point(390, 149)
        Me.btnKopiuj.Name = "btnKopiuj"
        Me.btnKopiuj.Size = New System.Drawing.Size(94, 23)
        Me.btnKopiuj.TabIndex = 5
        Me.btnKopiuj.Text = "Kopiuj"
        Me.btnKopiuj.UseVisualStyleBackColor = False
        '
        'btnExcel
        '
        Me.btnExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExcel.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnExcel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnExcel.ForeColor = System.Drawing.Color.White
        Me.btnExcel.Location = New System.Drawing.Point(490, 149)
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Size = New System.Drawing.Size(94, 23)
        Me.btnExcel.TabIndex = 6
        Me.btnExcel.Text = "Excel"
        Me.btnExcel.UseVisualStyleBackColor = False
        '
        'btnUserFiltr
        '
        Me.btnUserFiltr.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnUserFiltr.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnUserFiltr.ForeColor = System.Drawing.Color.White
        Me.btnUserFiltr.Location = New System.Drawing.Point(545, 15)
        Me.btnUserFiltr.Name = "btnUserFiltr"
        Me.btnUserFiltr.Size = New System.Drawing.Size(39, 23)
        Me.btnUserFiltr.TabIndex = 2
        Me.btnUserFiltr.Text = "Filtr"
        Me.btnUserFiltr.UseVisualStyleBackColor = False
        '
        'txtUserFiltr
        '
        Me.txtUserFiltr.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtUserFiltr.Location = New System.Drawing.Point(390, 17)
        Me.txtUserFiltr.Name = "txtUserFiltr"
        Me.txtUserFiltr.Size = New System.Drawing.Size(149, 20)
        Me.txtUserFiltr.TabIndex = 1
        '
        'chbUser
        '
        Me.chbUser.AutoSize = True
        Me.chbUser.Location = New System.Drawing.Point(7, 19)
        Me.chbUser.Name = "chbUser"
        Me.chbUser.Size = New System.Drawing.Size(238, 17)
        Me.chbUser.TabIndex = 0
        Me.chbUser.Text = "Zaznacz/Odznacz wszystkich użytkowników"
        Me.chbUser.UseVisualStyleBackColor = True
        '
        'dgvUserDokad
        '
        Me.dgvUserDokad.AllowUserToAddRows = False
        Me.dgvUserDokad.AllowUserToDeleteRows = False
        Me.dgvUserDokad.AllowUserToResizeColumns = False
        Me.dgvUserDokad.AllowUserToResizeRows = False
        Me.dgvUserDokad.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvUserDokad.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgvUserDokad.BackgroundColor = System.Drawing.Color.White
        Me.dgvUserDokad.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvUserDokad.Location = New System.Drawing.Point(6, 44)
        Me.dgvUserDokad.Name = "dgvUserDokad"
        Me.dgvUserDokad.RowHeadersVisible = False
        Me.dgvUserDokad.Size = New System.Drawing.Size(578, 101)
        Me.dgvUserDokad.TabIndex = 3
        '
        'btnZamknij
        '
        Me.btnZamknij.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnZamknij.Location = New System.Drawing.Point(299, 81)
        Me.btnZamknij.Name = "btnZamknij"
        Me.btnZamknij.Size = New System.Drawing.Size(75, 23)
        Me.btnZamknij.TabIndex = 7
        Me.btnZamknij.Text = "Button1"
        Me.btnZamknij.UseVisualStyleBackColor = True
        '
        'gbAdresy
        '
        Me.gbAdresy.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbAdresy.Controls.Add(Me.lblLicznik)
        Me.gbAdresy.Controls.Add(Me.chbAdresy)
        Me.gbAdresy.Controls.Add(Me.dgvAdresy)
        Me.gbAdresy.Location = New System.Drawing.Point(13, 90)
        Me.gbAdresy.Name = "gbAdresy"
        Me.gbAdresy.Size = New System.Drawing.Size(591, 180)
        Me.gbAdresy.TabIndex = 1
        Me.gbAdresy.TabStop = False
        Me.gbAdresy.Text = "Adresy"
        '
        'lblLicznik
        '
        Me.lblLicznik.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblLicznik.AutoSize = True
        Me.lblLicznik.Location = New System.Drawing.Point(6, 154)
        Me.lblLicznik.Name = "lblLicznik"
        Me.lblLicznik.Size = New System.Drawing.Size(39, 13)
        Me.lblLicznik.TabIndex = 2
        Me.lblLicznik.Text = "Label1"
        Me.lblLicznik.Visible = False
        '
        'chbAdresy
        '
        Me.chbAdresy.AutoSize = True
        Me.chbAdresy.Location = New System.Drawing.Point(6, 19)
        Me.chbAdresy.Name = "chbAdresy"
        Me.chbAdresy.Size = New System.Drawing.Size(196, 17)
        Me.chbAdresy.TabIndex = 0
        Me.chbAdresy.Text = "Zaznacz/Odznacz wszystkie adresy"
        Me.chbAdresy.UseVisualStyleBackColor = True
        '
        'dgvAdresy
        '
        Me.dgvAdresy.AllowUserToAddRows = False
        Me.dgvAdresy.AllowUserToDeleteRows = False
        Me.dgvAdresy.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvAdresy.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgvAdresy.BackgroundColor = System.Drawing.Color.White
        Me.dgvAdresy.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvAdresy.Location = New System.Drawing.Point(6, 42)
        Me.dgvAdresy.Name = "dgvAdresy"
        Me.dgvAdresy.RowHeadersVisible = False
        Me.dgvAdresy.Size = New System.Drawing.Size(578, 101)
        Me.dgvAdresy.TabIndex = 1
        '
        'frmAdresKopiuj
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnZamknij
        Me.ClientSize = New System.Drawing.Size(616, 468)
        Me.Controls.Add(Me.gbAdresy)
        Me.Controls.Add(Me.gbDokad)
        Me.Controls.Add(Me.gbSkad)
        Me.MinimumSize = New System.Drawing.Size(632, 506)
        Me.Name = "frmAdresKopiuj"
        Me.Text = "Kopiowanie adresów"
        Me.gbSkad.ResumeLayout(False)
        Me.gbSkad.PerformLayout()
        Me.gbDokad.ResumeLayout(False)
        Me.gbDokad.PerformLayout()
        CType(Me.dgvUserDokad, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbAdresy.ResumeLayout(False)
        Me.gbAdresy.PerformLayout()
        CType(Me.dgvAdresy, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gbSkad As System.Windows.Forms.GroupBox
    Friend WithEvents btnSciezkaExcel As System.Windows.Forms.Button
    Friend WithEvents txtSciezkaExcel As System.Windows.Forms.TextBox
    Friend WithEvents cmbUser As System.Windows.Forms.ComboBox
    Friend WithEvents rbExcel As System.Windows.Forms.RadioButton
    Friend WithEvents rbUser As System.Windows.Forms.RadioButton
    Friend WithEvents btnPobierzSzablon As System.Windows.Forms.Button
    Friend WithEvents gbDokad As System.Windows.Forms.GroupBox
    Friend WithEvents chbUser As System.Windows.Forms.CheckBox
    Friend WithEvents dgvUserDokad As System.Windows.Forms.DataGridView
    Friend WithEvents btnPobierzAdresy As System.Windows.Forms.Button
    Friend WithEvents gbAdresy As System.Windows.Forms.GroupBox
    Friend WithEvents btnExcel As System.Windows.Forms.Button
    Friend WithEvents btnKopiuj As System.Windows.Forms.Button
    Friend WithEvents chbAdresy As System.Windows.Forms.CheckBox
    Friend WithEvents dgvAdresy As System.Windows.Forms.DataGridView
    Friend WithEvents lblLicznik As System.Windows.Forms.Label
    Friend WithEvents btnUserFiltr As System.Windows.Forms.Button
    Friend WithEvents txtUserFiltr As System.Windows.Forms.TextBox
    Friend WithEvents lblLicznikUser As System.Windows.Forms.Label
    Friend WithEvents btnZamknij As System.Windows.Forms.Button
End Class
