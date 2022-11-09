<%@ Page Title="" Language="VB" MasterPageFile="~/RI.master" 
    AutoEventWireup="false" CodeFile="Mydefaults.aspx.vb" Inherits="MOC_Mydefaults" %>

<%@ MasterType VirtualPath="~/RI.master" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>


<asp:Content ID="Content1" ContentPlaceHolderID="_cphMain" Runat="Server">


<asp:Table ID="TableCauses" runat="server" Width="100%"  BorderWidth="0" BorderStyle="Dotted" BorderColor="Green" >


<asp:TableHeaderRow id="Table1HeaderRow" SkinID="rowheader" >
<asp:TableCell ColumnSpan="2">
<asp:Label ID="_lblClass1" runat="server" ColumnSpan="1" Text="Themes"
CssClass="TableHeaderRow" EnableViewState="false" />
</asp:TableCell>

</asp:TableHeaderRow>



    
        <asp:TableRow>
        <asp:TableCell Width="75px"><asp:RadioButton ID="rbtnBlue" runat="server" GroupName="Themes" Text="Blue" /></asp:TableCell>
        <asp:TableCell><img src="../App_Themes/RIBlue/Images/BlueTheme.PNG" /></asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
        <asp:TableCell><asp:RadioButton ID="rbtnGold" runat="server" GroupName="Themes" Text="Gold" /></asp:TableCell>
        <asp:TableCell><img src="../App_Themes/RIGold/Images/GoldTheme.PNG" /></asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
        <asp:TableCell><asp:RadioButton ID="rbtnGreen" runat="server" GroupName="Themes" Text="Green" /></asp:TableCell>
        <asp:TableCell><img src="../App_Themes/RIGreen/Images/GreenTheme.PNG" /></asp:TableCell>
        </asp:TableRow>

            <asp:TableRow>
        <asp:TableCell><asp:RadioButton ID="rbtnPurple" runat="server" GroupName="Themes" Text="Purple" /></asp:TableCell>
        <asp:TableCell><img src="../App_Themes/RIPurple/Images/PurpleTheme.PNG" /></asp:TableCell>
        </asp:TableRow>



        <asp:TableRow>
        <asp:TableCell><asp:RadioButton ID="rbtnYellow" runat="server" GroupName="Themes" Text="Yellow" /></asp:TableCell>
        <asp:TableCell><img src="../App_Themes/RIYellow/Images/YellowTheme.PNG" /></asp:TableCell>
        </asp:TableRow>

           <asp:TableRow>
        <asp:TableCell><asp:RadioButton ID="rbtnBlack" runat="server" GroupName="Themes" Text="White" /></asp:TableCell>
        <asp:TableCell><img src="../App_Themes/RIBlack/Images/BlackTheme.PNG" /></asp:TableCell>
        </asp:TableRow>
    
            <asp:TableRow >
        <asp:TableCell  ColumnSpan="2"><asp:Button ID="_btnThemeUpdate"  Text="Update Theme" runat="server" /></asp:TableCell>
 </asp:TableRow>
</asp:Table>


</asp:Content>

