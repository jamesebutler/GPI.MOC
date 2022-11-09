<%@ Page Title="Graphic Packaging: My MOCs" 
EnableTheming="true"
Language="VB" 
MasterPageFile="~/RI.master" 
AutoEventWireup="false" 
CodeFile="MyMOCsNew.aspx.vb" 
Inherits="MOC_MyMOCsNew" 
EnableEventValidation="false"
Trace="false"
MaintainScrollPositionOnPostback="true"
%>


<%@ MasterType VirtualPath="~/RI.master" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="_contentMain" ContentPlaceHolderID="_cphMain" EnableViewState="true" runat="Server">

	<telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" RenderMode="Lightweight">
    </telerik:RadStyleSheetManager>

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">

            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="RadGridMOCListing">
                <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="RadGridMOCListing" LoadingPanelID="RadAjaxLoadingPanel" UpdatePanelCssClass="" />
                </UpdatedControls>
                </telerik:AjaxSetting>

<%--            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel" UpdatePanelCssClass="" />
                </UpdatedControls>
                </telerik:AjaxSetting>	--%>

            </AjaxSettings>

</telerik:RadAjaxManager>

<style type="text/css">
	  .example
      {
       font-size:16px;
	   font-family: verdana,'Segoe UI', Tahoma, Geneva, sans-serif;
       text-align:center;
       color:red;
      }

</style>

	<Asp:UpdatePanel ChildrenAsTriggers="true" ID="_upViewScreen" runat="server" EnableViewState="true"
		UpdateMode="always">
		<ContentTemplate>
        <span id="_spMOCReqAttention" runat="server" backcolor="#CCCCCC"  >
            <asp:Label ID="_lbReqAttention" runat="server" Text="<%$ RIResources:Shared,MOCS Requiring Your Attention %>" Font-Bold="True" Font-Size="Large"></asp:Label>


<div style="width: 100%; " class="HeaderNewStuff"> *** GRID ImplOverride **** </div> 

        <span id="_spImplOverride" runat="server" style="border: thin solid #000000;">
        <asp:Label ID="_lbImplOverride" runat="server" Text="0" BackColor="#CCCCCC" Font-Size="10pt" Width="100%" Font-Italic="True" ></asp:Label>
        <br />

			<asp:GridView ID="_gvImplOverride" runat="server"  CssClass="BorderSecondary" AutoGenerateColumns="False" DataKeyNames="MOCNUMBER" EnableViewState="false"
				BorderWidth="2" Width="100%" HeaderStyle-BorderColor="red" HeaderStyle-Font-Bold="True" HeaderStyle-HorizontalAlign="Left" GridLines="None" >
				<Columns>
				    <asp:HyperLinkField HeaderText="MOC" HeaderStyle-horizontalalign="Left" DataTextField="MOCNUMBER" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Left"
						DataNavigateUrlFields="MOCNUMBER" DataNavigateUrlFormatString="~/MOC/EnterMOC.aspx?MOCNumber={0}"
						Target="_self" />
                    <asp:TemplateField ItemStyle-Width="5%" HeaderStyle-horizontalalign="Left" HeaderText="<%$RIResources:Shared,Date %>" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="_lblDate" runat="server" Font-Bold="false" Text='<%# Eval("EventDate", "{0:d}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
					<asp:TemplateField ItemStyle-Width="25%" HeaderStyle-horizontalalign="Left" HeaderText="<%$RIResources:Shared,Title %>" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="_lblTitle" runat="server" Font-Bold="false" Text='<%# Bind("TITLE") %>'></asp:Label><br />
                            <asp:Label ID="_lbDescription" runat="server" Font-Bold="false" Text='<%# Bind("DESCRIPTION") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="10%" HeaderStyle-horizontalalign="Left" HeaderText="<%$RIResources:MOC,Initiator/Owner %>" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="_lblInitiator" runat="server" Font-Bold="false" Text='<%# Bind("initiatorname") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    </Columns>
			</asp:GridView>

        </span>
        <br />
