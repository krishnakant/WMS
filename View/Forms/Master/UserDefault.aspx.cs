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

using System.Text;

using System.IO;

public partial class View_Forms_Master_UserDefault : System.Web.UI.Page
{

    QueryConroller objQuerycontroller = new QueryConroller();
    public string strProjectName = "";
    Utility objUtility = new Utility();
  
    int LevelID;
    int UserID;
    int RoleID;
    string UserParameter;
    string RegionParameter;
    static string str = "";
    string strItemCodeID;
    protected void Page_Load(object sender, EventArgs e)
    {

        strProjectName = System.Configuration.ConfigurationManager.AppSettings["WMSProjectName"];
        getAuthenticationDetails();
        if (!IsPostBack)
        {
            if (Session["updatePwd"] == "1")
            {
                string strjscript = "<script language='javascript' type='text/javascript'>";
                strjscript += " setLabelText('ctl00_ContentPlaceHolder1_lblMessage','Password Update Successfully..');";
                strjscript += "</script" + ">";
                literal1.Text = strjscript;
                Session["updatePwd"] = null;
            }
            bindData();
        }
        else
        {
            string strjscript = "<script language='javascript' type='text/javascript'>";
            strjscript += " setLabelText('ctl00_ContentPlaceHolder1_lblMessage','');";
            strjscript += "</script" + ">";
            literal1.Text = strjscript;
        }

    }

    protected void gridView_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate))
        {
            e.Row.TabIndex = -1;
            e.Row.Attributes["onclick"] = string.Format("javascript:SelectRow(this, {0});", e.Row.RowIndex);
            e.Row.Attributes["onkeydown"] = "javascript:return SelectSibling(event);";
            e.Row.Attributes["onselectstart"] = "javascript:return false;";
        }
    } 

    public void bindData()
    {
        string strQuery = "";

        DataTable dtinformation = new DataTable();
        if (txtsearch.Text != "")
        {
            strQuery = "select UserID,FullName,PermanentAddress,CurrentAddress,EmployeeCode,IsActive, Role,LoginID,EmailID,PhoneNo,MobileNo ,UserTypeName ,REPLACE(cast(day(DateOfJoing)as varchar)+'/'+cast(month(DateOfJoing)as varchar)+'/'+substring(Datename(year,DateOfJoing),1,4),'1/1/1900','') as DateOfJoing from vw_UserInfo where Fullname like '" + txtsearch.Text + "%' order by fullname";
        }
        else
        {
            strQuery = "select UserID,FullName,PermanentAddress,CurrentAddress,EmployeeCode,IsActive, Role,LoginID,EmailID,PhoneNo,MobileNo ,UserTypeName ,REPLACE(cast(day(DateOfJoing)as varchar)+'/'+cast(month(DateOfJoing)as varchar)+'/'+substring(Datename(year,DateOfJoing),1,4),'1/1/1900','') as DateOfJoing from vw_UserInfo  order by fullname";
        }
        dtinformation = objQuerycontroller.ExecuteQuery(strQuery);
        if (dtinformation != null)
        {
            if (dtinformation.Rows.Count > 0)
            {
                GridView1.DataSource = dtinformation;
                GridView1.DataBind();
                if (RoleID == 2)
                {
                    GridView1.Columns[15].Visible = true;
                }
                else
                {
                    GridView1.Columns[15].Visible = false;
                }
              
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }
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

   
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect(strProjectName+"/View/Forms/Master/User.aspx");
    }

    protected void btnGO_Click(object sender, EventArgs e)
    {
        bindData();
    }
    public void eventhandlerSerialNo(object Sender, EventArgs E)
    {
        int i = 1;
        int pi = GridView1.PageIndex;
        int ps = GridView1.PageSize;
        //<><> Use Name of Your GridView Instead Of gvDetailProspect <><>// 
        if (pi > 0)
        {
            i = pi * ps + 1;
        }
        foreach (GridViewRow gr in GridView1.Rows)
        {
            gr.Cells[0].Text = i.ToString();

            i++;
        }
    }

    protected void Role_RowDeleted(Object sender, GridViewDeleteEventArgs e)
    {
        int check = 0;
        string strQuery = "";
        string strseleetQuery = ""; 
        DataTable dtinformation = new DataTable();
        strseleetQuery = "select distinct ReportsToID from UserDetail  where ReportsToID in (select UserID from UserInfo where UserID=" + hdnUrsID.Value + ")  ";
        DataTable dt = objQuerycontroller.ExecuteQuery(strseleetQuery);
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            
            {
                if (dt.Rows[0]["ReportsToID"].ToString() == hdnUrsID.Value.ToString())
                {
                    string strjscript = "<script language='javascript' type='text/javascript'>";
                    strjscript += " setLabelText('ctl00_ContentPlaceHolder1_lblMessage','Record is in use cannot deleted..');";
                    strjscript += "</script" + ">";
                    literal1.Text = strjscript;
                    check = 1;
                }
            }
          
        }
        string strcheckQuery = "";
        int status = 1;
        if (check == 0)
        {
            strcheckQuery = "exec USP_CheckPOID " + hdnUrsID.Value + " ";
            DataTable dtStatus = objQuerycontroller.ExecuteQuery(strcheckQuery);
            status = Convert.ToInt16(dtStatus.Rows[0]["Status"]);
            if (status == 0)
            {
                string strjscript = "<script language='javascript' type='text/javascript'>";
                strjscript += " setLabelText('ctl00_ContentPlaceHolder1_lblMessage','Record is in use cannot deleted..');";
                strjscript += "</script" + ">";
                literal1.Text = strjscript;
                check = 1;  
            }
        }

        if (check == 0)
        {
            strQuery = "delete  from UserInfo where UserID=" + hdnUrsID.Value + " delete from Pomaster where UserID=" + hdnUrsID.Value + "";
            strQuery += " delete  from S_DealerDetail where UserID=" + hdnUrsID.Value + " ";
            strQuery += " delete  from UserDetail where UserID=" + hdnUrsID.Value + " ";
            dtinformation = objQuerycontroller.ExecuteQuery(strQuery);
            bindData();
            string strjscript = "<script language='javascript' type='text/javascript'>";
            strjscript += " setLabelText('ctl00_ContentPlaceHolder1_lblMessage','Record Deleted Successfully..');";
            strjscript += "</script" + ">";
            literal1.Text = strjscript;
        }
    }
}