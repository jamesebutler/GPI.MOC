  
<%@ Page Title="" 
Language="VB" 
MasterPageFile="~/RI.master" 
AutoEventWireup="false" 
CodeFile="EnterMOC.aspx.vb" 
Inherits="MOC_EnterMOCNew" 
EnableViewState="true" 
UnobtrusiveValidationMode="None"
%>


<%@ MasterType VirtualPath="~/RI.master" %>


<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="IP" Namespace="AdvancedTextBox" Assembly="AdvancedTextBox" %>


<%@ Register Src="~/User Controls/Common/ucMOCStatus.ascx" TagName="ucMOCStatus" TagPrefix="IP" %>
<%@ Register Src="~/User Controls/Common/ucMOCSwapListBox.ascx" TagName="ucMOCSwapListBox" TagPrefix="IP" %>
<%@ Register Src="~/User Controls/Common/UcMTTResponsible.ascx" TagName="Responsible" TagPrefix="IP" %>
<%@ Register Src="~/User Controls/Common/UcJQDate.ascx" TagName="JQDate" TagPrefix="IP" %>
<%@ Register Src="~/User Controls/Common/ucMessageBox.ascx" TagName="MessageBox" TagPrefix="IP" %>
<%--<%@ Register Src="~/User Controls/Common/ucFunctionalLocationTree.ascx" TagName="FunctionalLocationTree" TagPrefix="IP" %>--%>
<%@ Register Src="~/User Controls/Common/ucMOCTypes.ascx" TagName="MOCType" TagPrefix="IP" %>
<%@ Register Src="~/User Controls/Common/ucSpellcheck.ascx" TagName="SpellCheck" TagPrefix="IP" %>
<%--<%@ Register Src="~/User Controls/ucECSearch.ascx" TagName="FunctionalLocation" TagPrefix="IP" %>--%>
<%@ Register Src="~/User Controls/Common/ucMOCCategory.ascx" TagName="MOCCategory" TagPrefix="IP" %>
<%@ Register Src="~/User Controls/Common/ucMOCClassification.ascx" TagName="MOCClass" TagPrefix="IP" %>

<%@ Register Src="~/User Controls/Common/UcMOCDate.ascx" TagName="MOCDate" TagPrefix="IP" %>



<asp:Content ID="Content1" ContentPlaceHolderID="_cphMain" runat="Server">

<telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" RenderMode="Lightweight">
</telerik:RadStyleSheetManager>

<telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1"  runat="server" >


        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGridCurrentApprover">
            <UpdatedControls>
            <telerik:AjaxUpdatedControl ControlID="RadGridCurrentApprover" 
                LoadingPanelID="RadAjaxLoadingPanel"  />
            </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="ConfiguratorPanel">
            <UpdatedControls>
            <telerik:AjaxUpdatedControl ControlID="RadGridCurrentApprover" 
                LoadingPanelID="RadAjaxLoadingPanel" />
            </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="_udpRadGridCurrentApprover">
            <UpdatedControls>
            <telerik:AjaxUpdatedControl ControlID="_udpRadGridCurrentApprover" 
                LoadingPanelID="RadAjaxLoadingPanel"  />
            </UpdatedControls>
            </telerik:AjaxSetting>


            



            <telerik:AjaxSetting AjaxControlID="RadGridAssignedApprover">
            <UpdatedControls>
            <telerik:AjaxUpdatedControl ControlID="RadGridAssignedApprover" 
                LoadingPanelID="RadAjaxLoadingPanel" UpdatePanelCssClass="" />
            </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>


</telerik:RadAjaxManagerProxy>

<telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel">
</telerik:RadAjaxLoadingPanel>

<asp:UpdatePanel ID="_udpLocation" runat="server" UpdateMode="Conditional">
<ContentTemplate>

<asp:HiddenField ID="_hfMOCType" runat="server" />

<ajaxToolkit:CascadingDropDown ID="_cddlFacility" runat="server" Category="SiteID"
LoadingText="" PromptText='<%# IIf(CBool(Eval("IsBlocked")), "", "[Please select]") %>' ServiceMethod="GetFacilityList" ServicePath="~/CascadingLists.asmx"
TargetControlID="_ddlFacility" UseContextKey="true"></ajaxToolkit:CascadingDropDown>

<ajaxToolkit:CascadingDropDown ID="_cddlBusArea" runat="server" Category="BusArea"
LoadingText="" PromptText="    " ServiceMethod="GetBusArea" ServicePath="~/CascadingLists.asmx"
TargetControlID="_ddlBusinessUnit" ParentControlID="_ddlFacility"></ajaxToolkit:CascadingDropDown>

<ajaxToolkit:CascadingDropDown ID="_cddlLineBreak" runat="server" Category="LineBreak"
LoadingText="" PromptText="    " ServiceMethod="GetLineBreak" ServicePath="~/CascadingLists.asmx"
TargetControlID="_ddlLineBreak" ParentControlID="_ddlBusinessUnit"></ajaxToolkit:CascadingDropDown>

<ajaxToolkit:CascadingDropDown ID="_ccdlOwner" runat="server" Category="Employee"
LoadingText="" PromptText="    " ServiceMethod="GetEmployee" ServicePath="~/CascadingLists.asmx"
TargetControlID="_ddlOwner" ParentControlID="_ddlFacility"></ajaxToolkit:CascadingDropDown>



<asp:Table ID="table" runat="server" Width="100%" 
CellPadding="0" CellSpacing="0" BorderWidth="0" BorderColor="Red" BorderStyle="Solid">
<asp:TableRow>
<asp:TableCell>
<asp:Table ID="_tblDesc" runat="server" CellPadding="2" CellSpacing="0" Width="100%">
<asp:TableRow CssClass="Border" HorizontalAlign="center">
<asp:TableCell ColumnSpan="3">
<asp:Label ID="_lblDesc" runat="server" ForeColor="#8A2020" EnableViewState="false" />
</asp:TableCell>
</asp:TableRow>
</asp:Table>

<asp:Table ID="_tblMain" runat="server" CellPadding="6" CellSpacing="2"  BorderWidth="0" 
BorderColor="Green" BorderStyle="Dotted"
Width="100%" >
<asp:TableRow CssClass="Border" >
<asp:TableCell Width="20%" VerticalAlign="Top">
<span class="ValidationError">*</span>
<asp:Label ID="_lbOwner" runat="server" Text="MOC Project Manager" EnableViewState="false" />

<%--<asp:Label ID="_lbOwner" runat="server" Text='<%$RIResources:Shared,MOC Owner %>' 
EnableViewState="false" />--%>
&nbsp;
<asp:DropDownList ID="_ddlOwner" Width="200" runat="server"></asp:DropDownList>
<asp:RequiredFieldValidator ValidationGroup="EnterMOC" ID="_rfvOwner" runat="server"
Display="none" ControlToValidate="_ddlOwner" ErrorMessage="Please select a MOC Project Manager"
EnableClientScript="true" SetFocusOnError="true" Text="Please select a MOC Project Manager">
</asp:RequiredFieldValidator>
</asp:TableCell>

<asp:TableCell ID="_tcStatus"  >
</asp:TableCell>

<asp:TableCell VerticalAlign="Top">
<IP:ucMOCStatus runat="server" ID="MOCStatus" name="MOCStatus" DisplayMode="Enter" Visible="false" AllowPostBack="true" EnableValidation="false" />



</asp:TableCell>
</asp:TableRow>

<asp:TableRow CssClass="BorderTopLine" Visible="true">

<%--kickoff date is not shown at this time JEB 7/12/2022--%>
<asp:TableCell Visible="false">
<span class="ValidationError">*</span>
<asp:Label ID="_lblProjectKickoffDate" runat="server" Text="Project Kickoff Date" EnableViewState="false"></asp:Label>
&nbsp;
<telerik:RadDatePicker ID="_tbMOCProjectKickoffDate" runat="server" 
        RenderMode="Lightweight" Skin="Telerik">
       <HideAnimation Duration="111" />
    <ShowAnimation Duration="300" />
</telerik:RadDatePicker>
<asp:RequiredFieldValidator runat="server" ID="validatorMOCProjectKickoffDate"
Display="none"
ValidationGroup="EnterMOC"
EnableClientScript="true" SetFocusOnError="true"
            ControlToValidate="_tbMOCProjectKickoffDate"
            ErrorMessage="Please enter Project Kickoff Date">
            </asp:RequiredFieldValidator>



