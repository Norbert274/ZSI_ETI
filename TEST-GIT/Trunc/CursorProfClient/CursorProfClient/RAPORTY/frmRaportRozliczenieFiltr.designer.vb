﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRaportRozliczenieFiltr
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
        Me.lblDataOd = New System.Windows.Forms.Label()
        Me.dtpDataDo = New System.Windows.Forms.DateTimePicker()
        Me.lblDataDo = New System.Windows.Forms.Label()
        Me.dtpDataOd = New System.Windows.Forms.DateTimePicker()
        Me.btnGenerujRaport = New System.Windows.Forms.Button()
        Me.dbZakresDat = New System.Windows.Forms.GroupBox()
        Me.btnAnuluj = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.dbZakresDat.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblDataOd
        '
        Me.lblDataOd.AutoSize = True
        Me.lblDataOd.ForeColor = System.Drawing.Color.Black
        Me.lblDataOd.Location = New System.Drawing.Point(42, 22)
        Me.lblDataOd.Name = "lblDataOd"
        Me.lblDataOd.Size = New System.Drawing.Size(48, 13)
        Me.lblDataOd.TabIndex = 0
        Me.lblDataOd.Text = "Data od:"
        Me.ToolTip1.SetToolTip(Me.lblDataOd, "Zakres dat wyjścia towaru z magazynu" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10))
        '
        'dtpDataDo
        '
        Me.dtpDataDo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDataDo.Location = New System.Drawing.Point(96, 45)
        Me.dtpDataDo.Name = "dtpDataDo"
        Me.dtpDataDo.Size = New System.Drawing.Size(98, 20)
        Me.dtpDataDo.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.dtpDataDo, "Zakres dat złożenia zamówienia")
        Me.dtpDataDo.Value = New Date(2011, 10, 27, 11, 49, 46, 0)
        '
        'lblDataDo
        '
        Me.lblDataDo.AutoSize = True
        Me.lblDataDo.ForeColor = System.Drawing.Color.Black
        Me.lblDataDo.Location = New System.Drawing.Point(42, 48)
        Me.lblDataDo.Name = "lblDataDo"
        Me.lblDataDo.Size = New System.Drawing.Size(48, 13)
        Me.lblDataDo.TabIndex = 2
        Me.lblDataDo.Text = "Data do:"
        Me.ToolTip1.SetToolTip(Me.lblDataDo, "Zakres dat wyjścia towaru z magazynu" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10))
        '
        'dtpDataOd
        '
        Me.dtpDataOd.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDataOd.Location = New System.Drawing.Point(96, 19)
        Me.dtpDataOd.Name = "dtpDataOd"
        Me.dtpDataOd.Size = New System.Drawing.Size(98, 20)
        Me.dtpDataOd.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.dtpDataOd, "Zakres dat złożenia zamówienia")
        '
        'btnGenerujRaport
        '
        Me.btnGenerujRaport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGenerujRaport.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnGenerujRaport.ForeColor = System.Drawing.Color.White
        Me.btnGenerujRaport.Location = New System.Drawing.Point(52, 90)
        Me.btnGenerujRaport.Name = "btnGenerujRaport"
        Me.btnGenerujRaport.Size = New System.Drawing.Size(99, 23)
        Me.btnGenerujRaport.TabIndex = 1
        Me.btnGenerujRaport.Text = "Generuj raport"
        Me.ToolTip1.SetToolTip(Me.btnGenerujRaport, "Generowanie raportu rozliczeń wg zakresu dat złożenia zamówienia")
        Me.btnGenerujRaport.UseVisualStyleBackColor = False
        '
        'dbZakresDat
        '
        Me.dbZakresDat.Controls.Add(Me.dtpDataOd)
        Me.dbZakresDat.Controls.Add(Me.lblDataOd)
        Me.dbZakresDat.Controls.Add(Me.lblDataDo)
        Me.dbZakresDat.Controls.Add(Me.dtpDataDo)
        Me.dbZakresDat.Location = New System.Drawing.Point(5, 5)
        Me.dbZakresDat.Name = "dbZakresDat"
        Me.dbZakresDat.Size = New System.Drawing.Size(229, 81)
        Me.dbZakresDat.TabIndex = 0
        Me.dbZakresDat.TabStop = False
        Me.dbZakresDat.Text = "Zakres dat"
        Me.ToolTip1.SetToolTip(Me.dbZakresDat, "Zakres dat wyjścia towaru z magazynu")
        '
        'btnAnuluj
        '
        Me.btnAnuluj.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAnuluj.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnAnuluj.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnAnuluj.ForeColor = System.Drawing.Color.White
        Me.btnAnuluj.Location = New System.Drawing.Point(157, 90)
        Me.btnAnuluj.Name = "btnAnuluj"
        Me.btnAnuluj.Size = New System.Drawing.Size(75, 23)
        Me.btnAnuluj.TabIndex = 11
        Me.btnAnuluj.Text = "Anuluj"
        Me.btnAnuluj.UseVisualStyleBackColor = False
        '
        'ToolTip1
        '
        Me.ToolTip1.AutoPopDelay = 10000
        Me.ToolTip1.InitialDelay = 500
        Me.ToolTip1.ReshowDelay = 500
        '
        'frmRaportRozliczenieFiltr
        '
        Me.AcceptButton = Me.btnGenerujRaport
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnAnuluj
        Me.ClientSize = New System.Drawing.Size(238, 119)
        Me.Controls.Add(Me.btnAnuluj)
        Me.Controls.Add(Me.dbZakresDat)
        Me.Controls.Add(Me.btnGenerujRaport)
        Me.MaximumSize = New System.Drawing.Size(254, 157)
        Me.MinimumSize = New System.Drawing.Size(254, 157)
        Me.Name = "frmRaportRozliczenieFiltr"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Raport rozliczenia"
        Me.dbZakresDat.ResumeLayout(False)
        Me.dbZakresDat.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblDataOd As System.Windows.Forms.Label
    Friend WithEvents dtpDataDo As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblDataDo As System.Windows.Forms.Label
    Friend WithEvents dtpDataOd As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnGenerujRaport As System.Windows.Forms.Button
    Friend WithEvents dbZakresDat As System.Windows.Forms.GroupBox
    Friend WithEvents btnAnuluj As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class
