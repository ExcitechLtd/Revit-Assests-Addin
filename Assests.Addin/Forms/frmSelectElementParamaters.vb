Public Class frmSelectElementParamaters

    Public ParamaterNames As List(Of String)

    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub frmSelectElementParamaters_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ParamaterNames = New List(Of String)
    End Sub

    Private Sub frmSelectElementParamaters_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        ParamaterNames.Sort()

        For Each value As String In ParamaterNames
            chkListParamaters.Items.Add(value)
        Next
    End Sub
End Class