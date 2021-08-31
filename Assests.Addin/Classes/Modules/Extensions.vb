Imports System.Runtime.CompilerServices

Module Extensions

    <Extension()>
    Public Function GetDescription(value As SyncActionEnum) As String
        Select Case value
            Case SyncActionEnum.Disabled
                Return "Disabled"
            Case SyncActionEnum.SyncFromBIM360
                Return "Sync From BIM360"
            Case SyncActionEnum.SyncToBIM360
                Return "Sync To BIM360"
        End Select
        Return String.Empty
    End Function

    <Extension()>
    Public Function GetDescription(value As SyncStatusEnum) As String
        Select Case value
            Case SyncStatusEnum.Added
                Return "Asset added to BIM360"
            Case SyncStatusEnum.Error
                Return "Error adding asset to BIM360"
            Case SyncStatusEnum.Exists
                Return "Asset already exists in BIM360"
            Case SyncStatusEnum.HasMissingValues
                Return "Asset has missing values"
            Case SyncStatusEnum.InBIM360
                Return "Asset in BIM360"
            Case SyncStatusEnum.NotInBIM360
                Return "Asset Not in Bim360"
            Case SyncStatusEnum.NotInModel
                Return "Asset Not In Model"
            Case SyncStatusEnum.UnLinked
                Return "Missing Model <=> BIM360 Link"
            Case SyncStatusEnum.None
                Return ""
        End Select
        Return String.Empty
    End Function
End Module
