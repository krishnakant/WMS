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

public partial class View_Forms_Master_Culprit : System.Web.UI.Page
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
            getCulpritGroup();

            if (Request.QueryString["Code"] != null)
            {
                FillDetail(Request.QueryString["Code"].ToString(), Request.QueryString["GroupID"].ToString());
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
        string strQuery = "Select * from Label_Data where Form='Culprit'";
        dtControls = objQueryController.ExecuteQuery(strQuery);
        foreach (DataRow dr in dtControls.Rows)
        {
            string strLabel = dr["Label_ID"].ToString();
            string strLabelText = dr["Text"].ToString();

            if (strLabel == "lblCulpritCode")
            {
               lblCulpritCode.Text = strLabelText;
            }
            else if (strLabel == "lblCulpritGroup")
            {
                lblCulpritGroup.Text = strLabelText;
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

    public void getCulpritGroup()
    {

        string strQuery = "";
        DataTable dtinformation = new DataTable();
        strQuery = "select * from CulpritGroup order by GRoupName";
        dtinformation = objQueryController.ExecuteQuery(strQuery);
        if (dtinformation != null)
        {

            if (dtinformation.Rows.Count > 0)
            {
                drpCulpritGroup.DataSource = dtinformation;
                drpCulpritGroup.DataValueField = "CulpritGroupID";
                drpCulpritGroup.DataTextField = "GRoupName";
                drpCulpritGroup.DataBind();
                drpCulpritGroup.AppendDataBoundItems = true;
                ListItem list = new ListItem("Select", "0");
                drpCulpritGroup.Items.Insert(0, list);
                drpCulpritGroup.AppendDataBoundItems = false;
            }
        }
    }
    public void FillDetail(string Code, string GroupID)
    {
        hdnStatusID.Value = "1";
        btnAdd.Text = "Update";
        btnAdd.ToolTip = "Update";
        txtCulpritCode.Enabled = false;
        string strQuery = "";
        DataTable dtinformation = new DataTable();
        strQuery = "select * from Culprit where Code='" + Code + "'";
        dtinformation = objQueryController.ExecuteQuery(strQuery);
        if (dtinformation != null)
        {
            if (dtinformation.Rows.Count > 0)
            {
                txtCulpritCode.Text = dtinformation.Rows[0]["Code"].ToString();
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
                string strCalstartDate =dtinformation.Rows[0]["EffectDate"].ToString();
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
                    drpCulpritGroup.SelectedValue = GroupID;

                }

            }
        }

    }
    public int SaveCulpritGroup()
    {
        int GroupID = 0;
        ConfiguratorUI objUI = new ConfiguratorUI();
        ConfiguratorController objController = new ConfiguratorController();
        objUI.GroupName = txtCulpritGroup.Text.Trim();
        GroupID = objController.SaveCulpritGroup(objUI, null);
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
        CulpritUI objUI = new CulpritUI();
        MastersController objController = new MastersController();
        string lblMsg = "";
        int GroupID = 0;
        int CulpritCode = Convert.ToInt32(txtCulpritCode.Text.Trim());
        if (rdoAdd.Checked)
        {
            GroupID = SaveCulpritGroup();
        }
        if (rdoAssign.Checked)
        {
            if (drpCulpritGroup.SelectedIndex != 0)
            {
                GroupID = Convert.ToInt32(drpCulpritGroup.SelectedValue);
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
       
            objUI.CulpritCode = CulpritCode;
            objUI.GroupID = GroupID;
            objUI.Description = strDescription;
            objUI.IsActive = IsActive;
            objUI.InReport = InReport;
            objUI.IsGroup = 1;
            objUI.StatusID = Convert.ToInt32(hdnStatusID.Value);
            try
            {
                objController.AddCulprit(objUI, null);

                Response.Redirect(strProjectName+"/View/Forms/Master/CulpritDefault.aspx");
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
                    getCulpritGroup();
                }
                else
                {
                    lblMsg = "Record could not be added successfully";
                    string strjscript = "<script language='javascript'>";
                    strjscript += " setMessageText('ctl00_ContentPlaceHolder1_lblMessage','" + lblMsg + "' );";
                    strjscript += "</script" + ">";
                    Literal1.Text = strjscript;
                  
                }
            }
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(strProjectName+"/View/Forms/Master/CulpritDefault.aspx");
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
}
