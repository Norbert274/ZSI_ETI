<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLimityDodajUzytkownik
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLimityDodajUzytkownik))
        Me.Label2 = New System.Windows.Forms.Label()
        Me.rtxtKomentarz = New System.Windows.Forms.RichTextBox()
        Me.txtLimit = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dgv = New System.Windows.Forms.DataGridView()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.txtFiltruj = New System.Windows.Forms.ToolStripTextBox()
        Me.sc = New System.Windows.Forms.SplitContainer()
        Me.tv = New System.Windows.Forms.TreeView()
        Me.tsGrupy = New System.Windows.Forms.ToolStrip()
        Me.lblWszystkie = New System.Windows.Forms.ToolStripLabel()
        Me.btnZaznaczWszystkie = New System.Windows.Forms.ToolStripButton()
        Me.btnOdznaczWszystkie = New System.Windows.Forms.ToolStripButton()
        Me.btnZamknij = New System.Windows.Forms.Button()
        Me.btnZapisz = New System.Windows.Forms.Button()
        Me.tsGorny = New System.Windows.Forms.ToolStrip()
        Me.btnFiltruj = New System.Windows.Forms.ToolStripButton()
        Me.btnFiltr = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sc.Panel1.SuspendLayout()
        Me.sc.Panel2.SuspendLayout()
        Me.sc.SuspendLayout()
        Me.tsGrupy.SuspendLayout()
        Me.tsGorny.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(23, 327)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Komentarz:"
        Me.ToolTip1.SetToolTip(Me.Label2, "Komentarz do przydzielenia limitu")
        '
        'rtxtKomentarz
        '
        Me.rtxtKomentarz.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rtxtKomentarz.Location = New System.Drawing.Point(89, 327)
        Me.rtxtKomentarz.Name = "rtxtKomentarz"
        Me.rtxtKomentarz.Size = New System.Drawing.Size(299, 59)
        Me.rtxtKomentarz.TabIndex = 5
        Me.rtxtKomentarz.Text = ""
        Me.ToolTip1.SetToolTip(Me.rtxtKomentarz, "Komentarz do przydzielenia limitu")
        '
        'txtLimit
        '
        Me.txtLimit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtLimit.Location = New System.Drawing.Point(89, 301)
        Me.txtLimit.Name = "txtLimit"
        Me.txtLimit.Size = New System.Drawing.Size(130, 20)
        Me.txtLimit.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.txtLimit, "Limit, który zostanie przypisany zaznaczonym użytkownikom na powyżej liście")
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(23, 303)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(31, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Limit:"
        Me.ToolTip1.SetToolTip(Me.Label1, "Limit, który zostanie przypisany zaznaczonym użytkownikom na powyżej liście")
        '
        'dgv
        '
        Me.dgv.AllowUserToAddRows = False
        Me.dgv.AllowUserToDeleteRows = False
        Me.dgv.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue
        Me.dgv.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgv.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv.DefaultCellStyle = DataGridViewCellStyle3
        Me.dgv.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.dgv.Location = New System.Drawing.Point(0, 25)
        Me.dgv.Name = "dgv"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.dgv.RowHeadersVisible = False
        Me.dgv.Size = New System.Drawing.Size(400, 270)
        Me.dgv.TabIndex = 1
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(6, 25)
        '
        'txtFiltruj
        '
        Me.txtFiltruj.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFiltruj.Name = "txtFiltruj"
        Me.txtFiltruj.Size = New System.Drawing.Size(200, 25)
        '
        'sc
        '
        Me.sc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sc.Location = New System.Drawing.Point(0, 0)
        Me.sc.Name = "sc"
        '
        'sc.Panel1
        '
        Me.sc.Panel1.Controls.Add(Me.tv)
        Me.sc.Panel1.Controls.Add(Me.tsGrupy)
        Me.sc.Panel1Collapsed = True
        Me.sc.Panel1MinSize = 180
        '
        'sc.Panel2
        '
        Me.sc.Panel2.BackColor = System.Drawing.Color.White
        Me.sc.Panel2.Controls.Add(Me.btnZamknij)
        Me.sc.Panel2.Controls.Add(Me.btnZapisz)
        Me.sc.Panel2.Controls.Add(Me.Label2)
        Me.sc.Panel2.Controls.Add(Me.rtxtKomentarz)
        Me.sc.Panel2.Controls.Add(Me.txtLimit)
        Me.sc.Panel2.Controls.Add(Me.Label1)
        Me.sc.Panel2.Controls.Add(Me.dgv)
        Me.sc.Panel2.Controls.Add(Me.tsGorny)
        Me.sc.Panel2.Controls.Add(Me.btnFiltr)
        Me.sc.Size = New System.Drawing.Size(400, 423)
        Me.sc.SplitterDistance = 180
        Me.sc.TabIndex = 10
        '
        'tv
        '
        Me.tv.CheckBoxes = True
        Me.tv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tv.Location = New System.Drawing.Point(0, 0)
        Me.tv.Name = "tv"
        Me.tv.Size = New System.Drawing.Size(180, 100)
        Me.tv.TabIndex = 11
        '
        'tsGrupy
        '
        Me.tsGrupy.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblWszystkie, Me.btnZaznaczWszystkie, Me.btnOdznaczWszystkie})
        Me.tsGrupy.Location = New System.Drawing.Point(0, 0)
        Me.tsGrupy.Name = "tsGrupy"
        Me.tsGrupy.Size = New System.Drawing.Size(180, 25)
        Me.tsGrupy.TabIndex = 10
        Me.tsGrupy.Text = "ToolStrip1"
        Me.tsGrupy.Visible = False
        '
        'lblWszystkie
        '
        Me.lblWszystkie.Name = "lblWszystkie"
        Me.lblWszystkie.Size = New System.Drawing.Size(61, 22)
        Me.lblWszystkie.Text = "Wszystkie:"
        '
        'btnZaznaczWszystkie
        '
        Me.btnZaznaczWszystkie.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnZaznaczWszystkie.Image = CType(resources.GetObject("btnZaznaczWszystkie.Image"), System.Drawing.Image)
        Me.btnZaznaczWszystkie.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnZaznaczWszystkie.Name = "btnZaznaczWszystkie"
        Me.btnZaznaczWszystkie.Size = New System.Drawing.Size(53, 22)
        Me.btnZaznaczWszystkie.Text = "Zaznacz"
        '
        'btnOdznaczWszystkie
        '
        Me.btnOdznaczWszystkie.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnOdznaczWszystkie.Image = CType(resources.GetObject("btnOdznaczWszystkie.Image"), System.Drawing.Image)
        Me.btnOdznaczWszystkie.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnOdznaczWszystkie.Name = "btnOdznaczWszystkie"
        Me.btnOdznaczWszystkie.Size = New System.Drawing.Size(56, 22)
        Me.btnOdznaczWszystkie.Text = "Odznacz"
        '
        'btnZamknij
        '
        Me.btnZamknij.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnZamknij.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnZamknij.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnZamknij.ForeColor = System.Drawing.Color.White
        Me.btnZamknij.Location = New System.Drawing.Point(301, 392)
        Me.btnZamknij.Name = "btnZamknij"
        Me.btnZamknij.Size = New System.Drawing.Size(87, 23)
        Me.btnZamknij.TabIndex = 7
        Me.btnZamknij.Text = "Zamknij"
        Me.ToolTip1.SetToolTip(Me.btnZamknij, "Zamknięcie okna")
        Me.btnZamknij.UseVisualStyleBackColor = False
        '
        'btnZapisz
        '
        Me.btnZapisz.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnZapisz.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnZapisz.ForeColor = System.Drawing.Color.White
        Me.btnZapisz.Location = New System.Drawing.Point(220, 392)
        Me.btnZapisz.Name = "btnZapisz"
        Me.btnZapisz.Size = New System.Drawing.Size(75, 23)
        Me.btnZapisz.TabIndex = 6
        Me.btnZapisz.Text = "Zapisz"
        Me.ToolTip1.SetToolTip(Me.btnZapisz, "Zapisuje limit z komentarzem dla wybranych użytkowników")
        Me.btnZapisz.UseVisualStyleBackColor = False
        '
        'tsGorny
        '
        Me.tsGorny.BackColor = System.Drawing.Color.White
        Me.tsGorny.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.txtFiltruj, Me.btnFiltruj, Me.ToolStripSeparator6})
        Me.tsGorny.Location = New System.Drawing.Point(0, 0)
        Me.tsGorny.Name = "tsGorny"
        Me.tsGorny.Size = New System.Drawing.Size(400, 25)
        Me.tsGorny.TabIndex = 0
        Me.tsGorny.Text = "ToolStrip3"
        '
        'btnFiltruj
        '
        Me.btnFiltruj.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnFiltruj.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnFiltruj.ForeColor = System.Drawing.Color.White
        Me.btnFiltruj.Image = CType(resources.GetObject("btnFiltruj.Image"), System.Drawing.Image)
        Me.btnFiltruj.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnFiltruj.Name = "btnFiltruj"
        Me.btnFiltruj.Size = New System.Drawing.Size(130, 22)
        Me.btnFiltruj.Text = "Wyszukaj Użytkownika"
        '
        'btnFiltr
        '
        Me.btnFiltr.Location = New System.Drawing.Point(76, 134)
        Me.btnFiltr.Name = "btnFiltr"
        Me.btnFiltr.Size = New System.Drawing.Size(75, 23)
        Me.btnFiltr.TabIndex = 8
        Me.btnFiltr.Text = "Button1"
        Me.btnFiltr.UseVisualStyleBackColor = True
        '
        'ToolTip1
        '
        Me.ToolTip1.AutoPopDelay = 10000
        Me.ToolTip1.InitialDelay = 500
        Me.ToolTip1.ReshowDelay = 500
        '
        'frmLimityDodajUzytkownik
        '
        Me.AcceptButton = Me.btnFiltr
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnZamknij
        Me.ClientSize = New System.Drawing.Size(400, 423)
        Me.Controls.Add(Me.sc)
        Me.MinimumSize = New System.Drawing.Size(416, 461)
        Me.Name = "frmLimityDodajUzytkownik"
        Me.Text = "Limity dla wybranych użytkowników"
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sc.Panel1.ResumeLayout(False)
        Me.sc.Panel1.PerformLayout()
        Me.sc.Panel2.ResumeLayout(False)
        Me.sc.Panel2.PerformLayout()
        CType(Me.sc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sc.ResumeLayout(False)
        Me.tsGrupy.ResumeLayout(False)
        Me.tsGrupy.PerformLayout()
        Me.tsGorny.ResumeLayout(False)
        Me.tsGorny.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents rtxtKomentarz As System.Windows.Forms.RichTextBox
    Friend WithEvents txtLimit As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnZaznaczWszystkie As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnOdznaczWszystkie As System.Windows.Forms.ToolStripButton
    Friend WithEvents dgv As System.Windows.Forms.DataGridView
    Friend WithEvents btnFiltruj As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents txtFiltruj As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents sc As System.Windows.Forms.SplitContainer
    Friend WithEvents tv As System.Windows.Forms.TreeView
    Friend WithEvents tsGrupy As System.Windows.Forms.ToolStrip
    Friend WithEvents tsGorny As System.Windows.Forms.ToolStrip
    Friend WithEvents lblWszystkie As System.Windows.Forms.ToolStripLabel
    Friend WithEvents btnZapisz As System.Windows.Forms.Button
    Friend WithEvents btnZamknij As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents btnFiltr As System.Windows.Forms.Button
End Class
