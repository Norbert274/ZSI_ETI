<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRaportWejsciaWyjscia
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRaportWejsciaWyjscia))
        Me.lblDataDo = New System.Windows.Forms.Label()
        Me.dbZakresDat = New System.Windows.Forms.GroupBox()
        Me.dtpDataDo = New System.Windows.Forms.DateTimePicker()
        Me.lblDataOd = New System.Windows.Forms.Label()
        Me.dtpDataOd = New System.Windows.Forms.DateTimePicker()
        Me.btnGenerujRaport = New System.Windows.Forms.Button()
        Me.btnAnuluj = New System.Windows.Forms.Button()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.dbZakresDat.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblDataDo
        '
        Me.lblDataDo.AutoSize = True
        Me.lblDataDo.ForeColor = System.Drawing.Color.Black
        Me.lblDataDo.Location = New System.Drawing.Point(6, 43)
        Me.lblDataDo.Name = "lblDataDo"
        Me.lblDataDo.Size = New System.Drawing.Size(48, 13)
        Me.lblDataDo.TabIndex = 2
        Me.lblDataDo.Text = "Data do:"
        Me.ToolTip.SetToolTip(Me.lblDataDo, "Zakres dat wejœcia lub wyjœcia towaru z magazynu")
        '
        'dbZakresDat
        '
        Me.dbZakresDat.Controls.Add(Me.dtpDataDo)
        Me.dbZakresDat.Controls.Add(Me.lblDataDo)
        Me.dbZakresDat.Controls.Add(Me.lblDataOd)
        Me.dbZakresDat.Controls.Add(Me.dtpDataOd)
        Me.dbZakresDat.Location = New System.Drawing.Point(12, 7)
        Me.dbZakresDat.Name = "dbZakresDat"
        Me.dbZakresDat.Size = New System.Drawing.Size(194, 72)
        Me.dbZakresDat.TabIndex = 3
        Me.dbZakresDat.TabStop = False
        Me.dbZakresDat.Text = "Zakres dat"
        Me.ToolTip.SetToolTip(Me.dbZakresDat, "Zakres dat wejœcia lub wyjœcia towaru z magazynu")
        '
        'dtpDataDo
        '
        Me.dtpDataDo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDataDo.Location = New System.Drawing.Point(60, 43)
        Me.dtpDataDo.Name = "dtpDataDo"
        Me.dtpDataDo.Size = New System.Drawing.Size(128, 20)
        Me.dtpDataDo.TabIndex = 3
        Me.ToolTip.SetToolTip(Me.dtpDataDo, "Zakres dat wejœcia lub wyjœcia towaru z magazynu")
        '
        'lblDataOd
        '
        Me.lblDataOd.AutoSize = True
        Me.lblDataOd.ForeColor = System.Drawing.Color.Black
        Me.lblDataOd.Location = New System.Drawing.Point(6, 21)
        Me.lblDataOd.Name = "lblDataOd"
        Me.lblDataOd.Size = New System.Drawing.Size(48, 13)
        Me.lblDataOd.TabIndex = 0
        Me.lblDataOd.Text = "Data od:"
        Me.ToolTip.SetToolTip(Me.lblDataOd, "Zakres dat wejœcia lub wyjœcia towaru z magazynu")
        '
        'dtpDataOd
        '
        Me.dtpDataOd.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDataOd.Location = New System.Drawing.Point(60, 15)
        Me.dtpDataOd.Name = "dtpDataOd"
        Me.dtpDataOd.Size = New System.Drawing.Size(128, 20)
        Me.dtpDataOd.TabIndex = 1
        Me.ToolTip.SetToolTip(Me.dtpDataOd, "Zakres dat wejœcia lub wyjœcia towaru z magazynu")
        '
        'btnGenerujRaport
        '
        Me.btnGenerujRaport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGenerujRaport.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnGenerujRaport.ForeColor = System.Drawing.Color.White
        Me.btnGenerujRaport.Image = CType(resources.GetObject("btnGenerujRaport.Image"), System.Drawing.Image)
        Me.btnGenerujRaport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnGenerujRaport.Location = New System.Drawing.Point(12, 85)
        Me.btnGenerujRaport.Name = "btnGenerujRaport"
        Me.btnGenerujRaport.Size = New System.Drawing.Size(112, 27)
        Me.btnGenerujRaport.TabIndex = 4
        Me.btnGenerujRaport.Text = "Generuj raport ..."
        Me.btnGenerujRaport.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip.SetToolTip(Me.btnGenerujRaport, "Generowanie raportu wejœæ-wyjœæ wg zakresu dat wejœcia lub wyjœcia towaru z magaz" & _
        "ynu")
        Me.btnGenerujRaport.UseVisualStyleBackColor = False
        '
        'btnAnuluj
        '
        Me.btnAnuluj.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAnuluj.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnAnuluj.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnAnuluj.ForeColor = System.Drawing.Color.White
        Me.btnAnuluj.Location = New System.Drawing.Point(130, 85)
        Me.btnAnuluj.Name = "btnAnuluj"
        Me.btnAnuluj.Size = New System.Drawing.Size(76, 27)
        Me.btnAnuluj.TabIndex = 5
        Me.btnAnuluj.Text = "Anuluj"
        Me.btnAnuluj.UseVisualStyleBackColor = False
        '
        'ToolTip
        '
        Me.ToolTip.AutoPopDelay = 10000
        Me.ToolTip.InitialDelay = 500
        Me.ToolTip.ReshowDelay = 500
        '
        'frmRaportWejsciaWyjscia
        '
        Me.AcceptButton = Me.btnGenerujRaport
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnAnuluj
        Me.ClientSize = New System.Drawing.Size(225, 130)
        Me.Controls.Add(Me.btnAnuluj)
        Me.Controls.Add(Me.dbZakresDat)
        Me.Controls.Add(Me.btnGenerujRaport)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(233, 157)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(233, 157)
        Me.Name = "frmRaportWejsciaWyjscia"
        Me.Text = "Raport wejœæ-wyjœæ"
        Me.dbZakresDat.ResumeLayout(False)
        Me.dbZakresDat.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnGenerujRaport As System.Windows.Forms.Button
    Friend WithEvents lblDataDo As System.Windows.Forms.Label
    Friend WithEvents dbZakresDat As System.Windows.Forms.GroupBox
    Friend WithEvents dtpDataDo As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblDataOd As System.Windows.Forms.Label
    Friend WithEvents dtpDataOd As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnAnuluj As System.Windows.Forms.Button
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
End Class
