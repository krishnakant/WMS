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

public partial class View_Forms_Master_ChangPassword : System.Web.UI.Page
{
    QueryConroller objQuerycontroller = new QueryConroller();
    public string strProjectName = "";
    int LevelID;
    int UserID;
    int RoleID;
    string LoginID;
    string RegionParameter;
    string UserParameter;
    protected void Page_Load(object sender, EventArgs e)
    {
        getAuthenticationDetails();
        strProjectName = System.Configuration.ConfigurationManager.AppSettings["WMSProjectName"];
      
       
           
            lblUserName.Text = LoginID;
         
        

    }

    public void getAuthenticationDetails()
    {
        UserParameter = "";
        if (Request.Cookies["UserID"] != null)
        {
            HttpCookie aCookie = Request.Cookies["UserID"];
            UserID = Convert.ToInt32(Server.HtmlEncode(aCookie.Value));
        }

        string strUserDetailsQuery = " Select * from TempLoginUser where UserID=" + UserID;
        DataTable dtUserDetails = objQuerycontroller.ExecuteQuery(strUserDetailsQuery);
        if (dtUserDetails != null)
        {
            if (dtUserDetails.Rows.Count > 0)
            {
                foreach (DataRow drUserDetails in dtUserDetails.Rows)
                {
                    LevelID = Convert.ToInt32(drUserDetails["LevelID"]);
                    RoleID = Convert.ToInt32(drUserDetails["RoleID"]);
                    LoginID = drUserDetails["LoginID"].ToString();
                    RegionParameter = drUserDetails["RegionParameter"].ToString();

                }
            }
        }
        string strQuery = "";
        if (LevelID == 3)
        {
            strQuery = " Select distinct UserID from vw_UserDetail where ReportsToID=" + UserID;
            DataTable dtUserParameter = objQuerycontroller.ExecuteQuery(strQuery);
            if (dtUserParameter != null)
            {
                if (dtUserParameter.Rows.Count > 0)
                {
                    int flag = 0;
                    foreach (DataRow drUserParameter in dtUserParameter.Rows)
                    {
                        if (flag != 0)
                        {
                            UserParameter += " or ";
                        }
                        UserParameter += " UserID=" + drUserParameter["UserID"].ToString();
                        flag++;
                    }
                }
            }
        }
        if (LevelID > 3)
        {
            UserParameter = " UserID=" + UserID;
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
         string strQuery = "";
         //string LoginID = "";
         //if (Session["username"].ToString() != null)
         //{

         //    LoginID = Session["username"].ToString();
         //}
        DataTable dtinformation = new DataTable();
        strQuery = "select UserID from UserInfo where LoginID='" + LoginID + "' and Password='" + txtPassword.Text + "'";
        dtinformation = objQuerycontroller.ExecuteQuery(strQuery);
        if (dtinformation != null)
        {
            if (dtinformation.Rows.Count > 0)
            {
                strQuery = " update UserInfo set Password='" + txtNewPassword.Text + "'where Password='" + txtPassword.Text + "'and LoginID='" + LoginID + "'";
                dtinformation = objQuerycontroller.ExecuteQuery(strQuery);
                string str = "<script language = 'javascript'>";
                str += "setMessageText('ctl00_ContentPlaceHolder1_lblMessage','Password update successfully..');";
                str += "</script>";
                literal1.Text = str;
                Label1.Text = "Password update successfully..";
                Label1.Visible = true;
               
            }
            else
            {
                string str = "<script language = 'javascript'>";
                str += "setMessageText('ctl00_ContentPlaceHolder1_lblMessage','Don't Match Password and LoginID');";
                str += "</script>";
                literal1.Text = str;
                Label1.Text = "Don't Match Password and LoginID";
                Label1.Visible = true;
            }
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("/SHQ/Default.aspx");
    }
    
    }


