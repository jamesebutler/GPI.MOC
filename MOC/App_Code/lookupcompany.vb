Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols

Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic


' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")>
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
<System.Web.Script.Services.ScriptService()>
Public Class lookupcompany
    Inherits System.Web.Services.WebService

    <WebMethod()>
    Public Function HelloWorld() As String
        Return "Hello World"
    End Function

    <WebMethod()>
    <Script.Services.ScriptMethod()>
    Function lookupCompanyName(ByVal prefixText As String) As String()
        Dim count As Integer = 10
        Dim items As New List(Of String)(count)

        Dim ds As New DataSet()

        Dim connectionString As String = ConfigurationManager.ConnectionStrings("DataConnectionString_Company").ConnectionString

        Using connection As New SqlConnection(connectionString)

            Dim sql As String = "select distinct top 20 companyname from companynameLookup where companyname like '" + prefixText + "%'" & " order by companyname"

            Dim adapter As New SqlDataAdapter()

            adapter.SelectCommand = New SqlCommand(sql, connection)

            adapter.Fill(ds)

        End Using

        For Each dr As DataRow In ds.Tables(0).Rows

            items.Add(dr("companyname").ToString())

        Next

        Return items.ToArray()

    End Function


End Class