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

public partial class View_Forms_Master_UpdatePasswordByAdmin : System.Web.UI.Page
{
    QueryConroller objQueryConroller = new QueryConroller();
    public string strProjectName = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        strProjectName = System.Configuration.ConfigurationManager.AppSettings["WMSProjectName"];
        if (!IsPostBack)
        {

            bindData();
        }
        string strjscript = "<script language='javascript'>";
        strjscript += " setLabelText('ctl00_ContentPlaceHolder1_Message','' );";
        strjscript += "</script" + ">";
        Literal1.Text = strjscript;
    }

    public void bindData()
    {
        string strQuery = "";
        string UserID = "0";
        if (Request.QueryString["UserID"] != null)
        {
            UserID = Request.QueryString["UserID"].ToString();
        }
        DataTable dtinformation = new DataTable();
        strQuery = " select UserID,FullName,Password from UserInfo where UserID=" + UserID + "";
       
        dtinformation = objQueryConroller.ExecuteQuery(strQuery);
        if (dtinformation != null)
        {
            if (dtinformation.Rows.Count > 0)
            {
                txtOldPassword.Text = dtinformation.Rows[0]["Password"].ToString();
                txtUser.Text = dtinformation.Rows[0]["FullName"].ToString();
                      
            }
            else
            {
                txtOldPassword.Text = "";
                txtUser.Text = "";
            }
        }
        else
        {
            txtOldPassword.Text = "";
            txtUser.Text = ""; 
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {

        string UserID = "0";
        if (Request.QueryString["UserID"] != null)
        {
            UserID = Request.QueryString["UserID"].ToString();
        }
        string strQuery = " update UserInfo set Password='" + txtPassword.Text + "'where Password='" + txtOldPassword.Text + "'and UserID='" + UserID + "'";
        objQueryConroller.ExecuteQuery(strQuery);
        string strjscript = "<script language='javascript'>";
        strjscript += " setLabelText('ctl00_ContentPlaceHolder1_Message','Password update successfully..' );";
        strjscript += "</script" + ">";
        Literal1.Text = strjscript;
        Session.Add("updatePwd", "1");
        Response.Redirect(strProjectName + "/View/Forms/Master/UserDefault.aspx");

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(strProjectName + "/View/Forms/Master/UserDefault.aspx");
    }

}
