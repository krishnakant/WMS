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

public partial class View_Forms_Exceptions_ItemException : System.Web.UI.Page
{
    QueryConroller objQuerycontroller = new QueryConroller();
    ConfiguratorController objController = new ConfiguratorController();
    Utility objUtility = new Utility();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getItemCodeDetail();
            getItemCodeGroup();
        }
    }
    public void getItemCodeDetail()
    {

        string strQuery = "";
        DataTable dtinformation = new DataTable();
        strQuery = "select distinct [Item Code] as Code,(Convert(varchar(20),[Item Code])+'('+Description+')')as Code_Description from acrbulktemp where IsItemEx=1 and [Item Code]<>''";
        dtinformation = objQuerycontroller.ExecuteQuery(strQuery);
        if (dtinformation != null)
        {

            if (dtinformation.Rows.Count > 0)
            {
                chkItemCodeList.DataSource = dtinformation;
                chkItemCodeList.DataValueField = "Code";
                chkItemCodeList.DataTextField = "Code_Description";
                chkItemCodeList.DataBind();

                btnSave.Enabled = true;
            }
            else
            {
                btnSave.Enabled = false;
            }
        }
        else
        {
            btnSave.Enabled = false;
        }
    }
    public void getItemCodeGroup()
    {

        string strQuery = "";
        DataTable dtinformation = new DataTable();
        strQuery = "select * from ItemGRoup order by ItemGRoupName";
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
    public int SaveItemGroupName()
    {
        int GroupID = 0;
        ConfiguratorUI objUI = new ConfiguratorUI();
       
        objUI.GroupName = txtGroupName.Text.Trim();
        GroupID=objController.SaveGroup(objUI, null);
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
            GroupID=SaveItemGroupName();
        }
        if (rdoExixts.Checked)
        {
            GroupID = Convert.ToInt32(ddlItemGRoupList.SelectedValue);
        }
        if (GroupID > 0)
        {
            ConfiguratorUI objUI = new ConfiguratorUI();
            foreach (ListItem list in chkItemCodeList.Items)
            {
                if (list.Selected)
                {
                    objUI.Code = list.Value.Trim();
                    objUI.GroupID = GroupID;
                    objUI.source = "Exception";
                    objController.SaveItemGroupMapping(objUI, null);
                }
            }
            //string strQuery = "Select * from AcrTemp where IsModelEx=0 and IsItemEx=0 and IsCulpritEx=0 and IsCVoiceEx=0 and IsDefectEx=0";
            //DataTable dt = objQuerycontroller.ExecuteQuery(strQuery);
            //if (dt != null)
            //{
            //    SaveAcr(dt);
            //    string strDeleteQuery = "Delete from AcrTemp where IsModelEx=0 and IsItemEx=0 and IsCulpritEx=0 and IsCVoiceEx=0 and IsDefectEx=0";
            //    objQuerycontroller.ExecuteQuery(strDeleteQuery);
                
            //}
            
            SaveBulkAcr();
            Response.Redirect("/WMS/View/Forms/Exceptions/ItemException.aspx");
        }
    }

    public string SaveBulkAcr()
    {
        AcrController objAcrController = new AcrController();
        string sConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        string lblMessage = "";
        try
        {

            int Status = objAcrController.SaveBulkAcr();
            string strQuery = "Update AcrBulk set ModelMappingID = Production.ModelMappingID  from AcrBulk inner join Production on Convert(varchar,AcrBulk.Tractor_No) = Production.SerialNo"; 
            //string strQuery = "UPDATE AcrBulk SET ModelMappingID =( SELECT Production.ModelMappingID FROM Production WHERE Production.SerialNo = Convert(varchar,AcrBulk.Tractor_No)) WHERE EXISTS ( SELECT Production.ModelMappingID FROM Production WHERE Production.SerialNo = Convert(varchar,AcrBulk.Tractor_No))";
            objQuerycontroller.ExecuteQuery(strQuery);
            if (Status == 0)
            {
                lblMessage = "File Acr saved successfully";
            }
            else
            {
                lblMessage = "File Acr could not be saved successfully";
            }
        }
        catch
        {
            lblMessage = "File Acr could not be saved successfully";
        }
        return lblMessage;
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
                            strQuery += "Values ('" + dr["WCDOCNO"].ToString() + "','" + dr["DLR_REF"].ToString() + "','" + dr["TRACTOR NO"].ToString() + "','" + dr["ENGINE NO"].ToString() + "','" + dr["INS DATE"].ToString() + "','" + dr["DEF DATE"].ToString() + "','" + dr["REP DATE"].ToString() + "','" + dr["HMR"].ToString() + "','" + dr["DLR CO"].ToString() + "','" + dr["DEALER NAME"].ToString() + "','" + dr["REG"].ToString() + "','" + dr["CR DATE"].ToString() + "','" + dr["ITEM CODE"].ToString() + "','" + strDescription + "','" + dr["QTY"].ToString() + "','" + dr["COST"].ToString() + "','" + dr["DEF"].ToString() + "','" + dr["NDP"].ToString() + "','" + dr["VALUE"].ToString() + "','" + dr["CVOICE"].ToString() + "','" + dr["OUTLV"].ToString() + "','" + dr["DT"].ToString() + "','" + dr["CUL CODE"].ToString() + "','" + dr["F24"].ToString() + "','" + dr["CR-AMOUNT"].ToString() + "','" + dr["   AUTH AMT"].ToString() + "','" + dr["     DIFF"].ToString() + "','" + dr["Production_Month"].ToString() + "','" + dr["Model_Code"].ToString() + "','" + dr["HMR_Range"].ToString() + "','" + dr["Production_Month_Year"].ToString() + "','" + dr["MonthID"].ToString() + "','" + dr["YearID"].ToString() + "','" + dr["Quarter"].ToString() + "',0,'" + Convert.ToDateTime(dr["FromDate"]) +"','" + Convert.ToDateTime(dr["ToDate"]) +"','" + strCause + "'," + itemex + "," + culpritex + "," + cvoiceex + "," + modelex + "," + defectex + ",'" + dr["Engine"].ToString() + "'," + IsEngine + ")";
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
}
