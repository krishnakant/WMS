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

public partial class View_Forms_Reports_NotAssignGroupDetail : System.Web.UI.Page
{
    QueryConroller objQueryController = new QueryConroller();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getNotAssignCodeDetail();
        }
    }

    public void getNotAssignCodeDetail()
    {
        string strQuery = "";
        DataTable dtCodeDetail = new DataTable();
        strQuery = "select * from vw_GroupNotAssignDetail";
        dtCodeDetail = objQueryController.ExecuteQuery(strQuery);
        grdNotAssignCode.DataSource = dtCodeDetail;
        grdNotAssignCode.DataBind();
      
    }
    public void eventhandlerSerialNo(object Sender, EventArgs E)
    {
        int i = 1;
        int pi = grdNotAssignCode.PageIndex;
        int ps = grdNotAssignCode.PageSize;
        if (pi > 0)
        {
            i = pi * ps + 1;
        }
        foreach (GridViewRow gr in grdNotAssignCode.Rows)
        {
            gr.Cells[0].Text = i.ToString();

            i++;
        }
    }
    /***************************************************************************************************/
}
