<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" ValidateRequest="false"
    AutoEventWireup="true" CodeFile="CvoiceGRoupWiseACR.aspx.cs" Inherits="View_Forms_Reports_CvoiceGRoupWiseACR"
    Title="WMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
 function ClientValidateModel(source, arguments){
      if (document.getElementById('ctl00_ContentPlaceHolder1_ddlGroup').value!='')
         arguments.IsValid=true;
      else
         arguments.IsValid=false;
   }
    function getDiv(val)
    {
        document.getElementById('ctl00_ContentPlaceHolder1_hdnEngine').value = val;
        if(val=='0')
        {
             document.getElementById('placediv').style.display='';
             document.getElementById('enginediv').style.display='none';
        }
        else   if(val=='1')
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
    
     $(document).ready(function()
         {
            $('#ctl00_ContentPlaceHolder1_txtFromDate').datepicker();
              $('#ctl00_ContentPlaceHolder1_txtToDate').datepicker();
              });
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
     
              var checkCVoiceCode=getCheckBoxStatus('ctl00_ContentPlaceHolder1_chkCVoiceCode');
         
              if(checkCVoiceCode=='0')
               {
                alert('Please select atleast one Group');
                 return false;
                }
   //******************* Validation of fromdate todat  ******************************************//
              var startDate = document.getElementById('ctl00_ContentPlaceHolder1_txtFromDate').value;
              var endDate = document.getElementById('ctl00_ContentPlaceHolder1_txtToDate').value;
              var startdt;
                     startdt=startDate.split("/");
                      if(startDate!="")
                      {
                          if (startdt.length != 3) 
                           {      
                           alert('Invalid From Date');
                           return false ;
                          }
                      }
                
                    startdt[1]=parseFloat(startdt[1]);
                    if (startdt[1]<10)
                    {
                        startdt[1]='0'+startdt[1];
                    }
                    startdt[0]=parseFloat(startdt[0]);
                    if (startdt[0]<10)
                    {
                        startdt[0]='0'+startdt[0];
                    }
                    var date1 =startdt[2]+startdt[1]+startdt[0];
                    date1=parseFloat(date1);
                    
                    var enddt;
               
                     enddt=endDate.split("/");
                     if(endDate!='')
                      {
                          if (enddt.length != 3) 
                          {
                           alert('Invalid To Date');
                           return false ;
                           }
                     }
                    enddt[1]=parseFloat(enddt[1]);
                    if (enddt[1]<10)
                    {                 
                        enddt[1]='0'+enddt[1];
                    }              
                    
                    enddt[0]=parseFloat(enddt[0]);             
                    if (enddt[0]<10)
                    {                 
                        enddt[0]='0'+enddt[0];
                    } 
                    
                    var date2 =enddt[2]+enddt[1]+enddt[0] ;
                    date2=parseFloat(date2);
                     if(startDate==''&&  endDate!='')  
                     {
                        alert('Please select From Date');
                        return false ;
                     }
                     else if(endDate==''&&  startDate!='')  
                         {
                          alert('Please select To Date');
                          return false ;
                        }
                    else if(date1 > date2)
                    {
                        alert('FromDate Cannot be Greater than ToDate');
                        return false ;
                    }
           //******************* Validation of fromdate todat  ******************************************//        
    }
    
  
    </script>

    <fieldset class="sectionBorder">
        <legend>Customer Voice Group Wise ACR Detail</legend>
        <table border="0" cellpadding="0" cellspacing="0" width="950px">
            <tr>
                <td class="cssLabel">
                    <asp:Label ID="lblGroupName" runat="server" Text="Group:"></asp:Label>
                    </td><td colspan="2">
                     <asp:Panel  BorderWidth="1px" ID="Panel1" runat="server" BorderColor="#00678e"
                        ScrollBars="Vertical">
                        <asp:CheckBoxList ID="chkCVoiceCode" ToolTip="select Customer Voice Code"
                            RepeatColumns="5" runat="server">
                        </asp:CheckBoxList>
                    </asp:Panel>
                    <input type="checkbox" id="chkAllCVoiceCode" runat="server"  onclick="javascript:SetAllCheckBoxes('ctl00_ContentPlaceHolder1_chkCVoiceCode',this.id);" />Select
                    All
                </td>
               
            </tr>
            <tr>
                <td class="cssLabel"> 
                    <asp:RadioButtonList ID="rdoData" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="0" onclick="getDiv(this.value);">Overall</asp:ListItem>
                        <asp:ListItem Value="1" onclick="getDiv(this.value);">Engine</asp:ListItem>
                        <asp:ListItem Value="2" onclick="getDiv(this.value);">Total</asp:ListItem>
                    </asp:RadioButtonList></td>
                <td id="placediv" class="cssLabel">
                    <span class="cssLabel">Place:</span>
                    <asp:RadioButton ID="rdoAlwar" runat="server" Text="Alwar" GroupName="Place" /><asp:RadioButton
                        ID="rdoBhopal" GroupName="Place" runat="server" Text="Bhopal" /><asp:RadioButton
                            ID="rdoAllPlace" runat="server" Checked="true" Text="Both" GroupName="Place" /></td>
                <td id="enginediv" style="display: none;" class="cssLabel">
                    <span class="cssLabel">Engine:</span>
                    <asp:RadioButton ID="rdoAlwarEngine" runat="server" Text="Alwar" GroupName="Engine" /><asp:RadioButton
                        ID="rdoSimpsonEngine" GroupName="Engine" runat="server" Text="Simpson" /><asp:RadioButton
                            ID="rdoBothEngine" runat="server" Checked="true" Text="Both" GroupName="Engine" /></td>
               
              <td class="cssLabel">
                    <span >Year:</span>
                    <asp:RadioButton ID="rdoFirst" runat="server" Text="I" GroupName="warrantyear" />
                    <asp:RadioButton ID="rdoSecond" runat="server" Text="II" GroupName="warrantyear" />
                    <asp:RadioButton ID="rdoBothYear" runat="server" Text="Both" Checked="true" GroupName="warrantyear" />
                </td>
            </tr>
            <tr>
            <td class="cssLabel" >
                    <span >Problem Type:</span>
                    <asp:RadioButton ID="rdoPrimary" runat="server" Text="Primary" GroupName="Problem" />
                    <asp:RadioButton ID="rdoConsequences" GroupName="Problem" runat="server" Text="Consequences" />
                    <asp:RadioButton ID="rdoAllProblem" GroupName="Problem" runat="server" Text="All"
                        Checked="true" />
                </td>
                <td class="cssLabel">
                    From Date:
                    <asp:TextBox ID="txtFromDate" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td class="cssLabel">
                    To Date:
                    <asp:TextBox ID="txtToDate" runat="server" Width="80px"></asp:TextBox>
                </td>
            </tr>
           
        </table>
        <br />
        <div class="cssButtonPanel" style="width: 950px;">
            <asp:Button ID="btnGo" Text="Go" runat="server" ToolTip="Go" OnClientClick="return getValidation();" OnClick="btnGo_Click1" />
            <input type="button" value="Print" runat="server" visible="false" title="Print" id="btnPrint"
                class="cssButton" onclick="javascript:CallPrint('divGrid');" />
            <asp:Button ID="btnExport" Visible="false" OnClientClick="return CallExport('divGrid');"
                runat="server" CssClass="cssButton" Text="Excel Export" ToolTip="Excel Export"
                OnClick="Button1_Click"></asp:Button>
        </div>
        <br />
        <div id="divGrid" style="overflow: auto; height: 400px; width: 950px;">
            <asp:GridView ID="grdacrData" OnRowCreated="gridView_RowCreated" runat="server" OnDataBound="eventhandlerSerialNo"
                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                CellPadding="3" AutoGenerateColumns="false" Visible="false" EmptyDataText="No Data Found"
                AllowPaging="true" PageSize="500" OnPageIndexChanging="grdacrData_Paging">
                <Columns>
                    <asp:BoundField ItemStyle-HorizontalAlign="Center" HeaderText="#" />
                    <asp:BoundField DataField="WCDOCNO" HeaderStyle-HorizontalAlign="Left" HeaderText="WCDOCNO" />
                    <asp:BoundField DataField="DLR_REF" HeaderStyle-HorizontalAlign="Left" HeaderText="DLR_REF" />
                    <asp:BoundField DataField="TRACTOR_NO" HeaderStyle-HorizontalAlign="Left" HeaderText="TRACTOR NO" />
                    <asp:BoundField DataField="ENGINE_NO" HeaderStyle-HorizontalAlign="Left" HeaderText="ENGINE NO" />
                    <asp:BoundField DataField="INS_DATE" DataFormatString="{0:dd-MM-yyyy}" HtmlEncode="False"
                        HeaderStyle-HorizontalAlign="Left" HeaderText="INS DATE" />
                    <asp:BoundField DataField="DEF_DATE" DataFormatString="{0:dd-MM-yyyy}" HtmlEncode="False"
                        ItemStyle-HorizontalAlign="Center" HeaderText="DEF DATE" />
                    <asp:BoundField DataField="REP_DATE" DataFormatString="{0:dd-MM-yyyy}" HtmlEncode="False"
                        ItemStyle-HorizontalAlign="Center" HeaderText="REP DATE" />
                    <asp:BoundField DataField="HMR" ItemStyle-HorizontalAlign="Center" HeaderText="HMR" />
                    <asp:BoundField DataField="DLR_CO" ItemStyle-HorizontalAlign="Center" HeaderText="DEALER CODE" />
                    <asp:BoundField DataField="DEALER_NAME" HeaderStyle-HorizontalAlign="Left" HeaderText="DEALER NAME" />
                    <asp:BoundField DataField="REG" ItemStyle-HorizontalAlign="Center" HeaderText="REG" />
                    <asp:BoundField DataField="CR_DATE" DataFormatString="{0:dd-MM-yyyy}" HtmlEncode="False"
                        ItemStyle-HorizontalAlign="Center" HeaderText="CR DATE" />
                    <asp:BoundField DataField="ITEM_CODE" ItemStyle-HorizontalAlign="Center" HeaderText="ITEM CODE" />
                    <asp:BoundField DataField="DESCRIPTION" HeaderStyle-HorizontalAlign="Left" HeaderText="DESCRIPTION" />
                    <asp:BoundField DataField="QUANTITY" ItemStyle-HorizontalAlign="Center" HeaderText="N. of Defect" />
                    <asp:BoundField DataField="COST" ItemStyle-HorizontalAlign="Center" HeaderText="COST" />
                    <asp:BoundField DataField="DEF" ItemStyle-HorizontalAlign="Center" HeaderText="DEF" />
                    <asp:BoundField DataField="NDP" ItemStyle-HorizontalAlign="Center" HeaderText="NDP" />
                    <asp:BoundField DataField="VALUE" ItemStyle-HorizontalAlign="Center" HeaderText="VALUE" />
                    <asp:BoundField DataField="CVOICE" ItemStyle-HorizontalAlign="Center" HeaderText="CVOICE" />
                    <asp:BoundField DataField="OUTLV" ItemStyle-HorizontalAlign="Center" HeaderText="OUTLV" />
                    <asp:BoundField DataField="DT" ItemStyle-HorizontalAlign="Center" HeaderText="DT" />
                    <asp:BoundField DataField="CUL_CODE" ItemStyle-HorizontalAlign="Center" HeaderText="CULPRIT CODE" />
                    <asp:BoundField DataField="CR_AMOUNT" ItemStyle-HorizontalAlign="Center" HeaderText="CR-AMOUNT" />
                    <asp:BoundField DataField="AUTH_AMOUNT" ItemStyle-HorizontalAlign="Center" HeaderText="AUTH AMT" />
                    <asp:BoundField DataField="DIFF" ItemStyle-HorizontalAlign="Center" HeaderText="DIFF" />
                </Columns>
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
        </div>
    </fieldset>
    <asp:Literal ID="literal1" runat="server"></asp:Literal>
    <asp:HiddenField ID="hdnExport" runat="server" />
    <asp:HiddenField ID="hdnEngine" Value="0" runat="server" />

    <script type="text/javascript">
    var hdn = document.getElementById('ctl00_ContentPlaceHolder1_hdnEngine').value;
    getDiv(hdn);
    </script>

</asp:Content>
