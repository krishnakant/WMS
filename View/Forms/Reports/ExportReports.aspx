<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/master/MasterPage.master" CodeFile="ExportReports.aspx.cs" Inherits="View_Forms_Reports_ExportReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">
    function CallExport(strid){
    
        var prtContent = document.getElementById(strid); 
        alert(prtContent.innerHTML);       
        document.getElementById('ctl00_ContentPlaceHolder1_hdnExport').value=prtContent.innerHTML;          
    } 
    </script>

    <div style="vertical-align: top;">
        <asp:Button ID="btnExport" Visible="false" runat="server" Text="Export" CssClass="cssButton"
            OnClientClick="javascript:CallExport('print_Grid');" OnClick="btnExport_Click" />
    </div>
    <div id="print_Grid" runat="server">
    <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="true" SelectedRowStyle-CssClass="GridItems" OnDataBound="eventhandlerSerialNo"
            HeaderStyle-CssClass="GridHeader" RowStyle-CssClass="GridItems" AlternatingRowStyle-CssClass="GridItemAlter"
            Width="100%" DataMember="DefaultView">
            
           <FooterStyle BackColor="White" ForeColor="#000066" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
    </div>
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
    <asp:HiddenField ID="hdnExport" runat="server" />
</asp:Content>

