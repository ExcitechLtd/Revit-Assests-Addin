Public Class ForgeStatus
    Public Property ID As String
    Public Property Label As String

    Public Overrides Function Tostring() As String
        Return Label
    End Function
End Class
