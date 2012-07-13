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

public partial class View_Forms_Reports_ProblemStatement : System.Web.UI.Page
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
        grdFailureReport.Columns.Clear();
        DataTable dtFailure = GetTable();
        if (dtFailure != null)
        {
            if (dtFailure.Rows.Count > 0)
            {
                btnExport.Visible = true;
                grdFailureReport.DataSource = dtFailure;
                if (rdoReportType.SelectedValue == "0")
                {
                    string strPMModelQuery = "select distinct GroupID,ModelGroupName from temp_PMWise_ProblemStmt where Production_Month between " + drpFromMonth.SelectedValue + " and " + drpToMonth.SelectedValue;
                    DataTable dtPMModel = objQueryController.ExecuteQuery(strPMModelQuery);

                    BoundField bnd = new BoundField();
                    bnd.DataField = "Production_Month";
                    bnd.HeaderText = "Production Month";
                    grdFailureReport.Columns.Add(bnd);

                    if (dtPMModel != null)
                    {
                        if (dtPMModel.Rows.Count > 0)
                        {
                            foreach (DataRow drPMModel in dtPMModel.Rows)
                            {
                                string strModel = drPMModel["ModelGroupName"].ToString();
                              
                                BoundField bndfield = new BoundField();
                                bndfield.DataField = strModel + "_Production";
                                bndfield.HeaderText = "Production";
                                bndfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                                bndfield.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                                grdFailureReport.Columns.Add(bndfield);

                                BoundField bndf = new BoundField();
                                bndf.DataField = strModel + "_Failure";
                                bndf.HeaderText = "Failure";
                                bndf.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                                bndf.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                                grdFailureReport.Columns.Add(bndf);

                                BoundField bndfk = new BoundField();
                                bndfk.DataField = strModel + "_Failure_Per_K";
                                bndfk.HeaderText = "Failure/1000";
                                bndfk.DataFormatString = "{0:F2}";
                                bndfk.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                                bndfk.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                                grdFailureReport.Columns.Add(bndfk);
                                

                            }
                        }

                    }
                }
                else if (rdoReportType.SelectedValue == "1")
                {
                    string strHMRModelQuery = "select distinct GroupID,ModelGroupName from temp_HMRWise_ProblemStmt where HMR_Range between 0 and 2500";
                    DataTable dtHMRModel = objQueryController.ExecuteQuery(strHMRModelQuery);
                    BoundField bnd = new BoundField();
                    bnd.DataField = "HMR_Range";
                    bnd.HeaderText = "HMR Range";
                    grdFailureReport.Columns.Add(bnd);

                    if (dtHMRModel != null)
                    {
                        if (dtHMRModel.Rows.Count>0)
                        {
                            foreach (DataRow drHMRModel in dtHMRModel.Rows)
                            {
                                string strModel = drHMRModel["ModelGroupName"].ToString();
                                BoundField bndf = new BoundField();
                                bndf.DataField = strModel + "_Failure";
                                bndf.HeaderText = "Failure";
                                bndf.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                                bndf.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                                grdFailureReport.Columns.Add(bndf);

                                BoundField bndfield = new BoundField();
                                bndfield.DataField = strModel + "_Percentage_Contribution";
                                bndfield.HeaderText = "% Contribution";
                                bndfield.DataFormatString = "{0:F2}";
                                bndfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                                bndfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                                bndfield.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                                grdFailureReport.Columns.Add(bndfield);
                                
                            }

                        }

                    }
                }
                else
                {
                    string strPMModelQuery = "select distinct GroupID,ModelGroupName from temp_RegionWise_ProblemStmt";
                    DataTable dtPMModel = objQueryController.ExecuteQuery(strPMModelQuery);

                    BoundField bnd = new BoundField();
                    bnd.DataField = "Region";
                    bnd.HeaderText = "Region";
                    grdFailureReport.Columns.Add(bnd);

                    if (dtPMModel != null)
                    {
                        if (dtPMModel.Rows.Count > 0)
                        {
                            foreach (DataRow drPMModel in dtPMModel.Rows)
                            {
                                string strModel = drPMModel["ModelGroupName"].ToString();

                                BoundField bndfield = new BoundField();
                                bndfield.DataField = strModel + "_Sales";
                                bndfield.HeaderText = "Sales";
                                bndfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                                bndfield.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                                grdFailureReport.Columns.Add(bndfield);

                                BoundField bndf = new BoundField();
                                bndf.DataField = strModel + "_Failure";
                                bndf.HeaderText = "Failure";
                                bndf.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                                bndf.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                                grdFailureReport.Columns.Add(bndf);

                                BoundField bndfk = new BoundField();
                                bndfk.DataField = strModel + "_Failure_Per_K";
                                bndfk.HeaderText = "Failure/1000";
                                bndfk.HtmlEncode = false;
                                bndfk.DataFormatString = "{0:F2}";
                                bndfk.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                                bndfk.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                                grdFailureReport.Columns.Add(bndfk);
                                

                            }
                        }

                    }
                }

                grdFailureReport.DataBind();

                foreach (GridViewRow grFailureReport in grdFailureReport.Rows)
                {
                    if (grFailureReport.Cells[0].Text == "5000")
                    {
                        grFailureReport.Cells[0].Text = "Total:";
                        grFailureReport.Font.Bold = true;
                        grFailureReport.BackColor = System.Drawing.Color.Aqua;
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
        //string strFromMonth = drpFromMonth.SelectedValue;
        //string strToMonth = drpToMonth.SelectedValue;

        string strItemParameter = "";
        
            strItemParameter =  drpItemGroup.SelectedValue;
      
       
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

        string strYearParameter = rdoYear.SelectedValue;

        string strQuery = "";
        if (rdoReportType.SelectedValue == "0")
        {
          // strQuery = "exec usp_ProductionMonthWise_ProblemStatement '" + strModelParameter + "','" + strModelCategoryParameter + "','" + strModelClutchParameter + "','" + strModelSpecialParameter + "','" + strItemParameter + "','" + drpFromMonth.SelectedValue + "','" + drpToMonth.SelectedValue + "','" + strEngineParameter + "','" + strYearParameter + "'";
            strQuery = "exec usp_ProductionMonthWise_ProblemStatement_New '" + strModelParameter + "','" + strModelCategoryParameter + "','" + strModelClutchParameter + "','" + strModelSpecialParameter + "','" + strItemParameter + "','(" + listmonthvalue + ")','" + strEngineParameter + "','" + strYearParameter + "'";
        }
        else if (rdoReportType.SelectedValue == "1")
        {
            //strQuery = "exec usp_HMRWise_ProblemStatement '" + strModelParameter + "','" + strModelCategoryParameter + "','" + strModelClutchParameter + "','" + strModelSpecialParameter + "','" + strItemParameter + "','" + drpFromMonth.SelectedValue + "','" + drpToMonth.SelectedValue + "','0','2500','" + strEngineParameter + "','" + strYearParameter + "'";
            strQuery = "exec usp_HMRWise_ProblemStatement_New '" + strModelParameter + "','" + strModelCategoryParameter + "','" + strModelClutchParameter + "','" + strModelSpecialParameter + "','" + strItemParameter + "','(" + listmonthvalue + ")','0','2500','" + strEngineParameter + "','" + strYearParameter + "'";
        }
        else
        {
            string[] strFrom = getSalesMonthYear(Convert.ToInt32(drpFromMonth.SelectedValue)).Split('-');
            string strFromMonth = strFrom[0];
            string strFromYear = strFrom[1];
            string[] strTo = getSalesMonthYear(Convert.ToInt32(drpToMonth.SelectedValue)).Split('-');
            string strToMonth = strTo[0];
            string strToYear = strTo[1];
           // strQuery = "exec usp_RegionWise_ProblemStatement '" + strModelParameter + "','" + strModelCategoryParameter + "','" + strModelClutchParameter + "','" + strModelSpecialParameter + "','" + strItemParameter + "','" + drpFromMonth.SelectedValue + "','" + drpToMonth.SelectedValue + "','" + strFromMonth + "','" + strFromYear + "','" + strToMonth + "','" + strToYear + "','" + strEngineParameter + "','" + strYearParameter + "','RegionID>0'";
            strQuery = "exec usp_RegionWise_ProblemStatement_New '" + strModelParameter + "','" + strModelCategoryParameter + "','" + strModelClutchParameter + "','" + strModelSpecialParameter + "','" + strItemParameter + "','(" + listmonthvalue + ")','" + strFromMonth + "','" + strFromYear + "','" + strToMonth + "','" + strToYear + "','" + strEngineParameter + "','" + strYearParameter + "','RegionID>0'";
        }
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
            GridView oGridView = (GridView)sender;
            GridViewRow oGridViewRow = new GridViewRow(0, 14, DataControlRowType.Header, DataControlRowState.Insert);
            int colspan = 0;
            TableCell oTableCell = new TableCell();
            oTableCell.Text = "";
            oTableCell.ColumnSpan = 1;
            oGridViewRow.Cells.Add(oTableCell);

            if (rdoReportType.SelectedValue == "0")
            {
                string strPMModelQuery = "select distinct GroupID,ModelGroupName from temp_PMWise_ProblemStmt where Production_Month between " + drpFromMonth.SelectedValue + " and " + drpToMonth.SelectedValue;
                DataTable dtPMModel = objQueryController.ExecuteQuery(strPMModelQuery);


                if (dtPMModel != null)
                {
                    if (dtPMModel.Rows.Count > 0)
                    {
                        foreach (DataRow drPMModel in dtPMModel.Rows)
                        {
                            oTableCell = new TableCell();
                            oTableCell.Text = drPMModel["ModelGroupName"].ToString();
                            oTableCell.HorizontalAlign = HorizontalAlign.Center;
                            oTableCell.ColumnSpan = 3;
                            oGridViewRow.Cells.Add(oTableCell);
                        }
                    }
                }
            }
            else if (rdoReportType.SelectedValue == "1")
            {
                string strHMRModelQuery = "select distinct GroupID,ModelGroupName from temp_HMRWise_ProblemStmt where HMR_Range between 0 and 2500";
                DataTable dtHMRModel = objQueryController.ExecuteQuery(strHMRModelQuery);


                if (dtHMRModel != null)
                {
                    if (dtHMRModel.Rows.Count > 0)
                    {
                        foreach (DataRow drHMRModel in dtHMRModel.Rows)
                        {
                            oTableCell = new TableCell();
                            oTableCell.Text = drHMRModel["ModelGroupName"].ToString();
                            oTableCell.HorizontalAlign = HorizontalAlign.Center;
                            oTableCell.ColumnSpan = 2;
                            oGridViewRow.Cells.Add(oTableCell);
                        }
                    }
                }

            }
            else
            {
                string strPMModelQuery = "select distinct GroupID,ModelGroupName from temp_RegionWise_ProblemStmt";
                DataTable dtPMModel = objQueryController.ExecuteQuery(strPMModelQuery);


                if (dtPMModel != null)
                {
                    if (dtPMModel.Rows.Count > 0)
                    {
                        foreach (DataRow drPMModel in dtPMModel.Rows)
                        {
                            oTableCell = new TableCell();
                            oTableCell.Text = drPMModel["ModelGroupName"].ToString();
                            oTableCell.HorizontalAlign = HorizontalAlign.Center;
                            oTableCell.ColumnSpan = 3;
                            oGridViewRow.Cells.Add(oTableCell);
                        }
                    }
                }
            }

                oGridView.Controls[0].Controls.AddAt(0, oGridViewRow);
            
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindGrid();
        grdFailureReport.Visible = true;        
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
        str = str + "<tr><td style='font-size:small;font-weight:bold;'>Report For:</td><td style='font-size:small;font-weight:bold;'>Problem Statement</td></tr>";
        str = str + "<tr><td style='font-size:small;font-weight:bold;'>" + rdoReportType.SelectedItem.Text.ToString() + "</td></tr>";
        str = str + "</table>";
        str = str + strParameter;
        str = str + "<br/><table width='50%' border='1' cellpadding='0' cellspacing='0'>";
        str = str + "<tr><td >From Month:</td>";
        str = str + "<td style='font-size:small;font-weight:bold;'>" + drpFromMonth.SelectedItem.Text.ToString().Replace("Select All", "All") + "</td>";
        //str = str + "<td style='font-size:small;font-weight:bold;'>" + drpFromYear.SelectedItem.Text.ToString().Replace("Select All", "All") + "</td>";
        str = str + " <td>To Month:</td>";
        str = str + "<td style='font-size:small;font-weight:bold;'>" + drpToMonth.SelectedItem.Text.ToString().Replace("Select All", "All") + "</td>";
        //str = str + "<td style='font-size:small;font-weight:bold;'>" + drpToYear.SelectedItem.Text.ToString().Replace("Select All", "All") + "</td>";
        str = str + "<td >Item Group:</td>";
        str = str + "<td style='font-size:small;font-weight:bold;'>" + drpItemGroup.SelectedItem.Text.ToString().Replace("Select All", "All") + "</td></tr>";
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


    public string getSalesMonthYear(int ProductionMonth)
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

        string strSalesMonthYear = CurrentMonthID + "-" + strCurrentYearID;

        return strSalesMonthYear;
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

   
   
}
