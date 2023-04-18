<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmZamowieniaZExcela
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnSzablon = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbNazwaArkusza = New System.Windows.Forms.ComboBox()
        Me.btnWybierzPlik = New System.Windows.Forms.Button()
        Me.txtPlik = New System.Windows.Forms.TextBox()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.lblIloscZamowien = New System.Windows.Forms.Label()
        Me.btnZlozZamowienia = New System.Windows.Forms.Button()
        Me.btnAnuluj = New System.Windows.Forms.Button()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.dgv = New System.Windows.Forms.DataGridView()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.dtpDataRealizacji = New System.Windows.Forms.DateTimePicker()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.cmbGrupy = New System.Windows.Forms.ComboBox()
        Me.lblPrzetwarzanieDanych = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnSzablon)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.cmbNazwaArkusza)
        Me.GroupBox1.Controls.Add(Me.btnWybierzPlik)
        Me.GroupBox1.Controls.Add(Me.txtPlik)
        Me.GroupBox1.Location = New System.Drawing.Point(4, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(527, 67)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Wybierz plik :"
        '
        'btnSzablon
        '
        Me.btnSzablon.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnSzablon.ForeColor = System.Drawing.Color.White
        Me.btnSzablon.Location = New System.Drawing.Point(446, 40)
        Me.btnSzablon.Name = "btnSzablon"
        Me.btnSzablon.Size = New System.Drawing.Size(68, 21)
        Me.btnSzablon.TabIndex = 10
        Me.btnSzablon.Text = "Szablon"
        Me.btnSzablon.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(81, 43)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(83, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Nazwa arkusza:"
        '
        'cmbNazwaArkusza
        '
        Me.cmbNazwaArkusza.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbNazwaArkusza.FormattingEnabled = True
        Me.cmbNazwaArkusza.Location = New System.Drawing.Point(170, 40)
        Me.cmbNazwaArkusza.Name = "cmbNazwaArkusza"
        Me.cmbNazwaArkusza.Size = New System.Drawing.Size(270, 21)
        Me.cmbNazwaArkusza.TabIndex = 3
        '
        'btnWybierzPlik
        '
        Me.btnWybierzPlik.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnWybierzPlik.ForeColor = System.Drawing.Color.White
        Me.btnWybierzPlik.Location = New System.Drawing.Point(446, 13)
        Me.btnWybierzPlik.Name = "btnWybierzPlik"
        Me.btnWybierzPlik.Size = New System.Drawing.Size(68, 22)
        Me.btnWybierzPlik.TabIndex = 1
        Me.btnWybierzPlik.Text = "Wybierz plik"
        Me.btnWybierzPlik.UseVisualStyleBackColor = False
        '
        'txtPlik
        '
        Me.txtPlik.Enabled = False
        Me.txtPlik.Location = New System.Drawing.Point(7, 15)
        Me.txtPlik.Name = "txtPlik"
        Me.txtPlik.Size = New System.Drawing.Size(433, 20)
        Me.txtPlik.TabIndex = 0
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProgressBar1.Location = New System.Drawing.Point(4, 432)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(694, 23)
        Me.ProgressBar1.TabIndex = 7
        '
        'lblIloscZamowien
        '
        Me.lblIloscZamowien.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblIloscZamowien.AutoSize = True
        Me.lblIloscZamowien.BackColor = System.Drawing.Color.Transparent
        Me.lblIloscZamowien.Location = New System.Drawing.Point(8, 416)
        Me.lblIloscZamowien.Name = "lblIloscZamowien"
        Me.lblIloscZamowien.Size = New System.Drawing.Size(91, 13)
        Me.lblIloscZamowien.TabIndex = 6
        Me.lblIloscZamowien.Text = "Ilość zamówień: 0"
        '
        'btnZlozZamowienia
        '
        Me.btnZlozZamowienia.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnZlozZamowienia.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnZlozZamowienia.Enabled = False
        Me.btnZlozZamowienia.ForeColor = System.Drawing.Color.White
        Me.btnZlozZamowienia.Location = New System.Drawing.Point(520, 461)
        Me.btnZlozZamowienia.Name = "btnZlozZamowienia"
        Me.btnZlozZamowienia.Size = New System.Drawing.Size(101, 23)
        Me.btnZlozZamowienia.TabIndex = 9
        Me.btnZlozZamowienia.Text = "Złóż zamówienia"
        Me.btnZlozZamowienia.UseVisualStyleBackColor = False
        '
        'btnAnuluj
        '
        Me.btnAnuluj.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAnuluj.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnAnuluj.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnAnuluj.ForeColor = System.Drawing.Color.White
        Me.btnAnuluj.Location = New System.Drawing.Point(627, 461)
        Me.btnAnuluj.Name = "btnAnuluj"
        Me.btnAnuluj.Size = New System.Drawing.Size(71, 23)
        Me.btnAnuluj.TabIndex = 10
        Me.btnAnuluj.Text = "Anuluj"
        Me.btnAnuluj.UseVisualStyleBackColor = False
        '
        'lblProgress
        '
        Me.lblProgress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblProgress.AutoSize = True
        Me.lblProgress.BackColor = System.Drawing.Color.Transparent
        Me.lblProgress.Location = New System.Drawing.Point(12, 461)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(10, 13)
        Me.lblProgress.TabIndex = 8
        Me.lblProgress.Text = " "
        '
        'dgv
        '
        Me.dgv.AllowUserToAddRows = False
        Me.dgv.AllowUserToDeleteRows = False
        Me.dgv.AllowUserToResizeRows = False
        Me.dgv.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgv.BackgroundColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgv.Location = New System.Drawing.Point(4, 76)
        Me.dgv.Name = "dgv"
        Me.dgv.ReadOnly = True
        Me.dgv.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dgv.RowHeadersVisible = False
        Me.dgv.Size = New System.Drawing.Size(694, 337)
        Me.dgv.TabIndex = 4
        '
        'GroupBox3
        '
        Me.GroupBox3.Location = New System.Drawing.Point(537, 3)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(163, 67)
        Me.GroupBox3.TabIndex = 1
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Przewidywana data dostawy:"
        '
        'dtpDataRealizacji
        '
        Me.dtpDataRealizacji.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDataRealizacji.Location = New System.Drawing.Point(565, 28)
        Me.dtpDataRealizacji.Name = "dtpDataRealizacji"
        Me.dtpDataRealizacji.Size = New System.Drawing.Size(112, 20)
        Me.dtpDataRealizacji.TabIndex = 2
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.cmbGrupy)
        Me.GroupBox4.Location = New System.Drawing.Point(4, 75)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(391, 41)
        Me.GroupBox4.TabIndex = 3
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Wybierz grupę, z której będzie zamawiany towar:"
        Me.GroupBox4.Visible = False
        '
        'cmbGrupy
        '
        Me.cmbGrupy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbGrupy.FormattingEnabled = True
        Me.cmbGrupy.Location = New System.Drawing.Point(19, 14)
        Me.cmbGrupy.Name = "cmbGrupy"
        Me.cmbGrupy.Size = New System.Drawing.Size(352, 21)
        Me.cmbGrupy.TabIndex = 0
        '
        'lblPrzetwarzanieDanych
        '
        Me.lblPrzetwarzanieDanych.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblPrzetwarzanieDanych.AutoSize = True
        Me.lblPrzetwarzanieDanych.BackColor = System.Drawing.SystemColors.Control
        Me.lblPrzetwarzanieDanych.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblPrzetwarzanieDanych.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblPrzetwarzanieDanych.Location = New System.Drawing.Point(127, 225)
        Me.lblPrzetwarzanieDanych.Name = "lblPrzetwarzanieDanych"
        Me.lblPrzetwarzanieDanych.Size = New System.Drawing.Size(370, 13)
        Me.lblPrzetwarzanieDanych.TabIndex = 5
        Me.lblPrzetwarzanieDanych.Text = "Trwa sprawdzanie poprawności danych w załadowanym pliku ..."
        Me.lblPrzetwarzanieDanych.Visible = False
        '
        'frmZamowieniaZExcela
        '
        Me.AcceptButton = Me.btnZlozZamowienia
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnAnuluj
        Me.ClientSize = New System.Drawing.Size(704, 496)
        Me.Controls.Add(Me.dtpDataRealizacji)
        Me.Controls.Add(Me.lblPrzetwarzanieDanych)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.dgv)
        Me.Controls.Add(Me.lblProgress)
        Me.Controls.Add(Me.btnAnuluj)
        Me.Controls.Add(Me.btnZlozZamowienia)
        Me.Controls.Add(Me.lblIloscZamowien)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox4)
        Me.MinimumSize = New System.Drawing.Size(720, 534)
        Me.Name = "frmZamowieniaZExcela"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Składanie zamówień z pliku Excel"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox4.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnWybierzPlik As System.Windows.Forms.Button
    Friend WithEvents txtPlik As System.Windows.Forms.TextBox
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents lblIloscZamowien As System.Windows.Forms.Label
    Friend WithEvents btnZlozZamowienia As System.Windows.Forms.Button
    Friend WithEvents btnAnuluj As System.Windows.Forms.Button
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents dgv As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbNazwaArkusza As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents dtpDataRealizacji As System.Windows.Forms.DateTimePicker
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbGrupy As System.Windows.Forms.ComboBox
    Friend WithEvents lblPrzetwarzanieDanych As System.Windows.Forms.Label
    Friend WithEvents btnSzablon As System.Windows.Forms.Button
End Class
