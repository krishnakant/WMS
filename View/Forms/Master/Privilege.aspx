<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true"
    CodeFile="Privilege.aspx.cs" Inherits="View_Forms_Master_Privilege" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
function ClientValidateRole(source, arguments)
{
      if (document.getElementById('ctl00_ContentPlaceHolder1_ddlRole').value!=0)
         arguments.IsValid=true;
      else
         arguments.IsValid=false;
}    

   function setValuesForSelection(idSelectAll)
   {
 var rows="<%=(selectValue)%>",j=2;
  
       var j = 2;
        var s='ctl0'+j;
       
       if(document.getElementById('ctl00_ContentPlaceHolder1_gdPrivilege_ctl01_chkApplyToAll').checked)
       {
          for(var i=0;i< rows;i++)
           {
              if(j<14)
              {
                  s='ctl0'+j;
              }
              else
              {
                  s='ctl'+j;
              }
             var t='ctl00_ContentPlaceHolder1_gdPrivilege_'+s+'_chkviewing';
             
              if( document.getElementById(idSelectAll).checked)
              {
                  document.getElementById(t).checked=true;
//                 document.getElementById(t).parentElement.parentElement.style.backgroundColor='#88AAFF';
              }
              else
              {
                  document.getElementById(t).checked=false;
//              document.getElementById(t).parentElement.parentElement.style.backgroundColor='white';
              }
              j++;
           }  
        }
        else
        {
            for(var i=0;i<rows;i++)
            {
                  if(j<10)
                  {
                      s='ctl0'+j;
                  }
                  else
                  {
                      s='ctl'+j;
                  }
                  var t='ctl00_ContentPlaceHolder1_gdPrivilege_'+s+'_chkviewing';
//                  if(document.getElementById(t).checked==true)
//                  {
                      document.getElementById(t).checked=false;
//                  }
                  j++; 
             }
         }
     } 
         
    </script>

    <fieldset class="sectionBorder">
        <legend>Privilege</legend>
        <div style="overflow: auto; height: 400px; width: 950px;">
            <center>
                <table>
                    <tr>
                        <td class="cssLabel">
                            <asp:Label ID="lblRole" runat="server" Text="Role:"></asp:Label></td>
                        <td>
                            <asp:DropDownList ID="ddlRole" ToolTip="Role" runat="server" OnSelectedIndexChanged="ddlRole_SelectedIndexChanged"
                                AutoPostBack="true">
                            </asp:DropDownList>
                            <asp:CustomValidator ID="CustomValidator" ErrorMessage="Select the Role" ClientValidationFunction="ClientValidateRole"
                                ControlToValidate="ddlRole" runat="server"></asp:CustomValidator>
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="gdPrivilege" AutoGenerateColumns="false" OnRowCreated="gridView_RowCreated"
                    OnDataBound="eventhandlerSerialNo" runat="server">
                    <Columns>
                        <asp:BoundField HeaderText="#" ReadOnly="True">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FormCaption" HeaderText="Form">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="viewing">
                            <HeaderTemplate>
                                <input type="checkbox" id="chkApplyToAll" title="select All" runat="server" onclick="javascript:setValuesForSelection(this.id);" />View
                                All
                            </HeaderTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:HiddenField ID="hdnFormID" runat="server" Value='<%#Bind("FormID") %>' />
                                <asp:CheckBox ID="chkviewing" runat="server" Checked='<%#Bind("viewing") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:CheckBox ID="chkviewingEdit" runat="server" Checked='<%#Bind("viewing") %>' />
                            </EditItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <RowStyle ForeColor="#000066" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
            </center>
        </div>
    </fieldset>
    <div style="float: right" class="cssButtonPanel">
        <asp:Button ID="btnSave" CausesValidation="true" Text="Save" OnClick="btnSave_Click"
            runat="server" ToolTip="Save" />
        <asp:Button ID="btnCancel" CausesValidation="false" Text="Cancel" runat="server"
            ToolTip="Cancel" />
    </div>
</asp:Content>
