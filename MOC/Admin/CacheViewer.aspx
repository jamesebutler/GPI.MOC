<%@ Page Title="" Language="VB" MasterPageFile="~/RI.master" AutoEventWireup="false" 
CodeFile="CacheViewer.aspx.vb" Inherits="Admin_CacheViewer" %>

 <%@ MasterType VirtualPath="~/RI.master" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="_cphMain" Runat="Server">

  <script type="text/javascript" language="javascript">
<!--
   function OpenOrCloseSpan(spanTag)
   {
      var st = document.getElementById( spanTag );
      if ( st.style.display == 'none' )
         st.style.display = '';
      else
         st.style.display = 'none';
   }
// -->
	</script>

	<p>
		<b>The contents of the ASP.NET application cache are:</b></p>
	<asp:PlaceHolder ID="phTable" runat="server" />
	<br />
	 <asp:Button ID="_btnRefresh" Text="Refresh" runat="server" />&nbsp;&nbsp;<asp:Button
		ID="_btnDeleteAll" Text="Delete All Cache Items" runat="server" />

</asp:Content>