</asp:TableCell>


<asp:TableCell>

<asp:Label ID="_lblEndDate" runat="server" 
Text='<%$RIResources:Shared,Implementation Date %>' 
EnableViewState="false"></asp:Label>
&nbsp;	
<telerik:RadDatePicker ID="_tbMOCImplementationDate" runat="server" 
        RenderMode="Lightweight" Skin="Telerik">
       <HideAnimation Duration="111" />
        <ShowAnimation Duration="300" />
</telerik:RadDatePicker>

</asp:TableCell>

<asp:TableCell>
<asp:Label ID="_lbCompDate" runat="server" Width="175px" Text='<%$RIResources:Shared,Completion Date%>'
EnableViewState="false" Visible="false" />

<asp:TextBox ID="_tbCompDate" runat="server" Width="130px"
Enabled="true" BackColor="lightGray" Visible="false"></asp:TextBox></asp:TableCell>

</asp:TableRow>



<asp:TableRow CssClass="BorderTopLine" BackColor=""   >
<asp:TableCell VerticalAlign="Middle" Width="30%">
<span class="ValidationError">*</span>

<asp:TextBox ID="_tbDivision" runat="server" AutoPostBack="false"
Width="90%" Style="display: none;">
</asp:TextBox>

<%--Facility--%>
<asp:Label ID="_lblFacility" runat="server"
Text="<%$RIResources:Shared,Facility %>" EnableViewState="false"></asp:Label>  &nbsp;
<asp:DropDownList ID="_ddlFacility" CausesValidation="true" runat="server" AutoPostBack="false"
Width="80%" Enabled='<%# CBool(Eval("IsBlocked")) %>' onchange="updateDivision();">
</asp:DropDownList>
<asp:RequiredFieldValidator ValidationGroup="EnterMOC" ID="_rfvFacility" runat="server"
Display="none" ControlToValidate="_ddlFacility" ErrorMessage="<%$RIResources:Shared,SelectFacility %>"
EnableClientScript="true" SetFocusOnError="true" Text="<%$RIResources:Shared,SelectFacility %>"></asp:RequiredFieldValidator>
</asp:TableCell>

<%--Bus/Area--%>
<asp:TableCell VerticalAlign="Middle" Width="30%">
<span class="ValidationError">*</span>
<asp:Label ID="_lblBusUnit" runat="server" Text="<%$RIResources:Shared,BusArea %>" EnableViewState="false"></asp:Label>
&nbsp;<asp:DropDownList ID="_ddlBusinessUnit" CausesValidation="true" AutoPostBack="false"
EnableViewState="false" Width="80%" onchange="updateFL();" Visible="true"
runat="server" Enabled='<%# CBool(Eval("IsBlocked")) %>' />
<asp:RequiredFieldValidator ValidationGroup="EnterMOC" ID="_rfvBusinessUnit" runat="server"
Display="none" ControlToValidate="_ddlBusinessUnit" ErrorMessage="<%$RIResources:Shared,SelectBusinessUnit %>"
EnableClientScript="true" SetFocusOnError="true" Text="<%$RIResources:Shared,SelectBusinessUnit %>"></asp:RequiredFieldValidator>
</asp:TableCell>


<%--Line/Line Break--%>
<asp:TableCell VerticalAlign="Middle" Width="30%">
<asp:Label ID="_lblLine" runat="server" Text="<%$RIResources:Shared,LineLineBreak %>" EnableViewState="false"></asp:Label>
&nbsp;<asp:DropDownList ID="_ddlLineBreak" CausesValidation="true" AutoPostBack="false"
onchange="updateFL();" Width="60%" runat="server" Enabled='<%# CBool(Eval("IsBlocked")) %>' />
</asp:TableCell>
</asp:TableRow>
</asp:Table>



</asp:TableCell>
</asp:TableRow>


<%--Functional Location Tree
<asp:TableRow CssClass="BorderTopLine" ID="_FL" runat="server" BackColor="">
<asp:TableCell>
<div style="float: left">
<IP:FunctionalLocation id="_functionalLocationTree" runat="server" />
</div>
</asp:TableCell>
</asp:TableRow>--%>



<asp:TableRow CssClass="Border" ID="_trMOCType" runat="server">
<asp:TableCell>


<IP:MOCType id="_MOCType" runat="server" name="moctype" displaymode="Enter" autopostback="true" />
<span id="_divExpirationDate" style="display: none;" runat="server">
<asp:Label runat="server" ID="_lbExpirDate" Text="<%$RIResources:MOC,Expiration Date%>" Visible="false"></asp:Label>
<IP:JQDate runat="server" ID="_tbExpirationDate" FromLabel='<%$RIResources:Shared,Expiration Date %>' Required="false"></IP:JQDate>
<asp:TextBox ID="_tbExpirationDate2" Visible="false" CssClass="PerformanceDateTimePicker" runat="server" Width="15%" Style="text-align: left">   </asp:TextBox>
</span>
</asp:TableCell>
</asp:TableRow>

<asp:TableRow>
<asp:TableCell ColumnSpan="2">


<%--START OF TYPE OF CHANGE--%>
<asp:Table ID="_tbDetail" runat="server" BorderWidth="0" BorderColor="Pink" CellPadding="6" CellSpacing="2"
BackColor="white" Style="width: 100%; overflow: hidden;">

<asp:TableRow CssClass="Border">
<asp:TableCell ID="_tcTitle" runat="server" Width="45%" ColumnSpan="2">
<span class="ValidationError">*</span>
<asp:Label ID="_lblTitle" runat="server" Text="<%$RIResources:MOC,Title %>" EnableViewState="false" />&nbsp;
<asp:TextBox ID="_txtTitle" runat="server" Width="85%" OnTextChanged="FieldChanged"></asp:TextBox>
<asp:RequiredFieldValidator ValidationGroup="EnterMOC" ID="_rfvTitle" runat="server"
Display="none" ControlToValidate="_txtTitle" ErrorMessage="<%$RIResources:Shared,EnterTitle %>"
EnableClientScript="true" SetFocusOnError="true" Text="<%$RIResources:Shared,EnterTitle %>"></asp:RequiredFieldValidator>
</asp:TableCell>
<asp:TableCell Width="15%" ColumnSpan="2">
<asp:Panel ID="Panel1" runat="server" Visible="true">
<asp:Label ID="_lbWorkOrder" runat="server" Text="<%$RIResources:MOC,Work Order %>" />&nbsp;
<asp:TextBox ID="_txtWorkOrder" runat="server" EnableViewState="false" MaxLength="10" />&nbsp;
</asp:Panel>
</asp:TableCell>
</asp:TableRow>

<asp:TableHeaderRow SkinID="rowheader" >
<asp:TableCell HorizontalAlign="left" ColumnSpan="5">
<span class="ValidationError">*</span>
<asp:Label ID="_lblDescription" runat="server" Text="Functional Narrative of proposed work"
EnableViewState="false" />
<%--                                    <asp:Label ID="_lblDescription" runat="server" Text="<%$RIResources:MOC,DescJustification %>"
EnableViewState="false" />--%>
    </asp:TableCell>
    </asp:TableHeaderRow>
    <asp:TableRow>
        <asp:TableCell HorizontalAlign="left" ColumnSpan="5">
<div>
<IP:AdvancedTextBox id="_txtDescription" runat="server" expandheight="true" width="98%"
rows="2" textmode="MultiLine" maxlength="4000" style="width: 98%; font-size: 12px; color: Black; font-family: Verdana;"
ontextchanged="FieldChanged" />
<asp:RequiredFieldValidator ValidationGroup="EnterMOC" ID="_rfvNarrativeofproposedwork" runat="server"
Display="none" ControlToValidate="_txtDescription" ErrorMessage="Please enter Narrative of Proposed Work"
EnableClientScript="true" SetFocusOnError="true" Text="Please enter Narrative of Proposed Work">
</asp:RequiredFieldValidator>
</div>
</asp:TableCell>
</asp:TableRow>
</asp:Table>

  <%-- COST Heading--%>
 <asp:Table ID="_tblCostHeader" runat="Server" 
BorderStyle="Solid" BorderColor="blue" BorderWidth="0" >
<asp:TableHeaderRow SkinID="rowheader" >
    
<asp:TableCell HorizontalAlign="left">
<asp:Label ID="_lblCost" runat="server" EnableViewState="false" Text="Costs" />
</asp:TableCell>
</asp:TableHeaderRow>
</asp:Table>
 <%-- COST Heading END--%>

   <%-- COST --%>
  <asp:Table ID="_tblCost" runat="Server" CssClass="Border"
