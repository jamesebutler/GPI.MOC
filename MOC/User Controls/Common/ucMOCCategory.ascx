<script language="JavaScript" type="text/javascript">
function fnCheckParent(val, sender)
{
    var cbl = document.getElementById(sender);
    if (cbl.value = val)
    {
        cbl.checked=true;
       }
}

function fnUnCheckChild(sender, count)
{
    var cbl = document.getElementById(sender);

    if (cbl!=null){
        for (i=0;i<count;i++){
            var ca = document.getElementById(sender + "_"+i);
            if (ca!=null) {ca.checked=false}
        }
     }
}
</script>

<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucMOCCategory.ascx.vb" Inherits="ucMOCCategory" EnableViewState="true" %>


<%--<table  >
    <tr class="Header">
        <th colspan="3" >
        <asp:Label ID="_lblCategory" runat="server" Width="99%" Text="<%$RIResources:Shared,Category %>"
           SkinID="LabelWhite" EnableViewState="false" />
        </th>
    </tr>
</table>--%>


<asp:Table ID="TableCategoryHeader" runat="Server" CssClass="Border"
BorderStyle="Solid" BorderColor="Purple" BorderWidth="0">
            <asp:TableHeaderRow SkinID="rowheader" >
<asp:TableCell HorizontalAlign="left">
<asp:Label ID="_lblCategory" runat="server" EnableViewState="false"  Text="Category" />
  </asp:TableCell>
</asp:TableHeaderRow>
</asp:Table>

<%--testing category--%>

