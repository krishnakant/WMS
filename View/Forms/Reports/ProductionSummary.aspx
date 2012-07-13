<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true"
    CodeFile="ProductionSummary.aspx.cs" Inherits="View_Forms_Reports_ProductionSummary"
    ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 <script type="text/javascript">
 
  function getValidation()
       {
           var checkCheckBoxList=getCheckBoxStatus('ctl00_ContentPlaceHolder1_chkModelCodeList');
           var checkchkCategory=getCheckBoxStatus('ctl00_ContentPlaceHolder1_chkCategory');
           var checkchkClutchType=getCheckBoxStatus('ctl00_ContentPlaceHolder1_chkClutchType');
           var checkchkSpecialList=getCheckBoxStatus('ctl00_ContentPlaceHolder1_chkSpecialList');
           
        if(checkCheckBoxList=='0' && checkchkCategory=='0' && checkchkClutchType=='0' && checkchkSpecialList=='0')
            {
            alert('Please select atleast one model \n'+'Please select atleast one Category\n'+'Please select atleast one Clutch Type\n'+'Please select atleast one Special  ');
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
         var Flag;
         
       function SetFlag(Id)
       {
       
       Flag=1;
     
       getPopUp(Id);
     
       }
       
       function getPopUp(id)
    {
       var From;
       var To;
      From=(document.getElementById('ctl00_ContentPlaceHolder1_drpFromMonth').value);
       To=(document.getElementById('ctl00_ContentPlaceHolder1_drpToMonth').value);
       var myStringtest = id;
       var ModelList='';
      
       ModelList=GetValueOfSelectedCheckBox('ctl00_ContentPlaceHolder1_chkModelCodeList',"ModelGroupName");
      
       var  ModelCategoryIDList=GetValueOfSelectedCheckBox('ctl00_ContentPlaceHolder1_chkCategory',"ModelCategory");
       var ClutchTypeIDList=GetValueOfSelectedCheckBox('ctl00_ContentPlaceHolder1_chkClutchType',"ClutchType");
       var  ModelSpecialList=GetValueOfSelectedCheckBox('ctl00_ContentPlaceHolder1_chkSpecialList',"ModelSpecial");
       var mySplitResulttest = myStringtest.split("_");
       var ID =  mySplitResulttest[0]+'_'+mySplitResulttest[1]+'_'+mySplitResulttest[2]+'_'+mySplitResulttest[3];
       var Year=ID+'_hdnYear';   
       Year = document.getElementById(Year).value;
       var winSettings = 'center=yes,left=30px,top=70px,status=no,scrollbars=yes,menubar=no,toolbar=no,resizable=no,width=950,height=500';
	   window.open('/WMS/View/Forms/Reports/ProductionPopup.aspx?ModelList=('+ ModelList +')&ModelCategoryIDList=('+ModelCategoryIDList+')&ClutchTypeIDList=('+ClutchTypeIDList+')&ModelSpecialList=('+ModelSpecialList+')&Year='+Year+'&From='+From+'&To='+To+'&Flag='+Flag+' ','',winSettings);
	   Flag=0;
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
        <legend>Production Summary</legend>
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
                <td colspan="4" width="400px" id="tdProdMonth">
                    <span class="cssLabel">From Month:</span>
                    <asp:DropDownList ID="drpFromMonth" runat="server">
                    </asp:DropDownList>
                    <span class="cssLabel">To Month:</span>
                    <asp:DropDownList ID="drpToMonth" runat="server">
                    </asp:DropDownList>
                    
                </td>
            </tr>
        </table>
        <div class="cssButtonPanel">
            <asp:Button ID="btnExport" runat="server" Visible="false" Text="Export to Excel"
                ToolTip="Export to Excel" CssClass="cssButton" OnClick="btnExport_Click" OnClientClick="return CallExport('divGrid');" />
            <asp:Button ID="btnShow" runat="server" Text="Show" OnClientClick="return getValidation();" ToolTip="Show" CssClass="cssButton"
                OnClick="btnShow_Click" />
        </div>
        <div id="divGrid">
            <asp:GridView ID="grdProdData" runat="server" OnDataBound="eventhandlerSerialNo"
                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                CellPadding="3" AutoGenerateColumns="false" EmptyDataText="No Data Found" AllowPaging="true"
                PageSize="500" Width="100%" OnPageIndexChanging="grdsalesData_Paging">
                <Columns>
                    <asp:BoundField ItemStyle-HorizontalAlign="Center" HeaderText="#" />
                    <asp:BoundField DataField="Production_Month_Year" HeaderStyle-HorizontalAlign="Left" HeaderText="Production Month" />
                   <%-- <asp:BoundField DataField="Production" ItemStyle-HorizontalAlign="Center" HeaderText="Production" />                    --%>
                
                 <asp:TemplateField HeaderText="Production">
                        <ItemTemplate>
                            <%--<asp:HiddenField ID="hdnMonthID" runat="server" Value='<%#Bind("Sales_Month") %>' />--%>
                            <asp:HiddenField ID="hdnYear" runat="server" Value='<%#Bind("Production_Month_Year") %>' />
                            <asp:LinkButton ID="lnkQuantity" Text='<%#Bind("Production") %>' OnClientClick="javascript:return getPopUp(this.id); "
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
