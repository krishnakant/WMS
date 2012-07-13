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

public partial class master_LoginMasterPage : System.Web.UI.MasterPage
{
    public string strProjectName = "";
    QueryConroller objQueryController = new QueryConroller();
    protected void Page_Load(object sender, EventArgs e)
    {

        strProjectName = Convert.ToString(ConfigurationManager.AppSettings["WMSProjectName"].ToString());
        if (!IsPostBack)
        {
            if (Request.QueryString["from"] != null)
            {
                if (Request.QueryString["from"].ToString() == "SHQ")
                {
                    Session["FormName"] = null;
                    LoginUSER();
                }
            }
        }
    }
    public void LoginUSER()
    {
        string strQuery = "";
        DataTable dtinformation = new DataTable();

        int userID = 0;
        if (Request.Cookies["UserID"] != null)
        {
            HttpCookie aCookie = Request.Cookies["UserID"];
            userID = Convert.ToInt32(Server.HtmlEncode(aCookie.Value));
        }

        strQuery = "Select * from TempLoginUser where UserID=" + userID;
        dtinformation = objQueryController.ExecuteQuery(strQuery);
        if (dtinformation != null)
        {
            if (dtinformation.Rows.Count > 0)
            {
                Session.Add("SHQ", 1);
                Session.Add("sessionLoginID", dtinformation.Rows[0]["LoginID"].ToString());
                Session.Add("sessionUserID", dtinformation.Rows[0]["UserID"].ToString());
                Response.Redirect(strProjectName + "/View/Forms/Master/Default.aspx");
              
            }
        }
    }
}
