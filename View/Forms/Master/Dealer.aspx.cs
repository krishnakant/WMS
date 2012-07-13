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

public partial class View_Forms_Master_Dealer : System.Web.UI.Page
{

    QueryConroller objQuerycontroller = new QueryConroller();
    DealerController objController = new DealerController();
    DealerUI objUI = new DealerUI();

    public string strProjectName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        strProjectName = System.Configuration.ConfigurationManager.AppSettings["WMSProjectName"];
        if (!IsPostBack)
        {
            ChangelblName();
            BindRegion();
            if (Request.QueryString["DealerID"] != null)
            {
                Session.Add("UpadateRegionId", Request.QueryString["RegionID"].ToString());
                Session.Add("UpadatesearchCode", Request.QueryString["searchCode"].ToString());

                getDetail(Request.QueryString["DealerID"].ToString());
            }
            if (Request.QueryString["source"] != null)
            {
                if (Request.QueryString["Code"] != null)
                {
                    string strCode = Request.QueryString["Code"].ToString();
                    txtCode.Text = strCode;

                }
            }
        }
    }


    public void BindRegion()
    {
        DataTable dtRegion = new DataTable();
        string strQuery = "Select RegionID,Region from Region order by Region";
        dtRegion = objQuerycontroller.ExecuteQuery(strQuery);
        if (dtRegion != null)
        {
            if (dtRegion.Rows.Count > 0)
            {
                drpRegion.DataSource = dtRegion;
                drpRegion.DataTextField = "Region";
                drpRegion.DataValueField = "RegionID";
                drpRegion.DataBind();
                drpRegion.Visible = true;

                drpRegion.AppendDataBoundItems = true;
                ListItem list1 = new ListItem("Select", "0");
                drpRegion.Items.Insert(0, list1);
                drpRegion.AppendDataBoundItems = false;
            }
        }
    }


    public void ChangelblName()
    {
        DataTable dtControls = new DataTable();
        string strQuery = "Select * from Label_Data where Form='Dealer'";
        dtControls = objQuerycontroller.ExecuteQuery(strQuery);
        foreach (DataRow dr in dtControls.Rows)
        {
            string strLabel = dr["Label_ID"].ToString();
            string strLabelText = dr["Text"].ToString();

            if (strLabel == "lblDealer")
            {
                lblDealer.Text = strLabelText;
            }
            else if (strLabel == "lblCode")
            {
                lblCode.Text = strLabelText;
            }
            
            else if (strLabel == "lblActive")
            {
                lblActive.Text = strLabelText;
            }
           

        }


    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveDealer();
    }
    public int SaveDealer()
    {
        int DealerID = 0;
        DataTable dtControls = new DataTable();
        if (hdnCode.Value.Trim().ToLower() != txtCode.Text.Trim().ToLower())
        {

            string strQuery = "Select * from Dealer where Code='" + txtCode.Text.Trim()+ "'";
            dtControls = objQuerycontroller.ExecuteQuery(strQuery);
        }
        if (dtControls == null || dtControls.Rows.Count == 0)
        {
            objUI.Code = txtCode.Text.Trim();
            objUI.Dealer = txtDealer.Text;
            objUI.RegionID = Convert.ToInt16(drpRegion.SelectedValue.ToString());
            objUI.IsActive = chkActive.Checked;
            objUI.IsOperatingDealer= chkIsOperating.Checked;
            objUI.Id = Convert.ToInt32(hdnDealerID.Value);
            objUI.CheckID = Convert.ToInt32(hdnCheckID.Value);
            objUI.City = txtLocation.Text;
            objUI.InstallerName = txtInstaller.Text.Trim();
            DealerID = objController.SaveDealer(objUI, null);
            if (DealerID == 0)
            {
                string strjscript = "<script language='javascript' type='text/javascript'>";
                strjscript += " setLabelText('ctl00_ContentPlaceHolder1_lblMessage','Code already exists' );";
                strjscript += "</script" + ">";
                literal1.Text = strjscript;
            }
            else
            {
                Response.Redirect(strProjectName+"/View/Forms/Master/DealerDefault.aspx");
            }
        }
        else
        {
            string strjscript = "<script language='javascript' type='text/javascript'>";
            strjscript += " setLabelText('ctl00_ContentPlaceHolder1_lblMessage','Code already exists' );";
            strjscript += "</script" + ">";
            literal1.Text = strjscript;
        }

        return DealerID;
    }
    public void getDetail(string DealerId)
    {
       
        string strQuery = "";
        hdnDealerID.Value = DealerId;
        hdnCheckID.Value = "1";
        DataTable dtinformation = new DataTable();
        btnSave.ToolTip = "Update";
        btnSave.Text = "Update";

        // strQuery = "select * from Location where IsActive=1";
        strQuery = "select * from Dealer where ID=" + DealerId + " ";
        dtinformation = objQuerycontroller.ExecuteQuery(strQuery);
        if (dtinformation != null)
        {


            if (dtinformation.Rows.Count > 0)
            {
                foreach (DataRow drinformation in dtinformation.Rows)
                {
                    txtCode.Text = drinformation["Code"].ToString();
                    txtDealer.Text = drinformation["Dealer"].ToString();
                    hdnCode.Value = drinformation["Code"].ToString();
                    drpRegion.SelectedValue = drinformation["RegionID"].ToString();
                   txtLocation.Text= drinformation["City"].ToString();
                   txtInstaller.Text = drinformation["InstallerName"].ToString();
                    if (Convert.ToBoolean(drinformation["IsActive"]) == true)
                    {
                        chkActive.Checked = true;
                    }
                    else
                    {
                        chkActive.Checked = false;
                    }

                    if (Convert.ToBoolean(drinformation["IsOperatingDealer"]) == true)
                    {
                        chkIsOperating.Checked = true;
                    }
                    else
                    {
                        chkIsOperating.Checked = false;
                    }
                }
            }
        }
       
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["source"] != null)
        {
            if (Request.QueryString["source"].ToString() == "Exception")
            {
                Response.Redirect(strProjectName + "/View/Forms/Exceptions/DealerException.aspx");
            }
        }
        else
        {
            Response.Redirect(strProjectName + "/View/Forms/Master/DealerDefault.aspx");
        }
    }
}
