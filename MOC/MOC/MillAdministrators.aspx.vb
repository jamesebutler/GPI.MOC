Imports System.Data
Imports Devart.Data.Oracle
Imports GPI.GlobalClass.GlobalVariables

Partial Class MillAdministrators
    Inherits RIBasePage

    Private Sub MillAdministrators_Init(sender As Object, e As EventArgs) Handles Me.Init


        Master.SetBanner("Mill Administrators", True)
        Master.ShowMOCMenu(MyGlobalAuthLevel)

        MyGlobalDefaultFacility = GPI.XML.XMLUtility.ReadXmlNode(Session("UserProfile").ToString, "DefaultFacility")



    End Sub

    Private Sub MillAdministrators_Load(sender As Object, e As EventArgs) Handles Me.Load

        'go get admins

        PopulateAdministratorsList(MyGlobalDefaultFacility)




    End Sub

    Private Sub PopulateAdministratorsList(ByVal SiteID As String)

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
            param.ParameterName = "rsAdminList"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            ds1 = RI.SharedFunctions.GetDSFromPackage(paramCollection, "SP_GetAllSiteAdmins_List", "SP_GetAllSiteAdmins_List", 0)

            dr = ds1.Tables(0).CreateDataReader
            Dim dr2 As Data.DataTableReader = ds1.Tables(0).CreateDataReader


            Me._gvMillAdministrators.DataSource = dr2 'dt.Select("notifytype='L1'")
            Me._gvMillAdministrators.DataBind()


        Catch ex As Exception
            Throw New Data.DataException("SP_GetAllSiteAdmins_List", ex)
        Finally
        End Try

    End Sub

    Private Sub MillAdministrators_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit

        Page.Theme = GPI.XML.XMLUtility.ReadXmlNode(Session("UserProfile").ToString, "PageTheme")


    End Sub
End Class
