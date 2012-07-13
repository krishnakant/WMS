<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true"
    CodeFile="ModelNew.aspx.cs" Inherits="View_Forms_Master_ModelNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
function ClientValidateModel(source,arguments)
{
     var val =  document.getElementById('ctl00_ContentPlaceHolder1_drpModel').value;
    
     if(val == '0')
     {
        arguments.IsValid=false;
     }
     else
     {
        arguments.IsValid=true;
     }
}

function ClientValidateCategory(source,arguments)
{
     var val =  document.getElementById('ctl00_ContentPlaceHolder1_drpCategory').value;
    
     if(val == '0')
     {
        arguments.IsValid=false;
     }
     else
     {
        arguments.IsValid=true;
     }
}

function ClientValidateClutch(source,arguments)
{
    var val =  document.getElementById('ctl00_ContentPlaceHolder1_drpClutch').value;
   
     if(val == '0')
     {
        arguments.IsValid=false;
     }
     else
     {
        arguments.IsValid=true;
     }
}

   function setMessageText(ID,Text)
    {
       document.getElementById(ID).innerHTML =Text ;
       setTimeout("setMessageText('"+ID+"','')",3000);
    } 
    
    function getSpecialList(id)
    {
        var state = document.getElementById(id).checked;
        //alert(state);
        
        if(document.getElementById(id).checked)
        {
            document.getElementById('special').style.display = '';
        }
        else
        {
            document.getElementById('special').style.display = 'none';
        }
        
    }
    
    </script>

    <fieldset class="sectionBorder">
        <legend>Model Detail</legend>
        <table>
            <tr>
                <td class="cssLabel">
                    Material:</td>
                <td>
                    <asp:TextBox ID="txtMaterial" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                        ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtMaterial"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td class="cssLabel">
                    Model:</td>
                <td>
                    <asp:DropDownList ID="drpModel" runat="server">
                    </asp:DropDownList>
                    <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="*" ClientValidationFunction="ClientValidateModel"
                        ControlToValidate="drpModel"></asp:CustomValidator></td>
            </tr>
            <tr>
                <td class="cssLabel">
                    Category:</td>
                <td>
                    <asp:DropDownList ID="drpCategory" runat="server">
                    </asp:DropDownList>
                    <asp:CustomValidator ID="CustomValidator2" runat="server" ErrorMessage="*" ClientValidationFunction="ClientValidateCategory"
                        ControlToValidate="drpCategory"></asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td class="cssLabel">
                    Special:</td>
                <td>
                    <asp:CheckBox ID="chkSpecial" runat="server" onclick="javascript:getSpecialList(this.id);">
                    </asp:CheckBox>
                    <div id="special" style="display: none;">
                        <asp:DropDownList ID="drpSpecial" runat="server">
                        </asp:DropDownList></div>
                </td>
            </tr>
            <tr>
                <td class="cssLabel">
                    Clutch:</td>
                <td>
                    <asp:DropDownList ID="drpClutch" runat="server">
                    </asp:DropDownList>
                    <asp:CustomValidator ID="CustomValidator3" runat="server" ErrorMessage="*" ClientValidationFunction="ClientValidateClutch"
                        ControlToValidate="drpClutch"></asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td class="cssLabel">
                    Description:</td>
                <td>
                    <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine"></asp:TextBox>
        </table>
        <br />
        <div class="cssButtonPanel">
            <asp:Label ID="lblMessage" runat="server" ForeColor="red"></asp:Label>
            <asp:Button ID="btnSave" runat="server" Text="Save" ToolTip="Save" CssClass="cssButton"
                OnClick="btnSave_Click" />
            <asp:Button ID="btnCancel" runat="server" CausesValidation="false" Text="Cancel" ToolTip="Cancel" CssClass="cssButton"
                OnClick="btnCancel_Click" />
        </div>
        <asp:Literal ID="literal1" runat="server"></asp:Literal>
    </fieldset>
    <script type="text/javascript">
        getSpecialList('ctl00_ContentPlaceHolder1_chkSpecial');
    </script>
</asp:Content>
