<%@ Page Language="VB" MasterPageFile="~/RI.master" AutoEventWireup="false" CodeFile="ViewMOC.aspx.vb"
	Inherits="MOC" EnableTheming="true"

	EnableEventValidation="false" Title="Graphic Packaging International: MOC" Trace="false"
	MaintainScrollPositionOnPostback="true" 
	UnobtrusiveValidationMode="None"
%>

<%@ MasterType VirtualPath="~/RI.master" %>

<%@ Register Src="~/User Controls/Common/ucSiteLocation.ascx" TagName="SiteLocation" TagPrefix="IP" %>
<%@ Register Src="~/User Controls/Common/ucDateRange.ascx" TagName="DateRange" TagPrefix="IP" %>
<%@ Register Src="~/User Controls/Common/ucMOCStatus.ascx" TagName="ucMOCStatus" TagPrefix="IP" %>
<%@ Register Src="~/User Controls/Common/ucMOCTypes.ascx" TagName="MOCType" TagPrefix="IP" %>
<%@ Register Src="~/User Controls/Common/ucMOCClassification.ascx" TagName="MOCClass" TagPrefix="IP" %>
<%@ Register Src="~/User Controls/Common/ucMOCCategory.ascx" TagName="MOCCategory" TagPrefix="IP" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="_contentMain" ContentPlaceHolderID="_cphMain" EnableViewState="true" runat="Server">

<style>
.hyperlink a
	{
	   color:blue !important;
	}


</style>


<%--	<telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" RenderMode="Lightweight">
</telerik:RadStyleSheetManager>--%>

<telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1"  runat="server" >
<%--<telerik:RadAjaxManager ID="RadAjaxManager" runat="server" UpdateInitiatorPanelsOnly="true">--%>
		
	<AjaxSettings>

		   <telerik:AjaxSetting AjaxControlID="ConfiguratorPanel1">
				<UpdatedControls>
					<telerik:AjaxUpdatedControl ControlID="RadGridMocListing" LoadingPanelID="RadAjaxLoadingPanel1" />
					<telerik:AjaxUpdatedControl ControlID="ConfiguratorPanel" />					
				</UpdatedControls>
			</telerik:AjaxSetting>
			
			<telerik:AjaxSetting AjaxControlID="RadGridMocListing">
			<UpdatedControls>
			<telerik:AjaxUpdatedControl ControlID="RadGridMocListing" LoadingPanelID="RadAjaxLoadingPanel1"  />
			</UpdatedControls>
			</telerik:AjaxSetting>

		
	</AjaxSettings>

<%--</telerik:RadAjaxManager>--%>
    </telerik:RadAjaxManagerProxy>




	<IP:SiteLocation ID="_siteLocation" runat="server"/>

<asp:Table runat="server">
<asp:TableRow>
<asp:TableCell>
<asp:Label ID="_lblProjectKickoffDate" runat="server"  Visible="false"
Text="Team proposed review date" 
EnableViewState="false"></asp:Label>


<telerik:RadDatePicker ID="_tbMOCProjectKickoffDate" runat="server"  Visible="false"
		RenderMode="Lightweight" Skin="Telerik">
	   <HideAnimation Duration="111" />
	<ShowAnimation Duration="300" />
</telerik:RadDatePicker>


<%--&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--%>
<asp:Label ID="_lblStartDate" runat="server" 
Text="Start Date" 
EnableViewState="false"></asp:Label>
&nbsp; 
<telerik:RadDatePicker ID="_txtStartDate" runat="server" 
		RenderMode="Lightweight" Skin="Telerik">
	   <HideAnimation Duration="111" />
	<ShowAnimation Duration="300" />
</telerik:RadDatePicker>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
 <asp:Label ID="_lblEndDate" runat="server" 
Text="End Date" 
EnableViewState="false"></asp:Label>
&nbsp;	
<telerik:RadDatePicker ID="_txtEndDate" runat="server" 
		RenderMode="Lightweight" Skin="Telerik">
	   <HideAnimation Duration="111" />
	<ShowAnimation Duration="300" />
</telerik:RadDatePicker>

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
  <asp:Label ID="_lblDateRange" runat="server" Text="<%$ RIResources:Shared,DateRange %>"
					EnableViewState="false"></asp:Label> &nbsp;
				   <asp:DropDownList AutoPostBack="true" ID="_ddlDateRange" runat="server">
					</asp:DropDownList>

</asp:TableCell>


</asp:TableRow>

