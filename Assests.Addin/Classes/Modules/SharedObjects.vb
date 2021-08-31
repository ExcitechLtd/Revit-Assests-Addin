Module SharedObjects

    Public ReadOnly BaseURI As String = "https://developer.api.autodesk.com"
    Public ReadOnly ClientID As String = "dGQVFmXIk1ky45dp6PhUrANGoGLLtQJr"
    Public ReadOnly Secret As String = "gzLN2wZABI7CJg1c"
    Public ReadOnly RedirectURL As String = "http://localhost:3000"

    Public ForgeConnection As ForgeConnection
    Public UserSettings As settings
    Public UserAssetProjectSettings As AssetProjectSettings

    Public Function IsConnectionActive() As Boolean
        Try
            ''see if we can list the hubs
            ForgeConnection.GetHubs()

            If ForgeConnection.Hubs.Count <= 0 Then Return False
        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function

    Public Function DoForgeAuth() As Boolean
        Dim oauthUrl As String = GetAuthURL()

        Dim frmBrowser As New frmBrowser
        frmBrowser.URL = oauthUrl
        frmBrowser.TopMost = True
        frmBrowser.StartPosition = Windows.Forms.FormStartPosition.CenterParent
        frmBrowser.ShowDialog()

        Dim code As String = frmBrowser.AuthCode

        If String.IsNullOrWhiteSpace(code) Then
            MsgBox("Error authentication code is empty")
            Return False
        End If

        SharedObjects.ForgeConnection = ForgeConnection.WithCode(code)

        Return True
    End Function

    Public Function GetAuthURL() As String

        Dim url As String = "https://developer.api.autodesk.com/authentication/v1/authorize?"
        url += "response_type=code"
        url += "&client_id=" + Web.HttpUtility.UrlEncode(ClientID)
        url += "&redirect_uri=" + Web.HttpUtility.UrlEncode(RedirectURL)
        url += "&scope=data:read%20data:write%20data:create"

        Return url
    End Function

End Module
