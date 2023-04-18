<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDaneDpd
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
        Me.btnOk = New System.Windows.Forms.Button()
        Me.btnAnuluj = New System.Windows.Forms.Button()
        Me.chkPrzZwr = New System.Windows.Forms.CheckBox()
        Me.chkDokZw = New System.Windows.Forms.CheckBox()
        Me.chkOsPryw = New System.Windows.Forms.CheckBox()
        Me.lblWartosc = New System.Windows.Forms.Label()
        Me.lblCOD = New System.Windows.Forms.Label()
        Me.txtWartosc = New System.Windows.Forms.TextBox()
        Me.txtCOD = New System.Windows.Forms.TextBox()
        Me.cmbDorGwTyp = New System.Windows.Forms.ComboBox()
        Me.lblDorGwTyp = New System.Windows.Forms.Label()
        Me.ToolTip = New CursorProfClient.DisabledToolTip(Me.components)
        CType(Me.ToolTip, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnOk
        '
        Me.btnOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOk.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnOk.ForeColor = System.Drawing.Color.White
        Me.btnOk.Location = New System.Drawing.Point(25, 188)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(84, 23)
        Me.btnOk.TabIndex = 9
        Me.btnOk.Text = "Ok"
        Me.btnOk.UseVisualStyleBackColor = False
        '
        'btnAnuluj
        '
        Me.btnAnuluj.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAnuluj.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnAnuluj.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnAnuluj.ForeColor = System.Drawing.Color.White
        Me.btnAnuluj.Location = New System.Drawing.Point(115, 188)
        Me.btnAnuluj.Name = "btnAnuluj"
        Me.btnAnuluj.Size = New System.Drawing.Size(79, 23)
        Me.btnAnuluj.TabIndex = 10
        Me.btnAnuluj.Text = "Anuluj"
        Me.btnAnuluj.UseVisualStyleBackColor = False
        '
        'chkPrzZwr
        '
        Me.chkPrzZwr.AutoSize = True
        Me.chkPrzZwr.Location = New System.Drawing.Point(18, 37)
        Me.chkPrzZwr.Name = "chkPrzZwr"
        Me.chkPrzZwr.Size = New System.Drawing.Size(113, 17)
        Me.chkPrzZwr.TabIndex = 5
        Me.chkPrzZwr.Text = "Przesyłka zwrotna"
        Me.chkPrzZwr.UseVisualStyleBackColor = True
        '
        'chkDokZw
        '
        Me.chkDokZw.AutoSize = True
        Me.chkDokZw.Location = New System.Drawing.Point(18, 12)
        Me.chkDokZw.Name = "chkDokZw"
        Me.chkDokZw.Size = New System.Drawing.Size(120, 17)
        Me.chkDokZw.TabIndex = 0
        Me.chkDokZw.Text = "Dokumenty zwrotne"
        Me.chkDokZw.UseVisualStyleBackColor = True
        '
        'chkOsPryw
        '
        Me.chkOsPryw.AutoSize = True
        Me.chkOsPryw.Location = New System.Drawing.Point(18, 60)
        Me.chkOsPryw.Name = "chkOsPryw"
        Me.chkOsPryw.Size = New System.Drawing.Size(141, 17)
        Me.chkOsPryw.TabIndex = 8
        Me.chkOsPryw.Text = "Odbiera osoba prywatna"
        Me.chkOsPryw.UseVisualStyleBackColor = True
        '
        'lblWartosc
        '
        Me.lblWartosc.AutoSize = True
        Me.lblWartosc.Location = New System.Drawing.Point(17, 85)
        Me.lblWartosc.Name = "lblWartosc"
        Me.lblWartosc.Size = New System.Drawing.Size(50, 13)
        Me.lblWartosc.TabIndex = 1
        Me.lblWartosc.Text = "Wartość:"
        '
        'lblCOD
        '
        Me.lblCOD.AutoSize = True
        Me.lblCOD.Location = New System.Drawing.Point(17, 111)
        Me.lblCOD.Name = "lblCOD"
        Me.lblCOD.Size = New System.Drawing.Size(66, 13)
        Me.lblCOD.TabIndex = 6
        Me.lblCOD.Text = "Kwota COD:"
        '
        'txtWartosc
        '
        Me.txtWartosc.Location = New System.Drawing.Point(89, 82)
        Me.txtWartosc.Name = "txtWartosc"
        Me.txtWartosc.Size = New System.Drawing.Size(105, 20)
        Me.txtWartosc.TabIndex = 2
        Me.txtWartosc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtCOD
        '
        Me.txtCOD.Location = New System.Drawing.Point(89, 108)
        Me.txtCOD.Name = "txtCOD"
        Me.txtCOD.Size = New System.Drawing.Size(105, 20)
        Me.txtCOD.TabIndex = 7
        Me.txtCOD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cmbDorGwTyp
        '
        Me.cmbDorGwTyp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDorGwTyp.FormattingEnabled = True
        Me.cmbDorGwTyp.Location = New System.Drawing.Point(18, 157)
        Me.cmbDorGwTyp.Name = "cmbDorGwTyp"
        Me.cmbDorGwTyp.Size = New System.Drawing.Size(176, 21)
        Me.cmbDorGwTyp.TabIndex = 4
        '
        'lblDorGwTyp
        '
        Me.lblDorGwTyp.AutoSize = True
        Me.lblDorGwTyp.Location = New System.Drawing.Point(17, 141)
        Me.lblDorGwTyp.Name = "lblDorGwTyp"
        Me.lblDorGwTyp.Size = New System.Drawing.Size(137, 13)
        Me.lblDorGwTyp.TabIndex = 3
        Me.lblDorGwTyp.Text = "Doręczenie gwarantowane:"
        '
        'ToolTip
        '
        Me.ToolTip.AutomaticDelay = 100
        '
        'frmDaneDpd
        '
        Me.AcceptButton = Me.btnOk
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnAnuluj
        Me.ClientSize = New System.Drawing.Size(206, 220)
        Me.Controls.Add(Me.lblDorGwTyp)
        Me.Controls.Add(Me.chkPrzZwr)
        Me.Controls.Add(Me.cmbDorGwTyp)
        Me.Controls.Add(Me.chkDokZw)
        Me.Controls.Add(Me.txtCOD)
        Me.Controls.Add(Me.txtWartosc)
        Me.Controls.Add(Me.lblCOD)
        Me.Controls.Add(Me.lblWartosc)
        Me.Controls.Add(Me.chkOsPryw)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.btnAnuluj)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximumSize = New System.Drawing.Size(212, 248)
        Me.MinimumSize = New System.Drawing.Size(212, 248)
        Me.Name = "frmDaneDpd"
        Me.Text = "Dane przesyłki"
        CType(Me.ToolTip, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents btnAnuluj As System.Windows.Forms.Button
    Friend WithEvents chkPrzZwr As System.Windows.Forms.CheckBox
    Friend WithEvents chkDokZw As System.Windows.Forms.CheckBox
    Friend WithEvents chkOsPryw As System.Windows.Forms.CheckBox
    Friend WithEvents lblWartosc As System.Windows.Forms.Label
    Friend WithEvents lblCOD As System.Windows.Forms.Label
    Friend WithEvents txtWartosc As System.Windows.Forms.TextBox
    Friend WithEvents txtCOD As System.Windows.Forms.TextBox
    Friend WithEvents cmbDorGwTyp As System.Windows.Forms.ComboBox
    Friend WithEvents lblDorGwTyp As System.Windows.Forms.Label
    Friend WithEvents ToolTip As DisabledToolTip
End Class
