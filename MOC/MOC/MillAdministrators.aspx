<%@ Page Title="" Language="VB" MasterPageFile="~/RI.master" AutoEventWireup="false" CodeFile="MillAdministrators.aspx.vb" Inherits="MillAdministrators" %>


<%@ MasterType VirtualPath="~/RI.master" %>




<asp:Content ID="Content1" ContentPlaceHolderID="_cphMain" Runat="Server">

<style>
       .MaintenanceGridHeader {
            background-color: black;
            color: white;
            line-height: 30px;
        }

    </style>

<div align="center" style="color: #000000;" class='center' >

<br />
<asp:GridView ID="_gvMillAdministrators" runat="server"  

     RowStyle-BackColor="White"
    AlternatingRowStyle-BackColor="WhiteSmoke"
    AutoGenerateColumns="False"  
    EnableViewState="false"
	BorderWidth="2" Width="65%" 
    HeaderStyle-BorderColor="WHITE" 
    HeaderStyle-Font-Bold="True" 
    HeaderStyle-HorizontalAlign="Left" 
    GridLines="Both" >
				<Columns>
                    <asp:TemplateField ItemStyle-Width="25%"  HeaderStyle-CssClass="MaintenanceGridHeader" HeaderStyle-horizontalalign="Left" HeaderText="Mill Administrator" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="_lblName" runat="server" Font-Bold="false" Text='<%# Bind("fullname") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
					
                    <asp:TemplateField ItemStyle-Width="25%" HeaderStyle-CssClass="MaintenanceGridHeader" HeaderStyle-horizontalalign="Left" HeaderText="Email" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="_lblTitle" runat="server" Font-Bold="false" Text='<%# Bind("email") %>'></asp:Label><br />
                        </ItemTemplate>
                    </asp:TemplateField>
                    </Columns>
			</asp:GridView>

</div>








</asp:Content>

