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
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

/// <summary>
/// Summary description for DataAccessLayer
/// </summary>

public class DataAccessLayer
{
    #region Variables
    string m_CnnStr;
    SqlConnection m_oCnn = null;
    //SqlTransaction m_Transaction = null;
    SqlCommand m_command;
    SqlDataAdapter oDataAdapter;
    #endregion

    #region Constructor
    //Constructor Logic For Initailize The Connection object
    public DataAccessLayer()
    {
        //Fetch Connection Information From Configuration File And Asign To Local Variable
        // m_CnnStr = ConfigurationSettings.AppSettings["connectionString"];
        m_CnnStr = System.Configuration.ConfigurationSettings.AppSettings["connectionString"];
            //Configuration.ConfigurationSettings.AppSettings["connectionString"];
        if (m_CnnStr.Trim() == null)
        {
            throw new Exception("Unable to reterieve Connection String from the Config file !");
        }
        else
        {
            this.m_oCnn = new SqlConnection(this.m_CnnStr);
        }
        this.oDataAdapter = new SqlDataAdapter();
    }
    #endregion

    #region Property
    //Property Statement to Give Public Access of Connection Object 
    public SqlConnection GetConnection
    {
        get
        {
            return this.m_oCnn;
        }
        set
        {
            this.m_oCnn = value;
        }
    }
    //Property Statement to Give Public Access of Command Object
    public SqlCommand Command
    {
        get
        {
            return this.m_command;
        }
        set
        {
            this.m_command = value;
        }
    }
    #endregion

    #region Member Function
    //This Function Return DataTable Object, Just Configure Command Object
    //and Get DataTable Which Can be Directly Used to Data Binding
    public DataTable GetTable()
    {
        if (this.Command.CommandText.Trim() == null)
        {
            throw new Exception("Command Text not Set !");
        }
        this.Command.CommandTimeout = 1000;
        SqlDataAdapter oDataAdapter = new SqlDataAdapter();
        oDataAdapter.SelectCommand = this.Command;

        try
        {

            DataTable oTable = new DataTable();
            oDataAdapter.Fill(oTable);
            return oTable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            this.Command.Dispose();
            this.Command = null;
            oDataAdapter.Dispose();
            oDataAdapter = null;
        }
    }

    //This Function Return DataReader Object, Just Configure Command Object
    //and Get DataReader Object
    public SqlDataReader GetReader()
    {
        if (this.Command.CommandText.Trim() == null)
        {
            throw new Exception("Command Text not Set !");
        }
        this.Command.CommandTimeout = 1000;
        SqlDataReader oDataReader;
        try
        {
            if (m_oCnn.State == ConnectionState.Closed)
                m_oCnn.Open();
            oDataReader = this.Command.ExecuteReader();
            return oDataReader;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            //oDataReader = null;
        }
    }

    //This Function Can Perform All DML Operation, Just Configure Command Object
    //and Call This Function to Get The No. Of Rows Affected By This Operation.
    public double ExecQuery()
    {
        if (this.Command.CommandText.Trim() == null)
        {
            throw new Exception("Command Text not Set !");
        }
        this.Command.CommandTimeout = 1000;
        double iAffectedRec;
        try
        {
            iAffectedRec = this.Command.ExecuteNonQuery();
            return iAffectedRec;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            this.Command.Dispose();
            this.Command = null;
        }
    }

    //This Function Return a Single Cell Resultset, Just Configure Command Object
    //and Call This Function to Get Affected Record.
    public string ExecScalar()
    {
        if (this.Command.CommandText.Trim() == null)
        {
            throw new Exception("Command Text not Set !");
        }
        this.Command.CommandTimeout = 1000;
        string iAffectedRec;
        try
        {
            iAffectedRec = Convert.ToString(this.Command.ExecuteScalar());
            return iAffectedRec;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            this.Command.Dispose();
            this.Command = null;
        }
    }

    public DataTable PutTable(DataTable oTable, SqlCommand cmdInsert, SqlCommand cmdUpdate)
    {
        SqlDataAdapter oDataAdapater = new SqlDataAdapter();
        if (cmdInsert != null)
        {
            cmdInsert.Connection = m_oCnn;
            oDataAdapter.InsertCommand = cmdInsert;
        }
        if (cmdUpdate != null)
        {
            cmdUpdate.Connection = m_oCnn;
            oDataAdapter.UpdateCommand = cmdUpdate;
        }
        try
        {
            oDataAdapter.Update(oTable);
            return oTable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            oDataAdapter.Dispose();
            cmdInsert = null;
            cmdUpdate = null;
        }
    }


    //This Method Returns Boolean For Open Status of Connection 
    public Boolean IsConnection()
    {
        if (m_oCnn.State == ConnectionState.Closed)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    #endregion
}

