<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true"
    CodeFile="QuarterWiseReport.aspx.cs" Inherits="View_Forms_Exceptions_QuarterWiseReport"
    Title="WMS" Theme="SkinFile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="/WMS/JS/FusionCharts.js"></script>

    <script type="text/javascript" language="javascript">
function getcheckSelection(id)
{

   if(id=='ctl00_ContentPlaceHolder1_rdoCost')
   {
      document.getElementById('ctl00_ContentPlaceHolder1_rdoCost').checked=true;
      document.getElementById('ctl00_ContentPlaceHolder1_rdoDefect').checked=false;
      document.getElementById('ctl00_ContentPlaceHolder1_chkDefectGroup').style.display='none';
      
   }
    if(id=='ctl00_ContentPlaceHolder1_rdoDefect')
   {
        document.getElementById('ctl00_ContentPlaceHolder1_rdoDefect').checked=true;
        document.getElementById('ctl00_ContentPlaceHolder1_rdoCost').checked=false;
        document.getElementById('ctl00_ContentPlaceHolder1_chkDefectGroup').style.display='';
   }
   
}
function  indexchange()
{

if (document.getElementById('ctl00_ContentPlaceHolder1_ddlselection').value==2 || document.getElementById('ctl00_ContentPlaceHolder1_ddlselection').value==3)
{

 document.getElementById('spnCost').style.display='none';
 document.getElementById('spnText').innerHTML='Quantity';
 document.getElementById('ctl00_ContentPlaceHolder1_rdoDefect').checked=true;
 document.getElementById('ctl00_ContentPlaceHolder1_rdoCost').checked=false;
 document.getElementById('trhmr').style.display='none';
 document.getElementById('trhmr2').style.display='none';
 
}
else
{

document.getElementById('spnCost').style.display='';
document.getElementById('spnText').innerHTML='Defect';
  document.getElementById('trhmr').style.display='';
  document.getElementById('trhmr2').style.display='';
}

}
function ClientValidateModel(source, arguments)

{  
      if (document.getElementById('ctl00_ContentPlaceHolder1_drpModel').value!=0)
         arguments.IsValid=true;
      else
         arguments.IsValid=false;
         
       
} 

