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

public partial class View_Forms_Reports_ProductionSummary : System.Web.UI.Page
{
    LinkButton LnkTotal = new LinkButton();
    QueryConroller objQueryController = new QueryConroller();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindModel();
            BindModelCategory();
            BindModelClutch();
            BindModelSpecial();

            BindProductionMonth("From");
            BindProductionMonth("To");
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
                grdProdData.DataBind();

                int Total = 0;
                foreach (GridViewRow gr in grdProdData.Rows)
                {
                    Total = Total + Convert.ToInt32(((LinkButton)gr.FindControl("lnkQuantity")).Text);
                }
                GridViewRow oGridViewRow = new GridViewRow(grdProdData.Rows.Count + 1, 14, DataControlRowType.Footer, DataControlRowState.Insert);

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

                // To show Total & view Total records in popup window and SetFlag is for set Flag value
                //Flag is used for identify that which Link button is clicked Total(Flag=1) or individual Production(Flag=0)
                LnkTotal.ID = "ctl00_ContentPlaceHolder1_grdProdData_ctl02_lnktal";
                LnkTotal.Attributes.Add("href", "javascript:SetFlag('"+LnkTotal.ID+"');");
                LnkTotal.Text = Total.ToString();
                
                oTableCell.Controls.Add(LnkTotal);
                //
                
                oGridViewRow.Cells.Add(oTableCell);

                oGridViewRow.Font.Bold = true;
                oGridViewRow.BackColor = System.Drawing.Color.Aqua;
                grdProdData.Controls[0].Controls.AddAt(grdProdData.Rows.Count + 1, oGridViewRow);
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
        string strToMonth = drpToMonth.SelectedValue;
        //add by VD
        string listmonthvalue = "";
        int frommonthvalue = Convert.ToInt32(drpFromMonth.SelectedValue);
        int Tomonthvalue = Convert.ToInt32(drpToMonth.SelectedValue);
        string listmonthvalue2 = "";
        for (int iteration = frommonthvalue; iteration <= Tomonthvalue; iteration++)
        {
            listmonthvalue2 = iteration.ToString();
            if (listmonthvalue2 == "97")
            {
                listmonthvalue2 = "1"; 
            }
            if (listmonthvalue2 == "98")
            {
                listmonthvalue2 = "2";
            }
            if (listmonthvalue2 == "99")
            {
                listmonthvalue2 = "3";
            }
            listmonthvalue = "'" + listmonthvalue2 + "'" + "," + listmonthvalue;

        }
        listmonthvalue = listmonthvalue.Remove(listmonthvalue.Length - 1);
        //



