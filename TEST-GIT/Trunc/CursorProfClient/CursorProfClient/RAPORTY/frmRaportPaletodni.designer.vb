<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRaportPaletodni
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRaportPaletodni))
        Me.dgvPaletodni = New System.Windows.Forms.DataGridView()
        Me.btnZamknij = New System.Windows.Forms.Button()
        Me.btnRaportExcel = New System.Windows.Forms.Button()
        CType(Me.dgvPaletodni, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvPaletodni
        '
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue
        Me.dgvPaletodni.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvPaletodni.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvPaletodni.BackgroundColor = System.Drawing.Color.White
        Me.dgvPaletodni.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPaletodni.Location = New System.Drawing.Point(1, 2)
        Me.dgvPaletodni.Name = "dgvPaletodni"
        Me.dgvPaletodni.RowHeadersVisible = False
        Me.dgvPaletodni.Size = New System.Drawing.Size(999, 520)
        Me.dgvPaletodni.TabIndex = 12
        '
        'btnZamknij
        '
        Me.btnZamknij.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnZamknij.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnZamknij.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnZamknij.ForeColor = System.Drawing.Color.White
        Me.btnZamknij.Location = New System.Drawing.Point(925, 525)
        Me.btnZamknij.Name = "btnZamknij"
        Me.btnZamknij.Size = New System.Drawing.Size(75, 23)
        Me.btnZamknij.TabIndex = 13
        Me.btnZamknij.Text = "Zamknij"
        Me.btnZamknij.UseVisualStyleBackColor = False
        '
        'btnRaportExcel
        '
        Me.btnRaportExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRaportExcel.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnRaportExcel.ForeColor = System.Drawing.Color.White
        Me.btnRaportExcel.Image = CType(resources.GetObject("btnRaportExcel.Image"), System.Drawing.Image)
        Me.btnRaportExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRaportExcel.Location = New System.Drawing.Point(795, 525)
        Me.btnRaportExcel.Name = "btnRaportExcel"
        Me.btnRaportExcel.Size = New System.Drawing.Size(124, 23)
        Me.btnRaportExcel.TabIndex = 14
        Me.btnRaportExcel.Text = "Export do Excela"
        Me.btnRaportExcel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnRaportExcel.UseVisualStyleBackColor = False
        '
        'frmRaportPaletodni
        '
        Me.AcceptButton = Me.btnRaportExcel
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnZamknij
        Me.ClientSize = New System.Drawing.Size(1002, 550)
        Me.Controls.Add(Me.btnRaportExcel)
        Me.Controls.Add(Me.btnZamknij)
        Me.Controls.Add(Me.dgvPaletodni)
        Me.Location = New System.Drawing.Point(240, 50)
        Me.Name = "frmRaportPaletodni"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Raport zamówień"
        CType(Me.dgvPaletodni, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgvPaletodni As System.Windows.Forms.DataGridView
    Friend WithEvents btnZamknij As System.Windows.Forms.Button
    Friend WithEvents btnRaportExcel As System.Windows.Forms.Button
End Class
