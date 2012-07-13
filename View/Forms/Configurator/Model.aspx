<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true" CodeFile="Model.aspx.cs" Inherits="View_Forms_Configurator_Model" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table>
<tr>
<td><asp:Label ID="lblModel" runat="server" Font-Bold="true"></asp:Label> </td>
<%--<td><asp:TextBox ID="txtModel" runat="server"></asp:TextBox></td>--%>
<td><asp:DropDownList ID="drpModel" runat="server" OnSelectedIndexChanged="drpModel_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
</tr>
<tr>
<td>    <asp:Label ID="lblProduct" runat="server" Font-Bold="true"></asp:Label> </td>
<td>
<asp:CheckBoxList ID="chkModel" runat="server" BorderStyle="Solid"  BorderColor="Black" BorderWidth="1px" RepeatDirection="Horizontal" RepeatColumns="4">
</asp:CheckBoxList>
</td>
</tr>
<tr>
<td><asp:Label ID="lblDescription" runat="server"  Font-Bold="true"></asp:Label> </td>
<td>
<asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine"></asp:TextBox>
</td>
</tr>
<tr>
<td>
<asp:Label ID="lblActive" runat="server" Text="To be Monitored" Font-Bold="true"></asp:Label>
</td>
<td>
<asp:CheckBox ID="chkActive" runat="server" />
</td>
</tr>



</table>
</asp:Content>

