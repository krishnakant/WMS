<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true"
    CodeFile="Defect.aspx.cs" Inherits="View_Forms_Master_Defect" %>

<%@ Register TagPrefix="ew" Namespace="eWorld.UI" Assembly="eWorld.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script type="text/javascript">
         $(document).ready(function()
         {
            $('#ctl00_ContentPlaceHolder1_CalstartDate').datepicker();
         });
         
    </script>
    <script language="javascript" type="text/javascript">
function setMessageText(ID,Text)
{
   document.getElementById(ID).innerHTML = Text ;
   setTimeout("setMessageText('"+ID+"','')",3000);
} 

function ClearControls(id)
{
document.getElementById('ctl00_ContentPlaceHolder1_txtDefectCode').value='';
document.getElementById('ctl00_ContentPlaceHolder1_txtDesc').value='';
document.getElementById('ctl00_ContentPlaceHolder1_chkActive').checked = 0;
document.getElementById('ctl00_ContentPlaceHolder1_chkReport').checked = 0;
return false;
}

function getRadioSelection(id)
{

   if(id=='ctl00_ContentPlaceHolder1_rdoAdd')
   {
    document.getElementById('ctl00_ContentPlaceHolder1_txtDefectGroup').style.display='';
    document.getElementById('ctl00_ContentPlaceHolder1_drpDefectGroup').style.display='none';
   }
    if(id=='ctl00_ContentPlaceHolder1_rdoAssign')
   {
    document.getElementById('ctl00_ContentPlaceHolder1_txtDefectGroup').style.display='none';
    document.getElementById('ctl00_ContentPlaceHolder1_drpDefectGroup').style.display='';
   }
  
} 
function getGroup()
{

   if(document.getElementById('ctl00_ContentPlaceHolder1_rdoAdd').checked)
   {
    document.getElementById('ctl00_ContentPlaceHolder1_txtDefectGroup').style.display='';
    document.getElementById('ctl00_ContentPlaceHolder1_drpDefectGroup').style.display='none';
   }
   else
   {
     document.getElementById('ctl00_ContentPlaceHolder1_txtDefectGroup').style.display='none';
      document.getElementById('ctl00_ContentPlaceHolder1_drpDefectGroup').style.display='';
   }
  
} 


