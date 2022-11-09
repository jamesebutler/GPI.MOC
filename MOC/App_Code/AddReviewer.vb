Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols

Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Imports Devart.Data.Oracle

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class AddReviewer
    Inherits System.Web.Services.WebService

    <WebMethod()>
    Public Function HelloWorld() As String
        Return "Hello World"
    End Function

    <WebMethod()>
    <Script.Services.ScriptMethod()>
    Function lookupEmployeeByName(ByVal prefixText As String) As String()

        Dim ds As System.Data.DataSet = Nothing
        Dim mySql As String

        Dim count As Integer = 10
        Dim items As New List(Of String)(count)

        mySql = ""
        mySql += "SELECT  UPPER(TRIM(E.lastname)) || ', ' || UPPER(TRIM(E.firstname)) || '   (' || TRIM(S.sitename) || ')   Username:' || E.domain || '\' || UPPER(TRIM(E.username)) NAME"
        mySql += " From refemployee E,refsite S"
        mySql += " WHERE 1=1 AND E.plantcode = S.rcfaflid AND E.inactive_flag = 'N'  AND E.domain = 'NA'"
        mySql += " AND UPPER(E.lastname) LIKE '" + Trim(UCase(prefixText)) + "%'"
        mySql += " AND rownum <=20"
        mySql += " ORDER BY E.lastname"

        ds = RI.SharedFunctions.GetOracleDataSet(mySql, "")

        If ds IsNot Nothing Then
            'Dim row As DataRow
            'row = ds.Tables(0).NewRow
            'row("PROCESS") = "JAMES"
            'ds.Tables(0).Rows.Add(row)

        End If


        For Each dr As DataRow In ds.Tables(0).Rows

            items.Add(dr("NAME").ToString())

        Next

        Return items.ToArray()

        If ds IsNot Nothing Then
            ds.Dispose()
            ds = Nothing
        End If



    End Function


End Class