<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true"
    CodeFile="FileImport.aspx.cs" Inherits="View_Forms_Master_FileImport" %>

<%@ Register TagPrefix="ew" Namespace="eWorld.UI" Assembly="eWorld.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="<%=strProjectName%>/JS/BrowserDetect.js" language="javascript">
    </script>

    <script type="text/javascript" src="<%=strProjectName%>/JS/AjaxBase.js" language="javascript"></script>

    <%--<script src="<%=strProjectName%>/JS/jquery-1.2.3.js" type="text/javascript" charset="utf-8"></script>--%>

    <script type="text/javascript">
    
    
 
    </script>

    <script type="text/javascript" language="javascript">
  $(document).ready(function()
         {
           
            $('#ctl00_ContentPlaceHolder1_txtFromDate').datepicker();
            $('#ctl00_ContentPlaceHolder1_txtToDate').datepicker();
            $('#ctl00_ContentPlaceHolder1_txtSalesFromDate').datepicker();
            $('#ctl00_ContentPlaceHolder1_txtSalesToDate').datepicker();
            $('#ctl00_ContentPlaceHolder1_txtProdFromDate').datepicker();
            $('#ctl00_ContentPlaceHolder1_txtProdToDate').datepicker();
            $('#ctl00_ContentPlaceHolder1_txtEffectiveDate').datepicker();
                        
          });

      function ClientAcrValidateBetweenDate(source, arguments)
       {
        var from = 'ctl00_ContentPlaceHolder1_txtFromDate';
        var to = 'ctl00_ContentPlaceHolder1_txtToDate';
            var status=CheckDateValidation(from,to);
            if(status)
            {
                arguments.IsValid=true;
            }
            else
            {
                arguments.IsValid=false;
            }
       }
        function ClientProdValidateBetweenDate(source, arguments)
       {
        var from = 'ctl00_ContentPlaceHolder1_txtProdFromDate';
        var to = 'ctl00_ContentPlaceHolder1_txtProdToDate';
            var status=CheckDateValidation(from,to);
            if(status)
            {
                arguments.IsValid=true;
            }
            else
            {
                arguments.IsValid=false;
            }
       }
        function ClientSalesValidateBetweenDate(source, arguments)
       {
        var from = 'ctl00_ContentPlaceHolder1_txtSalesFromDate';
        var to = 'ctl00_ContentPlaceHolder1_txtSalesToDate';
            var status=CheckDateValidation(from,to);
            if(status)
            {
                arguments.IsValid=true;
            }
            else
            {
                arguments.IsValid=false;
            }
       }
function getSelectedindex(val)
{
    var hdnplantid= 'ctl00_ContentPlaceHolder1_hdnselplant';
    document.getElementById(hdnplantid).value = val;
    //alert(document.getElementById(hdnplantid).value);
}

 function showMessage(id)
 {
       el = document.getElementById(id);
       el.style.visibility = (el.style.visibility == "visible") ? "hidden" : "visible";
 }
    
//function getLargeImage(img1)
//{
//       document.getElementById('divtest').innerHTML= '' +'<div id="popupControls" style="border-color:#000000;width:100%;height:100%;">' +
//				'<img src="'+img1+'" border="1"  border-color="#000000" id="pop1CloseBox" />' +
//				'<span><center><input type="button" class="cssButton" value="Close" onclick="javascript:closeModelDiv();" /></center> <span></div> ' ;
//     showMessage('divtest');
//}

 function getLargeImage(img1,imageTitle)
    {
     
        document.getElementById('divtest').innerHTML= '' +
       			'<div id="popupControls" align="center" style="background-color:#000;color:#fff;width:100%;height:auto;overflow:scroll;">' +
					'<img src="/WMS/Images/Close.jpg" align="right" onclick="javascript:closeModelDiv();" title="Close"    /> <br /> <img src="'+img1+'" border="1"  border-color="#000000" id="pop1CloseBox" /> <br /> <center> <span style="color:white;font-size:16px;font-family:verdana;margin-bottom:10px;"> <b>'+imageTitle+' </b></span> </center>' +
				'  </div> ' ;
     
        showMassage('divtest');
    }

