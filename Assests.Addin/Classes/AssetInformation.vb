Public Class AssetInformation

#Region " Properties "

    Public Property ID As String
    Public Property CategoryID As String
    Public Property Category As String
    Public Property StatusId As String
    Public Property Description As String
    Public Property uniqueID As String
    Public Property Manufacturer As String
    Public Property Model As String

    Public Property Status As String ''used in progress dialog

    Public Property Astep As StatusStep

    Public Property AssetStatus As String ''used in add and view this is the status step thingy
    Public Property AssetStatusID As String ''used in add and view this is the status step thingy
    'Public Property SyncStatus As String ''used in view and delete is just a string
    Public Property SyncStatus As SyncStatusEnum

    Public ReadOnly Property SyncStatusValue As String
        Get
            Return SyncStatus.GetDescription
            'Select Case SyncStatus
            '    Case SyncStatusEnum.Added
            '        Return "Asset added to BIM360"
            '    Case SyncStatusEnum.Error
            '        Return "Error adding asset to BIM360"
            '    Case SyncStatusEnum.Exists
            '        Return "Asset already exists in BIM360"
            '    Case SyncStatusEnum.HasMissingValues
            '        Return "Asset has missing values"
            '    Case SyncStatusEnum.InBIM360
            '        Return "Asset in BIM360"
            '    Case SyncStatusEnum.NotInBIM360
            '        Return "Asset Not in Bim360"
            '    Case SyncStatus.NotInModel
            '        Return "Asset Not In Model"
            '    Case SyncStatusEnum.UnLinked
            '        Return "Missing Model <=> BIM360 Link"
            '    Case SyncStatusEnum.None
            '        Return ""
            'End Select
        End Get
    End Property
    Public Property SyncAction As SyncActionEnum

    Public ReadOnly Property SyncActionValue As String
        Get
            Return SyncAction.GetDescription
            'Select Case SyncAction
            '    Case SyncActionEnum.Disabled
            '        Return "Disabled"
            '    Case SyncActionEnum.SyncFromBIM360
            '        Return "Sync From BIM360"
            '    Case SyncActionEnum.SyncToBIM360
            '        Return "Sync To BIM360"
            'End Select
        End Get
    End Property
    Public Property NotInModel As Boolean
    'Public Property Exists As Boolean
    'Public Property HasMissingValues As Boolean



    Public Property AssetParamaters As Dictionary(Of String, String)
#End Region

#Region " Constructor "
    Public Sub New()
        ID = ""
        CategoryID = ""
        StatusId = ""
        Description = ""
        uniqueID = ""
        Astep = New StatusStep
        SyncStatus = SyncStatusEnum.None
        'Exists = False
        'HasMissingValues = False

        AssetParamaters = New Dictionary(Of String, String)
    End Sub

#End Region

End Class
