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
/// Summary description for ReworkCostManager
/// </summary>
public class ReworkCostManager
{
    static ReworkCostManager instance = null;

    public static ReworkCostManager Instance
    {
        get
        {
            if (instance == null)
                instance = new ReworkCostManager();
            return instance;
        }
    }

    public int SaveReworkCost(ReworkCostDB objDB, SqlTransaction objTrans)
    {
        int ID = 0;
        DataAccessLayer objDataLayer = new DataAccessLayer();
        SqlCommand objCmd = new SqlCommand();
        try
        {

            objCmd.CommandText = "usp_SaveReworkCost";
            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.Parameters.AddWithValue("@MonthID", objDB.MonthID);
            objCmd.Parameters.AddWithValue("@YearID", objDB.YearID);
            objCmd.Parameters.AddWithValue("@ModelGroupID", objDB.GroupID);
            objCmd.Parameters.AddWithValue("@ReworkCost_I_Year", objDB.ReworkCost_I_Year);
            objCmd.Parameters.AddWithValue("@ReworkCost_II_Year", objDB.ReworkCost_II_Year);
            objCmd.Parameters.AddWithValue("@ModelCategoryID", objDB.ModelCategoryID);
            objCmd.Parameters.AddWithValue("@HMR_Range", objDB.HMR_Range);
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