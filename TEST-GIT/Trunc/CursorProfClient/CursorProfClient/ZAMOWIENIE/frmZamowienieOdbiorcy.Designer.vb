<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmZamowienieOdbiorcy
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
        Me.lblUzytkownicy = New System.Windows.Forms.Label()
        Me.dgv = New System.Windows.Forms.DataGridView()
        Me.chkListTyp = New System.Windows.Forms.CheckedListBox()
        Me.lblTyp = New System.Windows.Forms.Label()
        Me.chkListWielkosc = New System.Windows.Forms.CheckedListBox()
        Me.lblWielkosc = New System.Windows.Forms.Label()
        Me.lblGrupy = New System.Windows.Forms.Label()
        Me.chkListGrupy = New System.Windows.Forms.CheckedListBox()
        Me.btnAnuluj = New System.Windows.Forms.Button()
        Me.btnWybierz = New System.Windows.Forms.Button()
        Me.lbluzytkownicyLicznik = New System.Windows.Forms.Label()
        Me.chkAll = New System.Windows.Forms.CheckBox()
        Me.rbOr = New System.Windows.Forms.RadioButton()
        Me.rbAnd = New System.Windows.Forms.RadioButton()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.chbGrupyUser = New System.Windows.Forms.CheckBox()
        Me.chbWielkoscUser = New System.Windows.Forms.CheckBox()
        Me.chbTypyUser = New System.Windows.Forms.CheckBox()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblUzytkownicy
        '
        Me.lblUzytkownicy.AutoSize = True
        Me.lblUzytkownicy.ForeColor = System.Drawing.Color.Black
        Me.lblUzytkownicy.Location = New System.Drawing.Point(217, 9)
        Me.lblUzytkownicy.Name = "lblUzytkownicy"
        Me.lblUzytkownicy.Size = New System.Drawing.Size(70, 13)
        Me.lblUzytkownicy.TabIndex = 8
        Me.lblUzytkownicy.Text = "Użytkownicy:"
        '
        'dgv
        '
        Me.dgv.AllowUserToAddRows = False
        Me.dgv.AllowUserToDeleteRows = False
        Me.dgv.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv.BackgroundColor = System.Drawing.Color.White
        Me.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv.Location = New System.Drawing.Point(208, 48)
        Me.dgv.Name = "dgv"
        Me.dgv.Size = New System.Drawing.Size(807, 410)
        Me.dgv.TabIndex = 11
        '
        'chkListTyp
        '
        Me.chkListTyp.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkListTyp.CheckOnClick = True
        Me.chkListTyp.ForeColor = System.Drawing.Color.Black
        Me.chkListTyp.FormattingEnabled = True
        Me.chkListTyp.Location = New System.Drawing.Point(12, 281)
        Me.chkListTyp.Name = "chkListTyp"
        Me.chkListTyp.Size = New System.Drawing.Size(190, 64)
        Me.chkListTyp.TabIndex = 5
        '
        'lblTyp
        '
        Me.lblTyp.AutoSize = True
        Me.lblTyp.ForeColor = System.Drawing.Color.Black
        Me.lblTyp.Location = New System.Drawing.Point(12, 265)
        Me.lblTyp.Name = "lblTyp"
        Me.lblTyp.Size = New System.Drawing.Size(103, 13)
        Me.lblTyp.TabIndex = 4
        Me.lblTyp.Text = "Typy użytkowników:"
        '
        'chkListWielkosc
        '
        Me.chkListWielkosc.CheckOnClick = True
        Me.chkListWielkosc.ForeColor = System.Drawing.Color.Black
        Me.chkListWielkosc.FormattingEnabled = True
        Me.chkListWielkosc.Location = New System.Drawing.Point(12, 153)
        Me.chkListWielkosc.Name = "chkListWielkosc"
        Me.chkListWielkosc.Size = New System.Drawing.Size(190, 109)
        Me.chkListWielkosc.TabIndex = 3
        '
        'lblWielkosc
        '
        Me.lblWielkosc.AutoSize = True
        Me.lblWielkosc.ForeColor = System.Drawing.Color.Black
        Me.lblWielkosc.Location = New System.Drawing.Point(12, 137)
        Me.lblWielkosc.Name = "lblWielkosc"
        Me.lblWielkosc.Size = New System.Drawing.Size(124, 13)
        Me.lblWielkosc.TabIndex = 2
        Me.lblWielkosc.Text = "Wielkość użytkowników:"
        '
        'lblGrupy
        '
        Me.lblGrupy.AutoSize = True
        Me.lblGrupy.ForeColor = System.Drawing.Color.Black
        Me.lblGrupy.Location = New System.Drawing.Point(12, 9)
        Me.lblGrupy.Name = "lblGrupy"
        Me.lblGrupy.Size = New System.Drawing.Size(108, 13)
        Me.lblGrupy.TabIndex = 0
        Me.lblGrupy.Text = "Grupy użytkowników:"
        '
        'chkListGrupy
        '
        Me.chkListGrupy.CheckOnClick = True
        Me.chkListGrupy.ForeColor = System.Drawing.Color.Black
        Me.chkListGrupy.FormattingEnabled = True
        Me.chkListGrupy.Location = New System.Drawing.Point(12, 25)
        Me.chkListGrupy.Name = "chkListGrupy"
        Me.chkListGrupy.Size = New System.Drawing.Size(190, 109)
        Me.chkListGrupy.TabIndex = 1
        '
        'btnAnuluj
        '
        Me.btnAnuluj.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAnuluj.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnAnuluj.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnAnuluj.ForeColor = System.Drawing.Color.White
        Me.btnAnuluj.Location = New System.Drawing.Point(938, 472)
        Me.btnAnuluj.Name = "btnAnuluj"
        Me.btnAnuluj.Size = New System.Drawing.Size(77, 23)
        Me.btnAnuluj.TabIndex = 13
        Me.btnAnuluj.Text = "Anuluj"
        Me.btnAnuluj.UseVisualStyleBackColor = False
        '
        'btnWybierz
        '
        Me.btnWybierz.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnWybierz.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnWybierz.ForeColor = System.Drawing.Color.White
        Me.btnWybierz.Location = New System.Drawing.Point(855, 472)
        Me.btnWybierz.Name = "btnWybierz"
        Me.btnWybierz.Size = New System.Drawing.Size(77, 23)
        Me.btnWybierz.TabIndex = 12
        Me.btnWybierz.Text = "Wybierz"
        Me.btnWybierz.UseVisualStyleBackColor = False
        '
        'lbluzytkownicyLicznik
        '
        Me.lbluzytkownicyLicznik.AutoSize = True
        Me.lbluzytkownicyLicznik.Location = New System.Drawing.Point(241, 25)
        Me.lbluzytkownicyLicznik.Name = "lbluzytkownicyLicznik"
        Me.lbluzytkownicyLicznik.Size = New System.Drawing.Size(29, 13)
        Me.lbluzytkownicyLicznik.TabIndex = 10
        Me.lbluzytkownicyLicznik.Text = "Brak"
        '
        'chkAll
        '
        Me.chkAll.AutoSize = True
        Me.chkAll.Location = New System.Drawing.Point(220, 25)
        Me.chkAll.Name = "chkAll"
        Me.chkAll.Size = New System.Drawing.Size(15, 14)
        Me.chkAll.TabIndex = 9
        Me.chkAll.UseVisualStyleBackColor = True
        '
        'rbOr
        '
        Me.rbOr.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rbOr.AutoSize = True
        Me.rbOr.Checked = True
        Me.rbOr.Location = New System.Drawing.Point(12, 439)
        Me.rbOr.Name = "rbOr"
        Me.rbOr.Size = New System.Drawing.Size(43, 17)
        Me.rbOr.TabIndex = 6
        Me.rbOr.TabStop = True
        Me.rbOr.Text = "Lub"
        Me.ToolTip.SetToolTip(Me.rbOr, "Wyświetla wszystkich użytkowników który znajdują się w zaznaczonych grupach lub w" & _
        "ielkościach lub typach")
        Me.rbOr.UseVisualStyleBackColor = True
        '
        'rbAnd
        '
        Me.rbAnd.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rbAnd.AutoSize = True
        Me.rbAnd.Location = New System.Drawing.Point(61, 439)
        Me.rbAnd.Name = "rbAnd"
        Me.rbAnd.Size = New System.Drawing.Size(28, 17)
        Me.rbAnd.TabIndex = 7
        Me.rbAnd.Text = "I"
        Me.ToolTip.SetToolTip(Me.rbAnd, "Wyświetla tylko tych użytkowników którzy znajdują się w zaznaczonych grupach i wi" & _
        "elkościach i typach")
        Me.rbAnd.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 351)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(49, 13)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "Filtruj po:"
        '
        'chbGrupyUser
        '
        Me.chbGrupyUser.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chbGrupyUser.AutoSize = True
        Me.chbGrupyUser.Location = New System.Drawing.Point(12, 369)
        Me.chbGrupyUser.Name = "chbGrupyUser"
        Me.chbGrupyUser.Size = New System.Drawing.Size(124, 17)
        Me.chbGrupyUser.TabIndex = 15
        Me.chbGrupyUser.Text = "Grupy użytkowników"
        Me.chbGrupyUser.UseVisualStyleBackColor = True
        '
        'chbWielkoscUser
        '
        Me.chbWielkoscUser.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chbWielkoscUser.AutoSize = True
        Me.chbWielkoscUser.Location = New System.Drawing.Point(12, 392)
        Me.chbWielkoscUser.Name = "chbWielkoscUser"
        Me.chbWielkoscUser.Size = New System.Drawing.Size(140, 17)
        Me.chbWielkoscUser.TabIndex = 16
        Me.chbWielkoscUser.Text = "Wielkość użytkowników"
        Me.chbWielkoscUser.UseVisualStyleBackColor = True
        '
        'chbTypyUser
        '
        Me.chbTypyUser.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chbTypyUser.AutoSize = True
        Me.chbTypyUser.Location = New System.Drawing.Point(12, 415)
        Me.chbTypyUser.Name = "chbTypyUser"
        Me.chbTypyUser.Size = New System.Drawing.Size(119, 17)
        Me.chbTypyUser.TabIndex = 17
        Me.chbTypyUser.Text = "Typy użytkowników"
        Me.chbTypyUser.UseVisualStyleBackColor = True
        '
        'frmZamowienieOdbiorcy
        '
        Me.AcceptButton = Me.btnWybierz
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnAnuluj
        Me.ClientSize = New System.Drawing.Size(1027, 507)
        Me.Controls.Add(Me.chbTypyUser)
        Me.Controls.Add(Me.chbWielkoscUser)
        Me.Controls.Add(Me.chbGrupyUser)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.rbAnd)
        Me.Controls.Add(Me.rbOr)
        Me.Controls.Add(Me.chkAll)
        Me.Controls.Add(Me.lbluzytkownicyLicznik)
        Me.Controls.Add(Me.btnAnuluj)
        Me.Controls.Add(Me.btnWybierz)
        Me.Controls.Add(Me.lblUzytkownicy)
        Me.Controls.Add(Me.dgv)
        Me.Controls.Add(Me.chkListTyp)
        Me.Controls.Add(Me.lblTyp)
        Me.Controls.Add(Me.chkListWielkosc)
        Me.Controls.Add(Me.lblWielkosc)
        Me.Controls.Add(Me.lblGrupy)
        Me.Controls.Add(Me.chkListGrupy)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximumSize = New System.Drawing.Size(1033, 535)
        Me.MinimumSize = New System.Drawing.Size(1033, 535)
        Me.Name = "frmZamowienieOdbiorcy"
        Me.Text = "Odbiorcy"
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblUzytkownicy As System.Windows.Forms.Label
    Friend WithEvents dgv As System.Windows.Forms.DataGridView
    Friend WithEvents chkListTyp As System.Windows.Forms.CheckedListBox
    Friend WithEvents lblTyp As System.Windows.Forms.Label
    Friend WithEvents chkListWielkosc As System.Windows.Forms.CheckedListBox
    Friend WithEvents lblWielkosc As System.Windows.Forms.Label
    Friend WithEvents lblGrupy As System.Windows.Forms.Label
    Friend WithEvents chkListGrupy As System.Windows.Forms.CheckedListBox
    Friend WithEvents btnAnuluj As System.Windows.Forms.Button
    Friend WithEvents btnWybierz As System.Windows.Forms.Button
    Friend WithEvents lbluzytkownicyLicznik As System.Windows.Forms.Label
    Friend WithEvents chkAll As System.Windows.Forms.CheckBox
    Friend WithEvents rbOr As System.Windows.Forms.RadioButton
    Friend WithEvents rbAnd As System.Windows.Forms.RadioButton
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents chbGrupyUser As System.Windows.Forms.CheckBox
    Friend WithEvents chbWielkoscUser As System.Windows.Forms.CheckBox
    Friend WithEvents chbTypyUser As System.Windows.Forms.CheckBox
End Class
