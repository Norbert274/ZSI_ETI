<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmZwrotyBezZamowien
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
        Me.btnZamknij = New System.Windows.Forms.Button()
        Me.lblDostawaEtykieta = New System.Windows.Forms.Label()
        Me.cmbDostawa = New System.Windows.Forms.ComboBox()
        Me.btnPrzypisz = New System.Windows.Forms.Button()
        Me.lblZamowilEtykieta = New System.Windows.Forms.Label()
        Me.lblDataRealizacjiEtykieta = New System.Windows.Forms.Label()
        Me.lblTypZamowieniaEtykieta = New System.Windows.Forms.Label()
        Me.lblZamowil = New System.Windows.Forms.Label()
        Me.lblDataRealizacji = New System.Windows.Forms.Label()
        Me.lblTypZamowienia = New System.Windows.Forms.Label()
        Me.cmbZamowienie = New System.Windows.Forms.ComboBox()
        Me.lblZamowienieEtykieta = New System.Windows.Forms.Label()
        Me.dgv = New System.Windows.Forms.DataGridView()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnZamknij
        '
        Me.btnZamknij.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnZamknij.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnZamknij.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnZamknij.ForeColor = System.Drawing.Color.White
        Me.btnZamknij.Location = New System.Drawing.Point(594, 518)
        Me.btnZamknij.Name = "btnZamknij"
        Me.btnZamknij.Size = New System.Drawing.Size(75, 23)
        Me.btnZamknij.TabIndex = 1
        Me.btnZamknij.Text = "Zamknij"
        Me.btnZamknij.UseVisualStyleBackColor = False
        '
        'lblDostawaEtykieta
        '
        Me.lblDostawaEtykieta.AutoSize = True
        Me.lblDostawaEtykieta.ForeColor = System.Drawing.Color.Black
        Me.lblDostawaEtykieta.Location = New System.Drawing.Point(47, 9)
        Me.lblDostawaEtykieta.Name = "lblDostawaEtykieta"
        Me.lblDostawaEtykieta.Size = New System.Drawing.Size(52, 13)
        Me.lblDostawaEtykieta.TabIndex = 2
        Me.lblDostawaEtykieta.Text = "Dostawa:"
        '
        'cmbDostawa
        '
        Me.cmbDostawa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDostawa.FormattingEnabled = True
        Me.cmbDostawa.Location = New System.Drawing.Point(105, 6)
        Me.cmbDostawa.Name = "cmbDostawa"
        Me.cmbDostawa.Size = New System.Drawing.Size(217, 21)
        Me.cmbDostawa.TabIndex = 3
        '
        'btnPrzypisz
        '
        Me.btnPrzypisz.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPrzypisz.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnPrzypisz.Enabled = False
        Me.btnPrzypisz.ForeColor = System.Drawing.Color.White
        Me.btnPrzypisz.Location = New System.Drawing.Point(513, 518)
        Me.btnPrzypisz.Name = "btnPrzypisz"
        Me.btnPrzypisz.Size = New System.Drawing.Size(75, 23)
        Me.btnPrzypisz.TabIndex = 4
        Me.btnPrzypisz.Text = "Przypisz"
        Me.btnPrzypisz.UseVisualStyleBackColor = False
        '
        'lblZamowilEtykieta
        '
        Me.lblZamowilEtykieta.AutoSize = True
        Me.lblZamowilEtykieta.ForeColor = System.Drawing.Color.Black
        Me.lblZamowilEtykieta.Location = New System.Drawing.Point(47, 63)
        Me.lblZamowilEtykieta.Name = "lblZamowilEtykieta"
        Me.lblZamowilEtykieta.Size = New System.Drawing.Size(51, 13)
        Me.lblZamowilEtykieta.TabIndex = 5
        Me.lblZamowilEtykieta.Text = "Zamówił:"
        '
        'lblDataRealizacjiEtykieta
        '
        Me.lblDataRealizacjiEtykieta.AutoSize = True
        Me.lblDataRealizacjiEtykieta.ForeColor = System.Drawing.Color.Black
        Me.lblDataRealizacjiEtykieta.Location = New System.Drawing.Point(23, 85)
        Me.lblDataRealizacjiEtykieta.Name = "lblDataRealizacjiEtykieta"
        Me.lblDataRealizacjiEtykieta.Size = New System.Drawing.Size(76, 13)
        Me.lblDataRealizacjiEtykieta.TabIndex = 6
        Me.lblDataRealizacjiEtykieta.Text = "Data realizacji:"
        '
        'lblTypZamowieniaEtykieta
        '
        Me.lblTypZamowieniaEtykieta.AutoSize = True
        Me.lblTypZamowieniaEtykieta.ForeColor = System.Drawing.Color.Black
        Me.lblTypZamowieniaEtykieta.Location = New System.Drawing.Point(13, 108)
        Me.lblTypZamowieniaEtykieta.Name = "lblTypZamowieniaEtykieta"
        Me.lblTypZamowieniaEtykieta.Size = New System.Drawing.Size(86, 13)
        Me.lblTypZamowieniaEtykieta.TabIndex = 7
        Me.lblTypZamowieniaEtykieta.Text = "Typ zamówienia:"
        '
        'lblZamowil
        '
        Me.lblZamowil.AutoSize = True
        Me.lblZamowil.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblZamowil.Location = New System.Drawing.Point(104, 63)
        Me.lblZamowil.Name = "lblZamowil"
        Me.lblZamowil.Size = New System.Drawing.Size(19, 13)
        Me.lblZamowil.TabIndex = 8
        Me.lblZamowil.Text = "---"
        '
        'lblDataRealizacji
        '
        Me.lblDataRealizacji.AutoSize = True
        Me.lblDataRealizacji.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblDataRealizacji.Location = New System.Drawing.Point(105, 85)
        Me.lblDataRealizacji.Name = "lblDataRealizacji"
        Me.lblDataRealizacji.Size = New System.Drawing.Size(19, 13)
        Me.lblDataRealizacji.TabIndex = 9
        Me.lblDataRealizacji.Text = "---"
        '
        'lblTypZamowienia
        '
        Me.lblTypZamowienia.AutoSize = True
        Me.lblTypZamowienia.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblTypZamowienia.Location = New System.Drawing.Point(105, 108)
        Me.lblTypZamowienia.Name = "lblTypZamowienia"
        Me.lblTypZamowienia.Size = New System.Drawing.Size(19, 13)
        Me.lblTypZamowienia.TabIndex = 10
        Me.lblTypZamowienia.Text = "---"
        '
        'cmbZamowienie
        '
        Me.cmbZamowienie.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbZamowienie.FormattingEnabled = True
        Me.cmbZamowienie.Location = New System.Drawing.Point(105, 33)
        Me.cmbZamowienie.Name = "cmbZamowienie"
        Me.cmbZamowienie.Size = New System.Drawing.Size(217, 21)
        Me.cmbZamowienie.TabIndex = 12
        '
        'lblZamowienieEtykieta
        '
        Me.lblZamowienieEtykieta.AutoSize = True
        Me.lblZamowienieEtykieta.ForeColor = System.Drawing.Color.Black
        Me.lblZamowienieEtykieta.Location = New System.Drawing.Point(32, 36)
        Me.lblZamowienieEtykieta.Name = "lblZamowienieEtykieta"
        Me.lblZamowienieEtykieta.Size = New System.Drawing.Size(67, 13)
        Me.lblZamowienieEtykieta.TabIndex = 11
        Me.lblZamowienieEtykieta.Text = "Zamówienie:"
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
        Me.dgv.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgv.Location = New System.Drawing.Point(12, 130)
        Me.dgv.Name = "dgv"
        Me.dgv.Size = New System.Drawing.Size(657, 384)
        Me.dgv.TabIndex = 13
        '
        'frmZwrotyBezZamowien
        '
        Me.AcceptButton = Me.btnPrzypisz
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnZamknij
        Me.ClientSize = New System.Drawing.Size(681, 546)
        Me.Controls.Add(Me.dgv)
        Me.Controls.Add(Me.cmbZamowienie)
        Me.Controls.Add(Me.lblZamowienieEtykieta)
        Me.Controls.Add(Me.lblTypZamowienia)
        Me.Controls.Add(Me.lblDataRealizacji)
        Me.Controls.Add(Me.lblZamowil)
        Me.Controls.Add(Me.lblTypZamowieniaEtykieta)
        Me.Controls.Add(Me.lblDataRealizacjiEtykieta)
        Me.Controls.Add(Me.lblZamowilEtykieta)
        Me.Controls.Add(Me.btnPrzypisz)
        Me.Controls.Add(Me.cmbDostawa)
        Me.Controls.Add(Me.lblDostawaEtykieta)
        Me.Controls.Add(Me.btnZamknij)
        Me.Name = "frmZwrotyBezZamowien"
        Me.Text = "Zwroty bez zamówień"
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnZamknij As System.Windows.Forms.Button
    Friend WithEvents lblDostawaEtykieta As System.Windows.Forms.Label
    Friend WithEvents cmbDostawa As System.Windows.Forms.ComboBox
    Friend WithEvents btnPrzypisz As System.Windows.Forms.Button
    Friend WithEvents lblZamowilEtykieta As System.Windows.Forms.Label
    Friend WithEvents lblDataRealizacjiEtykieta As System.Windows.Forms.Label
    Friend WithEvents lblTypZamowieniaEtykieta As System.Windows.Forms.Label
    Friend WithEvents lblZamowil As System.Windows.Forms.Label
    Friend WithEvents lblDataRealizacji As System.Windows.Forms.Label
    Friend WithEvents lblTypZamowienia As System.Windows.Forms.Label
    Friend WithEvents cmbZamowienie As System.Windows.Forms.ComboBox
    Friend WithEvents lblZamowienieEtykieta As System.Windows.Forms.Label
    Friend WithEvents dgv As System.Windows.Forms.DataGridView
End Class