BorderStyle="Solid" BorderColor="green" BorderWidth="0" CellPadding="5" >
<%-- Estimate Costs--%>


<asp:TableRow ID="_trEstimate1" runat="server" CssClass="Border" Style="width: 99%;">

<asp:TableCell HorizontalAlign="left" Width="35%" VerticalAlign="Top">
<asp:Label ID="_lblMOCSavings" runat="server" EnableViewState="false" Text="<%$RIResources:MOC,EstSavings %>"></asp:Label>
&nbsp;
<b>$</b><asp:TextBox ID="_txtSavings" MaxLength="10" runat="server"></asp:TextBox>

<ajaxToolkit:FilteredTextBoxExtender ID="_feSavings" runat="server" TargetControlID="_txtSavings"
FilterType="custom" ValidChars="()-1234567890"></ajaxToolkit:FilteredTextBoxExtender>
</asp:TableCell>

<asp:TableCell HorizontalAlign="left" Width="35%" VerticalAlign="Top">
<span class="ValidationError">*</span>
<asp:Label ID="_lblMOCCosts" runat="server" EnableViewState="false" Text="<%$RIResources:MOC,EstCosts %>"></asp:Label>
&nbsp;
<b>$</b><asp:TextBox ID="_tbCosts" MaxLength="10" runat="server" OnTextChanged="FieldChanged"></asp:TextBox>
<ajaxToolkit:FilteredTextBoxExtender ID="_feCosts" runat="server" TargetControlID="_tbCosts"
FilterType="custom" ValidChars="()-1234567890"></ajaxToolkit:FilteredTextBoxExtender>
<asp:RequiredFieldValidator ValidationGroup="EnterMOC" ID="_rfvEstimatedCost" runat="server"
Display="none" ControlToValidate="_tbCosts" ErrorMessage="Please enter Estimated Costs"
EnableClientScript="true" SetFocusOnError="true" Text="Please enter Estimated Costs">
</asp:RequiredFieldValidator>

</asp:TableCell>




<asp:TableCell HorizontalAlign="left" Width="35%" VerticalAlign="Middle" ColumnSpan="1"> 
<span class="ValidationError">*</span>&nbsp;
 <asp:Label ID="_lblMOCFinding" runat="server" EnableViewState="false" Text="Funding"></asp:Label>
 &nbsp;
                <asp:DropDownList ID="_ddlFunding" runat="server"  >
                    <asp:ListItem Value= ""  Text=""> </asp:ListItem>
                    <asp:ListItem Value= "Capital Spending" >Capital Spending</asp:ListItem>
                    <asp:ListItem Value= "Indirect Spending" >Indirect Spending</asp:ListItem>
                </asp:DropDownList>
<asp:RequiredFieldValidator ValidationGroup="EnterMOC" ID="_rfv_ddlFunding" runat="server"
Display="none" ControlToValidate="_ddlFunding" ErrorMessage="Please Select Funding"
EnableClientScript="true" SetFocusOnError="true" Text="Please Select Funding">
</asp:RequiredFieldValidator>

</asp:TableCell>




</asp:TableRow>
</asp:Table>
 <%--END OF COST--%>






<%--END OF TYPE OF CHANGE--%>






</asp:TableCell>
</asp:TableRow>

<asp:TableRow>
<asp:TableCell>

<asp:Table ID="_tblCommentHeader" runat="Server" CssClass="Border"
BorderStyle="Solid" BorderColor="green" BorderWidth="0">

<asp:TableHeaderRow SkinID="rowheader" >
<asp:TableCell HorizontalAlign="left">
<asp:Label ID="_lblComment" runat="server" EnableViewState="false"  Text="Comments" />
</asp:TableCell>
</asp:TableHeaderRow>
</asp:Table>




<asp:Panel ID="_pnlComments" runat="server" Width="100%" HorizontalAlign="Left">

<asp:Table ID="_tblComments" runat="Server" BorderColor="purple" BorderWidth="0" CellPadding="6" CellSpacing="4">
<asp:TableRow>

<asp:TableCell HorizontalAlign="left" >

<asp:GridView CssClass="Border"  
ID="_gvComments" runat="server" AutoGenerateColumns="False"
ShowFooter="False" DataKeyNames="USERNAME">

<Columns>                               

<asp:TemplateField ItemStyle-Width="99%" HeaderText="<%$RIResources:Shared,Comments %>"
HeaderStyle-Font-Underline="true">
<ItemTemplate>
<IP:AdvancedTextBox ID="_txtComments2" runat="server" expandheight="true" text='<%# Bind("comments") %>'
readonly="True" enabled="True" width="95%" style="font-size: 12px; color: Black; font-family: Verdana;" textmode="MultiLine" maxlength="2000" />
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField   HeaderText="<%$RIResources:MOC,Date %>"
HeaderStyle-Wrap="false" HeaderStyle-Font-Underline="true">
<ItemTemplate>
<div style="text-align:center;">
<asp:Label ID="_lbCommentDate" runat="server" width="200px"   Text='<%# Bind("lastupdatedate") %>' EnableViewState="false"></asp:Label>
</div>
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField  HeaderText="<%$RIResources:MOC,Created By %>"
HeaderStyle-Wrap="false" HeaderStyle-Font-Underline="true">
<ItemTemplate>
<div style="text-align:center;">
<asp:Label ID="_lbCommentUsername" runat="server" width="200px" Text='<%# Bind("username") %>' EnableViewState="false"></asp:Label>
</div>
</ItemTemplate>
</asp:TemplateField>

</Columns>
</asp:GridView>


</asp:TableCell>
</asp:TableRow>
<asp:TableRow CssClass="Border"  >

<%--<asp:TableCell HorizontalAlign="left" ColumnSpan="3" >
<asp:Label ID="_lblNewCommentJEB" style="text-align:left;vertical-align:middle" runat="server" Text="<%$RIResources:Shared,Comments%>"
EnableViewState="false" /> &nbsp; &nbsp;
<IP:AdvancedTextBox id="_txtCommentsNewJEB" runat="server" expandheight="true"
textmode="MultiLine" maxlength="2000" style="width: 90%; font-size: 12px; color: Black; font-family: Verdana;" />

</asp:TableCell>--%>
     
</asp:TableRow>
</asp:Table >

 <asp:Table runat="Server" BorderColor="purple" BorderWidth="0" CellPadding="3" CellSpacing="2">
<asp:TableRow >
<asp:TableCell > 
<%--<asp:Label ID="_lblNewComment" style="width: 5px; text-align:left;vertical-align:middle" runat="server" Text="<%$RIResources:Shared,Comments%>"--%>
<asp:Label ID="_lblNewComment" style="width: 5px; text-align:left;vertical-align:middle" runat="server" Text="Add Comments"
EnableViewState="false" />
</asp:TableCell>
<asp:TableCell>
 <IP:AdvancedTextBox id="_txtCommentsNew" runat="server" expandheight="true"
textmode="MultiLine" maxlength="2000" style="width: 1750px; font-size: 12px; color: Black; font-family: Verdana;" />

</asp:TableCell>

</asp:TableRow>

 </asp:Table>

</asp:Panel>
</asp:TableCell>
</asp:TableRow>
<asp:TableRow>
<asp:TableCell>



<asp:Table ID="_tblImpactHeader" runat="Server" CssClass="Border"
BorderStyle="Solid" BorderColor="green" BorderWidth="0">

<asp:TableHeaderRow SkinID="rowheader" >
<asp:TableCell HorizontalAlign="left">
<asp:Label ID="_lblImpact" runat="server" EnableViewState="false" Text="Potential Impact" />
</asp:TableCell>
</asp:TableHeaderRow>
</asp:Table>


<asp:Table runat="server" CellPadding="3" CellSpacing="2" 
Width="100%" BorderColor="red" BorderWidth="0">
<asp:TableRow BackColor="white">
<asp:TableCell Width="99%" HorizontalAlign="left" VerticalAlign="Bottom" >

<IP:AdvancedTextBox id="_txtImpact" runat="server" expandheight="true" width="98%"
rows="2" textmode="MultiLine" maxlength="1000" style="width: 98%; font-size: 12px; color: Black; font-family: Verdana;" />
</asp:TableCell>
</asp:TableRow>
</asp:Table>




