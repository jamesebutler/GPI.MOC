<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucIncidentClassificationSearch.ascx.vb" ClientIDMode="Static"
    Inherits="ucIncidentClassificationSearch" %>


<Asp:UpdatePanel ID="_udpIncidentType" runat="server" >
    <ContentTemplate>

            <asp:Table ID="TableSearch" runat="server" CellPadding="0" CellSpacing="0" BackColor="white"
            EnableViewState="true" style="overflow:hidden; width:100%; table-layout:fixed" Width="100%">                       
            <asp:TableRow CssClass="Border" VerticalAlign="top" Width = "100%">

<asp:TableCell Width="25%"  runat="server">
            <asp:Label ID="_lblEventCategory" runat="server" EnableViewState="false" Text="Event Category" />
            <br />
            <asp:DropDownList ID="_ddlCategory" runat="server"  Width="50%" />
            </asp:TableCell>


            <asp:TableCell Width="25%" runat="server">
            <asp:Label ID="_lblEquipmentProcess" runat="server" EnableViewState="false" Text="Equipment" />
            <br />
            <asp:DropDownList ID="_ddlEquipment" runat="server"  Width="50%" />
            </asp:TableCell>
                
            <asp:TableCell Width="25%"  runat="server">
            <asp:Label ID="_lblComponent" runat="server" EnableViewState="false" Text="Component" />
            <br />
            <asp:DropDownList ID="_ddlComponent" runat="server" Width="50%" />
            </asp:TableCell>

            </asp:TableRow>
            </asp:Table>

             <ajaxToolkit:CascadingDropDown ID="_cddlCategory" runat="server" Category="Causes" 
            LoadingText="..."  PromptText=" " ServiceMethod="GetEventCategoryFirst"
            ServicePath="~/IncidentCausesSearch.asmx" TargetControlID="_ddlCategory" >
            </ajaxToolkit:CascadingDropDown>


            <ajaxToolkit:CascadingDropDown ID="_cddlEquipment" runat="server" Category="Process"
            LoadingText="..." PromptText=" " ServiceMethod="GetEquipmentList"
            ServicePath="~/IncidentCausesSearch.asmx" TargetControlID="_ddlEquipment"> 
            </ajaxToolkit:CascadingDropDown>
        
            <ajaxToolkit:CascadingDropDown ID="_cddlComponent" runat="server" Category="Component"
            LoadingText="..." PromptText=" " ServiceMethod="GetCompontentList"
            ServicePath="~/IncidentCausesSearch.asmx" TargetControlID="_ddlComponent">
            </ajaxToolkit:CascadingDropDown>

    </ContentTemplate>
</Asp:UpdatePanel>
