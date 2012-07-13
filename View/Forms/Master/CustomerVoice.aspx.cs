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

public partial class View_Forms_Master_CustomerVoice : System.Web.UI.Page
{
    DateFormat objDate = new DateFormat();
    QueryConroller objQueryController = new QueryConroller();
    public string strProjectName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        strProjectName = System.Configuration.ConfigurationManager.AppSettings["WMSProjectName"];
       
        //if (Session["ID"] == null)
        //{
        //    Response.Redirect("/SHQ/Login.aspx");
        //}
        if (!IsPostBack)
        {

            ChangelblName();
            getCVoiceGroup();
           
             if (Request.QueryString["Code"] != null)
            {
                FillCustomerVoiceDetail(Request.QueryString["Code"].ToString(), Request.QueryString["GroupID"].ToString());
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
        string strQuery = "Select * from Label_Data where Form='CustomerVoice'";
        dtControls = objQueryController.ExecuteQuery(strQuery);
        foreach (DataRow dr in dtControls.Rows)
        {
            string strLabel = dr["Label_ID"].ToString();
            string strLabelText = dr["Text"].ToString();

            if (strLabel == "lblCVoiceCode")
            {
                lblCVoiceCode.Text = strLabelText;
            }
            else if (strLabel == "lblCVoiceGroup")
            {
                lblCVoiceGroup.Text = strLabelText;
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
    public void getCVoiceGroup()
    {

        string strQuery = "";
        DataTable dtinformation = new DataTable();
        strQuery = "select * from CVoiceGroup order by GRoupName";
        dtinformation = objQueryController.ExecuteQuery(strQuery);
        if (dtinformation != null)
        {

            if (dtinformation.Rows.Count > 0)
            {
                drpCVoiceGroup.DataSource = dtinformation;
                drpCVoiceGroup.DataValueField = "CVoiceGroupID";
                drpCVoiceGroup.DataTextField = "GroupName";
                drpCVoiceGroup.DataBind();
                drpCVoiceGroup.AppendDataBoundItems = true;
                ListItem list = new ListItem("Select", "0");
                drpCVoiceGroup.Items.Insert(0, list);
                drpCVoiceGroup.AppendDataBoundItems = false;
            }
        }
    }

    public int SaveCVoiceGroup()
    {
        int GroupID = 0;
        ConfiguratorUI objUI = new ConfiguratorUI();
        ConfiguratorController objController = new ConfiguratorController();
        objUI.GroupName = txtCVoiceGroup.Text.Trim();
        GroupID = objController.SaveCVoiceGroup(objUI, null);
        if (GroupID == 0)
        {
            string strjscript = "<script language='javascript' type='text/javascript'>";
            strjscript += " setLabelText('ctl00_ContentPlaceHolder1_lblMessage','group already exists' );";
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
        CVoiceUI objUI = new CVoiceUI();
        MastersController objController = new MastersController();
        string lblMsg = "";
        int GroupID = 0;
        int CVoiceCode = Convert.ToInt32(txtCVoiceCode.Text.Trim());
        if (rdoAdd.Checked)
        {
            GroupID = SaveCVoiceGroup();
        }
        if (rdoAssign.Checked)
        {
            if (drpCVoiceGroup.SelectedIndex != 0)
            {
                GroupID = Convert.ToInt32(drpCVoiceGroup.SelectedValue);
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
            else
            {
                InReport = 0;
            }


            
           // string strDate = getDate(strCalDate);
            objUI.CVoiceCode = CVoiceCode;
            objUI.GroupID = GroupID;
            objUI.Description = strDescription;
            objUI.IsActive = IsActive;
            objUI.InReport = InReport;
            objUI.IsGroup = 1;

            //string DateTimeAppointment = Convert.ToString(CalstartDate.SelectedDate);
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
            //string strQuery = "Insert into Culprit (Code,Description,IsActive,InReport,EffectDate) values (" + CulpritCode + ",'" + strDescription + "'," + IsActive + "," + InReport + ",'" + strDate + "')";
            try
            {
                objController.AddCVoice(objUI, null);
                Response.Redirect(strProjectName+"/View/Forms/Master/CustumerVoiceDefault.aspx");
            }
            catch (Exception ex)
            {
                string strMessage = ex.Message;
                if (strMessage.Contains("Cannot insert duplicate"))
                {
                    lblMsg = "Cannot insert duplicate value, Please check the culprit code";
                    string strjscript = "<script language='javascript'>";
                    strjscript += " setMessageText('ctl00_ContentPlaceHolder1_lblMessage','" + lblMsg + "' );";
                    strjscript += "</script" + ">";
                    Literal1.Text = strjscript;
                    // lblMessage.Text = "Cannot insert duplicate value, Please check the culprit code";
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

        string varDate = strDate[1] + "." + strDate[0] + "." + strDate[2];
        return varDate;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(strProjectName+"/View/Forms/Master/CustumerVoiceDefault.aspx");
    }

    public void FillCustomerVoiceDetail(string Code, string GroupID)
    {
        hdnStatusID.Value = "1";
        btnAdd.Text = "Update";
        btnAdd.ToolTip = "Update";
       txtCVoiceCode.Enabled = false;
        string strQuery = "";
        DataTable dtinformation = new DataTable();
        strQuery = "select * from CustomerVoice where Code='" + Code + "'";
        dtinformation = objQueryController.ExecuteQuery(strQuery);
        if (dtinformation != null)
        {
            if (dtinformation.Rows.Count > 0)
            {
               txtCVoiceCode.Text = dtinformation.Rows[0]["Code"].ToString();
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
                //else
                //{

                //    CalstartDate.NullableLabelText = "";
                //}
                if (dtinformation.Rows[0]["IsGroup"].ToString() == "1")
                {
                    rdoAdd.Checked = false;
                    rdoAssign.Checked = true;
                   drpCVoiceGroup.SelectedValue = GroupID;

                }

            }
        }

    }
}
