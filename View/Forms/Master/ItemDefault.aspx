<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true" CodeFile="ItemDefault.aspx.cs" Inherits="View_Forms_Master_ItemDefault" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    <script type="text/javascript">
     function UpdatingInfo(btnid)
     {
     
        var myStringtest = btnid;
        var mySplitResulttest = myStringtest.split("_");
        var Code =  mySplitResulttest[0]+'_'+mySplitResulttest[1]+'_'+mySplitResulttest[2]+'_'+mySplitResulttest[3];
        Code=Code+'_hdnCode';  
        Code=document.getElementById(Code).value;
        var GroupID =  mySplitResulttest[0]+'_'+mySplitResulttest[1]+'_'+mySplitResulttest[2]+'_'+mySplitResulttest[3];
        GroupID=GroupID+'_hdnGroupID';  
        GroupID=document.getElementById(GroupID).value;
        
        var Path='<%=strProjectName%>/View/Forms/Master/Item.aspx?Code='+Code+'&GroupID='+GroupID;
        window.location.href=Path;
       
     }
     
     function DeletingInfo(btnid)
    {
       
 
        var check=confirm('Are you sure you want to delete this record?');
        if(check)
        {
            var myStringtest = btnid;
            var mySplitResulttest = myStringtest.split("_");
            var Code =  mySplitResulttest[0]+'_'+mySplitResulttest[1]+'_'+mySplitResulttest[2]+'_'+mySplitResulttest[3];
            Code=Code+'_hdnCode'; 
           
            Code=document.getElementById(Code).value;
            document.getElementById('ctl00_ContentPlaceHolder1_hdnCodeID').value=Code;
        }
       
        return check
   
   }
    </script>
 <fieldset class="sectionBorder">
        <legend>Item Detail</legend>
    <div align="right" style="margin-right:4%;">
        <asp:Button ID="btnAdd" runat="server" CausesValidation="false" ToolTip="Add" Text="Add"
            Width="100px" OnClick="btnAdd_Click"></asp:Button>
    </div>
    <br />
     <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
    <div style="margin-left:1%;width:100%;" >
    
    <asp:GridView AutoGenerateColumns="false" OnRowCreated="gridView_RowCreated" OnRowDeleting="Role_RowDeleted" Width="100%"  OnDataBound="eventhandlerSerialNo"
        ID="gridDefault" runat="server">
       <Columns>
            <asp:BoundField HeaderText="#" ItemStyle-HorizontalAlign="Center" ReadOnly="True" />
            <asp:BoundField DataField="Code"
                HeaderText="Code" ReadOnly="True" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="ItemGroupName"  
                HeaderText="Group Name" ReadOnly="True" />
            <asp:BoundField DataField="Description"
                HeaderText="Description" ReadOnly="True" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left" />
            <asp:BoundField DataField="EffectDate"  
                HeaderText="Effective Date" DataFormatString="{0:dd-MM-yyyy}"
                    HtmlEncode="False" ReadOnly="True" ItemStyle-HorizontalAlign="Center" />
            
            <asp:TemplateField HeaderText="Is Active">
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:CheckBox ID="chkActive" runat="server" Enabled="false" Checked='<%#Bind("IsActive") %>' />
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="In Report">
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:CheckBox ID="chkInReport" runat="server" Enabled="false" Checked='<%#Bind("InReport") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Update" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:HiddenField ID="hdnCode" runat="server" Value='<%#Bind("Code") %>' />
                       <asp:HiddenField ID="hdnGroupID" runat="server" Value='<%#Bind("GroupID") %>' />
                    <img alt="" src='~/images/icon-edit.gif' height='15' width='16' style="cursor: pointer;"
                        id="imageButton" title='Update' onclick='UpdatingInfo(this.id)' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkbtnDelete" OnClientClick="return DeletingInfo(this.id)" runat="server"
                        CommandName="Delete">
                        <asp:Image ID="Image1" ImageUrl="~/images/icon-delete.gif" ToolTip="Delete" AlternateText="Delete"
                            BorderStyle="None" runat="server" /></asp:LinkButton>
                </ItemTemplate>
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
      <asp:Literal ID="Literal1" runat="server"></asp:Literal>
    <asp:HiddenField ID="hdnCodeID" runat="server" />
</asp:Content>

