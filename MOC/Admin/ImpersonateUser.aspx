<%@ Page Title="MOC:Impersonate User" Language="VB" 
MasterPageFile="~/RI.master" 
AutoEventWireup="false" 
CodeFile="ImpersonateUser.aspx.vb" 
Inherits="Admin_ImpersonateUser"
 %>

 <%@ MasterType VirtualPath="~/RI.master" %>



<asp:Content ID="Content1" ContentPlaceHolderID="_cphMain" Runat="Server">


<style type="text/css">
	  .example
      {
       font-size:16px;
	   font-family: verdana,'Segoe UI', Tahoma, Geneva, sans-serif;
       text-align:left;
       color:red;
      }
</style>

    <style type="text/css">
	  .dropdownfont
      {
       font-size:16px;
	   font-family: verdana,'Segoe UI', Tahoma, Geneva, sans-serif;
       text-align:left;
        background-color:black;
       color:red;
      }
</style>


<%-- Company Table --%>
<asp:UpdatePanel ID="UpdatePanelcompany" UpdateMode="Always" Visible="true" runat="server">
<ContentTemplate>

        <ajaxToolkit:AutoCompleteExtender
        ID="AutoCompleteExtender2"
        TargetControlID="TextBoxcompanyname"
        FirstRowSelected="true"
        runat="server"
        CompletionListCssClass="dropdownfont"
             
        ServiceMethod="lookupEmployeeByName"
        ServicePath="~/employeeLookup.asmx"
        MinimumPrefixLength="1">
        </ajaxToolkit:AutoCompleteExtender>


<asp:Table CellPadding="0" CellSpacing="6" ID="TableCompanyInfo" BorderColor="Red" BorderStyle="Dotted"
 runat="server" BorderWidth="0px" Width="99%">


<asp:TableRow ID="TableRowcompanyname" runat="server"  >
<asp:TableCell ID="TableCell13"   runat="server" CssClass="employee"   Text="Employee:"> </asp:TableCell>

<asp:TableCell  ID="TableCell14" runat="server"  > 
<asp:TextBox ID="TextBoxcompanyname" Width="680" CssClass="SearchEmployee"  runat="server" ></asp:TextBox>
 &nbsp;&nbsp;&nbsp;<asp:Button ID="_btnImpersonateUser" runat="server" Text="Impersonate User" ></asp:Button>


</asp:TableCell>

</asp:TableRow>

<asp:TableRow>
<asp:TableCell></asp:TableCell>
<asp:TableCell ColumnSpan="2" CssClass="example">(search by LAST name)</asp:TableCell>
</asp:TableRow>

 <asp:TableRow>
<asp:TableCell ColumnSpan="2">

 <div  ID="_divUserProfile" runat="server"></div>
</asp:TableCell>
 </asp:TableRow>
</asp:Table></ContentTemplate>

</asp:UpdatePanel>
<%-- END Company Table --%>



<%--<asp:table BorderColor="green" BorderStyle="Solid" BorderWidth="5" runat="server">

<asp:TableRow>
<asp:TableCell>
	 Acitve Users: <asp:RadioButton ID="RadioButtonActive" Checked="true" runat="server" />
                &nbsp;&nbsp;&nbsp;InAcitve Users: <asp:RadioButton ID="RadioButtonInActive" Checked="false" runat="server" />
</asp:TableCell>
</asp:TableRow>

<asp:TableRow >
<asp:TableCell>
<asp:Label ID="_lblUser" runat="server" Text="User:"></asp:Label>
<asp:DropDownList ID="_ddlUser" runat="server">
</asp:DropDownList>&nbsp;<asp:Button ID="_btnImpersonateUser1" runat="server" Text="Impersonate User" />

</asp:TableCell>
</asp:TableRow>

<asp:TableRow >
<asp:TableCell>

 <div ID="_divUserProfile1" runat="server"></div>
</asp:TableCell>

</asp:TableRow>

</asp:table>--%>




</asp:Content>

