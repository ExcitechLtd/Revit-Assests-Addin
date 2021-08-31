<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmAddAssetsToBim
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.grpsync = New DevExpress.XtraEditors.GroupControl()
        Me.btnSync = New DevExpress.XtraEditors.SimpleButton()
        Me.grpModelCategory = New DevExpress.XtraEditors.GroupControl()
        Me.cmbModelCategory = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.grpBIM360Category = New DevExpress.XtraEditors.GroupControl()
        Me.txtBIM360Category = New DevExpress.XtraEditors.TextEdit()
        Me.btnBIM360Category = New DevExpress.XtraEditors.SimpleButton()
        Me.grpBIM360Project = New DevExpress.XtraEditors.GroupControl()
        Me.txtBIM360Project = New DevExpress.XtraEditors.TextEdit()
        Me.btnBim360Project = New DevExpress.XtraEditors.SimpleButton()
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ToolTipController1 = New DevExpress.Utils.ToolTipController(Me.components)
        Me.BehaviorManager1 = New DevExpress.Utils.Behaviors.BehaviorManager(Me.components)
        Me.disabledCellevent = New DevExpress.Utils.Behaviors.Common.DisabledCellEvents(Me.components)
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.grpsync, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpsync.SuspendLayout()
        CType(Me.grpModelCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpModelCategory.SuspendLayout()
        CType(Me.cmbModelCategory.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpBIM360Category, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpBIM360Category.SuspendLayout()
        CType(Me.txtBIM360Category.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpBIM360Project, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpBIM360Project.SuspendLayout()
        CType(Me.txtBIM360Project.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BehaviorManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.grpsync)
        Me.PanelControl1.Controls.Add(Me.grpModelCategory)
        Me.PanelControl1.Controls.Add(Me.grpBIM360Category)
        Me.PanelControl1.Controls.Add(Me.grpBIM360Project)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(1190, 97)
        Me.PanelControl1.TabIndex = 0
        '
        'grpsync
        '
        Me.grpsync.Controls.Add(Me.btnSync)
        Me.grpsync.Enabled = False
        Me.grpsync.Location = New System.Drawing.Point(926, 10)
        Me.grpsync.Name = "grpsync"
        Me.grpsync.Size = New System.Drawing.Size(132, 73)
        Me.grpsync.TabIndex = 13
        Me.grpsync.Text = "4. Add Assets"
        '
        'btnSync
        '
        Me.btnSync.Location = New System.Drawing.Point(21, 30)
        Me.btnSync.Name = "btnSync"
        Me.btnSync.Size = New System.Drawing.Size(90, 36)
        Me.btnSync.TabIndex = 1
        Me.btnSync.Text = "Add Selected" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Assets" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'grpModelCategory
        '
        Me.grpModelCategory.Controls.Add(Me.cmbModelCategory)
        Me.grpModelCategory.Location = New System.Drawing.Point(9, 10)
        Me.grpModelCategory.Name = "grpModelCategory"
        Me.grpModelCategory.Size = New System.Drawing.Size(289, 73)
        Me.grpModelCategory.TabIndex = 12
        Me.grpModelCategory.Text = "1. Select Model Element Category"
        '
        'cmbModelCategory
        '
        Me.cmbModelCategory.EditValue = "No Model category selected"
        Me.cmbModelCategory.Location = New System.Drawing.Point(5, 35)
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
        Me.grpBIM360Category.Location = New System.Drawing.Point(614, 10)
        Me.grpBIM360Category.Name = "grpBIM360Category"
        Me.grpBIM360Category.Size = New System.Drawing.Size(306, 73)
        Me.grpBIM360Category.TabIndex = 11
        Me.grpBIM360Category.Text = "3. Select BIM360 Asset Category"
        '
        'txtBIM360Category
        '
        Me.txtBIM360Category.EditValue = "No category selected"
        Me.txtBIM360Category.Enabled = False
        Me.txtBIM360Category.Location = New System.Drawing.Point(12, 33)
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
        'grpBIM360Project
        '
        Me.grpBIM360Project.Controls.Add(Me.txtBIM360Project)
        Me.grpBIM360Project.Controls.Add(Me.btnBim360Project)
        Me.grpBIM360Project.Enabled = False
        Me.grpBIM360Project.Location = New System.Drawing.Point(304, 10)
        Me.grpBIM360Project.Name = "grpBIM360Project"
        Me.grpBIM360Project.Size = New System.Drawing.Size(304, 73)
        Me.grpBIM360Project.TabIndex = 10
        Me.grpBIM360Project.Text = "2. Select BIM360 Project"
        '
        'txtBIM360Project
        '
        Me.txtBIM360Project.EditValue = "No project selected"
        Me.txtBIM360Project.Enabled = False
        Me.txtBIM360Project.Location = New System.Drawing.Point(10, 33)
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
        Me.GridControl1.Location = New System.Drawing.Point(0, 97)
        Me.GridControl1.MainView = Me.GridView1
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.Size = New System.Drawing.Size(1190, 753)
        Me.GridControl1.TabIndex = 2
        Me.GridControl1.ToolTipController = Me.ToolTipController1
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.BehaviorManager1.SetBehaviors(Me.GridView1, New DevExpress.Utils.Behaviors.Behavior() {CType(DevExpress.Utils.Behaviors.Common.DisabledCellBehavior.Create(GetType(DevExpress.XtraGrid.Extensions.GridViewDisabledCellSource), "", AppearanceObject1, Me.disabledCellevent), DevExpress.Utils.Behaviors.Behavior)})
        Me.GridView1.GridControl = Me.GridControl1
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridView1.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridView1.OptionsSelection.MultiSelect = True
        Me.GridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect
        Me.GridView1.OptionsView.ColumnAutoWidth = False
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.GridView1.OptionsView.ShowIndicator = False
        '
        'ToolTipController1
        '
        '
        'frmAddAssetsToBim
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1190, 850)
        Me.Controls.Add(Me.GridControl1)
        Me.Controls.Add(Me.PanelControl1)
        Me.Name = "frmAddAssetsToBim"
        Me.Text = "Add model Assets To BIM360"
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.grpsync, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpsync.ResumeLayout(False)
        CType(Me.grpModelCategory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpModelCategory.ResumeLayout(False)
        CType(Me.cmbModelCategory.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpBIM360Category, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpBIM360Category.ResumeLayout(False)
        CType(Me.txtBIM360Category.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpBIM360Project, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpBIM360Project.ResumeLayout(False)
        CType(Me.txtBIM360Project.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BehaviorManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents grpModelCategory As DevExpress.XtraEditors.GroupControl
    Friend WithEvents cmbModelCategory As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents grpBIM360Category As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtBIM360Category As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btnBIM360Category As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents grpBIM360Project As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtBIM360Project As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btnBim360Project As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents grpsync As DevExpress.XtraEditors.GroupControl
    Friend WithEvents btnSync As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BehaviorManager1 As DevExpress.Utils.Behaviors.BehaviorManager
    Friend WithEvents disabledCellevent As DevExpress.Utils.Behaviors.Common.DisabledCellEvents
    Friend WithEvents ToolTipController1 As DevExpress.Utils.ToolTipController
End Class
