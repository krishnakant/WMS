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
using InfoSoftGlobal;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Office.Core;
using System.IO;

public partial class View_Forms_Reports_TopContributors : System.Web.UI.Page
{
    QueryConroller objQueryController = new QueryConroller();
    private GridViewHelper helper;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string Date = (System.DateTime.Now.ToShortDateString());
            string[] strDateArray = Date.Split('/');
            string CrDate = strDateArray[2].ToString();
            drpFromYear.SelectedValue = CrDate;
            drpToYear.SelectedValue = CrDate;
            BindRegion();
            BindModel();
            BindModelCategory();
            BindModelClutch();
            BindModelSpecial();
            
        }
    }

    public void BindRegion()
    {
        string strQuery = "";
        DataTable dtinformation = new DataTable();
        strQuery = "select * from Region order by RegionID";
        dtinformation = objQueryController.ExecuteQuery(strQuery);
        if (dtinformation != null)
        {

            if (dtinformation.Rows.Count > 0)
            {
                chkRegion.DataSource = dtinformation;
                chkRegion.DataValueField = "RegionID";
                chkRegion.DataTextField = "Region";
                chkRegion.DataBind();
            }
        }
    }

    public void BindModel()
    {
        string strQuery = "";
        DataTable dtModel = new DataTable();
        strQuery = "select * from ModelGroupName  order by ModelGroupName";
        dtModel = objQueryController.ExecuteQuery(strQuery);
        if (dtModel != null)
        {
            if (dtModel.Rows.Count > 0)
            {
                drpModel.DataSource = dtModel;
                drpModel.DataValueField = "GroupID";
                drpModel.DataTextField = "ModelGroupName";
                drpModel.DataBind();
                drpModel.AppendDataBoundItems = true;
                ListItem list = new ListItem("All", "0");
                drpModel.Items.Insert(0, list);
                drpModel.AppendDataBoundItems = true;

            }

        }

    }
    

    

    public void BindModelCategory()
    {
        string strQuery = "";
        DataTable dtinformation = new DataTable();
        strQuery = "select * from ModelCategory";
        dtinformation = objQueryController.ExecuteQuery(strQuery);
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
        dtinformation = objQueryController.ExecuteQuery(strQuery);
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
        dtinformation = objQueryController.ExecuteQuery(strQuery);
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

    public void eventhandlerSerialNo(object Sender, EventArgs E)
    {
        int i = 1;
        int pi = grdWarranty.PageIndex;
        int ps = grdWarranty.PageSize;
        //<><> Use Name of Your GridView Instead Of grdWarranty <><>// 
        if (pi > 0)
        {
            i = pi * ps + 1;
        }
        foreach (GridViewRow gr in grdWarranty.Rows)
        {
            gr.Cells[0].Text = i.ToString();

            i++;
        }
    }

    public string strGroup = "";
    public void BindGrid()
    {
       DataTable dtWarranty = getTable();
        
        if (dtWarranty != null)
        {
            if (dtWarranty.Rows.Count > 0)
            {
                string strTractorUnderWarranty = Convert.ToString(System.Math.Round(Convert.ToDecimal(dtWarranty.Rows[0]["Tractor_Under_Warranty"].ToString()),2));
                lblTractorUnderWarranty.Text = "Average Tractor Under Warranty: "+ strTractorUnderWarranty;
                DataView dv = new DataView(dtWarranty);
                grdWarranty.DataSource = dtWarranty;
                grdWarranty.DataBind();
                btnExport.Visible = true;
                foreach (GridViewRow gr in grdWarranty.Rows)
                {
                    if (gr.Cells[1].Text == "Total")
                    {
                        gr.Cells[6].Text = "";
                        gr.Cells[7].Text = "";
                        gr.BackColor = System.Drawing.Color.Aquamarine;
                        gr.Font.Bold = true;
                    }
                }
            }
            else
            {
                lblTractorUnderWarranty.Text = "";
                btnExport.Visible = false;
                grdWarranty.DataSource = null;
                grdWarranty.DataBind();
            }
        }
        else
        {
            lblTractorUnderWarranty.Text = "";
            btnExport.Visible = false;
            grdWarranty.DataSource = null;
            grdWarranty.DataBind();
        }
    }
    private void helper_GroupHeader(string groupName, object[] values, GridViewRow row)
    {
        if (groupName == strGroup)
        {
            row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
            row.Cells[0].Text = "Model:" + values[0].ToString();
            row.BackColor = System.Drawing.Color.Aqua;
        }
    }
    public DataTable getTable()
    {
        string strModelParameter = "";
        int modelflag = 0;

        if (drpModel.SelectedValue != "0")
        {
            strModelParameter += " GroupID=" + drpModel.SelectedValue + " ";
        }
        else
        {
            strModelParameter += "";
        }
        modelflag++;
        string strModelCategoryParameter = "";
        int modelcategoryflag = 0;
        foreach (ListItem list in chkCategory.Items)
        {
            if (list.Selected)
            {
                if (modelcategoryflag > 0)
                {
                    strModelCategoryParameter += " or ";
                }
                strModelCategoryParameter += " ModelCategoryID =" + list.Value;
                modelcategoryflag++;
            }

        }

        string strModelClutchParameter = "";
        int modelclutchflag = 0;
        foreach (ListItem list in chkClutchType.Items)
        {
            if (list.Selected)
            {
                if (modelclutchflag > 0)
                {
                    strModelClutchParameter += " or ";
                }
                strModelClutchParameter += " ClutchTypeID =" + list.Value;
                modelclutchflag++;
            }

        }


        string strModelSpecialParameter = "";
        int modelspecialflag = 0;
        foreach (ListItem list in chkSpecialList.Items)
        {
            if (list.Selected)
            {
                if (modelspecialflag > 0)
                {
                    strModelSpecialParameter += " or ";
                }
                if (list.Value == "0")
                {
                    strModelSpecialParameter +=  " ModelSpecialID is null or ModelSpecialID=0 ";
                }
                else
                {
                    strModelSpecialParameter += " ModelSpecialID =" + list.Value;
                }
                modelspecialflag++;
            }

        }


        string strEngineParameter = "";
        
        if (rdoData.SelectedValue == "0")
        {
            if (rdoAlwar.Checked)
            {
                strEngineParameter = "  (IsEngine=1 and Engine=''A'')";
            }
            else
            {
                if (rdoBhopal.Checked)
                {
                    strEngineParameter = "((Engine=''A'' and IsEngine=0) or Engine=''s'') ";
                }
                else
                {
                    strEngineParameter = "(IsEngine=0 or IsEngine=1)  ";
                }
            }
            
        }
        else if (rdoData.SelectedValue == "1")
        {
            if (rdoAlwarEngine.Checked)
            {
                strEngineParameter = "  (IsEngine=1 and Engine=''A'')";
            }
            else
            {
                if (rdoSimpsonEngine.Checked)
                {
                    strEngineParameter = " (Engine=''S'' and IsEngine=1) ";
                }
                else
                {
                    strEngineParameter = " (IsEngine=1)  ";
                }
            }
            
        }
        else
        {
            strEngineParameter = " (IsEngine=0)  ";           
        }

        string strRegionParameter = "";
        int regionflag = 0;
        foreach (ListItem list in chkRegion.Items)
        {
            if (list.Selected)
            {
                if (regionflag > 0)
                {
                    strRegionParameter += " or ";
                }
                strRegionParameter += " RegionID =" + list.Value;
                regionflag++;
            }

        }

        if (chkRegionAll.Checked)
        {
            strRegionParameter += " or RegionID is null ";
        }


        string strYearParameter = rdoYear.SelectedValue;

        string strQuery = "execute usp_TopContributors   '" + strModelParameter + "','" + strModelCategoryParameter + "','" + strModelClutchParameter + "','" + strModelSpecialParameter + "','" + drpFromMonth.SelectedValue + "','" + drpFromYear.SelectedValue + "','" + drpToMonth.SelectedValue + "','" + drpToYear.SelectedValue + "','" + strEngineParameter + "','" + strYearParameter + "','" + txtContributors.Text.Trim() + "'," + rdoHMR_Range.SelectedValue + ",'" + strRegionParameter + "'";
        DataSet ds = objQueryController.ExecuteMultiTableQuery(strQuery); ;
        DataTable dt = new DataTable();
        dt = ds.Tables[1];
        return dt;

    }

    

    protected void OnRowCreated(object sender, GridViewRowEventArgs e)
    {
       

    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        string str = "";
        string strParameter = "";
        GridViewExport objExport = new GridViewExport();


        strParameter = strParameter + getchkList(chkCategory, "Category");
        strParameter = strParameter + getchkList(chkClutchType, "ClutchType");
        strParameter = strParameter + getchkList(chkSpecialList, "Special");
        str = str + "<table width='50%' border='1' cellpadding='0' cellspacing='0'>";
        str = str + "<tr><td style='font-size:small;font-weight:bold;'>Report For:</td><td style='font-size:small;font-weight:bold;'>Top Contributors</td></tr>";
        str = str + "</table>";
        str = str + strParameter;
        str = str + "<br/><table width='50%' border='1' cellpadding='0' cellspacing='0'>";
        str = str + "<tr><td >From Period:</td>";
        str = str + "<td style='font-size:small;font-weight:bold;'>" + drpFromMonth.SelectedItem.Text.ToString().Replace("Select All", "All") + "</td>";
        str = str + "<td style='font-size:small;font-weight:bold;'>" + drpFromYear.SelectedItem.Text.ToString().Replace("Select All", "All") + "</td>";
        str = str + " <td>To Period:</td>";
        str = str + "<td style='font-size:small;font-weight:bold;'>" + drpToMonth.SelectedItem.Text.ToString().Replace("Select All", "All") + "</td>";
        str = str + "<td style='font-size:small;font-weight:bold;'>" + drpToYear.SelectedItem.Text.ToString().Replace("Select All", "All") + "</td>";
        str = str + "<td >Model:</td>";
        str = str + "<td style='font-size:small;font-weight:bold;'>" + drpModel.SelectedItem.Text.ToString().Replace("Select All", "All") + "</td></tr>";
        if (rdoData.SelectedValue == "0")
        {
            str = str + "<tr><td style='font-size:small;font-weight:bold;'>" + rdoData.SelectedItem.Text.ToString() + "</td></tr>";
            str = str + "<tr><td >Place:</td>";
            if (rdoAlwar.Checked)
            {
                str = str + "<td style='font-size:small;font-weight:bold;'>" + rdoAlwar.Text.ToString() + "</td>";
            }
            else if (rdoBhopal.Checked)
            {
                str = str + "<td style='font-size:small;font-weight:bold;'>" + rdoBhopal.Text.ToString() + "</td>";
            }
            else if (rdoAllPlace.Checked)
            {
                str = str + "<td style='font-size:small;font-weight:bold;'>" + rdoAllPlace.Text.ToString() + "</td>";
            }
            str = str + "</tr>";
        }
        else if (rdoData.SelectedValue == "1")
        {
            str = str + "<tr><td style='font-size:small;font-weight:bold;'>" + rdoData.SelectedItem.Text.ToString() + "</td></tr>";
            str = str + "<tr><td >Engine:</td>";
            if (rdoAlwarEngine.Checked)
            {
                str = str + "<td style='font-size:small;font-weight:bold;'>" + rdoAlwarEngine.Text.ToString() + "</td>";
            }
            else if (rdoSimpsonEngine.Checked)
            {
                str = str + "<td style='font-size:small;font-weight:bold;'>" + rdoSimpsonEngine.Text.ToString() + "</td>";
            }
            else if (rdoBothEngine.Checked)
            {
                str = str + "<td style='font-size:small;font-weight:bold;'>" + rdoBothEngine.Text.ToString() + "</td>";
            }
            str = str + "</tr>";
        }
        else
        {
            str = str + "<tr><td style='font-size:small;font-weight:bold;'>" + rdoData.SelectedItem.Text.ToString() + "</td></tr>";
        }
        str = str + "<tr><td >Year:</td>";
        str = str + "<td style='font-size:small;font-weight:bold;'>" + rdoYear.SelectedItem.Text.ToString().Replace("Select All", "All") + "</td>";
        str = str + "<td >Top Contributors:</td>";
        str = str + "<td style='font-size:small;font-weight:bold;'>" + txtContributors.Text.ToString() + "</td></tr>";
        str = str + "</table><br/>";
        hdnExport.Value = str + hdnExport.Value;
        objExport.ExportGridView(hdnExport.Value);

    }
    public string getchkList(CheckBoxList chkList, string chkListName)
    {

        string strParameter = "<h6>" + chkListName + "</h6> <table cellpadding='0' cellspacing='0' border='1' >";
        strParameter = strParameter + "<tr>";
        string strMiddleData = "";
        if (chkList != null)
        {
            int count = chkList.Items.Count;
            int Status = 0;
            if (chkList.Items.Count > 0)
            {
                foreach (ListItem list in chkList.Items)
                {
                    if (list.Selected)
                    {
                        Status++;
                        strMiddleData = strMiddleData + "<td> " + list.Text + " </td> ";
                    }
                }
            }
            if (Status == count)
            {
                strMiddleData = "<td> " + chkListName + " </td> <td> All </td>";
            }
        }
        strParameter = strParameter + strMiddleData;
        strParameter = strParameter + "</tr></table>";
        return strParameter;
    }

    
}
