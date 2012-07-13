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

public partial class View_Forms_Configurator_MonthOpenClose : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string strMessage = "";
        MonthOpenCloseController objController = new MonthOpenCloseController();
        MonthOpenCloseUI objUI = new MonthOpenCloseUI();
        try
        {
            int MonthID = Convert.ToInt16(drpMonth.SelectedValue);
            int YearID = Convert.ToInt16(drpYear.SelectedItem.Text);
            int Status = 0;
            if (chkOpenClose.Checked)
            {
                Status = 1;
            }
            else
            {
                Status = 0;
            }

            objUI.MonthID = MonthID;
            objUI.YearID = YearID;
            objUI.Status = Status;

            objController.SaveMonthOpenClose(objUI);
            strMessage = "Record saved successfully";
            string strjscript = "<script language='javascript'>";
            strjscript += " setMessageText('ctl00_ContentPlaceHolder1_lblMessage','" + strMessage + "' );";
            strjscript += "</script" + ">";
            Literal1.Text = strjscript;
        }
        catch(Exception ex)
        {
            strMessage = ex.Message;
            string strjscript = "<script language='javascript'>";
            strjscript += " setMessageText('ctl00_ContentPlaceHolder1_lblMessage','" + strMessage + "' );";
            strjscript += "</script" + ">";
            Literal1.Text = strjscript;
        }
    }
}
