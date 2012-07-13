<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true"
    CodeFile="LabelConfig.aspx.cs" Inherits="View_Configurator_LabelConfig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset class="sectionBorder">
        <legend>Label Configurator</legend>
        <div style="margin-left:2%;margin-top:2%;">
            <table >
            <tr>
                <td class="cssLabel">
                    <asp:Label ID="lblForms" runat="server" Text="Select Form:" ></asp:Label>
                </td>
                
                <td>
                    <asp:DropDownList ID="drpForms" runat="server" OnSelectedIndexChanged="drpForms_SelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        </div>
     
            <asp:GridView ID="grdFormData" runat="server" EmptyDataText="No Controls Found" AutoGenerateColumns="False"
                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                CellPadding="3">
                <Columns>
                   
                       <asp:BoundField DataField="Label" HeaderStyle-HorizontalAlign="Center"  HeaderText="Label">
                        <ItemStyle HorizontalAlign="center" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Label Text">
                        <ItemTemplate>
                            <asp:TextBox ID="txtLabelText" runat="server" Text='<%#Bind("Text") %>'></asp:TextBox>
                            <asp:HiddenField ID="hdnID" runat="server" Value='<%#Bind("Label_ID") %>' />
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
       
      
    </fieldset>
      <div class="cssButtonPanel">
      <asp:Button ID="btnUpdate" runat="server" Enabled="false" Text="Update" ToolTip="Update" OnClick="btnUpdate_Click" /></div>
</asp:Content>
