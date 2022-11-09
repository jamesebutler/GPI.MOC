<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucIncidentClassificationEnter.ascx.vb" ClientIDMode="Static"
    Inherits="ucIncidentClassificationEnter" %>


<Asp:UpdatePanel ID="_udpIncidentType" runat="server" >
    <ContentTemplate>

            <asp:Table ID="Table3" runat="server" CellPadding="0" CellSpacing="0" BackColor="white"
            EnableViewState="true" style="overflow:hidden; width:100%; table-layout:fixed" Width="100%">                       
            <asp:TableRow CssClass="Border" VerticalAlign="top" Width = "100%">

<asp:TableCell Width="25%"  runat="server">
            <asp:Label ID="_lblEventCategory" runat="server" EnableViewState="false" Text="Event Category" />
            <br />
            <asp:DropDownList ID="_ddlCategory" runat="server" onchange="self.focus()" Width="50%" />
            </asp:TableCell>


            <asp:TableCell Width="25%" runat="server">
            <asp:Label ID="_lblEquipmentProcess" runat="server" EnableViewState="false" Text="Equipment" />
            <br />
            <asp:DropDownList ID="_ddlEquipment" runat="server" onchange="self.focus()" Width="50%" />
            </asp:TableCell>
                
            <asp:TableCell Width="25%"  runat="server">
            <asp:Label ID="_lblComponent" runat="server" EnableViewState="false" Text="Component" />
            <br />
            <asp:DropDownList ID="_ddlComponent" runat="server" Width="50%" />
            </asp:TableCell>

            </asp:TableRow>
            </asp:Table>

             <ajaxToolkit:CascadingDropDown ID="_cddlCategory" runat="server" Category="Causes" 
            LoadingText="..."  PromptText="Select event..." ServiceMethod="GetEventCategoryFirst"
            ServicePath="~/IncidentCausesEnter.asmx" TargetControlID="_ddlCategory" >
            </ajaxToolkit:CascadingDropDown>


            <ajaxToolkit:CascadingDropDown ID="_cddlEquipment" runat="server" Category="Process"
            LoadingText="..." PromptText="Select equipment..." ServiceMethod="GetEquipmentList"
            ServicePath="~/IncidentCausesEnter.asmx" TargetControlID="_ddlEquipment" 
            ParentControlID="_ddlCategory"> 
            </ajaxToolkit:CascadingDropDown>
        
            <ajaxToolkit:CascadingDropDown ID="_cddlComponent" runat="server" Category="Component"
            LoadingText="..." PromptText="Select component..." ServiceMethod="GetCompontentList"
            ServicePath="~/IncidentCausesEnter.asmx" TargetControlID="_ddlComponent" 
            ParentControlID="_ddlCategory" >
            </ajaxToolkit:CascadingDropDown>

    </ContentTemplate>
</Asp:UpdatePanel>
