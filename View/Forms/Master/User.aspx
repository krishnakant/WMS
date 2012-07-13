<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true"
    CodeFile="User.aspx.cs" Inherits="View_Forms_Master_User" Title="Untitled Page" %>

<%@ Register TagPrefix="ew" Namespace="eWorld.UI" Assembly="eWorld.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
         $(document).ready(function()
         {
            $('#ctl00_ContentPlaceHolder1_culDateOfJoing').datepicker();
         });
         
    </script>

    <script type="text/javascript">
    function ClientValidateLevel(source, arguments)
{
      if (document.getElementById('ctl00_ContentPlaceHolder1_ddlLevel').value!=0)
         arguments.IsValid=true;
      else
         arguments.IsValid=false;
} 
function ClientValidateRegion(source, arguments)
{
    if (document.getElementById('ctl00_ContentPlaceHolder1_ddlLevel').value=='4' || document.getElementById('ctl00_ContentPlaceHolder1_ddlLevel').value=='5')
    {
          if (document.getElementById('ctl00_ContentPlaceHolder1_drpRegion').value!=0)
             arguments.IsValid=true;
          else
             arguments.IsValid=false;
     }
}   

function ClientValidateRegionRM(source, arguments)
{
    if (document.getElementById('ctl00_ContentPlaceHolder1_ddlLevel').value=='3'  )
    {
          if (document.getElementById('ctl00_ContentPlaceHolder1_ddlRegionRM').value!=0)
             arguments.IsValid=true;
          else
             arguments.IsValid=false;
     }
} 
function ClientValidateRegionTORM(source, arguments)
{
    if (document.getElementById('ctl00_ContentPlaceHolder1_ddlLevel').value=='6' )
    {
          if (document.getElementById('ctl00_ContentPlaceHolder1_ddlRegionToRM').value!=0)
             arguments.IsValid=true;
          else
             arguments.IsValid=false;
     }
} 


function ClientValidateReportsToRM(source, arguments)
{
    if (document.getElementById('ctl00_ContentPlaceHolder1_ddlLevel').value=='6' )
    {
          if (document.getElementById('ctl00_ContentPlaceHolder1_ddlRegionRSH').value!=0)
             arguments.IsValid=true;
          else
             arguments.IsValid=false;
     }
}     
function ClientValidateReportsTo(source, arguments)
{
     if (document.getElementById('ctl00_ContentPlaceHolder1_ddlLevel').value=='4' || document.getElementById('ctl00_ContentPlaceHolder1_ddlLevel').value=='5')
    {
      if (document.getElementById('ctl00_ContentPlaceHolder1_drpRegionHead').value!=0)
         arguments.IsValid=true;
      else
         arguments.IsValid=false;
     }
}   

function ClientValidateEmployee(source, arguments)
{
      if (document.getElementById('ctl00_ContentPlaceHolder1_ddlRole').value!=0)
         arguments.IsValid=true;
      else
         arguments.IsValid=false;
}





function getPanel(val)
{
getRegionalRM();
 getReportsTo();
 if(val == '1' || val == '2')
   { 
       document.getElementById('trChkDealer').style.display='none';
       document.getElementById('tdRegioncheckboxlist').style.display='none';
       document.getElementById('tdRegionRSH').style.display='none';
       document.getElementById('tdEnggList').style.display='none';
   }
   if(val == '3')
   {
          document.getElementById('trChkDealer').style.display='none';
        document.getElementById('tdRegioncheckboxlist').style.display='';
        document.getElementById('tdRegionRSH').style.display='none';
        document.getElementById('tdEnggList').style.display='none';
   }
   if(val == '4' || val == '5')
   {
        document.getElementById('trChkDealer').style.display='';
        document.getElementById('tdRegioncheckboxlist').style.display='none';
        document.getElementById('tdRegionRSH').style.display='none';
        document.getElementById('tdEnggList').style.display='';

   }
   
   if(val == '6')
   {
     document.getElementById('trChkDealer').style.display='none';
     document.getElementById('tdEnggList').style.display='none';
     document.getElementById('tdRegioncheckboxlist').style.display='none';
     document.getElementById('tdRegionRSH').style.display='';
       
   }
   
}