<div style="width: 100%; " class="HeaderNewStuff"> *** GRID ComplOverride **** </div>

        <span id="_spComplOverride" runat="server" style="border: thin solid #000000;">
                
        <div class="MyMOCsLabel">
        <asp:Label ID="_lbComplOverride" runat="server" Text="0" BackColor="#CCCCCC" Font-Size="12pt" Width="100%" Font-Italic="True" BorderColor="Black" ></asp:Label>
        </div>			


            <asp:GridView ID="_gvComplOverride" runat="server" CssClass="BorderSecondary"
				BorderWidth="2" AutoGenerateColumns="False" DataKeyNames="MOCNUMBER" EnableViewState="false"
				Width="100%" HeaderStyle-BorderColor="red" HeaderStyle-Font-Bold="True"
                GridLines="None" >
				<Columns>
				    <asp:HyperLinkField HeaderText="MOC" HeaderStyle-horizontalalign="Left" DataTextField="MOCNUMBER" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Left"
						DataNavigateUrlFields="MOCNUMBER" DataNavigateUrlFormatString="~/MOC/EnterMOC.aspx?MOCNumber={0}"
						Target="_self" >
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" Width="5%" />
                    </asp:HyperLinkField>
                    <asp:TemplateField ItemStyle-Width="5%" HeaderStyle-horizontalalign="Left" HeaderText="<%$RIResources:Shared,Date %>" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="_lblDate" runat="server" Font-Bold="false" Text='<%# Eval("EventDate", "{0:d}") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" Width="5%" />
                    </asp:TemplateField>
					<asp:TemplateField ItemStyle-Width="25%" HeaderStyle-horizontalalign="Left" HeaderText="<%$RIResources:Shared,Title %>" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="_lblTitle" runat="server" Font-Bold="false" Text='<%# Bind("TITLE") %>'></asp:Label><br />
                            <asp:Label ID="_lbDescription" runat="server" Font-Bold="false" Text='<%# Bind("DESCRIPTION") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" Width="25%" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="10%" HeaderStyle-horizontalalign="Left" HeaderText="<%$RIResources:MOC,Initiator/Owner %>" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="_lblInitiator" runat="server" Font-Bold="false" Text='<%# Bind("initiatorname") %>' ></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                    </asp:TemplateField>
                    </Columns>
			    <HeaderStyle BorderColor="Black" Font-Bold="True" />
			</asp:GridView>
        </span>
        <br />

<div style="width: 100%; " class="HeaderNewStuff"> **** GRID Approved But Not Implemented ****</div>


        <span id="_spApprovedNotImplemented" runat="server" style="border: thin solid #000000;">
        <%--                <asp:Label ID="_lbApproveNotImpl" runat="server" Text="0" BackColor="#CCCCCC" 
        Font-Size="10pt" Width="100%" Font-Italic="True" BorderColor="Black" ></asp:Label>--%>
        <div class="MyMOCsLabel">
        <asp:Label ID="_lbApproveNotImpl" runat="server" Text="0" CssClass="MyMOCsLabel" Font-Size="12pt" Width="100%" Font-Italic="True" BorderColor="Black" ForeColor="White"  ></asp:Label>
        </div>		

            <asp:GridView ID="_gvApprovedNotImpl" runat="server" CssClass="BorderSecondary" BorderColor="Black"
				BorderWidth="2" AutoGenerateColumns="False" DataKeyNames="MOCNUMBER" EnableViewState="false"
				AllowSorting="true" Width="100%" 
                HeaderStyle-BorderColor="Black" HeaderStyle-BorderWidth="1px" HeaderStyle-Font-Bold="True"
                GridLines="Both" 
                >

                      <alternatingrowstyle backcolor="WhiteSmoke"  
                                  forecolor="Black"
                                  font-italic="false"/>

                        <rowstyle backcolor="White"  
                           forecolor="Black"
                           font-italic="false" />
				<Columns>
				    
                    <asp:HyperLinkField HeaderText="MOC" HeaderStyle-horizontalalign="Left" DataTextField="MOCNUMBER" 
                        ItemStyle-Width="6%" ItemStyle-HorizontalAlign="Center"
						DataNavigateUrlFields="MOCNUMBER" DataNavigateUrlFormatString="~/MOC/EnterMOC.aspx?MOCNumber={0}"
						Target="_self" >
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" Width="6%" />
                    </asp:HyperLinkField>
                    
                    <asp:TemplateField ItemStyle-Width="6%" HeaderStyle-horizontalalign="Left" HeaderText="<%$RIResources:Shared,Date %>" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="_lblDate" runat="server" Font-Bold="false" Text='<%# Eval("EventDate", "{0:d}") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" Width="6%" />
                    </asp:TemplateField>
					
                    <asp:TemplateField ItemStyle-Width="73%" HeaderStyle-horizontalalign="Left" HeaderText="<%$RIResources:Shared,Title %>" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="_lblTitle" runat="server" Font-Bold="false" Text='<%# Bind("TITLE") %>'></asp:Label><br />
                            <asp:Label ID="_lbDescription" runat="server" Font-Bold="false" Text='<%# Bind("DESCRIPTION") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" Width="73%" />
                    </asp:TemplateField>
                    
                    <asp:TemplateField ItemStyle-Width="15%" HeaderStyle-horizontalalign="Left" HeaderText="<%$RIResources:MOC,Initiator/Owner %>" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="_lblInitiator" runat="server" Font-Bold="false" Text='<%# Bind("initiatorname") %>' ></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" Width="15%" />
                    </asp:TemplateField>
                    </Columns>
			    <HeaderStyle BorderColor="Black" Font-Bold="True" />
			</asp:GridView>
        </span>
        <br />

