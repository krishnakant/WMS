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

public partial class View_Forms_Reports_CostReport : System.Web.UI.Page
{
    QueryConroller objQueryController = new QueryConroller();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindModel();
            if (rdoReportType.SelectedValue == "0")
            {
                BindProductionMonth("From");
                BindProductionMonth("To");
            }
            
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


    public void BindGrid()
    {
        string strModelCode = drpModel.SelectedItem.Text;
        string strFromMonth = drpFromMonth.SelectedValue;
        string strToMonth = drpToMonth.SelectedValue;
        string strQuery = "";
        if (rdoReportType.SelectedValue == "0")
        {
            strQuery = "exec usp_ProductionCostDetailsNew '" + strModelCode + "'," + strFromMonth + "," + strToMonth;
        }
        else
        {
            strQuery = "exec usp_QuarterProductionCostDetails '" + strModelCode + "'";
        }
        DataTable dtCost = objQueryController.ExecuteQuery(strQuery);
        if (dtCost != null)
        {
            if (dtCost.Rows.Count > 0)
            {
                btnExport.Visible = true;
                grdCostReport.DataSource = dtCost;
                grdCostReport.DataBind();
                if (rdoReportType.SelectedValue == "0")
                {
                    DataRow drTotal = dtCost.NewRow();
                    DataRow drCost = dtCost.NewRow();

                    int[] Cost = new int[dtCost.Columns.Count - 1];
                    int[] Total = new int[dtCost.Columns.Count - 1];
                    drTotal["Field"] = "Total:";
                    drCost["Field"] = "Cost/Tractor:";
                    for (int i = 1; i < dtCost.Columns.Count; i++)
                    {
                        int flag = 0;
                        foreach (DataRow dr in dtCost.Rows)
                        {
                            if (flag > 0)
                            {

                                int coltot = 0;
                                try
                                {
                                    string strTest = dr[i].ToString();
                                    coltot = Convert.ToInt32(dr[i].ToString());
                                    Total[i - 1] = Total[i - 1] + coltot;

                                }
                                catch { }
                            }
                            flag++;

                        }


                        drTotal[i] = Total[i - 1];
                        int Production = Convert.ToInt32(dtCost.Rows[0][i].ToString());
                        if (Production > 0)
                        {
                            drCost[i] = Convert.ToString(Total[i - 1] / Production);
                        }
                        else
                        {
                            drCost[i] = "0";
                        }
                    }
                    dtCost.Rows.Add(drTotal);
                    dtCost.Rows.Add(drCost);
                    grdCostReport.DataSource = dtCost;
                    grdCostReport.DataBind();
                    foreach (GridViewRow gr in grdCostReport.Rows)
                    {
                        if (!(gr.Cells[0].Text.Contains("Production") || gr.Cells[0].Text.Contains("Total:") || gr.Cells[0].Text.Contains("Cost/Tractor:")))
                        {
                            string[] strMonthYear = gr.Cells[0].Text.Split('-');
                            int Month = Convert.ToInt32(strMonthYear[0]);
                            string strMonth = getMonth(Month);
                            gr.Cells[0].Text = strMonth + '-' + strMonthYear[1];
                        }
                        if (gr.Cells[0].Text.Contains("Total:") || gr.Cells[0].Text.Contains("Cost/Tractor:"))
                        {
                            gr.BackColor = System.Drawing.Color.Aqua;
                            gr.Font.Bold = true;
                        }
                    }
                }
            }
            else
            {
                btnExport.Visible = false;
                grdCostReport.DataSource = null;
                grdCostReport.DataBind();
            }
        }
        else
        {
            btnExport.Visible = false;
            grdCostReport.DataSource = null;
            grdCostReport.DataBind();
        }
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
            if (i == 97 || ProductionMonth==97)
            {
                ProductionMonthValue = 1;
            }
            if (i == 98 || ProductionMonth == 98 )
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

    protected void OnRowCreated(object sender, GridViewRowEventArgs e)
    {
        if (rdoReportType.SelectedValue == "0")
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                int ccount = e.Row.Cells.Count;
                e.Row.Cells[0].Text = "Production Month";
                for (int i = 1; i < ccount; i++)
                {
                    string strTest = e.Row.Cells[i].Text;
                    try
                    {
                        int HeaderText = Convert.ToInt32(e.Row.Cells[i].Text);
                        string strHeaderText = "'" + getProductionMonthYear(HeaderText);
                        e.Row.Cells[i].Text = strHeaderText;
                    }
                    catch
                    {
                    }
                }
            }
        }
          
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        GridViewExportUtil objExport = new GridViewExportUtil();
        objExport.ExportGridView(hdnExport.Value, "");
    }

    public string getProductionMonthYear(int ProductionMonth)
    {
        //string strProductionMonthYear = "";
        DataTable dtProdMonth = new DataTable();
        string strProdMonthQuery = "select * from ProductionMonth";
        dtProdMonth = objQueryController.ExecuteQuery(strProdMonthQuery);

        int BaseProductionMonth = Convert.ToInt16(dtProdMonth.Rows[0]["BaseProductionMonth_Code"]);
        int BaseMonthID = Convert.ToInt16(dtProdMonth.Rows[0]["Month_ID"]);
        int BaseYearID = Convert.ToInt16(dtProdMonth.Rows[0]["Year_ID"]);
        string strBaseDate = Convert.ToString(BaseMonthID) + "/1/" + BaseYearID;
        DateTime BaseDate = Convert.ToDateTime(strBaseDate);
        int Offset = Convert.ToInt16(ProductionMonth) - BaseProductionMonth;
        DateTime ProdMonthYear = BaseDate.AddMonths(Offset);

        int CurrentYearID = ProdMonthYear.Year;
        int CurrentMonthID = ProdMonthYear.Month;

        string strCurrentYearID = (Convert.ToString(CurrentYearID)).Substring(2, 2);

        string strMonth = getMonth(CurrentMonthID);

        string strProductionMonthYear = strMonth + "-" + strCurrentYearID;

        return strProductionMonthYear;
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

}