var ReportsToRMController='';
function getRegionalRM()
{

       var fieldsAjax1 = new Array();
	   fieldsAjax1.push("Status=fillRegionalRM");
	   
	   var RegionID=document.getElementById('ctl00_ContentPlaceHolder1_ddlRegionToRM').value;
	   //alert(RegionID);
	   if(RegionID!=0)
	    {
	   
	        fieldsAjax1.push("RegionID="+RegionID);
	        ReportsToRMController = new AjaxController("/WMS/Handler.aspx",fieldsAjax1.join("&"),getRMReportsDetail); 
	        ReportsToRMController.GetData();
	    }
	    else
	    {
	            var drpRegionHead = document.getElementById('ctl00_ContentPlaceHolder1_ddlRegionRSH');
	    	    while(drpRegionHead.length>0)
                {
		          for (y = 0; y < drpRegionHead.length; y++) 
		          {
		             drpRegionHead.options[y]=null;
		          }
		        }
	    }
	    return false;
}
    
    function getRMReportsDetail()
	{	    
	      var drpRegionHead = document.getElementById('ctl00_ContentPlaceHolder1_ddlRegionRSH');
	      try
            {		 
              if(ReportsToRMController.ParseResult())
	          {
	          
	    	    while(drpRegionHead.length>0)
                {
		          for (y = 0; y < drpRegionHead.length; y++) 
		          {
		             drpRegionHead.options[y]=null;
		          }
		        }
	             for (i = 0; i < ReportsToRMController.resultSet.length; i++) 
	             {
                    var cols=ReportsToRMController.resultSet[i];
                    var count = cols[0];
                    var k=1;
	                var l=2;
	                	        //alert(count);
	                   for(var j=0;j<parseInt(count);j++)
			           {
	                       var newOption = new Option(cols[l], cols[k]);
	                       drpRegionHead.options[j] = newOption;
	                       
                             if(document.getElementById('ctl00_ContentPlaceHolder1_hdnRMID').value==cols[k])
                            {
                                drpRegionHead.options[j].selected=true;
                            }
                                                   
	                       k+=2;
	                       l+=2;
                       }
                   
                 }
               }           
            }
            catch(e) {}
   
	}




var ReportsToController='';
function getReportsTo()
{

       var fieldsAjax1 = new Array();
	   fieldsAjax1.push("Status=fillReportsTo");
	   
	   var RegionID=document.getElementById('ctl00_ContentPlaceHolder1_drpRegion').value;
	   //alert(RegionID);
	   if(RegionID!=0)
	    {
	   
	        fieldsAjax1.push("RegionID="+RegionID);
	        ReportsToController = new AjaxController("/WMS/Handler.aspx",fieldsAjax1.join("&"),getReportsDetail); 
	        ReportsToController.GetData();
	    }
	    else
	    {
	            var drpRegionHead = document.getElementById('ctl00_ContentPlaceHolder1_drpRegionHead');
	    	    while(drpRegionHead.length>0)
                {
		          for (y = 0; y < drpRegionHead.length; y++) 
		          {
		             drpRegionHead.options[y]=null;
		          }
		        }
	    }
	    return false;
}
    
    function getReportsDetail()
	{	    
	      var drpRegionHead = document.getElementById('ctl00_ContentPlaceHolder1_drpRegionHead');
	      try
            {		 
              if(ReportsToController.ParseResult())
	          {
	          
	    	    while(drpRegionHead.length>0)
                {
		          for (y = 0; y < drpRegionHead.length; y++) 
		          {
		             drpRegionHead.options[y]=null;
		          }
		        }
	             for (i = 0; i < ReportsToController.resultSet.length; i++) 
	             {
                    var cols=ReportsToController.resultSet[i];
                    var count = cols[0];
                    var k=1;
	                var l=2;
	                	        //alert(count);
	                   for(var j=0;j<parseInt(count);j++)
			           {
	                       var newOption = new Option(cols[l], cols[k]);
	                       drpRegionHead.options[j] = newOption;
	                       
                             if(document.getElementById('ctl00_ContentPlaceHolder1_hdnReportsID').value==cols[k])
                            {
                                drpRegionHead.options[j].selected=true;
                            }
                                                   
	                       k+=2;
	                       l+=2;
                       }
                   
                 }
               }           
            }
            catch(e) {}
        
	}
	function setValues()
    {
        document.getElementById('ctl00_ContentPlaceHolder1_hdnReportsID').value=document.getElementById('ctl00_ContentPlaceHolder1_drpRegionHead').value;
        document.getElementById('ctl00_ContentPlaceHolder1_hdnRMID').value=document.getElementById('ctl00_ContentPlaceHolder1_ddlRegionRSH').value;
        getCheckBoxID();
    }

     function ClientValidateLevelTORole(source, arguments)
      {
        var RoleID  = document.getElementById('ctl00_ContentPlaceHolder1_ddlRole').value;
        var val  = document.getElementById('ctl00_ContentPlaceHolder1_ddlLevel').value;
        if(val == '1' || val == '2')
        {
         if(RoleID=='2' || RoleID=='16')
           {
             arguments.IsValid=true;
          
           }
           else
           arguments.IsValid=false;
        }
         else
           arguments.IsValid=true;       
      } 
      
      
      
      
      
      
      
    
   
    
 var bannerNewItem='';
