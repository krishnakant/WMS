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
/// Summary description for UserController
/// </summary>
public class UserController
{
	public UserController()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int SaveUser(UserUI objUI, System.Data.SqlClient.SqlTransaction objTrans)
    {
        int UserID = 0;
        bool flagTransation = true;

        UserDB objDB = new UserDB();
        objDB.FullName = objUI.FullName;
        objDB.EmployeeCode = objUI.EmployeeCode;
        objDB.EmailID = objUI.EmailID;
        objDB.RoleID = objUI.RoleID;
        objDB.LoginID = objUI.LoginID;
        objDB.Password = objUI.Password;
        objDB.PhoneNo = objUI.PhoneNo;
        objDB.MobileNo = objUI.MobileNo;
        objDB.PermanentAddress = objUI.PermanentAddress;
        objDB.CurrentAddress= objUI.CurrentAddress;
        objDB.DateOfJoing = objUI.DateOfJoing;
        objDB.CheckID = objUI.CheckID;
        objDB.Id = objUI.Id;
        objDB.UserTypeID = objUI.UserTypeID;
        objDB.LevelID = objUI.LevelID;
        objDB.IsActive = objUI.IsActive;
       
       
        DataAccessLayer objDataAccess = new DataAccessLayer();

        try
        {

            if (objTrans == null)
            {
                flagTransation = false;
                objDataAccess.GetConnection.Open();
                SqlTransaction objTransaction = objDataAccess.GetConnection.BeginTransaction();
                objTrans = objTransaction;
            }
            UserManager objManager = new UserManager();
            UserID = objManager.SaveUser(objDB, objTrans);
            if (!flagTransation)
                objTrans.Commit();
            return UserID;
        }
        catch (Exception ex)
        {
            if (!flagTransation)
                objTrans.Rollback();
            throw ex;
        }
        finally
        {
            objDataAccess.GetConnection.Close();
        }
    }


    public int SaveUserDetail(UserUI objUI, System.Data.SqlClient.SqlTransaction objTrans)
    {
        int UserID = 0;
        bool flagTransation = true;

        UserDB objDB = new UserDB();
        objDB.UserID = objUI.UserID;
        //objDB.LevelID = objUI.LevelID;
        objDB.dRegionID = objUI.dRegionID;
        objDB.ReportsToID = objUI.ReportsToID;
        objDB.CheckID = objUI.CheckID;

        DataAccessLayer objDataAccess = new DataAccessLayer();

        try
        {

            if (objTrans == null)
            {
                flagTransation = false;
                objDataAccess.GetConnection.Open();
                SqlTransaction objTransaction = objDataAccess.GetConnection.BeginTransaction();
                objTrans = objTransaction;
            }
            UserManager objManager = new UserManager();
            UserID = objManager.SaveUserDetail(objDB, objTrans);
            if (!flagTransation)
                objTrans.Commit();
            return UserID;
        }
        catch (Exception ex)
        {
            if (!flagTransation)
                objTrans.Rollback();
            throw ex;
        }
        finally
        {
            objDataAccess.GetConnection.Close();
        }
    }

}

