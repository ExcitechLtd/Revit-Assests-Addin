Imports System.ComponentModel
Imports Autodesk.Revit.DB
Public Class FamilyHelper

    Public Shared Function GetSchedules(Document As Document) As List(Of ViewSchedule)
        Dim _ret As New List(Of ViewSchedule)

        ''find view schedules
        For Each element As Element In New FilteredElementCollector(Document).OfClass(GetType(ViewSchedule))
            Dim vSchedule As ViewSchedule = CType(element, ViewSchedule)
            If Not vSchedule.IsTitleblockRevisionSchedule Then
                _ret.Add(vSchedule)
            End If
        Next

        Return _ret
    End Function

    Public Shared Function ConvertToProjectUnits(param As Parameter, field As ScheduleField) As Double
        Dim val As Double = param.AsDouble
        Dim format As FormatOptions = param.Element.Document.GetUnits.GetFormatOptions(param.Definition.UnitType)

        If Not field.GetFormatOptions.UseDefault Then
            format = field.GetFormatOptions
        End If

        Return UnitUtils.ConvertFromInternalUnits(val, format.DisplayUnits)
    End Function


    Public Shared Function GetModelCategoryAssets(RevitDocument As Document, category As modelCategory) As BindingList(Of AssetInformation)
        Dim _ret As New BindingList(Of AssetInformation)
        If category Is Nothing Then Return _ret

        Dim collector As New FilteredElementCollector(RevitDocument)
        Dim collection As List(Of Element) = collector.OfClass(GetType(FamilyInstance)).OfCategory(category.ID).ToElements

        Dim count As Integer = 1
        For Each el As Element In collection
            Dim aInfo As New AssetInformation
            aInfo.ID = category.Name.ToUpperInvariant + "-" + count.ToString
            aInfo.CategoryID = el.Category.Name
            aInfo.StatusId = ""
            aInfo.Description = el.Name
            aInfo.uniqueID = el.UniqueId


            _ret.Add(aInfo)
            count += 1
        Next

        Return _ret
    End Function

    Public Shared Function GetAssetParamterNames(RevitDocument As Document) As List(Of String)
        Dim _ret As New List(Of String)

        ''get the shared parameters
        Dim paramFile As DefinitionFile = RevitDocument.Application.OpenSharedParameterFile
        Dim paramGrp As DefinitionGroup = Nothing

        For Each paramGrp In paramFile.Groups
            If paramGrp.Name.ToUpperInvariant = "EXC_ASSETS" Then Exit For Else paramGrp = Nothing
        Next

        For Each def In paramGrp.Definitions
            _ret.Add(def.Name)
        Next

        Return _ret
    End Function
End Class
