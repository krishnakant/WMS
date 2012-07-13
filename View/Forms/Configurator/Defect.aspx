<%@ Page Language="VB" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="false" CodeFile="Defect.aspx.vb" Inherits="View_Forms_Configurator_Defect" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table>
<tr><td>&nbsp;</td></tr>

<tr>
<td>    <asp:Label ID="Label1" runat="server" Text="Model"></asp:Label> </td>
<td>    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>

</tr>
<tr><td>&nbsp;</td></tr>

<tr>
<td>    <asp:Label ID="Label2" runat="server" Text="Products"></asp:Label> </td>


<td>

    <asp:CheckBoxList ID="CheckBoxList1" runat="server" DataSourceID="SqlDataSource1"  DataTextField="Model" DataValueField="Code" BorderStyle="Solid"  BorderColor="Black" BorderWidth="1px" RepeatDirection="Horizontal" RepeatColumns="4">
    </asp:CheckBoxList>



</td>



</tr>
<tr><td>&nbsp;</td></tr>

<tr>

<td>    <asp:Label ID="Label3" runat="server" Text="Description"></asp:Label> </td>

<td>
<asp:TextBox ID="TextBox2" runat="server" TextMode="MultiLine"></asp:TextBox>

</td>



</tr>




</table>

<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:WMSConnectionString %>" SelectCommand="SELECT [Model], [Code] FROM [Model]"></asp:SqlDataSource>



</asp:Content>

