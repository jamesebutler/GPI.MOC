Imports Devart.Data.Oracle
Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.Drawing
Imports Telerik.Web.UI
Imports GPI.GlobalClass.GlobalVariables
Imports clsStartMOC
Partial Class jeb
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


    Dim clsstartmoc As New clsStartMOC()

    Private Sub jeb_Load(sender As Object, e As EventArgs) Handles Me.Load




    End Sub

    Private Sub jeb_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit

        Page.Theme = GPI.XML.XMLUtility.ReadXmlNode(Session("UserProfile").ToString, "PageTheme")
        Master.SetBanner("MOC Idea submittal Form", True)



    End Sub
End Class
