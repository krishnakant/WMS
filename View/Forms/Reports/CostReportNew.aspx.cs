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

public partial class View_Forms_Reports_CostReportNew : System.Web.UI.Page
{
    QueryConroller objQueryController = new QueryConroller();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindModel();
            BindModelCategory();
            BindModelClutch();
            BindModelSpecial();
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


    public void BindGrid()
    {
        DataTable dtCost = getTable();
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

                    double[] Cost = new double[dtCost.Columns.Count - 1];
                    double[] Total = new double[dtCost.Columns.Count - 1];
                    drTotal["Field"] = "Total:";
                    drCost["Field"] = "Cost/Tractor:";
                    for (int i = 1; i < dtCost.Columns.Count; i++)
                    {
                        int flag = 0;
                        foreach (DataRow dr in dtCost.Rows)
                        {
                            if (flag > 0)
                            {

                                double coltot = 0;
                                try
                                {
                                    string strTest = dr[i].ToString();
                                    coltot = Convert.ToDouble(dr[i].ToString());
                                    Total[i - 1] = Total[i - 1] + coltot;

                                }
                                catch { }
                            }
                            flag++;

                        }


                        drTotal[i] = Total[i - 1];
                        double Production = Convert.ToDouble(dtCost.Rows[0][i].ToString());
                        if (Production > 0)
                        {
                            drCost[i] = Convert.ToString(System.Math.Round((Total[i - 1] / Production),2));
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
                strModelParameter += " ModelGroupName =''" + list.Value + "'' ";
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

        string strPlaceParamter = "";
        if (rdoData.SelectedValue == "0")
        {
            if (rdoAlwar.Checked)
            {
                strPlaceParamter = "1";

            }
            else
            {
                if (rdoBhopal.Checked)
                {
                    strPlaceParamter = "2 ";
                }
                else
                {
                    strPlaceParamter = "3";
                }
            }
        }
        else if (rdoData.SelectedValue == "1")
        {

            if (rdoAlwarEngine.Checked)
            {
                strPlaceParamter = "1";

            }
            else
            {
                if (rdoSimpsonEngine.Checked)
                {
                    strPlaceParamter = "2 ";
                }
                else
                {
                    strPlaceParamter = "3";
                }
            }
        }
        else
        {
            strPlaceParamter = "4";
        }
        string strYear = rdoYear.SelectedValue;
        string strQuery ="";

        //add by VD
        string listmonthvalue = "";
        int frommonthvalue =Convert.ToInt32(drpFromMonth.SelectedValue);
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
            listmonthvalue = "''" + listmonthvalue2 + "''" + "," + listmonthvalue;
            
        }
       listmonthvalue= listmonthvalue.Remove(listmonthvalue.Length - 1);
        
            //

            if (rdoType.SelectedValue == "0")
            {
                // strQuery = "exec usp_ProductionMonthWiseCost '" + strModelParameter + "','" + strModelCategoryParameter + "','" + strModelClutchParameter + "','" + strModelSpecialParameter + "','" + drpFromMonth.SelectedValue + "','" + drpToMonth.SelectedValue + "'," + rdoData.SelectedValue + "," + strPlaceParamter + ",'" + strYear + "'," + rdoPrimary.SelectedValue + ""; ;
            strQuery = "exec usp_ProductionMonthWiseCost_New '" + strModelParameter + "','" + strModelCategoryParameter + "','" + strModelClutchParameter + "','" + strModelSpecialParameter + "','(" + listmonthvalue + ")'," + rdoData.SelectedValue + "," + strPlaceParamter + ",'" + strYear + "'," + rdoPrimary.SelectedValue + ""; ;
           } 
            else
            {
                strQuery = "exec usp_QuarterProductionCostDetailsNew '" + strModelParameter + "','" + strModelCategoryParameter + "','" + strModelClutchParameter + "','" + strModelSpecialParameter + "'," + rdoData.SelectedValue + "," + strPlaceParamter + ",'" + strYear + "'," + rdoPrimary.SelectedValue + ""; ;
            }
        //usp_QuarterProductionCostDetailsNew
        DataTable dt = objQueryController.ExecuteQuery(strQuery);
        return dt;

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
        string str = "";
        string strParameter = "";
        GridViewExport objExport = new GridViewExport();

        strParameter = strParameter + getchkList(chkModelCodeList, "Model");
        strParameter = strParameter + getchkList(chkCategory, "Category");
        strParameter = strParameter + getchkList(chkClutchType, "ClutchType");
        strParameter = strParameter + getchkList(chkSpecialList, "Special");
        str = str + "<table width='50%' border='1' cellpadding='0' cellspacing='0'>";
        str = str + "<tr><td style='font-size:small;font-weight:bold;'>Report For:</td><td style='font-size:small;font-weight:bold;'>Cost Per Tractor</td></tr>";
     
        str = str + "</table>";
        str = str + strParameter;
        str = str + "<br/><table width='50%' border='1' cellpadding='0' cellspacing='0'>";
        if (rdoType.SelectedValue == "0")
        {
            str = str + "<tr><td >From Month:</td>";
            str = str + "<td style='font-size:small;font-weight:bold;'>" + drpFromMonth.SelectedItem.Text.ToString().Replace("Select All", "All") + "</td>";
            //str = str + "<td style='font-size:small;font-weight:bold;'>" + drpFromYear.SelectedItem.Text.ToString().Replace("Select All", "All") + "</td>";
            str = str + " <td>To Month:</td>";
            str = str + "<td style='font-size:small;font-weight:bold;'>" + drpToMonth.SelectedItem.Text.ToString().Replace("Select All", "All") + "</td>";
            //str = str + "<td style='font-size:small;font-weight:bold;'>" + drpToYear.SelectedItem.Text.ToString().Replace("Select All", "All") + "</td>";
        }
       
          str = str + "<td style='font-size:small;font-weight:bold;'>" + rdoType.SelectedItem.Text.ToString() + "</td></tr>";
        
        if (rdoData.SelectedValue == "0")
         {
            str = str + "<tr><td style='font-size:small;font-weight:bold;'>" + rdoData.SelectedItem.Text.ToString() + "</td></tr>";
            str = str + "<tr><td >Place:</td>";
            if (rdoAlwar.Checked)
            {
                str = str + "<td style='font-size:small;font-weight:bold;'>" + rdoAlwar.Text.ToString() + "</td>";
            }
            else if (rdoBhopal.Checked)
            {
                str = str + "<td style='font-size:small;font-weight:bold;'>" + rdoBhopal.Text.ToString() + "</td>";
            }
            else if (rdoAllPlace.Checked)
            {
                str = str + "<td style='font-size:small;font-weight:bold;'>" + rdoAllPlace.Text.ToString() + "</td>";
            }
            str = str + "</tr>";
        }
        else if (rdoData.SelectedValue == "1")
        {
            str = str + "<tr><td style='font-size:small;font-weight:bold;'>" + rdoData.SelectedItem.Text.ToString() + "</td></tr>";
            str = str + "<tr><td >Engine:</td>";
            if (rdoAlwarEngine.Checked)
            {
                str = str + "<td style='font-size:small;font-weight:bold;'>" + rdoAlwarEngine.Text.ToString() + "</td>";
            }
            else if (rdoSimpsonEngine.Checked)
            {
                str = str + "<td style='font-size:small;font-weight:bold;'>" + rdoSimpsonEngine.Text.ToString() + "</td>";
            }
            else if (rdoBothEngine.Checked)
            {
                str = str + "<td style='font-size:small;font-weight:bold;'>" + rdoBothEngine.Text.ToString() + "</td>";
            }
            str = str + "</tr>";
        }
        else
        {
            str = str + "<tr><td style='font-size:small;font-weight:bold;'>" + rdoData.SelectedItem.Text.ToString() + "</td></tr>";
        }
        str = str + "<tr><td >Year:</td>";
        str = str + "<td style='font-size:small;font-weight:bold;'>" + rdoYear.SelectedItem.Text.ToString().Replace("Select All", "All") + "</td></tr>";
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
        //
        if (ProductionMonth == 1 )
        {
            ProductionMonth = 97;
        }
        if (ProductionMonth == 2 )
        {
            ProductionMonth = 98;
        }
        if (ProductionMonth == 3 )
        {
            ProductionMonth = 99;
        }
        //
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
