Public Class ForgeAsset

    Public Property Id As String
    Public Property ClientAssetId As String
    Public Property Description As String
    Public Property Attributes As Dictionary(Of String, String)
    Public Property Barcode As String
    Public Property StatusID As String
    Public Sub New()
        Attributes = New Dictionary(Of String, String)
    End Sub
End Class
