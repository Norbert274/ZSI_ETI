<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAwizoDodajPozycje
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.btnDodajPozycje = New System.Windows.Forms.Button()
        Me.txtWyszukajSKU = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.cmbGrupa = New System.Windows.Forms.ComboBox()
        Me.lblGrupa = New System.Windows.Forms.Label()
        Me.dgv = New System.Windows.Forms.DataGridView()
        Me.btnAnuluj = New System.Windows.Forms.Button()
        Me.btnDodajNowyProdukt = New System.Windows.Forms.Button()
        Me.btnZalozNoweProduktyZExcela = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnUsunProdukt = New System.Windows.Forms.Button()
        Me.ToolTip2 = New System.Windows.Forms.ToolTip(Me.components)
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnDodajPozycje
        '
        Me.btnDodajPozycje.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDodajPozycje.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnDodajPozycje.ForeColor = System.Drawing.Color.White
        Me.btnDodajPozycje.Location = New System.Drawing.Point(291, 482)
        Me.btnDodajPozycje.Name = "btnDodajPozycje"
        Me.btnDodajPozycje.Size = New System.Drawing.Size(91, 23)
        Me.btnDodajPozycje.TabIndex = 5
        Me.btnDodajPozycje.Text = "Dodaj"
        Me.ToolTip1.SetToolTip(Me.btnDodajPozycje, "Dodaje zaznaczone produkty do pozycji awiza")
        Me.btnDodajPozycje.UseVisualStyleBackColor = False
        '
        'txtWyszukajSKU
        '
        Me.txtWyszukajSKU.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtWyszukajSKU.Location = New System.Drawing.Point(14, 19)
        Me.txtWyszukajSKU.Name = "txtWyszukajSKU"
        Me.txtWyszukajSKU.Size = New System.Drawing.Size(196, 20)
        Me.txtWyszukajSKU.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.txtWyszukajSKU, "Wyszukiwanie produktu wg sku lub nazwy")
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.txtWyszukajSKU)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(226, 58)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Wyszukaj produkt:"
        Me.ToolTip1.SetToolTip(Me.GroupBox1, "Wyszukiwanie produktu wg sku lub nazwy")
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.cmbGrupa)
        Me.GroupBox2.Controls.Add(Me.lblGrupa)
        Me.GroupBox2.Controls.Add(Me.dgv)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 70)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(467, 406)
        Me.GroupBox2.TabIndex = 4
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Wybierz pozycje do awiza:"
        '
        'cmbGrupa
        '
        Me.cmbGrupa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbGrupa.FormattingEnabled = True
        Me.cmbGrupa.Location = New System.Drawing.Point(50, 23)
        Me.cmbGrupa.Name = "cmbGrupa"
        Me.cmbGrupa.Size = New System.Drawing.Size(209, 21)
        Me.cmbGrupa.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.cmbGrupa, "Grupa, na którą będą awizowane zaznaczone produkty")
        '
        'lblGrupa
        '
        Me.lblGrupa.AutoSize = True
        Me.lblGrupa.Location = New System.Drawing.Point(6, 26)
        Me.lblGrupa.Name = "lblGrupa"
        Me.lblGrupa.Size = New System.Drawing.Size(39, 13)
        Me.lblGrupa.TabIndex = 0
        Me.lblGrupa.Text = "Grupa:"
        Me.ToolTip1.SetToolTip(Me.lblGrupa, "Wybierz grupę, na którą będą awizowane zaznaczone produkty")
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
        Me.dgv.BackgroundColor = System.Drawing.Color.White
        Me.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv.Location = New System.Drawing.Point(6, 50)
        Me.dgv.Name = "dgv"
        Me.dgv.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.dgv.RowHeadersVisible = False
        Me.dgv.RowHeadersWidth = 5
        Me.dgv.Size = New System.Drawing.Size(455, 350)
        Me.dgv.TabIndex = 2
        '
        'btnAnuluj
        '
        Me.btnAnuluj.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAnuluj.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnAnuluj.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnAnuluj.ForeColor = System.Drawing.Color.White
        Me.btnAnuluj.Location = New System.Drawing.Point(388, 482)
        Me.btnAnuluj.Name = "btnAnuluj"
        Me.btnAnuluj.Size = New System.Drawing.Size(91, 23)
        Me.btnAnuluj.TabIndex = 6
        Me.btnAnuluj.Text = "Anuluj"
        Me.btnAnuluj.UseVisualStyleBackColor = False
        '
        'btnDodajNowyProdukt
        '
        Me.btnDodajNowyProdukt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDodajNowyProdukt.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnDodajNowyProdukt.ForeColor = System.Drawing.Color.White
        Me.btnDodajNowyProdukt.Location = New System.Drawing.Point(244, 12)
        Me.btnDodajNowyProdukt.Name = "btnDodajNowyProdukt"
        Me.btnDodajNowyProdukt.Size = New System.Drawing.Size(129, 23)
        Me.btnDodajNowyProdukt.TabIndex = 1
        Me.btnDodajNowyProdukt.Text = "Dodaj nowy produkt"
        Me.ToolTip1.SetToolTip(Me.btnDodajNowyProdukt, "Dodawanie nowego produktu do systemu")
        Me.btnDodajNowyProdukt.UseVisualStyleBackColor = False
        '
        'btnZalozNoweProduktyZExcela
        '
        Me.btnZalozNoweProduktyZExcela.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnZalozNoweProduktyZExcela.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnZalozNoweProduktyZExcela.ForeColor = System.Drawing.Color.White
        Me.btnZalozNoweProduktyZExcela.Location = New System.Drawing.Point(244, 41)
        Me.btnZalozNoweProduktyZExcela.Name = "btnZalozNoweProduktyZExcela"
        Me.btnZalozNoweProduktyZExcela.Size = New System.Drawing.Size(235, 23)
        Me.btnZalozNoweProduktyZExcela.TabIndex = 3
        Me.btnZalozNoweProduktyZExcela.Text = "Dodaj nowe produkty z pliku Excela"
        Me.ToolTip1.SetToolTip(Me.btnZalozNoweProduktyZExcela, "Importowanie nowych produktów do systemu z ustalonego pliku Excela")
        Me.btnZalozNoweProduktyZExcela.UseVisualStyleBackColor = False
        '
        'ToolTip1
        '
        Me.ToolTip1.AutoPopDelay = 10000
        Me.ToolTip1.InitialDelay = 500
        Me.ToolTip1.ReshowDelay = 500
        '
        'btnUsunProdukt
        '
        Me.btnUsunProdukt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnUsunProdukt.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnUsunProdukt.ForeColor = System.Drawing.Color.White
        Me.btnUsunProdukt.Location = New System.Drawing.Point(379, 12)
        Me.btnUsunProdukt.Name = "btnUsunProdukt"
        Me.btnUsunProdukt.Size = New System.Drawing.Size(100, 23)
        Me.btnUsunProdukt.TabIndex = 2
        Me.btnUsunProdukt.Text = "Usuń produkt"
        Me.btnUsunProdukt.UseVisualStyleBackColor = False
        '
        'frmAwizoDodajPozycje
        '
        Me.AcceptButton = Me.btnDodajPozycje
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnAnuluj
        Me.ClientSize = New System.Drawing.Size(491, 512)
        Me.Controls.Add(Me.btnUsunProdukt)
        Me.Controls.Add(Me.btnZalozNoweProduktyZExcela)
        Me.Controls.Add(Me.btnAnuluj)
        Me.Controls.Add(Me.btnDodajNowyProdukt)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnDodajPozycje)
        Me.MinimumSize = New System.Drawing.Size(507, 550)
        Me.Name = "frmAwizoDodajPozycje"
        Me.Text = "Awizo - Dodaj pozycje"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnDodajPozycje As System.Windows.Forms.Button
    Friend WithEvents txtWyszukajSKU As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents dgv As System.Windows.Forms.DataGridView
    Friend WithEvents btnAnuluj As System.Windows.Forms.Button
    Friend WithEvents btnDodajNowyProdukt As System.Windows.Forms.Button
    Friend WithEvents cmbGrupa As System.Windows.Forms.ComboBox
    Friend WithEvents lblGrupa As System.Windows.Forms.Label
    Friend WithEvents btnZalozNoweProduktyZExcela As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents btnUsunProdukt As System.Windows.Forms.Button
    Friend WithEvents ToolTip2 As System.Windows.Forms.ToolTip
End Class
