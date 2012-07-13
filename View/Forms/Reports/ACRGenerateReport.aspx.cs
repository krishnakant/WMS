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
using System.Collections.Generic;
using System.IO;

public partial class View_Forms_Reports_ACRGenerateReport : System.Web.UI.Page
{
    private GridViewHelper helper;
    private List<int> mQuantities = new List<int>();
    QueryConroller objQueryController = new QueryConroller();
    public string strProjectName = "";
    static DataTable dtGridData = null;
    public static string SumValue = "";
    public static string SumDefect = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        strProjectName = System.Configuration.ConfigurationManager.AppSettings["WMSProjectName"];

        if (!IsPostBack)
        {
            dtGridData = getTable();
            object sumValueCell = dtGridData.Compute("Sum(Value)", null);
            object sumDefectCell = dtGridData.Compute("Sum(Quantity)", null);
            SumValue = sumValueCell.ToString();
            SumDefect = sumDefectCell.ToString();
            getACRDetail();
        }
    }


    protected void gridView_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate))
        {
            e.Row.TabIndex = -1;
            e.Row.Attributes["onclick"] = string.Format("javascript:SelectRow(this, {0});", e.Row.RowIndex);
            e.Row.Attributes["onkeydown"] = "javascript:return SelectSibling(event);";
            e.Row.Attributes["onselectstart"] = "javascript:return false;";
        }
    } 
    public string strGroup = "";
    public void getACRDetail()
    {
            
             if (dtGridData != null)
             {
                 if (dtGridData.Rows.Count > 0)
                 {
                strGroup = "Model";
               grdacrData.Visible = true;
                grdacrData.DataSource = dtGridData;
                helper = new GridViewHelper(this.grdacrData, false);
                string[] cols = new string[1];
                cols[0] = strGroup;
                helper.RegisterGroup(cols, true, true);
                helper.GroupHeader += new GroupEvent(helper_GroupHeader);
                helper.RegisterSummary("Value", SummaryOperation.Sum, strGroup);
                helper.RegisterSummary("Quantity", SummaryOperation.Sum, strGroup);
                helper.GroupSummary += new GroupEvent(helper_GroupSummary);
                grdacrData.DataBind();
                   btnPrint.Visible = true;
                    btnExport.Visible = true;
                }
                else
                {
                    grdacrData.DataSource =null;
                    grdacrData.DataBind();
                    btnPrint.Visible = false;
                    btnExport.Visible = false;
                }
            }
            else
            {
                grdacrData.DataSource = null;
                grdacrData.DataBind();
                btnPrint.Visible = false;
                btnExport.Visible = false;
            }
   
       
    }
    public DataTable getTable()
    {
        string strQuery = "";
        if (Session["ModelCodeParameter"] != null)
        {
            string strModelCodeParameter = Session["ModelCodeParameter"].ToString();
            string strDealerCodeParameter = Session["DealerCodeParameter"].ToString();
            string strDefectCodeParameter = Session["DefectCodeParameter"].ToString();
            string strCVOICECodeParameter = Session["CVOICECodeParameter"].ToString();
            string strItemGroupIDCodeParameter = Session["ItemGroupIDCodeParameter"].ToString();
            string strCUL_CODEParameter = Session["CUL_CODEParameter"].ToString();
            string strFromDate = Session["FromDate"].ToString();
            string strModelCategoryParameter = Session["ModelCategoryParameter"].ToString();
            string strModelClutchParameter = Session["ModelClutchParameter"].ToString();
            string strModelSpecialParameter = Session["ModelSpecialParameter"].ToString();
            string strToDate = Session["ToDate"].ToString();
            string FromMonth = Session["FromMonth"].ToString();
            string ToMonth = Session["ToMonth"].ToString();
            string strParam = "";
            string strPlace = "";
            string strEngine = "";
            if (Session["place"] != null)
            {
                strPlace = Session["place"].ToString();
            }
            if (Session["Engine"] != null)
            {
                strEngine = Session["Engine"].ToString();
            }
            int status = 0;
            int HMRID = Convert.ToInt32(Session["HMRID"]);
            string strProblemType = Session["Problem"].ToString();
            if (strModelCodeParameter != "")
            {
                strParam = "(" + strModelCodeParameter + ")";
                status = 1;
            }


            if (strModelCategoryParameter != "")
            {

                if (status == 1)
                {
                    strParam = strParam + " and (" + strModelCategoryParameter + ")";
                }
                else
                {
                    strParam = "(" + strModelCategoryParameter + ")";
                    status = 1;
                }
            }


            if (strModelClutchParameter != "")
            {

                if (status == 1)
                {
                    strParam = strParam + " and (" + strModelClutchParameter + ")";
                }
                else
                {
                    strParam = "(" + strModelClutchParameter + ")";
                    status = 1;
                }
            }

            if (strModelSpecialParameter != "")
            {

                if (status == 1)
                {
                    strParam = strParam + " and (" + strModelSpecialParameter + ")";
                }
                else
                {
                    strParam = "(" + strModelSpecialParameter + ")";
                    status = 1;
                }
            }

            if (strDealerCodeParameter != "")
            {
                if (status == 1)
                {
                    strParam = strParam + " and (" + strDealerCodeParameter + ")";
                }
                else
                {
                    strParam = "(" + strDealerCodeParameter + ")";
                    status = 1;
                }
            }


            if (strDefectCodeParameter != "")
            {
                if (status == 1)
                {
                    strParam = strParam + " and (" + strDefectCodeParameter + ")";
                }
                else
                {
                    strParam = "(" + strDefectCodeParameter + ")";
                    status = 1;
                }

            }


            if (strCVOICECodeParameter != "")
            {

                if (status == 1)
                {
                    strParam = strParam + " and (" + strCVOICECodeParameter + ")";
                }
                else
                {
                    strParam = "(" + strCVOICECodeParameter + ")";
                    status = 1;
                }

            }


            if (strItemGroupIDCodeParameter != "")
            {
                if (status == 1)
                {
                    strParam = strParam + " and (" + strItemGroupIDCodeParameter + ")";
                }
                else
                {
                    strParam = "(" + strItemGroupIDCodeParameter + ")";
                    status = 1;
                }

            }


            if (strCUL_CODEParameter != "")
            {

                if (status == 1)
                {
                    strParam = strParam + " and (" + strCUL_CODEParameter + ")";
                }
                else
                {
                    strParam = "(" + strCUL_CODEParameter + ")";
                    status = 1;
                }
            }
            string strHMRRange = "";
            if (HMRID != 3)
            {
                if (HMRID == 1)
                {
                    strHMRRange = "(HMR_Range='0-250 ')";
                }
                else
                {
                    strHMRRange = "(HMR_Range='251-2500 ')";
                }
            }
            else
            {
                strHMRRange = "((HMR_Range='251-2500 ') or (HMR_Range='0-250 '))";
            }
            if (strHMRRange != "")
            {

                if (status == 1)
                {
                    strParam = strParam + " and (" + strHMRRange + ")";
                }
                else
                {
                    strParam = "(" + strHMRRange + ")";
                    status = 1;
                }
            }
            string strDataType = Session["DataType"].ToString();
            if (strDataType == "Place")
            {
                if (strPlace != "")
                {
                    if (strParam != "")
                    {
                        if (strPlace == "A")
                        {
                            strParam = strParam + " and (IsEngine=1 and Engine='A')";
                        }
                        else
                        {
                            strParam = strParam + "and ((Engine='A' and IsEngine=0) or Engine='s') ";
                        }

                    }
                    else
                    {
                        if (strPlace == "A")
                        {
                            strParam = "  (IsEngine=1 and Engine='A')";
                        }
                        else
                        {
                            strParam = "((Engine='A' and IsEngine=0) or Engine='s') ";
                        }
                    }
                }
            }
            else if (strDataType == "Engine")
            {
                if (strEngine != "")
                {
                    if (strParam != "")
                    {
                        if (strEngine == "A")
                        {
                            strParam = strParam + " and (IsEngine=1 and Engine='A')";
                        }
                        else
                        {
                            strParam = strParam + " and (IsEngine=1 and Engine='S')";
                        }

                    }
                    else
                    {
                        if (strEngine == "A")
                        {
                            strParam = strParam + " (IsEngine=1 and Engine='A')";
                        }
                        else
                        {
                            strParam = strParam + " (IsEngine=1 and Engine='S')";
                        }
                    }
                }
            }
            else
            {
                if (strParam != "")
                {
                    strParam = strParam + " and ";
                }

                strParam = strParam + " (IsEngine=0) ";
            }

            if (strProblemType != "")
            {
                if (strParam != "")
                {
                    if (strProblemType == "P")
                    {
                        strParam = strParam + " and (DT like '" + strProblemType + "%')";
                    }
                    else if (strProblemType == "C")
                    {
                        strParam = strParam + " and (DT like '" + strProblemType + "%')";
                    }

                }
                else
                {
                    if (strProblemType == "P")
                    {
                        strParam = "  (DT like '" + strProblemType + "%')";
                    }
                    else if (strProblemType == "C")
                    {
                        strParam = " (DT like '" + strProblemType + "%') ";
                    }
                }
            }
            if (strFromDate != "" && strToDate != "")
            {
                if (strParam != "")
                {
                    strParam = strParam + " and (CR_DATE between '" + strFromDate + "'  and  '" + strToDate + "') ";

                }
                else
                {

                    strParam = " and (CR_DATE between '" + strFromDate + "'  and  '" + strToDate + "' )";
                }
            }
            if (FromMonth != "" && ToMonth != "")
            {
                if (strParam != "")
                {
                    strParam = strParam + " and (Production_Month between '" + FromMonth + "'  and  '" + ToMonth + "') ";

                }
                else
                {

                    strParam = " and (Production_Month between '" + FromMonth + "'  and  '" + ToMonth + "') ";
                }
            }


       
            if (strParam != "")
            {
                //vw_DealerwiseAcrBulk
                //strQuery = "select distinct 'All' as Model,ID,[WCDOCNO],[DLR_REF],[TRACTOR_NO],[ENGINE_NO],[INS_DATE],[DEF_DATE],[REP_DATE],[HMR],[DEALER_NAME],[DLR_CO],[REG],[CR_DATE],[ITEM_CODE],[DESCRIPTION],[QUANTITY],[COST],[DEF] ,[NDP] ,[VALUE],[CVOICE],[OUTLV],[DT],[CUL_CODE] ,[CR_AMOUNT],[AUTH_AMOUNT] ,[DIFF],[HMR_Range],[Engine],[IsEngine],[Material],[WarrantyPeriod]  from vw_AcrBulkGroupDetail where " + strParam + "";
                strQuery = "select distinct 'All' as Model,ID,[WCDOCNO],[DLR_REF],[TRACTOR_NO],[ENGINE_NO],[INS_DATE],[DEF_DATE],[REP_DATE],[HMR],[DEALER_NAME],[DLR_CO],[REG],[CR_DATE],[ITEM_CODE],[DESCRIPTION],[QUANTITY],[COST],[DEF] ,[NDP] ,[VALUE],[CVOICE],[OUTLV],[DT],[CUL_CODE] ,[CR_AMOUNT],[AUTH_AMOUNT] ,[DIFF],[HMR_Range],[Engine],[IsEngine],[Material],[WarrantyPeriod]  from vw_DealerwiseAcrBulk where " + strParam + "";
            }
        }

       DataTable dt = objQueryController.ExecuteQuery(strQuery);
      
        return dt;
    }
    public void grdacrData_Paging(Object sender, GridViewPageEventArgs e)
    {

       
        grdacrData.PageIndex = e.NewPageIndex;
        strGroup = "Model";
        grdacrData.Visible = true;
        grdacrData.DataSource = dtGridData;
        helper = new GridViewHelper(this.grdacrData, false);
        string[] cols = new string[1];
        cols[0] = strGroup;
        helper.RegisterGroup(cols, true, true);
        helper.GroupHeader += new GroupEvent(helper_GroupHeader);
        helper.RegisterSummary("Value", SummaryOperation.Sum, strGroup);
        helper.RegisterSummary("Quantity", SummaryOperation.Sum, strGroup);
        helper.GroupSummary += new GroupEvent(helper_GroupSummary);
       
        grdacrData.DataBind();
        if (grdacrData != null)
        {
            if (grdacrData.Rows.Count > 0)
            {
                btnPrint.Visible = true;
                btnExport.Visible = true;
            }
            else
            {
                btnPrint.Visible = false;
                btnExport.Visible = false;
            }
        }
        else
        {
            btnPrint.Visible = false;
            btnExport.Visible = false;
        }

    }

    private void helper_GroupHeader(string groupName, object[] values, GridViewRow row)
    {
        if (groupName == strGroup)
        {
            row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
            row.Cells[0].Text = ":" + values[0].ToString();
            row.BackColor = System.Drawing.Color.Yellow;
        }
    }
    private void helper_GroupSummary(string groupName, object[] values, GridViewRow row)
    {
        row.Cells[0].HorizontalAlign = HorizontalAlign.Right;
        row.Cells[0].Text = "Total:";
        row.Cells[0].ForeColor = System.Drawing.Color.Black;
    }
    public void eventhandlerSerialNo(object Sender, EventArgs E)
    {
        int i = 1;
        int pi = grdacrData.PageIndex;
        int ps = grdacrData.PageSize;
        if (pi > 0)
        {
            i = pi * ps + 1;
        }
        foreach (GridViewRow gr in grdacrData.Rows)
        {
            gr.Cells[0].Text = i.ToString();

            i++;
        }
    }
    /***************************************************************************************************/
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    /*******************************Exporting Record Into Excel*****************************************/
    protected void Button1_Click(object sender, EventArgs e)
    {
        GridViewExport objExport = new GridViewExport();
        //objExport.ExportGridView(hdnExport.Value);
        grdacrData.ShowFooter = false;
        grdacrData.AllowPaging = false;
        grdacrData.AllowSorting = false;

        strGroup = "Model";
        grdacrData.Visible = true;
        grdacrData.PageIndex = 0;
        grdacrData.DataSource = dtGridData;
        helper = new GridViewHelper(this.grdacrData, false);
        string[] cols = new string[1];
        cols[0] = strGroup;
        helper.RegisterGroup(cols, true, true);
        helper.GroupHeader += new GroupEvent(helper_GroupHeader);
        helper.RegisterSummary("Value", SummaryOperation.Sum, strGroup);
        helper.RegisterSummary("Quantity", SummaryOperation.Sum, strGroup);
        helper.GroupSummary += new GroupEvent(helper_GroupSummary);
        grdacrData.DataBind();

        PrepareGridViewForExport(grdacrData);
        ExportGridView();

    }
    /***************************************************************************************************/

    private void PrepareGridViewForExport(Control gv)
    {

        LinkButton lb = new LinkButton();

        Literal l = new Literal();

        string name = String.Empty;

        for (int i = 0; i <= gv.Controls.Count - 1; i++)
        {

            if (object.ReferenceEquals(gv.Controls[i].GetType(), typeof(LinkButton)))
            {

                l.Text = (gv.Controls[i] as LinkButton).Text;

                gv.Controls.Remove(gv.Controls[i]);



                gv.Controls.AddAt(i, l);
            }
            else if (object.ReferenceEquals(gv.Controls[i].GetType(), typeof(DropDownList)))
            {

                l.Text = (gv.Controls[i] as DropDownList).SelectedItem.Text;


                gv.Controls.Remove(gv.Controls[i]);



                gv.Controls.AddAt(i, l);
            }
            else if (object.ReferenceEquals(gv.Controls[i].GetType(), typeof(CheckBox)))
            {

                l.Text = (gv.Controls[i] as CheckBox).Checked ? "True" : "False";

                gv.Controls.Remove(gv.Controls[i]);


                gv.Controls.AddAt(i, l);
            }

            if (gv.Controls[i].HasControls())
            {



                PrepareGridViewForExport(gv.Controls[i]);
            }
        }
    }


    private void ExportGridView()
    {
        btnExport.Enabled = false;

        string attachment = "attachment; filename=Report.xls";

        Response.ClearContent();

        Response.AddHeader("content-disposition", attachment);

        Response.ContentType = "application/ms-excel";

        StringWriter sw = new StringWriter();

        HtmlTextWriter htw = new HtmlTextWriter(sw);

        grdacrData.RenderControl(htw);
        grdacrData.ShowFooter = true;


        string strGrid = " <h2>ACR Dynamic Report " + System.DateTime.Now.ToString() + " </h2><table border='2' cellpadding='3' bordercolor='#000000' style='font-size:8px;font-family:Arial;margin-top:5px;margin-left:5px;margin-right:5px;margin-bottom:5px;left:5px;'><tr><td> " + sw.ToString() + "</td></tr></table>";
        //strGrid = strGrid.Replace("border-collapse:collapse;", "");
        //strGrid = strGrid.Replace("style=\"color:#F0F0F0;\"", "style=\"color:#000000;\"");
        //strGrid = strGrid.Replace("<input type=\"image\"", "<%--");

        //strGrid = strGrid.Replace("border=\"0\"", "border=\"1\"");
        //strGrid = strGrid.Replace("MoveFirstHS.png\" style=\"border-width:0px;\" />", "MoveFirstHS.png\"--%> ");
        //strGrid = strGrid.Replace("MovePreviousHS.png\" style=\"border-width:0px;\" />", "MovePreviousHS.png\"--%> ");
        //strGrid = strGrid.Replace("MoveNextHS.png\" style=\"border-width:0px;\" />", "MoveNextHS.png\"--%> ");
        //strGrid = strGrid.Replace("MoveLastHS.png\" style=\"border-width:0px;\" />", "MoveLastHS.png\"--%> ");
        //strGrid = strGrid.Replace("<div>", "<div style='border:1px;border-color:#000000;'>");

        Response.Write(strGrid);


        Response.End();
    }
    
}
