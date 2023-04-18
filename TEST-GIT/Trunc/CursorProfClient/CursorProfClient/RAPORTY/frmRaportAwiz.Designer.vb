<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRaportAwiz
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRaportAwiz))
        Me.btnZamknij = New System.Windows.Forms.Button()
        Me.dgv = New System.Windows.Forms.DataGridView()
        Me.dtpOd = New System.Windows.Forms.DateTimePicker()
        Me.lblOd = New System.Windows.Forms.Label()
        Me.lblDo = New System.Windows.Forms.Label()
        Me.dtpDo = New System.Windows.Forms.DateTimePicker()
        Me.btnGeneruj = New System.Windows.Forms.Button()
        Me.lblIlePozycji = New System.Windows.Forms.Label()
        Me.btnRaportExcel = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnZamknij
        '
        Me.btnZamknij.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnZamknij.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnZamknij.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnZamknij.ForeColor = System.Drawing.Color.White
        Me.btnZamknij.Location = New System.Drawing.Point(517, 378)
        Me.btnZamknij.Name = "btnZamknij"
        Me.btnZamknij.Size = New System.Drawing.Size(75, 23)
        Me.btnZamknij.TabIndex = 8
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
        Me.dgv.Location = New System.Drawing.Point(12, 41)
        Me.dgv.Name = "dgv"
        Me.dgv.ReadOnly = True
        Me.dgv.RowHeadersVisible = False
        Me.dgv.Size = New System.Drawing.Size(580, 331)
        Me.dgv.TabIndex = 5
        '
        'dtpOd
        '
        Me.dtpOd.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpOd.Location = New System.Drawing.Point(148, 12)
        Me.dtpOd.Name = "dtpOd"
        Me.dtpOd.Size = New System.Drawing.Size(93, 20)
        Me.dtpOd.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.dtpOd, "Zakres dat przyjęcia towaru na stan")
        '
        'lblOd
        '
        Me.lblOd.AutoSize = True
        Me.lblOd.ForeColor = System.Drawing.Color.Black
        Me.lblOd.Location = New System.Drawing.Point(12, 17)
        Me.lblOd.Name = "lblOd"
        Me.lblOd.Size = New System.Drawing.Size(130, 13)
        Me.lblOd.TabIndex = 0
        Me.lblOd.Text = "Data przyjęcia na stan od:"
        Me.ToolTip1.SetToolTip(Me.lblOd, "Zakres dat przyjęcia towaru na stan")
        '
        'lblDo
        '
        Me.lblDo.AutoSize = True
        Me.lblDo.ForeColor = System.Drawing.Color.Black
        Me.lblDo.Location = New System.Drawing.Point(247, 17)
        Me.lblDo.Name = "lblDo"
        Me.lblDo.Size = New System.Drawing.Size(22, 13)
        Me.lblDo.TabIndex = 2
        Me.lblDo.Text = "do:"
        Me.ToolTip1.SetToolTip(Me.lblDo, "Zakres dat przyjęcia towaru na stan")
        '
        'dtpDo
        '
        Me.dtpDo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDo.Location = New System.Drawing.Point(275, 12)
        Me.dtpDo.Name = "dtpDo"
        Me.dtpDo.Size = New System.Drawing.Size(93, 20)
        Me.dtpDo.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.dtpDo, "Zakres dat przyjęcia towaru na stan")
        '
        'btnGeneruj
        '
        Me.btnGeneruj.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnGeneruj.ForeColor = System.Drawing.Color.White
        Me.btnGeneruj.Location = New System.Drawing.Point(374, 9)
        Me.btnGeneruj.Name = "btnGeneruj"
        Me.btnGeneruj.Size = New System.Drawing.Size(106, 23)
        Me.btnGeneruj.TabIndex = 4
        Me.btnGeneruj.Text = "Generuj"
        Me.ToolTip1.SetToolTip(Me.btnGeneruj, "Generuje raport awiz wg zakresu dat przyjęcia na stan")
        Me.btnGeneruj.UseVisualStyleBackColor = False
        '
        'lblIlePozycji
        '
        Me.lblIlePozycji.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblIlePozycji.AutoSize = True
        Me.lblIlePozycji.ForeColor = System.Drawing.Color.Black
        Me.lblIlePozycji.Location = New System.Drawing.Point(9, 375)
        Me.lblIlePozycji.Name = "lblIlePozycji"
        Me.lblIlePozycji.Size = New System.Drawing.Size(76, 13)
        Me.lblIlePozycji.TabIndex = 6
        Me.lblIlePozycji.Text = "Ilość pozycji: 0"
        '
        'btnRaportExcel
        '
        Me.btnRaportExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRaportExcel.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnRaportExcel.ForeColor = System.Drawing.Color.White
        Me.btnRaportExcel.Image = CType(resources.GetObject("btnRaportExcel.Image"), System.Drawing.Image)
        Me.btnRaportExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRaportExcel.Location = New System.Drawing.Point(387, 378)
        Me.btnRaportExcel.Name = "btnRaportExcel"
        Me.btnRaportExcel.Size = New System.Drawing.Size(124, 23)
        Me.btnRaportExcel.TabIndex = 7
        Me.btnRaportExcel.Text = "Export do Excela"
        Me.btnRaportExcel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnRaportExcel.UseVisualStyleBackColor = False
        '
        'ToolTip1
        '
        Me.ToolTip1.AutoPopDelay = 10000
        Me.ToolTip1.InitialDelay = 500
        Me.ToolTip1.ReshowDelay = 500
        '
        'frmRaportAwiz
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnZamknij
        Me.ClientSize = New System.Drawing.Size(604, 409)
        Me.Controls.Add(Me.lblIlePozycji)
        Me.Controls.Add(Me.btnGeneruj)
        Me.Controls.Add(Me.dtpDo)
        Me.Controls.Add(Me.lblDo)
        Me.Controls.Add(Me.lblOd)
        Me.Controls.Add(Me.dtpOd)
        Me.Controls.Add(Me.btnRaportExcel)
        Me.Controls.Add(Me.btnZamknij)
        Me.Controls.Add(Me.dgv)
        Me.MinimumSize = New System.Drawing.Size(509, 284)
        Me.Name = "frmRaportAwiz"
        Me.Text = "Raport awiz"
        CType(Me.dgv,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents btnRaportExcel As System.Windows.Forms.Button
    Friend WithEvents btnZamknij As System.Windows.Forms.Button
    Friend WithEvents dgv As System.Windows.Forms.DataGridView
    Friend WithEvents dtpOd As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblOd As System.Windows.Forms.Label
    Friend WithEvents lblDo As System.Windows.Forms.Label
    Friend WithEvents dtpDo As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnGeneruj As System.Windows.Forms.Button
    Friend WithEvents lblIlePozycji As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class
