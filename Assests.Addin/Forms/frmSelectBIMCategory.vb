Imports System.Windows.Forms

Public Class frmSelectBIMCategory
#Region " Properties "
    Public Property ForgeConnection As ForgeConnection
    Public Property SelectedCategory As ForgeCategory
#End Region

    Private Sub frmSelectBIMProject_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ForgeConnection.GetCategories()


        ''find the root category
        populateTree(Nothing)

        'For Each hub As ForgeCategory In ForgeConnection.Categories
        '    If Not hub.IsRoot Then Continue For

        '    Dim tNode As New TreeNode
        '    tNode.Text = hub.Name
        '    tNode.Tag = hub

        '    For Each subCat As String In hub.SubCategorys
        '        Dim ind As Integer = ForgeConnection.Categories.FindIndex(Function(c) c.ID = subCat)
        '        If ind = -1 Then Continue For

        '        Dim sCat As ForgeCategory = ForgeConnection.Categories(ind)
        '        Dim sNode As New TreeNode
        '        sNode.Text = sCat.Name
        '        sNode.Tag = sCat


        '        tNode.Nodes.Add(sNode)
        '    Next

        '    tvBimCategories.Nodes.Add(tNode)
        'Next

    End Sub

    Private Sub populateTree(tNode As TreeNode)

        If tNode Is Nothing Then
            ''look for the root
            Dim rIndex As Integer = ForgeConnection.Categories.FindIndex(Function(c) c.IsRoot)
            If rIndex <= -1 Then
                Exit Sub
            End If

            Dim fCat As ForgeCategory = ForgeConnection.Categories(rIndex)
            tNode = New TreeNode
            tNode.Text = fCat.Name
            tNode.Tag = fCat

            populateTree(tNode)
            tvBimCategories.Nodes.Add(tNode)
        Else
            Dim fc As ForgeCategory = tNode.Tag
            If fc.SubCategorys.Count > 0 Then

                For Each sCat As String In fc.SubCategorys
                    Dim sIndex As Integer = ForgeConnection.Categories.FindIndex(Function(c) c.ID = sCat)
                    If Not sIndex <= -1 Then
                        Dim sfcCat As ForgeCategory = ForgeConnection.Categories(sIndex)
                        Dim sNode As New TreeNode
                        sNode.Text = sfcCat.Name
                        sNode.Tag = sfcCat
                        populateTree(sNode)
                        tNode.Nodes.Add(sNode)
                    End If
                Next

            End If

        End If

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        DialogResult = DialogResult.Cancel
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SelectedCategory = tvBimCategories.SelectedNode.Tag
        DialogResult = DialogResult.OK
    End Sub
End Class