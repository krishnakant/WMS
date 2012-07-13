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
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Office.Core;
using System.IO;


public partial class View_Forms_Master_UpldTest : System.Web.UI.Page
{
    public string strProjectName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        strProjectName = System.Configuration.ConfigurationManager.AppSettings["WMSProjectName"];

    }
    protected void btnUpld_Click(object sender, EventArgs e)
    {
        string strPath = Server.MapPath("~/") + "UploadFile";
        if (UploadFile.HasFile)
        {
            string strAcrFilePath = strPath;
            strAcrFilePath = strAcrFilePath + "\\AcrTestfinal11.xls";
            UploadFile.SaveAs(strAcrFilePath);
        }

        Upload3();
    }


  
    public void Upload1()
    {
        string sConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        string strPath = Server.MapPath("~/") + "UploadFile\\AcrTestf.xls";
        lblTest.Text = strPath;
        string excelConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strPath;
        excelConnectionString += @";Extended Properties=""Excel 8.0;HDR=YES;IMEX=1;""";
        OleDbConnection connection = new OleDbConnection(excelConnectionString);
        connection.Open();

        string Name = "";
        try
        {
            DataTable SheetName = new DataTable();
            SheetName = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string[] excelSheets = new string[SheetName.Rows.Count];
            int i = 0;
            // Add the sheet name to the string array.
            foreach (DataRow row in SheetName.Rows)
            {

                excelSheets[i] = SheetName.Rows[0]["TABLE_NAME"].ToString();
                Name = excelSheets[i];
            }

            string query = " Select * FROM [" + Name + "] ";

            OleDbCommand command = new OleDbCommand(query, connection);
            OleDbDataReader dr = command.ExecuteReader();
            try
            {
                SqlConnection sqlconn = new SqlConnection(sConnectionString);
                sqlconn.Open();
                SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlconn);
                bulkCopy.DestinationTableName = "ENGINE_NO";
                bulkCopy.WriteToServer(dr);
                sqlconn.Close();
                dr.Close();
                connection.Close();
            }
            catch
            {
                connection.Close();
            }



        }
        catch { connection.Close(); }


    }

      public void Upload3()
    {
        string strPathnew = Server.MapPath("~/") + "UploadFile\\AcrTestfinalresult.xls";
        string strPath = Server.MapPath("~/") + "UploadFile\\AcrTestfinal11.xls";
        StreamReader sr = new StreamReader(strPath); //Read the Excel Stream
        try
        {
            DataTable dt = new DataTable();
            string strTest = "";
            int i = 1;
            while (!sr.EndOfStream)
            {
                strTest = sr.ReadLine();
                string[] strData = strTest.Split('\t');
                int count = strData.Length;

                if (i == 1)
                {
                    for (int k = 1; k <= count; k++)
                    {
                        dt.Columns.Add(strData[k - 1]);
                    }
                }
                else
                {
                    DataRow dr = dt.NewRow();
                    
                  
                    for (int k = 1; k <= count; k++)
                    {
                        string str = strData[k - 1].Replace("\"", "");
                       
                        dr[k-1] = str;
                    }
                    dt.Rows.Add(dr);
                }
                i++;
            }
            sr.Close();
            sr.Dispose();
            
        }
        catch
        {
            sr.Close();
            sr.Dispose();
        }
    }

    public void Upload()
    {
        string strPathnew = Server.MapPath("~/") + "UploadFile\\AcrTestfinalresult.xls";
        try
        {
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;

            xlWorkBook = new Excel.Application().Workbooks.Add(Missing.Value);
            xlWorkBook.Application.Visible = true;
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.ActiveSheet;
          

            string strPath = Server.MapPath("~/") + "UploadFile\\AcrTestfinal.xls";
            StreamReader sr = new StreamReader(strPath); //Read the Excel Stream
            string strTest = "";
            int i = 1;
            while (!sr.EndOfStream)
            {
               
                strTest = sr.ReadLine();
                string[] strData = strTest.Split('\t');
                int count = strData.Length;
                for (int k = 1; k <= count; k++)
                {
                    string str = strData[k - 1].Replace("\"", "");
                    if (k == 4)  //1 based index of Column required to be changed
                    {
                        str = str.Insert(0, "'");
                    }
                    xlWorkSheet.Cells[i, k] = str;
                }
                   
                
                i++;
            }
            sr.Close();
            sr.Dispose();
            xlWorkSheet.Columns.AutoFit();
            xlWorkBook.SaveAs(strPathnew , Excel.XlFileFormat.xlExcel4, Missing.Value, Missing.Value, false, false, Excel.XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            xlWorkBook.Close(Missing.Value, strPathnew, Missing.Value);

        }
        catch
        { }
    }

    public void Upload2()
    {
        string strPath = Server.MapPath("~/") + "UploadFile\\AcrTestfinal11.xls";

        string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strPath;
        connectionString += @";Extended Properties=""Excel 8.0;HDR=YES;IMEX=1;""";
        OleDbDataAdapter adapter = new OleDbDataAdapter();
        string Name = "";
        OleDbConnection connection = new OleDbConnection();

        try
        {
           

 
        
            connection.ConnectionString = connectionString;
            connection.Open();
            DataTable SheetName = new DataTable();
            SheetName = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string[] excelSheets = new string[SheetName.Rows.Count];
            int i = 0;
            // Add the sheet name to the string array.
            foreach (DataRow row in SheetName.Rows)
            {

                excelSheets[i] = SheetName.Rows[0]["TABLE_NAME"].ToString();
                Name = excelSheets[i];
            }


            OleDbCommand selectCommand = new OleDbCommand();
            selectCommand.CommandText = "Select Cast([ENGINE_NO] as number) as new FROM [" + Name + "] ";



            selectCommand.Connection = connection;
            adapter.SelectCommand = selectCommand;
            DataSet cities = new DataSet();
            adapter.Fill(cities);
            connection.Close();
        }
        catch
        {
            connection.Close();
        }
    }

}