function FillDealerBychk()
        {
        
            var val = document.getElementById('ctl00_ContentPlaceHolder1_drpRegion').value;
             if(val!=0)
	         {
	         var fieldsAjax1 = new Array();
            fieldsAjax1.push("Status=fillDealerNameswithchk");
            fieldsAjax1.push("RegionID="+val);
            bannerNewItem = new AjaxController("/WMS/Handler.aspx",fieldsAjax1.join("&"),BuildTable);
            bannerNewItem.GetData();
            }
        }
        
        var flg=0;
        function BuildTable()
        {
	        try
	        {
                if(bannerNewItem.ParseResult())
	            {
			        if(flg==0)
			        {
					        flg=1;
			        }
			        var tblcampagin = document.getElementById("tableDealer");
			         
			        var i = 0;
			        if(tblcampagin.rows.length-1>0)
			        {
				        for (i = tblcampagin.rows.length-1; i >0; i--)
				        {
					        tblcampagin.deleteRow(i);
				        }
		            }
			        var str=""
			        var icheck=0;
			        str="<table border=0 width='100%' ><tr >";
			        for (i = 0; i < bannerNewItem.resultSet.length; i++)
			        {
				        var cols=bannerNewItem.resultSet[i];
			            var count = cols[0];
				        var k = 1;
				        var l=2
				        var ival =0;
				        var Temp =1;
				        document.getElementById('ctl00_ContentPlaceHolder1_hdnDealerCount').value=count;
			 	        for(j=1;j<=count;j++)
				        {
				       
				            str +="<td class='cssCheckBoxList'>"
				            str +="<input type=checkbox id='chkDealerCode'  name="+cols[k]+" value='"+cols[k]+"'  >"+cols[l]
				            str +="</td>"
				            if(ival==3)
				            {
					            Temp = 0;
					            ival = 0;
				            }
				            else
				            {
				                Temp = 1;
				                ival= ival + 1;
				            }
				            if(Temp==0)
				            {
				                str +="</tr>"
				                str +="<tr>"
				            }
				            str+=" "
				            k+=2
				            l+=2
				        }
				        if(count==0)
				        {
				            str +="<tr>"
				        }
				        if(Temp==0)
				        {
				            str +="</tr>"
				        }
				        var row=tblcampagin.insertRow(i+1);
				        str +="</table>"
			     }
			        row.insertCell(0).innerHTML= str;
             }
                  EnableDisableCheckBox();
                
                 
	    }
	        catch(e)
	        {
	            
	        }
        }
        
        function EnableDisableCheckBox()
        {
            var rows=document.getElementById('ctl00_ContentPlaceHolder1_hdnDealerCount').value;
            var strDepartmentID='',temp=0,strNew='';
            strDepartmentID=document.getElementById('ctl00_ContentPlaceHolder1_hdnCheckBoxID').value;
            
            if(rows==1)
            {
                if(document.aspnetForm.chkDealerCode.name==strDepartmentID)
                {
                    document.aspnetForm.chkDealerCode.checked=true;
                }
            }
            else
            {
                strNew=strDepartmentID.split(',');
                for(var i=0;i<strNew.length;i++)
                {
                    for (var j =0 ;j<rows ; j++)
                    {
                        if(document.aspnetForm.chkDealerCode[j].name==strNew[i])
                        {
                           document.aspnetForm.chkDealerCode[j].checked=true;
                        }
                    }
                }
            }
        }
        
        function getCheckBoxID()
        {
          
 	        var chkDealerID ='';
	        var CheckchkBox=0;
	       if(document.getElementById('ctl00_ContentPlaceHolder1_drpRegion').value!='0' && (document.getElementById('ctl00_ContentPlaceHolder1_ddlLevel').value=='4' || document.getElementById('ctl00_ContentPlaceHolder1_ddlLevel').value=='5'))
	       {
               if (document.aspnetForm.chkDealerCode.name>0)
               {
                chkDealerID = document.aspnetForm.chkDealerCode.name ;
                }
           else
            {
             for (var i =0 ;i< document.aspnetForm.chkDealerCode.length ; i++)
                {
             
                     if(document.aspnetForm.chkDealerCode[i].checked==true)
                     {
                     if(chkDealerID!='')
                     {
                   
                     chkDealerID = chkDealerID+','+(document.aspnetForm.chkDealerCode[i].name) ;
                    
                     }
                     else  
                     {
                    
                         chkDealerID =(document.aspnetForm.chkDealerCode[i].name);
                     }
                   }
                     
                }
            }
         
            document.getElementById('ctl00_ContentPlaceHolder1_hdnCheckBoxID').value =chkDealerID;
            
           }
        }
    </script>

    <fieldset class="sectionBorder">
        <legend>User</legend>
        <table border="0" cellspacing="0" cellpadding="0" width="95%">
            <tr>
                <td>
                    <table border="0" width="100%" cellspacing="0" cellpadding="0">
                        <tr>
                            <td class="cssLabel">
                                <asp:Label ID="lblFullName" runat="server" Text="Full&nbsp;Name:"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtFullName" SkinID="txtupper"  ToolTip="FullName" MaxLength="50" Width="150px" runat="server"></asp:TextBox>
                                <asp:Label ID="Label1" runat="server" ForeColor="red" Text="*"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator" ControlToValidate="txtFullName"
                                    runat="server" ErrorMessage=""></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="cssLabel">
                                <asp:Label ID="lblEmployeeCode" runat="server" Text="Employee&nbsp;Code:"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtEmployeeCode" SkinID="txtupper" ToolTip="EmployeeCode" MaxLength="50" Width="150px"
                                    runat="server"></asp:TextBox>
                                     <asp:Label ID="Label3" runat="server" ForeColor="red" Text="*"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtEmployeeCode"
                                    runat="server" ErrorMessage=""></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="cssLabel">
                                <asp:Label ID="lblLoginID" runat="server" Text="Login&nbsp;ID:"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtLoginID" ToolTip="LoginID" MaxLength="50" Width="150px" runat="server"></asp:TextBox>
                                 <asp:Label ID="Label4" runat="server" ForeColor="red" Text="*"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtLoginID"
                                    runat="server" ErrorMessage=""></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="cssLabel">
                                <asp:Label ID="lblRole"  runat="server" Text="Role:"></asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlRole" ToolTip="Role" runat="server">
                                </asp:DropDownList>
                                 <asp:Label ID="Label5" runat="server" ForeColor="red" Text="*"></asp:Label>
                                <asp:CustomValidator ID="CustomValidator" ErrorMessage="Select the Role" ClientValidationFunction="ClientValidateEmployee"
                                    ControlToValidate="ddlRole" runat="server"></asp:CustomValidator>
                            </td>
                        </tr>
                        <asp:Panel ID="pnlPassword" Visible="true" runat="server">
                            <tr>
                                <td class="cssLabel">
                                    <asp:Label ID="lblPassword" runat="server" Text="Password:"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtPassword" ToolTip="Password" Width="150px" MaxLength="16" TextMode="Password"
                                        runat="server"></asp:TextBox>
                                         <asp:Label ID="Label6" runat="server" ForeColor="red" Text="*"></asp:Label>
                                    <% if (strUpdate == "")
                                       {%>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtPassword"
                                        runat="server" ErrorMessage=""></asp:RequiredFieldValidator>
                                    <% }  %>
                                </td>
                            </tr>
                            <tr>
                                <td class="cssLabel">
                                    <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm&nbsp;Password:"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtConfirmPassword" MaxLength="16" Width="150px" ToolTip="ConfirmPassword"
                                        TextMode="Password" runat="server"></asp:TextBox>
                                    <% if (strUpdate == "")
                                       {%>
                                    <asp:CompareValidator runat="server" ID="cvPasswordConfirmPassword" ControlToCompare="txtPassword"
                                        ControlToValidate="txtConfirmPassword" ErrorMessage=""></asp:CompareValidator>
                                    <% }  %>
                                </td>
                            </tr>
                        </asp:Panel>
                        <%--<tr>
                            <td class="cssLabel">
                                <asp:Label Font-Bold="true" ID="lblSelect1" runat="server" Text="User Type"></asp:Label>
                            </td>
                            <td>
                                <asp:RadioButton ID="rdoPO" runat="server" Text="PO" GroupName="PO" /><asp:RadioButton
                                    ID="rdoSales" GroupName="PO" runat="server" Text="Sales" /><asp:RadioButton ID="rdoService"
                                        runat="server" Text="Service" GroupName="PO" /><asp:RadioButton ID="rdoOther" runat="server"
                                            Text="Other" GroupName="PO" Checked="true" />
                            </td>
                        </tr>--%>
                        <tr>
                            <td class="cssLabel">
                                <asp:Label ID="lblActive" runat="server" Text="Status:"></asp:Label>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkActive" Checked="true" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table border="0" width="100%" cellspacing="0" cellpadding="0">
                        <tr>
                            <td class="cssLabel">
                                <asp:Label ID="lblPermanentAddress"   runat="server" Text="Permanent&nbsp;Address:"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtPermanentAddress" SkinID="txtupper" Width="250px" MaxLength="250" ToolTip="PermanentAddress"
                                    TextMode="MultiLine" runat="server"></asp:TextBox>
                                    <asp:Label ID="Label7" runat="server" ForeColor="red" Text="*"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtPermanentAddress"
                                    runat="server" ErrorMessage=""></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="cssLabel">
                                <asp:Label ID="lblCurrentAddress" runat="server" Text="Current&nbsp;Address:"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtCurrentAddress"  SkinID="txtupper"  Width="250px" MaxLength="250" ToolTip="CurrentAddress"
                                    TextMode="MultiLine" runat="server"></asp:TextBox>
                                    <asp:Label ID="Label8" runat="server" ForeColor="red" Text="*"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtCurrentAddress"
                                    runat="server" ErrorMessage=" "></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="cssLabel">
                                <asp:Label ID="lblEmailID" runat="server" Text="Email&nbsp;ID:"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtEmailID" ToolTip="EmailID" Width="150px" MaxLength="50" runat="server"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtEmailID"
                                    runat="server" ErrorMessage="*" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="cssLabel">
                                <asp:Label ID="lblPhoneNo" runat="server" Text="Phone&nbsp;No.:"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtPhoneNo" MaxLength="15" Width="150px" ToolTip="PhoneNo" runat="server"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtPhoneNo"
                                    runat="server" ErrorMessage="*" ValidationExpression="^([0-9-]*\s?[0-9-]*)+$"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="cssLabel">
                                <asp:Label ID="lblMobileNo" runat="server" Text="Mobile&nbsp;No.:"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtMobileNo" ToolTip="MobileNo" Width="150px" MaxLength="15" runat="server"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="txtMobileNo"
                                    runat="server" ErrorMessage="*" ValidationExpression="^([0-9+-]*\s?[0-9+-]*)+$"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="cssLabel">
                                <asp:Label ID="lblDateOfJoing" runat="server" Text="Date&nbsp;Of&nbsp;Joing:"></asp:Label></td>
                            <td style="width: 168px">
                                <input type="text" id="culDateOfJoing" class="cssTextBox" runat="server" value=""
                                    style="width: 80px" />
                                <asp:CustomValidator ID="CustomValidator3" runat="server" ClientValidationFunction="ValidDate"
                                    ControlToValidate="culDateOfJoing" ErrorMessage="Invalid" Font-Size="10pt"></asp:CustomValidator>
                            </td>
                            <%-- <td>
                                <ew:CalendarPopup ID="culDateOfJoing" runat="server" CalendarWidth="200" Culture="English (United Kingdom)"
                                    Font-Size="9px" Height="20px" ImageUrl="~/images/icon_calendar.gif" Nullable="true"
                                    NullableLabelText='' ShowGoToToday="true" PadSingleDigits="True" ToolTip="Select Date"
                                    Width="80px">
                                    <SelectedDateStyle BackColor="LightSteelBlue" />
                                </ew:CalendarPopup>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="culDateOfJoing"
                                    runat="server" ErrorMessage="*">*</asp:RequiredFieldValidator>
                            </td>--%>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
        <asp:Label ID="lblMessage" runat="server" ForeColor="red" Font-Bold="true" Text=""></asp:Label>
    </fieldset>
    <br />
    <fieldset class="sectionBorder">
        <legend>User Permission</legend>
        <table border="0" cellspacing="0" cellpadding="0" width="95%">
            <tr>
              
                <td colspan="2">
                    <asp:Label ID="lblLevel" Font-Bold="true" runat="server" Text="Level"></asp:Label>
                    <asp:DropDownList ID="ddlLevel" ToolTip="Level" runat="server" onchange="javascript:getPanel(this.value);">
                    </asp:DropDownList>
                    <asp:Label ID="Label9" runat="server" ForeColor="red" Text="*"></asp:Label>
                                        <asp:CustomValidator ID="CustomValidator8" ErrorMessage="Select the Level" ClientValidationFunction="ClientValidateLevel"
                        ControlToValidate="ddlLevel" runat="server"></asp:CustomValidator>
                          <asp:CustomValidator ID="CustomValidator4" ErrorMessage="Please Select admin or QA for H.O Level" ClientValidationFunction="ClientValidateLevelTORole"
                        ControlToValidate="ddlLevel" runat="server"></asp:CustomValidator><br />
                        
                </td>
             
            </tr>
            <tr id="tdRegionRSH" style="display: none;">
            <td>
                   <br />
                     <asp:Label Font-Bold="true" ID="Label11" runat="server" Text="Region"></asp:Label>
                    <asp:DropDownList ID="ddlRegionToRM" ToolTip="Region" runat="server" onchange="javascript:getRegionalRM();">
                    </asp:DropDownList></td><td>
                    
                     <asp:Label Font-Bold="true" ID="Label10" runat="server" Text="Report To"></asp:Label>
                   <asp:DropDownList ID="ddlRegionRSH" ToolTip="Regional Manager" runat="server">
                    </asp:DropDownList>
                   
                       
                       
                    </td></tr><tr><td><asp:CustomValidator ID="CustomValidator7" ErrorMessage="Select Region" ClientValidationFunction="ClientValidateRegionTORM"
                        ControlToValidate="ddlRegionToRM" runat="server"></asp:CustomValidator></td><td> <asp:CustomValidator ID="CustomValidator10" ErrorMessage="Select Regional Manager" ClientValidationFunction="ClientValidateReportsToRM"
                        ControlToValidate="ddlRegionRSH" runat="server"></asp:CustomValidator></td></tr>
                        <tr id="tdRegioncheckboxlist" style="display: none;">
                <td colspan="2">
                    <h6>
                        <asp:Label Font-Bold="true" ID="Label2" runat="server" Text="Region"></asp:Label></h6>
                  
                         <asp:DropDownList ID="ddlRegionRM" ToolTip="Region" runat="server">
                    </asp:DropDownList>
                    <asp:CustomValidator ID="CustomValidator5" ErrorMessage="Select Region" ClientValidationFunction="ClientValidateRegionRM"
                        ControlToValidate="ddlRegionRM" runat="server"></asp:CustomValidator>
                        
                    </td>
                    </tr><tr>
                    <td colspan="2">  <asp:Panel BorderWidth="1px" ID="PnlRegion" Visible="false" runat="server" BorderColor="#00678e"
                        ScrollBars="Vertical">
                        <asp:CheckBoxList ID="chkRegion" CellSpacing="5" ToolTip="select Region" RepeatColumns="10"
                            runat="server">
                        </asp:CheckBoxList>
                        <input type="checkbox" id="chkRegionAll" onclick="javascript:SetAllCheckBoxes('ctl00_ContentPlaceHolder1_chkRegion',this.id);" />Select
                    All
                    </asp:Panel></td></tr>
                    <tr  id="tdEnggList" style="display: none;">
                <td >
                    <asp:Label Font-Bold="true" ID="lblRegionList" runat="server" Text="Region"></asp:Label>
                    <asp:DropDownList ID="drpRegion" ToolTip="Region" runat="server" onchange="javascript:FillDealerBychk();getReportsTo();">
                    </asp:DropDownList></td><td>
                 
                    <asp:Label Font-Bold="true" ID="lblReportsTo" runat="server" Text="Reports To:"></asp:Label>
                    <asp:DropDownList ID="drpRegionHead" ToolTip="Reports To" runat="server">
                    </asp:DropDownList>
                     
                </td></tr><tr><td>  <asp:CustomValidator ID="CustomValidator2" ErrorMessage="Select Region" ClientValidationFunction="ClientValidateRegion"
                        ControlToValidate="drpRegion" runat="server"></asp:CustomValidator></td> <td> <asp:CustomValidator ID="CustomValidator1" ErrorMessage="Please Select" ClientValidationFunction="ClientValidateReportsTo"
                        ControlToValidate="drpRegionHead" runat="server"></asp:CustomValidator></td> </tr>
            <tr><td colspan="3"><asp:Label ID="lblMasg" runat="server" ForeColor="red" Font-Bold="true" Text=""></asp:Label></td></tr>
           <tr id="trChkDealer" style="display: none;">
                <td colspan="3">
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
                  
                    <input type="checkbox" id="chkAllDealerCode" onclick="javascript:SetAllCheckBoxes('tableDealer',this.id);" />Select
                    All
                </td>
            </tr>
            <%--   <tr>
                <td colspan="3">
                    <h6>
                        <asp:Label ID="lblModule" runat="server" Text="Module"></asp:Label></h6>
                    <asp:Panel BorderWidth="1px" Width="350px" ID="PnlModule" runat="server" BorderColor="#00678e"
                        ScrollBars="Vertical">
                        <asp:CheckBoxList ID="ChkModule" CellSpacing="5" ToolTip="select Module" RepeatColumns="10"
                            runat="server">
                        </asp:CheckBoxList>
                    </asp:Panel>
                    <input type="checkbox" id="chkModuleAll" checked="checked" onclick="javascript:SetAllCheckBoxes('ctl00_ContentPlaceHolder1_ChkModule',this.id);" />Select
                    All
                    <asp:Button ID="btnShow" Text="Show Form" CausesValidation="false" OnClick="btnShow_Click"
                        ToolTip="ShowForm" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <h6>
                        <asp:Label ID="lblForm" runat="server" Text="Form"></asp:Label></h6>
                    <asp:Panel BorderWidth="1px" Width="350px" ID="PnlForm" runat="server" BorderColor="#00678e"
                        ScrollBars="Vertical">
                        <asp:CheckBoxList ID="chkForm" CellSpacing="5" ToolTip="select Form" RepeatColumns="5"
                            runat="server">
                        </asp:CheckBoxList>
                    </asp:Panel>
                    <input type="checkbox" id="chkFormALL" onclick="javascript:SetAllCheckBoxes('ctl00_ContentPlaceHolder1_chkForm',this.id);" />Select
                    All
                </td>
            </tr>--%>
        </table>
    </fieldset>
    <div class="cssButtonPanel">
        <asp:Button ID="btnSave" CausesValidation="true" Text="Save" OnClientClick="setValues();" OnClick="btnSave_Click"
            ToolTip="Save" runat="server" />
        <span style="margin-left: 1%;"></span>
        <asp:Button ID="btncencle" CausesValidation="false" Text="cancel" ToolTip="cancel"
            runat="server" OnClick="btncencle_Click" />
    </div>
    <asp:Literal ID="literal1" runat="server"></asp:Literal>
    <asp:HiddenField ID="hdnCheckID" Value="0" runat="server" />
    <asp:HiddenField ID="hdnUserID" Value="0" runat="server" />
    <asp:HiddenField ID="hdnEmployeeCode" Value="0" runat="server" />
    <asp:HiddenField ID="hdnReportsID" runat="server" />
     <asp:HiddenField ID="hdnRMID" runat="server" />
     <asp:HiddenField ID="hdnCheckBoxID" runat="server" />
     <asp:HiddenField ID="hdnDealerCount" runat="server" />
     <asp:HiddenField ID="hdnItemNameList" runat="server" />
     <asp:HiddenField ID="hdnchkIDList" runat="server" />
      <script type="text/javascript">
        //indexchange()
        //change();
        var drpval = document.getElementById('ctl00_ContentPlaceHolder1_ddlLevel').value;
        getPanel(drpval);
        getReportsTo();
        getRegionalRM()
        FillDealerBychk();
        EnableDisableCheckBox();
    </script>

</asp:Content>
