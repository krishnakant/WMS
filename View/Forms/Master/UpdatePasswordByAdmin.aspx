<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true"
    CodeFile="UpdatePasswordByAdmin.aspx.cs" Inherits="View_Forms_Master_UpdatePasswordByAdmin"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
    /********************************************************Start*******************************************************/
    //<><> Function Defination for Setting Page Title<><>// 
	function fnCallOnLoad()
	{
		fnSetPageTitle('Change Password ');
		
	}
	 function setLabelText(ID, Text)
    {
      document.getElementById(ID).innerHTML = Text;
      setTimeout("setLabelText('ctl00_ContentPlaceHolder1_Message','')",3000);
    }
	
	 
	/********************************************************END**************************************************/
    </script>

    <table cellspacing="0" cellpadding="0" width="100%" class="cssTable">
        <tr>
            <td class="cssPageText">
                <fieldset class="sectionBorder">
                    <legend>Change Password</legend>
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td class="cssLabel">
                                User:</td>
                            <td class="cssElements">
                                <asp:TextBox TextMode="SingleLine" Enabled="false" ID="txtUser" runat="server" CssClass="cssTextBox"
                                    MaxLength="30" Width="148" ToolTip="User Name"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="cssLabel">
                                Old Password:</td>
                            <td class="cssElements">
                                <asp:TextBox TextMode="SingleLine" Enabled="false" ID="txtOldPassword" runat="server"
                                    CssClass="cssTextBox" MaxLength="30" Width="148" ToolTip="Old Password"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="cssLabel">
                                New Password:</td>
                            <td class="cssElements">
                                <asp:TextBox TextMode="Password" ID="txtPassword" runat="server" CssClass="cssTextBox"
                                    MaxLength="30" Width="148" ToolTip="New Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtPassword"
                                    runat="server" ErrorMessage="*">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="cssLabel">
                                Confirm Password:</td>
                            <td class="cssElements">
                                <asp:TextBox TextMode="Password" ID="txtConfirmPassword" runat="server" CssClass="cssTextBox"
                                    MaxLength="30" Width="148" ToolTip="Confirm Password"></asp:TextBox><asp:CompareValidator
                                        runat="server" ID="cvPasswordConfirmPassword" ControlToCompare="txtPassword"
                                        ControlToValidate="txtConfirmPassword" ErrorMessage="Password and confirm password should be same"></asp:CompareValidator></td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
    <div id="buttonPanel" class="cssButtonPanel">
        <asp:Label runat="server" ID="Message" Visible="true" ForeColor="red"></asp:Label>
        <asp:Button ID="btnSave" runat="server" Text="Update" CssClass="cssButton" CausesValidation="true"
            ToolTip="Update Password" OnClick="btnSave_Click" />
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="cssButton" CausesValidation="false"
            ToolTip="Cancel" OnClick="btnCancel_Click" />
    </div>
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
</asp:Content>
