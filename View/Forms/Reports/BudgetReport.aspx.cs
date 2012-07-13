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

public partial class View_Forms_Reports_BudgetReport : System.Web.UI.Page
{
    QueryConroller objQueryController = new QueryConroller();
    private GridViewHelper helper;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindModel();
            BindModelCategory();
            BindModelClutch();
            BindModelSpecial();
           
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
                chkModelCodeList.DataSource = dtModel;
                chkModelCodeList.DataValueField = "ModelGroupName";
                chkModelCodeList.DataTextField = "ModelGroupName";
                chkModelCodeList.DataBind();
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

    public string strGroup = "";
    public void BindGrid()
    {
        DataTable dtCost = getTable();
        if (dtCost != null)
        {
            if (dtCost.Rows.Count > 0)
            {
                btnExport.Visible = true;
                grdCostReport.DataSource = dtCost;

                BoundField bnd = new BoundField();
                bnd.DataField = "Field";
                bnd.HeaderText = "Model";
                grdCostReport.Columns.Add(bnd);

                string strQuarterQuery = "Select distinct Quarter from vw_QuarterWiseBudgetCost";
                DataTable dtQuarter = objQueryController.ExecuteQuery(strQuarterQuery);
                if (dtQuarter != null)
                {
                    if (dtQuarter.Rows.Count > 0)
                    {
                        foreach (DataRow drQuarter in dtQuarter.Rows)
                        {
                            BoundField bndf = new BoundField();
                            bndf.DataField = drQuarter["Quarter"].ToString();
                            bndf.HeaderText = drQuarter["Quarter"].ToString();
                            grdCostReport.Columns.Add(bndf);
                        }
                    }
                } 
                strGroup = "ModelGroupName";
                helper = new GridViewHelper(this.grdCostReport, false);
                string[] cols = new string[1];
                cols[0] = strGroup;
                helper.RegisterGroup(cols, true, true);
                helper.GroupHeader += new GroupEvent(helper_GroupHeader);
                grdCostReport.DataBind();
                
            }
            else
            {
                btnExport.Visible = false;
                grdCostReport.DataSource = null;
                grdCostReport.DataBind();
                
            }
        }
        else
        {
            btnExport.Visible = false;
            grdCostReport.DataSource = null;
            grdCostReport.DataBind();
        }
    }



    
    

    private void helper_GroupHeader(string groupName, object[] values, GridViewRow row)
    {
        if (groupName == strGroup)
        {
            row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
            row.Cells[0].Text = "" + values[0].ToString();
            row.BackColor = System.Drawing.Color.Aqua;
        }
    }

    public DataTable getTable()
    {
        string strModelParameter = "";
        int modelflag = 0;
        foreach (ListItem list in chkModelCodeList.Items)
        {
            if (list.Selected)
            {
                if (modelflag > 0)
                {
                    strModelParameter += " or ";
                }
                strModelParameter += " ModelGroupName =''" + list.Value + "'' ";
                modelflag++;
            }

        }

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
                    strModelSpecialParameter += " ModelSpecialID is null ";
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

        string strYear = rdoYear.SelectedValue;
        string strQuery = "";


//        strQuery = "exec usp_BudgetCost '" + strModelParameter + "','" + strModelCategoryParameter + "','" + strModelClutchParameter + "','" + strModelSpecialParameter + "','" + strEngineParameter + "','" + strYear + "'"; ;
        strQuery = "exec usp_BudgetCost_Test '" + strModelParameter + "','" + strModelCategoryParameter + "','" + strModelClutchParameter + "','" + strModelSpecialParameter + "','" + strEngineParameter + "','" + strYear + "'"; ;


        //usp_QuarterProductionCostDetailsNew
        DataTable dt = objQueryController.ExecuteQuery(strQuery);
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

        strParameter = strParameter + getchkList(chkModelCodeList, "Model");
        strParameter = strParameter + getchkList(chkCategory, "Category");
        strParameter = strParameter + getchkList(chkClutchType, "ClutchType");
        strParameter = strParameter + getchkList(chkSpecialList, "Special");
        str = str + "<table width='50%' border='1' cellpadding='0' cellspacing='0'>";
        str = str + "<tr><td style='font-size:small;font-weight:bold;'>Report For:</td><td style='font-size:small;font-weight:bold;'>Budget Report</td></tr>";
        str = str + "</table>";
        str = str + strParameter;
        str = str + "<br/><table width='50%' border='1' cellpadding='0' cellspacing='0'>";
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
        str = str + "<td style='font-size:small;font-weight:bold;'>" + rdoYear.SelectedItem.Text.ToString().Replace("Select All", "All") + "</td></tr>";
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
