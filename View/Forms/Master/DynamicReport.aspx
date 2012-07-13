<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true" CodeFile="DynamicReport.aspx.cs" Inherits="View_Forms_Master_DynamicReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<fieldset>
<legend>Dynamic Report</legend>
<table>
<tr>
<td style="width: 160px">From Date</td> <td style="width: 158px"><asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>
   
<td>To Date  </td> <td>
 <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox> </td>
</tr>

<tr>
<td style="width: 160px">
Model 
</td>

<td style="width: 158px">

    
    <asp:CheckBoxList ID="CheckBoxList1" runat="server" DataSourceID="SqlDataSource1" DataTextField="Model" DataValueField="Code" BorderStyle="Solid"  BorderColor="Black" BorderWidth="1px" RepeatDirection="Horizontal" RepeatColumns="4" >
    </asp:CheckBoxList>

</td>

<td>
Defect 
</td>

<td >

    
    <asp:CheckBoxList ID="CheckBoxList2" runat="server" DataSourceID="SqlDataSource2" DataTextField="Description" DataValueField="Code" BorderStyle="Solid"  BorderColor="Black" BorderWidth="1px" RepeatDirection="Horizontal" RepeatColumns="8" >
    </asp:CheckBoxList>

</td>





</tr>

<tr>
<td colspan="4">
Hours (HMR)

    <asp:RadioButton ID="RadioButton1" runat="server" Text="Less than 250" GroupName="HMR" /><asp:RadioButton ID="RadioButton2" GroupName="HMR" runat="server" Text="250 to 2500" /><asp:RadioButton ID="RadioButton3" runat="server" Text="All" GroupName="HMR"/>
</td>
<td>



</td>

</tr>

<tr>
<td >&nbsp;</td>
</tr>

<tr>
<td  colspan="4"> Model Type 
    <asp:RadioButton ID="RadioButton4" runat="server" Text="Regular" GroupName="Model"/><asp:RadioButton ID="RadioButton5" runat="server" Text="New" GroupName="Model"/><asp:RadioButton ID="RadioButton6" runat="server" Text="All" GroupName="Model"/>

</td>
</tr>
<tr>
<td >&nbsp;</td>
</tr>


<tr>
<td  colspan="4"> Report based on  
    <asp:RadioButton ID="RadioButton7" runat="server" Text="Cost" GroupName="Cost"/><asp:RadioButton ID="RadioButton8" runat="server" Text="Defect" GroupName="Cost"/>
    
</td>
</tr>


<tr>
<td style="width: 160px">&nbsp;</td>
</tr>

<tr>
<td style="width: 160px">Region</td>
<td style="width: 158px">

    
    <asp:CheckBoxList ID="CheckBoxList3" runat="server" DataSourceID="SqlDataSource3" DataTextField="Region" DataValueField="RegionID" BorderStyle="Solid"  BorderColor="Black" BorderWidth="1px" RepeatDirection="Horizontal" RepeatColumns="4" >
    </asp:CheckBoxList>

</td>


<td style="width: 160px">Delear</td>
<td >

    
    <asp:CheckBoxList ID="CheckBoxList4" runat="server" DataSourceID="SqlDataSource4" DataTextField="Dealer" DataValueField="Code" BorderStyle="Solid"  BorderColor="Black" BorderWidth="1px" RepeatDirection="Vertical" >
    </asp:CheckBoxList>

</td>

</tr>

<tr>
<td colspan="4">

    <asp:Button ID="Button1" runat="server" Text="Generate Report" />
    <asp:Button ID="Button2" runat="server" Text="Export to Excel" />
    
</td>
</tr>
    
    
</table>

  <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:connectionString %>" SelectCommand="SELECT [Model], [Code] FROM [Model]"></asp:SqlDataSource>

 <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:connectionString %>" SelectCommand="SELECT [Description], [Code] FROM [Defect]"></asp:SqlDataSource>
 <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:connectionString %>" SelectCommand="SELECT [Region], [RegionID] FROM [Region]"></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:connectionString %>" SelectCommand="SELECT [Dealer], [Code] FROM [Dealer]"></asp:SqlDataSource>

</fieldset>
</asp:Content>

