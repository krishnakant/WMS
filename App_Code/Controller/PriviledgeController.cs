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
/// Summary description for PriviledgeController
/// </summary>
public class PriviledgeController
{
	public PriviledgeController()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int Save( PriviledgeUI objUI, System.Data.SqlClient.SqlTransaction objTrans)
    {
        int PriviledgeID = 0;
        bool flagTransation = true;

        PriviledgeDB objDB = new PriviledgeDB();
        objDB.RoleID = objUI.RoleID;
        objDB.FormID = objUI.FormID;
        objDB.viewing = objUI.viewing;
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
            PriviledgeManager objManager = new PriviledgeManager();
            PriviledgeID = objManager.Save(objDB, objTrans);
            if (!flagTransation)
                objTrans.Commit();
            return PriviledgeID;
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
