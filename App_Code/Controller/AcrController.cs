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
/// Summary description for AcrController
/// </summary>
public class AcrController
{
	public AcrController()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int SaveAcr(AcrUI objUI)
    {
        int ID = 0;
        bool flagTransation = true;

        AcrDB objDB = new AcrDB();
        objDB.WCDOCNO = objUI.WCDOCNO;
        objDB.DLR_REF = objUI.DLR_REF;
        objDB.TRACTOR_NO = objUI.TRACTOR_NO;
        objDB.ENGINE_NO = objUI.ENGINE_NO;
        objDB.INS_DATE = objUI.INS_DATE;
        objDB.REP_DATE = objUI.REP_DATE;
        objDB.DEF_DATE = objUI.DEF_DATE;
        objDB.HMR = objUI.HMR;
        objDB.DLR_CO = objUI.DLR_CO;
        objDB.DEALER_NAME = objUI.DEALER_NAME;
        objDB.REG = objUI.REG;
        objDB.CR_DATE = objUI.CR_DATE;
        objDB.ITEM_CODE = objUI.ITEM_CODE;
        objDB.DESCRIPTION = objUI.DESCRIPTION;
        objDB.COST = objUI.COST;
        objDB.QUANTITY = objUI.QUANTITY;
        objDB.DEF = objUI.DEF;
        objDB.NDP = objUI.NDP;
        objDB.VALUE = objUI.VALUE;
        objDB.CVOICE = objUI.CVOICE;
        objDB.OUTLV = objUI.OUTLV;
        objDB.DT = objUI.DT;
        objDB.CUL_CODE = objUI.CUL_CODE;
        objDB.BLANK = objUI.BLANK;
        objDB.CR_AMOUNT = objUI.CR_AMOUNT;
        objDB.AUTH_AMOUNT = objUI.AUTH_AMOUNT;
        objDB.DIFF = objUI.DIFF;
        objDB.Production_Month = objUI.Production_Month;
        objDB.Production_Month_Year = objUI.Production_Month_Year;
        objDB.ModelMappingID = objUI.ModelMappingID;
        objDB.HMR_Range = objUI.HMR_Range;
        objDB.FromDate = objUI.FromDate;
        objDB.ToDate = objUI.ToDate;
        objDB.MonthID = objUI.MonthID;
        objDB.YearID = objUI.YearID;
        objDB.Quarter = objUI.Quarter;
        objDB.Engine = objUI.Engine;
        objDB.IsEngine = objUI.IsEngine;

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
            AcrManager objManager = new AcrManager();
            ID = objManager.SaveAcr(objDB, objTrans);
            if (!flagTransation)
                objTrans.Commit();
           
        }
        catch (Exception ex)
        {
            if (!flagTransation)
                objTrans.Rollback();
            throw ex;
        }
        return ID;
    }

    public int SaveBulkAcr()
    {
        int ID = 0;
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
            AcrManager objManager = new AcrManager();
            ID = objManager.SaveBulkAcr(objTrans);
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
        return ID;
    }

    public int getModelMapping(Int64 TractorNo)
    {
        int ModelMappingID = 0;
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
            AcrManager objManager = new AcrManager();
            ModelMappingID = objManager.getModelMapping(TractorNo, objTrans);
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
        return ModelMappingID;
    }

    public void SaveAcrData()
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
            AcrManager objManager = new AcrManager();
            objManager.SaveAcrData(objTrans);
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
