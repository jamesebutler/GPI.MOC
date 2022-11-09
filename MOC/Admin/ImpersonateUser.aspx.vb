Option Explicit On
Option Strict On

Imports RI
Imports System.Data
Imports Devart.Data.Oracle
Imports System.Text
Imports GPI.GlobalClass.GlobalVariables

Partial Class Admin_ImpersonateUser
    Inherits RIBasePage



    Private Sub Admin_ImpersonateUser_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Page.Theme = GPI.XML.XMLUtility.ReadXmlNode(Session("UserProfile").ToString, "PageTheme")

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


        MyGlobalUsername = CStr(Session("ImpersonateUser"))

        If MyGlobalUsername = "nothing" Then
            Master.SetBanner("Impersonating User")
        Else
            Master.SetBanner("Impersonating User: " & MyGlobalUsername)

        End If




    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            Dim currentProfile As CurrentUserProfile = Nothing

            'Dim username As String
            'username = CurrentUserProfile.GetCurrentUser


            MyGlobalDomainName = GPI.XML.XMLUtility.ReadXmlNode(Session("UserProfile").ToString, "DomainName")
            MyGlobalUsername = GPI.XML.XMLUtility.ReadXmlNode(Session("UserProfile").ToString, "Username")



            Dim _currentusername As String
            _currentusername = MyGlobalDomainName & "\" & MyGlobalUsername


            Dim _ImpersonateUser As String = CStr(Session("ImpersonateUser"))

            If _ImpersonateUser = "nothing" Then
                'do nothing
            Else
                Console.WriteLine("Im here")
                Session.Add("CurrentUser", MyGlobalDomainName & "\" + _ImpersonateUser)
                _currentusername = MyGlobalDomainName & "\" + _ImpersonateUser

            End If


            currentProfile = RI.SharedFunctions.GetUserProfile
            currentProfile = CType(Session.Item(Replace(_currentusername, "\", "_")), CurrentUserProfile) ' = CType(Session("LDAP"), AuthUser) '= AuthUser.GetAuthUser()
            If currentProfile IsNot Nothing Then
                With currentProfile
                    _divUserProfile.InnerHtml = .ProfileTable
                End With
            End If

            'PopulateUsers()
        End If
    End Sub

    Protected Sub _btnImpersonateUser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnImpersonateUser.Click




        Dim myuser As String = TextBoxcompanyname.Text
        Dim mylength As Integer = myuser.Length
        Dim myposition As Integer
        myposition = InStr(1, myuser, ":", CompareMethod.Text)
        myuser = myuser.Substring(myposition, myuser.Length - (myposition))

        If myposition = 0 Then  'nothing was selected
            Exit Sub
        End If

        Dim currentProfile As CurrentUserProfile = Nothing

            'check to see if you are impersonating and you are the login user
            Dim MyGlobalUsername As String = GPI.XML.XMLUtility.ReadXmlNode(Session("UserProfile").ToString, "Username")
            Dim MyGlobalDomainName As String = GPI.XML.XMLUtility.ReadXmlNode(Session("UserProfile").ToString, "DomainName")


            Session.Remove("CurrentUser")
            Session.Add("CurrentUser", myuser)
            Session.Remove("clsSearch")
            currentProfile = RI.SharedFunctions.GetUserProfile

            Dim MyGlobalDefaultFacility = GPI.XML.XMLUtility.ReadXmlNode(Session("UserProfile").ToString, "DefaultFacility")


            'System.Web.HttpContext.Current.Session.Add("CurrentUser", currentUser)
            If currentProfile IsNot Nothing Then
                With currentProfile
                    _divUserProfile.InnerHtml = .ProfileTable
                    'Master.SetImpersonaterUser(.DomainName & "\" & .Username, .FullName)
                    If currentProfile.Username = MyGlobalUsername Then
                        Session("ImpersonateUser") = "nothing"
                    Else
                        Session("ImpersonateUser") = .Username
                        Master.SetImpersonateUser(.DomainName & "\" & .Username)
                        Master.SetBanner("Impersonating User: " & .Username)
                    End If


                    Response.Redirect("~/Admin/ImpersonateUser.aspx")
                End With

            End If



    End Sub

    Private Sub Admin_ImpersonateUser_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete

        'MyGlobalUsername = CStr(Session("ImpersonateUser"))

        'If MyGlobalUsername = "nothing" Then
        '    Master.SetBanner("Impersonating User")
        'Else
        '    Master.SetBanner("Impersonating User: " & MyGlobalUsername)

        'End If

    End Sub


    'Protected Sub _btnImpersonateUser1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnImpersonateUser1.Click
    '    Dim currentProfile As CurrentUserProfile = Nothing

    '    Session.Remove("CurrentUser")
    '    Session.Add("CurrentUser", Me._ddlUser.SelectedValue)
    '    Session.Remove("clsSearch")
    '    currentProfile = RI.SharedFunctions.GetUserProfile
    '    'System.Web.HttpContext.Current.Session.Add("CurrentUser", currentUser)
    '    If currentProfile IsNot Nothing Then
    '        With currentProfile
    '            _divUserProfile.InnerHtml = .ProfileTable
    '            'Master.SetImpersonaterUser(.DomainName & "\" & .Username, .FullName)
    '            Master.SetImpersonateUser(.DomainName & "\" & .Username)
    '            Session("ImpersonateUser") = .Username
    '        End With
    '    End If

    'End Sub


    'Private Function GetUserListDSFromPackage() As OracleDataReader
    '    Dim cmdSQL As OracleCommand = Nothing
    '    Dim connection As String = String.Empty
    '    Dim provider As String = String.Empty
    '    Dim dr As OracleDataReader = Nothing
    '    Dim daData As OracleDataAdapter = Nothing
    '    Dim cnConnection As OracleConnection = Nothing

    '    Try
    '        If connection.Length = 0 Then
    '            connection = ConfigurationManager.ConnectionStrings.Item("connectionRCFATST").ConnectionString
    '        End If
    '        If provider.Length = 0 Then
    '            provider = ConfigurationManager.ConnectionStrings.Item("connectionRCFATST").ProviderName
    '        End If
    '        cmdSQL = New OracleCommand
    '        With cmdSQL
    '            cnConnection = New OracleConnection(connection)
    '            cnConnection.Open()
    '            .Connection = cnConnection
    '            '.CommandText = "ri.Testusers"
    '            .CommandText = "ADMIN.AllActiveUsers"
    '            .CommandType = CommandType.StoredProcedure
    '            Dim param As New OracleParameter

    '            param = New OracleParameter
    '            'param.ParameterName = "RSTESTUSERS"
    '            param.ParameterName = "RSAllActiveUsers"
    '            param.OracleDbType = OracleDbType.Cursor
    '            param.Direction = ParameterDirection.Output
    '            .Parameters.Add(param)
    '        End With

    '        'ds = New DataSet()
    '        'ds.EnforceConstraints = False
    '        'daData = New OracleDataAdapter(cmdSQL)
    '        'daData.Fill(ds)
    '        'ds.EnforceConstraints = True
    '        dr = cmdSQL.ExecuteReader(CommandBehavior.CloseConnection)
    '    Catch ex As Exception
    '        Return Nothing
    '        Throw New DataException("GetUserListDSFromPackage", ex)
    '        If Not cnConnection Is Nothing Then cnConnection = Nothing
    '    Finally
    '        GetUserListDSFromPackage = dr
    '        If Not daData Is Nothing Then daData = Nothing
    '        'If Not ds Is Nothing Then ds = Nothing
    '        If Not cmdSQL Is Nothing Then cmdSQL = Nothing
    '        'cnConnection.Close()
    '        'If Not cnConnection Is Nothing Then cnConnection = Nothing
    '    End Try
    'End Function
    'Private Sub PopulateUsers()
    '    Dim savedXML As String = String.Empty
    '    Dim io As New System.IO.StringWriter
    '    Dim sb As New StringBuilder
    '    Dim sbUser As New StringBuilder
    '    Try
    '        'savedXML = SharedFunctions.GetDataFromSQLServerCache("TestUsers", 1440)
    '        Dim dr As OracleDataReader = GetUserListDSFromPackage()
    '        'dr = GetUserListDSFromPackage()
    '        If dr IsNot Nothing Then
    '            If dr.HasRows = True Then
    '                While dr.Read
    '                    sb.Length = 0
    '                    sb.Append(dr.Item("Name"))
    '                    sb.Append(" (")
    '                    sb.Append(dr.Item("Location"))
    '                    sb.Append(")")

    '                    sbUser.Length = 0
    '                    sbUser.Append(dr.Item("Domain"))
    '                    sbUser.Append("\")
    '                    sbUser.Append(dr.Item("UserName"))

    '                    _ddlUser.Items.Add(New ListItem(sb.ToString, sbUser.ToString))
    '                End While

    '            End If
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub
End Class
