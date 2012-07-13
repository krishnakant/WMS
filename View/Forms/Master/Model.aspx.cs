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

public partial class View_Forms_Master_Model : System.Web.UI.Page
{
    DateFormat objDate = new DateFormat();
    QueryConroller objQueryController = new QueryConroller();
    public string strProjectName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        strProjectName = System.Configuration.ConfigurationManager.AppSettings["WMSProjectName"];
        if (!IsPostBack)
        {
            ChangelblName();
            getModelGroup();
         if (Request.QueryString["Code"] != null)
            {
                FillModelDetail(Request.QueryString["Code"].ToString(), Request.QueryString["GroupID"].ToString());
            }
            else
            {
                CalstartDate.Value =objDate.ConvertDateFormat(System.DateTime.Now.ToShortDateString());
            }
        }
        Page.RegisterStartupScript("OnBlock", "<script>getGroup();</script>");
          
        
    }

    public void ChangelblName()
    {
        DataTable dtControls = new DataTable();
        string strQuery = "Select * from Label_Data where Form='Model'";
        dtControls = objQueryController.ExecuteQuery(strQuery);
        foreach (DataRow dr in dtControls.Rows)
        {
            string strLabel = dr["Label_ID"].ToString();
            string strLabelText = dr["Text"].ToString();

            if (strLabel == "lblCode")
            {
               lblCode.Text = strLabelText;
            }
            else if (strLabel == "lblModelCode")
            {
                lblModelCode.Text = strLabelText;
            }
            else if (strLabel == "lblDesc")
            {
                lblDesc.Text = strLabelText;
            }
            else if (strLabel == "lblActive")
            {
                lblActive.Text = strLabelText;
            }
            else if (strLabel == "lblReport")
            {
                lblReport.Text = strLabelText;
            }
            else if (strLabel == "lbldate")
            {
                lbldate.Text = strLabelText;
            }


        }


    }

    public void getModelGroup()
    {
        DataTable dtModel = new DataTable();
        string strQuery = "Select GroupID,ModelGroupName from ModelGroupName";
        dtModel = objQueryController.ExecuteQuery(strQuery);
        if (dtModel != null)
        {
            if (dtModel.Rows.Count > 0)
            {
                drpModelCode.DataSource = dtModel;
                drpModelCode.DataTextField = "ModelGroupName";
                drpModelCode.DataValueField = "GroupID";
                drpModelCode.DataBind();
                drpModelCode.Visible = true;

                drpModelCode.AppendDataBoundItems = true;
                ListItem list1 = new ListItem("Select", "0");
                drpModelCode.Items.Insert(0, list1);
                drpModelCode.AppendDataBoundItems = false;
            }
        }
    }

   

    public int SaveModelGroupName()
    {
        int GroupID = 0;
        ConfiguratorUI objUI = new ConfiguratorUI();
        ConfiguratorController objController = new ConfiguratorController();
        objUI.ModelGroupName = txtModelCode.Text.Trim();
        GroupID = objController.Save(objUI, null);
        if (GroupID == 0)
        {
            string strjscript = "<script language='javascript' type='text/javascript'>";
            strjscript += " setMessageText('ctl00_ContentPlaceHolder1_lblMessage','Group already exists' );";
            strjscript += "</script" + ">";
            Literal1.Text = strjscript;
        }
        else
        {
            string strjscript = "<script language='javascript' type='text/javascript'>";
            strjscript += " setLabelText('ctl00_ContentPlaceHolder1_lblMessage','' );";
            strjscript += "</script" + ">";
            Literal1.Text = strjscript;
        }
        return GroupID;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ModelUI objUI = new ModelUI();
        MastersController objController = new MastersController();
        string lblMsg = "";
        int Code = Convert.ToInt16(txtCode.Text.Trim());
        int GroupID = 0;
        string strModelCode = "";
        if (rdoAdd.Checked)
        {
            //strModelCode = txtModelCode.Text.Trim();
            GroupID = SaveModelGroupName();
        }
        if (rdoAssign.Checked)
        {
            if (drpModelCode.SelectedIndex != 0)
            {
               // strModelCode = drpModelCode.SelectedItem.Text;
                GroupID = Convert.ToInt32(drpModelCode.SelectedValue);
            }
        }
        if (GroupID > 0)
        {
            string strDescription = txtDesc.Text.Trim();
            int IsActive = 0;
            if (chkActive.Checked)
            {
                IsActive = 1;
            }
            else
            {
                IsActive = 0;
            }

            int InReport = 0;
            if (chkReport.Checked)
            {
                InReport = 1;
            }
     
            objUI.Code = Code;
            objUI.GroupID = GroupID;
            objUI.Description = strDescription;
            objUI.IsActive = IsActive;
            objUI.InReport = InReport;
            objUI.IsGroup = 1;


            string Date = objDate.ConvertDateFormat(CalstartDate.Value);
            if (Date == "")
            {
                objUI.EffectDate = Convert.ToDateTime("1/1/1900");
            }
            else
            {
                objUI.EffectDate = Convert.ToDateTime(Date);
            }
            objUI.StatusID = Convert.ToInt32(hdnStatusID.Value);

            try
            {
                objController.AddProduct(objUI, null);
                Response.Redirect(strProjectName+"/View/Forms/Master/ModelDefault.aspx");
            }
            catch (Exception ex)
            {
                string strMessage = ex.Message;
                if (strMessage.Contains("Cannot insert duplicate"))
                {
                    lblMsg = "Cannot insert duplicate value, Please check the code";
                    string strjscript = "<script language='javascript'>";
                    strjscript += " setMessageText('ctl00_ContentPlaceHolder1_lblMessage','" + lblMsg + "' );";
                    strjscript += "</script" + ">";
                    Literal1.Text = strjscript;
                    //lblMessage.Text = "Cannot insert duplicate value, Please check the code";
                }
                else
                {
                    lblMsg = "Record could not be added successfully";
                    string strjscript = "<script language='javascript'>";
                    strjscript += " setMessageText('ctl00_ContentPlaceHolder1_lblMessage','" + lblMsg + "' );";
                    strjscript += "</script" + ">";
                    Literal1.Text = strjscript;
                    //lblMessage.Text = "Record could not be added successfully";
                }
            }
        }

    }

    public string getDate(string strCalDate)
    {
        string[] strDateTime = strCalDate.Split(' ');
        string[] strDate = strDateTime[0].Split('/');
        if (strDate[0].Length == 1)
        {
            strDate[0] = "0" + strDate[0];
        }
        if (strDate[1].Length == 1)
        {
            strDate[1] = "0" + strDate[1];
        }

        string varDate = strDate[1]+"."+strDate[0]+"."+strDate[2];
        return varDate;
    }

    public void FillModelDetail(string Code, string GroupID)
    {
        hdnStatusID.Value = "1";
        btnAdd.Text = "Update";
        btnAdd.ToolTip = "Update";
        txtCode.Enabled = false;
        string strQuery = "";
        DataTable dtinformation = new DataTable();
        strQuery = "select * from ProductCode where ProductCode='" + Code + "'";
        dtinformation = objQueryController.ExecuteQuery(strQuery);
        if (dtinformation != null)
        {
            if (dtinformation.Rows.Count > 0)
            {
                txtCode.Text = dtinformation.Rows[0]["ProductCode"].ToString();
               txtDesc.Text = dtinformation.Rows[0]["Description"].ToString();
                if (dtinformation.Rows[0]["IsActive"].ToString() != "")
                {
                    if (dtinformation.Rows[0]["IsActive"].ToString() == "True")
                    {
                        chkActive.Checked = true;
                    }
                    else
                    {
                        chkActive.Checked = false;
                    }

                }
                else
                {
                    chkActive.Checked = false;
                }
                if (dtinformation.Rows[0]["InReport"].ToString() != "")
                {
                    if (dtinformation.Rows[0]["InReport"].ToString() == "True")
                    {
                        chkReport.Checked = true;
                    }
                    else
                    {
                        chkReport.Checked = false;
                    }

                }
                else
                {
                    chkReport.Checked = false;
                }

                string strCalstartDate = dtinformation.Rows[0]["EffectDate"].ToString();
              strCalstartDate = objDate.ConvertDateFormat(strCalstartDate);
                if (strCalstartDate != "")
                {
                    if (strCalstartDate == "1/1/1900 12:00:00 AM")
                    {
                        CalstartDate.Value = "";
                    }
                    else
                    {
                        string[] strDateArray = strCalstartDate.Split(' ');
                        CalstartDate.Value = strDateArray[0].ToString();
                    }
                }
           
                if (dtinformation.Rows[0]["IsGroup"].ToString() == "1")
                {
                    rdoAdd.Checked = false;
                    rdoAssign.Checked = true;
                   drpModelCode.SelectedValue = GroupID;

                }

            }
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(strProjectName+"/View/Forms/Master/ModelDefault.aspx");
    }
}

