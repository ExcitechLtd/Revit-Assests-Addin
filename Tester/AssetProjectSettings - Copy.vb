Imports System.Web.Script.Serialization
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
        Dim jSonSerializer As New JavaScriptSerializer
        Dim jsonSettings = jSonSerializer.Serialize(settings)

        IO.File.WriteAllText(settingsPath, jsonSettings)
    End Sub

    Public Shared Function LoadSettings() As AssetProjectSettings
        Dim ret As New AssetProjectSettings

        If Not IO.File.Exists(settingsPath) Then Return ret

        'read JSON settings data
        Dim jsonSettings = IO.File.ReadAllText(settingsPath)

        'de-serialize settings and return object
        Dim jSonSerializer As New JavaScriptSerializer
        Dim tmpObj As Dictionary(Of String, Object)
        Dim tmpValue As String = ""
        tmpObj = jSonSerializer.Deserialize(Of Dictionary(Of String, Object))(jsonSettings)
        '
        For Each key As String In tmpObj.Keys
            Dim value As Dictionary(Of String, Object) = tmpObj(key)
            Dim api As New AssetProjectInformation

            For Each pKey As String In value.Keys

                Select Case pKey
                    Case "DocumentGUID"
                        If String.IsNullOrEmpty(value(pKey)) Then tmpValue = "" Else tmpValue = value(pKey).ToString
                        api.DocumentGUID = tmpValue
                    Case "DocumentName"
                        If String.IsNullOrEmpty(value(pKey)) Then tmpValue = "" Else tmpValue = value(pKey).ToString
                        api.DocumentName = tmpValue
                    Case "BIMProject"
                        If String.IsNullOrEmpty(value(pKey)) Then tmpValue = "" Else tmpValue = value(pKey).ToString
                        api.BIMProject = tmpValue
                    Case "BIMCategory"
                        If String.IsNullOrEmpty(value(pKey)) Then tmpValue = "" Else tmpValue = value(pKey).ToString
                        api.BIMCategory = tmpValue
                    Case "ElementCategory"
                        If String.IsNullOrEmpty(value(pKey)) Then tmpValue = "" Else tmpValue = value(pKey).ToString
                        api.ElementCategory = tmpValue
                    Case "ElementAssetList"
                        api.ElementAssetList = value(pKey)
                End Select
            Next

            ret.Add(key, api)
        Next

        Return jSonSerializer.Deserialize(Of AssetProjectSettings)(jsonSettings)


        Return ret
    End Function

End Class
