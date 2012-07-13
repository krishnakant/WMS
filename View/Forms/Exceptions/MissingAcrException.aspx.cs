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

public partial class View_Forms_Exceptions_MissingAcrException : System.Web.UI.Page
{
    
    QueryConroller objQueryController = new QueryConroller();
    public string strProjectName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        strProjectName = System.Configuration.ConfigurationManager.AppSettings["WMSProjectName"];

        //if (Session["ID"] == null)
        //{
        //    Response.Redirect("/SHQ/Login.aspx");
        //}
        if(!IsPostBack)
        {
            string strQuery = "Select [WCDOCNO],[DLR_REF],[TRACTOR NO],[ENGINE NO],	[INS DATE],[DEF DATE],[REP DATE],[HMR],[DLR CO],[DEALER NAME],[REG],[CR DATE],[ITEM CODE],[DESCRIPTION],[QTY],[COST],[DEF],[NDP],[VALUE],[CVOICE],[OUTLV],[DT],[CUL CODE],[F24],[CR-AMOUNT],[   AUTH AMT] as Auth_Amt,[     DIFF] as Diff,[Production_Month],[Model_Code],[HMR_Range],[Production_Month_Year],[IsApproved],[ID],FromDate,ToDate,Cause,IsItemEx,IsDefectEx,IsModelEx,IsCulpritEx,IsCVoiceEx,Engine,IsEngine  from acrtemp where model_code='' or [cul code]='' or def='' or Cvoice='' or [item code]=''";
            DataTable dt = new DataTable();
            dt = objQueryController.ExecuteQuery(strQuery);

            grdAcrException.DataSource = dt;
            grdAcrException.DataBind();
        }
    }

    public string getMonth(int MonthID)
    {
        string strMonth = "";
        if (MonthID == 1)
        {
            strMonth = "Jan";
        }
        else if (MonthID == 2)
        {
            strMonth = "Feb";
        }
        else if (MonthID == 3)
        {
            strMonth = "Mar";
        }
        else if (MonthID == 4)
        {
            strMonth = "Apr";
        }
        else if (MonthID == 5)
        {
            strMonth = "May";
        }
        else if (MonthID == 6)
        {
            strMonth = "Jun";
        }
        else if (MonthID == 7)
        {
            strMonth = "Jul";
        }
        else if (MonthID == 8)
        {
            strMonth = "Aug";
        }
        else if (MonthID == 9)
        {
            strMonth = "Sep";
        }
        else if (MonthID == 10)
        {
            strMonth = "Oct";
        }
        else if (MonthID == 11)
        {
            strMonth = "Nov";
        }
        else if (MonthID == 12)
        {
            strMonth = "Dec";
        }

        return strMonth;
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {

        DataTable dtHMR = new DataTable();
        string strHMRQuery = "select * from HMR";
        dtHMR = objQueryController.ExecuteQuery(strHMRQuery);

        DataTable dtProdMonth = new DataTable();
        string strProdMonthQuery = "select * from ProductionMonth";
        dtProdMonth = objQueryController.ExecuteQuery(strProdMonthQuery);

        DataTable dtView = new DataTable();
        string strViewQuery = "Select distinct * from vwMastersCode";
        dtView = objQueryController.ExecuteQuery(strViewQuery);

        DataTable dtModel = new DataTable();
        string strModelQuery = "select * from Model";
        dtModel = objQueryController.ExecuteQuery(strModelQuery);

        AcrController objCont = new AcrController();
        int rcount = 0;
        foreach (GridViewRow gr in grdAcrException.Rows)
        {
            bool Discard = ((CheckBox)gr.FindControl("chkDiscard")).Checked;
            string str = ((HiddenField)gr.FindControl("hdnID")).Value;
            if (Discard == true)
            {
                string strDiscardQuery = "Delete from AcrTemp where ID=" + Convert.ToInt64(str);
                objQueryController.ExecuteQuery(strDiscardQuery);

                string strQuery = "Select [WCDOCNO],[DLR_REF],[TRACTOR NO],[ENGINE NO],	[INS DATE],[DEF DATE],[REP DATE],[HMR],[DLR CO],[DEALER NAME],[REG],[CR DATE],[ITEM CODE],[DESCRIPTION],[QTY],[COST],[DEF],[NDP],[VALUE],[CVOICE],[OUTLV],[DT],[CUL CODE],[F24],[CR-AMOUNT],[   AUTH AMT] as Auth_Amt,[     DIFF] as Diff,[Production_Month],[Model_Code],[HMR_Range],[Production_Month_Year],[IsApproved],[ID],FromDate,ToDate,Cause,IsItemEx,IsDefectEx,IsModelEx,IsCulpritEx,IsCVoiceEx,Engine,IsEngine  from acrtemp where model_code='' or [cul code]='' or def='' or Cvoice='' or [item code]=''";
                DataTable dt = new DataTable();
                dt = objQueryController.ExecuteQuery(strQuery);

                grdAcrException.DataSource = dt;
                grdAcrException.DataBind();
            }
            else
            {
                string strCause = "item;culprit;cvoice;defect;model";
                    AcrUI objUI = new AcrUI();
                int CurrentYearID = 0;
                int CurrentMonthID = 0;
                string strQuarterYear = "";
                
                    try
                    {
                        objUI.WCDOCNO = Convert.ToDecimal(((TextBox)gr.FindControl("txtWCDOCNO")).Text);
                        objUI.DLR_REF = Convert.ToString(((TextBox)gr.FindControl("txtDlrRef")).Text);
                        objUI.TRACTOR_NO = Convert.ToInt64(((TextBox)gr.FindControl("txtTractorNo")).Text);
                        objUI.ENGINE_NO = Convert.ToString(((TextBox)gr.FindControl("txtEngineNo")).Text);
                        

                        string strInsdate = ((TextBox)gr.FindControl("txtINSDATE")).Text;

                        if (strInsdate == "" || strInsdate == "00.00.0000")
                            objUI.INS_DATE = Convert.ToDateTime("1/1/2001");
                        else
                            objUI.INS_DATE = Convert.ToDateTime(ConvertDateTime(((TextBox)gr.FindControl("txtINSDATE")).Text));

                        if (((TextBox)gr.FindControl("txtDEFDATE")).Text == "" || ((TextBox)gr.FindControl("txtDEFDATE")).Text == "00.00.0000")
                            objUI.DEF_DATE = Convert.ToDateTime("1/1/2001");
                        else
                            objUI.DEF_DATE = Convert.ToDateTime(ConvertDateTime(((TextBox)gr.FindControl("txtDEFDATE")).Text));

                        if (((TextBox)gr.FindControl("txtREPDATE")).Text == "" || ((TextBox)gr.FindControl("txtREPDATE")).Text == "00.00.0000")
                            objUI.REP_DATE = Convert.ToDateTime("1/1/2001");
                        else
                            objUI.REP_DATE = Convert.ToDateTime(ConvertDateTime(((TextBox)gr.FindControl("txtREPDATE")).Text));                           


                        objUI.HMR = Convert.ToInt16(((TextBox)gr.FindControl("txtHMR")).Text);
                        objUI.DLR_CO = Convert.ToString(((TextBox)gr.FindControl("txtDealerCode")).Text);
                        objUI.DEALER_NAME = Convert.ToString(((TextBox)gr.FindControl("txtDealerName")).Text);
                        objUI.REG = Convert.ToString(((TextBox)gr.FindControl("txtREG")).Text);
                        //objUI.CR_DATE = Convert.ToDateTime(((TextBox)gr.FindControl("txtCRDATE")).Text);
                        if (((TextBox)gr.FindControl("txtCRDATE")).Text == "" || ((TextBox)gr.FindControl("txtCRDATE")).Text == "00.00.0000")
                            objUI.CR_DATE = Convert.ToDateTime("1/1/2001");
                        else
                            objUI.CR_DATE = Convert.ToDateTime(ConvertDateTime(((TextBox)gr.FindControl("txtCRDATE")).Text));

                        objUI.DESCRIPTION = Convert.ToString(((TextBox)gr.FindControl("txtDescription")).Text);
                        objUI.QUANTITY = Convert.ToInt16(((TextBox)gr.FindControl("txtQuantity")).Text);
                        objUI.COST = Convert.ToString(((TextBox)gr.FindControl("txtCost")).Text);
                       
                        objUI.NDP = Convert.ToDecimal(((TextBox)gr.FindControl("txtNDP")).Text);
                        objUI.VALUE = Convert.ToDecimal(((TextBox)gr.FindControl("txtVALUE")).Text);

                       
                        objUI.OUTLV = Convert.ToDecimal(((TextBox)gr.FindControl("txtOUTLV")).Text);
                        objUI.DT = Convert.ToString(((TextBox)gr.FindControl("txtDT")).Text);

                       
                        string strBlank = Convert.ToString(((TextBox)gr.FindControl("txtBlank")).Text);
                        if (strBlank == "")
                        {
                            objUI.BLANK = Convert.ToDouble(null);
                        }
                        else
                        {
                            objUI.BLANK = Convert.ToDouble(((TextBox)gr.FindControl("txtBlank")).Text);
                        }

                        string strCR_AMOUNT = Convert.ToString(((TextBox)gr.FindControl("txtCRAMT")).Text);
                        if (strCR_AMOUNT == "")
                        {
                            objUI.CR_AMOUNT = Convert.ToDouble(null);
                        }
                        else
                        {
                            objUI.CR_AMOUNT = Convert.ToDouble(((TextBox)gr.FindControl("txtCRAMT")).Text);
                        }

                        string strAUTH_AMOUNT = Convert.ToString(((TextBox)gr.FindControl("txtAUTHAMT")).Text);
                        if (strAUTH_AMOUNT == "")
                        {
                            objUI.AUTH_AMOUNT = Convert.ToDouble(null);
                        }
                        else
                        {
                            objUI.AUTH_AMOUNT = Convert.ToDouble(((TextBox)gr.FindControl("txtAUTHAMT")).Text);
                        }

                        string strDIFF = Convert.ToString(((TextBox)gr.FindControl("txtDIFF")).Text);
                        if (strDIFF == "")
                        {
                            objUI.DIFF = Convert.ToDouble(null);
                        }
                        else
                        {
                            objUI.DIFF = Convert.ToDouble(((TextBox)gr.FindControl("txtDIFF")).Text);
                        }

                       try
                            {
                                int HMR = Convert.ToInt16(((TextBox)gr.FindControl("txtHMR")).Text);

                                foreach (DataRow drHMR in dtHMR.Rows)
                                {
                                    int MinHMR = Convert.ToInt16(drHMR["Min_Value"]);    //Get the minimum HMR value from HMR Table
                                    int MaxHMR = Convert.ToInt16(drHMR["Max_Value"]);    //Get the maximum HMR value from HMR Table

                                    if (HMR >= MinHMR && HMR <= MaxHMR)                  //Check HMR Range
                                    {
                                        objUI.HMR_Range = Convert.ToString(drHMR["HMR_Range"]);
                                    }


                                }
                            }
                            catch { }
                            string strTractorNo = Convert.ToString(((TextBox)gr.FindControl("txtTractorNo")).Text);
                            int tractornoLength = strTractorNo.Length;
                            string ProductionMonth = "";
                            string ModelCode = "0";

                            if (tractornoLength == 11)                            //If Tractor No Length is 11
                            {
                                ProductionMonth = strTractorNo.Substring(1, 2);   // 2nd and 3rd digit is Production Month
                                ModelCode = strTractorNo.Substring(3, 3);         // The next 3 digits i.e. 4th, 5th and 6th digits is Model Code
                            }
                            else if (tractornoLength == 12)                       //If Tractor No Length is 12
                            {
                                ProductionMonth = strTractorNo.Substring(1, 3);   // 2nd to 4th digit is Production Month
                                ModelCode = strTractorNo.Substring(4, 3);         // The next 3 digits i.e. 5th, 6th and 7th digits is Model Code
                            }
                        
                            //DataView dv = new DataView(dtModel);
                            //dv.RowFilter = "Code =" + Convert.ToInt16(ModelCode);
                            //DataTable dtModelCode = dv.ToTable();
                            objUI.Production_Month = Convert.ToInt16(ProductionMonth);
                            objUI.Model_Code = Convert.ToString(ModelCode);
                                 //Convert.ToString(dtModelCode.Rows[0]["Model_Code"]);
                            
                            

                            if (ProductionMonth != "")
                            {
                                int BaseProductionMonth = Convert.ToInt16(dtProdMonth.Rows[0]["BaseProductionMonth_Code"]);
                                int BaseMonthID = Convert.ToInt16(dtProdMonth.Rows[0]["Month_ID"]);
                                int BaseYearID = Convert.ToInt16(dtProdMonth.Rows[0]["Year_ID"]);

                                string strBaseDate = Convert.ToString(BaseMonthID) + "/1/" + BaseYearID;
                            DateTime BaseDate = Convert.ToDateTime(strBaseDate);
                            int Offset = Convert.ToInt16(ProductionMonth) - BaseProductionMonth;
                            DateTime ProdMonthYear = BaseDate.AddMonths(Offset);
                            
                            CurrentYearID = ProdMonthYear.Year;
                            CurrentMonthID = ProdMonthYear.Month;

                            string strCurrentYearID = (Convert.ToString(CurrentYearID)).Substring(2,2);

                            string strMonth = getMonth(CurrentMonthID);

                            string strProductionMonthYear = strMonth + "-" + strCurrentYearID;
                            
                            objUI.MonthID = CurrentMonthID;
                            objUI.YearID = CurrentYearID;
                            string strQuarter = "";

                            if (CurrentMonthID == 4 || CurrentMonthID == 5 || CurrentMonthID == 6)
                            {
                                strQuarter = "Q1";
                            }
                            else if (CurrentMonthID == 7 || CurrentMonthID == 8 || CurrentMonthID == 9)
                            {
                                strQuarter = "Q2";
                            }
                            else if (CurrentMonthID == 10 || CurrentMonthID == 11 || CurrentMonthID == 12)
                            {
                                strQuarter = "Q3";
                            }
                            else if (CurrentMonthID == 1 || CurrentMonthID == 2 || CurrentMonthID == 3)
                            {
                                strQuarter = "Q4";
                            }

                            

                            if (CurrentMonthID < 4)
                            {
                                string strPreviousYearID = (Convert.ToString(CurrentYearID - 1)).Substring(2, 2);
                                strCurrentYearID = strCurrentYearID.Replace("0", "");
                                strQuarterYear = strPreviousYearID + strCurrentYearID + strQuarter;
                            }
                            else
                            {
                                string strNextYearID = (Convert.ToString(CurrentYearID + 1)).Substring(2, 2);
                                strNextYearID = strNextYearID.Replace("0", "");
                                strQuarterYear = strCurrentYearID + strNextYearID + strQuarter;
                            }

                                objUI.Quarter = strQuarterYear;
                                objUI.Production_Month_Year = strProductionMonthYear;
                            }
                               objUI.FromDate = Convert.ToDateTime(((Label)gr.FindControl("lblFromDate")).Text);
                               objUI.ToDate = Convert.ToDateTime(((Label)gr.FindControl("lblToDate")).Text);
                               objUI.Engine = Convert.ToString(((HiddenField)gr.FindControl("hdnEngine")).Value);
                               objUI.IsEngine = Convert.ToInt16(((HiddenField)gr.FindControl("hdnIsEngine")).Value);

                           
                            int flag = 0;

                            for (int i = 0; i < dtView.Rows.Count; i++)
                            {
                                if ("item" == dtView.Rows[i]["tablename"].ToString() && ((TextBox)gr.FindControl("txtItemCode")).Text == dtView.Rows[i]["code"].ToString())
                                {
                                    objUI.ITEM_CODE = Convert.ToString(((TextBox)gr.FindControl("txtItemCode")).Text);
                                    strCause = strCause.Replace("item", "");
                                    flag++;
                                }
                                if ("culprit" == dtView.Rows[i]["tablename"].ToString() && ((TextBox)gr.FindControl("txtCulCode")).Text == dtView.Rows[i]["code"].ToString())
                                {
                                    string strCUL_CODE = Convert.ToString(((TextBox)gr.FindControl("txtCulCode")).Text);
                                    if (strCUL_CODE == "")
                                    {
                                        objUI.CUL_CODE = Convert.ToInt32(null);
                                    }
                                    else
                                    {
                                        objUI.CUL_CODE = Convert.ToInt32(((TextBox)gr.FindControl("txtCulCode")).Text);
                                    }
                                    strCause = strCause.Replace(";culprit", "");
                                    flag++;
                                }
                                if ("customervoice" == dtView.Rows[i]["tablename"].ToString() && ((TextBox)gr.FindControl("txtCVOICE")).Text == dtView.Rows[i]["code"].ToString())
                                {
                                    string strCVOICE = Convert.ToString(((TextBox)gr.FindControl("txtCVOICE")).Text);
                                    if (strCVOICE == "")
                                    {
                                        objUI.CVOICE = Convert.ToInt16(null);
                                    }
                                    else
                                    {
                                        objUI.CVOICE = Convert.ToInt16(((TextBox)gr.FindControl("txtCVOICE")).Text);
                                    }
                                    strCause = strCause.Replace(";cvoice", "");
                                    flag++;
                                }
                                if ("defect" == dtView.Rows[i]["tablename"].ToString() && ((TextBox)gr.FindControl("txtDEF")).Text == dtView.Rows[i]["code"].ToString())
                                {
                                    string strDEF = Convert.ToString(((TextBox)gr.FindControl("txtDEF")).Text);
                                    if (strDEF == "")
                                    {
                                        objUI.DEF = Convert.ToInt16(null);
                                    }
                                    else
                                    {
                                        objUI.DEF = Convert.ToInt16(((TextBox)gr.FindControl("txtDEF")).Text);
                                    }
                                    strCause = strCause.Replace(";defect", "");
                                    flag++;
                                }
                                if ("model" == dtView.Rows[i]["tablename"].ToString() && Convert.ToString(ModelCode) == dtView.Rows[i]["code"].ToString())
                                {
                                    DataView dv = new DataView(dtModel);
                                    dv.RowFilter = "Code ='" + Convert.ToString(ModelCode) + "'";
                                    DataTable dtModelCode = dv.ToTable();
                                    objUI.Model_Code = Convert.ToString(dtModelCode.Rows[0]["Model_Code"]);
                                    strCause = strCause.Replace(";model", "");
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
                                string strEngine = ((HiddenField)gr.FindControl("hdnEngine")).Value;
                                string strIsEngine = Convert.ToString(((HiddenField)gr.FindControl("hdnIsEngine")).Value);
                                int IsEngine = 0;
                                if (strIsEngine == "False")
                                {
                                    IsEngine = 0;
                                }
                                else
                                {
                                    IsEngine = 1;
                                }
                                string strDescription = ((TextBox)gr.FindControl("txtDescription")).Text.Replace("'", "");
                                string strQuery = "Insert into AcrTemp ([WCDOCNO],[DLR_REF],[TRACTOR NO],[ENGINE NO],[INS DATE],[DEF DATE],[REP DATE],[HMR],[DLR CO],[DEALER NAME],[REG],[CR DATE],[ITEM CODE],[DESCRIPTION],[QTY],[COST],[DEF],[NDP],[VALUE],[CVOICE],[OUTLV],[DT],[CUL CODE],[F24],[CR-AMOUNT],[   AUTH AMT],[     DIFF],FromDate,ToDate,MonthID,YearID,Quarter,Engine,IsEngine) ";
                                strQuery += "Values ('" + ((TextBox)gr.FindControl("txtWCDOCNO")).Text + "','" + ((TextBox)gr.FindControl("txtDlrRef")).Text + "','" + ((TextBox)gr.FindControl("txtTractorNo")).Text + "','" + ((TextBox)gr.FindControl("txtEngineNo")).Text + "','" + ((TextBox)gr.FindControl("txtINSDATE")).Text + "','" + ((TextBox)gr.FindControl("txtDEFDATE")).Text + "','" + ((TextBox)gr.FindControl("txtREPDATE")).Text + "','" + ((TextBox)gr.FindControl("txtHMR")).Text + "','" + ((TextBox)gr.FindControl("txtDealerCode")).Text + "','" + ((TextBox)gr.FindControl("txtDealerName")).Text + "','" + ((TextBox)gr.FindControl("txtREG")).Text + "','" + ((TextBox)gr.FindControl("txtCRDATE")).Text + "','" + ((TextBox)gr.FindControl("txtItemCode")).Text + "','" + strDescription + "','" + ((TextBox)gr.FindControl("txtQuantity")).Text + "','" + ((TextBox)gr.FindControl("txtCost")).Text + "','" + ((TextBox)gr.FindControl("txtDEF")).Text + "','" + ((TextBox)gr.FindControl("txtNDP")).Text + "','" + ((TextBox)gr.FindControl("txtVALUE")).Text + "','" + ((TextBox)gr.FindControl("txtCVOICE")).Text + "','" + ((TextBox)gr.FindControl("txtOUTLV")).Text + "','" + ((TextBox)gr.FindControl("txtDT")).Text + "','" + ((TextBox)gr.FindControl("txtCulCode")).Text + "','" + ((TextBox)gr.FindControl("txtBlank")).Text + "','" + ((TextBox)gr.FindControl("txtCRAMT")).Text + "','" + ((TextBox)gr.FindControl("txtAUTHAMT")).Text + "','" + ((TextBox)gr.FindControl("txtDIFF")).Text + "','" + ((Label)gr.FindControl("lblFromDate")).Text + "','" + ((Label)gr.FindControl("lblToDate")).Text + "','"+ CurrentMonthID +"','"+ CurrentYearID +"','"+strQuarterYear+"','"+ strEngine +"',"+ IsEngine +")";
                                objQueryController.ExecuteQuery(strQuery); 
                            }
                                           
                    //    lblMessage = "File Acr saved successfully;No of Rows Affected:" + rcount;
                    }
                    catch
                    {
                        //lnkExceptions.Visible = true;
                        int index = strCause.IndexOf(';');

                        if (index == 0)
                        {
                            strCause = strCause.Remove(0, 1);
                        }
                        
                            string strEngine = ((HiddenField)gr.FindControl("hdnEngine")).Value;
                            string strIsEngine = Convert.ToString(((HiddenField)gr.FindControl("hdnIsEngine")).Value);
                            int IsEngine = 0;
                            if (strIsEngine == "False")
                            {
                                IsEngine = 0;
                            }
                            else
                            {
                                IsEngine = 1;
                            }
                            if (strIsEngine != "")
                            {
                                string strDescription = ((TextBox)gr.FindControl("txtDescription")).Text.Replace("'", "");
                                string strQuery = "Insert into AcrTemp ([WCDOCNO],[DLR_REF],[TRACTOR NO],[ENGINE NO],[INS DATE],[DEF DATE],[REP DATE],[HMR],[DLR CO],[DEALER NAME],[REG],[CR DATE],[ITEM CODE],[DESCRIPTION],[QTY],[COST],[DEF],[NDP],[VALUE],[CVOICE],[OUTLV],[DT],[CUL CODE],[F24],[CR-AMOUNT],[   AUTH AMT],[     DIFF],FromDate,ToDate,MonthID,YearID,Quarter,Engine,IsEngine)";
                                strQuery += "Values ('" + ((TextBox)gr.FindControl("txtWCDOCNO")).Text + "','" + ((TextBox)gr.FindControl("txtDlrRef")).Text + "','" + ((TextBox)gr.FindControl("txtTractorNo")).Text + "','" + ((TextBox)gr.FindControl("txtEngineNo")).Text + "','" + ((TextBox)gr.FindControl("txtINSDATE")).Text + "','" + ((TextBox)gr.FindControl("txtDEFDATE")).Text + "','" + ((TextBox)gr.FindControl("txtREPDATE")).Text + "','" + ((TextBox)gr.FindControl("txtHMR")).Text + "','" + ((TextBox)gr.FindControl("txtDealerCode")).Text + "','" + ((TextBox)gr.FindControl("txtDealerName")).Text + "','" + ((TextBox)gr.FindControl("txtREG")).Text + "','" + ((TextBox)gr.FindControl("txtCRDATE")).Text + "','" + ((TextBox)gr.FindControl("txtItemCode")).Text + "','" + strDescription + "','" + ((TextBox)gr.FindControl("txtQuantity")).Text + "','" + ((TextBox)gr.FindControl("txtCost")).Text + "','" + ((TextBox)gr.FindControl("txtDEF")).Text + "','" + ((TextBox)gr.FindControl("txtNDP")).Text + "','" + ((TextBox)gr.FindControl("txtVALUE")).Text + "','" + ((TextBox)gr.FindControl("txtCVOICE")).Text + "','" + ((TextBox)gr.FindControl("txtOUTLV")).Text + "','" + ((TextBox)gr.FindControl("txtDT")).Text + "','" + ((TextBox)gr.FindControl("txtCulCode")).Text + "','" + ((TextBox)gr.FindControl("txtBlank")).Text + "','" + ((TextBox)gr.FindControl("txtCRAMT")).Text + "','" + ((TextBox)gr.FindControl("txtAUTHAMT")).Text + "','" + ((TextBox)gr.FindControl("txtDIFF")).Text + "','" + ((Label)gr.FindControl("lblFromDate")).Text + "','" + ((Label)gr.FindControl("lblToDate")).Text + "','" + CurrentMonthID + "','" + CurrentYearID + "','" + strQuarterYear + "','" + strEngine + "'," + IsEngine + ")";
                                objQueryController.ExecuteQuery(strQuery);
                            }
                       
                    }

                    string strDeleteQuery = "Delete from AcrTemp where ID=" + Convert.ToInt32(str);
                    objQueryController.ExecuteQuery(strDeleteQuery);
                    grdAcrException.DataBind();
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
