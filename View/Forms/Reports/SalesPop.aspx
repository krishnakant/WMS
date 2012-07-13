<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalesPop.aspx.cs" Inherits="View_Forms_Reports_SalesPop" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">

    <script type="text/javascript">
 /* Function for Close a Model window */
function CloseWindow()
 {
 window.close();
 return false;
 }
 
  /* End Of Function  */
    </script>

    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
   <fieldset><legend>Sales</legend>

 <div style="overflow: auto; height: 600px; width: 100%;" id="print_Grid">
            <asp:GridView ID="grdSalesData" runat="server"
                OnDataBound="eventhandlerSerialNo" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None"
                BorderWidth="1px" CellPadding="3" AutoGenerateColumns="false" EmptyDataText="No Data Found"  AllowPaging="true" PageSize="500" >
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
         <div class="cssButtonPanel" style="width: 98%; margin: auto; margin-top: 20px; margin-bottom: 10px;"
                align="right">
               
                <asp:Button ID="btnClose" CssClass="cssButton" runat="server" Text="Close Window"
                    ToolTip="Close Window" OnClientClick="javascript:return CloseWindow();" />
            </div>
</fieldset>
<asp:HiddenField ID="hdnYear" runat="server" Value="0" />
<asp:HiddenField ID="hdnModelGroupName" runat="server" Value="0" />
<asp:HiddenField ID="hdnModelCategory" runat="server" Value="0" />

<asp:HiddenField ID="hdnClutchType" runat="server" Value="0" />

<asp:HiddenField ID="hdnModelSpecial" runat="server" Value="0" />

<asp:HiddenField ID="hdnRegion" runat="server" Value="0" />
<asp:HiddenField ID="hdnMonth" runat="server" Value="0" />

    </form>
</body>
</html>
