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

public partial class View_Forms_Master_ModelDefaultNew : System.Web.UI.Page
{
    QueryConroller objQueryController = new QueryConroller();
    public string strProjectName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        strProjectName = System.Configuration.ConfigurationManager.AppSettings["WMSProjectName"];
        if (!IsPostBack)
        {
            bindData();
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
        strQuery = "select * from vw_ModelMappingMaster order by GroupID ";
        dtinformation = objQueryController.ExecuteQuery(strQuery);
        if (dtinformation != null)
        {
            if (dtinformation.Rows.Count > 0)
            {
                gridDefault.DataSource = dtinformation;
                gridDefault.DataBind();
                

            }
            else
            {
                gridDefault.DataSource = null;
                gridDefault.EmptyDataText = "no data found";
                gridDefault.DataBind();

            }
        }
        else
        {
            gridDefault.DataSource = null;
            gridDefault.EmptyDataText = "no data found";
            gridDefault.DataBind();

        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string strjscript = "<script language='javascript'>";
        strjscript += " setMessageText('ctl00_ContentPlaceHolder1_lblMessage','' );";
        strjscript += "</script" + ">";
        Literal1.Text = strjscript;
        Response.Redirect(strProjectName+"/View/Forms/Master/ModelNew.aspx");
    }

    public void eventhandlerSerialNo(object Sender, EventArgs E)
    {
        int i = 1;
        int pi = gridDefault.PageIndex;
        int ps = gridDefault.PageSize;
        //<><> Use Name of Your GridView Instead Of gvDetailProspect <><>// 
        if (pi > 0)
        {
            i = pi * ps + 1;
        }
        foreach (GridViewRow gr in gridDefault.Rows)
        {
            gr.Cells[0].Text = i.ToString();

            i++;
        }
    }

    protected void Model_RowDeleted(Object sender, GridViewDeleteEventArgs e)
    {
        int rindex = e.RowIndex;
        int DeleteID = Convert.ToInt32(((HiddenField)gridDefault.Rows[rindex].FindControl("hdnCode")).Value);
        string strDeleteQuery = "Delete from ModelMapping where [ID]=" + DeleteID ;

        try
        {
            objQueryController.ExecuteQuery(strDeleteQuery);
            bindData();
            string str = "<script language = 'javascript'>";
            str += "fnSetLabelText('ctl00_ContentPlaceHolder1_lblmsg','Record Is In Use You Can Not Delete');";
            str += "</script>";
            Literal1.Text = str;
        }
        catch
        {
            string str = "<script language = 'javascript'>";
            str += "fnSetLabelText('ctl00_ContentPlaceHolder1_lblmsg','Record Is In Use You Can Not Delete');";
            str += "</script>";
            Literal1.Text = str;
        }


    }



}
