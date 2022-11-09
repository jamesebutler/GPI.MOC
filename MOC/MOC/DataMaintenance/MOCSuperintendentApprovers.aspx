<%@ Page Title="" Language="VB" MasterPageFile="~/RI.master" 
    AutoEventWireup="false" 
    CodeFile="MOCSuperintendentApprovers.aspx.vb" 
    Trace="false" EnableViewState="true"
    Inherits="MOC_DataMaintenance_MOCSuperintendentApprovers" 
    EnableEventValidation="false" %>


<%@ MasterType VirtualPath="~/RI.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>



<asp:Content ID="_contentMain" ContentPlaceHolderID="_cphMain" Runat="Server">


 <asp:UpdatePanel id="_udpLocation" runat="server" updatemode="Conditional">
    <ContentTemplate>

    <ajaxToolkit:CascadingDropDown id="_cddlFacility1" runat="server" category="SiteID"
        loadingtext="" prompttext="    " servicemethod="GetFacilityList" servicepath="~/CascadingLists.asmx"
        targetcontrolid="_ddlFacility" usecontextkey="true">
    </ajaxToolkit:CascadingDropDown>
    <ajaxToolkit:CascadingDropDown id="_cddlFacility" runat="server" category="SiteID"
        loadingtext="" prompttext="    " servicemethod="GetFacilityList" servicepath="~/CascadingLists.asmx"
        targetcontrolid="_ddlFacility2" usecontextkey="true">
    </ajaxToolkit:CascadingDropDown>

    <ajaxToolkit:CascadingDropDown id="_cddlPeople" runat="server" category="Person"
        loadingtext="" prompttext="    " servicemethod="GetSuperintendentPerson" servicepath="~/CascadingLists.asmx"
        targetcontrolid="_ddlPeople" parentcontrolid="_ddlFacility2">
    </ajaxToolkit:CascadingDropDown>

    <ajaxToolkit:CascadingDropDown id="_cddlBusUNit" runat="server" category="BusinessUnit"
        loadingtext="" prompttext="    " servicemethod="GetBusinessUnit" servicepath="~/CascadingLists.asmx"
        targetcontrolid="_ddlBusinessUnit" parentcontrolid="_ddlFacility">
    </ajaxToolkit:CascadingDropDown>
     <ajaxToolkit:CascadingDropDown id="_cddlArea" runat="server" category="Area"
        loadingtext="" prompttext="    " servicemethod="GetArea" servicepath="~/CascadingLists.asmx"
        targetcontrolid="_ddlArea" parentcontrolid="_ddlBusinessUnit">
    </ajaxToolkit:CascadingDropDown>
    <ajaxToolkit:CascadingDropDown id="_cddlLineBreak" runat="server" category="Line"
        loadingtext="" prompttext="    " servicemethod="GetLine" servicepath="~/CascadingLists.asmx"
        targetcontrolid="_ddlLineBreak" parentcontrolid="_ddlArea">
    </ajaxToolkit:CascadingDropDown>
   

<br />
 
    <asp:Label runat="server" ID="_lblMainHeading" Font-Bold="true" Font-Size="Large"></asp:Label><br />
<%--    <asp:Label runat="server" ID="_lblHeading"></asp:Label>--%>
    <asp:DropDownList ID="_ddlFacility" runat="server" AutoPostBack="true" Visible="false"></asp:DropDownList> 
    
    <asp:RadioButtonList ID="_rblMaintType" Visible="false" runat="server" RepeatDirection="Horizontal" AutoPostBack="true">
        <asp:ListItem Value="Business Unit" Visible="true" Text="<%$RIResources:Shared,BusinessUnit %>" Selected="true"></asp:ListItem>
        <asp:ListItem Value="Classification" Visible="true" Text="<%$RIResources:Shared,Classification %>"></asp:ListItem>
        <asp:ListItem Value="Category" Visible="true" Text="<%$RIResources:Shared,Category %>"></asp:ListItem>
     </asp:RadioButtonList>   
      <br />  
    
        
        
