<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true"
    CodeFile="ReportLink.aspx.cs" Inherits="View_Forms_Reports_ReportLink"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset class="sectionBorder">
        <legend>Other Reports</legend>
        <table  cellpadding="3" cellspacing="0">
            <tr>
                <td class="Label">
                    <img src="/wMS/Images/buttlem.gif" style="cursor: pointer;" width="12" height="12"
                        hspace="0" align="absBottom" border="0" /><a title="Model wise acr detail" style="font-size: 14px;
                            font-weight: bold; font-family: Trebuchet MS; color: #2889BC;" href="/WMS/View/Forms/Reports/ModelWiseACRDetail.aspx">Model wise acr detail
                           </a></td>
            </tr>
            <tr>
                <td class="Label">
                    <img src="/wMS/Images/buttlem.gif" style="cursor: pointer;" width="12" height="12"
                        hspace="0" align="absBottom" border="0" /><a title="Culprit Code wise acr detail" style="font-size: 14px;
                            font-weight: bold; font-family: Trebuchet MS; color: #2889BC;" href="/WMS/View/Forms/Reports/CulpritCodeWiseACRDetail.aspx">Culprit Code wise acr detail
                            </a>
                </td>
            </tr>
            <tr>
                <td class="Label">
                    <img src="/wMS/Images/buttlem.gif" style="cursor: pointer;" width="12" height="12"
                        hspace="0" align="absBottom" border="0" /><a title="Customer voice Code wise acr detail" style="font-size: 14px;
                            font-weight: bold; font-family: Trebuchet MS; color: #2889BC;" href="/WMS/View/Forms/Reports/CvoiceCodeWiseACR.aspx">Customer voice Code wise acr detail
                            </a>
                </td>
            </tr>
            <tr>
                <td class="Label">
                    <img src="/wMS/Images/buttlem.gif" style="cursor: pointer;" width="12" height="12"
                        hspace="0" align="absBottom" border="0" /><a  title="Item group wise acr detail" style="font-size: 14px;
                            font-weight: bold; font-family: Trebuchet MS; color: #2889BC;" href="/WMS/View/Forms/Reports/ItemGRoupWiseACRDetail.aspx">Item group wise acr detail
                            </a>
                </td>
            </tr>
            <tr>
                <td class="Label">
                    <img src="/wMS/Images/buttlem.gif" style="cursor: pointer;" width="12" height="12"
                        hspace="0" align="absBottom" border="0" /><a title="Dealer wise acr detail" style="font-size: 14px;
                            font-weight: bold; font-family: Trebuchet MS; color: #2889BC;" href="/WMS/View/Forms/Reports/DealerWiseACR.aspx">Dealer wise acr detail
                            </a>
                </td>
            </tr>
            <tr>
                <td class="Label">
                    <img src="/wMS/Images/buttlem.gif" style="cursor: pointer;" width="12" height="12"
                        hspace="0" align="absBottom" border="0" /><a title="Defect Code wise acr detail" style="font-size: 14px;
                            font-weight: bold; font-family: Trebuchet MS; color: #2889BC;" href="/WMS/View/Forms/Reports/DefectCodeWiseACR.aspx">Defect Code wise acr detail
                           </a>
                </td>
            </tr>
            <%-- <tr>
                <td class="Label">
                    <img src="/wMS/Images/buttlem.gif" style="cursor: pointer;" width="12" height="12"
                        hspace="0" align="absBottom" border="0" /><a title="Place wise acr detail" style="font-size: 14px;
                            font-weight: bold; font-family: Trebuchet MS; color: #2889BC;" href="/WMS/View/Forms/Reports/PlaceWiseReport.aspx">Plant wise acr detail
                           </a>
                </td>
            </tr>--%>
            <tr>
                <td class="Label">
                    <img src="/wMS/Images/buttlem.gif" style="cursor: pointer;" width="12" height="12"
                        hspace="0" align="absBottom" border="0" /><a title="Chassis Number wise acr detail" style="font-size: 14px;
                            font-weight: bold; font-family: Trebuchet MS; color: #2889BC;" href="/WMS/View/Forms/Reports/ChassisWiseACRDetail.aspx">Chassis Number wise acr detail
                           </a>
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>
