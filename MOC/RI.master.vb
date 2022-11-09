Option Explicit On
Option Strict On

Imports Devart.Data.Oracle
Imports System.IO
Imports System.Runtime.Serialization
Imports System.Runtime.Serialization.Formatters.Binary
Imports RI
Imports System.Exception
Imports System.Globalization

Imports Telerik.Web.UI
'Namespace RI
Partial Class RIMaster
    Inherits System.Web.UI.MasterPage


    Public RIRESOURCES As New IP.Bids.Localization.WebLocalization
    'Public DATEXREF As New IP.MTIS.Localization.DateTime
    'Public NUMBERXREF As New IP.MTIS.Localization.Numbers
    'Public CURRENCYXREF As New IP.MTIS.Localization.Currency

    Public _CopyRightDate As String = ""
    Public MyGlobalUsername As String


    Public Sub PageRefresh(ByVal minutes As Integer)
        Dim meta As New HtmlMeta
        meta.Attributes.Add("http-equiv", "refresh")
        meta.Attributes.Add("content", minutes.ToString)
        Head.Controls.AddAt(0, meta)

    End Sub
    Dim mUnloadEvents As Boolean = False
    Public Property UnloadEvents() As Boolean
        Get
            Return mUnloadEvents
        End Get
        Set(ByVal value As Boolean)
            mUnloadEvents = value
        End Set
    End Property

    Public Property ContentHeight() As String
        Get
            Return Me._Content.Style.Item("Height")
        End Get
        Set(ByVal value As String)
            Me._Content.Style.Item("Height") = value
        End Set
    End Property
    Public Property MasterPageWidth() As String
        Get
            Return Me._tblMaster.Style.Item("Width")
        End Get
        Set(ByVal value As String)
            'style="width: 99%"
            Me._tblMaster.Style.Item("Width") = value
        End Set
    End Property
    Public Sub SetBanner(ByVal textMessage As String, Optional ByVal displayPopupBanner As Boolean = False)

        LabelBanner.Text = textMessage

    End Sub
    Public Sub DisplayExcel(ByRef ds As Data.DataSet)
        Me._displayExcel.DisplayExcel(ds)
    End Sub
    Public Sub DisplayExcel(ByRef dr As OracleDataReader)
        Me._displayExcel.DisplayExcel(dr)
    End Sub
    Public Sub DisplayExcel(ByRef dr As Data.DataTableReader)
        Me._displayExcel.DisplayExcel(dr)
    End Sub
    Public Sub DisplayExcel(ByVal excelXML As String)
        Me._displayExcel.DisplayExcel(excelXML)
    End Sub
    Public Function GetPopupWindowJS(ByVal url As String, Optional ByVal pageName As String = "newWindow", Optional ByVal windowWidth As Integer = 300, Optional ByVal windowHeight As Integer = 300, Optional ByVal showScrollBars As Boolean = False, Optional ByVal showMenu As Boolean = False, Optional ByVal openFullScreen As Boolean = False) As String
        Dim sb As New StringBuilder
        sb.Append("javascript:PopupWindow('")
        sb.Append(Page.ResolveClientUrl(url))
        sb.Append("','")
        sb.Append(pageName)
        sb.Append("','")
        sb.Append(windowWidth)
        sb.Append("','")
        sb.Append(windowHeight)
        sb.Append("','")
        If showScrollBars = True Then
            sb.Append("yes")
        Else
            sb.Append("no")
        End If
        sb.Append("','")
        If showMenu = True Then
            sb.Append("yes")
        Else
            sb.Append("no")
        End If
        sb.Append("','")
        If openFullScreen = True Then
            sb.Append("true")
        Else
            sb.Append("false")
        End If
        sb.Append("');")
        Return sb.ToString
    End Function

    Public Function GetSaveChangesBeforeLeavingJS(ByVal url As String, Optional ByVal ConfirmMessage As String = "") As String
        Dim sb As New StringBuilder
        Dim returnValue As String = String.Empty
        If ConfirmMessage.Length = 0 Then
            ConfirmMessage = RIRESOURCES.GetResourceValue("YOU HAVE ATTEMPTED TO LEAVE THIS PAGE. IF YOU HAVE MADE ANY CHANGES TO THE FIELDS WITHOUT CLICKING THE SAVE BUTTON, YOUR CHANGES", True, "Shared")
        End If

        ConfirmMessage = RI.SharedFunctions.DataClean(ConfirmMessage)
        ConfirmMessage = ConfirmMessage.Replace("'", "\'")
        sb.Append("javascript:viewIncident(""{0}"",""{1}"");")
        returnValue = String.Format(sb.ToString, url, ConfirmMessage)
        Return returnValue
    End Function

    Public Sub ShowPopUpMessage(ByVal message As String, ByVal title As String, ByVal buttonType As ucMessageBox.ButtonTypes, ByVal OKScript As String, ByVal CancelScript As String)
        With Me._ConfirmMessage
            .ButtonType = buttonType
            .Message = message
            .CancelScript = CancelScript
            .OKScript = OKScript
            .Title = title
            .ShowMessage()
        End With

    End Sub


    Public Sub HideMainMenu()
        _mainMenu.Items.Clear()
        _mainMenuBottom.Items.Clear()
        _mainMenu.Visible = False
        _mainMenuBottom.Visible = False
    End Sub
    Public Sub ShowPopupMenu(ByVal newMenu As MenuItemCollection, Optional ByVal index As Integer = 0, Optional ByVal ClearCurrentMenuItems As Boolean = False)
        Dim menuIndex As Integer = index
        ShowPopupMenu()

        If ClearCurrentMenuItems = True Then
            _mainMenu.Items.Clear()
            _mainMenuBottom.Items.Clear()
        End If
        If newMenu IsNot Nothing Then
            For i As Integer = 0 To newMenu.Count - 1
                Dim btmMenu As New MenuItem(newMenu(i).Text, newMenu(i).Value, newMenu(i).ImageUrl, newMenu(i).NavigateUrl, newMenu(i).Target)
                Dim topMenu As MenuItem = New MenuItem(newMenu(i).Text, newMenu(i).Value, newMenu(i).ImageUrl, newMenu(i).NavigateUrl, newMenu(i).Target)

                If menuIndex > newMenu.Count - 1 Then menuIndex = newMenu.Count - 1
                Me._mainMenu.Items.AddAt(menuIndex, topMenu)
                Me._mainMenuBottom.Items.AddAt(menuIndex, btmMenu)

                If newMenu(i).ChildItems.Count > 0 Then
                    For Each childMenu As MenuItem In newMenu(i).ChildItems
                        Dim btnChildMenu As New MenuItem(childMenu.Text, childMenu.Value, childMenu.ImageUrl, childMenu.NavigateUrl, childMenu.Target)
                        Dim topChildMenu As New MenuItem(childMenu.Text, childMenu.Value, childMenu.ImageUrl, childMenu.NavigateUrl, childMenu.Target)
                        Me._mainMenu.Items(menuIndex).ChildItems.Add(topChildMenu)
                        Me._mainMenuBottom.Items(menuIndex).ChildItems.Add(btnChildMenu)
                    Next
                End If
                menuIndex += 1
            Next
            Me.selectMenu()
        End If
    End Sub

    Private Function GetGlobalLocalizeText() As String


        Dim confirmMessage As String = RIRESOURCES.GetResourceValue("YOU HAVE ATTEMPTED TO LEAVE THIS PAGE. IF YOU HAVE MADE ANY CHANGES TO THE FIELDS WITHOUT CLICKING THE SAVE BUTTON, YOUR CHANGES")
        confirmMessage = confirmMessage.Replace("'", "\'")
        Dim sb As New StringBuilder
        sb.Append("var localizedText = new function(){")
        sb.Append("this.Close = '" & RIRESOURCES.GetResourceValue("Close") & "';")
        sb.Append("this.FunctionalLocationTree = '" & RIRESOURCES.GetResourceValue("FunctionalLocation") & "';")
        sb.Append("this.ConfirmDelete='" & RIRESOURCES.GetResourceValue("ConfirmDelete") & "';")
        sb.Append("this.ConfirmRedirect='" & RIRESOURCES.GetResourceValue(confirmMessage) & "';")
        sb.Append("this.EstDueandTaskDescRequired='" & RIRESOURCES.GetResourceValue("EstDueandTaskDescRequired") & "';")
        sb.Append("this.EstimatedDueDateRequired='" & RIRESOURCES.GetResourceValue("EstimatedDueDateRequired") & "';")
        sb.Append("this.TaskDescriptionRequired='" & RIRESOURCES.GetResourceValue("TaskDescriptionRequired") & "';")
        sb.Append("this.SelectStartEnd='" & RIRESOURCES.GetResourceValue("SELECT START AND END DATE") & "';")
        sb.Append("this.SelectEstimatedCompletionDate='" & RIRESOURCES.GetResourceValue("SELECT ESTIMATED COMPLETION DATE") & "';")
        sb.Append("this.SelectDateCompleted='" & RIRESOURCES.GetResourceValue("SELECT DATE COMPLETED") & "';")
        sb.Append("};")
        Dim returnVal As String = sb.ToString
        Return returnVal
    End Function


    Public Sub selectMenu()

        Dim ThisPage As String = Page.AppRelativeVirtualPath
        Dim SlashPos As Integer = InStrRev(ThisPage, "/")
        Dim PageName As String = Right(ThisPage, Len(ThisPage) - SlashPos)
        Dim ClientQry As String = Page.ClientQueryString

        'Deselect All menu items
        For i As Integer = 0 To _mainMenu.Items.Count - 1
            _mainMenu.Items(i).Selected = False
            _mainMenuBottom.Items(i).Selected = False
            If _mainMenu.Items(i).NavigateUrl.Contains(PageName) Then
                _mainMenu.Items(i).Selected = True
                _mainMenuBottom.Items(i).Selected = True
            End If
        Next


        If ClientQry.Contains("RINumber=") Then 'Deselect Current page from menu
            _mainMenu.StaticSelectedStyle.CssClass = "normallink"
            _mainMenuBottom.StaticSelectedStyle.CssClass = "normallink"
        Else
            _mainMenu.StaticSelectedStyle.CssClass = "selectedlink"
            _mainMenuBottom.StaticSelectedStyle.CssClass = "selectedlink"
        End If


    End Sub
    Public Sub ShowPopupMenu()
        Me._mainMenu.DataSourceID = ""
        Me._mainMenuBottom.DataSourceID = ""
        _mainMenu.Items.Clear()
        _mainMenuBottom.Items.Clear()
        'Me._mainMenu.Items.Add(New MenuItem("Close", "Close", Nothing, "javascript:window.close();"))
        Me._mainMenu.Items.Add(New MenuItem(RIRESOURCES.GetResourceValue("PrintPage"), "Print", Nothing, "javascript:window.print();"))
        'Me._mainMenuBottom.Items.Add(New MenuItem("Close", "Close", Nothing, "javascript:window.close();"))
        Me._mainMenuBottom.Items.Add(New MenuItem(RIRESOURCES.GetResourceValue("PrintPage"), "Print", Nothing, "javascript:window.print();"))

    End Sub

    Public Sub ShowAdminMenu()
        Me.SiteMapDataSource1.SiteMapProvider = "AdminSiteMapProvider"
        Me._mainMenu.DataBind()
    End Sub



    Public Sub ShowMOCMenu()


        Exit Sub


        'Dim userProfile As CurrentUserProfile = Nothing
        'userProfile = RI.SharedFunctions.GetUserProfile
        'Dim username As String


        If IsNothing(Session("UserProfile")) = True Then

            ' the session has expired
            Throw New Exception("Your session has expired.")

        End If


        Dim marketChannel As New clsMOCSecurity(GPI.XML.XMLUtility.ReadXmlNode(Session("UserProfile").ToString, "Username"))

        Dim newMenu As New MenuItemCollection
        Dim menuIndex As Integer = 0
        Dim parentMenu As New MenuItem
        Dim mnuURL As String = "GetSaveChangesBeforeLeavingJS('{0}')"

        Dim PromptToSave As Boolean
        Me._mainMenu.DataSourceID = ""
        Me._mainMenuBottom.DataSourceID = ""
        _mainMenu.Items.Clear()
        _mainMenuBottom.Items.Clear()

        If Page.AppRelativeVirtualPath.ToUpper.Contains("ENTERMOC.ASPX") Then PromptToSave = True

        newMenu.Add(New MenuItem("My MOC's", Nothing, Nothing, "~/MOC/MyMOCs.aspx", ""))

        'newMenu.Add(New MenuItem("My NEW MOC's", Nothing, Nothing, "~/MOC/MyMOCsNew.aspx", ""))

        newMenu.Add(New MenuItem("Enter MOC", Nothing, Nothing, "~/MOC/StartMOC.aspx", ""))
        newMenu.Add(New MenuItem("View MOC", Nothing, Nothing, "~/MOC/ViewMOC.aspx", ""))
        If marketChannel.Security.MarketChannelUser = False Then
            newMenu.Add(New MenuItem("Reporting", Nothing, Nothing, "~/MOC/Reporting.aspx", ""))

        End If
        'newMenu.Add(New MenuItem("Data Maintenance", Nothing, Nothing, "~/MOC/DataMaintenance/DataMaintenance.aspx", ""))


        parentMenu.Text = "Help"
        parentMenu.NavigateUrl = "~/RI/Help/Default.aspx"
        parentMenu.Value = ""
        'parentMenu.ChildItems.Add(New MenuItem("Using My Help", Nothing, Nothing, "~/RI/Help/UsingMyHelp.aspx", ""))
        'parentMenu.ChildItems.Add(New MenuItem("Online Training", Nothing, Nothing, "~/MOC/Help/MOCOnlineTraining.aspx", ""))

        If RI.SharedFunctions.IsAdminUser(My.User.Name) Then
            parentMenu.ChildItems.Add(New MenuItem("Impersonate User", Nothing, Nothing, "~/Admin/ImpersonateUser.aspx", ""))
            parentMenu.ChildItems.Add(New MenuItem("Cache Viewer", Nothing, Nothing, "~/Admin/CacheViewer.aspx", ""))
            'parentMenu.ChildItems.Add(New MenuItem("Configuration", Nothing, Nothing, "~/RI/Admin/DowntimeMessage.aspx", ""))
            'parentMenu.ChildItems.Add(New MenuItem("Manage Offline Status", Nothing, Nothing, "~/RI/Admin/ManageOfflineStatus.aspx", ""))
            'parentMenu.ChildItems.Add(New MenuItem("Missing Keys", Nothing, Nothing, "~/RI/Admin/MissingResourceKeys.aspx", ""))
            'parentMenu.ChildItems.Add(New MenuItem("Translation Management", Nothing, Nothing, "~/RI/datamaintenance/ResourceManagement.aspx", ""))
        End If

        'newMenu.Add(New MenuItem(RIRESOURCES.GetResourceValue("mnuMyMOCs", True), " ", Nothing, CStr(IIf(PromptToSave = True, GetSaveChangesBeforeLeavingJS(Page.ResolveClientUrl("~/MOC/MyMOCs.aspx")), "~/MOC/MyMOCs.aspx"))))
        'newMenu.Add(New MenuItem(RIRESOURCES.GetResourceValue("mnuEnterMOC", True), " ", Nothing, CStr(IIf(PromptToSave = True, GetSaveChangesBeforeLeavingJS(Page.ResolveClientUrl("~/MOC/EnterMOC.aspx")), "~/MOC/EnterMOC.aspx"))))
        'newMenu.Add(New MenuItem(RIRESOURCES.GetResourceValue("mnuViewUpdateMOC", True), "", Nothing, CStr(IIf(PromptToSave = True, GetSaveChangesBeforeLeavingJS(Page.ResolveClientUrl("~/MOC/ViewMOC.aspx")), "~/MOC/ViewMOC.aspx"))))
        'If marketChannel.Security.MarketChannelUser = False Then
        '    newMenu.Add(New MenuItem(RIRESOURCES.GetResourceValue("mnuReports", True), "", Nothing, CStr(IIf(PromptToSave = True, GetSaveChangesBeforeLeavingJS(Page.ResolveClientUrl("~/MOC/Reporting.aspx")), "~/MOC/Reporting.aspx"))))
        'End If
        'newMenu.Add(New MenuItem(RIRESOURCES.GetResourceValue("mnuDataMaintenance", True), "", Nothing, CStr(IIf(PromptToSave = True, GetSaveChangesBeforeLeavingJS(Page.ResolveClientUrl("~/MOC/DataMaintenance/DataMaintenance.aspx")), "~/MOC/DataMaintenance/DataMaintenance.aspx"))))

        'newMenu.Add(New MenuItem(RIRESOURCES.GetResourceValue("mnuRI", True), "", Nothing, CStr(IIf(PromptToSave = True, GetSaveChangesBeforeLeavingJS(Page.ResolveClientUrl("~/RI/ViewUpdateSearch.aspx")), "~/RI/ViewUpdateSearch.aspx")))) '("~/RI/ViewUpdateSearch.aspx")))


        'OLD HELP MENU
        'parentMenu.Text = RIRESOURCES.GetResourceValue("mnuHelp", True)
        'parentMenu.NavigateUrl = CStr(IIf(PromptToSave = True, GetSaveChangesBeforeLeavingJS(Page.ResolveClientUrl("~/RI/Help/Default.aspx")), "~/RI/Help/Default.aspx")) '"~/RI/Help/Default.aspx"
        'parentMenu.Value = ""

        'parentMenu.ChildItems.Add(New MenuItem(RIRESOURCES.GetResourceValue("mnuUsingMyHelp", True), Nothing, Nothing, CStr(IIf(PromptToSave = True, GetSaveChangesBeforeLeavingJS(Page.ResolveClientUrl("~/RI/Help/UsingMyHelp.aspx")), "~/RI/Help/UsingMyHelp.aspx"))))
        'parentMenu.ChildItems.Add(New MenuItem(RIRESOURCES.GetResourceValue("mnuOnlineTraining", True), Nothing, Nothing, CStr(IIf(PromptToSave = True, GetSaveChangesBeforeLeavingJS(Page.ResolveClientUrl("~/MOC/Help/MOCOnlineTraining.aspx")), "~/MOC/Help/MOCOnlineTraining.aspx")))) '"~/RI/Help/OnlineTraining.aspx", ""))
        ''parentMenu.ChildItems.Add(New MenuItem(RIRESOURCES.GetResourceValue("mnuEnhancements", True), Nothing, Nothing, CStr(IIf(PromptToSave = True, GetSaveChangesBeforeLeavingJS(Page.ResolveClientUrl("~/RI/Help/Enhancements.aspx")), "~/RI/Help/OutageEnhancements.aspx"))))
        'If RI.SharedFunctions.IsAdminUser(My.User.Name) Then
        '    parentMenu.ChildItems.Add(New MenuItem(RIRESOURCES.GetResourceValue("mnuImpersonateUser", True), Nothing, Nothing, CStr(IIf(PromptToSave = True, GetSaveChangesBeforeLeavingJS(Page.ResolveClientUrl("~/RI/Admin/ImpersonateUser.aspx")), "~/RI/Admin/ImpersonateUser.aspx"))))
        '    parentMenu.ChildItems.Add(New MenuItem(RIRESOURCES.GetResourceValue("mnuCacheViewer", True), Nothing, Nothing, CStr(IIf(PromptToSave = True, GetSaveChangesBeforeLeavingJS(Page.ResolveClientUrl("~/RI/Admin/CacheViewer.aspx")), "~/RI/Admin/CacheViewer.aspx"))))
        '    parentMenu.ChildItems.Add(New MenuItem(RIRESOURCES.GetResourceValue("mnuConfiguration", True), Nothing, Nothing, CStr(IIf(PromptToSave = True, GetSaveChangesBeforeLeavingJS(Page.ResolveClientUrl("~/RI/Admin/DowntimeMessage.aspx")), "~/RI/Admin/DowntimeMessage.aspx"))))
        '    parentMenu.ChildItems.Add(New MenuItem("Manage Offline Status", Nothing, Nothing, CStr(IIf(PromptToSave = True, GetSaveChangesBeforeLeavingJS(Page.ResolveClientUrl("~/RI/Admin/ManageOfflineStatus.aspx")), "~/RI/Admin/ManageOfflineStatus.aspx"))))
        '    parentMenu.ChildItems.Add(New MenuItem("Missing Keys", Nothing, Nothing, "javascript:DisplayMissingKeys('" & Page.ResolveClientUrl("~/RI/Admin/MissingResourceKeys.aspx") & "');", ""))
        '    parentMenu.ChildItems.Add(New MenuItem(RIRESOURCES.GetResourceValue("Translation Management", True), Nothing, Nothing, CStr(IIf(PromptToSave = True, GetSaveChangesBeforeLeavingJS(Page.ResolveClientUrl("~/RI/datamaintenance/ResourceManagement.aspx")), "~/RI/datamaintenance/ResourceManagement.aspx"))))
        'End If




        newMenu.Add(parentMenu)
        Me.ShowPopupMenu(newMenu, 0, True)
    End Sub


    Public Sub ShowMOCMenu(ByVal admin As String)

        'Dim userProfile As CurrentUserProfile = Nothing
        'userProfile = RI.SharedFunctions.GetUserProfile
        'Dim username As String


        If IsNothing(Session("UserProfile")) = True Then

            ' the session has expired
            Throw New Exception("Your session has expired.")

        End If


        If admin = "MILLADMIN" Then
            RadMenu1.LoadContentFile("~/Menu/Data/AdminMenu.xml")
        Else
            RadMenu1.LoadContentFile("~/Menu/Data/UserMenu.xml")
        End If


        Dim currentItem As RadMenuItem = RadMenu1.FindItemByUrl(Request.Url.PathAndQuery)

        If currentItem IsNot Nothing Then
            'Select the current item and his parents
            currentItem.HighlightPath()
            'Update the title of the 
            'PageTitleLiteral.Text = currentItem.Text
            'Populate the breadcrumb
            'DataBindBreadCrumbSiteMap(currentItem)
        Else
            RadMenu1.Items(1).HighlightPath()
        End If


        '======================================================
        Exit Sub
        '======================================================




        Dim marketChannel As New clsMOCSecurity(GPI.XML.XMLUtility.ReadXmlNode(Session("UserProfile").ToString, "Username"))

        Dim newMenu As New MenuItemCollection
        Dim menuIndex As Integer = 0
        Dim parentMenu As New MenuItem
        Dim mnuURL As String = "GetSaveChangesBeforeLeavingJS('{0}')"

        Dim PromptToSave As Boolean
        Me._mainMenu.DataSourceID = ""
        Me._mainMenuBottom.DataSourceID = ""
        _mainMenu.Items.Clear()
        _mainMenuBottom.Items.Clear()

        If Page.AppRelativeVirtualPath.ToUpper.Contains("ENTERMOC.ASPX") Then PromptToSave = True

        newMenu.Add(New MenuItem("My MOC's", Nothing, Nothing, "~/MOC/MyMOCs.aspx", ""))

        'newMenu.Add(New MenuItem("My NEW MOC's", Nothing, Nothing, "~/MOC/MyMOCsNew.aspx", ""))

        newMenu.Add(New MenuItem("Enter MOC", Nothing, Nothing, "~/MOC/StartMOC.aspx", ""))
        newMenu.Add(New MenuItem("View MOC", Nothing, Nothing, "~/MOC/ViewMOC.aspx", ""))
        If marketChannel.Security.MarketChannelUser = False Then
            newMenu.Add(New MenuItem("Reporting", Nothing, Nothing, "~/MOC/Reporting.aspx", ""))

        End If
        'newMenu.Add(New MenuItem("Data Maintenance", Nothing, Nothing, "~/MOC/DataMaintenance/DataMaintenance.aspx", ""))





        If admin = "MILLADMIN" Then
            newMenu.Add(New MenuItem("Default Approvers", Nothing, Nothing, "~/MOC/DataMaintenance/MOCDefaultApprovers.aspx", ""))
            newMenu.Add(New MenuItem("Cache Viewer", Nothing, Nothing, "~/Admin/CacheViewer.aspx", ""))
            newMenu.Add(New MenuItem("Impersonate User", Nothing, Nothing, "~/Admin/ImpersonateUser.aspx", ""))

        End If


        parentMenu.Text = "Help"
        parentMenu.NavigateUrl = "~/RI/Help/Default.aspx"
        parentMenu.Value = ""
        'parentMenu.ChildItems.Add(New MenuItem("Using My Help", Nothing, Nothing, "~/RI/Help/UsingMyHelp.aspx", ""))
        'parentMenu.ChildItems.Add(New MenuItem("Online Training", Nothing, Nothing, "~/MOC/Help/MOCOnlineTraining.aspx", ""))

        If RI.SharedFunctions.IsAdminUser(My.User.Name) Then
            'parentMenu.ChildItems.Add(New MenuItem("Impersonate User", Nothing, Nothing, "~/Admin/ImpersonateUser.aspx", ""))
            'parentMenu.ChildItems.Add(New MenuItem("Cache Viewer", Nothing, Nothing, "~/Admin/CacheViewer.aspx", ""))

            'parentMenu.ChildItems.Add(New MenuItem("Configuration", Nothing, Nothing, "~/RI/Admin/DowntimeMessage.aspx", ""))
            'parentMenu.ChildItems.Add(New MenuItem("Manage Offline Status", Nothing, Nothing, "~/RI/Admin/ManageOfflineStatus.aspx", ""))
            'parentMenu.ChildItems.Add(New MenuItem("Missing Keys", Nothing, Nothing, "~/RI/Admin/MissingResourceKeys.aspx", ""))
            'parentMenu.ChildItems.Add(New MenuItem("Translation Management", Nothing, Nothing, "~/RI/datamaintenance/ResourceManagement.aspx", ""))
        End If






        newMenu.Add(parentMenu)
        Me.ShowPopupMenu(newMenu, 0, True)
    End Sub


    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init


        Context.Request.Browser.Adapters.Clear()
        Server.ScriptTimeout = 9000

        If CType(Application.Item("TestDatabase"), String) = "YES" Then
            LabelWarning.Visible = CBool("True")
        Else
            LabelWarning.Visible = CBool("False")
        End If

        Try


            If IsNothing(Session("UserProfile")) = True Then
                'we have a problem
                'need to go get the user info again.
                'but for know just throw an error
                Throw New Exception("Session has expired")

            Else

                LabelUserName.Text = "User: " & GPI.XML.XMLUtility.ReadXmlNode(Session("UserProfile").ToString, "FullName") & " (" & GPI.XML.XMLUtility.ReadXmlNode(Session("UserProfile").ToString, "DefaultFacility") & ")"
                LabelDatabase.Text = GetDataBaseName()

            End If



            'ShowRIMenu()

            copyrightinfo.Text = Chr(169) & " " & Now.Year & " Graphic Packaging International. All rights reserved."
        Catch ex As Exception
            Server.ClearError()

        End Try
    End Sub

    Public Function GetDataBaseName() As String
        If Application.Item("Servicename") IsNot Nothing Then
            Return CStr(Application.Item("Servicename"))
        Else
            Return "Unknown"
        End If
    End Function

    Public Function GetApplicationVersion() As String
        Try
            Dim buildDate = System.IO.File.GetLastWriteTime(System.Reflection.Assembly.GetExecutingAssembly().Location)
            Return String.Format("{0}.{1}.{2}.{3}.{4}", buildDate.Year, buildDate.Month, buildDate.Day, buildDate.Hour, buildDate.Minute)
        Catch ex As Exception
            Return "1.1.1"
        End Try
    End Function

    Public Sub AddJavascript()
        Dim jsVersion As String = GetApplicationVersion()
        With _scriptManager
            .Scripts.Add(New ScriptReference("~/JQueryTimePicker/jquery-1.11.1.min.js"))
            .Scripts.Add(New ScriptReference("~/JQueryTimePicker/jquery-ui.min.js"))
            .Scripts.Add(New ScriptReference("~/JQueryTimePicker/jquery-ui-timepicker-addon.js"))
            .Scripts.Add(New ScriptReference("~/JQueryTimePicker/jquery-ui-timepicker-addon-i18n.min.js"))
            .Scripts.Add(New ScriptReference("~/JQueryTimePicker/jquery-ui-sliderAccess.js"))
            .Scripts.Add(New ScriptReference("~/JQueryTableSorter/jquery.tablesorter.min.js"))
            .Scripts.Add(New ScriptReference("~/DropInContent.js?v=" & jsVersion))
            .Scripts.Add(New ScriptReference("~/MasterPage.js?v=" & jsVersion))
            .Scripts.Add(New ScriptReference("~/windowfiles/dhtmlwindow.js?v=" & jsVersion))
            .Scripts.Add(New ScriptReference("~/modalfiles/modal.js?v=" & jsVersion))
            '.Scripts.Add(New ScriptReference("~/Outage/textareaAutoResize.js?v=" & jsVersion))

        End With
    End Sub

    Public Sub RegisterStartupScript()
        Page.ClientScript.RegisterStartupScript(Page.GetType, "unload", " var ConfirmBeforeLeave=" & CStr(UnloadEvents).ToLower & ";", True)

    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddJavascript()
        RegisterStartupScript()
        If Not Page.ClientScript.IsOnSubmitStatementRegistered(Page.GetType, "PrepareValidationSummary") Then
            Page.ClientScript.RegisterOnSubmitStatement(Page.GetType, "PrepareValidationSummary", "CheckForm('" & _ValidationSummaryMessage.MessageClientID & "','" & _ValidationSummaryMessage.MessageTriggerClientID & "')")
        End If


        Page.ClientScript.RegisterClientScriptBlock(Page.GetType, "GetGlobalLocalizeText", GetGlobalLocalizeText(), True)


        Me._ValidationSummaryMessage.Title = "Warning!"
        Me._ValidationSummaryMessage.ButtonType = ucMessageBox.ButtonTypes.OK

        Me.selectMenu()

        'Dim username As String = CurrentUserProfile.GetCurrentUser


        MyGlobalUsername = CStr(Session("ImpersonateUser"))

        If MyGlobalUsername = "nothing" Then
            LabelDatabaseText.Visible = False
            LabelImperonateUser.Visible = False
        Else
            LabelDatabaseText.Visible = True
            LabelImperonateUser.Visible = True
            LabelImperonateUser.Text = MyGlobalUsername

        End If


    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        Server.ScriptTimeout = 90
        Me.RIRESOURCES.Dispose()
    End Sub

    Public Sub New()

    End Sub

    'Public Sub SetImpersonaterUser(ByVal currentUser As String, ByVal fullName As String)



    Public Sub SetImpersonateUser(ByVal currentUser As String)
        '_login.SetCurrentUserLabel(currentUser, fullName)
        LabelImperonateUser.Text = currentUser
        LabelImperonateUser.Visible = True
        LabelDatabaseText.Visible = True
        LabelImperonateUser.Visible = True
    End Sub


End Class

'End Namespace