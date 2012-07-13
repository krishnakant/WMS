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

public partial class View_Forms_Reports_ProductionPopup : System.Web.UI.Page
{
    QueryConroller objQueryController = new QueryConroller();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["ModelList"] != null && Request.QueryString["ModelCategoryIDList"] != null && Request.QueryString["ModelSpecialList"] != null && Request.QueryString["Year"] != null)
        {

            hdnModelGroupName.Value = Request.QueryString["ModelList"].ToString();
            hdnModelCategory.Value = Request.QueryString["ModelCategoryIDList"].ToString();
            hdnClutchType.Value = Request.QueryString["ClutchTypeIDList"].ToString();
            hdnModelSpecial.Value = Request.QueryString["ModelSpecialList"].ToString();
            hdnYear.Value = Request.QueryString["Year"].ToString();
            hdnFrom.Value = Request.QueryString["From"].ToString();
            hdnTo.Value = Request.QueryString["To"].ToString();
            hdnFlag.Value = Request.QueryString["Flag"].ToString();
        }
        BindGrid();

    }

    public void eventhandlerSerialNo(object Sender, EventArgs E)
    {
        int i = 1;
        int pi = grdProdData.PageIndex;
        int ps = grdProdData.PageSize;
        if (pi > 0)
        {
            i = pi * ps + 1;
        }
        foreach (GridViewRow gr in grdProdData.Rows)
        {
            gr.Cells[0].Text = i.ToString();

            i++;
        }
    }


    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

    public void BindGrid()
    {

        DataTable dt = getTable();
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                btnExport.Visible = true;
                grdProdData.DataSource = dt;
                grdProdData.DataBind();
            }
            else
            {
                btnExport.Visible = false;
                grdProdData.DataSource = null;
                grdProdData.DataBind();
            }
        }
        else
        {
            btnExport.Visible = false;
            grdProdData.DataSource = null;
            grdProdData.DataBind();
        }
    }

    public DataTable getTable()
    {
        string strQuery;
        if (hdnFlag.Value == "1")
        {
            strQuery = "select * from vw_ProductionReportData where " + hdnModelGroupName.Value + " and " + hdnModelCategory.Value + " and " + hdnClutchType.Value + " and " + hdnModelSpecial.Value + "";
            strQuery += " and (Production_Month between "+hdnFrom.Value +" and  "+hdnTo.Value+")";
        }
        else
        {
            strQuery = "select * from vw_ProductionReportData where " + hdnModelGroupName.Value + " and " + hdnModelCategory.Value + " and " + hdnClutchType.Value + " and " + hdnModelSpecial.Value + "";
            strQuery += " and   Production_Month_Year='" + hdnYear.Value + "' ";
        }
        DataTable dt = objQueryController.ExecuteQuery(strQuery);
        return dt;
    }

    public void grdsalesData_Paging(Object sender, GridViewPageEventArgs e)
    {
        DataTable dtGridData = getTable();
        grdProdData.PageIndex = e.NewPageIndex;
        grdProdData.DataSource = dtGridData;
        grdProdData.DataBind();

    }


    protected void btnExport_Click(object sender, EventArgs e)
    {
        string str = "";
        string strParameter = "";
        GridViewExport objExport = new GridViewExport();

        //strParameter = strParameter + getchkList(chkModelCodeList, "Model");
        //strParameter = strParameter + getchkList(chkCategory, "Category");
        //strParameter = strParameter + getchkList(chkClutchType, "ClutchType");
        //strParameter = strParameter + getchkList(chkSpecialList, "Special");
        //str = str + "<table width='50%' border='1' cellpadding='0' cellspacing='0'>";
        //str = str + "<tr><td style='font-size:small;font-weight:bold;'>Report For:</td><td style='font-size:small;font-weight:bold;'>Production</td></tr>";
        //str = str + "</table><br/>";
        //str = str + strParameter;
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
}
