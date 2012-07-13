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

public partial class View_Forms_Master_Role : System.Web.UI.Page
{
    QueryConroller objQuerycontroller = new QueryConroller();
    RoleController objController = new RoleController();
    RoleUI objUI = new RoleUI();


    public string strProjectName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        strProjectName = System.Configuration.ConfigurationManager.AppSettings["WMSProjectName"];
        if (!IsPostBack)
        {
            ChangelblName();
            if (Request.QueryString["RoleID"] != null)
            {

                getDetail(Request.QueryString["RoleID"].ToString());
            }
        }

    }

    public void ChangelblName()
    {
        DataTable dtControls = new DataTable();
        string strQuery = "Select * from Label_Data where Form='Role'";
        dtControls = objQuerycontroller.ExecuteQuery(strQuery);
        foreach (DataRow dr in dtControls.Rows)
        {
            string strLabel = dr["Label_ID"].ToString();
            string strLabelText = dr["Text"].ToString();

            if (strLabel == "lblRole")
            {
              lblRole.Text = strLabelText;
            }
            else if (strLabel == "lblActive")
            {
               lblActive.Text = strLabelText;
            }
           
        }


    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveRole();
    }
    public int SaveRole()
    {
        int RoleId = 0;
        
        DataTable dtControls = new DataTable();
        if (hdnRole.Value.Trim().ToLower() !=txtRole.Text.Trim().ToLower())
        {

            string strQuery = "Select * from Role where Role='" + txtRole.Text.Trim()+ "'";
            dtControls = objQuerycontroller.ExecuteQuery(strQuery);
        }
        if (dtControls == null || dtControls.Rows.Count == 0)
        {
            objUI.Role = txtRole.Text.Trim();
            objUI.IsActive = chkActive.Checked;
            objUI.Id = Convert.ToInt32(hdnRoleID.Value);
            objUI.CheckID = Convert.ToInt32(hdnCheckID.Value);


            RoleId = objController.Save(objUI, null);
            if (RoleId == 0)
            {
                string strjscript = "<script language='javascript' type='text/javascript'>";
                strjscript += " setLabelText('ctl00_ContentPlaceHolder1_lblMessage','role already exists' );";
                strjscript += "</script" + ">";
                literal1.Text = strjscript;
            }
            else
            {
                Response.Redirect(strProjectName+"/View/Forms/Master/RoleDefault.aspx");
            }
        }
        else
        {
            string strjscript = "<script language='javascript' type='text/javascript'>";
            strjscript += " setLabelText('ctl00_ContentPlaceHolder1_lblMessage','role already exists' );";
            strjscript += "</script" + ">";
            literal1.Text = strjscript;
        
        }

        return RoleId;
    }

    public void getDetail(string RoleID)
    {
        string strQuery = "";
        hdnRoleID.Value = RoleID;
        hdnCheckID.Value = "1";
        DataTable dtinformation = new DataTable();
        btnSave.ToolTip = "Update";
        btnSave.Text = "Update";

        // strQuery = "select * from Location where IsActive=1";
        strQuery = "select * from Role where RoleID=" + RoleID + " ";
        dtinformation = objQuerycontroller.ExecuteQuery(strQuery);
        if (dtinformation != null)
        {


            if (dtinformation.Rows.Count > 0)
            {
                foreach (DataRow drinformation in dtinformation.Rows)
                {
                 txtRole.Text= drinformation["Role"].ToString();
                   hdnRole.Value=drinformation["Role"].ToString(); 
                    if (Convert.ToBoolean(drinformation["IsActive"]) == true)
                    {
                      chkActive.Checked = true;
                    }
                    else
                    {
                         chkActive.Checked = false;
                    }


                 
   

            }
            } 
        }   
     }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(strProjectName+"/View/Forms/Master/RoleDefault.aspx");
    }
}