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
/// Summary description for ReworkCostController
/// </summary>
public class ReworkCostController
{
    public ReworkCostController()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int SaveReworkCost(ReworkCostUI objUI, System.Data.SqlClient.SqlTransaction objTrans)
    {
        int ID = 0;
        bool flagTransation = true;

        ReworkCostDB objDB = new ReworkCostDB();
        objDB.GroupID = objUI.GroupID;
        objDB.MonthID = objUI.MonthID;
        objDB.YearID = objUI.YearID;
        objDB.ReworkCost_I_Year = objUI.ReworkCost_I_Year;
        objDB.ReworkCost_II_Year = objUI.ReworkCost_II_Year;
        objDB.ModelCategoryID = objUI.ModelCategoryID;
        objDB.HMR_Range = objUI.HMR_Range;
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
            ReworkCostManager objManager = new ReworkCostManager();
            ID=objManager.SaveReworkCost(objDB, objTrans);
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