<asp:Table ID="_tblClassification" runat="server" CellPadding="0" CellSpacing="0"
BackColor="white" Style="width: 100%" EnableViewState="true" BorderColor="#3333ff" 
    BorderWidth="0">
<asp:TableRow CssClass="Border">
<asp:TableCell ColumnSpan="3" Width="100%" Wrap="false">

<IP:MOCClass id="_MOCClass" runat="server" displaymode="Enter" />
</asp:TableCell>
</asp:TableRow>
</asp:Table>

</asp:TableCell>
</asp:TableRow>
</asp:Table>


<asp:Panel ID="_pnlClassQuestions" runat="server" Width="100%" HorizontalAlign="Left">

<asp:Table ID="_tblClassQuestions" runat="Server" BackColor="white" BorderWidth="0" BorderColor="#66ff66">
<asp:TableRow ID="_trClassQuestions">
<asp:TableCell>
<RWG:BulkEditGridView ID="_gvClassQuestions" runat="server" Width="100%" CssClass="Border" BorderColor="Black" BorderWidth="2px" AutoGenerateColumns="False"
ShowFooter="False" DataKeyNames="mocquestion_seqid" ShowHeader="false" BackColor="Silver">
<Columns>
<asp:TemplateField ShowHeader="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="50%" HeaderText="Question" HeaderStyle-Wrap="false" HeaderStyle-Font-Underline="true">
<ItemTemplate>
<asp:Label ID="_lblTitle" runat="server" Text='<%# Bind("title") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField ItemStyle-Width="30%">
<EditItemTemplate>
<asp:CheckBoxList ID="_cbAnswer" runat="server" RepeatDirection="Horizontal" onclick="CheckBoxToRadio(this,2)" OnSelectedIndexChanged="_gvClassQuestions.HandleRowChanged" Visible='<%# Container.DataItem("yesnoquestion")%>'>
<asp:ListItem Text="<%$RIResources:Shared,Yes %>" Value="Y"></asp:ListItem>
<asp:ListItem Text="<%$RIResources:Shared,No %>" Value="N"></asp:ListItem>
</asp:CheckBoxList>
<IP:AdvancedTextBox id="_tbAnswer" runat="server" expandheight="true" text='<%# Bind("answer")%>' rows="1"
width="95%" textmode="MultiLine" maxlength="2000" style="font-size: 12px; color: Black; font-family: Verdana;" Visible='<%# Container.DataItem("textquestion")%>' ontextChanged="_gvClassQuestions.HandleRowChanged" />
</EditItemTemplate>
</asp:TemplateField>
<%--                                <asp:TemplateField ItemStyle-Width="10%" HeaderText="<%$RIResources:MOC,Answered Date %>"
HeaderStyle-Font-Underline="true">
<ItemTemplate>
<asp:Label ID="_lblClassAnswerDate" runat="server" Text='<%#RI.SharedFunctions.CleanDate(Eval("updatedate"),dateformat.ShortDate) %>' EnableViewState="false"></asp:Label>
</ItemTemplate>
</asp:TemplateField>--%>
</Columns>
</RWG:BulkEditGridView>
</asp:TableCell></asp:TableRow></asp:Table></asp:Panel>
    <asp:Table runat="server" BorderWidth="0" BorderColor="#ff9900">
<asp:TableRow ID="_trCat" runat="server" CssClass="Border">
<asp:TableCell ColumnSpan="3" Width="100%" Wrap="false" BorderColor="black" BorderWidth="0">

<IP:MOCCategory id="_MOCCat" runat="server" displaymode="Search" />
<asp:CustomValidator ID="_catVal" runat="server"
ClientValidationFunction="ValidateMarketChannel" ValidationGroup="EnterMOC"
ErrorMessage="" Text="<%$RIResources:Shared,ValidateRequiredFields%>" Display="Dynamic"></asp:CustomValidator>

</asp:TableCell></asp:TableRow><asp:TableRow CssClass="Header">
<asp:TableCell HorizontalAlign="left">
<a id="CatQuestions" href="#CatQuestions"></a>
<asp:Label ID="_lblCatQuestions" runat="server" Text="<%$RIResources:MOC,Category Questions%>" EnableViewState="false" SkinID="LabelWhite" />
</asp:TableCell></asp:TableRow></asp:Table><ajaxToolkit:CollapsiblePanelExtender ID="_cpeCatQuestions" runat="Server" TargetControlID="_pnlCatQuestions"
Collapsed="False" CollapseControlID="_lblCatQuestions" ExpandControlID="_lblCatQuestions"
SuppressPostBack="True" TextLabelID="_lblCatQuestions" CollapsedText="<%$RIResources:Shared,Show Category Questions %>"
ExpandedText="<%$RIResources:Shared,Hide Category Questions %>" ScrollContents="false" />





<asp:Panel ID="_pnlCatQuestions" runat="server" Width="100%" HorizontalAlign="Left">

<asp:Table ID="_tblCatQuestions" runat="Server" BackColor="white">
<asp:TableRow ID="_trCatQuestions">
<asp:TableCell>
<RWG:BulkEditGridView ID="_gvCatQuestions" runat="server" Width="100%" CssClass="Border" BorderColor="Black" BorderWidth="2px" AutoGenerateColumns="False"
ShowFooter="False" DataKeyNames="mocquestion_seqid" ShowHeader="false" BackColor="Silver">
<Columns>
<asp:TemplateField ShowHeader="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="50%" HeaderText="Question" HeaderStyle-Wrap="false" HeaderStyle-Font-Underline="true">
<ItemTemplate>
<asp:Label ID="_lblTitle" runat="server" Text='<%# Bind("title") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField ItemStyle-Width="30%">
<EditItemTemplate>
<asp:CheckBoxList ID="_cbAnswer" runat="server" RepeatDirection="Horizontal" onclick="CheckBoxToRadio(this,2)" OnSelectedIndexChanged="_gvCatQuestions.HandleRowChanged" Visible='<%# Container.DataItem("yesnoquestion")%>'>
<asp:ListItem Text="<%$RIResources:Shared,Yes %>" Value="Y"></asp:ListItem>
<asp:ListItem Text="<%$RIResources:Shared,No %>" Value="N"></asp:ListItem>
</asp:CheckBoxList>
<IP:AdvancedTextBox id="_tbAnswer" runat="server" expandheight="true" text='<%# Bind("answer")%>' rows="1"
width="95%" textmode="MultiLine" maxlength="2000" style="font-size: 12px; color: Black; font-family: Verdana;" Visible='<%# Container.DataItem("textquestion")%>' ontextChanged="_gvCatQuestions.HandleRowChanged" />
</EditItemTemplate>
</asp:TemplateField>
</Columns>
</RWG:BulkEditGridView>
</asp:TableCell></asp:TableRow></asp:Table></asp:Panel><%--START OF APPROVERS--%><%--Start of Superintendent  --%><asp:Panel ID="_pnlApprovals" runat="server" Visible="false">




<asp:Table ID="TableSuperintendentApprover" runat="Server" 
BorderStyle="Solid" BorderColor="Purple" BorderWidth="0" Width="100%">


<asp:TableHeaderRow  SkinID="rowheader"   >
<asp:TableCell HorizontalAlign="left" ColumnSpan="3" >
    <asp:Label ID="LabelSuperintendentApprover1" runat="server" 
EnableViewState="false"  Text="Superintendent Approver"  />
</asp:TableCell></asp:TableHeaderRow><asp:TableRow>
<asp:TableCell Width="25%"><strong>Name</strong></asp:TableCell><asp:TableCell Width="15%"><strong>Approval Date</strong></asp:TableCell><asp:TableCell Width="60%"><strong>Comments</strong></asp:TableCell></asp:TableRow><asp:TableRow>
<asp:TableCell ID="SuperintendentName" runat="server" Width="25%"></asp:TableCell><asp:TableCell ID="SuperintendentApprovaldate" runat="server" Width="15%"></asp:TableCell><asp:TableCell id="SuperintendentComments" runat="server" Width="60%"></asp:TableCell></asp:TableRow></asp:Table><hr>	
    
    <%--        <asp:GridView ID="_gvSuperintendentApprover" 
			
    runat="server" 
				AutoGenerateColumns="False" Width="100%" 

				align="Left"
				Visible="true" EmptyDataText="" >
				<headerstyle HorizontalAlign="Left"></headerstyle>
				<rowstyle backcolor="White"   forecolor="Black"/>
				<alternatingrowstyle backcolor="WhiteSmoke"  forecolor="Black"/>        
				<Columns>
				<asp:BoundField DataField="fullname" HeaderText="Name" ItemStyle-Width="15%"  />
				<asp:BoundField DataField="approvaldate" HeaderText="Approval Date" ItemStyle-Width="15%"/>
				<asp:BoundField DataField="comments" HeaderText="Comments" />
				</Columns> 
				</asp:GridView>--%><span>&nbsp;</span> <div style="width: 100%; text-align: left; color: White;" class="Header">
 <asp:Button ID="_btnShowAddApprover" BackColor="green" ForeColor="White" runat="server" Text="Add Approvers/Informed" OnClientClick="$find('bePopup').show();return false;" />
