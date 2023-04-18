<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDaneDpdUprawnienia
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
        Me.gbZwrot = New System.Windows.Forms.GroupBox()
        Me.chkPrzZwrotnaEnabled = New System.Windows.Forms.CheckBox()
        Me.chkPrzZwrotnaVisable = New System.Windows.Forms.CheckBox()
        Me.lblDokZwrotne = New System.Windows.Forms.Label()
        Me.lblPrzZwrotna = New System.Windows.Forms.Label()
        Me.chkDokZwrotneVisible = New System.Windows.Forms.CheckBox()
        Me.chkDokZwrotneEnabled = New System.Windows.Forms.CheckBox()
        Me.btnZapisz = New System.Windows.Forms.Button()
        Me.btnAnuluj = New System.Windows.Forms.Button()
        Me.chkOsPrywEnabled = New System.Windows.Forms.CheckBox()
        Me.chkOsPrywVisable = New System.Windows.Forms.CheckBox()
        Me.lblOsPryw = New System.Windows.Forms.Label()
        Me.chkWartoscEnabled = New System.Windows.Forms.CheckBox()
        Me.chkWartoscVisable = New System.Windows.Forms.CheckBox()
        Me.lblWartosc = New System.Windows.Forms.Label()
        Me.chkCODEnabled = New System.Windows.Forms.CheckBox()
        Me.chkCODVisable = New System.Windows.Forms.CheckBox()
        Me.lblKwotaCod = New System.Windows.Forms.Label()
        Me.chkDorGwEnabled = New System.Windows.Forms.CheckBox()
        Me.chkDorGwVisable = New System.Windows.Forms.CheckBox()
        Me.LblDorGw = New System.Windows.Forms.Label()
        Me.gbZwrot.SuspendLayout()
        Me.SuspendLayout()
        '
        'gbZwrot
        '
        Me.gbZwrot.Controls.Add(Me.chkPrzZwrotnaEnabled)
        Me.gbZwrot.Controls.Add(Me.chkPrzZwrotnaVisable)
        Me.gbZwrot.Controls.Add(Me.lblDokZwrotne)
        Me.gbZwrot.Controls.Add(Me.lblPrzZwrotna)
        Me.gbZwrot.Controls.Add(Me.chkDokZwrotneVisible)
        Me.gbZwrot.Controls.Add(Me.chkDokZwrotneEnabled)
        Me.gbZwrot.Location = New System.Drawing.Point(5, 4)
        Me.gbZwrot.Name = "gbZwrot"
        Me.gbZwrot.Size = New System.Drawing.Size(174, 106)
        Me.gbZwrot.TabIndex = 0
        Me.gbZwrot.TabStop = False
        '
        'chkPrzZwrotnaEnabled
        '
        Me.chkPrzZwrotnaEnabled.AutoSize = True
        Me.chkPrzZwrotnaEnabled.Location = New System.Drawing.Point(87, 73)
        Me.chkPrzZwrotnaEnabled.Name = "chkPrzZwrotnaEnabled"
        Me.chkPrzZwrotnaEnabled.Size = New System.Drawing.Size(76, 17)
        Me.chkPrzZwrotnaEnabled.TabIndex = 5
        Me.chkPrzZwrotnaEnabled.Text = "Włączone"
        Me.chkPrzZwrotnaEnabled.UseVisualStyleBackColor = True
        '
        'chkPrzZwrotnaVisable
        '
        Me.chkPrzZwrotnaVisable.AutoSize = True
        Me.chkPrzZwrotnaVisable.Location = New System.Drawing.Point(7, 73)
        Me.chkPrzZwrotnaVisable.Name = "chkPrzZwrotnaVisable"
        Me.chkPrzZwrotnaVisable.Size = New System.Drawing.Size(74, 17)
        Me.chkPrzZwrotnaVisable.TabIndex = 4
        Me.chkPrzZwrotnaVisable.Text = "Widoczne"
        Me.chkPrzZwrotnaVisable.UseVisualStyleBackColor = True
        '
        'lblDokZwrotne
        '
        Me.lblDokZwrotne.AutoSize = True
        Me.lblDokZwrotne.Location = New System.Drawing.Point(4, 14)
        Me.lblDokZwrotne.Name = "lblDokZwrotne"
        Me.lblDokZwrotne.Size = New System.Drawing.Size(101, 13)
        Me.lblDokZwrotne.TabIndex = 0
        Me.lblDokZwrotne.Text = "Dokumenty zwrotne"
        '
        'lblPrzZwrotna
        '
        Me.lblPrzZwrotna.AutoSize = True
        Me.lblPrzZwrotna.Location = New System.Drawing.Point(4, 56)
        Me.lblPrzZwrotna.Name = "lblPrzZwrotna"
        Me.lblPrzZwrotna.Size = New System.Drawing.Size(94, 13)
        Me.lblPrzZwrotna.TabIndex = 3
        Me.lblPrzZwrotna.Text = "Przesyłka zwrotna"
        '
        'chkDokZwrotneVisible
        '
        Me.chkDokZwrotneVisible.AutoSize = True
        Me.chkDokZwrotneVisible.Location = New System.Drawing.Point(7, 31)
        Me.chkDokZwrotneVisible.Name = "chkDokZwrotneVisible"
        Me.chkDokZwrotneVisible.Size = New System.Drawing.Size(74, 17)
        Me.chkDokZwrotneVisible.TabIndex = 1
        Me.chkDokZwrotneVisible.Text = "Widoczne"
        Me.chkDokZwrotneVisible.UseVisualStyleBackColor = True
        '
        'chkDokZwrotneEnabled
        '
        Me.chkDokZwrotneEnabled.AutoSize = True
        Me.chkDokZwrotneEnabled.Location = New System.Drawing.Point(87, 31)
        Me.chkDokZwrotneEnabled.Name = "chkDokZwrotneEnabled"
        Me.chkDokZwrotneEnabled.Size = New System.Drawing.Size(76, 17)
        Me.chkDokZwrotneEnabled.TabIndex = 2
        Me.chkDokZwrotneEnabled.Text = "Włączone"
        Me.chkDokZwrotneEnabled.UseVisualStyleBackColor = True
        '
        'btnZapisz
        '
        Me.btnZapisz.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnZapisz.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnZapisz.ForeColor = System.Drawing.Color.White
        Me.btnZapisz.Location = New System.Drawing.Point(225, 156)
        Me.btnZapisz.Name = "btnZapisz"
        Me.btnZapisz.Size = New System.Drawing.Size(84, 23)
        Me.btnZapisz.TabIndex = 13
        Me.btnZapisz.Text = "Zapisz"
        Me.btnZapisz.UseVisualStyleBackColor = False
        '
        'btnAnuluj
        '
        Me.btnAnuluj.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAnuluj.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnAnuluj.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnAnuluj.ForeColor = System.Drawing.Color.White
        Me.btnAnuluj.Location = New System.Drawing.Point(312, 156)
        Me.btnAnuluj.Name = "btnAnuluj"
        Me.btnAnuluj.Size = New System.Drawing.Size(79, 23)
        Me.btnAnuluj.TabIndex = 14
        Me.btnAnuluj.Text = "Anuluj"
        Me.btnAnuluj.UseVisualStyleBackColor = False
        '
        'chkOsPrywEnabled
        '
        Me.chkOsPrywEnabled.AutoSize = True
        Me.chkOsPrywEnabled.Location = New System.Drawing.Point(92, 130)
        Me.chkOsPrywEnabled.Name = "chkOsPrywEnabled"
        Me.chkOsPrywEnabled.Size = New System.Drawing.Size(76, 17)
        Me.chkOsPrywEnabled.TabIndex = 12
        Me.chkOsPrywEnabled.Text = "Włączone"
        Me.chkOsPrywEnabled.UseVisualStyleBackColor = True
        '
        'chkOsPrywVisable
        '
        Me.chkOsPrywVisable.AutoSize = True
        Me.chkOsPrywVisable.Location = New System.Drawing.Point(12, 130)
        Me.chkOsPrywVisable.Name = "chkOsPrywVisable"
        Me.chkOsPrywVisable.Size = New System.Drawing.Size(74, 17)
        Me.chkOsPrywVisable.TabIndex = 11
        Me.chkOsPrywVisable.Text = "Widoczne"
        Me.chkOsPrywVisable.UseVisualStyleBackColor = True
        '
        'lblOsPryw
        '
        Me.lblOsPryw.AutoSize = True
        Me.lblOsPryw.Location = New System.Drawing.Point(9, 113)
        Me.lblOsPryw.Name = "lblOsPryw"
        Me.lblOsPryw.Size = New System.Drawing.Size(122, 13)
        Me.lblOsPryw.TabIndex = 10
        Me.lblOsPryw.Text = "Odbiera osoba prywatna"
        '
        'chkWartoscEnabled
        '
        Me.chkWartoscEnabled.AutoSize = True
        Me.chkWartoscEnabled.Location = New System.Drawing.Point(280, 35)
        Me.chkWartoscEnabled.Name = "chkWartoscEnabled"
        Me.chkWartoscEnabled.Size = New System.Drawing.Size(76, 17)
        Me.chkWartoscEnabled.TabIndex = 3
        Me.chkWartoscEnabled.Text = "Włączone"
        Me.chkWartoscEnabled.UseVisualStyleBackColor = True
        '
        'chkWartoscVisable
        '
        Me.chkWartoscVisable.AutoSize = True
        Me.chkWartoscVisable.Location = New System.Drawing.Point(200, 35)
        Me.chkWartoscVisable.Name = "chkWartoscVisable"
        Me.chkWartoscVisable.Size = New System.Drawing.Size(74, 17)
        Me.chkWartoscVisable.TabIndex = 2
        Me.chkWartoscVisable.Text = "Widoczne"
        Me.chkWartoscVisable.UseVisualStyleBackColor = True
        '
        'lblWartosc
        '
        Me.lblWartosc.AutoSize = True
        Me.lblWartosc.Location = New System.Drawing.Point(197, 18)
        Me.lblWartosc.Name = "lblWartosc"
        Me.lblWartosc.Size = New System.Drawing.Size(169, 13)
        Me.lblWartosc.TabIndex = 1
        Me.lblWartosc.Text = "Wartość (ubezpieczenie przesyłki)"
        '
        'chkCODEnabled
        '
        Me.chkCODEnabled.AutoSize = True
        Me.chkCODEnabled.Location = New System.Drawing.Point(280, 77)
        Me.chkCODEnabled.Name = "chkCODEnabled"
        Me.chkCODEnabled.Size = New System.Drawing.Size(76, 17)
        Me.chkCODEnabled.TabIndex = 9
        Me.chkCODEnabled.Text = "Włączone"
        Me.chkCODEnabled.UseVisualStyleBackColor = True
        '
        'chkCODVisable
        '
        Me.chkCODVisable.AutoSize = True
        Me.chkCODVisable.Location = New System.Drawing.Point(200, 77)
        Me.chkCODVisable.Name = "chkCODVisable"
        Me.chkCODVisable.Size = New System.Drawing.Size(74, 17)
        Me.chkCODVisable.TabIndex = 8
        Me.chkCODVisable.Text = "Widoczne"
        Me.chkCODVisable.UseVisualStyleBackColor = True
        '
        'lblKwotaCod
        '
        Me.lblKwotaCod.AutoSize = True
        Me.lblKwotaCod.Location = New System.Drawing.Point(197, 60)
        Me.lblKwotaCod.Name = "lblKwotaCod"
        Me.lblKwotaCod.Size = New System.Drawing.Size(190, 13)
        Me.lblKwotaCod.TabIndex = 7
        Me.lblKwotaCod.Text = "Kwota COD (pobranie przy doręczeniu)"
        '
        'chkDorGwEnabled
        '
        Me.chkDorGwEnabled.AutoSize = True
        Me.chkDorGwEnabled.Location = New System.Drawing.Point(280, 130)
        Me.chkDorGwEnabled.Name = "chkDorGwEnabled"
        Me.chkDorGwEnabled.Size = New System.Drawing.Size(76, 17)
        Me.chkDorGwEnabled.TabIndex = 6
        Me.chkDorGwEnabled.Text = "Włączone"
        Me.chkDorGwEnabled.UseVisualStyleBackColor = True
        '
        'chkDorGwVisable
        '
        Me.chkDorGwVisable.AutoSize = True
        Me.chkDorGwVisable.Location = New System.Drawing.Point(200, 130)
        Me.chkDorGwVisable.Name = "chkDorGwVisable"
        Me.chkDorGwVisable.Size = New System.Drawing.Size(74, 17)
        Me.chkDorGwVisable.TabIndex = 5
        Me.chkDorGwVisable.Text = "Widoczne"
        Me.chkDorGwVisable.UseVisualStyleBackColor = True
        '
        'LblDorGw
        '
        Me.LblDorGw.AutoSize = True
        Me.LblDorGw.Location = New System.Drawing.Point(197, 113)
        Me.LblDorGw.Name = "LblDorGw"
        Me.LblDorGw.Size = New System.Drawing.Size(137, 13)
        Me.LblDorGw.TabIndex = 4
        Me.LblDorGw.Text = "Doręczenie gwarantowane."
        '
        'frmDaneDpdUprawnienia
        '
        Me.AcceptButton = Me.btnZapisz
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnAnuluj
        Me.ClientSize = New System.Drawing.Size(400, 185)
        Me.Controls.Add(Me.chkDorGwEnabled)
        Me.Controls.Add(Me.chkDorGwVisable)
        Me.Controls.Add(Me.LblDorGw)
        Me.Controls.Add(Me.chkCODEnabled)
        Me.Controls.Add(Me.chkCODVisable)
        Me.Controls.Add(Me.lblKwotaCod)
        Me.Controls.Add(Me.chkWartoscEnabled)
        Me.Controls.Add(Me.chkWartoscVisable)
        Me.Controls.Add(Me.lblWartosc)
        Me.Controls.Add(Me.chkOsPrywEnabled)
        Me.Controls.Add(Me.chkOsPrywVisable)
        Me.Controls.Add(Me.lblOsPryw)
        Me.Controls.Add(Me.gbZwrot)
        Me.Controls.Add(Me.btnZapisz)
        Me.Controls.Add(Me.btnAnuluj)
        Me.MaximumSize = New System.Drawing.Size(416, 223)
        Me.MinimumSize = New System.Drawing.Size(416, 223)
        Me.Name = "frmDaneDpdUprawnienia"
        Me.Text = "Dane przesyłki uprawnienia"
        Me.gbZwrot.ResumeLayout(False)
        Me.gbZwrot.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents gbZwrot As System.Windows.Forms.GroupBox
    Friend WithEvents chkPrzZwrotnaEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents chkPrzZwrotnaVisable As System.Windows.Forms.CheckBox
    Friend WithEvents lblDokZwrotne As System.Windows.Forms.Label
    Friend WithEvents lblPrzZwrotna As System.Windows.Forms.Label
    Friend WithEvents chkDokZwrotneVisible As System.Windows.Forms.CheckBox
    Friend WithEvents chkDokZwrotneEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents btnZapisz As System.Windows.Forms.Button
    Friend WithEvents btnAnuluj As System.Windows.Forms.Button
    Friend WithEvents chkOsPrywEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents chkOsPrywVisable As System.Windows.Forms.CheckBox
    Friend WithEvents lblOsPryw As System.Windows.Forms.Label
    Friend WithEvents chkWartoscEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents chkWartoscVisable As System.Windows.Forms.CheckBox
    Friend WithEvents lblWartosc As System.Windows.Forms.Label
    Friend WithEvents chkCODEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents chkCODVisable As System.Windows.Forms.CheckBox
    Friend WithEvents lblKwotaCod As System.Windows.Forms.Label
    Friend WithEvents chkDorGwEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents chkDorGwVisable As System.Windows.Forms.CheckBox
    Friend WithEvents LblDorGw As System.Windows.Forms.Label
End Class
