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

public partial class View_Forms_Configurator_ShowCulpritCodeByGroup : System.Web.UI.Page
{
    QueryConroller objQuerycontroller = new QueryConroller();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getCulpritGroup();
        }
    }
    public void getCulpritGroup()
    {

        string strQuery = "";
        DataTable dtinformation = new DataTable();
        strQuery = "select * from CulpritGroup  order by GRoupName";
        dtinformation = objQuerycontroller.ExecuteQuery(strQuery);
        if (dtinformation != null)
        {

            if (dtinformation.Rows.Count > 0)
            {
                ddlGRoupList.DataSource = dtinformation;
                ddlGRoupList.DataValueField = "CulpritGroupID";
                ddlGRoupList.DataTextField = "GroupName";
                ddlGRoupList.DataBind();
                ddlGRoupList.AppendDataBoundItems = true;
                ListItem list = new ListItem("Select", "0");
                ddlGRoupList.Items.Insert(0, list);
                ddlGRoupList.AppendDataBoundItems = false;

            }
        }
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {

        getCodeByGroup(Convert.ToInt32(ddlGRoupList.SelectedValue));
    }
    public void getCodeByGroup(int GroupID)
    {
        string strQuery = "";
        DataTable dtinformation = new DataTable();
        
        strQuery = "select Code, (Convert(varchar(20),Code)+'('+Description+')')as Code_Description  from vw_CulpritWholeDetail where GroupID=" + GroupID + " order by Code ";
        dtinformation = objQuerycontroller.ExecuteQuery(strQuery);
        if (dtinformation != null)
        {

            if (dtinformation.Rows.Count > 0)
            {
                chkCodeList.DataSource = dtinformation;
                chkCodeList.DataValueField = "Code";
                chkCodeList.DataTextField = "Code_Description";

                chkCodeList.DataBind();
                foreach (ListItem list in chkCodeList.Items)
                {
                    list.Selected = true;
                }
                btnSave.Enabled = true;
                string strjscript = "<script language='javascript' type='text/javascript'>";
                strjscript += " setLabelText('ctl00_ContentPlaceHolder1_lblMessage','' );";
                strjscript += "</script" + ">";
                literal1.Text = strjscript;
            }
            else
            {
                chkCodeList.DataSource = dtinformation;
                chkCodeList.DataBind();
                btnSave.Enabled = false;
                string strjscript = "<script language='javascript' type='text/javascript'>";
                strjscript += " setLabelText('ctl00_ContentPlaceHolder1_lblMessage','Code not present' );";
                strjscript += "</script" + ">";
                literal1.Text = strjscript;
            }
        }
        else
        {
            chkCodeList.DataSource = dtinformation;
            chkCodeList.DataBind();
            btnSave.Enabled = false;
            string strjscript = "<script language='javascript' type='text/javascript'>";
            strjscript += " setLabelText('ctl00_ContentPlaceHolder1_lblMessage','Code not present' );";
            strjscript += "</script" + ">";
            literal1.Text = strjscript;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string strjscript = "<script language='javascript' type='text/javascript'>";
        strjscript += " setLabelText('ctl00_ContentPlaceHolder1_lblMessage','' );";
        strjscript += "</script" + ">";
        literal1.Text = strjscript;
        int Status = 0;
        foreach (ListItem list in chkCodeList.Items)
        {
            if (!list.Selected)
            {
                UpdateItemGroupDetail(list.Value);
                Status = 1;
            }
        }
        if (Status == 1)
        {
            getCodeByGroup(Convert.ToInt32(ddlGRoupList.SelectedValue));
        }
    }
    public void UpdateItemGroupDetail(string Code)
    {
        string strQuery = "";
        strQuery = "execute usp_UpdateCulpritGroupMappingDetail " + Code + "";
        objQuerycontroller.ExecuteQuery(strQuery);
    }
}
