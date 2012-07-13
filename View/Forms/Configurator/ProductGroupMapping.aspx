<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true"
    CodeFile="ProductGroupMapping.aspx.cs" Inherits="View_Forms_Configurator_ProductGroupMapping"
     %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
function getRadioSelection(id)
{
   if(id=='ctl00_ContentPlaceHolder1_rdoNew')
   {
      document.getElementById('tddrpModel').style.display='none';
      document.getElementById('trtxtGroupName').style.display='';
   }
    if(id=='ctl00_ContentPlaceHolder1_rdoExisting')
   {
      document.getElementById('trtxtGroupName').style.display='none';
      document.getElementById('tddrpModel').style.display='';
   }
}
function checkValidation()
{
  
   var check=getCheckBoxStatus('ctl00_ContentPlaceHolder1_chkProductCode');
   if(!check){alert('please select Product Code'); return false;}
 
   if(document.getElementById('ctl00_ContentPlaceHolder1_rdoExisting').checked)
   {
      var ModelID=document.getElementById('ctl00_ContentPlaceHolder1_drpModel').value;
      if(ModelID==''){ModelID=0;}
      if(ModelID==0)
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
        <legend>Product Group Mapping</legend>
        <table width="90%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="cssLabel">
                    <asp:Label ID="lblProductCode" runat="server" Text="Product Code:"></asp:Label>
                </td>
                <td colspan="2">
                    <asp:Panel Height="150px" BorderWidth="1px"  ID="pnlItemCodeList" runat="server" ScrollBars="Vertical">
                        <asp:CheckBoxList RepeatColumns="3" ID="chkProductCode" CellSpacing="5" ToolTip="select Product Code" runat="server">
                        </asp:CheckBoxList>
                    </asp:Panel>
                    <input type="checkbox" id="chkSelectAll" onclick="javascript:SetAllCheckBoxes('ctl00_ContentPlaceHolder1_chkProductCode',this.id);" />Select
                    All
                </td>
            </tr>
            <tr>
                <td class="cssLabel">
                    <asp:Label ID="Assign" runat="server" Text="Assign:"></asp:Label>
                </td>
                <td colspan="2">
                    <asp:RadioButton ID="rdoExisting" onclick="javascript:getRadioSelection(this.id);" GroupName="Assign"
                        Text="Existing" runat="server" />
                    <asp:RadioButton ID="rdoNew" Checked="true" onclick="javascript:getRadioSelection(this.id);"
                        GroupName="Assign" Text="New" runat="server" /></td>
            </tr>
            <tr>
                <td class="cssLabel">
                    <asp:Label ID="lblGroupName" runat="server" Text="GroupName:"></asp:Label>
                </td>
                <td id="trtxtGroupName">
                    <asp:TextBox ID="txtGroupName" Width="120px"  MaxLength="50" runat="server"></asp:TextBox></td>
                <td id="tddrpModel" style="display: none">
                    <asp:DropDownList ID="drpModel" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="cssLabel">
                    <asp:Label ID="lblMessage" runat="server" ForeColor="red" Font-Bold="true" Text=""></asp:Label></td>
            </tr>
        </table>
         <div class="cssButtonPanel">
        <asp:Button OnClientClick="return checkValidation();" ID="btnSave" Text="Save" runat="server"
            ToolTip="Save" OnClick="btnSave_Click" />
    </div>
    </fieldset>
   
    <asp:Literal ID="literal1" runat="server"></asp:Literal>
</asp:Content>
