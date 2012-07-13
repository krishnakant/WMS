<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true"
    CodeFile="ModelExceptionBulk.aspx.cs" Inherits="View_Forms_Exceptions_ModelExceptionBulk" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script type="text/javascript">
function UpdatingInfo(btnid)
     {
     
        var myStringtest = btnid;
        var mySplitResulttest = myStringtest.split("_");
        var Code =  mySplitResulttest[0]+'_'+mySplitResulttest[1]+'_'+mySplitResulttest[2]+'_'+mySplitResulttest[3];
        Code=Code+'_hdnCode';  
        Code=document.getElementById(Code).value;
        var Path='/WMS/View/Forms/Master/ModelNew.aspx?Code='+Code+'&source=Exception';
        window.location.href=Path;
       
     }
     
</script>
    <fieldset class="sectionBorder">
        <legend>Model Exceptions</legend>
        <asp:GridView AutoGenerateColumns="false" Width="100%" OnDataBound="eventhandlerSerialNo" ID="grdMaterial" runat="server">
            <Columns>
                <asp:BoundField HeaderText="#" ItemStyle-HorizontalAlign="Center" ReadOnly="True" />
                <asp:BoundField DataField="Material" HeaderText="Material" ReadOnly="True" />
                <asp:TemplateField HeaderText="Update" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                    <asp:HiddenField ID="hdnCode" runat="server" Value='<%# Bind("Material") %>' />
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
    <asp:Literal ID="literal1" runat="server"></asp:Literal>
</asp:Content>
