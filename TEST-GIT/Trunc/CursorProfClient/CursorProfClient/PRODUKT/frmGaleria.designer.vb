<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGaleria
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
        Me.btnUsunZdjecie = New System.Windows.Forms.Button()
        Me.gbDodawanieZdjecia = New System.Windows.Forms.GroupBox()
        Me.btnZamknij = New System.Windows.Forms.Button()
        Me.btnDodajZdjecie = New System.Windows.Forms.Button()
        Me.picZdjeciePodglad = New System.Windows.Forms.PictureBox()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.txtNazwa = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtSciezka = New System.Windows.Forms.TextBox()
        Me.lblSciezka = New System.Windows.Forms.Label()
        Me.lblSKU = New System.Windows.Forms.Label()
        Me.ctrImgGaleria = New CursorProfClient.ctrImgGaleria()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.gbDodawanieZdjecia.SuspendLayout()
        CType(Me.picZdjeciePodglad, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnUsunZdjecie
        '
        Me.btnUsunZdjecie.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnUsunZdjecie.ForeColor = System.Drawing.Color.White
        Me.btnUsunZdjecie.Location = New System.Drawing.Point(9, 158)
        Me.btnUsunZdjecie.Name = "btnUsunZdjecie"
        Me.btnUsunZdjecie.Size = New System.Drawing.Size(88, 24)
        Me.btnUsunZdjecie.TabIndex = 6
        Me.btnUsunZdjecie.Text = "Usuń zdjęcie"
        Me.ToolTip.SetToolTip(Me.btnUsunZdjecie, "Usuwa zdjęcie z systemu")
        Me.btnUsunZdjecie.UseVisualStyleBackColor = False
        '
        'gbDodawanieZdjecia
        '
        Me.gbDodawanieZdjecia.Controls.Add(Me.btnZamknij)
        Me.gbDodawanieZdjecia.Controls.Add(Me.btnDodajZdjecie)
        Me.gbDodawanieZdjecia.Controls.Add(Me.picZdjeciePodglad)
        Me.gbDodawanieZdjecia.Controls.Add(Me.btnUsunZdjecie)
        Me.gbDodawanieZdjecia.Controls.Add(Me.btnBrowse)
        Me.gbDodawanieZdjecia.Controls.Add(Me.txtNazwa)
        Me.gbDodawanieZdjecia.Controls.Add(Me.Label1)
        Me.gbDodawanieZdjecia.Controls.Add(Me.txtSciezka)
        Me.gbDodawanieZdjecia.Controls.Add(Me.lblSciezka)
        Me.gbDodawanieZdjecia.Location = New System.Drawing.Point(6, 443)
        Me.gbDodawanieZdjecia.Name = "gbDodawanieZdjecia"
        Me.gbDodawanieZdjecia.Size = New System.Drawing.Size(480, 188)
        Me.gbDodawanieZdjecia.TabIndex = 2
        Me.gbDodawanieZdjecia.TabStop = False
        Me.gbDodawanieZdjecia.Text = "Dodawanie zdjęcia"
        '
        'btnZamknij
        '
        Me.btnZamknij.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnZamknij.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnZamknij.ForeColor = System.Drawing.Color.White
        Me.btnZamknij.Location = New System.Drawing.Point(367, 158)
        Me.btnZamknij.Name = "btnZamknij"
        Me.btnZamknij.Size = New System.Drawing.Size(107, 24)
        Me.btnZamknij.TabIndex = 7
        Me.btnZamknij.Text = "Zamknij"
        Me.btnZamknij.UseVisualStyleBackColor = False
        '
        'btnDodajZdjecie
        '
        Me.btnDodajZdjecie.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnDodajZdjecie.ForeColor = System.Drawing.Color.White
        Me.btnDodajZdjecie.Location = New System.Drawing.Point(9, 100)
        Me.btnDodajZdjecie.Name = "btnDodajZdjecie"
        Me.btnDodajZdjecie.Size = New System.Drawing.Size(88, 24)
        Me.btnDodajZdjecie.TabIndex = 5
        Me.btnDodajZdjecie.Text = "Dodaj zdjęcie"
        Me.ToolTip.SetToolTip(Me.btnDodajZdjecie, "Zapisuje zdjęcie w systemie")
        Me.btnDodajZdjecie.UseVisualStyleBackColor = False
        '
        'picZdjeciePodglad
        '
        Me.picZdjeciePodglad.Location = New System.Drawing.Point(253, 19)
        Me.picZdjeciePodglad.Name = "picZdjeciePodglad"
        Me.picZdjeciePodglad.Size = New System.Drawing.Size(221, 133)
        Me.picZdjeciePodglad.TabIndex = 5
        Me.picZdjeciePodglad.TabStop = False
        '
        'btnBrowse
        '
        Me.btnBrowse.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnBrowse.ForeColor = System.Drawing.Color.White
        Me.btnBrowse.Location = New System.Drawing.Point(217, 32)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(30, 23)
        Me.btnBrowse.TabIndex = 2
        Me.btnBrowse.Text = ". . ."
        Me.ToolTip.SetToolTip(Me.btnBrowse, "Ścieżka do pliku")
        Me.btnBrowse.UseVisualStyleBackColor = False
        '
        'txtNazwa
        '
        Me.txtNazwa.Location = New System.Drawing.Point(9, 74)
        Me.txtNazwa.Name = "txtNazwa"
        Me.txtNazwa.Size = New System.Drawing.Size(238, 20)
        Me.txtNazwa.TabIndex = 4
        Me.ToolTip.SetToolTip(Me.txtNazwa, "Nazwa pliku")
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 58)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(43, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Nazwa:"
        Me.ToolTip.SetToolTip(Me.Label1, "Nazwa pliku")
        '
        'txtSciezka
        '
        Me.txtSciezka.Location = New System.Drawing.Point(9, 35)
        Me.txtSciezka.Name = "txtSciezka"
        Me.txtSciezka.Size = New System.Drawing.Size(202, 20)
        Me.txtSciezka.TabIndex = 1
        Me.ToolTip.SetToolTip(Me.txtSciezka, "Ścieżka do pliku")
        '
        'lblSciezka
        '
        Me.lblSciezka.AutoSize = True
        Me.lblSciezka.Location = New System.Drawing.Point(6, 19)
        Me.lblSciezka.Name = "lblSciezka"
        Me.lblSciezka.Size = New System.Drawing.Size(48, 13)
        Me.lblSciezka.TabIndex = 0
        Me.lblSciezka.Text = "Ścieżka:"
        Me.ToolTip.SetToolTip(Me.lblSciezka, "Ścieżka do pliku")
        '
        'lblSKU
        '
        Me.lblSKU.AutoSize = True
        Me.lblSKU.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblSKU.ForeColor = System.Drawing.Color.Black
        Me.lblSKU.Location = New System.Drawing.Point(12, 11)
        Me.lblSKU.Name = "lblSKU"
        Me.lblSKU.Size = New System.Drawing.Size(150, 15)
        Me.lblSKU.TabIndex = 0
        Me.lblSKU.Text = "Produkt (SKU:315868)"
        '
        'ctrImgGaleria
        '
        Me.ctrImgGaleria.BackColor = System.Drawing.Color.White
        Me.ctrImgGaleria.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ctrImgGaleria.Location = New System.Drawing.Point(9, 37)
        Me.ctrImgGaleria.MaximumSize = New System.Drawing.Size(477, 400)
        Me.ctrImgGaleria.MinimumSize = New System.Drawing.Size(477, 400)
        Me.ctrImgGaleria.Name = "ctrImgGaleria"
        Me.ctrImgGaleria.Size = New System.Drawing.Size(477, 400)
        Me.ctrImgGaleria.TabIndex = 1
        '
        'frmGaleria
        '
        Me.AcceptButton = Me.btnDodajZdjecie
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnZamknij
        Me.ClientSize = New System.Drawing.Size(492, 643)
        Me.Controls.Add(Me.lblSKU)
        Me.Controls.Add(Me.ctrImgGaleria)
        Me.Controls.Add(Me.gbDodawanieZdjecia)
        Me.MinimumSize = New System.Drawing.Size(410, 410)
        Me.Name = "frmGaleria"
        Me.Text = "Galeria zdjęć"
        Me.gbDodawanieZdjecia.ResumeLayout(False)
        Me.gbDodawanieZdjecia.PerformLayout()
        CType(Me.picZdjeciePodglad, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnUsunZdjecie As System.Windows.Forms.Button
    Friend WithEvents gbDodawanieZdjecia As System.Windows.Forms.GroupBox
    Friend WithEvents btnDodajZdjecie As System.Windows.Forms.Button
    Friend WithEvents picZdjeciePodglad As System.Windows.Forms.PictureBox
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents txtNazwa As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtSciezka As System.Windows.Forms.TextBox
    Friend WithEvents lblSciezka As System.Windows.Forms.Label
    Friend WithEvents ctrImgGaleria As CursorProfClient.ctrImgGaleria
    Friend WithEvents lblSKU As System.Windows.Forms.Label
    Friend WithEvents btnZamknij As System.Windows.Forms.Button
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
End Class
