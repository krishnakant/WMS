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

public partial class View_Forms_Master_DealerDefault : System.Web.UI.Page
{
    QueryConroller objQuerycontroller = new QueryConroller();
    public string strProjectName = "";
    static string strValue = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        strProjectName = System.Configuration.ConfigurationManager.AppSettings["WMSProjectName"];
        if (!IsPostBack)
        {
            BindRegion();
            if(Session["UpadateRegionId"]!= null)
            {
                drpRegion.SelectedValue = Session["UpadateRegionId"].ToString();
                if (Session["UpadatesearchCode"].ToString() != "-1")
                {
                    txtsearch.Text = Session["UpadatesearchCode"].ToString();
                     Session["UpadatesearchCode"] = null;
                }
                Session["UpadateRegionId"] = null;
                
            }
            bindData();
        }
    }
    public void BindRegion()
    {
        DataTable dtRegion = new DataTable();
        string strQuery = "Select RegionID,Region from Region order by Region";
        dtRegion = objQuerycontroller.ExecuteQuery(strQuery);
        if (dtRegion != null)
        {
            if (dtRegion.Rows.Count > 0)
            {
                drpRegion.DataSource = dtRegion;
                drpRegion.DataTextField = "Region";
                drpRegion.DataValueField = "RegionID";
                drpRegion.DataBind();
                drpRegion.Visible = true;

                drpRegion.AppendDataBoundItems = true;
                ListItem list1 = new ListItem("All", "0");
                drpRegion.Items.Insert(0, list1);
                drpRegion.AppendDataBoundItems = false;
            }
        }
    }
    protected void gridView_RowCreated(object sender, GridViewRowEventArgs e)
    {
        
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate))
        {
            e.Row.TabIndex = -1;
            e.Row.Attributes["onclick"] = string.Format("javascript:SelectRow(this, {0});", e.Row.RowIndex);
            e.Row.Attributes["onkeydown"] = "javascript:return SelectSibling(event);";
            e.Row.Attributes["onselectstart"] = "javascript:return false;";

        }
    }

    public void bindData()
    {
        string strQuery = "";
        DataTable dtinformation = new DataTable();
        if (drpRegion.SelectedValue == "0")
        {
            strQuery = "select ID,Dealer,IsActive,Code,Region,City,IsOperatingDealer,InstallerName from vw_Dealer ";
        }
        else
        {
            strQuery = "select ID,Dealer,IsActive,Code,Region,City,IsOperatingDealer,InstallerName from vw_Dealer where RegionID=" + drpRegion.SelectedValue + " ";
        }
        if (txtsearch.Text!="")
        {
            if (drpRegion.SelectedValue == "0")
            {
                strQuery += " where Code like '" + txtsearch.Text + "%'";
            }
            else
            {
                strQuery += " and Code like '" + txtsearch.Text + "%'";
            }
        }
        strQuery += " order by Dealer";
       dtinformation = objQuerycontroller.ExecuteQuery(strQuery);
        if (dtinformation != null)
        {
            if (dtinformation.Rows.Count > 0)
            {
                GridView1.DataSource = dtinformation;
                GridView1.DataBind();
                btnExport.Visible = true;
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                btnExport.Visible = false;
            }
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            btnExport.Visible = false;
        }

        strValue = "";
        strValue = strValue + "<table width='50%' border='1' cellpadding='0' cellspacing='0'>";
        strValue = strValue + "<tr><td style='font-size:small;font-weight:bold;'>Report For:</td><td style='font-size:small;font-weight:bold;'>Dealer Deatails</td></tr>";
        strValue = strValue + "<tr><td style='background-color:Teal; font-size:small;font-weight:bold;' >Dealer Name</td>";
        strValue = strValue + "<td style='background-color:Teal; font-size:small;font-weight:bold;'>Code</td>";
        strValue = strValue + "<td style='background-color:Teal; font-size:small;font-weight:bold;'>Region</td>";
        strValue = strValue + "<td style='background-color:Teal; font-size:small;font-weight:bold;'>Location</td>";
        strValue = strValue + "<td style='background-color:Teal; font-size:small;font-weight:bold;'>InstallerName</td>";
        strValue = strValue + "<td style='background-color:Teal; font-size:small;font-weight:bold;'>Is Active</td></tr>";


        int flag = 0;

        foreach (GridViewRow gr in GridView1.Rows)
        {
            if (dtinformation.Rows.Count > 0)
            {

                strValue = strValue + "<td >" + dtinformation.Rows[flag]["Dealer"].ToString() + "</td>";
                strValue = strValue + "<td >" + dtinformation.Rows[flag]["Code"].ToString() + "</td>";
                strValue = strValue + "<td >" + dtinformation.Rows[flag]["Region"].ToString() + "</td>";
                strValue = strValue + "<td >" + dtinformation.Rows[flag]["City"].ToString() + "</td>";
                strValue = strValue + "<td >" + dtinformation.Rows[flag]["InstallerName"].ToString() + "</td>";
                if (dtinformation.Rows[flag]["IsActive"].ToString() == "True")
                {
                    strValue = strValue + "<td >Active</td>";
                }
                else
                {
                    strValue = strValue + "<td >InActive</td>";
                }

            }
            strValue = strValue + "</tr>";
            flag++;
        }
        strValue = strValue + "</table><br/><br/>";
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
       GridViewExport objExport = new GridViewExport();
        hdnExport.Value = strValue + hdnExport.Value;
        objExport.ExportGridView(hdnExport.Value);
    }
    protected void Role_RowDeleted(Object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string strQuery = "";
            DataTable dtinformation = new DataTable();
            strQuery = "delete  from Dealer where ID=" + hdnDelrID.Value + "";
            dtinformation = objQuerycontroller.ExecuteQuery(strQuery);
            bindData();
            string strjscript = "<script language='javascript'>";
            strjscript += " setLabelText('ctl00_ContentPlaceHolder1_lblMessage','Record Deleted Successfully..' );";
            strjscript += "</script" + ">";
            Literal1.Text = strjscript;
        }
        catch (Exception ex)
       {

           string strjscript = "<script language='javascript'>";
           strjscript += " setLabelText('ctl00_ContentPlaceHolder1_lblMessage','Record is in use can not deleted..' );";
           strjscript += "</script" + ">";
           Literal1.Text = strjscript;
        }
    }

    public void eventhandlerSerialNo(object Sender, EventArgs E)
    {
        int i = 1;
        int pi = GridView1.PageIndex;
        int ps = GridView1.PageSize;
        //<><> Use Name of Your GridView Instead Of gvDetailProspect <><>// 
        if (pi > 0)
        {
            i = pi * ps + 1;
        }
        foreach (GridViewRow gr in GridView1.Rows)
        {
            gr.Cells[0].Text = i.ToString();

            i++;
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect(strProjectName + "/View/Forms/Master/Dealer.aspx");
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        bindData();
    }



 
}