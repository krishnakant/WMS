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

public partial class View_Forms_Reports_ClaimReport : System.Web.UI.Page
{
    QueryConroller objQueryController = new QueryConroller();
    Utility objUtility = new Utility();
    public string strProjectName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        strProjectName = System.Configuration.ConfigurationManager.AppSettings["WMSProjectName"];

        if (!IsPostBack)
        {
            Session["ReportType"] = null;
            Session["HMR_Range"] = null;
            Session["ProductType"] = null;
            getDefectGroup();
        }

    }

    public void getDefectGroup()
    {
        string strQuery = "";
        DataTable dtinformation = new DataTable();
        strQuery = "select distinct  DefectGroupID,GroupName from DefectGroup  order by GroupName";
        dtinformation = objQueryController.ExecuteQuery(strQuery);
        if (dtinformation != null)
        {

            if (dtinformation.Rows.Count > 0)
            {
                chkDefectGroup.DataSource = dtinformation;
                chkDefectGroup.DataValueField = "DefectGroupID";
                chkDefectGroup.DataTextField = "GroupName";
                chkDefectGroup.DataBind();
            }

        }

    }

    protected void btnGenerateReport_Click(object sender, EventArgs e)
    {
        getParameters();
    }

    public string getChecked(CheckBoxList chkList, string ColoumnName)
    {
        string strParameter = "";
        if (chkList != null)
        {
            if (chkList.Items.Count > 0)
            {
                foreach (ListItem list in chkList.Items)
                {
                    if (list.Selected)
                    {
                        if (strParameter == "")
                        {
                            strParameter = " (" + ColoumnName + "='" + list.Text.Trim() + "')";
                        }
                        else
                        {
                            strParameter = strParameter + "  or  (" + ColoumnName + "='" + list.Text.Trim() + "')";
                        }
                    }
                }
            }
        }
        return strParameter;
    }

    public void getParameters()
    {
        string strProjectName = System.Configuration.ConfigurationManager.AppSettings["WMSProjectName"];
        int IsNew = 2;   // 2 is the value for all
        string strReportType = "";
        string strHMR_Range = "";
        if (rdoAll.Checked)
        {
            IsNew = 2;
        }
        else if (rdoNew.Checked)
        {
            IsNew = 1;
        }
        else if (rdoRegular.Checked)
        {
            IsNew = 0;
        }

        if (rdoCost.Checked)
        {
            strReportType = "Cost";
        }
        else if (rdoDefect.Checked)
        {
            strReportType = "Defect";
        }

        if (rdoLessThan250.Checked)
        {
            strHMR_Range = "0-250";
        }
        else if (rdoMoreThan250.Checked)
        {
            strHMR_Range = "251-2500";
        }
        else if (rdoHMRAll.Checked)
        {
            strHMR_Range = "All";
        }

        //string strFromDate =calFromDate.Text.ToString();
        //string strToDate = calToDate.Text.ToString();
        string strFromDate = txtFromDate.Text.Trim();
        string strToDate = txtToDate.Text.Trim();
        //DateTime dtFromDate = calFromDate.SelectedDate;
        //DateTime dtToDate = calToDate.SelectedDate;


        Session.Add("ProductType", IsNew);
        if (strReportType == "Cost")
        {
            Session.Add("ReportType", strReportType);
        }
        else
        {
            Session.Add("ReportType", getChecked(chkDefectGroup,"GroupName"));
        }
        Session.Add("HMR_Range", strHMR_Range);
        Session.Add("FromDate", strFromDate);
        Session.Add("ToDate", strToDate);
        Response.Redirect(strProjectName + "/View/Forms/Reports/ClaimGenerateReport.aspx");
    
    }


}
