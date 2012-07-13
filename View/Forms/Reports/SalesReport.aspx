<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true"
    CodeFile="SalesReport.aspx.cs" Inherits="View_Forms_Reports_SalesReport"  ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 <script type="text/javascript">
 
  function getValidation()
       {
           var checkCheckBoxList=getCheckBoxStatus('ctl00_ContentPlaceHolder1_chkModelCodeList');
           var checkchkCategory=getCheckBoxStatus('ctl00_ContentPlaceHolder1_chkCategory');
           var checkchkClutchType=getCheckBoxStatus('ctl00_ContentPlaceHolder1_chkClutchType');
           var checkchkSpecialList=getCheckBoxStatus('ctl00_ContentPlaceHolder1_chkSpecialList');
           
        if(checkCheckBoxList=='0' && checkchkCategory=='0' && checkchkClutchType=='0' && checkchkSpecialList=='0')
            {
            alert('Please select atleast one model \n'+'Please select atleast one Category\n'+'Please select atleast one Clutch Type\n'+'Please select atleast one Special  ');
            return false;
            }
          else  if(checkCheckBoxList=='0')
             {
             alert('Please select atleast one model');
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
         
            else
            {
                return true;
            }
       }  
 </script>
    <fieldset class="sectionBorder">
        <legend>Sales</legend>
        <table>
            <tr>
                <td style="width: 64%;">
                    <h5>
                        <asp:Label ID="lblModeCode" runat="server" Text="Model"></asp:Label></h5>
                    <asp:Panel Height="180px" BorderWidth="1px" ID="pnlModelCodeList" runat="server"
                        BorderColor="#00678e" ScrollBars="Vertical">
                        <asp:CheckBoxList ID="chkModelCodeList" CellSpacing="4" ToolTip="select Model" RepeatColumns="10"
                            runat="server">
                        </asp:CheckBoxList>
                    </asp:Panel>
                    <input type="checkbox" id="chkSelectAll" onclick="javascript:SetAllCheckBoxes('ctl00_ContentPlaceHolder1_chkModelCodeList',this.id);" />Select
                    All
                </td>
                <td style="width: 32%;">
                    <h5>
                        <asp:Label ID="lblModelVariants" runat="server" Text="Variants"></asp:Label></h5>
                    <asp:Panel Height="40px" BorderWidth="1px" ID="pnlCategory" runat="server" BorderColor="#00678e"
                        ScrollBars="Vertical">
                        <asp:CheckBoxList ID="chkCategory" CellSpacing="4" ToolTip="Select Category" RepeatColumns="2"
                            runat="server">
                        </asp:CheckBoxList>
                    </asp:Panel>
                    <input type="checkbox" id="chkCategoryAll" onclick="javascript:SetAllCheckBoxes('ctl00_ContentPlaceHolder1_chkCategory',this.id);" />Select
                    All
                    <asp:Panel Height="40px" BorderWidth="1px" ID="pnlClutchType" runat="server" BorderColor="#00678e"
                        ScrollBars="Vertical">
                        <asp:CheckBoxList ID="chkClutchType" CellSpacing="4" ToolTip="Select Clutch Type"
                            RepeatColumns="2" runat="server">
                        </asp:CheckBoxList>
                    </asp:Panel>
                    <input type="checkbox" id="chkClutchAll" onclick="javascript:SetAllCheckBoxes('ctl00_ContentPlaceHolder1_chkClutchType',this.id);" />Select
                    All
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
        </table>
        <div class="cssButtonPanel">
            <asp:Button ID="btnExport" runat="server" Visible="false" Text="Export to Excel" ToolTip="Export to Excel"
                CssClass="cssButton" OnClick="btnExport_Click" OnClientClick="return CallExport('divGrid');"/>
            <asp:Button ID="btnShow" runat="server" Text="Show" OnClientClick="return getValidation();" ToolTip="Show" CssClass="cssButton" OnClick="btnShow_Click" />
        </div>
        <div id="divGrid">
            <asp:GridView ID="grdSalesData" runat="server"
                OnDataBound="eventhandlerSerialNo" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None"
                BorderWidth="1px" CellPadding="3" AutoGenerateColumns="false" EmptyDataText="No Data Found"  AllowPaging="true" PageSize="500" OnPageIndexChanging="grdsalesData_Paging">
                <Columns>
                    <asp:BoundField ItemStyle-HorizontalAlign="Center" HeaderText="#" />
                    <asp:BoundField DataField="Sno" ItemStyle-HorizontalAlign="Center" HeaderText="Sno" />
                    <asp:BoundField DataField="InvoiceNo" ItemStyle-HorizontalAlign="Center" HeaderText="Invoice No" />
                    <asp:BoundField DataField="Date" ItemStyle-HorizontalAlign="Center" HeaderText="Date"  DataFormatString="{0:dd-MM-yyyy}" HtmlEncode="False"/>
                    <asp:BoundField DataField="Dealer_Code" ItemStyle-HorizontalAlign="Center" HeaderText="Dealer Code" />
                    <asp:BoundField DataField="Dealer_Name" HeaderStyle-HorizontalAlign="Left" HeaderText="Dealer Name" />
                    <asp:BoundField DataField="Blank" ItemStyle-HorizontalAlign="Center" HeaderText="Material" />
                    <asp:BoundField DataField="Model_Code" ItemStyle-HorizontalAlign="Center" HeaderText="REP DATE" />
                    <asp:BoundField DataField="Quantity" ItemStyle-HorizontalAlign="Center" HeaderText="Quantity" />
                    <asp:BoundField DataField="SalesAmount" ItemStyle-HorizontalAlign="Center" HeaderText="Sales Amount" />
                    <asp:BoundField DataField="Discount" ItemStyle-HorizontalAlign="Center" HeaderText="Discount" />
                    <asp:BoundField DataField="SPL.DIS" ItemStyle-HorizontalAlign="Center" HeaderText="SPL DIS" />
                    <asp:BoundField DataField="ExciseDuty" ItemStyle-HorizontalAlign="Center" HeaderText="Excise Duty" />
                    <asp:BoundField DataField="Edu_Cess" ItemStyle-HorizontalAlign="Center" HeaderText="Edu Cess" />
                    <asp:BoundField DataField="HR_ECess" ItemStyle-HorizontalAlign="Center" HeaderText="Hr ECess" />
                    <asp:BoundField DataField="LSPD" ItemStyle-HorizontalAlign="Center" HeaderText="LSPD" />
                    <asp:BoundField DataField="MSPSD" ItemStyle-HorizontalAlign="Center" HeaderText="MSPSD" />
                    <asp:BoundField DataField="DHC" ItemStyle-HorizontalAlign="Center" HeaderText="DHC" />
                    <asp:BoundField DataField="Taxable" ItemStyle-HorizontalAlign="Center" HeaderText="Taxable" />
                    <asp:BoundField DataField="CST" ItemStyle-HorizontalAlign="Center" HeaderText="CST" />
                    <asp:BoundField DataField="LST" ItemStyle-HorizontalAlign="Center" HeaderText="LST" />
                    <asp:BoundField DataField="Surch" ItemStyle-HorizontalAlign="Center" HeaderText="Surch" />
                    <asp:BoundField DataField="Entity/TOT" ItemStyle-HorizontalAlign="Center" HeaderText="Enty/TOT" />
                    <asp:BoundField DataField="Dely_Chgs" ItemStyle-HorizontalAlign="Center" HeaderText="Dely Chgs" />
                    <asp:BoundField DataField="Freight" ItemStyle-HorizontalAlign="Center" HeaderText="Freight" />
                    <asp:BoundField DataField="Net_Amount" ItemStyle-HorizontalAlign="Center" HeaderText="Net Amount" />
                    <asp:BoundField DataField="Cost" ItemStyle-HorizontalAlign="Center" HeaderText="Cost" />
                    <asp:BoundField DataField="S.Off" ItemStyle-HorizontalAlign="Center" HeaderText="S Off" />
                </Columns>
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
        </div>
    </fieldset>
    <asp:HiddenField ID="hdnExport" runat="server" />
</asp:Content>
