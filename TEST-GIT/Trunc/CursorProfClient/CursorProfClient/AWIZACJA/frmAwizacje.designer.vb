<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAwizacje
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAwizacje))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnUsunDostawce = New System.Windows.Forms.Button()
        Me.btnEdycjaDostawcy = New System.Windows.Forms.Button()
        Me.cmbNazwaDostawcy = New System.Windows.Forms.ComboBox()
        Me.btnDodajDostawce = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtKrajDostawca = New System.Windows.Forms.TextBox()
        Me.txtMiastoDostawca = New System.Windows.Forms.TextBox()
        Me.txtKodPocztowy = New System.Windows.Forms.TextBox()
        Me.txtAdresDostawca = New System.Windows.Forms.TextBox()
        Me.txtNrPO = New System.Windows.Forms.TextBox()
        Me.gbNrPO = New System.Windows.Forms.GroupBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.dtpPlanowanaDataDostawy = New System.Windows.Forms.DateTimePicker()
        Me.btnZatwierdz = New System.Windows.Forms.Button()
        Me.btnAnuluj = New System.Windows.Forms.Button()
        Me.dgv = New System.Windows.Forms.DataGridView()
        Me.gbDaneKontaktowe = New System.Windows.Forms.GroupBox()
        Me.txtUwagi = New System.Windows.Forms.TextBox()
        Me.lblUwagi = New System.Windows.Forms.Label()
        Me.txtTelefonKontaktowy = New System.Windows.Forms.TextBox()
        Me.lblTelefonKontaktowy = New System.Windows.Forms.Label()
        Me.lblOsobaKontaktowa = New System.Windows.Forms.Label()
        Me.txtOsobaKontaktowa = New System.Windows.Forms.TextBox()
        Me.btnUsunAwizo = New System.Windows.Forms.Button()
        Me.btnZapisz = New System.Windows.Forms.Button()
        Me.btnDodajPozycjeZExcela = New System.Windows.Forms.Button()
        Me.btnUsunPozycje = New System.Windows.Forms.Button()
        Me.btnDodajPozycje = New System.Windows.Forms.Button()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtIloscPaczek = New System.Windows.Forms.TextBox()
        Me.txtIloscPalet = New System.Windows.Forms.TextBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.gbTypDostawy = New System.Windows.Forms.GroupBox()
        Me.cmbTypDostawy = New System.Windows.Forms.ComboBox()
        Me.GroupBox1.SuspendLayout()
        Me.gbNrPO.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbDaneKontaktowe.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.gbTypDostawy.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnUsunDostawce)
        Me.GroupBox1.Controls.Add(Me.btnEdycjaDostawcy)
        Me.GroupBox1.Controls.Add(Me.cmbNazwaDostawcy)
        Me.GroupBox1.Controls.Add(Me.btnDodajDostawce)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtKrajDostawca)
        Me.GroupBox1.Controls.Add(Me.txtMiastoDostawca)
        Me.GroupBox1.Controls.Add(Me.txtKodPocztowy)
        Me.GroupBox1.Controls.Add(Me.txtAdresDostawca)
        Me.GroupBox1.Location = New System.Drawing.Point(4, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(434, 124)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Dostawca"
        '
        'btnUsunDostawce
        '
        Me.btnUsunDostawce.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnUsunDostawce.ForeColor = System.Drawing.Color.White
        Me.btnUsunDostawce.Image = CType(resources.GetObject("btnUsunDostawce.Image"), System.Drawing.Image)
        Me.btnUsunDostawce.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnUsunDostawce.Location = New System.Drawing.Point(331, 87)
        Me.btnUsunDostawce.Name = "btnUsunDostawce"
        Me.btnUsunDostawce.Size = New System.Drawing.Size(94, 26)
        Me.btnUsunDostawce.TabIndex = 12
        Me.btnUsunDostawce.Text = "Usuń"
        Me.ToolTip1.SetToolTip(Me.btnUsunDostawce, "Usuwa wybranego dostawcę.")
        Me.btnUsunDostawce.UseVisualStyleBackColor = False
        '
        'btnEdycjaDostawcy
        '
        Me.btnEdycjaDostawcy.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnEdycjaDostawcy.ForeColor = System.Drawing.Color.White
        Me.btnEdycjaDostawcy.Image = CType(resources.GetObject("btnEdycjaDostawcy.Image"), System.Drawing.Image)
        Me.btnEdycjaDostawcy.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEdycjaDostawcy.Location = New System.Drawing.Point(331, 55)
        Me.btnEdycjaDostawcy.Name = "btnEdycjaDostawcy"
        Me.btnEdycjaDostawcy.Size = New System.Drawing.Size(94, 26)
        Me.btnEdycjaDostawcy.TabIndex = 11
        Me.btnEdycjaDostawcy.Text = "Edytuj"
        Me.ToolTip1.SetToolTip(Me.btnEdycjaDostawcy, "Edycja wybranego dostawcy.")
        Me.btnEdycjaDostawcy.UseVisualStyleBackColor = False
        '
        'cmbNazwaDostawcy
        '
        Me.cmbNazwaDostawcy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbNazwaDostawcy.DropDownWidth = 360
        Me.cmbNazwaDostawcy.FormattingEnabled = True
        Me.cmbNazwaDostawcy.Location = New System.Drawing.Point(55, 19)
        Me.cmbNazwaDostawcy.Name = "cmbNazwaDostawcy"
        Me.cmbNazwaDostawcy.Size = New System.Drawing.Size(270, 21)
        Me.cmbNazwaDostawcy.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.cmbNazwaDostawcy, "Nazwa dostawcy")
        '
        'btnDodajDostawce
        '
        Me.btnDodajDostawce.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnDodajDostawce.ForeColor = System.Drawing.Color.White
        Me.btnDodajDostawce.Location = New System.Drawing.Point(331, 20)
        Me.btnDodajDostawce.Name = "btnDodajDostawce"
        Me.btnDodajDostawce.Size = New System.Drawing.Size(94, 29)
        Me.btnDodajDostawce.TabIndex = 10
        Me.btnDodajDostawce.Text = "Nowy Dostawca"
        Me.ToolTip1.SetToolTip(Me.btnDodajDostawce, "Dodanie nowego dostawcy.")
        Me.btnDodajDostawce.UseVisualStyleBackColor = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(145, 74)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(41, 13)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Miasto:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(158, 100)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(28, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Kraj:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(6, 49)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(37, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Adres:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(6, 74)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(77, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Kod pocztowy:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(6, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(43, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Nazwa:"
        '
        'txtKrajDostawca
        '
        Me.txtKrajDostawca.Enabled = False
        Me.txtKrajDostawca.Location = New System.Drawing.Point(192, 97)
        Me.txtKrajDostawca.Name = "txtKrajDostawca"
        Me.txtKrajDostawca.Size = New System.Drawing.Size(133, 20)
        Me.txtKrajDostawca.TabIndex = 9
        '
        'txtMiastoDostawca
        '
        Me.txtMiastoDostawca.Enabled = False
        Me.txtMiastoDostawca.Location = New System.Drawing.Point(192, 71)
        Me.txtMiastoDostawca.Name = "txtMiastoDostawca"
        Me.txtMiastoDostawca.Size = New System.Drawing.Size(133, 20)
        Me.txtMiastoDostawca.TabIndex = 7
        '
        'txtKodPocztowy
        '
        Me.txtKodPocztowy.Enabled = False
        Me.txtKodPocztowy.Location = New System.Drawing.Point(85, 72)
        Me.txtKodPocztowy.Name = "txtKodPocztowy"
        Me.txtKodPocztowy.Size = New System.Drawing.Size(54, 20)
        Me.txtKodPocztowy.TabIndex = 5
        '
        'txtAdresDostawca
        '
        Me.txtAdresDostawca.Enabled = False
        Me.txtAdresDostawca.Location = New System.Drawing.Point(55, 46)
        Me.txtAdresDostawca.Name = "txtAdresDostawca"
        Me.txtAdresDostawca.Size = New System.Drawing.Size(270, 20)
        Me.txtAdresDostawca.TabIndex = 3
        '
        'txtNrPO
        '
        Me.txtNrPO.Location = New System.Drawing.Point(13, 15)
        Me.txtNrPO.Name = "txtNrPO"
        Me.txtNrPO.Size = New System.Drawing.Size(234, 20)
        Me.txtNrPO.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.txtNrPO, "Numer PO (Purchase Order) ")
        '
        'gbNrPO
        '
        Me.gbNrPO.Controls.Add(Me.txtNrPO)
        Me.gbNrPO.Location = New System.Drawing.Point(4, 133)
        Me.gbNrPO.Name = "gbNrPO"
        Me.gbNrPO.Size = New System.Drawing.Size(253, 43)
        Me.gbNrPO.TabIndex = 1
        Me.gbNrPO.TabStop = False
        Me.gbNrPO.Text = "Numer PO"
        Me.ToolTip1.SetToolTip(Me.gbNrPO, "Numer PO (Purchase Order) " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "oznacza zewnętrzny numer zlecenia zakupu")
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.dtpPlanowanaDataDostawy)
        Me.GroupBox3.Location = New System.Drawing.Point(4, 182)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(253, 43)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Planowana data dostawy"
        Me.ToolTip1.SetToolTip(Me.GroupBox3, "Planowana data dostawy oznacza planowany termin przyjazdu dostawy do magazynu.")
        '
        'dtpPlanowanaDataDostawy
        '
        Me.dtpPlanowanaDataDostawy.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpPlanowanaDataDostawy.Location = New System.Drawing.Point(15, 15)
        Me.dtpPlanowanaDataDostawy.Name = "dtpPlanowanaDataDostawy"
        Me.dtpPlanowanaDataDostawy.Size = New System.Drawing.Size(232, 20)
        Me.dtpPlanowanaDataDostawy.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.dtpPlanowanaDataDostawy, "Planowana data dostawy oznacza planowany termin przyjazdu dostawy do magazynu.")
        '
        'btnZatwierdz
        '
        Me.btnZatwierdz.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnZatwierdz.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnZatwierdz.ForeColor = System.Drawing.Color.White
        Me.btnZatwierdz.Location = New System.Drawing.Point(505, 433)
        Me.btnZatwierdz.Name = "btnZatwierdz"
        Me.btnZatwierdz.Size = New System.Drawing.Size(92, 30)
        Me.btnZatwierdz.TabIndex = 11
        Me.btnZatwierdz.Text = "Zatwierdź"
        Me.ToolTip1.SetToolTip(Me.btnZatwierdz, resources.GetString("btnZatwierdz.ToolTip"))
        Me.btnZatwierdz.UseVisualStyleBackColor = False
        '
        'btnAnuluj
        '
        Me.btnAnuluj.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAnuluj.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnAnuluj.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnAnuluj.ForeColor = System.Drawing.Color.White
        Me.btnAnuluj.Location = New System.Drawing.Point(603, 433)
        Me.btnAnuluj.Name = "btnAnuluj"
        Me.btnAnuluj.Size = New System.Drawing.Size(105, 30)
        Me.btnAnuluj.TabIndex = 13
        Me.btnAnuluj.Text = "Anuluj"
        Me.btnAnuluj.UseVisualStyleBackColor = False
        '
        'dgv
        '
        Me.dgv.AllowUserToAddRows = False
        Me.dgv.AllowUserToDeleteRows = False
        Me.dgv.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.White
        Me.dgv.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv.DefaultCellStyle = DataGridViewCellStyle3
        Me.dgv.Location = New System.Drawing.Point(4, 229)
        Me.dgv.Name = "dgv"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.dgv.RowHeadersVisible = False
        Me.dgv.Size = New System.Drawing.Size(593, 198)
        Me.dgv.TabIndex = 6
        '
        'gbDaneKontaktowe
        '
        Me.gbDaneKontaktowe.Controls.Add(Me.txtUwagi)
        Me.gbDaneKontaktowe.Controls.Add(Me.lblUwagi)
        Me.gbDaneKontaktowe.Controls.Add(Me.txtTelefonKontaktowy)
        Me.gbDaneKontaktowe.Controls.Add(Me.lblTelefonKontaktowy)
        Me.gbDaneKontaktowe.Controls.Add(Me.lblOsobaKontaktowa)
        Me.gbDaneKontaktowe.Controls.Add(Me.txtOsobaKontaktowa)
        Me.gbDaneKontaktowe.Location = New System.Drawing.Point(444, 52)
        Me.gbDaneKontaktowe.Name = "gbDaneKontaktowe"
        Me.gbDaneKontaktowe.Size = New System.Drawing.Size(256, 173)
        Me.gbDaneKontaktowe.TabIndex = 5
        Me.gbDaneKontaktowe.TabStop = False
        '
        'txtUwagi
        '
        Me.txtUwagi.Location = New System.Drawing.Point(6, 94)
        Me.txtUwagi.Multiline = True
        Me.txtUwagi.Name = "txtUwagi"
        Me.txtUwagi.Size = New System.Drawing.Size(244, 65)
        Me.txtUwagi.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.txtUwagi, "Uwagi do awiza")
        '
        'lblUwagi
        '
        Me.lblUwagi.AutoSize = True
        Me.lblUwagi.Location = New System.Drawing.Point(6, 76)
        Me.lblUwagi.Name = "lblUwagi"
        Me.lblUwagi.Size = New System.Drawing.Size(40, 13)
        Me.lblUwagi.TabIndex = 4
        Me.lblUwagi.Text = "Uwagi:"
        '
        'txtTelefonKontaktowy
        '
        Me.txtTelefonKontaktowy.Location = New System.Drawing.Point(98, 52)
        Me.txtTelefonKontaktowy.Name = "txtTelefonKontaktowy"
        Me.txtTelefonKontaktowy.Size = New System.Drawing.Size(150, 20)
        Me.txtTelefonKontaktowy.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.txtTelefonKontaktowy, "Telefon kontaktowy")
        '
        'lblTelefonKontaktowy
        '
        Me.lblTelefonKontaktowy.AutoSize = True
        Me.lblTelefonKontaktowy.Location = New System.Drawing.Point(6, 55)
        Me.lblTelefonKontaktowy.Name = "lblTelefonKontaktowy"
        Me.lblTelefonKontaktowy.Size = New System.Drawing.Size(86, 13)
        Me.lblTelefonKontaktowy.TabIndex = 2
        Me.lblTelefonKontaktowy.Text = "Tel. kontaktowy:"
        '
        'lblOsobaKontaktowa
        '
        Me.lblOsobaKontaktowa.AutoSize = True
        Me.lblOsobaKontaktowa.Location = New System.Drawing.Point(6, 10)
        Me.lblOsobaKontaktowa.Name = "lblOsobaKontaktowa"
        Me.lblOsobaKontaktowa.Size = New System.Drawing.Size(100, 13)
        Me.lblOsobaKontaktowa.TabIndex = 0
        Me.lblOsobaKontaktowa.Text = "Osoba kontaktowa:"
        Me.ToolTip1.SetToolTip(Me.lblOsobaKontaktowa, "Osoba kontaktowa")
        '
        'txtOsobaKontaktowa
        '
        Me.txtOsobaKontaktowa.Location = New System.Drawing.Point(6, 26)
        Me.txtOsobaKontaktowa.Name = "txtOsobaKontaktowa"
        Me.txtOsobaKontaktowa.Size = New System.Drawing.Size(242, 20)
        Me.txtOsobaKontaktowa.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.txtOsobaKontaktowa, "Osoba kontaktowa")
        '
        'btnUsunAwizo
        '
        Me.btnUsunAwizo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnUsunAwizo.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnUsunAwizo.ForeColor = System.Drawing.Color.White
        Me.btnUsunAwizo.Location = New System.Drawing.Point(319, 433)
        Me.btnUsunAwizo.Name = "btnUsunAwizo"
        Me.btnUsunAwizo.Size = New System.Drawing.Size(77, 30)
        Me.btnUsunAwizo.TabIndex = 10
        Me.btnUsunAwizo.Text = "Usuń"
        Me.ToolTip1.SetToolTip(Me.btnUsunAwizo, "Po kliknięciu tego przycisku następuje usunięcie edytowanego awiza")
        Me.btnUsunAwizo.UseVisualStyleBackColor = False
        '
        'btnZapisz
        '
        Me.btnZapisz.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnZapisz.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnZapisz.ForeColor = System.Drawing.Color.White
        Me.btnZapisz.Location = New System.Drawing.Point(402, 433)
        Me.btnZapisz.Name = "btnZapisz"
        Me.btnZapisz.Size = New System.Drawing.Size(98, 30)
        Me.btnZapisz.TabIndex = 12
        Me.btnZapisz.Text = "Zapisz zmiany"
        Me.ToolTip1.SetToolTip(Me.btnZapisz, "Przycisk umożliwia zapisanie zmian w awizie. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Do edycji awiza można powrócić po " & _
        "zamknięciu aplikacji, klikając w oknie Zarządzanie awizami przycisk Edytuj w odp" & _
        "owiednim wierszu.")
        Me.btnZapisz.UseVisualStyleBackColor = False
        '
        'btnDodajPozycjeZExcela
        '
        Me.btnDodajPozycjeZExcela.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDodajPozycjeZExcela.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnDodajPozycjeZExcela.ForeColor = System.Drawing.Color.White
        Me.btnDodajPozycjeZExcela.Image = CType(resources.GetObject("btnDodajPozycjeZExcela.Image"), System.Drawing.Image)
        Me.btnDodajPozycjeZExcela.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDodajPozycjeZExcela.Location = New System.Drawing.Point(600, 309)
        Me.btnDodajPozycjeZExcela.Name = "btnDodajPozycjeZExcela"
        Me.btnDodajPozycjeZExcela.Size = New System.Drawing.Size(108, 34)
        Me.btnDodajPozycjeZExcela.TabIndex = 9
        Me.btnDodajPozycjeZExcela.Text = "Dodaj pozycje  z pliku Excela"
        Me.btnDodajPozycjeZExcela.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.btnDodajPozycjeZExcela, "Przycisk umożliwia dodanie pozycji do awiza z ustalonego pliku Excela")
        Me.btnDodajPozycjeZExcela.UseVisualStyleBackColor = False
        '
        'btnUsunPozycje
        '
        Me.btnUsunPozycje.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnUsunPozycje.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnUsunPozycje.ForeColor = System.Drawing.Color.White
        Me.btnUsunPozycje.Image = CType(resources.GetObject("btnUsunPozycje.Image"), System.Drawing.Image)
        Me.btnUsunPozycje.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnUsunPozycje.Location = New System.Drawing.Point(600, 229)
        Me.btnUsunPozycje.Name = "btnUsunPozycje"
        Me.btnUsunPozycje.Size = New System.Drawing.Size(108, 34)
        Me.btnUsunPozycje.TabIndex = 7
        Me.btnUsunPozycje.Text = "Usuń pozycje"
        Me.ToolTip1.SetToolTip(Me.btnUsunPozycje, "Usuwa wszystkie zaznaczone pozycje (w kolumnie wybierz)")
        Me.btnUsunPozycje.UseVisualStyleBackColor = False
        '
        'btnDodajPozycje
        '
        Me.btnDodajPozycje.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDodajPozycje.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnDodajPozycje.ForeColor = System.Drawing.Color.White
        Me.btnDodajPozycje.Image = CType(resources.GetObject("btnDodajPozycje.Image"), System.Drawing.Image)
        Me.btnDodajPozycje.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDodajPozycje.Location = New System.Drawing.Point(600, 269)
        Me.btnDodajPozycje.Name = "btnDodajPozycje"
        Me.btnDodajPozycje.Size = New System.Drawing.Size(108, 34)
        Me.btnDodajPozycje.TabIndex = 8
        Me.btnDodajPozycje.Text = "Dodaj pozycje"
        Me.ToolTip1.SetToolTip(Me.btnDodajPozycje, "Przycisk pozwala na dodawanie pozycji do awiza.")
        Me.btnDodajPozycje.UseVisualStyleBackColor = False
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Label7)
        Me.GroupBox4.Controls.Add(Me.Label6)
        Me.GroupBox4.Controls.Add(Me.txtIloscPaczek)
        Me.GroupBox4.Controls.Add(Me.txtIloscPalet)
        Me.GroupBox4.Location = New System.Drawing.Point(263, 133)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(175, 92)
        Me.GroupBox4.TabIndex = 3
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Ilość palet i paczek"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(9, 52)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(70, 13)
        Me.Label7.TabIndex = 2
        Me.Label7.Text = "Ilość paczek:"
        Me.ToolTip1.SetToolTip(Me.Label7, "Oczekiwana ilość paczek w dostawie")
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(9, 26)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(58, 13)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "Ilość palet:"
        Me.ToolTip1.SetToolTip(Me.Label6, "Oczekiwana ilość palet w dostawie")
        '
        'txtIloscPaczek
        '
        Me.txtIloscPaczek.Location = New System.Drawing.Point(85, 49)
        Me.txtIloscPaczek.Name = "txtIloscPaczek"
        Me.txtIloscPaczek.Size = New System.Drawing.Size(76, 20)
        Me.txtIloscPaczek.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.txtIloscPaczek, "Oczekiwana ilość paczek w dostawie")
        '
        'txtIloscPalet
        '
        Me.txtIloscPalet.Location = New System.Drawing.Point(85, 23)
        Me.txtIloscPalet.Name = "txtIloscPalet"
        Me.txtIloscPalet.Size = New System.Drawing.Size(76, 20)
        Me.txtIloscPalet.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.txtIloscPalet, "Oczekiwana ilość palet w dostawie")
        '
        'ToolTip1
        '
        Me.ToolTip1.AutoPopDelay = 10000
        Me.ToolTip1.InitialDelay = 500
        Me.ToolTip1.ReshowDelay = 500
        '
        'gbTypDostawy
        '
        Me.gbTypDostawy.Controls.Add(Me.cmbTypDostawy)
        Me.gbTypDostawy.Location = New System.Drawing.Point(444, 3)
        Me.gbTypDostawy.Name = "gbTypDostawy"
        Me.gbTypDostawy.Size = New System.Drawing.Size(256, 49)
        Me.gbTypDostawy.TabIndex = 4
        Me.gbTypDostawy.TabStop = False
        Me.gbTypDostawy.Text = "Typ dostawy"
        Me.ToolTip1.SetToolTip(Me.gbTypDostawy, "Typ dostawy")
        '
        'cmbTypDostawy
        '
        Me.cmbTypDostawy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTypDostawy.DropDownWidth = 242
        Me.cmbTypDostawy.FormattingEnabled = True
        Me.cmbTypDostawy.Location = New System.Drawing.Point(6, 19)
        Me.cmbTypDostawy.Name = "cmbTypDostawy"
        Me.cmbTypDostawy.Size = New System.Drawing.Size(242, 21)
        Me.cmbTypDostawy.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.cmbTypDostawy, "Typ dostawy")
        '
        'frmAwizacje
        '
        Me.AcceptButton = Me.btnZapisz
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnAnuluj
        Me.ClientSize = New System.Drawing.Size(713, 467)
        Me.Controls.Add(Me.btnUsunAwizo)
        Me.Controls.Add(Me.btnZapisz)
        Me.Controls.Add(Me.btnZatwierdz)
        Me.Controls.Add(Me.btnAnuluj)
        Me.Controls.Add(Me.gbTypDostawy)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.btnDodajPozycjeZExcela)
        Me.Controls.Add(Me.gbDaneKontaktowe)
        Me.Controls.Add(Me.dgv)
        Me.Controls.Add(Me.btnUsunPozycje)
        Me.Controls.Add(Me.btnDodajPozycje)
        Me.Controls.Add(Me.gbNrPO)
        Me.Controls.Add(Me.GroupBox1)
        Me.MinimumSize = New System.Drawing.Size(721, 487)
        Me.Name = "frmAwizacje"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Awizacja"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.gbNrPO.ResumeLayout(False)
        Me.gbNrPO.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbDaneKontaktowe.ResumeLayout(False)
        Me.gbDaneKontaktowe.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.gbTypDostawy.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtKrajDostawca As System.Windows.Forms.TextBox
    Friend WithEvents txtMiastoDostawca As System.Windows.Forms.TextBox
    Friend WithEvents txtKodPocztowy As System.Windows.Forms.TextBox
    Friend WithEvents txtAdresDostawca As System.Windows.Forms.TextBox
    Friend WithEvents txtNrPO As System.Windows.Forms.TextBox
    Friend WithEvents gbNrPO As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents dtpPlanowanaDataDostawy As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnDodajPozycje As System.Windows.Forms.Button
    Friend WithEvents btnUsunPozycje As System.Windows.Forms.Button
    Friend WithEvents btnZatwierdz As System.Windows.Forms.Button
    Friend WithEvents btnAnuluj As System.Windows.Forms.Button
    Friend WithEvents dgv As System.Windows.Forms.DataGridView
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnDodajDostawce As System.Windows.Forms.Button
    Friend WithEvents cmbNazwaDostawcy As System.Windows.Forms.ComboBox
    Friend WithEvents gbDaneKontaktowe As System.Windows.Forms.GroupBox
    Friend WithEvents txtUwagi As System.Windows.Forms.TextBox
    Friend WithEvents lblUwagi As System.Windows.Forms.Label
    Friend WithEvents txtTelefonKontaktowy As System.Windows.Forms.TextBox
    Friend WithEvents lblTelefonKontaktowy As System.Windows.Forms.Label
    Friend WithEvents lblOsobaKontaktowa As System.Windows.Forms.Label
    Friend WithEvents txtOsobaKontaktowa As System.Windows.Forms.TextBox
    Friend WithEvents btnUsunAwizo As System.Windows.Forms.Button
    Friend WithEvents btnZapisz As System.Windows.Forms.Button
    Friend WithEvents btnDodajPozycjeZExcela As System.Windows.Forms.Button
    Friend WithEvents btnEdycjaDostawcy As System.Windows.Forms.Button
    Friend WithEvents btnUsunDostawce As System.Windows.Forms.Button
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents txtIloscPaczek As System.Windows.Forms.TextBox
    Friend WithEvents txtIloscPalet As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents gbTypDostawy As System.Windows.Forms.GroupBox
    Friend WithEvents cmbTypDostawy As System.Windows.Forms.ComboBox
End Class
