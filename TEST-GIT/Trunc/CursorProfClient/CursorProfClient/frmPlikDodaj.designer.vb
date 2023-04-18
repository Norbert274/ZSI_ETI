<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPlikDodaj
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
        Me.btnDodaj = New System.Windows.Forms.Button()
        Me.btnAnuluj = New System.Windows.Forms.Button()
        Me.txtPilk = New System.Windows.Forms.TextBox()
        Me.lblPlik = New System.Windows.Forms.Label()
        Me.lblMiniaturka = New System.Windows.Forms.Label()
        Me.txtMiniaturka = New System.Windows.Forms.TextBox()
        Me.lblTytul = New System.Windows.Forms.Label()
        Me.txtTytul = New System.Windows.Forms.TextBox()
        Me.btnPlik = New System.Windows.Forms.Button()
        Me.btnMiniaturka = New System.Windows.Forms.Button()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.chkWyswietlajNaWWW = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'btnDodaj
        '
        Me.btnDodaj.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDodaj.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnDodaj.ForeColor = System.Drawing.Color.White
        Me.btnDodaj.Location = New System.Drawing.Point(158, 103)
        Me.btnDodaj.Name = "btnDodaj"
        Me.btnDodaj.Size = New System.Drawing.Size(75, 23)
        Me.btnDodaj.TabIndex = 8
        Me.btnDodaj.Text = "Dodaj"
        Me.btnDodaj.UseVisualStyleBackColor = False
        '
        'btnAnuluj
        '
        Me.btnAnuluj.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAnuluj.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnAnuluj.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnAnuluj.ForeColor = System.Drawing.Color.White
        Me.btnAnuluj.Location = New System.Drawing.Point(239, 103)
        Me.btnAnuluj.Name = "btnAnuluj"
        Me.btnAnuluj.Size = New System.Drawing.Size(75, 23)
        Me.btnAnuluj.TabIndex = 9
        Me.btnAnuluj.Text = "Anuluj"
        Me.btnAnuluj.UseVisualStyleBackColor = False
        '
        'txtPilk
        '
        Me.txtPilk.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtPilk.Location = New System.Drawing.Point(72, 32)
        Me.txtPilk.Name = "txtPilk"
        Me.txtPilk.Size = New System.Drawing.Size(202, 20)
        Me.txtPilk.TabIndex = 3
        Me.ToolTip.SetToolTip(Me.txtPilk, "Ścieżka do pliku")
        '
        'lblPlik
        '
        Me.lblPlik.AutoSize = True
        Me.lblPlik.ForeColor = System.Drawing.Color.Black
        Me.lblPlik.Location = New System.Drawing.Point(10, 35)
        Me.lblPlik.Name = "lblPlik"
        Me.lblPlik.Size = New System.Drawing.Size(24, 13)
        Me.lblPlik.TabIndex = 2
        Me.lblPlik.Text = "Plik"
        '
        'lblMiniaturka
        '
        Me.lblMiniaturka.AutoSize = True
        Me.lblMiniaturka.ForeColor = System.Drawing.Color.Black
        Me.lblMiniaturka.Location = New System.Drawing.Point(10, 61)
        Me.lblMiniaturka.Name = "lblMiniaturka"
        Me.lblMiniaturka.Size = New System.Drawing.Size(56, 13)
        Me.lblMiniaturka.TabIndex = 5
        Me.lblMiniaturka.Text = "Miniaturka"
        '
        'txtMiniaturka
        '
        Me.txtMiniaturka.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtMiniaturka.Location = New System.Drawing.Point(72, 58)
        Me.txtMiniaturka.Name = "txtMiniaturka"
        Me.txtMiniaturka.Size = New System.Drawing.Size(202, 20)
        Me.txtMiniaturka.TabIndex = 6
        Me.ToolTip.SetToolTip(Me.txtMiniaturka, "Miniatura pliku - pole nieobowiązkowe")
        '
        'lblTytul
        '
        Me.lblTytul.AutoSize = True
        Me.lblTytul.ForeColor = System.Drawing.Color.Black
        Me.lblTytul.Location = New System.Drawing.Point(10, 9)
        Me.lblTytul.Name = "lblTytul"
        Me.lblTytul.Size = New System.Drawing.Size(32, 13)
        Me.lblTytul.TabIndex = 0
        Me.lblTytul.Text = "Tytuł"
        '
        'txtTytul
        '
        Me.txtTytul.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTytul.BackColor = System.Drawing.Color.White
        Me.txtTytul.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtTytul.Location = New System.Drawing.Point(72, 6)
        Me.txtTytul.Name = "txtTytul"
        Me.txtTytul.Size = New System.Drawing.Size(202, 20)
        Me.txtTytul.TabIndex = 1
        Me.ToolTip.SetToolTip(Me.txtTytul, "Tytuł pod jakim będzie widoczny plik")
        '
        'btnPlik
        '
        Me.btnPlik.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPlik.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnPlik.ForeColor = System.Drawing.Color.White
        Me.btnPlik.Location = New System.Drawing.Point(280, 29)
        Me.btnPlik.Name = "btnPlik"
        Me.btnPlik.Size = New System.Drawing.Size(34, 23)
        Me.btnPlik.TabIndex = 4
        Me.btnPlik.Text = "..."
        Me.btnPlik.UseVisualStyleBackColor = False
        '
        'btnMiniaturka
        '
        Me.btnMiniaturka.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnMiniaturka.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnMiniaturka.ForeColor = System.Drawing.Color.White
        Me.btnMiniaturka.Location = New System.Drawing.Point(280, 55)
        Me.btnMiniaturka.Name = "btnMiniaturka"
        Me.btnMiniaturka.Size = New System.Drawing.Size(34, 23)
        Me.btnMiniaturka.TabIndex = 7
        Me.btnMiniaturka.Text = "..."
        Me.btnMiniaturka.UseVisualStyleBackColor = False
        '
        'chkWyswietlajNaWWW
        '
        Me.chkWyswietlajNaWWW.AutoSize = True
        Me.chkWyswietlajNaWWW.ForeColor = System.Drawing.Color.Black
        Me.chkWyswietlajNaWWW.Location = New System.Drawing.Point(72, 84)
        Me.chkWyswietlajNaWWW.Name = "chkWyswietlajNaWWW"
        Me.chkWyswietlajNaWWW.Size = New System.Drawing.Size(189, 17)
        Me.chkWyswietlajNaWWW.TabIndex = 10
        Me.chkWyswietlajNaWWW.Text = "Wyświetlaj ten plik w wersji WWW"
        Me.chkWyswietlajNaWWW.UseVisualStyleBackColor = True
        '
        'frmPlikDodaj
        '
        Me.AcceptButton = Me.btnDodaj
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnAnuluj
        Me.ClientSize = New System.Drawing.Size(326, 132)
        Me.Controls.Add(Me.chkWyswietlajNaWWW)
        Me.Controls.Add(Me.btnMiniaturka)
        Me.Controls.Add(Me.btnPlik)
        Me.Controls.Add(Me.lblTytul)
        Me.Controls.Add(Me.txtTytul)
        Me.Controls.Add(Me.lblMiniaturka)
        Me.Controls.Add(Me.txtMiniaturka)
        Me.Controls.Add(Me.lblPlik)
        Me.Controls.Add(Me.txtPilk)
        Me.Controls.Add(Me.btnAnuluj)
        Me.Controls.Add(Me.btnDodaj)
        Me.ForeColor = System.Drawing.Color.White
        Me.MaximumSize = New System.Drawing.Size(408, 170)
        Me.Name = "frmPlikDodaj"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Dodaj plik"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnDodaj As System.Windows.Forms.Button
    Friend WithEvents btnAnuluj As System.Windows.Forms.Button
    Friend WithEvents txtPilk As System.Windows.Forms.TextBox
    Friend WithEvents lblPlik As System.Windows.Forms.Label
    Friend WithEvents lblMiniaturka As System.Windows.Forms.Label
    Friend WithEvents txtMiniaturka As System.Windows.Forms.TextBox
    Friend WithEvents lblTytul As System.Windows.Forms.Label
    Friend WithEvents txtTytul As System.Windows.Forms.TextBox
    Friend WithEvents btnPlik As System.Windows.Forms.Button
    Friend WithEvents btnMiniaturka As System.Windows.Forms.Button
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents chkWyswietlajNaWWW As System.Windows.Forms.CheckBox
End Class
