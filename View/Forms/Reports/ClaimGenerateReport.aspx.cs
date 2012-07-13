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
using System.Drawing;

public partial class View_Forms_Reports_ClaimGenerateReport : System.Web.UI.Page
{
    QueryConroller objQueryController = new QueryConroller();
    private GridViewHelper helper;
    protected int count = 1;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getParameters();
            getSelection();
        }
        
    }

    public void getSelection()
    {
        int IsNew = Convert.ToInt16(Session["ProductType"].ToString());
        string strProduct = "";
        if (IsNew == 0)
        {
            strProduct = "Regular";
        }
        else if (IsNew == 1)
        {
            strProduct = "New";
        }
        else
        {
            strProduct = "All";
        }

        DataTable dt = new DataTable();
        dt.Columns.Add("From Date:");
        dt.Columns.Add("To Date:");
        dt.Columns.Add("Product:");
        dt.Columns.Add("HMR:");
        //dt.Columns.Add("Report Type:");
        DataRow dr = dt.NewRow();
        dr[0] = Session["FromDate"].ToString();
        dr[1] = Session["ToDate"].ToString();
        dr[2] = strProduct;
        dr[3] = Session["HMR_Range"].ToString();
        dt.Rows.Add(dr);
        dtlClaimDetail.DataSource = dt;
        dtlClaimDetail.DataBind();
       
    }

    public void getParameters()
    {
        int IsNew = Convert.ToInt16(Session["ProductType"].ToString());
        string strReportType = Session["ReportType"].ToString();
        string strHMR_Range = Session["HMR_Range"].ToString();
        string strFromDate = Session["FromDate"].ToString();
        string strToDate = Session["ToDate"].ToString();


        
        string strSelectList = " Model_Code,Sum([Value]) as [Value],Sum(Defect) as Defect,GroupName";
        string strParameter = "";
        int flag = 0;
       // int Selectflag = 0;
        if (IsNew != 2)   //2 stands for ALL
        {
            strParameter += " IsNew = " + IsNew + "";
            flag = 1;
        }
        else
        {
            //strSelectList += ",IsNew ";
           // Selectflag++;
        }

        if (strHMR_Range != "All")
        {
            if (flag == 1)
            {
                strParameter += " and ";
            }
            strParameter += "HMR_Range = '" + strHMR_Range + "'";
            flag = 1;
        }
        else
        {
            strSelectList += ",HMR_Range ";
        }
        if (strReportType != "Cost")
        {
            if (flag == 1)
            {
                strParameter += " and ";
            }
            strParameter += strReportType;
            flag = 1;
        }

        if (strFromDate == "" && strToDate == "")
        {
            if (flag == 1)
            {
                strParameter += " and ";
            }
            strParameter += " DEF_DATE between '" + Convert.ToDateTime(strFromDate) + "' and '" + Convert.ToDateTime(strToDate) + "' ";
        }
        string strQuery = strQuery = "Select " + strSelectList + " from vw_CostDefect "; 
        if (strParameter != "")
        {
            
           strQuery += " where " + strParameter ;
        }
        strQuery += " group by Model_Code,GroupName,HMR_Range";
        strQuery += " order by Model_Code";

        DataTable dtClaim = new DataTable();
        dtClaim = objQueryController.ExecuteQuery(strQuery);
        grdClaimDetail.DataSource = dtClaim;
        helper = new GridViewHelper(this.grdClaimDetail, false);
        group();
        grdClaimDetail.DataBind();
    }

    private void group()
    {
        string cols = "Model_Code";
        helper.RegisterGroup(cols, true, true);
        helper.GroupHeader += new GroupEvent(helper_GroupHeader);
        helper.RegisterSummary("Value", SummaryOperation.Sum, cols);
        helper.RegisterSummary("Defect", SummaryOperation.Sum, cols);
        helper.GroupSummary += new GroupEvent(helper_GroupSummary);
        //helper.GroupHeader += helper_Bug;
        helper.ApplyGroupSort();


    }


    private void helper_GroupHeader(string groupName, object[] values, GridViewRow row)
    {
        if (groupName == "Model_Code")
        {
            row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
            row.Cells[0].Text = values[0].ToString();
            row.BackColor = System.Drawing.Color.Turquoise;
        }
        
    }

    private void helper_GroupSummary(string groupName, object[] values, GridViewRow row)
    {

        row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
        //row.Cells[0].Text = "Sub Total :";
        //row.Cells[0].HorizontalAlign = HorizontalAlign.Right;
        row.BackColor = Color.Aqua;
        row.ForeColor = Color.Blue;
        row.Font.Bold = true;
    }


    private void helper_Bug(string groupName, object[] values, GridViewRow row)
    {

        if (groupName == null) return;

        //row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
        row.Cells[0].Text = count.ToString() + ". " + row.Cells[0].Text;
        count = count + 1;

    }


    protected int Value = 0;
    protected int Defect = 0;


    public void eventhandlerSerialNo(object Sender, EventArgs E)
    {
        int i = 1;
        int pi = grdClaimDetail.PageIndex;
        int ps = grdClaimDetail.PageSize;
        if (pi > 0)
        {
            i = pi * ps + 1;
        }
        foreach (GridViewRow gr in grdClaimDetail.Rows)
        {
            gr.Cells[0].Text = i.ToString();

            i++;
        }
    }

    protected void grdClaimDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Value += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Value").ToString());
            Defect += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Defect").ToString());
                       

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            

            e.Row.BackColor = Color.CadetBlue;
            e.Row.ForeColor = Color.Snow;
            e.Row.Font.Bold = true;
            e.Row.Cells[0].Text = "Grand Total :";
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[1].Text = Value.ToString();
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[2].Text = Defect.ToString();
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
        }
    }

    public void SortingData(object sender, GridViewSortEventArgs e)
    {

    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        GridViewExport objExport = new GridViewExport();
        objExport.ExportGridView(hdnExport.Value);

    }

}
