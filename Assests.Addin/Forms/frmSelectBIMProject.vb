Imports System.Windows.Forms

Public Class frmSelectBIMProject

#Region " Properties "
    Public Property ForgeConnection As ForgeConnection
    Public Property SelectedProject As ForgeProject
#End Region

    Private Sub frmSelectBIMProject_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ForgeConnection.GetHubs()

        For Each hub As ForgeHub In ForgeConnection.Hubs
            Dim tNode As New TreeNode
            tNode.Text = hub.Name
            tNode.Tag = hub
            tNode.Nodes.Add("")
            tvBimProjects.Nodes.Add(tNode)
        Next


    End Sub


    Private Sub tvBimProjects_BeforeExpand(sender As Object, e As TreeViewCancelEventArgs) Handles tvBimProjects.BeforeExpand
        Dim tvHub As ForgeHub = e.Node.Tag
        e.Node.Nodes.Clear()
        ForgeConnection.GetProject(tvHub.ID)

        For Each proj As ForgeProject In ForgeConnection.Projects
            Dim tvProj As New TreeNode
            tvProj.Text = proj.Name
            tvProj.Tag = proj

            e.Node.Nodes.Add(tvProj)
        Next

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        SelectedProject = Nothing
        DialogResult = DialogResult.Cancel
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SelectedProject = tvBimProjects.SelectedNode.Tag
        DialogResult = DialogResult.OK
    End Sub
End Class