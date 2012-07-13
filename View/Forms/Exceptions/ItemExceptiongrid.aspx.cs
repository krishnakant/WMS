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
    QueryConroller objQueryController = new QueryConroller();
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            BindGrid();
        }
    }

    public void BindGrid()
    {
        string strQuery = "select [Item Code] as Code,[ID] from acrtemp where IsItemEx=1 and [Item Code]<>''";
        DataTable dt = objQueryController.ExecuteQuery(strQuery);
        grdItemException.DataSource = dt;
        grdItemException.DataBind();
    }

    public void UpdateItem(object sender, EventArgs e)
    {
        MastersController objController = new MastersController();
        ItemUI objUI = new ItemUI();
        int Code = 0;
        string strItemGroupName = "";
        int GroupID = 0;
        int index = Convert.ToInt16(hdnIndex.Value);
       // string strQuery = "";
        int ID = Convert.ToInt32(((HiddenField)grdItemException.Rows[index].FindControl("hdnID")).Value);
        string strCode = ((Label)grdItemException.Rows[index].FindControl("lblCode")).Text;
        if (strCode == "")
        {
            Code = 0;
        }
        else
        {
            Code = Convert.ToInt32(strCode);
        }

        int selected = ((RadioButtonList)grdItemException.Rows[index].FindControl("rdoMode")).SelectedIndex;
        if (selected == 0)
        {
            strItemGroupName = ((DropDownList)grdItemException.Rows[index].FindControl("drpItem")).SelectedItem.Text;
            GroupID = Convert.ToInt16(((DropDownList)grdItemException.Rows[index].FindControl("drpItem")).SelectedValue);
        }
        else if (selected == 1)
        {
            strItemGroupName = ((TextBox)grdItemException.Rows[index].FindControl("txtItem")).Text;
        }
        objUI.ItemCode = Code;
        objUI.ItemGroupName = strItemGroupName;
        objUI.GroupID = GroupID;
        string strMessage = "";
        //strQuery = "Insert into Model (Code,Model_Code) values (" + Code + ",'" + strModelCode + "')";
        try
        {
            objController.SaveItem(objUI);
            string strQuery = "Select * from AcrTemp where IsModelEx=0 and IsItemEx=0 and IsCulpritEx=0 and IsCVoiceEx=0 and IsDefectEx=0";
            DataTable dt = objQueryController.ExecuteQuery(strQuery);
            if (dt != null)
            {
                SaveAcr(dt);
                string strDeleteQuery = "Delete from AcrTemp where IsModelEx=0 and IsItemEx=0 and IsCulpritEx=0 and IsCVoiceEx=0 and IsDefectEx=0";
                objQueryController.ExecuteQuery(strDeleteQuery);
                BindGrid();
            }
            strMessage = "Records updated successfully";
        }
        catch
        {
            strMessage = "Records could not be updated successfully";
        }
        string strjscript = "<script language='javascript'>";
        strjscript += " setMessageText('ctl00_ContentPlaceHolder1_lblMessage','" + strMessage + "' );";
        strjscript += "</script" + ">";
        Literal1.Text = strjscript;

    }

    public void SaveAcr(DataTable dt)
    {
        DataTable dtTemp = new DataTable();

        DataTable dtModel = new DataTable();
        string strModelQuery = "select * from Model";
        dtModel = objQueryController.ExecuteQuery(strModelQuery);

        DataTable dtView = new DataTable();
        string strViewQuery = "Select distinct * from vwMastersCode";
        dtView = objQueryController.ExecuteQuery(strViewQuery);
             
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

                        objUI.WCDOCNO = Convert.ToDecimal(dr["WCDOCNO"]);
                        objUI.DLR_REF = Convert.ToString(dr["DLR_REF"]);
                        objUI.TRACTOR_NO = Convert.ToInt64(dr["TRACTOR NO"]);
                        objUI.ENGINE_NO = Convert.ToString(dr["ENGINE NO"]);
                        //objUI.INS_DATE = Convert.ToString(dr["INS DATE"]);
                        //objUI.DEF_DATE = Convert.ToString(dr["DEF DATE"]);
                        //objUI.REP_DATE = Convert.ToString(dr["REP DATE"]);
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
                        objUI.FromDate = Convert.ToDateTime(dr["FromDate"]);
                        objUI.ToDate = Convert.ToDateTime(dr["ToDate"]);

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
                            string strQuery = "Insert into AcrTemp ([WCDOCNO],[DLR_REF],[TRACTOR NO],[ENGINE NO],[INS DATE],[DEF DATE],[REP DATE],[HMR],[DLR CO],[DEALER NAME],[REG],[CR DATE],[ITEM CODE],[DESCRIPTION],[QTY],[COST],[DEF],[NDP],[VALUE],[CVOICE],[OUTLV],[DT],[CUL CODE],[F24],[CR-AMOUNT],[   AUTH AMT],[     DIFF],[Production_Month],[Model_Code],[HMR_Range],[Production_Month_Year],[IsApproved],FromDate,ToDate,Cause,IsItemEx,IsCulpritEx,IsCVoiceEx,IsModelEx,IsDefectEx) ";
                            strQuery += "Values ('" + dr["WCDOCNO"].ToString() + "','" + dr["DLR_REF"].ToString() + "','" + dr["TRACTOR NO"].ToString() + "','" + dr["ENGINE NO"].ToString() + "','" + dr["INS DATE"].ToString() + "','" + dr["DEF DATE"].ToString() + "','" + dr["REP DATE"].ToString() + "','" + dr["HMR"].ToString() + "','" + dr["DLR CO"].ToString() + "','" + dr["DEALER NAME"].ToString() + "','" + dr["REG"].ToString() + "','" + dr["CR DATE"].ToString() + "','" + dr["ITEM CODE"].ToString() + "','" + strDescription + "','" + dr["QTY"].ToString() + "','" + dr["COST"].ToString() + "','" + dr["DEF"].ToString() + "','" + dr["NDP"].ToString() + "','" + dr["VALUE"].ToString() + "','" + dr["CVOICE"].ToString() + "','" + dr["OUTLV"].ToString() + "','" + dr["DT"].ToString() + "','" + dr["CUL CODE"].ToString() + "','" + dr["F24"].ToString() + "','" + dr["CR-AMOUNT"].ToString() + "','" + dr["   AUTH AMT"].ToString() + "','" + dr["     DIFF"].ToString() + "','" + dr["Production_Month"].ToString() + "','" + dr["Model_Code"].ToString() + "','" + dr["HMR_Range"].ToString() + "','" + dr["Production_Month_Year"].ToString() + "',0,'" + dr["FromDate"].ToString() +"','" + dr["ToDate"].ToString()+ "','" + strCause + "'," + itemex + "," + culpritex + "," + cvoiceex + "," + modelex + "," + defectex + ")";
                            objQueryController.ExecuteQuery(strQuery);
                        }

                        
                    }
                    catch
                    {
                        
                        string strDescription = dr["DESCRIPTION"].ToString().Replace("'", "");
                        string strQuery = "Insert into AcrTemp ([WCDOCNO],[DLR_REF],[TRACTOR NO],[ENGINE NO],[INS DATE],[DEF DATE],[REP DATE],[HMR],[DLR CO],[DEALER NAME],[REG],[CR DATE],[ITEM CODE],[DESCRIPTION],[QTY],[COST],[DEF],[NDP],[VALUE],[CVOICE],[OUTLV],[DT],[CUL CODE],[F24],[CR-AMOUNT],[   AUTH AMT],[     DIFF],[Production_Month],[Model_Code],[HMR_Range],[Production_Month_Year],[IsApproved],FromDate,ToDate,Cause,IsItemEx,IsCulpritEx,IsCVoiceEx,IsModelEx,IsDefectEx) ";
                        strQuery += "Values ('" + dr["WCDOCNO"].ToString() + "','" + dr["DLR_REF"].ToString() + "','" + dr["TRACTOR NO"].ToString() + "','" + dr["ENGINE NO"].ToString() + "','" + dr["INS DATE"].ToString() + "','" + dr["DEF DATE"].ToString() + "','" + dr["REP DATE"].ToString() + "','" + dr["HMR"].ToString() + "','" + dr["DLR CO"].ToString() + "','" + dr["DEALER NAME"].ToString() + "','" + dr["REG"].ToString() + "','" + dr["CR DATE"].ToString() + "','" + dr["ITEM CODE"].ToString() + "','" + strDescription + "','" + dr["QTY"].ToString() + "','" + dr["COST"].ToString() + "','" + dr["DEF"].ToString() + "','" + dr["NDP"].ToString() + "','" + dr["VALUE"].ToString() + "','" + dr["CVOICE"].ToString() + "','" + dr["OUTLV"].ToString() + "','" + dr["DT"].ToString() + "','" + dr["CUL CODE"].ToString() + "','" + dr["F24"].ToString() + "','" + dr["CR-AMOUNT"].ToString() + "','" + dr["   AUTH AMT"].ToString() + "','" + dr["     DIFF"].ToString() + "','" + dr["Production_Month"].ToString() + "','" + dr["Model_Code"].ToString() + "','" + dr["HMR_Range"].ToString() + "','" + dr["Production_Month_Year"].ToString() + "',0,'" + dr["FromDate"].ToString() + "','" + dr["ToDate"].ToString() + "','" + strCause + "'," + itemex + "," + culpritex + "," + cvoiceex + "," + modelex + "," + defectex + ")";
                        objQueryController.ExecuteQuery(strQuery);
                    }

                
        }
       
    }

    /***********************************Function to Convert Date Time Format***********************************/
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
