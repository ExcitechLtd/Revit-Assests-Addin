Public Class NewForgeAssets
    Public clientAssetId As String
    Public categoryId As String
    Public statusId As String ''guid

    Public barcode As String
    Public description As String

    Public customAttributes As Dictionary(Of String, Object)

    Public Sub New()
        customAttributes = New Dictionary(Of String, Object)
    End Sub
End Class
