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
/// Summary description for ConfiguratorManager
/// </summary>
public class ConfiguratorManager
{
    static ConfiguratorManager instance = null;

    public static ConfiguratorManager Instance
    {
        get
        {
            if (instance == null)
                instance = new ConfiguratorManager();
            return instance;
        }
    }





    public int DeleteGroupName(ConfiguratorDB objDB, SqlTransaction objTrans)
    {
        int StatusID = 0;
        DataAccessLayer objDataLayer = new DataAccessLayer();
        SqlCommand objCmd = new SqlCommand();
        try
        {
            if (objDB.source == "Model")
            {
                objCmd.CommandText = "Usp_DeleteGroupName";
            }
            if (objDB.source == "Culprit")
            {
                objCmd.CommandText = "Usp_DeletCulpritGroup";
            }
            if (objDB.source == "Defect")
            {
                objCmd.CommandText = "Usp_DeleteDefectGroup";
            }
            if (objDB.source == "Cvoice")
            {
                objCmd.CommandText = "Usp_DeleteCvoiceGroup";
            }
            if (objDB.source == "Item")
            {
                objCmd.CommandText = "Usp_DeleteItemGroup";
            }

            
            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.Parameters.AddWithValue("@GroupID", objDB.GroupID);
           


            objCmd.Parameters.AddWithValue("@StatusID", SqlDbType.Int);
            objCmd.Parameters["@StatusID"].Direction = ParameterDirection.Output;

            objCmd.Transaction = objTrans;
            objCmd.Connection = objTrans.Connection;
            objDataLayer.Command = objCmd;
            objDataLayer.ExecQuery();
            StatusID = Convert.ToInt32(objCmd.Parameters["@StatusID"].Value);
            return StatusID;
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    public int UpdateGroupName(ConfiguratorDB objDB, SqlTransaction objTrans)
    {
        int StatusID = 0;
        DataAccessLayer objDataLayer = new DataAccessLayer();
        SqlCommand objCmd = new SqlCommand();
        try
        {
           
            if (objDB.source == "Model")
            {
                objCmd.CommandText = "Usp_UpdateGroupName";
                objCmd.Parameters.AddWithValue("@IsNew", objDB.IsNew);
            }
            if (objDB.source == "Culprit")
            {
                objCmd.CommandText = "Usp_UpdateCulpritGroup";
            }
            if (objDB.source == "Defect")
            {
                objCmd.CommandText = "Usp_UpdateDefectGroup";
            }
            if (objDB.source == "Cvoice")
            {
                objCmd.CommandText = "Usp_UpdateCvoiceGroup";
            }
            if (objDB.source == "Item")
            {
                objCmd.CommandText = "Usp_UpdateItemGroup";
            }
            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.Parameters.AddWithValue("@GroupID", objDB.GroupID);
            objCmd.Parameters.AddWithValue("@GroupName", objDB.GroupName);


            objCmd.Parameters.AddWithValue("@StatusID", SqlDbType.Int);
            objCmd.Parameters["@StatusID"].Direction = ParameterDirection.Output;

            objCmd.Transaction = objTrans;
            objCmd.Connection = objTrans.Connection;
            objDataLayer.Command = objCmd;
            objDataLayer.ExecQuery();
            StatusID = Convert.ToInt32(objCmd.Parameters["@StatusID"].Value);
            return StatusID;
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    public int Save(ConfiguratorDB objDB, SqlTransaction objTrans)
    {
        int modelGroupID = 0;
        DataAccessLayer objDataLayer = new DataAccessLayer();
        SqlCommand objCmd = new SqlCommand();
        try
        {
            objCmd.CommandText = "usp_SaveProductGroupMapping";
            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.Parameters.AddWithValue("@ModelGroupName", objDB.ModelGroupName);
            objCmd.Parameters.AddWithValue("@IsNew", objDB.IsNew);
            objCmd.Parameters.AddWithValue("@WarrantyPeriod", objDB.WarrantyPeriod);
      

            objCmd.Parameters.AddWithValue("@ProductGroupMappingID", SqlDbType.Int);
            objCmd.Parameters["@ProductGroupMappingID"].Direction = ParameterDirection.Output;

            objCmd.Transaction = objTrans;
            objCmd.Connection = objTrans.Connection;
            objDataLayer.Command = objCmd;
            objDataLayer.ExecQuery();
            modelGroupID = Convert.ToInt32(objCmd.Parameters["@ProductGroupMappingID"].Value);
            return modelGroupID;
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }
    public int SaveGroup(ConfiguratorDB objDB, SqlTransaction objTrans)
    {
        int modelGroupID = 0;
        DataAccessLayer objDataLayer = new DataAccessLayer();
        SqlCommand objCmd = new SqlCommand();
        try
        {
            objCmd.CommandText = "usp_SaveItemGroupMapping";
            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.Parameters.AddWithValue("@ItemGRoupName", objDB.GroupName);



            objCmd.Parameters.AddWithValue("@ItemCodeGroupID", SqlDbType.Int);
            objCmd.Parameters["@ItemCodeGroupID"].Direction = ParameterDirection.Output;

            objCmd.Transaction = objTrans;
            objCmd.Connection = objTrans.Connection;
            objDataLayer.Command = objCmd;
            objDataLayer.ExecQuery();
            modelGroupID = Convert.ToInt32(objCmd.Parameters["@ItemCodeGroupID"].Value);
            return modelGroupID;
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }
    public int SaveDefectGroup(ConfiguratorDB objDB, SqlTransaction objTrans)
    {
        int modelGroupID = 0;
        DataAccessLayer objDataLayer = new DataAccessLayer();
        SqlCommand objCmd = new SqlCommand();
        try
        {
            objCmd.CommandText = "usp_SaveDefectGroup";
            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.Parameters.AddWithValue("@GroupName", objDB.GroupName);



            objCmd.Parameters.AddWithValue("@GroupID", SqlDbType.Int);
            objCmd.Parameters["@GroupID"].Direction = ParameterDirection.Output;

            objCmd.Transaction = objTrans;
            objCmd.Connection = objTrans.Connection;
            objDataLayer.Command = objCmd;
            objDataLayer.ExecQuery();
            modelGroupID = Convert.ToInt32(objCmd.Parameters["@GroupID"].Value);
            return modelGroupID;
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    public int SaveCVoiceGroup(ConfiguratorDB objDB, SqlTransaction objTrans)
    {
        int modelGroupID = 0;
        DataAccessLayer objDataLayer = new DataAccessLayer();
        SqlCommand objCmd = new SqlCommand();
        try
        {
            objCmd.CommandText = "usp_SaveCVoiceGroup";
            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.Parameters.AddWithValue("@GroupName", objDB.GroupName);



            objCmd.Parameters.AddWithValue("@GroupID", SqlDbType.Int);
            objCmd.Parameters["@GroupID"].Direction = ParameterDirection.Output;

            objCmd.Transaction = objTrans;
            objCmd.Connection = objTrans.Connection;
            objDataLayer.Command = objCmd;
            objDataLayer.ExecQuery();
            modelGroupID = Convert.ToInt32(objCmd.Parameters["@GroupID"].Value);
            return modelGroupID;
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }
    public int SaveCulpritGroup(ConfiguratorDB objDB, SqlTransaction objTrans)
    {
        int modelGroupID = 0;
        DataAccessLayer objDataLayer = new DataAccessLayer();
        SqlCommand objCmd = new SqlCommand();
        try
        {
            objCmd.CommandText = "usp_SaveCulpritGroup";
            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.Parameters.AddWithValue("@GroupName", objDB.GroupName);



            objCmd.Parameters.AddWithValue("@GroupID", SqlDbType.Int);
            objCmd.Parameters["@GroupID"].Direction = ParameterDirection.Output;

            objCmd.Transaction = objTrans;
            objCmd.Connection = objTrans.Connection;
            objDataLayer.Command = objCmd;
            objDataLayer.ExecQuery();
            modelGroupID = Convert.ToInt32(objCmd.Parameters["@GroupID"].Value);
            return modelGroupID;
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }
    public int SaveItemGroupMapping(ConfiguratorDB objDB, SqlTransaction objTrans)
    {
        int modelGroupID = 0;
        DataAccessLayer objDataLayer = new DataAccessLayer();
        SqlCommand objCmd = new SqlCommand();
        try
        {
            if (objDB.source == "Configurator")
            {
                objCmd.CommandText = "usp_SaveItemGroupMappingDetail";
            }
            else
            {
                objCmd.CommandText = "usp_SaveItemException";
            }
            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.Parameters.AddWithValue("@Code", objDB.Code);
            objCmd.Parameters.AddWithValue("@GroupID", objDB.GroupID);

            objCmd.Parameters.AddWithValue("@MappingDetailID", SqlDbType.Int);
            objCmd.Parameters["@MappingDetailID"].Direction = ParameterDirection.Output;

            objCmd.Transaction = objTrans;
            objCmd.Connection = objTrans.Connection;
            objDataLayer.Command = objCmd;
            objDataLayer.ExecQuery();
            modelGroupID = Convert.ToInt32(objCmd.Parameters["@MappingDetailID"].Value);
            return modelGroupID;
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    public int SaveDefectGroupMapping(ConfiguratorDB objDB, SqlTransaction objTrans)
    {
        int modelGroupID = 0;
        DataAccessLayer objDataLayer = new DataAccessLayer();
        SqlCommand objCmd = new SqlCommand();
        try
        {
          
            if (objDB.source == "Configurator")
            {
                objCmd.CommandText = "usp_SaveDefectGroupMapping";
            }
            else
            {
                objCmd.CommandText = "usp_SaveDefectException";
            }
            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.Parameters.AddWithValue("@Code", objDB.Code);
            objCmd.Parameters.AddWithValue("@GroupID", objDB.GroupID);

            objCmd.Parameters.AddWithValue("@MappingDetailID", SqlDbType.Int);
            objCmd.Parameters["@MappingDetailID"].Direction = ParameterDirection.Output;

            objCmd.Transaction = objTrans;
            objCmd.Connection = objTrans.Connection;
            objDataLayer.Command = objCmd;
            objDataLayer.ExecQuery();
            modelGroupID = Convert.ToInt32(objCmd.Parameters["@MappingDetailID"].Value);
            return modelGroupID;
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }



    public int SaveCulpritGroupMapping(ConfiguratorDB objDB, SqlTransaction objTrans)
    {
        int modelGroupID = 0;
        DataAccessLayer objDataLayer = new DataAccessLayer();
        SqlCommand objCmd = new SqlCommand();
        try
        {
          
            if (objDB.source == "Configurator")
            {
                objCmd.CommandText = "usp_SaveCulPritGroupMapping";
            }
            else
            {
                objCmd.CommandText = "usp_SaveCulpritException";
            }
            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.Parameters.AddWithValue("@Code", objDB.Code);
            objCmd.Parameters.AddWithValue("@GroupID", objDB.GroupID);

            objCmd.Parameters.AddWithValue("@MappingDetailID", SqlDbType.Int);
            objCmd.Parameters["@MappingDetailID"].Direction = ParameterDirection.Output;

            objCmd.Transaction = objTrans;
            objCmd.Connection = objTrans.Connection;
            objDataLayer.Command = objCmd;
            objDataLayer.ExecQuery();
            modelGroupID = Convert.ToInt32(objCmd.Parameters["@MappingDetailID"].Value);
            return modelGroupID;
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    public int SaveCvoiceGroupMapping(ConfiguratorDB objDB, SqlTransaction objTrans)
    {
        int modelGroupID = 0;
        DataAccessLayer objDataLayer = new DataAccessLayer();
        SqlCommand objCmd = new SqlCommand();
        try
        {
          
            if (objDB.source == "Configurator")
            {
                objCmd.CommandText = "usp_SaveCVoiceGroupMapping";
            }
            else
            {
                objCmd.CommandText = "usp_SaveCvoiceException";
            }
            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.Parameters.AddWithValue("@Code", objDB.Code);
            objCmd.Parameters.AddWithValue("@GroupID", objDB.GroupID);

            objCmd.Parameters.AddWithValue("@MappingDetailID", SqlDbType.Int);
            objCmd.Parameters["@MappingDetailID"].Direction = ParameterDirection.Output;

            objCmd.Transaction = objTrans;
            objCmd.Connection = objTrans.Connection;
            objDataLayer.Command = objCmd;
            objDataLayer.ExecQuery();
            modelGroupID = Convert.ToInt32(objCmd.Parameters["@MappingDetailID"].Value);
            return modelGroupID;
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }
    public int SaveProductGroupMapping(ConfiguratorDB objDB, SqlTransaction objTrans)
    {
        int modelGroupID = 0;
        DataAccessLayer objDataLayer = new DataAccessLayer();
        SqlCommand objCmd = new SqlCommand();
        try
        {
            if (objDB.source == "Configurator")
            {
                objCmd.CommandText = "usp_SaveProductCodeMapping";
            }
            else
            {
                objCmd.CommandText = "usp_SaveModelException";
            }
            objCmd.CommandType = CommandType.StoredProcedure;


            objCmd.Parameters.AddWithValue("@ProductCodeID", objDB.ID);
            objCmd.Parameters.AddWithValue("@GroupID", objDB.GroupID);

            objCmd.Parameters.AddWithValue("@ProductCodeMappingID", SqlDbType.Int);
            objCmd.Parameters["@ProductCodeMappingID"].Direction = ParameterDirection.Output;

            objCmd.Transaction = objTrans;
            objCmd.Connection = objTrans.Connection;
            objDataLayer.Command = objCmd;
            objDataLayer.ExecQuery();
            modelGroupID = Convert.ToInt32(objCmd.Parameters["@ProductCodeMappingID"].Value);
            return modelGroupID;
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }
    }


