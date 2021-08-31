Imports Autodesk.Revit.Attributes
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI

<Transaction(TransactionMode.Manual)>
Public Class ViewDoorAssets
    Implements IExternalCommand

#Region " Private "
    Private _revitdoc As Document
#End Region

    Public Function Execute(commandData As ExternalCommandData, ByRef message As String, elements As ElementSet) As Result Implements IExternalCommand.Execute
        _revitdoc = commandData.Application.ActiveUIDocument.Document

        'Dim assetList As New List(Of AssetInformation)
        'Dim collector As New FilteredElementCollector(_revitdoc)
        'Dim collection = collector.OfClass(GetType(FamilyInstance)).OfCategory(BuiltInCategory.OST_Doors).ToElements

        'Dim count As Integer = 1
        'For Each el As Element In collection

        '    Dim aInfo As New AssetInformation
        '    aInfo.ID = "DOORS-" + count.ToString
        '    aInfo.CategoryID = el.Category.Name
        '    aInfo.StatusId = "Specified"
        '    aInfo.Description = el.Name
        '    aInfo.Barcode = el.UniqueId

        '    ''
        '    Dim instance As FamilyInstance = CType(el, FamilyInstance)
        '    Dim symbol As FamilySymbol = instance.Symbol

        '    If Not symbol Is Nothing Then
        '        Dim param As Parameter = symbol.Parameter(BuiltInParameter.ALL_MODEL_MANUFACTURER)
        '        If Not param Is Nothing Then aInfo.Manufacturer = param.AsString

        '        param = symbol.Parameter(BuiltInParameter.ALL_MODEL_MODEL)
        '        If Not param Is Nothing Then aInfo.Model = param.AsString
        '    End If

        '    assetList.Add(aInfo)
        '    count += 1
        'Next

        Dim frmViewModelAsset As New frmModelAssets
        frmViewModelAsset.RevitDocument = _revitdoc
        frmViewModelAsset.StartPosition = Windows.Forms.FormStartPosition.CenterParent
        frmViewModelAsset.ShowDialog()

        Return Result.Succeeded
    End Function
End Class
