<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true"
    CodeFile="CostPerTractorUnderWarranty.aspx.cs" ValidateRequest="false" Inherits="View_Forms_Reports_CostPerTractorUnderWarranty" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  <script type="text/javascript" src="/WMS/JS/FusionCharts.js"></script>

    <script type="text/javascript">
function ExportGridWithSelection( strid )
       {
            var prtContent = document.getElementById(strid);
            document.getElementById('ctl00_ContentPlaceHolder1_hdnExport').value= prtContent.innerHTML;
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
       
       function getValidation()
       {
          
           var checkchkCategory=getCheckBoxStatus('ctl00_ContentPlaceHolder1_chkCategory');
           var checkchkClutchType=getCheckBoxStatus('ctl00_ContentPlaceHolder1_chkClutchType');
           var checkchkSpecialList=getCheckBoxStatus('ctl00_ContentPlaceHolder1_chkSpecialList');
             var checkchkRegionList=getCheckBoxStatus('ctl00_ContentPlaceHolder1_chkRegion');
         
           if( checkchkCategory=='0' && checkchkClutchType=='0' && checkchkSpecialList=='0' && checkchkRegionList=='0')
            {
            alert('Please select atleast one Category\n'+'Please select atleast one Clutch Type\n'+'Please select atleast one Special \n'+'Please select atleast one Region ');
            return false;
            }
         
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
    </script>

    <fieldset class="sectionBorder">
        <legend>Cost Per Tractor Under Warranty</legend>
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
            <td colspan="2">
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
                    <br />
                    <br />
                    <span class="cssLabel">From Period:</span>
                    <asp:DropDownList ID="drpFromMonth" runat="server">
                        <asp:ListItem Value="1">January</asp:ListItem>
                        <asp:ListItem Value="2">February</asp:ListItem>
                        <asp:ListItem Value="3">March</asp:ListItem>
                        <asp:ListItem Value="4">April</asp:ListItem>
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
                    </asp:DropDownList>
                </td>
                <td>
                    <br />
                    <br />
                    <span class="cssLabel">To Period:</span>
                    <asp:DropDownList ID="drpToMonth" runat="server">
                        <asp:ListItem Value="1">January</asp:ListItem>
                        <asp:ListItem Value="2">February</asp:ListItem>
                        <asp:ListItem Value="3">March</asp:ListItem>
                        <asp:ListItem Value="4">April</asp:ListItem>
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
                    </asp:DropDownList>
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
                <asp:RadioButtonList ID="rdoViewName" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="Reporting" >Reporting Month Wise</asp:ListItem>
                        <asp:ListItem Value="Replacement" >Replacement Month Wise</asp:ListItem>
                       
                    </asp:RadioButtonList>
                    <asp:RadioButtonList ID="rdo_Count_Quantity" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="Count" >Count</asp:ListItem>
                        <asp:ListItem Value="Quantity" >Quantity</asp:ListItem>
                       
                    </asp:RadioButtonList>
                                  

                </td>
                <td>
                    <b>Problem Type:</b>
                    <asp:RadioButtonList ID="rdoPrimary" runat="server" RepeatDirection="Horizontal">
                     <asp:ListItem  Value="0" >Primary</asp:ListItem>
                       <asp:ListItem  Value="1" >Consequences</asp:ListItem>
                         <asp:ListItem Selected="True" Value="2" >All</asp:ListItem>
                     </asp:RadioButtonList>
                   
                </td>
            </tr>
        </table>
        <br />
        <div class="cssButtonPanel">
            <asp:Button ID="btnShowGraph" runat="server" Text="Show Graph" CssClass="cssButton"
                ToolTip="Show Graph" OnClick="btnShowGraph_Click" />
            <asp:Button ID="btnExport" runat="server" Visible="false" OnClientClick="javascript:ExportGridWithSelection('ctl00_ContentPlaceHolder1_print_Grid');"
                OnClick="btnExport_Click" Text="Export to Excel" ToolTip="Export to Excel" CssClass="cssButton" />
            <asp:Button ID="btnShow" OnClientClick="return getValidation();" runat="server" CssClass="cssButton"
                Text="Show" ToolTip="Show" OnClick="btnShow_Click" />
        </div>
        <br />
        <div id="print_Grid" style="overflow: auto; height: 250px;" visible="false" runat="server">
            <asp:GridView AutoGenerateColumns="false" OnRowCreated="OnRowCreated" Width="100%"
                ID="grdWarranty" runat="server" EmptyDataText="No Records Found">
                <Columns>
                    <%-- <asp:BoundField DataField="Sales_Period" HeaderText="Sales Period" />
                    <asp:BoundField DataField="Sales" HeaderText="Sales" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="FY_Total" HeaderText="Tr. Under Wty I Year" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="SY_Total" HeaderText="Tr. Under Wty II Year" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"  />
                    <asp:BoundField DataField="FY_Average" HeaderText="Avg. Tr. Under Wty I Year" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" DataFormatString="{0:F2}" />
                    <asp:BoundField DataField="SY_Average" HeaderText="Avg. Tr. Under Wty II Year" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" DataFormatString="{0:F2}" />
                    <asp:BoundField DataField="Average" HeaderText="Avg. Tr. Under Wty" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" DataFormatString="{0:F2}" />
                    <asp:BoundField DataField="WarrantyPeriod" HeaderText="WP" />--%>
                </Columns>
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
          
        </div>
          <div id="divGraph" runat="server"  visible="false">
                <% =CreateChart() %>
            </div>
        <br />
        <br />
    </fieldset>
    <asp:HiddenField ID="hdnExport" runat="server" />
    <asp:HiddenField ID="hdnEngine" Value="0" runat="server" />

    <script type="text/javascript">
        var hdn = document.getElementById('ctl00_ContentPlaceHolder1_hdnEngine').value;
        getDiv(hdn);
    </script>

</asp:Content>
