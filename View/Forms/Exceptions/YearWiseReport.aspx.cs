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

public partial class View_Forms_Exceptions_YearWiseReport : System.Web.UI.Page
{

    QueryConroller objQuerycontroller = new QueryConroller();
    public string strProjectName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        strProjectName = System.Configuration.ConfigurationManager.AppSettings["WMSProjectName"];


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

    public void bindData()
    {
       
        string strQuery = "";
        DataTable dtinformation = new DataTable();
        if (Convert.ToInt32(ddlYear.SelectedValue) == 1 || Convert.ToInt32(ddlYear.SelectedValue) == 2 || Convert.ToInt32(ddlYear.SelectedValue) == 3)
        {
            strQuery = "select Model_Code,sum(Value) as Value,sum(Quantity) as Quantity from vw_AcrYearWiseDefect where YearTime='" + ddlYear.SelectedItem.Text + "' group by Model_Code order by Model_Code ";
        }
        else
        {
            strQuery = "select Model_Code,sum(Value) as Value,sum(Quantity) as Quantity from vw_AcrYearWiseDefect group by Model_Code order by Model_Code ";
        }
        dtinformation = objQuerycontroller.ExecuteQuery(strQuery);
        if (dtinformation != null)
        {
            if (dtinformation.Rows.Count > 0)
            {
               gdDefect.DataSource = dtinformation;
               gdDefect.DataBind();

            }
            else
            {
                gdDefect.DataSource = null;
                gdDefect.DataBind();
            }
        }
        else
        {
            gdDefect.DataSource = null;
            gdDefect.DataBind();
        }
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        bindData(); 
    }

    public void eventhandlerSerialNo(object Sender, EventArgs E)
    {
        int i = 1;
        int pi = gdDefect.PageIndex;
        int ps = gdDefect.PageSize;
        //<><> Use Name of Your GridView Instead Of gvDetailProspect <><>// 
        if (pi > 0)
        {
            i = pi * ps + 1;
        }
        foreach (GridViewRow gr in gdDefect.Rows)
        {
            gr.Cells[0].Text = i.ToString();

            i++;
        }
    }
}
