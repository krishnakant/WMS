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
/// Summary description for MastersManager
/// </summary>
public class MastersManager
{
    static MastersManager instance = null;
    public MastersManager()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static MastersManager Instance
    {
        get
        {
            if (instance == null)
                instance = new MastersManager();
            return instance;
        }
    }

    public void SaveCulprit(MastersDB objDB, SqlTransaction objTrans)
    {
        DataAccessLayer objDataLayer = new DataAccessLayer();
        SqlCommand objCmd = new SqlCommand();
        try
        {
            objCmd.CommandText = "usp_CulpritInsert";
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.AddWithValue("@Code", Convert.ToInt32(objDB.Code));
            objCmd.Parameters.AddWithValue("@Description", objDB.Description);
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

    public void SaveDefect(MastersDB objDB, SqlTransaction objTrans)
    {
        DataAccessLayer objDataLayer = new DataAccessLayer();
        SqlCommand objCmd = new SqlCommand();
        try
        {
            objCmd.CommandText = "usp_DefectInsert";
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.AddWithValue("@Code", Convert.ToInt32(objDB.Code));
            objCmd.Parameters.AddWithValue("@Description", objDB.Description);
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

    public void SaveCustomerVoice(MastersDB objDB, SqlTransaction objTrans)
    {
        DataAccessLayer objDataLayer = new DataAccessLayer();
        SqlCommand objCmd = new SqlCommand();
        try
        {
            objCmd.CommandText = "usp_CustomerVoiceInsert";
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.AddWithValue("@Code", Convert.ToInt32(objDB.Code));
            objCmd.Parameters.AddWithValue("@Description", objDB.Description);
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

    //public void SaveItem(MastersDB objDB, SqlTransaction objTrans)
    //{
    //    DataAccessLayer objDataLayer = new DataAccessLayer();
    //    SqlCommand objCmd = new SqlCommand();
    //    try
    //    {
    //        objCmd.CommandText = "usp_ItemInsert";
    //        objCmd.CommandType = CommandType.StoredProcedure;
    //        objCmd.Parameters.AddWithValue("@Code", Convert.ToInt32(objDB.Code));
    //        objCmd.Parameters.AddWithValue("@Description", objDB.Description);
    //        objCmd.Transaction = objTrans;
    //        objCmd.Connection = objTrans.Connection;
    //        objDataLayer.Command = objCmd;
    //        objDataLayer.ExecQuery();

    //    }
    //    catch (Exception ex)
    //    {

    //        throw ex;
    //    }
    //}

    public int SaveItemData(SqlTransaction objTrans)
    {
        DataAccessLayer objDataLayer = new DataAccessLayer();
        SqlCommand objCmd = new SqlCommand();
        int Status = 0;
        try
        {
            objCmd.CommandText = "usp_ItemDataInsert";
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.AddWithValue("@Status", SqlDbType.Int);
            objCmd.Parameters["@Status"].Direction = ParameterDirection.Output;
            objCmd.Transaction = objTrans;
            objCmd.Connection = objTrans.Connection;
            objDataLayer.Command = objCmd;
            objDataLayer.ExecQuery();
            Status = Convert.ToInt32(objCmd.Parameters["@Status"].Value);
        }
        catch (Exception ex)
        {

            throw ex;
        }


        return Status;

    }

    public int SaveDefectData(SqlTransaction objTrans)
    {
        DataAccessLayer objDataLayer = new DataAccessLayer();
        SqlCommand objCmd = new SqlCommand();
        int Status = 0;
        try
        {
            objCmd.CommandText = "usp_DefectDataInsert";
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.AddWithValue("@Status", SqlDbType.Int);
            objCmd.Parameters["@Status"].Direction = ParameterDirection.Output;
            objCmd.Transaction = objTrans;
            objCmd.Connection = objTrans.Connection;
            objDataLayer.Command = objCmd;
            objDataLayer.ExecQuery();
            Status = Convert.ToInt32(objCmd.Parameters["@Status"].Value);
        }
        catch (Exception ex)
        {

            throw ex;
        }
        return Status;
   }

    public int SaveCulpritData(SqlTransaction objTrans)
    {
        int Status = 0;
        DataAccessLayer objDataLayer = new DataAccessLayer();
        SqlCommand objCmd = new SqlCommand();
        try
        {
            objCmd.CommandText = "usp_CulpritDataInsert";
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.AddWithValue("@Status", SqlDbType.Int);
            objCmd.Parameters["@Status"].Direction = ParameterDirection.Output;
            objCmd.Transaction = objTrans;
            objCmd.Connection = objTrans.Connection;
            objDataLayer.Command = objCmd;
            objDataLayer.ExecQuery();
            Status = Convert.ToInt32(objCmd.Parameters["@Status"].Value);
        }
        catch (Exception ex)
        {

            throw ex;
        }

        return Status;


    }


    public int SaveCustomerVoiceData(SqlTransaction objTrans)
    {
        DataAccessLayer objDataLayer = new DataAccessLayer();
        SqlCommand objCmd = new SqlCommand();
        int Status = 0;
        try
        {
            objCmd.CommandText = "usp_CustomerVoiceDataInsert";
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.AddWithValue("@Status", SqlDbType.Int);
            objCmd.Parameters["@Status"].Direction = ParameterDirection.Output;
            objCmd.Transaction = objTrans;
            objCmd.Connection = objTrans.Connection;
            objDataLayer.Command = objCmd;
            objDataLayer.ExecQuery();
           Status = Convert.ToInt32(objCmd.Parameters["@Status"].Value);
        }
        catch (Exception ex)
        {

            throw ex;
        }
        return Status;



    }

    public int UpdateDefectData(SqlTransaction objTrans)
    {
        DataAccessLayer objDataLayer = new DataAccessLayer();
        SqlCommand objCmd = new SqlCommand();
        int Status = 0;
        try
        {
            objCmd.CommandText = "usp_DefectDataUpdate";
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.AddWithValue("@Status", SqlDbType.Int);
            objCmd.Parameters["@Status"].Direction = ParameterDirection.Output;
            objCmd.Transaction = objTrans;
            objCmd.Connection = objTrans.Connection;
            objDataLayer.Command = objCmd;
            objDataLayer.ExecQuery();
            Status = Convert.ToInt32(objCmd.Parameters["@Status"].Value);
        }
        catch (Exception ex)
        {

            throw ex;
        }
        return Status;
    }

    public int UpdateItemData(SqlTransaction objTrans)
    {
        DataAccessLayer objDataLayer = new DataAccessLayer();
        SqlCommand objCmd = new SqlCommand();
        int Status = 0;
        try
        {
            objCmd.CommandText = "usp_ItemDataUpdate";
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.AddWithValue("@Status", SqlDbType.Int);
            objCmd.Parameters["@Status"].Direction = ParameterDirection.Output;
            objCmd.Transaction = objTrans;
            objCmd.Connection = objTrans.Connection;
            objDataLayer.Command = objCmd;
            objDataLayer.ExecQuery();
            Status = Convert.ToInt32(objCmd.Parameters["@Status"].Value);
        }
        catch (Exception ex)
        {

            throw ex;
        }
        return Status;
    }

    public int UpdateCulpritData(SqlTransaction objTrans)
    {
        DataAccessLayer objDataLayer = new DataAccessLayer();
        SqlCommand objCmd = new SqlCommand();
        int Status = 0;
        try
        {
            objCmd.CommandText = "usp_CulpritDataUpdate";
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.AddWithValue("@Status", SqlDbType.Int);
            objCmd.Parameters["@Status"].Direction = ParameterDirection.Output;
            objCmd.Transaction = objTrans;
            objCmd.Connection = objTrans.Connection;
            objDataLayer.Command = objCmd;
            objDataLayer.ExecQuery();
            Status = Convert.ToInt32(objCmd.Parameters["@Status"].Value);


        }
        catch (Exception ex)
        {

            throw ex;
        }
        return Status;
    }

    public int UpdateCustomerVoiceData(SqlTransaction objTrans)
    {
        DataAccessLayer objDataLayer = new DataAccessLayer();
        SqlCommand objCmd = new SqlCommand();
        int Status = 0;
        try
        {
            objCmd.CommandText = "usp_CustomerVoiceDataUpdate";
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.AddWithValue("@Status", SqlDbType.Int);
            objCmd.Parameters["@Status"].Direction = ParameterDirection.Output;
            objCmd.Transaction = objTrans;
            objCmd.Connection = objTrans.Connection;
            objDataLayer.Command = objCmd;
            objDataLayer.ExecQuery();
            Status = Convert.ToInt32(objCmd.Parameters["@Status"].Value);
        }
        catch (Exception ex)
        {

            throw ex;
        }
        return Status;
    }

    public int SaveModel(ModelDB objDB,int TempID ,SqlTransaction objTrans)
    {
        DataAccessLayer objDataLayer = new DataAccessLayer();
        SqlCommand objCmd = new SqlCommand();
        int Status = 0;
        try
        {
            objCmd.CommandText = "usp_UpdateModelException";
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.AddWithValue("@ProductCode", Convert.ToInt32(objDB.Code));
            objCmd.Parameters.AddWithValue("@ModelGroupName", objDB.ModelCode);
            objCmd.Parameters.AddWithValue("@GroupID", objDB.GroupID);
            objCmd.Transaction = objTrans;
            objCmd.Connection = objTrans.Connection;
            objDataLayer.Command = objCmd;
            objDataLayer.ExecQuery();

        }
        catch (Exception ex)
        {

            throw ex;
        }
        return Status;
    }

    public int SaveItem(ItemDB objDB, SqlTransaction objTrans)
    {
        DataAccessLayer objDataLayer = new DataAccessLayer();
        SqlCommand objCmd = new SqlCommand();
        int Status = 0;
        try
        {
            objCmd.CommandText = "usp_UpdateItemException";
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.AddWithValue("@ItemCode",objDB.ItemCode);
            objCmd.Parameters.AddWithValue("@ItemGroupName", objDB.ItemGroupName);
            objCmd.Parameters.AddWithValue("@GroupID", objDB.GroupID);
            objCmd.Transaction = objTrans;
            objCmd.Connection = objTrans.Connection;
            objDataLayer.Command = objCmd;
            objDataLayer.ExecQuery();
        }
        catch (Exception ex)
        {

            throw ex;
        }
        return Status;
    }

    public int AddProduct(ModelDB objDB, SqlTransaction objTrans)
    {
        DataAccessLayer objDataLayer = new DataAccessLayer();
        SqlCommand objCmd = new SqlCommand();
        int Status = 0;
        try
        {
            if (objDB.StatusID == 0)
            {
                objCmd.CommandText = "usp_AddProduct";
            }
            else
            {
                objCmd.CommandText = "usp_UpdateModel";
            }
           
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.AddWithValue("@Code", objDB.Code);
            objCmd.Parameters.AddWithValue("@GroupID", objDB.GroupID);
            objCmd.Parameters.AddWithValue("@Description", objDB.Description);
            objCmd.Parameters.AddWithValue("@IsActive", objDB.IsActive);
            objCmd.Parameters.AddWithValue("@InReport", objDB.InReport);
            objCmd.Parameters.AddWithValue("@IsGroup", objDB.IsGroup);
            objCmd.Parameters.AddWithValue("@EffectDate", objDB.EffectDate);

            objCmd.Transaction = objTrans;
            objCmd.Connection = objTrans.Connection;
            objDataLayer.Command = objCmd;
            objDataLayer.ExecQuery();
        }
        catch (Exception ex)
        {

            throw ex;
        }
        return Status;
    }

    public int AddItem(ItemDB objDB, SqlTransaction objTrans)
    {
        DataAccessLayer objDataLayer = new DataAccessLayer();
        SqlCommand objCmd = new SqlCommand();
        int Status = 0;
        try
        {
            if (objDB.StatusID == 0)
            {
                objCmd.CommandText = "usp_AddItem";
            }
            else
            {
                objCmd.CommandText = "usp_UpdateItem";
            }
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.AddWithValue("@Code", objDB.ItemCode);
            objCmd.Parameters.AddWithValue("@GroupID", objDB.GroupID);
            objCmd.Parameters.AddWithValue("@Description", objDB.Description);
            objCmd.Parameters.AddWithValue("@IsActive", objDB.IsActive);
            objCmd.Parameters.AddWithValue("@InReport", objDB.InReport);
            objCmd.Parameters.AddWithValue("@IsGroup", objDB.IsGroup);
            objCmd.Parameters.AddWithValue("@EffectDate", objDB.EffectDate);

            objCmd.Transaction = objTrans;
            objCmd.Connection = objTrans.Connection;
            objDataLayer.Command = objCmd;
            objDataLayer.ExecQuery();
        }
        catch (Exception ex)
        {

            throw ex;
        }
        return Status;
    }

    public int AddDefect(DefectDB objDB, SqlTransaction objTrans)
    {
        DataAccessLayer objDataLayer = new DataAccessLayer();
        SqlCommand objCmd = new SqlCommand();
        int Status = 0;
        try
        {
            if (objDB.StatusID == 0)
            {
                objCmd.CommandText = "usp_AddDefect";
            }
            else
            {
                objCmd.CommandText = "usp_UpdateDefect";
            }
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.AddWithValue("@Code", objDB.DefectCode);
            objCmd.Parameters.AddWithValue("@GroupID", objDB.GroupID);
          
            
            objCmd.Parameters.AddWithValue("@Description", objDB.Description);
            objCmd.Parameters.AddWithValue("@IsActive", objDB.IsActive);
            objCmd.Parameters.AddWithValue("@InReport", objDB.InReport);
            objCmd.Parameters.AddWithValue("@IsGroup", objDB.IsGroup);
            objCmd.Parameters.AddWithValue("@EffectDate", objDB.EffectDate);

            objCmd.Transaction = objTrans;
            objCmd.Connection = objTrans.Connection;
            objDataLayer.Command = objCmd;
            objDataLayer.ExecQuery();
      
        }
        catch (Exception ex)
        {

            throw ex;
        }
        return Status;
    }

    public int AddCulprit(CulpritDB objDB, SqlTransaction objTrans)
    {
        DataAccessLayer objDataLayer = new DataAccessLayer();
        SqlCommand objCmd = new SqlCommand();
        int Status = 0;
        try
        {
           

            if (objDB.StatusID == 0)
            {
                objCmd.CommandText = "usp_AddCulprit";
            }
            else
            {
                objCmd.CommandText = "usp_UpdateCulprit";
            }
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.AddWithValue("@Code", objDB.CulpritCode);
            objCmd.Parameters.AddWithValue("@GroupID", objDB.GroupID);
            objCmd.Parameters.AddWithValue("@Description", objDB.Description);
            objCmd.Parameters.AddWithValue("@IsActive", objDB.IsActive);
            objCmd.Parameters.AddWithValue("@InReport", objDB.InReport);
            objCmd.Parameters.AddWithValue("@IsGroup", objDB.IsGroup);
            objCmd.Parameters.AddWithValue("@EffectDate", objDB.EffectDate);

            objCmd.Transaction = objTrans;
            objCmd.Connection = objTrans.Connection;
            objDataLayer.Command = objCmd;
            objDataLayer.ExecQuery();

            // Status = Convert.ToInt32(objCmd.Parameters["@Status"].Value);

        }
        catch (Exception ex)
        {

            throw ex;
        }
        return Status;
    }

    public int AddCVoice(CVoiceDB objDB, SqlTransaction objTrans)
    {
        DataAccessLayer objDataLayer = new DataAccessLayer();
        SqlCommand objCmd = new SqlCommand();
        int Status = 0;
        try
        {
            if (objDB.StatusID == 0)
            {

                objCmd.CommandText = "usp_AddCVoice";
            }
            else
            {
                objCmd.CommandText = "usp_UpdateCVoice";
            }
           
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.AddWithValue("@Code", objDB.CVoiceCode);
            objCmd.Parameters.AddWithValue("@GroupID", objDB.GroupID);
            objCmd.Parameters.AddWithValue("@Description", objDB.Description);
            objCmd.Parameters.AddWithValue("@IsActive", objDB.IsActive);
            objCmd.Parameters.AddWithValue("@InReport", objDB.InReport);
            objCmd.Parameters.AddWithValue("@IsGroup", objDB.IsGroup);
            objCmd.Parameters.AddWithValue("@EffectDate", objDB.EffectDate);

            objCmd.Transaction = objTrans;
            objCmd.Connection = objTrans.Connection;
            objDataLayer.Command = objCmd;
            objDataLayer.ExecQuery();

            // Status = Convert.ToInt32(objCmd.Parameters["@Status"].Value);

        }
        catch (Exception ex)
        {

            throw ex;
        }
        return Status;
    }
}
