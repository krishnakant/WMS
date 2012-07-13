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

public partial class View_Forms_Reports_FailureReport : System.Web.UI.Page
{
    QueryConroller objQueryController = new QueryConroller();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getModel();
            BindItem();
            BindProductionMonth("From");
            BindProductionMonth("To");
        }
    }

    public void getModel()
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

    public void BindItem()
    {

        string strQuery = "";
        DataTable dtItem = new DataTable();
        strQuery = "select * from Item  order by Code";
        dtItem = objQueryController.ExecuteQuery(strQuery);
        if (dtItem != null)
        {
            if (dtItem.Rows.Count > 0)
            {
              Chkitemlist.DataSource = dtItem;
              Chkitemlist.DataValueField = "Code";
              Chkitemlist.DataTextField = "Code";
              Chkitemlist.DataBind();
            }

        }

    }

    //public void BindItem()
    //{
    //    string strQuery = "";
    //    DataTable dtItem = new DataTable();
    //    strQuery = "select * from Item  order by Code";
    //    dtItem = objQueryController.ExecuteQuery(strQuery);
    //    if (dtItem != null)
    //    {
    //        if (dtItem.Rows.Count > 0)
    //        {
    //            drpItem.DataSource = dtItem;

    //            drpItem.DataTextField = "Code";
    //            drpItem.DataValueField = "Code";
    //            drpItem.DataBind();
    //            drpItem.AppendDataBoundItems = true;
    //            ListItem list = new ListItem("Select", "0");
    //            drpItem.Items.Insert(0, list);
    //            drpItem.AppendDataBoundItems = false;

    //        }
    //    }
    //}

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
        string strModelList = "";
        string strItemList = "";
        if (chkSelectAll.Checked)
        {
            strModelList = "All";
        }
        else
        {
            foreach (ListItem list in chkModelCodeList.Items)
            {
                if (list.Selected)
                {
                    string Model = list.Text;
                    if (strModelList == "")
                    {

                        strModelList = "''" + list.Text + "''";
                    }
                    else
                    {
                        strModelList = strModelList + "," + "''" + list.Text + "''";
                    }
                }
            }
        }
        if (ChkAllitemlist.Checked)
        {
            strItemList = "All";
        }
        else
        {
            foreach (ListItem list1 in Chkitemlist.Items)
            {
                if (list1.Selected)
                {
                    string Model = list1.Text;
                    if (strItemList == "")
                    {

                        strItemList = "''" + list1.Text + "''";
                    }
                    else
                    {
                        strItemList = strItemList + "," + "''" + list1.Text + "''";
                    }
                }
            }
        }
        string strQuery = "exec  usp_ProductionFailuremutipleNew '" + strModelList + "','" + strItemList + "','" + strFromMonth + "','" + strToMonth + "'";
        DataTable dtFailure = objQueryController.ExecuteQuery(strQuery);
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
            //string xml = "<graph bgColor='FFECAA' rotateNames='1' caption='Model:" + strModel + " /Item:" + strItem + " ' yAxisName='Failure/1000 Tractors' xAxisName='Production Month' formatNumberScale='0'  >";
            string xml = "<graph caption='Failure/1000 Tractors ' decimalPrecision='0' rotateNames='1' subcaption='Model:" + strModel + " /Item:" + strItem + " ' formatNumberScale='0'   yAxisMinValue='15000' yAxisName='Failure/1000 Tractors' xAxisName='Production Month' showNames='1' showValues='1'  showColumnShadow='1' animation='1' showAlternateHGridColor='1' AlternateHGridColor='ff5904' divLineColor='ff5904' divLineAlpha='20' alternateHGridAlpha='5' canvasBorderColor='666666' baseFontColor='666666' lineThickness='3'  bgColor='f1f1f1'>";
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
        float deg = 0.0F;
        foreach (DataRow dr in dsData.Rows)
        {

            Excel.Point oPoint = null;

            oPoint = (Excel.Point)oSeriesBar.Points(flag);
            oPoint.DataLabel.Font.Name = "Palatino Linotype";
            oPoint.DataLabel.Font.Size = 12;

            if (colorflag == 1)
            {
                colorindex = 8;
                deg = 0.3F;
            }
            else if (colorflag == 2)
            {
                colorindex = 50;
                deg = 0.3F;
            }
            else if (colorflag == 3)
            {
                colorindex = 4;
                deg = 0.3F;
            }
            else if (colorflag == 4)
            {
                colorindex = 6;
                deg = 0.3F;
            }
            else if (colorflag == 5)
            {
                colorindex = 22;
                deg = 0.3F;
            }
            else if (colorflag == 6)
            {
                colorindex = 24;
                deg = 0.3F;
            }
            else if (colorflag == 7)
            {
                colorindex = 39;  //26
                deg = 0.3F;
            }
            else if (colorflag == 7)
            {
                colorindex = 26;
                deg = 0.3F;
            }
            else if (colorflag == 7)
            {
                colorindex = 26;
                deg = 0.3F;
            }
            else if (colorflag == 7)
            {
                colorindex = 26;
                deg = 0.3F;
            }

            oPoint.DataLabel.Font.Bold = true;
            oPoint.Fill.ForeColor.SchemeColor = colorindex;
            oPoint.Fill.OneColorGradient(MsoGradientStyle.msoGradientHorizontal, 1, deg);


            colorflag++;
            if (colorflag > 7)
            {
                colorflag = 1;
            }
            flag = flag + 1;
        }
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
}
