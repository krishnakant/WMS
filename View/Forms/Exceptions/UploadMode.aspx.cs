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

public partial class View_Forms_Configurator_UploadMode : System.Web.UI.Page
{
    QueryConroller objQueryController = new QueryConroller();
    public string strProjectName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        strProjectName = System.Configuration.ConfigurationManager.AppSettings["WMSProjectName"];

        //if (Session["ID"] == null)
        //{
        //    Response.Redirect("/SHQ/Login.aspx");
        //}
        DataTable dt = new DataTable();
        string strQuery = "Select * from vwFileUploadMode order by Filename";
        dt = objQueryController.ExecuteQuery(strQuery);

        if (!IsPostBack)
        {
            grdUploadMode.DataSource = dt;
            grdUploadMode.DataBind();
            BindMode(dt);
        }
    }

    public void BindMode(DataTable dt)
    {
        int i = 0;
        foreach (GridViewRow gr in grdUploadMode.Rows)
        {
            string strAction  = dt.Rows[i]["Action"].ToString();
            if (strAction == "Append")
            {
                ((RadioButtonList)gr.FindControl("rdoMode")).SelectedIndex = 0;
            }
            else if (strAction == "Replace")
            {
                ((RadioButtonList)gr.FindControl("rdoMode")).SelectedIndex = 1;
            }
            //((RadioButtonList)gr.FindControl("rdoMode")).SelectedItem.Text = dt.Rows[i]["Action"].ToString();
            i++;
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gr in grdUploadMode.Rows)
        {
            int SelectedIndex = ((RadioButtonList)gr.FindControl("rdoMode")).SelectedIndex;
            string strFilename = ((Label)gr.FindControl("lblFile")).Text;
            string strQuery = "Update FileUploadMode set Mode=" + SelectedIndex + " where Filename='" + strFilename + "'";
            objQueryController.ExecuteQuery(strQuery);
        }   

    }
}
