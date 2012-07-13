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
using System.IO;

public partial class View_Forms_Reports_ExportReports : System.Web.UI.Page
{
    private GridViewHelper helper;
    QueryConroller objQueryConroller = new QueryConroller();
  //  CommonController objCommonController = new CommonController();
    DataTable dtData;
    string strQuery = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        helper = new GridViewHelper(this.gvData, false);
        // Export the details of specified columns
        try
        {
            if (Session["oExportQuery"] != null && Convert.ToString(Session["oExportQuery"]) != "")
            {
                strQuery = Convert.ToString(Session["oExportQuery"]);

                dtData = objQueryConroller.ExecuteQuery(strQuery);

                string[] cols = { "Model" };
                helper.RegisterGroup(cols, true, true);
                helper.GroupHeader += helper_GroupHeader;


                gvData.DataSource = dtData;
                gvData.DataBind();

                

                Session["oExportQuery"] = null;

                btnExport_Click(sender, e); // Call the Export Data...  
            }
        }
        catch
        { }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        ExportGridView("AcrReport.xls");
    }
    public void ExportGridView(string fileName)
    {
        string Finalstring = "";
        string attachment = "attachment; filename=" + fileName + ".xls";
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.AddHeader("content-disposition", attachment);
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        //HttpContext.Current.Response.Write(strGrid);
        StringWriter stringWrite = new StringWriter();
        HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        gvData.RenderControl(htmlWrite);
        Finalstring = stringWrite.ToString();
        //Finalstring = objCommonController.ReplaceComponentWTEnd("<a ", ">", Finalstring);

        Response.Write(Finalstring);
        HttpContext.Current.Response.End();
    }

    private void helper_GroupHeader(string groupName, object[] values, GridViewRow row)
    {
        // To change the colour of Group header in grid
        row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
        row.Cells[0].Text = ":" + values[0].ToString();
        row.BackColor = System.Drawing.Color.Yellow;
    }
    public void eventhandlerSerialNo(object Sender, EventArgs E)
    {
        int i = 1;
        int pi = gvData.PageIndex;
        int ps = gvData.PageSize;
        if (pi > 0)
        {
            i = pi * ps + 1;
        }
        foreach (GridViewRow gr in gvData.Rows)
        {
            gr.Cells[0].Text = i.ToString();

            i++;
        }
    }
}
