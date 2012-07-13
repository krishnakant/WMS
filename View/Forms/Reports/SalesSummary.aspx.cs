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

public partial class View_Forms_Reports_SalesSummary : System.Web.UI.Page
{
    QueryConroller objQueryController = new QueryConroller();
    private GridViewHelper helper; 

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
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
                chkModelCodeList.DataValueField = "ModelGroupName";
                chkModelCodeList.DataTextField = "ModelGroupName";
                chkModelCodeList.DataBind();
            }

        }

    }

    public void BindRegion()
    {
        string strQuery = "";
        DataTable dtRegion = new DataTable();
        strQuery = "select * from Region  order by RegionID";
        dtRegion = objQueryController.ExecuteQuery(strQuery);
        if (dtRegion != null)
        {
            if (dtRegion.Rows.Count > 0) 
            {
                drpRegion.DataSource = dtRegion;
                drpRegion.DataValueField = "RegionID";
                drpRegion.DataTextField = "Region";
                drpRegion.DataBind();
                drpRegion.AppendDataBoundItems = true;
                ListItem list = new ListItem("Select", "-1");
                drpRegion.Items.Insert(0, list);
                ListItem list1 = new ListItem("All", "0");
                drpRegion.Items.Insert(1, list1);
                drpRegion.AppendDataBoundItems = false;

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


    public void eventhandlerSerialNo(object Sender, EventArgs E)
    {
        int i = 1;
        int pi = grdProdData.PageIndex;
        int ps = grdProdData.PageSize;
        if (pi > 0)
        {
            i = pi * ps + 1;
        }
        foreach (GridViewRow gr in grdProdData.Rows)
        {
            gr.Cells[0].Text = i.ToString();

            i++;
        }
    }


    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

    public void BindGrid()
    {

        DataTable dt = getTable();
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                btnExport.Visible = true;
                grdProdData.DataSource = dt;

                //helper = new GridViewHelper(this.grdProdData);
                //helper.RegisterGroup("Group", true, true);
                //helper.GroupSummary += helper_GroupSummary;
                //helper.RegisterSummary("Quantity", SummaryOperation.Sum, "Group");
                grdProdData.DataBind();
                int Total = 0;
                foreach (GridViewRow gr in grdProdData.Rows)
                {
                    Total = Total + Convert.ToInt32(((LinkButton)gr.FindControl("lnkQuantity")).Text);
                }
                GridViewRow oGridViewRow = new GridViewRow(grdProdData.Rows.Count+1, 14, DataControlRowType.Footer, DataControlRowState.Insert);

                TableCell oTableCell = new TableCell();
                oTableCell.Text = "";
                oTableCell.HorizontalAlign = HorizontalAlign.Center;
                oGridViewRow.Cells.Add(oTableCell);

                oTableCell = new TableCell();
                oTableCell.Text = "Total:";
                oTableCell.HorizontalAlign = HorizontalAlign.Center;
                oGridViewRow.Cells.Add(oTableCell);

                oTableCell = new TableCell();
                oTableCell.Text = Total.ToString();
                oTableCell.HorizontalAlign = HorizontalAlign.Center;
                oGridViewRow.Cells.Add(oTableCell);

                oGridViewRow.Font.Bold = true;
                oGridViewRow.BackColor = System.Drawing.Color.Aqua;
                grdProdData.Controls[0].Controls.AddAt(grdProdData.Rows.Count+1, oGridViewRow);
                //grdProdData.DataBind();
            }
            else
            {
                btnExport.Visible = false;
                grdProdData.DataSource = null;
                grdProdData.DataBind();
            }
        }
        else
        {
            btnExport.Visible = false;
            grdProdData.DataSource = null;
            grdProdData.DataBind();
        }
    }

    private void helper_GroupSummary(string groupName, object[] values, GridViewRow row)
    {
        row.Cells[0].HorizontalAlign = HorizontalAlign.Right;
        row.Cells[0].Text = "Total:";
        row.Cells[0].ForeColor = System.Drawing.Color.Black;
        row.BackColor = System.Drawing.Color.Aqua;
        row.Font.Bold = true;
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
                strModelParameter += " ModelGroupName ='" + list.Value + "' ";
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
        string strFromMonth = drpFromMonth.SelectedValue;
        string strFromYear = drpFromYear.SelectedItem.Text;
        string strToMonth = drpToMonth.SelectedValue;
        string strToYear = drpToYear.SelectedItem.Text;

        //Select Sales_Month,Sales_Year,Sum(Quantity) as Quantity from (Select Month(Date) as Sales_Month,Year(Date) as Sales_Year,Sum(Quantity) as Quantity from vw_dealersalesdata group by Date)A group by Sales_Month,Sales_Year
        DateTime FromDate = Convert.ToDateTime(strFromMonth + "/01/" + strFromYear);
        string strFromDate = FromDate.ToString();        
        DateTime ToDate = Convert.ToDateTime(strToMonth + "/01/" + strToYear);
        ToDate = ToDate.AddMonths(1);
        ToDate = ToDate.AddDays(-1);
        string strToDate = ToDate.ToString();
        string strQuery = "";
        if (drpRegion.SelectedValue == "0")
        {
            strQuery = "Select 'All' as [Group],Sales_Month,Sales_Year,Sales_Period,Sum(Quantity) as Quantity from ( Select Month(Date) as Sales_Month,Year(Date) as Sales_Year,Datename(Month,Date)+'-'+Convert(varchar,Year(Date)) as Sales_Period,Sum(Quantity) as Quantity from vw_dealersalesdata where (Date between '" + strFromDate + "' and '" + strToDate + "') and (" + strModelParameter + ") and (" + strModelCategoryParameter + ") and (" + strModelClutchParameter + ") and (" + strModelSpecialParameter + ") group by Date )A group by Sales_Period,Sales_Year,Sales_Month order by Sales_Year,Sales_Month";
        }
        else
        {
            strQuery = "Select 'All' as [Group],Sales_Month,Sales_Year,Sales_Period,Sum(Quantity) as Quantity from ( Select Month(Date) as Sales_Month,Year(Date) as Sales_Year,Datename(Month,Date)+'-'+Convert(varchar,Year(Date)) as Sales_Period,Sum(Quantity) as Quantity from vw_dealersalesdata where (Date between '" + strFromDate + "' and '" + strToDate + "') and (" + strModelParameter + ") and (" + strModelCategoryParameter + ") and (" + strModelClutchParameter + ") and (" + strModelSpecialParameter + ") and (RegionID=" + drpRegion.SelectedValue + ") group by Date )A group by Sales_Period,Sales_Year,Sales_Month order by Sales_Year,Sales_Month";
        }
       DataTable dt = objQueryController.ExecuteQuery(strQuery);
        return dt;
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
        str = str + "<table width='50%' border='1' cellpadding='0' cellspacing='0'>";
        str = str + "<tr><td style='font-size:small;font-weight:bold;'>Report For:</td><td style='font-size:small;font-weight:bold;'>Sales Summary</td></tr>";
        str = str + "</table>";
        str = str + strParameter;
        str = str + "<br/><table width='50%' border='1' cellpadding='0' cellspacing='0'>";
        str = str + "<tr><td >From Period:</td>";
        str = str + "<td style='font-size:small;font-weight:bold;'>" + drpFromMonth.SelectedItem.Text.ToString().Replace("Select All", "All") + "</td>";
        str = str + "<td style='font-size:small;font-weight:bold;'>" + drpFromYear.SelectedItem.Text.ToString().Replace("Select All", "All") + "</td>";
        str = str + " <td>To Period:</td>";
        str = str + "<td style='font-size:small;font-weight:bold;'>" + drpToMonth.SelectedItem.Text.ToString().Replace("Select All", "All") + "</td>";
        str = str + "<td style='font-size:small;font-weight:bold;'>" + drpToYear.SelectedItem.Text.ToString().Replace("Select All", "All") + "</td>";
        str = str + "<td >Region:</td>";
        str = str + "<td style='font-size:small;font-weight:bold;'>" + drpRegion.SelectedItem.Text.ToString().Replace("Select All", "All") + "</td></tr>";
        str = str + "</table><br/>";

         hdnExport.Value = str + hdnExport.Value;
        //hdnExport.Value = objExport.RemoveComponent("id=ctl00_ContentPlaceHolder1_chk", "checkbox ", hdnExport.Value);
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
