<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSlownikWielkosc
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
        Me.btnNowy = New System.Windows.Forms.Button()
        Me.frmEdytuj = New System.Windows.Forms.Button()
        Me.btnUsun = New System.Windows.Forms.Button()
        Me.btnAnuluj = New System.Windows.Forms.Button()
        Me.btnZastosuj = New System.Windows.Forms.Button()
        Me.dgv = New System.Windows.Forms.DataGridView()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnNowy
        '
        Me.btnNowy.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnNowy.ForeColor = System.Drawing.Color.White
        Me.btnNowy.Location = New System.Drawing.Point(14, 12)
        Me.btnNowy.Name = "btnNowy"
        Me.btnNowy.Size = New System.Drawing.Size(75, 23)
        Me.btnNowy.TabIndex = 0
        Me.btnNowy.Text = "Nowy"
        Me.btnNowy.UseVisualStyleBackColor = False
        '
        'frmEdytuj
        '
        Me.frmEdytuj.BackColor = System.Drawing.Color.DodgerBlue
        Me.frmEdytuj.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.frmEdytuj.ForeColor = System.Drawing.Color.White
        Me.frmEdytuj.Location = New System.Drawing.Point(101, 12)
        Me.frmEdytuj.Name = "frmEdytuj"
        Me.frmEdytuj.Size = New System.Drawing.Size(75, 23)
        Me.frmEdytuj.TabIndex = 1
        Me.frmEdytuj.Text = "Edytuj"
        Me.frmEdytuj.UseVisualStyleBackColor = False
        '
        'btnUsun
        '
        Me.btnUsun.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnUsun.ForeColor = System.Drawing.Color.White
        Me.btnUsun.Location = New System.Drawing.Point(186, 12)
        Me.btnUsun.Name = "btnUsun"
        Me.btnUsun.Size = New System.Drawing.Size(75, 23)
        Me.btnUsun.TabIndex = 2
        Me.btnUsun.Text = "Usuń"
        Me.btnUsun.UseVisualStyleBackColor = False
        '
        'btnAnuluj
        '
        Me.btnAnuluj.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAnuluj.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnAnuluj.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnAnuluj.ForeColor = System.Drawing.Color.White
        Me.btnAnuluj.Location = New System.Drawing.Point(186, 215)
        Me.btnAnuluj.Name = "btnAnuluj"
        Me.btnAnuluj.Size = New System.Drawing.Size(75, 23)
        Me.btnAnuluj.TabIndex = 5
        Me.btnAnuluj.Text = "Zamknij"
        Me.btnAnuluj.UseVisualStyleBackColor = False
        '
        'btnZastosuj
        '
        Me.btnZastosuj.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnZastosuj.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnZastosuj.ForeColor = System.Drawing.Color.White
        Me.btnZastosuj.Location = New System.Drawing.Point(105, 215)
        Me.btnZastosuj.Name = "btnZastosuj"
        Me.btnZastosuj.Size = New System.Drawing.Size(75, 23)
        Me.btnZastosuj.TabIndex = 4
        Me.btnZastosuj.Text = "Odśwież"
        Me.btnZastosuj.UseVisualStyleBackColor = False
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
        Me.dgv.Location = New System.Drawing.Point(16, 41)
        Me.dgv.MultiSelect = False
        Me.dgv.Name = "dgv"
        Me.dgv.ReadOnly = True
        Me.dgv.RowHeadersVisible = False
        Me.dgv.Size = New System.Drawing.Size(245, 168)
        Me.dgv.TabIndex = 3
        '
        'frmSlownikWielkosc
        '
        Me.AcceptButton = Me.btnZastosuj
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnAnuluj
        Me.ClientSize = New System.Drawing.Size(273, 246)
        Me.Controls.Add(Me.btnNowy)
        Me.Controls.Add(Me.frmEdytuj)
        Me.Controls.Add(Me.btnUsun)
        Me.Controls.Add(Me.btnAnuluj)
        Me.Controls.Add(Me.btnZastosuj)
        Me.Controls.Add(Me.dgv)
        Me.Name = "frmSlownikWielkosc"
        Me.Text = "Słownik - wielkości"
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnNowy As System.Windows.Forms.Button
    Friend WithEvents frmEdytuj As System.Windows.Forms.Button
    Friend WithEvents btnUsun As System.Windows.Forms.Button
    Friend WithEvents btnAnuluj As System.Windows.Forms.Button
    Friend WithEvents btnZastosuj As System.Windows.Forms.Button
    Friend WithEvents dgv As System.Windows.Forms.DataGridView
End Class
