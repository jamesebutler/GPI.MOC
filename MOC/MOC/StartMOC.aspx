<%@ Page Title="" Language="VB" MasterPageFile="~/RI.master" 
AutoEventWireup="false" CodeFile="StartMOC.aspx.vb" 
Inherits="MOC_StartMOC" 
EnableViewState="true" 
UnobtrusiveValidationMode="None"%>


<%@ MasterType VirtualPath="~/RI.master" %>


<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="IP" Namespace="AdvancedTextBox" Assembly="AdvancedTextBox" %>


<%@ Register Src="~/User Controls/Common/ucMOCStatus.ascx" TagName="ucMOCStatus" TagPrefix="IP" %>
<%--<%@ Register Src="~/User Controls/Common/ucMOCSwapListBox.ascx" TagName="ucMOCSwapListBox" TagPrefix="IP" %>--%>
<%@ Register Src="~/User Controls/Common/UcMTTResponsible.ascx" TagName="Responsible" TagPrefix="IP" %>

<%--<%@ Register Src="~/User Controls/Common/ucMessageBox.ascx" TagName="MessageBox" TagPrefix="IP" %>--%>

<%@ Register Src="~/User Controls/Common/ucMOCTypes.ascx" TagName="MOCType" TagPrefix="IP" %>
<%@ Register Src="~/User Controls/Common/ucSpellcheck.ascx" TagName="SpellCheck" TagPrefix="IP" %>

<%--<%@ Register Src="~/User Controls/Common/ucMOCCategory.ascx" TagName="MOCCategory" TagPrefix="IP" %>--%>
<%--<%@ Register Src="~/User Controls/Common/ucMOCClassification.ascx" TagName="MOCClass" TagPrefix="IP" %>--%>

<%@ Register Src="~/User Controls/Common/UcMOCDate.ascx" TagName="MOCDate" TagPrefix="IP" %>
<%@ Register Src="~/User Controls/Common/UcJQDate.ascx" TagName="JQDate" TagPrefix="IP" %>




<asp:Content ID="Content1" ContentPlaceHolderID="_cphMain" runat="Server">


