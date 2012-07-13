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

public partial class View_Forms_Configurator_Model : System.Web.UI.Page
{
    QueryConroller objController = new QueryConroller();
    public string strProjectName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        strProjectName = System.Configuration.ConfigurationManager.AppSettings["WMSProjectName"];

        //if (Session["ID"] == null)
        //{
        //    Response.Redirect("/SHQ/Login.aspx");
        //}
        if (!IsPostBack)
        {
            BindModel();
            
        }
        DataTable dtControl = new DataTable();
        string strControlQuery = "Select * from Label_Data where Form='Model'";
        dtControl = objController.ExecuteQuery(strControlQuery);
        foreach (DataRow dr in dtControl.Rows)
        {
            string strLabel = dr["Label_ID"].ToString();
            string strLabelText = dr["Text"].ToString();

            if (strLabel == "lblModel")
            {
                lblModel.Text = strLabelText;
            }
            else if (strLabel == "lblProduct")
            {
                lblProduct.Text = strLabelText;
            }
            else if (strLabel == "lblDescription")
            {
                lblDescription.Text = strLabelText;
            }
           

        }


    }

    public void BindModel()
    {
        DataTable dtModel = new DataTable();
        string strQuery = "Select Distinct Model_Code from Model";
        dtModel = objController.ExecuteQuery(strQuery);
        drpModel.DataSource = dtModel;
        drpModel.DataTextField = "Model_Code";
        drpModel.DataValueField = "Model_Code";
        drpModel.DataBind();

    }
    protected void drpModel_SelectedIndexChanged(object sender, EventArgs e)
    {
        string strselModel = drpModel.SelectedItem.Text;
        DataTable dtModelCode = new DataTable();
        string strQuery = "Select [Model],[Code] from Model where [Model_Code]='"+strselModel+"'";
        dtModelCode = objController.ExecuteQuery(strQuery);
        chkModel.DataSource = dtModelCode;
        chkModel.DataTextField = "Code";
        chkModel.DataValueField = "Code";
        chkModel.DataBind();
    }
}
