<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true"
    CodeFile="ShowProductCodebyGroup.aspx.cs" Inherits="View_Forms_Configurator_ShowProductCodebyGroup"
     %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script type="text/javascript">
 function ClientValidateModel(source, arguments){
      if (document.getElementById('ctl00_ContentPlaceHolder1_drpModel').value!=0)
         arguments.IsValid=true;
      else
         arguments.IsValid=false;
   }
</script>
<fieldset class="sectionBorder"><legend>Show Product Code By Group</legend>
    <table width="90%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td class="cssLabel">
                <asp:Label ID="lblGroupName" runat="server" Text="GroupName:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="drpModel"  runat="server">
                </asp:DropDownList>
                <asp:CustomValidator ID="CustomValidator" ControlToValidate="drpModel" runat="server" ClientValidationFunction="ClientValidateModel" ErrorMessage="Select Group Name"></asp:CustomValidator>
            <asp:Button ID="btnGo" Text="Go" runat="server" OnClick="btnGo_Click" ToolTip="Go" />
            </td>
            
        </tr>
        <tr>
        <td class="cssLabel">
                <asp:Label ID="lblItemCode" runat="server" Text="Code:"></asp:Label>
            </td>
            <td colspan="2">
             <asp:Panel Height="150px" BorderWidth="1px"  ID="pnlItemCodeList" runat="server" ScrollBars="Vertical">
                 <asp:Label ID="lblMessage" runat="server" ForeColor="red" Font-Bold="true"  Text=""></asp:Label>
                <asp:CheckBoxList ID="chkProductCode" ToolTip="select Code" RepeatColumns="3" runat="server">
                </asp:CheckBoxList>
                    </asp:Panel>
                     <input type="checkbox"  checked id="chkSelectAll" onclick="javascript:SetAllCheckBoxes('ctl00_ContentPlaceHolder1_chkProductCode',this.id);" />Select
                    All
                </td>
                
        </tr>
    </table>
    <div class="cssButtonPanel">
       <asp:Button ID="btnSave" Enabled="false" Text="Save" runat="server"
            ToolTip="Save" OnClick="btnSave_Click" />
    </div>
    </fieldset>
      <asp:Literal ID="literal1" runat="server"></asp:Literal>
</asp:Content>
