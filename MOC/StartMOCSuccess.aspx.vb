Imports Devart.Data.Oracle
Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.Drawing
Imports Telerik.Web.UI
Imports GPI.GlobalClass.GlobalVariables






Partial Class StartMOCSuccess
    Inherits RIBasePage





    Private Sub StartMOCSuccess_Load(sender As Object, e As EventArgs) Handles Me.Load

        Dim mymoc As String = ""
        If Request.QueryString("MOCNumber") IsNot Nothing Then
            RI.SharedFunctions.InsertRILoggingRecord("EnterMOC.aspx.vb", Request.QueryString("MOCNumber") + " " + "MOC_EnterMOCNew_Load")
            mymoc = Request.QueryString("MOCNumber")
            Master.SetBanner("MOC Idea  -" & mymoc, True)
            lblMOC.Text = mymoc & " Record Created."
        End If

    End Sub

    Private Sub StartMOCSuccess_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit

        Try
            Page.Theme = GPI.XML.XMLUtility.ReadXmlNode(Session("UserProfile").ToString, "PageTheme")
            Master.SetBanner("MOC Idea ", True)

        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        Master.ShowMOCMenu()

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


        If CStr(Session("ImpersonateUser")) = "nothing" Then
            'do nothing
        Else
            MyGlobalUsername = CStr(Session("ImpersonateUser"))
        End If




    End Sub
End Class
