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

public partial class master_MasterPage : System.Web.UI.MasterPage
{
    QueryConroller objQueryController = new QueryConroller();
    public string ScriptSource = "";
    public string strProjectName = "";
    public string menu = "";
    public string strMenu = "";
    public string strHeader = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        strProjectName = System.Configuration.ConfigurationManager.AppSettings["WMSProjectName"];
        MenuDescriptor objMenuDescriptor = new MenuDescriptor();


        if (Session["sessionLoginID"] == null)
        {
            LoginUSER();
        }
        if (Session["sessionLoginID"] != null)
        {
            int UserID = 0;
            if (Request.Cookies["UserID"] != null)
            {
                HttpCookie aCookie = Request.Cookies["UserID"];
                UserID = Convert.ToInt32(Server.HtmlEncode(aCookie.Value));
            }
            if (Session["SHQ"] != null)
            {
                hdnSHQ.Value = "1";
            }
            if (Request.QueryString["Form"] != null)
            {
                if (Request.QueryString["Form"].ToString() == "SHQ")
                {
                    Session.Add("FormName", "SHQ");
                    string loginID = (Session["sessionLoginID"].ToString());
                    lblWelcomenew.InnerHtml = "Welcome" + "  " + " " + loginID;
                    strHeader = "Service Head Quarter";
                    menu = "SHQ.js";
                    menu = "/SHQ/js/" + menu;
                    string strQuery = "Select * from TempLoginUser";
                    DataTable dt = objQueryController.ExecuteQuery(strQuery);
                    int RoleID = 0;
                    int ModuleID = 7;
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            DataView dv = new DataView(dt);
                            dv.RowFilter = "UserID=" + UserID;
                            dt = dv.ToTable();
                            foreach (DataRow dr in dt.Rows)
                            {
                                RoleID = Convert.ToInt32(dr["RoleID"].ToString());
                            }
                        }
                    }
                    strMenu = objMenuDescriptor.GetMenu(RoleID, ModuleID);
                }
                if (Request.QueryString["Form"].ToString() == "WMS")
                {
                    Session.Add("FormName", "WMS");
                    string loginID = (Session["sessionLoginID"].ToString());
                    lblWelcomenew.InnerHtml = "Welcome" + "  " + " " + loginID;
                    strHeader = "Warranty Management System ";
                    menu = "SHQ.js";
                    menu = "/SHQ/js/" + menu;
                    string strQuery = "Select * from TempLoginUser";
                    DataTable dt = objQueryController.ExecuteQuery(strQuery);
                    int RoleID = 0;
                    int ModuleID = 2;
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            DataView dv = new DataView(dt);
                            dv.RowFilter = "UserID=" + UserID;
                            dt = dv.ToTable();
                            foreach (DataRow dr in dt.Rows)
                            {
                                RoleID = Convert.ToInt32(dr["RoleID"].ToString());
                            }
                        }
                    }
                    strMenu = objMenuDescriptor.GetMenu(RoleID, ModuleID);
                }
                if (Request.QueryString["Form"].ToString() == "CSI")
                {
                    Session.Add("FormName", "CSI");
                    string loginID = (Session["sessionLoginID"].ToString());
                    lblWelcomenew.InnerHtml = "Welcome" + "  " + " " + loginID;
                    strHeader = "Customer Satisfaction Index ";
                    menu = "CSI.js";
                    menu = "/SSMgmt/js/" + menu;
                    string strQuery = "Select * from TempLoginUser";
                    DataTable dt = objQueryController.ExecuteQuery(strQuery);
                    int RoleID = 0;
                    int ModuleID = 6;
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            DataView dv = new DataView(dt);
                            dv.RowFilter = "UserID=" + UserID;
                            dt = dv.ToTable();
                            foreach (DataRow dr in dt.Rows)
                            {
                                RoleID = Convert.ToInt32(dr["RoleID"].ToString());
                            }
                        }
                    }
                    strMenu = objMenuDescriptor.GetMenu(RoleID, ModuleID);
                }
                if (Request.QueryString["Form"].ToString() == "EMSPDI")
                {
                    Session.Add("FormName", "EMSPDI");
                    string loginID = (Session["sessionLoginID"].ToString());
                    lblWelcomenew.InnerHtml = "Welcome" + "  " + " " + loginID;
                    strHeader = "Pre Delivery Inspection";
                    menu = "EMSPDI.js";
                    menu = "/EMSPDI/js/" + menu;
                    string strQuery = "Select * from TempLoginUser";
                    DataTable dt = objQueryController.ExecuteQuery(strQuery);
                    int RoleID = 0;
                    int ModuleID = 1;
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            DataView dv = new DataView(dt);
                            dv.RowFilter = "UserID=" + UserID;
                            dt = dv.ToTable();
                            foreach (DataRow dr in dt.Rows)
                            {
                                RoleID = Convert.ToInt32(dr["RoleID"].ToString());
                            }
                        }
                    }
                    strMenu = objMenuDescriptor.GetMenu(RoleID, ModuleID);
                }
            }
            else
            {
                if (Session["FormName"] != null)
                {
                    int check = 0;
                    if (Session["FormName"].ToString() == "SHQ")
                    {
                        Session.Add("FormName", "SHQ");
                        string loginID = (Session["sessionLoginID"].ToString());
                        lblWelcomenew.InnerHtml = "Welcome" + "  " + " " + loginID;
                        strHeader = "Service Head Quarter";
                        menu = "SHQ.js";
                        menu = "/SHQ/js/" + menu;
                        string strQuery = "Select * from TempLoginUser";
                        DataTable dt = objQueryController.ExecuteQuery(strQuery);
                        int RoleID = 0;
                        int ModuleID = 7;
                        if (dt != null)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                DataView dv = new DataView(dt);
                                dv.RowFilter = "UserID=" + UserID;
                                dt = dv.ToTable();
                                foreach (DataRow dr in dt.Rows)
                                {
                                    RoleID = Convert.ToInt32(dr["RoleID"].ToString());
                                }
                            }
                        }
                        strMenu = objMenuDescriptor.GetMenu(RoleID, ModuleID);
                        check = 1;
                    }
                    if (Session["FormName"].ToString() == "CSI")
                    {
                        Session.Add("FormName", "CSI");
                        string loginID = (Session["sessionLoginID"].ToString());
                        lblWelcomenew.InnerHtml = "Welcome" + "  " + " " + loginID;
                        strHeader = "Customer Satisfaction Index ";
                        menu = "CSI.js";
                        menu = "/SSMgmt/js/" + menu;
                        string strQuery = "Select * from TempLoginUser";
                        DataTable dt = objQueryController.ExecuteQuery(strQuery);
                        int RoleID = 0;
                        int ModuleID = 6;
                        if (dt != null)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                DataView dv = new DataView(dt);
                                dv.RowFilter = "UserID=" + UserID;
                                dt = dv.ToTable();
                                foreach (DataRow dr in dt.Rows)
                                {
                                    RoleID = Convert.ToInt32(dr["RoleID"].ToString());
                                }
                            }
                        }
                        strMenu = objMenuDescriptor.GetMenu(RoleID, ModuleID);
                        check = 1;
                    }
                    if (Session["FormName"].ToString() == "EMSPDI")
                    {
                        Session.Add("FormName", "EMSPDI");
                        string loginID = (Session["sessionLoginID"].ToString());
                        lblWelcomenew.InnerHtml = "Welcome" + "  " + " " + loginID;
                        strHeader = "Pre Delivery Inspection";
                        menu = "EMSPDI.js";
                        menu = "/EMSPDI/js/" + menu;
                        string strQuery = "Select * from TempLoginUser";
                        DataTable dt = objQueryController.ExecuteQuery(strQuery);
                        int RoleID = 0;
                        int ModuleID = 1;
                        if (dt != null)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                DataView dv = new DataView(dt);
                                dv.RowFilter = "UserID=" + UserID;
                                dt = dv.ToTable();
                                foreach (DataRow dr in dt.Rows)
                                {
                                    RoleID = Convert.ToInt32(dr["RoleID"].ToString());
                                }
                            }
                        }
                        strMenu = objMenuDescriptor.GetMenu(RoleID, ModuleID);
                        check = 1;
                    }
                    if (check == 0)
                    {
                        string loginID = (Session["sessionLoginID"].ToString());
                        lblWelcomenew.InnerHtml = "Welcome" + "  " + " " + loginID;
                        strHeader = "Warranty Management System ";
                        menu = "WMS.js";
                        menu = "/WMS/JS/" + menu;
                        string strQuery = "Select * from TempLoginUser";
                        DataTable dt = objQueryController.ExecuteQuery(strQuery);
                        int RoleID = 0;
                        int ModuleID = 2;
                        if (dt != null)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                DataView dv = new DataView(dt);
                                dv.RowFilter = "UserID=" + UserID;
                                dt = dv.ToTable();
                                foreach (DataRow dr in dt.Rows)
                                {
                                    RoleID = Convert.ToInt32(dr["RoleID"].ToString());
                                }
                            }
                        }
                        strMenu = objMenuDescriptor.GetMenu(RoleID, ModuleID);
                    }
                }
                else
                {
                    string loginID = (Session["sessionLoginID"].ToString());
                    lblWelcomenew.InnerHtml = "Welcome" + "  " + " " + loginID;
                    strHeader = "Warranty Management System ";
                    menu = "WMS.js";
                    menu = "/WMS/JS/" + menu;
                    string strQuery = "Select * from TempLoginUser";
                    DataTable dt = objQueryController.ExecuteQuery(strQuery);
                    int RoleID = 0;
                    int ModuleID = 2;
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            DataView dv = new DataView(dt);
                            dv.RowFilter = "UserID=" + UserID;
                            dt = dv.ToTable();
                            foreach (DataRow dr in dt.Rows)
                            {
                                RoleID = Convert.ToInt32(dr["RoleID"].ToString());
                            }
                        }
                    }
                    strMenu = objMenuDescriptor.GetMenu(RoleID, ModuleID);
                }
            }
        }
        else
        {
            if (Session["ID"] == null)
            {
                Response.Redirect("/SHQ/Login.aspx");
            }
            else
            {
                string loginID = (Session["username"].ToString());
                lblWelcomenew.InnerHtml = "Welcome" + "  " + " " + loginID;
                //ScriptSource = Session["MenuFileName"].ToString();
                strHeader = "Warranty Management System ";
                menu = "WMS.js";
                menu = "/WMS/JS/" + menu;
                string strQuery = "Select * from TempLoginUser";
                DataTable dt = objQueryController.ExecuteQuery(strQuery);
                int RoleID = 0;
                int ModuleID = 2;
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        int UserID = Convert.ToInt32(Session["ID"].ToString());
                        DataView dv = new DataView(dt);
                        dv.RowFilter = "UserID=" + UserID;
                        dt = dv.ToTable();
                        foreach (DataRow dr in dt.Rows)
                        {
                            RoleID = Convert.ToInt32(dr["RoleID"].ToString());
                        }
                    }
                }
                strMenu = objMenuDescriptor.GetMenu(RoleID, ModuleID);
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
                Session.Add("username", dtinformation.Rows[0]["LoginID"].ToString());
              

            }
        }
    }
}
