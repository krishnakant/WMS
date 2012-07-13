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
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Office.Core;
using System.IO;

public partial class View_Forms_Master_FileImport : System.Web.UI.Page
{
    QueryConroller objController = new QueryConroller();
    DataAccessLayer objDataAccessLayer = new DataAccessLayer();
    Utility objUtility = new Utility();
    public string strProjectName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        strProjectName = System.Configuration.ConfigurationManager.AppSettings["WMSProjectName"];
               
            DataTable dtControls = new DataTable();
            string strQuery = "Select * from Label_Data where Form='FileImport'";
            dtControls = objController.ExecuteQuery(strQuery);
            foreach (DataRow dr in dtControls.Rows)
            {
                string strLabel = dr["Label_ID"].ToString();
                string strLabelText = dr["Text"].ToString();

                if (strLabel == "lblFileAcr")
                {
                    lblFileAcr.Text = strLabelText;
                }
                else if (strLabel == "lblFileSales")
                {
                    lblFileSales.Text = strLabelText;
                }
                else if (strLabel == "lblFileProd")
                {
                    lblFileProd.Text = strLabelText;
                }
                else if (strLabel == "lblFileItem")
                {
                    lblFileItem.Text = strLabelText;
                }
                else if (strLabel == "lblFileCulprit")
                {
                    lblFileCulprit.Text = strLabelText;
                }
                else if (strLabel == "lblFileCustVoice")
                {
                    lblFileCustVoice.Text = strLabelText;
                }
                else if (strLabel == "lblFileDefect")
                {
                    lblFileDefect.Text = strLabelText;
                }

            }
        
    }
    public string Path = "";
    public string FileName = "";
    public void AlertMessage(string Message)
    {
        string msgBody = "<script>alert('" + Message + "');</script>";
        Response.Write(msgBody);
    }
    public void Importfile(FileUpload fldReport, string tblName, string EDate)
    {
        string strPath = Convert.ToString(ConfigurationManager.AppSettings["WMSProjectName"].ToString());

        if (fldReport.HasFile == true)
        {
            string FName = fldReport.PostedFile.FileName;
            string str = FName.Substring(FName.LastIndexOf("\\") + 1);
            object FileName = str;
            strPath = Server.MapPath(strPath);
            Path = strPath + "\\UploadFile\\" + str;
            fldReport.SaveAs(Path);
        }
        else
        {
            //AlertMessage("No Such File Found !") 
        }
        string sConnectionString = "";
        string InitialCatalog = "";
        //Dim dtApplicationSettings As DataTable = GetApplicationSettings() 
        DataTable dtExcelData = new DataTable();
        string filePath = "";
        if (fldReport.HasFile)
        {
            string FName = fldReport.PostedFile.FileName;
            string str = FName.Substring(FName.LastIndexOf("\\") + 1);
            FileName = str;
            filePath = FName;
            sConnectionString = ConfigurationManager.AppSettings["connectionString"];
            Utility objUtility = new Utility();
            int result = objUtility.getExcelDatatosql(Path, tblName, sConnectionString, EDate, null);
            if (result == 1)
            {
                AlertMessage("File Imported successfully");
            }
            //Response.Redirect("/QAP/View/DataInput/BOMDisplay.aspx?WorkOrder=" + WorkOrder + "&LastPage=" + LastPage + "&test=" + test) 
            else
            {
                if (result == 3)
                {
                    AlertMessage("File is already Imported");
                }
                else
                {
                    AlertMessage("File is not in proper format");
                }
            }
        }
        else
        {
            AlertMessage("Pls Select file to be import!");
        }
    } 
    
    protected void btnImport_Click(object sender, EventArgs e)
    {
        
    }
    protected void btnDealer_Click(object sender, EventArgs e)
    {
        //Importfile(fileupldDealer, "DealerTemp", CalEffectiveDate.SelectedDate.ToShortDateString());     
        Importfile(fileupldDealer, "DealerTemp", objUtility.ConvertDateTime(txtEffectiveDate.Text.Trim()));     
    }
    
    protected void btnAcrImport_Click(object sender, EventArgs e)
    {
        DataTable dtUploadMode = new DataTable();
        string strUploadModeQuery = "Select * from FileUploadMode";
        dtUploadMode = objController.ExecuteQuery(strUploadModeQuery);


        string strPath = Server.MapPath("~/") + "UploadFile";
        string lblMessage = "";
        //Upload Acr Excel
        if (fileUpldAcr.HasFile)
        {
            DataView dv = new DataView(dtUploadMode);
            dv.RowFilter = "Filename='Acr'";
            DataTable dtMode = dv.ToTable();
            int Mode = Convert.ToInt16(dtMode.Rows[0]["Mode"].ToString());

            //DataTable dt = new DataTable();
            string strAcrFilePath = strPath;
            strAcrFilePath = strAcrFilePath + "\\Acr.xls";
            File.Delete(strAcrFilePath);

            fileUpldAcr.SaveAs(strAcrFilePath);

            //fileUpldAcr.SaveAs(strAcrFilePath);
            // string strsel = rdoAcrUploadMode.SelectedItem.Text;
            if (Mode == 1)
            {
                //DeleteContents("Acr");
                //string strDeleteAcr = "Delete from Acrbulk where FromDate='" + objUtility.ConvertDateTime(txtFromDate.Text.Trim()) + "' and ToDate='" + objUtility.ConvertDateTime(txtToDate.Text.Trim()) + "'";
                //objController.ExecuteQuery(strDeleteAcr);
            }
            //lblMessage = getAcr();
            lblMessage = getBulkAcr();
            //  lblAcrStatus.Text = lblMessage;
            string strjscript = "<script language='javascript'>";
            strjscript += " setMessageText('ctl00_ContentPlaceHolder1_lblAcrStatus','" + lblMessage + "' );";
            strjscript += "</script" + ">";
            Literal1.Text = strjscript;

            //  lblMessage = SaveAcr();
        }
    }
    protected void btnSalesImport_Click(object sender, EventArgs e)
    {
        DataTable dtUploadMode = new DataTable();
        string strUploadModeQuery = "Select * from FileUploadMode";
        dtUploadMode = objController.ExecuteQuery(strUploadModeQuery);


        string strPath = Server.MapPath("~/") + "UploadFile";
        string lblMessage = "";
        //Upload Sales Excel
        if (fileUpldSales.HasFile)
        {
            DataView dv = new DataView(dtUploadMode);
            dv.RowFilter = "Filename='Sales'";
            DataTable dtMode = dv.ToTable();
            int Mode = Convert.ToInt16(dtMode.Rows[0]["Mode"].ToString());

            string strSalesFilePath = strPath;
            strSalesFilePath = strSalesFilePath + "\\Sales.xls";
            fileUpldSales.SaveAs(strSalesFilePath);
            if (Mode == 1)
            {
                //DeleteContents("Sales");
                string strDeleteSales = "Delete from Sales where FromDate='" + objUtility.ConvertDateTime(txtSalesFromDate.Text.Trim()) + "' and ToDate='" + objUtility.ConvertDateTime(txtSalesToDate.Text.Trim()) + "'";
                objController.ExecuteQuery(strDeleteSales);
            }

            lblMessage = getSales();
            // lblMessage = SaveSales();
            //lblSalesStatus.Text = lblMessage;
            string strjscript = "<script language='javascript'>";
            strjscript += " setMessageText('ctl00_ContentPlaceHolder1_lblSalesStatus','" + lblMessage + "' );";
            strjscript += "</script" + ">";
            Literal3.Text = strjscript;
        }

        
    }
    protected void btnProdImport_Click(object sender, EventArgs e)
    {
        DataTable dtUploadMode = new DataTable();
        string strUploadModeQuery = "Select * from FileUploadMode";
        dtUploadMode = objController.ExecuteQuery(strUploadModeQuery);


        string strPath = Server.MapPath("~/") + "UploadFile";
        string lblMessage = "";
        //Upload Production Excel
        if (fileUpldProd.HasFile)
        {
            DataView dv = new DataView(dtUploadMode);
            dv.RowFilter = "Filename='Production'";
            DataTable dtMode = dv.ToTable();
            int Mode = Convert.ToInt16(dtMode.Rows[0]["Mode"].ToString());


            string strProdFilePath = strPath;
            strProdFilePath = strProdFilePath + "\\Production.xls";
            File.Delete(strProdFilePath);
            fileUpldProd.SaveAs(strProdFilePath);


            if (Mode == 1)  //If Mode is Replace
            {
                //DeleteContents("Production");
                string strDeleteProduction = "Delete from Production where FromDate='" + objUtility.ConvertDateTime(txtProdFromDate.Text.Trim()) + "' and ToDate='" + objUtility.ConvertDateTime(txtProdToDate.Text.Trim()) + "'";
                objController.ExecuteQuery(strDeleteProduction);
            }

            lblMessage = getProduction();
            // lblProductionStatus.Text = lblMessage;
            string strjscript = "<script language='javascript'>";
            strjscript += " setMessageText('ctl00_ContentPlaceHolder1_lblProductionStatus','" + lblMessage + "' );";
            strjscript += "</script" + ">";
            Literal2.Text = strjscript;
            //  lblMessage =  SaveProduction();

        }
    }
    protected void btnItemImport_Click(object sender, EventArgs e)
    {
        DataTable dtUploadMode = new DataTable();
        string strUploadModeQuery = "Select * from FileUploadMode";
        dtUploadMode = objController.ExecuteQuery(strUploadModeQuery);


        string strPath = Server.MapPath("~/") + "UploadFile";
        string lblMessage = "";
        //Upload Item Excel
        if (fileUpldItem.HasFile)
        {
            DataView dv = new DataView(dtUploadMode);
            dv.RowFilter = "Filename='Item'";
            DataTable dtMode = dv.ToTable();
            int Mode = Convert.ToInt16(dtMode.Rows[0]["Mode"].ToString());

            string strItemFilePath = strPath;
            strItemFilePath = strItemFilePath + "\\Item.xls";
            fileUpldItem.SaveAs(strItemFilePath);
            if (Mode == 1)
            {
                DeleteContents("Item");
            }
            lblMessage = getItem();
            // lblMessage = SaveItem();
            //lblItemStatus.Text = lblMessage;
            string strjscript = "<script language='javascript'>";
            strjscript += " setMessageText('ctl00_ContentPlaceHolder1_lblItemStatus','" + lblMessage + "' );";
            strjscript += "</script" + ">";
            Literal7.Text = strjscript;

        }
    }
    protected void btnDefectImport_Click(object sender, EventArgs e)
    {
        DataTable dtUploadMode = new DataTable();
        string strUploadModeQuery = "Select * from FileUploadMode";
        dtUploadMode = objController.ExecuteQuery(strUploadModeQuery);


        string strPath = Server.MapPath("~/") + "UploadFile";
        string lblMessage = "";
        //Upload Defect Excel
        if (fileUpldDefect.HasFile)
        {
            DataView dv = new DataView(dtUploadMode);
            dv.RowFilter = "Filename='Defect'";
            DataTable dtMode = dv.ToTable();
            int Mode = Convert.ToInt16(dtMode.Rows[0]["Mode"].ToString());

            string strDefectFilePath = strPath;
            strDefectFilePath = strDefectFilePath + "\\Defect.xls";
            fileUpldDefect.SaveAs(strDefectFilePath);
            if (Mode == 1)
            {
                DeleteContents("Defect");
            }
            lblMessage = getDefect();
            // lblMessage = SaveDefect();
            //lblDefectStatus.Text = lblMessage;
            string strjscript = "<script language='javascript'>";
            strjscript += " setMessageText('ctl00_ContentPlaceHolder1_lblDefectStatus','" + lblMessage + "' );";
            strjscript += "</script" + ">";
            Literal6.Text = strjscript;

        }
    }
    protected void btnCulpritImport_Click(object sender, EventArgs e)
    {
        DataTable dtUploadMode = new DataTable();
        string strUploadModeQuery = "Select * from FileUploadMode";
        dtUploadMode = objController.ExecuteQuery(strUploadModeQuery);


        string strPath = Server.MapPath("~/") + "UploadFile";
        string lblMessage = "";
        //Upload Culprit Excel
        if (fileUpldCulprit.HasFile)
        {
            DataView dv = new DataView(dtUploadMode);
            dv.RowFilter = "Filename='Culprit'";
            DataTable dtMode = dv.ToTable();
            int Mode = Convert.ToInt16(dtMode.Rows[0]["Mode"].ToString());

            string strCulpritFilePath = strPath;
            strCulpritFilePath = strCulpritFilePath + "\\Culprit.xls";
            fileUpldCulprit.SaveAs(strCulpritFilePath);
            if (Mode == 1)
            {
                DeleteContents("Culprit");
            }
            lblMessage = getCulprit();
            // lblMessage = SaveCulprit();
            //lblCulpritStatus.Text = lblMessage;
            string strjscript = "<script language='javascript'>";
            strjscript += " setMessageText('ctl00_ContentPlaceHolder1_lblCulpritStatus','" + lblMessage + "' );";
            strjscript += "</script" + ">";
            Literal4.Text = strjscript;

        }

       

    }
    protected void btnCustVoiceImport_Click(object sender, EventArgs e)
    {
        DataTable dtUploadMode = new DataTable();
        string strUploadModeQuery = "Select * from FileUploadMode";
        dtUploadMode = objController.ExecuteQuery(strUploadModeQuery);


        string strPath = Server.MapPath("~/") + "UploadFile";
        string lblMessage = "";
        //Upload Customer Voice Excel
        if (fileUpldCustVoice.HasFile)
        {
            DataView dv = new DataView(dtUploadMode);
            dv.RowFilter = "Filename='Customer Voice'";
            DataTable dtMode = dv.ToTable();
            int Mode = Convert.ToInt16(dtMode.Rows[0]["Mode"].ToString());

            string strCustVoiceFilePath = strPath;
            strCustVoiceFilePath = strCustVoiceFilePath + "\\Customer Voice.xls";
            fileUpldCustVoice.SaveAs(strCustVoiceFilePath);
            if (Mode == 1)
            {
                DeleteContents("CustomerVoice");
            }
            lblMessage = getCustomerVoice();
            //lblMessage = SaveCustomerVoice();
            //lblCustVoiceStatus.Text = lblMessage;
            string strjscript = "<script language='javascript'>";
            strjscript += " setMessageText('ctl00_ContentPlaceHolder1_lblCustVoiceStatus','" + lblMessage + "' );";
            strjscript += "</script" + ">";
            Literal5.Text = strjscript;

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


    /***********************************Function to Get Acr File into DataTable***********************************/
    public string getAcr()
    {
        string lblMessage = "";
        DataTable dt = new DataTable();

        DataTable dtHMR = new DataTable();
        string strHMRQuery = "select * from HMR";
        dtHMR = objController.ExecuteQuery(strHMRQuery);

        DataTable dtProdMonth = new DataTable();
        string strProdMonthQuery = "select * from ProductionMonth";
        dtProdMonth = objController.ExecuteQuery(strProdMonthQuery);

        string strxlsPath = Server.MapPath("~/") + "UploadFile\\Acr.xls";
        StreamReader sr = new StreamReader(strxlsPath);     //Read the Excel Stream
        try
        {

            string strTest = "";
            int i = 1;
            while (!sr.EndOfStream)
            {
                strTest = sr.ReadLine();
                string[] strData = strTest.Split('\t');
                int count = strData.Length;

                if (i == 1)
                {
                    for (int k = 1; k <= count; k++)
                    {
                        dt.Columns.Add(strData[k - 1]);        //Add Columns to Datatable
                    }
                    dt.Columns.Add("Production_Month");
                    dt.Columns.Add("Model_Code");
                    dt.Columns.Add("HMR_Range");
                    dt.Columns.Add("Production_Month_Year");
                    dt.Columns.Add("MonthID");
                    dt.Columns.Add("YearID");
                    dt.Columns.Add("Quarter");
                    dt.Columns.Add("Engine");
                    dt.Columns.Add("IsEngine");

                }
                else
                {
                    DataRow dr = dt.NewRow();                   //Add new Row to Datatable
                    for (int k = 1; k <= count; k++)
                    {
                        string str = strData[k - 1].Replace("\"", "");
                        str = str.Replace(",", "");
                        dr[k - 1] = str;
                    }
                    if (count > 3)
                    {
                        

                        try
                        {
                            int HMR = Convert.ToInt16(strData[7]);

                            foreach (DataRow drHMR in dtHMR.Rows)
                            {
                                int MinHMR = Convert.ToInt16(drHMR["Min_Value"]);    //Get the minimum HMR value from HMR Table
                                int MaxHMR = Convert.ToInt16(drHMR["Max_Value"]);    //Get the maximum HMR value from HMR Table

                                if (HMR >= MinHMR && HMR <= MaxHMR)                  //Check HMR Range
                                {
                                    dr["HMR_Range"] = Convert.ToString(drHMR["HMR_Range"]);
                                }


                            }
                        }
                        catch { }
                        string strTractorNo = strData[2];
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
                       
                        dr["Production_Month"] = ProductionMonth;

                            //dr["Model_Code"] = Convert.ToString(dtModelCode.Rows[0]["Model_Code"]);
                            dr["Model_Code"] = Convert.ToString(ModelCode);

                       
                        if (ProductionMonth != "")
                        {
                            int BaseProductionMonth = Convert.ToInt16(dtProdMonth.Rows[0]["BaseProductionMonth_Code"]);
                            int BaseMonthID = Convert.ToInt16(dtProdMonth.Rows[0]["Month_ID"]);
                            int BaseYearID = Convert.ToInt16(dtProdMonth.Rows[0]["Year_ID"]);
                            string strBaseDate = Convert.ToString(BaseMonthID) + "/1/" + BaseYearID;
                            DateTime BaseDate = Convert.ToDateTime(strBaseDate);
                            int Offset = Convert.ToInt16(ProductionMonth) - BaseProductionMonth;
                            DateTime ProdMonthYear = BaseDate.AddMonths(Offset);
                            
                            int CurrentYearID = ProdMonthYear.Year;
                            int CurrentMonthID = ProdMonthYear.Month;

                            string strCurrentYearID = (Convert.ToString(CurrentYearID)).Substring(2,2);

                            string strMonth = getMonth(CurrentMonthID);

                            string strProductionMonthYear = strMonth + "-" + strCurrentYearID;
                            dr["Production_Month_Year"] = strProductionMonthYear;
                            dr["MonthID"] = CurrentMonthID;
                            dr["YearID"] = CurrentYearID;
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

                            string strQuarterYear = "";

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

                            dr["Quarter"] = strQuarterYear;
                            string strDescription = dr["Description"].ToString();
                            string strPlant = rdoPlant.SelectedItem.Text;
                            string strEngine = "";
                            int IsEngine=0;

                            if (strPlant == "Alwar")
                            {
                                strEngine = "A";
                                IsEngine = 1;
                            }
                            else if (strPlant == "Bhopal")
                            {
                                if(strDescription.Contains("="))
                                {
                                    strEngine = "S";
                                    IsEngine = 1;
                                }
                                else
                                {
                                    strEngine = "A";
                                    IsEngine = 0;
                                }

                            }
                            dr["Engine"] = strEngine;
                            dr["IsEngine"] = IsEngine;
                        }
                    }
                     dt.Rows.Add(dr);
                }
                i++;
            }
            sr.Close();
            sr.Dispose();
            
           //lblMessage = SaveAcr(dt);
            SaveBulkAcr(dt);
           return lblMessage;
        }
        catch(Exception ex)
        {
            sr.Close();
            sr.Dispose();
            string strMessage = ex.Message.ToString();
            if (strMessage.Contains("does not belong to table"))
            {
                lblMessage = strMessage.Replace("belong to table", "exist in Excel file");
            }
            else if (strMessage.Contains("Input string was not in a correct format"))
            {
                lblMessage = "Excel file is not in valid format";
            }
            else
            {
                lblMessage = "File Acr could not be saved successfully";
            }
            return lblMessage;
        }


    }

    /***********************************Function to Save Acr File into Sql Server 2000 ***********************************/

    public string SaveAcr(DataTable dt)
    {
        DataTable dtTemp = new DataTable();
        int ID = 0;
        DataTable dtModel = new DataTable();
        string strModelQuery = "select * from Model";
        dtModel = objController.ExecuteQuery(strModelQuery);

        DataTable dtView = new DataTable();
        string strViewQuery = "Select distinct * from vwMastersCode order by tablename";
        dtView = objController.ExecuteQuery(strViewQuery);

        DataTable dtMonthOC = new DataTable();
        string strMonthOCQuery = "select * from MonthOpenClose";
        dtMonthOC = objController.ExecuteQuery(strMonthOCQuery);
        DataView dvMonthOC = new DataView(dtMonthOC);
        DataTable dtFilter = new DataTable();
        //int FromMonth = CalAcrfromDate.SelectedDate.Month;
        string[] strFromDt = txtFromDate.Text.Trim().Split('/');
        int FromMonth = Convert.ToInt16(strFromDt[1]);
        dvMonthOC.RowFilter = "MonthID =" + FromMonth;
        dtFilter = dvMonthOC.ToTable();
        string lblMessage = "";

        if (dtFilter.Rows != null)
        {
            bool OCStatus = Convert.ToBoolean(dtFilter.Rows[0]["Status"]);

            if (OCStatus == false)
            {
                lblMessage = "Month is Closed, Please open month to enter data";
            }
            else
            {
                string[] strToDt = txtToDate.Text.Trim().Split('/');
                //int ToMonth = CalAcrtoDate.SelectedDate.Month;
                int ToMonth = Convert.ToInt16(strToDt[1]);

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

                        DataView dvItem = new DataView(dtView);
                        dvItem.RowFilter = "tablename='item' and code='" + dr["ITEM CODE"].ToString() + "'";
                        if (dvItem.ToTable() != null)
                        {
                            if (dvItem.ToTable().Rows.Count > 0)
                            {
                                objUI.ITEM_CODE = Convert.ToString(dr["ITEM CODE"]);
                                strCause = strCause.Replace("item", "");
                                itemex = 0;
                                flag++;
                            }
                        }


                        DataView dvCulprit = new DataView(dtView);
                        dvCulprit.RowFilter = "tablename='culprit' and code='" + dr["CUL CODE"].ToString() + "'";
                        if (dvCulprit.ToTable() != null)
                        {
                            if (dvCulprit.ToTable().Rows.Count > 0)
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
                        }

                        string strCVOICE = Convert.ToString(dr["CVOICE"]);
                            if (strCVOICE == "")
                            {
                                strCVOICE = "0";
                            }

                        DataView dvCVoice = new DataView(dtView);
                        dvCVoice.RowFilter = "tablename='customervoice' and code='" + strCVOICE + "'";
                        if (dvCVoice.ToTable() != null)
                        {
                            if (dvCVoice.ToTable().Rows.Count > 0)
                            {
                                if (strCVOICE == "")
                                {
                                    objUI.CVOICE = Convert.ToInt32(null);
                                }
                                else
                                {
                                    objUI.CVOICE = Convert.ToInt32(strCVOICE);
                                }
                                strCause = strCause.Replace(";cvoice", "");
                                cvoiceex = 0;
                                flag++;
                            }
                        }

                         DataView dvDefect = new DataView(dtView);
                         dvDefect.RowFilter = "tablename='defect' and code='" + dr["DEF"].ToString()+"'";
                         if (dvDefect.ToTable() != null)
                         {
                             if (dvDefect.ToTable().Rows.Count > 0)
                             {
                                 string strDEF = Convert.ToString(dr["DEF"]);
                                 if (strDEF == "")
                                 {
                                     objUI.DEF = Convert.ToInt32(null);
                                 }
                                 else
                                 {
                                     objUI.DEF = Convert.ToInt32(dr["DEF"]);
                                 }
                                 strCause = strCause.Replace(";defect", "");
                                 defectex = 0;
                                 flag++;
                             }
                         }
                        //for (int i = 0; i < dtView.Rows.Count; i++)
                        //{
                        //    if ("item" == dtView.Rows[i]["tablename"].ToString() && dr["ITEM CODE"].ToString() == dtView.Rows[i]["code"].ToString())
                        //    {
                        //        objUI.ITEM_CODE = Convert.ToString(dr["ITEM CODE"]);
                        //        strCause = strCause.Replace("item", "");
                        //        itemex = 0;
                        //        flag++;
                        //    }
                        //    if ("culprit" == dtView.Rows[i]["tablename"].ToString() && dr["CUL CODE"].ToString() == dtView.Rows[i]["code"].ToString())
                        //    {
                        //        string strCUL_CODE = Convert.ToString(dr["CUL CODE"]);
                        //        if (strCUL_CODE == "")
                        //        {
                        //            objUI.CUL_CODE = Convert.ToInt32(null);
                        //        }
                        //        else
                        //        {
                        //            objUI.CUL_CODE = Convert.ToInt32(dr["CUL CODE"]);
                        //        }
                        //        strCause = strCause.Replace(";culprit", "");
                        //        culpritex = 0;
                        //        flag++;
                        //    }
                        //    string strCVOICE = Convert.ToString(dr["CVOICE"]);
                        //    if (strCVOICE == "")
                        //    {
                        //        strCVOICE = "0";
                        //    }
                        //    if ("customervoice" == dtView.Rows[i]["tablename"].ToString() && strCVOICE == dtView.Rows[i]["code"].ToString())
                        //    {
                               
                        //        if (strCVOICE == "")
                        //        {
                        //            objUI.CVOICE = Convert.ToInt32(null);
                        //        }
                        //        else
                        //        {
                        //            objUI.CVOICE = Convert.ToInt32(strCVOICE);
                        //        }
                        //        strCause = strCause.Replace(";cvoice", "");
                        //        cvoiceex = 0;
                        //        flag++;
                        //    }
                        //    if ("defect" == dtView.Rows[i]["tablename"].ToString() && dr["DEF"].ToString() == dtView.Rows[i]["code"].ToString())
                        //    {
                        //        string strDEF = Convert.ToString(dr["DEF"]);
                        //        if (strDEF == "")
                        //        {
                        //            objUI.DEF = Convert.ToInt32(null);
                        //        }
                        //        else
                        //        {
                        //            objUI.DEF = Convert.ToInt32(dr["DEF"]);
                        //        }
                        //        strCause = strCause.Replace(";defect", "");
                        //        defectex = 0;
                        //        flag++;
                        //    }
                        //    //if ("model" == dtView.Rows[i]["tablename"].ToString() && dr["Model_Code"].ToString() == dtView.Rows[i]["code"].ToString())
                        //    //{
                        //    //    DataView dv = new DataView(dtModel);
                        //    //    dv.RowFilter = "Code ='" + dr["Model_Code"].ToString() + "'";
                        //    //    DataTable dtModelCode = dv.ToTable();
                        //    //    objUI.Model_Code = Convert.ToString(dtModelCode.Rows[0]["Model_Code"]);
                        //    //    strCause = strCause.Replace(";model", "");
                        //    //    modelex = 0;
                        //    //    flag++;
                        //    //}

                            

                        //}

                        Int64 TractorNo = Convert.ToInt64(dr["TRACTOR NO"]);

                        int ModelMappingID = objCont.getModelMapping(TractorNo);
                        if (ModelMappingID > 0)
                        {
                            objUI.ModelMappingID = ModelMappingID;
                            modelex = 0;
                            flag++;
                        }
                       
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
                        objUI.QUANTITY = Convert.ToInt32(dr["QTY"]);
                        objUI.COST = Convert.ToString(dr["COST"]);
                        objUI.NDP = Convert.ToDecimal(dr["NDP"]);
                        objUI.VALUE = Convert.ToDecimal(dr["VALUE"]);
                        objUI.OUTLV = Convert.ToDecimal(dr["OUTLV"]);
                        objUI.DT = Convert.ToString(dr["DT"]);


                        string strBlank = Convert.ToString(dr["Column1"]);
                        if (strBlank == "")
                        {
                            objUI.BLANK = Convert.ToDouble(null);
                        }
                        else
                        {
                            objUI.BLANK = Convert.ToDouble(dr["Column1"]);
                        }

                        string strCR_AMOUNT = Convert.ToString(dr["CR-AMOUNT"]);
                        if (strCR_AMOUNT == "" )
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
                        objUI.FromDate = Convert.ToDateTime(objUtility.ConvertDateTime(txtFromDate.Text.Trim()));
                        //objUI.ToDate = CalAcrtoDate.SelectedDate;
                        objUI.ToDate = Convert.ToDateTime(objUtility.ConvertDateTime(txtToDate.Text.Trim()));
                        objUI.MonthID = Convert.ToInt16(dr["MonthID"]);
                        objUI.YearID = Convert.ToInt16(dr["YearID"]);
                        objUI.Quarter = Convert.ToString(dr["Quarter"]);
                        objUI.Engine = Convert.ToString(dr["Engine"]);
                        objUI.IsEngine = Convert.ToInt16(dr["IsEngine"].ToString());
                        
                        if (flag == 5)
                        {
                            ID = objCont.SaveAcr(objUI);
                            rcount++;
                        }
                        else
                        {
                            int index = strCause.IndexOf(';');
                           
                            if (index == 0)
                            {
                                strCause = strCause.Remove(0,1);
                            }
                            lnkExceptions.Visible = true;
                            string strDescription = dr["DESCRIPTION"].ToString().Replace("'", "");
                            string strIsEngine = dr["IsEngine"].ToString();
                            int IsEngine = 0;
                            if (strIsEngine == "")
                            {
                                IsEngine = 0;
                            }
                            else
                            {
                                IsEngine = Convert.ToInt16(strIsEngine);
                            }
                            string strCvoice = dr["CVOICE"].ToString();
                            if (strCvoice == "")
                            {
                                strCvoice = "0";
                            }
                            string strQuery = "Insert into AcrTemp ([WCDOCNO],[DLR_REF],[TRACTOR NO],[ENGINE NO],[INS DATE],[DEF DATE],[REP DATE],[HMR],[DLR CO],[DEALER NAME],[REG],[CR DATE],[ITEM CODE],[DESCRIPTION],[QTY],[COST],[DEF],[NDP],[VALUE],[CVOICE],[OUTLV],[DT],[CUL CODE],[F24],[CR-AMOUNT],[   AUTH AMT],[     DIFF],[Production_Month],[Model_Code],[HMR_Range],[Production_Month_Year],MonthID,YearID,[Quarter],[IsApproved],FromDate,ToDate,Cause,IsItemEx,IsCulpritEx,IsCVoiceEx,IsModelEx,IsDefectEx,Engine,IsEngine) ";
                            strQuery += "Values ('" + dr["WCDOCNO"].ToString() + "','" + dr["DLR_REF"].ToString() + "','" + dr["TRACTOR NO"].ToString() + "','" + dr["ENGINE NO"].ToString() + "','" + dr["INS DATE"].ToString() + "','" + dr["DEF DATE"].ToString() + "','" + dr["REP DATE"].ToString() + "','" + dr["HMR"].ToString() + "','" + dr["DLR CO"].ToString() + "','" + dr["DEALER NAME"].ToString() + "','" + dr["REG"].ToString() + "','" + dr["CR DATE"].ToString() + "','" + dr["ITEM CODE"].ToString() + "','" + strDescription + "','" + dr["QTY"].ToString() + "','" + dr["COST"].ToString() + "','" + dr["DEF"].ToString() + "','" + dr["NDP"].ToString() + "','" + dr["VALUE"].ToString() + "','" + strCvoice + "','" + dr["OUTLV"].ToString() + "','" + dr["DT"].ToString() + "','" + dr["CUL CODE"].ToString() + "','" + dr["Column1"].ToString() + "','" + dr["CR-AMOUNT"].ToString() + "','" + dr["   AUTH AMT"].ToString() + "','" + dr["     DIFF"].ToString() + "','" + dr["Production_Month"].ToString() + "','" + dr["Model_Code"].ToString() + "','" + dr["HMR_Range"].ToString() + "','" + dr["Production_Month_Year"].ToString() + "','" + dr["MonthID"].ToString() + "','" + dr["YearID"].ToString() + "','" + dr["Quarter"].ToString() + "',0,'" + objUtility.ConvertDateTime(txtFromDate.Text.Trim()) + "','" + objUtility.ConvertDateTime(txtToDate.Text.Trim()) + "','" + strCause + "'," + itemex + "," + culpritex + "," + cvoiceex + "," + modelex + "," + defectex + ",'" + dr["Engine"].ToString() + "'," + IsEngine + ")";
                            objController.ExecuteQuery(strQuery);
                        }

                        lblMessage = "File Acr saved successfully;No of Rows Affected:" + rcount;
                    }
                    catch
                    {
                        lnkExceptions.Visible = true;
                        string strDescription = dr["DESCRIPTION"].ToString().Replace("'", "");
                        string strIsEngine = dr["IsEngine"].ToString();
                        int IsEngine = 0;
                        if (strIsEngine == "")
                        {
                            IsEngine = 0;
                        }
                        else
                        {
                            IsEngine = Convert.ToInt16(strIsEngine);
                        }
                        string strCvoice = dr["CVOICE"].ToString();
                        if (strCvoice == "")
                        {
                            strCvoice = "0";
                        }
                        string strQuery = "Insert into AcrTemp ([WCDOCNO],[DLR_REF],[TRACTOR NO],[ENGINE NO],[INS DATE],[DEF DATE],[REP DATE],[HMR],[DLR CO],[DEALER NAME],[REG],[CR DATE],[ITEM CODE],[DESCRIPTION],[QTY],[COST],[DEF],[NDP],[VALUE],[CVOICE],[OUTLV],[DT],[CUL CODE],[F24],[CR-AMOUNT],[   AUTH AMT],[     DIFF],[Production_Month],[Model_Code],[HMR_Range],[Production_Month_Year],MonthID,YearID,[Quarter],[IsApproved],FromDate,ToDate,Cause,IsItemEx,IsCulpritEx,IsCVoiceEx,IsModelEx,IsDefectEx,Engine,IsEngine) ";
                        strQuery += "Values ('" + dr["WCDOCNO"].ToString() + "','" + dr["DLR_REF"].ToString() + "','" + dr["TRACTOR NO"].ToString() + "','" + dr["ENGINE NO"].ToString() + "','" + dr["INS DATE"].ToString() + "','" + dr["DEF DATE"].ToString() + "','" + dr["REP DATE"].ToString() + "','" + dr["HMR"].ToString() + "','" + dr["DLR CO"].ToString() + "','" + dr["DEALER NAME"].ToString() + "','" + dr["REG"].ToString() + "','" + dr["CR DATE"].ToString() + "','" + dr["ITEM CODE"].ToString() + "','" + strDescription + "','" + dr["QTY"].ToString() + "','" + dr["COST"].ToString() + "','" + dr["DEF"].ToString() + "','" + dr["NDP"].ToString() + "','" + dr["VALUE"].ToString() + "','" + strCvoice + "','" + dr["OUTLV"].ToString() + "','" + dr["DT"].ToString() + "','" + dr["CUL CODE"].ToString() + "','" + dr["Column1"].ToString() + "','" + dr["CR-AMOUNT"].ToString() + "','" + dr["   AUTH AMT"].ToString() + "','" + dr["     DIFF"].ToString() + "','" + dr["Production_Month"].ToString() + "','" + dr["Model_Code"].ToString() + "','" + dr["HMR_Range"].ToString() + "','" + dr["Production_Month_Year"].ToString() + "','" + dr["MonthID"].ToString() + "','" + dr["YearID"].ToString() + "','" + dr["Quarter"].ToString() + "',0,'" + objUtility.ConvertDateTime(txtFromDate.Text.Trim()) + "','" + objUtility.ConvertDateTime(txtToDate.Text.Trim()) + "','" + strCause + "'," + itemex + "," + culpritex + "," + cvoiceex + "," + modelex + "," + defectex + ",'" + dr["Engine"].ToString() + "'," + IsEngine + ")";
                        objController.ExecuteQuery(strQuery);
                    }

                }
            }
        }
        return lblMessage;
    }


    public string getBulkAcr()
    {
        string lblMessage = "";
        DataTable dt = new DataTable();

        DataTable dtHMR = new DataTable();
        string strHMRQuery = "select * from HMR";
        dtHMR = objController.ExecuteQuery(strHMRQuery);

        DataTable dtProdMonth = new DataTable();
        string strProdMonthQuery = "select * from ProductionMonth";
        dtProdMonth = objController.ExecuteQuery(strProdMonthQuery);

        string strxlsPath = Server.MapPath("~/") + "UploadFile\\Acr.xls";
        StreamReader sr = new StreamReader(strxlsPath);     //Read the Excel Stream
        try
        {

            string strTest = "";
            int i = 1;
            while (!sr.EndOfStream)
            {
                strTest = sr.ReadLine();
                string[] strData = strTest.Split('\t');
                int count = strData.Length;

                if (i == 1)
                {
                    for (int k = 1; k <= count; k++)
                    {
                        dt.Columns.Add(strData[k - 1]);        //Add Columns to Datatable
                    }
                    dt.Columns.Add("Production_Month");
                    dt.Columns.Add("Model_Code");
                    dt.Columns.Add("HMR_Range");
                    dt.Columns.Add("Production_Month_Year");
                    dt.Columns.Add("MonthID");
                    dt.Columns.Add("YearID");
                    dt.Columns.Add("Quarter");
                    dt.Columns.Add("Engine");
                    dt.Columns.Add("IsEngine");
                    dt.Columns.Add("FromDate");
                    dt.Columns.Add("ToDate");
                    dt.Columns.Add("IsModelEx",Type.GetType("System.Boolean"));
                    dt.Columns.Add("IsItemEx",Type.GetType("System.Boolean"));
                    dt.Columns.Add("IsCulpritEx", Type.GetType("System.Boolean"));
                    dt.Columns.Add("IsCVoiceEx", Type.GetType("System.Boolean"));
                    dt.Columns.Add("IsDefectEx", Type.GetType("System.Boolean"));

                }
                else
                {
                    DataRow dr = dt.NewRow();                   //Add new Row to Datatable
                    for (int k = 1; k <= count; k++)
                    {
                        if ((k - 1) == 4 || (k - 1) == 5 || (k - 1) == 6 || (k - 1) == 11)
                        {
                            string str = strData[k - 1].Replace("\"", "");
                            str = str.Replace(",", "");
                            string strDate = "";
                            if (str == "" || str == "00.00.0000" || str.Contains("0000"))
                            {
                                strDate = Convert.ToString(Convert.ToDateTime("1/1/2001"));
                            }
                            else
                            {
                                strDate = Convert.ToString(Convert.ToDateTime(ConvertDateTime(str)));
                            }
                            dr[k - 1] = strDate;

                        }
                        else
                        {
                            string str = strData[k - 1].Replace("\"", "");
                            str = str.Replace(",", "");
                            dr[k - 1] = str;
                        }
                    }
                    if (count > 3)
                    {


                        try
                        {
                            int HMR = Convert.ToInt16(strData[7]);

                            foreach (DataRow drHMR in dtHMR.Rows)
                            {
                                int MinHMR = Convert.ToInt16(drHMR["Min_Value"]);    //Get the minimum HMR value from HMR Table
                                int MaxHMR = Convert.ToInt16(drHMR["Max_Value"]);    //Get the maximum HMR value from HMR Table

                                if (HMR >= MinHMR && HMR <= MaxHMR)                  //Check HMR Range
                                {
                                    dr["HMR_Range"] = Convert.ToString(drHMR["HMR_Range"]);
                                }


                            }
                        }
                        catch { }
                        string strTractorNo = strData[2];
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

                        dr["Production_Month"] = ProductionMonth;

                        //dr["Model_Code"] = Convert.ToString(dtModelCode.Rows[0]["Model_Code"]);
                        dr["Model_Code"] = Convert.ToString(ModelCode);


                        if (ProductionMonth != "")
                        {
                            int BaseProductionMonth = Convert.ToInt16(dtProdMonth.Rows[0]["BaseProductionMonth_Code"]);
                            int BaseMonthID = Convert.ToInt16(dtProdMonth.Rows[0]["Month_ID"]);
                            int BaseYearID = Convert.ToInt16(dtProdMonth.Rows[0]["Year_ID"]);
                            string strBaseDate = Convert.ToString(BaseMonthID) + "/1/" + BaseYearID;
                            DateTime BaseDate = Convert.ToDateTime(strBaseDate);
                            int Offset = Convert.ToInt16(ProductionMonth) - BaseProductionMonth;
                            DateTime ProdMonthYear = BaseDate.AddMonths(Offset);

                            int CurrentYearID = ProdMonthYear.Year;
                            int CurrentMonthID = ProdMonthYear.Month;

                            string strCurrentYearID = (Convert.ToString(CurrentYearID)).Substring(2, 2);

                            string strMonth = getMonth(CurrentMonthID);

                            string strProductionMonthYear = strMonth + "-" + strCurrentYearID;
                            dr["Production_Month_Year"] = strProductionMonthYear;
                            dr["MonthID"] = CurrentMonthID;
                            dr["YearID"] = CurrentYearID;
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

                            string strQuarterYear = "";

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

                            dr["Quarter"] = strQuarterYear;
                            string strDescription = dr["Description"].ToString();
                            string strPlant = rdoPlant.SelectedItem.Text;
                            string strEngine = "";
                            int IsEngine = 0;

                            if (strPlant == "Alwar")
                            {
                                strEngine = "A";
                                IsEngine = 1;
                            }
                            else if (strPlant == "Bhopal")
                            {
                                if (strDescription.Contains("="))
                                {
                                    strEngine = "S";
                                    IsEngine = 1;
                                }
                                else
                                {
                                    strEngine = "A";
                                    IsEngine = 0;
                                }

                            }
                            dr["Engine"] = strEngine;
                            dr["IsEngine"] = IsEngine;
                            dr["FromDate"] = Convert.ToDateTime(objUtility.ConvertDateTime(txtFromDate.Text.Trim()));
                            dr["ToDate"] = Convert.ToDateTime(objUtility.ConvertDateTime(txtToDate.Text.Trim()));
                            dr["IsModelEx"] = 1;
                            dr["IsItemEx"] = 1;
                            dr["IsCulpritEx"] = 1 ;
                            dr["IsCVoiceEx"] = 1;
                            dr["IsDefectEx"] =  1;
                        }
                    }
                    dt.Rows.Add(dr);
                }
                i++;
            }
            sr.Close();
            sr.Dispose();

            //lblMessage = SaveAcr(dt);
            lblMessage = SaveBulkAcr(dt);
            return lblMessage;
        }
        catch (Exception ex)
        {
            sr.Close();
            sr.Dispose();
            string strMessage = ex.Message.ToString();
            if (strMessage.Contains("does not belong to table"))
            {
                lblMessage = strMessage.Replace("belong to table", "exist in Excel file");
            }
            else if (strMessage.Contains("Input string was not in a correct format"))
            {
                lblMessage = "Excel file is not in valid format";
            }
            else
            {
                lblMessage = "File Acr could not be saved successfully";
            }
            return lblMessage;
        }


    }


    public string SaveBulkAcr(DataTable dt)
    {
        AcrController objAcrController = new AcrController();
        string sConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        string lblMessage = "";
        try
        {
            SqlConnection sqlconn = new SqlConnection(sConnectionString);
            sqlconn.Open();
            SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlconn);
            bulkCopy.DestinationTableName = "AcrBulkTemp";
            bulkCopy.WriteToServer(dt);
            int Status = objAcrController.SaveBulkAcr();
            //string strQuery = "UPDATE AcrBulk SET ModelMappingID =( SELECT Production.ModelMappingID FROM Production WHERE Production.SerialNo = Convert(varchar,AcrBulk.Tractor_No)) WHERE EXISTS ( SELECT Production.ModelMappingID FROM Production WHERE Production.SerialNo = Convert(varchar,AcrBulk.Tractor_No))";
            string strQuery = "Update AcrBulk set ModelMappingID = Production.ModelMappingID  from AcrBulk inner join Production on Convert(varchar,AcrBulk.Tractor_No) = Production.SerialNo ";
            objController.ExecuteQuery(strQuery);
            if (Status == 0)
            {
                lblMessage = "File Acr saved successfully";
            }
            else
            {
                lnkExceptions.Visible = true;
                lblMessage = "File Acr could not be saved successfully";
            }
        }
        catch
        {
            lnkExceptions.Visible = true;
            lblMessage = "File Acr could not be saved successfully";
        }
        return lblMessage;
    }

    /***********************************Function to Delete Contents from Sql Server 2000 table***********************************/
    public void DeleteContents(string strTableName)
    {
        string strdeletequery = "Delete from "+strTableName;
        try
        {
            objController.ExecuteQuery(strdeletequery);
        }
        catch
        { }
    }

    /***********************************Function to Get Production File into DataTable***********************************/
    public string getProduction()
    {

        DataTable dtProdMonth = new DataTable();
        string strProdMonthQuery = "select * from ProductionMonth";
        dtProdMonth = objController.ExecuteQuery(strProdMonthQuery);

        DataTable dtModel = new DataTable();
        string strModelQuery = "select * from Model";
        dtModel = objController.ExecuteQuery(strModelQuery);

        string lblMessage = "";
        DataTable dt = new DataTable();
        string strxlsPath = Server.MapPath("~/") + "UploadFile\\Production.xls";
        StreamReader sr = new StreamReader(strxlsPath);     //Read the Excel Stream
        try
        {

            string strTest = "";
            int i = 1;
            while (!sr.EndOfStream)
            {
                strTest = sr.ReadLine();
                string[] strData = strTest.Split('\t');
                int count = strData.Length;

                if (i == 1)
                {
                    for (int k = 1; k <= count; k++)
                    {
                        dt.Columns.Add(strData[k - 1]);        //Add Columns to Datatable
                    }
                    dt.Columns.Add("Production_Month");
                    dt.Columns.Add("Model_Code");
                    dt.Columns.Add("Production_Month_Year");
                    dt.Columns.Add("MonthID");
                    dt.Columns.Add("YearID");
                    dt.Columns.Add("Quarter");
                }
                else
                {
                    DataRow dr = dt.NewRow();                   //Add new Row to Datatable
                    for (int k = 1; k <= count; k++)
                    {
                        string str = strData[k - 1].Replace("\"", "");
                        str = str.Replace(",", "");
                        dr[k - 1] = str;
                    }

                    if (count > 2)
                    {
                        string strSerialNo = strData[2];
                        int serialnoLength = strSerialNo.Length;
                        string ProductionMonth = "";
                        string ModelCode = "0";

                        if (serialnoLength == 11)                            //If Tractor No Length is 11
                        {
                            ProductionMonth = strSerialNo.Substring(1, 2);   // 2nd and 3rd digit is Production Month
                            ModelCode = strSerialNo.Substring(3, 3);         // The next 3 digits i.e. 4th, 5th and 6th digits is Model Code
                        }
                        else if (serialnoLength == 12)                       //If Tractor No Length is 12
                        {
                            ProductionMonth = strSerialNo.Substring(1, 3);   // 2nd to 4th digit is Production Month
                            ModelCode = strSerialNo.Substring(4, 3);         // The next 3 digits i.e. 5th, 6th and 7th digits is Model Code
                        }
                        //DataView dv = new DataView(dtModel);
                        //dv.RowFilter = "Code =" + Convert.ToInt16(ModelCode);
                        //DataTable dtModelCode = dv.ToTable();

                        dr["Production_Month"] = ProductionMonth;
                        dr["Model_Code"] = Convert.ToString(ModelCode);                        
                        
                        if (ProductionMonth != "")
                        {
                            int BaseProductionMonth = Convert.ToInt16(dtProdMonth.Rows[0]["BaseProductionMonth_Code"]);
                            int BaseMonthID = Convert.ToInt16(dtProdMonth.Rows[0]["Month_ID"]);
                            int BaseYearID = Convert.ToInt16(dtProdMonth.Rows[0]["Year_ID"]);
                            string strBaseDate = Convert.ToString(BaseMonthID) + "/1/" + BaseYearID;
                            DateTime BaseDate = Convert.ToDateTime(strBaseDate);
                            int Offset = Convert.ToInt16(ProductionMonth) - BaseProductionMonth;
                            DateTime ProdMonthYear = BaseDate.AddMonths(Offset);

                            int CurrentYearID = ProdMonthYear.Year;
                            int CurrentMonthID = ProdMonthYear.Month;

                            string strCurrentYearID = (Convert.ToString(CurrentYearID)).Substring(2, 2);

                            string strMonth = getMonth(CurrentMonthID);

                            string strProductionMonthYear = strMonth + "-" + strCurrentYearID;
                            dr["Production_Month_Year"] = strProductionMonthYear;
                            dr["MonthID"] = CurrentMonthID;
                            dr["YearID"] = CurrentYearID;
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

                            string strQuarterYear = "";

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

                            dr["Quarter"] = strQuarterYear;
                           
                        }
                       
                    }
                    dt.Rows.Add(dr);
                }
                i++;
            }
            sr.Close();
            sr.Dispose();


            lblMessage = SaveProduction(dt);
            return lblMessage;
        }
        catch(Exception ex)
        {
            sr.Close();
            sr.Dispose();
            string strMessage = ex.Message.ToString();
            if (strMessage.Contains("does not belong to table"))
            {
                lblMessage = strMessage.Replace("belong to table", "exist in Excel file");
            }
            else if (strMessage.Contains("Input string was not in a correct format"))
            {
                lblMessage = "Excel file is not in valid format";
            }
            else
            {
                lblMessage = "File Production could not be saved successfully";
            }
            return lblMessage;
        }




    }

    /***********************************Function to Production Acr File into Sql Server 2000 using Bulk Copy Method***********************************/
    public string SaveProduction(DataTable dt)
    {
        string lblMessage = "";
        ProductionController objCont = new ProductionController();

        DataTable dtView = new DataTable();
        string strViewQuery = "Select distinct * from vwMastersCode";
        dtView = objController.ExecuteQuery(strViewQuery);


        //DataTable dtModel = new DataTable();
        //string strModelQuery = "select * from Model";
        //dtModel = objController.ExecuteQuery(strModelQuery);

        DataTable dtMonthOC = new DataTable();
        string strMonthOCQuery = "select * from MonthOpenClose";
        dtMonthOC = objController.ExecuteQuery(strMonthOCQuery);
        DataView dvMonthOC = new DataView(dtMonthOC);
        DataTable dtFilter = new DataTable();
        string[] strProdFromMonth = txtProdFromDate.Text.Trim().Split('/');
        int FromMonth = Convert.ToInt16(strProdFromMonth[1]);
        //int FromMonth = CalProdfromDate.SelectedDate.Month;
        dvMonthOC.RowFilter = "MonthID =" + FromMonth;
        dtFilter = dvMonthOC.ToTable();
       

        if (dtFilter.Rows != null)
        {
            bool OCStatus = Convert.ToBoolean(dtFilter.Rows[0]["Status"]);

            if (OCStatus == false)
            {
                lblMessage = "Month is Closed, Please open month to enter data";
            }
            else
            {
                //int ToMonth = CalProdtoDate.SelectedDate.Month;
                string[] strToProdMonth = txtProdToDate.Text.Trim().Split('/');
                int ToMonth = Convert.ToInt16(strToProdMonth[1]);
                string sConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ToString();

                //try
                //{
                //    string strDelete = "Delete from ProductionTemp";
                //    objController.ExecuteQuery(strDelete);

                //    SqlConnection sqlconn = new SqlConnection(sConnectionString);
                //    sqlconn.Open();
                //    SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlconn);
                //    bulkCopy.DestinationTableName = "ProductionTemp";
                //    bulkCopy.WriteToServer(dt);

                //}
                //catch
                //{ 

                //}
                int rcount = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    ProductionUI objUI = new ProductionUI();
                    string strCause = "model";
                    int modelex = 1;
                    

                    try
                    {
                        int flag = 0;
                        //for (int i = 0; i < dtView.Rows.Count; i++)
                        //{
                        //    if ("model" == dtView.Rows[i]["tablename"].ToString() && dr["Model_Code"].ToString() == dtView.Rows[i]["code"].ToString())
                        //    {
                        //        DataView dv = new DataView(dtModel);
                        //        dv.RowFilter = "Code ='" + dr["Model_Code"].ToString() + "'";
                        //        DataTable dtModelCode = dv.ToTable();
                        //        objUI.Model_Code = Convert.ToString(dtModelCode.Rows[0]["Model_Code"]);
                        //        strCause = strCause.Replace("model", "");
                        //        modelex = 0;
                        //        flag++;
                        //    }
                        //}
                        string strMaterial = Convert.ToString(dr["Material"]);
                          string strModelQuery = "Select * from ModelMapping where Material='" + strMaterial + "'";
                        DataTable dtModel = objController.ExecuteQuery(strModelQuery);
                        if (dtModel != null)
                        {
                            if (dtModel.Rows.Count > 0)
                            {
                                foreach (DataRow drModel in dtModel.Rows)
                                {
                                    objUI.ModelMappingID = Convert.ToInt32(drModel["ID"].ToString());
                                    strCause = strCause.Replace("model", "");
                                    modelex = 0;
                                    flag++;
                                }
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
                        
                        objUI.SerialNo = Convert.ToString(dr["Serial no."]);
                        objUI.Plnt = Convert.ToString(dr["Plnt"]);

                        objUI.SLoc = Convert.ToString(dr["SLoc"]);
                       
                        objUI.Description = Convert.ToString(dr["Description of technical object"]);
                        objUI.Production_Month = Convert.ToInt16(dr["Production_Month"]);
                        objUI.Production_Month_Year = Convert.ToString(dr["Production_Month_Year"]);
                        //objUI.FromDate = CalProdfromDate.SelectedDate;
                        //objUI.ToDate = CalProdtoDate.SelectedDate;
                        objUI.FromDate = Convert.ToDateTime(objUtility.ConvertDateTime(txtProdFromDate.Text.Trim()));
                        objUI.ToDate = Convert.ToDateTime(objUtility.ConvertDateTime(txtProdToDate.Text.Trim()));
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
                            lnkProdException.Visible = true;
                            string strQuery = "Insert into ProductionTemp ([S],[Material],[Serial no#],[Plnt],[SLoc],[Description of technical object],[Production_Month],[Model_Code],[Production_Month_Year],[IsApproved],MonthID,YearID,[Quarter],IsModelEx,FromDate,ToDate) ";
                            strQuery += "Values ('" + dr["S"].ToString() + "','" + dr["Material"].ToString() + "','" + dr["Serial no."].ToString() + "','" + dr["Plnt"].ToString() + "','" + dr["SLoc"].ToString() + "','" + dr["Description of technical object"].ToString() + "','" + dr["Production_Month"].ToString() + "','" + dr["Model_Code"].ToString() + "','" + dr["Production_Month_Year"].ToString() + "',0,'" + dr["MonthID"].ToString() + "','" + dr["YearID"].ToString() + "','" + dr["Quarter"].ToString() + "','" + modelex + "','" + objUtility.ConvertDateTime(txtProdFromDate.Text.Trim()) + "','" + objUtility.ConvertDateTime(txtProdToDate.Text.Trim()) + "')";
                            objController.ExecuteQuery(strQuery);
                        }
                        lblMessage = "File Production saved successfully;No of Rows Affected:" + rcount;
                    }
                    catch
                    {
                        lnkProdException.Visible = true;
                        string strQuery = "Insert into ProductionTemp ([S],[Material],[Serial no#],[Plnt],[SLoc],[Description of technical object],[Production_Month],[Model_Code],[Production_Month_Year],[IsApproved],MonthID,YearID,[Quarter],IsModelEx,FromDate,ToDate) ";
                        strQuery += "Values ('" + dr["S"].ToString() + "','" + dr["Material"].ToString() + "','" + dr["Serial no."].ToString() + "','" + dr["Plnt"].ToString() + "','" + dr["SLoc"].ToString() + "','" + dr["Description of technical object"].ToString() + "','" + dr["Production_Month"].ToString() + "','" + dr["Model_Code"].ToString() + "','" + dr["Production_Month_Year"].ToString() + "',0,'" + dr["MonthID"].ToString() + "','" + dr["YearID"].ToString() + "','" + dr["Quarter"].ToString() + "','" + modelex + "','" + objUtility.ConvertDateTime(txtProdFromDate.Text.Trim()) + "','" + objUtility.ConvertDateTime(txtProdToDate.Text.Trim()) + "')";
                        objController.ExecuteQuery(strQuery);
                    }
                    
                }
                //string strAcrUpdateQuery = "UPDATE AcrBulk SET ModelMappingID =( SELECT Production.ModelMappingID FROM Production WHERE Production.SerialNo = Convert(varchar,AcrBulk.Tractor_No)) WHERE EXISTS ( SELECT Production.ModelMappingID FROM Production WHERE Production.SerialNo = Convert(varchar,AcrBulk.Tractor_No))";
                string strAcrUpdateQuery = "Update AcrBulk set ModelMappingID = Production.ModelMappingID  from AcrBulk inner join Production on Convert(varchar,AcrBulk.Tractor_No) = Production.SerialNo ";
                objController.ExecuteQuery(strAcrUpdateQuery);

            }
        }
        return lblMessage;
    }

    /***********************************Function to Get Sales File into DataTable***********************************/
    public string getSales()
    {
        SalesController objSalesController = new SalesController();
        string lblMessage = "";
        string strPath = Server.MapPath("~/") + "UploadFile\\Sales.xls";
        string excelConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strPath;


        excelConnectionString += @";Extended Properties=""Excel 8.0;HDR=YES;IMEX=1;""";
        OleDbConnection connection = new OleDbConnection(excelConnectionString);
      

        string Name = "";
        try
        {
            connection.Open();
            DataTable SheetName = new DataTable();
            SheetName = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string[] excelSheets = new string[SheetName.Rows.Count];
            int i = 0;
            // Add the sheet name to the string array.
            foreach (DataRow row in SheetName.Rows)
            {

                excelSheets[i] = SheetName.Rows[0]["TABLE_NAME"].ToString();
                Name = excelSheets[i];
            }

            DataTable dtNew = new DataTable();

            string query = "Select * FROM [" + Name + "]";

            OleDbCommand command = new OleDbCommand(query, connection);
            OleDbDataAdapter dataread = new OleDbDataAdapter(command);
            DataTable dt = new DataTable();

            dataread.Fill(dt);
            dtNew = dt;

           
            lblMessage = SaveSales(dtNew);
            //lblMessage = "File Sales saved successfully";
            connection.Close();
            return lblMessage;


        }
        catch (Exception ex)
        {
            connection.Close();
            string strMessage = ex.Message.ToString();

            if (strMessage == "External table is not in the expected format." || strMessage.Contains("does not belong to table"))
            {
                lblMessage = "Excel file is not in valid format";
            }
            else
            {
                lblMessage = "File Sales could not be saved successfully";
            }
            return lblMessage;
        }


    }

    /***********************************Function to Save Sales File into Sql Server 2000 using Bulk Copy Method***********************************/
    public string SaveSales(DataTable dt)
    {
        string lblMessage = "";

        SalesController objCont = new SalesController();
        string sConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ToString();

        //try
        //{
        //    string strDelete = "Delete from SalesTemp";
        //    objController.ExecuteQuery(strDelete);

        //    SqlConnection sqlconn = new SqlConnection(sConnectionString);
        //    sqlconn.Open();
        //    SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlconn);
        //    bulkCopy.DestinationTableName = "SalesTemp";
        //    bulkCopy.WriteToServer(dt);
        //}
        //catch
        //{
        //}
        DataTable dtMonthOC = new DataTable();
        string strMonthOCQuery = "select * from MonthOpenClose";
        dtMonthOC = objController.ExecuteQuery(strMonthOCQuery);
        DataView dvMonthOC = new DataView(dtMonthOC);
        DataTable dtFilter = new DataTable();
        string[] strFromSalesMonth = txtSalesFromDate.Text.Trim().Split('/');
        int FromMonth = Convert.ToInt16(strFromSalesMonth[1]);
        //int FromMonth = CalSalesfromDate.SelectedDate.Month;
        dvMonthOC.RowFilter = "MonthID =" + FromMonth;
        dtFilter = dvMonthOC.ToTable();
        
        if (dtFilter.Rows != null)
        {
            bool OCStatus = Convert.ToBoolean(dtFilter.Rows[0]["Status"]);

            if (OCStatus == false)
            {
                lblMessage = "Month is Closed, Please open month to enter data";
            }
            else
            {
                string[] strToSalesDate = txtSalesToDate.Text.Trim().Split('/');
                //int ToMonth = CalSalestoDate.SelectedDate.Month;
                int ToMonth = Convert.ToInt16(strToSalesDate[1]);
                int rcount = 0;

                foreach (DataRow dr in dt.Rows)
                {

                    int IsModelEx = 1;
                    SalesUI objUI = new SalesUI();
                    try
                    {
                        objUI.Sno = Convert.ToInt32(dr["Sno"]);
                        if (dr["Inv#No"].ToString() == "7253102")
                        {
                            string strTest = "";
                        }
                        objUI.InvoiceNo = Convert.ToInt32(dr["Inv#No"]);
                        //objUI.Date = Convert.ToString(dr["Date"]);
                       

                        string strDate = Convert.ToString(dr["Date"]);
                        if (strDate == "" || strDate == "00.00.0000")
                        {
                            objUI.Date = Convert.ToDateTime("1/1/2001");
                        }
                        else
                        {
                            objUI.Date = Convert.ToDateTime(ConvertDateTime(Convert.ToString(dr["Date"])));
                        }

                        objUI.Dealer_Code = Convert.ToString(dr["DlrCode"]);
                        objUI.Dealer_Name = Convert.ToString(dr["Dlr Name"]);
                        objUI.Blank = Convert.ToString(dr["F10"]);
                        string strMaterial = dr["F10"].ToString();


                        string strModelQuery = "Select * from ModelMapping where Material='" + strMaterial + "'";
                        DataTable dtModel = objController.ExecuteQuery(strModelQuery);

                        if (dtModel != null)
                        {
                            if (dtModel.Rows.Count > 0)
                            {
                                foreach (DataRow drModel in dtModel.Rows)
                                {
                                    objUI.ModelMappingID = Convert.ToInt32(drModel["ID"].ToString());
                                    IsModelEx = 0;
                                }

                            }
                            
                        }


                        objUI.Model_Code = Convert.ToString(dr["Model Code"]);
                        string strQuantity = Convert.ToString(dr["Qty"]);
                        if (strQuantity == "")
                        {
                            objUI.Quantity = Convert.ToInt32(null);
                        }
                        else
                        {
                            objUI.Quantity = Convert.ToInt32(dr["Qty"]);
                        }

                        string strSaleAmt = dr["Sale Amt"].ToString();
                        if (strSaleAmt == "")
                        {
                            objUI.SalesAmount = Convert.ToDouble(null);
                        }
                        else
                        {
                            objUI.SalesAmount = Convert.ToDouble(dr["Sale Amt"]);
                        }

                        string strDiscount = Convert.ToString(dr["Discount"]);
                        if (strDiscount == "")
                        {
                            objUI.Discount = Convert.ToInt32(null);
                        }
                        else
                        {
                            objUI.Discount = Convert.ToInt32(dr["Discount"]);
                        }

                        string strSPLDIS = Convert.ToString(dr["SPL#DIS"]);
                        if (strSPLDIS == "")
                        {
                            objUI.SPLDIS = Convert.ToDouble(null);
                        }
                        else
                        {
                            objUI.SPLDIS = Convert.ToDouble(dr["SPL#DIS"]);
                        }

                        string strExciseDuty = Convert.ToString(dr["Excise Duty"]);
                        if (strExciseDuty == "")
                        {
                            objUI.ExciseDuty = Convert.ToInt32(null);
                        }
                        else
                        {
                            objUI.ExciseDuty = Convert.ToInt32(dr["Excise Duty"]);
                        }

                        //objUI.Edu_Cess = Convert.ToInt32(dr["Edu# Cess"]);
                        //objUI.HR_ECess = Convert.ToInt32(dr["Hr#ECess"]);

                        string strEdu_Cess = Convert.ToString(dr["Edu# Cess"]);
                        if (strEdu_Cess == "")
                        {
                            objUI.Edu_Cess = Convert.ToInt32(null);
                        }
                        else
                        {
                            objUI.Edu_Cess = Convert.ToInt32(dr["Edu# Cess"]);
                        }

                        string strHRECess = Convert.ToString(dr["Hr#ECess"]);
                        if (strHRECess == "")
                        {
                            objUI.HR_ECess = Convert.ToInt32(null);
                        }
                        else
                        {
                            objUI.HR_ECess = Convert.ToInt32(dr["Hr#ECess"]);
                        }

                        string strLSPD = Convert.ToString(dr["LSPD"]);
                        if (strLSPD == "")
                        {
                            objUI.LSPD = Convert.ToInt32(null);
                        }
                        else
                        {
                            objUI.LSPD = Convert.ToInt32(dr["LSPD"]);
                        }

                        string strMSPSD = Convert.ToString(dr["MSPSD"]);
                        if (strMSPSD == "")
                        {
                            objUI.MSPSD = Convert.ToInt32(null);
                        }
                        else
                        {
                            objUI.MSPSD = Convert.ToInt32(dr["MSPSD"]);
                        }

                        string strDHC = Convert.ToString(dr["DHC"]);
                        if (strDHC == "")
                        {
                            objUI.DHC = Convert.ToInt32(null);
                        }
                        else
                        {
                            objUI.DHC = Convert.ToInt32(dr["DHC"]);
                        }

                        string strTaxable = Convert.ToString(dr["Taxable"]);
                        if (strTaxable == "")
                        {
                            objUI.Taxable = Convert.ToDouble(null);
                        }
                        else
                        {
                            objUI.Taxable = Convert.ToDouble(dr["Taxable"]);
                        }

                        string strCST = Convert.ToString(dr["CST"]);
                        if (strCST == "")
                        {
                            objUI.CST = Convert.ToDouble(null);
                        }
                        else
                        {
                            objUI.CST = Convert.ToDouble(dr["CST"]);
                        }

                        string strLST = Convert.ToString(dr["LST"]);
                        if (strLST == "")
                        {
                            objUI.LST = Convert.ToDouble(null);
                        }
                        else
                        {
                            objUI.LST = Convert.ToDouble(dr["LST"]);
                        }

                        string strSurch = Convert.ToString(dr["Surch"]);
                        if (strSurch == "")
                        {
                            objUI.Surch = Convert.ToInt32(null);
                        }
                        else
                        {
                            objUI.Surch = Convert.ToInt32(dr["Surch"]);
                        }

                        string strDely_Chgs = Convert.ToString(dr["Dely Chgs"]);
                        if (strDely_Chgs == "")
                        {
                            objUI.Dely_Chgs = Convert.ToInt32(null);
                        }
                        else
                        {
                            objUI.Dely_Chgs = Convert.ToInt32(dr["Dely Chgs"]);
                        }

                        string strEntityTot = Convert.ToString(dr["Enty/TOT"]);
                        if (strEntityTot == "")
                        {
                            objUI.EntityTot = Convert.ToDouble(null);
                        }
                        else
                        {
                            objUI.EntityTot = Convert.ToDouble(dr["Enty/TOT"]);
                        }

                        string strFreight = Convert.ToString(dr["Freight"]);
                        if (strFreight == "")
                        {
                            objUI.Freight = Convert.ToDouble(null);
                        }
                        else
                        {
                            objUI.Freight = Convert.ToDouble(dr["Freight"]);
                        }
                        string strNetAmount = Convert.ToString(dr["Net Amount"]);
                        if (strNetAmount == "")
                        {
                            objUI.Net_Amount = Convert.ToDouble(null);
                        }
                        else
                        {
                            objUI.Net_Amount = Convert.ToDouble(dr["Net Amount"]);
                        }

                        string strCost = Convert.ToString(dr["Cost"]);
                        if (strCost == "")
                        {
                            objUI.Cost = Convert.ToInt32(null);
                        }
                        else
                        {
                            objUI.Cost = Convert.ToInt32(dr["Cost"]);
                        }
                        objUI.SOff = Convert.ToString(dr["S#Off"]);
                        //objUI.FromDate = CalSalesfromDate.SelectedDate;
                        //objUI.ToDate = CalSalestoDate.SelectedDate;
                        objUI.FromDate = Convert.ToDateTime(objUtility.ConvertDateTime(txtSalesFromDate.Text.Trim()));
                        objUI.ToDate = Convert.ToDateTime(objUtility.ConvertDateTime(txtSalesToDate.Text.Trim()));

                        if (IsModelEx == 0)
                        {
                            objCont.SaveSales(objUI);
                            rcount++;
                        }
                        else
                        {
                            string strQuery = "Insert into SalesTemp ([Sno],[Inv#No],[Date],[DlrCode],[Dlr Name],[F10],[Model Code],[Qty],[Sale Amt],[Discount],[SPL#DIS],[Excise Duty],[Edu# Cess],[Hr#ECess],[LSPD],[MSPSD],[DHC],[Taxable],[CST],[LST],[Surch],[Enty/TOT],[Dely Chgs],[Freight],[Net Amount],[Cost],[S#off],[IsApproved],[FromDate],[ToDate],IsModelEx) ";
                            strQuery += "Values ('" + dr["Sno"].ToString() + "','" + dr["Inv#No"].ToString() + "','" + dr["Date"].ToString() + "','" + dr["DlrCode"].ToString() + "','" + dr["Dlr Name"].ToString() + "','" + dr["F10"].ToString() + "','" + dr["Model Code"].ToString() + "','" + dr["Qty"].ToString() + "','" + dr["Sale Amt"].ToString() + "','" + dr["Discount"].ToString() + "','" + dr["SPL#DIS"].ToString() + "','" + dr["Excise Duty"].ToString() + "','" + dr["Edu# Cess"].ToString() + "','" + dr["Hr#ECess"].ToString() + "','" + dr["LSPD"].ToString() + "','" + dr["MSPSD"].ToString() + "','" + dr["DHC"].ToString() + "','" + dr["Taxable"].ToString() + "','" + dr["CST"].ToString() + "','" + dr["LST"].ToString() + "','" + dr["Surch"].ToString() + "','" + dr["Enty/TOT"].ToString() + "','" + dr["Dely Chgs"].ToString() + "','" + dr["Freight"].ToString() + "','" + dr["Net Amount"].ToString() + "','" + dr["Cost"].ToString() + "','" + dr["S#off"] + "',0,'" + objUtility.ConvertDateTime(txtSalesFromDate.Text.Trim()) + "','" + objUtility.ConvertDateTime(txtSalesToDate.Text.Trim()) + "'," + IsModelEx + ")";
                            objController.ExecuteQuery(strQuery);
                        }
                        
                        
                        lblMessage = "File Sales saved successfully;No of Rows Affected:" + rcount;
                    }
                    catch
                    {
                        lnkSalesException.Visible = true;
                        string strQuery = "Insert into SalesTemp ([Sno],[Inv#No],[Date],[DlrCode],[Dlr Name],[F10],[Model Code],[Qty],[Sale Amt],[Discount],[SPL#DIS],[Excise Duty],[Edu# Cess],[Hr#ECess],[LSPD],[MSPSD],[DHC],[Taxable],[CST],[LST],[Surch],[Enty/TOT],[Dely Chgs],[Freight],[Net Amount],[Cost],[S#off],[IsApproved],[FromDate],[ToDate],IsModelEx) ";
                        strQuery += "Values ('" + dr["Sno"].ToString() + "','" + dr["Inv#No"].ToString() + "','" + dr["Date"].ToString() + "','" + dr["DlrCode"].ToString() + "','" + dr["Dlr Name"].ToString() + "','" + dr["F10"].ToString() + "','" + dr["Model Code"].ToString() + "','" + dr["Qty"].ToString() + "','" + dr["Sale Amt"].ToString() + "','" + dr["Discount"].ToString() + "','" + dr["SPL#DIS"].ToString() + "','" + dr["Excise Duty"].ToString() + "','" + dr["Edu# Cess"].ToString() + "','" + dr["Hr#ECess"].ToString() + "','" + dr["LSPD"].ToString() + "','" + dr["MSPSD"].ToString() + "','" + dr["DHC"].ToString() + "','" + dr["Taxable"].ToString() + "','" + dr["CST"].ToString() + "','" + dr["LST"].ToString() + "','" + dr["Surch"].ToString() + "','" + dr["Enty/TOT"].ToString() + "','" + dr["Dely Chgs"].ToString() + "','" + dr["Freight"].ToString() + "','" + dr["Net Amount"].ToString() + "','" + dr["Cost"].ToString() + "','" + dr["S#off"] + "',0,'" + objUtility.ConvertDateTime(txtSalesFromDate.Text.Trim()) + "','" + objUtility.ConvertDateTime(txtSalesToDate.Text.Trim()) + "'," + IsModelEx + ")";
                        objController.ExecuteQuery(strQuery);
                    }

                }

            }
        }
        return lblMessage;
    }

    /***********************************Function to get Culprit File into DataTable***********************************/
    public string getCulprit()
    {
        string lblMessage = "";
        string strPath = Server.MapPath("~/") + "UploadFile\\Culprit.xls";
        string excelConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strPath;


        excelConnectionString += @";Extended Properties=""Excel 8.0;HDR=YES;IMEX=1;""";
        OleDbConnection connection = new OleDbConnection(excelConnectionString);
        string Name = "";
        try
        {
            connection.Open();
            DataTable SheetName = new DataTable();
            SheetName = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string[] excelSheets = new string[SheetName.Rows.Count];
            int i = 0;
            // Add the sheet name to the string array.
            foreach (DataRow row in SheetName.Rows)
            {
                excelSheets[i] = SheetName.Rows[0]["TABLE_NAME"].ToString();
                Name = excelSheets[i];
            }

            DataTable dtNew = new DataTable();

            string query = "Select * FROM [" + Name + "]";

            OleDbCommand command = new OleDbCommand(query, connection);
            OleDbDataAdapter dr = new OleDbDataAdapter(command);
            DataTable dt = new DataTable();

            dr.Fill(dt);
            dtNew = dt;

            //Session.Add("dtNew", dtNew);

            lblMessage = SaveCulprit(dtNew);
            connection.Close();
            return lblMessage;


        }
        catch(Exception ex)
        {
            connection.Close();
            string strMessage = ex.Message.ToString();

            if (strMessage == "External table is not in the expected format." || strMessage.Contains("does not belong to table"))
            {
                lblMessage = "Excel file is not in valid format";
            }
            else
            {
                lblMessage = "File Culprit could not be saved successfully";
            }
           
            return lblMessage;
        }


    }

    /***********************************Function to Save Culprit File into Sql Server 2000 using Bulk Copy Method***********************************/
    public string SaveCulprit(DataTable dt)
    {
        string lblMessage = "";
        MastersController objCont = new MastersController();
        string sConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        int Status = 0;
        try
        {
            string strDelete = "Delete from CulpritTemp";
            objController.ExecuteQuery(strDelete);

            SqlConnection sqlconn = new SqlConnection(sConnectionString);
            sqlconn.Open();
            SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlconn);
            bulkCopy.DestinationTableName = "CulpritTemp";
            bulkCopy.WriteToServer(dt);
            Status = objCont.SaveCulpritData();
            if (Status == 1)
            {
                //Ask User if the duplicates are to be replaced
                //if Yes then delete 
                Page.RegisterStartupScript("OnBlock", "<script language='javascript'>getStatus('culprit');</script>");
                Session.Add("dtculprit", dt);
            }
            else
            {
                lblMessage = "File Culprit saved successfully";
            }
        }
        catch(Exception ex)
        {
           // lblMessage = "File Culprit could not be saved successfully";
            string strMessage = ex.Message.ToString();
            if (strMessage.Contains("Cannot insert duplicate key in object"))
            {
                lblMessage = "Cannot insert duplicate values";
            }
            else
            {
                lblMessage = "Excel file is not in valid format";
            }
        }

        return lblMessage;
    }

    /***********************************Function to Get Defect File into DataTable***********************************/
    public string getDefect()
    {
        string lblMessage = "";
        string strPath = Server.MapPath("~/") + "UploadFile\\Defect.xls";
        string excelConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strPath;


        excelConnectionString += @";Extended Properties=""Excel 8.0;HDR=YES;IMEX=1;""";
        OleDbConnection connection = new OleDbConnection(excelConnectionString);
        string Name = "";
        try
        {
            connection.Open();
            DataTable SheetName = new DataTable();
            SheetName = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string[] excelSheets = new string[SheetName.Rows.Count];
            int i = 0;
            // Add the sheet name to the string array.
            foreach (DataRow row in SheetName.Rows)
            {

                excelSheets[i] = SheetName.Rows[0]["TABLE_NAME"].ToString();
                Name = excelSheets[i];
            }

            DataTable dtNew = new DataTable();

            string query = "Select * FROM [" + Name + "]";

            OleDbCommand command = new OleDbCommand(query, connection);
            OleDbDataAdapter dr = new OleDbDataAdapter(command);
            DataTable dt = new DataTable();

            dr.Fill(dt);
            dtNew = dt;

            //Session.Add("dtNew", dtNew);

            lblMessage = SaveDefect(dtNew);
            connection.Close();
            return lblMessage;


        }
        catch (Exception ex)
        {
            connection.Close();
            string strMessage = ex.Message.ToString();

            if (strMessage == "External table is not in the expected format." || strMessage.Contains("does not belong to table"))
            {
                lblMessage = "Excel file is not in valid format";
            }
            else
            {
                lblMessage = "File Defect could not be saved successfully";
            }
           
          
            return lblMessage;
        }
    }

    /***********************************Function to Save Defect File into Sql Server 2000 using Bulk Copy Method***********************************/
    public string SaveDefect(DataTable dt)
    {

        string lblMessage = "";
        MastersController objCont = new MastersController();
        string sConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        int Status = 0;
        try
        {
            string strDelete = "Delete from DefectTemp";
            objController.ExecuteQuery(strDelete);

            SqlConnection sqlconn = new SqlConnection(sConnectionString);
            sqlconn.Open();
            SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlconn);
            bulkCopy.DestinationTableName = "DefectTemp";
            bulkCopy.WriteToServer(dt);
            Status = objCont.SaveDefectData();
            if (Status == 1)
            {
                //Ask User if the duplicates are to be replaced
                //if Yes then delete 
                Page.RegisterStartupScript("OnBlock", "<script language='javascript'>getStatus('defect');</script>");
                Session.Add("dtdefect", dt);
            }
            else
            {
                lblMessage = "File Defect saved successfully"; 
            }
        }
        catch(Exception ex)
        {
            //lblMessage = "File Defect could not be saved successfully";
            string strMessage = ex.Message.ToString();
            if (strMessage.Contains("Cannot insert duplicate key in object"))
            {
                lblMessage = "Cannot insert duplicate values";
            }
            else
            {
                lblMessage = "Excel file is not in valid format";
            }

        }

        return lblMessage;
    }

    /***********************************Function to Get Customer Voice File into DataTable***********************************/
    public string getCustomerVoice()
    {
        string lblMessage = "";
        string strPath = Server.MapPath("~/") + "UploadFile\\Customer Voice.xls";
        string excelConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strPath;


        excelConnectionString += @";Extended Properties=""Excel 8.0;HDR=YES;IMEX=1;""";
        OleDbConnection connection = new OleDbConnection(excelConnectionString);
           

        string Name = "";
        try
        {
            connection.Open();
            DataTable SheetName = new DataTable();
            SheetName = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string[] excelSheets = new string[SheetName.Rows.Count];
            int i = 0;
            // Add the sheet name to the string array.
            foreach (DataRow row in SheetName.Rows)
            {

                excelSheets[i] = SheetName.Rows[0]["TABLE_NAME"].ToString();
                Name = excelSheets[i];
            }

            DataTable dtNew = new DataTable();

            string query = "Select * FROM [" + Name + "]";

            OleDbCommand command = new OleDbCommand(query, connection);
            OleDbDataAdapter dr = new OleDbDataAdapter(command);
            DataTable dt = new DataTable();

            dr.Fill(dt);
            dtNew = dt;

            //Session.Add("dtNew", dtNew);

            lblMessage = SaveCustomerVoice(dtNew);
            connection.Close();
           return lblMessage;


        }
        catch (Exception ex)
        {
            connection.Close();
            string strMessage = ex.Message.ToString();

            if (strMessage == "External table is not in the expected format." || strMessage.Contains("does not belong to table"))
            {
                lblMessage = "Excel file is not in valid format";
            }
            else
            {
                lblMessage = "File Customer Voice could not be saved successfully";
            }
           
            return lblMessage;
        }


    }

    /***********************************Function to Save Customer Voice File into Sql Server 2000 using Bulk Copy Method***********************************/
    public string SaveCustomerVoice(DataTable dt)
    {

        string lblMessage = "";
        MastersController objCont = new MastersController();
        string sConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        int Status = 0;
        try
        {
            string strDelete = "Delete from CustomerVoiceTemp";
            objController.ExecuteQuery(strDelete);

            SqlConnection sqlconn = new SqlConnection(sConnectionString);
            sqlconn.Open();
            SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlconn);
            bulkCopy.DestinationTableName = "CustomerVoiceTemp";
            bulkCopy.WriteToServer(dt);
            Status = objCont.SaveCustomerVoiceData();
            if (Status == 1)
            {
                //Ask User if the duplicates are to be replaced
                //if Yes then delete 
                Page.RegisterStartupScript("OnBlock", "<script language='javascript'>getStatus('customervoice');</script>");
                Session.Add("dtcustvoice", dt);
            }
            else
            {
                lblMessage = "File Customer Voice saved successfully";
            }
        }
        catch(Exception ex)
        {
           // lblMessage = "File Customer Voice could not be saved successfully";
            string strMessage = ex.Message.ToString();
            if (strMessage.Contains("Cannot insert duplicate key in object"))
            {
                lblMessage = "Cannot insert duplicate values";
            }
            else
            {
                lblMessage = "Excel file is not in valid format";
            }
        }
        return lblMessage;
    }

    /***********************************Function to Get Item File into DataTable***********************************/
    public string getItem()
    {

        string lblMessage = "";
        string strPath = Server.MapPath("~/") + "UploadFile\\Item.xls";
        string excelConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strPath;


        excelConnectionString += @";Extended Properties=""Excel 8.0;HDR=YES;IMEX=1;""";
        OleDbConnection connection = new OleDbConnection(excelConnectionString);
       
        string Name = "";
        try
        {
            connection.Open();
            DataTable SheetName = new DataTable();
            SheetName = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string[] excelSheets = new string[SheetName.Rows.Count];
            int i = 0;
            // Add the sheet name to the string array.
            foreach (DataRow row in SheetName.Rows)
            {

                excelSheets[i] = SheetName.Rows[0]["TABLE_NAME"].ToString();
                Name = excelSheets[i];
            }

            DataTable dtNew = new DataTable();

            string query = "Select * FROM [" + Name + "]";

            OleDbCommand command = new OleDbCommand(query, connection);
            OleDbDataAdapter dr = new OleDbDataAdapter(command);
            DataTable dt = new DataTable();

            dr.Fill(dt);
            dtNew = dt;

            //Session.Add("dtNew", dtNew);

            lblMessage = SaveItem(dtNew);
            connection.Close();
            return lblMessage;


        }
        catch (Exception ex)
        {
            connection.Close();
            string strMessage = ex.Message.ToString();

            if (strMessage == "External table is not in the expected format." || strMessage.Contains("does not belong to table"))
            {
                lblMessage = "Excel file is not in valid format";
            }
            else
            {
                lblMessage = "File Item could not be saved successfully";
            }
           
           
          return lblMessage;
        }


    }

    /***********************************Function to Save Item File into Sql Server 2000 using Bulk Copy Method***********************************/
    public string SaveItem(DataTable dt)
    {

        string lblMessage = "";
        MastersController objCont = new MastersController();
        string sConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        int Status = 0;
        try
        {
            string strDelete = "Delete from ItemTemp";
            objController.ExecuteQuery(strDelete);

            SqlConnection sqlconn = new SqlConnection(sConnectionString);
            sqlconn.Open();
            SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlconn);
            bulkCopy.DestinationTableName = "ItemTemp";
            bulkCopy.WriteToServer(dt);
            Status = objCont.SaveItemData();
            if (Status == 1)
            {
                //Ask User if the duplicates are to be replaced
                //if Yes then delete 

                Page.RegisterStartupScript("OnBlock", "<script language='javascript'>getStatus('item');</script>");
                Session.Add("dtitem", dt);
            }
            else
            {
                string strItemException = "Declare @cnt int exec usp_AcrBulkDataInsert @cnt out";
                objController.ExecuteQuery(strItemException);
                lblMessage = "File Item saved successfully";
            }
        }
        catch(Exception ex)
        {
            string strMessage = ex.Message.ToString();
            if (strMessage.Contains("Cannot insert duplicate key in object"))
            {
                lblMessage = "Cannot insert duplicate values";
            }
            else
            {
                lblMessage = "Excel file is not in valid format";
            }
                //"File Item could not be saved successfully";
        }
        return lblMessage;
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

  
    protected void lnkExceptions_Click(object sender, EventArgs e)
    {
        string strProjectName = System.Configuration.ConfigurationManager.AppSettings["WMSProjectName"];
        Response.Redirect(strProjectName + "/View/forms/Exceptions/Exception.aspx");
        
    }
    protected void lnkProdException_Click(object sender, EventArgs e)
    {
        string strProjectName = System.Configuration.ConfigurationManager.AppSettings["WMSProjectName"];
        Response.Redirect(strProjectName + "/View/forms/Exceptions/Exception.aspx");
    }
    protected void lnkSalesException_Click(object sender, EventArgs e)
    {
        string strProjectName = System.Configuration.ConfigurationManager.AppSettings["WMSProjectName"];
        Response.Redirect(strProjectName + "/View/forms/Exceptions/Exception.aspx");
    }
    
}
