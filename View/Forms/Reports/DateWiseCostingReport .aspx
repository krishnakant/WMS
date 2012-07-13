<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true" CodeFile="DateWiseCostingReport .aspx.cs" Inherits="View_Forms_Reports_DateWiseCostingReport_"  %>

<%@ Register TagPrefix="ew" Namespace="eWorld.UI" Assembly="eWorld.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<table>
<tr>
                    <td class="cssLabel">
                     <asp:Label ID="lblModelCode" runat="server"  Text="Model" ></asp:Label>
                    </td>
                    <td>
                        
                        <asp:DropDownList ID="drpModelCode" runat="server" ToolTip="Model">
                        </asp:DropDownList>
                       
                    </td>
                    
                    <td class="cssLabel"><asp:Label ID="lblFromdate" runat="server" Text="From Date:" ToolTip="From Date"></asp:Label>
                      
                    </td>
                    <td>
                        <ew:CalendarPopup ID="CalstartDate" runat="server" CalendarWidth="200" Culture="English (United Kingdom)"
                            Font-Size="9px" Height="20px" ImageUrl="/prototype/images/icon-calendar.gif"
                            Nullable="true" NullableLabelText='' ShowGoToToday="true" PadSingleDigits="True"
                            ToolTip="Select Date" Width="80px">
                            <SelectedDateStyle BackColor="LightSteelBlue" />
                        </ew:CalendarPopup>
                    </td>
                    
                    
                    <td class="cssLabel"><asp:Label ID="lblToDate" runat="server" Text="To Date:" ToolTip="To Date"></asp:Label>
                      
                    </td>
                    <td>
                        <ew:CalendarPopup ID="CalendarPopup1" runat="server" CalendarWidth="200" Culture="English (United Kingdom)"
                            Font-Size="9px" Height="20px" ImageUrl="/prototype/images/icon-calendar.gif"
                            Nullable="true" NullableLabelText='' ShowGoToToday="true" PadSingleDigits="True"
                            ToolTip="Select Date" Width="80px">
                            <SelectedDateStyle BackColor="LightSteelBlue" />
                        </ew:CalendarPopup>
                    </td>
                      
                       <td class="cssLabel">
                        <asp:Label ID="lblTyepe" runat="server" Text="Type:"></asp:Label></td>
    <td>
    
                        <asp:DropDownList ID="drpType" runat="server">
                            <asp:ListItem Selected="true" Value="1">Select</asp:ListItem>
                            <asp:ListItem Value="2">Monthly</asp:ListItem>
                            <asp:ListItem Value="3">yearly</asp:ListItem>
                            <asp:ListItem Value="4">Quartely</asp:ListItem>
                            
                            
                        </asp:DropDownList>
                    
                
                    
                </tr>
                
                
</table>
</asp:Content>

