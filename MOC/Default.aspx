<%@ Page Language="VB" MasterPageFile="~/RI.master" 
    EnableTheming="true" AutoEventWireup="false"
    CodeFile="Default.aspx.vb" Trace="false" 
    Inherits="_Default" Title="Graphic Packaging: Reliability Reporting" %>
<%@ MasterType VirtualPath="~/RI.master" %>
<asp:Content ID="_cphMain" ContentPlaceHolderID="_cphMain" runat="Server">

    <style>

h1 {
  color: red;
  margin-left: 40px;
}
</style>
     <h1>WARNING&nbsp;&nbsp;&nbsp;  WARNING&nbsp;&nbsp;&nbsp;  WARNING&nbsp;&nbsp;&nbsp;  WARNING&nbsp;&nbsp;&nbsp;</h1>   <br />
 <h2>You are about to enter the RI TEST system!  Do you want to continue?
      &nbsp;
     <asp:Button ID="ButtonYes" runat="server" Text="YES" />
     
    
     


 </h2>

</asp:Content>
