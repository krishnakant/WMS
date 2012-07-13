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

public partial class View_Forms_Exceptions_MissingSalesException : System.Web.UI.Page
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
            string strQuery = "Select * from salestemp";
            DataTable dt = new DataTable();
            dt = objQueryController.ExecuteQuery(strQuery);

            grdSalesException.DataSource = dt;
            grdSalesException.DataBind();
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        SalesController objCont = new SalesController();
        int rcount = 0;
        foreach (GridViewRow gr in grdSalesException.Rows)
        {
            bool Discard = ((CheckBox)gr.FindControl("chkDiscard")).Checked;
            string str = ((HiddenField)gr.FindControl("hdnID")).Value;
            if (Discard == true)
            {
                string strDiscardQuery = "Delete from SalesTemp where ID=" + Convert.ToInt16(str);
                objQueryController.ExecuteQuery(strDiscardQuery);

                string strQuery = "Select * from SalesTemp";
                DataTable dt = new DataTable();
                dt = objQueryController.ExecuteQuery(strQuery);

                grdSalesException.DataSource = dt;
                grdSalesException.DataBind();
            }
            else
            {
                SalesUI objUI = new SalesUI();
                try
                {
                    objUI.Sno = Convert.ToInt16(((TextBox)gr.FindControl("txtSno")).Text);
                    objUI.InvoiceNo = Convert.ToInt32(((TextBox)gr.FindControl("txtInvoiceNo")).Text);
                    //objUI.Date = Convert.ToString(((TextBox)gr.FindControl("txtDate")).Text);
                    string strDate = Convert.ToString(((TextBox)gr.FindControl("txtDate")).Text);
                    if (strDate == "" || strDate == "00.00.0000")
                    {
                        objUI.Date = Convert.ToDateTime("1/1/2001");
                    }
                    else
                    {
                        objUI.Date = Convert.ToDateTime(ConvertDateTime(((TextBox)gr.FindControl("txtDate")).Text));
                    }
                    objUI.Dealer_Code = Convert.ToString(((TextBox)gr.FindControl("txtDealerCode")).Text);
                    objUI.Dealer_Name = Convert.ToString(((TextBox)gr.FindControl("txtDealerName")).Text);
                    objUI.Blank = Convert.ToString(((TextBox)gr.FindControl("txtBlank")).Text);
                    string strMaterial = Convert.ToString(((TextBox)gr.FindControl("txtBlank")).Text);
                    objUI.Model_Code = Convert.ToString(((TextBox)gr.FindControl("txtModelCode")).Text);
                    objUI.Quantity = Convert.ToInt16(((TextBox)gr.FindControl("txtQuantity")).Text);
                    objUI.SalesAmount = Convert.ToDouble(((TextBox)gr.FindControl("txtSalesAmt")).Text);

                    string strDiscount = Convert.ToString(((TextBox)gr.FindControl("txtDiscount")).Text);
                    if (strDiscount == "")
                    {
                        objUI.Discount = Convert.ToInt32(null);
                    }
                    else
                    {
                        objUI.Discount = Convert.ToInt32(((TextBox)gr.FindControl("txtDiscount")).Text);
                    }

                    string strSPLDIS = Convert.ToString(((TextBox)gr.FindControl("txtSPLDIS")).Text);
                    if (strSPLDIS == "")
                    {
                        objUI.SPLDIS = Convert.ToDouble(null);
                    }
                    else
                    {
                        objUI.SPLDIS = Convert.ToDouble(((TextBox)gr.FindControl("txtSPLDIS")).Text);
                    }

                    string strExciseDuty = Convert.ToString(((TextBox)gr.FindControl("txtExciseDuty")).Text);
                    if (strExciseDuty == "")
                    {
                        objUI.ExciseDuty = Convert.ToInt16(null);
                    }
                    else
                    {
                        objUI.ExciseDuty = Convert.ToInt16(((TextBox)gr.FindControl("txtExciseDuty")).Text);
                    }

                    objUI.Edu_Cess = Convert.ToInt16(((TextBox)gr.FindControl("txtEduCess")).Text);
                    objUI.HR_ECess = Convert.ToInt16(((TextBox)gr.FindControl("txtHrECess")).Text);

                    string strLSPD = Convert.ToString(((TextBox)gr.FindControl("txtLSPD")).Text);
                    if (strLSPD == "")
                    {
                        objUI.LSPD = Convert.ToInt16(null);
                    }
                    else
                    {
                        objUI.LSPD = Convert.ToInt16(((TextBox)gr.FindControl("txtLSPD")).Text);
                    }

                    string strMSPSD = Convert.ToString(((TextBox)gr.FindControl("txtMSPSD")).Text);
                    if (strMSPSD == "")
                    {
                        objUI.MSPSD = Convert.ToInt16(null);
                    }
                    else
                    {
                        objUI.MSPSD = Convert.ToInt16(((TextBox)gr.FindControl("txtMSPSD")).Text);
                    }

                    string strDHC = Convert.ToString(((TextBox)gr.FindControl("txtDHC")).Text);
                    if (strDHC == "")
                    {
                        objUI.DHC = Convert.ToInt16(null);
                    }
                    else
                    {
                        objUI.DHC = Convert.ToInt16(((TextBox)gr.FindControl("txtDHC")).Text);
                    }

                    string strTaxable = Convert.ToString(((TextBox)gr.FindControl("txtTaxable")).Text);
                    if (strTaxable == "")
                    {
                        objUI.Taxable = Convert.ToDouble(null);
                    }
                    else
                    {
                        objUI.Taxable = Convert.ToDouble(((TextBox)gr.FindControl("txtTaxable")).Text);
                    }

                    string strCST = Convert.ToString(((TextBox)gr.FindControl("txtCST")).Text);
                    if (strCST == "")
                    {
                        objUI.CST = Convert.ToDouble(null);
                    }
                    else
                    {
                        objUI.CST = Convert.ToDouble(((TextBox)gr.FindControl("txtCST")).Text);
                    }

                    string strLST = Convert.ToString(((TextBox)gr.FindControl("txtLST")).Text);
                    if (strLST == "")
                    {
                        objUI.LST = Convert.ToDouble(null);
                    }
                    else
                    {
                        objUI.LST = Convert.ToDouble(((TextBox)gr.FindControl("txtLST")).Text);
                    }

                    string strSurch = Convert.ToString(((TextBox)gr.FindControl("txtSurch")).Text);
                    if (strSurch == "")
                    {
                        objUI.Surch = Convert.ToInt16(null);
                    }
                    else
                    {
                        objUI.Surch = Convert.ToInt16(((TextBox)gr.FindControl("txtSurch")).Text);
                    }

                    string strDely_Chgs = Convert.ToString(((TextBox)gr.FindControl("txtDelyChgs")).Text);
                    if (strDely_Chgs == "")
                    {
                        objUI.Dely_Chgs = Convert.ToInt16(null);
                    }
                    else
                    {
                        objUI.Dely_Chgs = Convert.ToInt16(((TextBox)gr.FindControl("txtDelyChgs")).Text);
                    }

                    string strEntityTot = Convert.ToString(((TextBox)gr.FindControl("txtEntityTot")).Text);
                    if (strEntityTot == "")
                    {
                        objUI.EntityTot = Convert.ToInt16(null);
                    }
                    else
                    {
                        objUI.EntityTot = Convert.ToInt16(((TextBox)gr.FindControl("txtEntityTot")).Text);
                    }

                    string strFreight = Convert.ToString(((TextBox)gr.FindControl("txtFreight")).Text);
                    if (strFreight == "")
                    {
                        objUI.Freight = Convert.ToDouble(null);
                    }
                    else
                    {
                        objUI.Freight = Convert.ToDouble(((TextBox)gr.FindControl("txtFreight")).Text);
                    }
                    objUI.Net_Amount = Convert.ToDouble(((TextBox)gr.FindControl("txtNetAmt")).Text);
                    objUI.Cost = Convert.ToInt16(((TextBox)gr.FindControl("txtCost")).Text);
                    objUI.SOff = Convert.ToString(((TextBox)gr.FindControl("txtSOff")).Text);
                    objUI.FromDate = Convert.ToDateTime(gr.Cells[27].Text);
                    objUI.ToDate = Convert.ToDateTime(gr.Cells[28].Text);
                    string strModelQuery = "Select * from ModelMapping where Material='" + strMaterial + "'";
                    DataTable dtModel = objQueryController.ExecuteQuery(strModelQuery);
                    if (dtModel != null)
                    {
                        if (dtModel.Rows.Count > 0)
                        {
                            foreach (DataRow drModel in dtModel.Rows)
                            {
                                objUI.ModelMappingID = Convert.ToInt32(drModel["ID"].ToString());
                            }

                            objCont.SaveSales(objUI);
                            rcount++;
                        }
                        else
                        {
                            string strQuery = "Insert into SalesTemp ([Sno],[Inv#No],[Date],[DlrCode],[Dlr Name],[F10],[Model Code],[Qty],[Sale Amt],[Discount],[SPL#DIS],[Excise Duty],[Edu# Cess],[Hr#ECess],[LSPD],[MSPSD],[DHC],[Taxable],[CST],[LST],[Surch],[Enty/TOT],[Dely Chgs],[Freight],[Net Amount],[Cost],[S#off],[FromDate],[ToDate])";
                            strQuery += "Values ('" + ((TextBox)gr.FindControl("txtSno")).Text + "','" + ((TextBox)gr.FindControl("txtInvoiceNo")).Text + "','" + ((TextBox)gr.FindControl("txtDate")).Text + "','" + ((TextBox)gr.FindControl("txtDealerCode")).Text + "','" + ((TextBox)gr.FindControl("txtDealerName")).Text + "','" + ((TextBox)gr.FindControl("txtBlank")).Text + "','" + ((TextBox)gr.FindControl("txtModelCode")).Text + "','" + ((TextBox)gr.FindControl("txtQuantity")).Text + "','" + ((TextBox)gr.FindControl("txtSalesAmt")).Text + "','" + ((TextBox)gr.FindControl("txtDiscount")).Text + "','" + ((TextBox)gr.FindControl("txtSPLDIS")).Text + "','" + ((TextBox)gr.FindControl("txtExciseDuty")).Text + "','" + ((TextBox)gr.FindControl("txtEduCess")).Text + "','" + ((TextBox)gr.FindControl("txtHrECess")).Text + "','" + ((TextBox)gr.FindControl("txtLSPD")).Text + "','" + ((TextBox)gr.FindControl("txtMSPSD")).Text + "','" + ((TextBox)gr.FindControl("txtDHC")).Text + "','" + ((TextBox)gr.FindControl("txtTaxable")).Text + "','" + ((TextBox)gr.FindControl("txtCST")).Text + "','" + ((TextBox)gr.FindControl("txtLST")).Text + "','" + ((TextBox)gr.FindControl("txtSurch")).Text + "','" + ((TextBox)gr.FindControl("txtEntityTot")).Text + "','" + ((TextBox)gr.FindControl("txtDelyChgs")).Text + "','" + ((TextBox)gr.FindControl("txtFreight")).Text + "','" + ((TextBox)gr.FindControl("txtNetAmt")).Text + "','" + ((TextBox)gr.FindControl("txtCost")).Text + "','" + ((TextBox)gr.FindControl("txtSOff")).Text + "','" + Convert.ToDateTime(gr.Cells[27].Text) + "','" + Convert.ToDateTime(gr.Cells[28].Text) + "')";
                            objQueryController.ExecuteQuery(strQuery);
                        }

                    }
                    else
                    {
                        string strQuery = "Insert into SalesTemp ([Sno],[Inv#No],[Date],[DlrCode],[Dlr Name],[F10],[Model Code],[Qty],[Sale Amt],[Discount],[SPL#DIS],[Excise Duty],[Edu# Cess],[Hr#ECess],[LSPD],[MSPSD],[DHC],[Taxable],[CST],[LST],[Surch],[Enty/TOT],[Dely Chgs],[Freight],[Net Amount],[Cost],[S#off],[FromDate],[ToDate])";
                        strQuery += "Values ('" + ((TextBox)gr.FindControl("txtSno")).Text + "','" + ((TextBox)gr.FindControl("txtInvoiceNo")).Text + "','" + ((TextBox)gr.FindControl("txtDate")).Text + "','" + ((TextBox)gr.FindControl("txtDealerCode")).Text + "','" + ((TextBox)gr.FindControl("txtDealerName")).Text + "','" + ((TextBox)gr.FindControl("txtBlank")).Text + "','" + ((TextBox)gr.FindControl("txtModelCode")).Text + "','" + ((TextBox)gr.FindControl("txtQuantity")).Text + "','" + ((TextBox)gr.FindControl("txtSalesAmt")).Text + "','" + ((TextBox)gr.FindControl("txtDiscount")).Text + "','" + ((TextBox)gr.FindControl("txtSPLDIS")).Text + "','" + ((TextBox)gr.FindControl("txtExciseDuty")).Text + "','" + ((TextBox)gr.FindControl("txtEduCess")).Text + "','" + ((TextBox)gr.FindControl("txtHrECess")).Text + "','" + ((TextBox)gr.FindControl("txtLSPD")).Text + "','" + ((TextBox)gr.FindControl("txtMSPSD")).Text + "','" + ((TextBox)gr.FindControl("txtDHC")).Text + "','" + ((TextBox)gr.FindControl("txtTaxable")).Text + "','" + ((TextBox)gr.FindControl("txtCST")).Text + "','" + ((TextBox)gr.FindControl("txtLST")).Text + "','" + ((TextBox)gr.FindControl("txtSurch")).Text + "','" + ((TextBox)gr.FindControl("txtEntityTot")).Text + "','" + ((TextBox)gr.FindControl("txtDelyChgs")).Text + "','" + ((TextBox)gr.FindControl("txtFreight")).Text + "','" + ((TextBox)gr.FindControl("txtNetAmt")).Text + "','" + ((TextBox)gr.FindControl("txtCost")).Text + "','" + ((TextBox)gr.FindControl("txtSOff")).Text + "','" + Convert.ToDateTime(gr.Cells[27].Text) + "','" + Convert.ToDateTime(gr.Cells[28].Text) + "')";
                        objQueryController.ExecuteQuery(strQuery);
                    }
                   
                   
                }
                catch
                {
                    string strQuery = "Insert into SalesTemp ([Sno],[Inv#No],[Date],[DlrCode],[Dlr Name],[F10],[Model Code],[Qty],[Sale Amt],[Discount],[SPL#DIS],[Excise Duty],[Edu# Cess],[Hr#ECess],[LSPD],[MSPSD],[DHC],[Taxable],[CST],[LST],[Surch],[Enty/TOT],[Dely Chgs],[Freight],[Net Amount],[Cost],[S#off],[FromDate],[ToDate])";
                    strQuery += "Values ('" + ((TextBox)gr.FindControl("txtSno")).Text + "','" + ((TextBox)gr.FindControl("txtInvoiceNo")).Text + "','" + ((TextBox)gr.FindControl("txtDate")).Text + "','" + ((TextBox)gr.FindControl("txtDealerCode")).Text + "','" + ((TextBox)gr.FindControl("txtDealerName")).Text + "','" + ((TextBox)gr.FindControl("txtBlank")).Text + "','" + ((TextBox)gr.FindControl("txtModelCode")).Text + "','" + ((TextBox)gr.FindControl("txtQuantity")).Text + "','" + ((TextBox)gr.FindControl("txtSalesAmt")).Text + "','" + ((TextBox)gr.FindControl("txtDiscount")).Text + "','" + ((TextBox)gr.FindControl("txtSPLDIS")).Text + "','" + ((TextBox)gr.FindControl("txtExciseDuty")).Text + "','" + ((TextBox)gr.FindControl("txtEduCess")).Text + "','" + ((TextBox)gr.FindControl("txtHrECess")).Text + "','" + ((TextBox)gr.FindControl("txtLSPD")).Text + "','" + ((TextBox)gr.FindControl("txtMSPSD")).Text + "','" + ((TextBox)gr.FindControl("txtDHC")).Text + "','" + ((TextBox)gr.FindControl("txtTaxable")).Text + "','" + ((TextBox)gr.FindControl("txtCST")).Text + "','" + ((TextBox)gr.FindControl("txtLST")).Text + "','" + ((TextBox)gr.FindControl("txtSurch")).Text + "','" + ((TextBox)gr.FindControl("txtEntityTot")).Text + "','" + ((TextBox)gr.FindControl("txtDelyChgs")).Text + "','" + ((TextBox)gr.FindControl("txtFreight")).Text + "','" + ((TextBox)gr.FindControl("txtNetAmt")).Text + "','" + ((TextBox)gr.FindControl("txtCost")).Text + "','" + ((TextBox)gr.FindControl("txtSOff")).Text + "','" + Convert.ToDateTime(gr.Cells[27].Text) + "','" + Convert.ToDateTime(gr.Cells[28].Text) +"')";
                    objQueryController.ExecuteQuery(strQuery);
                }


                string strDeleteQuery = "Delete from SalesTemp where ID=" + Convert.ToInt16(str);
                objQueryController.ExecuteQuery(strDeleteQuery);

                string strGridQuery = "Select * from SalesTemp";
                DataTable dt = new DataTable();
                dt = objQueryController.ExecuteQuery(strGridQuery);

                grdSalesException.DataSource = dt;
                grdSalesException.DataBind();

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
