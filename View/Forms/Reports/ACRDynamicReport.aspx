<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeFile="ACRDynamicReport.aspx.cs" Inherits="View_Forms_Reports_ACRDynamicReport"
    Title="WMS" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 


    <script type="text/javascript">
function getDiv(val)
{
    document.getElementById('ctl00_ContentPlaceHolder1_hdnEngine').value = val;
    if(val=='0')
    {
         document.getElementById('placediv').style.display='';
         document.getElementById('enginediv').style.display='none';
    }
    else  if(val=='1')
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




       
        function ClientValidateBetweenDate(source, arguments)
       {
            var status=CheckDateValidation1();
            if(status)
            {
                arguments.IsValid=true;
            }
            else
            {
                arguments.IsValid=false;
            }
       }
       
  
    
    
    function getValidation()
       {
           
           var checkCheckBoxList=getCheckBoxStatus('ctl00_ContentPlaceHolder1_chkModelCodeList');
          var checkchkCategory=getCheckBoxStatus('ctl00_ContentPlaceHolder1_chkCategory');
           var checkchkClutchType=getCheckBoxStatus('ctl00_ContentPlaceHolder1_chkClutchType');
           var checkchkSpecialList=getCheckBoxStatus('ctl00_ContentPlaceHolder1_chkSpecialList');
           var checkDefectCode=getCheckBoxStatus('ctl00_ContentPlaceHolder1_chkDefectCode');
           var checkCVoiceCode=getCheckBoxStatus('ctl00_ContentPlaceHolder1_chkCVoiceCode');
           var checkItemGroup=getCheckBoxStatus('ctl00_ContentPlaceHolder1_chkItemGroup');
           var checkCulpritCode=getCheckBoxStatus('ctl00_ContentPlaceHolder1_chkCulpritCode');
             var chkDealerCode=getCheckBoxStatus('ctl00_ContentPlaceHolder1_chkDealerCode');
             
             var chkitemCode=getCheckBoxStatus('ctl00_ContentPlaceHolder1_chkItemCode');
             if (chkitemCode==false)
             {
             chkitemCode='0';
             }
             
           
          if(checkCheckBoxList=='0' && checkchkCategory=='0' && checkchkClutchType=='0' && checkchkSpecialList=='0' && checkDefectCode == '0' && checkCVoiceCode == '0' && checkItemGroup == '0' && checkCulpritCode == '0' && chkitemCode=='0')
            {
            
            alert('Please select atleast one model \n'+'Please select atleast one Category\n'+'Please select atleast one Clutch Type\n'+'Please select atleast one Special \n'+'Please select atleast one Defect Code \n'+'Please select atleast one CVoice Code \n'+'Please select atleast one Item Group\n'+'Please select atleast one Culprit Code \n' +'Please select atleast one Item Code \n' );
            return false;
            }
          else  
           if(checkCheckBoxList=='0')
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
          
             else if (checkDefectCode == '0')
             {
               alert('Please select atleast one Defect Code');
                return false;
            }
             else if (checkCVoiceCode == '0')
            {
               alert('Please select atleast one CVoice Code');
                return false;
            }
             else if (checkItemGroup == '0')
            {
               alert('Please select atleast one Item Group');
                return false;
            }
             else if (checkCulpritCode == '0')
            {
               alert('Please select atleast one Culprit Code');
                return false;
            }
             else if (chkDealerCode == '0')
            {
               alert('Please select atleast one Dealer');
                return false;
            }
            else if (chkitemCode=='0')
            {
               alert('Please Generate & select atleast one Item code');
                return false;
            }
            
            
          
           var startDate = document.getElementById('ctl00_ContentPlaceHolder1_txtFromDate').value;
           var endDate = document.getElementById('ctl00_ContentPlaceHolder1_txtToDate').value ;
          if (startDate !='' &&  endDate!='')
           {
            if (startDate == "")
            {
                return false ;
            }
            else
            {
                if (endDate == "")
                {
                    return false ;
                }
                else
                {            
                    var startdt;
                    startdt=startDate.split("/");
                    if (startdt.length !=null) 
                    {
                    if (startdt.length != 3) 
                    {
                        return false ;
                    }
                    }
                    else
                    {
                          return false ;
                    }
                    
                    startdt[0]=Number(startdt[0]);
                    if (startdt[0]<10)
                    {
                        startdt[0]='0'+startdt[0];
                    }
                    startdt[1]=Number(startdt[1]);
                    if (startdt[1]<10)
                    {
                        startdt[1]='0'+startdt[1];
                    }
                    var enddt;
                    enddt=endDate.split("/");
                     if(enddt.length!=null)
                     {
                     if (enddt.length != 3) 
                     {
                        return false ;
                     }
                      }
                      else
                      {
                           return false ;
                      }
                    enddt[0]=Number(enddt[0]);
                    if (enddt[0]<10)
                    {                 
                        enddt[0]='0'+enddt[0];
                    }              
                    
                    enddt[1]=Number(enddt[1]);             
                    if (enddt[1]<10)
                    {                 
                        enddt[1]='0'+enddt[1];
                    } 
                    var date1 = new Date(startdt[2],startdt[1],startdt[0]);
                    var date2 = new Date(enddt[2],enddt[1],enddt[0]);
                    if(date1 > date2 )
                    {
                       alert('From Date cannot be greater than To Date');
                        return false ;
                    }
                    
                  
                }
            }
}
            
            else
            {
                return true;
            }
       } 
       
       function validateitemgroup()
       {
       var checkItemGroup=getCheckBoxStatus('ctl00_ContentPlaceHolder1_chkItemGroup');
 if (checkItemGroup=='0')
 {
 alert('Plese select atleast one group');
 return false;
 }
 else
 {
 return true;
 }
 
       }
        
    </script>

    <fieldset class="sectionBorder">
        <legend>Dynamic ACR Report</legend>
        <table cellspacing="0" cellpadding="0" width="90%" style="margin: 2%">
            <tr>
                <td>
                    <span class="cssLabel">From Date:</span>
                        <asp:TextBox ID="txtFromDate"  runat="server" Width="75px"></asp:TextBox>
                
                    <a onclick="displayDatePicker('ctl00_ContentPlaceHolder1_txtFromDate', false, 'dmy', '/');"
                        href="#">
                       
                        <img src="/WMS/images/icon_calendar.gif" alt="" style="width: 20px" /></a>
           
                    <span class="cssLabel">To Date:</span>
                     <asp:TextBox ID="txtToDate"  runat="server" Width="75px"></asp:TextBox>
                  
                    <a onclick="displayDatePicker('ctl00_ContentPlaceHolder1_txtToDate', false, 'dmy', '/');"
                        href="#">
                        <img src="/WMS/images/icon_calendar.gif" alt="" style="width: 20px" /></a>
                 
                </td>
                <td id="tdProdMonth">
                    <span class="cssLabel">From Month:</span>
                    <asp:DropDownList ID="drpFromMonth" runat="server">
                    </asp:DropDownList>
                    <span class="cssLabel">To Month:</span>
                    <asp:DropDownList ID="drpToMonth" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                
                 <td>
                 <span>
                    <asp:CustomValidator ID="CustomValidator3" runat="server" ErrorMessage="Invalid Date"
                        ClientValidationFunction="ValidDate" ControlToValidate="txtFromDate"></asp:CustomValidator>              
                         </span> <span>
                    <asp:CustomValidator ID="CustomValidator2" runat="server" ErrorMessage="Invalid Date"
                        ClientValidationFunction="ValidDate" ControlToValidate="txtToDate"></asp:CustomValidator>        </span></td>
            </tr>
        </table>
        <table width="96%" border="0" cellpadding="3" cellspacing="4">
            <tr>
                <td style="width: 32%;">
                    <h5>
                        <asp:Label ID="lblModeCode" runat="server" Text="Model"></asp:Label></h5>
                    <asp:Panel Height="180px" BorderWidth="1px" ID="pnlModelCodeList" runat="server"
                        BorderColor="#00678e" ScrollBars="Vertical">
                        <asp:CheckBoxList ID="chkModelCodeList" CellSpacing="4" ToolTip="select Model" RepeatColumns="4"
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
                <td style="width: 32%;">
                    <h5>
                        <asp:Label ID="lblDefectCode" runat="server" Text="Defect&nbsp;"></asp:Label></h5>
                    <asp:Panel Height="180px" BorderWidth="1px" BorderColor="#00678e" ID="pnlDefectCode"
                        runat="server" ScrollBars="Vertical">
                        <asp:CheckBoxList ID="chkDefectCode" CellSpacing="2" ToolTip="select Defect Code"
                            RepeatColumns="3" runat="server">
                        </asp:CheckBoxList>
                    </asp:Panel>
                    <input type="checkbox" id="chkAllDefectCode" onclick="javascript:SetAllCheckBoxes('ctl00_ContentPlaceHolder1_chkDefectCode',this.id);" />Select
                    All
                </td>
            </tr>
            <tr>
                <td style="width: 32%;">
                    <h5>
                        <asp:Label ID="lblCVoiceCode" runat="server" Text="Customer Voice "></asp:Label></h5>
                    <asp:Panel Height="180px" BorderWidth="1px" ID="Panel1" runat="server" BorderColor="#00678e"
                        ScrollBars="Vertical">
                        <asp:CheckBoxList ID="chkCVoiceCode" CellSpacing="5" ToolTip="select Customer Voice Code"
                            RepeatColumns="2" runat="server">
                        </asp:CheckBoxList>
                    </asp:Panel>
                    <input type="checkbox" id="chkAllCVoiceCode" onclick="javascript:SetAllCheckBoxes('ctl00_ContentPlaceHolder1_chkCVoiceCode',this.id);" />Select
                    All
                </td>
                <td style="width: 32%;">
                    <h5>
                        <asp:Label ID="lblItemGroup" runat="server" Text="Item Group"></asp:Label></h5>
                    <atlas:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />
                    <atlas:UpdatePanel runat="server" ID="UpdatePanel1">
                        <ContentTemplate>
                            <asp:TextBox ID="txtItem" runat="server"></asp:TextBox>
                            <asp:Button ID="btnGo" runat="server"
                                Text="Go" Width="30px" CssClass="cssButton" OnClick="btnGo_Click1" />
                                <asp:Button ID="btnitem" runat="server" OnClientClick="return validateitemgroup();"
                                Text="Generate Item code list"  CssClass="cssButton" OnClick="btnitem_Click" />
                      
                                 &nbsp;<atlas:UpdateProgress ID="UpdateProgress1" runat="server">
                <ProgressTemplate>
         
                  <font color="red"  style="font-family: Verdana; font-size: 18px;"><b>Please wait...</b></font>
             
            </ProgressTemplate>
            </atlas:UpdateProgress>
                            <asp:Panel Height="180px" BorderWidth="1px" ID="Panel2" runat="server" BorderColor="#00678e"
                                ScrollBars="Vertical">
                                <asp:CheckBoxList ID="chkItemGroup"   CellSpacing="5" ToolTip="select Item Group" RepeatColumns="3"
                                    runat="server" >
                                </asp:CheckBoxList>
                            </asp:Panel>
                            
                            
                         </ContentTemplate>
                    </atlas:UpdatePanel>
                    <input type="checkbox" id="chkAllItemGroup" onclick="javascript:SetAllCheckBoxes('ctl00_ContentPlaceHolder1_chkItemGroup',this.id);" />Select
                    All
                </td>
                <%--Add By VD--%>
                <td style="width: 32%;">
                    <h5>
                        <asp:Label ID="Label1" runat="server" Text="Item Code"></asp:Label></h5>
                    
                     <atlas:UpdatePanel runat="server" ID="UpdatePanel2"> 
                    
                        <ContentTemplate>
                            
                            <asp:Panel Height="180px" BorderWidth="1px" ID="Panel4" runat="server" BorderColor="#00678e"
                                ScrollBars="Vertical">
                                <asp:CheckBoxList ID="chkItemCode"  CellSpacing="5" ToolTip="select Item Code" RepeatColumns="3"
                                    runat="server" >
                                </asp:CheckBoxList>
                            </asp:Panel>
                            
                            
                        </ContentTemplate>
                    </atlas:UpdatePanel> 
                    <input type="checkbox" id="chkAllItemCode" onclick="javascript:SetAllCheckBoxes('ctl00_ContentPlaceHolder1_chkItemCode',this.id);" />Select
                    All
                </td>
                <%--End--%>
                <td style="width: 32%;">
                    <h5>
                        <asp:Label ID="lblCulpritCode" runat="server" Text="Culprit "></asp:Label></h5>
                    <asp:Panel Height="180px" BorderWidth="1px" ID="Panel3" runat="server" BorderColor="#00678e"
                        ScrollBars="Vertical">
                        <asp:CheckBoxList ID="chkCulpritCode" CellSpacing="5" ToolTip="select Culprit Code"
                            RepeatColumns="2" runat="server">
                        </asp:CheckBoxList>
                    </asp:Panel>
                    <input type="checkbox" id="chkAllCulpritCode" onclick="javascript:SetAllCheckBoxes('ctl00_ContentPlaceHolder1_chkCulpritCode',this.id);" />Select
                    All
                </td>
            </tr>
            <tr>
                <td style="width: 100%;" colspan="3">
                    <span class="cssLabel">Region: </span>
                    <div>
                        <%--<asp:DropDownList ID="drpRegion" runat="server" onchange="FillDealerBychk();">
                        </asp:DropDownList>--%>
                        <asp:DropDownList ID="drpRegion" AutoPostBack="true" runat="server" OnSelectedIndexChanged="drpRegion_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <h5>
                        <asp:Label ID="lblDealer" runat="server" Text="Dealer"></asp:Label></h5>
                    <asp:Panel Height="180px" BorderWidth="1px" ID="pnlDealerCode" BorderColor="#00678e"
                        runat="server" ScrollBars="Vertical">
                        <asp:CheckBoxList ID="chkDealerCode" CellSpacing="5" ToolTip="select Dealer Code"
                            RepeatColumns="4" runat="server">
                        </asp:CheckBoxList>
                        <table id="tableDealer" width="90%">
                            <tr>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <%-- <input type="checkbox" id="chkAllDealerCode" onclick="chkedMultipleItem();" />Select
                    All--%>
                    <input type="checkbox" id="chkAllDealerCode" onclick="javascript:SetAllCheckBoxes('ctl00_ContentPlaceHolder1_chkDealerCode',this.id);" />Select
                    All
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td>
                    <b>Problem Type:</b>
                    <asp:RadioButton ID="rdoPrimary" runat="server" Text="Primary" GroupName="Problem" />
                    <asp:RadioButton ID="rdoConsequences" GroupName="Problem" runat="server" Text="Consequences" />
                    <asp:RadioButton ID="rdoAllProblem" GroupName="Problem" runat="server" Text="All"
                        Checked="true" />
                </td>
                <td>
                    <b>Hours (HMR)</b>
                    <asp:RadioButton ID="rdoLessThan250" runat="server" Text="Less than 250" GroupName="HMR" /><asp:RadioButton
                        ID="rdoMoreThan250" GroupName="HMR" runat="server" Text="250 to 2500" /><asp:RadioButton
                            ID="rdoAll" runat="server" Checked="true" Text="All" GroupName="HMR" /></td>
                <td>
                    <div>
                        <asp:RadioButtonList ID="rdoData" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True" Value="0" onclick="getDiv(this.value);">Overall</asp:ListItem>
                            <asp:ListItem Value="1" onclick="getDiv(this.value);">Engine</asp:ListItem>
                            <asp:ListItem Value="2" onclick="getDiv(this.value);">Tractor</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div id="placediv">
                        <b>Place</b>
                        <asp:RadioButton ID="rdoAlwar" runat="server" Text="Alwar" GroupName="Place" /><asp:RadioButton
                            ID="rdoBhopal" GroupName="Place" runat="server" Text="Bhopal" /><asp:RadioButton
                                ID="rdoAllPlace" runat="server" Checked="true" Text="Both" GroupName="Place" />
                    </div>
                    <div id="enginediv" style="display: none;">
                        <b>Engine</b>
                        <asp:RadioButton ID="rdoAlwarEngine" runat="server" Text="Alwar" GroupName="Engine" /><asp:RadioButton
                            ID="rdoSimpsonEngine" GroupName="Engine" runat="server" Text="Simpson" /><asp:RadioButton
                                ID="rdoBothEngine" runat="server" Checked="true" Text="Both" GroupName="Engine" />
                    </div>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hdnEngine" Value="0" runat="server" />
        <asp:HiddenField ID="hdnCheckBoxID" runat="server" />
        <asp:HiddenField ID="hdnDealerCount" runat="server" />
        <asp:HiddenField ID="hdnItemNameList" runat="server" />
        <asp:HiddenField ID="hdnchkIDList" runat="server" />
        <asp:HiddenField ID="hdnCourierID" runat="server" />
    </fieldset>
    <div class="cssButtonPanel">
        <asp:Button ToolTip="Generate Report" ID="btnGenerate" CausesValidation="true" runat="server"
            Text="Generate Report" OnClientClick="return getValidation();" OnClick="btnGenerate_Click" />
    </div>
    <asp:Literal ID="literal1" runat="server"></asp:Literal>

    <script type="text/javascript">
   //FillDealerBychk();
    var hdn = document.getElementById('ctl00_ContentPlaceHolder1_hdnEngine').value;
    getDiv(hdn);
    
    </script>

</asp:Content>
