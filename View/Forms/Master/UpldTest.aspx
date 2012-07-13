<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true" CodeFile="UpldTest.aspx.cs" Inherits="View_Forms_Master_UpldTest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:FileUpload ID="UploadFile" runat="server" /><br />
    <asp:Button ID="btnUpld" runat="server" Text="Upload" OnClick="btnUpld_Click" />
    <asp:Label ID="lblTest" runat="server"></asp:Label>
</asp:Content>

