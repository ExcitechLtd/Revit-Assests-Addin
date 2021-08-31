<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCreateNewCategory
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
        Me.tvBimCategories = New System.Windows.Forms.TreeView()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.txtCatName = New DevExpress.XtraEditors.TextEdit()
        Me.grpProject = New DevExpress.XtraEditors.GroupControl()
        Me.txtBIM360Project = New DevExpress.XtraEditors.TextEdit()
        Me.btnBim360Project = New DevExpress.XtraEditors.SimpleButton()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        CType(Me.txtCatName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpProject, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpProject.SuspendLayout()
        CType(Me.txtBIM360Project.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        Me.SuspendLayout()
        '
        'tvBimCategories
        '
        Me.tvBimCategories.HideSelection = False
        Me.tvBimCategories.Location = New System.Drawing.Point(9, 33)
        Me.tvBimCategories.Name = "tvBimCategories"
        Me.tvBimCategories.Size = New System.Drawing.Size(394, 314)
        Me.tvBimCategories.TabIndex = 5
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(118, 540)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(99, 37)
        Me.btnAdd.TabIndex = 9
        Me.btnAdd.Text = "Add"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(223, 540)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(99, 37)
        Me.btnClose.TabIndex = 10
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'txtCatName
        '
        Me.txtCatName.Location = New System.Drawing.Point(8, 33)
        Me.txtCatName.Name = "txtCatName"
        Me.txtCatName.Properties.Padding = New System.Windows.Forms.Padding(3)
        Me.txtCatName.Size = New System.Drawing.Size(394, 26)
        Me.txtCatName.TabIndex = 8
        '
        'grpProject
        '
        Me.grpProject.Controls.Add(Me.txtBIM360Project)
        Me.grpProject.Controls.Add(Me.btnBim360Project)
        Me.grpProject.Location = New System.Drawing.Point(12, 12)
        Me.grpProject.Name = "grpProject"
        Me.grpProject.Size = New System.Drawing.Size(414, 72)
        Me.grpProject.TabIndex = 11
        Me.grpProject.Text = "Select BIM360 Project"
        '
        'txtBIM360Project
        '
        Me.txtBIM360Project.EditValue = "No project selected"
        Me.txtBIM360Project.Enabled = False
        Me.txtBIM360Project.Location = New System.Drawing.Point(8, 33)
        Me.txtBIM360Project.Name = "txtBIM360Project"
        Me.txtBIM360Project.Properties.Padding = New System.Windows.Forms.Padding(3)
        Me.txtBIM360Project.Size = New System.Drawing.Size(357, 26)
        Me.txtBIM360Project.TabIndex = 8
        '
        'btnBim360Project
        '
        Me.btnBim360Project.Location = New System.Drawing.Point(371, 34)
        Me.btnBim360Project.Name = "btnBim360Project"
        Me.btnBim360Project.Size = New System.Drawing.Size(31, 24)
        Me.btnBim360Project.TabIndex = 7
        Me.btnBim360Project.Text = "..."
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.tvBimCategories)
        Me.GroupControl1.Location = New System.Drawing.Point(12, 90)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(414, 358)
        Me.GroupControl1.TabIndex = 12
        Me.GroupControl1.Text = "Select parent category"
        '
        'GroupControl2
        '
        Me.GroupControl2.Controls.Add(Me.txtCatName)
        Me.GroupControl2.Location = New System.Drawing.Point(12, 454)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(414, 68)
        Me.GroupControl2.TabIndex = 13
        Me.GroupControl2.Text = "New category name"
        '
        'frmCreateNewCategory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(440, 583)
        Me.Controls.Add(Me.GroupControl2)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.grpProject)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnAdd)
        Me.Name = "frmCreateNewCategory"
        Me.Text = "Create new category"
        CType(Me.txtCatName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpProject, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpProject.ResumeLayout(False)
        CType(Me.txtBIM360Project.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents tvBimCategories As Windows.Forms.TreeView
    Friend WithEvents txtCatName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btnAdd As Windows.Forms.Button
    Friend WithEvents btnClose As Windows.Forms.Button
    Friend WithEvents grpProject As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtBIM360Project As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btnBim360Project As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
End Class
