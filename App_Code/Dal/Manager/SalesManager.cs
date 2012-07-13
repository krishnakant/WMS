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
/// Summary description for SalesManager
/// </summary>
public class SalesManager
{
    static SalesManager instance = null;
    public SalesManager()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static SalesManager Instance
    {
        get
        {
            if (instance == null)
                instance = new SalesManager();
            return instance;
        }
    }
    public void SaveSales(SalesDB objDB, SqlTransaction objTrans)
    {
        DataAccessLayer objDataLayer = new DataAccessLayer();
        SqlCommand objCmd = new SqlCommand();
        try
        {
            objCmd.CommandText = "usp_SalesInsert";
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.AddWithValue("@Sno", objDB.Sno);
            objCmd.Parameters.AddWithValue("@InvoiceNo", objDB.InvoiceNo);
            objCmd.Parameters.AddWithValue("@Date", objDB.Date);
            objCmd.Parameters.AddWithValue("@Dealer_Code", objDB.Dealer_Code);
            objCmd.Parameters.AddWithValue("@Dealer_Name", objDB.Dealer_Name);
            objCmd.Parameters.AddWithValue("@Blank", objDB.Blank);
            objCmd.Parameters.AddWithValue("@Model_Code", objDB.Model_Code);
            objCmd.Parameters.AddWithValue("@Quantity", objDB.Quantity);
            objCmd.Parameters.AddWithValue("@SalesAmount", objDB.SalesAmount);
            objCmd.Parameters.AddWithValue("@Discount", objDB.Discount);
            objCmd.Parameters.AddWithValue("@SPLDIS", objDB.SPLDIS);
            objCmd.Parameters.AddWithValue("@ExciseDuty", objDB.ExciseDuty);
            objCmd.Parameters.AddWithValue("@Edu_Cess", objDB.Edu_Cess);
            objCmd.Parameters.AddWithValue("@HR_ECess", objDB.HR_ECess);
            objCmd.Parameters.AddWithValue("@LSPD", objDB.LSPD);
            objCmd.Parameters.AddWithValue("@MSPSD", objDB.MSPSD);
            objCmd.Parameters.AddWithValue("@DHC", objDB.DHC);
            objCmd.Parameters.AddWithValue("@Taxable", objDB.Taxable);
            objCmd.Parameters.AddWithValue("@CST", objDB.CST);
            objCmd.Parameters.AddWithValue("@LST", objDB.LST);
            objCmd.Parameters.AddWithValue("@Surch", objDB.Surch);
            objCmd.Parameters.AddWithValue("@EntityTot", objDB.EntityTot);
            objCmd.Parameters.AddWithValue("@Dely_Chgs", objDB.Dely_Chgs);
            objCmd.Parameters.AddWithValue("@Freight", objDB.Freight);
            objCmd.Parameters.AddWithValue("@Net_Amount", objDB.Net_Amount);
            objCmd.Parameters.AddWithValue("@Cost", objDB.Cost);
            objCmd.Parameters.AddWithValue("@SOff", objDB.SOff);
            objCmd.Parameters.AddWithValue("@FromDate", objDB.FromDate);
            objCmd.Parameters.AddWithValue("@ToDate", objDB.ToDate);
            objCmd.Parameters.AddWithValue("@ModelMappingID", objDB.ModelMappingID);
            objCmd.Transaction = objTrans;
            objCmd.Connection = objTrans.Connection;
            objDataLayer.Command = objCmd;
            objDataLayer.ExecQuery();

        }
        catch (Exception ex)
        {
            string strBlank = objDB.Blank;
            throw ex;
        }




    }

    public void SaveSalesData(SqlTransaction objTrans)
    {
        DataAccessLayer objDataLayer = new DataAccessLayer();
        SqlCommand objCmd = new SqlCommand();
        try
        {
            objCmd.CommandText = "usp_SalesDataInsert";
            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.Transaction = objTrans;
            objCmd.Connection = objTrans.Connection;
            objDataLayer.Command = objCmd;
            objDataLayer.ExecQuery();

        }
        catch (Exception ex)
        {

            throw ex;
        }




    }
  
}
