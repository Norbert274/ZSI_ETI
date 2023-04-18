<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRaportCursorAwiza
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRaportCursorAwiza))
        Me.gbFiltry = New System.Windows.Forms.GroupBox()
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
        Me.gbFiltry.Size = New System.Drawing.Size(158, 395)
        Me.gbFiltry.TabIndex = 0
        Me.gbFiltry.TabStop = False
        Me.gbFiltry.Text = "Warunki wyszukiwania"
        '
        'chbStatusy
        '
        Me.chbStatusy.AutoSize = True
        Me.chbStatusy.Location = New System.Drawing.Point(6, 58)
        Me.chbStatusy.Name = "chbStatusy"
        Me.chbStatusy.Size = New System.Drawing.Size(61, 17)
        Me.chbStatusy.TabIndex = 28
        Me.chbStatusy.Text = "Statusy"
        Me.chbStatusy.UseVisualStyleBackColor = True
        '
        'dtpPlanowanaDataDo
        '
        Me.dtpPlanowanaDataDo.CustomFormat = "dd-MM-yyyy"
        Me.dtpPlanowanaDataDo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpPlanowanaDataDo.Location = New System.Drawing.Point(46, 342)
        Me.dtpPlanowanaDataDo.Name = "dtpPlanowanaDataDo"
        Me.dtpPlanowanaDataDo.Size = New System.Drawing.Size(106, 20)
        Me.dtpPlanowanaDataDo.TabIndex = 27
        '
        'dtpPlanowanaDataOd
        '
        Me.dtpPlanowanaDataOd.CustomFormat = "dd-MM-yyyy"
        Me.dtpPlanowanaDataOd.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpPlanowanaDataOd.Location = New System.Drawing.Point(46, 316)
        Me.dtpPlanowanaDataOd.Name = "dtpPlanowanaDataOd"
        Me.dtpPlanowanaDataOd.Size = New System.Drawing.Size(106, 20)
        Me.dtpPlanowanaDataOd.TabIndex = 26
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(6, 300)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(129, 13)
        Me.Label11.TabIndex = 25
        Me.Label11.Text = "Planowana data dostawy:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(6, 348)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(24, 13)
        Me.Label10.TabIndex = 24
        Me.Label10.Text = "Do:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(6, 322)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(24, 13)
        Me.Label9.TabIndex = 23
        Me.Label9.Text = "Od:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 183)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(69, 13)
        Me.Label5.TabIndex = 17
        Me.Label5.Text = "OGUAR_ZA:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(3, 222)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(110, 13)
        Me.Label4.TabIndex = 16
        Me.Label4.Text = "OGUAR_DOSTAWA:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 261)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(69, 13)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "OGUAR_PZ:"
        '
        'txtQrZaFiltr
        '
        Me.txtQrZaFiltr.Location = New System.Drawing.Point(6, 199)
        Me.txtQrZaFiltr.Name = "txtQrZaFiltr"
        Me.txtQrZaFiltr.Size = New System.Drawing.Size(146, 20)
        Me.txtQrZaFiltr.TabIndex = 14
        '
        'txtQrDostawaFiltr
        '
        Me.txtQrDostawaFiltr.Location = New System.Drawing.Point(6, 238)
        Me.txtQrDostawaFiltr.Name = "txtQrDostawaFiltr"
        Me.txtQrDostawaFiltr.Size = New System.Drawing.Size(146, 20)
        Me.txtQrDostawaFiltr.TabIndex = 13
        '
        'txtQrPzFiltr
        '
        Me.txtQrPzFiltr.Location = New System.Drawing.Point(6, 277)
        Me.txtQrPzFiltr.Name = "txtQrPzFiltr"
        Me.txtQrPzFiltr.Size = New System.Drawing.Size(146, 20)
        Me.txtQrPzFiltr.TabIndex = 12
        '
        'txtAwizoFiltr
        '
        Me.txtAwizoFiltr.Location = New System.Drawing.Point(6, 32)
        Me.txtAwizoFiltr.Name = "txtAwizoFiltr"
        Me.txtAwizoFiltr.Size = New System.Drawing.Size(146, 20)
        Me.txtAwizoFiltr.TabIndex = 11
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 13)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Awizo id:"
        '
        'listStatus
        '
        Me.listStatus.FormattingEnabled = True
        Me.listStatus.Location = New System.Drawing.Point(6, 81)
        Me.listStatus.Name = "listStatus"
        Me.listStatus.Size = New System.Drawing.Size(146, 94)
        Me.listStatus.TabIndex = 6
        '
        'btnGeneruj
        '
        Me.btnGeneruj.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnGeneruj.ForeColor = System.Drawing.Color.White
        Me.btnGeneruj.Location = New System.Drawing.Point(6, 368)
        Me.btnGeneruj.Name = "btnGeneruj"
        Me.btnGeneruj.Size = New System.Drawing.Size(146, 23)
        Me.btnGeneruj.TabIndex = 4
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
        Me.dgv.BackgroundColor = System.Drawing.Color.White
        Me.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv.Location = New System.Drawing.Point(176, 12)
        Me.dgv.Name = "dgv"
        Me.dgv.ReadOnly = True
        Me.dgv.RowHeadersVisible = False
        Me.dgv.Size = New System.Drawing.Size(761, 395)
        Me.dgv.TabIndex = 1
        '
        'btnZamknij
        '
        Me.btnZamknij.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnZamknij.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnZamknij.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnZamknij.ForeColor = System.Drawing.Color.White
        Me.btnZamknij.Location = New System.Drawing.Point(862, 413)
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
        Me.btnEksportExcel.Location = New System.Drawing.Point(732, 413)
        Me.btnEksportExcel.Name = "btnEksportExcel"
        Me.btnEksportExcel.Size = New System.Drawing.Size(124, 23)
        Me.btnEksportExcel.TabIndex = 4
        Me.btnEksportExcel.Text = "Export do Excela"
        Me.btnEksportExcel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnEksportExcel.UseVisualStyleBackColor = False
        '
        'frmRaportCursorAwiza
        '
        Me.AcceptButton = Me.btnGeneruj
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnZamknij
        Me.ClientSize = New System.Drawing.Size(949, 450)
        Me.Controls.Add(Me.btnEksportExcel)
        Me.Controls.Add(Me.btnZamknij)
        Me.Controls.Add(Me.dgv)
        Me.Controls.Add(Me.gbFiltry)
        Me.Name = "frmRaportCursorAwiza"
        Me.Text = "Raport awiz - cursor"
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
End Class
