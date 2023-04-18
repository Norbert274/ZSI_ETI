<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUzytkownik
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
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.btnOk = New System.Windows.Forms.Button()
        Me.btnAnuluj = New System.Windows.Forms.Button()
        Me.btnZastosuj = New System.Windows.Forms.Button()
        Me.tc = New System.Windows.Forms.TabControl()
        Me.tpPodstawowe = New System.Windows.Forms.TabPage()
        Me.gbLimitZamowienPerUzytkownik = New System.Windows.Forms.GroupBox()
        Me.lblIloscZamowien = New System.Windows.Forms.Label()
        Me.lblOkres = New System.Windows.Forms.Label()
        Me.chkCzyLimitWydanOsoba = New System.Windows.Forms.CheckBox()
        Me.cmbTypOkresZamowien = New System.Windows.Forms.ComboBox()
        Me.txtMaxIloscZamowien = New System.Windows.Forms.TextBox()
        Me.btnNotyfikacja = New System.Windows.Forms.Button()
        Me.gbAdresy = New System.Windows.Forms.GroupBox()
        Me.lblAdresyOpis = New System.Windows.Forms.Label()
        Me.btnAdresy = New System.Windows.Forms.Button()
        Me.lblAdresy = New System.Windows.Forms.Label()
        Me.btnZmienHaslo = New System.Windows.Forms.Button()
        Me.btnUsunHaslo = New System.Windows.Forms.Button()
        Me.gbDanePodstawowe = New System.Windows.Forms.GroupBox()
        Me.cmbSiecSprzedazy = New System.Windows.Forms.ComboBox()
        Me.lblSiecSprzedazy = New System.Windows.Forms.Label()
        Me.cmbZespolSprzedazy = New System.Windows.Forms.ComboBox()
        Me.lblZespolSprzedazy = New System.Windows.Forms.Label()
        Me.cmbRegionSprzedazy = New System.Windows.Forms.ComboBox()
        Me.lblRegionSprzedazy = New System.Windows.Forms.Label()
        Me.cmbObszarSprzedazy = New System.Windows.Forms.ComboBox()
        Me.lblObszarSprzedazy = New System.Windows.Forms.Label()
        Me.cmbTyp = New System.Windows.Forms.ComboBox()
        Me.lblTyp = New System.Windows.Forms.Label()
        Me.cmbWielkosc = New System.Windows.Forms.ComboBox()
        Me.lblWielkosc = New System.Windows.Forms.Label()
        Me.txtTelkom = New System.Windows.Forms.MaskedTextBox()
        Me.lblEmail = New System.Windows.Forms.Label()
        Me.lblTelkom = New System.Windows.Forms.Label()
        Me.lblNazwa = New System.Windows.Forms.Label()
        Me.lblNazwisko = New System.Windows.Forms.Label()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.txtNazwa = New System.Windows.Forms.TextBox()
        Me.txtNazwisko = New System.Windows.Forms.TextBox()
        Me.txtImie = New System.Windows.Forms.TextBox()
        Me.lblImie = New System.Windows.Forms.Label()
        Me.gbDaneLogowania = New System.Windows.Forms.GroupBox()
        Me.lblHasloStatus = New System.Windows.Forms.Label()
        Me.lblHaslo2 = New System.Windows.Forms.Label()
        Me.txtHaslo2 = New System.Windows.Forms.TextBox()
        Me.lblHaslo = New System.Windows.Forms.Label()
        Me.txtHaslo = New System.Windows.Forms.TextBox()
        Me.txtLogin = New System.Windows.Forms.TextBox()
        Me.lblLogin = New System.Windows.Forms.Label()
        Me.gbDaneDpd = New System.Windows.Forms.GroupBox()
        Me.btnDaneDostawy = New System.Windows.Forms.Button()
        Me.tpGrupy = New System.Windows.Forms.TabPage()
        Me.btnEdytujGrupy = New System.Windows.Forms.Button()
        Me.dgvGrupy = New System.Windows.Forms.DataGridView()
        Me.tpFunkcje = New System.Windows.Forms.TabPage()
        Me.dgvFunkcje = New System.Windows.Forms.DataGridView()
        Me.btnEdytujFunkcje = New System.Windows.Forms.Button()
        Me.tc.SuspendLayout()
        Me.tpPodstawowe.SuspendLayout()
        Me.gbLimitZamowienPerUzytkownik.SuspendLayout()
        Me.gbAdresy.SuspendLayout()
        Me.gbDanePodstawowe.SuspendLayout()
        Me.gbDaneLogowania.SuspendLayout()
        Me.gbDaneDpd.SuspendLayout()
        Me.tpGrupy.SuspendLayout()
        CType(Me.dgvGrupy, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpFunkcje.SuspendLayout()
        CType(Me.dgvFunkcje, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnOk
        '
        Me.btnOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOk.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnOk.ForeColor = System.Drawing.Color.White
        Me.btnOk.Location = New System.Drawing.Point(633, 433)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(75, 23)
        Me.btnOk.TabIndex = 2
        Me.btnOk.Text = "OK"
        Me.btnOk.UseVisualStyleBackColor = False
        '
        'btnAnuluj
        '
        Me.btnAnuluj.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAnuluj.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnAnuluj.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnAnuluj.ForeColor = System.Drawing.Color.White
        Me.btnAnuluj.Location = New System.Drawing.Point(714, 433)
        Me.btnAnuluj.Name = "btnAnuluj"
        Me.btnAnuluj.Size = New System.Drawing.Size(75, 23)
        Me.btnAnuluj.TabIndex = 3
        Me.btnAnuluj.Text = "Anuluj"
        Me.btnAnuluj.UseVisualStyleBackColor = False
        '
        'btnZastosuj
        '
        Me.btnZastosuj.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnZastosuj.BackColor = System.Drawing.Color.LightGray
        Me.btnZastosuj.ForeColor = System.Drawing.Color.White
        Me.btnZastosuj.Location = New System.Drawing.Point(552, 433)
        Me.btnZastosuj.Name = "btnZastosuj"
        Me.btnZastosuj.Size = New System.Drawing.Size(75, 23)
        Me.btnZastosuj.TabIndex = 1
        Me.btnZastosuj.Text = "&Zastosuj"
        Me.btnZastosuj.UseVisualStyleBackColor = False
        '
        'tc
        '
        Me.tc.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tc.Controls.Add(Me.tpPodstawowe)
        Me.tc.Controls.Add(Me.tpGrupy)
        Me.tc.Controls.Add(Me.tpFunkcje)
        Me.tc.Location = New System.Drawing.Point(12, 12)
        Me.tc.Name = "tc"
        Me.tc.SelectedIndex = 0
        Me.tc.Size = New System.Drawing.Size(777, 415)
        Me.tc.TabIndex = 0
        '
        'tpPodstawowe
        '
        Me.tpPodstawowe.Controls.Add(Me.gbLimitZamowienPerUzytkownik)
        Me.tpPodstawowe.Controls.Add(Me.btnNotyfikacja)
        Me.tpPodstawowe.Controls.Add(Me.gbAdresy)
        Me.tpPodstawowe.Controls.Add(Me.btnZmienHaslo)
        Me.tpPodstawowe.Controls.Add(Me.btnUsunHaslo)
        Me.tpPodstawowe.Controls.Add(Me.gbDanePodstawowe)
        Me.tpPodstawowe.Controls.Add(Me.gbDaneLogowania)
        Me.tpPodstawowe.Controls.Add(Me.gbDaneDpd)
        Me.tpPodstawowe.Location = New System.Drawing.Point(4, 22)
        Me.tpPodstawowe.Name = "tpPodstawowe"
        Me.tpPodstawowe.Padding = New System.Windows.Forms.Padding(3)
        Me.tpPodstawowe.Size = New System.Drawing.Size(769, 389)
        Me.tpPodstawowe.TabIndex = 0
        Me.tpPodstawowe.Text = "Podstawowe"
        Me.tpPodstawowe.UseVisualStyleBackColor = True
        '
        'gbLimitZamowienPerUzytkownik
        '
        Me.gbLimitZamowienPerUzytkownik.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbLimitZamowienPerUzytkownik.Controls.Add(Me.lblIloscZamowien)
        Me.gbLimitZamowienPerUzytkownik.Controls.Add(Me.lblOkres)
        Me.gbLimitZamowienPerUzytkownik.Controls.Add(Me.chkCzyLimitWydanOsoba)
        Me.gbLimitZamowienPerUzytkownik.Controls.Add(Me.cmbTypOkresZamowien)
        Me.gbLimitZamowienPerUzytkownik.Controls.Add(Me.txtMaxIloscZamowien)
        Me.gbLimitZamowienPerUzytkownik.ForeColor = System.Drawing.Color.Black
        Me.gbLimitZamowienPerUzytkownik.Location = New System.Drawing.Point(457, 304)
        Me.gbLimitZamowienPerUzytkownik.Name = "gbLimitZamowienPerUzytkownik"
        Me.gbLimitZamowienPerUzytkownik.Size = New System.Drawing.Size(306, 79)
        Me.gbLimitZamowienPerUzytkownik.TabIndex = 7
        Me.gbLimitZamowienPerUzytkownik.TabStop = False
        Me.gbLimitZamowienPerUzytkownik.Text = "Limit zamówień dla użytkownika:"
        '
        'lblIloscZamowien
        '
        Me.lblIloscZamowien.AutoSize = True
        Me.lblIloscZamowien.ForeColor = System.Drawing.Color.Black
        Me.lblIloscZamowien.Location = New System.Drawing.Point(7, 42)
        Me.lblIloscZamowien.Name = "lblIloscZamowien"
        Me.lblIloscZamowien.Size = New System.Drawing.Size(84, 26)
        Me.lblIloscZamowien.TabIndex = 1
        Me.lblIloscZamowien.Text = "Maksymalna" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "ilość zamówień: "
        '
        'lblOkres
        '
        Me.lblOkres.AutoSize = True
        Me.lblOkres.ForeColor = System.Drawing.Color.Black
        Me.lblOkres.Location = New System.Drawing.Point(148, 49)
        Me.lblOkres.Name = "lblOkres"
        Me.lblOkres.Size = New System.Drawing.Size(15, 13)
        Me.lblOkres.TabIndex = 3
        Me.lblOkres.Text = "w"
        '
        'chkCzyLimitWydanOsoba
        '
        Me.chkCzyLimitWydanOsoba.AutoSize = True
        Me.chkCzyLimitWydanOsoba.ForeColor = System.Drawing.Color.Black
        Me.chkCzyLimitWydanOsoba.Location = New System.Drawing.Point(10, 19)
        Me.chkCzyLimitWydanOsoba.Name = "chkCzyLimitWydanOsoba"
        Me.chkCzyLimitWydanOsoba.Size = New System.Drawing.Size(198, 17)
        Me.chkCzyLimitWydanOsoba.TabIndex = 0
        Me.chkCzyLimitWydanOsoba.Text = "Czy limit zamówień dla użytkownika?"
        Me.chkCzyLimitWydanOsoba.UseVisualStyleBackColor = True
        '
        'cmbTypOkresZamowien
        '
        Me.cmbTypOkresZamowien.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTypOkresZamowien.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cmbTypOkresZamowien.ForeColor = System.Drawing.Color.Black
        Me.cmbTypOkresZamowien.FormattingEnabled = True
        Me.cmbTypOkresZamowien.Location = New System.Drawing.Point(169, 44)
        Me.cmbTypOkresZamowien.Name = "cmbTypOkresZamowien"
        Me.cmbTypOkresZamowien.Size = New System.Drawing.Size(131, 21)
        Me.cmbTypOkresZamowien.TabIndex = 4
        '
        'txtMaxIloscZamowien
        '
        Me.txtMaxIloscZamowien.ForeColor = System.Drawing.Color.Black
        Me.txtMaxIloscZamowien.Location = New System.Drawing.Point(97, 45)
        Me.txtMaxIloscZamowien.MaxLength = 10
        Me.txtMaxIloscZamowien.Name = "txtMaxIloscZamowien"
        Me.txtMaxIloscZamowien.Size = New System.Drawing.Size(45, 20)
        Me.txtMaxIloscZamowien.TabIndex = 2
        '
        'btnNotyfikacja
        '
        Me.btnNotyfikacja.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNotyfikacja.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnNotyfikacja.ForeColor = System.Drawing.Color.White
        Me.btnNotyfikacja.Location = New System.Drawing.Point(597, 218)
        Me.btnNotyfikacja.Name = "btnNotyfikacja"
        Me.btnNotyfikacja.Size = New System.Drawing.Size(150, 23)
        Me.btnNotyfikacja.TabIndex = 5
        Me.btnNotyfikacja.Text = "Edytuj notyfikacje"
        Me.btnNotyfikacja.UseVisualStyleBackColor = False
        '
        'gbAdresy
        '
        Me.gbAdresy.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbAdresy.Controls.Add(Me.lblAdresyOpis)
        Me.gbAdresy.Controls.Add(Me.btnAdresy)
        Me.gbAdresy.Controls.Add(Me.lblAdresy)
        Me.gbAdresy.ForeColor = System.Drawing.Color.Black
        Me.gbAdresy.Location = New System.Drawing.Point(457, 138)
        Me.gbAdresy.Name = "gbAdresy"
        Me.gbAdresy.Size = New System.Drawing.Size(306, 74)
        Me.gbAdresy.TabIndex = 4
        Me.gbAdresy.TabStop = False
        Me.gbAdresy.Text = "Adresy zdefiniowane dla użytkownika"
        '
        'lblAdresyOpis
        '
        Me.lblAdresyOpis.AutoSize = True
        Me.lblAdresyOpis.Location = New System.Drawing.Point(6, 24)
        Me.lblAdresyOpis.Name = "lblAdresyOpis"
        Me.lblAdresyOpis.Size = New System.Drawing.Size(151, 13)
        Me.lblAdresyOpis.TabIndex = 0
        Me.lblAdresyOpis.Text = "Ilość zdefiniowanych adresów:"
        '
        'btnAdresy
        '
        Me.btnAdresy.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnAdresy.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnAdresy.ForeColor = System.Drawing.Color.White
        Me.btnAdresy.Location = New System.Drawing.Point(140, 45)
        Me.btnAdresy.Name = "btnAdresy"
        Me.btnAdresy.Size = New System.Drawing.Size(150, 23)
        Me.btnAdresy.TabIndex = 2
        Me.btnAdresy.Text = "Edytuj adresy zdefiniowane"
        Me.btnAdresy.UseVisualStyleBackColor = False
        '
        'lblAdresy
        '
        Me.lblAdresy.AutoSize = True
        Me.lblAdresy.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblAdresy.Location = New System.Drawing.Point(163, 24)
        Me.lblAdresy.Name = "lblAdresy"
        Me.lblAdresy.Size = New System.Drawing.Size(14, 13)
        Me.lblAdresy.TabIndex = 1
        Me.lblAdresy.Text = "0"
        '
        'btnZmienHaslo
        '
        Me.btnZmienHaslo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnZmienHaslo.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnZmienHaslo.ForeColor = System.Drawing.Color.White
        Me.btnZmienHaslo.Location = New System.Drawing.Point(557, 110)
        Me.btnZmienHaslo.Name = "btnZmienHaslo"
        Me.btnZmienHaslo.Size = New System.Drawing.Size(100, 23)
        Me.btnZmienHaslo.TabIndex = 2
        Me.btnZmienHaslo.Text = "Zmień hasło"
        Me.btnZmienHaslo.UseVisualStyleBackColor = False
        Me.btnZmienHaslo.Visible = False
        '
        'btnUsunHaslo
        '
        Me.btnUsunHaslo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnUsunHaslo.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnUsunHaslo.ForeColor = System.Drawing.Color.White
        Me.btnUsunHaslo.Location = New System.Drawing.Point(663, 109)
        Me.btnUsunHaslo.Name = "btnUsunHaslo"
        Me.btnUsunHaslo.Size = New System.Drawing.Size(100, 23)
        Me.btnUsunHaslo.TabIndex = 3
        Me.btnUsunHaslo.Text = "Usuń hasło"
        Me.btnUsunHaslo.UseVisualStyleBackColor = False
        Me.btnUsunHaslo.Visible = False
        '
        'gbDanePodstawowe
        '
        Me.gbDanePodstawowe.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbDanePodstawowe.Controls.Add(Me.cmbSiecSprzedazy)
        Me.gbDanePodstawowe.Controls.Add(Me.lblSiecSprzedazy)
        Me.gbDanePodstawowe.Controls.Add(Me.cmbZespolSprzedazy)
        Me.gbDanePodstawowe.Controls.Add(Me.lblZespolSprzedazy)
        Me.gbDanePodstawowe.Controls.Add(Me.cmbRegionSprzedazy)
        Me.gbDanePodstawowe.Controls.Add(Me.lblRegionSprzedazy)
        Me.gbDanePodstawowe.Controls.Add(Me.cmbObszarSprzedazy)
        Me.gbDanePodstawowe.Controls.Add(Me.lblObszarSprzedazy)
        Me.gbDanePodstawowe.Controls.Add(Me.cmbTyp)
        Me.gbDanePodstawowe.Controls.Add(Me.lblTyp)
        Me.gbDanePodstawowe.Controls.Add(Me.cmbWielkosc)
        Me.gbDanePodstawowe.Controls.Add(Me.lblWielkosc)
        Me.gbDanePodstawowe.Controls.Add(Me.txtTelkom)
        Me.gbDanePodstawowe.Controls.Add(Me.lblEmail)
        Me.gbDanePodstawowe.Controls.Add(Me.lblTelkom)
        Me.gbDanePodstawowe.Controls.Add(Me.lblNazwa)
        Me.gbDanePodstawowe.Controls.Add(Me.lblNazwisko)
        Me.gbDanePodstawowe.Controls.Add(Me.txtEmail)
        Me.gbDanePodstawowe.Controls.Add(Me.txtNazwa)
        Me.gbDanePodstawowe.Controls.Add(Me.txtNazwisko)
        Me.gbDanePodstawowe.Controls.Add(Me.txtImie)
        Me.gbDanePodstawowe.Controls.Add(Me.lblImie)
        Me.gbDanePodstawowe.ForeColor = System.Drawing.Color.Black
        Me.gbDanePodstawowe.Location = New System.Drawing.Point(6, 6)
        Me.gbDanePodstawowe.MinimumSize = New System.Drawing.Size(445, 377)
        Me.gbDanePodstawowe.Name = "gbDanePodstawowe"
        Me.gbDanePodstawowe.Size = New System.Drawing.Size(445, 377)
        Me.gbDanePodstawowe.TabIndex = 0
        Me.gbDanePodstawowe.TabStop = False
        Me.gbDanePodstawowe.Text = "Dane podstawowe"
        '
        'cmbSiecSprzedazy
        '
        Me.cmbSiecSprzedazy.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbSiecSprzedazy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSiecSprzedazy.FormattingEnabled = True
        Me.cmbSiecSprzedazy.Location = New System.Drawing.Point(110, 254)
        Me.cmbSiecSprzedazy.Name = "cmbSiecSprzedazy"
        Me.cmbSiecSprzedazy.Size = New System.Drawing.Size(316, 21)
        Me.cmbSiecSprzedazy.TabIndex = 17
        '
        'lblSiecSprzedazy
        '
        Me.lblSiecSprzedazy.AutoSize = True
        Me.lblSiecSprzedazy.ForeColor = System.Drawing.Color.Black
        Me.lblSiecSprzedazy.Location = New System.Drawing.Point(6, 258)
        Me.lblSiecSprzedazy.Name = "lblSiecSprzedazy"
        Me.lblSiecSprzedazy.Size = New System.Drawing.Size(81, 13)
        Me.lblSiecSprzedazy.TabIndex = 16
        Me.lblSiecSprzedazy.Text = "Sieć sprzedaży:"
        '
        'cmbZespolSprzedazy
        '
        Me.cmbZespolSprzedazy.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbZespolSprzedazy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbZespolSprzedazy.FormattingEnabled = True
        Me.cmbZespolSprzedazy.Location = New System.Drawing.Point(110, 311)
        Me.cmbZespolSprzedazy.Name = "cmbZespolSprzedazy"
        Me.cmbZespolSprzedazy.Size = New System.Drawing.Size(316, 21)
        Me.cmbZespolSprzedazy.TabIndex = 21
        '
        'lblZespolSprzedazy
        '
        Me.lblZespolSprzedazy.AutoSize = True
        Me.lblZespolSprzedazy.ForeColor = System.Drawing.Color.Black
        Me.lblZespolSprzedazy.Location = New System.Drawing.Point(6, 315)
        Me.lblZespolSprzedazy.Name = "lblZespolSprzedazy"
        Me.lblZespolSprzedazy.Size = New System.Drawing.Size(94, 13)
        Me.lblZespolSprzedazy.TabIndex = 20
        Me.lblZespolSprzedazy.Text = "Zespół sprzedaży:"
        '
        'cmbRegionSprzedazy
        '
        Me.cmbRegionSprzedazy.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbRegionSprzedazy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRegionSprzedazy.FormattingEnabled = True
        Me.cmbRegionSprzedazy.Location = New System.Drawing.Point(110, 283)
        Me.cmbRegionSprzedazy.Name = "cmbRegionSprzedazy"
        Me.cmbRegionSprzedazy.Size = New System.Drawing.Size(316, 21)
        Me.cmbRegionSprzedazy.TabIndex = 19
        '
        'lblRegionSprzedazy
        '
        Me.lblRegionSprzedazy.AutoSize = True
        Me.lblRegionSprzedazy.ForeColor = System.Drawing.Color.Black
        Me.lblRegionSprzedazy.Location = New System.Drawing.Point(6, 287)
        Me.lblRegionSprzedazy.Name = "lblRegionSprzedazy"
        Me.lblRegionSprzedazy.Size = New System.Drawing.Size(94, 13)
        Me.lblRegionSprzedazy.TabIndex = 18
        Me.lblRegionSprzedazy.Text = "Region sprzedaży:"
        '
        'cmbObszarSprzedazy
        '
        Me.cmbObszarSprzedazy.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbObszarSprzedazy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbObszarSprzedazy.FormattingEnabled = True
        Me.cmbObszarSprzedazy.Location = New System.Drawing.Point(110, 225)
        Me.cmbObszarSprzedazy.Name = "cmbObszarSprzedazy"
        Me.cmbObszarSprzedazy.Size = New System.Drawing.Size(316, 21)
        Me.cmbObszarSprzedazy.TabIndex = 15
        '
        'lblObszarSprzedazy
        '
        Me.lblObszarSprzedazy.AutoSize = True
        Me.lblObszarSprzedazy.ForeColor = System.Drawing.Color.Black
        Me.lblObszarSprzedazy.Location = New System.Drawing.Point(6, 229)
        Me.lblObszarSprzedazy.Name = "lblObszarSprzedazy"
        Me.lblObszarSprzedazy.Size = New System.Drawing.Size(93, 13)
        Me.lblObszarSprzedazy.TabIndex = 14
        Me.lblObszarSprzedazy.Text = "Obszar sprzedaży:"
        '
        'cmbTyp
        '
        Me.cmbTyp.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbTyp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTyp.FormattingEnabled = True
        Me.cmbTyp.Location = New System.Drawing.Point(110, 198)
        Me.cmbTyp.Name = "cmbTyp"
        Me.cmbTyp.Size = New System.Drawing.Size(316, 21)
        Me.cmbTyp.TabIndex = 13
        '
        'lblTyp
        '
        Me.lblTyp.AutoSize = True
        Me.lblTyp.ForeColor = System.Drawing.Color.Black
        Me.lblTyp.Location = New System.Drawing.Point(6, 201)
        Me.lblTyp.Name = "lblTyp"
        Me.lblTyp.Size = New System.Drawing.Size(28, 13)
        Me.lblTyp.TabIndex = 12
        Me.lblTyp.Text = "Typ:"
        '
        'cmbWielkosc
        '
        Me.cmbWielkosc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbWielkosc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbWielkosc.FormattingEnabled = True
        Me.cmbWielkosc.Location = New System.Drawing.Point(110, 171)
        Me.cmbWielkosc.Name = "cmbWielkosc"
        Me.cmbWielkosc.Size = New System.Drawing.Size(316, 21)
        Me.cmbWielkosc.TabIndex = 11
        '
        'lblWielkosc
        '
        Me.lblWielkosc.AutoSize = True
        Me.lblWielkosc.ForeColor = System.Drawing.Color.Black
        Me.lblWielkosc.Location = New System.Drawing.Point(6, 174)
        Me.lblWielkosc.Name = "lblWielkosc"
        Me.lblWielkosc.Size = New System.Drawing.Size(54, 13)
        Me.lblWielkosc.TabIndex = 10
        Me.lblWielkosc.Text = "Wielkość:"
        '
        'txtTelkom
        '
        Me.txtTelkom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTelkom.Location = New System.Drawing.Point(110, 100)
        Me.txtTelkom.Mask = "000000000"
        Me.txtTelkom.Name = "txtTelkom"
        Me.txtTelkom.Size = New System.Drawing.Size(316, 20)
        Me.txtTelkom.TabIndex = 7
        '
        'lblEmail
        '
        Me.lblEmail.AutoSize = True
        Me.lblEmail.ForeColor = System.Drawing.Color.Black
        Me.lblEmail.Location = New System.Drawing.Point(6, 129)
        Me.lblEmail.Name = "lblEmail"
        Me.lblEmail.Size = New System.Drawing.Size(35, 13)
        Me.lblEmail.TabIndex = 8
        Me.lblEmail.Text = "Email:"
        '
        'lblTelkom
        '
        Me.lblTelkom.AutoSize = True
        Me.lblTelkom.ForeColor = System.Drawing.Color.Black
        Me.lblTelkom.Location = New System.Drawing.Point(6, 103)
        Me.lblTelkom.Name = "lblTelkom"
        Me.lblTelkom.Size = New System.Drawing.Size(85, 13)
        Me.lblTelkom.TabIndex = 6
        Me.lblTelkom.Text = "Tel. komórkowy:"
        '
        'lblNazwa
        '
        Me.lblNazwa.AutoSize = True
        Me.lblNazwa.ForeColor = System.Drawing.Color.Black
        Me.lblNazwa.Location = New System.Drawing.Point(6, 74)
        Me.lblNazwa.Name = "lblNazwa"
        Me.lblNazwa.Size = New System.Drawing.Size(103, 13)
        Me.lblNazwa.TabIndex = 4
        Me.lblNazwa.Text = "Nazwa wyświetlana:"
        '
        'lblNazwisko
        '
        Me.lblNazwisko.AutoSize = True
        Me.lblNazwisko.ForeColor = System.Drawing.Color.Black
        Me.lblNazwisko.Location = New System.Drawing.Point(6, 48)
        Me.lblNazwisko.Name = "lblNazwisko"
        Me.lblNazwisko.Size = New System.Drawing.Size(56, 13)
        Me.lblNazwisko.TabIndex = 2
        Me.lblNazwisko.Text = "Nazwisko:"
        '
        'txtEmail
        '
        Me.txtEmail.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtEmail.Location = New System.Drawing.Point(110, 126)
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(316, 20)
        Me.txtEmail.TabIndex = 9
        '
        'txtNazwa
        '
        Me.txtNazwa.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNazwa.Location = New System.Drawing.Point(110, 71)
        Me.txtNazwa.Name = "txtNazwa"
        Me.txtNazwa.Size = New System.Drawing.Size(316, 20)
        Me.txtNazwa.TabIndex = 5
        '
        'txtNazwisko
        '
        Me.txtNazwisko.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNazwisko.Location = New System.Drawing.Point(110, 45)
        Me.txtNazwisko.Name = "txtNazwisko"
        Me.txtNazwisko.Size = New System.Drawing.Size(316, 20)
        Me.txtNazwisko.TabIndex = 3
        '
        'txtImie
        '
        Me.txtImie.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtImie.Location = New System.Drawing.Point(110, 19)
        Me.txtImie.Name = "txtImie"
        Me.txtImie.Size = New System.Drawing.Size(316, 20)
        Me.txtImie.TabIndex = 1
        '
        'lblImie
        '
        Me.lblImie.AutoSize = True
        Me.lblImie.ForeColor = System.Drawing.Color.Black
        Me.lblImie.Location = New System.Drawing.Point(6, 22)
        Me.lblImie.Name = "lblImie"
        Me.lblImie.Size = New System.Drawing.Size(29, 13)
        Me.lblImie.TabIndex = 0
        Me.lblImie.Text = "Imię:"
        '
        'gbDaneLogowania
        '
        Me.gbDaneLogowania.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbDaneLogowania.Controls.Add(Me.lblHasloStatus)
        Me.gbDaneLogowania.Controls.Add(Me.lblHaslo2)
        Me.gbDaneLogowania.Controls.Add(Me.txtHaslo2)
        Me.gbDaneLogowania.Controls.Add(Me.lblHaslo)
        Me.gbDaneLogowania.Controls.Add(Me.txtHaslo)
        Me.gbDaneLogowania.Controls.Add(Me.txtLogin)
        Me.gbDaneLogowania.Controls.Add(Me.lblLogin)
        Me.gbDaneLogowania.ForeColor = System.Drawing.Color.Black
        Me.gbDaneLogowania.Location = New System.Drawing.Point(457, 6)
        Me.gbDaneLogowania.Name = "gbDaneLogowania"
        Me.gbDaneLogowania.Size = New System.Drawing.Size(306, 97)
        Me.gbDaneLogowania.TabIndex = 1
        Me.gbDaneLogowania.TabStop = False
        Me.gbDaneLogowania.Text = "Dane do logowania"
        '
        'lblHasloStatus
        '
        Me.lblHasloStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblHasloStatus.Location = New System.Drawing.Point(51, 48)
        Me.lblHasloStatus.Name = "lblHasloStatus"
        Me.lblHasloStatus.Size = New System.Drawing.Size(239, 15)
        Me.lblHasloStatus.TabIndex = 3
        Me.lblHasloStatus.Text = "v"
        Me.lblHasloStatus.Visible = False
        '
        'lblHaslo2
        '
        Me.lblHaslo2.AutoSize = True
        Me.lblHaslo2.Location = New System.Drawing.Point(6, 74)
        Me.lblHaslo2.Name = "lblHaslo2"
        Me.lblHaslo2.Size = New System.Drawing.Size(85, 13)
        Me.lblHaslo2.TabIndex = 5
        Me.lblHaslo2.Text = "Hasło (powtórz):"
        '
        'txtHaslo2
        '
        Me.txtHaslo2.Location = New System.Drawing.Point(115, 71)
        Me.txtHaslo2.Name = "txtHaslo2"
        Me.txtHaslo2.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtHaslo2.Size = New System.Drawing.Size(175, 20)
        Me.txtHaslo2.TabIndex = 6
        '
        'lblHaslo
        '
        Me.lblHaslo.AutoSize = True
        Me.lblHaslo.Location = New System.Drawing.Point(6, 48)
        Me.lblHaslo.Name = "lblHaslo"
        Me.lblHaslo.Size = New System.Drawing.Size(39, 13)
        Me.lblHaslo.TabIndex = 2
        Me.lblHaslo.Text = "Hasło:"
        '
        'txtHaslo
        '
        Me.txtHaslo.Location = New System.Drawing.Point(115, 45)
        Me.txtHaslo.Name = "txtHaslo"
        Me.txtHaslo.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtHaslo.Size = New System.Drawing.Size(175, 20)
        Me.txtHaslo.TabIndex = 4
        '
        'txtLogin
        '
        Me.txtLogin.Location = New System.Drawing.Point(115, 19)
        Me.txtLogin.Name = "txtLogin"
        Me.txtLogin.Size = New System.Drawing.Size(175, 20)
        Me.txtLogin.TabIndex = 1
        '
        'lblLogin
        '
        Me.lblLogin.AutoSize = True
        Me.lblLogin.Location = New System.Drawing.Point(6, 22)
        Me.lblLogin.Name = "lblLogin"
        Me.lblLogin.Size = New System.Drawing.Size(36, 13)
        Me.lblLogin.TabIndex = 0
        Me.lblLogin.Text = "Login:"
        '
        'gbDaneDpd
        '
        Me.gbDaneDpd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbDaneDpd.Controls.Add(Me.btnDaneDostawy)
        Me.gbDaneDpd.ForeColor = System.Drawing.Color.Black
        Me.gbDaneDpd.Location = New System.Drawing.Point(457, 247)
        Me.gbDaneDpd.Name = "gbDaneDpd"
        Me.gbDaneDpd.Size = New System.Drawing.Size(306, 51)
        Me.gbDaneDpd.TabIndex = 6
        Me.gbDaneDpd.TabStop = False
        Me.gbDaneDpd.Text = "Użytkownik może edytować dane dostawy "
        '
        'btnDaneDostawy
        '
        Me.btnDaneDostawy.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDaneDostawy.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnDaneDostawy.ForeColor = System.Drawing.Color.White
        Me.btnDaneDostawy.Location = New System.Drawing.Point(100, 22)
        Me.btnDaneDostawy.Name = "btnDaneDostawy"
        Me.btnDaneDostawy.Size = New System.Drawing.Size(190, 23)
        Me.btnDaneDostawy.TabIndex = 0
        Me.btnDaneDostawy.Text = "Edytuj ustawiania danych dostawy"
        Me.btnDaneDostawy.UseVisualStyleBackColor = False
        '
        'tpGrupy
        '
        Me.tpGrupy.Controls.Add(Me.btnEdytujGrupy)
        Me.tpGrupy.Controls.Add(Me.dgvGrupy)
        Me.tpGrupy.Location = New System.Drawing.Point(4, 22)
        Me.tpGrupy.Name = "tpGrupy"
        Me.tpGrupy.Padding = New System.Windows.Forms.Padding(3)
        Me.tpGrupy.Size = New System.Drawing.Size(769, 389)
        Me.tpGrupy.TabIndex = 1
        Me.tpGrupy.Text = "Grupy"
        Me.tpGrupy.UseVisualStyleBackColor = True
        '
        'btnEdytujGrupy
        '
        Me.btnEdytujGrupy.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnEdytujGrupy.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnEdytujGrupy.ForeColor = System.Drawing.Color.White
        Me.btnEdytujGrupy.Location = New System.Drawing.Point(563, 349)
        Me.btnEdytujGrupy.Name = "btnEdytujGrupy"
        Me.btnEdytujGrupy.Size = New System.Drawing.Size(200, 23)
        Me.btnEdytujGrupy.TabIndex = 3
        Me.btnEdytujGrupy.Text = "Edytuj przynależność do grup..."
        Me.btnEdytujGrupy.UseVisualStyleBackColor = False
        '
        'dgvGrupy
        '
        Me.dgvGrupy.AllowUserToAddRows = False
        Me.dgvGrupy.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue
        Me.dgvGrupy.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvGrupy.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvGrupy.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgvGrupy.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvGrupy.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvGrupy.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvGrupy.ColumnHeadersVisible = False
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvGrupy.DefaultCellStyle = DataGridViewCellStyle3
        Me.dgvGrupy.Location = New System.Drawing.Point(3, 3)
        Me.dgvGrupy.MultiSelect = False
        Me.dgvGrupy.Name = "dgvGrupy"
        Me.dgvGrupy.ReadOnly = True
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvGrupy.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.dgvGrupy.RowHeadersVisible = False
        Me.dgvGrupy.Size = New System.Drawing.Size(760, 341)
        Me.dgvGrupy.TabIndex = 2
        '
        'tpFunkcje
        '
        Me.tpFunkcje.Controls.Add(Me.dgvFunkcje)
        Me.tpFunkcje.Controls.Add(Me.btnEdytujFunkcje)
        Me.tpFunkcje.Location = New System.Drawing.Point(4, 22)
        Me.tpFunkcje.Name = "tpFunkcje"
        Me.tpFunkcje.Padding = New System.Windows.Forms.Padding(3)
        Me.tpFunkcje.Size = New System.Drawing.Size(769, 389)
        Me.tpFunkcje.TabIndex = 2
        Me.tpFunkcje.Text = "Funkcje"
        Me.tpFunkcje.UseVisualStyleBackColor = True
        '
        'dgvFunkcje
        '
        Me.dgvFunkcje.AllowUserToAddRows = False
        Me.dgvFunkcje.AllowUserToResizeRows = False
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.AliceBlue
        Me.dgvFunkcje.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle5
        Me.dgvFunkcje.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvFunkcje.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgvFunkcje.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvFunkcje.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.dgvFunkcje.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvFunkcje.ColumnHeadersVisible = False
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvFunkcje.DefaultCellStyle = DataGridViewCellStyle7
        Me.dgvFunkcje.Location = New System.Drawing.Point(2, 4)
        Me.dgvFunkcje.MultiSelect = False
        Me.dgvFunkcje.Name = "dgvFunkcje"
        Me.dgvFunkcje.ReadOnly = True
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvFunkcje.RowHeadersDefaultCellStyle = DataGridViewCellStyle8
        Me.dgvFunkcje.RowHeadersVisible = False
        Me.dgvFunkcje.Size = New System.Drawing.Size(764, 341)
        Me.dgvFunkcje.TabIndex = 0
        '
        'btnEdytujFunkcje
        '
        Me.btnEdytujFunkcje.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnEdytujFunkcje.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnEdytujFunkcje.ForeColor = System.Drawing.Color.White
        Me.btnEdytujFunkcje.Location = New System.Drawing.Point(563, 350)
        Me.btnEdytujFunkcje.Name = "btnEdytujFunkcje"
        Me.btnEdytujFunkcje.Size = New System.Drawing.Size(200, 23)
        Me.btnEdytujFunkcje.TabIndex = 1
        Me.btnEdytujFunkcje.Text = "Edycja przypisanych funkcji..."
        Me.btnEdytujFunkcje.UseVisualStyleBackColor = False
        '
        'frmUzytkownik
        '
        Me.AcceptButton = Me.btnZastosuj
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnAnuluj
        Me.ClientSize = New System.Drawing.Size(802, 467)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.btnAnuluj)
        Me.Controls.Add(Me.btnZastosuj)
        Me.Controls.Add(Me.tc)
        Me.MinimumSize = New System.Drawing.Size(818, 505)
        Me.Name = "frmUzytkownik"
        Me.Text = "Użytkownik"
        Me.tc.ResumeLayout(False)
        Me.tpPodstawowe.ResumeLayout(False)
        Me.gbLimitZamowienPerUzytkownik.ResumeLayout(False)
        Me.gbLimitZamowienPerUzytkownik.PerformLayout()
        Me.gbAdresy.ResumeLayout(False)
        Me.gbAdresy.PerformLayout()
        Me.gbDanePodstawowe.ResumeLayout(False)
        Me.gbDanePodstawowe.PerformLayout()
        Me.gbDaneLogowania.ResumeLayout(False)
        Me.gbDaneLogowania.PerformLayout()
        Me.gbDaneDpd.ResumeLayout(False)
        Me.tpGrupy.ResumeLayout(False)
        CType(Me.dgvGrupy, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpFunkcje.ResumeLayout(False)
        CType(Me.dgvFunkcje, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents btnAnuluj As System.Windows.Forms.Button
    Friend WithEvents btnZastosuj As System.Windows.Forms.Button
    Friend WithEvents tc As System.Windows.Forms.TabControl
    Friend WithEvents tpPodstawowe As System.Windows.Forms.TabPage
    Friend WithEvents gbAdresy As System.Windows.Forms.GroupBox
    Friend WithEvents lblAdresyOpis As System.Windows.Forms.Label
    Friend WithEvents btnAdresy As System.Windows.Forms.Button
    Friend WithEvents lblAdresy As System.Windows.Forms.Label
    Friend WithEvents gbDanePodstawowe As System.Windows.Forms.GroupBox
    Friend WithEvents txtTelkom As System.Windows.Forms.MaskedTextBox
    Friend WithEvents lblEmail As System.Windows.Forms.Label
    Friend WithEvents lblTelkom As System.Windows.Forms.Label
    Friend WithEvents lblNazwa As System.Windows.Forms.Label
    Friend WithEvents lblNazwisko As System.Windows.Forms.Label
    Friend WithEvents txtEmail As System.Windows.Forms.TextBox
    Friend WithEvents txtNazwa As System.Windows.Forms.TextBox
    Friend WithEvents txtNazwisko As System.Windows.Forms.TextBox
    Friend WithEvents txtImie As System.Windows.Forms.TextBox
    Friend WithEvents lblImie As System.Windows.Forms.Label
    Friend WithEvents gbDaneLogowania As System.Windows.Forms.GroupBox
    Friend WithEvents lblHasloStatus As System.Windows.Forms.Label
    Friend WithEvents btnUsunHaslo As System.Windows.Forms.Button
    Friend WithEvents btnZmienHaslo As System.Windows.Forms.Button
    Friend WithEvents lblHaslo2 As System.Windows.Forms.Label
    Friend WithEvents txtHaslo2 As System.Windows.Forms.TextBox
    Friend WithEvents lblHaslo As System.Windows.Forms.Label
    Friend WithEvents txtHaslo As System.Windows.Forms.TextBox
    Friend WithEvents txtLogin As System.Windows.Forms.TextBox
    Friend WithEvents lblLogin As System.Windows.Forms.Label
    Friend WithEvents tpGrupy As System.Windows.Forms.TabPage
    Friend WithEvents btnEdytujGrupy As System.Windows.Forms.Button
    Friend WithEvents dgvGrupy As System.Windows.Forms.DataGridView
    Friend WithEvents tpFunkcje As System.Windows.Forms.TabPage
    Friend WithEvents dgvFunkcje As System.Windows.Forms.DataGridView
    Friend WithEvents btnEdytujFunkcje As System.Windows.Forms.Button
    Friend WithEvents cmbWielkosc As System.Windows.Forms.ComboBox
    Friend WithEvents lblWielkosc As System.Windows.Forms.Label
    Friend WithEvents cmbTyp As System.Windows.Forms.ComboBox
    Friend WithEvents lblTyp As System.Windows.Forms.Label
    Friend WithEvents cmbRegionSprzedazy As System.Windows.Forms.ComboBox
    Friend WithEvents lblRegionSprzedazy As System.Windows.Forms.Label
    Friend WithEvents cmbObszarSprzedazy As System.Windows.Forms.ComboBox
    Friend WithEvents lblObszarSprzedazy As System.Windows.Forms.Label
    Friend WithEvents cmbZespolSprzedazy As System.Windows.Forms.ComboBox
    Friend WithEvents lblZespolSprzedazy As System.Windows.Forms.Label
    Friend WithEvents cmbSiecSprzedazy As System.Windows.Forms.ComboBox
    Friend WithEvents lblSiecSprzedazy As System.Windows.Forms.Label
    Friend WithEvents gbDaneDpd As System.Windows.Forms.GroupBox
    Friend WithEvents btnDaneDostawy As System.Windows.Forms.Button
    Friend WithEvents btnNotyfikacja As System.Windows.Forms.Button
    Friend WithEvents gbLimitZamowienPerUzytkownik As System.Windows.Forms.GroupBox
    Friend WithEvents lblIloscZamowien As System.Windows.Forms.Label
    Friend WithEvents lblOkres As System.Windows.Forms.Label
    Friend WithEvents chkCzyLimitWydanOsoba As System.Windows.Forms.CheckBox
    Friend WithEvents cmbTypOkresZamowien As System.Windows.Forms.ComboBox
    Friend WithEvents txtMaxIloscZamowien As System.Windows.Forms.TextBox
End Class
