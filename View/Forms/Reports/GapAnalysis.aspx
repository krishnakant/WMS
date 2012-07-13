<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true"
    CodeFile="GapAnalysis.aspx.cs" ValidateRequest="false" Inherits="View_Forms_Reports_GapAnalysis" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="/WMS/JS/FusionCharts.js"></script>

    <script type="text/javascript">
function ExportGridWithSelection( strid )
       {
            var prtContent = document.getElementById(strid);
            var content = getselection();
            document.getElementById('ctl00_ContentPlaceHolder1_hdnExport').value = content + prtContent.innerHTML;
       }    
       
        function getselection()
       {
            //var d = new Date();
            var Model=document.getElementById('ctl00_ContentPlaceHolder1_drpModel').options[document.getElementById('ctl00_ContentPlaceHolder1_drpModel').selectedIndex].text;
            var TrUnWty = document.getElementById('ctl00_ContentPlaceHolder1_lblTractorUnderWarranty').innerHTML;
            var TrUnWty_To = document.getElementById('ctl00_ContentPlaceHolder1_lblTractorUnderWarranty_To').innerHTML;
            var creteDiv='<br /><table cellpadding=0 cellspacing=0 border=1><tr><td class=cssLabel style="width:100%;align:center;"><b>Top Contributors</b></td></tr><tr><td class="cssLabel"><b>Model: '+ Model +'</b></td></tr><tr><td class="cssLabel">'+ TrUnWty +'</td> <td class="cssLabel">'+ TrUnWty_To +'</td></tr>';
            creteDiv +='</table><br />';
            return creteDiv;
       }
       
       function getValidation()
       {
           //var checkCheckBoxList=getCheckBoxStatus('ctl00_ContentPlaceHolder1_chkModelCodeList');
           var checkchkCategory=getCheckBoxStatus('ctl00_ContentPlaceHolder1_chkCategory');
           var checkchkClutchType=getCheckBoxStatus('ctl00_ContentPlaceHolder1_chkClutchType');
           var checkchkSpecialList=getCheckBoxStatus('ctl00_ContentPlaceHolder1_chkSpecialList');
            var checkchkRegionList=getCheckBoxStatus('ctl00_ContentPlaceHolder1_chkRegion');
         //checkCheckBoxList=='0' &&
           if( checkchkCategory=='0' && checkchkClutchType=='0' && checkchkSpecialList=='0' && checkchkRegionList=='0')
            {
            alert('Please select atleast one model \n'+'Please select atleast one Category\n'+'Please select atleast one Clutch Type\n'+'Please select atleast one Special\n'+'Please select atleast one Region ');
            return false;
            }
//          else  if(checkCheckBoxList=='0')
//             {
//             alert('Please select atleast one model');
//             return false;
//             }
          else  if(checkchkCategory=='0')
               {
             alert('Please select atleast one Category');
             return false;
              }
          else  if(checkchkClutchType=='0')
              {
             alert('Please select atleast one Clutch Type');
             return false;
              }
         else  if(checkchkSpecialList=='0')
               {
             alert('Please select atleast one Special ');
             return false;
              }
       else  if(checkchkRegionList=='0')
             {
               alert('Please select atleast one Region ');
               return false;
             }
          else
            {
           return true;
            }
       }  
       
       function getDiv(val)
        {
             document.getElementById('ctl00_ContentPlaceHolder1_hdnEngine').value = val;
            if(val=='0')
            {
                 document.getElementById('placediv').style.display='';
                 document.getElementById('enginediv').style.display='none';                    
            }
            else if(val =='1')
            {
                 document.getElementById('enginediv').style.display='';
                 document.getElementById('placediv').style.display='none';                 
            }
            else
            {
                document.getElementById('enginediv').style.display='none';
                document.getElementById('placediv').style.display='none';  
            }
    }
    </script>

    <fieldset class="sectionBorder">
        <legend>GAP Analysis</legend>
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td>
                    <h5>
                        <asp:Label ID="lblModeCode" runat="server" Text="Model"></asp:Label></h5>
                    <asp:DropDownList ID="drpModel" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td width="33%">
                    <h5>
                        <asp:Label ID="lblModelVariants" runat="server" Text="Variants"></asp:Label></h5>
                    <asp:Panel Height="60px" BorderWidth="1px" ID="pnlCategory" runat="server" BorderColor="#00678e"
                        ScrollBars="Vertical">
                        <asp:CheckBoxList ID="chkCategory" CellSpacing="4" ToolTip="Select Category" RepeatColumns="2"
                            runat="server">
                        </asp:CheckBoxList>
                    </asp:Panel>
                    <input type="checkbox" id="chkCategoryAll" onclick="javascript:SetAllCheckBoxes('ctl00_ContentPlaceHolder1_chkCategory',this.id);" />Select
                    All
                </td>
                <td width="33%">
                    <br />
                    <asp:Panel Height="60px" BorderWidth="1px" ID="pnlClutchType" runat="server" BorderColor="#00678e"
                        ScrollBars="Vertical">
                        <asp:CheckBoxList ID="chkClutchType" CellSpacing="4" ToolTip="Select Clutch Type"
                            RepeatColumns="2" runat="server">
                        </asp:CheckBoxList>
                    </asp:Panel>
                    <input type="checkbox" id="chkClutchAll" onclick="javascript:SetAllCheckBoxes('ctl00_ContentPlaceHolder1_chkClutchType',this.id);" />Select
                    All
                </td>
                <td width="33%">
                    <br />
                    <asp:Panel Height="60px" Width="100%" BorderWidth="1px" ID="pnlSpecial" runat="server"
                        BorderColor="#00678e" ScrollBars="Vertical">
                        <asp:CheckBoxList ID="chkSpecialList" CellSpacing="4" ToolTip="Select Special" RepeatColumns="2"
                            runat="server">
                        </asp:CheckBoxList>
                    </asp:Panel>
                    <input type="checkbox" id="chkSpecialAll" onclick="javascript:SetAllCheckBoxes('ctl00_ContentPlaceHolder1_chkSpecialList',this.id);" />Select
                    All
                </td>
            </tr>
             <tr>
            <td >
                    <h5>
                        <asp:Label ID="lblRegion" runat="server" Text="Region"></asp:Label></h5>
                    <asp:Panel Height="90px" BorderWidth="1px" ID="pnlRegion" runat="server"
                        BorderColor="#00678e" ScrollBars="Vertical">
                        <asp:CheckBoxList ID="chkRegion" CellSpacing="4" ToolTip="Select Region" RepeatColumns="5"
                            runat="server">
                        </asp:CheckBoxList>
                    </asp:Panel>
                    <input type="checkbox" id="chkRegionAll" runat="server" onclick="javascript:SetAllCheckBoxes('ctl00_ContentPlaceHolder1_chkRegion',this.id);" />Select
                    All
                </td>
            </tr>
            <tr>
                <td>
                   
                <span class="cssLabel">
                    Period:</span>
               
                    <asp:DropDownList ID="drpFromMonth" runat="server">
                        <asp:ListItem Value="1">January</asp:ListItem>
                        <asp:ListItem Value="2">February</asp:ListItem>
                        <asp:ListItem Value="3">March</asp:ListItem>
                        <asp:ListItem Value="4" Selected="True">April</asp:ListItem>
                        <asp:ListItem Value="5">May</asp:ListItem>
                        <asp:ListItem Value="6">June</asp:ListItem>
                        <asp:ListItem Value="7">July</asp:ListItem>
                        <asp:ListItem Value="8">August</asp:ListItem>
                        <asp:ListItem Value="9">September</asp:ListItem>
                        <asp:ListItem Value="10">October</asp:ListItem>
                        <asp:ListItem Value="11">November</asp:ListItem>
                        <asp:ListItem Value="12">December</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="drpFromYear" runat="server">
                        <asp:ListItem Selected="true" Value="2000">2000</asp:ListItem>
                        <asp:ListItem Value="2001">2001</asp:ListItem>
                        <asp:ListItem Value="2002">2002</asp:ListItem>
                        <asp:ListItem Value="2003">2003</asp:ListItem>
                        <asp:ListItem Value="2004">2004</asp:ListItem>
                        <asp:ListItem Value="2005">2005</asp:ListItem>
                        <asp:ListItem Value="2006">2006</asp:ListItem>
                        <asp:ListItem Value="2007">2007</asp:ListItem>
                        <asp:ListItem Value="2008">2008</asp:ListItem>
                        <asp:ListItem Value="2009">2009</asp:ListItem>
                        <asp:ListItem Value="2010">2010</asp:ListItem>
                        <asp:ListItem Value="2011">2011</asp:ListItem>
                        <asp:ListItem Value="2012">2012</asp:ListItem>
                        <asp:ListItem Value="2013">2013</asp:ListItem>
                    </asp:DropDownList>
                    </td>
           
                 <%--   <asp:DropDownList ID="drp_Financial_Year_I" runat="server">
                        <asp:ListItem Value="0">2000-2001</asp:ListItem>
                        <asp:ListItem Value="1">2001-2002</asp:ListItem>
                        <asp:ListItem Value="2">2002-2003</asp:ListItem>
                        <asp:ListItem Value="3">2003-2004</asp:ListItem>
                        <asp:ListItem Value="4">2004-2005</asp:ListItem>
                        <asp:ListItem Value="5">2005-2006</asp:ListItem>
                        <asp:ListItem Value="6">2006-2007</asp:ListItem>
                        <asp:ListItem Value="7" Selected="True">2007-2008</asp:ListItem>
                        <asp:ListItem Value="8">2008-2009</asp:ListItem>
                        <asp:ListItem Value="9">2009-2010</asp:ListItem>
                    </asp:DropDownList>--%>
                    
               
                <td>
                    <br />
                    <br />
                     <span class="cssLabel">
                    Period:</span>
               
                    <asp:DropDownList ID="drpToMonth" runat="server">
                        <asp:ListItem Value="1">January</asp:ListItem>
                        <asp:ListItem Value="2">February</asp:ListItem>
                        <asp:ListItem Value="3">March</asp:ListItem>
                        <asp:ListItem Value="4" Selected="True">April</asp:ListItem>
                        <asp:ListItem Value="5">May</asp:ListItem>
                        <asp:ListItem Value="6">June</asp:ListItem>
                        <asp:ListItem Value="7">July</asp:ListItem>
                        <asp:ListItem Value="8">August</asp:ListItem>
                        <asp:ListItem Value="9">September</asp:ListItem>
                        <asp:ListItem Value="10">October</asp:ListItem>
                        <asp:ListItem Value="11">November</asp:ListItem>
                        <asp:ListItem Value="12">December</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="drpToYear" runat="server">
                        <asp:ListItem Selected="true" Value="2000">2000</asp:ListItem>
                        <asp:ListItem Value="2001">2001</asp:ListItem>
                        <asp:ListItem Value="2002">2002</asp:ListItem>
                        <asp:ListItem Value="2003">2003</asp:ListItem>
                        <asp:ListItem Value="2004">2004</asp:ListItem>
                        <asp:ListItem Value="2005">2005</asp:ListItem>
                        <asp:ListItem Value="2006">2006</asp:ListItem>
                        <asp:ListItem Value="2007">2007</asp:ListItem>
                        <asp:ListItem Value="2008">2008</asp:ListItem>
                        <asp:ListItem Value="2009">2009</asp:ListItem>
                        <asp:ListItem Value="2010">2010</asp:ListItem>
                        <asp:ListItem Value="2011">2011</asp:ListItem>
                        <asp:ListItem Value="2012">2012</asp:ListItem>
                        <asp:ListItem Value="2013">2013</asp:ListItem>
                    </asp:DropDownList>
                   <%-- <span class="cssLabel">Financial Year II:</span>
                    <asp:DropDownList ID="drp_Financial_Year_II" runat="server">
                        <asp:ListItem Value="0">2000-2001</asp:ListItem>
                        <asp:ListItem Value="1">2001-2002</asp:ListItem>
                        <asp:ListItem Value="2">2002-2003</asp:ListItem>
                        <asp:ListItem Value="3">2003-2004</asp:ListItem>
                        <asp:ListItem Value="4">2004-2005</asp:ListItem>
                        <asp:ListItem Value="5">2005-2006</asp:ListItem>
                        <asp:ListItem Value="6">2006-2007</asp:ListItem>
                        <asp:ListItem Value="7" Selected="True">2007-2008</asp:ListItem>
                        <asp:ListItem Value="8">2008-2009</asp:ListItem>
                        <asp:ListItem Value="9">2009-2010</asp:ListItem>
                    </asp:DropDownList>--%>
                   
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButtonList ID="rdoData" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="0" onclick="getDiv(this.value);">Overall</asp:ListItem>
                        <asp:ListItem Value="1" onclick="getDiv(this.value);">Engine</asp:ListItem>
                        <asp:ListItem Value="2" onclick="getDiv(this.value);">Tractor</asp:ListItem>
                    </asp:RadioButtonList>
                    <div id="enginediv" style="display: none;">
                        <span class="cssLabel">Engine:</span>
                        <asp:RadioButton ID="rdoAlwarEngine" runat="server" Text="Alwar" GroupName="Engine" /><asp:RadioButton
                            ID="rdoSimpsonEngine" GroupName="Engine" runat="server" Text="Simpson" /><asp:RadioButton
                                ID="rdoBothEngine" runat="server" Checked="true" Text="Both" GroupName="Engine" /></div>
                    <div id="placediv">
                        <span class="cssLabel">Place:</span>
                        <asp:RadioButton ID="rdoAlwar" runat="server" Text="Alwar" GroupName="Place" /><asp:RadioButton
                            ID="rdoBhopal" GroupName="Place" runat="server" Text="Bhopal" /><asp:RadioButton
                                ID="rdoAllPlace" runat="server" Checked="true" Text="Both" GroupName="Place" /></div>
                </td>
                <td>
                    <asp:RadioButtonList ID="rdoYear" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="0" Selected="True">Total</asp:ListItem>
                        <asp:ListItem Value="1">I Year</asp:ListItem>
                        <asp:ListItem Value="2" >II Year</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>
                    <span class="cssLabel">HMR Range:</span>
                    <asp:RadioButtonList ID="rdoHMR_Range" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="0">0-250</asp:ListItem>
                        <asp:ListItem Value="1">251-2500</asp:ListItem>
                        <asp:ListItem Value="2" Selected="True">Both</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                
            </tr>
        </table>
        <br />
        <div class="cssButtonPanel">
            <asp:Button ID="btnExport" runat="server" Visible="false" OnClientClick="javascript:ExportGridWithSelection('print_Grid');"
                OnClick="btnExport_Click" Text="Export to Excel" ToolTip="Export to Excel" CssClass="cssButton" />
            <asp:Button ID="btnShow" OnClientClick="return getValidation();" runat="server" CssClass="cssButton"
                Text="Show" ToolTip="Show" OnClick="btnShow_Click" />
        </div>
        <br />
        
        <asp:Label ID="lblTractorUnderWarranty" runat="server" CssClass="cssLabel"></asp:Label>
        <asp:Label ID="lblTractorUnderWarranty_To" runat="server" CssClass="cssLabel"></asp:Label>
        <br />
        <br />
        <div id="print_Grid" style="overflow: auto; height: 250px;">
            <asp:GridView AutoGenerateColumns="false" OnRowCreated="grdWarranty_RowCreated" Width="100%"
                ID="grdWarranty" runat="server" EmptyDataText="No Records Found" OnDataBound="eventhandlerSerialNo">
                <Columns>
                    <asp:BoundField HeaderText="#" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="ItemGroupName" HeaderText="Item Description" />
                    <asp:BoundField DataField="Quantity_I" HeaderText="Quantity" ItemStyle-HorizontalAlign="Center"
                        HeaderStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Failure_Per_K_I" HeaderText="Failure/1000" ItemStyle-HorizontalAlign="Center"
                        HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" DataFormatString="{0:F2}" />
                    <asp:BoundField DataField="Value_I" HeaderText="Value" ItemStyle-HorizontalAlign="Center"
                        HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" DataFormatString="{0:F2}" />
                    <asp:BoundField DataField="Cost_Per_Tractor_I" HeaderText="Cost Per Tractor" ItemStyle-HorizontalAlign="Center"
                        HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" DataFormatString="{0:F2}" />
                    <asp:BoundField DataField="Quantity_II" HeaderText="Quantity" ItemStyle-HorizontalAlign="Center"
                        HeaderStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Failure_Per_K_II" HeaderText="Failure/1000" ItemStyle-HorizontalAlign="Center"
                        HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" DataFormatString="{0:F2}" />
                    <asp:BoundField DataField="Value_II" HeaderText="Value" ItemStyle-HorizontalAlign="Center"
                        HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" DataFormatString="{0:F2}" />
                    <asp:BoundField DataField="Cost_Per_Tractor_II" HeaderText="Cost Per Tractor" ItemStyle-HorizontalAlign="Center"
                        HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" DataFormatString="{0:F2}" />
                          <asp:BoundField DataField="Diff" HeaderText="Diff" ItemStyle-HorizontalAlign="Center"
                        HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" DataFormatString="{0:F2}" />
                </Columns>
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
        </div>
        <br />
        <br />
    </fieldset>
    <asp:HiddenField ID="hdnExport" runat="server" />
    <asp:HiddenField ID="hdnSelected" runat="server" Value="0" />
    <asp:HiddenField ID="hdnEngine" Value="0" runat="server" />

    <script type="text/javascript">
        var hdn = document.getElementById('ctl00_ContentPlaceHolder1_hdnEngine').value;
        getDiv(hdn);
    </script>

</asp:Content>
