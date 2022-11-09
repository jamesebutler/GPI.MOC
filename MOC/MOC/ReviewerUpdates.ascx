<%@ Control Language="VB" 
AutoEventWireup="false" 
CodeFile="ReviewerUpdates.ascx.vb" 
Inherits="MOC_ReviewerUpdates" %>


<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>


<table id="Table2" cellspacing="12" cellpadding="1" width="250%" border="0" rules="none"
    style="border-collapse: collapse; background-color:whitesmoke">
    <tr class="EditFormHeader">
        <td colspan="2">
            <b>Reviewer: <%# DataBinder.Eval(Container, "DataItem.personname") %></b>
        </td>
    </tr>
    <tr>
        
        <td style="vertical-align: top">
            
            <table id="Table3" cellspacing="1" cellpadding="1" width="250" border="0" class="module">
              
              	<tr>
                    <td align="left" >Approved:&nbsp;
                        <asp:DropDownList ID="ddlApproval" runat="server" >
                        </asp:DropDownList>
                    </td>
                    <td align="left"></td>
                </tr>




				<tr>
					<td><b>Comments:</b>
					</td>
				</tr>
				<tr>
					<td>
                        <asp:TextBox ID="TextBox1" Text='<%# DataBinder.Eval(Container, "DataItem.comments") %>' runat="server" TextMode="MultiLine"
                            Rows="5" Columns="80" TabIndex="5">
                        </asp:TextBox>
                    </td>
                </tr>

                
           
                <tr>
                <td align="right" colspan="2">
                    <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" Visible='<%# Not (TypeOf DataItem Is Telerik.Web.UI.GridInsertionObject) %>'></asp:Button>
                    <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                        Visible='<%# (TypeOf DataItem Is Telerik.Web.UI.GridInsertionObject) %>'></asp:Button>
                    &nbsp;
                    <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                        CommandName="Cancel"></asp:Button>
                </td>
            </tr>

        </table>
</td>
	</tr>
	<tr>
		<td colspan="2"></td>
	</tr>
	<tr>
		<td></td>
		<td></td>
	</tr>


<tr><td colspan="2">&nbsp;</td></tr>
</table>