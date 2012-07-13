<%@ Page Language="C#" MasterPageFile="~/master/WMSModelWindow.master" AutoEventWireup="true" CodeFile="ProductionPopup.aspx.cs" Inherits="View_Forms_Reports_ProductionPopup" ValidateRequest="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <fieldset class="sectionBorder">
        <legend>Production</legend>
        
               
        <%--<div class="cssButtonPanel">
            <asp:Button ID="btnExport" runat="server" Visible="false" Text="Export to Excel"
                ToolTip="Export to Excel" CssClass="cssButton" OnClick="btnExport_Click" OnClientClick="return CallExport('divGrid');" />
           
        </div>--%>
        <div style="overflow: auto; height: 500px; width: 100%;" id="divGrid_PopUp">
            <asp:GridView ID="grdProdData" runat="server" OnDataBound="eventhandlerSerialNo"
                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                CellPadding="3" AutoGenerateColumns="false" EmptyDataText="No Data Found" AllowPaging="true"
                PageSize="500" Width="100%" OnPageIndexChanging="grdsalesData_Paging">
                <Columns>
                    <asp:BoundField ItemStyle-HorizontalAlign="Center" HeaderText="#" />
                    <asp:BoundField DataField="Material" ItemStyle-HorizontalAlign="Center" HeaderText="Material" />
                    <asp:BoundField DataField="SerialNo" ItemStyle-HorizontalAlign="Center" HeaderText="Serial No" />
                    <asp:BoundField DataField="Plnt" ItemStyle-HorizontalAlign="Center" HeaderText="Plnt" />
                    <asp:BoundField DataField="SLoc" ItemStyle-HorizontalAlign="Center" HeaderText="SLoc" />
                    <asp:BoundField DataField="Description" HeaderStyle-HorizontalAlign="Left" HeaderText="Description of Technical Object" />
                </Columns>
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
        </div>
        <div class="cssButtonPanel" style="width: 98%; margin: auto; margin-top: 20px; margin-bottom: 10px;"
                align="right">
                 <asp:Button ID="btnExport" runat="server" Text="Export to Excel"
                ToolTip="Export to Excel" CssClass="cssButton" OnClick="btnExport_Click" OnClientClick="return CallExport('divGrid_PopUp');" />
                <asp:Button ID="btnClose" CssClass="cssButton" runat="server" Text="Close Window"
                    ToolTip="Close Window" OnClientClick="javascript:return CloseWindow();" />
            </div>
    </fieldset>
    <asp:HiddenField ID="hdnExport" runat="server" />
    <asp:HiddenField ID="hdnYear" runat="server" Value="0" />
<asp:HiddenField ID="hdnModelGroupName" runat="server" Value="0" />
<asp:HiddenField ID="hdnModelCategory" runat="server" Value="0" />
<asp:HiddenField ID="hdnClutchType" runat="server" Value="0" />
<asp:HiddenField ID="hdnModelSpecial" runat="server" Value="0" />
<asp:HiddenField ID="hdnRegion" runat="server" Value="0" />
<asp:HiddenField ID="hdnMonth" runat="server" Value="0" />

<asp:HiddenField ID="hdnFrom" runat="server" Value="0" />
<asp:HiddenField ID="hdnTo" runat="server" Value="0" />
<asp:HiddenField ID="hdnFlag" runat="server" Value="0" />
</asp:Content>

