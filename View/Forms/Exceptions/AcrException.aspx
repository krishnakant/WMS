<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true" CodeFile="AcrException.aspx.cs" Inherits="View_Forms_Exceptions_AcrException" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div>
<fieldset>
<legend>Exceptions</legend>
<div style="overflow:auto; height:400px; width:950px;">
<table>
<asp:GridView ID="grdAcrException" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="false" EmptyDataText="No Data Found">
<Columns>
<asp:TemplateField HeaderText = "WCDOCNO">
<ItemTemplate>
<asp:TextBox ID="txtWCDOCNO" runat="server" Text ='<%# Bind("WCDOCNO") %>'  />
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="DLR_REF" >
<ItemTemplate>
<asp:TextBox ID="txtDlrRef" runat="server" Text ='<%# Bind("DLR_REF") %>' />
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="TRACTOR NO">
<ItemTemplate>
<asp:TextBox ID="txtTractorNo" runat="server" Text='<%#Bind("[TRACTOR NO]") %>'/>
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="ENGINE NO">
<ItemTemplate>
<asp:TextBox ID="txtEngineNo" runat="server" Text ='<%# Bind("[ENGINE NO]")  %>' />
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="INS DATE" >
<ItemTemplate>
<asp:TextBox ID="txtINSDATE" runat="server" Text='<%# Bind("[INS DATE]") %>' />
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="DEF DATE" >
<ItemTemplate>
<asp:TextBox ID="txtDEFDATE" runat="server" Text ='<%# Bind("[DEF DATE]") %>'/>
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="REP DATE" >
<ItemTemplate>
<asp:TextBox ID="txtREPDATE" runat="server" Text ='<%# Bind("[REP DATE]")%>' />
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="HMR" >
<ItemTemplate>
<asp:TextBox ID="txtHMR" runat="server" Text='<%# Bind("HMR") %>' />
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="DEALER CODE" >
<ItemTemplate>
<asp:TextBox ID="txtDealerCode" runat="server" Text='<%# Bind("[DLR CO]") %>'  />
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="DEALER NAME" >
<ItemTemplate>
<asp:TextBox ID="txtDealerName" runat="server" Text ='<% # Bind("[DEALER NAME]")%>' />
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="REG" >
<ItemTemplate>
<asp:TextBox ID="txtREG" runat="server" Text='<%# Bind("REG") %>'  />
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="CR DATE" >
<ItemTemplate>
<asp:TextBox ID="txtCRDATE" runat="server" Text='<%# Bind("[CR DATE]") %>'/>
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="ITEM CODE" >
<ItemTemplate>
<asp:TextBox ID="txtItemCode" Text='<%# Bind("[ITEM CODE]") %>' runat="server"/>
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="DESCRIPTION" >
<ItemTemplate>
<asp:TextBox ID="txtDescription" runat="server" Text='<%# Bind("DESCRIPTION")%>'  />
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="QUANTITY" >
<ItemTemplate>
<asp:TextBox ID="txtQuantity" runat="server" Text='<%# Bind("QTY")%>'/>
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="COST" >
<ItemTemplate>
<asp:TextBox ID="txtCost" runat="server" Text='<%# Bind("COST")%>'/>
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="DEF" >
<ItemTemplate>
<asp:TextBox ID="txtDEF" runat="server" Text='<%# Bind("DEF")%>'/>
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="NDP" >
<ItemTemplate>
<asp:TextBox ID="txtNDP" runat="server" Text='<%# Bind("NDP")%>'/>
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="VALUE" >
<ItemTemplate>
<asp:TextBox ID="txtVALUE" runat="server" Text='<%# Bind("VALUE")%>'/>
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="CVOICE" >
<ItemTemplate>
<asp:TextBox ID="txtCVOICE" runat="server" Text='<%# Bind("CVOICE")%>'/>
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="OUTLV" >
<ItemTemplate>
<asp:TextBox ID="txtOUTLV" runat="server" Text='<%# Bind("OUTLV")%>'/>
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="DT" >
<ItemTemplate>
<asp:TextBox ID="txtDT" runat="server" Text='<%# Bind("DT")%>'/>
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="CULPRIT CODE" >
<ItemTemplate>
<asp:TextBox ID="txtCulCode" runat="server" Text='<%# Bind("[CUL CODE]")%>'/>
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="Blank" >
<ItemTemplate>
<asp:TextBox ID="txtBlank" runat="server" Text='<%# Bind("[F24]")%>'/>
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="CR-AMOUNT" >
<ItemTemplate>
<asp:TextBox ID="txtCRAMT" runat="server" Text='<%# Bind("[CR-AMOUNT]")%>'/>
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="AUTH AMT" >
<ItemTemplate>
<asp:TextBox ID="txtAUTHAMT" runat="server" Text='<%# Bind("Auth_Amt")%>'/>
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="DIFF" >
<ItemTemplate>
<asp:TextBox ID="txtDIFF" runat="server" Text='<%# Bind("Diff")%>'/>
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="FromDate" >
<ItemTemplate>
<asp:Label ID="lblFromDate" runat="server" Text='<%# Bind("FromDate")%>'/>
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="ToDate" >
<ItemTemplate>
<asp:Label ID="lblToDate" runat="server" Text='<%# Bind("ToDate")%>'/>
</ItemTemplate>
</asp:TemplateField>

<%--<asp:TemplateField HeaderText="Cause" >
<ItemTemplate>
<asp:Label ID="lblCause" runat="server" Text='<%# Bind("Cause")%>'/>
</ItemTemplate>
</asp:TemplateField>--%>
<%--<asp:TemplateField HeaderText="Is Approved">
<ItemTemplate>
<asp:CheckBox ID="chkIsApproved" runat="server" Checked='<%# Bind("IsApproved") %>' />
</ItemTemplate>
</asp:TemplateField>--%>
<%--<asp:TemplateField HeaderText="Item Exception">
<ItemTemplate>
<asp:CheckBox ID="chkIsItemEx" runat="server" Checked='<%# Bind("IsItemEx") %>' />
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="Defect Exception">
<ItemTemplate>
<asp:CheckBox ID="chkIsDefectEx" runat="server" Checked='<%# Bind("IsDefectEx") %>' />
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="CVoice Exception">
<ItemTemplate>
<asp:CheckBox ID="chkIsCVoiceEx" runat="server" Checked='<%# Bind("IsCVoiceEx") %>' />
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="Culprit Exception">
<ItemTemplate>
<asp:CheckBox ID="chkIsCulpritEx" runat="server" Checked='<%# Bind("IsCulpritEx") %>' />
</ItemTemplate>
</asp:TemplateField>


<asp:TemplateField HeaderText="Model Exception">
<ItemTemplate>
<asp:CheckBox ID="chkIsModelEx" runat="server" Checked='<%# Bind("IsModelEx") %>' />
</ItemTemplate>
</asp:TemplateField>
--%>
<asp:TemplateField HeaderText="Discard">
<ItemTemplate>
<asp:CheckBox ID="chkDiscard" runat="server" />
<asp:HiddenField ID="hdnID" runat="server" Value = '<%# Bind("ID") %>' />
<asp:HiddenField ID="hdnEngine" runat="server" Value = '<%# Bind("Engine") %>' />
<asp:HiddenField ID="hdnIsEngine" runat="server" Value = '<%# Bind("IsEngine") %>' />
</ItemTemplate>
</asp:TemplateField>

</Columns>
    <FooterStyle BackColor="White" ForeColor="#000066" />
    <RowStyle ForeColor="#000066" />
    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
</asp:GridView>
</table>
<asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" />
</div>
</fieldset>
</div>
</asp:Content>

