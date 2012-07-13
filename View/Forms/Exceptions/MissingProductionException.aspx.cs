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

public partial class View_Forms_Exceptions_MissingProductionException : System.Web.UI.Page
{
    
    QueryConroller objQueryController = new QueryConroller();
    Utility objUtility = new Utility();
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
            string strQuery = "Select * from productiontemp";
            DataTable dt = new DataTable();
            dt = objQueryController.ExecuteQuery(strQuery);

            grdProdException.DataSource = dt;
            grdProdException.DataBind();
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
        DataTable dtProdMonth = new DataTable();
        string strProdMonthQuery = "select * from ProductionMonth";
        dtProdMonth = objQueryController.ExecuteQuery(strProdMonthQuery);

        DataTable dtView = new DataTable();
        string strViewQuery = "Select distinct * from vwMastersCode";
        dtView = objQueryController.ExecuteQuery(strViewQuery);

        //DataTable dtModel = new DataTable();
        //string strModelQuery = "select * from Model";
        //dtModel = objQueryController.ExecuteQuery(strModelQuery);
        ProductionController objCont = new ProductionController();

        int rcount = 0;
        foreach (GridViewRow gr in grdProdException.Rows)
        {
            bool Discard = ((CheckBox)gr.FindControl("chkDiscard")).Checked;
            string str = ((HiddenField)gr.FindControl("hdnID")).Value;
            if (Discard == true)
            {
                string strDiscardQuery = "Delete from ProductionTemp where ID=" + Convert.ToInt16(str);
                objQueryController.ExecuteQuery(strDiscardQuery);

                string strQuery = "Select * from Productiontemp";
                DataTable dt = new DataTable();
                dt = objQueryController.ExecuteQuery(strQuery);

                grdProdException.DataSource = dt;
                grdProdException.DataBind();
            }
            else
            {

                ProductionUI objUI = new ProductionUI();
                int CurrentYearID = 0;
                int CurrentMonthID = 0;
                string strProductionMonthYear = "";
                int modelex=1;
                string strQuarterYear = "";
                string ProductionMonth = "";
                try
                {
                    string strS = Convert.ToString(((TextBox)gr.FindControl("txtS")).Text);
                    if (strS == "")
                    {
                        objUI.S = Convert.ToInt32(null);
                    }
                    else
                    {
                        objUI.S = Convert.ToInt32(((TextBox)gr.FindControl("txtS")).Text);
                    }

                    objUI.Material = Convert.ToString(((TextBox)gr.FindControl("txtMaterial")).Text);
                    objUI.SerialNo = Convert.ToString(((TextBox)gr.FindControl("txtSerialNo")).Text.Trim());
                    objUI.Plnt = Convert.ToString(((TextBox)gr.FindControl("txtPlnt")).Text);

                    string strSLoc = Convert.ToString(((TextBox)gr.FindControl("txtSLoc")).Text);
                    if (strSLoc == "")
                    {
                        objUI.SLoc = Convert.ToString(null);
                    }
                    else
                    {
                        objUI.SLoc = Convert.ToString(((TextBox)gr.FindControl("txtSLoc")).Text);
                    }
                    objUI.Description = Convert.ToString(((TextBox)gr.FindControl("txtDescription")).Text);
             
                    string strSerialNo = Convert.ToString(((TextBox)gr.FindControl("txtSerialNo")).Text.Trim());
                    int serialnoLength = strSerialNo.Length;
                    
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
                    //objUI.Production_Month = Convert.ToInt16(ProductionMonth);
                    //try
                    //{
                    //    objUI.Model_Code = Convert.ToString(dtModelCode.Rows[0]["Model_Code"]);
                    //}
                    //catch 
                    //{
                        
                    //}
                    int flag = 0;
                    //for (int i = 0; i < dtView.Rows.Count; i++)
                    //{
                    //    if ("model" == dtView.Rows[i]["tablename"].ToString() && Convert.ToString(ModelCode) == dtView.Rows[i]["code"].ToString())
                    //    {
                    //        DataView dv = new DataView(dtModel);
                    //        dv.RowFilter = "Code ='" + Convert.ToString(ModelCode) + "'";
                    //        DataTable dtModelCode = dv.ToTable();
                    //        objUI.Model_Code = Convert.ToString(dtModelCode.Rows[0]["Model_Code"]);
                    //        //strCause = strCause.Replace("model", "");
                    //        //modelex = 0;
                    //        flag++;
                    //    }
                    //}
                    string strMaterial = Convert.ToString(((TextBox)gr.FindControl("txtMaterial")).Text);
                    string strModelQuery = "Select * from ModelMapping where Material='" + strMaterial + "'";
                    DataTable dtModel = objQueryController.ExecuteQuery(strModelQuery);
                    if (dtModel != null)
                    {
                        if (dtModel.Rows.Count > 0)
                        {
                            foreach (DataRow drModel in dtModel.Rows)
                            {
                                objUI.ModelMappingID = Convert.ToInt32(drModel["ID"].ToString());
                                //strCause = strCause.Replace("model", "");
                                modelex = 0;
                                flag++;
                            }
                        }
                    }

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

                        string strCurrentYearID = (Convert.ToString(CurrentYearID)).Substring(2, 2);

                        string strMonth = getMonth(CurrentMonthID);

                        strProductionMonthYear = strMonth + "-" + strCurrentYearID;

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
                        objUI.Production_Month = Convert.ToInt32(ProductionMonth);
                        objUI.Production_Month_Year = strProductionMonthYear;
                        objUI.FromDate = Convert.ToDateTime(objUtility.ConvertDateTime(gr.Cells[6].Text));
                        objUI.ToDate = Convert.ToDateTime(objUtility.ConvertDateTime(gr.Cells[7].Text));
                        //string strProductionMonthYear = strMonth + "-" + strCurrentYearID;
                       // objUI.Production_Month_Year = strProductionMonthYear;
                    }

                    if (flag == 1)
                    {
                        objCont.SaveProduction(objUI);
                        rcount++;
                    }
                    else
                    {
                        
                        string strQuery = "Insert into ProductionTemp ([Material],[Serial no#],[Plnt],[SLoc],[Description of technical object],[Production_Month],[Production_Month_Year],[IsApproved],MonthID,YearID,[Quarter],IsModelEx,FromDate,ToDate) ";
                        strQuery += "Values ('" + ((TextBox)gr.FindControl("txtMaterial")).Text + "','" + ((TextBox)gr.FindControl("txtSerialNo")).Text + "','" + ((TextBox)gr.FindControl("txtPlnt")).Text + "','" + ((TextBox)gr.FindControl("txtSloc")).Text + "','" + ((TextBox)gr.FindControl("txtDescription")).Text + "','" + ProductionMonth + "','" + strProductionMonthYear + "','0','"+ CurrentMonthID +"','"+ CurrentYearID +"','"+ strQuarterYear +"','"+ modelex +"','" + Convert.ToDateTime(objUtility.ConvertDateTime(gr.Cells[6].Text)) + "','" + Convert.ToDateTime(objUtility.ConvertDateTime(gr.Cells[7].Text)) + "')";
                        objQueryController.ExecuteQuery(strQuery);
                    }
                  
                    
                }
                catch
                {

                    string strQuery = "Insert into ProductionTemp ([Material],[Serial no#],[Plnt],[SLoc],[Description of technical object],[Production_Month],[Production_Month_Year],[IsApproved],MonthID,YearID,[Quarter],IsModelEx,FromDate,ToDate) ";
                    strQuery += "Values ('" + ((TextBox)gr.FindControl("txtMaterial")).Text + "','" + ((TextBox)gr.FindControl("txtSerialNo")).Text + "','" + ((TextBox)gr.FindControl("txtPlnt")).Text + "','" + ((TextBox)gr.FindControl("txtSloc")).Text + "','" + ((TextBox)gr.FindControl("txtDescription")).Text + "','" + ProductionMonth + "','" + strProductionMonthYear + "','0','" + CurrentMonthID + "','" + CurrentYearID + "','" + strQuarterYear + "','" + modelex + "','" + Convert.ToDateTime(objUtility.ConvertDateTime(gr.Cells[6].Text)) + "','" + Convert.ToDateTime(objUtility.ConvertDateTime(gr.Cells[7].Text)) + "')";
                    objQueryController.ExecuteQuery(strQuery);
                }
                string strDeleteQuery = "Delete from ProductionTemp where ID=" + Convert.ToInt16(str);
                objQueryController.ExecuteQuery(strDeleteQuery);

                string strGridQuery = "Select * from Productiontemp";
                DataTable dt = new DataTable();
                dt = objQueryController.ExecuteQuery(strGridQuery);

                grdProdException.DataSource = dt;
                grdProdException.DataBind();
            }
        }
       
    }
}
