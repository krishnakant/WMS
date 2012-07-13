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

public partial class View_Forms_Reports_SalesPop : System.Web.UI.Page
{
    QueryConroller objQueryController = new QueryConroller();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["ModelList"] != null && Request.QueryString["ModelCategoryIDList"] != null && Request.QueryString["ModelSpecialList"] != null && Request.QueryString["RegionID"] != null && Request.QueryString["Year"] != null && Request.QueryString["MonthID"] != null)
        {

            hdnModelGroupName.Value = Request.QueryString["ModelList"].ToString();
            hdnModelCategory.Value = Request.QueryString["ModelCategoryIDList"].ToString();
            hdnClutchType.Value = Request.QueryString["ClutchTypeIDList"].ToString();
            hdnModelSpecial.Value = Request.QueryString["ModelSpecialList"].ToString();
            hdnRegion.Value = Request.QueryString["RegionID"].ToString();
            hdnMonth.Value = Request.QueryString["MonthID"].ToString();
            hdnYear.Value = Request.QueryString["Year"].ToString();
        }
        BindGrid();
    }


    public void BindGrid()
    {

        DataTable dt = getTable();
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {

                grdSalesData.DataSource = dt;
                grdSalesData.DataBind();
            }
            else
            {

                grdSalesData.DataSource = null;
                grdSalesData.DataBind();
            }
        }
        else
        {

            grdSalesData.DataSource = null;
            grdSalesData.DataBind();
        }
    }

    public DataTable getTable()
    {
        string strQuery = "select * from vw_DealerSalesData where " + hdnModelGroupName.Value + " and " + hdnModelCategory.Value + " and " + hdnClutchType.Value + " and " + hdnModelSpecial.Value + " ";
        strQuery += " and  RegionID=" + hdnRegion.Value + " and Month(Date)=" + hdnMonth.Value + " and  Year(Date)=" + hdnYear.Value + " ";
        DataTable dt = objQueryController.ExecuteQuery(strQuery);
        return dt;
    }

    public void eventhandlerSerialNo(object Sender, EventArgs E)
    {
        int i = 1;
        int pi = grdSalesData.PageIndex;
        int ps = grdSalesData.PageSize;
        if (pi > 0)
        {
            i = pi * ps + 1;
        }
        foreach (GridViewRow gr in grdSalesData.Rows)
        {
            gr.Cells[0].Text = i.ToString();

            i++;
        }
    }
}
