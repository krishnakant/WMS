using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.Data.SqlClient;
using System.ComponentModel;


public partial class View_Forms_Master_Forgetyourpassword : System.Web.UI.Page
{
    QueryConroller objQuerycontroller = new QueryConroller();
    public string strProjectName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        strProjectName = System.Configuration.ConfigurationManager.AppSettings["WMSProjectName"];

    }
    protected void btnSend_Click(object sender, EventArgs e)
    {
        sendEmail();


    }


    public void sendEmail()
    {
        string strQuery = "";
        string host = "smtp.gmail.com";
        string User = "loginsonu23884@gmail.com";
        string Password = "9827664909";

        DataTable dtinformation = new DataTable();
        strQuery = "select Password from UserInfo where EmailID='" + txtEmailID.Text + "'";
        dtinformation = objQuerycontroller.ExecuteQuery(strQuery);

       

        MailAddress from = new MailAddress("loginsonu23884@gmail.com");


        MailAddress to = new MailAddress(txtEmailID.Text);

        MailMessage msg = new MailMessage(from, to);
        msg.To.Add(txtEmailID.Text);
        
            


        msg.Body =Convert.ToString(dtinformation.Rows[0]["Password"]);

       
        //Html message body
        msg.IsBodyHtml = true;

        //    assigning Priority 
        msg.Priority = MailPriority.High;

        //    giving the all SMTP server information from web.config for sending mail....
        SmtpClient client = new SmtpClient(host, 587);
        client.UseDefaultCredentials = false;
        client.EnableSsl = true;
        //     System.Net.NetworkCredential theCredential = new System.Net.NetworkCredential("excellent", "m49q3r");
        System.Net.NetworkCredential theCredential = new System.Net.NetworkCredential(User, Password);
        client.Credentials = theCredential;
        Console.WriteLine("Sending an e-mail message to {0} by using the SMTP host{1}.", to.Address, client.Host);
        client.Send(msg);//Sending the mail..
        }

    }
