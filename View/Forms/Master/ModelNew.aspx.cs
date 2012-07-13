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

public partial class View_Forms_Master_ModelNew : System.Web.UI.Page
{
    QueryConroller objQueryController = new QueryConroller();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindModel();
            BindClutch();
            BindCategory();
            BindModelSpecial();
            if (Request.QueryString["Code"] != null)
            {
                FetchData();
            }
        }

    }

    public void FetchData()
    {
        btnSave.Text = "Update";
        if (Request.QueryString["source"] == null)
        {
            int MappingID = Convert.ToInt32(Request.QueryString["Code"].ToString());
            string strQuery = "select * from vw_ModelMappingMaster where ModelMappingID=" + MappingID;
            DataTable dt = objQueryController.ExecuteQuery(strQuery);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        txtMaterial.Text = dr["Material"].ToString();
                        drpModel.SelectedValue = dr["GroupID"].ToString();
                        drpCategory.SelectedValue = dr["ModelCategoryID"].ToString();
                        string strSpecial = dr["ModelSpecialID"].ToString();
                        if (strSpecial == "")
                        {
                            strSpecial = "0";
                        }
                        drpSpecial.SelectedValue = strSpecial;
                        drpClutch.SelectedValue = dr["ClutchTypeID"].ToString();
                        txtDescription.Text = dr["Description"].ToString();

                    }
                }
            }
        }
        else
        {
            string strSource = Request.QueryString["source"].ToString();
            string Material = Request.QueryString["Code"].ToString();
            if (strSource == "Exception")
            {
                txtMaterial.Text = Material;                                 
            }
        }
    }

    public void BindModel()
    {
        string strQuery = "select * from ModelGroupName order by ModelGroupName";
        DataTable dt = objQueryController.ExecuteQuery(strQuery);
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                drpModel.DataSource = dt;
                drpModel.DataTextField = "ModelGroupName";
                drpModel.DataValueField = "GroupID";
                drpModel.DataBind();
                drpModel.AppendDataBoundItems = true;
                ListItem list = new ListItem("Select", "0");
                drpModel.Items.Insert(0, list);
                drpModel.AppendDataBoundItems = false;
            }
        }
    }

    public void BindClutch()
    {
        string strQuery = "select * from ModelClutchType order by ClutchType";
        DataTable dt = objQueryController.ExecuteQuery(strQuery);
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                drpClutch.DataSource = dt;
                drpClutch.DataTextField = "ClutchType";
                drpClutch.DataValueField = "ClutchTypeID";
                drpClutch.DataBind();
                drpClutch.AppendDataBoundItems = true;
                ListItem list = new ListItem("Select", "0");
                drpClutch.Items.Insert(0, list);
                drpClutch.AppendDataBoundItems = false;
            }
        }
    }

    public void BindCategory()
    {
        string strQuery = "select * from ModelCategory order by ModelCategory";
        DataTable dt = objQueryController.ExecuteQuery(strQuery);
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                drpCategory.DataSource = dt;
                drpCategory.DataTextField = "ModelCategory";
                drpCategory.DataValueField = "ModelCategoryID";
                drpCategory.DataBind();
                drpCategory.AppendDataBoundItems = true;
                ListItem list = new ListItem("Select", "0");
                drpCategory.Items.Insert(0, list);
                drpCategory.AppendDataBoundItems = false;
            }
        }
    }

    public void BindModelSpecial()
    {
        string strQuery = "select * from ModelSpecialDetails order by ModelSpecial";
        DataTable dt = objQueryController.ExecuteQuery(strQuery);
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                drpSpecial.DataSource = dt;
                drpSpecial.DataTextField = "ModelSpecial";
                drpSpecial.DataValueField = "ModelSpecialID";
                drpSpecial.DataBind();
                drpSpecial.AppendDataBoundItems = true;
                ListItem list = new ListItem("NA", "0");
                drpSpecial.Items.Insert(0, list);
                drpSpecial.AppendDataBoundItems = false;
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["source"] == null)
        {
            Response.Redirect("ModelDefaultNew.aspx");
        }
        else
        {
            Response.Redirect("/WMS/view/Forms/Exceptions/ModelExceptionBulk.aspx");
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Update")
        {
            if (Request.QueryString["source"] == null)
            {
                UpdateInformation();
            }
            else
            {
                SaveInformation();
            }
        }
        else
        {
            SaveInformation();
        }
    }

    public void SaveInformation()
    {
        ModelUI objUI = new ModelUI();
        ModelController objController = new ModelController();
        int flag = CheckMaterial();
        int MappingID = 0;
        if (flag == 0)
        {
            objUI.Material = txtMaterial.Text.Trim();
            objUI.GroupID =Convert.ToInt32(drpModel.SelectedValue);
            objUI.ModelCategoryID = Convert.ToInt32(drpCategory.SelectedValue);
            if (chkSpecial.Checked)
            {
                objUI.ModelSpecialID = Convert.ToInt32(drpSpecial.SelectedValue);
            }
            else
            {
                objUI.ModelSpecialID = 0;
            }
            objUI.ClutchTypeID = Convert.ToInt32(drpClutch.SelectedValue);
            objUI.Description = txtDescription.Text.Trim();

            try
            {
                MappingID = objController.SaveModelDetail(objUI, null);
                if (MappingID > 0)
                {
                    ResolveExceptions();
                    if (Request.QueryString["source"] == null)
                    {
                        Response.Redirect("ModelDefaultNew.aspx");
                    }
                    else
                    {
                        Response.Redirect("/WMS/view/Forms/Exceptions/ModelExceptionBulk.aspx");
                    }
                   
                }
                else
                {
                    string str = "<script language = 'javascript'>";
                    str += "setMessageText('ctl00_ContentPlaceHolder1_lblMessage','Records could not ne saved successfully');";
                    str += "</script>";
                    literal1.Text = str;
                }
            }
            catch
            {
                string str = "<script language = 'javascript'>";
                str += "setMessageText('ctl00_ContentPlaceHolder1_lblMessage','Records could not ne saved successfully');";
                str += "</script>";
                literal1.Text = str;
            }
        }
        else
        {
            string str = "<script language = 'javascript'>";
            str += "setMessageText('ctl00_ContentPlaceHolder1_lblMessage','Material already exists');";
            str += "</script>";
            literal1.Text = str;
        }
    }

    public void UpdateInformation()
    {
        int MappingID = Convert.ToInt32(Request.QueryString["Code"].ToString());
        ModelUI objUI = new ModelUI();
        ModelController objController = new ModelController();
        int flag = CheckMaterial();
        
        if (flag == 0)
        {
            objUI.Material = txtMaterial.Text.Trim();
            objUI.GroupID = Convert.ToInt32(drpModel.SelectedValue);
            objUI.ModelCategoryID = Convert.ToInt32(drpCategory.SelectedValue);
            if (chkSpecial.Checked)
            {
                objUI.ModelSpecialID = Convert.ToInt32(drpSpecial.SelectedValue);
            }
            else
            {
                objUI.ModelSpecialID = 0;
            }
            objUI.ClutchTypeID = Convert.ToInt32(drpClutch.SelectedValue);
            objUI.Description = txtDescription.Text.Trim();
            objUI.MappingID = MappingID;

            try
            {
                MappingID = objController.UpdateModelDetail(objUI, null);
                if (MappingID > 0)
                {
                    Response.Redirect("ModelDefaultNew.aspx");
                   
                }
                else
                {
                    string str = "<script language = 'javascript'>";
                    str += "setMessageText('ctl00_ContentPlaceHolder1_lblMessage','Records could not ne updated successfully');";
                    str += "</script>";
                    literal1.Text = str;
                }
            }
            catch
            {
                string str = "<script language = 'javascript'>";
                str += "setMessageText('ctl00_ContentPlaceHolder1_lblMessage','Records could not ne updated successfully');";
                str += "</script>";
                literal1.Text = str;
            }
        }
        else
        {
            string str = "<script language = 'javascript'>";
            str += "setMessageText('ctl00_ContentPlaceHolder1_lblMessage','Material already exists');";
            str += "</script>";
            literal1.Text = str;
        }
    }

    public int CheckMaterial()
    {

        int Flag = 0;
        ModelUI objUI = new ModelUI();
        objUI.Material = txtMaterial.Text.Trim();

        ModelController objController = new ModelController();
        Flag = objController.CheckMaterial(objUI, null);


        return Flag;
    }

    public void ResolveExceptions()
    {
        string strProductionQuery = "select * from ProductionTemp where Material='" + txtMaterial.Text.Trim() +"'";
        DataTable dtProduction = objQueryController.ExecuteQuery(strProductionQuery);
        if (dtProduction != null)
        {
            if (dtProduction.Rows.Count > 0)
            {
                SaveProduction(dtProduction);
            }
        }

        string strSalesQuery = "select * from SalesTemp where [F10]='" + txtMaterial.Text.Trim() + "'";
        DataTable dtSales = objQueryController.ExecuteQuery(strSalesQuery);
        if (dtSales != null)
        {
            if (dtSales.Rows.Count > 0)
            {
                SaveSales(dtSales);
            }
        }

        string strAcrQuery = "select * from AcrBulkTemp where [Tractor No] in (select SerialNo from Production) and IsModelEx=1";
        DataTable dtAcr = objQueryController.ExecuteQuery(strAcrQuery);
        if (dtAcr != null)
        {
            if (dtAcr.Rows.Count > 0)
            {
                SaveBulkAcr();
            }
        }

    }

    public string SaveProduction(DataTable dt)
    {
        string lblMessage = "";
        ProductionController objCont = new ProductionController();

        DataTable dtView = new DataTable();
        string strViewQuery = "Select distinct * from vwMastersCode";
        dtView = objQueryController.ExecuteQuery(strViewQuery);

                string sConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ToString();

                int rcount = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    ProductionUI objUI = new ProductionUI();
                    string strCause = "model";
                    int modelex = 1;
                    int ProductionID = Convert.ToInt32(dr["ID"].ToString());

                    try
                    {
                        int flag = 0;

                        string strMaterial = Convert.ToString(dr["Material"]);
                        string strModelQuery = "Select * from ModelMapping where Material='" + strMaterial + "'";
                        DataTable dtModel = objQueryController.ExecuteQuery(strModelQuery);
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
                            objQueryController.ExecuteQuery(strQuery);
                        }
                        lblMessage = "File Production saved successfully;No of Rows Affected:" + rcount;
                    }
                    catch
                    {
                        string strQuery = "Insert into ProductionTemp ([S],[Material],[Serial no#],[Plnt],[SLoc],[Description of technical object],[Production_Month],[Model_Code],[Production_Month_Year],[IsApproved],MonthID,YearID,[Quarter],IsModelEx,FromDate,ToDate) ";
                        strQuery += "Values ('" + dr["S"].ToString() + "','" + dr["Material"].ToString() + "','" + dr["Serial no."].ToString() + "','" + dr["Plnt"].ToString() + "','" + dr["SLoc"].ToString() + "','" + dr["Description of technical object"].ToString() + "','" + dr["Production_Month"].ToString() + "','" + dr["Model_Code"].ToString() + "','" + dr["Production_Month_Year"].ToString() + "',0,'" + dr["MonthID"].ToString() + "','" + dr["YearID"].ToString() + "','" + dr["Quarter"].ToString() + "','" + modelex + "','" + dr["FromDate"].ToString() + "','" + dr["ToDate"].ToString() + "')";
                        objQueryController.ExecuteQuery(strQuery);
                    }
                    string strDeleteQuery = "Delete from ProductionTemp where [ID]=" + ProductionID;
                    objQueryController.ExecuteQuery(strDeleteQuery);

                }

            
       
        return lblMessage;
    }

    public string SaveSales(DataTable dt)
    {
        string lblMessage = "";

        SalesController objCont = new SalesController();
        string sConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ToString();





        int rcount = 0;

        foreach (DataRow dr in dt.Rows)
        {

            int IsModelEx = 1;
            int SalesID = Convert.ToInt32(dr["ID"].ToString());
            SalesUI objUI = new SalesUI();
            try
            {
                objUI.Sno = Convert.ToInt32(dr["Sno"]);
                objUI.InvoiceNo = Convert.ToInt32(dr["Inv#No"]);
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
                DataTable dtModel = objQueryController.ExecuteQuery(strModelQuery);

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
                objUI.Quantity = Convert.ToInt32(dr["Qty"]);
                objUI.SalesAmount = Convert.ToDouble(dr["Sale Amt"]);

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

                objUI.Edu_Cess = Convert.ToInt32(dr["Edu# Cess"]);
                objUI.HR_ECess = Convert.ToInt32(dr["Hr#ECess"]);

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
                    objUI.EntityTot = Convert.ToInt32(null);
                }
                else
                {
                    objUI.EntityTot = Convert.ToInt32(dr["Enty/TOT"]);
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
                objUI.Net_Amount = Convert.ToDouble(dr["Net Amount"]);
                objUI.Cost = Convert.ToInt32(dr["Cost"]);
                objUI.SOff = Convert.ToString(dr["S#Off"]);
                objUI.FromDate = Convert.ToDateTime(dr["FromDate"].ToString());
                objUI.ToDate = Convert.ToDateTime(dr["ToDate"].ToString());

                if (IsModelEx == 0)
                {
                    objCont.SaveSales(objUI);
                    rcount++;
                }
                else
                {
                    string strQuery = "Insert into SalesTemp ([Sno],[Inv#No],[Date],[DlrCode],[Dlr Name],[F10],[Model Code],[Qty],[Sale Amt],[Discount],[SPL#DIS],[Excise Duty],[Edu# Cess],[Hr#ECess],[LSPD],[MSPSD],[DHC],[Taxable],[CST],[LST],[Surch],[Enty/TOT],[Dely Chgs],[Freight],[Net Amount],[Cost],[S#off],[IsApproved],[FromDate],[ToDate],IsModelEx) ";
                    strQuery += "Values ('" + dr["Sno"].ToString() + "','" + dr["Inv#No"].ToString() + "','" + dr["Date"].ToString() + "','" + dr["DlrCode"].ToString() + "','" + dr["Dlr Name"].ToString() + "','" + dr["F10"].ToString() + "','" + dr["Model Code"].ToString() + "','" + dr["Qty"].ToString() + "','" + dr["Sale Amt"].ToString() + "','" + dr["Discount"].ToString() + "','" + dr["SPL#DIS"].ToString() + "','" + dr["Excise Duty"].ToString() + "','" + dr["Edu# Cess"].ToString() + "','" + dr["Hr#ECess"].ToString() + "','" + dr["LSPD"].ToString() + "','" + dr["MSPSD"].ToString() + "','" + dr["DHC"].ToString() + "','" + dr["Taxable"].ToString() + "','" + dr["CST"].ToString() + "','" + dr["LST"].ToString() + "','" + dr["Surch"].ToString() + "','" + dr["Enty/TOT"].ToString() + "','" + dr["Dely Chgs"].ToString() + "','" + dr["Freight"].ToString() + "','" + dr["Net Amount"].ToString() + "','" + dr["Cost"].ToString() + "','" + dr["S#off"] + "',0,'" + dr["FromDate"].ToString() + "','" + dr["ToDate"].ToString() + "'," + IsModelEx + ")";
                    objQueryController.ExecuteQuery(strQuery);
                }


                lblMessage = "File Sales saved successfully;No of Rows Affected:" + rcount;
            }
            catch
            {
                string strQuery = "Insert into SalesTemp ([Sno],[Inv#No],[Date],[DlrCode],[Dlr Name],[F10],[Model Code],[Qty],[Sale Amt],[Discount],[SPL#DIS],[Excise Duty],[Edu# Cess],[Hr#ECess],[LSPD],[MSPSD],[DHC],[Taxable],[CST],[LST],[Surch],[Enty/TOT],[Dely Chgs],[Freight],[Net Amount],[Cost],[S#off],[IsApproved],[FromDate],[ToDate],IsModelEx) ";
                strQuery += "Values ('" + dr["Sno"].ToString() + "','" + dr["Inv#No"].ToString() + "','" + dr["Date"].ToString() + "','" + dr["DlrCode"].ToString() + "','" + dr["Dlr Name"].ToString() + "','" + dr["F10"].ToString() + "','" + dr["Model Code"].ToString() + "','" + dr["Qty"].ToString() + "','" + dr["Sale Amt"].ToString() + "','" + dr["Discount"].ToString() + "','" + dr["SPL#DIS"].ToString() + "','" + dr["Excise Duty"].ToString() + "','" + dr["Edu# Cess"].ToString() + "','" + dr["Hr#ECess"].ToString() + "','" + dr["LSPD"].ToString() + "','" + dr["MSPSD"].ToString() + "','" + dr["DHC"].ToString() + "','" + dr["Taxable"].ToString() + "','" + dr["CST"].ToString() + "','" + dr["LST"].ToString() + "','" + dr["Surch"].ToString() + "','" + dr["Enty/TOT"].ToString() + "','" + dr["Dely Chgs"].ToString() + "','" + dr["Freight"].ToString() + "','" + dr["Net Amount"].ToString() + "','" + dr["Cost"].ToString() + "','" + dr["S#off"] + "',0,'" + dr["FromDate"].ToString() + "','" + dr["ToDate"].ToString() + "'," + IsModelEx + ")";
                objQueryController.ExecuteQuery(strQuery);
            }

            string strDeleteQuery = "Delete from SalesTemp where [ID]=" + SalesID;
            objQueryController.ExecuteQuery(strDeleteQuery);


        }

        return lblMessage;
    }

    public string SaveBulkAcr()
    {
        AcrController objAcrController = new AcrController();
        string sConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        string lblMessage = "";
        try
        {
           
            int Status = objAcrController.SaveBulkAcr();
            //string strQuery = "UPDATE AcrBulk SET ModelMappingID =( SELECT Production.ModelMappingID FROM Production WHERE Production.SerialNo = Convert(varchar,AcrBulk.Tractor_No)) WHERE EXISTS ( SELECT Production.ModelMappingID FROM Production WHERE Production.SerialNo = Convert(varchar,AcrBulk.Tractor_No))";
            string strQuery = "Update AcrBulk set ModelMappingID = Production.ModelMappingID  from AcrBulk inner join Production on Convert(varchar,AcrBulk.Tractor_No) = Production.SerialNo ";
            objQueryController.ExecuteQuery(strQuery);
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
