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

public partial class View_Forms_Reports_Report : System.Web.UI.Page
{
    QueryConroller objQueryController = new QueryConroller();

    public string strProjectName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        strProjectName = System.Configuration.ConfigurationManager.AppSettings["WMSProjectName"];

       
            DataTable dtGridData = new DataTable();
            string table = Request.QueryString["file"];
            string strQuery = "";
            if (table == "acr")
            {
                checkException();
                strQuery = "select * from acr order by WCDOCNO";
                dtGridData = objQueryController.ExecuteQuery(strQuery);
                grdSalesData.Visible = false;
                grdProdData.Visible = false;
                grdacrData.Visible = true;
                grdData.Visible = false;
                grdacrData.DataSource = dtGridData;
                grdacrData.DataBind();
              
            }
            else if (table == "sales")
            {
                strQuery = "select * from sales order by Sno";
                dtGridData = objQueryController.ExecuteQuery(strQuery);
                grdSalesData.Visible = true;
                grdProdData.Visible = false;
                grdacrData.Visible = false;
                grdData.Visible = false;
                grdSalesData.DataSource = dtGridData;
                grdSalesData.DataBind();
            }
            else if (table == "production")
            {
                strQuery = "select * from production";
                dtGridData = objQueryController.ExecuteQuery(strQuery);
                grdSalesData.Visible = false;
                grdProdData.Visible = true;
                grdacrData.Visible = false;
                grdData.Visible = false;
                grdProdData.DataSource = dtGridData;
                grdProdData.DataBind();
            }
            else if (table == "culprit")
            {
                strQuery = "select * from culprit order by Code";
                dtGridData = objQueryController.ExecuteQuery(strQuery);
                grdSalesData.Visible = false;
                grdProdData.Visible = false;
                grdacrData.Visible = false;
                grdData.Visible = true;
                grdData.DataSource = dtGridData;
                grdData.DataBind();
            }
            else if (table == "custvoice")
            {
                strQuery = "select * from CustomerVoice order by Code";
                dtGridData = objQueryController.ExecuteQuery(strQuery);
                grdSalesData.Visible = false;
                grdProdData.Visible = false;
                grdacrData.Visible = false;
                grdData.Visible = true;
                grdData.DataSource = dtGridData;
                grdData.DataBind();
            }
            else if (table == "item")
            {
                strQuery = "select * from item order by Code";
                dtGridData = objQueryController.ExecuteQuery(strQuery);
                grdSalesData.Visible = false;
                grdProdData.Visible = false;
                grdacrData.Visible = false;
                grdData.Visible = true;
                grdData.DataSource = dtGridData;
                grdData.DataBind();
            }
            else if (table == "defect")
            {
                strQuery = "select * from defect order by code";
                dtGridData = objQueryController.ExecuteQuery(strQuery);
                grdSalesData.Visible = false;
                grdProdData.Visible = false;
                grdacrData.Visible = false;
                grdData.Visible = true;
                grdData.DataSource = dtGridData;
                grdData.DataBind();
            }
            if (dtGridData != null)
            {
                if (dtGridData.Rows.Count > 0)
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


    public void checkException()
    {
        int countRow = objQueryController.getNoOfException("execute usp_countException");
        if (countRow > 0)
        {
            string strjscript = "<script language='javascript' type='text/javascript'>";
            strjscript += " checkException();";
            strjscript += "</script" + ">";
            literal1.Text = strjscript;

        }
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
    public void eventhandlerSerialNo1(object Sender, EventArgs E)
    {
        int i = 1;
        int pi = grdSalesData.PageIndex;
        int ps = grdSalesData.PageSize;
        if (pi > 0)
        {
            i = pi * ps + 1;
        }
        foreach (GridViewRow gr in grdSalesData.Rows)
        {
            gr.Cells[0].Text = i.ToString();

            i++;
        }
    }
    public void eventhandlerSerialNo2(object Sender, EventArgs E)
    {
        int i = 1;
        int pi = grdProdData.PageIndex;
        int ps = grdProdData.PageSize;
        if (pi > 0)
        {
            i = pi * ps + 1;
        }
        foreach (GridViewRow gr in grdProdData.Rows)
        {
            gr.Cells[0].Text = i.ToString();

            i++;
        }
    }
    public void eventhandlerSerialNo3(object Sender, EventArgs E)
    {
        int i = 1;
        int pi = grdData.PageIndex;
        int ps = grdData.PageSize;
        if (pi > 0)
        {
            i = pi * ps + 1;
        }
        foreach (GridViewRow gr in grdData.Rows)
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
        objExport.ExportGridView(hdnExport.Value);

    }
    /***************************************************************************************************/
}
