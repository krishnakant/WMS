<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true"
    CodeFile="FailureReportNew.aspx.cs" Inherits="View_Forms_Reports_FailureReportNew"
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
//    function ClientValidateItem(source, arguments){
// 
//      if (document.getElementById('ctl00_ContentPlaceHolder1_drpItemGroup').value=='0')
//      {
//         arguments.IsValid=false;
//      }
//      else
//      {
//          arguments.IsValid=true;
//      }
//   }
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
       
       function getSelectedIndex(val)
       {
           document.getElementById('ctl00_ContentPlaceHolder1_hdnReportType').value=val;
       }
        function getValidation()
        {
           var checkCheckBoxList=getCheckBoxStatus('ctl00_ContentPlaceHolder1_chkModelCodeList');
           var checkchkCategory=getCheckBoxStatus('ctl00_ContentPlaceHolder1_chkCategory');
           var checkchkClutchType=getCheckBoxStatus('ctl00_ContentPlaceHolder1_chkClutchType');
           var checkchkSpecialList=getCheckBoxStatus('ctl00_ContentPlaceHolder1_chkSpecialList');
          
         
           if(checkCheckBoxList=='0' && checkchkCategory=='0' && checkchkClutchType=='0' && checkchkSpecialList=='0')
            {
            alert('Please select atleast one model \n'+'Please select atleast one Category\n'+'Please select atleast one Clutch Type\n'+'Please select atleast one Special ');
            return false;
            }
          else  if(checkCheckBoxList=='0')
               {
              alert('Please select atleast one model');
              return false;
               }
          else  if(checkchkCategory=='0')
               {
              alert('Please select atleast one Category');
              return false;
               }
          else  if(checkchkClutchType=='0')
              {
              alert('Please select atleast one Clutch Type');
              return false;
              }
          else  if(checkchkSpecialList=='0')
               {
              alert('Please select atleast one Special ');
              return false;
               }
          else if (document.getElementById('ctl00_ContentPlaceHolder1_drpItemGroup').value=='0')
              {
              alert('Please select  one Group ');
              return false;
              }
         else if (document.getElementById('ctl00_ContentPlaceHolder1_drpItemGroup').value !='0')
              {
                   
                   
                   var itemcount = <% = ItemCount %>;
                   //alert(itemcount)
                     if(itemcount>0)
                     {
                        var checkitem=getCheckBoxStatus('ctl00_ContentPlaceHolder1_Chkitemlist');
                            if(checkitem=='0')
                            {
                                alert('please select at least one Item Code ');
                                return false;
                            } 
                            else
                            {
                                 return true;
                            } 
                    } 
                    else
                    {
                          alert('There are no items in the list');
                                return false;
                    }
              }   
          else
            {
                return true;
            }
       }  
       
    </script>

    <fieldset class="sectionBorder">
        <legend>Failure Per 1000 Tractors</legend>
        <br />
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <asp:RadioButtonList ID="rdoReportType" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="0" Selected="True" onclick="getSelectedIndex(this.value);">Count Wise</asp:ListItem>
                        <asp:ListItem Value="1" onclick="getSelectedIndex(this.value);">Quantity Wise</asp:ListItem>
                    </asp:RadioButtonList></td>
            </tr>
            <tr>
                <td style="width: 64%;">
                    <h5>
                        <asp:Label ID="lblModeCode" runat="server" Text="Model"></asp:Label></h5>
                    <asp:Panel Height="180px" BorderWidth="1px" ID="pnlModelCodeList" runat="server"
                        BorderColor="#00678e" ScrollBars="Vertical">
                        <asp:CheckBoxList ID="chkModelCodeList" CellSpacing="4" ToolTip="select Model" RepeatColumns="10"
                            runat="server">
                        </asp:CheckBoxList>
                    </asp:Panel>
                    <input type="checkbox" id="chkSelectAll" runat="server" onclick="javascript:SetAllCheckBoxes('ctl00_ContentPlaceHolder1_chkModelCodeList',this.id);" />Select
                    All
                </td>
                <td style="width: 32%;">
                    <h5>
                        <asp:Label ID="lblModelVariants" runat="server" Text="Variants"></asp:Label></h5>
                    <asp:Panel Height="40px" BorderWidth="1px" ID="pnlCategory" runat="server" BorderColor="#00678e"
                        ScrollBars="Vertical">
                        <asp:CheckBoxList ID="chkCategory" CellSpacing="4" ToolTip="Select Category" RepeatColumns="2"
                            runat="server">
                        </asp:CheckBoxList>
                    </asp:Panel>
                    <input type="checkbox" id="chkCategoryAll" onclick="javascript:SetAllCheckBoxes('ctl00_ContentPlaceHolder1_chkCategory',this.id);" />Select
                    All
                    <asp:Panel Height="40px" BorderWidth="1px" ID="pnlClutchType" runat="server" BorderColor="#00678e"
                        ScrollBars="Vertical">
                        <asp:CheckBoxList ID="chkClutchType" CellSpacing="4" ToolTip="Select Clutch Type"
                            RepeatColumns="2" runat="server">
                        </asp:CheckBoxList>
                    </asp:Panel>
                    <input type="checkbox" id="chkClutchAll" onclick="javascript:SetAllCheckBoxes('ctl00_ContentPlaceHolder1_chkClutchType',this.id);" />Select
                    All
                    <asp:Panel Height="60px" Width="100%" BorderWidth="1px" ID="pnlSpecial" runat="server"
                        BorderColor="#00678e" ScrollBars="Vertical">
                        <asp:CheckBoxList ID="chkSpecialList" CellSpacing="4" ToolTip="Select Special" RepeatColumns="2"
                            runat="server">
                        </asp:CheckBoxList>
                    </asp:Panel>
                    <input type="checkbox" id="chkSpecialAll" onclick="javascript:SetAllCheckBoxes('ctl00_ContentPlaceHolder1_chkSpecialList',this.id);" />Select
                    All
                </td>
            </tr>
            <tr>
                <td>
                    <span class="cssLabel">Group:</span>
                    <asp:DropDownList AutoPostBack="true" ID="drpItemGroup" runat="server" OnSelectedIndexChanged="drpItemGroup_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <h5>
                        <asp:Label ID="LblItem" runat="server" Text="Item"></asp:Label></h5>
                    <asp:Panel Height="180px" BorderWidth="1px" ID="Panel1" runat="server" BorderColor="#00678e"
                        ScrollBars="Vertical">
                        <asp:CheckBoxList ID="Chkitemlist" CellSpacing="4" ToolTip="select Item" RepeatColumns="11"
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
                <td colspan="4" width="400px" id="tdProdMonth">
                    <span class="cssLabel">From Month:</span>
                    <asp:DropDownList ID="drpFromMonth" runat="server">
                    </asp:DropDownList>
                    <span class="cssLabel">To Month:</span>
                    <asp:DropDownList ID="drpToMonth" runat="server">
                    </asp:DropDownList>
                    <span class="cssLabel">Problem Type</span>
                    <asp:RadioButtonList ID="rdoProblemType" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="0">Primary</asp:ListItem>
                        <asp:ListItem Value="1">Consequences</asp:ListItem>
                        <asp:ListItem Value="2" Selected="True">Total</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
        </table>
        <br />
        <div class="cssButtonPanel">
            <asp:Button ID="btnExcelGraph" runat="server" CssClass="cssButton" Text="Excel Graph"
                ToolTip="Excel Graph" OnClientClick="return getValidation();" OnClick="btnExcelGraph_Click" />
            <asp:Button ID="btnShowGraph" runat="server" CssClass="cssButton" Text="Show Graph"
                ToolTip="Show Graph" OnClientClick="return getValidation();" OnClick="btnShowGraph_Click" />
           <asp:Button ID="btnExport" runat="server" Visible="false" Text="Export to Excel"
                ToolTip="Export to Excel" CssClass="cssButton" OnClick="btnExport_Click" OnClientClick="return CallExport('print_Grid');" />
            <asp:Button ID="btnShow" runat="server" CssClass="cssButton" Text="Show Data" ToolTip="Show Data"
                OnClientClick="return getValidation();" OnClick="btnShow_Click" />
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
    <asp:HiddenField ID="hdnReportType" runat="server" Value="0" />
</asp:Content>
