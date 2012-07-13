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

public partial class View_Forms_Reports_ACRDynamicReport : System.Web.UI.Page
{
    Utility objUtility = new Utility();
    QueryConroller objQuerycontroller = new QueryConroller();
    int LevelID;
    int UserID;
    int RoleID;
    string UserParameter;
    string RegionParameter;
    static string str = "";
    string strItemCodeID;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        ProductionController Objproduction = new ProductionController();
        getAuthenticationDetails();
        if (!IsPostBack)
        {
            BindRegion();
            Session["ModelCodeParameter"] = null;
            Session["DealerCodeParameter"] = null;
            Session["DefectCodeParameter"] = null;
            Session["CVOICECodeParameter"] = null;
            Session["ItemGroupIDCodeParameter"] = null;
            Session["CUL_CODEParameter"] = null;
            Session["HMRID"] = null;
            checkException();
            getModel();
            BindModelCategory();
            BindModelClutch();
            BindModelSpecial();
            getDealer();
            getCulpritCodeDetail();
            getDefectCodeDetail();
            getItemCodeGroup();
            getCustomerVoiceCodeDetail();
            BindProductionMonth("From");
            BindProductionMonth("To");

        }

        else
        {
            string strjscript = "<script language='javascript' type='text/javascript'>";
            strjscript += "";
            strjscript += "</script" + ">";
            literal1.Text = strjscript;
            
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

    public void BindProductionMonth(string source)
    {
        DataTable dtProdMonth = new DataTable();
        string strProdMonthQuery = "select * from ProductionMonth";
        dtProdMonth = objQuerycontroller.ExecuteQuery(strProdMonthQuery);

        int BaseProductionMonth = Convert.ToInt16(dtProdMonth.Rows[0]["BaseProductionMonth_Code"]);
        int BaseMonthID = Convert.ToInt16(dtProdMonth.Rows[0]["Month_ID"]);
        int BaseYearID = Convert.ToInt16(dtProdMonth.Rows[0]["Year_ID"]);
        string strBaseDate = Convert.ToString(BaseMonthID) + "/1/" + BaseYearID;
        DateTime BaseDate = Convert.ToDateTime(strBaseDate);
        int ProductionMonth = BaseProductionMonth;
        int ProductionMonthValue;
        for (int i = BaseProductionMonth; i < BaseProductionMonth + 200; i++)
        {
            int Offset = Convert.ToInt16(ProductionMonth) - BaseProductionMonth;
            DateTime ProdMonthYear = BaseDate.AddMonths(Offset);

            int CurrentYearID = ProdMonthYear.Year;
            int CurrentMonthID = ProdMonthYear.Month;
            int PresentYearID = System.DateTime.Now.Year;
            int PresentMonthID = System.DateTime.Now.Month;

            string strCurrentYearID = (Convert.ToString(CurrentYearID)).Substring(2, 2);
            string strPresentYearID = (Convert.ToString(PresentYearID)).Substring(2, 2);

            string strMonth = getMonth(CurrentMonthID);
            string strPresentMonth = getMonth(PresentMonthID);
            ProductionMonthValue = ProductionMonth;
            //add this condition for change the production month value 
            //ProductionMonth == 97 then it should be 1
            //ProductionMonth == 98 then it should be 2
            //ProductionMonth == 99 then it should be 3 and with the 100 it will continue 
            if (i == 97 || ProductionMonth == 97)
            {
                ProductionMonthValue = 1;
            }
            if (i == 98 || ProductionMonth == 98)
            {
                ProductionMonthValue = 2;
            }
            if (i == 99 || ProductionMonth == 99)
            {
                ProductionMonthValue = 3;
            }


            string strProductionMonthYear = strMonth + "-" + strCurrentYearID;
            string strPresentMonthYear = strPresentMonth + "-" + strPresentYearID;


            ListItem list = new ListItem(strProductionMonthYear, ProductionMonthValue.ToString());
            if (source == "From")
            {
                drpFromMonth.AppendDataBoundItems = true;
                drpFromMonth.Items.Add(list);
                drpFromMonth.AppendDataBoundItems = false;
                if (strProductionMonthYear == strPresentMonthYear)
                {
                    drpFromMonth.SelectedValue = list.Value;
                }
            }
            else
            {
                drpToMonth.AppendDataBoundItems = true;
                drpToMonth.Items.Add(list);
                drpToMonth.AppendDataBoundItems = false;
                if (strProductionMonthYear == strPresentMonthYear)
                {
                    drpToMonth.SelectedValue = list.Value;
                }
            }
            ProductionMonth++;
        }
    }

    public void BindModelCategory()
    {
        string strQuery = "";
        DataTable dtinformation = new DataTable();
        strQuery = "select * from ModelCategory";
        dtinformation = objQuerycontroller.ExecuteQuery(strQuery);
        if (dtinformation != null)
        {

            if (dtinformation.Rows.Count > 0)
            {
                chkCategory.DataSource = dtinformation;
                chkCategory.DataValueField = "ModelCategoryID";
                chkCategory.DataTextField = "ModelCategory";
                chkCategory.DataBind();
            }
        }
    }

    public void BindModelClutch()
    {
        string strQuery = "";
        DataTable dtinformation = new DataTable();
        strQuery = "select * from ModelClutchType";
        dtinformation = objQuerycontroller.ExecuteQuery(strQuery);
        if (dtinformation != null)
        {

            if (dtinformation.Rows.Count > 0)
            {
                chkClutchType.DataSource = dtinformation;
                chkClutchType.DataValueField = "ClutchTypeID";
                chkClutchType.DataTextField = "Description";
                chkClutchType.DataBind();
            }
        }
    }

    public void BindModelSpecial()
    {
        string strQuery = "";
        DataTable dtinformation = new DataTable();
        strQuery = "select * from ModelSpecialDetails";
        dtinformation = objQuerycontroller.ExecuteQuery(strQuery);
        if (dtinformation != null)
        {

            if (dtinformation.Rows.Count > 0)
            {
                chkSpecialList.DataSource = dtinformation;
                chkSpecialList.DataValueField = "ModelSpecialID";
                chkSpecialList.DataTextField = "ModelSpecial";
                chkSpecialList.DataBind();
                chkSpecialList.AppendDataBoundItems = true;
                ListItem list = new ListItem("NA", "0");
                chkSpecialList.Items.Insert(0, list);
                chkSpecialList.AppendDataBoundItems = false;
               
            }
        }
    }

    public void getAuthenticationDetails()
    {
        UserParameter = "";
        if (Request.Cookies["UserID"] != null)
        {
            HttpCookie aCookie = Request.Cookies["UserID"];
            UserID = Convert.ToInt32(Server.HtmlEncode(aCookie.Value));
        }

        string strUserDetailsQuery = " Select * from TempLoginUser where UserID=" + UserID;
        DataTable dtUserDetails = objQuerycontroller.ExecuteQuery(strUserDetailsQuery);
        if (dtUserDetails != null)
        {
            if (dtUserDetails.Rows.Count > 0)
            {
                foreach (DataRow drUserDetails in dtUserDetails.Rows)
                {
                    LevelID = Convert.ToInt32(drUserDetails["LevelID"]);
                    RoleID = Convert.ToInt32(drUserDetails["RoleID"]);
                    RegionParameter = drUserDetails["RegionParameter"].ToString();

                }
            }
        }
        string strQuery = "";
        if (LevelID == 3)
        {
            strQuery = " Select distinct UserID from vw_UserDetail where ReportsToID=" + UserID;
            DataTable dtUserParameter = objQuerycontroller.ExecuteQuery(strQuery);
            if (dtUserParameter != null)
            {
                if (dtUserParameter.Rows.Count > 0)
                {
                    int flag = 0;
                    foreach (DataRow drUserParameter in dtUserParameter.Rows)
                    {
                        if (flag != 0)
                        {
                            UserParameter += " or ";
                        }
                        UserParameter += " UserID=" + drUserParameter["UserID"].ToString();
                        flag++;
                    }
                }
            }
        }
        if (LevelID > 3)
        {
            UserParameter = " UserID=" + UserID;
        }
    }
    public void BindRegion()
    {
        string strQuery = "Select * from Region ";


        //if (LevelID > 2)
        //{
        //    strQuery += " where " + RegionParameter;
        //}


        strQuery += " order by Region";


        DataTable dtRegion = new DataTable();
        dtRegion = objQuerycontroller.ExecuteQuery(strQuery);
        if (dtRegion != null)
        {
            if (dtRegion.Rows.Count > 0)
            {
                drpRegion.DataSource = dtRegion;
                drpRegion.DataTextField = "Region";
                drpRegion.DataValueField = "RegionID";
                drpRegion.DataBind();
                drpRegion.AppendDataBoundItems = true;
                ListItem list = new ListItem("All", "0");
                drpRegion.Items.Insert(0, list);
                drpRegion.AppendDataBoundItems = false;
                drpRegion.SelectedValue = "0";

            }
        }
    }
    public void checkException()
    {
        int countRow = objQuerycontroller.getNoOfException("execute usp_countException");
        if (countRow > 0)
        {
            string strjscript = "<script language='javascript' type='text/javascript'>";
            strjscript += " checkException();";
            strjscript += "</script" + ">";
            literal1.Text = strjscript;

        }
    }
    public void getModel()
    {

        string strQuery = "";
        DataTable dtModel = new DataTable();
        strQuery = "select * from ModelGroupName  order by ModelGroupName";
        dtModel = objQuerycontroller.ExecuteQuery(strQuery);
        if (dtModel != null)
        {
            if (dtModel.Rows.Count > 0)
            {
                chkModelCodeList.DataSource = dtModel;
                chkModelCodeList.DataValueField = "ModelGroupName";
                chkModelCodeList.DataTextField = "ModelGroupName";
                chkModelCodeList.DataBind();
            }

        }

    }
    public void getDealer()
    {

        string strQuery = "";
        DataTable dtinformation = new DataTable();
        string strRegionID = drpRegion.SelectedValue;
        if (strRegionID != "0")
        {
            strRegionID = " where RegionID=" + drpRegion.SelectedValue;
        }
        else
        {
            strRegionID = "";
        }
        strQuery = "select distinct Code,Dealer+'('+Code+')' as Dealer from Dealer " + strRegionID + "  order by Dealer";
        dtinformation = objQuerycontroller.ExecuteQuery(strQuery);
        if (dtinformation != null)
        {

            if (dtinformation.Rows.Count > 0)
            {
                chkDealerCode.DataSource = dtinformation;
                chkDealerCode.DataValueField = "Code";
                chkDealerCode.DataTextField = "Dealer";
                chkDealerCode.DataBind();

            }
        }
    }
    public void getDefectCodeDetail()
    {

        string strQuery = "";
        DataTable dtinformation = new DataTable();
        strQuery = "select distinct  Code,Description from Defect  order by Description";
        dtinformation = objQuerycontroller.ExecuteQuery(strQuery);
        if (dtinformation != null)
        {

            if (dtinformation.Rows.Count > 0)
            {
                chkDefectCode.DataSource = dtinformation;
                chkDefectCode.DataValueField = "Code";
                chkDefectCode.DataTextField = "Description";
                chkDefectCode.DataBind();
            }

        }

    }
    public void getItemCodeGroup()
    {

        string strQuery = "";
        DataTable dtinformation = new DataTable();
        if (txtItem.Text.Trim() == "")
        {
            strQuery = "select * from ItemGRoup  order by ItemGRoupName";
        }
        else
        {
            strQuery = "select * from ItemGRoup where ItemGroupName like '"+ txtItem.Text.Trim() +"%'  order by ItemGRoupName";
        }
        dtinformation = objQuerycontroller.ExecuteQuery(strQuery);
        if (dtinformation != null)
        {

            if (dtinformation.Rows.Count > 0)
            {
                chkItemGroup.DataSource = dtinformation;
                chkItemGroup.DataValueField = "ItemCodeGroupID";
                chkItemGroup.DataTextField = "ItemGRoupName";
                chkItemGroup.DataBind();
            }
        }
    }

    public void BindItemcode()
    {
        string strItemQuery = "";
        DataTable dtItem = new DataTable();
        strItemQuery = "select Item.Code,Item.Description,Item.Code+'('+Item.Description+')' as CodeDescription from  item inner join ItemGRoupMapping on Item.Code=ItemGRoupMapping.ItemCode inner join ItemGroup on ItemGroup.ItemCodeGroupID=ItemGRoupMapping.ItemGroupID where " + strItemCodeID; 
        dtItem = objQuerycontroller.ExecuteQuery(strItemQuery);
        int cnt = dtItem.Rows.Count;
        if (dtItem != null)
        {

            if (dtItem.Rows.Count > 0)
            {
                chkItemCode.DataSource = dtItem;
                chkItemCode.DataValueField = "Code";
                chkItemCode.DataTextField = "CodeDescription";
                chkItemCode.DataBind();
            }
        }
        //select * from Item
    }

    public void getCustomerVoiceCodeDetail()
    {

        string strQuery = "";
        DataTable dtinformation = new DataTable();
        strQuery = "select distinct Code,Description from CustomerVoice order by Description";
        dtinformation = objQuerycontroller.ExecuteQuery(strQuery);
        if (dtinformation != null)
        {

            if (dtinformation.Rows.Count > 0)
            {
                chkCVoiceCode.DataSource = dtinformation;
                chkCVoiceCode.DataValueField = "Code";
                chkCVoiceCode.DataTextField = "Description";
                chkCVoiceCode.DataBind();
            }

        }

    }
    public void getCulpritCodeDetail()
    {

        string strQuery = "";
        DataTable dtinformation = new DataTable();
        strQuery = "select distinct Code,Description from Culprit  order by Description";
        dtinformation = objQuerycontroller.ExecuteQuery(strQuery);
        if (dtinformation != null)
        {

            if (dtinformation.Rows.Count > 0)
            {
                chkCulpritCode.DataSource = dtinformation;
                chkCulpritCode.DataValueField = "Code";
                chkCulpritCode.DataTextField = "Description";
                chkCulpritCode.DataBind();
            }

        }

    }
    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        getSessionParameterList();

    }
    public string ConvertDate(string strDate)
    {
        string strDateTemp = "";
        string[] strDateArray = strDate.Split('/');
        if (strDateArray.Length == 3)
        {
            if (strDateArray[1].Length == 1)
            {
                strDateArray[1] = "0" + strDateArray[1];
            }
            if (strDateArray[0].Length == 1)
            {
                strDateArray[0] = "0" + strDateArray[0];
            }
            strDateTemp = strDateArray[1] + "/" + strDateArray[0] + "/" + strDateArray[2];
        }
        else
        {
            strDateTemp = "01/01/1900";
        }
        return strDateTemp;
    } 
    public void getSessionParameterList()
    {
        string FromDate = "";
        string ToDate = "";
        if (txtFromDate.Text != "" && txtToDate.Text != "")
        {
            FromDate = ConvertDate(txtFromDate.Text.Trim());
            ToDate = ConvertDate(txtToDate.Text.Trim());
        }
        Session.Add("ModelCodeParameter", getParameter(chkModelCodeList, "ModelGroupName"));
        string chkIDList = hdnchkIDList.Value;
        //Session.Add("DealerCodeParameter", hdnchkIDList.Value);
        Session.Add("ModelCategoryParameter", getParameter(chkCategory, "ModelCategoryID"));
        Session.Add("ModelClutchParameter", getParameter(chkClutchType, "ClutchTypeID"));

        Session.Add("ModelSpecialParameter", getParameter(chkSpecialList, "ModelSpecialID"));

        Session.Add("FromDate", FromDate);
        Session.Add("ToDate", ToDate);
        Session.Add("DealerCodeParameter", getParameter(chkDealerCode, "DLR_CO"));
        Session.Add("DefectCodeParameter", getParameter(chkDefectCode, "DEF"));
        Session.Add("CVOICECodeParameter", getParameter(chkCVoiceCode, "CVOICE"));
        Session.Add("ItemGroupIDCodeParameter", getIntegerParameter(chkItemGroup, "ItemGroupID"));
        Session.Add("ITEM_CODE", getIntegerParameter1(chkItemCode, "ITEM_CODE"));
        string strITEM_CODE = Session["ITEM_CODE"].ToString();
        string strItemGroupIDCodeParameter = Session["ItemGroupIDCodeParameter"].ToString();
        Session.Add("CUL_CODEParameter", getParameter(chkCulpritCode, "CUL_CODE"));
        string DataType = rdoData.SelectedValue;
        if (DataType == "0")
        {
            Session.Add("DataType", "Place");
            if (rdoAlwar.Checked)
            {
                Session.Add("place", "A");
            }
            if (rdoBhopal.Checked)
            {
                Session.Add("place", "B");
            }
        }
        else if (DataType == "1")
        {
            Session.Add("DataType", "Engine");
            if (rdoAlwarEngine.Checked)
            {
                Session.Add("Engine", "A");
            }
            if (rdoSimpsonEngine.Checked)
            {
                Session.Add("Engine", "S");
            }

        }
        else
        {
            Session.Add("DataType", "Tractor");
        }
        int HMRID=0;
        if (rdoLessThan250.Checked)
        {
            HMRID = 1;
        }
        if (rdoMoreThan250.Checked)
        {
            HMRID = 2;
        }
        if (rdoAll.Checked)
        {
            HMRID = 3;
        }
        string strProblemType = "";
        if (rdoPrimary.Checked)
        {
            strProblemType = "P";
        }
        if (rdoConsequences.Checked)
        {
            strProblemType = "C";
        }
        if (rdoAllProblem.Checked)
        {
            strProblemType = "All";
        }
        Session.Add("HMRID", HMRID);
        Session.Add("Problem", strProblemType);
        string strFromMonth = drpFromMonth.SelectedValue;
        string strToMonth = drpToMonth.SelectedValue;
        Session.Add("HMRID", HMRID);
        Session.Add("FromMonth", strFromMonth);
        Session.Add("ToMonth", strToMonth);
        Response.Redirect("/WMS/View/Forms/Reports/ACRGenerateReport.aspx");

    }
    public string getParameter(CheckBoxList chkList,string ColoumnName)
    {
        string strParameter = "";
        if (chkList != null)
        {
            if (chkList.Items.Count > 0)
            {
                if (ColoumnName == "ModelSpecialID")
                {
                    foreach (ListItem list in chkList.Items)
                    {
                        if (list.Selected)
                        {
                            if (strParameter == "")
                            {
                                if (list.Value == "0")
                                {
                                    strParameter = " (" + ColoumnName + " is null)";
                                }
                                else
                                {
                                    strParameter = " (" + ColoumnName + "='" + list.Value.Trim() + "')";
                                }
                            }
                            else
                            {
                                if (list.Value == "0")
                                {
                                    strParameter = strParameter + "  or  (" + ColoumnName + " is null)";
                                }
                                else
                                {
                                    strParameter = strParameter + "  or  (" + ColoumnName + "='" + list.Value.Trim() + "')";
                                }
                            }
                        }
                    }
                }
                else
                {
                    foreach (ListItem list in chkList.Items)
                    {
                        if (list.Selected)
                        {
                            if (strParameter == "")
                            {
                                strParameter = " (" + ColoumnName + "='" + list.Value.Trim() + "')";
                            }
                            else
                            {
                                strParameter = strParameter + "  or  (" + ColoumnName + "='" + list.Value.Trim() + "')";
                            }
                        }
                    }
                }
            }
        }
        return strParameter;
    }
    public string getIntegerParameter(CheckBoxList chkList, string ColoumnName)
    {
        string strParameter = "";
        if (chkList != null)
        {
            if (chkList.Items.Count > 0)
            {
                foreach (ListItem list in chkList.Items)
                {
                    if (list.Selected)
                    {
                        if (strParameter == "")
                        {
                            strParameter = " (" + ColoumnName + "=" + list.Value + ")";
                        }
                        else
                        {
                            strParameter = strParameter + "  or  (" + ColoumnName + "=" + list.Value + ")";
                        }
                        
                    }
                }
            }
        }
        return strParameter;
    }
    protected void drpRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        getDealer();
    }
    
    protected void btnGo_Click(object sender, EventArgs e)
    {
        getItemCodeGroup();
    }
    protected void btnGo_Click1(object sender, EventArgs e)
    {
        getItemCodeGroup();
    }
    
    protected void btnitem_Click(object sender, EventArgs e)
    {
        strItemCodeID = getIntegerParameter(chkItemGroup, "ItemCodeGroupID");
    
        BindItemcode();

    }
    public string getIntegerParameter1(CheckBoxList chkList, string ColoumnName)
    {
        string strParameter = "";
        if (chkList != null)
        {
            if (chkList.Items.Count > 0)
            {
                foreach (ListItem list in chkList.Items)
                {
                    if (list.Selected)
                    {
                        if (strParameter == "")
                        {
                            strParameter = " (" + ColoumnName + "='" + list.Value + "')";
                        }
                        else
                        {
                            strParameter = strParameter + "  or  (" + ColoumnName + "='" + list.Value + "')";
                        }

                    }
                }
            }
        }
        return strParameter;
    }
}
