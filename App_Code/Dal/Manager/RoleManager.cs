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
/// Summary description for RoleManager
/// </summary>
public class RoleManager
{
    static RoleManager instance = null;

    public static RoleManager Instance
    {
        get
        {
            if (instance == null)
                instance = new RoleManager();
            return instance;
        }
    }

    public int SaveRole(RoleDB objDB, SqlTransaction objTrans)
    {
        int RoleID = 0;
        DataAccessLayer objDataLayer = new DataAccessLayer();
        SqlCommand objCmd = new SqlCommand();
        try
        {
            if (objDB.CheckID == 0)
            {
                objCmd.CommandText = "usp_SaveRole";
            }
            else
            {
                objCmd.CommandText = "usp_UpdateRole";
                objCmd.Parameters.AddWithValue("@Id", objDB.Id);
            }
            
            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.Parameters.AddWithValue("@Role", objDB.Role);

            objCmd.Parameters.AddWithValue("@IsActive", objDB.IsActive);

            objCmd.Parameters.AddWithValue("@RoleID", SqlDbType.Int);
            objCmd.Parameters["@RoleID"].Direction = ParameterDirection.Output;

            objCmd.Transaction = objTrans;
            objCmd.Connection = objTrans.Connection;
            objDataLayer.Command = objCmd;
            objDataLayer.ExecQuery();
            RoleID = Convert.ToInt32(objCmd.Parameters["@RoleID"].Value);
            return RoleID;
        }
        catch (Exception ex)
        {

            throw ex;
        }



    }
}