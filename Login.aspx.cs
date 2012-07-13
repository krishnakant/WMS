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

public partial class Login : System.Web.UI.Page
{
    QueryConroller objContoller = new QueryConroller();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["ID"] = null;
            
            
            string strjscript = "<script language='javascript' type='text/javascript'>";
            strjscript += " setLabelText('ctl00_ContentPlaceHolder1_lblMessage','' );";
            strjscript += "</script" + ">";
            literal1.Text = strjscript;
        }
    }
    protected void btnLogin_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dtUser = new DataTable();
        string strQuery = "select * from vw_UserInfo where LoginID='" + txtun.Text.Trim() + "' and password='" + txtpwd.Text + "'";
        dtUser = objContoller.ExecuteQuery(strQuery);
        if (dtUser != null)
        {
            if (dtUser.Rows.Count > 0)
            {
                string username = Convert.ToString(dtUser.Rows[0]["LoginID"]);
                string password = Convert.ToString(dtUser.Rows[0]["Password"]);
                int UserID = Convert.ToInt16(dtUser.Rows[0]["UserID"]);
                Session.Add("ID", UserID);
                Session.Add("username", username);
                Session.Add("MenuFileName", dtUser.Rows[0]["Role"].ToString() + ".js");
              
                Response.Redirect("/WMS/View/Forms/Master/Default.aspx");
            }
            else
            {
                string strjscript = "<script language='javascript' type='text/javascript'>";
                strjscript += " setLabelText('ctl00_ContentPlaceHolder1_lblMessage','Invalid Login ID or Password' );";
                strjscript += "</script" + ">";
                literal1.Text = strjscript;

            }
        }
        else
        {
            string strjscript = "<script language='javascript' type='text/javascript'>";
            strjscript += " setLabelText('ctl00_ContentPlaceHolder1_lblMessage','Invalid Login ID or Password' );";
            strjscript += "</script" + ">";
            literal1.Text = strjscript;
          
        }
       
    }
}
