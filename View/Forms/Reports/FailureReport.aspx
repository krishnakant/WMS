<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true"
    CodeFile="FailureReport.aspx.cs" Inherits="View_Forms_Reports_FailureReport"
    ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="/WMS/JS/FusionCharts.js"></script>

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
    function ClientValidateItem(source, arguments){
 
      if (document.getElementById('ctl00_ContentPlaceHolder1_drpItem').value=='0')
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
            var creteDiv='<br /><table cellpadding=0 cellspacing=0 border=1><tr><td class=cssLabel style="width:100%;align:center;"><b>Failure Per 1000</b></td></tr><tr><td class="cssLabel"><b>Model: '+ Model +'</b></td></tr>';
            creteDiv +='</table><br />';
            return creteDiv;
       }
       function checkValidation()
      {
          var checkModelCode=getCheckBoxStatus('ctl00_ContentPlaceHolder1_chkModelCodeList');
        var checkitem=getCheckBoxStatus('ctl00_ContentPlaceHolder1_Chkitemlist');
        if(!checkModelCode){alert('please select at least one Model Code'); return false;}
        if(!checkitem){alert('please select at least one Item Code'); return false;}
        
       }
       
    </script>

    <fieldset class="sectionBorder">
        <legend>Failure Per 1000 Tractors</legend>
        <br />
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <%-- <td class="cssLabel">
                    Model:
                </td>--%>
                <td>
                    <h5>
                        <asp:Label ID="lblModeCode" runat="server" Text="Model"></asp:Label></h5>
                    <asp:Panel Height="180px" BorderWidth="1px" ID="pnlModelCodeList" runat="server"
                        BorderColor="#00678e" ScrollBars="Vertical">
                        <asp:CheckBoxList ID="chkModelCodeList" CellSpacing="4" ToolTip="select Model" RepeatColumns="5"
                            runat="server">
                        </asp:CheckBoxList>
                    </asp:Panel>
                    <input type="checkbox" id="chkSelectAll" runat="server" onclick="javascript:SetAllCheckBoxes('ctl00_ContentPlaceHolder1_chkModelCodeList',this.id);" />Select
                    All
                </td>
                <%-- <td>
                    <asp:DropDownList ID="drpModel" runat="server">
                    </asp:DropDownList></td>
                <td>
                    <asp:CustomValidator ID="CustomValidator1" ControlToValidate="drpModel" runat="server"
                        ClientValidationFunction="ClientValidateModel" ErrorMessage="Select Model"></asp:CustomValidator></td>--%>
                <%--  <td class="cssLabel">
                    Item:
                </td>--%>
                <td>
                    <h5>
                        <asp:Label ID="LblItem" runat="server" Text="Item"></asp:Label></h5>
                    <asp:Panel Height="180px" BorderWidth="1px" ID="Panel1" runat="server" BorderColor="#00678e"
                        ScrollBars="Vertical">
                        <asp:CheckBoxList ID="Chkitemlist" CellSpacing="4" ToolTip="select Item" RepeatColumns="5"
                            runat="server">
                        </asp:CheckBoxList>
                    </asp:Panel>
                    <input type="checkbox" id="ChkAllitemlist" runat="server" onclick="javascript:SetAllCheckBoxes('ctl00_ContentPlaceHolder1_Chkitemlist',this.id);" />Select
                    All
                </td>
                <%-- <td>
                    <asp:DropDownList ID="drpItem" runat="server">
                    </asp:DropDownList></td>
                <td>
                    <asp:CustomValidator ID="CustomValidator2" ControlToValidate="drpItem" runat="server"
                        ClientValidationFunction="ClientValidateItem" ErrorMessage="Select Item"></asp:CustomValidator>
                </td>--%>
            </tr>
            <tr>
                <td colspan="4" width="400px"  id="tdProdMonth">
                    <span class="cssLabel">From Month:</span>
                    <asp:DropDownList ID="drpFromMonth" runat="server">
                    </asp:DropDownList>
                    <span class="cssLabel">To Month:</span>
                    <asp:DropDownList ID="drpToMonth" runat="server">
                    </asp:DropDownList>
                </td></tr>
        </table>
        <br />
        <div class="cssButtonPanel">
            <asp:Button ID="btnExcelGraph" runat="server" CssClass="cssButton" Text="Excel Graph"
                ToolTip="Excel Graph" OnClick="btnExcelGraph_Click" />
            <asp:Button ID="btnShowGraph" runat="server" CssClass="cssButton" Text="Show Graph"
                ToolTip="Show Graph" OnClientClick="return checkValidation();" OnClick="btnShowGraph_Click" />
            <asp:Button ID="btnExport" runat="server" Visible="false" OnClientClick="javascript:ExportGridWithSelection('print_Grid');"
                OnClick="btnExport_Click" Text="Export to Excel" ToolTip="Export to Excel" CssClass="cssButton" />
            <asp:Button ID="btnShow" runat="server" CssClass="cssButton" Text="Show Data" ToolTip="Show Data"
                OnClientClick="return checkValidation();" OnClick="btnShow_Click" />
        </div>
        <br />
        <div id="print_Grid" style="overflow: auto;">
            <asp:GridView AutoGenerateColumns="true" OnRowCreated="OnRowCreated" Width="100%"
                ID="grdFailureReport" Visible="false" runat="server" EmptyDataText="No Records Found">
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
        </div>
        <div id="divGraph" runat="server" visible="false">
            <% =CreateChart() %>
        </div>
    </fieldset>
    <asp:HiddenField ID="hdnExport" runat="server" />
</asp:Content>
