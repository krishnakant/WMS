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
/// Summary description for PriviledgeManager
/// </summary>
public class PriviledgeManager
{
    static PriviledgeManager instance = null;

    public static PriviledgeManager Instance
    {
        get
        {
            if (instance == null)
                instance = new PriviledgeManager();
            return instance;
        }
    }

    public int Save(PriviledgeDB objDB, SqlTransaction objTrans)
    {
        int PriviledgeID = 0;
        DataAccessLayer objDataLayer = new DataAccessLayer();
        SqlCommand objCmd = new SqlCommand();
        try
        {



            objCmd.CommandText = "usp_Priviledges";
               
            

            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.AddWithValue("@FormID", objDB.FormID);
            objCmd.Parameters.AddWithValue("@RoleID", objDB.RoleID);

            objCmd.Parameters.AddWithValue("@viewing", objDB.viewing);

            objCmd.Parameters.AddWithValue("@PriviledgeID", SqlDbType.Int);
            objCmd.Parameters["@PriviledgeID"].Direction = ParameterDirection.Output;

            objCmd.Transaction = objTrans;
            objCmd.Connection = objTrans.Connection;
            objDataLayer.Command = objCmd;
            objDataLayer.ExecQuery();
            PriviledgeID = Convert.ToInt32(objCmd.Parameters["@PriviledgeID"].Value);
            return PriviledgeID;
        }
        catch (Exception ex)
        {

            throw ex;
        }



    }
}
