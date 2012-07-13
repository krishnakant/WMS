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
/// Summary description for ConfiguratorController
/// </summary>
public class ConfiguratorController
{
    public int Save(ConfiguratorUI objcUI, System.Data.SqlClient.SqlTransaction objTrans)
    {
        int ModelGroupID = 0;
        bool flagTransation = true;
        ConfiguratorDB objDB = new ConfiguratorDB();
       objDB.ModelGroupName = objcUI.ModelGroupName;
       objDB.IsNew = objcUI.IsNew;
       objDB.WarrantyPeriod = objcUI.WarrantyPeriod;

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
           ConfiguratorManager objManager = new ConfiguratorManager();
           ModelGroupID= objManager.Save(objDB, objTrans);
           if (!flagTransation)
               objTrans.Commit();
           return ModelGroupID;
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



    public int SaveGroup(ConfiguratorUI objUI, System.Data.SqlClient.SqlTransaction objTrans)
    {
        int ModelGroupID = 0;
        bool flagTransation = true;
        ConfiguratorDB objDB = new ConfiguratorDB();
        objDB.GroupName = objUI.GroupName;

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
            ConfiguratorManager objManager = new ConfiguratorManager();
            ModelGroupID = objManager.SaveGroup(objDB, objTrans);
            if (!flagTransation)
                objTrans.Commit();
            return ModelGroupID;
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

    public int SaveCulpritGroup(ConfiguratorUI objUI, System.Data.SqlClient.SqlTransaction objTrans)
    {
        int GroupID = 0;
        bool flagTransation = true;
        ConfiguratorDB objDB = new ConfiguratorDB();
        objDB.GroupName = objUI.GroupName;
        

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
            ConfiguratorManager objManager = new ConfiguratorManager();
            GroupID = objManager.SaveCulpritGroup(objDB, objTrans);
            if (!flagTransation)
                objTrans.Commit();
            return GroupID;
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
    public int SaveDefectGroup(ConfiguratorUI objUI, System.Data.SqlClient.SqlTransaction objTrans)
    {
        int GroupID = 0;
        bool flagTransation = true;
        ConfiguratorDB objDB = new ConfiguratorDB();
        objDB.GroupName = objUI.GroupName;

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
            ConfiguratorManager objManager = new ConfiguratorManager();
            GroupID = objManager.SaveDefectGroup(objDB, objTrans);
            if (!flagTransation)
                objTrans.Commit();
            return GroupID;
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


    public int SaveCVoiceGroup(ConfiguratorUI objUI, System.Data.SqlClient.SqlTransaction objTrans)
    {
        int GroupID = 0;
        bool flagTransation = true;
        ConfiguratorDB objDB = new ConfiguratorDB();
        objDB.GroupName = objUI.GroupName;

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
            ConfiguratorManager objManager = new ConfiguratorManager();
            GroupID = objManager.SaveCVoiceGroup(objDB, objTrans);
            if (!flagTransation)
                objTrans.Commit();
            return GroupID;
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
    public int SaveItemGroupMapping(ConfiguratorUI objUI, System.Data.SqlClient.SqlTransaction objTrans)
    {
        int ModelGroupID = 0;
        bool flagTransation = true;
        ConfiguratorDB objDB = new ConfiguratorDB();
        objDB.Code = objUI.Code;
        objDB.GroupID = objUI.GroupID;
        objDB.source = objUI.source;
        
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
            ConfiguratorManager objManager = new ConfiguratorManager();
            ModelGroupID = objManager.SaveItemGroupMapping(objDB, objTrans);
            if (!flagTransation)
                objTrans.Commit();
            return ModelGroupID;
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


    public int DeleteGroupName(ConfiguratorUI objUI, System.Data.SqlClient.SqlTransaction objTrans)
    {
        int StatusID = 0;
        bool flagTransation = true;

        ConfiguratorDB objDB = new ConfiguratorDB();

        objDB.GroupID = objUI.GroupID;
        objDB.source = objUI.source;
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
            ConfiguratorManager objManager = new ConfiguratorManager();
            StatusID = objManager.DeleteGroupName(objDB, objTrans);
            if (!flagTransation)
                objTrans.Commit();
            return StatusID;
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

    public int UpdateGroupName(ConfiguratorUI objUI, System.Data.SqlClient.SqlTransaction objTrans)
    {
        int StatusID = 0;
        bool flagTransation = true;

        ConfiguratorDB objDB = new ConfiguratorDB();
        objDB.GroupName = objUI.GroupName;
        objDB.GroupID = objUI.GroupID;
        objDB.IsNew = objUI.IsNew;
        objDB.source = objUI.source;
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
            ConfiguratorManager objManager = new ConfiguratorManager();
            StatusID = objManager.UpdateGroupName(objDB, objTrans);
            if (!flagTransation)
                objTrans.Commit();
            return StatusID;
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
    public int SaveDefectGroupMapping(ConfiguratorUI objUI, System.Data.SqlClient.SqlTransaction objTrans)
    {
        int ModelGroupID = 0;
        bool flagTransation = true;
        ConfiguratorDB objDB = new ConfiguratorDB();
        objDB.Code = objUI.Code;
        objDB.GroupID = objUI.GroupID;
        objDB.source = objUI.source;
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
            ConfiguratorManager objManager = new ConfiguratorManager();
            ModelGroupID = objManager.SaveDefectGroupMapping(objDB, objTrans);
            if (!flagTransation)
                objTrans.Commit();
            return ModelGroupID;
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


    public int SaveCulpritGroupMapping(ConfiguratorUI objUI, System.Data.SqlClient.SqlTransaction objTrans)
    {
        int ModelGroupID = 0;
        bool flagTransation = true;
        ConfiguratorDB objDB = new ConfiguratorDB();
        objDB.Code = objUI.Code;
        objDB.GroupID = objUI.GroupID;
        objDB.source = objUI.source;
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
            ConfiguratorManager objManager = new ConfiguratorManager();
            ModelGroupID = objManager.SaveCulpritGroupMapping(objDB, objTrans);
            if (!flagTransation)
                objTrans.Commit();
            return ModelGroupID;
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
    public int SaveCvoiceGroupMapping(ConfiguratorUI objUI, System.Data.SqlClient.SqlTransaction objTrans)
    {
        int ModelGroupID = 0;
        bool flagTransation = true;
        ConfiguratorDB objDB = new ConfiguratorDB();
        objDB.Code = objUI.Code;
        objDB.GroupID = objUI.GroupID;
        objDB.source = objUI.source;
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
            ConfiguratorManager objManager = new ConfiguratorManager();
            ModelGroupID = objManager.SaveCvoiceGroupMapping(objDB, objTrans);
            if (!flagTransation)
                objTrans.Commit();
            return ModelGroupID;
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
    public int SaveProductGroupMapping(ConfiguratorUI objUI, System.Data.SqlClient.SqlTransaction objTrans)
    {
        int ModelGroupID = 0;
        bool flagTransation = true;
        ConfiguratorDB objDB = new ConfiguratorDB();
        objDB.ID = objUI.ID;
        objDB.source = objUI.source;
        objDB.GroupID = objUI.GroupID;
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
            ConfiguratorManager objManager = new ConfiguratorManager();
            ModelGroupID = objManager.SaveProductGroupMapping(objDB, objTrans);
            if (!flagTransation)
                objTrans.Commit();
            return ModelGroupID;
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
