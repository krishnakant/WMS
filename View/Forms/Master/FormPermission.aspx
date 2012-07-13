<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true"
    CodeFile="FormPermission.aspx.cs" Inherits="View_Forms_Master_FormPermission"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
   
  

function ClientValidateEmployee(source, arguments)
{
      if (document.getElementById('ctl00_ContentPlaceHolder1_ddlRole').value!=0)
         arguments.IsValid=true;
      else
         arguments.IsValid=false;
}



    </script>

    <fieldset class="sectionBorder">
        <legend>New permission</legend>
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="center">
                    <asp:Label ID="lblRole"  runat="server" Text="Role:"></asp:Label>
                    <asp:DropDownList ID="ddlRole"  ToolTip="Role" runat="server" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlRole_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:CustomValidator ID="CustomValidator" ErrorMessage="Select the Role" ClientValidationFunction="ClientValidateEmployee"
                        ControlToValidate="ddlRole" runat="server"></asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td> <span  style="font-size:14px;font-weight:bold;font-family:Verdana;color:Black;">Module</span> <input type="checkbox" id="chkModuleAll" onclick="javascript:SetAllCheckBoxes('ctl00_ContentPlaceHolder1_ChkModule',this.id);" />Select
                    All
                    <asp:Panel BorderWidth="2px" BorderStyle="solid" ID="PnlModule" runat="server"
                        ScrollBars="Vertical">
                        <asp:CheckBoxList ID="ChkModule" ToolTip="select Module" RepeatColumns="8" runat="server">
                        </asp:CheckBoxList>
                    </asp:Panel>
                   
                </td>
            </tr>
        </table>
        <br />
        <div class="cssButtonPanel">
            <asp:Button ID="btnShow" Text="Show Form" CausesValidation="true" OnClick="btnShow_Click"
                ToolTip="ShowForm" runat="server" /></div>
        <fieldset id="fldCSI" runat="server" visible="false" style="border-color:Lime;border:2px solid;">
            <legend  style="font-size:14px;font-weight:bold; font-family:Verdana;color:Maroon;" >CSI</legend>
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <asp:Panel BorderWidth="1px" ID="Panel1" GroupingText="Master" runat="server" ScrollBars="Vertical">
                            <asp:CheckBoxList ID="CSIChkMaster" ToolTip="select Form" RepeatColumns="5" runat="server">
                            </asp:CheckBoxList>
                        </asp:Panel>
                        <input type="checkbox" id="CSIChkMasterAll" onclick="javascript:SetAllCheckBoxes('ctl00_ContentPlaceHolder1_CSIChkMaster',this.id);" />Select
                        All
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel BorderWidth="1px" ID="Panel3" GroupingText="Data Input" runat="server"
                            ScrollBars="Vertical">
                            <asp:CheckBoxList ID="CSIChkDatainput" ToolTip="select Form" RepeatColumns="5" runat="server">
                            </asp:CheckBoxList>
                        </asp:Panel>
                        <input type="checkbox" id="CSIChkDatainputAll" onclick="javascript:SetAllCheckBoxes('ctl00_ContentPlaceHolder1_CSIChkDatainput',this.id);" />Select
                        All
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel BorderWidth="1px" ID="Panel4" GroupingText="Reports" runat="server" ScrollBars="Vertical">
                            <asp:CheckBoxList ID="CSIChkReports" ToolTip="select Form" RepeatColumns="5" runat="server">
                            </asp:CheckBoxList>
                        </asp:Panel>
                        <input type="checkbox" id="CSIChkReportsAll" onclick="javascript:SetAllCheckBoxes('ctl00_ContentPlaceHolder1_CSIChkReports',this.id);" />Select
                        All
                    </td>
                </tr>
            </table>
        </fieldset>
        <table>
        </table>
        <fieldset id="fldMIS" runat="server" visible="false"  style="border-color:Lime;border:2px solid;">
            <legend style="font-size:14px;font-weight:bold; font-family:Verdana;color:Maroon;">MIS</legend>
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <asp:Panel BorderWidth="1px" ID="Panel2" GroupingText="Master" runat="server" ScrollBars="Vertical">
                            <asp:CheckBoxList ID="MISChkMaster" ToolTip="select Form" RepeatColumns="5" runat="server">
                            </asp:CheckBoxList>
                        </asp:Panel>
                        <input type="checkbox" id="MISChkMasterAll" onclick="javascript:SetAllCheckBoxes('ctl00_ContentPlaceHolder1_MISChkMaster',this.id);" />Select
                        All
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel BorderWidth="1px" ID="Panel5" GroupingText="Data Input" runat="server"
                            ScrollBars="Vertical">
                            <asp:CheckBoxList ID="MISChkDatainput" ToolTip="select Form" RepeatColumns="5" runat="server">
                            </asp:CheckBoxList>
                        </asp:Panel>
                        <input type="checkbox" id="MISChkDatainputall" onclick="javascript:SetAllCheckBoxes('ctl00_ContentPlaceHolder1_MISChkDatainput',this.id);" />Select
                        All
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel BorderWidth="1px" ID="Panel6" GroupingText="Reports" runat="server" ScrollBars="Vertical">
                            <asp:CheckBoxList ID="MISChkReports" ToolTip="select Form" RepeatColumns="5" runat="server">
                            </asp:CheckBoxList>
                        </asp:Panel>
                        <input type="checkbox" id="MISChkReportsall" onclick="javascript:SetAllCheckBoxes('ctl00_ContentPlaceHolder1_MISChkReports',this.id);" />Select
                        All
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel BorderWidth="1px" ID="Panel7" GroupingText="ESA" runat="server" ScrollBars="Vertical">
                            <asp:CheckBoxList ID="MISChkESA" ToolTip="select Form" RepeatColumns="5" runat="server">
                            </asp:CheckBoxList>
                        </asp:Panel>
                        <input type="checkbox" id="MISChkESAall" onclick="javascript:SetAllCheckBoxes('ctl00_ContentPlaceHolder1_MISChkESA',this.id);" />Select
                        All
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel BorderWidth="1px" ID="Panel8" GroupingText="Service Camp" runat="server"
                            ScrollBars="Vertical">
                            <asp:CheckBoxList ID="MISChkServiceCamp" ToolTip="select Form" RepeatColumns="5"
                                runat="server">
                            </asp:CheckBoxList>
                        </asp:Panel>
                        <input type="checkbox" id="MISChkServiceCampall" onclick="javascript:SetAllCheckBoxes('ctl00_ContentPlaceHolder1_MISChkServiceCamp',this.id);" />Select
                        All
                    </td>
                </tr>
            </table>
        </fieldset>
        <fieldset class="sectionBorder" id="fldPQR" runat="server" visible="false" style="border-color:Lime;border:2px solid;">
            <legend style="font-size:14px;font-weight:bold; font-family:Verdana;color:Maroon;">PQR</legend>
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <asp:Panel BorderWidth="1px" ID="Panel9" GroupingText="Engine Tracking" runat="server"
                            ScrollBars="Vertical">
                            <asp:CheckBoxList ID="PQRCHKEngineTracking" ToolTip="select Form" RepeatColumns="5"
                                runat="server">
                            </asp:CheckBoxList>
                        </asp:Panel>
                        <input type="checkbox" id="PQRCHKEngineTrackingall" onclick="javascript:SetAllCheckBoxes('ctl00_ContentPlaceHolder1_PQRCHKEngineTracking',this.id);" />Select
                        All
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel BorderWidth="1px" ID="Panel10" GroupingText="PQR" runat="server" ScrollBars="Vertical">
                            <asp:CheckBoxList ID="PQRCHKPQR" ToolTip="select Form" RepeatColumns="5" runat="server">
                            </asp:CheckBoxList>
                        </asp:Panel>
                        <input type="checkbox" id="PQRCHKPQRall" onclick="javascript:SetAllCheckBoxes('ctl00_ContentPlaceHolder1_PQRCHKPQR',this.id);" />Select
                        All
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel BorderWidth="1px" ID="Panel11" GroupingText="Reports" runat="server" ScrollBars="Vertical">
                            <asp:CheckBoxList ID="PQRCHKReport" ToolTip="select Form" RepeatColumns="5" runat="server">
                            </asp:CheckBoxList>
                        </asp:Panel>
                        <input type="checkbox" id="PQRCHKReportall" onclick="javascript:SetAllCheckBoxes('ctl00_ContentPlaceHolder1_PQRCHKReport',this.id);" />Select
                        All
                    </td>
                </tr>
            </table>
        </fieldset>
        <fieldset class="sectionBorder" id="fldSPARC" runat="server" visible="false" style="border-color:Lime;border:2px solid;">
            <legend style="font-size:14px;font-weight:bold; font-family:Verdana;color:Maroon;">SPARC</legend>
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <asp:Panel BorderWidth="1px" ID="Panel12" GroupingText="Master" runat="server" ScrollBars="Vertical">
                            <asp:CheckBoxList ID="ssmChkMaster" ToolTip="select Form" RepeatColumns="5" runat="server">
                            </asp:CheckBoxList>
                        </asp:Panel>
                        <input type="checkbox" id="ssmChkMasterall" onclick="javascript:SetAllCheckBoxes('ctl00_ContentPlaceHolder1_ssmChkMasterall',this.id);" />Select
                        All
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel BorderWidth="1px" ID="Panel13" GroupingText="Data Input" runat="server"
                            ScrollBars="Vertical">
                            <asp:CheckBoxList ID="ssmChkDatainput" ToolTip="select Form" RepeatColumns="5" runat="server">
                            </asp:CheckBoxList>
                        </asp:Panel>
                        <input type="checkbox" id="ssmChkDatainputall" onclick="javascript:SetAllCheckBoxes('ctl00_ContentPlaceHolder1_ssmChkDatainput',this.id);" />Select
                        All
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel BorderWidth="1px" ID="Panel14" GroupingText="Reports" runat="server" ScrollBars="Vertical">
                            <asp:CheckBoxList ID="ssmChkReports" ToolTip="select Form" RepeatColumns="4" runat="server">
                            </asp:CheckBoxList>
                        </asp:Panel>
                        <input type="checkbox" id="ssmChkReportsall" onclick="javascript:SetAllCheckBoxes('ctl00_ContentPlaceHolder1_ssmChkReports',this.id);" />Select
                        All
                    </td>
                </tr>
            </table>
        </fieldset>
        <fieldset class="sectionBorder" id="fldWMS" runat="server" visible="false" style="border-color:Lime;border:2px solid;">
            <legend style="font-size:14px;font-weight:bold; font-family:Verdana;color:Maroon;">WMS</legend>
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <asp:Panel BorderWidth="1px" ID="Panel15" GroupingText="Master" runat="server" ScrollBars="Vertical">
                            <asp:CheckBoxList ID="WMSChkMaster" ToolTip="select Form" RepeatColumns="5" runat="server">
                            </asp:CheckBoxList>
                        </asp:Panel>
                        <input type="checkbox" id="WMSChkMasterall" onclick="javascript:SetAllCheckBoxes('ctl00_ContentPlaceHolder1_WMSChkMaster',this.id);" />Select
                        All
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel BorderWidth="1px" ID="Panel16" GroupingText="Configurator" runat="server"
                            ScrollBars="Vertical">
                            <asp:CheckBoxList ID="WMSChkConfigurator" ToolTip="select Form" RepeatColumns="5"
                                runat="server">
                            </asp:CheckBoxList>
                        </asp:Panel>
                        <input type="checkbox" id="WMSChkConfiguratorall" onclick="javascript:SetAllCheckBoxes('ctl00_ContentPlaceHolder1_WMSChkConfigurator',this.id);" />Select
                        All
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel BorderWidth="1px" ID="Panel17" GroupingText="Reports" runat="server" ScrollBars="Vertical">
                            <asp:CheckBoxList ID="WMSChkReports" ToolTip="select Form" RepeatColumns="5" runat="server">
                            </asp:CheckBoxList>
                        </asp:Panel>
                        <input type="checkbox" id="WMSChkReportsall" onclick="javascript:SetAllCheckBoxes('ctl00_ContentPlaceHolder1_WMSChkReports',this.id);" />Select
                        All
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel BorderWidth="1px" ID="Panel19" GroupingText="Graph" runat="server" ScrollBars="Vertical">
                            <asp:CheckBoxList ID="WMSChkGraph" ToolTip="select Form" RepeatColumns="3" runat="server">
                            </asp:CheckBoxList>
                        </asp:Panel>
                        <input type="checkbox" id="WMSChkGraphall" onclick="javascript:SetAllCheckBoxes('ctl00_ContentPlaceHolder1_WMSChkGraph',this.id);" />Select
                        All
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel BorderWidth="1px" ID="Panel18" GroupingText="Import" runat="server" ScrollBars="Vertical">
                            <asp:CheckBoxList ID="WMSChkImport" ToolTip="select Form" RepeatColumns="2" runat="server">
                            </asp:CheckBoxList>
                        </asp:Panel>
                        <input type="checkbox" id="WMSChkImportall" onclick="javascript:SetAllCheckBoxes('ctl00_ContentPlaceHolder1_WMSChkImport',this.id);" />Select
                        All
                    </td>
                </tr>
            </table>
        </fieldset>
        <fieldset class="sectionBorder" id="fldECM" runat="server" visible="false" style="border-color:Lime;border:2px solid;">
            <legend style="font-size:14px;font-weight:bold; font-family:Verdana;color:Maroon;">ECM</legend>
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <asp:Panel BorderWidth="1px" ID="Panel20" GroupingText="Data Input" runat="server"
                            ScrollBars="Vertical">
                            <asp:CheckBoxList ID="ECMChkDataInput" ToolTip="select Form" RepeatColumns="5" runat="server">
                            </asp:CheckBoxList>
                        </asp:Panel>
                        <input type="checkbox" id="ECMChkDataInputall" onclick="javascript:SetAllCheckBoxes('ctl00_ContentPlaceHolder1_ECMChkDataInput',this.id);" />Select
                        All
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel BorderWidth="1px" ID="Panel22" GroupingText="Reports" runat="server" ScrollBars="Vertical">
                            <asp:CheckBoxList ID="ECMChkReports" ToolTip="select Form" RepeatColumns="4" runat="server">
                            </asp:CheckBoxList>
                        </asp:Panel>
                        <input type="checkbox" id="ECMChkReportsall" onclick="javascript:SetAllCheckBoxes('ctl00_ContentPlaceHolder1_ECMChkReports',this.id);" />Select
                        All
                    </td>
                </tr>
            </table>
        </fieldset>
        <fieldset class="sectionBorder" id="fldPDI" runat="server" visible="false" style="border-color:Lime;border:2px solid;">
            <legend style="font-size:14px;font-weight:bold; font-family:Verdana;color:Maroon;">PDI</legend>
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <asp:Panel BorderWidth="1px" ID="Panel21" GroupingText="Master" runat="server" ScrollBars="Vertical">
                            <asp:CheckBoxList ID="PDIChkMaster" ToolTip="select Form" RepeatColumns="5" runat="server">
                            </asp:CheckBoxList>
                        </asp:Panel>
                        <input type="checkbox" id="PDIChkMasterall" onclick="javascript:SetAllCheckBoxes('ctl00_ContentPlaceHolder1_PDIChkMaster',this.id);" />Select
                        All
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel BorderWidth="1px" ID="Panel23" GroupingText="Data Input" runat="server"
                            ScrollBars="Vertical">
                            <asp:CheckBoxList ID="PDIChkDataInput" ToolTip="select Form" RepeatColumns="5" runat="server">
                            </asp:CheckBoxList>
                        </asp:Panel>
                        <input type="checkbox" id="PDIChkDataInputall" onclick="javascript:SetAllCheckBoxes('ctl00_ContentPlaceHolder1_PDIChkDataInput',this.id);" />Select
                        All
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel BorderWidth="1px" ID="Panel24" GroupingText="Reports" runat="server" ScrollBars="Vertical">
                            <asp:CheckBoxList ID="PDIChkReports" ToolTip="select Form" RepeatColumns="4" runat="server">
                            </asp:CheckBoxList>
                        </asp:Panel>
                        <input type="checkbox" id="PDIChkReportsall" onclick="javascript:SetAllCheckBoxes('ctl00_ContentPlaceHolder1_PDIChkReports',this.id);" />Select
                        All
                    </td>
                </tr>
            </table>
        </fieldset>
        <fieldset class="sectionBorder" id="fldSHQ" runat="server" visible="false" style="border-color:Lime;border:2px solid;">
            <legend style="font-size:14px;font-weight:bold; font-family:Verdana;color:Maroon;">SHQ</legend>
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <asp:Panel BorderWidth="1px" ID="Panel25" GroupingText="Master" runat="server" ScrollBars="Vertical">
                            <asp:CheckBoxList ID="SHQChkMaster" ToolTip="select Form" RepeatColumns="5" runat="server">
                            </asp:CheckBoxList>
                        </asp:Panel>
                        <input type="checkbox" id="SHQChkMasterall" onclick="javascript:SetAllCheckBoxes('ctl00_ContentPlaceHolder1_SHQChkMaster',this.id);" />Select
                        All
                    </td>
                </tr>
            </table>
        </fieldset>
        <%-- <table>
            <tr>
                <td>
                    <h6>
                        <asp:Label ID="lblForm" runat="server" Text="Form"></asp:Label></h6>
                    <asp:Panel BorderWidth="1px" ID="PnlForm" runat="server" BorderColor="#00678e" ScrollBars="Vertical">
                        <asp:CheckBoxList ID="chkForm" ToolTip="select Form"   RepeatColumns="5"
                            runat="server">
                        </asp:CheckBoxList>
                    </asp:Panel>
                    <input type="checkbox" id="chkFormALL" onclick="javascript:SetAllCheckBoxes('ctl00_ContentPlaceHolder1_chkForm',this.id);" />Select
                    All
                </td>
            </tr>
           </table>--%>
        <asp:Label ID="lblMessage" runat="server" ForeColor="red"></asp:Label>
    </fieldset>
    <div class="cssButtonPanel">
        <asp:Button ID="btnSave" Visible="false" CausesValidation="true" Text="Save" ToolTip="Save"
            runat="server" OnClick="btnSave_Click" />
        <span style="margin-left: 1%;"></span>
        <asp:Button ID="btncencle" Visible="false" CausesValidation="false" Text="cancel"
            ToolTip="cancel" runat="server" />
    </div>
    <asp:Literal ID="literal1" runat="server"></asp:Literal>
</asp:Content>
