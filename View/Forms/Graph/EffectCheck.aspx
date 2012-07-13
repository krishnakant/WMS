<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true"
    CodeFile="EffectCheck.aspx.cs" Inherits="View_Forms_Reports_EffectCheck"
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
             //var creteDiv=getselection();
            document.getElementById('ctl00_ContentPlaceHolder1_hdnExport').value=prtContent.innerHTML;
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
           
          var strEntendedMonth=document.getElementById('ctl00_ContentPlaceHolder1_txtEntendedMonth').value;
          
           if (strEntendedMonth=='')
           {
            alert('Please enter the value of Extended Month');
            return false;
           }
           if (strEntendedMonth=='0')
           {
            alert('Please enter the value of Extended Month');
            return false;
           }
            if (strEntendedMonth > 12 )
            {
            alert(' Extended Months should not be more than 12');
             return false;
            }
                        
            document.getElementById('ctl00_ContentPlaceHolder1_hdnExtended').value=strEntendedMonth;
          
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
        <legend>Effect Check</legend>
        <br />
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <asp:RadioButtonList ID="rdoReportType" runat="server" RepeatDirection="Horizontal" Visible="false">
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
                <td>
                    <span class="cssLabel">Extended Months:</span>
                    <asp:TextBox ID="txtEntendedMonth" runat="server" ToolTip="Entended Months" Text="1">
                    
                    </asp:TextBox>
                    
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
                <td class="cssLabel">
                    Period:</td>
                <td>
                    <asp:DropDownList ID="drpMonth" runat="server">
                        <asp:ListItem Value="1">January</asp:ListItem>
                        <asp:ListItem Value="2">February</asp:ListItem>
                        <asp:ListItem Value="3">March</asp:ListItem>
                        <asp:ListItem Value="4" Selected="True">April</asp:ListItem>
                        <asp:ListItem Value="5">May</asp:ListItem>
                        <asp:ListItem Value="6">June</asp:ListItem>
                        <asp:ListItem Value="7">July</asp:ListItem>
                        <asp:ListItem Value="8">August</asp:ListItem>
                        <asp:ListItem Value="9">September</asp:ListItem>
                        <asp:ListItem Value="10">October</asp:ListItem>
                        <asp:ListItem Value="11">November</asp:ListItem>
                        <asp:ListItem Value="12">December</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="drpYear" runat="server">
                        <asp:ListItem Selected="true" Value="2000">2000</asp:ListItem>
                        <asp:ListItem Value="2001">2001</asp:ListItem>
                        <asp:ListItem Value="2002">2002</asp:ListItem>
                        <asp:ListItem Value="2003">2003</asp:ListItem>
                        <asp:ListItem Value="2004">2004</asp:ListItem>
                        <asp:ListItem Value="2005">2005</asp:ListItem>
                        <asp:ListItem Value="2006">2006</asp:ListItem>
                        <asp:ListItem Value="2007">2007</asp:ListItem>
                        <asp:ListItem Value="2008">2008</asp:ListItem>
                        <asp:ListItem Value="2009">2009</asp:ListItem>
                        <asp:ListItem Value="2010">2010</asp:ListItem>
                        <asp:ListItem Value="2011">2011</asp:ListItem>
                        <asp:ListItem Value="2012">2012</asp:ListItem>
                        <asp:ListItem Value="2013">2013</asp:ListItem>
                        
                        <asp:ListItem Value="2014">2014</asp:ListItem>
                        <asp:ListItem Value="2015">2015</asp:ListItem>
                        <asp:ListItem Value="2016">2016</asp:ListItem>
                        <asp:ListItem Value="2017">2017</asp:ListItem>
                        <asp:ListItem Value="2018">2018</asp:ListItem>
                        <asp:ListItem Value="2019">2019</asp:ListItem>
                        <asp:ListItem Value="2020">2020</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
        </table>
        <br />
        <div class="cssButtonPanel">
            <asp:Button ID="btnExcelGraph" runat="server" CssClass="cssButton" Text="Excel Graph"
                ToolTip="Excel Graph"  OnClientClick="return getValidation();" OnClick="btnExcelGraph_Click" />
            <asp:Button ID="btnShowGraph" runat="server" CssClass="cssButton" Text="Show Graph"
                ToolTip="Show Graph"   OnClientClick="return getValidation();" OnClick="btnShowGraph_Click" />
            <asp:Button ID="btnExport" runat="server" Visible="false" OnClientClick="javascript:ExportGridWithSelection('print_Grid');"
                OnClick="btnExport_Click" Text="Export to Excel" ToolTip="Export to Excel" CssClass="cssButton" />
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
    <asp:HiddenField ID="hdnExtended" runat="server" />
    <asp:HiddenField ID="hdnExport" runat="server" />
    <asp:HiddenField ID="hdnReportType" runat="server" Value="0" />
    
</asp:Content>
