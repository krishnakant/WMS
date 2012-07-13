<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true"
    ValidateRequest="false" CodeFile="ChassisWiseACRDetail.aspx.cs" Inherits="View_Forms_Reports_ChassisWiseACRDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
 function ClientValidateModel(source, arguments){
    
      if (document.getElementById('ctl00_ContentPlaceHolder1_drpChassis').value!='')
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
    </script>

    <fieldset class="sectionBorder">
        <legend>Chassis Wise ACR Detail</legend>
        <table width="950" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="cssLabel">
                    <asp:Label ID="lblGroupName" runat="server" Text="Chassis:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtChassisNo" runat="server"></asp:TextBox>
                </td>
                <td>
                    <span class="cssLabel">Problem Type:</span>
                    <asp:RadioButton ID="rdoPrimary" runat="server" Text="Primary" GroupName="Problem" />
                    <asp:RadioButton ID="rdoConsequences" GroupName="Problem" runat="server" Text="Consequences" />
                    <asp:RadioButton ID="rdoAllProblem" GroupName="Problem" runat="server" Text="All"
                        Checked="true" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:RadioButtonList ID="rdoData" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="0" onclick="getDiv(this.value);">Overall</asp:ListItem>
                        <asp:ListItem Value="1" onclick="getDiv(this.value);">Engine</asp:ListItem>
                        <asp:ListItem Value="2" onclick="getDiv(this.value);">Tractor</asp:ListItem>
                    </asp:RadioButtonList></td>
                <td id="placediv">
                    <span class="cssLabel">Place:</span>
                    <asp:RadioButton ID="rdoAlwar" runat="server" Text="Alwar" GroupName="Place" /><asp:RadioButton
                        ID="rdoBhopal" GroupName="Place" runat="server" Text="Bhopal" /><asp:RadioButton
                            ID="rdoAllPlace" runat="server" Checked="true" Text="Both" GroupName="Place" /></td>
                <td id="enginediv" style="display: none;">
                    <span class="cssLabel">Engine:</span>
                    <asp:RadioButton ID="rdoAlwarEngine" runat="server" Text="Alwar" GroupName="Engine" /><asp:RadioButton
                        ID="rdoSimpsonEngine" GroupName="Engine" runat="server" Text="Simpson" /><asp:RadioButton
                            ID="rdoBothEngine" runat="server" Checked="true" Text="Both" GroupName="Engine" /></td>
                <td>
                    <span class="cssLabel">Year:</span>
                    <asp:RadioButton ID="rdoFirst" runat="server" Text="I" GroupName="warrantyear" />
                    <asp:RadioButton ID="rdoSecond" runat="server" Text="II" GroupName="warrantyear" />
                    <asp:RadioButton ID="rdoBothYear" runat="server" Text="Both" Checked="true" GroupName="warrantyear" />
                </td>
            </tr>
        </table>
        <br />
        <div class="cssButtonPanel" style="width: 950px;">
            <asp:Button ID="btnGo" Text="Go" runat="server" ToolTip="Go" OnClick="btnGo_Click1" />
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