&nbsp;&nbsp;&nbsp;&nbsp; <asp:Label ID="noReviewersAssigned" Visible="false" runat="server" Text="No Reviewers Assigned.  Assigned Reviewers." style="text-align: left; color: Red; font-size: 15px"></asp:Label><br /><a id="Approval" href="#Approval"></a></div><asp:Panel ID="PanelCurrentApprover" runat="server" Visible="true">  
        <asp:Table ID="currentReviewrGoesHere" runat="server" 
 BorderColor="Red" BorderStyle="Dotted" BorderWidth="0">

<asp:TableHeaderRow  SkinID="rowheader"  >
<asp:TableCell HorizontalAlign="left"  >
<asp:Label ID="_lblL1Approvers" runat="server" EnableViewState="false"  Text="Current Approver" />
</asp:TableCell></asp:TableHeaderRow><asp:TableRow ID="_trL1Grid">
        <asp:TableCell>
        <asp:GridView ID="_gvApprovals" runat="server" Width="100%" 
            CssClass="Border" BorderColor="Black" 
            BorderWidth="2px" AutoGenerateColumns="False"
        ShowFooter="False" DataKeyNames="approval_seqid" 
            EmptyDataText="" EmptyDataRowStyle-BackColor="silver">
        <Columns>
        <asp:TemplateField ItemStyle-Width="8%" HeaderText="Role" HeaderStyle-Wrap="false" HeaderStyle-Font-Underline="true" Visible="false">
        <ItemTemplate>
        <asp:Label ID="_lblRole" runat="server" Text='<%# Bind("roledescription") %>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>
   
        <asp:TemplateField ItemStyle-Width="15%" HeaderText="Name"
        HeaderStyle-Wrap="false" HeaderStyle-Font-Underline="true">
        <ItemTemplate>
        <asp:Label ID="_lblApproverL1Name" runat="server" Text='<%# Bind("personname") %>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>
        
         <asp:TemplateField Visible="false">
        <ItemTemplate>
        <asp:Label ID="_lblRoleUserNames" runat="server" Text='<%# Bind("roleusernames") %>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="<%$RIResources:Global,Required %>"
        HeaderStyle-Font-Underline="true">
        <ItemTemplate>
        <center>
        <asp:Label ID="_lblRequired" runat="server" Text='<%# Bind("required_flag") %>' EnableViewState="false"></asp:Label>
        </center>
        </ItemTemplate>
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="<%$RIResources:MOC,Approved %>" ItemStyle-Width="10%"
        HeaderStyle-Font-Underline="true">
        <ItemTemplate>
        <asp:CheckBoxList ID="_cbApproval" runat="server" RepeatDirection="Horizontal" onclick="CheckBoxToRadio(this,2)">
        <asp:ListItem Text="<%$RIResources:Shared,Yes %>" Value="Y"></asp:ListItem>
        <asp:ListItem Text="<%$RIResources:Shared,No %>" Value="N"></asp:ListItem>
        </asp:CheckBoxList>
        </ItemTemplate>
        </asp:TemplateField>
        
        <asp:TemplateField ItemStyle-Width="10%" HeaderText="<%$RIResources:MOC,ApprovalDate %>"
        HeaderStyle-Wrap="false" HeaderStyle-Font-Underline="true">
        <ItemTemplate>
        <center>
        <asp:Label ID="_lblApproverL1Date" runat="server" Text='<%#RI.SharedFunctions.CleanDate(Eval("approvedate"), DateFormat.ShortDate) %>' EnableViewState="false"></asp:Label>
        </center>
        </ItemTemplate>
        </asp:TemplateField>
        
            <asp:TemplateField ItemStyle-Width="55%" HeaderText="<%$RIResources:Shared,Comments %>"
        HeaderStyle-Font-Underline="true">
        <ItemTemplate>
        <center>
        <IP:AdvancedTextBox id="_txtComments" runat="server" expandheight="true" text='<%# Bind("comments") %>'
        width="95%" textmode="MultiLine" maxlength="2000" style="font-size: 12px; color: Black; font-family: Verdana;" />
        </center>
        <asp:Label Font-Italic="true" ID="_lblResponse" runat="server" Text='<%# Bind("roleresponse") %>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField>
        <ItemTemplate>
        <asp:Button ID="_btnApproverDelete" CommandName="Delete" OnClick="FieldChanged" runat="server"
        Text="<%$RIResources:Global,Remove %>" />
        <ajaxToolkit:ConfirmButtonExtender ID="_cbeDelete" runat="server" ConfirmText="<%$RIResources:Shared,ConfirmDelete %>"
        TargetControlID="_btnApproverDelete"></ajaxToolkit:ConfirmButtonExtender>
        </ItemTemplate>
        </asp:TemplateField>
        
        </Columns>
        </asp:GridView>
        </asp:TableCell></asp:TableRow></asp:Table><br /></asp:Panel><%--Start of Approvers--%><asp:Table ID="TableApprovers" runat="Server" 
BorderStyle="Solid" BorderColor="Green" BorderWidth="0">

<asp:TableHeaderRow SkinID="rowheader" >
<asp:TableCell HorizontalAlign="left">
<asp:Label ID="LabelApprovers" runat="server" EnableViewState="false"  Text="Approvers" />
</asp:TableCell></asp:TableHeaderRow></asp:Table><telerik:RadGrid RenderMode="Lightweight" ID="RadGridAssignedApprover" runat="server"
            OnItemDataBound="RadGridAssignedApprover_ItemDataBound"
            OnNeedDataSource="RadGridAssignedApprover_NeedDataSource"
            OnDeleteCommand="RadGridAssignedApprover_DeleteCommand"
            DataKeyNames="approval_seqid" 
            Width="99%"
            Visible="true"
            AllowPaging="true"
            ShowGroupPanel="false"
            AllowSorting="True"
            AllowFilteringByColumn="True"
            ExportSettings-FileName="Report"
            ExportSettings-ExportOnlyData="true"
            ExportSettings-IgnorePaging="true"
            ExportSettings-OpenInNewWindow="true"
            ExportSettings-UseItemStyles="true"
            PageSize="25"
            AutoGenerateColumns="false"
            HeaderStyle-HorizontalAlign="Left"
            HeaderStyle-Font-Underline="true"
            HeaderStyle-Wrap="false"
            HeaderStyle-Font-Bold="true"
            HeaderStyle-Font-Size="13px"
            HeaderStyle-Font-Names="Arial"
            ShowFooter="True"
            FooterStyle-HorizontalAlign="Right"
            FooterStyle-Font-Bold="true"
            FooterStyle-Font-Size="Medium"
            ItemStyle-Font-Names="Arial"
            ItemStyle-Font-Size="Larger"
            ItemStyle-Font-Bold="true"
            ItemStyle-BackColor="White"
            AlternatingItemStyle-Font-Names="Arial"
            AlternatingItemStyle-Font-Size="Larger"
            AlternatingItemStyle-Font-Bold="true"
            AlternatingItemStyle-BackColor="WhiteSmoke"
            Skin="Office2010Silver">

<ExportSettings>
<Excel WorksheetName="RI" />
<Excel DefaultCellAlignment="Right" />
<Pdf PageWidth="297mm" PageHeight="210mm" />
</ExportSettings>
<MasterTableView CommandItemDisplay="Top" TableLayout="Fixed" DataKeyNames="approval_seqid">
<PagerStyle AlwaysVisible="true" />
<CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="false" />
<Columns>
<telerik:GridBoundColumn AllowSorting="false" AllowFiltering="false" DataField="approval_type" HeaderText="Approver Type" HeaderStyle-Width="10px"></telerik:GridBoundColumn>
<telerik:GridBoundColumn AllowSorting="false" AllowFiltering="false" DataField="personname" HeaderText="Name" HeaderStyle-Width="21px"></telerik:GridBoundColumn>

