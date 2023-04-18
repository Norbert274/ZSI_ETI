<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPlikiDoPobrania
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
        Me.lblListaPlikow = New System.Windows.Forms.Label()
        Me.dgvPliki = New System.Windows.Forms.DataGridView()
        Me.btnDodajPlik = New System.Windows.Forms.Button()
        Me.btnZamknij = New System.Windows.Forms.Button()
        Me.btnUsunPliki = New System.Windows.Forms.Button()
        CType(Me.dgvPliki, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblListaPlikow
        '
        Me.lblListaPlikow.AutoSize = True
        Me.lblListaPlikow.ForeColor = System.Drawing.Color.Black
        Me.lblListaPlikow.Location = New System.Drawing.Point(4, 6)
        Me.lblListaPlikow.Name = "lblListaPlikow"
        Me.lblListaPlikow.Size = New System.Drawing.Size(65, 13)
        Me.lblListaPlikow.TabIndex = 0
        Me.lblListaPlikow.Text = "Lista plików:"
        '
        'dgvPliki
        '
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue
        Me.dgvPliki.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvPliki.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvPliki.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgvPliki.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvPliki.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvPliki.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvPliki.DefaultCellStyle = DataGridViewCellStyle3
        Me.dgvPliki.Location = New System.Drawing.Point(5, 26)
        Me.dgvPliki.Name = "dgvPliki"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvPliki.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.dgvPliki.RowHeadersVisible = False
        Me.dgvPliki.Size = New System.Drawing.Size(356, 263)
        Me.dgvPliki.TabIndex = 1
        '
        'btnDodajPlik
        '
        Me.btnDodajPlik.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDodajPlik.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnDodajPlik.ForeColor = System.Drawing.Color.White
        Me.btnDodajPlik.Location = New System.Drawing.Point(286, 1)
        Me.btnDodajPlik.Name = "btnDodajPlik"
        Me.btnDodajPlik.Size = New System.Drawing.Size(75, 23)
        Me.btnDodajPlik.TabIndex = 2
        Me.btnDodajPlik.Text = "Dodaj plik"
        Me.btnDodajPlik.UseVisualStyleBackColor = False
        '
        'btnZamknij
        '
        Me.btnZamknij.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnZamknij.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnZamknij.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnZamknij.ForeColor = System.Drawing.Color.White
        Me.btnZamknij.Location = New System.Drawing.Point(286, 291)
        Me.btnZamknij.Name = "btnZamknij"
        Me.btnZamknij.Size = New System.Drawing.Size(75, 23)
        Me.btnZamknij.TabIndex = 4
        Me.btnZamknij.Text = "Zamknij"
        Me.btnZamknij.UseVisualStyleBackColor = False
        '
        'btnUsunPliki
        '
        Me.btnUsunPliki.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnUsunPliki.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnUsunPliki.ForeColor = System.Drawing.Color.White
        Me.btnUsunPliki.Location = New System.Drawing.Point(205, 1)
        Me.btnUsunPliki.Name = "btnUsunPliki"
        Me.btnUsunPliki.Size = New System.Drawing.Size(75, 23)
        Me.btnUsunPliki.TabIndex = 3
        Me.btnUsunPliki.Text = "Usuń pliki"
        Me.btnUsunPliki.UseVisualStyleBackColor = False
        '
        'frmPlikiDoPobrania
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnZamknij
        Me.ClientSize = New System.Drawing.Size(365, 315)
        Me.Controls.Add(Me.btnUsunPliki)
        Me.Controls.Add(Me.btnZamknij)
        Me.Controls.Add(Me.btnDodajPlik)
        Me.Controls.Add(Me.dgvPliki)
        Me.Controls.Add(Me.lblListaPlikow)
        Me.Name = "frmPlikiDoPobrania"
        Me.Text = "Pliki do pobrania"
        CType(Me.dgvPliki, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblListaPlikow As System.Windows.Forms.Label
    Friend WithEvents dgvPliki As System.Windows.Forms.DataGridView
    Friend WithEvents btnDodajPlik As System.Windows.Forms.Button
    Friend WithEvents btnZamknij As System.Windows.Forms.Button
    Friend WithEvents btnUsunPliki As System.Windows.Forms.Button
End Class
