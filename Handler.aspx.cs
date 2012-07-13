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
public partial class AjaxHandler : System.Web.UI.Page
{
    MastersController objController = new MastersController();
    string cnnString = "";
    SqlConnection cnn;
    QueryConroller objQueryController = new QueryConroller();
    int LevelID;
    int RoleID;
    int ReportsToID;
    int UserID;
    string RegionParameter;
    protected void Page_Load(object sender, EventArgs e)
    {
        cnnString = ConfigurationManager.AppSettings["connectionString"];
        cnn = new SqlConnection(cnnString);
        string status = Request.Params["Status"];
        if (status.Trim().Equals("defect"))
        {
            if (Session["dtdefect"] != null)
            {
                DataTable dtdefect = (DataTable)Session["dtdefect"];
                try
                {
                    objController.UpdateDefectData();
                    string str = "";
                    str += "<Detail>";
                    str += "<Records>File Defect saved successfully</Records>";
                    str += "</Detail>";
                    SetData(str);

                }
                catch
                {
                    cnn.Close();
                }
            }
        }
        if (status.Trim().Equals("culprit"))
        {
            if (Session["dtculprit"] != null)
            {
                DataTable dtdefect = (DataTable)Session["dtculprit"];
                try
                {
                    objController.UpdateCulpritData();
                    string str = "";
                    str += "<Detail>";
                    str += "<Records>File Culprit saved successfully</Records>";
                    str += "</Detail>";
                    SetData(str);
                }
                catch
                {
                    cnn.Close();
                }
            }
        }
        if (status.Trim().Equals("customervoice"))
        {
            if (Session["dtcustvoice"] != null)
            {
                DataTable dtdefect = (DataTable)Session["dtcustvoice"];
                try
                {
                    objController.UpdateCustomerVoiceData();
                    string str = "";
                    str += "<Detail>";
                    str += "<Records>File Customer Voice saved successfully</Records>";
                    str += "</Detail>";
                    SetData(str);
                }
                catch
                {
                    cnn.Close();
                }
            }
        }
        if (status.Trim().Equals("item"))
        {
            if (Session["dtitem"] != null)
            {
                DataTable dtdefect = (DataTable)Session["dtitem"];
                try
                {
                    objController.UpdateItemData();
                    string str = "";
                    str += "<Detail>";
                    str += "<Records>File Item saved successfully</Records>";
                    str += "</Detail>";
                    SetData(str);
                }
                catch
                {
                    cnn.Close();
                }
            }
        }


        if (status.Trim().Equals("fillDealerNameswithchk"))
        {
            try
            {
                DataSet ds = new DataSet();
                string regionId = Request.Params["RegionID"];
                int UserID = 0;
                if (Request.Cookies["UserID"] != null)
                {
                    HttpCookie aCookie = Request.Cookies["UserID"];
                    UserID = Convert.ToInt32(Server.HtmlEncode(aCookie.Value));
                }
                string strUserDetailsQuery = " Select * from TempLoginUser where UserID=" + UserID;
                DataTable dtUserDetails = objQueryController.ExecuteQuery(strUserDetailsQuery);
                if (dtUserDetails != null)
                {
                    if (dtUserDetails.Rows.Count > 0)
                    {
                        RegionParameter = "";
                        LevelID = 0;
                        foreach (DataRow drUserDetails in dtUserDetails.Rows)
                        {
                            LevelID = Convert.ToInt32(drUserDetails["LevelID"]);
                            RoleID = Convert.ToInt32(drUserDetails["RoleID"]);
                            RegionParameter = drUserDetails["RegionParameter"].ToString();
                        }

                    }
                }

                string strQuery = "";
                if (regionId == "0")
                {
                    strQuery = "select [ID] as DealerID,Dealer+' ('+Code+')' as DealerCode from Dealer";
                    if (RoleID > 2)
                    {
                        if (RegionParameter != "")
                        {
                            strQuery += " where " + RegionParameter;
                        }
                    }
                    strQuery += " order by Dealer";
                }
                else
                {
                    strQuery = "select [ID] as DealerID,Dealer+' ('+Code+')' as DealerCode from Dealer where RegionID=" + regionId + " order by Dealer";
                }
                SqlCommand cmdDealer = new SqlCommand(strQuery, cnn);
                SqlDataAdapter objAdapter = new SqlDataAdapter();
                objAdapter.SelectCommand = cmdDealer;
                DataTable dt = new DataTable();
                objAdapter.Fill(dt);
                string str = "";
                str += "<Detail>";
                if (dt != null)
                {
                    if (dt.Rows.Count == 0)
                    {
                        str += "<Records>0</Records>";
                    
                    }
                    else
                    {


                      
                        str += "<Records>" + (dt.Rows.Count).ToString() + "</Records>";
                       
                        foreach (DataRow dr1 in dt.Rows)
                        {
                            str += "<DealerID>" + dr1["DealerID"].ToString() + "</DealerID>";
                            str += "<Dealer>" + SymbolValue(dr1["DealerCode"].ToString()) + "</Dealer>";
                        }
                    }
                }
                else
                {
                    str += "<Records>0</Records>";
                  
                }
                str += "</Detail>";
                SetData(str);
            }
            catch
            {
                cnn.Close();
            }
        }
        if (status.Trim().Equals("fillReportsTo"))
        {
            try
            {
                DataSet ds = new DataSet();
                string regionId = Request.Params["RegionID"];
                string strQuery = "";
                strQuery = "select UserID as ReportsToID,FullName as ReportsTo from vw_UserDetail where (LevelID=3 or  LevelID=6)  and RegionID=" + regionId;

                SqlCommand cmdDealer = new SqlCommand(strQuery, cnn);
                SqlDataAdapter objAdapter = new SqlDataAdapter();
                objAdapter.SelectCommand = cmdDealer;
                DataTable dt = new DataTable();
                objAdapter.Fill(dt);
                string str = "";
                str += "<Detail>";
                if (dt != null)
                {
                    if (dt.Rows.Count == 0)
                    {
                        str += "<Records>1</Records>";
                        str += "<ReportsToID>0</ReportsToID>";
                        str += "<ReportsTo>Select</ReportsTo>";

                    }
                    else
                    {


                        str += "<Records>" + Convert.ToString((dt.Rows.Count)+1) + "</Records>";
                        str += "<ReportsToID>0</ReportsToID>";
                        str += "<ReportsTo>Select</ReportsTo>";
                       
                        foreach (DataRow dr1 in dt.Rows)
                        {
                            str += "<ReportsToID>" + dr1["ReportsToID"].ToString() + "</ReportsToID>";
                            str += "<ReportsTo>" + SymbolValue(dr1["ReportsTo"].ToString()) + "</ReportsTo>";
                        }
                    }
                }
                else
                {
                    str += "<Records>1</Records>";
                    str += "<ReportsToID>0</ReportsToID>";
                    str += "<ReportsTo>Select</ReportsTo>";
                }
                str += "</Detail>";
                SetData(str);
            }
            catch
            {
                cnn.Close();
            }

          
        }


        if (status.Trim().Equals("fillRegionalRM"))
        {
            try
            {
                DataSet ds = new DataSet();
                string regionId = Request.Params["RegionID"];
                string strQuery = "";
                strQuery = "select UserID as ReportsToID,FullName as ReportsTo from vw_UserDetail where LevelID=3 and RegionID=" + regionId;

                SqlCommand cmdDealer = new SqlCommand(strQuery, cnn);
                SqlDataAdapter objAdapter = new SqlDataAdapter();
                objAdapter.SelectCommand = cmdDealer;
                DataTable dt = new DataTable();
                objAdapter.Fill(dt);
                string str = "";
                str += "<Detail>";
                if (dt != null)
                {
                    if (dt.Rows.Count == 0)
                    {
                        str += "<Records>1</Records>";
                        str += "<ReportsToID>0</ReportsToID>";
                        str += "<ReportsTo>Select</ReportsTo>";

                    }
                    else
                    {


                        str += "<Records>" + Convert.ToString((dt.Rows.Count) + 1) + "</Records>";
                        str += "<ReportsToID>0</ReportsToID>";
                        str += "<ReportsTo>Select</ReportsTo>";

                        foreach (DataRow dr1 in dt.Rows)
                        {
                            str += "<ReportsToID>" + dr1["ReportsToID"].ToString() + "</ReportsToID>";
                            str += "<ReportsTo>" + SymbolValue(dr1["ReportsTo"].ToString()) + "</ReportsTo>";
                        }
                    }
                }
                else
                {
                    str += "<Records>1</Records>";
                    str += "<ReportsToID>0</ReportsToID>";
                    str += "<ReportsTo>Select</ReportsTo>";
                }
                str += "</Detail>";
                SetData(str);
            }
            catch
            {
                cnn.Close();
            }


        }





        if (status.Trim().Equals("ModelDetails"))
        {
            try
            {
                DataSet ds = new DataSet();
                string ModelGroupID = Request.Params["ModelGroupID"];
                string strQuery = "";
                strQuery = "select * from ModelGroupName where GroupID=" + ModelGroupID;

                SqlCommand cmdDealer = new SqlCommand(strQuery, cnn);
                SqlDataAdapter objAdapter = new SqlDataAdapter();
                objAdapter.SelectCommand = cmdDealer;
                DataTable dt = new DataTable();
                objAdapter.Fill(dt);
                string str = "";
                str += "<Detail>";
                if (dt != null)
                {
                    if (dt.Rows.Count == 0)
                    {
                        str += "<Records>1</Records>";
                        str += "<WarrantyPeriod>0</WarrantyPeriod>";                        

                    }
                    else
                    {


                        str += "<Records>" + Convert.ToString((dt.Rows.Count)+1) + "</Records>";
                       
                      
                       
                        foreach (DataRow dr1 in dt.Rows)
                        {
                            str += "<WarrantyPeriod>" + dr1["WarrantyPeriod"].ToString() + "</WarrantyPeriod>";
                          
                        }
                    }
                }
                else
                {
                    str += "<Records>1</Records>";
                    str += "<WarrantyPeriod>0</WarrantyPeriod>";                    
                }
                str += "</Detail>";
                SetData(str);
            }
            catch
            {
                cnn.Close();
            }

          
        }
    }



