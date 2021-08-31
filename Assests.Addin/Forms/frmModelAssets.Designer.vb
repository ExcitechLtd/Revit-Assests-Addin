<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmModelAssets
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtDocumentProject = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbCategories = New System.Windows.Forms.ComboBox()
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colInBim360 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCatID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colStatusID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colBarcode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colStatus = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.btnBimCheck = New System.Windows.Forms.Button()
        Me.cmbHubs = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbProjects = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.bimCategorys = New System.Windows.Forms.ComboBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.btnPushToBim = New System.Windows.Forms.Button()
        Me.cmbStatuses = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnAddCategory = New System.Windows.Forms.Button()
        Me.btnCheck = New System.Windows.Forms.Button()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.lblCnStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblBimProject = New System.Windows.Forms.ToolStripStatusLabel()
        Me.btnClearBIMSettings = New System.Windows.Forms.Button()
        Me.btnBimProject = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Button4 = New System.Windows.Forms.Button()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(92, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Document Project"
        '
        'txtDocumentProject
        '
        Me.txtDocumentProject.Location = New System.Drawing.Point(107, 10)
        Me.txtDocumentProject.Name = "txtDocumentProject"
        Me.txtDocumentProject.Size = New System.Drawing.Size(256, 20)
        Me.txtDocumentProject.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 51)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(89, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Model Categories"
        '
        'cmbCategories
        '
        Me.cmbCategories.FormattingEnabled = True
        Me.cmbCategories.Location = New System.Drawing.Point(107, 43)
        Me.cmbCategories.Name = "cmbCategories"
        Me.cmbCategories.Size = New System.Drawing.Size(256, 21)
        Me.cmbCategories.TabIndex = 3
        '
        'GridControl1
        '
        Me.GridControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GridControl1.Location = New System.Drawing.Point(12, 291)
        Me.GridControl1.MainView = Me.GridView1
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.Size = New System.Drawing.Size(1062, 389)
        Me.GridControl1.TabIndex = 4
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.ActiveFilterEnabled = False
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colInBim360, Me.colID, Me.colCatID, Me.colStatusID, Me.colDescription, Me.colBarcode, Me.colStatus})
        Me.GridView1.GridControl = Me.GridControl1
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsCustomization.AllowFilter = False
        Me.GridView1.OptionsCustomization.AllowGroup = False
        Me.GridView1.OptionsView.ShowDetailButtons = False
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.GridView1.OptionsView.ShowIndicator = False
        '
        'colInBim360
        '
        Me.colInBim360.Name = "colInBim360"
        Me.colInBim360.UnboundType = DevExpress.Data.UnboundColumnType.[String]
        Me.colInBim360.Visible = True
        Me.colInBim360.VisibleIndex = 0
        '
        'colID
        '
        Me.colID.Caption = "ID"
        Me.colID.FieldName = "ID"
        Me.colID.Name = "colID"
        Me.colID.Visible = True
        Me.colID.VisibleIndex = 1
        Me.colID.Width = 208
        '
        'colCatID
        '
        Me.colCatID.Caption = "Category"
        Me.colCatID.FieldName = "CategoryID"
        Me.colCatID.Name = "colCatID"
        Me.colCatID.Visible = True
        Me.colCatID.VisibleIndex = 2
        Me.colCatID.Width = 208
        '
        'colStatusID
        '
        Me.colStatusID.Caption = "Status"
        Me.colStatusID.FieldName = "StatusId"
        Me.colStatusID.Name = "colStatusID"
        Me.colStatusID.Visible = True
        Me.colStatusID.VisibleIndex = 4
        Me.colStatusID.Width = 106
        '
        'colDescription
        '
        Me.colDescription.Caption = "Description"
        Me.colDescription.FieldName = "Description"
        Me.colDescription.Name = "colDescription"
        Me.colDescription.Visible = True
        Me.colDescription.VisibleIndex = 3
        Me.colDescription.Width = 208
        '
        'colBarcode
        '
        Me.colBarcode.Caption = "Barcode"
        Me.colBarcode.FieldName = "Barcode"
        Me.colBarcode.Name = "colBarcode"
        Me.colBarcode.Visible = True
        Me.colBarcode.VisibleIndex = 5
        Me.colBarcode.Width = 327
        '
        'colStatus
        '
        Me.colStatus.Caption = "Status"
        Me.colStatus.FieldName = "Status"
        Me.colStatus.Name = "colStatus"
        Me.colStatus.Visible = True
        Me.colStatus.VisibleIndex = 6
        '
        'btnBimCheck
        '
        Me.btnBimCheck.Location = New System.Drawing.Point(992, 104)
        Me.btnBimCheck.Name = "btnBimCheck"
        Me.btnBimCheck.Size = New System.Drawing.Size(75, 37)
        Me.btnBimCheck.TabIndex = 5
        Me.btnBimCheck.Text = "Connect to BIM360"
        Me.btnBimCheck.UseVisualStyleBackColor = True
        '
        'cmbHubs
        '
        Me.cmbHubs.FormattingEnabled = True
        Me.cmbHubs.Location = New System.Drawing.Point(79, 30)
        Me.cmbHubs.Name = "cmbHubs"
        Me.cmbHubs.Size = New System.Drawing.Size(252, 21)
        Me.cmbHubs.TabIndex = 6
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(11, 34)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(49, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "BIM Hub"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(11, 69)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(62, 13)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "BIM Project"
        '
        'cmbProjects
        '
        Me.cmbProjects.FormattingEnabled = True
        Me.cmbProjects.Location = New System.Drawing.Point(79, 65)
        Me.cmbProjects.Name = "cmbProjects"
        Me.cmbProjects.Size = New System.Drawing.Size(252, 21)
        Me.cmbProjects.TabIndex = 8
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(15, 83)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(86, 13)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Asset Categories"
        '
        'bimCategorys
        '
        Me.bimCategorys.FormattingEnabled = True
        Me.bimCategorys.Location = New System.Drawing.Point(107, 80)
        Me.bimCategorys.Name = "bimCategorys"
        Me.bimCategorys.Size = New System.Drawing.Size(256, 21)
        Me.bimCategorys.TabIndex = 11
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Button4)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.btnPushToBim)
        Me.GroupBox1.Controls.Add(Me.cmbStatuses)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.btnAddCategory)
        Me.GroupBox1.Controls.Add(Me.cmbHubs)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.cmbProjects)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 141)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1061, 138)
        Me.GroupBox1.TabIndex = 12
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "BIM360"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(967, 20)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(88, 38)
        Me.Button1.TabIndex = 16
        Me.Button1.Text = "Assign Status Set to Category"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'btnPushToBim
        '
        Me.btnPushToBim.Location = New System.Drawing.Point(455, 83)
        Me.btnPushToBim.Name = "btnPushToBim"
        Me.btnPushToBim.Size = New System.Drawing.Size(75, 37)
        Me.btnPushToBim.TabIndex = 15
        Me.btnPushToBim.Text = "Push to BIM360"
        Me.btnPushToBim.UseVisualStyleBackColor = True
        '
        'cmbStatuses
        '
        Me.cmbStatuses.FormattingEnabled = True
        Me.cmbStatuses.Location = New System.Drawing.Point(79, 99)
        Me.cmbStatuses.Name = "cmbStatuses"
        Me.cmbStatuses.Size = New System.Drawing.Size(252, 21)
        Me.cmbStatuses.TabIndex = 14
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(11, 102)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(48, 13)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "Statuses"
        '
        'btnAddCategory
        '
        Me.btnAddCategory.Location = New System.Drawing.Point(717, 31)
        Me.btnAddCategory.Name = "btnAddCategory"
        Me.btnAddCategory.Size = New System.Drawing.Size(75, 23)
        Me.btnAddCategory.TabIndex = 12
        Me.btnAddCategory.Text = "Add"
        Me.btnAddCategory.UseVisualStyleBackColor = True
        '
        'btnCheck
        '
        Me.btnCheck.Location = New System.Drawing.Point(369, 83)
        Me.btnCheck.Name = "btnCheck"
        Me.btnCheck.Size = New System.Drawing.Size(75, 37)
        Me.btnCheck.TabIndex = 17
        Me.btnCheck.Text = "Check BIM 360"
        Me.btnCheck.UseVisualStyleBackColor = True
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblCnStatus, Me.lblBimProject})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 727)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1086, 22)
        Me.StatusStrip1.TabIndex = 13
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'lblCnStatus
        '
        Me.lblCnStatus.Name = "lblCnStatus"
        Me.lblCnStatus.Size = New System.Drawing.Size(182, 17)
        Me.lblCnStatus.Text = "Connection Status: Disconnected"
        '
        'lblBimProject
        '
        Me.lblBimProject.Name = "lblBimProject"
        Me.lblBimProject.Size = New System.Drawing.Size(103, 17)
        Me.lblBimProject.Text = "BIM Project: None"
        '
        'btnClearBIMSettings
        '
        Me.btnClearBIMSettings.Location = New System.Drawing.Point(987, 12)
        Me.btnClearBIMSettings.Name = "btnClearBIMSettings"
        Me.btnClearBIMSettings.Size = New System.Drawing.Size(87, 37)
        Me.btnClearBIMSettings.TabIndex = 14
        Me.btnClearBIMSettings.Text = "Clear BIM360 Settings"
        Me.btnClearBIMSettings.UseVisualStyleBackColor = True
        '
        'btnBimProject
        '
        Me.btnBimProject.Location = New System.Drawing.Point(369, 3)
        Me.btnBimProject.Name = "btnBimProject"
        Me.btnBimProject.Size = New System.Drawing.Size(86, 37)
        Me.btnBimProject.TabIndex = 15
        Me.btnBimProject.Text = "Select BIM Project"
        Me.btnBimProject.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(369, 46)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 18
        Me.Button2.Text = "Button2"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(987, 61)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(75, 23)
        Me.Button3.TabIndex = 19
        Me.Button3.Text = "Button3"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(461, 9)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBox1.Size = New System.Drawing.Size(520, 126)
        Me.TextBox1.TabIndex = 20
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(612, 77)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(107, 38)
        Me.Button4.TabIndex = 17
        Me.Button4.Text = "Get Shared Paramaters Group"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'frmModelAssets
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1086, 749)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.btnCheck)
        Me.Controls.Add(Me.btnBimProject)
        Me.Controls.Add(Me.btnClearBIMSettings)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnBimCheck)
        Me.Controls.Add(Me.GridControl1)
        Me.Controls.Add(Me.cmbCategories)
        Me.Controls.Add(Me.bimCategorys)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtDocumentProject)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmModelAssets"
        Me.Text = "Form1"
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents txtDocumentProject As Windows.Forms.TextBox
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents cmbCategories As Windows.Forms.ComboBox
    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCatID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colStatusID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colBarcode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents btnBimCheck As Windows.Forms.Button
    Friend WithEvents cmbHubs As Windows.Forms.ComboBox
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents cmbProjects As Windows.Forms.ComboBox
    Friend WithEvents Label5 As Windows.Forms.Label
    Friend WithEvents bimCategorys As Windows.Forms.ComboBox
    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
    Friend WithEvents btnAddCategory As Windows.Forms.Button
    Friend WithEvents cmbStatuses As Windows.Forms.ComboBox
    Friend WithEvents Label6 As Windows.Forms.Label
    Friend WithEvents btnPushToBim As Windows.Forms.Button
    Friend WithEvents Button1 As Windows.Forms.Button
    Friend WithEvents btnCheck As Windows.Forms.Button
    Friend WithEvents colInBim360 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colStatus As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents StatusStrip1 As Windows.Forms.StatusStrip
    Friend WithEvents lblCnStatus As Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblBimProject As Windows.Forms.ToolStripStatusLabel
    Friend WithEvents btnClearBIMSettings As Windows.Forms.Button
    Friend WithEvents btnBimProject As Windows.Forms.Button
    Friend WithEvents Button2 As Windows.Forms.Button
    Friend WithEvents Button3 As Windows.Forms.Button
    Friend WithEvents TextBox1 As Windows.Forms.TextBox
    Friend WithEvents Button4 As Windows.Forms.Button
End Class
