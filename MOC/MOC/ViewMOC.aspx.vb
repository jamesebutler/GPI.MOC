Imports RI
Imports RI.SharedFunctions

Imports System.Data
Imports Devart.Data.Oracle


Imports Telerik.Web.UI
Imports xi = Telerik.Web.UI.ExportInfrastructure
Imports Telerik.Web.UI.GridExcelBuilder
Imports Telerik.Web.UI.Export
Imports GPI.GlobalClass.GlobalVariables
Imports System.Drawing

Partial Class MOC
    Inherits RIBasePage

    Dim HoldSortExpression As String
    Dim userProfile As RI.CurrentUserProfile = Nothing


    Private _dsSessionSearch As DataSet = Nothing

    Public Property SearchSortDirection() As String
        Get
            Return Session("SearchSortDirection")
        End Get
        Set(ByVal value As String)
            Session("SearchSortDirection") = value
        End Set
    End Property

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
        Master.SetBanner(Master.RIRESOURCES.GetResourceValue("ViewMOC", True, "MOC"))
        PopulateDateRange()

        'Me._DateRange.ChangeDateLabel = "true"
    End Sub

    Function GetUserid() As String
        'userProfile = RI.SharedFunctions.GetUserProfile
        'Return userProfile.Username
        Return GPI.XML.XMLUtility.ReadXmlNode(Session("UserProfile").ToString, "Username")

    End Function

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If IsNothing(Session("UserProfile")) = True Then
            ' the session has expired
            Throw New Exception("Your session has expired.")
        End If




        If Not Page.IsPostBack Then
            Session.Remove("moclisting")
            'Dim userProfile As RI.CurrentUserProfile = RI.SharedFunctions.GetUserProfile
            Dim clsSearch As clsMOCViewSearch = Session("clsMOCSearch")

            'Dim _txtStartDateId As String = Me._txtStartDate.FindControl("_txtStartDate").ClientID '.UniqueID
            'Dim _txtEndDateId As String = Me._txtEndDate.FindControl("_txtEndDate").ClientID '.
            'Me._ddlDateRange.Attributes.Add("onchange", "ChangeDate('" & _txtStartDateId & "','" & _txtEndDateId & "',this.value,'" & _txtStartDateValueId & "','" & _txtEndDateValueId & "');return false;")


            'JEB

            'Me._DateRange.SelectedDateRange = -1
            '_DateRange.StartDate = DatePart(DateInterval.Month, Today()) & "/1/" & DatePart(DateInterval.Year, Today())

            '_DateRange.EndDate = "12/31/" & DatePart(DateInterval.Year, Today()) + 1
            'If userProfile IsNot Nothing Then

            Dim todaysDate As Date = Now
            _txtStartDate.SelectedDate = DateSerial(Year(Now), 1, 1)
            _txtEndDate.SelectedDate = Now.ToShortDateString

            'set to current year
            _ddlDateRange.SelectedIndex = 6


            _siteLocation.FacilityValue = GPI.XML.XMLUtility.ReadXmlNode(Session("UserProfile").ToString, "DefaultFacility") 'userProfile.DefaultFacility
            _siteLocation.DivisionValue = GPI.XML.XMLUtility.ReadXmlNode(Session("UserProfile").ToString, "DefaultDivision")  'userProfile.DefaultDivision
            _siteLocation.BusinessUnitValue = "All"
            _siteLocation.AreaValue = "All"
            _siteLocation.LineValue = "All"
            _siteLocation.LineBreakValue = "All"



            Dim sc As ScriptManager
            sc = CType(Page.Form.FindControl("_scriptManager"), ScriptManager)
            If sc IsNot Nothing Then
                Dim loService As New ServiceReference
                loService.InlineScript = True
                loService.Path = "~/CascadingLists.asmx"
                sc.Services.Add(loService)
            End If
            If Not Page.ClientScript.IsClientScriptIncludeRegistered(Page.GetType, "MOC") Then
                Page.ClientScript.RegisterClientScriptInclude(Page.GetType, "MOC", Page.ResolveClientUrl("~/MOC/MOC.js"))
            End If

        End If

        'Dim clsDDL As New clsViewMOCDDL
        'clsDDL.GetDDLData()

        'RI.SharedFunctions.BindList(Me._ddlStatus, clsDDL.Status, False, True)






    End Sub

    'Private Sub SetDefaults()




    '    Dim defaults As New RIUserDefaults.CurrentUserDefaults(userProfile.Username, RIUserDefaults.Applications.MTT, ConfigurationManager.ConnectionStrings.Item("connectionRCFATST").ConnectionString)

    '    If defaults.DoesDefaultValueExist(RIUserDefaults.UserProfileTypes.SiteId) Then
    '        _siteLocation.FacilityValue = defaults.GetDefaultValue(RIUserDefaults.UserProfileTypes.SiteId).ToString
    '    End If

    '    If defaults.DoesDefaultValueExist(RIUserDefaults.UserProfileTypes.BusinessUnit) Then
    '        _siteLocation.BusinessUnitValue = defaults.GetDefaultValue(RIUserDefaults.UserProfileTypes.BusinessUnit).ToString
    '    Else
    '        _siteLocation.BusinessUnitValue = "All"
    '    End If

    '    If defaults.DoesDefaultValueExist(RIUserDefaults.UserProfileTypes.Area) Then
    '        _siteLocation.AreaValue = defaults.GetDefaultValue(RIUserDefaults.UserProfileTypes.Area)
    '    Else
    '        _siteLocation.AreaValue = "All"
    '    End If

    '    If defaults.DoesDefaultValueExist(RIUserDefaults.UserProfileTypes.Line) Then
    '        _siteLocation.LineValue = defaults.GetDefaultValue(RIUserDefaults.UserProfileTypes.Line)
    '    Else
    '        _siteLocation.LineValue = "All"
    '    End If

    '    If defaults.DoesDefaultValueExist(RIUserDefaults.UserProfileTypes.Machine) Then
    '        _siteLocation.LineBreakValue = defaults.GetDefaultValue(RIUserDefaults.UserProfileTypes.Machine)
    '    Else
    '        _siteLocation.LineBreakValue = "All"
    '    End If
    'End Sub

    Protected Sub _btnViewUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnViewUpdate.Click
        Dim clsSearch As New clsMOCViewSearch
        Dim CallSource As String = String.Empty
        SearchSortDirection = "Asc"
        CallSource = "View"
        CallDatabase("", CallSource)
        clsSearch = Session.Item("clsMOCSearch")

        'Me._gvMOCListing.DataSource = clsSearch.Search
        'Me._gvMOCListing.DataBind()
        'Me._gvMOCListing.Visible = True

        Session("moclisting") = ""
        _dsSessionSearch = clsSearch.SearchDS
        RadGridMocListing.DataSource = _dsSessionSearch
        RadGridMocListing.DataBind()
        RadGridMocListing.Visible = True

        Session("moclisting") = _dsSessionSearch





        'Dim RecordCount As Integer = _gvMOCListing.Rows.Count
        Me._lblRecCount.Text = clsSearch.SearchDS.Tables(0).Rows.Count.ToString

    End Sub

    Private Sub CallDatabase(ByVal Orderby As String, ByVal CallSource As String)
        'Me._MOCCategory.RefreshDisplay()

        Dim sqlOrderby As String = String.Empty
        Dim AndOr As String = String.Empty
        Dim clsSearch As New clsMOCViewSearch

        If Orderby.Length = 0 Then
            Orderby = ""
        End If

        If _tbMOCProjectKickoffDate.SelectedDate.ToString = "" Then
            'do nothing
        Else
            clsSearch.KickOffDate = _tbMOCProjectKickoffDate.SelectedDate
        End If

        If _txtStartDate.SelectedDate.ToString = "" Then
            'do nothing
        Else
            clsSearch.StartDate = _txtStartDate.SelectedDate
        End If

        If _txtEndDate.SelectedDate.ToString = "" Then
            'do nothing
        Else
            clsSearch.EndDate = _txtEndDate.SelectedDate
        End If


        If Not (_tbMocNumber.Text = "") Then
            clsSearch.MOCNumber = _tbMocNumber.Text
        End If

        If Not (_siteLocation.DivisionValue = "" Or _siteLocation.DivisionValue = "All") Then
            clsSearch.Division = _siteLocation.DivisionValue
        End If
        If Not (_siteLocation.FacilityValue = "" Or _siteLocation.FacilityValue = "AL") Then
            clsSearch.Facility = _siteLocation.FacilityValue
        End If
        If Not (_siteLocation.BusinessUnitValue = "") And Not (_siteLocation.BusinessUnitValue = "All") Then
            clsSearch.BusinessUnit = _siteLocation.BusinessUnitValue
        End If
        If Not (_siteLocation.AreaValue = "") And Not (_siteLocation.AreaValue = "All") Then
            clsSearch.Area = _siteLocation.AreaValue
        End If
        If Not (_siteLocation.LineValue = "") And Not (_siteLocation.LineValue = "All") Then
            clsSearch.Line = _siteLocation.LineValue
        End If

        If Not (_MOCType.Types = "") Then
            clsSearch.Type = _MOCType.Types
        End If

        If Not (_MOCCategory.Category = "") And Not (_MOCCategory.Category = "All") Then
            clsSearch.Category = _MOCCategory.Category
        End If

        If Not (_MOCCategory.SubCategory = "") And Not (_MOCCategory.SubCategory = "All") Then
            clsSearch.SubCategory = _MOCCategory.SubCategory
        End If

        If Not (_MOCClass.Classification = "") And Not (_MOCClass.Classification = "All") Then
            clsSearch.Classification = _MOCClass.Classification
        End If

        If Not (_ddlInitiator.SelectedValue = "") And Not (_ddlInitiator.SelectedValue = "All") Then
            clsSearch.Initiator = _ddlInitiator.SelectedValue
        End If

        'If Not (_ddlStatus.SelectedValue = "") And Not (_ddlStatus.SelectedValue = "All") Then
        '    clsSearch.Status = _ddlStatus.SelectedValue
        'End If

        If Not (MOCStatus.Status = "") And Not (MOCStatus.Status = "All") Then
            clsSearch.Status = MOCStatus.Status
        End If

        If Not (_siteLocation.FacilityValue = "" Or _siteLocation.FacilityValue = "AL") Then
            clsSearch.Facility = _siteLocation.FacilityValue
            '_gvMOCListing.Columns(0).Visible = False
        Else
            '_gvMOCListing.Columns(0).Visible = True
        End If

        If Not (_ddlOwner.SelectedValue = "") And Not (_ddlOwner.SelectedValue = "All") Then
            clsSearch.Owner = _ddlOwner.SelectedValue
        End If

        If Not (_txtTitleSearch.Text = "") Then
            clsSearch.Title = _txtTitleSearch.Text
        End If

        clsSearch.Username = GetUserid()

        clsSearch.OrderBy = Orderby

        Session.Remove("clsMOCSearch")
        Session.Add("clsMOCSearch", clsSearch)

    End Sub




    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender

        If Not Page.IsPostBack Then
            SetSelectedValue()
        End If


    End Sub

    Protected Sub _btnExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnExcel.Click
        Dim clsExcel As New clsMOCViewSearch
        Dim CallSource As String = String.Empty
        SearchSortDirection = "Asc"
        CallSource = "View"
        CallExcelDatabase("", CallSource)
        clsExcel = Session.Item("clsExcelSearch")

        'Me._gvMOCListing.DataSource = clsExcel.ExcelSearch

        Dim ipLoc As New IP.Bids.Localization.DataLocalization(Master.RIRESOURCES)
        Dim dr As Data.DataTableReader = clsExcel.ExcelSearch
        'Master.DisplayExcel(ipLoc.WriteExcelXml(dr, New ArrayList))
        Master.DisplayExcel(SharedFunctions.WriteExcelXml(dr, New ArrayList))
        dr = Nothing

        'Dim key As String
        'key = "MOCExcelSearch_" & clsExcel.Facility & "_" & clsExcel.Division & "_" & clsExcel.BusinessUnit & "_" & clsExcel.Area & "_" & clsExcel.Line & "_" & clsExcel.LineBreak & "_" & clsExcel.StartDate & "_" & clsExcel.EndDate & "_" & clsExcel.Type & "_" & clsExcel.Category & "_" & clsExcel.Classification & "_" & clsExcel.OrderBy
        'If HttpRuntime.Cache.Item(key) IsNot Nothing Then
        ' Web.UI.ScriptManager.RegisterStartupScript(Me, Page.GetType, "pop", "PopupWindow('excel.aspx?id=1','Excel',800,600,'yes','no','yes');", True)
        'End If
    End Sub
    Private Sub CallExcelDatabase(ByVal Orderby As String, ByVal CallSource As String)

        Dim sqlOrderby As String = String.Empty
        Dim AndOr As String = String.Empty
        Dim clsSearch As New clsMOCViewSearch

        If Orderby.Length = 0 Then
            Orderby = ""
        End If

        If _tbMOCProjectKickoffDate.SelectedDate.ToString = "" Then
            'do nothing
        Else
            clsSearch.KickOffDate = _tbMOCProjectKickoffDate.SelectedDate
        End If

        If _txtStartDate.SelectedDate.ToString = "" Then
            'do nothing
        Else
            clsSearch.StartDate = _txtStartDate.SelectedDate
        End If

        If _txtEndDate.SelectedDate.ToString = "" Then
            'do nothing
        Else
            clsSearch.EndDate = _txtEndDate.SelectedDate
        End If

        If Not (_siteLocation.DivisionValue = "" Or _siteLocation.DivisionValue = "All") Then
            clsSearch.Division = _siteLocation.DivisionValue
        End If
        If Not (_siteLocation.FacilityValue = "" Or _siteLocation.FacilityValue = "AL") Then
            clsSearch.Facility = _siteLocation.FacilityValue
        End If
        If Not (_siteLocation.BusinessUnitValue = "") And Not (_siteLocation.BusinessUnitValue = "All") Then
            clsSearch.BusinessUnit = _siteLocation.BusinessUnitValue
        End If
        If Not (_siteLocation.AreaValue = "") And Not (_siteLocation.AreaValue = "All") Then
            clsSearch.Area = _siteLocation.AreaValue
        End If
        If Not (_siteLocation.LineValue = "") And Not (_siteLocation.LineValue = "All") Then
            clsSearch.Line = _siteLocation.LineValue
        End If


        If Not (_MOCType.Types = "") Then
            clsSearch.Type = _MOCType.Types
        End If

        If Not (_MOCCategory.Category = "") And Not (_MOCCategory.Category = "All") Then
            clsSearch.Category = _MOCCategory.Category
        End If

        If Not (_MOCCategory.SubCategory = "") And Not (_MOCCategory.SubCategory = "All") Then
            clsSearch.SubCategory = _MOCCategory.SubCategory
        End If

        If Not (_MOCClass.Classification = "") And Not (_MOCClass.Classification = "All") Then
            clsSearch.Classification = _MOCClass.Classification
        End If

        If Not (_ddlInitiator.SelectedValue = "") And Not (_ddlInitiator.SelectedValue = "All") Then
            clsSearch.Initiator = _ddlInitiator.SelectedValue
        End If

        If Not (_ddlOwner.SelectedValue = "") And Not (_ddlOwner.SelectedValue = "All") Then
            clsSearch.Owner = _ddlOwner.SelectedValue
        End If

        'If Not (_ddlStatus.SelectedValue = "") And Not (_ddlStatus.SelectedValue = "All") Then
        '    clsSearch.Status = _ddlStatus.SelectedValue
        'End If

        If Not (MOCStatus.Status = "") And Not (MOCStatus.Status = "All") Then
            clsSearch.Status = MOCStatus.Status
        End If

        If Not (_txtTitleSearch.Text = "") Then
            clsSearch.Title = _txtTitleSearch.Text
        End If

        clsSearch.OrderBy = Orderby

        Session.Remove("clsExcelSearch")
        Session.Add("clsExcelSearch", clsSearch)

    End Sub

    Private Sub SetSelectedValue()
        Dim clsSearch As clsMOCViewSearch = Session("clsMOCSearch")
        Dim dr As Data.DataTableReader = Nothing


        Try
            RI.SharedFunctions.DisablePageCache(Response)
            Me._MOCType.DisplayMode = ucMOCTypes.MOCMode.Search
            If clsSearch IsNot Nothing Then
                dr = clsSearch.Search
                If dr IsNot Nothing Then
                    If dr.HasRows Then
                        _siteLocation.FacilityValue = clsSearch.Facility
                        _siteLocation.AreaValue = clsSearch.Area
                        _siteLocation.LineValue = clsSearch.Line
                        _siteLocation.DivisionValue = clsSearch.Division

                        'JEB
                        'Me._DateRange.SelectedDateRange = -1
                        'Me._DateRange.StartDate = clsSearch.StartDate
                        'Me._DateRange.EndDate = clsSearch.EndDate

                        'Display the Search results
                        'Me._gvMOCListing.DataSource = clsSearch.Search
                        'Me._gvMOCListing.DataBind()
                        'Dim RecordCount As Integer = _gvMOCListing.Rows.Count
                        'Me._lblRecCount.Text = RecordCount

                        RadGridMocListing.DataSource = clsSearch.Search
                        RadGridMocListing.DataBind()
                        RadGridMocListing.Visible = True



                    End If
                End If
            Else
                'JEB
                'Me._DateRange.SelectedDateRange = RI_User_Controls_Common_ucDateRange.range.YearToDate
            End If



        Catch ex As Exception
            Throw
        Finally
            If dr IsNot Nothing Then dr = Nothing
        End Try
    End Sub

    Private Sub MOC_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit

        If IsNothing(Session("UserProfile")) = True Then

            'go login
        Else



        End If
        Page.Theme = GPI.XML.XMLUtility.ReadXmlNode(Session("UserProfile").ToString, "PageTheme")



    End Sub

    Public Enum range
        LastMonth = 1
        Last3Months = 2
        LastYearToDate = 3
        YearToDate = 4
        FirstQuarter = 5
        SecondQuarter = 6
        ThirdQuarter = 7
        FourthQuarter = 8
        EnteredLast7Days = 9
        LastYear = 10
        EndOfYear = 11
        Last12Months = 12
        CurrentMonth = 13
        TMinus15MthToTMinus3Mth = 14
    End Enum

    Public Sub PopulateDateRange()
        Dim iploc As New IP.Bids.Localization.WebLocalization()
        Me._ddlDateRange.Items.Clear()
        'Select Case mDisplayAsDropDown


        '    Case True
        Me._ddlDateRange.Items.Add(New ListItem(" ", " "))
        Me._ddlDateRange.Items.Add(New ListItem(iploc.GetResourceValue("Current Month"), range.CurrentMonth))
        Me._ddlDateRange.Items.Add(New ListItem(iploc.GetResourceValue("Last Month"), range.LastMonth))
        Me._ddlDateRange.Items.Add(New ListItem(iploc.GetResourceValue("Last 3 Months"), range.Last3Months))
        Me._ddlDateRange.Items.Add(New ListItem(iploc.GetResourceValue("Last 12 Months"), range.Last12Months))
        Me._ddlDateRange.Items.Add(New ListItem(iploc.GetResourceValue("Last Year"), range.LastYear))
        Me._ddlDateRange.Items.Add(New ListItem(iploc.GetResourceValue("Year To Date"), range.YearToDate))
        Me._ddlDateRange.Items.Add(New ListItem(iploc.GetResourceValue("1st Quarter"), range.FirstQuarter))
        Me._ddlDateRange.Items.Add(New ListItem(iploc.GetResourceValue("2nd Quarter"), range.SecondQuarter))
        Me._ddlDateRange.Items.Add(New ListItem(iploc.GetResourceValue("Entered Last 7 Days"), range.EnteredLast7Days))
        Me._ddlDateRange.Items.Add(New ListItem(iploc.GetResourceValue("3rd Quarter"), range.ThirdQuarter))
        Me._ddlDateRange.Items.Add(New ListItem(iploc.GetResourceValue("4th Quarter"), range.FourthQuarter))
        Me._ddlDateRange.Items.Add(New ListItem(iploc.GetResourceValue("T Minus 15 Months"), range.TMinus15MthToTMinus3Mth))
        Me._ddlDateRange.Visible = True
        ' Me._rblDateRange.Visible = False

        '    Case False
        '        Me._rblDateRange.Items.Add(New ListItem(" ", " "))
        '        Me._rblDateRange.Items.Add(New ListItem(iploc.GetResourceValue("Current Month"), range.CurrentMonth))
        '        Me._rblDateRange.Items.Add(New ListItem(iploc.GetResourceValue("Last Month"), range.LastMonth))
        '        Me._rblDateRange.Items.Add(New ListItem(iploc.GetResourceValue("Last 3 Months"), range.Last3Months))
        '        Me._rblDateRange.Items.Add(New ListItem(iploc.GetResourceValue("Last 12 Months"), range.Last12Months))
        '        Me._rblDateRange.Items.Add(New ListItem(iploc.GetResourceValue("Last Year"), range.LastYear))
        '        Me._rblDateRange.Items.Add(New ListItem(iploc.GetResourceValue("Year To Date"), range.YearToDate))
        '        Me._rblDateRange.Items.Add(New ListItem(iploc.GetResourceValue("1st Quarter"), range.FirstQuarter))
        '        Me._rblDateRange.Items.Add(New ListItem(iploc.GetResourceValue("2nd Quarter"), range.SecondQuarter))
        '        Me._rblDateRange.Items.Add(New ListItem(iploc.GetResourceValue("Entered Last 7 Days"), range.EnteredLast7Days))
        '        Me._rblDateRange.Items.Add(New ListItem(iploc.GetResourceValue("3rd Quarter"), range.ThirdQuarter))
        '        Me._rblDateRange.Items.Add(New ListItem(iploc.GetResourceValue("4th Quarter"), range.FourthQuarter))
        '        Me._ddlDateRange.Items.Add(New ListItem(iploc.GetResourceValue("Today Minus 15 Months to Today Minus 3 Months"), range.TMinus15MthToTMinus3Mth))
        '        Me._ddlDateRange.Visible = False
        '        Me._rblDateRange.Visible = True
        'End Select

    End Sub

    Protected Sub _ddlDateRange_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ddlDateRange.SelectedIndexChanged
        If _ddlDateRange.SelectedIndex > 0 Then
            DateRangeChange(_ddlDateRange.SelectedValue)
        Else
            _txtStartDate.SelectedDate = Nothing
            _txtEndDate.SelectedDate = Nothing
        End If
    End Sub

    Private Sub DateRangeChange(ByVal selectedValue As range)
        SetDateRange(selectedValue)
    End Sub

    Private Sub SetDateRange(ByVal dtRange As range)
        Try
            Dim todaysDate As Date = Now

            If _ddlDateRange.Items.Count = 0 Then
                PopulateDateRange()
            End If
            If Me._ddlDateRange.Items.FindByValue(dtRange) IsNot Nothing Then
                Me._ddlDateRange.ClearSelection()
                Me._ddlDateRange.Items.FindByValue(dtRange).Selected = True
            End If

            Select Case dtRange
                Case range.LastMonth
                    _txtStartDate.SelectedDate = DateSerial(Year(todaysDate), Month(todaysDate) - 1, 1)
                    _txtEndDate.SelectedDate = DateSerial(Year(todaysDate), Month(todaysDate), 0)
                Case range.Last3Months
                    _txtStartDate.SelectedDate = DateSerial(Year(todaysDate), Month(todaysDate) - 3, 1)
                    _txtEndDate.SelectedDate = DateSerial(Year(todaysDate), Month(todaysDate), 0)
                Case range.LastYearToDate '"last year to date"
                    _txtStartDate.SelectedDate = DateSerial(Year(todaysDate) - 1, 1, 1)
                    _txtEndDate.SelectedDate = todaysDate.ToShortDateString
                Case range.YearToDate '"year to date"
                    _txtStartDate.SelectedDate = DateSerial(Year(todaysDate), 1, 1)
                    _txtEndDate.SelectedDate = todaysDate.ToShortDateString
                Case range.FirstQuarter '"1st quarter"
                    _txtStartDate.SelectedDate = DateSerial(Year(todaysDate), 1, 1)
                    _txtEndDate.SelectedDate = DateSerial(Year(todaysDate), 4, 0)
                Case range.SecondQuarter '"2nd quarter"
                    _txtStartDate.SelectedDate = DateSerial(Year(todaysDate), 4, 1)
                    _txtEndDate.SelectedDate = DateSerial(Year(todaysDate), 7, 0)
                Case range.ThirdQuarter '"3rd quarter"
                    _txtStartDate.SelectedDate = DateSerial(Year(todaysDate), 7, 1)
                    _txtEndDate.SelectedDate = DateSerial(Year(todaysDate), 10, 0)
                Case range.FourthQuarter '"4th quarter"
                    _txtStartDate.SelectedDate = DateSerial(Year(todaysDate), 10, 1)
                    _txtEndDate.SelectedDate = DateSerial(Year(todaysDate), 13, 0)
                Case range.EnteredLast7Days '"entered last 7 days"
                    _txtStartDate.SelectedDate = todaysDate.AddDays(-7).ToShortDateString  'DateSerial(Year(todaysDate), Month(todaysDate), -7)
                    _txtEndDate.SelectedDate = todaysDate.ToShortDateString
                Case range.LastYear
                    _txtStartDate.SelectedDate = DateSerial(Year(todaysDate) - 1, 1, 1)
                    _txtEndDate.SelectedDate = DateSerial(Year(todaysDate) - 1, 12, 31)
                Case range.EndOfYear
                    _txtStartDate.SelectedDate = DateSerial(Year(todaysDate), 1, 1)
                    _txtEndDate.SelectedDate = DateSerial(Year(todaysDate), 12, 31)
                Case range.Last12Months
                    _txtStartDate.SelectedDate = Now.AddYears(-1).ToShortDateString
                    _txtEndDate.SelectedDate = todaysDate.ToShortDateString
                Case range.CurrentMonth
                    _txtStartDate.SelectedDate = DateSerial(Year(todaysDate), Month(todaysDate), 1)
                    _txtEndDate.SelectedDate = DateSerial(Year(todaysDate), Month(todaysDate), 1).AddMonths(1).AddDays(-1)
                Case range.TMinus15MthToTMinus3Mth
                    _txtStartDate.SelectedDate = Now.AddMonths(-15).ToShortDateString
                    _txtEndDate.SelectedDate = Now.AddMonths(-3).ToShortDateString
                Case Else
                    _txtStartDate.SelectedDate = Nothing
                    _txtEndDate.SelectedDate = Nothing
            End Select
        Catch ex As Exception
            Throw
        End Try

    End Sub

    Protected Sub RadGridViewMOC_GridExporting(ByVal sender As Object, ByVal e As GridExportingArgs)

        If e.ExportType = ExportType.Pdf Then


        End If



        If e.ExportType = ExportType.Word Then
            e.ExportOutput = e.ExportOutput.Replace("<body>", "<body><div class=WordSection1>")
            e.ExportOutput = e.ExportOutput.Replace("</body>", "</div></body>")


        End If

        If e.ExportType = ExportType.WordDocx Then


        End If


    End Sub


    Protected Sub RadGridMocListing_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGridMocListing.NeedDataSource


        If Not Session("moclisting") Is Nothing Then
            RadGridMocListing.DataSource = Session("moclisting")

        End If
    End Sub

    Private Sub MOC_Unload(sender As Object, e As EventArgs) Handles Me.Unload
        'Dim s As String
        'Dim value As String
        'For Each s In Session
        '    value = "Key: " + s + ", Value: " + Session(s).ToString()
        '    '
        '    Console.Write(value)
        'Next
    End Sub

    Private Sub MOC_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed

    End Sub


    Protected Sub DownloadCSV_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        ConfigureExport()
        RadGridMocListing.ExportSettings.FileName = "MOCList_" + DateTime.Now.ToShortDateString()
        RadGridMocListing.MasterTableView.ExportToCSV()
    End Sub

    Public Sub ConfigureExport()

        RadGridMocListing.ExportSettings.ExportOnlyData = True
        RadGridMocListing.ExportSettings.IgnorePaging = True
        RadGridMocListing.ExportSettings.OpenInNewWindow = True


    End Sub

    Private Sub RadGridMocListing_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles RadGridMocListing.ItemDataBound



        If TypeOf e.Item Is GridDataItem Then


            ' Setup the Resend Link



            Dim item As GridDataItem = CType(e.Item, GridDataItem)

            Dim cellMocNumber As TableCell = item("mocnumber")
            Dim cell As TableCell = item("Status")


            If (TypeOf e.Item Is GridDataItem) Then
                Dim linkCell As GridTableCell = DirectCast(TryCast(e.Item, GridDataItem)("TemplateLinkColumn"), GridTableCell)
                Dim reportLink As HyperLink = DirectCast(linkCell.FindControl("Link"), HyperLink)
                reportLink.Text = item("Title").Text
                Select Case item("Status").Text
                    Case "Superintendent Requested"
                        reportLink.NavigateUrl = "~/MOC/StartMOC.aspx?MOCNumber=" & item("mocnumber").Text
                    Case "Superintendent Denied"
                        reportLink.NavigateUrl = "~/MOC/StartMOC.aspx?MOCNumber=" & item("mocnumber").Text


                    Case Else
                        ' reportLink.NavigateUrl = "~/MOC/EnterMOC.aspx?MOCNumber=" & item("mocnumber").Text
                        reportLink.NavigateUrl = "~/MOC/EnterMOC.aspx?MOCNumber=" & item("mocnumber").Text

                End Select

            End If





            'Get the row from the grid.
            'Dim item As GridDataItem = CType(e.Item, GridDataItem)
            'Dim cell As TableCell = item("Status")
            Dim cellTitle As TableCell = item("Status")



            Select Case item("Status").Text
                Case "Superintendent Endorser"
                    cellTitle.BackColor = Color.LightGoldenrodYellow


            End Select


        End If







    End Sub


End Class
