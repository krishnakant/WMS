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
using System.Net.Mail;
/// <summary>
/// Summary description for QueryManager
/// </summary>
public class QueryManager
{
	public QueryManager()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable ExecuteQuery(string strQuery, SqlTransaction objTrans)
    {

        SqlCommand cmd = new SqlCommand();
        DataAccessLayer objDataAccess = new DataAccessLayer();
        try
        {
            cmd.CommandText = strQuery;
            cmd.Transaction = objTrans;
            cmd.Connection = objTrans.Connection;
            objDataAccess.Command = cmd;
            DataTable dt = new DataTable();
            dt = objDataAccess.GetTable();
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            cmd.Dispose();
        }
    }

    public DataSet ExecuteMultiTableQuery(string strQuery, SqlTransaction objTrans)
    {

        SqlCommand cmd = new SqlCommand();
        DataAccessLayer objDataAccess = new DataAccessLayer();
        try
        {
            cmd.CommandText = strQuery;
            cmd.CommandType = CommandType.Text;

            cmd.CommandTimeout = 1000;
            cmd.Transaction = objTrans;
            cmd.Connection = objTrans.Connection;
            objDataAccess.Command = cmd;
            DataSet ds = new DataSet();
            SqlDataAdapter Sda = new SqlDataAdapter(cmd);
            Sda.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            cmd.Dispose();
        }
    }

    public DataSet ExecuteQueryWithDataSet(string strQuery, SqlTransaction objTrans)
    {
        

        SqlCommand cmd = new SqlCommand();
        DataAccessLayer objDataAccess = new DataAccessLayer();
        try
        {

            cmd.CommandText = "execute usp_getMultipleTable '" + strQuery + "'";
            cmd.CommandType = CommandType.Text;

           
            cmd.Transaction = objTrans;
            cmd.Connection = objTrans.Connection;
            objDataAccess.Command = cmd;
            DataSet ds = new DataSet();
            SqlDataAdapter Sda = new SqlDataAdapter(cmd);
            Sda.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            cmd.Dispose();
        }
    }
    public int getNoOfException(string strQuery, SqlTransaction objTrans)
    {

        SqlCommand cmd = new SqlCommand();
        DataAccessLayer objDataAccess = new DataAccessLayer();
        try
        {
            cmd.CommandText = strQuery;
            cmd.Transaction = objTrans;
            cmd.Connection = objTrans.Connection;
            objDataAccess.Command = cmd;
            DataTable dt = new DataTable();
            dt = objDataAccess.GetTable();

            return Convert.ToInt32(dt.Rows[0]["Rowcount"]);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            cmd.Dispose();
        }
    }
    public DataTable getMailSetting()
    {
        try
        {

            DataTable dt = new DataTable();
            DataSet ds = new DataSet("mailSettingInfo");
            ds.ReadXml(HttpRuntime.AppDomainAppPath + "/js/xmlDB/MailDB.xml");
            dt = (DataTable)ds.Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {

        }
    }
    public bool MailSend(string Body,string Subject, string LoginName, string FromEmailID,string  ToEmailID)
    {
        bool status = false;
        try
        {

            QueryManager objManager = new QueryManager();
            DataTable dtsmtpserver = objManager.getMailSetting();
            // string senduserEMail = dtsmtpserver.Rows[0][8].ToString();
            int Port = Convert.ToInt32(dtsmtpserver.Rows[0][4].ToString());
            string host = dtsmtpserver.Rows[0][3].ToString();
            string User = dtsmtpserver.Rows[0][1].ToString();
            string Password = dtsmtpserver.Rows[0][2].ToString();
            //***************Updated Code*********************
            // MailAddress from = new MailAddress(senduserEMail);
            if (FromEmailID == "")
            {
                FromEmailID = dtsmtpserver.Rows[0][10].ToString();
            }
            MailAddress from = new MailAddress(FromEmailID);


            MailAddress to = new MailAddress(ToEmailID);
           
            MailMessage msg = new MailMessage(from, to);

            //msg.Body = Body.ToString();
            msg.Body = Body;
            // Subject
            msg.Subject = Subject;
            // Html message body
            msg.IsBodyHtml = true;

            //assigning Priority 
            msg.Priority = MailPriority.High;

            //giving the all SMTP server information from web.config for sending mail....
            SmtpClient client = new SmtpClient(host, Port);//
            //client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            // System.Net.NetworkCredential theCredential = new System.Net.NetworkCredential("excellent", "m49q3r");
            System.Net.NetworkCredential theCredential = new System.Net.NetworkCredential(User, Password);
            client.Credentials = theCredential;
            Console.WriteLine("Sending an e-mail message to {0} by using the SMTP host{1}.", to.Address, client.Host);
            client.Send(msg);//Sending the mail..
            status = true;
            return status;
        }

        catch (Exception ex)
        {

            throw ex;
        }
        finally { }

    }
    public bool MailSendTo(string Body, string Subject, string ToEmailID)
    {
        bool status = false;
        try
        {

            QueryManager objManager = new QueryManager();
            DataTable dtsmtpserver = objManager.getMailSetting();
            // string senduserEMail = dtsmtpserver.Rows[0][8].ToString();
            int Port = Convert.ToInt32(dtsmtpserver.Rows[0][4].ToString());
            string host = dtsmtpserver.Rows[0][3].ToString();
            string User = dtsmtpserver.Rows[0][1].ToString();
            string Password = dtsmtpserver.Rows[0][2].ToString();
            //***************Updated Code*********************
            // MailAddress from = new MailAddress(senduserEMail);
        
            MailAddress from = new MailAddress(dtsmtpserver.Rows[0][10].ToString());


            MailAddress to = new MailAddress(ToEmailID);

            MailMessage msg = new MailMessage(from, to);
            string[] strUser = ToEmailID.Split(',');
            for (int i = 1; i < strUser.Length; i++)
            {

                msg.To.Add(strUser[i]);
            }
            //msg.Body = Body.ToString();
            msg.Body = Body;
            // Subject
            msg.Subject = Subject;
            // Html message body
            msg.IsBodyHtml = true;

            //assigning Priority 
            msg.Priority = MailPriority.High;

            //giving the all SMTP server information from web.config for sending mail....
            SmtpClient client = new SmtpClient(host, Port);//
            //client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            // System.Net.NetworkCredential theCredential = new System.Net.NetworkCredential("excellent", "m49q3r");
            System.Net.NetworkCredential theCredential = new System.Net.NetworkCredential(User, Password);
            client.Credentials = theCredential;
            Console.WriteLine("Sending an e-mail message to {0} by using the SMTP host{1}.", to.Address, client.Host);
            client.Send(msg);//Sending the mail..
            status = true;
            return status;
        }

        catch (Exception ex)
        {

            throw ex;
        }
        finally { }

    }
}
