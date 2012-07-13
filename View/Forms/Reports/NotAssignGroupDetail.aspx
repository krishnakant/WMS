<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true"
    CodeFile="NotAssignGroupDetail.aspx.cs" Inherits="View_Forms_Reports_NotAssignGroupDetail"
    Title="WMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
    function GotoAssign(FormName)
    {
              
        var url = '/WMS/View/Forms/Configurator/'+FormName;
        window.location.href=url;
        return false;
                
    }

    </script>

    <fieldset class="sectionBorder">
        <legend>Not Assign Code Detail</legend>
        <div style="width: 97%;margin-top:1%;margin-left:1%;">
            <asp:GridView ID="grdNotAssignCode" runat="server" OnDataBound="eventhandlerSerialNo" BackColor="White" BorderColor="#CCCCCC"
                BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False"
                EmptyDataText="No Data Found">
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                <Columns>
                  <asp:BoundField ItemStyle-HorizontalAlign="Center" HeaderText="#" />
                    <asp:TemplateField HeaderText="No. of Product Code" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkException" runat="server" Text='<%# Bind("Model") %>' OnClientClick="return GotoAssign('ProductGroupMapping.aspx');"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                     <asp:TemplateField HeaderText="No. of Culprit Code" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkException" runat="server" Text='<%# Bind("Culprit") %>' OnClientClick="return GotoAssign('CulpritConfigurator.aspx');"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText=" No. of Defect Code" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkException" runat="server" Text='<%# Bind("Defect") %>' OnClientClick="return GotoAssign('DefectConfigurator.aspx');"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText=" No. of Customer Voice Code" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkException" runat="server" Text='<%# Bind("CustomerVoice") %>' OnClientClick="return GotoAssign('CVoiceConfigurator.aspx');"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="No. of Item Code" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkException" runat="server" Text='<%# Bind("Item") %>' OnClientClick="return GotoAssign('ItemConfigurator.aspx');"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                  
                </Columns>
            </asp:GridView>
        </div>
         <br />
          <br />
    </fieldset>
    <br />
     <br />
</asp:Content>
