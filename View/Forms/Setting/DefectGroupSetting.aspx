<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true" CodeFile="DefectGroupSetting.aspx.cs" Inherits="View_Forms_Setting_DefectGroupSetting"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
 function DeletingInfo(btnid)
    {
       
 
        var check=confirm('Are you sure you want to delete this record?');
        if(check)
        {
            var myStringtest = btnid;
            var mySplitResulttest = myStringtest.split("_");
            var GroupID=  mySplitResulttest[0]+'_'+mySplitResulttest[1]+'_'+mySplitResulttest[2]+'_'+mySplitResulttest[3];
            GroupID=GroupID+'_hdnGroupID'; 
            GroupID=document.getElementById(GroupID).value;
            document.getElementById('ctl00_ContentPlaceHolder1_hdngupID').value=GroupID;
        }
       
        return check
     }   
</script>
<fieldset class="sectionBorder"><legend>Defect Group Setting</legend>
    <table>
        <tr>
            <td class="cssLabel">
                <asp:Label ID="lblGroupName" runat="server" Text="Defect Group Name:"></asp:Label>
            </td>
            <td id="trtxtGroupName">
                <asp:TextBox ID="txtGroupName" ToolTip="Defect Group Name" Width="120px" MaxLength="50" runat="server"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator" ControlToValidate="txtGroupName"
                            ErrorMessage="*" runat="server">*</asp:RequiredFieldValidator>
                 <asp:Button ID="btnSave" Text="Save" runat="server" ToolTip="Save" OnClick="btnSave_Click" /></td>
           
        </tr>
        
      
    </table>
    <asp:Label ID="lblMessage" runat="server" ForeColor="red" Font-Bold="true" Text=""></asp:Label>
    <asp:GridView ID="GridView1" OnRowCreated="gridView_RowCreated" DataKeyNames="DefectGroupID,GroupName" OnRowUpdating="Record_Updating" OnDataBound="eventhandlerSerialNo" OnRowCancelingEdit="Record_cancel"  OnRowDeleting="Role_RowDeleted" OnRowEditing="Record_Edit"
        AutoGenerateColumns="false" runat="server">
        <Columns>
            <asp:BoundField HeaderText="#" ReadOnly="True">
                <ItemStyle HorizontalAlign="center" />
            </asp:BoundField>
          
            
             <asp:TemplateField HeaderText="Group Name">
                    <ItemTemplate>
                        <asp:Label ID="lblName" runat="server" Text='<%#Bind("GroupName")%>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtGroupName" Text='<%#Bind("GroupName")%>' runat="server"></asp:TextBox>
                    </EditItemTemplate>
                      
                </asp:TemplateField>
                
            <asp:TemplateField HeaderText="Update">
                <ItemTemplate>
                <asp:HiddenField ID="hdnGroupID" runat="server" Value='<%#Bind("DefectGroupID") %>' />
                    <asp:LinkButton ID="LinkButtonUpdate" CausesValidation="false" runat="Server" CommandName="Edit">
                        <asp:Image ID="Image1Update" ImageUrl="~/images/icon-edit.gif" ToolTip="Edit" AlternateText="Edit"
                            BorderStyle="None" runat="server" />
                    </asp:LinkButton>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:LinkButton ID="LinkButtonEdit" CausesValidation="false" runat="Server" CommandName="Update">
                        <asp:Image ID="Image1Edit" ImageUrl="~/images/icon-edit.gif" ToolTip="Update" AlternateText="Update"
                            BorderStyle="None" runat="server" />
                    </asp:LinkButton>
                    <asp:LinkButton ID="LinkButtonCancel" CausesValidation="false" runat="Server" CommandName="Cancel">
                        <asp:Image ID="ImageCancel" ImageUrl="~/images/icon-delete.gif" ToolTip="Cancel"
                            AlternateText="Cancel" BorderStyle="None" runat="server" />
                    </asp:LinkButton>
                </EditItemTemplate>
                <ItemStyle HorizontalAlign="center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Delete">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkbtnDelete" runat="server" CausesValidation="false" OnClientClick="return DeletingInfo(this.id)"
                        CommandName="Delete" ToolTip="Delete">
                        <asp:Image ID="Image1" ImageUrl="~/images/icon-delete.gif" AlternateText="Delete"
                            BorderStyle="None" runat="server" ToolTip="Delete" /></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="White" ForeColor="#000066" />
        <RowStyle ForeColor="#000066" />
        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
    </asp:GridView>
    <asp:Literal ID="literal1" runat="server"></asp:Literal>
    <asp:HiddenField ID="hdngupID" runat="server" />
   </fieldset>
</asp:Content>