<div class='Category2' >
<asp:Panel ID="moccategoryPanel" runat="server" >
 
    <asp:table CssClass="jeb" BackColor="" runat="server" CellSpacing="14"
        BorderStyle="Solid" BorderWidth="0" BorderColor="Black" CellPadding="4"
         GridLines="Horizontal"  >




        <asp:TableRow BackColor="white" >
            <asp:TableCell Width="200px" >
                <asp:CheckBox id="_cbCategoryNewchemicalapproval" runat="server"
                        Text='Chemical/Raw Material' >
                </asp:CheckBox>
            </asp:TableCell>


            <asp:TableCell>
                <asp:CheckBox id="_cbsubNewchemicalapproval" runat="server"
                        Text='New chemical approval' >
                </asp:CheckBox>
            </asp:TableCell>
        </asp:TableRow>

        <%--NEW COE--%>
		 <asp:TableRow  >
            <asp:TableCell >
                 <asp:CheckBox id="_cbCOE" runat="server"
                        Text='COE' >
                </asp:CheckBox>	&nbsp;&nbsp;
            </asp:TableCell>


            <asp:TableCell>
 		        <asp:CheckBox id="_cbsubCOEAlert" runat="server"
                Text='COE Alert' >
                </asp:CheckBox>
                &nbsp;


               <asp:CheckBox id="_cbsubCOEManufacturing" runat="server"
                Text='Manufacturing Policy Change' >
                </asp:CheckBox>
                &nbsp;


            </asp:TableCell>
        </asp:TableRow>





        <asp:TableRow BackColor="white">
            <asp:TableCell>
                <asp:CheckBox id="_cbEquipment" runat="server"
                        Text='Equipment' >
                </asp:CheckBox>
            </asp:TableCell>
           <asp:TableCell  >
                        <asp:DropDownList ID="_ddlSubEquipment" runat="server"  >
                            <asp:ListItem Value= ""  Text=""></asp:ListItem>
                            <asp:ListItem Value= "Additives">Additives</asp:ListItem>
                            <asp:ListItem Value= "Agitator/Pulper/Mixer">Agitator/Pulper/Mixer</asp:ListItem>
                            <asp:ListItem Value= "BMS/Flame Safety">BMS/Flame Safety</asp:ListItem>
                            <asp:ListItem Value= "Bleaching">Bleaching</asp:ListItem>
                            <asp:ListItem Value= "Boiler Specific">Boiler Specific</asp:ListItem>
                            <asp:ListItem Value= "Broke System">Broke System</asp:ListItem>
                            <asp:ListItem Value= "Building: Structural">Building: Structural</asp:ListItem>
                            <asp:ListItem Value= "Bumper/Kicker/Stop">Bumper/Kicker/Stop</asp:ListItem>
                            <asp:ListItem Value= "Burner">Burner</asp:ListItem>
                            <asp:ListItem Value= "Calcining">Calcining</asp:ListItem>
                            <asp:ListItem Value= "Calender Stack">Calender Stack</asp:ListItem>
                            <asp:ListItem Value= "Causticizing">Causticizing</asp:ListItem>
                            <asp:ListItem Value= "Chemical Additives">Chemical Additives</asp:ListItem>
                            <asp:ListItem Value= "Chemline">Chemline</asp:ListItem>
                            <asp:ListItem Value= "Chiller">Chiller</asp:ListItem>
                            <asp:ListItem Value= "Chipper">Chipper</asp:ListItem>
                            <asp:ListItem Value= "Clothing">Clothing</asp:ListItem>
                            <asp:ListItem Value= "Compressor">Compressor</asp:ListItem>
                            <asp:ListItem Value= "Conveyor">Conveyor</asp:ListItem>
                            <asp:ListItem Value= "Cooking">Cooking</asp:ListItem>
                            <asp:ListItem Value= "Crane/Hoist">Crane/Hoist</asp:ListItem>
                            <asp:ListItem Value= "Crusher">Crusher</asp:ListItem>
                            <asp:ListItem Value= "Debarking">Debarking</asp:ListItem>
                            <asp:ListItem Value= "Doctor">Doctor</asp:ListItem>
                            <asp:ListItem Value= "Drive">Drive</asp:ListItem>
                            <asp:ListItem Value= "Dryer Section">Dryer Section</asp:ListItem>
                            <asp:ListItem Value= "Electrical Control Devices">Electrical Control Devices</asp:ListItem>
                            <asp:ListItem Value= "Electrical Distribution">Electrical Distribution</asp:ListItem>
                            <asp:ListItem Value= "Evaporating">Evaporating</asp:ListItem>
                            <asp:ListItem Value= "Extruder">Extruder</asp:ListItem>
                            <asp:ListItem Value= "Fan/Blower/Air Handler">Fan/Blower/Air Handler</asp:ListItem>
                            <asp:ListItem Value= "Feeder">Feeder</asp:ListItem>
                            <asp:ListItem Value= "Firing">Firing</asp:ListItem>
                            <asp:ListItem Value= "Forming">Forming</asp:ListItem>
                            <asp:ListItem Value= "Fourdrininer">Fourdrininer</asp:ListItem>
                            <asp:ListItem Value= "Gearbox">Gearbox</asp:ListItem>
                            <asp:ListItem Value= "Headbox">Headbox</asp:ListItem>
                            <asp:ListItem Value= "Heat Exchanger">Heat Exchanger</asp:ListItem>
                            <asp:ListItem Value= "Hydraulic/Lubrication System">Hydraulic/Lubrication System</asp:ListItem>
                            <asp:ListItem Value= "Kiln">Kiln</asp:ListItem>
                            <asp:ListItem Value= "Knives/Slitters">Knives/Slitters</asp:ListItem>
                            <asp:ListItem Value= "MCC">MCC</asp:ListItem>
                            <asp:ListItem Value= "Motor">Motor</asp:ListItem>
                            <asp:ListItem Value= "Paper">Paper</asp:ListItem>
                            <asp:ListItem Value= "Piping">Piping</asp:ListItem>
                            <asp:ListItem Value= "Power Distribution">Power Distribution</asp:ListItem>
                            <asp:ListItem Value= "Pressure Vessel">Pressure Vessel</asp:ListItem>
                            <asp:ListItem Value= "Pulp">Pulp</asp:ListItem>
                            <asp:ListItem Value= "Pump">Pump</asp:ListItem>
                            <asp:ListItem Value= "QCS/Web Inspection">QCS/Web Inspection</asp:ListItem>
                            <asp:ListItem Value= "Reel">Reel</asp:ListItem>
                            <asp:ListItem Value= "Refiner">Refiner</asp:ListItem>
                            <asp:ListItem Value= "Repulping">Repulping</asp:ListItem>
                            <asp:ListItem Value= "Rewinding">Rewinding</asp:ListItem>
                            <asp:ListItem Value= "Roll: Paper Mach">Roll: Paper Mach</asp:ListItem>
                            <asp:ListItem Value= "Ropes">Ropes</asp:ListItem>
                            <asp:ListItem Value= "Screens">Screens</asp:ListItem>
                            <asp:ListItem Value= "Sheeters">Sheeters</asp:ListItem>
                            <asp:ListItem Value= "Soot Blower">Soot Blower</asp:ListItem>
                            <asp:ListItem Value= "Supplier">Supplier</asp:ListItem>
                            <asp:ListItem Value= "Tank/Chest/Tower">Tank/Chest/Tower</asp:ListItem>
                            <asp:ListItem Value= "Turbine-Gas/Steam">Turbine-Gas/Steam</asp:ListItem>
                            <asp:ListItem Value= "Utilities Specific">Utilities Specific</asp:ListItem>
                            <asp:ListItem Value= "Vacuum System">Vacuum System</asp:ListItem>
                            <asp:ListItem Value= "Valve">Valve</asp:ListItem>
                            <asp:ListItem Value= "Valve/Piping">Valve/Piping</asp:ListItem>
                            <asp:ListItem Value= "Washer">Washer</asp:ListItem>
                        </asp:DropDownList>
            </asp:TableCell>
        </asp:TableRow>



         <asp:TableRow BackColor="white" Visible="false">
            <asp:TableCell>
                <asp:CheckBox id="_cbOther" runat="server"
                        Text='Other' >
                </asp:CheckBox>
            </asp:TableCell>
           <asp:TableCell>
 		        <asp:CheckBox id="_cbsubCapitalProject" runat="server"
                Text='Capital Project' >
                </asp:CheckBox>
                &nbsp;


               <asp:CheckBox id="_cbsubGMSCertification" runat="server"
                Text='GMS Certification' >
                </asp:CheckBox>
                &nbsp;

               <asp:CheckBox id="_cbsubOther" runat="server"
                Text='Other' >
                </asp:CheckBox>
            </asp:TableCell>
         </asp:TableRow>

         <asp:TableRow BackColor="whitesmoke">
            <asp:TableCell>
                <asp:CheckBox id="_cbProcedure" runat="server"
                        Text='Procedure' >
                </asp:CheckBox>
            </asp:TableCell>
           <asp:TableCell  >
                  <asp:CheckBox id="_cbsubBasicCare" runat="server"
                Text='Basic Care' >
                </asp:CheckBox>
                &nbsp;&nbsp;

			     <asp:CheckBox id="_cbsubKOP" runat="server"
                Text='KOP' >
                </asp:CheckBox>
				&nbsp;&nbsp;

               <asp:CheckBox id="_cbsubPM" runat="server"
                Text='PM' >
                </asp:CheckBox>
                &nbsp;&nbsp;

               <asp:CheckBox id="_cbsubPdM" runat="server"
                Text='PdM' >
                </asp:CheckBox>
                &nbsp;&nbsp;

               <asp:CheckBox id="_cbsubSOP" runat="server"
                Text='SOP' >
                </asp:CheckBox>
               	&nbsp;&nbsp;

               <asp:CheckBox id="_cbsubShutdownAbandon" runat="server"
                Text='Shutdown/Abandon' >
                </asp:CheckBox>
               &nbsp;&nbsp;

               <asp:CheckBox id="_cbsubTCC" runat="server"
                Text='TCC' >
                </asp:CheckBox>
                &nbsp;&nbsp;


            </asp:TableCell>
         
