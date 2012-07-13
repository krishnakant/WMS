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
/// Summary description for RoleController
/// </summary>
public class RoleController
{
	public RoleController()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int Save(RoleUI objUI, System.Data.SqlClient.SqlTransaction objTrans)
    {
        int RoleID = 0;
        bool flagTransation = true;

        RoleDB objDB = new RoleDB();
        objDB.Role = objUI.Role;
        objDB.IsActive = objUI.IsActive;
        objDB.Id = objUI.Id;
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
            RoleManager objManager = new RoleManager();
            RoleID=objManager.SaveRole(objDB, objTrans);
            if (!flagTransation)
                objTrans.Commit();
            return RoleID;
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
