<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true"
    CodeFile="MonthWiseCostGraph.aspx.cs" Inherits="View_Forms_Graphs_MonthWiseCostGraph"
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
   
   function ExportGridWithSelection( strid )
       {
            var prtContent = document.getElementById(strid);
             //var creteDiv=getselection();
            document.getElementById('ctl00_ContentPlaceHolder1_hdnExport').value= prtContent.innerHTML;
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
          else
            {
           return true;
            }
       }  
       
        function getDiv(val)
        {
             document.getElementById('ctl00_ContentPlaceHolder1_hdnEngine').value = val;
        if(val=='0')
        {
             document.getElementById('placediv').style.display='';
             document.getElementById('enginediv').style.display='none';
                
        }
        else if(val =='1')
        {
             document.getElementById('enginediv').style.display='';
             document.getElementById('placediv').style.display='none';
             
        }
        else
        {
            document.getElementById('enginediv').style.display='none';
             document.getElementById('placediv').style.display='none';  
        }
    }
     
    function getMonthYear(val)
    {
        if(val == '0')
        {
            document.getElementById('tdProdMonth').style.display='';
        }
        else
        {
             document.getElementById('tdProdMonth').style.display='none';
        }
    }
    
    function  indexchange()
    {

        if (document.getElementById('ctl00_ContentPlaceHolder1_ddlselection').value==2  )
        {

           document.getElementById('trdata').style.display='none';
            document.getElementById('spnReportType').style.display='none';
         
        }
        else if(document.getElementById('ctl00_ContentPlaceHolder1_ddlselection').value==3)
        {
           document.getElementById('trdata').style.display='none';
            document.getElementById('spnReportType').style.display='none';
        }
        else if (document.getElementById('ctl00_ContentPlaceHolder1_ddlselection').value==1)
        {
            document.getElementById('trdata').style.display='';
            document.getElementById('spnReportType').style.display='';                  
        }

}
    </script>

    <fieldset class="sectionBorder">
        <legend>Graphical Analysis</legend>
        <br />
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="color: #1b0b6e; width: 203px;display:none;">
                    Selection:
                    <asp:DropDownList ID="ddlselection" onchange="javaScript:indexchange();" runat="server">
                        <asp:ListItem Selected="true" Value="1">Defect</asp:ListItem>
                        <asp:ListItem Value="2">Production</asp:ListItem>
                        <asp:ListItem Value="3">Sales</asp:ListItem>
                    </asp:DropDownList>
                    </td><td>
                    <span id="spnReportType">
                        <asp:RadioButtonList ID="rdoReportType" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True" Value="0">Cost/Tractor</asp:ListItem>
                            <asp:ListItem Value="1">Defect/1000 Tractor</asp:ListItem>
                        </asp:RadioButtonList>
                    </span>
                </td>
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
                    <input type="checkbox" id="chkSelectAll" onclick="javascript:SetAllCheckBoxes('ctl00_ContentPlaceHolder1_chkModelCodeList',this.id);" />Select
                    All
                </td>
                <td style="width: 32%;">
                    <h5>
                        <asp:Label ID="lblModelVariants" runat="server" Text="Variants"></asp:Label></h5>
                    <asp:Panel Height="40px" BorderWidth="1px" ID="pnlCategory" runat="server" BorderColor="#00678e"
                        ScrollBars="Vertical">
                        <asp:RadioButtonList ID="chkCategory" CellSpacing="4" ToolTip="Select Category" RepeatColumns="2"
                            runat="server">
                        </asp:RadioButtonList>
                    </asp:Panel>
                    <%--<input type="checkbox" id="chkCategoryAll" onclick="javascript:SetAllCheckBoxes('ctl00_ContentPlaceHolder1_chkCategory',this.id);" />Select
                    All--%>
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
                    <span class="cssLabel">From Period:</span>
                    <asp:DropDownList ID="drpFromMonth" runat="server">
                        <asp:ListItem Value="1">January</asp:ListItem>
                        <asp:ListItem Value="2">February</asp:ListItem>
                        <asp:ListItem Value="3">March</asp:ListItem>
                        <asp:ListItem Value="4">April</asp:ListItem>
                        <asp:ListItem Value="5">May</asp:ListItem>
                        <asp:ListItem Value="6">June</asp:ListItem>
                        <asp:ListItem Value="7">July</asp:ListItem>
                        <asp:ListItem Value="8">August</asp:ListItem>
                        <asp:ListItem Value="9">September</asp:ListItem>
                        <asp:ListItem Value="10">October</asp:ListItem>
                        <asp:ListItem Value="11">November</asp:ListItem>
                        <asp:ListItem Value="12">December</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="drpFromYear" runat="server">
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
                    </asp:DropDownList>
                    <span class="cssLabel">To Period:</span>
                    <asp:DropDownList ID="drpToMonth" runat="server">
                        <asp:ListItem Value="1">January</asp:ListItem>
                        <asp:ListItem Value="2">February</asp:ListItem>
                        <asp:ListItem Value="3">March</asp:ListItem>
                        <asp:ListItem Value="4">April</asp:ListItem>
                        <asp:ListItem Value="5">May</asp:ListItem>
                        <asp:ListItem Value="6">June</asp:ListItem>
                        <asp:ListItem Value="7">July</asp:ListItem>
                        <asp:ListItem Value="8">August</asp:ListItem>
                        <asp:ListItem Value="9">September</asp:ListItem>
                        <asp:ListItem Value="10">October</asp:ListItem>
                        <asp:ListItem Value="11">November</asp:ListItem>
                        <asp:ListItem Value="12">December</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="drpToYear" runat="server">
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
                    </asp:DropDownList>
                </td>
            </tr>
            <tr id="trdata">
                <td>
                    <asp:RadioButtonList ID="rdoData" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="0" onclick="getDiv(this.value);">Overall</asp:ListItem>
                        <asp:ListItem Value="1" onclick="getDiv(this.value);">Engine</asp:ListItem>
                        <asp:ListItem Value="2" onclick="getDiv(this.value);">Tractor</asp:ListItem>
                    </asp:RadioButtonList>
                    <div id="enginediv" style="display: none;">
                        <span class="cssLabel">Engine:</span>
                        <asp:RadioButton ID="rdoAlwarEngine" runat="server" Text="Alwar" GroupName="Engine" /><asp:RadioButton
                            ID="rdoSimpsonEngine" GroupName="Engine" runat="server" Text="Simpson" /><asp:RadioButton
                                ID="rdoBothEngine" runat="server" Checked="true" Text="Both" GroupName="Engine" /></div>
                    <div id="placediv">
                        <span class="cssLabel">Plant:</span>
                        <asp:RadioButton ID="rdoAlwar" runat="server" Text="Alwar" GroupName="Place" /><asp:RadioButton
                            ID="rdoBhopal" GroupName="Place" runat="server" Text="Bhopal" /><asp:RadioButton
                                ID="rdoAllPlace" runat="server" Checked="true" Text="Both" GroupName="Place" /></div>
                    <span class="cssLabel">Hours (HMR):</span>
                    <asp:RadioButton ID="rdoLessThan250" runat="server" Text="Less than 250" GroupName="HMR" /><asp:RadioButton
                        ID="rdoMoreThan250" GroupName="HMR" runat="server" Text="250 to 2500" /><asp:RadioButton
                            ID="rdoHMRAll" runat="server" Checked="true" Text="All" GroupName="HMR" />
                </td>
                <td>
                    <asp:RadioButtonList ID="rdoYear" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="0" onclick="getYear(this.value);">I Year</asp:ListItem>
                        <asp:ListItem Value="1" onclick="getYear(this.value);">II Year</asp:ListItem>
                        <asp:ListItem Value="2" Selected="True" onclick="getYear(this.value);">Total</asp:ListItem>
                    </asp:RadioButtonList>
                    <span class="cssLabel">Problem Type:</span>
                    <asp:RadioButton ID="rdoPrimary" runat="server" Text="Primary" GroupName="Problem" />
                    <asp:RadioButton ID="rdoConsequences" GroupName="Problem" runat="server" Text="Consequences" />
                    <asp:RadioButton ID="rdoAllProblem" GroupName="Problem" runat="server" Text="All"
                        Checked="true" />
                </td>
            </tr>
        </table>
        <div class="cssButtonPanel">
        <asp:Button ID="btnViewGraph" Text="ViewGraph" ToolTip="ViewGraph" runat="server"
            OnClick="btnViewGraph_Click" />
        <span style="margin-left: 1%;"></span>
        <asp:Button ID="btnExcelGraph" Text="Excel Graph" ToolTip="Excel Graph" runat="server"
            OnClick="btnExcelGraph_Click" />
        <span style="margin-left: 1%;"></span>
        <asp:Button ID="btnPrint" Text="Print Excel" ToolTip="Print Excel" runat="server"
            OnClick="btnPrint_Click" />
            <asp:Button ID="btnExport" runat="server" Visible="false" OnClientClick="javascript:ExportGridWithSelection('print_Grid');"
                OnClick="btnExport_Click" Text="Export to Excel" ToolTip="Export to Excel" CssClass="cssButton" />
            <asp:Button ID="btnShow" OnClientClick="return getValidation();" runat="server" CssClass="cssButton"
                Text="Show" ToolTip="Show" OnClick="btnShow_Click" />
        </div>
        <br />
        <div id="print_Grid" style="overflow: auto; height: 250px; width: 1000px;">
            <asp:GridView OnDataBound="eventhandlerSerialNo" AutoGenerateColumns="false"  Width="100%"
                ID="grdCostReport" runat="server" EmptyDataText="No Records Found">
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
        </div>
         <div>
        <%-- <iframe runat="server" visible="false" width="100%" height="270px" id="rptChart"
            src="../Graph/Graph.aspx" frameborder="0" scrolling="no"></iframe>--%>
        <asp:Panel Visible="false" ID="rptChart" runat="server">
            <% =CreateChart() %>
        </asp:Panel>
        <asp:Panel Visible="false" ID="rptExcelChart" runat="server">
            <fieldset class="sectionBorder">
                <legend>Graph </legend>
                <asp:RadioButtonList ID="rdoChartType" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="0" onclick="getGraphs(this.value);">Bar Chart</asp:ListItem>
                    <asp:ListItem Value="1" onclick="getGraphs(this.value);">Pie Chart</asp:ListItem>
                    <asp:ListItem Value="2" onclick="getGraphs(this.value);">Line Chart</asp:ListItem>
                </asp:RadioButtonList>
                <asp:Image ID="imgGraph" runat="server" ImageUrl="~/UploadFile/Graphs/Summary_files/image004.gif"
                    Height="623px" Width="911px" />
            </fieldset>
        </asp:Panel>
    </div>
        <br />
        <br />
    </fieldset>
    <asp:HiddenField ID="hdnExport" runat="server" />
    <asp:HiddenField ID="hdnSelected" runat="server" Value="0" />
    <asp:HiddenField ID="hdnEngine" Value="0" runat="server" />

    <script type="text/javascript">
        var val = document.getElementById('ctl00_ContentPlaceHolder1_hdnSelected').value;
        getContent(val);
        var hdn = document.getElementById('ctl00_ContentPlaceHolder1_hdnEngine').value;
        getDiv(hdn);
    </script>

</asp:Content>