<div style="width: 100%; " class="HeaderNewStuff">**** GRID MOC's Listing ****</div> 

        <span id="_spPending" runat="server" backcolor="#CCCCCC" style="border: thin solid #000000;" >

        <div class="MyMOCsLabel">			        
        <asp:Label ID="_lblRecCount" runat="server" Text="0" CssClass="MyMOCsLabel" Font-Size="12pt" Width="100%" Font-Italic="True" BorderColor="Black" ForeColor="White"  ></asp:Label>
        </div>


			<asp:GridView ID="_gvMOCListing" runat="server" CssClass="BorderSecondary" BorderColor="Black"
				BorderWidth="2" AutoGenerateColumns="False" DataKeyNames="approval_seqid" EnableViewState="false"
				AllowSorting="true" Width="100%"   
                HeaderStyle-BorderColor="Black" HeaderStyle-BorderWidth="1px" HeaderStyle-Font-Bold="True"
                GridLines="Both" 
                >
                <headerstyle backcolor="#cccccc"   forecolor="Black"/>
                <alternatingrowstyle backcolor="WhiteSmoke"  
                forecolor="Black"
                font-italic="false"/>

                <rowstyle backcolor="White"  
                forecolor="Black"
                font-italic="false" />

				<Columns>
				    <asp:HyperLinkField HeaderText="MOC"
                        DataTextField="MOCNUMBER"
                        ItemStyle-Width="5%"
                        ItemStyle-HorizontalAlign="Center"
                        HeaderStyle-HorizontalAlign="Center"
						DataNavigateUrlFields="MOCNUMBER"
                        DataNavigateUrlFormatString="~/MOC/EnterMOC.aspx?MOCNumber={0}"
						Target="_self" />

                    <asp:TemplateField ItemStyle-Width="5%" HeaderText="<%$RIResources:Shared,Date %>" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="_lblDate" runat="server" Font-Bold="false" Text='<%# Eval("EventDate", "{0:d}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
					<asp:TemplateField ItemStyle-Width="30%" HeaderText="<%$RIResources:Shared,Title %>" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="_lblTitle" runat="server" Font-Bold="false" Text='<%# Bind("TITLE") %>'></asp:Label><br />
                            <asp:Label ID="_lbDescription" runat="server" Font-Bold="false" Text='<%# Bind("DESCRIPTION") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="7%" HeaderText="<%$RIResources:MOC,Status %>" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:HiddenField ID="_hfInitiatorEmail" runat="server" Value='<%# Bind("EMAIL") %>' />
                            <asp:Label ID="_lblType" runat="server" Font-Bold="false" Text='<%# Bind("APPROVAL_TYPE") %>'></asp:Label>
                            <asp:Label ID="_lblMOCNUmber" runat="server" visible="false" Text='<%# Bind("MOCNUmber") %>'></asp:Label>
                            <asp:label id="_lbLevel" runat="server" Text='<%# Bind("approval_level") %>' Visible="False"></asp:label>
				        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="8%" HeaderText="<%$RIResources:MOC,Initiator %>" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="_lblInitiator" runat="server" Font-Bold="false" Text='<%# Bind("initiatorname") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField  HeaderText="<%$RIResources:MOC,ApproveReview %>" ItemStyle-Width="7%" Itemstyle-Font-Bold="false" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                         <ItemTemplate>
                            <asp:CheckBoxList Font-Bold="false" ID="_cbApproval" runat="server" RepeatDirection="Horizontal" onclick="CheckBoxToRadio(this,2)">
                                <asp:ListItem Text="<%$RIResources:Shared,Yes %>" Value="Y" ></asp:ListItem>
                            </asp:CheckBoxList>
                        </ItemTemplate>
                    </asp:TemplateField>
					<asp:TemplateField HeaderText="<%$RIResources:MOC,Comments %>" ItemStyle-Width="30%" HeaderStyle-HorizontalAlign="Left">
                         <ItemTemplate>
                            <asp:TextBox ID="_tbComment"  TextMode="MultiLine" runat="server" Font-Bold="false"  Width="99%">
                            </asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
				</Columns>
			</asp:GridView>
            <br />
			<div style="text-align: center">
				<asp:Button ID="_btnSave" Text="<%$RIResources:Shared,Save %>" runat="server" />
			</div>
        </span>
        </span>

