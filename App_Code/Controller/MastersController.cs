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
/// Summary description for MastersController
/// </summary>
public class MastersController
{
    public MastersController()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void SaveCulprit(MastersUI objUI)
    {
        bool flagTransation = true;

        MastersDB objDB = new MastersDB();
        objDB.Code = objUI.Code;
        objDB.Description = objUI.Description;

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
            MastersManager objManager = new MastersManager();
            objManager.SaveCulprit(objDB, objTrans);
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

    public void SaveDefect(MastersUI objUI)
    {
        bool flagTransation = true;

        MastersDB objDB = new MastersDB();
        objDB.Code = objUI.Code;
        objDB.Description = objUI.Description;

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
            MastersManager objManager = new MastersManager();
            objManager.SaveDefect(objDB, objTrans);
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

    public void SaveCustomerVoice(MastersUI objUI)
    {
        bool flagTransation = true;

        MastersDB objDB = new MastersDB();
        objDB.Code = objUI.Code;
        objDB.Description = objUI.Description;

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
            MastersManager objManager = new MastersManager();
            objManager.SaveCustomerVoice(objDB, objTrans);
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

    //public void SaveItem(MastersUI objUI)
    //{
    //    bool flagTransation = true;

    //    MastersDB objDB = new MastersDB();
    //    objDB.Code = objUI.Code;
    //    objDB.Description = objUI.Description;

    //    DataAccessLayer objDataAccess = new DataAccessLayer();
    //    SqlTransaction objTrans = null;
    //    try
    //    {

    //        if (objTrans == null)
    //        {
    //            flagTransation = false;
    //            objDataAccess.GetConnection.Open();
    //            SqlTransaction objTransaction = objDataAccess.GetConnection.BeginTransaction();
    //            objTrans = objTransaction;
    //        }
    //        MastersManager objManager = new MastersManager();
    //        objManager.SaveItem(objDB, objTrans);
    //        if (!flagTransation)
    //            objTrans.Commit();

    //    }
    //    catch (Exception ex)
    //    {
    //        if (!flagTransation)
    //            objTrans.Rollback();
    //        throw ex;
    //    }
    //}

    public int SaveItemData()
    {
        int Status = 0;
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
            MastersManager objManager = new MastersManager();
            Status = objManager.SaveItemData(objTrans);
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
        return Status;
    }

    public int SaveCulpritData()
    {

        bool flagTransation = true;
        int Status = 0;


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
            MastersManager objManager = new MastersManager();
            Status = objManager.SaveCulpritData(objTrans);
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
        return Status;
    }

    public int SaveDefectData()
    {

        bool flagTransation = true;



        DataAccessLayer objDataAccess = new DataAccessLayer();
        SqlTransaction objTrans = null;
        int Status = 0;
        try
        {

            if (objTrans == null)
            {
                flagTransation = false;
                objDataAccess.GetConnection.Open();
                SqlTransaction objTransaction = objDataAccess.GetConnection.BeginTransaction();
                objTrans = objTransaction;
            }
            MastersManager objManager = new MastersManager();
            Status = objManager.SaveDefectData(objTrans);
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
        return Status;
    }

   public int SaveCustomerVoiceData()
    {
        int Status = 0;
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
            MastersManager objManager = new MastersManager();
            Status = objManager.SaveCustomerVoiceData(objTrans);
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
        return Status;
    }

    public int UpdateDefectData()
    {
        bool flagTransation = true;



        DataAccessLayer objDataAccess = new DataAccessLayer();
        SqlTransaction objTrans = null;
        int Status = 0;
        try
        {

            if (objTrans == null)
            {
                flagTransation = false;
                objDataAccess.GetConnection.Open();
                SqlTransaction objTransaction = objDataAccess.GetConnection.BeginTransaction();
                objTrans = objTransaction;
            }
            MastersManager objManager = new MastersManager();
            Status = objManager.UpdateDefectData(objTrans);
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
        return Status;
    }

    public int UpdateItemData()
    {
        bool flagTransation = true;



        DataAccessLayer objDataAccess = new DataAccessLayer();
        SqlTransaction objTrans = null;
        int Status = 0;
        try
        {

            if (objTrans == null)
            {
                flagTransation = false;
                objDataAccess.GetConnection.Open();
                SqlTransaction objTransaction = objDataAccess.GetConnection.BeginTransaction();
                objTrans = objTransaction;
            }
            MastersManager objManager = new MastersManager();
            Status = objManager.UpdateItemData(objTrans);
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
        return Status;
    }

    public int UpdateCulpritData()
    {
        bool flagTransation = true;



        DataAccessLayer objDataAccess = new DataAccessLayer();
        SqlTransaction objTrans = null;
        int Status = 0;
        try
        {

            if (objTrans == null)
            {
                flagTransation = false;
                objDataAccess.GetConnection.Open();
                SqlTransaction objTransaction = objDataAccess.GetConnection.BeginTransaction();
                objTrans = objTransaction;
            }
            MastersManager objManager = new MastersManager();
            Status = objManager.UpdateCulpritData(objTrans);
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
        return Status;
    }

    public int UpdateCustomerVoiceData()
    {
        bool flagTransation = true;



        DataAccessLayer objDataAccess = new DataAccessLayer();
        SqlTransaction objTrans = null;
        int Status = 0;
        try
        {

            if (objTrans == null)
            {
                flagTransation = false;
                objDataAccess.GetConnection.Open();
                SqlTransaction objTransaction = objDataAccess.GetConnection.BeginTransaction();
                objTrans = objTransaction;
            }
            MastersManager objManager = new MastersManager();
            Status = objManager.UpdateCustomerVoiceData(objTrans);
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
        return Status;
    }

    public int SaveModel(ModelUI objUI,int TempID)
    {
        bool flagTransation = true;
        int Status = 0;
        ModelDB objDB = new ModelDB();
        objDB.Code = objUI.Code;
        objDB.ModelCode = objUI.ModelCode;
        objDB.GroupID = objUI.GroupID;
  

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
            MastersManager objManager = new MastersManager();
            Status = objManager.SaveModel(objDB,TempID ,objTrans);
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
        return Status;
    }

    public int SaveItem(ItemUI objUI)
    {
        bool flagTransation = true;
        int Status = 0;
        ItemDB objDB = new ItemDB();
        objDB.ItemCode = objUI.ItemCode;
        objDB.ItemGroupName = objUI.ItemGroupName;
        objDB.GroupID = objUI.GroupID;
        objDB.StatusID = objUI.StatusID;
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
            MastersManager objManager = new MastersManager();
            Status = objManager.SaveItem(objDB, objTrans);
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
        return Status;
    }

    public void AddProduct(ModelUI objUI, SqlTransaction objTrans)
    {
        bool flagTransation = true;
        int Status = 0;
        ModelDB objDB = new ModelDB();
        objDB.GroupID = objUI.GroupID;
        objDB.Code = objUI.Code;
        objDB.Description = objUI.Description;
        objDB.IsActive = objUI.IsActive;
        objDB.InReport = objUI.InReport;
        objDB.IsGroup = objUI.IsGroup;
        objDB.EffectDate = objUI.EffectDate;
        objDB.StatusID = objUI.StatusID;

        DataAccessLayer objDataAccess = new DataAccessLayer();
        //SqlTransaction objTrans = null;
        try
        {

            if (objTrans == null)
            {
                flagTransation = false;
                objDataAccess.GetConnection.Open();
                SqlTransaction objTransaction = objDataAccess.GetConnection.BeginTransaction();
                objTrans = objTransaction;
            }
            MastersManager objManager = new MastersManager();
            Status = objManager.AddProduct(objDB, objTrans);
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
    
     public void AddItem(ItemUI objUI, SqlTransaction objTrans)
    {
        bool flagTransation = true;
        int Status = 0;
        ItemDB objDB = new ItemDB();
        objDB.GroupID = objUI.GroupID;
        objDB.ItemCode = objUI.ItemCode;
        objDB.Description = objUI.Description;
        objDB.IsActive = objUI.IsActive;
        objDB.InReport = objUI.InReport;
        objDB.IsGroup = objUI.IsGroup;
        objDB.EffectDate = objUI.EffectDate;
        objDB.StatusID = objUI.StatusID;

        DataAccessLayer objDataAccess = new DataAccessLayer();
        //SqlTransaction objTrans = null;
        try
        {

            if (objTrans == null)
            {
                flagTransation = false;
                objDataAccess.GetConnection.Open();
                SqlTransaction objTransaction = objDataAccess.GetConnection.BeginTransaction();
                objTrans = objTransaction;
            }
            MastersManager objManager = new MastersManager();
            Status = objManager.AddItem(objDB, objTrans);
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

    public void AddDefect(DefectUI objUI, SqlTransaction objTrans)
    {
        bool flagTransation = true;
        int Status = 0;
        DefectDB objDB = new DefectDB();
        objDB.GroupID = objUI.GroupID;
        objDB.DefectCode = objUI.DefectCode;
        objDB.Description = objUI.Description;
        objDB.IsActive = objUI.IsActive;
        objDB.InReport = objUI.InReport;
        objDB.IsGroup = objUI.IsGroup;
        objDB.EffectDate = objUI.EffectDate;
        objDB.StatusID = objUI.StatusID;
        
        DataAccessLayer objDataAccess = new DataAccessLayer();
        //SqlTransaction objTrans = null;
        try
        {

            if (objTrans == null)
            {
                flagTransation = false;
                objDataAccess.GetConnection.Open();
                SqlTransaction objTransaction = objDataAccess.GetConnection.BeginTransaction();
                objTrans = objTransaction;
            }
            MastersManager objManager = new MastersManager();
            Status = objManager.AddDefect(objDB, objTrans);
            if (!flagTransation)
                objTrans.Commit();

        }
        catch (Exception ex)
        {
            if (!flagTransation)
                objTrans.Rollback();
            throw ex;
        }

    }

    public void AddCulprit(CulpritUI objUI, SqlTransaction objTrans)
    {
        bool flagTransation = true;
        int Status = 0;
        CulpritDB objDB = new CulpritDB();
        objDB.GroupID = objUI.GroupID;
        objDB.CulpritCode = objUI.CulpritCode;
        objDB.Description = objUI.Description;
        objDB.IsActive = objUI.IsActive;
        objDB.InReport = objUI.InReport;
        objDB.IsGroup = objUI.IsGroup;
        objDB.EffectDate = objUI.EffectDate;
        objDB.StatusID = objUI.StatusID;
        
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
            MastersManager objManager = new MastersManager();
            Status = objManager.AddCulprit(objDB, objTrans);
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

    public void AddCVoice(CVoiceUI objUI, SqlTransaction objTrans)
    {
        bool flagTransation = true;
        int Status = 0;
        CVoiceDB objDB = new CVoiceDB();
        objDB.GroupID = objUI.GroupID;
        objDB.CVoiceCode = objUI.CVoiceCode;
        objDB.Description = objUI.Description;
        objDB.IsActive = objUI.IsActive;
        objDB.InReport = objUI.InReport;
        objDB.IsGroup = objUI.IsGroup;
        objDB.EffectDate = objUI.EffectDate;
        objDB.StatusID = objUI.StatusID;
        DataAccessLayer objDataAccess = new DataAccessLayer();
        //SqlTransaction objTrans = null;
        try
        {

            if (objTrans == null)
            {
                flagTransation = false;
                objDataAccess.GetConnection.Open();
                SqlTransaction objTransaction = objDataAccess.GetConnection.BeginTransaction();
                objTrans = objTransaction;
            }
            MastersManager objManager = new MastersManager();
            Status = objManager.AddCVoice(objDB, objTrans);
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
