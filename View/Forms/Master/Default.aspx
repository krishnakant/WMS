<%@ Page Language="C#" MasterPageFile="~/master/MasterPage.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="View_Forms_Master_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<div ><center><img alt="" src="<%=strProjectName%>/images/mid-7-08-06.png" alt="" height="396px" width="100%" border="0"  /></center></div>--%>
    <div>

        <script type="text/javascript">
	function fnCallOnLoad()
	{
		fnSetPageTitle('Welcome');
	}
	fnAttachEvent(window,'load',fnCallOnLoad,false);
        </script>

        <table cellspacing="0" cellpadding="0" width="100%" class="cssTable">
            <tr>
                <td class="cssPageText">
                    <fieldset class="sectionBorder">
                        <legend></legend>
                        <table cellspacing="0" cellpadding="0" width="100%" class="cssTable">
                            <tr>
                                <td align="center">
                                <br />
                                    <b><font color="red">
                                        <h3>
                                            Welcome to</h3>
                                    </font></b>
                                    <br />
                                    
                                    <b><font color="red">
                                        <h3>
                                            Eicher Tractors</h3>
                                    </font></b>
                                      <br />
                                    
                                    <b><font color="red">
                                        <h3>
                                            Warranty Management System
                                        </h3>
                                    </font></b>
                                    <br />
                                    <br />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
