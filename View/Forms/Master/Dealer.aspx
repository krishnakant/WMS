<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true" CodeFile="Dealer.aspx.cs" Inherits="View_Forms_Master_Dealer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
    function Validation()
    {
        var drpregid = 'ctl00_ContentPlaceHolder1_drpRegion';
        var txtcodeid = 'ctl00_ContentPlaceHolder1_txtCode';
        var txtdelid = 'ctl00_ContentPlaceHolder1_txtDealer';
        var lblcodemsgid = 'ctl00_ContentPlaceHolder1_lblCodemsg';
        var lbldelmsgid = 'ctl00_ContentPlaceHolder1_lblDealermsg';
        var lblregmsgid = 'ctl00_ContentPlaceHolder1_lblRegionmsg';
        
        var drpregvalue = document.getElementById(drpregid).value;
        var txtcodevalue = document.getElementById(txtcodeid).value;
        var txtdelvalue = document.getElementById(txtdelid).value;
        
        
        if(txtcodevalue == '')
        {
            document.getElementById(lblcodemsgid).innerHTML = 'Please enter Code';
            return false;
        }
        else
        {
            document.getElementById(lblcodemsgid).innerHTML = '';
            if(txtdelvalue == '')
            {
                document.getElementById(lbldelmsgid).innerHTML = 'Please enter Dealer Name';
                return false;
            }
            else
            {
                document.getElementById(lbldelmsgid).innerHTML = '';
                if(drpregvalue == '0')
                {
                    document.getElementById(lblregmsgid).innerHTML = 'Please select a Region';
                    return false;
                }
                else
                {
                    document.getElementById(lblregmsgid).innerHTML = '';
                    return true;
                }
            }
        }
        
        
        
       
        
        
    }

</script>
 <fieldset class="sectionBorder">
        <legend>Dealer</legend>
        <div>
            <table>
            <tr>
                    <td class="cssLabel">
                        <asp:Label ID="lblCode" runat="server" Text="Code:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCode" ToolTip="Code" MaxLength="50" runat="server"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtCode"
                            ErrorMessage="*" runat="server">*</asp:RequiredFieldValidator>--%>
                    </td>
                    <td align="left" class="cssLabel">
                    <asp:Label ID="lblCodemsg" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                    
                </tr>
                <tr>
                    <td class="cssLabel">
                        <asp:Label ID="lblDealer" runat="server" Text="Dealer Name:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDealer" Width="230px" ToolTip="Dealer Name" MaxLength="50" runat="server"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator" ControlToValidate="txtDealer"
                            ErrorMessage="*" runat="server">*</asp:RequiredFieldValidator>--%>
                    </td>
                    <td align="left" class="cssLabel">
                    <asp:Label ID="lblDealermsg" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                   <td>
                        <asp:Label ID="lblMessage" runat="server" ForeColor="red" Font-Bold="true" Text=""></asp:Label></td>
                </tr>
                 <tr>
                    <td class="cssLabel">
                        <asp:Label ID="lblRegion" runat="server" Text="Region:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="drpRegion" runat="server"></asp:DropDownList>
                    </td>
                    <td align="left" class="cssLabel">
                    <asp:Label ID="lblRegionmsg" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                   <td>
                        <asp:Label ID="Label2" runat="server" ForeColor="red" Font-Bold="true" Text=""></asp:Label></td>
                </tr>
                <tr> <td class="cssLabel">
                        <asp:Label ID="lblLocation" runat="server" Text="Location:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtLocation" Width="230px" ToolTip="Location"  runat="server"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtCode"
                            ErrorMessage="*" runat="server">*</asp:RequiredFieldValidator>--%>
                    </td></tr>
                     <tr> <td class="cssLabel">
                        <asp:Label ID="Label3" runat="server" Text="Name of Installer:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtInstaller" MaxLength="100" ToolTip="Name of Installer"  runat="server"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtInstaller"
                            ErrorMessage="*" runat="server">*</asp:RequiredFieldValidator>--%>
                    </td></tr>
                <tr>
                    <td class="cssLabel">
                        <asp:Label ID="lblActive" runat="server" Text="IsActive:"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:CheckBox ID="chkActive" ToolTip="IsActive"  Checked="true" runat="server" />
                    </td>
                </tr>
                  <tr>
                    <td class="cssLabel">
                        <asp:Label ID="Label1" runat="server" Text="Is Operating Dealer:"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:CheckBox ID="chkIsOperating" ToolTip="Is Operating Dealer"  Checked="true" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
    </fieldset>
    <div class="cssButtonPanel">
        <asp:Button ID="btnSave" CausesValidation="true" Text="Save" OnClick="btnSave_Click"
            runat="server" ToolTip="Save" OnClientClick="return Validation();" />
        <asp:Button ID="btnCancel" CausesValidation="false" Text="Cancel" OnClick="btnCancel_Click"
            runat="server" ToolTip="Cancel" />
    </div>
     <asp:Literal ID="literal1" runat="server"></asp:Literal>
    <asp:HiddenField ID="hdnCheckID" Value="0" runat="server" />
    <asp:HiddenField ID="hdnDealerID" Value="0" runat="server" />
    <asp:HiddenField ID="hdnCode" Value="0" runat="server" />
</asp:Content>