function validate()
{
return true;
}

    </script>

    <script type="text/javascript">
 function getGraphs(selval)
    {
        var imagename = '';
        
        if(selval == 0)
        {
            imagename = 'image004.gif';
               
        }
        if(selval == 1)
        {
            imagename = 'image003.gif';
        }
        if(selval == 2)
        {
            imagename = 'image002.gif';
        }
        if(selval == 3)
        {
            imagename = 'image001.gif';
        }
         
            document.getElementById('ctl00_ContentPlaceHolder1_imgGraph').src='/WMS/UploadFile/Graphs/Summary_files/'+imagename;
           
                      
    }   
    
    function getYear(val)
    {
        //alert(val);
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
    
    </script>

    <fieldset class="sectionBorder">
        <legend>Generated Report </legend>
        <table border="0" width="95%" cellpadding="2" cellspacing="3">
            <tr>
                <td>
                    <span class="cssLabel">Report for:</span>
                    <asp:DropDownList ID="ddlselection" onchange="javaScript:indexchange();" runat="server">
                        <asp:ListItem Selected="true" Value="1">Defect</asp:ListItem>
                        <asp:ListItem Value="2">Production</asp:ListItem>
                        <asp:ListItem Value="3">Sales</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <span class="cssLabel">Model :</span>
                    <asp:DropDownList ID="drpModel" runat="server">
                    </asp:DropDownList>
                    <%--<asp:CustomValidator ID="CustomValidator" ErrorMessage="Select" ClientValidationFunction="ClientValidateModel"
                        ControlToValidate="drpModel" runat="server"></asp:CustomValidator>--%>
                </td>
                 <td>
                    <span class="cssLabel">Category :</span>
                    <asp:DropDownList ID="drpCategory" runat="server">
                    </asp:DropDownList>                    
                </td>
                 <td>
                    <span class="cssLabel">Clutch :</span>
                    <asp:DropDownList ID="drpClutch" runat="server">
                    </asp:DropDownList>                    
                </td>
                 <td>
                    <span class="cssLabel">Special :</span>
                    <asp:DropDownList ID="drpSpecial" runat="server">
                    </asp:DropDownList>                    
                </td>
               
               
            </tr>
            <tr>
             <td>
                    <span class="cssLabel">Month:</span>
                    <asp:DropDownList ID="drpMonth" runat="server">
                        <asp:ListItem Selected="true" Value="1">Jan</asp:ListItem>
                        <asp:ListItem Value="2">Feb</asp:ListItem>
                        <asp:ListItem Value="3">Mar</asp:ListItem>
                        <asp:ListItem Value="4">Apr</asp:ListItem>
                        <asp:ListItem Value="5">May</asp:ListItem>
                        <asp:ListItem Value="6">Jun</asp:ListItem>
                        <asp:ListItem Value="7">Jul</asp:ListItem>
                        <asp:ListItem Value="8">Aug</asp:ListItem>
                        <asp:ListItem Value="9">Sept</asp:ListItem>
                        <asp:ListItem Value="10">Oct</asp:ListItem>
                        <asp:ListItem Value="11">Nov</asp:ListItem>
                        <asp:ListItem Value="12">Dec</asp:ListItem>
                    </asp:DropDownList>
                    <span class="cssLabel">Year:</span>
                    <asp:DropDownList ID="drpYear" runat="server">
                        <asp:ListItem Value="2006" Selected="True">2006</asp:ListItem>
                        <asp:ListItem Value="2007">2007</asp:ListItem>
                        <asp:ListItem Value="2008">2008</asp:ListItem>
                        <asp:ListItem Value="2009">2009</asp:ListItem>
                        <asp:ListItem Value="2010">2010</asp:ListItem>
                        <asp:ListItem Value="2011">2011</asp:ListItem>
                        <asp:ListItem Value="2012">2012</asp:ListItem>
                        <asp:ListItem Value="2013">2013</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <span class="cssLabel">Last:</span>
                    <asp:TextBox ID="txtLastYear" Width="15px" MaxLength="2" runat="server" Text="3"></asp:TextBox>Year
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtLastYear"
                        ErrorMessage="*" ValidationExpression="^([0-9]*\s?[0-9]*)+$"> </asp:RegularExpressionValidator>
                </td>
             <td colspan="3">
               <span id="spnCost">
                        <asp:RadioButton ID="rdoCost" runat="server" Text="Cost" onclick="getcheckSelection(this.id);" />
                    </span><span id="spnDefect">
                        <asp:RadioButton ID="rdoDefect" Checked="true" runat="server" onclick="getcheckSelection(this.id);" /></span><span
                            id="spnText">Defect</span></td></tr>
            <tr id="trhmr" style="display: none">
                <td colspan="2">
                    <span class="cssLabel">Hours(HMR):</span>
                    <asp:RadioButton ID="rdoLessThan250" runat="server" Text="Less than 250" GroupName="HMR" /><asp:RadioButton
                        ID="rdoMoreThan250" GroupName="HMR" runat="server" Text="250 to 2500" /><asp:RadioButton
                            ID="rdoHMRAll" runat="server" Checked="true" Text="All" GroupName="HMR" /></td>
                <td>
              
                   <asp:RadioButtonList ID="rdoData" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True" Value="0" onclick="getDiv(this.value);">Overall</asp:ListItem>
                            <asp:ListItem Value="1" onclick="getDiv(this.value);">Engine</asp:ListItem>
                            <asp:ListItem Value="2" onclick="getDiv(this.value);">Tractor</asp:ListItem>
                        </asp:RadioButtonList></td>
               
                <td id="enginediv" style="display: none;"  colspan="2">
                    <span class="cssLabel">Engine:</span>
                    <asp:RadioButton ID="rdoAlwarEngine" runat="server" Text="Alwar" GroupName="Engine" /><asp:RadioButton
                        ID="rdoSimpsonEngine" GroupName="Engine" runat="server" Text="Simpson" /><asp:RadioButton
                            ID="rdoBothEngine" runat="server" Checked="true" Text="Both" GroupName="Engine" /></td>
                               <td id="placediv" colspan="2">
                    <span class="cssLabel">Place:</span>
                    <asp:RadioButton ID="rdoAlwar" runat="server" Text="Alwar" GroupName="Place" /><asp:RadioButton
                        ID="rdoBhopal" GroupName="Place" runat="server" Text="Bhopal" /><asp:RadioButton
                            ID="rdoAllPlace" runat="server" Checked="true" Text="Both" GroupName="Place" /></td>
            </tr>
            <tr id="trhmr2" style="display: none">
                <td colspan="2">
                    <span class="cssLabel">Problem Type:</span>
                    <asp:RadioButton ID="rdoPrimary" runat="server" Text="Primary" GroupName="Problem" />
                    <asp:RadioButton ID="rdoConsequences" GroupName="Problem" runat="server" Text="Consequences" />
                    <asp:RadioButton ID="rdoAllProblem" GroupName="Problem" runat="server" Text="All"
                        Checked="true" />
                </td>
                <td>
                    <asp:RadioButtonList ID="rdoYear" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="0" onclick="getYear(this.value);">I Year</asp:ListItem>
                        <asp:ListItem Value="1" onclick="getYear(this.value);">II Year</asp:ListItem>
                        <asp:ListItem Value="2" Selected="True" onclick="getYear(this.value);">Total</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
              
           <%--   <td colspan="2">
                    <span class="cssLabel">Product:</span>
                    <asp:RadioButton ID="rdoNew" runat="server" Text="New" GroupName="Prod" />
                    <asp:RadioButton ID="rdoRegular" GroupName="Prod" runat="server" Text="Regular" />
                    <asp:RadioButton ID="rdoAllModel" GroupName="Prod" runat="server" Text="All"
                        Checked="true" />
                </td>--%>
            </tr>
        </table>
    </fieldset>
    <br />
    <div class="cssButtonPanel" align="center">
        <asp:Button ID="btnViewTable" CausesValidation="true" Text="ViewTable" ToolTip="ViewTable"
            runat="server" OnClick="btnViewTable_Click" />
        <span style="margin-left: 1%;"></span>
        <asp:Button ID="btnViewGraph" Text="ViewGraph" ToolTip="ViewGraph" runat="server"
            OnClick="btnViewGraph_Click" />
        <span style="margin-left: 1%;"></span>
        <asp:Button ID="btnExcelGraph" Text="Excel Graph" ToolTip="Excel Graph" runat="server"
            OnClick="btnExcelGraph_Click" />
        <span style="margin-left: 1%;"></span>
        <asp:Button ID="btnPrint" Text="Print Excel" ToolTip="Print Excel" runat="server"
            OnClick="btnPrint_Click" />
    </div>
    <br />
    <div style="width: 100%; margin-left: 2%;">
        <table width="100%">
            <asp:GridView ID="GridView1" OnDataBound="eventhandlerSerialNo" AutoGenerateColumns="false"
                runat="server" Width="100%">
                <Columns>
                    <asp:BoundField HeaderText="#" ReadOnly="True">
                        <ItemStyle HorizontalAlign="center" />
                    </asp:BoundField>
                    <%--<asp:BoundField DataField="HMR_Range" HeaderText="HMR Range">
                        <ItemStyle HorizontalAlign="center" />
                    </asp:BoundField>--%>
                    <asp:BoundField DataField="DefectAmount" HeaderText="Cost">
                        <ItemStyle HorizontalAlign="center" />
                    </asp:BoundField>
                    <%-- <asp:BoundField DataField="IYear" HeaderText="IYear">
                        <ItemStyle HorizontalAlign="center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="IIYear" HeaderText="IIYear">
                        <ItemStyle HorizontalAlign="center" />
                    </asp:BoundField>--%>
                    <asp:BoundField DataField="Fyear" HeaderText="Fyear">
                        <ItemStyle HorizontalAlign="center" />
                    </asp:BoundField>
                </Columns>
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
        </table>
        </div><div >
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
<asp:HiddenField ID="hdnEngine" Value="0" runat="server" />
    <script type="text/javascript">
    checkEnable()
    </script>

    <script type="text/javascript">
     indexchange()
    </script>
 

    <script type="text/javascript">
    var hdn = document.getElementById('ctl00_ContentPlaceHolder1_hdnEngine').value;
    getDiv(hdn);
    </script>
</asp:Content>
