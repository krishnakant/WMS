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
/// Summary description for SalesController
/// </summary>
public class SalesController
{
    public SalesController()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void SaveSales(SalesUI objUI)
    {
        bool flagTransation = true;

        SalesDB objDB = new SalesDB();
        objDB.Sno = objUI.Sno;
        objDB.InvoiceNo = objUI.InvoiceNo;
        objDB.Date = objUI.Date;
        objDB.Dealer_Code = objUI.Dealer_Code;
        objDB.Dealer_Name = objUI.Dealer_Name;
        objDB.Blank = objUI.Blank;
        objDB.Model_Code = objUI.Model_Code;
        objDB.Quantity = objUI.Quantity;
        objDB.SalesAmount = objUI.SalesAmount;
        objDB.Discount = objUI.Discount;
        objDB.SPLDIS = objUI.SPLDIS;
        objDB.ExciseDuty = objUI.ExciseDuty;
        objDB.Edu_Cess = objUI.Edu_Cess;
        objDB.HR_ECess = objUI.HR_ECess;
        objDB.LSPD = objUI.LSPD;
        objDB.MSPSD = objUI.MSPSD;
        objDB.DHC = objUI.DHC;
        objDB.Taxable = objUI.Taxable;
        objDB.CST = objUI.CST;
        objDB.LST = objUI.LST;
        objDB.Surch = objUI.Surch;
        objDB.EntityTot = objUI.EntityTot;
        objDB.Dely_Chgs = objUI.Dely_Chgs;
        objDB.Freight = objUI.Freight;
        objDB.Net_Amount = objUI.Net_Amount;
        objDB.Cost = objUI.Cost;
        objDB.SOff = objUI.SOff;
        objDB.FromDate = objUI.FromDate;
        objDB.ToDate = objUI.ToDate;
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
            SalesManager objManager = new SalesManager();
            objManager.SaveSales(objDB, objTrans);
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

    public void SaveSalesData()
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
            SalesManager objManager = new SalesManager();
            objManager.SaveSalesData(objTrans);
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
