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

public partial class View_Forms_Master_User : System.Web.UI.Page
{
    DateFormat objDate = new DateFormat();
    QueryConroller objQuerycontroller = new QueryConroller();
    UserController objController = new UserController();
    UserUI objcUI = new UserUI();
    public string strUpdate = "";
    public string strProjectName = "";
    
    protected void Page_Load(object sender, EventArgs e)
    {
        strProjectName = System.Configuration.ConfigurationManager.AppSettings["WMSProjectName"];
        if (!IsPostBack)
        {
            FetchRecord();
            ChangelblName();
            BindRole();
            if (Request.QueryString["UserID"] != null)
            {
                strUpdate = "Update";
                pnlPassword.Visible = false;
                getDetail(Request.QueryString["UserID"].ToString());
            }
            else
            {
                hdnCheckID.Value = "0";
                pnlPassword.Visible = true;
            }
        }
        string strjscript = "<script language='javascript' type='text/javascript'>";
        strjscript += " setLabelText('ctl00_ContentPlaceHolder1_lblMessage','' );";
        strjscript += " setLabelText('ctl00_ContentPlaceHolder1_lblMasg','' );";
        strjscript += "</script" + ">";
        literal1.Text = strjscript;
       

    }

    public void FetchRecord()
    {
        string strQuery = "";
        string strModule = "";
        string strRegion = "";
        string strLevel = "";
        string strForm = "";

        DataSet ds = new DataSet();

        strModule = "  Select * from Module order by ModulName  ";
        strRegion = " Select * from Region order by Region ";
        strLevel = " Select * from [Level] order by [Level] ";
        strForm = " Select * from FormName order by FormName ";
        strQuery = strModule + strRegion + strLevel + strForm;
        ds = objQuerycontroller.ExecuteQueryWithDataSet(strQuery);

        chkRegion.DataSource = ds.Tables[1];
        chkRegion.DataTextField = "Region";
        chkRegion.DataValueField = "RegionID";
        chkRegion.DataBind();


        ddlLevel.DataSource = ds.Tables[2];
        ddlLevel.DataTextField = "Level";
        ddlLevel.DataValueField = "LevelID";
        ddlLevel.DataBind();
        ddlLevel.AppendDataBoundItems = true;
        ListItem list2 = new ListItem("Select", "0");
        ddlLevel.Items.Insert(0, list2);
        ddlLevel.AppendDataBoundItems = false;


        drpRegion.DataSource = ds.Tables[1];
        drpRegion.DataTextField = "Region";
        drpRegion.DataValueField = "RegionID";
        drpRegion.DataBind();
        drpRegion.AppendDataBoundItems = true;
        ListItem list3 = new ListItem("Select", "0");
        drpRegion.Items.Insert(0, list3);
        drpRegion.AppendDataBoundItems = false;


        ddlRegionRM.DataSource = ds.Tables[1];
        ddlRegionRM.DataTextField = "Region";
        ddlRegionRM.DataValueField = "RegionID";
        ddlRegionRM.DataBind();
        ddlRegionRM.AppendDataBoundItems = true;
        ListItem list4 = new ListItem("Select", "0");
        ddlRegionRM.Items.Insert(0, list4);
        ddlRegionRM.AppendDataBoundItems = false;



        ddlRegionToRM.DataSource = ds.Tables[1];
        ddlRegionToRM.DataTextField = "Region";
        ddlRegionToRM.DataValueField = "RegionID";
        ddlRegionToRM.DataBind();
        ddlRegionToRM.AppendDataBoundItems = true;
        ListItem list5 = new ListItem("Select", "0");
        ddlRegionToRM.Items.Insert(0, list5);
        ddlRegionToRM.AppendDataBoundItems = false;



    }

    public void BindRole()
    {
        string strQuery = "";
        DataTable dtRole = new DataTable();
        strQuery = "select * from Role where IsActive=1 order by Role ";
        dtRole = objQuerycontroller.ExecuteQuery(strQuery);
        if (dtRole != null)
        {
            if (dtRole.Rows.Count > 0)
            {
                ddlRole.DataSource = dtRole;

                ddlRole.DataTextField = "Role";
                ddlRole.DataValueField = "RoleID";
                ddlRole.DataBind();
                ddlRole.AppendDataBoundItems = true;
                ListItem list = new ListItem("Select", "0");
                ddlRole.Items.Insert(0, list);
                ddlRole.AppendDataBoundItems = false;

            }
        }
    }

