<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true"
    CodeFile="CostReport.aspx.cs" Inherits="View_Forms_Reports_CostReport" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
 function ClientValidateModel(source, arguments){
 
      if (document.getElementById('ctl00_ContentPlaceHolder1_drpModel').value=='0')
      {
         arguments.IsValid=false;
      }
      else
      {
          arguments.IsValid=true;
      }
   }
   
   function ExportGridWithSelection( strid )
       {
            var prtContent = document.getElementById(strid);
             var creteDiv=getselection();
            document.getElementById('ctl00_ContentPlaceHolder1_hdnExport').value=creteDiv + prtContent.innerHTML;
       }
       
       function getselection()
       {
            var d = new Date();
            var Model=document.getElementById('ctl00_ContentPlaceHolder1_drpModel').options[document.getElementById('ctl00_ContentPlaceHolder1_drpModel').selectedIndex].text;
            var creteDiv='<br /><table cellpadding=0 cellspacing=0 border=1><tr><td class=cssLabel style="width:100%;align:center;"><b>Cost Per Tractor</b></td></tr><tr><td class="cssLabel"><b>Model: '+ Model +'</b></td></tr>';
            creteDiv +='</table><br />';
            return creteDiv;
       }
       function getContent(selval)
       {
            document.getElementById('ctl00_ContentPlaceHolder1_hdnSelected').value = selval;
            if(selval == 0)
            {
               // document.getElementById('tdQuarter').style.display='none';
                document.getElementById('tdProdMonth').style.display='';
            }
            else
            {
                // document.getElementById('tdQuarter').style.display='';
                 document.getElementById('tdProdMonth').style.display='none';
            }
       }
       
    </script>

    <fieldset class="sectionBorder">
        <legend>Cost Per Tractor</legend>
        <br />
        <table>
            <tr>
                <td class="cssLabel">
                    Report Type:</td>
                <td width="550px" colspan="6">
                    <asp:RadioButtonList ID="rdoReportType" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="0" Selected="True" onclick="javascript:getContent(this.value);">Production Month Wise</asp:ListItem>
                        <asp:ListItem Value="1" onclick="javascript:getContent(this.value);">Quarter Wise</asp:ListItem>
                    </asp:RadioButtonList></td>
            </tr>
            <tr>
                <td class="cssLabel">
                    <asp:Label ID="lblGroupName" runat="server" Text="Model:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="drpModel" runat="server">
                    </asp:DropDownList></td>
                <td>
                    <asp:CustomValidator ID="CustomValidator1" ControlToValidate="drpModel" runat="server"
                        ClientValidationFunction="ClientValidateModel" ErrorMessage="Select Model"></asp:CustomValidator></td>
                <td colspan="4" width="400px"  id="tdProdMonth">
                    <span class="cssLabel">From Month:</span>
                    <asp:DropDownList ID="drpFromMonth" runat="server">
                    </asp:DropDownList>
                    <span class="cssLabel">To Month:</span>
                    <asp:DropDownList ID="drpToMonth" runat="server">
                    </asp:DropDownList>
                </td>
                <td colspan="4" id="tdQuarter" width="400px" style="display:none;">
                    <span class="cssLabel">Quarter:</span>
                    <asp:DropDownList ID="drpQuarter" runat="server">
                    </asp:DropDownList>
                </td>
                
            </tr>
        </table>
        <div class="cssButtonPanel">
            <asp:Button ID="btnExport" runat="server" Visible="false" OnClientClick="javascript:ExportGridWithSelection('print_Grid');"
                OnClick="btnExport_Click" Text="Export to Excel" ToolTip="Export to Excel" CssClass="cssButton" />
            <asp:Button ID="btnShow" runat="server" CssClass="cssButton" Text="Show" ToolTip="Show"
                OnClick="btnShow_Click" />
        </div>
        <br />
        <div id="print_Grid" style="overflow: auto; height: 200px; width: 950px;">
            <asp:GridView AutoGenerateColumns="true" OnRowCreated="OnRowCreated" Width="100%"
                ID="grdCostReport" runat="server" EmptyDataText="No Records Found">
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
        </div>
        <br />
        <br />
    </fieldset>
    <asp:HiddenField ID="hdnExport" runat="server" />
    <asp:HiddenField ID="hdnSelected" runat="server" Value="0" />

    <script type="text/javascript">
        var val = document.getElementById('ctl00_ContentPlaceHolder1_hdnSelected').value;
        getContent(val);
    </script>

</asp:Content>
