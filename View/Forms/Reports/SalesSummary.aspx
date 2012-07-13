<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true"
    CodeFile="SalesSummary.aspx.cs" Inherits="View_Forms_Reports_SalesSummary" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
function ClientValidateRegion(val)
{
     
    if(val=='-1')
    {
        alert('Please select one region');
        
        return false;
    }
    else
    {
        return true;
    }
    
}

 function getValidation()
       {
           var checkCheckBoxList=getCheckBoxStatus('ctl00_ContentPlaceHolder1_chkModelCodeList');
           var checkchkCategory=getCheckBoxStatus('ctl00_ContentPlaceHolder1_chkCategory');
           var checkchkClutchType=getCheckBoxStatus('ctl00_ContentPlaceHolder1_chkClutchType');
           var checkchkSpecialList=getCheckBoxStatus('ctl00_ContentPlaceHolder1_chkSpecialList');
            var drpRegion=document.getElementById('ctl00_ContentPlaceHolder1_drpRegion').value;
             
           if(checkCheckBoxList=='0' && checkchkCategory=='0' && checkchkClutchType=='0' && checkchkSpecialList=='0' && drpRegion == '-1')
            {
            alert('Please select atleast one model \n'+'Please select atleast one Category\n'+'Please select atleast one Clutch Type\n'+'Please select atleast one Special \n'+'Please select atleast one Region ');
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
          else if (drpRegion == '-1')
            {
               alert('Please select atleast one Region');
                return false;
            }
            else
            {
                return true;
            }
       } 
       
        
       
    </script>

    <script type="text/javascript">

function getPopUp(id)
    {
      
       var myStringtest = id;
       var ModelList='';
      
       ModelList=GetValueOfSelectedCheckBox('ctl00_ContentPlaceHolder1_chkModelCodeList',"ModelGroupName");
      
       var  ModelCategoryIDList=GetValueOfSelectedCheckBox('ctl00_ContentPlaceHolder1_chkCategory',"ModelCategory");
       var ClutchTypeIDList=GetValueOfSelectedCheckBox('ctl00_ContentPlaceHolder1_chkClutchType',"ClutchType");
       var  ModelSpecialList=GetValueOfSelectedCheckBox('ctl00_ContentPlaceHolder1_chkSpecialList',"ModelSpecial");
      
       var RegionID = document.getElementById('ctl00_ContentPlaceHolder1_drpRegion').value;
    
       var mySplitResulttest = myStringtest.split("_");
       var ID =  mySplitResulttest[0]+'_'+mySplitResulttest[1]+'_'+mySplitResulttest[2]+'_'+mySplitResulttest[3];
       var MonthID=ID+'_hdnMonthID';  
       var Year=ID+'_hdnYear';   
           MonthID = document.getElementById(MonthID).value;
           Year = document.getElementById(Year).value;
        var winSettings = 'center=yes,left=30px,top=70px,status=no,scrollbars=yes,menubar=no,toolbar=no,resizable=no,width=950,height=500';
		 window.open('/WMS/View/Forms/Reports/SalesPopup.aspx?ModelList=('+ ModelList +')&ModelCategoryIDList=('+ModelCategoryIDList+')&ClutchTypeIDList=('+ClutchTypeIDList+')&ModelSpecialList=('+ModelSpecialList+')&RegionID='+RegionID+'&MonthID='+MonthID+'&Year='+Year+' ','',winSettings);
		 return false;
    }
    
    function GetValueOfSelectedCheckBox(cbControl,ColumnName)
    {   
        var chktext='';
        var chkBoxList = document.getElementById(cbControl);
         
        var chkBoxCount= chkBoxList.getElementsByTagName("Input");
           var chktr = chkBoxList.getElementsByTagName('tr');
        var m=1;
        for(var i=0; i<chktr.length; i++)
        {
         var chktd = chktr[i].getElementsByTagName('td');
        
         for(var j=0; j<chktd.length; j++)
            {
               var chkinput = chktd[j].getElementsByTagName('input');
               var chklabel= chktd[j].getElementsByTagName('label');                             
               for(k=0;k<chkinput.length;k++)
                {                    
                    var chkopt = chkinput[k];                    
                      
                    if(chkopt.checked)
                    {
                      if(chklabel[k].innerText=='Single Clutch')
                      {
                       chklabel[k].innerText='S/C';
                      }
                      if(chklabel[k].innerText=='Double Clutch')
                      {
                      chklabel[k].innerText='D/C';
                      }
                       if(chktext=="")
                       {
                         if(chklabel[k].innerText=='NA')
                       {
                         chktext="("+ColumnName+" is null)";
                       }
                       else
                       {
                        chktext=" ("+ColumnName+"='"+chklabel[k].innerText+"')";
                       }
                       }
                       else if(chklabel[k].innerText=='NA')
                       {
                        chktext=chktext+" or ("+ColumnName+" is null)";
                       }
                       else
                       {
                       chktext=chktext+" or ("+ColumnName+"='"+chklabel[k].innerText+"')";
                       }
                        //chkText = chkText + chklabel[k].innerText + ',';
                    }
                } 
            } 
        
        
        
        
        /*for(var i=0;i<chkBoxCount.length;i++)
        {
           if(chkBoxCount[i].checked)
           {
           
           
                if(strCheckBoxParameter=='')
                {
                  strCheckBoxParameter=" ("+ColumnName+"="+chkBoxCount[i]+")";
                }
                else
                {
                  strCheckBoxParameter=strCheckBoxParameter+" or ("+ColumnName+"="+chkBoxCount[i]+")";
                }
           } 
           }*/
           
           m++;
        }
     
        return chktext; 
    }




    </script>

    <fieldset class="sectionBorder">
        <legend>Sales Summary</legend>
        <table>
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
                <td>
                    <span class="cssLabel">Region:</span>
                    <asp:DropDownList ID="drpRegion" runat="server" ToolTip="Region" onchange="javascript:return ClientValidateRegion(this.value);">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div class="cssButtonPanel">
            <asp:Button ID="btnExport" runat="server" Visible="false" Text="Export to Excel"
                ToolTip="Export to Excel" CssClass="cssButton" OnClick="btnExport_Click" OnClientClick="return CallExport('divGrid');" />
            <asp:Button ID="btnShow" OnClientClick="return getValidation();" runat="server" Text="Show"
                ToolTip="Show" CssClass="cssButton" OnClick="btnShow_Click" />
        </div>
        <div id="divGrid">
            <asp:GridView ID="grdProdData" runat="server" OnDataBound="eventhandlerSerialNo"
                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                CellPadding="3" AutoGenerateColumns="false" EmptyDataText="No Data Found" Width="100%">
                <Columns>
                    <asp:BoundField ItemStyle-HorizontalAlign="Center" HeaderText="#" />
                    <asp:BoundField DataField="Sales_Period" HeaderStyle-HorizontalAlign="Left" HeaderText="Sales Period" />
                    
                    <asp:TemplateField HeaderText="Quantity">
                        <ItemTemplate>
                           <asp:HiddenField ID="hdnMonthID" runat="server" Value='<%#Bind("Sales_Month") %>' />
                            <asp:HiddenField ID="hdnYear" runat="server" Value='<%#Bind("Sales_Year") %>' />
                            <asp:LinkButton ID="lnkQuantity" Text='<%#Bind("Quantity") %>' OnClientClick="javascript:return getPopUp(this.id); "
                                runat="server"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
        </div>
    </fieldset>
    <asp:HiddenField ID="hdnExport" runat="server" />
</asp:Content>
