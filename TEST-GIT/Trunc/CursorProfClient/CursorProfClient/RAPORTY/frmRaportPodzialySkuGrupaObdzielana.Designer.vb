<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRaportPodzialySkuGrupaObdzielana
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRaportPodzialySkuGrupaObdzielana))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.cmbListaSku = New System.Windows.Forms.ComboBox()
        Me.cmbGrupaObdzielana = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnWyszukaj = New System.Windows.Forms.Button()
        Me.btnRaportExcel = New System.Windows.Forms.Button()
        Me.btnZamknij = New System.Windows.Forms.Button()
        Me.dgv = New System.Windows.Forms.DataGridView()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbNazwaSku = New System.Windows.Forms.ComboBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmbListaSku
        '
        Me.cmbListaSku.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbListaSku.FormattingEnabled = True
        Me.cmbListaSku.Location = New System.Drawing.Point(106, 14)
        Me.cmbListaSku.Name = "cmbListaSku"
        Me.cmbListaSku.Size = New System.Drawing.Size(138, 21)
        Me.cmbListaSku.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.cmbListaSku, "Sku to kod produktu")
        '
        'cmbGrupaObdzielana
        '
        Me.cmbGrupaObdzielana.BackColor = System.Drawing.SystemColors.Window
        Me.cmbGrupaObdzielana.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbGrupaObdzielana.FormattingEnabled = True
        Me.cmbGrupaObdzielana.Location = New System.Drawing.Point(353, 14)
        Me.cmbGrupaObdzielana.Name = "cmbGrupaObdzielana"
        Me.cmbGrupaObdzielana.Size = New System.Drawing.Size(190, 21)
        Me.cmbGrupaObdzielana.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.cmbGrupaObdzielana, "Grupa obdzielana")
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(71, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(29, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Sku:"
        Me.ToolTip1.SetToolTip(Me.Label1, "Sku to kod produktu")
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(254, 17)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(93, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Grupa obdzielana:"
        '
        'btnWyszukaj
        '
        Me.btnWyszukaj.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnWyszukaj.ForeColor = System.Drawing.Color.White
        Me.btnWyszukaj.Location = New System.Drawing.Point(549, 12)
        Me.btnWyszukaj.Name = "btnWyszukaj"
        Me.btnWyszukaj.Size = New System.Drawing.Size(84, 23)
        Me.btnWyszukaj.TabIndex = 6
        Me.btnWyszukaj.Text = "Wyszukaj"
        Me.ToolTip1.SetToolTip(Me.btnWyszukaj, "Wyszukaj podziały wg wybranych filtrów")
        Me.btnWyszukaj.UseVisualStyleBackColor = False
        '
        'btnRaportExcel
        '
        Me.btnRaportExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRaportExcel.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnRaportExcel.ForeColor = System.Drawing.Color.White
        Me.btnRaportExcel.Image = CType(resources.GetObject("btnRaportExcel.Image"), System.Drawing.Image)
        Me.btnRaportExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRaportExcel.Location = New System.Drawing.Point(428, 281)
        Me.btnRaportExcel.Name = "btnRaportExcel"
        Me.btnRaportExcel.Size = New System.Drawing.Size(124, 23)
        Me.btnRaportExcel.TabIndex = 8
        Me.btnRaportExcel.Text = "Export do Excela"
        Me.btnRaportExcel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnRaportExcel.UseVisualStyleBackColor = False
        '
        'btnZamknij
        '
        Me.btnZamknij.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnZamknij.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnZamknij.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnZamknij.ForeColor = System.Drawing.Color.White
        Me.btnZamknij.Location = New System.Drawing.Point(558, 281)
        Me.btnZamknij.Name = "btnZamknij"
        Me.btnZamknij.Size = New System.Drawing.Size(75, 23)
        Me.btnZamknij.TabIndex = 9
        Me.btnZamknij.Text = "Zamknij"
        Me.btnZamknij.UseVisualStyleBackColor = False
        '
        'dgv
        '
        Me.dgv.AllowUserToAddRows = False
        Me.dgv.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue
        Me.dgv.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv.BackgroundColor = System.Drawing.Color.White
        Me.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv.Location = New System.Drawing.Point(5, 67)
        Me.dgv.Name = "dgv"
        Me.dgv.ReadOnly = True
        Me.dgv.RowHeadersVisible = False
        Me.dgv.Size = New System.Drawing.Size(628, 208)
        Me.dgv.TabIndex = 7
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 44)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(88, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Nazwa produktu:"
        '
        'cmbNazwaSku
        '
        Me.cmbNazwaSku.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbNazwaSku.FormattingEnabled = True
        Me.cmbNazwaSku.Location = New System.Drawing.Point(106, 41)
        Me.cmbNazwaSku.Name = "cmbNazwaSku"
        Me.cmbNazwaSku.Size = New System.Drawing.Size(437, 21)
        Me.cmbNazwaSku.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.cmbNazwaSku, "Nazwa produktu")
        '
        'ToolTip1
        '
        Me.ToolTip1.AutoPopDelay = 10000
        Me.ToolTip1.InitialDelay = 500
        Me.ToolTip1.ReshowDelay = 500
        '
        'frmRaportPodzialySkuGrupaObdzielana
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnZamknij
        Me.ClientSize = New System.Drawing.Size(641, 310)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cmbNazwaSku)
        Me.Controls.Add(Me.dgv)
        Me.Controls.Add(Me.btnRaportExcel)
        Me.Controls.Add(Me.btnZamknij)
        Me.Controls.Add(Me.btnWyszukaj)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmbGrupaObdzielana)
        Me.Controls.Add(Me.cmbListaSku)
        Me.MinimumSize = New System.Drawing.Size(657, 348)
        Me.Name = "frmRaportPodzialySkuGrupaObdzielana"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Raport podziałów towaru według SKU i grupy obdzielanej"
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmbListaSku As System.Windows.Forms.ComboBox
    Friend WithEvents cmbGrupaObdzielana As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnWyszukaj As System.Windows.Forms.Button
    Friend WithEvents btnRaportExcel As System.Windows.Forms.Button
    Friend WithEvents btnZamknij As System.Windows.Forms.Button
    Friend WithEvents dgv As System.Windows.Forms.DataGridView
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbNazwaSku As System.Windows.Forms.ComboBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class
