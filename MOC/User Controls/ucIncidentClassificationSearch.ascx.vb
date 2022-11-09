
Partial Class ucIncidentClassificationSearch
    Inherits System.Web.UI.UserControl

    Dim Trigger As String
    Dim Type As String
    Dim Equipment As String

    Dim Component As String

    Dim Category As String



    Public Event IncidentClassificationChanged()
    Private mCascadingList As Boolean = False
    Public Property CascadingLists() As Boolean
        Get
            Return mCascadingList
        End Get
        Set(ByVal value As Boolean)
            mCascadingList = value
        End Set
    End Property

    Private mAutoPostBack As Boolean
    Public Property AutoPostBack() As Boolean
        Get
            Return mAutoPostBack
        End Get
        Set(ByVal value As Boolean)
            mAutoPostBack = value
        End Set
    End Property
    Private mSelectedType As String = String.Empty
    Public Property SelectedType() As String
        Get
            Return mSelectedType
        End Get
        Set(ByVal value As String)
            mSelectedType = value
        End Set
    End Property
    Private mSelectedProcess As String = String.Empty

    Public Property CategoryValue() As String
        Get
            Return Me._ddlCategory.SelectedValue
        End Get
        Set(ByVal value As String)
            If _ddlCategory.Items.FindByValue(value) IsNot Nothing Then
                _ddlCategory.Items.FindByValue(value).Selected = True
            End If
            Me._cddlCategory.SelectedValue = value
        End Set
    End Property

    Public Property EquipmentValue() As String
        Get
            Return Me._ddlEquipment.SelectedValue
        End Get
        Set(ByVal value As String)
            If _ddlEquipment.Items.FindByValue(value) IsNot Nothing Then
                _ddlEquipment.Items.FindByValue(value).Selected = True
            End If
            Me._cddlEquipment.SelectedValue = value
        End Set
    End Property



    Public Property ComponentValue() As String
        Get
            Return Me._ddlComponent.SelectedValue
        End Get
        Set(ByVal value As String)
            If _ddlComponent.Items.FindByValue(value) IsNot Nothing Then
                _ddlComponent.Items.FindByValue(value).Selected = True
            End If
            Me._cddlComponent.SelectedValue = value
        End Set
    End Property

    Private mTypeData As clsData
    Public Property TypeData() As clsData
        Get
            Return mTypeData
        End Get
        Set(ByVal value As clsData)
            mTypeData = value
        End Set
    End Property


    Private mCategoryData As clsData
    Public Property CategoryData() As clsData
        Get
            Return mCategoryData
        End Get
        Set(ByVal value As clsData)
            mCategoryData = value
        End Set
    End Property


    Private mEquipmentData As clsData
    Public Property EquipmentData() As clsData
        Get
            Return mEquipmentData
        End Get
        Set(ByVal value As clsData)
            mEquipmentData = value
        End Set
    End Property

    Private mComponentData As clsData
    Public Property ComponentData() As clsData
        Get
            Return mComponentData
        End Get
        Set(ByVal value As clsData)
            mComponentData = value
        End Set
    End Property

    Private mTriggerData As clsData
    Public Property TriggerData() As clsData
        Get
            Return mTriggerData
        End Get
        Set(ByVal value As clsData)
            mTriggerData = value
        End Set
    End Property
    Private mShowPrevention As Boolean = True


    Public Overrides Sub DataBind()
        'Dim selectedCause As String = String.Empty
        'Dim selectedProcess As String = String.Empty
        Dim CauseFilter As String = String.Empty
        Dim ProcessFilter As String = String.Empty


        Me._cddlComponent.Enabled = False



    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load


    End Sub



End Class
