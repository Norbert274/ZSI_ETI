<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRaportPodzialyZamowieniaPerUser
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRaportPodzialyZamowieniaPerUser))
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbUsers = New System.Windows.Forms.ComboBox()
        Me.dgv = New System.Windows.Forms.DataGridView()
        Me.btnZamknij = New System.Windows.Forms.Button()
        Me.btnWyszukaj = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnRaportExcel = New System.Windows.Forms.Button()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 15)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 13)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "Użytkownik:"
        '
        'cmbUsers
        '
        Me.cmbUsers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbUsers.FormattingEnabled = True
        Me.cmbUsers.Location = New System.Drawing.Point(81, 12)
        Me.cmbUsers.Name = "cmbUsers"
        Me.cmbUsers.Size = New System.Drawing.Size(437, 21)
        Me.cmbUsers.TabIndex = 15
        Me.ToolTip1.SetToolTip(Me.cmbUsers, "Nazwa produktu")
        '
        'dgv
        '
        Me.dgv.AllowUserToAddRows = False
        Me.dgv.AllowUserToDeleteRows = False
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.AliceBlue
        Me.dgv.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle3
        Me.dgv.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv.BackgroundColor = System.Drawing.Color.White
        Me.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv.Location = New System.Drawing.Point(12, 39)
        Me.dgv.Name = "dgv"
        Me.dgv.ReadOnly = True
        Me.dgv.RowHeadersVisible = False
        Me.dgv.Size = New System.Drawing.Size(700, 365)
        Me.dgv.TabIndex = 17
        '
        'btnZamknij
        '
        Me.btnZamknij.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnZamknij.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnZamknij.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnZamknij.ForeColor = System.Drawing.Color.White
        Me.btnZamknij.Location = New System.Drawing.Point(637, 410)
        Me.btnZamknij.Name = "btnZamknij"
        Me.btnZamknij.Size = New System.Drawing.Size(75, 23)
        Me.btnZamknij.TabIndex = 19
        Me.btnZamknij.Text = "Zamknij"
        Me.btnZamknij.UseVisualStyleBackColor = False
        '
        'btnWyszukaj
        '
        Me.btnWyszukaj.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnWyszukaj.ForeColor = System.Drawing.Color.White
        Me.btnWyszukaj.Location = New System.Drawing.Point(524, 10)
        Me.btnWyszukaj.Name = "btnWyszukaj"
        Me.btnWyszukaj.Size = New System.Drawing.Size(84, 23)
        Me.btnWyszukaj.TabIndex = 16
        Me.btnWyszukaj.Text = "Wyszukaj"
        Me.ToolTip1.SetToolTip(Me.btnWyszukaj, "Wyszukaj podziały wg wybranych filtrów")
        Me.btnWyszukaj.UseVisualStyleBackColor = False
        '
        'ToolTip1
        '
        Me.ToolTip1.AutoPopDelay = 10000
        Me.ToolTip1.InitialDelay = 500
        Me.ToolTip1.ReshowDelay = 500
        '
        'btnRaportExcel
        '
        Me.btnRaportExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRaportExcel.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnRaportExcel.ForeColor = System.Drawing.Color.White
        Me.btnRaportExcel.Image = CType(resources.GetObject("btnRaportExcel.Image"), System.Drawing.Image)
        Me.btnRaportExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRaportExcel.Location = New System.Drawing.Point(507, 410)
        Me.btnRaportExcel.Name = "btnRaportExcel"
        Me.btnRaportExcel.Size = New System.Drawing.Size(124, 23)
        Me.btnRaportExcel.TabIndex = 18
        Me.btnRaportExcel.Text = "Export do Excela"
        Me.btnRaportExcel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnRaportExcel.UseVisualStyleBackColor = False
        '
        'frmRaportPodzialyZamowieniaPerUser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(724, 445)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cmbUsers)
        Me.Controls.Add(Me.dgv)
        Me.Controls.Add(Me.btnRaportExcel)
        Me.Controls.Add(Me.btnZamknij)
        Me.Controls.Add(Me.btnWyszukaj)
        Me.Name = "frmRaportPodzialyZamowieniaPerUser"
        Me.Text = "Raport podziały-zamówienia per użytkownik"
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbUsers As System.Windows.Forms.ComboBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents dgv As System.Windows.Forms.DataGridView
    Friend WithEvents btnRaportExcel As System.Windows.Forms.Button
    Friend WithEvents btnZamknij As System.Windows.Forms.Button
    Friend WithEvents btnWyszukaj As System.Windows.Forms.Button
End Class
