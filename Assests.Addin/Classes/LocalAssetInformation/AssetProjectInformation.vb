Public Class AssetProjectInformation

    Public Property DocumentGUID As String
    Public Property DocumentName As String
    Public Property BIMProject As String
    Public Property BIMCategory As String
    Public Property ElementCategory As String

    Public Property ElementAssetList As List(Of AssetElementInfo)

    Public Sub New()
        ElementAssetList = New List(Of AssetElementInfo)
    End Sub
End Class