<telerik:GridBoundColumn AllowSorting="false" AllowFiltering="false" DataField="required_flag" HeaderText="Required" HeaderStyle-Width="5px" DataType="System.String"></telerik:GridBoundColumn>
<telerik:GridBoundColumn AllowSorting="false" AllowFiltering="false" DataField="approved" HeaderText="Approved" HeaderStyle-Wrap="true" HeaderStyle-Width="5px" DataType="System.String"></telerik:GridBoundColumn>

<telerik:GridBoundColumn AllowSorting="false" AllowFiltering="false" DataField="approvedate" ItemStyle-HorizontalAlign="Left" HeaderText="Date" HeaderStyle-Width="6px" DataFormatString="{0:M/d/yyyy}"></telerik:GridBoundColumn>
<telerik:GridBoundColumn AllowSorting="false" AllowFiltering="false" DataField="comments" HeaderText="Comments" HeaderStyle-Width="47px" DataType="System.String"></telerik:GridBoundColumn>
<telerik:GridBoundColumn AllowSorting="false" AllowFiltering="false" DataField="roleresponse" HeaderText="Response by" DataType="System.String" HeaderStyle-Width="10px"></telerik:GridBoundColumn>

<telerik:GridBoundColumn AllowSorting="false" AllowFiltering="false" DataField="approval_seqid" Visible="false" HeaderText="approval_seqid" HeaderStyle-Width="12px"></telerik:GridBoundColumn>


 <telerik:GridButtonColumn CommandName="Delete" Text="Delete" UniqueName="DeleteColumn" HeaderText="Delete" HeaderStyle-Width="5px">
                    </telerik:GridButtonColumn>


</Columns>
</MasterTableView>
<ClientSettings ReorderColumnsOnClient="True" AllowDragToGroup="False" AllowColumnsReorder="True">
<Scrolling AllowScroll="true" UseStaticHeaders="true" ScrollHeight="" />
<Selecting AllowRowSelect="True"></Selecting>
<Resizing
AllowRowResize="False"
AllowColumnResize="False"
EnableRealTimeResize="False"
ResizeGridOnColumnResize="False"></Resizing>


</ClientSettings>
<GroupingSettings ShowUnGroupButton="true"></GroupingSettings>

<FilterMenu RenderMode="Lightweight"></FilterMenu>

<HeaderContextMenu RenderMode="Lightweight"></HeaderContextMenu>

<PagerStyle Mode="NextPrevAndNumeric" PageSizeControlType="None"></PagerStyle>
</telerik:RadGrid>


<br />


    </asp:Panel>
    
    
    <%--End of telerik Grid--%><%-- JEB TEST--%><%-- Company Table --%><%--<asp:UpdatePanel ID="UpdatePanelcompany" UpdateMode="Always" Visible="true" runat="server">
<ContentTemplate>

        <ajaxToolkit:AutoCompleteExtender
        ID="AutoCompleteExtender2"
        TargetControlID="TextBoxcompanyname"
        FirstRowSelected="true"
        runat="server"
        ServiceMethod="lookupCompanyName"
        ServicePath="~/lookupcompany.asmx"
        MinimumPrefixLength="1">
        </ajaxToolkit:AutoCompleteExtender>


<asp:Table CellPadding="0" CellSpacing="0" ID="TableCompanyInfo"
CssClass="PR_SearchTable" runat="server" BorderWidth="1px" Width="660px">
<asp:TableRow ID="TableRowCompanyHeading" runat="server"
CssClass="InputlabelHeadingText">
<asp:TableCell ID="TableCell56" runat="server" ColumnSpan="8" CssClass="InputlabelHeadingText">&nbsp;Company

</asp:TableCell></asp:TableRow>



<asp:TableRow ID="TableRowcompanyname" runat="server" CssClass="tablerowheight">
<asp:TableCell ID="TableCell13" runat="server" CssClass="InputlabelText">&nbsp;Company: </asp:TableCell>

<asp:TableCell ColumnSpan="7" ID="TableCell14" runat="server" CssClass="mytextalignleft"> 
<asp:TextBox ID="TextBoxcompanyname" Width="280" CssClass="shadeform" runat="server">

</asp:TextBox>

</asp:TableCell>

</asp:TableRow>

</asp:Table>


</ContentTemplate>

</asp:UpdatePanel>--%><%-- END Company Table --%><br /><%-- Action Items Heading--%><asp:Table ID="_tblActionItemsHeader" runat="Server" 
BorderStyle="Solid" BorderColor="blue" BorderWidth="0" >
<asp:TableHeaderRow SkinID="rowheader" >
    
<asp:TableCell HorizontalAlign="left">
<asp:Label ID="_lblActionItems" runat="server" EnableViewState="false" Text="Action Items" />
&nbsp;<asp:HyperLink ID="_hypSystemDefinition" CssClass="LabelLink" runat="server"
ImageUrl="../Images/question.gif"></asp:HyperLink>
</asp:TableCell></asp:TableHeaderRow></asp:Table><%-- Action Items Heading END--%><asp:Panel ID="_pnlChecklist" runat="server" Width="100%" HorizontalAlign="Left" Visible="true"> 

<%-- START ACTION ITEMS--%>
<asp:DataList ID="_dlSystem" runat="server" Width="100%" Visible="true" 
RepeatLayout="Table"
RepeatColumns="3" 
RepeatDirection="Horizontal" 
ItemStyle-Font-Strikeout="false"
GridLines="Both" 
ItemStyle-Width="30%" 
ItemStyle-VerticalAlign="Top">
<ItemTemplate>
<asp:Label ID="_lblSystem" runat="server" Visible="true" 
Text='<%# RI.SharedFunctions.LocalizeValue(DataBinder.Eval(Container.DataItem, "MOCSystem")) %>'>

</asp:Label><asp:Label ID="_lblSystemSeq" Visible="false" runat="server" Text='<%# Bind("mocsystem_seq_id") %>'>

 </asp:Label><asp:HiddenField ID="_hdftaskitem" Visible="false" runat="server"></asp:HiddenField>

<asp:CheckBoxList ID="_cblSystem" runat="server" 
RepeatDirection="Horizontal" onclick="CheckBoxToRadio(this,2)"
OnSelectedIndexChanged="SystemChanged">
<asp:ListItem Text="<%$RIResources:Shared,Yes %>" Value="Y">
</asp:ListItem><asp:ListItem Text="<%$RIResources:Shared,No %>" Value="N" Selected="True">
</asp:ListItem></asp:CheckBoxList><asp:Table ID="_tblSystemDetail" runat="server">
<asp:TableRow>
<asp:TableCell Width="60%">
<asp:DropDownList ID="_ddlSystemFacility" runat="server" Width="100%"></asp:DropDownList>
</asp:TableCell><asp:TableCell>
<asp:DropDownList ID="_ddlSystemPerson" runat="server"></asp:DropDownList>
<asp:CustomValidator ID="CustomValidator1" runat="server" ValidationGroup="EnterMOC"
ClientValidationFunction="ValidatePerson" ErrorMessage="" Text="<%$RIResources:Shared,ValidateRequiredFields%>" Display="Dynamic">
</asp:CustomValidator>
</asp:TableCell></asp:TableRow><asp:TableRow>
<asp:TableCell>
<asp:Label ID="_lblPriority" runat="server" Text="<%$RIResources:Shared,Priority %>" EnableViewState="false"></asp:Label>&nbsp;&nbsp;
<asp:DropDownList ID="_ddlPriority" runat="server">
<asp:ListItem Value="1" Text="<%$RIResources:Shared,Low %>"></asp:ListItem>
<asp:ListItem Value="2" Text="<%$RIResources:Shared,Medium %>"></asp:ListItem>
<asp:ListItem Value="3" Text="<%$RIResources:Shared,High %>"></asp:ListItem>
</asp:DropDownList>
</asp:TableCell><asp:TableCell>
<asp:Label ID="_lblDaysAfter" runat="server" Text="<%$RIResources:Shared,Days after Approval %>" EnableViewState="false"></asp:Label>&nbsp;
<asp:TextBox ID="_txtDaysAfter" runat="server" Width="20"></asp:TextBox>&nbsp;
<asp:Label ID="_lblStatus" runat="server" Text="" EnableViewState="false"></asp:Label>&nbsp;

