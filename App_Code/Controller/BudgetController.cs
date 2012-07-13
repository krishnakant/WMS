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
/// Summary description for BudgetController
/// </summary>
public class BudgetController
{
    public BudgetController()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int SaveBudget(BudgetUI objUI, System.Data.SqlClient.SqlTransaction objTrans)
    {
        int ID = 0;
        bool flagTransation = true;

        BudgetDB objDB = new BudgetDB();
        objDB.FinancialYear = objUI.FinancialYear;
        objDB.QuarterID = objUI.QuarterID;
        objDB.Budget = objUI.Budget;
        objDB.ModelGroupID = objUI.ModelGroupID;
        objDB.ModelCategoryID = objUI.ModelCategoryID;
        objDB.ModelClutchID = objUI.ModelClutchID;
        objDB.ModelSpecialID = objUI.ModelSpecialID;

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
            BudgetManager objManager = new BudgetManager();
            ID = objManager.SaveBudget(objDB, objTrans);
            if (!flagTransation)
                objTrans.Commit();
            return ID;
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
