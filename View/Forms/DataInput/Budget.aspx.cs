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

public partial class View_Forms_DataInput_Budget : System.Web.UI.Page
{
    QueryConroller objQueryController = new QueryConroller();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGrid();
            BindModel();
            BindModelCategory();
            BindClutch();
            BindModelSpecial();
        }
    }

    public void BindModelSpecial()
    {
        string strQuery = "select * from ModelSpecialDetails order by ModelSpecial";
        DataTable dt = objQueryController.ExecuteQuery(strQuery);
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                drpSpecial.DataSource = dt;
                drpSpecial.DataTextField = "ModelSpecial";
                drpSpecial.DataValueField = "ModelSpecialID";
                drpSpecial.DataBind();
                drpSpecial.AppendDataBoundItems = true;
                ListItem list = new ListItem("NA", "0");
                drpSpecial.Items.Insert(0, list);
                drpSpecial.AppendDataBoundItems = false;
            }
        }
    }


    public void BindClutch()
    {
        string strQuery = "select * from ModelClutchType order by ClutchType";
        DataTable dt = objQueryController.ExecuteQuery(strQuery);
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                drpClutch.DataSource = dt;
                drpClutch.DataTextField = "ClutchType";
                drpClutch.DataValueField = "ClutchTypeID";
                drpClutch.DataBind();
                drpClutch.AppendDataBoundItems = true;
                ListItem list = new ListItem("Select", "0");
                drpClutch.Items.Insert(0, list);
                drpClutch.AppendDataBoundItems = false;
            }
        }
    }

    public void BindModel()
    {
        string strModelQuery = "Select * from ModelGroupName order by GroupID";
        DataTable dtModel = objQueryController.ExecuteQuery(strModelQuery);

        if (dtModel != null)
        {
            if (dtModel.Rows.Count > 0)
            {
                drpModel.DataSource = dtModel;
                drpModel.DataTextField = "ModelGroupName";
                drpModel.DataValueField = "GroupID";
                drpModel.DataBind();
                drpModel.AppendDataBoundItems = true;
                ListItem list = new ListItem("Select", "0");
                drpModel.Items.Insert(0, list);
                drpModel.AppendDataBoundItems = false;


            }
        }
    }


    public void BindModelCategory()
    {
        string strModelQuery = "Select * from ModelCategory order by ModelCategoryID";
        DataTable dtModel = objQueryController.ExecuteQuery(strModelQuery);

        if (dtModel != null)
        {
            if (dtModel.Rows.Count > 0)
            {
                drpModelCategory.DataSource = dtModel;
                drpModelCategory.DataTextField = "ModelCategory";
                drpModelCategory.DataValueField = "ModelCategoryID";
                drpModelCategory.DataBind();
                drpModelCategory.AppendDataBoundItems = true;
                ListItem list = new ListItem("Select", "0");
                drpModelCategory.Items.Insert(0, list);
                drpModelCategory.AppendDataBoundItems = false;


            }
        }
    }


    public void BindGrid()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Quarter");
        dt.Columns.Add("QuarterID");

        for (int i = 1; i <= 4; i++)
        {
            DataRow dr = dt.NewRow();
            dr["Quarter"] = "Quarter-" + i.ToString();
            dr["QuarterID"] = i;
            dt.Rows.Add(dr);
        }
        grdBudget.DataSource = dt;
        grdBudget.DataBind();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveInformation();
    }

    public void SaveInformation()
    {
        BudgetController objController = new BudgetController();
        BudgetUI objUI = new BudgetUI();
        int Status = 0;
        try
        {
            string strDeleteQuery = "Delete from Budget where FinancialYear='" + drp_FinancialYear.SelectedItem.Text + "' and ModelGroupID=" + drpModel.SelectedValue + " and ModelCategoryID=" + drpModelCategory.SelectedValue + " and ModelClutchID=" + drpClutch.SelectedValue + " and ModelSpecialID=" + drpSpecial.SelectedValue; ;
            objQueryController.ExecuteQuery(strDeleteQuery);

            objUI.FinancialYear = drp_FinancialYear.SelectedItem.Text;
            foreach (GridViewRow gr in grdBudget.Rows)
            {
                objUI.QuarterID = Convert.ToInt32(((HiddenField)gr.FindControl("hdnQuarterID")).Value);
                string strBudget = ((TextBox)gr.FindControl("txtBudget")).Text.Trim();
                if (strBudget == "")
                {
                    objUI.Budget = Convert.ToDouble(null);
                }
                else
                {
                    objUI.Budget = Convert.ToDouble(strBudget);
                }
                objUI.ModelGroupID =  Convert.ToInt32(drpModel.SelectedValue);
                objUI.ModelCategoryID = Convert.ToInt32(drpModelCategory.SelectedValue);
                objUI.ModelClutchID = Convert.ToInt32(drpClutch.SelectedValue);
                if (chkSpecial.Checked)
                {
                    objUI.ModelSpecialID = Convert.ToInt32(drpSpecial.SelectedValue);
                }
                else
                {
                    objUI.ModelSpecialID = 0;
                }
                Status = objController.SaveBudget(objUI, null);

            }
        }
        catch
        {
            Status = 0;
        }
        if (Status > 0)
        {
            string str = "<script language = 'javascript'>";
            str += "setMessageText('ctl00_ContentPlaceHolder1_lblMessage','Records Saved Successfully');";
            str += "</script>";
            literal1.Text = str;
        }
        else
        {
            string str = "<script language = 'javascript'>";
            str += "setMessageText('ctl00_ContentPlaceHolder1_lblMessage','Records could not be Saved Successfully');";
            str += "</script>";
            literal1.Text = str;
        }
    }
}
