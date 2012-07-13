<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExcelGraph.aspx.cs" Inherits="View_Forms_Graph_ExcelGraph" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript">
 function getGraphs(selval)
    {
        var imagename = '';
        
        if(selval == 0)
        {
            imagename = 'image004.gif';
               
        }
        if(selval == 1)
        {
            imagename = 'image003.gif';
        }
        if(selval == 2)
        {
            imagename = 'image002.gif';
        }
        if(selval == 3)
        {
            imagename = 'image001.gif';
        }
         
            document.getElementById('imgGraph').src='/WMS/UploadFile/Graphs/Summary_files/'+imagename;
           
                      
    }    

    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div Width="100%" >
    <asp:RadioButtonList ID="rdoChartType" runat="server" RepeatDirection="Horizontal">
            <asp:ListItem Selected="True" Value="0" onclick="getGraphs(this.value);">Bar Chart</asp:ListItem>
            <asp:ListItem Value="1" onclick="getGraphs(this.value);">Pie Chart</asp:ListItem>
            <asp:ListItem Value="2" onclick="getGraphs(this.value);">Line Chart</asp:ListItem>
            <asp:ListItem Value="3" onclick="getGraphs(this.value);">Doughnut Chart</asp:ListItem>
        </asp:RadioButtonList>
        <table>
        <tr>
        <td>
        <asp:Image ID="imgGraph" runat="server" ImageUrl="~/UploadFile/Graphs/Summary_files/image004.gif" Height="70%" Width="100%" />
        </td>
        
        </tr>
        
        </table>
    </div>
    </form>
</body>
</html>
