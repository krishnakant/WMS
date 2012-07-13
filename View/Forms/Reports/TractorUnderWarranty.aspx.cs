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

public partial class View_Forms_Reports_TractorUnderWarranty : System.Web.UI.Page
{
    QueryConroller objQueryController = new QueryConroller();
    private GridViewHelper helper;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string Date = (System.DateTime.Now.ToShortDateString());
            string[] strDateArray = Date.Split('/');
            string CrDate = strDateArray[2].ToString();
            drpFromYear.SelectedValue = CrDate;
            drpToYear.SelectedValue = CrDate;

            BindModel();
            BindModelCategory();
            BindModelClutch();
            BindModelSpecial();
            BindRegion();
        }
    }

    public void BindModel()
    {
        string strQuery = "";
        DataTable dtModel = new DataTable();
        strQuery = "select * from ModelGroupName  order by ModelGroupName";
        dtModel = objQueryController.ExecuteQuery(strQuery);
        if (dtModel != null)
        {
            if (dtModel.Rows.Count > 0)
            {
                chkModelCodeList.DataSource = dtModel;
                chkModelCodeList.DataValueField = "GroupID";
                chkModelCodeList.DataTextField = "ModelGroupName";
                chkModelCodeList.DataBind();
            }

        }

    }

    public void BindModelCategory()
    {
        string strQuery = "";
        DataTable dtinformation = new DataTable();
        strQuery = "select * from ModelCategory";
        dtinformation = objQueryController.ExecuteQuery(strQuery);
        if (dtinformation != null)
        {

            if (dtinformation.Rows.Count > 0)
            {
                chkCategory.DataSource = dtinformation;
                chkCategory.DataValueField = "ModelCategoryID";
                chkCategory.DataTextField = "ModelCategory";
                chkCategory.DataBind();
            }
        }
    }

    public void BindModelClutch()
    {
        string strQuery = "";
        DataTable dtinformation = new DataTable();
        strQuery = "select * from ModelClutchType";
        dtinformation = objQueryController.ExecuteQuery(strQuery);
        if (dtinformation != null)
        {

            if (dtinformation.Rows.Count > 0)
            {
                chkClutchType.DataSource = dtinformation;
                chkClutchType.DataValueField = "ClutchTypeID";
                chkClutchType.DataTextField = "Description";
                chkClutchType.DataBind();
            }
        }
    }

    public void BindModelSpecial()
    {
        string strQuery = "";
        DataTable dtinformation = new DataTable();
        strQuery = "select * from ModelSpecialDetails";
        dtinformation = objQueryController.ExecuteQuery(strQuery);
        if (dtinformation != null)
        {

            if (dtinformation.Rows.Count > 0)
            {
                chkSpecialList.DataSource = dtinformation;
                chkSpecialList.DataValueField = "ModelSpecialID";
                chkSpecialList.DataTextField = "ModelSpecial";
                chkSpecialList.DataBind();
                chkSpecialList.AppendDataBoundItems = true;
                ListItem list = new ListItem("NA", "0");
                chkSpecialList.Items.Insert(0, list);
                chkSpecialList.AppendDataBoundItems = false;

            }
        }
    }

    public void BindRegion()
    {
        string strQuery = "";
        DataTable dtinformation = new DataTable();
        strQuery = "select * from Region order by RegionID";
        dtinformation = objQueryController.ExecuteQuery(strQuery);
        if (dtinformation != null)
        {

            if (dtinformation.Rows.Count > 0)
            {
                chkRegion.DataSource = dtinformation;
                chkRegion.DataValueField = "RegionID";
                chkRegion.DataTextField = "Region";
                chkRegion.DataBind();
            }
        }
    }

    public string strGroup = "";
    public void BindGrid()
    {
        grdWarranty.Columns.Clear();
        DataTable dtWarranty = getTable();
        string strFromDate = drpFromMonth.SelectedValue + "/01/" + drpFromYear.SelectedItem.Text;
        string strToDate = drpToMonth.SelectedValue + "/02/" + drpToYear.SelectedItem.Text;
        string strSalesPeriodQuery = "Select distinct Sales_Month,Sales_Year,substring(DateName(month,Convert(varchar,Sales_Month)+'/01/'+Convert(varchar,Sales_Year)),1,3)+'-'+Convert(varchar,Sales_Year) as Sales_Period from TempWarrantyMain where SalesDate between '"+ strFromDate +"' and '"+ strToDate +"' order by Sales_Year,Sales_Month";
        DataTable dt = objQueryController.ExecuteQuery(strSalesPeriodQuery);

        BoundField bndField = new BoundField();
        bndField.DataField = "Field";
        bndField.HeaderText = "Model";
        grdWarranty.Columns.Add(bndField);

        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    BoundField bnField = new BoundField();
                    bnField.DataField = dr["Sales_Period"].ToString();
                    bnField.HeaderText = dr["Sales_Period"].ToString();
                    grdWarranty.Columns.Add(bnField);
                }
            }
        }
        if (dtWarranty != null)
        {
            if (dtWarranty.Rows.Count > 0)
            {
                grdWarranty.DataSource = dtWarranty;
                grdWarranty.DataBind();
                strGroup = "Model";
                helper = new GridViewHelper(this.grdWarranty, false);
                string[] cols = new string[1];
                cols[0] = strGroup;
                helper.RegisterGroup(cols, true, true);
                helper.GroupHeader += new GroupEvent(helper_GroupHeader);
                btnExport.Visible = true;
                grdWarranty.DataBind();
               
                //foreach (GridViewRow gr in grdWarranty.Rows)
                //{
                //    string strWarrantyPeriod = gr.Cells[7].Text;
                //    if (strWarrantyPeriod == "1")
                //    {
                //        gr.Cells[3].Text = "NA";
                //        gr.Cells[5].Text = "NA";
                //        gr.Cells[6].Text = "NA";
                //    }
                //}
                //grdWarranty.Columns[7].Visible = false;
            }
            else
            {
                btnExport.Visible = false;
                grdWarranty.DataSource = null;
                grdWarranty.DataBind();

            }
        }
        else
        {
            btnExport.Visible = false;
            grdWarranty.DataSource = null;
            grdWarranty.DataBind();
        }
    }
    private void helper_GroupHeader(string groupName, object[] values, GridViewRow row)
    {
        if (groupName == strGroup)
        {
            row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
            row.Cells[0].Text = "Model:" + values[0].ToString();
            row.BackColor = System.Drawing.Color.Aqua;
        }
    }
    public DataTable getTable()
    {
        string strModelParameter = "";
        int modelflag = 0;
       
        foreach (ListItem list in chkModelCodeList.Items)
        {
            if (list.Selected)
            {
                if (modelflag > 0)
                {
                    strModelParameter += " or ";
                }
                strModelParameter += " GroupID =" + list.Value + " ";
                modelflag++;
            }

        }

        string strModelCategoryParameter = "";
        int modelcategoryflag = 0;
        foreach (ListItem list in chkCategory.Items)
        {
            if (list.Selected)
            {
                if (modelcategoryflag > 0)
                {
                    strModelCategoryParameter += " or ";
                }
                strModelCategoryParameter += " ModelCategoryID =" + list.Value;
                modelcategoryflag++;
            }

        }

        string strModelClutchParameter = "";
        int modelclutchflag = 0;
        foreach (ListItem list in chkClutchType.Items)
        {
            if (list.Selected)
            {
                if (modelclutchflag > 0)
                {
                    strModelClutchParameter += " or ";
                }
                strModelClutchParameter += " ClutchTypeID =" + list.Value;
                modelclutchflag++;
            }

        }

        string strRegionParameter = "";
        int regionflag = 0;
        foreach (ListItem list in chkRegion.Items)
        {
            if (list.Selected)
            {
                if (regionflag > 0)
                {
                    strRegionParameter += " or ";
                }
                strRegionParameter += " RegionID =" + list.Value;
                regionflag++;
            }

        }

        if (chkRegionAll.Checked)
        {
           strRegionParameter += " or RegionID is null ";
        }
        string strModelSpecialParameter = "";
        int modelspecialflag = 0;
        foreach (ListItem list in chkSpecialList.Items)
        {
            if (list.Selected)
            {
                if (modelspecialflag > 0)
                {
                    strModelSpecialParameter += " or ";
                }
                if (list.Value == "0")
                {
                    strModelSpecialParameter += " ModelSpecialID is null ";
                }
                else
                {
                    strModelSpecialParameter += " ModelSpecialID =" + list.Value;
                }
                modelspecialflag++;
            }

        }

        string strQuery = "exec usp_WarrantyDetailsNew '" + strModelParameter + "','" + strModelCategoryParameter + "','" + strModelClutchParameter + "','" + strModelSpecialParameter + "','" + strRegionParameter + "','" + drpFromMonth.SelectedValue + "','" + drpFromYear.SelectedValue + "','" + drpToMonth.SelectedValue + "','" + drpToYear.SelectedValue + "'";
        DataTable dt = objQueryController.ExecuteQuery(strQuery);
        return dt;

    }

    

    protected void OnRowCreated(object sender, GridViewRowEventArgs e)
    {
       

    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        string str = "";
        string strParameter = "";
        GridViewExport objExport = new GridViewExport();

        strParameter = strParameter + getchkList(chkModelCodeList, "Model");
        strParameter = strParameter + getchkList(chkCategory, "Category");
        strParameter = strParameter + getchkList(chkClutchType, "ClutchType");
        strParameter = strParameter + getchkList(chkSpecialList, "Special");
        strParameter = strParameter + getchkList(chkRegion, "Region");
        str = str + "<table width='50%' border='1' cellpadding='0' cellspacing='0'>";
        str = str + "<tr><td style='font-size:small;font-weight:bold;'>Report For:</td><td style='font-size:small;font-weight:bold;'> Tractor Under Warranty</td></tr>";
        str = str + "</table>";
        str = str + strParameter;
        str = str + "<br/><table width='50%' border='1' cellpadding='0' cellspacing='0'>";
        str = str + "<tr><td >From Period:</td>";
        str = str + "<td style='font-size:small;font-weight:bold;'>" + drpFromMonth.SelectedItem.Text.ToString().Replace("Select All", "All") + "</td>";
        str = str + "<td style='font-size:small;font-weight:bold;'>" + drpFromYear.SelectedItem.Text.ToString().Replace("Select All", "All") + "</td>";
        str = str + " <td>To Period:</td>";
        str = str + "<td style='font-size:small;font-weight:bold;'>" + drpToMonth.SelectedItem.Text.ToString().Replace("Select All", "All") + "</td>";
        str = str + "<td style='font-size:small;font-weight:bold;'>" + drpToYear.SelectedItem.Text.ToString().Replace("Select All", "All") + "</td></tr>";
        str = str + "</table><br/>";
        hdnExport.Value = str + hdnExport.Value;
        objExport.ExportGridView(hdnExport.Value);

    }
    public string getchkList(CheckBoxList chkList, string chkListName)
    {

        string strParameter = "<h6>" + chkListName + "</h6> <table cellpadding='0' cellspacing='0' border='1' >";
        strParameter = strParameter + "<tr>";
        string strMiddleData = "";
        if (chkList != null)
        {
            int count = chkList.Items.Count;
            int Status = 0;
            if (chkList.Items.Count > 0)
            {
                foreach (ListItem list in chkList.Items)
                {
                    if (list.Selected)
                    {
                        Status++;
                        strMiddleData = strMiddleData + "<td> " + list.Text + " </td> ";
                    }
                }
            }
            if (Status == count)
            {
                strMiddleData = "<td> " + chkListName + " </td> <td> All </td>";
            }
        }
        strParameter = strParameter + strMiddleData;
        strParameter = strParameter + "</tr></table>";
        return strParameter;
    }

   
}
