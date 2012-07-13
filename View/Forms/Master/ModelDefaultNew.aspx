<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true"
    CodeFile="ModelDefaultNew.aspx.cs" Inherits="View_Forms_Master_ModelDefaultNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
     function UpdatingInfo(btnid)
     {
     
        var myStringtest = btnid;
        var mySplitResulttest = myStringtest.split("_");
        var Code =  mySplitResulttest[0]+'_'+mySplitResulttest[1]+'_'+mySplitResulttest[2]+'_'+mySplitResulttest[3];
        Code=Code+'_hdnCode';  
        Code=document.getElementById(Code).value;
        
        
        var Path='<%=strProjectName%>/View/Forms/Master/ModelNew.aspx?Code='+Code;
        window.location.href=Path;
       
     }
     
     function fnSetLabelText(ID,Text)
        {
           document.getElementById(ID).innerHTML =Text ;
           setTimeout("setMessageText('"+ID+"','')",3000);
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
        <legend>Model Detail</legend>
        <div align="right" style="margin-right: 4%;" class="cssButtonPanel">
            <asp:Button ID="btnAdd" runat="server" CausesValidation="false" ToolTip="Add" Text="Add"
                Width="100px" OnClick="btnAdd_Click"></asp:Button>
        </div>
        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
        <div style="margin-left: 1%; width: 100%;">
            <asp:GridView AutoGenerateColumns="false" OnRowCreated="gridView_RowCreated" OnRowDeleting="Model_RowDeleted"
                Width="100%" OnDataBound="eventhandlerSerialNo"  DataKeyNames="ModelMappingID" ID="gridDefault" runat="server">
                <Columns>
                    <asp:BoundField HeaderText="#" ItemStyle-HorizontalAlign="Center" ReadOnly="True" />
                    <asp:BoundField DataField="Material" HeaderText="Material" ReadOnly="True" />
                    <asp:BoundField DataField="ModelGroupName" HeaderText="Model" ReadOnly="True" />
                    <asp:BoundField DataField="ModelCategory" HeaderText="Category" ReadOnly="True" />
                    <asp:BoundField DataField="ModelSpecial" HeaderText="Special" ReadOnly="True" />
                    <asp:BoundField DataField="ClutchType" HeaderText="Clutch Type" ReadOnly="True" />
                    <asp:BoundField DataField="Description" HeaderText="Description" ReadOnly="True"
                        ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left" />
                    <asp:TemplateField HeaderText="Status">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:CheckBox ID="chkActive" runat="server" Enabled="false" Checked='<%#Bind("IsActive") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                  
                    <asp:TemplateField HeaderText="Update" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:HiddenField ID="hdnCode" runat="server" Value='<%#Bind("ModelMappingID") %>' />
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
    <asp:Label ID="lblmsg" runat="server" ForeColor="red"></asp:Label>
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
    <asp:HiddenField ID="hdnCodeID" runat="server" />
</asp:Content>
