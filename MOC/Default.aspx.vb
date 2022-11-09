Option Explicit On
Option Strict On
Imports RI
Partial Class _Default
    Inherits RIBasePage

    Dim ViewUpdate As clsViewUpdate

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        Dim username As String = CurrentUserProfile.GetCurrentUser
        RI.SharedFunctions.InsertAuditRecord("Default.aspx Page_Init", "USER: " & username & " URL: " & Request.Url.OriginalString & "  Time: " & Date.Now)

        If Request.Url.OriginalString = "http://gpiri.graphicpkg.com/Default.aspx" Then
            RI.SharedFunctions.ResponseRedirect("~/ri/ViewUpdateSearch.aspx")
        Else
            RI.SharedFunctions.InsertAuditRecord("Default.aspx Page_Init", "USER: " & username & " TEST System: " & Request.Url.OriginalString & "  Time: " & Date.Now)
        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub ButtonYes_Click(sender As Object, e As EventArgs) Handles ButtonYes.Click
        Dim username As String = CurrentUserProfile.GetCurrentUser
        RI.SharedFunctions.InsertAuditRecord("Default.aspx ButtonYes_Click", "USER: " & username & " Using TEST System" & "  Time: " & Date.Now)
        Session("YesToTest") = "YES"
        RI.SharedFunctions.ResponseRedirect("~/moc/ViewMOC.aspx")
    End Sub


End Class
