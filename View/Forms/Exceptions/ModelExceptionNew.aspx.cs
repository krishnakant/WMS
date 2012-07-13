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

public partial class View_Forms_Exceptions_ModelExceptionNew : System.Web.UI.Page
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
        strQuery = "select distinct Model_Code as Code from acrtemp where IsModelEx=1 and Model_Code<>'' and Model_Code<>'0' ";
        strQuery += " Union all ";
        strQuery += " select distinct Model_Code as Code from ProductionTemp where IsModelEx=1 and Model_Code<>'' and Model_Code<>'0'";
        dtProductCode = objQuerycontroller.ExecuteQuery(strQuery);
        if (dtProductCode != null)
        {
            if (dtProductCode.Rows.Count > 0)
            {
                chkProductCode.DataSource = dtProductCode;

                chkProductCode.DataTextField = "Code";
                chkProductCode.DataValueField = "Code";
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
            GroupID = saveGroupName();
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
                    objUI.ID = Convert.ToInt32(list.Value);
                    objUI.GroupID = GroupID;
                    objUI.source = "Exception";
                    objController.SaveProductGroupMapping(objUI, null);
                }
            }
            string strQuery = "Select * from AcrTemp where IsModelEx=0 and IsItemEx=0 and IsCulpritEx=0 and IsCVoiceEx=0 and IsDefectEx=0";
            DataTable dt = objQuerycontroller.ExecuteQuery(strQuery);
            if (dt != null)
            {
                SaveAcr(dt);
                string strDeleteQuery = "Delete from AcrTemp where IsModelEx=0 and IsItemEx=0 and IsCulpritEx=0 and IsCVoiceEx=0 and IsDefectEx=0";
                objQuerycontroller.ExecuteQuery(strDeleteQuery);

            }

            string strProductionQuery = "Select * from ProductionTemp where IsModelEx=0";
            DataTable dtProduction = objQuerycontroller.ExecuteQuery(strProductionQuery);
            if (dtProduction != null)
            {
                SaveProduction(dtProduction);
                string strDeleteQuery = "Delete from ProductionTemp where IsModelEx=0";
                objQuerycontroller.ExecuteQuery(strDeleteQuery);

            }

            Response.Redirect("/WMS/View/Forms/Exceptions/ModelExceptionNew.aspx");
        }
    }

    public void SaveProduction(DataTable dt)
    {
       
        ProductionController objCont = new ProductionController();

        DataTable dtView = new DataTable();
        string strViewQuery = "Select distinct * from vwMastersCode";
        dtView = objQuerycontroller.ExecuteQuery(strViewQuery);


        DataTable dtModel = new DataTable();
        string strModelQuery = "select * from Model";
        dtModel = objQuerycontroller.ExecuteQuery(strModelQuery);

      
               
                int rcount = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    ProductionUI objUI = new ProductionUI();
                    string strCause = "model";
                    int modelex = 1;


                    try
                    {
                        int flag = 0;
                        for (int i = 0; i < dtView.Rows.Count; i++)
                        {
                            if ("model" == dtView.Rows[i]["tablename"].ToString() && dr["Model_Code"].ToString() == dtView.Rows[i]["code"].ToString())
                            {
                                DataView dv = new DataView(dtModel);
                                dv.RowFilter = "Code ='" + dr["Model_Code"].ToString() + "'";
                                DataTable dtModelCode = dv.ToTable();
                                objUI.Model_Code = Convert.ToString(dtModelCode.Rows[0]["Model_Code"]);
                                strCause = strCause.Replace("model", "");
                                modelex = 0;
                                flag++;
                            }
                        }
                        string strS = Convert.ToString(dr["S"]);
                        if (strS == "")
                        {
                            objUI.S = Convert.ToInt16(null);
                        }
                        else
                        {
                            objUI.S = Convert.ToInt16(dr["S"]);
                        }

                        objUI.Material = Convert.ToString(dr["Material"]);
                        objUI.SerialNo = Convert.ToString(dr["Serial no#"]);
                        objUI.Plnt = Convert.ToString(dr["Plnt"]);

                        string strSLoc = Convert.ToString(dr["SLoc"]);
                        if (strSLoc == "")
                        {
                            objUI.SLoc = Convert.ToString(null);
                        }
                        else
                        {
                            objUI.SLoc = Convert.ToString(dr["SLoc"]); 
                        }
                        objUI.Description = Convert.ToString(dr["Description of technical object"]);
                        objUI.Production_Month = Convert.ToInt16(dr["Production_Month"]);
                        objUI.Production_Month_Year = Convert.ToString(dr["Production_Month_Year"]);
                        //objUI.FromDate = CalProdfromDate.SelectedDate;
                        //objUI.ToDate = CalProdtoDate.SelectedDate;
                        objUI.FromDate = Convert.ToDateTime(dr["FromDate"]);
                        objUI.ToDate = Convert.ToDateTime(dr["ToDate"]);
                        objUI.MonthID = Convert.ToInt16(dr["MonthID"]);
                        objUI.YearID = Convert.ToInt16(dr["YearID"]);
                        objUI.Quarter = Convert.ToString(dr["Quarter"]);


                        if (flag == 1)
                        {
                            objCont.SaveProduction(objUI);
                            rcount++;
                        }
                        else
                        {
                           
                            string strQuery = "Insert into ProductionTemp ([S],[Material],[Serial no#],[Plnt],[SLoc],[Description of technical object],[Production_Month],[Model_Code],[Production_Month_Year],[IsApproved],MonthID,YearID,[Quarter],IsModelEx,FromDate,ToDate) ";
                            strQuery += "Values ('" + dr["S"].ToString() + "','" + dr["Material"].ToString() + "','" + dr["Serial no."].ToString() + "','" + dr["Plnt"].ToString() + "','" + dr["SLoc"].ToString() + "','" + dr["Description of technical object"].ToString() + "','" + dr["Production_Month"].ToString() + "','" + dr["Model_Code"].ToString() + "','" + dr["Production_Month_Year"].ToString() + "',0,'" + dr["MonthID"].ToString() + "','" + dr["YearID"].ToString() + "','" + dr["Quarter"].ToString() + "','" + modelex + "','" + dr["FromDate"].ToString() + "','" + dr["ToDate"].ToString() + "')";
                            objQuerycontroller.ExecuteQuery(strQuery);
                        }
                       
                    }
                    catch
                    {
                       
                        string strQuery = "Insert into ProductionTemp ([S],[Material],[Serial no#],[Plnt],[SLoc],[Description of technical object],[Production_Month],[Model_Code],[Production_Month_Year],[IsApproved],MonthID,YearID,[Quarter],IsModelEx,FromDate,ToDate) ";
                        strQuery += "Values ('" + dr["S"].ToString() + "','" + dr["Material"].ToString() + "','" + dr["Serial no#"].ToString() + "','" + dr["Plnt"].ToString() + "','" + dr["SLoc"].ToString() + "','" + dr["Description of technical object"].ToString() + "','" + dr["Production_Month"].ToString() + "','" + dr["Model_Code"].ToString() + "','" + dr["Production_Month_Year"].ToString() + "',0,'" + dr["MonthID"].ToString() + "','" + dr["YearID"].ToString() + "','" + dr["Quarter"].ToString() + "','" + modelex + "','" + dr["FromDate"].ToString() + "','" + dr["ToDate"].ToString() + "')";
                        objQuerycontroller.ExecuteQuery(strQuery);
                    }

                
        }
     
    }

    public void SaveAcr(DataTable dt)
    {
        DataTable dtTemp = new DataTable();

        DataTable dtModel = new DataTable();
        string strModelQuery = "select * from Model";
        dtModel = objQuerycontroller.ExecuteQuery(strModelQuery);

        DataTable dtView = new DataTable();
        string strViewQuery = "Select distinct * from vwMastersCode";
        dtView = objQuerycontroller.ExecuteQuery(strViewQuery);


        int tempIndex = 0;
        string sConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ToString();


        AcrController objCont = new AcrController();

        int rcount = 0;
        foreach (DataRow dr in dt.Rows)
        {
            string strCause = "item;culprit;cvoice;defect;model";
            int itemex = 1;
            int modelex = 1;
            int culpritex = 1;
            int cvoiceex = 1;
            int defectex = 1;

            AcrUI objUI = new AcrUI();
            try
            {
                int flag = 0;

                for (int i = 0; i < dtView.Rows.Count; i++)
                {
                    if ("item" == dtView.Rows[i]["tablename"].ToString() && dr["ITEM CODE"].ToString() == dtView.Rows[i]["code"].ToString())
                    {
                        objUI.ITEM_CODE = Convert.ToString(dr["ITEM CODE"]);
                        strCause = strCause.Replace("item", "");
                        itemex = 0;
                        flag++;
                    }
                    if ("culprit" == dtView.Rows[i]["tablename"].ToString() && dr["CUL CODE"].ToString() == dtView.Rows[i]["code"].ToString())
                    {
                        string strCUL_CODE = Convert.ToString(dr["CUL CODE"]);
                        if (strCUL_CODE == "")
                        {
                            objUI.CUL_CODE = Convert.ToInt32(null);
                        }
                        else
                        {
                            objUI.CUL_CODE = Convert.ToInt32(dr["CUL CODE"]);
                        }
                        strCause = strCause.Replace(";culprit", "");
                        culpritex = 0;
                        flag++;
                    }
                    if ("customervoice" == dtView.Rows[i]["tablename"].ToString() && dr["CVOICE"].ToString() == dtView.Rows[i]["code"].ToString())
                    {
                        string strCVOICE = Convert.ToString(dr["CVOICE"]);
                        if (strCVOICE == "")
                        {
                            objUI.CVOICE = Convert.ToInt16(null);
                        }
                        else
                        {
                            objUI.CVOICE = Convert.ToInt16(dr["CVOICE"]);
                        }
                        strCause = strCause.Replace(";cvoice", "");
                        cvoiceex = 0;
                        flag++;
                    }
                    if ("defect" == dtView.Rows[i]["tablename"].ToString() && dr["DEF"].ToString() == dtView.Rows[i]["code"].ToString())
                    {
                        string strDEF = Convert.ToString(dr["DEF"]);
                        if (strDEF == "")
                        {
                            objUI.DEF = Convert.ToInt16(null);
                        }
                        else
                        {
                            objUI.DEF = Convert.ToInt16(dr["DEF"]);
                        }
                        strCause = strCause.Replace(";defect", "");
                        defectex = 0;
                        flag++;
                    }
                    if ("model" == dtView.Rows[i]["tablename"].ToString() && dr["Model_Code"].ToString() == dtView.Rows[i]["code"].ToString())
                    {
                        DataView dv = new DataView(dtModel);
                        dv.RowFilter = "Code ='" + dr["Model_Code"].ToString() + "'";
                        DataTable dtModelCode = dv.ToTable();
                        objUI.Model_Code = Convert.ToString(dtModelCode.Rows[0]["Model_Code"]);
                        strCause = strCause.Replace(";model", "");
                        modelex = 0;
                        flag++;
                    }

                }
                objUI.WCDOCNO = Convert.ToDecimal(dr["WCDOCNO"]);
                objUI.DLR_REF = Convert.ToString(dr["DLR_REF"]);
                objUI.TRACTOR_NO = Convert.ToInt64(dr["TRACTOR NO"]);
                objUI.ENGINE_NO = Convert.ToString(dr["ENGINE NO"]);

                string strInsdate = Convert.ToString(dr["INS DATE"]);

                if (strInsdate == "" || strInsdate == "00.00.0000")
                    objUI.INS_DATE = Convert.ToDateTime("1/1/2001");
                else
                    objUI.INS_DATE = Convert.ToDateTime(ConvertDateTime(Convert.ToString(dr["INS DATE"])));

                if (Convert.ToString(dr["DEF DATE"]) == "" || Convert.ToString(dr["DEF DATE"]) == "00.00.0000")
                    objUI.DEF_DATE = Convert.ToDateTime("1/1/2001");
                else
                    objUI.DEF_DATE = Convert.ToDateTime(ConvertDateTime(Convert.ToString(dr["DEF DATE"])));

                if (Convert.ToString(dr["REP DATE"]) == "" || Convert.ToString(dr["REP DATE"]) == "00.00.0000")
                    objUI.REP_DATE = Convert.ToDateTime("1/1/2001");
                else
                    objUI.REP_DATE = Convert.ToDateTime(ConvertDateTime(Convert.ToString(dr["REP DATE"])));

                objUI.HMR = Convert.ToInt16(dr["HMR"]);
                objUI.DLR_CO = Convert.ToString(dr["DLR CO"]);
                objUI.DEALER_NAME = Convert.ToString(dr["DEALER NAME"]);
                objUI.REG = Convert.ToString(dr["REG"]);
                //objUI.CR_DATE = Convert.ToString(dr["CR DATE"]);
                if (Convert.ToString(dr["CR DATE"]) == "" || Convert.ToString(dr["CR DATE"]) == "00.00.0000")
                    objUI.CR_DATE = Convert.ToDateTime("1/1/2001");
                else
                    objUI.CR_DATE = Convert.ToDateTime(ConvertDateTime(Convert.ToString(dr["CR DATE"])));


                objUI.DESCRIPTION = Convert.ToString(dr["DESCRIPTION"]);
                objUI.QUANTITY = Convert.ToInt16(dr["QTY"]);
                objUI.COST = Convert.ToString(dr["COST"]);
                objUI.NDP = Convert.ToDecimal(dr["NDP"]);
                objUI.VALUE = Convert.ToDecimal(dr["VALUE"]);
                objUI.OUTLV = Convert.ToDecimal(dr["OUTLV"]);
                objUI.DT = Convert.ToString(dr["DT"]);


                string strBlank = Convert.ToString(dr["F24"]);
                if (strBlank == "")
                {
                    objUI.BLANK = Convert.ToDouble(null);
                }
                else
                {
                    objUI.BLANK = Convert.ToDouble(dr["F24"]);
                }

                string strCR_AMOUNT = Convert.ToString(dr["CR-AMOUNT"]);
                if (strCR_AMOUNT == "")
                {
                    objUI.CR_AMOUNT = Convert.ToDouble(null);
                }
                else
                {
                    objUI.CR_AMOUNT = Convert.ToDouble(dr["CR-AMOUNT"]);
                }

                string strAUTH_AMOUNT = Convert.ToString(dr["   AUTH AMT"]);
                if (strAUTH_AMOUNT == "")
                {
                    objUI.AUTH_AMOUNT = Convert.ToDouble(null);
                }
                else
                {
                    objUI.AUTH_AMOUNT = Convert.ToDouble(dr["   AUTH AMT"]);
                }

                string strDIFF = Convert.ToString(dr["     DIFF"]);
                if (strDIFF == "")
                {
                    objUI.DIFF = Convert.ToDouble(null);
                }
                else
                {
                    objUI.DIFF = Convert.ToDouble(dr["     DIFF"]);
                }

                objUI.Production_Month = Convert.ToInt16(dr["Production_Month"]);
                //objUI.Model_Code = Convert.ToString(dr["Model_Code"]);
                objUI.Production_Month_Year = Convert.ToString(dr["Production_Month_Year"]);
                objUI.HMR_Range = Convert.ToString(dr["HMR_Range"]);
                //objUI.FromDate =  CalAcrfromDate.SelectedDate;
                objUI.FromDate = Convert.ToDateTime(dr["FromDate"]);
                objUI.ToDate = Convert.ToDateTime(dr["ToDate"]);
                objUI.MonthID = Convert.ToInt16(dr["MonthID"]);
                objUI.YearID = Convert.ToInt16(dr["YearID"]);
                objUI.Quarter = Convert.ToString(dr["Quarter"]);
                objUI.Engine = Convert.ToString(dr["Engine"]);
                objUI.IsEngine = Convert.ToInt16(dr["IsEngine"].ToString());

                if (flag == 5)
                {
                    objCont.SaveAcr(objUI);
                    rcount++;
                }
                else
                {
                    int index = strCause.IndexOf(';');

                    if (index == 0)
                    {
                        strCause = strCause.Remove(0, 1);
                    }

                    string strDescription = dr["DESCRIPTION"].ToString().Replace("'", "");
                    string strIsEngine = dr["IsEngine"].ToString();
                    int IsEngine = 0;
                    if (strIsEngine == "False")
                    {
                        IsEngine = 0;
                    }
                    else
                    {
                        IsEngine = 1;
                    }
                    string strQuery = "Insert into AcrTemp ([WCDOCNO],[DLR_REF],[TRACTOR NO],[ENGINE NO],[INS DATE],[DEF DATE],[REP DATE],[HMR],[DLR CO],[DEALER NAME],[REG],[CR DATE],[ITEM CODE],[DESCRIPTION],[QTY],[COST],[DEF],[NDP],[VALUE],[CVOICE],[OUTLV],[DT],[CUL CODE],[F24],[CR-AMOUNT],[   AUTH AMT],[     DIFF],[Production_Month],[Model_Code],[HMR_Range],[Production_Month_Year],MonthID,YearID,[Quarter],[IsApproved],FromDate,ToDate,Cause,IsItemEx,IsCulpritEx,IsCVoiceEx,IsModelEx,IsDefectEx,Engine,IsEngine) ";
                    strQuery += "Values ('" + dr["WCDOCNO"].ToString() + "','" + dr["DLR_REF"].ToString() + "','" + dr["TRACTOR NO"].ToString() + "','" + dr["ENGINE NO"].ToString() + "','" + dr["INS DATE"].ToString() + "','" + dr["DEF DATE"].ToString() + "','" + dr["REP DATE"].ToString() + "','" + dr["HMR"].ToString() + "','" + dr["DLR CO"].ToString() + "','" + dr["DEALER NAME"].ToString() + "','" + dr["REG"].ToString() + "','" + dr["CR DATE"].ToString() + "','" + dr["ITEM CODE"].ToString() + "','" + strDescription + "','" + dr["QTY"].ToString() + "','" + dr["COST"].ToString() + "','" + dr["DEF"].ToString() + "','" + dr["NDP"].ToString() + "','" + dr["VALUE"].ToString() + "','" + dr["CVOICE"].ToString() + "','" + dr["OUTLV"].ToString() + "','" + dr["DT"].ToString() + "','" + dr["CUL CODE"].ToString() + "','" + dr["F24"].ToString() + "','" + dr["CR-AMOUNT"].ToString() + "','" + dr["   AUTH AMT"].ToString() + "','" + dr["     DIFF"].ToString() + "','" + dr["Production_Month"].ToString() + "','" + dr["Model_Code"].ToString() + "','" + dr["HMR_Range"].ToString() + "','" + dr["Production_Month_Year"].ToString() + "','" + dr["MonthID"].ToString() + "','" + dr["YearID"].ToString() + "','" + dr["Quarter"].ToString() + "',0,'" + Convert.ToDateTime(dr["FromDate"]) + "','" + Convert.ToDateTime(dr["ToDate"]) + "','" + strCause + "'," + itemex + "," + culpritex + "," + cvoiceex + "," + modelex + "," + defectex + ",'" + dr["Engine"].ToString() + "'," + IsEngine + ")";
                    objQuerycontroller.ExecuteQuery(strQuery);
                }

                //lblMessage = "File Acr saved successfully;No of Rows Affected:" + rcount;
            }
            catch
            {
                //lnkExceptions.Visible = true;
                string strDescription = dr["DESCRIPTION"].ToString().Replace("'", "");
                string strIsEngine = dr["IsEngine"].ToString();
                int IsEngine = 0;
                if (strIsEngine == "False")
                {
                    IsEngine = 0;
                }
                else
                {
                    IsEngine = 1;
                }
                string strQuery = "Insert into AcrTemp ([WCDOCNO],[DLR_REF],[TRACTOR NO],[ENGINE NO],[INS DATE],[DEF DATE],[REP DATE],[HMR],[DLR CO],[DEALER NAME],[REG],[CR DATE],[ITEM CODE],[DESCRIPTION],[QTY],[COST],[DEF],[NDP],[VALUE],[CVOICE],[OUTLV],[DT],[CUL CODE],[F24],[CR-AMOUNT],[   AUTH AMT],[     DIFF],[Production_Month],[Model_Code],[HMR_Range],[Production_Month_Year],MonthID,YearID,[Quarter],[IsApproved],FromDate,ToDate,Cause,IsItemEx,IsCulpritEx,IsCVoiceEx,IsModelEx,IsDefectEx,Engine,IsEngine) ";
                strQuery += "Values ('" + dr["WCDOCNO"].ToString() + "','" + dr["DLR_REF"].ToString() + "','" + dr["TRACTOR NO"].ToString() + "','" + dr["ENGINE NO"].ToString() + "','" + dr["INS DATE"].ToString() + "','" + dr["DEF DATE"].ToString() + "','" + dr["REP DATE"].ToString() + "','" + dr["HMR"].ToString() + "','" + dr["DLR CO"].ToString() + "','" + dr["DEALER NAME"].ToString() + "','" + dr["REG"].ToString() + "','" + dr["CR DATE"].ToString() + "','" + dr["ITEM CODE"].ToString() + "','" + strDescription + "','" + dr["QTY"].ToString() + "','" + dr["COST"].ToString() + "','" + dr["DEF"].ToString() + "','" + dr["NDP"].ToString() + "','" + dr["VALUE"].ToString() + "','" + dr["CVOICE"].ToString() + "','" + dr["OUTLV"].ToString() + "','" + dr["DT"].ToString() + "','" + dr["CUL CODE"].ToString() + "','" + dr["F24"].ToString() + "','" + dr["CR-AMOUNT"].ToString() + "','" + dr["   AUTH AMT"].ToString() + "','" + dr["     DIFF"].ToString() + "','" + dr["Production_Month"].ToString() + "','" + dr["Model_Code"].ToString() + "','" + dr["HMR_Range"].ToString() + "','" + dr["Production_Month_Year"].ToString() + "','" + dr["MonthID"].ToString() + "','" + dr["YearID"].ToString() + "','" + dr["Quarter"].ToString() + "',0,'" + Convert.ToDateTime(dr["FromDate"]) + "','" + Convert.ToDateTime(dr["ToDate"]) + "','" + strCause + "'," + itemex + "," + culpritex + "," + cvoiceex + "," + modelex + "," + defectex + ",'" + dr["Engine"].ToString() + "'," + IsEngine + ")";
                objQuerycontroller.ExecuteQuery(strQuery);
            }



        }
        //return lblMessage;
    }
    public string ConvertDateTime(string strDate)
    {
        string strDateTemp = "";
        try
        {
            string[] strDateArray = strDate.Split('.');
            strDateTemp = strDateArray[1] + "/" + strDateArray[0] + "/" + strDateArray[2];

        }
        catch { }
        return strDateTemp;
    }
}
