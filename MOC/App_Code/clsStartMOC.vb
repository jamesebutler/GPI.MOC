Imports Devart.Data.Oracle
Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.Drawing

Imports System.Xml.Xsl
Imports System.IO
Imports System.Net.Mail
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Net
Imports System.Web


Imports System.Net.Dns


Imports RI.SharedFunctions


Public Class clsStartMOC





    Public Function GetDefaultReviewerList(ByVal mocNumber As Integer,
                             ByVal siteID As String,
                             ByVal in_BusUnitArea As String,
                             ByVal in_line As String,
                             ByVal in_class As String,
                             ByVal in_cat As String,
                             ByVal in_SubCategory As String,
                             ByVal in_EquipSubCategory As String) As Boolean

        Try

            Dim paramCollection As New OracleParameterCollection
            Dim param As New OracleParameter
            Dim ds As System.Data.DataSet = Nothing


            param = New OracleParameter
            param.ParameterName = "in_Siteid"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = siteID
            param.Direction = Data.ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_BusUnitArea"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = in_BusUnitArea
            param.Direction = Data.ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Line"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = in_line
            param.Direction = Data.ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Class"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = in_class
            param.Direction = Data.ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Cat"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = in_cat
            param.Direction = Data.ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_SubCat"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = in_SubCategory & in_EquipSubCategory
            param.Direction = Data.ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsInformedList"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsL1List"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsL2List"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsL3List"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsL4List"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "NewMOC.GetDefaultApproverList", "GetDefaultApproverList_" & mocNumber, 0)


            If ds IsNot Nothing Then
                Dim drInformed As DataTableReader = ds.Tables(0).CreateDataReader
                If drInformed IsNot Nothing Then
                    Do While drInformed.Read

                        If drInformed.Item("username") IsNot DBNull.Value Then
                            InsertMOCApprovalDefaults(mocNumber.ToString, RemoveCharacter(drInformed.Item("username").ToString, "*"), drInformed.Item("Notifytype").ToString, "Y")

                        End If

                    Loop
                End If
            End If

            If ds IsNot Nothing Then
                Dim drReviewer As DataTableReader = ds.Tables(1).CreateDataReader
                If drReviewer IsNot Nothing Then
                    Do While drReviewer.Read

                        If drReviewer.Item("username") IsNot DBNull.Value Then
                            InsertMOCApprovalDefaults(mocNumber.ToString, RemoveCharacter(drReviewer.Item("username").ToString, "*"), drReviewer.Item("Notifytype").ToString, "Y")

                        End If

                    Loop
                End If
            End If



            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function


    Public Function CheckApprovalBUMCount(ByVal mocNumber As String, ByVal approvalType As String, ByVal username As String) As String
        RI.SharedFunctions.InsertRITraceRecord("clsEnterMOC.vb", mocNumber + " CheckApprovalBUMCount ")
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim connection As String = String.Empty
        Dim provider As String = String.Empty
        Dim roleSeqId As String = String.Empty
        Dim rolePlantCode As String = String.Empty




        'Check input paramaters
        Try


            If UCase(mocNumber) <> "" Then

                paramCollection = New OracleParameterCollection

                param = New OracleParameter
                param.ParameterName = "IN_MOCNUMBER"
                param.OracleDbType = OracleDbType.Integer
                param.Direction = Data.ParameterDirection.Input
                param.Value = mocNumber
                paramCollection.Add(param)

                param = New OracleParameter
                param.ParameterName = "IN_APPROVALTYPE"
                param.OracleDbType = OracleDbType.VarChar
                param.Direction = Data.ParameterDirection.Input
                param.Value = approvalType
                paramCollection.Add(param)


                param = New OracleParameter
                param.ParameterName = "out_count"
                param.OracleDbType = OracleDbType.Number
                param.Direction = Data.ParameterDirection.Output
                paramCollection.Add(param)
                'TODO: change to production package
                Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.jebgpi.CountApprovalFlag")
                Return returnStatus
                If CDbl(returnStatus) <> 0 Then
                    Throw New Data.DataException("Error Saving Approval " & mocNumber)
                End If
                'Next
            End If
        Catch ex As Exception
            Throw
        End Try
        Return "999"
    End Function

    Public Function InsertMOCApprovalDefaults(ByVal mocNumber As String, ByVal username As String, ByVal approvalType As String, ByVal Required As String) As String
        RI.SharedFunctions.InsertRITraceRecord("clsEnterMOC.vb", mocNumber + " InsertMOCApprovalBUM ")
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim connection As String = String.Empty
        Dim provider As String = String.Empty
        Dim roleSeqId As String = String.Empty
        Dim rolePlantCode As String = String.Empty

        Dim userNameArray As String() = username.Split(CChar("/"))

        'Check input paramaters
        Try

            If userNameArray.Length > 1 Then
                roleSeqId = userNameArray(1)
                rolePlantCode = userNameArray(0)
                username = String.Empty
            Else
                username = userNameArray(0)
            End If

            If UCase(username) <> "ALL" Then
                paramCollection = New OracleParameterCollection

                param = New OracleParameter
                param.ParameterName = "IN_MOCNUMBER"
                param.OracleDbType = OracleDbType.Integer
                param.Direction = Data.ParameterDirection.Input
                param.Value = mocNumber
                paramCollection.Add(param)

                param = New OracleParameter
                param.ParameterName = "in_username"
                param.OracleDbType = OracleDbType.VarChar
                param.Direction = Data.ParameterDirection.Input
                param.Value = username
                paramCollection.Add(param)

                param = New OracleParameter
                param.ParameterName = "IN_APPROVALTYPE"
                param.OracleDbType = OracleDbType.VarChar
                param.Direction = Data.ParameterDirection.Input
                param.Value = approvalType
                paramCollection.Add(param)

                param = New OracleParameter
                param.ParameterName = "in_Required"
                param.OracleDbType = OracleDbType.VarChar
                param.Direction = Data.ParameterDirection.Input
                param.Value = Required
                paramCollection.Add(param)


                param = New OracleParameter
                param.ParameterName = "out_status"
                param.OracleDbType = OracleDbType.Number
                param.Direction = Data.ParameterDirection.Output
                paramCollection.Add(param)
                'TODO: change to production package
                Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.jebgpi.InsertNewApprovalDefault")

                If CDbl(returnStatus) <> 0 Then
                    Throw New Data.DataException("Error Saving Default Approval During StartMOC" & mocNumber & " - " & username)
                End If
                Return returnStatus
            End If
        Catch ex As Exception
            Throw
        End Try
        Return ""
    End Function

    Public Function UpdateEmailDate(ByVal seqid As Integer, ByVal mocnumber As Integer) As String

        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        Try
            param = New OracleParameter
            param.ParameterName = "in_ApprovalSeqId"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = seqid
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_MOCNumber"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = mocnumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.rimoc.UpdateMOCReviewerEmailDate")


            Return returnStatus

            If CDbl(returnStatus) <> 0 Then
                Throw New Data.DataException("Error Saving System Record " & mocnumber)
            End If

        Catch ex As Exception
            Throw
        End Try

    End Function
    Public Function SaveReviewerComments(ByVal seqid As Integer, ByVal mocnumber As Integer, ByVal approval_flag As String, ByVal mycomments As String, ByVal username As String) As String

        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        'Check input paramaters
        Try
            param = New OracleParameter
            param.ParameterName = "in_MOCNumber"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = mocnumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_ApprovalSeqId"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Input
            param.Value = seqid
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_username"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = username
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Comments"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = mycomments
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_approval_flag"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = approval_flag
            paramCollection.Add(param)


            param = New OracleParameter
            param.ParameterName = "out_status"
            param.OracleDbType = OracleDbType.Number
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim returnStatus As String = RI.SharedFunctions.CallDROraclePackage(paramCollection, "Reladmin.rimoc.UpdateMOCReviewerComment")


            Return returnStatus

            If CDbl(returnStatus) <> 0 Then
                Throw New Data.DataException("Error Saving System Record " & mocnumber)
            End If

        Catch ex As Exception
            Throw
        End Try


    End Function

    Public Function PrepareSuperintendentEmail(ByVal mocNumber As Integer,
                             ByVal title As String,
                             ByVal description As String,
                             ByVal owner As String,
                             ByVal MOCType As String,
                             ByVal Costs As String,
                             ByVal Funding As String,
                             ByVal Impact As String,
                             ByVal fromemailaddress As String,
                             ByVal toemailaddress As String,
                             ByVal header As String,
                             ByVal subject As String,
                             ByVal BusinessAreaSuperintendent As String,
                             ByVal SuperintendentComments As String) As Boolean

        Try


            Dim host As String = HttpContext.Current.Request.Url.Host
            Dim hostname As IPHostEntry = Dns.GetHostEntry(host)
            Dim ip As IPAddress() = hostname.AddressList



            Dim FilePath As String = ""
            FilePath = HttpContext.Current.Server.MapPath("~\\EmailTemplates\\SuperintendentApprover.html")
            Debug.Print(FilePath)

            Dim str As New StreamReader(FilePath)
            Dim MailText As String = str.ReadToEnd()
            str.Close()

            MailText = MailText.Replace("[mocnumber]", mocNumber.ToString.Trim())
            MailText = MailText.Replace("[Title]", title.ToString.Trim())
            MailText = MailText.Replace("[Description]", description.ToString.Trim())
            MailText = MailText.Replace("[Initiator]", owner.ToString.Trim())
            MailText = MailText.Replace("[MOCType]", MOCType.ToString.Trim())
            MailText = MailText.Replace("[Cost]", Costs.ToString.Trim())
            MailText = MailText.Replace("[Funding]", Funding.ToString.Trim())
            MailText = MailText.Replace("[Impact]", Impact.ToString.Trim())
            MailText = MailText.Replace("[BusinessAreaSuperintendent]", BusinessAreaSuperintendent.ToString.Trim())
            MailText = MailText.Replace("[SuperintendentComments]", SuperintendentComments.ToString.Trim())




            Dim urlstuff As String
            urlstuff = "Authority: " & HttpContext.Current.Request.Url.Authority & Environment.NewLine
            urlstuff += "AbsolutePath: " & HttpContext.Current.Request.Url.AbsolutePath & Environment.NewLine
            urlstuff += "AbsoluteUri: " & HttpContext.Current.Request.Url.AbsoluteUri & Environment.NewLine
            urlstuff += "Port: " & HttpContext.Current.Request.Url.Port & Environment.NewLine
            urlstuff += "OriginalString: " & HttpContext.Current.Request.Url.OriginalString & Environment.NewLine
            urlstuff += "LocalPath: " & HttpContext.Current.Request.Url.LocalPath & Environment.NewLine

            MailText = MailText.Replace("[urlstuff]", urlstuff.ToString.Trim())


            Dim strheader As String
            Dim strheaderwarning As String


            'TODO: figure out to check if we are on a test system

            If HttpContext.Current.Request.Url.IdnHost = "localhost" Or HttpContext.Current.Request.Url.IdnHost = "127.0.0.1" Then
                strheaderwarning = "WARNING WARNING WARNING - This is a test MOC <br />"
                MailText = MailText.Replace("[Headerwarning]", strheaderwarning.ToString.Trim())
            Else
                MailText = MailText.Replace("[Headerwarning]", "")
            End If


            strheader = header '"You have been selected as a Superintendent approver. <br />Please review this MOC and approve or deny."
            MailText = MailText.Replace("[Header]", strheader.ToString.Trim())


            Dim urlstring As String = "http://gpiri.graphicpkg.com/moc/EnterMOC.aspx?MOCNumber=22935#Approval"

            Dim urlScheme As String = HttpContext.Current.Request.Url.Scheme  'http
            Dim urlAuthority As String = HttpContext.Current.Request.Url.Authority  'server address and port
            Dim urlLocalPath As String = HttpContext.Current.Request.Url.LocalPath  'path

            Dim strURL As String = ""  '= HttpContext.Current.Request.Url.AbsolutePath & "?MOCNumber=" & mocNumber

            strURL = urlScheme & "://"
            strURL += urlAuthority
            strURL += urlLocalPath
            strURL += "?MOCNumber=" & mocNumber

            MailText = MailText.Replace("[hrefurl]", strURL.ToString.Trim())


            Dim _mailmsg As New MailMessage()

            'Make TRUE because our body text Is html
            _mailmsg.IsBodyHtml = True

            _mailmsg.From = New MailAddress(fromemailaddress.ToString.Trim())

            _mailmsg.To.Add(toemailaddress)
            _mailmsg.Subject = subject & title.ToString.Trim()


            _mailmsg.Body = MailText


            Dim emailTryCount As Integer = 0
            Dim emailSuccess As Boolean = False
            Do While emailTryCount < 5 And emailSuccess = False
                Dim client As SmtpClient = New SmtpClient()
                Try
                    With client
                        emailTryCount += 1
                        .Timeout = 1000000
                        .Send(_mailmsg)
                        emailSuccess = True
                    End With
                Catch ex As SmtpException
                    HttpContext.Current.Server.ClearError()
                    System.Threading.Thread.Sleep(1000)

                Finally
                    client = Nothing
                End Try

            Loop




        Catch ex As Exception
            Return False
        End Try

        Return True

    End Function



    Public Function PrepareCreatorEmail(ByVal mocNumber As Integer,
                             ByVal title As String,
                             ByVal description As String,
                             ByVal owner As String,
                             ByVal MOCType As String,
                             ByVal Costs As String,
                             ByVal Funding As String,
                             ByVal Impact As String,
                             ByVal fromemailaddress As String,
                             ByVal toemailaddress As String,
                             ByVal header As String,
                             ByVal subject As String,
                             ByVal BusinessAreaSuperintendent As String,
                             ByVal SuperintendentComments As String,
                             ByVal hasApproval As Boolean) As Boolean
        Try


            Dim host As String = HttpContext.Current.Request.Url.Host
            Dim hostname As IPHostEntry = Dns.GetHostEntry(host)
            Dim ip As IPAddress() = hostname.AddressList



            Dim FilePath As String = ""
            FilePath = HttpContext.Current.Server.MapPath("~\\EmailTemplates\\SuperintendentApprover.html")
            Debug.Print(FilePath)

            Dim str As New StreamReader(FilePath)
            Dim MailText As String = str.ReadToEnd()
            str.Close()

            MailText = MailText.Replace("[mocnumber]", mocNumber.ToString.Trim())
            MailText = MailText.Replace("[Title]", title.ToString.Trim())
            MailText = MailText.Replace("[Description]", description.ToString.Trim())
            MailText = MailText.Replace("[Initiator]", owner.ToString.Trim())
            MailText = MailText.Replace("[MOCType]", MOCType.ToString.Trim())
            MailText = MailText.Replace("[Cost]", Costs.ToString.Trim())
            MailText = MailText.Replace("[Funding]", Funding.ToString.Trim())
            MailText = MailText.Replace("[Impact]", Impact.ToString.Trim())
            MailText = MailText.Replace("[BusinessAreaSuperintendent]", BusinessAreaSuperintendent.ToString.Trim())
            MailText = MailText.Replace("[SuperintendentComments]", SuperintendentComments.ToString.Trim())




            Dim urlstring As String = "http://gpiri.graphicpkg.com/moc/EnterMOC.aspx?MOCNumber=22935#Approval"

            Dim urlScheme As String = HttpContext.Current.Request.Url.Scheme  'http
            Dim urlAuthority As String = HttpContext.Current.Request.Url.Authority  'server address and port
            Dim urlLocalPath As String = HttpContext.Current.Request.Url.LocalPath  'path

            Dim strURL As String = ""

            If hasApproval = True Then
                strURL = urlScheme & "://"
                strURL += urlAuthority
                strURL += "/MOC/EnterMOC.aspx"
                strURL += "?MOCNumber=" & mocNumber
            Else

                strURL = urlScheme & "://"
                strURL += urlAuthority
                strURL += urlLocalPath
                strURL += "?MOCNumber=" & mocNumber
            End If
            MailText = MailText.Replace("[hrefurl]", strURL.ToString.Trim())


            Dim strheader As String
            Dim strheaderwarning As String


            Dim ServiceName As String = "Database ({0})"
            ServiceName = String.Format(ServiceName, RI.SharedFunctions.GetDatabaseName())
            If ServiceName.Contains("GPCIOD02") Then
                'testing DB
                strheaderwarning = "WARNING WARNING WARNING - This is a test MOC <br />"
                MailText = MailText.Replace("[Headerwarning]", strheaderwarning.ToString.Trim())

            Else
                MailText = MailText.Replace("[Headerwarning]", "")
            End If

            strheader = header '"You have been selected as a Superintendent approver. <br />Please review this MOC and approve or deny."
            MailText = MailText.Replace("[Header]", strheader.ToString.Trim())




            Dim _mailmsg As New MailMessage()

            'Make TRUE because our body text Is html
            _mailmsg.IsBodyHtml = True

            _mailmsg.From = New MailAddress(fromemailaddress.ToString.Trim())

            _mailmsg.To.Add(toemailaddress)
            _mailmsg.Subject = subject & title.ToString.Trim()


            _mailmsg.Body = MailText


            Dim emailTryCount As Integer = 0
            Dim emailSuccess As Boolean = False
            Do While emailTryCount < 5 And emailSuccess = False
                Dim client As SmtpClient = New SmtpClient()
                Try
                    With client
                        emailTryCount += 1
                        .Timeout = 1000000
                        .Send(_mailmsg)
                        emailSuccess = True
                    End With
                Catch ex As SmtpException
                    HttpContext.Current.Server.ClearError()
                    System.Threading.Thread.Sleep(1000)

                Finally
                    client = Nothing
                End Try

            Loop




        Catch ex As Exception
            Return False
        End Try

        Return True

    End Function



End Class