<ajaxToolkit:FilteredTextBoxExtender ID="_feDaysAfter" runat="server" TargetControlID="_txtDaysAfter"
FilterType="custom" ValidChars="1234567890/" Enabled="true">
</ajaxToolkit:FilteredTextBoxExtender>
<br />
<asp:CustomValidator ID="_systemVal" runat="server"
ClientValidationFunction="ValidateDaysAfter" ValidationGroup="EnterMOC"
ErrorMessage="" Text="<%$RIResources:Shared,ValidateRequiredFields%>" Display="Dynamic"></asp:CustomValidator>
</asp:TableCell></asp:TableRow><asp:TableRow>
<asp:TableCell ColumnSpan="2">
<asp:Label ID="_lbSysTitle" runat="server" Text="<%$RIResources:Shared,Title %>" EnableViewState="false"></asp:Label>&nbsp;
<IP:AdvancedTextBox id="_tbSysTitle" runat="server" expandheight="true" rows="1"
width="75%" textmode="MultiLine" maxlength="90" style="font-size: 12px; color: Black; font-family: Verdana;" />
</div>
</asp:TableCell></asp:TableRow></asp:Table></ItemTemplate></asp:DataList><%-- END ACTION ITEMS--%></asp:Panel><br /><%-- Pending Template Tasks Heading--%><asp:Table ID="_tblTempTaskTitle" runat="server" CellPadding="2" CellSpacing="0"
BackColor="white" Style="width: 100%" EnableViewState="true" Visible="false">
<asp:TableHeaderRow SkinID="rowheader" >
<asp:TableCell HorizontalAlign="left">
<asp:Label ID="_lbPendingTasks" runat="server" Text="<%$RIResources:MOC,Pending Template Tasks %>"  EnableViewState="false"></asp:Label>
&nbsp;<asp:HyperLink ID="_hypMOCTemplateTasks" CssClass="LabelLink" runat="server" ImageUrl="../Images/question.gif"></asp:HyperLink><br />
</asp:TableCell></asp:TableHeaderRow></asp:Table><%-- Pending Template Tasks END--%><asp:Table ID="_tblTempTasks" runat="Server" Style="width: 100%" Visible="false">
<asp:TableRow>
<asp:TableCell HorizontalAlign="left">
<asp:GridView CssClass="Border" BorderColor="Black" BorderWidth="2px" Width="100%"
ShowFooter="False" ID="_gvPendingTemplateTasks" runat="server" AutoGenerateColumns="False"
DataKeyNames="taskitemseqid" EmptyDataText="<%$RIResources:MOC,No Pending Template Tasks%>" EmptyDataRowStyle-BackColor="silver">
<Columns>
<asp:TemplateField ItemStyle-Width="60%" HeaderText="<%$RIResources:Shared,Title/Description %>"
HeaderStyle-Font-Underline="true">
<ItemTemplate>
<asp:Label ID="_lbTitle" runat="server" Text='<%# Bind("title") %>' EnableViewState="false"></asp:Label></div>
<IP:AdvancedTextBox ID="_tbDescription" runat="server" expandheight="true" text='<%# Bind("description") %>'
readonly="True" enabled="True" width="95%" style="font-size: 12px; color: Black; font-family: Verdana;" textmode="MultiLine" maxlength="4000" />
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField ItemStyle-Width="10%" HeaderText="<%$RIResources:Shared,Responsible %>"
HeaderStyle-Wrap="false" HeaderStyle-Font-Underline="true" HeaderStyle-HorizontalAlign="Center">
<ItemTemplate>
<div style="text-align:center;">
<asp:Label ID="_lbResponsible" runat="server" Text='<%# Bind("responsible") %>' EnableViewState="false"></asp:Label></div>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField ItemStyle-Width="10%" HeaderText="<%$RIResources:Shared, Days After Approval %>"
HeaderStyle-Wrap="false" HeaderStyle-Font-Underline="true" HeaderStyle-HorizontalAlign="Center">
<ItemTemplate>
<div style="text-align:center;">
<asp:Label ID="_lbDaysAfter" runat="server" Text='<%# Bind("daysafter") %>' EnableViewState="false"></asp:Label></div>
</ItemTemplate>
</asp:TemplateField>
</Columns>
</asp:GridView>
</asp:TableCell></asp:TableRow></asp:Table><br /><asp:Table ID="_tblUpdate" runat="server" CellPadding="2" CellSpacing="2" BackColor="white" Style="width: 99%" Visible="false" BorderWidth="0" BorderColor="#0000cc">
<asp:TableRow CssClass="Border">
<asp:TableCell Width="25%">
<asp:Label ID="_lblCreatedBy" runat="server" EnableViewState="false"></asp:Label>
</asp:TableCell><asp:TableCell Width="25%">
<asp:Label ID="_lblCreatedDate" runat="server" EnableViewState="false"></asp:Label>
</asp:TableCell><asp:TableCell Width="25%">
<asp:Label ID="_lblUpdatedBy" runat="server" EnableViewState="false"></asp:Label>
</asp:TableCell><asp:TableCell Width="25%">
<asp:Label ID="_lblLastUpdateDate" runat="server" EnableViewState="false"></asp:Label>
</asp:TableCell></asp:TableRow></asp:Table><div style="text-align:center;">
<br />



<asp:Button ID="_btnSubmit" clientidmode="static" Text="<%$RIResources:MOC,Submit %>" runat="server" ValidationGroup="EnterMOC" Font-Size="Large" OnClientClick="if(Page_ClientValidate('EnterMOC')) return ShowModalPopup();"></asp:Button>
<asp:Button ID="_btnInitiate" Text="<%$RIResources:MOC,Submit MOC %>" runat="server" ValidationGroup="EnterMOC" Visible="false" Font-Size="Large" />
<IP:SpellCheck id="_btnSpell" runat="server" ControlIdsToCheck="_txtTitle,_txtDescription,_txtImpact" />
<asp:Button ID="_btnDelete" runat="server" Text="<%$RIResources:Shared,Delete %>" />
</div>
<ajaxToolkit:ConfirmButtonExtender ID="_cbeDeletePage" runat="server" ConfirmText="<%$RIResources:Shared,ConfirmDelete %>"
TargetControlID="_btnDelete"></ajaxToolkit:ConfirmButtonExtender>

<asp:ValidationSummary ID="_vsSummary" runat="server" CssClass="ValidationError"
DisplayMode="BulletList" ValidationGroup="EnterMOC" HeaderText="<%$RIResources:Shared,RequiredFields %>"
ShowSummary="false" EnableClientScript="true" ShowMessageBox="false" />
    <br />
<asp:Panel ID="_pnlUpdateButtons" runat="server" HorizontalAlign="center" Visible="false" BorderWidth="0">
<asp:Button ID="_btnAttachment" runat="server" Text='<%$RIResources:Shared,Attachments %>'
ValidationGroup="EnterMOC" />
<asp:Button ID="_btnMOCActionItems" runat="server" Text='<%$RIResources:Shared,Task Items %>'
ValidationGroup="EnterMOC" />
<asp:Button ID="_btnDetailReport" runat="server" Text="<%$RIResources:MOC,MOCSummary %>" />
</asp:Panel>
<br />

<ajaxToolkit:ModalPopupExtender ID="_mpeSwapList" runat="server" TargetControlID="_btnShowList"
PopupControlID="_pnlSwapListBox" CancelControlID="_btnDummy" BehaviorID="bePopup"
BackgroundCssClass="modalBackground" DropShadow="true">
</ajaxToolkit:ModalPopupExtender>

<div style="display: none">
<asp:Button ID="_btnShowList" runat="server" Text="Show List" />
<asp:Button ID="_btnDummy" runat="server" Text="Cancel" Style="display: none" />
</div>


<%--Add approvers--%>
<asp:Panel ID="_pnlSwapListBox" runat="server" CssClass="modalPopup" Width="900px" Style="overflow: auto;" Height="625px">
<asp:Label ID="_lblNewFacility" runat="server" Text="<%$RIResources:Shared,Facility %>" EnableViewState="false">


</asp:Label><br /><asp:DropDownList ID="_ddlApproverFacilityNew" runat="server" Width="220px" />
<IP:ucMOCSwapListBox ID="_slbApprovalNotificationList" runat="server" />

