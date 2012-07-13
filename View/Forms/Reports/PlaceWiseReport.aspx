<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true"
    CodeFile="PlaceWiseReport.aspx.cs" Inherits="View_Forms_Reports_PlaceWiseReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
function ClientValidatePlace(source, arguments)
{
      if (document.getElementById('ctl00_ContentPlaceHolder1_ddlPlace').value!=0)
         arguments.IsValid=true;
      else
         arguments.IsValid=false;
}        
    </script>

    <fieldset class="sectionBorder">
        <legend>Plant Wise Report</legend>
        <center>
            <table width="950px" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <b>Plant:</b>
                        <asp:DropDownList ID="ddlPlace" Width="85px" ToolTip="Role" runat="server">
                            <asp:ListItem Selected="true" Value="0">Select</asp:ListItem>
                            <asp:ListItem Value="A">Alwar</asp:ListItem>
                            <asp:ListItem Value="B">Bhopal </asp:ListItem>
                            <asp:ListItem Value="C">All</asp:ListItem>
                        </asp:DropDownList>
                        <asp:CustomValidator ID="CustomValidator" ErrorMessage="Select" ClientValidationFunction="ClientValidatePlace"
                            ControlToValidate="ddlPlace" runat="server"></asp:CustomValidator>
                    </td>
                </tr>
            </table>
            <br />
            <div class="cssButtonPanel" style="width: 950px;">
                <asp:Button ID="btnGo" Text="Go" runat="server" ToolTip="Go" OnClick="btnGo_Click" />
                <input type="button" value="Print" runat="server" visible="false" title="Print" id="btnPrint"
                    class="cssButton" onclick="javascript:CallPrint('divGrid');" />
                <asp:Button ID="btnExport" Visible="false" OnClientClick="return CallExport('divGrid');"
                    runat="server" CssClass="cssButton" Text="Excel Export" ToolTip="Excel Export"
                    OnClick="Button1_Click"></asp:Button>
            </div>
            <br />
            <div id="divGrid" style="overflow: auto; height: 400px; width: 950px;">
                <asp:GridView ID="gdPlace" OnRowCreated="gridView_RowCreated" AutoGenerateColumns="false"
                    OnDataBound="eventhandlerSerialNo" runat="server" AllowPaging="true" PageSize="500"
                    OnPageIndexChanging="grdacrData_Paging" EmptyDataText="No Records Found">
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
                        <asp:BoundField DataField="BLANK" ItemStyle-HorizontalAlign="Center" HeaderText="Blank" />
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
        </center>
    </fieldset>
    <asp:Literal ID="literal1" runat="server"></asp:Literal>
    <asp:HiddenField ID="hdnExport" runat="server" />
</asp:Content>
