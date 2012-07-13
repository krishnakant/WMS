<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true" CodeFile="ShowCVoiceCodeByGroup.aspx.cs" Inherits="View_Forms_Configurator_ShowCVoiceCodeByGroup" Title="WMS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
function ClientValidateGRoupList(source, arguments){
      if (document.getElementById('ctl00_ContentPlaceHolder1_ddlGRoupList').value!=0)
         arguments.IsValid=true;
      else
         arguments.IsValid=false;
   }


    </script>
<fieldset class="sectionBorder">
<legend>Show Customer Voice Code By Group </legend>
    <table width="90%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td class="cssLabel">
                <asp:Label ID="lblGroupName" runat="server" Text="Group&nbsp;Name:"></asp:Label></td>
            <td>
                <asp:DropDownList ID="ddlGRoupList" runat="server">
                </asp:DropDownList>
                   <asp:CustomValidator ID="CustomValidator" ControlToValidate="ddlGRoupList" runat="server"
                    ClientValidationFunction="ClientValidateGRoupList" ErrorMessage="select group"></asp:CustomValidator>
                <asp:Button ID="btnGo" ToolTip="Go" Text="Go" runat="server" OnClick="btnGo_Click" />
                </td>
          
        </tr>
        <tr>
            <td class="cssLabel">
                <asp:Label ID="lblItemCode" runat="server" Text="Code:"></asp:Label>
            </td>
            <td colspan="3">
             <asp:Panel Height="120px" BorderWidth="1px" ID="pnlItemCodeList" runat="server" ScrollBars="Vertical">
                <asp:Label ID="lblMessage" runat="server" ForeColor="red" Font-Bold="true"  Text=""></asp:Label>
                <asp:CheckBoxList  ID="chkCodeList" ToolTip="select Code" CellSpacing="5"
                    RepeatColumns="3" runat="server">
                </asp:CheckBoxList>
                </asp:Panel>
                    <input type="checkbox" checked id="chkSelectAll" onclick="javascript:SetAllCheckBoxes('ctl00_ContentPlaceHolder1_chkCodeList',this.id);" />Select All
            </td>
        </tr>
    </table>
      <div class="cssButtonPanel">
        <asp:Button ID="btnSave"  ToolTip="Save" Text="Save"  runat="server" OnClick="btnSave_Click"  />
    </div>
    </fieldset>
    <asp:Literal ID="literal1" runat="server"></asp:Literal>
</asp:Content>


