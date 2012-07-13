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

public partial class View_Forms_Configurator_ShowItemCodeByGroupName : System.Web.UI.Page
{
    QueryConroller objQuerycontroller = new QueryConroller();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getItemCodeGroup();
        }
    }
    public void getItemCodeGroup()
    {

        string strQuery = "";
        DataTable dtinformation = new DataTable();
        strQuery = "select * from ItemGRoup  order by ItemGRoupName";
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
    protected void btnGo_Click(object sender, EventArgs e)
    {

        getItemByGroup(Convert.ToInt32(ddlItemGRoupList.SelectedValue));
    }
    public void getItemByGroup(int GroupID)
    {
        string strQuery = "";
        DataTable dtinformation = new DataTable();
        strQuery = "select ItemCode, (Convert(varchar(20),Code)+'('+Description+')')as Code_Description from  vw_wholeItemDetail where GroupID=" + GroupID + " order by ItemCode ";
        dtinformation = objQuerycontroller.ExecuteQuery(strQuery);
        if (dtinformation != null)
        {

            if (dtinformation.Rows.Count > 0)
            {
                chkItemCodeList.DataSource = dtinformation;
                chkItemCodeList.DataValueField = "ItemCode";
                chkItemCodeList.DataTextField = "Code_Description";

                chkItemCodeList.DataBind();
                foreach (ListItem list in chkItemCodeList.Items)
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
                chkItemCodeList.DataSource = dtinformation;
                chkItemCodeList.DataBind();
                btnSave.Enabled = false;
                string strjscript = "<script language='javascript' type='text/javascript'>";
                strjscript += " setLabelText('ctl00_ContentPlaceHolder1_lblMessage','Code not present' );";
                strjscript += "</script" + ">";
                literal1.Text = strjscript;
            }
        }
        else
        {
            chkItemCodeList.DataSource = dtinformation;
            chkItemCodeList.DataBind();
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
        foreach (ListItem list in chkItemCodeList.Items)
        {
            if (!list.Selected)
            {
                UpdateItemGroupDetail(list.Value);
                Status = 1;
            }
        }
        if (Status == 1)
        {
            getItemByGroup(Convert.ToInt32(ddlItemGRoupList.SelectedValue));
        }
    }
    public void UpdateItemGroupDetail(string ItemCode)
    {
        string strQuery = "";
        strQuery = "execute usp_UpdateItemGroupMappingDetail " + ItemCode + "";
        objQuerycontroller.ExecuteQuery(strQuery);
    }
}
