<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true"
    CodeFile="RoleDefault.aspx.cs" Inherits="View_Forms_Master_RoleDefault" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
 function UpdatingInfo(btnid)
     {
     
        var myStringtest = btnid;
        var mySplitResulttest = myStringtest.split("_");
        var RoleID =  mySplitResulttest[0]+'_'+mySplitResulttest[1]+'_'+mySplitResulttest[2]+'_'+mySplitResulttest[3];
       RoleID=RoleID+'_hdnRoleID';  
         RoleID=document.getElementById(RoleID).value;
        var Path='<%=strProjectName%>/View/Forms/Master/Role.aspx?RoleID='+RoleID;
        window.location.href=Path;
       
     }
     
      function DeletingInfo(btnid)
    {
       
 
        var check=confirm('Are you sure you want to delete this record?');
        if(check)
        {
            var myStringtest = btnid;
            var mySplitResulttest = myStringtest.split("_");
            var RoleID =  mySplitResulttest[0]+'_'+mySplitResulttest[1]+'_'+mySplitResulttest[2]+'_'+mySplitResulttest[3];
            RoleID=RoleID+'_hdnRoleID'; 
            RoleID=document.getElementById(RoleID).value;
            document.getElementById('ctl00_ContentPlaceHolder1_hdnRoID').value=RoleID;
        }
       
        return check
   
   }
    </script>

    <fieldset class="sectionBorder">
        <legend>Role</legend>
        <div align="right" style="margin-right: 4%;">
            <asp:Button ID="btnAdd" Text="Add" ToolTip="Add" OnClick="btnAdd_Click" runat="server" />
        </div>
        <br />
        <div style="margin-left: 1%; width: 100%;">
            <asp:GridView ID="GridView1" runat="server" OnRowDeleting="Role_RowDeleted" OnDataBound="eventhandlerSerialNo"
                AutoGenerateColumns="false" OnRowCreated="gridView_RowCreated">
                <Columns>
                    <asp:BoundField HeaderText="#" ReadOnly="True">
                        <ItemStyle HorizontalAlign="center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Role" HeaderText="Role Name"></asp:BoundField>
                    <asp:TemplateField HeaderText="Is Active">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:CheckBox ID="chkIsActive" runat="server" Enabled="false" Checked='<%#Bind("IsActive") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Edit">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:HiddenField ID="hdnRoleID" runat="server" Value='<%#Bind("RoleID") %>' />
                            <img alt="" src='~/images/icon-edit.gif' height='15' width='16' style="cursor: pointer;"
                                id="imageButton" title='Update' onclick='UpdatingInfo(this.id)' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkbtnDelete" runat="server" OnClientClick="return DeletingInfo(this.id)"
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
        </div>
    </fieldset>
    <asp:HiddenField ID="hdnRoID" runat="server" />
</asp:Content>
