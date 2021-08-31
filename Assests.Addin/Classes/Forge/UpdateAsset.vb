Imports Newtonsoft.Json

Public Class UpdateAsset

    <JsonIgnore> Public Property bimAssetID As String
    Public Property clientAssetId As String
    Public Property customAttributes As Dictionary(Of String, String)
    Public Property barcode As String
    Public Sub New()
        customAttributes = Nothing
        barcode = Nothing
        clientAssetId = Nothing
        bimAssetID = Nothing
    End Sub

End Class
