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

public partial class View_Forms_Configurator_ShowProductCodebyGroup : System.Web.UI.Page
{
    QueryConroller objQuerycontroller = new QueryConroller();
    ConfiguratorUI objcUI = new ConfiguratorUI();
    ConfiguratorController objController = new ConfiguratorController();

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            BindModel();
        }

    }
    public void BindModel()
    {
        string strQuery = "";
        DataTable dtModel = new DataTable();
        strQuery = "select * from ModelGroupName  order by ModelGroupName";
        dtModel = objQuerycontroller.ExecuteQuery(strQuery);
        if (dtModel != null)
        {
            if (dtModel.Rows.Count > 0)
            {
                drpModel.DataSource = dtModel;

                drpModel.DataTextField = "ModelGroupName";
                drpModel.DataValueField = "GroupID";
                drpModel.DataBind();
                drpModel.AppendDataBoundItems = true;
                ListItem list = new ListItem("Select", "0");
                drpModel.Items.Insert(0, list);
                drpModel.AppendDataBoundItems = false;

            }
        }
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        getProductCode();
    }
    public void getProductCode()
    {
        string strQuery = "";
        DataTable dtProductcode = new DataTable();
        strQuery = "select ProductCodeID, ProductCode,(Convert(varchar(20),ProductCode)+'('+isnull( Description,'NA')+')')as Code_Description from vw_wholeModelDetail where GroupID='" + drpModel.SelectedValue + "'";
        dtProductcode = objQuerycontroller.ExecuteQuery(strQuery);
        if (dtProductcode != null)
        {
            if (dtProductcode.Rows.Count > 0)
            {

                chkProductCode.DataSource = dtProductcode;
                chkProductCode.DataTextField = "Code_Description";
                chkProductCode.DataValueField = "ProductCodeID";
                chkProductCode.DataBind();
                btnSave.Enabled =true;

                foreach (ListItem list in chkProductCode.Items)
                {
                    list.Selected = true;

                }
                string strjscript = "<script language='javascript' type='text/javascript'>";
                strjscript += " setLabelText('ctl00_ContentPlaceHolder1_lblMessage','' );";
                strjscript += "</script" + ">";
                literal1.Text = strjscript;
            }
            else
            {
                btnSave.Enabled =false;
                chkProductCode.DataSource = dtProductcode;
               
                chkProductCode.DataBind();
                string strjscript = "<script language='javascript' type='text/javascript'>";
                strjscript += " setLabelText('ctl00_ContentPlaceHolder1_lblMessage','Code not present' );";
                strjscript += "</script" + ">";
                literal1.Text = strjscript;
            }

        }
        else
        {
            btnSave.Enabled =false;
            chkProductCode.DataSource = dtProductcode;
           
            chkProductCode.DataBind();
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
        foreach (ListItem list in chkProductCode.Items)
        {
            if (!list.Selected)
            {
                SaveProductCode(list.Value);
                Status = 1;
            }

        }
        if (Status == 1)
        {
            getProductCode();
        }
        
       
    }
    public void SaveProductCode(string productCodeID)
    {

        string strQuery = "";
        DataTable dtProductCode = new DataTable();
        strQuery = "execute Usp_UpdateProductCode " + productCodeID + "";
        dtProductCode = objQuerycontroller.ExecuteQuery(strQuery);
       
        
    }

}