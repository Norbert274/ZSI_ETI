﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRaportPrzyjeciaPoDatachFiltr
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
        Me.Tooltip = New System.Windows.Forms.ToolTip(Me.components)
        Me.dbZakresDat.SuspendLayout()
        Me.SuspendLayout()
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
        Me.Tooltip.SetToolTip(Me.lblDataOd, "Zakres dat przyjęcia towaru na stan")
        '
        'dtpDataDo
        '
        Me.dtpDataDo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDataDo.Location = New System.Drawing.Point(60, 43)
        Me.dtpDataDo.Name = "dtpDataDo"
        Me.dtpDataDo.Size = New System.Drawing.Size(128, 20)
        Me.dtpDataDo.TabIndex = 3
        Me.Tooltip.SetToolTip(Me.dtpDataDo, "Zakres dat przyjęcia towaru na stan")
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
        Me.Tooltip.SetToolTip(Me.lblDataDo, "Zakres dat przyjęcia towaru na stan")
        '
        'dtpDataOd
        '
        Me.dtpDataOd.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDataOd.Location = New System.Drawing.Point(60, 15)
        Me.dtpDataOd.Name = "dtpDataOd"
        Me.dtpDataOd.Size = New System.Drawing.Size(128, 20)
        Me.dtpDataOd.TabIndex = 1
        Me.Tooltip.SetToolTip(Me.dtpDataOd, "Zakres dat przyjęcia towaru na stan")
        '
        'btnGenerujRaport
        '
        Me.btnGenerujRaport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGenerujRaport.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnGenerujRaport.ForeColor = System.Drawing.Color.White
        Me.btnGenerujRaport.Location = New System.Drawing.Point(21, 85)
        Me.btnGenerujRaport.Name = "btnGenerujRaport"
        Me.btnGenerujRaport.Size = New System.Drawing.Size(92, 23)
        Me.btnGenerujRaport.TabIndex = 4
        Me.btnGenerujRaport.Text = "Generuj raport"
        Me.Tooltip.SetToolTip(Me.btnGenerujRaport, "Generowanie raportu przyjęć wg zakresu dat przyjęcia towaru na stan")
        Me.btnGenerujRaport.UseVisualStyleBackColor = False
        '
        'dbZakresDat
        '
        Me.dbZakresDat.Controls.Add(Me.dtpDataOd)
        Me.dbZakresDat.Controls.Add(Me.lblDataOd)
        Me.dbZakresDat.Controls.Add(Me.lblDataDo)
        Me.dbZakresDat.Controls.Add(Me.dtpDataDo)
        Me.dbZakresDat.Location = New System.Drawing.Point(12, 7)
        Me.dbZakresDat.Name = "dbZakresDat"
        Me.dbZakresDat.Size = New System.Drawing.Size(194, 72)
        Me.dbZakresDat.TabIndex = 3
        Me.dbZakresDat.TabStop = False
        Me.dbZakresDat.Text = "Zakres dat przyjęcia towaru"
        Me.Tooltip.SetToolTip(Me.dbZakresDat, "Zakres dat przyjęcia towaru na stan")
        '
        'btnAnuluj
        '
        Me.btnAnuluj.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAnuluj.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnAnuluj.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnAnuluj.ForeColor = System.Drawing.Color.White
        Me.btnAnuluj.Location = New System.Drawing.Point(124, 85)
        Me.btnAnuluj.Name = "btnAnuluj"
        Me.btnAnuluj.Size = New System.Drawing.Size(76, 23)
        Me.btnAnuluj.TabIndex = 5
        Me.btnAnuluj.Text = "Anuluj"
        Me.btnAnuluj.UseVisualStyleBackColor = False
        '
        'Tooltip
        '
        Me.Tooltip.AutoPopDelay = 10000
        Me.Tooltip.InitialDelay = 500
        Me.Tooltip.ReshowDelay = 500
        '
        'frmRaportPrzyjeciaPoDatachFiltr
        '
        Me.AcceptButton = Me.btnGenerujRaport
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnAnuluj
        Me.ClientSize = New System.Drawing.Size(217, 115)
        Me.Controls.Add(Me.btnAnuluj)
        Me.Controls.Add(Me.dbZakresDat)
        Me.Controls.Add(Me.btnGenerujRaport)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(233, 153)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(233, 153)
        Me.Name = "frmRaportPrzyjeciaPoDatachFiltr"
        Me.Text = "Raport - Przyjęcia po datach"
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
    Friend WithEvents Tooltip As System.Windows.Forms.ToolTip
End Class
