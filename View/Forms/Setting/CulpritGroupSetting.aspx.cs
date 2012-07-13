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

public partial class View_Forms_Setting_CulpritGroupSetting : System.Web.UI.Page
{
    QueryConroller objQuerycontroller = new QueryConroller();
    ConfiguratorUI objcUI = new ConfiguratorUI();
    ConfiguratorController objController = new ConfiguratorController();
    public string strProjectName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        strProjectName = System.Configuration.ConfigurationManager.AppSettings["WMSProjectName"];

        if (!IsPostBack)
        {
            bindData();
        }

        string strjscript = "<script language='javascript' type='text/javascript'>";
        strjscript += " setLabelText('ctl00_ContentPlaceHolder1_lblMessage','' );";
        strjscript += "</script" + ">";
        literal1.Text = strjscript;

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
        strQuery = "select CulpritGroupID,GRoupName from CulpritGroup order by GRoupName";
        dtinformation = objQuerycontroller.ExecuteQuery(strQuery);
        if (dtinformation != null)
        {
            if (dtinformation.Rows.Count > 0)
            {
                GridView1.DataSource = dtinformation;
                GridView1.DataBind();

            }
            else
            {
                GridView1.DataSource = null;
                GridView1.EmptyDataText = "no data found";
                GridView1.DataBind();
            }
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.EmptyDataText = "no data found";
            GridView1.DataBind();
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
    public void Record_cancel(Object sender, GridViewCancelEditEventArgs e)
    {

        GridView1.EditIndex = -1;
        bindData();

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveGroupName();
    }
    public void saveGroupName()
    {

        int GroupID = 0;
        objcUI.GroupName = txtGroupName.Text.Trim(); ;
        GroupID = objController.SaveCulpritGroup(objcUI, null);
        if (GroupID == 0)
        {
            string strjscript = "<script language='javascript' type='text/javascript'>";
            strjscript += " setLabelText('ctl00_ContentPlaceHolder1_lblMessage','group already exists' );";
            strjscript += "</script" + ">";
            literal1.Text = strjscript;
        }
        else
        {
            string strjscript = "<script language='javascript' type='text/javascript'>";
            strjscript += " setLabelText('ctl00_ContentPlaceHolder1_lblMessage','Record Save Sccessfully' );";
            strjscript += "</script" + ">";
            literal1.Text = strjscript;
            txtGroupName.Text = "";
            bindData();

        }


    }

    protected void Role_RowDeleted(Object sender, GridViewDeleteEventArgs e)
    {
        DeletCulGroup();

    }
    public void DeletCulGroup()
    {
        int StatusID = 0;
        objcUI.source = "Culprit";
        objcUI.GroupID = Convert.ToInt32(hdngupID.Value);
        StatusID = objController.DeleteGroupName(objcUI, null);
        if (StatusID == 1)
        {
            string strjscript = "<script language='javascript' type='text/javascript'>";
            strjscript += " setLabelText('ctl00_ContentPlaceHolder1_lblMessage','Record Deleted Sccessfully' );";
            strjscript += "</script" + ">";
            literal1.Text = strjscript;
            bindData();
        }
        else
        {
            string strjscript = "<script language='javascript' type='text/javascript'>";
            strjscript += " setLabelText('ctl00_ContentPlaceHolder1_lblMessage','Record is in use cannot deleted..' );";
            strjscript += "</script" + ">";
            literal1.Text = strjscript;
        }
    }

    public void Record_Edit(Object sender, GridViewEditEventArgs e)
    {

        GridView1.EditIndex = e.NewEditIndex;
        bindData();
    }

    public void Record_Updating(Object sender, GridViewUpdateEventArgs e)
    {

        int StatusID = 0;
        TextBox txtGroupName = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtGRoupName");
        string GroupName = txtGroupName.Text;
        DataKey dtKey = GridView1.DataKeys[e.RowIndex];
        if (dtKey["GRoupName"].ToString() != txtGroupName.Text.Trim())
        {
            objcUI.GroupName = GroupName;
            objcUI.source = "Culprit";
            objcUI.GroupID = Convert.ToInt32(dtKey["CulpritGroupID"].ToString());
            StatusID = objController.UpdateGroupName(objcUI, null);
        }
        else
        {
            StatusID = 1;
        }
        if (StatusID == 1)
        {
            string strjscript = "<script language='javascript' type='text/javascript'>";
            strjscript += " setLabelText('ctl00_ContentPlaceHolder1_lblMessage','Record Update Sccessfully...' );";
            strjscript += "</script" + ">";
            literal1.Text = strjscript;
            GridView1.EditIndex = -1;

            bindData();
        }
        else
        {
            string strjscript = "<script language='javascript' type='text/javascript'>";
            strjscript += " setLabelText('ctl00_ContentPlaceHolder1_lblMessage','group already exists ' );";
            strjscript += "</script" + ">";
            literal1.Text = strjscript;
        }


    }

}
