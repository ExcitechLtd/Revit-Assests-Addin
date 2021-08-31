Imports Autodesk.Revit.DB
Imports DevExpress.Data
Imports DevExpress.XtraGrid.Views.Base

Public Class frmDeleteAssets

    Private modelElements As List(Of AssetInformation)
    Public RevitDocument As Document

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

            grpPreviewDelete.Enabled = True
        End If
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

        ''pull the category custom attributes add these as columns
        SharedObjects.ForgeConnection.GetCustomAttributes()

        ''pull the category assets and match them with the model elements
        SharedObjects.ForgeConnection.GetAssets()

        modelElements = New List(Of AssetInformation)
        ''convert the asset object an assetinformation object
        For Each fAsset In SharedObjects.ForgeConnection.Assets.Values

            Dim aInfo As New AssetInformation
            aInfo.ID = fAsset.ClientAssetId
            aInfo.Description = fAsset.Description
            aInfo.NotInModel = True
            aInfo.SyncStatus = "Asset not in model"
            aInfo.SyncAction = "Disabled"

            modelElements.Add(aInfo)

        Next

        CreateGridColumns()
        GridControl1.DataSource = modelElements
        GridView1.OptionsView.ColumnAutoWidth = False
        GridView1.BestFitColumns()
    End Sub

    Private Sub GridView1_CustomUnboundColumnData(sender As Object, e As CustomColumnDataEventArgs) Handles GridView1.CustomUnboundColumnData
        If sender Is Nothing Then Exit Sub

        If e.IsGetData Then

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
            If Not bimAsset.Attributes.TryGetValue(e.Column.FieldName, value) Then e.Value = "" Else e.Value = value

        End If


    End Sub

    Private Sub CreateGridColumns()

        ''clear columns
        GridView1.Columns.Clear()
        Dim col As DevExpress.XtraGrid.Columns.GridColumn


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

        col = New DevExpress.XtraGrid.Columns.GridColumn With {.Name = "colBarcode", .Caption = "Barcode", .FieldName = "Barcode", .Visible = True}
        col.OptionsColumn.AllowEdit = False
        GridView1.Columns.Add(col)

        col = New DevExpress.XtraGrid.Columns.GridColumn With {.Name = "colSyncStatus", .Caption = "Sync Status", .FieldName = "SyncStatus", .Visible = True}
        col.OptionsColumn.AllowEdit = False
        GridView1.Columns.Add(col)
        '

        For Each _col As ForgeAttribute In SharedObjects.ForgeConnection.Attributes
            Dim colName As String = "colAtt" + _col.Name.Replace(" ", "_")
            'colName
            col = New DevExpress.XtraGrid.Columns.GridColumn With {.Name = colName, .Caption = _col.Displayname, .Visible = True, .FieldName = _col.Name, .UnboundType = DevExpress.Data.UnboundColumnType.String}
            col.OptionsColumn.AllowEdit = False
            GridView1.Columns.Add(col)

        Next

    End Sub

    Private Sub GridView1_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles GridView1.SelectionChanged
        grpDeleteAsset.Enabled = GridView1.SelectedRowsCount > 0
    End Sub

    Private Sub btnDeleteAssets_Click(sender As Object, e As EventArgs) Handles btnDeleteAssets.Click
        If Not SharedObjects.IsConnectionActive Then
            If Not SharedObjects.ForgeConnection.AccessToken = "" Then
                SharedObjects.ForgeConnection.GetNewRefreshToken()
                SharedObjects.UserSettings.UpdateTokensAndSave(SharedObjects.ForgeConnection)
            Else
                SharedObjects.DoForgeAuth()
                SharedObjects.UserSettings.UpdateTokensAndSave(SharedObjects.ForgeConnection)
            End If
        End If

        Dim deleteIDs As New List(Of String)

        For Each row In GridView1.GetSelectedRows
            Dim datarow As AssetInformation = GridView1.GetRow(row)

            Dim itemGuid As String = SharedObjects.ForgeConnection.Assets.Item(datarow.ID).Id

            deleteIDs.Add(itemGuid)
        Next

        SharedObjects.ForgeConnection.DeleteAssets(deleteIDs)

        ''remove the deleted asset from the saved assets
        SharedObjects.UserAssetProjectSettings = AssetProjectSettings.LoadSettings()
        If SharedObjects.UserAssetProjectSettings.ContainsKey(ExportUtils.GetGBXMLDocumentId(RevitDocument).ToString) Then
            Dim assetProjSet = SharedObjects.UserAssetProjectSettings(ExportUtils.GetGBXMLDocumentId(RevitDocument).ToString)

            For Each itemGuid In deleteIDs
                Dim index = assetProjSet.ElementAssetList.FindIndex(Function(a) a.AssetID.ToUpperInvariant = itemGuid.ToUpperInvariant)
                If Not index = -1 Then assetProjSet.ElementAssetList.RemoveAt(index)
            Next

            SharedObjects.UserAssetProjectSettings(ExportUtils.GetGBXMLDocumentId(RevitDocument).ToString) = assetProjSet
            AssetProjectSettings.Savesettings(SharedObjects.UserAssetProjectSettings)
        End If

        ''refresh the data in the grid
        ''pull the category assets and match them with the model elements
        SharedObjects.ForgeConnection.GetAssets()

        modelElements = New List(Of AssetInformation)
        ''convert the asset object an assetinformation object
        For Each fAsset In SharedObjects.ForgeConnection.Assets.Values

            Dim aInfo As New AssetInformation
            aInfo.ID = fAsset.ClientAssetId
            aInfo.Description = fAsset.Description
            aInfo.NotInModel = True
            aInfo.SyncStatus = "Asset not in model"
            aInfo.SyncAction = "Disabled"

            modelElements.Add(aInfo)

        Next

        GridControl1.DataSource = modelElements
        GridView1.OptionsView.ColumnAutoWidth = False
        GridView1.BestFitColumns()

    End Sub

    Private Sub frmDeleteAssets_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub frmDeleteAssets_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        If SharedObjects.ForgeConnection Is Nothing Then Exit Sub

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
        End If
    End Sub
End Class