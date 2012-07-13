<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true"
    CodeFile="UploadMode.aspx.cs" Inherits="View_Forms_Configurator_UploadMode" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset class="sectionBorder">
        <legend>File Upload Mode</legend>
        <div style="margin-left:2%;margin-top:2%;">
            <asp:GridView ID="grdUploadMode" runat="server" AutoGenerateColumns="False" BackColor="White"
                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                <Columns>
                   
                     <asp:BoundField DataField="Filename" HeaderStyle-HorizontalAlign="Center"  HeaderText="Excel File">
                        <ItemStyle HorizontalAlign="center" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Upload Mode">
                        <ItemTemplate  >
                            <asp:RadioButtonList ID="rdoMode" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">Append</asp:ListItem>
                                <asp:ListItem Value="1">Replace</asp:ListItem>
                            </asp:RadioButtonList>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
        </div>
        <div class="cssButtonPanel">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" OnClick="btnUpdate_Click" />
        </div>
    </fieldset>
</asp:Content>
