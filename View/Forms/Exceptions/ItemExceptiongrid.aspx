<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true" CodeFile="ItemExceptiongrid.aspx.cs" Inherits="View_Forms_Exceptions_ItemException"%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">


function getIndex(id)
{
    //alert(id);
    var myString = id;
        var mySplitResult = myString.split("_");
        var ID = mySplitResult[3].split('ctl') ;
        var SerialRowID=ID[1]-02;
       // alert(SerialRowID);
        
         var str = mySplitResult[0]+'_'+mySplitResult[1];
                       
         var mylbl = str+'_hdnIndex';
         document.getElementById(mylbl).value=SerialRowID;
        
         
         return true;
}         


function SetControl(index,val) 
{

            var ctrlID = val.split('_');
            var ModelIDdropdown='';
            var Modeltextbox='';
            ModelIDdropdown = 'ctl00_ContentPlaceHolder1_grdItemException_'+ctrlID[3]+'_drpItem';
            Modeltextbox = 'ctl00_ContentPlaceHolder1_grdItemException_'+ctrlID[3]+'_txtItem';
        
            if(index==0) 
            {
            document.getElementById(ModelIDdropdown).style.display='';
            document.getElementById(Modeltextbox).style.display='none';
            }
            else if(index==1)
            {
            document.getElementById(ModelIDdropdown).style.display='none';
            document.getElementById(Modeltextbox).style.display='';
            }
}
function setMessageText(ID,Text)
{
   document.getElementById(ID).innerHTML =Text ;
   setTimeout("setMessageText('"+ID+"','')",3000);
} 

</script>
<fieldset>
<legend>Item Exceptions</legend>
<div style="overflow:auto; height:325px; width:950px;">
<asp:GridView ID="grdItemException" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="false" EmptyDataText="No Data Found">
<Columns>
<asp:TemplateField HeaderText="Code" >
<ItemTemplate>
<asp:Label ID="lblCode" runat="server" Text='<%# Bind("Code") %>'></asp:Label>
<asp:HiddenField ID="hdnID" runat="server" Value='<%# Bind("ID") %>' />
</ItemTemplate>
</asp:TemplateField>


<asp:TemplateField HeaderText="Assign/Add">
<ItemTemplate>
<asp:RadioButtonList ID="rdoMode" runat="server" RepeatDirection="Horizontal" >
<asp:ListItem Value="0" onclick="javaScript:SetControl(this.value,this.id);">Assign Existing</asp:ListItem>
<asp:ListItem Value="1" onclick="javaScript:SetControl(this.value,this.id);" Selected="True">Add New</asp:ListItem>
</asp:RadioButtonList>
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="Item Code">
<ItemTemplate>
<asp:DropDownList ID="drpItem" runat="server" AppendDataBoundItems="true" DataTextField="ItemGroupName" DataValueField="ItemCodeGroupID" DataSourceID="SqlItem"  style="display:none;">
<asp:ListItem Value="0" Selected="True">Select</asp:ListItem>
</asp:DropDownList>
<asp:TextBox ID="txtItem" runat="server"></asp:TextBox>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Update">
<ItemTemplate>
<asp:Button ID="btnUpdate" runat="server" Text="Update" OnClientClick="return getIndex(this.id);" OnClick="UpdateItem" />
</ItemTemplate>
</asp:TemplateField>
   
    
    </Columns>
    <FooterStyle BackColor="White" ForeColor="#000066" />
    <RowStyle ForeColor="#000066" />
    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />

</asp:GridView>

    <asp:SqlDataSource ID="SqlItem" runat="server" ConnectionString="<%$ ConnectionStrings:connectionString %>"
        SelectCommand="SELECT Distinct  [ItemCodeGroupID],[ItemGroupName] FROM [ItemGroup]"></asp:SqlDataSource>
</div>
<asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
</fieldset>
<asp:HiddenField ID="hdnIndex" runat="server" />
<asp:Literal ID="Literal1" runat="server"></asp:Literal>
</asp:Content>