<div style="text-align:center;">
<asp:Button ID="_btnSaveDraft" runat="server" Text="<%$RIResources:Shared,Save As Draft %>" onClick="_btnOkSaveApprovers_Click" ></asp:Button>
<asp:Button ID="_btnOkSaveApprovers" runat="server" Text="Save Reviewers" Font-Size="Large"  ></asp:Button>
<%--<asp:Button ID="_btnOkSaveApprovers" runat="server" Text="<%$RIResources:Shared,Submit MOC %>" Font-Size="Large"  ></asp:Button>--%>

<asp:Button ID="_btnCancel" runat="server" Text="<%$RIResources:Shared,Cancel %>" OnClientClick="return HideApproverMP()" ></asp:Button>
</div>



</asp:Panel>


<ajaxToolkit:ModalPopupExtender ID="_mpeSelectTemplateTasks" runat="server"
TargetControlID="Button1" PopupControlID="_pnlTemplateTasks"
BackgroundCssClass="modalBackground"
DropShadow="true" />

<div style="display: none">
<asp:Button ID="Button1" runat="server" Text="Show List" />
<asp:Button ID="Button2" runat="server" Text="Cancel" Style="display: none" />
<asp:Button ID="Button3" runat="server" Text="Cancel1" Style="display: none" />
</div>



<asp:Panel ID="_pnlTemplateTasks" runat="server" CssClass="modalPopup" Width="900px">

<asp:Table ID="Table1" runat="Server" Width="90%">
<asp:TableRow HorizontalAlign="Center">
<asp:TableCell>
<asp:Button ID="_btnCreateTasks" runat="server" Text="<%$RIResources:Shared, Create Tasks %>" />&nbsp;&nbsp;
<asp:Button ID="_btnCloseTasks" runat="server" Text="<%$RIResources:Shared, Close %>" Visible="false" />
</asp:TableCell></asp:TableRow></asp:Table><br /><asp:Table ID="_tblTemplateTasksDaysAfter" runat="Server" Width="90%">
<asp:TableHeaderRow>
<asp:TableHeaderCell HorizontalAlign="center">
<asp:Literal ID="_ltDaysAfter" runat="server"
Text="<%$RIResources:Shared,MOC TEMPLATE TASK MESSAGE FOR TASKS WITH DAYS AFTER%>"></asp:Literal>
</asp:TableHeaderCell></asp:TableHeaderRow><asp:TableRow>
<asp:TableCell HorizontalAlign="left">

<asp:GridView CssClass="Border" ID="_gvTemplateTasksDaysAfter" runat="server" AutoGenerateColumns="False"
ShowFooter="False" DataKeyNames="taskitemseqid" Width="100%">
<HeaderStyle CssClass="LockHeader" Font-Size="8" />

<Columns>
<asp:TemplateField ItemStyle-Width="10%" HeaderText="<%$RIResources:Shared,Create %>"
ItemStyle-VerticalAlign="Top">
<ItemTemplate>
<asp:HiddenField ID="_hfTaskItemSeqID" runat="server" Value='<%# Bind("TaskItemSeqid") %>' />
<asp:CheckBox ID="_cbCreate" runat="server" Checked="true" />
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField ItemStyle-Width="60%" HeaderText="<%$RIResources:Shared,Title/Description %>"
ItemStyle-VerticalAlign="Top">
<ItemTemplate>
<asp:Label ID="_lbTitle" runat="server" Text='<%# Bind("title") %>' EnableViewState="false"></asp:Label>
</div>
<IP:AdvancedTextBox ID="_tbDescription" runat="server" expandheight="true" text='<%# Bind("description") %>'
enabled="True" width="95%" style="font-size: 12px; color: Black; font-family: Verdana;" textmode="MultiLine" maxlength="4000" />
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField ItemStyle-Width="10%" HeaderText="<%$RIResources:Shared,Responsible %>"
HeaderStyle-Wrap="false" ItemStyle-VerticalAlign="Top">
<ItemTemplate>
<div style="text-align:center;">
<asp:Label ID="_lbResponsible" runat="server" Text='<%# Bind("responsibleusername") %>' Visible="false"></asp:Label>

</div>
<IP:Responsible runat="server" ID="_ucNewResponsible" ResponsibleValue='<%# Bind("responsibleusername") %>' FacilityValue='<%# Bind("resproleplantcode") %>'></IP:Responsible>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField ItemStyle-Width="5%" HeaderStyle-Wrap="true" ItemStyle-VerticalAlign="Top">
<ItemTemplate>
<div style="text-align:center;">
<IP:MOCDate id="_ucMOCDate" runat="server" AllowManualDate="True" DaysAfter='<%# Bind("responsibleusername") %>' >
</IP:MOCDate>                                
</div>
</ItemTemplate>

</asp:TemplateField>
</Columns>
</asp:GridView>
</asp:TableCell></asp:TableRow></asp:Table></asp:Panel><ajaxToolkit:ModalPopupExtender ID="_mpeTrialtoPerm" BehaviorID="BePopup2" runat="server"
BackgroundCssClass="modalBackground" PopupControlID="pnlPopup" TargetControlID="lnkDummy">
</ajaxToolkit:ModalPopupExtender>

<div style="display: none">
<asp:Button ID="lnkDummy" runat="server"></asp:Button>
<asp:Button ID="Button4" runat="server"></asp:Button>
</div>

<%--Popup for Reviewers - CssClass="modalPopup sets the color--%>

<asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Width="650px" Style="display: none; overflow: auto;" Height="200px">
<div style="text-align:center;">
<br />
<asp:Button ID="_btnSaveMOC" runat="server"  Style="display: none" Text="<%$RIResources:Shared,Move MOC to Permanent%>" />
<asp:Button ID="_btnCopyMOC" runat="server" Style="display: none" Text="<%$RIResources:Shared,Copy MOC to Permanent and Resubmit for Approval%>"  />
<asp:Button ID="_btnCancelPopup" runat="server" Text="Cancel" OnClientClick="return HideModalPopup()"/>
</div>

</asp:Panel>

<%--END Popup for Reviewers - CssClass sets the color--%>

<IP:MessageBox ID="_messageBox" runat="server" ButtonType="OKCancel" />

<div style="display: none;">
<asp:Button ID="btnDummy2" runat="server" Text="Edit" Style="display: none;" />
</div>
<ajaxToolkit:ModalPopupExtender ID="_mpeNotAuthorized" runat="server" TargetControlID="btnDummy2" PopupControlID="PnlModal" BackgroundCssClass="modalBackground">
</ajaxToolkit:ModalPopupExtender>
<asp:Panel ID="PnlModal" runat="server" Width="600px" Height="100px" CssClass="modalPopup">
<br />
<div style="text-align:center;">
<asp:label ID="lblAuth" runat="server" Text="You are not Authorized to view this MOC. Please contact your Facility Administrator. "></asp:label><br /><br /><asp:Button ID="_btnUnauthorized" runat="server" Text="OK" />

</div>
</asp:Panel>


<ajaxToolkit:ModalPopupExtender ID="_mpeMarketChannel" BehaviorID="MarketChannel" runat="server"
BackgroundCssClass="modalBackground" PopupControlID="pnlMCPopup" TargetControlID="_btnMCDummy">
</ajaxToolkit:ModalPopupExtender>

<div style="display: none">
<asp:Button ID="_btnMCDummy" runat="server"></asp:Button>
</div>

<asp:Panel ID="pnlMCPopup" runat="server" CssClass="modalPopup" Width="650px" Style="display: none; overflow: auto;" Height="150px">
<div style="text-align:center;">
<br />
<asp:Label id="_lbMarketChannel" runat="server" text ="You must select a Market Channel for Product Trial MOC's">
</asp:Label><br /><br /><asp:Button ID="_btnOk" runat="server" Text="OK" OnClientClick="return HideMCModalPopup()" />	
</div>

</asp:Panel>



</ContentTemplate>

</asp:UpdatePanel> <div id="_divSystemDefinition" class="modalPopup" style="display: none">
<span style="text-align: left" class="ContentHeader">
<asp:Literal ID="Literal1" runat="Server" Text="System"></asp:Literal></span><asp:Localize ID="_SystemDef" runat="server" Text="<%$ RIResources:Shared,SystemDefinition %>"></asp:Localize></div><div id="_divMOCDefinition" class="modalPopup" style="display: none">
<span style="text-align: left" class="ContentHeader">
<asp:Literal ID="Literal2" runat="Server" Text="System"></asp:Literal></span><asp:Localize ID="Localize1" runat="server" Text="<%$ RIResources:Shared,MOCTempTaskDefinition %>"></asp:Localize></div></asp:Content>