<%--Start of Approvers--%>
        <asp:table runat="server" ID="_tblApprover" cellpadding="2" cellspacing="0" style="width: 100%"
        BorderColor="green" BorderStyle="dotted" BorderWidth="0"   >
        <asp:TableRow cssclass="Border" BackColor="whitesmoke">
            <asp:TableCell verticalalign="top">
               <asp:Label ID="_lbl" runat="server" Text="<%$RIResources:MOC,AvailApprover %>"></asp:Label><br />
                <asp:DropDownList ID="_ddlFacility2" runat="server" Width="35%" > </asp:DropDownList>
                &nbsp;<asp:DropDownList ID="_ddlPeople" runat="server"></asp:DropDownList>
                <asp:HiddenField ID="_hfPeople" runat="server" />
            </asp:TableCell>

            <asp:TableCell verticalalign="top"><asp:Label ID="Label1" runat="server" Text="<%$RIResources:MOC,Approval Level %>"></asp:Label><br />
                <asp:DropDownList runat="server" ID="_ddlApproval">
<%--                    <asp:ListItem value="S4" Text="Mill Wide Superintendent"></asp:ListItem>
                    <asp:ListItem value="S3" Text="Paper Superintendent"></asp:ListItem>
                    <asp:ListItem value="S1" Text="Power Superintendent"></asp:ListItem>
                    <asp:ListItem value="S2" Text="Pulp Superintendent"></asp:ListItem>--%>
                </asp:DropDownList>
            </asp:TableCell> 

             <asp:TableCell Width="8%">
                <asp:Button ID="_btnAdd" runat="server" Text="<%$RIResources:Shared,Add %>"/>
            </asp:TableCell>


            <%--All below is hidden--%>

            <asp:TableCell ID="_tcBusinessUnit" verticalalign="top" >
                <asp:Label Visible="false" ID="_lblBusinessUNit" runat="server" Text="<%$RIResources:Shared,BusinessUnit %>"></asp:Label><br />
                <asp:DropDownList Visible="FALSE" ID="_ddlBusinessUnit" AutoPostBack="false" runat="server" />
            </asp:TableCell>
            <asp:TableCell ID="_tcArea" verticalalign="top" >
                <asp:Label Visible="false" ID="_lblArea" runat="server" Text="<%$RIResources:Shared,Area %>"></asp:Label><br />
                <asp:DropDownList visible="false" ID="_ddlArea"  AutoPostBack="false" runat="server" />
            </asp:TableCell>
            <asp:TableCell ID="_tcLine" verticalalign="top" >
                <asp:Label Visible="false" ID="_lblLine" runat="server" Text="<%$RIResources:Shared,Line %>"></asp:Label><br />
                <asp:DropDownList visible="false" ID="_ddlLineBreak" AutoPostBack="false" runat="server" />
            </asp:TableCell>
            <asp:TableCell Width="8%" verticalalign="top">
                <asp:Label ID="_lblRequired" visible="false" runat="server" Text="<%$RIResources:Shared,Required %>"></asp:Label><br />
                <asp:CheckBox ID="_cbRequired" Checked="true" Visible="false" runat="server"/>
            </asp:TableCell>
            <%--End of hidden--%>

        </asp:TableRow>        
    </asp:table>

 <br /> 



    
        <asp:GridView ID="_gvClass" runat="server" AutoGenerateColumns="False" Width="100%" align="Center"
         DataKeyNames="classnotify_seqid" Visible="false" EmptyDataText="NO DEFAULT APPROVERS FOR CLASSIFICATIONS" >
                    <headerstyle />
                    <rowstyle backcolor="White"   forecolor="Black"/>
                    <alternatingrowstyle backcolor="WhiteSmoke"  forecolor="Black"/>        
        <Columns>
        <asp:BoundField DataField="MocClassification" HeaderText="<%$RIResources:Shared,Classification %>" />
        <asp:BoundField DataField="NotifyType" HeaderText="<%$RIResources:MOC,ApprovalLevel %>"/>
         <asp:BoundField DataField="Fullname" HeaderText="<%$RIResources:MOC,RolePerson %>" />
        <asp:TemplateField HeaderText="<%$RIResources:Shared,Required %>" >
            <ItemTemplate>
                <asp:CheckBox runat="Server" Enabled="false" checked='<%# Eval("required").ToString().Equals("Y") %>'/>
            </itemtemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:LinkButton ID="_btnApproverDelete" CommandName="Delete" runat="server" cssclass="button"
                    Text="<%$RIResources:Global,Delete %>" />
                <ajaxToolkit:ConfirmButtonExtender ID="_cbeDelete" runat="server" confirmtext="<%$RIResources:Shared,ConfirmDelete %>"
                    TargetControlID="_btnApproverDelete">
                </ajaxToolkit:ConfirmButtonExtender>
            </ItemTemplate>
        </asp:TemplateField>
        </Columns> 
    </asp:GridView>
    
    



        <br />
        
        
        
        <%--REVIEWERS--%>
        
        <asp:Panel ID="_pnlBUA" runat="server">
    <asp:Table ID="_tblL1Header" runat="Server"  >            
    <asp:TableHeaderRow CssClass="TableHeaderRow">
        <asp:TableCell HorizontalAlign="left">       
            <asp:Label ID="_lblL1Header" Text="Reviewers"  runat="server" EnableViewState="false"  />
        </asp:TableCell></asp:TableHeaderRow></asp:Table><%--<ajaxToolkit:CollapsiblePanelExtender id="_cpeComment" runat="Server" targetcontrolid="_pnlL1Approver"
            collapsed="False" CollapseControlID="_lblL1Header" ExpandControlID="_lblL1Header"
            SuppressPostBack="True" TextLabelID="_lblL1Header" CollapsedText="L1 Approvers>"
            expandedtext="L1 Approvers" ScrollContents="false" />--%><asp:Panel ID="_pnlL1Approver" runat="server" Width="100%" HorizontalAlign="Left">
        <asp:Table ID="_tblL1Approver" runat="Server">            
            <asp:TableRow >
                <asp:TableCell HorizontalAlign="left">       
                <asp:GridView Width="100%" CssClass="Border" ID="_gvL1" runat="server" 
                    AutoGenerateColumns="False" ShowFooter="False" DataKeyNames="notify_seqid" EmptyDataText="No Superintendent Reviewers" ShowHeader="true">
                <EmptyDataRowStyle Font-Bold="true" />
                    <headerstyle backcolor="Black"   forecolor="White"/>
                    <rowstyle backcolor="White"   forecolor="Black"/>

                    <alternatingrowstyle backcolor="WhiteSmoke"  forecolor="Black"/>
            <Columns>
                <asp:BoundField HeaderStyle-Width="25%" DataField="FullName" HeaderText="Reviewer"      />
                <asp:BoundField HeaderStyle-Width="25%" DataField="notifytypeName" HeaderText="Superintendent Type"  />
                <asp:TemplateField HeaderStyle-Width="25%"  HeaderText="<%$RIResources:Shared,Required %>" >
                    <ItemTemplate>
                        <asp:CheckBox ID="_cbL1Required" runat="Server" Enabled="false" checked='<%# Eval("required").ToString().Equals("Y") %>'/>
                    </itemtemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="_btnApproverDelete" CommandName="Delete" runat="server" CssClass="Button"
                            Text="<%$RIResources:Global,Delete %>" />
                        <ajaxToolkit:ConfirmButtonExtender ID="_cbeDelete" runat="server" confirmtext="<%$RIResources:Shared,ConfirmDelete %>"
                            TargetControlID="_btnApproverDelete">
                        </ajaxToolkit:ConfirmButtonExtender>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
                </asp:GridView>            
                </asp:TableCell></asp:TableRow></asp:Table>


            </asp:Panel>
                    
                    
                    
    </asp:Panel>
 
    
    </ContentTemplate>
    </asp:UpdatePanel>








</asp:Content>