    public void ChangelblName()
    {
        DataTable dtControls = new DataTable();
        string strQuery = "Select * from Label_Data where Form='User'";
        dtControls = objQuerycontroller.ExecuteQuery(strQuery);
        foreach (DataRow dr in dtControls.Rows)
        {
            string strLabel = dr["Label_ID"].ToString();
            string strLabelText = dr["Text"].ToString();

            if (strLabel == "lblFullName")
            {
               lblFullName.Text = strLabelText;
            }
            else if (strLabel =="lblEmployeeCode")
            {
            lblEmployeeCode.Text = strLabelText;
            }
            else if (strLabel == "lblRole")
            {
               lblRole.Text = strLabelText;
            }
            else if (strLabel == "lblActive")
            {
              lblActive.Text = strLabelText;
            }
            else if (strLabel == "lblCurrentAddresst")
            {
               lblCurrentAddress.Text = strLabelText;
            }
            else if (strLabel == "lblPermanentAddress")
            {
               lblPermanentAddress.Text = strLabelText;
            }
            else if (strLabel == "lblPhoneNo")
            {
              lblPhoneNo.Text = strLabelText;
            }
            else if (strLabel == "lblPermanentAddress")
            {
               lblMobileNo.Text = strLabelText;
            }
            else if (strLabel == "lblPassword")
            {
               lblPassword.Text = strLabelText;
            }
            else if (strLabel == "lblDateOfJoing")
            {
              lblDateOfJoing.Text = strLabelText;
            }
            else if (strLabel == "lblConfirmPassword")
            {
               lblConfirmPassword.Text = strLabelText;
            }
            else if (strLabel == "lblEmailID")
            {
              lblEmailID.Text = strLabelText;
            }
            else if (strLabel == "lblMobileNo")
            {
               lblMobileNo.Text = strLabelText;
            }


        }


    }

   

   
    public int SaveUser()
    {
        int TypeID = 0;
        int Check = 0;
        if (ddlLevel.SelectedValue != "4" &&  ddlLevel.SelectedValue != "5")
        {
            TypeID = 1;
        }

        else if (ddlLevel.SelectedValue == "5")
        {
            TypeID = 2;
        }
        else if (ddlLevel.SelectedValue == "4")
        {
            TypeID = 3;
        }
        int UserID = 0;
        int UserDetailID = 0;
       
        DataTable dtControls = new DataTable();
         if (hdnEmployeeCode.Value.Trim().ToLower() !=txtEmployeeCode.Text.Trim().ToLower())
        {

            string strQuery = "Select * from UserInfo where EmployeeCode='" + txtEmployeeCode.Text.Trim() + "'";
            dtControls = objQuerycontroller.ExecuteQuery(strQuery);
        }
        if (dtControls == null || dtControls.Rows.Count == 0)
        {
            objcUI.FullName = txtFullName.Text;
            objcUI.EmployeeCode = txtEmployeeCode.Text;
            objcUI.RoleID = Convert.ToInt32(ddlRole.SelectedValue);
            objcUI.LoginID = txtLoginID.Text;

            objcUI.EmailID = txtEmailID.Text;
            objcUI.PhoneNo = txtPhoneNo.Text;
            objcUI.MobileNo = txtMobileNo.Text.Trim();
            objcUI.Password = txtPassword.Text;
            objcUI.CurrentAddress = txtCurrentAddress.Text.Trim();
            objcUI.PermanentAddress = txtPermanentAddress.Text.Trim();
            objcUI.UserTypeID = TypeID;
            objcUI.LevelID = Convert.ToInt32(ddlLevel.SelectedValue);
            //string DateTimeAppointment = Convert.ToString(culDateOfJoing.SelectedDate);
            string DateOfJoing = objDate.ConvertDateFormat(culDateOfJoing.Value);
            if (DateOfJoing == "")
            {
                objcUI.DateOfJoing = Convert.ToDateTime("1/1/1900");
            }
            else
            {
                objcUI.DateOfJoing = Convert.ToDateTime(DateOfJoing);
            }



            objcUI.IsActive = chkActive.Checked;
            objcUI.Id = Convert.ToInt32(hdnUserID.Value);
            objcUI.CheckID = Convert.ToInt32(hdnCheckID.Value);
            int status = 0;

            if (ddlLevel.SelectedValue == "4" || ddlLevel.SelectedValue == "5")
            {
                if (hdnCheckBoxID.Value == "")
                {
                    string strjscript = "<script language='javascript' type='text/javascript'>";
                    strjscript += " setLabelText('ctl00_ContentPlaceHolder1_lblMessage',' please Select atleast one Dealer ' );";
                    strjscript += "</script" + ">";
                    literal1.Text = strjscript;
                    status = 1;
                }
                else
                {

                    string ChkID = hdnCheckBoxID.Value;
                    string[] strArrayActualValue = ChkID.Split(',');
                    string strDealerID = "";
                    DataTable dt = new DataTable();
                    if (hdnCheckID.Value == "0")
                    {
                        strDealerID = "select code from Dealer Where ID in (select DealerID from S_DealerDetail where DealerID in (" + hdnCheckBoxID.Value + "))";
                        dt = objQuerycontroller.ExecuteQuery(strDealerID);
                        string Dealer = "";
                        if (dt != null)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                status = 1;
                                foreach (DataRow drDealer in dt.Rows)
                                {
                                    if (Dealer == "")
                                    {
                                        Dealer = drDealer["code"].ToString();
                                    }
                                    else
                                    {
                                        Dealer = Dealer + "," + drDealer["code"].ToString();
                                    }
                                }
                                string strjscript = "<script language='javascript' type='text/javascript'>";
                                strjscript += " setLabelText('ctl00_ContentPlaceHolder1_lblMasg','this Dealer already assign(" + Dealer + ")' );";
                                strjscript += "</script" + ">";
                                literal1.Text = strjscript;


                            }
                        }
                    }
                }
            }


