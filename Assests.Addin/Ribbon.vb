Imports Autodesk.Revit.DB
Imports Autodesk.Revit.DB.Events
Imports Autodesk.Revit.UI
Imports Autodesk.Revit.UI.Events
Imports System.Drawing
Imports System.Reflection
Imports System.Windows.Media
Imports System.Windows.Media.Imaging

Public Class Ribbon
    Implements IExternalApplication

#Region " Private "
    Private Shared uiapp As UIApplication
    Private Shared uictrlapp As UIControlledApplication
#End Region

#Region " Startup / ShutDown "
    Public Function OnStartup(application As UIControlledApplication) As Result Implements IExternalApplication.OnStartup
        uictrlapp = application

        ''add ribbon
        AddRibbon(application)

        initObjects()

        Return Result.Succeeded
    End Function

    Public Function OnShutdown(application As UIControlledApplication) As Result Implements IExternalApplication.OnShutdown

    End Function
#End Region

#Region " Init "
    Private Sub initObjects()
        ForgeConnection.BaseUri = New Uri(BaseURI)
        ForgeConnection.clientID = ClientID
        ForgeConnection.clientSecret = Secret
        ForgeConnection.redirectURL = RedirectURL
        SharedObjects.ForgeConnection = New ForgeConnection
        UserSettings = New settings
        UserSettings = settings.LoadSettings

        SharedObjects.UserAssetProjectSettings = New AssetProjectSettings

        If UserSettings.AreSettingsValid Then
            SharedObjects.ForgeConnection = New ForgeConnection
            SharedObjects.ForgeConnection.LoadSettings(UserSettings)
            SharedObjects.ForgeConnection.GetNewRefreshToken(True)

            SharedObjects.UserSettings.AccessToken = SharedObjects.ForgeConnection.AccessToken
            SharedObjects.UserSettings.RefreshToken = SharedObjects.ForgeConnection.RefreshToken
            SharedObjects.UserSettings.TokenExpiry = SharedObjects.ForgeConnection.TokenExpiry
            SharedObjects.UserSettings.TokenType = SharedObjects.ForgeConnection.TokenType
            SharedObjects.UserSettings.SaveSettings()
        End If

    End Sub
#End Region

#Region " Ribbon "
    Private Sub AddRibbon(application As UIControlledApplication)

        ''create ribbonm tab
        application.CreateRibbonTab("Assets")

        ''add panel
        Dim panel As RibbonPanel = application.CreateRibbonPanel("Assets", "Assets")
        Dim pnlButton As PushButtonData
        ''new read button
        ''Dim pnlButton As New PushButtonData("btReadAssets", "Read", Assembly.GetExecutingAssembly.Location, "Assets.Addin.Read")
        ''pnlButton.LongDescription = "Read Schedule"
        ''pnlButton.Image = GetImageSource(My.Resources.R2D2_24)
        ''pnlButton.LargeImage = pnlButton.Image
        ''panel.AddItem(pnlButton)        ''add to panel

        pnlButton = New PushButtonData("btAddAssets", "Add model Assets" + vbCrLf + "to BIM360", Assembly.GetExecutingAssembly.Location, "Assets.Addin.AddAssets")
        pnlButton.LongDescription = "Add Assets to BIM360"
        pnlButton.Image = GetImageSource(My.Resources.add_assets_24)
        pnlButton.LargeImage = pnlButton.Image
        panel.AddItem(pnlButton)

        pnlButton = New PushButtonData("btSyncAssets", "Sync Assets", Assembly.GetExecutingAssembly.Location, "Assets.Addin.SyncBimAssets")
        pnlButton.LongDescription = "Sync Assets to/from BIM360"
        pnlButton.Image = GetImageSource(My.Resources.sync_from_24)
        pnlButton.LargeImage = pnlButton.Image
        panel.AddItem(pnlButton)

        pnlButton = New PushButtonData("btDeleteAssets", "Delete Assets", Assembly.GetExecutingAssembly.Location, "Assets.Addin.DeleteAssets")
        pnlButton.LongDescription = "Delete Bim Assets"
        pnlButton.Image = GetImageSource(My.Resources.delete_assets_24)
        pnlButton.LargeImage = pnlButton.Image
        panel.AddItem(pnlButton)

        panel = application.CreateRibbonPanel("Assets", "Categories")

        pnlButton = New PushButtonData("btAddCategory", "Add Category", Assembly.GetExecutingAssembly.Location, "Assets.Addin.AddCategory")
        pnlButton.LongDescription = "Add Bim Category"
        pnlButton.Image = GetImageSource(My.Resources.add_category_24)
        pnlButton.LargeImage = pnlButton.Image
        panel.AddItem(pnlButton)        ''add to panel

        panel = application.CreateRibbonPanel("Assets", "Settings")

        pnlButton = New PushButtonData("btConnectBIM", "Connect To" + vbCrLf + "BIM360", Assembly.GetExecutingAssembly.Location, "Assets.Addin.ConnectToBIM360")
        pnlButton.LongDescription = "Connect to BIM360"
        pnlButton.Image = GetImageSource(My.Resources.login_bim360_24)
        pnlButton.LargeImage = pnlButton.Image
        panel.AddItem(pnlButton)
        ''create a new panel
        'panel = application.CreateRibbonPanel("Assets", "BIM Testing")

        'pnlButton = New PushButtonData("btViewBimAssets", "View Assets", Assembly.GetExecutingAssembly.Location, "Assets.Addin.ViewDoorAssets") 'ViewDoorAssets
        'pnlButton.LongDescription = "View Bim Assets"
        'pnlButton.Image = GetImageSource(My.Resources.atat_24)
        'pnlButton.LargeImage = pnlButton.Image
        'panel.AddItem(pnlButton)        ''add to panel

        'pnlButton = New PushButtonData("btCreateBimAssets", "Create Asset", Assembly.GetExecutingAssembly.Location, "Assets.Addin.CreateBimAssets")
        'pnlButton.LongDescription = "Create an asset in BIM"
        'pnlButton.Image = GetImageSource(My.Resources.dstar_24)
        'pnlButton.LargeImage = pnlButton.Image
        'panel.AddItem(pnlButton)        ''add to panel

        'pnlButton = New PushButtonData("btViewBimCategorys", "View Categorys", Assembly.GetExecutingAssembly.Location, "Assets.Addin.ViewCategorys")
        'pnlButton.LongDescription = "View asset categorys in BIM"
        'pnlButton.Image = GetImageSource(My.Resources.mFalcon_24)
        'pnlButton.LargeImage = pnlButton.Image
        'panel.AddItem(pnlButton)        ''add to panel

    End Sub

    Public Function GetImageSource(Image As Bitmap) As ImageSource
        Dim hbitmap As IntPtr = Image.GetHbitmap()
        Return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hbitmap, IntPtr.Zero, Windows.Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions())
    End Function
#End Region


End Class
