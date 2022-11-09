Imports Devart.Data.Oracle

Partial Class ucMOCCategory
    Inherits System.Web.UI.UserControl




    'Protected WithEvents chkBoxLst As System.Web.UI.WebControls.CheckBox
    'Protected WithEvents chkBoxList2 As System.Web.UI.WebControls.CheckBoxList
    Public Enum MOCMode
        Search = 0
        Enter = 1
    End Enum
    Private mDisplayMode As MOCMode = MOCMode.Search
    Public Property DisplayMode() As MOCMode
        Get
            Return mDisplayMode
        End Get
        Set(ByVal value As MOCMode)
            mDisplayMode = value
        End Set
    End Property
    Public Property Category() As String
        Get

            Dim strCat As String = Nothing



            If _cbCategoryNewchemicalapproval.Checked Then
                strCat = strCat & _cbCategoryNewchemicalapproval.Text & ","
            End If

            If _cbCOE.Checked Then
                strCat = strCat & _cbCOE.Text & ","
            End If

            If _cbEquipment.Checked Then
                strCat = strCat & _cbEquipment.Text & ","
            End If

            If _cbOther.Checked Then
                strCat = strCat & _cbOther.Text & ","
            End If

            If _cbProcedure.Checked Then
                strCat = strCat & _cbProcedure.Text & ","
            End If

            If _cbProcess.Checked Then
                strCat = strCat & _cbProcess.Text & ","
            End If

            If _cbProcessControl.Checked Then
                strCat = strCat & _cbProcessControl.Text & ","
            End If

            If _cbProductTrial.Checked Then
                strCat = strCat & _cbProductTrial.Text & ","
            End If

            If _cbMarketChannel.Checked Then
                strCat = strCat & _cbMarketChannel.Text & ","
            End If
            If _cbStoreroom.Checked Then
                strCat = strCat & _cbStoreroom.Text & ","
            End If


            If strCat <> "" Then
                strCat = Mid(strCat, 1, strCat.Length - 1)
            End If
            Return strCat
        End Get
        Set(ByVal value As String)

            If DisplayMode = MOCMode.Search Then
                If value.Length > 0 Then

                    Dim listCategory As String() = value.Split(CChar(","))
                    If listCategory.Length > 0 Then

                        For j As Integer = 0 To listCategory.Length - 1
                            If (_cbCategoryNewchemicalapproval.Text = listCategory(j)) Then _cbCategoryNewchemicalapproval.Checked = True
                            If (_cbCOE.Text = listCategory(j)) Then _cbCOE.Checked = True
                            If (_cbEquipment.Text = listCategory(j)) Then _cbEquipment.Checked = True
                            If (_cbOther.Text = listCategory(j)) Then _cbOther.Checked = True
                            If (_cbProcedure.Text = listCategory(j)) Then _cbProcedure.Checked = True
                            If (_cbProcess.Text = listCategory(j)) Then _cbProcess.Checked = True
                            If (_cbProcessControl.Text = listCategory(j)) Then _cbProcessControl.Checked = True
                            If (_cbProductTrial.Text = listCategory(j)) Then _cbProductTrial.Checked = True
                            If (_cbMarketChannel.Text = listCategory(j)) Then _cbMarketChannel.Checked = True
                            If (_cbStoreroom.Text = listCategory(j)) Then _cbStoreroom.Checked = True

                        Next
                    End If
                End If

            End If





        End Set

    End Property
    Public Property SubCategory() As String
        Get

            Dim strCat As String = Nothing
            Dim strCatFinal As String = Nothing



            If _cbsubNewchemicalapproval.Checked Then strCat = strCat & _cbsubNewchemicalapproval.Text & ","
            If _cbsubCOEAlert.Checked Then strCat = strCat & _cbsubCOEAlert.Text & ","
            If _cbsubCOEManufacturing.Checked Then strCat = strCat & _cbsubCOEManufacturing.Text & ","
            If _cbsubCapitalProject.Checked Then strCat = strCat & _cbsubCapitalProject.Text & ","
            If _cbsubGMSCertification.Checked Then strCat = strCat & _cbsubGMSCertification.Text & ","
            If _cbsubOther.Checked Then strCat = strCat & _cbsubOther.Text & ","
            If _cbsubBasicCare.Checked Then strCat = strCat & _cbsubBasicCare.Text & ","
            If _cbsubPM.Checked Then strCat = strCat & _cbsubPM.Text & ","
            If _cbsubPdM.Checked Then strCat = strCat & _cbsubPdM.Text & ","
            If _cbsubSOP.Checked Then strCat = strCat & _cbsubSOP.Text & ","
            If _cbsubShutdownAbandon.Checked Then strCat = strCat & _cbsubShutdownAbandon.Text & ","
            If _cbsubTCC.Checked Then strCat = strCat & _cbsubTCC.Text & ","
            If _cbsubKOP.Checked Then strCat = strCat & _cbsubKOP.Text & ","
            If _cbsubGradeSpecification.Checked Then strCat = strCat & _cbsubGradeSpecification.Text & ","
            If _cbsubMeasurementTechnique.Checked Then strCat = strCat & _cbsubMeasurementTechnique.Text & ","
            If _cbsubProductrecipe.Checked Then strCat = strCat & _cbsubProductrecipe.Text & ","
            If _cbsubSetpoint.Checked Then strCat = strCat & _cbsubSetpoint.Text & ","
            If _cbsubCLPM.Checked Then strCat = strCat & _cbsubCLPM.Text & ","
            If _cbsubDCS.Checked Then strCat = strCat & _cbsubDCS.Text & ","
            If _cbsubHMIGraphics.Checked Then strCat = strCat & _cbsubHMIGraphics.Text & ","
            If _cbsubInstrumentLoops.Checked Then strCat = strCat & _cbsubInstrumentLoops.Text & ","
            If _cbsubLogicchanges.Checked Then strCat = strCat & _cbsubLogicchanges.Text & ","
            If _cbsubPARCView.Checked Then strCat = strCat & _cbsubPARCView.Text & ","
            If _cbsubPI.Checked Then strCat = strCat & _cbsubPI.Text & ","
            If _cbsubPLC.Checked Then strCat = strCat & _cbsubPLC.Text & ","
            If _cbsubProficy.Checked Then strCat = strCat & _cbsubProficy.Text & ","
            If _cbsubTuning.Checked Then strCat = strCat & _cbsubTuning.Text & ","
            If _cbsubNewCustomer.Checked Then strCat = strCat & _cbsubNewCustomer.Text & ","
            If _cbsubNewProduct.Checked Then strCat = strCat & _cbsubNewProduct.Text & ","
            If _cbsubProductAffecting.Checked Then strCat = strCat & _cbsubProductAffecting.Text & ","
            If _cbsubQualifications.Checked Then strCat = strCat & _cbsubQualifications.Text & ","
            If _cbsubRollHandling.Checked Then strCat = strCat & _cbsubRollHandling.Text & ","
            If _cbsubChangeMaterialDescription.Checked Then strCat = strCat & _cbsubChangeMaterialDescription.Text & ","
            If _cbsubExtend.Checked Then strCat = strCat & _cbsubExtend.Text & ","
            If _cbsubMaterialAdds.Checked Then strCat = strCat & _cbsubMaterialAdds.Text & ","
            If _cbsubMaterialDelete.Checked Then strCat = strCat & _cbsubMaterialDelete.Text & ","
            If _cbsubMinMaxChange.Checked Then strCat = strCat & _cbsubMinMaxChange.Text & ","
            If _cbsubPurchasingVendorMaterialSpec.Checked Then strCat = strCat & _cbsubPurchasingVendorMaterialSpec.Text & ","



            If strCat <> "" Then
                strCatFinal = Mid(strCat, 1, strCat.Length - 1)
            End If

            Return strCatFinal
            'Return strCat
        End Get
        Set(ByVal value As String)


            If value = Nothing Then
                'do nothing
            Else
                Dim listSubCategory As String() = value.Split(CChar(","))
                If listSubCategory.Length > 0 Then

                    For j As Integer = 0 To listSubCategory.Length - 1
                        If _cbsubNewchemicalapproval.Text = listSubCategory(j) Then _cbsubNewchemicalapproval.Checked = True
                        If _cbsubCapitalProject.Text = listSubCategory(j) Then _cbsubCapitalProject.Checked = True
                        If _cbsubGMSCertification.Text = listSubCategory(j) Then _cbsubGMSCertification.Checked = True
                        If _cbsubOther.Text = listSubCategory(j) Then _cbsubOther.Checked = True
                        If _cbsubBasicCare.Text = listSubCategory(j) Then _cbsubBasicCare.Checked = True
                        If _cbsubPM.Text = listSubCategory(j) Then _cbsubPM.Checked = True
                        If _cbsubPdM.Text = listSubCategory(j) Then _cbsubPdM.Checked = True
                        If _cbsubSOP.Text = listSubCategory(j) Then _cbsubSOP.Checked = True
                        If _cbsubShutdownAbandon.Text = listSubCategory(j) Then _cbsubShutdownAbandon.Checked = True
                        If _cbsubTCC.Text = listSubCategory(j) Then _cbsubTCC.Checked = True
                        If _cbsubKOP.Text = listSubCategory(j) Then _cbsubKOP.Checked = True
                        If _cbsubGradeSpecification.Text = listSubCategory(j) Then _cbsubGradeSpecification.Checked = True
                        If _cbsubMeasurementTechnique.Text = listSubCategory(j) Then _cbsubMeasurementTechnique.Checked = True
                        If _cbsubProductrecipe.Text = listSubCategory(j) Then _cbsubProductrecipe.Checked = True
                        If _cbsubSetpoint.Text = listSubCategory(j) Then _cbsubSetpoint.Checked = True
                        If _cbsubCLPM.Text = listSubCategory(j) Then _cbsubCLPM.Checked = True
                        If _cbsubDCS.Text = listSubCategory(j) Then _cbsubDCS.Checked = True
                        If _cbsubHMIGraphics.Text = listSubCategory(j) Then _cbsubHMIGraphics.Checked = True
                        If _cbsubInstrumentLoops.Text = listSubCategory(j) Then _cbsubInstrumentLoops.Checked = True
                        If _cbsubLogicchanges.Text = listSubCategory(j) Then _cbsubLogicchanges.Checked = True
                        If _cbsubPARCView.Text = listSubCategory(j) Then _cbsubPARCView.Checked = True
                        If _cbsubPI.Text = listSubCategory(j) Then _cbsubPI.Checked = True
                        If _cbsubPLC.Text = listSubCategory(j) Then _cbsubPLC.Checked = True
                        If _cbsubProficy.Text = listSubCategory(j) Then _cbsubProficy.Checked = True
                        If _cbsubTuning.Text = listSubCategory(j) Then _cbsubTuning.Checked = True
                        If _cbsubNewCustomer.Text = listSubCategory(j) Then _cbsubNewCustomer.Checked = True
                        If _cbsubNewProduct.Text = listSubCategory(j) Then _cbsubNewProduct.Checked = True
                        If _cbsubProductAffecting.Text = listSubCategory(j) Then _cbsubProductAffecting.Checked = True
                        If _cbsubQualifications.Text = listSubCategory(j) Then _cbsubQualifications.Checked = True
                        If _cbsubRollHandling.Text = listSubCategory(j) Then _cbsubRollHandling.Checked = True
                        If _cbsubChangeMaterialDescription.Text = listSubCategory(j) Then _cbsubChangeMaterialDescription.Checked = True
                        If _cbsubExtend.Text = listSubCategory(j) Then _cbsubExtend.Checked = True
                        If _cbsubMaterialAdds.Text = listSubCategory(j) Then _cbsubMaterialAdds.Checked = True
                        If _cbsubMaterialDelete.Text = listSubCategory(j) Then _cbsubMaterialDelete.Checked = True
                        If _cbsubMinMaxChange.Text = listSubCategory(j) Then _cbsubMinMaxChange.Checked = True
                        If _cbsubPurchasingVendorMaterialSpec.Text = listSubCategory(j) Then _cbsubPurchasingVendorMaterialSpec.Checked = True

                        If _cbsubCOEAlert.Text = listSubCategory(j) Then _cbsubCOEAlert.Checked = True
                        If _cbsubCOEManufacturing.Text = listSubCategory(j) Then _cbsubCOEManufacturing.Checked = True

                    Next
                End If
            End If


        End Set

    End Property
    Public Property EquipSubCategory() As String
        Get

            Dim strEquipCat As String = Nothing
            Dim strCatFinal As String = Nothing

            strEquipCat = _ddlSubEquipment.SelectedValue

            Return strEquipCat

        End Get

        Set(ByVal value As String)


            If value = "" Then
                ' do nothing
            Else
                _ddlSubEquipment.SelectedValue = value
            End If

        End Set

    End Property

    Public Property MarketChannelSubCategory() As String
        Get

            Dim strEquipCat As String = Nothing
            Dim strCatFinal As String = Nothing


            strEquipCat = _ddlSubMarketChannel.SelectedValue


            Return strEquipCat
        End Get

        Set(ByVal value As String)



            If value = "" Then
                ' do nothing
            Else
                _ddlSubMarketChannel.SelectedValue = value
            End If

        End Set

    End Property
    Public Property VisibleDropDown() As Boolean
        Get
            Return Me._lblCategory.Visible
        End Get
        Set(ByVal value As Boolean)
            Me._lblCategory.Visible = value
        End Set
    End Property
    Public Property enablePanel() As Boolean
        Get
            Return Me.moccategoryPanel.Enabled
        End Get
        Set(ByVal value As Boolean)
            Me.moccategoryPanel.Enabled = value

        End Set
    End Property


    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'If Page.IsPostBack = False Or Me.IsViewStateEnabled = False Then
        '    PopulateCategory()
        'End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.ClientScript.IsClientScriptIncludeRegistered(Page.GetType, "MOC") Then
            Page.ClientScript.RegisterClientScriptInclude(Page.GetType, "MOC", Page.ResolveClientUrl("~/MOC/MOC.js"))
        End If

        If Not IsPostBack Then
            'PopulateCategory()
        End If




    End Sub
    Public Sub RefreshDisplay()
        'PopulateCategory()
    End Sub

    'Private Sub PopulateCategory(Optional ByVal userName As String = "", Optional ByVal facility As String = "")

    '    Exit Sub


    '    Dim paramCollection As New OracleParameterCollection
    '    Dim param As New OracleParameter
    '    Dim ds As New System.Data.DataSet
    '    Dim ipLoc As New IP.Bids.Localization.WebLocalization()

    '    Try


    '        param = New OracleParameter
    '        param.ParameterName = "rsMOCCategory"
    '        param.OracleDbType = OracleDbType.Cursor
    '        param.Direction = Data.ParameterDirection.Output
    '        paramCollection.Add(param)

    '        param = New OracleParameter
    '        param.ParameterName = "rsMOCSubCategory"
    '        param.OracleDbType = OracleDbType.Cursor
    '        param.Direction = Data.ParameterDirection.Output
    '        paramCollection.Add(param)

    '        'ds = New dataset
    '        Dim key As String = "ViewMOC.CatSubCatList" ' & facility & "_" & division & "_" & inActiveFlag & "_" & userName

    '        ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.ViewMOC.CatSubCatList", key, 3)


    '    Catch ex As Exception
    '        Throw
    '    Finally
    '        If ds IsNot Nothing Then
    '            ds.Dispose()
    '            ds = Nothing
    '        End If
    '        paramCollection = Nothing
    '    End Try
    'End Sub


End Class
