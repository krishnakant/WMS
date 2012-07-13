<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SpecialDefault.aspx.cs" MasterPageFile="~/master/MasterPage.master" Inherits="View_Forms_Master_SpecialDefault" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset class="sectionBorder">
        <legend><%=strfieldset %></legend>
<asp:Panel runat="server" ID="pnlgrid">
    
        <div align="right" style="margin-right: 4%;">
            <asp:Button ID="btnAdd" runat="server" CausesValidation="false" ToolTip="Add" Text="Add"
                Width="100px" OnClick="btnAdd_Click"></asp:Button>
        </div>
        <br />
        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
        <div style="margin-left: 1%; width: 100%;">
            <asp:GridView AutoGenerateColumns="false"  Width="100%" DataKeyNames="ModelSpecialID"
               OnRowDeleting="gridDefault_RowDeleting"  OnRowEditing="gridDefault_RowEditing"   OnDataBound="eventhandlerSerialNo" ID="gridDefault"
                runat="server">
                <Columns>
                    <asp:BoundField HeaderText="#" ItemStyle-HorizontalAlign="Center" ReadOnly="True" />
                    <asp:BoundField DataField="ModelSpecial" HeaderText="Model Special" ReadOnly="True" ItemStyle-HorizontalAlign="Center" />
                    
                        <asp:CommandField ShowEditButton="True" EditImageUrl="~/images/icon-edit.gif" HeaderText="Update"
                                            ItemStyle-HorizontalAlign="Center" UpdateImageUrl="~/images/icon-edit.gif" ButtonType="Image"
                                            CancelImageUrl="~/images/icon-cancel.gif">
                                            
                                        </asp:CommandField>
                    
                    <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkbtnDelete" OnClientClick="return confirm('Are you sure you want to delete this record?');" runat="server"
                                CommandName="Delete">
                                <asp:Image ID="Image1" ImageUrl="~/images/icon-delete.gif" ToolTip="Delete" AlternateText="Delete"
                                    BorderStyle="None" runat="server" /></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
        </div>
        </asp:Panel>
        <asp:Panel ID="pnlAdd" runat="server" Visible="false">
 
<div>
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="cssLabel">
                        <asp:Label ID="lblModelSpecial" runat="server" Text="Model Special:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtModelSpecial" runat="server"  ToolTip="Model Special"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvtxtModelSpecial" ErrorMessage ="*" runat="server" Display="Dynamic" ControlToValidate="txtModelSpecial">
                        </asp:RequiredFieldValidator>
                        </td></tr></table>


</div>

<div class="cssButtonPanel">
            <asp:Button ID="btnSave" Text="Save" runat="server" ToolTip="Save" OnClick="btnSave_Click"  />
            <span style="margin-left: 1%;"></span>
            <asp:Button ID="btnCancel" ToolTip="Cancel" Text="Cancel" runat="server" CausesValidation="false" OnClick="btnCancel_Click" />
        </div>
</asp:Panel>

    </fieldset>
    
    <asp:HiddenField ID="hdnModelSpecialID" runat="server"  />
    
</asp:Content>