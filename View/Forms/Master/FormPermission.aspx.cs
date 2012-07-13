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

public partial class View_Forms_Master_FormPermission : System.Web.UI.Page
{
    QueryConroller objQuerycontroller = new QueryConroller();
    UserController objController = new UserController();
    UserUI objcUI = new UserUI();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindRole();
            FetchRecord();
            ShowForm();

        }
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


    public void FetchRecord()
    {
        string strQuery = "";
        string strModule = "";
        string strFormsDetail = "";

        string strForm = "";

        DataSet ds = new DataSet();

        strModule = "  Select * from Module order by ModulName  ";

        strForm = " Select * from FormName order by FormName ";
        strFormsDetail = " select distinct  * from VW_FormsDetail  ";
        strQuery = strModule + strForm + strFormsDetail;
        ds = objQuerycontroller.ExecuteQueryWithDataSet(strQuery);
        DataTable dtFormsDetail = ds.Tables[2];
        ChkModule.DataSource = ds.Tables[0];
        ChkModule.DataTextField = "ModulName";
        ChkModule.DataValueField = "ModulID";
        ChkModule.DataBind();
        //foreach (ListItem list in ChkModule.Items)
        //{
        //    list.Selected = true;

        //}



      
        string strRowFilter = "";

        /********************CSI**************************/
        DataView dv = new DataView(dtFormsDetail);
        strRowFilter = "ModulID=6 and ID=12";
        dv.RowFilter = strRowFilter;
        DataTable dtMaster = dv.ToTable();
        CSIChkMaster.DataSource = dtMaster;
        CSIChkMaster.DataTextField = "FormCaption";
        CSIChkMaster.DataValueField = "FormID";
        CSIChkMaster.DataBind();

        strRowFilter = "ModulID=6 and ID=13";
        dv.RowFilter = strRowFilter;
        DataTable dtDatainput = dv.ToTable();
        CSIChkDatainput.DataSource = dtDatainput;
        CSIChkDatainput.DataTextField = "FormCaption";
        CSIChkDatainput.DataValueField = "FormID";
        CSIChkDatainput.DataBind();


        strRowFilter = "ModulID=6 and ID=14";
        dv.RowFilter = strRowFilter;
        DataTable dtReport = dv.ToTable();
        CSIChkReports.DataSource = dtReport;
        CSIChkReports.DataTextField = "FormCaption";
        CSIChkReports.DataValueField = "FormID";
        CSIChkReports.DataBind();
/********************MIS**************************/
        strRowFilter = "ModulID=5 and ID=18";
        dv.RowFilter = strRowFilter;
        DataTable dtMISMaster = dv.ToTable();
        MISChkMaster.DataSource = dtMISMaster;
        MISChkMaster.DataTextField = "FormCaption";
        MISChkMaster.DataValueField = "FormID";
        MISChkMaster.DataBind();

        strRowFilter = "ModulID=5 and ID=23";
        dv.RowFilter = strRowFilter;
        DataTable dtMISDatainput = dv.ToTable();
        MISChkDatainput.DataSource = dtMISDatainput;
        MISChkDatainput.DataTextField = "FormCaption";
        MISChkDatainput.DataValueField = "FormID";
        MISChkDatainput.DataBind();

        strRowFilter = "ModulID=5 and ID=24";
        dv.RowFilter = strRowFilter;
        DataTable dtMISReport = dv.ToTable();
        MISChkReports.DataSource = dtMISReport;
        MISChkReports.DataTextField = "FormCaption";
        MISChkReports.DataValueField = "FormID";
        MISChkReports.DataBind();

        strRowFilter = "ModulID=5 and ID=25";
        dv.RowFilter = strRowFilter;
        DataTable dtESA = dv.ToTable();
        MISChkESA.DataSource = dtESA;
        MISChkESA.DataTextField = "FormCaption";
        MISChkESA.DataValueField = "FormID";
        MISChkESA.DataBind();

        strRowFilter = "ModulID=5 and ID=26";
        dv.RowFilter = strRowFilter;
        DataTable dtservic = dv.ToTable();
        MISChkServiceCamp.DataSource = dtservic;
        MISChkServiceCamp.DataTextField = "FormCaption";
        MISChkServiceCamp.DataValueField = "FormID";
        MISChkServiceCamp.DataBind();


        /********************PQR**************************/
        strRowFilter = "ModulID=4 and ID=19";
        dv.RowFilter = strRowFilter;
        DataTable dtPQRMaster = dv.ToTable();
        PQRCHKEngineTracking.DataSource = dtPQRMaster;
        PQRCHKEngineTracking.DataTextField = "FormCaption";
        PQRCHKEngineTracking.DataValueField ="FormID";
        PQRCHKEngineTracking.DataBind();

        strRowFilter = "ModulID=4 and ID=20";
        dv.RowFilter = strRowFilter;
        DataTable dtPQRDatainput = dv.ToTable();
        PQRCHKPQR.DataSource = dtPQRDatainput;
        PQRCHKPQR.DataTextField = "FormCaption";
        PQRCHKPQR.DataValueField = "FormID";
        PQRCHKPQR.DataBind();

        strRowFilter = "ModulID=4 and ID=22";
        dv.RowFilter = strRowFilter;
        DataTable dtPQRReport = dv.ToTable();
        PQRCHKReport.DataSource = dtPQRReport;
        PQRCHKReport.DataTextField = "FormCaption";
        PQRCHKReport.DataValueField = "FormID";
        PQRCHKReport.DataBind();

        /********************SSM**************************/
        strRowFilter = "ModulID=3 and ID=6";
        dv.RowFilter = strRowFilter;
        DataTable dtSSMMaster = dv.ToTable();
        ssmChkMaster.DataSource = dtSSMMaster;
        ssmChkMaster.DataTextField = "FormCaption";
        ssmChkMaster.DataValueField = "FormID";
        ssmChkMaster.DataBind();

        strRowFilter = "ModulID=3 and ID=7";
        dv.RowFilter = strRowFilter;
        DataTable dtSSMDatainput = dv.ToTable();
        ssmChkDatainput.DataSource = dtSSMDatainput;
        ssmChkDatainput.DataTextField = "FormCaption";
        ssmChkDatainput.DataValueField = "FormID";
        ssmChkDatainput.DataBind();

        strRowFilter = "ModulID=3 and (ID=8 or ID=11)";
        dv.RowFilter = strRowFilter;
        DataTable dtSSMReport = dv.ToTable();
        ssmChkReports.DataSource = dtSSMReport;
        ssmChkReports.DataTextField = "FormCaption";
        ssmChkReports.DataValueField = "FormID";
        ssmChkReports.DataBind();


        /********************WMS**************************/
        strRowFilter = "ModulID=2 and ID=1";
        dv.RowFilter = strRowFilter;
        DataTable dtWMSMaster = dv.ToTable();
       WMSChkMaster.DataSource = dtWMSMaster;
       WMSChkMaster.DataTextField = "FormCaption";
       WMSChkMaster.DataValueField = "FormID";
       WMSChkMaster.DataBind();

        strRowFilter = "ModulID=2 and ID=2";
        dv.RowFilter = strRowFilter;
        DataTable dtWMSDatainput = dv.ToTable();
        WMSChkConfigurator.DataSource = dtWMSDatainput;
        WMSChkConfigurator.DataTextField = "FormCaption";
        WMSChkConfigurator.DataValueField = "FormID";
        WMSChkConfigurator.DataBind();

        strRowFilter = "ModulID=2 and ID=4 ";
        dv.RowFilter = strRowFilter;
        DataTable dtWMSReport = dv.ToTable();
        WMSChkReports.DataSource = dtWMSReport;
        WMSChkReports.DataTextField = "FormCaption";
        WMSChkReports.DataValueField = "FormID";
        WMSChkReports.DataBind();

        strRowFilter = "ModulID=2 and ID=3 ";
        dv.RowFilter = strRowFilter;
        DataTable dtWMSImport = dv.ToTable();
        WMSChkImport.DataSource = dtWMSImport;
        WMSChkImport.DataTextField = "FormCaption";
        WMSChkImport.DataValueField = "FormID";
        WMSChkImport.DataBind();

        strRowFilter = "ModulID=2 and ID=5 ";
        dv.RowFilter = strRowFilter;
        DataTable dtWMSGraph = dv.ToTable();
        WMSChkGraph.DataSource = dtWMSGraph;
        WMSChkGraph.DataTextField = "FormCaption";
        WMSChkGraph.DataValueField = "FormID";
        WMSChkGraph.DataBind();

        /********************ECM**************************/
       
        strRowFilter = "ModulID=8 and ID=29";
        dv.RowFilter = strRowFilter;
        DataTable dtECMDatainput = dv.ToTable();
       ECMChkDataInput.DataSource = dtECMDatainput;
       ECMChkDataInput.DataTextField = "FormCaption";
       ECMChkDataInput.DataValueField = "FormID";
       ECMChkDataInput.DataBind();

        strRowFilter = "ModulID=8 and ID=30";
        dv.RowFilter = strRowFilter;
        DataTable dtECMReport = dv.ToTable();
        ECMChkReports.DataSource = dtECMReport;
        ECMChkReports.DataTextField = "FormCaption";
        ECMChkReports.DataValueField = "FormID";
        ECMChkReports.DataBind();

        /********************PDI**************************/
        strRowFilter = "ModulID=1 and ID=15";
        dv.RowFilter = strRowFilter;
        DataTable dtPDIMaster = dv.ToTable();
        PDIChkMaster.DataSource = dtPDIMaster;
        PDIChkMaster.DataTextField = "FormCaption";
        PDIChkMaster.DataValueField = "FormID";
        PDIChkMaster.DataBind();

        strRowFilter = "ModulID=1 and ID=16";
        dv.RowFilter = strRowFilter;
        DataTable dtPDIDatainput = dv.ToTable();
        PDIChkDataInput.DataSource = dtPDIDatainput;
        PDIChkDataInput.DataTextField = "FormCaption";
        PDIChkDataInput.DataValueField = "FormID";
        PDIChkDataInput.DataBind();

        strRowFilter = "ModulID=1 and ID=17 ";
        dv.RowFilter = strRowFilter;
        DataTable dtPDIReport = dv.ToTable();
        PDIChkReports.DataSource = dtPDIReport;
        PDIChkReports.DataTextField = "FormCaption";
        PDIChkReports.DataValueField = "FormID";
        PDIChkReports.DataBind();

        /********************SHQ**************************/

        strRowFilter = "ModulID=7 and ID=28";
        dv.RowFilter = strRowFilter;
        DataTable dtSHQMaster = dv.ToTable();
        SHQChkMaster.DataSource = dtSHQMaster;
        SHQChkMaster.DataTextField = "FormCaption";
        SHQChkMaster.DataValueField = "FormID";
        SHQChkMaster.DataBind();

       
    }

    protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillModuleList();
        fillFormList();
        ShowForm();
        btncencle.Visible = true;
        btnSave.Visible = true;
    }



    public void FillModuleList()
    {
        int RoleID = Convert.ToInt32(ddlRole.SelectedValue);
        string strModuleQuery = "select distinct ModuleID from Priviledges where RoleID=" + RoleID;
        DataTable dtModule = objQuerycontroller.ExecuteQuery(strModuleQuery);
        if (dtModule != null)
        {
            if (dtModule.Rows.Count > 0)
            {

                int count1 = 0;
                foreach (ListItem modlist in ChkModule.Items)
                {
                    modlist.Selected = false;
                    DataView dvModule = new DataView(dtModule);
                    dvModule.RowFilter = "ModuleID=" + Convert.ToInt32(modlist.Value);
                    DataTable dtModuleFilter = dvModule.ToTable();
                    if (dtModuleFilter != null)
                    {
                        if (dtModuleFilter.Rows.Count > 0)
                        {
                            modlist.Selected = true;
                            string itemStyle = "color:Red;";
                            ChkModule.Items[count1].Attributes["Style"] = itemStyle;
                        }
                    }
                    count1++;
                }
            }
        }
    }

    public void fillFormList()
    {
        int RoleID = Convert.ToInt32(ddlRole.SelectedValue);
        string strQuery = "Select * from vw_UserPrivileges where viewing=1 and RoleID=" + RoleID;
        DataTable dt = objQuerycontroller.ExecuteQuery(strQuery);
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                
                foreach (ListItem list in ChkModule.Items)
                {
                    if (list.Selected)
                    {
                        if (list.Value == "1")
                        {
                            getDetail(dt, "1", "15", PDIChkMaster);
                            getDetail(dt, "1", "16", PDIChkDataInput);
                            getDetail(dt, "1", "17", PDIChkReports);
                        }
                      else  if (list.Value == "2")
                        {

                            getDetail(dt, "2", "1", WMSChkMaster);
                            getDetail(dt, "2", "2", WMSChkConfigurator);
                            getDetail(dt, "2", "4", WMSChkReports);
                            getDetail(dt, "2", "5", WMSChkGraph);
                            getDetail(dt, "2", "3", WMSChkImport);
                        }
                        else if (list.Value == "3")
                        {

                            getDetail(dt, "3", "6", ssmChkMaster);
                            getDetail(dt, "3", "7", ssmChkDatainput);
                            getDetail(dt, "3", "8", ssmChkReports);
                        }
                        else if (list.Value == "4")
                        {
                            getDetail(dt, "4", "19", PQRCHKEngineTracking);
                            getDetail(dt, "4", "20", PQRCHKPQR);
                            getDetail(dt, "4", "22", PQRCHKReport);
                        }
                        else if (list.Value == "5")
                        {

                            getDetail(dt, "5", "18", MISChkMaster);
                            getDetail(dt, "5", "23", MISChkDatainput);
                            getDetail(dt, "5", "24", MISChkReports);
                            getDetail(dt, "5", "25", MISChkESA);
                            getDetail(dt, "5", "26", MISChkServiceCamp);
                        }
                        else if (list.Value == "6")
                        {

                            getDetail(dt, "6", "12", CSIChkMaster);
                            getDetail(dt, "6", "13", CSIChkDatainput);
                            getDetail(dt, "6", "14", CSIChkReports);
                        }

                        else if (list.Value == "7")
                        {
                            getDetail(dt, "7", "28", SHQChkMaster);
                        }
                        else if (list.Value == "8")
                        {

                            getDetail(dt, "8", "29", ECMChkDataInput);
                            getDetail(dt, "8", "30", ECMChkReports);
                        }
                    }
                }
            }
        }



    }
    public void getDetail(DataTable dtInfo, string ModulID, string SubModuleID, CheckBoxList chkList)
    {
        int count1 = 0;
        DataView dvFilter = new DataView(dtInfo);
        DataTable dtFilter = new DataTable();

        if (ModulID == "3" && SubModuleID == "8")
        {
            dvFilter.RowFilter = "ModulID=" + ModulID + " and  (ExistsinID=" + SubModuleID + " or  ExistsinID=11)";
        }
        else
        {
            dvFilter.RowFilter = "ModulID=" + ModulID + " and ExistsinID=" + SubModuleID + "";
        }
        dtFilter = dvFilter.ToTable();
        if (dtFilter != null)
        {
            if (dtFilter.Rows.Count > 0)
            {
                if (chkList != null)
                {
                    if (chkList.Items.Count > 0)
                    {
                        foreach (ListItem list in chkList.Items)
                        {
                            list.Selected = false;
                            DataView dv = new DataView(dtFilter);
                            dv.RowFilter = "FormID=" + Convert.ToInt32(list.Value);
                            DataTable dtFilterForm = dv.ToTable();
                            
                            if (dtFilterForm != null)
                            {
                                if (dtFilterForm.Rows.Count > 0)
                                {
                                    list.Selected = true;
                                    string itemStyle = "color:Red;";
                                    chkList.Items[count1].Attributes["Style"] = itemStyle;

                                }
                            }
                            
                            count1++;
                        }
                    }
                }

            }
        }
    }

    public string getModuleIDList()
    {
        int i = 0;
        string strModuleIDList = "";
        foreach (ListItem list in ChkModule.Items)
        {
            if (list.Selected)
            {
                if (list.Value == "6")
                {
                    fldCSI.Visible = true;
                }

                else if (list.Value == "1")
                {
                    fldPDI.Visible = true;
                }

                else if (list.Value == "2")
                {
                    fldWMS.Visible = true;
                }

                else if (list.Value == "3")
                {
                    fldSPARC.Visible = true;
                }

                else if (list.Value == "4")
                {
                    fldPQR.Visible = true;
                }

                else if (list.Value == "5")
                {
                    fldMIS.Visible = true;
                }

                else if (list.Value == "7")
                {
                    fldSHQ.Visible = true;
                }

                else if (list.Value == "8")
                {
                    fldECM.Visible = true;

                }

                if (i == 0)
                {
                    strModuleIDList = "ModulID=" + list.Value;
                }
                else
                {
                    strModuleIDList = strModuleIDList + " or ModulID=" + list.Value;
                }
                i++;
            }
            else
            {
                if (list.Value == "6")
                {
                    fldCSI.Visible = false;
                }

                else if (list.Value == "1")
                {
                    fldPDI.Visible = false;
                }

                else if (list.Value == "2")
                {
                    fldWMS.Visible = false;
                }

                else if (list.Value == "3")
                {
                    fldSPARC.Visible = false;
                }

                else if (list.Value == "4")
                {
                    fldPQR.Visible = false;
                }

                else if (list.Value == "5")
                {
                    fldMIS.Visible = false;
                }

                else if (list.Value == "7")
                {
                    fldSHQ.Visible = false;
                }

                else if (list.Value == "8")
                {
                    fldECM.Visible = false;
                }
            }
            
        }
        return strModuleIDList;
    }


    public void ShowForm()
    {
          getModuleIDList();
       
        
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {

        ShowForm();
      
       
    }

  
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SavePrivilegeInformation();
    }
   
   


    public void SavePrivilegeInformation()
    {
        PriviledgeUI objUI = new PriviledgeUI();
        PriviledgeController objController = new PriviledgeController();

        int RoleID = Convert.ToInt32(ddlRole.SelectedValue);

        string strDeleteQuery = "Delete from Priviledges where RoleID=" + RoleID;
        objQuerycontroller.ExecuteQuery(strDeleteQuery);
        int Status = 0;
        string strQuery = "";

        if (ChkModule != null)
        {
            if (ChkModule.Items.Count > 0)
            {
                foreach (ListItem list in ChkModule.Items)
                {
                    if (list.Selected)
                    {
                        if (list.Value == "6")
                        {
                            strQuery = strQuery + " " + getCheckBoxselection(CSIChkMaster, "6");
                            strQuery = strQuery + " " + getCheckBoxselection(CSIChkDatainput, "6");
                            strQuery = strQuery + " " + getCheckBoxselection(CSIChkReports, "6");
                            Status = 1;
                        }

                       else  if (list.Value == "1")
                        {
                            strQuery = strQuery + " " + getCheckBoxselection(PDIChkMaster, "1");
                            strQuery = strQuery + " " + getCheckBoxselection(PDIChkDataInput, "1");
                            strQuery = strQuery + " " + getCheckBoxselection(PDIChkReports, "1");
                            Status = 1;
                        }

                        else if (list.Value == "2")
                        {
                            strQuery = strQuery + " " + getCheckBoxselection(WMSChkMaster, "2");
                            strQuery = strQuery + " " + getCheckBoxselection(WMSChkConfigurator, "2");
                            strQuery = strQuery + " " + getCheckBoxselection(WMSChkReports, "2");
                            strQuery = strQuery + " " + getCheckBoxselection(WMSChkGraph, "2");
                            strQuery = strQuery + " " + getCheckBoxselection(WMSChkImport, "2");
                            Status = 1;

                        }

                        else if (list.Value == "3")
                        {
                            strQuery = strQuery + " " + getCheckBoxselection(ssmChkMaster, "3");
                            strQuery = strQuery + " " + getCheckBoxselection(ssmChkDatainput, "3");
                            strQuery = strQuery + " " + getCheckBoxselection(ssmChkReports, "3");
                            Status = 1;

                        }

                        else if (list.Value == "4")
                        {
                            strQuery = strQuery + " " + getCheckBoxselection(PQRCHKEngineTracking, "4");
                            strQuery = strQuery + " " + getCheckBoxselection(PQRCHKPQR, "4");
                            strQuery = strQuery + " " + getCheckBoxselection(PQRCHKReport, "4");
                            Status = 1;

                        }

                        else if (list.Value == "5")
                        {
                            strQuery = strQuery + " " + getCheckBoxselection(MISChkMaster, "5");
                            strQuery = strQuery + " " + getCheckBoxselection(MISChkDatainput, "5");
                            strQuery = strQuery + " " + getCheckBoxselection(MISChkReports, "5");
                            strQuery = strQuery + " " + getCheckBoxselection(MISChkESA, "5");
                            strQuery = strQuery + " " + getCheckBoxselection(MISChkServiceCamp, "5");
                            Status = 1;

                        }

                        else if (list.Value == "7")
                        {
                            strQuery = strQuery + " " + getCheckBoxselection(SHQChkMaster, "7");
                            Status = 1;
                        }

                        else if (list.Value == "8")
                        {
                            strQuery = strQuery + " " + getCheckBoxselection(ECMChkDataInput, "8");
                            strQuery = strQuery + " " + getCheckBoxselection(ECMChkReports, "8");
                            Status = 1;

                        }
                    }
                }
                  
                try
                {
                    objQuerycontroller.ExecuteQuery(strQuery);
                    //foreach (ListItem list in chkForm.Items)
                    //{

                    //    int FormID = Convert.ToInt32(list.Value);
                    //    bool viewing = false;
                    //    if (list.Selected)
                    //    {
                    //        viewing = true;
                    //    }
                    //    else
                    //    {
                    //        viewing = false;
                    //    }

                    //    objUI.FormID = FormID;
                    //    objUI.RoleID = RoleID;
                    //    objUI.viewing = viewing;

                    //    Status = objController.Save(objUI, null);
                        if (Status > 0)
                        {
                            string strjscript = "<script language='javascript' type='text/javascript'>";
                            strjscript += " setLabelText('ctl00_ContentPlaceHolder1_lblMessage','Record saved successfully' );";
                            strjscript += "</script" + ">";
                            literal1.Text = strjscript;
                        }
                        else
                        {
                            string strjscript = "<script language='javascript' type='text/javascript'>";
                            strjscript += " setLabelText('ctl00_ContentPlaceHolder1_lblMessage','Record could not be saved successfully' );";
                            strjscript += "</script" + ">";
                            literal1.Text = strjscript;
                        }
                    //}
                }
                catch
                {
                    string strjscript = "<script language='javascript' type='text/javascript'>";
                    strjscript += " setLabelText('ctl00_ContentPlaceHolder1_lblMessage','Record could not be saved successfully' );";
                    strjscript += "</script" + ">";
                    literal1.Text = strjscript;
                }

            }
        }
    }
        
    public string getCheckBoxselection(CheckBoxList chkList, string ModuleID)
    {
        int RoleID = Convert.ToInt32(ddlRole.SelectedValue);
        string strInsertParameter = "";
        if (chkList != null)
        {
            if (chkList.Items.Count > 0)
            {
                int viewValue = 0;
                foreach (ListItem list in chkList.Items)
                {
                    if (list.Selected)
                    {
                        viewValue = 1;
                    }
                    else
                    {
                        viewValue = 0;
                    }
                    strInsertParameter = strInsertParameter + " insert into Priviledges(RoleID,viewing,FormID,ModuleID ) values (" + RoleID + "," + viewValue + "," + list.Value + "," + ModuleID + ") ";
                }
            }
        }
        return strInsertParameter;
    }
         
}
