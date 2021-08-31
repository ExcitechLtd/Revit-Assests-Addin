Imports Autodesk.Revit.Attributes
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI

<Transaction(TransactionMode.Manual)>
Public Class AddCategory
    Implements IExternalCommand

#Region " Private "
    Private _revitdoc As Document
#End Region

    Public Function Execute(commandData As ExternalCommandData, ByRef message As String, elements As ElementSet) As Result Implements IExternalCommand.Execute
        _revitdoc = commandData.Application.ActiveUIDocument.Document

        Dim frmview As New frmCreateNewCategory
        'frmview.RevitDocument = _revitdoc
        frmview.StartPosition = Windows.Forms.FormStartPosition.CenterParent
        frmview.ShowDialog()

        Return Result.Succeeded
    End Function
End Class
