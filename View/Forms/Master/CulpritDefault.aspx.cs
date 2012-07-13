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

public partial class View_Forms_Master_CulpritDefault : System.Web.UI.Page
{
    QueryConroller objQuerycontroller = new QueryConroller();
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
        strQuery = "select * from vw_CulpritWholeDetail order by Code";
        dtinformation = objQuerycontroller.ExecuteQuery(strQuery);
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
        Response.Redirect(strProjectName+"/View/Forms/Master/Culprit.aspx");
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

    protected void Role_RowDeleted(Object sender, GridViewDeleteEventArgs e)
    {

        string strQuery = "";
        DataTable dtinformation = new DataTable();
        strQuery = " execute usp_DeleteCulpritCode  " + hdnCodeID.Value + "";
        dtinformation = objQuerycontroller.ExecuteQuery(strQuery);
        if (Convert.ToInt32(dtinformation.Rows[0]["RowCount"]) > 0)
        {
            string strjscript = "<script language='javascript'>";
            strjscript += " setLabelText('ctl00_ContentPlaceHolder1_lblMessage','Record Deleted Successfully..' );";
            strjscript += "</script" + ">";
            Literal1.Text = strjscript;
            bindData();
        }
        else
        {
            string strjscript = "<script language='javascript'>";
            strjscript += " setLabelText('ctl00_ContentPlaceHolder1_lblMessage','Record is in use cannot deleted..' );";
            strjscript += "</script" + ">";
            Literal1.Text = strjscript;
        }

    }
}
