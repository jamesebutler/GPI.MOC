
Partial Class MOC_ReviewerBUMUpdates
    Inherits System.Web.UI.UserControl


    Private _dataItem As Object = Nothing

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim vdate As String = _dataItem.Row.ItemArray(8)
        'Dim vdate1 As Date = _dataItem.Row.Table("aaa")

        If _dataItem Is Nothing Then
            ''do nothing
        Else
            'look at PACKAGE RIMOC 
            'get approval type
            Dim i As Integer = _dataItem.Row.Table.Columns("approval_type").Ordinal
            Dim reviewerType As String = _dataItem.Row.ItemArray(i).ToString

            Dim iapproval As Integer = DataItem.Row.Table.Columns("APPROVED_FULL").Ordinal
            Dim approvalValue As String = _dataItem.Row.ItemArray(iapproval).ToString

            If reviewerType = "Informed" Then
                ''ddlApproval.Visible = False
                ddlApproval.Items.Insert(0, New ListItem("Reviewed", "Reviewed"))
            Else
                ddlApproval.Items.Insert(0, New ListItem("Select", ""))
                ddlApproval.Items.Insert(1, New ListItem("YES", "YES"))
                ddlApproval.Items.Insert(2, New ListItem("NO", "NO"))

            End If

            If approvalValue = "" Then
                'do nothing
            Else
                ddlApproval.SelectedValue = approvalValue
            End If

            'get the value of approver
            '_dataItem.Row.Table.Columns("APPROVED_FULL").Ordinal
        End If



    End Sub

    Public Property DataItem() As Object
        Get
            Return Me._dataItem
        End Get
        Set(ByVal value As Object)
            Me._dataItem = value
        End Set
    End Property

End Class
