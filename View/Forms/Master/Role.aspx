<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true"
    CodeFile="Role.aspx.cs" Inherits="View_Forms_Master_Role" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset class="sectionBorder">
        <legend>Role</legend>
        <div>
            <table>
                <tr>
                    <td class="cssLabel">
                        <asp:Label ID="lblRole" runat="server" Text="Role:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtRole" ToolTip="Role" MaxLength="50" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator" ControlToValidate="txtRole"
                            ErrorMessage="*" runat="server">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:Label ID="lblMessage" runat="server" ForeColor="red" Font-Bold="true" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td class="cssLabel">
                        <asp:Label ID="lblActive" runat="server" Text="Active:"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:CheckBox ID="chkActive" Checked="true" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
    </fieldset>
    <div class="cssButtonPanel">
        <asp:Button ID="btnSave" CausesValidation="true" Text="Save" OnClick="btnSave_Click"
            runat="server" ToolTip="Save" />
        <asp:Button ID="btnCancel" CausesValidation="false" Text="Cancel" OnClick="btnCancel_Click"
            runat="server" ToolTip="Cancel" />
    </div>
     <asp:Literal ID="literal1" runat="server"></asp:Literal>
    <asp:HiddenField ID="hdnCheckID" Value="0" runat="server" />
    <asp:HiddenField ID="hdnRoleID" Value="0" runat="server" />
    <asp:HiddenField ID="hdnRole" Value="0" runat="server" />
</asp:Content>
