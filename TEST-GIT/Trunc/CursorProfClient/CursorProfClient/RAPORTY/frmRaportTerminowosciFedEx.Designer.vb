<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRaportTerminowosciFedEx
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRaportTerminowosciFedEx))
        Me.lblDataOd = New System.Windows.Forms.Label()
        Me.dtpDataDo = New System.Windows.Forms.DateTimePicker()
        Me.lblDataDo = New System.Windows.Forms.Label()
        Me.dtpDataOd = New System.Windows.Forms.DateTimePicker()
        Me.btnGenerujRaport = New System.Windows.Forms.Button()
        Me.gbZakresDat = New System.Windows.Forms.GroupBox()
        Me.dgv = New System.Windows.Forms.DataGridView()
        Me.btnZamknij = New System.Windows.Forms.Button()
        Me.btnExportExcel = New System.Windows.Forms.Button()
        Me.gbZakresDat.SuspendLayout()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblDataOd
        '
        Me.lblDataOd.AutoSize = True
        Me.lblDataOd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(61, Byte), Integer))
        Me.lblDataOd.Location = New System.Drawing.Point(18, 24)
        Me.lblDataOd.Name = "lblDataOd"
        Me.lblDataOd.Size = New System.Drawing.Size(48, 13)
        Me.lblDataOd.TabIndex = 19
        Me.lblDataOd.Text = "Data od:"
        '
        'dtpDataDo
        '
        Me.dtpDataDo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDataDo.Location = New System.Drawing.Point(231, 21)
        Me.dtpDataDo.Name = "dtpDataDo"
        Me.dtpDataDo.Size = New System.Drawing.Size(98, 20)
        Me.dtpDataDo.TabIndex = 21
        Me.dtpDataDo.Value = New Date(2011, 10, 27, 11, 49, 46, 0)
        '
        'lblDataDo
        '
        Me.lblDataDo.AutoSize = True
        Me.lblDataDo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(61, Byte), Integer))
        Me.lblDataDo.Location = New System.Drawing.Point(177, 24)
        Me.lblDataDo.Name = "lblDataDo"
        Me.lblDataDo.Size = New System.Drawing.Size(48, 13)
        Me.lblDataDo.TabIndex = 20
        Me.lblDataDo.Text = "Data do:"
        '
        'dtpDataOd
        '
        Me.dtpDataOd.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDataOd.Location = New System.Drawing.Point(72, 21)
        Me.dtpDataOd.Name = "dtpDataOd"
        Me.dtpDataOd.Size = New System.Drawing.Size(98, 20)
        Me.dtpDataOd.TabIndex = 18
        '
        'btnGenerujRaport
        '
        Me.btnGenerujRaport.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnGenerujRaport.ForeColor = System.Drawing.Color.White
        Me.btnGenerujRaport.Location = New System.Drawing.Point(335, 19)
        Me.btnGenerujRaport.Name = "btnGenerujRaport"
        Me.btnGenerujRaport.Size = New System.Drawing.Size(149, 23)
        Me.btnGenerujRaport.TabIndex = 17
        Me.btnGenerujRaport.Text = "Generuj raport"
        Me.btnGenerujRaport.UseVisualStyleBackColor = False
        '
        'gbZakresDat
        '
        Me.gbZakresDat.Controls.Add(Me.btnGenerujRaport)
        Me.gbZakresDat.Controls.Add(Me.lblDataOd)
        Me.gbZakresDat.Controls.Add(Me.dtpDataOd)
        Me.gbZakresDat.Controls.Add(Me.dtpDataDo)
        Me.gbZakresDat.Controls.Add(Me.lblDataDo)
        Me.gbZakresDat.Location = New System.Drawing.Point(12, 12)
        Me.gbZakresDat.Name = "gbZakresDat"
        Me.gbZakresDat.Size = New System.Drawing.Size(493, 56)
        Me.gbZakresDat.TabIndex = 22
        Me.gbZakresDat.TabStop = False
        Me.gbZakresDat.Text = "Zakres dat złożenia zamówienia"
        '
        'dgv
        '
        Me.dgv.AllowUserToAddRows = False
        Me.dgv.AllowUserToDeleteRows = False
        Me.dgv.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue
        Me.dgv.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgv.BackgroundColor = System.Drawing.Color.White
        Me.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv.Location = New System.Drawing.Point(12, 74)
        Me.dgv.Name = "dgv"
        Me.dgv.ReadOnly = True
        Me.dgv.RowHeadersVisible = False
        Me.dgv.Size = New System.Drawing.Size(1066, 419)
        Me.dgv.TabIndex = 23
        '
        'btnZamknij
        '
        Me.btnZamknij.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnZamknij.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnZamknij.ForeColor = System.Drawing.Color.White
        Me.btnZamknij.Location = New System.Drawing.Point(1003, 499)
        Me.btnZamknij.Name = "btnZamknij"
        Me.btnZamknij.Size = New System.Drawing.Size(75, 23)
        Me.btnZamknij.TabIndex = 24
        Me.btnZamknij.Text = "Zamknij"
        Me.btnZamknij.UseVisualStyleBackColor = False
        '
        'btnExportExcel
        '
        Me.btnExportExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExportExcel.BackColor = System.Drawing.Color.LightGray
        Me.btnExportExcel.Enabled = False
        Me.btnExportExcel.ForeColor = System.Drawing.Color.DimGray
        Me.btnExportExcel.Image = CType(resources.GetObject("btnExportExcel.Image"), System.Drawing.Image)
        Me.btnExportExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExportExcel.Location = New System.Drawing.Point(873, 499)
        Me.btnExportExcel.Name = "btnExportExcel"
        Me.btnExportExcel.Size = New System.Drawing.Size(124, 23)
        Me.btnExportExcel.TabIndex = 25
        Me.btnExportExcel.Text = "Export do Excela"
        Me.btnExportExcel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnExportExcel.UseVisualStyleBackColor = False
        '
        'frmRaportTerminowosciFedEx
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1090, 534)
        Me.Controls.Add(Me.btnExportExcel)
        Me.Controls.Add(Me.btnZamknij)
        Me.Controls.Add(Me.dgv)
        Me.Controls.Add(Me.gbZakresDat)
        Me.MinimumSize = New System.Drawing.Size(532, 245)
        Me.Name = "frmRaportTerminowosciFedEx"
        Me.Text = "Raport terminowości FedEx"
        Me.gbZakresDat.ResumeLayout(false)
        Me.gbZakresDat.PerformLayout
        CType(Me.dgv,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents lblDataOd As System.Windows.Forms.Label
    Friend WithEvents dtpDataDo As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblDataDo As System.Windows.Forms.Label
    Friend WithEvents dtpDataOd As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnGenerujRaport As System.Windows.Forms.Button
    Friend WithEvents gbZakresDat As System.Windows.Forms.GroupBox
    Friend WithEvents dgv As System.Windows.Forms.DataGridView
    Friend WithEvents btnExportExcel As System.Windows.Forms.Button
    Friend WithEvents btnZamknij As System.Windows.Forms.Button
End Class
