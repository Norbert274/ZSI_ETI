<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSlownikZespolySprzedazy
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
        Me.dgv = New System.Windows.Forms.DataGridView()
        Me.btnAnuluj = New System.Windows.Forms.Button()
        Me.btnZastosuj = New System.Windows.Forms.Button()
        Me.btnNowy = New System.Windows.Forms.Button()
        Me.frmEdytuj = New System.Windows.Forms.Button()
        Me.btnUsun = New System.Windows.Forms.Button()
        Me.btnSzablon = New System.Windows.Forms.Button()
        Me.btnFromExcel = New System.Windows.Forms.Button()
        Me.cmbSlowniki = New System.Windows.Forms.ComboBox()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgv
        '
        Me.dgv.AllowUserToAddRows = False
        Me.dgv.AllowUserToDeleteRows = False
        Me.dgv.AllowUserToResizeRows = False
        Me.dgv.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv.BackgroundColor = System.Drawing.Color.White
        Me.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv.Location = New System.Drawing.Point(12, 33)
        Me.dgv.MultiSelect = False
        Me.dgv.Name = "dgv"
        Me.dgv.ReadOnly = True
        Me.dgv.RowHeadersVisible = False
        Me.dgv.Size = New System.Drawing.Size(481, 201)
        Me.dgv.TabIndex = 3
        '
        'btnAnuluj
        '
        Me.btnAnuluj.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAnuluj.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnAnuluj.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnAnuluj.ForeColor = System.Drawing.Color.White
        Me.btnAnuluj.Location = New System.Drawing.Point(418, 240)
        Me.btnAnuluj.Name = "btnAnuluj"
        Me.btnAnuluj.Size = New System.Drawing.Size(75, 23)
        Me.btnAnuluj.TabIndex = 8
        Me.btnAnuluj.Text = "Zamknij"
        Me.btnAnuluj.UseVisualStyleBackColor = False
        '
        'btnZastosuj
        '
        Me.btnZastosuj.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnZastosuj.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnZastosuj.ForeColor = System.Drawing.Color.White
        Me.btnZastosuj.Location = New System.Drawing.Point(337, 240)
        Me.btnZastosuj.Name = "btnZastosuj"
        Me.btnZastosuj.Size = New System.Drawing.Size(75, 23)
        Me.btnZastosuj.TabIndex = 7
        Me.btnZastosuj.Text = "Odśwież"
        Me.btnZastosuj.UseVisualStyleBackColor = False
        '
        'btnNowy
        '
        Me.btnNowy.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnNowy.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnNowy.ForeColor = System.Drawing.Color.White
        Me.btnNowy.Location = New System.Drawing.Point(12, 240)
        Me.btnNowy.Name = "btnNowy"
        Me.btnNowy.Size = New System.Drawing.Size(75, 23)
        Me.btnNowy.TabIndex = 4
        Me.btnNowy.Text = "Nowy"
        Me.btnNowy.UseVisualStyleBackColor = False
        '
        'frmEdytuj
        '
        Me.frmEdytuj.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.frmEdytuj.BackColor = System.Drawing.Color.DodgerBlue
        Me.frmEdytuj.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.frmEdytuj.ForeColor = System.Drawing.Color.White
        Me.frmEdytuj.Location = New System.Drawing.Point(93, 240)
        Me.frmEdytuj.Name = "frmEdytuj"
        Me.frmEdytuj.Size = New System.Drawing.Size(75, 23)
        Me.frmEdytuj.TabIndex = 5
        Me.frmEdytuj.Text = "Edytuj"
        Me.frmEdytuj.UseVisualStyleBackColor = False
        '
        'btnUsun
        '
        Me.btnUsun.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnUsun.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnUsun.ForeColor = System.Drawing.Color.White
        Me.btnUsun.Location = New System.Drawing.Point(174, 240)
        Me.btnUsun.Name = "btnUsun"
        Me.btnUsun.Size = New System.Drawing.Size(75, 23)
        Me.btnUsun.TabIndex = 6
        Me.btnUsun.Text = "Usuń"
        Me.btnUsun.UseVisualStyleBackColor = False
        '
        'btnSzablon
        '
        Me.btnSzablon.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSzablon.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnSzablon.ForeColor = System.Drawing.Color.White
        Me.btnSzablon.Location = New System.Drawing.Point(338, 4)
        Me.btnSzablon.Name = "btnSzablon"
        Me.btnSzablon.Size = New System.Drawing.Size(75, 23)
        Me.btnSzablon.TabIndex = 1
        Me.btnSzablon.Text = "Szablon"
        Me.btnSzablon.UseVisualStyleBackColor = False
        '
        'btnFromExcel
        '
        Me.btnFromExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnFromExcel.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnFromExcel.ForeColor = System.Drawing.Color.White
        Me.btnFromExcel.Location = New System.Drawing.Point(419, 4)
        Me.btnFromExcel.MinimumSize = New System.Drawing.Size(75, 23)
        Me.btnFromExcel.Name = "btnFromExcel"
        Me.btnFromExcel.Size = New System.Drawing.Size(75, 23)
        Me.btnFromExcel.TabIndex = 2
        Me.btnFromExcel.Text = "Z pliku"
        Me.btnFromExcel.UseVisualStyleBackColor = False
        '
        'cmbSlowniki
        '
        Me.cmbSlowniki.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbSlowniki.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSlowniki.FormattingEnabled = True
        Me.cmbSlowniki.Location = New System.Drawing.Point(12, 6)
        Me.cmbSlowniki.Name = "cmbSlowniki"
        Me.cmbSlowniki.Size = New System.Drawing.Size(320, 21)
        Me.cmbSlowniki.TabIndex = 0
        '
        'frmSlownikZespolySprzedazy
        '
        Me.AcceptButton = Me.btnZastosuj
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnAnuluj
        Me.ClientSize = New System.Drawing.Size(505, 268)
        Me.Controls.Add(Me.cmbSlowniki)
        Me.Controls.Add(Me.btnFromExcel)
        Me.Controls.Add(Me.btnSzablon)
        Me.Controls.Add(Me.btnNowy)
        Me.Controls.Add(Me.frmEdytuj)
        Me.Controls.Add(Me.btnUsun)
        Me.Controls.Add(Me.btnAnuluj)
        Me.Controls.Add(Me.btnZastosuj)
        Me.Controls.Add(Me.dgv)
        Me.MinimumSize = New System.Drawing.Size(521, 306)
        Me.Name = "frmSlownikZespolySprzedazy"
        Me.Text = "Slownik - Zespoły sprzedaży"
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgv As System.Windows.Forms.DataGridView
    Friend WithEvents btnAnuluj As System.Windows.Forms.Button
    Friend WithEvents btnZastosuj As System.Windows.Forms.Button
    Friend WithEvents btnNowy As System.Windows.Forms.Button
    Friend WithEvents frmEdytuj As System.Windows.Forms.Button
    Friend WithEvents btnUsun As System.Windows.Forms.Button
    Friend WithEvents btnSzablon As System.Windows.Forms.Button
    Friend WithEvents btnFromExcel As System.Windows.Forms.Button
    Friend WithEvents cmbSlowniki As System.Windows.Forms.ComboBox
End Class
