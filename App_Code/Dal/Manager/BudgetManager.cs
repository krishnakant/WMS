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
/// Summary description for BudgetManager
/// </summary>
public class BudgetManager
{
    static BudgetManager instance = null;

    public static BudgetManager Instance
    {
        get
        {
            if (instance == null)
                instance = new BudgetManager();
            return instance;
        }
    }

    public int SaveBudget(BudgetDB objDB, SqlTransaction objTrans)
    {
        int ID = 0;
        DataAccessLayer objDataLayer = new DataAccessLayer();
        SqlCommand objCmd = new SqlCommand();
        try
        {

            objCmd.CommandText = "usp_SaveBudget";
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.AddWithValue("@FinancialYear", objDB.FinancialYear);
            objCmd.Parameters.AddWithValue("@QuarterID", objDB.QuarterID);
            objCmd.Parameters.AddWithValue("@Budget", objDB.Budget);
            objCmd.Parameters.AddWithValue("@ModelGroupID", objDB.ModelGroupID);
            objCmd.Parameters.AddWithValue("@ModelCategoryID", objDB.ModelCategoryID);
            objCmd.Parameters.AddWithValue("@ModelClutchID", objDB.ModelClutchID);
            objCmd.Parameters.AddWithValue("@ModelSpecialID", objDB.ModelSpecialID);

            objCmd.Parameters.AddWithValue("@ID", SqlDbType.Int);
            objCmd.Parameters["@ID"].Direction = ParameterDirection.Output;

            objCmd.Transaction = objTrans;
            objCmd.Connection = objTrans.Connection;
            objDataLayer.Command = objCmd;
            objDataLayer.ExecQuery();
            ID = Convert.ToInt32(objCmd.Parameters["@ID"].Value);
            return ID;
        }
        catch (Exception ex)
        {

            throw ex;
        }



    }
}