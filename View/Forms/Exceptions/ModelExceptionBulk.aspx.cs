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

public partial class View_Forms_Exceptions_ModelExceptionBulk : System.Web.UI.Page
{
    QueryConroller objQuerycontroller = new QueryConroller();
    ConfiguratorUI objcUI = new ConfiguratorUI();
    ConfiguratorController objController = new ConfiguratorController();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            BindMaterial();
           
        }

    }

    public void BindMaterial()
    {
        string strQuery = "";
        DataTable dtProductCode = new DataTable();
        strQuery = "select distinct Material from ( ";
        strQuery += "select distinct Material from ProductionTemp where Material<>'' and IsModelEx=1";
        strQuery += " Union all ";
        strQuery += " select distinct [F10] as Material from SalesTemp where F10<>'' and IsModelEx=1) A";
        dtProductCode = objQuerycontroller.ExecuteQuery(strQuery);
        if (dtProductCode != null)
        {
            if (dtProductCode.Rows.Count > 0)
            {
                grdMaterial.DataSource = dtProductCode;
                grdMaterial.DataBind();
            }
            else
            {
                grdMaterial.DataSource = dtProductCode;
                grdMaterial.DataBind();
            }
        }
        else
        {
            grdMaterial.DataSource = dtProductCode;
            grdMaterial.DataBind();
        }
    }
    public void eventhandlerSerialNo(object Sender, EventArgs E)
    {
        int i = 1;
        int pi = grdMaterial.PageIndex;
        int ps = grdMaterial.PageSize;
        //<><> Use Name of Your GridView Instead Of gvDetailProspect <><>// 
        if (pi > 0)
        {
            i = pi * ps + 1;
        }
        foreach (GridViewRow gr in grdMaterial.Rows)
        {
            gr.Cells[0].Text = i.ToString();

            i++;
        }
    }
   
}
