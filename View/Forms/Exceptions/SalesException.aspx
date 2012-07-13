<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true" CodeFile="SalesException.aspx.cs" Inherits="View_Forms_Exceptions_SalesException" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
function GotoException(id)
{
            var ctrlID = id.split('_');
            var ExType = 'ctl00_ContentPlaceHolder1_grdExceptionCount_'+ctrlID[3]+'_lblExType';
            var ExRef = document.getElementById(ExType).innerHTML;
            var url = '<%=strProjectName%>/View/Forms/Exceptions/'+ExRef+'Exception.aspx';
            window.location.href=url;
           return false;
            
}

</script>
<fieldset>
<legend>Exceptions</legend>
<div style="overflow:auto; height:335px; width:950px;">
<asp:GridView ID="grdExceptionCount" OnRowCreated="gridView_RowCreated" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" EmptyDataText="No Data Found">
    <FooterStyle BackColor="White" ForeColor="#000066" />
    <RowStyle ForeColor="#000066" />
    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
    <Columns>
     <asp:TemplateField HeaderText="Exception Type" ItemStyle-HorizontalAlign="Center">
        <ItemTemplate>
        <asp:Label ID="lblExType" runat="server" Text='<%# Bind("Cause") %>' />
        </ItemTemplate>
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="No. of Exceptions" ItemStyle-HorizontalAlign="Center">
        <ItemTemplate> 
        <asp:LinkButton ID="lnkException" runat="server" Text='<%# Bind("CountEx") %>' OnClientClick="return GotoException(this.id);" ></asp:LinkButton>       
        </ItemTemplate>
        </asp:TemplateField>
    </Columns>

</asp:GridView>

</div>
</fieldset>
</asp:Content>

