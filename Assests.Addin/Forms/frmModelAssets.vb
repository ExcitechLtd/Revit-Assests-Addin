Imports System.ComponentModel
Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports System.Threading
Imports Autodesk.Revit.DB
Public Class frmModelAssets

#Region " Private "
    Private _modelCatList As List(Of modelCategory)
    Private _usersettings As settings

    Private _baseUrl As String = "https://developer.api.autodesk.com"
    Private _redirectUrl As String = "http://localhost:3000"
    Private _clientId As String = "dGQVFmXIk1ky45dp6PhUrANGoGLLtQJr"
    Private _secret As String = "gzLN2wZABI7CJg1c"

    Private _forgeCN As New ForgeConnection

    Private _waithandle As New Threading.ManualResetEvent(False)
    Private assetList As New BindingList(Of AssetInformation)

#End Region

#Region " Properties "
    Public Property RevitDocument As Document
#End Region

#Region " Form Methods "
    Private Sub frmModelAssets_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtDocumentProject.Text = RevitDocument.ProjectInformation.Name

        _modelCatList = New List(Of modelCategory)

        For Each c As Category In RevitDocument.Settings.Categories
            If Not c.CategoryType = CategoryType.Model Then Continue For

            _modelCatList.Add(New modelCategory With {
                .ID = c.Id.IntegerValue,
                .Name = c.Name
            })

        Next

        _modelCatList.Sort(Function(x, y) x.Name.CompareTo(y.Name))

        cmbCategories.DataSource = _modelCatList
        cmbCategories.ValueMember = "ID"
        cmbCategories.DisplayMember = "NAME"

        '''read any settings we have
        '_usersettings = New settings
        '_usersettings = settings.LoadSettings
        '_forgeCN.LoadSettings(_usersettings)

        '''are the settings valid?
        ''If Not _usersettings.AreSettingsValid Then


        '''update settings
        'DoForgeAuth()
        '    _usersettings.AccessToken = _forgeCN.AccessToken
        '    _usersettings.RefreshToken = _forgeCN.RefreshToken
        '    _usersettings.TokenExpiry = _forgeCN.TokenExpiry
        '    _usersettings.TokenType = _forgeCN.TokenType
        '    _usersettings.SaveSettings()
        ''End If
    End Sub

    Private Sub cmbCategories_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCategories.SelectedIndexChanged

        If cmbCategories.SelectedItem Is Nothing Then Exit Sub

        Dim selectedCat As modelCategory = cmbCategories.SelectedItem
        assetList = New BindingList(Of AssetInformation)
        Dim collector As New FilteredElementCollector(RevitDocument)
        Dim collection = collector.OfClass(GetType(FamilyInstance)).OfCategory(selectedCat.ID).ToElements

        Dim count As Integer = 1
        For Each el As Element In collection

            Dim aInfo As New AssetInformation
            aInfo.ID = selectedCat.Name.ToUpperInvariant + "-" + count.ToString
            aInfo.CategoryID = el.Category.Name
            aInfo.StatusId = ""
            aInfo.Description = el.Name
            aInfo.uniqueID = el.UniqueId

            ''
            Dim instance As FamilyInstance = CType(el, FamilyInstance)
            Dim symbol As FamilySymbol = instance.Symbol

            If Not symbol Is Nothing Then
                Dim param As Parameter = symbol.Parameter(BuiltInParameter.ALL_MODEL_MANUFACTURER)
                If Not param Is Nothing Then aInfo.Manufacturer = param.AsString

                param = symbol.Parameter(BuiltInParameter.ALL_MODEL_MODEL)
                If Not param Is Nothing Then aInfo.Model = param.AsString
            End If

            assetList.Add(aInfo)
            count += 1
        Next

        GridControl1.DataSource = assetList
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnBimCheck.Click

        ForgeConnection.BaseUri = New Uri(_baseUrl)
        ForgeConnection.clientID = _clientId
        ForgeConnection.clientSecret = _secret
        ForgeConnection.redirectURL = _redirectUrl

        ''do we already have a forge token?
        If Not _forgeCN.AccessToken = "" Then
            ''we have a token saved has it expired?
            'If _forgeCN.HasTokenExpired Then
            '    ''get a new token using the refresh token
            _forgeCN.GetNewRefreshToken()

            'Else


            'End If
        Else
            ''we have no auth token so go get one
            DoForgeAuth()

        End If

        '_waithandle.WaitOne()

        _usersettings.AccessToken = _forgeCN.AccessToken
        _usersettings.RefreshToken = _forgeCN.RefreshToken
        _usersettings.TokenExpiry = _forgeCN.TokenExpiry
        _usersettings.TokenType = _forgeCN.TokenType
        _usersettings.SaveSettings()

        ''get hubs
        _forgeCN.GetHubs()
        cmbHubs.DisplayMember = "Name"
        cmbHubs.ValueMember = "ID"
        cmbHubs.DataSource = _forgeCN.Hubs

        GroupBox1.Enabled = True

    End Sub

    Private Sub UpdateConnectionStatus()
        If _usersettings.AreSettingsValid Then
            lblCnStatus.Text = "Connection Status: Connected"
        Else
            lblCnStatus.Text = "Connection Status: Disconnected"
        End If
    End Sub
#End Region

#Region " Forge Helper "


    Private Sub DoForgeAuth()
        ForgeConnection.BaseUri = New Uri(_baseUrl)
        ForgeConnection.clientID = _clientId
        ForgeConnection.clientSecret = _secret
        ForgeConnection.redirectURL = _redirectUrl

        'Dim _tcpListener As New TcpListener(IPAddress.Loopback, 3000)
        'Dim _tcpCancelToken As New CancellationTokenSource
        'Dim _timeoutToken As New CancellationTokenSource(TimeSpan.FromSeconds(180))

        '_tcpCancelToken.Token.Register(Sub()
        '                                   MsgBox("Auth cancelled")
        '                               End Sub)

        '_timeoutToken.Token.Register(Sub()
        '                                 MsgBox("Auth timeout")
        '                             End Sub)

        'Dim _linkTokens As CancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(_tcpCancelToken.Token, _timeoutToken.Token)
        '_linkTokens.Token.Register(Sub()
        '                               _tcpListener.Stop()
        '                           End Sub)

        '_tcpListener.Start()

        '''setup the browser
        ''CefSharp.Cef.EnableHighDPISupport()
        Dim oauthUrl As String = GetAuthURL()

        '_tcpListener.Start()

        'Dim frmBrowser As New frmBrowser
        'frmBrowser.URL = oauthUrl
        'frmBrowser.TopMost = True
        'frmBrowser.StartPosition = Windows.Forms.FormStartPosition.CenterParent
        'frmBrowser.Show(Me)

        'Dim tcpClient = Await Task.Run(Function()
        '                                   Return _tcpListener.AcceptTcpClientAsync
        '                               End Function, _linkTokens.Token)

        'Dim response = readString(tcpClient)
        'tcpClient.Dispose()
        '_tcpListener.Stop()

        '_tcpCancelToken.Dispose()
        '_timeoutToken.Dispose()
        '_linkTokens.Dispose()

        'frmBrowser.Close()

        'Dim code = getAuthenicationCode(response)

        'ForgeConnection.BaseUri = New Uri(_baseUrl)
        'ForgeConnection.clientID = _clientId
        'ForgeConnection.clientSecret = _secret
        'ForgeConnection.redirectURL = _redirectUrl

        '_forgeCN = ForgeConnection.WithCode(code)

        'Debugger.Break()

        '_waithandle.Set()


        Dim frmBrowser As New frmBrowser
        frmBrowser.URL = oauthUrl
        frmBrowser.TopMost = True
        frmBrowser.StartPosition = Windows.Forms.FormStartPosition.CenterParent
        frmBrowser.ShowDialog()

        Dim code As String = frmBrowser.AuthCode

        If String.IsNullOrWhiteSpace(code) Then
            MsgBox("Error authentication code is empty")
            Exit Sub
        End If

        _forgeCN = ForgeConnection.WithCode(code)
    End Sub

    Private Sub cmbHubs_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbHubs.SelectedValueChanged
        ''get the projects from the selected hub

        If cmbHubs.SelectedItem Is Nothing Then
            cmbProjects.Items.Clear()
            Exit Sub
        End If

        Dim hub As ForgeHub = cmbHubs.SelectedItem

        _forgeCN.GetProject(hub.ID)

        cmbProjects.DisplayMember = "Name"
        cmbProjects.ValueMember = "ID"
        cmbProjects.DataSource = _forgeCN.Projects

    End Sub

    Private Sub cmbProjects_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbProjects.SelectedValueChanged
        If cmbProjects.SelectedItem Is Nothing Then
            bimCategorys.Items.Clear()
            Exit Sub
        End If

        Dim proj As ForgeProject = cmbProjects.SelectedItem

        _forgeCN.GetStatuses(proj.ID)

        cmbStatuses.DisplayMember = "Label"
        cmbStatuses.ValueMember = "ID"
        cmbStatuses.DataSource = _forgeCN.Statuses


        _forgeCN.GetCategories(proj.ID)

        bimCategorys.DisplayMember = "Name"
        bimCategorys.ValueMember = "ID"
        bimCategorys.DataSource = _forgeCN.Categories
    End Sub

    Private Function GetAuthURL() As String

        Dim url As String = "https://developer.api.autodesk.com/authentication/v1/authorize?"
        url += "response_type=code"
        url += "&client_id=" + Web.HttpUtility.UrlEncode(_clientId)
        url += "&redirect_uri=" + Web.HttpUtility.UrlEncode(_redirectUrl)
        url += "&scope=data:read%20data:write%20data:create"

        Return url
    End Function

    Private Function getAuthenicationCode(response As String) As String

        'split response by space  ' '
        Dim responseSegments = response.Split(" ")

        'search segments for code
        Dim results = From segment As String In responseSegments Where segment.Contains("/?code=") Select segment
        If results.Count Then Return results.First.Replace("/?code=", "")

        'search segements for error
        results = From segment As String In responseSegments Where segment.Contains("/?error=") Select segment
        If results.Count Then Throw New Exception(results.First.Replace("/?error=", ""))

        'catch all error
        Throw New Exception("Unable to retrieve authentication code")
    End Function

    Private Function readString(client As TcpClient) As String

        Dim readBuffer(client.ReceiveBufferSize) As Byte
        Dim fullserverReply As String

        Using inStream = New IO.MemoryStream

            Dim stream As NetworkStream
            Do
                stream = client.GetStream()
            Loop Until stream.DataAvailable

            While stream.DataAvailable
                Dim numberOfBytesRead = stream.Read(readBuffer, 0, readBuffer.Length)
                If numberOfBytesRead <= 0 Then Exit While

                inStream.Write(readBuffer, 0, numberOfBytesRead)
            End While

            fullserverReply = System.Text.Encoding.UTF8.GetString(inStream.ToArray())
        End Using

        Return fullserverReply
    End Function

    Private Sub btnAddCategory_Click(sender As Object, e As EventArgs) Handles btnAddCategory.Click
        ''	https://developer.api.autodesk.com/bim360/assets/v1/projects/:projectId/categories
        ''data:write data:create

        Dim newCatName As String = InputBox("New Category Name")
        Dim pCat As ForgeCategory = bimCategorys.SelectedItem
        Dim proj As ForgeProject = cmbProjects.SelectedItem

        Dim newCat As New NewForgeCategory
        newCat.name = newCatName
        newCat.parentId = pCat.ID
        'newCat.description = ""

        _forgeCN.CreateCategory(proj.ID, newCat)

        _forgeCN.GetCategories(proj.ID)

        bimCategorys.DisplayMember = "Name"
        bimCategorys.ValueMember = "ID"
        bimCategorys.DataSource = Nothing
        bimCategorys.DisplayMember = "Name"
        bimCategorys.ValueMember = "ID"
        bimCategorys.DataSource = _forgeCN.Categories
    End Sub

    Private Sub btnPushToBim_Click(sender As Object, e As EventArgs) Handles btnPushToBim.Click
        Dim aList As New List(Of NewForgeAssets)

        Dim proj As ForgeProject = cmbProjects.SelectedItem
        Dim cat As ForgeCategory = bimCategorys.SelectedItem
        Dim sta As ForgeStatus = cmbStatuses.SelectedItem

        '_forgeCN.GetStatusStep(proj.ID, cat.ID)
        _forgeCN.GetCategoryStatusStepID(True)

        For Each ainfo As AssetInformation In GridControl1.DataSource
            Dim nFAsset As New NewForgeAssets
            'nFAsset.barcode = ainfo.Barcode
            nFAsset.description = ainfo.Description
            nFAsset.clientAssetId = ainfo.ID
            nFAsset.statusId = sta.ID
            nFAsset.categoryId = cat.ID

            aList.Add(nFAsset)
        Next

        _forgeCN.CreateAssets(proj.ID, aList)

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Dim proj As ForgeProject = cmbProjects.SelectedItem
        Dim cat As ForgeCategory = bimCategorys.SelectedItem


        _forgeCN.GetStatusSteps(proj.ID)
        Dim dflt = _forgeCN.AllStatusSteps(0)

        _forgeCN.AssignStatusStep(proj.ID, cat.ID, dflt.ID)

    End Sub

    Private Sub btnCheck_Click(sender As Object, e As EventArgs) Handles btnCheck.Click
        Dim proj As ForgeProject = cmbProjects.SelectedItem
        Dim cat As ForgeCategory = bimCategorys.SelectedItem
        Dim sta As ForgeStatus = cmbStatuses.SelectedItem

        Dim chkProgress As New frmProgress
        chkProgress.StartPosition = Windows.Forms.FormStartPosition.CenterParent
        chkProgress.ForgeCN = _forgeCN
        chkProgress.AssetList = assetList
        chkProgress.assetCategory = cat
        chkProgress.ShowDialog()


    End Sub

    Private Sub btnClearBIMSettings_Click(sender As Object, e As EventArgs) Handles btnClearBIMSettings.Click
        _usersettings.ClearSettings()
        _forgeCN.ClearSettings()
    End Sub

    Private Sub btnBimProject_Click(sender As Object, e As EventArgs) Handles btnBimProject.Click
        Dim frmSelectProj As New frmSelectBIMProject
        frmSelectProj.ForgeConnection = _forgeCN
        frmSelectProj.StartPosition = Windows.Forms.FormStartPosition.CenterParent

        If frmSelectProj.ShowDialog = Windows.Forms.DialogResult.OK Then
            _forgeCN.SelectedProject = frmSelectProj.SelectedProject

            ''load the asset categories we have    
            _forgeCN.GetCategories()

            bimCategorys.DisplayMember = "Name"
            bimCategorys.ValueMember = "ID"
            bimCategorys.DataSource = _forgeCN.Categories
        End If

        lblBimProject.Text = "BIM Project: " + _forgeCN.SelectedProject.Name

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim proj As ForgeProject = cmbProjects.SelectedItem
        Dim cat As ForgeCategory = bimCategorys.SelectedItem
        Dim sta As ForgeStatus = cmbStatuses.SelectedItem

        _forgeCN.GetCustomAttributes()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        TextBox1.Text += ExportUtils.GetGBXMLDocumentId(RevitDocument).ToString + vbCrLf
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim paramFile As DefinitionFile = RevitDocument.Application.OpenSharedParameterFile
        Dim paramGrp As DefinitionGroup = Nothing

        For Each paramGrp In paramFile.Groups
            If paramGrp.Name.ToUpperInvariant = "EXC_ASSETS" Then Exit For Else paramGrp = Nothing
        Next

        TextBox1.Text = ""

        For Each def In paramGrp.Definitions
            TextBox1.Text += def.Name + vbCrLf
        Next

        'Using grpEnumerator = paramFile.Groups.GetEnumerator
        '    While grpEnumerator.MoveNext

        '    End While
        'End Using

        'DefinitionFile FParamFile = doc.Application.OpenSharedParameterFile();
        '        DefinitionGroup FParamGroup = null;
        '        String SearchName = "Wall Parameters";
        '        Using (IEnumerator < DefinitionGroup > enumerator = FParamFile.Groups.GetEnumerator())
        '        {
        '            While (enumerator.MoveNext())
        '            {
        '                DefinitionGroup current = enumerator.Current;
        '                If (current.Name == SearchName) Then FParamGroup = current;
        '            }
        '        }
        '        Definitions paramDefs = FParamGroup.Definitions;
    End Sub


#End Region

End Class