</asp:Table>


<%--<IP:DateRange ID="_DateRange" runat="server" />

<br />--%>
	

<IP:ucMOCStatus runat="server" ID="MOCStatus" name="MOCStatus" DisplayMode="Search" />
<IP:MOCType ID="_MOCType" runat="server" DisplayMode="Search" />


<asp:Table ID="TableCurrentApprover" runat="Server" CssClass="Border"
BorderStyle="Solid" BorderColor="Purple" BorderWidth="0">
<asp:TableRow CssClass="Header">
<asp:TableCell HorizontalAlign="left">
<asp:Label ID="LabelMOCInformation" runat="server" EnableViewState="false" SkinID="LabelWhite" Text="MOC" />
  </asp:TableCell></asp:TableRow></asp:Table><asp:table runat="server"  BorderColor="red" BorderStyle="dotted" BorderWidth="0"  > 
<asp:TableRow ID="_tr"   runat="server">

			<asp:TableCell HorizontalAlign="left" Width="99%">
			<asp:Label ID="_lbMoc" runat="server" Text="<%$RIResources:Shared,MOCNumber %>"
			CssClass="LabelBlackBold"  BackColor="white"/>
			&nbsp;<asp:TextBox ID="_tbMocNumber" Width="100px" runat="server"></asp:TextBox>
			
			&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			
			<asp:Label ID="_lbOwner" runat="server" Text="<%$RIResources:Shared,MOC Owner %>"
			BackColor="white" />
				<ajaxToolkit:CascadingDropDown ID="_cddlOwner" runat="server" Category="Trigger" LoadingText="[Loading Owner...]"
				PromptText="   " ServiceMethod="GetMOCOwner" ServicePath="~/CascadingLists.asmx"
				TargetControlID="_ddlOwner" ParentControlID="ctl00__cphMain__siteLocation__ddlFacility">
				</ajaxToolkit:CascadingDropDown>
			&nbsp;<asp:DropDownList ID="_ddlOwner" runat="server"></asp:DropDownList>

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

			<asp:Label ID="_lbInitiator" runat="server"  Text="<%$RIResources:Shared,Initiator %>"
			BackColor="white" />
				<ajaxToolkit:CascadingDropDown ID="_cddlInitiator" runat="server" Category="Trigger" LoadingText="[Loading Initiator...]"
				PromptText="   " ServiceMethod="GetMOCInitiator" ServicePath="~/CascadingLists.asmx"
				TargetControlID="_ddlInitiator" ParentControlID="ctl00__cphMain__siteLocation__ddlFacility">
				</ajaxToolkit:CascadingDropDown>
			&nbsp;<asp:DropDownList ID="_ddlInitiator" runat="server"></asp:DropDownList>
	
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

		<asp:Label ID="_lblTitleSearch" runat="server" CssClass="LabelBlackBold"  Text="<%$ RIResources:Shared,Title Search %>"></asp:Label>
		&nbsp;&nbsp;<asp:TextBox runat="server" ID="_txtTitleSearch" width="30%" MaxLength="50" ></asp:TextBox>
		
		
</asp:TableCell></asp:TableRow></asp:table>

<IP:MOCClass ID="MOCClass" runat="server" DisplayMode="Search" />

	<IP:MOCCategory ID="MOCCategory" runat="server" DisplayMode="Search" />


	<%--Search results--%>
<%--	<Asp:UpdatePanel 
ChildrenAsTriggers="true" 
ID="_upViewScreen" 
runat="server" 
EnableViewState="true"
UpdateMode="always">--%>

<%--<Asp:UpdatePanel  ID="_upViewScreen"   runat="server" >
		
<ContentTemplate>--%>
 <div style="text-align: center">
<asp:Table CellPadding="5"  Width="100%" runat="server"  BorderColor="red" BorderStyle="solid" BorderWidth="0">
<asp:TableRow>
<asp:TableCell ></asp:TableCell>

<asp:TableCell HorizontalAlign="right" >				
<asp:Button ID="_btnViewUpdate" Text="<%$RIResources:Shared,ViewUpdate %>" runat="server" /> &nbsp
<asp:Button ID="_btnExcel" Text="<%$RIResources:Shared,Excel %>" runat="server" />
</asp:TableCell>

<asp:TableCell HorizontalAlign="right" >
<asp:Label ID="_lblRecordCount" runat="server" Style="text-align: left" Font-Size="11pt" Text="<%$RIResources:Shared,RecordCount%>"
						BackColor="White" ForeColor="Black"></asp:Label>&nbsp
