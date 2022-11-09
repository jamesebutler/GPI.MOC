Imports Devart.Data.Oracle
Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.Drawing
Imports Telerik.Web.UI
Imports GPI.GlobalClass.GlobalVariables
Imports clsStartMOC
Imports GPI.MOC.Superintendent


'Imports SuperintendentClass




Partial Class MOC_StartMOC
    Inherits RIBasePage


    Dim enterMOC As clsEnterMOC
    Dim currentMOC As clsCurrentMOC
    Dim userProfile As RI.CurrentUserProfile = Nothing
    Dim selectedFacility As String = String.Empty
    Dim selectedBusArea As String = String.Empty
    Dim selectedLine As String = String.Empty
    Dim NewMOCFlag As String = String.Empty
    Dim AdminLevel As String
    Dim MOCInitiator As String = String.Empty
    Dim showMOC As Boolean = True
    Dim tracing As Boolean = ConfigurationManager.AppSettings("Tracing")
    Dim tracingFunctions As Boolean = ConfigurationManager.AppSettings("TracingFunctions")
    Dim logging As Boolean = ConfigurationManager.AppSettings("Logging")
    Dim testingemail As String = ConfigurationManager.AppSettings("TestingEmail")

    Dim CurrentApprover_UpdateCommand As Integer = 0
    Dim CurrentBUMApprover_UpdateCommand As Integer = 0

    Dim CurrentSuperintendentApprover As String = String.Empty
    Dim CurrentSuperintendentApproverSeqID As Integer
    Dim CurrentSuperintendentFlag As String = String.Empty
    Dim CurrentSuperintendentComments As String = String.Empty
    Dim CurrentSuperintendentApprovalDate As String = String.Empty
    Dim CurrentSuperintendentEmail As String = String.Empty
    Dim CurrentSuperintendentFullName As String = String.Empty
    Dim CurrentSuperintendentemaildate As String = String.Empty
    Dim confirmMessage As String = "localizedText.ConfirmRedirect"

    Dim SuperintendentComments As String = String.Empty




    Dim classtartmoc As New clsStartMOC()

    Dim superintendent As New SuperintendentClass()
    Dim ListOfSuperintendents As New List(Of SuperintendentClass)




    Private Sub MOC_StartMOC_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit

        Page.Theme = GPI.XML.XMLUtility.ReadXmlNode(Session("UserProfile").ToString, "PageTheme")
        Master.SetBanner("MOC Idea Submittal Form", True)
        ''Master.SetBanner("IDEA form to be approved by BUM", True, "MOC")



    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init



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



    End Sub

    Private Sub MOC_StartMOC_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender

        Try


            '_tbMOCProjectKickoffDate.MinDate = Date.Now.AddDays(-120)
            '_tbMOCProjectKickoffDate.MaxDate = Date.Today


        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try

    End Sub


    Private Sub _btnSubmit_Click(sender As Object, e As EventArgs) Handles _btnSubmit.Click
        RI.SharedFunctions.InsertRILoggingRecord("StartMOC.aspx.vb", " _btnSubmit_Click ")


        If NewMOCFlag = "Y" Then
            SaveMOC(True)
        ElseIf NewMOCFlag = "N" And _btnSubmit.Text = "Update MOC" Then
            UpdateStartMOC(False)
        Else
            UpdateStartMOC(True)
        End If
        'End If
    End Sub

    Private Sub _btnDenied_Click(sender As Object, e As EventArgs) Handles _btnDenied.Click
        RI.SharedFunctions.InsertRILoggingRecord("StartMOC.aspx.vb", " _btnDenied_Click ")

        Me.Validate("StartMOC")


        'If Page.IsValid Then

        NewMOCFlag = ""
            Dim strReturn = clsCurrentMOC.SaveSuperintendentComments(CurrentSuperintendentApproverSeqID, currentMOC.MOCNumber, "N", SuperintendentComments, CurrentSuperintendentApprover)

            SendEmailToCreator("N")

            Dim sredirect As String
            sredirect = "~/moc/emailsent.aspx?MOCNumber=?" & currentMOC.MOCNumber.ToString & "&emailto=" & MyGlobalFullName
            Response.Redirect(sredirect, False)

        'End If


    End Sub

    Private Sub _btnApprove_Click(sender As Object, e As EventArgs) Handles _btnApprove.Click
        RI.SharedFunctions.InsertRILoggingRecord("StartMOC.aspx.vb", " _btnApprove_Click ")

        Me.Validate("StartMOC")


        NewMOCFlag = ""
            Dim strReturn = clsCurrentMOC.SaveSuperintendentComments(CurrentSuperintendentApproverSeqID, currentMOC.MOCNumber, "Y", SuperintendentComments, CurrentSuperintendentApprover)

            SendEmailToCreator("Y")

            Dim sredirect As String
            sredirect = "~/moc/emailsent.aspx?MOCNumber=?" & currentMOC.MOCNumber.ToString & "&emailto=" & MyGlobalFullName
            Response.Redirect(sredirect, False)



    End Sub

    Protected Sub SendEmailToSuperintendent(status As String, CurrentSuperintendentEmail As String, CurrentSuperintendentFullName As String)

        Dim strHeader As String = ""
        Dim strSubject As String = ""


        If status = "True" Then
            strHeader = "You have been assigned as a Superintendent approver. <br />Please review this MOC and approve or deny."
            strSubject = "New MOC initiated: "
        Else
            strHeader = "This MOC has been resubmitted for you to review again.  You are the Superintendent approver. <br />Please review this MOC and approve or deny."
            strSubject = "MOC resubmitted: "
        End If

        'Superintendent email

        'email to Superintendent reviewers
        Dim bsent As Boolean = classtartmoc.PrepareSuperintendentEmail(currentMOC.MOCNumber,
                                              currentMOC.Title,
                                              currentMOC.Description,
                                              MyGlobalFullName & " (" & MyGlobalEmail & ")",
                                              currentMOC.MOCType,
                                              currentMOC.Costs,
                                              currentMOC.Funding,
                                              currentMOC.Impact,
                                              "MOC.Notification@graphicpkg.com",
                                              CurrentSuperintendentEmail,
                                              strHeader,
                                              strSubject,
                                              Me._SuperintendentBusinessType.SelectedItem.Text,
                                                Me._txtSuperintendentCommentsComments.Text)



    End Sub


    Protected Sub SendEmailToCreator(status As String)

        Dim strHeader As String = ""
        Dim strSubject As String = ""
        Dim strSpanApproved As String = "<span style='color:black;font-weight:bolder;font-size:14px'>"
        Dim strSpanApprovedEnd As String = "</span>"
        Dim strSpanDenied As String = "<span style='color:red;font-weight:bolder;font-size:14px'>"
        Dim strSpanDeniedEnd As String = "</span>"
        Dim hasApproval As Boolean = False


        If status = "Y" Then
            hasApproval = True
            strHeader = "Your MOC has been " & strSpanApproved & "APPROVED" & strSpanApprovedEnd & " by the Superintendent."
            strSubject = "MOC approved "
        Else
            strHeader = "Your MOC has been " & strSpanDenied & "DENIED" & strSpanDeniedEnd & " by the Superintendent."
            strSubject = "MOC DENIED "
        End If
        'email to Superintendent reviewers
        Dim bsent As Boolean = classtartmoc.PrepareCreatorEmail(currentMOC.MOCNumber,
                                              currentMOC.Title,
                                              currentMOC.Description,
                                              MyGlobalFullName & " (" & MyGlobalEmail & ")",
                                              currentMOC.MOCType,
                                              currentMOC.Costs,
                                              currentMOC.Funding,
                                              currentMOC.Impact,
                                              "MOC.Notification@graphicpkg.com",
                                              MyGlobalEmail,
                                              strHeader,
                                              strSubject,
                                               Me._SuperintendentBusinessType.SelectedItem.Text,
                                                SuperintendentComments,
                                                hasApproval)




    End Sub

    Protected Sub SaveMOC(Optional ByVal refreshPage As Boolean = True, Optional ByVal SavedMOCStatus As String = "")
        RI.SharedFunctions.InsertRILoggingRecord("StartMOC.aspx.vb", " START - SaveMOC ")
        Dim returnStatus2 As String = ""

        'ListOfSuperintendents
        'go check to see a Superintendent has been setup for the mill
        If CheckforSupertendentApprovers(Me._ddlFacility.SelectedValue, Me._SuperintendentBusinessType.SelectedValue) = True Then
            'continue
        Else
            _lblStatusLine.Text = Me._ddlFacility.SelectedItem.Text & " does Not have a Superintent seup"
            Exit Sub
        End If

        Try
            Me.Validate("StartMOC")



            If Page.IsValid Then
                If currentMOC Is Nothing Then
                    currentMOC = New clsCurrentMOC()
                End If

                Dim sendEmailFlag As Boolean = False

                'If Request.QueryString("MOCNumber") IsNot Nothing Then
                '    currentMOC.MOCNumber = Request.QueryString("MOCNumber")
                'End If


                With currentMOC
                    RI.SharedFunctions.InsertRILoggingRecord("EnterMOC.aspx.vb", " START - SaveMOC " & currentMOC.MOCNumber)
                    'Location
                    .SiteID = Me._ddlFacility.SelectedValue
                    .BusinessUnit = "" 'Me._ddlBusinessUnit.SelectedValue()
                    .Line = "" 'Me._ddlLineBreak.SelectedValue
                    .FunctionalLocation = ""  'RI.SharedFunctions.DataClean(Me._functionalLocationTree.Text)
                    .Owner = ""  'Me._ddlOwner.SelectedValue
                    .MOCType = Me._MOCType.Types

                    .StartDate = ""

                    .EndDate = Me._tbExpirationDate.StartDate

                    .KickOffDate = "" 'Me._tbMOCProjectKickoffDate.SelectedDate

                    'JEB
                    '.KickOffDate = Me._tbMOCProjectKickoffDate.StartDate
                    '.EndDate = Me._tbExpirationDate.Text

                    .Title = RI.SharedFunctions.DataClean(Me._txtTitle.Text)
                    .Description = RI.SharedFunctions.DataClean(Me._txtDescription.Text)
                    .Impact = Me._txtImpact.Text


                    .Status = "Superintendent Requested"
                    .Savings = 0


                    If Me._tbCosts.Text = "" Then
                        .Costs = 0
                    Else
                        .Costs = CLng(RI.SharedFunctions.DataClean(Me._tbCosts.Text, CStr(0)))
                    End If

                    .Funding = _ddlFunding.SelectedValue

                    .Classification = ""
                    .Category = ""
                    .SubCategory = ""
                    .EquipSubCategory = ""
                    .MarketChannelSubCategory = ""


                    .WorkOrder = ""
                    .UserName = MyGlobalUsername ' userProfile.Username
                    .SuperintendentType = Me._SuperintendentBusinessType.SelectedValue
                    'Approval/Informed save
                    'Only Applicable for existing MOC
                    Dim approvalEmailList As String = ""
                    Dim ccEmailList As String = ""
                    Dim approvalEmailMsg As String = ""
                    Dim approvalEmailSubMsg As String = ""
                    If NewMOCFlag = "N" Then
                        Dim returnStatus As String = ""
                    End If

                    returnStatus2 = .SaveMOC()

                    'run through and find the reviewer
                    If returnStatus2 = "0" Then
                        'assign superintendent reviewers to MOC
                        Dim bretrun As String = ""
                        Dim SuperintendentEmail As String = ""
                        Dim SuperintendentFullName As String = ""
                        Dim SuperintendentApproverSeqID As String = ""
                        Dim listCount As Integer
                        For listCount = 0 To ListOfSuperintendents.Count - 1
                            If ListOfSuperintendents(listCount).notifytype = _SuperintendentBusinessType.SelectedValue And ListOfSuperintendents(listCount).siteid = currentMOC.SiteID Then
                                bretrun = currentMOC.InsertMOCApprovalSuperintendent(currentMOC.MOCNumber.ToString, ListOfSuperintendents(listCount).username.ToString, ListOfSuperintendents(listCount).notifytype.ToString, "Y")
                                SuperintendentEmail = ListOfSuperintendents(listCount).email
                                SuperintendentFullName = ListOfSuperintendents(listCount).fullname
                                SuperintendentApproverSeqID = ListOfSuperintendents(listCount).notify_seqid
                                Exit For
                            End If
                        Next


                        'Dim bretrun As Boolean = currentMOC.GetDefaultSuperintendentList(currentMOC.MOCNumber,
                        'currentMOC.SiteID,
                        '_SuperintendentBusinessType.SelectedValue)

                        If bretrun = "0" Then
                            'need to get superintendent email
                            'GetSuperintendent(currentMOC.MOCNumber, currentMOC.SuperintendentType)

                            If refreshPage = True Then

                                'SendEmailToSuperintendent("True", CurrentSuperintendentEmail, CurrentSuperintendentFullName)
                                SendEmailToSuperintendent("True", SuperintendentEmail, SuperintendentFullName)



                                'update superintendent that an email was sent
                                'classtartmoc.UpdateEmailDate(CurrentSuperintendentApproverSeqID, currentMOC.MOCNumber)
                                classtartmoc.UpdateEmailDate(currentMOC.OutNumber, currentMOC.MOCNumber)



                            End If
                        Else
                            Throw New Exception("Error Assiging Superintendent Reviewer - StartMOC.aspx.vb " & currentMOC.MOCNumber)

                        End If

                    End If


                    'Dim sredirect As String
                    'sredirect = "~/moc/emailsent.aspx?MOCNumber=?" & currentMOC.MOCNumber.ToString & "&emailto=" & Me._SuperintendentBusinessType.SelectedItem.Text & " Superintendent (" & CurrentSuperintendentFullName & ")"
                    'Response.Redirect(sredirect, False)

                    LabelEmailSent.Text = "Email has been sent To " & Me._SuperintendentBusinessType.SelectedItem.Text & " Superintendent (" & CurrentSuperintendentFullName & ")"

                    Session("SystemChanged") = "N"
                    Response.Redirect(Page.AppRelativeVirtualPath & "?MOCNumber=" & .MOCNumber, False)


                End With

            End If
            RI.SharedFunctions.InsertRILoggingRecord("EnterMOC.aspx.vb", " End - SaveMOC " & currentMOC.MOCNumber)

        Catch ex As Exception
            Throw New Exception("Error Saving Current MOC " & currentMOC.MOCNumber)
        End Try
    End Sub

    Protected Sub UpdateStartMOC(Optional ByVal refreshPage As Boolean = True, Optional ByVal SavedMOCStatus As String = "")
        RI.SharedFunctions.InsertRILoggingRecord("StartMOC.aspx.vb", " START - UpdateStartMOC ")
        Dim returnStatus2 As String = ""
        Try
            Me.Validate("StartMOC")



            If Page.IsValid Then
                If currentMOC Is Nothing Then
                    currentMOC = New clsCurrentMOC()
                End If

                Dim sendEmailFlag As Boolean = False

                With currentMOC
                    RI.SharedFunctions.InsertRILoggingRecord("StartMOC.aspx.vb", " START - UpdateStartMOC " & currentMOC.MOCNumber)
                    'Location
                    .SiteID = Me._ddlFacility.SelectedValue
                    .MOCType = Me._MOCType.Types

                    .StartDate = ""

                    .EndDate = Me._tbExpirationDate.StartDate

                    .Title = RI.SharedFunctions.DataClean(Me._txtTitle.Text)
                    .Description = RI.SharedFunctions.DataClean(Me._txtDescription.Text)
                    .Impact = Me._txtImpact.Text
                    If Me._tbCosts.Text = "" Then
                        .Costs = 0
                    Else
                        .Costs = CLng(RI.SharedFunctions.DataClean(Me._tbCosts.Text, CStr(0)))
                    End If

                    .Status = "Superintendent Requested"

                    .Funding = _ddlFunding.SelectedValue

                    .UserName = MyGlobalUsername ' userProfile.Username
                    .SuperintendentType = Me._SuperintendentBusinessType.SelectedValue
                    'Approval/Informed save
                    'Only Applicable for existing MOC
                    Dim approvalEmailList As String = ""
                    Dim ccEmailList As String = ""
                    Dim approvalEmailMsg As String = ""
                    Dim approvalEmailSubMsg As String = ""
                    If NewMOCFlag = "N" Then
                        Dim returnStatus As String = ""
                    End If

                    returnStatus2 = .UpdateStartMOC()

                    If returnStatus2 = "0" Then

                        'get the email of the assigned superintendent.
                        'need to get superintendent email
                        GetSuperintendent(currentMOC.MOCNumber, currentMOC.SuperintendentType)

                        'email to Superintendent only if refreshPage is true
                        If refreshPage = True Then

                            SendEmailToSuperintendent("False", CurrentSuperintendentEmail, CurrentSuperintendentFullName)
                            classtartmoc.UpdateEmailDate(CurrentSuperintendentApproverSeqID, currentMOC.MOCNumber)

                        End If
                    End If

                    Session("SystemChanged") = "N"

                    'Dim sredirect As String
                    'sredirect = "~/moc/emailsent.aspx?MOCNumber=?" & currentMOC.MOCNumber.ToString & "&emailto=" & CurrentSuperintendentFullName
                    'Response.Redirect(sredirect, False)


                    Response.Redirect(Page.AppRelativeVirtualPath & "?MOCNumber=" & .MOCNumber, False)


                End With

            End If
            RI.SharedFunctions.InsertRILoggingRecord("StartMOC.aspx.vb", " End - UpdateStartMOC " & currentMOC.MOCNumber)

        Catch ex As Exception
            Throw New Exception("Error Saving Current MOC " & currentMOC.MOCNumber)
        End Try
    End Sub




    Private Sub MOC_StartMOC_Load(sender As Object, e As EventArgs) Handles Me.Load



        ScriptManager.RegisterClientScriptInclude(Me._udpLocation, _udpLocation.GetType, "StartMOC", Page.ResolveClientUrl("~/moc/EnterMOC.js?v=1"))

            Dim popupJS As String = "Javascript:displayModalPopUpWindow('{0}','{1}','{2}','{3}','{4}');"



            'go get the superintendents
            '
            Dim Superintendents As DataSet = superintendent.GetAllSuperintendents()

            ListOfSuperintendents = superintendent.GetSuperintendentsList(Superintendents)




        If Me._SuperintendentBusinessType.Items.Count = 4 Then
            'do nothing
        Else
            Dim item As New ListItem("Mill Wide", "S4")
            Me._SuperintendentBusinessType.Items.Add(item)
            item.Enabled = False

            item = New ListItem("Paper", "S3")
            Me._SuperintendentBusinessType.Items.Add(item)
            item.Enabled = False

            item = New ListItem("Power", "S1")
            Me._SuperintendentBusinessType.Items.Add(item)
            item.Enabled = False

            item = New ListItem("Pulp", "S2")
            Me._SuperintendentBusinessType.Items.Add(item)
            item.Enabled = False
        End If


        'now loop and enabled the Superintendent types.
        Dim listCountSuperintendent As Integer
        Dim listCount As Integer
        For listCountSuperintendent = 0 To ListOfSuperintendents.Count - 1
            If ListOfSuperintendents(listCountSuperintendent).siteid = MyGlobalDefaultFacility Then
                For listCount = 0 To _SuperintendentBusinessType.Items.Count - 1
                    If _SuperintendentBusinessType.Items(listCount).Value = ListOfSuperintendents(listCountSuperintendent).notifytype Then
                        _SuperintendentBusinessType.Items(listCount).Enabled = True
                    End If

                Next

            End If
        Next





        'get all superintendents
        'Dim ds As DataSet = superintendent.GetAllSuperintendentBySiteID(MyGlobalDefaultFacility)

        '    If ds IsNot Nothing Then
        '        Dim drSuperintendent As DataTableReader = ds.Tables(0).CreateDataReader
        '        If drSuperintendent IsNot Nothing Then
        '            Do While drSuperintendent.Read

        '                If drSuperintendent.Item("notifytype") IsNot DBNull.Value Then
        '                    'loop and enable superintendents that has been setup.
        '                    For listCount = 0 To _SuperintendentBusinessType.Items.Count - 1
        '                        If _SuperintendentBusinessType.Items(listCount).Value = drSuperintendent.Item("notifytype").ToString Then
        '                            _SuperintendentBusinessType.Items(listCount).Enabled = True
        '                        End If

        '                    Next
        '                End If



        '            Loop
        '        End If
        '    End If


        'now loop and see what is not enabled.
        Dim icountNotSetup = 0
        Dim SuperintendentNameMissingText As String = ""
        Dim htmlReturn = "<br />"
        For listCount = 0 To _SuperintendentBusinessType.Items.Count - 1
                If _SuperintendentBusinessType.Items(listCount).Enabled = False Then

                SuperintendentNameMissingText = SuperintendentNameMissingText & htmlReturn & _SuperintendentBusinessType.Items(listCount).Text & " superintnedent has not been setup. "
                icountNotSetup = icountNotSetup + 1
                End If
            Next

            If SuperintendentNameMissingText.Length > 1 Then
            SuperintendentNameMissingText = SuperintendentNameMissingText & htmlReturn & " Click <a href='MillAdministrators.aspx' target='_blank'>here</a> for Mill administrator." & htmlReturn & htmlReturn
            'go email the mill administartors.
        End If

        'if No Superintendents have been setup - disable buttons
        If icountNotSetup = 4 Then
            'disable start button
            _btnSubmit.Visible = False
            _btnSpell.Visible = False
            SuperintendentNameMissingText = ""
            _lblStatusLine.Text = "No Mill Superintendents have been setup.  Click <a href='MillAdministrators.aspx' target='_blank'>here</a> to contact the Mill Administrators."
            'Exit Sub    'exit
        End If

        'display on the page
        SuperintendentNameMissing.Text = SuperintendentNameMissingText


            If _txtSuperintendentComments.Text <> "" Then
                SuperintendentComments = _txtSuperintendentComments.Text
            End If


            ' Dim AuthLevel As String = RI.SharedFunctions.GetAuthLevelAdmin(MyGlobalUsername, MyGlobalDefaultFacility)
            AdminLevel = MyGlobalAuthLevel

            If Request.QueryString("MOCNumber") IsNot Nothing Then

                NewMOCFlag = "N"
                currentMOC = New clsCurrentMOC(Request.QueryString("MOCNumber"))

                GetMOC()

                GetSuperintendent(currentMOC.MOCNumber, currentMOC.SuperintendentType)

                _btnSubmit.Text = "Update MOC"

                'do not allow the supertindent to change after 
                'while waiting review from superintendent

                _SuperintendentBusinessType.Items(0).Enabled = False
                _SuperintendentBusinessType.Items(1).Enabled = False
                _SuperintendentBusinessType.Items(2).Enabled = False
                _SuperintendentBusinessType.Items(3).Enabled = False


                'check to see if this is the Superintendent if so
                'show and hide buttons
                If CurrentSuperintendentApprover = MyGlobalUsername Then

                    _rfvSuperintendentComments.Enabled = True

                    _btnApprove.Visible = True
                    _btnDenied.Visible = True

                    _btnSubmit.Visible = False
                    _btnSpell.Visible = False

                    _btnAttachmentSuperintendent.Visible = True

                    _btnAttachment.Visible = False

                    _tblSuperintendent.Visible = True

                    _lblSuperintendentComments.Text = "Superintendent Approver - " & CurrentSuperintendentApprover

                    _SuperintendentBusinessType.Items(0).Enabled = False
                    _SuperintendentBusinessType.Items(1).Enabled = False
                    _SuperintendentBusinessType.Items(2).Enabled = False
                    _SuperintendentBusinessType.Items(3).Enabled = False


                Else

                    If CurrentSuperintendentFlag = "N" Then
                        _lblStatusLine.Text = "NOT APPROVED by " & CurrentSuperintendentApprover
                        _SuperintendentBusinessType.Items(0).Enabled = False
                        _SuperintendentBusinessType.Items(1).Enabled = False
                        _SuperintendentBusinessType.Items(2).Enabled = False
                        _SuperintendentBusinessType.Items(3).Enabled = False
                        _btnSubmit.Text = "Update and Resubmit to Superintendent"
                        _txtSuperintendentCommentsComments.Text = CurrentSuperintendentComments
                        _tblSuperintendentComments.Visible = True
                    Else
                        If currentMOC.Status = "Superintendent Requested" Then
                            _lblStatusLine.Text = "Waiting for Superintendent Review"
                        End If
                    End If

                    _btnSubmit.Visible = True
                    _btnSpell.Visible = True
                    _btnAttachment.Visible = True

                    _btnApprove.Visible = False
                    _btnDenied.Visible = False
                    _btnAttachmentSuperintendent.Visible = False
                    _tblSuperintendent.Visible = False

                End If


            Else
                NewMOCFlag = "Y"

                Me._cddlFacility.SelectedValue = MyGlobalDefaultFacility
                selectedFacility = MyGlobalDefaultFacility         '

            End If

    End Sub

    Private Sub InsertDefaultReviewers()

        Dim bretrun As Boolean = classtartmoc.GetDefaultReviewerList(currentMOC.MOCNumber,
                                        currentMOC.SiteID,
                                        currentMOC.BusinessUnit,
                                        currentMOC.Line,
                                        currentMOC.Classification,
                                        currentMOC.Category,
                                        currentMOC.SubCategory,
                                        currentMOC.EquipSubCategory)

    End Sub



    Private Sub GetSuperintendent(in_MOCNumber As Integer, in_SuperintendentType As String)

        Dim dr As Data.DataTableReader = Nothing
        Dim dt As New DataTable
        Dim ds As DataSet = clsCurrentMOC.GetCurrentSuperintendentApproverGPI(in_MOCNumber, in_SuperintendentType)


        dr = ds.Tables(0).CreateDataReader

        If dr IsNot Nothing Then
            Do While dr.Read
                If dr.Item("username").ToString <> "" Then
                    CurrentSuperintendentApprover = dr.Item("username").ToString
                    CurrentSuperintendentApproverSeqID = dr.Item("approval_seqid")
                    CurrentSuperintendentFlag = dr.Item("approval_flag").ToString
                    CurrentSuperintendentComments = dr.Item("comments").ToString
                    CurrentSuperintendentApprovalDate = dr.Item("approval_date").ToString
                    CurrentSuperintendentEmail = dr.Item("email").ToString
                    CurrentSuperintendentFullName = dr.Item("fullname").ToString
                    CurrentSuperintendentemaildate = dr.Item("emaildate").ToString
                End If

            Loop

            _txtSuperintendentComments.Text = CurrentSuperintendentComments
            _txtSuperintendentComments.Visible = True
        End If



    End Sub





    Private Sub GetMOC()


        If (Not Page.IsPostBack) Then

            If currentMOC.UserName = "" Then

                Master.SetBanner("MOC Idea submittal Form", True)

            Else
                With currentMOC
                    RI.SharedFunctions.InsertRILoggingRecord("EnterMOC.aspx.vb", " START - SaveMOC " & currentMOC.MOCNumber)
                    'Location

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
                        'Me._tbMOCProjectKickoffDate.SelectedDate = .KickOffDate
                    End If

                    Me._tbExpirationDate.StartDate = .EndDate
                    '

                    Me._txtTitle.Text = .Title
                    'Me._lblStatusText.Text = .Status
                    Me._txtDescription.Text = .Description
                    '

                    If .Savings = "" Or .Savings Is Nothing Then
                        .Savings = 0
                    End If
                    ''''Me._txtSavings.Text = Mid(String.Format("{0:c}", FormatCurrency(.Savings, 0)), 1)
                    If .Costs = "" Or .Costs Is Nothing Then
                        .Costs = 0
                    End If
                    Me._tbCosts.Text = Mid(String.Format("{0:c}", FormatCurrency(.Costs, 0)), 1)
                    Me._txtImpact.Text = .Impact
                    Me._btnAttachment.Text = Master.RIRESOURCES.GetResourceValue("Attachments", True, "Shared") & " (" & CStr(currentMOC.AttachmentCount) & ")"
                    Me._btnAttachmentSuperintendent.Text = Master.RIRESOURCES.GetResourceValue("Attachments", True, "Shared") & " (" & CStr(currentMOC.AttachmentCount) & ")"



                    Select Case .SuperintendentType.ToString
                        Case "S4"
                            _SuperintendentBusinessType.SelectedIndex = 0
                        Case "S3"
                            _SuperintendentBusinessType.SelectedIndex = 1
                        Case "S1"
                            _SuperintendentBusinessType.SelectedIndex = 2
                        Case "S2"
                            _SuperintendentBusinessType.SelectedIndex = 3
                    End Select


                    Me._ddlFunding.SelectedValue = .Funding

                    Master.SetBanner("MOC Idea submittal Form - " & currentMOC.MOCNumber, True)


                    Me._btnAttachment.OnClientClick = "Javascript:viewPopUp('FileUpload.aspx?MOCNumber=" & currentMOC.MOCNumber & "'," & confirmMessage & ",'fu');return false"
                    Me._btnAttachmentSuperintendent.OnClientClick = "Javascript:viewPopUp('FileUpload.aspx?MOCNumber=" & currentMOC.MOCNumber & "'," & confirmMessage & ",'fu');return false"


                End With

            End If

        End If


        RI.SharedFunctions.InsertRILoggingRecord("StartMOC.aspx.vb", " END GetMOC ")


    End Sub

    Public ReadOnly Property SuperintendentScores() As DataTable
        Get
            Dim dr As Data.DataTableReader = Nothing
            Dim dt As New DataTable
            Dim obj As Object = Me.Session("ApproverScores")
            'If (Not obj Is Nothing) Then
            'Return CType(obj, DataTable)
            'End If

            Dim myDataSet As DataSet = New DataSet
            myDataSet = currentMOC.GetCurrentBUMApproverGPI()

            dr = myDataSet.Tables(0).CreateDataReader


            'If dr IsNot Nothing Then
            '    Do While dr.Read
            '        Dim spaceChar As String = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"

            '        If dr.Item("username") Then
            '        End If

            '    Loop

            'End If

            dt.TableName = "RESULTS"
            dt.Load(dr)

            'Me.Session("ApproverScores") = dt
            Return dt
        End Get
    End Property

    Private Function CheckforSupertendentApprovers(ByVal siteid As String, ByVal suptType As String) As Boolean

        'now loop and see if there a Superintendent by associated by types.
        Dim listCountSuperintendent As Integer
        CheckforSupertendentApprovers = False
        For listCountSuperintendent = 0 To ListOfSuperintendents.Count - 1
            If (ListOfSuperintendents(listCountSuperintendent).siteid = siteid And ListOfSuperintendents(listCountSuperintendent).notifytype = suptType) Then
                CheckforSupertendentApprovers = True
                Exit For
            End If
        Next
        Return CheckforSupertendentApprovers

    End Function

End Class
