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
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Office.Core;
using Microsoft.Office;
using InfoSoftGlobal;
using System.IO;

public partial class View_Forms_Graphs_MonthWiseCostGraph : System.Web.UI.Page
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
                chkCategory.SelectedValue = "1";
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
        grdCostReport.Columns.Clear();
        DataTable dtCost = getTable();
        if (dtCost != null)
        {
            if (dtCost.Rows.Count > 0)
            {
                btnExport.Visible = true;
                grdCostReport.DataSource = dtCost;
                BoundField bnds = new BoundField();
                bnds.HeaderText = "#";
                bnds.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                grdCostReport.Columns.Add(bnds);

                if (ddlselection.SelectedValue == "1")
                {
                    if (rdoReportType.SelectedValue == "0")
                    {
                        BoundField bnd1 = new BoundField();
                        bnd1.DataField = "Cost_Per_Tractor";
                        bnd1.HeaderText = "Cost/Tractor";
                        bnd1.HtmlEncode = false;
                        bnd1.DataFormatString = "{0:F2}";
                        bnd1.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                        grdCostReport.Columns.Add(bnd1);
                    }
                    else
                    {
                        BoundField bnd3 = new BoundField();
                        bnd3.DataField = "Failure_Per_K_Tractor";
                        bnd3.HeaderText = "Defect/1000 Tractors";
                        bnd3.HtmlEncode = false;
                        bnd3.DataFormatString = "{0:F2}";
                        bnd3.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                        grdCostReport.Columns.Add(bnd3);
                    }
                    BoundField bnd2 = new BoundField();
                    bnd2.DataField = "MonthYear";
                    bnd2.HeaderText = "Period";
                    grdCostReport.Columns.Add(bnd2);

                }
                else if (ddlselection.SelectedValue == "2")
                {
                }
                else
                {
                }
                grdCostReport.DataBind();
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

    public void eventhandlerSerialNo(object Sender, EventArgs E)
    {
        int i = 1;
        int pi = grdCostReport.PageIndex;
        int ps = grdCostReport.PageSize;
        //<><> Use Name of Your GridView Instead Of grdCostReport <><>// 
        if (pi > 0)
        {
            i = pi * ps + 1;
        }
        foreach (GridViewRow gr in grdCostReport.Rows)
        {
            gr.Cells[0].Text = i.ToString();

            i++;
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
                strModelParameter += " GroupID =" + list.Value + "";
                modelflag++;
            }

        }

        string strModelCategoryParameter = "";
        //int modelcategoryflag = 0;
        //foreach (ListItem list in chkCategory.Items)
        //{
        //    if (list.Selected)
        //    {
        //        if (modelcategoryflag > 0)
        //        {
        //            strModelCategoryParameter += " or ";
        //        }
        //        strModelCategoryParameter += " ModelCategoryID =" + list.Value;
        //        modelcategoryflag++;
        //    }

        //}

        strModelCategoryParameter = " ModelCategoryID=" + chkCategory.SelectedValue;
        

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
        string strProblemParameter = "";
        if (rdoPrimary.Checked)
        {
            strProblemParameter = "1";
        }
        else if (rdoConsequences.Checked)
        {
            strProblemParameter = "2";
        }
        else
        {
            strProblemParameter = "0";
        }


        string strHMRParameter = "";
        if (rdoLessThan250.Checked)
        {
            strHMRParameter = "0";
        }
        else if (rdoMoreThan250.Checked)
        {
            strHMRParameter = "1";
        }
        else
        {
            strHMRParameter = "2";
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

        string strEngineParameter = "";

        if (rdoData.SelectedValue == "0")
        {
            if (rdoAlwar.Checked)
            {
                strEngineParameter = "  (IsEngine=1 and Engine='A')";
            }
            else
            {
                if (rdoBhopal.Checked)
                {
                    strEngineParameter = "((Engine='A' and IsEngine=0) or Engine='s') ";
                }
                else
                {
                    strEngineParameter = "(IsEngine=0 or IsEngine=1)  ";
                }
            }

        }
        else if (rdoData.SelectedValue == "1")
        {
            if (rdoAlwarEngine.Checked)
            {
                strEngineParameter = "  (IsEngine=1 and Engine='A')";
            }
            else
            {
                if (rdoSimpsonEngine.Checked)
                {
                    strEngineParameter = " (Engine='S' and IsEngine=1) ";
                }
                else
                {
                    strEngineParameter = " (IsEngine=1)  ";
                }
            }

        }
        else
        {
            strEngineParameter = " (IsEngine=0)  ";
        }
        string strYear = rdoYear.SelectedValue;
        string strQuery ="";


        strQuery = "exec usp_MonthWiseDefectGraph '" + strModelParameter + "','" + strModelCategoryParameter + "','" + strModelClutchParameter + "','" + strModelSpecialParameter + "','" + strProblemParameter + "','" + drpFromMonth.SelectedValue + "','" + drpFromYear.SelectedItem.Text + "','" + drpToMonth.SelectedValue + "','" + drpToYear.SelectedItem.Text + "'," + strHMRParameter + ",'" + strEngineParameter + "','" + strYear + "',''"; ;
     
        //usp_QuarterProductionCostDetailsNew
        DataTable dt = objQueryController.ExecuteQuery(strQuery);
        return dt;

    }

  

   
    protected void btnShow_Click(object sender, EventArgs e)
    {
       
        rptChart.Visible = false;
        rptExcelChart.Visible = false;
        grdCostReport.Visible = true;
        BindGrid();
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        GridViewExportUtil objExport = new GridViewExportUtil();
        objExport.ExportGridView(hdnExport.Value, "");
    }



    protected void btnViewGraph_Click(object sender, EventArgs e)
    {
        rptChart.Visible = true;
        rptExcelChart.Visible = false;
        grdCostReport.Visible = false;
        DataTable dtinformation = new DataTable();
        dtinformation = getTable();
        ViewState.Add("dtinformation", dtinformation);
        
    }
    public string XMLData(DataTable dt)
    {

        try
        {
            string strCaption = chkCategory.SelectedItem.Text + " Products";
            string strSubCaption = "";
            if (rdoReportType.SelectedValue == "0")
            {
                strSubCaption = "Cost/Tractor";
            }
            else
            {
                strSubCaption = "Failure/1000 Tractor";
            }

            if (rdoLessThan250.Checked)
            {
                strSubCaption += " 0-250 Hours";
            }
            else if (rdoMoreThan250.Checked)
            {
                strSubCaption += " 251-2500 Hours";
            }
            if (rdoData.SelectedValue == "0")
            {
            }
            else if (rdoData.SelectedValue == "1")
            {
                strSubCaption += "(Engine)";
            }
            else
            {
                strSubCaption += "(Tractor)";
            }


            string xml = "<graph bgColor='FFECAA' rotateNames='1' caption='"+ strCaption +"' subCaption='"+ strSubCaption +"' yAxisName='Amount' xAxisName='Period' formatNumberScale='0'  >";
            foreach (DataRow dr in dt.Rows)
            {
                string strAmount = "";

                if (rdoReportType.SelectedValue == "0")
                {
                    strAmount = dr["Cost_Per_Tractor"].ToString();
                }
                else
                {
                    strAmount = dr["Failure_Per_K_Tractor"].ToString();
                }
                xml = xml + "<set name='" + dr["MonthYear"].ToString() + "' value='" + strAmount + "' />";

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
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        rptChart.Visible = false;
        rptExcelChart.Visible = true;
        grdCostReport.Visible = false;
        GenerateGraph();
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





            string paramWorkbookPath = AppDomain.CurrentDomain.BaseDirectory + "UploadFile\\Graphs\\Summary";
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
            DataTable dsData = getTable();
            int colcount = dsData.Columns.Count;
            int rowcount = dsData.Rows.Count;
            int i = 2;

            targetSheet.Cells[1, 1] = "Financial Year";
            targetSheet.Cells[1, 2] = "Value";

            // Outputting the data
            foreach (DataRow dr in dsData.Rows)
            {
                //targetSheet.Cells[i, 1] = dr["Fyear"];
                targetSheet.Cells[i, 1] = "'" + dr["MonthYear"];
                if (rdoReportType.SelectedValue == "0")
                {
                    targetSheet.Cells[i, 2] = dr["Cost_Per_Tractor"].ToString();
                }
                else
                {
                    targetSheet.Cells[i, 2] = dr["Failure_Per_K_Tractor"].ToString();
                }
                //targetSheet.Cells[i, 2] = dr["DefectAmount"];
                // Going to the next row
                i = i + 1;
            }
            object fromCell = "$A1";
            object toCell = "$B" + (rowcount + 1).ToString();
            dataRange = targetSheet.get_Range(fromCell, toCell);


            /**************************************Get Column Graph()************************************************************/
            // Generating the graph
            Excel.Chart barchart;
            barchart = (Excel.Chart)newWorkbook.Charts.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            barchart.ChartType = Excel.XlChartType.xlColumnClustered;
            ColumnGraph(barchart, targetSheet, dataRange);


            /**************************************Get pie Graph()************************************************************/
            // Generating the graph
            Excel.Chart piechart;

            piechart = (Excel.Chart)newWorkbook.Charts.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            piechart.ChartType = Excel.XlChartType.xlPie;
            PieGraph(piechart, targetSheet, dataRange);

            /**************************************Get Line Chart()************************************************************/
            // Generating the graph
            Excel.Chart linechart;
            linechart = (Excel.Chart)newWorkbook.Charts.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            linechart.ChartType = Excel.XlChartType.xlLine;
            ColumnGraph(linechart, targetSheet, dataRange);

            /**************************************Get Doughnut Graph()************************************************************/
            // Generating the graph
            Excel.Chart doughchart;
            doughchart = (Excel.Chart)newWorkbook.Charts.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            doughchart.ChartType = Excel.XlChartType.xlDoughnut;
            PieGraph(doughchart, targetSheet, dataRange);

            newWorkbook.SaveAs(paramWorkbookPath, Excel.XlFileFormat.xlHtml, null, null, false, false, Excel.XlSaveAsAccessMode.xlNoChange, false, false, null, null, null);
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


    public string CreateChart()
    {



        string strProjectName = ConfigurationManager.AppSettings["WMSProjectName"].ToString();
        //strXML will be used to store the entire XML document generated
        string strXML;


        DataTable dt = new DataTable();
        //dt = (DataTable)ViewState["dtinformation"];
        dt = getTable();
        strXML = XMLData(dt);
        int i = 0;

        string width = "";
        string height = "";

        //Create the chart - Pie 3D Chart with data from strXML

        string strChartPath = strProjectName + "/JS/Flash/Column3DTest.swf";

        width = "100%";
        height = "270";

        return FusionCharts.RenderChart(strChartPath, "", strXML, "chartid", width, height, false, true);


    }


    public void ColumnGraph(Excel.Chart chart, Excel.Worksheet targetSheet, Excel.Range dataRange)
    {

        chart.SetSourceData(dataRange, Excel.XlRowCol.xlColumns);
        string strChartType = Convert.ToString(chart.ChartType);
        string strReportType = "";
        if (rdoReportType.SelectedValue == "0")
        {
            strReportType = "Cost/Tractor";
        }
        else
        {
            strReportType = "Failure/1000 Tractors";
        }


        string strHMR = "";
        if (rdoHMRAll.Checked)
        {
            strHMR = "All";
        }
        else if (rdoLessThan250.Checked)
        {
            strHMR = "0 to 250 Hours";
        }
        else
        {
            strHMR = "251 to 2500 Hours";
        }
        string strYear = "";
        if (rdoYear.SelectedIndex == 0)
        {
            strYear = "I Year";
        }
        else if (rdoYear.SelectedIndex == 1)
        {
            strYear = "II Year";
        }
        else
        {
            strYear = "Total";
        }


        chart.HasTitle = true;
        //chart.ChartTitle.Font.Bold = true;
        chart.ChartTitle.Font.Size = 15;
        //chart.ChartTitle.Text = "Model: " + strModel;
        chart.ChartTitle.Text = chkCategory.SelectedItem.Text;
        chart.ChartTitle.Text += "" + strReportType + " / Tractor ";
        chart.ChartTitle.Text += "| HMR:" + strHMR;
        //chart.ChartTitle.Text += "\n " + strYear;
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
            xlAxisValue.Item(Excel.XlAxisType.xlValue, Excel.XlAxisGroup.xlPrimary).AxisTitle.Text = "Rs.";

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


        DataTable dsData = getTable();
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

    public void PieGraph(Excel.Chart chart, Excel.Worksheet targetSheet, Excel.Range dataRange)
    {
        //chart.ChartType = Excel.XlChartType.xl3DPie;
        chart.SetSourceData(dataRange, Excel.XlRowCol.xlColumns);

        string strChartType = Convert.ToString(chart.ChartType);
        string strReportType = "";
        if (rdoReportType.SelectedValue == "0")
        {
            strReportType = "Cost/Tractor";
        }
        else
        {
            strReportType = "Failure/1000 Tractors";
        }


        string strHMR = "";
        if (rdoHMRAll.Checked)
        {
            strHMR = "All";
        }
        else if (rdoLessThan250.Checked)
        {
            strHMR = "0 to 250 Hours";
        }
        else
        {
            strHMR = "251 to 2500 Hours";
        }
        string strYear = "";
        if (rdoYear.SelectedIndex == 0)
        {
            strYear = "I Year";
        }
        else if (rdoYear.SelectedIndex == 1)
        {
            strYear = "II Year";
        }
        else
        {
            strYear = "Total";
        }


        chart.HasTitle = true;
        //chart.ChartTitle.Font.Bold = true;
        chart.ChartTitle.Font.Size = 15;
        //chart.ChartTitle.Text = "Model: " + strModel;
        chart.ChartTitle.Text = chkCategory.SelectedItem.Text;
        chart.ChartTitle.Text += "" + strReportType + " / Tractor ";
        chart.ChartTitle.Text += "| HMR:" + strHMR;
        //chart.ChartTitle.Text += "\n " + strYear;
        chart.ChartTitle.Font.Name = "Times New Roman";
        chart.PlotArea.Fill.ForeColor.SchemeColor = 2;
        chart.PlotArea.Border.ColorIndex = Excel.Constants.xlNone;




        chart.ApplyDataLabels(Excel.XlDataLabelsType.xlDataLabelsShowValue, null, null, false, false, false, true, false, false, ",");
        chart.HasLegend = true;

        Excel.Series oSeriesBar = null;
        oSeriesBar = (Excel.Series)chart.SeriesCollection(1);
        oSeriesBar.Border.ColorIndex = Excel.Constants.xlNone;
        oSeriesBar.HasLeaderLines = true;


        DataTable dsData = getTable();
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
        rptChart.Visible = false;
        rptExcelChart.Visible = true;
        grdCostReport.Visible = false;
        GenerateGraph();
    }
   

}
