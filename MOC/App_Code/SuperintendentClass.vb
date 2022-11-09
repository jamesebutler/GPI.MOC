Imports Microsoft.VisualBasic
Imports Devart.Data.Oracle
Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient

Public Class SuperintendentClass

    Public Property siteid As String
    Public Property notify_seqid As String
    Public Property username As String
    Public Property notifytype As String
    Public Property required As String
    Public Property fullname As String
    Public Property email As String
    Public Property notifytypeName As String


    Sub Main()

    End Sub


    ' Function GetSuperintendents returns a list of Student objects.

    Function GetSuperintendentsList(ByVal ds As DataSet) As List(Of SuperintendentClass)
        Dim SuperintendentClassInstance As SuperintendentClass
        Dim MySuperintendentList As New List(Of SuperintendentClass)
        ' Dim MyDataRow As DataRow

        Dim dr As Data.DataTableReader = Nothing
        Dim dt As New DataTable

        dr = ds.Tables(0).CreateDataReader
        If dr IsNot Nothing Then
            Do While dr.Read
                SuperintendentClassInstance = New SuperintendentClass

                SuperintendentClassInstance.siteid = dr.Item("siteid").ToString
                SuperintendentClassInstance.notify_seqid = dr.Item("notify_seqid").ToString
                SuperintendentClassInstance.username = dr.Item("username").ToString
                SuperintendentClassInstance.notifytype = dr.Item("notifytype").ToString
                SuperintendentClassInstance.required = dr.Item("required").ToString
                SuperintendentClassInstance.fullname = dr.Item("fullname").ToString
                SuperintendentClassInstance.email = dr.Item("email").ToString
                SuperintendentClassInstance.notifytypeName = dr.Item("notifytypeName").ToString
                MySuperintendentList.Add(SuperintendentClassInstance)
            Loop

        End If


        'For Each MyDataRow In ds.Tables(0).Rows
        '    SuperintendentClassInstance = New SuperintendentClass
        '    ' I'm going to assume that the class properties are in the same order as the dataset
        '    SuperintendentClassInstance.siteid = MyDataRow(0)
        '    SuperintendentClassInstance.notify_seqid = MyDataRow(1)
        '    SuperintendentClassInstance.username = MyDataRow(2)
        '    SuperintendentClassInstance.notifytype = MyDataRow(3)
        '    SuperintendentClassInstance.required = MyDataRow(4)
        '    SuperintendentClassInstance.fullname = MyDataRow(5)
        '    SuperintendentClassInstance.email = MyDataRow(6)
        '    SuperintendentClassInstance.notifytypeName = MyDataRow(7)
        '    MySuperintendentList.Add(SuperintendentClassInstance)
        '    ' Everytime you loop you're adding an instance of the class to the list
        'Next

        Return MySuperintendentList


    End Function


    Function GetAllSuperintendents() As DataSet

        Dim dr As Data.DataTableReader = Nothing
        Dim dt As New DataTable
        Dim ds As DataSet = clsCurrentMOC.GetAllSuperintendentAllList()

        GetAllSuperintendents = ds
        ds.Dispose()
        Return GetAllSuperintendents


    End Function


    Function GetAllSuperintendentBySiteID(in_siteID As String) As DataSet

        Dim dr As Data.DataTableReader = Nothing
        Dim dt As New DataTable
        Dim ds As DataSet = clsCurrentMOC.GetAllSuperintendentList(in_siteID)



        GetAllSuperintendentBySiteID = ds
        ds.Dispose()
        Return GetAllSuperintendentBySiteID




    End Function



End Class
