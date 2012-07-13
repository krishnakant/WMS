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

public partial class View_Forms_Configurator_DefectConfigurator : System.Web.UI.Page
{
    QueryConroller objQuerycontroller = new QueryConroller();
    ConfiguratorController objController = new ConfiguratorController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getDefectCodeDetail();
            getDefectGroup();
        }
    }
    public void getDefectCodeDetail()
    {

        string strQuery = "";
        DataTable dtinformation = new DataTable();
        strQuery = "select Code, (Convert(varchar(20),Code)+'('+Description+')')as Code_Description from Defect where IsGroup=0 order by Code";
        dtinformation = objQuerycontroller.ExecuteQuery(strQuery);
        if (dtinformation != null)
        {

            if (dtinformation.Rows.Count > 0)
            {
                chkCodeList.DataSource = dtinformation;
                chkCodeList.DataValueField = "Code";
                chkCodeList.DataTextField = "Code_Description";
                chkCodeList.DataBind();

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
    public void getDefectGroup()
    {

        string strQuery = "";
        DataTable dtinformation = new DataTable();
        strQuery = "select * from DefectGroup order by GroupName";
        dtinformation = objQuerycontroller.ExecuteQuery(strQuery);
        if (dtinformation != null)
        {

            if (dtinformation.Rows.Count > 0)
            {
                ddlGRoupList.DataSource = dtinformation;
                ddlGRoupList.DataValueField = "DefectGroupID";
                ddlGRoupList.DataTextField = "GroupName";
                ddlGRoupList.DataBind();
                ddlGRoupList.AppendDataBoundItems = true;
                ListItem list = new ListItem("Select", "0");
                ddlGRoupList.Items.Insert(0, list);
                ddlGRoupList.AppendDataBoundItems = false;
            }
        }
    }
    public int SaveGroupName()
    {
        int GroupID = 0;
        ConfiguratorUI objUI = new ConfiguratorUI();

        objUI.GroupName = txtGroupName.Text.Trim();
        GroupID = objController.SaveDefectGroup(objUI, null);
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
            GroupID = SaveGroupName();
        }
        if (rdoExixts.Checked)
        {
            GroupID = Convert.ToInt32(ddlGRoupList.SelectedValue);
        }
        if (GroupID > 0)
        {
            ConfiguratorUI objUI = new ConfiguratorUI();
            foreach (ListItem list in chkCodeList.Items)
            {
                if (list.Selected)
                {
                    objUI.source = "Configurator";
                    objUI.Code = list.Value.Trim();
                    objUI.GroupID = GroupID;
                    objController.SaveDefectGroupMapping(objUI, null);
                }
            }
            Response.Redirect("/WMS/View/Forms/Configurator/DefectConfigurator.aspx");
        }
    }
}
