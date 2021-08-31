Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports Autodesk.Revit.DB
Imports DevExpress.Data
Imports DevExpress.Data.Extensions
Imports DevExpress.Utils
Imports DevExpress.Utils.Menu
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.ViewInfo
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo

Public Class frmViewBIMAssets
    Private _modelCatList As List(Of modelCategory)
    Private modelElements As BindingList(Of AssetInformation)
    Private _showModelAtt As Boolean = False ''default is to show the bim attributes
    Public RevitDocument As Document
    Private revitDocumentID As String

    Private txtEditItem As Repository.RepositoryItemTextEdit
    Private syncStatusEdit As Repository.RepositoryItemComboBox

    Private Sub btnBim360Project_Click(sender As Object, e As EventArgs) Handles btnBim360Project.Click
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

            cmbModelCategory.Enabled = True
            cmbModelCategory.Text = "No model category selected"

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
            grpModelCategory.Enabled = True
        End If


        '''have we added any assets from this project to bim360?
        'Dim projAssetInfo As New AssetProjectInformation
        'projAssetInfo.BIMCategory = SharedObjects.ForgeConnection.SelectedCategory.Name
        'projAssetInfo.BIMProject = SharedObjects.ForgeConnection.SelectedProject.Name
        'projAssetInfo.DocumentName = System.IO.Path.GetFileName(RevitDocument.PathName)
        'projAssetInfo.DocumentGUID = ExportUtils.GetGBXMLDocumentId(RevitDocument).ToString

    End Sub

    Private Sub btnPreview_Click(sender As Object, e As EventArgs) Handles btnPreview.Click
        If Not SharedObjects.IsConnectionActive Then
            If Not SharedObjects.ForgeConnection.AccessToken = "" Then
                SharedObjects.ForgeConnection.GetNewRefreshToken()
                SharedObjects.UserSettings.UpdateTokensAndSave(SharedObjects.ForgeConnection)
            Else
                SharedObjects.DoForgeAuth()
                SharedObjects.UserSettings.UpdateTokensAndSave(SharedObjects.ForgeConnection)
            End If
        End If

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
            modelEl.SyncStatus = SyncStatusEnum.None

            For Each def In paramGrp.Definitions
                Dim param = el.LookupParameter(def.Name)

                If param Is Nothing Then
                    modelEl.AssetParamaters.Add(def.Name, String.Empty)
                Else
                    modelEl.AssetParamaters.Add(def.Name, param.AsString)
                End If
            Next

        Next

        ''pull the category custom attributes add these as columns
        SharedObjects.ForgeConnection.GetCustomAttributes()

        ''pull the category assets and match them with the model elements
        SharedObjects.ForgeConnection.GetAssets()

        SharedObjects.ForgeConnection.GetCategoryStatusStepID(True)

        ''compare the lists
        modelElements = CompareAssetLists(modelElements)

        ''add attributes
        Select Case RadioGroup1.Properties.Items.Item(RadioGroup1.SelectedIndex).Value
            Case "bimatt"
                ''show the attributes from BIM360
                _showModelAtt = False
            Case "modelatt"
                ''show the attributes from the MODEL
                _showModelAtt = True
        End Select

        ''populate the category
        ''update the modelelements with the category
        For Each asset As AssetInformation In modelElements
            asset.Category = SharedObjects.ForgeConnection.SelectedCategory.Name
            asset.CategoryID = SharedObjects.ForgeConnection.SelectedCategory.ID

            asset.AssetStatus = SharedObjects.ForgeConnection.GetStatusStepNameFromID(asset.AssetStatusID)
        Next

        CreateGridColumns()
        UpdatecolumnImage()
        GridControl1.DataSource = modelElements
        GridView1.OptionsView.ColumnAutoWidth = False
        GridView1.BestFitColumns()

        grpsync.Enabled = True
    End Sub

    Private Function CompareAssetLists(modelElements As BindingList(Of AssetInformation)) As BindingList(Of AssetInformation)
        Dim fAsset As New ForgeAsset

        For Each modelelement As AssetInformation In modelElements
            ''does this model asset exist in the bim assets?
            ''check the asset / element id lookup we saved and have loaded in to the sharedobject

            Dim projInfo = SharedObjects.UserAssetProjectSettings(revitDocumentID)
            Dim aeIndex As Integer = projInfo.ElementAssetList.FindIndex(Function(f) f.ElementUniqueId.ToUpperInvariant = modelelement.uniqueID.ToUpperInvariant)

            If aeIndex > -1 Then
                modelelement.SyncStatus = SyncStatusEnum.InBIM360
            Else
                modelelement.SyncStatus = SyncStatusEnum.NotInBIM360
                modelelement.SyncAction = SyncActionEnum.Disabled
            End If

            'If SharedObjects.ForgeConnection.Assets.TryGetValue(modelelement.ID, fAsset) Then
            '    modelelement.SyncStatus = "Model Asset in BIM360"
            '    ''asset is in BIM360 

            'Else
            '    modelelement.SyncStatus = "Model Asset not in BIM360"
            '    modelelement.SyncAction = "Disabled"
            'End If
        Next

        ''are there any BIM assets that arent in the model
        For Each fAsset In SharedObjects.ForgeConnection.Assets.Values
            Dim aIndex As Integer = modelElements.FindIndex(Function(f) f.ID = fAsset.ClientAssetId)
            If Not aIndex = -1 Then
                modelElements(aIndex).AssetStatusID = fAsset.StatusID

                If Not modelElements(aIndex).SyncStatus = SyncStatusEnum.NotInBIM360 Then
                    Continue For
                End If
                modelElements(aIndex).SyncStatus = SyncStatusEnum.UnLinked
                modelElements(aIndex).SyncAction = SyncActionEnum.Disabled

                Continue For
            End If

            ''add this to the list of elements 
            Dim aInfo As New AssetInformation
            aInfo.ID = fAsset.ClientAssetId
            aInfo.Description = fAsset.Description
            aInfo.NotInModel = True
            aInfo.SyncStatus = SyncStatusEnum.NotInModel
            aInfo.SyncAction = SyncActionEnum.Disabled

            modelElements.Add(aInfo)
        Next

        Return modelElements
    End Function

    Private Sub CreateGridColumns()

        ''clear columns
        GridView1.Columns.Clear()
        Dim col As DevExpress.XtraGrid.Columns.GridColumn
        txtEditItem = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
        GridControl1.RepositoryItems.Add(txtEditItem)

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
        col.OptionsColumn.AllowEdit = False
        GridView1.Columns.Add(col)

        'col = New DevExpress.XtraGrid.Columns.GridColumn With {.Name = "colBarcode", .Caption = "Barcode", .FieldName = "Barcode", .Visible = True}
        'col.OptionsColumn.AllowEdit = False
        'GridView1.Columns.Add(col)

        syncStatusEdit = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox
        GridControl1.RepositoryItems.Add(syncStatusEdit)
        For Each en As SyncActionEnum In System.Enum.GetValues(GetType(SyncActionEnum))
            syncStatusEdit.Items.Add(en.GetDescription)
        Next
        AddHandler syncStatusEdit.CloseUp, AddressOf syncStatusEditCloseUp
        '
        col = GridView1.Columns.AddField("SyncActionEdit")
        col.Caption = "Sync Action"
        col.Visible = True
        col.ColumnEdit = syncStatusEdit
        col.UnboundType = DevExpress.Data.UnboundColumnType.String

        '     col = New DevExpress.XtraGrid.Columns.GridColumn With {.Name = "colSyncAction", .Caption = "Sync Action", .Visible = True, .ColumnEdit = syncStatusEdit, .UnboundType = DevExpress.Data.UnboundColumnType.String}
        GridView1.Columns.Add(col)

        ''model attributes
        For Each _str As String In FamilyHelper.GetAssetParamterNames(RevitDocument)
            Dim colName As String = "colAtt" + _str.Replace(" ", "_")
            Dim colCaption As String = _str.Replace("ASSETS.", "")
            'colName
            col = New DevExpress.XtraGrid.Columns.GridColumn With {.Name = colName, .Caption = colCaption, .Visible = True, .FieldName = _str, .UnboundType = DevExpress.Data.UnboundColumnType.String}
            col.OptionsColumn.AllowEdit = False
            GridView1.Columns.Add(col)
        Next
        'For Each _col As ForgeAttribute In SharedObjects.ForgeConnection.Attributes
        '    Dim colName As String = "colAtt" + _col.Name.Replace(" ", "_")
        '    'colName
        '    col = New DevExpress.XtraGrid.Columns.GridColumn With {.Name = colName, .Caption = _col.Displayname, .Visible = True, .FieldName = _col.Name, .UnboundType = DevExpress.Data.UnboundColumnType.String}
        '    col.OptionsColumn.AllowEdit = False
        '    GridView1.Columns.Add(col)

        'Next

    End Sub

    Private Sub cmbModelCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbModelCategory.SelectedIndexChanged
        grpPreview.Enabled = True
    End Sub

    Private Sub cmbModelCategory_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbModelCategory.SelectedValueChanged
        SharedObjects.ForgeConnection.SelectedModelCategory = cmbModelCategory.SelectedItem
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

        ''save the asset information

        ''sync from bim 360
        ''we already have the attributes so just set the paramater values
        For Each rowHandle As Integer In GridView1.GetSelectedRows
            Dim assetRow As AssetInformation = GridView1.GetRow(rowHandle)

            Select Case assetRow.SyncAction
                Case SyncActionEnum.SyncFromBIM360
                    SyncFromBim360(assetRow)
                Case SyncActionEnum.SyncToBIM360
                    SyncToBim360(assetRow)
            End Select
        Next

        XtraMessageBox.Show("Asset sync completed", "Completed", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub SyncToBim360(datarow As AssetInformation)
        Dim updatelist As New List(Of UpdateAsset)
        Dim upAsset As New UpdateAsset
        Dim bimAsset As New ForgeAsset

        bimAsset = SharedObjects.ForgeConnection.Assets(datarow.ID)

        upAsset.bimAssetID = bimAsset.Id

        For Each pKey As String In datarow.AssetParamaters.Keys.ToList
            Dim atValue As String = datarow.AssetParamaters(pKey)
            Dim displayName As String = pKey.Replace("ASSETS.", "")
            Dim atIndex As Integer

            If displayName.ToUpperInvariant = "BARCODE" Then
                upAsset.barcode = atValue
            Else
                atIndex = SharedObjects.ForgeConnection.Attributes.FindIndex(Function(f) f.Displayname.ToUpperInvariant = displayName.ToUpperInvariant)

                If atIndex <= -1 Then Continue For
                Dim atID As String = SharedObjects.ForgeConnection.Attributes(atIndex).Name

                Dim BimAtValue As String
                bimAsset.Attributes.TryGetValue(atID, BimAtValue)
                If atValue = BimAtValue Then Continue For
                If upAsset.customAttributes Is Nothing Then upAsset.customAttributes = New Dictionary(Of String, String)
                upAsset.CustomAttributes.Add(atID, atValue)
            End If


        Next

        updatelist.Add(upAsset)

        SharedObjects.ForgeConnection.UpdateAsset(updatelist)
    End Sub

    Private Sub SyncFromBim360(datarow As AssetInformation)
        ' SharedObjects.ForgeConnection.Attributes ''this has the bim attributes
        Dim bimAsset As ForgeAsset
        Dim atIndex As Integer
        Dim atValue As String

        If SharedObjects.ForgeConnection.Assets.TryGetValue(datarow.ID, bimAsset) Then
            ''update the attribute for this model element
            ''get the internal bim asset id as this is what is stored against the bimSset object

            For Each key As String In datarow.AssetParamaters.Keys.ToList
                Dim displayName As String = key.Replace("ASSETS.", "")

                If displayName.ToUpperInvariant = "BARCODE" Then
                    atValue = bimAsset.Barcode

                Else
                    atIndex = SharedObjects.ForgeConnection.Attributes.FindIndex(Function(f) f.Displayname.ToUpperInvariant = displayName.ToUpperInvariant)

                    If atIndex <= -1 Then Continue For
                    Dim atID As String = SharedObjects.ForgeConnection.Attributes(atIndex).Name
                    bimAsset.Attributes.TryGetValue(atID, atValue)
                End If


                Dim el As Element = RevitDocument.GetElement(datarow.uniqueID)
                Dim param = el.LookupParameter(key)

                If param Is Nothing Then Continue For ''if this paramater doesnt exist then contine

                If param.AsString = atValue Then Continue For ''skip if the values are the same

                Using tr As New Transaction(RevitDocument)
                    tr.Start("SyncFromBIM360")

                    param.Set(atValue)
                    tr.Commit()

                End Using

                datarow.SyncStatus = SyncStatusEnum.Synced
            Next
        End If

    End Sub

#Region " Cell Edit events "
    Private Sub syncStatusEditCloseUp(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.CloseUpEventArgs)
        Dim aInfo As AssetInformation = GridView1.GetFocusedRow
        If String.IsNullOrEmpty(e.Value.ToString) Then
            aInfo.SyncAction = SyncActionEnum.Disabled

            Exit Sub
        End If

        Dim sa As SyncActionEnum = System.Enum.Parse(GetType(SyncActionEnum), e.Value.ToString.Replace(" ", ""), True)

        aInfo.SyncAction = sa
    End Sub
#End Region

#Region " Grid Events "

#End Region

#Region " Grid Style "

    Private Sub GridView1_CustomUnboundColumnData(sender As Object, e As CustomColumnDataEventArgs) Handles GridView1.CustomUnboundColumnData
        If sender Is Nothing Then Exit Sub

        Debug.WriteLine(e.Column.Name)

        If e.IsGetData Then
            If e.Column.Caption = "Sync Action" Then
                Dim dataRow As AssetInformation = e.Row
                e.Value = dataRow.SyncActionValue
                Exit Sub
            End If

            'e.Column.FieldName ''this is the attributename
            If _showModelAtt Then
                ''get the model attributes
                Dim dataRow As AssetInformation = e.Row
                e.Value = dataRow.AssetParamaters(e.Column.FieldName)
            Else
                ''get the bim attributes
                Dim dataRow As AssetInformation = e.Row
                Dim bimAsset As ForgeAsset

                If Not SharedObjects.ForgeConnection.Assets.TryGetValue(dataRow.ID, bimAsset) Then
                    e.Value = ""
                    Exit Sub
                End If

                If bimAsset Is Nothing Then
                    e.Value = ""
                    Exit Sub
                End If


                Dim value As String = ""
                Dim displayName As String = e.Column.FieldName.Replace("ASSETS.", "")

                If displayName.ToUpperInvariant = "BARCODE" Then
                    e.Value = bimAsset.Barcode
                    Exit Sub
                End If

                Dim atIndex As Integer = SharedObjects.ForgeConnection.Attributes.FindIndex(Function(f) f.Displayname.ToUpperInvariant = displayName.ToUpperInvariant)

                If atIndex <= -1 Then
                    e.Value = ""
                    Exit Sub
                End If

                Dim atID As String = SharedObjects.ForgeConnection.Attributes(atIndex).Name
                If Not bimAsset.Attributes.TryGetValue(atID, value) Then e.Value = "" Else e.Value = value
            End If
        End If


    End Sub

    Private Sub frmViewBIMAssets_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddHandler disabledCellevent.ProcessingCell, AddressOf DisabledCellEventHandler

        _showModelAtt = Not RadioGroup1.SelectedIndex = 0

        ''load the saved element unique id asset mappings
        SharedObjects.UserAssetProjectSettings = AssetProjectSettings.LoadSettings()

        revitDocumentID = ExportUtils.GetGBXMLDocumentId(RevitDocument).ToString
    End Sub

    Private Sub RadioGroup1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RadioGroup1.SelectedIndexChanged
        _showModelAtt = Not RadioGroup1.SelectedIndex = 0

        UpdatecolumnImage()
    End Sub

    Private Sub frmViewBIMAssets_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        If SharedObjects.ForgeConnection Is Nothing Then Exit Sub

        If SharedObjects.ForgeConnection.SelectedProject Is Nothing Then
            Exit Sub
        Else
            txtBIM360Project.Text = SharedObjects.ForgeConnection.SelectedProject.Name
            txtBIM360Project.BackColor = System.Drawing.Color.White
            grpBIM360Category.Enabled = True
        End If

        If SharedObjects.ForgeConnection.SelectedCategory Is Nothing Then
            Exit Sub
        Else
            txtBIM360Category.Text = SharedObjects.ForgeConnection.SelectedCategory.Name
            txtBIM360Category.BackColor = System.Drawing.Color.White
        End If

        If SharedObjects.ForgeConnection.SelectedModelCategory Is Nothing Then
            Exit Sub
        Else
            cmbModelCategory.Enabled = True
            cmbModelCategory.Text = "No model category selected"

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
            grpModelCategory.Enabled = True

            Dim cat = cmbModelCategory.Properties.Items.OfType(Of modelCategory).FirstOrDefault(Function(i) i.Name = SharedObjects.ForgeConnection.SelectedModelCategory.Name)
            cmbModelCategory.EditValue = cat
        End If

        If Not SharedObjects.UserAssetProjectSettings.ContainsKey(revitDocumentID) Then
            XtraMessageBox.Show("No assets from this document have been added to BIM360, please use the 'Add Model Assets Option'", "No assets in BIM360", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
            PanelControl1.Enabled = False
            Exit Sub
        End If

    End Sub

    Private Sub UpdatecolumnImage()
        For Each col As GridColumn In GridView1.Columns
            If col.FieldName.ToUpperInvariant.StartsWith("ASSETS.") Then
                If _showModelAtt Then
                    col.ImageOptions.Image = My.Resources.Revit_16
                Else
                    col.ImageOptions.Image = My.Resources.BIM360_16
                End If
            End If
        Next

        GridView1.LayoutChanged()
    End Sub
#End Region

#Region " Disabled Cells / Rows "
    Private Sub DisabledCellEventHandler(ByVal sender As Object, ByVal e As DevExpress.Utils.Behaviors.Common.ProcessCellEventArgs)
        Dim row As AssetInformation = GridView1.GetRow(e.RecordId)
        e.Disabled = row.SyncStatus = SyncStatusEnum.NotInBIM360 _
            Or row.SyncStatus = SyncStatusEnum.NotInModel _
            Or row.SyncStatus = SyncStatusEnum.UnLinked _
             Or row.SyncStatus = SyncStatusEnum.Synced
    End Sub

    Private Sub GridView1_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles GridView1.SelectionChanged
        grpsync.Enabled = GridView1.SelectedRowsCount > 0

        ''make sure that no disabled rows are selected
        GridView1.BeginUpdate()

        For Each i As Integer In GridView1.GetSelectedRows
            If modelElements(i).SyncStatus = SyncStatusEnum.NotInBIM360 _
                Or modelElements(i).SyncStatus = SyncStatusEnum.NotInModel _
                Or modelElements(i).SyncStatus = SyncStatusEnum.UnLinked Then GridView1.UnselectRow(i)
        Next

        GridView1.EndUpdate()
    End Sub

    Private Sub GridView1_CustomDrawCell(sender As Object, e As RowCellCustomDrawEventArgs) Handles GridView1.CustomDrawCell
        Dim dRow As AssetInformation = GridView1.GetRow(e.RowHandle)
        If dRow Is Nothing Then Exit Sub

        Select Case True
            Case e.Column.Name = "imgCol" And dRow.SyncStatus = SyncStatusEnum.None
                Dim cellInfo As GridCellInfo = TryCast(e.Cell, GridCellInfo)
                Dim info As TextEditViewInfo = TryCast(cellInfo.ViewInfo, TextEditViewInfo)
                If info IsNot Nothing Then
                    info.ContextImage = My.Resources.blank
                    info.CalcViewInfo()
                End If
            Case e.Column.Name = "imgCol" And dRow.SyncStatus = SyncStatusEnum.InBIM360
                Dim cellInfo As GridCellInfo = TryCast(e.Cell, GridCellInfo)
                Dim info As TextEditViewInfo = TryCast(cellInfo.ViewInfo, TextEditViewInfo)
                If info IsNot Nothing Then
                    info.ContextImage = My.Resources.blank
                    info.CalcViewInfo()
                End If
            Case e.Column.Name = "imgCol" And dRow.SyncStatus = SyncStatusEnum.NotInBIM360
                Dim cellInfo As GridCellInfo = TryCast(e.Cell, GridCellInfo)
                Dim info As TextEditViewInfo = TryCast(cellInfo.ViewInfo, TextEditViewInfo)
                If info IsNot Nothing Then
                    info.ContextImage = My.Resources.stop_16
                    info.CalcViewInfo()
                End If
            Case e.Column.Name = "imgCol" And dRow.SyncStatus = SyncStatusEnum.NotInModel
                Dim cellInfo As GridCellInfo = TryCast(e.Cell, GridCellInfo)
                Dim info As TextEditViewInfo = TryCast(cellInfo.ViewInfo, TextEditViewInfo)
                If info IsNot Nothing Then
                    info.ContextImage = My.Resources.Warning_16
                    info.CalcViewInfo()
                End If
            Case e.Column.Name = "imgCol" And dRow.SyncStatus = SyncStatusEnum.UnLinked
                Dim cellInfo As GridCellInfo = TryCast(e.Cell, GridCellInfo)
                Dim info As TextEditViewInfo = TryCast(cellInfo.ViewInfo, TextEditViewInfo)
                If info IsNot Nothing Then
                    info.ContextImage = My.Resources.broken_link_16
                    info.CalcViewInfo()
                End If
            Case e.Column.Name = "imgCol" And dRow.SyncStatus = SyncStatusEnum.Synced
                Dim cellInfo As GridCellInfo = TryCast(e.Cell, GridCellInfo)
                Dim info As TextEditViewInfo = TryCast(cellInfo.ViewInfo, TextEditViewInfo)
                If info IsNot Nothing Then
                    info.ContextImage = My.Resources.checked_16
                    info.CalcViewInfo()
                End If
        End Select
    End Sub

    Private Sub GridView1_MouseDown(sender As Object, e As MouseEventArgs) Handles GridView1.MouseDown
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
                        If Not modelElements(i).SyncStatus = SyncStatusEnum.NotInBIM360 Or Not modelElements(i).SyncStatus = SyncStatusEnum.NotInModel Then GridView1.SelectRow(i)
                    Next

                    hitInfo.Column.Tag = "ALL"
                    CType(e, DXMouseEventArgs).Handled = True
                End If
        End Select
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        For Each ai In modelElements.ToList
            modelElements.Add(ai)
        Next

        Dim col1 = GridView1.Columns.ColumnByFieldName("ID")
        Dim gcsi1 As New GridColumnSortInfo(col1, ColumnSortOrder.Ascending)
        '
        Dim col2 = GridView1.Columns.ColumnByFieldName("Description")
        Dim gcsi2 As New GridColumnSortInfo(col2, ColumnSortOrder.Ascending)

        '  GridView1.SortInfo.ClearAndAddRange(New GridColumnSortInfo() {gcsi1, gcsi2}, 2)
        Dim gmsi As New GridMergedColumnSortInfo({col1, col2}, {ColumnSortOrder.Ascending, ColumnSortOrder.Ascending})
        GridView1.SortInfo.ClearAndAddRange({gmsi}, 1)
        GridView1.OptionsView.ShowGroupExpandCollapseButtons = False

        GridView1.ExpandAllGroups()
    End Sub
#End Region

#Region " Grid "

#End Region

End Class