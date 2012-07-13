<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true" CodeFile="MissingTractorNo.aspx.cs" Inherits="View_Forms_Exceptions_MissingTractorNo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <fieldset class="sectionBorder">
        <legend>Missing Tractor No.</legend>
        <h2 class="cssLabel">No Production found for following Tractor Nos. Please import Production for following Tractor Nos.</h2>
         <asp:GridView AutoGenerateColumns="false" Width="100%" OnDataBound="eventhandlerSerialNo" ID="grdMissingTractor" runat="server">
            <Columns>
                <asp:BoundField HeaderText="#" ItemStyle-HorizontalAlign="Center" ReadOnly="True" />
                <asp:BoundField DataField="Tractor_No" HeaderText="Tractor No" ReadOnly="True" />                
            </Columns>
             <FooterStyle BackColor="White" ForeColor="#000066" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
        </fieldset>
</asp:Content>