            if (status == 0)
            {
                UserID = objController.SaveUser(objcUI, null);
                if (UserID == 0)
                {
                    string strjscript = "<script language='javascript' type='text/javascript'>";
                    strjscript += " setLabelText('ctl00_ContentPlaceHolder1_lblMessage','Employee Code already exists' );";
                    strjscript += "</script" + ">";
                    literal1.Text = strjscript;
                }


                else
                {
                    //Response.Redirect(strProjectName+"/View/Forms/Master/UserDefault.aspx");
                    if (hdnCheckID.Value == "1")
                    {
                        UserID = objcUI.Id;
                        string strDeleteUserDetail = "Delete from UserDetail where UserID=" + UserID;
                        objQuerycontroller.ExecuteQuery(strDeleteUserDetail);
                    }
                    if (ddlLevel.SelectedValue != null)
                    {
                        objcUI.UserID = UserID;
                        if (ddlLevel.SelectedValue == "1" || ddlLevel.SelectedValue == "2")
                        {
                            if (ddlRole.SelectedValue == "2" || ddlRole.SelectedValue == "16")
                            {
                                PnlRegion.Visible = true;
                                foreach (ListItem list in chkRegion.Items)
                                {
                                    objcUI.UserID = UserID;
                                    objcUI.ReportsToID = 1;
                                    objcUI.dRegionID = Convert.ToInt32(list.Value);
                                    UserDetailID = objController.SaveUserDetail(objcUI, null);

                                }
                            }
                            else
                            {
                                PnlRegion.Visible = false;

                            }
                        }
                        if (ddlLevel.SelectedValue == "3")
                        {
                            objcUI.ReportsToID = 1;
                            objcUI.dRegionID = Convert.ToInt32(ddlRegionRM.SelectedValue);
                            UserDetailID = objController.SaveUserDetail(objcUI, null);
                        }
                        if (ddlLevel.SelectedValue == "6")
                        {
                            objcUI.ReportsToID = Convert.ToInt32(hdnRMID.Value);
                            objcUI.dRegionID = Convert.ToInt32(ddlRegionToRM.SelectedValue);
                            UserDetailID = objController.SaveUserDetail(objcUI, null);
                        }
                        else if (ddlLevel.SelectedValue == "4" || ddlLevel.SelectedValue == "5")
                        {
                            objcUI.ReportsToID = Convert.ToInt32(hdnReportsID.Value);
                            objcUI.dRegionID = Convert.ToInt32(drpRegion.SelectedValue);
                            UserDetailID = objController.SaveUserDetail(objcUI, null);
                            string strQuery = "";

                            strQuery = strQuery + " " + getCheckBoxselection(UserID.ToString());
                            if (strQuery.Trim() != "")
                            {
                                objQuerycontroller.ExecuteQuery(strQuery);
                            }
                            else
                            {
                                Check = 1;
                            }
                        }
                    }
                    if (Check == 0)
                    {

                        if (UserDetailID == 0)
                        {
                            string strjscript = "<script language='javascript' type='text/javascript'>";
                            strjscript += " setLabelText('ctl00_ContentPlaceHolder1_lblMessage','User not saved successfully' );";
                            strjscript += "</script" + ">";
                            literal1.Text = strjscript;
                        }
                        else
                        {

                            if (hdnCheckID.Value == "1")
                            {
                                if (Check == 0)
                                {
                                    Response.Redirect("UserDefault.aspx");
                                }
                            }

                            else
                            {
                                string strjscript = "<script language='javascript' type='text/javascript'>";
                                strjscript += " setLabelText('ctl00_ContentPlaceHolder1_lblMessage','User saved successfully' );";
                                strjscript += "</script" + ">";
                                literal1.Text = strjscript;
                            }
                        }
                    }

                }
            }
      }
        else
        {
                string strjscript = "<script language='javascript' type='text/javascript'>";
                strjscript += " setLabelText('ctl00_ContentPlaceHolder1_lblMessage','Employee Code already exists' );";
                strjscript += "</script" + ">";
                literal1.Text = strjscript;
           
        
        }
      
        return UserID;
    }

    public string getCheckBoxselection(string UserID)
    {

       
        string strInsertParameter = "";
        string ChkID = hdnCheckBoxID.Value;
        int checkStatus = 0;
        string[] strArrayActualValue = ChkID.Split(',');
        string strDealerID = "";
        DataTable dt = new DataTable();
        if (hdnCheckBoxID.Value != "")
        {
            strDealerID = "select code from Dealer Where ID in (select DealerID from S_DealerDetail where DealerID in (" + hdnCheckBoxID.Value + ") and  UserID<>" + UserID + ")";
            dt = objQuerycontroller.ExecuteQuery(strDealerID);
        }
        else
        {
            string strjscript = "<script language='javascript' type='text/javascript'>";
            strjscript += " setLabelText('ctl00_ContentPlaceHolder1_lblMasg','please Select atleast one Dealer ' );";
            strjscript += "</script" + ">";
            literal1.Text = strjscript;
            checkStatus = 1;

        }
       
       
        string Dealer = ""; 
        if(dt!=null)
        {
            if (dt.Rows.Count > 0)
            {
                checkStatus = 1;
                foreach (DataRow drDealer in dt.Rows)
                {
                    if (Dealer == "")
                    {
                        Dealer = drDealer["code"].ToString();
                    }
                    else
                    {
                        Dealer = Dealer + "," + drDealer["code"].ToString();
                    }
                }
                string strjscript = "<script language='javascript' type='text/javascript'>";
                strjscript += " setLabelText('ctl00_ContentPlaceHolder1_lblMasg','This Dealer already assign(" + Dealer + ")' );";
                strjscript += "</script" + ">";
                literal1.Text = strjscript;

               
            }   
        }
       if (checkStatus == 0)
        {
            string strDelete = "Delete from S_DealerDetail where UserID=" + UserID;
            objQuerycontroller.ExecuteQuery(strDelete);
            int index = 0;

            int count = Convert.ToInt32(strArrayActualValue.Length.ToString());
            for (int i = 0; i < count; i++)
            {

                strInsertParameter = strInsertParameter + " insert into S_DealerDetail(DealerID,UserID) values (" + strArrayActualValue[i] + "," + UserID + ") ";
            }

           
        }
        return strInsertParameter;
    }



   

    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveUser();
    }

    protected void btncencle_Click(object sender, EventArgs e)
    {
      Response.Redirect(strProjectName+"/View/Forms/Master/UserDefault.aspx");
    }

    public void getDetail(string UserID)
    {
        string strQuery = "";
        hdnUserID.Value = UserID;
        hdnCheckID.Value = "1";
        DataTable dtinformation = new DataTable();
        btnSave.ToolTip = "Update";
        btnSave.Text = "Update";

        // strQuery = "select * from Location where IsActive=1";
        strQuery = "select * from UserInfo where UserID=" + UserID + " ";
        dtinformation = objQuerycontroller.ExecuteQuery(strQuery);
        if (dtinformation != null)
        {


            if (dtinformation.Rows.Count > 0)
            {
                foreach (DataRow drinformation in dtinformation.Rows)
                {
                   
                    txtFullName.Text = drinformation["FullName"].ToString();
                    txtEmployeeCode.Text = drinformation["EmployeeCode"].ToString();
                    hdnEmployeeCode.Value = drinformation["EmployeeCode"].ToString();
                    ddlRole.SelectedValue = drinformation["RoleID"].ToString();
                    txtLoginID.Text = drinformation["LoginID"].ToString();
                    txtEmailID.Text = drinformation["EmailID"].ToString();
                    txtPhoneNo.Text = drinformation["PhoneNo"].ToString();
                    txtMobileNo.Text = drinformation["MobileNo"].ToString();
                    txtPermanentAddress.Text = drinformation["PermanentAddress"].ToString();
                    txtCurrentAddress.Text = drinformation["CurrentAddress"].ToString();
                    string strJoinDate = drinformation["DateOfJoing"].ToString();
                    int Usertypeid = Convert.ToInt32(drinformation["UserTypeID"].ToString());
                    strJoinDate = objDate.ConvertDateFormat(strJoinDate);
                    if (drinformation["DateOfJoing"].ToString() == "1/1/1900 12:00:00 AM")
                    {
                         culDateOfJoing.Value = "";
                    }
                    else
                    {
                      
                        string[] strDateArray = strJoinDate.Split(' ');
                        culDateOfJoing.Value = strDateArray[0].ToString();
                    }


                    if (Convert.ToBoolean(drinformation["IsActive"]) == true)
                    {
                        chkActive.Checked = true;
                    }
                    else
                    {
                        chkActive.Checked = false;
                    }


                    ddlLevel.SelectedValue = drinformation["LevelID"].ToString();
               


                }
                string strDetailQuery = "select * from UserDetail where UserID=" + UserID;
                string strSDealerDetail = "select * from S_DealerDetail where UserID=" + UserID;
                 DataSet ds = objQuerycontroller.ExecuteQueryWithDataSet(strDetailQuery + strSDealerDetail);
                 DataTable dtDetail = ds.Tables[0];
                DataTable dtSDealer =ds.Tables[1];
                if (dtDetail != null)
                {
                    if (dtDetail.Rows.Count > 0)
                    {
                        foreach (DataRow drDetail in dtDetail.Rows)
                        {
                            if (ddlLevel.SelectedValue == "3")
                            {
                                ddlRegionRM.SelectedValue = drDetail["RegionID"].ToString();
                            }
                            else if (ddlLevel.SelectedValue == "6")
                            {
                                ddlRegionToRM.SelectedValue = drDetail["RegionID"].ToString();
                                hdnRMID.Value = drDetail["ReportsToID"].ToString();
                               
                            }
                            else if (ddlLevel.SelectedValue == "4" || ddlLevel.SelectedValue == "5")
                            {
                                drpRegion.SelectedValue = drDetail["RegionID"].ToString();
                                hdnReportsID.Value = drDetail["ReportsToID"].ToString();

                               
                            }
                        }
                    }
                }

                if (dtSDealer != null)
                {
                    if (dtSDealer.Rows.Count > 0)
                    {
                        if (ddlLevel.SelectedValue == "4" || ddlLevel.SelectedValue == "5")
                        {
                            foreach (DataRow drDealer in dtSDealer.Rows)
                            {
                                if (hdnCheckBoxID.Value == "")
                                {
                                    hdnCheckBoxID.Value = drDealer["DealerID"].ToString();
                                }
                                else
                                {
                                    hdnCheckBoxID.Value = hdnCheckBoxID.Value + "," + drDealer["DealerID"].ToString();
                                }
                            }
                        }
                    }
                }
            }
            
        }
    }


  

   




   

    

   



}