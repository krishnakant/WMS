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

public partial class View_Forms_Exceptions_MissingTractorNo : System.Web.UI.Page
{
    QueryConroller objQueryController = new QueryConroller();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGrid();
        }
    }

    public void BindGrid()
    {
        string strQuery = "select * from vw_MissingTractorNo";
        DataTable dt = objQueryController.ExecuteQuery(strQuery);
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                grdMissingTractor.DataSource = dt;
                grdMissingTractor.DataBind();
            }
            else
            {
                grdMissingTractor.DataSource = null;
                grdMissingTractor.DataBind();
            }
        }
        else
        {
            grdMissingTractor.DataSource = null;
            grdMissingTractor.DataBind();
        }
    }


    public void eventhandlerSerialNo(object Sender, EventArgs E)
    {
        int i = 1;
        int pi = grdMissingTractor.PageIndex;
        int ps = grdMissingTractor.PageSize;
        //<><> Use Name of Your GridView Instead Of grdMissingTractor <><>// 
        if (pi > 0)
        {
            i = pi * ps + 1;
        }
        foreach (GridViewRow gr in grdMissingTractor.Rows)
        {
            gr.Cells[0].Text = i.ToString();

            i++;
        }


    }
}
