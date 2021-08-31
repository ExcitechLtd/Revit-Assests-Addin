Public Class ForgeCategory
    Public Property ID As String
    Public Property Name As String

    Public Property SubCategorys As List(Of String)

    Public Property IsRoot As Boolean
    Public Property IsLeaf As Boolean
    Public Sub New()
        SubCategorys = New List(Of String)
        IsRoot = False
        IsLeaf = False
    End Sub

End Class
