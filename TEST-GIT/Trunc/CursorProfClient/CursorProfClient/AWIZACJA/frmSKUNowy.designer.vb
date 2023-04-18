<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSKUNowy
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblKlasa = New System.Windows.Forms.Label()
        Me.txtNrSKU = New System.Windows.Forms.TextBox()
        Me.txtNazwaSku = New System.Windows.Forms.TextBox()
        Me.cmbKlasa = New System.Windows.Forms.ComboBox()
        Me.btnDodaj = New System.Windows.Forms.Button()
        Me.btnAnuluj = New System.Windows.Forms.Button()
        Me.cmbKategoria = New System.Windows.Forms.ComboBox()
        Me.lblKategoria = New System.Windows.Forms.Label()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmbGrupaArtykulow = New System.Windows.Forms.ComboBox()
        Me.lblGrArt = New System.Windows.Forms.Label()
        Me.cmbBrand = New System.Windows.Forms.ComboBox()
        Me.cmbJM = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtCena = New System.Windows.Forms.MaskedTextBox()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(29, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Sku:"
        Me.ToolTip.SetToolTip(Me.Label1, "Numer produktu.")
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(7, 34)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(43, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Nazwa:"
        Me.ToolTip.SetToolTip(Me.Label2, "Nazwa produktu.")
        '
        'lblKlasa
        '
        Me.lblKlasa.AutoSize = True
        Me.lblKlasa.Location = New System.Drawing.Point(7, 189)
        Me.lblKlasa.Name = "lblKlasa"
        Me.lblKlasa.Size = New System.Drawing.Size(36, 13)
        Me.lblKlasa.TabIndex = 12
        Me.lblKlasa.Text = "Klasa:"
        Me.ToolTip.SetToolTip(Me.lblKlasa, "Klasa produktu")
        '
        'txtNrSKU
        '
        Me.txtNrSKU.Location = New System.Drawing.Point(102, 8)
        Me.txtNrSKU.Name = "txtNrSKU"
        Me.txtNrSKU.Size = New System.Drawing.Size(120, 20)
        Me.txtNrSKU.TabIndex = 1
        Me.ToolTip.SetToolTip(Me.txtNrSKU, "Numer produktu.")
        '
        'txtNazwaSku
        '
        Me.txtNazwaSku.Location = New System.Drawing.Point(102, 34)
        Me.txtNazwaSku.Multiline = True
        Me.txtNazwaSku.Name = "txtNazwaSku"
        Me.txtNazwaSku.Size = New System.Drawing.Size(290, 38)
        Me.txtNazwaSku.TabIndex = 3
        Me.ToolTip.SetToolTip(Me.txtNazwaSku, "Nazwa produktu.")
        '
        'cmbKlasa
        '
        Me.cmbKlasa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbKlasa.FormattingEnabled = True
        Me.cmbKlasa.Items.AddRange(New Object() {"A", "B", "C"})
        Me.cmbKlasa.Location = New System.Drawing.Point(102, 186)
        Me.cmbKlasa.Name = "cmbKlasa"
        Me.cmbKlasa.Size = New System.Drawing.Size(59, 21)
        Me.cmbKlasa.TabIndex = 13
        Me.ToolTip.SetToolTip(Me.cmbKlasa, "Klasa produktu")
        '
        'btnDodaj
        '
        Me.btnDodaj.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDodaj.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnDodaj.ForeColor = System.Drawing.Color.White
        Me.btnDodaj.Location = New System.Drawing.Point(227, 239)
        Me.btnDodaj.Name = "btnDodaj"
        Me.btnDodaj.Size = New System.Drawing.Size(86, 25)
        Me.btnDodaj.TabIndex = 16
        Me.btnDodaj.Text = "Dodaj"
        Me.ToolTip.SetToolTip(Me.btnDodaj, "Dodanie produktu do systemu")
        Me.btnDodaj.UseVisualStyleBackColor = False
        '
        'btnAnuluj
        '
        Me.btnAnuluj.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAnuluj.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnAnuluj.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnAnuluj.ForeColor = System.Drawing.Color.White
        Me.btnAnuluj.Location = New System.Drawing.Point(319, 239)
        Me.btnAnuluj.Name = "btnAnuluj"
        Me.btnAnuluj.Size = New System.Drawing.Size(75, 25)
        Me.btnAnuluj.TabIndex = 17
        Me.btnAnuluj.Text = "Anuluj"
        Me.btnAnuluj.UseVisualStyleBackColor = False
        '
        'cmbKategoria
        '
        Me.cmbKategoria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbKategoria.FormattingEnabled = True
        Me.cmbKategoria.Location = New System.Drawing.Point(102, 78)
        Me.cmbKategoria.Name = "cmbKategoria"
        Me.cmbKategoria.Size = New System.Drawing.Size(290, 21)
        Me.cmbKategoria.TabIndex = 5
        Me.ToolTip.SetToolTip(Me.cmbKategoria, "Kategoria produktu.")
        '
        'lblKategoria
        '
        Me.lblKategoria.AutoSize = True
        Me.lblKategoria.Location = New System.Drawing.Point(7, 81)
        Me.lblKategoria.Name = "lblKategoria"
        Me.lblKategoria.Size = New System.Drawing.Size(55, 13)
        Me.lblKategoria.TabIndex = 4
        Me.lblKategoria.Text = "Kategoria:"
        Me.ToolTip.SetToolTip(Me.lblKategoria, "Kategoria produktu.")
        '
        'cmbGrupaArtykulow
        '
        Me.cmbGrupaArtykulow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbGrupaArtykulow.FormattingEnabled = True
        Me.cmbGrupaArtykulow.Location = New System.Drawing.Point(102, 159)
        Me.cmbGrupaArtykulow.Name = "cmbGrupaArtykulow"
        Me.cmbGrupaArtykulow.Size = New System.Drawing.Size(290, 21)
        Me.cmbGrupaArtykulow.TabIndex = 11
        Me.ToolTip.SetToolTip(Me.cmbGrupaArtykulow, "Grupa artykułów w systemie magazynowym")
        '
        'lblGrArt
        '
        Me.lblGrArt.AutoSize = True
        Me.lblGrArt.Location = New System.Drawing.Point(7, 162)
        Me.lblGrArt.Name = "lblGrArt"
        Me.lblGrArt.Size = New System.Drawing.Size(89, 13)
        Me.lblGrArt.TabIndex = 10
        Me.lblGrArt.Text = "Grupa artykułów:"
        Me.ToolTip.SetToolTip(Me.lblGrArt, "Grupa artykułów w systemie magazynowym")
        '
        'cmbBrand
        '
        Me.cmbBrand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBrand.FormattingEnabled = True
        Me.cmbBrand.Location = New System.Drawing.Point(102, 105)
        Me.cmbBrand.Name = "cmbBrand"
        Me.cmbBrand.Size = New System.Drawing.Size(290, 21)
        Me.cmbBrand.TabIndex = 7
        Me.ToolTip.SetToolTip(Me.cmbBrand, "Grupa artykułów w systemie magazynowym")
        '
        'cmbJM
        '
        Me.cmbJM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbJM.FormattingEnabled = True
        Me.cmbJM.Location = New System.Drawing.Point(102, 132)
        Me.cmbJM.Name = "cmbJM"
        Me.cmbJM.Size = New System.Drawing.Size(290, 21)
        Me.cmbJM.TabIndex = 9
        Me.ToolTip.SetToolTip(Me.cmbJM, "Grupa artykułów w systemie magazynowym")
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(7, 108)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(38, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Brand:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(7, 135)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(24, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "J.M"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(7, 216)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(35, 13)
        Me.Label5.TabIndex = 14
        Me.Label5.Text = "Cena:"
        '
        'txtCena
        '
        Me.txtCena.Location = New System.Drawing.Point(102, 213)
        Me.txtCena.Name = "txtCena"
        Me.txtCena.Size = New System.Drawing.Size(59, 20)
        Me.txtCena.TabIndex = 15
        '
        'frmSKUNowy
        '
        Me.AcceptButton = Me.btnDodaj
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnAnuluj
        Me.ClientSize = New System.Drawing.Size(400, 276)
        Me.Controls.Add(Me.txtCena)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cmbJM)
        Me.Controls.Add(Me.cmbBrand)
        Me.Controls.Add(Me.cmbGrupaArtykulow)
        Me.Controls.Add(Me.lblGrArt)
        Me.Controls.Add(Me.cmbKategoria)
        Me.Controls.Add(Me.lblKategoria)
        Me.Controls.Add(Me.btnAnuluj)
        Me.Controls.Add(Me.btnDodaj)
        Me.Controls.Add(Me.cmbKlasa)
        Me.Controls.Add(Me.txtNazwaSku)
        Me.Controls.Add(Me.txtNrSKU)
        Me.Controls.Add(Me.lblKlasa)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.MaximizeBox = False
        Me.MinimumSize = New System.Drawing.Size(416, 314)
        Me.Name = "frmSKUNowy"
        Me.Text = "Nowy produkt"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblKlasa As System.Windows.Forms.Label
    Friend WithEvents txtNrSKU As System.Windows.Forms.TextBox
    Friend WithEvents txtNazwaSku As System.Windows.Forms.TextBox
    Friend WithEvents cmbKlasa As System.Windows.Forms.ComboBox
    Friend WithEvents btnDodaj As System.Windows.Forms.Button
    Friend WithEvents btnAnuluj As System.Windows.Forms.Button
    Friend WithEvents cmbKategoria As System.Windows.Forms.ComboBox
    Friend WithEvents lblKategoria As System.Windows.Forms.Label
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents cmbGrupaArtykulow As System.Windows.Forms.ComboBox
    Friend WithEvents lblGrArt As System.Windows.Forms.Label
    Friend WithEvents cmbBrand As System.Windows.Forms.ComboBox
    Friend WithEvents cmbJM As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtCena As System.Windows.Forms.MaskedTextBox
End Class