function Validate(id)
{
   var checkDate=CheckValidDateFormat(document.getElementById('ctl00_ContentPlaceHolder1_CalstartDate'));
   if(!checkDate)
   {
          alert('Invalid date');
          return false;
   }
        var assign = 'ctl00_ContentPlaceHolder1_rdoAssign';
        var group='';
        var lblgrp='ctl00_ContentPlaceHolder1_lblGroupMessage';
        var lblcode='ctl00_ContentPlaceHolder1_lblDefectMessage';
        var lbldesc='ctl00_ContentPlaceHolder1_lblDescMessage';
          var code=document.getElementById('ctl00_ContentPlaceHolder1_txtDefectCode').value
        if(document.getElementById('ctl00_ContentPlaceHolder1_txtDefectCode').value=='')
        {
            document.getElementById(lblcode).innerHTML='*';
            return false;
        }
        else
        
            var RegExp = /^([0-9]*\s?[0-9]*)+$/;
                
		        if (!RegExp.test(code))
		        {
		        document.getElementById(lblcode).innerHTML='';
		        document.getElementById(lblcode).innerHTML='Numeric Only';
		           
    		      return false;
    		    }  
        else
        {
            document.getElementById(lblcode).innerHTML='';
                if(document.getElementById('ctl00_ContentPlaceHolder1_txtDesc').value=='')
                {
                    document.getElementById(lbldesc).innerHTML='*';
                    return false;
                }
                else
                {
                    document.getElementById(lbldesc).innerHTML='';
                    if(document.getElementById(assign).checked)
                    {
                         group = document.getElementById('ctl00_ContentPlaceHolder1_drpDefectGroup').value;
                        if(group==0)
                        {
                             document.getElementById(lblgrp).innerHTML='*';
                             return false;
                         }
                         else
                         {
                              document.getElementById(lblgrp).innerHTML='';
                               return true;
                         }
            
                     }
                     else
                     {
                         group = document.getElementById('ctl00_ContentPlaceHolder1_txtDefectGroup').value;
                        if(group=='')
                        {
                             document.getElementById(lblgrp).innerHTML='*';
                             return false;
                         }
                         else
                         {
                              document.getElementById(lblgrp).innerHTML='';
                               return true;
                         }
                     }
                }
        
            
        }
        
        
        

}
    </script>

    <fieldset class="sectionBorder">
        <legend>Defect</legend>
        <div >
            <table width="90%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="cssLabel">
                        <asp:Label ID="lblDefectCode" runat="server" Text="Defect Code:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDefectCode" ToolTip="Defect Code" runat="server" MaxLength="3"></asp:TextBox>
                        <asp:Label ID="lblDefectMessage" runat="server" ForeColor="Red"></asp:Label>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                            ControlToValidate="txtDefectCode"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please enter numeric values only"
                            ValidationExpression="^([0-9]*\s?[0-9]*)+$" ControlToValidate="txtDefectCode"></asp:RegularExpressionValidator>--%>
                    </td>
                </tr>
                <tr>
                    <td class="cssLabel">
                        <asp:Label ID="lblDefectGroup" runat="server" Text="Defect Group:"></asp:Label>
                    </td>
                    <td>
                        <asp:RadioButton ID="rdoAssign" runat="server" GroupName="Mode" Text="Assign Existing"
                            onclick="javascript:getRadioSelection(this.id);" />
                        <asp:RadioButton ID="rdoAdd" runat="server" Checked="true" GroupName="Mode" Text="Add"
                            onclick="javascript:getRadioSelection(this.id);" />
                        <asp:DropDownList ID="drpDefectGroup" runat="server" Style="display: none;">
                        </asp:DropDownList>
                        <asp:TextBox ID="txtDefectGroup" ToolTip="group Name" runat="server"></asp:TextBox>
                        <asp:Label ID="lblGroupMessage" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="cssLabel">
                        <asp:Label ID="lblDesc" runat="server" Text="Description:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDesc" Width="250px" ToolTip="Description" runat="server" TextMode="MultiLine"></asp:TextBox>
                        <asp:Label ID="lblDescMessage" runat="server" ForeColor="Red"></asp:Label>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                            ControlToValidate="txtDesc"></asp:RequiredFieldValidator>--%>
                    </td>
                </tr>
                <tr>
                    <td class="cssLabel">
                        <asp:Label ID="lblActive" runat="server" Text="Active:"></asp:Label>
                    </td>
                    <td>
                        <asp:CheckBox ID="chkActive" Checked="true" ToolTip="select Status" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="cssLabel">
                        <asp:Label ID="lblReport" runat="server" Text="To&nbsp;be&nbsp;used&nbsp;in&nbsp;Report:"></asp:Label>
                    </td>
                    <td>
                        <asp:CheckBox ID="chkReport" Checked="true" ToolTip="To&nbsp;be&nbsp;used&nbsp;in&nbsp;Report" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="cssLabel">
                        <asp:Label ID="lbldate" runat="server" Text="Effective Date:"></asp:Label>
                    </td>
                    <td  style="width: 168px">
                                <input type="text" id="CalstartDate"  class="cssTextBox" runat="server" 
                                    value="" style="width: 80px" />
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="CalstartDate"
                                    runat="server" ErrorMessage="*">*</asp:RequiredFieldValidator>
                                 
                             
                            </td>
                   
                </tr>
                 <tr>
                    <td colspan="2">
                        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <br />
        
      
    </fieldset>
      <div class="cssButtonPanel">
          <asp:Button ID="btnAdd" Text="Save" ToolTip="Save" runat="server" OnClick="btnAdd_Click" OnClientClick="return Validate(this.id);" />
          <span style="margin-left:1%;"></span>
            <asp:Button ID="btnCancel" ToolTip="Cancel" Text="Cancel" runat="server" OnClick="btnCancel_Click"  />
        </div>
    <asp:HiddenField ID="hdnStatusID" Value="0" runat="server" />
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
</asp:Content>
