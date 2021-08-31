Imports Autodesk.Revit.DB
Public Class frmSchedules

#Region " Private "
    Private vSch As ViewSchedule
    Private dt As DataTable
    Private listFields As List(Of ScheduleField)
#End Region

#Region " Properties "
    Public Property Schedules As List(Of ViewSchedule)
    Public Property document As Document
#End Region

    Private Sub frmSchedules_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cmbSchedules.Items.Clear()
        cmbSchedules.DisplayMember = "Name"
        cmbSchedules.ValueMember = "Name"
        cmbSchedules.DataSource = Schedules
        cmbSchedules.DisplayMember = "Name"
        cmbSchedules.ValueMember = "Name"

        'For Each vSch As ViewSchedule In Schedules
        '    cmbSchedules.Items.Add(vSch.Name)
        'Next
    End Sub

    Private Sub cmbSchedules_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSchedules.SelectedIndexChanged
        ''get the schedule
        vSch = cmbSchedules.SelectedItem

        dt = New DataTable
        dt.Columns.Add("ID")

        listFields = New List(Of ScheduleField)

        For i As Integer = 0 To vSch.Definition.GetFieldCount - 1
            Dim field As ScheduleField = vSch.Definition.GetField(i)
            listFields.Add(field)

            Dim fName As String = field.GetName
            Dim count As Integer = 1
            While dt.Columns.Contains(fName)
                fName = field.GetName + "(" + count.ToString + ")"
                count += 1
            End While
            dt.Columns.Add(fName)
        Next

        Dim fec = New FilteredElementCollector(document, vSch.Id).WhereElementIsNotElementType
        For Each element As Element In fec
            Dim row As DataRow = dt.NewRow

            row("ID") = element.Id.IntegerValue


            'Public Enum ScheduleFieldType
            '    Instance = 0
            '    ElementType = 1
            '    Count = 2
            '    ViewBased = 4
            '    Formula = 5
            '    Percentage = 6
            '    Room = 7
            '    FromRoom = 8
            '    ToRoom = 9
            '    ProjectInfo = 10
            '    Material = 11
            '    MaterialQuantity = 12
            '    RevitLinkInstance = 13
            '    RevitLinkType = 14
            '    StructuralMaterial = 15
            '    Space = 16
            '    Analytical = 17
            '    PhysicalType = 18
            '    PhysicalInstance = 19
            '    CombinedParameter = 20
            'End Enum

            For Each field As ScheduleField In listFields

                Try
                    Dim p = element.Parameter(field.ParameterId.IntegerValue)
                    If Not p.HasValue Then Continue For

                    Select Case p.StorageType
                        Case StorageType.Double
                            'row(field.GetName) = p.AsDouble.ToString
                            ''convert to project units

                            row(field.GetName) = FamilyHelper.ConvertToProjectUnits(p, field)

                        Case StorageType.ElementId
                            Dim val As String = ""
                            Dim elID As ElementId = p.AsElementId
                            Dim el As Element = document.GetElement(p.AsElementId)
                            val = el.Name

                            Select Case True
                                Case elID.IntegerValue < 0
                                    Dim cat As Category = document.Settings.Categories(elID.IntegerValue)
                                    If Not cat Is Nothing Then val = cat.Name
                                Case p.Id.IntegerValue = BuiltInParameter.ELEM_FAMILY_PARAM OrElse p.Id.IntegerValue = BuiltInParameter.ELEM_FAMILY_AND_TYPE_PARAM
                                    Dim elType As ElementType = document.GetElement(elID)
                                    If Not elType Is Nothing Then
                                        val = elType.FamilyName


                                    End If
                                Case p.Id.IntegerValue = BuiltInParameter.AREA_TYPE
                                    val = p.AsValueString
                            End Select


                            row(field.GetName) = val
                            ''if element id
                        Case StorageType.Integer
                            row(field.GetName) = p.AsInteger.ToString
                        Case StorageType.String
                            row(field.GetName) = p.AsString
                    End Select

                    Debug.WriteLine(p.ToString)
                Catch ex As Exception

                End Try

                Select Case field.FieldType
                End Select
            Next

            dt.Rows.Add(row)
        Next



        DataGridView1.DataSource = dt
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click




        Debugger.Launch()
        Debugger.Break()
    End Sub
End Class