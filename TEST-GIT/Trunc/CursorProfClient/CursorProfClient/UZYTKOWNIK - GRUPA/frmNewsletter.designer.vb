<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNewsletter
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
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.btnWyslij = New System.Windows.Forms.Button()
        Me.btnAnuluj = New System.Windows.Forms.Button()
        Me.lblWiadomosc = New System.Windows.Forms.Label()
        Me.lblTytul = New System.Windows.Forms.Label()
        Me.rtxtWiadomosc = New System.Windows.Forms.RichTextBox()
        Me.txtTytul = New System.Windows.Forms.TextBox()
        Me.lblGrupy = New System.Windows.Forms.Label()
        Me.chkListGrupy = New System.Windows.Forms.CheckedListBox()
        Me.gbZalacznik = New System.Windows.Forms.GroupBox()
        Me.btnPlik = New System.Windows.Forms.Button()
        Me.lblPlik = New System.Windows.Forms.Label()
        Me.tc = New System.Windows.Forms.TabControl()
        Me.tpAdresaci = New System.Windows.Forms.TabPage()
        Me.lblUzytkownicy = New System.Windows.Forms.Label()
        Me.dgv = New System.Windows.Forms.DataGridView()
        Me.chkListTyp = New System.Windows.Forms.CheckedListBox()
        Me.lblTyp = New System.Windows.Forms.Label()
        Me.chkListWielkosc = New System.Windows.Forms.CheckedListBox()
        Me.lblWielkosc = New System.Windows.Forms.Label()
        Me.tpWiadomosc = New System.Windows.Forms.TabPage()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.gbZalacznik.SuspendLayout()
        Me.tc.SuspendLayout()
        Me.tpAdresaci.SuspendLayout()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpWiadomosc.SuspendLayout()
        Me.SuspendLayout()
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'btnWyslij
        '
        Me.btnWyslij.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnWyslij.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnWyslij.ForeColor = System.Drawing.Color.White
        Me.btnWyslij.Location = New System.Drawing.Point(701, 427)
        Me.btnWyslij.Name = "btnWyslij"
        Me.btnWyslij.Size = New System.Drawing.Size(77, 23)
        Me.btnWyslij.TabIndex = 1
        Me.btnWyslij.Text = "Wyślij"
        Me.ToolTip1.SetToolTip(Me.btnWyslij, "Wysłanie wiadomości do wybranych użytkowników")
        Me.btnWyslij.UseVisualStyleBackColor = False
        '
        'btnAnuluj
        '
        Me.btnAnuluj.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAnuluj.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnAnuluj.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnAnuluj.ForeColor = System.Drawing.Color.White
        Me.btnAnuluj.Location = New System.Drawing.Point(784, 427)
        Me.btnAnuluj.Name = "btnAnuluj"
        Me.btnAnuluj.Size = New System.Drawing.Size(77, 23)
        Me.btnAnuluj.TabIndex = 2
        Me.btnAnuluj.Text = "Anuluj"
        Me.btnAnuluj.UseVisualStyleBackColor = False
        '
        'lblWiadomosc
        '
        Me.lblWiadomosc.AutoSize = True
        Me.lblWiadomosc.ForeColor = System.Drawing.Color.Black
        Me.lblWiadomosc.Location = New System.Drawing.Point(7, 71)
        Me.lblWiadomosc.Name = "lblWiadomosc"
        Me.lblWiadomosc.Size = New System.Drawing.Size(66, 13)
        Me.lblWiadomosc.TabIndex = 9
        Me.lblWiadomosc.Text = "Wiadomość:"
        '
        'lblTytul
        '
        Me.lblTytul.AutoSize = True
        Me.lblTytul.ForeColor = System.Drawing.Color.Black
        Me.lblTytul.Location = New System.Drawing.Point(7, 16)
        Me.lblTytul.Name = "lblTytul"
        Me.lblTytul.Size = New System.Drawing.Size(35, 13)
        Me.lblTytul.TabIndex = 8
        Me.lblTytul.Text = "Tytuł:"
        '
        'rtxtWiadomosc
        '
        Me.rtxtWiadomosc.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rtxtWiadomosc.Location = New System.Drawing.Point(79, 68)
        Me.rtxtWiadomosc.Name = "rtxtWiadomosc"
        Me.rtxtWiadomosc.Size = New System.Drawing.Size(367, 318)
        Me.rtxtWiadomosc.TabIndex = 7
        Me.rtxtWiadomosc.Text = ""
        '
        'txtTytul
        '
        Me.txtTytul.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTytul.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtTytul.Location = New System.Drawing.Point(79, 13)
        Me.txtTytul.Multiline = True
        Me.txtTytul.Name = "txtTytul"
        Me.txtTytul.Size = New System.Drawing.Size(367, 49)
        Me.txtTytul.TabIndex = 6
        '
        'lblGrupy
        '
        Me.lblGrupy.AutoSize = True
        Me.lblGrupy.ForeColor = System.Drawing.Color.Black
        Me.lblGrupy.Location = New System.Drawing.Point(7, 3)
        Me.lblGrupy.Name = "lblGrupy"
        Me.lblGrupy.Size = New System.Drawing.Size(108, 13)
        Me.lblGrupy.TabIndex = 0
        Me.lblGrupy.Text = "Grupy użytkowników:"
        Me.ToolTip1.SetToolTip(Me.lblGrupy, "Filtrowanie użytkowników wg grupy")
        '
        'chkListGrupy
        '
        Me.chkListGrupy.BackColor = System.Drawing.Color.White
        Me.chkListGrupy.CheckOnClick = True
        Me.chkListGrupy.ForeColor = System.Drawing.Color.Black
        Me.chkListGrupy.FormattingEnabled = True
        Me.chkListGrupy.Location = New System.Drawing.Point(7, 19)
        Me.chkListGrupy.Name = "chkListGrupy"
        Me.chkListGrupy.Size = New System.Drawing.Size(135, 109)
        Me.chkListGrupy.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.chkListGrupy, "Filtrowanie użytkowników wg grupy")
        '
        'gbZalacznik
        '
        Me.gbZalacznik.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbZalacznik.Controls.Add(Me.btnPlik)
        Me.gbZalacznik.Controls.Add(Me.lblPlik)
        Me.gbZalacznik.ForeColor = System.Drawing.Color.Black
        Me.gbZalacznik.Location = New System.Drawing.Point(21, 305)
        Me.gbZalacznik.Name = "gbZalacznik"
        Me.gbZalacznik.Size = New System.Drawing.Size(425, 70)
        Me.gbZalacznik.TabIndex = 14
        Me.gbZalacznik.TabStop = False
        Me.gbZalacznik.Text = "Załącznik"
        Me.gbZalacznik.Visible = False
        '
        'btnPlik
        '
        Me.btnPlik.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnPlik.ForeColor = System.Drawing.Color.White
        Me.btnPlik.Location = New System.Drawing.Point(8, 23)
        Me.btnPlik.Name = "btnPlik"
        Me.btnPlik.Size = New System.Drawing.Size(84, 23)
        Me.btnPlik.TabIndex = 13
        Me.btnPlik.Text = "Wybierz plik"
        Me.btnPlik.UseVisualStyleBackColor = False
        '
        'lblPlik
        '
        Me.lblPlik.ForeColor = System.Drawing.Color.Black
        Me.lblPlik.Location = New System.Drawing.Point(98, 28)
        Me.lblPlik.MinimumSize = New System.Drawing.Size(200, 13)
        Me.lblPlik.Name = "lblPlik"
        Me.lblPlik.Size = New System.Drawing.Size(391, 36)
        Me.lblPlik.TabIndex = 12
        Me.lblPlik.Text = "Nie wybrano pliku"
        '
        'tc
        '
        Me.tc.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tc.Controls.Add(Me.tpAdresaci)
        Me.tc.Controls.Add(Me.tpWiadomosc)
        Me.tc.Location = New System.Drawing.Point(1, 2)
        Me.tc.Name = "tc"
        Me.tc.SelectedIndex = 0
        Me.tc.Size = New System.Drawing.Size(870, 418)
        Me.tc.TabIndex = 0
        '
        'tpAdresaci
        '
        Me.tpAdresaci.BackColor = System.Drawing.Color.White
        Me.tpAdresaci.Controls.Add(Me.lblUzytkownicy)
        Me.tpAdresaci.Controls.Add(Me.dgv)
        Me.tpAdresaci.Controls.Add(Me.chkListTyp)
        Me.tpAdresaci.Controls.Add(Me.lblTyp)
        Me.tpAdresaci.Controls.Add(Me.chkListWielkosc)
        Me.tpAdresaci.Controls.Add(Me.lblWielkosc)
        Me.tpAdresaci.Controls.Add(Me.lblGrupy)
        Me.tpAdresaci.Controls.Add(Me.chkListGrupy)
        Me.tpAdresaci.Location = New System.Drawing.Point(4, 22)
        Me.tpAdresaci.Name = "tpAdresaci"
        Me.tpAdresaci.Padding = New System.Windows.Forms.Padding(3)
        Me.tpAdresaci.Size = New System.Drawing.Size(862, 392)
        Me.tpAdresaci.TabIndex = 0
        Me.tpAdresaci.Text = "Adresaci"
        '
        'lblUzytkownicy
        '
        Me.lblUzytkownicy.AutoSize = True
        Me.lblUzytkownicy.ForeColor = System.Drawing.Color.Black
        Me.lblUzytkownicy.Location = New System.Drawing.Point(146, 3)
        Me.lblUzytkownicy.Name = "lblUzytkownicy"
        Me.lblUzytkownicy.Size = New System.Drawing.Size(70, 13)
        Me.lblUzytkownicy.TabIndex = 6
        Me.lblUzytkownicy.Text = "Użytkownicy:"
        Me.ToolTip1.SetToolTip(Me.lblUzytkownicy, "Lista użytkowników, na której zaznaczamy osoby, " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "do których zostanie wysłana wia" & _
        "domość")
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
        Me.dgv.Location = New System.Drawing.Point(149, 19)
        Me.dgv.Name = "dgv"
        Me.dgv.Size = New System.Drawing.Size(707, 364)
        Me.dgv.TabIndex = 7
        '
        'chkListTyp
        '
        Me.chkListTyp.BackColor = System.Drawing.Color.White
        Me.chkListTyp.CheckOnClick = True
        Me.chkListTyp.ForeColor = System.Drawing.Color.Black
        Me.chkListTyp.FormattingEnabled = True
        Me.chkListTyp.Location = New System.Drawing.Point(7, 275)
        Me.chkListTyp.Name = "chkListTyp"
        Me.chkListTyp.Size = New System.Drawing.Size(135, 109)
        Me.chkListTyp.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.chkListTyp, "Filtrowanie użytkowników wg typu")
        '
        'lblTyp
        '
        Me.lblTyp.AutoSize = True
        Me.lblTyp.ForeColor = System.Drawing.Color.Black
        Me.lblTyp.Location = New System.Drawing.Point(7, 259)
        Me.lblTyp.Name = "lblTyp"
        Me.lblTyp.Size = New System.Drawing.Size(103, 13)
        Me.lblTyp.TabIndex = 4
        Me.lblTyp.Text = "Typy użytkowników:"
        Me.ToolTip1.SetToolTip(Me.lblTyp, "Filtrowanie użytkowników wg typu")
        '
        'chkListWielkosc
        '
        Me.chkListWielkosc.BackColor = System.Drawing.Color.White
        Me.chkListWielkosc.CheckOnClick = True
        Me.chkListWielkosc.ForeColor = System.Drawing.Color.Black
        Me.chkListWielkosc.FormattingEnabled = True
        Me.chkListWielkosc.Location = New System.Drawing.Point(7, 147)
        Me.chkListWielkosc.Name = "chkListWielkosc"
        Me.chkListWielkosc.Size = New System.Drawing.Size(135, 109)
        Me.chkListWielkosc.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.chkListWielkosc, "Filtrowanie użytkowników wg wielkości")
        '
        'lblWielkosc
        '
        Me.lblWielkosc.AutoSize = True
        Me.lblWielkosc.ForeColor = System.Drawing.Color.Black
        Me.lblWielkosc.Location = New System.Drawing.Point(7, 131)
        Me.lblWielkosc.Name = "lblWielkosc"
        Me.lblWielkosc.Size = New System.Drawing.Size(124, 13)
        Me.lblWielkosc.TabIndex = 2
        Me.lblWielkosc.Text = "Wielkość użytkowników:"
        Me.ToolTip1.SetToolTip(Me.lblWielkosc, "Filtrowanie użytkowników wg wielkości")
        '
        'tpWiadomosc
        '
        Me.tpWiadomosc.BackColor = System.Drawing.Color.White
        Me.tpWiadomosc.Controls.Add(Me.txtTytul)
        Me.tpWiadomosc.Controls.Add(Me.rtxtWiadomosc)
        Me.tpWiadomosc.Controls.Add(Me.lblWiadomosc)
        Me.tpWiadomosc.Controls.Add(Me.lblTytul)
        Me.tpWiadomosc.Controls.Add(Me.gbZalacznik)
        Me.tpWiadomosc.Location = New System.Drawing.Point(4, 22)
        Me.tpWiadomosc.Name = "tpWiadomosc"
        Me.tpWiadomosc.Padding = New System.Windows.Forms.Padding(3)
        Me.tpWiadomosc.Size = New System.Drawing.Size(454, 392)
        Me.tpWiadomosc.TabIndex = 1
        Me.tpWiadomosc.Text = "Wiadomość"
        '
        'ToolTip1
        '
        Me.ToolTip1.AutoPopDelay = 10000
        Me.ToolTip1.InitialDelay = 500
        Me.ToolTip1.ReshowDelay = 500
        '
        'frmNewsletter
        '
        Me.AcceptButton = Me.btnWyslij
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnAnuluj
        Me.ClientSize = New System.Drawing.Size(871, 462)
        Me.Controls.Add(Me.tc)
        Me.Controls.Add(Me.btnAnuluj)
        Me.Controls.Add(Me.btnWyslij)
        Me.MinimumSize = New System.Drawing.Size(479, 500)
        Me.Name = "frmNewsletter"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Newsletter"
        Me.gbZalacznik.ResumeLayout(False)
        Me.tc.ResumeLayout(False)
        Me.tpAdresaci.ResumeLayout(False)
        Me.tpAdresaci.PerformLayout()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpWiadomosc.ResumeLayout(False)
        Me.tpWiadomosc.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents btnWyslij As System.Windows.Forms.Button
    Friend WithEvents btnAnuluj As System.Windows.Forms.Button
    Friend WithEvents lblWiadomosc As System.Windows.Forms.Label
    Friend WithEvents lblTytul As System.Windows.Forms.Label
    Friend WithEvents rtxtWiadomosc As System.Windows.Forms.RichTextBox
    Friend WithEvents txtTytul As System.Windows.Forms.TextBox
    Friend WithEvents lblGrupy As System.Windows.Forms.Label
    Friend WithEvents chkListGrupy As System.Windows.Forms.CheckedListBox
    Friend WithEvents gbZalacznik As System.Windows.Forms.GroupBox
    Friend WithEvents btnPlik As System.Windows.Forms.Button
    Friend WithEvents lblPlik As System.Windows.Forms.Label
    Friend WithEvents tc As System.Windows.Forms.TabControl
    Friend WithEvents tpAdresaci As System.Windows.Forms.TabPage
    Friend WithEvents lblUzytkownicy As System.Windows.Forms.Label
    Friend WithEvents dgv As System.Windows.Forms.DataGridView
    Friend WithEvents chkListTyp As System.Windows.Forms.CheckedListBox
    Friend WithEvents lblTyp As System.Windows.Forms.Label
    Friend WithEvents chkListWielkosc As System.Windows.Forms.CheckedListBox
    Friend WithEvents lblWielkosc As System.Windows.Forms.Label
    Friend WithEvents tpWiadomosc As System.Windows.Forms.TabPage
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class
