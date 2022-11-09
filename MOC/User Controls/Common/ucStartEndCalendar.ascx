<%@ Control EnableTheming="false" Language="VB" AutoEventWireup="false" CodeFile="ucStartEndCalendar.ascx.vb"
    Inherits="ucStartEndCalendar" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


<telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">


<script type="text/javascript">

            //<![CDATA[

            function OnStartDateSelected(sender, eventArgs) {
                var date1 = sender.get_selectedDate();
                var date2 = sender.get_selectedDate();
                date1.setDate(date1.getDate() + 31);
                var datepicker = $find("<%=_RadDatePickerEnd.ClientID %>");
                //datepicker.set_maxDate(date1);
                datepicker.set_minDate(date2);
                document.getElementById('<%=_txtStartDate.ClientID %>').value = date2.format('MM/dd/yyyy');
                document.getElementById('<%=_hdfStartDateValue.ClientID %>').value = date2.format('MM/dd/yyyy');

            }

            function OnEndDateSelected(sender, eventArgs) {
                var date1 = sender.get_selectedDate();
                document.getElementById('<%=_txtEndDate.ClientID %>').value = date1.format('MM/dd/yyyy');
                document.getElementById('<%=_hdfEndDateValue.ClientID %>').value = date1.format('MM/dd/yyyy');

            }

            //]]>
        </script>


<script type="text/javascript" >

        function clearDatesSamplePageLevel() {
            document.getElementById('<%=_txtEndDate.ClientID %>').value = "";
            document.getElementById('<%=_txtEndDate.ClientID %>').value = "";

        }

    </script>


 <script lang="javascript" type="text/javascript" >

        //$(function datetime() {
        $(document).ready(function() {
        var dateFormat = "mm/dd/yy",

         from = $('#<%=_txtStartDate.ClientID %>').datepicker
         ({

           gotoCurrent: true,
          changeMonth: true,
          changeYear: true,
          showAnim: "slide",
          numberOfMonths: 1
         })

             .on("change", function () {
                to.datepicker("option", "minDate", getDate(this));
                document.getElementById('<%=_hdfStartDateValue.ClientID %>').value = document.getElementById('<%= _txtStartDate.ClientID %>').value;

             }),


         to = $('#<%=_txtEndDate.ClientID %>').datepicker
         ({

         gotoCurrent: true,
         changeMonth: true,
         changeYear: true,
         showAnim: "slide",
         numberOfMonths: 1
         })

          .on( "change", function() {
              from.datepicker("option", "maxDate", getDate(this));
              <%--document.getElementById('<%=_hdfEndDateValue.ClientID %>').value = document.getElementById('<%= _txtEndDate.ClientID %>').value;   --%>
          });


            function getDate(element) {
      var date;
      try {
        date = $.datepicker.parseDate( dateFormat, element.value );
      } catch( error ) {
        date = null;
      }

      return date;
                }

           });

       </script>

   </telerik:RadScriptBlock>



<telerik:RadFormDecorator RenderMode="Lightweight" runat="server" DecorationZoneID="containerDiv"></telerik:RadFormDecorator>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>

<%-- ID="_RadDatePickerStart"--%>

<table  cellpadding="0" cellspacing="0"   style="text-align: left; border: 4px dotted green ;" >
    <tr>
        <td>
            <asp:Label ID="_lblStartDate" Text='<%$RIResources:Shared,Start %>' runat="server"></asp:Label>
            &nbsp;<asp:TextBox Visible="false"
                ID="_txtStartDate"
                runat="server"
                ClientIDMode="Static"
                CssClass="DateRange"></asp:TextBox>
            
<telerik:RadDatePicker
                RenderMode="Lightweight"
                 visible="false"
                   DateInput-ReadOnly="true"
                  ID="_RadDatePickerStart"
                 Skin="Telerik"
                 ClientEvents-OnDateSelected="OnStartDateSelected"
                 runat="server"
                  ClientIDMode="Static"
                    DateInput-DateFormat="MM/dd/yyyy"
                 DateInput-Label="">


            </telerik:RadDatePicker>




            <asp:HiddenField ID="_hdfStartDateValue" runat="server" ClientIDMode="Static" />



        </td>
        <td valign="baseline">
           <%-- <asp:ImageButton runat="server" ID="_imgStartCal" Visible="false" ImageUrl="~/Images/calendar.jpg"
                ImageAlign="Bottom" />--%>
        </td>
        <td nowrap=nowrap>
            &nbsp;
            <asp:Label ID="_lblStartTime" runat="server" Text="<%$RIResources:Global,Time %>"></asp:Label>
            <asp:DropDownList ID="_ddlStartHrs" runat="server">
            </asp:DropDownList>&nbsp;
            <asp:DropDownList ID="_ddlStartMins" runat="server">
            </asp:DropDownList>
        </td>
        <td style="width: 10px">
            &nbsp;</td>
        <td>
            <asp:Label ID="_lblEndDate" Text='<%$RIResources:Shared,End %>' runat="server"></asp:Label>
            &nbsp;<asp:TextBox visible="true" ID="_txtEndDate" EnableViewState="true" runat="server" CssClass="DateRange" ClientIDMode="Static"></asp:TextBox>



            <telerik:RadDatePicker
                RenderMode="Lightweight"
                visible="false"
                DateInput-ReadOnly="true"
                ID="_RadDatePickerEnd"
                ClientEvents-OnDateSelected="OnEndDateSelected"
                Skin="Telerik"
                runat="server"
                ClientIDMode="Static"
                DateInput-Label="">
        </telerik:RadDatePicker>


            <asp:HiddenField ID="_hdfEndDateValue" runat="server" ClientIDMode="Static" />
        </td>
        <td>


        </td>

        <td nowrap=nowrap>
            &nbsp;
            <asp:Label ID="_lblEndTime" runat="server" Text="<%$RIResources:Global,Time %>"></asp:Label>
            <asp:DropDownList ID="_ddlEndHrs" runat="server">
            </asp:DropDownList>&nbsp;
            <asp:DropDownList ID="_ddlEndMins" runat="server">
            </asp:DropDownList>
        </td>

         <td>
</td>
    </tr>

</table>



