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

public partial class View_Forms_Reports_PlaceWiseReport : System.Web.UI.Page
{
    QueryConroller objQuerycontroller = new QueryConroller();
    public string strProjectName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        strProjectName = System.Configuration.ConfigurationManager.AppSettings["WMSProjectName"];


    }

    public void bindData()
    {

        DataTable dtinformation = getTable();
        if (dtinformation != null)
        {
            if (dtinformation.Rows.Count > 0)
            {
                gdPlace.DataSource = dtinformation;
               btnExport.Visible = true;
               btnPrint.Visible = true;
               gdPlace.DataBind();

            }
            else
            {
                gdPlace.DataSource = null;
                btnExport.Visible = false;
                btnPrint.Visible = false;
                gdPlace.DataBind();
            }
        }
        else
        {
            gdPlace.DataSource = null;
            btnExport.Visible = false;
            btnPrint.Visible = false;
            gdPlace.DataBind();
        }
    }

    public DataTable getTable()
    {
        string strQuery = "";
        DataTable dtinformation = new DataTable();
        if ((ddlPlace.SelectedValue) == "A")
        {
            strQuery = "select  * from vw_ACRGroupDetail where IsEngine=1 and Engine='" + ddlPlace.SelectedValue + "'  ";
        }
        else
        {
            if ((ddlPlace.SelectedValue) == "B")
            {
                strQuery = "select * from ACR where   (Engine='A' and IsEngine=0) or Engine='s' ";

            }
            else
            {
                strQuery = "select * from vw_ACRGroupDetail ";
            }
        }
        dtinformation = objQuerycontroller.ExecuteQuery(strQuery);
        return dtinformation;
    }

    public void grdacrData_Paging(Object sender, GridViewPageEventArgs e)
    {
        DataTable dtGridData = getTable();
        gdPlace.PageIndex = e.NewPageIndex;
        gdPlace.DataSource = dtGridData;
        gdPlace.DataBind();

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

  
    protected void btnGo_Click(object sender, EventArgs e)
    {
        bindData(); 
    }

    public void eventhandlerSerialNo(object Sender, EventArgs E)
    {
        int i = 1;
        int pi = gdPlace.PageIndex;
        int ps = gdPlace.PageSize;
        //<><> Use Name of Your GridView Instead Of gvDetailProspect <><>// 
        if (pi > 0)
        {
            i = pi * ps + 1;
        }
        foreach (GridViewRow gr in gdPlace.Rows)
        {
            gr.Cells[0].Text = i.ToString();

            i++;
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        GridViewExport objExport = new GridViewExport();
        objExport.ExportGridView(hdnExport.Value);

    }
    
}
