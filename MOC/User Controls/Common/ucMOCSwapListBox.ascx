<%@ Control Language="VB" CodeFile="ucMOCSwapListBox.ascx.vb"
    Inherits="ucMOCSwapList"  %>
<%@ Register Src="~/User Controls/Common/UcMTTResponsible.ascx"
    TagName="Responsible" TagPrefix="IP" %>


<style type="text/css">
	.auto-style1 {
		height: 360px;
	}
</style>


<table cellpadding="2" cellspacing="2" border="0" width="100%" class="auto-style1">
    <tr style="font-size:9pt">
        <td style="width: 30%;font-size:9pt">
            <asp:Label ID="_lblAllFields" Width="100%" runat="server" Font-Size="9pt" Text="<%$RIResources:Shared,Available Approvers/Informed %>"></asp:Label></td>
        <td style="width: 20%">
        </td>
        <td style="width: 30%">
            <asp:Label ID="_lblSelectedFields" Width="100%" runat="server" Font-Size="9pt" Text="<% $RIResources:Shared,Selected Approvers/Informed%>"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 30%;font-size:8pt" valign="top">
            <asp:ListBox ID="_lbAllFields" SelectionMode="Multiple" runat="server" Font-Size="9pt"
                Rows="24" Width="95%"></asp:ListBox></td>
        <td valign="top" align="center" width="20%">
            <asp:Button ID="_btnMoveSelected" runat="server" Text="<%$RIResources:BUTTONTEXT,MoveSelected %>" Width="125" Font-Size="9pt" /><br />
            <asp:Button ID="_btnMoveAll" runat="server" Text="<%$RIResources:BUTTONTEXT,MoveAll %>" Width="125" Visible="false" Font-Size="9pt" /><br />
            <br />
            <asp:Button ID="_btnRemoveSelected" runat="server" Text="<%$RIResources:BUTTONTEXT,RemoveSelected %>" Width="125" Font-Size="9pt" /><br />
            <br />
            <asp:Button ID="_btnRemoveAll" runat="server" Text="<%$RIResources:BUTTONTEXT,RemoveAll %>" Width="125" Font-Size="9pt" /><br />
        </td>


        <td style="width: 30%" valign="top">

            <asp:RadioButton ID="_rbL1Approvers" Checked="true" runat="server" GroupName="SendList" Text="<%$RIResources:Shared,First Level Approvers %>" Font-Size="9pt" Font-Bold="true" /><br />
            <asp:ListBox Width="95%"  ID="_lbApproversL1" CssClass="Border" SelectionMode="Multiple" runat="server" Font-Size="9pt" Rows="10" ></asp:ListBox><br />

            <br />
            <asp:RadioButton Visible="true" ID="_rbInformed" runat="server" GroupName="SendList" Text="Informed:" Font-Size="9pt" Font-Bold="true" /><br />
            <asp:ListBox Width="95%" ID="_lbInformed" SelectionMode="Multiple" runat="server" Font-Size="9pt" Rows="10"></asp:ListBox> <br />

        </td>
    </tr>

</table>

<%--            
            // ***********************************************
            // The folowing has been updated to move the Radiobutton out of sight but it is still visible
            // If the Visible property is set to false the Javascript will not work.
            // The best way is to modifty the JS but now now.
            // ***********************************************
--%>

            <asp:RadioButton Style="position:absolute; left:-100px; top:-150px" Visible="true" ForeColor="#ffffcc" BackColor="#ffffcc"  ID="_rbL2Approvers" runat="server" GroupName="SendList" Text="" Font-Size="9pt" Font-Bold="true" /><br />
            
            <asp:ListBox Style="position:absolute; left:-100px; top:-150px" Visible="true" Width="95%" ID="_lbApproversL2" SelectionMode="Multiple" runat="server" Font-Size="9pt" Rows=1></asp:ListBox><br />

            <asp:RadioButton Style="position:absolute; left:-100px; top:-150px" Visible="true" ForeColor="#ffffcc" BackColor="#ffffcc" ID="_rbL3Approvers" runat="server" GroupName="SendList" Text="<%$RIResources:Shared,Third Level Approvers %>" Font-Size="9pt" Font-Bold="true"/><br />
            
            <asp:ListBox Style="position:absolute; left:-100px; top:-150px" Visible="true" Width="95%" ID="_lbApproversL3" SelectionMode="Multiple" runat="server" Font-Size="9pt" Rows=1></asp:ListBox><br />


<asp:HiddenField ID="_hdfAllFields" runat="server" />
<asp:HiddenField ID="_hdSelectedL1Fields" runat="server" />
<asp:HiddenField ID="_hdSelectedL2Fields" runat="server" />
<asp:HiddenField ID="_hdSelectedL3Fields" runat="server" />
<asp:HiddenField ID="_hdSelectedInformed" runat="server" />
