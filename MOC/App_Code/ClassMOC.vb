

Imports Microsoft.VisualBasic
Imports Devart.Data.Oracle
Imports RI.SharedFunctions
Imports System.Net
Imports System.Diagnostics
Imports System.IO
Imports System.Net.Mail
Imports System.Environment

Imports clsCurrentMOC

Public Class ClassMOC
    Dim _clscurrentmoc As New clsCurrentMOC()


    Public Sub SendEmailToReviewerTEST(_clscurrentmoc As clsCurrentMOC,
                                            emailAddress As String,
                                            _MyGlobalFullName As String,
                                            _SuperintendentName As String,
                                            _SuperintendentDate As String,
                                            _SuperintendentComments As String,
                                            Optional ByVal toL1List As String = "",
                                            Optional ByVal toEList As String = "",
                                            Optional ByVal toccList As String = "",
                                            Optional ByVal SubMsg As String = "")

        Dim strHeader As String = ""
        Dim strSubject As String = ""

        Dim toPerson As String = ""

        RI.SharedFunctions.InsertRILoggingRecord("ClassMOC.aspx.vb", " START SendEmailToReviewer ")


        strHeader = "You have been selected as an approver.  Please review this MOC and approve or reject."
        strSubject = SubMsg & _clscurrentmoc.BusinessUnit

        Dim sb As New StringBuilder
        Dim list As String() = toL1List.Split(CChar(","))


        For i As Integer = 0 To list.Length - 1
            If list.Length > 0 Then
                toPerson = list(i)


                'email to Superintendent reviewers
                Dim bsent As Boolean = clsCurrentMOC.PrepareL1Email(_clscurrentmoc.MOCNumber,
                                              _clscurrentmoc.Title,
                                              _clscurrentmoc.Description,
                                              _MyGlobalFullName & " (" & _MyGlobalFullName & ")",
                                              _clscurrentmoc.MOCType,
                                              _clscurrentmoc.Costs,
                                              _clscurrentmoc.Funding,
                                              _clscurrentmoc.Impact,
                                              _clscurrentmoc.NotificationL1FullName,
                                              _clscurrentmoc.NotificationEFullName,
                                              _clscurrentmoc.StartDate,
                                              _clscurrentmoc.Classification,
                                              _clscurrentmoc.Category,
                                              _clscurrentmoc.BusinessUnit,
                                              _SuperintendentName,
                                              _SuperintendentDate,
                                              _SuperintendentComments,
                                              "MOC.Notification@graphicpkg.com",
                                              toPerson,
                                              strHeader,
                                              strSubject,
                                              "") ' Me._SuperintendentBusinessType.SelectedItem.Text)


            End If
        Next

        RI.SharedFunctions.InsertRILoggingRecord("ClassMOC.aspx.vb", " END SendEmailToReviewer ")


    End Sub
















    'nothing below this line

End Class
