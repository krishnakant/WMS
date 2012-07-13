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

public partial class View_Forms_Reports_EffectCheck : System.Web.UI.Page
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
        int strExtendedmonth = 0;
        if (txtEntendedMonth.Text != "0")
        {
            strExtendedmonth = Convert.ToInt32(hdnExtended.Value);
        }

        //string strQuery = "exec usp_EffectCheck '" + strItemParameter + "','" + strModelParameter + "','" + strModelCategoryParameter + "','" + strModelClutchParameter + "','" + strModelSpecialParameter + "','" + drpFromMonth.SelectedValue + "','" + drpToMonth.SelectedValue + "','" + PrevFromMonth + "','" + PrevToMonth + "'";
        //string strQuery = "exec usp_EffectCheckNew '" + strItemParameter + "','" + strModelParameter + "','" + strModelCategoryParameter + "','" + strModelClutchParameter + "','" + strModelSpecialParameter + "','" + drpMonth.SelectedValue + "','" + drpYear.SelectedItem.Text + "'";
	//string strQuery = "exec usp_EffectCheckMain'" + strItemParameter + "','" + strModelParameter + "','" + strModelCategoryParameter + "','" + strModelClutchParameter + "','" + strModelSpecialParameter + "','" + drpMonth.SelectedValue + "','" + drpYear.SelectedItem.Text + "'";
        string strQuery = "exec usp_EffectCheckMain_Extend'" + strItemParameter + "','" + strModelParameter + "','" + strModelCategoryParameter + "','" + strModelClutchParameter + "','" + strModelSpecialParameter + "','" + drpMonth.SelectedValue + "','" + drpYear.SelectedItem.Text + "'," + strExtendedmonth;
       

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
                //int HeaderText = Convert.ToInt32(e.Row.Cells[i].Text);
                //string strHeaderText ="'"+getProductionMonthYear(HeaderText);
                //e.Row.Cells[i].Text = strHeaderText;
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

    protected void btnShowGraph_Click(object sender, EventArgs e)
    {
        CreateChart();
        grdFailureReport.Visible = false;
        btnExport.Visible = false;
        divGraph.Visible = true;   
       
      
    }

    public string XMLData(DataTable dt)
    {
        string strModel = "";

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

        try
        {
            
            //string xml = "<graph bgColor='FFECAA' rotateNames='1' caption='Model:" + strModel + " /Item:" + strItem + " ' yAxisName='Failure/1000 Tractors' xAxisName='Production Month' formatNumberScale='0'  >";
            string xml = "<graph caption='Cumulative Failure/1000 Tractors   Group: (" + drpItemGroup.SelectedItem.ToString() + ") ' subcaption='Model: " + strModel + "' decimalPrecision='3' rotateNames='1' subcaption='' formatNumberScale='0'   yAxisMinValue='15000' yAxisName='Failure/1000 Tractors' xAxisName='Production Month' showNames='1' showValues='1'  showColumnShadow='1' animation='1' showAlternateHGridColor='1' AlternateHGridColor='ff5904' divLineColor='ff5904' divLineAlpha='20' alternateHGridAlpha='5' canvasBorderColor='666666' baseFontColor='666666' lineThickness='3'  bgColor='f1f1f1'>";
            string strCategoriesTag = "<categories>";
            int rowFlag = 1;
            int colnameflag = 0;
            foreach (DataColumn dccol in dt.Columns)
            {
                if (colnameflag > 0)
                {
                    string[] colname=dccol.ColumnName.Split('-');
                    strCategoriesTag += "<category name='" + colname[0] + "'/>";
                }
                colnameflag++; 
            }

            strCategoriesTag += "</categories>";
            string strDataSet = "";
            foreach (DataRow dr in dt.Rows)
            {
                strDataSet += "<dataset seriesName='" + dr["PM"].ToString() +"'";
                if (rowFlag == 1)
                {
                    strDataSet += "color='A66EDD'>";
                }
                else
                {
                    strDataSet += "color='F6BD0F'>";
                }

                  int colflag = 1;
                    foreach (DataColumn dc in dt.Columns)
                    {
                        if (colflag > 1)
                        {
                            strDataSet = strDataSet + "<set value='" + dr[dc].ToString() + "' />";
                        }
                        colflag++;
                    }
                    strDataSet += "</dataset>";
                    rowFlag++;
            }

            xml += strCategoriesTag;
            xml += strDataSet;
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

        string strChartPath = strProjectName + "/JS/Flash/FCF_MSLine.swf";

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
            string paramWorkbookPath = AppDomain.CurrentDomain.BaseDirectory + "UploadFile\\Graphs\\EffectCheck";
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

         
            int columnflag = 1;
            //foreach (DataColumn dccol in dsData.Columns)
            //{
            //    targetSheet.Cells[1, columnflag] = dccol.ColumnName;
            //    targetSheet.Cells[2, columnflag] = dsData.Rows[0][dccol].ToString();
            //    targetSheet.Cells[3, columnflag] = dsData.Rows[1][dccol].ToString();
            //    columnflag++;
            //}
            string beforafter;

            foreach (DataColumn dccol in dsData.Columns)
            {
                //New changes for Excel graph presentation 
                string[] strTest = dccol.ColumnName.Split('-');

                targetSheet.Cells[1, columnflag] = strTest[0];

                if (dsData.Rows[0][dccol].ToString() == "Before")
                {
                    beforafter = dsData.Rows[0][dccol].ToString() + "-" + drpMonth.SelectedItem.Text + " " + drpYear.SelectedItem.Text;

                    targetSheet.Cells[2, 1] = beforafter;

                }

                if (dsData.Rows[1][dccol].ToString() == "After")
                {
                    beforafter = dsData.Rows[1][dccol].ToString() + "-" + drpMonth.SelectedItem.Text + " " + drpYear.SelectedItem.Text;
                    targetSheet.Cells[3, 1] = beforafter;
                }
                if (dccol.ToString() != "PM")
                {
                    string str1 = dsData.Rows[0][dccol].ToString();
                    string str2 = dsData.Rows[1][dccol].ToString();

                    targetSheet.Cells[2, columnflag] = dsData.Rows[0][dccol].ToString();
                    targetSheet.Cells[3, columnflag] = dsData.Rows[1][dccol].ToString();
                }


                columnflag++;
            }


          
            object fromCell = "$A1";
            //object toCell = "$B" + (colcount - 1).ToString();
            //To claculate the excel column for the Exdended months
            string Sheetcellno = "$N3";
            if (Convert.ToInt32(txtEntendedMonth.Text) > 1)
            {
                if (Convert.ToInt32(txtEntendedMonth.Text) == 2)
                {
                    Sheetcellno = "$O3";
                }
                else if (Convert.ToInt32(txtEntendedMonth.Text) == 3)
                {
                    Sheetcellno = "$P3";
                }
                else if (Convert.ToInt32(txtEntendedMonth.Text) == 4)
                {
                    Sheetcellno = "$Q3";
                }
                else if (Convert.ToInt32(txtEntendedMonth.Text) == 5)
                {
                    Sheetcellno = "$R3";
                }
                else if (Convert.ToInt32(txtEntendedMonth.Text) == 6)
                {
                    Sheetcellno = "$S3";
                }
                else if (Convert.ToInt32(txtEntendedMonth.Text) == 7)
                {
                    Sheetcellno = "$T3";
                }
                else if (Convert.ToInt32(txtEntendedMonth.Text) == 8)
                {
                    Sheetcellno = "$U3";
                }
                else if (Convert.ToInt32(txtEntendedMonth.Text) == 9)
                {
                    Sheetcellno = "$V3";
                }
                else if (Convert.ToInt32(txtEntendedMonth.Text) == 10)
                {
                    Sheetcellno = "$W3";
                }
                else if (Convert.ToInt32(txtEntendedMonth.Text) == 11)
                {
                    Sheetcellno = "$X3";
                }
                else if (Convert.ToInt32(txtEntendedMonth.Text) == 12)
                {
                    Sheetcellno = "$Y3";
                }

            }
            //object toCell = "$S3";
            object toCell = Sheetcellno;
            dataRange = targetSheet.get_Range(fromCell, toCell);


            /**************************************Get Line Chart()************************************************************/
            // Generating the graph
            Excel.Chart linechart;
            linechart = (Excel.Chart)newWorkbook.Charts.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            linechart.ChartType = Excel.XlChartType.xlLineMarkers;            
            ColumnGraph(linechart, targetSheet, dataRange);

            //newWorkbook.SaveAs(paramWorkbookPath, Excel.XlFileFormat.xlExcel9795, null, null, false, false, Excel.XlSaveAsAccessMode.xlNoChange, false, false, null, null, null);
            newWorkbook.SaveCopyAs(paramWorkbookPath);
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
        chart.SetSourceData(dataRange, Excel.XlRowCol.xlRows);
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
        chart.HasDataTable = true;
        chart.HasTitle = true;
        chart.HasLegend = true;
        chart.ChartTitle.Font.Bold = true;
        chart.ChartTitle.Font.Size = 15;
        chart.ChartTitle.Text = "Model: " + strModel;
        //chart.ChartTitle.Text += "\n Item:" + strItem;   Changed on 20 Feb 2010

        chart.ChartTitle.Text += "\n Group:" + drpItemGroup.SelectedItem.Text;
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
            xlAxisCategory.Item(Excel.XlAxisType.xlCategory, Excel.XlAxisGroup.xlPrimary).TickLabels.Orientation = (Excel.XlTickLabelOrientation)orient;
            xlAxisValue = (Excel.Axes)chart.Axes(Type.Missing, Excel.XlAxisGroup.xlPrimary);
            xlAxisValue.Item(Excel.XlAxisType.xlValue, Excel.XlAxisGroup.xlPrimary).HasTitle = true;
            xlAxisValue.Item(Excel.XlAxisType.xlValue, Excel.XlAxisGroup.xlPrimary).AxisTitle.Text = "Cumulative Failure/1000 Tractors";

        }
        catch { }

       
        chart.ApplyDataLabels(Excel.XlDataLabelsType.xlDataLabelsShowValue, null, null, false, false, false, false, false, false, ",");
        chart.HasLegend = false;

      
        DataTable dsData = GetTable();
        int colcount = dsData.Columns.Count;
        int rowcount = dsData.Rows.Count;


       
        /**************************************************************************************************/


    }


    protected void btnExcelGraph_Click(object sender, EventArgs e)
    {
        GenerateGraph();
        string paramWorkbookPath = "/WMS/UploadFile/Graphs/EffectCheck";
        paramWorkbookPath += ".xls";
        //File.Open(paramWorkbookPath,FileMode.Open);
        Response.Redirect(paramWorkbookPath);
    }
    protected void drpItemGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindItem();
    }


}