<asp:Label ID="_lblRecCount" runat="server" Style="text-align: left" Text="0" Font-Size="11pt" BackColor="White"
						ForeColor="Black"> </asp:Label>&nbsp&nbsp&nbsp&nbsp&nbsp
</asp:TableCell>

</asp:TableRow>


</asp:Table>
			
  



 			
			</div>
		
			

	     <asp:ImageButton ID="DownloadCSV" runat="server" 
	 OnClick="DownloadCSV_Click"  Visible="true"
	 ImageUrl="~/images/file-extension-csv-icon_bigger.png"
            CssClass="pdfButton">

 </asp:ImageButton>  

  <%--Start of telerik Grid--%>
  <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1">
 </telerik:RadAjaxLoadingPanel>    
<telerik:RadGrid RenderMode="Lightweight" ID="RadGridMocListing" runat="server"

OnNeedDataSource="RadGridMocListing_NeedDataSource"
			Width="99%"
			Visible="true"
			AllowPaging="true"
			ShowGroupPanel="true"
			
			AllowSorting="true"
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

			EnableLinqExpressions="false"

			Skin="Office2010Silver">

<ExportSettings>
     <Excel DefaultCellAlignment="Left" Format="Xlsx" WorksheetName="MOCListing" />
<Pdf PageWidth="297mm" PageHeight="210mm" />
</ExportSettings>




<MasterTableView CommandItemDisplay="Top" Width="100%" >
		<%--TableLayout="Fixed"--%>
		<GroupHeaderItemStyle Height="10px" /> 
		<PagerStyle AlwaysVisible="true" />

<CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="false" />

<Columns>

<telerik:GridBoundColumn HeaderStyle-Width="9%" AllowSorting="true" AllowFiltering="false"   DataField="SiteName" HeaderText="Site" ></telerik:GridBoundColumn>

<%--<telerik:GridBoundColumn HeaderStyle-Width="6%" AllowSorting="true" AllowFiltering="false" DataField="kickoffdate" ItemStyle-HorizontalAlign="Left" HeaderText="Kickoff Date"  DataFormatString="{0:M/d/yyyy}"></telerik:GridBoundColumn>--%>
<telerik:GridBoundColumn HeaderStyle-Width="6%" AllowSorting="true" AllowFiltering="false" DataField="STARTDATE" ItemStyle-HorizontalAlign="Left" HeaderText="Implenentation Date"  HeaderStyle-Wrap="true"	DataFormatString="{0:M/d/yyyy}"></telerik:GridBoundColumn>
 
<telerik:GridBoundColumn HeaderStyle-Width="7%" AllowSorting="true" AllowFiltering="false" DataField="RISUPERAREA" HeaderText="Business Unit" ></telerik:GridBoundColumn>
<telerik:GridBoundColumn HeaderStyle-Width="6%" AllowSorting="true" AllowFiltering="false" DataField="SUBAREA" HeaderText="Area" ></telerik:GridBoundColumn>
<telerik:GridBoundColumn HeaderStyle-Width="6%" AllowSorting="true" AllowFiltering="false" DataField="Area" HeaderText="Line" ></telerik:GridBoundColumn>


<%--PRODUCTION--%>
<%--				<telerik:GridHyperLinkColumn 
					HeaderStyle-Width="5%"
					DataTextField="MOCNUMBER"
					DataNavigateUrlFormatString="http://gpimv.graphicpkg.com/cereporting/CrystalReportDisplay.aspx?Report=MOCSummary&mocNumber={0}"
					DataNavigateUrlFields="MOCNUMBER"
					UniqueName="HypColReport" Target="_blank"
					HeaderText="Report"
					AllowFiltering="false" 
					AllowSorting="true" >
					<ItemStyle CssClass="hyperlink" />
					</telerik:GridHyperLinkColumn>--%>

					<%--PRODUCTION--%>
