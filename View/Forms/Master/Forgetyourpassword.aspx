<%@ Page Language="C#" MasterPageFile="~/master/LoginMasterPage.master" AutoEventWireup="true" CodeFile="Forgetyourpassword.aspx.cs" Inherits="View_Forms_Master_Forgetyourpassword"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<fieldset><legend>Forget your password</legend>
<center>
<h5> Please enter the email address you provided when creating your account</h5></center>
<center>
<table>
<tr></tr>
<tr align="center">
                                <td>
                                    <asp:Label ID="lblEmailID" runat="server" Text="Email&nbsp;ID:"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtEmailID" ToolTip="LoginID" runat="server"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtEmailID"
                                        runat="server" ErrorMessage="*" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
</table>
</center>

                    
                
</fieldset>
<div style="float:right">
 <asp:Button ID="btnSend" Text="Send" runat="server" Width="60px" OnClick="btnSend_Click" />
</div>
</asp:Content>

