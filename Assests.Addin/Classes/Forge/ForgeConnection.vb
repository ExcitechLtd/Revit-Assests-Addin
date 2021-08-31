Imports Newtonsoft.Json
Imports RestSharp

Public Class ForgeConnection

    Public Shared BaseUri As Uri
    Public Shared clientID As String
    Public Shared clientSecret As String
    Public Shared redirectURL As String

    Private _restClient As New RestClient
    Private hubID As String
    Private projID As String

    Public SelectedProject As ForgeProject
    Public SelectedCategory As ForgeCategory
    Public SelectedModelCategory As modelCategory

    Public ReadOnly Property AccessToken As String = ""
    Public ReadOnly Property RefreshToken As String = ""
    Public ReadOnly Property TokenType As String = ""
    Public ReadOnly Property TokenExpiry As Date

    Public ReadOnly Property Hubs As List(Of ForgeHub)
    Public ReadOnly Property Projects As List(Of ForgeProject)
    Public ReadOnly Property Categories As List(Of ForgeCategory)
    Public ReadOnly Property Statuses As List(Of ForgeStatus)
    Public ReadOnly Property StatusStep As List(Of ForgeStatusStep)
    Public ReadOnly Property AllStatusSteps As List(Of StatusStep)

    Public ReadOnly Property Assets As Dictionary(Of String, ForgeAsset)

    Public ReadOnly Property Attributes As List(Of ForgeAttribute)

    Public Event Status(Value As String)

    Private listCustomAttr As SortedList(Of String, Integer)

    Public Sub New()
        TokenExpiry = Date.MinValue
        AccessToken = ""
        RefreshToken = ""
        TokenType = ""

        Net.ServicePointManager.SecurityProtocol = Net.SecurityProtocolType.Tls12 Or Net.SecurityProtocolType.Tls11 Or Net.SecurityProtocolType.Tls
        _restClient = New RestClient

        If _restClient.BaseUrl Is Nothing Then
            BaseUri = New Uri(SharedObjects.BaseURI)
            _restClient.BaseUrl = BaseUri
        End If
    End Sub

    Public Shared Function WithCode(code As String) As ForgeConnection
        Logging.WriteToLog("Method: WithCode")

        Try
            Dim cn As New ForgeConnection
            cn.postGetToken(code)
            Return cn
        Catch ex As Exception
            Logging.WriteToLog("Code: " + code)
            Logging.WriteToLog("---- Error ----")
            Logging.WriteToLog(ex.ToString)
        End Try


    End Function

    Public Function HasTokenExpired() As Boolean
        ''offset the time now by 5 minutes to give us some head room with the token
        Return Not TokenExpiry > Now.AddMinutes(-1)
    End Function

    Public Sub LoadSettings(settings As settings)
        _AccessToken = settings.AccessToken
        _RefreshToken = settings.RefreshToken
        _TokenType = settings.TokenType
        _TokenExpiry = settings.TokenExpiry
    End Sub

    Public Sub ClearSettings()
        _AccessToken = ""
        _RefreshToken = ""
        _TokenType = ""
        _TokenExpiry = Nothing
    End Sub

    Private Sub postGetToken(code As String)
        Logging.WriteToLog("Method: postGetToken")

        Dim request As RestRequest
        Dim response As IRestResponse(Of Dictionary(Of String, Object))

        Try
            request = New RestRequest("authentication/v1/gettoken", Method.POST)
            request.AddParameter("client_id", clientID)
            request.AddParameter("client_secret", clientSecret)
            request.AddParameter("grant_type", "authorization_code")
            request.AddParameter("code", code)
            request.AddParameter("redirect_uri", redirectURL)

            response = _restClient.Execute(Of Dictionary(Of String, Object))(request)
            Dim authenticationData = response.Data

            'check data valid
            If authenticationData Is Nothing Then Throw New Exception("Error retrieving Forge authentication data")

            'check developerMessage
            If authenticationData.ContainsKey("developerMessage") Then Throw New Exception(authenticationData("developerMessage"))

            'store token data
            _AccessToken = authenticationData("access_token")
            _RefreshToken = authenticationData("refresh_token")
            _TokenType = authenticationData("token_type")
            _TokenExpiry = Now.AddSeconds(authenticationData("expires_in"))

            _restClient.AddDefaultHeader("authorization", "Bearer " + _AccessToken)
        Catch ex As Exception
            Logging.WriteToLog("Raw Request:")
            Logging.WriteToLog(Logging.RequestToString(request))
            Logging.WriteToLog("Raw Response:")
            Logging.WriteToLog(Logging.ResponseToString(response))
            Logging.WriteToLog("---- Error ----")
            Logging.WriteToLog(ex.ToString)

            Throw
        End Try

        _restClient.BaseUrl = BaseUri


    End Sub

    Public Sub GetNewRefreshToken(Optional initToken As Boolean = False)
        Logging.WriteToLog("Method: GetNewRefreshToken")

        Dim request As RestRequest
        Dim response As IRestResponse(Of Dictionary(Of String, Object))

        Try
            request = New RestRequest("authentication/v1/refreshtoken", Method.POST)
            request.AddParameter("client_id", clientID)
            request.AddParameter("client_secret", clientSecret)
            request.AddParameter("grant_type", "refresh_token")
            request.AddParameter("refresh_token", RefreshToken)

            response = _restClient.Execute(Of Dictionary(Of String, Object))(request)

            Dim authenticationData = response.Data

            'check data valid
            If authenticationData Is Nothing Then
                If Not initToken Then
                    Throw New Exception("Error retrieving Forge refresh data")
                Else
                    _AccessToken = ""
                    _RefreshToken = ""
                    _TokenType = ""

                    Exit Sub
                End If
            End If

            'check developerMessage
            If authenticationData.ContainsKey("developerMessage") Then
                If Not initToken Then
                    Throw New Exception(authenticationData("developerMessage"))
                Else
                    _AccessToken = ""
                    _RefreshToken = ""
                    _TokenType = ""

                    Exit Sub
                End If
            End If

            'store token data
            _AccessToken = authenticationData("access_token")
            _RefreshToken = authenticationData("refresh_token")
            _TokenType = authenticationData("token_type")
            _TokenExpiry = Now.AddSeconds(authenticationData("expires_in"))

            _restClient.RemoveDefaultParameter("authorization")
            _restClient.AddDefaultHeader("authorization", "Bearer " + _AccessToken)
        Catch ex As Exception
            Logging.WriteToLog("Raw Request:")
            Logging.WriteToLog(Logging.RequestToString(request))
            Logging.WriteToLog("Raw Response:")
            Logging.WriteToLog(Logging.ResponseToString(response))
            Logging.WriteToLog("---- Error ----")
            Logging.WriteToLog(ex.ToString)

            Throw
        End Try

        _restClient.BaseUrl = BaseUri


    End Sub

    Public Sub GetHubs()
        Logging.WriteToLog("Method: Gethubs")

        Dim request As RestRequest
        Dim response As IRestResponse(Of Dictionary(Of String, Object))

        Try
            _restClient.BaseUrl = BaseUri
            request = New RestRequest("project/v1/hubs", Method.GET)
            response = _restClient.Execute(Of Dictionary(Of String, Object))(request)
            Dim hubsData = response.Data
            _Hubs = New List(Of ForgeHub)

            'If hubsData.ContainsKey("errorCode") Then

            'End If

            ''project hubs
            For Each hub As Dictionary(Of String, Object) In hubsData("data")
                _Hubs.Add(New ForgeHub With {
                    .ID = hub("id"),
                    .Name = hub("attributes")("name")
                })

            Next

        Catch ex As Exception
            Logging.WriteToLog("Raw Request:")
            Logging.WriteToLog(Logging.RequestToString(request))
            Logging.WriteToLog("Raw Response:")
            Logging.WriteToLog(Logging.ResponseToString(response))
            Logging.WriteToLog("---- Error ----")
            Logging.WriteToLog(ex.ToString)

            Throw
        End Try


    End Sub

    Public Sub GetProject()
        Logging.WriteToLog("Method: GetProject")

        Dim request As RestRequest
        Dim response As IRestResponse(Of Dictionary(Of String, Object))

        Try
            _restClient.BaseUrl = BaseUri
            request = New RestRequest("project/v1/hubs/" + hubID + "/projects", Method.GET)
            response = _restClient.Execute(Of Dictionary(Of String, Object))(request)
            Dim projData = response.Data


            ''project hubs
            For Each project As Dictionary(Of String, Object) In projData("data")
                projID = project("id")

                Dim msg As String = "project ID: " + projID + vbCrLf
                msg += "name: " + project("attributes")("name") + vbCrLf

                RaiseEvent Status(msg)
            Next
        Catch ex As Exception
            Logging.WriteToLog("Raw Request:")
            Logging.WriteToLog(Logging.RequestToString(request))
            Logging.WriteToLog("Raw Response:")
            Logging.WriteToLog(Logging.ResponseToString(response))
            Logging.WriteToLog("---- Error ----")
            Logging.WriteToLog(ex.ToString)

            Throw
        End Try

    End Sub

    Public Sub GetProject(hubID As String)
        Logging.WriteToLog("Method: GetProject")

        Dim request As RestRequest
        Dim response As IRestResponse(Of Dictionary(Of String, Object))

        Try
            _Projects = New List(Of ForgeProject)

            _restClient.BaseUrl = BaseUri
            request = New RestRequest("project/v1/hubs/" + hubID + "/projects", Method.GET)
            response = _restClient.Execute(Of Dictionary(Of String, Object))(request)
            Dim projData = response.Data


            ''project hubs
            For Each project As Dictionary(Of String, Object) In projData("data")
                projID = project("id")

                _Projects.Add(New ForgeProject With {
                    .ID = projID,
                    .Name = project("attributes")("name")
                })

                'Dim msg As String = "project ID: " + projID + vbCrLf
                'msg += "name: " + project("attributes")("name") + vbCrLf

                'RaiseEvent Status(msg)
            Next

        Catch ex As Exception
            Logging.WriteToLog("Raw Request:")
            Logging.WriteToLog(Logging.RequestToString(request))
            Logging.WriteToLog("Raw Response:")
            Logging.WriteToLog(Logging.ResponseToString(response))
            Logging.WriteToLog("---- Error ----")
            Logging.WriteToLog(ex.ToString)

            Throw
        End Try

    End Sub

    Public Sub GetCategories()
        Logging.WriteToLog("Method: GetCategories")

        Dim request As RestRequest
        Dim response As IRestResponse(Of Dictionary(Of String, Object))

        Try
            _Categories = New List(Of ForgeCategory)

            _restClient.BaseUrl = BaseUri
            request = New RestRequest("bim360/assets/v1/projects/" + SelectedProject.ID + "/categories", Method.GET)
            response = _restClient.Execute(Of Dictionary(Of String, Object))(request)
            Dim projData = response.Data


            ''project hubs
            For Each project As Dictionary(Of String, Object) In projData("results")

                Dim _cat As New ForgeCategory With {
                    .ID = project("id"),
                    .Name = project("name"),
                    .IsRoot = CBool(project("isRoot")),
                    .IsLeaf = CBool(project("isLeaf"))
                }

                For Each sCat As String In project("subcategoryIds")
                    _cat.SubCategorys.Add(sCat)
                Next

                _Categories.Add(_cat)
            Next
        Catch ex As Exception
            Logging.WriteToLog("Raw Request:")
            Logging.WriteToLog(Logging.RequestToString(request))
            Logging.WriteToLog("Raw Response:")
            Logging.WriteToLog(Logging.ResponseToString(response))
            Logging.WriteToLog("---- Error ----")
            Logging.WriteToLog(ex.ToString)

            Throw
        End Try


    End Sub

    Public Sub GetCategories(projectID As String)
        Logging.WriteToLog("Method: GetCategories")

        Dim request As RestRequest
        Dim response As IRestResponse(Of Dictionary(Of String, Object))

        Try
            _Categories = New List(Of ForgeCategory)

            _restClient.BaseUrl = BaseUri
            request = New RestRequest("bim360/assets/v1/projects/" + projectID + "/categories", Method.GET)
            response = _restClient.Execute(Of Dictionary(Of String, Object))(request)
            Dim projData = response.Data


            ''project hubs
            For Each project As Dictionary(Of String, Object) In projData("results")

                Dim _cat As New ForgeCategory With {
                    .ID = project("id"),
                    .Name = project("name"),
                    .IsRoot = CBool(project("isRoot")),
                    .IsLeaf = CBool(project("isLeaf"))
                }

                For Each sCat As String In project("subcategoryIds")
                    _cat.SubCategorys.Add(sCat)
                Next

                _Categories.Add(_cat)

                'Dim msg As String = "project ID: " + projID + vbCrLf
                'msg += "name: " + project("attributes")("name") + vbCrLf

                'RaiseEvent Status(msg)
            Next
        Catch ex As Exception
            Logging.WriteToLog("Raw Request:")
            Logging.WriteToLog(Logging.RequestToString(request))
            Logging.WriteToLog("Raw Response:")
            Logging.WriteToLog(Logging.ResponseToString(response))
            Logging.WriteToLog("---- Error ----")
            Logging.WriteToLog(ex.ToString)

            Throw
        End Try


    End Sub

    Public Sub CreateCategory(newCategory As NewForgeCategory)
        Logging.WriteToLog("Method: CreateCategory")

        Dim request As RestRequest
        Dim response As IRestResponse(Of Dictionary(Of String, Object))

        Try
            Dim jsSer As New System.Web.Script.Serialization.JavaScriptSerializer
            Dim json As String = jsSer.Serialize(newCategory)

            _restClient.BaseUrl = BaseUri
            request = New RestRequest("bim360/assets/v1/projects/" + SelectedProject.ID + "/categories", Method.POST)
            request.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody)

            response = _restClient.Execute(Of Dictionary(Of String, Object))(request)
            Dim projData = response.Data

        Catch ex As Exception
            Logging.WriteToLog("Raw Request:")
            Logging.WriteToLog(Logging.RequestToString(request))
            Logging.WriteToLog("Raw Response:")
            Logging.WriteToLog(Logging.ResponseToString(response))
            Logging.WriteToLog("---- Error ----")
            Logging.WriteToLog(ex.ToString)

            Throw
        End Try



    End Sub

    Public Sub CreateCategory(projectID As String, newCategory As NewForgeCategory)
        Logging.WriteToLog("Method: CreateCategory")

        Dim request As RestRequest
        Dim response As IRestResponse(Of Dictionary(Of String, Object))

        Try
            Dim jsSer As New System.Web.Script.Serialization.JavaScriptSerializer
            Dim json As String = jsSer.Serialize(newCategory)

            _restClient.BaseUrl = BaseUri
            request = New RestRequest("bim360/assets/v1/projects/" + projectID + "/categories", Method.POST)
            request.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody)

            response = _restClient.Execute(Of Dictionary(Of String, Object))(request)
            Dim projData = response.Data
        Catch ex As Exception
            Logging.WriteToLog("Raw Request:")
            Logging.WriteToLog(Logging.RequestToString(request))
            Logging.WriteToLog("Raw Response:")
            Logging.WriteToLog(Logging.ResponseToString(response))
            Logging.WriteToLog("---- Error ----")
            Logging.WriteToLog(ex.ToString)

            Throw
        End Try


    End Sub

    Public Function CreateAssets(assetList As List(Of NewForgeAssets)) As List(Of ForgeAsset)
        Logging.WriteToLog("Method: CreateAssets")
        Dim response As IRestResponse(Of Dictionary(Of String, Object))
        Dim request As RestRequest

        Try
            'https://developer.api.autodesk.com/bim360/assets/v2/projects/:projectId/assets:batch-create
            'content application/json

            'Dim jsSer As New System.Web.Script.Serialization.JavaScriptSerializer
            'Dim json As String = jsSer.Serialize(assetList)

            Dim json = JsonConvert.SerializeObject(assetList, New JsonSerializerSettings With {.NullValueHandling = NullValueHandling.Ignore})

            _restClient.BaseUrl = BaseUri
            request = New RestRequest("bim360/assets/v2/projects/" + Me.SelectedProject.ID + "/assets:batch-create", Method.POST)
            request.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody)

            response = _restClient.Execute(Of Dictionary(Of String, Object))(request)

            Dim results As JsonArray = response.Data("results")

            Dim ret As New List(Of ForgeAsset)
            For Each nAsset As Dictionary(Of String, Object) In results
                ret.Add(New ForgeAsset With {
                    .Id = nAsset("id"),
                    .ClientAssetId = nAsset("clientAssetId"),
                    .Description = nAsset("description")
                })

            Next

            Return ret
        Catch ex As Exception
            Logging.WriteToLog("Raw Request:")
            Logging.WriteToLog(Logging.RequestToString(request))
            Logging.WriteToLog("Raw Response:")
            Logging.WriteToLog(Logging.ResponseToString(response))
            Logging.WriteToLog(ex.ToString)

            Throw New Exception("Error in 'Create Assets' please check the log file for more information")

        End Try

    End Function

    Public Sub CreateAssets(projectId As String, assetList As List(Of NewForgeAssets))
        Logging.WriteToLog("Method: CreateAssets")

        Dim request As RestRequest
        Dim response As IRestResponse(Of Dictionary(Of String, Object))

        Try
            'https://developer.api.autodesk.com/bim360/assets/v2/projects/:projectId/assets:batch-create
            'content application/json

            Dim jsSer As New System.Web.Script.Serialization.JavaScriptSerializer
            Dim json As String = jsSer.Serialize(assetList)

            _restClient.BaseUrl = BaseUri
            request = New RestRequest("bim360/assets/v2/projects/" + projectId + "/assets:batch-create", Method.POST)
            request.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody)

            response = _restClient.Execute(Of Dictionary(Of String, Object))(request)
            Dim projData = response.Data

        Catch ex As Exception
            Logging.WriteToLog("Raw Request:")
            Logging.WriteToLog(Logging.RequestToString(request))
            Logging.WriteToLog("Raw Response:")
            Logging.WriteToLog(Logging.ResponseToString(response))
            Logging.WriteToLog("---- Error ----")
            Logging.WriteToLog(ex.ToString)

            Throw
        End Try


    End Sub

    Public Sub AssignStatusStep(projectID As String, categoryID As String, statusStepID As String)
        Logging.WriteToLog("Method: AssignStatusStep")

        Dim request As RestRequest
        Dim response As IRestResponse(Of Dictionary(Of String, Object))

        Try
            '	https://developer.api.autodesk.com/bim360/assets/v1/projects/:projectId/categories/:categoryId/status-step-set/:statusStepSetId

            _restClient.BaseUrl = BaseUri
            request = New RestRequest("bim360/assets/v1/projects/" + projectID + "/categories/" + categoryID + "/status-step-set/" + statusStepID, Method.PUT)
            response = _restClient.Execute(Of Dictionary(Of String, Object))(request)

        Catch ex As Exception
            Logging.WriteToLog("Raw Request:")
            Logging.WriteToLog(Logging.RequestToString(request))
            Logging.WriteToLog("Raw Response:")
            Logging.WriteToLog(Logging.ResponseToString(response))
            Logging.WriteToLog("---- Error ----")
            Logging.WriteToLog(ex.ToString)

            Throw
        End Try



    End Sub

    Public Sub GetStatusSteps(projectId As String)
        Logging.WriteToLog("Method: GetStatusSteps")

        Dim request As RestRequest
        Dim response As IRestResponse(Of Dictionary(Of String, Object))

        Try
            _restClient.BaseUrl = BaseUri
            request = New RestRequest("bim360/assets/v1/projects/" + projID + "/status-step-sets", Method.GET)

            response = _restClient.Execute(Of Dictionary(Of String, Object))(request)
            Dim projData = response.Data

            _AllStatusSteps = New List(Of StatusStep)

            ''project hubs
            For Each project As Dictionary(Of String, Object) In projData("results")
                _AllStatusSteps.Add(New StatusStep With {
                            .ID = project("id"),
                            .Name = project("name")
                            })
            Next
        Catch ex As Exception
            Logging.WriteToLog("Raw Request:")
            Logging.WriteToLog(Logging.RequestToString(request))
            Logging.WriteToLog("Raw Response:")
            Logging.WriteToLog(Logging.ResponseToString(response))
            Logging.WriteToLog("---- Error ----")
            Logging.WriteToLog(ex.ToString)

            Throw
        End Try


    End Sub

    'Public Sub GetCategoryStatusStepID(projectID As String, categoryID As String, Optional includeInherited As Boolean = True)
    Public Sub GetCategoryStatusStepID(Optional includeInherited As Boolean = True)
        Logging.WriteToLog("Method: GetCategoryStatusStepID")

        Dim request As RestRequest
        Dim response As IRestResponse(Of Dictionary(Of String, Object))

        Try
            ''https://developer.api.autodesk.com/bim360/assets/v1/projects/:projectId/category-status-step-sets/status-step-sets:batch-get
            _StatusStep = New List(Of ForgeStatusStep)

            Dim catList As New StatusSetCategoryList
            catList.ids.Add(Me.SelectedCategory.ID)

            Dim jsSer As New System.Web.Script.Serialization.JavaScriptSerializer
            Dim json As String = jsSer.Serialize(catList)


            _restClient.BaseUrl = BaseUri
            request = New RestRequest("bim360/assets/v1/projects/" + Me.SelectedProject.ID + "/category-status-step-sets/status-step-sets:batch-get?includeInherited=true", Method.POST)
            'If includeInherited Then request.AddParameter("includeInherited", True)
            request.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody)

            response = _restClient.Execute(Of Dictionary(Of String, Object))(request)
            Dim projData = response.Data

            ''project hubs
            For Each project As Dictionary(Of String, Object) In projData("results")

                _StatusStep.Add(New ForgeStatusStep With {
                    .CategoryID = project("categoryId"),
                    .StatusStepsetId = project("statusStepSetId")
                })

            Next

            Dim ssList As New StatusSetList
            For Each _sTep As ForgeStatusStep In _StatusStep
                ssList.ids.Add(_sTep.StatusStepsetId)
            Next

            Me.GetStatusSetStatuses(ssList)
        Catch ex As Exception
            Logging.WriteToLog("Raw Request:")
            Logging.WriteToLog(Logging.RequestToString(request))
            Logging.WriteToLog("Raw Response:")
            Logging.WriteToLog(Logging.ResponseToString(response))
            Logging.WriteToLog("---- Error ----")
            Logging.WriteToLog(ex.ToString)

            Throw
        End Try

    End Sub

    Public Sub GetStatusSetStatuses(statusSetIDs As StatusSetList)
        Logging.WriteToLog("Method: GetStatusSetStatuses")

        Dim request As RestRequest
        Dim response As IRestResponse(Of Dictionary(Of String, Object))

        Try
            ''	https://developer.api.autodesk.com/bim360/assets/v1/projects/:projectId/status-step-sets:batch-get

            _Statuses = New List(Of ForgeStatus)
            _restClient.BaseUrl = BaseUri

            Dim jsSer As New System.Web.Script.Serialization.JavaScriptSerializer
            Dim json As String = jsSer.Serialize(statusSetIDs)

            request = New RestRequest("bim360/assets/v1/projects/" + Me.SelectedProject.ID + "/status-step-sets:batch-get", Method.POST)
            'If includeInherited Then request.AddParameter("includeInherited", True)
            request.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody)

            response = _restClient.Execute(Of Dictionary(Of String, Object))(request)
            Dim projData = CType(response.Data, Dictionary(Of String, Object))
            Dim results As JsonArray = projData("results")

            For Each sSet As Dictionary(Of String, Object) In results
                If Not sSet.ContainsKey("values") Then Continue For

                Dim values As JsonArray = sSet("values")

                For Each item As Dictionary(Of String, Object) In values
                    _Statuses.Add(New ForgeStatus With {
                            .ID = item("id"),
                            .Label = item("label")
                        })

                Next
            Next
        Catch ex As Exception
            Logging.WriteToLog("Raw Request:")
            Logging.WriteToLog(Logging.RequestToString(request))
            Logging.WriteToLog("Raw Response:")
            Logging.WriteToLog(Logging.ResponseToString(response))
            Logging.WriteToLog("---- Error ----")
            Logging.WriteToLog(ex.ToString)

            Throw
        End Try

    End Sub

    Public Function GetStatusStepNameFromID(statusID As String) As String
        Logging.WriteToLog("Method: GetStatusStepNameFromID")

        Try
            If String.IsNullOrWhiteSpace(statusID) Then Return ""

            Dim sIndex As Integer = -1
            sIndex = Statuses.FindIndex(Function(s) s.ID = statusID)

            If sIndex = -1 Then Return ""

            Return Statuses(sIndex).Label
        Catch ex As Exception
            Logging.WriteToLog("---- Error ----")
            Logging.WriteToLog(ex.ToString)

            Throw
        End Try

    End Function

    Public Sub GetStatuses()
        Logging.WriteToLog("Method: GetStatuses")

        Dim request As RestRequest
        Dim response As IRestResponse(Of Dictionary(Of String, Object))

        Try
            'https://developer.api.autodesk.com/bim360/assets/v1/projects/:projectId/asset-statuses

            _Statuses = New List(Of ForgeStatus)
            _restClient.BaseUrl = BaseUri
            request = New RestRequest("bim360/assets/v1/projects/" + Me.SelectedProject.ID + "/asset-statuses", Method.GET)
            response = _restClient.Execute(Of Dictionary(Of String, Object))(request)
            Dim projData = response.Data


            ''project hubs
            For Each project As Dictionary(Of String, Object) In projData("results")

                _Statuses.Add(New ForgeStatus With {
                    .ID = project("id"),
                    .Label = project("label")
                })

            Next
        Catch ex As Exception
            Logging.WriteToLog(Logging.RequestToString(request))
            Logging.WriteToLog("Raw Response:")
            Logging.WriteToLog(Logging.ResponseToString(response))
            Logging.WriteToLog("---- Error ----")
            Logging.WriteToLog(ex.ToString)

            Throw
        End Try


    End Sub

    Public Sub GetStatuses(projectId As String)
        Logging.WriteToLog("Method: GetStatuses")

        Dim request As RestRequest
        Dim response As IRestResponse(Of Dictionary(Of String, Object))

        Try
            'https://developer.api.autodesk.com/bim360/assets/v1/projects/:projectId/asset-statuses

            _Statuses = New List(Of ForgeStatus)
            _restClient.BaseUrl = BaseUri
            request = New RestRequest("bim360/assets/v1/projects/" + projectId + "/asset-statuses", Method.GET)
            response = _restClient.Execute(Of Dictionary(Of String, Object))(request)
            Dim projData = response.Data


            ''project hubs
            For Each project As Dictionary(Of String, Object) In projData("results")

                _Statuses.Add(New ForgeStatus With {
                    .ID = project("id"),
                    .Label = project("label")
                })

            Next
        Catch ex As Exception
            Logging.WriteToLog("Raw Request:")
            Logging.WriteToLog(Logging.RequestToString(request))
            Logging.WriteToLog("Raw Response:")
            Logging.WriteToLog(Logging.ResponseToString(response))
            Logging.WriteToLog("---- Error ----")
            Logging.WriteToLog(ex.ToString)

            Throw
        End Try


    End Sub

    Public Sub GetCustomAttributes()
        Logging.WriteToLog("Method: GetCustomAttributes")

        Dim request As RestRequest
        Dim response As IRestResponse(Of Dictionary(Of String, Object))

        Try
            _restClient.BaseUrl = BaseUri
            request = New RestRequest("bim360/assets/v1/projects/" + projID + "/custom-attributes", Method.GET)
            response = _restClient.Execute(Of Dictionary(Of String, Object))(request)
            Dim customAttrData = response.Data
            Dim value As String
            _Attributes = New List(Of ForgeAttribute)


            For Each att As Dictionary(Of String, Object) In customAttrData("results")
                Dim forgeAT As New ForgeAttribute
                forgeAT.ID = att("id")

                If att.TryGetValue("description", value) Then forgeAT.Description = value Else : value = ""
                If att.TryGetValue("name", value) Then forgeAT.Name = value Else : value = ""
                If att.TryGetValue("displayName", value) Then forgeAT.Displayname = value Else : value = ""
                If att.TryGetValue("requiredOnIngress", value) Then
                    forgeAT.RequiredOnIngress = CBool(value)
                Else
                    forgeAT.RequiredOnIngress = False
                End If

                _Attributes.Add(forgeAT)
            Next
        Catch ex As Exception
            Logging.WriteToLog("Raw Request:")
            Logging.WriteToLog(Logging.RequestToString(request))
            Logging.WriteToLog("Raw Response:")
            Logging.WriteToLog(Logging.ResponseToString(response))
            Logging.WriteToLog("---- Error ----")
            Logging.WriteToLog(ex.ToString)

            Throw
        End Try


    End Sub

    Public Sub GetCustomAttributesForCategory()
        Logging.WriteToLog("Method: GetCustomAttributesForCategory")

        Dim request As RestRequest
        Dim response As IRestResponse(Of Dictionary(Of String, Object))

        Try
            'https://forge.autodesk.com/en/docs/bim360/v1/reference/http/assets-categories-category-id-custom-attributes-GET/
            '	https://developer.api.autodesk.com/bim360/assets/v1/projects/:projectId/categories/:categoryId/custom-attributes

            _restClient.BaseUrl = BaseUri
            request = New RestRequest("bim360/assets/v1/projects/" + Me.SelectedProject.ID + "/categories/" + Me.SelectedCategory.ID + "/custom-attributes?includeInherited=true", Method.GET)
            response = _restClient.Execute(Of Dictionary(Of String, Object))(request)
            Dim customAttrData = CType(response.Data, Dictionary(Of String, Object))
            Dim results As JsonArray = customAttrData("results")

            _Attributes = New List(Of ForgeAttribute)

            For Each cAtt As Dictionary(Of String, Object) In results
                'If Not cAtt.ContainsKey("values") Then Continue For

                'Dim values As JsonArray = cAtt("values")

                Dim forgeAT As New ForgeAttribute
                forgeAT.ID = cAtt("id")
                forgeAT.Name = cAtt("name")
                forgeAT.Displayname = cAtt("displayName")

                _Attributes.Add(forgeAT)
            Next
        Catch ex As Exception
            Logging.WriteToLog("Raw Request:")
            Logging.WriteToLog(Logging.RequestToString(request))
            Logging.WriteToLog("Raw Response:")
            Logging.WriteToLog(Logging.ResponseToString(response))
            Logging.WriteToLog("---- Error ----")
            Logging.WriteToLog(ex.ToString)

            Throw
        End Try


    End Sub

    Public Function GetAssetNames() As List(Of String)
        Logging.WriteToLog("Method: GetAssetNames")

        Dim request As RestRequest
        Dim response As IRestResponse(Of Dictionary(Of String, Object))

        Try
            'https://forge.autodesk.com/en/docs/bim360/v1/reference/http/assets-assets-v2-GET/
            'https://developer.api.autodesk.com/bim360/assets/v2/projects/:projectId/assets

            _restClient.BaseUrl = BaseUri
            request = New RestRequest("bim360/assets/v2/projects/" + SelectedProject.ID + "/assets", Method.GET)
            request.AddParameter("filter[categoryId]", SelectedCategory.ID)

            response = _restClient.Execute(Of Dictionary(Of String, Object))(request)
            Dim assestData = response.Data

            Dim _ret As New List(Of String)

            For Each assetDct As Dictionary(Of String, Object) In assestData("results")
                _ret.Add(assetDct("clientAssetId"))
                'Dim forgeAtt As New ForgeAsset With {
                '    .Id = assetDct("id"),
                '    .ClientAssetId = assetDct("clientAssetId"),
                '    .Description = assetDct("description"),
                '    .StatusID = assetDct("statusId")
                '}


                '_Assets.Add(assetDct("clientAssetId"), forgeAtt)
            Next

            Return _ret
        Catch ex As Exception
            Logging.WriteToLog("Raw Request:")
            Logging.WriteToLog(Logging.RequestToString(request))
            Logging.WriteToLog("Raw Response:")
            Logging.WriteToLog(Logging.ResponseToString(response))
            Logging.WriteToLog("---- Error ----")
            Logging.WriteToLog(ex.ToString)

            Throw
        End Try

    End Function

    Public Function GetAssets(categoryId As String, includeCustomAttributes As Boolean) As RestSharp.JsonArray
        Logging.WriteToLog("Method: GetAssets")

        Dim request As RestRequest
        Dim response As IRestResponse(Of Dictionary(Of String, Object))

        Try
            'https://forge.autodesk.com/en/docs/bim360/v1/reference/http/assets-assets-v2-GET/
            'https://developer.api.autodesk.com/bim360/assets/v2/projects/:projectId/assets

            _restClient.BaseUrl = BaseUri
            request = New RestRequest("bim360/assets/v2/projects/" + projID + "/assets", Method.GET)
            request.AddParameter("filter[categoryId]", categoryId)
            If includeCustomAttributes Then request.AddParameter("includeCustomAttributes", True)

            response = _restClient.Execute(Of Dictionary(Of String, Object))(request)
            Dim assestData = response.Data

            Return assestData("results")
        Catch ex As Exception
            Logging.WriteToLog("Raw Request:")
            Logging.WriteToLog(Logging.RequestToString(request))
            Logging.WriteToLog("Raw Response:")
            Logging.WriteToLog(Logging.ResponseToString(response))
            Logging.WriteToLog("---- Error ----")
            Logging.WriteToLog(ex.ToString)

            Throw
        End Try

    End Function

    Public Sub GetAssets()
        Logging.WriteToLog("Method: GetAssets")

        Dim request As RestRequest
        Dim response As IRestResponse(Of Dictionary(Of String, Object))


        Try
            'https://forge.autodesk.com/en/docs/bim360/v1/reference/http/assets-assets-v2-GET/
            'https://developer.api.autodesk.com/bim360/assets/v2/projects/:projectId/assets


            _restClient.BaseUrl = BaseUri
            request = New RestRequest("bim360/assets/v2/projects/" + SelectedProject.ID + "/assets", Method.GET)
            request.AddParameter("filter[categoryId]", SelectedCategory.ID)
            request.AddParameter("includeCustomAttributes", True)

            response = _restClient.Execute(Of Dictionary(Of String, Object))(request)
            Dim assestData = response.Data

            _Assets = New Dictionary(Of String, ForgeAsset)

            For Each assetDct As Dictionary(Of String, Object) In assestData("results")
                Dim forgeAtt As New ForgeAsset With {
                    .Id = assetDct("id"),
                    .ClientAssetId = assetDct("clientAssetId"),
                    .Description = assetDct("description"),
                    .StatusID = assetDct("statusId")
                }

                Dim bCode As String = ""
                assetDct.TryGetValue("barcode", bCode)
                forgeAtt.Barcode = bCode

                ''havet we got any attributes?
                Dim obj As Dictionary(Of String, Object)
                If assetDct.TryGetValue("customAttributes", obj) Then
                    For Each key As String In obj.Keys
                        forgeAtt.Attributes.Add(key, obj(key).ToString)
                    Next
                End If

                _Assets.Add(assetDct("clientAssetId"), forgeAtt)
            Next
        Catch ex As Exception
            Logging.WriteToLog("Raw Request:")
            Logging.WriteToLog(Logging.RequestToString(request))
            Logging.WriteToLog("Raw Response:")
            Logging.WriteToLog(Logging.ResponseToString(response))
            Logging.WriteToLog("---- Error ----")
            Logging.WriteToLog(ex.ToString)

            Throw
        End Try


    End Sub

    Public Sub DeleteAssets(deleteList As List(Of String))
        Logging.WriteToLog("Method: DeleteAssets")

        Dim request As RestRequest
        Dim response As IRestResponse(Of Dictionary(Of String, Object))

        Try
            ''https://developer.api.autodesk.com/bim360/assets/v2/projects/:projectId/assets:batch-delete
            ''id's array of string

            If deleteList Is Nothing Then Exit Sub
            If deleteList.Count = 0 Then Exit Sub

            Dim jsonList As New AssetDeleteList
            jsonList.ids = deleteList

            Dim jsSer As New System.Web.Script.Serialization.JavaScriptSerializer
            Dim json As String = jsSer.Serialize(jsonList)

            _restClient.BaseUrl = BaseUri
            request = New RestRequest("bim360/assets/v2/projects/" + SelectedProject.ID + "/assets:batch-delete", Method.POST)
            request.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody)

            response = _restClient.Execute(Of Dictionary(Of String, Object))(request)
            Dim projData = response.Data
        Catch ex As Exception
            Logging.WriteToLog("Raw Request:")
            Logging.WriteToLog(Logging.RequestToString(request))
            Logging.WriteToLog("Raw Response:")
            Logging.WriteToLog(Logging.ResponseToString(response))
            Logging.WriteToLog("---- Error ----")
            Logging.WriteToLog(ex.ToString)

            Throw
        End Try

    End Sub

    Public Sub UpdateAsset(UpdateAssets As List(Of UpdateAsset))
        Logging.WriteToLog("Method: UpdateAsset")

        Dim request As RestRequest
        Dim response As IRestResponse(Of Dictionary(Of String, Object))

        Try
            'https://developer.api.autodesk.com/bim360/assets/v2/projects/:projectId/assets:batch-patch

            ''transform the list in to a nested Key value pair 
            Dim updateDCT As New Dictionary(Of String, Object)

            For Each item As UpdateAsset In UpdateAssets
                'Dim attrDct As New Dictionary(Of String, Dictionary(Of String, String))
                'attrDct.Add("customAttributes", item.CustomAttributes)
                updateDCT.Add(item.bimAssetID, item)
            Next

            'Dim jsSer As New System.Web.Script.Serialization.JavaScriptSerializer
            'Dim json As String = jsSer.Serialize(updateDCT)
            Dim json = JsonConvert.SerializeObject(updateDCT, New JsonSerializerSettings With {.NullValueHandling = NullValueHandling.Ignore})

            _restClient.BaseUrl = BaseUri
            request = New RestRequest("bim360/assets/v2/projects/" + Me.SelectedProject.ID + "/assets:batch-patch", Method.PATCH)
            request.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody)

            response = _restClient.Execute(Of Dictionary(Of String, Object))(request)
            Dim projData = response.Data
        Catch ex As Exception
            Logging.WriteToLog("Raw Request:")
            Logging.WriteToLog(Logging.RequestToString(request))
            Logging.WriteToLog("Raw Response:")
            Logging.WriteToLog(Logging.ResponseToString(response))
            Logging.WriteToLog("---- Error ----")
            Logging.WriteToLog(ex.ToString)

            Throw
        End Try



    End Sub

End Class
