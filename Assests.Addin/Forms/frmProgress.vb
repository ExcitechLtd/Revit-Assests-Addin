Imports System.ComponentModel

Public Class frmProgress

#Region " Properties "
    Public Property ForgeCN As ForgeConnection
    Public Property AssetList As BindingList(Of AssetInformation)
    Public Property assetCategory As ForgeCategory
#End Region

    Public Event UpdateGrid()


    Private Sub frmProgress_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.StartPosition = Windows.Forms.FormStartPosition.CenterParent
        Me.TopMost = True

    End Sub

    Public Sub UpdateProgress(value As Integer)
        If ProgressBar1.InvokeRequired Then
            ProgressBar1.Invoke(Sub()
                                    ProgressBar1.Value = value
                                End Sub)
        Else
            ProgressBar1.Value = value
        End If
    End Sub

    Public Sub UpdateLabel(value As String)
        If lblLog.InvokeRequired Then
            lblLog.Invoke(Sub()
                              lblLog.Text = value
                          End Sub)
        Else
            lblLog.Text = value
        End If
    End Sub

    Private Sub frmProgress_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        'Task.Run(Sub()

        UpdateLabel("Getting assets for category: " + assetCategory.Name)
        'Dim response As Dictionary(Of String, Object) = _ForgeCN.GetAssets(assetCategory.ID)
        Dim results As RestSharp.JsonArray = _ForgeCN.GetAssets(assetCategory.ID, True)
        Dim max = results.Count

        UpdateLabel("Total number of assets found: " + results.Count.ToString)

        For aCount As Integer = 0 To max - 1
            Dim aInfo As AssetInformation = AssetList(aCount)

            UpdateLabel("Checking for asset: " + aInfo.ID)
            UpdateProgress((aCount / max) * 100)

            aInfo.Status = "Checking BIM360..."
            Dim aIndex As Integer = results.FindIndex(Function(a)
                                                          Dim name As String = a("clientAssetId")

                                                          Return name.ToUpperInvariant = aInfo.ID.ToUpperInvariant
                                                      End Function)

            If aIndex = -1 Then
                aInfo.Status = "Not in BIM360"
                AssetList(aCount) = aInfo
                Continue For
            End If

            aInfo.Status = "Found"

            AssetList(aCount) = aInfo
        Next

        Debugger.Launch()
        Debugger.Break()

        Me.Close()
        'End Sub)
    End Sub
End Class