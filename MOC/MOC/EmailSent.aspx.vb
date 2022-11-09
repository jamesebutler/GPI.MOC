Imports GPI.GlobalClass.GlobalVariables
Partial Class MOC_EmailSent
    Inherits RIBasePage

    Private Sub MOC_EmailSent_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit

        Page.Theme = GPI.XML.XMLUtility.ReadXmlNode(Session("UserProfile").ToString, "PageTheme")
        Master.SetBanner("Email Sent", True)



    End Sub

    Private Sub MOC_EmailSent_Init(sender As Object, e As EventArgs) Handles Me.Init

        If CStr(Session("ImpersonateUser")) = "nothing" Then
            'do nothing
        Else
            MyGlobalUsername = CStr(Session("ImpersonateUser"))
        End If


    End Sub

    Private Sub MOC_EmailSent_Load(sender As Object, e As EventArgs) Handles Me.Load


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

        Dim buildString As String = String.Empty

        If Request.QueryString("MOCNumber") IsNot Nothing Then
            'add to label MOC number
            'buildString = "MOC: " & Request.QueryString("MOCNumber")


        End If

        If Request.QueryString("emailto") IsNot Nothing Then
            'add to label full name number
            buildString = buildString & " Email sent to: " & Request.QueryString("emailto")

        End If

        LabelResults.Text = buildString

    End Sub
End Class
