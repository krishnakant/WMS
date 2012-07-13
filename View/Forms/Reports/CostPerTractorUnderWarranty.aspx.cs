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

public partial class View_Forms_Reports_CostPerTractorUnderWarranty : System.Web.UI.Page
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

    //public void BindModel()
    //{
    //    string strQuery = "";
    //    DataTable dtModel = new DataTable();
    //    strQuery = "select * from ModelGroupName  order by ModelGroupName";
    //    dtModel = objQueryController.ExecuteQuery(strQuery);
    //    if (dtModel != null)
    //    {
    //        if (dtModel.Rows.Count > 0)
    //        {
    //            chkModelCodeList.DataSource = dtModel;
    //            chkModelCodeList.DataValueField = "GroupID";
    //            chkModelCodeList.DataTextField = "ModelGroupName";
    //            chkModelCodeList.DataBind();
    //        }

    //    }

    //}

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
                drpModel.DataValueField = "GroupID";
                drpModel.DataTextField = "ModelGroupName";
                drpModel.DataBind();
                drpModel.AppendDataBoundItems = true;
                ListItem list = new ListItem("All", "0");
                drpModel.Items.Insert(0,list);
                drpModel.AppendDataBoundItems = false;
            }

        }
        drpModel.SelectedValue = "1";
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
        string strSalesPeriodQuery = "Select distinct MonthID,YearID,substring(DateName(month,Convert(varchar,MonthID)+'/01/'+Convert(varchar,YearID)),1,3)+'-'+Convert(varchar,YearID) as Sales_Period from TempWarrantyCostFinal where SalesDate between '"+ strFromDate +"' and '"+ strToDate +"' order by YearID,MonthID";
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
       
        //foreach (ListItem list in chkModelCodeList.Items)
        //{
        //    if (list.Selected)
        //    {
        //        if (modelflag > 0)
        //        {
        //            strModelParameter += " or ";
        //        }
        //        strModelParameter += " GroupID =" + list.Value + " ";
        //        modelflag++;
        //    }

        //}
        if (drpModel.SelectedValue != "0")
        {
            strModelParameter += " GroupID=" + drpModel.SelectedValue + " ";
        }
        else
        {
            strModelParameter = "";
        }
        modelflag++;
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

        string strEngineParameter = "";

        if (rdoData.SelectedValue == "0")
        {
            if (rdoAlwar.Checked)
            {
                strEngineParameter = "  (IsEngine=1 and Engine=''A'')";
            }
            else
            {
                if (rdoBhopal.Checked)
                {
                    strEngineParameter = "((Engine=''A'' and IsEngine=0) or Engine=''s'') ";
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
                strEngineParameter = "  (IsEngine=1 and Engine=''A'')";
            }
            else
            {
                if (rdoSimpsonEngine.Checked)
                {
                    strEngineParameter = " (Engine=''S'' and IsEngine=1) ";
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

        string strQuery = "";

        string ViewSelection="";
        //add by vaibhav
        if (rdoPrimary.SelectedValue == "0")
        {
            if (rdoViewName.SelectedValue == "Reporting")
            {
                if (rdo_Count_Quantity.SelectedValue == "Count")
                {
                    //Existing view
                    ViewSelection = "vw_ReportingMonthWiseReworkCost";
                }
                else if (rdo_Count_Quantity.SelectedValue == "Quantity")
                {
                    //New view
                    ViewSelection = "vw_ReportingMonthWiseReworkCost_Quantity";
                }
            }
            else if (rdoViewName.SelectedValue == "Replacement")
            {
                if (rdo_Count_Quantity.SelectedValue == "Count")
                {
                    //Existing view
                    ViewSelection = "vw_ReplacementMonthWiseReworkCost";
                }
                else if (rdo_Count_Quantity.SelectedValue == "Quantity")
                {
                    //New view
                    ViewSelection = "vw_ReplacementMonthWiseReworkCost_Quantity";
                }

            }
        }
        else
        {
            if (rdoViewName.SelectedValue == "Reporting")
            {
                //Existing view
                ViewSelection = "vw_ReportingMonthWiseReworkCost";
            }
            else if (rdoViewName.SelectedValue == "Replacement")
            {

                //Existing view
                ViewSelection = "vw_ReplacementMonthWiseReworkCost";
            }
        
        }

        //End

        if (drpModel.SelectedValue != "0")
        {

           


//            strQuery = "exec usp_TestCostPerTractorUnderWarranty '" + strModelParameter + "','" + strModelCategoryParameter + "','" + strModelClutchParameter + "','" + strModelSpecialParameter + "','" + drpFromMonth.SelectedValue + "','" + drpFromYear.SelectedValue + "','" + drpToMonth.SelectedValue + "','" + drpToYear.SelectedValue + "'," + rdoHMR_Range.SelectedValue + ",'" + strEngineParameter + "','" + strRegionParameter + "','" + rdoViewName.SelectedValue + "'," + rdoPrimary .SelectedValue+ "";
            strQuery = "exec usp_TestCostPerTractorUnderWarranty '" + strModelParameter + "','" + strModelCategoryParameter + "','" + strModelClutchParameter + "','" + strModelSpecialParameter + "','" + drpFromMonth.SelectedValue + "','" + drpFromYear.SelectedValue + "','" + drpToMonth.SelectedValue + "','" + drpToYear.SelectedValue + "'," + rdoHMR_Range.SelectedValue + ",'" + strEngineParameter + "','" + strRegionParameter + "'," + ViewSelection + "," + rdoPrimary.SelectedValue + "";
        }
        else
        {
            //strQuery = "exec usp_All_CostPerTractorUnderWarrantyTest '" + strModelParameter + "','" + strModelCategoryParameter + "','" + strModelClutchParameter + "','" + strModelSpecialParameter + "','" + drpFromMonth.SelectedValue + "','" + drpFromYear.SelectedValue + "','" + drpToMonth.SelectedValue + "','" + drpToYear.SelectedValue + "'," + rdoHMR_Range.SelectedValue + ",'" + strEngineParameter + "','" + strRegionParameter + "','" + rdoViewName.SelectedValue + "'," + rdoPrimary.SelectedValue + "";
            strQuery = "exec usp_All_CostPerTractorUnderWarrantyTest '" + strModelParameter + "','" + strModelCategoryParameter + "','" + strModelClutchParameter + "','" + strModelSpecialParameter + "','" + drpFromMonth.SelectedValue + "','" + drpFromYear.SelectedValue + "','" + drpToMonth.SelectedValue + "','" + drpToYear.SelectedValue + "'," + rdoHMR_Range.SelectedValue + ",'" + strEngineParameter + "','" + strRegionParameter + "'," + ViewSelection + "," + rdoPrimary.SelectedValue + "";
        }
        DataTable dt = new DataTable();
        if (IsPostBack)
        {
            dt = objQueryController.ExecuteQuery(strQuery);
        }
        return dt;

    }

    

    protected void OnRowCreated(object sender, GridViewRowEventArgs e)
    {
       

    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        divGraph.Visible = false;
        print_Grid.Visible = true;
        BindGrid();
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        string str = "";
        string strParameter = "";
        GridViewExport objExport = new GridViewExport();


        strParameter = strParameter + getchkList(chkCategory, "Category");
        strParameter = strParameter + getchkList(chkClutchType, "ClutchType");
        strParameter = strParameter + getchkList(chkSpecialList, "Special");
        strParameter = strParameter + getchkList(chkRegion, "Region");
        str = str + "<table width='50%' border='1' cellpadding='0' cellspacing='0'>";
        str = str + "<tr><td style='font-size:small;font-weight:bold;'>Report For:</td><td style='font-size:small;font-weight:bold;'>Cost Per Tractor Under Warranty</td></tr>";
        str = str + "</table>";
        str = str + strParameter;
        str = str + "<br/><table width='50%' border='1' cellpadding='0' cellspacing='0'>";
        str = str + "<tr><td >From Period:</td>";
        str = str + "<td style='font-size:small;font-weight:bold;'>" + drpFromMonth.SelectedItem.Text.ToString().Replace("Select All", "All") + "</td>";
        str = str + "<td style='font-size:small;font-weight:bold;'>" + drpFromYear.SelectedItem.Text.ToString().Replace("Select All", "All") + "</td>";
        str = str + " <td>To Period:</td>";
        str = str + "<td style='font-size:small;font-weight:bold;'>" + drpToMonth.SelectedItem.Text.ToString().Replace("Select All", "All") + "</td>";
        str = str + "<td style='font-size:small;font-weight:bold;'>" + drpToYear.SelectedItem.Text.ToString().Replace("Select All", "All") + "</td>";
        str = str + "<td >Model:</td>";
        str = str + "<td style='font-size:small;font-weight:bold;'>" + drpModel.SelectedItem.Text.ToString().Replace("Select All", "All") + "</td></tr>";
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
        str = str + "<tr><td >View Name:</td>";
        str = str + "<td style='font-size:small;font-weight:bold;'>" + rdoViewName.SelectedItem.Text.ToString().Replace("Select All", "All") + "</td>";
        str = str + "<td >HMR Range:</td>";
        str = str + "<td style='font-size:small;font-weight:bold;'>" + rdoHMR_Range.SelectedItem.Text.ToString() + "</td></tr>";
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


    protected void btnShowGraph_Click(object sender, EventArgs e)
    {
        print_Grid.Visible = false;
        btnExport.Visible = false;
        CreateChart();
        divGraph.Visible = true;  
       

       
    }

    public string XMLData(DataTable dt)
    {

        try
        {
            int ModelGroupID = Convert.ToInt32(drpModel.SelectedValue);
            string strModelQuery = "Select * from ModelGroupName where GroupID=" + ModelGroupID;
            DataTable dtWarranty = objQueryController.ExecuteQuery(strModelQuery);
            int WarrantyPeriod = 0;
            foreach (DataRow drWarranty in dtWarranty.Rows)
            {
                WarrantyPeriod = Convert.ToInt32(drWarranty["WarrantyPeriod"].ToString());
            }


           
            //string xml = "<graph bgColor='FFECAA' rotateNames='1' caption='Model:" + strModel + " /Item:" + strItem + " ' yAxisName='Cost/Tractor Under Warranty' xAxisName='Production Month' formatNumberScale='0'  >";
            string xml = "<graph caption='Warranty Cost Per Tractor ' decimalPrecision='0' rotateNames='1' subcaption='Tractor-" + drpModel.SelectedItem.Text + "' formatNumberScale='0'   yAxisMinValue='15000' yAxisName='Cost/Tractor Under Warranty' xAxisName='Reporting Month' showNames='1' showValues='1'  showColumnShadow='1' animation='1' showAlternateHGridColor='1' AlternateHGridColor='ff5904' divLineColor='ff5904' divLineAlpha='20' alternateHGridAlpha='5' canvasBorderColor='666666' baseFontColor='666666' lineThickness='3'  bgColor='f1f1f1'>";
            int rowFlag = 1;
            foreach (DataRow dr in dt.Rows)
            {
                string strField = dr["Field"].ToString();
                if (WarrantyPeriod == 1)
                {
                    if (strField == "Cost/Tractor 1st Year")
                    {
                        if (rowFlag > 2)
                        {
                            int colflag = 1;
                            foreach (DataColumn dc in dt.Columns)
                            {
                                if (colflag > 2)
                                {
                                    xml = xml + "<set name='" + dc.ColumnName + "' value='" + dr[dc].ToString() + "' />";
                                }
                                colflag++;
                            }
                        }
                    }
                }
                else
                {
                    if (strField == "Cost/Tractor 1st Year")
                    {
                        if (rowFlag > 2)
                        {
                            int colflag = 1;
                            foreach (DataColumn dc in dt.Columns)
                            {
                                if (colflag > 1)
                                {
                                    xml = xml + "<set name='" + dc.ColumnName + "' value='" + dr[dc].ToString() + "' />";
                                }
                                colflag++;
                            }
                        }
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
}
