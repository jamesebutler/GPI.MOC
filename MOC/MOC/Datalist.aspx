<%@ Page Title="" Language="VB" MasterPageFile="~/RI.master" 
    AutoEventWireup="false" CodeFile="Datalist.aspx.vb" 
    Inherits="MOC_Datalist" %>

<%@ MasterType VirtualPath="~/RI.master" %>


<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="IP" Namespace="AdvancedTextBox" Assembly="AdvancedTextBox" %>



<asp:Content ID="Content1" ContentPlaceHolderID="_cphMain" Runat="Server">


    <asp:DataList ID="_dlSystem" runat="server" Width="100%" CssClass="Border" 
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

</asp:Label><asp:Label ID="_lblSystemSeq" Visible="false" runat="server" Text='<%# Bind("mocsystem_seq_id") %>'></asp:Label><asp:HiddenField ID="_hdftaskitem" Visible="false" runat="server"></asp:HiddenField>

<asp:CheckBoxList ID="_cblSystem" runat="server" 
RepeatDirection="Horizontal" >
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
</asp:TableCell></asp:TableRow></asp:Table></ItemTemplate></asp:DataList>


</asp:Content>

