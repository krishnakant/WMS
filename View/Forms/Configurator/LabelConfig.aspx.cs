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

public partial class View_Configurator_LabelConfig : System.Web.UI.Page
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
            BindForms();
        }

    }

    public void BindForms()
    {
        if (drpForms.SelectedIndex != 0)
        {

            DataTable dt = new DataTable();
            string strQuery = "Select distinct Form from Label_Data";
            dt = objController.ExecuteQuery(strQuery);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    drpForms.DataSource = dt;
                    drpForms.DataTextField = "Form";
                    drpForms.DataValueField = "Form";
                    drpForms.DataBind();
                    drpForms.AppendDataBoundItems = true;
                    ListItem list = new ListItem("Select Form", "0");
                    drpForms.Items.Insert(0, list);
                    drpForms.AppendDataBoundItems = false;
                   
                }
            }
        }
        
        
    }
    protected void drpForms_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }

    public void BindGrid()
    {
        string strSelectedForm = drpForms.SelectedItem.Text;
        DataTable dtGrid = new DataTable();
        string strQuery = "Select * from Label_Data where Form='" + strSelectedForm + "'";
        dtGrid = objController.ExecuteQuery(strQuery);
        if (dtGrid != null)
        {
            if (dtGrid.Rows.Count > 0)
            {
                grdFormData.DataSource = dtGrid;
                grdFormData.DataBind();
                btnUpdate.Enabled = true;
            }
            else
            {
                grdFormData.DataSource = null;
                grdFormData.EmptyDataText = "Label Not present";
                grdFormData.DataBind();
                btnUpdate.Enabled = false;
            }
            
        }
        else
        {
            grdFormData.DataSource = null;
            grdFormData.EmptyDataText = "Label Not present";
            grdFormData.DataBind();
            btnUpdate.Enabled = false;
        }
      
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gr in grdFormData.Rows)
        {
            HiddenField hdnID = ((HiddenField)gr.FindControl("hdnID"));
            string strFormName = drpForms.SelectedItem.Text;
            string strLabelText = ((TextBox)gr.FindControl("txtLabelText")).Text;
            string LabelID = Convert.ToString(hdnID.Value);
            string strQuery = "Update Label_Data set Text ='" + strLabelText + "' where Label_ID='" + LabelID+"' and Form='"+ strFormName +"'";
            objController.ExecuteQuery(strQuery);
        }

    }
}
