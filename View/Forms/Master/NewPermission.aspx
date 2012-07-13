<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true"
    CodeFile="NewPermission.aspx.cs" Inherits="View_Forms_Master_NewPermission"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
   
  

function ClientValidateEmployee(source, arguments)
{
      if (document.getElementById('ctl00_ContentPlaceHolder1_ddlRole').value!=0)
         arguments.IsValid=true;
      else
         arguments.IsValid=false;
}



    </script>

    <fieldset class="sectionBorder">
        <legend>New permission</legend>
        <table>
            <tr>
                <td align="center">
                    <asp:Label ID="lblRole" runat="server" Text="Role:"></asp:Label>
                    <asp:DropDownList ID="ddlRole" ToolTip="Role" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRole_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:CustomValidator ID="CustomValidator" ErrorMessage="Select the Role" ClientValidationFunction="ClientValidateEmployee"
                        ControlToValidate="ddlRole" runat="server"></asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <h6>
                        <asp:Label ID="lblModule" runat="server" Text="Module"></asp:Label></h6>
                    <asp:Panel BorderWidth="1px"  ID="PnlModule" runat="server" BorderColor="#00678e"
                        ScrollBars="Vertical">
                        <asp:CheckBoxList ID="ChkModule"  ToolTip="select Module" RepeatColumns="5"
                            runat="server">
                        </asp:CheckBoxList>
                    </asp:Panel>
                    <input type="checkbox" id="chkModuleAll" checked="checked" onclick="javascript:SetAllCheckBoxes('ctl00_ContentPlaceHolder1_ChkModule',this.id);" />Select
                    All
                    <asp:Button ID="btnShow" Text="Show Form" CausesValidation="false" OnClick="btnShow_Click"
                        ToolTip="ShowForm" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <h6>
                        <asp:Label ID="lblForm" runat="server" Text="Form"></asp:Label></h6>
                    <asp:Panel BorderWidth="1px" ID="PnlForm" runat="server" BorderColor="#00678e" ScrollBars="Vertical">
                        <asp:CheckBoxList ID="chkForm" ToolTip="select Form"   RepeatColumns="5"
                            runat="server">
                        </asp:CheckBoxList>
                    </asp:Panel>
                    <input type="checkbox" id="chkFormALL" onclick="javascript:SetAllCheckBoxes('ctl00_ContentPlaceHolder1_chkForm',this.id);" />Select
                    All
                </td>
            </tr>
        </table>
        <asp:Label ID="lblMessage" runat="server" ForeColor="red"></asp:Label>
    </fieldset>
    <div class="cssButtonPanel">
        <asp:Button ID="btnSave" CausesValidation="true" Text="Save" ToolTip="Save" runat="server" OnClick="btnSave_Click" />
        <span style="margin-left: 1%;"></span>
        <asp:Button ID="btncencle" CausesValidation="false" Text="cancel" ToolTip="cancel"
            runat="server" />
    </div>
    <asp:Literal ID="literal1" runat="server"></asp:Literal>
</asp:Content>
