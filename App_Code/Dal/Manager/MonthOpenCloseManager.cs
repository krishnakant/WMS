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

/// <summary>
/// Summary description for MonthOpenCloseManager
/// </summary>
public class MonthOpenCloseManager
{
    static MonthOpenCloseManager instance = null;
    public MonthOpenCloseManager()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static MonthOpenCloseManager Instance
    {
        get
        {
            if (instance == null)
                instance = new MonthOpenCloseManager();
            return instance;
        }
    }

    public void SaveMonthOpenClose(MonthOpenCloseDB objDB, SqlTransaction objTrans)
    {
        DataAccessLayer objDataLayer = new DataAccessLayer();
        SqlCommand objCmd = new SqlCommand();
        try
        {
            objCmd.CommandText = "usp_SaveMonthOpenClose";
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.AddWithValue("@MonthID", objDB.MonthID);
            objCmd.Parameters.AddWithValue("@YearID", objDB.YearID);
            objCmd.Parameters.AddWithValue("@Status", objDB.Status);
            objCmd.Parameters.AddWithValue("@id", SqlDbType.Int);
            objCmd.Parameters["@id"].Direction = ParameterDirection.Output;
            
            objCmd.Transaction = objTrans;
            objCmd.Connection = objTrans.Connection;
            objDataLayer.Command = objCmd;
            objDataLayer.ExecQuery();

        }
        catch (Exception ex)
        {

            throw ex;
        }

    }

   

      
  
}
