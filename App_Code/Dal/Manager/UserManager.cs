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
/// Summary description for UserManager
/// </summary>
public class UserManager
{

    static UserManager instance = null;

    public static UserManager Instance
    {
        get
        {
            if (instance == null)
                instance = new UserManager();
            return instance;
        }
    }

    public int SaveUser(UserDB objDB, SqlTransaction objTrans)
    {
        int UserID = 0;
        DataAccessLayer objDataLayer = new DataAccessLayer();
        SqlCommand objCmd = new SqlCommand();
        try
        {
            if (objDB.CheckID == 0)
            {
                objCmd.CommandText = "usp_SaveUserInfo";
                objCmd.Parameters.AddWithValue("@Password", objDB.Password);
            }
            else
            {
                objCmd.CommandText = "usp_UpdateUserInfo";
                objCmd.Parameters.AddWithValue("@Id", objDB.Id);
            }

            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.Parameters.AddWithValue("@FullName", objDB.FullName);
            objCmd.Parameters.AddWithValue("@EmployeeCode", objDB.EmployeeCode);
            objCmd.Parameters.AddWithValue("@EmailID", objDB.EmailID);
            objCmd.Parameters.AddWithValue("@PermanentAddress", objDB.PermanentAddress);
            objCmd.Parameters.AddWithValue("@CurrentAddress", objDB.CurrentAddress);
            objCmd.Parameters.AddWithValue("@DateOfJoing", objDB.DateOfJoing);
            objCmd.Parameters.AddWithValue("@LoginID", objDB.LoginID);
            objCmd.Parameters.AddWithValue("@PhoneNo", objDB.PhoneNo);
            objCmd.Parameters.AddWithValue("@MobileNo", objDB.MobileNo);
            objCmd.Parameters.AddWithValue("@RoleID", objDB.RoleID);
            objCmd.Parameters.AddWithValue("@UserTypeID", objDB.UserTypeID);
            objCmd.Parameters.AddWithValue("@LevelID", objDB.LevelID);
          
            
           objCmd.Parameters.AddWithValue("@IsActive", objDB.IsActive);

            objCmd.Parameters.AddWithValue("@UserID", SqlDbType.Int);
            objCmd.Parameters["@UserID"].Direction = ParameterDirection.Output;

            objCmd.Transaction = objTrans;
            objCmd.Connection = objTrans.Connection;
            objDataLayer.Command = objCmd;
            objDataLayer.ExecQuery();
            UserID = Convert.ToInt32(objCmd.Parameters["@UserID"].Value);
            return UserID;
        }
        catch (Exception ex)
        {

            throw ex;
        }



    }

    public int SaveUserDetail(UserDB objDB, SqlTransaction objTrans)
    {
        int UserDetailID = 0;
        DataAccessLayer objDataLayer = new DataAccessLayer();
        SqlCommand objCmd = new SqlCommand();
        try
        {

           
            objCmd.CommandText = "usp_SaveUserDetail";
            
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.AddWithValue("@UserID", objDB.UserID);
            objCmd.Parameters.AddWithValue("@RegionID", objDB.dRegionID);
            objCmd.Parameters.AddWithValue("@ReportsToID", objDB.ReportsToID);
            objCmd.Parameters.AddWithValue("@UserDetailID", SqlDbType.Int);
            objCmd.Parameters["@UserDetailID"].Direction = ParameterDirection.Output;
            objCmd.Transaction = objTrans;
            objCmd.Connection = objTrans.Connection;
            objDataLayer.Command = objCmd;
            objDataLayer.ExecQuery();
            UserDetailID = Convert.ToInt32(objCmd.Parameters["@UserDetailID"].Value);
            return UserDetailID;
        }
        catch (Exception ex)
        {

            throw ex;
        }



    }
}
