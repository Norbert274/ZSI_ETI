<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctrImgGaleria
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.btnStart = New System.Windows.Forms.Button()
        Me.btnPoprzednie = New System.Windows.Forms.Button()
        Me.btnNastepne = New System.Windows.Forms.Button()
        Me.btnKoniec = New System.Windows.Forms.Button()
        Me.lblNrBiezacegoZdjecia = New System.Windows.Forms.Label()
        Me.picZdjecie = New System.Windows.Forms.PictureBox()
        Me.lblZ = New System.Windows.Forms.Label()
        Me.lblIloscZdjec = New System.Windows.Forms.Label()
        Me.lblBrakZdjecia = New System.Windows.Forms.Label()
        Me.chkIsThumbnail = New System.Windows.Forms.CheckBox()
        CType(Me.picZdjecie, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnStart
        '
        Me.btnStart.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnStart.ForeColor = System.Drawing.Color.White
        Me.btnStart.Location = New System.Drawing.Point(3, 372)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(75, 23)
        Me.btnStart.TabIndex = 0
        Me.btnStart.Text = "<<"
        Me.btnStart.UseVisualStyleBackColor = False
        '
        'btnPoprzednie
        '
        Me.btnPoprzednie.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnPoprzednie.ForeColor = System.Drawing.Color.White
        Me.btnPoprzednie.Location = New System.Drawing.Point(84, 372)
        Me.btnPoprzednie.Name = "btnPoprzednie"
        Me.btnPoprzednie.Size = New System.Drawing.Size(75, 23)
        Me.btnPoprzednie.TabIndex = 1
        Me.btnPoprzednie.Text = "<"
        Me.btnPoprzednie.UseVisualStyleBackColor = False
        '
        'btnNastepne
        '
        Me.btnNastepne.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnNastepne.ForeColor = System.Drawing.Color.White
        Me.btnNastepne.Location = New System.Drawing.Point(316, 372)
        Me.btnNastepne.Name = "btnNastepne"
        Me.btnNastepne.Size = New System.Drawing.Size(75, 23)
        Me.btnNastepne.TabIndex = 2
        Me.btnNastepne.Text = ">"
        Me.btnNastepne.UseVisualStyleBackColor = False
        '
        'btnKoniec
        '
        Me.btnKoniec.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnKoniec.ForeColor = System.Drawing.Color.White
        Me.btnKoniec.Location = New System.Drawing.Point(397, 372)
        Me.btnKoniec.Name = "btnKoniec"
        Me.btnKoniec.Size = New System.Drawing.Size(75, 23)
        Me.btnKoniec.TabIndex = 3
        Me.btnKoniec.Text = ">>"
        Me.btnKoniec.UseVisualStyleBackColor = False
        '
        'lblNrBiezacegoZdjecia
        '
        Me.lblNrBiezacegoZdjecia.AutoSize = True
        Me.lblNrBiezacegoZdjecia.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblNrBiezacegoZdjecia.ForeColor = System.Drawing.Color.Black
        Me.lblNrBiezacegoZdjecia.Location = New System.Drawing.Point(172, 374)
        Me.lblNrBiezacegoZdjecia.MinimumSize = New System.Drawing.Size(59, 13)
        Me.lblNrBiezacegoZdjecia.Name = "lblNrBiezacegoZdjecia"
        Me.lblNrBiezacegoZdjecia.Size = New System.Drawing.Size(59, 15)
        Me.lblNrBiezacegoZdjecia.TabIndex = 4
        Me.lblNrBiezacegoZdjecia.Text = "1"
        Me.lblNrBiezacegoZdjecia.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'picZdjecie
        '
        Me.picZdjecie.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picZdjecie.Location = New System.Drawing.Point(3, 3)
        Me.picZdjecie.Name = "picZdjecie"
        Me.picZdjecie.Size = New System.Drawing.Size(469, 341)
        Me.picZdjecie.TabIndex = 5
        Me.picZdjecie.TabStop = False
        '
        'lblZ
        '
        Me.lblZ.AutoSize = True
        Me.lblZ.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblZ.ForeColor = System.Drawing.Color.Black
        Me.lblZ.Location = New System.Drawing.Point(237, 374)
        Me.lblZ.Name = "lblZ"
        Me.lblZ.Size = New System.Drawing.Size(13, 15)
        Me.lblZ.TabIndex = 6
        Me.lblZ.Text = "z"
        '
        'lblIloscZdjec
        '
        Me.lblIloscZdjec.AutoSize = True
        Me.lblIloscZdjec.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblIloscZdjec.ForeColor = System.Drawing.Color.Black
        Me.lblIloscZdjec.Location = New System.Drawing.Point(255, 374)
        Me.lblIloscZdjec.Name = "lblIloscZdjec"
        Me.lblIloscZdjec.Size = New System.Drawing.Size(13, 15)
        Me.lblIloscZdjec.TabIndex = 7
        Me.lblIloscZdjec.Text = "x"
        Me.lblIloscZdjec.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblBrakZdjecia
        '
        Me.lblBrakZdjecia.AutoSize = True
        Me.lblBrakZdjecia.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblBrakZdjecia.Location = New System.Drawing.Point(187, 165)
        Me.lblBrakZdjecia.Name = "lblBrakZdjecia"
        Me.lblBrakZdjecia.Size = New System.Drawing.Size(97, 17)
        Me.lblBrakZdjecia.TabIndex = 8
        Me.lblBrakZdjecia.Text = "Brak zdjęcia"
        Me.lblBrakZdjecia.Visible = False
        '
        'chkIsThumbnail
        '
        Me.chkIsThumbnail.AutoSize = True
        Me.chkIsThumbnail.Location = New System.Drawing.Point(166, 350)
        Me.chkIsThumbnail.Name = "chkIsThumbnail"
        Me.chkIsThumbnail.Size = New System.Drawing.Size(145, 17)
        Me.chkIsThumbnail.TabIndex = 10
        Me.chkIsThumbnail.Text = "To zdjęcie jest miniaturką"
        Me.chkIsThumbnail.UseVisualStyleBackColor = True
        '
        'ctrImgGaleria
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Controls.Add(Me.chkIsThumbnail)
        Me.Controls.Add(Me.lblBrakZdjecia)
        Me.Controls.Add(Me.lblIloscZdjec)
        Me.Controls.Add(Me.lblZ)
        Me.Controls.Add(Me.picZdjecie)
        Me.Controls.Add(Me.lblNrBiezacegoZdjecia)
        Me.Controls.Add(Me.btnKoniec)
        Me.Controls.Add(Me.btnNastepne)
        Me.Controls.Add(Me.btnPoprzednie)
        Me.Controls.Add(Me.btnStart)
        Me.MaximumSize = New System.Drawing.Size(477, 400)
        Me.MinimumSize = New System.Drawing.Size(477, 400)
        Me.Name = "ctrImgGaleria"
        Me.Size = New System.Drawing.Size(475, 398)
        CType(Me.picZdjecie, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnStart As System.Windows.Forms.Button
    Friend WithEvents btnPoprzednie As System.Windows.Forms.Button
    Friend WithEvents btnNastepne As System.Windows.Forms.Button
    Friend WithEvents btnKoniec As System.Windows.Forms.Button
    Friend WithEvents lblNrBiezacegoZdjecia As System.Windows.Forms.Label
    Friend WithEvents picZdjecie As System.Windows.Forms.PictureBox
    Friend WithEvents lblZ As System.Windows.Forms.Label
    Friend WithEvents lblIloscZdjec As System.Windows.Forms.Label
    Friend WithEvents lblBrakZdjecia As System.Windows.Forms.Label
    Friend WithEvents chkIsThumbnail As System.Windows.Forms.CheckBox

End Class
