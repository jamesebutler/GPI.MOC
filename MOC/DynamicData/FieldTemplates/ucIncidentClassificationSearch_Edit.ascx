<%@ Control Language="VB" CodeFile="ucIncidentClassificationSearch_Edit.ascx.vb" Inherits="RI_User_Controls_ucIncidentClassificationSearch_EditField" %>

<asp:TextBox ID="TextBox1" runat="server" Text='<%# FieldValueEditString %>' CssClass="droplist"></asp:TextBox>

<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" CssClass="droplist" ControlToValidate="TextBox1" Display="Dynamic" Enabled="false" />
<asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" CssClass="droplist" ControlToValidate="TextBox1" Display="Dynamic" Enabled="false" />
<asp:DynamicValidator runat="server" ID="DynamicValidator1" CssClass="droplist" ControlToValidate="TextBox1" Display="Dynamic" />