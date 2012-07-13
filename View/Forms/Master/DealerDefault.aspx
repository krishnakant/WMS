<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true"
    CodeFile="DealerDefault.aspx.cs" Inherits="View_Forms_Master_DealerDefault" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
    
    
    
 function UpdatingInfo(btnid)
     {
     
        var myStringtest = btnid;
        var mySplitResulttest = myStringtest.split("_");
        var DealerId =  mySplitResulttest[0]+'_'+mySplitResulttest[1]+'_'+mySplitResulttest[2]+'_'+mySplitResulttest[3];
        DealerId=DealerId+'_hdnDealerID';  
         DealerId=document.getElementById(DealerId).value;
        var RegionID=document.getElementById('ctl00_ContentPlaceHolder1_drpRegion').value;
       var searchCode=document.getElementById('ctl00_ContentPlaceHolder1_txtsearch').value;
        if(searchCode=='')
        {
         searchCode=-1;
        }
        var Path='<%=strProjectName%>/View/Forms/Master/Dealer.aspx?DealerId='+DealerId+'&RegionID='+RegionID+'&searchCode='+searchCode;
        window.location.href=Path;
       
     }
     
      function DeletingInfo(btnid)
    {
       
 
        var check=confirm('Are you sure you want to delete this record?');
        if(check)
        {
            var myStringtest = btnid;
            var mySplitResulttest = myStringtest.split("_");
            var DealerID =  mySplitResulttest[0]+'_'+mySplitResulttest[1]+'_'+mySplitResulttest[2]+'_'+mySplitResulttest[3];
            DealerID=DealerID+'_hdnDealerID'; 
            DealerID=document.getElementById(DealerID).value;
            document.getElementById('ctl00_ContentPlaceHolder1_hdnDelrID').value=DealerID;
        }
       
        return check
   
   }
   
    
   
   
    </script>
        <fieldset class="sectionBorder"><legend>Dealer</legend>
    <div  class="cssButtonPanel">
        <asp:Button ID="btnAdd" Text="Add" ToolTip="Add" OnClick="btnAdd_Click" runat="server" />
         
    </div>
    <table border="0" cellpadding="0" cellspacing="0" width="100%"><tr>
      <td class="cssLabel">
                       Region:
                    </td>
                    <td>
                        <asp:DropDownList ID="drpRegion" runat="server"></asp:DropDownList>
                    </td>
                    <td class="cssLabel">search:</td>
       <td><asp:TextBox ID="txtsearch" ToolTip="Dealer Code" runat="server"></asp:TextBox></td>
    </tr></table>
   <div class="cssButtonPanel"><asp:Button ID="btnShow" Text="Show" ToolTip="Show" OnClick="btnShow_Click" runat="server" />
    <asp:Button ID="btnExport" Visible="false" 
                runat="server" CssClass="cssButton" Text="Excel Export" ToolTip="Excel Export"
                OnClick="Button1_Click"></asp:Button>
   </div>
    <br />
         <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
    <div style="margin-left: 1%; width: 100%;" id="divGrid">
        <asp:GridView ID="GridView1" Width="100%" runat="server" OnRowCreated="gridView_RowCreated"  OnRowDeleting="Role_RowDeleted" OnDataBound="eventhandlerSerialNo"
            AutoGenerateColumns="false" EmptyDataText="No Records Found">
            <Columns>
                <asp:BoundField HeaderText="#" ReadOnly="True">
                    <ItemStyle HorizontalAlign="center" />
                </asp:BoundField>
                <asp:BoundField DataField="Dealer" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="Dealer Name">
                   </asp:BoundField>
                <asp:BoundField DataField="Code" HeaderText="Code" ItemStyle-HorizontalAlign="center" HeaderStyle-HorizontalAlign="center">
                    <ItemStyle HorizontalAlign="center" />
                </asp:BoundField>
                <asp:BoundField DataField="Region" HeaderText="Region" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                   </asp:BoundField>
                 <asp:BoundField DataField="City" HeaderText="Location" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                    </asp:BoundField>
                       <asp:BoundField DataField="InstallerName" HeaderText="Installer Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                    </asp:BoundField>
                    
                <asp:TemplateField HeaderText="Is Active">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:CheckBox ID="chkIsActive" runat="server" Enabled="false" Checked='<%#Bind("IsActive") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="Is Operating Dealer">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:CheckBox ID="chkIsOperating" runat="server" Enabled="false" Checked='<%#Bind("IsOperatingDealer") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Edit">
                    <ItemStyle HorizontalAlign="Center" />
                  
                    <ItemTemplate>
                        <asp:HiddenField ID="hdnDealerID" runat="server" Value='<%#Bind("ID") %>' />
                        <img alt="" src='~/images/icon-edit.gif' height='15' width='16' style="cursor: pointer;"
                            id="imageButton" title='Update' onclick='UpdatingInfo(this.id)' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkbtnDelete" runat="server" OnClientClick="return DeletingInfo(this.id)"
                            CommandName="Delete" ToolTip="Delete">
                            <asp:Image ID="Image1" ImageUrl="~/images/icon-delete.gif" AlternateText="Delete"
                                BorderStyle="None" runat="server" ToolTip="Delete" /></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
    </div>
    </fieldset>
     <asp:Literal ID="Literal1" runat="server"></asp:Literal>
      <asp:HiddenField ID="hdnExport" runat="server" />
    <asp:HiddenField ID="hdnDelrID" runat="server" />
</asp:Content>
