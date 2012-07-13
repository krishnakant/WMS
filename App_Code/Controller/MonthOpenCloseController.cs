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
/// Summary description for MonthOpenCloseController
/// </summary>
public class MonthOpenCloseController
{
    public MonthOpenCloseController()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void SaveMonthOpenClose(MonthOpenCloseUI objUI)
    {
        bool flagTransation = true;

        MonthOpenCloseDB objDB = new MonthOpenCloseDB();
        objDB.MonthID = objUI.MonthID;
        objDB.YearID = objUI.YearID;
        objDB.Status = objUI.Status;

        DataAccessLayer objDataAccess = new DataAccessLayer();
        SqlTransaction objTrans = null;
        try
        {

            if (objTrans == null)
            {
                flagTransation = false;
                objDataAccess.GetConnection.Open();
                SqlTransaction objTransaction = objDataAccess.GetConnection.BeginTransaction();
                objTrans = objTransaction;
            }
            MonthOpenCloseManager objManager = new MonthOpenCloseManager();
            objManager.SaveMonthOpenClose(objDB, objTrans);
            if (!flagTransation)
                objTrans.Commit();

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
