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
/// Summary description for AcrManager
/// </summary>
public class AcrManager
{
    static AcrManager instance = null;
    public AcrManager()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static AcrManager Instance
    {
        get
        {
            if (instance == null)
                instance = new AcrManager();
            return instance;
        }
    }

    public int SaveAcr(AcrDB objDB,SqlTransaction objTrans)
    {
        int ID = 0;
        DataAccessLayer objDataLayer = new DataAccessLayer();
        SqlCommand objCmd = new SqlCommand();
        try
        {
            objCmd.CommandText = "usp_AcrInsert";
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.AddWithValue("@WCDOCNO", objDB.WCDOCNO);
            objCmd.Parameters.AddWithValue("@DLR_REF", objDB.DLR_REF);
            objCmd.Parameters.AddWithValue("@TRACTOR_NO", objDB.TRACTOR_NO);
            objCmd.Parameters.AddWithValue("@ENGINE_NO", objDB.ENGINE_NO);
            objCmd.Parameters.AddWithValue("@INS_DATE", objDB.INS_DATE);
            objCmd.Parameters.AddWithValue("@DEF_DATE", objDB.DEF_DATE);
            objCmd.Parameters.AddWithValue("@REP_DATE", objDB.REP_DATE);
            objCmd.Parameters.AddWithValue("@HMR", objDB.HMR);
            objCmd.Parameters.AddWithValue("@DLR_CO", objDB.DLR_CO);
            objCmd.Parameters.AddWithValue("@DEALER_NAME", objDB.DEALER_NAME);
            objCmd.Parameters.AddWithValue("@REG", objDB.REG);
            objCmd.Parameters.AddWithValue("@CR_DATE", objDB.CR_DATE);
            objCmd.Parameters.AddWithValue("@ITEM_CODE", objDB.ITEM_CODE);
            objCmd.Parameters.AddWithValue("@DESCRIPTION", objDB.DESCRIPTION);
            objCmd.Parameters.AddWithValue("@QUANTITY", objDB.QUANTITY);
            objCmd.Parameters.AddWithValue("@COST", objDB.COST);
            objCmd.Parameters.AddWithValue("@DEF", objDB.DEF);
            objCmd.Parameters.AddWithValue("@NDP", objDB.NDP);
            objCmd.Parameters.AddWithValue("@VALUE", objDB.VALUE);
            objCmd.Parameters.AddWithValue("@CVOICE", objDB.CVOICE);
            objCmd.Parameters.AddWithValue("@OUTLV", objDB.OUTLV);
            objCmd.Parameters.AddWithValue("@DT", objDB.DT);
            objCmd.Parameters.AddWithValue("@CUL_CODE", objDB.CUL_CODE);
            objCmd.Parameters.AddWithValue("@BLANK", objDB.BLANK);
            objCmd.Parameters.AddWithValue("@CR_AMOUNT", objDB.CR_AMOUNT);
            objCmd.Parameters.AddWithValue("@AUTH_AMT", objDB.AUTH_AMOUNT);
            objCmd.Parameters.AddWithValue("@DIFF", objDB.DIFF);
            objCmd.Parameters.AddWithValue("@Production_Month", objDB.Production_Month);
            objCmd.Parameters.AddWithValue("@Production_Month_Year", objDB.Production_Month_Year);
            objCmd.Parameters.AddWithValue("@ModelMappingID", objDB.ModelMappingID);
            objCmd.Parameters.AddWithValue("@HMR_Range", objDB.HMR_Range);
            objCmd.Parameters.AddWithValue("@FromDate", objDB.FromDate);
            objCmd.Parameters.AddWithValue("@ToDate", objDB.ToDate);
            objCmd.Parameters.AddWithValue("@MonthID", objDB.MonthID);
            objCmd.Parameters.AddWithValue("@YearID", objDB.YearID);
            objCmd.Parameters.AddWithValue("@Quarter", objDB.Quarter);
            objCmd.Parameters.AddWithValue("@Engine", objDB.Engine);
            objCmd.Parameters.AddWithValue("@IsEngine", objDB.IsEngine);
            objCmd.Parameters.AddWithValue("@ID", SqlDbType.Int);
            objCmd.Parameters["@ID"].Direction = ParameterDirection.Output;
            objCmd.Transaction = objTrans;
            objCmd.Connection = objTrans.Connection;
            objDataLayer.Command = objCmd;
            objDataLayer.ExecQuery();
            ID = Convert.ToInt32(objCmd.Parameters["@ID"].Value);   
        }
        catch (Exception ex)
        {

            throw ex;
        }



        return ID;
    }


    public int SaveBulkAcr( SqlTransaction objTrans)
    {
        int ID = 0;
        DataAccessLayer objDataLayer = new DataAccessLayer();
        SqlCommand objCmd = new SqlCommand();
        try
        {
            objCmd.CommandText = "usp_AcrBulkDataInsert";
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.AddWithValue("@count", SqlDbType.Int);
            objCmd.Parameters["@count"].Direction = ParameterDirection.Output;
            objCmd.Transaction = objTrans;
            objCmd.Connection = objTrans.Connection;
            objDataLayer.Command = objCmd;
            objDataLayer.ExecQuery();
            ID = Convert.ToInt32(objCmd.Parameters["@count"].Value);
        }
        catch (Exception ex)
        {

            throw ex;
        }



        return ID;
    }

    public int getModelMapping(Int64 TractorNo, SqlTransaction objTrans)
    {
        int ModelMappingID = 0;
        DataAccessLayer objDataLayer = new DataAccessLayer();
        SqlCommand objCmd = new SqlCommand();
        try
        {
            objCmd.CommandText = "usp_getModelMappingID";
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.AddWithValue("@TRACTOR_NO", TractorNo);
            objCmd.Parameters.AddWithValue("@ModelMappingID", SqlDbType.Int);
            objCmd.Parameters["@ModelMappingID"].Direction = ParameterDirection.Output;
            objCmd.Transaction = objTrans;
            objCmd.Connection = objTrans.Connection;
            objDataLayer.Command = objCmd;
            objDataLayer.ExecQuery();
            ModelMappingID = Convert.ToInt32(objCmd.Parameters["@ModelMappingID"].Value);   
        }
        catch (Exception ex)
        {

            throw ex;
        }



        return ModelMappingID;
    }
    public void SaveAcrData(SqlTransaction objTrans)
    {
        DataAccessLayer objDataLayer = new DataAccessLayer();
        SqlCommand objCmd = new SqlCommand();
        try
        {
            objCmd.CommandText = "usp_AcrDataInsert";
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