<%--					<telerik:GridHyperLinkColumn 
					HeaderStyle-Width="5%"
					DataTextField="MOCNUMBER"
					DataNavigateUrlFormatString="http://gpiazmeswebp01:6767/ReportMOCSummary.aspx?ReportName=MOCSummary&MOCNumber={0}"
					DataNavigateUrlFields="MOCNUMBER"
					UniqueName="HypColReport" Target="_blank"
					HeaderText="Report"
					AllowFiltering="false" 
					AllowSorting="true" >
					<ItemStyle CssClass="hyperlink" />
					</telerik:GridHyperLinkColumn>--%>

				
	
	<%--DEVELOPEMENT--%>
				<telerik:GridHyperLinkColumn 
					HeaderStyle-Width="5%"
					DataTextField="MOCNUMBER"
					DataNavigateUrlFormatString="http://gpiazmeswebp01:7777/ReportMOCSummary.aspx?ReportName=MOCSummary&MOCNumber={0}"
					DataNavigateUrlFields="MOCNUMBER"
					UniqueName="HypColReport" Target="_blank"
					HeaderText="Report"
					AllowFiltering="false" 
					AllowSorting="true" >
					<ItemStyle CssClass="hyperlink" />
					</telerik:GridHyperLinkColumn>
<%--
				<telerik:GridHyperLinkColumn 
					HeaderStyle-Width="25%"
					DataTextField="Title"
					DataNavigateUrlFormatString="~/MOC/EnterMOC.aspx?MOCNumber={0}"
					DataNavigateUrlFields="MOCNUMBER"
					UniqueName="HypColTitle" 
					HeaderText="Title"
					AllowSorting="true"
					AllowFiltering="false" >
					<ItemStyle CssClass="hyperlink" />
					</telerik:GridHyperLinkColumn>
--%>


    <telerik:GridTemplateColumn 
    UniqueName="TemplateLinkColumn" 
    AllowFiltering="false" 
    HeaderText="Title">
    <ItemTemplate>
        <asp:HyperLink ID="Link" runat="server"></asp:HyperLink>
    </ItemTemplate>
        <HeaderStyle Width="23%" />
        <ItemStyle CssClass="hyperlink" />
</telerik:GridTemplateColumn>

<telerik:GridBoundColumn HeaderStyle-Width="10%" AllowSorting="true" AllowFiltering="false" DataField="person" HeaderText="Initiator"  DataType="System.String"></telerik:GridBoundColumn>
<telerik:GridBoundColumn HeaderStyle-Width="5%" AllowSorting="true" AllowFiltering="false" DataField="MOCType" HeaderText="Type of Change" HeaderStyle-Wrap="true"  DataType="System.String"></telerik:GridBoundColumn>
<telerik:GridBoundColumn HeaderStyle-Width="5%" AllowSorting="true" AllowFiltering="false" DataField="Savings"  ItemStyle-HorizontalAlign="Right" dataFormatString="{0:$###,##0.00}" Aggregate="Sum" FooterAggregateFormatString="{0:C}" HeaderText="Savings"   ></telerik:GridBoundColumn>
				  
<telerik:GridBoundColumn HeaderStyle-Width="13%" AllowSorting="true" AllowFiltering="false" DataField="Status" HeaderText="MOC Status" HeaderStyle-Wrap="true"  DataType="System.String"></telerik:GridBoundColumn>


<%--setting Display=false will still let you have access to the data but will not display or export--%>
<telerik:GridBoundColumn  Display="false"  AllowSorting="true" AllowFiltering="false" DataField="MOCNUMBER" HeaderText="MOC #" HeaderStyle-Wrap="true"  DataType="System.String"></telerik:GridBoundColumn>
<telerik:GridBoundColumn Display="false"  AllowSorting="true" AllowFiltering="false" DataField="Title" HeaderText="t" HeaderStyle-Wrap="true"  DataType="System.String"></telerik:GridBoundColumn>




</Columns>


</MasterTableView>



<ClientSettings ReorderColumnsOnClient="True" AllowDragToGroup="true" AllowColumnsReorder="True">
<Scrolling AllowScroll="true" UseStaticHeaders="true" ScrollHeight="" />
<Selecting AllowRowSelect="True"></Selecting>
<Resizing
AllowRowResize="False"
AllowColumnResize="False"
EnableRealTimeResize="False"
ResizeGridOnColumnResize="False"></Resizing>

 
</ClientSettings>

<GroupingSettings ShowUnGroupButton="true" ></GroupingSettings>

<FilterMenu RenderMode="Lightweight"></FilterMenu>

<HeaderContextMenu RenderMode="Lightweight"></HeaderContextMenu>

<PagerStyle Mode="NextPrevAndNumeric" PageSizeControlType="None"></PagerStyle>
</telerik:RadGrid>


<%--End of telerik Grid--%>

	 
   
		 
<%--		
 </ContentTemplate>

</Asp:UpdatePanel>--%>


</asp:Content>