    public string SymbolValue(string strTemp)
    {
        string strValue = "";
        for (int i = 0; i < 3; i++)
        {
            if (i == 0)
            {
                strValue = ConvertSymbol(strTemp, "'&'", "&amp;", i);
            }
            if (i == 1)
            {
                strValue = ConvertSymbol(strValue, "'<'", "&lt;", i);
            }
            if (i == 2)
            {
                strValue = ConvertSymbol(strValue, "'>'", "&gt;", i);
            }
        }
        return strValue;
    }

    /***************************************************************************************************************/
    public string ConvertSymbol(string str, string symbol, string CSymbol, int Temp)
    {
        //string strSymbol = ;
        string[] arr = str.Split(' '); ;
        if (Temp == 0)
            arr = str.Split('&');
        if (Temp == 1)
            arr = str.Split('<');
        if (Temp == 2)
            arr = str.Split('>');

        string strValue = "";
        int Status = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            Status = 1;
            strValue = strValue + arr[i];
            if (i < arr.Length - 1)
            {
                strValue = strValue + CSymbol;
            }
        }
        if (Status == 1)
        {
            str = strValue;
        }
        return str;
    }

    public void SetData(string str)
    {
        //cnn.Close();
        Response.Clear();
        Response.ContentType = "text/xml";
        Response.Write("<data>" + str + "</data>");
        Response.End();
    }

   



}
