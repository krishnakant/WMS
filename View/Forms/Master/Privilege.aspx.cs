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
public partial class View_Forms_Master_Privilege : System.Web.UI.Page
{
    QueryConroller objQuerycontroller = new QueryConroller();
    PriviledgeController objPrivilegeController = new PriviledgeController();
    PriviledgeUI objPrivilegeUI = new PriviledgeUI();
    public string strProjectName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        strProjectName = System.Configuration.ConfigurationManager.AppSettings["WMSProjectName"];  
        if (!IsPostBack)
        {

            BindRole();
           
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

    public void BindRole()
    {
        string strQuery = "";
        DataTable dtRole = new DataTable();
        strQuery = "select * from Role where IsActive=1 order by Role ";
        dtRole = objQuerycontroller.ExecuteQuery(strQuery);
        if (dtRole != null)
        {
            if (dtRole.Rows.Count > 0)
            {
                ddlRole.DataSource = dtRole;

                ddlRole.DataTextField = "Role";
                ddlRole.DataValueField = "RoleID";
                ddlRole.DataBind();
                ddlRole.AppendDataBoundItems = true;
                ListItem list = new ListItem("Select", "0");
                ddlRole.Items.Insert(0, list);
                ddlRole.AppendDataBoundItems = false;
                //bindData();
            }
        }
    }
   public  string selectValue = "";
    public void bindData()
    {
        int RoleID = Convert.ToInt32(ddlRole.SelectedValue);
        string strQuery = "";
        DataTable dtinformation = new DataTable();

        strQuery = "execute usp_PriviledgesDetail " + RoleID.ToString() + "";
        dtinformation = objQuerycontroller.ExecuteQuery(strQuery);
        if (dtinformation != null)
        {
            if (dtinformation.Rows.Count > 0)
            {
                gdPrivilege.DataSource = dtinformation;
                gdPrivilege.DataBind();
                selectValue = gdPrivilege.Rows.Count.ToString();

            }
            else
            {
                gdPrivilege.DataSource = null;
                gdPrivilege.DataBind();
            }
        }
        else
        {
            gdPrivilege.DataSource = null;
            gdPrivilege.DataBind();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        SavePriviledge();
        
    }
    public int SavePriviledge()
    { 
       int PriviledgeID =0;

        for (int counter = 0; counter <= gdPrivilege.Rows.Count - 1; counter++)
        {
            HiddenField hdnFormID = (HiddenField)gdPrivilege.Rows[counter].FindControl("hdnFormID");
            int FormID = Convert.ToInt16(hdnFormID.Value);
            CheckBox chkviewing = (CheckBox)gdPrivilege.Rows[counter].FindControl("chkviewing");
            bool viewing = chkviewing.Checked;
            objPrivilegeUI.FormID = Convert.ToInt16(hdnFormID.Value);
            objPrivilegeUI.RoleID = Convert.ToInt16(ddlRole.SelectedValue);
            objPrivilegeUI.viewing = viewing;
            PriviledgeID = objPrivilegeController.Save(objPrivilegeUI, null);
        }

        GenerateMenuFile(ddlRole.SelectedValue);
            //
            ////if (objPrivilegeUI.Success == 1)
            ////{
            ////    string strjscript = "<script language='javascript'><b>";
            ////    strjscript += " setLabelText('ctl00_cphWWT_lblMessage','Privileges for Role Saved Successfully!' );";
            ////    strjscript += "</b></script" + ">";
            ////    Literal1.Text = strjscript;
            ////}
        
        //GenerateMenuFile(ddlRole.SelectedValue);
        return PriviledgeID;

    }

    public void eventhandlerSerialNo(object Sender, EventArgs E)
    {
        int i = 1;
        int pi = gdPrivilege.PageIndex;
        int ps = gdPrivilege.PageSize;
        //<><> Use Name of Your GridView Instead Of gvDetailProspect <><>// 
        if (pi > 0)
        {
            i = pi * ps + 1;
        }
        foreach (GridViewRow gr in gdPrivilege.Rows)
        {
            gr.Cells[0].Text = i.ToString();

            i++;
        }
    }

    private void GenerateMenuFile(string RoleID)
    {

        string JSPath = Server.MapPath("~");
        JSPath = JSPath + "/JS/" + ddlRole.SelectedItem.Text.Trim();
        JSPath = JSPath + ".js";
        FileStream fs = File.Create(JSPath);
        fs.Close();
        TextWriter tw = new StreamWriter(JSPath);
        tw.WriteLine("var menuHTML=\"<ul id='navmenu'><li>\"+");
        string varFirstModuleNameOld = "";
        string varFirstModuleNameNew = "";
        string strQuery;
        strQuery = "select * from vw_FmgPriviledges where RoleID=" + RoleID.ToString() + " and viewing=1 order by FirstModuleOrder";
        DataTable dtMenuGenerationTable = objQuerycontroller.ExecuteQuery(strQuery);
        foreach (DataRow dr in dtMenuGenerationTable.Rows)
        {
            varFirstModuleNameOld = dr["FirstModule"].ToString().Trim();
            if (varFirstModuleNameNew != varFirstModuleNameOld)
            {
                tw.WriteLine("\"<a href='#'>" + dr["FirstModule"].ToString().Trim() + "</a><ul><li>\"+");
                string varSecondModuleNameOld = "";
                string varSecondModuleNameNew = "";
                DataRow[] foundRows;
                foundRows = dtMenuGenerationTable.Select("FirstModule = '" + varFirstModuleNameOld + "'");
                for (int i = 0; i <= foundRows.Length - 1; i++)
                {
                    varSecondModuleNameOld = foundRows[i].ItemArray[1].ToString().Trim();
                    if (varSecondModuleNameOld != varSecondModuleNameNew)
                    {
                        tw.WriteLine("\"<a href='#'>" + varSecondModuleNameOld + "->" + "</a><ul>\"+");
                        string varThirdModuleNameOld = "";
                        string varThirdModuleNameNew = "";
                        DataRow[] foundRows1;
                        foundRows1 = dtMenuGenerationTable.Select("SecondModule = '" + varSecondModuleNameOld + "' And FirstModule = '" + varFirstModuleNameOld + "'");
                        for (int j = 0; j <= foundRows1.Length - 1; j++)
                        {
                            varThirdModuleNameOld = foundRows1[j].ItemArray[2].ToString().Trim();
                            if (varThirdModuleNameOld != varThirdModuleNameNew)
                            {
                                tw.WriteLine("\"<li><a href='" + foundRows1[j].ItemArray[3].ToString().Trim() + "'>" + foundRows1[j].ItemArray[2].ToString().Trim() + "</a></li>\"+");
                            }
                            varThirdModuleNameNew = varThirdModuleNameOld;
                        }
                        tw.WriteLine("\"</ul>\"+");
                        tw.WriteLine("\"</li><li>\"+");
                    }
                    varSecondModuleNameNew = varSecondModuleNameOld;
                }
                tw.WriteLine("\"</li></ul></li><li>\"+");
            }

            varFirstModuleNameNew = varFirstModuleNameOld;
        }
        tw.WriteLine("\"</li></ul>\";");
        tw.WriteLine("document.write(menuHTML);");
        tw.Close();
    } 

    protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindData();
    }
}