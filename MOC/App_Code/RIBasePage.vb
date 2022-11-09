Option Explicit On
Option Strict On
Imports RI
Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Xml
Imports System.Text
Imports GPI.XML



''' <summary>
''' This class should be inherited by all pages to insure proper error handling
''' </summary>
''' <remarks>This page will contain all events or functions that should be inherited by all pages</remarks>
Public Class RIBasePage
    Inherits System.Web.UI.Page


    'Public MyGlobalAuthLevel As String
    'Public MyGlobalAuthLevelID As String
    'Public MyGlobalBusType As String
    'Public MyGlobalDefaultDivision As String
    'Public MyGlobalDefaultFacility As String
    'Public MyGlobalDefaultLanguage As String
    'Public MyGlobalDistinguishedName As String
    'Public MyGlobalDivestedLocation As String
    'Public MyGlobalDomainName As String
    'Public MyGlobalEmail As String
    'Public MyGlobalFullName As String
    'Public MyGlobalGroupName As String
    'Public MyGlobalInActiveFlag As String
    'Public MyGlobalProfileTable As String
    'Public MyGlobalUsername As String

    ''' <summary>
    ''' This event captures all page level errors and logs them appropriately
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Page_Error(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Error
        'Log Errors Here
        RI.SharedFunctions.HandleError()
    End Sub


    Protected Overrides Sub InitializeCulture()
        MyBase.InitializeCulture()
        InitCulture()
    End Sub
    Private Sub InitCulture()
        Dim userprofile As RI.CurrentUserProfile = Nothing
        Dim defaultCulture As String = ""
        Dim selectedCulture As String = ""
        Dim cultureBeingUsed As String = ""
        Dim allKeys() As String = Request.Form.AllKeys
        Dim cultureIsSet As Boolean
        Dim myculture As String = ""
        'Dim username As String

        Try

            'To set session to blank
            'Session.Remove("UserProfile")

            If IsNothing(Session("UserProfile")) = True Then
                userprofile = RI.SharedFunctions.GetUserProfile
                Dim myUserProfile As String = CreateXMLUserInfo(userprofile)
                Session("UserProfile") = myUserProfile
                Session("ImpersonateUser") = "nothing"
            End If




            'Look to see if the user has selected a different language
            For i As Integer = 0 To allKeys.Length - 1
                If allKeys(i) IsNot Nothing Then
                    If allKeys(i).Contains("_rblLanguages") Then
                        If Request.Form(allKeys(i).ToString) IsNot Nothing And Request.Form(allKeys(i).ToString).Length > 0 Then
                            selectedCulture = Request.Form(allKeys(i).ToString)
                        End If
                        Exit For
                    End If
                End If
            Next

            If selectedCulture.Length > 0 Then
                cultureBeingUsed = RI.SharedFunctions.InitCulture(selectedCulture)
                If cultureBeingUsed <> "Auto" And cultureBeingUsed.Length > 0 Then
                    cultureIsSet = True
                Else
                    cultureIsSet = False
                End If
            End If

            If cultureIsSet = False Then
                'Populate the current culture from the User Profile table
                'userprofile = RI.SharedFunctions.GetUserProfile
                'If userprofile IsNot Nothing Then defaultCulture = userprofile.DefaultLanguage
                'userprofile = Nothing


                defaultCulture = GPI.XML.XMLUtility.ReadXmlNode(Session("UserProfile").ToString, "DefaultLanguage")



                cultureBeingUsed = RI.SharedFunctions.InitCulture(defaultCulture)
                If cultureBeingUsed <> "Auto" And cultureBeingUsed.Length > 0 Then
                    cultureIsSet = True
                Else
                    cultureIsSet = False
                End If
            End If

            Dim CultureCookie As HttpCookie
            If cultureIsSet = False Then
                'Use the last selected culture from the cookies
                If Request.Cookies("SelectedCulture") IsNot Nothing Then
                    cultureBeingUsed = RI.SharedFunctions.InitCulture(RI.SharedFunctions.DataClean(Request.Cookies("SelectedCulture").Value, "EN-US"))
                End If
                If cultureBeingUsed <> "Auto" And cultureBeingUsed.Length > 0 Then
                    cultureIsSet = True
                Else
                    cultureIsSet = False
                    Me.UICulture = "Auto"
                    Me.Culture = "Auto"
                    If Request.Cookies("SelectedCulture") IsNot Nothing Then
                        Response.Cookies("SelectedCulture").Expires = DateTime.Now.AddDays(-1)
                    End If
                End If

            Else
                CultureCookie = New HttpCookie("SelectedCulture")
                CultureCookie.Value = cultureBeingUsed
                Response.SetCookie(CultureCookie)
            End If

            'If cultureBeingUsed.ToUpper = "RU-RU" Then Response.Charset = "Windows-1251"
        Catch
            Throw
        Finally
            userprofile = Nothing
        End Try
    End Sub
    'Public Function SetCulture(ByVal culture As String) As Boolean
    '    Dim returnValue As Boolean
    '    If (culture <> "Auto") Then
    '        Try
    '            'Dim ci As New System.Globalization.CultureInfo(culture)
    '            Dim ci As System.Globalization.CultureInfo
    '            ci = System.Globalization.CultureInfo.GetCultureInfo(culture)
    '            System.Threading.Thread.CurrentThread.CurrentCulture = ci
    '            System.Threading.Thread.CurrentThread.CurrentUICulture = ci
    '            Me.UICulture = culture
    '            Me.Culture = culture
    '            returnValue = True
    '        Catch ex As ArgumentNullException
    '            'System.ArgumentNullException: name is null.
    '            returnValue = False
    '        Catch ex As System.ArgumentException
    '            'System.ArgumentException: name specifies a culture that is not supported.
    '            returnValue = False
    '        Catch
    '            Throw
    '        End Try
    '    End If
    '    Return returnValue
    'End Function
#Region "ListControlLocalization"
    ''' <summary>
    ''' Overrides the common functionality for the creation
    ''' of controls within the page.
    ''' </summary>
    Protected Overloads Overrides Sub CreateChildControls()


        ' Call the base implementation
        MyBase.CreateChildControls()
    End Sub

    Private Sub FindListControls(ByVal c As Control)
        Try
            For i As Integer = 0 To (c.Controls.Count - 1)
                If i < c.Controls.Count Then
                    If TypeOf c.Controls(i) Is DropDownList Then
                        Dim ddl As DropDownList
                        ddl = TryCast(c.Controls(i), DropDownList)
                        AddHandler ddl.PreRender, AddressOf ListControlDataBound
                    ElseIf TypeOf c.Controls(i) Is CheckBoxList Then
                        Dim lcl As CheckBoxList = TryCast(c.Controls(i), CheckBoxList)
                        AddHandler lcl.PreRender, AddressOf ListControlDataBound
                    ElseIf TypeOf c.Controls(i) Is RadioButtonList Then
                        Dim lcl As RadioButtonList = TryCast(c.Controls(i), RadioButtonList)
                        AddHandler lcl.PreRender, AddressOf ListControlDataBound
                    ElseIf TypeOf c.Controls(i) Is ListControl Then
                        Dim lcl As ListControl = TryCast(c.Controls(i), ListControl)
                        AddHandler lcl.PreRender, AddressOf ListControlDataBound
                    End If
                    If c.Controls(i).HasControls Then
                        FindListControls(c.Controls(i))
                    End If
                End If
            Next
        Catch ex As Exception
            Throw New Exception("FindListControls", ex.InnerException)
        End Try
    End Sub
    Public Sub ListControlDataBound(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim RIRESOURCES As New IP.Bids.Localization.WebLocalization
        Dim lc As ListControl = TryCast(sender, ListControl)
        RIRESOURCES.LocalizeListControl(lc)
    End Sub



    Public Shared Function CreateXMLUserInfo(userprofile As RI.CurrentUserProfile) As String

        'write results to session xml
        Dim settings As New XmlWriterSettings
        With settings
            .CloseOutput = True
            .Encoding = Encoding.UTF8
            .Indent = False
            .OmitXmlDeclaration = True
        End With
        Dim sw As New System.IO.StringWriter
        Using xw As XmlWriter = XmlWriter.Create(sw, settings)
            With xw
                .WriteStartDocument()
                .WriteStartElement("UserProfileInfo")

                .WriteStartElement("AuthLevel")
                .WriteValue(userprofile.AuthLevel)
                .WriteEndElement()

                .WriteStartElement("AuthLevelID")
                .WriteValue(userprofile.AuthLevelID)
                .WriteEndElement()

                .WriteStartElement("BusType")
                .WriteValue(userprofile.BusType)
                .WriteEndElement()

                .WriteStartElement("DefaultDivision")
                .WriteValue(userprofile.DefaultDivision)
                .WriteEndElement()

                .WriteStartElement("DefaultFacility")
                .WriteValue(userprofile.DefaultFacility)
                .WriteEndElement()

                .WriteStartElement("DefaultLanguage")
                .WriteValue(userprofile.DefaultLanguage)
                .WriteEndElement()

                .WriteStartElement("DistinguishedName")
                .WriteValue(userprofile.DistinguishedName)
                .WriteEndElement()

                .WriteStartElement("DivestedLocation")
                .WriteValue(userprofile.DivestedLocation)
                .WriteEndElement()

                .WriteStartElement("DomainName")
                .WriteValue(userprofile.DomainName)
                .WriteEndElement()

                .WriteStartElement("Email")
                .WriteValue(userprofile.Email)
                .WriteEndElement()

                .WriteStartElement("FullName")
                .WriteValue(userprofile.FullName)
                .WriteEndElement()

                .WriteStartElement("GroupName")
                .WriteValue(userprofile.GroupName)
                .WriteEndElement()

                .WriteStartElement("InActiveFlag")
                .WriteValue(userprofile.InActiveFlag)
                .WriteEndElement()

                .WriteStartElement("ProfileTable")
                .WriteValue(userprofile.ProfileTable)
                .WriteEndElement()

                .WriteStartElement("Username")
                .WriteValue(userprofile.Username)
                .WriteEndElement()

                .WriteStartElement("ImpersonateUsername")
                .WriteValue("nothing")
                .WriteEndElement()

                .WriteStartElement("PageTheme")
                .WriteValue(userprofile.PageTheme)
                .WriteEndElement()

                .WriteEndElement()
                .WriteEndDocument()

            End With
        End Using


        Return sw.ToString

    End Function


#End Region
End Class
