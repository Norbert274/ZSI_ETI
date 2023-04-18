<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRaportHistoriiMaterialu
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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRaportHistoriiMaterialu))
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.btnZamknij = New System.Windows.Forms.Button()
        Me.dgvRaport = New System.Windows.Forms.DataGridView()
        Me.btnRaportExcel = New System.Windows.Forms.Button()
        Me.btnGenerujRaport = New System.Windows.Forms.Button()
        Me.gbWybierzProdukt = New System.Windows.Forms.GroupBox()
        Me.txtFiltr = New System.Windows.Forms.TextBox()
        Me.lblFiltr = New System.Windows.Forms.Label()
        Me.dgvProdukty = New System.Windows.Forms.DataGridView()
        Me.gbRaport = New System.Windows.Forms.GroupBox()
        Me.sc = New System.Windows.Forms.SplitContainer()
        CType(Me.dgvRaport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbWybierzProdukt.SuspendLayout()
        CType(Me.dgvProdukty, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbRaport.SuspendLayout()
        CType(Me.sc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sc.Panel1.SuspendLayout()
        Me.sc.Panel2.SuspendLayout()
        Me.sc.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnZamknij
        '
        Me.btnZamknij.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnZamknij.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnZamknij.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnZamknij.ForeColor = System.Drawing.Color.White
        Me.btnZamknij.Location = New System.Drawing.Point(546, 400)
        Me.btnZamknij.Name = "btnZamknij"
        Me.btnZamknij.Size = New System.Drawing.Size(96, 23)
        Me.btnZamknij.TabIndex = 2
        Me.btnZamknij.Text = "Zamknij"
        Me.btnZamknij.UseVisualStyleBackColor = False
        '
        'dgvRaport
        '
        Me.dgvRaport.AllowUserToAddRows = False
        Me.dgvRaport.AllowUserToDeleteRows = False
        Me.dgvRaport.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue
        Me.dgvRaport.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvRaport.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvRaport.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvRaport.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvRaport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvRaport.DefaultCellStyle = DataGridViewCellStyle3
        Me.dgvRaport.Location = New System.Drawing.Point(6, 18)
        Me.dgvRaport.Name = "dgvRaport"
        Me.dgvRaport.ReadOnly = True
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvRaport.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.dgvRaport.RowHeadersVisible = False
        Me.dgvRaport.Size = New System.Drawing.Size(636, 375)
        Me.dgvRaport.TabIndex = 0
        '
        'btnRaportExcel
        '
        Me.btnRaportExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRaportExcel.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnRaportExcel.ForeColor = System.Drawing.Color.White
        Me.btnRaportExcel.Image = CType(resources.GetObject("btnRaportExcel.Image"), System.Drawing.Image)
        Me.btnRaportExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRaportExcel.Location = New System.Drawing.Point(416, 400)
        Me.btnRaportExcel.Name = "btnRaportExcel"
        Me.btnRaportExcel.Size = New System.Drawing.Size(124, 23)
        Me.btnRaportExcel.TabIndex = 1
        Me.btnRaportExcel.Text = "Export do Excela"
        Me.btnRaportExcel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnRaportExcel.UseVisualStyleBackColor = False
        '
        'btnGenerujRaport
        '
        Me.btnGenerujRaport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGenerujRaport.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnGenerujRaport.ForeColor = System.Drawing.Color.White
        Me.btnGenerujRaport.Location = New System.Drawing.Point(169, 400)
        Me.btnGenerujRaport.Name = "btnGenerujRaport"
        Me.btnGenerujRaport.Size = New System.Drawing.Size(117, 23)
        Me.btnGenerujRaport.TabIndex = 3
        Me.btnGenerujRaport.Text = "Generuj raport"
        Me.btnGenerujRaport.UseVisualStyleBackColor = False
        '
        'gbWybierzProdukt
        '
        Me.gbWybierzProdukt.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbWybierzProdukt.Controls.Add(Me.txtFiltr)
        Me.gbWybierzProdukt.Controls.Add(Me.lblFiltr)
        Me.gbWybierzProdukt.Controls.Add(Me.dgvProdukty)
        Me.gbWybierzProdukt.Controls.Add(Me.btnGenerujRaport)
        Me.gbWybierzProdukt.Location = New System.Drawing.Point(3, 3)
        Me.gbWybierzProdukt.Name = "gbWybierzProdukt"
        Me.gbWybierzProdukt.Size = New System.Drawing.Size(292, 429)
        Me.gbWybierzProdukt.TabIndex = 0
        Me.gbWybierzProdukt.TabStop = False
        Me.gbWybierzProdukt.Text = "Wybierz produkty:"
        '
        'txtFiltr
        '
        Me.txtFiltr.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtFiltr.Location = New System.Drawing.Point(46, 18)
        Me.txtFiltr.Name = "txtFiltr"
        Me.txtFiltr.Size = New System.Drawing.Size(240, 20)
        Me.txtFiltr.TabIndex = 1
        '
        'lblFiltr
        '
        Me.lblFiltr.AutoSize = True
        Me.lblFiltr.Location = New System.Drawing.Point(6, 21)
        Me.lblFiltr.Name = "lblFiltr"
        Me.lblFiltr.Size = New System.Drawing.Size(34, 13)
        Me.lblFiltr.TabIndex = 0
        Me.lblFiltr.Text = "Filtruj:"
        '
        'dgvProdukty
        '
        Me.dgvProdukty.AllowUserToAddRows = False
        Me.dgvProdukty.AllowUserToDeleteRows = False
        Me.dgvProdukty.AllowUserToResizeRows = False
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.AliceBlue
        Me.dgvProdukty.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle5
        Me.dgvProdukty.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvProdukty.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgvProdukty.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvProdukty.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.dgvProdukty.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvProdukty.DefaultCellStyle = DataGridViewCellStyle7
        Me.dgvProdukty.Location = New System.Drawing.Point(4, 44)
        Me.dgvProdukty.Name = "dgvProdukty"
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvProdukty.RowHeadersDefaultCellStyle = DataGridViewCellStyle8
        Me.dgvProdukty.RowHeadersVisible = False
        Me.dgvProdukty.Size = New System.Drawing.Size(282, 349)
        Me.dgvProdukty.TabIndex = 2
        '
        'gbRaport
        '
        Me.gbRaport.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbRaport.Controls.Add(Me.dgvRaport)
        Me.gbRaport.Controls.Add(Me.btnZamknij)
        Me.gbRaport.Controls.Add(Me.btnRaportExcel)
        Me.gbRaport.Location = New System.Drawing.Point(3, 3)
        Me.gbRaport.Name = "gbRaport"
        Me.gbRaport.Size = New System.Drawing.Size(648, 429)
        Me.gbRaport.TabIndex = 0
        Me.gbRaport.TabStop = False
        Me.gbRaport.Text = "Raport"
        '
        'sc
        '
        Me.sc.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.sc.BackColor = System.Drawing.Color.White
        Me.sc.Location = New System.Drawing.Point(4, 3)
        Me.sc.Name = "sc"
        '
        'sc.Panel1
        '
        Me.sc.Panel1.BackColor = System.Drawing.Color.White
        Me.sc.Panel1.Controls.Add(Me.gbWybierzProdukt)
        '
        'sc.Panel2
        '
        Me.sc.Panel2.BackColor = System.Drawing.Color.White
        Me.sc.Panel2.Controls.Add(Me.gbRaport)
        Me.sc.Size = New System.Drawing.Size(955, 435)
        Me.sc.SplitterDistance = 299
        Me.sc.SplitterWidth = 2
        Me.sc.TabIndex = 0
        '
        'frmRaportHistoriiMaterialu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnZamknij
        Me.ClientSize = New System.Drawing.Size(960, 444)
        Me.Controls.Add(Me.sc)
        Me.MinimumSize = New System.Drawing.Size(508, 280)
        Me.Name = "frmRaportHistoriiMaterialu"
        Me.Text = "Historia materiału"
        CType(Me.dgvRaport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbWybierzProdukt.ResumeLayout(False)
        Me.gbWybierzProdukt.PerformLayout()
        CType(Me.dgvProdukty, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbRaport.ResumeLayout(False)
        Me.sc.Panel1.ResumeLayout(False)
        Me.sc.Panel2.ResumeLayout(False)
        CType(Me.sc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sc.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnRaportExcel As System.Windows.Forms.Button
    Friend WithEvents btnZamknij As System.Windows.Forms.Button
    Friend WithEvents dgvRaport As System.Windows.Forms.DataGridView
    Friend WithEvents btnGenerujRaport As System.Windows.Forms.Button
    Friend WithEvents gbWybierzProdukt As System.Windows.Forms.GroupBox
    Friend WithEvents dgvProdukty As System.Windows.Forms.DataGridView
    Friend WithEvents gbRaport As System.Windows.Forms.GroupBox
    Friend WithEvents sc As System.Windows.Forms.SplitContainer
    Friend WithEvents txtFiltr As System.Windows.Forms.TextBox
    Friend WithEvents lblFiltr As System.Windows.Forms.Label
End Class
