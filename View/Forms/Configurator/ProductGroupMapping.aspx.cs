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

public partial class View_Forms_Configurator_ProductGroupMapping : System.Web.UI.Page
{
    QueryConroller objQuerycontroller = new QueryConroller();
    ConfiguratorUI objcUI = new ConfiguratorUI();
    ConfiguratorController objController = new ConfiguratorController();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            BindProductCode();
            BindModel();
        }
       
    }

    public void BindProductCode()
    {
        string strQuery = "";
        DataTable dtProductCode = new DataTable();
        // strQuery = "select * from Location where IsActive=1";
        strQuery = "select ProductCodeID,(Convert(varchar(20),ProductCode)+'('+isnull( Description,'NA')+')')as Code_Description from ProductCode  where IsGroup=0 ";
        dtProductCode = objQuerycontroller.ExecuteQuery(strQuery);
        if (dtProductCode != null)
        {
            if (dtProductCode.Rows.Count > 0)
            {
                chkProductCode.DataSource = dtProductCode;

                chkProductCode.DataTextField = "Code_Description";
                chkProductCode.DataValueField = "ProductCodeID";
                chkProductCode.DataBind();
              

            }
        }
    }

    public void BindModel()
    {
        string strQuery = "";
        DataTable dtModel = new DataTable();
        // strQuery = "select * from Location where IsActive=1";
        strQuery = "select * from ModelGroupName ";
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
                //ListItem list = new ListItem("Select Location", "0");
                ListItem list = new ListItem("Select", "0");
                drpModel.Items.Insert(0, list);
                drpModel.AppendDataBoundItems = false;

            }
        }
    }
    public int saveGroupName()
    {

        int GroupID = 0;
        objcUI.ModelGroupName = txtGroupName.Text;


        GroupID = objController.Save(objcUI, null);
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
            GroupID=saveGroupName();
        }
        if (rdoExisting.Checked)
        {
            GroupID = Convert.ToInt32(drpModel.SelectedValue);
        }
        if (GroupID > 0)
        {
            ConfiguratorUI objUI = new ConfiguratorUI();
            foreach (ListItem list in chkProductCode.Items)
            {
                if (list.Selected)
                {
                    objUI.ID =Convert.ToInt32(list.Value);
                    objUI.GroupID = GroupID;
                    objUI.source = "Exception";
                    objController.SaveProductGroupMapping(objUI, null);
                }
            }
            Response.Redirect("/WMS/View/Forms/Configurator/ProductGroupMapping.aspx");
        }
    }
               
    }