<div style="width: 100%; " class="HeaderNewStuff"> *** NEW GRID MOCListing **** </div> <br />

  <%--Start of telerik Grid RadGridMOCListing --%>
<telerik:RadGrid RenderMode="Lightweight" ID="RadGridMOCListing" runat="server"

          AllowAutomaticUpdates="True"
            Width="99%"
            PageSize="2"
            AutoGenerateColumns="False"
            HeaderStyle-HorizontalAlign="Left"
            HeaderStyle-Font-Underline="true"
            HeaderStyle-Wrap="false"
            HeaderStyle-Font-Bold="true"
            HeaderStyle-Font-Size="13px"
            HeaderStyle-Font-Names="Arial"
            ItemStyle-Font-Names="Arial"
            ItemStyle-Font-Size="13px"
            ItemStyle-Font-Bold="false"
            ItemStyle-BackColor="White"
            AlternatingItemStyle-Font-Names="Arial"
            AlternatingItemStyle-Font-Bold="false"
            AlternatingItemStyle-Font-Size="13px"
            Skin="Metro">


        <MasterTableView
        CommandItemDisplay="Top"
        TableLayout="Fixed"
        DataKeyNames="approval_seqid">

        <PagerStyle AlwaysVisible="true" />

        <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="false" />

            <Columns>
            <telerik:GridEditCommandColumn HeaderStyle-Width="3px" HeaderText="Edit" UniqueName="EditCommandColumn" >
            <HeaderStyle Width="3px" />
            </telerik:GridEditCommandColumn>


        <telerik:GridBoundColumn    AllowSorting="false" 
                                        ReadOnly="true" 
                                        AllowFiltering="false"
                                        HeaderStyle-Width="5px" 
                                        DataField="MOCNUMBER" 
                                        HeaderText="MOC"
                                        DataType="System.String" 
                                        >
            <HeaderStyle Width="5px" />
			</telerik:GridBoundColumn>


            <telerik:GridBoundColumn    AllowSorting="false"
                                        ReadOnly="true"
                                        AllowFiltering="false"
                                        HeaderStyle-Width="5px" 
                                        DataField="EventDate" 
                                        HeaderText="Date"
                                        ItemStyle-HorizontalAlign="Left"
                                        DataFormatString="{0:M/d/yyyy}">
            <HeaderStyle Width="5px" />
            <ItemStyle HorizontalAlign="Left" />
            </telerik:GridBoundColumn>

            <telerik:GridBoundColumn    AllowSorting="false" 
                                        ReadOnly="true" 
                                        AllowFiltering="false"
                                        HeaderStyle-Width="10px" 
                                        DataField="TITLE" 
                                        HeaderText="TITLE"
                                        DataType="System.String" 
                                        >
            <HeaderStyle Width="25px" />
			</telerik:GridBoundColumn>



			           
            <telerik:GridBoundColumn    AllowSorting="false" 
                                        ReadOnly="true" 
                                        AllowFiltering="false"
                                        HeaderStyle-Width="10px" 
                                        DataField="approval_type" 
                                        HeaderText="MOC Status"
                                        DataType="System.String" 
                                        >
            <HeaderStyle Width="4px" />
			</telerik:GridBoundColumn>


            <telerik:GridBoundColumn    AllowSorting="false" 
                                        ReadOnly="true" 
                                        AllowFiltering="false"
                                        HeaderStyle-Width="10px" 
                                        DataField="initiatorname" 
                                        HeaderText="Initiator"
                                        DataType="System.String" 
                                        >
            <HeaderStyle Width="4px" />
			</telerik:GridBoundColumn>


            <telerik:GridBoundColumn    AllowSorting="false" 
                                        ReadOnly="true" 
                                        AllowFiltering="false"
                                        HeaderStyle-Width="10px" 
                                        DataField="Comments" 
                                        HeaderText="Comments"
                                        DataType="System.String" 
                                        >
            <HeaderStyle Width="25px" />
			</telerik:GridBoundColumn>



  
            </Columns>


				<EditFormSettings 
								UserControlName="ReviewerUpdates.ascx" 		    
								EditFormType="WebUserControl">
					<EditColumn UniqueName="EditCommandColumn1">
					</EditColumn>
				</EditFormSettings>

