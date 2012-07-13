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

public partial class View_Forms_Configurator_ItemConfigurator : System.Web.UI.Page
{
    QueryConroller objQuerycontroller = new QueryConroller();
    ConfiguratorController objController = new ConfiguratorController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getItemCodeDetail();
            getItemCodeGroup();
        }
    }
    public void getItemCodeDetail()
    {

        string strQuery = "";
        DataTable dtinformation = new DataTable();
        strQuery = "select Code, (Convert(varchar(20),Code)+'('+Description+')')as Code_Description from Item where IsGroup=0 order by Code";
        dtinformation = objQuerycontroller.ExecuteQuery(strQuery);
        if (dtinformation != null)
        {

            if (dtinformation.Rows.Count > 0)
            {
                chkItemCodeList.DataSource = dtinformation;
                chkItemCodeList.DataValueField = "Code";
                chkItemCodeList.DataTextField = "Code_Description";
                chkItemCodeList.DataBind();

                btnSave.Enabled = true;
            }
            else
            {
                btnSave.Enabled = false;
            }
        }
        else
        {
            btnSave.Enabled = false;
        }
    }
    public void getItemCodeGroup()
    {

        string strQuery = "";
        DataTable dtinformation = new DataTable();
        strQuery = "select * from ItemGRoup order by ItemGRoupName";
        dtinformation = objQuerycontroller.ExecuteQuery(strQuery);
        if (dtinformation != null)
        {

            if (dtinformation.Rows.Count > 0)
            {
                ddlItemGRoupList.DataSource = dtinformation;
                ddlItemGRoupList.DataValueField = "ItemCodeGroupID";
                ddlItemGRoupList.DataTextField = "ItemGRoupName";
                ddlItemGRoupList.DataBind();
                ddlItemGRoupList.AppendDataBoundItems = true;
                ListItem list = new ListItem("Select", "0");
                ddlItemGRoupList.Items.Insert(0, list);
                ddlItemGRoupList.AppendDataBoundItems = false;
            }
        }
    }
    public int SaveItemGroupName()
    {
        int GroupID = 0;
        ConfiguratorUI objUI = new ConfiguratorUI();
       
        objUI.GroupName = txtGroupName.Text.Trim();
        GroupID=objController.SaveGroup(objUI, null);
        if (GroupID == 0)
        {
            string strjscript = "<script language='javascript' type='text/javascript'>";
            strjscript += " setLabelText('ctl00_ContentPlaceHolder1_lblMessage','group already exists' );";
            strjscript += "</script" + ">";
            literal1.Text = strjscript;
        }
        else
        {
            string strjscript = "<script language='javascript' type='text/javascript'>";
            strjscript += " setLabelText('ctl00_ContentPlaceHolder1_lblMessage','' );";
            strjscript += "</script" + ">";
            literal1.Text = strjscript;
        }
        return GroupID;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int GroupID = 0;
        if (rdoNew.Checked)
        {
            GroupID=SaveItemGroupName();
        }
        if (rdoExixts.Checked)
        {
            GroupID = Convert.ToInt32(ddlItemGRoupList.SelectedValue);
        }
        if (GroupID > 0)
        {
            ConfiguratorUI objUI = new ConfiguratorUI();
            foreach (ListItem list in chkItemCodeList.Items)
            {
                if (list.Selected)
                {
                    objUI.source = "Configurator";
                    objUI.Code = list.Value.Trim();
                    objUI.GroupID = GroupID;
                    objController.SaveItemGroupMapping(objUI, null);
                }
            }
            Response.Redirect("/WMS/View/Forms/Configurator/ItemConfigurator.aspx");
        }
    }
}
