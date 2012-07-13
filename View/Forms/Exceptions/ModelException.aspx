<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true"
    CodeFile="ModelException.aspx.cs" Inherits="View_Forms_Exceptions_ModelException" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
        
         var baseid = mySplitResult[0]+'_'+mySplitResult[1]+'_'+mySplitResult[2]+'_'+mySplitResult[3];
         var lblid = baseid+'_lblMessage';
         var selectedrdo = document.getElementById('ctl00_ContentPlaceHolder1_hdnrdoSelected').value;
         if(selectedrdo=='')
         {
         selectedrdo=1;
         }
         var modelid='';
         var modelval='';
         
         if(selectedrdo==0)
         {
         modelid = baseid+'_drpModel';
         modelval = document.getElementById(modelid).value;
         if(modelval==0)
         {
         document.getElementById(lblid).innerHTML = '*';
         return false;
         }
         else
         {
         document.getElementById(lblid).innerHTML = '';
         return true;
         }
         }
         else
         {
         modelid = baseid+'_txtModel';
         modelval = document.getElementById(modelid).value;
         if(modelval=='')
         {
         document.getElementById(lblid).innerHTML = '*';
         return false;
         }
         else
         {
         document.getElementById(lblid).innerHTML = '';
         return true;
         }
         
         }
         
       return false;
         //return true;
}         


function SetControl(index,val) 
{

            var ctrlID = val.split('_');
            var ModelIDdropdown='';
            var Modeltextbox='';
            ModelIDdropdown = 'ctl00_ContentPlaceHolder1_grdModelException_'+ctrlID[3]+'_drpModel';
            Modeltextbox = 'ctl00_ContentPlaceHolder1_grdModelException_'+ctrlID[3]+'_txtModel';
        document.getElementById('ctl00_ContentPlaceHolder1_hdnrdoSelected').value=index;
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

    <fieldset class="sectionBorder">
        <legend>Model Exceptions</legend>
        <div style="overflow: auto; height: 335px; width: 950px;">
            <asp:GridView ID="grdModelException" OnRowCreated="gridView_RowCreated" runat="server" BackColor="White" BorderColor="#CCCCCC"
                BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="false"
                EmptyDataText="No Data Found">
                <Columns>
                    <asp:TemplateField HeaderText="Code">
                        <ItemTemplate>
                            <asp:Label ID="lblCode" runat="server" Text='<%# Bind("Code") %>'></asp:Label>
                        
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Assign/Add">
                        <ItemTemplate>
                            <asp:RadioButtonList ID="rdoMode" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0" onclick="javaScript:SetControl(this.value,this.id);">Assign Existing</asp:ListItem>
                                <asp:ListItem Value="1" onclick="javaScript:SetControl(this.value,this.id);" Selected="True">Add New</asp:ListItem>
                            </asp:RadioButtonList>
                        </ItemTemplate>
                         <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Model Code">
                        <ItemTemplate>
                            <asp:DropDownList ID="drpModel" runat="server" AppendDataBoundItems="true" DataTextField="ModelGroupName"
                                DataValueField="GroupID" DataSourceID="SqlModel" Style="display: none;">
                                <asp:ListItem Value="0" Selected="True">Select</asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox ID="txtModel" runat="server"></asp:TextBox>
                            <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                        </ItemTemplate>
                         <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Update">
                        <ItemTemplate>
                            <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClientClick="return getIndex(this.id);"
                                OnClick="UpdateModel" />
                        </ItemTemplate>
                         <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
            <asp:SqlDataSource ID="SqlModel" runat="server" ConnectionString="<%$ ConnectionStrings:connectionString %>"
                SelectCommand="SELECT Distinct  [GroupID],[ModelGroupName] FROM [ModelGroupName]">
            </asp:SqlDataSource>
        </div>
    </fieldset>
    <asp:HiddenField ID="hdnIndex" runat="server" />
    <asp:HiddenField ID="hdnrdoSelected" runat="server" />
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
</asp:Content>
