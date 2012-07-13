<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true"
    CodeFile="MonthOpenClose.aspx.cs" Inherits="View_Forms_Configurator_MonthOpenClose" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">

function setMessageText(ID,Text)
{
   document.getElementById(ID).innerHTML =Text ;
   setTimeout("setMessageText('"+ID+"','')",3000);
} 
    </script>

    <fieldset class="sectionBorder">
        <legend>Month Open/Close</legend>
        <div style="margin-left:2%;margin-top:2%;">
            <table width="90%" border="0" cellpadding="0" cellspacing="0" >
                <tr>
                    <td class="cssLabel">
                        <asp:Label ID="lblMonth" runat="server" Text="Month:" ></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="drpMonth" runat="server">
                            <asp:ListItem Value="1">January</asp:ListItem>
                            <asp:ListItem Value="2">February</asp:ListItem>
                            <asp:ListItem Value="3">March</asp:ListItem>
                            <asp:ListItem Value="4">April</asp:ListItem>
                            <asp:ListItem Value="5">May</asp:ListItem>
                            <asp:ListItem Value="6">June</asp:ListItem>
                            <asp:ListItem Value="7">July</asp:ListItem>
                            <asp:ListItem Value="8">August</asp:ListItem>
                            <asp:ListItem Value="9">September</asp:ListItem>
                            <asp:ListItem Value="10">October</asp:ListItem>
                            <asp:ListItem Value="11">November</asp:ListItem>
                            <asp:ListItem Value="12">December</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="cssLabel">
                        <asp:Label ID="Year" runat="server" Text="Year:" ></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="drpYear" runat="server">
                            <asp:ListItem>2008</asp:ListItem>
                            <asp:ListItem>2009</asp:ListItem>
                            <asp:ListItem>2010</asp:ListItem>
                            <asp:ListItem>2011</asp:ListItem>
                            <asp:ListItem>2012</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="cssLabel">
                        <asp:Label ID="lblOpenClose" runat="server" Text="Open/Close Status:" ></asp:Label>
                    </td>
                    <td>
                        <asp:CheckBox ID="chkOpenClose" runat="server" Checked="true" />
                    </td>
                </tr>
             
                <tr>
                    <td colspan="3">
                        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
         
    </fieldset>
     <div class="cssButtonPanel">
          <asp:Button ID="btnSave" Text="Save"  ToolTip="Save" runat="server" OnClick="btnSave_Click" />
       
          </div>
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
</asp:Content>
