<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true"
    CodeFile="UserDefault.aspx.cs" Inherits="View_Forms_Master_UserDefault" Title="WMS" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
 function UpdatingInfo(btnid)
     {
     
        var myStringtest = btnid;
        var mySplitResulttest = myStringtest.split("_");
        var UserID =  mySplitResulttest[0]+'_'+mySplitResulttest[1]+'_'+mySplitResulttest[2]+'_'+mySplitResulttest[3];
      UserID=UserID+'_hdnUserID';  
        UserID=document.getElementById(UserID).value;
        var Path='<%=strProjectName%>/View/Forms/Master/User.aspx?UserID='+UserID;
        window.location.href=Path;
       
     }
     
      function DeletingInfo(btnid)
    {
       
 
        var check=confirm('Are you sure you want to delete this record?');
        if(check)
        {
            var myStringtest = btnid;
            var mySplitResulttest = myStringtest.split("_");
            var UserID =  mySplitResulttest[0]+'_'+mySplitResulttest[1]+'_'+mySplitResulttest[2]+'_'+mySplitResulttest[3];
           UserID=UserID+'_hdnUserID'; 
           UserID=document.getElementById(UserID).value;
            document.getElementById('ctl00_ContentPlaceHolder1_hdnUrsID').value=UserID;
        }
       
        return check
        
        
   
   }
   
    function UpdatingPassword(btnid)
     {
     
        var myStringtest = btnid;
        var mySplitResulttest = myStringtest.split("_");
        var UserID =  mySplitResulttest[0]+'_'+mySplitResulttest[1]+'_'+mySplitResulttest[2]+'_'+mySplitResulttest[3];
        UserID=UserID+'_hdnUserID';  
        UserID=document.getElementById(UserID).value;
        var Path='<%=strProjectName%>/View/Forms/Master/UpdatePasswordByAdmin.aspx?UserID='+UserID;
        window.location.href=Path;
       
     }
   
    function CallExport(strid)
   {
       var prtContent = document.getElementById(strid);
        prtContent = ReplaceTag(prtContent.innerHTML);
        alert(prtContent);
        document.getElementById('ctl00_ContentPlaceHolder1_hdnExport').value=prtContent;
     
   }
   
   
    function ReplaceTag(printtext)
{
//Remove TextArea Tag by ''
var re = /(<TEXTAREA([^>]+)>)/gi;
printtext=printtext.replace(re,'');
//Remove Link  Tag by ''

re =/(<A([^>]+)>)/gi;
printtext=printtext.replace(re,'');

printtext=printtext.replace(/<\/TEXTAREA>/g,'');
printtext=printtext.replace(/<\/A>/g,'');
//Remove IMG Tag by ''

 re =/(<input ([^>]+)>)/gi;
printtext=printtext.replace(re,'');
re =/(<IMG ([^>]+)>)/gi;
printtext=printtext.replace(re,'');
re =/(<asp:Image ([^>]+)>)/gi;
printtext=printtext.replace(re,'');
re =/(<SPAN ([^>]+)>)/gi;
printtext=printtext.replace(re,'');
printtext=printtext.replace(/<\/SPAN>/g,'');
re =/(<INPUT CLASS=BORDER ([^>]+)>)/gi;
printtext=printtext.replace(re,'');
//Remove Date validation  Error Msg by ''
printtext=printtext.replace(/\(dd-mm-yyyy\)/g,'');
re =/(<TR style="CURSOR: ([^>]+)>)/gi;
printtext=printtext.replace(re,'<TR>');
printtext=printtext.replace(/Actual date must be less than Today/g,'\'');
printtext=printtext.replace(/''s Date/g,'');

printtext=printtext.replace(/View <\/TD>/g,'<\/TD>');
//Set All TD Value To Middle
printtext=printtext.replace(/<TD align=middle/g,'<TD');

printtext=printtext.replace(/<TD/g,'<TD align=middle');
//Replace IMages for Pages Navigation on Grid
re=/(<INPUT style="BORDER-TOP-WIDTH: ([^>]+)>)/gi;
printtext=printtext.replace(re,'');
re=/(<SELECT ([^>]+)>)/gi;
printtext=printtext.replace(re,'');
printtext=printtext.replace(/<\/SELECT>/g,'');

return printtext;
} 
    </script>

    <fieldset class="sectionBorder">
        <legend>User</legend>
        <div align="right" style="margin-right: 4%;" class="cssButtonPanel">
             <asp:Label ID="lblMessage" runat="server" ForeColor="red" Font-Bold="true" Text=""></asp:Label>
            <asp:Button ID="btnAdd" runat="server" CausesValidation="false" ToolTip="Add" Text="Add"
                Width="100px" OnClick="btnAdd_Click"></asp:Button>
        </div>     <br />
       <div  align="right">
       <table style="right:auto"><tr><td class="cssLabel">search:</td>
       <td><asp:TextBox ID="txtsearch" ToolTip="User Name" runat="server"></asp:TextBox></td>
      <td>
       <asp:Button ID="btnGO" runat="server" CausesValidation="false" ToolTip="GO" Text="GO"
                Width="40px" OnClick="btnGO_Click" /></td></tr></table></div>
   
        <div style="margin-left: 1%; width: 100%;" id="divGrid">
            <asp:GridView AutoGenerateColumns="false" OnRowCreated="gridView_RowCreated" Width="100%"
                OnRowDeleting="Role_RowDeleted" OnDataBound="eventhandlerSerialNo" 
                runat="server" ID="GridView1" EmptyDataText="No Records Found">
                <Columns>
                    <asp:BoundField HeaderText="#" ItemStyle-HorizontalAlign="Center" ReadOnly="True" />
                    <asp:BoundField DataField="FullName" SortExpression="FullName" HeaderText="Full Name"
                        ReadOnly="True" />
                    <asp:BoundField DataField="EmployeeCode" SortExpression="EmployeeCode" HeaderText="Employee Code"
                        ReadOnly="True" />
                    <asp:BoundField DataField="LoginID" SortExpression="LoginID" HeaderText="LoginID"
                        ReadOnly="True" />
                    <asp:BoundField DataField="PermanentAddress" SortExpression="Address" HeaderText="Permanent Address"
                        ReadOnly="True" />
                    <asp:BoundField DataField="CurrentAddress" SortExpression="Address" HeaderText="Current Address"
                        ReadOnly="True" />
                    <asp:BoundField DataField="EmailID" SortExpression="EmailID" HeaderText="EmailID"
                        ReadOnly="True" />
                    <asp:BoundField DataField="PhoneNo" SortExpression="PhoneNo" HeaderText="Phone No."
                        ReadOnly="True" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Role" SortExpression="Role" HeaderText="Role" ReadOnly="True" />
                    <asp:BoundField DataField="MobileNo" SortExpression="MobileNo" HeaderText="Mobile No."
                        ReadOnly="True" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="DateOfJoing" SortExpression="DateOfJoing" HeaderText="Date Of Joining "
                        DataFormatString="{0:MM-dd-yyyy}" HtmlEncode="False" ReadOnly="True" />
                    <asp:BoundField DataField="UserTypeName" SortExpression="UserTypeName" HeaderText="User Type"
                        ReadOnly="True" ItemStyle-HorizontalAlign="Center" />
                    <asp:TemplateField HeaderText="Is Active">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:CheckBox ID="chkActive" runat="server" Enabled="false" Checked='<%#Bind("IsActive") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Update" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:HiddenField ID="hdnUserID" runat="server" Value='<%#Bind("UserID") %>' />
                            <img alt="" src='~/images/icon-edit.gif' height='15' width='16' style="cursor: pointer;"
                                id="imageButton" title='Update' onclick='UpdatingInfo(this.id)' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                           
                            
                             <asp:ImageButton ID="Image1" runat="server" ToolTip="Delete" CausesValidation="false" CommandName="Delete"
                                        ImageUrl="~/images/icon-delete.gif" OnClientClick="return DeletingInfo(this.id)"
                                        Text="Delete" >   </asp:ImageButton>     
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                     <asp:TemplateField HeaderText="Change Password" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                           
                            <img alt="" src='~/images/icn_forgetpwd.jpg' height='15' width='16' style="cursor: pointer;"
                                id="imagechg" title='Change Password' onclick='UpdatingPassword(this.id)' runat="server" />
                              
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
      <asp:Literal ID="literal1" runat="server"></asp:Literal>
      <asp:HiddenField ID="hdnExport" runat="server" />
    <asp:HiddenField ID="hdnUrsID" runat="server" />
</asp:Content>
