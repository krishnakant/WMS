<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true"
    CodeFile="ReworkCost.aspx.cs" Inherits="View_Forms_DataInput_ReworkCost" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
 
        var Status='';
        function getModelDetails(id)
        {
              var ModelID = document.getElementById(id).value;
              var fieldsAjax1 = new Array();
	          fieldsAjax1.push("Status=ModelDetails");
	          fieldsAjax1.push("ModelGroupID="+ModelID);
	          Status = new AjaxController("/WMS/Handler.aspx",fieldsAjax1.join("&"), getContent); 
	          Status.GetData();
	          return false;       	               
        }

        function getContent()
	    {
        
	      try
            {		 
              if(Status.ParseResult())
	          {
	             for (i = 0; i < Status.resultSet.length; i++) 
	             {
                    var cols=Status.resultSet[i];
                    var count = cols[0];
                    var warrantyperiod = cols[1];
                    
                    if(warrantyperiod =='1')
                    {
                         document.getElementById('trYearIItxt').style.display='none';
                         document.getElementById('trYearIIlbl').style.display='none';
                    }
                    else
                    {
                         document.getElementById('trYearIItxt').style.display='';
                         document.getElementById('trYearIIlbl').style.display='';
                    }                    
                  
                 }
               }           
            }
            catch(e) {}
	    }
	    
	     function setMessageText(ID,Text)
        {
           document.getElementById(ID).innerHTML =Text ;
           setTimeout("setMessageText('"+ID+"','')",3000);
        } 

    </script>

    <fieldset class="sectionBorder">
        <legend>Rework Cost</legend>
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="cssLabel">
                    Period:</td>
                <td>
                    <asp:DropDownList ID="drpMonth" runat="server">
                        <asp:ListItem Value="0" Selected="True">Select</asp:ListItem>
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
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td class="cssLabel">
                    Model:</td>
                <td>
                    <asp:DropDownList ID="drpModel" runat="server" onchange="javascript:return getModelDetails(this.id);">
                    </asp:DropDownList>
                </td>
                <td>
                    <span class="cssLabel">Category:</span>
                </td>
                <td>
                    <asp:RadioButtonList RepeatDirection="Horizontal" ID="rdoModelCategory" runat="server">
                        <asp:ListItem Value="1">Regular</asp:ListItem>
                        <asp:ListItem Value="2">New</asp:ListItem>
                    </asp:RadioButtonList></td>
            </tr>
            <tr>
                <td class="cssLabel">
                    HMR:</td>
                <td>
                    <asp:RadioButtonList RepeatDirection="Horizontal" ID="rdoHMR" runat="server">
                        <asp:ListItem Value="1">0-250</asp:ListItem>
                        <asp:ListItem Value="2">251-2500</asp:ListItem>
                       
                    </asp:RadioButtonList></td>
            </tr>
            <tr>
                <td class="cssLabel">
                    Rework Cost I Year:
                </td>
                <td>
                    <asp:TextBox ID="txtReworkCostIYear" runat="server" Text="0"></asp:TextBox>
                </td>
                <td id='trYearIIlbl' style="display: none;">
                    <span class="cssLabel">Rework Cost II Year:</span>
                </td>
                <td id='trYearIItxt' style="display: none;">
                    <asp:TextBox ID="txtReworkCostIIYear" runat="server" Text="0"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div class="cssButtonPanel">
            <asp:Label ID="lblMessage" runat="server" ForeColor="red"></asp:Label>
            <asp:Button ID="btnSave" CssClass="cssButton" runat="server" Text="Save" ToolTip="Save"
                OnClick="btnSave_Click" />
            <asp:Button ID="btnCancel" CssClass="cssButton" runat="server" Text="Cancel" ToolTip="Cancel" />
        </div>
    </fieldset>
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>

    <script type="text/javascript">
         var ddlid = 'ctl00_ContentPlaceHolder1_drpModel';
         getModelDetails(ddlid);
    </script>

</asp:Content>
