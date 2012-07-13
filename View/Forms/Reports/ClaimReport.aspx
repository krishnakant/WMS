<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true"
    CodeFile="ClaimReport.aspx.cs" Inherits="View_Forms_Reports_ClaimReport" ValidateRequest="false" %>

<%@ Register TagPrefix="ew" Namespace="eWorld.UI" Assembly="eWorld.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script type="text/javascript" language="javascript">
 $(document).ready(function()
         {
            $('#ctl00_ContentPlaceHolder1_txtFromDate').datepicker();
            $('#ctl00_ContentPlaceHolder1_txtToDate').datepicker();
         });
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

 function ClientValidateBetweenDate(source, arguments)
       {
          
            var status=CheckDateValidation();
            if(status)
            {
                arguments.IsValid=true;
            }
            else
            {
                arguments.IsValid=false;
            }
       }
       
</script>
    <fieldset class="sectionBorder">
        <legend></legend>
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
               <td class="cssLabel">
                    From Period:</td>
                <td>
                    <asp:TextBox ID="txtFromDate" runat="server" Width="80px"></asp:TextBox></td>
                <td>
                    
                    <asp:CustomValidator ID="CustomValidator3" runat="server" ErrorMessage="Invalid Date"
                        ClientValidationFunction="ValidDate" ControlToValidate="txtFromDate"></asp:CustomValidator></td>
                <td class="cssLabel">
                    To Period:</td>
                <td>
                    <asp:TextBox ID="txtToDate" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td>
                    
                    <asp:CustomValidator ID="CustomValidator2" runat="server" ErrorMessage="Invalid Date"
                        ClientValidationFunction="ValidDate" ControlToValidate="txtToDate"></asp:CustomValidator>
                    <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="From Date cannot be greater than To Date"
                        ClientValidationFunction="ClientValidateBetweenDate" ControlToValidate="txtToDate"></asp:CustomValidator></td>
            </tr>
            <tr>
              <td   class="cssLabel" colspan="4">
                  <b> Product</b>
                    <asp:RadioButton ID="rdoNew"  runat="server" Text="New" GroupName="Product" /><asp:RadioButton
                        ID="rdoRegular" GroupName="Product" runat="server" Text="Regular" /><asp:RadioButton
                            ID="rdoAll" runat="server" Checked="true" Text="All" GroupName="Product" /></td>
                            <td  ></td>
            </tr>
              <tr>
                <td   class="cssLabel" colspan="4">
                  <b> Hours (HMR)</b>
                    <asp:RadioButton ID="rdoLessThan250"  runat="server" Text="Less than 250" GroupName="HMR" /><asp:RadioButton
                        ID="rdoMoreThan250" GroupName="HMR" runat="server" Text="250 to 2500" /><asp:RadioButton
                            ID="rdoHMRAll" runat="server" Checked="true" Text="All" GroupName="HMR" /></td>
                            <td  ></td>
            </tr>
             <tr>
                <td   class="cssLabel" colspan="4">
                  <b> Cost</b>
                    <asp:RadioButton ID="rdoCost" Checked="true"  runat="server" Text="Cost" onclick ="getcheckSelection(this.id);" />
                    <asp:RadioButton ID="rdoDefect" runat="server" Text="Defect" onclick ="getcheckSelection(this.id);" /></td>
                            <td><asp:CheckBoxList ID="chkDefectGroup" runat="server" style="display:none;"></asp:CheckBoxList></td>
            </tr>
        </table>
    </fieldset>
    <div class="cssButtonPanel">
        <asp:Button ID="btnGenerateReport" Text="Generate Report"   ToolTip="Generate Report" runat="server" OnClick="btnGenerateReport_Click" OnClientClick="return validate();" />
    </div>
    <asp:HiddenField ID="hdnExport" runat="server" />
</asp:Content>
