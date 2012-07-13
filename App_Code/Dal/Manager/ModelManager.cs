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
/// Summary description for ModelManager
/// </summary>
public class ModelManager
{

    static ModelManager instance = null;

    public static ModelManager Instance
    {
        get
        {
            if (instance == null)
                instance = new ModelManager();
            return instance;
        }
    }

    public int CheckMaterial(ModelDB objDB, SqlTransaction objTrans)
    {
        int flag = 0;
        DataAccessLayer objDataLayer = new DataAccessLayer();
        SqlCommand objCmd = new SqlCommand();
        try
        {

            objCmd.CommandText = "Usp_CheckMaterial";
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.AddWithValue("@Material", objDB.Material);
            objCmd.Parameters.AddWithValue("@flag", SqlDbType.Int);
            objCmd.Parameters["@flag"].Direction = ParameterDirection.Output;
            objCmd.Transaction = objTrans;
            objCmd.Connection = objTrans.Connection;
            objDataLayer.Command = objCmd;
            objDataLayer.ExecQuery();
            flag = Convert.ToInt32(objCmd.Parameters["@flag"].Value);
            return flag;

        }
        catch (Exception ex)
        {

            throw ex;
        }



    }


    public int SaveModelDetail(ModelDB objDB, SqlTransaction objTrans)
    {
        int MappingID = 0;
        DataAccessLayer objDataLayer = new DataAccessLayer();
        SqlCommand objCmd = new SqlCommand();
        try
        {

            objCmd.CommandText = "Usp_SaveModelMapping";
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.AddWithValue("@Material", objDB.Material);
            objCmd.Parameters.AddWithValue("@GroupID", objDB.GroupID);
            objCmd.Parameters.AddWithValue("@ModelSpecialID", objDB.ModelSpecialID);
            objCmd.Parameters.AddWithValue("@ClutchTypeID", objDB.ClutchTypeID);
            objCmd.Parameters.AddWithValue("@ModelCategoryID", objDB.ModelCategoryID);
            objCmd.Parameters.AddWithValue("@Description", objDB.Description);

            objCmd.Parameters.AddWithValue("@MappingID", SqlDbType.Int);
            objCmd.Parameters["@MappingID"].Direction = ParameterDirection.Output;
            objCmd.Transaction = objTrans;
            objCmd.Connection = objTrans.Connection;
            objDataLayer.Command = objCmd;
            objDataLayer.ExecQuery();
            MappingID = Convert.ToInt32(objCmd.Parameters["@MappingID"].Value);
            return MappingID;

        }
        catch (Exception ex)
        {

            throw ex;
        }



    }


    public int UpdateModelDetail(ModelDB objDB, SqlTransaction objTrans)
    {
        int MappingID = 0;
        DataAccessLayer objDataLayer = new DataAccessLayer();
        SqlCommand objCmd = new SqlCommand();
        try
        {

            objCmd.CommandText = "Usp_UpdateModelMapping";
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.AddWithValue("@Material", objDB.Material);
            objCmd.Parameters.AddWithValue("@GroupID", objDB.GroupID);
            objCmd.Parameters.AddWithValue("@ModelSpecialID", objDB.ModelSpecialID);
            objCmd.Parameters.AddWithValue("@ClutchTypeID", objDB.ClutchTypeID);
            objCmd.Parameters.AddWithValue("@ModelCategoryID", objDB.ModelCategoryID);
            objCmd.Parameters.AddWithValue("@Description", objDB.Description);
            objCmd.Parameters.AddWithValue("@MappingID", objDB.MappingID);
            
            objCmd.Transaction = objTrans;
            objCmd.Connection = objTrans.Connection;
            objDataLayer.Command = objCmd;
            objDataLayer.ExecQuery();
            MappingID = Convert.ToInt32(objCmd.Parameters["@MappingID"].Value);
            return MappingID;

        }
        catch (Exception ex)
        {

            throw ex;
        }

    }

}
