<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormDTYPU
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim SideBySideBarSeriesLabel1 As DevExpress.XtraCharts.SideBySideBarSeriesLabel = New DevExpress.XtraCharts.SideBySideBarSeriesLabel()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormDTYPU))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.GridControl_PU = New DevExpress.XtraGrid.GridControl()
        Me.GridView_OverView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.DataGridView_AllTimeHistory = New System.Windows.Forms.DataGridView()
        Me.ChartControl1 = New DevExpress.XtraCharts.ChartControl()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripComboBox_PUSelector = New System.Windows.Forms.ToolStripComboBox()
        Me.ToolStripButton_Refresh = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.GridControl_PU, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView_OverView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView_AllTimeHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChartControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(SideBySideBarSeriesLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Cursor = System.Windows.Forms.Cursors.SizeWE
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 50)
        Me.SplitContainer1.Margin = New System.Windows.Forms.Padding(0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.GridControl_PU)
        Me.SplitContainer1.Panel1.Controls.Add(Me.DataGridView1)
        Me.SplitContainer1.Panel1.Cursor = System.Windows.Forms.Cursors.Default
        Me.SplitContainer1.Panel1.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.DataGridView_AllTimeHistory)
        Me.SplitContainer1.Panel2.Controls.Add(Me.ChartControl1)
        Me.SplitContainer1.Panel2.Cursor = System.Windows.Forms.Cursors.Default
        Me.SplitContainer1.Panel2.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.SplitContainer1.Size = New System.Drawing.Size(1239, 598)
        Me.SplitContainer1.SplitterDistance = 762
        Me.SplitContainer1.TabIndex = 0
        '
        'GridControl_PU
        '
        Me.GridControl_PU.Cursor = System.Windows.Forms.Cursors.Hand
        Me.GridControl_PU.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridControl_PU.Location = New System.Drawing.Point(3, 0)
        Me.GridControl_PU.LookAndFeel.SkinName = "Office 2010 Blue"
        Me.GridControl_PU.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GridControl_PU.MainView = Me.GridView_OverView
        Me.GridControl_PU.Name = "GridControl_PU"
        Me.GridControl_PU.Size = New System.Drawing.Size(759, 595)
        Me.GridControl_PU.TabIndex = 4
        Me.GridControl_PU.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView_OverView})
        '
        'GridView_OverView
        '
        Me.GridView_OverView.ActiveFilterEnabled = False
        Me.GridView_OverView.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.GridView_OverView.Appearance.FocusedRow.Options.UseBackColor = True
        Me.GridView_OverView.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.GridView_OverView.Appearance.GroupPanel.Options.UseBackColor = True
        Me.GridView_OverView.Appearance.HeaderPanel.Font = New System.Drawing.Font("Trebuchet MS", 12.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView_OverView.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView_OverView.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.GridView_OverView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridView_OverView.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Bottom
        Me.GridView_OverView.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.GridView_OverView.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.GridView_OverView.Appearance.Row.Font = New System.Drawing.Font("Trebuchet MS", 11.25!)
        Me.GridView_OverView.Appearance.Row.Options.UseBackColor = True
        Me.GridView_OverView.Appearance.Row.Options.UseFont = True
        Me.GridView_OverView.Appearance.ViewCaption.BackColor = System.Drawing.Color.OrangeRed
        Me.GridView_OverView.Appearance.ViewCaption.Font = New System.Drawing.Font("Yu Gothic UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView_OverView.Appearance.ViewCaption.Options.UseBackColor = True
        Me.GridView_OverView.Appearance.ViewCaption.Options.UseFont = True
        Me.GridView_OverView.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.GridView_OverView.ColumnPanelRowHeight = 55
        Me.GridView_OverView.DetailHeight = 200
        Me.GridView_OverView.FixedLineWidth = 1
        Me.GridView_OverView.GridControl = Me.GridControl_PU
        Me.GridView_OverView.Name = "GridView_OverView"
        Me.GridView_OverView.OptionsBehavior.AutoSelectAllInEditor = False
        Me.GridView_OverView.OptionsBehavior.Editable = False
        Me.GridView_OverView.OptionsBehavior.ReadOnly = True
        Me.GridView_OverView.OptionsCustomization.AllowColumnMoving = False
        Me.GridView_OverView.OptionsCustomization.AllowFilter = False
        Me.GridView_OverView.OptionsCustomization.AllowSort = False
        Me.GridView_OverView.OptionsFilter.AllowColumnMRUFilterList = False
        Me.GridView_OverView.OptionsFilter.AllowFilterEditor = False
        Me.GridView_OverView.OptionsFilter.AllowFilterIncrementalSearch = False
        Me.GridView_OverView.OptionsFilter.AllowMRUFilterList = False
        Me.GridView_OverView.OptionsFilter.AllowMultiSelectInCheckedFilterPopup = False
        Me.GridView_OverView.OptionsFilter.ShowAllTableValuesInCheckedFilterPopup = False
        Me.GridView_OverView.OptionsFind.AllowFindPanel = False
        Me.GridView_OverView.OptionsMenu.EnableFooterMenu = False
        Me.GridView_OverView.OptionsMenu.EnableGroupPanelMenu = False
        Me.GridView_OverView.OptionsView.ColumnAutoWidth = False
        Me.GridView_OverView.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.GridView_OverView.OptionsView.ShowGroupPanel = False
        Me.GridView_OverView.OptionsView.ShowViewCaption = True
        Me.GridView_OverView.PaintStyleName = "Skin"
        Me.GridView_OverView.ViewCaption = "DTY PU Life Overview"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToResizeRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Trebuchet MS", 12.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Trebuchet MS", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.PaleGreen
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.DefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2
        Me.DataGridView1.GridColor = System.Drawing.SystemColors.ActiveCaption
        Me.DataGridView1.Location = New System.Drawing.Point(132, 190)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.WhiteSmoke
        Me.DataGridView1.Size = New System.Drawing.Size(90, 55)
        Me.DataGridView1.TabIndex = 2
        '
        'DataGridView_AllTimeHistory
        '
        Me.DataGridView_AllTimeHistory.AllowUserToAddRows = False
        Me.DataGridView_AllTimeHistory.AllowUserToDeleteRows = False
        Me.DataGridView_AllTimeHistory.AllowUserToResizeRows = False
        Me.DataGridView_AllTimeHistory.BackgroundColor = System.Drawing.SystemColors.ButtonFace
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Yu Gothic UI", 12.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView_AllTimeHistory.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.DataGridView_AllTimeHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView_AllTimeHistory.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Trebuchet MS", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView_AllTimeHistory.DefaultCellStyle = DataGridViewCellStyle4
        Me.DataGridView_AllTimeHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView_AllTimeHistory.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2
        Me.DataGridView_AllTimeHistory.GridColor = System.Drawing.SystemColors.ActiveCaption
        Me.DataGridView_AllTimeHistory.Location = New System.Drawing.Point(0, 372)
        Me.DataGridView_AllTimeHistory.MultiSelect = False
        Me.DataGridView_AllTimeHistory.Name = "DataGridView_AllTimeHistory"
        Me.DataGridView_AllTimeHistory.RowHeadersVisible = False
        Me.DataGridView_AllTimeHistory.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.DataGridView_AllTimeHistory.RowTemplate.Height = 25
        Me.DataGridView_AllTimeHistory.Size = New System.Drawing.Size(470, 223)
        Me.DataGridView_AllTimeHistory.TabIndex = 3
        '
        'ChartControl1
        '
        Me.ChartControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.ChartControl1.Location = New System.Drawing.Point(0, 0)
        Me.ChartControl1.Name = "ChartControl1"
        Me.ChartControl1.SeriesSerializable = New DevExpress.XtraCharts.Series(-1) {}
        SideBySideBarSeriesLabel1.LineVisible = True
        Me.ChartControl1.SeriesTemplate.Label = SideBySideBarSeriesLabel1
        Me.ChartControl1.Size = New System.Drawing.Size(470, 372)
        Me.ChartControl1.TabIndex = 14
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.ToolStrip1)
        Me.Panel1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1239, 50)
        Me.Panel1.TabIndex = 4
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ToolStrip1.Font = New System.Drawing.Font("Yu Gothic UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripSeparator1, Me.ToolStripComboBox_PUSelector, Me.ToolStripButton_Refresh, Me.ToolStripSeparator2, Me.ToolStripLabel1, Me.ToolStripSeparator3})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1239, 50)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 50)
        '
        'ToolStripComboBox_PUSelector
        '
        Me.ToolStripComboBox_PUSelector.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripComboBox_PUSelector.AutoSize = False
        Me.ToolStripComboBox_PUSelector.Font = New System.Drawing.Font("Yu Gothic UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripComboBox_PUSelector.Items.AddRange(New Object() {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U"})
        Me.ToolStripComboBox_PUSelector.Margin = New System.Windows.Forms.Padding(0, 5, 0, 3)
        Me.ToolStripComboBox_PUSelector.Name = "ToolStripComboBox_PUSelector"
        Me.ToolStripComboBox_PUSelector.Size = New System.Drawing.Size(55, 28)
        '
        'ToolStripButton_Refresh
        '
        Me.ToolStripButton_Refresh.Image = Global.DTY_PU.My.Resources.Resources.RepeatHS
        Me.ToolStripButton_Refresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_Refresh.Margin = New System.Windows.Forms.Padding(0, 5, 0, 3)
        Me.ToolStripButton_Refresh.Name = "ToolStripButton_Refresh"
        Me.ToolStripButton_Refresh.Size = New System.Drawing.Size(67, 42)
        Me.ToolStripButton_Refresh.Text = "Refresh"
        Me.ToolStripButton_Refresh.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolStripButton_Refresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 50)
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripLabel1.Margin = New System.Windows.Forms.Padding(0, 5, 0, 3)
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(130, 42)
        Me.ToolStripLabel1.Text = "View SET History:"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 50)
        '
        'FormDTYPU
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(1239, 648)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FormDTYPU"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "PU Usage Viewer (NALC Filament)"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.GridControl_PU, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView_OverView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView_AllTimeHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(SideBySideBarSeriesLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChartControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridView_AllTimeHistory As System.Windows.Forms.DataGridView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripLabel1 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButton_Refresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripComboBox_PUSelector As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents GridControl_PU As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView_OverView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ChartControl1 As DevExpress.XtraCharts.ChartControl

End Class
