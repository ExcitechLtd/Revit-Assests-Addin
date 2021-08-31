Imports Newtonsoft.Json

Public Class AssetProjectSettings
    Inherits Dictionary(Of String, AssetProjectInformation)

    Private Shared settingsPath As String

    Public Sub New()
        MyBase.New

        settingsPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)
        settingsPath = IO.Path.Combine(settingsPath, "Revit Assets")

        If Not IO.Directory.Exists(settingsPath) Then IO.Directory.CreateDirectory(settingsPath)

        settingsPath = IO.Path.Combine(settingsPath, "AssetsProject.settings")
    End Sub

    Public Shared Sub Savesettings(settings As AssetProjectSettings)
        Dim jsonSettings As String = JsonConvert.SerializeObject(settings)

        IO.File.WriteAllText(settingsPath, jsonSettings)
    End Sub

    Public Shared Function LoadSettings() As AssetProjectSettings

        If Not IO.File.Exists(settingsPath) Then Return New AssetProjectSettings

        'read JSON settings data
        Dim jsonSettings = IO.File.ReadAllText(settingsPath)

        'de-serialize settings and return object
        Return JsonConvert.DeserializeObject(Of AssetProjectSettings)(jsonSettings)

    End Function

End Class
