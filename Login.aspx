<%@ Page Language="C#" MasterPageFile="~/master/LoginMasterPage.master" AutoEventWireup="true"
    CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 <script type="text/javascript">
   function setLabelText(ID,Text)
        {
           document.getElementById('ctl00_ContentPlaceHolder1_lblMessage').innerHTML =Text ;
           setTimeout("setLabelText('ctl00_ContentPlaceHolder1_lblMessage','')",3000);
        }
 </script>
    <div id="login">
        <table border="0" cellpadding="0" cellspacing="3" class="cssLogin">
            <tr align="left" valign="top">
                <td class="logtext">
                    Login ID:</td>
                <td>
                    <asp:TextBox ID="txtun" ToolTip="Login ID" runat="server" Width="130px"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="left" class="logtext">
                    Password:</td>
                <td>
                    <asp:TextBox ID="txtpwd" ToolTip="Password" runat="server" Width="130px" TextMode="Password"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2" align="right">
                    <asp:ImageButton ID="btnLogin" runat="server" ToolTip="login" ImageUrl="~/images/log_button_new.jpg"
                        OnClick="btnLogin_Click" />
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="50">
                    <asp:Label ID="lblMessage" runat="server" ForeColor="red"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:Literal ID="literal1" runat="server"></asp:Literal>
    </div>
</asp:Content>