</asp:TableRow>

         <asp:TableRow BackColor="white">
            <asp:TableCell >
                <asp:CheckBox id="_cbProcess" runat="server"
                        Text='Process' >
                </asp:CheckBox>
            </asp:TableCell>
           <asp:TableCell>
                <asp:CheckBox id="_cbsubGradeSpecification" runat="server"
                Text='Grade Specification' >
                </asp:CheckBox>
                &nbsp;&nbsp;


               <asp:CheckBox id="_cbsubMeasurementTechnique" runat="server"
                Text='Measurement Technique' >
                </asp:CheckBox>
                &nbsp;&nbsp;

               <asp:CheckBox id="_cbsubProductrecipe" runat="server"
                Text='Product Recipe' >
                </asp:CheckBox>
                &nbsp;&nbsp;

               <asp:CheckBox id="_cbsubSetpoint" runat="server"
                Text='Setpoint' >
                </asp:CheckBox>
            </asp:TableCell>
         </asp:TableRow>

         <asp:TableRow BackColor="whitesmoke">
            <asp:TableCell >
                <asp:CheckBox id="_cbProcessControl" runat="server"
                        Text='Process Control' >
                </asp:CheckBox>
            </asp:TableCell>
           <asp:TableCell >
            <asp:CheckBox id="_cbsubCLPM" runat="server"
                Text='CLPM' >
                </asp:CheckBox>
                &nbsp;&nbsp;


               <asp:CheckBox id="_cbsubDCS" runat="server"
                Text='DCS' >
                </asp:CheckBox>
               &nbsp;&nbsp;

               <asp:CheckBox id="_cbsubHMIGraphics" runat="server"
                Text='HMI/Graphics' >
                </asp:CheckBox>
                &nbsp;&nbsp;

               <asp:CheckBox id="_cbsubInstrumentLoops" runat="server"
                Text='Instrument Loops' >
                </asp:CheckBox>
               	&nbsp;&nbsp;

               <asp:CheckBox id="_cbsubLogicchanges" runat="server"
                Text='Logic changes' >
                </asp:CheckBox>
               &nbsp;&nbsp;

               <asp:CheckBox id="_cbsubPARCView" runat="server"
                Text='PARCView' >
                </asp:CheckBox>
                &nbsp;&nbsp;

               <asp:CheckBox id="_cbsubPI" runat="server"
                Text='PI' >
                </asp:CheckBox>
                &nbsp;&nbsp;

               <asp:CheckBox id="_cbsubPLC" runat="server"
                Text='PLC' >
                </asp:CheckBox>
                &nbsp;&nbsp;

               <asp:CheckBox id="_cbsubProficy" runat="server"
                Text='Proficy' >
                </asp:CheckBox>
               &nbsp;&nbsp;

               <asp:CheckBox id="_cbsubTuning" runat="server"
                Text='Tuning' >
                </asp:CheckBox>
               &nbsp;&nbsp;

            </asp:TableCell>
         </asp:TableRow>

         <asp:TableRow BackColor="white">
            <asp:TableCell>
                <asp:CheckBox id="_cbProductTrial" runat="server"
                        Text='Product Trial' >
                </asp:CheckBox>
            </asp:TableCell>
           <asp:TableCell>
                <asp:CheckBox id="_cbsubNewCustomer" runat="server"
                Text='New Customer' >
                </asp:CheckBox>
                &nbsp;&nbsp;
               <asp:CheckBox id="_cbsubNewProduct" runat="server"
                Text='New Product' >
                </asp:CheckBox>
               &nbsp;&nbsp;
               <asp:CheckBox id="_cbsubProductAffecting" runat="server"
                Text='Product Affecting' >
                </asp:CheckBox>
               &nbsp;&nbsp;
               <asp:CheckBox id="_cbsubQualifications" runat="server"
                Text='Qualifications' >
                </asp:CheckBox>
               &nbsp;&nbsp;
               <asp:CheckBox id="_cbsubRollHandling" runat="server"
                Text='Roll Handling' >
                </asp:CheckBox>
            </asp:TableCell>
         </asp:TableRow>

           <%-- Market Channel - false--%>
         <asp:TableRow BackColor="whitesmoke" Visible="false">
            <asp:TableCell>
                <asp:CheckBox id="_cbMarketChannel" runat="server"
                        Text='Market Channel' >
                </asp:CheckBox>
            </asp:TableCell>
           <asp:TableCell>
                <asp:DropDownList ID="_ddlSubMarketChannel" runat="server"  >
                    <asp:ListItem Value= ""  Text=""> </asp:ListItem>
                    <asp:ListItem Value= "Domestic" >Domestic</asp:ListItem>
                    <asp:ListItem Value= "Export" >Export</asp:ListItem>
                    <asp:ListItem Value= "NAC" >NAC</asp:ListItem>
                    <%--<asp:ListItem Value= "Science" >Science</asp:ListItem>--%>
                    <asp:ListItem Value= "Trades" >Trades</asp:ListItem>
                </asp:DropDownList>
            </asp:TableCell>
         </asp:TableRow>

         <%--Storeroom-- false--%>
         <asp:TableRow backcolor="white" Visible="false">
            <asp:TableCell>
                <asp:CheckBox id="_cbStoreroom" runat="server"
                        Text='Storeroom' >
                </asp:CheckBox>
            </asp:TableCell>
           <asp:TableCell>

               <asp:CheckBox id="_cbsubChangeMaterialDescription" runat="server"
                Text='Change Material Description' >
                </asp:CheckBox>
                &nbsp;
               <asp:CheckBox id="_cbsubExtend" runat="server"
                Text='Extend' >
                </asp:CheckBox>
                &nbsp;
               <asp:CheckBox id="_cbsubMaterialAdds" runat="server"
                Text='Material Adds' >
                </asp:CheckBox>
               &nbsp;
               <asp:CheckBox id="_cbsubMaterialDelete" runat="server"
                Text='Material Delete' >
                </asp:CheckBox>
              <asp:CheckBox id="_cbsubMinMaxChange" runat="server"
                Text='Min/Max Change' >
                </asp:CheckBox>
               &nbsp;
               <asp:CheckBox id="_cbsubPurchasingVendorMaterialSpec" runat="server"
                Text='Purchasing Vendor/Material Spec' >
                </asp:CheckBox>

           </asp:TableCell>
         </asp:TableRow>


    </asp:table>

     </asp:Panel>
</div>
