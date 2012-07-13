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
/// Summary description for ModelController
/// </summary>
public class ModelController
{
    public ModelController()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int CheckMaterial(ModelUI objUI, System.Data.SqlClient.SqlTransaction objTrans)
    {
        int Flag = 0;
        bool flagTransation = true;

        ModelDB objDB = new ModelDB();
        objDB.Material = objUI.Material;
       
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
            ModelManager objManager = new ModelManager();
            Flag = objManager.CheckMaterial(objDB, objTrans);
            if (!flagTransation)
                objTrans.Commit();
            return Flag;
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


    public int SaveModelDetail(ModelUI objUI, System.Data.SqlClient.SqlTransaction objTrans)
    {
        int Flag = 0;
        bool flagTransation = true;

        ModelDB objDB = new ModelDB();
        objDB.Material = objUI.Material;
        objDB.GroupID = objUI.GroupID;
        objDB.ModelCategoryID = objUI.ModelCategoryID;
        objDB.ModelSpecialID = objUI.ModelSpecialID;
        objDB.ClutchTypeID = objUI.ClutchTypeID;
        objDB.Description = objUI.Description;

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
            ModelManager objManager = new ModelManager();
            Flag = objManager.SaveModelDetail(objDB, objTrans);
            if (!flagTransation)
                objTrans.Commit();
            return Flag;
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

    public int UpdateModelDetail(ModelUI objUI, System.Data.SqlClient.SqlTransaction objTrans)
    {
        int Flag = 0;
        bool flagTransation = true;

        ModelDB objDB = new ModelDB();
        objDB.Material = objUI.Material;
        objDB.GroupID = objUI.GroupID;
        objDB.ModelCategoryID = objUI.ModelCategoryID;
        objDB.ModelSpecialID = objUI.ModelSpecialID;
        objDB.ClutchTypeID = objUI.ClutchTypeID;
        objDB.Description = objUI.Description;
        objDB.MappingID = objUI.MappingID;
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
            ModelManager objManager = new ModelManager();
            Flag = objManager.UpdateModelDetail(objDB, objTrans);
            if (!flagTransation)
                objTrans.Commit();
            return Flag;
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

