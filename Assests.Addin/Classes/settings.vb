Imports System.Web.Script.Serialization
Public Class settings

#Region " Properties "
    Public Property AccessToken As String
    Public Property RefreshToken As String
    Public Property TokenType As String
    Public Property TokenExpiry As Date

#End Region

#Region " Priavte "
    Private Shared settingsPath As String
#End Region

#Region " Constructor "
    Public Sub New()
        settingsPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)
        settingsPath = IO.Path.Combine(settingsPath, "Revit Assets")

        If Not IO.Directory.Exists(settingsPath) Then IO.Directory.CreateDirectory(settingsPath)

        settingsPath = IO.Path.Combine(settingsPath, "Assets.settings")
    End Sub
#End Region

#Region " Methods "
    Public Sub UpdateTokensAndSave(forgeCN As ForgeConnection)
        AccessToken = SharedObjects.ForgeConnection.AccessToken
        RefreshToken = SharedObjects.ForgeConnection.RefreshToken
        TokenExpiry = SharedObjects.ForgeConnection.TokenExpiry
        TokenType = SharedObjects.ForgeConnection.TokenType

        Me.SaveSettings()
    End Sub

    Public Sub UpdateTokens(forgeCN As ForgeConnection)
        AccessToken = SharedObjects.ForgeConnection.AccessToken
        RefreshToken = SharedObjects.ForgeConnection.RefreshToken
        TokenExpiry = SharedObjects.ForgeConnection.TokenExpiry
        TokenType = SharedObjects.ForgeConnection.TokenType
    End Sub

    Public Function AreSettingsValid() As Boolean

        If String.IsNullOrWhiteSpace(AccessToken) Then Return False
        If String.IsNullOrWhiteSpace(RefreshToken) Then Return False
        If String.IsNullOrWhiteSpace(TokenType) Then Return False
        If String.IsNullOrWhiteSpace(TokenType) Then Return False

        Return True
    End Function
#End Region

#Region " Save/Load "
    Public Sub ClearSettings()
        IO.File.Delete(settingsPath)
    End Sub

    Public Sub SaveSettings()

        Dim jSonSerializer As New JavaScriptSerializer
        Dim jsonSettings = jSonSerializer.Serialize(Me)

        IO.File.WriteAllText(settingsPath, jsonSettings)
    End Sub

    Public Shared Function LoadSettings() As settings
        Dim ret As New settings

        If Not IO.File.Exists(settingsPath) Then Return ret

        'read JSON settings data
        Dim jsonSettings = IO.File.ReadAllText(settingsPath)

        'de-serialize settings and return object
        Dim jSonSerializer As New JavaScriptSerializer
        Return jSonSerializer.Deserialize(Of settings)(jsonSettings)

        Return ret
    End Function

#End Region

End Class
