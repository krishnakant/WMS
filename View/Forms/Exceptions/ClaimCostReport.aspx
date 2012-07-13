<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true"
    CodeFile="ClaimCostReport.aspx.cs" Inherits="View_Forms_Exceptions_ClaimCostReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="/WMS/JS/FusionCharts.js"></script>

    <script type="text/javascript" language="javascript">



function ClientValidateModel(source, arguments)

{  
      if (document.getElementById('ctl00_ContentPlaceHolder1_drpModel').value!=0)
         arguments.IsValid=true;
      else
         arguments.IsValid=false;
         
       
} 

function checkValidation()
{
if (document.getElementById('ctl00_ContentPlaceHolder1_ddlselection').value==1)
  
  {
   var check=getCheckBoxStatus('ctl00_ContentPlaceHolder1_chkDefectGroup');
   if(!check){alert('please select Defect Group Name'); return false;}
   }
 }
 
 function  indexchange()
{

if (document.getElementById('ctl00_ContentPlaceHolder1_ddlselection').value==2  )
{

 document.getElementById('spnCost').style.display='none';
 document.getElementById('spnText').innerHTML='Quantity';
 document.getElementById('ctl00_ContentPlaceHolder1_rdoDefect').checked=true;
  document.getElementById('ctl00_ContentPlaceHolder1_rdoCost').checked=false;
  document.getElementById('tbldefect').style.display='none';
  document.getElementById('tdregion').style.display='none';
 
}
else if(document.getElementById('ctl00_ContentPlaceHolder1_ddlselection').value==3)
{
 document.getElementById('spnCost').style.display='none';
 document.getElementById('spnText').innerHTML='Quantity';
 document.getElementById('ctl00_ContentPlaceHolder1_rdoDefect').checked=true;
  document.getElementById('ctl00_ContentPlaceHolder1_rdoCost').checked=false;
  document.getElementById('tbldefect').style.display='none';
  document.getElementById('tdregion').style.display='';
}
else
if (document.getElementById('ctl00_ContentPlaceHolder1_ddlselection').value==1)
{
document.getElementById('tbldefect').style.display='';
document.getElementById('spnCost').style.display='';
 document.getElementById('tdregion').style.display='';

document.getElementById('spnText').innerHTML='Defect';
 
}

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

    <fieldset class="sectionBorder" style="width: 100%;">
        <legend></legend>
        <table border="0" width="100%" cellpadding="2" cellspacing="3">
            <tr>
                <td style="color: #1b0b6e; width: 203px;">
                    Product:
                    <asp:RadioButton ID="rdoNew" runat="server" Text="New" GroupName="Product" /><asp:RadioButton
                        ID="rdoRegular" GroupName="Product" runat="server" Text="Regular" /><asp:RadioButton
                            ID="rdoAll" runat="server" Checked="true" Text="All" GroupName="Product" /></td>
            </tr>
            <tr>
                <td style="color: #1b0b6e; width: 203px;">
                    Selection:
                    <asp:DropDownList ID="ddlselection" onchange="javaScript:indexchange();" runat="server">
                        <asp:ListItem Selected="true" Value="1">Defect</asp:ListItem>
                        <asp:ListItem Value="2">Production</asp:ListItem>
                        <asp:ListItem Value="3">Sales</asp:ListItem>
                    </asp:DropDownList></td>
                <td style="color: #1b0b6e; width: 228px;">
                    Month:
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
                    Year:
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
                        <asp:ListItem Value="2013">2014</asp:ListItem>
                        <asp:ListItem Value="2013">2015</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="color: #1b0b6e;">
                    Last :
                    <asp:TextBox ID="txtLastYear" Width="15px" MaxLength="2" Height="15px" runat="server"
                        Text="3"></asp:TextBox>Year
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtLastYear"
                        ErrorMessage="*" ValidationExpression="^([0-9]*\s?[0-9]*)+$"> </asp:RegularExpressionValidator>
                </td>
                <td id="tdregion">
                    <span class="cssLabel">Region:</span>
                    <asp:DropDownList ID="drpRegion" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="color: #1b0b6e; width: 203px;">
                    <span id="spnCost">
                        <asp:RadioButton ID="rdoCost" runat="server" Text="Cost" GroupName="Cost" />
                    </span><span id="spnDefect">
                        <asp:RadioButton ID="rdoDefect" Checked="true" runat="server" GroupName="Cost" /></span><span
                            id="spnText">Defect</span>
                </td>
            </tr>
        </table>
        <table id="tbldefect" style="display: none" cellpadding="4" cellspacing="2">
            <tr>
                <td style="color: #1b0b6e; width: 350px;">
                    Hours (HMR):
                    <asp:RadioButton ID="rdoLessThan250" runat="server" Text="Less than 250" GroupName="HMR" /><asp:RadioButton
                        ID="rdoMoreThan250" GroupName="HMR" runat="server" Text="250 to 2500" /><asp:RadioButton
                            ID="rdoHMRAll" runat="server" Checked="true" Text="All" GroupName="HMR" /></td>
                <td style="color: #1b0b6e;">
                    <asp:RadioButtonList ID="rdoData" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="0" onclick="getDiv(this.value);">Overall</asp:ListItem>
                        <asp:ListItem Value="1" onclick="getDiv(this.value);">Engine</asp:ListItem>
                        <asp:ListItem Value="2" onclick="getDiv(this.value);">Tractor</asp:ListItem>
                    </asp:RadioButtonList></td>
                <td id="enginediv" style="display: none;" colspan="2">
                    <span style="color: #1b0b6e;">Engine:</span>
                    <asp:RadioButton ID="rdoAlwarEngine" runat="server" Text="Alwar" GroupName="Engine" /><asp:RadioButton
                        ID="rdoSimpsonEngine" GroupName="Engine" runat="server" Text="Simpson" /><asp:RadioButton
                            ID="rdoBothEngine" runat="server" Checked="true" Text="Both" GroupName="Engine" /></td>
                <td id="placediv" colspan="2">
                    <span style="color: #1b0b6e;">Place:</span>
                    <asp:RadioButton ID="rdoAlwar" runat="server" Text="Alwar" GroupName="Place" /><asp:RadioButton
                        ID="rdoBhopal" GroupName="Place" runat="server" Text="Bhopal" /><asp:RadioButton
                            ID="rdoAllPlace" runat="server" Checked="true" Text="Both" GroupName="Place" /></td>
            </tr>
            <tr>
                <%--<td style="color: #1b0b6e;">
                        <b>Defect Group:</b>
                    </td>--%>
                <td colspan="2" style="color: #1b0b6e;">
                    Problem Type:
                    <asp:RadioButton ID="rdoPrimary" runat="server" Text="Primary" GroupName="Problem" />
                    <asp:RadioButton ID="rdoConsequences" GroupName="Problem" runat="server" Text="Consequences" />
                    <asp:RadioButton ID="rdoAllProblem" GroupName="Problem" runat="server" Text="All"
                        Checked="true" />
                </td>
                <td style="color: #1b0b6e;">
                    <asp:RadioButtonList ID="rdoYear" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="0" onclick="getYear(this.value);">I Year</asp:ListItem>
                        <asp:ListItem Value="1" onclick="getYear(this.value);">II Year</asp:ListItem>
                        <asp:ListItem Value="2" Selected="True" onclick="getYear(this.value);">Total</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td style="color: #1b0b6e;">
                    <asp:CheckBoxList Visible="false" ID="chkDefectGroup" RepeatColumns="3" runat="server"
                        ToolTip="Defect Group">
                    </asp:CheckBoxList></td>
            </tr>
        </table>
    </fieldset>
    <br />
    <div class="cssButtonPanel" align="center">
        <asp:Button ID="btnViewTable" CausesValidation="true" OnClientClick="return checkValidation();"
            Text="ViewTable" ToolTip="ViewTable" runat="server" OnClick="btnViewTable_Click" />
        <span style="margin-left: 1%;"></span>
        <asp:Button ID="btnViewGraph" Text="View Flash Graph" ToolTip="View Flash Graph"
            OnClientClick="return checkValidation();" OnClick="btnViewGraph_Click" runat="server" />
        <span style="margin-left: 1%;"></span>
        <asp:Button ID="btnExcelGraph" OnClientClick="return checkValidation();" Text="Excel Graph"
            ToolTip="Excel Graph" runat="server" OnClick="btnExcelGraph_Click" />
        <span style="margin-left: 1%;"></span>
        <asp:Button ID="btnPrint" Visible="false" OnClientClick="return checkValidation();"
            Text="Print Excel" ToolTip="Print Excel" runat="server" OnClick="btnPrint_Click" />
    </div>
    <br />
    <asp:GridView ID="GridView1" OnDataBound="eventhandlerSerialNo" AutoGenerateColumns="false"
        runat="server" Width="100%">
        <Columns>
            <asp:BoundField HeaderText="#" ReadOnly="True">
                <ItemStyle HorizontalAlign="center" />
            </asp:BoundField>
            <asp:BoundField DataField="DefectAmount" HeaderText="Cost">
                <ItemStyle HorizontalAlign="center" />
            </asp:BoundField>
            <asp:BoundField DataField="FinancialQtr" HeaderText="Qtr">
                <ItemStyle HorizontalAlign="center" />
            </asp:BoundField>
        </Columns>
        <FooterStyle BackColor="White" ForeColor="#000066" />
        <RowStyle ForeColor="#000066" />
        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
    </asp:GridView>
    <table border="0" width="100%" cellpadding="6" cellspacing="3">
        <tr>
            <td>
                <asp:Label ID="lblHmr" Width="40px" Font-Bold="true" Visible="false" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblProduct" Width="40px" Font-Bold="true" Visible="false" runat="server"></asp:Label></td>
        </tr>
    </table>
    <%--<iframe runat="server" visible="false" width="100%" height="270px" id="rptChart"
            src="../Graph/Graph.aspx" frameborder="0" scrolling="no"></iframe>--%>
    <asp:Panel Visible="false" ID="rptChart" Width="100%" Height="260px" runat="server">
        '<% =CreateChart() %>
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
    <asp:HiddenField ID="hdnEngine" Value="0" runat="server" />

    <script type="text/javascript">
     indexchange()
    </script>

    <script type="text/javascript">
    var hdn = document.getElementById('ctl00_ContentPlaceHolder1_hdnEngine').value;
    getDiv(hdn);
    </script>

</asp:Content>
