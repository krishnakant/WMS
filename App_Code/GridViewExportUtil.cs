using Microsoft.VisualBasic;
using System;
using System.Data;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls; 


/// <summary>
/// Summary description for GridViewExportUtil
/// </summary>
public class GridViewExportUtil
{
	public GridViewExportUtil()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void ExportGridView(string strGrid ,string filename)
    {
        string attachment = "attachment; filename=" + filename + "Reports.xls"; 
       // string attachment = "attachment; filename=Reports.xls";
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.AddHeader("content-disposition", attachment);
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        HttpContext.Current.Response.Write(strGrid);
        HttpContext.Current.Response.End();
    }

    public string ReplaceComponent(string StartComponent, string EndComponent, string BreakComponent, string RowType, string ExportValue)
    {
        string Finalstring = "";

        if (ExportValue.Contains(StartComponent))
        {
            if (RowType == "H")
            {
                ExportValue = ExportValue.Replace(StartComponent, StartComponent + " ~");
            }
            else
            {
                ExportValue = ExportValue.Replace(StartComponent, "~");
            }

        }
        if (ExportValue.Contains(EndComponent))
        {
            ExportValue = ExportValue.Replace(EndComponent, "^");
        }
        string[] href = ExportValue.Split('~');
        Finalstring = Finalstring + href[0];
        for (int i = 1; i <= href.Length - 1; i++)
        {

            Int32 len = href[i].IndexOf(BreakComponent, 0);
            if (href[i].Contains("^"))
            {
                href[i] = href[i].Remove(0, len + 1);
            }
            Finalstring = Finalstring + href[i];

        }
        Finalstring = Finalstring.Replace("^", "");
        return Finalstring;

    }

    public string ChangeComponent(string StartComponent, string EndComponent, string ExportValue)
    {
        string Finalstring = "";
        if (ExportValue.Contains(StartComponent))
        {
            ExportValue = ExportValue.Replace(StartComponent, "~");
        }

        string[] href = ExportValue.Split('~');
        Finalstring = Finalstring + href[0];
        for (int i = 1; i <= href.Length - 1; i++)
        {
            Int32 len = href[i].IndexOf(EndComponent, 0);
            href[i] = href[i].Remove(0, len + 1);
            Finalstring = Finalstring + href[i];
        }
        Finalstring = Finalstring.Replace("^", EndComponent);
        return Finalstring;

    }



    
    public string RemoveComponent(string StartComponent, string EndComponent, string ExportValue)
    {
        string check = ExportValue;
        string Finalstring = "";
        if (check.Contains(EndComponent))
        {
            if (ExportValue.Contains(StartComponent))
            {
                ExportValue = ExportValue.Replace(StartComponent, "~");
            }
            if (ExportValue.Contains(EndComponent))
            {
                ExportValue = ExportValue.Replace(EndComponent, "^");
            }
            string[] href = ExportValue.Split('~');
            Finalstring = Finalstring + href[0];
            for (int i = 1; i <= href.Length - 1; i++)
            {
                Int32 len = href[i].IndexOf("^", 0);
                href[i] = href[i].Remove(0, len + 1);
                Finalstring = Finalstring + href[i];
            }
            Finalstring = Finalstring.Replace("^", EndComponent);

            if (check.Contains(EndComponent))
            {

                Finalstring = ChangeComponent("name", ">", Finalstring);
             

            }
        }
        return Finalstring;
    } 





}
