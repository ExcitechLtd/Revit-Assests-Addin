Public Class modelCategory
    Public Property Name As String
    Public Property ID As Integer

    Public Overrides Function ToString() As String
        Return Name
    End Function
End Class
