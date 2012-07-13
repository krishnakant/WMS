<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true"
    CodeFile="ClaimGenerateReport.aspx.cs" Inherits="View_Forms_Reports_ClaimGenerateReport" ValidateRequest="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script type="text/javascript">
function CallExportGrid( strid )
   {
        var prtContent = document.getElementById(strid);
        document.getElementById('ctl00_ContentPlaceHolder1_hdnExport').value=prtContent.innerHTML;
     
   } 
   
</script>
 <fieldset class="sectionBorder">
        <legend>Claim Report</legend>
    <div class="cssButtonPanel">
        <input type="button" value="Print" runat="server" title="Print" id="btnPrint" class="cssButton"
            onclick="javascript:CallPrint('divGridExport');" />
        <asp:Button ID="btnExport" OnClientClick="javascript:CallExportGrid('divGridExport');" runat="server"
            CssClass="cssButton" Text="Excel Export" ToolTip="Excel Export" OnClick="btnExport_Click">
        </asp:Button></div>
    <div id="divGridExport" style="width: 100%; overflow-x: scroll; margin-top: 1%;">
        <asp:DetailsView ID="dtlClaimDetail" runat="server" Height="50px" Width="125px" BackColor="White"
            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <EditRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <RowStyle ForeColor="#000066" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
        </asp:DetailsView>
        <asp:GridView ID="grdClaimDetail" runat="server" EmptyDataText="Data not found" BackColor="White"
            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" RowStyle-HorizontalAlign="Center"
            ShowFooter="true" OnRowDataBound="grdClaimDetail_RowDataBound" OnSorting="SortingData"
            AutoGenerateColumns="false" OnDataBound="eventhandlerSerialNo">
            <Columns>
                <asp:BoundField ItemStyle-HorizontalAlign="Center" HeaderText="Serial No." />
                <asp:BoundField DataField="Value" HeaderText="Value" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Defect" HeaderText="Defect" ItemStyle-HorizontalAlign="Center" />
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
