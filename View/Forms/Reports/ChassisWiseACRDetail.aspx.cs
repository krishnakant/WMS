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
using System.Collections.Generic;
public partial class View_Forms_Reports_ChassisWiseACRDetail : System.Web.UI.Page
{
    private GridViewHelper helper;
    private List<int> mQuantities = new List<int>();
    QueryConroller objQueryController = new QueryConroller();
    public string strProjectName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        strProjectName = System.Configuration.ConfigurationManager.AppSettings["WMSProjectName"];

        if (!IsPostBack)
        {
          
            checkException();

            getACRDetail();
        }
        else
        {
            string strjscript = "<script language='javascript' type='text/javascript'>";
            strjscript += "";
            strjscript += "</script" + ">";
            literal1.Text = strjscript;

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
    public void checkException()
    {
        int countRow =objQueryController.getNoOfException("execute usp_countException");
        if (countRow > 0)
        {
            string strjscript = "<script language='javascript' type='text/javascript'>";
            strjscript += " checkException();";
            strjscript += "</script" + ">";
            literal1.Text = strjscript;

        }
    }
  

    public string strGroup = "";
    public void getACRDetail()
    {
        DataTable dtGridData = getTable();
      
        grdacrData.Visible = true;
      
        grdacrData.DataSource = dtGridData;
       

        //helper = new GridViewHelper(this.grdacrData, false);
        //string[] cols = new string[1];
        //cols[0] = strGroup;
        //helper.RegisterGroup(cols, true, true);
        //helper.GroupHeader += new GroupEvent(helper_GroupHeader);
        //helper.RegisterSummary("Value", SummaryOperation.Sum, strGroup);
        //helper.RegisterSummary("Quantity", SummaryOperation.Sum, strGroup);
        //helper.GroupSummary += new GroupEvent(helper_GroupSummary);
        grdacrData.DataBind();
        if (dtGridData != null)
        {
            if (dtGridData.Rows.Count > 0)
            {
                btnPrint.Visible = true;
                btnExport.Visible = true;
            }
            else
            {
                btnPrint.Visible = false;
                btnExport.Visible = false;
            }
        }
        else
        {
            btnPrint.Visible = false;
            btnExport.Visible = false;
        }

    }

    public DataTable getTable()
    {
        DataTable dtGridData = new DataTable();
        string strQuery = "";
        string strwhrprm = "";
        int flag = 0;
        if (rdoData.SelectedValue == "0")
        {
            if (rdoAlwar.Checked)
            {
                strwhrprm = "  (IsEngine=1 and Engine='A')";
            }
            else
            {
                if (rdoBhopal.Checked)
                {
                    strwhrprm = "((Engine='A' and IsEngine=0) or Engine='s') ";
                }
                else
                {
                    strwhrprm = "(IsEngine=0 or IsEngine=1)  ";
                }
            }
            flag = 1;
        }
        else if (rdoData.SelectedValue == "1")
        {
            if (rdoAlwarEngine.Checked)
            {
                strwhrprm = "  (IsEngine=1 and Engine='A')";
            }
            else
            {
                if (rdoSimpsonEngine.Checked)
                {
                    strwhrprm = " (Engine='S' and IsEngine=1) ";
                }
                else
                {
                    strwhrprm = " (IsEngine=1)  ";
                }
            }
            flag = 1;
        }
        else
        {
            strwhrprm = " (IsEngine=0)  ";
            flag = 1;
        }



        if (rdoPrimary.Checked)
        {
            if (flag == 1)
            {
                strwhrprm += " and ";
            }
            strwhrprm += " DT like 'P%' ";
        }
        else if (rdoConsequences.Checked)
        {
            if (flag == 1)
            {
                strwhrprm += " and ";
            }
            strwhrprm += " DT like 'C%' ";
            flag = 1;
        }
        if (rdoFirst.Checked)
        {
            if (flag == 1)
            {
                strwhrprm += " and ";
            }
            strwhrprm += "  DATEDIFF(MM, INS_DATE, DEF_DATE) >= 0 AND DATEDIFF(MM, INS_DATE, DEF_DATE) < 13 ";
        }
        else if (rdoSecond.Checked)
        {
            if (flag == 1)
            {
                strwhrprm += " and ";
            }
            strwhrprm += " DATEDIFF(MM, INS_DATE, DEF_DATE) > 12 AND DATEDIFF(MM, INS_DATE, DEF_DATE) < 25 ";
        }

        if (txtChassisNo.Text.Trim() == "")
        {
            strQuery = "select *,'All' as Model from acrbulk where " + strwhrprm + "  order by TRACTOR_NO";
            strGroup = "Model";
        }
        else
        {
            strQuery = "select * from acrbulk where Tractor_No like '" + txtChassisNo.Text.Trim() + "%' and " + strwhrprm + " order by TRACTOR_NO";
            strGroup = "Tractor_No";
        }
        dtGridData = objQueryController.ExecuteQuery(strQuery);
        return dtGridData;
    }

    public void grdacrData_Paging(Object sender, GridViewPageEventArgs e)
    {
        DataTable dtGridData = getTable();
        grdacrData.PageIndex = e.NewPageIndex;
        grdacrData.DataSource = dtGridData;
        grdacrData.DataBind();

    }


    private void helper_GroupHeader(string groupName, object[] values, GridViewRow row)
    {
        if (groupName == strGroup)
        {
            row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
            row.Cells[0].Text = "Chassis No. :" + values[0].ToString();
            row.BackColor = System.Drawing.Color.Yellow;
        }
    }
    private void helper_GroupSummary(string groupName, object[] values, GridViewRow row)
    {
        row.Cells[0].HorizontalAlign = HorizontalAlign.Right;
        row.Cells[0].Text = "Total:";
        row.Cells[0].ForeColor = System.Drawing.Color.Black;
    }
    public void eventhandlerSerialNo(object Sender, EventArgs E)
    {
        int i = 1;
        int pi = grdacrData.PageIndex;
        int ps = grdacrData.PageSize;
        if (pi > 0)
        {
            i = pi * ps + 1;
        }
        foreach (GridViewRow gr in grdacrData.Rows)
        {
            gr.Cells[0].Text = i.ToString();

            i++;
        }
    }
    /***************************************************************************************************/
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    /*******************************Exporting Record Into Excel*****************************************/
    protected void Button1_Click(object sender, EventArgs e)
    {
        string str = "";
        string strParameter = "";
        GridViewExport objExport = new GridViewExport();


        str = str + "<table width='50%' border='1' cellpadding='0' cellspacing='0'>";
        str = str + "<tr><td style='font-size:small;font-weight:bold;'>Report For:</td><td style='font-size:small;font-weight:bold;'>Chassis Wise ACR Detail</td></tr>";
        str = str + "<tr><td > Chassis:</td>";
        str = str + "<td style='font-size:small;font-weight:bold;'>" + txtChassisNo.Text.ToString() + "</td></tr>";
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
        str = str + "<tr><td >Problem Type:</td>";
        if (rdoPrimary.Checked)
        {
            str = str + "<td style='font-size:small;font-weight:bold;'>" + rdoPrimary.Text.ToString() + "</td>";
        }
        else if (rdoConsequences.Checked)
        {
            str = str + "<td style='font-size:small;font-weight:bold;'>" + rdoConsequences.Text.ToString() + "</td>";
        }
        else if (rdoAllProblem.Checked)
        {
            str = str + "<td style='font-size:small;font-weight:bold;'>" + rdoAllProblem.Text.ToString() + "</td>";
        }
        str = str + "<td >Year:</td>";
        if (rdoFirst.Checked)
        {
            str = str + "<td style='font-size:small;font-weight:bold;'>" + rdoFirst.Text.ToString() + "</td>";
        }
        else if (rdoSecond.Checked)
        {
            str = str + "<td style='font-size:small;font-weight:bold;'>" + rdoSecond.Text.ToString() + "</td>";
        }
        else if (rdoBothYear.Checked)
        {
            str = str + "<td style='font-size:small;font-weight:bold;'>" + rdoBothYear.Text.ToString() + "</td></tr>";
        }

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
    /***************************************************************************************************/
    protected void btnGo_Click1(object sender, EventArgs e)
    {
        getACRDetail();
    }
}
