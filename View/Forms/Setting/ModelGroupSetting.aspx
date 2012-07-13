<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true"
    CodeFile="ModelGroupSetting.aspx.cs" Inherits="View_Forms_Setting_ModelGroupSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
     
     function getIndex(val)
     {
        document.getElementById('ctl00_ContentPlaceHolder1_hdnIndex').value=val;
     }
    </script>

    <fieldset class="sectionBorder">
        <legend>Model Group Setting</legend>
        <table>
            <tr>
                <td class="cssLabel">
                    <asp:Label ID="lblGroupName" runat="server" Text="Model Group Name:"></asp:Label>
                </td>
                <td id="trtxtGroupName">
                    <asp:TextBox ID="txtGroupName" ToolTip="Model Group Name" Width="120px" MaxLength="50"
                        runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator" ControlToValidate="txtGroupName"
                        ErrorMessage="*" runat="server">*</asp:RequiredFieldValidator>
                    <asp:Button ID="btnSave" Text="Save" runat="server" ToolTip="Save" OnClick="btnSave_Click" />
                </td>
            </tr>
            <tr>
                <td class="cssLabel">
                    <asp:Label ID="lblIsNew" runat="server" Text="IsNew:"></asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="chkIsNew" Checked="true" runat="server" ToolTip="IsNew" />
                </td>
            </tr>
            <tr>
                <td class="cssLabel">
                    <asp:Label ID="lblWarrantyPeriod" runat="server" Text="Warranty Period:"></asp:Label>
                </td>
                <td>
                    <asp:RadioButtonList ID="rdoWarrantyPeriod" runat="server" ToolTip="Warranty Period"
                        RepeatDirection="Horizontal">
                        <asp:ListItem Selected="true" Value="1" onclick="getIndex(this.value);">1 Year</asp:ListItem>
                        <asp:ListItem Value="2" onclick="getIndex(this.value);">2 Years</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
        </table>
        <asp:Label ID="lblMessage" runat="server" ForeColor="red" Font-Bold="true" Text=""></asp:Label>
        <asp:GridView ID="GridView1" OnRowCreated="gridView_RowCreated" DataKeyNames="GroupID,ModelGroupName"
            OnRowUpdating="Record_Updating" OnDataBound="eventhandlerSerialNo" OnRowCancelingEdit="Record_cancel"
            OnRowDeleting="Role_RowDeleted" OnRowEditing="Record_Edit" AutoGenerateColumns="false"
            runat="server">
            <Columns>
                <asp:BoundField HeaderText="#" ReadOnly="True">
                    <ItemStyle HorizontalAlign="center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Model Group Name">
                    <ItemTemplate>
                        <asp:Label ID="lblName" runat="server" Text='<%#Bind("ModelGroupName")%>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtModelGroupName" Text='<%#Bind("ModelGroupName")%>' runat="server"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Is New">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:CheckBox ID="chkIsNew" runat="server" Enabled="false" Checked='<%#Bind("IsNew") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:CheckBox ID="chkIsNewEdit" runat="server" Checked='<%#Bind("IsNew") %>' />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Warranty Period">
                    <ItemTemplate>
                        <asp:Label ID="lblWarrantyPeriod" runat="server" Text='<%#Bind("WarrantyPeriod")%>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtWarrantyPeriod" Text='<%#Bind("WarrantyPeriod")%>' runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Update">
                    <ItemTemplate>
                        <asp:HiddenField ID="hdnGroupID" runat="server" Value='<%#Bind("GroupID") %>' />
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
                        <asp:LinkButton ID="lnkbtnDelete" CausesValidation="false" runat="server" OnClientClick="return DeletingInfo(this.id)"
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
        <asp:HiddenField ID="hdnIndex" runat="server" Value="0" />
    </fieldset>
</asp:Content>
