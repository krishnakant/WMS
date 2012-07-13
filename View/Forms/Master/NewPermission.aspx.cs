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

public partial class View_Forms_Master_NewPermission : System.Web.UI.Page
{
    QueryConroller objQuerycontroller = new QueryConroller();
    UserController objController = new UserController();
    UserUI objcUI = new UserUI();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindRole();
            FetchRecord();
            //ShowForm();
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

            }
        }
    }


    public void FetchRecord()
    {
        string strQuery = "";
        string strModule = "";
       
        string strForm = "";

        DataSet ds = new DataSet();

        strModule = "  Select * from Module order by ModulName  ";
       
        strForm = " Select * from FormName order by FormName ";
        strQuery = strModule  + strForm;
        ds = objQuerycontroller.ExecuteQueryWithDataSet(strQuery);
        ChkModule.DataSource = ds.Tables[0];
        ChkModule.DataTextField = "ModulName";
        ChkModule.DataValueField = "ModulID";
        ChkModule.DataBind();
        foreach (ListItem list in ChkModule.Items)
        {
            list.Selected = true;
           
        }

       

        chkForm.DataSource = ds.Tables[1];
        chkForm.DataTextField = "FormName";
        chkForm.DataValueField = "FormID";
        chkForm.DataBind();


      
    }


    public string getModuleIDList()
    {
        int i = 0;
        string strModuleIDList = "";
        foreach (ListItem list in ChkModule.Items)
        {
            if (list.Selected)
            {
                if (i == 0)
                {
                    strModuleIDList = "ModulID=" + list.Value;
                }
                else
                {
                    strModuleIDList = strModuleIDList + " or ModulID=" + list.Value;
                }
                i++;
            }
        }
        return strModuleIDList;
    }


    public void ShowForm()
    {
        string strQuery = "";
        DataTable dt = new DataTable();
        string strModuleIDList = getModuleIDList();
        if (strModuleIDList != "")
        {


            strQuery = "Select * from FormName where " + strModuleIDList + " order by FormName  ";
            dt = objQuerycontroller.ExecuteQuery(strQuery);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    chkForm.DataSource = dt;

                    chkForm.DataTextField = "FormName";
                    chkForm.DataValueField = "FormID";
                    chkForm.DataBind();
                    fillFormList();
                }
            }
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        ShowForm(); 
    }

    public void SavePrivilegeInformation()
    {
        PriviledgeUI objUI = new PriviledgeUI();
        PriviledgeController objController = new PriviledgeController();

        int RoleID = Convert.ToInt32(ddlRole.SelectedValue);

        string strDeleteQuery = "Delete from Priviledges where RoleID=" + RoleID;
        objQuerycontroller.ExecuteQuery(strDeleteQuery);
        int Status = 0;
        try
        {
            foreach (ListItem list in chkForm.Items)
            {

                int FormID = Convert.ToInt32(list.Value);
                bool viewing = false;
                if (list.Selected)
                {
                    viewing = true;
                }
                else
                {
                    viewing = false;
                }

                objUI.FormID = FormID;
                objUI.RoleID = RoleID;
                objUI.viewing = viewing;

                Status = objController.Save(objUI, null);
                if (Status > 0)
                {
                    string strjscript = "<script language='javascript' type='text/javascript'>";
                    strjscript += " setLabelText('ctl00_ContentPlaceHolder1_lblMessage','Record saved successfully' );";
                    strjscript += "</script" + ">";
                    literal1.Text = strjscript;
                }
                else
                {
                    string strjscript = "<script language='javascript' type='text/javascript'>";
                    strjscript += " setLabelText('ctl00_ContentPlaceHolder1_lblMessage','Record could not be saved successfully' );";
                    strjscript += "</script" + ">";
                    literal1.Text = strjscript;
                }
            }
        }
        catch
        {
            string strjscript = "<script language='javascript' type='text/javascript'>";
            strjscript += " setLabelText('ctl00_ContentPlaceHolder1_lblMessage','Record could not be saved successfully' );";
            strjscript += "</script" + ">";
            literal1.Text = strjscript;
        }
        
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SavePrivilegeInformation();
    }
    protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillFormList();
        FillModuleList();
        ShowForm();
    }

    public void fillFormList()
    {
        int RoleID = Convert.ToInt32(ddlRole.SelectedValue);
        string strQuery = "Select * from vw_UserPrivileges where viewing=1 and RoleID=" + RoleID;
        DataTable dt = objQuerycontroller.ExecuteQuery(strQuery);
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                int count=0;                
                foreach (ListItem list in chkForm.Items)
                {
                    list.Selected = false;
                    DataView dv = new DataView(dt);
                    dv.RowFilter = "FormID=" + Convert.ToInt32(list.Value);
                    DataTable dtFilter = dv.ToTable();
                    if (dtFilter != null)
                    {
                        if (dtFilter.Rows.Count > 0)
                        {
                            list.Selected = true;
                            string itemStyle = "color:Red;";
                            chkForm.Items[count].Attributes["Style"] = itemStyle;
                            
                        }
                    }
                    count++;
                }
            }
        }

       
   
    }

    public void FillModuleList()
    {
        int RoleID = Convert.ToInt32(ddlRole.SelectedValue);
        string strModuleQuery = "select distinct ModuleID from Priviledges where RoleID=" + RoleID;
        DataTable dtModule = objQuerycontroller.ExecuteQuery(strModuleQuery);
        if (dtModule != null)
        {
            if (dtModule.Rows.Count > 0)
            {
                int count1 = 0;  
                foreach (ListItem modlist in ChkModule.Items)
                {
                    modlist.Selected = false;
                    DataView dvModule = new DataView(dtModule);
                    dvModule.RowFilter = "ModuleID=" + Convert.ToInt32(modlist.Value);
                    DataTable dtModuleFilter = dvModule.ToTable();
                    if (dtModuleFilter != null)
                    {
                        if (dtModuleFilter.Rows.Count > 0)
                        {
                            modlist.Selected = true;
                            string itemStyle = "color:Red;";
                            ChkModule.Items[count1].Attributes["Style"] = itemStyle;
                        }
                    }
                    count1++;
                }
            }
        }
    }
}
