<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master"  AutoEventWireup="true" CodeFile="ChangPassword.aspx.cs" Inherits="View_Forms_Master_ChangPassword"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">  
function setMessageText(ID,Text)
    {
       document.getElementById(ID).innerHTML =Text ;
       setTimeout("setMessageText('"+ID+"','')",3000);
    } </script>
<fieldset class="sectionBorder">
<legend>Change Password </legend>
<center>
<table cellpadding="0" cellspacing="0" border="1"  width="60%">
 <tr><td class="cssLabel">
  <asp:Label ID="Label2" runat="server" Text="User Name:"></asp:Label>
    </td>
    <td>
     <asp:Label ID="lblUserName" runat="server" ></asp:Label>
    
    </td>
    </tr>
 <tr><td class="cssLabel">
     <asp:Label ID="lblOldPassword" runat="server" Text="Current Password:"></asp:Label></td>
    <td>
     <asp:TextBox ID= "txtPassword" MaxLength="16" ToolTip="OldPassword" TextMode="Password" runat="server"></asp:TextBox>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtPassword" runat="server" 
    ErrorMessage="*">*</asp:RequiredFieldValidator>
    </td>
    </tr>
     <tr><td class="cssLabel">
     <asp:Label ID="lblNewPassword" runat="server" Text="New Password:"></asp:Label></td>
    <td>
     <asp:TextBox ID= "txtNewPassword" MaxLength="16" ToolTip="Password" TextMode="Password" runat="server"></asp:TextBox>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtNewPassword" runat="server" 
    ErrorMessage="*">*</asp:RequiredFieldValidator>
    </td>
    </tr>
    <tr><td class="cssLabel">
     <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm Password:"></asp:Label></td>
    <td>
    <asp:TextBox ID= "txtConfirmPassword" MaxLength="16" ToolTip="ConfirmPassword" TextMode="Password" runat="server"></asp:TextBox>
    <asp:CompareValidator ID="CompareValidator" ControlToCompare="txtNewPassword"
     ControlToValidate="txtConfirmPassword" ErrorMessage="*" runat="server"></asp:CompareValidator>
    </td>
    </tr>
    <tr>
      
  <td>
 
 
  <asp:Label ID="Label1" ForeColor="Red" Visible="false" runat="server" Text=""></asp:Label>
    <asp:Label ID="lblMessage" Visible="false" runat="server" ForeColor="Red"></asp:Label>
  </td>
  </tr>
    
    </table>
    </center>
</fieldset>

<div class="cssButtonPanel">
 <asp:Button ID="btnUpdate" Text="Update" runat="server" OnClick="btnUpdate_Click" />
   <asp:Button ID="btnCancel" CausesValidation="false" Text="Cancel" OnClick="btnCancel_Click"
            runat="server" ToolTip="Cancel" />
</div>
 <asp:Literal ID="literal1" runat="server"></asp:Literal>
</asp:Content>

