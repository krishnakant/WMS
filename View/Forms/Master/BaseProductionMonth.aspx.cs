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

public partial class View_Forms_Master_BaseProductionMonth : System.Web.UI.Page
{

      QueryConroller objQuerycontroller = new QueryConroller();
      public string strProjectName = "";
      protected void Page_Load(object sender, EventArgs e)
      {
           strProjectName = System.Configuration.ConfigurationManager.AppSettings["WMSProjectName"];
        if(!IsPostBack)
        {
             getDetail();
        }
      
     }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string strQuery = "";
        DataTable dtinformation = new DataTable();

        strQuery = " update ProductionMonth set BaseProductionMonth_Code='" + txtCode.Text + "',Month_ID='" + drpMonth.SelectedValue + "',BaseProductionMonth='" + drpMonth.SelectedItem.Text+ "-" + drpYear.SelectedValue + "',Year_ID='" + drpYear.SelectedValue + "' ";
        dtinformation = objQuerycontroller.ExecuteQuery(strQuery);
    }


    public void getDetail()
    {
        string strQuery = "";
        DataTable dtinformation = new DataTable();
        strQuery = "select * from ProductionMonth";
        dtinformation = objQuerycontroller.ExecuteQuery(strQuery);
        if (dtinformation != null)
        {

            if (dtinformation.Rows.Count > 0)
            {
                foreach (DataRow drinformation in dtinformation.Rows)
                {
                    txtCode.Text = drinformation["BaseProductionMonth_Code"].ToString();
                    drpMonth.SelectedValue = drinformation["Month_ID"].ToString();
                    drpYear.SelectedValue = drinformation["Year_ID"].ToString();




                }
            }
        }
    }

   
        


}
