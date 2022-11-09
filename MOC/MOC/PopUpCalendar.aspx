<%@ Page Title="" Language="VB" MasterPageFile="~/RI.master" AutoEventWireup="false" CodeFile="PopUpCalendar.aspx.vb" Inherits="MOC_PopUpCalendar" %>


 <%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>



<asp:Content ID="Content1" ContentPlaceHolderID="_cphMain" Runat="Server">



           <telerik:RadDatePicker RenderMode="Lightweight" runat="server" 
ID="RadDatePicker1" AutoPostBack="true">
        </telerik:RadDatePicker>



</asp:Content>

