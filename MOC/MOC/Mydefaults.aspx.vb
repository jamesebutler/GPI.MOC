Imports RI
Imports RI.SharedFunctions
Imports GPI.GlobalClass.GlobalVariables
Imports Devart.Data.Oracle
Imports System.Diagnostics
Imports System.Text

Imports Telerik.Web.UI

Partial Class MOC_Mydefaults
    Inherits RIBasePage


    Dim userprofile As RI.CurrentUserProfile = Nothing
    Dim themeselected As String

    Private Sub MOC_Mydefaults_Init(sender As Object, e As EventArgs) Handles Me.Init

        MyGlobalAuthLevel = GPI.XML.XMLUtility.ReadXmlNode(Session("UserProfile").ToString, "AuthLevel")
        MyGlobalUsername = GPI.XML.XMLUtility.ReadXmlNode(Session("UserProfile").ToString, "Username")


        Master.ShowMOCMenu(MyGlobalAuthLevel)

        Master.SetBanner(Master.RIRESOURCES.GetResourceValue("My Defaults"))


    End Sub

    Private Sub MOC_Mydefaults_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit

        If IsNothing(Session("UserProfile")) = True Then
            'go login
        Else

        End If

        Page.Theme = GPI.XML.XMLUtility.ReadXmlNode(Session("UserProfile").ToString, "PageTheme")


    End Sub

    Private Sub MOC_Mydefaults_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Page.IsPostBack Then


        Else
            Select Case Page.Theme
                Case "RIBlue"
                    rbtnBlue.Checked = True
                Case "RIGold"
                    rbtnGold.Checked = True
                Case "RIGreen"
                    rbtnGreen.Checked = True
                Case "RIPurple"
                    rbtnPurple.Checked = True
                Case "RIYellow"
                    rbtnYellow.Checked = True
                Case "RIBlack"
                    rbtnBlack.Checked = True
            End Select
        End If




    End Sub

    Protected Sub _btnThemeUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnThemeUpdate.Click

        Dim _theme As String = ""
        Try

            If rbtnBlue.Checked Then
                _theme = "RIBlue"

            ElseIf rbtnGold.Checked Then
                _theme = "RIGold"
            ElseIf rbtnGreen.Checked Then
                _theme = "RIGreen"
            ElseIf rbtnPurple.Checked Then
                _theme = "RIPurple"
            ElseIf rbtnYellow.Checked Then
                _theme = "RIYellow"
            ElseIf rbtnBlack.Checked Then
                _theme = "RIBlack"

            End If
            'go update theme in the database.
            UpdateUserTheme(MyGlobalUsername, _theme)

            'remove session
            Session.Remove("UserProfile")

            'the profile settings will load from RIBasePage.vb
            Server.TransferRequest("Mydefaults.aspx")

        Catch ex As Exception
            Throw
        Finally

        End Try

    End Sub

    Private Sub UpdateUserTheme(ByVal User As String, ByVal InTheme As String)
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        'Dim ds As System.Data.DataSet = Nothing
        Dim status As String
        Dim dr As OracleDataReader = Nothing
        'Check input paramaters

        Try
            Dim date1 As Date = DateTime.Now


            param = New OracleParameter
            param.ParameterName = "in_username"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = User
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_uitheme"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = InTheme
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            status = RI.SharedFunctions.CallDROraclePackage(paramCollection, "riuser.UpdateUserTheme")
            If status <> "0" Then
                Throw New Data.DataException("UpdateUserTheme")
            End If


            Dim date2 As Date = DateTime.Now
            Dim duration As TimeSpan = date2 - date1
            InsertLoggingRecord("UpdateUserTheme   END", "Duration: " & duration.ToString)
        Catch ex As Exception
            Throw New Data.DataException("UpdateExcelList", ex)
        Finally
            If Not dr Is Nothing Then dr = Nothing
        End Try
    End Sub


End Class
