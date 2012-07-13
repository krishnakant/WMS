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
/// Summary description for QueryConroller
/// </summary>
public class QueryConroller
{
	public QueryConroller()
	{
		//
		// TODO: Add constructor logic here
		//
	}

  
    public DataTable ExecuteQuery(string Query)
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
            QueryManager objManager = new QueryManager();
            DataTable dtQueryResult = objManager.ExecuteQuery(Query, objTrans);
            if (!flagTransation)
                objTrans.Commit();
            return dtQueryResult;
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

    public DataSet ExecuteMultiTableQuery(string Query)
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
            QueryManager objManager = new QueryManager();
            DataSet dtQueryResult = objManager.ExecuteMultiTableQuery(Query, objTrans);
            if (!flagTransation)
                objTrans.Commit();
            return dtQueryResult;
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


    public int getNoOfException(string Query)
    {
        bool flagTransation = true;
        int TotalException=0;
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
            QueryManager objManager = new QueryManager();
            TotalException = objManager.getNoOfException(Query, objTrans);
            if (!flagTransation)
                objTrans.Commit();
            return TotalException;
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

    public DataSet ExecuteQueryWithDataSet(string Query)
    {
        bool flagTransation = true;
        SqlCommand cmd = new SqlCommand();
        DataAccessLayer objDataAccess = new DataAccessLayer();
        
        //EMSPDI.dil.DataAccessLayer objDataAccess = new EMSPDI.dil.DataAccessLayer();
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
            QueryManager objManager = new QueryManager();
            DataSet dsQueryResult = objManager.ExecuteQueryWithDataSet(Query, objTrans);
            if (!flagTransation)
                objTrans.Commit();
            return dsQueryResult;
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
