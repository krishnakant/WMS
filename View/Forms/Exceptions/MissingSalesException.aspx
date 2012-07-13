<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true"
    CodeFile="MissingSalesException.aspx.cs" Inherits="View_Forms_Exceptions_MissingSalesException" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <fieldset>
            <legend>Exceptions</legend>
            <div style="overflow: auto; height: 400px; width: 950px;">
                <table>
                    <asp:GridView ID="grdSalesException" runat="server" BackColor="White" BorderColor="#CCCCCC"
                        BorderStyle="None" BorderWidth="1px" Width="100%" HeaderStyle-CssClass="cssGridHeader" CellPadding="3" AutoGenerateColumns="false"
                        EmptyDataText="No Data Found">
                        <Columns>
                            <asp:TemplateField HeaderText="Sno">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtSno" runat="server" Text='<%# Bind("Sno") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Invoice No">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtInvoiceno" Text='<%#Bind("[Inv#No]") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDate" Text='<%#Bind("Date") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Dealer Code">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDealerCode" runat="server" Text='<%#Bind("DlrCode") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Dealer Name">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDealerName" runat="server" Text='<%# Bind("[Dlr Name]") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Material">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtBlank" runat="server" Text='<%#Bind("F10") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Model Code" Visible="false">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtModelCode" runat="server" Text='<%# Bind("[Model Code]") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Quantity">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtQuantity" runat="server" Text='<%# Bind("[Qty]") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sales Amount">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtSalesAmt" runat="server" Text='<%# Bind("[Sale Amt]") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Discount">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDiscount" runat="server" Text='<%#Bind("Discount") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SPL.DIS">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtSPLDIS" runat="server" Text='<%#Bind("[SPL#DIS]") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Excise Duty">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtExciseDuty" Text='<%# Bind("[Excise Duty]") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Edu Cess">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtEduCess" runat="server" Text='<%#Bind("[Edu# Cess]") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Hr ECess">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtHrECess" Text='<%#Bind("[HR#ECess]") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="LSPD">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtLSPD" runat="server" Text='<%#Bind("LSPD") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MSPSD">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtMSPSD" Text='<%#Bind("MSPSD") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DHC">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDHC" runat="server" Text='<%#Bind("DHC") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Taxable">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtTaxable" runat="server" Text='<%#Bind("Taxable") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CST">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCST" Text='<%# Bind("CST") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="LST">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtLST" runat="server" Text='<%#Bind("LST") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Surch">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtSurch" runat="server" Text='<%#Bind("Surch") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Enty/TOT">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtEntityTot" Text='<%#Bind("[Enty/TOT]")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Dely Chgs">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDelyChgs" runat="server" Text='<%# Bind("[Dely Chgs]") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Freight">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFreight" runat="server" Text='<%#Bind("Freight") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Net Amount">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtNetAmt" runat="server" Text='<%# Bind("[Net Amount]") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cost">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCost" runat="server" Text='<%#Bind("Cost") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="S Off">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtSOff" runat="server" Text='<%#Bind("[S#Off]") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="FromDate" HeaderText="From Date" />
                            <asp:BoundField DataField="ToDate" HeaderText="To Date" />
                            <%--<asp:TemplateField HeaderText="Is Approved">
<ItemTemplate>
<asp:CheckBox ID="chkIsApproved" runat="server" Checked='<%# Bind("IsApproved") %>' />
</ItemTemplate>
</asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Discard">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkDiscard" runat="server" />
                                    <asp:HiddenField ID="hdnID" runat="server" Value='<%# Bind("ID") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <RowStyle ForeColor="#000066" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        
                    </asp:GridView>
                </table>
                <div class="cssButtonPanel">
                <asp:Button ID="btnUpdate"  runat="server" Text="Update" OnClick="btnUpdate_Click" />
                </div>
            </div>
        </fieldset>
    </div>
</asp:Content>
