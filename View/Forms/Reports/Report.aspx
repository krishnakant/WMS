<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true" ValidateRequest="false"
    CodeFile="Report.aspx.cs" Inherits="View_Forms_Reports_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset class="sectionBorder">
        <legend>Data</legend>
          <div class="cssButtonPanel" id="btnPanel">
    
     
       <input type="button" value="Print" runat="server"  Visible="false" title="Print" id="btnPrint" class="cssButton"
            onclick="javascript:CallPrint('divGrid');" />
        <asp:Button ID="btnExport" Visible="false"  OnClientClick="return CallExport('divGrid');"
            runat="server" CssClass="cssButton"
            Text="Excel Export" ToolTip="Excel Export" OnClick="Button1_Click"></asp:Button>
    </div>
        <div id="divGrid" style="width:97%;overflow-x:scroll;">
            <asp:GridView ID="grdacrData" OnRowCreated="gridView_RowCreated" runat="server" OnDataBound="eventhandlerSerialNo" BackColor="White" BorderColor="#CCCCCC"
                BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="false"
                Visible="false" EmptyDataText="No Data Found">
                <Columns>
                <asp:BoundField   ItemStyle-HorizontalAlign="Center" HeaderText="#" />
                    <asp:BoundField DataField="WCDOCNO" HeaderStyle-HorizontalAlign="Left" HeaderText="WCDOCNO" />
                    <asp:BoundField DataField="DLR_REF" HeaderStyle-HorizontalAlign="Left" HeaderText="DLR_REF" />
                    <asp:BoundField DataField="TRACTOR_NO" HeaderStyle-HorizontalAlign="Left" HeaderText="TRACTOR NO" />
                    <asp:BoundField DataField="ENGINE_NO" HeaderStyle-HorizontalAlign="Left" HeaderText="ENGINE NO" />
                    <asp:BoundField DataField="INS_DATE" DataFormatString="{0:dd-MM-yyyy}"
                    HtmlEncode="False" HeaderStyle-HorizontalAlign="Left" HeaderText="INS DATE" />
                    <asp:BoundField DataField="DEF_DATE" DataFormatString="{0:dd-MM-yyyy}"
                    HtmlEncode="False" ItemStyle-HorizontalAlign="Center" HeaderText="DEF DATE" />
                    <asp:BoundField DataField="REP_DATE" DataFormatString="{0:dd-MM-yyyy}"
                    HtmlEncode="False" ItemStyle-HorizontalAlign="Center" HeaderText="REP DATE" />
                    <asp:BoundField DataField="HMR" ItemStyle-HorizontalAlign="Center" HeaderText="HMR" />
                    <asp:BoundField DataField="DLR_CO" ItemStyle-HorizontalAlign="Center" HeaderText="DEALER CODE" />
                    <asp:BoundField DataField="DEALER_NAME" HeaderStyle-HorizontalAlign="Left" HeaderText="DEALER NAME" />
                    <asp:BoundField DataField="REG"  ItemStyle-HorizontalAlign="Center" HeaderText="REG" />
                    <asp:BoundField DataField="CR_DATE"  DataFormatString="{0:dd-MM-yyyy}"
                    HtmlEncode="False" ItemStyle-HorizontalAlign="Center" HeaderText="CR DATE" />
                    <asp:BoundField DataField="ITEM_CODE"  ItemStyle-HorizontalAlign="Center" HeaderText="ITEM CODE" />
                    <asp:BoundField DataField="DESCRIPTION" HeaderStyle-HorizontalAlign="Left"  HeaderText="DESCRIPTION" />
                    <asp:BoundField DataField="QUANTITY"  ItemStyle-HorizontalAlign="Center" HeaderText="QUANTITY" />
                    <asp:BoundField DataField="COST"  ItemStyle-HorizontalAlign="Center" HeaderText="COST" />
                    <asp:BoundField DataField="DEF" ItemStyle-HorizontalAlign="Center" HeaderText="DEF" />
                    <asp:BoundField DataField="NDP"  ItemStyle-HorizontalAlign="Center" HeaderText="NDP" />
                    <asp:BoundField DataField="VALUE"  ItemStyle-HorizontalAlign="Center" HeaderText="VALUE" />
                    <asp:BoundField DataField="CVOICE"  ItemStyle-HorizontalAlign="Center" HeaderText="CVOICE" />
                    <asp:BoundField DataField="OUTLV"  ItemStyle-HorizontalAlign="Center" HeaderText="OUTLV" />
                    <asp:BoundField DataField="DT"  ItemStyle-HorizontalAlign="Center" HeaderText="DT" />
                    <asp:BoundField DataField="CUL_CODE"  ItemStyle-HorizontalAlign="Center" HeaderText="CULPRIT CODE" />
                    <asp:BoundField DataField="BLANK"  ItemStyle-HorizontalAlign="Center" HeaderText="Blank" />
                    <asp:BoundField DataField="CR_AMOUNT"  ItemStyle-HorizontalAlign="Center" HeaderText="CR-AMOUNT" />
                    <asp:BoundField DataField="AUTH_AMOUNT"  ItemStyle-HorizontalAlign="Center" HeaderText="AUTH AMT" />
                    <asp:BoundField DataField="DIFF"  ItemStyle-HorizontalAlign="Center" HeaderText="DIFF" />
                </Columns>
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
            <asp:GridView ID="grdSalesData" OnRowCreated="gridView_RowCreated" runat="server" OnDataBound="eventhandlerSerialNo1" BackColor="White" BorderColor="#CCCCCC"
                BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="false"
                Visible="false" EmptyDataText="No Data Found">
                <Columns>
              <asp:BoundField   ItemStyle-HorizontalAlign="Center" HeaderText="#" />
                    <asp:BoundField DataField="Sno"  ItemStyle-HorizontalAlign="Center" HeaderText="Sno" />
                    <asp:BoundField DataField="InvoiceNo"  ItemStyle-HorizontalAlign="Center" HeaderText="Invoice No" />
                    <asp:BoundField DataField="Date"  ItemStyle-HorizontalAlign="Center" HeaderText="Date" />
                    <asp:BoundField DataField="Dealer_Code"  ItemStyle-HorizontalAlign="Center" HeaderText="Dealer Code" />
                    <asp:BoundField DataField="Dealer_Name" HeaderStyle-HorizontalAlign="Left" HeaderText="Dealer Name" />
                    <asp:BoundField DataField="Blank"  ItemStyle-HorizontalAlign="Center" HeaderText="Blank" />
                    <asp:BoundField DataField="Model_Code"  ItemStyle-HorizontalAlign="Center" HeaderText="REP DATE" />
                    <asp:BoundField DataField="Quantity"  ItemStyle-HorizontalAlign="Center" HeaderText="Quantity" />
                    <asp:BoundField DataField="SalesAmount"  ItemStyle-HorizontalAlign="Center" HeaderText="Sales Amount" />
                    <asp:BoundField DataField="Discount"  ItemStyle-HorizontalAlign="Center" HeaderText="Discount" />
                    <asp:BoundField DataField="SPL.DIS"  ItemStyle-HorizontalAlign="Center" HeaderText="SPL#DIS" />
                    <asp:BoundField DataField="ExciseDuty"   ItemStyle-HorizontalAlign="Center" HeaderText="Excise Duty" />
                    <asp:BoundField DataField="Edu_Cess"  ItemStyle-HorizontalAlign="Center" HeaderText="Edu Cess" />
                    <asp:BoundField DataField="HR_ECess"  ItemStyle-HorizontalAlign="Center" HeaderText="Hr ECess" />
                    <asp:BoundField DataField="LSPD"  ItemStyle-HorizontalAlign="Center" HeaderText="LSPD" />
                    <asp:BoundField DataField="MSPSD"  ItemStyle-HorizontalAlign="Center" HeaderText="MSPSD" />
                    <asp:BoundField DataField="DHC"  ItemStyle-HorizontalAlign="Center" HeaderText="DHC" />
                    <asp:BoundField DataField="Taxable"  ItemStyle-HorizontalAlign="Center" HeaderText="Taxable" />
                    <asp:BoundField DataField="CST"  ItemStyle-HorizontalAlign="Center" HeaderText="CST" />
                    <asp:BoundField DataField="LST"  ItemStyle-HorizontalAlign="Center" HeaderText="LST" />
                    <asp:BoundField DataField="Surch"  ItemStyle-HorizontalAlign="Center" HeaderText="Surch" />
                    <asp:BoundField DataField="Entity/TOT"  ItemStyle-HorizontalAlign="Center" HeaderText="Enty/TOT" />
                    <asp:BoundField DataField="Dely_Chgs"  ItemStyle-HorizontalAlign="Center" HeaderText="Dely Chgs" />
                    <asp:BoundField DataField="Freight"  ItemStyle-HorizontalAlign="Center" HeaderText="Freight" />
                    <asp:BoundField DataField="Net_Amount"  ItemStyle-HorizontalAlign="Center" HeaderText="Net Amount" />
                    <asp:BoundField DataField="Cost"   ItemStyle-HorizontalAlign="Center" HeaderText="Cost" />
                    <asp:BoundField DataField="S.Off"  ItemStyle-HorizontalAlign="Center" HeaderText="S Off" />
                </Columns>
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
            <asp:GridView ID="grdProdData" OnRowCreated="gridView_RowCreated" runat="server" OnDataBound="eventhandlerSerialNo2" BackColor="White" BorderColor="#CCCCCC"
                BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="false"
                Visible="false" EmptyDataText="No Data Found">
                <Columns>
                <asp:BoundField   ItemStyle-HorizontalAlign="Center" HeaderText="#" />
                    <asp:BoundField DataField="Material"  ItemStyle-HorizontalAlign="Center" HeaderText="Material" />
                    <asp:BoundField DataField="SerialNo"  ItemStyle-HorizontalAlign="Center" HeaderText="Serial No" />
                    <asp:BoundField DataField="Plnt"  ItemStyle-HorizontalAlign="Center" HeaderText="Plnt" />
                    <asp:BoundField DataField="SLoc"  ItemStyle-HorizontalAlign="Center" HeaderText="SLoc" />
                    <asp:BoundField DataField="Description" HeaderStyle-HorizontalAlign="Left" HeaderText="Description of Technical Object" />
                </Columns>
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
            <asp:GridView ID="grdData" OnRowCreated="gridView_RowCreated" OnDataBound="eventhandlerSerialNo3" runat="server" BackColor="White" BorderColor="#CCCCCC"
                BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="false"
                Visible="false" EmptyDataText="No Data Found">
                <Columns>
                <asp:BoundField   ItemStyle-HorizontalAlign="Center" HeaderText="#" />
                    <asp:BoundField DataField="Code"  ItemStyle-HorizontalAlign="Center" HeaderText="Code" />
                    <asp:BoundField DataField="Description" HeaderStyle-HorizontalAlign="Left" HeaderText="Description" />
                </Columns>
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
        </div>
    </fieldset>
    <asp:Literal ID="literal1" runat="server"></asp:Literal>
    <asp:HiddenField ID="hdnExport" runat="server" />
</asp:Content>
