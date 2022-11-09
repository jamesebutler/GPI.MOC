Imports System.Data
Imports Devart.Data.Oracle
Imports GPI.GlobalClass.GlobalVariables
Partial Class MOC_DataMaintenance_MOCSuperintendentApprovers
    Inherits RIBasePage

    Dim userProfile As RI.CurrentUserProfile = Nothing


    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init


        Master.SetBanner("Superintendent Approvers", True)


        _lblMainHeading.Text = RI.SharedFunctions.LocalizeValue("The page is used to select which Superintendent approvers will show up when an MOC is initiated.")

        '_lblHeading.Text = RI.SharedFunctions.LocalizeValue("Default Approver Maintenance for ")


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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            'userProfile = RI.SharedFunctions.GetUserProfile

            Dim sc As ScriptManager
            sc = CType(Page.Form.FindControl("_scriptManager"), ScriptManager)
            If sc IsNot Nothing Then
                Dim SiteService As New ServiceReference
                SiteService.InlineScript = False
                SiteService.Path = "~/RIMOCSharedWS.asmx"
                sc.Services.Add(SiteService)
            End If

            If Not Page.IsPostBack Then
                _cddlFacility.SelectedValue = MyGlobalDefaultFacility 'userProfile.DefaultFacility
                _cddlFacility1.SelectedValue = MyGlobalDefaultFacility 'userProfile.DefaultFacility

                '_cddlBusUNit.SelectedValue = "All"
                '_cddlArea.SelectedValue = "All"
                '_cddlLineBreak.SelectedValue = "All"

                If _rblMaintType.SelectedValue = "Business Unit" Then
                    PopulateSuperintendentNotificationList(MyGlobalDefaultFacility)

                    ' PopulateNotificationList(MyGlobalDefaultFacility)  'userProfile.DefaultFacility
                End If


            Else
                _cddlFacility.SelectedValue = Me._ddlFacility2.SelectedValue
                'PopulateClassCat()
                '_ddlFacility.SelectedValue = userProfile.DefaultFacility
                If _rblMaintType.SelectedValue = "Classification" Then
                    GetClassData()
                    'PopulateFacility()
                ElseIf _rblMaintType.SelectedValue = "Category" Then


                    Me._tcBusinessUnit.Visible = False
                    Me._tcArea.Visible = False
                    Me._tcLine.Visible = False
                Else
                    'PopulateNotificationList(Me._ddlFacility2.SelectedValue)
                    PopulateSuperintendentNotificationList(Me._ddlFacility2.SelectedValue)
                End If
            End If

            Dim authlevel As String
            authlevel = GetAuthLevel(MyGlobalUsername, Me._cddlFacility.SelectedValue)
            'authlevel = GetAuthLevel("YBROOKS", Me._ddlFacility.SelectedValue)
            If authlevel = "MILLADMIN" Then
                Me._btnAdd.Visible = True
                Me._tblApprover.Visible = True


                Me._gvL1.Columns(3).Visible = True


            Else
                Me._btnAdd.Visible = False
                Me._tblApprover.Visible = False


                Me._gvL1.Columns(3).Visible = False


            End If

        Catch ex As Exception
            Throw New Data.DataException("Page Load", ex)
        End Try
    End Sub

    Protected Sub _btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnAdd.Click

        UpdateBUANotification()
        _ddlPeople.Items.Clear()
        'PopulateNotificationList(Me._ddlFacility2.SelectedValue)
        PopulateSuperintendentNotificationList(Me._ddlFacility2.SelectedValue)
    End Sub


    Public Function GetAuthLevel(ByVal user As String, ByVal site As String) As String
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim ret As String = String.Empty
        'Check input paramaters
        Try
            param = New OracleParameter
            param.ParameterName = "in_username"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = user
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_siteid"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = site
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsAuthLevel"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "GetAuthLevel_" & user
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.newmoc.GetAuthLevel", key, 0)

            If ds IsNot Nothing Then
                If ds.Tables.Count = 1 Then
                    Dim dr As Data.DataTableReader = ds.Tables(0).CreateDataReader
                    If dr IsNot Nothing Then
                        If dr.HasRows Then
                            dr.Read()
                            With Me
                                ret = RI.SharedFunctions.DataClean(dr.Item("AuthLevel"))
                            End With
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            Throw
        Finally
            If ds IsNot Nothing Then
                ds = Nothing
            End If
            GetAuthLevel = ret
        End Try
    End Function



    Protected Sub _ddlApproval_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ddlApproval.SelectedIndexChanged

        Dim i As Int16
        i = 1 + 3





    End Sub




    Protected Sub _gvClass_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles _gvClass.RowDeleting
        Dim dr As OracleDataReader = Nothing
        Dim status As String
        Dim paramCollection As New OracleParameterCollection

        Try

            Dim param As New OracleParameter
            param = New OracleParameter
            param.ParameterName = "in_ClassNotify_SeqId"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Me._gvClass.DataKeys.Item(e.RowIndex).Value
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            status = RI.SharedFunctions.CallDROraclePackage(paramCollection, "MOCMaint.DeleteMOCClassNotification")
            If status <> 0 Then
                Throw New Data.DataException("DeleteMOCClassNotification Oracle Error:" & status)
            End If

            GetClassData()

        Catch ex As Exception
            Throw New Data.DataException("DeleteMOCClassNotification", ex)
            status = -1
        Finally
            If Not dr Is Nothing Then dr = Nothing
        End Try
    End Sub
    Protected Sub _rblMaintType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _rblMaintType.SelectedIndexChanged
        If _rblMaintType.SelectedValue = "Classification" Then


            _tcBusinessUnit.Visible = "false"
            _tcArea.Visible = "false"
            _tcLine.Visible = "false"

            _pnlBUA.Visible = "false"



            GetClassData()
            Me._ddlFacility2.Enabled = "true"
        ElseIf _rblMaintType.SelectedValue = "Category" Then


            _tcBusinessUnit.Visible = "false"
            _tcArea.Visible = "false"
            _tcLine.Visible = "false"


            _pnlBUA.Visible = "false"
            Me._ddlFacility2.Enabled = "true"



        Else
            ' PopulateNotificationList(Me._ddlFacility2.SelectedValue)
            PopulateSuperintendentNotificationList(Me._ddlFacility2.SelectedValue)



            _tcBusinessUnit.Visible = "true"
            _tcArea.Visible = "true"
            _tcLine.Visible = "true"
            Me._ddlFacility.Enabled = "false"

            _pnlBUA.Visible = "true"


            'Me._ddlFacility2.SelectedValue = userProfile.DefaultFacility

            'Me._ddlFacility2.Enabled = "false"
        End If
    End Sub
    Private Sub GetClassData()
        Dim dr As OracleDataReader = Nothing

        Try

            dr = GetClassRecordsDRFromPackage()
            If dr IsNot Nothing Then
                With Me._gvClass
                    .DataSource = dr
                    .DataBind()
                End With
            End If

        Catch ex As Exception
            Throw New Data.DataException("GetClassData", ex)
        End Try

    End Sub
    Private Function GetClassRecordsDRFromPackage() As OracleDataReader
        Dim dr As OracleDataReader = Nothing
        Dim paramCollection As New OracleParameterCollection

        Try

            Dim param As New OracleParameter

            paramCollection.Clear()
            param = New OracleParameter
            param.ParameterName = "in_SiteID"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = _ddlFacility.SelectedValue
            param.Direction = Data.ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsClassList"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            dr = RI.SharedFunctions.GetOraclePackageDR(paramCollection, "MOCMaint.GetClassNotificationList")

        Catch ex As Exception
            Throw New Data.DataException("GetClassRecordsDRFromPackage", ex)
            Return Nothing
        Finally
            GetClassRecordsDRFromPackage = dr
        End Try
    End Function


    Private Sub PopulateSuperintendentNotificationList(ByVal SiteID As String)

        Try
            Dim paramCollection As New OracleParameterCollection
            Dim param As New OracleParameter
            Dim ds1 As System.Data.DataSet = Nothing
            Dim dr As Data.DataTableReader = Nothing

            'Get the initial list of approvers based on tblmocnotification table.  This should only show up when an MOC is created.
            param = New OracleParameter
            param.ParameterName = "in_siteid"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = SiteID
            param.Direction = Data.ParameterDirection.Input
            paramCollection.Add(param)


            param = New OracleParameter
            param.ParameterName = "rsSuperintendentList"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            ds1 = RI.SharedFunctions.GetDSFromPackage(paramCollection, "SP_MOC_SuperintendentApprovers_List", "GetBUANotificationList", 0)

            dr = ds1.Tables(0).CreateDataReader
            Dim dr2 As Data.DataTableReader = ds1.Tables(0).CreateDataReader


            Me._gvL1.DataSource = Nothing
            Me._gvL1.DataBind()

            Me._gvL1.DataSource = dr2 'dt.Select("notifytype='L1'")
            Me._gvL1.DataBind()


            _ddlApproval.Items.Clear()
            _ddlApproval.Items.Add(New ListItem("Mill Wide Superintendent", "S4"))
            _ddlApproval.Items.Add(New ListItem("Paper Superintendent", "S3"))
            _ddlApproval.Items.Add(New ListItem("Power Superintendent", "S1"))
            _ddlApproval.Items.Add(New ListItem("Pulp Superintendent", "S2"))



            'remove Notifytype if already assigned.
            For Each dr3 As DataRow In ds1.Tables(0).Rows
                _ddlApproval.Items.Remove(_ddlApproval.Items.FindByValue(dr3("notifytype")))
            Next


        Catch ex As Exception
            Throw New Data.DataException("PopulateSuperintendentNotificationList", ex)
        Finally
        End Try

    End Sub



    Private Sub PopulateNotificationList(ByVal SiteID As String)
        ', ByVal busunit As String, ByVal area As String, ByVal line As String)
        Try
            Dim paramCollection As New OracleParameterCollection
            Dim param As New OracleParameter
            Dim ds3 As System.Data.DataSet = Nothing
            Dim dr As Data.DataTableReader = Nothing

            Dim busunit As String = "All"

            Dim area As String = "All"

            Dim line As String = "All"


            'Get the initial list of approvers based on tblmocnotification table.  This should only show up when an MOC is created.
            param = New OracleParameter
            param.ParameterName = "in_siteid"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = SiteID
            param.Direction = Data.ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_BusUnit"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = busunit
            param.Direction = Data.ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_area"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = area
            param.Direction = Data.ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_line"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = line
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
            param.ParameterName = "rsBumList"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            ds3 = RI.SharedFunctions.GetDSFromPackage(paramCollection, "MOCMaint.GetBUANotificationList2", "GetBUANotificationList", 0)

            dr = ds3.Tables(0).CreateDataReader
            Dim dr2 As Data.DataTableReader = ds3.Tables(1).CreateDataReader
            Dim dr3 As Data.DataTableReader = ds3.Tables(2).CreateDataReader
            Dim dr4 As Data.DataTableReader = ds3.Tables(3).CreateDataReader
            Dim dr5 As Data.DataTableReader = ds3.Tables(4).CreateDataReader



            Me._gvL1.DataSource = dr2 'dt.Select("notifytype='L1'")
            Me._gvL1.DataBind()



        Catch ex As Exception
            Throw New Data.DataException("PopulateNotificationList", ex)
        Finally
        End Try

    End Sub

    Sub UpdateBUANotification()
        Try
            Dim paramCollection As New OracleParameterCollection
            Dim param As New OracleParameter
            Dim status As String = String.Empty
            Dim strRequired As String = String.Empty
            Dim strRoleSeqId As String = String.Empty
            Dim strRoleSiteID As String = String.Empty
            Dim strUsername As String = String.Empty




            If IsNumeric(_ddlPeople.SelectedValue) Then
                strRoleSeqId = Me._ddlPeople.SelectedValue
                strRoleSiteID = Me._ddlFacility2.SelectedValue
                strUsername = ""
            Else
                strUsername = Me._ddlPeople.SelectedValue
            End If


            strRequired = "Y"
            Dim busunit As String = "All"
            Dim area As String = "All"
            Dim line As String = "All"


            param = New OracleParameter
            param.ParameterName = "in_siteid"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = _ddlFacility2.SelectedValue
            param.Direction = Data.ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_BusinessUnit"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = busunit
            param.Direction = Data.ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_area"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = area
            param.Direction = Data.ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_line"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = line
            param.Direction = Data.ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_NotifyType"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = Me._ddlApproval.SelectedValue
            param.Direction = Data.ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_username"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = strUsername
            param.Direction = Data.ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Required"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = strRequired
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_RoleSeqId"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = strRoleSeqId
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_RoleSiteId"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = strRoleSiteID
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_UpdateUsername"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = MyGlobalUsername 'userProfile.Username
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            status = RI.SharedFunctions.CallDROraclePackage(paramCollection, "MOCMaint.UpdateMOCNotification")
            If status <> 0 Then
                Throw New Data.DataException("UpdateBUANotification Oracle Error:" & status)
            End If

        Catch ex As Exception
            Throw New Data.DataException("UpdateBUANotification", ex)
        End Try

    End Sub

    Protected Sub DeleteBUA(ByVal deleteRecord As String)
        Dim status As String
        Dim paramCollection As New OracleParameterCollection

        Try

            Dim param As New OracleParameter
            param = New OracleParameter
            param.ParameterName = "in_BUANotify_SeqId"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = deleteRecord
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            status = RI.SharedFunctions.CallDROraclePackage(paramCollection, "MOCMaint.DeleteMOCBUANotification")
            If status <> 0 Then
                Throw New Data.DataException("DeleteMOCBUANotification Oracle Error:" & status)
            End If

            'PopulateNotificationList(_ddlFacility2.SelectedValue)
            PopulateSuperintendentNotificationList(_ddlFacility2.SelectedValue)

        Catch ex As Exception
            Throw New Data.DataException("DeleteMOCBUANotification", ex)
            status = -1
        Finally
            status = Nothing
        End Try
    End Sub


    Protected Sub _gvL1Approvers_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles _gvL1.RowDeleting
        Dim strDeleteRecord As String = Me._gvL1.DataKeys.Item(e.RowIndex).Value

        DeleteBUA(strDeleteRecord)
    End Sub

    Private Sub MOC_DataMaintenance_MOCSuperintendentApprovers_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit

        Page.Theme = GPI.XML.XMLUtility.ReadXmlNode(Session("UserProfile").ToString, "PageTheme")


    End Sub
End Class
