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
/// Summary description for ProductionManager
/// </summary>
public class ProductionManager
{
    static ProductionManager instance = null;
    public ProductionManager()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static ProductionManager Instance
    {
        get
        {
            if (instance == null)
                instance = new ProductionManager();
            return instance;
        }
    }

    public void SaveProduction(ProductionDB objDB, SqlTransaction objTrans)
    {
        DataAccessLayer objDataLayer = new DataAccessLayer();
        SqlCommand objCmd = new SqlCommand();
        try
        {
            objCmd.CommandText = "usp_ProductionInsert";
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.AddWithValue("@S", objDB.S);
            objCmd.Parameters.AddWithValue("@Material", objDB.Material);
            objCmd.Parameters.AddWithValue("@SerialNo", objDB.SerialNo);
            objCmd.Parameters.AddWithValue("@Plnt", objDB.Plnt);
            objCmd.Parameters.AddWithValue("@SLoc", objDB.SLoc);
            objCmd.Parameters.AddWithValue("@Description", objDB.Description);
            objCmd.Parameters.AddWithValue("@Production_Month", objDB.Production_Month);
            objCmd.Parameters.AddWithValue("@Production_Month_Year", objDB.Production_Month_Year);
            //objCmd.Parameters.AddWithValue("@Model_Code", objDB.Model_Code);
            objCmd.Parameters.AddWithValue("@FromDate", objDB.FromDate);
            objCmd.Parameters.AddWithValue("@ToDate", objDB.ToDate);
            objCmd.Parameters.AddWithValue("@MonthID", objDB.MonthID);
            objCmd.Parameters.AddWithValue("@YearID", objDB.YearID);
            objCmd.Parameters.AddWithValue("@Quarter", objDB.Quarter);
            objCmd.Parameters.AddWithValue("@ModelMappingID", objDB.ModelMappingID);
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

    public void SaveProductionData(SqlTransaction objTrans)
    {
        DataAccessLayer objDataLayer = new DataAccessLayer();
        SqlCommand objCmd = new SqlCommand();
        try
        {
            objCmd.CommandText = "usp_ProductionDataInsert";
            objCmd.CommandType = CommandType.StoredProcedure;

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
