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

public partial class View_Forms_Exceptions_DealerException : System.Web.UI.Page
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
        string strQuery = "select * from vw_DealerExceptions order by dealer_code";
        DataTable dt = objQueryController.ExecuteQuery(strQuery);
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                grdDealerExceptions.DataSource = dt;
                grdDealerExceptions.DataBind();
            }
            else
            {
                grdDealerExceptions.DataSource = null;
                grdDealerExceptions.DataBind();
            }
        }
        else
        {
            grdDealerExceptions.DataSource = null;
            grdDealerExceptions.DataBind();
        }
    }

    public void eventhandlerSerialNo(object Sender, EventArgs E)
    {
        int i = 1;
        int pi = grdDealerExceptions.PageIndex;
        int ps = grdDealerExceptions.PageSize;
        //<><> Use Name of Your GridView Instead Of grdDealerExceptions <><>// 
        if (pi > 0)
        {
            i = pi * ps + 1;
        }
        foreach (GridViewRow gr in grdDealerExceptions.Rows)
        {
            gr.Cells[0].Text = i.ToString();

            i++;
        }


    }

}
