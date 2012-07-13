using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for GridViewExport
/// </summary>
public class GridViewExport
{
	public GridViewExport()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void ExportGridView(string strGrid)
    {
        string attachment= "attachment; filename=Reports.xls";
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.AddHeader("content-disposition", attachment);
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        HttpContext.Current.Response.Write(strGrid);
        HttpContext.Current.Response.End();
    }
}
