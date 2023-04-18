<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPodzialGrupa
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPodzialGrupa))
        Me.tsListaDolny = New System.Windows.Forms.ToolStrip()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnZapisz = New System.Windows.Forms.ToolStripButton()
        Me.scPrzelozeniTowar = New System.Windows.Forms.SplitContainer()
        Me.treeListPodzial = New DevExpress.XtraTreeList.TreeList()
        Me.btnZamknij = New System.Windows.Forms.Button()
        Me.tsListaDolny.SuspendLayout()
        CType(Me.scPrzelozeniTowar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scPrzelozeniTowar.Panel2.SuspendLayout()
        Me.scPrzelozeniTowar.SuspendLayout()
        CType(Me.treeListPodzial, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tsListaDolny
        '
        Me.tsListaDolny.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.tsListaDolny.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripSeparator1, Me.btnZapisz})
        Me.tsListaDolny.Location = New System.Drawing.Point(0, 419)
        Me.tsListaDolny.Name = "tsListaDolny"
        Me.tsListaDolny.Size = New System.Drawing.Size(777, 25)
        Me.tsListaDolny.TabIndex = 1
        Me.tsListaDolny.Text = "ToolStrip1"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'btnZapisz
        '
        Me.btnZapisz.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.btnZapisz.Image = CType(resources.GetObject("btnZapisz.Image"), System.Drawing.Image)
        Me.btnZapisz.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnZapisz.Name = "btnZapisz"
        Me.btnZapisz.Size = New System.Drawing.Size(101, 22)
        Me.btnZapisz.Text = "Zapisz podział"
        '
        'scPrzelozeniTowar
        '
        Me.scPrzelozeniTowar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scPrzelozeniTowar.Location = New System.Drawing.Point(0, 0)
        Me.scPrzelozeniTowar.Name = "scPrzelozeniTowar"
        Me.scPrzelozeniTowar.Panel1Collapsed = True
        Me.scPrzelozeniTowar.Panel1MinSize = 200
        '
        'scPrzelozeniTowar.Panel2
        '
        Me.scPrzelozeniTowar.Panel2.Controls.Add(Me.treeListPodzial)
        Me.scPrzelozeniTowar.Panel2.Controls.Add(Me.tsListaDolny)
        Me.scPrzelozeniTowar.Panel2.Controls.Add(Me.btnZamknij)
        Me.scPrzelozeniTowar.Size = New System.Drawing.Size(777, 444)
        Me.scPrzelozeniTowar.SplitterDistance = 200
        Me.scPrzelozeniTowar.TabIndex = 12
        '
        'treeListPodzial
        '
        Me.treeListPodzial.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.treeListPodzial.Appearance.Empty.BackColor2 = System.Drawing.Color.White
        Me.treeListPodzial.Appearance.Empty.Options.UseBackColor = True
        Me.treeListPodzial.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(247, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.treeListPodzial.Appearance.EvenRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(247, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.treeListPodzial.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black
        Me.treeListPodzial.Appearance.EvenRow.Options.UseBackColor = True
        Me.treeListPodzial.Appearance.EvenRow.Options.UseBorderColor = True
        Me.treeListPodzial.Appearance.EvenRow.Options.UseForeColor = True
        Me.treeListPodzial.Appearance.FocusedCell.BackColor = System.Drawing.Color.White
        Me.treeListPodzial.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black
        Me.treeListPodzial.Appearance.FocusedCell.Options.UseBackColor = True
        Me.treeListPodzial.Appearance.FocusedCell.Options.UseForeColor = True
        Me.treeListPodzial.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(189, Byte), Integer))
        Me.treeListPodzial.Appearance.FocusedRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(139, Byte), Integer), CType(CType(206, Byte), Integer))
        Me.treeListPodzial.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White
        Me.treeListPodzial.Appearance.FocusedRow.Options.UseBackColor = True
        Me.treeListPodzial.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.treeListPodzial.Appearance.FocusedRow.Options.UseForeColor = True
        Me.treeListPodzial.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(184, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.treeListPodzial.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(184, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.treeListPodzial.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black
        Me.treeListPodzial.Appearance.FooterPanel.Options.UseBackColor = True
        Me.treeListPodzial.Appearance.FooterPanel.Options.UseBorderColor = True
        Me.treeListPodzial.Appearance.FooterPanel.Options.UseForeColor = True
        Me.treeListPodzial.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(184, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.treeListPodzial.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(184, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.treeListPodzial.Appearance.GroupButton.Options.UseBackColor = True
        Me.treeListPodzial.Appearance.GroupButton.Options.UseBorderColor = True
        Me.treeListPodzial.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.treeListPodzial.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.treeListPodzial.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black
        Me.treeListPodzial.Appearance.GroupFooter.Options.UseBackColor = True
        Me.treeListPodzial.Appearance.GroupFooter.Options.UseBorderColor = True
        Me.treeListPodzial.Appearance.GroupFooter.Options.UseForeColor = True
        Me.treeListPodzial.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(139, Byte), Integer), CType(CType(201, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.treeListPodzial.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(139, Byte), Integer), CType(CType(201, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.treeListPodzial.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black
        Me.treeListPodzial.Appearance.HeaderPanel.Options.UseBackColor = True
        Me.treeListPodzial.Appearance.HeaderPanel.Options.UseBorderColor = True
        Me.treeListPodzial.Appearance.HeaderPanel.Options.UseForeColor = True
        Me.treeListPodzial.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(105, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.treeListPodzial.Appearance.HideSelectionRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(83, Byte), Integer), CType(CType(155, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.treeListPodzial.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.treeListPodzial.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.treeListPodzial.Appearance.HideSelectionRow.Options.UseBorderColor = True
        Me.treeListPodzial.Appearance.HideSelectionRow.Options.UseForeColor = True
        Me.treeListPodzial.Appearance.HorzLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(184, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.treeListPodzial.Appearance.HorzLine.Options.UseBackColor = True
        Me.treeListPodzial.Appearance.OddRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.treeListPodzial.Appearance.OddRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.treeListPodzial.Appearance.OddRow.ForeColor = System.Drawing.Color.Black
        Me.treeListPodzial.Appearance.OddRow.Options.UseBackColor = True
        Me.treeListPodzial.Appearance.OddRow.Options.UseBorderColor = True
        Me.treeListPodzial.Appearance.OddRow.Options.UseForeColor = True
        Me.treeListPodzial.Appearance.Preview.Font = New System.Drawing.Font("Verdana", 7.5!)
        Me.treeListPodzial.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(CType(CType(83, Byte), Integer), CType(CType(155, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.treeListPodzial.Appearance.Preview.Options.UseFont = True
        Me.treeListPodzial.Appearance.Preview.Options.UseForeColor = True
        Me.treeListPodzial.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(CType(CType(247, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.treeListPodzial.Appearance.Row.ForeColor = System.Drawing.Color.Black
        Me.treeListPodzial.Appearance.Row.Options.UseBackColor = True
        Me.treeListPodzial.Appearance.Row.Options.UseForeColor = True
        Me.treeListPodzial.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(83, Byte), Integer), CType(CType(155, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.treeListPodzial.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White
        Me.treeListPodzial.Appearance.SelectedRow.Options.UseBackColor = True
        Me.treeListPodzial.Appearance.SelectedRow.Options.UseForeColor = True
        Me.treeListPodzial.Appearance.TreeLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(133, Byte), Integer), CType(CType(195, Byte), Integer))
        Me.treeListPodzial.Appearance.TreeLine.Options.UseBackColor = True
        Me.treeListPodzial.Appearance.VertLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(184, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.treeListPodzial.Appearance.VertLine.Options.UseBackColor = True
        Me.treeListPodzial.ColumnPanelRowHeight = 62
        Me.treeListPodzial.Dock = System.Windows.Forms.DockStyle.Fill
        Me.treeListPodzial.Location = New System.Drawing.Point(0, 0)
        Me.treeListPodzial.Name = "treeListPodzial"
        Me.treeListPodzial.OptionsView.AutoWidth = False
        Me.treeListPodzial.OptionsView.EnableAppearanceEvenRow = True
        Me.treeListPodzial.OptionsView.EnableAppearanceOddRow = True
        Me.treeListPodzial.OptionsView.ShowAutoFilterRow = True
        Me.treeListPodzial.Size = New System.Drawing.Size(777, 419)
        Me.treeListPodzial.TabIndex = 25
        '
        'btnZamknij
        '
        Me.btnZamknij.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnZamknij.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnZamknij.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnZamknij.ForeColor = System.Drawing.Color.White
        Me.btnZamknij.Location = New System.Drawing.Point(3, 420)
        Me.btnZamknij.Name = "btnZamknij"
        Me.btnZamknij.Size = New System.Drawing.Size(75, 23)
        Me.btnZamknij.TabIndex = 5
        Me.btnZamknij.Text = "Zamknij"
        Me.btnZamknij.UseVisualStyleBackColor = False
        '
        'frmPodzialGrupaNew
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnZamknij
        Me.ClientSize = New System.Drawing.Size(777, 444)
        Me.Controls.Add(Me.scPrzelozeniTowar)
        Me.Name = "frmPodzialGrupaNew"
        Me.Text = "Podziel towar"
        Me.tsListaDolny.ResumeLayout(False)
        Me.tsListaDolny.PerformLayout()
        Me.scPrzelozeniTowar.Panel2.ResumeLayout(False)
        Me.scPrzelozeniTowar.Panel2.PerformLayout()
        CType(Me.scPrzelozeniTowar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scPrzelozeniTowar.ResumeLayout(False)
        CType(Me.treeListPodzial, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tsListaDolny As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnZapisz As System.Windows.Forms.ToolStripButton
    Friend WithEvents scPrzelozeniTowar As System.Windows.Forms.SplitContainer
    Friend WithEvents btnZamknij As System.Windows.Forms.Button
    Friend WithEvents treeListPodzial As DevExpress.XtraTreeList.TreeList
End Class
