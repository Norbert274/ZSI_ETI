<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmWyszukiwanieAdresu
    Inherits CursorProfClient.frmBase

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
        Dim StyleFormatCondition1 As DevExpress.XtraGrid.StyleFormatCondition = New DevExpress.XtraGrid.StyleFormatCondition()
        Dim StyleFormatCondition2 As DevExpress.XtraGrid.StyleFormatCondition = New DevExpress.XtraGrid.StyleFormatCondition()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmWyszukiwanieAdresu))
        Me.DGC_Lista = New DevExpress.XtraGrid.GridControl()
        Me.DGV_Lista = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.SplitContainerMain = New System.Windows.Forms.SplitContainer()
        Me.btnShowHideFindPanel = New DevExpress.XtraEditors.SimpleButton()
        Me.btnZnajdz = New DevExpress.XtraEditors.SimpleButton()
        Me.lblMasterRowCount = New System.Windows.Forms.Label()
        Me.txtTekstFiltru = New DevExpress.XtraEditors.TextEdit()
        Me.lblTekstFiltru = New System.Windows.Forms.Label()
        Me.barManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.bar1 = New DevExpress.XtraBars.Bar()
        Me.bbiPierwszaStrona = New DevExpress.XtraBars.BarButtonItem()
        Me.bbiPoprzedniaStrona = New DevExpress.XtraBars.BarButtonItem()
        Me.bbiStrona = New DevExpress.XtraBars.BarStaticItem()
        Me.bbiStronaNumer = New DevExpress.XtraBars.BarEditItem()
        Me.RepositoryItemTextEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.bbiStronaZ = New DevExpress.XtraBars.BarStaticItem()
        Me.bbiStronaLiczba = New DevExpress.XtraBars.BarEditItem()
        Me.RepositoryItemTextEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.bbiNastepnaStrona = New DevExpress.XtraBars.BarButtonItem()
        Me.bbiOstatniaStrona = New DevExpress.XtraBars.BarButtonItem()
        Me.bbiPageSize = New DevExpress.XtraBars.BarStaticItem()
        Me.bbiStronaIloscLista = New DevExpress.XtraBars.BarEditItem()
        Me.cmStronaIloscLista = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        Me.bbiLblTotal = New DevExpress.XtraBars.BarStaticItem()
        Me.bbiTotalLiczbaWierszy = New DevExpress.XtraBars.BarEditItem()
        Me.RepositoryItemTextEdit3 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.bbiSortPo = New DevExpress.XtraBars.BarStaticItem()
        Me.bbiSortPoLista = New DevExpress.XtraBars.BarEditItem()
        Me.cmbSortPo = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        CType(Me.DGC_Lista, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGV_Lista, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainerMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerMain.Panel1.SuspendLayout()
        Me.SplitContainerMain.Panel2.SuspendLayout()
        Me.SplitContainerMain.SuspendLayout()
        CType(Me.txtTekstFiltru.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.barManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmStronaIloscLista, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbSortPo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DGC_Lista
        '
        Me.DGC_Lista.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGC_Lista.Location = New System.Drawing.Point(0, 0)
        Me.DGC_Lista.MainView = Me.DGV_Lista
        Me.DGC_Lista.Name = "DGC_Lista"
        Me.DGC_Lista.Size = New System.Drawing.Size(794, 315)
        Me.DGC_Lista.TabIndex = 1
        Me.DGC_Lista.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.DGV_Lista})
        '
        'DGV_Lista
        '
        Me.DGV_Lista.Appearance.EvenRow.BackColor = System.Drawing.Color.WhiteSmoke
        Me.DGV_Lista.Appearance.EvenRow.Options.UseBackColor = True
        Me.DGV_Lista.Appearance.OddRow.BackColor = System.Drawing.Color.Transparent
        Me.DGV_Lista.Appearance.OddRow.Options.UseBackColor = True
        Me.DGV_Lista.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        StyleFormatCondition1.Appearance.Font = New System.Drawing.Font("Tempus Sans ITC", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        StyleFormatCondition1.Appearance.ForeColor = System.Drawing.Color.Red
        StyleFormatCondition1.Appearance.Options.UseFont = True
        StyleFormatCondition1.Appearance.Options.UseForeColor = True
        StyleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Expression
        StyleFormatCondition1.Expression = "[HASLO]  == 'BRAK'"
        StyleFormatCondition2.Condition = DevExpress.XtraGrid.FormatConditionEnum.Expression
        StyleFormatCondition2.Expression = "GetDay([DATA_DO]) == \'10'\"
        Me.DGV_Lista.FormatConditions.AddRange(New DevExpress.XtraGrid.StyleFormatCondition() {StyleFormatCondition1, StyleFormatCondition2})
        Me.DGV_Lista.GridControl = Me.DGC_Lista
        Me.DGV_Lista.Name = "DGV_Lista"
        Me.DGV_Lista.OptionsBehavior.Editable = False
        Me.DGV_Lista.OptionsBehavior.ReadOnly = True
        Me.DGV_Lista.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.DGV_Lista.OptionsView.ColumnAutoWidth = False
        Me.DGV_Lista.OptionsView.EnableAppearanceEvenRow = True
        Me.DGV_Lista.OptionsView.EnableAppearanceOddRow = True
        Me.DGV_Lista.OptionsView.ShowGroupPanel = False
        Me.DGV_Lista.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always
        '
        'SplitContainerMain
        '
        Me.SplitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainerMain.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerMain.Name = "SplitContainerMain"
        Me.SplitContainerMain.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainerMain.Panel1
        '
        Me.SplitContainerMain.Panel1.BackColor = System.Drawing.Color.White
        Me.SplitContainerMain.Panel1.Controls.Add(Me.btnShowHideFindPanel)
        Me.SplitContainerMain.Panel1.Controls.Add(Me.btnZnajdz)
        Me.SplitContainerMain.Panel1.Controls.Add(Me.lblMasterRowCount)
        Me.SplitContainerMain.Panel1.Controls.Add(Me.txtTekstFiltru)
        Me.SplitContainerMain.Panel1.Controls.Add(Me.lblTekstFiltru)
        Me.SplitContainerMain.Panel1MinSize = 96
        '
        'SplitContainerMain.Panel2
        '
        Me.SplitContainerMain.Panel2.Controls.Add(Me.DGC_Lista)
        Me.SplitContainerMain.Size = New System.Drawing.Size(794, 415)
        Me.SplitContainerMain.SplitterDistance = 96
        Me.SplitContainerMain.TabIndex = 2
        '
        'btnShowHideFindPanel
        '
        Me.btnShowHideFindPanel.Appearance.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnShowHideFindPanel.Appearance.ForeColor = System.Drawing.Color.White
        Me.btnShowHideFindPanel.Appearance.Options.UseBackColor = True
        Me.btnShowHideFindPanel.Appearance.Options.UseForeColor = True
        Me.btnShowHideFindPanel.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat
        Me.btnShowHideFindPanel.Location = New System.Drawing.Point(12, 70)
        Me.btnShowHideFindPanel.Name = "btnShowHideFindPanel"
        Me.btnShowHideFindPanel.Size = New System.Drawing.Size(134, 23)
        Me.btnShowHideFindPanel.TabIndex = 57
        Me.btnShowHideFindPanel.Text = "Poka¿ Panel Wyszukiwania"
        Me.btnShowHideFindPanel.ToolTip = "Panel Wyszukiania"
        '
        'btnZnajdz
        '
        Me.btnZnajdz.Appearance.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnZnajdz.Appearance.BackColor2 = System.Drawing.Color.DodgerBlue
        Me.btnZnajdz.Appearance.BorderColor = System.Drawing.Color.DodgerBlue
        Me.btnZnajdz.Appearance.ForeColor = System.Drawing.Color.White
        Me.btnZnajdz.Appearance.Options.UseBackColor = True
        Me.btnZnajdz.Appearance.Options.UseBorderColor = True
        Me.btnZnajdz.Appearance.Options.UseForeColor = True
        Me.btnZnajdz.Location = New System.Drawing.Point(308, 11)
        Me.btnZnajdz.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat
        Me.btnZnajdz.LookAndFeel.UseDefaultLookAndFeel = False
        Me.btnZnajdz.Name = "btnZnajdz"
        Me.btnZnajdz.Size = New System.Drawing.Size(148, 22)
        Me.btnZnajdz.TabIndex = 14
        Me.btnZnajdz.Text = "Wyszukaj"
        '
        'lblMasterRowCount
        '
        Me.lblMasterRowCount.AutoSize = True
        Me.lblMasterRowCount.ForeColor = System.Drawing.Color.Black
        Me.lblMasterRowCount.Location = New System.Drawing.Point(13, 46)
        Me.lblMasterRowCount.Name = "lblMasterRowCount"
        Me.lblMasterRowCount.Size = New System.Drawing.Size(68, 13)
        Me.lblMasterRowCount.TabIndex = 58
        Me.lblMasterRowCount.Text = "Liczba:       0"
        Me.lblMasterRowCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtTekstFiltru
        '
        Me.txtTekstFiltru.Location = New System.Drawing.Point(99, 11)
        Me.txtTekstFiltru.Name = "txtTekstFiltru"
        Me.txtTekstFiltru.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtTekstFiltru.Properties.Appearance.Options.UseFont = True
        Me.txtTekstFiltru.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTekstFiltru.Size = New System.Drawing.Size(191, 22)
        Me.txtTekstFiltru.TabIndex = 13
        '
        'lblTekstFiltru
        '
        Me.lblTekstFiltru.AutoSize = True
        Me.lblTekstFiltru.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblTekstFiltru.ForeColor = System.Drawing.Color.Black
        Me.lblTekstFiltru.Location = New System.Drawing.Point(12, 14)
        Me.lblTekstFiltru.Name = "lblTekstFiltru"
        Me.lblTekstFiltru.Size = New System.Drawing.Size(55, 16)
        Me.lblTekstFiltru.TabIndex = 10
        Me.lblTekstFiltru.Text = "Szukaj:"
        '
        'barManager1
        '
        Me.barManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.bar1})
        Me.barManager1.DockControls.Add(Me.barDockControlTop)
        Me.barManager1.DockControls.Add(Me.barDockControlBottom)
        Me.barManager1.DockControls.Add(Me.barDockControlLeft)
        Me.barManager1.DockControls.Add(Me.barDockControlRight)
        Me.barManager1.Form = Me
        Me.barManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.bbiPoprzedniaStrona, Me.bbiNastepnaStrona, Me.bbiOstatniaStrona, Me.bbiPierwszaStrona, Me.bbiStrona, Me.bbiStronaNumer, Me.bbiStronaZ, Me.bbiStronaLiczba, Me.bbiPageSize, Me.bbiSortPo, Me.bbiStronaIloscLista, Me.bbiSortPoLista, Me.bbiLblTotal, Me.bbiTotalLiczbaWierszy})
        Me.barManager1.MaxItemId = 17
        Me.barManager1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemTextEdit1, Me.RepositoryItemTextEdit2, Me.cmbSortPo, Me.cmStronaIloscLista, Me.RepositoryItemTextEdit3})
        '
        'bar1
        '
        Me.bar1.BarName = "Tools"
        Me.bar1.DockCol = 0
        Me.bar1.DockRow = 0
        Me.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom
        Me.bar1.FloatLocation = New System.Drawing.Point(616, 230)
        Me.bar1.FloatSize = New System.Drawing.Size(793, 29)
        Me.bar1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.bbiPierwszaStrona), New DevExpress.XtraBars.LinkPersistInfo(Me.bbiPoprzedniaStrona), New DevExpress.XtraBars.LinkPersistInfo(Me.bbiStrona), New DevExpress.XtraBars.LinkPersistInfo(Me.bbiStronaNumer), New DevExpress.XtraBars.LinkPersistInfo(Me.bbiStronaZ), New DevExpress.XtraBars.LinkPersistInfo(Me.bbiStronaLiczba), New DevExpress.XtraBars.LinkPersistInfo(Me.bbiNastepnaStrona), New DevExpress.XtraBars.LinkPersistInfo(Me.bbiOstatniaStrona), New DevExpress.XtraBars.LinkPersistInfo(Me.bbiPageSize), New DevExpress.XtraBars.LinkPersistInfo(Me.bbiStronaIloscLista), New DevExpress.XtraBars.LinkPersistInfo(Me.bbiLblTotal), New DevExpress.XtraBars.LinkPersistInfo(Me.bbiTotalLiczbaWierszy), New DevExpress.XtraBars.LinkPersistInfo(Me.bbiSortPo), New DevExpress.XtraBars.LinkPersistInfo(Me.bbiSortPoLista)})
        Me.bar1.OptionsBar.AllowQuickCustomization = False
        Me.bar1.OptionsBar.DisableClose = True
        Me.bar1.OptionsBar.DisableCustomization = True
        Me.bar1.OptionsBar.DrawDragBorder = False
        Me.bar1.OptionsBar.RotateWhenVertical = False
        Me.bar1.Text = "Tools"
        '
        'bbiPierwszaStrona
        '
        Me.bbiPierwszaStrona.Border = DevExpress.XtraEditors.Controls.BorderStyles.[Default]
        Me.bbiPierwszaStrona.Caption = "|<"
        Me.bbiPierwszaStrona.Id = 4
        Me.bbiPierwszaStrona.Name = "bbiPierwszaStrona"
        '
        'bbiPoprzedniaStrona
        '
        Me.bbiPoprzedniaStrona.Border = DevExpress.XtraEditors.Controls.BorderStyles.[Default]
        Me.bbiPoprzedniaStrona.Caption = "<"
        Me.bbiPoprzedniaStrona.Id = 0
        Me.bbiPoprzedniaStrona.Name = "bbiPoprzedniaStrona"
        '
        'bbiStrona
        '
        Me.bbiStrona.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.bbiStrona.Caption = "Strona:"
        Me.bbiStrona.Id = 5
        Me.bbiStrona.Name = "bbiStrona"
        Me.bbiStrona.TextAlignment = System.Drawing.StringAlignment.Near
        '
        'bbiStronaNumer
        '
        Me.bbiStronaNumer.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right
        Me.bbiStronaNumer.Edit = Me.RepositoryItemTextEdit1
        Me.bbiStronaNumer.Id = 6
        Me.bbiStronaNumer.Name = "bbiStronaNumer"
        Me.bbiStronaNumer.Width = 35
        '
        'RepositoryItemTextEdit1
        '
        Me.RepositoryItemTextEdit1.AutoHeight = False
        Me.RepositoryItemTextEdit1.Mask.EditMask = "0|\d{1,3}"
        Me.RepositoryItemTextEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.RepositoryItemTextEdit1.Name = "RepositoryItemTextEdit1"
        '
        'bbiStronaZ
        '
        Me.bbiStronaZ.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.bbiStronaZ.Caption = "z"
        Me.bbiStronaZ.Id = 7
        Me.bbiStronaZ.Name = "bbiStronaZ"
        Me.bbiStronaZ.TextAlignment = System.Drawing.StringAlignment.Near
        '
        'bbiStronaLiczba
        '
        Me.bbiStronaLiczba.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right
        Me.bbiStronaLiczba.Border = DevExpress.XtraEditors.Controls.BorderStyles.[Default]
        Me.bbiStronaLiczba.Edit = Me.RepositoryItemTextEdit2
        Me.bbiStronaLiczba.Id = 8
        Me.bbiStronaLiczba.Name = "bbiStronaLiczba"
        Me.bbiStronaLiczba.Width = 35
        '
        'RepositoryItemTextEdit2
        '
        Me.RepositoryItemTextEdit2.AutoHeight = False
        Me.RepositoryItemTextEdit2.Mask.EditMask = "n0"
        Me.RepositoryItemTextEdit2.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.RepositoryItemTextEdit2.Name = "RepositoryItemTextEdit2"
        Me.RepositoryItemTextEdit2.ReadOnly = True
        '
        'bbiNastepnaStrona
        '
        Me.bbiNastepnaStrona.Border = DevExpress.XtraEditors.Controls.BorderStyles.[Default]
        Me.bbiNastepnaStrona.Caption = ">"
        Me.bbiNastepnaStrona.Id = 1
        Me.bbiNastepnaStrona.Name = "bbiNastepnaStrona"
        '
        'bbiOstatniaStrona
        '
        Me.bbiOstatniaStrona.Border = DevExpress.XtraEditors.Controls.BorderStyles.[Default]
        Me.bbiOstatniaStrona.Caption = ">|"
        Me.bbiOstatniaStrona.Id = 2
        Me.bbiOstatniaStrona.Name = "bbiOstatniaStrona"
        '
        'bbiPageSize
        '
        Me.bbiPageSize.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.bbiPageSize.Caption = "Rekordów na stronie:"
        Me.bbiPageSize.Id = 10
        Me.bbiPageSize.Name = "bbiPageSize"
        Me.bbiPageSize.TextAlignment = System.Drawing.StringAlignment.Near
        '
        'bbiStronaIloscLista
        '
        Me.bbiStronaIloscLista.Edit = Me.cmStronaIloscLista
        Me.bbiStronaIloscLista.Id = 13
        Me.bbiStronaIloscLista.Name = "bbiStronaIloscLista"
        Me.bbiStronaIloscLista.Width = 75
        '
        'cmStronaIloscLista
        '
        Me.cmStronaIloscLista.AutoHeight = False
        Me.cmStronaIloscLista.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmStronaIloscLista.Items.AddRange(New Object() {"Wszystkie", "          10", "          50", "        100", "      1000", "    10000"})
        Me.cmStronaIloscLista.Name = "cmStronaIloscLista"
        Me.cmStronaIloscLista.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        '
        'bbiLblTotal
        '
        Me.bbiLblTotal.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.bbiLblTotal.Caption = "Total:"
        Me.bbiLblTotal.Id = 15
        Me.bbiLblTotal.Name = "bbiLblTotal"
        Me.bbiLblTotal.TextAlignment = System.Drawing.StringAlignment.Near
        '
        'bbiTotalLiczbaWierszy
        '
        Me.bbiTotalLiczbaWierszy.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right
        Me.bbiTotalLiczbaWierszy.Edit = Me.RepositoryItemTextEdit3
        Me.bbiTotalLiczbaWierszy.Id = 16
        Me.bbiTotalLiczbaWierszy.Name = "bbiTotalLiczbaWierszy"
        Me.bbiTotalLiczbaWierszy.Width = 75
        '
        'RepositoryItemTextEdit3
        '
        Me.RepositoryItemTextEdit3.AutoHeight = False
        Me.RepositoryItemTextEdit3.Name = "RepositoryItemTextEdit3"
        Me.RepositoryItemTextEdit3.ReadOnly = True
        '
        'bbiSortPo
        '
        Me.bbiSortPo.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.bbiSortPo.Caption = "Sortuj po:"
        Me.bbiSortPo.Id = 11
        Me.bbiSortPo.Name = "bbiSortPo"
        Me.bbiSortPo.TextAlignment = System.Drawing.StringAlignment.Near
        '
        'bbiSortPoLista
        '
        Me.bbiSortPoLista.Edit = Me.cmbSortPo
        Me.bbiSortPoLista.Id = 14
        Me.bbiSortPoLista.Name = "bbiSortPoLista"
        Me.bbiSortPoLista.Width = 120
        '
        'cmbSortPo
        '
        Me.cmbSortPo.AutoHeight = False
        Me.cmbSortPo.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbSortPo.Name = "cmbSortPo"
        Me.cmbSortPo.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Size = New System.Drawing.Size(794, 0)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 415)
        Me.barDockControlBottom.Size = New System.Drawing.Size(794, 29)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 415)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(794, 0)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 415)
        '
        'frmWyszukiwanieAdresu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(794, 444)
        Me.Controls.Add(Me.SplitContainerMain)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmWyszukiwanieAdresu"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Wyszukiwanie adresu"
        CType(Me.DGC_Lista, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGV_Lista, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerMain.Panel1.ResumeLayout(False)
        Me.SplitContainerMain.Panel1.PerformLayout()
        Me.SplitContainerMain.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerMain.ResumeLayout(False)
        CType(Me.txtTekstFiltru.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.barManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmStronaIloscLista, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEdit3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbSortPo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DGC_Lista As DevExpress.XtraGrid.GridControl
    Friend WithEvents DGV_Lista As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents SplitContainerMain As System.Windows.Forms.SplitContainer
    Friend WithEvents btnZnajdz As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtTekstFiltru As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblTekstFiltru As System.Windows.Forms.Label
    Friend WithEvents btnShowHideFindPanel As DevExpress.XtraEditors.SimpleButton
    Private WithEvents lblMasterRowCount As System.Windows.Forms.Label
    Private WithEvents barManager1 As DevExpress.XtraBars.BarManager
    Private WithEvents bar1 As DevExpress.XtraBars.Bar
    Private WithEvents bbiPoprzedniaStrona As DevExpress.XtraBars.BarButtonItem
    Private WithEvents bbiNastepnaStrona As DevExpress.XtraBars.BarButtonItem
    Private WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Private WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Private WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Private WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents bbiOstatniaStrona As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents bbiPierwszaStrona As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents bbiStrona As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents bbiStronaNumer As DevExpress.XtraBars.BarEditItem
    Friend WithEvents RepositoryItemTextEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents bbiStronaZ As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents bbiStronaLiczba As DevExpress.XtraBars.BarEditItem
    Friend WithEvents RepositoryItemTextEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents bbiPageSize As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents bbiSortPo As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents bbiStronaIloscLista As DevExpress.XtraBars.BarEditItem
    Friend WithEvents cmStronaIloscLista As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
    Friend WithEvents bbiSortPoLista As DevExpress.XtraBars.BarEditItem
    Friend WithEvents cmbSortPo As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
    Friend WithEvents bbiLblTotal As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents bbiTotalLiczbaWierszy As DevExpress.XtraBars.BarEditItem
    Friend WithEvents RepositoryItemTextEdit3 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit

End Class