<script language="javascript" type="text/javascript">  
function validate()  
{  
      if (document.getElementById("<%=_tbCosts.ClientID%>").value=="")  
      {  
                 alert("Please enter superintendent comments");  
                 document.getElementById("<%=_tbCosts.ClientID%>").focus();  
                 return false;  
      }  


</script> 
		 
 <asp:UpdatePanel ID="_udpLocation" runat="server" UpdateMode="Conditional">
<ContentTemplate>


<telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" RenderMode="Lightweight">
</telerik:RadStyleSheetManager>



 <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1">
</telerik:RadAjaxLoadingPanel>



<asp:HiddenField ID="_hfMOCType" runat="server" />

<ajaxToolkit:CascadingDropDown ID="_cddlFacility" runat="server" Category="SiteID"
LoadingText=""  ServiceMethod="GetFacilityList" ServicePath="~/CascadingLists.asmx"
TargetControlID="_ddlFacility" UseContextKey="true"></ajaxToolkit:CascadingDropDown>


<ajaxToolkit:CascadingDropDown ID="_ccdlOwner" runat="server" Category="Employee"
LoadingText="" PromptText="    " ServiceMethod="GetEmployee" ServicePath="~/CascadingLists.asmx"
TargetControlID="_ddlOwner" ParentControlID="_ddlFacility"></ajaxToolkit:CascadingDropDown>





<asp:Table ID="table" runat="server" Width="100%" 
CellPadding="0" CellSpacing="0" BorderWidth="0" BorderColor="Red" BorderStyle="dotted" >
<asp:TableRow >
<asp:TableCell>





<%--START of MOC PROJECT MANAGER AND FACILITY--%>


<%--Manager--%>
<asp:Table ID="_tblMain" runat="server" CellPadding="2" CellSpacing="2"  
BorderWidth="0" BorderColor="Green" BorderStyle="Dotted"
Width="100%" >

<%-- Satus--%>
 <asp:TableHeaderRow SkinID="rowheader" >
<asp:TableCell ID="_tcStatus" columnspan="2"   Width="100%">
<asp:Label ID="_lblStatus"  runat="server" Text="Status" EnableViewState="false" />
</asp:TableCell>
</asp:TableHeaderRow>

<asp:TableHeaderRow>
<asp:TableCell columnspan="2" BackColor="white" Width="100%">
<asp:Label ID="_lblStatusLine" ForeColor="red" runat="server" Font-Bold="true" Font-Size="25px" EnableViewState="false" SkinID="LabelWhite" Text="" />
</asp:TableCell>
</asp:TableHeaderRow>


<%-- Type of Change--%>
 <asp:TableRow CssClass="Border" ID="_trMOCType" runat="server" >
<asp:TableCell ColumnSpan="2">
<IP:MOCType id="_MOCType" runat="server" name="moctype" displaymode="Enter" autopostback="true" />
<span id="_divExpirationDate" style="display: none;" runat="server">
<asp:Label runat="server" ID="_lbExpirDate" Text="<%$RIResources:MOC,Expiration Date%>" Visible="false"></asp:Label>
<IP:JQDate runat="server" ID="_tbExpirationDate" FromLabel='<%$RIResources:Shared,Expiration Date %>' Required="false"></IP:JQDate>
<asp:TextBox ID="_tbExpirationDate2" Visible="false" CssClass="PerformanceDateTimePicker" runat="server" Width="15%" Style="text-align: left">   </asp:TextBox>
</span>
</asp:TableCell>
</asp:TableRow>

<%--<asp:TableHeaderRow>
<asp:TableCell columnspan="2" BackColor="black" Width="100%">
<asp:Label ID="_lblStart" runat="server" EnableViewState="false" SkinID="LabelWhite" Text="" />
</asp:TableCell>
</asp:TableHeaderRow>--%>





<%--Project Manager--%>
<asp:TableRow  >
<asp:TableCell  VerticalAlign="Middle" id="cellLableOwner" Visible="false"   Width="15%"  >
<span class="ValidationError">*</span>
<asp:Label ID="_lbOwner" runat="server" Text="MOC Project Manager" EnableViewState="false" />
</asp:TableCell>

<asp:TableCell  VerticalAlign="Middle" ID="cellDropDownListOwner" Visible="false" >
<asp:DropDownList ID="_ddlOwner" Width="200" runat="server"></asp:DropDownList>

<asp:RequiredFieldValidator ValidationGroup="StartMOC" 
Enabled ="false"
ID="_rfvOwner" 
runat="server"
Display="none" 
ControlToValidate="_ddlOwner" 
ErrorMessage="Please select MOC Project Manager"
EnableClientScript="true" 
SetFocusOnError="true" 
Text="<%$RIResources:Shared,SelectMOCOwner %>">
</asp:RequiredFieldValidator>
</asp:TableCell>
</asp:TableRow>

 <%--Facility--%>
<asp:TableHeaderRow SkinID="rowheader" >
<asp:TableCell VerticalAlign="Middle" HorizontalAlign="Left"  ColumnSpan="2"  >
<span class="ValidationError">*</span>
    <asp:Label ID="_lblFacility" runat="server" Text="<%$RIResources:Shared,Facility %>" EnableViewState="false"></asp:Label>

<asp:TextBox ID="_tbDivision" runat="server" AutoPostBack="false" Style="display: none;"> </asp:TextBox>
</asp:TableCell>
</asp:TableHeaderRow  >

<asp:TableRow>
<asp:TableCell>



<asp:DropDownList ID="_ddlFacility" CausesValidation="true" runat="server" 
	AutoPostBack="false" onchange="updateDivision();"> </asp:DropDownList>
<asp:RequiredFieldValidator ValidationGroup="StartMOC" ID="_rfvFacility" runat="server"
Display="none" ControlToValidate="_ddlFacility" 
ErrorMessage="<%$RIResources:Shared,SelectFacility %>"
EnableClientScript="true" SetFocusOnError="true" 
Text="<%$RIResources:Shared,SelectFacility %>">

</asp:RequiredFieldValidator>

</asp:TableCell>
 </asp:TableRow>

    <%--space--%>
 <asp:TableRow><asp:TableCell ></asp:TableCell></asp:TableRow>

     <%--<asp:TableRow><asp:TableCell BackColor="white"></asp:TableCell></asp:TableRow>--%>



<%--Business Area Superintendent--%>
<asp:TableHeaderRow SkinID="rowheader" >
<asp:TableCell columnspan="2"  Width="100%"  HorizontalAlign="Left">
&nbsp;<span class="ValidationError">*</span>
<asp:Label CssClass="LabelHeader" ID="_lblBusiness" runat="server" EnableViewState="false" Width="25%"  Text="Business Area Superintendent for Approval" />
</asp:TableCell>
</asp:TableHeaderRow>

<asp:TableRow CssClass="Border">
<asp:TableCell>

<asp:RadioButtonList ID="_SuperintendentBusinessType" runat="server" RepeatDirection="Horizontal">
<%--	<asp:ListItem Enabled="True"  Text="Mill Wide" Value="S4" />
	<asp:ListItem Enabled="True"  Text="Paper" Value="S3" />
	<asp:ListItem Enabled="True"  Text="Power" Value="S1" />
    <asp:ListItem Enabled="True"  Text="Pulp" Value="S2" />--%>
</asp:RadioButtonList>

&nbsp;&nbsp;&nbsp&nbsp
    <asp:Label runat="server" ForeColor="red" ID="SuperintendentNameMissing"  Text=""></asp:Label>

<%--&nbsp;&nbsp;&nbsp;<asp:Label ID="_SuperintendentName" runat="server" Text="(superintnedent name goes here)"></asp:Label>--%>

<asp:RequiredFieldValidator ValidationGroup="StartMOC" ID="_rfvSuperintendentBusinessType" runat="server"
Display="none" ControlToValidate="_SuperintendentBusinessType" ErrorMessage="Select Business Area Superintendent for Approval"
EnableClientScript="true" SetFocusOnError="true" Text="Select Business Area Superintendent for Approval"></asp:RequiredFieldValidator>

</asp:TableCell>

</asp:TableRow>

</asp:Table>

    <%-- TITLE--%>
<asp:Table BorderWidth="0" BorderColor="Green" BorderStyle="Dotted" runat="server" BackColor="white">



 <asp:TableHeaderRow SkinID="rowheader" >
<asp:TableCell ID="_tcTitle" columnspan="2"   Width="100%">
&nbsp;<span class="ValidationError">*</span>
<asp:Label ID="_lblTitle" runat="server" Text="<%$RIResources:MOC,Title %>" EnableViewState="false" />
</asp:TableCell>
</asp:TableHeaderRow>



 <asp:TableRow   >
 <asp:TableCell  VerticalAlign="Middle" ColumnSpan="2" >

<asp:TextBox ID="_txtTitle" runat="server" Width="93%" ></asp:TextBox>
<%--'''JEB <asp:TextBox ID="_txtTitle" runat="server" Width="85%" OnTextChanged="FieldChanged"></asp:TextBox>--%>
 <asp:RequiredFieldValidator ValidationGroup="StartMOC" ID="_rfvTitle" runat="server"
Display="none" ControlToValidate="_txtTitle" ErrorMessage="<%$RIResources:Shared,EnterTitle %>"
EnableClientScript="true" SetFocusOnError="true" Text="<%$RIResources:Shared,EnterTitle %>"></asp:RequiredFieldValidator>
 

</asp:TableCell>
</asp:TableRow>

        <%--space--%>
 <asp:TableRow><asp:TableCell ></asp:TableCell></asp:TableRow>

</asp:Table>


<asp:Table BorderWidth="0" BorderColor="Green" BorderStyle="Dotted" runat="server" BackColor="white">

<%--Functional Narrative--%>

<asp:TableHeaderRow SkinID="rowheader" >
<asp:TableCell columnspan="2"   Width="100%">
&nbsp;<span class="ValidationError">*</span>
<asp:Label ID="_lblFunctionalHeader" EnableViewState="false" runat="server"   Text="Functional Narrative of proposed work" />
</asp:TableCell>
</asp:TableHeaderRow>


<asp:TableRow>
<asp:TableCell columnspan="2">

<IP:AdvancedTextBox id="_txtDescription" runat="server" expandheight="true" width="98%"
rows="4" textmode="MultiLine" maxlength="4000" style="width: 98%; font-size: 12px; color: Black; font-family: Verdana;"
 />


<asp:RequiredFieldValidator ValidationGroup="StartMOC" ID="_rfvNarrativeofproposedwork" runat="server"
Display="none" ControlToValidate="_txtDescription" ErrorMessage="Please enter Narrative of Proposed Work"
EnableClientScript="true" SetFocusOnError="true" Text="Please enter Narrative of Proposed Work">
</asp:RequiredFieldValidator>

</asp:TableCell>
</asp:TableRow>

        <%--space--%>
 <asp:TableRow><asp:TableCell ></asp:TableCell></asp:TableRow>

    </asp:Table>


<asp:Table BorderWidth="0" BorderColor="Green" BorderStyle="Dotted" runat="server" BackColor="white">

 <%-- Potential Impact--%>
<asp:TableHeaderRow SkinID="rowheader">
<asp:TableCell columnspan="2"  Width="100%">
&nbsp;<asp:Label ID="_lblImpact" runat="server" EnableViewState="false"  Text="Potential Impact" />
</asp:TableCell>
</asp:TableHeaderRow>


 <asp:TableRow >
<asp:TableCell Width="99%" HorizontalAlign="left" VerticalAlign="Bottom" ColumnSpan="2" >
<IP:AdvancedTextBox id="_txtImpact" runat="server" expandheight="true" width="98%"
rows="4" textmode="MultiLine" maxlength="1000" style="width: 98%; font-size: 12px; color: Black; font-family: Verdana;" />
</asp:TableCell>

</asp:TableRow>

        <%--space--%>
 <asp:TableRow><asp:TableCell ></asp:TableCell></asp:TableRow>

</asp:Table>




 <%--COST HEADING--%>
<asp:Table BorderWidth="0" BorderColor="Green" BorderStyle="Dotted" runat="server" BackColor="white">



<asp:TableHeaderRow SkinID="rowheader">
<asp:TableCell columnspan="2"  Width="100%">
&nbsp;<asp:Label ID="_lblCost" runat="server" EnableViewState="false"  Text="Costs" />
</asp:TableCell>
</asp:TableHeaderRow>


</asp:Table>
    <asp:Table BorderWidth="0" BorderColor="Green" BorderStyle="Dotted" runat="server" BackColor="white">

<asp:TableRow ID="_trEstimate2" runat="server" Style="width: 99%;" >

<asp:TableCell HorizontalAlign="left"  VerticalAlign="Top" ColumnSpan="2">
<span class="ValidationError">*</span>
<asp:Label ID="_lblMOCCosts" runat="server" EnableViewState="false" Text="Estimated cost of MOC"></asp:Label>
<b>($)</b>															  
&nbsp;
    <asp:TextBox ID="_tbCosts" MaxLength="10" runat="server" ></asp:TextBox>
<%--'''JEB<asp:TextBox ID="_tbCosts" MaxLength="10" runat="server" OnTextChanged="FieldChanged"></asp:TextBox>--%>

<ajaxToolkit:FilteredTextBoxExtender ID="_feCosts" runat="server" TargetControlID="_tbCosts"
FilterType="custom" ValidChars="()-1234567890"></ajaxToolkit:FilteredTextBoxExtender>
<asp:RequiredFieldValidator ValidationGroup="StartMOC" ID="_rfvEstimatedCost" runat="server"
Display="none" ControlToValidate="_tbCosts" ErrorMessage="Please enter Estimated Costs"
EnableClientScript="true" SetFocusOnError="true" Text="Please enter Estimated Costs">
</asp:RequiredFieldValidator>
 </asp:TableCell>


</asp:TableRow>



<asp:TableRow ID="_trEstimate1" runat="server"  Style="width: 99%;">

<asp:TableCell HorizontalAlign="left"  VerticalAlign="Middle" > 
<span class="ValidationError">*</span>
 <asp:Label ID="_lblMOCFinding" runat="server" EnableViewState="false" Text="Proposed funding source"></asp:Label>
 &nbsp;&nbsp;&nbsp;
				<asp:DropDownList ID="_ddlFunding" runat="server"  >
					<asp:ListItem Value= ""  Text=""> </asp:ListItem>
					<asp:ListItem Value= "Capital Spending" >Capital Spending</asp:ListItem>
					<asp:ListItem Value= "Indirect Spending" >Indirect Spending</asp:ListItem>
				</asp:DropDownList>
<asp:RequiredFieldValidator ValidationGroup="StartMOC" ID="_rfv_ddlFunding" runat="server"
Display="none" ControlToValidate="_ddlFunding" ErrorMessage="Please Select Proposed funding source"
EnableClientScript="true" SetFocusOnError="true" Text="Please Select Proposed funding source">
</asp:RequiredFieldValidator>
</asp:TableCell>


</asp:TableRow>



</asp:Table>
<%--END of MOC PROJECT MANAGER AND FACILITY--%>
	<br />
<asp:Table ID="_tblSuperintendent" runat="server" CellPadding="2" CellSpacing="2" BackColor="white" 
	Style="width: 99%" Visible="false" BorderWidth="0" BorderColor="#0000cc">


<asp:TableHeaderRow>
<asp:TableCell columnspan="2" BackColor="black" Width="100%">
<asp:Label ID="_lblStartSuperintendentLine" runat="server" EnableViewState="false" SkinID="LabelWhite" Text="" />
</asp:TableCell>
</asp:TableHeaderRow>


<asp:TableHeaderRow SkinID="rowheader">
<asp:TableCell   Width="100%">
&nbsp;<asp:Label ID="_lblSuperintendent" runat="server" EnableViewState="false"   Text="Superintendent" />
</asp:TableCell>
</asp:TableHeaderRow>

    	<asp:TableRow>
		<asp:TableCell>
            <span class="ValidationError">*</span>
			<asp:Label ID="_lblSuperintendentComments" runat="server" Text="Comments" /></asp:TableCell>
	</asp:TableRow>

     <asp:TableRow >
<asp:TableCell Width="99%" HorizontalAlign="left" VerticalAlign="Bottom"  >


<asp:TextBox id="_txtSuperintendentComments" AutoPostBack="false" runat="server" expandheight="true" width="98%"
rows="3" textmode="MultiLine"  maxlength="1000" style="width: 98%; font-size: 12px; color: Black; font-family: Verdana;" />


<asp:RequiredFieldValidator ValidationGroup="StartMOC" ID="_rfvSuperintendentComments" runat="server"
Display="none" Enabled="false" ControlToValidate="_txtSuperintendentComments" ErrorMessage="Please enter superintendent comments"
EnableClientScript="true" SetFocusOnError="true" Text="Please enter superintendent comments">
</asp:RequiredFieldValidator>

</asp:TableCell>
</asp:TableRow>
</asp:Table>



<%--Read only Comments--%>
<asp:Table ID="_tblSuperintendentComments" runat="server" CellPadding="2" CellSpacing="2" BackColor="white" 
	Style="width: 99%" Visible="false" BorderWidth="0" BorderColor="#0000cc">


<asp:TableHeaderRow>
<asp:TableCell columnspan="2" BackColor="black" Width="100%">
<asp:Label ID="_lblStartSuperintendentLineComments" runat="server" EnableViewState="false" SkinID="LabelWhite" Text="" />
</asp:TableCell>
</asp:TableHeaderRow>


<asp:TableHeaderRow SkinID="rowheader">
<asp:TableCell   Width="100%">
&nbsp;<asp:Label ID="_lblSuperintendentCommentsComments" runat="server" EnableViewState="false"  Text="Superintendent Comments" />
</asp:TableCell>
</asp:TableHeaderRow>


<asp:TableRow >
<asp:TableCell Width="99%" HorizontalAlign="left" VerticalAlign="Bottom"  >
<%--<IP:AdvancedTextBox id="_txtSuperintendentCommentsComments" runat="server" ReadOnly="true" expandheight="true" width="98%"
rows="3" textmode="MultiLine" maxlength="1000" style="width: 98%; font-size: 12px; color: Black; font-family: Verdana;" />--%>

    <asp:TextBox id="_txtSuperintendentCommentsComments" runat="server" expandheight="true" width="98%"
rows="3" textmode="MultiLine" maxlength="1000" style="width: 98%; font-size: 12px; color: Black; font-family: Verdana;" />


</asp:TableCell>
</asp:TableRow>
</asp:Table>


</asp:TableCell>
</asp:TableRow>
</asp:Table>



	
<div style="text-align:center;">



<asp:Button ID="_btnSubmit" clientidmode="static" Text="Proceed with development" runat="server" ValidationGroup="StartMOC"  OnClientClick="if(Page_ClientValidate('StartMOC')) return ShowModalPopup();"></asp:Button>
   
&nbsp;&nbsp; <IP:SpellCheck id="_btnSpell" runat="server" ControlIdsToCheck="_txtTitle,_txtDescription,_txtImpact"  />

&nbsp;&nbsp;<asp:Button ID="_btnAttachment" runat="server" Visible="false" Text='<%$RIResources:Shared,Attachments %>' /> 
<br />
	<asp:Label ID="LabelEmailSent" runat="server" Text="" style="width: 50%; font-size: 16px; color: Black; font-family: Verdana;"
></asp:Label> 
</div>

<div style="text-align:left;">

<asp:Button ID="_btnApprove" clientidmode="static" Visible="false" Text="Approve" runat="server" ValidationGroup="StartMOC"  OnClientClick="if(Page_ClientValidate('StartMOC')) return ShowModalPopup();"></asp:Button>
 
&nbsp;&nbsp;
 
<asp:Button ID="_btnDenied" clientidmode="static" Visible="false" Text="Denied" runat="server" ValidationGroup="StartMOC"  OnClientClick="if(Page_ClientValidate('StartMOC')) return ShowModalPopup();"></asp:Button>
 


&nbsp;&nbsp;<asp:Button ID="_btnAttachmentSuperintendent" runat="server" Visible="false" Text='<%$RIResources:Shared,Attachments %>' /> 
 
</div>      

<br />
<asp:Table ID="_tblUpdate" runat="server" CellPadding="2" CellSpacing="2" BackColor="white" Style="width: 99%" Visible="true" BorderWidth="0" BorderColor="#0000cc">



<asp:TableRow CssClass="Border">
<asp:TableCell Width="25%">
<asp:Label ID="_lblCreatedBy" runat="server" EnableViewState="false"></asp:Label>
</asp:TableCell><asp:TableCell Width="25%">
<asp:Label ID="_lblCreatedDate" runat="server" EnableViewState="false"></asp:Label>
</asp:TableCell><asp:TableCell Width="25%">
<asp:Label ID="_lblUpdatedBy" runat="server" EnableViewState="false"></asp:Label>
</asp:TableCell><asp:TableCell Width="25%">
<asp:Label ID="_lblLastUpdateDate" runat="server" EnableViewState="false"></asp:Label>
</asp:TableCell></asp:TableRow></asp:Table>

 </ContentTemplate>
</asp:UpdatePanel> 





<%-- ==================================NOT SURE ABOUT THE STUFF BELOW ================================== --%>




</asp:Content>