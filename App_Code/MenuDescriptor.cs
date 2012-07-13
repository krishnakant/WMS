using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for MenuDescriptor
/// </summary>
public class MenuDescriptor
{
    QueryConroller objQueryController = new QueryConroller();
   
	public MenuDescriptor()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public string GetMenu(int RoleID,int ModuleID)
    {
        string strMenu = "<ul id='navmenu'>";
        //string strQuery = "select distinct ExistsInID,MainCaption from vw_MenuGenerationDetail where ModuleID=" + ModuleID;
        string strQuery = "select distinct ExistsInID,MainCaption from vw_MenuGenerationDetail where ModuleID="+ModuleID+" and FormID in (select Distinct FormID from Priviledges where ModuleID=" + ModuleID + " and RoleID=" + RoleID+" and viewing=1)";
        DataTable dtMainMenu = objQueryController.ExecuteQuery(strQuery);
        if (dtMainMenu != null)
        {
            if (dtMainMenu.Rows.Count > 0)
            {
                foreach (DataRow drMainMenu in dtMainMenu.Rows)
                {
                    strMenu += "<li><a href='#'>" + drMainMenu["MainCaption"].ToString() + "</a><ul>";
                    string strMenuQuery = "select distinct FormID,Path,Caption from vw_MenuGenerationDetail where ExistsInID=" + drMainMenu["ExistsInID"].ToString() + " and FormID in (select Distinct FormID from Priviledges where ModuleID=" + ModuleID + " and RoleID=" + RoleID + " and viewing=1)";
                    DataTable dtMenu = objQueryController.ExecuteQuery(strMenuQuery);
                    if (dtMenu != null)
                    {
                        if (dtMenu.Rows.Count > 0)
                        {
                            foreach (DataRow drMenu in dtMenu.Rows)
                            {
                                strMenu += "<li><a href='" + drMenu["Path"].ToString() + "'>" + drMenu["Caption"].ToString() + "</a></li>";
                            }

                        }
                    }
                    strMenu += "</ul></li>";
                }
            }
        }

        strMenu += "</ul>";
        return strMenu;
    }
}
