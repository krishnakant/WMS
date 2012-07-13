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
using InfoSoftGlobal;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Office.Core;
using System.IO;

public partial class View_Forms_Reports_FailureReportNew : System.Web.UI.Page
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
            BindItemGroup();
            //BindItem();
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

    public void BindItemGroup()
    {
        DataTable dtItem = new DataTable();
        string strQuery = "select distinct ItemCodeGroupID,[ItemGroupName] from ItemGroup where [ItemGroupName]<>''  order by [ItemGroupName]";
        dtItem = objQueryController.ExecuteQuery(strQuery);
        if (dtItem != null)
        {
            if (dtItem.Rows.Count > 0)
            {
                drpItemGroup.DataSource = dtItem;
                drpItemGroup.DataValueField = "ItemCodeGroupID";
                drpItemGroup.DataTextField = "ItemGroupName";
                drpItemGroup.DataBind();
                //drpItemGroup.AppendDataBoundItems = true;
                ListItem list = new ListItem("Select", "0");
                drpItemGroup.Items.Insert(0, list);
                drpItemGroup.AppendDataBoundItems = false;
            }

        }

    }
    public static int ItemCount;
    public void BindItem()
    {
        ItemCount = 0;
        string strQuery = "";
        DataTable dtItem = new DataTable();
        strQuery = "select item_code+'('+Description+')' as item,item_code from acrbulk where item_code in (select ItemCode from ItemGroupMapping where ItemGroupID="+ drpItemGroup.SelectedValue +")  order by Item_Code";
        dtItem = objQueryController.ExecuteQuery(strQuery);
        if (dtItem != null)
        {
            if (dtItem.Rows.Count > 0)
            {
                Chkitemlist.DataSource = dtItem;
                Chkitemlist.DataValueField = "Item_Code";
                Chkitemlist.DataTextField = "Item";
                Chkitemlist.DataBind();
                ItemCount = Chkitemlist.Items.Count;
               
            }
            else
            {
                Chkitemlist.Items.Clear();
              
            }

        }
        else
        {
            Chkitemlist.Items.Clear();
          
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

    public void BindGrid()
    {
        DataTable dtFailure = GetTable();
        if (dtFailure != null)
        {
            if (dtFailure.Rows.Count > 0)
            {
                btnExport.Visible = true;
                grdFailureReport.DataSource = dtFailure;
                grdFailureReport.DataBind();
                DataRow drFailure = dtFailure.NewRow();

                double[] Failure = new double[dtFailure.Columns.Count - 1];
                
                drFailure["Field"] = "Failure/1000 Tractor:";

                for (int i = 1; i < dtFailure.Columns.Count; i++)
                {
                   
                    double Production = Convert.ToDouble(dtFailure.Rows[0][i].ToString());
                    if (Production > 0)
                    {
                        drFailure[i] = Convert.ToString(System.Math.Round(Convert.ToDouble(dtFailure.Rows[1][i])*1000 / Production,2));
                    }
                    else
                    {
                        drFailure[i] = "0";
                    }
                }
                
               
                dtFailure.Rows.Add(drFailure);
                grdFailureReport.DataSource = dtFailure;
                grdFailureReport.DataBind();
                foreach (GridViewRow gr in grdFailureReport.Rows)
                {
                    
                    if (gr.Cells[0].Text.Contains("Failure/1000 Tractor:"))
                    {
                        gr.BackColor = System.Drawing.Color.Aqua;
                        gr.Font.Bold = true;
                    }
                }
            }
            else
            {
                btnExport.Visible = false;
                grdFailureReport.DataSource = null;
                grdFailureReport.DataBind();
            }
        }
        else
        {
            btnExport.Visible = false;
            grdFailureReport.DataSource = null;
            grdFailureReport.DataBind();
        }
    }

    public DataTable GetTable()
    {
        string strFromMonth = drpFromMonth.SelectedValue;
        string strToMonth = drpToMonth.SelectedValue;

        string strItemParameter = "";
        if (ChkAllitemlist.Checked)
        {
            strItemParameter = " ItemGroupID=" + drpItemGroup.SelectedValue;
        }
        else
        {
            strItemParameter = " Item_Code in (";
            int itemflag = 0;
            foreach (ListItem list in Chkitemlist.Items)
            {
                if (list.Selected)
                {
                    if (itemflag > 0)
                    {
                        strItemParameter += ",";
                    }
                    strItemParameter += " ''" + list.Value + "'' ";
                    itemflag++;
                }

            }
            strItemParameter += ")";
        }
        
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
            listmonthvalue = "''" + listmonthvalue2 + "''" + "," + listmonthvalue;

        }
        listmonthvalue = listmonthvalue.Remove(listmonthvalue.Length - 1);
        //
        

        //string strQuery = "exec usp_ProductionMonthWiseFailureReport '" + strItemParameter + "','" + strModelParameter + "','" + strModelCategoryParameter + "','" + strModelClutchParameter + "','" + strModelSpecialParameter + "','" + drpFromMonth.SelectedValue + "','" + drpToMonth.SelectedValue + "','" + rdoProblemType.SelectedValue + "'";
        string strQuery = "exec usp_ProductionMonthWiseFailureReport_New '" + strItemParameter + "','" + strModelParameter + "','" + strModelCategoryParameter + "','" + strModelClutchParameter + "','" + strModelSpecialParameter + "','(" + listmonthvalue + ")','" + rdoProblemType.SelectedValue + "'";
        DataTable dtFailure = objQueryController.ExecuteQuery(strQuery);
        if (hdnReportType.Value == "0")
        {
            DataView dv = new DataView(dtFailure);
            dv.RowFilter = "Field<>'Quantity'";
            dtFailure = dv.ToTable();
        }
        else
        {
            DataView dv = new DataView(dtFailure);
            dv.RowFilter = "Field<>'Failure'";
            dtFailure = dv.ToTable();
        }
        return dtFailure;
    }

    public void eventhandlerSerialNo(object Sender, EventArgs E)
    {
        int i = 1;
        int pi = grdFailureReport.PageIndex;
        int ps = grdFailureReport.PageSize;
        //<><> Use Name of Your GridView Instead Of gvDetailProspect <><>// 
        if (pi > 0)
        {
            i = pi * ps + 1;
        }
        foreach (GridViewRow gr in grdFailureReport.Rows)
        {
            gr.Cells[0].Text = i.ToString();

            i++;
        }
    }

    protected void OnRowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            int ccount = e.Row.Cells.Count;
            e.Row.Cells[0].Text = "Production Month";
            for (int i = 1; i < ccount; i++)
            {
                int HeaderText = Convert.ToInt32(e.Row.Cells[i].Text);
                string strHeaderText ="'"+getProductionMonthYear(HeaderText);
                e.Row.Cells[i].Text = strHeaderText;
            }
        }
          
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindGrid();
        grdFailureReport.Visible = true;
        divGraph.Visible = false;
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
        str = str + "<tr><td style='font-size:small;font-weight:bold;'>Report For:</td><td style='font-size:small;font-weight:bold;'>Failure Per 1000 Tractors</td></tr>";
        str = str + "<tr><td style='font-size:small;font-weight:bold;'>" + rdoReportType.SelectedItem.Text.ToString() + "</td></tr>";
        str = str + "</table>";
        str = str + strParameter;
        str = str + "<br/><table width='50%' border='1' cellpadding='0' cellspacing='0'>";
        str = str + "<tr>";
        str = str + "<td >Item Group:</td>";
        str = str + "<td style='font-size:small;font-weight:bold;'>" + drpItemGroup.SelectedItem.Text.ToString().Replace("Select All", "All") + "</td></tr>";
        str = str + "</table>";
        str = str + getchkList(Chkitemlist, "Item");
        str = str + "<br/><table width='50%' border='1' cellpadding='0' cellspacing='0'>";
        str = str + "<tr>";
        str = str + "<td >From Month:</td>";
        str = str + "<td style='font-size:small;font-weight:bold;'>" + drpFromMonth.SelectedItem.Text.ToString().Replace("Select All", "All") + "</td>";
     
        str = str + " <td>To Month:</td>";
        str = str + "<td style='font-size:small;font-weight:bold;'>" + drpToMonth.SelectedItem.Text.ToString().Replace("Select All", "All") + "</td>";

        str = str + "<tr><td >Problem Type:</td>";
        str = str + "<td style='font-size:small;font-weight:bold;'>" + rdoProblemType.SelectedItem.Text.ToString().Replace("Select All", "All") + "</td></tr>";
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
        if (ProductionMonth == 1)
        {
            ProductionMonth = 97;
        }
        if (ProductionMonth == 2)
        {
            ProductionMonth = 98;
        }
        if (ProductionMonth == 3)
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

    protected void btnShowGraph_Click(object sender, EventArgs e)
    {
        CreateChart();
        grdFailureReport.Visible = false;
        btnExport.Visible = false;
        divGraph.Visible = true;   
       
      
    }

    public string XMLData(DataTable dt)
    {

        try
        {
            string strModel = "";
            string strItem = "";
            if (chkSelectAll.Checked)
            {
                strModel = "All";
            }
            else
            {
                int modelflag = 0;
                foreach (ListItem modellist in chkModelCodeList.Items)
                {
                    if (modellist.Selected)
                    {
                        if (modelflag > 0)
                        {
                            strModel += " , ";
                        }
                        strModel += modellist.Text;
                        modelflag++;
                    }
                }
            }

            if (ChkAllitemlist.Checked)
            {
                strItem = "All";
            }
            else
            {
                int itemflag = 0;
                foreach (ListItem itemlist in Chkitemlist.Items)
                {
                    if (itemlist.Selected)
                    {
                        if (itemflag > 0)
                        {
                            strItem += " , ";
                        }
                        strItem += itemlist.Text;
                        itemflag++;
                    }
                }
            }

            DataRow drFailure = dt.NewRow();

            double[] Failure = new double[dt.Columns.Count - 1];

            drFailure["Field"] = "Failure/1000 Tractor:";

            for (int i = 1; i < dt.Columns.Count; i++)
            {

                double Production = Convert.ToDouble(dt.Rows[0][i].ToString());
                if (Production > 0)
                {
                    drFailure[i] = Convert.ToString(System.Math.Round(Convert.ToDouble(dt.Rows[1][i]) * 1000 / Production, 2));
                }
                else
                {
                    drFailure[i] = "0";
                }
            }


            dt.Rows.Add(drFailure);
            //string xml = "<graph bgColor='FFECAA' rotateNames='1' caption='Model:" + strModel + " /Item:" + strItem + " ' yAxisName='Failure/1000 Tractors' xAxisName='Production Month' formatNumberScale='0'  >";
            string xml = "<graph caption='Failure/1000 Tractors ' decimalPrecision='0' rotateNames='1' subcaption='Model:" + strModel + " /Item:" + drpItemGroup.SelectedItem.Text + " ' formatNumberScale='0'   yAxisMinValue='15000' yAxisName='Failure/1000 Tractors' xAxisName='Production Month' showNames='1' showValues='1'  showColumnShadow='1' animation='1' showAlternateHGridColor='1' AlternateHGridColor='ff5904' divLineColor='ff5904' divLineAlpha='20' alternateHGridAlpha='5' canvasBorderColor='666666' baseFontColor='666666' lineThickness='3'  bgColor='f1f1f1'>";
            int rowFlag = 1;
            foreach (DataRow dr in dt.Rows)
            {
                if (rowFlag > 2)
                {
                    int colflag = 1;
                    foreach (DataColumn dc in dt.Columns)
                    {
                        if (colflag > 1)
                        {
                            xml = xml + "<set name='" + getProductionMonthYear(Convert.ToInt32(dc.ColumnName)) + "' value='" + dr[dc].ToString() + "' />";
                        }
                        colflag++;
                    }
                }

                rowFlag++;
            }

            
            xml += "</graph>";

            return xml;
          

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {

        }


    }

    public string CreateChart()
    {
        string strProjectName = ConfigurationManager.AppSettings["WMSProjectName"].ToString();
        //strXML will be used to store the entire XML document generated
        string strXML;


        DataTable dt = new DataTable();
        //dt = (DataTable)ViewState["dtinformation"];
        dt = GetTable();
        strXML = XMLData(dt);
        int i = 0;

        string width = "";
        string height = "";

        //Create the chart - Pie 3D Chart with data from strXML

        string strChartPath = strProjectName + "/JS/Flash/FCF_Line.swf";

        width = "950";
        height = "270";

        return FusionCharts.RenderChart(strChartPath, "", strXML, "chartid", width, height, false, true);


    }

    public void GenerateGraph()
    {
        Excel.ApplicationClass excelApplication = new Excel.ApplicationClass();
        Excel.Application obj = new Excel.Application();
        Excel.Workbook newWorkbook = null;
        Excel.Worksheet targetSheet = null;
        Excel.Range dataRange = null;
        try
        {
            string paramWorkbookPath = AppDomain.CurrentDomain.BaseDirectory + "UploadFile\\Graphs\\Failure";
            paramWorkbookPath += ".xls";

            File.Delete(paramWorkbookPath);
            object paramMissing = Type.Missing;

            object paramChartFormat = 1;
            object paramCategoryLabels = 1;
            object paramSeriesLabels = 0;

            string strTitle = "";
            object paramTitle = strTitle;
            object paramCategoryTitle = "Category Title";
            object paramValueTitle = "Value Title";

            newWorkbook = excelApplication.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
            targetSheet = (Excel.Worksheet)(newWorkbook.Worksheets[1]);
            targetSheet.Name = "Sheet1";

            // Gets the datatable containing the data
            DataTable dsData = GetTable();
            int colcount = dsData.Columns.Count;
            int rowcount = dsData.Rows.Count;
            int i = 2;

            targetSheet.Cells[1, 1] = "Production Month";
            targetSheet.Cells[1, 2] = "Failure/1000 Tractors";

            DataRow drFailure = dsData.NewRow();

            double[] Failure = new double[dsData.Columns.Count - 1];

            drFailure["Field"] = "Failure/1000 Tractor:";

            for (int k = 1; k < dsData.Columns.Count; k++)
            {

                double Production = Convert.ToDouble(dsData.Rows[0][k].ToString());
                if (Production > 0)
                {
                    drFailure[k] = Convert.ToString(System.Math.Round(Convert.ToDouble(dsData.Rows[1][k]) * 1000 / Production, 2));
                }
                else
                {
                    drFailure[k] = "0";
                }
            }


            dsData.Rows.Add(drFailure);
            int rowFlag = 1;
            foreach (DataRow dr in dsData.Rows)
            {
                if (rowFlag > 2)
                {
                    int colflag = 1;
                    foreach (DataColumn dc in dsData.Columns)
                    {
                        if (colflag > 1)
                        {
                            targetSheet.Cells[i, 1] = "'" + getProductionMonthYear(Convert.ToInt32(dc.ColumnName));
                            targetSheet.Cells[i, 2] = dr[dc].ToString();
                            i = i + 1;
                        }
                        colflag++;
                    }
                }

                rowFlag++;
            }

            object fromCell = "$A1";
            object toCell = "$B" + (colcount - 1).ToString();
            dataRange = targetSheet.get_Range(fromCell, toCell);


            /**************************************Get Column Graph()************************************************************/
            // Generating the graph
            Excel.Chart barchart;
            barchart = (Excel.Chart)newWorkbook.Charts.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            barchart.ChartType = Excel.XlChartType.xlColumnClustered;
            ColumnGraph(barchart, targetSheet, dataRange);


            /**************************************Get Line Chart()************************************************************/
            // Generating the graph
            Excel.Chart linechart;
            linechart = (Excel.Chart)newWorkbook.Charts.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            linechart.ChartType = Excel.XlChartType.xlLine;
            ColumnGraph(linechart, targetSheet, dataRange);

            newWorkbook.SaveAs(paramWorkbookPath, Excel.XlFileFormat.xlExcel9795, null, null, false, false, Excel.XlSaveAsAccessMode.xlNoChange, false, false, null, null, null);
            // Release the references to the Excel objects.


        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            dataRange = null;
            targetSheet = null;


            if (newWorkbook != null)
            {
                newWorkbook.Close(false, Type.Missing, Type.Missing);
                newWorkbook = null;
            }

            // Quit Excel and release the ApplicationClass object.
            if (excelApplication != null)
            {
                excelApplication.Quit();
                excelApplication = null;
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }

    public void ColumnGraph(Excel.Chart chart, Excel.Worksheet targetSheet, Excel.Range dataRange)
    {
        chart.SetSourceData(dataRange, Excel.XlRowCol.xlColumns);
        string strChartType = Convert.ToString(chart.ChartType);


        string strModel = "";
        string strItem = "";
        if (chkSelectAll.Checked)
        {
            strModel = "All";
        }
        else
        {
            int modelflag = 0;
            foreach (ListItem modellist in chkModelCodeList.Items)
            {
                if (modellist.Selected)
                {
                    if (modelflag > 0)
                    {
                        strModel += " , ";
                    }
                    strModel += modellist.Text;
                    modelflag++;
                }
            }
        }

        if (ChkAllitemlist.Checked)
        {
            strItem = "All";
        }
        else
        {
            int itemflag = 0;
            foreach (ListItem itemlist in Chkitemlist.Items)
            {
                if (itemlist.Selected)
                {
                    if (itemflag > 0)
                    {
                        strItem += " , ";
                    }
                    strItem += itemlist.Text;
                    itemflag++;
                }
            }
        }

        chart.HasTitle = true;
        chart.ChartTitle.Font.Bold = true;
        chart.ChartTitle.Font.Size = 15;
        chart.ChartTitle.Text = "Model: " + strModel;
        chart.ChartTitle.Text += "\n Item:" + strItem;
        chart.ChartTitle.Font.Name = "Times New Roman";
        chart.PlotArea.Fill.ForeColor.SchemeColor = 2;

        //chart.Walls.Fill.ForeColor.SchemeColor = 20;
        //chart.Walls.Fill.OneColorGradient(MsoGradientStyle.msoGradientHorizontal, 1, 0.3F);

        Excel.Axes xlAxisCategory;
        Excel.Axes xlAxisValue;
        object orient = 40;
        try
        {
            xlAxisCategory = (Excel.Axes)chart.Axes(Type.Missing, Excel.XlAxisGroup.xlPrimary);
            //xlAxisCategory = (Excel.Axes)chart.Axes(null, Excel.XlAxisGroup.xlPrimary);
            //xlAxisCategory.Item(Excel.XlAxisType.xlCategory, Excel.XlAxisGroup.xlPrimary).HasTitle = true;
            //xlAxisCategory.Item(Excel.XlAxisType.xlCategory, Excel.XlAxisGroup.xlPrimary).AxisTitle.Text = "Financial Year";
            xlAxisCategory.Item(Excel.XlAxisType.xlCategory, Excel.XlAxisGroup.xlPrimary).TickLabels.Orientation = (Excel.XlTickLabelOrientation)orient;
            xlAxisValue = (Excel.Axes)chart.Axes(Type.Missing, Excel.XlAxisGroup.xlPrimary);
            xlAxisValue.Item(Excel.XlAxisType.xlValue, Excel.XlAxisGroup.xlPrimary).HasTitle = true;
            xlAxisValue.Item(Excel.XlAxisType.xlValue, Excel.XlAxisGroup.xlPrimary).AxisTitle.Text = "Failure/1000 Tractors";

        }
        catch { }


        chart.ApplyDataLabels(Excel.XlDataLabelsType.xlDataLabelsShowValue, null, null, false, false, false, true, false, false, ",");
        chart.HasLegend = false;

        Excel.Series oSeriesBar = null;
        oSeriesBar = (Excel.Series)chart.SeriesCollection(1);
        if (strChartType != "xlLine")
        {
            oSeriesBar.Border.ColorIndex = Excel.Constants.xlNone;
        }


        DataTable dsData = GetTable();
        int colcount = dsData.Columns.Count;
        int rowcount = dsData.Rows.Count;


        int flag = 1;
        int colorflag = 1;
        int colorindex = 8;
        //float deg = 0.0F;
        //foreach (DataRow dr in dsData.Rows)
        //{

        //    Excel.Point oPoint = null;

        //    oPoint = (Excel.Point)oSeriesBar.Points(flag);
        //    oPoint.DataLabel.Font.Name = "Palatino Linotype";
        //    oPoint.DataLabel.Font.Size = 12;

        //    if (colorflag == 1)
        //    {
        //        colorindex = 8;
        //        deg = 0.3F;
        //    }
        //    else if (colorflag == 2)
        //    {
        //        colorindex = 50;
        //        deg = 0.3F;
        //    }
        //    else if (colorflag == 3)
        //    {
        //        colorindex = 4;
        //        deg = 0.3F;
        //    }
        //    else if (colorflag == 4)
        //    {
        //        colorindex = 6;
        //        deg = 0.3F;
        //    }
        //    else if (colorflag == 5)
        //    {
        //        colorindex = 22;
        //        deg = 0.3F;
        //    }
        //    else if (colorflag == 6)
        //    {
        //        colorindex = 24;
        //        deg = 0.3F;
        //    }
        //    else if (colorflag == 7)
        //    {
        //        colorindex = 39;  //26
        //        deg = 0.3F;
        //    }
        //    else if (colorflag == 7)
        //    {
        //        colorindex = 26;
        //        deg = 0.3F;
        //    }
        //    else if (colorflag == 7)
        //    {
        //        colorindex = 26;
        //        deg = 0.3F;
        //    }
        //    else if (colorflag == 7)
        //    {
        //        colorindex = 26;
        //        deg = 0.3F;
        //    }

        //    oPoint.DataLabel.Font.Bold = true;
        //    oPoint.Fill.ForeColor.SchemeColor = colorindex;
        //    oPoint.Fill.OneColorGradient(MsoGradientStyle.msoGradientHorizontal, 1, deg);


        //    colorflag++;
        //    if (colorflag > 7)
        //    {
        //        colorflag = 1;
        //    }
            flag = flag + 1;
       // }
        /**************************************************************************************************/


    }

    protected void btnExcelGraph_Click(object sender, EventArgs e)
    {
        GenerateGraph();
        string paramWorkbookPath = "/WMS/UploadFile/Graphs/Failure";
        paramWorkbookPath += ".xls";
        //File.Open(paramWorkbookPath,FileMode.Open);
        Response.Redirect(paramWorkbookPath);
    }
    protected void drpItemGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindItem();
    }
}
