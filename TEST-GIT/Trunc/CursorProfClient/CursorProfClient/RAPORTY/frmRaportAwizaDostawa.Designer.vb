<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRaportAwizaDostawa
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRaportAwizaDostawa))
        Me.gbFiltry = New System.Windows.Forms.GroupBox()
        Me.txtPOFiltr = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.chbStatusy = New System.Windows.Forms.CheckBox()
        Me.dtpPlanowanaDataDo = New System.Windows.Forms.DateTimePicker()
        Me.dtpPlanowanaDataOd = New System.Windows.Forms.DateTimePicker()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtQrZaFiltr = New System.Windows.Forms.TextBox()
        Me.txtQrDostawaFiltr = New System.Windows.Forms.TextBox()
        Me.txtQrPzFiltr = New System.Windows.Forms.TextBox()
        Me.txtAwizoFiltr = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.listStatus = New System.Windows.Forms.CheckedListBox()
        Me.btnGeneruj = New System.Windows.Forms.Button()
        Me.dgv = New System.Windows.Forms.DataGridView()
        Me.btnZamknij = New System.Windows.Forms.Button()
        Me.btnEksportExcel = New System.Windows.Forms.Button()
        Me.gbFiltry.SuspendLayout()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gbFiltry
        '
        Me.gbFiltry.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbFiltry.Controls.Add(Me.txtPOFiltr)
        Me.gbFiltry.Controls.Add(Me.Label1)
        Me.gbFiltry.Controls.Add(Me.chbStatusy)
        Me.gbFiltry.Controls.Add(Me.dtpPlanowanaDataDo)
        Me.gbFiltry.Controls.Add(Me.dtpPlanowanaDataOd)
        Me.gbFiltry.Controls.Add(Me.Label11)
        Me.gbFiltry.Controls.Add(Me.Label10)
        Me.gbFiltry.Controls.Add(Me.Label9)
        Me.gbFiltry.Controls.Add(Me.Label5)
        Me.gbFiltry.Controls.Add(Me.Label4)
        Me.gbFiltry.Controls.Add(Me.Label3)
        Me.gbFiltry.Controls.Add(Me.txtQrZaFiltr)
        Me.gbFiltry.Controls.Add(Me.txtQrDostawaFiltr)
        Me.gbFiltry.Controls.Add(Me.txtQrPzFiltr)
        Me.gbFiltry.Controls.Add(Me.txtAwizoFiltr)
        Me.gbFiltry.Controls.Add(Me.Label2)
        Me.gbFiltry.Controls.Add(Me.listStatus)
        Me.gbFiltry.Controls.Add(Me.btnGeneruj)
        Me.gbFiltry.Location = New System.Drawing.Point(12, 12)
        Me.gbFiltry.Name = "gbFiltry"
        Me.gbFiltry.Size = New System.Drawing.Size(158, 441)
        Me.gbFiltry.TabIndex = 0
        Me.gbFiltry.TabStop = False
        Me.gbFiltry.Text = "Warunki wyszukiwania"
        '
        'txtPOFiltr
        '
        Me.txtPOFiltr.Location = New System.Drawing.Point(6, 71)
        Me.txtPOFiltr.Name = "txtPOFiltr"
        Me.txtPOFiltr.Size = New System.Drawing.Size(146, 20)
        Me.txtPOFiltr.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 55)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Numer PO:"
        '
        'chbStatusy
        '
        Me.chbStatusy.AutoSize = True
        Me.chbStatusy.Location = New System.Drawing.Point(6, 97)
        Me.chbStatusy.Name = "chbStatusy"
        Me.chbStatusy.Size = New System.Drawing.Size(61, 17)
        Me.chbStatusy.TabIndex = 4
        Me.chbStatusy.Text = "Statusy"
        Me.chbStatusy.UseVisualStyleBackColor = True
        '
        'dtpPlanowanaDataDo
        '
        Me.dtpPlanowanaDataDo.CustomFormat = "yyyy-MM-dd"
        Me.dtpPlanowanaDataDo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpPlanowanaDataDo.Location = New System.Drawing.Point(46, 386)
        Me.dtpPlanowanaDataDo.Name = "dtpPlanowanaDataDo"
        Me.dtpPlanowanaDataDo.Size = New System.Drawing.Size(106, 20)
        Me.dtpPlanowanaDataDo.TabIndex = 15
        '
        'dtpPlanowanaDataOd
        '
        Me.dtpPlanowanaDataOd.CustomFormat = "yyyy-MM-dd"
        Me.dtpPlanowanaDataOd.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpPlanowanaDataOd.Location = New System.Drawing.Point(46, 360)
        Me.dtpPlanowanaDataOd.Name = "dtpPlanowanaDataOd"
        Me.dtpPlanowanaDataOd.Size = New System.Drawing.Size(106, 20)
        Me.dtpPlanowanaDataOd.TabIndex = 13
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(6, 344)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(129, 13)
        Me.Label11.TabIndex = 12
        Me.Label11.Text = "Planowana data dostawy:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(6, 392)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(24, 13)
        Me.Label10.TabIndex = 16
        Me.Label10.Text = "Do:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(6, 366)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(24, 13)
        Me.Label9.TabIndex = 14
        Me.Label9.Text = "Od:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(3, 217)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(69, 13)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "OGUAR_ZA:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(3, 256)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(110, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "OGUAR_DOSTAWA:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(3, 295)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(69, 13)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "OGUAR_PZ:"
        '
        'txtQrZaFiltr
        '
        Me.txtQrZaFiltr.Location = New System.Drawing.Point(6, 233)
        Me.txtQrZaFiltr.Name = "txtQrZaFiltr"
        Me.txtQrZaFiltr.Size = New System.Drawing.Size(146, 20)
        Me.txtQrZaFiltr.TabIndex = 7
        '
        'txtQrDostawaFiltr
        '
        Me.txtQrDostawaFiltr.Location = New System.Drawing.Point(6, 272)
        Me.txtQrDostawaFiltr.Name = "txtQrDostawaFiltr"
        Me.txtQrDostawaFiltr.Size = New System.Drawing.Size(146, 20)
        Me.txtQrDostawaFiltr.TabIndex = 9
        '
        'txtQrPzFiltr
        '
        Me.txtQrPzFiltr.Location = New System.Drawing.Point(6, 311)
        Me.txtQrPzFiltr.Name = "txtQrPzFiltr"
        Me.txtQrPzFiltr.Size = New System.Drawing.Size(146, 20)
        Me.txtQrPzFiltr.TabIndex = 11
        '
        'txtAwizoFiltr
        '
        Me.txtAwizoFiltr.Location = New System.Drawing.Point(6, 32)
        Me.txtAwizoFiltr.Name = "txtAwizoFiltr"
        Me.txtAwizoFiltr.Size = New System.Drawing.Size(146, 20)
        Me.txtAwizoFiltr.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Nr awiza:"
        '
        'listStatus
        '
        Me.listStatus.FormattingEnabled = True
        Me.listStatus.Location = New System.Drawing.Point(6, 120)
        Me.listStatus.Name = "listStatus"
        Me.listStatus.Size = New System.Drawing.Size(146, 94)
        Me.listStatus.TabIndex = 5
        '
        'btnGeneruj
        '
        Me.btnGeneruj.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnGeneruj.ForeColor = System.Drawing.Color.White
        Me.btnGeneruj.Location = New System.Drawing.Point(6, 412)
        Me.btnGeneruj.Name = "btnGeneruj"
        Me.btnGeneruj.Size = New System.Drawing.Size(146, 23)
        Me.btnGeneruj.TabIndex = 17
        Me.btnGeneruj.Text = "Generuj"
        Me.btnGeneruj.UseVisualStyleBackColor = False
        '
        'dgv
        '
        Me.dgv.AllowUserToAddRows = False
        Me.dgv.AllowUserToDeleteRows = False
        Me.dgv.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgv.BackgroundColor = System.Drawing.Color.White
        Me.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv.Location = New System.Drawing.Point(176, 12)
        Me.dgv.Name = "dgv"
        Me.dgv.ReadOnly = True
        Me.dgv.RowHeadersVisible = False
        Me.dgv.Size = New System.Drawing.Size(761, 441)
        Me.dgv.TabIndex = 1
        '
        'btnZamknij
        '
        Me.btnZamknij.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnZamknij.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnZamknij.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnZamknij.ForeColor = System.Drawing.Color.White
        Me.btnZamknij.Location = New System.Drawing.Point(862, 459)
        Me.btnZamknij.Name = "btnZamknij"
        Me.btnZamknij.Size = New System.Drawing.Size(75, 23)
        Me.btnZamknij.TabIndex = 3
        Me.btnZamknij.Text = "Zamknij"
        Me.btnZamknij.UseVisualStyleBackColor = False
        '
        'btnEksportExcel
        '
        Me.btnEksportExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnEksportExcel.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnEksportExcel.ForeColor = System.Drawing.Color.White
        Me.btnEksportExcel.Image = CType(resources.GetObject("btnEksportExcel.Image"), System.Drawing.Image)
        Me.btnEksportExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEksportExcel.Location = New System.Drawing.Point(732, 459)
        Me.btnEksportExcel.Name = "btnEksportExcel"
        Me.btnEksportExcel.Size = New System.Drawing.Size(124, 23)
        Me.btnEksportExcel.TabIndex = 2
        Me.btnEksportExcel.Text = "Export do Excela"
        Me.btnEksportExcel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnEksportExcel.UseVisualStyleBackColor = False
        '
        'frmRaportAwizaDostawa
        '
        Me.AcceptButton = Me.btnGeneruj
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnZamknij
        Me.ClientSize = New System.Drawing.Size(949, 496)
        Me.Controls.Add(Me.btnEksportExcel)
        Me.Controls.Add(Me.btnZamknij)
        Me.Controls.Add(Me.dgv)
        Me.Controls.Add(Me.gbFiltry)
        Me.MinimumSize = New System.Drawing.Size(965, 534)
        Me.Name = "frmRaportAwizaDostawa"
        Me.Text = "Raport awiz dostawa"
        Me.gbFiltry.ResumeLayout(False)
        Me.gbFiltry.PerformLayout()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gbFiltry As System.Windows.Forms.GroupBox
    Friend WithEvents dgv As System.Windows.Forms.DataGridView
    Friend WithEvents listStatus As System.Windows.Forms.CheckedListBox
    Friend WithEvents btnGeneruj As System.Windows.Forms.Button
    Friend WithEvents btnZamknij As System.Windows.Forms.Button
    Friend WithEvents dtpPlanowanaDataDo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpPlanowanaDataOd As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtQrZaFiltr As System.Windows.Forms.TextBox
    Friend WithEvents txtQrDostawaFiltr As System.Windows.Forms.TextBox
    Friend WithEvents txtQrPzFiltr As System.Windows.Forms.TextBox
    Friend WithEvents txtAwizoFiltr As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnEksportExcel As System.Windows.Forms.Button
    Friend WithEvents chbStatusy As System.Windows.Forms.CheckBox
    Friend WithEvents txtPOFiltr As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
