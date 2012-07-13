<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true" CodeFile="YearWiseReport.aspx.cs" Inherits="View_Forms_Exceptions_YearWiseReport"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script type="text/javascript">
function ClientValidateYear(source, arguments)
{
      if (document.getElementById('ctl00_ContentPlaceHolder1_ddlYear').value!=0)
         arguments.IsValid=true;
      else
         arguments.IsValid=false;
}        
</script>
    <fieldset class="sectionBorder">
        <legend>Year Wise Report</legend>
        <center>
        <div style="overflow: auto; height: 400px; width: 950px;">
           
                <table>
                    <tr>
                        <%--<td class="cssLabel">
                            <asp:Label ID="lblRole" runat="server" Text="Role:"></asp:Label></td>--%>
                        <td>
                            <asp:DropDownList ID="ddlYear" Width="85px" ToolTip="Role" runat="server">
                             <asp:ListItem Selected="true" Value="0">Select</asp:ListItem>
                           <asp:ListItem  Value="1">IYear</asp:ListItem>
                            <asp:ListItem Value="2">IIYear</asp:ListItem>
                            <asp:ListItem Value="3">AfterIIYear</asp:ListItem>
                            <asp:ListItem Value="4">Total</asp:ListItem>
                           
                            </asp:DropDownList>
                             <asp:CustomValidator ID="CustomValidator" ErrorMessage="Select" ClientValidationFunction="ClientValidateYear"
                                    ControlToValidate="ddlYear" runat="server"></asp:CustomValidator>
                        </td>
                        <td>
                          <asp:Button ID="btnGo" Width="40px" CausesValidation="true" Text="Go" 
            runat="server" ToolTip="Go" OnClick="btnGo_Click" />
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="gdDefect" AutoGenerateColumns="false" OnRowCreated="gridView_RowCreated" OnDataBound="eventhandlerSerialNo"  runat="server">
                    <Columns>
                        <asp:BoundField HeaderText="#" ReadOnly="True">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Model_Code" HeaderText="Model_Code">
                            <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Quantity" HeaderText="No. Of Def.">
                            <ItemStyle HorizontalAlign="center" />
                            
                        </asp:BoundField>
                            <asp:BoundField DataField="Value" HeaderText="Value">
                            <ItemStyle HorizontalAlign="center" />
                            
                        </asp:BoundField>
                        
                      
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
   
</asp:Content>

