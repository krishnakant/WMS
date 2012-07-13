using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

public partial class View_Forms_Reports_DealerWiseACR : System.Web.UI.Page
{
    private GridViewHelper helper;
    private List<int> mQuantities = new List<int>();
    Utility objUtility = new Utility();
    QueryConroller objQueryController = new QueryConroller();
    int LevelID;
    int UserID;
    int RoleID;
    string UserParameter;
    string RegionParameter;
    static string str = "";
    public string strProjectName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        getAuthenticationDetails();
        strProjectName = System.Configuration.ConfigurationManager.AppSettings["WMSProjectName"];

        if (!IsPostBack)
        {
            BindRegion();
            checkException();
            //getACRDetail();
        }

        else
        {
            string strjscript = "<script language='javascript' type='text/javascript'>";
            strjscript += "";
            strjscript += "</script" + ">";
            literal1.Text = strjscript;

        }
    }


    public void getAuthenticationDetails()
    {
        UserParameter = "";
        if (Request.Cookies["UserID"] != null)
        {
            HttpCookie aCookie = Request.Cookies["UserID"];
            UserID = Convert.ToInt32(Server.HtmlEncode(aCookie.Value));
        }

        string strUserDetailsQuery = " Select * from TempLoginUser where UserID=" + UserID;
        DataTable dtUserDetails = objQueryController.ExecuteQuery(strUserDetailsQuery);
        if (dtUserDetails != null)
        {
            if (dtUserDetails.Rows.Count > 0)
            {
                foreach (DataRow drUserDetails in dtUserDetails.Rows)
                {
                    LevelID = Convert.ToInt32(drUserDetails["LevelID"]);
                    RoleID = Convert.ToInt32(drUserDetails["RoleID"]);
                    RegionParameter = drUserDetails["RegionParameter"].ToString();

                }
            }
        }
        string strQuery = "";
        if (LevelID == 3)
        {
            strQuery = " Select distinct UserID from vw_UserDetail where ReportsToID=" + UserID;
            DataTable dtUserParameter = objQueryController.ExecuteQuery(strQuery);
            if (dtUserParameter != null)
            {
                if (dtUserParameter.Rows.Count > 0)
                {
                    int flag = 0;
                    foreach (DataRow drUserParameter in dtUserParameter.Rows)
                    {
                        if (flag != 0)
                        {
                            UserParameter += " or ";
                        }
                        UserParameter += " UserID=" + drUserParameter["UserID"].ToString();
                        flag++;
                    }
                }
            }
        }
        if (LevelID > 3)
        {
            UserParameter = " UserID=" + UserID;
        }
    }

    public void BindRegion()
    {
        string strQuery = "Select * from Region ";
        //if (LevelID > 2)
        //{
        //    strQuery += " where " + RegionParameter;
        //}
        strQuery += " order by Region";
        DataTable dtRegion = new DataTable();
        dtRegion = objQueryController.ExecuteQuery(strQuery);
        if (dtRegion != null)
        {
            if (dtRegion.Rows.Count > 0)
            {
                drpRegion.DataSource = dtRegion;
                drpRegion.DataTextField = "Region";
                drpRegion.DataValueField = "RegionID";
                drpRegion.DataBind();
                drpRegion.AppendDataBoundItems = true;

                ListItem list1 = new ListItem("Select One", "-1");
                drpRegion.Items.Insert(0, list1);
                ListItem list = new ListItem("All", "0");
                drpRegion.Items.Insert(1, list);
                drpRegion.AppendDataBoundItems = false;
                drpRegion.SelectedValue = "-1";

            }
        }
    }
    protected void gridView_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate))
        {
            e.Row.TabIndex = -1;
            e.Row.Attributes["onclick"] = string.Format("javascript:SelectRow(this, {0});", e.Row.RowIndex);
            e.Row.Attributes["onkeydown"] = "javascript:return SelectSibling(event);";
            e.Row.Attributes["onselectstart"] = "javascript:return false;";
        }
    } 

    public void checkException()
    {
        int countRow = objQueryController.getNoOfException("execute usp_countException");
        if (countRow > 0)
        {
            string strjscript = "<script language='javascript' type='text/javascript'>";
            strjscript += " checkException();";
            strjscript += "</script" + ">";
            literal1.Text = strjscript;

        }
    }
    //public void getDealer()
    //{

    //    string strQuery = "";
    //    DataTable dtinformation = new DataTable();
    //    strQuery = "select Code+'('+Dealer+')' as Dealer,Code from Dealer  order by Code";
    //    dtinformation = objQueryController.ExecuteQuery(strQuery);
    //    if (dtinformation != null)
    //    {

    //        if (dtinformation.Rows.Count > 0)
    //        {
    //            ddlDealer.DataSource = dtinformation;
    //            ddlDealer.DataValueField = "Code";
    //            ddlDealer.DataTextField = "Dealer";
    //            ddlDealer.DataBind();
    //            ddlDealer.AppendDataBoundItems = true;
    //            ListItem list = new ListItem("Select All", "0");
    //            ddlDealer.Items.Insert(0, list);
    //            ddlDealer.AppendDataBoundItems = false;

    //        }
    //    }
    //}

    public string strGroup = "";
    public void getACRDetail()
    {
        DataTable dtGridData = getTable();

        grdacrData.Visible = true;

        grdacrData.DataSource = dtGridData;
        //helper = new GridViewHelper(this.grdacrData, false);
        //string[] cols = new string[1];
        //cols[0] = strGroup;
        //helper.RegisterGroup(cols, true, true);
        //helper.GroupHeader += new GroupEvent(helper_GroupHeader);
        //helper.RegisterSummary("Value", SummaryOperation.Sum, strGroup);
        //helper.RegisterSummary("Quantity", SummaryOperation.Sum, strGroup);
        //helper.GroupSummary += new GroupEvent(helper_GroupSummary);
        grdacrData.DataBind();
        if (dtGridData != null)
        {
            if (dtGridData.Rows.Count > 0)
            {
                btnPrint.Visible = true;
                btnExport.Visible = true;
            }
            else
            {
                btnPrint.Visible = false;
                btnExport.Visible = false;
            }
        }
        else
        {
            btnPrint.Visible = false;
            btnExport.Visible = false;
        }

    }

    public DataTable getTable()
    {
        DataTable dtGridData = new DataTable();
        string strQuery = "";
        string strwhrprm = "";
        int flag = 0;
        string FromDate = "";
        string ToDate = "";
        if (txtFromDate.Text != "" && txtToDate.Text != "")
        {
            FromDate = objUtility.ConvertDateTime(txtFromDate.Text.Trim());
            ToDate = objUtility.ConvertDateTime(txtToDate.Text.Trim());
        }
        if (rdoData.SelectedValue == "0")
        {
            if (rdoAlwar.Checked)
            {
                strwhrprm = "  (IsEngine=1 and Engine='A')";
            }
            else
            {
                if (rdoBhopal.Checked)
                {
                    strwhrprm = "((Engine='A' and IsEngine=0) or Engine='s') ";
                }
                else
                {
                    strwhrprm = "(IsEngine=0 or IsEngine=1)  ";
                }
            }
            flag = 1;
        }
        else if (rdoData.SelectedValue == "1")
        {
            if (rdoAlwarEngine.Checked)
            {
                strwhrprm = "  (IsEngine=1 and Engine='A')";
            }
            else
            {
                if (rdoSimpsonEngine.Checked)
                {
                    strwhrprm = " (Engine='S' and IsEngine=1) ";
                }
                else
                {
                    strwhrprm = " (IsEngine=1)  ";
                }
            }
            flag = 1;
        }
        else
        {
            strwhrprm = " (IsEngine=0)  ";
            flag = 1;
        }



        if (rdoPrimary.Checked)
        {
            if (flag == 1)
            {
                strwhrprm += " and ";
            }
            strwhrprm += " DT like 'P%' ";
        }
        else if (rdoConsequences.Checked)
        {
            if (flag == 1)
            {
                strwhrprm += " and ";
            }
            strwhrprm += " DT like 'C%' ";
            flag = 1;
        }
        if (rdoFirst.Checked)
        {
            if (flag == 1)
            {
                strwhrprm += " and ";
            }
            strwhrprm += "  DATEDIFF(MM, INS_DATE, DEF_DATE) >= 0 AND DATEDIFF(MM, INS_DATE, DEF_DATE) < 13 ";
        }
        else if (rdoSecond.Checked)
        {
            if (flag == 1)
            {
                strwhrprm += " and ";
            }
            strwhrprm += " DATEDIFF(MM, INS_DATE, DEF_DATE) > 12 AND DATEDIFF(MM, INS_DATE, DEF_DATE) < 25 ";
        }
        if(strwhrprm!="")
        {
            if (hdnDealerID.Value != "0" && drpRegion.SelectedValue == "0")
            {
                strwhrprm += " and DealerID='" + hdnDealerID.Value + "'";
            }
            else
            {

                if (hdnDealerID.Value != "0" && drpRegion.SelectedValue != "0")
                {
                    strwhrprm += " and DealerID='" + hdnDealerID.Value + "' and RegionID=" + drpRegion.SelectedValue + " ";
                }
                else
                {
                    if (hdnDealerID.Value == "0" && drpRegion.SelectedValue != "0")
                    {
                        strwhrprm += " and RegionID=" + drpRegion.SelectedValue + " ";
                    }  
                }
            }
        }
        if (FromDate != "" && ToDate != "")
        {
            if (strwhrprm != "")
            {
                strwhrprm = strwhrprm + " and CR_DATE between '" + FromDate + "'  and  '" + ToDate + "' ";

            }
            else
            {

                strwhrprm = " and CR_DATE between '" + FromDate + "'  and  '" + ToDate + "' ";
            }
        }
        if (hdnDealerID.Value == "0" && drpRegion.SelectedValue=="0")
        {
            strQuery = "select *,'All' as Model from VW_ACRBulkGroupDetailWithRegion where    " + strwhrprm + " order by DLR_CO";
            strGroup = "Model";
        }
        else
        {
            strQuery = "select * from VW_ACRBulkGroupDetailWithRegion where " + strwhrprm + " order by DLR_CO";
            strGroup = "DEALER_NAME";

        }
        dtGridData = objQueryController.ExecuteQuery(strQuery);
        return dtGridData;
    }

    public void grdacrData_Paging(Object sender, GridViewPageEventArgs e)
    {
        DataTable dtGridData = getTable();
        grdacrData.PageIndex = e.NewPageIndex;
        grdacrData.DataSource = dtGridData;
        grdacrData.DataBind();

    }

    private void helper_GroupHeader(string groupName, object[] values, GridViewRow row)
    {
        if (groupName == strGroup)
        {
            row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
            row.Cells[0].Text = "DEALER:" + values[0].ToString();
            row.BackColor = System.Drawing.Color.Yellow;
        }
    }
    private void helper_GroupSummary(string groupName, object[] values, GridViewRow row)
    {
        row.Cells[0].HorizontalAlign = HorizontalAlign.Right;
        row.Cells[0].Text = "Total:";
        row.Cells[0].ForeColor = System.Drawing.Color.Black;
    }
    public void eventhandlerSerialNo(object Sender, EventArgs E)
    {
        int i = 1;
        int pi = grdacrData.PageIndex;
        int ps = grdacrData.PageSize;
        if (pi > 0)
        {
            i = pi * ps + 1;
        }
        foreach (GridViewRow gr in grdacrData.Rows)
        {
            gr.Cells[0].Text = i.ToString();

            i++;
        }
    }
    /***************************************************************************************************/
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    /*******************************Exporting Record Into Excel*****************************************/
    protected void Button1_Click(object sender, EventArgs e)
    {
        string str = "";
        string strParameter = "";
        GridViewExport objExport = new GridViewExport();


        str = str + "<table width='50%' border='1' cellpadding='0' cellspacing='0'>";
        str = str + "<tr><td style='font-size:small;font-weight:bold;'>Report For:</td><td style='font-size:small;font-weight:bold;'>Dealer Wise ACR Detail</td></tr>";
        str = str + "<tr><td >Region:</td>";
        str = str + "<td style='font-size:small;font-weight:bold;'>" + drpRegion.SelectedItem.Text.ToString().Replace("Select All", "All") + "</td>";
        str = str + "<tr><td >Dealer:</td>";
        str = str + "<td style='font-size:small;font-weight:bold;'>" + hdnDealerName.Value.ToString().Replace("Select All", "All") + "</td></tr>";
        str = str + "<tr><td >From Date:</td>";
        str = str + "<td style='font-size:small;font-weight:bold;'>" + txtFromDate.Text.ToString() + "</td>";
        //str = str + "<td style='font-size:small;font-weight:bold;'>" + drpFromYear.SelectedItem.Text.ToString().Replace("Select All", "All") + "</td>";
        str = str + " <td>To Date:</td>";
        str = str + "<td style='font-size:small;font-weight:bold;'>" + txtToDate.Text.ToString() + "</td></tr>";
        //str = str + "<td style='font-size:small;font-weight:bold;'>" + drpToYear.SelectedItem.Text.ToString().Replace("Select All", "All") + "</td>";

        if (rdoData.SelectedValue == "0")
        {
            str = str + "<tr><td style='font-size:small;font-weight:bold;'>" + rdoData.SelectedItem.Text.ToString() + "</td></tr>";
            str = str + "<tr><td >Place:</td>";
            if (rdoAlwar.Checked)
            {
                str = str + "<td style='font-size:small;font-weight:bold;'>" + rdoAlwar.Text.ToString() + "</td>";
            }
            else if (rdoBhopal.Checked)
            {
                str = str + "<td style='font-size:small;font-weight:bold;'>" + rdoBhopal.Text.ToString() + "</td>";
            }
            else if (rdoAllPlace.Checked)
            {
                str = str + "<td style='font-size:small;font-weight:bold;'>" + rdoAllPlace.Text.ToString() + "</td>";
            }
            str = str + "</tr>";
        }
        else if (rdoData.SelectedValue == "1")
        {
            str = str + "<tr><td style='font-size:small;font-weight:bold;'>" + rdoData.SelectedItem.Text.ToString() + "</td></tr>";
            str = str + "<tr><td >Engine:</td>";
            if (rdoAlwarEngine.Checked)
            {
                str = str + "<td style='font-size:small;font-weight:bold;'>" + rdoAlwarEngine.Text.ToString() + "</td>";
            }
            else if (rdoSimpsonEngine.Checked)
            {
                str = str + "<td style='font-size:small;font-weight:bold;'>" + rdoSimpsonEngine.Text.ToString() + "</td>";
            }
            else if (rdoBothEngine.Checked)
            {
                str = str + "<td style='font-size:small;font-weight:bold;'>" + rdoBothEngine.Text.ToString() + "</td>";
            }
            str = str + "</tr>";
        }
        else
        {
            str = str + "<tr><td style='font-size:small;font-weight:bold;'>" + rdoData.SelectedItem.Text.ToString() + "</td></tr>";
        }
        str = str + "<tr><td >Problem Type:</td>";
        if (rdoPrimary.Checked)
        {
            str = str + "<td style='font-size:small;font-weight:bold;'>" + rdoPrimary.Text.ToString() + "</td>";
        }
        else if (rdoConsequences.Checked)
        {
            str = str + "<td style='font-size:small;font-weight:bold;'>" + rdoConsequences.Text.ToString() + "</td>";
        }
        else if (rdoAllProblem.Checked)
        {
            str = str + "<td style='font-size:small;font-weight:bold;'>" + rdoAllProblem.Text.ToString() + "</td>";
        }
        str = str + "<td >Year:</td>";
        if (rdoFirst.Checked)
        {
            str = str + "<td style='font-size:small;font-weight:bold;'>" + rdoFirst.Text.ToString() + "</td>";
        }
        else if (rdoSecond.Checked)
        {
            str = str + "<td style='font-size:small;font-weight:bold;'>" + rdoSecond.Text.ToString() + "</td>";
        }
        else if (rdoBothYear.Checked)
        {
            str = str + "<td style='font-size:small;font-weight:bold;'>" + rdoBothYear.Text.ToString() + "</td></tr>";
        }

        str = str + "</table><br/>";
        hdnExport.Value = str + hdnExport.Value;
        objExport.ExportGridView(hdnExport.Value);

    }
    public string getchkList(CheckBoxList chkList, string chkListName)
    {

        string strParameter = "<h6>" + chkListName + "</h6> <table cellpadding='0' cellspacing='0' border='1' >";
        strParameter = strParameter + "<tr>";
        string strMiddleData = "";
        if (chkList != null)
        {
            int count = chkList.Items.Count;
            int Status = 0;
            if (chkList.Items.Count > 0)
            {
                foreach (ListItem list in chkList.Items)
                {
                    if (list.Selected)
                    {
                        Status++;
                        strMiddleData = strMiddleData + "<td> " + list.Text + " </td> ";
                    }
                }
            }
            if (Status == count)
            {
                strMiddleData = "<td> " + chkListName + " </td> <td> All </td>";
            }
        }
        strParameter = strParameter + strMiddleData;
        strParameter = strParameter + "</tr></table>";
        return strParameter;
    }
    /***************************************************************************************************/
    protected void btnGo_Click1(object sender, EventArgs e)
    {
        getACRDetail();
    }
}
