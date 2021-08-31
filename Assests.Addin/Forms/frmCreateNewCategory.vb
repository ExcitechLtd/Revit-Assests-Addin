Imports System.Windows.Forms

Public Class frmCreateNewCategory


    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If String.IsNullOrWhiteSpace(txtCatName.Text) Then
            Exit Sub
        End If

        Dim newCat As New NewForgeCategory
        newCat.name = txtCatName.Text

        Dim tNode As TreeNode = tvBimCategories.SelectedNode
        Dim sfcCat As ForgeCategory = tNode.Tag

        newCat.parentId = sfcCat.ID

        SharedObjects.ForgeConnection.CreateCategory(newCat)
        SharedObjects.ForgeConnection.GetCategories()

        tvBimCategories.Nodes.Clear()
        populateTree(Nothing)
        ''find the new node

        tNode = tvBimCategories.Nodes.Find(txtBIM360Project.Text, True).FirstOrDefault
        tNode.EnsureVisible()

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub frmCreateNewCategory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ''perform auth just in case
        If Not SharedObjects.IsConnectionActive Then
            If Not SharedObjects.ForgeConnection.AccessToken = "" Then
                SharedObjects.ForgeConnection.GetNewRefreshToken()
                SharedObjects.UserSettings.UpdateTokensAndSave(SharedObjects.ForgeConnection)
            Else
                SharedObjects.DoForgeAuth()
                SharedObjects.UserSettings.UpdateTokensAndSave(SharedObjects.ForgeConnection)
            End If
        End If

    End Sub

    Private Sub populateTree(tNode As TreeNode)

        If tNode Is Nothing Then
            ''look for the root
            Dim rIndex As Integer = SharedObjects.ForgeConnection.Categories.FindIndex(Function(c) c.IsRoot)
            If rIndex <= -1 Then
                Exit Sub
            End If

            Dim fCat As ForgeCategory = SharedObjects.ForgeConnection.Categories(rIndex)
            tNode = New TreeNode
            tNode.Text = fCat.Name
            tNode.Tag = fCat

            populateTree(tNode)
            tvBimCategories.Nodes.Add(tNode)
        Else
            Dim fc As ForgeCategory = tNode.Tag
            If fc.SubCategorys.Count > 0 Then

                For Each sCat As String In fc.SubCategorys
                    Dim sIndex As Integer = SharedObjects.ForgeConnection.Categories.FindIndex(Function(c) c.ID = sCat)
                    If Not sIndex <= -1 Then
                        Dim sfcCat As ForgeCategory = SharedObjects.ForgeConnection.Categories(sIndex)
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

    Private Sub btnBim360Project_Click(sender As Object, e As EventArgs) Handles btnBim360Project.Click
        ''perform auth just in case
        If Not SharedObjects.IsConnectionActive Then
            If Not SharedObjects.ForgeConnection.AccessToken = "" Then
                SharedObjects.ForgeConnection.GetNewRefreshToken()
                SharedObjects.UserSettings.UpdateTokensAndSave(SharedObjects.ForgeConnection)
            Else
                SharedObjects.DoForgeAuth()
                SharedObjects.UserSettings.UpdateTokensAndSave(SharedObjects.ForgeConnection)
            End If
        End If

        ''show a list of BIM projects that we have access to
        Dim frmSelectProj As New frmSelectBIMProject
        frmSelectProj.ForgeConnection = SharedObjects.ForgeConnection
        frmSelectProj.StartPosition = Windows.Forms.FormStartPosition.CenterParent

        If frmSelectProj.ShowDialog = Windows.Forms.DialogResult.OK Then
            SharedObjects.ForgeConnection.SelectedProject = frmSelectProj.SelectedProject
            txtBIM360Project.Text = SharedObjects.ForgeConnection.SelectedProject.Name
            txtBIM360Project.BackColor = System.Drawing.Color.White

            ''show the categories
            SharedObjects.ForgeConnection.GetCategories()
            populateTree(Nothing)

        Else

        End If

    End Sub


End Class