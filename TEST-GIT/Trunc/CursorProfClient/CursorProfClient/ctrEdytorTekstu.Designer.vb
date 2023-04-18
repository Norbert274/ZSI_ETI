<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctrEdytorTekstu
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ctrEdytorTekstu))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmbRozmiarCzcionki = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.picKolor = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnBold = New System.Windows.Forms.Button()
        Me.btnUnderline = New System.Windows.Forms.Button()
        Me.btnItalic = New System.Windows.Forms.Button()
        Me.cmbNazwaCzcionki = New System.Windows.Forms.ComboBox()
        Me.btnKolorCzcionki = New System.Windows.Forms.Button()
        Me.rtbTekst = New System.Windows.Forms.RichTextBox()
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.GroupBox1.SuspendLayout()
        CType(Me.picKolor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmbRozmiarCzcionki)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.picKolor)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.btnBold)
        Me.GroupBox1.Controls.Add(Me.btnUnderline)
        Me.GroupBox1.Controls.Add(Me.btnItalic)
        Me.GroupBox1.Controls.Add(Me.cmbNazwaCzcionki)
        Me.GroupBox1.Controls.Add(Me.btnKolorCzcionki)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(512, 63)
        Me.GroupBox1.TabIndex = 11
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Czcionka:"
        '
        'cmbRozmiarCzcionki
        '
        Me.cmbRozmiarCzcionki.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRozmiarCzcionki.FormattingEnabled = True
        Me.cmbRozmiarCzcionki.Location = New System.Drawing.Point(206, 32)
        Me.cmbRozmiarCzcionki.Name = "cmbRozmiarCzcionki"
        Me.cmbRozmiarCzcionki.Size = New System.Drawing.Size(62, 21)
        Me.cmbRozmiarCzcionki.TabIndex = 11
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(282, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(34, 13)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Kolor:"
        '
        'picKolor
        '
        Me.picKolor.Location = New System.Drawing.Point(325, 31)
        Me.picKolor.Name = "picKolor"
        Me.picKolor.Size = New System.Drawing.Size(22, 21)
        Me.picKolor.TabIndex = 9
        Me.picKolor.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(17, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(43, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Nazwa:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(203, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(48, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Rozmiar:"
        '
        'btnBold
        '
        Me.btnBold.BackColor = System.Drawing.SystemColors.Control
        Me.btnBold.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnBold.Location = New System.Drawing.Point(361, 29)
        Me.btnBold.Name = "btnBold"
        Me.btnBold.Size = New System.Drawing.Size(33, 23)
        Me.btnBold.TabIndex = 1
        Me.btnBold.Text = " B"
        Me.btnBold.UseVisualStyleBackColor = False
        '
        'btnUnderline
        '
        Me.btnUnderline.BackColor = System.Drawing.SystemColors.Control
        Me.btnUnderline.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnUnderline.Location = New System.Drawing.Point(400, 29)
        Me.btnUnderline.Name = "btnUnderline"
        Me.btnUnderline.Size = New System.Drawing.Size(33, 23)
        Me.btnUnderline.TabIndex = 2
        Me.btnUnderline.Text = "U"
        Me.btnUnderline.UseVisualStyleBackColor = False
        '
        'btnItalic
        '
        Me.btnItalic.BackColor = System.Drawing.SystemColors.Control
        Me.btnItalic.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnItalic.Location = New System.Drawing.Point(439, 29)
        Me.btnItalic.Name = "btnItalic"
        Me.btnItalic.Size = New System.Drawing.Size(33, 23)
        Me.btnItalic.TabIndex = 6
        Me.btnItalic.Text = "I"
        Me.btnItalic.UseVisualStyleBackColor = False
        '
        'cmbNazwaCzcionki
        '
        Me.cmbNazwaCzcionki.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbNazwaCzcionki.FormattingEnabled = True
        Me.cmbNazwaCzcionki.Location = New System.Drawing.Point(20, 32)
        Me.cmbNazwaCzcionki.Name = "cmbNazwaCzcionki"
        Me.cmbNazwaCzcionki.Size = New System.Drawing.Size(180, 21)
        Me.cmbNazwaCzcionki.TabIndex = 3
        '
        'btnKolorCzcionki
        '
        Me.btnKolorCzcionki.BackColor = System.Drawing.SystemColors.Control
        Me.btnKolorCzcionki.BackgroundImage = CType(resources.GetObject("btnKolorCzcionki.BackgroundImage"), System.Drawing.Image)
        Me.btnKolorCzcionki.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnKolorCzcionki.Location = New System.Drawing.Point(285, 30)
        Me.btnKolorCzcionki.Name = "btnKolorCzcionki"
        Me.btnKolorCzcionki.Size = New System.Drawing.Size(38, 23)
        Me.btnKolorCzcionki.TabIndex = 5
        Me.btnKolorCzcionki.Text = " "
        Me.btnKolorCzcionki.UseVisualStyleBackColor = False
        '
        'rtbTekst
        '
        Me.rtbTekst.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rtbTekst.Location = New System.Drawing.Point(3, 72)
        Me.rtbTekst.Name = "rtbTekst"
        Me.rtbTekst.Size = New System.Drawing.Size(512, 247)
        Me.rtbTekst.TabIndex = 10
        Me.rtbTekst.Text = ""
        '
        'ctrEdytorTekstu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.rtbTekst)
        Me.Name = "ctrEdytorTekstu"
        Me.Size = New System.Drawing.Size(521, 323)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.picKolor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents picKolor As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnBold As System.Windows.Forms.Button
    Friend WithEvents btnUnderline As System.Windows.Forms.Button
    Friend WithEvents btnItalic As System.Windows.Forms.Button
    Friend WithEvents cmbNazwaCzcionki As System.Windows.Forms.ComboBox
    Friend WithEvents btnKolorCzcionki As System.Windows.Forms.Button
    Friend WithEvents rtbTekst As System.Windows.Forms.RichTextBox
    Friend WithEvents ColorDialog1 As System.Windows.Forms.ColorDialog
    Friend WithEvents cmbRozmiarCzcionki As System.Windows.Forms.ComboBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip

End Class
