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
using System.Data.SqlClient;
using WMS.DIL;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Office.Core;
using Microsoft.Office;
using InfoSoftGlobal;



public partial class View_Forms_Exceptions_QuarterWiseReport : System.Web.UI.Page
{
    string FilepathTemp = ConfigurationManager.AppSettings["WMSProjectName"].ToString();
    QueryConroller objQuerycontroller = new QueryConroller();
    public string strProjectName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        strProjectName = System.Configuration.ConfigurationManager.AppSettings["WMSProjectName"];

        if (!IsPostBack)
        {
            string Date = (System.DateTime.Now.ToShortDateString());
            string[] strDateArray = Date.Split('/');
            string CrDate = strDateArray[2].ToString();
            drpYear.SelectedValue = CrDate;
            BindModel();
            BindClutch();
            BindCategory();
            BindModelSpecial();
           
        }
    }


  
    public void bindData()
    {
       
        if (rdoCost.Checked)
        {

            GridView1.Columns[2].HeaderText = "Cost";
            GridView1.Columns[1].Visible =true;
        }
        if (rdoDefect.Checked)
        {

            GridView1.Columns[2].HeaderText = "Defect";
            GridView1.Columns[1].Visible = true;
        }
        if ((Convert.ToInt32(ddlselection.SelectedValue) == 2 || Convert.ToInt32(ddlselection.SelectedValue) == 3) && rdoDefect.Checked)
        {

            GridView1.Columns[2].HeaderText = "Quantity";
            GridView1.Columns[1].Visible = false;
        
        }
            
                 
               
        DataTable dtinformation = new DataTable();
        dtinformation = GetTable();
            if (dtinformation != null)
            {
                if (dtinformation.Rows.Count > 0)
                {
                    GridView1.DataSource = dtinformation;
                    GridView1.DataBind();

                }
                else
                {
                    GridView1.DataSource = null;
                    GridView1.EmptyDataText = "no data found";
                    GridView1.DataBind();
                }
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.EmptyDataText = "no data found";
                GridView1.DataBind();
            }
        
    }

    public void BindClutch()
    {
        string strQuery = "select * from ModelClutchType order by ClutchType";
        DataTable dt = objQuerycontroller.ExecuteQuery(strQuery);
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                drpClutch.DataSource = dt;
                drpClutch.DataTextField = "ClutchType";
                drpClutch.DataValueField = "ClutchTypeID";
                drpClutch.DataBind();
                drpClutch.AppendDataBoundItems = true;
                ListItem list = new ListItem("All", "0");
                drpClutch.Items.Insert(0, list);
                drpClutch.AppendDataBoundItems = false;
            }
        }
    }

    public void BindCategory()
    {
        string strQuery = "select * from ModelCategory order by ModelCategory";
        DataTable dt = objQuerycontroller.ExecuteQuery(strQuery);
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                drpCategory.DataSource = dt;
                drpCategory.DataTextField = "ModelCategory";
                drpCategory.DataValueField = "ModelCategoryID";
                drpCategory.DataBind();
                drpCategory.AppendDataBoundItems = true;
                ListItem list = new ListItem("All", "0");
                drpCategory.Items.Insert(0, list);
                drpCategory.AppendDataBoundItems = false;
            }
        }
    }

    public void BindModelSpecial()
    {
        string strQuery = "select * from ModelSpecialDetails order by ModelSpecial";
        DataTable dt = objQuerycontroller.ExecuteQuery(strQuery);
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                drpSpecial.DataSource = dt;
                drpSpecial.DataTextField = "ModelSpecial";
                drpSpecial.DataValueField = "ModelSpecialID";
                drpSpecial.DataBind();
                drpSpecial.AppendDataBoundItems = true;
                ListItem list = new ListItem("NA", "0");
                drpSpecial.Items.Insert(0, list);
                drpSpecial.AppendDataBoundItems = false;
            }
        }
    }

    public DataTable GetTable()
    {
        string intWhereQueryHMR = "";
        string strSum = "";
        string strQuery = "";
        string stradd = "";
        string strwhrprm = "";
        DataTable dtinformation = new DataTable();
        string strYear = rdoYear.SelectedValue; 
        if (rdoCost.Checked)
        {

            strSum = "'Cost'";
        }
        else
        {
            if (rdoDefect.Checked)
            {
                strSum = "'Defect'";
            }

        }
        if (rdoLessThan250.Checked)
        {
            intWhereQueryHMR = "1";

        }
        else
        {
            if (rdoMoreThan250.Checked)
            {
                intWhereQueryHMR = "2";

            }
            else
            {
                intWhereQueryHMR = "3";
            }

        }

        if (rdoData.SelectedValue == "0")
        {
            if (rdoAlwar.Checked)
            {
                strwhrprm = "1";

            }
            else
            {
                if (rdoBhopal.Checked)
                {
                    strwhrprm = "2 ";
                }
                else
                {
                    strwhrprm = "3";
                }
            }
        }
        else if (rdoData.SelectedValue == "1")
        {

            if (rdoAlwarEngine.Checked)
            {
                strwhrprm = "1";

            }
            else
            {
                if (rdoSimpsonEngine.Checked)
                {
                    strwhrprm = "2 ";
                }
                else
                {
                    strwhrprm = "3";
                }
            }
        }
        else
        {
            strwhrprm = "4";
        }


        string strModelCategoryID = drpCategory.SelectedValue;
        string strClutchTypeID = drpClutch.SelectedValue;
        string strModelSpecialID = drpSpecial.SelectedValue;




        string Model_Code = drpModel.SelectedItem.Text;
        if (drpModel.SelectedValue == "0")
        {
            Model_Code = "";
        }
        int Month = Convert.ToInt32(drpMonth.SelectedValue);
        string year = drpYear.SelectedItem.Text;
        string strFin = (year.Substring(2, 2));

        string Last = txtLastYear.Text;

        if (Month > 3)
        {

            stradd = (Convert.ToString(Convert.ToInt16(year) + 1).Substring(3, 1));

            strFin = strFin + stradd;


        }
        else

            if (Month < 4)
            {

                stradd = (Convert.ToString(Convert.ToInt16(year) - 1).Substring(2, 2));
                strFin = (year.Substring(3, 1));
                strFin = stradd + strFin;
            }
        string type = "";
        if (rdoPrimary.Checked)
        {
            type = "0";
        }
        else if (rdoConsequences.Checked)
        {
            type = "1";
        }
        else
        {
            type = "2";
        }
        if (Convert.ToInt32(ddlselection.SelectedValue) == 1)
        {
            strQuery = " execute usp_getCostBulk '" + drpYear.SelectedItem.Text + "', " + strFin + "," + Last + "," + type + ",'" + Model_Code + "'," + strModelCategoryID + "," + strClutchTypeID + "," + strModelSpecialID + "," + strSum + "," + intWhereQueryHMR + "," + rdoData.SelectedValue + "," + strwhrprm + "," + Month + ",'" + strYear + "'";
        }
        else
            if (Convert.ToInt32(ddlselection.SelectedValue)== 2)
            {

                strQuery = " execute usp_ProductionCostNewModel '" + drpYear.SelectedItem.Text + "', " + strFin + "," + Last + ",'" + Model_Code + "'," + strModelCategoryID + "," + strClutchTypeID + "," + strModelSpecialID + "," + strSum + "," + 0 + "," + Month + "";
            }
            else
                if (Convert.ToInt32(ddlselection.SelectedValue) == 3)
                {

                    strQuery = " execute usp_SalesCostNewModel '" + drpYear.SelectedItem.Text + "', " + strFin + "," + Last + ",'" + Model_Code + "'," + strModelCategoryID + "," + strClutchTypeID + "," + strModelSpecialID + "," + strSum + "," + 0 + "," + Month + "";
                
                }

      
        dtinformation = objQuerycontroller.ExecuteQuery(strQuery);
        //if (rdoPrimary.Checked)
        //{
        //    DataView dv = new DataView(dtinformation);
        //    dv.RowFilter = " DT like 'P%'";
        //    dtinformation = dv.ToTable();
        //}
        //if (rdoConsequences.Checked)
        //{
        //    DataView dv = new DataView(dtinformation);
        //    dv.RowFilter = " DT like 'C%' ";
        //    dtinformation = dv.ToTable();
        //}

        //if (rdoNew.Checked)
        //{
        //    DataView dv = new DataView(dtinformation);
        //    dv.RowFilter = " IsNew=0 ";
        //    dtinformation = dv.ToTable();
        //}
        //if (rdoRegular.Checked)
        //{
        //    DataView dv = new DataView(dtinformation);
        //    dv.RowFilter = " IsNew=1 ";
        //    dtinformation = dv.ToTable();
        //}
       
        return dtinformation;
    }
    protected void btnViewTable_Click(object sender, EventArgs e)
    {
        GridView1.Visible = true;
        bindData();
        rptChart.Visible = false;
        rptExcelChart.Visible = false;
      
    }
    
    public void eventhandlerSerialNo(object Sender, EventArgs E)
    {
        int i = 1;
        int pi = GridView1.PageIndex;
        int ps = GridView1.PageSize;
        //<><> Use Name of Your GridView Instead Of gvDetailProspect <><>// 
        if (pi > 0)
        {
            i = pi * ps + 1;
        }
        foreach (GridViewRow gr in GridView1.Rows)
        {
            gr.Cells[0].Text = i.ToString();

            i++;
        }
    
      
    }

    public void BindModel()
    {
        string strQuery = "";
        DataTable dtModel = new DataTable();
        strQuery = "select * from ModelGroupName ";
        dtModel = objQuerycontroller.ExecuteQuery(strQuery);
        if (dtModel != null)
        {
            if (dtModel.Rows.Count > 0)
            {
                drpModel.DataSource = dtModel;

                drpModel.DataTextField = "ModelGroupName";
                drpModel.DataValueField = "GroupID";
                drpModel.DataBind();
                drpModel.AppendDataBoundItems = true;
                ListItem list = new ListItem("All", "0");
                drpModel.Items.Insert(0, list);
                drpModel.AppendDataBoundItems = false;
                
            }
        }
    }

    protected void btnViewGraph_Click(object sender, EventArgs e)
    {
        rptChart.Visible = true;
        rptExcelChart.Visible = false;
        GridView1.Visible = false;
        DataTable dtinformation = new DataTable();
        dtinformation = GetTable();
        ViewState.Add("dtinformation", dtinformation);
        //string strXML = XMLData(dtinformation);
        //string FileTemp = Server.MapPath(FilepathTemp);
        //FileTemp = FileTemp + "\\UploadFile\\charts.xml";
        //try
        //{
        //    using (StreamWriter writer = new StreamWriter(FileTemp, false))
        //    {

        //        writer.WriteLine(strXML);
        //        writer.Close();
        //    }

        //}
        //catch (Exception ex)
        //{

        //}
    }
    public string XMLData(DataTable dt)
    {

        try
        {

            string xml = "<graph bgColor='FFECAA' rotateNames='1' caption='Tractor " + drpModel.SelectedItem.Text + "' yAxisName='Amount' xAxisName='Period' formatNumberScale='0'  >";
            foreach (DataRow dr in dt.Rows)
            {
                string strAmount = "";
                if (dr["DefectAmount"].ToString() == "")
                {
                    strAmount = "0";
                }
                else
                {
                    strAmount = dr["DefectAmount"].ToString();
                }
                xml = xml + "<set name='" + dr["Fyear"].ToString() + "' value='" + strAmount + "' />";

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
            DataTable dsData = GetTable();
            int colcount = dsData.Columns.Count;
            int rowcount = dsData.Rows.Count;
            int i = 2;

            targetSheet.Cells[1, 1] = "Financial Year";
            targetSheet.Cells[1, 2] = "Value";

            // Outputting the data
            foreach (DataRow dr in dsData.Rows)
            {
                //targetSheet.Cells[i, 1] = dr["Fyear"];
                targetSheet.Cells[i, 1] = "'"+ dr["Fyear"];
                targetSheet.Cells[i, 2] = dr["DefectAmount"];
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
        dt = GetTable();
        strXML = XMLData(dt);
        int i = 0;
        
        string width = "";
        string height = "";

        //Create the chart - Pie 3D Chart with data from strXML
       
        string strChartPath = strProjectName+"/JS/Flash/Column3DTest.swf";

        width = "100%";
        height = "270";

        return FusionCharts.RenderChart(strChartPath, "", strXML, "chartid", width, height, false, true);


    }


    public void ColumnGraph(Excel.Chart chart, Excel.Worksheet targetSheet, Excel.Range dataRange)
    {
        chart.SetSourceData(dataRange, Excel.XlRowCol.xlColumns);
        string strChartType = Convert.ToString(chart.ChartType);

        string strModel = Convert.ToString(drpModel.SelectedItem.Text);
        string strReportType = "";
        if (rdoCost.Checked)
        {
            strReportType = "Cost";
        }
        else
        {
            strReportType = "Defect";
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
        chart.ChartTitle.Text = "Model: " + strModel;
        chart.ChartTitle.Text += "\n " + strReportType + " / Tractor ";
        chart.ChartTitle.Text += "| HMR:"+ strHMR;
        chart.ChartTitle.Text += "\n " + strYear ;
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

    public void PieGraph(Excel.Chart chart, Excel.Worksheet targetSheet, Excel.Range dataRange)
    {
        //chart.ChartType = Excel.XlChartType.xl3DPie;
        chart.SetSourceData(dataRange, Excel.XlRowCol.xlColumns);

        string strModel = Convert.ToString(drpModel.SelectedItem.Text);
        string strReportType = "";
        if (rdoCost.Checked)
        {
            strReportType = "Cost";
        }
        else
        {
            strReportType = "Defect";
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
        chart.ChartTitle.Text = "Model: " + strModel;
        chart.ChartTitle.Text += "\n " + strReportType + " / Tractor ";
        chart.ChartTitle.Text += "| HMR:" + strHMR;
        chart.ChartTitle.Text += "\n " + strYear;
        chart.ChartTitle.Font.Name = "Times New Roman";
        chart.PlotArea.Fill.ForeColor.SchemeColor = 2;
        chart.PlotArea.Border.ColorIndex = Excel.Constants.xlNone;

       
        

        chart.ApplyDataLabels(Excel.XlDataLabelsType.xlDataLabelsShowValue, null, null, false, false, false, true, false, false, ",");
        chart.HasLegend = true;

        Excel.Series oSeriesBar = null;
        oSeriesBar = (Excel.Series)chart.SeriesCollection(1);
        oSeriesBar.Border.ColorIndex = Excel.Constants.xlNone;
        oSeriesBar.HasLeaderLines = true;
        

        DataTable dsData =GetTable();
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
        GridView1.Visible = false;
        GenerateGraph();
    }
   
} 

