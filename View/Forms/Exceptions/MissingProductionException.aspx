<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true"
    CodeFile="MissingProductionException.aspx.cs" Inherits="View_Forms_Exceptions_MissingProductionException" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <fieldset>
            <legend>Exceptions</legend>
            <div style="overflow: auto; height: 300px; width: 950px;">
                <table>
                    <tr>
                        <asp:GridView ID="grdProdException" runat="server" BackColor="White" BorderColor="#CCCCCC"
                            BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="false"
                            EmptyDataText="No Data Found">
                            <Columns>
                                <asp:TemplateField HeaderText="S">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtS" runat="server" Text='<%# Bind("S") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Material">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtMaterial" runat="server" Text='<%# Bind("Material") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Serial No">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtSerialNo" runat="server" Text='<%# Bind("[Serial no#]") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Plnt">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPlnt" runat="server" Text='<%# Bind("[Plnt]") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SLoc">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtSLoc" runat="server" Text='<%# Bind("[SLoc]") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDescription" runat="server" Text='<%# Bind("[Description of technical object]") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="FromDate" HeaderText="From Date" HtmlEncode="false" DataFormatString="{0:dd-MM-yyyy} " />
                                <asp:BoundField DataField="ToDate" HeaderText="To Date"  HtmlEncode="false" DataFormatString="{0:dd-MM-yyyy} " />
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
                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                        </asp:GridView>
                    </tr>
                    
                </table>
            </div>
            <div class="cssButtonPanel">
              <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" />
            </div>
        </fieldset>
    </div>
</asp:Content>
