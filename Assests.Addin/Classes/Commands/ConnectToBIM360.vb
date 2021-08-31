Imports Autodesk.Revit.Attributes
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI

<Transaction(TransactionMode.Manual)>
Public Class ConnectToBIM360
    Implements IExternalCommand

    Public Function Execute(commandData As ExternalCommandData, ByRef message As String, elements As ElementSet) As Result Implements IExternalCommand.Execute
        SharedObjects.ForgeConnection = New ForgeConnection
        ''load the settings just in case anything has changed since Revit started

        If Not SharedObjects.ForgeConnection.AccessToken = "" Then
            SharedObjects.ForgeConnection.GetNewRefreshToken()
        Else
            If Not SharedObjects.DoForgeAuth Then Return Result.Failed
        End If

        SharedObjects.UserSettings.AccessToken = SharedObjects.ForgeConnection.AccessToken
        SharedObjects.UserSettings.RefreshToken = SharedObjects.ForgeConnection.RefreshToken
        SharedObjects.UserSettings.TokenExpiry = SharedObjects.ForgeConnection.TokenExpiry
        SharedObjects.UserSettings.TokenType = SharedObjects.ForgeConnection.TokenType
        SharedObjects.UserSettings.SaveSettings()

        ''enable the other buttons

        Return Result.Succeeded
    End Function
End Class
