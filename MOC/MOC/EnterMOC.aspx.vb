Imports Devart.Data.Oracle
Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.Drawing
Imports Telerik.Web.UI
Imports GPI.GlobalClass.GlobalVariables
Imports System.Threading
Imports System.Diagnostics
Imports clsCurrentMOC
Imports ClassMOC

Partial Class MOC_EnterMOCNew
    Inherits RIBasePage

    Dim enterMOC As clsEnterMOC
    Dim currentMOC As clsCurrentMOC
    Dim userProfile As RI.CurrentUserProfile = Nothing
    Dim selectedFacility As String = String.Empty
    Dim selectedBusArea As String = String.Empty
    Dim selectedLine As String = String.Empty
    Dim NewMOCFlag As String = String.Empty
    Dim MOCInitiator As String = String.Empty
    Dim showMOC As Boolean = True
    Dim tracing As Boolean = ConfigurationManager.AppSettings("Tracing")
    Dim tracingFunctions As Boolean = ConfigurationManager.AppSettings("TracingFunctions")
    Dim logging As Boolean = ConfigurationManager.AppSettings("Logging")
    Dim testingemail As String = ConfigurationManager.AppSettings("TestingEmail")

    Dim CurrentApprover_UpdateCommand As Integer = 0
    Dim CurrentBUMApprover_UpdateCommand As Integer = 0



    Dim AuthLevel As String = ""


    Public _reviewersupdate As New reviewersUpdate()

    Dim _classmoc As New ClassMOC()


    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        RI.SharedFunctions.InsertRILoggingRecord(
            "EnterMOC.aspx.vb", "Page_Init ")



        MyGlobalAuthLevel = GPI.XML.XMLUtility.ReadXmlNode(Session("UserProfile").ToString, "AuthLevel")
        MyGlobalAuthLevelID = GPI.XML.XMLUtility.ReadXmlNode(Session("UserProfile").ToString, "AuthLevelID")
        MyGlobalBusType = GPI.XML.XMLUtility.ReadXmlNode(Session("UserProfile").ToString, "BusType")
        MyGlobalDefaultDivision = GPI.XML.XMLUtility.ReadXmlNode(Session("UserProfile").ToString, "DefaultDivision")
        MyGlobalDefaultFacility = GPI.XML.XMLUtility.ReadXmlNode(Session("UserProfile").ToString, "DefaultFacility")
        MyGlobalDefaultLanguage = GPI.XML.XMLUtility.ReadXmlNode(Session("UserProfile").ToString, "DefaultLanguage")
        MyGlobalDistinguishedName = GPI.XML.XMLUtility.ReadXmlNode(Session("UserProfile").ToString, "DistinguishedName")
        MyGlobalDivestedLocation = GPI.XML.XMLUtility.ReadXmlNode(Session("UserProfile").ToString, "DivestedLocation")
        MyGlobalDomainName = GPI.XML.XMLUtility.ReadXmlNode(Session("UserProfile").ToString, "DomainName")
        MyGlobalEmail = GPI.XML.XMLUtility.ReadXmlNode(Session("UserProfile").ToString, "Email")
        MyGlobalFullName = GPI.XML.XMLUtility.ReadXmlNode(Session("UserProfile").ToString, "FullName")
        MyGlobalGroupName = GPI.XML.XMLUtility.ReadXmlNode(Session("UserProfile").ToString, "GroupName")
        MyGlobalInActiveFlag = GPI.XML.XMLUtility.ReadXmlNode(Session("UserProfile").ToString, "InActiveFlag")
        MyGlobalProfileTable = GPI.XML.XMLUtility.ReadXmlNode(Session("UserProfile").ToString, "ProfileTable")
        MyGlobalUsername = GPI.XML.XMLUtility.ReadXmlNode(Session("UserProfile").ToString, "Username")


        Master.ShowMOCMenu(MyGlobalAuthLevel)

        If CStr(Session("ImpersonateUser")) = "nothing" Then
            'do nothing
        Else
            MyGlobalUsername = CStr(Session("ImpersonateUser"))
        End If

        AuthLevel = MyGlobalAuthLevel




        RI.SharedFunctions.InsertRILoggingRecord(
    "EnterMOC.aspx.vb", "Page_Init END")

    End Sub


    Protected Sub _gvApprovals_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles _gvApprovals.RowDataBound

        Dim btnApproverDelete As Button = CType(e.Row.Cells(0).FindControl("_btnApproverDelete"), Button)
        Dim lbApproveCheckBox As CheckBoxList = CType(e.Row.Cells(0).FindControl("_cbApproval"), CheckBoxList)
        Dim lbComments As TextBox = CType(e.Row.Cells(0).FindControl("_txtComments"), TextBox)
        Dim lbRequired As Label = CType(e.Row.Cells(0).FindControl("_lblRequired"), Label)
        Dim lbRoles As Label = CType(e.Row.Cells(0).FindControl("_lblRoleUserNames"), Label)

        If e.Row.RowIndex >= 0 Then
            Dim roles As String
            roles = lbRoles.Text
            If roles.Contains(MyGlobalUsername) Then
                lbApproveCheckBox.Enabled = True
                lbComments.Enabled = True
                Session("SavedApprovalRow") = e.Row.RowIndex
            Else
                lbApproveCheckBox.Enabled = False
                lbComments.ReadOnly = True
            End If

            If AuthLevel = "MILLADMIN" Then
                btnApproverDelete.Visible = True
            Else
                btnApproverDelete.Visible = False
            End If


        End If
    End Sub

    Protected Sub _gvApprovals_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles _gvApprovals.RowCreated


        If Page.IsPostBack = True Then
            Exit Sub
        Else

            RI.SharedFunctions.InsertRILoggingRecord("EnterMOC.aspx.vb", "_gvApprovals_RowCreated Enter")
            If e.Row.RowIndex >= 0 Then
                Dim cbApproval As CheckBoxList = CType(e.Row.Cells(2).FindControl("_cbApproval"), CheckBoxList)
                Dim approval_flag As String = RI.SharedFunctions.DataClean(DataBinder.Eval(e.Row.DataItem, "approved"), "")
                If cbApproval IsNot Nothing Then
                    If approval_flag = "Y" Then
                        cbApproval.SelectedValue = "Y"
                    Else
                        If approval_flag = "N" Then
                            cbApproval.SelectedValue = "N"
                        End If
                    End If
                End If
            End If
            RI.SharedFunctions.InsertRILoggingRecord("EnterMOC.aspx.vb", "_gvApprovals_RowCreated Exit")

        End If
    End Sub


    Protected Sub _gvApprovals_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles _gvApprovals.RowDeleting
        Dim returnStatus As String
        Dim approvalEmailList As String = ""
        Dim ccEmailList As String = ""
        Dim approvalEmailMsg As String = ""

        returnStatus = clsCurrentMOC.DeleteMOCApproval(currentMOC.MOCNumber, currentMOC.UserName, "L1", Me._gvApprovals.DataKeys.Item(e.RowIndex).Value.ToString)
        If returnStatus = "888" Then
            ccEmailList = currentMOC.MOCCoordinatorEmail & "," & currentMOC.OwnerEmail

            'This indicates that all L1 approvers have approved the moc and an email should be sent to L2 approvers.
            EmailMOC("Approved", approvalEmailList, ccEmailList, approvalEmailMsg)
        End If

        currentMOC = New clsCurrentMOC(currentMOC.MOCNumber)
        Response.Clear()
        Response.Redirect(Page.AppRelativeVirtualPath & "?MOCNumber=" & currentMOC.MOCNumber, True)
        Response.End()
    End Sub



    Protected Sub RadGridAssignedApprover_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGridAssignedApprover.NeedDataSource


        If Not Page.IsPostBack Then
        Else
        End If

    End Sub


    Private Sub NewMOCSetup()

        NewMOCFlag = "Y"

        _pnlApprovals.Visible = False
        LabelApprovers.Visible = False
        Me._btnDelete.Visible = False
        Me._tblTempTaskTitle.Visible = False
        Me._tblCommentHeader.Visible = False
        Me._pnlComments.Visible = False
        Me._lblCatQuestions.Attributes.Add("Style", "display: none;")
        Me._ccdlOwner.SelectedValue = MyGlobalUsername 'userProfile.Username

        Master.SetBanner(Master.RIRESOURCES.GetResourceValue("EnterNewMOC", True, "MOC"))
        _btnSubmit.Text = Master.RIRESOURCES.GetResourceValue("SaveNewMOCRecord", True, "MOC")
        _tbDivision.Text = MyGlobalDefaultDivision  'userProfile.DefaultDivision

        Me._cddlFacility.SelectedValue = MyGlobalDefaultFacility
        selectedFacility = MyGlobalDefaultFacility 'userProfile.DefaultFacility

        'enterMOC = New clsEnterMOC(userProfile.Username, selectedFacility, userProfile.InActiveFlag, userProfile.DefaultDivision, selectedBusArea, selectedLine)

        enterMOC = New clsEnterMOC(MyGlobalUsername, selectedFacility, MyGlobalInActiveFlag, MyGlobalDefaultDivision, selectedBusArea, selectedLine)

        SetupFunctionalLocation()
        'If Me._functionalLocationTree.Text.Length = 0 Then
        '    Me._functionalLocationTree.PopulateFunctionalLocation(selectedFacility)
        'End If


    End Sub

    Private Sub GetMOCRecord()

        NewMOCFlag = "N"
        RI.SharedFunctions.InsertRILoggingRecord("EnterMOC.aspx.vb", "GetMOCRecord START ")


        currentMOC = New clsCurrentMOC(Request.QueryString("MOCNumber"))

        GetMOCSuperintendent(currentMOC.MOCNumber, currentMOC.SuperintendentType)


        'currentMOC.UserName = userProfile.Username
        currentMOC.UserName = MyGlobalUsername
        _tbDivision.Text = currentMOC.Division

        'Check for NAIPG Market Channel user to determine if the MOC can be viewed
        Dim marketChannel As New clsMOCSecurity(currentMOC.UserName)
        Dim marketChannelRole As String = marketChannel.Security.MarketChannelRole
        If currentMOC.MarketChannelSubCategory IsNot Nothing Then
            Dim MOCSubCategory As String = currentMOC.MarketChannelSubCategory.ToUpper
            If marketChannelRole = "DOMESTIC" Or marketChannelRole = "EXPORT" Then
                If MOCSubCategory = "TRADES" Or MOCSubCategory = "NAC" Then
                    showMOC = False
                    'ModalPopupExtender1.Show()
                    'Response.Redirect(Page.AppRelativeVirtualPath, True)
                    'MsgBox("Unauthorized")
                End If
            ElseIf marketChannelRole = "TRADES" Then

                If MOCSubCategory = "DOMESTIC" Or MOCSubCategory = "NAC" Or MOCSubCategory = "EXPORT" Then
                    showMOC = False
                    '                       ModalPopupExtender1.Show()
                    'Response.Redirect(Page.AppRelativeVirtualPath, True)
                    'MsgBox("Unauthorized")
                End If
            ElseIf marketChannelRole = "NAC" Then
                If MOCSubCategory = "TRADES" Or MOCSubCategory = "DOMESTIC" Or MOCSubCategory = "EXPORT" Then
                    showMOC = False
                    '                       ModalPopupExtender1.Show()
                    'Response.Redirect(Page.AppRelativeVirtualPath, True)

                End If
            End If

        End If


        'If Page.IsPostBack Then

        If showMOC = True Then


            If currentMOC.SiteID Is Nothing Then currentMOC = Nothing
            If currentMOC IsNot Nothing Then

                _pnlApprovals.Visible = True
                LabelApprovers.Visible = True


                'enterMOC = New clsEnterMOC(userProfile.Username, currentMOC.SiteID, userProfile.InActiveFlag, , currentMOC.BusinessUnit, currentMOC.Line, Request.QueryString("MOCNumber"))
                enterMOC = New clsEnterMOC(MyGlobalUsername.ToString, currentMOC.SiteID, MyGlobalInActiveFlag.ToString, currentMOC.BusinessUnit, currentMOC.Line, Request.QueryString("MOCNumber"))

                selectedFacility = currentMOC.SiteID
                selectedBusArea = currentMOC.BusinessUnit
                selectedLine = currentMOC.Line

                MOCInitiator = currentMOC.MOCCoordinator


                SetupFunctionalLocation()

                If currentMOC.Status = "Superintendent Approved" Then
                    'do nothing
                Else
                    PopulateNotificationList(currentMOC.MOCNumber)
                End If

            Else
                'User has tried to load an invalid MOC Number
                'MJP - 5/13/2010 - Added the response.redirect to send the user back the Enter New MOC screen
                Response.Redirect(Page.AppRelativeVirtualPath, True)
            End If

            If Request.QueryString("UserName") IsNot Nothing Then
                If currentMOC IsNot Nothing Then
                    currentMOC.Approved = "Y"
                    currentMOC.SaveMOCApproval(Request.QueryString("MOCNumber"), Request.QueryString("UserName"), "E", "", "")
                    Response.Clear()
                    Response.Redirect(Page.AppRelativeVirtualPath & "?MOCNumber=" & Request.QueryString("MOCNumber"), True)
                    Response.End()
                End If
            End If


            DisplayUpdateMenu()


            'Only display delete button for facility admins and MOC initiator
            If AuthLevel <> "MILLADMIN" Then
                If currentMOC.Status = "Approval Requested" Or currentMOC.Status = "Not Approved" Then
                    'If currentMOC.MOCCoordinator <> userProfile.Username Then
                    Me._ddlFacility.Enabled = False
                    Me._ddlBusinessUnit.Enabled = False
                    Me._ddlLineBreak.Enabled = False
                    Me._txtTitle.Enabled = False
                    Me._txtDescription.Enabled = False
                    Me._txtImpact.Enabled = False
                    Me._txtSavings.Enabled = False
                    Me._tbCosts.Enabled = False
                    'functional location tree
                    'Me._FL.Enabled = False

                    'EnableUserControls(False, Me._MOCType)
                    _lblDesc.Enabled = True
                    '_lblDesc.Text = userProfile.DomainName & "/" & userProfile.Email & " *** This MOC has been submitted for approval.  Certain fields may not be changed at this point. ***"
                    _lblDesc.Text = MyGlobalDomainName & "/" & MyGlobalEmail & " *** This MOC has been submitted for approval.  Certain fields may not be changed at this point. ***"

                ElseIf Mid(currentMOC.Status, 1, 8) = "Approved" Or currentMOC.Status = "Implemented" Or currentMOC.Status = "Completed" Then
                    EnableUserControls(False, Me._MOCType)
                    EnableUserControls(False, Me._MOCClass)
                    Me._ddlFacility.Enabled = False
                    Me._ddlBusinessUnit.Enabled = False
                    Me._ddlLineBreak.Enabled = False
                    Me._txtTitle.Enabled = False
                    Me._txtDescription.Enabled = False
                    Me._txtImpact.Enabled = False
                    Me._txtSavings.Enabled = False
                    Me._tbCosts.Enabled = False
                    'functional location tree
                    'Me._FL.Enabled = False
                    Me._MOCCat.enablePanel = False

                    _lblDesc.Text = "*** This MOC has been approved.  Changes can only be made to Approver List and System Checklist. ***"
                    _lblDesc.Enabled = True

                End If
            End If


        Else
            _mpeNotAuthorized.Show()
        End If


        RI.SharedFunctions.InsertRILoggingRecord("EnterMOC.aspx.vb", "GetMOCRecord END ")




    End Sub

    Private Sub EnterMOCLoad()


        If Request.QueryString("MOCNumber") Is Nothing Then
            Dim sredirect As String
            sredirect = "~/moc/viewmoc.aspx"
            Response.Redirect(sredirect, False)
        End If

        RI.SharedFunctions.InsertRILoggingRecord("EnterMOC.aspx.vb", "START EnterMOCLoad ")


        If Request.QueryString("MOCNumber") Is Nothing Then
            _pnlUpdateButtons.Visible = False
            _btnSubmit.Visible = False
            _btnSpell.Visible = False
            _btnDelete.Visible = False
            RI.SharedFunctions.InsertRILoggingRecord("EnterMOC.aspx.vb", "EXIT SUB EnterMOCLoad ")

            Exit Sub

        End If


        Dim ipLoc As New IP.Bids.Localization.WebLocalization()
        Dim popupJS As String = "Javascript:displayModalPopUpWindow('{0}','{1}','{2}','{3}','{4}');"

        RI.SharedFunctions.DisablePageCache(Response)


        Dim sc As ScriptManager
        sc = CType(Page.Form.FindControl("_scriptManager"), ScriptManager)
        If sc IsNot Nothing Then
            Dim loService As New ServiceReference
            loService.InlineScript = False
            loService.Path = "~/FunctionalLocation.asmx"
            sc.Services.Add(loService)
        End If
        If sc IsNot Nothing Then
            Dim SiteService As New ServiceReference
            SiteService.InlineScript = False
            SiteService.Path = "~/RIMOCSharedWS.asmx"
            sc.Services.Add(SiteService)
        End If
        If sc IsNot Nothing Then
            Dim MTTService As New ServiceReference
            MTTService.InlineScript = False
            MTTService.Path = "~/MOC/TaskTracker.asmx"
            sc.Services.Add(MTTService)
        End If

        ScriptManager.RegisterClientScriptInclude(Me._udpLocation, _udpLocation.GetType, "EnterMOC", Page.ResolveClientUrl("~/moc/EnterMOC.js?v=1"))
        ScriptManager.RegisterStartupScript(Me.Page, Page.GetType, "GetGlobalJSVarMOC", GetGlobalJSVarMOC, True)
        'ScriptManager.RegisterStartupScript(Page, Page.GetType, "PerformanceDateTimePicker", "$('.PerformanceDateTimePicker').datetimepicker();", True)
        'ScriptManager.RegisterStartupScript(Page, Page.GetType, "TimePicker", "$('#basicExample').timepicker();", True)


        Me._hypSystemDefinition.NavigateUrl = "javascript:var x=dhtmlmodal.open('SystemDefinition', 'div', '_divSystemDefinition', '" & Master.RIRESOURCES.GetResourceValue("Definition", False, "Shared") & "', 'width=400px,height=250px,center=1,resize=0,scrolling=1');"
        _ddlApproverFacilityNew.Attributes.Add("onchange", "this.blur;GetEmployee('" & _ddlApproverFacilityNew.ClientID & "','" & Me._slbApprovalNotificationList.AvailableListID & "');")
        'JEB      Me._hypApproval.NavigateUrl = String.Format(popupJS, Page.ResolveUrl("~/images/moc workflow.gif#view=fit"), "MC", "MOC Workflow", "950", "700")
        Me._hypMOCTemplateTasks.NavigateUrl = "javascript:var x=dhtmlmodal.open('MOCTempTaskDefinition', 'div', '_divMOCDefinition', '" & Master.RIRESOURCES.GetResourceValue("Definition", False, "Shared") & "', 'width=400px,height=250px,center=1,resize=0,scrolling=1');"



        If Request.QueryString("MOCNumber") IsNot Nothing Then
            GetMOCRecord()
        Else
            'this will never be called if there is no mocnumber
            'in the querystring there is a redirect to viewmoc.aspx
            NewMOCSetup()
        End If

        'setup for reporting path
        Dim urlPath As String = String.Empty
        If Request.Url.Host = "localhost" Then

            'If Request.UserHostAddress = "127.0.0.1" Or Request.UserHostAddress = "http://s29edev13/riajax" Then
            'urlPath = "http://ridev/CEReporting/"
            urlPath = "http://gpiazmeswebp01:7777/ReportMOCSummary.aspx?ReportName=MOCSummary&MOCNumber={0}"

        ElseIf Request.Url.Port = "6569" Then
            'urlPath = "../../CEReporting/"
            'TODO change for production
            'urlPath = "http://gpiazmeswebp01:6767/ReportMOCSummary.aspx?ReportName=MOCSummary&MOCNumber={0}"
            urlPath = "http://gpiazmeswebp01:7777/ReportMOCSummary.aspx?ReportName=MOCSummary&MOCNumber={0}"

        Else


            'urlPath = "http://gpiazmeswebp01:6767/ReportMOCSummary.aspx?ReportName=MOCSummary&MOCNumber={0}"

        End If
        Dim MOCSummaryURL As String = String.Format(urlPath, currentMOC.MOCNumber)

        Me._btnDetailReport.OnClientClick = Master.GetPopupWindowJS(MOCSummaryURL, "MOCSummary", 600, 300, True, True, True) & ";return false;"



        If currentMOC.Status = "Superintendent Approved" Then
            Me.MOCStatus.Visible = False
            Me._pnlApprovals.Visible = False
        Else
            Me.MOCStatus.Visible = True
            Me._pnlApprovals.Visible = True

        End If


        If Page.IsPostBack Then
            If RI.SharedFunctions.CausedPostBack(Me.MOCStatus.UniqueID) Then
                If Request(MOCStatus.UniqueID & "$_ddlStatus") IsNot Nothing Then
                    MOCStatus.Status = Request(MOCStatus.UniqueID & "$_ddlStatus")
                End If
                MOCStatus_StatusChanged(Nothing)
            End If
        End If

        If enterMOC IsNot Nothing Then 'Or currentMOC IsNot Nothing Then
            If Not Page.IsPostBack Then
                Me._dlSystem.DataSource = enterMOC.SystemDT
                Me._dlSystem.DataBind()

            End If
        End If

        RI.SharedFunctions.InsertRILoggingRecord("EnterMOC.aspx.vb", "END EnterMOCLoad ")
    End Sub
    Private Sub MOC_EnterMOCNew_Load(sender As Object, e As EventArgs) Handles Me.Load


        If Page.IsPostBack = True Then
            Exit Sub
        Else
            EnterMOCLoad()
        End If



    End Sub
    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        RI.SharedFunctions.InsertRILoggingRecord(
            "EnterMOC.aspx.vb", "Page_LoadComplete START ========")






        If Request.QueryString("MOCNumber") IsNot Nothing Then
            If AuthLevel = "MILLADMIN" Or currentMOC.MOCCoordinator = MyGlobalUsername.ToString Then
                Me._btnDelete.Visible = True
            Else
                Me._btnDelete.Visible = False
            End If
        End If



        If (_gvApprovals.Rows.Count = 0 And RadGridAssignedApprover.Items.Count = 0) Then

            noReviewersAssigned.Visible = True

        Else

            noReviewersAssigned.Visible = False

        End If

        If Not Page.IsPostBack Then
            If currentMOC IsNot Nothing And showMOC = True Then
                GetMOC()
            End If
        End If

        RI.SharedFunctions.InsertRILoggingRecord(
    "EnterMOC.aspx.vb", "Page_LoadComplete END ========")

    End Sub
    Private Sub DisplayUpdateMenu()
        RI.SharedFunctions.InsertRILoggingRecord("EnterMOC.aspx.vb", "DisplayUpdateMenu ")
        Dim ret As String = "return false;"
        Dim confirmMessage As String = "localizedText.ConfirmRedirect"

        Dim urlPath As String
        If Request.UserHostAddress = "127.0.0.1" Or Request.UserHostAddress = "http://s29edev13/riajax" Then
            'urlPath = "http://ridev/CEReporting/"
            urlPath = "http://gpimv.graphicpkg.com/CEReporting/"


        Else
            'urlPath = "../../CEReporting/"
            urlPath = "http://gpimv.graphicpkg.com/CEReporting/"
        End If
        Dim MOCSummaryURL As String = String.Format(urlPath & "CrystalReportDisplay.aspx?Report=MOCSummary&mocNumber={0}", currentMOC.MOCNumber)
        Me._btnDetailReport.OnClientClick = Master.GetPopupWindowJS(MOCSummaryURL, "MOCSummary", 600, 300, True, True, True) & ";return false;"

        Me._btnAttachment.Text = Master.RIRESOURCES.GetResourceValue("Attachments", True, "Shared") & " (" & CStr(currentMOC.AttachmentCount) & ")"
        Me._btnMOCActionItems.Text = Master.RIRESOURCES.GetResourceValue("Task Items", True, "Shared") & " (" & CStr(currentMOC.TaskCount) & ")"
        'Me._btnOkSaveApprovers.Text = Master.RIRESOURCES.GetResourceValue("Save MOC", True, "Shared")
        Me._btnOkSaveApprovers.Text = "Save Reviewers"
        Me._btnSaveDraft.Visible = False

        Master.SetBanner(Master.RIRESOURCES.GetResourceValue("UpdateMOC", True, "MOC") & " " & currentMOC.MOCNumber)
        _btnSubmit.Text = Master.RIRESOURCES.GetResourceValue("SaveMOCRecord", True, "MOC")


        Me._pnlUpdateButtons.Visible = True
        Me._btnAttachment.OnClientClick = "Javascript:viewPopUp('FileUpload.aspx?MOCNumber=" & currentMOC.MOCNumber & "'," & confirmMessage & ",'fu');return false"

        If currentMOC.MTTTaskHeaderSeqId = "" Then
            Me._btnMOCActionItems.OnClientClick = "Javascript:CreateTaskHeader('" & currentMOC.MOCNumber & "','OTHER','MOC','" & currentMOC.UserName & "','" & currentMOC.InsertDate & "');return false"
        Else
            Me._btnMOCActionItems.OnClientClick = "Javascript:OnMTTComplete('" & currentMOC.MTTTaskHeaderSeqId & "');return false"
        End If

        Me._tblUpdate.Visible = True
        Me.MOCStatus.Visible = True
        Me._tcStatus.Visible = True
        Me._lbCompDate.Visible = True
        Me._tbCompDate.Visible = True
    End Sub

    Private Sub GetMOC()
        Dim ipLoc As New IP.Bids.Localization.DataLocalization(Master.RIRESOURCES)
        RI.SharedFunctions.InsertRILoggingRecord("EnterMOC.aspx.vb", "GetMOC START")



        If currentMOC IsNot Nothing Then

            Me._MOCCat.RefreshDisplay()
            With currentMOC
                Me._MOCType.Types = .MOCType
                If .MOCType = "Trial/Temporary" Then
                    Me._divExpirationDate.Attributes.Add("Style", "display: block;")
                End If

                Me._hfMOCType.Value = .MOCType
                Me._lblCreatedBy.Text = String.Format(Master.RIRESOURCES.GetResourceValue("CreatedBy", True, "Shared"), .InsertUsername)
                Me._lblCreatedDate.Text = String.Format(Master.RIRESOURCES.GetResourceValue("CreationDate", True, "Shared"), .InsertDate)
                Me._lblLastUpdateDate.Text = String.Format(Master.RIRESOURCES.GetResourceValue("LastUpdateDate", True, "Shared"), .LastUpdatedDate)
                Me._lblUpdatedBy.Text = String.Format(Master.RIRESOURCES.GetResourceValue("LastUpdatedBy", True, "Shared"), .LastUpdatedBy)

                'Location
                Me._cddlFacility.SelectedValue = .SiteID

                'MOC

                'JEB
                'Me._tbMOCProjectKickoffDate.StartDate = .KickOffDate
                If .KickOffDate = "" Then
                    'do nothing
                Else
                    Me._tbMOCProjectKickoffDate.SelectedDate = .KickOffDate
                End If


                If .StartDate = "" Then

                Else
                    Me._tbMOCImplementationDate.SelectedDate = .StartDate
                End If

                'Me._tbMOCImplementationDate.StartDate = .StartDate
                'Me._tbMOCImplementationDate.Text = .StartDate
                'Me._tbExpirationDate.Text = .EndDate

                Me._tbExpirationDate.StartDate = .EndDate
                Me.MOCStatus.Status = .Status
                Me.MOCStatus.Info = .StatusDesc

                Me._txtTitle.Text = .Title
                'Me._lblStatusText.Text = .Status
                Me._txtDescription.Text = .Description
                Me._txtWorkOrder.Text = .WorkOrder

                'Functional Location Tree
                'If .FunctionalLocation.Length > 0 Then
                '    Me._functionalLocationTree.Text = .FunctionalLocation
                '    If Me._functionalLocationTree.Text.Length > 0 Then
                '        Me._functionalLocationTree.SetEquipmentDescription(.FunctionalLocation)
                '    End If
                'End If

                If .Savings = "" Or .Savings Is Nothing Then
                    .Savings = 0
                End If
                Me._txtSavings.Text = Mid(String.Format("{0:c}", FormatCurrency(.Savings, 0)), 1)
                If .Costs = "" Or .Costs Is Nothing Then
                    .Costs = 0
                End If
                Me._tbCosts.Text = Mid(String.Format("{0:c}", FormatCurrency(.Costs, 0)), 1)
                Me._txtImpact.Text = .Impact
                'Me._txtTasksCompleted.Text = .AICompDate
                Me._ddlBusinessUnit.SelectedValue = .BusinessUnit
                Me._ddlLineBreak.SelectedValue = .Line
                Me._cddlBusArea.SelectedValue = .BusinessUnit
                Me._cddlLineBreak.SelectedValue = .Line

                Me._MOCCat.Category = .Category
                Me._MOCCat.SubCategory = .SubCategory
                Me._MOCCat.EquipSubCategory = .EquipSubCategory
                Me._MOCCat.MarketChannelSubCategory = .MarketChannelSubCategory
                Me._MOCClass.Classification = .Classification
                Me._ccdlOwner.SelectedValue = .Owner
                Me._tbCompDate.Text = .CompDate

                Me._ddlFunding.SelectedValue = .Funding

                Me._SuperintendentName.Text = .SuperintendentName
                Me._SuperintendentApprovaldate.Text = .SuperintendentDate
                Me._SuperintendentComments.Text = .SuperintendentComments




                'Loop thru system data list
                Dim intRowsCount, intRowsLoop As Integer
                Dim cbCurrent As CheckBoxList
                Dim lbCurrent, lblDaysAfter, lblStatus As Label
                Dim ddlEditFacility, ddlEditPerson, ddlEditPriority As DropDownList
                Dim tbEditDaysAfter, tbSystemTitle As TextBox
                Dim hdfTaskitem As HiddenField

                'Dim ddlEditSystemPerson As DropDownList = CType(e.Item.FindControl("_ddlSystemPerson"), DropDownList)

                'Get the number of rows in the datalist
                intRowsCount = _dlSystem.Items.Count() - 1

                Dim sb As New StringBuilder
                Dim list As String() = .System.Split(CChar(","))
                Dim FacilityList As String() = .SystemFacility.Split(CChar(","))
                Dim PersonList As String() = .SystemPerson.Split(CChar(","))
                Dim PriorityList As String() = .SystemPriority.Split(CChar(","))
                Dim DaysAfterList As String() = .SystemDaysAfter.Split(CChar(","))
                Dim DueDateList As String() = .SystemDueDate.Split(CChar(","))
                Dim TaskItemList As String() = .SystemTaskItem.Split(CChar(","))
                Dim SystemStatus As String() = .SystemStatus.Split(CChar(","))
                Dim SystemTitle As String() = .SystemTitle.Split(CChar(","))

                Dim LastSiteId As String = ""

                Dim dsFacility As System.Data.DataSet = Nothing
                Dim ResponsibleList As System.Data.DataSet = Nothing


                For i As Integer = 0 To list.Length - 1
                    If list.Length > 0 Then

                        For intRowsLoop = 0 To intRowsCount
                            'Get the current row's checkbox and label
                            lbCurrent = CType(_dlSystem.Items(intRowsLoop).FindControl("_lblSystemSeq"), Label)
                            cbCurrent = CType(_dlSystem.Items(intRowsLoop).FindControl("_cblSystem"), CheckBoxList)
                            Dim tblDetail As Table = CType(_dlSystem.Items(intRowsLoop).FindControl("_tblSystemDetail"), Table)

                            If cbCurrent.SelectedValue <> "Y" Then
                                If list(i) = lbCurrent.Text Then
                                    ddlEditFacility = CType(_dlSystem.Items(intRowsLoop).FindControl("_ddlSystemFacility"), DropDownList)
                                    ddlEditPerson = CType(_dlSystem.Items(intRowsLoop).FindControl("_ddlSystemPerson"), DropDownList)
                                    ddlEditPriority = CType(_dlSystem.Items(intRowsLoop).FindControl("_ddlPriority"), DropDownList)
                                    lblDaysAfter = CType(_dlSystem.Items(intRowsLoop).FindControl("_lblDaysAfter"), Label)
                                    lblStatus = CType(_dlSystem.Items(intRowsLoop).FindControl("_lblStatus"), Label)
                                    tbEditDaysAfter = CType(_dlSystem.Items(intRowsLoop).FindControl("_txtDaysAfter"), TextBox)
                                    hdfTaskitem = CType(_dlSystem.Items(intRowsLoop).FindControl("_hdfTaskItem"), HiddenField)
                                    tbSystemTitle = CType(_dlSystem.Items(intRowsLoop).FindControl("_tbSysTitle"), TextBox)
                                    cbCurrent.SelectedValue = "Y"

                                    'Dim paramCollection As New OracleParameterCollection
                                    'Dim param As New OracleParameter
                                    Dim ds As System.Data.DataSet = Nothing


                                    'Get Facility List And set to current Facility
                                    'should only make this call onece
                                    'param = New OracleParameter
                                    'param.ParameterName = "rsFacility"
                                    'param.OracleDbType = OracleDbType.Cursor
                                    'param.Direction = Data.ParameterDirection.Output
                                    'paramCollection.Add(param)

                                    'Dim key As String = "MOC.FacilityList"
                                    'dsFacility = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.RINEWINCIDENT.FacilityList", key, 24)

                                    'added JEB 11/15/2021
                                    If dsFacility IsNot Nothing Then
                                        ' do nothing have already have data
                                    Else
                                        dsFacility = currentMOC.GetFacilityList()
                                    End If


                                    ddlEditFacility.DataSource = dsFacility.Tables(0)
                                    ddlEditFacility.DataTextField = dsFacility.Tables(0).Columns("SiteName").ColumnName.ToString
                                    ddlEditFacility.DataValueField = dsFacility.Tables(0).Columns("SiteID").ColumnName.ToString
                                    ddlEditFacility.DataBind()
                                    ddlEditFacility.Items.Insert(0, "")

                                    ddlEditFacility.SelectedValue = FacilityList(i)

                                    Dim dr As Data.DataTableReader
                                    Dim values As New Generic.List(Of String)
                                    Dim roleDescription As String = String.Empty
                                    Dim ddlList As New Collections.Generic.List(Of ListItem)
                                    Dim currentUserMode As Integer = 0

                                    'param = New OracleParameter
                                    'paramCollection = New OracleParameterCollection

                                    'param = New OracleParameter
                                    'param.ParameterName = "in_siteid"
                                    'param.OracleDbType = OracleDbType.VarChar
                                    'param.Value = FacilityList(i)
                                    'param.Direction = Data.ParameterDirection.Input
                                    'paramCollection.Add(param)

                                    'param = New OracleParameter
                                    'param.ParameterName = "rsResponsibleList"
                                    'param.OracleDbType = OracleDbType.Cursor
                                    'param.Direction = Data.ParameterDirection.Output
                                    'paramCollection.Add(param)

                                    If LastSiteId = "" Then
                                        LastSiteId = FacilityList(i)
                                        ResponsibleList = currentMOC.GetResponsibleList(FacilityList(i))
                                        'ResponsibleList = RI.SharedFunctions.GetDSFromPackage(paramCollection, "RIMOC.GetResponsibleList", "MTTResponsible" & FacilityList(i), 0)

                                    ElseIf LastSiteId = FacilityList(i) Then
                                        ' do nothing
                                    Else  'set to another site
                                        LastSiteId = FacilityList(i)
                                        ResponsibleList = currentMOC.GetResponsibleList(FacilityList(i))
                                        'ResponsibleList = RI.SharedFunctions.GetDSFromPackage(paramCollection, "RIMOC.GetResponsibleList", "MTTResponsible" & FacilityList(i), 0)

                                    End If


                                    'ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "RIMOC.GetResponsibleList", "MTTResponsible" & FacilityList(i), 0)
                                    ' Dim dr As Data.DataTableReader = ds.CreateDataReader

                                    RI.SharedFunctions.InsertRILoggingRecord("EnterMOC.aspx.vb", "GetMOC ResponsibleList ")

                                    dr = ResponsibleList.CreateDataReader

                                    'Build the dropdownlist values
                                    If dr IsNot Nothing Then
                                        Do While dr.Read
                                            Dim spaceChar As String = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                            With ddlEditPerson
                                                If dr.Item("RoleDescription") <> roleDescription Then 'New Group
                                                    'No Roleseqid indicates individual
                                                    Dim roleItem As New ListItem
                                                    roleDescription = RI.SharedFunctions.LocalizeValue(dr.Item("RoleDescription"))
                                                    roleItem.Text = roleDescription.ToUpper 'dr.Item("RoleDescription").ToUpper
                                                    If dr.Item("RoleSeqID") IsNot DBNull.Value Then
                                                        roleItem.Value = dr.Item("RoleSeqID")
                                                    End If

                                                    If ddlEditPerson.Items.Count > 0 Then
                                                        Dim blankItem As New ListItem
                                                        With blankItem
                                                            .Attributes.Add("disabled", "true")
                                                            .Text = ""
                                                            .Value = -1
                                                        End With
                                                        ddlEditPerson.Items.Add(blankItem)
                                                    End If

                                                    If roleDescription.Length > 0 Then
                                                        roleItem.Attributes.Add("style", "background-color:ActiveBorder; color:black; font-size:Larger;")
                                                        ddlEditPerson.Items.Add(roleItem)
                                                    Else
                                                        roleItem.Attributes.Add("style", "background-color:ActiveBorder; color:black;")
                                                        roleItem.Attributes.Add("disabled", "true")
                                                        ddlEditPerson.Items.Add(roleItem)
                                                    End If

                                                End If

                                                Dim useritem As New ListItem
                                                With useritem
                                                    .Text = Server.HtmlDecode(spaceChar & dr.Item("Name"))
                                                    .Value = dr.Item("UserName")
                                                End With

                                                ddlEditPerson.Items.Add(useritem)

                                            End With

                                        Loop
                                    End If

                                    ddlEditPriority.SelectedValue = PriorityList(i).ToString
                                    ddlEditPerson.SelectedValue = PersonList(i).ToString
                                    If DueDateList(i) <> "" Then
                                        tbEditDaysAfter.Text = DueDateList(i)

                                        tbEditDaysAfter.Width = 70
                                        lblDaysAfter.Text = "Due Date"
                                        lblStatus.Text = SystemStatus(i)
                                        tbSystemTitle.Text = SystemTitle(i)
                                    Else
                                        tbEditDaysAfter.Text = DaysAfterList(i)
                                        tbSystemTitle.Text = SystemTitle(i)
                                        'lblDaysAfter.Text = "Days After Implementation"
                                    End If

                                    hdfTaskitem.Value = TaskItemList(i)
                                    tblDetail.Style.Item("display") = "block"

                                    If hdfTaskitem.Value <> "" Then
                                        tblDetail.Enabled = False
                                        tbSystemTitle.Enabled = False
                                    End If
                                Else
                                    cbCurrent.SelectedValue = "N"
                                    tblDetail.Style.Item("display") = "none"

                                End If
                            End If
                        Next
                    End If
                Next

            End With



            If Request.QueryString("MOCNumber") IsNot Nothing Then


                Dim RS As DataSet
                RS = currentMOC.GetCurrentApproverListGPI()
                RadGridAssignedApprover.DataSource = RS
                RadGridAssignedApprover.DataBind()

                'get approvers/reviewers
                'Dim RS1 As DataSet
                'RS1 = currentMOC.GetCurrentBUMApproverGPI()


                Dim currentUserRS As DataSet
                currentUserRS = currentMOC.GetCurrentApproverGPI()
                Me._gvApprovals.DataSource = currentUserRS
                Me._gvApprovals.DataBind()


                If (_gvApprovals.Rows.Count = 0 And RadGridAssignedApprover.Items.Count = 0) Then

                    noReviewersAssigned.Visible = True

                Else

                    noReviewersAssigned.Visible = False

                End If

                If (_gvApprovals.Rows.Count = 0) Then
                    PanelCurrentApprover.Visible = False
                Else
                    PanelCurrentApprover.Visible = True

                End If



                Me._gvComments.DataSource = currentMOC.MOCCommentsDT
                Me._gvComments.DataBind()

                If currentMOC.MOCClassQuestionsDT.HasRows = "True" Then
                    Me._gvClassQuestions.DataSource = currentMOC.MOCClassQuestionsDT
                    Me._gvClassQuestions.DataBind()
                Else
                    'Me._lblClassQuestions.Attributes.Add("Style", "display: none;")
                End If

                If currentMOC.MOCCatQuestionsDT.HasRows = "True" Then
                    Me._gvCatQuestions.DataSource = currentMOC.MOCCatQuestionsDT
                    Me._gvCatQuestions.DataBind()
                Else
                    Me._lblCatQuestions.Attributes.Add("Style", "display: none;")
                End If


                If currentMOC.MOCPendingTempTasksDT Is Nothing Then
                Else
                    _tblTempTaskTitle.Visible = "True"
                    _tblTempTasks.Visible = "True"
                    Me._gvPendingTemplateTasks.DataSource = currentMOC.MOCPendingTempTasksDT
                    Me._gvPendingTemplateTasks.DataBind()
                End If
            End If


            'TODO - give this some more thought 8/21/2022
            'If currentMOC.Status = "No Approvers" Then
            '    'current approver
            '    _pnlApprovals.Visible = False
            '    RadGridCurrentApprover.Visible = False
            '    TableCurrentApprover.Visible = False

            '    TableApprovers.Visible = False
            '    RadGridAssignedApprover.Visible = False

            'End If

        End If


        ''get current user data
        'Dim currentUserRS As DataSet
        'currentUserRS = currentMOC.GetCurrentApproverGPI()

        'RadGridCurrentApprover.DataSource = currentUserRS
        'RadGridCurrentApprover.DataBind()



        RI.SharedFunctions.InsertRILoggingRecord("EnterMOC.aspx.vb", "GetMOC END")
    End Sub


    Private Sub GetMOCSuperintendent(in_MOCNumber As Integer, in_SuperintendentType As String)
        'Exit Sub

        RI.SharedFunctions.InsertRILoggingRecord(
                "EnterMOC.aspx.vb", "GetMOCSuperintendent ")

        Dim dr As Data.DataTableReader = Nothing
        Dim dt As New DataTable
        Dim ds As DataSet = clsCurrentMOC.GetCurrentMOCSuperintendentApproverGPI(in_MOCNumber, in_SuperintendentType)


        '_gvSuperintendentApprover.DataSource = ds
        '_gvSuperintendentApprover.DataBind()

        dr = ds.Tables(0).CreateDataReader

        ''store this for email purposes.
        If dr IsNot Nothing Then
            Do While dr.Read
                If dr.Item("username").ToString <> "" Then
                    currentMOC.SuperintendentName = dr.Item("fullname").ToString
                    currentMOC.SuperintendentDate = dr.Item("approvaldate").ToString
                    currentMOC.SuperintendentComments = dr.Item("comments").ToString

                End If

            Loop


        End If

        RI.SharedFunctions.InsertRILoggingRecord(
"EnterMOC.aspx.vb", "GetMOCSuperintendent END")

    End Sub




    Protected Sub SystemChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        'Session("SystemChanged") = "Y"
    End Sub

    Private Sub SetupFunctionalLocation()
        'With _functionalLocationTree
        '    If selectedBusArea.Length > 0 Then
        '        Dim splitBusArea As String()
        '        splitBusArea = Split(selectedBusArea, ",")
        '        If splitBusArea.Length = 2 Then
        '            .BusinessUnit = splitBusArea(0)
        '            .Area = splitBusArea(1)
        '        ElseIf splitBusArea.Length = 1 Then
        '            .BusinessUnit = splitBusArea(0)
        '            .Area = String.Empty
        '        End If
        '    End If
        '    If selectedLine.Length > 0 Then
        '        Dim splitLine As String()
        '        splitLine = Split(selectedLine, ",")
        '        If splitLine.Length >= 1 Then
        '            .Line = splitLine(0)
        '        End If
        '    End If
        '    .Facility = selectedFacility
        '    .SetContextKey()
        'End With
    End Sub

    Private Function GetGlobalJSVarMOC() As String
        Dim sb As New StringBuilder

        sb.Append("var facilityClient = $get('")
        sb.Append(Me._ddlFacility.ClientID)
        sb.Append("');")

        sb.AppendLine()
        sb.Append("var facility= $get('")
        sb.Append(Me._cddlFacility.ClientID)
        sb.Append("');")
        sb.AppendLine()
        sb.Append("var businessUnit= $get('")
        sb.Append(Me._cddlLineBreak.ClientID)
        sb.Append("');")
        sb.AppendLine()
        sb.Append("var businessUnitArea= $get('")
        sb.Append(Me._cddlBusArea.ClientID)
        sb.Append("');")
        sb.AppendLine()
        'sb.Append("var txtAutoComplete =$get('")
        'sb.Append(_functionalLocationTree.ClientID)
        'sb.Append("__txtFunctionalLocationSearch');")
        'sb.AppendLine()
        'sb.Append("var txtAutoComplete2 =$get('")
        'sb.Append(_functionalLocationTree.ClientID)
        'sb.Append("__txtFunctionalLocationSearch2');")

        sb.Append("var busArea = $get('")
        sb.Append(_ddlBusinessUnit.ClientID)
        sb.Append("');")
        sb.AppendLine()
        sb.Append("var lineBreak = $get('")
        sb.Append(_ddlLineBreak.ClientID)
        sb.Append("');")
        sb.AppendLine()

        sb.Append("var description = $get('")
        sb.Append(Me._txtDescription.ClientID)
        sb.Append("');")
        sb.AppendLine()
        sb.Append("var startDate = $get('")
        sb.Append(Me._lblCreatedDate.ClientID)
        sb.Append("');")
        sb.AppendLine()

        sb.Append("var endDate= $get('")
        sb.Append(Me._lblCreatedDate.ClientID)
        sb.Append("');")
        sb.AppendLine()

        sb.Append("var divisionClient = $get('")
        sb.Append(Me._tbDivision.ClientID)
        sb.Append("');")

        sb.AppendLine()
        sb.Append("var titleClient = $get('")
        sb.Append(Me._txtTitle.ClientID)
        sb.Append("');")

        Return sb.ToString
    End Function

    Protected Sub _dlSystem_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles _dlSystem.ItemDataBound
        'If e.Item.ItemType = ListItemType.Item Then


        Dim ddlEditFacility As DropDownList = CType(e.Item.FindControl("_ddlSystemFacility"), DropDownList)
        Dim ddlEditSystemPerson As DropDownList = CType(e.Item.FindControl("_ddlSystemPerson"), DropDownList)
        Dim cblYesNo As CheckBoxList = CType(e.Item.FindControl("_cblSystem"), CheckBoxList)
        Dim hdfTaskItem As HiddenField = CType(e.Item.FindControl("_hdfTaskItem"), HiddenField)
        Dim tblDetail As Table = CType(e.Item.FindControl("_tblSystemDetail"), Table)
        Dim txtDaysAfter As TextBox = CType(e.Item.FindControl("_txtDaysAfter"), TextBox)
        Dim CustomValidator As CustomValidator = CType(e.Item.FindControl("_systemVal"), CustomValidator)
        Dim CustomValidator1 As CustomValidator = CType(e.Item.FindControl("CustomValidator1"), CustomValidator)

        ScriptManager.RegisterExpandoAttribute(sender, CustomValidator.ClientID, "chkId", cblYesNo.ClientID, False)
        ScriptManager.RegisterExpandoAttribute(sender, CustomValidator.ClientID, "txtDaysAfter", txtDaysAfter.ClientID, False)
        ScriptManager.RegisterExpandoAttribute(sender, CustomValidator.ClientID, "ddlPerson", ddlEditSystemPerson.ClientID, False)
        ScriptManager.RegisterExpandoAttribute(sender, CustomValidator1.ClientID, "chkId", cblYesNo.ClientID, False)
        ScriptManager.RegisterExpandoAttribute(sender, CustomValidator1.ClientID, "txtDaysAfter", txtDaysAfter.ClientID, False)
        ScriptManager.RegisterExpandoAttribute(sender, CustomValidator1.ClientID, "ddlPerson", ddlEditSystemPerson.ClientID, False)

        tblDetail.Style.Item("display") = "none"

        ddlEditFacility.Attributes.Add("onchange", "this.blur;GetEmployeeWRoles('" & ddlEditFacility.ClientID & "','" & ddlEditSystemPerson.ClientID & "');")

        ' Need to change so that we check the yes no and only call one javascript function
        Dim i As Integer
        For i = 0 To cblYesNo.Items.Count - 1
            If cblYesNo.Items(i).Value = "Y" Then
                'cblYesNo.Items(i).Attributes.Add("onclick", "ShowSystemDetail('" & ddlEditSystemPerson.ClientID & "','" & tblDetail.ClientID & "','" & ddlEditFacility.ClientID & "','" & _ddlFacility.ClientID & "','" & userProfile.DefaultFacility & "');")
                cblYesNo.Items(i).Attributes.Add("onclick", "ShowSystemDetail('" & ddlEditSystemPerson.ClientID & "','" & tblDetail.ClientID & "','" & ddlEditFacility.ClientID & "','" & _ddlFacility.ClientID & "','" & MyGlobalDefaultFacility & "');")
            Else
                cblYesNo.Items(i).Attributes.Add("onclick", "HideSystemDetail('" & cblYesNo.ClientID & "','" & tblDetail.ClientID & "');")
            End If
        Next

        'AddHandler ddlEditSystemPerson.SelectedIndexChanged, AddressOf ddlEditSystemPerson_SelectedIndexChanged

        If hdfTaskItem.Value <> "" Then
            tblDetail.Enabled = "false"
        End If

    End Sub

    Private Sub _btnSubmit_Click(sender As Object, e As EventArgs) Handles _btnSubmit.Click
        RI.SharedFunctions.InsertRILoggingRecord("EnterMOC.aspx.vb", " _btnSubmit_Click START")
        If NewMOCFlag = "Y" Then
            SaveMOC(True)
        Else
            SaveMOC(True)
        End If
        RI.SharedFunctions.InsertRILoggingRecord("EnterMOC.aspx.vb", " _btnSubmit_Click END")

        'End If
    End Sub


    'Protected Sub SaveMOC(Optional ByVal refreshPage As Boolean = True, Optional ByVal SavedMOCStatus As String = "")
    '    RI.SharedFunctions.Trace("EnterMOC.aspx.vb", " START - SaveMOC ")
    '    Dim returnStatus2 As String = ""
    '    Try
    '        Me.Validate("EnterMOC")

    '        If Page.IsValid Then
    '            If currentMOC Is Nothing Then
    '                currentMOC = New clsCurrentMOC()
    '            End If

    '            Dim sendEmailFlag As Boolean = False


    '            With currentMOC
    '                RI.SharedFunctions.Trace("EnterMOC.aspx.vb", " START - SaveMOC " & currentMOC.MOCNumber)
    '                .SiteID = Me._ddlFacility.SelectedValue
    '                .BusinessUnit = Me._ddlBusinessUnit.SelectedValue()
    '                .Line = Me._ddlLineBreak.SelectedValue
    '                .FunctionalLocation = ""  'RI.SharedFunctions.DataClean(Me._functionalLocationTree.Text)
    '                .Owner = Me._ddlOwner.SelectedValue
    '                .MOCType = Me._MOCType.Types
    '                .EndDate = Me._tbExpirationDate.StartDate
    '                .KickOffDate = "" 'Me._tbMOCProjectKickoffDate.SelectedDate
    '                .Title = RI.SharedFunctions.DataClean(Me._txtTitle.Text)
    '                .Description = RI.SharedFunctions.DataClean(Me._txtDescription.Text)
    '                .Impact = Me._txtImpact.Text
    '                .Funding = _ddlFunding.SelectedValue
    '                .Classification = Me._MOCClass.Classification
    '                .Category = RI.SharedFunctions.DataClean(Me._MOCCat.Category)
    '                .SubCategory = RI.SharedFunctions.DataClean(Me._MOCCat.SubCategory)
    '                .EquipSubCategory = RI.SharedFunctions.DataClean(Me._MOCCat.EquipSubCategory)
    '                .MarketChannelSubCategory = RI.SharedFunctions.DataClean(Me._MOCCat.MarketChannelSubCategory)
    '                .WorkOrder = Me._txtWorkOrder.Text
    '                .UserName = MyGlobalUsername ' userProfile.Username

    '                If Me._tbMOCImplementationDate.SelectedDate.ToString = "" Then
    '                    'do nothing
    '                Else
    '                    .StartDate = Me._tbMOCImplementationDate.SelectedDate
    '                End If
    '                If Me._txtSavings.Text = "" Then
    '                    .Savings = 0
    '                Else
    '                    .Savings = CLng(RI.SharedFunctions.DataClean(Me._txtSavings.Text, CStr(0)))
    '                End If
    '                If Me._tbCosts.Text = "" Then
    '                    .Costs = 0
    '                Else
    '                    .Costs = CLng(RI.SharedFunctions.DataClean(Me._tbCosts.Text, CStr(0)))
    '                End If


    '                'Approval/Informed save
    '                'Only Applicable for existing MOC
    '                Dim approvalEmailList As String = ""
    '                Dim ccEmailList As String = ""
    '                Dim approvalEmailMsg As String = ""
    '                Dim approvalEmailSubMsg As String = ""

    '                If NewMOCFlag = "N" Then
    '                    Dim returnStatus As String = ""

    '                    '1/10/2014 ALA - Changed to send email to all level approvers when MOC is approved at each
    '                    ' level.  If all levels have approved, email should go to everyone in the approver list included those informed.

    '                    '3/30/2017 ALA - If Only Informed MOC then email informed that MOC was created.


    '                    If Me._gvClassQuestions IsNot Nothing Then

    '                        Dim i As Integer = 0
    '                        Dim intRow As Integer

    '                        For i = 0 To Me._gvClassQuestions.DirtyRows.Count - 1
    '                            intRow = _gvClassQuestions.DirtyRows.Item(i).DataItemIndex
    '                            Dim strQuestionSeqid As String
    '                            strQuestionSeqid = Me._gvClassQuestions.DataKeys.Item(intRow).Value
    '                            Dim strAnswer As String
    '                            If Me._gvClassQuestions.Rows(intRow).FindControl("_cbAnswer").Visible = "true" Then
    '                                strAnswer = CType(Me._gvClassQuestions.Rows(intRow).FindControl("_cbAnswer"), CheckBoxList).SelectedValue
    '                            Else
    '                                strAnswer = CType(Me._gvClassQuestions.Rows(intRow).FindControl("_tbAnswer"), TextBox).Text
    '                            End If
    '                            currentMOC.SaveMOCClassQuestionBySeqId(strQuestionSeqid, currentMOC.UserName, "T", strAnswer)
    '                        Next

    '                    End If


    '                    If Me._gvCatQuestions IsNot Nothing Then

    '                        Dim i As Integer = 0
    '                        Dim intRow As Integer

    '                        For i = 0 To Me._gvCatQuestions.DirtyRows.Count - 1
    '                            intRow = _gvCatQuestions.DirtyRows.Item(i).DataItemIndex
    '                            Dim strQuestionSeqid As String
    '                            strQuestionSeqid = Me._gvCatQuestions.DataKeys.Item(intRow).Value
    '                            Dim strAnswer As String
    '                            If Me._gvCatQuestions.Rows(intRow).FindControl("_cbAnswer").Visible = "true" Then
    '                                strAnswer = CType(Me._gvCatQuestions.Rows(intRow).FindControl("_cbAnswer"), CheckBoxList).SelectedValue
    '                            Else
    '                                strAnswer = CType(Me._gvCatQuestions.Rows(intRow).FindControl("_tbAnswer"), TextBox).Text
    '                            End If
    '                            currentMOC.SaveMOCCatQuestionBySeqId(strQuestionSeqid, currentMOC.UserName, "T", strAnswer)
    '                        Next

    '                    End If

    '                    .MOCComment = Me.__txtCommentsNew.Text

    '                End If


    '                'Check status to see what emails (if any) need to be sent
    '                If SavedMOCStatus = "INITIATE" Then
    '                    .Status = "INITIATE"
    '                    EmailMOC("NewMOC")
    '                ElseIf SavedMOCStatus = "IMPLEMENT" Then
    '                    .Status = "Implemented"
    '                    'currentMOC.CreateMOCTemplateTasks()

    '                ElseIf SavedMOCStatus = "MoveToPerm" Then
    '                    approvalEmailList = currentMOC.NotificationEList & "," & currentMOC.NotificationL1List & "," & currentMOC.NotificationL2List & "," & currentMOC.NotificationL3List
    '                    approvalEmailMsg = RI.SharedFunctions.LocalizeValue("Trial/Temporary MOC Moved To Permanent") & " - " & currentMOC.BusinessUnit & ": " & currentMOC.Title
    '                    EmailMOC("Approved", approvalEmailList, currentMOC.MOCCoordinatorEmail & "," & currentMOC.OwnerEmail, approvalEmailMsg)
    '                Else
    '                    If MOCStatus.Status Is Nothing Then
    '                        .Status = "Draft"
    '                    Else
    '                        .Status = MOCStatus.Status
    '                    End If
    '                End If






    '                returnStatus2 = .SaveMOC()
    '                '.MOCNumber = .SaveMOC()
    '                If returnStatus2 = "777" Then
    '                    EmailMOC("NewMOC", currentMOC.NotificationL1List & "," & currentMOC.NotificationL2List, ccEmailList, "MOC Moved from Draft/On-Hold to Approval Requested")
    '                End If


    '                'check to make sure that the MOC has just been approved by
    '                'superentient and this is the first save after that

    '                If currentMOC.NotificationL1FullName = "" And currentMOC.Status = "JEB" Then
    '                    'If currentMOC.NotificationL1FullName = "" And currentMOC.Status = "No Approvers" Then
    '                    GetDefaultReviewers(currentMOC.MOCNumber)
    '                    'go mail to the default reviewers
    '                    'EmailMOC("NewMOC")
    '                End If



    '                sendEmailFlag = False
    '                If Request.QueryString("MOCNumber") IsNot Nothing Then
    '                    .MOCNumber = Request.QueryString("MOCNumber")
    '                Else
    '                    sendEmailFlag = True
    '                End If


    '                'Loop thru system data list
    '                Dim intRowsCount, intRowsLoop As Integer
    '                Dim cbCurrent As CheckBoxList
    '                Dim ddlFacility, ddlUsername, ddlPriority As DropDownList
    '                Dim tbDaysAfter As TextBox
    '                Dim lbCurrent As Label
    '                Dim hdfTaskItem As HiddenField
    '                Dim tbTaskTitle As TextBox

    '                Dim strSystem, strUsername, strFacility As String
    '                Dim strRole As String = ""
    '                'Get the number of rows in the datalist
    '                intRowsCount = _dlSystem.Items.Count() - 1

    '                Dim SystemRecords As Boolean
    '                Dim sb As New StringBuilder
    '                For intRowsLoop = 0 To intRowsCount
    '                    'Get the current row's checkbox and label
    '                    lbCurrent = CType(_dlSystem.Items(intRowsLoop).FindControl("_lblSystemSeq"), Label)
    '                    cbCurrent = CType(_dlSystem.Items(intRowsLoop).FindControl("_cblSystem"), CheckBoxList)
    '                    strSystem = cbCurrent.SelectedValue
    '                    hdfTaskItem = CType(_dlSystem.Items(intRowsLoop).FindControl("_hdfTaskItem"), HiddenField)

    '                    If strSystem = "Y" And hdfTaskItem.Value = "" Then
    '                        ddlFacility = CType(_dlSystem.Items(intRowsLoop).FindControl("_ddlSystemFacility"), DropDownList)
    '                        ddlUsername = CType(_dlSystem.Items(intRowsLoop).FindControl("_ddlSystemPerson"), DropDownList)
    '                        strUsername = Request.Form(ddlUsername.UniqueID)
    '                        strFacility = Request.Form(ddlFacility.UniqueID)

    '                        If IsNumeric(Replace(strUsername, "/", "")) Then
    '                            strRole = Mid(strUsername, InStr(strUsername, "/") + 1)
    '                            strUsername = ""
    '                        End If
    '                        ddlPriority = CType(_dlSystem.Items(intRowsLoop).FindControl("_ddlPriority"), DropDownList)
    '                        tbDaysAfter = CType(_dlSystem.Items(intRowsLoop).FindControl("_txtDaysAfter"), TextBox)
    '                        tbTaskTitle = CType(_dlSystem.Items(intRowsLoop).FindControl("_tbSysTitle"), TextBox)

    '                        'Dim returnStatus2 As String = ""
    '                        returnStatus2 = currentMOC.SaveMOCSystem(.MOCNumber, lbCurrent.Text, strRole, strFacility, strUsername, ddlPriority.Text, tbDaysAfter.Text, tbTaskTitle.Text)
    '                        SystemRecords = True

    '                    ElseIf (strSystem = "N") Then
    '                        If Session("SystemChanged") = "Y" Then
    '                            'Dim returnStatus2 As String = ""
    '                            returnStatus2 = currentMOC.DeleteMOCSystem(.MOCNumber, lbCurrent.Text)
    '                        End If
    '                    End If

    '                Next
    '                Session("SystemChanged") = "N"

    '                If currentMOC.Status = "Approved" Then
    '                    'currentMOC.CreateMOCTemplateTasks()
    '                    If currentMOC.USDTicket <> "Y" Then
    '                        Dim strSummary = currentMOC.SiteID & " - MOC " & currentMOC.MOCNumber & " " & Me._txtTitle.Text 'currentMOC.Title
    '                        Dim strDesc = currentMOC.SiteID & " - MOC " & currentMOC.MOCNumber & " initiated by " & currentMOC.InsertUsername & " http://ri/RI/MOC/entermoc.aspx?mocnumber=" & currentMOC.MOCNumber & " " & Me._txtTitle.Text
    '                        If RI.SharedFunctions.DataClean(currentMOC.SubCategory).Length > 0 Then

    '                            If currentMOC.SubCategory.Contains("PI") = True Or currentMOC.SubCategory.Contains("Proficy") Or currentMOC.SubCategory.Contains("PARCView") Then
    '                                Me.CreateUSDTicket(currentMOC.SiteID, strSummary, strDesc, currentMOC.MOCCoordinator)
    '                            End If
    '                        End If

    '                    End If
    '                End If

    '                If sendEmailFlag = True Then
    '                    'ToDo: MJP 6/7/2010 - Add logic to display the Notification Dialog and remove logic to send email here
    '                    'ToDo: JEB 
    '                    'EmailMOC(True)
    '                    PopulateNotificationList(.MOCNumber)
    '                    _btnCancel.Visible = False

    '                    ' currentMOC.GetMOCTemplateTasks()
    '                    ' Me._gvTemplateTasks.DataSource = currentMOC.MOCTemplateTasksDaysAfterDT
    '                    ' Me._gvTemplateTasks.DataBind()

    '                    _mpeSwapList.Show()
    '                Else
    '                    currentMOC.CheckTemplateTasks()
    '                    If .MOCNumber.Length > 0 And refreshPage = True Then 'And .MOCNumber <> Request.QueryString("MOCNumber") Then
    '                        Response.Clear()
    '                        Response.Redirect(Page.AppRelativeVirtualPath & "?MOCNumber=" & .MOCNumber, False)
    '                    Else
    '                        currentMOC = New clsCurrentMOC(.MOCNumber)
    '                    End If

    '                End If
    '            End With

    '        End If
    '        RI.SharedFunctions.Trace("EnterMOC.aspx.vb", " END - SaveMOC " & currentMOC.MOCNumber)

    '    Catch ex As Exception
    '        Throw New Exception("Error Saving Current MOC " & currentMOC.MOCNumber)
    '    End Try
    'End Sub





    Protected Sub _messageBox_OKClick() Handles _messageBox.OKClick
        If Request.QueryString("MOCNumber") IsNot Nothing Then
            SaveMOC(True, "IMPLEMENT")
        End If
    End Sub


    Private Sub GetDefaultReviewers(ByVal mocNumber As Integer)

        RI.SharedFunctions.InsertRILoggingRecord("EnterMOC.aspx.vb", " START GetDefaultReviewers ")


        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        Dim sqlTO As String = String.Empty
        Dim sqlE As String = String.Empty

        paramCollection.Clear()

        'Get the initial list of approvers based on tblmocnotification table.  This should only show up when an MOC is created.
        'If an existing MOC, only show facility and person ddl.
        param = New OracleParameter
        param.ParameterName = "in_Siteid"
        param.OracleDbType = OracleDbType.VarChar
        param.Value = currentMOC.SiteID
        param.Direction = Data.ParameterDirection.Input
        paramCollection.Add(param)

        param = New OracleParameter
        param.ParameterName = "in_BusUnitArea"
        param.OracleDbType = OracleDbType.VarChar
        param.Value = Me._ddlBusinessUnit.SelectedValue
        param.Direction = Data.ParameterDirection.Input
        paramCollection.Add(param)

        param = New OracleParameter
        param.ParameterName = "in_Line"
        param.OracleDbType = OracleDbType.VarChar
        param.Value = Me._ddlLineBreak.SelectedValue
        param.Direction = Data.ParameterDirection.Input
        paramCollection.Add(param)

        param = New OracleParameter
        param.ParameterName = "in_Class"
        param.OracleDbType = OracleDbType.VarChar
        param.Value = Me._MOCClass.Classification
        param.Direction = Data.ParameterDirection.Input
        paramCollection.Add(param)

        param = New OracleParameter
        param.ParameterName = "in_Cat"
        param.OracleDbType = OracleDbType.VarChar
        param.Value = Me._MOCCat.Category
        param.Direction = Data.ParameterDirection.Input
        paramCollection.Add(param)

        param = New OracleParameter
        param.ParameterName = "in_SubCat"
        param.OracleDbType = OracleDbType.VarChar
        param.Value = Me._MOCCat.SubCategory & Me._MOCCat.EquipSubCategory
        param.Direction = Data.ParameterDirection.Input
        paramCollection.Add(param)

        param = New OracleParameter
        param.ParameterName = "rsInformedList"
        param.OracleDbType = OracleDbType.Cursor
        param.Direction = Data.ParameterDirection.Output
        paramCollection.Add(param)

        param = New OracleParameter
        param.ParameterName = "rsL1List"
        param.OracleDbType = OracleDbType.Cursor
        param.Direction = Data.ParameterDirection.Output
        paramCollection.Add(param)

        param = New OracleParameter
        param.ParameterName = "rsL2List"
        param.OracleDbType = OracleDbType.Cursor
        param.Direction = Data.ParameterDirection.Output
        paramCollection.Add(param)

        param = New OracleParameter
        param.ParameterName = "rsL3List"
        param.OracleDbType = OracleDbType.Cursor
        param.Direction = Data.ParameterDirection.Output
        paramCollection.Add(param)

        param = New OracleParameter
        param.ParameterName = "rsL4List"
        param.OracleDbType = OracleDbType.Cursor
        param.Direction = Data.ParameterDirection.Output
        paramCollection.Add(param)

        ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "NewMOC.GetDefaultApproverList", "GetDefaultApproverList_" & mocNumber, 0)

        Dim roleDescription As String = String.Empty
        Dim ddlList As New Collections.Generic.List(Of ListItem)

        If ds IsNot Nothing Then


            Dim dr As DataTableReader = ds.Tables(1).CreateDataReader

            'Dim dr1 As DataTableReader = ds.Tables(2).CreateDataReader
            'Dim dr2 As DataTableReader = ds.Tables(3).CreateDataReader
            'Dim dr3 As DataTableReader = ds.Tables(4).CreateDataReader




            If dr IsNot Nothing Then
                Do While dr.Read
                    dr = ds.Tables(1).CreateDataReader
                    If dr IsNot Nothing Then
                        Do While dr.Read

                            If dr.Item("username") IsNot DBNull.Value Then
                                currentMOC.InsertDefaultApprovers(mocNumber.ToString,
                                                                  dr.Item("username").ToString.Replace("*", ""),
                                                                  dr.Item("notifytype").ToString,
                                                                  MyGlobalUsername,
                                                                  "Y")

                            End If

                        Loop
                    End If
                Loop
            End If

        End If

        RI.SharedFunctions.InsertRILoggingRecord("EnterMOC.aspx.vb", " END GetDefaultReviewers ")

    End Sub


    Private Sub PopulateNotificationList(ByVal mocNumber As Integer)

        RI.SharedFunctions.InsertRILoggingRecord(
     "EnterMOC.aspx.vb", "PopulateNotificationList ")


        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim ds2 As System.Data.DataSet = Nothing
        Dim ds3 As System.Data.DataSet = Nothing
        Dim ds4 As System.Data.DataSet = Nothing
        Dim sqlTO As String = String.Empty
        Dim sqlE As String = String.Empty

        'Get Facility List and set to current Facility
        param = New OracleParameter
        param.ParameterName = "rsFacility"
        param.OracleDbType = OracleDbType.Cursor
        param.Direction = Data.ParameterDirection.Output
        paramCollection.Add(param)

        Dim key As String = "MOC.FacilityList"
        ds4 = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.RINEWINCIDENT.FacilityList", key, 24)

        'Drop down list for Approver pop up window
        With Me._ddlApproverFacilityNew
            .DataSource = ds4.Tables(0).CreateDataReader
            .DataTextField = "Sitename"
            .DataValueField = "siteid"
            .DataBind()
            .SelectedValue = currentMOC.SiteID
        End With
        Session.Add("MOCNumber", mocNumber)

        paramCollection.Clear()

        'Get Initial list of people based on site for the MOC
        param = New OracleParameter
        param.ParameterName = "in_siteid"
        param.OracleDbType = OracleDbType.VarChar
        param.Value = currentMOC.SiteID
        param.Direction = ParameterDirection.Input
        paramCollection.Add(param)

        param = New OracleParameter
        param.ParameterName = "in_mocnumber"
        param.OracleDbType = OracleDbType.VarChar
        param.Value = mocNumber
        param.Direction = ParameterDirection.Input
        paramCollection.Add(param)

        param = New OracleParameter
        param.ParameterName = "rsResponsibleList"
        param.OracleDbType = OracleDbType.Cursor
        param.Direction = ParameterDirection.Output
        paramCollection.Add(param)

        ds2 = RI.SharedFunctions.GetDSFromPackage(paramCollection, "NewMOC.GetMOCResponsibleList", "MOCPerson_" & currentMOC.SiteID, 0)
        'ds2 = RI.SharedFunctions.GetDSFromPackage(paramCollection, "NewMOC.GetResponsibleList", "MOCPerson_" & currentMOC.SiteID, 0)


        paramCollection.Clear()

        'Get the initial list of approvers based on tblmocnotification table.  This should only show up when an MOC is created.
        'If an existing MOC, only show facility and person ddl.
        param = New OracleParameter
        param.ParameterName = "in_Siteid"
        param.OracleDbType = OracleDbType.VarChar
        param.Value = currentMOC.SiteID
        param.Direction = Data.ParameterDirection.Input
        paramCollection.Add(param)

        param = New OracleParameter
        param.ParameterName = "in_BusUnitArea"
        param.OracleDbType = OracleDbType.VarChar
        param.Value = Me._ddlBusinessUnit.SelectedValue
        param.Direction = Data.ParameterDirection.Input
        paramCollection.Add(param)

        param = New OracleParameter
        param.ParameterName = "in_Line"
        param.OracleDbType = OracleDbType.VarChar
        param.Value = Me._ddlLineBreak.SelectedValue
        param.Direction = Data.ParameterDirection.Input
        paramCollection.Add(param)

        param = New OracleParameter
        param.ParameterName = "in_Class"
        param.OracleDbType = OracleDbType.VarChar
        param.Value = Me._MOCClass.Classification
        param.Direction = Data.ParameterDirection.Input
        paramCollection.Add(param)

        param = New OracleParameter
        param.ParameterName = "in_Cat"
        param.OracleDbType = OracleDbType.VarChar
        param.Value = Me._MOCCat.Category
        param.Direction = Data.ParameterDirection.Input
        paramCollection.Add(param)

        param = New OracleParameter
        param.ParameterName = "in_SubCat"
        param.OracleDbType = OracleDbType.VarChar
        param.Value = Me._MOCCat.SubCategory & Me._MOCCat.EquipSubCategory
        param.Direction = Data.ParameterDirection.Input
        paramCollection.Add(param)

        param = New OracleParameter
        param.ParameterName = "rsInformedList"
        param.OracleDbType = OracleDbType.Cursor
        param.Direction = Data.ParameterDirection.Output
        paramCollection.Add(param)

        param = New OracleParameter
        param.ParameterName = "rsL1List"
        param.OracleDbType = OracleDbType.Cursor
        param.Direction = Data.ParameterDirection.Output
        paramCollection.Add(param)

        param = New OracleParameter
        param.ParameterName = "rsL2List"
        param.OracleDbType = OracleDbType.Cursor
        param.Direction = Data.ParameterDirection.Output
        paramCollection.Add(param)

        param = New OracleParameter
        param.ParameterName = "rsL3List"
        param.OracleDbType = OracleDbType.Cursor
        param.Direction = Data.ParameterDirection.Output
        paramCollection.Add(param)

        param = New OracleParameter
        param.ParameterName = "rsL4List"
        param.OracleDbType = OracleDbType.Cursor
        param.Direction = Data.ParameterDirection.Output
        paramCollection.Add(param)

        ds3 = RI.SharedFunctions.GetDSFromPackage(paramCollection, "NewMOC.GetDefaultApproverList", "GetDefaultApproverList_" & mocNumber, 0)

        Dim roleDescription As String = String.Empty
        Dim ddlList As New Collections.Generic.List(Of ListItem)

        'GetApprovalList("PN", _slbApprovalNotificationList.AvailableListID)
        If ds2 IsNot Nothing Then
            With Me._slbApprovalNotificationList
                '.DataTextField = "Roledescription"
                '.DataValueField = "username"
                '.DataTextField = "Person"
                '.DataValueField = "username"
                Dim dr As DataTableReader = ds2.Tables(0).CreateDataReader
                If dr IsNot Nothing Then
                    Do While dr.Read
                        Dim spaceChar As String = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                        If dr.Item("roledescription") <> roleDescription Then 'New Group
                            'No Roleseqid indicates individual
                            Dim roleItem As New ListItem
                            roleDescription = dr.Item("roledescription")
                            roleItem.Text = dr.Item("roledescription").ToUpper
                            If dr.Item("RoleSeqID") IsNot DBNull.Value Then
                                roleItem.Value = dr.Item("roleplantcode") & "/" & dr.Item("RoleSeqid") '& "/" & dr.Item("UserName")
                            End If

                            If roleDescription.Length > 0 Then
                                roleItem.Attributes.Add("style", "background-color:ActiveBorder; color:white; font-size:Larger;")
                                .AvailableID.Items.Add(roleItem)
                            Else
                                roleItem.Attributes.Add("style", "background-color:ActiveBorder; color:white;")
                                roleItem.Attributes.Add("disabled", "true")
                                .AvailableID.Items.Add(roleItem)
                            End If
                        End If

                        Dim useritem As New ListItem
                        With useritem
                            .Text = Server.HtmlDecode(spaceChar & UCase(dr.Item("Name")))
                            If dr.Item("RoleSeqid").ToString <> "" Then
                                .Value = dr.Item("roleplantcode") & "/" & dr.Item("RoleSeqid")
                            Else
                                .Value = dr.Item("UserName")
                            End If
                        End With
                        .AvailableID.Items.Add(useritem)

                    Loop
                End If

                If Request.QueryString("mocnumber") Is Nothing Then
                    dr = ds3.Tables(1).CreateDataReader
                    If dr IsNot Nothing Then
                        Do While dr.Read
                            Dim spaceChar As String = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                            If dr.Item("roledescription") <> roleDescription Then 'New Group
                                'No Roleseqid indicates individual
                                Dim roleItem As New ListItem
                                roleDescription = dr.Item("roledescription")
                                roleItem.Text = dr.Item("roledescription").ToUpper
                                If dr.Item("RoleSeqID") IsNot DBNull.Value Then
                                    roleItem.Value = dr.Item("roleplantcode") & "/" & dr.Item("RoleSeqid") '& "/" & dr.Item("UserName")
                                End If

                                'If roleDescription = "All" Then
                                ' roleItem.Attributes.Add("style", "background-color:ActiveBorder; color:black; font-size:Larger; ")
                                ' roleItem.Attributes.Add("disabled", "true")
                                ' .ApproverL1ID.Items.Add("----------------------------------")
                                'Else
                                If roleDescription.Length > 0 Then
                                    roleItem.Attributes.Add("style", "background-color:ActiveBorder; color:white; font-size:Larger; ")
                                    .ApproverL1ID.Items.Add(roleItem)
                                Else
                                    roleItem.Attributes.Add("style", "background-color:ActiveBorder; color:white;")
                                    roleItem.Attributes.Add("disabled", "true")
                                    .ApproverL1ID.Items.Add(roleItem)
                                End If


                            End If

                            Dim useritem As New ListItem
                            With useritem
                                .Text = Server.HtmlDecode(spaceChar & UCase(dr.Item("FullName")))
                                If dr.Item("RoleSeqid").ToString <> "" Then
                                    .Value = dr.Item("roleplantcode") & "/" & dr.Item("RoleSeqid")
                                Else
                                    .Value = dr.Item("UserName")
                                End If
                            End With
                            .ApproverL1ID.Items.Add(useritem)

                        Loop
                    End If
                    roleDescription = ""

                    'Build Level 2 Default Approver Listbox
                    dr = ds3.Tables(2).CreateDataReader
                    If dr IsNot Nothing Then
                        Do While dr.Read
                            Dim spaceChar As String = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                            If dr.Item("roledescription") <> roleDescription Then 'New Group
                                'No Roleseqid indicates individual
                                Dim roleItem As New ListItem
                                roleDescription = dr.Item("roledescription")
                                roleItem.Text = dr.Item("roledescription").ToUpper
                                If dr.Item("RoleSeqID") IsNot DBNull.Value Then
                                    roleItem.Value = dr.Item("roleplantcode") & "/" & dr.Item("RoleSeqid") '& "/" & dr.Item("UserName")
                                End If

                                If roleDescription.Length > 0 Then
                                    roleItem.Attributes.Add("style", "background-color:ActiveBorder; color:white; font-size:Larger;")
                                    .ApproverL2ID.Items.Add(roleItem)
                                Else
                                    roleItem.Attributes.Add("style", "background-color:ActiveBorder; color:white;")
                                    roleItem.Attributes.Add("disabled", "true")
                                    .ApproverL2ID.Items.Add(roleItem)
                                End If
                            End If

                            Dim useritem As New ListItem
                            With useritem
                                .Text = Server.HtmlDecode(spaceChar & UCase(dr.Item("FullName")))
                                If dr.Item("RoleSeqid").ToString <> "" Then
                                    .Value = dr.Item("roleplantcode") & "/" & dr.Item("RoleSeqid")
                                Else
                                    .Value = dr.Item("UserName")
                                End If
                            End With
                            .ApproverL2ID.Items.Add(useritem)

                        Loop
                    End If
                    roleDescription = ""

                    'Build Level 3 Default Approver Listbox
                    dr = ds3.Tables(3).CreateDataReader
                    If dr IsNot Nothing Then
                        Do While dr.Read
                            Dim spaceChar As String = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                            If dr.Item("roledescription") <> roleDescription Then 'New Group
                                'No Roleseqid indicates individual
                                Dim roleItem As New ListItem
                                roleDescription = dr.Item("roledescription")
                                roleItem.Text = dr.Item("roledescription").ToUpper
                                If dr.Item("RoleSeqID") IsNot DBNull.Value Then
                                    roleItem.Value = dr.Item("roleplantcode") & "/" & dr.Item("RoleSeqid") '& "/" & dr.Item("UserName")
                                End If

                                If roleDescription.Length > 0 Then
                                    roleItem.Attributes.Add("style", "background-color:ActiveBorder; color:white; font-size:Larger;")
                                    .ApproverL3ID.Items.Add(roleItem)
                                Else
                                    roleItem.Attributes.Add("style", "background-color:ActiveBorder; color:white;")
                                    roleItem.Attributes.Add("disabled", "true")
                                    .ApproverL3ID.Items.Add(roleItem)
                                End If
                            End If

                            Dim useritem As New ListItem
                            With useritem
                                .Text = Server.HtmlDecode(spaceChar & UCase(dr.Item("FullName")))
                                If dr.Item("RoleSeqid").ToString <> "" Then
                                    .Value = dr.Item("roleplantcode") & "/" & dr.Item("RoleSeqid")
                                Else
                                    .Value = dr.Item("UserName")
                                End If
                            End With
                            .ApproverL3ID.Items.Add(useritem)

                        Loop
                    End If
                    roleDescription = ""

                    'Build Informed Default Approver Listbox
                    dr = ds3.Tables(0).CreateDataReader
                    If dr IsNot Nothing Then
                        Do While dr.Read
                            Dim spaceChar As String = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                            If dr.Item("roledescription") <> roleDescription Then 'New Group
                                'No Roleseqid indicates individual
                                Dim roleItem As New ListItem
                                roleDescription = dr.Item("roledescription")
                                roleItem.Text = dr.Item("roledescription").ToUpper
                                If dr.Item("RoleSeqID") IsNot DBNull.Value Then
                                    roleItem.Value = dr.Item("roleplantcode") & "/" & dr.Item("RoleSeqid") '& "/" & dr.Item("UserName")
                                End If

                                If roleDescription.Length > 0 Then
                                    roleItem.Attributes.Add("style", "background-color:ActiveBorder; color:white; font-size:Larger;")
                                    .InformedID.Items.Add(roleItem)
                                Else
                                    roleItem.Attributes.Add("style", "background-color:ActiveBorder; color:white;")
                                    roleItem.Attributes.Add("disabled", "true")
                                    .InformedID.Items.Add(roleItem)
                                End If
                            End If

                            Dim useritem As New ListItem
                            With useritem
                                .Text = Server.HtmlDecode(spaceChar & UCase(dr.Item("FullName")))
                                If dr.Item("RoleSeqid").ToString <> "" Then
                                    .Value = dr.Item("roleplantcode") & "/" & dr.Item("RoleSeqid")
                                Else
                                    .Value = dr.Item("UserName")
                                End If
                            End With
                            .InformedID.Items.Add(useritem)

                        Loop
                    End If

                End If

                .DataBind()
                .LocalizeData = False
            End With
        End If

        RI.SharedFunctions.InsertRILoggingRecord(
"EnterMOC.aspx.vb", "PopulateNotificationList END")

        'JEB Me._pnlApprovals.Visible = True

    End Sub

    'Assume this is a brand new MOC so this will be the first email sent.  Only initiator and first level approvers should
    ' receive this email.  MOC record has already been saved to database.  MOCNumber saved in session variable.
    '
    ' 3/28/2017 ALA
    ' Change logic so no emails are sent if DRAFT is clicked
    Protected Sub _btnOkSaveApprovers_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnOkSaveApprovers.Click

        'Exit Sub

        RI.SharedFunctions.InsertRILoggingRecord("EnterMOC.aspx.vb", " _btnOkSaveApprovers_Click START ")
        Dim mocNumber As Integer
        Dim returnStatus As String = ""
        Dim approverSelected As Boolean = False
        Dim approvalEmailList As String = ""
        Dim approvalEmailMsg As String = ""
        Dim approvalSubEmailMsg As String = ""
        Dim newrevier As String = ""



        If Session.Item("MOCNumber") IsNot Nothing AndAlso IsNumeric(Session.Item("MOCNumber")) Then
            mocNumber = Session.Item("MOCNumber")
            If mocNumber > 0 Then
                'why do we need to go get the MOC again?  because its a postback
                currentMOC = New clsCurrentMOC(mocNumber)
                GetMOCSuperintendent(currentMOC.MOCNumber, currentMOC.SuperintendentType)


                _mpeSwapList.Hide()
                Dim HiddenSelectedL1Value As String = Me._slbApprovalNotificationList.HiddenSelectedValue
                Dim L1SelectedText As String = Me._slbApprovalNotificationList.SelectedText
                Dim SelectedL1Text As String() = L1SelectedText.Split(",")
                Dim HiddenSelectedL2Value As String = Me._slbApprovalNotificationList.HiddenSelectedSecondaryValue
                Dim HiddenSelectedL3Value As String = Me._slbApprovalNotificationList.HiddenSelectedThirdValue
                Dim HiddenSelectedInformedValue As String = Me._slbApprovalNotificationList.HiddenInformedSecondaryValue
                Dim SelectedApproversL1 As String() = HiddenSelectedL1Value.Split(",")
                Dim selectedApproversL2 As String() = HiddenSelectedL2Value.Split(",")
                Dim selectedApproversL3 As String() = HiddenSelectedL3Value.Split(",")
                Dim selectedInformed As String() = HiddenSelectedInformedValue.Split(",")
                Dim strRequired As String = "N"

                Me._slbApprovalNotificationList.InformedID.Items.Clear()
                Me._slbApprovalNotificationList.ApproverL1ID.Items.Clear()


                'Save Level 1 approvers
                If SelectedApproversL1 IsNot Nothing AndAlso SelectedApproversL1.Length > 1 Then
                    approverSelected = True
                    Dim s As String
                    Dim previousS As String = ""
                    For Each s In SelectedApproversL1

                        If s.Length > 1 And s <> previousS Then
                            If s.Contains("*") Then
                                strRequired = "Y"
                                currentMOC.InsertMOCApproval(mocNumber, Mid(s, 2), "L1", strRequired, MyGlobalUsername)
                            Else
                                'All level 1 reviewers are now required
                                strRequired = "Y"
                                'currentMOC.InsertMOCApproval(mocNumber, s, "L1", strRequired)
                                _reviewersupdate.InsertMOCApprovalJEB(mocNumber, s, "L1", strRequired, MyGlobalUsername)
                            End If
                        End If
                        previousS = s
                    Next
                End If


                'Save Informed Records
                strRequired = "N"
                If selectedInformed IsNot Nothing AndAlso selectedInformed.Length > 0 Then
                    Dim s As String
                    Dim previousS As String = ""
                    For Each s In selectedInformed
                        If s.Length > 1 And s <> previousS Then
                            If s.Contains("*") Then
                                strRequired = "Y"
                                currentMOC.InsertMOCApproval(mocNumber, Mid(s, 2), "E", strRequired, MyGlobalUsername)
                            Else
                                strRequired = "N"
                                currentMOC.InsertMOCApproval(mocNumber, s, "E", strRequired, MyGlobalUsername)
                            End If
                        End If
                        previousS = s
                    Next
                End If


                'currentMOC.GetApprovalsList()

                currentMOC = New clsCurrentMOC(mocNumber)
                GetMOCSuperintendent(currentMOC.MOCNumber, currentMOC.SuperintendentType)


                'go get the reviewers/informed and email
                Dim ds As DataSet = clsCurrentMOC.GetMOCReviewersForEmail(mocNumber)
                'loop through dataset
                Dim dr As Data.DataTableReader = Nothing
                Dim dt As New DataTable
                dr = ds.Tables(0).CreateDataReader

                If dr IsNot Nothing Then
                    RI.SharedFunctions.InsertRILoggingRecord("EnterMOC.aspx.vb", "Email reviewers START")

                    Do While dr.Read
                        If dr.Item("email").ToString() <> "" And dr.Item("approval_type").ToString() <> "" Then

                            If UCase(dr.Item("email").ToString) = UCase(MyGlobalEmail) Then
                                'the current user has just been added as a reviewer
                                'do something
                                newrevier = "Yes"
                            End If

                            SendEmail(dr.Item("email").ToString(), dr.Item("approval_type").ToString())
                        End If

                        'timestamp when the email was sent
                        currentMOC.UpdateEmailDate(dr.Item("approval_seqid").ToString, mocNumber)
                    Loop


                End If


                _mpeSwapList.Hide()

                If newrevier = "Yes" Then
                    Dim currentUserRS As DataSet
                    currentUserRS = currentMOC.GetCurrentApproverGPI()
                    Me._gvApprovals.DataSource = currentUserRS
                    Me._gvApprovals.DataBind()
                    PanelCurrentApprover.Visible = True

                End If

                'get approvers/reviewers
                Dim RS As DataSet
                RS = currentMOC.GetCurrentApproverListGPI()

                RadGridAssignedApprover.DataSource = RS
                RadGridAssignedApprover.DataBind()
                RadGridAssignedApprover.Rebind()
                'GetMOC()  'should this be GetMOCRecord()

            End If

        End If

        RI.SharedFunctions.InsertRILoggingRecord("EnterMOC.aspx.vb", "_btnOkSaveApprovers_Click END")

    End Sub

    Private Sub EmailMOC(emailType As String, Optional ByVal toList As String = "", Optional ByVal ccList As String = "", Optional ByVal Comments As String = "", Optional ByVal SubMsg As String = "")
        RI.SharedFunctions.InsertRILoggingRecord("EnterMOC.aspx.vb", " EmailMOC ")
        Dim Body As New StringBuilder
        Dim commonBody As New StringBuilder
        Dim spacer As String = "&nbsp;&nbsp;&nbsp;&nbsp;"
        Dim url As String = String.Empty
        Dim urlUpdateMOC As String = "<p><a href='{0}'>" & RI.SharedFunctions.LocalizeValue("Click here to View/Update Information") & "</a></p>"
        Dim urlMTT, urlMTTHost As String '= "<p><a href='{0}'>" & RI.SharedFunctions.LocalizeValue("Click here to View/Update Information") & "</a></p>"
        Dim additionalText As String = "<h2>**** THIS IS A TEST MOC NOTIFICATION ****</H2>" & toList
        Dim EmailSubMsg As String = SubMsg
        Dim strTaskEmails As String = ""
        Dim MOCNumber As String = String.Empty

        Dim urlHost As String = String.Empty
        Dim MOCStatus As String = String.Empty

        Dim subjectForNotificationList As String = Comments

        'Exit Sub

        If Request.UserHostAddress = "127.0.0.1" Then
            urlHost = "http://gpiazmeswebp01:6569/moc/"
            urlMTTHost = "http://gpiri.graphicpkg.com:130"
        Else
            If Request.ServerVariables("HTTP_HOST").ToLower.Contains("localhost") Then
                urlHost = "http://gpiazmeswebp01:6569/moc/"
                urlMTTHost = "http://gpiri.graphicpkg.com:130"
            ElseIf Request.ServerVariables("HTTP_HOST").ToLower.Contains("6569") Then       'test
                urlHost = "http://gpiazmeswebp01:6569/moc/"
                urlMTTHost = "http://gpiri.graphicpkg.com:130"
            Else
                urlHost = "http://gpiazmeswebp01:6565/moc/"
                urlMTTHost = "http://gpitasktracker.graphicpkg.com"

                additionalText = String.Empty
            End If
        End If


        If emailType = "NewMOC" Then
            If currentMOC IsNot Nothing Then
                MOCNumber = currentMOC.MOCNumber
                currentMOC = New clsCurrentMOC(MOCNumber)
                MOCStatus = Master.RIRESOURCES.GetResourceValue("New")
                subjectForNotificationList = MOCStatus & " " & Master.RIRESOURCES.GetResourceValue("MOC initiated in ") & " " & currentMOC.BusinessUnit & ": " & currentMOC.Title
                If currentMOC.NotificationL1List.Length > 0 Or currentMOC.NotificationL2List.Length > 0 Or currentMOC.NotificationL3List.Length > 0 Then
                    EmailSubMsg = EmailSubMsg & " " & Master.RIRESOURCES.GetResourceValue("You have been selected as an approver.  Please review this MOC and approve or reject.") & "</strong><br></p>"
                ElseIf currentMOC.NotificationEList.Length > 0 Then
                    EmailSubMsg = EmailSubMsg & " " & Master.RIRESOURCES.GetResourceValue("You have been selected to be informed of this MOC.")
                Else
                    EmailSubMsg = EmailSubMsg & " " & Master.RIRESOURCES.GetResourceValue("No approvers or people to be informed were selected for this MOC.  Please update MOC or proceed with execution.")
                End If
                If toList = "" Then
                    If currentMOC.NotificationL1List.Length > 0 Then
                        toList = currentMOC.NotificationL1List
                    ElseIf currentMOC.NotificationL2List.Length > 0 Then
                        toList = currentMOC.NotificationL2List
                    ElseIf currentMOC.NotificationL3List.Length > 0 Then
                        toList = currentMOC.NotificationL3List
                    Else
                        toList = currentMOC.NotificationEList
                    End If
                End If
                ccList = currentMOC.MOCCoordinatorEmail & "," & currentMOC.OwnerEmail
            End If
        End If

        'previousEmailSent = True
        'JEB 2/5/2019  add 'Or MOCNumber = ""
        If (MOCNumber Is Nothing Or MOCNumber = "") Then
            MOCNumber = Request.QueryString("MOCNumber")
        End If

        ''if this is testing then change email addresses to testing email address
        If Request.ServerVariables("HTTP_HOST").ToLower.Contains("localhost") Then
            toList = testingemail
            ccList = testingemail
        ElseIf Request.ServerVariables("HTTP_HOST").ToLower.Contains("6569") Then       'test
            toList = testingemail
            ccList = testingemail
        End If

        url = urlHost & "EnterMOC.aspx?MOCNumber=" & currentMOC.MOCNumber & "#Approval"


        'MOCNumber = Request.QueryString("MOCNumber")
        currentMOC = New clsCurrentMOC(MOCNumber)
        If emailType = "NotApproved" Then
            subjectForNotificationList = RI.SharedFunctions.LocalizeValue("MOC NOT Approved")
            'EmailSubMsg = RI.SharedFunctions.LocalizeValue("This MOC was not approved by") & " " & userProfile.FullName & "."
            EmailSubMsg = RI.SharedFunctions.LocalizeValue("This MOC was not approved by") & " " & MyGlobalFullName & "."
            If currentMOC.MOCTasksDT.HasRows Then
                commonBody.Append("<br>")
                commonBody.Append("<strong>" & RI.SharedFunctions.LocalizeValue("Tasks associated with this MOC that have already been created.  Please cancel the tasks as appropriate.") & "</strong><br>")
                commonBody.Append("<table><font size = ""2"" face=""Arial""><tr><td>Due Date</td><td>Title</TD><td>Responsible</td><td>Status</td></tr>")
                While currentMOC.MOCTasksDT.Read
                    Dim strResponsible As String = ""
                    If currentMOC.MOCTasksDT("responsibleusername").ToString = "" Then
                        strResponsible = currentMOC.MOCTasksDT("roledescription") & "(" & currentMOC.MOCTasksDT("responsible_role_names") & ")"
                        strTaskEmails = strTaskEmails & currentMOC.MOCTasksDT("responsibleMyGlobalEmail") & ","
                    Else
                        strResponsible = currentMOC.MOCTasksDT("whole_name_responsible_person")
                        strTaskEmails = strTaskEmails & currentMOC.MOCTasksDT("email") & ","
                    End If
                    urlMTT = urlMTTHost & "TaskDetails.aspx?HeaderNumber=" & currentMOC.MOCTasksDT("taskheaderseqid") & "&TaskNumber=" & currentMOC.MOCTasksDT("taskitemseqid") & "&refsite=MOC"
                    commonBody.Append("<tr><td>" & currentMOC.MOCTasksDT("duedate") & spacer & "</td><td><a href=" & urlMTT & ">" & currentMOC.MOCTasksDT("title") & "</a></td><td>" & spacer & strResponsible & "</td><td>" & spacer & currentMOC.MOCTasksDT("taskstatus") & "</td></tr>")
                End While
                commonBody.Append("</table>")
            End If

        ElseIf emailType = "Informed" Then
            subjectForNotificationList = "MOC Reviewed with New Comment"
            If currentMOC Is Nothing Then Throw New Exception("Error Getting Current Incident " & MOCNumber)
        End If
        'End If



        Body.Append("<html><head><title>Assign</title></head>")
        Body.Append("<body bgcolor=""#FFFFFF"">")
        Body.Append(additionalText)
        Body.Append("<p><font size = ""2"" face=""Arial""><strong>")
        Body.Append(EmailSubMsg)

        Body.Append("</strong><br></p>")

        '*****MOC Detail ***************
        commonBody.Append("<strong><li>" & Master.RIRESOURCES.GetResourceValue("MOCNumber") & ":</strong>" & spacer & currentMOC.MOCNumber)
        commonBody.Append("<strong><li>" & Master.RIRESOURCES.GetResourceValue("Title") & ":</strong>" & spacer & Me._txtTitle.Text)
        If Me._txtDescription.Text.Length > 0 Then
            commonBody.Append("<strong><li>" & Master.RIRESOURCES.GetResourceValue("Description") & ":</strong>" & spacer & RI.SharedFunctions.DataClean(Me._txtDescription.Text))
        End If

        commonBody.Append("<strong><li>" & Master.RIRESOURCES.GetResourceValue("Initiator/Owner") & ":</strong>" & spacer & Me.currentMOC.MOCCoordinatorName & "/" & currentMOC.OwnerName)
        'commonBody.Append("<strong><li>" & Master.RIRESOURCES.GetResourceValue("Project Kickoff Date") & ":</strong>" & spacer & Me.currentMOC.KickOffDate)
        'commonBody.Append("<strong><li>" & Master.RIRESOURCES.GetResourceValue("Implementation Date") & ":</strong>" & spacer & Me._tbMOCImplementationDate.StartDate)
        commonBody.Append("<strong><li>" & Master.RIRESOURCES.GetResourceValue("Implementation Date") & ":</strong>" & spacer & Me._tbMOCImplementationDate.SelectedDate)


        'commonBody.Append("<strong><li>" & Master.RIRESOURCES.GetResourceValue("Expiration Date") & ":</strong>" & spacer & Me._startEnd.EndDate)
        commonBody.Append("<strong><li>" & Master.RIRESOURCES.GetResourceValue("Area") & ":</strong>" & spacer & Me._ddlBusinessUnit.SelectedValue & " " & Me._ddlLineBreak.SelectedValue)
        commonBody.Append("<strong><li>" & Master.RIRESOURCES.GetResourceValue("Type") & "Type:</strong>" & spacer & Me._MOCType.Types)
        commonBody.Append("<strong><li>" & Master.RIRESOURCES.GetResourceValue("Classification") & ":</strong>" & spacer & Me.currentMOC.Classification)
        commonBody.Append("<strong><li>" & Master.RIRESOURCES.GetResourceValue("Level 1 Approvers") & ":</strong>" & spacer & Me.currentMOC.NotificationL1FullName & "<br>")
        'commonBody.Append("<strong><li>" & Master.RIRESOURCES.GetResourceValue("Level 2 Approvers") & ":</strong>" & spacer & Me.currentMOC.NotificationL2FullName & "<br>")
        'commonBody.Append("<strong><li>" & Master.RIRESOURCES.GetResourceValue("Level 3 Approvers") & ":</strong>" & spacer & Me.currentMOC.NotificationL3FullName & "<br>")
        commonBody.Append("<strong><li>" & Master.RIRESOURCES.GetResourceValue("Informed") & ":</strong>" & spacer & Me.currentMOC.NotificationEFullName)

        commonBody.Append("</ul>")
        '******End MOC Detail ************

        Body.Append(commonBody.ToString)

        urlUpdateMOC = String.Format(urlUpdateMOC, url)

        Body.Append(urlUpdateMOC)
        Body.Append("</body></html>")

        Dim msgForNotificationList As String = Body.ToString




        If emailType = "Approved" Then
            'RI.SharedFunctions.SendEmail(toList, userProfile.Email, subjectForNotificationList, msgForNotificationList, userProfile.FullName, ccList)
            RI.SharedFunctions.SendEmail(toList, MyGlobalEmail, subjectForNotificationList, msgForNotificationList, MyGlobalFullName, ccList)
            'RI.SharedFunctions.SendEmail(toList, currentMOC.MOCCoordinatorEmail, subjectForNotificationList, msgForNotificationList, currentMOC.MOCCoordinatorName, ccList)


        ElseIf emailType = "NewMOC" Then
            RI.SharedFunctions.SendEmail(toList, MyGlobalEmail, subjectForNotificationList, msgForNotificationList, MyGlobalFullName, ccList)

        ElseIf emailType = "NotApproved" Then
            RI.SharedFunctions.SendEmail(toList, MyGlobalEmail, subjectForNotificationList, msgForNotificationList, MyGlobalFullName, ccList & strTaskEmails)

        ElseIf emailType = "Informed" Then
            RI.SharedFunctions.SendEmail(currentMOC.MOCCoordinatorEmail, MyGlobalEmail, subjectForNotificationList, msgForNotificationList, MyGlobalFullName, MyGlobalEmail)

        End If

        'added 3/27/2019
        If CBool(ConfigurationManager.AppSettings("TrackMOC")) Then
            RI.SharedFunctions.SendEmail(ConfigurationManager.AppSettings("TrackMOCEmail"), MyGlobalEmail, subjectForNotificationList, msgForNotificationList)

        End If

        RI.SharedFunctions.InsertRILoggingRecord(
            "EnterMOC.aspx.vb", " EmailMOC END")


    End Sub


    Private Sub EmailMOCNotApproved(emailType As String, Optional ByVal toList As String = "", Optional ByVal ccList As String = "", Optional ByVal Comments As String = "", Optional ByVal SubMsg As String = "")
        RI.SharedFunctions.InsertRILoggingRecord("EnterMOC.aspx.vb", " EmailMOCNotApproved ")
        Dim Body As New StringBuilder
        Dim commonBody As New StringBuilder
        Dim spacer As String = "&nbsp;&nbsp;&nbsp;&nbsp;"
        Dim url As String = String.Empty
        Dim additionalText As String = "<h2>**** THIS IS A TEST MOC NOTIFICATION ****</H2>" & toList
        Dim EmailSubMsg As String = SubMsg
        Dim strTaskEmails As String = ""
        Dim MOCNumber As String = String.Empty

        Dim urlHost As String = String.Empty
        Dim MOCStatus As String = String.Empty

        Dim subjectForNotificationList As String = Comments

        Dim urlMTTHost As String = "http://gpitasktracker.graphicpkg.com"

        MOCNumber = Request.QueryString("MOCNumber")
        currentMOC = New clsCurrentMOC(MOCNumber)

        subjectForNotificationList = RI.SharedFunctions.LocalizeValue("MOC NOT Approved")

        EmailSubMsg = RI.SharedFunctions.LocalizeValue("This MOC was not approved by") & " " & MyGlobalFullName & "."

        If currentMOC.MOCTasksDT.HasRows Then
            commonBody.Append("<strong>" & RI.SharedFunctions.LocalizeValue("Tasks associated with this MOC that have already been created.  Please cancel the tasks as appropriate.") & "</strong><br>")
            commonBody.Append("<table><font size = ""2"" face=""Arial""><tr><td>Due Date</td><td>Title</TD><td>Responsible</td><td>Status</td></tr>")
            While currentMOC.MOCTasksDT.Read
                Dim strResponsible As String = ""
                If currentMOC.MOCTasksDT("responsibleusername").ToString = "" Then
                    strResponsible = currentMOC.MOCTasksDT("roledescription") & "(" & currentMOC.MOCTasksDT("responsible_role_names") & ")"
                    strTaskEmails = strTaskEmails & currentMOC.MOCTasksDT("responsibleMyGlobalEmail") & ","
                Else
                    strResponsible = currentMOC.MOCTasksDT("whole_name_responsible_person")
                    strTaskEmails = strTaskEmails & currentMOC.MOCTasksDT("email") & ","
                End If

                commonBody.Append("<tr><td>" & currentMOC.MOCTasksDT("duedate") & spacer & "</td><td><a href=" & urlMTTHost & ">" & currentMOC.MOCTasksDT("title") & "</a></td><td>" & spacer & strResponsible & "</td><td>" & spacer & currentMOC.MOCTasksDT("taskstatus") & "</td></tr>")
            End While
            commonBody.Append("</table>")
        End If




        Body.Append("<html><head><title>Assign</title></head>")
        Body.Append("<body bgcolor=""#FFFFFF"">")
        Body.Append("</strong><br>")
        Body.Append(commonBody)
        Body.Append("</body></html>")

        Dim msgForNotificationList As String = Body.ToString





        If emailType = "NotApproved" Then
            'RI.SharedFunctions.SendEmail(toList, MyGlobalEmail, subjectForNotificationList, msgForNotificationList, MyGlobalFullName, ccList & strTaskEmails)
        End If

        'added 3/27/2019



    End Sub

    Private Sub SendEmail(ByVal emailAddress As String, ByVal reviewerType As String, Optional ByVal notapprovedby As String = "")

        RI.SharedFunctions.InsertRILoggingRecord(
"EnterMOC.aspx.vb", " SendEmail ")

        Dim strHeader As String = ""
        Dim strSubject As String = ""

        Dim toPerson As String = ""

        If reviewerType = "E" Then
            strHeader = "You have been selected as an Informed person.  Please view this MOC."

        Else
            strHeader = "You have been selected as an approver.  Please review this MOC and approve or reject."

        End If
        strSubject = "New MOC initiated - " & currentMOC.BusinessUnit

        If notapprovedby = "" Then
            'do nothing
        Else
            strHeader = "This MOC was not approved by: " & notapprovedby

        End If


        'email to Superintendent reviewers
        Dim bsent As Boolean = clsCurrentMOC.PrepareL1Email(currentMOC.MOCNumber,
                                              currentMOC.Title,
                                              currentMOC.Description,
                                              MyGlobalFullName & " (" & MyGlobalEmail & ")",
                                              currentMOC.MOCType,
                                              currentMOC.Costs,
                                              currentMOC.Funding,
                                              currentMOC.Impact,
                                              currentMOC.NotificationL1FullName,
                                              currentMOC.NotificationEFullName,
                                              currentMOC.StartDate,
                                              currentMOC.Classification,
                                              currentMOC.Category,
                                              currentMOC.BusinessUnit,
                                              currentMOC.SuperintendentName,
                                              currentMOC.SuperintendentDate,
                                              currentMOC.SuperintendentComments,
                                              "MOC.Notification@graphicpkg.com",
                                              emailAddress,
                                              strHeader,
                                              strSubject,
                                              "") ' Me._SuperintendentBusinessType.SelectedItem.Text)




        RI.SharedFunctions.InsertRILoggingRecord(
"EnterMOC.aspx.vb", " SendEmail END")


    End Sub

    'Private Sub EmailMOCGPI(emailType As String, Optional ByVal toL1List As String = "", Optional ByVal toEList As String = "", Optional ByVal toccList As String = "", Optional ByVal SubMsg As String = "")
    Private Sub EmailMOCGPI()


        'go get reviewers that have not been emailed
        'go get the reviewers/informed and email
        Dim ds As DataSet = clsCurrentMOC.GetMOCReviewersForEmail(currentMOC.MOCNumber)
        'loop through dataset
        Dim dr As Data.DataTableReader = Nothing
        Dim dt As New DataTable
        dr = ds.Tables(0).CreateDataReader

        If dr IsNot Nothing Then
            RI.SharedFunctions.InsertRILoggingRecord("EnterMOC.aspx.vb", "Email reviewers START")

            Do While dr.Read
                If dr.Item("email").ToString() <> "" And dr.Item("approval_type").ToString() <> "" Then

                    If UCase(dr.Item("email").ToString) = UCase(MyGlobalEmail) Then
                        'the current user has just been added as a reviewer
                        'do something

                    End If

                    SendEmail(dr.Item("email").ToString(), dr.Item("approval_type").ToString())
                End If

                'timestamp when the email was sent
                currentMOC.UpdateEmailDate(dr.Item("approval_seqid").ToString, currentMOC.MOCNumber)
            Loop


        End If




    End Sub

    Protected Sub CreateUSDTicket(ByVal Facility As String, ByVal Summary As String, ByVal Desc As String, ByVal ReportedBy As String)

        Dim dbParam As New SqlParameter
        Dim dbCmd As New SqlClient.SqlCommand
        Dim cn As SqlClient.SqlConnection = Nothing

        Dim USDGenerationOn As String = ConfigurationManager.AppSettings("USDGeneration")
        Dim connectString As String
        Dim UniqueKey As String
        Dim key As String

        Try

            If USDGenerationOn <> "True" Then Exit Sub

            UniqueKey = Facility & "-MOC-" & Now()
            connectString = ConfigurationManager.ConnectionStrings.Item("USDSqlServer").ConnectionString

            cn = New SqlConnection(connectString)

            cn.Open()
            'dbCmd = dbPF.CreateCommand
            dbCmd.Connection = cn
            dbCmd.CommandType = CommandType.StoredProcedure
            dbCmd.CommandText = "dbo.up_Insert_Ticket"

            dbCmd.Parameters.Add("@FacilityAbbr", SqlDbType.VarChar, Facility.Length).Value = Facility
            dbCmd.Parameters.Add("@Application", SqlDbType.VarChar, 50).Value = "MOC"
            dbCmd.Parameters.Add("@Summary", SqlDbType.VarChar, 150).Value = Summary
            dbCmd.Parameters.Add("@Description", SqlDbType.VarChar, 250).Value = Desc
            dbCmd.Parameters.Add("@ReportedBy", SqlDbType.VarChar, 50).Value = ReportedBy
            dbCmd.Parameters.Add("@Priority", SqlDbType.Int, 2).Value = 4
            dbCmd.Parameters.Add("@Type", SqlDbType.VarChar, 100).Value = "RFS"
            dbCmd.Parameters.Add("@UniqueKey", SqlDbType.VarChar, 100).Value = UniqueKey

            'SqlParameter pvNewId = new SqlParameter();
            dbParam.ParameterName = "@IndexID"
            dbParam.DbType = DbType.String
            dbParam.Size = 100
            dbParam.Direction = ParameterDirection.Output
            dbCmd.Parameters.Add(dbParam)

            key = dbCmd.ExecuteNonQuery()
            If key = "-1" Then
                ' resave MOC to indicate ticket created.
                currentMOC.SaveMOCUSD(currentMOC.MOCNumber, key)
            End If
        Catch ex As Exception
            Throw
        Finally
            If Not dbCmd Is Nothing Then
                dbCmd = Nothing
            End If
            If Not (cn Is Nothing) Then
                If (cn.State = ConnectionState.Open) Then
                    cn.Close()
                End If
            End If
            If Not (dbParam Is Nothing) Then
                dbParam = Nothing
            End If
        End Try

    End Sub

    Private Sub _btnSaveDraft_Click(sender As Object, e As EventArgs) Handles _btnSaveDraft.Click
        RI.SharedFunctions.InsertRILoggingRecord("EnterMOC.aspx.vb", " _btnSaveDraft_Click ")
        Dim currentDraftMOC As clsCurrentMOCDraftStatus
        If Session.Item("MOCNumber") IsNot Nothing AndAlso IsNumeric(Session.Item("MOCNumber")) Then
            currentDraftMOC = New clsCurrentMOCDraftStatus()
            currentDraftMOC.SaveMOCDraftStatus(Session.Item("MOCNumber"))
            _btnOkSaveApprovers_Click(sender, e)
        End If
    End Sub

    Private Sub _btnCreateTasks_Click(sender As Object, e As EventArgs) Handles _btnCreateTasks.Click

        RI.SharedFunctions.InsertRILoggingRecord(
"EnterMOC.aspx.vb", " _btnCreateTasks_Click ")

        Dim mocNumber As Integer

        If Session.Item("MOCNumber") IsNot Nothing AndAlso IsNumeric(Session.Item("MOCNumber")) Then
            mocNumber = Session.Item("MOCNumber")
            If mocNumber > 0 Then
                currentMOC = New clsCurrentMOC(mocNumber)
                'userProfile = RI.SharedFunctions.GetUserProfile
                Dim cntRowChecked As Integer = 0

                Dim returns As String
                For Each row As GridViewRow In Me._gvTemplateTasksDaysAfter.Rows
                    Dim cb As CheckBox = row.FindControl("_cbCreate")

                    If cb.Checked = True Then
                        cntRowChecked = 1

                        Dim taskSeqID As HiddenField
                        taskSeqID = row.FindControl("_hfTaskItemSeqID")

                        Dim ucResponsible As ucMTTResponsible
                        Dim ucMOCDate As ucMOCDate
                        Dim tbDescription As TextBox
                        Dim strResponsibleUsername, strResponsibleRole As String
                        Dim strFacility As String
                        Dim strDaysAfter, strDueDate As String

                        ucResponsible = row.FindControl("_ucNewResponsible")
                        tbDescription = row.FindControl("_tbDescription")
                        ucMOCDate = row.FindControl("_ucMOCDate")
                        strDaysAfter = ucMOCDate.DaysAfter
                        strDueDate = ucMOCDate.DateValue
                        If IsNumeric(ucResponsible.ResponsibleValue) Then
                            strResponsibleRole = ucResponsible.ResponsibleValue
                            strFacility = ucResponsible.FacilityValue
                            strResponsibleUsername = ""
                        Else
                            strResponsibleUsername = ucResponsible.ResponsibleValue
                            strFacility = ucResponsible.FacilityValue = ""
                            strResponsibleRole = ""
                        End If

                        returns = currentMOC.SaveMOCTemplateTasks(currentMOC.MOCNumber, taskSeqID.Value.ToString, strResponsibleUsername, strResponsibleRole, strFacility, tbDescription.Text, strDaysAfter, strDueDate, MyGlobalUsername)

                    End If
                Next

                ''Call function to check if any template tasks with duedates were selected and need to be created.
                'currentMOC.CreateMOCTemplateTasks()
            End If
        End If

        RI.SharedFunctions.InsertRILoggingRecord(
"EnterMOC.aspx.vb", " _btnCreateTasks_Click END")

        Response.Clear()
        Response.Redirect(Page.AppRelativeVirtualPath & "?MOCNumber=" & currentMOC.MOCNumber, True)
        Response.End()
    End Sub


    Protected Sub _gvClassQuestions_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles _gvClassQuestions.RowCreated
        Exit Sub
        If e.Row.RowIndex >= 0 Then
            Dim cbAnswer As CheckBoxList = CType(e.Row.Cells(1).FindControl("_cbAnswer"), CheckBoxList)
            Dim answer_flag As String = RI.SharedFunctions.DataClean(DataBinder.Eval(e.Row.DataItem, "answer"), "")
            If cbAnswer IsNot Nothing Then
                If answer_flag = "Y" Then
                    cbAnswer.SelectedValue = "Y"
                Else
                    If answer_flag = "N" Then
                        cbAnswer.SelectedValue = "N"
                    End If
                End If
            End If
        End If
    End Sub

    Protected Sub _gvCatQuestions_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles _gvCatQuestions.RowCreated
        If e.Row.RowIndex >= 0 Then
            Dim cbAnswer As CheckBoxList = CType(e.Row.Cells(1).FindControl("_cbAnswer"), CheckBoxList)
            Dim answer_flag As String = RI.SharedFunctions.DataClean(DataBinder.Eval(e.Row.DataItem, "answer"), "")
            If cbAnswer IsNot Nothing Then
                If answer_flag = "Y" Then
                    cbAnswer.SelectedValue = "Y"
                Else
                    If answer_flag = "N" Then
                        cbAnswer.SelectedValue = "N"
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub _btnCloseTasks_Click(sender As Object, e As EventArgs) Handles _btnCloseTasks.Click
        Dim MocNumber As String
        MocNumber = Session.Item("MOCNumber")
        If MocNumber > 0 Then
            currentMOC = New clsCurrentMOC(MocNumber)
        End If
        Response.Clear()
        Response.Redirect(Page.AppRelativeVirtualPath & "?MOCNumber=" & currentMOC.MOCNumber, True)
        Response.End()
    End Sub


    Private Sub _btnCopyMOC_Click(sender As Object, e As EventArgs) Handles _btnCopyMOC.Click
        If Request.QueryString("MOCNumber") IsNot Nothing Then
            currentMOC = New clsCurrentMOC(Request.QueryString("MOCNumber"))
            Dim out_MOCnumber = currentMOC.CopyMOC()
            currentMOC.MOCNumber = out_MOCnumber

            EmailMOC("NewMOC")

            If out_MOCnumber IsNot Nothing Then
                Response.Clear()
                Response.Redirect(Page.AppRelativeVirtualPath & "?MOCNumber=" & out_MOCnumber, True)
                Response.End()
            End If
        End If

    End Sub

    Private Sub _btnSaveMOC_Click(sender As Object, e As EventArgs) Handles _btnSaveMOC.Click
        RI.SharedFunctions.InsertRILoggingRecord("EnterMOC.aspx.vb", " btnSaveMOC_Click ")
        SaveMOC(True, "MoveToPerm")
    End Sub

    Protected Sub _btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnDelete.Click
        RI.SharedFunctions.InsertRILoggingRecord("EnterMOC.aspx.vb", " _btnDelete_Click ")

        If Request.QueryString("MOCNumber") IsNot Nothing Then
            Dim MOCNumber As String = Request.QueryString("MOCNumber")
            If MOCNumber > 0 Then
                clsCurrentMOC.DeleteCurrentMOC(MOCNumber, MyGlobalUsername)
                'MsgBox(MOCNumber & " has been deleted", MsgBoxStyle.Information)
                Response.Redirect("~/MOC/EnterMOC.aspx", True)
            End If
        End If

    End Sub


    'Protected Sub MOC_StatusChanged(ByVal sender As System.Web.UI.WebControls.TextBox) Handles MOCStatus.StatusDDLChanged

    '    SaveMOC()

    'End Sub

    Private Sub MOCStatus_StatusChanged(sender As DropDownList) Handles MOCStatus.StatusDDLChanged

        RI.SharedFunctions.InsertRILoggingRecord(
"EnterMOC.aspx.vb", " MOCStatus_StatusChanged ")

        Dim msg As New StringBuilder
        Dim msg2 As New StringBuilder

        If UCase(MOCStatus.Status) = "REMOVE FROM HOLD" Or UCase(MOCStatus.Status) = "APPROVAL REQUESTED" Then
            SaveMOC(True, "INITIATE")
        ElseIf UCase(MOCStatus.Status) = "IMPLEMENTED" Then
            'If implemented check if any pending approvers.  If there are pending approvers,
            'give option to cancel Or proceed.
            'Dim msg As New StringBuilder
            'Dim pendingMOC As Boolean = False
            'For Each row As GridViewRow In Me._gvApprovalsL1.Rows
            '    Dim approved As CheckBoxList = CType(row.FindControl("_cbApproval"), CheckBoxList)
            '    If approved.SelectedValue = "" Then
            '        pendingMOC = True
            '        Exit For
            '    End If
            'Next

            'For Each row As GridViewRow In Me._gvApprovalsL2.Rows
            '    Dim approved As CheckBoxList = CType(row.FindControl("_cbApprovalL2"), CheckBoxList)
            '    If approved.SelectedValue = "" Then
            '        pendingMOC = True
            '        Exit For
            '    End If
            'Next

            'For Each row As GridViewRow In Me._gvApprovalsL3.Rows
            '    Dim approved As CheckBoxList = CType(row.FindControl("_cbApprovalL3"), CheckBoxList)
            '    If approved.SelectedValue = "" Then
            '        pendingMOC = True
            '        Exit For
            '    End If
            'Next

            Dim pendingMOCQuestions As Boolean = False
            For Each row As GridViewRow In Me._gvCatQuestions.Rows
                Dim answered As CheckBoxList = CType(row.FindControl("_cbAnswer"), CheckBoxList)
                If answered IsNot Nothing Then
                    If answered.SelectedValue = "" Then
                        pendingMOCQuestions = True
                        Exit For
                    End If
                Else
                    Dim questiontext As TextBox = CType(row.FindControl("_tbAnswer"), TextBox)
                    If questiontext IsNot Nothing Then
                        If questiontext.Text = "" Then
                            pendingMOCQuestions = True
                        End If
                    End If
                End If
            Next

            If pendingMOCQuestions = False Then
                For Each row As GridViewRow In Me._gvClassQuestions.Rows
                    Dim answered As CheckBoxList = CType(row.FindControl("_cbAnswer"), CheckBoxList)
                    Dim questiontext As TextBox = CType(row.FindControl("_tbAnswer"), TextBox)
                    If answered IsNot Nothing Then
                        If answered.Visible = True And answered.SelectedValue = "" Then
                            pendingMOCQuestions = True
                            Exit For
                        End If
                    End If
                    If questiontext IsNot Nothing Then
                        If questiontext.Visible = True And questiontext.Text = "" Then
                            pendingMOCQuestions = True
                            Exit For
                        End If
                    End If
                Next
            End If

            'Not sure why this is
            'JEB If pendingMOC = True Then
            'JEB msg.Append(Master.RIRESOURCES.GetResourceValue("All approvers have NOT approved this MOC. Do you want to override approvers and implement?"))
            'JEB End If

            If pendingMOCQuestions = True Then
                msg2.Append(Master.RIRESOURCES.GetResourceValue("All questions have NOT been answered for this MOC."))
            End If

            If msg.ToString.Length > 0 Or msg2.ToString.Length > 0 Then
                MOCStatus.Status = currentMOC.Status
                SaveMOC(False)
                MOCStatus.Status = "Implemented"
                msg.Append("</ul>")
                _messageBox.Title = Master.RIRESOURCES.GetResourceValue("MOC Warning")
                _messageBox.Message = "<center><h2>" & Master.RIRESOURCES.GetResourceValue("ATTENTION:") & "</h2></center><br>" & msg.ToString & "<br>" & msg2.ToString & "<br>"
                _messageBox.Width = 200
                '                    _messageBox.CancelScript = "javascript:window.location='" & Page.ResolveClientUrl("~/MOC/EnterMOC.aspx?MOCNumber=" & Request.QueryString("MOCNumber")) & "'"
                _messageBox.CancelScript = "javascript: return false;"
                _messageBox.ShowMessage()
                'Dim ret As MsgBoxResult = MsgBox("WARNING:" & vbCrLf & vbCrLf & msg.ToString & vbCrLf & "<br>Do you want to continue?", MsgBoxStyle.YesNo)
                'RI.SharedFunctions.ASPNET_MsgBox("WARNING:" & vbCrLf & vbCrLf & msg.ToString & vbCrLf & "<br>Do you want to continue?")
            Else
                SaveMOC(True)
            End If
        Else
            SaveMOC(True)
        End If

        RI.SharedFunctions.InsertRILoggingRecord(
"EnterMOC.aspx.vb", " MOCStatus_StatusChanged END")
        'SaveMOC()
    End Sub
    'Protected Sub _Status_StatusChanged(ByVal sender As System.Web.UI.WebControls.TextBox, ByVal e As System.EventArgs) Handles MOCStatus.StatusChangedEventHandler
    '    _btnSubmit_Click(sender, e)
    '    'Dim taskstatus As New TaskTrackerListsBll
    '    'Dim status As System.Collections.Generic.List(Of TaskStatus)
    '    ''        Dim imgPath As String = Page.ResolveUrl("~/Images/") ' COMMENTED BY CODEIT.RIGHT

    '    'status = taskstatus.GetTaskStatus
    '    'If Me._rblStatus.SelectedValue = "1" Then
    '    '    For Each item As TaskStatus In status
    '    '        If item.StatusName.ToLower = "complete" Then
    '    '            If Me._rblStatus.Items.FindByValue(CStr(item.StatusSeqid)) IsNot Nothing Then
    '    '                Me._rblStatus.ClearSelection()
    '    '                Me._rblStatus.Items.FindByValue(CStr(item.StatusSeqid)).Selected = True
    '    '            End If
    '    '        End If
    '    '    Next
    '    'End If
    '    'Me.UpdateSubTask()
    'End Sub

    Public Sub EnableUserControls(ByVal ctrlstatus As Boolean, ByVal currControl As UserControl)
        For Each ctrl As Control In currControl.Controls
            If TypeOf ctrl Is TextBox Then
                DirectCast(ctrl, TextBox).Enabled = ctrlstatus
            ElseIf TypeOf ctrl Is Button Then
                DirectCast(ctrl, Button).Enabled = ctrlstatus
            ElseIf TypeOf ctrl Is RadioButtonList Then
                DirectCast(ctrl, RadioButtonList).Enabled = ctrlstatus
            ElseIf TypeOf ctrl Is ImageButton Then
                DirectCast(ctrl, ImageButton).Enabled = ctrlstatus
            ElseIf TypeOf ctrl Is CheckBox Then
                DirectCast(ctrl, CheckBox).Enabled = ctrlstatus
            ElseIf TypeOf ctrl Is CheckBoxList Then
                DirectCast(ctrl, CheckBoxList).Enabled = ctrlstatus
            ElseIf TypeOf ctrl Is DropDownList Then
                DirectCast(ctrl, DropDownList).Enabled = ctrlstatus
            ElseIf TypeOf ctrl Is HtmlImage Then
                DirectCast(ctrl, HtmlImage).Visible = ctrlstatus
            End If
        Next
    End Sub

    Protected Sub FieldChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Session("MOCChanged") = "Y"
    End Sub

    Private Sub _btnUnauthorized_Click(sender As Object, e As EventArgs) Handles _btnUnauthorized.Click
        Response.Clear()
        Response.Redirect(Page.AppRelativeVirtualPath, True)
        Response.End()
    End Sub
    Protected Sub _dlSystem_SelectedIndexChanged(sender As Object, e As EventArgs) Handles _dlSystem.SelectedIndexChanged

    End Sub

    Protected Sub RadGridAssignedApprover_DeleteCommand(ByVal source As Object, ByVal e As GridCommandEventArgs) Handles RadGridAssignedApprover.DeleteCommand

        Dim mocNumber As Integer

        If Session.Item("MOCNumber") IsNot Nothing AndAlso IsNumeric(Session.Item("MOCNumber")) Then
            mocNumber = Session.Item("MOCNumber")
            If mocNumber > 0 Then
                If e.Canceled = False Then
                    Dim ID As String = (CType(e.Item, GridDataItem)).OwnerTableView.DataKeyValues(e.Item.ItemIndex)("approval_seqid").ToString

                    'go delete the reviewer

                    Dim returnstatus As String
                    'returnstatus = currentMOC.DeleteMOCApprovalBySeqid(currentMOC.MOCNumber, ID)
                    returnstatus = reviewersUpdate.DeleteMOCApprovalBySeqid(mocNumber.ToString, ID)

                    If returnstatus = 0 Then
                        currentMOC = New clsCurrentMOC(mocNumber)
                        Dim RS As DataSet = currentMOC.GetCurrentApproverListGPI()

                        RadGridAssignedApprover.DataSource = RS
                        RadGridAssignedApprover.DataBind()
                    End If


                End If
            End If
        End If
        e.Canceled = True

    End Sub

    Protected Sub RadGridAssignedApprover_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles RadGridAssignedApprover.ItemDataBound



        If (TypeOf e.Item Is GridDataItem) Then

            RI.SharedFunctions.InsertRILoggingRecord(
"EnterMOC.aspx.vb", "RadGridAssignedApprover_ItemDataBound START ")

            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)

            Dim cell As TableCell = dataItem("approval_type")
            Dim cellApproval = dataItem("approved")
            Dim cellRequired = dataItem("required_flag")

            Dim superintendentColor As System.Drawing.Color = Color.FromArgb(187, 255, 187)
            Dim required_flagColor As System.Drawing.Color = Color.FromArgb(0, 232, 0)


            If cell.Text.Trim = "Level 1" Then
                'dataItem.BackColor = Color.AliceBlue  'this will change the row color
                'cell.BackColor = Color.AliceBlue
                'cell.ForeColor = Color.Black
            ElseIf cell.Text.Trim = "Level 2" Then
                ' cell.BackColor = Color.LightGreen
                ' cell.ForeColor = Color.Black
                'dataItem.BackColor = Color.LightGray  'this will change the row color
            ElseIf cell.Text.Trim = "Level 3" Then
                'cell.BackColor = Color.LightYellow
                'cell.ForeColor = Color.Black
                'dataItem.BackColor = Color.LightSteelBlue 'this will change the row color
            ElseIf cell.Text.Trim = "Informed" Then
                'cell.BackColor = Color.LightYellow
                'cell.ForeColor = Color.Black
                'dataItem.BackColor = Color.LightGoldenrodYellow  'this will change the row color

                'Informed
            End If


            Select Case dataItem("approval_type").Text
                Case "Superintendent"
                    cell.BackColor = superintendentColor
                Case "Level 1"
                    cell.BackColor = Color.AliceBlue
                Case "Level 2"
                    cell.BackColor = Color.LightGreen
                Case "Level 3"
                    cell.BackColor = Color.LightSteelBlue
                Case "Informed"
                    cell.BackColor = Color.LightGoldenrodYellow
            End Select

            Select Case dataItem("approved").Text
                Case "NO"
                    cellApproval.BackColor = Color.Red
                    cellApproval.ForeColor = Color.White
                Case "N"
                    cellApproval.BackColor = Color.Red
                    cellApproval.ForeColor = Color.White

            End Select


            Select Case dataItem("approved").Text.ToString
                Case "&nbsp;"
                    If dataItem("required_flag").Text = "YES" Or dataItem("required_flag").Text = "Y" Then
                        cellApproval.BackColor = Color.Yellow
                        ' cellApproval.ForeColor = Color.White
                    End If
            End Select

            Select Case dataItem("required_flag").Text
                Case "YES"
                    cellRequired.BackColor = required_flagColor
                Case "Y"
                    cellRequired.BackColor = required_flagColor
            End Select

            Dim contactName As String = dataItem("personname").Text
            'for the Lightweight RenderMode
            Dim button As ElasticButton = CType(dataItem("DeleteColumn").Controls(0), ElasticButton)


            If UCase(dataItem("approval_type").Text) = UCase("Superintendent") Then
                'do not add delete button
                button.Visible = False
            Else
                button.Attributes("onclick") = "if (!confirm('Are you sure you want to delete reviewer? " & contactName & "?')) {return false;}"
                If AuthLevel = "MILLADMIN" Then
                    button.Visible = True
                Else
                    button.Visible = False
                End If
            End If



            'If UCase(dataItem("approval_type").Text) = UCase("Superintendent") Then
            '    dataItem.CssClass = "someRowClass"
            'End If


            RI.SharedFunctions.InsertRILoggingRecord(
"EnterMOC.aspx.vb", "RadGridAssignedApprover_ItemDataBound END ")

        End If



    End Sub


    Public Function SaveReviewerComments(ByVal seqid As Integer, ByVal mocnumber As Integer, ByVal approval_flag As String, ByVal mycomments As String, ByVal username As String) As String


        RI.SharedFunctions.InsertRILoggingRecord(
"EnterMOC.aspx.vb", " SaveReviewerComments  ")

        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        'Check input paramaters
        Try
            param = New OracleParameter
            param.ParameterName = "in_MOCNumber"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = mocnumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_ApprovalSeqId"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = seqid
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_username"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = username
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Comments"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = mycomments
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_approval_flag"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = approval_flag
            paramCollection.Add(param)


            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.rimoc.UpdateMOCReviewerComment")


            Return returnStatus

            If CDbl(returnStatus) <> 0 Then
                Throw New Data.DataException("Error Saving System Record " & mocnumber)
            End If

        Catch ex As Exception
            Throw
        End Try

        RI.SharedFunctions.InsertRILoggingRecord(
"EnterMOC.aspx.vb", " SaveReviewerComments  END")

    End Function


    Public ReadOnly Property ApproverScores() As DataTable

        Get
            Dim dr As Data.DataTableReader = Nothing
            Dim dt As New DataTable
            Dim obj As Object = Me.Session("ApproverScores")
            'If (Not obj Is Nothing) Then
            'Return CType(obj, DataTable)
            'End If

            RI.SharedFunctions.InsertRILoggingRecord(
            "EnterMOC.aspx.vb", " ApproverScores  ")
            Dim myDataSet As DataSet = New DataSet
            myDataSet = currentMOC.GetCurrentApproverGPI()

            dr = myDataSet.Tables(0).CreateDataReader
            dt.TableName = "RESULTS"
            dt.Load(dr)

            RI.SharedFunctions.InsertRILoggingRecord(
            "EnterMOC.aspx.vb", " ApproverScores END ")

            'Me.Session("ApproverScores") = dt
            Return dt
        End Get
    End Property


    Public ReadOnly Property ApproverBUMScores() As DataTable
        Get
            Dim dr As Data.DataTableReader = Nothing
            Dim dt As New DataTable
            Dim obj As Object = Me.Session("ApproverScores")
            'If (Not obj Is Nothing) Then
            'Return CType(obj, DataTable)
            'End If

            RI.SharedFunctions.InsertRILoggingRecord(
            "EnterMOC.aspx.vb", " ApproverBUMScores  ")

            Dim myDataSet As DataSet = New DataSet
            myDataSet = currentMOC.GetCurrentBUMApproverGPI()

            dr = myDataSet.Tables(0).CreateDataReader
            dt.TableName = "RESULTS"
            dt.Load(dr)

            RI.SharedFunctions.InsertRILoggingRecord(
            "EnterMOC.aspx.vb", " ApproverBUMScores END ")

            'Me.Session("ApproverScores") = dt
            Return dt
        End Get
    End Property


    Private Sub DisplayMessage(ByVal isError As Boolean, ByVal text As String)
        'Dim label As Label = IIf(isError, Me.Label1, Me.Label2)
        'Label.Text = text
    End Sub

    Private Sub MOC_EnterMOCNew_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit



        RI.SharedFunctions.InsertRILoggingRecord(
"EnterMOC.aspx.vb", "MOC_EnterMOCNew_PreInit ")

        Page.Theme = GPI.XML.XMLUtility.ReadXmlNode(Session("UserProfile").ToString, "PageTheme")

        MyGlobalUsername = GPI.XML.XMLUtility.ReadXmlNode(Session("UserProfile").ToString, "Username")


        If RI.SharedFunctions.IsDevloperUser(MyGlobalUsername) = True And Request.QueryString("MOCNumber") Is Nothing Then
            ''do something
        Else
            If Request.QueryString("MOCNumber") Is Nothing Then
                Response.Redirect("~/MOC/ViewMOC.aspx", True)
            End If
        End If

        RI.SharedFunctions.InsertRILoggingRecord(
"EnterMOC.aspx.vb", "MOC_EnterMOCNew_PreInit END")

    End Sub

    Private Sub MOC_EnterMOCNew_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender

        RI.SharedFunctions.InsertRILoggingRecord(
"EnterMOC.aspx.vb", "MOC_EnterMOCNew_PreRender ")

        If Page.IsPostBack = True Then
            Exit Sub
        End If


        Try


            _tbMOCProjectKickoffDate.MinDate = Date.Now.AddDays(-120)
            _tbMOCProjectKickoffDate.MaxDate = Date.Today

            'refProjectKickoffDate.MinimumValue = DateTime.Now.Date.AddYears(-1).ToString("dd-MM-yy")
            'refProjectKickoffDate.MaximumValue = DateTime.Now.Date.ToString("dd-MM-yy")

        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try

        RI.SharedFunctions.InsertRILoggingRecord(
"EnterMOC.aspx.vb", "MOC_EnterMOCNew_PreRender END")

    End Sub


    Protected Sub SaveMOC(Optional ByVal refreshPage As Boolean = True, Optional ByVal SavedMOCStatus As String = "")
        RI.SharedFunctions.InsertRILoggingRecord("EnterMOC.aspx.vb", " START - SaveMOC ")
        Dim returnStatus2 As String = ""
        Dim returnStatus As String = ""
        Dim showPopUp As Boolean = False



        'Dim sredirect As String = ""
        'sredirect = "~/moc/SaveMOC.aspx?MOCNumber=?" & currentMOC.MOCNumber.ToString & "&emailto=" & MyGlobalFullName
        'sredirect = "~/moc/SaveMOC.aspx?MOCNumber"

        'Response.Redirect(sredirect, False)
        'Exit Sub


        Try
            Me.Validate("EnterMOC")

            If Page.IsValid Then


                'JEB added 
                'assuming that there is already a moc record
                'this changed when superintendent was added
                'startmoc.aspx
                If Request.QueryString("MOCNumber") IsNot Nothing Then
                    If currentMOC Is Nothing Then
                        currentMOC = New clsCurrentMOC(Request.QueryString("MOCNumber"))
                        NewMOCFlag = "N"
                    End If
                Else
                    NewMOCFlag = "Y"
                    Exit Sub

                End If
                ''''''''''''''''''


                If currentMOC.Owner = "" Then
                    showPopUp = True
                Else
                    showPopUp = False
                End If
                Dim sendEmailFlag As Boolean = False


                With currentMOC
                    RI.SharedFunctions.InsertRILoggingRecord("EnterMOC.aspx.vb", " START - SaveMOC " & currentMOC.MOCNumber)
                    'Location
                    .SiteID = Me._ddlFacility.SelectedValue
                    .BusinessUnit = Me._ddlBusinessUnit.SelectedValue()
                    .Line = Me._ddlLineBreak.SelectedValue
                    .FunctionalLocation = ""  'RI.SharedFunctions.DataClean(Me._functionalLocationTree.Text)
                    .Owner = Me._ddlOwner.SelectedValue
                    .MOCType = Me._MOCType.Types
                    .EndDate = Me._tbExpirationDate.StartDate
                    .KickOffDate = "" 'Me._tbMOCProjectKickoffDate.SelectedDate
                    .Title = RI.SharedFunctions.DataClean(Me._txtTitle.Text)
                    .Description = RI.SharedFunctions.DataClean(Me._txtDescription.Text)
                    .Impact = Me._txtImpact.Text
                    .Funding = _ddlFunding.SelectedValue
                    .Classification = Me._MOCClass.Classification
                    .Category = RI.SharedFunctions.DataClean(Me._MOCCat.Category)
                    .SubCategory = RI.SharedFunctions.DataClean(Me._MOCCat.SubCategory)
                    .EquipSubCategory = RI.SharedFunctions.DataClean(Me._MOCCat.EquipSubCategory)
                    .MarketChannelSubCategory = RI.SharedFunctions.DataClean(Me._MOCCat.MarketChannelSubCategory)
                    .WorkOrder = Me._txtWorkOrder.Text
                    .UserName = MyGlobalUsername ' userProfile.Username

                    If Me._tbMOCImplementationDate.SelectedDate.ToString = "" Then
                        'do nothing
                    Else
                        .StartDate = Me._tbMOCImplementationDate.SelectedDate
                    End If
                    If Me._txtSavings.Text = "" Then
                        .Savings = 0
                    Else
                        .Savings = CLng(RI.SharedFunctions.DataClean(Me._txtSavings.Text, CStr(0)))
                    End If
                    If Me._tbCosts.Text = "" Then
                        .Costs = 0
                    Else
                        .Costs = CLng(RI.SharedFunctions.DataClean(Me._tbCosts.Text, CStr(0)))
                    End If


                    'Approval/Informed save
                    'Only Applicable for existing MOC
                    Dim approvalEmailList As String = ""
                    Dim ccEmailList As String = ""
                    Dim approvalEmailMsg As String = ""
                    Dim approvalEmailSubMsg As String = ""

                    If NewMOCFlag = "N" Then
                        returnStatus = ""

                        '1/10/2014 ALA - Changed to send email to all level approvers when MOC is approved at each
                        ' level.  If all levels have approved, email should go to everyone in the approver list included those informed.

                        '3/30/2017 ALA - If Only Informed MOC then email informed that MOC was created.


                        If Me._gvClassQuestions IsNot Nothing Then

                            Dim i As Integer = 0
                            Dim intRow As Integer

                            For i = 0 To Me._gvClassQuestions.DirtyRows.Count - 1
                                intRow = _gvClassQuestions.DirtyRows.Item(i).DataItemIndex
                                Dim strQuestionSeqid As String
                                strQuestionSeqid = Me._gvClassQuestions.DataKeys.Item(intRow).Value
                                Dim strAnswer As String
                                If Me._gvClassQuestions.Rows(intRow).FindControl("_cbAnswer").Visible = "true" Then
                                    strAnswer = CType(Me._gvClassQuestions.Rows(intRow).FindControl("_cbAnswer"), CheckBoxList).SelectedValue
                                Else
                                    strAnswer = CType(Me._gvClassQuestions.Rows(intRow).FindControl("_tbAnswer"), TextBox).Text
                                End If
                                currentMOC.SaveMOCClassQuestionBySeqId(strQuestionSeqid, currentMOC.UserName, "T", strAnswer)
                            Next

                        End If


                        If Me._gvCatQuestions IsNot Nothing Then

                            Dim i As Integer = 0
                            Dim intRow As Integer

                            For i = 0 To Me._gvCatQuestions.DirtyRows.Count - 1
                                intRow = _gvCatQuestions.DirtyRows.Item(i).DataItemIndex
                                Dim strQuestionSeqid As String
                                strQuestionSeqid = Me._gvCatQuestions.DataKeys.Item(intRow).Value
                                Dim strAnswer As String
                                If Me._gvCatQuestions.Rows(intRow).FindControl("_cbAnswer").Visible = "true" Then
                                    strAnswer = CType(Me._gvCatQuestions.Rows(intRow).FindControl("_cbAnswer"), CheckBoxList).SelectedValue
                                Else
                                    strAnswer = CType(Me._gvCatQuestions.Rows(intRow).FindControl("_tbAnswer"), TextBox).Text
                                End If
                                currentMOC.SaveMOCCatQuestionBySeqId(strQuestionSeqid, currentMOC.UserName, "T", strAnswer)
                            Next

                        End If

                        .MOCComment = Me.__txtCommentsNew.Text

                    End If



                    If currentMOC.Status = "Superintendent Approved" Then
                        'do nothing

                    Else

                        'Check status to see what emails (if any) need to be sent
                        If SavedMOCStatus = "INITIATE" Then
                            .Status = "INITIATE"
                            EmailMOC("NewMOC")
                        ElseIf SavedMOCStatus = "IMPLEMENT" Then
                            .Status = "Implemented"
                            'currentMOC.CreateMOCTemplateTasks()

                        ElseIf SavedMOCStatus = "MoveToPerm" Then
                            approvalEmailList = currentMOC.NotificationEList & "," & currentMOC.NotificationL1List & "," & currentMOC.NotificationL2List & "," & currentMOC.NotificationL3List
                            approvalEmailMsg = RI.SharedFunctions.LocalizeValue("Trial/Temporary MOC Moved To Permanent") & " - " & currentMOC.BusinessUnit & ": " & currentMOC.Title
                            EmailMOC("Approved", approvalEmailList, currentMOC.MOCCoordinatorEmail & "," & currentMOC.OwnerEmail, approvalEmailMsg)

                        Else
                            If MOCStatus.Status Is Nothing Then
                                .Status = "Draft"
                            Else
                                .Status = MOCStatus.Status
                            End If
                        End If

                    End If

                    'check to make sure that the MOC has just been approved by
                    'superentient and this is the first save after that

                    If currentMOC.NotificationL1FullName = "" And currentMOC.Status = "Superintendent Approved" Then
                        GetDefaultReviewers(currentMOC.MOCNumber)
                        'go mail to the default reviewers
                        returnStatus2 = .SaveMOC()
                        currentMOC.GetApprovalsList()
                        currentMOC = New clsCurrentMOC(.MOCNumber)
                        GetMOCSuperintendent(currentMOC.MOCNumber, currentMOC.SuperintendentType)



                        EmailMOCGPI()
                    Else
                        returnStatus2 = .SaveMOC()
                    End If


                    '.MOCNumber = .SaveMOC()


                    If returnStatus2 = "777" Then
                        EmailMOC("NewMOC", currentMOC.NotificationL1List & "," & currentMOC.NotificationL2List, ccEmailList, "MOC Moved from Draft/On-Hold to Approval Requested")
                    End If


                    'Current Approver
                    For Each row As GridViewRow In Me._gvApprovals.Rows

                        Dim approved As CheckBoxList = CType(row.FindControl("_cbApproval"), CheckBoxList)
                        If approved.Enabled = True Then
                            Dim comments As TextBox = CType(row.FindControl("_txtComments"), TextBox)
                            Dim Roles As Label = CType(row.FindControl("_lblRoleUserNames"), Label)
                            Dim currentUserEmail As String = clsCurrentMOC.GetUserEmail(currentMOC.UserName)
                            'Dim MOCOriginatorEmail As String = clsCurrentMOC.GetUserEmail(MOCInitiator)
                            'Dim MOCOriginatorEmail As String = clsCurrentMOC.GetUserEmail(currentMOC.MOCCoordinator)


                            Dim UniqueKey As String = _gvApprovals.DataKeys(row.RowIndex).Values(0).ToString

                            currentMOC.Comments = comments.Text
                            currentMOC.Approved = approved.SelectedValue
                            currentMOC.Roles = Roles.Text

                            If currentMOC.Approved <> "" Then
                                ccEmailList = currentMOC.MOCCoordinatorEmail & "," & currentMOC.OwnerEmail
                                returnStatus = currentMOC.SaveMOCApprovalBySeqId(Request.QueryString("MOCNumber"), MyGlobalUsername, "L1", Roles.ToString, UniqueKey)
                                If returnStatus = "999" Then
                                    ''Send email to MOC originator and all L1 approvers indicating that MOC has been rejected.
                                    'If this is an Outage MOC, also include message that Outage dates will not be changed.
                                    'EmailNotApproved(False, MOCOriginatorEmail, currentUserEmail, currentMOC.Comments, "L1")
                                    'EmailNotApproved(currentMOC.NotificationL1List, currentMOC.MOCCoordinatorEmail, currentMOC.Comments)
                                    If currentMOC.Classification = "Outage" Then
                                        approvalEmailSubMsg = "Outage Dates have not been changed."
                                    End If
                                    SendEmailToNotApproved(currentMOC.NotificationL1List, "Not Approved")

                                    SendEmailToProjectManger(currentMOC.OwnerEmail, "Approved", "N")


                                    'EmailMOC("NotApproved", currentMOC.NotificationL1List, ccEmailList, currentMOC.Comments, approvalEmailSubMsg)

                                ElseIf returnStatus = "888" Then
                                    'This indicates that all L1 approvers have approved the moc and an email should be sent to L2 approvers.
                                    'EmailMOC(approvalEmailList, userProfile.Email, approvalEmailMsg, approvalEmailSubMsg)
                                    'NO longer using any other approvers but L1
                                    'email to project manger now

                                    SendEmailToProjectManger(currentMOC.OwnerEmail, "Approved", "Y")

                                    'EmailMOC("Approved", approvalEmailList, ccEmailList, approvalEmailMsg, approvalEmailSubMsg)
                                End If
                            End If
                        End If
                    Next


                    'Loop thru system data list
                    Dim intRowsCount, intRowsLoop As Integer
                    Dim cbCurrent As CheckBoxList
                    Dim ddlFacility, ddlUsername, ddlPriority As DropDownList
                    Dim tbDaysAfter As TextBox
                    Dim lbCurrent As Label
                    Dim hdfTaskItem As HiddenField
                    Dim tbTaskTitle As TextBox

                    Dim strSystem, strUsername, strFacility As String
                    Dim strRole As String = ""
                    'Get the number of rows in the datalist
                    intRowsCount = _dlSystem.Items.Count() - 1

                    Dim SystemRecords As Boolean
                    Dim sb As New StringBuilder
                    For intRowsLoop = 0 To intRowsCount
                        'Get the current row's checkbox and label
                        lbCurrent = CType(_dlSystem.Items(intRowsLoop).FindControl("_lblSystemSeq"), Label)
                        cbCurrent = CType(_dlSystem.Items(intRowsLoop).FindControl("_cblSystem"), CheckBoxList)
                        strSystem = cbCurrent.SelectedValue
                        hdfTaskItem = CType(_dlSystem.Items(intRowsLoop).FindControl("_hdfTaskItem"), HiddenField)

                        If strSystem = "Y" And hdfTaskItem.Value = "" Then
                            ddlFacility = CType(_dlSystem.Items(intRowsLoop).FindControl("_ddlSystemFacility"), DropDownList)
                            ddlUsername = CType(_dlSystem.Items(intRowsLoop).FindControl("_ddlSystemPerson"), DropDownList)
                            strUsername = Request.Form(ddlUsername.UniqueID)
                            strFacility = Request.Form(ddlFacility.UniqueID)

                            If IsNumeric(Replace(strUsername, "/", "")) Then
                                strRole = Mid(strUsername, InStr(strUsername, "/") + 1)
                                strUsername = ""
                            End If
                            ddlPriority = CType(_dlSystem.Items(intRowsLoop).FindControl("_ddlPriority"), DropDownList)
                            tbDaysAfter = CType(_dlSystem.Items(intRowsLoop).FindControl("_txtDaysAfter"), TextBox)
                            tbTaskTitle = CType(_dlSystem.Items(intRowsLoop).FindControl("_tbSysTitle"), TextBox)

                            'Dim returnStatus2 As String = ""
                            returnStatus2 = currentMOC.SaveMOCSystem(.MOCNumber, lbCurrent.Text, strRole, strFacility, strUsername, ddlPriority.Text, tbDaysAfter.Text, tbTaskTitle.Text)
                            SystemRecords = True

                        ElseIf (strSystem = "N") Then
                            If Session("SystemChanged") = "Y" Then
                                'Dim returnStatus2 As String = ""
                                returnStatus2 = currentMOC.DeleteMOCSystem(.MOCNumber, lbCurrent.Text)
                            End If
                        End If

                    Next
                    Session("SystemChanged") = "N"

                    If currentMOC.Status = "Approved" Then
                        'currentMOC.CreateMOCTemplateTasks()
                        If currentMOC.USDTicket <> "Y" Then
                            Dim strSummary = currentMOC.SiteID & " - MOC " & currentMOC.MOCNumber & " " & Me._txtTitle.Text 'currentMOC.Title
                            Dim strDesc = currentMOC.SiteID & " - MOC " & currentMOC.MOCNumber & " initiated by " & currentMOC.InsertUsername & " http://ri/RI/MOC/entermoc.aspx?mocnumber=" & currentMOC.MOCNumber & " " & Me._txtTitle.Text
                            If RI.SharedFunctions.DataClean(currentMOC.SubCategory).Length > 0 Then

                                If currentMOC.SubCategory.Contains("PI") = True Or currentMOC.SubCategory.Contains("Proficy") Or currentMOC.SubCategory.Contains("PARCView") Then
                                    Me.CreateUSDTicket(currentMOC.SiteID, strSummary, strDesc, currentMOC.MOCCoordinator)
                                End If
                            End If

                        End If
                    End If

                    If showPopUp = True Then
                        'ToDo: JEB 
                        PopulateNotificationList(.MOCNumber)
                        '_btnCancel.Visible = False
                        '_mpeSwapList.Show()

                        'get approvers/reviewers
                        Dim RS As DataSet
                        RS = currentMOC.GetCurrentApproverListGPI()

                        RadGridAssignedApprover.DataSource = RS
                        RadGridAssignedApprover.DataBind()

                        'get approvers/reviewers
                        Dim RS1 As DataSet
                        RS1 = currentMOC.GetCurrentBUMApproverGPI()


                        Dim currentUserRS As DataSet
                        currentUserRS = currentMOC.GetCurrentApproverGPI()


                        Me._gvApprovals.DataSource = currentUserRS
                        Me._gvApprovals.DataBind()

                        _pnlApprovals.Visible = True
                        LabelApprovers.Visible = True

                    Else
                        currentMOC.CheckTemplateTasks()
                        If .MOCNumber.Length > 0 And refreshPage = True Then 'And .MOCNumber <> Request.QueryString("MOCNumber") Then
                            Response.Clear()
                            Response.Redirect(Page.AppRelativeVirtualPath & "?MOCNumber=" & .MOCNumber, False)
                        Else
                            currentMOC = New clsCurrentMOC(.MOCNumber)
                        End If

                    End If

                End With

            End If



            RI.SharedFunctions.InsertRILoggingRecord("EnterMOC.aspx.vb", " END - SaveMOC " & currentMOC.MOCNumber)



        Catch ex As Exception
            Throw New Exception("Error Saving Current MOC " & currentMOC.MOCNumber)
        End Try
    End Sub



    Protected Sub SendEmailToProjectManger(emailAddress As String, ByVal SubMsg As String, ByVal Status As String)

        Dim strHeader As String = ""
        Dim strSubject As String = ""
        Dim strSpanApproved As String = "<span style='color:black;font-weight:bolder;font-size:14px'>"
        Dim strSpanApprovedEnd As String = "</span>"
        Dim strSpanDenied As String = "<span style='color:red;font-weight:bolder;font-size:14px'>"
        Dim strSpanDeniedEnd As String = "</span>"


        Dim toPerson As String = emailAddress

        RI.SharedFunctions.InsertRILoggingRecord("EnterMOC.aspx.vb", " START SendEmailToProjectManger ")


        'strHeader = currentMOC.MOCNumber & " MOC has been approved"
        If Status = "Y" Then
            strHeader = "Your MOC has been " & strSpanApproved & "APPROVED" & strSpanApprovedEnd & " by all reviewers."
            strSubject = "MOC approved "
        Else
            strHeader = " MOC has " & strSpanDenied & "NOT" & strSpanDeniedEnd & " been approved by " & MyGlobalFullName
            strSubject = "MOC not approved "
        End If


        'strSubject = SubMsg & " " & currentMOC.BusinessUnit

        Dim sb As New StringBuilder



        'email to Superintendent reviewers
        Dim bsent As Boolean = clsCurrentMOC.PrepareL1Email(currentMOC.MOCNumber,
                                              currentMOC.Title,
                                              currentMOC.Description,
                                              MyGlobalFullName & " (" & MyGlobalEmail & ")",
                                              currentMOC.MOCType,
                                              currentMOC.Costs,
                                              currentMOC.Funding,
                                              currentMOC.Impact,
                                              currentMOC.NotificationL1FullName,
                                              currentMOC.NotificationEFullName,
                                              currentMOC.StartDate,
                                              currentMOC.Classification,
                                              currentMOC.Category,
                                              currentMOC.BusinessUnit,
                                              currentMOC.SuperintendentName,
                                              currentMOC.SuperintendentDate,
                                              currentMOC.SuperintendentComments,
                                              "MOC.Notification@graphicpkg.com",
                                              toPerson,
                                              strHeader,
                                              strSubject,
                                              "") ' Me._SuperintendentBusinessType.SelectedItem.Text)




        RI.SharedFunctions.InsertRILoggingRecord("EnterMOC.aspx.vb", " END SendEmailToProjectManger ")


    End Sub

    Protected Sub SendEmailToNotApproved(emailAddress As String, ByVal SubMsg As String)

        Dim strHeader As String = ""
        Dim strSubject As String = ""

        Dim toPerson As String = emailAddress

        RI.SharedFunctions.InsertRILoggingRecord("EnterMOC.aspx.vb", " START SendEmailToNotApproved ")


        strHeader = currentMOC.MOCNumber & " MOC has NOT been approved by " & MyGlobalFullName
        strSubject = "MOC " & currentMOC.MOCNumber & " " & SubMsg '& " " & currentMOC.BusinessUnit

        Dim sb As New StringBuilder



        'email to Superintendent reviewers
        Dim bsent As Boolean = clsCurrentMOC.PrepareL1Email(currentMOC.MOCNumber,
                                              currentMOC.Title,
                                              currentMOC.Description,
                                              MyGlobalFullName & " (" & MyGlobalEmail & ")",
                                              currentMOC.MOCType,
                                              currentMOC.Costs,
                                              currentMOC.Funding,
                                              currentMOC.Impact,
                                              currentMOC.NotificationL1FullName,
                                              currentMOC.NotificationEFullName,
                                              currentMOC.StartDate,
                                              currentMOC.Classification,
                                              currentMOC.Category,
                                              currentMOC.BusinessUnit,
                                              currentMOC.SuperintendentName,
                                              currentMOC.SuperintendentDate,
                                              currentMOC.SuperintendentComments,
                                              "MOC.Notification@graphicpkg.com",
                                              toPerson,
                                              strHeader,
                                              strSubject,
                                              "") ' Me._SuperintendentBusinessType.SelectedItem.Text)




        RI.SharedFunctions.InsertRILoggingRecord("EnterMOC.aspx.vb", " END SendEmailToProjectManger ")


    End Sub



    Protected Sub SendEmailToReviewer(emailAddress As String, Optional ByVal toL1List As String = "", Optional ByVal toEList As String = "", Optional ByVal toccList As String = "", Optional ByVal SubMsg As String = "")

        Dim strHeader As String = ""
        Dim strSubject As String = ""

        Dim toPerson As String = ""

        RI.SharedFunctions.InsertRILoggingRecord("EnterMOC.aspx.vb", " START SendEmailToReviewer ")


        strHeader = "You have been selected as an approver.  Please review this MOC and approve or reject."
        strSubject = SubMsg & currentMOC.BusinessUnit

        Dim sb As New StringBuilder
        Dim list As String() = toL1List.Split(CChar(","))


        For i As Integer = 0 To list.Length - 1
            If list.Length > 0 Then
                toPerson = list(i)


                'email to Superintendent reviewers
                Dim bsent As Boolean = clsCurrentMOC.PrepareL1Email(currentMOC.MOCNumber,
                                              currentMOC.Title,
                                              currentMOC.Description,
                                              MyGlobalFullName & " (" & MyGlobalEmail & ")",
                                              currentMOC.MOCType,
                                              currentMOC.Costs,
                                              currentMOC.Funding,
                                              currentMOC.Impact,
                                              currentMOC.NotificationL1FullName,
                                              currentMOC.NotificationEFullName,
                                              currentMOC.StartDate,
                                              currentMOC.Classification,
                                              currentMOC.Category,
                                              currentMOC.BusinessUnit,
                                              currentMOC.SuperintendentName,
                                              currentMOC.SuperintendentDate,
                                              currentMOC.SuperintendentComments,
                                              "MOC.Notification@graphicpkg.com",
                                              toPerson,
                                              strHeader,
                                              strSubject,
                                              "") ' Me._SuperintendentBusinessType.SelectedItem.Text)


            End If
        Next

        RI.SharedFunctions.InsertRILoggingRecord("EnterMOC.aspx.vb", " END SendEmailToReviewer ")


    End Sub


End Class




