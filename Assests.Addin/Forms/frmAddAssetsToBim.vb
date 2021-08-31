Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports Autodesk.Revit.DB
Imports DevExpress.Data
Imports DevExpress.Data.Extensions
Imports DevExpress.Utils
Imports DevExpress.Utils.Menu
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraEditors.ViewInfo
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo

Public Class frmAddAssetsToBim

#Region " Private "
    Private _modelCatList As List(Of modelCategory)
    Private modelElements As BindingList(Of AssetInformation)
    Private cmbStatusCellEditor As Repository.RepositoryItemComboBox
    Private txtEditItem As Repository.RepositoryItemTextEdit
    Private imageEdit As Repository.RepositoryItemImageComboBox

    Private attributeTextEdit As Repository.RepositoryItemTextEdit

    Public RevitDocument As Document
#End Region

    Private Sub cmbModelCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbModelCategory.SelectedIndexChanged
        grpBIM360Project.Enabled = True
        ''load the elements

        SharedObjects.ForgeConnection.SelectedModelCategory = cmbModelCategory.SelectedItem

        ''load all the elements from the selected model category
        modelElements = FamilyHelper.GetModelCategoryAssets(RevitDocument, cmbModelCategory.SelectedItem)

        ''get the shared parameters
        Dim paramFile As DefinitionFile = RevitDocument.Application.OpenSharedParameterFile
        Dim paramGrp As DefinitionGroup = Nothing

        For Each paramGrp In paramFile.Groups
            If paramGrp.Name.ToUpperInvariant = "EXC_ASSETS" Then Exit For Else paramGrp = Nothing
        Next

        For Each modelEl As AssetInformation In modelElements
            Dim el As Element = RevitDocument.GetElement(modelEl.uniqueID)
            modelEl.AssetParamaters = New Dictionary(Of String, String)

            For Each def In paramGrp.Definitions
                Dim param = el.LookupParameter(def.Name)

                If param Is Nothing Then
                    modelEl.AssetParamaters.Add(def.Name, String.Empty)
                Else
                    modelEl.AssetParamaters.Add(def.Name, param.AsString)
                End If
            Next

        Next

        CreateGridcolumns()

        ''show the model data
        GridControl1.DataSource = modelElements
        GridView1.OptionsView.ColumnAutoWidth = False
        GridView1.BestFitColumns()
    End Sub

    Private Sub CreateGridcolumns()
        GridView1.Columns.Clear()
        Dim col As DevExpress.XtraGrid.Columns.GridColumn
        txtEditItem = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
        attributeTextEdit = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit

        GridControl1.RepositoryItems.Add(txtEditItem)
        GridControl1.RepositoryItems.Add(attributeTextEdit)

        ''img column ' .FieldName = "SyncStatus"
        col = New DevExpress.XtraGrid.Columns.GridColumn With {.Name = "imgCol", .Caption = "Sync Status", .FieldName = "SyncStatusValue", .Visible = True, .UnboundType = DevExpress.Data.UnboundColumnType.String}
        col.OptionsColumn.AllowEdit = False
        txtEditItem.ContextImageOptions.Image = My.Resources.blank
        col.ColumnEdit = txtEditItem
        GridView1.Columns.Add(col)

        ''standard columns
        col = New DevExpress.XtraGrid.Columns.GridColumn With {.Name = "colID", .Caption = "ID", .FieldName = "ID", .Visible = True}
        col.OptionsColumn.AllowEdit = False
        GridView1.Columns.Add(col)

        col = New DevExpress.XtraGrid.Columns.GridColumn With {.Name = "colCategory", .Caption = "Category", .FieldName = "Category", .Visible = True}
        col.OptionsColumn.AllowEdit = False
        GridView1.Columns.Add(col)

        col = New DevExpress.XtraGrid.Columns.GridColumn With {.Name = "colDescription", .Caption = "Description", .FieldName = "Description", .Visible = True}
        col.OptionsColumn.AllowEdit = False
        GridView1.Columns.Add(col)

        col = New DevExpress.XtraGrid.Columns.GridColumn With {.Name = "colAssetStatus", .Caption = "Asset Status", .FieldName = "AssetStatus", .Visible = True}
        col.OptionsColumn.AllowEdit = True
        col.ColumnEdit = cmbStatusCellEditor
        GridView1.Columns.Add(col)

        'col = New DevExpress.XtraGrid.Columns.GridColumn With {.Name = "colAssetStatus", .Caption = "Asset Status", .FieldName = "AssetStatus", .Visible = True, .UnboundType = DevExpress.Data.UnboundColumnType.String}
        'col.OptionsColumn.AllowEdit = True
        'col.ColumnEdit = cmbStatusCellEditor
        'GridView1.Columns.Add(col)

        'col = New DevExpress.XtraGrid.Columns.GridColumn With {.Name = "colBarcode", .Caption = "Barcode", .FieldName = "Barcode", .Visible = True}
        'col.OptionsColumn.AllowEdit = False
        'GridView1.Columns.Add(col)

        ''model attributes
        For Each _str As String In FamilyHelper.GetAssetParamterNames(RevitDocument)
            Dim colName As String = "colAtt" + _str.Replace(" ", "_")
            Dim colCaption As String = _str.Replace("ASSETS.", "")
            'colName
            col = New DevExpress.XtraGrid.Columns.GridColumn With {.Name = colName, .Caption = colCaption, .Visible = True, .FieldName = _str, .UnboundType = DevExpress.Data.UnboundColumnType.String}
            col.OptionsColumn.AllowEdit = True
            col.ColumnEdit = attributeTextEdit
            GridView1.Columns.Add(col)
        Next


    End Sub

    Private Sub frmAddAssetsToBim_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddHandler disabledCellevent.ProcessingCell, AddressOf DisabledCellEventHandler

        ''load the model categories
        _modelCatList = New List(Of modelCategory)

        For Each c As Category In RevitDocument.Settings.Categories
            If Not c.CategoryType = CategoryType.Model Then Continue For

            _modelCatList.Add(New modelCategory With {
                .ID = c.Id.IntegerValue,
                .Name = c.Name
            })

        Next

        _modelCatList.Sort(Function(x, y) x.Name.CompareTo(y.Name))
        cmbModelCategory.Properties.Items.AddRange(_modelCatList)

        cmbStatusCellEditor = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox
        AddHandler cmbStatusCellEditor.ParseEditValue, AddressOf ParseStatusValue
        AddHandler cmbStatusCellEditor.CloseUp, AddressOf cmbStatusHandleCloseUp

        GridControl1.RepositoryItems.Add(cmbStatusCellEditor)
    End Sub

    Private Sub GridView1_CustomUnboundColumnData(sender As Object, e As CustomColumnDataEventArgs) Handles GridView1.CustomUnboundColumnData
        If sender Is Nothing Then Exit Sub

        If e.IsGetData Then
            'e.Column.FieldName ''this is the attributename
            ''get the bim attributes
            Dim dataRow As AssetInformation = e.Row
            e.Value = dataRow.AssetParamaters(e.Column.FieldName)

            Exit Sub
        End If

        If e.IsSetData Then
            Dim dataRow As AssetInformation = e.Row
            dataRow.AssetParamaters(e.Column.FieldName) = e.Value

            ''set the model parameter
            ' dataRow.uniqueID ''this is the model element
            Dim el As Element = RevitDocument.GetElement(dataRow.uniqueID)
            Dim param = el.LookupParameter(e.Column.FieldName)
            If Not param Is Nothing Then
                Using tr As New Transaction(RevitDocument)
                    tr.Start("SyncFromBIM360")

                    param.Set(e.Value.ToString)
                    tr.Commit()

                End Using

            End If

        End If

    End Sub

    Private Sub btnBIM360Category_Click(sender As Object, e As EventArgs) Handles btnBIM360Category.Click
        If Not SharedObjects.IsConnectionActive Then
            If Not SharedObjects.ForgeConnection.AccessToken = "" Then
                SharedObjects.ForgeConnection.GetNewRefreshToken()
                SharedObjects.UserSettings.UpdateTokensAndSave(SharedObjects.ForgeConnection)
            Else
                SharedObjects.DoForgeAuth()
                SharedObjects.UserSettings.UpdateTokensAndSave(SharedObjects.ForgeConnection)
            End If
        End If

        Dim frmSelect As New frmSelectBIMCategory
        frmSelect.ForgeConnection = SharedObjects.ForgeConnection
        frmSelect.StartPosition = Windows.Forms.FormStartPosition.CenterParent

        If frmSelect.ShowDialog = Windows.Forms.DialogResult.OK Then
            SharedObjects.ForgeConnection.SelectedCategory = frmSelect.SelectedCategory
            txtBIM360Category.Text = SharedObjects.ForgeConnection.SelectedCategory.Name
            txtBIM360Category.BackColor = System.Drawing.Color.White

            'grpsync.Enabled = True
            'SharedObjects.ForgeConnection.GetStatuses()
            SharedObjects.ForgeConnection.GetCategoryStatusStepID(True)
            cmbStatusCellEditor.Items.AddRange(SharedObjects.ForgeConnection.Statuses)

            ''check the existing assets just get there names 
            Dim existing = SharedObjects.ForgeConnection.GetAssetNames
            SharedObjects.ForgeConnection.GetCustomAttributes() ''need to check for required attributes

            ''update the modelelements with th category
            For Each asset As AssetInformation In modelElements
                asset.Category = SharedObjects.ForgeConnection.SelectedCategory.Name
                asset.CategoryID = SharedObjects.ForgeConnection.SelectedCategory.ID

                If SharedObjects.ForgeConnection.Statuses.Count >= 0 Then
                    asset.AssetStatusID = SharedObjects.ForgeConnection.Statuses(0).ID
                    asset.AssetStatus = SharedObjects.ForgeConnection.Statuses(0).Label
                    asset.StatusId = SharedObjects.ForgeConnection.Statuses(0).ID
                End If

                Dim eIndex As Integer = existing.FindIndex(Function(ei) ei.ToUpperInvariant = asset.ID.ToUpperInvariant)

                If eIndex > -1 Then asset.SyncStatus = SyncStatusEnum.Exists

            Next
            GridView1.RefreshData()

        End If

        GridView1.BestFitColumns(True)
    End Sub

    Private Sub btnBim360Project_Click(sender As Object, e As EventArgs) Handles btnBim360Project.Click
        ''perform auth just in case
        If Not SharedObjects.IsConnectionActive Then
            If Not SharedObjects.ForgeConnection.AccessToken = "" Then
                SharedObjects.ForgeConnection.GetNewRefreshToken()
                SharedObjects.UserSettings.UpdateTokensAndSave(SharedObjects.ForgeConnection)
            Else
                SharedObjects.DoForgeAuth()
                SharedObjects.UserSettings.UpdateTokensAndSave(SharedObjects.ForgeConnection)
            End If
        End If

        ''show a list of BIM projects that we have access to
        Dim frmSelectProj As New frmSelectBIMProject
        frmSelectProj.ForgeConnection = SharedObjects.ForgeConnection
        frmSelectProj.StartPosition = Windows.Forms.FormStartPosition.CenterParent

        If frmSelectProj.ShowDialog = Windows.Forms.DialogResult.OK Then
            SharedObjects.ForgeConnection.SelectedProject = frmSelectProj.SelectedProject
            txtBIM360Project.Text = SharedObjects.ForgeConnection.SelectedProject.Name
            txtBIM360Project.BackColor = System.Drawing.Color.White

            grpBIM360Category.Enabled = True
            txtBIM360Category.Text = "No category Selected"
        Else

        End If
    End Sub

    Private Sub GridView1_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles GridView1.SelectionChanged
        grpsync.Enabled = GridView1.SelectedRowsCount > 0

        ''make sure that no disabled rows are selected
        GridView1.BeginUpdate()
        Dim reqAtt = SharedObjects.ForgeConnection.Attributes.Where(Function(at) at.RequiredOnIngress).ToList
        For Each i As Integer In GridView1.GetSelectedRows
            If modelElements(i).SyncStatus = SyncStatusEnum.Exists Or modelElements(i).SyncStatus = SyncStatusEnum.Added Then GridView1.UnselectRow(i)
        Next

        For Each i As Integer In GridView1.GetSelectedRows
            ''check if we have any values missing for attributes that are mandatory
            Dim aInfo = modelElements(i)

            ''barcode is needed as well

        Next

        'For i As Integer = 0 To modelElements.Count - 1
        '    If modelElements(i).Exists Then GridView1.UnselectRow(i)
        'Next
        GridView1.EndUpdate()
    End Sub

    Private Sub GridView1_CellValueChanged(sender As Object, e As CellValueChangedEventArgs) Handles GridView1.CellValueChanged
        If e.Value Is Nothing Then
            Debug.WriteLine("Cell value changed: nothing")
            Exit Sub
        End If

        Debug.WriteLine("Cell value changed:" + e.Value.ToString)
    End Sub

    Private Sub ParseStatusValue(sender As Object, e As ConvertEditValueEventArgs)
        If e.Value Is Nothing Then e.Value = ""
        e.Value = e.Value.ToString
        e.Handled = True
    End Sub

    Private Sub cmbStatusHandleCloseUp(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.CloseUpEventArgs)
        Dim aInfo As AssetInformation = GridView1.GetFocusedRow
        If String.IsNullOrEmpty(e.Value.ToString) Then
            aInfo.StatusId = ""
            Exit Sub
        End If

        If Not TypeOf (e.Value) Is ForgeStatus Then Exit Sub
        aInfo.StatusId = CType(e.Value, ForgeStatus).ID
    End Sub

    Private Sub btnSync_Click(sender As Object, e As EventArgs) Handles btnSync.Click
        If Not SharedObjects.IsConnectionActive Then
            If Not SharedObjects.ForgeConnection.AccessToken = "" Then
                SharedObjects.ForgeConnection.GetNewRefreshToken()
                SharedObjects.UserSettings.UpdateTokensAndSave(SharedObjects.ForgeConnection)
            Else
                SharedObjects.DoForgeAuth()
                SharedObjects.UserSettings.UpdateTokensAndSave(SharedObjects.ForgeConnection)
            End If
        End If

        ''have we selected a status step?
        Dim missingStatus As Boolean = False
        For Each rowHandle As Integer In GridView1.GetSelectedRows
            Dim ainfo As AssetInformation = CType(GridView1.GetRow(rowHandle), AssetInformation)
            If String.IsNullOrWhiteSpace(ainfo.StatusId) Then missingStatus = True
        Next
        If missingStatus Then
            MsgBox("The selected rows have no Asset Status selected, assign asset status and retry", MsgBoxStyle.OkOnly Or MsgBoxStyle.Information, "Missing Asset Status")
            Exit Sub
        End If

        ''
        SharedObjects.ForgeConnection.GetCustomAttributesForCategory()

        ''need projectId, catId, Status (for each item)
        Dim aList As New List(Of NewForgeAssets)

        For Each rowHandle As Integer In GridView1.GetSelectedRows
            Dim ainfo As AssetInformation = CType(GridView1.GetRow(rowHandle), AssetInformation)
            Dim nFAsset As New NewForgeAssets
            nFAsset.barcode = ""
            nFAsset.description = ainfo.Description
            nFAsset.clientAssetId = ainfo.ID
            nFAsset.statusId = ainfo.StatusId
            nFAsset.categoryId = ainfo.CategoryID

            ''attributes
            'e.Value = DataRow.AssetParamaters(e.Column.FieldName)

            For Each key As String In ainfo.AssetParamaters.Keys
                Dim bimDisplayName As String = key.Replace("ASSETS.", "")

                If Not ainfo.AssetParamaters(key) Is Nothing Then
                    If bimDisplayName.ToUpperInvariant = "BARCODE" Then
                        nFAsset.barcode = ainfo.AssetParamaters(key)
                        Continue For
                    End If

                    Dim atIndex As Integer = SharedObjects.ForgeConnection.Attributes.FindIndex(Function(at) at.Displayname.ToUpperInvariant = bimDisplayName.ToUpperInvariant)
                    If atIndex <= -1 Then Continue For

                    Dim atName As String = SharedObjects.ForgeConnection.Attributes.Item(atIndex).Name
                    ''only attributes that exist in bim are added
                    nFAsset.customAttributes.Add(atName, ainfo.AssetParamaters(key))
                End If
            Next

            If String.IsNullOrEmpty(nFAsset.barcode) Then nFAsset.barcode = Nothing
            aList.Add(nFAsset)
        Next

        Dim results = SharedObjects.ForgeConnection.CreateAssets(aList)

        ''the results has the asset id/guid we need to store this, the asset name/clientasstid and the element unique id
        Dim projAssetInfo As New AssetProjectInformation
        projAssetInfo.BIMCategory = SharedObjects.ForgeConnection.SelectedCategory.Name
        projAssetInfo.BIMProject = SharedObjects.ForgeConnection.SelectedProject.Name
        projAssetInfo.DocumentName = System.IO.Path.GetFileName(RevitDocument.PathName)
        projAssetInfo.DocumentGUID = ExportUtils.GetGBXMLDocumentId(RevitDocument).ToString

        For Each newAsset In results
            Dim assetEl As New AssetElementInfo
            Dim rowHandle As Integer = modelElements.FindIndex(Function(r) r.ID = newAsset.ClientAssetId)

            modelElements(rowHandle).SyncStatus = SyncStatusEnum.Added
            assetEl.ElementUniqueId = modelElements(rowHandle).uniqueID
            assetEl.ClientAssetID = newAsset.ClientAssetId
            assetEl.AssetID = newAsset.Id

            projAssetInfo.ElementAssetList.Add(assetEl)
        Next

        GridView1.RefreshData()
        GridView1.ClearSelection()

        If SharedObjects.UserAssetProjectSettings.ContainsKey(projAssetInfo.DocumentGUID) Then
            SharedObjects.UserAssetProjectSettings.Remove(projAssetInfo.DocumentGUID)
        End If
        SharedObjects.UserAssetProjectSettings.Add(projAssetInfo.DocumentGUID, projAssetInfo)
        AssetProjectSettings.Savesettings(SharedObjects.UserAssetProjectSettings)

        XtraMessageBox.Show("Assets added to BM360", "Completed", MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub

    Private Sub frmAddAssetsToBim_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        If SharedObjects.ForgeConnection Is Nothing Then Exit Sub
        If SharedObjects.ForgeConnection.SelectedModelCategory Is Nothing Then
            Exit Sub
        Else
            Dim c = cmbModelCategory.Properties.Items.OfType(Of modelCategory).FirstOrDefault(Function(i) i.Name = SharedObjects.ForgeConnection.SelectedModelCategory.Name)
            cmbModelCategory.EditValue = c
        End If

        If SharedObjects.ForgeConnection.SelectedProject Is Nothing Then
            Exit Sub
        Else
            txtBIM360Project.Text = SharedObjects.ForgeConnection.SelectedProject.Name
            txtBIM360Project.BackColor = System.Drawing.Color.White
            grpBIM360Category.Enabled = True
        End If

        If SharedObjects.ForgeConnection.SelectedModelCategory Is Nothing Then
            Exit Sub
        Else
            txtBIM360Category.Text = SharedObjects.ForgeConnection.SelectedCategory.Name
            txtBIM360Category.BackColor = System.Drawing.Color.White

            'grpsync.Enabled = True
            'SharedObjects.ForgeConnection.GetStatuses()
            SharedObjects.ForgeConnection.GetCategoryStatusStepID(True)
            cmbStatusCellEditor.Items.AddRange(SharedObjects.ForgeConnection.Statuses)

            ''update the modelelements with the category and the first status in the list
            For Each asset As AssetInformation In modelElements
                asset.AssetStatusID = SharedObjects.ForgeConnection.Statuses(0).ID
                asset.AssetStatus = SharedObjects.ForgeConnection.Statuses(0).Label
                asset.StatusId = SharedObjects.ForgeConnection.Statuses(0).ID
            Next
            GridView1.RefreshData()
        End If
    End Sub

    Private Sub DisabledCellEventHandler(ByVal sender As Object, ByVal e As DevExpress.Utils.Behaviors.Common.ProcessCellEventArgs)
        Dim row As AssetInformation = GridView1.GetRow(e.RecordId)
        e.Disabled = row.SyncStatus = SyncStatusEnum.Exists Or row.SyncStatus = SyncStatusEnum.Added
    End Sub

    Private Sub ToolTipController1_GetActiveObjectInfo(sender As Object, e As ToolTipControllerGetActiveObjectInfoEventArgs) Handles ToolTipController1.GetActiveObjectInfo
        If e.Info Is Nothing AndAlso e.SelectedControl Is GridControl1 Then
            Dim view As GridView = TryCast(GridControl1.FocusedView, GridView)
            Dim info As GridHitInfo = view.CalcHitInfo(e.ControlMousePosition)
            Dim dRow As AssetInformation = view.GetRow(info.RowHandle)

            If dRow Is Nothing Then Exit Sub

            If info.InRowCell Then
                Select Case info.Column.Name
                    Case "imgCol"
                        If Not dRow.SyncStatus = SyncStatusEnum.Exists Then Exit Sub

                        Dim cellKey As String = info.RowHandle.ToString() & " - " & info.Column.ToString()
                        e.Info = New ToolTipControlInfo(cellKey, "Asset already exists in BIM360")
                End Select

            End If

        End If
    End Sub

    Private Sub GridView1_CustomDrawCell(sender As Object, e As RowCellCustomDrawEventArgs) Handles GridView1.CustomDrawCell
        Dim dRow As AssetInformation = GridView1.GetRow(e.RowHandle)
        If dRow Is Nothing Then Exit Sub

        Select Case True
            Case e.Column.Name = "imgCol" And dRow.SyncStatus = SyncStatusEnum.None
                Dim cellInfo As GridCellInfo = TryCast(e.Cell, GridCellInfo)
                Dim info As TextEditViewInfo = TryCast(cellInfo.ViewInfo, TextEditViewInfo)
                If info IsNot Nothing Then
                    info.ContextImage = My.Resources.Blank
                    info.CalcViewInfo()
                End If
            Case e.Column.Name = "imgCol" And dRow.SyncStatus = SyncStatusEnum.Exists
                Dim cellInfo As GridCellInfo = TryCast(e.Cell, GridCellInfo)
                Dim info As TextEditViewInfo = TryCast(cellInfo.ViewInfo, TextEditViewInfo)
                If info IsNot Nothing Then
                    info.ContextImage = My.Resources.Warning_16
                    info.CalcViewInfo()
                End If

            Case e.Column.Name = "imgCol" And dRow.SyncStatus = SyncStatusEnum.HasMissingValues
                Dim cellInfo As GridCellInfo = TryCast(e.Cell, GridCellInfo)
                Dim info As TextEditViewInfo = TryCast(cellInfo.ViewInfo, TextEditViewInfo)
                If info IsNot Nothing Then
                    info.ContextImage = My.Resources.information_16
                    info.CalcViewInfo()
                End If

            Case e.Column.Name = "imgCol" And dRow.SyncStatus = SyncStatusEnum.Added
                Dim cellInfo As GridCellInfo = TryCast(e.Cell, GridCellInfo)
                Dim info As TextEditViewInfo = TryCast(cellInfo.ViewInfo, TextEditViewInfo)
                If info IsNot Nothing Then
                    info.ContextImage = My.Resources.checked_16
                    info.CalcViewInfo()
                End If
        End Select

        'Select Case True
        '    Case dRow.SyncStatus = SyncStatusEnum.None
        '    Case e.Column.Name = "imgCol" And dRow.SyncStatus = SyncStatusEnum.Exists
        '        ''item already exists in BIM360
        '        e.DefaultDraw()

        '        Dim img = My.Resources.Warning_16
        '        Dim p As New System.Drawing.Point
        '        p.X = e.Bounds.Location.X + 3
        '        p.Y = e.Bounds.Location.Y + 2
        '        'p.X = e.Bounds.Location.X + (e.Bounds.Width - (img.Width)) / 2
        '        'p.Y = e.Bounds.Location.Y + (e.Bounds.Height - img.Height) / 2
        '        'e.Graphics.DrawImage(img, p)
        '        e.Cache.DrawImage(img, p)

        '        Dim r As System.Drawing.Rectangle = e.Bounds
        '        r.X = p.X + img.Width + 3
        '        'e.Appearance.DrawString(e.Cache, "Asset exists in BIM360", r)

        '        e.Handled = True

        '    Case e.Column.Name = "imgCol" And dRow.SyncStatus = SyncStatusEnum.HasMissingValues
        '        e.DefaultDraw()

        '        Dim img = My.Resources.information_16
        '        Dim p As New System.Drawing.Point
        '        p.X = e.Bounds.Location.X + (e.Bounds.Width - (img.Width)) / 2
        '        p.Y = e.Bounds.Location.Y + (e.Bounds.Height - img.Height) / 2
        '        e.Graphics.DrawImage(img, p)

        '        e.Handled = True
        '    Case e.Column.Name = "imgCol" And dRow.SyncStatus = SyncStatusEnum.Added
        '        e.DefaultDraw()

        '        Dim img = My.Resources.checked_16
        '        Dim p As New System.Drawing.Point
        '        p.X = e.Bounds.Location.X + (e.Bounds.Width - (img.Width)) / 2
        '        p.Y = e.Bounds.Location.Y + (e.Bounds.Height - img.Height) / 2
        '        e.Graphics.DrawImage(img, p)

        '        e.Handled = True
        'End Select


    End Sub

    Private Sub GridView1_MouseDown(sender As Object, e As MouseEventArgs) Handles GridView1.MouseDown
        ''cancel the row selection if the
        Dim hitInfo As GridHitInfo = GridView1.CalcHitInfo(e.Location)

        Select Case hitInfo.HitTest
            Case GridHitTest.RowCell
                Dim dRow As AssetInformation = GridView1.GetRow(hitInfo.RowHandle)
                CType(e, DXMouseEventArgs).Handled = dRow.SyncStatus = SyncStatusEnum.Exists
            Case GridHitTest.Column
                If Not hitInfo.Column.Caption = "Selection" Then Exit Sub
                If hitInfo.Column.Tag Is Nothing Then hitInfo.Column.Tag = ""

                If hitInfo.Column.Tag = "ALL" Then
                    GridView1.ClearSelection()
                    hitInfo.Column.Tag = "NONE"
                    CType(e, DXMouseEventArgs).Handled = True
                Else
                    ''select rows that arent disabled
                    For i As Integer = 0 To modelElements.Count - 1
                        If Not modelElements(i).SyncStatus = SyncStatusEnum.Exists Then GridView1.SelectRow(i)
                    Next

                    hitInfo.Column.Tag = "ALL"
                    CType(e, DXMouseEventArgs).Handled = True
                End If
        End Select




    End Sub
End Class