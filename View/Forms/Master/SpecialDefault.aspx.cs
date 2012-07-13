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

public partial class View_Forms_Master_SpecialDefault : System.Web.UI.Page
{
    QueryConroller objQueryController = new QueryConroller();
    DataTable dtModelSpecialDetails = new DataTable();
    public string strfieldset = "";
    string query;
    int UpdateId;
    protected void Page_Load(object sender, EventArgs e)
    {
        strfieldset = "Special Details";
        if (!IsPostBack)
        {
            Bindgrid();
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {

            query = "insert into ModelSpecialDetails (ModelSpecial) values ('" + txtModelSpecial.Text + "')";
        }
        else
        {
            query = "Update ModelSpecialDetails set ModelSpecial='" + txtModelSpecial.Text + "' Where ModelSpecialID=" + hdnModelSpecialID.Value;
        }
         objQueryController.ExecuteQuery(query);

        Bindgrid();
        pnlAdd.Visible = false;
        pnlgrid.Visible = true;
        btnSave.Text = "Save";
        btnSave.ToolTip = "Save";
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        txtModelSpecial.Text = "";
        pnlAdd.Visible = true;
        pnlgrid.Visible = false;
        strfieldset = "Special";
    }
    public void Bindgrid()
    {
        dtModelSpecialDetails=objQueryController.ExecuteQuery("Select * from ModelSpecialDetails");
        gridDefault.DataSource = dtModelSpecialDetails;
        gridDefault.DataBind(); 
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
     
    protected void gridDefault_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string deleteID = gridDefault.DataKeys[e.RowIndex].Values[0].ToString();
        
        query = "Delete From ModelSpecialDetails where ModelSpecialID=" + deleteID;
        objQueryController.ExecuteQuery(query);
        Bindgrid();
    }
    protected void gridDefault_RowEditing(object sender, GridViewEditEventArgs e)
    {
         
        int EditID = Convert.ToInt32(gridDefault.DataKeys[e.NewEditIndex].Value);
        UpdateId = EditID;
        EditRecord(EditID);
        e.Cancel = true;
    }

    public void EditRecord(int ID)
    {
        hdnModelSpecialID.Value = ID.ToString();
        btnSave.Text = "Update";
        btnSave.ToolTip = "Update";
        DataTable dtFill = new DataTable();
        query = "Select * from ModelSpecialDetails where ModelSpecialID=" + ID;
        dtFill = objQueryController.ExecuteQuery(query);
        txtModelSpecial.Text = dtFill.Rows[0]["ModelSpecial"].ToString();
        pnlAdd.Visible = true;
        pnlgrid.Visible = false ;


    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        pnlgrid.Visible = true;
        pnlAdd.Visible = false;
    }
}
