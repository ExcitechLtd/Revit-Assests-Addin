Imports RestSharp
Public Class Logging

    Private Shared ReadOnly Property GetAppPath As String
        Get
            Dim ret As String = ""
            ret = System.Reflection.Assembly.GetExecutingAssembly.Location
            ret = System.IO.Path.GetDirectoryName(ret)
            Return ret
        End Get
    End Property


    Public Shared Sub WriteToLog(value As String)


        Dim path As String = IO.Path.Combine(GetAppPath, "Logs")
        If Not IO.Directory.Exists(path) Then
            IO.Directory.CreateDirectory(path)
        End If

        If Not path.EndsWith("\") Then path += "\"

        Dim fName As String = "Assets_" + Now.ToString("ddMMyyyy") + ".log"
        path += fName

        Using sr As New IO.StreamWriter(path, True)
            sr.WriteLine(value)
        End Using

    End Sub

    Public Shared Function RequestToString(request As RestRequest) As String

        Dim ret As String
        Dim retList As New List(Of String)

        Try

            Dim rc As IRestClient
            rc = New RestClient
            rc.BaseUrl = ForgeConnection.BaseUri

            retList.Add("URL: " + rc.BuildUri(request).ToString)
            rc = Nothing

            retList.Add("Request.Resource: " + request.Resource)

            retList.Add("Request.Paramaters: " + request.Parameters.Count.ToString)
            For Each p In request.Parameters
                retList.Add(vbTab + "Name: " + p.Name)
                retList.Add(vbTab + "Value: " + p.Value.ToString)
                retList.Add(vbTab + "Type: " + p.Type.ToString)
            Next

        Catch ex As Exception
            ret = ex.ToString
            Return ret
        End Try

        ret = String.Join(vbCrLf, retList)
        Return ret
    End Function

    Public Shared Function ResponseToString(response As IRestResponse(Of Dictionary(Of String, Object))) As String

        Dim ret As String
        Dim retList As New List(Of String)

        Try
            retList.Add("StatusCode: " + response.StatusCode.ToString)
            retList.Add("Content: " + response.Content)
            retList.Add("Response.Headers: " + response.Headers.Count.ToString)

            For Each p In response.Headers
                retList.Add(vbTab + "Name: " + p.Name)
                retList.Add(vbTab + "Value: " + p.Value.ToString)
                retList.Add(vbTab + "Type: " + p.Type.ToString)
            Next

            If response.ResponseUri Is Nothing Then
                retList.Add("Response.ResponseURI: nothing")
            Else
                retList.Add("Response.ResponseURI: " + response.ResponseUri.ToString)
            End If

            retList.Add("ErrorMessage: " + response.ErrorMessage)

        Catch ex As Exception
            ret = ex.ToString
            Return ret
        End Try

        ret = String.Join(vbCrLf, retList)
        Return ret
    End Function

End Class
