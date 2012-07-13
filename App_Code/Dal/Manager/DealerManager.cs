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
/// Summary description for DealerManager
/// </summary>
public class DealerManager
{
    static DealerManager instance = null;

    public static DealerManager Instance
    {
        get
        {
            if (instance == null)
                instance = new DealerManager();
            return instance;
        }
    }

    public int SaveDealer(DealerDB objDB, SqlTransaction objTrans)
    {
        int DealerID = 0;
        DataAccessLayer objDataLayer = new DataAccessLayer();
        SqlCommand objCmd = new SqlCommand();
        try
        {
            if (objDB.CheckID == 0)
            {
                objCmd.CommandText = "usp_SaveDealer";
            }
            else
            {
                objCmd.CommandText = "usp_UpdateDealer";
                objCmd.Parameters.AddWithValue("@Id", objDB.Id);
            }

            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.Parameters.AddWithValue("@Dealer", objDB.Dealer);
            objCmd.Parameters.AddWithValue("@Code", objDB.Code);
            objCmd.Parameters.AddWithValue("@RegionID", objDB.RegionID);
            objCmd.Parameters.AddWithValue("@City", objDB.City);
            objCmd.Parameters.AddWithValue("@InstallerName", objDB.InstallerName);
            objCmd.Parameters.AddWithValue("@IsActive", objDB.IsActive);
            objCmd.Parameters.AddWithValue("@IsOperatingDealer", objDB.IsOperatingDealer);
            objCmd.Parameters.AddWithValue("@DealerID", SqlDbType.Int);
            objCmd.Parameters["@DealerID"].Direction = ParameterDirection.Output;

            objCmd.Transaction = objTrans;
            objCmd.Connection = objTrans.Connection;
            objDataLayer.Command = objCmd;
            objDataLayer.ExecQuery();
            DealerID = Convert.ToInt32(objCmd.Parameters["@DealerID"].Value);
            return DealerID;
        }
        catch (Exception ex)
        {

            throw ex;
        }



    }
}
