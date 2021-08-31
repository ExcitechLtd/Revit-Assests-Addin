Imports Autodesk.Revit.Attributes
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI

<Transaction(TransactionMode.Manual)>
Public Class AddAssets
    Implements IExternalCommand

#Region " Private "
    Private _revitdoc As Document
#End Region

    Public Function Execute(commandData As ExternalCommandData, ByRef message As String, elements As ElementSet) As Result Implements IExternalCommand.Execute
        _revitdoc = commandData.Application.ActiveUIDocument.Document

        Dim frm As New frmAddAssetsToBim()
        frm.RevitDocument = _revitdoc
        frm.StartPosition = Windows.Forms.FormStartPosition.CenterParent
        frm.ShowDialog()

        Return Result.Succeeded
    End Function
End Class
