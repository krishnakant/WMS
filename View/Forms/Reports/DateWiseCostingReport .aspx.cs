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

public partial class View_Forms_Reports_DateWiseCostingReport_ : System.Web.UI.Page
{

    QueryConroller objQueryController = new QueryConroller();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getModelGroup();
        }

    }

    public void getModelGroup()
    {
        DataTable dtModel = new DataTable();
        string strQuery = "Select GroupID,ModelGroupName from ModelGroupName";
        dtModel = objQueryController.ExecuteQuery(strQuery);
        if (dtModel != null)
        {
            if (dtModel.Rows.Count > 0)
            {
                drpModelCode.DataSource = dtModel;
                drpModelCode.DataTextField = "ModelGroupName";
                drpModelCode.DataValueField = "GroupID";
                drpModelCode.DataBind();
                drpModelCode.Visible = true;

                drpModelCode.AppendDataBoundItems = true;
                ListItem list1 = new ListItem("Select", "0");
                drpModelCode.Items.Insert(0, list1);
                drpModelCode.AppendDataBoundItems = false;
            }
        }
    }

}