function closeModelDiv()
{
       document.getElementById('divtest').innerHTML= '';
       showMessage('divtest');
}

function openPopup(param)
{
//alert(param);
var filename = '';
filename = param+'.aspx';
window.location.href='<%=strProjectName%>/View/Forms/Exceptions/'+filename;
return false;
}

  
function setMessageText(ID,Text)
{
   document.getElementById(ID).innerHTML =Text ;
   setTimeout("setMessageText('"+ID+"','')",3000);
} 
 var Status='';
 var globalTablename='';
 
function getStatus(tablename)
{
globalTablename = tablename;
   
    if(confirm('Do you want to replace duplicate data'))
    {
  
        var fieldsAjax1 = new Array();
	    fieldsAjax1.push("Status="+tablename);
	    Status = new AjaxController("<%=strProjectName%>/Handler.aspx",fieldsAjax1.join("&"), performUpdate); 
	    Status.GetData();
	    
     }
}

      function performUpdate()
	{
    var tablename = globalTablename;
	  try
        {		 
          if(Status.ParseResult())
	      {
	         for (i = 0; i < Status.resultSet.length; i++) 
	         {
                var cols=Status.resultSet[i];
                var count = cols[0];
                //alert(count);
               var lbl='';
                if(tablename == 'defect')
                {
                lbl = 'ctl00_ContentPlaceHolder1_lblDefectStatus';
                }
                else if(tablename == 'culprit')
                {
                 lbl = 'ctl00_ContentPlaceHolder1_lblCulpritStatus';
                }
                else if(tablename == 'customervoice')
                {
                 lbl = 'ctl00_ContentPlaceHolder1_lblCustVoiceStatus';
                }
                else if(tablename == 'item')
                {
                 lbl = 'ctl00_ContentPlaceHolder1_lblItemStatus';
                }
                
                document.getElementById(lbl).innerHTML = count;
             }
           }           
        }
        catch(e) {}
	}



  
   
    </script>

    <div>
        <fieldset class="sectionBorder">
            <legend>Import</legend>
            <div>
                <table width="90%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="cssLabel">
                            <asp:Label ID="lblPlant" runat="server" Text="Select Plant:"></asp:Label>
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rdoPlant" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0" Selected="True" onclick="getSelectedindex(this.value);">Bhopal</asp:ListItem>
                                <asp:ListItem Value="1" onclick="getSelectedindex(this.value);">Alwar</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class="cssLabel">
                            <asp:Label ID="lblFileAcr" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:FileUpload ID="fileUpldAcr" onchange="CheckXlsFileExtension(this.id);" runat="server" />
                        </td>
                        <td>
                            <asp:Button ID="btnAcrImport" ToolTip="Import" runat="server" Text="Import" OnClick="btnAcrImport_Click"
                                OnClientClick="return validate('acr');" />
                        </td>
                        <td class="cssLabel">
                            <asp:Label ID="lblAcrFromDate" runat="server" Text="From:"></asp:Label>
                        </td>
                        <td>
                           
                            <asp:TextBox ID="txtFromDate" runat="server" Width="80px"></asp:TextBox>
                        </td>
                        <td class="cssLabel">
                            <asp:Label ID="lblAcrtoDate" runat="server" Text="To:"></asp:Label>
                        </td>
                        <td>
                           
                            <asp:TextBox ID="txtToDate" runat="server" Width="80px"></asp:TextBox>
                        </td>
                        <td class="cssLabel">
                            <asp:Label ID="lblAcrStatus" runat="server" ForeColor="red"></asp:Label>
                        </td>
                        <td>
                            <input type="button" id="btnPreviewAcr" class="cssButton" title="Sample Excel file format"
                                value="Sample Excel file format" onclick="javascript:getLargeImage('/WMS/images/ACRTemp.jpg','Acr');"
                                runat="server" />
                        </td>
                        <td>
                            <asp:LinkButton ID="lnkExceptions" runat="server" Visible="false" Text="Exceptions"
                                OnClientClick="return openPopup('Exception');"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:CustomValidator ID="CustomValidator3" runat="server" ErrorMessage="Invalid Date"
                                ClientValidationFunction="ValidDate" ControlToValidate="txtFromDate"></asp:CustomValidator>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Invalid Date"
                                ClientValidationFunction="ValidDate" ControlToValidate="txtToDate"></asp:CustomValidator>
                            <asp:CustomValidator ID="CustomValidator2" runat="server" ErrorMessage="*" ClientValidationFunction="ClientAcrValidateBetweenDate"
                                ControlToValidate="txtToDate"> </asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="cssLabel">
                            <asp:Label ID="lblFileSales" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:FileUpload ID="fileUpldSales" onchange="CheckXlsFileExtension(this.id);" runat="server"
                                CssClass="file_1" />
                        </td>
                        <td>
                            <asp:Button ID="btnSalesImport" ToolTip="Import" runat="server" Text="Import" OnClick="btnSalesImport_Click"
                                OnClientClick="return validate('sales');" />
                        </td>
                        <td class="cssLabel">
                            <asp:Label ID="lblSalesFromDate" runat="server" Text="From:"></asp:Label>
                        </td>
                        <td>
                           
                            <asp:TextBox ID="txtSalesFromDate" runat="server" Width="80px"></asp:TextBox>
                        </td>
                        <td class="cssLabel">
                            <asp:Label ID="lblSalestoDate" runat="server" Text="To:"></asp:Label>
                        </td>
                        <td>
                           
                            <asp:TextBox ID="txtSalesToDate" runat="server" Width="80px"></asp:TextBox>
                        </td>
                        <td class="cssLabel">
                            <asp:Label ID="lblSalesStatus" runat="server" ForeColor="red"></asp:Label>
                        </td>
                        <td>
                            <input type="button" id="btnPreviewSales" class="cssButton" title="Sample Excel file format"
                                value="Sample Excel file format" onclick="javascript:getLargeImage('/WMS/images/SalesImport.jpg','Sales');"
                                runat="server" />
                        </td>
                        <td>
                            <asp:LinkButton ID="lnkSalesException" runat="server" Visible="false" Text="Exceptions"
                                OnClientClick="return openPopup('Exception');"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:CustomValidator ID="CustomValidator4" runat="server" ErrorMessage="Invalid Date"
                                ClientValidationFunction="ValidDate" ControlToValidate="txtSalesFromDate"></asp:CustomValidator>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:CustomValidator ID="CustomValidator5" runat="server" ErrorMessage="Invalid Date"
                                ClientValidationFunction="ValidDate" ControlToValidate="txtSalesToDate"></asp:CustomValidator>
                            <asp:CustomValidator ID="CustomValidator6" runat="server" ErrorMessage="*" ClientValidationFunction="ClientSalesValidateBetweenDate"
                                ControlToValidate="txtSalesToDate"> </asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="cssLabel">
                            <asp:Label ID="lblFileProd" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:FileUpload ID="fileUpldProd" onchange="CheckXlsFileExtension(this.id);" runat="server"
                                CssClass="file_1" />
                        </td>
                        <td>
                            <asp:Button ID="btnProdImport" runat="server" ToolTip="Import" Text="Import" OnClick="btnProdImport_Click"
                                OnClientClick="return validate('production');" />
                        </td>
                        <td class="cssLabel">
                            <asp:Label ID="lblProdFromDate" runat="server" Text="From:"></asp:Label>
                        </td>
                        <td>
                           
                            <asp:TextBox ID="txtProdFromDate" runat="server" Width="80px"></asp:TextBox>
                        </td>
                        <td class="cssLabel">
                            <asp:Label ID="lblProdtoDate" runat="server" Text="To:"></asp:Label>
                        </td>
                        <td>
                           
                            <asp:TextBox ID="txtProdToDate" runat="server" Width="80px"></asp:TextBox>
                        </td>
                        <td class="cssLabel">
                            <asp:Label ID="lblProductionStatus" runat="server" ForeColor="red"></asp:Label>
                        </td>
                        <td>
                            <input type="button" id="btnPreviewProd" class="cssButton" title="Sample Excel file format"
                                value="Sample Excel file format" onclick="javascript:getLargeImage('/WMS/images/Production.jpg','Production');"
                                runat="server" />
                        </td>
                        <td>
                            <asp:LinkButton ID="lnkProdException" runat="server" Visible="false" Text="Exceptions"
                                OnClientClick="return openPopup('Exception');"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:CustomValidator ID="CustomValidator7" runat="server" ErrorMessage="Invalid Date"
                                ClientValidationFunction="ValidDate" ControlToValidate="txtProdFromDate"></asp:CustomValidator>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:CustomValidator ID="CustomValidator8" runat="server" ErrorMessage="Invalid Date"
                                ClientValidationFunction="ValidDate" ControlToValidate="txtProdToDate"></asp:CustomValidator>
                            <asp:CustomValidator ID="CustomValidator9" runat="server" ErrorMessage="*" ClientValidationFunction="ClientProdValidateBetweenDate"
                                ControlToValidate="txtProdToDate"> </asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="cssLabel">
                            <asp:Label ID="lblFileItem" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:FileUpload ID="fileUpldItem" onchange="CheckXlsFileExtension(this.id);" runat="server"
                                CssClass="file_1" />
                        </td>
                        <td colspan="5">
                            <asp:Button ID="btnItemImport" ToolTip="Import" runat="server" Text="Import" OnClick="btnItemImport_Click"
                                OnClientClick="return validate('item');" />
                        </td>
                        <td class="cssLabel">
                            <asp:Label ID="lblItemStatus" runat="server" ForeColor="red"></asp:Label>
                        </td>
                        <td colspan="2">
                            <input type="button" id="btnPreviewItem" class="cssButton" title="Sample Excel file format"
                                value="Sample Excel file format" onclick="javascript:getLargeImage('/WMS/images/ItemImage.bmp','Item');"
                                runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="cssLabel">
                            <asp:Label ID="lblFileDefect" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:FileUpload ID="fileUpldDefect" onchange="CheckXlsFileExtension(this.id);" runat="server"
                                CssClass="file_1" />
                        </td>
                        <td colspan="5">
                            <asp:Button ID="btnDefectImport" ToolTip="Import" runat="server" Text="Import" OnClick="btnDefectImport_Click"
                                OnClientClick="return validate('defect');" />
                        </td>
                        <td class="cssLabel">
                            <asp:Label ID="lblDefectStatus" runat="server" ForeColor="red"></asp:Label>
                        </td>
                        <td colspan="2">
                            <input type="button" id="btnPreviewDefect" class="cssButton" title="Sample Excel file format"
                                value="Sample Excel file format" onclick="javascript:getLargeImage('/WMS/images/Defect.jpg','Defect');"
                                runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="cssLabel">
                            <asp:Label ID="lblFileCulprit" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:FileUpload ID="fileUpldCulprit" onchange="CheckXlsFileExtension(this.id);" runat="server"
                                CssClass="file_1" />
                        </td>
                        <td colspan="5">
                            <asp:Button ID="btnCulpritImport" ToolTip="Import" runat="server" Text="Import" OnClick="btnCulpritImport_Click"
                                OnClientClick="return validate('culprit');" />
                        </td>
                        <td class="cssLabel">
                            <asp:Label ID="lblCulpritStatus" runat="server" ForeColor="red"></asp:Label>
                        </td>
                        <td colspan="2">
                            <input type="button" id="btnPreviewCulprit" class="cssButton" title="Sample Excel file format"
                                value="Sample Excel file format" onclick="javascript:getLargeImage('/WMS/images/culprit.jpg','Culprit');"
                                runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="cssLabel">
                            <asp:Label ID="lblFileCustVoice" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:FileUpload ID="fileUpldCustVoice" onchange="CheckXlsFileExtension(this.id);"
                                runat="server" CssClass="file_1" />
                        </td>
                        <td colspan="5">
                            <asp:Button ID="btnCustVoiceImport" ToolTip="Import" runat="server" Text="Import"
                                OnClick="btnCustVoiceImport_Click" OnClientClick="return validate('cvoice');" />
                        </td>
                        <td class="cssLabel">
                            <asp:Label ID="lblCustVoiceStatus" runat="server" ForeColor="red"></asp:Label>
                        </td>
                        <td colspan="2">
                            <input type="button" id="btnPreviewCustVoice" class="cssButton" title="Sample Excel file format"
                                value="Sample Excel file format" onclick="javascript:getLargeImage('/WMS/images/Cvoice.jpg','CustomerVoice');"
                                runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="cssLabel">
                            <asp:Label ID="lblDealerImport" runat="server" Text="Dealer Master"></asp:Label>
                        </td>
                        <td>
                            <asp:FileUpload ID="fileupldDealer" onchange="CheckXlsFileExtension(this.id);" runat="server"
                                ToolTip="Click to browse Excel file" />
                        </td>
                        <td>
                            <asp:Button ID="btnDealer" ToolTip="Import" runat="server" Text="Import" OnClick="btnDealer_Click" />
                        </td>
                        <td class="cssLabel">
                            <asp:Label ID="lblEffectiveDate" runat="server" Text="Effective Date:"></asp:Label>
                        </td>
                        <td>
                      
                            <asp:TextBox ID="txtEffectiveDate" runat="server" Width="80px"></asp:TextBox>
                            <asp:CustomValidator ID="CustomValidator10" runat="server" ErrorMessage="Invalid Date"
                                ClientValidationFunction="ValidDate" ControlToValidate="txtEffectiveDate"></asp:CustomValidator>
                        </td>
                        <td colspan="2"></td>
                         
                        <td class="cssLabel">
                            <asp:Label ID="lblDealerStatus" runat="server" ForeColor="red"></asp:Label>
                        </td>
                        <td>
                            <input type="button" id="btnPreviewDealer" class="cssButton" title="Sample Excel file format"
                                value="Sample Excel file format" onclick="javascript:getLargeImage('/WMS/images/dealer.jpg','Dealer');"
                                runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <div class="cssButtonPanel">
                <asp:Button ID="btnImport" runat="server" Text="Import" OnClick="btnImport_Click"
                    Visible="false" />
                <asp:Button ID="btnCancel" ToolTip="Cancel" runat="server" Text="Cancel" />
            </div>
        </fieldset>
    </div>
    <div style="border-color: #000000; visibility: hidden; position: absolute; margin-left: 10px;
        top: 10px; width: 100%; height: 409px; text-align: center;" id="divtest">
    </div>
    <asp:HiddenField ID="hdnStatus" runat="server" />
    <asp:HiddenField ID="hdnselplant" runat="server" />
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
    <asp:Literal ID="Literal2" runat="server"></asp:Literal>
    <asp:Literal ID="Literal3" runat="server"></asp:Literal>
    <asp:Literal ID="Literal4" runat="server"></asp:Literal>
    <asp:Literal ID="Literal5" runat="server"></asp:Literal>
    <asp:Literal ID="Literal6" runat="server"></asp:Literal>
    <asp:Literal ID="Literal7" runat="server"></asp:Literal>
</asp:Content>
