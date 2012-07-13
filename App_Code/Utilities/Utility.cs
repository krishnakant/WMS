using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.Odbc;
using System.Text;
using System.IO;

/// <summary>
/// Summary description for Utility
/// </summary>
public class Utility
{
	public Utility()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int getExcelDatatosql(string Path, string tblName, string sConnectionString, string EDate, SqlTransaction objtrans)
    {
        int result = 0;
        DataTable dt = new DataTable();
        bool flagTransation = true;
        DataAccessLayer objDataAccess = new DataAccessLayer();

        if (objtrans == null)
        {
            flagTransation = false;
            objDataAccess.GetConnection.Open();
            SqlTransaction objTransaction = objDataAccess.GetConnection.BeginTransaction();
            objtrans = objTransaction;
        }

        string Name = "";
        string excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Path;
        excelConnectionString += ";Extended Properties=\"Excel 8.0;IMEX=1;HDR=YES;\"";
        OleDbConnection connection = new OleDbConnection(excelConnectionString);
        try
        {
            connection.Open();
            DataTable SheetName = new DataTable();
            SheetName = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string[] excelSheets = new string[SheetName.Rows.Count];
            int i = 0;
            // Add the sheet name to the string array. 
            foreach (DataRow row in SheetName.Rows)
            {
                excelSheets[i] = row["TABLE_NAME"].ToString();
                Name = excelSheets[i];
                break;
            }

            if (tblName == "DealerTemp")
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "delete from DealerTemp";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = objtrans.Connection;
                cmd.Transaction = objtrans;
                cmd.ExecuteNonQuery();
            }

            string query = "select distinct '" + EDate + "' as EDate,Code,Name,Region,city FROM [" + Name + "] ";
            OleDbCommand ocommand = new OleDbCommand(query, connection);
            ocommand.CommandTimeout = 1000;
            OleDbDataReader dr = ocommand.ExecuteReader();
            try
            {
                SqlConnection sqlconn = new SqlConnection(sConnectionString);
                sqlconn.Open();
                SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlconn);
                bulkCopy.DestinationTableName = tblName;
                bulkCopy.WriteToServer(dr);
                sqlconn.Close();
                dr.Close();
                if (tblName == "DealerTemp")
                {
                    SqlCommand cmd4 = new SqlCommand();
                    cmd4.CommandTimeout = 1000;
                    cmd4.CommandText = "usp_insertDealerData";
                    cmd4.CommandType = CommandType.StoredProcedure;

                    cmd4.Connection = objtrans.Connection;
                    cmd4.Transaction = objtrans;
                    cmd4.ExecuteNonQuery();
                    cmd4.Dispose();
                }
          
                
                result = 1;
                objtrans.Commit();
            }

            catch (Exception ex)
            {
                objtrans.Rollback();
            }
            finally
            {
                dr.Close();
            }
        }

        catch (Exception ex)
        {
            connection.Close();
        }

        connection.Close();
        return result;

    }
    public string ConvertDateTime(string strDate)
    {
        string strDateTemp = "";
        string[] strDateArray = strDate.Split('/');
        if (strDateArray.Length == 3)
        {
            if (strDateArray[1].Length == 1)
            {
                strDateArray[1] = "0" + strDateArray[1];
            }
            if (strDateArray[0].Length == 1)
            {
                strDateArray[0] = "0" + strDateArray[0];
            }
            strDateTemp = strDateArray[1] + "/" + strDateArray[0] + "/" + strDateArray[2];
        }
        else
        {
            strDateTemp = "01/01/1900";
        }
        return strDateTemp;
    } 
}
