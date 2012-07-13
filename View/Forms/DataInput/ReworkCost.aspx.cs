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

public partial class View_Forms_DataInput_ReworkCost : System.Web.UI.Page
{
    QueryConroller objQueryController = new QueryConroller();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string Date = (System.DateTime.Now.ToShortDateString());
            string[] strDateArray = Date.Split('/');
            string CrDate = strDateArray[2].ToString();
            drpYear.SelectedValue = CrDate;
            BindModel();
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveInformation();
    }

    public void SaveInformation()
    {
        int MonthID = Convert.ToInt32(drpMonth.SelectedValue);
        int YearID = Convert.ToInt32(drpYear.SelectedValue);
        int ModelGroupID = Convert.ToInt32(drpModel.SelectedValue);
        double ReworkCost_I_Year = Convert.ToDouble(txtReworkCostIYear.Text.Trim());
        double ReworkCost_II_Year = Convert.ToDouble(txtReworkCostIIYear.Text.Trim());

        ReworkCostController objController = new ReworkCostController();
        ReworkCostUI objUI = new ReworkCostUI();

        objUI.MonthID = MonthID;
        objUI.YearID = YearID;
        objUI.GroupID = ModelGroupID;
        objUI.ReworkCost_I_Year = ReworkCost_I_Year;
        objUI.ReworkCost_II_Year = ReworkCost_II_Year;
        objUI.ModelCategoryID = Convert.ToInt32(rdoModelCategory.SelectedValue);
        objUI.HMR_Range = rdoHMR.SelectedItem.Text;
        int ID = 0;
        try
        {
            ID = objController.SaveReworkCost(objUI, null);
            if (ID >0)
            {
                string str = "<script language = 'javascript'>";
                str += "setMessageText('ctl00_ContentPlaceHolder1_lblMessage','Records Saved Successfully');";
                str += "</script>";
                Literal1.Text = str;
            }
            else
            {
                string str = "<script language = 'javascript'>";
                str += "setMessageText('ctl00_ContentPlaceHolder1_lblMessage','Records could not be Saved Successfully');";
                str += "</script>";
                Literal1.Text = str;
            }
        }
        catch
        {
            string str = "<script language = 'javascript'>";
            str += "setMessageText('ctl00_ContentPlaceHolder1_lblMessage','Records could not be Saved Successfully');";
            str += "</script>";
            Literal1.Text = str;
        }
    }
}
