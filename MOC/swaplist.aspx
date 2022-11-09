<%@ Page Language="VB" AutoEventWireup="false" CodeFile="swaplist.aspx.vb" Inherits="swaplist" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<style type="text/css">



</style>


</head>
<body>
    <form id="form1" runat="server">
        <div>

<table  cellpadding="2" cellspacing="2"  border="5" width="98%" >
    <tr style="font-size:9pt;background-color:aqua;">
        <td style="width: 30%;font-size:9pt">
            <asp:Label ID="_lblAllFields" Width="100%" runat="server" Font-Size="9pt" Text="Approvers/Informed"></asp:Label></td>
        <td style="width: 20%"></td>
        <td style="width: 30%">
            <asp:Label ID="_lblSelectedFields" Width="100%" runat="server" Font-Size="9pt" Text="Approvers/Informed"></asp:Label></td>
    </tr>


    <tr style="background-color:sandybrown;height:20px">
        <td style="width: 30%;font-size:8pt;vertical-align:top">
            <asp:ListBox ID="_lbAllFields" SelectionMode="Multiple" runat="server" Font-Size="9pt"
                Rows="24" Width="95%"></asp:ListBox>

        </td>
        
<td style="background-color:red;vertical-align:top;width:20%;text-align:center;">
            
<asp:Button ID="_btnMoveSelected" runat="server" Text="MoveSelected" Width="125" Font-Size="9pt" /><br />
            <asp:Button ID="_btnMoveAll" runat="server" Text="MoveAll" Width="125" Visible="false" Font-Size="9pt" /><br /><br />
            <asp:Button ID="_btnRemoveSelected" runat="server" Text="RemoveSelected" Width="125" Font-Size="9pt" /><br /><br />
            <asp:Button ID="_btnRemoveAll" runat="server" Text="RemoveAll" Width="125" Font-Size="9pt" /><br />
 </td>


       
 <td style="width: 30%" valign="top">

            <asp:RadioButton ID="_rbL1Approvers" Checked="true" runat="server" GroupName="SendList" Text="First Level Approvers" Font-Size="9pt" Font-Bold="true" /><br />
            <asp:ListBox Width="95%"  ID="_lbApproversL1" CssClass="Border" SelectionMode="Multiple" runat="server" Font-Size="9pt" Rows="10" ></asp:ListBox><br />

            <br />
            <asp:RadioButton Visible="true" ID="_rbInformed" runat="server" GroupName="SendList" Text="Informed:" Font-Size="9pt" Font-Bold="true" /><br />
            <asp:ListBox Width="95%" ID="_lbInformed" SelectionMode="Multiple" runat="server" Font-Size="9pt" Rows="10"></asp:ListBox> <br />
 

        </td>
    </tr>

</table>
	            <asp:RadioButton Style="position:absolute; left:-100px; top:-150px" Visible="false" ForeColor="#ffffcc" BackColor="#ffffcc"  ID="_rbL2Approvers" runat="server" GroupName="SendList" Text="" Font-Size="9pt" Font-Bold="true" /><br />
            
            <asp:ListBox Style="position:absolute; left:-100px; top:-150px" Visible="false" Width="95%" ID="_lbApproversL2" SelectionMode="Multiple" runat="server" Font-Size="9pt" Rows=1></asp:ListBox><br />

            <asp:RadioButton Style="position:absolute; left:-100px; top:-150px" Visible="false" ForeColor="#ffffcc" BackColor="#ffffcc" ID="_rbL3Approvers" runat="server" GroupName="SendList" Text="Third Level Approvers" Font-Size="9pt" Font-Bold="true"/><br />
            
            <asp:ListBox Style="position:absolute; left:-100px; top:-150px" Visible="false" Width="95%" ID="_lbApproversL3" SelectionMode="Multiple" runat="server" Font-Size="9pt" Rows="1"></asp:ListBox><br />


        </div>
    </form>
</body>
</html>
