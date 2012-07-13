<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true"
    CodeFile="ItemException.aspx.cs" Inherits="View_Forms_Exceptions_ItemException"
    Title="WMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
function getradioSelection(id)
{
   if(id=='ctl00_ContentPlaceHolder1_rdoNew')
   {
      document.getElementById('tdDropdownList').style.display='none';
      document.getElementById('tdTextBox').style.display='';
   }
    if(id=='ctl00_ContentPlaceHolder1_rdoExixts')
   {
      document.getElementById('tdTextBox').style.display='none';
      document.getElementById('tdDropdownList').style.display='';
   }
}
function checkValidation()
{
  
   var check=getCheckBoxStatus('ctl00_ContentPlaceHolder1_chkItemCodeList');
   if(!check){alert('please select Item Code'); return false;}
 
   if(document.getElementById('ctl00_ContentPlaceHolder1_rdoExixts').checked)
   {
      var ItemGRoupList=document.getElementById('ctl00_ContentPlaceHolder1_ddlItemGRoupList').value;
      if(ItemGRoupList==''){ItemGRoupList=0;}
      if(ItemGRoupList==0)
      {
        alert('please select group name'); return false;
      }
      else
      {
           return true;
      }
   }
   else
   {
      if(document.getElementById('ctl00_ContentPlaceHolder1_txtGroupName').value=='')
      {
        alert('please type group name'); return false;
      }
      else
      {
           return true;
      }
   }
}

    </script>
<fieldset class="sectionBorder">
<legend>Item Exceptions</legend>


    <table>
        <tr>
            <td class="cssLabel">
                <asp:Label ID="lblItemCode" runat="server" Text="Code:"></asp:Label>
            </td>
            <td colspan="2">
            <asp:Panel Height="200px" BorderWidth="1px" ID="pnlItemCodeList" runat="server" ScrollBars="Vertical">
                <asp:CheckBoxList ID="chkItemCodeList" CellSpacing="5" ToolTip="select Item Code" RepeatColumns="5" runat="server">
                </asp:CheckBoxList>
                </asp:Panel>
                <input type="checkbox" id="chkSelectAll" onclick="javascript:SetAllCheckBoxes('ctl00_ContentPlaceHolder1_chkItemCodeList',this.id);" />Select All
            </td>
        </tr>
        <tr>
            <td class="cssLabel">
                Assign
            </td>
            <td colspan="2">
                <asp:RadioButton ID="rdoExixts" ToolTip="Existsing" onclick="javascript:getradioSelection(this.id);" GroupName="Assign"
                    runat="server" />Existing
                <asp:RadioButton ID="rdoNew"  ToolTip="New" Checked="true" onclick="javascript:getradioSelection(this.id);" GroupName="Assign"
                    runat="server" />New
            </td>
        </tr>
        <tr>
        <td  class="cssLabel">   <asp:Label ID="lblGroupName" runat="server" Text="Group Name:"></asp:Label></td>
         <td id="tdDropdownList" style="display:none;">
             <asp:DropDownList ID="ddlItemGRoupList" runat="server">
             </asp:DropDownList>
         </td>
         <td id="tdTextBox">
         <asp:TextBox ID="txtGroupName" runat="server" ToolTip="Group Name" Width="120px" MaxLength="50"></asp:TextBox>
         </td>
         <td>
          <asp:Label ID="lblMessage" runat="server" ForeColor="red" Font-Bold="true"  Text=""></asp:Label>
         </td>
        </tr>
    </table>
     <div class="cssButtonPanel">
        <asp:Button ID="btnSave" CausesValidation="true"  OnClientClick="return checkValidation();" ToolTip="Save" Text="Save"  runat="server" OnClick="btnSave_Click" />
    </div>
    </fieldset>
    <asp:Literal ID="literal1" runat="server"></asp:Literal>
</asp:Content>
