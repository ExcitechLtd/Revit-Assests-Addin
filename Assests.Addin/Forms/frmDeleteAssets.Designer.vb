<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDeleteAssets
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.grpPreviewDelete = New DevExpress.XtraEditors.GroupControl()
        Me.btnPreview = New DevExpress.XtraEditors.SimpleButton()
        Me.grpDeleteAsset = New DevExpress.XtraEditors.GroupControl()
        Me.btnDeleteAssets = New DevExpress.XtraEditors.SimpleButton()
        Me.grpBIM360Category = New DevExpress.XtraEditors.GroupControl()
        Me.txtBIM360Category = New DevExpress.XtraEditors.TextEdit()
        Me.btnBIM360Category = New DevExpress.XtraEditors.SimpleButton()
        Me.gtpBIM360Project = New DevExpress.XtraEditors.GroupControl()
        Me.txtBIM360Project = New DevExpress.XtraEditors.TextEdit()
        Me.btnBim360Project = New DevExpress.XtraEditors.SimpleButton()
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.grpPreviewDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpPreviewDelete.SuspendLayout()
        CType(Me.grpDeleteAsset, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpDeleteAsset.SuspendLayout()
        CType(Me.grpBIM360Category, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpBIM360Category.SuspendLayout()
        CType(Me.txtBIM360Category.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gtpBIM360Project, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gtpBIM360Project.SuspendLayout()
        CType(Me.txtBIM360Project.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.grpPreviewDelete)
        Me.PanelControl1.Controls.Add(Me.grpDeleteAsset)
        Me.PanelControl1.Controls.Add(Me.grpBIM360Category)
        Me.PanelControl1.Controls.Add(Me.gtpBIM360Project)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(1206, 97)
        Me.PanelControl1.TabIndex = 0
        '
        'grpPreviewDelete
        '
        Me.grpPreviewDelete.Controls.Add(Me.btnPreview)
        Me.grpPreviewDelete.Enabled = False
        Me.grpPreviewDelete.Location = New System.Drawing.Point(634, 12)
        Me.grpPreviewDelete.Name = "grpPreviewDelete"
        Me.grpPreviewDelete.Size = New System.Drawing.Size(132, 73)
        Me.grpPreviewDelete.TabIndex = 12
        Me.grpPreviewDelete.Text = "3. Preview Assets"
        '
        'btnPreview
        '
        Me.btnPreview.Location = New System.Drawing.Point(21, 30)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(90, 36)
        Me.btnPreview.TabIndex = 0
        Me.btnPreview.Text = "Generate" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Preview" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'grpDeleteAsset
        '
        Me.grpDeleteAsset.Controls.Add(Me.btnDeleteAssets)
        Me.grpDeleteAsset.Enabled = False
        Me.grpDeleteAsset.Location = New System.Drawing.Point(772, 12)
        Me.grpDeleteAsset.Name = "grpDeleteAsset"
        Me.grpDeleteAsset.Size = New System.Drawing.Size(132, 73)
        Me.grpDeleteAsset.TabIndex = 11
        Me.grpDeleteAsset.Text = "4. Delete"
        '
        'btnDeleteAssets
        '
        Me.btnDeleteAssets.Location = New System.Drawing.Point(21, 30)
        Me.btnDeleteAssets.Name = "btnDeleteAssets"
        Me.btnDeleteAssets.Size = New System.Drawing.Size(90, 36)
        Me.btnDeleteAssets.TabIndex = 0
        Me.btnDeleteAssets.Text = "Delete Selected" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Assets"
        '
        'grpBIM360Category
        '
        Me.grpBIM360Category.Controls.Add(Me.txtBIM360Category)
        Me.grpBIM360Category.Controls.Add(Me.btnBIM360Category)
        Me.grpBIM360Category.Enabled = False
        Me.grpBIM360Category.Location = New System.Drawing.Point(322, 12)
        Me.grpBIM360Category.Name = "grpBIM360Category"
        Me.grpBIM360Category.Size = New System.Drawing.Size(306, 73)
        Me.grpBIM360Category.TabIndex = 10
        Me.grpBIM360Category.Text = "2. Select BIM360 Asset Category"
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
        'gtpBIM360Project
        '
        Me.gtpBIM360Project.Controls.Add(Me.txtBIM360Project)
        Me.gtpBIM360Project.Controls.Add(Me.btnBim360Project)
        Me.gtpBIM360Project.Location = New System.Drawing.Point(12, 12)
        Me.gtpBIM360Project.Name = "gtpBIM360Project"
        Me.gtpBIM360Project.Size = New System.Drawing.Size(304, 73)
        Me.gtpBIM360Project.TabIndex = 9
        Me.gtpBIM360Project.Text = "1. Select BIM360 Project"
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
        Me.GridControl1.Size = New System.Drawing.Size(1206, 762)
        Me.GridControl1.TabIndex = 1
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.GridControl = Me.GridControl1
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridView1.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridView1.OptionsSelection.MultiSelect = True
        Me.GridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.GridView1.OptionsView.ShowIndicator = False
        '
        'frmDeleteAssets
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1206, 859)
        Me.Controls.Add(Me.GridControl1)
        Me.Controls.Add(Me.PanelControl1)
        Me.Name = "frmDeleteAssets"
        Me.Text = "frmDeleteAssets"
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.grpPreviewDelete, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpPreviewDelete.ResumeLayout(False)
        CType(Me.grpDeleteAsset, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpDeleteAsset.ResumeLayout(False)
        CType(Me.grpBIM360Category, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpBIM360Category.ResumeLayout(False)
        CType(Me.txtBIM360Category.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gtpBIM360Project, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gtpBIM360Project.ResumeLayout(False)
        CType(Me.txtBIM360Project.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents grpBIM360Category As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtBIM360Category As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btnBIM360Category As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents gtpBIM360Project As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtBIM360Project As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btnBim360Project As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents grpDeleteAsset As DevExpress.XtraEditors.GroupControl
    Friend WithEvents btnDeleteAssets As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents grpPreviewDelete As DevExpress.XtraEditors.GroupControl
    Friend WithEvents btnPreview As DevExpress.XtraEditors.SimpleButton
End Class
