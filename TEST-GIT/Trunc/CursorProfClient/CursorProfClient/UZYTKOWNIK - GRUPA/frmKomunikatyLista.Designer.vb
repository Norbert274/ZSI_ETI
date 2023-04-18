<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmKomunikatyLista
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
        Me.rtbKomunikat = New System.Windows.Forms.RichTextBox()
        Me.btnNowy = New System.Windows.Forms.Button()
        Me.btnEdytuj = New System.Windows.Forms.Button()
        Me.btnUsun = New System.Windows.Forms.Button()
        Me.dgvGrupy = New System.Windows.Forms.DataGridView()
        Me.btnPrzypiszKomunikatDoGrup = New System.Windows.Forms.Button()
        Me.btnZamknij = New System.Windows.Forms.Button()
        Me.dgvKomunikaty = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        CType(Me.dgvGrupy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvKomunikaty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'rtbKomunikat
        '
        Me.rtbKomunikat.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rtbKomunikat.BackColor = System.Drawing.Color.White
        Me.rtbKomunikat.Location = New System.Drawing.Point(8, 19)
        Me.rtbKomunikat.MaximumSize = New System.Drawing.Size(675, 2065)
        Me.rtbKomunikat.Name = "rtbKomunikat"
        Me.rtbKomunikat.ReadOnly = True
        Me.rtbKomunikat.Size = New System.Drawing.Size(544, 207)
        Me.rtbKomunikat.TabIndex = 1
        Me.rtbKomunikat.Text = ""
        '
        'btnNowy
        '
        Me.btnNowy.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNowy.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnNowy.ForeColor = System.Drawing.Color.White
        Me.btnNowy.Location = New System.Drawing.Point(608, 3)
        Me.btnNowy.Name = "btnNowy"
        Me.btnNowy.Size = New System.Drawing.Size(98, 23)
        Me.btnNowy.TabIndex = 1
        Me.btnNowy.Text = "Nowy komunikat"
        Me.ToolTip1.SetToolTip(Me.btnNowy, "Utworzenie nowego komunikatu")
        Me.btnNowy.UseVisualStyleBackColor = False
        '
        'btnEdytuj
        '
        Me.btnEdytuj.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnEdytuj.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnEdytuj.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnEdytuj.ForeColor = System.Drawing.Color.White
        Me.btnEdytuj.Location = New System.Drawing.Point(712, 3)
        Me.btnEdytuj.Name = "btnEdytuj"
        Me.btnEdytuj.Size = New System.Drawing.Size(75, 23)
        Me.btnEdytuj.TabIndex = 2
        Me.btnEdytuj.Text = "Edytuj"
        Me.ToolTip1.SetToolTip(Me.btnEdytuj, "Edycja wybranego komunikatu")
        Me.btnEdytuj.UseVisualStyleBackColor = False
        '
        'btnUsun
        '
        Me.btnUsun.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnUsun.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnUsun.ForeColor = System.Drawing.Color.White
        Me.btnUsun.Location = New System.Drawing.Point(793, 3)
        Me.btnUsun.Name = "btnUsun"
        Me.btnUsun.Size = New System.Drawing.Size(75, 23)
        Me.btnUsun.TabIndex = 3
        Me.btnUsun.Text = "Usuń"
        Me.ToolTip1.SetToolTip(Me.btnUsun, "Usunięcie wybranego komunikatu")
        Me.btnUsun.UseVisualStyleBackColor = False
        '
        'dgvGrupy
        '
        Me.dgvGrupy.AllowUserToAddRows = False
        Me.dgvGrupy.AllowUserToDeleteRows = False
        Me.dgvGrupy.AllowUserToResizeRows = False
        Me.dgvGrupy.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvGrupy.BackgroundColor = System.Drawing.Color.White
        Me.dgvGrupy.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvGrupy.Location = New System.Drawing.Point(556, 19)
        Me.dgvGrupy.MultiSelect = False
        Me.dgvGrupy.Name = "dgvGrupy"
        Me.dgvGrupy.RowHeadersVisible = False
        Me.dgvGrupy.Size = New System.Drawing.Size(316, 207)
        Me.dgvGrupy.TabIndex = 2
        '
        'btnPrzypiszKomunikatDoGrup
        '
        Me.btnPrzypiszKomunikatDoGrup.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPrzypiszKomunikatDoGrup.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnPrzypiszKomunikatDoGrup.ForeColor = System.Drawing.Color.White
        Me.btnPrzypiszKomunikatDoGrup.Location = New System.Drawing.Point(572, 471)
        Me.btnPrzypiszKomunikatDoGrup.Name = "btnPrzypiszKomunikatDoGrup"
        Me.btnPrzypiszKomunikatDoGrup.Size = New System.Drawing.Size(204, 26)
        Me.btnPrzypiszKomunikatDoGrup.TabIndex = 1
        Me.btnPrzypiszKomunikatDoGrup.Text = "Przypisz komunikat wybranym grupom"
        Me.ToolTip1.SetToolTip(Me.btnPrzypiszKomunikatDoGrup, "Przypisanie komunikatu wybranym grupom")
        Me.btnPrzypiszKomunikatDoGrup.UseVisualStyleBackColor = False
        '
        'btnZamknij
        '
        Me.btnZamknij.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnZamknij.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnZamknij.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnZamknij.ForeColor = System.Drawing.Color.White
        Me.btnZamknij.Location = New System.Drawing.Point(782, 471)
        Me.btnZamknij.Name = "btnZamknij"
        Me.btnZamknij.Size = New System.Drawing.Size(97, 26)
        Me.btnZamknij.TabIndex = 2
        Me.btnZamknij.Text = "Zamknij"
        Me.ToolTip1.SetToolTip(Me.btnZamknij, "Zamknięcie okna")
        Me.btnZamknij.UseVisualStyleBackColor = False
        '
        'dgvKomunikaty
        '
        Me.dgvKomunikaty.AllowUserToAddRows = False
        Me.dgvKomunikaty.AllowUserToDeleteRows = False
        Me.dgvKomunikaty.AllowUserToResizeRows = False
        Me.dgvKomunikaty.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvKomunikaty.BackgroundColor = System.Drawing.Color.White
        Me.dgvKomunikaty.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvKomunikaty.Location = New System.Drawing.Point(3, 27)
        Me.dgvKomunikaty.MultiSelect = False
        Me.dgvKomunikaty.Name = "dgvKomunikaty"
        Me.dgvKomunikaty.ReadOnly = True
        Me.dgvKomunikaty.RowHeadersVisible = False
        Me.dgvKomunikaty.Size = New System.Drawing.Size(865, 188)
        Me.dgvKomunikaty.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(4, 4)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(95, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Treść komunikatu:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(3, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(98, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Lista komunikatów:"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.BackColor = System.Drawing.Color.Gray
        Me.SplitContainer1.Location = New System.Drawing.Point(4, 3)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.BackColor = System.Drawing.Color.White
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnUsun)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dgvKomunikaty)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnEdytuj)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnNowy)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.BackColor = System.Drawing.Color.White
        Me.SplitContainer1.Panel2.Controls.Add(Me.dgvGrupy)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rtbKomunikat)
        Me.SplitContainer1.Size = New System.Drawing.Size(875, 463)
        Me.SplitContainer1.SplitterDistance = 234
        Me.SplitContainer1.SplitterWidth = 3
        Me.SplitContainer1.TabIndex = 0
        '
        'ToolTip1
        '
        Me.ToolTip1.AutoPopDelay = 10000
        Me.ToolTip1.InitialDelay = 500
        Me.ToolTip1.ReshowDelay = 500
        '
        'frmKomunikatyLista
        '
        Me.AcceptButton = Me.btnPrzypiszKomunikatDoGrup
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnZamknij
        Me.ClientSize = New System.Drawing.Size(884, 502)
        Me.Controls.Add(Me.btnPrzypiszKomunikatDoGrup)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.btnZamknij)
        Me.MinimumSize = New System.Drawing.Size(800, 400)
        Me.Name = "frmKomunikatyLista"
        Me.Text = "Zarządzanie komunikatami"
        CType(Me.dgvGrupy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvKomunikaty, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents rtbKomunikat As System.Windows.Forms.RichTextBox
    Friend WithEvents btnNowy As System.Windows.Forms.Button
    Friend WithEvents btnEdytuj As System.Windows.Forms.Button
    Friend WithEvents btnUsun As System.Windows.Forms.Button
    Friend WithEvents dgvGrupy As System.Windows.Forms.DataGridView
    Friend WithEvents btnPrzypiszKomunikatDoGrup As System.Windows.Forms.Button
    Friend WithEvents btnZamknij As System.Windows.Forms.Button
    Friend WithEvents dgvKomunikaty As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class
