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
/// Summary description for DealerController
/// </summary>
public class DealerController
{
	public DealerController()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int SaveDealer(DealerUI objUI, System.Data.SqlClient.SqlTransaction objTrans)
    {
        int DealerID = 0;
        bool flagTransation = true;

        DealerDB objDB = new DealerDB();
        objDB.Code = objUI.Code;
        objDB.Dealer = objUI.Dealer;
        objDB.RegionID = objUI.RegionID;
        objDB.IsActive = objUI.IsActive;
        objDB.Id = objUI.Id;
        objDB.CheckID = objUI.CheckID;
        objDB.City = objUI.City;
        objDB.InstallerName = objUI.InstallerName;
        objDB.IsOperatingDealer = objUI.IsOperatingDealer;
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
            DealerManager objManager = new DealerManager();
            DealerID = objManager.SaveDealer(objDB, objTrans);
            if (!flagTransation)
                objTrans.Commit();
            return DealerID;
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
