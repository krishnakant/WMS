<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true" EnableViewState="false"
    CodeFile="ACRGenerateReport.aspx.cs" Inherits="View_Forms_Reports_ACRGenerateReport"
    ValidateRequest="false" Title="WMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset class="sectionBorder">
        <legend>ACR Detail</legend>
        <div class="cssButtonPanel">
            <input type="button" value="Print" runat="server" visible="false" title="Print" id="btnPrint"
                class="cssButton" onclick="javascript:CallPrint('divGrid');" />
            <asp:Button ID="btnExport" Visible="false" 
                runat="server" CssClass="cssButton" Text="Excel Export" ToolTip="Excel Export"
                OnClick="Button1_Click"></asp:Button></div>
                <table border="1" cellpadding="0" cellspacing="0" width="40px">
                <tr align="left"><td style="color:Black;"><h6> VALUE:</h6></td><td style="color:Black;"><%=SumValue%></td></tr>
                <tr align="left"><td style="color:Black;"><h6>NO.&nbsp;Of&nbsp;Defect:</h6></td><td style="color:Black;"><%=SumDefect%></td></tr>
                </table>
        <div id="divGrid">
            <asp:GridView ID="grdacrData" Width="100%" OnRowCreated="gridView_RowCreated" runat="server" OnDataBound="eventhandlerSerialNo" BackColor="White"
                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" AllowPaging="true" OnPageIndexChanging="grdacrData_Paging" CellPadding="3" AutoGenerateColumns="false"
                Visible="false" EmptyDataText="No Data Found" PageSize="1000">
                <Columns>
                    <asp:BoundField ItemStyle-HorizontalAlign="Center" HeaderText="#" />
                    <asp:BoundField DataField="WCDOCNO" HeaderStyle-HorizontalAlign="Left" HeaderText="WCDOCNO" />
                    <asp:BoundField DataField="DLR_REF" HeaderStyle-HorizontalAlign="Left" HeaderText="DLR_REF" />
                    <asp:BoundField DataField="TRACTOR_NO" HeaderStyle-HorizontalAlign="Left" HeaderText="TRACTOR NO" />
                    <asp:BoundField DataField="ENGINE_NO" HeaderStyle-HorizontalAlign="Left" HeaderText="ENGINE NO" />
                    <asp:BoundField DataField="INS_DATE" DataFormatString="{0:dd-MM-yyyy}" HtmlEncode="False"
                        HeaderStyle-HorizontalAlign="Left" HeaderText="INS DATE" />
                    <asp:BoundField DataField="DEF_DATE" DataFormatString="{0:dd-MM-yyyy}" HtmlEncode="False"
                        ItemStyle-HorizontalAlign="Center" HeaderText="DEF DATE" />
                    <asp:BoundField DataField="REP_DATE" DataFormatString="{0:dd-MM-yyyy}" HtmlEncode="False"
                        ItemStyle-HorizontalAlign="Center" HeaderText="REP DATE" />
                    <asp:BoundField DataField="HMR" ItemStyle-HorizontalAlign="Center" HeaderText="HMR" />
                    <asp:BoundField DataField="DLR_CO" ItemStyle-HorizontalAlign="Center" HeaderText="DEALER CODE" />
                    <asp:BoundField DataField="DEALER_NAME" HeaderStyle-HorizontalAlign="Left" HeaderText="DEALER NAME" />
                    <asp:BoundField DataField="REG" ItemStyle-HorizontalAlign="Center" HeaderText="REG" />
                    <asp:BoundField DataField="CR_DATE" DataFormatString="{0:dd-MM-yyyy}" HtmlEncode="False"
                        ItemStyle-HorizontalAlign="Center" HeaderText="CR DATE" />
                    <asp:BoundField DataField="ITEM_CODE" ItemStyle-HorizontalAlign="Center" HeaderText="ITEM CODE" />
                    <asp:BoundField DataField="DESCRIPTION" HeaderStyle-HorizontalAlign="Left" HeaderText="DESCRIPTION" />
                    <asp:BoundField DataField="QUANTITY" ItemStyle-HorizontalAlign="Center" HeaderText="NO. Of Defect" />
                    <asp:BoundField DataField="COST" ItemStyle-HorizontalAlign="Center" HeaderText="COST" />
                    <asp:BoundField DataField="DEF" ItemStyle-HorizontalAlign="Center" HeaderText="DEF" />
                    <asp:BoundField DataField="NDP" ItemStyle-HorizontalAlign="Center" HeaderText="NDP" />
                    <asp:BoundField DataField="VALUE" ItemStyle-HorizontalAlign="Center" HeaderText="VALUE" />
                   <asp:BoundField DataField="CVOICE" ItemStyle-HorizontalAlign="Center" HeaderText="CVOICE" />
                    <asp:BoundField DataField="OUTLV" ItemStyle-HorizontalAlign="Center" HeaderText="OUTLV" />
                    <asp:BoundField DataField="DT" ItemStyle-HorizontalAlign="Center" HeaderText="DT" />
                    <asp:BoundField DataField="CUL_CODE" ItemStyle-HorizontalAlign="Center" HeaderText="CULPRIT CODE" />
                    <asp:BoundField DataField="CR_AMOUNT" ItemStyle-HorizontalAlign="Center" HeaderText="CR-AMOUNT" />
                    <asp:BoundField DataField="AUTH_AMOUNT" ItemStyle-HorizontalAlign="Center" HeaderText="AUTH AMT" />
                    <asp:BoundField DataField="DIFF" ItemStyle-HorizontalAlign="Center" HeaderText="DIFF" />
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
