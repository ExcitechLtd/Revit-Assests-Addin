<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmViewBIMAssets
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Me.RibbonStatusBar1 = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.RibbonControl1 = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.RadioGroup1 = New DevExpress.XtraEditors.RadioGroup()
        Me.grpsync = New DevExpress.XtraEditors.GroupControl()
        Me.btnSync = New DevExpress.XtraEditors.SimpleButton()
        Me.grpPreview = New DevExpress.XtraEditors.GroupControl()
        Me.btnPreview = New DevExpress.XtraEditors.SimpleButton()
        Me.grpModelCategory = New DevExpress.XtraEditors.GroupControl()
        Me.cmbModelCategory = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.grpBIM360Category = New DevExpress.XtraEditors.GroupControl()
        Me.txtBIM360Category = New DevExpress.XtraEditors.TextEdit()
        Me.btnBIM360Category = New DevExpress.XtraEditors.SimpleButton()
        Me.gtpBIM360Project = New DevExpress.XtraEditors.GroupControl()
        Me.txtBIM360Project = New DevExpress.XtraEditors.TextEdit()
        Me.btnBim360Project = New DevExpress.XtraEditors.SimpleButton()
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.RepositoryItemComboBox1 = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        Me.BehaviorManager1 = New DevExpress.Utils.Behaviors.BehaviorManager(Me.components)
        Me.disabledCellevent = New DevExpress.Utils.Behaviors.Common.DisabledCellEvents(Me.components)
        Me.ToolTipController1 = New DevExpress.Utils.ToolTipController(Me.components)
        CType(Me.RibbonControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.RadioGroup1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpsync, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpsync.SuspendLayout()
        CType(Me.grpPreview, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpPreview.SuspendLayout()
        CType(Me.grpModelCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpModelCategory.SuspendLayout()
        CType(Me.cmbModelCategory.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpBIM360Category, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpBIM360Category.SuspendLayout()
        CType(Me.txtBIM360Category.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gtpBIM360Project, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gtpBIM360Project.SuspendLayout()
        CType(Me.txtBIM360Project.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BehaviorManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RibbonStatusBar1
        '
        Me.RibbonStatusBar1.Location = New System.Drawing.Point(0, 833)
        Me.RibbonStatusBar1.Name = "RibbonStatusBar1"
        Me.RibbonStatusBar1.Ribbon = Me.RibbonControl1
        Me.RibbonStatusBar1.Size = New System.Drawing.Size(1206, 26)
        '
        'RibbonControl1
        '
        Me.RibbonControl1.ExpandCollapseItem.Id = 0
        Me.RibbonControl1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl1.ExpandCollapseItem, Me.RibbonControl1.SearchEditItem})
        Me.RibbonControl1.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl1.MaxItemId = 1
        Me.RibbonControl1.Name = "RibbonControl1"
        Me.RibbonControl1.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl1.ShowDisplayOptionsMenuButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl1.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl1.ShowMoreCommandsButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl1.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide
        Me.RibbonControl1.ShowQatLocationSelector = False
        Me.RibbonControl1.ShowToolbarCustomizeItem = False
        Me.RibbonControl1.Size = New System.Drawing.Size(1206, 26)
        Me.RibbonControl1.StatusBar = Me.RibbonStatusBar1
        Me.RibbonControl1.Toolbar.ShowCustomizeItem = False
        Me.RibbonControl1.Visible = False
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.Button1)
        Me.PanelControl1.Controls.Add(Me.RadioGroup1)
        Me.PanelControl1.Controls.Add(Me.grpsync)
        Me.PanelControl1.Controls.Add(Me.grpPreview)
        Me.PanelControl1.Controls.Add(Me.grpModelCategory)
        Me.PanelControl1.Controls.Add(Me.grpBIM360Category)
        Me.PanelControl1.Controls.Add(Me.gtpBIM360Project)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl1.Location = New System.Drawing.Point(0, 26)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(1206, 143)
        Me.PanelControl1.TabIndex = 2
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(385, 92)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 16
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'RadioGroup1
        '
        Me.RadioGroup1.EditValue = "bimatt"
        Me.RadioGroup1.Location = New System.Drawing.Point(9, 89)
        Me.RadioGroup1.Name = "RadioGroup1"
        Me.RadioGroup1.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem("bimatt", "Show BIM Attributes"), New DevExpress.XtraEditors.Controls.RadioGroupItem("modelatt", "Show Model Attributes")})
        Me.RadioGroup1.Size = New System.Drawing.Size(304, 37)
        Me.RadioGroup1.TabIndex = 15
        '
        'grpsync
        '
        Me.grpsync.Controls.Add(Me.btnSync)
        Me.grpsync.Enabled = False
        Me.grpsync.Location = New System.Drawing.Point(1064, 10)
        Me.grpsync.Name = "grpsync"
        Me.grpsync.Size = New System.Drawing.Size(132, 73)
        Me.grpsync.TabIndex = 11
        Me.grpsync.Text = "5. Sync Assets"
        '
        'btnSync
        '
        Me.btnSync.Location = New System.Drawing.Point(21, 30)
        Me.btnSync.Name = "btnSync"
        Me.btnSync.Size = New System.Drawing.Size(90, 36)
        Me.btnSync.TabIndex = 1
        Me.btnSync.Text = "Sync Asset" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Information" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'grpPreview
        '
        Me.grpPreview.Controls.Add(Me.btnPreview)
        Me.grpPreview.Enabled = False
        Me.grpPreview.Location = New System.Drawing.Point(926, 10)
        Me.grpPreview.Name = "grpPreview"
        Me.grpPreview.Size = New System.Drawing.Size(132, 73)
        Me.grpPreview.TabIndex = 10
        Me.grpPreview.Text = "4. Preview Assets"
        '
        'btnPreview
        '
        Me.btnPreview.Location = New System.Drawing.Point(21, 30)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(90, 36)
        Me.btnPreview.TabIndex = 0
        Me.btnPreview.Text = "Generate" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Preview" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'grpModelCategory
        '
        Me.grpModelCategory.Controls.Add(Me.cmbModelCategory)
        Me.grpModelCategory.Enabled = False
        Me.grpModelCategory.Location = New System.Drawing.Point(631, 10)
        Me.grpModelCategory.Name = "grpModelCategory"
        Me.grpModelCategory.Size = New System.Drawing.Size(289, 73)
        Me.grpModelCategory.TabIndex = 9
        Me.grpModelCategory.Text = "3. Select Model Element Category"
        '
        'cmbModelCategory
        '
        Me.cmbModelCategory.EditValue = "No Model category selected"
        Me.cmbModelCategory.Location = New System.Drawing.Point(5, 35)
        Me.cmbModelCategory.MenuManager = Me.RibbonControl1
        Me.cmbModelCategory.Name = "cmbModelCategory"
        Me.cmbModelCategory.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbModelCategory.Properties.Padding = New System.Windows.Forms.Padding(2)
        Me.cmbModelCategory.Size = New System.Drawing.Size(277, 24)
        Me.cmbModelCategory.TabIndex = 12
        '
        'grpBIM360Category
        '
        Me.grpBIM360Category.Controls.Add(Me.txtBIM360Category)
        Me.grpBIM360Category.Controls.Add(Me.btnBIM360Category)
        Me.grpBIM360Category.Enabled = False
        Me.grpBIM360Category.Location = New System.Drawing.Point(319, 10)
        Me.grpBIM360Category.Name = "grpBIM360Category"
        Me.grpBIM360Category.Size = New System.Drawing.Size(306, 73)
        Me.grpBIM360Category.TabIndex = 8
        Me.grpBIM360Category.Text = "2. Select BIM360 Asset Category"
        '
        'txtBIM360Category
        '
        Me.txtBIM360Category.EditValue = "No category selected"
        Me.txtBIM360Category.Enabled = False
        Me.txtBIM360Category.Location = New System.Drawing.Point(12, 33)
        Me.txtBIM360Category.MenuManager = Me.RibbonControl1
        Me.txtBIM360Category.Name = "txtBIM360Category"
        Me.txtBIM360Category.Properties.Padding = New System.Windows.Forms.Padding(2)
        Me.txtBIM360Category.Size = New System.Drawing.Size(246, 24)
        Me.txtBIM360Category.TabIndex = 6
        '
        'btnBIM360Category
        '
        Me.btnBIM360Category.Location = New System.Drawing.Point(264, 33)
        Me.btnBIM360Category.Name = "btnBIM360Category"
        Me.btnBIM360Category.Size = New System.Drawing.Size(31, 24)
        Me.btnBIM360Category.TabIndex = 5
        Me.btnBIM360Category.Text = "..."
        '
        'gtpBIM360Project
        '
        Me.gtpBIM360Project.Controls.Add(Me.txtBIM360Project)
        Me.gtpBIM360Project.Controls.Add(Me.btnBim360Project)
        Me.gtpBIM360Project.Location = New System.Drawing.Point(9, 10)
        Me.gtpBIM360Project.Name = "gtpBIM360Project"
        Me.gtpBIM360Project.Size = New System.Drawing.Size(304, 73)
        Me.gtpBIM360Project.TabIndex = 7
        Me.gtpBIM360Project.Text = "1. Select BIM360 Project"
        '
        'txtBIM360Project
        '
        Me.txtBIM360Project.EditValue = "No project selected"
        Me.txtBIM360Project.Enabled = False
        Me.txtBIM360Project.Location = New System.Drawing.Point(10, 33)
        Me.txtBIM360Project.MenuManager = Me.RibbonControl1
        Me.txtBIM360Project.Name = "txtBIM360Project"
        Me.txtBIM360Project.Properties.Padding = New System.Windows.Forms.Padding(3)
        Me.txtBIM360Project.Size = New System.Drawing.Size(247, 26)
        Me.txtBIM360Project.TabIndex = 6
        '
        'btnBim360Project
        '
        Me.btnBim360Project.Location = New System.Drawing.Point(263, 33)
        Me.btnBim360Project.Name = "btnBim360Project"
        Me.btnBim360Project.Size = New System.Drawing.Size(31, 24)
        Me.btnBim360Project.TabIndex = 4
        Me.btnBim360Project.Text = "..."
        '
        'GridControl1
        '
        Me.GridControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridControl1.Location = New System.Drawing.Point(0, 169)
        Me.GridControl1.MainView = Me.GridView1
        Me.GridControl1.MenuManager = Me.RibbonControl1
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemComboBox1})
        Me.GridControl1.Size = New System.Drawing.Size(1206, 664)
        Me.GridControl1.TabIndex = 0
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(216, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.GridView1.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black
        Me.GridView1.Appearance.FocusedRow.Options.UseBackColor = True
        Me.GridView1.Appearance.FocusedRow.Options.UseForeColor = True
        Me.GridView1.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(216, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.GridView1.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.GridView1.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.GridView1.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Black
        Me.GridView1.Appearance.SelectedRow.Options.UseBackColor = True
        Me.GridView1.Appearance.SelectedRow.Options.UseForeColor = True
        Me.BehaviorManager1.SetBehaviors(Me.GridView1, New DevExpress.Utils.Behaviors.Behavior() {CType(DevExpress.Utils.Behaviors.Common.DisabledCellBehavior.Create(GetType(DevExpress.XtraGrid.Extensions.GridViewDisabledCellSource), "", AppearanceObject1, Me.disabledCellevent), DevExpress.Utils.Behaviors.Behavior)})
        Me.GridView1.GridControl = Me.GridControl1
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsCustomization.AllowGroup = False
        Me.GridView1.OptionsCustomization.AllowMergedGrouping = DevExpress.Utils.DefaultBoolean.[True]
        Me.GridView1.OptionsMenu.EnableGroupPanelMenu = False
        Me.GridView1.OptionsSelection.MultiSelect = True
        Me.GridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect
        Me.GridView1.OptionsView.ColumnAutoWidth = False
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.GridView1.OptionsView.ShowIndicator = False
        '
        'RepositoryItemComboBox1
        '
        Me.RepositoryItemComboBox1.AutoHeight = False
        Me.RepositoryItemComboBox1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemComboBox1.Items.AddRange(New Object() {"foo", "foo2", "bar", "jam"})
        Me.RepositoryItemComboBox1.Name = "RepositoryItemComboBox1"
        '
        'frmViewBIMAssets
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1206, 859)
        Me.Controls.Add(Me.GridControl1)
        Me.Controls.Add(Me.PanelControl1)
        Me.Controls.Add(Me.RibbonControl1)
        Me.Controls.Add(Me.RibbonStatusBar1)
        Me.LookAndFeel.SkinName = "The Bezier"
        Me.LookAndFeel.UseDefaultLookAndFeel = False
        Me.Name = "frmViewBIMAssets"
        Me.Text = "frmViewBIMAssets"
        CType(Me.RibbonControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.RadioGroup1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpsync, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpsync.ResumeLayout(False)
        CType(Me.grpPreview, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpPreview.ResumeLayout(False)
        CType(Me.grpModelCategory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpModelCategory.ResumeLayout(False)
        CType(Me.cmbModelCategory.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpBIM360Category, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpBIM360Category.ResumeLayout(False)
        CType(Me.txtBIM360Category.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gtpBIM360Project, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gtpBIM360Project.ResumeLayout(False)
        CType(Me.txtBIM360Project.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BehaviorManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RibbonStatusBar1 As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents RibbonControl1 As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents grpPreview As DevExpress.XtraEditors.GroupControl
    Friend WithEvents grpModelCategory As DevExpress.XtraEditors.GroupControl
    Friend WithEvents grpBIM360Category As DevExpress.XtraEditors.GroupControl
    Friend WithEvents btnBIM360Category As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents gtpBIM360Project As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtBIM360Project As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btnBim360Project As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents txtBIM360Category As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btnSync As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnPreview As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents grpsync As DevExpress.XtraEditors.GroupControl
    Friend WithEvents cmbModelCategory As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents RadioGroup1 As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents BehaviorManager1 As DevExpress.Utils.Behaviors.BehaviorManager
    Friend WithEvents ToolTipController1 As DevExpress.Utils.ToolTipController
    Friend WithEvents disabledCellevent As DevExpress.Utils.Behaviors.Common.DisabledCellEvents
    Friend WithEvents RepositoryItemComboBox1 As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
    Friend WithEvents Button1 As Windows.Forms.Button
End Class
