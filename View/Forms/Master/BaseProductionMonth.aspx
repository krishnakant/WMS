<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true"
    CodeFile="BaseProductionMonth.aspx.cs" Inherits="View_Forms_Master_BaseProductionMonth"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset class="sectionBorder">
        <legend>Base Production Model</legend>
         <div style="margin-left:2%;margin-top:2%;">
            <table width="90%" border="0" cellpadding="0" cellspacing="0" >
                <tr>
                    <td class="cssLabel">
                        <asp:Label ID="lblCode" runat="server" Text="Code:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCode" runat="server" MaxLength="3" CausesValidation="true"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                            ControlToValidate="txtCode"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please Enter numeric values only "
                            ValidationExpression="^([0-9]*\s?[0-9]*)+$" ControlToValidate="txtCode"></asp:RegularExpressionValidator></td>
                </tr>
                <tr>
                    <td class="cssLabel">
                        <asp:Label ID="LabelMonth" runat="server" Text="Month:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="drpMonth"  runat="server">
                            <asp:ListItem Selected="true" Value="1">Jan</asp:ListItem>
                            <asp:ListItem Value="2">Feb</asp:ListItem>
                            <asp:ListItem Value="3">Mar</asp:ListItem>
                            <asp:ListItem Value="4">Apr</asp:ListItem>
                            <asp:ListItem Value="5">May</asp:ListItem>
                            <asp:ListItem Value="6">Jun</asp:ListItem>
                            <asp:ListItem Value="7">Jul</asp:ListItem>
                            <asp:ListItem Value="8">Aug</asp:ListItem>
                            <asp:ListItem Value="9">Sept</asp:ListItem>
                            <asp:ListItem Value="10">Oct</asp:ListItem>
                            <asp:ListItem Value="11">Nov</asp:ListItem>
                            <asp:ListItem Value="12">Dec</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="cssLabel">
                        <asp:Label ID="Year" runat="server" Text="Year:"></asp:Label>
                    </td>
                    <td>
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
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
         <br />
    </fieldset>
   
    <div class="cssButtonPanel">
        <asp:Button ID="btnUpdate" ToolTip="Update" Text="Update" OnClick="btnUpdate_Click" runat="server" />
    </div>
    <asp:Label ID="Label1" runat="server" ></asp:Label>
</asp:Content>
