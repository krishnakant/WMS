<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BudgetNew.aspx.cs" MasterPageFile="~/master/MasterPage.master" Inherits="View_Forms_DataInput_BudgetNew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
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
     var val =  document.getElementById('ctl00_ContentPlaceHolder1_drpModelCategory').value;
    
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

    
    </script>

    <fieldset class="sectionBorder">
        <legend>Budget</legend>
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="cssLabel">
                    Model:</td>
                <td>
                    <asp:DropDownList ID="drpModel" runat="server">
                    </asp:DropDownList><asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="*" ClientValidationFunction="ClientValidateModel"
                        ControlToValidate="drpModel"></asp:CustomValidator></td>
                <td class="cssLabel">
                    Model Category:</td>
                <td>
                    <asp:DropDownList ID="drpModelCategory" runat="server">
                    </asp:DropDownList><asp:CustomValidator ID="CustomValidator2" runat="server" ErrorMessage="*" ClientValidationFunction="ClientValidateCategory"
                        ControlToValidate="drpModelCategory"></asp:CustomValidator></td>
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
                    Financial Year:</td>
                <td>
                    <asp:DropDownList ID="drp_FinancialYear" runat="server">
                        <asp:ListItem Value="0">2000-2001</asp:ListItem>
                        <asp:ListItem Value="1">2001-2002</asp:ListItem>
                        <asp:ListItem Value="2">2002-2003</asp:ListItem>
                        <asp:ListItem Value="3">2003-2004</asp:ListItem>
                        <asp:ListItem Value="4">2004-2005</asp:ListItem>
                        <asp:ListItem Value="5">2005-2006</asp:ListItem>
                        <asp:ListItem Value="6">2006-2007</asp:ListItem>
                        <asp:ListItem Value="7" Selected="True">2007-2008</asp:ListItem>
                        <asp:ListItem Value="8">2008-2009</asp:ListItem>
                        <asp:ListItem Value="9">2009-2010</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
        </table>
        <div>
            <asp:GridView AutoGenerateColumns="false" Width="100%" ID="grdBudget" runat="server"
                EmptyDataText="No Records Found">
                <Columns>
                    <asp:BoundField HeaderText="Quarter" DataField="Quarter" />
                    <asp:TemplateField HeaderText="Budget">
                        <ItemTemplate>
                            <asp:TextBox ID="txtBudget" runat="server"></asp:TextBox>
                            <asp:HiddenField ID="hdnQuarterID" runat="server" Value='<%# Bind("QuarterID") %>' />
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
        <br />
        <div class="cssButtonPanel">
            <asp:Label ID="lblMessage" runat="server" ForeColor="red"></asp:Label>
            <asp:Button ID="btnSave" CssClass="cssButton" runat="server" Text="Save" ToolTip="Save"
                OnClick="btnSave_Click" />
        </div>
        <asp:Literal ID="literal1" runat="server"></asp:Literal>
    </fieldset>
</asp:Content>

 
