<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true"
    CodeFile="DealerException.aspx.cs" Inherits="View_Forms_Exceptions_DealerException" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script type="text/javascript">
function UpdatingInfo(btnid)
     {
     
        var myStringtest = btnid;
        var mySplitResulttest = myStringtest.split("_");
        var Code =  mySplitResulttest[0]+'_'+mySplitResulttest[1]+'_'+mySplitResulttest[2]+'_'+mySplitResulttest[3];
        Code=Code+'_hdnCode';  
        Code=document.getElementById(Code).value;
        var Path='/WMS/View/Forms/Master/Dealer.aspx?Code='+Code+'&source=Exception';
        window.location.href=Path;
       
     }
     
</script>
    <fieldset class="sectionBorder">
        <legend>Dealer Exceptions</legend>
        <asp:GridView AutoGenerateColumns="false" Width="100%"
            ID="grdDealerExceptions" OnDataBound="eventhandlerSerialNo"  runat="server" EmptyDataText="No Records Found">
            <Columns>
              <asp:BoundField HeaderText="#" ReadOnly="True">
                <ItemStyle HorizontalAlign="center" />
            </asp:BoundField>
            <asp:BoundField DataField="Dealer_Code" HeaderText="Dealer Code" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
              <asp:TemplateField HeaderText="Update" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                    <asp:HiddenField ID="hdnCode" runat="server" Value='<%# Bind("dealer_code") %>' />
                        <img alt="" src='~/images/icon-edit.gif' height='15' width='16' style="cursor: pointer;"
                            id="imageButton" title='Update' onclick='UpdatingInfo(this.id)' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
               <FooterStyle BackColor="White" ForeColor="#000066" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
    </fieldset>
</asp:Content>
