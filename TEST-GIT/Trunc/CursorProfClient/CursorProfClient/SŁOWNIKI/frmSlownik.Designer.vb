<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSlownik
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
        Me.cmbSlowniki = New System.Windows.Forms.ComboBox()
        Me.btnSzablon = New System.Windows.Forms.Button()
        Me.btnFromExcel = New System.Windows.Forms.Button()
        Me.btnNowy = New System.Windows.Forms.Button()
        Me.btnEdytuj = New System.Windows.Forms.Button()
        Me.btnUsun = New System.Windows.Forms.Button()
        Me.btnZamknij = New System.Windows.Forms.Button()
        Me.btnToExcel = New System.Windows.Forms.Button()
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
        Me.dgv.Location = New System.Drawing.Point(12, 34)
        Me.dgv.MultiSelect = False
        Me.dgv.Name = "dgv"
        Me.dgv.ReadOnly = True
        Me.dgv.RowHeadersVisible = False
        Me.dgv.Size = New System.Drawing.Size(481, 201)
        Me.dgv.TabIndex = 3
        '
        'cmbSlowniki
        '
        Me.cmbSlowniki.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbSlowniki.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSlowniki.FormattingEnabled = True
        Me.cmbSlowniki.Location = New System.Drawing.Point(12, 7)
        Me.cmbSlowniki.Name = "cmbSlowniki"
        Me.cmbSlowniki.Size = New System.Drawing.Size(320, 21)
        Me.cmbSlowniki.TabIndex = 0
        '
        'btnSzablon
        '
        Me.btnSzablon.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSzablon.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnSzablon.ForeColor = System.Drawing.Color.White
        Me.btnSzablon.Location = New System.Drawing.Point(338, 5)
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
        Me.btnFromExcel.Location = New System.Drawing.Point(418, 5)
        Me.btnFromExcel.MinimumSize = New System.Drawing.Size(75, 23)
        Me.btnFromExcel.Name = "btnFromExcel"
        Me.btnFromExcel.Size = New System.Drawing.Size(75, 23)
        Me.btnFromExcel.TabIndex = 2
        Me.btnFromExcel.Text = "Z pliku"
        Me.btnFromExcel.UseVisualStyleBackColor = False
        '
        'btnNowy
        '
        Me.btnNowy.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnNowy.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnNowy.ForeColor = System.Drawing.Color.White
        Me.btnNowy.Location = New System.Drawing.Point(12, 241)
        Me.btnNowy.Name = "btnNowy"
        Me.btnNowy.Size = New System.Drawing.Size(75, 23)
        Me.btnNowy.TabIndex = 4
        Me.btnNowy.Text = "Nowy"
        Me.btnNowy.UseVisualStyleBackColor = False
        '
        'btnEdytuj
        '
        Me.btnEdytuj.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnEdytuj.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnEdytuj.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnEdytuj.ForeColor = System.Drawing.Color.White
        Me.btnEdytuj.Location = New System.Drawing.Point(93, 241)
        Me.btnEdytuj.Name = "btnEdytuj"
        Me.btnEdytuj.Size = New System.Drawing.Size(75, 23)
        Me.btnEdytuj.TabIndex = 5
        Me.btnEdytuj.Text = "Edytuj"
        Me.btnEdytuj.UseVisualStyleBackColor = False
        '
        'btnUsun
        '
        Me.btnUsun.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnUsun.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnUsun.ForeColor = System.Drawing.Color.White
        Me.btnUsun.Location = New System.Drawing.Point(174, 241)
        Me.btnUsun.Name = "btnUsun"
        Me.btnUsun.Size = New System.Drawing.Size(75, 23)
        Me.btnUsun.TabIndex = 6
        Me.btnUsun.Text = "Usuń"
        Me.btnUsun.UseVisualStyleBackColor = False
        '
        'btnZamknij
        '
        Me.btnZamknij.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnZamknij.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnZamknij.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnZamknij.ForeColor = System.Drawing.Color.White
        Me.btnZamknij.Location = New System.Drawing.Point(419, 241)
        Me.btnZamknij.Name = "btnZamknij"
        Me.btnZamknij.Size = New System.Drawing.Size(75, 23)
        Me.btnZamknij.TabIndex = 8
        Me.btnZamknij.Text = "Zamknij"
        Me.btnZamknij.UseVisualStyleBackColor = False
        '
        'btnToExcel
        '
        Me.btnToExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnToExcel.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnToExcel.ForeColor = System.Drawing.Color.White
        Me.btnToExcel.Location = New System.Drawing.Point(338, 241)
        Me.btnToExcel.Name = "btnToExcel"
        Me.btnToExcel.Size = New System.Drawing.Size(75, 23)
        Me.btnToExcel.TabIndex = 7
        Me.btnToExcel.Text = "Do pliku"
        Me.btnToExcel.UseVisualStyleBackColor = False
        '
        'frmSlownik
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnZamknij
        Me.ClientSize = New System.Drawing.Size(505, 268)
        Me.Controls.Add(Me.btnToExcel)
        Me.Controls.Add(Me.btnZamknij)
        Me.Controls.Add(Me.btnUsun)
        Me.Controls.Add(Me.btnEdytuj)
        Me.Controls.Add(Me.btnNowy)
        Me.Controls.Add(Me.btnFromExcel)
        Me.Controls.Add(Me.btnSzablon)
        Me.Controls.Add(Me.cmbSlowniki)
        Me.Controls.Add(Me.dgv)
        Me.MinimumSize = New System.Drawing.Size(521, 306)
        Me.Name = "frmSlownik"
        Me.Text = "Słownik"
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgv As System.Windows.Forms.DataGridView
    Friend WithEvents cmbSlowniki As System.Windows.Forms.ComboBox
    Friend WithEvents btnSzablon As System.Windows.Forms.Button
    Friend WithEvents btnFromExcel As System.Windows.Forms.Button
    Friend WithEvents btnNowy As System.Windows.Forms.Button
    Friend WithEvents btnEdytuj As System.Windows.Forms.Button
    Friend WithEvents btnUsun As System.Windows.Forms.Button
    Friend WithEvents btnZamknij As System.Windows.Forms.Button
    Friend WithEvents btnToExcel As System.Windows.Forms.Button
End Class
