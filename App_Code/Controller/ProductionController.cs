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
/// Summary description for ProductionController
/// </summary>
public class ProductionController
{
    public ProductionController()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void SaveProduction(ProductionUI objUI)
    {
        bool flagTransation = true;

        ProductionDB objDB = new ProductionDB();
        objDB.S = objUI.S;
        objDB.Material = objUI.Material;
        objDB.SerialNo = objUI.SerialNo;
        objDB.Plnt = objUI.Plnt;
        objDB.SLoc = objUI.SLoc;
        objDB.Description = objUI.Description;
        objDB.Production_Month = objUI.Production_Month;
        objDB.Production_Month_Year = objUI.Production_Month_Year;
        //objDB.Model_Code = objUI.Model_Code;
        objDB.FromDate = objUI.FromDate;
        objDB.ToDate = objUI.ToDate;
        objDB.MonthID = objUI.MonthID;
        objDB.YearID = objUI.YearID;
        objDB.Quarter = objUI.Quarter;
        objDB.ModelMappingID = objUI.ModelMappingID;
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
            ProductionManager objManager = new ProductionManager();
            objManager.SaveProduction(objDB, objTrans);
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

    public void SaveProductionData()
    {

        bool flagTransation = true;



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
            ProductionManager objManager = new ProductionManager();
            objManager.SaveProductionData(objTrans);
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
