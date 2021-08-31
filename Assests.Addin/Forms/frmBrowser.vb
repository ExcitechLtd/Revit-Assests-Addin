Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports System.Threading
Imports CefSharp
Imports CefSharp.WinForms
Public Class frmBrowser

#Region " Properties "
    Public Property URL As String
    Public Property AuthCode As String
#End Region

#Region " Private "
    Private cefBrowser As ChromiumWebBrowser
#End Region


    Private Async Sub frmBrowser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.CenterToParent()
        cefBrowser = New ChromiumWebBrowser(URL)
        Me.Controls.Add(cefBrowser)
        cefBrowser.Dock = Windows.Forms.DockStyle.Fill


        Dim _tcpListener As New TcpListener(IPAddress.Loopback, 3000)
        Dim _tcpCancelToken As New CancellationTokenSource

        _tcpCancelToken.Token.Register(Sub()
                                           MsgBox("Auth cancelled")
                                       End Sub)

        _tcpListener.Start()

        Dim tcpClient = Await Task.Run(Function()
                                           Return _tcpListener.AcceptTcpClientAsync
                                       End Function, _tcpCancelToken.Token)

        Dim response = readString(tcpClient)
        tcpClient.Dispose()
        _tcpListener.Stop()

        _tcpCancelToken.Dispose()

        AuthCode = getAuthenicationCode(response)

        DialogResult = Windows.Forms.DialogResult.OK

    End Sub

    Private Function readString(client As TcpClient) As String

        Dim readBuffer(client.ReceiveBufferSize) As Byte
        Dim fullserverReply As String

        Using inStream = New IO.MemoryStream

            Dim stream As NetworkStream
            Do
                stream = client.GetStream()
            Loop Until stream.DataAvailable

            While stream.DataAvailable
                Dim numberOfBytesRead = stream.Read(readBuffer, 0, readBuffer.Length)
                If numberOfBytesRead <= 0 Then Exit While

                inStream.Write(readBuffer, 0, numberOfBytesRead)
            End While

            fullserverReply = System.Text.Encoding.UTF8.GetString(inStream.ToArray())
        End Using

        Return fullserverReply
    End Function

    Private Function getAuthenicationCode(response As String) As String

        'split response by space  ' '
        Dim responseSegments = response.Split(" ")

        'search segments for code
        Dim results = From segment As String In responseSegments Where segment.Contains("/?code=") Select segment
        If results.Count Then Return results.First.Replace("/?code=", "")

        'search segements for error
        results = From segment As String In responseSegments Where segment.Contains("/?error=") Select segment
        If results.Count Then Throw New Exception(results.First.Replace("/?error=", ""))

        'catch all error
        Throw New Exception("Unable to retrieve authentication code")
    End Function
End Class