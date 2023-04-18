<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNewsleterLista
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmNewsleterLista))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.ts = New System.Windows.Forms.ToolStrip()
        Me.btnNowyNewsletter = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnOdswierz = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.dgvNewsleter = New System.Windows.Forms.DataGridView()
        Me.btnZamknij = New System.Windows.Forms.Button()
        Me.ts.SuspendLayout()
        CType(Me.dgvNewsleter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ts
        '
        Me.ts.BackColor = System.Drawing.Color.White
        Me.ts.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNowyNewsletter, Me.ToolStripSeparator1, Me.btnOdswierz, Me.ToolStripSeparator2})
        Me.ts.Location = New System.Drawing.Point(0, 0)
        Me.ts.Name = "ts"
        Me.ts.Size = New System.Drawing.Size(503, 25)
        Me.ts.TabIndex = 0
        Me.ts.Text = "ToolStrip1"
        '
        'btnNowyNewsletter
        '
        Me.btnNowyNewsletter.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.btnNowyNewsletter.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnNowyNewsletter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnNowyNewsletter.ForeColor = System.Drawing.Color.White
        Me.btnNowyNewsletter.Image = CType(resources.GetObject("btnNowyNewsletter.Image"), System.Drawing.Image)
        Me.btnNowyNewsletter.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnNowyNewsletter.Name = "btnNowyNewsletter"
        Me.btnNowyNewsletter.Size = New System.Drawing.Size(106, 22)
        Me.btnNowyNewsletter.Text = "Nowa wiadomość"
        Me.btnNowyNewsletter.ToolTipText = "Utworzenie nowej wiadomości dla użytkowników"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'btnOdswierz
        '
        Me.btnOdswierz.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.btnOdswierz.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnOdswierz.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnOdswierz.ForeColor = System.Drawing.Color.White
        Me.btnOdswierz.Image = CType(resources.GetObject("btnOdswierz.Image"), System.Drawing.Image)
        Me.btnOdswierz.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnOdswierz.Name = "btnOdswierz"
        Me.btnOdswierz.Size = New System.Drawing.Size(55, 22)
        Me.btnOdswierz.Text = "Odśwież"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'dgvNewsleter
        '
        Me.dgvNewsleter.AllowUserToAddRows = False
        Me.dgvNewsleter.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue
        Me.dgvNewsleter.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvNewsleter.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvNewsleter.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvNewsleter.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvNewsleter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvNewsleter.DefaultCellStyle = DataGridViewCellStyle3
        Me.dgvNewsleter.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvNewsleter.Location = New System.Drawing.Point(0, 28)
        Me.dgvNewsleter.Name = "dgvNewsleter"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvNewsleter.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.dgvNewsleter.RowHeadersVisible = False
        Me.dgvNewsleter.Size = New System.Drawing.Size(503, 370)
        Me.dgvNewsleter.TabIndex = 1
        '
        'btnZamknij
        '
        Me.btnZamknij.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnZamknij.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnZamknij.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnZamknij.ForeColor = System.Drawing.Color.White
        Me.btnZamknij.Location = New System.Drawing.Point(427, 399)
        Me.btnZamknij.Name = "btnZamknij"
        Me.btnZamknij.Size = New System.Drawing.Size(75, 23)
        Me.btnZamknij.TabIndex = 2
        Me.btnZamknij.Text = "Zamknij"
        Me.btnZamknij.UseVisualStyleBackColor = False
        '
        'frmNewsleterLista
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnZamknij
        Me.ClientSize = New System.Drawing.Size(503, 423)
        Me.Controls.Add(Me.btnZamknij)
        Me.Controls.Add(Me.dgvNewsleter)
        Me.Controls.Add(Me.ts)
        Me.MinimumSize = New System.Drawing.Size(331, 239)
        Me.Name = "frmNewsleterLista"
        Me.Text = "Newsletter"
        Me.ts.ResumeLayout(False)
        Me.ts.PerformLayout()
        CType(Me.dgvNewsleter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ts As System.Windows.Forms.ToolStrip
    Friend WithEvents btnNowyNewsletter As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnOdswierz As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents dgvNewsleter As System.Windows.Forms.DataGridView
    Friend WithEvents btnZamknij As System.Windows.Forms.Button
End Class
