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

public partial class View_Forms_Exceptions_Exception : System.Web.UI.Page
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
        string strQuery = "Select * from vwExceptionCountNew";
        try
        {
            DataTable dt = objQueryController.ExecuteQuery(strQuery);
            grdExceptionCount.DataSource = dt;
            grdExceptionCount.DataBind();
        }
        catch { }
    }
}
