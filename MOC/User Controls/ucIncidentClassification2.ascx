<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucIncidentClassification2.ascx.vb" ClientIDMode="Static"
    Inherits="ucIncidentClassification2" %>


<Asp:UpdatePanel ID="_udpIncidentType" runat="server" >
    <ContentTemplate>

            <asp:Table ID="Table3" runat="server" CellPadding="0" CellSpacing="0" BackColor="white"
            EnableViewState="true" style="overflow:hidden; width:100%; table-layout:fixed" Width="100%">                       
            <asp:TableRow CssClass="Border" VerticalAlign="top" Width = "100%">

            <asp:TableCell Width="25%"  runat="server">
            <asp:Label ID="_lblCause" runat="server" EnableViewState="false" Text="Event Category" />
            <br />
            <asp:DropDownList ID="_ddlType" runat="server" onchange="self.focus()" Width="50%" />
            </asp:TableCell>

                   
 
            <asp:TableCell Width="25%" runat="server">
            <asp:Label ID="_lblEquipmentProcess" runat="server" EnableViewState="false" Text="Equipment" />
            <br />
            <asp:DropDownList ID="_ddlProcess" runat="server" onchange="self.focus()" Width="50%" />
            </asp:TableCell>
                

                
            <asp:TableCell Width="25%"  runat="server">
            <asp:Label ID="_lblComponent" runat="server" EnableViewState="false" Text="Component" />
            <br />
            <asp:DropDownList ID="_ddlComponent" runat="server" Width="50%" />
            </asp:TableCell>


                  <asp:TableCell Width="25%"  runat="server" Visible="false">
                <asp:Label ID="_lblPrevention" runat="server" EnableViewState="false" Text="Prevention" />
                <br />
                <asp:DropDownList ID="_ddlPrevention" runat="server" onchange="self.focus();" Width="50%" />
                </asp:TableCell>
                
            <asp:TableCell Width="1%"  runat="server" Visible="False">
            <asp:Label ID="_lblReason" runat="server" EnableViewState="false" Text="Cause " />
            <font  size="3o"  color="red"><strong>(GOING AWAY)</strong></font>
            <br />
            <asp:DropDownList ID="_ddlCause" runat="server" onchange="self.focus();" Width="33%" />
            </asp:TableCell>

            </asp:TableRow>
            </asp:Table>






        <%--<%$RIResources:Shared,SelectProcess %>--%>
    
       
        <ajaxToolkit:CascadingDropDown ID="_cddlProcess" runat="server" Category="Process"
            LoadingText="..." PromptText="Select ..." ServiceMethod="GetProcessListFirst"
            ServicePath="~/IncidentCauses.asmx" TargetControlID="_ddlProcess" > 
        </ajaxToolkit:CascadingDropDown>


      
        <ajaxToolkit:CascadingDropDown ID="_cddlTypes" runat="server" Category="Causes" 
            LoadingText="..."  PromptText="<%$RIResources:Shared,SelectType %>" ServiceMethod="GetProcessTypeList"
            ServicePath="~/IncidentCauses.asmx" TargetControlID="_ddlType"   
            ParentControlID="_ddlProcess">
        </ajaxToolkit:CascadingDropDown>

        
        <ajaxToolkit:CascadingDropDown ID="_cddlComponent" runat="server" Category="Component"
            LoadingText="..." PromptText="<%$RIResources:Shared,SelectComponent %>" ServiceMethod="GetComponentList"
            ServicePath="~/IncidentCauses.asmx" TargetControlID="_ddlComponent" 
            ParentControlID="_ddlType" >
        </ajaxToolkit:CascadingDropDown>

     
        <ajaxToolkit:CascadingDropDown ID="_ccdlReasons" runat="server" Category="Reason"
            LoadingText="..." PromptText="<%$RIResources:Shared,SelectCause %>" ServiceMethod="GetReasonList"
            ServicePath="~/IncidentCauses.asmx" TargetControlID="_ddlCause" 
            ParentControlID="_ddlType">
        </ajaxToolkit:CascadingDropDown>



        <ajaxToolkit:CascadingDropDown ID="_ccdlPrevention" runat="server" Category="Prevention"
            LoadingText="[Loading prevention...]" PromptText="<%$RIResources:Shared,SelectPrevention %>" ServiceMethod="GetPrevention" 
            ServicePath="~/IncidentCauses.asmx" TargetControlID="_ddlPrevention">
        </ajaxToolkit:CascadingDropDown>

                



    </ContentTemplate>
</Asp:UpdatePanel>