</MasterTableView>
</telerik:RadGrid>
<%--END of telerik Grid RadGridMOCListing --%>



        <br />
        <asp:Label ID="_lblInterestHeader" runat="server" Text="<%$ RIResources:Shared,MOCs You May Be Interested In %>" Font-Bold="True" Font-Size="Large" Visible="false"></asp:Label>

<br /> <div style="width: 100%; " class="HeaderNewStuff"> *** GRID MOCDraftListing **** </div> <br />



        <span id="_spDrafts" runat="server">
			<br /><asp:Label ID="_lbDrafts" runat="server" Text="0" BackColor="#ffffff" Font-Size="12pt" Width="100%" Font-Italic="True" ></asp:Label>
	        <br />
			<asp:GridView ID="_gvMOCDraftListing" runat="server" CssClass="Border"
                BorderColor="Black"
				BorderWidth="2" AutoGenerateColumns="False"
                DataKeyNames="MOCNUMBER" EnableViewState="false"
				AllowSorting="true" Width="100%" Style="table-layout: fixed" Font-Bold="True"  >
				<headerstyle backcolor="Black"   forecolor="White"/>
                <rowstyle backcolor="White"   forecolor="Black"/>
			    <alternatingrowstyle backcolor="WhiteSmoke"  forecolor="Black"/>

                <Columns>
				    <asp:HyperLinkField HeaderText="MOC" DataTextField="MOCNUMBER" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center"
						DataNavigateUrlFields="MOCNUMBER" DataNavigateUrlFormatString="~/MOC/EnterMOC.aspx?MOCNumber={0}"
						Target="_self" />
                    <asp:TemplateField ItemStyle-Width="5%" HeaderText="<%$RIResources:Shared,Date %>" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="_lblDate" runat="server" Font-Bold="false" Text='<%# Eval("EventDate", "{0:d}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
					<asp:TemplateField ItemStyle-Width="25%" HeaderText="<%$RIResources:Shared,Title %>">
                        <ItemTemplate>
                            <asp:Label ID="_lblTitle" runat="server" Font-Bold="false" Text='<%# Bind("TITLE") %>'></asp:Label><br />
                            <asp:Label ID="_lbDescription" runat="server" Font-Bold="false" Text='<%# Bind("DESCRIPTION") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="<%$RIResources:MOC,Initiator/Owner %>">
                        <ItemTemplate>
                            <asp:Label ID="_lblInitiator" runat="server" Font-Bold="false" Text='<%# Bind("initiatorname") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    </Columns>
			</asp:GridView>
            <br />
			<br />
        </span>

<br /> <div style="width: 100%; " class="HeaderNewStuff"> *** GRID MOCOnHoldListing **** </div> <br />


        <span id="_spOnHold" runat="server">
            <br />
                <asp:Label ID="_lbOnHold" runat="server" Text="0" BackColor="#ffffff" Font-Size="10pt" Width="100%" Font-Italic="True" ></asp:Label>
	        <br />
			<asp:GridView ID="_gvMOCOnHoldListing" runat="server" CssClass="Border" BorderColor="Black"
				BorderWidth="2" AutoGenerateColumns="False" DataKeyNames="MOCNUMBER" EnableViewState="false"
				AllowSorting="true" Width="100%" HeaderStyle-BorderColor="Black" HeaderStyle-Font-Bold="True" >
				<Columns>
				    <asp:HyperLinkField HeaderText="MOC" DataTextField="MOCNUMBER" ItemStyle-Width="6%" ItemStyle-HorizontalAlign="Center"
						DataNavigateUrlFields="MOCNUMBER" DataNavigateUrlFormatString="~/MOC/EnterMOC.aspx?MOCNumber={0}"
						Target="_self" />
                    <asp:TemplateField ItemStyle-Width="6%" HeaderText="<%$RIResources:Shared,Date %>" HeaderStyle-horizontalalign="Left" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="_lblDate" runat="server" Font-Bold="false" Text='<%# Eval("EventDate", "{0:d}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
					<asp:TemplateField ItemStyle-Width="73%" HeaderText="<%$RIResources:Shared,Title %>" HeaderStyle-horizontalalign="Left" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="_lblTitle" runat="server" Font-Bold="false" Text='<%# Bind("TITLE") %>'></asp:Label><br />
                            <asp:Label ID="_lbDescription" runat="server" Font-Bold="false" Text='<%# Bind("DESCRIPTION") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="15%" HeaderText="<%$RIResources:MOC,Initiator/Owner %>" HeaderStyle-horizontalalign="Left" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="_lblInitiator" runat="server" Font-Bold="false" Text='<%# Bind("initiatorname") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    </Columns>
			</asp:GridView>
            <br />
			<br />
        </span>