        //string strQuery = "select Production_Month_Year,Count(ID) as Production from vw_ProductionReportData where (Production_Month between " + strFromMonth + " and " + strToMonth + ") and (" + strModelParameter + ") and (" + strModelCategoryParameter + ") and (" + strModelClutchParameter + ") and (" + strModelSpecialParameter + ") group by Production_Month_Year,YearID, MonthID order by YearID, MonthID";
        string strQuery = "select Production_Month_Year,Count(ID) as Production from vw_ProductionReportData where Production_Month IN (" + listmonthvalue + ") and (" + strModelParameter + ") and (" + strModelCategoryParameter + ") and (" + strModelClutchParameter + ") and (" + strModelSpecialParameter + ") group by Production_Month_Year,YearID, MonthID order by YearID, MonthID";
        DataTable dt = objQueryController.ExecuteQuery(strQuery);
        return dt;
    }

    public string getMonth(int MonthID)
    {
        string strMonth = "";
        if (MonthID == 1)
        {
            strMonth = "Jan";
        }
        else if (MonthID == 2)
        {
            strMonth = "Feb";
        }
        else if (MonthID == 3)
        {
            strMonth = "Mar";
        }
        else if (MonthID == 4)
        {
            strMonth = "Apr";
        }
        else if (MonthID == 5)
        {
            strMonth = "May";
        }
        else if (MonthID == 6)
        {
            strMonth = "Jun";
        }
        else if (MonthID == 7)
        {
            strMonth = "Jul";
        }
        else if (MonthID == 8)
        {
            strMonth = "Aug";
        }
        else if (MonthID == 9)
        {
            strMonth = "Sep";
        }
        else if (MonthID == 10)
        {
            strMonth = "Oct";
        }
        else if (MonthID == 11)
        {
            strMonth = "Nov";
        }
        else if (MonthID == 12)
        {
            strMonth = "Dec";
        }

        return strMonth;
    }

    public void grdsalesData_Paging(Object sender, GridViewPageEventArgs e)
    {
        DataTable dtGridData = getTable();
        grdProdData.PageIndex = e.NewPageIndex;
        grdProdData.DataSource = dtGridData;
        grdProdData.DataBind();

    }



    public void BindProductionMonth(string source)
    {
        DataTable dtProdMonth = new DataTable();
        string strProdMonthQuery = "select * from ProductionMonth";
        dtProdMonth = objQueryController.ExecuteQuery(strProdMonthQuery);

        int BaseProductionMonth = Convert.ToInt16(dtProdMonth.Rows[0]["BaseProductionMonth_Code"]);
        int BaseMonthID = Convert.ToInt16(dtProdMonth.Rows[0]["Month_ID"]);
        int BaseYearID = Convert.ToInt16(dtProdMonth.Rows[0]["Year_ID"]);
        string strBaseDate = Convert.ToString(BaseMonthID) + "/1/" + BaseYearID;
        DateTime BaseDate = Convert.ToDateTime(strBaseDate);
        int ProductionMonth = BaseProductionMonth;
        int ProductionMonthValue;
        for (int i = BaseProductionMonth; i < BaseProductionMonth + 200; i++)
        {
            int Offset = Convert.ToInt16(ProductionMonth) - BaseProductionMonth;
            DateTime ProdMonthYear = BaseDate.AddMonths(Offset);

            int CurrentYearID = ProdMonthYear.Year;
            int CurrentMonthID = ProdMonthYear.Month;
            int PresentYearID = System.DateTime.Now.Year;
            int PresentMonthID = System.DateTime.Now.Month;

            string strCurrentYearID = (Convert.ToString(CurrentYearID)).Substring(2, 2);
            string strPresentYearID = (Convert.ToString(PresentYearID)).Substring(2, 2);

            string strMonth = getMonth(CurrentMonthID);
            string strPresentMonth = getMonth(PresentMonthID);
            ProductionMonthValue = ProductionMonth;
            //add this condition for change the production month value 
            //ProductionMonth == 97 then it should be 1
            //ProductionMonth == 98 then it should be 2
            //ProductionMonth == 99 then it should be 3 and with the 100 it will continue 
            if (i == 97 || ProductionMonth == 97)
            {
                ProductionMonthValue = 1;
            }
            if (i == 98 || ProductionMonth == 98)
            {
                ProductionMonthValue = 2;
            }
            if (i == 99 || ProductionMonth == 99)
            {
                ProductionMonthValue = 3;
            }


            string strProductionMonthYear = strMonth + "-" + strCurrentYearID;
            string strPresentMonthYear = strPresentMonth + "-" + strPresentYearID;


            ListItem list = new ListItem(strProductionMonthYear, ProductionMonthValue.ToString());
            if (source == "From")
            {
                drpFromMonth.AppendDataBoundItems = true;
                drpFromMonth.Items.Add(list);
                drpFromMonth.AppendDataBoundItems = false;
                if (strProductionMonthYear == strPresentMonthYear)
                {
                    drpFromMonth.SelectedValue = list.Value;
                }
            }
            else
            {
                drpToMonth.AppendDataBoundItems = true;
                drpToMonth.Items.Add(list);
                drpToMonth.AppendDataBoundItems = false;
                if (strProductionMonthYear == strPresentMonthYear)
                {
                    drpToMonth.SelectedValue = list.Value;
                }
            }
            ProductionMonth++;
        }
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
        str = str + "<tr><td style='font-size:small;font-weight:bold;'>Report For:</td><td style='font-size:small;font-weight:bold;'>Production Summary</td></tr>";
        str = str + "</table>";
        str = str + strParameter;
        str = str + "<br/><table width='50%' border='1' cellpadding='0' cellspacing='0'>";
        str = str + "<tr><td >From Month:</td>";
        str = str + "<td style='font-size:small;font-weight:bold;'>" + drpFromMonth.SelectedItem.Text.ToString().Replace("Select All", "All") + "</td>";
        //str = str + "<td style='font-size:small;font-weight:bold;'>" + drpFromYear.SelectedItem.Text.ToString().Replace("Select All", "All") + "</td>";
        str = str + " <td>To Month:</td>";
        str = str + "<td style='font-size:small;font-weight:bold;'>" + drpToMonth.SelectedItem.Text.ToString().Replace("Select All", "All") + "</td>";
        //str = str + "<td style='font-size:small;font-weight:bold;'>" + drpToYear.SelectedItem.Text.ToString().Replace("Select All", "All") + "</td>";
        //str = str + "<td >Region:</td>";
        //str = str + "<td style='font-size:small;font-weight:bold;'>" + drpRegion.SelectedItem.Text.ToString().Replace("Select All", "All") + "</td></tr>";
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
