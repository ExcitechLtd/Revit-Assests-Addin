Imports Autodesk.Revit.Attributes
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI

<Transaction(TransactionMode.Manual)>
Public Class Read
    Implements IExternalCommand

#Region " Private "
    Private _revitdoc As Document
#End Region

    Public Function Execute(commandData As ExternalCommandData, ByRef message As String, elements As ElementSet) As Result Implements IExternalCommand.Execute
        _revitdoc = commandData.Application.ActiveUIDocument.Document

        Dim listSchedules = FamilyHelper.GetSchedules(_revitdoc)

        Dim frmRead As New frmSchedules
        frmRead.document = _revitdoc
        frmRead.Schedules = listSchedules
        frmRead.StartPosition = Windows.Forms.FormStartPosition.CenterParent
        frmRead.ShowDialog()

        Return Result.Succeeded
    End Function
End Class