<br /> <div style="width: 100%; " class="HeaderNewStuff"> *** GRID PendingOwner **** </div> <br />

        <span id="_spPendingOwner" runat="server">

         <div class="MyMOCsLabel">			        
        <asp:Label ID="_lbPendingOwner" runat="server" Text="0" CssClass="MyMOCsLabel" Font-Size="12pt" Width="100%" Font-Italic="True" BorderColor="Black" ForeColor="White"  ></asp:Label>
        </div>


 


			<asp:GridView ID="_gvPendingOwner" 
                runat="server" CssClass="BorderSecondary" BorderColor="Black"
				BorderWidth="2" AutoGenerateColumns="False" DataKeyNames="MOCNUMBER" EnableViewState="false"
				AllowSorting="true" Width="100%"   
                HeaderStyle-BorderColor="Black" HeaderStyle-BorderWidth="1px" HeaderStyle-Font-Bold="True"
                GridLines="Both"
			    Style="table-layout: fixed" Font-Bold="True">  
				<headerstyle backcolor="#cccccc"   forecolor="Black"/>

                <alternatingrowstyle backcolor="WhiteSmoke"  
                forecolor="Black"
                font-italic="false"/>

                <rowstyle backcolor="White"  
                forecolor="Black"
                font-italic="false" />


                <Columns>
				    <asp:HyperLinkField HeaderText="MOC" DataTextField="MOCNUMBER" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center"
						DataNavigateUrlFields="MOCNUMBER" DataNavigateUrlFormatString="~/MOC/EnterMOC.aspx?MOCNumber={0}"
						Target="_self" />
                    <asp:TemplateField ItemStyle-Width="5%" HeaderText="<%$RIResources:Shared,Date %>" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="_lblDate" runat="server" Font-Bold="false" Text='<%# Eval("EventDate", "{0:d}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
					<asp:TemplateField ItemStyle-Width="25%" HeaderText="<%$RIResources:Shared,Title %>">
                        <ItemTemplate>
                            <asp:Label ID="_lblTitle" runat="server" Font-Bold="false" Text='<%# Bind("TITLE") %>'></asp:Label><br />
                            <asp:Label ID="_lbDescription" runat="server" Font-Bold="false" Text='<%# Bind("DESCRIPTION") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="<%$RIResources:MOC,Initiator/Owner %>">
                        <ItemTemplate>
                            <asp:Label ID="_lblInitiator" runat="server" Font-Bold="false" Text='<%# Bind("initiatorname") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    </Columns>
			</asp:GridView>

        </span>
</ContentTemplate>
</Asp:UpdatePanel>

<%--	<div class="labellogin">
                    <IP:IPLogin ID="_login" runat="server" />
                </div>--%>
	<%--<ajaxToolkit:CascadingDropDown ID="_cddlCoordinator" runat="server" Category="Leader"
		LoadingText="[Loading Coordinator...]" PromptText="   " ServiceMethod="GetPerson"
		ServicePath="~/CascadingLists.asmx" TargetControlID="_ddlPlanner" ParentControlID="ctl00__cphMain__siteLocation__ddlFacility">
	</ajaxToolkit:CascadingDropDown>--%>

<br />


<asp:Panel ID="_pnlStartofNew" runat="server" Width="100%" HorizontalAlign="Left" Visible="true"> 
<div style="width: 100%; " class="HeaderNewStuff">
*** START OF NEW STUFF **** 
</div>

</asp:Panel>


<telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel">
</telerik:RadAjaxLoadingPanel>

</asp:Content>

